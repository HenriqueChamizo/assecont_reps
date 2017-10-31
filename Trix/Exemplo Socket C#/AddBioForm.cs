using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Suprema;
using Validadores;

namespace TRXHumanResource
{
    public partial class AddBioForm : Form
    {
        // Biometria
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

        // Variáveis tamanho de campo
        int tamanhoNome = 52;
        int tamanhoPIS = 12;
        int tamanhoCracha = 15;


        public AddBioForm()
        {
            InitializeComponent();
        }

        private void AddBioForm_Load(object sender, EventArgs e)
        {
            int i;

            // Inicia os valores



            m_template1 = null;
            m_template2 = null;
            m_template3 = null;

            m_template_size = 0;
            m_template_num = 0;

            m_ScannerManager = new UFScannerManager(this);
        }

        private void cbTeclado_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaSenha();
        }

        /// <summary>
        /// Decide se a senha pode ser digitada ou não
        /// </summary>
        public void HabilitaSenha()
        {
            if (cbBarras.Checked || cbBio.Checked || cbProx.Checked || cbTeclado.Checked)
            {
                cbSenha.Enabled = true;
            }
            else
            {
                cbSenha.Checked = false;
                cbSenha.Enabled = false;
                labelSenha1.Enabled = false;
                labelSenha2.Enabled = false;
                tbxSenha.Text = string.Empty;
                tbxSenha1.Text = string.Empty;
                tbxSenha.Enabled = false;
                tbxSenha1.Enabled = false;
            }
        }

