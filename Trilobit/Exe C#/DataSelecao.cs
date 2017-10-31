using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trilobit
{
    public partial class DataSelecao : Form
    {
        public DataSelecao()
        {
            InitializeComponent();
        }

        private void cbImportarTudo_CheckedChanged(object sender, EventArgs e)
        {
            edDataInicial.Enabled = !cbImportarTudo.Checked;
            edDataFinal.Enabled = !cbImportarTudo.Checked;
        }
    }
}
