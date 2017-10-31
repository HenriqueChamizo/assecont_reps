using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO;
using Wr;

namespace Trix
{
    public class Rede
    {
        public DateTime tempoTimeout;
        bool expirouTimeout = false;
        //bool Cancela = false;
        bool abortaEnvio = false;
        public string IP;
        public int Porta;
        TextBox Log;

        public Rede(string ip, int porta, TextBox log)
        {
            IP = ip;
            Porta = porta;
            Log = log;
        }

        public bool EnviaComandoTCP(Comando cmd, string Mensagem = "")
        {
            bool executa = false;
            bool ret = false;
            TcpClient sock = new TcpClient();

            if (cmd.Modo == "S")
            {
                switch (cmd.TipoComando)
                {
                    case (Protocolo.CMD_EMPRESA):
                        {
                            if (Mensagem == "") AddLog("ENVIANDO CADASTRO EMPREGADOR");
                            break;
                        }
                    case (Protocolo.CMD_DATA_HORA):
                        {
                            if (Mensagem == "") AddLog("ATUALIZANDO DATA E HORA");
                            break;
                        }
                    case (Protocolo.CMD_FUNCIONARIO):
                        {
                            if (Mensagem == "") AddLog("ENVIANDO FUNCIONÁRIO: " + cmd.Mensagem.Substring(39));
                            break;
                        }
                }
            }

            switch (cmd.Modo)
            {
                #region SET
                case "S":
                    try
                    {
                        Application.DoEvents();
                        sock.Connect(IP, Porta);
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

                                msg = msg + rStrings.ConvertByteToString(recebimento);
                                executa = false;
                            }
                            expirouTimeout = VerificaTimeout();
                            if (expirouTimeout)
                                executa = false;
                        } while (executa);
                        if (expirouTimeout)
                        {
                            AddLog("Timeout na comunicação");

                            if (cmd.TipoComando.Equals("!03"))
                            {
                                AddLog(cmd.ToString());
                                expirouTimeout = false;
                                sock.Close();
                                return false;
                            }
                        }

                        Comando resp = new Comando(msg);

                        if (resp.Mensagem.Equals("06"))
                        {
                            AddLog("Comando enviado com sucesso", true);
                            ret = true;
                        }
                        else
                        {
                            AddLog("Erro no REP ao receber comando");
                        }

                        sock.Close();

                    }
                    catch (Exception ex)
                    {

                        if (cmd.TipoComando.Equals("!03"))
                        {
                            AddLog(cmd.ToString());
                            abortaEnvio = true;
                        }
                        sock.Close();
                        AddLog(ex.Message);
                        return false;
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
                                sock.Connect(IP, Porta);
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
                                        msg = msg + rStrings.ConvertByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    AddLog("Timeout na comunicação");
                                    expirouTimeout = false;
                                    sock.Close();
                                    return false;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        AddLog(resp.InfoEmpresa());
                                    else
                                        AddLog("Erro ao receber informação do REP");
                                }
                                else
                                {
                                    AddLog("Resposta incorreta");
                                }
                                sock.Close();
                            }
                            catch (Exception ex)
                            {
                                sock.Close();
                                AddLog(ex.Message);
                                return false;
                            }
                            break;
                        #endregion

                        #region Data/Hora
                        case "!02":

