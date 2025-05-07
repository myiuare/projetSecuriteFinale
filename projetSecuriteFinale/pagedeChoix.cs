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
    public partial class pagedeChoix : Form
    {
        public pagedeChoix()
        {
            InitializeComponent();
        }

        private void buttonConnexion_Click(object sender, EventArgs e)
        {
            this.Hide();  // Masque pagedeChoix
            Form pageDeConnexion = new pageConnex(); // Crée une nouvelle instance de pageConnex
            pageDeConnexion.ShowDialog(); // Ouvre pageDeConnexion de manière modale
           // this.Show();  // Une fois pageDeConnexion fermée, réaffiche pagedeChoix
        }

        private void buttonInscription_Click(object sender, EventArgs e)
        {
            // Ajouter ici la logique pour le bouton d'inscription si nécessaire.
        }
    }
}