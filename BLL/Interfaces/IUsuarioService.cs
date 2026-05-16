using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IUsuarioService
    {
        List<BE.Usuario> ObtenerTodos();
        BE.Usuario        ObtenerPorId(int id);
        bool  Login(string email, string password);
        void  Logout();
        int   Alta(BE.Usuario usuario, string passwordPlano);
        void  ResetearPassword(int id, string passwordNuevo);
    }
}
