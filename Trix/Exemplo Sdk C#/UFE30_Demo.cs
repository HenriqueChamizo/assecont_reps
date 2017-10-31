using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Suprema;

namespace Suprema {
	public partial class UFE30_Demo : Form {
		//=========================================================================//
		UFScannerManager m_ScannerManager;
		UFMatcher m_Matcher;
		string m_strError;
		byte[][] m_template;
		int[] m_template_size;
		int m_template_num;
		//
		const int MAX_TEMPLATE_SIZE = 512;
		const int MAX_TEMPLATE_NUM	= 50;

		public UFE30_Demo() {
			InitializeComponent();
		}

		private void UFE30_Demo_Load(object sender, EventArgs e) {
			int i;

			// Set initial values
			cbEnrollQuality.SelectedIndex = 2;
			cbSecurityLevel.SelectedIndex = 3;
            cbScanTemplateType.SelectedIndex = 1;
            cbMatchTemplateType.SelectedIndex = 1;

			m_template = new byte[MAX_TEMPLATE_NUM][];
			for (i = 0; i < MAX_TEMPLATE_NUM; i++) {
				m_template[i] = new byte[MAX_TEMPLATE_SIZE];
			}
			m_template_size = new int[MAX_TEMPLATE_NUM];
			m_template_num = 0;

			m_ScannerManager = new UFScannerManager(this);
		}

		private void UFE30_Demo_FormClosing(object sender, FormClosingEventArgs e) {
			btnUninit_Click(sender, e);
		}
		//=========================================================================//

