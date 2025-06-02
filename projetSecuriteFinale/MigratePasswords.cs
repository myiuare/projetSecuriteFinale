using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class MigratePasswords
{
    // Même fonction que dans ta page de connexion pour hasher
    public static string HashMotDePasse(string motDePasse)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(motDePasse);
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            foreach (byte b in hash)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public static void UpdatePasswordsToHash()
    {
        string connectionString = "server=localhost; database=gestion_securite; uid=root; pwd=";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();

            // Récupère tous les utilisateurs et leurs mots de passe
            string selectQuery = "SELECT id_utilisateur, mot_de_passe FROM utilisateur";
            MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);

            List<(int id, string mdp)> utilisateurs = new List<(int, string)>();

            using (MySqlDataReader reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    utilisateurs.Add((reader.GetInt32("id_utilisateur"), reader.GetString("mot_de_passe")));
                }
            }

            // Pour chaque utilisateur, on hash le mot de passe clair et on met à jour
            foreach (var user in utilisateurs)
            {
                string mdpHash = HashMotDePasse(user.mdp);

                string updateQuery = "UPDATE utilisateur SET mot_de_passe = @mdpHash WHERE id_utilisateur = @id";
                MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@mdpHash", mdpHash);
                updateCmd.Parameters.AddWithValue("@id", user.id);
                updateCmd.ExecuteNonQuery();
            }
        }
    }
}
