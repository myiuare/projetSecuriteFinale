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


    public partial class pageProfesseur : Form
    {
        public pageProfesseur()
        {
            InitializeComponent();
        }

        private void gestionDesÉlèvesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();  // Masque la page
            Form pageDeGestionEleve = new GestionMotdePasse(); // Crée une nouvelle instance de pagemdp
            pageDeGestionEleve.ShowDialog(); // Ouvre pageDemdp de manière modale           
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pageProfesseur_Load(object sender, EventArgs e)
        {

        }
    }
}
