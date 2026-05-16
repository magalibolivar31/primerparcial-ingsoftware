using System.Collections.Generic;
using System.Text;

namespace BE
{
    // PATRON COMPOSITE — Nodo compuesto (Composite).
    // Puede contener ArticulosIndividuales y otros Lotes sin limite de profundidad.
    // ObtenerPrecioBase() y ObtenerDescripcion() delegan recursivamente a sus hijos.
    public class Lote : UnidadDeVenta
    {
        private readonly List<UnidadDeVenta> _componentes = new List<UnidadDeVenta>();

        public int CantidadComponentes => _componentes.Count;

        // RF-03: precio base = suma recursiva de todos los componentes.
        public override decimal ObtenerPrecioBase()
        {
            decimal total = 0;
            foreach (var componente in _componentes)
                total += componente.ObtenerPrecioBase();
            return total;
        }

        // RF-04: descripcion con desglose completo y recursivo del contenido.
        public override string ObtenerDescripcion()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"[Lote] {Nombre} (${ObtenerPrecioBase():N2})");
            foreach (var componente in _componentes)
            {
                foreach (var linea in componente.ObtenerDescripcion().Split('\n'))
                {
                    if (!string.IsNullOrWhiteSpace(linea))
                        sb.AppendLine("  " + linea.TrimEnd());
                }
            }
            return sb.ToString().TrimEnd();
        }

        // RF-01 / RF-02: agrega un componente (articulo u otro lote) a este lote.
        public override void AgregarComponente(UnidadDeVenta unidad)
        {
            if (unidad != null && !_componentes.Contains(unidad))
                _componentes.Add(unidad);
        }

        public override List<UnidadDeVenta> ObtenerHijos() => _componentes;
    }
}
