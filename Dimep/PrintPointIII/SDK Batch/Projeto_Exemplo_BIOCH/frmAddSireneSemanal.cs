using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExemploBioLite
{
    public partial class frmAddSireneSemanal : Form
    {
        public string diaDaSemana = "";
        public int hora = 0;
        public int minuto = 0;

        public frmAddSireneSemanal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.diaDaSemana = cmbDiaSemana.Text;
            this.hora = Int32.Parse(txthora.Text);
            this.minuto = Int32.Parse(txtMinuto.Text);

            this.Close();
        }
    }
}
