namespace DemoREP_CSharp
{
    partial class Frm_CadastraEmpregador
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Rdb_Inclusao = new System.Windows.Forms.RadioButton();
            this.Rdb_Alteracao = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Rdb_Juridica = new System.Windows.Forms.RadioButton();
            this.Rdb_Fisica = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.Txt_CNPJCPF = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_CEI = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Txt_RazaoSocial = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Txt_LocalTrabalho = new System.Windows.Forms.TextBox();
            this.Btn_Confirmar = new System.Windows.Forms.Button();
            this.Btn_Cancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Txt_LocalTrabalho);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Txt_RazaoSocial);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Txt_CEI);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.Txt_CNPJCPF);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(5, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 203);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Rdb_Alteracao);
            this.groupBox2.Controls.Add(this.Rdb_Inclusao);
            this.groupBox2.Location = new System.Drawing.Point(8, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 49);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo de Operação:";
            // 
            // Rdb_Inclusao
            // 
            this.Rdb_Inclusao.AutoSize = true;
            this.Rdb_Inclusao.Checked = true;
            this.Rdb_Inclusao.Location = new System.Drawing.Point(12, 20);
            this.Rdb_Inclusao.Name = "Rdb_Inclusao";
            this.Rdb_Inclusao.Size = new System.Drawing.Size(65, 17);
            this.Rdb_Inclusao.TabIndex = 0;
            this.Rdb_Inclusao.TabStop = true;
            this.Rdb_Inclusao.Text = "Inclusão";
            this.Rdb_Inclusao.UseVisualStyleBackColor = true;
            this.Rdb_Inclusao.CheckedChanged += new System.EventHandler(this.Rdb_Inclusao_CheckedChanged);
            // 
            // Rdb_Alteracao
            // 
            this.Rdb_Alteracao.AutoSize = true;
            this.Rdb_Alteracao.Location = new System.Drawing.Point(84, 20);
            this.Rdb_Alteracao.Name = "Rdb_Alteracao";
            this.Rdb_Alteracao.Size = new System.Drawing.Size(70, 17);
            this.Rdb_Alteracao.TabIndex = 1;
            this.Rdb_Alteracao.Text = "Alteração";
            this.Rdb_Alteracao.UseVisualStyleBackColor = true;
            this.Rdb_Alteracao.CheckedChanged += new System.EventHandler(this.Rdb_Alteracao_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Rdb_Fisica);
            this.groupBox3.Controls.Add(this.Rdb_Juridica);
            this.groupBox3.Location = new System.Drawing.Point(215, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(181, 49);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de Pessoa:";
            // 
            // Rdb_Juridica
            // 
            this.Rdb_Juridica.AutoSize = true;
            this.Rdb_Juridica.Checked = true;
            this.Rdb_Juridica.Location = new System.Drawing.Point(13, 20);
            this.Rdb_Juridica.Name = "Rdb_Juridica";
            this.Rdb_Juridica.Size = new System.Drawing.Size(63, 17);
            this.Rdb_Juridica.TabIndex = 0;
            this.Rdb_Juridica.TabStop = true;
            this.Rdb_Juridica.Text = "Jurídica";
            this.Rdb_Juridica.UseVisualStyleBackColor = true;
            this.Rdb_Juridica.CheckedChanged += new System.EventHandler(this.Rdb_Juridica_CheckedChanged);
            // 
            // Rdb_Fisica
            // 
            this.Rdb_Fisica.AutoSize = true;
            this.Rdb_Fisica.Location = new System.Drawing.Point(83, 20);
            this.Rdb_Fisica.Name = "Rdb_Fisica";
            this.Rdb_Fisica.Size = new System.Drawing.Size(54, 17);
            this.Rdb_Fisica.TabIndex = 1;
            this.Rdb_Fisica.Text = "Física";
            this.Rdb_Fisica.UseVisualStyleBackColor = true;
            this.Rdb_Fisica.CheckedChanged += new System.EventHandler(this.Rdb_Fisica_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "CNPJ / CPF:";
            // 
            // Txt_CNPJCPF
            // 
            this.Txt_CNPJCPF.Location = new System.Drawing.Point(8, 84);
            this.Txt_CNPJCPF.MaxLength = 14;
            this.Txt_CNPJCPF.Name = "Txt_CNPJCPF";
            this.Txt_CNPJCPF.Size = new System.Drawing.Size(100, 20);
            this.Txt_CNPJCPF.TabIndex = 3;
            this.Txt_CNPJCPF.Text = "00908118000112";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "CEI:";
            // 
            // Txt_CEI
            // 
            this.Txt_CEI.Location = new System.Drawing.Point(115, 84);
            this.Txt_CEI.MaxLength = 12;
            this.Txt_CEI.Name = "Txt_CEI";
            this.Txt_CEI.Size = new System.Drawing.Size(100, 20);
            this.Txt_CEI.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Razão Social:";
            // 
            // Txt_RazaoSocial
            // 
            this.Txt_RazaoSocial.Location = new System.Drawing.Point(8, 128);
            this.Txt_RazaoSocial.MaxLength = 150;
            this.Txt_RazaoSocial.Name = "Txt_RazaoSocial";
            this.Txt_RazaoSocial.Size = new System.Drawing.Size(388, 20);
            this.Txt_RazaoSocial.TabIndex = 7;
            this.Txt_RazaoSocial.Text = "ZPM";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Local Trabalho:";
            // 
            // Txt_LocalTrabalho
            // 
            this.Txt_LocalTrabalho.Location = new System.Drawing.Point(8, 172);
            this.Txt_LocalTrabalho.MaxLength = 100;
            this.Txt_LocalTrabalho.Name = "Txt_LocalTrabalho";
            this.Txt_LocalTrabalho.Size = new System.Drawing.Size(388, 20);
            this.Txt_LocalTrabalho.TabIndex = 9;
            this.Txt_LocalTrabalho.Text = "Rua Araguaia, 175";
            // 
            // Btn_Confirmar
            // 
            this.Btn_Confirmar.Location = new System.Drawing.Point(131, 212);
            this.Btn_Confirmar.Name = "Btn_Confirmar";
            this.Btn_Confirmar.Size = new System.Drawing.Size(75, 23);
            this.Btn_Confirmar.TabIndex = 1;
            this.Btn_Confirmar.Text = "Confirmar";
            this.Btn_Confirmar.UseVisualStyleBackColor = true;
            this.Btn_Confirmar.Click += new System.EventHandler(this.Btn_Confirmar_Click);
            // 
            // Btn_Cancelar
            // 
            this.Btn_Cancelar.Location = new System.Drawing.Point(210, 212);
            this.Btn_Cancelar.Name = "Btn_Cancelar";
            this.Btn_Cancelar.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancelar.TabIndex = 2;
            this.Btn_Cancelar.Text = "Cancelar";
            this.Btn_Cancelar.UseVisualStyleBackColor = true;
            this.Btn_Cancelar.Click += new System.EventHandler(this.Btn_Cancelar_Click);
            // 
            // Frm_CadastraEmpregador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 243);
            this.Controls.Add(this.Btn_Cancelar);
            this.Controls.Add(this.Btn_Confirmar);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_CadastraEmpregador";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastra Empregador";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton Rdb_Inclusao;
        private System.Windows.Forms.RadioButton Rdb_Alteracao;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton Rdb_Fisica;
        private System.Windows.Forms.RadioButton Rdb_Juridica;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Txt_CNPJCPF;
        private System.Windows.Forms.TextBox Txt_CEI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_RazaoSocial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Txt_LocalTrabalho;
        private System.Windows.Forms.Button Btn_Confirmar;
        private System.Windows.Forms.Button Btn_Cancelar;
    }
}