using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
//using System.Threading;
using System.IO;
using Suprema;
using Wr;

namespace Trix {
    public partial class Principal : Form {

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
        const int FUNC_SUBITEM_BARRAS = 6;
        const int FUNC_SUBITEM_PROXIMIDADE = 7;
        const int FUNC_SUBITEM_BIOMETRIA = 8;
        const int FUNC_SUBITEM_TECLADO = 9;
        const int FUNC_SUBITEM_SENHA = 10;
        const int FUNC_SUBITEM_SENHA_VALOR = 11;

        int Grupo;
        public string dirroot = "C:\\";
        bool expirouTimeout = false;

        public bool CANCELAR = false;

        public List<string> logStatus = new List<string>();

        private string IP;
        private int PORTA;
        private string TERMINALNOME;

        private string ULTIMOARQUIVOIMPORTADO = "";

        public Principal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e) {
            preencher_terminais();
            ler_grupo();
        }

        public string MontaMensagemEmpresa(string Identificador, string Nome, string CEI, string Localidade)
        {
            string mensagem = "";

            string tipoIdentificador = "1";

            string identificador = Identificador;
            for (int i = identificador.Length; i < 14; i++)
                identificador = identificador + " ";

            string cei = CEI;
            for (int i = cei.Length; i < 12; i++)
                cei = cei + " ";

            string razao = Nome;
            for (int i = razao.Length; i < 150; i++)
                razao = razao + " ";

            string local = Localidade;
            for (int i = local.Length; i < 100; i++)
                local = local + " ";

            mensagem = tipoIdentificador + identificador + cei + razao + local;

            return mensagem;
        }

        public string MontaMensagemFuncionario(string Cartao, string PIS, string Senha, string Nome, bool CodigoBarras, bool Proximidade, bool Biometria, bool Teclado)
        {
            /*const string CODIGOBARRAS_SIM = "1";
            const string CODIGOBARRAS_NAO = "0";
            const string PROXIMIDADE_SIM = "1";
            const string PROXIMIDADE_NAO = "0";
            const string BIOMETRIA_SIM = "1";
            const string BIOMETRIA_NAO = "0";
            const string TECLADO_SIM = "1";
            const string TECLADO_NAO = "0";
            const string SENHA_SIM = "1";
            const string SENHA_NAO = "0";
            const string BIOMETRIAMODO_11 = "0";*/
            const string BIOMETRIAMODO_1N = "1";

            string mensagem = "";

            string cartao = Cartao;
            for (int i = cartao.Length; i < 15; i++)
                cartao = "0" + cartao;

            string pis = PIS;
            for (int i = pis.Length; i < 12; i++)
                pis = "0" + pis;

            string nome = Nome;
            for (int i = nome.Length; i < 52; i++)
                nome = nome + " ";

            //string config = "011001";
            //string config = CODIGOBARRAS_NAO + PROXIMIDADE_SIM + BIOMETRIA_NAO + TECLADO_SIM + SENHA_SIM + BIOMETRIAMODO_1N;

            string config = (CodigoBarras ? "1" : "0") +
                            (Proximidade ? "1" : "0") +
                            (Biometria ? "1" : "0") +
                            (Teclado ? "1" : "0") +
                            (Senha != "" ? "1" : "0") +
                            BIOMETRIAMODO_1N;

            string senha = Senha;
            for (int i = senha.Length; i < 6; i++)
                senha = senha + " ";

            mensagem = cartao + pis + config + senha + nome;

            return mensagem;
        }

        public string MontaMensagemExclusaoFuncionario(string PIS)
        {
            string mensagem = "";

            string pis = PIS;
            for (int i = pis.Length; i < 12; i++)
                pis = "0" + pis;

            mensagem = pis + "1";

            return mensagem;
        }

        public Comando MontaComando(string mensagem, string tipoComando, string modo)
        {
            Comando cmd = new Comando(tipoComando, modo, mensagem);
            return cmd;
        }

