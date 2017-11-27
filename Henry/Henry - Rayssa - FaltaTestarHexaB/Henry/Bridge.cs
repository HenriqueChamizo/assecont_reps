using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssepontoRep;
using Wr.Classes;
using System.Text.RegularExpressions;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Henry;

namespace HexaB
{
    class Bridge : AssepontoRep.Bridge
    {
        int NMarcacoes = 0;
        Marcacoes marcacoes;
        int NSR = 0;
        public bool connect = false;
        //IPHostEntry ipHostInfo;
        IPAddress ipAddress;
        IPEndPoint remoteEP;
        AsyncCallback asyncCallback;

        #region Variaveis Padrão do Relógio
        private static String response = String.Empty;
        // ManualResetEvent instances signal completion.
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        private static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private byte[] chaveAes;
        private byte[] bufferBytes = new byte[1024];

        byte[] biometriaExemplo = new byte[384];

        private static int quantBytesRec = 0;
        #endregion

        //Testado no Firmware:

        private const string FILE_CONFIGURATION = "rep_configuracao.txt";
        private const string FILE_EMPLOYER = "rep_empregador.txt";
        private const string FILE_STAFF = "rep_colaborador.txt";
        private string FILE_PATH = Folders.folderArquivoUsb("Henry");

        #region Constructor
        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog) { }
        #endregion

        #region Override Abstract
        public override bool getPin() { return false; }
        public override bool getAutenticacao() { return false; }
        public override int getPortaPadrao() { return 3000; }
        public override string getRepFabricante() { return "Henry - Advanced/HexaB"; }
        public override string getArquivoUsb() { return "Advanced-HexaB"; }
        public override bool getGerarUsb() { return true; }
        public override bool getGerarBackup() { return true; }
        public override bool getCadastroTerminalSupervisor() { return false; }
        public override bool getEnviarHorarioVerao() { return false; }
        public override bool getEnviarHorarioVeraoUsb() { return true; }
        public override bool getFuncionariosAlteradosUsb() { return false; }
        public override bool getGerarBiometria() { return false; }
        public override bool getGerarUsbEmpregador() { return true; }
        public override bool getNumeroSerieREP() { return false; }
        public override bool getContemChaveAcessoREP() { return false; }
        public override bool getBoxFuncoes() { return false; }
        public override bool getCadastroTerminalResponsavel() { return true; }
        public override bool getDisconnectOnExit() { return true; }
        public override bool getColumnId() { return false; }
        public override List<Types.Permissao> getPermissoes() { return null; }

        #endregion

        #region SubClass
        class Client : TcpClient
        {
            string IP;
            int Porta;
            Log log;

            public Client(string IP, int Porta, Log log)
                : base(IP, Porta)
            {
                this.IP = IP;
                this.Porta = Porta;
                this.log = log;
            }
        }
        #endregion

        #region Override
        public override bool Connect(int Terminal)
        {
            base.Connect(Terminal);
            bool retorno = false;
            int count = 0;

            asyncCallback = new AsyncCallback(ConnectCallback);
            do
            {
                ipAddress = Dns.GetHostAddresses(TerminalDados.IP)[0];
                remoteEP = new IPEndPoint(ipAddress, TerminalDados.Porta);

                string command = "";
                string preCommand = "";
                string strModulo = "";
                string strExpodente = "";
                string strRec = "";
                byte chkSum = 0;
                string strComandoComCriptografia = "";
                string strAux = "";


                do
                {
                    //if (client == null)
                    //    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    if (!client.Connected)
                    {
                        client.BeginConnect(remoteEP, asyncCallback, client);
                        Thread.Sleep(2500);
                        connectDone.WaitOne();
                        connect = client.Connected;
                    }
                    else
                        connect = client.Connected;

                    Random rnd = new Random();

                    if (count == 1)
                        log.AddLog("TENTANDO CONEXÃO...");
                    if (connect)
                    {
                        if (chaveAes == null)
                        {
                            chaveAes = new byte[16];
                            chaveAes[0] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[1] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[2] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[3] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[4] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[5] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[6] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[7] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[8] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[9] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[10] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[11] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[12] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[13] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[14] = Convert.ToByte(rnd.Next(1, 256));
                            chaveAes[15] = Convert.ToByte(rnd.Next(1, 256));
                        }
                        command = "";
                        command = command + (char)(2);
                        // start byte
                        preCommand = preCommand + (char)(7);
                        // tamanho do comando
                        preCommand = preCommand + (char)(0);
                        // tamanho do comando
                        preCommand = preCommand + "1+RA+00";
                        chkSum = calcCheckSumString(preCommand);
                        command = command + preCommand;
                        command = command + Convert.ToChar(chkSum);
                        // checksum
                        // end byte
                        command = command + (char)(3);
                        // Send test data to the remote device.
                        Send(client, command);
                        sendDone.WaitOne();

                        quantBytesRec = client.Receive(bufferBytes);

                        response = "";
                        for (int i = 0; i < quantBytesRec; i++)
                        {
                            response = response + (char)bufferBytes[i];
                        }

                        for (int i = 0; i < quantBytesRec; i++)
                        {
                            if (i >= 3)
                            {
                                if (i <= quantBytesRec - 3)
                                    strRec = strRec + response.ElementAt(i);
                            }
                        }
                        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
                        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
                        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

                        int index = strRec.IndexOf("]");
                        if (index > 0)
                        {
                            strModulo = Mid(strRec, 1, index);
                            strExpodente = Trim(Mid(strRec, index + 2, strRec.Length - index - 1));
                            strAux = "1]" + TerminalDados.OperadorLogin + "]" + TerminalDados.OperadorSenha + "]" + System.Convert.ToBase64String(chaveAes);

                            RSAPersistKeyInCSP(strModulo);
                            byte[] dataToEncrypt = Encoding.Default.GetBytes(strAux);
                            byte[] encryptedData = null;

                            RSAParameters RSAKeyInfo = new RSAParameters();

                            RSAKeyInfo.Modulus = System.Convert.FromBase64String(strModulo);
                            RSAKeyInfo.Exponent = System.Convert.FromBase64String(strExpodente);

                            encryptedData = RSAEncrypt(dataToEncrypt, RSAKeyInfo, false);

                            strAux = System.Convert.ToBase64String(encryptedData);


                            strComandoComCriptografia = "2+EA+00+" + strAux;

                            preCommand = "";
                            command = "";
                            command = command + Convert.ToChar(2);
                            // start byte
                            preCommand = preCommand + Convert.ToChar(strComandoComCriptografia.Length);
                            // tamanho do comando
                            preCommand = preCommand + Convert.ToChar(0);
                            // tamanho do comando
                            preCommand = preCommand + strComandoComCriptografia;
                            chkSum = calcCheckSumString(preCommand);

                            command = command + preCommand;
                            command = command + Convert.ToChar(chkSum);
                            // checksum

                            command = command + Convert.ToChar(3);
                            // end byte
                            Send(client, command);
                            sendDone.WaitOne();

                            quantBytesRec = client.Receive(bufferBytes);

                            response = "";
                            for (int i = 0; i < quantBytesRec; i++)
                            {
                                response = response + Convert.ToChar(bufferBytes[i]);
                            }

                            strRec = "";

                            for (int i = 0; i < quantBytesRec; i++)
                            {
                                if (i >= 3)
                                {
                                    if (i <= quantBytesRec - 3)
                                        strRec = strRec + response.ElementAt(i);
                                }
                            }

                        }
                        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
                        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

                        if (strRec == "000")
                        {
                            log.AddLog(Consts.SERVIDOR_ONLINE);
                            retorno = true;
                        }
                        else
                        {
                            try
                            {
                                strRec = Convert.ToInt32(strRec).ToString();
                                log.AddLog(String.Format(Consts.ERRO_ENVIO_COMANDO_CODIGO, strRec));
                                retorno = false;
                            }
                            catch
                            {
                                log.AddLog("ERRO DE CRIPTOGRAFIA: " + strRec);
                                Disconnect(Terminal);
                                retorno = false;
                            }
                            connect = false;
                        }
                    }
                    //else
                    //{
                    //    log.AddLog(String.Format("ERRO NA CONEXÃO"));
                    //    retorno = false;
                    //}

                    count++;
                } while (!connect && count <= 10);
                //}
                //catch (Exception ex)
                //{
                //    log.AddLog("ERRO AO CONECTAR COM O RELÓGIO: "+ex.Message);
                //    retorno = false;
                //}
            }
            while (!connect && count <= 15);
            return retorno;
        }

