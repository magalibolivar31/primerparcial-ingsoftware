using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class LoteDAL : BaseDAL<BE.Lote>
    {
        public override List<BE.Lote> ObtenerTodos()
        {
            var lista = new List<BE.Lote>();
            DataTable tabla = acceso.Leer(
                "SELECT u.Id, u.Nombre, u.Descripcion, u.PrecioBase, u.FechaAlta, u.Activo, u.IdLotePadre, " +
                "       l.CantidadComponentes " +
                "FROM UnidadDeVenta u INNER JOIN Lote l ON l.Id = u.Id " +
                "WHERE u.Activo = 1 ORDER BY u.Nombre", null);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        public override BE.Lote ObtenerPorId(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            DataTable tabla = acceso.Leer(
                "SELECT u.Id, u.Nombre, u.Descripcion, u.PrecioBase, u.FechaAlta, u.Activo, u.IdLotePadre, " +
                "       l.CantidadComponentes " +
                "FROM UnidadDeVenta u INNER JOIN Lote l ON l.Id = u.Id " +
                "WHERE u.Id = @Id AND u.Activo = 1", p);
            if (tabla == null || tabla.Rows.Count == 0) return null;
            return Mapear(tabla.Rows[0]);
        }

        // Alta: inserta en UnidadDeVenta y en Lote dentro de una transaccion.
        public int Alta(BE.Lote lote)
        {
            int idNuevo = 0;
            acceso.EjecutarTransaccion((conn, tx) =>
            {
                var cmdU = new SqlCommand(
                    "INSERT INTO UnidadDeVenta (Nombre, Descripcion, Tipo, PrecioBase, FechaAlta, Activo) " +
                    "VALUES (@Nombre, @Descripcion, 'LOTE', 0, @FechaAlta, 1); " +
                    "SELECT SCOPE_IDENTITY();", conn, tx);
                cmdU.Parameters.AddWithValue("@Nombre",      lote.Nombre);
                cmdU.Parameters.AddWithValue("@Descripcion", (object)lote.Descripcion ?? DBNull.Value);
                cmdU.Parameters.AddWithValue("@FechaAlta",   lote.FechaAlta);
                idNuevo = Convert.ToInt32(cmdU.ExecuteScalar());

                var cmdL = new SqlCommand(
                    "INSERT INTO Lote (Id, CantidadComponentes) VALUES (@Id, 0)", conn, tx);
                cmdL.Parameters.AddWithValue("@Id", idNuevo);
                cmdL.ExecuteNonQuery();
            });
            return idNuevo;
        }

        // Actualiza el precio base y la cantidad de componentes del lote.
        public void ActualizarPrecioYCantidad(int id, decimal precioBase, int cantidad)
        {
            acceso.EjecutarTransaccion((conn, tx) =>
            {
                var cmdU = new SqlCommand(
                    "UPDATE UnidadDeVenta SET PrecioBase = @PrecioBase WHERE Id = @Id", conn, tx);
                cmdU.Parameters.AddWithValue("@PrecioBase", precioBase);
                cmdU.Parameters.AddWithValue("@Id", id);
                cmdU.ExecuteNonQuery();

                var cmdL = new SqlCommand(
                    "UPDATE Lote SET CantidadComponentes = @Cantidad WHERE Id = @Id", conn, tx);
                cmdL.Parameters.AddWithValue("@Cantidad", cantidad);
                cmdL.Parameters.AddWithValue("@Id", id);
                cmdL.ExecuteNonQuery();
            });
        }

        private BE.Lote Mapear(DataRow row)
        {
            return new BE.Lote
            {
                Id          = Convert.ToInt32(row["Id"]),
                Nombre      = row["Nombre"].ToString(),
                Descripcion = row["Descripcion"] != DBNull.Value ? row["Descripcion"].ToString() : null,
                PrecioBase  = Convert.ToDecimal(row["PrecioBase"]),
                FechaAlta   = Convert.ToDateTime(row["FechaAlta"]),
                Activo      = Convert.ToBoolean(row["Activo"]),
                IdLotePadre = row["IdLotePadre"] != DBNull.Value ? (int?)Convert.ToInt32(row["IdLotePadre"]) : null
            };
        }
    }
}
