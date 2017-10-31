using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Zpm
{
    public partial class ImportacaoMarcacoes : Form
    {
        public ImportacaoMarcacoes()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Graphics Canvas;

            Canvas = ((Panel)sender).CreateGraphics();

            Pen myPen = new Pen(ColorTranslator.FromHtml("#E0E0E0"), 1);

            Canvas.DrawLine(myPen, 0, 1, ((Panel)sender).Width, 0);
        }
    }
}
