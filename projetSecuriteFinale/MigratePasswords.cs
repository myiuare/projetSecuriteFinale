using MySql.Data.MySqlClient;
using projetSecuriteFinale;
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

            // Récupérer les utilisateurs qui n'ont pas encore de sel
            string selectQuery = "SELECT id_utilisateur, mot_de_passe FROM utilisateur WHERE sel IS NULL OR sel = ''";
            MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);

            List<(int id, string mdp)> utilisateurs = new List<(int, string)>();

            using (MySqlDataReader reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    utilisateurs.Add((reader.GetInt32("id_utilisateur"), reader.GetString("mot_de_passe")));
                }
            }

            foreach (var user in utilisateurs)
            {
                string sel = UtilsMotDePasse.GenererSel(); // Génère un sel aléatoire
                string hash = UtilsMotDePasse.HashMotDePasse(user.mdp, sel); // SHA256(mdp + sel)

                string updateQuery = "UPDATE utilisateur SET mot_de_passe = @hash, sel = @sel WHERE id_utilisateur = @id";
                MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@hash", hash);
                updateCmd.Parameters.AddWithValue("@sel", sel);
                updateCmd.Parameters.AddWithValue("@id", user.id);

                updateCmd.ExecuteNonQuery();
            }
        }
    }

}


