using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace projetSecuriteFinale
{
    public static class EmailUtils
    {
        public static void EnvoyerMail(string destinataire, string sujet, string corps)
        {
            string email = ConfigurationManager.AppSettings["email"];
            string motDePasse = ConfigurationManager.AppSettings["mdp"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(motDePasse))
                throw new Exception("Email expéditeur ou mot de passe manquant dans App.config");

            if (string.IsNullOrWhiteSpace(destinataire))
                throw new ArgumentNullException(nameof(destinataire), "L'adresse du destinataire ne peut pas être vide.");

            MailMessage mail = new MailMessage(email, destinataire, sujet, corps);

            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(email, motDePasse);
                client.Send(mail);
            }
        }


    }
}
