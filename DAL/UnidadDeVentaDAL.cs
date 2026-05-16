using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class UnidadDeVentaDAL : BaseDAL<BE.UnidadDeVenta>
    {
        // Devuelve todas las unidades activas sin cargar hijos (lista plana para catalogo).
        public override List<BE.UnidadDeVenta> ObtenerTodos()
        {
            var lista = new List<BE.UnidadDeVenta>();
            try
            {
                DataTable tabla = acceso.Leer(
                    "SELECT u.Id, u.Nombre, u.Descripcion, u.Tipo, u.PrecioBase, " +
                    "       u.FechaAlta, u.Activo, u.IdLotePadre, " +
                    "       a.ValorDeclarado, a.Categoria, a.EstadoFisico, a.Ubicacion " +
                    "FROM UnidadDeVenta u " +
                    "LEFT JOIN ArticuloIndividual a ON a.Id = u.Id " +
                    "WHERE u.Activo = 1 " +
                    "ORDER BY u.Nombre", null);

                foreach (DataRow row in tabla.Rows)
                    lista.Add(Mapear(row));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el catálogo.", ex);
            }
            return lista;
        }

        public override BE.UnidadDeVenta ObtenerPorId(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            try
            {
                DataTable tabla = acceso.Leer(
                    "SELECT u.Id, u.Nombre, u.Descripcion, u.Tipo, u.PrecioBase, " +
                    "       u.FechaAlta, u.Activo, u.IdLotePadre, " +
                    "       a.ValorDeclarado, a.Categoria, a.EstadoFisico, a.Ubicacion " +
                    "FROM UnidadDeVenta u " +
                    "LEFT JOIN ArticuloIndividual a ON a.Id = u.Id " +
                    "WHERE u.Id = @Id AND u.Activo = 1", p);

                if (tabla == null || tabla.Rows.Count == 0) return null;
                return Mapear(tabla.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la unidad de venta.", ex);
            }
        }

        // Actualiza el PrecioBase persitido (se llama tras recalculo en BLL).
        public void ActualizarPrecioBase(int id, decimal precioBase)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@PrecioBase", precioBase),
                new SqlParameter("@Id", id)
            };
            acceso.Escribir("UPDATE UnidadDeVenta SET PrecioBase = @PrecioBase WHERE Id = @Id", p);
        }

        // Baja logica.
        public void Baja(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            acceso.Escribir("UPDATE UnidadDeVenta SET Activo = 0 WHERE Id = @Id", p);
        }

        // Asigna el lote padre a una unidad (o null para quitarle el padre).
        public void AsignarLotePadre(int idUnidad, int? idLotePadre)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@IdLotePadre", (object)idLotePadre ?? DBNull.Value),
                new SqlParameter("@Id", idUnidad)
            };
            acceso.Escribir("UPDATE UnidadDeVenta SET IdLotePadre = @IdLotePadre WHERE Id = @Id", p);
        }

        // Devuelve los hijos directos de un lote dado.
        public List<BE.UnidadDeVenta> ObtenerHijosDeLote(int idLote)
        {
            var lista = new List<BE.UnidadDeVenta>();
            SqlParameter[] p = { new SqlParameter("@IdLote", idLote) };
            DataTable tabla = acceso.Leer(
                "SELECT u.Id, u.Nombre, u.Descripcion, u.Tipo, u.PrecioBase, " +
                "       u.FechaAlta, u.Activo, u.IdLotePadre, " +
                "       a.ValorDeclarado, a.Categoria, a.EstadoFisico, a.Ubicacion " +
                "FROM UnidadDeVenta u " +
                "LEFT JOIN ArticuloIndividual a ON a.Id = u.Id " +
                "WHERE u.IdLotePadre = @IdLote AND u.Activo = 1", p);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        private BE.UnidadDeVenta Mapear(DataRow row)
        {
            string tipo = row["Tipo"].ToString();
            if (tipo == "ARTICULO")
            {
                return new BE.ArticuloIndividual
                {
                    Id             = Convert.ToInt32(row["Id"]),
                    Nombre         = row["Nombre"].ToString(),
                    Descripcion    = row["Descripcion"].ToString(),
                    PrecioBase     = Convert.ToDecimal(row["PrecioBase"]),
                    FechaAlta      = Convert.ToDateTime(row["FechaAlta"]),
                    Activo         = Convert.ToBoolean(row["Activo"]),
                    IdLotePadre    = row["IdLotePadre"] != DBNull.Value ? (int?)Convert.ToInt32(row["IdLotePadre"]) : null,
                    ValorDeclarado = row["ValorDeclarado"] != DBNull.Value ? Convert.ToDecimal(row["ValorDeclarado"]) : 0,
                    Categoria      = row["Categoria"]    != DBNull.Value ? row["Categoria"].ToString()    : null,
                    EstadoFisico   = row["EstadoFisico"] != DBNull.Value ? row["EstadoFisico"].ToString() : null,
                    Ubicacion      = row["Ubicacion"]    != DBNull.Value ? row["Ubicacion"].ToString()    : null
                };
            }
            else
            {
                return new BE.Lote
                {
                    Id          = Convert.ToInt32(row["Id"]),
                    Nombre      = row["Nombre"].ToString(),
                    Descripcion = row["Descripcion"].ToString(),
                    PrecioBase  = Convert.ToDecimal(row["PrecioBase"]),
                    FechaAlta   = Convert.ToDateTime(row["FechaAlta"]),
                    Activo      = Convert.ToBoolean(row["Activo"]),
                    IdLotePadre = row["IdLotePadre"] != DBNull.Value ? (int?)Convert.ToInt32(row["IdLotePadre"]) : null
                };
            }
        }
    }
}
