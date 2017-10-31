using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Wr
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

        //public DataSet Select(string Sql)
        //{
        //    SqlDataAdapter da = new SqlDataAdapter("SELECT FUNC_IND, FUNC_PIS, TRMF_ARMAZENADO_NO_TERMINAL " +
        //           "FROM Funcionarios " +
        //           "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
        //           "WHERE TRMF_TERMINAL = " + Terminal + " AND FUNC_PIS IS NOT NULL " +
        //           "ORDER BY FUNC_NOME", Conn);

        //    DataSet ds = new DataSet();
        //    da.Fill(ds);

        //    return ds;
        //}
    }
}