        private string FormataCnpj(string Cnpj)
        {
            string res = "";

            for (int i = 0; i <= Cnpj.Length - 1; i++)
            {
                if (
                    (Cnpj[i] != Convert.ToChar(".")) && 
                    (Cnpj[i] != Convert.ToChar("/")) && 
                    (Cnpj[i] != Convert.ToChar("-")) && 
                    (Cnpj[i] != Convert.ToChar(" "))
                    )
                    res = res + Cnpj[i];
            }

            return res;
        }

        public void EnviaInfoEmpresa(int Terminal, string IP, int Porta)
        {
            string Identificador;
            string Nome;
            string Cei;
            string Endereco;
            byte IdentificadorTipo;

            DB db = new DB();

            db.LerEmpresa(Terminal, out IdentificadorTipo, out Identificador, out Nome, out Cei, out Endereco);

            string mensagem = MontaMensagemEmpresa(Identificador, Nome, Cei, Endereco);
            Comando cmd = MontaComando(mensagem, Protocolo.CMD_EMPRESA, Protocolo.SET);
            Rede rede = new Rede(IP, Porta, edLog);
            rede.EnviaComandoTCP(cmd);
        }

        private void preencher_terminais() {
            ListViewItem item;
            DB db = new DB();
            SqlCommand comm = db.Conn.CreateCommand();
            comm.CommandText = "SELECT TRM_DESCRICAO, TRM_IP, TRM_PORTA, TRM_IND FROM Terminais ORDER BY TRM_DESCRICAO";
            SqlDataReader reader = comm.ExecuteReader();

            listRelogios.Items.Clear();

            while (reader.Read()) {
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
            // Monta a data e hora
            string data = DateTime.Now.Date.ToString("ddMMyyyy");

            string hora = DateTime.Now.Hour.ToString();
            if (hora.Length < 2)
                hora = "0" + hora;
            string min = DateTime.Now.Minute.ToString();
            if (min.Length < 2)
                min = "0" + min;
            string sec = DateTime.Now.Second.ToString();
            if (sec.Length < 2)
                sec = "0" + sec;

            string horario = hora + min + sec;
            string msg = "";
            msg = data + horario;

            Comando cmd = new Comando(Protocolo.CMD_DATA_HORA, Protocolo.SET, msg);
            Rede rede = new Rede(IP, Porta, edLog);
            rede.EnviaComandoTCP(cmd);
        }

        private string ReadFile(string filePath)
        {
            byte[] buffer = new byte[384];

            using (FileStream file = new FileStream(filePath, FileMode.Open))
            {
                file.Read(buffer, 0, 384);
                file.Close();
            }

            return rStrings.ConvertByteToString(buffer);
        }
        //private static byte[] ReadFile(string filePath)
        //{
        //    byte[] buffer;
        //    FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        //    try
        //    {
        //        int length = (int)fileStream.Length;  // get file length
        //        buffer = new byte[length];            // create buffer
        //        int count;                            // actual number of bytes read
        //        int sum = 0;                          // total number of bytes read

        //        // read until Read method returns 0 (end of the stream has been reached)
        //        while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
        //            sum += count;  // sum is a buffer offset for next reading
        //    }
        //    finally
        //    {
        //        fileStream.Close();
        //    }
        //    return buffer;
        //}

        private void EnviarBiometria(int Terminal, string IP, int Porta, string PIS, string Nome, int Funcionario, string Arquivo)
        {
            string bio = ReadFile(Arquivo);
            string pis = PIS;
            for (int i = pis.Length; i < 12; i++)
                pis = "0" + pis;

            string msg = pis + "1" + bio;

            Comando cmd = new Comando(Protocolo.CMD_BIOMETRIA, Protocolo.SET, msg);
            Rede rede = new Rede(IP, Porta, edLog);
            if (rede.EnviaComandoTCP(cmd, Nome) == true)
            {
                DB db = new DB();
                db.ExecuteCommand(
                                  "UPDATE TerminaisFuncionarios " +
                                  "SET TRMF_BIOMETRIA_ENVIADA_EM = GETDATE() " +
                                  "WHERE TRMF_TERMINAL = " + Terminal.ToString() + " " +
                                  "AND TRMF_FUNC = " + Funcionario.ToString()
                                  );
            }
        }

        private void EnviarFuncionario(int Terminal, string IP, int Porta, string Cartao, string PIS, string Senha, string Nome, int Funcionario, bool CodigoBarras, bool Proximidade, bool Biometria, bool Teclado)
        {
            Rede rede = new Rede(IP, Porta, edLog);

            if (PIS == string.Empty)
            {
                rede.AddLog(String.Format("{0}: PIS NÃO INFORMADO", Nome));
                return;
            }

            string msg = MontaMensagemFuncionario(Cartao, PIS, Senha, Nome, CodigoBarras, Proximidade, Biometria, Teclado);
            Comando cmd = new Comando(Protocolo.CMD_FUNCIONARIO, Protocolo.SET, msg);
            DB db = new DB();

            if (rede.EnviaComandoTCP(cmd) == true)
            {
                db.ExecuteCommand(
                                  "UPDATE TerminaisFuncionarios " +
                                  "SET TRMF_ARMAZENADO_EM = GETDATE() " +
                                  "WHERE TRMF_TERMINAL = " + Terminal.ToString() + " " +
                                  "AND TRMF_FUNC = " + Funcionario.ToString()
                                  );               
            }
        }

        private void ExcluirFuncionario(int Terminal, string IP, int Porta, string PIS, string Nome, int Funcionario)
        {
            string msg = MontaMensagemExclusaoFuncionario(PIS);
            Comando cmd = new Comando(Protocolo.CMD_EXCLUSAO, Protocolo.SET, msg);
            Rede rede = new Rede(IP, Porta, edLog);
            rede.AddLog("EXCLUINDO " + Nome);
            rede.EnviaComandoTCP(cmd);

            DB db = new DB();
            db.ExecuteCommand(
                              "UPDATE TerminaisFuncionarios " +
                              "SET TRMF_ARMAZENADO_EM = NULL " +
                              "WHERE TRMF_TERMINAL = " + Terminal.ToString() + " " +
                              "AND TRMF_FUNC = " + Funcionario.ToString()
                              );
        }

        private void enviar_funcionarios_terminal(int Terminal)
        {
            DB db = new DB();
            SqlCommand comm = db.Conn.CreateCommand();

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            comm.CommandText = "SELECT FUNC_NOME, FUNC_PIS, FUNC_IND, CRA_NUMERO, " +
                               "(CASE WHEN TRMF_CODIGOBARRAS IS NOT NULL THEN TRMF_CODIGOBARRAS ELSE 0 END) AS TRMF_CODIGOBARRAS, " +
                               "(CASE WHEN TRMF_PROXIMIDADE IS NOT NULL THEN TRMF_PROXIMIDADE ELSE 0 END) AS TRMF_PROXIMIDADE, " +
                               "(CASE WHEN TRMF_BIOMETRIA IS NOT NULL THEN TRMF_BIOMETRIA ELSE 0 END) AS TRMF_BIOMETRIA, " +
                               "(CASE WHEN TRMF_TECLADO IS NOT NULL THEN TRMF_TECLADO ELSE 0 END) AS TRMF_TECLADO, " +
                               "TRMF_TECLADO_PASSWORD " +
                               "FROM Funcionarios " +
                               "LEFT JOIN Cargos ON CAR_IND = FUNC_CARGO " +
                               "LEFT JOIN Crachas ON CRA_FUNC = FUNC_IND " +
                               "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                               "WHERE TRMF_TERMINAL = " + Terminal + " " +
                               "ORDER BY FUNC_NOME";
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                EnviarFuncionario(
                                Terminal,
                                IP, PORTA,
                                reader["CRA_NUMERO"].ToString(),
                                reader["FUNC_PIS"].ToString().Trim(),
                                reader["TRMF_TECLADO_PASSWORD"].ToString().Trim(),
                                reader["FUNC_NOME"].ToString().Trim(),
                                Convert.ToInt32(reader["FUNC_IND"]),
                                Convert.ToBoolean(reader["TRMF_CODIGOBARRAS"]),
                                Convert.ToBoolean(reader["TRMF_PROXIMIDADE"]),
                                Convert.ToBoolean(reader["TRMF_BIOMETRIA"]),
                                Convert.ToBoolean(reader["TRMF_TECLADO"])
                                );
            }

            reader.Close();
        }

