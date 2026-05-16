using System;
using BE;

namespace Seguridad
{
    // PATRON SINGLETON — Gestiona la sesion del usuario autenticado.
    // Implementacion identica al ejemplo de catedra (patr-n-singleton-en-csharp)
    // y a WardrobeFlow/Seguridad/SessionManager.cs.
    // Double-checked locking garantiza thread-safety con minimo overhead.
    public class SessionManager
    {
        private static readonly object _lock = new object();
        private static SessionManager _session;

        public Usuario  Usuario     { get; set; }
        public DateTime FechaInicio { get; set; }

        // Punto de acceso unico. Lanza excepcion si no hay sesion activa.
        public static SessionManager GetInstance
        {
            get
            {
                if (_session == null)
                    throw new Exception("Sesión no iniciada. Debe hacer Login primero.");
                return _session;
            }
        }

        public static bool IsLoggedIn => _session != null;

        // Crea la sesion para el usuario autenticado.
        public static void Login(Usuario usuario)
        {
            lock (_lock)
            {
                if (_session == null)
                {
                    _session             = new SessionManager();
                    _session.Usuario     = usuario;
                    _session.FechaInicio = DateTime.Now;
                }
                else
                {
                    throw new Exception("Sesión ya iniciada.");
                }
            }
        }

        // Destruye la sesion activa.
        public static void Logout()
        {
            lock (_lock)
            {
                if (_session != null)
                    _session = null;
                else
                    throw new Exception("No hay sesión activa para cerrar.");
            }
        }

        private SessionManager() { }
    }
}
