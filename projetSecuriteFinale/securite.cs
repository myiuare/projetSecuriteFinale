using System;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using System.Linq;

namespace projetSecuriteFinale
{
    public class Securite
    {
        private string connectionString = "Server=localhost;Database=gestion_securite;Uid=root;Pwd=;";

        // Bloquer un élève
        public void BloquerEleve(int idEleve)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE utilisateur SET is_active = 0 WHERE id_utilisateur = @id", conn);
                cmd.Parameters.AddWithValue("@id", idEleve);
                cmd.ExecuteNonQuery();
            }
        }

        // Supprimer un élève
        public void SupprimerEleve(int idEleve)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("DELETE FROM utilisateur WHERE id_utilisateur = @id", conn);
                cmd.Parameters.AddWithValue("@id", idEleve);
                cmd.ExecuteNonQuery();
            }
        }

        // Générer un mot de passe aléatoire
        public static class GenerateurMotDePasse
        {
            public static string Generer(int longueur = 10)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@#";
                var random = new Random();
                return new string(Enumerable.Repeat(chars, longueur)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }

        // Utilitaires pour le hashage des mots de passe
        public static class PasswordUtils
        {
            public static string Hash(string motDePasse)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(motDePasse));
                    return BitConverter.ToString(bytes).Replace("-", "").ToLower();
                }
            }
        }

        // Classe pour envoyer des mails
        public static class MailSender
        {
            public static void EnvoyerMailRenouvellement(string destinataire)
            {
                EnvoyerMail(destinataire,
                    "Renouvellement requis",
                    "Votre accès a été bloqué. Veuillez contacter l'administration pour renouveler votre mot de passe.");
            }

            public static void EnvoyerMailAvertissement(string destinataire)
            {
                EnvoyerMail(destinataire,
                    "Avertissement : mot de passe ancien",
                    "Votre mot de passe est ancien. Merci de le changer rapidement.");
            }

            private static void EnvoyerMail(string destinataire, string sujet, string corps)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("admin@ecole.com");
                    mail.To.Add(destinataire);
                    mail.Subject = sujet;
                    mail.Body = corps;

                    SmtpClient smtp = new SmtpClient("smtp.exemple.com")
                    {
                        Port = 587,
                        Credentials = new System.Net.NetworkCredential("admin@ecole.com", "password"),
                        EnableSsl = true
                    };

                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erreur lors de l'envoi du mail : " + ex.Message);
                }
            }
        }

    }
}

