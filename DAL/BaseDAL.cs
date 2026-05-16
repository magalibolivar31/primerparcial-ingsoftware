using System.Collections.Generic;

namespace DAL
{
    // Clase base abstracta para todos los DAL.
    // Centraliza el acceso a la instancia Singleton de Acceso.
    public abstract class BaseDAL<T> where T : class
    {
        protected readonly Acceso acceso = Acceso.GetInstance();

        public abstract List<T> ObtenerTodos();
        public abstract T ObtenerPorId(int id);
    }
}
