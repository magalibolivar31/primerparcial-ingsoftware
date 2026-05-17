using System;
using System.Collections.Generic;
using System.Text;

namespace Servicios
{
    // RF-13: Reporte consolidado de jornada.
    // Recorre toda la estructura del catalogo de forma recursiva usando el patron Composite.
    // El mismo mecanismo de ObtenerDescripcion() que se usa para mostrar un lote
    // se reutiliza aqui para generar el informe completo de la jornada.
    public class ReporteJornada
    {
        private readonly DAL.UnidadDeVentaDAL _dalUnidad  = new DAL.UnidadDeVentaDAL();
        private readonly DAL.SubastaDAL       _dalSubasta = new DAL.SubastaDAL();
        private readonly DAL.AdjudicacionDAL  _dalAdj     = new DAL.AdjudicacionDAL();

        // Genera el informe consolidado de la jornada.
        // Lista todas las unidades de venta con su precio final o estado "Desierta".
        public string GenerarReporte(DateTime fecha)
        {
            var sb = new StringBuilder();
            sb.AppendLine("========================================");
            sb.AppendLine("  LA ALMONEDA NACIONAL — Reporte de Jornada");
            sb.AppendLine($"  Fecha: {fecha:dd/MM/yyyy}");
            sb.AppendLine("========================================");
            sb.AppendLine();

            // Cargar todas las unidades activas (lista plana desde BD).
            List<BE.UnidadDeVenta> todas = _dalUnidad.ObtenerTodos();

            // Construir el arbol Composite en memoria.
            BE.UnidadDeVenta arbol = ConstruirArbol(todas);

            // Cargar adjudicaciones del dia para consultar resultados.
            List<BE.Adjudicacion> adjudicaciones = _dalAdj.ObtenerTodos();
            var mapaAdj = new Dictionary<int, BE.Adjudicacion>();
            foreach (var adj in adjudicaciones)
                mapaAdj[adj.IdUnidad] = adj;

            // Recorrer el arbol de forma recursiva (Composite) y generar lineas del reporte.
            RecorrerNodo(arbol, sb, mapaAdj, 0);

            sb.AppendLine();
            sb.AppendLine("========================================");
            sb.AppendLine($"  Informe generado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine("========================================");
            return sb.ToString();
        }

        // Recorre recursivamente el arbol Composite para generar el reporte.
        // Cada nodo delega en sus hijos, igual que ObtenerPrecioBase() en Lote.
        private void RecorrerNodo(BE.UnidadDeVenta nodo, StringBuilder sb,
                                  Dictionary<int, BE.Adjudicacion> mapaAdj, int nivel)
        {
            if (nodo == null) return;

            string sangria = new string(' ', nivel * 2);
            string resultado;

            if (mapaAdj.TryGetValue(nodo.Id, out BE.Adjudicacion adj))
                resultado = $"ADJUDICADA a {adj.NombreGanador} por ${adj.PrecioFinal:N2}";
            else
                resultado = "Desierta";

            sb.AppendLine($"{sangria}{nodo.ObtenerDescripcion().Split('\n')[0].Trim()} → {resultado}");

            // Si es un Lote, recorrer sus hijos (recursion del Composite).
            var hijos = nodo.ObtenerHijos();
            if (hijos != null)
                foreach (var hijo in hijos)
                    RecorrerNodo(hijo, sb, mapaAdj, nivel + 1);
        }

        // Construye el arbol Composite en memoria a partir de la lista plana de la BD.
        private BE.UnidadDeVenta ConstruirArbol(List<BE.UnidadDeVenta> todas)
        {
            // Crear un lote raiz invisible que contiene a todos los nodos sin padre.
            var raiz = new BE.Lote { Nombre = "Catálogo Completo", Id = -1 };

            // Indice por Id para lookups rapidos.
            var mapa = new Dictionary<int, BE.UnidadDeVenta>();
            foreach (var u in todas)
                mapa[u.Id] = u;

            // Asignar cada unidad a su padre o a la raiz.
            foreach (var u in todas)
            {
                if (u.IdLotePadre.HasValue && mapa.TryGetValue(u.IdLotePadre.Value, out BE.UnidadDeVenta padre))
                    padre.AgregarComponente(u);
                else
                    raiz.AgregarComponente(u);
            }

            return raiz;
        }
    }
}