        public override bool Disconnect(int Terminal)
        {
            try
            {
                if (connect)
                {
                    if (chaveAes != null)
                    {
                        string command = "";
                        string preCommand = "";
                        string strModulo = "";
                        string strExpodente = "";
                        string strRec = "";
                        byte chkSum = 0;
                        string strComandoComCriptografia = "";
                        string strAux = "";

                        command = "";
                        command = command + (char)(2);
                        // start byte
                        preCommand = preCommand + (char)(7);
                        // tamanho do comando
                        preCommand = preCommand + (char)(0);
                        // tamanho do comando
                        preCommand = preCommand + "1+RA+00";
                        chkSum = calcCheckSumString(preCommand);
                        command = command + preCommand;
                        command = command + Convert.ToChar(chkSum);
                        // checksum
                        // end byte
                        command = command + (char)(3);
                        // Send test data to the remote device.
                        Send(client, command);
                        sendDone.WaitOne();

                        quantBytesRec = client.Receive(bufferBytes);

                        response = "";
                        for (int i = 0; i < quantBytesRec; i++)
                        {
                            response = response + (char)bufferBytes[i];
                        }

                        for (int i = 0; i < quantBytesRec; i++)
                        {
                            if (i >= 3)
                            {
                                if (i <= quantBytesRec - 3)
                                    strRec = strRec + response.ElementAt(i);
                            }
                        }
                        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
                        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
                        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

                        int index = strRec.IndexOf("]");
                        strModulo = Mid(strRec, 1, index);
                        strExpodente = Trim(Mid(strRec, index + 2, strRec.Length - index - 1));
                        strAux = "0]" + TerminalDados.OperadorLogin + "]" + TerminalDados.OperadorSenha + "]" + System.Convert.ToBase64String(chaveAes);

                        RSAPersistKeyInCSP(strModulo);
                        byte[] dataToEncrypt = Encoding.Default.GetBytes(strAux);
                        byte[] encryptedData = null;

                        RSAParameters RSAKeyInfo = new RSAParameters();

                        RSAKeyInfo.Modulus = System.Convert.FromBase64String(strModulo);
                        RSAKeyInfo.Exponent = System.Convert.FromBase64String(strExpodente);

                        encryptedData = RSAEncrypt(dataToEncrypt, RSAKeyInfo, false);

                        strAux = System.Convert.ToBase64String(encryptedData);


                        strComandoComCriptografia = "2+EA+00+" + strAux;

                        preCommand = "";
                        command = "";
                        command = command + Convert.ToChar(2);
                        // start byte
                        preCommand = preCommand + Convert.ToChar(strComandoComCriptografia.Length);
                        // tamanho do comando
                        preCommand = preCommand + Convert.ToChar(0);
                        // tamanho do comando
                        preCommand = preCommand + strComandoComCriptografia;
                        chkSum = calcCheckSumString(preCommand);

                        command = command + preCommand;
                        command = command + Convert.ToChar(chkSum);
                        // checksum

                        command = command + Convert.ToChar(3);
                        // end byte
                        Send(client, command);
                        sendDone.WaitOne();

                        quantBytesRec = client.Receive(bufferBytes);

                        response = "";
                        for (int i = 0; i < quantBytesRec; i++)
                        {
                            response = response + Convert.ToChar(bufferBytes[i]);
                        }

                        strRec = "";

                        for (int i = 0; i < quantBytesRec; i++)
                        {
                            if (i >= 3)
                            {
                                if (i <= quantBytesRec - 3)
                                    strRec = strRec + response.ElementAt(i);
                            }
                        }

                        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
                        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

                        if (strRec == "000")
                        {
                            log.AddLog(Consts.SERVIDOR_OFFLINE);
                            connect = false;
                        }
                        else
                        {
                            try
                            {
                                strRec = Convert.ToInt32(strRec).ToString();
                                log.AddLog(String.Format(Consts.ERRO_ENVIO_COMANDO_CODIGO, strRec));
                            }
                            catch
                            {
                                log.AddLog("ERRO DE CRIPTOGRAFIA: " + strRec);
                                //Disconnect();
                            }
                            connect = false;
                        }
                    }
                    log.AddLog(Consts.SERVIDOR_PARANDO);
                    //client.Disconnect(false);
                    //client.EndDisconnect(client.BeginDisconnect(false, new AsyncCallback(ConnectCallback), client));
                    //Disconnect();
                    log.AddLog("SERVIDOR OFFLINE", true);
                    connect = false;
                    return true;
                }
                else
                {
                    log.AddLog("SEM CONEXÃO ATIVA", true);
                    connect = false;
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.AddLog("ERRO: " + ex.Message, true);
                return false;
            }
        }

        //public void Disconnect()
        //{
        //    if (client != null)
        //    {
        //        if (client.Connected)
        //        {
        //            client.BeginDisconnect(false, new AsyncCallback(ConnectCallback), client);
        //            client.Close();
        //        }
        //        connect = false;
        //        //client.Dispose();
        //        //client = null;
        //    }
        //}

        public override bool sendDataHora(int Terminal)
        {
            base.sendDataHora(Terminal);

            if (!connect)
                Connect(Terminal);
            if (connect)
            {
                Random rnd = new Random();

                string dia = DateTime.Now.Day <= 9 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
                string mes = DateTime.Now.Month <= 9 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
                string ano = (DateTime.Now.Year - 2000).ToString();
                string hora = DateTime.Now.Hour <= 9 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
                string min = DateTime.Now.Minute <= 9 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
                string strComandoSemCriptografia = "01+EH+00+";
                string strComandoComCriptografia = String.Format("{0}/{1}/{2} {3}:{4}:00]00/00/00]00/00/00", dia, mes, ano, hora, min);
                string strRec = "";

                string cmd = strComandoSemCriptografia + strComandoComCriptografia;

                byte[] comandoByte;
                byte[] IV;
                PreSend(cmd, rnd, out comandoByte, out IV);

                Send2(client, comandoByte);

                #region Send String (Comentado)
                //string preCommand;
                //string command;
                //byte[] Crypt = Encoding.Default.GetBytes(Encoding.Default.GetChars(EncryptStringToBytes_Aes(strComandoComCriptografia, chaveAes, IV)));
                //for (int i = 0; i < Crypt.Length; i++)
                //{
                //    strComandoSemCriptografia = strComandoSemCriptografia + (char)Crypt[i];
                //}
                //preCommand = "";
                //command = "";
                //command = command + Convert.ToChar(2);
                //// start byte
                //preCommand = preCommand + Convert.ToChar(strComandoSemCriptografia.Length);
                //// tamanho do comando
                //preCommand = preCommand + Convert.ToChar(0);
                //// tamanho do comando
                //preCommand = preCommand + strComandoSemCriptografia;

                //byte[] comandoByte = Encoding.Default.GetBytes(preCommand);
                //for (int i = 0; i < comandoByte.Length; i++)
                //{
                //    chkSum = chkSum ^ comandoByte[i];
                //}

                ////chkSum = calcCheckSumString(preCommand);

                //command = command + preCommand;
                //command = command + Convert.ToChar(chkSum);
                //// checksum

                //command = command + Convert.ToChar(3);
                //// end byte
                //Send(client, command);
                #endregion

                strRec = PosSend(IV);

                return ProcessAnswer(strRec, TypesRespostas.DataHora);
            }
            else
            {
                Disconnect(Terminal);
                log.AddLog("SERVIDOR OFFLINE");
                log.AddLineBreak();
                return false;
            }
        }

        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            base.sendInfoEmpresa(Terminal, out EmpregadorDados);

            if (!connect)
                Connect(Terminal);
            if (connect)
            {
                Random rnd = new Random();

                string Pessoa = ((int)EmpregadorDados.PessoaTipo).ToString();
                string CnpjCpf = EmpregadorDados.Pessoa;
                string Cei = String.IsNullOrEmpty(EmpregadorDados.Cei) ? "" : EmpregadorDados.Cei;
                string Nome = EmpregadorDados.Nome;
                string Local = EmpregadorDados.Endereco;
                string strComandoSemCriptografia = "01+EE+00+";
                string strComandoComCriptografia = String.Format("{0}]{1}]{2}]{3}]{4}",
                                                                Pessoa, CnpjCpf, Cei, Nome, Local);
                string strRec = "";

                byte[] cmd = Encoding.Default.GetBytes(strComandoComCriptografia);

                byte[] comandoByte;
                byte[] IV;
                PreSend2(strComandoSemCriptografia, cmd, rnd, out comandoByte, out IV);

                Send2(client, comandoByte);

                strRec = PosSend(IV);

                return ProcessAnswer(strRec, TypesRespostas.Empregador);
            }
            else
            {
                Close();
                log.AddLog("SERVIDOR OFFLINE");
                log.AddLineBreak();
                return false;
            }
        }