		//=========================================================================//
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new UFE30_Demo());
		}
		//=========================================================================//

		//=========================================================================//
		private void GetScannerTypeString(UFS_SCANNER_TYPE ScannerType, out string strScannerType)
		{
			if (ScannerType == UFS_SCANNER_TYPE.SFR200) {
				strScannerType = "SFR200";
			} else if (ScannerType == UFS_SCANNER_TYPE.SFR300) {
				strScannerType = "SFR300";
			} else if (ScannerType == UFS_SCANNER_TYPE.SFR300v2) {
				strScannerType = "SFR300v2";
			} else {
				strScannerType = "Error";
			}
		}

        private bool GetGetCurrentScanner(out UFScanner Scanner)
        {
			Scanner = m_ScannerManager.Scanners[lbScannerList.SelectedIndex];
			if (Scanner != null) {
				return true;
			} else {
				tbxMessage.AppendText("Selected Scanner is not connected\r\n");
				return false;
			}
        }

		private void GetCurrentScannerSettings()
		{
			UFScanner Scanner;
			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}
			
			// Unit of timeout is millisecond
			cbTimeout.SelectedIndex = Scanner.Timeout / 1000;

			nudBrightness.Minimum = 0;
			nudBrightness.Maximum = 255;
			nudBrightness.Value = Scanner.Brightness;

			nudSensitivity.Minimum = 0;
			nudSensitivity.Maximum = 7;
			nudSensitivity.Value = Scanner.Sensitivity;

			cbDetectCore.Checked = Scanner.DetectCore;
		}

		private void GetMatcherSettings(UFMatcher Matcher)
		{
			// Security level ranges from 1 to 7
			cbSecurityLevel.SelectedIndex = Matcher.SecurityLevel - 1;

			cbFastMode.Checked = Matcher.FastMode;
		}

		private void DrawCapturedImage(UFScanner Scanner)
		{
			Graphics g = pbImageFrame.CreateGraphics();
			Rectangle rect = new Rectangle(0, 0, pbImageFrame.Width, pbImageFrame.Height);
			try {
				//
                //Scanner.DrawCaptureImageBuffer(g, rect, cbDetectCore.Checked);
				//
				Bitmap bitmap;
				int Resolution;
				Scanner.GetCaptureImageBuffer(out bitmap, out Resolution);
				pbImageFrame.Image = bitmap;
			}
			finally {
				g.Dispose();
			}
		}
		//=========================================================================//

		//=========================================================================//
		private void lbScannerList_SelectedValueChanged(object sender, EventArgs e) {
            GetCurrentScannerSettings();
		}

        private void cbTimeout_SelectedIndexChanged(object sender, EventArgs e) {
			UFScanner Scanner;
			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}
			// Unit of timeout is millisecond
            Scanner.Timeout = cbTimeout.SelectedIndex * 1000;
        }

        private void nudBrightness_ValueChanged(object sender, EventArgs e) {
			UFScanner Scanner;
			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}
            Scanner.Brightness = (int)nudBrightness.Value;
        }

        private void nudSensitivity_ValueChanged(object sender, EventArgs e) {
			UFScanner Scanner;
			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}
			Scanner.Sensitivity = (int)nudSensitivity.Value;
        }

        private void cbDetectCore_CheckedChanged(object sender, EventArgs e) {
			UFScanner Scanner;
			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}
			Scanner.DetectCore = cbDetectCore.Checked;
        }

		private void cbSecurityLevel_SelectedIndexChanged(object sender, EventArgs e) {
			if (m_Matcher != null) {
				// Security level ranges from 1 to 7
				m_Matcher.SecurityLevel = cbSecurityLevel.SelectedIndex + 1;
			}
		}

         private void cbScanTemplateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            UFScanner Scanner;
			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}
            
            switch(this.cbScanTemplateType.SelectedIndex){
                case 0:
                    Scanner.nTemplateType=2001;
                break;
                case 1:
                    Scanner.nTemplateType=2002;
                    break;
                case 2:
                    Scanner.nTemplateType=2003;
                break;
            }

        }

        private void cbMatchTemplateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_Matcher != null)
            {
                //matching template type 2001,2002,2003
                switch (this.cbMatchTemplateType.SelectedIndex)
                {
                    case 0:
                        m_Matcher.nTemplateType = 2001;
                        break;
                    case 1:
                        m_Matcher.nTemplateType = 2002;
                        break;
                    case 2:
                        m_Matcher.nTemplateType = 2003;
                        break;
                }
            }
        }

		private void cbFastMode_CheckedChanged(object sender, EventArgs e) {
			if (m_Matcher != null) {
				m_Matcher.FastMode = cbFastMode.Checked;
			}
		}

		private void btnClear_Click(object sender, EventArgs e) {
			tbxMessage.Clear();
		}
		//=========================================================================//

		//=========================================================================//
		private void UpdateScannerList()
		{
			int nScannerNumber;

			nScannerNumber = m_ScannerManager.Scanners.Count;

			lbScannerList.Items.Clear();

			for (int i = 0; i < nScannerNumber; i++) {
				UFScanner Scanner;
				UFS_SCANNER_TYPE ScannerType;
				string strScannerType;
				string strID;
				string str_tmp;

				Scanner = m_ScannerManager.Scanners[i];

				tbxMessage.AppendText("Scanner " + i + " serial: " + Scanner.Serial + "\r\n");

				ScannerType = Scanner.ScannerType;
				strID = Scanner.ID;
				GetScannerTypeString(ScannerType, out strScannerType);

				str_tmp = i + ": " + strScannerType + " " + strID;
				lbScannerList.Items.Add(str_tmp);
			}

			if (nScannerNumber > 0) {
				lbScannerList.SetSelected(0, true);
				GetCurrentScannerSettings();
			}
		}

		public void ScannerEvent(object sender, UFScannerManagerScannerEventArgs e)
		{
			if (e.SensorOn) {
				UpdateScannerList();
			} else {
				UpdateScannerList();
			}
		}

		private void btnInit_Click(object sender, EventArgs e) {
			//=========================================================================//
			// Initilize scanners
			//=========================================================================//
			UFS_STATUS ufs_res;
			int nScannerNumber;

			Cursor.Current = Cursors.WaitCursor;
			ufs_res = m_ScannerManager.Init();
			Cursor.Current = this.Cursor;
			if (ufs_res == UFS_STATUS.OK) {
				tbxMessage.AppendText("UFScanner Init: OK\r\n");
			} else {
				UFScanner.GetErrorString(ufs_res, out m_strError);
				tbxMessage.AppendText("UFScanner Init: " + m_strError + "\r\n");
				return;
			}

			m_ScannerManager.ScannerEvent += new UFS_SCANNER_PROC(ScannerEvent);

			nScannerNumber = m_ScannerManager.Scanners.Count;
			tbxMessage.AppendText("UFScanner GetScannerNumber: " + nScannerNumber + "\r\n");

			UpdateScannerList();
			//=========================================================================//

			//=========================================================================//
			// Create matcher
			//=========================================================================//
			m_Matcher = new UFMatcher();

			GetMatcherSettings(m_Matcher);
			//=========================================================================//
		}

		private void btnUpdate_Click(object sender, EventArgs e) {
			UFS_STATUS ufs_res;

			Cursor.Current = Cursors.WaitCursor;
			ufs_res = m_ScannerManager.Update();
			Cursor.Current = this.Cursor;

			if (ufs_res == UFS_STATUS.OK) {
				tbxMessage.AppendText("UFScanner Update: OK\r\n");
				UpdateScannerList();
			} else {
				UFScanner.GetErrorString(ufs_res, out m_strError);
				tbxMessage.AppendText("UFScanner Update: " + m_strError + "\r\n");
			}	
		}

		private void btnUninit_Click(object sender, EventArgs e) {
			//=========================================================================//
			// Uninit Scanners
			//=========================================================================//
			UFS_STATUS ufs_res;
			
			Cursor.Current = Cursors.WaitCursor;
			ufs_res = m_ScannerManager.Uninit();
			Cursor.Current = this.Cursor;
			if (ufs_res == UFS_STATUS.OK) {
				tbxMessage.AppendText("UFScanner Uninit: OK\r\n");
				m_ScannerManager.ScannerEvent -= ScannerEvent;
				lbScannerList.Items.Clear();
			} else {
				UFScanner.GetErrorString(ufs_res, out m_strError);
				tbxMessage.AppendText("UFScanner Uninit: " + m_strError + "\r\n");
			}

			pbImageFrame.Image = null;
			//=========================================================================//
		}

		private delegate void _UpdatePictureBox(PictureBox pbox, Bitmap image);

		public void UpdatePictureBox(PictureBox pbox, Image image) 
		{
			if (pbox.InvokeRequired) {
				_UpdatePictureBox del = new _UpdatePictureBox(UpdatePictureBox);
				// Call the function in the correct thread
				BeginInvoke(del, new object[] { pbox, image });
			} else {
				// We are in the correct thread, so assign the image
				pbox.Image = image;
				//System.Threading.Thread.Sleep(100);
			}
		}

		public void CaptureEvent(object sender, UFScannerCaptureEventArgs e)
		{
			// We cannot use pbImageFrame.Image directly from the different thread,
			// so we use UpdatePictureBox() to update PictureBox indirectly
			UpdatePictureBox(pbImageFrame, e.ImageFrame);
		}

        private void btnStartCapturing_Click(object sender, EventArgs e) {
			UFScanner Scanner;
			UFS_STATUS ufs_res;
			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}

			Scanner.CaptureEvent += new UFS_CAPTURE_PROC(CaptureEvent);
	        ufs_res = Scanner.StartCapturing();
	        if (ufs_res == UFS_STATUS.OK) {
		        tbxMessage.AppendText("UFScanner StartCapturing: OK\r\n");
	        } else {
				UFScanner.GetErrorString(ufs_res, out m_strError);
				tbxMessage.AppendText("UFScanner StartCapturing: " + m_strError + "\r\n");
	        }
        }

		private void btnAbortCapturing_Click(object sender, EventArgs e) {
			UFScanner Scanner;
			UFS_STATUS ufs_res;
			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}

	        ufs_res = Scanner.AbortCapturing();
	        if (ufs_res == UFS_STATUS.OK) {
		        tbxMessage.AppendText("UFScanner AbortCapturing: OK\r\n");
	        } else {
				UFScanner.GetErrorString(ufs_res, out m_strError);
				tbxMessage.AppendText("UFScanner AbortCapturing: " + m_strError + "\r\n");
	        }
		}

		private void btnCaptureSingle_Click(object sender, EventArgs e) {
			UFScanner Scanner;
			UFS_STATUS ufs_res;
			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}

            Cursor.Current = Cursors.WaitCursor;
	        ufs_res = Scanner.CaptureSingleImage();
			Cursor.Current = this.Cursor;

	        if (ufs_res == UFS_STATUS.OK) {
		        tbxMessage.AppendText("UFScanner CaptureSingleImage: OK\r\n");
				DrawCapturedImage(Scanner);
	        } else {
				UFScanner.GetErrorString(ufs_res, out m_strError);
				tbxMessage.AppendText("UFScanner CaptureSingleImage: " + m_strError + "\r\n");
	        }
		}

		private void btnExtract_Click(object sender, EventArgs e) {
			UFScanner Scanner;
			UFS_STATUS ufs_res;
			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}

			Scanner.TemplateSize = MAX_TEMPLATE_SIZE;
			Scanner.DetectCore = cbDetectCore.Checked;

			byte[] Template = new byte[MAX_TEMPLATE_SIZE];
			int TemplateSize;
			int EnrollQuality;

            Cursor.Current = Cursors.WaitCursor;
	        ufs_res = Scanner.Extract(Template, out TemplateSize, out EnrollQuality);
			Cursor.Current = this.Cursor;

			if (ufs_res == UFS_STATUS.OK) {
				tbxMessage.AppendText("UFScanner Extract: OK\r\n");
			} else {
				UFScanner.GetErrorString(ufs_res, out m_strError);
				tbxMessage.AppendText("UFScanner Extract: " + m_strError + "\r\n");
			}	
		}

		private void btnEnroll_Click(object sender, EventArgs e) {
			UFScanner Scanner;
			UFS_STATUS ufs_res;
			int EnrollQuality;

			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}
			Scanner.ClearCaptureImageBuffer();

			tbxMessage.AppendText("Place Finger\r\n");

			while (true) {
				ufs_res = Scanner.CaptureSingleImage();
				if (ufs_res != UFS_STATUS.OK) {
					UFScanner.GetErrorString(ufs_res, out m_strError);
					tbxMessage.AppendText("UFScanner CaptureSingleImage: " + m_strError + "\r\n");
					return;
				}
                
                switch (this.cbScanTemplateType.SelectedIndex)
                {
                    case 0:
                        Scanner.nTemplateType = 2001;
                        break;
                    case 1:
                        Scanner.nTemplateType = 2002;
                        break;
                    case 2:
                        Scanner.nTemplateType = 2003;
                        break;
                }

				ufs_res = Scanner.Extract(m_template[m_template_num], out m_template_size[m_template_num], out EnrollQuality);
				if (ufs_res == UFS_STATUS.OK) {
					tbxMessage.AppendText("UFScanner Extract: OK\r\n");
					DrawCapturedImage(Scanner);
					break;
				} else {
					UFScanner.GetErrorString(ufs_res, out m_strError);
					tbxMessage.AppendText("UFScanner Extract: " + m_strError + "\r\n");
				}

			}

			if (EnrollQuality < cbEnrollQuality.SelectedIndex * 10 + 30 ) {
				tbxMessage.AppendText("Too low quality [Q:" + EnrollQuality + "]\r\n");
				return;
			}

			tbxMessage.AppendText("Enrollment success (No." + (m_template_num+1) + ") [Q:" + EnrollQuality + "]\r\n");

			cbID.Items.Add(m_template_num+1);
			if (m_template_num+1 == MAX_TEMPLATE_NUM) {
				tbxMessage.AppendText("Template memory is full\r\n");
			} else {
				m_template_num++;
			}
		}

		private void btnVerify_Click(object sender, EventArgs e) {
			UFScanner Scanner;
			UFS_STATUS ufs_res;
			UFM_STATUS ufm_res;
			byte[] Template = new byte[MAX_TEMPLATE_SIZE];
			int TemplateSize;
			int EnrollQuality;
			bool VerifySucceed;
			int SelectID;

			SelectID = cbID.SelectedIndex;
			if (SelectID == -1) {
				tbxMessage.AppendText("Select Enroll ID\r\n");
				return;
			}

			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}
			Scanner.ClearCaptureImageBuffer();

			tbxMessage.AppendText("Place Finger\r\n");

			ufs_res = Scanner.CaptureSingleImage();
			if (ufs_res != UFS_STATUS.OK) {
				UFScanner.GetErrorString(ufs_res, out m_strError);
				tbxMessage.AppendText("UFScanner CaptureSingleImage: " + m_strError + "\r\n");
				return;
			}

            switch (this.cbScanTemplateType.SelectedIndex)
            {
                case 0:
                    Scanner.nTemplateType = 2001;
                    break;
                case 1:
                    Scanner.nTemplateType = 2002;
                    break;
                case 2:
                    Scanner.nTemplateType = 2003;
                    break;
            }
                       
			ufs_res = Scanner.Extract(Template, out TemplateSize, out EnrollQuality);
			if (ufs_res == UFS_STATUS.OK) {
				DrawCapturedImage(Scanner);
			} else {
				UFScanner.GetErrorString(ufs_res, out m_strError);
				tbxMessage.AppendText("UFScanner Extract: " + m_strError + "\r\n");
				return;
			}

            switch (this.cbMatchTemplateType.SelectedIndex)
            {
                case 0:
                    m_Matcher.nTemplateType = 2001;
                    break;
                case 1:
                    m_Matcher.nTemplateType = 2002;
                    break;
                case 2:
                    m_Matcher.nTemplateType = 2003;
                    break;
            }

			ufm_res = m_Matcher.Verify(Template, TemplateSize, m_template[SelectID], m_template_size[SelectID], out VerifySucceed);
			if (ufm_res != UFM_STATUS.OK) {
				UFMatcher.GetErrorString(ufm_res, out m_strError);
				tbxMessage.AppendText("UFMatcher Verify: " + m_strError + "\r\n");
				return;
			}

			if (VerifySucceed) {
				tbxMessage.AppendText("Verification succeed (No." + (SelectID+1) + ")\r\n");
			} else {
				tbxMessage.AppendText("Verification failed\r\n");
			}
		}

		private void btnIdentify_Click(object sender, EventArgs e) {
			UFScanner Scanner;
			UFS_STATUS ufs_res;
			UFM_STATUS ufm_res;
			byte[] Template = new byte[MAX_TEMPLATE_SIZE];
			int TemplateSize;
			int EnrollQuality;
			int MatchIndex;

			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}
			Scanner.ClearCaptureImageBuffer();

			tbxMessage.AppendText("Place Finger\r\n");

			ufs_res = Scanner.CaptureSingleImage();
			if (ufs_res != UFS_STATUS.OK) {
				UFScanner.GetErrorString(ufs_res, out m_strError);
				tbxMessage.AppendText("UFScanner CaptureSingleImage: " + m_strError + "\r\n");
				return;
			}
            
            switch (this.cbScanTemplateType.SelectedIndex)
            {
                case 0:
                    Scanner.nTemplateType = 2001;
                    break;
                case 1:
                    Scanner.nTemplateType = 2002;
                    break;
                case 2:
                    Scanner.nTemplateType = 2003;
                    break;
            }

			ufs_res = Scanner.Extract(Template, out TemplateSize, out EnrollQuality);
			if (ufs_res == UFS_STATUS.OK) {
				DrawCapturedImage(Scanner);
			} else {
				UFScanner.GetErrorString(ufs_res, out m_strError);
				tbxMessage.AppendText("UFScanner Extract: " + m_strError + "\r\n");
				return;
			}
            
            switch (this.cbMatchTemplateType.SelectedIndex)
            {
                case 0:
                    m_Matcher.nTemplateType = 2001;
                    break;
                case 1:
                    m_Matcher.nTemplateType = 2002;
                    break;
                case 2:
                    m_Matcher.nTemplateType = 2003;
                    break;
            }

			Cursor.Current = Cursors.WaitCursor;
			//*
			ufm_res = m_Matcher.Identify(Template, TemplateSize, m_template, m_template_size, m_template_num, 5000, out MatchIndex);
			//ufm_res = m_Matcher.IdentifyMT(Template, TemplateSize, m_template, m_template_size, m_template_num, 5000, out MatchIndex);
			/*/
			{
				byte[,] Template2 = new byte[m_template_num, MAX_TEMPLATE_SIZE];
				int i, j;
				for (j = 0; j < m_template_num; j++) {
					for (i = 0; i < m_template_size[j]; i++) {
						Template2[j,i] = m_template[j][i];
					}
				}
				ufm_res = m_Matcher.Identify(Template, TemplateSize, m_template, m_template_size, m_template_num, 5000, out MatchIndex);
			}
			//*/
			Cursor.Current = this.Cursor;
			if (ufm_res != UFM_STATUS.OK) {
				UFMatcher.GetErrorString(ufm_res, out m_strError);
				tbxMessage.AppendText("UFMatcher Identify: " + m_strError + "\r\n");
				return;
			}

			if (MatchIndex != -1) {
				tbxMessage.AppendText("Identification succeed (No." + (MatchIndex+1) + ")\r\n");
			} else {
				tbxMessage.AppendText("Identification failed\r\n");
			}
		}

		private void btnSaveTemplate_Click(object sender, EventArgs e) {
			int SelectID;

			SelectID = cbID.SelectedIndex;
			if (SelectID == -1) {
				tbxMessage.AppendText("Select Enroll ID\r\n");
				return;
			}

			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Template files (*.tmp)|*.tmp";
			dlg.DefaultExt = "tmp";
			if (dlg.ShowDialog() != DialogResult.OK) {
				return;
			}

			using (FileStream fs = File.Create(dlg.FileName)) {
				fs.Write(m_template[SelectID], 0, m_template_size[SelectID]);
				fs.Close();
				tbxMessage.AppendText("Selected template is saved\r\n");
			}
		}

		private void btnSaveImage_Click(object sender, EventArgs e) {
			UFScanner Scanner;
			UFS_STATUS ufs_res;

			if (!GetGetCurrentScanner(out Scanner)) {
				return;
			}

			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Bitmap files (*.bmp)|*.bmp";
			dlg.DefaultExt = "bmp";
			if (dlg.ShowDialog() != DialogResult.OK) {
				return;
			}

			ufs_res = Scanner.SaveCaptureImageBufferToBMP(dlg.FileName);
			if (ufs_res == UFS_STATUS.OK) {
				tbxMessage.AppendText("UFScanner Image Buffer is saved to " + dlg.FileName + "\r\n");
			}
		}
		//=========================================================================//
	}
}