        private void enviar_biometrias_terminal(int Terminal)
        {
            DB db = new DB();

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            Rede rede = new Rede(IP, PORTA, edLog);  // apenas para o log

            SqlCommand comm = db.Conn.CreateCommand();

            comm.CommandText = "SELECT FUNC_NOME, FUNC_PIS, FUNC_IND " +
                               "FROM Funcionarios " +
                               "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                               "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_PIS IS NOT NULL " +
                               "ORDER BY FUNC_NOME";

            SqlDataReader reader = comm.ExecuteReader();

            string diretoriobiometrias = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Asseponto\\Biometria";
            string arquivo;

            while (reader.Read())
            {
                arquivo = diretoriobiometrias + "\\" + reader["FUNC_PIS"].ToString().Trim() + "-1.bio";

                rede.AddLog("ENVIANDO BIOMETRIA: " + reader["FUNC_NOME"].ToString().Trim());

                if (File.Exists(arquivo))
                {
                    EnviarBiometria(
                                Terminal, IP, PORTA,
                                reader["FUNC_PIS"].ToString().Trim(),
                                "",
                                Convert.ToInt32(reader["FUNC_IND"].ToString()),
                                arquivo
                                );
                }
                else
                {
                    rede.AddLog("ARQUIVO DE BIOMETRIA NÃO ENCONTRADO");
                }
            }
            reader.Close();
        }

