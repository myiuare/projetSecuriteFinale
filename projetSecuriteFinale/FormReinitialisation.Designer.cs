
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
            this.textBoxNouveauMotDePasse = new System.Windows.Forms.TextBox();
            this.textBoxToken = new System.Windows.Forms.TextBox();
            this.textBoxConfirmerMotDePasse = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnValider = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxNouveauMotDePasse
            // 
            this.textBoxNouveauMotDePasse.Location = new System.Drawing.Point(350, 137);
            this.textBoxNouveauMotDePasse.Name = "textBoxNouveauMotDePasse";
            this.textBoxNouveauMotDePasse.Size = new System.Drawing.Size(198, 20);
            this.textBoxNouveauMotDePasse.TabIndex = 0;
            // 
            // textBoxToken
            // 
            this.textBoxToken.Location = new System.Drawing.Point(350, 179);
            this.textBoxToken.Name = "textBoxToken";
            this.textBoxToken.Size = new System.Drawing.Size(256, 20);
            this.textBoxToken.TabIndex = 1;
            // 
            // textBoxConfirmerMotDePasse
            // 
            this.textBoxConfirmerMotDePasse.Location = new System.Drawing.Point(350, 219);
            this.textBoxConfirmerMotDePasse.Name = "textBoxConfirmerMotDePasse";
            this.textBoxConfirmerMotDePasse.Size = new System.Drawing.Size(198, 20);
            this.textBoxConfirmerMotDePasse.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(264, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Token fourni";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nouveau Mot De Passe";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Confirmation Nouveau Mot De Passe";
            // 
            // btnValider
            // 
            this.btnValider.Location = new System.Drawing.Point(473, 368);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(75, 23);
            this.btnValider.TabIndex = 6;
            this.btnValider.Text = "Valider";
            this.btnValider.UseVisualStyleBackColor = true;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // FormReinitialisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxConfirmerMotDePasse);
            this.Controls.Add(this.textBoxToken);
            this.Controls.Add(this.textBoxNouveauMotDePasse);
            this.Name = "FormReinitialisation";
            this.Text = "FormReinitialisation";
            this.Load += new System.EventHandler(this.FormReinitialisation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNouveauMotDePasse;
        private System.Windows.Forms.TextBox textBoxToken;
        private System.Windows.Forms.TextBox textBoxConfirmerMotDePasse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnValider;
    }
}