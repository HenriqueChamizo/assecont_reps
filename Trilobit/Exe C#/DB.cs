using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Trilobit
{
    public class DB
    {
        public SqlConnection Conn;

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

            SqlCommand cmd = new SqlCommand("SET DATEFORMAT DMY", Conn);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
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

        public string GetPis(int Funcionario)
        {
            string Pis = GetFieldValue(
                                       "SELECT FUNC_PIS " +
                                       "FROM Funcionarios " +
                                       "WHERE FUNC_IND = " + Funcionario + " " +
                                       "ORDER BY FUNC_NOME"
                                       );
            return (Pis);
        }

        public void SalvarConfiguracao(int Grupo, string Campo, string Valor)
        {
            ExecuteCommand("UPDATE Config SET " + Campo + " = '" + Valor + "' WHERE CFG_GRUPO = " + Grupo.ToString());
        }

        public string LerConfiguracao(int Grupo, string Campo)
        {
            return (GetFieldValue("SELECT " + Campo + " FROM Config WHERE CFG_GRUPO = " + Grupo.ToString()));
        }

        public int GetSenhaTerminal(int Terminal)
        {
            return Convert.ToInt32(GetFieldValue("SELECT TRM_SENHA FROM Terminais WHERE TRM_IND = " + Terminal.ToString()).Trim());
        }
    }
}
