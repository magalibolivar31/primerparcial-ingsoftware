using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    // PATRON SINGLETON — Punto de acceso unico a la base de datos.
    // Implementacion thread-safe con double-checked locking (igual que WardrobeFlow).
    // Estrategia: nueva conexion por operacion; ADO.NET gestiona el pool automaticamente.
    public sealed class Acceso
    {
        private static volatile Acceso _instance;
        private static readonly object _lock = new object();

        private readonly string _cadenaConexion;

        private Acceso()
        {
            _cadenaConexion = ConfigurationManager
                .ConnectionStrings["AlmonedaNacionalDB"]
                .ConnectionString;
        }

        public static Acceso GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new Acceso();
                }
            }
            return _instance;
        }

        // Ejecuta SELECT y retorna DataTable.
        public DataTable Leer(string consulta, SqlParameter[] parametros)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            using (var cmd = new SqlCommand(consulta, conexion))
            {
                if (parametros != null)
                    cmd.Parameters.AddRange(parametros);
                conexion.Open();
                var tabla = new DataTable();
                using (var adapter = new SqlDataAdapter(cmd))
                    adapter.Fill(tabla);
                return tabla;
            }
        }

        // Ejecuta INSERT / UPDATE / DELETE.
        public int Escribir(string consulta, SqlParameter[] parametros)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            using (var cmd = new SqlCommand(consulta, conexion))
            {
                if (parametros != null)
                    cmd.Parameters.AddRange(parametros);
                conexion.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // Ejecuta un bloque de operaciones dentro de una transaccion SQL atomica.
        public void EjecutarTransaccion(Action<SqlConnection, SqlTransaction> accion)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var tx = conexion.BeginTransaction())
                {
                    try
                    {
                        accion(conexion, tx);
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool VerificarConexion()
        {
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    conexion.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Acceso.VerificarConexion] {ex.Message}");
                return false;
            }
        }
    }
}
