using System;

namespace BE
{
    // Persona fisica o juridica que puede suscribirse a subastas y realizar ofertas.
    // Es el Observer concreto a nivel de datos.
    public class Postor
    {
        public int                Id          { get; set; }
        public string             Nombre      { get; set; }
        public string             DniCuit     { get; set; }
        public string             Email       { get; set; }
        public string             Telefono    { get; set; }
        public CanalNotificacion  Canal       { get; set; }
        public DateTime           FechaAlta   { get; set; }
        public bool               Activo      { get; set; }
    }
}
