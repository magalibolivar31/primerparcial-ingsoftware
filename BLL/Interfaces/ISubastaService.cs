using System;
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
        List<BE.Puja>    ObtenerHistorialPujas(int idSubasta);
        List<BE.Subasta> ObtenerHistorial(DateTime? desde, DateTime? hasta,
            string filtroUnidad, string filtroGanador, string resultado);
        List<BE.Subasta> ObtenerBitacora(DateTime? desde, DateTime? hasta,
            string estado, int? idUnidad, string filtroUnidad, string tipoUnidad,
            int? idPostor, string filtroPostor, bool soloGanadores,
            decimal? montoMin, decimal? montoMax);

        // RF-05 / RF-08 — Observer: suscripción desde la GUI
        void SuscribirObserver(int idSubasta, Servicios.IObserverPostor observer);
        void DesuscribirObserver(int idSubasta, Servicios.IObserverPostor observer);
    }
}
