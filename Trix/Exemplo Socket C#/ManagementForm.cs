using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TRXHumanResource
{
    public partial class ManagementForm : Form
    {
        public List<Empresa> empresas = new List<Empresa>();
        public List<Coletor> coletores = new List<Coletor>();
        public List<Funcionario> funcionarios = new List<Funcionario>();
        public List<string> naoEnviados = new List<string>();
        public DateTime tempoTimeout;
        bool expirouTimeout = false;
        bool Cancela = false;
        bool abortaEnvio = false;
        

        public ManagementForm()
        {
            InitializeComponent();
        }

        private void ManagementForm_Load(object sender, EventArgs e)
        {
            //CarregaEmpresas();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox2.SelectedIndex = -1;
            //if (comboBox1.SelectedIndex >= 0)
            //{
            //    Empresa emp = (Empresa)empresas[comboBox1.SelectedIndex];
            //    CarregaColetores(emp.ID);
            //}
        }

        public void CarregaColetores(long idEmpresa)
        {
            //comboBox2.Items.Clear();
            //coletores = DB.ColetoresEmpresa(idEmpresa);
            //foreach (Coletor col in coletores)
            //{
            //    comboBox2.Items.Add(col.Descricao);
            //}
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBox2.SelectedIndex >= 0)
            //{
            //    Coletor col = (Coletor)coletores[comboBox2.SelectedIndex];
            //    CarregaInfo(col);
            //    groupBox3.Enabled = true;
            //    Empresa emp = (Empresa)empresas[comboBox1.SelectedIndex];
            //    CarregaFuncionarios(emp.ID);
            //    button6.Enabled = false;
            //    button9.Enabled = false;
            //    button10.Enabled = false;
            //}
            //else
            //{
            //    LimpaInfo();

            //}
        }

        public void CarregaFuncionarios(long idEmpresa)
        {
            //listBox1.Items.Clear();
            //funcionarios = DB.ListaFuncionarios(idEmpresa);
            //foreach (Funcionario fun in funcionarios)
            //{
            //    listBox1.Items.Add(fun.Nome + "           |" + fun.PIS);
            //}
        }

        public void CarregaInfo(Coletor col)
        {
            //Localidade loc = DB.PesquisaLocalidade(col.ID);

            //textBox1.Text = col.Descricao;
            
            //textBox3.Text = loc.Descricao;

            //switch (col.Modo)
            //{
            //    case "IP":
            //        modoLabel.Text = col.Modo + ": " + col.IP + ":" + col.Porta;
            //        break;
            //    case "Serial":
            //        modoLabel.Text = col.Modo + ": " + col.COM;
            //        break;
            //}

            

        }

        //public void LimpaInfo()
        //{
        //    groupBox3.Enabled = false;
        //    textBox1.Text = string.Empty;
            
        //    textBox3.Text = string.Empty;

           
        //    listBox1.Items.Clear();
            
        //}

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void TravaComandos()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            //button7.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
            button12.Enabled = false;
        }

        public void DestravaComandos()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            //button7.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;
            //listBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //progressBar1.Value = 0;
            //Thread.Sleep(500);
            //labelStatus.Text = string.Empty;
            //foreach (Coletor col in ManagementGroupForm.lista_coletor)
            //{
            //    TravaComandos();
            //    EnviaInfoEmpresa(col);
            //    DestravaComandos();
    
            //}
            
            

        }

        public string MontaMensagemEmpresa(List<Coletor> lista_col, Empresa emp)
        {
            string mensagem = "";
            foreach (Coletor col in lista_col)
            {
                Localidade loc = DB.PesquisaLocalidade(col.ID);

                string tipoIdentificador = string.Empty;
                if (emp.Tipo.Equals("Jurídica"))
                    tipoIdentificador = "1";
                if (emp.Tipo.Equals("Física"))
                    tipoIdentificador = "2";

                string identificador = emp.Identificador;
                for (int i = identificador.Length; i < 14; i++)
                    identificador = identificador + " ";

                string cei = emp.CEI;
                for (int i = cei.Length; i < 12; i++)
                    cei = cei + " ";

                string razao = emp.Nome;
                for (int i = razao.Length; i < 150; i++)
                    razao = razao + " ";

                string local = loc.Descricao;
                for (int i = local.Length; i < 100; i++)
                    local = local + " ";

                mensagem = tipoIdentificador + identificador + cei + razao + local;
            }
            return mensagem;
        }

        public Comando MontaComando(string mensagem,string tipoComando, string modo)
        {
            Comando cmd = new Comando(tipoComando, modo, mensagem);
            return cmd;
        }

        public void EnviaInfoEmpresa(Coletor coletor, Empresa emp)
        {
            string mensagem = MontaMensagemEmpresa(ManagementGroupForm.lista_coletor, emp);
            Comando cmd = MontaComando(mensagem, Protocolo.CMD_EMPRESA, Protocolo.SET);

            progressBar1.Maximum = 1;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;

            // Inicia comunicação
            switch (coletor.Modo)
            {
                case "IP":
                    EnviaComandoTCP(cmd, coletor);
                    break;
                case "Serial":
                    EnviaComandoSerial(cmd, coletor);
                    break;
            }

            progressBar1.Value++;
            

            

        }

        public void IniciaTempoTimeout()
        {

            tempoTimeout = DateTime.Now.AddSeconds(10);
        }

        /// <summary>
        /// Verifica se houve timeout
        /// </summary>
        /// <returns>true, caso o tempo tenha expirado. false, caso não tenha expirado</returns>
        public bool VerificaTimeout()
        {

            DateTime atual = DateTime.Now;
            int res = atual.CompareTo(tempoTimeout);
            if (res >= 0)
                return true;
            else
                return false;
            
            
            
        }

        public void EnviaComandoSerial(Comando cmd, Coletor col)
        {
            bool executa = false;
            serialPort1.PortName = col.COM;
            serialPort1.BaudRate = 9600;
            switch (cmd.Modo)
            {
                #region SET
                case "S":
                    try
                    {
                        Application.DoEvents();
                        serialPort1.Open();

                        byte[] envio = cmd.ToByte();
                        serialPort1.Write(envio, 0, envio.Length);

                        byte[] recebimento = new byte[15];
                        string msg = string.Empty;
                        executa = true;
                        IniciaTempoTimeout();
                        do
                        {
                            Application.DoEvents();
                            if(serialPort1.BytesToRead >= 15)
                            {
                                serialPort1.Read(recebimento, 0, recebimento.Length);

                                msg = msg + Protocolo.ConverteByteToString(recebimento);
                                executa = false;
                            }
                            expirouTimeout = VerificaTimeout();
                            if (expirouTimeout)
                                executa = false;
                        } while (executa);
                        if (expirouTimeout)
                        {
                            labelStatus.Text = "Timeout na comunicação";
                            if (cmd.TipoComando.Equals("!03"))
                            {
                                naoEnviados.Add(cmd.ToString());
                                expirouTimeout = false;
                                serialPort1.Close();
                                return;
                            }
                        }
                        switch (cmd.TipoComando)
                        {
                            case "!03":

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);

                                    if (resp.Mensagem.Equals("15"))
                                        naoEnviados.Add(cmd.ToString());
                                }
                                else
                                {
                                    naoEnviados.Add(cmd.ToString());
                                }
                                break;
                            default:
                                if (expirouTimeout)
                                {
                                    progressBar1.Value = 0;
                                    expirouTimeout = false;
                                    serialPort1.Close();
                                    return;
                                }
                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        labelStatus.Text = "Comando enviado com sucesso";
                                    else
                                        labelStatus.Text = "Erro no REP ao receber comando";
                                }
                                else
                                {
                                    labelStatus.Text = "Resposta incorreta";
                                }
                                break;
                        }
                        serialPort1.Close();

                    }
                    catch (Exception ex)
                    {
                        if (cmd.TipoComando.Equals("!03"))
                            naoEnviados.Add(cmd.ToString());
                        serialPort1.Close();
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    break;
                #endregion

                #region Envia Data/Hora
                case "R":
                    switch (cmd.TipoComando)
                    {
                        #region Empresa
                        case "!01":

                            try
                            {
                                serialPort1.Open();

                                byte[] envio = cmd.ToByte();
                                serialPort1.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[290];
                                string msg = string.Empty;
                                executa = true;
                                IniciaTempoTimeout();
                                do
                                {
                                    Application.DoEvents();
                                    if(serialPort1.BytesToRead >= 290)
                                    {
                                        serialPort1.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    labelStatus.Text = "Timeout na comunicação";
                                    expirouTimeout = false;
                                    serialPort1.Close();
                                    return;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        labelStatus.Text = resp.InfoEmpresa();
                                    else
                                        labelStatus.Text = "Erro ao receber informação do REP";
                                }
                                else
                                {
                                    labelStatus.Text = "Resposta incorreta";
                                }
                                serialPort1.Close();
                            }
                            catch (Exception ex)
                            {
                                serialPort1.Close();
                                labelStatus.Text = ex.Message;
                                return;
                            }
                            break;
                        #endregion

                        #region Data/Hora
                        case "!02":

                            try
                            {
                                serialPort1.Open();
                                byte[] envio = cmd.ToByte();
                                serialPort1.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[25];
                                string msg = string.Empty;
                                executa = true;
                                IniciaTempoTimeout();
                                do
                                {
                                    Application.DoEvents();
                                    if(serialPort1.BytesToRead >= 25)
                                    {
                                        serialPort1.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    labelStatus.Text = "Timeout na comunicação";
                                    expirouTimeout = false;
                                    serialPort1.Close();
                                    return;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        labelStatus.Text = resp.InfoDataHora();
                                    else
                                        labelStatus.Text = "Erro ao receber informação do REP";
                                }
                                else
                                {
                                    labelStatus.Text = "Resposta incorreta";
                                }
                                serialPort1.Close();
                            }
                            catch (Exception ex)
                            {
                                serialPort1.Close();
                                labelStatus.Text = ex.Message;
                                return;
                            }
                            break;
                        #endregion

                        #region Funcionario
                        case "!03":

                            try
                            {
                                serialPort1.Open();

                                byte[] envio = cmd.ToByte();
                                serialPort1.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[108];
                                string msg = string.Empty;
                                executa = true;
                                IniciaTempoTimeout();
                                do
                                {
                                    Application.DoEvents();
                                    if(serialPort1.BytesToRead >= 108)
                                    {
                                        serialPort1.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    labelStatus.Text = "Timeout na comunicação";
                                    expirouTimeout = false;
                                    serialPort1.Close();
                                    return;
                                }
                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("15"))
                                        naoEnviados.Add(cmd.ToString());
                                }
                                else
                                {
                                    naoEnviados.Add(cmd.ToString());
                                }
                                serialPort1.Close();
                            }
                            catch (Exception ex)
                            {
                                serialPort1.Close();
                                labelStatus.Text = ex.Message;
                                return;
                            }
                            break;
                        #endregion

                        #region Biometria
                        case "!05":

                            try
                            {
                                serialPort1.Open();

                                byte[] envio = cmd.ToByte();
                                serialPort1.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[397];
                                string msg = string.Empty;
                                executa = true;
                                IniciaTempoTimeout();
                                do
                                {
                                    Application.DoEvents();
                                    if(serialPort1.BytesToRead >= 397)
                                    {
                                        serialPort1.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    labelStatus.Text = "Timeout na comunicação";
                                    expirouTimeout = false;
                                    return;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        labelStatus.Text = resp.InfoFuncionario();
                                    else
                                        labelStatus.Text = "Erro ao receber informação do REP";
                                }
                                else
                                {
                                    labelStatus.Text = "Resposta incorreta";
                                }
                                serialPort1.Close();
                            }
                            catch (Exception ex)
                            {
                                serialPort1.Close();
                            }
                            break;
                        #endregion

                        #region Marcacao
                        case "!06":

                            try
                            {
                                serialPort1.Open();

                                byte[] envio = cmd.ToByte();
                                serialPort1.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[397];
                                string msg = string.Empty;
                                executa = true;
                                IniciaTempoTimeout();
                                do
                                {
                                    Application.DoEvents();
                                    if(serialPort1.BytesToRead >= 397)
                                    {
                                        serialPort1.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;

                                } while (executa);
                                if (expirouTimeout)
                                {
                                    labelStatus.Text = "Timeout na comunicação";
                                    expirouTimeout = false;
                                    serialPort1.Close();
                                    return;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        labelStatus.Text = resp.InfoFuncionario();
                                    else
                                        labelStatus.Text = "Erro ao receber informação do REP";
                                }
                                else
                                {
                                    labelStatus.Text = "Resposta incorreta";
                                }
                                serialPort1.Close();
                            }
                            catch (Exception ex)
                            {
                                serialPort1.Close();
                                labelStatus.Text = ex.Message;
                                return;
                            }
                            break;
                        #endregion
                    }
                    break;
                #endregion

            }
        }

        public void EnviaComandoTCP(Comando cmd, Coletor col)
        {
            bool executa = false;
            TcpClient sock = new TcpClient();
            switch (cmd.Modo)
            {
                #region SET
                case "S":
                    try
                    {
                        Application.DoEvents();
                        sock.Connect(col.IP, col.Porta);
                        NetworkStream stream = sock.GetStream();
                        
                        byte[] envio = cmd.ToByte();
                        stream.Write(envio, 0, envio.Length);
                        
                        byte[] recebimento = new byte[15];
                        string msg = string.Empty;
                        executa = true;
                        IniciaTempoTimeout();
                        do
                        {
                            Application.DoEvents();
                            if (sock.Available >= 15)
                            {
                                stream.Read(recebimento, 0, recebimento.Length);
                                
                                msg = msg + Protocolo.ConverteByteToString(recebimento);
                                executa = false;
                            }
                            expirouTimeout = VerificaTimeout();
                            if (expirouTimeout)
                                executa = false;
                        } while (executa);
                        if (expirouTimeout)
                        {
                            labelStatus.Text = "Timeout na comunicação";
                            if (cmd.TipoComando.Equals("!03"))
                            {
                                naoEnviados.Add(cmd.ToString());
                                expirouTimeout = false;
                                sock.Close();
                                return;
                            }
                        }
                        switch (cmd.TipoComando)
                        {
                            case "!03":
                                
                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);

                                    if (resp.Mensagem.Equals("15"))
                                        naoEnviados.Add(cmd.ToString());
                                }
                                else
                                {
                                    naoEnviados.Add(cmd.ToString());
                                }
                                break;
                            default:
                                if (expirouTimeout)
                                {
                                    progressBar1.Value = 0;
                                    expirouTimeout = false;
                                    sock.Close();
                                    return;
                                }
                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        labelStatus.Text = "Comando enviado com sucesso";
                                    else
                                        labelStatus.Text = "Erro no REP ao receber comando";
                                }
                                else
                                {
                                    labelStatus.Text = "Resposta incorreta";
                                }
                                break;
                        }
                        sock.Close();
                        
                    }
                    catch (Exception ex)
                    {

                        if (cmd.TipoComando.Equals("!03"))
                        {
                            naoEnviados.Add(cmd.ToString());
                            abortaEnvio = true;
                        }
                        sock.Close();
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    break;
                #endregion

                #region READ
                case "R":
                    switch (cmd.TipoComando)
                    {
                        #region Empresa
                        case "!01":
                            
                            try
                            {
                                sock.Connect(col.IP, col.Porta);
                                NetworkStream stream = sock.GetStream();

                                byte[] envio = cmd.ToByte();
                                stream.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[290];
                                string msg = string.Empty;
                                executa = true;
                                IniciaTempoTimeout();
                                do
                                {
                                    Application.DoEvents();
                                    if (sock.Available >= 290)
                                    {
                                        stream.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    labelStatus.Text = "Timeout na comunicação";
                                    expirouTimeout = false;
                                    sock.Close();
                                    return;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        MessageBox.Show(resp.InfoEmpresa());
                                    else
                                        labelStatus.Text = "Erro ao receber informação do REP";
                                }
                                else
                                {
                                    labelStatus.Text = "Resposta incorreta";
                                }
                                sock.Close();
                            }
                            catch (Exception ex)
                            {
                                sock.Close();
                                labelStatus.Text = ex.Message;
                                return;
                            }
                            break;
                        #endregion

                        #region Data/Hora
                        case "!02":
                            
                            try
                            {
                                sock.Connect(col.IP, col.Porta);
                                NetworkStream stream = sock.GetStream();

                                byte[] envio = cmd.ToByte();
                                stream.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[25];
                                string msg = string.Empty;
                                executa = true;
                                IniciaTempoTimeout();
                                do
                                {
                                    Application.DoEvents();
                                    if (sock.Available >= 25)
                                    {
                                        stream.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    labelStatus.Text = "Timeout na comunicação";
                                    expirouTimeout = false;
                                    sock.Close();
                                    return;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        MessageBox.Show(resp.InfoDataHora());
                                    else
                                        labelStatus.Text = "Erro ao receber informação do REP";
                                }
                                else
                                {
                                    labelStatus.Text = "Resposta incorreta";
                                }
                                sock.Close();
                            }
                            catch (Exception ex)
                            {
                                sock.Close();
                                labelStatus.Text = ex.Message;
                                return;
                            }
                            break;
                        #endregion

                        #region Funcionario
                        case "!03":
                            
                            try
                            {
                                sock.Connect(col.IP, col.Porta);
                                NetworkStream stream = sock.GetStream();

                                byte[] envio = cmd.ToByte();
                                stream.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[108];
                                string msg = string.Empty;
                                executa = true;
                                IniciaTempoTimeout();
                                do
                                {
                                    Application.DoEvents();
                                    if (sock.Available >= 108)
                                    {
                                        stream.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    labelStatus.Text = "Timeout na comunicação";
                                    expirouTimeout = false;
                                    sock.Close();
                                    return;
                                }
                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("15"))
                                        naoEnviados.Add(cmd.ToString());
                                }
                                else
                                {
                                    naoEnviados.Add(cmd.ToString());
                                }
                                sock.Close();
                            }
                            catch (Exception ex)
                            {
                                sock.Close();
                                MessageBox.Show(ex.Message);
                                return;
                            }
                            break;
                        #endregion

                        #region Biometria
                        case "!05":
                            
                            try
                            {
                                sock.Connect(col.IP, col.Porta);
                                NetworkStream stream = sock.GetStream();

                                byte[] envio = cmd.ToByte();
                                stream.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[397];
                                string msg = string.Empty;
                                executa = true;
                                IniciaTempoTimeout();
                                do
                                {
                                    Application.DoEvents();
                                    if (sock.Available >= 397)
                                    {
                                        stream.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    labelStatus.Text = "Timeout na comunicação";
                                    expirouTimeout = false;
                                    return;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        MessageBox.Show(resp.InfoFuncionario());
                                    else
                                        labelStatus.Text = "Erro ao receber informação do REP";
                                }
                                else
                                {
                                    labelStatus.Text = "Resposta incorreta";
                                }
                                sock.Close();
                            }
                            catch (Exception ex)
                            {
                                sock.Close();
                            }
                            break;
                        #endregion

                        #region Marcacao
                        case "!06":
                            if (cmd.Mensagem.Equals("0"))
                            {
                                #region Novas Marcações
                                try
                                {

                                    sock.Connect(col.IP, col.Porta);
                                    NetworkStream stream = sock.GetStream();

                                    byte[] envio = cmd.ToByte();
                                    stream.Write(envio, 0, envio.Length);

                                    byte[] recebimento = new byte[397];
                                    string msg = string.Empty;
                                    executa = true;
                                    IniciaTempoTimeout();
                                    do
                                    {
                                        Application.DoEvents();
                                        if (sock.Available >= 397)
                                        {
                                            stream.Read(recebimento, 0, recebimento.Length);
                                            msg = msg + Protocolo.ConverteByteToString(recebimento);
                                            executa = false;
                                        }
                                        expirouTimeout = VerificaTimeout();
                                        if (expirouTimeout)
                                            executa = false;

                                    } while (executa);
                                    if (expirouTimeout)
                                    {
                                        labelStatus.Text = "Timeout na comunicação";
                                        expirouTimeout = false;
                                        sock.Close();
                                        return;
                                    }

                                    if (Comando.VerificaCRC(msg))
                                    {
                                        Comando resp = new Comando(msg);
                                        if (resp.Mensagem.Equals("06"))
                                            MessageBox.Show(resp.InfoFuncionario());
                                        else
                                            labelStatus.Text = "Erro ao receber informação do REP";
                                    }
                                    else
                                    {
                                        labelStatus.Text = "Resposta incorreta";
                                    }
                                    sock.Close();
                                }
                                catch (Exception ex)
                                {
                                    sock.Close();
                                    labelStatus.Text = ex.Message;
                                    return;
                                }
                                #endregion
                            }

                            if (cmd.Mensagem.Equals("1"))
                            {
                                #region Todas Marcações
                                try
                                {
                                    sock.Connect(col.IP, col.Porta);
                                    NetworkStream stream = sock.GetStream();

                                    byte[] envio = cmd.ToByte();
                                    stream.Write(envio, 0, envio.Length);

                                    byte[] recebimento = new byte[397];
                                    string msg = string.Empty;
                                    executa = true;
                                    IniciaTempoTimeout();
                                    do
                                    {
                                        Application.DoEvents();
                                        if (sock.Available >= 397)
                                        {
                                            stream.Read(recebimento, 0, recebimento.Length);
                                            msg = msg + Protocolo.ConverteByteToString(recebimento);
                                            executa = false;
                                        }
                                        expirouTimeout = VerificaTimeout();
                                        if (expirouTimeout)
                                            executa = false;

                                    } while (executa);
                                    if (expirouTimeout)
                                    {
                                        labelStatus.Text = "Timeout na comunicação";
                                        expirouTimeout = false;
                                        sock.Close();
                                        return;
                                    }

                                    if (Comando.VerificaCRC(msg))
                                    {
                                        Comando resp = new Comando(msg);
                                        if (resp.Mensagem.Equals("06"))
                                            MessageBox.Show(resp.InfoFuncionario());
                                        else
                                            labelStatus.Text = "Erro ao receber informação do REP";
                                    }
                                    else
                                    {
                                        labelStatus.Text = "Resposta incorreta";
                                    }
                                    sock.Close();
                                }
                                catch (Exception ex)
                                {
                                    sock.Close();
                                    MessageBox.Show(ex.Message);
                                    return;
                                }
                                #endregion
                            }
                            break;
                        #endregion
                    }
                    break;
                #endregion

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
                progressBar1.Value = 0;
                Thread.Sleep(500);
                labelStatus.Text = string.Empty;

                foreach (Coletor col in ManagementGroupForm.lista_coletor)
                {
                    TravaComandos();
                    EnviaDataHora(col);
                    DestravaComandos();
                }
        }

        

        public void EnviaDataHora(Coletor coletor)
        {
            

            // Monta a data e hora
            string data = DateTime.Now.Date.ToString("ddMMyyyy");

            string hora = DateTime.Now.Hour.ToString();
            if (hora.Length < 2)
                hora = "0" + hora;
            string min = DateTime.Now.Minute.ToString();
            if (min.Length < 2)
                min = "0" + min;
            string sec = DateTime.Now.Second.ToString();
            if (sec.Length < 2)
                sec = "0" + sec;

            string horario = hora + min + sec;
            string hora_verao = null;
            string msg = "";
            if(File.Exists(Application.StartupPath + "\\SummerTime.ini"))
            {
            using(StreamReader sr = new StreamReader(Application.StartupPath + "\\SummerTime.ini"))
            {
                string comeco = sr.ReadLine();
                string fim = sr.ReadLine();
                string datacab = comeco;
                string datai, datamei, datafim;
                datai = datacab.Substring(0, datacab.IndexOf("/"));
                datacab = datacab.Substring(datai.Length + 1);
                datamei = datacab.Substring(0, datacab.IndexOf("/"));
                datacab = datacab.Substring(datamei.Length + 1);
                datafim = datacab;
                if (datai.Length == 1)
                {
                    datai = "0" + datai;
                }
                if (datamei.Length == 1)
                {
                    datamei = "0" + datamei;
                }
                datacab = datai + datamei + datafim.Substring(0, 4);
                comeco = datacab;
                datacab = fim;
                datai = datacab.Substring(0, datacab.IndexOf("/"));
                datacab = datacab.Substring(datai.Length + 1);
                datamei = datacab.Substring(0, datacab.IndexOf("/"));
                datacab = datacab.Substring(datamei.Length + 1);
                datafim = datacab;
                if (datai.Length == 1)
                {
                    datai = "0" + datai;
                }
                if (datamei.Length == 1)
                {
                    datamei = "0" + datamei;
                }
                datacab = datai + datamei + datafim.Substring(0, 4);
                fim = datacab;
                hora_verao = comeco + fim;
            }
                msg = data + horario + hora_verao;
            }
            else
            {
                msg = data+horario;
            }
            Comando comando = new Comando(Protocolo.CMD_DATA_HORA, Protocolo.SET, msg);

            progressBar1.Maximum = 1;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;

            // Inicia comando
            switch (coletor.Modo)
            {
                case "IP":
                    EnviaComandoTCP(comando, coletor);
                    break;
                case "Serial":
                    EnviaComandoSerial(comando, coletor);
                    break;
            }

            progressBar1.Value++;
            

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        public void EnviaTodosFuncionarios(Coletor coletor)
        {
            

            naoEnviados.Clear();
            progressBar1.Maximum = funcionarios.Count;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            foreach (Funcionario func in funcionarios)
            {
                if (abortaEnvio)
                {
                    progressBar1.Value = 0;
                    abortaEnvio = false;
                    return;
                }
                string msg = func.ToString();
                Comando cmd = new Comando(Protocolo.CMD_FUNCIONARIO, Protocolo.SET, msg);

                // Inicia comando
                switch (coletor.Modo)
                {
                    case "IP":
                        EnviaComandoTCP(cmd, coletor);
                        break;
                    case "Serial":
                        EnviaComandoSerial(cmd, coletor);
                        break;
                }
                
                progressBar1.Value++;
            }

            if (naoEnviados.Count != 0)
                labelStatus.Text = "Concluído. Erro no envio de " + naoEnviados.Count + " funcionário(s)";
            else
                labelStatus.Text = "Concluido";

            
            
        }
        public void EnviaTodosFuncionarios(Coletor coletor, List<Funcionario> lista)
        {


            naoEnviados.Clear();
            progressBar1.Maximum = lista.Count;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            int i = 0;
            foreach (Funcionario func in lista)
            {
                if (!Cancela)
                {
                    if (abortaEnvio)
                    {
                        progressBar1.Value = 0;
                        abortaEnvio = false;
                        return;
                    }
                    string msg = func.ToString();
                    Comando cmd = new Comando(Protocolo.CMD_FUNCIONARIO, Protocolo.SET, msg);
                    i++;
                    lbl_funcionarios.Text = "Func. " + i + " de " + ManagementEmployee.lista_func.Count;
                    // Inicia comando
                    switch (coletor.Modo)
                    {
                        case "IP":
                            EnviaComandoTCP(cmd, coletor);
                            break;
                        case "Serial":
                            EnviaComandoSerial(cmd, coletor);
                            break;
                    }

                    progressBar1.Value++;
                }
            }
            if (naoEnviados.Count != 0)
            {
                labelStatus.Text = "Concluído. Erro no envio de " + naoEnviados.Count + " funcionário(s)";
                StreamWriter escreveLOG = new StreamWriter(Application.StartupPath+"\\naoEnviados.txt");
                foreach (String st in naoEnviados)
                {
                    escreveLOG.WriteLine(st);
                }
                escreveLOG.Close();
            }
            else
                labelStatus.Text = "Concluido";



        }
        public void EnviaUMFuncionario(Coletor coletor)
        {


            naoEnviados.Clear();
            progressBar1.Maximum = ManagementEmployee.lista_func.Count;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            foreach (Funcionario func in ManagementEmployee.lista_func)
            {
                    if (abortaEnvio)
                    {
                        progressBar1.Value = 0;
                        abortaEnvio = false;
                        return;
                    }
                    string msg = func.ToString();
                    Comando cmd = new Comando(Protocolo.CMD_FUNCIONARIO, Protocolo.SET, msg);

                    // Inicia comando
                    switch (coletor.Modo)
                    {
                        case "IP":
                            EnviaComandoTCP(cmd, coletor);
                            break;
                        case "Serial":
                            EnviaComandoSerial(cmd, coletor);
                            break;
                    }

                    progressBar1.Value++;


                    if (naoEnviados.Count != 0)
                    {
                        labelStatus.Text = "Concluído. Erro no envio de " + naoEnviados.Count + " funcionário(s)";
                        return;
                    }
                    else
                    {
                        labelStatus.Text = "Concluido";
                        return;
                    }
                progressBar1.Value++;
            }
            MessageBox.Show("Funcionário não pertence ao grupo", "Aviso");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                TravaComandos();
                progressBar1.Value = 0;
                labelStatus.Text = string.Empty;
                switch (col.Modo)
                {
                    case "IP":
                        RecebeTodasMarcacoesTCP(col);
                        break;
                    case "Serial":
                        RecebeTodasMarcacoesSerial(col);
                        break;
                }


                DestravaComandos();
            }
        }

        public void RecebeTodasMarcacoesSerial(Coletor coletor)
        {
            bool executa = false;
            string arquivo = "C:\\marcacao-rep.txt";
            List<string> marcacoes = new List<string>();
            int j = 0;
            Comando cmd = new Comando(Protocolo.CMD_MARCACAO, Protocolo.READ, "1");

            serialPort1.PortName = coletor.COM;
            serialPort1.BaudRate = 9600;
            try
            {
                serialPort1.Open();

                byte[] envio = cmd.ToByte();
                serialPort1.Write(envio, 0, envio.Length);

                byte[] recebimento = new byte[71];
                string msg = string.Empty;
                executa = true;
                IniciaTempoTimeout();
                do
                {
                    if (!Cancela)
                    {
                        Application.DoEvents();
                        if (serialPort1.BytesToRead >= 71)
                        {
                            serialPort1.Read(recebimento, 0, recebimento.Length);
                            msg = msg + Protocolo.ConverteByteToString(recebimento);
                            executa = false;
                        }
                        expirouTimeout = VerificaTimeout();
                        if (expirouTimeout)
                            executa = false;
                    }
                    else
                    {
                        progressBar1.Value = 0;
                        return;
                    }
                } while (executa);
                if (expirouTimeout)
                {
                    labelStatus.Text = "Timeout na comunicação";
                    progressBar1.Value = 0;
                    expirouTimeout = false;
                    serialPort1.Close();
                    return;
                }

                // Verifica a primeira mensagem
                if (Comando.VerificaCRC(msg))
                {
                    Comando resp = new Comando(msg);
                    if (resp.Mensagem.Equals("15"))
                    {
                        labelStatus.Text = "Erro ao receber marcações do coletor";
                        progressBar1.Value = 0;
                        return;
                    }

                    marcacoes.Add(resp.Mensagem.Substring(8, resp.Mensagem.Length - 8));
                    long nMarcacoes = Convert.ToInt64(resp.Mensagem.Substring(0, 8), 16);
                    progressBar1.Maximum = (int)nMarcacoes;
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 1;

                    for (int i = 1; i < nMarcacoes; i++)
                    {
                        if (!Cancela)
                        {
                            string info = "06";
                            Comando comando = new Comando(Protocolo.CMD_MARCACAO, Protocolo.INFO, info);
                            envio = comando.ToByte();
                            serialPort1.Write(envio, 0, envio.Length);

                            recebimento = new byte[71];
                            msg = string.Empty;
                            executa = true;
                            IniciaTempoTimeout();
                            do
                            {
                                Application.DoEvents();
                                if (serialPort1.BytesToRead >= 71)
                                {
                                    serialPort1.Read(recebimento, 0, recebimento.Length);
                                    j++;
                                    lbl_funcionarios.Text = "Marcações recebidas: " + j + " de " + nMarcacoes;
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    executa = false;
                                }
                                expirouTimeout = VerificaTimeout();
                                if (expirouTimeout)
                                    executa = false;
                            } while (executa);
                            if (expirouTimeout)
                            {
                                labelStatus.Text = "Timeout na comunicação";
                                progressBar1.Value = 0;
                                expirouTimeout = false;
                                serialPort1.Close();
                                return;
                            }

                            progressBar1.Value++;
                        }
                        else
                        {
                            progressBar1.Value = 0;
                            serialPort1.Close();
                            return;
                        }

                        if (Comando.VerificaCRC(msg))
                        {
                            resp = new Comando(msg);
                            if (resp.Mensagem.Equals("15"))
                            {
                                labelStatus.Text = "Erro ao receber marcações do coletor";
                                progressBar1.Value = 0;
                                return;
                            }
                            else
                            {
                                marcacoes.Add(resp.Mensagem.Substring(8, resp.Mensagem.Length - 8));
                            }
                        }
                    }
                }
                else
                {
                    labelStatus.Text = "Erro ao receber marcações do coletor";
                    progressBar1.Value = 0;
                    return;
                }


                foreach (string s in marcacoes)
                {
                    Marcacao marc = new Marcacao(s, coletor.ID, coletor.EmpresaID);
                    marc.Salvar();
                }



                serialPort1.Close();

                labelStatus.Text = "Marcações recebidas";
            }
            catch (Exception ex)
            {
                serialPort1.Close();
                MessageBox.Show(ex.Message);
                progressBar1.Value = 0;
                return;
            }
            
        }

        public void RecebeTodasMarcacoesTCP(Coletor coletor)
        {
            

            bool executa = false;
            string arquivo = "C:\\marcacao-rep.txt";
            List<string> marcacoes = new List<string>();
            int j = 0;
            Comando cmd = new Comando(Protocolo.CMD_MARCACAO, Protocolo.READ, "1");

            TcpClient sock = new TcpClient();
            try
            {
                sock.Connect(coletor.IP, coletor.Porta);
                NetworkStream stream = sock.GetStream();

                byte[] envio = cmd.ToByte();
                stream.Write(envio, 0, envio.Length);

                byte[] recebimento = new byte[71];
                string msg = string.Empty;
                executa = true;
                IniciaTempoTimeout();
                do
                {
                    if (!Cancela)
                    {
                        Application.DoEvents();
                        if (sock.Available >= 71)
                        {
                            stream.Read(recebimento, 0, recebimento.Length);
                            msg = msg + Protocolo.ConverteByteToString(recebimento);
                            executa = false;
                        }
                        expirouTimeout = VerificaTimeout();
                        if (expirouTimeout)
                            executa = false;
                    }
                    else
                    {
                        executa = false;
                        progressBar1.Value = 0;
                        break;
                    }
                } while (executa);
                if (expirouTimeout)
                {
                    labelStatus.Text = "Timeout na comunicação";
                    progressBar1.Value = 0;
                    expirouTimeout = false;
                    sock.Close();
                    return;
                }

                // Verifica a primeira mensagem
                if (Comando.VerificaCRC(msg))
                {
                    Comando resp = new Comando(msg);
                    if (resp.Mensagem.Equals("15"))
                    {
                        labelStatus.Text = "Erro ao receber marcações do coletor";
                        progressBar1.Value = 0;
                        return;
                    }

                    marcacoes.Add(resp.Mensagem.Substring(8, resp.Mensagem.Length - 8));
                    long nMarcacoes = Convert.ToInt64(resp.Mensagem.Substring(0, 8), 16);
                    progressBar1.Maximum = (int)nMarcacoes;
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 1;
                    for (int i = 1; i < nMarcacoes; i++)
                    {
                        if (!Cancela)
                        {
                            string info = "06";
                            Comando comando = new Comando(Protocolo.CMD_MARCACAO, Protocolo.INFO, info);
                            envio = comando.ToByte();
                            stream.Write(envio, 0, envio.Length);
                            progressBar1.Value++;

                            recebimento = new byte[71];
                            msg = string.Empty;
                            executa = true;
                            IniciaTempoTimeout();
                            do
                            {
                                Application.DoEvents();
                                if (sock.Available >= 71)
                                {
                                    stream.Read(recebimento, 0, recebimento.Length);
                                    j++;
                                    lbl_funcionarios.Text = "Marcações recebidas: " + j + " de " + nMarcacoes;
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    executa = false;
                                }
                                expirouTimeout = VerificaTimeout();
                                if (expirouTimeout)
                                    executa = false;
                            } while (executa);
                            if (expirouTimeout)
                            {
                                labelStatus.Text = "Timeout na comunicação";
                                progressBar1.Value = 0;
                                expirouTimeout = false;
                                sock.Close();
                                return;
                            }
                        }
                        else
                        {
                            progressBar1.Value = 0;
                            sock.Close();
                            return;
                        }

                        if (Comando.VerificaCRC(msg))
                        {
                            resp = new Comando(msg);
                            if (resp.Mensagem.Equals("15"))
                            {
                                labelStatus.Text = "Erro ao receber marcações do coletor";
                                progressBar1.Value = 0;
                                return;
                            }
                            else
                            {
                                marcacoes.Add(resp.Mensagem.Substring(8, resp.Mensagem.Length - 8));
                            }
                        }
                    }
                    //for (int i = 1; i < nMarcacoes; i++)
                    //{
                    //    if (!Cancela)
                    //    {
                    //        string info = "06";
                    //        Comando comando = new Comando(Protocolo.CMD_MARCACAO, Protocolo.INFO, info);
                    //        envio = comando.ToByte();
                    //        stream.Write(envio, 0, envio.Length);
                    //        j++;
                    //        lbl_funcionarios.Text = "Marcações Recebidas: " + j + " de " + nMarcacoes;
                    //        recebimento = new byte[71];
                    //        msg = string.Empty;
                    //        executa = true;
                    //        IniciaTempoTimeout();
                    //        do
                    //        {
                    //            Application.DoEvents();
                    //            if (sock.Available >= 71)
                    //            {
                    //                stream.Read(recebimento, 0, recebimento.Length);
                                    
                    //                msg = msg + Protocolo.ConverteByteToString(recebimento);
                    //                executa = false;
                    //            }
                    //            expirouTimeout = VerificaTimeout();
                    //            if (expirouTimeout)
                    //                executa = false;
                    //        } while (executa);
                    //        if (expirouTimeout)
                    //        {
                    //            labelStatus.Text = "Timeout na comunicação";
                    //            progressBar1.Value = 0;
                    //            expirouTimeout = false;
                    //            sock.Close();
                    //            return;
                    //        }
                    //        else
                    //        {
                    //            progressBar1.Value = 0;
                    //            sock.Close();
                    //            return;
                    //        }

                    //        progressBar1.Value++;

                    //        if (Comando.VerificaCRC(msg))
                    //        {
                    //            resp = new Comando(msg);
                    //            if (resp.Mensagem.Equals("15"))
                    //            {
                    //                labelStatus.Text = "Erro ao receber marcações do coletor";
                    //                progressBar1.Value = 0;
                    //                return;
                    //            }
                    //            else
                    //            {
                    //                marcacoes.Add(resp.Mensagem.Substring(8, resp.Mensagem.Length - 8));
                    //            }
                    //        }
                    //        Comando comand = new Comando(Protocolo.CMD_MARCACAO, Protocolo.INFO, "06");
                    //        envio = comand.ToByte();
                    //        stream.Write(envio, 0, envio.Length);
                    //    }
                    //    else
                    //    {
                    //        progressBar1.Value = 0;
                    //        return;
                    //    }
                    //}
                }
                else
                {
                    labelStatus.Text = "Erro ao receber marcações do coletor";
                    progressBar1.Value = 0;
                    return;
                }


                foreach (string s in marcacoes)
                {
                    Marcacao marc = new Marcacao(s, coletor.ID, coletor.EmpresaID);
                    marc.Salvar();
                }



                sock.Close();

                labelStatus.Text = "Marcações recebidas";
            }
            catch (Exception ex)
            {
                sock.Close();
                MessageBox.Show(ex.Message);
                progressBar1.Value = 0;
                return;
            }
            
        }

        public long ConverteHexaToLong(string hex)
        {
            long res = Convert.ToInt64(hex, 16);

            return res;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                TravaComandos();
                progressBar1.Value = 0;
                labelStatus.Text = string.Empty;
                // Inicia comunicação
                switch (col.Modo)
                {
                    case "IP":
                        RecebeNovasMarcacoesTCP(col);
                        break;
                    case "Serial":
                        RecebeNovasMarcacoesSerial(col);
                        break;
                }


                DestravaComandos();
            }
        }

        public void RecebeNovasMarcacoesTCP(Coletor coletor)
        {
            bool executa = false;
            string arquivo = "C:\\marcacao-rep.txt";
            List<string> marcacoes = new List<string>();
            int j = 0;
            Comando cmd = new Comando(Protocolo.CMD_MARCACAO, Protocolo.READ, "0");

            TcpClient sock = new TcpClient();
            try
            {
                sock.Connect(coletor.IP, coletor.Porta);
                NetworkStream stream = sock.GetStream();

                byte[] envio = cmd.ToByte();
                stream.Write(envio, 0, envio.Length);

                byte[] recebimento;
                string msg = string.Empty;
                executa = true;
                IniciaTempoTimeout();
                do
                {
                    Application.DoEvents();
                    if ((sock.Available == 71))
                    {
                        recebimento = new byte[71];
                        stream.Read(recebimento, 0, recebimento.Length);
                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                        executa = false;
                    }
                    if ((sock.Available == 15))
                    {
                        recebimento = new byte[15];
                        stream.Read(recebimento, 0, recebimento.Length);
                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                        executa = false;
                    }
                    expirouTimeout = VerificaTimeout();
                    if (expirouTimeout)
                        executa = false;
                } while (executa);
                if (expirouTimeout)
                {
                    labelStatus.Text = "Timeout na comunicação";
                    progressBar1.Value = 0;
                    expirouTimeout = false;
                    sock.Close();
                    return;
                }

                // Verifica a primeira mensagem
                if (Comando.VerificaCRC(msg))
                {
                    Comando resp = new Comando(msg);
                    if (resp.Mensagem.Equals("15"))
                    {
                        labelStatus.Text = "Não existem novas marcações";
                        progressBar1.Value = 0;
                        sock.Close();
                        return;
                    }

                    marcacoes.Add(resp.Mensagem.Substring(8, resp.Mensagem.Length - 8));
                    long nMarcacoes = Convert.ToInt64(resp.Mensagem.Substring(0, 8), 16);
                    progressBar1.Maximum = (int)nMarcacoes;
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 0;
                    if (nMarcacoes > 1)
                    {
                        for (int i = 1; i < nMarcacoes; i++)
                        {
                            if (!Cancela)
                            {
                                string info = "06";
                                Comando comando = new Comando(Protocolo.CMD_MARCACAO, Protocolo.INFO, info);
                                envio = comando.ToByte();
                                stream.Write(envio, 0, envio.Length);
                                progressBar1.Value++;

                                recebimento = new byte[71];
                                msg = string.Empty;
                                executa = true;
                                IniciaTempoTimeout();
                                do
                                {
                                    Application.DoEvents();
                                    if (sock.Available >= 71)
                                    {
                                        stream.Read(recebimento, 0, recebimento.Length);
                                        j++;
                                        lbl_funcionarios.Text = "Marcações recebidas: " + j + " de " +nMarcacoes;
                                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    labelStatus.Text = "Timeout na comunicação";
                                    progressBar1.Value = 0;
                                    expirouTimeout = false;
                                    sock.Close();
                                    return;
                                }
                            }
                            else
                            {
                                progressBar1.Value = 0;
                                sock.Close();
                                return;
                            }

                            if (Comando.VerificaCRC(msg))
                            {
                                resp = new Comando(msg);
                                if (resp.Mensagem.Equals("15"))
                                {
                                    labelStatus.Text = "Erro ao receber marcações do coletor";
                                    progressBar1.Value = 0;
                                    return;
                                }
                                else
                                {
                                    marcacoes.Add(resp.Mensagem.Substring(8, resp.Mensagem.Length - 8));
                                }
                            }
                        }
                    }
                    Comando comand = new Comando(Protocolo.CMD_MARCACAO, Protocolo.INFO, "06");
                    envio = comand.ToByte();
                    stream.Write(envio, 0, envio.Length);
                    progressBar1.Value++;
                }
                else
                {
                    labelStatus.Text = "Erro ao receber marcações do coletor";
                    progressBar1.Value = 0;
                    return;
                }


                foreach (string s in marcacoes)
                {
                    Marcacao marc = new Marcacao(s, coletor.ID, coletor.EmpresaID);
                    marc.Salvar();
                }



                sock.Close();

                if (marcacoes.Count == 0)
                    labelStatus.Text = "Não existem novas marcações";
                else
                    labelStatus.Text = "Concluído";
            }
            catch (Exception ex)
            {
                sock.Close();
                MessageBox.Show(ex.Message);
                progressBar1.Value = 0;
                return;
            }
            
        }

        public void RecebeNovasMarcacoesSerial(Coletor coletor)
        {
            bool executa = false;
            string arquivo = "C:\\marcacao-rep.txt";
            List<string> marcacoes = new List<string>();
            int j = 0;
            Comando cmd = new Comando(Protocolo.CMD_MARCACAO, Protocolo.READ, "0");

            
            try
            {
                serialPort1.PortName = coletor.COM;
                serialPort1.BaudRate = 9600;
                serialPort1.Open();
                
                byte[] envio = cmd.ToByte();
                serialPort1.Write(envio, 0, envio.Length);

                byte[] recebimento;
                string msg = string.Empty;
                executa = true;
                IniciaTempoTimeout();
                do
                {
                    Application.DoEvents();
                    if(serialPort1.BytesToRead == 71)
                    {
                        recebimento = new byte[71];
                        serialPort1.Read(recebimento, 0, recebimento.Length);
                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                        executa = false;
                    }
                    if(serialPort1.BytesToRead == 15)
                    {
                        recebimento = new byte[15];
                        serialPort1.Read(recebimento, 0, recebimento.Length);
                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                        executa = false;
                    }
                    expirouTimeout = VerificaTimeout();
                    if (expirouTimeout)
                        executa = false;
                } while (executa);
                if (expirouTimeout)
                {
                    labelStatus.Text = "Timeout na comunicação";
                    progressBar1.Value = 0;
                    expirouTimeout = false;
                    serialPort1.Close();
                    return;
                }

                // Verifica a primeira mensagem
                if (Comando.VerificaCRC(msg))
                {
                    Comando resp = new Comando(msg);
                    if (resp.Mensagem.Equals("15"))
                    {
                        labelStatus.Text = "Não existem novas marcações";
                        progressBar1.Value = 0;
                        serialPort1.Close();
                        return;
                    }

                    marcacoes.Add(resp.Mensagem.Substring(8, resp.Mensagem.Length - 8));
                    long nMarcacoes = Convert.ToInt64(resp.Mensagem.Substring(0, 8), 16);
                    progressBar1.Maximum = (int)nMarcacoes;
                    progressBar1.Minimum = 0;
                    progressBar1.Value = 0;
                    if (nMarcacoes > 1)
                    {
                        for (int i = 1; i < nMarcacoes; i++)
                        {
                            if (!Cancela)
                            {
                                string info = "06";
                                Comando comando = new Comando(Protocolo.CMD_MARCACAO, Protocolo.INFO, info);
                                envio = comando.ToByte();
                                serialPort1.Write(envio, 0, envio.Length);
                                progressBar1.Value++;

                                recebimento = new byte[71];
                                msg = string.Empty;
                                executa = true;
                                IniciaTempoTimeout();
                                do
                                {
                                    Application.DoEvents();
                                    if (serialPort1.BytesToRead >= 71)
                                    {
                                        serialPort1.Read(recebimento, 0, recebimento.Length);
                                        j++;
                                        lbl_funcionarios.Text = "Marcações recebidas: " + j + " de " + nMarcacoes ;
                                        msg = msg + Protocolo.ConverteByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    labelStatus.Text = "Timeout na comunicação";
                                    progressBar1.Value = 0;
                                    expirouTimeout = false;
                                    serialPort1.Close();
                                    return;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("15"))
                                    {
                                        labelStatus.Text = "Erro ao receber marcações do coletor";
                                        progressBar1.Value = 0;
                                        return;
                                    }
                                    else
                                    {
                                        marcacoes.Add(resp.Mensagem.Substring(8, resp.Mensagem.Length - 8));
                                    }
                                }
                            }
                        }
                        Comando comand = new Comando(Protocolo.CMD_MARCACAO, Protocolo.INFO, "06");
                        envio = comand.ToByte();
                        serialPort1.Write(envio, 0, envio.Length);
                        progressBar1.Value++;
                    }
                    else
                    {
                        labelStatus.Text = "Erro ao receber marcações do coletor";
                        progressBar1.Value = 0;
                        return;
                    }
                }
                else
                {
                    progressBar1.Value = 0;
                    serialPort1.Close();
                    return;
                }

                foreach (string s in marcacoes)
                {
                    Marcacao marc = new Marcacao(s, coletor.ID, coletor.EmpresaID);
                    marc.Salvar();
                }



                serialPort1.Close();

                if (marcacoes.Count == 0)
                    labelStatus.Text = "Não existem novas marcações";
                else
                    labelStatus.Text = "Concluído";
            }
            catch (Exception ex)
            {
                serialPort1.Close();
                MessageBox.Show(ex.Message);
                progressBar1.Value = 0;
                return;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                TravaComandos();
                progressBar1.Value = 0;
                labelStatus.Text = string.Empty;
                EnviaTodosTemplates(col);

                DestravaComandos();
            }
        }

        public void EnviaTodosTemplates(Coletor coletor)
        {
            naoEnviados.Clear();
            
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            int nFuncBio = 0;
            #region Verifica quantos funcionários possuem biometria
            foreach (Funcionario func in funcionarios)
            {
                if (func.Permissao.Biometria)
                    nFuncBio++;
            }
            #endregion
            progressBar1.Maximum = nFuncBio;

            foreach (Funcionario func in funcionarios)
            {
                if (func.Permissao.Biometria)
                {
                    string msg = func.ToBio1();
                    Comando cmd = new Comando(Protocolo.CMD_BIOMETRIA, Protocolo.SET, msg);

                    // Inicia comando
                    switch (coletor.Modo)
                    {
                        case "IP":
                            EnviaComandoTCP(cmd, coletor);
                            break;
                        case "Serial":
                            EnviaComandoSerial(cmd, coletor);
                            break;
                    }
                    msg = func.ToBio2();
                    cmd = new Comando(Protocolo.CMD_BIOMETRIA, Protocolo.SET, msg);

                    // Inicia comando
                    switch (coletor.Modo)
                    {
                        case "IP":
                            EnviaComandoTCP(cmd, coletor);
                            break;
                        case "Serial":
                            EnviaComandoSerial(cmd, coletor);
                            break;
                    }
                    msg = func.ToBio3();
                    cmd = new Comando(Protocolo.CMD_BIOMETRIA, Protocolo.SET, msg);

                    // Inicia comando
                    switch (coletor.Modo)
                    {
                        case "IP":
                            EnviaComandoTCP(cmd, coletor);
                            break;
                        case "Serial":
                            EnviaComandoSerial(cmd, coletor);
                            break;
                    }
                    progressBar1.Value++;
                }
            }

            if (naoEnviados.Count != 0)
                labelStatus.Text = "Concluído. Erro no envio de " + naoEnviados.Count + " template(s)";
            else
                labelStatus.Text = "Concluido";

            

        }


        

        private void button6_Click(object sender, EventArgs e)
        {
            TravaComandos();
            progressBar1.Value = 0;
            labelStatus.Text = string.Empty;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                EnviaSingleTemplate(col);

                DestravaComandos();
            }
        }

        public void EnviaSingleTemplate(Coletor coletor)
        {
            naoEnviados.Clear();
            progressBar1.Maximum = ManagementEmployee.lista_func.Count;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            int i = 0;
            foreach(Funcionario func in ManagementEmployee.lista_func)
            {
                i++;
                lbl_funcionarios.Text = "Func. " + i + " de " + ManagementEmployee.lista_func.Count;
                if (func.Permissao.Biometria)
                {
                    string arquivo = MainForm.root + "\\" + func.PIS + "\\" + func.PIS + "-1.max";
                    if (File.Exists(arquivo))
                    {
                        string msg = func.ToBio1();
                        Comando cmd = new Comando(Protocolo.CMD_BIOMETRIA, Protocolo.SET, msg);

                        // Inicia comando
                        switch (coletor.Modo)
                        {
                            case "IP":
                                EnviaComandoTCP(cmd, coletor);
                                break;
                            case "Serial":
                                EnviaComandoSerial(cmd, coletor);
                                break;
                        }
                    }
                    arquivo = MainForm.root + "\\" + func.PIS + "\\" + func.PIS + "-2.max";
                    if (File.Exists(arquivo))
                    {
                        string msg = func.ToBio2();
                        Comando cmd = new Comando(Protocolo.CMD_BIOMETRIA, Protocolo.SET, msg);

                        // Inicia comando
                        switch (coletor.Modo)
                        {
                            case "IP":
                                EnviaComandoTCP(cmd, coletor);
                                break;
                            case "Serial":
                                EnviaComandoSerial(cmd, coletor);
                                break;
                        }
                    }
                    arquivo = MainForm.root + "\\" + func.PIS + "\\" + func.PIS + "-3.max";
                    if (File.Exists(arquivo))
                    {
                        string msg = func.ToBio3();
                        Comando cmd = new Comando(Protocolo.CMD_BIOMETRIA, Protocolo.SET, msg);

                        // Inicia comando
                        switch (coletor.Modo)
                        {
                            case "IP":
                                EnviaComandoTCP(cmd, coletor);
                                break;
                            case "Serial":
                                EnviaComandoSerial(cmd, coletor);
                                break;
                        }
                    }
                }
                progressBar1.Value++;
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TravaComandos();

            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                progressBar1.Value = 0;
                labelStatus.Text = string.Empty;
                ExcluiFuncionario();

                DestravaComandos();
            }
        }

        public void ExcluiFuncionario()
        {
            int i = 0;
            int j = 0;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                progressBar1.Maximum = ManagementEmployee.lista_func.Count;
                progressBar1.Minimum = 0;
                progressBar1.Value = 0;
                i++;
                lbl_coletores.Text = "Coletor " + i + " de " + ManagementGroupForm.lista_coletor.Count.ToString();
                foreach (Funcionario func in ManagementEmployee.lista_func)
                {
                    if (!Cancela)
                    {
                        if (j >= ManagementEmployee.lista_func.Count)
                        {
                            j = 0;
                        }
                        else
                        {
                            j++;
                        }
                        lbl_funcionarios.Text = "Func. " + j + " de " + ManagementEmployee.lista_func.Count;
                        string msg = func.PIS + "1";
                        Comando cmd = new Comando(Protocolo.CMD_EXCLUSAO, Protocolo.SET, msg);

                        // Inicia comando
                        switch (col.Modo)
                        {
                            case "IP":
                                EnviaComandoTCP(cmd, col);
                                break;
                            case "Serial":
                                EnviaComandoSerial(cmd, col);
                                break;
                        }
                        progressBar1.Value++;
                    }
                    else
                    {
                        progressBar1.Value = 0;
                        return;
                    }
                }
            }

            

            

        }

        private void button10_Click(object sender, EventArgs e)
        {
            TravaComandos();
            progressBar1.Value = 0;
            labelStatus.Text = string.Empty;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                ExcluiSingleTemplate(col);

                DestravaComandos();
            }
        }

        public void ExcluiSingleTemplate(Coletor coletor)
        {
            
            progressBar1.Maximum = 1;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            int i = 0;
            foreach (Funcionario func in ManagementEmployee.lista_func)
            {
                if (func.Permissao.Biometria)
                {

                    string msg = func.PIS + "0";
                    Comando cmd = new Comando(Protocolo.CMD_EXCLUSAO, Protocolo.SET, msg);
                    i++;
                    lbl_funcionarios.Text = "Func. " + i + " de " + ManagementEmployee.lista_func.Count;
                    // Inicia comando
                    switch (coletor.Modo)
                    {
                        case "IP":
                            EnviaComandoTCP(cmd, coletor);
                            break;
                        case "Serial":
                            EnviaComandoSerial(cmd, coletor);
                            break;
                    }
                    if (naoEnviados.Count != 0)
                        labelStatus.Text = "Erro ao apagar " + naoEnviados.Count + " template(s)";
                    //else
                        //labelStatus.Text = "Concluido";
                }
                else
                {
                    labelStatus.Text = "Funcionário não possui biometria";
                }
                
            }

        }
        public void ExcluiTodosTemplate(Coletor coletor)
        {

            progressBar1.Maximum = 1;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            int i = 0;
                Comando cmd = new Comando(Protocolo.CMD_EXCLUSAO, Protocolo.SET, "0000000000000");
                //i++;
                //lbl_funcionarios.Text = "Func. " + i + " de " + ManagementEmployee.lista_func.Count;
                // Inicia comando
                switch (coletor.Modo)
                {
                    case "IP":
                        EnviaComandoTCP(cmd, coletor);
                        break;
                    case "Serial":
                        EnviaComandoSerial(cmd, coletor);
                        break;
                }
                if (naoEnviados.Count != 0)
                {
                    labelStatus.Text = "Erro na exclusão dos templates";
                }
                else
                {
                    //labelStatus.Text = "Concluido";
                }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            TravaComandos();
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = 1;
            labelStatus.Text = string.Empty;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                foreach (Funcionario func in ManagementEmployee.lista_func)
                {
                    RecebeSingleTemplate(col, func);

                    DestravaComandos();
                }
            }
        }

        public void RecebeSingleTemplate(Coletor coletor, Funcionario func)
        {
            naoEnviados.Clear();
            


            bool recebeu = false;
            

            if (func.Permissao.Biometria)
            {
                // Biometria 1
                Comando cmd = new Comando(Protocolo.CMD_BIOMETRIA, Protocolo.READ, func.PIS+"1");

                bool executa = false;
                // Inicia comando
                switch (coletor.Modo)
                {
                        
                    case "IP":
                        #region IP
                        try
                        {
                            TcpClient sock = new TcpClient();
                            sock.Connect(coletor.IP,coletor.Porta);
                            NetworkStream stream = sock.GetStream();

                            byte[] envio = cmd.ToByte();
                            stream.Write(envio, 0, envio.Length);

                            byte[] recebimento;
                            byte[] sucess = new byte[410];
                            string msg = string.Empty;
                            executa = true;
                            IniciaTempoTimeout();
                            do
                            {
                                Application.DoEvents();
                                if (sock.Available == 410)
                                {
                                    recebimento = new byte[410];
                                    stream.Read(recebimento,0,410);
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    sucess = recebimento;
                                    executa = false;
                                }
                                if (sock.Available == 15)
                                {
                                    recebimento = new byte[15];
                                    stream.Read(recebimento, 0, 15);
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    executa = false;
                                }
                                expirouTimeout = VerificaTimeout();
                                if (expirouTimeout)
                                    executa = false;
                            } while (executa);
                            if (expirouTimeout)
                            {
                                labelStatus.Text = "Timeout na comunicação";
                                progressBar1.Value = 0;
                                expirouTimeout = false;
                                sock.Close();
                                return;
                            }

                            if (Comando.VerificaCRC(msg))
                            {
                                Comando resp = new Comando(msg);
                                if (resp.Mensagem.Equals("15"))
                                {
                                    labelStatus.Text = "Funcionário não possui biometria no REP";
                                    progressBar1.Value++;
                                    sock.Close();
                                    return;
                                }
                                else
                                {
                                    byte[] template = new byte[384];
                                    for(int i =0;i<384;i++)
                                    {
                                        template[i] = sucess[i + 23];
                                    }
                                    SalvaTemplate1(template,func.PIS);
                                    recebeu = true;
                                    sock.Close();
                                    labelStatus.Text = "Digital 1 recebida";
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Aviso");
                        }
                        #endregion
                        break;
                    case "Serial":
                        #region Serial
                        try
                        {
                            serialPort1.PortName = coletor.COM;
                            serialPort1.BaudRate = 9600;
                            serialPort1.Open();
                            
                            byte[] envio = cmd.ToByte();
                            serialPort1.Write(envio, 0, envio.Length);

                            byte[] recebimento;
                            byte[] sucess = new byte[410];
                            string msg = string.Empty;
                            executa = true;
                            IniciaTempoTimeout();
                            do
                            {
                                Application.DoEvents();
                                if (serialPort1.BytesToRead == 410)
                                {
                                    recebimento = new byte[410];
                                    serialPort1.Read(recebimento, 0, 410);
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    sucess = recebimento;
                                    executa = false;
                                }
                                if (serialPort1.BytesToRead == 15)
                                {
                                    recebimento = new byte[15];
                                    serialPort1.Read(recebimento, 0, 15);
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    executa = false;
                                }
                                expirouTimeout = VerificaTimeout();
                                if (expirouTimeout)
                                    executa = false;
                            } while (executa);
                            if (expirouTimeout)
                            {
                                labelStatus.Text = "Timeout na comunicação";
                                progressBar1.Value = 0;
                                expirouTimeout = false;
                                serialPort1.Close();
                                return;
                            }

                            if (Comando.VerificaCRC(msg))
                            {
                                Comando resp = new Comando(msg);
                                if (resp.Mensagem.Equals("15"))
                                {
                                    labelStatus.Text = "Funcionário não possui biometria no REP";
                                    progressBar1.Value++;
                                    serialPort1.Close();
                                    return;
                                }
                                else
                                {
                                    byte[] template = new byte[384];
                                    for (int i = 0; i < 384; i++)
                                    {
                                        template[i] = sucess[i + 23];
                                    }
                                    SalvaTemplate1(template, func.PIS);
                                    recebeu = true;
                                    serialPort1.Close();
                                    labelStatus.Text = "Digital 1 recebida";
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Aviso");
                        }
                        #endregion
                        break;
                }

                // Biometria 2
                cmd = new Comando(Protocolo.CMD_BIOMETRIA, Protocolo.READ, func.PIS + "2");

                executa = false;
                // Inicia comando
                switch (coletor.Modo)
                {

                    case "IP":
                        #region IP
                        try
                        {
                            TcpClient sock = new TcpClient();
                            sock.Connect(coletor.IP, coletor.Porta);
                            NetworkStream stream = sock.GetStream();

                            byte[] envio = cmd.ToByte();
                            stream.Write(envio, 0, envio.Length);

                            byte[] recebimento;
                            byte[] sucess = new byte[410];
                            string msg = string.Empty;
                            executa = true;
                            IniciaTempoTimeout();
                            do
                            {
                                Application.DoEvents();
                                if (sock.Available == 410)
                                {
                                    recebimento = new byte[410];
                                    stream.Read(recebimento, 0, 410);
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    sucess = recebimento;
                                    executa = false;
                                }
                                if (sock.Available == 15)
                                {
                                    recebimento = new byte[15];
                                    stream.Read(recebimento, 0, 15);
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    executa = false;
                                }
                                expirouTimeout = VerificaTimeout();
                                if (expirouTimeout)
                                    executa = false;
                            } while (executa);
                            if (expirouTimeout)
                            {
                                labelStatus.Text = "Timeout na comunicação";
                                progressBar1.Value = 0;
                                expirouTimeout = false;
                                sock.Close();
                                return;
                            }

                            if (Comando.VerificaCRC(msg))
                            {
                                Comando resp = new Comando(msg);
                                if (resp.Mensagem.Equals("15"))
                                {
                                    labelStatus.Text = "Funcionário não possui biometria no REP";
                                    progressBar1.Value++;
                                    sock.Close();
                                    return;
                                }
                                else
                                {
                                    byte[] template = new byte[384];
                                    for (int i = 0; i < 384; i++)
                                    {
                                        template[i] = sucess[i + 23];
                                    }
                                    SalvaTemplate2(template, func.PIS);
                                    recebeu = true;
                                    sock.Close();
                                    labelStatus.Text = "Digital 2 recebida";
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Aviso");
                        }
                        #endregion
                        break;
                    case "Serial":
                        #region Serial
                        try
                        {
                            serialPort1.PortName = coletor.COM;
                            serialPort1.BaudRate = 9600;
                            serialPort1.Open();

                            byte[] envio = cmd.ToByte();
                            serialPort1.Write(envio, 0, envio.Length);

                            byte[] recebimento;
                            byte[] sucess = new byte[410];
                            string msg = string.Empty;
                            executa = true;
                            IniciaTempoTimeout();
                            do
                            {
                                Application.DoEvents();
                                if (serialPort1.BytesToRead == 410)
                                {
                                    recebimento = new byte[410];
                                    serialPort1.Read(recebimento, 0, 410);
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    sucess = recebimento;
                                    executa = false;
                                }
                                if (serialPort1.BytesToRead == 15)
                                {
                                    recebimento = new byte[15];
                                    serialPort1.Read(recebimento, 0, 15);
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    executa = false;
                                }
                                expirouTimeout = VerificaTimeout();
                                if (expirouTimeout)
                                    executa = false;
                            } while (executa);
                            if (expirouTimeout)
                            {
                                labelStatus.Text = "Timeout na comunicação";
                                progressBar1.Value = 0;
                                expirouTimeout = false;
                                serialPort1.Close();
                                return;
                            }

                            if (Comando.VerificaCRC(msg))
                            {
                                Comando resp = new Comando(msg);
                                if (resp.Mensagem.Equals("15"))
                                {
                                    labelStatus.Text = "Funcionário não possui biometria no REP";
                                    progressBar1.Value++;
                                    serialPort1.Close();
                                    return;
                                }
                                else
                                {
                                    byte[] template = new byte[384];
                                    for (int i = 0; i < 384; i++)
                                    {
                                        template[i] = sucess[i + 23];
                                    }
                                    SalvaTemplate2(template, func.PIS);
                                    recebeu = true;
                                    serialPort1.Close();
                                    labelStatus.Text = "Digital 2 recebida";
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Aviso");
                        }
                        #endregion
                        break;
                }

                // Biometria 3
                cmd = new Comando(Protocolo.CMD_BIOMETRIA, Protocolo.READ, func.PIS + "3");

                executa = false;
                // Inicia comando
                switch (coletor.Modo)
                {

                    case "IP":
                        #region IP
                        try
                        {
                            TcpClient sock = new TcpClient();
                            sock.Connect(coletor.IP, coletor.Porta);
                            NetworkStream stream = sock.GetStream();

                            byte[] envio = cmd.ToByte();
                            stream.Write(envio, 0, envio.Length);

                            byte[] recebimento;
                            byte[] sucess = new byte[410];
                            string msg = string.Empty;
                            executa = true;
                            IniciaTempoTimeout();
                            do
                            {
                                Application.DoEvents();
                                if (sock.Available == 410)
                                {
                                    recebimento = new byte[410];
                                    stream.Read(recebimento, 0, 410);
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    sucess = recebimento;
                                    executa = false;
                                }
                                if (sock.Available == 15)
                                {
                                    recebimento = new byte[15];
                                    stream.Read(recebimento, 0, 15);
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    executa = false;
                                }
                                expirouTimeout = VerificaTimeout();
                                if (expirouTimeout)
                                    executa = false;
                            } while (executa);
                            if (expirouTimeout)
                            {
                                labelStatus.Text = "Timeout na comunicação";
                                progressBar1.Value = 0;
                                expirouTimeout = false;
                                sock.Close();
                                return;
                            }

                            if (Comando.VerificaCRC(msg))
                            {
                                Comando resp = new Comando(msg);
                                if (resp.Mensagem.Equals("15"))
                                {
                                    labelStatus.Text = "Funcionário não possui biometria no REP";
                                    progressBar1.Value++;
                                    sock.Close();
                                    return;
                                }
                                else
                                {
                                    byte[] template = new byte[384];
                                    for (int i = 0; i < 384; i++)
                                    {
                                        template[i] = sucess[i + 23];
                                    }
                                    SalvaTemplate3(template, func.PIS);
                                    recebeu = true;
                                    sock.Close();
                                    labelStatus.Text = "Digital 3 recebida";
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Aviso");
                        }
                        #endregion
                        break;
                    case "Serial":
                        #region Serial
                        try
                        {
                            serialPort1.PortName = coletor.COM;
                            serialPort1.BaudRate = 9600;
                            serialPort1.Open();

                            byte[] envio = cmd.ToByte();
                            serialPort1.Write(envio, 0, envio.Length);

                            byte[] recebimento;
                            byte[] sucess = new byte[410];
                            string msg = string.Empty;
                            executa = true;
                            IniciaTempoTimeout();
                            do
                            {
                                Application.DoEvents();
                                if (serialPort1.BytesToRead == 410)
                                {
                                    recebimento = new byte[410];
                                    serialPort1.Read(recebimento, 0, 410);
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    sucess = recebimento;
                                    executa = false;
                                }
                                if (serialPort1.BytesToRead == 15)
                                {
                                    recebimento = new byte[15];
                                    serialPort1.Read(recebimento, 0, 15);
                                    msg = msg + Protocolo.ConverteByteToString(recebimento);
                                    executa = false;
                                }
                                expirouTimeout = VerificaTimeout();
                                if (expirouTimeout)
                                    executa = false;
                            } while (executa);
                            if (expirouTimeout)
                            {
                                labelStatus.Text = "Timeout na comunicação";
                                progressBar1.Value = 0;
                                expirouTimeout = false;
                                serialPort1.Close();
                                return;
                            }

                            if (Comando.VerificaCRC(msg))
                            {
                                Comando resp = new Comando(msg);
                                if (resp.Mensagem.Equals("15"))
                                {
                                    labelStatus.Text = "Funcionário não possui biometria no REP";
                                    progressBar1.Value++;
                                    serialPort1.Close();
                                    return;
                                }
                                else
                                {
                                    byte[] template = new byte[384];
                                    for (int i = 0; i < 384; i++)
                                    {
                                        template[i] = sucess[i + 23];
                                    }
                                    SalvaTemplate3(template, func.PIS);
                                    recebeu = true;
                                    serialPort1.Close();
                                    labelStatus.Text = "Digital 3 recebida";
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Aviso");
                        }
                        #endregion
                        break;
                }

                progressBar1.Value++;

                
                    
            }
            else
            {
                labelStatus.Text = "Funcionário não possui biometria";
                progressBar1.Value++;
            }

            

        }

        public bool SalvaTemplate1(byte[] template, string pis)
        {
            try
            {
                string diretorioUsuario = MainForm.root + "\\" + pis;
                if (!Directory.Exists(diretorioUsuario))
                    Directory.CreateDirectory(diretorioUsuario);
                string arquivo = diretorioUsuario + "\\" + pis + "-1.max";
                using (FileStream fs = File.Create(arquivo))
                {
                    fs.Write(template, 0, 384);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool SalvaTemplate2(byte[] template, string pis)
        {
            try
            {
                string diretorioUsuario = MainForm.root + "\\" + pis;
                if (!Directory.Exists(diretorioUsuario))
                    Directory.CreateDirectory(diretorioUsuario);
                string arquivo = diretorioUsuario + "\\" + pis + "-2.max";
                using (FileStream fs = File.Create(arquivo))
                {
                    fs.Write(template, 0, 384);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool SalvaTemplate3(byte[] template, string pis)
        {
            try
            {
                string diretorioUsuario = MainForm.root + "\\" + pis;
                if (!Directory.Exists(diretorioUsuario))
                    Directory.CreateDirectory(diretorioUsuario);
                string arquivo = diretorioUsuario + "\\" + pis + "-3.max";
                using (FileStream fs = File.Create(arquivo))
                {
                    fs.Write(template, 0, 384);
                }
                return true;
            }         
            catch (Exception ex)
            {
                return false;
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            TravaComandos();
            progressBar1.Value = 0;
            progressBar1.Maximum = funcionarios.Count;
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            labelStatus.Text = string.Empty;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                foreach (Funcionario func in funcionarios)
                {
                    RecebeSingleTemplate(col, func);
                    labelStatus.Text = String.Empty;
                }

                DestravaComandos();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                TravaComandos();
                progressBar1.Value = 0;
                labelStatus.Text = string.Empty;
                EnviaUMFuncionario(col);
                DestravaComandos();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            TravaComandos();
            progressBar1.Value = 0;
            labelStatus.Text = string.Empty;
            Cancela = false;
            int i = 0;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                if (!Cancela)
                {
                    i++;
                    lbl_coletores.Text = "Coletor " + i + " de " + ManagementGroupForm.lista_coletor.Count.ToString();
                    EnviaTodosFuncionarios(col, ManagementEmployee.lista_func);
                }
                else
                {
                    progressBar1.Value = 0;
                    lbl_coletores.Text = String.Empty;
                    lbl_funcionarios.Text = String.Empty;
                    break;
                }
            }
            DestravaComandos();
            lbl_coletores.Text = String.Empty;
            lbl_funcionarios.Text = String.Empty;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            Thread.Sleep(500);
            labelStatus.Text = string.Empty;
            Cancela = false;
            int i = 0;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                if (!Cancela)
                {
                    i++;
                    lbl_coletores.Text = "Coletor " + i + " de " + ManagementGroupForm.lista_coletor.Count.ToString();
                    TravaComandos();
                    List<Empresa> lista_empresas = DB.RetornaEmpresas();
                    Empresa emp = new Empresa();
                    foreach (Empresa selec_emp in lista_empresas)
                    {
                        if (col.EmpresaID == selec_emp.ID)
                        {
                            emp = selec_emp;
                            break;
                        }
                    }
                    EnviaInfoEmpresa(col, emp);
                    DestravaComandos();
                }
                else
                {
                    progressBar1.Value = 0;
                    break;
                }
            }
            lbl_coletores.Text = String.Empty;

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            Thread.Sleep(500);
            labelStatus.Text = string.Empty;
            Cancela = false;
            int i = 0;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                if (!Cancela)
                {
                    TravaComandos();
                    i++;
                    lbl_coletores.Text = "Coletor " + i + " de " + ManagementGroupForm.lista_coletor.Count.ToString();
                    EnviaDataHora(col);
                    DestravaComandos();
                }
                else
                {
                    progressBar1.Value = 0;
                    DestravaComandos();
                    lbl_coletores.Text = String.Empty;
                    break;
                }
            }
            lbl_coletores.Text = String.Empty;
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                TravaComandos();
                progressBar1.Value = 0;
                labelStatus.Text = string.Empty;
                EnviaUMFuncionario(col);
                DestravaComandos();
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            TravaComandos();
            labelStatus.Text = String.Empty;
            Cancela = false;
            TravaComandos();
            ExcluiFuncionario();
            progressBar1.Value = 0;
            //labelStatus.Text = "Concluído";
            lbl_funcionarios.Text = String.Empty;
            lbl_coletores.Text = String.Empty;
            DestravaComandos();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Cancela = false;
            int i = 0;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                if (!Cancela)
                {
                    TravaComandos();
                    progressBar1.Value = 0;
                    labelStatus.Text = string.Empty;
                    i++;
                    lbl_coletores.Text = "Coletor " + i + " de " + ManagementGroupForm.lista_coletor.Count.ToString();
                    switch (col.Modo)
                    {
                        case "IP":
                            RecebeTodasMarcacoesTCP(col);
                            break;
                        case "Serial":
                            RecebeTodasMarcacoesSerial(col);
                            break;
                    }
                    DestravaComandos();
                    lbl_funcionarios.Text = String.Empty;
                    lbl_coletores.Text = String.Empty;
                }
                else
                {
                    progressBar1.Value = 0;
                    break;
                }
                DestravaComandos();
                lbl_funcionarios.Text = String.Empty;
                lbl_coletores.Text = String.Empty;
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Cancela = false;
            int i = 0;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                if (!Cancela)
                {
                    TravaComandos();
                    progressBar1.Value = 0;
                    labelStatus.Text = string.Empty;
                    i++;
                    lbl_coletores.Text = "Coletor " + i + " de " + ManagementGroupForm.lista_coletor.Count.ToString();
                    // Inicia comunicação
                    switch (col.Modo)
                    {
                        case "IP":
                            RecebeNovasMarcacoesTCP(col);
                            break;
                        case "Serial":
                            RecebeNovasMarcacoesSerial(col);
                            break;
                    }


                    DestravaComandos();
                }
                else
                {
                    progressBar1.Value = 0;
                    lbl_coletores.Text = String.Empty;
                    lbl_funcionarios.Text = String.Empty;
                    DestravaComandos();
                    break;
                }
            }
            DestravaComandos();
            lbl_coletores.Text = String.Empty;
            lbl_funcionarios.Text = String.Empty;
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            TravaComandos();
            progressBar1.Value = 0;
            labelStatus.Text = string.Empty;
            Cancela = false;
            int i = 0;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                if (!Cancela)
                {
                    i++;
                    lbl_coletores.Text = "Coletor " + i + " de " + ManagementGroupForm.lista_coletor.Count.ToString();
                    ExcluiSingleTemplate(col);
                    progressBar1.Value++;
                    DestravaComandos();
                }
                else
                {
                    progressBar1.Value = 0;
                    lbl_coletores.Text = String.Empty;
                    lbl_funcionarios.Text = String.Empty;
                    break;
                }
            }
            //labelStatus.Text = "Concluído";
            lbl_coletores.Text = String.Empty;
            lbl_funcionarios.Text = String.Empty;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            TravaComandos();
            progressBar1.Maximum = ManagementEmployee.lista_func.Count;
            progressBar1.Value = 0;
            labelStatus.Text = string.Empty;
            Cancela = false;
            int i = 0;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                if (!Cancela)
                {
                    i++;
                    lbl_coletores.Text = "Coletor " + i + " de " + ManagementGroupForm.lista_coletor.Count.ToString();
                    EnviaSingleTemplate(col);
                    DestravaComandos();
                }
                else
                {
                    progressBar1.Value = 0;
                    lbl_coletores.Text = String.Empty;
                    lbl_funcionarios.Text = String.Empty;
                    break;
                }
            }
            lbl_coletores.Text = String.Empty;
            lbl_funcionarios.Text = String.Empty;
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            TravaComandos();
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = 1;
            labelStatus.Text = string.Empty;
            Cancela = false;
            int i = 0;
            int j = 0;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                i++;
                lbl_coletores.Text = "Coletor " + i + " de " + ManagementGroupForm.lista_coletor.Count.ToString();
                foreach (Funcionario func in ManagementEmployee.lista_func)
                {
                    if (j >= ManagementEmployee.lista_func.Count)
                    {
                        j = 1;
                    }
                    else
                    {
                        j++;
                    }
                    lbl_funcionarios.Text = "Func. " + j + " de " + ManagementEmployee.lista_func.Count;
                    if (!Cancela)
                    {
                        TravaComandos();
                        progressBar1.Value = 0;
                        RecebeSingleTemplate(col, func);
                        DestravaComandos();
                    }
                    else
                    {
                        progressBar1.Value = 0;
                        lbl_funcionarios.Text = String.Empty;
                        lbl_coletores.Text = String.Empty;
                        DestravaComandos();
                        break;
                    }
                }
            }
            labelStatus.Text = "Concluído";
            lbl_funcionarios.Text = String.Empty;
            lbl_coletores.Text = String.Empty;
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Cancela = true;
        }

        private void ManagementForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            TravaComandos();
            progressBar1.Value = 0;
            labelStatus.Text = string.Empty;
            Cancela = false;
            int i = 0;
            foreach (Coletor col in ManagementGroupForm.lista_coletor)
            {
                if (!Cancela)
                {
                    i++;
                    lbl_coletores.Text = "Coletor " + i + " de " + ManagementGroupForm.lista_coletor.Count.ToString();
                    lbl_funcionarios.Text = "Aguarde, excluindo...";
                    ExcluiTodosTemplate(col);
                    progressBar1.Value++;
                    DestravaComandos();
                }
                else
                {
                    progressBar1.Value = 0;
                    lbl_coletores.Text = String.Empty;
                    lbl_funcionarios.Text = String.Empty;
                    break;
                }
            }

            lbl_coletores.Text = String.Empty;
            lbl_funcionarios.Text = String.Empty;
        }


    }
}