        //public override bool sendFuncionarios(int Terminal, List<Types.Funcionario> listFuncionarios)
        //{
        //    return base.sendFuncionarios(Terminal, listFuncionarios);
        //}

        public override bool sendFuncionario(Types.Funcionario Funcionario)
        {
            base.sendFuncionario(Funcionario);

            if (!connect || client == null || chaveAes == null)
                Connect(TerminalDados.Indice);

            Random rnd = new Random();

            string Pis = Funcionario.Pis;
            string Nome = Funcionario.Nome;
            string Referencia = Funcionario.Teclado;
            string strComandoSemCriptografia = "01+EU+00+1+";
            string strComandoComCriptografia = String.Format("[{0}[{1}[0[1[{2}]",
                                                            Pis, Nome, Referencia);

            if (FindDados(Funcionario, rnd, TypesRespostas.Funcionario))
                strComandoComCriptografia = "A" + strComandoComCriptografia;
            else
            {
                strComandoComCriptografia = "I" + strComandoComCriptografia;
            }

            string strRec = "";

            byte[] cmd = Encoding.Default.GetBytes(strComandoComCriptografia);

            byte[] comandoByte;
            byte[] IV;
            PreSend2(strComandoSemCriptografia, cmd, rnd, out comandoByte, out IV);

            Send2(client, comandoByte);

            strRec = PosSend(IV);
            strRec = strRec.Replace("1+", "");

            log.AddLog(String.Format(Consts.FUNCIONARIO_ENVIADO_COM_SUCESSO, Funcionario.Nome));
            return ProcessAnswer(strRec, TypesRespostas.Funcionario);
        }

