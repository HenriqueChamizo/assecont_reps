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
    public partial class Frm_ConfiguraCodigoBarras : Form
    {
        public Frm_ConfiguraCodigoBarras()
        {
            InitializeComponent();
        }

        private void Btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Confirmar_Click(object sender, EventArgs e)
        {
            /*Seta o tamanho do código de barras*/
            REPZPM_DLL.DLLREP_ConfiguraCodigoBarra(Convert.ToInt16(Txt_Tamanho.Text));

            /*realiza a leitura do código de barras setado*/
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_LeCodigoBarra(REPZPM_DLL.Handle);    /*EXISTE UM ERRO NESTA LINHA DO CÓDIGO*/

            if ((Convert.ToInt16(Txt_Tamanho.Text)) == (Convert.ToInt16(REPZPM_DLL.Retorno)))
            {
                MessageBox.Show("Código de barras configurado com sucesso!","Sucesso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro ao configurar o código de barras!","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
