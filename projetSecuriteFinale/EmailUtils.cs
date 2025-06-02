using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace projetSecuriteFinale
{
    class EmailUtils
    {
        public static void EnvoyerMail(string destinataire, string sujet, string corps)
        {
            string email = ConfigurationManager.AppSettings["email"];
            string motDePasse = ConfigurationManager.AppSettings["mdp"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(motDePasse))
                throw new Exception("Email expéditeur ou mot de passe manquant dans App.config");

            if (string.IsNullOrWhiteSpace(destinataire))
                throw new ArgumentNullException(nameof(destinataire), "L'adresse du destinataire ne peut pas être vide.");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(email);
            mail.To.Add(destinataire);
            mail.Subject = sujet;
            mail.Body = corps;

            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(email, motDePasse);
                client.Timeout = 10000; // timeout 10s

                try
                {
                    client.Send(mail);
                }
                catch (SmtpException smtpEx)
                {
                    throw new Exception($"Erreur SMTP: {smtpEx.StatusCode} - {smtpEx.Message}", smtpEx);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erreur lors de l'envoi du mail : {ex.Message}", ex);
                }
            }
        }

    }
}
