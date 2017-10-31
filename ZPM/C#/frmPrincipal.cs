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
using System.Diagnostics;
using Wr;
using Microsoft.Win32;

namespace Zpm 
{
    public partial class Principal : Form 
    {
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

        public List<string> logStatus = new List<string>();

        WinRegistry Registro = new WinRegistry("Assecont", "Asseponto4");

        public Principal() 
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            preencher_terminais();
            ler_grupo();
            abrirÚltimoArquivoDeImportaçãoToolStripMenuItem.Visible = (Utilitarios.getArquivoImportacao(Registro) != String.Empty);
        }

        public void EnviaInfoEmpresa(int Terminal)
        {
            Types.Terminal TerminalDados;
            Types.Empregador EmpregadorDados;

            DBApp db = new DBApp();
            db.getDadosTerminal(Terminal, out TerminalDados);
            db.LerEmpresa(Terminal, out EmpregadorDados);

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, TerminalDados.Serial, edLog);
            rede.sendEmpregador(EmpregadorDados.PessoaTipo, EmpregadorDados.Pessoa, EmpregadorDados.Nome, EmpregadorDados.Cei, EmpregadorDados.Endereco);
        }

        public void EnviaDataHora(int Terminal)
        {
            Types.Terminal TerminalDados;

            DBApp db = new DBApp();
            db.getDadosTerminal(Terminal, out TerminalDados);

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, TerminalDados.Serial, edLog);
            rede.sendDataHora();
        }

        private void preencher_terminais() 
        {
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

        private void enviar_funcionarios_terminal(int Terminal)
        {
            Types.Terminal TerminalDados;

            DBApp db = new DBApp();
            SqlCommand comm = db.Conn.CreateCommand();

            db.getDadosTerminal(Terminal, out TerminalDados);

            //comm.CommandText = "SELECT FUNC_IND, FUNC_PIS, TRMF_ARMAZENADO_NO_TERMINAL " +
            //                   "FROM Funcionarios " +
            //                   "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
            //                   "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_PIS IS NOT NULL " +
            //                   "ORDER BY FUNC_NOME";

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, TerminalDados.Serial, edLog);

            CANCELAR = false;

            //DataSet ds = db.Select("SELECT FUNC_IND, FUNC_PIS, TRMF_ARMAZENADO_NO_TERMINAL " +
            //                   "FROM Funcionarios " +
            //                   "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
            //                   "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_PIS IS NOT NULL " +
            //                   "ORDER BY FUNC_NOME");

            SqlDataAdapter da = new SqlDataAdapter("SELECT FUNC_IND, FUNC_PIS, TRMF_ARMAZENADO_NO_TERMINAL " +
                               "FROM Funcionarios " +
                               "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                               "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_PIS IS NOT NULL " +
                               "ORDER BY FUNC_NOME", db.Conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            rede.log.AddLog(String.Format(Consts.FUNCIONARIOS_ENVIANDO, ds.Tables[0].Rows.Count));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(dr["TRMF_ARMAZENADO_NO_TERMINAL"]) == 1)
                {
                    rede.log.AddLog(String.Format(Consts.PIS_JA_EXISTE, dr["FUNC_PIS"].ToString()));
                }
                else
                {
                    EnviarCadastro(db, rede, Terminal, Convert.ToInt32(dr["FUNC_IND"].ToString()));
                }

                Application.DoEvents();

                if (CANCELAR)
                {
                    rede.log.AddLog(Consts.CANCELADO);
                    break;
                }
            }

            if (Terminal == Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text));
                preencher_funcionarios(Terminal);

            rede.log.LogOk();
        }

        private void importar_marcacoes_terminal(int Terminal, DateTime DataInicial, DateTime DataFinal)
        {
            Types.Terminal TerminalDados;

            DBApp db = new DBApp();

            db.getDadosTerminal(Terminal, out TerminalDados);

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, TerminalDados.Serial, edLog);

            string arquivo = Folders.folderMarcacoes(TerminalDados.Descricao) + String.Format("{0:yyyy MM dd hhmm}", DateTime.Now) + ".txt";

            if (rede.getArquivoMarcacoes(arquivo, Grupo, DataInicial, DataFinal))
            {
                Utilitarios.setArquivoImportacao(Registro, arquivo);
                abrirÚltimoArquivoDeImportaçãoToolStripMenuItem.Visible = (Utilitarios.getArquivoImportacao(Registro) != String.Empty);
                processar_marcacoes(Terminal, arquivo);
            }
        }

        private void processar_marcacoes(int Terminal, string Arquivo)
        {
            Marcacoes marcacoes = new Marcacoes(Arquivo);
            DBApp db = new DBApp();
            Log log = new Log(edLog);
            db.ProcessarMarcacoes(Grupo, marcacoes.listMarcacoes, log);
        }

        private void ExcluirTerminal(int Terminal)
        {
            DBApp db = new DBApp();
            db.ExecuteCommand(String.Format("DELETE FROM Terminais WHERE TRM_IND = {0}", Terminal));
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
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            preencher_funcionarios(Terminal);
        }

        private void cadastrarNovoTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir_Terminal(0);
        }

        private void terminalSelecionadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            DateTime DataInicial;
            DateTime DataFinal;
            if (!getPeriodo(out DataInicial, out DataFinal)) return;

            importar_marcacoes_terminal(Terminal, DataInicial, DataFinal);
        }

        private void todosOsTerminaisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int Terminal;
            
            DateTime DataInicial;
            DateTime DataFinal;
            if (!getPeriodo(out DataInicial, out DataFinal)) return;

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
                int Terminal = Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text);
                EnviaDataHora(Terminal);
            }
        }

        private void terminalSelecionadoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            EnviaDataHora(Terminal);
        }

        private void todosOsTerminaisToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                EnviaInfoEmpresa(Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text));
            }
        }

        private void terminalSelecionadoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);

            EnviaInfoEmpresa(Terminal);
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
                int Terminal = Convert.ToInt32(listRelogios.Items[i].SubItems[TRM_SUBITEM_IND].Text);
                enviar_funcionarios_terminal(Terminal);
            }
        }

        private void EnviarCadastro(DBApp db, Rede rede, int Terminal, int Funcionario)
        {
            string TecladoPassword = "";
            string Cracha = "";

            List<int> FuncionariosAtualizados = new List<int>();

            SqlDataAdapter da = new SqlDataAdapter("SELECT FUNC_NOME, FUNC_PIS, CRA_NUMERO, TRMF_TECLADO, " +
                               "TRMF_TECLADO_PASSWORD, TRMF_PROXIMIDADE, TRMF_CODIGOBARRAS " +
                               "FROM Funcionarios " +
                               "LEFT JOIN Crachas ON CRA_FUNC = FUNC_IND " +
                               "LEFT JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                               "WHERE FUNC_IND = " + Funcionario + " " +
                               "ORDER BY FUNC_NOME", db.Conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["TRMF_TECLADO_PASSWORD"] != DBNull.Value)
                {
                    TecladoPassword = Convert.ToBoolean(dr["TRMF_TECLADO"]) ? dr["TRMF_TECLADO_PASSWORD"].ToString().Trim() : "";
                }
                else
                {
                    TecladoPassword = "";
                }

                if (dr["CRA_NUMERO"] != DBNull.Value)
                {
                    Cracha = dr["CRA_NUMERO"].ToString().Trim();
                }
                else
                {
                    Cracha = "";
                }

                rede.log.AddLog(string.Format(Consts.FUNCIONARIO_ENVIANDO, dr["FUNC_NOME"].ToString().Trim()));

                if (dr["FUNC_PIS"].ToString().Trim() != string.Empty)
                    if (rede.sendFuncionario(
                            dr["FUNC_PIS"].ToString().Trim(),
                            dr["FUNC_NOME"].ToString().Trim(),
                            Cracha,
                            Convert.ToBoolean(dr["TRMF_TECLADO"]),
                            TecladoPassword,
                            Cracha,
                            Cracha, 
                            Cracha)
                        )
                    {
                        FuncionariosAtualizados.Add(Funcionario);
                    }
            }

            db.Atualizar_TerminaisFuncionarios(Terminal, FuncionariosAtualizados);
        }

        private void funcionárioSelecionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listFuncionarios.SelectedIndices.Count == 0) return;

            Types.Terminal TerminalDados;

            DBApp db = new DBApp();

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            db.getDadosTerminal(Terminal, out TerminalDados);

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, TerminalDados.Serial, edLog);

            if (listFuncionarios.SelectedIndices.Count > 1)
            {
                rede.log.AddLog(String.Format(Consts.FUNCIONARIOS_ENVIANDO, listFuncionarios.SelectedIndices.Count));
            }

            CANCELAR = false;

            for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
            {
                int index = listFuncionarios.SelectedIndices[i];
                string EnviadoEm = listFuncionarios.Items[index].SubItems[FUNC_SUBITEM_ENVIADOEM].Text;

                if (EnviadoEm != String.Empty)
                {
                    rede.log.AddLog(String.Format(Consts.PIS_JA_EXISTE, listFuncionarios.Items[index].SubItems[FUNC_SUBITEM_PIS].Text));
                }
                else
                {
                    int Funcionario = Convert.ToInt32(listFuncionarios.Items[index].SubItems[FUNC_SUBITEM_IND].Text);
                    EnviarCadastro(db, rede, Terminal, Funcionario);
                }

                Application.DoEvents();

                if (CANCELAR)
                {
                    rede.log.AddLog(Consts.CANCELADO);
                    break;
                }
            }

            rede.log.LogOk();
            atualizar_funcionarios_teminal();
        }

        private void Principal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CANCELAR = true;
            }
        }

        private string Get_Folder_Terminais_Config()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Asseponto\\Config\\";
        }

        private bool getPeriodo(out DateTime DataInicial, out DateTime DataFinal)
        {
            bool Result = false;

            ImportacaoMarcacoes frmImportacaoMarcacoes = new ImportacaoMarcacoes();

            DataInicial = Convert.ToDateTime(Registro.getValue(Consts.TERMINALNOME, "Data inicial importacao", DateTime.Today.ToShortDateString()));
            DataFinal = Convert.ToDateTime(Registro.getValue(Consts.TERMINALNOME, "Data final importacao", DateTime.Today.ToShortDateString())); ;

            frmImportacaoMarcacoes.edDataInicial.Value = DataInicial;
            frmImportacaoMarcacoes.edDataFinal.Value = DataFinal;

            if (frmImportacaoMarcacoes.ShowDialog() == DialogResult.OK)
            {
                DataInicial = frmImportacaoMarcacoes.edDataInicial.Value;
                DataFinal = frmImportacaoMarcacoes.edDataFinal.Value;

                Registro.setValue(Consts.TERMINALNOME, "Data inicial importacao", DataInicial.ToShortDateString());
                Registro.setValue(Consts.TERMINALNOME, "Data final importacao", DataFinal.ToShortDateString());
                Result = true;
            }

            return Result;
        }

        private void marcarSelecionadosComoNãoEnviadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarcarSelecionadosComoNaoEnviados();
        }

        private void MarcarSelecionadosComoNaoEnviados()
        {
            List<int> FuncionariosSelecionados = new List<int>();
            if (listFuncionarios.SelectedIndices.Count == 0) return;

            for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
            {
                int index = listFuncionarios.SelectedIndices[i];
                int Funcionario = Convert.ToInt32(listFuncionarios.Items[index].SubItems[FUNC_SUBITEM_IND].Text);
                FuncionariosSelecionados.Add(Funcionario);
            }

            DBApp db = new DBApp();

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            db.MarcarFuncionariosComoEnviados(Terminal, FuncionariosSelecionados);
            atualizar_funcionarios_teminal();
        }

        private void excluirFuncionárioDoTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Types.Terminal TerminalDados;

            DBApp db = new DBApp();
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            db.getDadosTerminal(Terminal, out TerminalDados);

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, TerminalDados.Serial, edLog);

            if (listFuncionarios.SelectedIndices.Count > 1)
            {
                rede.log.AddLog(String.Format(Consts.FUNCIONARIOS_EXCLUINDO, listFuncionarios.SelectedIndices.Count));
            }

            for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
            {
                int index = listFuncionarios.SelectedIndices[i];
                string Pis = listFuncionarios.Items[index].SubItems[FUNC_SUBITEM_PIS].Text;
                int Funcionario = Convert.ToInt32(listFuncionarios.Items[index].SubItems[FUNC_SUBITEM_IND].Text);

                if (rede.deleteFuncionario(Pis))
                {
                    db.MarcarFuncionarioComoNaoEnviado(Terminal, Funcionario);
                }

            atualizar_funcionarios_teminal();
            }
        }

        private void abrirÚltimoArquivoDeImportaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Arquivo = Utilitarios.getArquivoImportacao(Registro);

            if (Arquivo == String.Empty) return;

            Files.ExecuteFile(Arquivo);
        }
    }
}
