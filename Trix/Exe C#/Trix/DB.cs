using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using Wr;
using System.Windows.Forms;

namespace Trix
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

        public string GetFieldValue(string Sql, string Default = "")
        {
            string res = "";

            SqlCommand comm = Conn.CreateCommand();
            comm.CommandText = Sql;
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                if (reader.GetDataTypeName(0) == "bit")
                {
                    if (String.IsNullOrEmpty(reader[0].ToString()))
                        res = "0";
                    else
                        res = Convert.ToInt32(reader[0]).ToString();
                }
                else
                {
                    if (String.IsNullOrEmpty(reader[0].ToString()))
                        res = Default;
                    else
                        res = reader[0].ToString();
                }
            }
            
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

        public int LastNsr
        {
            get
            {
                return Convert.ToInt32(GetFieldValue("SELECT TRM_ULTIMO_NSR FROM Terminais WHERE TRM_IND = " + Terminal.ToString()));
            }
            set
            {
                ExecuteCommand("UPDATE Terminais SET TRM_ULTIMO_NSR = " + value + " WHERE TRM_IND = " + Terminal);
            }
        }

        public void LerEmpresa(int Terminal, out byte IdentificadorTipo, out string Identificador, out string Nome, out string Cei, out string Endereco)
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
            Cei = "";
            Endereco = reader["TRM_ENDERECO"].ToString().Trim();

            reader.Close();
        }

        private void Inicializar_TabTemp_DescontoDsr()
        {
            ExecuteCommand(
            "CREATE TABLE #dbtDescDsr ( " +
                "    TDSR_FUNC INT, " +
                "    TDSR_ANTERIOR SMALLDATETIME, " +
                "    TDSR_POSTERIOR SMALLDATETIME " +
                "    ) ON [PRIMARY]"
                );
        }

        private void Finalizar_TabTemp_DescontoDsr()
        {
            ExecuteCommand("EXEC dsr_calcular_desconto_lote");
            ExecuteCommand("DROP TABLE #dbtDescDsr");
        }

        public void ProcessarMarcacoes(Rede rede, int Grupo, List<string> marcacoes)
        {
            Inicializar_TabTemp_DescontoDsr();

            rede.AddLogUnformatted();
            rede.AddLog(string.Format("PROCESSANDO {0} MARCAÇÕES", marcacoes.Count));

            int marcacao = 1;

            try
            {
                foreach (string s in marcacoes)
                {
                    string Pis = "";
                    string Data = "";
                    string Hora = "";
                    string Nome = "";
                    string Mensagem = "";

                    Pis = s.Substring(0, 12).Trim();
                    Data = s.Substring(13, 10);
                    Hora = s.Substring(24);

                    //progressimportacao.lbMensagem.Text = String.Format("{0} {1} {2}", Pis, Data, Hora);
                    //ExecuteCommand(String.Format("EXEC importar_marcacao @P_GRUPO = {0},@P_PIS = {1}, @P_DATA = '{2}', @P_HORA = '{3}'", Grupo, Pis, Data, Hora));
                    ProcessarMarcacao(Grupo, Pis, Data, Hora, out Nome, out Mensagem);
                    rede.AddLogUnformatted(String.Format("{0} {1} {2} {3} {4} {5}", marcacao.ToString() + "/" + marcacoes.Count.ToString(), Pis, Data, Hora, Nome, Mensagem));
                    Application.DoEvents();
                    marcacao++;
                }
            }
            finally
            {
                Finalizar_TabTemp_DescontoDsr();
            }
        }

        private void ProcessarMarcacao(int Grupo, string Pis, string Data, string Hora, out string Nome, out string Mensagem)
        {
            bool DataValida = false;
            Nome = "";
            Mensagem = "";
            DateTime DataHora;
            DataValida = (DateTime.TryParse(String.Format("{0} {1}", Data, Hora), out DataHora));

            if (DataValida)
            {
                SqlCommand cmd = new SqlCommand("importar_marcacao", Conn);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_GRUPO", Grupo);
                cmd.Parameters.AddWithValue("@P_PIS", Pis);
                cmd.Parameters.AddWithValue("@P_DATA", Data);
                cmd.Parameters.AddWithValue("@P_HORA", Hora);

                SqlParameter parameter_funcnome = cmd.Parameters.Add("@R_FUNCNOME", SqlDbType.Char, 50);
                parameter_funcnome.Direction = ParameterDirection.Output;

                SqlParameter parameter_statusdescricao = cmd.Parameters.Add("@R_STATUSDESCRICAO", SqlDbType.Char, 25);
                parameter_statusdescricao.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                Nome = parameter_funcnome.Value.ToString().Trim();
                Mensagem = parameter_statusdescricao.Value.ToString().Trim();
            }
            else
            {
                Mensagem = "DATA INVÁLIDA";
            }
        }

        public string GetStringConfigFuncionario(int Terminal, int Funcionario)
        {
            string Sql = 
                "SELECT " +
                "(CASE WHEN TRMF_CODIGOBARRAS = 1 THEN '1' ELSE '0' END) + " +
                "(CASE WHEN TRMF_PROXIMIDADE = 1 THEN '1' ELSE '0' END) + " +
                "(CASE WHEN TRMF_BIOMETRIA = 1 THEN '1' ELSE '0' END) + " +
                "(CASE WHEN TRMF_TECLADO = 1 THEN '1' ELSE '0' END) + " +
                "(CASE WHEN TRMF_TECLADO_PASSWORD IS NOT NULL THEN '1' ELSE '0' END) " +
                "FROM TerminaisFuncionarios " +
                "WHERE TRMF_TERMINAL = " + Terminal.ToString() + " AND TRMF_FUNC = " + Funcionario.ToString();

            return GetFieldValue(Sql);
        }
    }
}
