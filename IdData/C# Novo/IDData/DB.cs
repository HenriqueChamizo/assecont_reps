using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace IdData
{
    public class DB
    {
        public SqlConnection Conn;
        public int Terminal = -1;

        public DB()
        {
            string server;
            string database;
            string user;
            string pwd;

            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Assecont\\Asseponto4\\Login");

            server = rStrings.CryptString((string)key.GetValue("S"), -2);
            database = rStrings.CryptString((string)key.GetValue("D"), -2);
            user = rStrings.CryptString((string)key.GetValue("U"), -3);
            pwd = rStrings.CryptString((string)key.GetValue("P"), -4);

            key.Close();

            Conn = new SqlConnection("Data Source=" + server + ";Initial Catalog=" + database + ";Persist Security Info=True;User ID=" + user + ";Password=" + pwd);
            Conn.Open();
        }

        public string GetFieldValue(string Sql)
        {
            string res = "";

            SqlCommand comm = Conn.CreateCommand();
            comm.CommandText = Sql;
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read()) res = reader[0].ToString();
            reader.Close();
            return res;
        }

        public int ExecuteCommand(string Sql)
        {
            SqlCommand Comm = Conn.CreateCommand();
            Comm.CommandText = Sql;
            Comm.ExecuteNonQuery();
            return 0;
        }

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

        public void LerEmpresa(int Terminal, out byte IdentificadorTipo, out string Identificador, out string Nome, out ulong Cei, out string Endereco)
        {
            SqlCommand comm = Conn.CreateCommand();
            comm.CommandText = "SELECT TOP 1 (CASE WHEN ISNULL(TRM_CNPJ, '') <> '' THEN 1 ELSE 0 END) AS IDENTIFICADORTIPO, TRM_CNPJ, TRM_CPF, TRM_RAZAOSOCIAL, TRM_ENDERECO FROM Terminais WHERE TRM_IND = " + Terminal.ToString();
            SqlDataReader reader = comm.ExecuteReader();
            reader.Read();
            IdentificadorTipo = Convert.ToByte(rStrings.FormataCnpj(reader["IDENTIFICADORTIPO"].ToString().Trim()));

            if (IdentificadorTipo == 1)
                Identificador = rStrings.FormataCnpj(reader["TRM_CNPJ"].ToString().Trim());
            else
                Identificador = rStrings.FormataCnpj(reader["TRM_CPF"].ToString().Trim());

            Nome = reader["TRM_RAZAOSOCIAL"].ToString().Trim();
            Cei = 0;
            Endereco = reader["TRM_ENDERECO"].ToString().Trim();

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

        public void ProcessarMarcacoes(int Grupo, List<string> marcacoes, Rede rede)
        {
            //ProgressImportacao progressimportacao = new ProgressImportacao(); 
            
            Inicializar_TabTemp_DescontoDsr();

            rede.AddLog(string.Format("MARCAÇÕES A PROCESSAR: {0}", marcacoes.Count));
            int contador = 1;
            //progressimportacao.Show();
            //progressimportacao.progressBar1.Maximum = marcacoes.Count;
            //progressimportacao.progressBar1.Value = 0;

            try
            {
                foreach (string s in marcacoes)
                {
                    string Pis;
                    string Data;
                    string Hora;

                    Pis = s.Substring(0, 12).Trim();
                    Data = s.Substring(13, 10);
                    Hora = s.Substring(24);

                    //progressimportacao.lbMensagem.Text = String.Format("{0} {1} {2}", Pis, Data, Hora);
                    rede.AddLogUnformatted(String.Format("{0}/{1}: {2} {3} {4}", contador, marcacoes.Count, Pis, Data, Hora));
                    try
                    {
                        if (contador != 3)
                        ExecuteCommand(String.Format("EXEC importar_marcacao @P_GRUPO = {0},@P_PIS = {1}, @P_DATA = '{2}', @P_HORA = '{3}'", Grupo, Pis, Data, Hora));
                        else
                            ExecuteCommand(String.Format("EXEC importar_marcacao @P_GRUPO = {0},@P_PIS = {1}, @P_DATA = '{2}', @P_HORA = '{3}'", Grupo, Pis, Data, Hora));
                    }
                    catch (Exception e)
                    {
                        rede.AddLogUnformatted(e.ToString());
                    }

                    //progressimportacao.progressBar1.Value++;
                    contador++;
                    Application.DoEvents();
                }

            }
            finally
            {
                //progressimportacao.Close();
                Finalizar_TabTemp_DescontoDsr();
            }
        }

    }
}
