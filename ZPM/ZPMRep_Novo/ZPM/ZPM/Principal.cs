using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Wr.Classes;
using DemoREP_CSharp;

namespace ZPM
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
