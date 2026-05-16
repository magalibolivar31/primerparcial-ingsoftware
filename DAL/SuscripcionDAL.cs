using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SuscripcionDAL : BaseDAL<BE.Suscripcion>
    {
        public override List<BE.Suscripcion> ObtenerTodos()
        {
            var lista = new List<BE.Suscripcion>();
            DataTable tabla = acceso.Leer(
                "SELECT s.Id, s.IdPostor, s.IdSubasta, s.FechaSuscripcion, s.FechaBaja, s.Activa, " +
                "       p.Nombre AS NombrePostor, u.Nombre AS NombreUnidad " +
                "FROM Suscripcion s " +
                "INNER JOIN Postor p        ON p.Id  = s.IdPostor " +
                "INNER JOIN Subasta sb      ON sb.Id = s.IdSubasta " +
                "INNER JOIN UnidadDeVenta u ON u.Id  = sb.IdUnidad " +
                "ORDER BY s.FechaSuscripcion DESC", null);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        public override BE.Suscripcion ObtenerPorId(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            DataTable tabla = acceso.Leer(
                "SELECT s.Id, s.IdPostor, s.IdSubasta, s.FechaSuscripcion, s.FechaBaja, s.Activa, " +
                "       p.Nombre AS NombrePostor, u.Nombre AS NombreUnidad " +
                "FROM Suscripcion s " +
                "INNER JOIN Postor p        ON p.Id  = s.IdPostor " +
                "INNER JOIN Subasta sb      ON sb.Id = s.IdSubasta " +
                "INNER JOIN UnidadDeVenta u ON u.Id  = sb.IdUnidad " +
                "WHERE s.Id = @Id", p);
            if (tabla == null || tabla.Rows.Count == 0) return null;
            return Mapear(tabla.Rows[0]);
        }

        // Devuelve los postores actualmente suscriptos a una subasta.
        public List<BE.Suscripcion> ObtenerActivasPorSubasta(int idSubasta)
        {
            var lista = new List<BE.Suscripcion>();
            SqlParameter[] p = { new SqlParameter("@IdSubasta", idSubasta) };
            DataTable tabla = acceso.Leer(
                "SELECT s.Id, s.IdPostor, s.IdSubasta, s.FechaSuscripcion, s.FechaBaja, s.Activa, " +
                "       p.Nombre AS NombrePostor, '' AS NombreUnidad " +
                "FROM Suscripcion s INNER JOIN Postor p ON p.Id = s.IdPostor " +
                "WHERE s.IdSubasta = @IdSubasta AND s.Activa = 1", p);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        // Verifica si el postor ya esta suscripto activamente a la subasta.
        public bool ExisteSuscripcionActiva(int idPostor, int idSubasta)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@IdPostor",  idPostor),
                new SqlParameter("@IdSubasta", idSubasta)
            };
            DataTable tabla = acceso.Leer(
                "SELECT Id FROM Suscripcion WHERE IdPostor=@IdPostor AND IdSubasta=@IdSubasta AND Activa=1", p);
            return tabla != null && tabla.Rows.Count > 0;
        }

        // Alta de suscripcion. Devuelve el Id generado.
        public int Alta(BE.Suscripcion suscripcion)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@IdPostor",          suscripcion.IdPostor),
                new SqlParameter("@IdSubasta",         suscripcion.IdSubasta),
                new SqlParameter("@FechaSuscripcion",  suscripcion.FechaSuscripcion)
            };
            DataTable tabla = acceso.Leer(
                "INSERT INTO Suscripcion (IdPostor, IdSubasta, FechaSuscripcion, Activa) " +
                "VALUES (@IdPostor, @IdSubasta, @FechaSuscripcion, 1); " +
                "SELECT SCOPE_IDENTITY() AS Id", p);
            return Convert.ToInt32(tabla.Rows[0]["Id"]);
        }

        // Baja de suscripcion: pone Activa=0 y registra la fecha de baja.
        public void Baja(int idPostor, int idSubasta)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@FechaBaja", DateTime.Now),
                new SqlParameter("@IdPostor",  idPostor),
                new SqlParameter("@IdSubasta", idSubasta)
            };
            acceso.Escribir(
                "UPDATE Suscripcion SET Activa=0, FechaBaja=@FechaBaja " +
                "WHERE IdPostor=@IdPostor AND IdSubasta=@IdSubasta AND Activa=1", p);
        }

        private BE.Suscripcion Mapear(DataRow row)
        {
            return new BE.Suscripcion
            {
                Id               = Convert.ToInt32(row["Id"]),
                IdPostor         = Convert.ToInt32(row["IdPostor"]),
                IdSubasta        = Convert.ToInt32(row["IdSubasta"]),
                FechaSuscripcion = Convert.ToDateTime(row["FechaSuscripcion"]),
                FechaBaja        = row["FechaBaja"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["FechaBaja"]) : null,
                Activa           = Convert.ToBoolean(row["Activa"]),
                NombrePostor     = row["NombrePostor"].ToString(),
                NombreUnidad     = row["NombreUnidad"].ToString()
            };
        }
    }
}
