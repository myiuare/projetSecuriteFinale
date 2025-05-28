using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace projetSecuriteFinale
{
    public static class UtilisateurDAO
    {
        public static void MettreAJourResetToken(int id, string token, DateTime expiration)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE utilisateur SET reset_token = @token, reset_token_expiration = @expiration WHERE id_utilisateur = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@token", token);
                cmd.Parameters.AddWithValue("@expiration", expiration);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static Eleve ObtenirParToken(string token)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string sql = "SELECT * FROM utilisateur WHERE LOWER(reset_token) = LOWER(@token)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@token", token);

                Console.WriteLine("Recherche du token : " + token);  // ou MessageBox.Show

                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Eleve
                    {
                        Id = (int)reader["id_utilisateur"],
                        Email = reader["email"].ToString(),
                        ResetToken = reader["reset_token"].ToString(),
                        ResetTokenExpiration = reader["reset_token_expiration"] == DBNull.Value
                            ? null
                            : (DateTime?)Convert.ToDateTime(reader["reset_token_expiration"])
                    };
                }

                Console.WriteLine("Aucun utilisateur trouvé pour le token."); // ou MessageBox.Show

                return null;
            }
        }


        public static void MettreAJourMotDePasse(int id, string hash, string sel)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE utilisateur SET mot_de_passe = @mdp, sel = @sel, dernier_changement_mdp = @now, expiration_mot_de_passe = @expire WHERE id_utilisateur = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mdp", hash);
                cmd.Parameters.AddWithValue("@sel", sel);
                cmd.Parameters.AddWithValue("@now", DateTime.Now);
                cmd.Parameters.AddWithValue("@expire", DateTime.Now.AddDays(90));
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public static Eleve ObtenirParEmail(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string sql = "SELECT * FROM utilisateur WHERE LOWER(email) = LOWER(@Email)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Eleve
                        {
                            Id = Convert.ToInt32(reader["id_utilisateur"]),
                            Email = reader["email"].ToString(),
                            MotDePasse = reader["mot_de_passe"].ToString(),
                            Sel = reader["sel"].ToString()
                        };
                    }
                }
            }

            return null;
        }


        public static void InvaliderResetToken(string token)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string sql = "UPDATE utilisateur SET reset_token = NULL, reset_token_expiration = NULL WHERE reset_token = @token";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@token", token);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
