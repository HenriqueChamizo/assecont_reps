using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
//using System.Threading;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using Wr.Classes;

namespace IdData
{
    public partial class Principal : Form
    {
        private CIDSysR30 objIDSysR30;

        const int TRM_SUBITEM_DESCRICAO = 0;
        const int TRM_SUBITEM_IP = 1;
        const int TRM_SUBITEM_PORTA = 2;
        const int TRM_SUBITEM_IND = 3;

        const int FUNC_SUBITEM_NOME = 0;
        const int FUNC_SUBITEM_FUNCAO = 1;
        const int FUNC_SUBITEM_CRACHA = 2;
        const int FUNC_SUBITEM_PIS = 3;
        const int FUNC_SUBITEM_IND = 4;
        const int FUNC_SUBITEM_ENVIADOEM = 5;

        int Grupo;
        public string dirroot = "C:\\";
        public bool CANCELAR = false;

        private string IP;
        private int PORTA;
        private string TERMINALNOME;

        public List<string> logStatus = new List<string>();

        public Principal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Assecont\\Asseponto4\\Opcoes");
            mmSobreporFuncionarios.Checked = Convert.ToBoolean(key.GetValue("Sobrepor funcionarios no terminal", true));
            mmNaoEnviarFuncEnviados.Checked = Convert.ToBoolean(key.GetValue("Nao enviar funcionarios para o terminal se ja foi enviado", true));
            mmGerarArquivoUsbApenasNaoEnviados.Checked = Convert.ToBoolean(key.GetValue("Gerar no arquivo usb apenas os funcionarios nao enviados", true));
            key.Close();

