namespace DemoREP_CSharp
{
    partial class Frm_BuscaPonto
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_DataInicial = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_DataFinal = new System.Windows.Forms.TextBox();
            this.Btn_Localizar = new System.Windows.Forms.Button();
            this.Lst_Marcacoes = new System.Windows.Forms.ListBox();
            this.Btn_Sair = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Lst_Marcacoes);
            this.groupBox1.Controls.Add(this.Btn_Localizar);
            this.groupBox1.Controls.Add(this.Txt_DataFinal);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Txt_DataInicial);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(437, 227);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Inicial:";
            // 
            // Txt_DataInicial
            // 
            this.Txt_DataInicial.Location = new System.Drawing.Point(72, 15);
            this.Txt_DataInicial.Name = "Txt_DataInicial";
            this.Txt_DataInicial.Size = new System.Drawing.Size(72, 20);
            this.Txt_DataInicial.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data Final:";
            // 
            // Txt_DataFinal
            // 
            this.Txt_DataFinal.Location = new System.Drawing.Point(210, 15);
            this.Txt_DataFinal.Name = "Txt_DataFinal";
            this.Txt_DataFinal.Size = new System.Drawing.Size(70, 20);
            this.Txt_DataFinal.TabIndex = 3;
            // 
            // Btn_Localizar
            // 
            this.Btn_Localizar.Location = new System.Drawing.Point(297, 13);
            this.Btn_Localizar.Name = "Btn_Localizar";
            this.Btn_Localizar.Size = new System.Drawing.Size(75, 23);
            this.Btn_Localizar.TabIndex = 4;
            this.Btn_Localizar.Text = "Localizar";
            this.Btn_Localizar.UseVisualStyleBackColor = true;
            this.Btn_Localizar.Click += new System.EventHandler(this.Btn_Localizar_Click);
            // 
            // Lst_Marcacoes
            // 
            this.Lst_Marcacoes.FormattingEnabled = true;
            this.Lst_Marcacoes.Location = new System.Drawing.Point(9, 45);
            this.Lst_Marcacoes.Name = "Lst_Marcacoes";
            this.Lst_Marcacoes.Size = new System.Drawing.Size(419, 173);
            this.Lst_Marcacoes.TabIndex = 5;
            // 
            // Btn_Sair
            // 
            this.Btn_Sair.Location = new System.Drawing.Point(178, 235);
            this.Btn_Sair.Name = "Btn_Sair";
            this.Btn_Sair.Size = new System.Drawing.Size(75, 23);
            this.Btn_Sair.TabIndex = 1;
            this.Btn_Sair.Text = "Sair";
            this.Btn_Sair.UseVisualStyleBackColor = true;
            this.Btn_Sair.Click += new System.EventHandler(this.Btn_Sair_Click);
            // 
            // Frm_BuscaPonto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 264);
            this.Controls.Add(this.Btn_Sair);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BuscaPonto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Busca Ponto";
            this.Load += new System.EventHandler(this.Frm_BuscaPonto_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Txt_DataInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Txt_DataFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Btn_Localizar;
        private System.Windows.Forms.ListBox Lst_Marcacoes;
        private System.Windows.Forms.Button Btn_Sair;
    }
}