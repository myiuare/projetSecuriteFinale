using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace projetSecuriteFinale
{
    public static class UtilsMotDePasse
    {
        public static bool EstMotDePasseRobuste(string mdp)
        {
            return mdp.Length >= 8 &&
                   mdp.Any(char.IsUpper) &&
                   mdp.Any(char.IsLower) &&
                   mdp.Any(char.IsDigit) &&
                   mdp.Any(c => !char.IsLetterOrDigit(c));
        }

 
            public static string GenererSel()
            {
                byte[] selBytes = new byte[16];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(selBytes);
                }
                return Convert.ToBase64String(selBytes);
            }

            public static string HashMotDePasse(string motDePasse, string selBase64)
            {
                byte[] selBytes = Convert.FromBase64String(selBase64);
                byte[] mdpBytes = Encoding.UTF8.GetBytes(motDePasse);

                byte[] mdpAvecSel = new byte[selBytes.Length + mdpBytes.Length];
                Buffer.BlockCopy(selBytes, 0, mdpAvecSel, 0, selBytes.Length);
                Buffer.BlockCopy(mdpBytes, 0, mdpAvecSel, selBytes.Length, mdpBytes.Length);

                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hash = sha256.ComputeHash(mdpAvecSel);
                    return Convert.ToBase64String(hash);
                }
            }

            public static bool VerifierMotDePasse(string motDePasseSaisi, string selBase64, string hashStocke)
            {
                string hashCalcule = HashMotDePasse(motDePasseSaisi, selBase64);
                return hashCalcule == hashStocke;
            }
        }
    }


