using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace projetSecuriteFinale
{
    /// Classe responsable de la gestion des élèves (récupération, mise à jour, suppression).
    public class GestionEleves
    {
        private string connectionString = "Server=localhost;Database=gestion_securite;Uid=root;Pwd=;";

        /// Récupère la liste des élèves en appliquant éventuellement des filtres sur la validité du mot de passe.
        public List<Eleve> GetEleves(List<string> filtres)
        {
            List<Eleve> listeEleves = new List<Eleve>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT id_utilisateur, nom, prenom, email, date_naissance, Code_classe, photo_path " +
                        "FROM utilisateur WHERE role = 'eleve'";


                    if (filtres != null && filtres.Count > 0)
                    {
                        query += " AND (";
                        List<string> conditions = new List<string>();

                        foreach (string filtre in filtres)
                        {
                            if (filtre == "90j")
                                conditions.Add("DATEDIFF(NOW(), dernier_changement_mdp) > 90");
                            else if (filtre == "55j")
                                conditions.Add("DATEDIFF(NOW(), dernier_changement_mdp) BETWEEN 50 AND 55");
                            else if (filtre == "ok")
                                conditions.Add("DATEDIFF(NOW(), dernier_changement_mdp) <= 50");
                        }

                        query += string.Join(" OR ", conditions) + ")";
                    }

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
                            reader["photo_path"] != DBNull.Value ? reader["photo_path"].ToString() : string.Empty
                        );

                        eleve.Email = reader["email"].ToString(); 

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

        /// Met à jour l’état de blocage et le mot de passe d’un élève.
        public void UpdateEleve(Eleve eleve)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE utilisateur SET IsLoginBlocked = @IsLoginBlocked, mot_de_passe = @MotDePasse WHERE id_utilisateur = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IsLoginBlocked", eleve.IsLoginBlocked);
                        cmd.Parameters.AddWithValue("@MotDePasse", eleve.MotDePasse ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Id", eleve.Id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la mise à jour de l'élève : " + ex.Message);
            }
        }

        /// Supprime un élève de la base de données.
        public void SupprimerEleve(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "DELETE FROM utilisateur WHERE id_utilisateur = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression de l'élève : " + ex.Message);
            }
        }
    }
}
