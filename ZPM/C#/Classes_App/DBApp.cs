using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wr;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Zpm
{
    class DBApp : DB
    {
        public uint LastNsr
        {
            get
            {
                return Convert.ToUInt32(GetFieldValue("SELECT TRM_ULTIMO_NSR FROM Terminais WHERE TRM_IND = " + Terminal.ToString()));
            }
            set
            {
                ExecuteCommand("UPDATE Terminais SET TRM_ULTIMO_NSR = " + value + " WHERE TRM_IND = " + Terminal);
            }
        }

        public void LerEmpresa(int Terminal, out Types.Empregador empregador)
        {
            SqlCommand comm = Conn.CreateCommand();
            comm.CommandText = "SELECT TOP 1 (CASE WHEN ISNULL(TRM_CNPJ, '') <> '' THEN 'J' ELSE 'F' END) AS PESSOATIPO, " +
                               "TRM_CNPJ, TRM_CPF, TRM_RAZAOSOCIAL, TRM_ENDERECO FROM Terminais WHERE TRM_IND = " + Terminal.ToString();

            SqlDataReader reader = comm.ExecuteReader();
            reader.Read();
            empregador.PessoaTipo = reader["PESSOATIPO"].ToString().Trim();

            if (empregador.PessoaTipo == "J")
                empregador.Pessoa = rStrings.FormataCnpj(reader["TRM_CNPJ"].ToString().Trim());
            else
                empregador.Pessoa = rStrings.FormataCnpj(reader["TRM_CPF"].ToString().Trim());

            empregador.Nome = reader["TRM_RAZAOSOCIAL"].ToString().Trim();
            empregador.Cei = "";
            empregador.Endereco = reader["TRM_ENDERECO"].ToString().Trim();

            reader.Close();
        }

        public void Atualizar_TerminaisFuncionarios(int Terminal, int Funcionario, bool Exclusao = false)
        {
            if (Exclusao)
            {
                ExecuteCommand(
                    String.Format("UPDATE TerminaisFuncionarios SET TRMF_ARMAZENADO_NO_TERMINAL = 0, TRMF_ARMAZENADO_EM = NULL " +
                    "WHERE TRMF_TERMINAL = {0} AND TRMF_FUNC = {1}",
                    Terminal,
                    Funcionario));
            }
            else
            {
                ExecuteCommand(
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
            ExecuteCommand(
            "CREATE TABLE #dbtDescDsr ( " +
                "    TDSR_FUNC INT, " +
                "    TDSR_ANTERIOR SMALLDATETIME, " +
                "    TDSR_POSTERIOR SMALLDATETIME " +
                "    ) ON [PRIMARY]"
                );
        }

        public void Finalizar_TabTemp_DescontoDsr()
        {
            ExecuteCommand("EXEC dsr_calcular_desconto_lote");
            ExecuteCommand("DROP TABLE #dbtDescDsr");
        }

        public void ProcessarMarcacoes(int Grupo, List<Marcacoes.Marcacao> marcacoes, Log log)
        {
            //ProgressImportacao progressimportacao = new ProgressImportacao(); 

            Inicializar_TabTemp_DescontoDsr();

            log.AddLog(string.Format("MARCAÇÕES A PROCESSAR: {0}", marcacoes.Count));
            int contador = 0;
            int erros = 0;

            try
            {
                foreach (Marcacoes.Marcacao marcacao in marcacoes)
                {
                    string Pis;
                    string Data;
                    string Hora;

                    Pis = marcacao.Pis;
                    Data = marcacao.DataHora.ToShortDateString();
                    Hora = marcacao.DataHora.ToShortTimeString();

                    log.AddLogUnformatted(String.Format("{0}/{1}: {2} {3} {4}", contador + 1, marcacoes.Count, Pis, Data, Hora));
                    try
                    {
                        ExecuteCommand(String.Format("EXEC importar_marcacao @P_GRUPO = {0},@P_PIS = {1}, @P_DATA = '{2}', @P_HORA = '{3}'", Grupo, Pis, Data, Hora));
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
                    log.AddLogUnformatted(String.Format(Consts.MARCACOES_PROCESSAR_FINALIZADO, contador, marcacoes.Count));
                }

                log.LogOk();
                log.AddLineBreak();

                Finalizar_TabTemp_DescontoDsr();
            }
        }

        public void getDadosTerminal(int Terminal, out Types.Terminal terminal)
        {
            terminal.IP = GetFieldValue("SELECT TRM_IP FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
            terminal.Porta = Convert.ToInt32(GetFieldValue("SELECT TRM_PORTA FROM Terminais WHERE TRM_IND = " + Terminal.ToString()));
            terminal.Descricao = GetFieldValue("SELECT TRM_DESCRICAO FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
            terminal.Serial = GetFieldValue("SELECT TRM_SERIAL FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim();
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
            ExecuteCommand(String.Format("UPDATE TerminaisFuncionarios SET TRMF_ARMAZENADO_NO_TERMINAL = 0, TRMF_ARMAZENADO_EM = NULL WHERE TRMF_FUNC = {0} AND TRMF_TERMINAL = {1}", Funcionario, Terminal));
        }
    }
}
