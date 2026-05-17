using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class UsuarioDAL : BaseDAL<BE.Usuario>
    {
        public override List<BE.Usuario> ObtenerTodos()
        {
            var lista = new List<BE.Usuario>();
            DataTable tabla = acceso.Leer(
                "SELECT Id, Nombre, Apellido, Email, PasswordHash, Rol, Activo, " +
                "       IntentosFallidos, Bloqueado, UltimoIntentoFallido, FechaAlta " +
                "FROM Usuario WHERE Activo = 1 ORDER BY Apellido, Nombre", null);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        public override BE.Usuario ObtenerPorId(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            DataTable tabla = acceso.Leer(
                "SELECT Id, Nombre, Apellido, Email, PasswordHash, Rol, Activo, " +
                "       IntentosFallidos, Bloqueado, UltimoIntentoFallido, FechaAlta " +
                "FROM Usuario WHERE Id = @Id", p);
            if (tabla == null || tabla.Rows.Count == 0) return null;
            return Mapear(tabla.Rows[0]);
        }

        public BE.Usuario ObtenerPorEmail(string email)
        {
            SqlParameter[] p = { new SqlParameter("@Email", email) };
            DataTable tabla = acceso.Leer(
                "SELECT Id, Nombre, Apellido, Email, PasswordHash, Rol, Activo, " +
                "       IntentosFallidos, Bloqueado, UltimoIntentoFallido, FechaAlta " +
                "FROM Usuario WHERE Email = @Email", p);
            if (tabla == null || tabla.Rows.Count == 0) return null;
            return Mapear(tabla.Rows[0]);
        }

        public int Alta(BE.Usuario usuario)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Nombre",   usuario.Nombre),
                new SqlParameter("@Apellido", usuario.Apellido),
                new SqlParameter("@Email",    usuario.Email),
                new SqlParameter("@Hash",     usuario.PasswordHash),
                new SqlParameter("@Rol",      usuario.Rol.ToString()),
                new SqlParameter("@FechaAlta",usuario.FechaAlta)
            };
            DataTable tabla = acceso.Leer(
                "INSERT INTO Usuario (Nombre, Apellido, Email, PasswordHash, Rol, Activo, IntentosFallidos, Bloqueado, FechaAlta) " +
                "VALUES (@Nombre, @Apellido, @Email, @Hash, @Rol, 1, 0, 0, @FechaAlta); " +
                "SELECT SCOPE_IDENTITY() AS Id", p);
            return Convert.ToInt32(tabla.Rows[0]["Id"]);
        }

        public void RegistrarIntentoFallido(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            acceso.Escribir(
                "UPDATE Usuario SET IntentosFallidos = IntentosFallidos + 1, " +
                "                   UltimoIntentoFallido = GETDATE() " +
                "WHERE Id = @Id", p);
        }

        public void Bloquear(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            acceso.Escribir("UPDATE Usuario SET Bloqueado = 1 WHERE Id = @Id", p);
        }

        public void ResetearIntentos(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            acceso.Escribir(
                "UPDATE Usuario SET IntentosFallidos = 0, Bloqueado = 0 WHERE Id = @Id", p);
        }

        public void ActualizarPassword(int id, string nuevoHash)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Hash", nuevoHash),
                new SqlParameter("@Id",   id)
            };
            acceso.Escribir(
                "UPDATE Usuario SET PasswordHash = @Hash, IntentosFallidos = 0, Bloqueado = 0 WHERE Id = @Id", p);
        }

        private BE.Usuario Mapear(DataRow row)
        {
            return new BE.Usuario
            {
                Id               = Convert.ToInt32(row["Id"]),
                Nombre           = row["Nombre"].ToString(),
                Apellido         = row["Apellido"].ToString(),
                Email            = row["Email"].ToString(),
                PasswordHash     = row["PasswordHash"].ToString(),
                Rol              = (BE.RolUsuario)Enum.Parse(typeof(BE.RolUsuario), row["Rol"].ToString()),
                Activo           = Convert.ToBoolean(row["Activo"]),
                IntentosFallidos     = Convert.ToInt32(row["IntentosFallidos"]),
                Bloqueado            = Convert.ToBoolean(row["Bloqueado"]),
                UltimoIntentoFallido = row["UltimoIntentoFallido"] == DBNull.Value
                                       ? (DateTime?)null
                                       : Convert.ToDateTime(row["UltimoIntentoFallido"]),
                FechaAlta            = Convert.ToDateTime(row["FechaAlta"])
            };
        }
    }
}
