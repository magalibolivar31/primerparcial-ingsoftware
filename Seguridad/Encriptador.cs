using System;
using System.Security.Cryptography;
using System.Text;

namespace Seguridad
{
    // Utilidades de encriptacion de contrasenas usando SHA-256.
    public static class Encriptador
    {
        public static string Encriptar(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                throw new ArgumentNullException(nameof(texto));

            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(texto));
                var sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        public static bool Verificar(string textoPlano, string hash)
            => Encriptar(textoPlano) == hash;
    }
}
