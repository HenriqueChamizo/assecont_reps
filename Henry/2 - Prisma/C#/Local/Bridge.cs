using System;
using System.Windows.Forms;
using AssepontoRep;
using System.Collections.Generic;
using Wr.Classes;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.IO;

namespace Henry
{
    class Convert
    {
        private static byte CheckSum(string S)
        {
            int Result = 0;

            for (int i = 0; i < S.Length; i++)
            {
                Result = Result ^ S[i];
            }

            Result = Result ^ (S.Length & 0xFF);
            Result = Result ^ ((S.Length >> 8) & 0xFF);

            return (byte)Result;

        }

        public static byte[] ConvertStringToBytes(string Pacote)
        {
            List<byte> Result = new List<byte>();

            const byte CONST_START_BYTE = 02; // Byte inicial.
            const byte CONST_END_BYTE = 03; // Byte final.

            Result.Add(CONST_START_BYTE);
            Result.Add((byte)(Pacote.Length & 0xFF));
            Result.Add((byte)((byte)((byte)(Pacote.Length) >> 8) & 0xFF));

            for (int i = 0; i < Pacote.Length; i++)
            {
                Result.Add(System.Convert.ToByte(Pacote[i]));
            }

            Result.Add(CheckSum(Pacote));
            Result.Add(CONST_END_BYTE);

            return Result.ToArray();
        }
    }

    class Bridge : AssepontoRep.Bridge
    {
        int NMarcacoes = 0;
        Marcacoes marcacoes;
        int NSR = 0;

        //Testado no Firmware:

        private const string FILE_CONFIGURATION = "rep_configuracao.txt";
        private const string FILE_EMPLOYER = "rep_empregador.txt";
        private const string FILE_STAFF = "rep_colaborador.txt";
        private string FILE_PATH = Folders.folderArquivoUsb("Henry");

        #region Enum
        private enum CodigosErro
        {
            [Description("Não há dados")]
            NaoHaDados = 1,
            [Description("Comando desconhecido")]
            ComandoDesconhecido = 10,
            [Description("Tamanho do pacote é inválido")]
            TamanhoPacoteInvalido = 11,
            [Description("Parâmetros informados são inválidos")]
            ParametrosInformadosInvalidos = 12,
            [Description("Tamanho dos parâmetros são inválidos")]
            ParametrosTamanhoInvalido = 14,
            [Description("Número da mensagem é inválido")]
            NumeroMensagemInvalido = 15,
            [Description("Equipamento não possui eventos")]
            EquipamentoNaoPossuiEventos = 28,
            [Description("Documento do empregador é inválido")]
            EmpregadorDocumentoInvalido = 30,
            [Description("Tipo do documento do empregador é inválido")]
            EmpreadorDocumentoTipoInvalido = 31,
            [Description("Ip é inválido")]
            IpInvalido = 32,
            [Description("Tipo de operação do usuário é inválida")]
            OperacaoTipoUsuarioInvalida = 33,
            [Description("PIS do empregado é inválido")]
            FuncionarioPisInvalido = 34,
            [Description("Cei do empregador é inválido")]
            EmpregadorCEIInvalido = 35,
            [Description("Referencia do empregado é inválido")]
            FuncionarioReferenciaInvalido = 36,
            [Description("Nome do empregado é inválido")]
            FuncionarioNome = 37,
            [Description("Matrícula já existe")]
            MaticulaJaExiste = 61,
            [Description("Pis já existe")]
            PisJaExiste = 62,
            [Description("Opção inválida")]
            OpcaoInvalida = 63,
            [Description("Matrícula não existe")]
            MatriculaNaoExiste = 64,
            [Description("Comando informado é inválido")]
            ComandoInformadoInvalido = 105,
            [Description("Data hora do comando é inválida")]
            DataHoraInvalida = 106,
            [Description("Evento é inválido")]
            EventoInvalido = 107,
        }
        #endregion

        #region Constructor
        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog) { }
        #endregion

