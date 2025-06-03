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


        private void btnValider_Click(object sender, EventArgs e)
        {
            // Récupère le token saisi par l'utilisateur (code reçu par mail)
            string token = textBoxToken.Text.Trim();

            // Récupère le nouveau mot de passe saisi par l'utilisateur
            string nouveauMdp = textBoxNouveauMotDePasse.Text;

            // Récupère la confirmation du mot de passe
            string confirmerMdp = textBoxConfirmerMotDePasse.Text;

            // Vérifie que le token n'est pas vide
            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Veuillez entrer le code de réinitialisation."); // avertit si vide
                return;
            }

            // Recherche l'utilisateur en base via le token
            var utilisateur = UtilisateurDAO.ObtenirParToken(token);

            // Vérifie que le token existe et n'est pas expiré
            if (utilisateur == null || utilisateur.ResetTokenExpiration < DateTime.Now)
            {
                MessageBox.Show("Code invalide ou expiré."); // avertit si token invalide ou expiré
                return;
            }

            // Vérifie que le nouveau mot de passe et la confirmation correspondent
            if (nouveauMdp != confirmerMdp)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas."); // avertit si pas de correspondance
                return;
            }

            // Vérifie la robustesse du mot de passe via ta fonction dédiée (ex: longueur, majuscules, chiffres...)
            if (!UtilsMotDePasse.EstMotDePasseRobuste(nouveauMdp))
            {
                MessageBox.Show("Mot de passe trop faible."); // avertit si mot de passe faible
                return;
            }

            // Génère un sel aléatoire (pour sécuriser le hash)
            string sel = UtilsMotDePasse.GenererSel();

            // Hash le mot de passe combiné avec le sel (algorithme SHA256 ou autre)
            string hash = UtilsMotDePasse.        HasherAvecSel(nouveauMdp, sel);

            // Met à jour le mot de passe haché et le sel en base, avec date de modification et expiration
            UtilisateurDAO.MettreAJourMotDePasse(utilisateur.Id, hash, sel);

            // Invalide le token pour éviter sa réutilisation
            UtilisateurDAO.InvaliderResetToken(token);

            // Confirme la réussite à l'utilisateur
            MessageBox.Show("Mot de passe mis à jour !");

            // Ferme la fenêtre de réinitialisation
            this.Close();
        }
        }

    }



    