            try
            {
                this.objIDSysR30 = new CIDSysR30();
            }
            catch (Exception exError)
            {
                MessageBox.Show(exError.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            preencher_terminais();
            ler_grupo();
        }

        public void EnviaInfoEmpresa(int Terminal, string IP, int Porta)
        {
            string Identificador;
            string Nome;
            ulong Cei;
            string Endereco;
            byte IdentificadorTipo;

            DB db = new DB();

            db.LerEmpresa(Terminal, out IdentificadorTipo, out Identificador, out Nome, out Cei, out Endereco);

            Rede rede = new Rede(this.objIDSysR30, IP, Porta, edLog);
            rede.IdData_EnviaEmpresa(IdentificadorTipo, Identificador, Nome, Cei, Endereco);
        }

        private void preencher_terminais()
        {
            ListViewItem item;
            DB db = new DB();
            SqlCommand comm = db.Conn.CreateCommand();

            comm.CommandText = "SELECT TRM_DESCRICAO, TRM_IP, TRM_PORTA, TRM_IND FROM Terminais ORDER BY TRM_DESCRICAO";
            SqlDataReader reader = comm.ExecuteReader();

            listRelogios.Items.Clear();

            while (reader.Read())
            {
                item = new ListViewItem();
                item.Text = reader["TRM_DESCRICAO"].ToString().Trim();
                item.SubItems.Add(reader["TRM_IP"].ToString().Trim());
                item.SubItems.Add(reader["TRM_PORTA"].ToString().Trim());
                item.SubItems.Add(reader["TRM_IND"].ToString());
                item.ImageIndex = 0;
                listRelogios.Items.Add(item);
            }

            reader.Close();
        }

        private void ler_grupo()
        {
            DB db = new DB();
            Grupo = Convert.ToInt32(db.GetFieldValue("SELECT TOP 1 GRU_IND FROM Grupos"));
        }

        private void Abrir_Terminal(int Terminal)
        {
            RepEdicao frmRepEdicao = new RepEdicao();
            frmRepEdicao.Terminal = Terminal;
            frmRepEdicao.Grupo = Grupo;

            if (frmRepEdicao.ShowDialog() == DialogResult.OK)
                preencher_terminais();
        }

        public void EnviaDataHora(string IP, int Porta)
        {
            Rede rede = new Rede(this.objIDSysR30, IP, Porta, edLog);
            rede.IdData_EnviaDataHora();
        }


        //private string ReadFile(string filePath)
        //{
        //byte[] buffer = new byte[384];

        //using (FileStream file = new FileStream(filePath, FileMode.Open))
        //{
        //    file.Read(buffer, 0, 384);
        //    file.Close();
        //}

        //return Protocolo.ConverteByteToString(buffer);
        //}

        private void EnviarBiometria(Rede rede, string PIS, string Nome, int Funcionario, string Arquivo)
        {
            //Rede rede = new Rede(objIDSysR30, IP, Porta, edLog);

            string strPIS = PIS;
            string strUserName = Nome;
            uint uiKeyCode = 0;
            string strBarCode = "";
            byte byFacilityCode = 0;
            uint uiProxCode = 0;
            byte byUserType = 0;
            string strPassword = "";
            ushort usSizeSample = 404;
            byte byQuantitySamples = 2;
            byte[] rgbyBiometrics = new byte[(usSizeSample * byQuantitySamples)];
            byte byAccessType = 0;

            using (FileStream file = new FileStream(Arquivo, FileMode.Open))
            {
                file.Read(rgbyBiometrics, 0, rgbyBiometrics.Length);
                file.Close();
            }

            rede.IdData_EnviarFuncionarioComBiometria(strPIS, strUserName, uiKeyCode, strBarCode, byFacilityCode, uiProxCode, byUserType, byAccessType, strPassword, usSizeSample, byQuantitySamples, rgbyBiometrics);
        }

        private int EnviarFuncionario(Rede rede, string Cartao, string PIS, string Senha, string Nome, int Funcionario)
        {
            string strPIS = PIS;
            string strUserName = Nome;
            uint uiKeyCode = 0;
            string strBarCode = Cartao;
            byte byFacilityCode = 0;
            uint uiProxCode = 0;
            uint.TryParse(Cartao, out uiProxCode);
            byte byUserType = 0;
            string strPassword = Senha;

            int iReturn = rede.IdData_EnviarFuncionarioSemBiometria(strPIS,
                strUserName,
                uiKeyCode,
                strBarCode,
                byFacilityCode,
                uiProxCode,
                byUserType,
                strPassword,
                mmSobreporFuncionarios.Checked);

            return iReturn;
        }

        private void enviar_funcionarios_terminal(int Terminal)
        {
            DB db = new DB();
            SqlCommand comm = db.Conn.CreateCommand();

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            int NFuncs = Convert.ToInt32(db.GetFieldValue(
                                "SELECT FUNC_IND " +
                                "FROM Funcionarios " +
                                "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                                "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_PIS IS NOT NULL"
                                ));

            comm.CommandText = "SELECT FUNC_IND, FUNC_PIS, TRMF_ARMAZENADO_NO_TERMINAL " +
                               "FROM Funcionarios " +
                               "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                               "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_PIS IS NOT NULL " +
                               "ORDER BY FUNC_NOME";

            Rede rede = new Rede(this.objIDSysR30, IP, PORTA, edLog);
            rede.AddLog("ENVIANDO " + NFuncs.ToString() + " REGISTROS <ESC> PARA CANCELAR");
            if (!rede.Connection_Init()) return;

            CANCELAR = false;

            try
            {
                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    if (mmNaoEnviarFuncEnviados.Checked && Convert.ToInt32(reader["TRMF_ARMAZENADO_NO_TERMINAL"]) == 1)
                    {
                        rede.AddLog("PIS: " + reader["FUNC_PIS"].ToString() + " JÁ EXISTE NO TERMINAL");
                    }
                    else
                    {
                        EnviarCadastro(rede, Terminal, Convert.ToInt32(reader["FUNC_IND"].ToString()));
                    }

                    Application.DoEvents();

                    if (CANCELAR)
                    {
                        rede.AddLog("CANCELADO");
                        break;
                    }
                }

                reader.Close();
                atualizar_funcionarios_teminal();
            }
            finally
            {
                rede.Connection_Finalize();
            }
        }

