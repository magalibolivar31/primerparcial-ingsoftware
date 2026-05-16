using System;

namespace BE
{
    // Personal interno de La Almoneda Nacional con acceso al sistema de gestion.
    public class Usuario
    {
        public int        Id               { get; set; }
        public string     Nombre           { get; set; }
        public string     Apellido         { get; set; }
        public string     Email            { get; set; }
        public string     PasswordHash     { get; set; }
        public RolUsuario Rol              { get; set; }
        public bool       Activo           { get; set; }
        public int        IntentosFallidos { get; set; }
        public bool       Bloqueado        { get; set; }
        public DateTime   FechaAlta        { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";

        public bool EsMartillero   => Rol == RolUsuario.Martillero;
        public bool EsOperador     => Rol == RolUsuario.Operador;
        public bool EsAdministrador => Rol == RolUsuario.Administrador;
        public bool EsSupervisor   => Rol == RolUsuario.Supervisor;
    }
}
