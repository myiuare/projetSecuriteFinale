using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace projetSecuriteFinale
{
    class EmailUtils
    {
        public static void EnvoyerMail(string destinataire, string sujet, string corps)
        {
            string emailExpediteur = ConfigurationManager.AppSettings["email"]?.Trim();
            string motDePasse = ConfigurationManager.AppSettings["mdp"]?.Trim();

            // Debug rapide
            MessageBox.Show("Email lu depuis App.config : " + emailExpediteur);
            MessageBox.Show("Mot de passe lu : " + (motDePasse?.Length > 0 ? "[OK]" : "[VIDE]"));

            if (string.IsNullOrWhiteSpace(destinataire))
            {
                MessageBox.Show("Erreur : email du destinataire vide ou null");
                return;
            }
            if (string.IsNullOrWhiteSpace(emailExpediteur))
            {
                MessageBox.Show("Erreur : email expéditeur manquant dans App.config");
                return;
            }
            if (string.IsNullOrWhiteSpace(motDePasse))
            {
                MessageBox.Show("Erreur : mot de passe manquant dans App.config");
                return;
            }

            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(emailExpediteur),
                    Subject = sujet,
                    Body = corps
                };
                mail.To.Add(destinataire);

                using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(emailExpediteur, motDePasse);
                    client.Send(mail);
                }

                MessageBox.Show("Email envoyé avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'envoi de l'email : " + ex.Message);
            }
        }
    }
}
