namespace ExemploREP.Forms
{
    partial class frmFormatoCartao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormatoCartao));
            this.grbFormatoCartao = new System.Windows.Forms.GroupBox();
            this.btnApagaFormatoCartao = new System.Windows.Forms.Button();
            this.pnlInformacaoFixa = new System.Windows.Forms.Panel();
            this.btnInformacaoFixa = new System.Windows.Forms.Button();
            this.txtInformacaoFixa = new System.Windows.Forms.TextBox();
            this.txtFormatoCartao = new System.Windows.Forms.TextBox();
            this.lblFormatoCartao = new System.Windows.Forms.Label();
            this.pnlChecagem = new System.Windows.Forms.Panel();
            this.btnChecagem = new System.Windows.Forms.Button();
            this.lblChecagem = new System.Windows.Forms.Label();
            this.txtChecagem = new System.Windows.Forms.TextBox();
            this.pnlNumeroVia = new System.Windows.Forms.Panel();
            this.btnNumeroVia = new System.Windows.Forms.Button();
            this.lblNumeroVia = new System.Windows.Forms.Label();
            this.txtNumeroVia = new System.Windows.Forms.TextBox();
            this.pnlIgnorarDigito = new System.Windows.Forms.Panel();
            this.btnIgnorarDigito = new System.Windows.Forms.Button();
            this.lblIgnorarDigito = new System.Windows.Forms.Label();
            this.txtIgnorarDigito = new System.Windows.Forms.TextBox();
            this.pnlInformacaoOpcional = new System.Windows.Forms.Panel();
            this.btnInformacaoOpcional = new System.Windows.Forms.Button();
            this.lblInformacaoOpcional = new System.Windows.Forms.Label();
            this.txtInformacaoOpcional = new System.Windows.Forms.TextBox();
            this.pnlInformacao = new System.Windows.Forms.Panel();
            this.btnInformacao = new System.Windows.Forms.Button();
            this.lblInformacao = new System.Windows.Forms.Label();
            this.txtInformacao = new System.Windows.Forms.TextBox();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.grbFormatoCartao.SuspendLayout();
            this.pnlInformacaoFixa.SuspendLayout();
            this.pnlChecagem.SuspendLayout();
            this.pnlNumeroVia.SuspendLayout();
            this.pnlIgnorarDigito.SuspendLayout();
            this.pnlInformacaoOpcional.SuspendLayout();
            this.pnlInformacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbFormatoCartao
            // 
            this.grbFormatoCartao.BackColor = System.Drawing.SystemColors.Control;
            this.grbFormatoCartao.Controls.Add(this.btnApagaFormatoCartao);
            this.grbFormatoCartao.Controls.Add(this.pnlInformacaoFixa);
            this.grbFormatoCartao.Controls.Add(this.txtFormatoCartao);
            this.grbFormatoCartao.Controls.Add(this.lblFormatoCartao);
            this.grbFormatoCartao.Controls.Add(this.pnlChecagem);
            this.grbFormatoCartao.Controls.Add(this.pnlNumeroVia);
            this.grbFormatoCartao.Controls.Add(this.pnlIgnorarDigito);
            this.grbFormatoCartao.Controls.Add(this.pnlInformacaoOpcional);
            this.grbFormatoCartao.Controls.Add(this.pnlInformacao);
            this.grbFormatoCartao.Location = new System.Drawing.Point(8, 2);
            this.grbFormatoCartao.Name = "grbFormatoCartao";
            this.grbFormatoCartao.Size = new System.Drawing.Size(544, 208);
            this.grbFormatoCartao.TabIndex = 0;
            this.grbFormatoCartao.TabStop = false;
            // 
            // btnApagaFormatoCartao
            // 
            this.btnApagaFormatoCartao.Image = global::ExemploREP.Properties.Resources.eraser;
            this.btnApagaFormatoCartao.Location = new System.Drawing.Point(491, 164);
            this.btnApagaFormatoCartao.Name = "btnApagaFormatoCartao";
            this.btnApagaFormatoCartao.Size = new System.Drawing.Size(44, 37);
            this.btnApagaFormatoCartao.TabIndex = 21;
            this.btnApagaFormatoCartao.UseVisualStyleBackColor = true;
            this.btnApagaFormatoCartao.Click += new System.EventHandler(this.btnApagaFormatoCartao_Click);
            // 
            // pnlInformacaoFixa
            // 
            this.pnlInformacaoFixa.BackColor = System.Drawing.SystemColors.Control;
            this.pnlInformacaoFixa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInformacaoFixa.Controls.Add(this.btnInformacaoFixa);
            this.pnlInformacaoFixa.Controls.Add(this.txtInformacaoFixa);
            this.pnlInformacaoFixa.Location = new System.Drawing.Point(276, 105);
            this.pnlInformacaoFixa.Name = "pnlInformacaoFixa";
            this.pnlInformacaoFixa.Size = new System.Drawing.Size(258, 39);
            this.pnlInformacaoFixa.TabIndex = 17;
            // 
            // btnInformacaoFixa
            // 
            this.btnInformacaoFixa.Location = new System.Drawing.Point(7, 6);
            this.btnInformacaoFixa.Name = "btnInformacaoFixa";
            this.btnInformacaoFixa.Size = new System.Drawing.Size(167, 25);
            this.btnInformacaoFixa.TabIndex = 18;
            this.btnInformacaoFixa.Text = "Informação fixa";
            this.btnInformacaoFixa.UseVisualStyleBackColor = true;
            this.btnInformacaoFixa.Click += new System.EventHandler(this.btnInformacaoFixa_Click);
            // 
            // txtInformacaoFixa
            // 
            this.txtInformacaoFixa.BackColor = System.Drawing.SystemColors.Info;
            this.txtInformacaoFixa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInformacaoFixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInformacaoFixa.Location = new System.Drawing.Point(181, 9);
            this.txtInformacaoFixa.MaxLength = 20;
            this.txtInformacaoFixa.Name = "txtInformacaoFixa";
            this.txtInformacaoFixa.Size = new System.Drawing.Size(68, 20);
            this.txtInformacaoFixa.TabIndex = 19;
            this.txtInformacaoFixa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtFormatoCartao
            // 
            this.txtFormatoCartao.BackColor = System.Drawing.SystemColors.Info;
            this.txtFormatoCartao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFormatoCartao.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormatoCartao.Location = new System.Drawing.Point(10, 165);
            this.txtFormatoCartao.MaxLength = 20;
            this.txtFormatoCartao.Name = "txtFormatoCartao";
            this.txtFormatoCartao.ReadOnly = true;
            this.txtFormatoCartao.Size = new System.Drawing.Size(474, 35);
            this.txtFormatoCartao.TabIndex = 20;
            // 
            // lblFormatoCartao
            // 
            this.lblFormatoCartao.AutoSize = true;
            this.lblFormatoCartao.Location = new System.Drawing.Point(7, 149);
            this.lblFormatoCartao.Name = "lblFormatoCartao";
            this.lblFormatoCartao.Size = new System.Drawing.Size(94, 13);
            this.lblFormatoCartao.TabIndex = 5;
            this.lblFormatoCartao.Text = "Formato do Cartão";
            // 
            // pnlChecagem
            // 
            this.pnlChecagem.BackColor = System.Drawing.SystemColors.Control;
            this.pnlChecagem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlChecagem.Controls.Add(this.btnChecagem);
            this.pnlChecagem.Controls.Add(this.lblChecagem);
            this.pnlChecagem.Controls.Add(this.txtChecagem);
            this.pnlChecagem.Location = new System.Drawing.Point(10, 105);
            this.pnlChecagem.Name = "pnlChecagem";
            this.pnlChecagem.Size = new System.Drawing.Size(258, 39);
            this.pnlChecagem.TabIndex = 14;
            // 
            // btnChecagem
            // 
            this.btnChecagem.Location = new System.Drawing.Point(7, 6);
            this.btnChecagem.Name = "btnChecagem";
            this.btnChecagem.Size = new System.Drawing.Size(167, 25);
            this.btnChecagem.TabIndex = 15;
            this.btnChecagem.Text = "Checagem com";
            this.btnChecagem.UseVisualStyleBackColor = true;
            this.btnChecagem.Click += new System.EventHandler(this.btnChecagem_Click);
            // 
            // lblChecagem
            // 
            this.lblChecagem.AutoSize = true;
            this.lblChecagem.Location = new System.Drawing.Point(210, 12);
            this.lblChecagem.Name = "lblChecagem";
            this.lblChecagem.Size = new System.Drawing.Size(41, 13);
            this.lblChecagem.TabIndex = 2;
            this.lblChecagem.Text = "Dígitos";
            // 
            // txtChecagem
            // 
            this.txtChecagem.BackColor = System.Drawing.SystemColors.Info;
            this.txtChecagem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChecagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChecagem.Location = new System.Drawing.Point(181, 9);
            this.txtChecagem.MaxLength = 1;
            this.txtChecagem.Name = "txtChecagem";
            this.txtChecagem.Size = new System.Drawing.Size(25, 20);
            this.txtChecagem.TabIndex = 16;
            this.txtChecagem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlNumeroVia
            // 
            this.pnlNumeroVia.BackColor = System.Drawing.SystemColors.Control;
            this.pnlNumeroVia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNumeroVia.Controls.Add(this.btnNumeroVia);
            this.pnlNumeroVia.Controls.Add(this.lblNumeroVia);
            this.pnlNumeroVia.Controls.Add(this.txtNumeroVia);
            this.pnlNumeroVia.Location = new System.Drawing.Point(276, 60);
            this.pnlNumeroVia.Name = "pnlNumeroVia";
            this.pnlNumeroVia.Size = new System.Drawing.Size(258, 39);
            this.pnlNumeroVia.TabIndex = 10;
            // 
            // btnNumeroVia
            // 
            this.btnNumeroVia.Location = new System.Drawing.Point(7, 6);
            this.btnNumeroVia.Name = "btnNumeroVia";
            this.btnNumeroVia.Size = new System.Drawing.Size(167, 25);
            this.btnNumeroVia.TabIndex = 11;
            this.btnNumeroVia.Text = "Número da via com";
            this.btnNumeroVia.UseVisualStyleBackColor = true;
            this.btnNumeroVia.Click += new System.EventHandler(this.btnNumeroVia_Click);
            // 
            // lblNumeroVia
            // 
            this.lblNumeroVia.AutoSize = true;
            this.lblNumeroVia.Location = new System.Drawing.Point(210, 12);
            this.lblNumeroVia.Name = "lblNumeroVia";
            this.lblNumeroVia.Size = new System.Drawing.Size(41, 13);
            this.lblNumeroVia.TabIndex = 13;
            this.lblNumeroVia.Text = "Dígitos";
            // 
            // txtNumeroVia
            // 
            this.txtNumeroVia.BackColor = System.Drawing.SystemColors.Info;
            this.txtNumeroVia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumeroVia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroVia.Location = new System.Drawing.Point(181, 9);
            this.txtNumeroVia.MaxLength = 1;
            this.txtNumeroVia.Name = "txtNumeroVia";
            this.txtNumeroVia.Size = new System.Drawing.Size(25, 20);
            this.txtNumeroVia.TabIndex = 12;
            this.txtNumeroVia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlIgnorarDigito
            // 
            this.pnlIgnorarDigito.BackColor = System.Drawing.SystemColors.Control;
            this.pnlIgnorarDigito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlIgnorarDigito.Controls.Add(this.btnIgnorarDigito);
            this.pnlIgnorarDigito.Controls.Add(this.lblIgnorarDigito);
            this.pnlIgnorarDigito.Controls.Add(this.txtIgnorarDigito);
            this.pnlIgnorarDigito.Location = new System.Drawing.Point(10, 60);
            this.pnlIgnorarDigito.Name = "pnlIgnorarDigito";
            this.pnlIgnorarDigito.Size = new System.Drawing.Size(258, 39);
            this.pnlIgnorarDigito.TabIndex = 7;
            // 
            // btnIgnorarDigito
            // 
            this.btnIgnorarDigito.Location = new System.Drawing.Point(7, 6);
            this.btnIgnorarDigito.Name = "btnIgnorarDigito";
            this.btnIgnorarDigito.Size = new System.Drawing.Size(167, 25);
            this.btnIgnorarDigito.TabIndex = 8;
            this.btnIgnorarDigito.Text = "Ignorar dígito com";
            this.btnIgnorarDigito.UseVisualStyleBackColor = true;
            this.btnIgnorarDigito.Click += new System.EventHandler(this.btnIgnorarDigito_Click);
            // 
            // lblIgnorarDigito
            // 
            this.lblIgnorarDigito.AutoSize = true;
            this.lblIgnorarDigito.Location = new System.Drawing.Point(210, 12);
            this.lblIgnorarDigito.Name = "lblIgnorarDigito";
            this.lblIgnorarDigito.Size = new System.Drawing.Size(41, 13);
            this.lblIgnorarDigito.TabIndex = 2;
            this.lblIgnorarDigito.Text = "Dígitos";
            // 
            // txtIgnorarDigito
            // 
            this.txtIgnorarDigito.BackColor = System.Drawing.SystemColors.Info;
            this.txtIgnorarDigito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIgnorarDigito.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIgnorarDigito.Location = new System.Drawing.Point(181, 9);
            this.txtIgnorarDigito.MaxLength = 1;
            this.txtIgnorarDigito.Name = "txtIgnorarDigito";
            this.txtIgnorarDigito.Size = new System.Drawing.Size(25, 20);
            this.txtIgnorarDigito.TabIndex = 9;
            this.txtIgnorarDigito.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlInformacaoOpcional
            // 
            this.pnlInformacaoOpcional.BackColor = System.Drawing.SystemColors.Control;
            this.pnlInformacaoOpcional.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInformacaoOpcional.Controls.Add(this.btnInformacaoOpcional);
            this.pnlInformacaoOpcional.Controls.Add(this.lblInformacaoOpcional);
            this.pnlInformacaoOpcional.Controls.Add(this.txtInformacaoOpcional);
            this.pnlInformacaoOpcional.Location = new System.Drawing.Point(276, 15);
            this.pnlInformacaoOpcional.Name = "pnlInformacaoOpcional";
            this.pnlInformacaoOpcional.Size = new System.Drawing.Size(258, 39);
            this.pnlInformacaoOpcional.TabIndex = 4;
            // 
            // btnInformacaoOpcional
            // 
            this.btnInformacaoOpcional.Location = new System.Drawing.Point(7, 6);
            this.btnInformacaoOpcional.Name = "btnInformacaoOpcional";
            this.btnInformacaoOpcional.Size = new System.Drawing.Size(167, 25);
            this.btnInformacaoOpcional.TabIndex = 5;
            this.btnInformacaoOpcional.Text = "Informação opcional com";
            this.btnInformacaoOpcional.UseVisualStyleBackColor = true;
            this.btnInformacaoOpcional.Click += new System.EventHandler(this.btnInformacaoOpcional_Click);
            // 
            // lblInformacaoOpcional
            // 
            this.lblInformacaoOpcional.AutoSize = true;
            this.lblInformacaoOpcional.Location = new System.Drawing.Point(210, 12);
            this.lblInformacaoOpcional.Name = "lblInformacaoOpcional";
            this.lblInformacaoOpcional.Size = new System.Drawing.Size(41, 13);
            this.lblInformacaoOpcional.TabIndex = 2;
            this.lblInformacaoOpcional.Text = "Dígitos";
            // 
            // txtInformacaoOpcional
            // 
            this.txtInformacaoOpcional.BackColor = System.Drawing.SystemColors.Info;
            this.txtInformacaoOpcional.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInformacaoOpcional.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInformacaoOpcional.Location = new System.Drawing.Point(181, 9);
            this.txtInformacaoOpcional.MaxLength = 1;
            this.txtInformacaoOpcional.Name = "txtInformacaoOpcional";
            this.txtInformacaoOpcional.Size = new System.Drawing.Size(25, 20);
            this.txtInformacaoOpcional.TabIndex = 6;
            this.txtInformacaoOpcional.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlInformacao
            // 
            this.pnlInformacao.BackColor = System.Drawing.SystemColors.Control;
            this.pnlInformacao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInformacao.Controls.Add(this.btnInformacao);
            this.pnlInformacao.Controls.Add(this.lblInformacao);
            this.pnlInformacao.Controls.Add(this.txtInformacao);
            this.pnlInformacao.Location = new System.Drawing.Point(10, 15);
            this.pnlInformacao.Name = "pnlInformacao";
            this.pnlInformacao.Size = new System.Drawing.Size(258, 39);
            this.pnlInformacao.TabIndex = 0;
            // 
            // btnInformacao
            // 
            this.btnInformacao.Location = new System.Drawing.Point(7, 6);
            this.btnInformacao.Name = "btnInformacao";
            this.btnInformacao.Size = new System.Drawing.Size(167, 25);
            this.btnInformacao.TabIndex = 1;
            this.btnInformacao.Text = "Informação com";
            this.btnInformacao.UseVisualStyleBackColor = true;
            this.btnInformacao.Click += new System.EventHandler(this.btnInformacao_Click);
            // 
            // lblInformacao
            // 
            this.lblInformacao.AutoSize = true;
            this.lblInformacao.Location = new System.Drawing.Point(210, 12);
            this.lblInformacao.Name = "lblInformacao";
            this.lblInformacao.Size = new System.Drawing.Size(41, 13);
            this.lblInformacao.TabIndex = 3;
            this.lblInformacao.Text = "Dígitos";
            // 
            // txtInformacao
            // 
            this.txtInformacao.BackColor = System.Drawing.SystemColors.Info;
            this.txtInformacao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInformacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInformacao.Location = new System.Drawing.Point(181, 9);
            this.txtInformacao.MaxLength = 1;
            this.txtInformacao.Name = "txtInformacao";
            this.txtInformacao.Size = new System.Drawing.Size(25, 20);
            this.txtInformacao.TabIndex = 2;
            this.txtInformacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Image = ((System.Drawing.Image)(resources.GetObject("btnConfirmar.Image")));
            this.btnConfirmar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfirmar.Location = new System.Drawing.Point(454, 216);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(98, 25);
            this.btnConfirmar.TabIndex = 23;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.Location = new System.Drawing.Point(350, 216);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(98, 25);
            this.btnCancelar.TabIndex = 22;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmFormatoCartao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 246);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.grbFormatoCartao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmFormatoCartao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formato do Cartão";
            this.grbFormatoCartao.ResumeLayout(false);
            this.grbFormatoCartao.PerformLayout();
            this.pnlInformacaoFixa.ResumeLayout(false);
            this.pnlInformacaoFixa.PerformLayout();
            this.pnlChecagem.ResumeLayout(false);
            this.pnlChecagem.PerformLayout();
            this.pnlNumeroVia.ResumeLayout(false);
            this.pnlNumeroVia.PerformLayout();
            this.pnlIgnorarDigito.ResumeLayout(false);
            this.pnlIgnorarDigito.PerformLayout();
            this.pnlInformacaoOpcional.ResumeLayout(false);
            this.pnlInformacaoOpcional.PerformLayout();
            this.pnlInformacao.ResumeLayout(false);
            this.pnlInformacao.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbFormatoCartao;
        private System.Windows.Forms.Panel pnlInformacao;
        private System.Windows.Forms.Label lblInformacao;
        private System.Windows.Forms.TextBox txtInformacao;
        private System.Windows.Forms.Button btnInformacao;
        private System.Windows.Forms.Panel pnlInformacaoFixa;
        private System.Windows.Forms.Button btnInformacaoFixa;
        private System.Windows.Forms.TextBox txtInformacaoFixa;
        private System.Windows.Forms.Panel pnlChecagem;
        private System.Windows.Forms.Button btnChecagem;
        private System.Windows.Forms.Label lblChecagem;
        private System.Windows.Forms.TextBox txtChecagem;
        private System.Windows.Forms.Panel pnlNumeroVia;
        private System.Windows.Forms.Button btnNumeroVia;
        private System.Windows.Forms.Label lblNumeroVia;
        private System.Windows.Forms.TextBox txtNumeroVia;
        private System.Windows.Forms.Panel pnlIgnorarDigito;
        private System.Windows.Forms.Button btnIgnorarDigito;
        private System.Windows.Forms.Label lblIgnorarDigito;
        private System.Windows.Forms.TextBox txtIgnorarDigito;
        private System.Windows.Forms.Panel pnlInformacaoOpcional;
        private System.Windows.Forms.Button btnInformacaoOpcional;
        private System.Windows.Forms.Label lblInformacaoOpcional;
        private System.Windows.Forms.TextBox txtInformacaoOpcional;
        private System.Windows.Forms.Button btnApagaFormatoCartao;
        private System.Windows.Forms.TextBox txtFormatoCartao;
        private System.Windows.Forms.Label lblFormatoCartao;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
    }
}