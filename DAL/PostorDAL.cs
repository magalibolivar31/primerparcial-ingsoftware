using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class PostorDAL : BaseDAL<BE.Postor>
    {
        public override List<BE.Postor> ObtenerTodos()
        {
            var lista = new List<BE.Postor>();
            DataTable tabla = acceso.Leer(
                "SELECT Id, Nombre, DniCuit, Email, Telefono, Canal, FechaAlta, Activo " +
                "FROM Postor WHERE Activo = 1 ORDER BY Nombre", null);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        public override BE.Postor ObtenerPorId(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            DataTable tabla = acceso.Leer(
                "SELECT Id, Nombre, DniCuit, Email, Telefono, Canal, FechaAlta, Activo " +
                "FROM Postor WHERE Id = @Id", p);
            if (tabla == null || tabla.Rows.Count == 0) return null;
            return Mapear(tabla.Rows[0]);
        }

        public bool ExisteDniCuit(string dniCuit)
        {
            SqlParameter[] p = { new SqlParameter("@DniCuit", dniCuit) };
            DataTable tabla = acceso.Leer(
                "SELECT Id FROM Postor WHERE DniCuit = @DniCuit AND Activo = 1", p);
            return tabla != null && tabla.Rows.Count > 0;
        }

        public int Alta(BE.Postor postor)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Nombre",    postor.Nombre),
                new SqlParameter("@DniCuit",   postor.DniCuit),
                new SqlParameter("@Email",     postor.Email),
                new SqlParameter("@Telefono",  (object)postor.Telefono ?? DBNull.Value),
                new SqlParameter("@Canal",     postor.Canal.ToString().ToUpper()),
                new SqlParameter("@FechaAlta", postor.FechaAlta)
            };
            DataTable tabla = acceso.Leer(
                "INSERT INTO Postor (Nombre, DniCuit, Email, Telefono, Canal, FechaAlta, Activo) " +
                "VALUES (@Nombre, @DniCuit, @Email, @Telefono, @Canal, @FechaAlta, 1); " +
                "SELECT SCOPE_IDENTITY() AS Id", p);
            return Convert.ToInt32(tabla.Rows[0]["Id"]);
        }

        public void Modificar(BE.Postor postor)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Nombre",   postor.Nombre),
                new SqlParameter("@DniCuit",  postor.DniCuit),
                new SqlParameter("@Email",    postor.Email),
                new SqlParameter("@Telefono", (object)postor.Telefono ?? DBNull.Value),
                new SqlParameter("@Canal",    postor.Canal.ToString().ToUpper()),
                new SqlParameter("@Id",       postor.Id)
            };
            acceso.Escribir(
                "UPDATE Postor SET Nombre=@Nombre, DniCuit=@DniCuit, Email=@Email, " +
                "Telefono=@Telefono, Canal=@Canal WHERE Id=@Id", p);
        }

        public void Baja(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            acceso.Escribir("UPDATE Postor SET Activo = 0 WHERE Id = @Id", p);
        }

        private BE.Postor Mapear(DataRow row)
        {
            return new BE.Postor
            {
                Id        = Convert.ToInt32(row["Id"]),
                Nombre    = row["Nombre"].ToString(),
                DniCuit   = row["DniCuit"].ToString(),
                Email     = row["Email"].ToString(),
                Telefono  = row["Telefono"] != DBNull.Value ? row["Telefono"].ToString() : null,
                Canal     = (BE.CanalNotificacion)Enum.Parse(typeof(BE.CanalNotificacion), row["Canal"].ToString(), true),
                FechaAlta = Convert.ToDateTime(row["FechaAlta"]),
                Activo    = Convert.ToBoolean(row["Activo"])
            };
        }
    }
}
