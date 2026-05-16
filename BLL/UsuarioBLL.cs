using System;
using System.Collections.Generic;

namespace BLL
{
    public class UsuarioBLL : Interfaces.IUsuarioService
    {
        private readonly DAL.UsuarioDAL _dalUsuario = new DAL.UsuarioDAL();

        private const int MaxIntentosFallidos = 3;

        public List<BE.Usuario> ObtenerTodos() => _dalUsuario.ObtenerTodos();

        public BE.Usuario ObtenerPorId(int id) => _dalUsuario.ObtenerPorId(id);

        public bool Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new Exception("Usuario y contraseña son obligatorios.");

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
                    throw new Exception($"Cuenta bloqueada por {MaxIntentosFallidos} intentos fallidos.");
                }

                throw new Exception($"Credenciales incorrectas. Intentos restantes: {restantes}.");
            }

            _dalUsuario.ResetearIntentos(usuario.Id);
            Seguridad.SessionManager.Login(usuario);
            return true;
        }

        public void Logout()
        {
            if (Seguridad.SessionManager.IsLoggedIn)
                Seguridad.SessionManager.Logout();
        }

        public int Alta(BE.Usuario usuario, string passwordPlano)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new Exception("El usuario es obligatorio.");
            if (string.IsNullOrWhiteSpace(passwordPlano))
                throw new Exception("La contraseña es obligatoria.");

            usuario.PasswordHash = Seguridad.Encriptador.Encriptar(passwordPlano);
            usuario.FechaAlta    = DateTime.Now;
            return _dalUsuario.Alta(usuario);
        }

        public void ResetearPassword(int id, string passwordNuevo)
        {
            if (string.IsNullOrWhiteSpace(passwordNuevo) || passwordNuevo.Length < 6)
                throw new Exception("La nueva contraseña debe tener al menos 6 caracteres.");
            _dalUsuario.ActualizarPassword(id, Seguridad.Encriptador.Encriptar(passwordNuevo));
        }

        public void Desbloquear(int id)
        {
            _dalUsuario.ResetearIntentos(id);
        }
    }
}