        private void enviar_biometrias_funcionario(int Terminal, int Funcionario)
        {
            DB db = new DB();
            SqlCommand comm = db.Conn.CreateCommand();
            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            Rede rede = new Rede(IP, PORTA, edLog);  // apenas para o log

            comm.CommandText = "SELECT FUNC_NOME, FUNC_PIS, FUNC_IND " +
                               "FROM Funcionarios " +
                               "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                               "WHERE FUNC_IND = " + Funcionario.ToString();

            SqlDataReader reader = comm.ExecuteReader();
            reader.Read();

            string diretoriobiometrias = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Asseponto\\Biometria";
            string arquivo;

            arquivo = diretoriobiometrias + "\\" + reader["FUNC_PIS"].ToString().Trim() + "-1.bio";

            rede.AddLog("ENVIANDO BIOMETRIA: " + reader["FUNC_NOME"].ToString().Trim());

            if (File.Exists(arquivo))
            {
                EnviarBiometria(
                            Terminal, IP, PORTA,
                            reader["FUNC_PIS"].ToString().Trim(),
                            "",
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

        private void FinalizarImportacao(string arquivo, List<string> marcacoes, Rede rede)
        {
            using (StreamWriter file = new StreamWriter(arquivo))
                foreach (string s in marcacoes)
                {
                    file.WriteLine(s);
                }

            rede.AddLog("MARCAÇÕES SALVAS EM " + arquivo);
        }

        private void ExcluirTerminal(int Terminal)
        {
            DB db = new DB();
            db.ExecuteCommand("DELETE FROM Terminais WHERE TRM_IND = " + Terminal.ToString());
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
                               "dbo.fn_getdatetime(TRMF_ARMAZENADO_EM) AS TRMF_ARMAZENADO_EM, " +
                               "(CASE WHEN TRMF_CODIGOBARRAS = 1 THEN 'Sim' ELSE 'Não' END) AS TRMF_CODIGOBARRAS, " +
                               "(CASE WHEN TRMF_PROXIMIDADE = 1 THEN 'Sim' ELSE 'Não' END) AS TRMF_PROXIMIDADE, " +
                               "(CASE WHEN TRMF_BIOMETRIA = 1 THEN 'Sim' ELSE 'Não' END) AS TRMF_BIOMETRIA, " +
                               "(CASE WHEN TRMF_TECLADO = 1 THEN 'Sim' ELSE 'Não' END) AS TRMF_TECLADO, " +
                               "(CASE WHEN TRMF_TECLADO_PASSWORD IS NOT NULL THEN 'Sim' ELSE 'Não' END) AS TRMF_TECLADO_PASSWORD_TEM, " +
                               "TRMF_TECLADO_PASSWORD " +
                               "FROM Funcionarios " +
                               "LEFT JOIN Cargos ON CAR_IND = FUNC_CARGO " +
                               "LEFT JOIN Crachas ON CRA_FUNC = FUNC_IND " +
                               "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                               "WHERE TRMF_TERMINAL = " + Terminal + " " +
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

                item.SubItems.Add(reader["TRMF_CODIGOBARRAS"].ToString());
                item.SubItems.Add(reader["TRMF_PROXIMIDADE"].ToString());
                item.SubItems.Add(reader["TRMF_BIOMETRIA"].ToString());
                item.SubItems.Add(reader["TRMF_TECLADO"].ToString());
                item.SubItems.Add(reader["TRMF_TECLADO_PASSWORD_TEM"].ToString());
                item.SubItems.Add(reader["TRMF_TECLADO_PASSWORD"].ToString().Trim());
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

            if (InputBox.Show("Informe o número de matrícula", "Associar Matrícula", ref value) == DialogResult.OK)
             {
                 DB db = new DB();
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
            Captura frmCaptura = new Captura();
            frmCaptura.Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[0]].SubItems[FUNC_SUBITEM_IND].Text);
            frmCaptura.ShowDialog();
        }

        private void importar_marcacoes_terminal(int Terminal)
        {
            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            string diretoriomarcacoes = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Asseponto\\Marcacoes\\" + TERMINALNOME;
            string arquivo = diretoriomarcacoes + "\\" + String.Format("{0:yyyy MM dd}", DateTime.Now) + ".txt";

            bool ExpirouTimeout = false;
            CANCELAR = false;

            Rede rede = new Rede(IP, PORTA, edLog);

            for (int y = 1; y <= 1; y++)
            {
                rede.RecebeMarcacoesTCP(Terminal, arquivo, Grupo, ref ExpirouTimeout, this);
                ULTIMOARQUIVOIMPORTADO = arquivo;
                mmUltimoArquivoImportado.Visible = true;

                if (ExpirouTimeout)
                {
                    rede.AddLog("RECONECTANDO APÓS TIMEOUT  [TENTATIVA " + y.ToString() + "]");

                    for (int i = 5; i > 0; i--)
                    {
                        if (i == 1)
                            rede.AddLog("CONECTANDO EM 1 SEGUNDO");
                        else
                            rede.AddLog("CONECTANDO EM " + i.ToString() + " SEGUNDOS");

                        Application.DoEvents();
                        rSystem.PauseForSeconds(1);

                        if (CANCELAR)
                        {
                            rede.AddLog("RECONEXÃO CANCELADA");
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }

                if (CANCELAR) break;
            }
        }

        private void terminalSelecionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            importar_marcacoes_terminal(Terminal);
        }

        private void todosOsTerminaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Terminal;

            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                //Thread.Sleep(500);
                Terminal = Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text);
                importar_marcacoes_terminal(Terminal);
            }
        }

