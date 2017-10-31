using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IdData
{
    public partial class HorarioDeVerao : Form
    {
        public string Inicio;
        public string Fim;

        public HorarioDeVerao()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void btEnviar_Click(object sender, EventArgs e)
        {
            Inicio = maskedTextBoxInicio.Text;
            Fim = maskedTextBoxFim.Text;
            Close();
        }
    }
}
