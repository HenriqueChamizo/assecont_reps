namespace DemoREP_CSharp
{
    partial class Frm_BuscaPontoAFD
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
            this.Txt_DataInicio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_DataFim = new System.Windows.Forms.TextBox();
            this.Btn_Localizar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Btn_Sair = new System.Windows.Forms.Button();
            this.Lst_Marcacoes = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Btn_Localizar);
            this.groupBox1.Controls.Add(this.Txt_DataFim);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Txt_DataInicio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(448, 42);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Início:";
            // 
            // Txt_DataInicio
            // 
            this.Txt_DataInicio.Location = new System.Drawing.Point(75, 13);
            this.Txt_DataInicio.Name = "Txt_DataInicio";
            this.Txt_DataInicio.Size = new System.Drawing.Size(72, 20);
            this.Txt_DataInicio.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data Fim:";
            // 
            // Txt_DataFim
            // 
            this.Txt_DataFim.Location = new System.Drawing.Point(207, 13);
            this.Txt_DataFim.Name = "Txt_DataFim";
            this.Txt_DataFim.Size = new System.Drawing.Size(70, 20);
            this.Txt_DataFim.TabIndex = 3;
            // 
            // Btn_Localizar
            // 
            this.Btn_Localizar.Location = new System.Drawing.Point(284, 12);
            this.Btn_Localizar.Name = "Btn_Localizar";
            this.Btn_Localizar.Size = new System.Drawing.Size(75, 23);
            this.Btn_Localizar.TabIndex = 4;
            this.Btn_Localizar.Text = "Localizar";
            this.Btn_Localizar.UseVisualStyleBackColor = true;
            this.Btn_Localizar.Click += new System.EventHandler(this.Btn_Localizar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Lst_Marcacoes);
            this.groupBox2.Location = new System.Drawing.Point(5, 46);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(448, 181);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // Btn_Sair
            // 
            this.Btn_Sair.Location = new System.Drawing.Point(197, 233);
            this.Btn_Sair.Name = "Btn_Sair";
            this.Btn_Sair.Size = new System.Drawing.Size(75, 23);
            this.Btn_Sair.TabIndex = 2;
            this.Btn_Sair.Text = "Sair";
            this.Btn_Sair.UseVisualStyleBackColor = true;
            this.Btn_Sair.Click += new System.EventHandler(this.Btn_Sair_Click);
            // 
            // Lst_Marcacoes
            // 
            this.Lst_Marcacoes.FormattingEnabled = true;
            this.Lst_Marcacoes.HorizontalScrollbar = true;
            this.Lst_Marcacoes.Location = new System.Drawing.Point(8, 13);
            this.Lst_Marcacoes.Name = "Lst_Marcacoes";
            this.Lst_Marcacoes.ScrollAlwaysVisible = true;
            this.Lst_Marcacoes.Size = new System.Drawing.Size(434, 160);
            this.Lst_Marcacoes.TabIndex = 0;
            // 
            // Frm_BuscaPontoAFD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 264);
            this.Controls.Add(this.Btn_Sair);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_BuscaPontoAFD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Busca Ponto no formato AFD";
            this.Load += new System.EventHandler(this.Frm_BuscaPontoAFD_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Txt_DataInicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_DataFim;
        private System.Windows.Forms.Button Btn_Localizar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Btn_Sair;
        private System.Windows.Forms.ListBox Lst_Marcacoes;
    }
}