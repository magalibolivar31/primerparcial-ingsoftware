using System;
using System.Collections.Generic;

namespace BLL
{
    public class SubastaBLL : Interfaces.ISubastaService
    {
        private readonly DAL.SubastaDAL      _dalSubasta = new DAL.SubastaDAL();
        private readonly DAL.PujaDAL         _dalPuja    = new DAL.PujaDAL();
        private readonly DAL.AdjudicacionDAL _dalAdj     = new DAL.AdjudicacionDAL();
        private readonly DAL.UnidadDeVentaDAL _dalUnidad  = new DAL.UnidadDeVentaDAL();
        private readonly Servicios.Bitacora  _bitacora   = new Servicios.Bitacora();

        // Singleton del GestorPujas — punto de acceso unico a la logica de pujas.
        private readonly GestorPujas _gestorPujas = GestorPujas.GetInstance();

        public List<BE.Subasta> ObtenerTodas() => _dalSubasta.ObtenerTodos();

        public List<BE.Subasta> ObtenerActivas() => _dalSubasta.ObtenerActivas();

        public BE.Subasta ObtenerPorId(int id) => _dalSubasta.ObtenerPorId(id);

        // Abre una nueva subasta sobre la unidad indicada.
        // Crea el GestorNotificaciones (Subject del Observer) para esta subasta.
        public int AbrirSubasta(int idUnidad, int idMartillero)
        {
            if (_dalSubasta.TieneSubastaActiva(idUnidad))
                throw new Exception("La unidad ya tiene una subasta activa en curso.");

            // Obtener precio base de la unidad (Composite: recursivo si es Lote).
            var catalogoBLL = new CatalogoBLL();
            decimal precioBase = catalogoBLL.ObtenerPrecioBase(idUnidad);

            var subasta = new BE.Subasta
            {
                IdUnidad      = idUnidad,
                IdMartillero  = idMartillero,
                Estado        = BE.EstadoSubasta.Activa,
                PrecioInicial = precioBase,
                PrecioVigente = precioBase,
                FechaApertura = DateTime.Now
            };

            int id = _dalSubasta.Abrir(subasta);
            subasta.Id = id;

            // Crear el GestorNotificaciones para esta subasta y registrarlo en el Singleton.
            var gestor = new Servicios.GestorNotificaciones(subasta);
            _gestorPujas.RegistrarGestor(id, gestor);

            _bitacora.Registrar("Subasta", $"Apertura subasta ID {id} — Unidad ID {idUnidad} — Precio inicial: ${precioBase:N2}", BE.Criticidad.Media);
            return id;
        }

        // Cierra la subasta, persiste la adjudicacion y notifica a todos los observers (RF-07).
        public void CerrarSubasta(int idSubasta, int idMartillero, string observaciones = null)
        {
            var subasta = _dalSubasta.ObtenerPorId(idSubasta);
            if (subasta == null)
                throw new Exception($"Subasta {idSubasta} no encontrada.");
            if (!subasta.EstaActiva)
                throw new Exception("Solo se pueden cerrar subastas en estado Activo.");

            // Determinar ganador: mejor puja aceptada.
            BE.Puja mejorPuja = _dalPuja.ObtenerMejorPuja(idSubasta);

            DateTime ahora = DateTime.Now;
            int?    idGanador   = mejorPuja?.IdPostor;
            decimal? precioFinal = mejorPuja?.Monto;

            // Cerrar en BD.
            _dalSubasta.Cerrar(idSubasta, idGanador, precioFinal, ahora);

            // Persistir adjudicacion si hubo ganador.
            if (mejorPuja != null)
            {
                var adj = new BE.Adjudicacion
                {
                    IdSubasta             = idSubasta,
                    IdUnidad              = subasta.IdUnidad,
                    IdGanador             = mejorPuja.IdPostor,
                    PrecioFinal           = mejorPuja.Monto,
                    FechaHoraAdjudicacion = ahora,
                    IdMartillero          = idMartillero,
                    Observaciones         = observaciones
                };
                _dalAdj.Insertar(adj);
            }

            // Actualizar objeto en memoria y notificar observers (RF-07).
            subasta.Estado      = BE.EstadoSubasta.Cerrada;
            subasta.FechaCierre = ahora;
            subasta.IdGanador   = idGanador;
            subasta.PrecioFinal = precioFinal;

            // RF-07: notificar el cierre a todos los suscriptores y luego limpiar el gestor.
            _gestorPujas.NotificarCierre(idSubasta, subasta);
            _gestorPujas.EliminarGestor(idSubasta);

            string resumen = mejorPuja != null
                ? $"Ganador: Postor ID {idGanador} — Precio final: ${precioFinal:N2}"
                : "Subasta desierta — sin pujas validas.";
            _bitacora.Registrar("Subasta", $"Cierre subasta ID {idSubasta} — {resumen}", BE.Criticidad.Alta);
        }

        // Historial de pujas de una subasta (todas, aceptadas y rechazadas).
        public List<BE.Puja> ObtenerHistorialPujas(int idSubasta)
            => _dalPuja.ObtenerPorSubasta(idSubasta);

        // RF-13: todas las adjudicaciones para el reporte de jornada.
        public List<BE.Adjudicacion> ObtenerAdjudicaciones()
            => _dalAdj.ObtenerTodos();

        // RF-10: registra una puja delegando en el Singleton GestorPujas.
        public BE.Puja RegistrarPuja(int idSubasta, int idPostor, decimal monto)
        {
            var subasta = _dalSubasta.ObtenerPorId(idSubasta);
            if (subasta == null)
                throw new Exception($"Subasta {idSubasta} no encontrada.");

            return _gestorPujas.RegistrarPuja(subasta, idPostor, monto, _dalPuja, _dalSubasta);
        }
    }
}