        private void enviar_biometrias_terminal(int Terminal)
        {
            DB db = new DB();

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            int NFuncs = Convert.ToInt32(db.GetFieldValue(
                                "SELECT FUNC_IND " +
                                "FROM Funcionarios " +
                                "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                                "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_PIS IS NOT NULL"
                                ));

            SqlCommand comm = db.Conn.CreateCommand();
            comm.CommandText = "SELECT FUNC_IND " +
                               "FROM Funcionarios " +
                               "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                               "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_PIS IS NOT NULL " +
                               "ORDER BY FUNC_NOME";

            SqlDataReader reader = comm.ExecuteReader();

            Rede rede = new Rede(this.objIDSysR30, IP, PORTA, edLog);
            rede.AddLog("ENVIANDO " + NFuncs.ToString() + " REGISTROS <ESC> PARA CANCELAR");

            if (!rede.Connection_Init()) return;

            CANCELAR = false;
            try
            {
                while (reader.Read())
                {
                    enviar_biometrias_funcionario(rede, Convert.ToInt32(reader[0].ToString()));

                    Application.DoEvents();

                    if (CANCELAR)
                    {
                        rede.AddLog("CANCELADO");
                        break;
                    }
                }

                reader.Close();
            }

            finally
            {
                rede.Connection_Finalize();
            }
        }

        private void enviar_biometrias_funcionario(Rede rede, int Funcionario)
        {
            DB db = new DB();

            SqlCommand comm = db.Conn.CreateCommand();

            comm.CommandText = "SELECT FUNC_NOME, FUNC_PIS, FUNC_IND " +
                               "FROM Funcionarios " +
                               "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                               "WHERE FUNC_IND = " + Funcionario.ToString();

            SqlDataReader reader = comm.ExecuteReader();
            reader.Read();

            string diretoriobiometrias = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Asseponto\\Biometria";
            string arquivo;

            arquivo = diretoriobiometrias + "\\" + reader["FUNC_PIS"].ToString().Trim() + ".bio";

            rede.AddLog("ENVIANDO BIOMETRIA: " + reader["FUNC_NOME"].ToString().Trim());

            if (File.Exists(arquivo))
            {
                EnviarBiometria(
                            rede,
                            reader["FUNC_PIS"].ToString().Trim(),
                            reader["FUNC_NOME"].ToString().Trim(),
                            Convert.ToInt32(reader["FUNC_IND"].ToString()),
                            arquivo
                            );
            }
            else
            {
                rede.AddLog("ARQUIVO DE BIOMETRIA NÃO ENCONTRADO");
            }

            reader.Close();
        }

        public void RecebeTodasMarcacoesTCP(int Terminal, string IP, int Porta, string arquivo)
        {
            Rede rede = new Rede(this.objIDSysR30, IP, Porta, edLog);
            rede.IdData_RecebeMarcacoes(Terminal, arquivo, Grupo, this);
        }

        private void ExcluirTerminal(int Terminal)
        {
            DB db = new DB();
            SqlCommand Comm = db.Conn.CreateCommand();

            Comm.CommandText = "DELETE FROM Terminais WHERE TRM_IND = " + Terminal.ToString();

            Comm.ExecuteNonQuery();
            preencher_terminais();
        }

        private void excluirTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Excluir o terminal selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            ExcluirTerminal(Terminal);
        }

        private void listRelogios_DoubleClick(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            Abrir_Terminal(Terminal);
        }

