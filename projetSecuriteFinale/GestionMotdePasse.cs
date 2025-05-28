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
using static projetSecuriteFinale.Securite;

namespace projetSecuriteFinale
{
    public partial class GestionMotdePasse : Form
    {
        private List<Eleve> listeEleves = new List<Eleve>();

        public GestionMotdePasse()
        {
            InitializeComponent();
            comboBoxEleves.SelectedIndexChanged += comboBoxEleves_SelectedIndexChanged;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> filtres = new List<string>();

                if (radioButton1.Checked)
                    filtres.Add("90j");

                if (radioButton2.Checked)
                    filtres.Add("55j");

                if (radioButton3.Checked)
                    filtres.Add("ok");

                if (filtres.Count == 0)
                {
                    MessageBox.Show("Veuillez cocher au moins un filtre.");
                    return;
                }

                GestionEleves gestionEleves = new GestionEleves();
                listeEleves = gestionEleves.GetEleves(filtres);

                comboBoxEleves.Items.Clear();

                foreach (var eleve in listeEleves)
                {
                    comboBoxEleves.Items.Add($"{eleve.Prenom} {eleve.Nom}");
                }

                if (comboBoxEleves.Items.Count > 0)
                    comboBoxEleves.SelectedIndex = 0;
                else
                    MessageBox.Show("Aucun élève trouvé.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void comboBoxEleves_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxEleves.SelectedIndex;

            if (index >= 0 && index < listeEleves.Count)
            {
                Eleve eleve = listeEleves[index];

                labelNom.Text = "Nom : " + eleve.Nom;
                labelPrenom.Text = "Prénom : " + eleve.Prenom;
                labelClasse.Text = "Classe : " + eleve.CodeClasse;
                labelDateNaissance.Text = "Date de naissance : " + eleve.DateNaissance.ToShortDateString();

                if (!string.IsNullOrEmpty(eleve.PhotoPath))
                {
                    try
                    {
                        if (Uri.IsWellFormedUriString(eleve.PhotoPath, UriKind.Absolute))
                        {
                            pictureBoxEleve.Image = Image.FromStream(new System.Net.WebClient().OpenRead(eleve.PhotoPath));
                        }
                        else
                        {
                            if (File.Exists(eleve.PhotoPath))
                            {
                                pictureBoxEleve.Image = Image.FromFile(eleve.PhotoPath);
                            }
                            else
                            {
                                MessageBox.Show("Le fichier d'image n'existe pas à cet emplacement : " + eleve.PhotoPath);
                                pictureBoxEleve.Image = null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur : " + ex.Message);
                    }
                }
                else
                {
                    pictureBoxEleve.Image = null;
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = comboBoxEleves.SelectedIndex;

            if (index < 0 || index >= listeEleves.Count)
            {
                MessageBox.Show("Veuillez sélectionner un élève.");
                return;
            }

            Eleve eleve = listeEleves[index];

            try
            {
                if (radioBloquerLogin.Checked)
                {
                    try
                    {
                        string email = ConfigurationManager.AppSettings["email"];
                        string motDePasse = ConfigurationManager.AppSettings["mdp"];

                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(email);
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
                        client.Send(mail);

                        MessageBox.Show("Email envoyé avec succès !");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de l'envoi du mail d'avertissement : {ex.Message}");
                    }
                }
                else if (radioAvertissement.Checked)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(eleve.Email))
                        {
                            MessageBox.Show("L'élève sélectionné n'a pas d'adresse email valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                       
                        

                        string token = Guid.NewGuid().ToString();
                        DateTime expiration = DateTime.Now.AddHours(1);

                        // Appel à ta DAL pour sauvegarder le token + expiration
                        UtilisateurDAO.MettreAJourResetToken(eleve.Id, token, expiration);

                        string corps = $"Bonjour,\n\nVotre mot de passe dépasse les 90 jours.\n\n" +
                                       $"Pour réinitialiser votre mot de passe, ouvrez l'application, cliquez sur 'Réinitialiser mot de passe', puis entrez ce code unique : {token}\n\n" +
                                       $"Ce code expire dans 1 heure.";

                        MessageBox.Show("Lien de réinitialisation envoyé par email.");
                        EmailUtils.EnvoyerMail(
                         eleve.Email,
                         "Réinitialisation de mot de passe",
                         corps
                     );






                        MessageBox.Show("Email envoyé avec succès !");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de l'envoi de l'email : {ex.Message}");
                    }

                }
                //else if (radioChangerMDP.Checked)
                //{
                //        string nouveauMDP = GenerateurMotDePasse.Generer(); // Génère un mdp aléatoire
                //        eleve.MotDePasse = PasswordUtils.Hash(nouveauMDP);  // Hash ce mdp
                //        GestionEleves gestionEleves = new GestionEleves();
                //        gestionEleves.UpdateEleve(eleve);                   // Mets à jour en base avec le hash
                //        MessageBox.Show($"Mot de passe changé : {nouveauMDP}"); // Affiche le mdp en clair à l'utilisateur
                //    }

                    else if (radioSupprimer.Checked)
                {
                    var confirm = MessageBox.Show($"Supprimer {eleve.Prenom} {eleve.Nom} ?", "Confirmation", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        GestionEleves gestionEleves = new GestionEleves();
                        gestionEleves.SupprimerEleve(eleve.Id);
                        comboBoxEleves.Items.RemoveAt(index);
                        listeEleves.RemoveAt(index);
                        MessageBox.Show("Élève supprimé.");
                    }
                }
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

