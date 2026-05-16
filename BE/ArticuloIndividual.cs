using System.Collections.Generic;

namespace BE
{
    // PATRON COMPOSITE — Hoja (Leaf).
    // Representa una pieza unica e indivisible del catalogo.
    // No puede contener otros componentes.
    public class ArticuloIndividual : UnidadDeVenta
    {
        public decimal ValorDeclarado { get; set; }

        // RF-03: el precio base de un articulo es su valor declarado.
        public override decimal ObtenerPrecioBase() => ValorDeclarado;

        // RF-04: descripcion del articulo (Nombre + Descripcion + Valor).
        public override string ObtenerDescripcion()
            => $"[Artículo] {Nombre} — ${ValorDeclarado:N2}" +
               (string.IsNullOrWhiteSpace(Descripcion) ? "" : $"\n  {Descripcion}");

        // Las hojas no admiten hijos — operacion vacia.
        public override void AgregarComponente(UnidadDeVenta unidad) { }

        public override List<UnidadDeVenta> ObtenerHijos() => null;
    }
}
