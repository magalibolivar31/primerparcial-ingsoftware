using System;

namespace BE
{
    // Tabla de relacion Postor <-> Subasta.
    // Implementa la lista de observadores del patron Observer en la base de datos.
    public class Suscripcion
    {
        public int       Id                 { get; set; }
        public int       IdPostor           { get; set; }
        public int       IdSubasta          { get; set; }
        public DateTime  FechaSuscripcion   { get; set; }
        public DateTime? FechaBaja          { get; set; }
        public bool      Activa             { get; set; }

        // Datos enriquecidos (JOIN).
        public string NombrePostor  { get; set; }
        public string NombreUnidad  { get; set; }
    }
}
