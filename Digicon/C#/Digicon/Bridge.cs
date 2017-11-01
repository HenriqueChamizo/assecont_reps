using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DigiconFrameworkServer;
using AssepontoRep;
using DigiconFrameworkServer.Objects.CommandObjects;
using DigiconFrameworkServer.Control;
using DigiconFrameworkServer.Objects.MessageObjects;
using DigiconFrameworkServer.Objects.MessageObjects.AsyncObjects;
using DigiconFrameworkServer.Objects.MessageObjects.CommandReturnObjects;
using DigiconFrameworkServer.Objects.MessageObjects.ResponseObjects;
using DigiconFrameworkServer.Objects.MessageObjects.EventObjects;
using DigiconFrameworkServer.Objects.MessageObjects.RequestObjects;
using System.Windows.Forms;
using DigiconFrameworkServer.Objects.ClockObjects;
using DigiconFrameworkServer.Info;

namespace Digicon
{
    public class Bridge : AssepontoRep.Bridge
    {
        #region Classes
        private class Devices
        {
            private Dictionary<string, int> devices = new Dictionary<string, int>();

            public void Add(string ip, int id)
            {
                if (!devices.ContainsKey(ip)) devices.Add(ip, id);
            }

            public void Clear()
            {
                devices.Clear();
            }

            public int getDeviceId(string IP)
            {
                if (devices.ContainsKey(IP))
                    return devices[IP];
                else
                    return 0;
            }

            public string getDeviceIP(int Device)
            {
                string Result = "";

                if (devices.ContainsValue(Device))
                    foreach (KeyValuePair<string, int> device in devices)
                    {
                        if (device.Value == Device)
                            Result = device.Key;
                    }

                return Result;
            }

            public bool isServerOnline()
            {
                return devices.Count > 0;
            }
        }
        #endregion

        private Devices devices = new Devices();
        private DigiconServer DFS = new DigiconServer();
        private bool start = false;
        private string IPLocal;
        private TextBox consoleLog;
        //private TipoImportacaoMarcacoes tipoimportacaomarcacoes;

        public delegate void updateResp(string message);

        public Bridge(TextBox edLog)
            : base(Consts.ONLINE, edLog)
        {
            consoleLog = edLog;
            IPLocal = Wr.Classes.Net.getLocalIPAddress();
            MessageManager.MessageReceivedChanged += new MessageReceivedHandler(MenssageAccessReceive);
            MessageManagerClock.MessageReceivedChangedClock += new MessageReceivedHandlerClock(MenssageClockReceive);
        }

        #region Overrides Abstract
        public override int getPortaPadrao()
        {
            return 3000;
        }

        public override string getRepFabricante()
        {
            return String.Format("Digicon: {0}", Wr.Classes.Net.getLocalIPAddress());
        }
        #endregion

        #region Funções de apoio
        private void AtualizaResp(string strMensagem)
        {
            strMensagem = strMensagem.Replace("\n", Environment.NewLine);
            consoleLog.AppendText(strMensagem);
            consoleLog.ScrollToCaret();
        }

        private string formatMensagem(string IP, string Comando, string Status)
        {
            return "\n" + IP + " " + String.Format("{0:dd/MM HH:mm}", DateTime.Now) + " " + Comando + ": " + Status;
        }

        public override bool checkServerIsOnline()
        {
            bool Result = devices.isServerOnline();

            if (!Result)
            {
                MessageBox.Show(Consts.SERVIDOR_OFFLINE, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Result;
        }
        #endregion

        public void StopDFS()
        {
            DFS.Stop();
        }

        public void StopServer()
        {
            log.AddLog(Consts.SERVIDOR_PARANDO);
            StopDFS();
            start = false;
            devices.Clear();
        }

        public void StartServer()
        {
            if (start) return;

            if (DFS.Start(IPLocal, 3000, 2) == 0)
            {
                start = true;
                log.AddLog(Consts.SERVIDOR_INICIALIZANDO);
            }
            else
            {
                log.AddLog(Consts.SERVIDOR_PORTA_EM_USO);
                devices.Clear();
            }
        }

        #region Overrides
        public override bool sendDataHora(int Terminal)
        {
            if (!base.sendDataHora(Terminal)) return false;

            UpdateDateTime udt = new UpdateDateTime();
            udt.Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            udt.DeviceID = devices.getDeviceId(TerminalDados.IP);
            udt.SeqCmd = 2;
            udt.CommStatus = 1;

            if (DFS.Execute(udt) == -1)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
            }

            return true;
        }

        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            if (!base.sendInfoEmpresa(Terminal, out EmpregadorDados)) return false;

            ManagerCompany obj = new ManagerCompany();
            obj.DeviceID = devices.getDeviceId(TerminalDados.IP);
            obj.Company.Cei = EmpregadorDados.Cei != "" ? EmpregadorDados.Cei : "000000000000";
            obj.Company.Identifier = EmpregadorDados.Pessoa;
            obj.Company.SocialName = EmpregadorDados.Nome;
            obj.Company.Local = EmpregadorDados.Endereco;

            DFS.Execute(obj);

            return true;
        }

        public override bool sendFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            bool Result = base.sendFuncionario(Funcionario);

            EmployeeListClock employeeListsClock = new EmployeeListClock();
            Person person = new Person();

            if (Funcionario.Crachas.Count > 0)
            {
                CardClock[] cardClock = new CardClock[Funcionario.Crachas.Count];

                for (int i = 0; i < Funcionario.Crachas.Count; i++)
                {
                    cardClock[i] = new CardClock();

                    if (TerminalDados.Barras)
                    {
                        cardClock[i].CardTec = 1;
                    }
                    else
                        if (TerminalDados.Proximidade)
                        {
                            cardClock[i].CardTec = 2;
                        }
                        else
                            if (TerminalDados.Smartcard)
                            {
                                cardClock[i].CardTec = 3;
                            }

                    byte[] card = new byte[8];
                    card = BitConverter.GetBytes(Funcionario.Crachas[i]);
                    Array.Reverse(card);
                    cardClock[i].CardID = card;
                }

                person.CardClock = cardClock;
            }