        private void preencher_funcionarios(int Terminal)
        {
            ListViewItem item;
            DB db = new DB();
            SqlCommand comm = db.Conn.CreateCommand();

            comm.CommandText = "SELECT FUNC_NOME, CAR_DESCRICAO, CRA_NUMERO, FUNC_PIS, FUNC_IND, " +
                "dbo.fn_getdatetime(TRMF_ARMAZENADO_EM) AS TRMF_ARMAZENADO_EM " +
                "FROM Funcionarios " +
                "LEFT JOIN Cargos ON CAR_IND = FUNC_CARGO " +
                "LEFT JOIN Crachas ON CRA_FUNC = FUNC_IND " +
                "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_DT_DEM IS NULL AND FUNC_MARCADO_EXCLUSAO = 0 " +
                "ORDER BY FUNC_NOME";
            SqlDataReader reader = comm.ExecuteReader();

            listFuncionarios.Items.Clear();

            while (reader.Read())
            {
                item = new ListViewItem();
                item.Text = reader["FUNC_NOME"].ToString().Trim();
                item.SubItems.Add(reader["CAR_DESCRICAO"].ToString().Trim());
                item.SubItems.Add(reader["CRA_NUMERO"].ToString());
                item.SubItems.Add(reader["FUNC_PIS"].ToString().Trim());
                item.SubItems.Add(reader["FUNC_IND"].ToString());
                item.SubItems.Add(reader["TRMF_ARMAZENADO_EM"].ToString());
                listFuncionarios.Items.Add(item);
            }

            reader.Close();
        }

        private void listRelogios_Click(object sender, EventArgs e)
        {
            atualizar_funcionarios_teminal();
        }