        private void todosOsTerminaisToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                //Thread.Sleep(500);
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
                //Thread.Sleep(500);
                EnviaInfoEmpresa(
                                 Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text),
                                 listRelogios.Items[i].SubItems[TRM_SUBITEM_IP].Text,
                                 Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_PORTA].Text)
                                 );
            }
        }

        private void terminalSelecionadoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            EnviaInfoEmpresa(Terminal, IP, PORTA);
        }

        private void todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            enviar_funcionarios_terminal(Terminal);
            atualizar_funcionarios_teminal();
        }

        private void todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                //Thread.Sleep(500);
                enviar_funcionarios_terminal(Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text));
            }

            atualizar_funcionarios_teminal();
        }

        private void funcionárioSelecionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
            {
                string Nome = listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_NOME].Text;
                string Pis = listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_PIS].Text;
                string Cracha = listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_CRACHA].Text;
                int Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_IND].Text);

                bool CodigoBarras = rStrings.ConvertStringPtToBool(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_BARRAS].Text);
                bool Proximidade = rStrings.ConvertStringPtToBool(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_PROXIMIDADE].Text);
                bool Biometria = rStrings.ConvertStringPtToBool(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_BIOMETRIA].Text);
                bool Teclado = rStrings.ConvertStringPtToBool(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_TECLADO].Text);
                string Senha = listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_SENHA_VALOR].Text;

                EnviarFuncionario(Terminal, IP, PORTA, Cracha, Pis, Senha, Nome, Funcionario, CodigoBarras, Proximidade, Biometria, Teclado);
            }

            atualizar_funcionarios_teminal();
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
                //Thread.Sleep(500);
                enviar_biometrias_terminal(Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text));
            }
        }

        private void funcionárioSelecionadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            int Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[0]].SubItems[FUNC_SUBITEM_IND].Text);

            enviar_biometrias_funcionario(Terminal, Funcionario);
        }

        private void Principal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CANCELAR = true;
            }
        }

        private void timeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rSystem.PauseForSeconds(35);
        }

        private void excluirFuncionárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            GetDadosTerminal(Terminal, out IP, out PORTA, out TERMINALNOME);

            for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
            {
                string Nome = listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_NOME].Text;
                string Pis = listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_PIS].Text;
                int Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_IND].Text);

                if (Pis != string.Empty)
                    ExcluirFuncionario(Terminal, IP, PORTA, Pis, Nome, Funcionario);
            }

            atualizar_funcionarios_teminal();
        }

        private void GetDadosTerminal(int Terminal, out string IP, out int Porta, out string TerminalNome)
        {
            DB db = new DB();

            IP = db.GetFieldValue("SELECT TRM_IP FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
            Porta = Convert.ToInt32(db.GetFieldValue("SELECT TRM_PORTA FROM Terminais WHERE TRM_IND = " + Terminal.ToString()));
            TerminalNome = db.GetFieldValue("SELECT TRM_DESCRICAO FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
        }

        private void definirPróximoNSRToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DB db = new DB();

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            db.Terminal = Terminal;
            int uNsr = db.LastNsr;

            string sNsr = uNsr.ToString();

            if (InputBox.Show("Informe o Último Nsr", "NSR", ref sNsr) == DialogResult.OK)
                db.LastNsr = Convert.ToInt32(sNsr);
        }

        private void mmUltimoArquivoImportado_Click(object sender, EventArgs e)
        {
            if (ULTIMOARQUIVOIMPORTADO == string.Empty) return;

            System.Diagnostics.Process.Start("Notepad.exe", ULTIMOARQUIVOIMPORTADO);
        }

        private void biometriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] Campos = {"TRMF_CODIGOBARRAS", "TRMF_PROXIMIDADE", "TRMF_BIOMETRIA", "TRMF_TECLADO"};

            int IndexComando = Convert.ToInt32(((ToolStripMenuItem)sender).Tag);
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            DB db = new DB();

            for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
            {
                int Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_IND].Text);
                bool Valor = !Convert.ToBoolean(Convert.ToInt32(db.GetFieldValue(String.Format("SELECT {0} FROM TerminaisFuncionarios WHERE TRMF_TERMINAL = {1} AND TRMF_FUNC = {2}", Campos[IndexComando], Terminal, Funcionario), "0")));
                listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_BARRAS + IndexComando].Text = Valor ? "Sim" : "Não";

                db.ExecuteCommand(String.Format("UPDATE TerminaisFuncionarios SET {0} = {1} WHERE TRMF_TERMINAL = {2} AND TRMF_FUNC = {3}", Campos[IndexComando], Convert.ToInt32(Valor), Terminal, Funcionario));
                //char[] config = db.GetStringConfigFuncionario(Terminal, Funcionario).ToCharArray();

                //config[IndexComando] = Convert.ToChar(valor);
            }
        }

        private void definirSenhaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            string Senha = "";

            DB db = new DB();
            if (InputBox.Show("Informe a senha", "Associar Senha", ref Senha) != DialogResult.OK) return;
    
            for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
            {
                int Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_IND].Text);
                listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_SENHA].Text = Senha != ""?"Sim":"Não";
                listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_SENHA_VALOR].Text = Senha;
                db.ExecuteCommand(String.Format("UPDATE TerminaisFuncionarios SET TRMF_TECLADO_PASSWORD = {0} WHERE TRMF_TERMINAL = {1} AND TRMF_FUNC = {2}", Senha != "" ? "'" + Senha + "'" : "NULL", Terminal, Funcionario));
            }
        }
    }
}
