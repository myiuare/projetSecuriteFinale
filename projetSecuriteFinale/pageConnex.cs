using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.IO;  // Pour File.Exists

namespace projetSecuriteFinale
{
    public partial class pageConnex : Form
    {
        public pageConnex()
        {
            InitializeComponent();
        }

        // Fonction pour hasher le mot de passe en SHA256
        public static string HashMotDePasse(string motDePasse)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(motDePasse);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        

        private void buttonReChoix_Click(object sender, EventArgs e)
        {
            this.Close(); // Ferme la fenêtre actuelle
        }

        private void buttonConnexion_Click(object sender, EventArgs e)
        {
            string log = log_connex.Text.Trim();
            string motDepass = mdp_connex.Text.Trim();

            if (string.IsNullOrWhiteSpace(log) || string.IsNullOrWhiteSpace(motDepass))
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Champs manquants", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hash du mot de passe saisi
            string motDepassHashe = HashMotDePasse(motDepass);

            string connectionString = "server=localhost; database=gestion_securite; uid=root; pwd=";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM utilisateur WHERE email = @email AND mot_de_passe = @mdp";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@email", log);
                    cmd.Parameters.AddWithValue("@mdp", motDepassHashe);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader["role"].ToString();

                            MessageBox.Show("Connexion réussie !", "Bienvenue", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Hide();

                            if (role == "eleve")
                            {
                                Form pageEleve = new pageEleve();
                                pageEleve.ShowDialog();
                            }
                            else if (role == "professeur")
                            {
                                Form pageProf = new pageProfesseur();
                                pageProf.ShowDialog();
                            }
                            else if (role == "administrateur")
                            {
                                Form pageAdmin = new pageAccueil();
                                pageAdmin.ShowDialog();
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connexion à la base : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pageConnex_Load(object sender, EventArgs e)
        {
        }

        private void btnMigrate_Click(object sender, EventArgs e)
        {
            
                MigratePasswords.UpdatePasswordsToHash();
                MessageBox.Show("Migration terminée !");
            
        }

        private void buttonReinitialiserMDP_Click(object sender, EventArgs e)
        {
            // Instancie le formulaire de réinitialisation
            FormReinitialisation formReset = new FormReinitialisation();

            // Affiche-le en modal (empêche d'interagir avec le formulaire parent tant que c'est ouvert)
            formReset.ShowDialog();

        }
    }
}
