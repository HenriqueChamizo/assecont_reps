using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using IDSysR30_CSharp.Controller;

namespace IDSysR30_CSharp.Forms
{
    public partial class frmGeneric : Form
    {
        private CController pController;

        public frmGeneric(CController pController)
        {
            InitializeComponent();

            this.pController = pController;
        }

        ~frmGeneric()
        {
            this.pController = null;
        }

        private void frmGeneric_Load(object sender, EventArgs e)
        {
            this.Text = this.pController.GetCommand().ToString();

            this.Refresh_lstEvent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Refresh_lstEvent()
        {
            List<string> lstrEventData = new List<string>();
            this.lstEvent.Items.Clear();

            lstrEventData = this.pController.GetEventData();

            if (lstrEventData == null)
            {
                throw new Exception("Evento não encontrado.");
            }

            for (int iIdx = 0; iIdx < lstrEventData.Count; iIdx++)
            {
                this.lstEvent.Items.Add(lstrEventData[iIdx].ToString());
            }

            lstrEventData = null;
        }

    }
}