            person.Name = Funcionario.Nome;
            person.PersonID = Funcionario.Pis.PadRight(30, ' ');
            person.Pis = Convert.ToInt64(Funcionario.Pis);
            person.RecordKeyboard = (byte)(TerminalDados.Teclado ? 1 : 0);
            person.TypeOfValidation = 2;

            employeeListsClock.Person = person;

            LoadList loadList = new LoadList();
            loadList.DeviceID = devices.getDeviceId(TerminalDados.IP);
            loadList.CommStatus = 2;
            loadList.SeqCmd = 10;
            loadList.Type = 9;
            loadList.List = employeeListsClock;
            loadList.OperationType = 0;

            return (DFS.Execute(loadList) >= 0);
        }

        public override bool deleteFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            bool Result = base.deleteFuncionario(Funcionario);

            EmployeeListClock employeeListsClock = new EmployeeListClock();
            Person person = new Person();

            person.Name = Funcionario.Nome;
            person.PersonID = Funcionario.Pis.PadRight(30, ' ');
            person.Pis = Convert.ToInt64(Funcionario.Pis);
            person.RecordKeyboard = (byte)(TerminalDados.Teclado ? 1 : 0);
            person.TypeOfValidation = 2;

            employeeListsClock.Person = person;

            LoadList loadList = new LoadList();
            loadList.DeviceID = devices.getDeviceId(TerminalDados.IP);
            loadList.CommStatus = 2;
            loadList.SeqCmd = 10;
            loadList.Type = 9;
            loadList.List = employeeListsClock;
            loadList.OperationType = 2;

