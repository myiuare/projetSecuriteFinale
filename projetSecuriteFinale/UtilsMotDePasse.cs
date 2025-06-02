using System;
using System.Security.Cryptography;
using System.Text;

namespace projetSecuriteFinale
{
    public static class UtilsMotDePasse
    {
        public static string GenererSel()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string HashMotDePasse(string motDePasse, string sel)
        {
            var sha256 = SHA256.Create();
            byte[] combined = Encoding.UTF8.GetBytes(motDePasse + sel);
            byte[] hash = sha256.ComputeHash(combined);
            return Convert.ToBase64String(hash);
        }

        public static bool EstMotDePasseRobuste(string motDePasse)
        {
            return motDePasse.Length >= 6;
        }
    }
}
