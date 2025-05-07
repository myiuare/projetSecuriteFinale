
namespace projetSecuriteFinale
{
    partial class pagedeChoix
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonConnexion = new System.Windows.Forms.Button();
            this.buttonInscription = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonConnexion
            // 
            this.buttonConnexion.BackColor = System.Drawing.Color.Indigo;
            this.buttonConnexion.Font = new System.Drawing.Font("Noto Mono", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnexion.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonConnexion.Location = new System.Drawing.Point(343, 312);
            this.buttonConnexion.Name = "buttonConnexion";
            this.buttonConnexion.Size = new System.Drawing.Size(138, 40);
            this.buttonConnexion.TabIndex = 7;
            this.buttonConnexion.Text = "Se connecter";
            this.buttonConnexion.UseVisualStyleBackColor = false;
            this.buttonConnexion.Click += new System.EventHandler(this.buttonConnexion_Click);
            // 
            // buttonInscription
            // 
            this.buttonInscription.BackColor = System.Drawing.Color.Indigo;
            this.buttonInscription.Font = new System.Drawing.Font("Noto Mono", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInscription.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonInscription.Location = new System.Drawing.Point(343, 254);
            this.buttonInscription.Name = "buttonInscription";
            this.buttonInscription.Size = new System.Drawing.Size(138, 40);
            this.buttonInscription.TabIndex = 6;
            this.buttonInscription.Text = "S\'inscrire";
            this.buttonInscription.UseVisualStyleBackColor = false;
            this.buttonInscription.Click += new System.EventHandler(this.buttonInscription_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::projetSecuriteFinale.Properties.Resources.lycee_marie;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(76, 376);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(660, 149);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::projetSecuriteFinale.Properties.Resources._1570528548468;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-76, -15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(995, 249);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pagedeChoix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 537);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.buttonConnexion);
            this.Controls.Add(this.buttonInscription);
            this.Controls.Add(this.pictureBox1);
            this.Name = "pagedeChoix";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonConnexion;
        private System.Windows.Forms.Button buttonInscription;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

