using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using AssepontoRep;
using Wr.Classes;

namespace Dimep
{
    public class Bridge
    {
        static List<int> FuncionariosAtualizados = new List<int>();

        public static void EnviaInfoEmpresa(int Terminal, TextBox edLog)
        {
            Types.Terminal TerminalDados;
            Types.Empregador EmpregadorDados;

            DBApp db = new DBApp();
            db.getDadosTerminal(Terminal, out TerminalDados);

            db.LerEmpresa(Terminal, out EmpregadorDados);

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, edLog);

            if (TerminalDados.SupervisorPis == "" || TerminalDados.SupervisorCodigo == 0 || TerminalDados.SupervisorSenha == 0)
            {
                rede.log.AddLog(AssepontoRep.Consts.DADOS_SUPERVISOR_NAO_DEFINIDOS);
            }
            else
            {
                if (rede.Dimep_EnviaEmpresa(EmpregadorDados.PessoaTipo, EmpregadorDados.Pessoa, EmpregadorDados.Nome, EmpregadorDados.Cei, EmpregadorDados.Endereco))
                {
                    rede.Dimep_InsereFuncionarioMasterListaEnvio(TerminalDados.SupervisorPis, TerminalDados.SupervisorCodigo.ToString(), TerminalDados.SupervisorSenha.ToString());
                    rede.Dimep_EnviaFuncionariosMasterListaEnvio();
                }
            }
        }

