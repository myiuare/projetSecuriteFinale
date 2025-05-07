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
            // On crée une nouvelle instance à chaque clic pour éviter une erreur si le formulaire a été fermé.
           
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
