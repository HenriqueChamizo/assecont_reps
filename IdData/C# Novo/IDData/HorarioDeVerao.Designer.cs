namespace IdData
{
    partial class HorarioDeVerao
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
            this.btEnviar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maskedTextBoxInicio = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxFim = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // btEnviar
            // 
            this.btEnviar.Location = new System.Drawing.Point(92, 113);
            this.btEnviar.Name = "btEnviar";
            this.btEnviar.Size = new System.Drawing.Size(103, 30);
            this.btEnviar.TabIndex = 0;
            this.btEnviar.Text = "Enviar";
            this.btEnviar.UseVisualStyleBackColor = true;
            this.btEnviar.Click += new System.EventHandler(this.btEnviar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Inicio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Fim";
            // 
            // maskedTextBoxInicio
            // 
            this.maskedTextBoxInicio.Location = new System.Drawing.Point(96, 33);
            this.maskedTextBoxInicio.Mask = "00/00/0000";
            this.maskedTextBoxInicio.Name = "maskedTextBoxInicio";
            this.maskedTextBoxInicio.Size = new System.Drawing.Size(100, 20);
            this.maskedTextBoxInicio.TabIndex = 6;
            this.maskedTextBoxInicio.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTextBoxFim
            // 
            this.maskedTextBoxFim.Location = new System.Drawing.Point(95, 72);
            this.maskedTextBoxFim.Mask = "00/00/0000";
            this.maskedTextBoxFim.Name = "maskedTextBoxFim";
            this.maskedTextBoxFim.Size = new System.Drawing.Size(100, 20);
            this.maskedTextBoxFim.TabIndex = 7;
            this.maskedTextBoxFim.ValidatingType = typeof(System.DateTime);
            // 
            // HorarioDeVerao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 178);
            this.Controls.Add(this.maskedTextBoxFim);
            this.Controls.Add(this.maskedTextBoxInicio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btEnviar);
            this.Name = "HorarioDeVerao";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Horario de Verão";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btEnviar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxInicio;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxFim;
    }
}