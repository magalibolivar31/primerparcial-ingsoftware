using System;

namespace BE
{
    // Entidad Subasta. Registro del ciclo de vida de una subasta sobre una UnidadDeVenta.
    // Es el sujeto conceptual del patron Observer (la clase BLL.GestorPujas coordina
    // las notificaciones sobre instancias de Subasta).
    public class Subasta
    {
        public int           Id             { get; set; }
        public int           IdUnidad       { get; set; }
        public int           IdMartillero   { get; set; }
        public EstadoSubasta Estado         { get; set; }
        public decimal       PrecioInicial  { get; set; }
        public decimal       PrecioVigente  { get; set; }
        public DateTime      FechaApertura  { get; set; }
        public DateTime?     FechaCierre    { get; set; }
        public int?          IdGanador      { get; set; }
        public decimal?      PrecioFinal    { get; set; }

        // Datos enriquecidos (cargados por JOIN, no persisten en esta tabla).
        public string NombreUnidad    { get; set; }
        public string NombreMartillero { get; set; }
        public string NombreGanador   { get; set; }

        public bool EstaActiva  => Estado == EstadoSubasta.Activa;
        public bool EstaCerrada => Estado == EstadoSubasta.Cerrada;
        public bool EsDesierta  => EstaCerrada && !IdGanador.HasValue;
    }
}
