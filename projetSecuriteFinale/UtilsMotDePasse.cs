using System;
using System.Security.Cryptography;
using System.Text;

namespace projetSecuriteFinale
{
    public static class UtilsMotDePasse
    {
      
            public static string HasherAvecSel(string motDePasse, string sel)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(motDePasse + sel);
                    byte[] hash = sha256.ComputeHash(inputBytes);
                    return Convert.ToBase64String(hash);
                }
            }

            public static string HashMotDePasse(string motDePasse, string sel)
            {
                // pour compatibilité avec ancien code
                return HasherAvecSel(motDePasse, sel);
            }

            public static string GenererSel(int taille = 16)
            {
                byte[] sel = new byte[taille];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(sel);
                }
                return Convert.ToBase64String(sel);
            }
        


        public static bool EstMotDePasseRobuste(string motDePasse)
        {
            return motDePasse.Length >= 6;
        }
        

    }
}
