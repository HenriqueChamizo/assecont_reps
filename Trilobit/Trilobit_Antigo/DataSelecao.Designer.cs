namespace Trilobit
{
    partial class DataSelecao
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.edDataInicial = new System.Windows.Forms.DateTimePicker();
            this.edDataFinal = new System.Windows.Forms.DateTimePicker();
            this.cbImportarTudo = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.panel1.Controls.Add(this.btCancelar);
            this.panel1.Controls.Add(this.btOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 244);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 41);
            this.panel1.TabIndex = 3;
            // 
            // btCancelar
            // 
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.Location = new System.Drawing.Point(318, 9);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(75, 23);
            this.btCancelar.TabIndex = 1;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Location = new System.Drawing.Point(237, 9);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 0;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(12, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "Importação de Marcações";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Informe o período de importação";
            // 
            // edDataInicial
            // 
            this.edDataInicial.CustomFormat = "";
            this.edDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.edDataInicial.Location = new System.Drawing.Point(16, 152);
            this.edDataInicial.Name = "edDataInicial";
            this.edDataInicial.Size = new System.Drawing.Size(93, 21);
            this.edDataInicial.TabIndex = 0;
            // 
            // edDataFinal
            // 
            this.edDataFinal.CustomFormat = "";
            this.edDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.edDataFinal.Location = new System.Drawing.Point(115, 152);
            this.edDataFinal.Name = "edDataFinal";
            this.edDataFinal.Size = new System.Drawing.Size(93, 21);
            this.edDataFinal.TabIndex = 1;
            // 
            // cbImportarTudo
            // 
            this.cbImportarTudo.AutoSize = true;
            this.cbImportarTudo.Location = new System.Drawing.Point(16, 203);
            this.cbImportarTudo.Name = "cbImportarTudo";
            this.cbImportarTudo.Size = new System.Drawing.Size(93, 17);
            this.cbImportarTudo.TabIndex = 2;
            this.cbImportarTudo.Text = "Importar tudo";
            this.cbImportarTudo.UseVisualStyleBackColor = true;
            this.cbImportarTudo.CheckedChanged += new System.EventHandler(this.cbImportarTudo_CheckedChanged);
            // 
            // DataSelecao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(404, 285);
            this.Controls.Add(this.cbImportarTudo);
            this.Controls.Add(this.edDataFinal);
            this.Controls.Add(this.edDataInicial);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataSelecao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataSelecao";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker edDataInicial;
        public System.Windows.Forms.DateTimePicker edDataFinal;
        public System.Windows.Forms.CheckBox cbImportarTudo;
    }
}