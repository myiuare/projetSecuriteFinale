// Importation des bibliothèques nécessaires
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using projetSecuriteFinale;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Configuration;
using System.Net;

namespace projetSecuriteFinale
{
    public partial class GestionMotdePasse : Form
    {
        // Liste contenant les élèves affichés dans le formulaire
        private List<Eleve> listeEleves = new List<Eleve>();

        // Constructeur du formulaire
        public GestionMotdePasse()
        {
            InitializeComponent(); // Initialise les composants de l'interface
            comboBoxEleves.SelectedIndexChanged += comboBoxEleves_SelectedIndexChanged; // Abonne un événement à la sélection d'un élève
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> filtres = new List<string>(); // Crée une liste pour les filtres sélectionnés

                // Vérifie quels filtres sont cochés
                if (radioButton1.Checked) filtres.Add("90j");
                if (radioButton2.Checked) filtres.Add("55j");
                if (radioButton6.Checked) filtres.Add("ok");

                // Affiche une alerte si aucun filtre n'est sélectionné
                if (filtres.Count == 0)
                {
                    MessageBox.Show("Veuillez cocher au moins un filtre.");
                    return;
                }

                // Récupère les élèves depuis la classe de gestion
                GestionEleves gestionEleves = new GestionEleves();
                listeEleves = gestionEleves.GetEleves(filtres);

                comboBoxEleves.Items.Clear(); // Vide la comboBox

                // Ajoute les élèves trouvés à la comboBox
                foreach (var eleve in listeEleves)
                {
                    comboBoxEleves.Items.Add($"{eleve.Prenom} {eleve.Nom}");
                }

                // Sélectionne automatiquement le premier élève s'il y en a
                if (comboBoxEleves.Items.Count > 0)
                    comboBoxEleves.SelectedIndex = 0;
                else
                    MessageBox.Show("Aucun élève trouvé.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message); // Gère les erreurs de chargement
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            // Bouton pour appliquer l'action (bloquer ou envoyer un mail)
           
                int index = comboBoxEleves.SelectedIndex;

                if (index < 0 || index >= listeEleves.Count)
                {
                    MessageBox.Show("Veuillez sélectionner un élève.");
                    return;
                }

                Eleve eleve = listeEleves[index];

                try
                {
                // Si on veut bloquer l'accès
                if (radioBloquerLogin.Checked)
                {
                    try
                    {
                        string email = ConfigurationManager.AppSettings["email"];
                        string motDePasse = ConfigurationManager.AppSettings["mdp"];

                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(email);

                        // Vérifie que l'élève a un email
                        if (string.IsNullOrWhiteSpace(eleve.Email))
                        {
                            MessageBox.Show("L'élève sélectionné n'a pas d'adresse email valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        mail.To.Add(eleve.Email);
                        mail.Subject = "Alerte mot de passe";
                        mail.Body = "Bonjour, votre mot de passe dépasse les 90 jours.";

                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(email, motDePasse);
                        client.Send(mail); // Envoie l'email

                        MessageBox.Show("Email envoyé avec succès !");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de l'envoi du mail d'avertissement : {ex.Message}");
                    }
                }

                // Si on veut envoyer un lien de réinitialisation
                else if (radioAvertissement.Checked)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(eleve.Email))
                        {
                            MessageBox.Show("L'élève sélectionné n'a pas d'adresse email valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }



                        // Stocker en base
                        string token = UtilisateurDAO.CreerEtStockerResetToken(eleve.Id);

                        // Préparer l'email AVEC LE TOKEN, PAS DE LIEN
                        string email = ConfigurationManager.AppSettings["email"];
                        string motDePasse = ConfigurationManager.AppSettings["mdp"];
                        if (string.IsNullOrWhiteSpace(email))
                        {
                            MessageBox.Show("Adresse expéditeur manquante dans App.config !");
                            return;
                        }


                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(email);
                        mail.To.Add(eleve.Email);
                        mail.Subject = "Code de réinitialisation du mot de passe";

                        // Le mail contient simplement le token à saisir dans l'application
                        mail.Body = $"Bonjour {eleve.Prenom},\n\n" +
                                    "Voici votre code de réinitialisation du mot de passe.\n" +
                                    "Veuillez le saisir dans l'application pour valider le changement.\n\n" +
                                    $"Code : {token}\n\n" +
                                    "Ce code est valable 1 heure.\n\n" +
                                    "Si vous n'avez pas demandé cette réinitialisation, ignorez ce mail.\n\n" +
                                    "Cordialement,\nL'équipe du site";

                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                        client.EnableSsl = true;
                        client.Credentials = new NetworkCredential(email, motDePasse);
                        client.Send(mail);

                        MessageBox.Show("Email avec code envoyé avec succès !");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de l'envoi de l'email : {ex.Message}");
                    }
                }



                // Supprimer un élève (optionnelle)
                else if (radioSupprimer.Checked)
                {
                    var confirm = MessageBox.Show($"Supprimer {eleve.Prenom} {eleve.Nom} ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        // Logique à implémenter : suppression dans la base + UI
                        MessageBox.Show("Élève supprimé.");
                    }
                }

                // Si aucune option n’est sélectionnée
                else
                {
                    MessageBox.Show("Veuillez sélectionner une action.");
                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                }
            }

        

        private void checkBox1_CheckedChanged(object sender, EventArgs e) { } // Événement vide (non utilisé)

        // Bouton de chargement des élèves en fonction des filtres
       

        // Gère le changement de sélection dans la comboBox
        private void comboBoxEleves_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxEleves.SelectedIndex; // Récupère l'index de l'élève sélectionné

            if (index >= 0 && index < listeEleves.Count)
            {
                Eleve eleve = listeEleves[index]; // Récupère l'élève correspondant

                // Affiche les infos de l'élève
                labelNom.Text = "Nom : " + eleve.Nom;
                labelPrenom.Text = "Prénom : " + eleve.Prenom;
                labelClasse.Text = "Classe : " + eleve.CodeClasse;
                labelDateNaissance.Text = "Date de naissance : " + eleve.DateNaissance.ToShortDateString();

                // Tente d'afficher la photo de l'élève
                if (!string.IsNullOrEmpty(eleve.PhotoPath))
                {
                    try
                    {
                        // Si la photo est une URL distante
                        if (Uri.IsWellFormedUriString(eleve.PhotoPath, UriKind.Absolute))
                        {
                            pictureBoxEleve.Image = Image.FromStream(new WebClient().OpenRead(eleve.PhotoPath));
                        }
                        // Sinon, c'est un chemin local
                        else if (File.Exists(eleve.PhotoPath))
                        {
                            pictureBoxEleve.Image = Image.FromFile(eleve.PhotoPath);
                        }
                        else
                        {
                            MessageBox.Show("Le fichier d'image n'existe pas à cet emplacement : " + eleve.PhotoPath);
                            pictureBoxEleve.Image = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur : " + ex.Message);
                        if (!string.IsNullOrEmpty(eleve.PhotoPath) && File.Exists(eleve.PhotoPath))
                        {
                            pictureBoxEleve.Image = Image.FromFile(eleve.PhotoPath);
                        }
                        else
                        {
                            // Image par défaut ou rien
                            pictureBoxEleve.Image = null;
                        }

                    }
                }
                else
                {
                    pictureBoxEleve.Image = null; // Aucune photo
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) { } // Événement vide (non utilisé)

      
        // Événements de base non utilisés
        private void groupBox2_Enter(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void labelClasse_Click(object sender, EventArgs e)
        {

        }
    }
}