            return (DFS.Execute(loadList) >= 0);
        }

        public override void InicializarImportacaoMarcacoesOnline(TipoImportacaoMarcacoes tipoimportacaomarcacoes)
        {
            base.InicializarImportacaoMarcacoesOnline(tipoimportacaomarcacoes);
            //this.tipoimportacaomarcacoes = tipoimportacaomarcacoes;

            switch (tipoimportacaomarcacoes)
            {
                case TipoImportacaoMarcacoes.Backup:
                    {
                        RequestBackupLog obj = new RequestBackupLog();
                        obj.DeviceID = devices.getDeviceId(TerminalDados.IP);

                        DFS.Execute(obj);
                        break;
                    }
                case TipoImportacaoMarcacoes.OnlyNew:
                    {
                        RequestLog obj = new RequestLog();
                        obj.DeviceID = devices.getDeviceId(TerminalDados.IP);

                        DFS.Execute(obj);
                        break;
                    }
            }
        }
        #endregion

        public void ProcessarMarcacoes(Marcacoes marcacoes)
        {
            if (marcacoes.Count == 0) return;

            DBApp db = new DBApp();
            db.openConnection();
            db.Inicializar_TabTemp_DescontoDsr();

            string msg = "\n" + string.Format(Consts.MARCACOES_A_PROCESSAR, marcacoes.Count);
            consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });

            int contador = 0;
            int erros = 0;

            try
            {
                foreach (Marcacoes.Marcacao marcacao in marcacoes.listMarcacoes)
                {
                    string Pis;
                    string Data;
                    string Hora;

                    Pis = marcacao.Pis;
                    if (Pis.Substring(0, 1) == "0")
                        Pis = Pis.Substring(1);

                    Data = marcacao.DataHora.ToShortDateString();
                    Hora = marcacao.DataHora.ToShortTimeString();

                    msg = String.Format("\n{0}/{1}: {2} {3} {4}", contador + 1, marcacoes.listMarcacoes.Count, Pis, Data, Hora);
                    consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });

                    try
                    {
                        db.executeCommand(String.Format("EXEC importar_marcacao @P_GRUPO = {0},@P_PIS = '{1}', @P_DATA = '{2}', @P_HORA = '{3}'", marcacoes.TerminalDados.Grupo, Pis, Data, Hora), false);
                        contador++;
                    }
                    catch (Exception e)
                    {
                        erros++;
                        msg = "\n" + e.ToString();
                        consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
                    }

                    Application.DoEvents();
                }
            }
            finally
            {
                if (erros > 0)
                {
                    msg = "\n" + String.Format(Consts.MARCACOES_PROCESSAR_ERROS, erros);
                    consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
                }

                if (contador > 0)
                {
                    msg = "\n" + String.Format(Consts.MARCACOES_PROCESSAR_FINALIZADO, contador, marcacoes.listMarcacoes.Count);
                    consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
                }

                db.Finalizar_TabTemp_DescontoDsr();
                db.closeConnection();
            }
        }

        #region Access Messages
        public void MenssageAccessReceive(MessageReceivedArgs args)
        {
            //LogWriter.Instance.WriteLog(LogWriterType.DEBUG, "Delegate - Mensagem Recebida...");

            if ((args.Msg.MsgType != MessageType.MSG_ACCESS_REQUEST_CARD) &&
                 (args.Msg.MsgType != MessageType.MSG_ACCESS_REQUEST_PERSON) &&
                 (args.Msg.MsgType != MessageType.MSG_TEMPLATE_REQUEST) &&
                 (args.Msg.MsgType != MessageType.MSG_DATA_REQUEST_CARD) &&
                 (args.Msg.MsgType != MessageType.MSG_CONNECTION_REQUEST) &&
                 (args.Msg.MsgType != MessageType.MSG_DATA_REQUEST_PERSON) &&
                 (args.Msg.MsgType != MessageType.MSG_SYNC_RETURN) &&
                 !(args.Msg is EventConnChangeStatus))
            {
                //LogWriter.Instance.WriteLog(LogWriterType.DEBUG, "Delegate - Enviando ACK" + args.Msg.MsgType.ToString());
                DFS.Ack(args.Msg.DeviceID);
            }

            Session session = DFS.Session(args.Msg.DeviceID);
            devices.Add(session.IpDevice, args.Msg.DeviceID);

            #region MSG_CONNECTION_REQUEST
            if (args.Msg.MsgType == MessageType.MSG_CONNECTION_REQUEST)
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { formatMensagem(devices.getDeviceIP(((DeviceConnectedMessage)args.Msg).DeviceID), ((DeviceConnectedMessage)args.Msg).DeviceTyp.ToString() + ": Versão de Firmware: " + ((DeviceConnectedMessage)args.Msg).FirmwareVersion.ToString(), "ONLINE") });
            }
            #endregion MSG_CONNECTION_REQUEST

            #region MSG_EVENT --
            if (args.Msg.MsgType == MessageType.MSG_EVENT)
            {
                #region Event Generic
                String msg;
                if ((EventMessageType)((EventMessage)args.Msg).EventID != EventMessageType.MSG_EVENT_CONN_CHANGED)
                {
                    msg = "*** Evento ";
                    msg += ((EventMessage)args.Msg).EventType == 1 ? "Online ***" : "Offline ***";
                    msg += "\nCod evento....= " + ((EventMessage)args.Msg).EventID.ToString() +
                           "\nDeviceID.......= " + ((EventMessage)args.Msg).DeviceID.ToString() +
                           "\nReaderID.......= " + ((EventMessage)args.Msg).ReaderID.ToString() +
                           "\nData............= " + ((EventMessage)args.Msg).Timestamp.ToString() +
                           "\nDireção.......= " + ((EventMessage)args.Msg).AcessDirection.ToString() +
                           "\nVerão..........= " + ((EventMessage)args.Msg).SummerTime.ToString() +
                           "\nTipo.............= " + ((EventMessage)args.Msg).IdentificationType.ToString() + "\nID...............=  ";
                    msg += BitConverter.ToString(((EventMessage)args.Msg).IdentificationData).Replace("-", "");
                    msg += "\nGmt........." + ((EventMessage)args.Msg).GMT1.ToString();

                #endregion Event Generic

                    #region ACCESS_GRANTED
                    if ((EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_ACCESS_GRANTED ||
                   (EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_ACCESS_GRANTED_CHEAT ||
                   (EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_ACCESS_GRANTED_COERCION ||
                   (EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_ACCESS_GRANTED_FRISK ||
                   (EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_ACCESS_GRANTED_MASTER_CARD ||
                   (EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_ACCESS_GRANTED_OUT_REPOSE ||
                   (EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_ACCESS_VALID_ACCOMPANY
                  )
                    {
                        msg += "\n\nTecla pressionada........" + ((EventAccessGrantedBase)args.Msg).KeyPressed.ToString() +
                                "\nMsgQuantity..............." + ((EventAccessGrantedBase)args.Msg).MsgQuantity.ToString() +
                                "\nAcessCredits.............." + ((EventAccessGrantedBase)args.Msg).AcessCredits.ToString() +
                                "\nCardLevel................." + ((EventAccessGrantedBase)args.Msg).CardLevel.ToString();
                    }
                    #endregion ACCESS_GRANTED

                    #region ACCESS_DENIED

                    else if ((EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_ACCESS_DENIED_LEVEL)
                    {
                        msg += "\nTipo de fluxo ........ " + ((EventAccessDeniedLevel)args.Msg).FlowTipe.ToString() +
                            "\nNível do cartão ........ " + ((EventAccessDeniedLevel)args.Msg).CardLevel.ToString();
                    }

                    else if ((EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_ACCESS_DENIED_ANTIPASSBACK)
                    {
                        msg += "\nTipo de fluxo ........ " + ((EventAccessDeniedAntiPassback)args.Msg).FlowTipe.ToString() +
                            "\nNível do cartão ........ " + ((EventAccessDeniedAntiPassback)args.Msg).CardLevel.ToString();
                    }
                    else if ((EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_ACCESS_DENIED_REMOVAL)
                    {
                        msg += "\nTipo de fluxo ........ " + ((EventAccessDeniedRemoval)args.Msg).FlowTipe.ToString() +
                            "\nNível do cartão ........ " + ((EventAccessDeniedRemoval)args.Msg).CardLevel.ToString();
                    }

                    #endregion ACCESS_DENIED

                    #region INPUT_CHANGED

                    else if ((EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_INPUT_CHANGED)
                    {
                        msg += "InputId" + ((EventInputChanged)args.Msg).InputId.ToString() +
                           "\nInputStatus" + ((EventInputChanged)args.Msg).InputStatus.ToString();
                    }
                    #endregion INPUT_CHANGED

                    #region INPUT_MEMORY_CONTROL

                    else if ((EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_MEMORY_CONTROL)
                    {
                        msg += "MemoryFreeSpace" + ((EventMemoryControl)args.Msg).MemoryFreeSpace.ToString();
                    }
                    #endregion INPUT_MEMORY_CONTROL

                    #region MSG_EVENT_FIRMWARE_STARTED

                    else if ((EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_FIRMWARE_STARTED)
                    {
                        msg += "\nMemoria livre ....." + ((EventFirmwareInitialized)args.Msg).MemoryFreeSpace.ToString();
                    }
                    #endregion MSG_EVENT_FIRMWARE_STARTED

                    #region TEMPLATE_REGISTERED

                    else if ((EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_TEMPLATE_REGISTERED)
                    {
                        msg += "\nTotal Templates = " + ((EventTemplateRegister)args.Msg).TemplateQuantity.ToString() + "\n";
                        msg += "\nSize = " + ((EventTemplateRegister)args.Msg).Payload.ToString() + "\n";
                        for (int i = 0; i < ((EventTemplateRegister)args.Msg).TemplateQuantity; i++)
                        {
                            msg += "\nTemplate " + i.ToString() +
                           "\nTemplateVendor = " + ((EventTemplateRegister)args.Msg).Template[i].TemplateVendor.ToString() +
                             "\nTemplateSize = " + ((EventTemplateRegister)args.Msg).Template[i].TemplateSize.ToString() +
                             "\nTemplate \n";
                            msg += BitConverter.ToString(((EventTemplateRegister)args.Msg).Template[i].Template) + "\n";
                        }
                    }
                    #endregion TEMPLATE_REGISTERED

                    #region BIOMETRIC_READER_NOT_FOUND

                    else if ((EventMessageType)((EventMessage)args.Msg).EventID == EventMessageType.MSG_EVENT_BIOMETRIC_READER_NOT_FOUND)
                    {
                        msg += "Device IP ....." + ((EventUnregisteredBioReader)args.Msg).DeviceIp[0].ToString() + "." +
                                                    ((EventUnregisteredBioReader)args.Msg).DeviceIp[1].ToString() + "." +
                                                    ((EventUnregisteredBioReader)args.Msg).DeviceIp[2].ToString() + "." +
                                                    ((EventUnregisteredBioReader)args.Msg).DeviceIp[3].ToString() +
                          "\nReader Location ....." + ((EventUnregisteredBioReader)args.Msg).ReaderLocation.ToString();
                    }
                    #endregion BIOMETRIC_READER_NOT_FOUND


                    consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
                }

            }
            #endregion MSG_EVENT --

            #region Mensagens Assync --
            if (args.Msg.MsgType == MessageType.MSG_SYNC_RETURN)
            {
                String BuffMsg = "";
                #region CommandReturn Main
                if (
                    args.Msg is AutoProcessResp ||
                    args.Msg is BlockDeviceResp ||
                    args.Msg is CheckBioResp ||
                    args.Msg is ConfigFirmwareResp ||
                    args.Msg is DeletListResp ||
                    args.Msg is DisableDigitalOutResp ||
                    args.Msg is DisableEmergencyResp ||
                    args.Msg is EnableDigitalOutResp ||
                    args.Msg is EnableEmergencyResp ||
                    args.Msg is HandkeyCalibrateResp ||
                    args.Msg is LoadBadgeIDsResp ||
                    args.Msg is UpdateDateTimeResp ||
                    args.Msg is UpdateSummerTimeResp ||
                    args.Msg is DigitalOutStatusResp ||
                    args.Msg is DigitalInStatusResp ||
                    args.Msg is LoadListResp ||
                    args.Msg is ListStatusResp ||
                    args.Msg is AlarmBackupResp ||
                    args.Msg is DeviceStatusResp ||
                    args.Msg is AccessBackupResp ||
                    args.Msg is UnblockDeviceResp ||
                    args.Msg is LoadSmartMapResp ||
                    args.Msg is UpdateBioResp)
                {
                    DFS.Ack(args.Msg.DeviceID);
                    BuffMsg +=
                        "Retorno de Comandos" +
                        "\nRetorno Tipo  " + (CommandReturnType)((CommandReturn)args.Msg).CommandId +
                        "\nDeviceID      " + ((CommandReturn)args.Msg).DeviceId.ToString() +
                        "\nSequencia CMD " + ((CommandReturn)args.Msg).CmdSeq.ToString() +
                        "\nCodigo de ret " + ((CommandReturn)args.Msg).ReturnCode.ToString() +
                        "\nData Ger CMD  " + ((CommandReturn)args.Msg).CmdGenerationTimestamp.ToString() +
                        "\nData Exe CMD  " + ((CommandReturn)args.Msg).CmdExecutionTimestamp.ToString() +
                        "\nTam dos Dados " + ((CommandReturn)args.Msg).PayloadSize.ToString();

                }
                #endregion CommandReturn Main

                #region LIST_STATUS --*

                if (((CommandReturn)args.Msg).CommandId == (int)CommandReturnType.LIST_STATUS
                                                    && ((CommandReturn)args.Msg).ReturnCode == 1)
                {
                    BuffMsg += "\nSolicitado = " + ((ListStatusResp)args.Msg).Time.ToString() +
                                 "\nTipo da Lista = " + ((ListStatusResp)args.Msg).ListType +
                                 "\nQuantidade de Registros = " + ((ListStatusResp)args.Msg).TotReg +
                                 "\nFree       = " + ((ListStatusResp)args.Msg).TotFree.ToString();
                }
                #endregion

                #region ALARM_BACKUP --*

                if (args.Msg.GetType().ToString().CompareTo("DigiconFrameworkServer.Objects.MessageObjects.AsyncObjects.AlarmBackupResp") == 0)
                {
                    BuffMsg += "\nTotal Alarm = " + ((AlarmBackupResp)args.Msg).TotEvent.ToString() + "\n\n" +
                        "--------------------------------------------------------------------\n";
                    for (int i = 0, n = 1; i < (int)((AlarmBackupResp)args.Msg).TotEvent; i++, n++)
                    {
                        BuffMsg += "Evento nº " + n.ToString() +
                                "\nId Evento  = " + ((AlarmBackupResp)args.Msg).Events[i].EventID.ToString() +
                                  "\nDate    = " + ((AlarmBackupResp)args.Msg).Events[i].Timestamp.ToString() +
                                  "\nDireção = " + ((AlarmBackupResp)args.Msg).Events[i].AcessDirection.ToString() +
                                  "\nVerão   = " + ((AlarmBackupResp)args.Msg).Events[i].SummerTime.ToString() +
                                  "\ntipo    = " + ((AlarmBackupResp)args.Msg).Events[i].IdentificationType.ToString() +
                                  "\nid      = " + BitConverter.ToString(((AlarmBackupResp)args.Msg).Events[i].IdentificationData).Replace("-", "") +
                                  "\nGmt     = " + ((AlarmBackupResp)args.Msg).Events[i].GMT1.ToString() +
                                  "\nReader  = " + ((AlarmBackupResp)args.Msg).Events[i].ReaderID.ToString() + " \n " +
                                   "--------------------------------------------------------------------\n";
                    }
                }
                #endregion

                #region DEVICE_STATUS -- *
                //Device status
                if (((CommandReturn)args.Msg).CommandId == (int)CommandReturnType.DEVICE_STATUS
                                                        && ((CommandReturn)args.Msg).ReturnCode == 1)
                {
                    BuffMsg +=
                    "\nRetorno Tipo  " + (CommandReturnType)((CommandReturn)args.Msg).CommandId +

                        "\nVersion          : " + ((DeviceStatusResp)args.Msg).FirmwareVersion[0] + "." +
                                                  ((DeviceStatusResp)args.Msg).FirmwareVersion[1] + "." +
                                                  ((DeviceStatusResp)args.Msg).FirmwareVersion[2] + "." +
                                                  ((DeviceStatusResp)args.Msg).FirmwareVersion[3] +
                      "\nLastFirmwareUpdate : " + ((DeviceStatusResp)args.Msg).LastFirmwareUpdate.ToString() +
                      "\nGmt                : " + ((DeviceStatusResp)args.Msg).Gmt.ToString() +
                      "\nLastConfUpdate     : " + ((DeviceStatusResp)args.Msg).LastConfUpdate.ToString() +
                      "\nInitSummerTime     : " + ((DeviceStatusResp)args.Msg).InitSummerTime.ToString() +
                      "\nEndSummerTime      : " + ((DeviceStatusResp)args.Msg).EndSummerTime.ToString() +
                      "\nDeviceIP           : " + ((DeviceStatusResp)args.Msg).DeviceIP[0] + "." +
                                                  ((DeviceStatusResp)args.Msg).DeviceIP[1] + "." +
                                                  ((DeviceStatusResp)args.Msg).DeviceIP[2] + "." +
                                                  ((DeviceStatusResp)args.Msg).DeviceIP[3] +
                      "\nEmergencia         : " + ((DeviceStatusResp)args.Msg).Emergency +
                      "\nDevice Id          : " + ((DeviceStatusResp)args.Msg).DeviceID.ToString() +
                      "\nReaderBlock        : " + ((DeviceStatusResp)args.Msg).ReaderBlock.ToString();

                    consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { BuffMsg });

                }
                //end block
                #endregion

                #region ACCESS_BACKUP --*
                if (((CommandReturn)args.Msg).CommandId == (int)CommandReturnType.ACCESS_BACKUP
                              && ((CommandReturn)args.Msg).ReturnCode == 1)
                {
                    if (args.Msg.GetType().ToString().CompareTo("DigiconFrameworkServer.Objects.MessageObjects.AsyncObjects.AccessBackupResp") == 0)
                    {

                        BuffMsg += "\nTotal Access = " + ((AccessBackupResp)args.Msg).TotEvent.ToString() + "\n\n" +
                          "--------------------------------------------------------------------\n";
                        for (int i = 0, n = 1; i < (int)((AccessBackupResp)args.Msg).TotEvent; i++, n++)
                        {
                            BuffMsg += "Evento nº " + n.ToString() +
                                "\nId Evento  = " + ((AccessBackupResp)args.Msg).Events[i].EventID.ToString() +
                                      "\nDate    = " + ((AccessBackupResp)args.Msg).Events[i].Timestamp.ToString() +
                                      "\nDireção = " + ((AccessBackupResp)args.Msg).Events[i].AcessDirection.ToString() +
                                      "\nVerão   = " + ((AccessBackupResp)args.Msg).Events[i].SummerTime.ToString() +
                                      "\ntipo    = " + ((AccessBackupResp)args.Msg).Events[i].IdentificationType.ToString() +
                                      "\nid      = " + BitConverter.ToString(((AccessBackupResp)args.Msg).Events[i].IdentificationData).Replace("-", "") +
                                      "\nGmt     = " + ((AccessBackupResp)args.Msg).Events[i].GMT1.ToString() +
                                      "\nReader  = " + ((AccessBackupResp)args.Msg).Events[i].ReaderID.ToString() + " \n " +
                                   "--------------------------------------------------------------------\n";
                        }
                    }

                }
                //End block
                #endregion

                #region ALIVE --*
                if (((CommandReturn)args.Msg).CommandId == (int)CommandReturnType.ALIVE
                                                && ((CommandReturn)args.Msg).ReturnCode == 1)
                {
                    BuffMsg += "\nDevice" + args.Msg.DeviceID.ToString() + " IS ALIVE";
                }

                #endregion

                #region CHECK_BIO --*
                if (args.Msg.GetType().ToString().CompareTo("DigiconFrameworkServer.Objects.MessageObjects.AsyncObjects.CheckBioResp") == 0)
                {
                    System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
                    BuffMsg += "\nTotal de Pessoas " + ((CheckBioResp)args.Msg).TotPerson.ToString() + "\n\n" +
                      "--------------------------------------------------------------------\n";

                    for (int i = 0; i < ((CheckBioResp)args.Msg).TotPerson; i++)
                    {
                        BuffMsg += "\n Person iD " + ascii.GetString(((CheckBioResp)args.Msg).Users[i].PersonID) +
                                "\nTotCard " + ((CheckBioResp)args.Msg).Users[i].TotCard.ToString();

                        for (int j = 0; j < ((CheckBioResp)args.Msg).Users[i].TotCard; j++)
                        {
                            BuffMsg += "\nCardID " + BitConverter.ToString((((CheckBioResp)args.Msg).Users[i].CardData[j].CardID)).Replace("-", "").ToString() +
                                    "\nTecCard " + ((CheckBioResp)args.Msg).Users[i].CardData[j].TecCard.ToString() + "\n";
                        }
                        BuffMsg += "--------------------------------------------------------------------\n";
                    }
                }
                #endregion

                #region DIGITAL_IN_STATUS --*
                if (((CommandReturn)args.Msg).CommandId == (int)CommandReturnType.DIGITAL_IN_STATUS
                                                        && ((CommandReturn)args.Msg).ReturnCode == 1)
                {
                    BuffMsg += "\nOutputs ";
                    for (int i = 0; i < ((DigitalInStatusResp)args.Msg).InStatus.Length; i++)
                    {
                        BuffMsg += ((DigitalInStatusResp)args.Msg).InStatus[i].ToString();
                    }
                }
                #endregion

                #region DIGITAL_OUT_STATUS --*
                if (((CommandReturn)args.Msg).CommandId == (int)CommandReturnType.DIGITAL_OUT_STATUS
                                                        && ((CommandReturn)args.Msg).ReturnCode == 1)
                {
                    BuffMsg += "\nOutputs ";
                    for (int i = 0; i < ((DigitalOutStatusResp)args.Msg).OutStatus.Length; i++)
                    {
                        BuffMsg += ((DigitalOutStatusResp)args.Msg).OutStatus[i].ToString();
                    }
                }
                #endregion

                #region AUTO_PROCESS --
                if (args.Msg.GetType().ToString().CompareTo("DigiconFrameworkServer.Objects.MessageObjects.AsyncObjects.AutoProcessResp") == 0)
                {
                    BuffMsg +=
                             "\nTotProcess   " + ((AutoProcessResp)args.Msg).TotProcess.ToString() +
                             "\n--------------------------------------------------------\n";

                    for (int i = 0; i < ((AutoProcessResp)args.Msg).TotProcess; i++)
                    {
                        BuffMsg += "\nProcessCod   " + BitConverter.ToString(((AutoProcessResp)args.Msg).Process[i].ProcessCode) +
                                   "\nDta da ultima Execução" + ((AutoProcessResp)args.Msg).Process[i].ExeDateTime.ToString() +
                                     "\n--------------------------------------------------------\n";
                    }
                }
                #endregion

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { BuffMsg });
            }
            #endregion

        }

        #endregion Access Message

        #region Clock Messages
        public void MenssageClockReceive(MessageReceivedArgsClock args)
        {
            String msg = "";
            //LogWriter.Instance.WriteLog(LogWriterType.DEBUG, "Delegate - Mensagem Recebida... clock " + args.Msg.MsgType);

            #region Keep Alive/ConfigDeviceResp
            if (args.Msg is DeviceConfigResp)
            {
                DFS.Ack(args.Msg.DeviceID);
                //  consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "\nDev "+ args.Msg.DeviceID + " Is Alive\n" });
            }

            if (args.Msg is KeepAliveResp)
            {
                DFS.Ack(args.Msg.DeviceID);
                //    consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "\nDev " + args.Msg.DeviceID + " Keep Alive\n" });
            }
            #endregion Keep Alive/ConfigBiometric

            #region  Manager Person Resp
            if (args.Msg is RequestPersonListResp)
            {
                DFS.Ack(args.Msg.DeviceID);

                msg += "\nRetorno do comando RequestPeopleList " +
               "\n\nDeviceID " + ((RequestPersonListResp)args.Msg).DeviceID.ToString() +
                "\nTotPerson " + ((RequestPersonListResp)args.Msg).Totperson.ToString() + "\n\n\n";

                for (int i = 0; i < ((RequestPersonListResp)args.Msg).Totperson; i++)
                {
                    msg += i.ToString() +
                        "\nName " + ((RequestPersonListResp)args.Msg).Person[i].Name +
                       "\nPersonID " + ((RequestPersonListResp)args.Msg).Person[i].PersonID +
                       "\nPis " + ((RequestPersonListResp)args.Msg).Person[i].Pis.ToString() +
                       "\nTypeOfValidation " + ((RequestPersonListResp)args.Msg).Person[i].TypeOfValidation.ToString() + "\n";


                    for (int x = 0; x < ((RequestPersonListResp)args.Msg).Person[i].CardClock.Length; x++)
                    {
                        msg += "\nCardId " + ((RequestPersonListResp)args.Msg).Person[i].CardClock[x].CardID.ToString() +
                            "\nCardTec " + (CardTechType)((RequestPersonListResp)args.Msg).Person[i].CardClock[x].CardTec + "\n";
                    }
                    msg += "------------------------------------------------\n\n";
                }

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
            }
            else if (args.Msg is InsertUpdatePersonResp)
            {
                DBApp db = new DBApp();
                string Status = "";

                if (((InsertUpdatePersonResp)args.Msg).ErrorCount == 0)
                {
                    Status = "Ok";
                }
                else
                {
                    for (int i = 0; i < ((InsertUpdatePersonResp)args.Msg).ErrorCount; i++)
                    {
                        Status += (ClockResultType)((InsertUpdatePersonResp)args.Msg).ErrorCodeList[i] + (i < ((InsertUpdatePersonResp)args.Msg).ErrorCount - 1 ? ", " : "");
                    }
                }

                int FuncionarioInd = db.getFuncionarioInd(((InsertUpdatePersonResp)args.Msg).PersonID.Trim());
                string FuncionarioNome = db.getFuncionarioNome(((InsertUpdatePersonResp)args.Msg).PersonID.Trim()).ToUpper();

                msg = formatMensagem(devices.getDeviceIP(((InsertUpdatePersonResp)args.Msg).DeviceID), "InsertUpdatePerson: " + FuncionarioNome, Status);
                int Terminal = db.getTerminalFromIP(devices.getDeviceIP(args.Msg.DeviceID));
                db.Atualizar_TerminaisFuncionarios(Terminal, FuncionarioInd, false);
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
                DFS.Ack(args.Msg.DeviceID);
            }
            else if (args.Msg is InsertUpdateReadersResp)
            {
                msg = "\nRetorno do comando InsertUpdateReaders " +
               "\nDeviceID " + ((InsertUpdateReadersResp)args.Msg).DeviceID.ToString() +
               "\nPersonID " + ((InsertUpdateReadersResp)args.Msg).PersonID +
               "\nErrorCount " + ((InsertUpdateReadersResp)args.Msg).ErrorCount.ToString() +
                "\nErrors : \n";

                for (int i = 0; i < ((InsertUpdateReadersResp)args.Msg).ErrorCount; i++)
                {
                    msg += (ClockResultType)((InsertUpdateReadersResp)args.Msg).ErrorCodeList[i] + "\n";
                }

                DFS.Ack(args.Msg.DeviceID);

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });

            }
            else if (args.Msg is DeletePersonResp)
            {
                DBApp db = new DBApp();
                string Status = Status = ((((DeletePersonResp)args.Msg).Status == 0)? "Ok": "Falha");

                int FuncionarioInd = db.getFuncionarioInd(((InsertUpdatePersonResp)args.Msg).PersonID.Trim());
                string FuncionarioNome = db.getFuncionarioNome(((InsertUpdatePersonResp)args.Msg).PersonID.Trim()).ToUpper();

                msg = formatMensagem(devices.getDeviceIP(((DeletePersonResp)args.Msg).DeviceID), "DeletePersonResp: " + FuncionarioNome, Status);
                int Terminal = db.getTerminalFromIP(devices.getDeviceIP(args.Msg.DeviceID));
                db.Atualizar_TerminaisFuncionarios(Terminal, FuncionarioInd, true);
                DFS.Ack(args.Msg.DeviceID);
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });

               // msg = "Retorno do comando DeletePersonResp " +
               //"\nDeviceID " + ((DeletePersonResp)args.Msg).DeviceID.ToString() +
               //"\nPersonID " + ((DeletePersonResp)args.Msg).PersonID +
               //"\nStatus " + (ClockResultType)((DeletePersonResp)args.Msg).Status
               //;
            }
            #endregion Manager People Resp

            #region UpdateClockResp
            else if (args.Msg is UpdateClockResp)
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] {
                    formatMensagem(devices.getDeviceIP(((UpdateClockResp)args.Msg).DeviceID), "UpdateClock", ((ClockResultType)((UpdateClockResp)args.Msg).Status).ToString())                 
                });

                DFS.Ack(args.Msg.DeviceID);

            }
            #endregion UpdateClockResp

            #region DeviceStatus
            else if (args.Msg is DeviceStatusClockResp)
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] {
                 "\nRetorno do comando DeviceStatus DeviceStatusClockResp " +
                "\nDeviceID " + ((DeviceStatusClockResp)args.Msg).DeviceID.ToString()  +
                "\nMrpAddressAbsolute " + ((DeviceStatusClockResp)args.Msg).MrpAddressAbsolute.ToString() +
                "\nMrpAddressCurrent " + ((DeviceStatusClockResp)args.Msg).MrpAddressCurrent.ToString() +
                "\nMrpMaxLenght " + ((DeviceStatusClockResp)args.Msg).MrpMaxLenght.ToString() +
                "\nMrpNumber " + ((DeviceStatusClockResp)args.Msg).MrpNumber.ToString() +
                "\nMrpRevision " + ((DeviceStatusClockResp)args.Msg).MrpRevision.ToString() +
                "\nMrpStatus " + ((DeviceStatusClockResp)args.Msg).MrpStatus +
                "\nMrpUsed " + ((DeviceStatusClockResp)args.Msg).MrpUsed.ToString() +
                "\nPrinterFwrVersion " + ((DeviceStatusClockResp)args.Msg).PrinterFwrVersion.ToString() +
                "\nRamUsed " + ((DeviceStatusClockResp)args.Msg).RamUsed.ToString() +
                "\nCountUsers " + ((DeviceStatusClockResp)args.Msg).CountUsers.ToString() +
                "\nCountCards " + ((DeviceStatusClockResp)args.Msg).CountCards.ToString() 
                
                 });



                DFS.Ack(args.Msg.DeviceID);

            }
            #endregion RequestDeviceStatusResp

            #region SetMifareMapResp
            else if (args.Msg is SetMifareMapResp)
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] {
                "\nRetorno do comando SetMifareMap " +
                "\nDeviceID " + ((SetMifareMapResp)args.Msg).DeviceID.ToString() 
                 });

                DFS.Ack(args.Msg.DeviceID);

            }
            #endregion SetMifareMapResp

            #region ManagerCompanyResp
            else if (args.Msg is ManagerCompanyResp)
            {
                msg = formatMensagem(
                    devices.getDeviceIP(((ManagerCompanyResp)args.Msg).DeviceID),
                    "InsertUpdateCompany",
                    ((ClockResultType)((ManagerCompanyResp)args.Msg).Status).ToString()
                    );

                DFS.Ack(args.Msg.DeviceID);

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });

            }
            #endregion ManagerCompanyResp

            #region DeviceConnectedMessageClock
            else if (args.Msg is DeviceConnectedMessageClock)
            {

                Session session = DFS.Session(((DeviceConnectedMessageClock)args.Msg).DeviceID);

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { formatMensagem(session.IpDevice, ((DeviceConnectedMessageClock)args.Msg).DeviceType.ToString() + ": Versão de Firmware: " + ((DeviceConnectedMessageClock)args.Msg).FirmwareVersion.ToString(), "ONLINE") });


            }
            #endregion DeviceConnectedMessageClock

            #region Events
            else if (args.Msg.MsgType == MessageType.MSG_EVENT)
            {
                msg += "\nEvento....= " + ((EventClockType)((EventMessageClock)args.Msg).EventID).ToString() +
                    "\nEventDate....= " + ((EventMessageClock)args.Msg).TimeEvent.ToString() +
                "\nDeviceID.......= " + ((EventMessageClock)args.Msg).DeviceID.ToString();

                DFS.Ack(args.Msg.DeviceID);

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });

            }
            #endregion Events

            #region  RequestLogResp
            else if (args.Msg is RequestLogResp)
            {

                DBApp db = new DBApp();
                int Terminal = db.getTerminalFromIP(devices.getDeviceIP(args.Msg.DeviceID));

                prepareTerminal(Terminal);
                Marcacoes marcacoes = new Marcacoes(TerminalDados);

                //msg = "\nEvento....= RequestLogResp " +
                //    "\nDeviceID.......= " + ((RequestLogResp)args.Msg).DeviceID.ToString() +
                //   "\nTot Event.....= " + ((RequestLogResp)args.Msg).TotEvent.ToString() + "\n";

                DFS.Ack(args.Msg.DeviceID);

                //log.AddLog(String.Format(Consts.TOTAL_MARCACOES, ((RequestLogResp)args.Msg).TotEvent));

                for (int i = 0, n = 1; i < (int)((RequestLogResp)args.Msg).TotEvent; i++, n++)
                {
                    //msg += "\nEvento nº " + n.ToString() +
                    //    "\nId Evento  = " + (EventClockType)((RequestLogResp)args.Msg).Events[i].EventID +
                    //          "\nDate    = " + ((RequestLogResp)args.Msg).Events[i].TimeEvent.ToString() + "\n";

                    if ((EventClockType)((RequestLogResp)args.Msg).Events[i].EventID == EventClockType.REGISTERED_POINT)
                    {
                        DateTime DataHora = ((RequestLogResp)args.Msg).Events[i].TimeEvent;
                        string Pis = ((EventRegisteredPoint)((RequestLogResp)args.Msg).Events[i]).Pis.ToString();
                        int Nsr = ((EventRegisteredPoint)((RequestLogResp)args.Msg).Events[i]).Nsr;

                        if (marcacoes.Add(Pis, DataHora, Nsr))
                        {
                            msg = String.Format("{0} {1} {2}", Pis, DataHora, Nsr);
                            consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
                        }
                        //msg += "\nNSR " + ((EventRegisteredPoint)((RequestLogResp)args.Msg).Events[i]).Nsr.ToString() +
                        //  "\nPis " + ((EventRegisteredPoint)((RequestLogResp)args.Msg).Events[i]).Pis.ToString() +
                        //  "\nTec reader " + (CardTechType)((EventRegisteredPoint)((RequestLogResp)args.Msg).Events[i]).Technology + "\n";
                    }

                    //msg += "\n";
                }

                if (marcacoes.Count == 0)
                {
                    msg = "\n" + Consts.SEM_MARCACOES_NOVAS;
                    consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
                }
                else
                {
                    FinalizarImportacaoMarcacoes(marcacoes, ProcessarMarcacoesAoImportar.ProcessarNao /* não deixar processar na classe raiz, pois as mensagens de andamento não são compativeis com threads */);

                    msg = String.Format("\n" + Consts.ARQUIVO_GERADO, marcacoes.Arquivo);
                    consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
                    ProcessarMarcacoes(marcacoes);
                }

            //consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
            }
            #endregion  RequestLogResp

            #region RequestBackupLogResp
            else if (args.Msg is RequestBackupLogResp)
            {
                //msg = "\nEvento....= RequestBackupLogResp " +
                //    "\nDeviceID.......= " + ((RequestBackupLogResp)args.Msg).DeviceID.ToString() +
                //   "\nTot Event.....= " + ((RequestBackupLogResp)args.Msg).TotEvent.ToString() + "\n";

                DBApp db = new DBApp();
                int Terminal = db.getTerminalFromIP(devices.getDeviceIP(args.Msg.DeviceID));

                prepareTerminal(Terminal);
                Marcacoes marcacoes = new Marcacoes(TerminalDados);

                DFS.Ack(args.Msg.DeviceID);

                for (int i = 0, n = 1; i < (int)((RequestBackupLogResp)args.Msg).TotEvent; i++, n++)
                {
                    //msg += "\nEvento nº " + n.ToString() +
                    //    "\nId Evento  = " + (EventClockType)((RequestBackupLogResp)args.Msg).Events[i].EventID +
                    //          "\nDate    = " + ((RequestBackupLogResp)args.Msg).Events[i].TimeEvent.ToString() + "\n";

                    if ((EventClockType)((RequestBackupLogResp)args.Msg).Events[i].EventID == EventClockType.REGISTERED_POINT)
                    {
                        DateTime DataHora = ((RequestBackupLogResp)args.Msg).Events[i].TimeEvent;
                        string Pis = ((EventRegisteredPoint)((RequestBackupLogResp)args.Msg).Events[i]).Pis.ToString();
                        int Nsr = ((EventRegisteredPoint)((RequestBackupLogResp)args.Msg).Events[i]).Nsr;

                        if (marcacoes.Add(Pis, DataHora, Nsr))
                        {
                            msg = String.Format("\nPIS: {0} {1} {2}", Pis, DataHora, Nsr);
                            consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
                        }

                        //msg += "\nNSR " + ((EventRegisteredPoint)((RequestBackupLogResp)args.Msg).Events[i]).Nsr.ToString() +
                        //  "\nPis " + ((EventRegisteredPoint)((RequestBackupLogResp)args.Msg).Events[i]).Pis.ToString() +
                        //  "\nTec reader " + (CardTechType)((EventRegisteredPoint)((RequestBackupLogResp)args.Msg).Events[i]).Technology + "\n";
                    }

//                    msg += "\n";
                }

                FinalizarImportacaoMarcacoes(marcacoes, ProcessarMarcacoesAoImportar.ProcessarNao);
                msg = String.Format("\n" + Consts.ARQUIVO_GERADO, marcacoes.Arquivo);
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
                //                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
            }

        }
            #endregion RequestBackupLogResp

        #endregion
    }
}
