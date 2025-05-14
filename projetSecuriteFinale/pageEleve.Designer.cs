
namespace projetSecuriteFinale
{
    partial class pageEleve
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.stagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDesStagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paramètresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marie_curie_picturebox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marie_curie_picturebox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stagesToolStripMenuItem,
            this.paramètresToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1443, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // stagesToolStripMenuItem
            // 
            this.stagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionDesStagesToolStripMenuItem});
            this.stagesToolStripMenuItem.Name = "stagesToolStripMenuItem";
            this.stagesToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.stagesToolStripMenuItem.Text = "Stages";
            // 
            // gestionDesStagesToolStripMenuItem
            // 
            this.gestionDesStagesToolStripMenuItem.Name = "gestionDesStagesToolStripMenuItem";
            this.gestionDesStagesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gestionDesStagesToolStripMenuItem.Text = "Gestion des stages";
            this.gestionDesStagesToolStripMenuItem.Click += new System.EventHandler(this.gestionDesStagesToolStripMenuItem_Click);
            // 
            // paramètresToolStripMenuItem
            // 
            this.paramètresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitterToolStripMenuItem});
            this.paramètresToolStripMenuItem.Name = "paramètresToolStripMenuItem";
            this.paramètresToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.paramètresToolStripMenuItem.Text = "Paramètres";
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.quitterToolStripMenuItem.Text = "Quitter";
            // 
            // marie_curie_picturebox
            // 
            this.marie_curie_picturebox.BackgroundImage = global::projetSecuriteFinale.Properties.Resources.lycee_marie;
            this.marie_curie_picturebox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.marie_curie_picturebox.Location = new System.Drawing.Point(41, 401);
            this.marie_curie_picturebox.Name = "marie_curie_picturebox";
            this.marie_curie_picturebox.Size = new System.Drawing.Size(1360, 342);
            this.marie_curie_picturebox.TabIndex = 7;
            this.marie_curie_picturebox.TabStop = false;
            // 
            // pageEleve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1443, 862);
            this.Controls.Add(this.marie_curie_picturebox);
            this.Controls.Add(this.menuStrip1);
            this.Name = "pageEleve";
            this.Text = "pageEleve";
            this.Load += new System.EventHandler(this.pageEleve_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marie_curie_picturebox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem stagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionDesStagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paramètresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.PictureBox marie_curie_picturebox;
    }
}