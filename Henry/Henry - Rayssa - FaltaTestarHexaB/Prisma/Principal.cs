using AssepontoRep;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Prisma
{
    public partial class Principal : AssepontoRep.Principal
    {
        Bridge bridge;

        public Principal()
        {
            InitializeComponent();
            bridge = new Bridge(edLog);
            assignBridge(bridge);
        }

        private void Principal_Load(object sender, EventArgs e)
        {

        }
    }
}