using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class AdjudicacionDAL : BaseDAL<BE.Adjudicacion>
    {
        public override List<BE.Adjudicacion> ObtenerTodos()
        {
            var lista = new List<BE.Adjudicacion>();
            DataTable tabla = acceso.Leer(
                "SELECT a.Id, a.IdSubasta, a.IdUnidad, a.IdGanador, a.PrecioFinal, " +
                "       a.FechaHoraAdjudicacion, a.IdMartillero, a.Observaciones, " +
                "       u.Nombre AS NombreUnidad, p.Nombre AS NombreGanador, " +
                "       us.Nombre + ' ' + us.Apellido AS NombreMartillero " +
                "FROM Adjudicacion a " +
                "INNER JOIN UnidadDeVenta u ON u.Id  = a.IdUnidad " +
                "INNER JOIN Postor p        ON p.Id  = a.IdGanador " +
                "INNER JOIN Usuario us      ON us.Id = a.IdMartillero " +
                "ORDER BY a.FechaHoraAdjudicacion DESC", null);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        public override BE.Adjudicacion ObtenerPorId(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            DataTable tabla = acceso.Leer(
                "SELECT a.Id, a.IdSubasta, a.IdUnidad, a.IdGanador, a.PrecioFinal, " +
                "       a.FechaHoraAdjudicacion, a.IdMartillero, a.Observaciones, " +
                "       u.Nombre AS NombreUnidad, p.Nombre AS NombreGanador, " +
                "       us.Nombre + ' ' + us.Apellido AS NombreMartillero " +
                "FROM Adjudicacion a " +
                "INNER JOIN UnidadDeVenta u ON u.Id  = a.IdUnidad " +
                "INNER JOIN Postor p        ON p.Id  = a.IdGanador " +
                "INNER JOIN Usuario us      ON us.Id = a.IdMartillero " +
                "WHERE a.Id = @Id", p);
            if (tabla == null || tabla.Rows.Count == 0) return null;
            return Mapear(tabla.Rows[0]);
        }

        public int Insertar(BE.Adjudicacion adj)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@IdSubasta",            adj.IdSubasta),
                new SqlParameter("@IdUnidad",             adj.IdUnidad),
                new SqlParameter("@IdGanador",            adj.IdGanador),
                new SqlParameter("@PrecioFinal",          adj.PrecioFinal),
                new SqlParameter("@FechaHoraAdjudicacion",adj.FechaHoraAdjudicacion),
                new SqlParameter("@IdMartillero",         adj.IdMartillero),
                new SqlParameter("@Observaciones",        (object)adj.Observaciones ?? DBNull.Value)
            };
            DataTable tabla = acceso.Leer(
                "INSERT INTO Adjudicacion (IdSubasta, IdUnidad, IdGanador, PrecioFinal, FechaHoraAdjudicacion, IdMartillero, Observaciones) " +
                "VALUES (@IdSubasta, @IdUnidad, @IdGanador, @PrecioFinal, @FechaHoraAdjudicacion, @IdMartillero, @Observaciones); " +
                "SELECT SCOPE_IDENTITY() AS Id", p);
            return Convert.ToInt32(tabla.Rows[0]["Id"]);
        }

        private BE.Adjudicacion Mapear(DataRow row)
        {
            return new BE.Adjudicacion
            {
                Id                    = Convert.ToInt32(row["Id"]),
                IdSubasta             = Convert.ToInt32(row["IdSubasta"]),
                IdUnidad              = Convert.ToInt32(row["IdUnidad"]),
                IdGanador             = Convert.ToInt32(row["IdGanador"]),
                PrecioFinal           = Convert.ToDecimal(row["PrecioFinal"]),
                FechaHoraAdjudicacion = Convert.ToDateTime(row["FechaHoraAdjudicacion"]),
                IdMartillero          = Convert.ToInt32(row["IdMartillero"]),
                Observaciones         = row["Observaciones"] != DBNull.Value ? row["Observaciones"].ToString() : null,
                NombreUnidad          = row["NombreUnidad"].ToString(),
                NombreGanador         = row["NombreGanador"].ToString(),
                NombreMartillero      = row["NombreMartillero"].ToString()
            };
        }
    }
}
