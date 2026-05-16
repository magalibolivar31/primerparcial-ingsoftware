using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class PujaDAL : BaseDAL<BE.Puja>
    {
        public override List<BE.Puja> ObtenerTodos()
        {
            var lista = new List<BE.Puja>();
            DataTable tabla = acceso.Leer(
                "SELECT pj.Id, pj.IdSubasta, pj.IdPostor, pj.Monto, pj.FechaHora, pj.Estado, pj.MotivoRechazo, " +
                "       p.Nombre AS NombrePostor " +
                "FROM Puja pj INNER JOIN Postor p ON p.Id = pj.IdPostor " +
                "ORDER BY pj.FechaHora DESC", null);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        public override BE.Puja ObtenerPorId(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            DataTable tabla = acceso.Leer(
                "SELECT pj.Id, pj.IdSubasta, pj.IdPostor, pj.Monto, pj.FechaHora, pj.Estado, pj.MotivoRechazo, " +
                "       p.Nombre AS NombrePostor " +
                "FROM Puja pj INNER JOIN Postor p ON p.Id = pj.IdPostor " +
                "WHERE pj.Id = @Id", p);
            if (tabla == null || tabla.Rows.Count == 0) return null;
            return Mapear(tabla.Rows[0]);
        }

        // Devuelve el historial de pujas de una subasta especifica.
        public List<BE.Puja> ObtenerPorSubasta(int idSubasta)
        {
            var lista = new List<BE.Puja>();
            SqlParameter[] p = { new SqlParameter("@IdSubasta", idSubasta) };
            DataTable tabla = acceso.Leer(
                "SELECT pj.Id, pj.IdSubasta, pj.IdPostor, pj.Monto, pj.FechaHora, pj.Estado, pj.MotivoRechazo, " +
                "       po.Nombre AS NombrePostor " +
                "FROM Puja pj INNER JOIN Postor po ON po.Id = pj.IdPostor " +
                "WHERE pj.IdSubasta = @IdSubasta ORDER BY pj.FechaHora DESC", p);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        // Inserta una puja. Devuelve el Id generado.
        public int Insertar(BE.Puja puja)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@IdSubasta",     puja.IdSubasta),
                new SqlParameter("@IdPostor",      puja.IdPostor),
                new SqlParameter("@Monto",         puja.Monto),
                new SqlParameter("@FechaHora",     puja.FechaHora),
                new SqlParameter("@Estado",        puja.Estado.ToString().ToUpper()),
                new SqlParameter("@MotivoRechazo", (object)puja.MotivoRechazo ?? DBNull.Value)
            };
            DataTable tabla = acceso.Leer(
                "INSERT INTO Puja (IdSubasta, IdPostor, Monto, FechaHora, Estado, MotivoRechazo) " +
                "VALUES (@IdSubasta, @IdPostor, @Monto, @FechaHora, @Estado, @MotivoRechazo); " +
                "SELECT SCOPE_IDENTITY() AS Id", p);
            return Convert.ToInt32(tabla.Rows[0]["Id"]);
        }

        // Obtiene la puja aceptada mas alta de una subasta (para determinar ganador al cierre).
        public BE.Puja ObtenerMejorPuja(int idSubasta)
        {
            SqlParameter[] p = { new SqlParameter("@IdSubasta", idSubasta) };
            DataTable tabla = acceso.Leer(
                "SELECT TOP 1 pj.Id, pj.IdSubasta, pj.IdPostor, pj.Monto, pj.FechaHora, pj.Estado, pj.MotivoRechazo, " +
                "       po.Nombre AS NombrePostor " +
                "FROM Puja pj INNER JOIN Postor po ON po.Id = pj.IdPostor " +
                "WHERE pj.IdSubasta = @IdSubasta AND pj.Estado = 'ACEPTADA' " +
                "ORDER BY pj.Monto DESC", p);
            if (tabla == null || tabla.Rows.Count == 0) return null;
            return Mapear(tabla.Rows[0]);
        }

        private BE.Puja Mapear(DataRow row)
        {
            return new BE.Puja
            {
                Id            = Convert.ToInt32(row["Id"]),
                IdSubasta     = Convert.ToInt32(row["IdSubasta"]),
                IdPostor      = Convert.ToInt32(row["IdPostor"]),
                Monto         = Convert.ToDecimal(row["Monto"]),
                FechaHora     = Convert.ToDateTime(row["FechaHora"]),
                Estado        = (BE.EstadoPuja)Enum.Parse(typeof(BE.EstadoPuja), row["Estado"].ToString(), true),
                MotivoRechazo = row["MotivoRechazo"] != DBNull.Value ? row["MotivoRechazo"].ToString() : null,
                NombrePostor  = row["NombrePostor"].ToString()
            };
        }
    }
}
