using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Wr.Classes;
using Microsoft.Win32;

namespace Gertec
{
    class DBApp : Wr.Classes.BD
    {
        public DBApp()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Assecont\\Asseponto4\\Login");

            string server = Strings.CryptString((string)key.GetValue("S"), -2);
            string database = Strings.CryptString((string)key.GetValue("D"), -2);
            string user = Strings.CryptString((string)key.GetValue("U"), -3);
            string pwd = Strings.CryptString((string)key.GetValue("P"), -4);

            key.Close();

            base.Conectar(server, database, user, pwd);
        }

        public int getLastNsr(int Terminal)
        {
            return Convert.ToInt32(getFieldValueInteger("SELECT TRM_ULTIMO_NSR FROM Terminais WHERE TRM_IND = " + Terminal.ToString()));
        }

        public void setLastNsr(int Terminal, int Nsr)
        {
            executeCommand("UPDATE Terminais SET TRM_ULTIMO_NSR = " + Nsr + " WHERE TRM_IND = " + Terminal);
        }

        public void LerEmpresa(int Terminal, out Types.Empregador empregador)
        {
            SqlDataReader reader = getReader("SELECT TOP 1 (CASE WHEN ISNULL(TRM_CNPJ, '') <> '' THEN 'J' ELSE 'F' END) AS PESSOATIPO, " +
                               "TRM_CNPJ, TRM_CPF, TRM_RAZAOSOCIAL, TRM_ENDERECO FROM Terminais WHERE TRM_IND = " + Terminal.ToString());
            reader.Read(); 
            empregador.PessoaTipo = reader["PESSOATIPO"].ToString().Trim();

            if (empregador.PessoaTipo == "J")
                empregador.Pessoa = Strings.trimCnpj(reader["TRM_CNPJ"].ToString().Trim());
            else
                empregador.Pessoa = Strings.trimCnpj(reader["TRM_CPF"].ToString().Trim());

            empregador.Nome = reader["TRM_RAZAOSOCIAL"].ToString().Trim();
            empregador.Cei = "";
            empregador.Endereco = reader["TRM_ENDERECO"].ToString().Trim();

            reader.Close();
            closeConnection();
        }

        public void Atualizar_TerminaisFuncionarios(int Terminal, int Funcionario, bool Exclusao = false)
        {
            if (Exclusao)
            {
                executeCommand(
                    String.Format("UPDATE TerminaisFuncionarios SET TRMF_ARMAZENADO_NO_TERMINAL = 0, TRMF_ARMAZENADO_EM = NULL " +
                    "WHERE TRMF_TERMINAL = {0} AND TRMF_FUNC = {1}",
                    Terminal,
                    Funcionario));
            }
            else
            {
                executeCommand(
                    String.Format("UPDATE TerminaisFuncionarios SET TRMF_ARMAZENADO_NO_TERMINAL = 1, TRMF_ARMAZENADO_EM = GETDATE() " +
                    "WHERE TRMF_TERMINAL = {0} AND TRMF_FUNC = {1}",
                    Terminal,
                    Funcionario));
            }
        }

        public void Atualizar_TerminaisFuncionarios(int Terminal, List<int> Funcionarios)
        {
            for (int i = 0; i <= Funcionarios.Count - 1; i++)
            {
                Atualizar_TerminaisFuncionarios(Terminal, Funcionarios[i]);
            }
        }

        public void Inicializar_TabTemp_DescontoDsr()
        {
            executeCommand(
            "CREATE TABLE #dbtDescDsr ( " +
                "    TDSR_FUNC INT, " +
                "    TDSR_ANTERIOR SMALLDATETIME, " +
                "    TDSR_POSTERIOR SMALLDATETIME " +
                "    ) ON [PRIMARY]", false
                );
        }

        public void Finalizar_TabTemp_DescontoDsr()
        {
            executeCommand("EXEC dsr_calcular_desconto_lote", false);
            executeCommand("DROP TABLE #dbtDescDsr", false);
        }

        public void ProcessarMarcacoes(int Grupo, Marcacoes marcacoes, Log log)
        {
            openConnection();
            Inicializar_TabTemp_DescontoDsr();

            log.AddLog(string.Format("MARCAÇÕES A PROCESSAR: {0}", marcacoes.listMarcacoes.Count));
            int contador = 0;
            int erros = 0;

            try
            {
                foreach (Marcacoes.Marcacao marcacao in marcacoes.listMarcacoes)
                {
                    string Pis;
                    string Data;
                    string Hora;

                    Pis = marcacao.Pis;
                    Data = marcacao.DataHora.ToShortDateString();
                    Hora = marcacao.DataHora.ToShortTimeString();

                    log.AddLogUnformatted(String.Format("{0}/{1}: {2} {3} {4}", contador + 1, marcacoes.listMarcacoes.Count, Pis, Data, Hora));
                    try
                    {
                        executeCommand(String.Format("EXEC importar_marcacao @P_GRUPO = {0},@P_PIS = {1}, @P_DATA = '{2}', @P_HORA = '{3}'", Grupo, Pis, Data, Hora), false);
                        contador++;
                    }
                    catch (Exception e)
                    {
                        erros++;
                        log.AddLogUnformatted(e.ToString());
                    }

                    Application.DoEvents();
                }
            }
            finally
            {
                if (erros > 0)
                {
                    log.AddLogUnformatted(String.Format(Consts.MARCACOES_PROCESSAR_ERROS, erros));
                }

                if (contador > 0)
                {
                    log.AddLogUnformatted(String.Format(Consts.MARCACOES_PROCESSAR_FINALIZADO, contador, marcacoes.listMarcacoes.Count));
                }

                log.LogOk();
                log.AddLineBreak();

                Finalizar_TabTemp_DescontoDsr();
                closeConnection();
            }
        }

        public void getDadosTerminal(int Terminal, out Types.Terminal terminal)
        {
            terminal.IP = getFieldValueString("SELECT TRM_IP FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
            terminal.Porta = Convert.ToInt32(getFieldValueInteger("SELECT TRM_PORTA FROM Terminais WHERE TRM_IND = " + Terminal.ToString()));
            terminal.Descricao = getFieldValueString("SELECT TRM_DESCRICAO FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
            terminal.Serial = getFieldValueString("SELECT TRM_SERIAL FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
            terminal.Grupo = getFieldValueInteger("SELECT TRM_GRUPO FROM Terminais WHERE TRM_IND = " + Terminal.ToString());
        }

        public void MarcarFuncionariosComoEnviados(int Terminal, List<int> Funcionarios)
        {
            foreach (int Funcionario in Funcionarios)
            {
                MarcarFuncionarioComoNaoEnviado(Terminal, Funcionario);
            }
        }

        public void MarcarFuncionarioComoNaoEnviado(int Terminal, int Funcionario)
        {
            executeCommand(String.Format("UPDATE TerminaisFuncionarios SET TRMF_ARMAZENADO_NO_TERMINAL = 0, TRMF_ARMAZENADO_EM = NULL WHERE TRMF_FUNC = {0} AND TRMF_TERMINAL = {1}", Funcionario, Terminal));
        }
    }
}
