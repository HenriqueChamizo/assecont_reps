using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace IdData
{
    public partial class Captura : Form
    {
        private CIDSysR30 objIDSysR30;
        public int Funcionario;
        private string strPIS;
        public Rede rede;

        public Captura(CIDSysR30 objIDSysR30)
        {
            InitializeComponent();
            this.strPIS = "";

            this.objIDSysR30 = objIDSysR30;
        }

        private void Init()
        {
            bool bReturn = this.objIDSysR30.IsHamsterConnected();

            if (bReturn)
            {
                this.btnCapturarBiometria.Enabled = true;
            }
            else
            {
                this.btnCapturarBiometria.Enabled = false;
                throw new Exception("Hamster não conectado");
                //MessageBox.Show("HAMSTER NÃO CONECTADO", "Hamster", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Captura_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void ler_funcionario()
        {
            DB db = new DB();

            SqlCommand Comm = db.Conn.CreateCommand();
            Comm.CommandText = "SELECT FUNC_NOME, FUNC_PIS FROM Funcionarios WHERE FUNC_IND = " + Funcionario.ToString();
            SqlDataReader reader = Comm.ExecuteReader();

            reader.Read();
            edNome.Text = reader["FUNC_NOME"].ToString().Trim();
            edPis.Text = reader["FUNC_PIS"].ToString().Trim();
            this.strPIS = edPis.Text;
            //Conn.Close();
        }

        private void Captura_Shown(object sender, EventArgs e)
        {
            ler_funcionario();
        }

        public bool SalvaTemplate(byte[] template, string pis, int n)
        {
            try
            {
                string diretoriobiometrias = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Asseponto\\Biometria";

                if (!Directory.Exists(diretoriobiometrias))
                    Directory.CreateDirectory(diretoriobiometrias);

                string arquivo = diretoriobiometrias + "\\" + pis + ".bio";
                using (FileStream fs = File.Create(arquivo))
                {
                    fs.Write(template, 0, template.Length);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCapturarBiometria_Click(object sender, EventArgs e)
        {
            //byte rgbyTemplate_1 = new byte[400];
            //byte byFingerPID_1 = 0;
            //byte rgbyTemplate_2 = new byte[400];
            //bool bStat = false;

            //try
            //{
            //    bStat = this.objIDSysR30.GetTemplates_Virdi(ref byFingerPID_1, ref rgbyTemplate_1, ref rgbyTemplate_2);

            //    if (bStat)
            //    {
            //        Array.Copy(rgbySample404_1, 0, rgbyBiometrics, 0, rgbySample404_1.Length);
            //        Array.Copy(rgbySample404_2, 0, rgbyBiometrics, rgbySample404_1.Length, rgbySample404_2.Length);

            //        if (this.SalvaTemplate(rgbyBiometrics, this.strPIS, 1))
            //        {
            //            //Sucesso
            //        }
            //        else
            //        {
            //            // Não sucesso
            //            // TO DO
            //        }
            //    }
            //    else
            //    {
            //        // Não sucesso
            //        // TO DO
            //    }
            //}
            //catch (Exception exError)
            //{
            //    throw exError;
            //}

            byte[] rgbySample404_1 = new byte[404];
            byte byFingerPID_1 = 0;
            byte bySampleID_1 = 0;
            byte byQuality_1 = 0;
            byte[] rgbySample404_2 = new byte[404];
            byte byFingerPID_2 = 0;
            byte bySampleID_2 = 0;
            byte byQuality_2 = 0;
            bool bRotateSamples = true;
            byte[] rgbyBiometrics = new byte[808];

            bool bReturn = false;

            try
            {
                bReturn = this.objIDSysR30.GetTemplates_FIM01(ref rgbySample404_1, ref byFingerPID_1, ref bySampleID_1, ref byQuality_1, ref rgbySample404_2, ref byFingerPID_2, ref bySampleID_2, ref byQuality_2, bRotateSamples);

                if (bReturn)
                {
                    Array.Copy(rgbySample404_1, 0, rgbyBiometrics, 0, rgbySample404_1.Length);
                    Array.Copy(rgbySample404_2, 0, rgbyBiometrics, rgbySample404_1.Length, rgbySample404_2.Length);

                    if (this.SalvaTemplate(rgbyBiometrics, this.strPIS, 1))
                    {
                        //Sucesso
                    }
                    else
                    {
                        // Não sucesso
                        // TO DO
                    }
                }
                else
                {
                    // Não sucesso
                    // TO DO
                }
            }
            catch (Exception exError)
            {
                throw exError;
            }
        }

    }
}
