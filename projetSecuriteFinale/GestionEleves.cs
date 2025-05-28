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
        public List<Eleve> GetEleves(List<string> filtres)
        {
            List<Eleve> listeEleves = new List<Eleve>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT id_utilisateur, nom, prenom, date_naissance, Code_classe, photo_path, email, sel, reset_token, reset_token_expiration FROM utilisateur WHERE role = 'eleve'";

                    if (filtres != null && filtres.Count > 0)
                    {
                        // Ajoute une condition combinée avec OR pour les différents filtres
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
                        // Assigner correctement le chemin de la photo (photo_path) récupéré depuis la base de données
                        string email = reader["email"] != DBNull.Value ? reader["email"].ToString() : string.Empty;
                        string photoPath = reader["photo_path"] != DBNull.Value ? reader["photo_path"].ToString() : string.Empty;

                        // Créer l'objet Eleve avec le chemin de la photo
                        Eleve eleve = new Eleve(
                         Convert.ToInt32(reader["id_utilisateur"]),
                         reader["nom"].ToString(),
                         reader["prenom"].ToString(),
                         Convert.ToDateTime(reader["date_naissance"]),
                         reader["Code_classe"].ToString(),
                         photoPath,
                         email,
                         reader["sel"].ToString(),
                         reader["reset_token"]?.ToString(),
                         reader["reset_token_expiration"] == DBNull.Value ? null : (DateTime?)reader["reset_token_expiration"]
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