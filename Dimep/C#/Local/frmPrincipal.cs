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
using Wr.Classes;
using Microsoft.Win32;
using AssepontoRep;

namespace Dimep 
{
    public partial class Principal : Form 
    {
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
            abrirÚltimoArquivoDeImportaçãoToolStripMenuItem.Visible = (Utilitarios.getArquivoImportacao(Consts.TERMINALNOME, Registro) != String.Empty);
        }

        private void preencher_terminais() 
        {
            ListViewItem item;
            //DBApp db = new DBApp();
            //SqlCommand comm = db.Conn.CreateCommand();

            //comm.CommandText = "SELECT TRM_DESCRICAO, TRM_IP, TRM_PORTA, TRM_IND FROM Terminais ORDER BY TRM_DESCRICAO";
            //SqlDataReader reader = comm.ExecuteReader();

            DBApp db = new DBApp();
            SqlDataReader reader = db.getReader("SELECT TRM_DESCRICAO, TRM_IP, TRM_PORTA, TRM_IND FROM Terminais ORDER BY TRM_DESCRICAO");
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
            db.closeConnection();
        }

        private void ler_grupo()
        {
            DBApp db = new DBApp();
            Grupo = Convert.ToInt32(db.getFieldValueInteger("SELECT TOP 1 GRU_IND FROM Grupos"));
        }

        private void Abrir_Terminal(int Terminal)
        {
            RepEdicao frmRepEdicao = new RepEdicao(Grupo, Terminal, Consts.PORTAPADRAO);

            if (frmRepEdicao.ShowDialog() == DialogResult.OK)
                preencher_terminais();
        }

        //private void processar_marcacoes(int Terminal, string Arquivo)
        //{
        //    Marcacoes marcacoes = new Marcacoes(Arquivo);
        //    DBApp db = new DBApp();
        //    Log log = new Log(edLog);
        //    db.ProcessarMarcacoes(Grupo, marcacoes.listMarcacoes, log);
        //}

        private void ExcluirTerminal(int Terminal)
        {
            DBApp db = new DBApp();
            db.executeCommand(String.Format("DELETE FROM Terminais WHERE TRM_IND = {0}", Terminal));
            preencher_terminais();
        }

        private void excluirTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            if (MessageBox.Show("Excluir o terminal selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);
            ExcluirTerminal(Terminal);
        }

        private void listRelogios_DoubleClick(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);
            Abrir_Terminal(Terminal);
        }

        private void preencher_funcionarios(int Terminal)
        {
            ListViewItem item;
            DBApp db = new DBApp();
            //SqlCommand comm = db.Conn.CreateCommand();

            //comm.CommandText = "SELECT FUNC_NOME, CAR_DESCRICAO, CRA_NUMERO, FUNC_PIS, FUNC_IND, " +
            //    "dbo.fn_getdatetime(TRMF_ARMAZENADO_EM) AS TRMF_ARMAZENADO_EM " +
            //    "FROM Funcionarios " +
            //    "LEFT JOIN Cargos ON CAR_IND = FUNC_CARGO " +
            //    "LEFT JOIN Crachas ON CRA_FUNC = FUNC_IND " +
            //    "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
            //    "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_DT_DEM IS NULL AND FUNC_MARCADO_EXCLUSAO = 0 " +
            //    "ORDER BY FUNC_NOME";
            //SqlDataReader reader = comm.ExecuteReader();

            SqlDataReader reader = db.getReader("SELECT FUNC_NOME, CAR_DESCRICAO, CRA_NUMERO, FUNC_PIS, FUNC_IND, " +
                "dbo.fn_getdatetime(TRMF_ARMAZENADO_EM) AS TRMF_ARMAZENADO_EM " +
                "FROM Funcionarios " +
                "LEFT JOIN Cargos ON CAR_IND = FUNC_CARGO " +
                "LEFT JOIN Crachas ON CRA_FUNC = FUNC_IND " +
                "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_DT_DEM IS NULL AND FUNC_MARCADO_EXCLUSAO = 0 " +
                "ORDER BY FUNC_NOME");

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
            db.closeConnection();
        }

        private void listRelogios_Click(object sender, EventArgs e)
        {
            atualizar_funcionarios_teminal();
        }

