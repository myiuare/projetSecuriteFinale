
namespace projetSecuriteFinale
{
    partial class FormReinitialisation
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
            this.textBoxTokenN = new System.Windows.Forms.TextBox();
            this.mailNouveauMotDePasse = new System.Windows.Forms.TextBox();
            this.textBoxNouveauMotDePasse = new System.Windows.Forms.TextBox();
            this.buttonChangerMotDePasse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxToken = new System.Windows.Forms.Label();
            this.labelEmailNMDP = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxTokenN
            // 
            this.textBoxTokenN.Location = new System.Drawing.Point(339, 133);
            this.textBoxTokenN.Name = "textBoxTokenN";
            this.textBoxTokenN.Size = new System.Drawing.Size(127, 20);
            this.textBoxTokenN.TabIndex = 0;
            this.textBoxTokenN.TextChanged += new System.EventHandler(this.textBoxNouveauMotDePasse_TextChanged);
            // 
            // mailNouveauMotDePasse
            // 
            this.mailNouveauMotDePasse.Location = new System.Drawing.Point(339, 182);
            this.mailNouveauMotDePasse.Name = "mailNouveauMotDePasse";
            this.mailNouveauMotDePasse.Size = new System.Drawing.Size(155, 20);
            this.mailNouveauMotDePasse.TabIndex = 1;
            // 
            // textBoxNouveauMotDePasse
            // 
            this.textBoxNouveauMotDePasse.Location = new System.Drawing.Point(339, 93);
            this.textBoxNouveauMotDePasse.Name = "textBoxNouveauMotDePasse";
            this.textBoxNouveauMotDePasse.Size = new System.Drawing.Size(127, 20);
            this.textBoxNouveauMotDePasse.TabIndex = 2;
            // 
            // buttonChangerMotDePasse
            // 
            this.buttonChangerMotDePasse.Location = new System.Drawing.Point(351, 304);
            this.buttonChangerMotDePasse.Name = "buttonChangerMotDePasse";
            this.buttonChangerMotDePasse.Size = new System.Drawing.Size(75, 23);
            this.buttonChangerMotDePasse.TabIndex = 3;
            this.buttonChangerMotDePasse.Text = "Valider";
            this.buttonChangerMotDePasse.UseVisualStyleBackColor = true;
            this.buttonChangerMotDePasse.Click += new System.EventHandler(this.buttonChangerMotDePasse_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nouveau mot de passe";
            // 
            // textBoxToken
            // 
            this.textBoxToken.AutoSize = true;
            this.textBoxToken.Location = new System.Drawing.Point(295, 140);
            this.textBoxToken.Name = "textBoxToken";
            this.textBoxToken.Size = new System.Drawing.Size(38, 13);
            this.textBoxToken.TabIndex = 5;
            this.textBoxToken.Text = "Token";
            // 
            // labelEmailNMDP
            // 
            this.labelEmailNMDP.AutoSize = true;
            this.labelEmailNMDP.Location = new System.Drawing.Point(298, 189);
            this.labelEmailNMDP.Name = "labelEmailNMDP";
            this.labelEmailNMDP.Size = new System.Drawing.Size(35, 13);
            this.labelEmailNMDP.TabIndex = 6;
            this.labelEmailNMDP.Text = "E-mail";
            // 
            // FormReinitialisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelEmailNMDP);
            this.Controls.Add(this.textBoxToken);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonChangerMotDePasse);
            this.Controls.Add(this.textBoxNouveauMotDePasse);
            this.Controls.Add(this.mailNouveauMotDePasse);
            this.Controls.Add(this.textBoxTokenN);
            this.Name = "FormReinitialisation";
            this.Text = "FormReinitialisation";
            this.Load += new System.EventHandler(this.FormReinitialisation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTokenN;
        private System.Windows.Forms.TextBox mailNouveauMotDePasse;
        private System.Windows.Forms.TextBox textBoxNouveauMotDePasse;
        private System.Windows.Forms.Button buttonChangerMotDePasse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label textBoxToken;
        private System.Windows.Forms.Label labelEmailNMDP;
    }
}