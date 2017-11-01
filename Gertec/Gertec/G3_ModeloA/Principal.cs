using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace G3_ModeloA
{
    public partial class Principal : AssepontoRep.Principal
    {
        Bridge bridge;

        public Principal()
        {
            InitializeComponent();
        }

        public override void InicializarObjetos()
        {
            base.InicializarObjetos();
            bridge = new Bridge(edLog, this);
            assignBridge(bridge);
        }

        //public override void RepEdicaoClosed(int Terminal)
        //{
        //}
    }
}
