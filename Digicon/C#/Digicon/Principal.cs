using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Digicon
{
    public partial class Principal : AssepontoRep.Principal
    {
        Bridge bridge;

        public Principal()
        {
            InitializeComponent();
            enviarConfiguraçãoToolStripMenuItem.Visible = false;
            bridge = new Bridge(edLog);
            assignBridge(bridge);
            timer1.Enabled = true;
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            bridge.StopDFS();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            bridge.StartServer();
        }
    }
}
