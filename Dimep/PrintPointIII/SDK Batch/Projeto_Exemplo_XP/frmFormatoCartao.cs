using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExemploXP
{
    public partial class frmFormatoCartao : Form
    {
        private String _formatoCartao;

        public String FormatoCartao
        {
            get { return _formatoCartao; }            
        }

        public frmFormatoCartao(String formatoCartao)
        {
            InitializeComponent();

            AddHandlers();

            this._formatoCartao = formatoCartao;
            this.txtFormatoCartao.Text = formatoCartao;
        }

        private void AddHandlers()
        {        
            this.txtInformacao.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtIgnorarDigito.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtChecagem.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtInformacaoOpcional.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtNumeroVia.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtInformacaoFixa.KeyPress += new KeyPressEventHandler(ApenasNumeros);
        }

        private void ApenasNumeros(object sender, KeyPressEventArgs e)
        {
            if (!((Encoding.Default.GetBytes(e.KeyChar.ToString())[0] >= (int)Keys.D0) &&
                (Encoding.Default.GetBytes(e.KeyChar.ToString())[0] <= (int)Keys.D9) ||
                (Encoding.Default.GetBytes(e.KeyChar.ToString())[0] == (int)Keys.Back)))
            {
                e.Handled = true;
                e.KeyChar = (Char)Keys.Cancel;                
            }            
        }

        private void btnApagaFormatoCartao_Click(object sender, EventArgs e)
        {
            txtFormatoCartao.Text = "";
        }        

        private void btnInformacao_Click(object sender, EventArgs e)
        {
            if (this.txtInformacao.Text != "")
                IncrementaFormatoCartao("I".PadLeft(int.Parse(this.txtInformacao.Text), Char.Parse("I")));
        }

        private void btnIgnorarDigito_Click(object sender, EventArgs e)
        {
            if (this.txtIgnorarDigito.Text != "")
                IncrementaFormatoCartao("X".PadLeft(int.Parse(this.txtIgnorarDigito.Text), Char.Parse("X")));
        }

        private void btnChecagem_Click(object sender, EventArgs e)
        {
            if (this.txtChecagem.Text != "")
                IncrementaFormatoCartao("C".PadLeft(int.Parse(this.txtChecagem.Text), Char.Parse("C")));
        }

        private void btnInformacaoOpcional_Click(object sender, EventArgs e)
        {
            if (this.txtInformacaoOpcional.Text != "")
                IncrementaFormatoCartao("O".PadLeft(int.Parse(this.txtInformacaoOpcional.Text), Char.Parse("O")));
        }

        private void btnNumeroVia_Click(object sender, EventArgs e)
        {
            if (this.txtNumeroVia.Text != "")
                IncrementaFormatoCartao("V".PadLeft(int.Parse(this.txtNumeroVia.Text), Char.Parse("V")));
        }

        private void btnInformacaoFixa_Click(object sender, EventArgs e)
        {
            if (this.txtInformacaoFixa.Text != "")
                IncrementaFormatoCartao(this.txtInformacaoFixa.Text);
        }

        private void IncrementaFormatoCartao(String texto)
        {
            if (this.txtFormatoCartao.Text.Length + texto.Length <= 20)
                this.txtFormatoCartao.Text += texto;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            this._formatoCartao = this.txtFormatoCartao.Text;

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}
