using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetSecuriteFinale
{
    public partial class pageConnex : Form
    {

        Form pageDaccueil = new pageAccueil();
        Form pageProf = new pageProfesseur();
        Form pagElev = new pageEleve();
        public pageConnex()
        {
            InitializeComponent();
        }

        private void buttonReChoix_Click(object sender, EventArgs e)
        {
            this.Hide();  // Masque pageDeConnexion


        }

        private void buttonConnexion_Click(object sender, EventArgs e)
        {
            string log = log_connex.Text;
            string motDepass = mdp_connex.Text;
            string roleSelectionne = ""; // "eleve" ou "professeur" selon le choix de l'utilisateur
            if (rad_eleve.Checked)
            {
                roleSelectionne = "eleve";
            }
            else if (rad_prof.Checked)
            {
                roleSelectionne = "professeur";
            }else if (rad_admin.Checked)
            {
                roleSelectionne = "administrateur";
            }

            // Connexion à la base de données MySQL
            MySqlConnection utilisateur = new MySqlConnection("database=gestion_securite; server=localhost; user id=root; mdp=");

            try
            {
                utilisateur.Open();
                // On va maintenant vérifier le rôle et les informations de connexion dans la base de données
                string query = "SELECT * FROM utilisateur WHERE email = @email AND mot_de_passe = @mdp AND role = @role";
                MySqlCommand cmd = new MySqlCommand(query, utilisateur);

                cmd.Parameters.AddWithValue("@email", log);
                cmd.Parameters.AddWithValue("@mdp", motDepass);
                cmd.Parameters.AddWithValue("@role", roleSelectionne); // On passe le rôle sélectionné

                MySqlDataReader recherche_connexion = cmd.ExecuteReader();

                if (recherche_connexion.HasRows)
                {
                    // Si l'utilisateur existe avec les bonnes informations et le bon rôle
                    MessageBox.Show("Super " + log + " Vous êtes bien dans la base", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    recherche_connexion.Read();

                    // Vérifier le rôle de l'utilisateur dans la base de données
                    string roleUtilisateur = recherche_connexion["role"].ToString(); // Récupère le rôle

                    // Selon le rôle de l'utilisateur, ouvrir la page correspondante
                    if (roleUtilisateur == "eleve")
                    {
                        pagElev.ShowDialog(); // Afficher la page Élève
                    }
                    else if (roleUtilisateur == "professeur")
                    {
                        pageProf.ShowDialog(); // Afficher la page Professeur
                    }
                else if (roleUtilisateur == "administrateur")
                {
                        pageDaccueil.ShowDialog(); // Afficher la page admin
                }

                this.Hide(); // Masquer la fenêtre actuelle (connexion)
                utilisateur.Close();
            }
                else
                {
                    // Si l'utilisateur ne correspond pas à la base de données ou au rôle choisi
                    MessageBox.Show("Erreur : l'utilisateur ou le rôle ne correspondent pas dans la base de données.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    utilisateur.Close();
                }

            }
            catch (Exception ex)
            {
                // Si une erreur de connexion à la base de données se produit
                MessageBox.Show("Erreur de connexion : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pageConnex_Load(object sender, EventArgs e)
        {

        }
    }
}





