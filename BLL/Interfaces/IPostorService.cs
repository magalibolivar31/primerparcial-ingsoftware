using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IPostorService
    {
        List<BE.Postor> ObtenerTodos();
        BE.Postor        ObtenerPorId(int id);
        int  Alta(BE.Postor postor);
        void Modificar(BE.Postor postor);
        void Baja(int id);
        void Suscribir(int idPostor, int idSubasta);
        void Desuscribir(int idPostor, int idSubasta);
        List<BE.Suscripcion> ObtenerSuscripcionesActivas(int idSubasta);
    }
}
