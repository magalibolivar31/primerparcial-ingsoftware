using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class SubastaDAL : BaseDAL<BE.Subasta>
    {
        public override List<BE.Subasta> ObtenerTodos()
        {
            var lista = new List<BE.Subasta>();
            DataTable tabla = acceso.Leer(
                "SELECT s.Id, s.IdUnidad, s.IdMartillero, s.Estado, s.PrecioInicial, " +
                "       s.PrecioVigente, s.FechaApertura, s.FechaCierre, s.IdGanador, s.PrecioFinal, " +
                "       u.Nombre AS NombreUnidad, " +
                "       us.Nombre + ' ' + us.Apellido AS NombreMartillero, " +
                "       p.Nombre AS NombreGanador " +
                "FROM Subasta s " +
                "INNER JOIN UnidadDeVenta u  ON u.Id  = s.IdUnidad " +
                "INNER JOIN Usuario us       ON us.Id = s.IdMartillero " +
                "LEFT  JOIN Postor  p        ON p.Id  = s.IdGanador " +
                "ORDER BY s.FechaApertura DESC", null);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        public override BE.Subasta ObtenerPorId(int id)
        {
            SqlParameter[] p = { new SqlParameter("@Id", id) };
            DataTable tabla = acceso.Leer(
                "SELECT s.Id, s.IdUnidad, s.IdMartillero, s.Estado, s.PrecioInicial, " +
                "       s.PrecioVigente, s.FechaApertura, s.FechaCierre, s.IdGanador, s.PrecioFinal, " +
                "       u.Nombre AS NombreUnidad, " +
                "       us.Nombre + ' ' + us.Apellido AS NombreMartillero, " +
                "       po.Nombre AS NombreGanador " +
                "FROM Subasta s " +
                "INNER JOIN UnidadDeVenta u  ON u.Id  = s.IdUnidad " +
                "INNER JOIN Usuario us       ON us.Id = s.IdMartillero " +
                "LEFT  JOIN Postor  po       ON po.Id = s.IdGanador " +
                "WHERE s.Id = @Id", p);
            if (tabla == null || tabla.Rows.Count == 0) return null;
            return Mapear(tabla.Rows[0]);
        }

        // Verifica si la unidad ya tiene una subasta activa.
        public bool TieneSubastaActiva(int idUnidad)
        {
            SqlParameter[] p = { new SqlParameter("@IdUnidad", idUnidad) };
            DataTable tabla = acceso.Leer(
                "SELECT Id FROM Subasta WHERE IdUnidad = @IdUnidad AND Estado = 'ACTIVA'", p);
            return tabla != null && tabla.Rows.Count > 0;
        }

        // Abre una nueva subasta. Devuelve el Id generado.
        public int Abrir(BE.Subasta subasta)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@IdUnidad",      subasta.IdUnidad),
                new SqlParameter("@IdMartillero",  subasta.IdMartillero),
                new SqlParameter("@PrecioInicial", subasta.PrecioInicial),
                new SqlParameter("@PrecioVigente", subasta.PrecioVigente),
                new SqlParameter("@FechaApertura", subasta.FechaApertura)
            };
            DataTable tabla = acceso.Leer(
                "INSERT INTO Subasta (IdUnidad, IdMartillero, Estado, PrecioInicial, PrecioVigente, FechaApertura) " +
                "VALUES (@IdUnidad, @IdMartillero, 'ACTIVA', @PrecioInicial, @PrecioVigente, @FechaApertura); " +
                "SELECT SCOPE_IDENTITY() AS Id", p);
            return Convert.ToInt32(tabla.Rows[0]["Id"]);
        }

        // Actualiza el precio vigente tras una puja aceptada.
        public void ActualizarPrecioVigente(int idSubasta, decimal nuevoPrecio)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@Precio",    nuevoPrecio),
                new SqlParameter("@IdSubasta", idSubasta)
            };
            acceso.Escribir(
                "UPDATE Subasta SET PrecioVigente = @Precio WHERE Id = @IdSubasta", p);
        }

        // Cierra la subasta y registra el ganador y precio final.
        public void Cerrar(int idSubasta, int? idGanador, decimal? precioFinal, DateTime fechaCierre)
        {
            SqlParameter[] p =
            {
                new SqlParameter("@IdGanador",   (object)idGanador   ?? DBNull.Value),
                new SqlParameter("@PrecioFinal", (object)precioFinal ?? DBNull.Value),
                new SqlParameter("@FechaCierre", fechaCierre),
                new SqlParameter("@IdSubasta",   idSubasta)
            };
            acceso.Escribir(
                "UPDATE Subasta SET Estado='CERRADA', IdGanador=@IdGanador, " +
                "PrecioFinal=@PrecioFinal, FechaCierre=@FechaCierre " +
                "WHERE Id=@IdSubasta", p);
        }

        // Devuelve todas las subastas activas.
        public List<BE.Subasta> ObtenerActivas()
        {
            var lista = new List<BE.Subasta>();
            DataTable tabla = acceso.Leer(
                "SELECT s.Id, s.IdUnidad, s.IdMartillero, s.Estado, s.PrecioInicial, " +
                "       s.PrecioVigente, s.FechaApertura, s.FechaCierre, s.IdGanador, s.PrecioFinal, " +
                "       u.Nombre AS NombreUnidad, " +
                "       us.Nombre + ' ' + us.Apellido AS NombreMartillero, " +
                "       NULL AS NombreGanador " +
                "FROM Subasta s " +
                "INNER JOIN UnidadDeVenta u ON u.Id  = s.IdUnidad " +
                "INNER JOIN Usuario us      ON us.Id = s.IdMartillero " +
                "WHERE s.Estado = 'ACTIVA'", null);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        // Bitácora de subastas con todos los filtros combinables.
        public List<BE.Subasta> ObtenerBitacora(
            DateTime? desde, DateTime? hasta,
            string estadoFiltro,
            int? idUnidad, string filtroNombreUnidad, string tipoUnidad,
            int? idPostor, string filtroNombrePostor,
            bool soloGanadores,
            decimal? montoMin, decimal? montoMax)
        {
            var lista = new List<BE.Subasta>();
            string sql =
                "SELECT s.Id, s.IdUnidad, s.IdMartillero, s.Estado, s.PrecioInicial, " +
                "       s.PrecioVigente, s.FechaApertura, s.FechaCierre, s.IdGanador, s.PrecioFinal, " +
                "       u.Nombre AS NombreUnidad, u.Tipo AS TipoUnidad, " +
                "       us.Nombre + ' ' + us.Apellido AS NombreMartillero, " +
                "       p.Nombre AS NombreGanador " +
                "FROM Subasta s " +
                "INNER JOIN UnidadDeVenta u  ON u.Id  = s.IdUnidad " +
                "INNER JOIN Usuario us       ON us.Id = s.IdMartillero " +
                "LEFT  JOIN Postor  p        ON p.Id  = s.IdGanador " +
                "WHERE 1=1";

            var pars = new List<SqlParameter>();

            if (desde.HasValue)
            { sql += " AND s.FechaApertura >= @Desde"; pars.Add(new SqlParameter("@Desde", desde.Value)); }
            if (hasta.HasValue)
            { sql += " AND s.FechaApertura < @Hasta";  pars.Add(new SqlParameter("@Hasta", hasta.Value.AddDays(1))); }
            if (!string.IsNullOrWhiteSpace(estadoFiltro))
            { sql += " AND s.Estado = @Estado"; pars.Add(new SqlParameter("@Estado", estadoFiltro)); }
            if (idUnidad.HasValue && idUnidad.Value > 0)
            { sql += " AND s.IdUnidad = @IdUnidad"; pars.Add(new SqlParameter("@IdUnidad", idUnidad.Value)); }
            else if (!string.IsNullOrWhiteSpace(filtroNombreUnidad))
            { sql += " AND u.Nombre LIKE @NombreUnidad"; pars.Add(new SqlParameter("@NombreUnidad", "%" + filtroNombreUnidad.Trim() + "%")); }
            if (!string.IsNullOrWhiteSpace(tipoUnidad))
            { sql += " AND u.Tipo = @TipoUnidad"; pars.Add(new SqlParameter("@TipoUnidad", tipoUnidad)); }

            bool hayPostor = (idPostor.HasValue && idPostor.Value > 0) || !string.IsNullOrWhiteSpace(filtroNombrePostor);

            if (soloGanadores && hayPostor)
            {
                if (idPostor.HasValue && idPostor.Value > 0)
                { sql += " AND s.IdGanador = @IdPostorGanador"; pars.Add(new SqlParameter("@IdPostorGanador", idPostor.Value)); }
                else
                { sql += " AND p.Nombre LIKE @NombrePostorGanador"; pars.Add(new SqlParameter("@NombrePostorGanador", "%" + filtroNombrePostor.Trim() + "%")); }
            }
            else if (soloGanadores)
            {
                sql += " AND s.IdGanador IS NOT NULL";
            }
            else if (hayPostor)
            {
                if (idPostor.HasValue && idPostor.Value > 0)
                { sql += " AND EXISTS (SELECT 1 FROM Puja pj WHERE pj.IdSubasta = s.Id AND pj.IdPostor = @IdPostorPart AND pj.Estado = 'ACEPTADA')"; pars.Add(new SqlParameter("@IdPostorPart", idPostor.Value)); }
                else
                { sql += " AND EXISTS (SELECT 1 FROM Puja pj INNER JOIN Postor po ON po.Id = pj.IdPostor WHERE pj.IdSubasta = s.Id AND po.Nombre LIKE @NombrePostorPart AND pj.Estado = 'ACEPTADA')"; pars.Add(new SqlParameter("@NombrePostorPart", "%" + filtroNombrePostor.Trim() + "%")); }
            }

            if (montoMin.HasValue)
            { sql += " AND EXISTS (SELECT 1 FROM Puja pj WHERE pj.IdSubasta = s.Id AND pj.Monto >= @MontoMin AND pj.Estado = 'ACEPTADA')"; pars.Add(new SqlParameter("@MontoMin", montoMin.Value)); }
            if (montoMax.HasValue)
            { sql += " AND EXISTS (SELECT 1 FROM Puja pj WHERE pj.IdSubasta = s.Id AND pj.Monto <= @MontoMax AND pj.Estado = 'ACEPTADA')"; pars.Add(new SqlParameter("@MontoMax", montoMax.Value)); }

            sql += " ORDER BY s.FechaApertura DESC";

            DataTable tabla = acceso.Leer(sql, pars.Count > 0 ? pars.ToArray() : null);
            foreach (DataRow row in tabla.Rows)
            {
                BE.Subasta s = Mapear(row);
                s.TipoUnidad = row["TipoUnidad"] != DBNull.Value ? row["TipoUnidad"].ToString() : null;
                lista.Add(s);
            }
            return lista;
        }

        // Devuelve subastas cerradas con filtros opcionales para el historial.
        public List<BE.Subasta> ObtenerCerradas(DateTime? desde, DateTime? hasta,
            string filtroUnidad, string filtroGanador, string resultado)
        {
            var lista = new List<BE.Subasta>();
            string sql =
                "SELECT s.Id, s.IdUnidad, s.IdMartillero, s.Estado, s.PrecioInicial, " +
                "       s.PrecioVigente, s.FechaApertura, s.FechaCierre, s.IdGanador, s.PrecioFinal, " +
                "       u.Nombre AS NombreUnidad, " +
                "       us.Nombre + ' ' + us.Apellido AS NombreMartillero, " +
                "       p.Nombre AS NombreGanador " +
                "FROM Subasta s " +
                "INNER JOIN UnidadDeVenta u  ON u.Id  = s.IdUnidad " +
                "INNER JOIN Usuario us       ON us.Id = s.IdMartillero " +
                "LEFT  JOIN Postor  p        ON p.Id  = s.IdGanador " +
                "WHERE s.Estado = 'CERRADA'";

            var pars = new List<SqlParameter>();

            if (desde.HasValue)
            {
                sql += " AND s.FechaCierre >= @Desde";
                pars.Add(new SqlParameter("@Desde", desde.Value));
            }
            if (hasta.HasValue)
            {
                sql += " AND s.FechaCierre < @Hasta";
                pars.Add(new SqlParameter("@Hasta", hasta.Value.AddDays(1)));
            }
            if (!string.IsNullOrWhiteSpace(filtroUnidad))
            {
                sql += " AND u.Nombre LIKE @Unidad";
                pars.Add(new SqlParameter("@Unidad", "%" + filtroUnidad.Trim() + "%"));
            }
            if (!string.IsNullOrWhiteSpace(filtroGanador))
            {
                sql += " AND p.Nombre LIKE @Ganador";
                pars.Add(new SqlParameter("@Ganador", "%" + filtroGanador.Trim() + "%"));
            }
            if (resultado == "ADJUDICADA") sql += " AND s.IdGanador IS NOT NULL";
            if (resultado == "DESIERTA")   sql += " AND s.IdGanador IS NULL";

            sql += " ORDER BY s.FechaCierre DESC";

            DataTable tabla = acceso.Leer(sql, pars.Count > 0 ? pars.ToArray() : null);
            foreach (DataRow row in tabla.Rows)
                lista.Add(Mapear(row));
            return lista;
        }

        private BE.Subasta Mapear(DataRow row)
        {
            return new BE.Subasta
            {
                Id              = Convert.ToInt32(row["Id"]),
                IdUnidad        = Convert.ToInt32(row["IdUnidad"]),
                IdMartillero    = Convert.ToInt32(row["IdMartillero"]),
                Estado          = (BE.EstadoSubasta)Enum.Parse(typeof(BE.EstadoSubasta), row["Estado"].ToString(), true),
                PrecioInicial   = Convert.ToDecimal(row["PrecioInicial"]),
                PrecioVigente   = Convert.ToDecimal(row["PrecioVigente"]),
                FechaApertura   = Convert.ToDateTime(row["FechaApertura"]),
                FechaCierre     = row["FechaCierre"]   != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["FechaCierre"])   : null,
                IdGanador       = row["IdGanador"]     != DBNull.Value ? (int?)Convert.ToInt32(row["IdGanador"])             : null,
                PrecioFinal     = row["PrecioFinal"]   != DBNull.Value ? (decimal?)Convert.ToDecimal(row["PrecioFinal"])     : null,
                NombreUnidad    = row["NombreUnidad"].ToString(),
                NombreMartillero = row["NombreMartillero"].ToString(),
                NombreGanador   = row["NombreGanador"] != DBNull.Value ? row["NombreGanador"].ToString() : null
            };
        }
    }
}
