using AssepontoRep;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BioproxC
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
    }
}
