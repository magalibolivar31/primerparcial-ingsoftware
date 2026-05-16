using System;
using System.Windows.Forms;

namespace GUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!DAL.Acceso.GetInstance().VerificarConexion())
            {
                MessageBox.Show(
                    "No se pudo conectar a la base de datos.\nVerifique que SQL Server esté en ejecución y vuelva a intentarlo.",
                    "Error de conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            using (var login = new Login())
            {
                if (login.ShowDialog() == DialogResult.OK)
                    Application.Run(new Menu());
            }
        }
    }
}
