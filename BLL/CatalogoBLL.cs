using System;
using System.Collections.Generic;

namespace BLL
{
    public class CatalogoBLL : Interfaces.ICatalogoService
    {
        private readonly DAL.UnidadDeVentaDAL      _dalUnidad   = new DAL.UnidadDeVentaDAL();
        private readonly DAL.ArticuloIndividualDAL _dalArticulo = new DAL.ArticuloIndividualDAL();
        private readonly DAL.LoteDAL               _dalLote     = new DAL.LoteDAL();
        private readonly Servicios.Bitacora        _bitacora    = new Servicios.Bitacora();

        public List<BE.UnidadDeVenta> ObtenerTodos() => _dalUnidad.ObtenerTodos();

        public BE.UnidadDeVenta ObtenerPorId(int id) => _dalUnidad.ObtenerPorId(id);

        // RF-01 / Alta de articulo individual.
        public int AltaArticulo(BE.ArticuloIndividual articulo)
        {
            ValidarArticulo(articulo);
            articulo.FechaAlta = DateTime.Now;
            int id = _dalArticulo.Alta(articulo);
            articulo.Id = id;
            _bitacora.Registrar("Catálogo", $"Alta artículo: {articulo.Nombre} (${articulo.ValorDeclarado:N2})", BE.Criticidad.Baja);
            return id;
        }

        // Modificacion de articulo.
        public void ModificarArticulo(BE.ArticuloIndividual articulo)
        {
            ValidarArticulo(articulo);
            _dalArticulo.Modificar(articulo);
            // Recalcular precio base del lote padre si pertenece a uno.
            if (articulo.IdLotePadre.HasValue)
                RecalcularPrecioLote(articulo.IdLotePadre.Value);
            _bitacora.Registrar("Catálogo", $"Modificación artículo ID {articulo.Id}: {articulo.Nombre}", BE.Criticidad.Baja);
        }

        // RF-01 / Alta de lote.
        public int AltaLote(BE.Lote lote)
        {
            if (string.IsNullOrWhiteSpace(lote.Nombre))
                throw new Exception("El nombre del lote es obligatorio.");
            lote.FechaAlta = DateTime.Now;
            int id = _dalLote.Alta(lote);
            lote.Id = id;
            _bitacora.Registrar("Catálogo", $"Alta lote: {lote.Nombre}", BE.Criticidad.Baja);
            return id;
        }

        // RF-02: agrega una unidad (articulo u otro lote) a un lote padre.
        public void AgregarALote(int idLote, int idUnidad)
        {
            var lote   = _dalLote.ObtenerPorId(idLote);
            var unidad = _dalUnidad.ObtenerPorId(idUnidad);

            if (lote   == null) throw new Exception($"Lote {idLote} no encontrado.");
            if (unidad == null) throw new Exception($"Unidad {idUnidad} no encontrada.");
            if (unidad.IdLotePadre.HasValue)
                throw new Exception($"La unidad '{unidad.Nombre}' ya pertenece a otro lote.");
            if (idUnidad == idLote)
                throw new Exception("Un lote no puede contenerse a sí mismo.");

            // Asignar padre y recalcular precio del lote.
            _dalUnidad.AsignarLotePadre(idUnidad, idLote);
            RecalcularPrecioLote(idLote);
            _bitacora.Registrar("Catálogo", $"Unidad '{unidad.Nombre}' agregada al lote '{lote.Nombre}'", BE.Criticidad.Baja);
        }

        // Baja logica de una unidad (no se puede dar de baja si tiene subasta activa).
        public void BajaUnidad(int id)
        {
            var substaDAL = new DAL.SubastaDAL();
            if (substaDAL.TieneSubastaActiva(id))
                throw new Exception("No se puede eliminar una unidad con una subasta activa en curso.");
            _dalUnidad.Baja(id);
            _bitacora.Registrar("Catálogo", $"Baja unidad ID {id}", BE.Criticidad.Media);
        }

        // RF-04: descripcion completa (recursiva si es Lote).
        public string ObtenerDescripcionCompleta(int id)
        {
            BE.UnidadDeVenta unidad = ConstruirArbolDesde(id);
            if (unidad == null) throw new Exception($"Unidad {id} no encontrada.");
            return unidad.ObtenerDescripcion();
        }

        // RF-03: precio base calculado recursivamente.
        public decimal ObtenerPrecioBase(int id)
        {
            BE.UnidadDeVenta unidad = ConstruirArbolDesde(id);
            if (unidad == null) throw new Exception($"Unidad {id} no encontrada.");
            return unidad.ObtenerPrecioBase();
        }

        // Construye el sub-arbol Composite desde un nodo raiz hacia abajo.
        private BE.UnidadDeVenta ConstruirArbolDesde(int id)
        {
            var nodo = _dalUnidad.ObtenerPorId(id);
            if (nodo == null) return null;

            if (nodo is BE.Lote lote)
            {
                var hijos = _dalUnidad.ObtenerHijosDeLote(id);
                foreach (var hijo in hijos)
                {
                    // Recursion: si el hijo es un lote, cargar sus hijos tambien.
                    if (hijo is BE.Lote)
                    {
                        var hijoCompleto = ConstruirArbolDesde(hijo.Id);
                        lote.AgregarComponente(hijoCompleto);
                    }
                    else
                    {
                        lote.AgregarComponente(hijo);
                    }
                }
            }
            return nodo;
        }

        // Recalcula y persiste el precio base de un lote tras cambios en sus componentes.
        private void RecalcularPrecioLote(int idLote)
        {
            var loteCompleto = ConstruirArbolDesde(idLote);
            if (loteCompleto == null) return;
            decimal nuevoPrecio = loteCompleto.ObtenerPrecioBase();
            _dalLote.ActualizarPrecioYCantidad(idLote, nuevoPrecio, loteCompleto.ObtenerHijos()?.Count ?? 0);

            // Si el lote tiene un padre, propagar el recalculo hacia arriba.
            var loteBase = _dalLote.ObtenerPorId(idLote);
            if (loteBase?.IdLotePadre.HasValue == true)
                RecalcularPrecioLote(loteBase.IdLotePadre.Value);
        }

        private void ValidarArticulo(BE.ArticuloIndividual a)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (string.IsNullOrWhiteSpace(a.Nombre))
                throw new Exception("El nombre del artículo es obligatorio.");
            if (a.ValorDeclarado <= 0)
                throw new Exception("El valor declarado debe ser mayor a cero.");
        }
    }
}
