using System;
using System.Collections.Generic;

namespace BLL
{
    public class UsuarioBLL : Interfaces.IUsuarioService
    {
        private readonly DAL.UsuarioDAL     _dalUsuario = new DAL.UsuarioDAL();
        private readonly Servicios.Bitacora _bitacora   = new Servicios.Bitacora();

        private const int MaxIntentosFallidos = 3;

        public List<BE.Usuario> ObtenerTodos() => _dalUsuario.ObtenerTodos();

        public BE.Usuario ObtenerPorId(int id) => _dalUsuario.ObtenerPorId(id);

        // Autentica al usuario y crea la sesion Singleton.
        public bool Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new Exception("Email y contraseña son obligatorios.");

            var usuario = _dalUsuario.ObtenerPorEmail(email);
            if (usuario == null)
                throw new Exception("Credenciales incorrectas.");

            if (!usuario.Activo)
                throw new Exception("El usuario se encuentra deshabilitado.");

            if (usuario.Bloqueado)
                throw new Exception($"La cuenta está bloqueada por {MaxIntentosFallidos} intentos fallidos. Contacte al administrador.");

            if (!Seguridad.Encriptador.Verificar(password, usuario.PasswordHash))
            {
                _dalUsuario.RegistrarIntentoFallido(usuario.Id);
                int restantes = MaxIntentosFallidos - (usuario.IntentosFallidos + 1);

                if (restantes <= 0)
                {
                    _dalUsuario.Bloquear(usuario.Id);
                    _bitacora.Registrar("Login", $"Cuenta bloqueada: {email}", BE.Criticidad.Alta, usuario.Id);
                    throw new Exception($"Cuenta bloqueada por {MaxIntentosFallidos} intentos fallidos.");
                }

                _bitacora.Registrar("Login", $"Intento fallido: {email} — Intentos restantes: {restantes}", BE.Criticidad.Media, usuario.Id);
                throw new Exception($"Credenciales incorrectas. Intentos restantes: {restantes}.");
            }

            // Credenciales correctas.
            _dalUsuario.ResetearIntentos(usuario.Id);
            Seguridad.SessionManager.Login(usuario);
            _bitacora.Registrar("Login", $"Login exitoso: {usuario.NombreCompleto} [{usuario.Rol}]", BE.Criticidad.Baja, usuario.Id);
            return true;
        }

        public void Logout()
        {
            if (Seguridad.SessionManager.IsLoggedIn)
            {
                var u = Seguridad.SessionManager.GetInstance.Usuario;
                _bitacora.Registrar("Logout", $"Logout: {u.NombreCompleto}", BE.Criticidad.Baja, u.Id);
                Seguridad.SessionManager.Logout();
            }
        }

        public int Alta(BE.Usuario usuario, string passwordPlano)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new Exception("El email es obligatorio.");
            if (string.IsNullOrWhiteSpace(passwordPlano))
                throw new Exception("La contraseña es obligatoria.");

            usuario.PasswordHash = Seguridad.Encriptador.Encriptar(passwordPlano);
            usuario.FechaAlta    = DateTime.Now;
            int id = _dalUsuario.Alta(usuario);
            _bitacora.Registrar("Usuarios", $"Alta usuario: {usuario.NombreCompleto} [{usuario.Rol}]", BE.Criticidad.Media);
            return id;
        }

        public void CambiarPassword(int id, string passwordActual, string passwordNuevo)
        {
            var usuario = _dalUsuario.ObtenerPorId(id);
            if (usuario == null) throw new Exception("Usuario no encontrado.");
            if (!Seguridad.Encriptador.Verificar(passwordActual, usuario.PasswordHash))
                throw new Exception("La contraseña actual es incorrecta.");
            if (string.IsNullOrWhiteSpace(passwordNuevo) || passwordNuevo.Length < 6)
                throw new Exception("La nueva contraseña debe tener al menos 6 caracteres.");

            _dalUsuario.ActualizarPassword(id, Seguridad.Encriptador.Encriptar(passwordNuevo));
            _bitacora.Registrar("Usuarios", $"Cambio de contraseña — Usuario ID {id}", BE.Criticidad.Media, id);
        }

        public void ResetearPassword(int id, string passwordNuevo)
        {
            if (string.IsNullOrWhiteSpace(passwordNuevo) || passwordNuevo.Length < 6)
                throw new Exception("La nueva contraseña debe tener al menos 6 caracteres.");

            _dalUsuario.ActualizarPassword(id, Seguridad.Encriptador.Encriptar(passwordNuevo));
            _bitacora.Registrar("Usuarios", $"Reset de contraseña — Usuario ID {id}", BE.Criticidad.Alta);
        }

        // Desbloquea una cuenta reseteando el contador de intentos fallidos.
        public void Desbloquear(int id)
        {
            _dalUsuario.ResetearIntentos(id);
            _bitacora.Registrar("Usuarios", $"Cuenta desbloqueada — Usuario ID {id}", BE.Criticidad.Alta);
        }
    }
}
