using System;
using System.Windows.Forms;
using AssepontoRep;
using System.Net.Sockets;
using System.Text;

namespace Trix
{
    class Bridge : AssepontoRep.Bridge
    {
        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
        }

        #region Overrides Abstract
        public override int getPortaPadrao()
        {
            return 3000;
        }

        public override string getRepFabricante()
        {
            return "Trix";
        }

        public override string getArquivoUsb()
        {
            return "";
        }

        public override bool getGerarUsb()
        {
            return false;
        }

        public override bool getGerarBackup()
        {
            return false;
        }
        #endregion

        #region Private
        private bool EnviaComandoTCP(Comando cmd, string Mensagem = "")
        {
            bool expirouTimeout = false;
            bool executa = false;
            bool ret = false;
            bool abortaEnvio = false;

            TcpClient sock = new TcpClient();

            if (cmd.Modo == "S")
            {
                switch (cmd.TipoComando)
                {
                    case (Protocolo.CMD_EMPRESA):
                        {
                            if (Mensagem == "") log.AddLog(Consts.EMPREGADOR_ENVIANDO);
                            break;
                        }
                    case (Protocolo.CMD_DATA_HORA):
                        {
                            if (Mensagem == "") log.AddLog(Consts.DATA_HORA_ENVIANDO);
                            break;
                        }
                    case (Protocolo.CMD_FUNCIONARIO):
                        {
                            if (Mensagem == "") log.AddLog(String.Format(Consts.FUNCIONARIO_ENVIANDO, cmd.Mensagem.Substring(39)));
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
                        sock.Connect(TerminalDados.IP, TerminalDados.Porta);
                        NetworkStream stream = sock.GetStream();

                        byte[] envio = cmd.ToByte();
                        stream.Write(envio, 0, envio.Length);

                        byte[] recebimento = new byte[15];
                        string msg = string.Empty;
                        executa = true;

                        TimeOut timeout = new TimeOut();

                        do
                        {
                            Application.DoEvents();
                            if (sock.Available >= 15)
                            {
                                stream.Read(recebimento, 0, recebimento.Length);

                                msg = msg + Strings.ConvertByteToString(recebimento);
                                executa = false;
                            }
                            expirouTimeout = timeout.Check();
                            if (expirouTimeout)
                                executa = false;
                        } while (executa);
                        if (expirouTimeout)
                        {
                            log.AddLog(Consts.TIMEOUT_COMUNICACAO);

                            if (cmd.TipoComando.Equals("!03"))
                            {
                                log.AddLog(cmd.ToString());
                                expirouTimeout = false;
                                sock.Close();
                                return false;
                            }
                        }

                        Comando resp = new Comando(msg);

                        if (resp.Mensagem.Equals("06"))
                        {
                            log.LogOk();
                            ret = true;
                        }
                        else
                        {
                            log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                        }

                        sock.Close();

                    }
                    catch (Exception ex)
                    {

                        if (cmd.TipoComando.Equals("!03"))
                        {
                            log.AddLog(cmd.ToString());
                            abortaEnvio = true;
                        }
                        sock.Close();
                        log.AddLog(ex.Message);
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
                                sock.Connect(TerminalDados.IP, TerminalDados.Porta);
                                NetworkStream stream = sock.GetStream();

                                byte[] envio = cmd.ToByte();
                                stream.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[290];
                                string msg = string.Empty;
                                executa = true;
                                TimeOut timeout = new TimeOut();
                                do
                                {
                                    Application.DoEvents();
                                    if (sock.Available >= 290)
                                    {
                                        stream.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Strings.ConvertByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = timeout.Check();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    log.AddLog(Consts.TIMEOUT_COMUNICACAO);
                                    expirouTimeout = false;
                                    sock.Close();
                                    return false;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        log.AddLog(resp.InfoEmpresa());
                                    else
                                        log.AddLog(Consts.ERRO_RECEPCAO_INFORMACAO_REP);
                                }
                                else
                                {
                                    log.AddLog("Resposta incorreta");
                                }
                                sock.Close();
                            }
                            catch (Exception ex)
                            {
                                sock.Close();
                                log.AddLog(ex.Message);
                                return false;
                            }
                            break;
                        #endregion

                        #region Data/Hora
                        case "!02":

                            try
                            {
                                sock.Connect(TerminalDados.IP, TerminalDados.Porta);
                                NetworkStream stream = sock.GetStream();

                                byte[] envio = cmd.ToByte();
                                stream.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[25];
                                string msg = string.Empty;
                                executa = true;
                                TimeOut timeout = new TimeOut();
                                do
                                {
                                    Application.DoEvents();
                                    if (sock.Available >= 25)
                                    {
                                        stream.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Strings.ConvertByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = timeout.Check();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    log.AddLog(Consts.TIMEOUT_COMUNICACAO);
                                    expirouTimeout = false;
                                    sock.Close();
                                    return false;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        log.AddLog(resp.InfoDataHora());
                                    else
                                        log.AddLog(Consts.ERRO_RECEPCAO_INFORMACAO_REP);
                                }
                                else
                                {
                                    log.AddLog("Resposta incorreta");
                                }
                                sock.Close();
                            }
                            catch (Exception ex)
                            {
                                sock.Close();
                                log.AddLog(ex.Message);
                                return false;
                            }
                            break;
                        #endregion

                        #region Funcionario
                        case "!03":

                            try
                            {
                                sock.Connect(TerminalDados.IP, TerminalDados.Porta);
                                NetworkStream stream = sock.GetStream();

                                byte[] envio = cmd.ToByte();
                                stream.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[108];
                                string msg = string.Empty;
                                executa = true;
                                TimeOut timeout = new TimeOut();
                                do
                                {
                                    Application.DoEvents();
                                    if (sock.Available >= 108)
                                    {
                                        stream.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Strings.ConvertByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = timeout.Check();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    log.AddLog(Consts.TIMEOUT_COMUNICACAO);
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
                                log.AddLog(ex.Message);
                                return false;
                            }
                            break;
                        #endregion

                        #region Biometria
                        case "!05":

                            try
                            {
                                sock.Connect(TerminalDados.IP, TerminalDados.Porta);
                                NetworkStream stream = sock.GetStream();

                                byte[] envio = cmd.ToByte();
                                stream.Write(envio, 0, envio.Length);

                                byte[] recebimento = new byte[397];
                                string msg = string.Empty;
                                executa = true;
                                TimeOut timeout = new TimeOut();
                                do
                                {
                                    Application.DoEvents();
                                    if (sock.Available >= 397)
                                    {
                                        stream.Read(recebimento, 0, recebimento.Length);
                                        msg = msg + Strings.ConvertByteToString(recebimento);
                                        executa = false;
                                    }
                                    expirouTimeout = timeout.Check();
                                    if (expirouTimeout)
                                        executa = false;
                                } while (executa);
                                if (expirouTimeout)
                                {
                                    log.AddLog(Consts.TIMEOUT_COMUNICACAO);
                                    expirouTimeout = false;
                                    return false;
                                }

                                if (Comando.VerificaCRC(msg))
                                {
                                    Comando resp = new Comando(msg);
                                    if (resp.Mensagem.Equals("06"))
                                        log.AddLog(resp.InfoFuncionario());
                                    else
                                        log.AddLog(Consts.ERRO_RECEPCAO_INFORMACAO_REP);
                                }
                                else
                                {
                                    log.AddLog("Resposta incorreta");
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

                                    sock.Connect(TerminalDados.IP, TerminalDados.Porta);
                                    NetworkStream stream = sock.GetStream();

                                    byte[] envio = cmd.ToByte();
                                    stream.Write(envio, 0, envio.Length);

                                    byte[] recebimento = new byte[397];
                                    string msg = string.Empty;
                                    executa = true;
                                    TimeOut timeout = new TimeOut(); ;
                                    do
                                    {
                                        Application.DoEvents();
                                        if (sock.Available >= 397)
                                        {
                                            stream.Read(recebimento, 0, recebimento.Length);
                                            msg = msg + Strings.ConvertByteToString(recebimento);
                                            executa = false;
                                        }
                                        expirouTimeout = timeout.Check();
                                        if (expirouTimeout)
                                            executa = false;

                                    } while (executa);
                                    if (expirouTimeout)
                                    {
                                        log.AddLog(Consts.TIMEOUT_COMUNICACAO);
                                        expirouTimeout = false;
                                        sock.Close();
                                        return false;
                                    }

                                    if (Comando.VerificaCRC(msg))
                                    {
                                        Comando resp = new Comando(msg);
                                        if (resp.Mensagem.Equals("06"))
                                            log.AddLog(resp.InfoFuncionario());
                                        else
                                            log.AddLog(Consts.ERRO_RECEPCAO_INFORMACAO_REP);
                                    }
                                    else
                                    {
                                        log.AddLog("Resposta incorreta");
                                    }
                                    sock.Close();
                                }
                                catch (Exception ex)
                                {
                                    sock.Close();
                                    log.AddLog(ex.Message);
                                    return false;
                                }
                                #endregion
                            }

                            if (cmd.Mensagem.Equals("1"))
                            {
                                #region Todas Marcações
                                try
                                {
                                    sock.Connect(TerminalDados.IP, TerminalDados.Porta);
                                    NetworkStream stream = sock.GetStream();

                                    byte[] envio = cmd.ToByte();
                                    stream.Write(envio, 0, envio.Length);

                                    byte[] recebimento = new byte[397];
                                    string msg = string.Empty;
                                    executa = true;
                                    TimeOut timeout = new TimeOut();
                                    do
                                    {
                                        Application.DoEvents();
                                        if (sock.Available >= 397)
                                        {
                                            stream.Read(recebimento, 0, recebimento.Length);
                                            msg = msg + Strings.ConvertByteToString(recebimento);
                                            executa = false;
                                        }
                                        expirouTimeout = timeout.Check();
                                        if (expirouTimeout)
                                            executa = false;

                                    } while (executa);
                                    if (expirouTimeout)
                                    {
                                        log.AddLog(Consts.TIMEOUT_COMUNICACAO);
                                        expirouTimeout = false;
                                        sock.Close();
                                        return false;
                                    }

                                    if (Comando.VerificaCRC(msg))
                                    {
                                        Comando resp = new Comando(msg);
                                        if (resp.Mensagem.Equals("06"))
                                            log.AddLog(resp.InfoFuncionario());
                                        else
                                            log.AddLog(Consts.ERRO_RECEPCAO_INFORMACAO_REP);
                                    }
                                    else
                                    {
                                        log.AddLog("Resposta incorreta");
                                    }
                                    sock.Close();
                                }
                                catch (Exception ex)
                                {
                                    sock.Close();
                                    log.AddLog(ex.Message);
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

        private string CalculaCRC(string mensagem, byte[] arrayNSR)
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

        private static byte[] CalculaNsr(int data)
        {
            byte[] b = new byte[4];

            b[0] = ((byte)(data & 0x000000FF));
            b[1] = ((byte)((data >> 8) & 0x000000FF));
            b[2] = ((byte)((data >> 16) & 0x000000FF));
            b[3] = ((byte)((data >> 24) & 0x000000FF));

            return b;
        }

        private void EnviaMensagem(string dados, NetworkStream stream)
        {
            int tam = dados.Length;
            for (int i = 0; i < tam; i++)
                stream.WriteByte((Byte)Convert.ToChar(dados.Substring(i, 1)));
        }

        private void EnviaNSR(int data, NetworkStream stream)
        {
            stream.WriteByte((byte)(data & 0x000000FF));
            stream.WriteByte((byte)((data >> 8) & 0x000000FF));
            stream.WriteByte((byte)((data >> 16) & 0x000000FF));
            stream.WriteByte((byte)((data >> 24) & 0x000000FF));
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

        private string ConverteMensagem(byte[] vetorBytes, int index, int count)
        {
            string resposta = "";
            resposta = Encoding.ASCII.GetString(vetorBytes, index, count);
            return resposta;
        }

        private int CortaMensagem(byte[] vetorBytes, Marcacoes marcacoes, out int intNSR)
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
                string horariofinal = String.Format("{0}:{1}", hora, minuto);
                i = i + 3;

                string PIS = ConverteMensagem(vetorBytes, i, 12);
                i = i + 12;

                marcacoes.Add(PIS, Convert.ToDateTime(datafinal + horariofinal), Convert.ToInt32(NSR));

                if (i >= (vetorBytes.Length - 17))
                    return restantesInt;
            }
            return restantesInt;
        }

        private bool RecebeMarcacoesTCP(Marcacoes marcacoes)
        {
            bool Result = false;

            DBApp db = new DBApp();
            int NSR = db.getLastNsr(TerminalDados.Indice);
            if (NSR == 0) NSR = 1;
            string mensagem = "!06,R,004,";
            string CRC = CalculaCRC(mensagem, CalculaNsr(NSR));
            bool primeiro = true;

            TcpClient sock = new TcpClient();
            NetworkStream stream;

            sock.Connect(TerminalDados.IP, TerminalDados.Porta);
            stream = sock.GetStream();

            EnviaMensagem(mensagem.Substring(0, 10), stream);
            EnviaNSR(NSR, stream);
            EnviaMensagem(",", stream);
            EnviaMensagem(CRC, stream);

            bool recolhe = true;

            log.AddLog(string.Format(Consts.INICIALIZANDO_IMPORTACAO_ARQUIVO_NSR, NSR));

            while (recolhe)
            {
                Application.DoEvents();
                if (sock.Available > 9)
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

                    log.AddLog(CortaMensagem(byMarcacoes, marcacoes, out NSR).ToString() + " restantes");

                    byte[] limpa = new byte[3];

                    stream.Read(limpa, 0, limpa.Length);

                    EnviaMensagem("!06,I,002,06,78", stream);
                    primeiro = false;

                    Application.DoEvents();
                }
            }

            if (Cancelar)
            {
                log.AddLog("CANCELADO");
            }
            else
                if (primeiro)
                    log.AddLog(Consts.MARCACOES_NAO_HA_NOVAS);

            stream.Close();
            sock.Close();

            return marcacoes.Count > 0;
        }
        #endregion

        #region Override
        public override bool sendDataHora(int Terminal)
        {
            base.sendDataHora(Terminal);
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
            string msg = "";
            msg = data + horario;

            EnviaComandoTCP(new Comando(Protocolo.CMD_DATA_HORA, Protocolo.SET, msg));

            return false;
        }

        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            base.sendInfoEmpresa(Terminal, out EmpregadorDados);

            Types.PessoaTipo tipoIdentificador = EmpregadorDados.PessoaTipo;
            string identificador = EmpregadorDados.Pessoa.PadRight(14);
            string cei = EmpregadorDados.Cei.PadRight(12);
            string razao = EmpregadorDados.Nome.PadRight(150);
            string local = EmpregadorDados.Endereco.PadRight(100);

            Comando cmd = new Comando(Protocolo.CMD_EMPRESA, Protocolo.SET, String.Format("{0}{1}{2}{3}{4}", (int)tipoIdentificador + identificador + cei + razao + local));

            return EnviaComandoTCP(cmd);
        }

        public override bool sendFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            base.sendFuncionario(Funcionario);

            bool Result = true;

            foreach (int Cracha in Funcionario.Crachas)
            {
                const string BIOMETRIAMODO_1N = "1";

                string cartao = Cracha.ToString().PadRight(15, '0');
                string pis = Funcionario.Pis.PadRight(12, '0');
                string nome = Funcionario.Nome.PadRight(52);

                string config = (TerminalDados.Barras ? "1" : "0") +
                                (TerminalDados.Proximidade ? "1" : "0") +
                                (TerminalDados.Biometria ? "1" : "0") +
                                (TerminalDados.Teclado ? "1" : "0") +
                                (Funcionario.TecladoPassword != "" ? "1" : "0") +
                                BIOMETRIAMODO_1N;

                string senha = Funcionario.TecladoPassword.PadRight(6);

                Comando cmd = new Comando(Protocolo.CMD_FUNCIONARIO, Protocolo.SET, String.Format("{0}{1}{2}{3}{4}", cartao, pis, config, senha, nome));

                if (!EnviaComandoTCP(cmd))
                {
                    Result = false;
                }
            }

            return Result;
        }

        public override bool deleteFuncionario(Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);

            string msg = Funcionario.Pis.PadRight(12, '0') + "1";
            Comando cmd = new Comando(Protocolo.CMD_EXCLUSAO, Protocolo.SET, msg);
            return EnviaComandoTCP(cmd);
        }

        public override bool LerMarcacoes(Marcacoes marcacoes, TipoImportacaoMarcacoes tipoimportacao)
        {
            base.LerMarcacoes(marcacoes, tipoimportacao);

            bool ExpirouTimeout = false;

            for (int y = 1; y <= 1; y++)
            {
                RecebeMarcacoesTCP(marcacoes);

                if (ExpirouTimeout)
                {
                    log.AddLog(String.Format("RECONECTANDO APÓS TIMEOUT  [TENTATIVA {0}]", y));

                    for (int i = 5; i > 0; i--)
                    {
                        if (i == 1)
                            log.AddLog("CONECTANDO EM 1 SEGUNDO");
                        else
                            log.AddLog(String.Format("CONECTANDO EM {0} SEGUNDOS", i));

                        Application.DoEvents();
                        Wr.Classes.Utils.Pause(1);

                        if (Cancelar)
                        {
                            log.AddLog("RECONEXÃO CANCELADA");
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            return marcacoes.Count > 0;
        }
        #endregion
    }
}
