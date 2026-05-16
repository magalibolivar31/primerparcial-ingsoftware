using System;

namespace BE
{
    // Registro definitivo del resultado de una subasta cerrada.
    // Se persiste al momento del cierre de la subasta.
    public class Adjudicacion
    {
        public int      Id                   { get; set; }
        public int      IdSubasta            { get; set; }
        public int      IdUnidad             { get; set; }
        public int      IdGanador            { get; set; }
        public decimal  PrecioFinal          { get; set; }
        public DateTime FechaHoraAdjudicacion { get; set; }
        public int      IdMartillero         { get; set; }
        public string   Observaciones        { get; set; }

        // Datos enriquecidos (JOIN).
        public string NombreUnidad    { get; set; }
        public string NombreGanador   { get; set; }
        public string NombreMartillero { get; set; }
    }
}
