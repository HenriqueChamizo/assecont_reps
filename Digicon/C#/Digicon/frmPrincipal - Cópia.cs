using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DigiconFrameworkServer.Control;

namespace Digicon
{
    public partial class frmPrincipal : AssepontoRep.Principal
    {
        public frmPrincipal(Bridge bridge)
            : base(bridge)
        {
            InitializeComponent();
            //base.initializeBridge(bridge);
            //MessageManager.MessageReceivedChanged += new MessageReceivedHandler(MenssageAccessReceive);
            //MessageManagerClock.MessageReceivedChangedClock += new MessageReceivedHandlerClock(MenssageClockReceive);
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
