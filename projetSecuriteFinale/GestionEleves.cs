using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace projetSecuriteFinale
{
    // La classe doit être publique ou interne, et elle contient la méthode
    public class GestionEleves
    {
        private string connectionString = "Server=localhost;Database=gestion_securite;Uid=root;Pwd=;";

        // Récupérer la liste des élèves selon le filtre
        public List<Eleve> GetEleves(string filtre)
        {
            List<Eleve> listeEleves = new List<Eleve>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id_utilisateur, nom, prenom, date_naissance, Code_classe, photo_path FROM utilisateur WHERE role = 'eleve' ";

                    if (filtre == "90j")
                        query += "AND DATEDIFF(NOW(), dernier_changement_mdp) > 90";
                    else if (filtre == "55j")
                        query += "AND DATEDIFF(NOW(), dernier_changement_mdp) BETWEEN 50 AND 55";
                    else if (filtre == "ok")
                        query += "AND DATEDIFF(NOW(), dernier_changement_mdp) <= 50";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Eleve eleve = new Eleve(
                            Convert.ToInt32(reader["id_utilisateur"]),
                            reader["nom"].ToString(),
                            reader["prenom"].ToString(),
                            Convert.ToDateTime(reader["date_naissance"]),
                            reader["Code_classe"].ToString(),
                            reader["photo_path"].ToString()  // Récupérer le chemin de la photo depuis la base de données
                        );
                        listeEleves.Add(eleve);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des élèves : " + ex.Message);
            }

            return listeEleves;
        }


    }
}

