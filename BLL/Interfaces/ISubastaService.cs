using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface ISubastaService
    {
        List<BE.Subasta> ObtenerTodas();
        List<BE.Subasta> ObtenerActivas();
        BE.Subasta       ObtenerPorId(int id);
        int  AbrirSubasta(int idUnidad, int idMartillero);
        void CerrarSubasta(int idSubasta, int idMartillero, string observaciones = null);
        List<BE.Puja>         ObtenerHistorialPujas(int idSubasta);
        List<BE.Adjudicacion> ObtenerAdjudicaciones();
    }
}