        private bool FindDados(Types.Funcionario func, Random rnd, TypesRespostas tipo)
        {
            bool retorno = false;
            if (tipo == TypesRespostas.Funcionario)
            {
                string cmd = "01+RU+00+-2]" + func.Pis;
                string strRec = "";
                //byte[] cmdBytes = Encoding.Default.GetBytes(cmd);

                byte[] comandoByte;
                byte[] IV;
                PreSend(cmd, rnd, out comandoByte, out IV);

                Send2(client, comandoByte);

                //01+RU+00+1+123456789012[Eduardo[0[2[1535}6587
                strRec = PosSend(IV);
                String[] split = strRec.Split(new String[] { "+" }, StringSplitOptions.None);
                string dados = split[split.Length - 1];
                String[] dadosSplit = dados.Split(new String[] { "[" }, StringSplitOptions.None);
                if (dadosSplit[0] == func.Pis || dadosSplit[0] == "0" + func.Pis)
                    retorno = true;
                else
                    retorno = false;
            }
            return retorno;
        }

        public override bool deleteFuncionario(Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);
            bool retorno = false;

            if (!connect)
                Connect(TerminalDados.Indice);

            Random rnd = new Random();

            string Pis = Funcionario.Pis;
            string Nome = Funcionario.Nome;
            string Referencia = Funcionario.Teclado;
            string strComandoSemCriptografia = "01+EU+00+1+";
            string strComandoComCriptografia = String.Format("E[{0}[{1}[1[1[{2}]",
                                                            Pis, Nome, Referencia);
            string strRec = "";

            byte[] cmd = Encoding.Default.GetBytes(strComandoComCriptografia);

            byte[] comandoByte;
            byte[] IV;
            PreSend2(strComandoSemCriptografia, cmd, rnd, out comandoByte, out IV);

            Send2(client, comandoByte);

            strRec = PosSend(IV);
            //strRec = strRec.Replace("1+", "");

