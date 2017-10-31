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
    public partial class Frm_DefineModoIP : Form
    {
        public Frm_DefineModoIP()
        {
            InitializeComponent();
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Confirmar_Click(object sender, EventArgs e)
        {
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_DefineModoIP(REPZPM_DLL.Handle, Txt_IP.Text, 5000);

            if (REPZPM_DLL.Retorno == 1)
            {
                REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.Retorno);
                REPZPM_DLL.Modo = 0;
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
