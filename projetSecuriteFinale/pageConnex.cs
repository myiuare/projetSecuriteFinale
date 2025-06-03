using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace projetSecuriteFinale
{
    public partial class pageConnex : Form
    {
        public pageConnex()
        {
            InitializeComponent();
        }

        private void pageConnex_Load(object sender, EventArgs e)
        {
        }

        private void buttonReChoix_Click(object sender, EventArgs e)
        {
            Application.Exit();        }

        private void buttonConnexion_Click(object sender, EventArgs e)
        {

            string log = log_connex.Text.Trim().ToLower();
            string motDepass = mdp_connex.Text.Trim();

                if (string.IsNullOrWhiteSpace(log) || string.IsNullOrWhiteSpace(motDepass))
                {
                    MessageBox.Show("Veuillez remplir tous les champs.", "Champs manquants", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    string role;
                    if (GestionEleves.VerifierConnexion(log, motDepass, out role))
                    {
                        MessageBox.Show("Connexion réussie ! Rôle : " + role, "Bienvenue", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();

                        if (role == "eleve")
                        {
                            new pageEleve().ShowDialog();
                        }
                        else if (role == "professeur")
                        {
                            new pageProfesseur().ShowDialog();
                        }
                        else if (role == "administrateur")
                        {
                            new pageAccueil().ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Rôle non reconnu. Veuillez contacter l’administrateur.", "Erreur rôle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("Identifiants incorrects.", "Erreur de connexion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur de connexion à la base : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        

       

        private void buttonReinitialiserMDP_Click(object sender, EventArgs e)
        {
            FormReinitialisation formReset = new FormReinitialisation();
            formReset.ShowDialog();
        }

        private void mdp_connex_TextChanged(object sender, EventArgs e)
        {
            // Événement inutilisé actuellement
        }

        private void btnMigrate_Click(object sender, EventArgs e)
        {
            MigratePasswords.UpdatePasswordsToHash();
            MessageBox.Show("Migration terminée !");
        }
    }
}
