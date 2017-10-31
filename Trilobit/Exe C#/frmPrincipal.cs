using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace Trilobit 
{
    public partial class Principal : Form 
    {
        const int TRM_SUBITEM_DESCRICAO = 0;
        const int TRM_SUBITEM_IP = 1;
        const int TRM_SUBITEM_PORTA = 2;
        const int TRM_SUBITEM_IND = 3;
        const int TRM_SUBITEM_SENHA = 4;

        const int FUNC_SUBITEM_NOME = 0;
        const int FUNC_SUBITEM_FUNCAO = 1;
        const int FUNC_SUBITEM_CRACHA = 2;
        const int FUNC_SUBITEM_PIS = 3;
        const int FUNC_SUBITEM_IND = 4;

        const int TIPOIDENTIFICADOR_CNPJ = 1;
        const int TIPOIDENTIFICADOR_CPF = 0;

        int Grupo;
        public string dirroot = "C:\\";

        public List<string> logStatus = new List<string>();

        public Principal() 
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            preencher_terminais();
            ler_grupo();
        }

        private void LerEmpresa(int Terminal, out string CNPJ, out string CPF, out string Nome, out ulong Cei, out string Endereco)
        {
            DB db = new DB();

            SqlCommand comm = db.Conn.CreateCommand();
            comm.CommandText = "SELECT TOP 1 TRM_CNPJ, TRM_CPF, TRM_RAZAOSOCIAL, TRM_ENDERECO FROM Terminais WHERE TRM_IND = " + Terminal.ToString();
            SqlDataReader reader = comm.ExecuteReader();
            reader.Read();
            CNPJ = FormataCnpj(reader["TRM_CNPJ"].ToString().Trim());
            CPF = FormataCnpj(reader["TRM_CPF"].ToString().Trim());
            Nome = reader["TRM_RAZAOSOCIAL"].ToString().Trim();
            Cei = 0;
            Endereco = reader["TRM_ENDERECO"].ToString().Trim();
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

        public void EnviaInfoEmpresa(int Terminal, string IP, int Porta, int Senha)
        {
            string CNPJ;
            string CPF;
            string Nome;
            ulong Cei;
            string Endereco;

            try
            {
                this.LerEmpresa(Terminal, out CNPJ, out CPF, out Nome, out Cei, out Endereco);

                Rede rede = new Rede(IP, Porta, edLog, Senha);

                rede.Trilobit_EnviaEmpresa(CNPJ, CPF, Nome, Cei, Endereco);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void preencher_terminais() 
        {
            ListViewItem item;
            DB db = new DB();
            SqlCommand comm = db.Conn.CreateCommand();

            comm.CommandText = "SELECT TRM_DESCRICAO, TRM_IP, TRM_PORTA, TRM_IND, TRM_SENHA FROM Terminais ORDER BY TRM_DESCRICAO";
            SqlDataReader reader = comm.ExecuteReader();

            listRelogios.Items.Clear();

            while (reader.Read()) {
                item = new ListViewItem();
                item.Text = reader["TRM_DESCRICAO"].ToString().Trim();
                item.SubItems.Add(reader["TRM_IP"].ToString().Trim());
                item.SubItems.Add(reader["TRM_PORTA"].ToString().Trim());
                item.SubItems.Add(reader["TRM_IND"].ToString());
                item.SubItems.Add(reader["TRM_SENHA"].ToString());
                listRelogios.Items.Add(item);
            }
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

        public void EnviaDataHora(string IP, int Porta, int Senha)
        {
            Rede rede = new Rede(IP, Porta, edLog, Senha);
            rede.Trilobit_EnviaDataHora();
        }

        private void EnviarFuncionario(int Terminal, string IP, int Porta, int Senha, string Cartao, string PIS, string Nome, int Funcionario)
        {
            Rede rede = new Rede(IP, Porta, edLog, Senha);
            rede.Trilobit_EnviarFuncionario(PIS, Nome, Cartao);
        }

        private void ExcluirFuncionario(string IP, int Porta, int Senha, string PIS)
        {
            Rede rede = new Rede(IP, Porta, edLog, Senha);
            rede.Trilobit_ExcluirFuncionario(PIS);
        }

        private void enviar_funcionarios_terminal(int Terminal)
        {
            DB db = new DB();
            SqlCommand comm = db.Conn.CreateCommand();
         
            comm.CommandText = "SELECT FUNC_IND " +
                               "FROM Funcionarios " +
                               "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                               "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_PIS IS NOT NULL AND FUNC_MARCADO_EXCLUSAO = 0 " +
                               "ORDER BY FUNC_NOME";

            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                EnviarCadastro(Terminal, Convert.ToInt32(reader[0].ToString()));
            }
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
            if (listRelogios.SelectedIndices.Count == 0) return; 
            
            if (MessageBox.Show("Excluir o terminal selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            ExcluirTerminal(Terminal);
        }

        private void listRelogios_DoubleClick(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            Abrir_Terminal(Terminal);
        }

        private void preencher_funcionarios(int Terminal)
        {
            ListViewItem item;
            DB db = new DB();
            SqlCommand comm = db.Conn.CreateCommand();

            comm.CommandText = "SELECT FUNC_NOME, CAR_DESCRICAO, CRA_NUMERO, FUNC_PIS, FUNC_IND " +
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
                listFuncionarios.Items.Add(item);
            }
        }

        private void listRelogios_Click(object sender, EventArgs e)
        {
            atualizar_funcionarios_teminal();
        }

        private void atualizar_funcionarios_teminal()
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

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

        private void GetDadosTerminal(int Terminal, out string IP, out int Porta, out string TerminalNome, out int Senha, out int CartaoDigitos)
        {
            DB db = new DB();

            IP = db.GetFieldValue("SELECT TRM_IP FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
            Porta = Convert.ToInt32(db.GetFieldValue("SELECT TRM_PORTA FROM Terminais WHERE TRM_IND = " + Terminal.ToString()));
            TerminalNome = db.GetFieldValue("SELECT TRM_DESCRICAO FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
            Senha = db.GetSenhaTerminal(Terminal);
            CartaoDigitos = Convert.ToInt32(db.GetFieldValue("SELECT TRM_CARTAO_DIGITOS FROM Terminais WHERE TRM_IND = " + Terminal.ToString()));
        }

        private void terminalSelecionadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            DateTime DataInicial;
            DateTime DataFinal;
            if (!GetDatasSelecao(out DataInicial, out DataFinal)) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            importar_marcacoes_terminal(Terminal, DataInicial, DataFinal);
        }

        private bool GetDatasSelecao(out DateTime DataInicial, out DateTime DataFinal)
        {
            DataInicial = rDateTime.GetFirstDayOfMonth(DateTime.Now);
            DataFinal = rDateTime.GetLastDayOfMonth(DateTime.Now);

            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Assecont\\Asseponto4\\Opcoes");

            var sDataInicial = (string)key.GetValue("Trilobit importacao data inicial");
            var sDataFinal = (string)key.GetValue("Trilobit importacao data final");

            if (String.IsNullOrEmpty(sDataInicial)) sDataInicial = DataInicial.ToString();
            if (String.IsNullOrEmpty(sDataFinal)) sDataFinal = DataFinal.ToString();

            key.Close();

            DataSelecao frmDataSelecao = new DataSelecao();

            try
            {
                frmDataSelecao.edDataInicial.Value = Convert.ToDateTime(sDataInicial);
            }
            catch
            {
            }

            try
            {
                frmDataSelecao.edDataFinal.Value = Convert.ToDateTime(sDataFinal);
            }
            catch
            {
            }

            if (frmDataSelecao.ShowDialog() == DialogResult.OK)
            {
                if (frmDataSelecao.cbImportarTudo.Checked)
                {
                    DataInicial = Convert.ToDateTime("1/1/2010");
                    DataFinal = Convert.ToDateTime("31/12/2050");
                }
                else
                {
                    DataInicial = frmDataSelecao.edDataInicial.Value;
                    DataFinal = frmDataSelecao.edDataFinal.Value;
                }

                return (true);
            }
            else
                return (false);
        }

        private void todosOsTerminaisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DateTime DataInicial;
            DateTime DataFinal;
            if (!GetDatasSelecao(out DataInicial, out DataFinal)) return;

            int Terminal;

            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                Terminal = Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text);
                importar_marcacoes_terminal(Terminal, DataInicial, DataFinal);
            }
        }

        private void todosOsTerminaisToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                EnviaDataHora(
                              listRelogios.Items[i].SubItems[TRM_SUBITEM_IP].Text,
                              Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_PORTA].Text),
                              Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_SENHA].Text)
                              );
            }
        }

        private void terminalSelecionadoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            string IP;
            int Porta;
            string TerminalNome;
            int Senha;
            int CartaoDigitos;

            GetDadosTerminal(Terminal, out IP, out Porta, out TerminalNome, out Senha, out CartaoDigitos);

            EnviaDataHora(IP, Porta, Senha);
        }

        private void todosOsTerminaisToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                EnviaInfoEmpresa(
                                 Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text),
                                 listRelogios.Items[i].SubItems[TRM_SUBITEM_IP].Text,
                                 Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_PORTA].Text),
                                 Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_SENHA].Text)
                                 );
            }
        }

        private void terminalSelecionadoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            string IP;
            int Porta;
            string TerminalNome;
            int Senha;
            int CartaoDigitos;

            GetDadosTerminal(Terminal, out IP, out Porta, out TerminalNome, out Senha, out CartaoDigitos);
            //string IP = DB.GetFieldValue("SELECT TRM_IP FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
            //int Porta = Convert.ToInt32(DB.GetFieldValue("SELECT TRM_PORTA FROM Terminais WHERE TRM_IND = " + Terminal.ToString()));

            EnviaInfoEmpresa(Terminal, IP, Porta, Senha);
        }

        private void todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

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

        private void EnviarCadastro(int Terminal, int Funcionario)
        {
            DB db = new DB();

            string IP;
            int Porta;
            string TerminalNome;
            int Senha;
            int CartaoDigitos;

            GetDadosTerminal(Terminal, out IP, out Porta, out TerminalNome, out Senha, out CartaoDigitos);

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
                    EnviarFuncionario(
                                  Terminal,
                                  IP, Porta, Senha,
                                  reader["CRA_NUMERO"].ToString().PadLeft(CartaoDigitos, '0'),
                                  Pis,
                                  reader["FUNC_NOME"].ToString().Trim(),
                                  Convert.ToInt32(Funcionario)
                                  );
            }
        }

        private void funcionárioSelecionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
            {
                int Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_IND].Text);
                EnviarCadastro(Terminal, Funcionario);
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

        private void empresaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void excluirFuncionárioDoTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
            {
                int Funcionario = Convert.ToInt32(listFuncionarios.Items[listFuncionarios.SelectedIndices[i]].SubItems[FUNC_SUBITEM_IND].Text);
                ExcluirCadastro(Terminal, Funcionario);
            }
        }

        private void ExcluirCadastro(int Terminal, int Funcionario)
        {
            string IP;
            int Porta;
            string TerminalNome;
            int Senha;
            int CartaoDigitos;

            DB db = new DB();

            GetDadosTerminal(Terminal, out IP, out Porta, out TerminalNome, out Senha, out CartaoDigitos);

            Rede rede = new Rede(IP, Porta, edLog, Senha);
            rede.Trilobit_ExcluirFuncionario(db.GetPis(Funcionario));
        }


        private void funcionárioSelecionadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void importar_marcacoes_terminal(int Terminal, DateTime DataInicial, DateTime DataFinal)
        {
            string IP;
            int Porta;
            string TerminalNome;
            int Senha;
            int CartaoDigitos;

            DB db = new DB();

            GetDadosTerminal(Terminal, out IP, out Porta, out TerminalNome, out Senha, out CartaoDigitos);

            string diretoriomarcacoes = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Asseponto\\Marcacoes\\" + TerminalNome;
            if (!Directory.Exists(diretoriomarcacoes))
                Directory.CreateDirectory(diretoriomarcacoes);

            string arquivo = diretoriomarcacoes + "\\" + String.Format("{0:yyyy MM dd}", DateTime.Now) + ".txt";

            Rede rede = new Rede(IP, Porta, edLog, Senha);
            rede.Trilobit_RecebeMarcacoes(arquivo, Grupo, DataInicial, DataFinal);
        }

        private void abrirÚltimoArquivoImportadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            string Arquivo = db.LerConfiguracao(Grupo, "CFG_ULTIMA_IMPORTACAO_ARQUIVO");

            Rede rede = new Rede(edLog);
            if (String.IsNullOrEmpty(Arquivo))
            {
                rede.AddLog("NÃO HOUVE IMPORTAÇÕES ANTERIORES");
                return;
            }

            if (!File.Exists(Arquivo))
            {
                rede.AddLog("ARQUIVO NÃO LOCALIZADO: " + Arquivo);
                return;
            }

            System.Diagnostics.Process.Start("Notepad.exe", Arquivo);
        }

        private void lerConfiguraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int Terminal;

            //for (int i = 0; i <= listRelogios.SelectedIndices.Count - 1; i++)
            //{
            //    Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[i]].SubItems[TRM_SUBITEM_IND].Text);
            //    lerconfiguracao(Terminal);
            //}            
        }

        private void lerconfiguracao(int Terminal)
        {
            //string IP;
            //int Porta;
            //string TerminalNome;
            //int Senha;
            //bool TemBiometria;

            //GetDadosTerminal(Terminal, out IP, out Porta, out TerminalNome, out Senha);

            //DB db = new DB();
            //TemBiometria = Convert.ToBoolean(db.GetFieldValue("SELECT TRM_BIOMETRIA FROM Terminais WHERE TRM_IND = " + Terminal.ToString()));

            //Rede rede = new Rede(IP, Porta, edLog, Senha);
            //rede.LerConfiguracao(TemBiometria);
        }

        private void enviarHorarioDeVerãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            string IP;
            int Porta;
            string TerminalNome;
            int Senha;
            int CartaoDigitos;

            string[] data = new string[] { "20/10/2013", "16/02/2013"};
            int[] parametro = new int[] { 106, 107 };

            GetDadosTerminal(Terminal, out IP, out Porta, out TerminalNome, out Senha, out CartaoDigitos);
            Rede rede = new Rede(IP, Porta, edLog, Senha);

            for (int i = 0; i < 2; i++)
            {
                rede.HorarioDeVerao(IP, Porta, Senha, data[i], parametro[i]);
            }
        }
    }
}