        private void cbSenha_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSenha.Checked)
            {
                labelSenha1.Enabled = true;
                labelSenha2.Enabled = true;
                tbxSenha.Enabled = true;
                tbxSenha1.Enabled = true;
            }
            else
            {
                labelSenha1.Enabled = false;
                labelSenha2.Enabled = false;
                tbxSenha.Enabled = false;
                tbxSenha1.Enabled = false;
                label7.Visible = false;
                label8.Visible = false;
            }
        }

        private void cbBarras_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaSenha();
        }

        private void cbProx_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaSenha();
        }

        private void cbBio_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaSenha();
            if (cbBio.Checked)
            {
                InicializaBiometria();
                checkBox1.Visible = true;
                checkBox2.Visible = true;
            }
            else
            {
                FinalizaBiometria();
                checkBox1.Visible = false;
                checkBox2.Visible = false;
            }
        }

        public void FinalizaBiometria()
        {
            groupBox2.Enabled = false;
            //=========================================================================//
            // Finaliza algoritmo
            //=========================================================================//
            UFS_STATUS ufs_res;

            Cursor.Current = Cursors.WaitCursor;
            ufs_res = m_ScannerManager.Uninit();
            Cursor.Current = this.Cursor;
            if (ufs_res == UFS_STATUS.OK)
            {
                toolStripStatusLabel1.Text = "Algoritmo finalizado";
                m_ScannerManager.ScannerEvent -= ScannerEvent;
            }
            else
            {
                UFScanner.GetErrorString(ufs_res, out m_strError);
                toolStripStatusLabel1.Text = m_strError;
            }

            pictureBox1.Image = null;
        }

        public void InicializaBiometria()
        {
            groupBox2.Enabled = true;

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
                toolStripStatusLabel1.Text = "Inicialização de algoritmo: OK";
            }
            else
            {
                UFScanner.GetErrorString(ufs_res, out m_strError);
                toolStripStatusLabel1.Text = m_strError;
                return;
            }

            m_ScannerManager.ScannerEvent += new UFS_SCANNER_PROC(ScannerEvent);

            nScannerNumber = m_ScannerManager.Scanners.Count;
            toolStripStatusLabel1.Text = "Algoritmo inicializado";

            groupBox2.Enabled = true;

            m_template1 = null;
            m_template2 = null;
            m_template3 = null;

        }

        public void ScannerEvent(object sender, UFScannerManagerScannerEventArgs e)
        {
            if (e.SensorOn)
            {
                AtualizaInfoScanner();
            }
            else
            {
                AtualizaInfoScanner();
            }
        }

        public void AtualizaInfoScanner()
        {
            int nScannerNumber;

            nScannerNumber = m_ScannerManager.Scanners.Count;

            if (m_ScannerManager.Scanners.Count != 0)
            {
                for (int i = 0; i < nScannerNumber; i++)
                {
                    UFScanner Scanner;
                    UFS_SCANNER_TYPE ScannerType;
                    string strScannerType;
                    string strID;
                    string str_tmp;

                    Scanner = m_ScannerManager.Scanners[i];

                    toolStripStatusLabel1.Text = "Scanner S/N: (" + Scanner.Serial + ") conectado";
                    indexScannerSelecionado = i;

                    ScannerType = Scanner.ScannerType;
                    strID = Scanner.ID;




                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Nenhum scanner conectado";
            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool GetCurrentScanner(out UFScanner Scanner)
        {
            Scanner = m_ScannerManager.Scanners[indexScannerSelecionado];
            if (Scanner != null)
            {
                return true;
            }
            else
            {
                toolStripStatusLabel1.Text = "Scanner não está conectado";
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UFScanner Scanner;
            UFS_STATUS ufs_res;
            int EnrollQuality = 0;

            if (!GetCurrentScanner(out Scanner))
            {
                return;
            }
            Scanner.ClearCaptureImageBuffer();

            toolStripStatusLabel1.Text = "Posicione o dedo";
            pictureBox1.Image = null;
            digitalCapturada = false;

            while (true)
            {
                Application.DoEvents();
                ufs_res = Scanner.CaptureSingleImage();
                if (ufs_res != UFS_STATUS.OK)
                {
                    UFScanner.GetErrorString(ufs_res, out m_strError);
                    toolStripStatusLabel1.Text = m_strError;
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
                    toolStripStatusLabel1.Text = "Digital capturada [Q:"+EnrollQuality+"/100]";
                    statusLabel.Text = "Digital capturada";
                    statusLabel.ForeColor = Color.Green;
                    DrawCapturedImage(Scanner);
                    digitalCapturada = true;
                    break;
                }
                else
                {
                    UFScanner.GetErrorString(ufs_res, out m_strError);
                    toolStripStatusLabel1.Text = m_strError;
                    digitalCapturada = false;
                }

            }

            if (EnrollQuality < 50)
            {
                toolStripStatusLabel1.Text = "Qualidade da digital muito baixa [Q:" + EnrollQuality + "/100]";
                digitalCapturada = false;
                return;
            }
            

        }

        private void DrawCapturedImage(UFScanner Scanner)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Rectangle rect = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            try
            {
                
                //Scanner.DrawCaptureImageBuffer(g, rect, cbDetectCore.Checked);
                
                Bitmap bitmap;
                int Resolution;
                Scanner.GetCaptureImageBuffer(out bitmap, out Resolution);
                pictureBox1.Image = bitmap;
            }
            finally
            {
                g.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            char[] trim = {'=', '\\', ';','.', ':',',','+','*','"','[',']','{','}','_','-','&','¨','%','¬','(',')','!','@','^','~','º','#','$'}; 
                 int pos;
                 while ((pos = this.textBox1.Text.IndexOfAny (trim)) >= 0) 
                 {
                 this.textBox1.Text = this.textBox1.Text.Remove (pos, 1);
                 }
                 string comeco = textBox1.Text.Substring(0, 1);
                if (textBox1.Text != " " && comeco != " ")
                {
                    if (ValidaDados())
                    {
                        // Cria a permissao
                        Permissao perm = new Permissao();
                        perm.Barras = cbBarras.Checked;
                        perm.Biometria = cbBio.Checked;
                        perm.Prox = cbProx.Checked;
                        perm.Senha = cbSenha.Checked;
                        perm.Teclado = cbTeclado.Checked;
                        if (checkBox1.Checked)
                            perm.ModoBio = 1;
                        if (checkBox2.Checked)
                            perm.ModoBio = 0;

                        // Cria o funcionário
                        Funcionario func = new Funcionario();
                        func.ID = 0;
                        //func.IDEmpresa = EmpresaForm.idEmpresa;
                        func.Nome = textBox1.Text;
                        func.PIS = textBox2.Text;
                        func.Cracha = textBox3.Text;
                        func.Senha = tbxSenha.Text;
                        func.Permissao = perm;
                        func.Grupo = EmployeeGroupForm.gpcl.Id;
                        if (digitalCapturada)
                        {
                            if (m_template1 != null)
                                func.FullTemplate1 = m_template1;
                            if (m_template2 != null)
                                func.FullTemplate2 = m_template2;
                            if (m_template3 != null)
                                func.FullTemplate3 = m_template3;
                        }
                        else
                        {
                            func.FullTemplate1 = null;
                            func.FullTemplate2 = null;
                            func.FullTemplate3 = null;
                        }

                        if (func.Salva())
                        {
                            MessageBox.Show("Funcionário cadastrado", "Aviso");
                            this.Close();
                        }
                        else
                            MessageBox.Show("Erro ao cadastrar funcionário", "Aviso");

                    }
            }
        }

        

        public bool ValidaDados()
        {
            #region Verifica todos os textBox (necessários)
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("O nome não foi informado", "Aviso");
                textBox1.Focus();
                return false;
            }
            if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("O PIS não foi informado", "Aviso");
                textBox2.Focus();
                return false;
            }
            if (textBox3.Text == string.Empty)
            {
                MessageBox.Show("O crachá não foi informado", "Aviso");
                textBox3.Focus();
                return false;
            }
            #endregion

            #region Valida os dados essenciais
            // Verifica o tamanho do nome
            string nome = textBox1.Text;
            if (nome.Length > 52)
            {
                MessageBox.Show("O nome deve conter no máximo 52 caracteres", "Aviso");
                textBox1.SelectAll();
                textBox1.Focus();
                return false;
            }
            // Verifica o tamanho do PIS
            string pis = textBox2.Text;
            if (pis.Length != 12)
            {
                MessageBox.Show("O PIS deve conter 12 caracteres", "Aviso");
                textBox2.SelectAll();
                textBox2.Focus();
                return false;
            }
            // Verifica se o PIS é válido
            ValidaPis validador = new ValidaPis();
            if (!validador.Pis(textBox2.Text))
            {
                MessageBox.Show("Pis Inválido", "Aviso");
                return false;
            }
            // Verifica o tamanho do crachá
            string cracha = textBox3.Text;
            if (cracha.Length > 15)
            {
                MessageBox.Show("O crachá deve conter no máximo 15 caracteres", "Aviso");
                textBox3.SelectAll();
                textBox3.Focus();
                return false;
            }

            //Verifica se já existe um crachá cadastrado com esse número
            string verifica_cracha = ArrumaCracha(cracha);
            if (DB.VerificaCracha(verifica_cracha))
            {
                MessageBox.Show("Já existe um crachá com esse número cadastrado no Banco de Dados", "Aviso");
                return false;
            }
            #endregion

            #region Verifica os métodos de marcação
            bool teclado = cbTeclado.Checked;
            bool prox = cbProx.Checked;
            bool codigoBarras = cbBarras.Checked;
            bool biometria = cbBio.Checked;
            bool senha = cbSenha.Checked;
            int modoBio = -1;

            if (!teclado && !prox && !codigoBarras && !biometria)
            {
                MessageBox.Show("Pelo menos um método de marcação deve ser selecionado", "Aviso");
                return false;
            }

            if (biometria)
            {
                if (checkBox1.Checked)
                    modoBio = 1;
                if (checkBox2.Checked)
                    modoBio = 0;
                switch (modoBio)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    default:
                        MessageBox.Show("O modo de identificação biométrica deve ser selecionado", "Aviso");
                        break;
                }
            }

            if (senha)
            {
                #region Verifica se as senhas foram digitadas
                if ((tbxSenha.Text == string.Empty) || (tbxSenha1.Text == string.Empty))
                {
                    if (tbxSenha.Text == string.Empty)
                    {
                        label7.Text = "Informar senha";
                        label7.ForeColor = Color.Red;
                        label7.Font = new Font(label7.Font, FontStyle.Bold);
                        label7.Visible = true;
                    }
                    else
                        label7.Visible = false;
                    if (tbxSenha1.Text == string.Empty)
                    {
                        label8.Text = "Confirmar senha";
                        label8.ForeColor = Color.Red;
                        label8.Font = new Font(label8.Font, FontStyle.Bold);
                        label8.Visible = true;
                    }
                    else
                        label8.Visible = false;
                    return false;
                }
                else
                {
                    if (tbxSenha.Text != string.Empty)
                        label7.Visible = false;
                    if (tbxSenha1.Text != string.Empty)
                        label8.Visible = false;
                }
                
                #endregion

                #region Compara as senhas
                if (!tbxSenha.Text.Equals(tbxSenha1.Text))
                {
                    MessageBox.Show("As senhas não coincidem", "Aviso");
                    tbxSenha.SelectAll();
                    tbxSenha.Focus();
                    return false;
                }
                #endregion

                #region Verifica o tamanho das senhas
                string senha1 = tbxSenha.Text;
                string senha2 = tbxSenha1.Text;

                if (senha1.Length != 6)
                {
                    MessageBox.Show("A senha deve conter seis (6) dígitos", "Aviso");
                    tbxSenha.SelectAll();
                    tbxSenha.Focus();
                    return false;
                }
                if (senha2.Length != 6)
                {
                    MessageBox.Show("A senha deve conter seis (6) dígitos", "Aviso");
                    tbxSenha1.SelectAll();
                    tbxSenha1.Focus();
                    return false;
                }
                #endregion
            }


            #endregion

            // Verifica se o pis já existe
            if (DB.ExistePis(pis))
            {
                MessageBox.Show("O PIS informado já existe", "Aviso");
                textBox2.SelectAll();
                textBox2.Focus();
                return false;
            }

            return true;


        }
        //Se o crachá tiver menos de 15 caracteres, completa com '0' à esquerda
        private static string ArrumaCracha(string pre_cracha)
        {
            if (pre_cracha.Length < 15)
            {
                while (pre_cracha.Length < 15)
                {
                    pre_cracha = "0" + pre_cracha;
                }
            }
            else
            {
                pre_cracha = pre_cracha.Substring(pre_cracha.Length - 15);
            }
            return pre_cracha;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tamanhoNome = 52 - textBox1.Text.Length;
            if (tamanhoNome < 0)
            {
                int temp = tamanhoNome * (-1);
                label4.Text = "Caracteres excedidos (" + temp + ")";
                label4.ForeColor = Color.Red;
                label4.Font = new Font(label4.Font, FontStyle.Bold);
            }
            if (tamanhoNome == 0)
            {
                label4.Text = "Tamanho máximo";
                label4.ForeColor = Color.Green;
                label4.Font = new Font(label4.Font, FontStyle.Bold);
            }
            if (tamanhoNome > 0)
            {
                label4.Text = "Caracteres restantes: " + tamanhoNome;
                label4.ForeColor = Color.Black;
                label4.Font = new Font(label4.Font, FontStyle.Regular);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            tamanhoPIS = 12 - textBox2.Text.Length;
            if (tamanhoPIS < 0)
            {
                int temp = tamanhoPIS * (-1);
                label5.Text = "Caracteres excedidos (" + temp + ")";
                label5.ForeColor = Color.Red;
                label5.Font = new Font(label5.Font, FontStyle.Bold);
            }
            if (tamanhoPIS == 0)
            {
                label5.Text = "Tamanho máximo";
                label5.ForeColor = Color.Green;
                label5.Font = new Font(label5.Font, FontStyle.Bold);
            }
            if (tamanhoPIS > 0)
            {
                label5.Text = "Caracteres restantes: " + tamanhoPIS;
                label5.ForeColor = Color.Black;
                label5.Font = new Font(label5.Font, FontStyle.Regular);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            tamanhoCracha = 15 - textBox3.Text.Length;
            if (tamanhoCracha < 0)
            {
                int temp = tamanhoCracha * (-1);
                label6.Text = "Caracteres excedidos (" + temp + ")";
                label6.ForeColor = Color.Red;
                label6.Font = new Font(label6.Font, FontStyle.Bold);
            }
            if (tamanhoCracha == 0)
            {
                label6.Text = "Tamanho máximo";
                label6.ForeColor = Color.Green;
                label6.Font = new Font(label6.Font, FontStyle.Bold);
            }
            if (tamanhoCracha > 0)
            {
                label6.Text = "Caracteres restantes: " + tamanhoCracha;
                label6.ForeColor = Color.Black;
                label6.Font = new Font(label6.Font, FontStyle.Regular);
            }
        }

        private void AddBioForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FinalizaBiometria();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBio.Checked)
            {
                if (checkBox1.Checked)
                    checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBio.Checked)
            {
                if (checkBox2.Checked)
                    checkBox1.Checked = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                button1.Enabled = true;
                numDigital = comboBox1.SelectedIndex + 1;
                ControleBiometrico();
            }
            else
            {
                button1.Enabled = false;
            }
        }

        public void ControleBiometrico()
        {
            switch (numDigital)
            {
                case 1:
                    statusLabel.Visible = true;
                    if (m_template1 != null)
                    {
                        statusLabel.Text = "Digital capturada";
                        statusLabel.ForeColor = Color.Green;
                    }
                    else
                    {
                        statusLabel.Text = "Não capturada";
                        statusLabel.ForeColor = Color.Tomato;
                    }
                    break;
                case 2:
                    statusLabel.Visible = true;
                    if (m_template2 != null)
                    {
                        statusLabel.Text = "Digital capturada";
                        statusLabel.ForeColor = Color.Green;
                    }
                    else
                    {
                        statusLabel.Text = "Não capturada";
                        statusLabel.ForeColor = Color.Tomato;
                    }
                    break;
                case 3:
                    statusLabel.Visible = true;
                    if (m_template3 != null)
                    {
                        statusLabel.Text = "Digital capturada";
                        statusLabel.ForeColor = Color.Green;
                    }
                    else
                    {
                        statusLabel.Text = "Não capturada";
                        statusLabel.ForeColor = Color.Tomato;
                    }
                    break;
            }

        }
        

    }
}