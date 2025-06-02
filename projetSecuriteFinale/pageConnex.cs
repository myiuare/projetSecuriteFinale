using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
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

            try
            {
                if (VerifierConnexion(log, motDepass))
                {
                    using (MySqlConnection conn = new MySqlConnection("server=localhost; database=gestion_securite; uid=root; pwd="))
                    {
                        conn.Open();

                        string query = "SELECT role FROM utilisateur WHERE email = @mail";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@mail", log);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string role = reader["role"].ToString();
                                MessageBox.Show("Connexion réussie !", "Bienvenue", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                        }
                    }
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

        private void btnMigrate_Click(object sender, EventArgs e)
        {
            MigratePasswords.UpdatePasswordsToHash();
            MessageBox.Show("Migration terminée !");
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

        // Appelle une méthode déjà sécurisée, mais tu peux aussi déplacer celle-ci dans une classe Utils
        public static bool VerifierConnexion(string email, string motDePasse)
        {
            string connectionString = "server=localhost; database=gestion_securite; uid=root; pwd=";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string sql = "SELECT mot_de_passe, sel FROM utilisateur WHERE email = @mail AND is_active = 1";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@mail", email);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string hashEnBase = reader["mot_de_passe"].ToString();
                        string sel = reader["sel"].ToString();

                        string hashEntre = HashMotDePasseAvecSel(motDePasse, sel);

                        return hashEnBase == hashEntre;
                    }
                }
            }

            return false;
        }

        // Fonction pour hasher avec un sel
        public static string HashMotDePasseAvecSel(string motDePasse, string sel)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(motDePasse + sel);
                byte[] hash = sha256.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
