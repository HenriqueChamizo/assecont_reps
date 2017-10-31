using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Wr;
using iddata.async_communication;
using iddata.async_communication.business.client;
using Wr.Classes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace IdData
{
    public class Rede
    {
        private CSocketClient objSocketClient;
        private CIDSysR30 objIDSysR30;

        string IP;
        int Porta;
        TextBox Log;

        public enum CodigoFuncoesMonitoramento
        {
            TotalPrinterTickets,
            PrinterKM,
            PaperStatus,
            CurrentPaperRollSize,
            CurrentPaperRollKM,
            CurrentPaperRollTicketsPrinted,
            CurrentPaperRollEstimatedTickets,
            Temperatura,
        }

        public struct FuncaoMonitoramento
        {
            public CodigoFuncoesMonitoramento Codigo;
            public string MensagemStart;
            public string MensagemFim;

            public FuncaoMonitoramento(CodigoFuncoesMonitoramento Codigo, string MensagemStart, string MensagemFim)
            {
                this.Codigo = Codigo;
                this.MensagemStart = MensagemStart;
                this.MensagemFim = MensagemFim;
            }

            public static FuncaoMonitoramento GetFuncao(CodigoFuncoesMonitoramento Codigo)
            {
                FuncaoMonitoramento Result = Monitoramento[0];

                foreach (FuncaoMonitoramento funcao in Monitoramento)
                {
                    if (funcao.Codigo == Codigo)
                    {
                        Result = funcao;
                        break;
                    }
                }

                return Result;
            }
        }

        static readonly IList<FuncaoMonitoramento> Monitoramento = new ReadOnlyCollection<FuncaoMonitoramento>
            (new[] {
             new FuncaoMonitoramento (CodigoFuncoesMonitoramento.TotalPrinterTickets, "OBTENDO TOTAL DE TICKETS", "TOTAL DE TICKETS: {0}"),
             new FuncaoMonitoramento (CodigoFuncoesMonitoramento.PrinterKM, "OBTENDO QUILOMETRAGEM", "QUILOMETRAGEM: {0} KM"),
             new FuncaoMonitoramento (CodigoFuncoesMonitoramento.PaperStatus, "OBTENDO STATUS DO PAPEL", "STATUS DO PAPEL: {0}"),
             new FuncaoMonitoramento (CodigoFuncoesMonitoramento.CurrentPaperRollSize, "OBTENDO TAMANHO DO PAPEL", "TAMANHO DO PAPEL: {0}"),
             new FuncaoMonitoramento (CodigoFuncoesMonitoramento.CurrentPaperRollKM, "OBTENDO TAMANHO DO PAPEL EM KM", "TAMANHO DO PAPEL EM KM: {0}"),
             new FuncaoMonitoramento (CodigoFuncoesMonitoramento.CurrentPaperRollTicketsPrinted, "OBTENDO TOTAL DE TICKETS IMPRESSOS", "TOTAL DE TICKETS IMPRESSOS: {0}"),
             new FuncaoMonitoramento (CodigoFuncoesMonitoramento.CurrentPaperRollEstimatedTickets, "OBTENDO ESTIMATIVA DE TICKETS RESTANTES", "QUANTIDADE ESTIMATIVA DE TICKETS RESTANTES: {0}"),
             new FuncaoMonitoramento (CodigoFuncoesMonitoramento.Temperatura, "OBTENDO TEMPERATURA", "TEMPERATURA DO TERMINAL: {0}"),
        });

        public Rede(CIDSysR30 objIDSysR30, string ip, int porta, TextBox log/*, CSocketClient socket = null*/)
        {
            this.IP = ip;
            this.Porta = porta;
            this.Log = log;

            try
            {
                this.objIDSysR30 = objIDSysR30;
                this.objSocketClient = new CSocketClient(IP, Porta);
            }
            catch (Exception exError)
            {
                this.AddLog(exError.Message);
            }
        }

        public Rede(TextBox log)
        {
            this.Log = log;
        }

        public void AddLog(string Mensagem, bool NewLine = false)
        {
            if (Log.Text.Length > 0) Log.AppendText(Environment.NewLine);
            Log.AppendText(IP + ": " + String.Format("{0:dd/MM HH:mm}", DateTime.Now) + " " + Mensagem.ToUpper());
            if (NewLine == true) Log.AppendText(Environment.NewLine);
        }

        public void AddLogUnformatted(string Mensagem)
        {
            if (Log.Text.Length > 0) Log.AppendText(Environment.NewLine);
            Log.AppendText(Mensagem);
        }

        public void LogSucesso()
        {
            this.AddLog("Comando enviado com sucesso", true);
        }

        public void LogErro()
        {
            this.AddLog("Erro no REP ao receber comando", true);
        }

        public void AddLogStatus(int Status)
        {
            switch (Status)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    break;
                case -1:
                    this.AddLog("Cartão de Proximidade já cadastrado para outro usuário.");
                    break;
                case -2:
                    this.AddLog("Cartão de Código de Barras já cadastrado para outro usuário.");
                    break;
                case -3:
                    this.AddLog("PIS já cadastrado para outro usuário.");
                    break;
                case -4:
                    this.AddLog("Código individual já cadastrado para outro usuário.");
                    break;
                case -5:
                    this.AddLog("Erro na memória MRP.");
                    break;
                case -6:
                    this.AddLog("Erro na memória MT.");
                    break;
                case -7:
                    this.AddLog("Erro na memória RAM.");
                    break;
                case -8:
                    this.AddLog("Dados enviados inválidos.");
                    break;
                case -9:
                    this.AddLog("ID REP não possui trabalhadores cadastrados.");
                    break;
                case -10:
                    this.AddLog("Trabalhador não cadastrado.");
                    break;
                case -11:
                    this.AddLog("ID REP não possui o cadastro do empregador.");
                    break;
                case -12:
                    this.AddLog("Dados do empregador inválidos: CPF / CNPJ.");
                    break;
                case -13:
                    this.AddLog("Dados do empregador inválidos: Nome / Razão Social.");
                    break;
                case -14:
                    this.AddLog("Dados do empregador inválidos: Endereço.");
                    break;
                case -15:
                    this.AddLog("Data e/ou hora inválida(s).");
                    break;
                case -16:
                    this.AddLog("Erro no módulo biométrico: ERROR.");
                    break;
                case -17:
                    this.AddLog("Erro no módulo biométrico: TIMEOUT.");
                    break;
                case -18:
                    this.AddLog("Dados de comunicação inválidos: Endereço IP.");
                    break;
                case -19:
                    this.AddLog("Dados de comunicação inválidos: Máscara de sub-rede.");
                    break;
                case -20:
                    this.AddLog("Dados de comunicação inválidos: IP Gateway.");
                    break;
                case -21:
                    this.AddLog("Não existem eventos.");
                    break;
                case -22:
                    this.AddLog("Erro no módulo biométrico: CHEIO.");
                    break;
                case -23:
                    this.AddLog("Erro na leitura do módulo biométrico: ERROR.");
                    break;
                case -24:
                    this.AddLog("Erro na leitura do módulo biométrico: TIMEOUT.");
                    break;
                case -25:
                    this.AddLog("Erro de checksum da área de dados.");
                    break;
                case -26:
                    this.AddLog("Dados do empregador inválidos: CEI.");
                    break;
                case -27:
                    this.AddLog("Equipamento bloqueado.");
                    break;
                case -100:
                    this.AddLog("Erro no checksum do cabeçalho do pacote (Verificação na DLL).");
                    break;
                case -101:
                    this.AddLog("Erro no checksum dos dados do pacote (Verificação na DLL).");
                    break;
                case -102:
                    this.AddLog("Comando inválido (Verificação na DLL).");
                    break;
                case -103:
                    this.AddLog("Erro pacote inválido (Verificação na DLL).");
                    break;
                case -104:
                    this.AddLog("Erro no tamanho do pacote: pacote vazio (Verificação na DLL).");
                    break;
                case -105:
                    this.AddLog("Erro no tamanho dos dados (Verificação na DLL).");
                    break;
                default:
                    this.AddLog("Erro desconhecido.");
                    break;
            }
        }

        public bool Connection_Init()
        {
            this.objSocketClient.Connect();

            this.AddLog("CONECTANDO COM O TERMINAL");

            return this.Connection_Verification();
        }

        private bool Connection_Send(byte[] rgbyBuffer)
        {
            this.objSocketClient.SendBuffer(rgbyBuffer);

            return this.Connection_Verification();
        }

        public bool Connection_Finalize()
        {
            if (this.objSocketClient.Connected)
            {
                //AddLog("DESCONECTANDO COM O TERMINAL");
                this.objSocketClient.Disconnect();
            }

            return true;
            //return this.Connection_Verification();
        }

        private bool Connection_Verification()
        {
            bool bEndVerification = false;
            bool bReturn = false;
            DateTime dtmTIMEOUT = DateTime.Now.AddSeconds(30);

            while (bEndVerification == false)
            {
                switch (this.objSocketClient.CurrentConnectionState)
                {
                    case enumConnState.None:
                        {
                        }
                        break;
                    case enumConnState.ConnectionAttempt:
                        {
                        }
                        break;
                    case enumConnState.ConnectionAttemptFailed:
                        {
                            this.AddLog("FALHA AO TENTAR CONECTAR COM O TERMINAL");
                            bEndVerification = true;
                            bReturn = false;
                        }
                        break;
                    case enumConnState.Connected:
                        {
                            //this.AddLog("CONECTADO COM O TERMINAL COM SUCESSO");
                            bEndVerification = true;
                            bReturn = true;
                        }
                        break;
                    case enumConnState.ConnectionError:
                        {
                            this.AddLog("ERRO DE CONEXÃO COM O TERMINAL");
                            bEndVerification = true;
                            bReturn = false;
                        }
                        break;
                    case enumConnState.Disconnected:
                        {
                            //this.AddLog("DESCONECTADO DO TERMINAL");
                            bEndVerification = true;
                            bReturn = true;
                        }
                        break;
                    case enumConnState.DataSent:
                        {
                            bReturn = true;
                        }
                        break;
                    case enumConnState.ReceivedData:
                        {
                            bEndVerification = true;
                            bReturn = true;
                        }
                        break;
                }

                if (DateTime.Now >= dtmTIMEOUT)
                {
                    this.AddLog("TIMEOUT - DESCONECTANDO COM O TERMINAL");
                    this.Connection_Finalize();
                    bEndVerification = true;
                    bReturn = false;
                }
            }

            return bReturn;
        }

        public void IdData_EnviaEmpresa(byte IdentificadorTipo, string Identificador, string Nome, ulong Cei, string Endereco)
        {
            try
            {
                if (this.Connection_Init() == false)
                {
                    return;
                }

                this.AddLog("ENVIANDO CADASTRO DO EMPREGADOR");

                this.Connection_Send(this.objIDSysR30.SetEmployer(IdentificadorTipo, Identificador, Cei, Nome, Endereco));

                int iReturn = this.objIDSysR30.PacketAvail(this.objSocketClient.GetReceivedData());

                if (iReturn == 0)
                {
                    this.AddLog("CADASTRO DO EMPREGADOR ENVIADO COM SUCESSO");
                }
                else
                {
                    this.AddLogStatus(iReturn);
                }
            }
            catch (Exception exError)
            {
                this.AddLog(exError.Message);
            }
            finally
            {
                this.Connection_Finalize();
            }
        }

        public void IdData_EnviaDataHora()
        {
            try
            {
                if (!this.Connection_Init()) return;

                this.AddLog("ENVIANDO DATA E HORA");

                this.Connection_Send(this.objIDSysR30.SetDateTime((byte)DateTime.Now.Day, (byte)DateTime.Now.Month, (ushort)DateTime.Now.Year, (byte)DateTime.Now.Hour, (byte)DateTime.Now.Minute, (byte)DateTime.Now.Second));

                int iReturn = this.objIDSysR30.PacketAvail(this.objSocketClient.GetReceivedData());

                if (iReturn == 0)
                {
                    this.AddLog("DATA E HORA ATUALIZADA COM SUCESSO");
                }
                else
                {
                    this.AddLogStatus(iReturn);
                }
            }
            catch (Exception exError)
            {
                this.AddLog(exError.Message);
            }
            finally
            {
                this.Connection_Finalize();
            }
        }

        public void IdData_HorarioVerao(string Inicio, string Fim)
        {
            try
            {
                if (!this.Connection_Init()) return;

                this.AddLog("ENVIANDO HORARIO DE VERAO");

                DateTime dtmDST_Start = Convert.ToDateTime(Inicio);
                DateTime dtmDST_End = Convert.ToDateTime(Fim);

                this.Connection_Send(this.objIDSysR30.SetDST(dtmDST_Start, dtmDST_End));

                int iReturn = this.objIDSysR30.PacketAvail(this.objSocketClient.GetReceivedData());

                if (iReturn == 0)
                {
                    this.AddLog("HORARIO DE VERAO ATUALIZADO COM SUCESSO");
                }
                else
                {
                    this.AddLogStatus(iReturn);
                }
            }
            catch (Exception exError)
            {
                this.AddLog(exError.Message);
            }
            finally
            {
                this.Connection_Finalize();
            }
        }

        public void IdData_RecebeMarcacoes(int Terminal, string arquivo, int Grupo, Principal frmPrincipal)
        // o parâmetro Form é para processar se o usuário pressionou <ESC>
        {
            int iReturn = 0;
            uint uiTotalNSR = 0;
            DB db = new DB();
            frmPrincipal.CANCELAR = false;

            db.Terminal = Terminal;
            uint uiStartNSR = db.LastNsr + 1;

            List<string> marcacoes = new List<string>();
            List<string> marcacoesnovas = new List<string>();

            Files.ReadFile(arquivo, marcacoes);

            uint uiNSR = 0;
            byte byRegType = 0;
            byte byRegDateDay = 0;
            byte byRegDateMonth = 0;
            ushort usRegDateYear = 0;
            byte byRegTimeHour = 0;
            byte byRegTimeMin = 0;
            string strPIS = "";
            string strDataHora = "";

            byte byOpType = 0;
            string strUsername = "";

            uint uiTotalLogType3 = 0;

            try
            {
                if (this.Connection_Init() == false)
                {
                    return;
                }

                this.Connection_Send(this.objIDSysR30.RequestTotalNSR());

                iReturn = this.objIDSysR30.PacketAvail(this.objSocketClient.GetReceivedData());

                if (iReturn != 0)
                {
                    this.AddLogStatus(iReturn);
                    return;
                }

                this.objIDSysR30.GetTotalNSR(ref uiTotalNSR);

                this.AddLog("RECEBENDO MARCAÇÕES NSR INICIAL: " + uiStartNSR + ", NSR TOTAL: " + uiTotalNSR + "  <ESC> PARA CANCELAR");

                for (uint uiIdx = uiStartNSR; uiIdx <= uiTotalNSR; uiIdx++)
                {
                    this.Connection_Send(this.objIDSysR30.RequestEventByNSR(uiIdx));

                    try
                    {
                        iReturn = this.objIDSysR30.PacketAvail(this.objSocketClient.GetReceivedData());

                        Application.DoEvents();

                        if (iReturn >= 0)
                        {
                            switch (iReturn)
                            {
                                case 2:
                                    //this.GetLogType2();
                                    break;
                                case 3:
                                    {
                                        uiTotalLogType3++;
                                        objIDSysR30.GetLogType3(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref strPIS);

                                        if ((strPIS != "") && (strPIS[0] >= Convert.ToChar("0")) && (strPIS[0] <= Convert.ToChar("9")))
                                        {
                                            strDataHora = String.Format("{0:00}", byRegDateDay) + "/" +
                                                          String.Format("{0:00}", byRegDateMonth) + "/" +
                                                          String.Format("{0:0000}", usRegDateYear) + " " +
                                                          String.Format("{0:00}", byRegTimeHour) + ":" +
                                                          String.Format("{0:00}", byRegTimeMin);

                                            strPIS = strPIS.PadLeft(12);

                                            AddLogUnformatted(String.Format("MARCAÇÃO RECEBIDA PIS: {0} {1} {2}", strPIS, strDataHora, uiNSR));
                                            marcacoes.Add(String.Format("{0} {1} {2}", strPIS, strDataHora, uiNSR));
                                            marcacoesnovas.Add(String.Format("{0} {1} {2}", strPIS, strDataHora, uiNSR));
                                        }
                                        else
                                            AddLog("MARCAÇÃO RECEBIDA: ERRO");
                                    }
                                    break;
                                case 4:
                                    // this.GetLogType4();
                                    AddLog("AJUSTE DE DATA/HORA: ");
                                    break;
                                case 5:
                                    objIDSysR30.GetLogType5(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref byOpType, ref strPIS, ref strUsername);

                                    if ((strPIS != "") && (strPIS[0] >= Convert.ToChar("0")) && (strPIS[0] <= Convert.ToChar("9")))
                                    {
                                        strDataHora = String.Format("{0:00}", byRegDateDay) + "/" +
                                                        String.Format("{0:00}", byRegDateMonth) + "/" +
                                                        String.Format("{0:0000}", usRegDateYear) + " " +
                                                        String.Format("{0:00}", byRegTimeHour) + ":" +
                                                        String.Format("{0:00}", byRegTimeMin);

                                        strPIS = strPIS.PadLeft(12);

                                        AddLogUnformatted(String.Format("REGISTRO DE PIS ENVIADO: {0} {1} {2} {3}", uiIdx, strPIS, strDataHora, strUsername));
                                    }
                                    else
                                    {
                                        AddLog("REGISTRO DE CADASTRO ENVIADO: ERRO");
                                    }

                                    break;
                                default:
                                    {
                                        this.AddLogStatus(iReturn);
                                    }
                                    break;
                            }
                        }

                    }
                    catch (Exception exError)
                    {
                        AddLog(exError.Message);
                    }

                    if (frmPrincipal.CANCELAR)
                    {
                        AddLog("CANCELADO");
                        break;
                    }
                }

                AddLog("TOTAL DE MARCAÇÕES RECEBIDAS: " + uiTotalLogType3.ToString());

                if (uiTotalLogType3 > 0)
                {
                    string diretorio = Directory.GetParent(arquivo).FullName;
                    if (!Directory.Exists(diretorio)) Directory.CreateDirectory(diretorio);

                    using (StreamWriter file = new StreamWriter(arquivo))
                        foreach (string s in marcacoes)
                        {
                            file.WriteLine(s);
                        }

                    AddLog("MARCAÇÕES SALVAS EM " + arquivo);

                    db.ProcessarMarcacoes(Grupo, marcacoesnovas, this);
                }

                if (uiNSR > 0)
                {
                    db.LastNsr = uiNSR;
                }
            }
            catch (Exception exError)
            {
                AddLog(exError.Message);
            }
            finally
            {
                Connection_Finalize();
            }
        }

        public void IdData_ExcluirFuncionario(string strPIS)
        {
            try
            {
                this.AddLog("EXCLUINDO FUNCIONÁRIO PIS: " + strPIS);

                this.Connection_Send(this.objIDSysR30.DeleteUser(strPIS));

                int iReturn = this.objIDSysR30.PacketAvail(this.objSocketClient.GetReceivedData());

                if (iReturn == 0)
                {
                    AddLog("FUNCIONÁRIO EXCLUÍDO");
                }
                else
                {
                    this.AddLogStatus(iReturn);
                }
            }
            catch (Exception exError)
            {
                this.AddLog(exError.Message);
            }
        }

        public int IdData_EnviarFuncionarioSemBiometria(string strPIS, string strUserName, uint uiKeyCode, string strBarCode, byte byFacilityCode, uint uiProxCode, byte byUserType, string strPassword, bool SobreporSeExistir)
        {
            System.IO.MemoryStream msPhoto = new System.IO.MemoryStream();
            ushort usSizeSample = 0;
            byte byQuantitySamples = 0;
            byte[] rgbyBiometrics = new byte[1];
            byte byAccessType = 0;

            int iReturn = 0;

            try
            {
                if (strBarCode != String.Empty)
                    this.AddLog("ENVIANDO FUNCIONÁRIO PIS: " + strPIS + ", CRACHA: " + strBarCode);
                else
                    this.AddLog("ENVIANDO FUNCIONÁRIO PIS: " + strPIS);

                this.Connection_Send(this.objIDSysR30.AddUser(strPIS,
                                         strUserName,
                                         uiKeyCode,
                                         strBarCode,
                                         byFacilityCode,
                                         uiProxCode,
                                         byUserType,
                                         byAccessType,
                                         strPassword,
                                         msPhoto,
                                         usSizeSample,
                                         byQuantitySamples,
                                         rgbyBiometrics));

                iReturn = this.objIDSysR30.PacketAvail(this.objSocketClient.GetReceivedData());

                // Erro -3 - PIS já cadastrado para outro usário
                // então enviamos um comando de alteração de usuário
                if (iReturn == 0 || iReturn == -3)
                {
                    if (iReturn == 0)
                    {
                        AddLog("FUNCIONÁRIO ENVIADO");
                    }
                    else if (iReturn == -3)
                    {
                        if (SobreporSeExistir)
                        {
                            this.Connection_Send(this.objIDSysR30.ChangeUserData(strPIS, strPIS, strUserName, uiKeyCode, strBarCode, byFacilityCode, uiProxCode, byUserType, strPassword, msPhoto, usSizeSample, byQuantitySamples, rgbyBiometrics));

                            iReturn = this.objIDSysR30.PacketAvail(this.objSocketClient.GetReceivedData());

                            if (iReturn == 0)
                            {
                                AddLog("FUNCIONÁRIO ATUALIZADO");
                            }
                            else
                            {
                                this.AddLogStatus(iReturn);
                            }
                        }
                        else
                        {
                            AddLog("FUNCIONÁRIO JÁ EXISTE NO TERMINAL");
                        }
                    }
                }
                else
                {
                    this.AddLogStatus(iReturn);
                }
            }
            catch (Exception exError)
            {
                this.AddLog(exError.Message);
            }

            return iReturn;
        }

        public void IdData_EnviarFuncionarioComBiometria(string strPIS,
            string strUserName,
            uint uiKeyCode,
            string strBarCode,
            byte byFacilityCode,
            uint uiProxCode,
            byte byUserType,
            byte byAccessType,
            string strPassword,
            ushort usSizeSample,
            byte byQuantitySamples,
            byte[] rgbyBiometrics)
        {
            System.IO.MemoryStream msPhoto = new System.IO.MemoryStream();

            int iReturn = 0;

            try
            {
                AddLog("ENVIANDO FUNCIONÁRIO PIS: " + strPIS);

                Connection_Send(objIDSysR30.AddUser(strPIS,
                                    strUserName,
                                    uiKeyCode,
                                    strBarCode,
                                    byFacilityCode,
                                    uiProxCode,
                                    byUserType,
                                    byAccessType,
                                    strPassword,
                                    msPhoto,
                                    usSizeSample,
                                    byQuantitySamples,
                                    rgbyBiometrics));

                iReturn = this.objIDSysR30.PacketAvail(this.objSocketClient.GetReceivedData());

                // Erro -3 - PIS já cadastrado para outro usuário
                // então enviamos um comando de alteração de usuário
                if (iReturn == 0 || iReturn == -3)
                {
                    if (iReturn == 0)
                    {
                        this.AddLog("FUNCIONÁRIO ENVIADO COM SUCESSO");
                    }
                    else if (iReturn == -3)
                    {
                        this.Connection_Send(this.objIDSysR30.ChangeUserData(strPIS, strPIS, strUserName, uiKeyCode, strBarCode, byFacilityCode, uiProxCode, byUserType, strPassword, msPhoto, usSizeSample, byQuantitySamples, rgbyBiometrics));

                        iReturn = this.objIDSysR30.PacketAvail(this.objSocketClient.GetReceivedData());

                        if (iReturn == 0)
                        {
                            this.AddLog("FUNCIONÁRIO ENVIADO COM SUCESSO");
                        }
                        else
                        {
                            this.AddLogStatus(iReturn);
                        }
                    }
                }
                else
                {
                    this.AddLogStatus(iReturn);
                }
            }
            catch (Exception exError)
            {
                this.AddLog(exError.Message);
            }
        }

        public void IdData_Get_CrachaFuncionario(string strPIS)
        {
            int iReturn = 0;

            AddLog("LENDO FUNCIONÁRIO NO TERMINAL PIS: " + strPIS);

            this.Connection_Send(this.objIDSysR30.ReadUserData(strPIS));
            iReturn = this.objIDSysR30.PacketAvail(this.objSocketClient.GetReceivedData());

            if (iReturn >= 0)
            {
                string strPis = "";
                string strUserName = "";
                uint uiKeyCode = 0;
                string strBarCode = "";
                byte byFacilityCode = 0;
                ushort usProxCode = 0;
                byte byUserType = 0;
                string strPassword = "";
                ushort usSizePhoto = 0;
                System.IO.MemoryStream msPhoto = null;
                ushort usSizeSample = 0;
                byte byQuantitySamples = 0;
                byte[] rgbyBiometric_Sample1 = new byte[404];
                byte[] rgbyBiometric_Sample2 = new byte[404];

                this.objIDSysR30.GetUserData(ref strPis,
                    ref strUserName,
                    ref uiKeyCode,
                    ref strBarCode,
                    ref byFacilityCode,
                    ref usProxCode,
                    ref byUserType,
                    ref strPassword,
                    ref usSizePhoto,
                    ref msPhoto,
                    ref usSizeSample,
                    ref byQuantitySamples,
                    ref rgbyBiometric_Sample1,
                    ref rgbyBiometric_Sample2);

                AddLogUnformatted("DADOS DO FUNCIONÁRIO");
                AddLogUnformatted("PIS...............................: " + rStrings.FormatString(strPis, "###.#####.##-#"));
                AddLogUnformatted("Nome do Trabalhador...............: " + strUserName);
                AddLogUnformatted("Código de Barras..................: " + strBarCode.ToString());
                AddLogUnformatted("Código Proximidade................: " + usProxCode.ToString());
                AddLogUnformatted("Senha.............................: " + strPassword.ToString());
                AddLogUnformatted("Quant. Amostras...................: " + byQuantitySamples.ToString());
                AddLogUnformatted("Tamanho Amostras..................: " + usSizeSample.ToString());
            }
            else
            {
                AddLogStatus(iReturn);
            }
        }

        public void IdData_Criar_USBFile(int Terminal, string Arquivo, List<int> Funcionarios)
        {
            AddLog("GERANDO " + Funcionarios.Count + " FUNCIONÁRIO(S), ARQUIVO: " + Arquivo + " ");

            DB db = new DB();

            string Identificador;
            string Nome;
            ulong Cei;
            string Endereco;
            byte IdentificadorTipo;

            db.LerEmpresa(Terminal, out IdentificadorTipo, out Identificador, out Nome, out Cei, out Endereco);

            string strPath = Arquivo;
            uint uiQuantityUsers = Convert.ToUInt32(Funcionarios.Count);
            string strEmployerName = Nome;
            string strCNPJ_CPF = Identificador;
            string strCEI = Cei.ToString();
            string strEmployerAddress = Endereco;
            string strNFR = "";

            try
            {
                int iReturn = this.objIDSysR30.CreateFile(strPath, uiQuantityUsers, strEmployerName, strCNPJ_CPF, strCEI, strEmployerAddress, strNFR);

                if (iReturn < 0)
                {
                    AddLog("Erro ao criar o arquivo " + strPath + ". Erro: " + iReturn.ToString());
                    return;
                }

                for (int i = 0; i <= Funcionarios.Count - 1; i++)
                {
                    IdData_Exportar_USBUser(db, Funcionarios[i]);
                }

                IdData_Fechar_USBFile();
            }
            catch (Exception exError)
            {
                AddLog("Erro : " + exError.Message);
            }

        }

        private void IdData_Exportar_USBUser(DB db, int Funcionario)
        {
            SqlCommand comm = db.Conn.CreateCommand();

            comm.CommandText = String.Format("SELECT FUNC_PIS, FUNC_NOME, CRA_NUMERO FROM Funcionarios " +
                "INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND " +
                "LEFT JOIN Crachas ON CRA_FUNC = FUNC_IND " +
                "WHERE FUNC_IND = {0}", Funcionario);

            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                string strPIS = reader["FUNC_PIS"].ToString().Trim();
                string strUserName = reader["FUNC_NOME"].ToString().Trim();
                uint uiKeyCode = 0;

                string strBarCode = reader["CRA_NUMERO"].ToString();
                uint uiProxCode = 0;
                byte byUserType = 0;
                string strPassword = "";
                byte byAccessType = 0;
                uint uiSizeSample = 0;
                byte[] rgbyBiometrics = new byte[1];

                this.objIDSysR30.ExportUbsUser(strPIS,
                    strUserName,
                    uiKeyCode,
                    strBarCode,
                    uiProxCode,
                    byUserType,
                    strPassword,
                    byAccessType,
                    rgbyBiometrics,
                    uiSizeSample);
            }

            reader.Close();
        }

        private void IdData_Fechar_USBFile()
        {
            try
            {
                int iReturn = this.objIDSysR30.FileClose();
                //int iReturn = this.objIDSysR30.CloseUBSFile();

                if (iReturn < 0)
                {
                    AddLog("Erro ao fechar o arquivo. Erro: " + iReturn.ToString());
                    return;
                }

                AddLog("ARQUIVO GERADO COM SUCESSO");
            }
            catch (Exception exError)
            {
                MessageBox.Show("Erro : " + exError.Message);
            }
        }

        public void IdData_Monitoramento(CodigoFuncoesMonitoramento codigofuncao)
        {
            FuncaoMonitoramento funcao = FuncaoMonitoramento.GetFuncao(codigofuncao);

            try
            {
                if (!this.Connection_Init()) return;

                this.AddLog(funcao.MensagemStart);

                switch (funcao.Codigo)
                {
                    case CodigoFuncoesMonitoramento.TotalPrinterTickets:
                        this.Connection_Send(this.objIDSysR30.RequestTotalPrinterTickets());
                        break;
                    case CodigoFuncoesMonitoramento.PrinterKM:
                        this.Connection_Send(this.objIDSysR30.RequestPrinterKM());
                        break;
                    case CodigoFuncoesMonitoramento.PaperStatus:
                        this.Connection_Send(this.objIDSysR30.RequestPaperStatus());
                        break;
                    case CodigoFuncoesMonitoramento.CurrentPaperRollSize:
                        this.Connection_Send(this.objIDSysR30.RequestCurrentPaperRollSize());
                        break;
                    case CodigoFuncoesMonitoramento.CurrentPaperRollKM:
                        this.Connection_Send(this.objIDSysR30.RequestCurrentPaperRollKM());
                        break;
                    case CodigoFuncoesMonitoramento.CurrentPaperRollTicketsPrinted:
                        this.Connection_Send(this.objIDSysR30.RequestCurrentPaperRollTicketsPrinted());
                        break;
                    case CodigoFuncoesMonitoramento.CurrentPaperRollEstimatedTickets:
                        this.Connection_Send(this.objIDSysR30.RequestCurrentPaperRollEstimatedTickets());
                        break;
                    case CodigoFuncoesMonitoramento.Temperatura:
                        this.Connection_Send(this.objIDSysR30.RequestTemperature());
                        break;
                }

                int iReturn = this.objIDSysR30.PacketAvail(this.objSocketClient.GetReceivedData());

                if (iReturn == 0)
                {
                    switch (funcao.Codigo)
                    {
                        case CodigoFuncoesMonitoramento.TotalPrinterTickets:
                            this.objIDSysR30.GetTotalPrinterTickets(ref iReturn);
                            break;
                        case CodigoFuncoesMonitoramento.PrinterKM:
                            this.objIDSysR30.GetPrinterKM(ref iReturn);
                            break;
                        case CodigoFuncoesMonitoramento.PaperStatus:
                            this.objIDSysR30.GetPaperStatus(ref iReturn);
                            break;
                        case CodigoFuncoesMonitoramento.CurrentPaperRollSize:
                            this.objIDSysR30.GetCurrentPaperRollSize(ref iReturn);
                            break;
                        case CodigoFuncoesMonitoramento.CurrentPaperRollKM:
                            this.objIDSysR30.GetCurrentPaperRollKM(ref iReturn);
                            break;
                        case CodigoFuncoesMonitoramento.CurrentPaperRollTicketsPrinted:
                            this.objIDSysR30.GetCurrentPaperRollTicketsPrinted(ref iReturn);
                            break;
                        case CodigoFuncoesMonitoramento.CurrentPaperRollEstimatedTickets:
                            this.objIDSysR30.GetCurrentPaperRollEstimatedTickets(ref iReturn);
                            break;
                        case CodigoFuncoesMonitoramento.Temperatura:
                            this.objIDSysR30.GetTemperature(ref iReturn);
                            break;
                    }

                    this.AddLog(String.Format(funcao.MensagemFim, iReturn));
                }
                else
                {
                    this.AddLogStatus(iReturn);
                }
            }
            catch (Exception exError)
            {
                this.AddLog(exError.Message);
            }
            finally
            {
                this.Connection_Finalize();
            }
        }
    }
}
