using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Suprema;
using System.Data.SqlClient;

namespace Trix
{
    public partial class Captura : Form
    {
        public int Funcionario;

        UFScannerManager m_ScannerManager;
        UFMatcher m_Matcher;
        string m_strError;
        byte[] m_template1 = null;
        byte[] m_template2 = null;
        byte[] m_template3 = null;
        int m_template_size;
        int m_template_num;
        int indexScannerSelecionado = 0;
        bool digitalCapturada = false;
        int numDigital = 0;

        const int MAX_TEMPLATE_SIZE = 384;
        const int MAX_TEMPLATE_NUM = 50;

        public Captura()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Init()
        {
            //=========================================================================//
            // Inicializa algoritmo
            //=========================================================================//
            UFS_STATUS ufs_res;
            int nScannerNumber;

            Cursor.Current = Cursors.WaitCursor;
            ufs_res = m_ScannerManager.Init();
            Cursor.Current = this.Cursor;
            if (ufs_res == UFS_STATUS.OK)
            {
                lbMensagem.Text = "Inicialização de algoritmo: OK";
            }
            else
            {
                UFScanner.GetErrorString(ufs_res, out m_strError);
                lbMensagem.Text = m_strError;
                return;
            }

            //m_ScannerManager.ScannerEvent += new UFS_SCANNER_PROC(ScannerEvent);

            nScannerNumber = m_ScannerManager.Scanners.Count;

            m_template1 = null;
            m_template2 = null;
            m_template3 = null;

        }

        private void Captura_Load(object sender, EventArgs e)
        {
            m_template1 = null;
            m_template2 = null;
            m_template3 = null;

            m_template_size = 0;
            m_template_num = 0;

            m_ScannerManager = new UFScannerManager(this);

            Init();
        }

        private void ler_funcionario()
        {
            DB db = new DB();

            SqlCommand Comm = db.Conn.CreateCommand();

            Comm.CommandText = "SELECT FUNC_NOME, FUNC_PIS FROM Funcionarios WHERE FUNC_IND = " + Funcionario.ToString();
            SqlDataReader reader = Comm.ExecuteReader();

            reader.Read();
            edNome.Text = reader["FUNC_NOME"].ToString().Trim();
            edPis.Text = reader["FUNC_PIS"].ToString().Trim();
            reader.Close();
        }

        private bool GetCurrentScanner(out UFScanner Scanner)
        {
            Scanner = m_ScannerManager.Scanners[0];
            if (Scanner != null)
            {
                return true;
            }
            else
            {
                lbMensagem.Text = "Scanner não está conectado";
                return false;
            }
        }

        private void Capturar()
        {
            UFScanner Scanner;
            UFS_STATUS ufs_res;
            int EnrollQuality = 0;
            numDigital = 1;

            if (!GetCurrentScanner(out Scanner))
            {
                return;
            }
            Scanner.ClearCaptureImageBuffer();

            lbMensagem.Text = "Posicione o dedo";
            pbImageFrame.Image = null;
            digitalCapturada = false;

            while (true)
            {
                Application.DoEvents();
                ufs_res = Scanner.CaptureSingleImage();
                if (ufs_res != UFS_STATUS.OK)
                {
                    UFScanner.GetErrorString(ufs_res, out m_strError);
                    lbMensagem.Text = m_strError;
                    return;
                }

                // Configura o tipo de template pra Suprema (2001)
                Scanner.nTemplateType = 2001;

                switch (numDigital)
                {
                    case 1:
                        m_template1 = new byte[MAX_TEMPLATE_SIZE];
                        ufs_res = Scanner.Extract(m_template1, out m_template_size, out EnrollQuality);
                        break;
                    case 2:
                        m_template2 = new byte[MAX_TEMPLATE_SIZE];
                        ufs_res = Scanner.Extract(m_template2, out m_template_size, out EnrollQuality);
                        break;
                    case 3:
                        m_template3 = new byte[MAX_TEMPLATE_SIZE];
                        ufs_res = Scanner.Extract(m_template3, out m_template_size, out EnrollQuality);
                        break;
                }
                if (ufs_res == UFS_STATUS.OK)
                {
                    lbMensagem.Text = "Digital capturada [Q:" + EnrollQuality + "/100]";
                    //statusLabel.Text = "Digital capturada";
                    lbMensagem.ForeColor = Color.Green;
                    DrawCapturedImage(Scanner);
                    SalvaTemplate(m_template1, edPis.Text, 1);
                    digitalCapturada = true;
                    break;
                }
                else
                {
                    UFScanner.GetErrorString(ufs_res, out m_strError);
                    lbMensagem.Text = m_strError;
                    digitalCapturada = false;
                }

            }

            if (EnrollQuality < 50)
            {
                lbMensagem.Text = "Qualidade da digital muito baixa [Q:" + EnrollQuality + "/100]";
                digitalCapturada = false;
                return;
            }
        }

        private void DrawCapturedImage(UFScanner Scanner)
        {
            Graphics g = pbImageFrame.CreateGraphics();
            Rectangle rect = new Rectangle(0, 0, pbImageFrame.Width, pbImageFrame.Height);
            try
            {
                Scanner.DrawCaptureImageBuffer(g, rect, true);
                Bitmap bitmap;
                int Resolution;
                Scanner.GetCaptureImageBuffer(out bitmap, out Resolution);
                pbImageFrame.Image = bitmap;
            }
            finally
            {
                g.Dispose();
            }
        }

        private void Captura_Shown(object sender, EventArgs e)
        {
            ler_funcionario();
            Capturar();
        }

        public void FinalizaBiometria()
        {
            //=========================================================================//
            // Finaliza algoritmo
            //=========================================================================//
            UFS_STATUS ufs_res;

            Cursor.Current = Cursors.WaitCursor;
            ufs_res = m_ScannerManager.Uninit();
            Cursor.Current = this.Cursor;
            if (ufs_res != UFS_STATUS.OK)
            {
                UFScanner.GetErrorString(ufs_res, out m_strError);
                lbMensagem.Text = m_strError;
            }

            pictureBox1.Image = null;
        }

        private void Captura_FormClosing(object sender, FormClosingEventArgs e)
        {
            FinalizaBiometria();
        }

        public bool SalvaTemplate(byte[] template, string pis, int n)
        {
            try
            {
                string diretoriobiometrias = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Asseponto\\Biometria";

                if (!Directory.Exists(diretoriobiometrias))
                    Directory.CreateDirectory(diretoriobiometrias);

                string arquivo = diretoriobiometrias + "\\" + pis + "-" + n.ToString() + ".bio";
                using (FileStream fs = File.Create(arquivo))
                {
                    fs.Write(template, 0, 384);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
