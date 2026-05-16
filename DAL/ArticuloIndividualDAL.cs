using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ArticuloIndividualDAL : BaseDAL<BE.ArticuloIndividual>
    {
        public override List<BE.ArticuloIndividual> ObtenerTodos()
        {
            var lista = new List<BE.ArticuloIndividual>();
            DataTable tabla = acceso.Leer(
                "SELECT u.Id, u.Nombre, u.Descripcion, u.PrecioBase, u.FechaAlta, u.Activo, u.IdLotePadre, " +
                "       a.ValorDeclarado " +
                "FROM UnidadDeVenta u INNER JOIN ArticuloIndividual a ON a.Id = u.Id " +
                "WHERE u.Activo = 1 ORDER BY u.Nombre", null);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        public override BE.ArticuloIndividual ObtenerPorId(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            DataTable tabla = acceso.Leer(
                "SELECT u.Id, u.Nombre, u.Descripcion, u.PrecioBase, u.FechaAlta, u.Activo, u.IdLotePadre, " +
                "       a.ValorDeclarado " +
                "FROM UnidadDeVenta u INNER JOIN ArticuloIndividual a ON a.Id = u.Id " +
                "WHERE u.Id = @Id AND u.Activo = 1", p);
            if (tabla == null || tabla.Rows.Count == 0) return null;
            return Mapear(tabla.Rows[0]);
        }

        public int Alta(BE.ArticuloIndividual articulo)
        {
            int idNuevo = 0;
            acceso.EjecutarTransaccion((conn, tx) =>
            {
                var cmdU = new SqlCommand(
                    "INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo) " +
                    "VALUES (@Nombre, @Descripcion, 'ARTICULO', @PrecioBase, @FechaAlta, 1); " +
                    "SELECT SCOPE_IDENTITY();", conn, tx);
                cmdU.Parameters.AddWithValue("@Nombre",      articulo.Nombre);
                cmdU.Parameters.AddWithValue("@Descripcion", (object)articulo.Descripcion ?? DBNull.Value);
                cmdU.Parameters.AddWithValue("@PrecioBase",  articulo.ValorDeclarado);
                cmdU.Parameters.AddWithValue("@FechaAlta",   articulo.FechaAlta);
                idNuevo = Convert.ToInt32(cmdU.ExecuteScalar());

                var cmdA = new SqlCommand(
                    "INSERT INTO ArticuloIndividual (Id, ValorDeclarado) VALUES (@Id, @ValorDeclarado)", conn, tx);
                cmdA.Parameters.AddWithValue("@Id",             idNuevo);
                cmdA.Parameters.AddWithValue("@ValorDeclarado", articulo.ValorDeclarado);
                cmdA.ExecuteNonQuery();
            });
            return idNuevo;
        }

        public void Modificar(BE.ArticuloIndividual articulo)
        {
            acceso.EjecutarTransaccion((conn, tx) =>
            {
                var cmdU = new SqlCommand(
                    "UPDATE UnidadDeVenta SET Nombre=@Nombre, Descripcion=@Descripcion, " +
                    "PrecioBase=@PrecioBase WHERE Id=@Id", conn, tx);
                cmdU.Parameters.AddWithValue("@Nombre",      articulo.Nombre);
                cmdU.Parameters.AddWithValue("@Descripcion", (object)articulo.Descripcion ?? DBNull.Value);
                cmdU.Parameters.AddWithValue("@PrecioBase",  articulo.ValorDeclarado);
                cmdU.Parameters.AddWithValue("@Id",          articulo.Id);
                cmdU.ExecuteNonQuery();

                var cmdA = new SqlCommand(
                    "UPDATE ArticuloIndividual SET ValorDeclarado=@ValorDeclarado WHERE Id=@Id", conn, tx);
                cmdA.Parameters.AddWithValue("@ValorDeclarado", articulo.ValorDeclarado);
                cmdA.Parameters.AddWithValue("@Id",             articulo.Id);
                cmdA.ExecuteNonQuery();
            });
        }

        private BE.ArticuloIndividual Mapear(DataRow row)
        {
            return new BE.ArticuloIndividual
            {
                Id             = Convert.ToInt32(row["Id"]),
                Nombre         = row["Nombre"].ToString(),
                Descripcion    = row["Descripcion"] != DBNull.Value ? row["Descripcion"].ToString() : null,
                PrecioBase     = Convert.ToDecimal(row["PrecioBase"]),
                FechaAlta      = Convert.ToDateTime(row["FechaAlta"]),
                Activo         = Convert.ToBoolean(row["Activo"]),
                IdLotePadre    = row["IdLotePadre"] != DBNull.Value ? (int?)Convert.ToInt32(row["IdLotePadre"]) : null,
                ValorDeclarado = Convert.ToDecimal(row["ValorDeclarado"])
            };
        }
    }
}
