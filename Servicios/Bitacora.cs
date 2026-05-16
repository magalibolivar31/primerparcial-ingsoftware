using System;
using System.Data.SqlClient;

namespace Servicios
{
    // Servicio de registro de auditoría. Se usa en todas las capas de negocio.
    public class Bitacora
    {
        private readonly DAL.Acceso _acceso = DAL.Acceso.GetInstance();

        public void Registrar(string formulario, string accion, BE.Criticidad criticidad, int? idUsuario = null)
        {
            try
            {
                SqlParameter[] p =
                {
                    new SqlParameter("@Formulario", (object)formulario ?? DBNull.Value),
                    new SqlParameter("@Accion",     accion),
                    new SqlParameter("@Criticidad", criticidad.ToString()),
                    new SqlParameter("@FechaHora",  DateTime.Now),
                    new SqlParameter("@IdUsuario",  (object)idUsuario ?? DBNull.Value)
                };
                _acceso.Escribir(
                    "INSERT INTO Bitacora (Formulario, Accion, Criticidad, FechaHora, IdUsuario) " +
                    "VALUES (@Formulario, @Accion, @Criticidad, @FechaHora, @IdUsuario)", p);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Bitacora] Error al registrar: {ex.Message}");
            }
        }
    }
}
