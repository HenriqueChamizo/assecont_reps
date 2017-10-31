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
    public partial class Frm_IniciaDriver : Form
    {
        public Frm_IniciaDriver()
        {
            InitializeComponent();
        }

        private void Btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Confirmar_Click(object sender, EventArgs e)
        {
                                      
            REPZPM_DLL.Handle = REPZPM_DLL.DLLREP_IniciaDriver(Txt_NumFabricacao.Text);

            if (REPZPM_DLL.Handle == -1)
            {
                MessageBox.Show("Erro na inicialização do Driver!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                MessageBox.Show("Sucesso na inicialização do Driver!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            

        }
    }
}
