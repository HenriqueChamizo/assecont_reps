using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Wr;

namespace Zpm {
    public partial class RepEdicao : Form {
        public int Terminal;
        public int Grupo;
        private DBApp db;

        public RepEdicao() 
        {
            InitializeComponent();
        }

        private void RepEdicao_Load(object sender, EventArgs e)
        {
            if (Terminal > 0)
            {
                db = new DBApp();

                SqlCommand Comm = db.Conn.CreateCommand();
                Comm.CommandText = "SELECT TRM_DESCRICAO, TRM_IP, TRM_PORTA, TRM_RAZAOSOCIAL, TRM_CNPJ, TRM_CPF, " +
                                   "TRM_ENDERECO, TRM_NUMERO, TRM_SERIAL FROM Terminais WHERE TRM_IND = " + Terminal.ToString() + " " +
                                   "ORDER BY TRM_DESCRICAO";
                SqlDataReader reader = Comm.ExecuteReader();

                reader.Read();

                edDescricao.Text = reader["TRM_DESCRICAO"].ToString().Trim();
                edIp.Text = reader["TRM_IP"].ToString().Trim();
                edPorta.Text = reader["TRM_PORTA"].ToString().Trim();
                edRazaoSocial.Text = reader["TRM_RAZAOSOCIAL"].ToString().Trim();
                edCnpj.Text = reader["TRM_CNPJ"].ToString().Trim();
                edCpf.Text = reader["TRM_CPF"].ToString().Trim();
                edEndereco.Text = reader["TRM_ENDERECO"].ToString().Trim();
                edNumero.Text = reader["TRM_NUMERO"].ToString().Trim();
                edSerial.Text = reader["TRM_SERIAL"].ToString().Trim();
            }
            else
                edPorta.Text = Consts.PORTAPADRAO.ToString();
        }

        private void btOk_Click(object sender, EventArgs e)
        {

            //SqlConnection Conn = DB.Connection();
            //SqlCommand Comm = Conn.CreateCommand();

            DB db = new DBApp();
            SqlCommand Comm = db.Conn.CreateCommand();

            if (Terminal > 0)
                Comm.CommandText = "UPDATE Terminais SET TRM_DESCRICAO = @DESCRICAO, TRM_IP = @IP, " +
                                   "TRM_PORTA = @PORTA, TRM_GRUPO = @GRUPO, TRM_FABRICANTE = @FABRICANTE, " +
                                   "TRM_RAZAOSOCIAL = @RAZAOSOCIAL, TRM_CNPJ = @CNPJ, TRM_CPF = @CPF, TRM_ENDERECO = @ENDERECO, TRM_NUMERO = @NUMERO, TRM_SERIAL = @SERIAL " +
                                   "WHERE TRM_IND = " + Terminal.ToString();
            else
                Comm.CommandText = "INSERT INTO Terminais (TRM_DESCRICAO, TRM_IP, TRM_PORTA, TRM_GRUPO, " +
                                   "TRM_FABRICANTE, TRM_RAZAOSOCIAL, TRM_CNPJ, TRM_CPF, TRM_ENDERECO, TRM_NUMERO, TRM_SERIAL) VALUES (@DESCRICAO, " +
                                   "@IP, @PORTA, @GRUPO, @FABRICANTE, @RAZAOSOCIAL, @CNPJ, @CPF, @ENDERECO, @NUMERO, @SERIAL)";

            Comm.Parameters.Add(new SqlParameter("@DESCRICAO", edDescricao.Text));
            Comm.Parameters.Add(new SqlParameter("@IP", edIp.Text));
            Comm.Parameters.Add(new SqlParameter("@PORTA", edPorta.Text));
            Comm.Parameters.Add(new SqlParameter("@GRUPO", Grupo));
            Comm.Parameters.Add(new SqlParameter("@FABRICANTE", "TRIX"));

            Comm.Parameters.Add(new SqlParameter("@RAZAOSOCIAL", edRazaoSocial.Text));

            Comm.Parameters.Add(new SqlParameter("@CNPJ", edCnpj.Text));
            Comm.Parameters.Add(new SqlParameter("@CPF", edCpf.Text));

            Comm.Parameters.Add(new SqlParameter("@ENDERECO", edEndereco.Text));
            Comm.Parameters.Add(new SqlParameter("@NUMERO", edNumero.Text));
            Comm.Parameters.Add(new SqlParameter("@SERIAL", edSerial.Text));

            Comm.ExecuteNonQuery();
            //Conn.Close();

            this.Close();
        }
    }
}
