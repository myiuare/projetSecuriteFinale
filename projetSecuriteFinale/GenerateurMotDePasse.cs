using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSecuriteFinale
{
    public static class GenerateurMotDePasse
    {
        public static string GenererMotDePasse(int longueur = 10)
        {
            const string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";
            Random random = new Random();
            return new string(Enumerable.Repeat(caracteres, longueur)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}