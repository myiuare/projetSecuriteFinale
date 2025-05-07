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
        public DateTime DateNaissance { get; set; }
        public string CodeClasse { get; set; }
        public string PhotoPath { get; set; }  // Chemin de l'image

        public Eleve(int id, string nom, string prenom, DateTime dateNaissance, string codeClasse, string photoPath)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            CodeClasse = codeClasse;
            PhotoPath = photoPath;
        }
    }



}
