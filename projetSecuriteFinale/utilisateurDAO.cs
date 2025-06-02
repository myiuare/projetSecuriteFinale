using System;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using projetSecuriteFinale;

public static class UtilisateurDAO
{
    // Chaîne de connexion à ta base MySQL
    private static string connectionString = "server=localhost;database=gestion_securite;uid=root;pwd=;";

    // Génère un token sécurisé en base64 URL-safe (pour éviter les caractères spéciaux)
    private static string GenererToken()
    {
        byte[] tokenData = new byte[16]; // 128 bits pour un token unique
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(tokenData); // Remplit le tableau avec des octets aléatoires
        }
        // Conversion en base64 URL-safe : remplace +,/ et supprime =
        return Convert.ToBase64String(tokenData).Replace("+", "-").Replace("/", "_").Replace("=", "");
    }

   
    /// Crée un token de réinitialisation, le stocke en base avec expiration (1h), et retourne le token.
  
 
    public static string CreerEtStockerResetToken(int idUtilisateur)
    {
        string token = GenererToken();
        DateTime expiration = DateTime.Now.AddHours(1); // Token valide 1 heure

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string sql = "UPDATE utilisateur SET reset_token = @token, reset_token_expiration = @expiration WHERE id_utilisateur = @id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@token", token);
            cmd.Parameters.AddWithValue("@expiration", expiration);
            cmd.Parameters.AddWithValue("@id", idUtilisateur);
            conn.Open();
            cmd.ExecuteNonQuery(); // Mise à jour du token et expiration dans la table
        }

        return token; // On retourne le token pour pouvoir l’envoyer par mail
    }

    /// Récupère un utilisateur par son adresse mail
   
    public static Eleve ObtenirParEmail(string email)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string sql = "SELECT * FROM utilisateur WHERE email = @mail";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@mail", email);
            conn.Open();

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Eleve
                    {
                        Id = Convert.ToInt32(reader["id_utilisateur"]),
                        Email = reader["email"].ToString(),
                        ResetToken = reader["reset_token"].ToString(),
                        ResetTokenExpiration = reader["reset_token_expiration"] == DBNull.Value ? null : (DateTime?)reader["reset_token_expiration"]
                    };
                }
            }
        }
        return null; // Utilisateur non trouvé
    }

    /// Récupère un utilisateur à partir d'un token de réinitialisation
    public static Eleve ObtenirParToken(string token)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string sql = "SELECT * FROM utilisateur WHERE reset_token = @token";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@token", token);
            conn.Open();

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Eleve
                    {
                        Id = Convert.ToInt32(reader["id_utilisateur"]),
                        Email = reader["email"].ToString(),
                        ResetToken = reader["reset_token"].ToString(),
                        ResetTokenExpiration = reader["reset_token_expiration"] == DBNull.Value ? null : (DateTime?)reader["reset_token_expiration"]
                    };
                }
            }
        }
        return null; // Token invalide ou non trouvé
    }

    /// Met à jour le mot de passe hashé, le sel, la date de changement, et expiration
 
    public static void MettreAJourMotDePasse(int id, string hash, string sel)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            string sql = "UPDATE utilisateur SET mot_de_passe = @mdp, sel = @sel, dernier_changement_mdp = @now, expiration_mot_de_passe = @expire WHERE id_utilisateur = @id";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@mdp", hash);
            cmd.Parameters.AddWithValue("@sel", sel);
            cmd.Parameters.AddWithValue("@now", DateTime.Now);
            cmd.Parameters.AddWithValue("@expire", DateTime.Now.AddDays(90)); // expiration 90 jours
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    /// Invalide un token de reset (après usage)
    public static void InvaliderResetToken(string token)
    {
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