                            try
                            {
                                sock.Connect(IP, Porta);
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
                                        msg = msg + rStrings.ConvertByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    AddLog("Timeout na comunicação");
                                    expirouTimeout = false;
                                    sock.Close();
                                    return false;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        AddLog(resp.InfoDataHora());
                                    else
                                        AddLog("Erro ao receber informação do REP");
                                }
                                else
                                {
                                    AddLog("Resposta incorreta");
                                }
                                sock.Close();
                            }
                            catch (Exception ex)
                            {
                                sock.Close();
                                AddLog(ex.Message);
                                return false;
                            }
                            break;
                        #endregion

                        #region Funcionario
                        case "!03":

                            try
                            {
                                sock.Connect(IP, Porta);
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
                                        msg = msg + rStrings.ConvertByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    AddLog("Timeout na comunicação");
                                    expirouTimeout = false;
                                    sock.Close();
                                    return false;
                                }
                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    //if (resp.Mensagem.Equals("15"))
                                    //    naoEnviados.Add(cmd.ToString());
                                }
                                //else
                                //{
                                //    naoEnviados.Add(cmd.ToString());
                                //}
                                sock.Close();
                            }
                            catch (Exception ex)
                            {
                                sock.Close();
                                AddLog(ex.Message);
                                return false;
                            }
                            break;
                        #endregion

                        #region Biometria
                        case "!05":

                            try
                            {
                                sock.Connect(IP, Porta);
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
                                        msg = msg + rStrings.ConvertByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = VerificaTimeout();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    AddLog("Timeout na comunicação");
                                    expirouTimeout = false;
                                    return false;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        AddLog(resp.InfoFuncionario());
                                    else
                                        AddLog("Erro ao receber informação do REP");
                                }
                                else
                                {
                                    AddLog("Resposta incorreta");
                                }
                                sock.Close();
                            }
                            catch
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

                                    sock.Connect(IP, Porta);
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
                                            msg = msg + rStrings.ConvertByteToString(recebimento);
                                            executa = false;
                                        }
                                        expirouTimeout = VerificaTimeout();
                                        if (expirouTimeout)
                                            executa = false;

                                    } while (executa);
                                    if (expirouTimeout)
                                    {
                                        AddLog("Timeout na comunicação");
                                        expirouTimeout = false;
                                        sock.Close();
                                        return false;
                                    }

                                    if (Comando.VerificaCRC(msg))
                                    {
                                        Comando resp = new Comando(msg);
                                        if (resp.Mensagem.Equals("06"))
                                            AddLog(resp.InfoFuncionario());
                                        else
                                            AddLog("Erro ao receber informação do REP");
                                    }
                                    else
                                    {
                                        AddLog("Resposta incorreta");
                                    }
                                    sock.Close();
                                }
                                catch (Exception ex)
                                {
                                    sock.Close();
                                    AddLog(ex.Message);
                                    return false;
                                }
                                #endregion
                            }

                            if (cmd.Mensagem.Equals("1"))
                            {
                                #region Todas Marcações
                                try
                                {
                                    sock.Connect(IP, Porta);
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
                                            msg = msg + rStrings.ConvertByteToString(recebimento);
                                            executa = false;
                                        }
                                        expirouTimeout = VerificaTimeout();
                                        if (expirouTimeout)
                                            executa = false;

                                    } while (executa);
                                    if (expirouTimeout)
                                    {
                                        AddLog("Timeout na comunicação");
                                        expirouTimeout = false;
                                        sock.Close();
                                        return false;
                                    }

                                    if (Comando.VerificaCRC(msg))
                                    {
                                        Comando resp = new Comando(msg);
                                        if (resp.Mensagem.Equals("06"))
                                            AddLog(resp.InfoFuncionario());
                                        else
                                            AddLog("Erro ao receber informação do REP");
                                    }
                                    else
                                    {
                                        AddLog("Resposta incorreta");
                                    }
                                    sock.Close();
                                }
                                catch (Exception ex)
                                {
                                    sock.Close();
                                    AddLog(ex.Message);
                                    return false;
                                }
                                #endregion
                            }
                            break;
                        #endregion
                    }
                    break;
                #endregion

            }
            return ret;
        }

        public void IniciaTempoTimeout()
        {
            tempoTimeout = DateTime.Now.AddSeconds(30);
        }

        public bool VerificaTimeout()
        {
            DateTime atual = DateTime.Now;
            int res = atual.CompareTo(tempoTimeout);
            if (res >= 0)
                return true;
            else
                return false;
        }

        public void AddLog(string Mensagem, bool NewLine = false)
        {
            if (Log.Text.Length > 0) Log.AppendText(Environment.NewLine);
            Log.AppendText(String.Format("{0}: {1:dd/MM HH:mm} {2}", IP, DateTime.Now, Mensagem.ToUpper()));
            if (NewLine == true) Log.AppendText(Environment.NewLine);
        }

        public void AddLogUnformatted(string Mensagem = "")
        {
            if (Log.Text.Length > 0) Log.AppendText(Environment.NewLine);
            Log.AppendText(Mensagem);
        }

        public void EnviaMensagem(string dados, NetworkStream stream)
        {
            int tam = dados.Length;
            for (int i = 0; i < tam; i++)
                stream.WriteByte((Byte)Convert.ToChar(dados.Substring(i, 1)));
        }

        public int CortaMensagem(byte[] vetorBytes, List<string> marcacoes, List<string> marcacoesnovas, out int intNSR)
        {
            string NFR = ConverteMensagem(vetorBytes, vetorBytes.Length - 17, 17);
            string restantesStr = ConverteMensagem(vetorBytes, 1, 8);
            int restantesInt = Convert.ToInt32(restantesStr, 16);
            intNSR = 0;

            for (int i = 9; i < vetorBytes.Length; )
            {
                intNSR = (vetorBytes[i + 3] << 24) | (vetorBytes[i + 2] << 16) | (vetorBytes[i + 1] << 8) | vetorBytes[i];

                string NSR = Convert.ToString(intNSR);
                //string NSR = Convert.ToString((vetorBytes[i + 3] << 24) | (vetorBytes[i + 2] << 16) | (vetorBytes[i + 1] << 8) | vetorBytes[i]);

                if (NSR.Length < 9)
                {
                    for (int j = NSR.Length; j < 9; j++)
                    {
                        NSR = "0" + NSR;
                    }
                }
                i = i + 4;

                string dia = Convert.ToInt32(BitConverter.ToString(vetorBytes, i, 1), 16).ToString();
                if (dia.Length == 1)
                    dia = "0" + dia;
                string mes = Convert.ToInt32(BitConverter.ToString(vetorBytes, i + 1, 1), 16).ToString();
                if (mes.Length == 1)
                    mes = "0" + mes;
                string ano = Convert.ToInt32(BitConverter.ToString(vetorBytes, i + 2, 1), 16).ToString();
                if (ano.Length == 1)
                    ano = "200" + ano;
                if (ano.Length == 2)
                    ano = "20" + ano;
                string datafinal = dia + "/" + mes + "/" + ano;
                i = i + 3;

                string hora = Convert.ToInt32(BitConverter.ToString(vetorBytes, i, 1), 16).ToString();
                if (hora.Length == 1)
                    hora = "0" + hora;
                string minuto = Convert.ToInt32(BitConverter.ToString(vetorBytes, i + 1, 1), 16).ToString();
                if (minuto.Length == 1)
                    minuto = "0" + minuto;
                string segundo = Convert.ToInt32(BitConverter.ToString(vetorBytes, i + 2, 1), 16).ToString();
                if (segundo.Length == 1)
                    segundo = "0" + segundo;
                string horariofinal = hora + ":" + minuto;
                i = i + 3;

                string PIS = ConverteMensagem(vetorBytes, i, 12);
                i = i + 12;

                //Marcacao marc = new Marcacao(NSR, datafinal, horariofinal, PIS, col.ID, col.EmpresaID, NFR);
                //marc.Salvar();

                AddLogUnformatted(NSR + " " + PIS + " " + datafinal.ToString() + " " + horariofinal.ToString());
                marcacoes.Add(String.Format("{0} {1}", PIS, datafinal.ToString() + " " + horariofinal.ToString()));
                marcacoesnovas.Add(String.Format("{0} {1}", PIS, datafinal.ToString() + " " + horariofinal.ToString()));

                if (i >= (vetorBytes.Length - 17))
                    return restantesInt;
            }
            return restantesInt;
        }

        private string ConverteMensagem(byte[] vetorBytes)
        {
            string resposta = "";
            foreach (byte b in vetorBytes)
            {
                resposta += Convert.ToChar(b);
            }
            return resposta;
        }

        public string ConverteMensagem(byte[] vetorBytes, int index, int count)
        {
            string resposta = "";
            resposta = Encoding.ASCII.GetString(vetorBytes, index, count);
            return resposta;
        }

        private void FinalizarImportacao(string arquivo, int Grupo, List<string> marcacoes, List<string> marcacoesnovas, DB db, int NSR)
        {
            AddLog("TOTAL DE MARCAÇÕES RECEBIDAS: " + marcacoesnovas.Count);

            if (marcacoesnovas.Count > 0)
            {
                rFiles.WriteFile(arquivo, marcacoes);
                AddLog("MARCAÇÕES SALVAS EM " + arquivo);

                db.ProcessarMarcacoes(this, Grupo, marcacoesnovas);
                db.LastNsr = NSR;
            }

            AddLog("FINALIZADO: MARCAÇÕES PROCESSADAS: " + marcacoesnovas.Count, true);
        }
        
        public void RecebeMarcacoesTCP(int Terminal, string arquivo, int Grupo, ref bool ExpirouTimeOut, Principal frmPrincipal)
        {
            List<string> marcacoes = new List<string>();
            List<string> marcacoesnovas = new List<string>();

            ExpirouTimeOut = false;
            DB db = new DB();
            db.Terminal = Terminal;
            int NSR = db.LastNsr;
            //if (NSR == 0) NSR = 1;
            string mensagem = "!06,R,004,";
            string CRC = CalculaCRC(mensagem, CalculaNsr(NSR));
            bool primeiro = true;

            rFiles.ReadFile(arquivo, marcacoes);

            string diretorio = Directory.GetParent(arquivo).FullName;
            rFiles.ForceDirectories(diretorio);

            TcpClient sock = new TcpClient();
            NetworkStream stream;

            sock.Connect(IP, Porta);
            stream = sock.GetStream();

            EnviaMensagem(mensagem.Substring(0, 10), stream);
            EnviaNSR(NSR, stream);
            EnviaMensagem(",", stream);
            EnviaMensagem(CRC, stream);

            bool recolhe = true;

            frmPrincipal.CANCELAR = false;

            AddLog(string.Format("RECOLHENDO MARCAÇÕES  NSR INICIAL: {0}", NSR));

            while (recolhe && !frmPrincipal.CANCELAR)
            {
                Application.DoEvents();
                if (sock.Available > 9)
                {
                    try
                    {
                        byte[] comeco = new byte[10];
                        stream.Read(comeco, 0, 10);
                        string cabecalho = ConverteMensagem(comeco);

                        if (cabecalho == "!06,I,002,")
                        {
                            stream.Read(new byte[sock.Available], 0, sock.Available);
                            break;
                        }

                        int quantidadeBytes = Convert.ToInt32(cabecalho.Substring(6, 3));

                        byte[] byMarcacoes = new byte[quantidadeBytes];

                        while (sock.Available <= quantidadeBytes)
                        {
                        }

                        stream.Read(byMarcacoes, 0, byMarcacoes.Length);

                        AddLog(CortaMensagem(byMarcacoes, marcacoes, marcacoesnovas, out NSR).ToString() + " restantes");

                        byte[] limpa = new byte[3];

                        stream.Read(limpa, 0, limpa.Length);

                        EnviaMensagem("!06,I,002,06,78", stream);
                        primeiro = false;

                        Application.DoEvents();
                    }
                    catch
                    {
                    }
                }
            }

            if (frmPrincipal.CANCELAR)
            {
                AddLog("CANCELADO");
                FinalizarImportacao(arquivo, Grupo, marcacoes, marcacoesnovas, db, NSR);
            }
            else
                if (primeiro)
                    AddLog("NÃO HÁ NOVAS MARCAÇÕES");
                else
                {
                    FinalizarImportacao(arquivo, Grupo, marcacoes, marcacoesnovas, db, NSR);
                }

            stream.Close();
            sock.Close();
        }

        public string CalculaCRC(string mensagem, byte[] arrayNSR)
        {
            string nBytes = mensagem.Length.ToString();
            for (int i = nBytes.Length; i < 3; i++)
                nBytes = "0" + nBytes;

            int soma = 0;

            foreach (char c in mensagem)
            {
                soma = soma + c;
            }
            foreach (byte b in arrayNSR)
            {
                soma = soma + b;
            }

            soma = soma + ',';

            int res = soma % 256;

            string crc = res.ToString("X");
            if (crc.Length < 2)
                crc = "0" + crc;
            return crc;
        }

        public static byte[] CalculaNsr(int data)
        {
            byte[] b = new byte[4];
            
            //b[0] = ((byte)((data >> 24) & 0x000000FF));
            //b[1] = ((byte)((data >> 16) & 0x000000FF));
            //b[2] = ((byte)((data >> 8) & 0x000000FF));
            //b[3] = ((byte)(data & 0x000000FF));

            b[0] = ((byte)(data & 0x000000FF));
            b[1] = ((byte)((data >> 8) & 0x000000FF));
            b[2] = ((byte)((data >> 16) & 0x000000FF));
            b[3] = ((byte)((data >> 24) & 0x000000FF));

            return b;
        }

        public void EnviaNSR(int data, NetworkStream stream)
        {
            stream.WriteByte((byte)(data & 0x000000FF));
            stream.WriteByte((byte)((data >> 8) & 0x000000FF));
            stream.WriteByte((byte)((data >> 16) & 0x000000FF));
            stream.WriteByte((byte)((data >> 24) & 0x000000FF));

            //stream.WriteByte((byte)((data >> 24) & 0x000000FF));
            //stream.WriteByte((byte)((data >> 16) & 0x000000FF));
            //stream.WriteByte((byte)((data >> 8) & 0x000000FF));
            //stream.WriteByte((byte)(data & 0x000000FF));
        }
    }
}
