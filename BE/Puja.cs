using System;

namespace BE
{
    // Registro inmutable de cada oferta procesada.
    // Una puja aceptada nunca se modifica ni elimina.
    public class Puja
    {
        public int        Id             { get; set; }
        public int        IdSubasta      { get; set; }
        public int        IdPostor       { get; set; }
        public decimal    Monto          { get; set; }
        public DateTime   FechaHora      { get; set; }
        public EstadoPuja Estado         { get; set; }
        public string     MotivoRechazo  { get; set; }

        // Datos enriquecidos (JOIN).
        public string NombrePostor { get; set; }
    }
}
