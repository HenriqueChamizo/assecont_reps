using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Create Standalone SDK class dynamicly.
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

        /********************************************************************************************************************************************
        * Before you refer to this demo,we strongly suggest you read the development manual deeply first.                                           *
        * This part is for demonstrating the communication with your device.There are 3 communication ways: "TCP/IP","Serial Port" and "USB Client".*
        * The communication way which you can use duing to the model of the device.                                                                 *
        * *******************************************************************************************************************************************/
        #region Communication
        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber = 1;
        //the serial number of the device.After connecting the device ,this value will be changed.

        //If your device supports the TCP/IP communications, you can refer to this.
        //when you are using the tcp/ip communication,you can distinguish different devices by their IP address.

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtIP.Text.Trim() == "" || txtPort.Text.Trim() == "")
            {
                MessageBox.Show("IP and Port cannot be null", "Error");
                return;
            }
            if (txtOCPF.Text.Trim() == "")
            {
                MessageBox.Show("Please input CPF Firstly", "Error");
                return;
            
            }
            int idwErrorCode = 0;
            if (txtkey.Text.Trim() != "")
            {
                int ComKey = Convert.ToInt32(txtkey.Text.Trim());
                axCZKEM1.SetCommPassword(ComKey);
            
            }
            if (txtAes.Text.Trim() != "")
            {
              //  int ComKey = Convert.ToInt32(txtkey.Text.Trim());
               // axCZKEM1.SetAesPassword(txtAes.Text.Trim());
                axCZKEM1.SetAesPassword("trix2016");

            }

            Cursor = Cursors.WaitCursor;   
            if (btnConnect.Text == "DisConnect")
            {
                axCZKEM1.Disconnect();
                bIsConnected = false;
                btnConnect.Text = "Connect";
                lblState.Text = "Current State:DisConnected";
                Cursor = Cursors.Default;
                return;
            }

            bIsConnected = axCZKEM1.Connect_Net(txtIP.Text, Convert.ToInt32(txtPort.Text));
            if (bIsConnected == true)
            {
                btnConnect.Text = "DisConnect";
                btnConnect.Refresh();
                lblState.Text = "Current State:Connected";
                iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                if (axCZKEM1.SetOprateCPF(iMachineNumber, txtOCPF.Text))
                {
                    ;
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    MessageBox.Show("Wrong CPF,you should disconnect firstly and input a correct CPF,=" + idwErrorCode.ToString(), "Error");
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Unable to connect the device,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        #endregion



        private void btnGetalluser_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            string PIN2 = "";
            string Name = "";
            string Pwd = "";
            int Privilege = 0;
            string PIS = "";
            string CPF = "";
            string card = "";
            int iIndex = lvUdata.Items.Count;
            int idwErrorCode = -9;
  

            Cursor = Cursors.WaitCursor;
           // lvUdata.Items.Clear();
            axCZKEM1.EnableDevice(iMachineNumber, false);

            while (axCZKEM1.SSR_GetAllUserInfoEx_BZ900(iMachineNumber, out PIN2, out Name, out Pwd, out Privilege, out PIS, out CPF,out card))
            {

                lvUdata.Items.Add(PIN2);
                lvUdata.Items[iIndex].SubItems.Add(Name);
                lvUdata.Items[iIndex].SubItems.Add(Pwd);
                lvUdata.Items[iIndex].SubItems.Add(Privilege.ToString());
                lvUdata.Items[iIndex].SubItems.Add(PIS);
                lvUdata.Items[iIndex].SubItems.Add(CPF);
                lvUdata.Items[iIndex].SubItems.Add(card);
                iIndex++;
            }
            Cursor = Cursors.Default;
            axCZKEM1.GetLastError(ref idwErrorCode);

            if (idwErrorCode != 0)
            {
                MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
            }
            else if (idwErrorCode == 0)
            {
                MessageBox.Show("No data from terminal returns!", "Error");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            Cursor = Cursors.Default;
        }

        private void btnSetuserinfo_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            int idwErrorCode = -9;
            string PIN2 = "";
            string Name = "";
            string Pwd = "";
            int Privilege = 0;
            string PIS = "";
            string CPF = "";
            string card = "";

            PIN2 = txtID.Text.Trim();
            Name = txtUname.Text.Trim();
            Pwd = txtPwd.Text.Trim();
            Privilege = Convert.ToInt32(cmbPri.Text.Trim());
            if (Privilege == 1)
                Privilege = 14;
            PIS = txtPIS.Text.Trim();
            CPF = txtCPF.Text.Trim();
            card = txtCard.Text.Trim();
            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (txtCard.Text.Trim() == "")
                card = null;
            if (axCZKEM1.SSR_SetUserInfoEx_BZ900(iMachineNumber,PIN2,Name,Pwd,Privilege,PIS,CPF,card))
            {
                MessageBox.Show("Set user info successfully!");            
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                if (idwErrorCode == -5)
                {
                    MessageBox.Show("some charater is unreadable,");
                }
                else if (idwErrorCode == -3)
                {
                    MessageBox.Show("Name field is empty,");
                }
                else
                    MessageBox.Show("Set employee failed," + idwErrorCode.ToString(), "Error");
            }
            axCZKEM1.EnableDevice(iMachineNumber,true);
            Cursor = Cursors.Default;
        }

        private void btnSetEmployer_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            string name = txtName.Text.Trim();
            int type = 0;
            int idwErrorCode = -9;
            if (CNPJ.Checked)
            {
                type = 1;
            }
            else if (CPF.Checked)
            {
                type = 2;
            }
            if (type == 0)
            {
                MessageBox.Show("You must choose CNPJ or CPF!", "Error");
                return;
            }
            string cnpj_cpf = txtCNPJ_CPF.Text.Trim();
            string cei = txtCEI.Text.Trim();
            string addr = txtAddr.Text.Trim();
            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.SetEmployer(iMachineNumber, name, type, cnpj_cpf, cei, addr))
            {
                MessageBox.Show("Set Employer info successfully!");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                if (idwErrorCode == -5)
                {
                    MessageBox.Show("some charater is unreadable,");
                }
                else if (idwErrorCode == -3)
                {
                    MessageBox.Show("Name and Address field is empty,");
                }
                else
                    MessageBox.Show("Set employer failed,some charater is unreadable or blank fied" + idwErrorCode.ToString(), "Error");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
        }

        private void btnGetEmployer_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            string name = "";
            string type = "";
            string cnpj_cpf = "";
            string cei = "";
            string addr = "";
            int optype = -1;
            int idwErrorCode = -9;
            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.GetEmployer(iMachineNumber, out name, out type, out cnpj_cpf, out cei, out addr))
            {
                txtName.Text = name;
                optype = Convert.ToInt32(type);
                if (optype == 1)
                {
                    CNPJ.Checked = true;
                }
                else if (optype == 2)
                {
                    CPF.Checked = true;
                }
                txtCNPJ_CPF.Text = cnpj_cpf;
                txtCEI.Text = cei;
                txtAddr.Text = addr;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Get employer failed," +idwErrorCode.ToString(), "Error");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;

        }

        private void btnGetDevLog_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            string nsr = "";
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int min = 0;
            int sec = 0;
            int type = 0;
            int iIndex = 0;
            int idwErrorCode = -9;
      
            Cursor = Cursors.WaitCursor;
            lvDev.Items.Clear();
            axCZKEM1.EnableDevice(iMachineNumber,false);
            while (axCZKEM1.GetDevOpLog_BZ900(iMachineNumber,out nsr,out year,out month,out day,out hour,out min,out sec,out type))
            {
                lvDev.Items.Add(nsr);
                lvDev.Items[iIndex].SubItems.Add(year.ToString() + "-" + month.ToString() + "-" + day.ToString() + " " + hour.ToString() + ":" + min.ToString() + ":" + sec.ToString());
                lvDev.Items[iIndex].SubItems.Add(type.ToString());
                lvDev.EnsureVisible(iIndex);
                lvDev.Refresh();
                if (iIndex % 50 == 0)
                    System.Threading.Thread.Sleep(200);
                iIndex++;

            }
            Cursor = Cursors.Default;
            axCZKEM1.GetLastError(ref idwErrorCode);

            if (idwErrorCode != 0)
            {
                MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
            }
            else
            {
                MessageBox.Show("No data from terminal returns or All data have downlod!", "Error");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;

        }

        private void btnGetAttLog_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            string nsr = "";
            string pis = "";
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int min = 0;
            int sec = 0;
            int iIndex = 0;
            int idwErrorCode = -9;

            Cursor = Cursors.WaitCursor;
            lvAtt.Items.Clear();
            axCZKEM1.EnableDevice(iMachineNumber, false);
            while (axCZKEM1.GetAttLogs_BZ900(iMachineNumber, out nsr,out pis, out year, out month, out day, out hour, out min, out sec))
            {
                lvAtt.Items.Add(nsr);
                lvAtt.Items[iIndex].SubItems.Add(pis);
                lvAtt.Items[iIndex].SubItems.Add(year.ToString() + "-" + month.ToString() + "-" + day.ToString() + " " + hour.ToString() + ":" + min.ToString() + ":" + sec.ToString());
                lvAtt.EnsureVisible(iIndex);
                lvAtt.Refresh();
                if (iIndex % 50 == 0)
                    System.Threading.Thread.Sleep(200);
                iIndex++;

            }
            Cursor = Cursors.Default;
            axCZKEM1.GetLastError(ref idwErrorCode);

            if (idwErrorCode != 0)
            {
                MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
            }
            else
            {
                MessageBox.Show("No data from terminal returns or All data have downlod!", "Error");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
        }

        private void btnGetTimeLog_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            string nsr = "";
            string oldtime = "";
            string newtime = "";
            string opcpf = "";

            int iIndex = 0;
            int idwErrorCode = -9;

            Cursor = Cursors.WaitCursor;
            lvTime.Items.Clear();
            axCZKEM1.EnableDevice(iMachineNumber, false);
            while (axCZKEM1.GetDatetimeOpLog_BZ900(iMachineNumber, out nsr,out oldtime,out newtime,out opcpf))
            {
                lvTime.Items.Add(nsr);
                lvTime.Items[iIndex].SubItems.Add(oldtime);
                lvTime.Items[iIndex].SubItems.Add(newtime);
                lvTime.Items[iIndex].SubItems.Add(opcpf);
                lvTime.EnsureVisible(iIndex);
                lvTime.Refresh();
                if (iIndex % 50 == 0)
                    System.Threading.Thread.Sleep(200);
                iIndex++;

            }
            Cursor = Cursors.Default;
            axCZKEM1.GetLastError(ref idwErrorCode);

            if (idwErrorCode != 0)
            {
                MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
            }
            else
            {
                MessageBox.Show("No data from terminal returns or All data have downlod!", "Error");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
        }

        bool runiing = false;
        int count = 0;
        private void btnGetUserLog_Click(object sender, EventArgs e)
        {
            runiing = true;
            count = 0;
            while (runiing) {
                getlogs(sender, e);
                count++;
                Refresh();
                runiing = false;
            }
        }

        private void getlogs(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            string nsr = "";
            string pis = "";
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int min = 0;
            int sec = 0;
            string optype = "";
            string cpf = "";
            string name = "";
            int mark = 0;

            int iIndex = 0;
            int idwErrorCode = -9;

            Cursor = Cursors.WaitCursor;
            lvUsr.Items.Clear();
            //axCZKEM1.EnableDevice(iMachineNumber, false);
            while (axCZKEM1.GetEmployeeOpLog_BZ900(iMachineNumber, out nsr,out year, out month, out day, out hour, out min, out sec,out optype,out pis,out cpf,out name,out mark))
            {
                lvUsr.Items.Add(nsr);
                lvUsr.Items[iIndex].SubItems.Add(year.ToString() + "-" + month.ToString() + "-" + day.ToString() + " " + hour.ToString() + ":" + min.ToString() + ":" + sec.ToString());
                lvUsr.Items[iIndex].SubItems.Add(optype);
                lvUsr.Items[iIndex].SubItems.Add(pis);
                lvUsr.Items[iIndex].SubItems.Add(cpf);
                lvUsr.Items[iIndex].SubItems.Add(name);
                lvUsr.Items[iIndex].SubItems.Add(mark.ToString());
                lvUsr.EnsureVisible(iIndex);
                lvUsr.Refresh();
                if (iIndex % 50 == 0)
                    System.Threading.Thread.Sleep(200);
                iIndex++;

            }
            Cursor = Cursors.Default;
            axCZKEM1.GetLastError(ref idwErrorCode);

            if (idwErrorCode != 0)
            {
                runiing = false;  
                MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
                        
            }
            else
            {
                //MessageBox.Show("No data from terminal returns or All data have downlod!", "Error"); 
            }

            //axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
            System.Threading.Thread.Sleep(1000);
        }

        private void btnGetEmrLog_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            string nsr = "";
            string cei = "";
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int min = 0;
            int sec = 0;
            string cnpj_cpf = "";
            string opcpf = "";
            string name = "";
            string addr = "";
            int optype = 0;

            int iIndex = 0;
            int idwErrorCode = -9;

            Cursor = Cursors.WaitCursor;
            lvEmr.Items.Clear();
            axCZKEM1.EnableDevice(iMachineNumber, false);
            while (axCZKEM1.GetEmployerOpLog_BZ900(iMachineNumber,out nsr,out year,out month,out day,out hour,out min,out sec,out cnpj_cpf,out opcpf,out cei,out name,out addr,out optype))
            {
                lvEmr.Items.Add(nsr);
                lvEmr.Items[iIndex].SubItems.Add(year.ToString() + "-" + month.ToString() + "-" + day.ToString() + " " + hour.ToString() + ":" + min.ToString() + ":" + sec.ToString());
                lvEmr.Items[iIndex].SubItems.Add(cnpj_cpf);
                lvEmr.Items[iIndex].SubItems.Add(opcpf);
                lvEmr.Items[iIndex].SubItems.Add(name);
                lvEmr.Items[iIndex].SubItems.Add(addr);
                lvEmr.Items[iIndex].SubItems.Add(cei);      
                lvEmr.Items[iIndex].SubItems.Add(optype.ToString());
                lvEmr.EnsureVisible(iIndex);
                lvEmr.Refresh();
                if (iIndex % 50 == 0)
                    System.Threading.Thread.Sleep(200);
                iIndex++;

            }
            Cursor = Cursors.Default;
            axCZKEM1.GetLastError(ref idwErrorCode);

            if (idwErrorCode != 1)
            {
                MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
            }
            else
            {
                MessageBox.Show("No data from terminal returns or All data have downlod!", "Error");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
        }

        private void btnSetpara_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            string name = "";
            string value = "";
            if (txtParaname.Text.Trim() == "" || txtValue.Text.Trim() == "")
            {
                MessageBox.Show("Para name and Value cannot be null", "Error");
                return;
            }
            name = txtParaname.Text.Trim();
            value = txtValue.Text.Trim();
            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.SetSysOption(iMachineNumber, name, value))
            {
                MessageBox.Show("Set Optionpara successfully!");
            }
            else
            {
                MessageBox.Show("Set Optionpara fail!");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;

        }

        private void btnGetpara_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            string name = "";
            string value = "";
            if (txtParaname.Text.Trim() == "")
            {
                MessageBox.Show("Para name cannot be null", "Error");
                return;
            }
            name = txtParaname.Text.Trim();
            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.GetSysOption(iMachineNumber,name,out value))
            {
                MessageBox.Show("Get Optionpara successfully!");
            }
            else
            {
                MessageBox.Show("Get Optionpara fail!");
            }
            txtValue.Text = value;
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;

        }

        private void btnSetTime_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }
            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.SetDeviceTime(iMachineNumber))
            {
                MessageBox.Show("Set device time successfully!");
            }
            else
            {
                MessageBox.Show("Set device time fail!");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;
        }

        private void btnGetUnlockPWD_Click(object sender, EventArgs e)
        {
            if (txtUnlockpwd.Text.Trim() == "")
            {
                MessageBox.Show("serial number cannot be null", "Error");
                return;
            }
            string pwd = "";
            string serialnum = txtUnlockpwd.Text.Trim();
            if (axCZKEM1.GetUnlockPwd(serialnum,out pwd))
            {
                MessageBox.Show("Set device time successfully!");
            }
            txtUnlockpwd.Text = pwd;
        }

        private void btnSetIndex_Click(object sender, EventArgs e)
        {
            if (txtIndex.Text.Trim() == "")
            {
                MessageBox.Show("NSR number cannot be null", "Error");
                return;
            }
            int nsr = Convert.ToInt32(txtIndex.Text.Trim());
            if (axCZKEM1.SetSeekPosition(iMachineNumber, nsr))
            {
                MessageBox.Show("Set seek position successfully!");
            
            }
        }

        private void btnUpdatatmp_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (lvTemp.Items.Count == 0)
            {
                MessageBox.Show("There is no data to upload!", "Error");
                return;
            }

            int idwErrorCode = 0;

            string sdwEnrollNumber = "";
            int idwFingerIndex = 0;
            string sTmpData = "";
            int iFlag = 1;
            int iUpdateFlag = 1;


            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber,false);
            for (int i = 0; i < lvTemp.Items.Count; i++)
            {
                sdwEnrollNumber = lvTemp.Items[i].SubItems[0].Text;
                idwFingerIndex = Convert.ToInt32(lvTemp.Items[i].SubItems[1].Text);
                sTmpData = lvTemp.Items[i].SubItems[2].Text;
                iFlag = Convert.ToInt32(lvTemp.Items[i].SubItems[3].Text);
                axCZKEM1.SetUserTmpExStr_BZ900(iMachineNumber,sdwEnrollNumber,idwFingerIndex,iFlag,sTmpData);
            }
            Cursor = Cursors.Default;
            axCZKEM1.EnableDevice(iMachineNumber,true);
            MessageBox.Show("Successfully upload fingerprint templates, " + "total:" + lvTemp.Items.Count.ToString(), "Success");

        }

        private void btnDelUserDataEx_Click(object sender, EventArgs e)
        {
             if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (txtUserPIN2.Text.Trim() == "" || txtBackNumber.Text.Trim() == "")
            {
                MessageBox.Show("Please input the UserID and BackupNumber first!", "Error");
                return;
            }
            int idwErrorCode = 0;

            string sUserID = txtUserPIN2.Text.Trim();
            int iBackupNumber = Convert.ToInt32(txtBackNumber.Text.Trim());

            Cursor = Cursors.WaitCursor;
            if (axCZKEM1.SSR_DeleteEnrollDataExt_BZ900(iMachineNumber,sUserID,iBackupNumber))
            {
                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
                MessageBox.Show("DeleteEnrollData,UserID=" + sUserID + " BackupNumber=" + iBackupNumber.ToString(), "Success");
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            }
            Cursor = Cursors.Default;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (txtpath.Text.Trim() == "")
            {
                MessageBox.Show("Please input file path!", "Error");
                return;
            }
            string pathname = "";
            pathname = txtpath.Text.Trim();
            Cursor = Cursors.WaitCursor;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            if (axCZKEM1.UpdateZKOS(iMachineNumber, pathname))
            {
                MessageBox.Show("Update the ZKOS successfully!");

            }
            else
            {
                MessageBox.Show("Update the ZKOS failed!");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;



            

        }

        private void btngetfinger_Click(object sender, EventArgs e)
        {

            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            if (txtUserPIN2.Text.Trim() == "" || txtBackNumber.Text.Trim() == "")
            {
                MessageBox.Show("Please input the UserID and BackupNumber first!", "Error");
                return;
            }

            int flag = 0;
            string sTmpData = "";
            int iTmpLength = 0;
            int Tindex = lvTemp.Items.Count;
            string PIN2 = txtUserPIN2.Text.Trim();
            int idwFingerIndex = Convert.ToInt32(txtBackNumber.Text.Trim());
            int idwErrorCode = 0;

            //lvTemp.Items.Clear();
            Cursor = Cursors.WaitCursor;
            if (idwFingerIndex == 10)
            {
                for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {
                    if (axCZKEM1.GetUserTmpExStr_BZ900(iMachineNumber, PIN2, idwFingerIndex, out flag, out sTmpData, out iTmpLength))
                    {
                        lvTemp.Items.Add(PIN2);
                        lvTemp.Items[Tindex].SubItems.Add(idwFingerIndex.ToString());
                        lvTemp.Items[Tindex].SubItems.Add(sTmpData);
                        lvTemp.Items[Tindex].SubItems.Add(flag.ToString());
                        Tindex++;
                    }

                }
            }
            else if (0 <= idwFingerIndex && idwFingerIndex < 10)
            {
                axCZKEM1.GetUserTmpExStr_BZ900(iMachineNumber, PIN2, idwFingerIndex, out flag, out sTmpData, out iTmpLength);
                lvTemp.Items.Add(PIN2);
                lvTemp.Items[Tindex].SubItems.Add(idwFingerIndex.ToString());
                lvTemp.Items[Tindex].SubItems.Add(sTmpData);
                lvTemp.Items[Tindex].SubItems.Add(flag.ToString());
            }
            axCZKEM1.GetLastError(ref idwErrorCode);
            if (idwErrorCode != 1)
            {
                MessageBox.Show("Reading Finger from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
            }
            else
            {
                MessageBox.Show("Reading successfully");
            }
            Cursor = Cursors.Default;
        }

        private void btnclearadmin_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Please connect the device first!", "Error");
                return;
            }

            Cursor = Cursors.WaitCursor;

            if (axCZKEM1.ClearAdministrators(iMachineNumber))
            {
                MessageBox.Show("Clear Admin successfully!");

            }
            else
            {
                MessageBox.Show("Clear Admin failed!");
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
            Cursor = Cursors.Default;

        }

        private void lvUdata_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtIndex_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int punches_count = 0;
            int val = 0;

            //device OPlogs
            if (axCZKEM1.GetMRPTotal(iMachineNumber, 31, ref val))
            {
                punches_count += val;
            }
            //employee logs
            if (axCZKEM1.GetMRPTotal(iMachineNumber, 32, ref val))
            {
                punches_count += val;
            }
            //employer logs
            if (axCZKEM1.GetMRPTotal(iMachineNumber, 34, ref val))
            {
                punches_count += val;
            }
            //OpTimelogs
            if (axCZKEM1.GetMRPTotal(iMachineNumber, 35, ref val))
            {
               punches_count += val;
            }
            //Attlog
            if (axCZKEM1.GetMRPTotal(iMachineNumber, 36, ref val))
            {
                punches_count += val;
            }

            textBox1.Text = "" + punches_count;
        }
    }
}
