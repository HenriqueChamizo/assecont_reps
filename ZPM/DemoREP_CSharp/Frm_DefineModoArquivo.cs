using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoREP_CSharp
{
    public partial class Frm_DefineModoArquivo : Form
    {
        public Frm_DefineModoArquivo()
        {
            InitializeComponent();
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Confirmar_Click(object sender, EventArgs e)
        {
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_DefineModoArquivo(REPZPM_DLL.Handle, Txt_Unidade.Text + ":");

            if (REPZPM_DLL.Retorno == 1)
            {
                MessageBox.Show("Modo Arquivo definido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                REPZPM_DLL.Modo = 1;
                this.Close();
            }
            else
            {
                REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.Retorno);
                this.Close();
            }

        }
    }
}
