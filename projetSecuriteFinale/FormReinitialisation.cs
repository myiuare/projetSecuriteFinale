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
   

        public partial class FormReinitialisation : Form
        {
            public FormReinitialisation()
            {
                InitializeComponent();
            }



        private void FormReinitialisation_Load(object sender, EventArgs e)
        {

        }

        private void buttonChangerMotDePasse_Click_1(object sender, EventArgs e)
        {
            string token = textBoxTokenN.Text.Trim();
            string nouveauMdp = textBoxNouveauMotDePasse.Text;

            MessageBox.Show($"Token saisi : '{token}'");

            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Veuillez entrer le code de réinitialisation.");
                return;
            }

            var utilisateur = UtilisateurDAO.ObtenirParToken(token);
            if (utilisateur == null)
            {
                MessageBox.Show("Token non trouvé.");
                return;
            }

            if (utilisateur.ResetTokenExpiration == null || utilisateur.ResetTokenExpiration < DateTime.Now)
            {
                MessageBox.Show("Code expiré.");
                return;
            }

            if (!UtilsMotDePasse.EstMotDePasseRobuste(nouveauMdp))
            {
                MessageBox.Show("Mot de passe trop faible.");
                return;
            }

            // === ICI ON GENERE LE SEL ET LE HASH AVEC LE SEL ===
            string sel = Securite.PasswordUtils.GenererSel(); // génère un sel aléatoire
            string hash = Securite.PasswordUtils.HashMotDePasse(nouveauMdp, sel); // hash mot de passe + sel
           


            try
            {
                UtilisateurDAO.MettreAJourMotDePasse(utilisateur.Id, hash, sel); // enregistre hash et sel en BDD
                UtilisateurDAO.InvaliderResetToken(token); // supprime le token utilisé
                MessageBox.Show("Mot de passe mis à jour !");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la mise à jour : " + ex.Message);
            }
        }


        private void textBoxNouveauMotDePasse_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }


 

    