        #region Override Abstract
        public override int getPortaPadrao() { return 3000; }
        public override string getRepFabricante() { return "Henry"; }
        public override string getArquivoUsb() { return "Henry"; }
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
        public override bool getCadastroTerminalResponsavel(){ return false; }
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
        public override bool sendDataHora(int Terminal)
        {
            base.sendDataHora(Terminal);
            string Answer = "";
            string cmd = String.Format("01+EH+00+{0}]00/00/00]00/00/00", DateTime.Now.ToString("dd/MM/yy HH:mm:00"));
            Send(cmd, out Answer);

            return ProcessAnswer(Answer, cmd);
        }

        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            base.sendInfoEmpresa(Terminal, out EmpregadorDados);

            string Answer = "";
            bool Result = false;

            Send(String.Format("01+EE+00+{0}]{1}]]{2}]{3}", (int)EmpregadorDados.PessoaTipo, EmpregadorDados.Pessoa, EmpregadorDados.Nome, EmpregadorDados.Endereco), out Answer);

            Result = ProcessAnswer(Answer);

            return Result;
        }

        public override bool sendFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            base.sendFuncionario(Funcionario);

            bool Result = true;
            string Answer = "";

            foreach (int Cracha in Funcionario.Crachas)
            {
                Send(String.Format("01+EU+00+1+I[{0}[{1}[{2}[{3}[{4}", Funcionario.Pis, Funcionario.Nome, 1, 1, Cracha), out Answer);

                if (!ProcessAnswer(Answer))
                {
                    Result = false;
                }
            }

            return Result;
        }

        public override bool deleteFuncionario(Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);

            bool Result = true;
            string Answer = "";

            foreach (int Cracha in Funcionario.Crachas)
            {
                Send(String.Format("01+EU+00+1+E[{0}[{1}[{2}[{3}[{4}", Funcionario.Pis, Funcionario.Nome, 1, 1, Cracha), out Answer);
            }

            if (!ProcessAnswer(Answer))
            {
                Result = false;
            }

            return Result;
        }

        public override bool LerMarcacoes(Marcacoes marcacoes, TipoImportacaoMarcacoes tipoimportacao)
        {
            base.LerMarcacoes(marcacoes, tipoimportacao);

            this.marcacoes = marcacoes;

            string Answer = "";

            if (tipoimportacao == TipoImportacaoMarcacoes.Backup)
            {
                if (!GetNMarcacoes()) return false;

                if (NMarcacoes == 0)
                {
                    log.AddLog(Consts.MARCACOES_NAO_HA_NOVAS);
                    return false;
                }
            }

            int NsrInicio = (tipoimportacao == TipoImportacaoMarcacoes.OnlyNew ? (new DBApp()).getLastNsr(TerminalDados.Indice) + 1 : 1);
            Send(String.Format("01+RR+00+N]999999999]{0}", NsrInicio), out Answer);

            return ProcessAnswer(Answer);
        }
        #endregion

        #region Private
        public void Send(string Packet, out string Answer)
        {
            bool executa = true;
            byte[] bytes = new byte[50];

            Client socket = new Client(TerminalDados.IP, TerminalDados.Porta, log);
            NetworkStream stream = socket.GetStream();
            bytes = Convert.ConvertStringToBytes(Packet);
            stream.Write(bytes, 0, bytes.Length);
            byte[] recebimento = new byte[50];
            Answer = "";

            TimeOut timeout = new TimeOut();

            try
            {
                do
                {
                    Application.DoEvents();

                    if (socket.Available > 0)
                    {
                        do
                        {
                            Wr.Classes.Utils.InitializeBytes(ref recebimento);
                            stream.Read(recebimento, 0, recebimento.Length);
                            Answer += Strings.Convert.BytesToString(recebimento);
                        }

                        while (socket.Available > 0);

                        timeout.SetSmallerTime();
                    }

                    executa = !timeout.Check();

                } while (executa);

                if (Answer == "")
                {
                    Answer = Consts.TIMEOUT_COMUNICACAO;
                }
            }
            finally
            {
                socket.Close();
            }

            if (Answer != "")
            {
                int CharDesprezarInicio = 2;

                if (Packet.StartsWith("01+RR+00")) // retorno de registros inicia em uma posição diferente
                    CharDesprezarInicio = 3;

                Answer = Answer.Remove(Answer.Length - 2).Substring(CharDesprezarInicio);
            }
        }

        private bool GetNMarcacoes()
        {
            string Answer = "";
            Send("01+RQ+00+R", out Answer);

            return ProcessAnswer(Answer);
        }

        private void ProcessMarcacoes(string Marcacoes)
        {
            //string[] arrayMarcacoes = Regex.Split(Marcacoes, "\r\n");
            string[] arrayMarcacoes = Regex.Split(Marcacoes, Environment.NewLine);

            marcacoes.Clear();

            foreach (string S in arrayMarcacoes)
            {
                AssepontoRep.Marcacoes.Marcacao marcacao;

                if (marcacoes.InterpretarRegistroAfd(S, out marcacao))
                {
                    if (marcacao.Tipo == AssepontoRep.Marcacoes.TiposRegistroAfd.Marcacao)
                    {
                        marcacoes.Add(marcacao);
                    }

                    if (marcacao.NSR > 0)
                        NSR = marcacao.NSR;
                }
            }

            log.AddLog(String.Format(Consts.MARCACOES_A_PROCESSAR, marcacoes.Count));

            (new DBApp()).setLastNsr(TerminalDados.Indice, NSR);
        }

        private bool ProcessAnswer(string Answer, string data = "")
        {
            bool Result = false;

            if (Answer.StartsWith("01+EH+00"))
            {
                log.AddLog(Consts.DATA_HORA_ATUALIZADA_SUCESSO);
                Result = true;
            }
            else
                if (Answer.StartsWith("01+EE+00"))
                {
                    log.AddLog(Consts.EMPREGADOR_ENVIADO_SUCESSO);
                    Result = true;
                }
                else
                    if (Answer.StartsWith("01+EU+00"))
                    {
                        log.LogOk();
                        Result = true;
                    }
                    else
                        if (Answer.StartsWith("01+RQ+00"))
                        {
                            NMarcacoes = System.Convert.ToInt32(Answer.Substring(11));
                            log.AddLog(String.Format(Consts.REGISTROS_LENDO, NMarcacoes));
                            Result = true;
                        }
                        else
                            if (Answer.StartsWith("01+RR+0+") || Answer.StartsWith("01+RR+000+"))
                            {
                                string temp = Answer.Substring(12);
                                NMarcacoes = System.Convert.ToInt32(temp.Substring(0, temp.IndexOf(']')));
                                ProcessMarcacoes(temp.Substring(temp.IndexOf(']') + 1));
                                Result = true;
                            }
                            else
                                if (Answer.StartsWith("1+RR+0+") || Answer.StartsWith("1+RR+000+"))
                                {
                                    string temp = Answer.Substring(10); //7
                                    NMarcacoes = System.Convert.ToInt32(temp.Substring(0, temp.IndexOf(']')));
                                    ProcessMarcacoes(temp.Substring(temp.IndexOf(']') + 1));
                                    Result = true;
                                }
                                else
                                    if (Answer.StartsWith("1+RR+"))
                                    {
                                        int CodigoErro = System.Convert.ToInt32(Answer.Substring(5));

                                        log.AddLog((CodigosErro.IsDefined(typeof(CodigosErro), CodigoErro)) ? Wr.Classes.Utils.GetDescription((CodigosErro)CodigoErro) : String.Format(Consts.ERRO_ENVIO_COMANDO_CODIGO, Answer));
                                    }
                                    else
                                    {
                                        int CodigoErro = System.Convert.ToInt32(Answer.Substring(5));
                                        log.AddLog(String.Format(Consts.ERRO_ENVIO_COMANDO_CODIGO, CodigoErro));
                                    }

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
                                (string.IsNullOrEmpty(func.Barras) ? string.IsNullOrEmpty(func.Teclado) ? func.Proximidade : func.Teclado : func.Barras ).PadLeft(20, '0')));
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
        public override bool GerarArquivoEmpregador(int Terminal, Types.Opcao Opcao , out Types.Empregador EmpregadorDados)
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
    }
}
