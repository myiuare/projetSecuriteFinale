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


namespace projetSecuriteFinale
{
    public partial class GestionMotdePasse : Form
    {
        // Déclare une variable globale pour stocker la liste d'élèves récupérés
        private List<Eleve> listeEleves = new List<Eleve>();

        public GestionMotdePasse()
        {
            InitializeComponent();
            comboBoxEleves.SelectedIndexChanged += comboBoxEleves_SelectedIndexChanged;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        // Méthode appelée lorsqu'on clique sur le bouton pour afficher les élèves
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Liste des filtres sélectionnés
                List<string> filtres = new List<string>();

                // Ajouter les filtres en fonction des cases à cocher
                if (checkBox1.Checked)
                    filtres.Add("90j");

                if (checkBox6.Checked)
                    filtres.Add("55j");

                if (checkBox9.Checked)
                    filtres.Add("ok");

                // Si aucun filtre n'est sélectionné, afficher un message d'erreur
                if (filtres.Count == 0)
                {
                    MessageBox.Show("Veuillez cocher au moins un filtre.");
                    return;
                }

                // Créer une instance de GestionEleves pour récupérer les élèves avec les filtres appliqués
                GestionEleves gestionEleves = new GestionEleves();

                // Appeler la méthode GetEleves pour obtenir la liste des élèves selon les filtres sélectionnés
                listeEleves = gestionEleves.GetEleves(filtres);

                // Effacer les éléments précédemment ajoutés dans la ComboBox
                comboBoxEleves.Items.Clear();

                // Ajouter chaque élève dans la ComboBox sous la forme "Prénom Nom"
                foreach (var eleve in listeEleves)
                {
                    comboBoxEleves.Items.Add($"{eleve.Prenom} {eleve.Nom}");
                }

                // Si des élèves sont trouvés, sélectionner le premier élément
                if (comboBoxEleves.Items.Count > 0)
                    comboBoxEleves.SelectedIndex = 0;
                else
                    MessageBox.Show("Aucun élève trouvé.");
            }
            catch (Exception ex)
            {
                // Si une erreur se produit, afficher un message d'erreur
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        // Méthode appelée lorsqu'on change de sélection dans la ComboBox pour afficher les détails de l'élève
        private void comboBoxEleves_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Vérifier que l'index sélectionné est valide
            int index = comboBoxEleves.SelectedIndex;

            // Si l'index est valide (c'est-à-dire dans la plage de la liste d'élèves)
            if (index >= 0 && index < listeEleves.Count)
            {
                // Récupérer l'élève sélectionné dans la liste
                Eleve eleve = listeEleves[index];

                // Mettre à jour les labels pour afficher les informations de l'élève
                labelNom.Text = "Nom : " + eleve.Nom;
                labelPrenom.Text = "Prénom : " + eleve.Prenom;
                labelClasse.Text = "Classe : " + eleve.CodeClasse;
                labelDateNaissance.Text = "Date de naissance : " + eleve.DateNaissance.ToShortDateString();

                // Vérifie si le chemin de la photo est valide
                if (!string.IsNullOrEmpty(eleve.PhotoPath))
                {
                    try
                    {
                        // Vérifier si l'URL est valide (pas nécessaire ici car tu utilises un chemin local)
                        if (Uri.IsWellFormedUriString(eleve.PhotoPath, UriKind.Absolute))  // Si c'est une URL
                        {
                            Console.WriteLine("URL valide trouvée : " + eleve.PhotoPath);  // Message de débogage
                            try
                            {
                                // Télécharger l'image depuis l'URL et l'afficher dans la PictureBox
                                pictureBoxEleve.Image = Image.FromStream(new System.Net.WebClient().OpenRead(eleve.PhotoPath));
                            }
                            catch (Exception ex)
                            {
                                // Si l'image ne peut pas être téléchargée, afficher une image par défaut
                                pictureBoxEleve.Image = null; // ou une image par défaut
                                MessageBox.Show("Erreur lors du téléchargement de l'image : " + ex.Message);
                            }
                        }
                        else
                        {
                            // Si c'est un chemin local
                            Console.WriteLine("Chemin local trouvé : " + eleve.PhotoPath);  // Message de débogage
                                                                                            // Vérifier si le fichier existe réellement
                            if (File.Exists(eleve.PhotoPath))
                            {
                                Console.WriteLine("Fichier trouvé localement : " + eleve.PhotoPath);  // Message de débogage
                                pictureBoxEleve.Image = Image.FromFile(eleve.PhotoPath);
                            }
                            else
                            {
                                // Afficher une erreur si le fichier n'existe pas
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
                    pictureBoxEleve.Image = null;  // Afficher une image par défaut si aucun chemin n'est fourni
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

            if (radioBloquerLogin.Checked)
            {
                eleve.IsLoginBlocked = true;
                GestionEleves gestionEleves = new GestionEleves();
                gestionEleves.UpdateEleve(eleve);
                MessageBox.Show($"Login de {eleve.Prenom} {eleve.Nom} bloqué.");
           
                MailSender.EnvoyerMailRenouvellement(eleve.Email);
                MessageBox.Show("Mail de renouvellement envoyé.");
            }
            else if (radioAvertissement.Checked)
            {
                MailSender.EnvoyerMailAvertissement(eleve.Email);
                MessageBox.Show("Mail d'avertissement envoyé.");
            }
            else if (radioChangerMDP.Checked)
            {
                string nouveauMDP = GenerateurMotDePasse.Generer();
                eleve.MotDePasse = Hash(nouveauMDP);
                GestionEleves gestionEleves = new GestionEleves();
                gestionEleves.UpdateEleve(eleve);
                MessageBox.Show($"Mot de passe changé : {nouveauMDP}");
            }
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
    }
    }
}