        private void atualizar_funcionarios_teminal()
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            preencher_funcionarios(Terminal);
        }

        private void btAssociarMatricula_Click(object sender, EventArgs e)
        {
            string value = string.Empty;
            string Funcionario;

            DB db = new DB();

            if (InputBox.Show("Informe o número de matrícula", "Associar Matrícula", ref value) == DialogResult.OK)
            {
                Funcionario = listFuncionarios.Items[listFuncionarios.SelectedIndices[0]].SubItems[FUNC_SUBITEM_IND].Text;
                int ret = db.ExecuteCommand("INSERT INTO Crachas (CRA_FUNC, CRA_NUMERO, CRA_DATAINICIAL) VALUES (" + Funcionario + "," + value + ",'1/1/2000')");
                atualizar_funcionarios_teminal();
            }
        }

        private void cadastrarNovoTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir_Terminal(0);
        }

        private void capturarBiometriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Captura frmCaptura = new Captura(this.objIDSysR30);
            frmCaptura.rede = new Rede(edLog);
            frmCaptura.Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[0]].SubItems[FUNC_SUBITEM_IND].Text);
            frmCaptura.ShowDialog();
        }

        private void GetDadosTerminal(int Terminal, out string IP, out int Porta, out string TerminalNome)
        {
            DB db = new DB();

            IP = db.GetFieldValue("SELECT TRM_IP FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
            Porta = Convert.ToInt32(db.GetFieldValue("SELECT TRM_PORTA FROM Terminais WHERE TRM_IND = " + Terminal.ToString()));
            TerminalNome = db.GetFieldValue("SELECT TRM_DESCRICAO FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
        }

        private void importar_marcacoes_terminal(int Terminal)
        {
            DB db = new DB();

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            string diretoriomarcacoes = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Asseponto\\Marcacoes\\" + TERMINALNOME;
            string arquivo = diretoriomarcacoes + "\\" + String.Format("{0:yyyy MM dd}", DateTime.Now) + ".txt";

            RecebeTodasMarcacoesTCP(Terminal, IP, PORTA, arquivo);
        }

        private void terminalSelecionadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            importar_marcacoes_terminal(Terminal);
        }

        private void todosOsTerminaisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int Terminal;

            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                Terminal = Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text);
                importar_marcacoes_terminal(Terminal);
            }
        }

        private void terminalSelecionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            importar_marcacoes_terminal(Terminal);
        }

        private void todosOsTerminaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Terminal;

            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                Terminal = Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text);
                importar_marcacoes_terminal(Terminal);
            }
        }

        private void todosOsTerminaisToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                EnviaDataHora(
                                 listRelogios.Items[i].SubItems[TRM_SUBITEM_IP].Text,
                                 Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_PORTA].Text)
                                 );
            }
        }

        private void terminalSelecionadoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            EnviaDataHora(IP, PORTA);
        }

        private void todosOsTerminaisToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                EnviaInfoEmpresa(
                                 Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text),
                                 listRelogios.Items[i].SubItems[TRM_SUBITEM_IP].Text,
                                 Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_PORTA].Text)
                                 );
            }
        }

        private void terminalSelecionadoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);
            EnviaInfoEmpresa(Terminal, IP, PORTA);
        }

        private void todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            enviar_funcionarios_terminal(Terminal);
        }

        private void todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                enviar_funcionarios_terminal(Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text));
            }
        }

        private void EnviarCadastro(Rede rede, int Terminal, int Funcionario)
        {
            DB db = new DB();
            List<int> FuncionariosAtualizados = new List<int>();

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            string Pis;

            SqlCommand comm = db.Conn.CreateCommand();

            comm.CommandText = "SELECT FUNC_NOME, FUNC_PIS, CRA_NUMERO " +
                               "FROM Funcionarios " +
                               "LEFT JOIN Crachas ON CRA_FUNC = FUNC_IND " +
                               "WHERE FUNC_IND = " + Funcionario + " " +
                               "ORDER BY FUNC_NOME";
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                Pis = reader["FUNC_PIS"].ToString().Trim();

                if (Pis[0] == Convert.ToChar("0")) Pis = Pis.Substring(1);

                if (Pis != string.Empty)
                    if (EnviarFuncionario(
                                  rede,
                                  reader["CRA_NUMERO"].ToString().Trim(),
                                  Pis,
                                  string.Empty,
                                  reader["FUNC_NOME"].ToString().Trim(),
                                  Funcionario
                                  ) == 0)
                    {
                        FuncionariosAtualizados.Add(Funcionario);
                    }
            }

            reader.Close();

            db.Atualizar_TerminaisFuncionarios(Terminal, FuncionariosAtualizados);
        }

        private void funcionárioSelecionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listFuncionarios.SelectedIndices.Count == 0) return;

            DB db = new DB();

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            Rede rede = new Rede(this.objIDSysR30, IP, PORTA, edLog);
            rede.AddLog("ENVIANDO " + listFuncionarios.SelectedIndices.Count.ToString() + " REGISTROS <ESC> PARA CANCELAR");
            if (!rede.Connection_Init()) return;

            CANCELAR = false;

            try
            {
                for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
                {
                    string EnviadoEm = listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_ENVIADOEM].Text;
                    if (mmNaoEnviarFuncEnviados.Checked && EnviadoEm != String.Empty)
                    {
                        rede.AddLog("PIS: " + listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_PIS].Text + " JÁ EXISTE NO TERMINAL");
                    }
                    else
                    {
                        int Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_IND].Text);
                        EnviarCadastro(rede, Terminal, Funcionario);
                    }

                    Application.DoEvents();

                    if (CANCELAR)
                    {
                        rede.AddLog("CANCELADO");
                        break;
                    }
                }

                atualizar_funcionarios_teminal();
            }
            finally
            {
                rede.Connection_Finalize();
            }
        }

        private void todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            enviar_biometrias_terminal(Terminal);
        }

        private void todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                enviar_biometrias_terminal(Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text));
            }
        }

        private void funcionárioSelecionadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            DB db = new DB();

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            Rede rede = new Rede(this.objIDSysR30, IP, PORTA, edLog);  // apenas para o log
            rede.AddLog("ENVIANDO " + listFuncionarios.SelectedIndices.Count.ToString() + " REGISTROS <ESC> PARA CANCELAR");
            if (!rede.Connection_Init()) return;

            CANCELAR = false;

            try
            {
                for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
                {
                    int Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_IND].Text);
                    enviar_biometrias_funcionario(rede, Funcionario);

                    Application.DoEvents();

                    if (CANCELAR)
                    {
                        rede.AddLog("CANCELADO");
                        break;
                    }
                }
            }
            finally
            {
                rede.Connection_Finalize();
            }
        }

        private void Wait(int Segundos)
        {
            Stopwatch sw = new Stopwatch(); // sw cotructor
            sw.Start(); // starts the stopwatch
            for (int i = 0; ; i++)
            {
                if (i % 100000 == 0) // if in 100000th iteration (could be any other large number
                // depending on how often you want the time to be checked) 
                {
                    sw.Stop(); // stop the time measurement
                    if (sw.ElapsedMilliseconds > Segundos * 1000) // check if desired period of time has elapsed
                    {
                        break; // if more than 5000 milliseconds have passed, stop looping and return
                        // to the existing code
                    }
                    else
                    {
                        sw.Start(); // if less than 5000 milliseconds have elapsed, continue looping
                        // and resume time measurement
                    }
                }
            }

        }

        private void Principal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CANCELAR = true;
            }
        }

        private void definirPróximoNSRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            db.Terminal = Terminal;
            uint uNsr = db.LastNsr;

            string sNsr = uNsr.ToString();

            if (InputBox.Show("Informe o Último Nsr", "NSR", ref sNsr) == DialogResult.OK)
                db.LastNsr = Convert.ToUInt32(sNsr);
        }

        private string Get_Folder_Terminais_Config()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Asseponto\\Config\\";
        }

        private void Gerar_Usb_Terminal(int Terminal, bool Todos)
        {
            DB db = new DB();

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            string diretorioconfig = Get_Folder_Terminais_Config() + TERMINALNOME;
            Wr.Classes.Files.ForceDirectories(diretorioconfig);
            string arquivo = diretorioconfig + "\\USERS.ubs";

            Rede rede = new Rede(this.objIDSysR30, IP, PORTA, edLog);

            List<int> Funcionarios = new List<int>();

            if (Todos)
            {
                for (int i = 0; i <= listFuncionarios.Items.Count - 1; i++)
                {
                    string EnviadoEm = listFuncionarios.Items[i].SubItems[FUNC_SUBITEM_ENVIADOEM].Text;

                    if (!mmGerarArquivoUsbApenasNaoEnviados.Checked || EnviadoEm == String.Empty)
                    {
                        int Funcionario = Convert.ToInt32(listFuncionarios.Items[i].SubItems[FUNC_SUBITEM_IND].Text);
                        Funcionarios.Add(Funcionario);
                    }
                }
            }
            else
            {
                for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
                {
                    string EnviadoEm = listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_ENVIADOEM].Text;

                    if (!mmGerarArquivoUsbApenasNaoEnviados.Checked || EnviadoEm == String.Empty)
                    {
                        int Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_IND].Text);
                        Funcionarios.Add(Funcionario);
                    }
                }
            }

            rede.IdData_Criar_USBFile(Terminal, arquivo, Funcionarios);
            db.Atualizar_TerminaisFuncionarios(Terminal, Funcionarios);
        }

        private void todosOsTerminaisToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            //{
            //    int Terminal = Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text);

            //    Gerar_Usb_Terminal(Terminal);
            //}
        }

        private void excluirFuncionárioDoTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            Rede rede = new Rede(this.objIDSysR30, IP, PORTA, edLog);

            if (!rede.Connection_Init()) return;

            try
            {
                CANCELAR = false;

                for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
                {
                    string Pis = listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_PIS].Text;
                    int Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_IND].Text);
                    rede.IdData_ExcluirFuncionario(Pis);
                    db.Atualizar_TerminaisFuncionarios(Terminal, Funcionario, true);
                    Application.DoEvents();

                    if (CANCELAR)
                    {
                        rede.AddLog("CANCELADO");
                        break;
                    }
                }

                atualizar_funcionarios_teminal();
            }
            finally
            {
                rede.Connection_Finalize();
            }
        }

        private void sobreporFuncionárioSeExistirABiometriaSeráExcluídaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Assecont\\Asseponto4\\Opcoes", RegistryKeyPermissionCheck.ReadWriteSubTree);
            key.SetValue("Sobrepor funcionarios no terminal", mmSobreporFuncionarios.Checked);
            key.Close();
        }

        private void obterDadosDoFuncionárioNoTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            string Pis = listFuncionarios.Items[listFuncionarios.SelectedIndices[0]].SubItems[FUNC_SUBITEM_PIS].Text;

            Rede rede = new Rede(this.objIDSysR30, IP, PORTA, edLog);

            if (!rede.Connection_Init()) return;

            try
            {
                rede.IdData_Get_CrachaFuncionario(Pis);
            }
            finally
            {
                rede.Connection_Finalize();
            }
        }

        private void nãoEnviarSeJáFoiEnviadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Assecont\\Asseponto4\\Opcoes", RegistryKeyPermissionCheck.ReadWriteSubTree);
            key.SetValue("Nao enviar funcionarios para o terminal se ja foi enviado", mmNaoEnviarFuncEnviados.Checked);
            key.Close();
        }

        private void mmGerarArquivoUsbApenasSelecao_Click(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Assecont\\Asseponto4\\Opcoes", RegistryKeyPermissionCheck.ReadWriteSubTree);
            key.SetValue("Gerar no arquivo usb apenas os funcionarios nao enviados", mmGerarArquivoUsbApenasNaoEnviados.Checked);
            key.Close();
        }

        private void todosOsFuncionáriosDoTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            Gerar_Usb_Terminal(Terminal, true);
        }

        private void funcionáriosSelecionadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            Gerar_Usb_Terminal(Terminal, false);
        }

        private void abrirPastaDeConfiguraçõesDoTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Files.OpenFolderInExplorer(Get_Folder_Terminais_Config());
        }

        public void Monitoramento(string IP, int Porta)
        {
            Rede rede = new Rede(this.objIDSysR30, IP, Porta, edLog);
            //rede.IdData_Monitoramento(Rede.CodigoFuncoesMonitoramento.TotalPrinterTickets);
            //rede.IdData_Monitoramento(Rede.CodigoFuncoesMonitoramento.PrinterKM);
            //rede.IdData_Monitoramento(Rede.CodigoFuncoesMonitoramento.PaperStatus);
            //rede.IdData_Monitoramento(Rede.CodigoFuncoesMonitoramento.CurrentPaperRollSize);
            //rede.IdData_Monitoramento(Rede.CodigoFuncoesMonitoramento.CurrentPaperRollKM);
            //rede.IdData_Monitoramento(Rede.CodigoFuncoesMonitoramento.CurrentPaperRollTicketsPrinted);
            rede.IdData_Monitoramento(Rede.CodigoFuncoesMonitoramento.CurrentPaperRollEstimatedTickets);
            //rede.IdData_Monitoramento(Rede.CodigoFuncoesMonitoramento.Temperatura);
        }

        private void todosOsTerminaisToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                Monitoramento(
                                 listRelogios.Items[i].SubItems[TRM_SUBITEM_IP].Text,
                                 Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_PORTA].Text)
                                 );
            }
        }

        private void terminalSelecionadoToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            Monitoramento(IP, PORTA);
        }

        private void enviarHorarioDeVerãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        
        private void enviarDataEHoraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void terminalSelecionadoToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            
            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);
            
            HorarioDeVerao frmhorarioverao = new HorarioDeVerao();
            frmhorarioverao.ShowDialog();

            Rede rede = new Rede(this.objIDSysR30, IP, PORTA, edLog);
            rede.IdData_HorarioVerao(frmhorarioverao.Inicio, frmhorarioverao.Fim);
        }

        private void todosOsTerminaisToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            HorarioDeVerao frmhorarioverao = new HorarioDeVerao();
            frmhorarioverao.ShowDialog();

            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[i]].SubItems[TRM_SUBITEM_IND].Text);

                GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

                Rede rede = new Rede(this.objIDSysR30, IP, PORTA, edLog);
                rede.IdData_HorarioVerao(frmhorarioverao.Inicio, frmhorarioverao.Fim);
            }
        }
    }
}
