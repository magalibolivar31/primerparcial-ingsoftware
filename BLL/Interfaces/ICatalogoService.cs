using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ICatalogoService
    {
        List<BE.UnidadDeVenta> ObtenerTodos();
        BE.UnidadDeVenta       ObtenerPorId(int id);
        int  AltaArticulo(BE.ArticuloIndividual articulo);
        void ModificarArticulo(BE.ArticuloIndividual articulo);
        int  AltaLote(BE.Lote lote);
        void AgregarALote(int idLote, int idUnidad);
        void BajaUnidad(int id);
        string ObtenerDescripcionCompleta(int id);
        decimal ObtenerPrecioBase(int id);
    }
}
