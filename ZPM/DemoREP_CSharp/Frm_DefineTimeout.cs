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
    public partial class Frm_DefineTimeout : Form
    {
        public Frm_DefineTimeout()
        {
            InitializeComponent();
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Confirmar_Click(object sender, EventArgs e)
        {
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_DefineTimeout(REPZPM_DLL.Handle, Convert.ToInt16(Txt_Timeout.Text));

            if (REPZPM_DLL.Retorno == 1)
            {
                MessageBox.Show("Novo Timeout definido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Erro na definição do Timeout!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
    }
}