        public static void EnviaDataHora(int Terminal, TextBox edLog)
        {
            Types.Terminal TerminalDados;

            DBApp db = new DBApp();
            db.getDadosTerminal(Terminal, out TerminalDados);

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, edLog);
            rede.Dimep_AtualizaDataHora();
        }

        public static bool EnviarFuncionariosTerminal(int Terminal, Principal form, TextBox edLog)
        {
            bool Result = false;

            Types.Terminal TerminalDados;

            DBApp db = new DBApp();
            SqlCommand comm = db.Conn.CreateCommand();

            db.getDadosTerminal(Terminal, out TerminalDados);

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, edLog);
            if (!rede.Dimep_Conectar()) return Result;

            FuncionariosAtualizados.Clear();
            form.CANCELAR = false;

            SqlDataAdapter da = new SqlDataAdapter("SELECT FUNC_IND, FUNC_PIS, TRMF_ARMAZENADO_NO_TERMINAL " +
                               "FROM Funcionarios " +
                               "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                               "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_PIS IS NOT NULL " +
                               "ORDER BY FUNC_NOME", db.Conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            rede.log.AddLog(String.Format(AssepontoRep.Consts.FUNCIONARIOS_ENVIANDO, ds.Tables[0].Rows.Count));

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(dr["TRMF_ARMAZENADO_NO_TERMINAL"]) == 1)
                {
                    rede.log.AddLog(String.Format(AssepontoRep.Consts.PIS_JA_ENVIADO, dr["FUNC_PIS"].ToString()));
                }
                else
                {
                    if (EnviarFuncionario(db, rede, Terminal, Convert.ToInt32(dr["FUNC_IND"].ToString())))
                    {
                        Result = true;
                    }
                }

                Application.DoEvents();

                if (form.CANCELAR)
                {
                    rede.log.AddLog(AssepontoRep.Consts.CANCELADO);
                    break;
                }
            }

            if (Result)
            {
                if (rede.Dimep_EnviaFuncionariosListaEnvio())
                {
                    db.Atualizar_TerminaisFuncionarios(Terminal, FuncionariosAtualizados);
                }
            }

            rede.Dimep_Desconectar();
            return Result;
        }

        public static bool EnviarFuncionariosSelecao(int Terminal, Principal form, ListView listFuncionarios, TextBox edLog)
        {
            if (listFuncionarios.SelectedIndices.Count == 0) return false;

            bool Result = false;

            Types.Terminal TerminalDados;

            DBApp db = new DBApp();

            db.getDadosTerminal(Terminal, out TerminalDados);

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, edLog);
            if (!rede.Dimep_Conectar()) return Result;

            if (listFuncionarios.SelectedIndices.Count > 1)
            {
                rede.log.AddLog(String.Format(AssepontoRep.Consts.FUNCIONARIOS_ENVIANDO, listFuncionarios.SelectedIndices.Count));
            }

            FuncionariosAtualizados.Clear();

            form.CANCELAR = false;

            for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
            {
                int index = listFuncionarios.SelectedIndices[i];
                string EnviadoEm = listFuncionarios.Items[index].SubItems[AssepontoRep.Consts.FUNC_SUBITEM_ENVIADOEM].Text;

                if (EnviadoEm != String.Empty)
                {
                    rede.log.AddLog(String.Format(AssepontoRep.Consts.PIS_JA_ENVIADO, listFuncionarios.Items[index].SubItems[AssepontoRep.Consts.FUNC_SUBITEM_PIS].Text));
                }
                else
                {
                    int Funcionario = Convert.ToInt32(listFuncionarios.Items[index].SubItems[AssepontoRep.Consts.FUNC_SUBITEM_IND].Text);

                    if (EnviarFuncionario(db, rede, Terminal, Funcionario))
                    {
                        Result = true;
                    }
                }

                Application.DoEvents();

                if (form.CANCELAR)
                {
                    rede.log.AddLog(AssepontoRep.Consts.CANCELADO);
                    break;
                }
            }

            if (Result)
            {
                if (rede.Dimep_EnviaFuncionariosListaEnvio())
                {
                    db.Atualizar_TerminaisFuncionarios(Terminal, FuncionariosAtualizados);
                }
            }

            rede.Dimep_Desconectar();
            return Result;
        }

        static bool EnviarFuncionario(DBApp db, Rede rede, int Terminal, int Funcionario)
        {
            bool Result = false;

            //string TecladoPassword = "";
            string Cracha = "";
            string Pis = "";

            SqlDataAdapter da = new SqlDataAdapter("SELECT FUNC_NOME, FUNC_PIS, CRA_NUMERO, TRMF_TECLADO, " +
                               "TRMF_TECLADO_PASSWORD, TRMF_PROXIMIDADE, TRMF_CODIGOBARRAS " +
                               "FROM Funcionarios " +
                               "LEFT JOIN Crachas ON CRA_FUNC = FUNC_IND " +
                               "LEFT JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                               "WHERE FUNC_IND = " + Funcionario + " AND TRMF_TERMINAL = " + Terminal + " " +
                               "ORDER BY FUNC_NOME", db.Conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["CRA_NUMERO"] != DBNull.Value)
                {
                    Cracha = dr["CRA_NUMERO"].ToString().Trim();
                }
                else
                {
                    Cracha = "";
                }

                Pis = dr["FUNC_PIS"].ToString().Trim();

                if (Pis[0] == Convert.ToChar("0")) Pis = Pis.Substring(1);

                if (Pis != string.Empty)
                {
                    rede.Dimep_InsereFuncionarioListaEnvio(dr["FUNC_NOME"].ToString().Trim(), Pis, Cracha);
                    FuncionariosAtualizados.Add(Funcionario);
                    Result = true;
                }
            }

            return Result;
        }

        public static bool ExcluirFuncionariosSelecao(int Terminal, ListView listFuncionarios, TextBox edLog)
        {
            bool Result = false;

            Types.Terminal TerminalDados;

            DBApp db = new DBApp();

            db.getDadosTerminal(Terminal, out TerminalDados);

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, edLog);
            if (!rede.Dimep_Conectar()) return Result;

            if (listFuncionarios.SelectedIndices.Count > 1)
            {
                rede.log.AddLog(String.Format(AssepontoRep.Consts.FUNCIONARIOS_EXCLUINDO, listFuncionarios.SelectedIndices.Count));
            }

            for (int i = 0; i <= listFuncionarios.SelectedIndices.Count - 1; i++)
            {
                int index = listFuncionarios.SelectedIndices[i];
                string Nome = listFuncionarios.Items[index].SubItems[AssepontoRep.Consts.FUNC_SUBITEM_NOME].Text;
                string Pis = listFuncionarios.Items[index].SubItems[AssepontoRep.Consts.FUNC_SUBITEM_PIS].Text;
                int Funcionario = Convert.ToInt32(listFuncionarios.Items[index].SubItems[AssepontoRep.Consts.FUNC_SUBITEM_IND].Text);

                if (rede.Dimep_ExcluirFuncionario(Nome, Pis))
                {
                    db.MarcarFuncionarioComoNaoEnviado(Terminal, Funcionario);
                    Result = true;
                }
            }

            rede.Dimep_Desconectar();
            return Result;
        }

        public static void ImportarMarcacoesTerminal(int Terminal, TextBox edLog)
        {
            Types.Terminal TerminalDados;

            DBApp db = new DBApp();
            WinRegistry Registro = new WinRegistry("Assecont", "Asseponto4");

            db.getDadosTerminal(Terminal, out TerminalDados);

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, edLog);

            string Arquivo = Folders.folderMarcacoes(TerminalDados.Descricao) + String.Format("{0:yyyy MM dd hhmm}", DateTime.Now) + ".txt";
            Marcacoes marcacoes = new Marcacoes(Arquivo);

            if (rede.Dimep_ImportarMarcacoes(Terminal, TerminalDados.Descricao, marcacoes))
            {
                Utilitarios.setArquivoImportacao(Consts.TERMINALNOME, Registro, Arquivo);
                db.ProcessarMarcacoes(TerminalDados.Grupo, marcacoes, rede.log);
            }
        }

        public static void EnviaConfiguracao(int Terminal, TextBox edLog)
        {
            Types.Terminal TerminalDados;

            DBApp db = new DBApp();
            db.getDadosTerminal(Terminal, out TerminalDados);

            Rede rede = new Rede(TerminalDados.IP, TerminalDados.Porta, edLog);
            rede.Dimep_EnviaConfiguracao();
        }
    }
}