        private void atualizar_funcionarios_teminal()
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);

            preencher_funcionarios(Terminal);
        }

        private void cadastrarNovoTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Abrir_Terminal(0);
        }

        private void terminalSelecionadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);

            Bridge.ImportarMarcacoesTerminal(Terminal, edLog);
        }

        private void todosOsTerminaisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                int Terminal = Convert.ToInt32(listRelogios.Items[i].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);
                Bridge.ImportarMarcacoesTerminal(Terminal, edLog);
            }
        }

        private void todosOsTerminaisToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                int Terminal = Convert.ToInt32(listRelogios.Items[i].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);
                Bridge.EnviaDataHora(Terminal, edLog);
            }
        }

        private void terminalSelecionadoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);

            Bridge.EnviaDataHora(Terminal, edLog);
        }

        private void todosOsTerminaisToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                Bridge.EnviaInfoEmpresa(Convert.ToInt32(listRelogios.Items[i].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text), edLog);
            }
        }

        private void terminalSelecionadoToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);

            Bridge.EnviaInfoEmpresa(Terminal, edLog);
        }

        private void todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listRelogios.SelectedIndices.Count == 0) return;

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);

            Bridge.EnviarFuncionariosTerminal(Terminal, this, edLog);
            atualizar_funcionarios_teminal();
        }

        private void todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= listRelogios.Items.Count - 1; i++)
            {
                int Terminal = Convert.ToInt32(listRelogios.Items[i].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);
                Bridge.EnviarFuncionariosTerminal(Terminal, this, edLog);
            }

            atualizar_funcionarios_teminal();
        }

        //private void EnviarCadastro(DBApp db, Rede rede, int Terminal, int Funcionario)
        //{
        //    string TecladoPassword = "";
        //    string Cracha = "";

        //    List<int> FuncionariosAtualizados = new List<int>();

        //    SqlDataAdapter da = new SqlDataAdapter("SELECT FUNC_NOME, FUNC_PIS, CRA_NUMERO, TRMF_TECLADO, " +
        //                       "TRMF_TECLADO_PASSWORD, TRMF_PROXIMIDADE, TRMF_CODIGOBARRAS " +
        //                       "FROM Funcionarios " +
        //                       "LEFT JOIN Crachas ON CRA_FUNC = FUNC_IND " +
        //                       "LEFT JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
        //                       "WHERE FUNC_IND = " + Funcionario + " " +
        //                       "ORDER BY FUNC_NOME", db.Conn);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds);
            
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        if (dr["TRMF_TECLADO_PASSWORD"] != DBNull.Value)
        //        {
        //            TecladoPassword = Convert.ToBoolean(dr["TRMF_TECLADO"]) ? dr["TRMF_TECLADO_PASSWORD"].ToString().Trim() : "";
        //        }
        //        else
        //        {
        //            TecladoPassword = "";
        //        }

        //        if (dr["CRA_NUMERO"] != DBNull.Value)
        //        {
        //            Cracha = dr["CRA_NUMERO"].ToString().Trim();
        //        }
        //        else
        //        {
        //            Cracha = "";
        //        }

        //        rede.log.AddLog(string.Format(Consts.FUNCIONARIO_ENVIANDO, dr["FUNC_NOME"].ToString().Trim()));

        //        if (dr["FUNC_PIS"].ToString().Trim() != string.Empty)
        //            if (rede.Gertec_EnviaFuncionario(dr["FUNC_NOME"].ToString().Trim(), dr["FUNC_PIS"].ToString().Trim(), Cracha, Cracha, ""))
        //            {
        //                FuncionariosAtualizados.Add(Funcionario);
        //            }
        //    }

        //    db.Atualizar_TerminaisFuncionarios(Terminal, FuncionariosAtualizados);
        //}

        private void funcionárioSelecionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);

            if (Bridge.EnviarFuncionariosSelecao(Terminal, this, listFuncionarios, edLog))
                atualizar_funcionarios_teminal();

            //if (listFuncionarios.SelectedIndices.Count == 0) return;

            //Types.Terminal TerminalDados;

            //DBApp db = new DBApp();

            //int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[TRM_SUBITEM_IND].Text);
            //db.getDadosTerminal(Terminal, out TerminalDados);

            //Rede rede = new Rede(TerminalDados.IP, edLog);

            //if (listFuncionarios.SelectedIndices.Count > 1)
            //{
            //    rede.log.AddLog(String.Format(Consts.FUNCIONARIOS_ENVIANDO, listFuncionarios.SelectedIndices.Count));
            //}

            //CANCELAR = false;

            //for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
            //{
            //    int index = listFuncionarios.SelectedIndices[i];
            //    string EnviadoEm = listFuncionarios.Items[index].SubItems[FUNC_SUBITEM_ENVIADOEM].Text;

            //    if (EnviadoEm != String.Empty)
            //    {
            //        rede.log.AddLog(String.Format(Consts.PIS_JA_EXISTE, listFuncionarios.Items[index].SubItems[FUNC_SUBITEM_PIS].Text));
            //    }
            //    else
            //    {
            //        int Funcionario = Convert.ToInt32(listFuncionarios.Items[index].SubItems[FUNC_SUBITEM_IND].Text);
            //        EnviarCadastro(db, rede, Terminal, Funcionario);
            //    }

            //    Application.DoEvents();

            //    if (CANCELAR)
            //    {
            //        rede.log.AddLog(Consts.CANCELADO);
            //        break;
            //    }
            //}

            //rede.log.LogOk();
            //atualizar_funcionarios_teminal();
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
                int Funcionario = Convert.ToInt32(listFuncionarios.Items[index].SubItems[AssepontoRep.Consts.FUNC_SUBITEM_IND].Text);
                FuncionariosSelecionados.Add(Funcionario);
            }

            DBApp db = new DBApp();

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);
            db.MarcarFuncionariosComoEnviados(Terminal, FuncionariosSelecionados);
            atualizar_funcionarios_teminal();
        }

        private void excluirFuncionárioDoTerminalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);

            if (Bridge.ExcluirFuncionariosSelecao(Terminal, listFuncionarios, edLog))
            {
                atualizar_funcionarios_teminal();
            }
        }

        private void abrirÚltimoArquivoDeImportaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Arquivo = Utilitarios.getArquivoImportacao(Consts.TERMINALNOME, Registro);

            if (Arquivo == String.Empty) return;

            Files.ExecuteFile(Arquivo);
        }

        private void definirPróximoNSRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBApp db = new DBApp();

            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);

            int uNsr = db.getLastNsr(Terminal);

            string sNsr = uNsr.ToString();

            if (InputBox.Show("Informe o Último Nsr", "NSR", ref sNsr) == DialogResult.OK)
                db.setLastNsr(Terminal, Convert.ToInt32(sNsr));
        }

        private void enviarConfiguraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Terminal = Convert.ToInt32(listRelogios.Items[listRelogios.SelectedIndices[0]].SubItems[AssepontoRep.Consts.TRM_SUBITEM_IND].Text);

            Bridge.EnviaConfiguracao(Terminal, edLog);
        }
    }
}
