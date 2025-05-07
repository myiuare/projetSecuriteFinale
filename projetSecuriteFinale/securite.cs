using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSecuriteFinale
{
    using System;
    using MySql.Data.MySqlClient;
    using System.Net.Mail;

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

        // Envoyer un mail d'avertissement
        public void EnvoyerAvertissement(string email)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("admin@ecole.com");
                mail.To.Add(email);
                mail.Subject = "Avertissement : Mot de passe ancien";
                mail.Body = "Votre mot de passe est ancien. Merci de le mettre à jour.";

                SmtpClient smtp = new SmtpClient("smtp.exemple.com");
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("admin@ecole.com", "password");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'envoi du mail : " + ex.Message);
            }
        }
    }

}
