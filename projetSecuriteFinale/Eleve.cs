using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSecuriteFinale
{
    public class Eleve
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string CodeClasse { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public bool IsLoginBlocked { get; set; }
        public string MotDePasse { get; set; }
        public string Sel { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpiration { get; set; }
        public string Role { get; set; }

        public Eleve(int id, string nom, string prenom, DateTime dateNaissance, string codeClasse, string photoPath, string email, string sel, string resetToken, DateTime? resetTokenExpiration)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            CodeClasse = codeClasse;
            PhotoPath = photoPath;
            Email = email;
            Sel = sel;
            ResetToken = resetToken;
            ResetTokenExpiration = resetTokenExpiration;
        }

        public Eleve() { } // 👈 important pour certains cas (ex: Dapper, DataGridView)
    }
}