            if (strRec.IndexOf("22") == 0)
            {
                log.AddLog(String.Format("{0} NÃO CADASTRADO", Funcionario.Nome));
                retorno = false;
            }
            else if (strRec.IndexOf("1+0") == 0)
            {
                log.AddLog(String.Format(Consts.FUNCIONARIO_EXCLUIDO, Funcionario.Nome));
                retorno = true;
            }
            return retorno;
        }

        public override bool LerMarcacoes(Marcacoes marcacoes, TipoImportacaoMarcacoes tipoimportacao)
        {
            base.LerMarcacoes(marcacoes, tipoimportacao);
            this.marcacoes = marcacoes;
            bool retorno;
            DBApp dBApp = new DBApp();

            if (!connect || client == null || chaveAes == null)
                Connect(TerminalDados.Indice);
            if (connect)
            {
                int Nsr = (tipoimportacao == TipoImportacaoMarcacoes.OnlyNew ? dBApp.getLastNsr(TerminalDados.Indice) : 1);
                //int Nsr = 1;
                Random rnd = new Random();
                List<String> linhas = new List<string>();
                string strRec = "";
                int cod;
                int count = 0;
                string strComandoSemCriptografia = "01+RR+00+";
                string strComandoComCriptografia;
                log.AddLineBreak();
                log.AddLog("PROCESSANDO MARCAÇÕES...");
                do
                {
                    strComandoComCriptografia = String.Format("N]1]{0}", Nsr);
                    Nsr++;
                    //string strComandoComCriptografia = "EV]";

                    byte[] cmd = Encoding.Default.GetBytes(strComandoComCriptografia);
                    //string cmd = strComandoSemCriptografia + strComandoComCriptografia;

                    byte[] comandoByte;
                    byte[] IV;
                    PreSend(strComandoSemCriptografia + strComandoComCriptografia, rnd, out comandoByte, out IV);

                    Send2(client, comandoByte);

                    strRec = PosSend(IV, TypesRespostas.Marcacoes);
                    if (strRec.IndexOf("01+RR+000+") != -1)
                    {
                        //linhas.Add(strRec);
                        cod = 0;
                        count++;
                        string l = strRec.Replace("01+RR+000+00001]", "");
                        int codMarc = Convert.ToInt32(l.Substring(9, 1));
                        if (codMarc == 3)
                        {
                            string nsr = l.Substring(0, 9);
                            string dia = l.Substring(10, 2);
                            string mes = l.Substring(12, 2);
                            string ano = l.Substring(14, 4);
                            string hr = l.Substring(18, 2);
                            string min = l.Substring(20, 2);
                            string pis = l.Substring(22, 12);
                            DateTime datahora = Convert.ToDateTime(dia + "/" + mes + "/" + ano + " " + hr + ":" + min + ":00");
                            log.AddLog("MARCAÇÃO DE " + pis + ", DATA: " + datahora.ToString());

                            marcacoes.Add(new Marcacoes.Marcacao()
                            {
                                NSR = Convert.ToInt32(nsr),
                                Pis = pis,
                                DataHora = datahora,
                                Tipo = Marcacoes.TiposRegistroAfd.Marcacao
                            });
                        }
                    }
                    else
                        cod = Convert.ToInt32(strRec.Substring(6, 3));


                    dBApp.setLastNsr(TerminalDados.Indice, (Nsr - 2));
                } while (cod == 0);


                if (cod != 50)
                    log.AddLog("ERRO NO REGISTRO DE NSR: " + (Nsr - 1).ToString(), true);
                else
                {
                    log.AddLog("ERRO '" + cod.ToString() + "': " + (Nsr - 1).ToString());
                    cod = 0;
                }

                #region Comments
                //log.AddLog(String.Format(Consts.MARCACOES_A_PROCESSAR, count));
                //foreach (string linha in linhas)
                //{
                //    string l = linha.Replace("01+RR+000+00001]", "");
                //    int codMarc = Convert.ToInt32(l.Substring(9,1));
                //    if (codMarc == 3)
                //    {
                //        string nsr = l.Substring(0, 9);
                //        string dia = l.Substring(10, 2);
                //        string mes = l.Substring(12, 2);
                //        string ano = l.Substring(14, 4);
                //        string hr = l.Substring(18, 2);
                //        string min = l.Substring(20, 2);
                //        string pis = l.Substring(22, 12);
                //        DateTime datahora = Convert.ToDateTime(dia + "/" + mes + "/" + ano + " " + hr + ":" + min + ":00");
                //        log.AddLog("MARCAÇÃO DE " + pis + ", DATA: " + datahora.ToString());

                //        marcacoes.Add(new Marcacoes.Marcacao()
                //            {
                //                NSR = Convert.ToInt32(nsr),
                //                Pis = pis,
                //                DataHora = datahora,
                //                Tipo = Marcacoes.TiposRegistroAfd.Marcacao
                //            });
                //    }
                //}
                #endregion
                retorno = ProcessAnswer(cod.ToString(), TypesRespostas.Marcacoes);
            }
            else
                retorno = false;

            return retorno;
        }

        //public override bool EnviarResponsavel(int Terminal, string OperadorCPF, string OperadorUsuario, string OperadorSenha)
        //{
        //    base.EnviarResponsavel(Terminal, OperadorCPF, OperadorUsuario, OperadorSenha);

        //}
        #endregion

        #region Private
        private void Close()
        {
            //client.Close();
        }

        private void PreSend(string cmd, Random rnd, out byte[] cmdByteReturn, out byte[] IVreturn)
        {
            int chkSum = 0;

            byte[] IV = new byte[16];
            IV[0] = Convert.ToByte(rnd.Next(1, 256));
            IV[1] = Convert.ToByte(rnd.Next(1, 256));
            IV[2] = Convert.ToByte(rnd.Next(1, 256));
            IV[3] = Convert.ToByte(rnd.Next(1, 256));
            IV[4] = Convert.ToByte(rnd.Next(1, 256));
            IV[5] = Convert.ToByte(rnd.Next(1, 256));
            IV[6] = Convert.ToByte(rnd.Next(1, 256));
            IV[7] = Convert.ToByte(rnd.Next(1, 256));
            IV[8] = Convert.ToByte(rnd.Next(1, 256));
            IV[9] = Convert.ToByte(rnd.Next(1, 256));
            IV[10] = Convert.ToByte(rnd.Next(1, 256));
            IV[11] = Convert.ToByte(rnd.Next(1, 256));
            IV[12] = Convert.ToByte(rnd.Next(1, 256));
            IV[13] = Convert.ToByte(rnd.Next(1, 256));
            IV[14] = Convert.ToByte(rnd.Next(1, 256));
            IV[15] = Convert.ToByte(rnd.Next(1, 256));

            string strAux = "";
            byte[] cmdCrypt = Encoding.Default.GetBytes(Encoding.Default.GetChars(EncryptStringToBytes_Aes(cmd, chaveAes, IV)));

            int tamanhoPacote = cmdCrypt.Length + IV.Length;
            byte[] comandoByte = new byte[tamanhoPacote + 5];
            int IdxComandoByte = 3;
            comandoByte[0] = 2;
            // start byte
            comandoByte[1] = (byte)(tamanhoPacote & 0xff);
            // tamanho
            comandoByte[2] = (byte)((tamanhoPacote >> 8) & 0xff);
            // tamanho
            chkSum = 0;

            for (int i = 0; i < IV.Length; i++)
            {
                comandoByte[IdxComandoByte] = IV[i];
                IdxComandoByte = IdxComandoByte + 1;
            }

            for (int i = 0; i < cmdCrypt.Length; i++)
            {
                comandoByte[IdxComandoByte] = cmdCrypt[i];
                IdxComandoByte = IdxComandoByte + 1;
            }

            for (int i = 1; i < IdxComandoByte; i++)
            {
                chkSum = chkSum ^ comandoByte[i];
            }
            comandoByte[IdxComandoByte] = (byte)chkSum;
            IdxComandoByte = IdxComandoByte + 1;
            comandoByte[IdxComandoByte] = 3;

            for (int i = 0; i < IdxComandoByte; i++)
            {
                strAux = strAux + Convert.ToChar(comandoByte[i]);
            }
            cmdByteReturn = comandoByte;
            IVreturn = IV;
        }

        private void PreSend2(string cmd1, byte[] cmd2, Random rnd, out byte[] cmdByteReturn, out byte[] IVreturn)
        {
            int chkSum = 0;

            byte[] IV = new byte[16];
            IV[0] = Convert.ToByte(rnd.Next(1, 256));
            IV[1] = Convert.ToByte(rnd.Next(1, 256));
            IV[2] = Convert.ToByte(rnd.Next(1, 256));
            IV[3] = Convert.ToByte(rnd.Next(1, 256));
            IV[4] = Convert.ToByte(rnd.Next(1, 256));
            IV[5] = Convert.ToByte(rnd.Next(1, 256));
            IV[6] = Convert.ToByte(rnd.Next(1, 256));
            IV[7] = Convert.ToByte(rnd.Next(1, 256));
            IV[8] = Convert.ToByte(rnd.Next(1, 256));
            IV[9] = Convert.ToByte(rnd.Next(1, 256));
            IV[10] = Convert.ToByte(rnd.Next(1, 256));
            IV[11] = Convert.ToByte(rnd.Next(1, 256));
            IV[12] = Convert.ToByte(rnd.Next(1, 256));
            IV[13] = Convert.ToByte(rnd.Next(1, 256));
            IV[14] = Convert.ToByte(rnd.Next(1, 256));
            IV[15] = Convert.ToByte(rnd.Next(1, 256));

            string strAux = "";
            byte[] cmdCrypt = Encoding.Default.GetBytes(Encoding.Default.GetChars(EncryptStringToBytes_Aes2(cmd1, cmd2, chaveAes, IV)));

            int tamanhoPacote = cmdCrypt.Length + IV.Length;
            byte[] comandoByte = new byte[tamanhoPacote + 5];
            int IdxComandoByte = 3;
            comandoByte[0] = 2;
            // start byte
            comandoByte[1] = (byte)(tamanhoPacote & 0xff);
            // tamanho
            comandoByte[2] = (byte)((tamanhoPacote >> 8) & 0xff);
            // tamanho
            chkSum = 0;

            for (int i = 0; i < IV.Length; i++)
            {
                comandoByte[IdxComandoByte] = IV[i];
                IdxComandoByte = IdxComandoByte + 1;
            }

            for (int i = 0; i < cmdCrypt.Length; i++)
            {
                comandoByte[IdxComandoByte] = cmdCrypt[i];
                IdxComandoByte = IdxComandoByte + 1;
            }

            for (int i = 1; i < IdxComandoByte; i++)
            {
                chkSum = chkSum ^ comandoByte[i];
            }
            comandoByte[IdxComandoByte] = (byte)chkSum;
            IdxComandoByte = IdxComandoByte + 1;
            comandoByte[IdxComandoByte] = 3;

            for (int i = 0; i < IdxComandoByte; i++)
            {
                strAux = strAux + Convert.ToChar(comandoByte[i]);
            }
            cmdByteReturn = comandoByte;
            IVreturn = IV;
        }

        private string PosSend(byte[] IV, TypesRespostas tipo = TypesRespostas.Nulo)
        {
            sendDone.WaitOne();

            quantBytesRec = client.Receive(bufferBytes);

            response = "";
            for (int i = 0; i < quantBytesRec; i++)
            {
                response = response + (char)bufferBytes[i];
            }

            string strRec = "";
            int idxByte = 0;
            byte[] byteData = new byte[quantBytesRec - 5];
            for (int i = 0; i < quantBytesRec; i++)
            {
                if (i >= 3)
                {
                    if (i <= quantBytesRec - 3)
                    {
                        byteData[idxByte] = Convert.ToByte(response.ElementAt(i));
                        idxByte++;
                        strRec = strRec + response.ElementAt(i);
                    }
                }
            }

            for (int i = 0; i < 16; i++)
            {
                IV[i] = byteData[i];
            }

            byte[] byteData2 = new byte[quantBytesRec - 16 - 5];

            for (int i = 0; i < byteData.Length - 16; i++)
            {
                byteData2[i] = byteData[i + 16];
            }

            strRec = DecryptStringFromBytes_Aes(byteData2, chaveAes, IV);
            if (tipo == TypesRespostas.Marcacoes)
            {
                return strRec;
            }

            strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
            strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

            strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

            return strRec;
        }

        private static void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.Default.GetBytes(data);

            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private void Send2(Socket client, byte[] byteData)
        {
            //if (client == null)
            //{
            //    ipAddress = Dns.GetHostAddresses(TerminalDados.IP)[0];
            //    remoteEP = new IPEndPoint(ipAddress, TerminalDados.Porta);

            //    client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //    client.BeginConnect(remoteEP, asyncCallback, client);
            //    connectDone.WaitOne();
            //}
            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private bool ProcessAnswer(string Answer, TypesRespostas tipo)
        {
            bool Result = false;
            Answer = Answer.Replace("\0", "");
            try
            {
                if (Convert.ToInt32(Answer) == 0)
                {
                    if (tipo == TypesRespostas.DataHora)
                    {
                        log.AddLog(Consts.DATA_HORA_ATUALIZADA_SUCESSO);
                        Result = true;
                    }
                    else if (tipo == TypesRespostas.Empregador)
                    {
                        log.AddLog(Consts.EMPREGADOR_ENVIADO_SUCESSO);
                        Result = true;
                    }
                    else if (tipo == TypesRespostas.Funcionario)
                    {
                        Result = true;
                    }
                    else if (tipo == TypesRespostas.Marcacoes)
                    {
                        log.AddLog(Consts.OPERACAO_FINALIZADA);
                        Result = true;
                    }
                }
                else
                {
                    if (Answer.Replace("\0", "").Trim() == "12")
                        log.AddLog(String.Format(Consts.ERRO_ENVIO_COMANDO_CODIGO, "Parâmetros informados são inválidos"));
                    else
                        log.AddLog(String.Format(Consts.ERRO_ENVIO_COMANDO_CODIGO, Answer.Replace("\0", "")));
                    Result = false;
                }
            }
            catch
            {
                log.AddLog("ERRO DE CRIPTOGRAFIA: " + Answer);
                log.AddLog("TENTE NOVAMENTE");
                Result = false;
            }

            #region Código Antigo (Comentado)
            //if (Answer.StartsWith("01+EH+00"))
            //{
            //    log.AddLog(Consts.DATA_HORA_ATUALIZADA_SUCESSO);
            //    Result = true;
            //}
            //else
            //    if (Answer.StartsWith("01+EE+00"))
            //    {
            //        log.AddLog(Consts.EMPREGADOR_ENVIADO_SUCESSO);
            //        Result = true;
            //    }
            //    else
            //        if (Answer.StartsWith("01+EU+00"))
            //        {
            //            log.LogOk();
            //            Result = true;
            //        }
            //        else
            //            if (Answer.StartsWith("01+RQ+00"))
            //            {
            //                NMarcacoes = System.Convert.ToInt32(Answer.Substring(11));
            //                log.AddLog(String.Format(Consts.REGISTROS_LENDO, NMarcacoes));
            //                Result = true;
            //            }
            //            else
            //                if (Answer.StartsWith("01+RR+0+") || Answer.StartsWith("01+RR+000+"))
            //                {
            //                    string temp = Answer.Substring(12);
            //                    NMarcacoes = System.Convert.ToInt32(temp.Substring(0, temp.IndexOf(']')));
            //                    ProcessMarcacoes(temp.Substring(temp.IndexOf(']') + 1));
            //                    Result = true;
            //                }
            //                else
            //                    if (Answer.StartsWith("1+RR+0+") || Answer.StartsWith("1+RR+000+"))
            //                    {
            //                        string temp = Answer.Substring(10); //7
            //                        NMarcacoes = System.Convert.ToInt32(temp.Substring(0, temp.IndexOf(']')));
            //                        ProcessMarcacoes(temp.Substring(temp.IndexOf(']') + 1));
            //                        Result = true;
            //                    }
            //                    else
            //                        if (Answer.StartsWith("1+RR+"))
            //                        {
            //                            int CodigoErro = System.Convert.ToInt32(Answer.Substring(5));

            //                            log.AddLog((CodigosErro.IsDefined(typeof(CodigosErro), CodigoErro)) ? Wr.Classes.Utils.GetDescription((CodigosErro)CodigoErro) : String.Format(Consts.ERRO_ENVIO_COMANDO_CODIGO, Answer));
            //                        }
            //                        else
            //                        {
            //                            //int CodigoErro = System.Convert.ToInt32(Answer.Substring(5));
            //                            log.AddLog(String.Format(Consts.ERRO_ENVIO_COMANDO_CODIGO, Answer.Replace("\0", "")));
            //                        }
            #endregion
            if (tipo != TypesRespostas.Funcionario)
                Close();
            log.AddLineBreak();
            //Disconnect();
            return Result;
        }
        #endregion

        //Comandos USB
        #region USB Funcionario
        public override bool createUsbFile(int Terminal, Types.Opcao Opcao, List<Types.Funcionario> listFuncionarios)
        {
            char option = Opcao == Types.Opcao.Inclusao ? 'I' : Opcao == Types.Opcao.Alteracao ? 'A' : 'E';

            try
            {
                log.AddLog(Consts.USB_GERANDO_ARQUIVO);
                deleteArquivo(FILE_PATH, FILE_STAFF);
                List<string> Novo = new List<string>();

                foreach (Types.Funcionario func in listFuncionarios)
                {
                    if (!Wr.Classes.Validadores.ValidaPis(func.Pis))
                    {
                        log.AddLog(func.Nome);
                        log.AddLog("    PIS INVALIDO");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(func.Barras) && string.IsNullOrEmpty(func.Proximidade) && string.IsNullOrEmpty(func.Teclado))
                        {
                            log.AddLog(func.Nome);
                            log.AddLog("    FUNCIONÁRIO SEM CRACHÁ");
                        }
                        else
                            Novo.Add("1+1+" + option + string.Format("[{0}[{1}[{2}[1[{3}", func.Pis, func.Nome, func.Biometria ? 1 : 0,
                                (string.IsNullOrEmpty(func.Barras) ? string.IsNullOrEmpty(func.Teclado) ? func.Proximidade : func.Teclado : func.Barras).PadLeft(20, '0')));
                    }
                }

                if (Novo.Count == 0)
                {
                    log.AddLog(Consts.USB_SEM_FUNCIONARIOS);
                    return false;
                }

                Files.WriteFile(FILE_PATH + FILE_STAFF, Novo);
                log.AddLog(string.Format(Consts.USB_ARQUIVO_GERADO_SUCESSO, FILE_PATH));
                return true;
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }
        }
        #endregion

        #region USB Empregador
        public override bool GerarArquivoEmpregador(int Terminal, Types.Opcao Opcao, out Types.Empregador EmpregadorDados)
        {
            if (!base.sendInfoEmpresa(Terminal, out EmpregadorDados)) return false;

            try
            {
                log.AddLog(Consts.USB_GERANDO_ARQUIVO);
                List<string> Novo = new List<string>();

                deleteArquivo(FILE_PATH, FILE_EMPLOYER);
                Novo.Add("2+" + (int)EmpregadorDados.PessoaTipo + "]" + EmpregadorDados.Pessoa + "]" + EmpregadorDados.Cei + "]" +
                                 EmpregadorDados.Nome + "]" + EmpregadorDados.Endereco);

                Files.WriteFile(FILE_PATH + FILE_EMPLOYER, Novo);
                log.AddLog(Consts.USB_EMPEGADOR_SUCESSO);
                return true;
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }
        }
        #endregion

        #region Util
        private void deleteArquivo(string filePath, string Name)
        {
            List<string> Arquivos = new List<string>();

            Files.DirSearch(filePath, Name, Arquivos);

            foreach (string arquivo in Arquivos)
            {
                File.Delete(arquivo);
            }
        }
        #endregion

        //Encriptação e Descriptação
        #region Encrypt
        static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                //Import the RSA Key information. This only needs
                //toinclude the public key information.
                RSA.ImportParameters(RSAKeyInfo);

                //Encrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                return RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                aesAlg.Padding = PaddingMode.None;
                aesAlg.Mode = CipherMode.CBC;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);

                            int quant = plainText.Length;

                            if (quant > 16)
                            {
                                quant = quant % 16;
                            }
                            quant = 16 - quant;
                            while (quant < 16 && quant != 0)
                            {
                                swEncrypt.Write(Convert.ToChar(Convert.ToByte("0")));
                                quant = quant - 1;
                            }
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        static byte[] EncryptStringToBytes_Aes2(String plainText, byte[] plainText2, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (plainText2 == null || plainText2.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                aesAlg.Padding = PaddingMode.None;
                aesAlg.Mode = CipherMode.CBC;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] bytesStr = Encoding.Default.GetBytes(plainText);
                        csEncrypt.Write(bytesStr, 0, bytesStr.Length);
                        csEncrypt.Write(plainText2, 0, plainText2.Length);
                        int quant = bytesStr.Length + plainText2.Length;

                        if (quant > 16)
                        {
                            quant = quant % 16;
                        }
                        quant = 16 - quant;
                        byte[] bytesZeros = new byte[16] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                                          0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
                        if (quant < 16 && quant != 0)
                        {
                            csEncrypt.Write(bytesZeros, 0, quant);
                            quant = quant - 1;
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }
        #endregion

        #region Decrypt
        static public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                //Import the RSA Key information. This needs
                //to include the private key information.
                RSA.ImportParameters(RSAKeyInfo);

                //Decrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                return RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                aesAlg.Padding = PaddingMode.None;
                aesAlg.Mode = CipherMode.CBC;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }

        static byte[] DecryptStringFromBytes_Aes2(byte[] cipherText, byte[] Key, byte[] IV)
        {

            byte[] bufferDecrypt = new byte[cipherText.Length];

            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                aesAlg.Padding = PaddingMode.None;
                aesAlg.Mode = CipherMode.CBC;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        csDecrypt.Read(bufferDecrypt, 0, bufferDecrypt.Length);
                    }
                }
            }
            return bufferDecrypt;
        }
        #endregion

        //Chaves e CheckSum
        #region KeyInCSP
        public static void RSAPersistKeyInCSP(string ContainerName)
        {
            try
            {
                // Create a new instance of CspParameters.  Pass
                // 13 to specify a DSA container or 1 to specify
                // an RSA container.  The default is 1.
                CspParameters cspParams = new CspParameters();

                // Specify the container name using the passed variable.
                cspParams.KeyContainerName = ContainerName;

                //Create a new instance of RSACryptoServiceProvider to generate
                //a new key pair.  Pass the CspParameters class to persist the 
                //key in the container.  The PersistKeyInCsp property is true by 
                //default, allowing the key to be persisted. 
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);

                //Indicate that the key was persisted.
                Console.WriteLine("The RSA key was persisted in the container, \"{0}\".", ContainerName);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void RSADeleteKeyInCSP(string ContainerName)
        {
            try
            {
                // Create a new instance of CspParameters.  Pass
                // 13 to specify a DSA container or 1 to specify
                // an RSA container.  The default is 1.
                CspParameters cspParams = new CspParameters();

                // Specify the container name using the passed variable.
                cspParams.KeyContainerName = ContainerName;

                //Create a new instance of RSACryptoServiceProvider. 
                //Pass the CspParameters class to use the 
                //key in the container.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);

                //Explicitly set the PersistKeyInCsp property to false
                //to delete the key entry in the container.
                RSAalg.PersistKeyInCsp = false;

                //Call Clear to release resources and delete the key from the container.
                RSAalg.Clear();

                //Indicate that the key was persisted.
                Console.WriteLine("The RSA key was deleted from the container, \"{0}\".", ContainerName);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static string Trim(string s)
        {
            return s.Trim();
        }

        public static string Mid(string s, int a, int b)
        {
            string temp = s.Substring(a - 1, b);
            return temp;
        }
        #endregion

        #region CheckSum
        public byte calcCheckSumString(string data)
        {
            String strBuf = "";
            String strAux = "";
            byte cks = 0;

            for (int i = 0; i < data.Length; i++)
            {
                strAux = ((byte)(data.ElementAt(i))).ToString("X2");
                strBuf = strBuf + strAux;
                cks = (byte)(cks ^ (byte)(data.ElementAt(i)));
            }
            return cks;
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Receive(Socket client)
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.
                    if (state.sb.Length > 1)
                        response = state.sb.ToString();
                    // Signal that all bytes have been received.
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        #endregion

    }
}
