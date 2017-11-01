/* * * Digicon  - VCA Department ***
 * This is a simple sample, you can use with reference.
 * Version DFS: 10.3.1.0
 * Tank you for using Digicon solutions.
 * Enjoy.
 * 
 * 20/04/2013 10:00:07
 */

using System;
using System.Text;
using System.Windows.Forms;
using DigiconFrameworkServer;
using DigiconFrameworkServer.Control;
using DigiconFrameworkServer.Logger;
using DigiconFrameworkServer.Objects.CommandObjects;
using DigiconFrameworkServer.Objects.MessageObjects;
using DigiconFrameworkServer.Objects.MessageObjects.AsyncObjects;
using DigiconFrameworkServer.Objects.MessageObjects.CommandReturnObjects;
using DigiconFrameworkServer.Objects.MessageObjects.EventObjects;
using DigiconFrameworkServer.Objects.MessageObjects.ResponseObjects;
using DigiconFrameworkServer.Objects.MessageObjects.RequestObjects;
using DigiconFrameworkServer.Objects.ClockObjects;
using DigiconFrameworkServer.Info;

namespace DFSSample
{
    public partial class Form1 : Form
    {

        #region bio
        //Biometria
        byte[] biometric1 = ConvertHexString2Value("722900425bff80809c6d8a8e828f8d934caea28f6892537ead65575777a97fa1969fa481a2a2a399aa9932a2a9abb17db192ad73ada1b88db9863696b4ae688eb86cc49453735f7269706c5d6e82955597627c6d84647c7f8c77ed9afeedfd7f984225c7cf5469ff");
        byte[] biometric2 = ConvertHexString2Value("8229003f55ff80807f749955847668a96c935e7e30663c65496650657a6caf4b7498769085b3519ba09f6188ad98b05db371b37db887ba6cba8f667ac298317c537cc9743675c5893c793f7130657278c0534751585e6065804beadcdff8e7ff91489a484d5fedff");
        #endregion

        #region Auxiliary methods

        private int valor()
        {
            return cbResp.SelectedIndex;
        }

        /// <summary>
        /// Retrieves current IP
        /// </summary>
        /// <returns>IP of the local machine</returns>
        protected string GetLocalIPAddress()
        {
            string IPAddress = string.Empty;
            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            foreach (System.Net.IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    IPAddress = ip.ToString();
                }
            }
            return IPAddress;
        }

        public static byte[] ConvertHexString2Value(string hex)
        {
            // If the hex string size is not multiple of 2, it's invalid!
            if (hex.Length % 2 != 0)
            {
                Console.WriteLine("Invalid hex string");
                return null;
            }

            // Creates the byte[] that will receive the result.
            byte[] result = new byte[hex.Length / 2];

            // For the loop...
            int j = 0;

            // Converts each 2 bytes of the input on your equivalent hex value and stores it in the byte[]
            for (int i = 0; i <= hex.Length - 2; i += 2, j++)
            {
                try
                {
                    result[j] = (byte)Convert.ToChar(Int32.Parse(hex.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return result;
        }

        #endregion Auxiliary methods

        #region ***Global
        /// <summary>
        /// Delegate atualiza console
        /// </summary>
        /// <param name="strMensagem"></param> 
        private delegate void AtualizaStatusCallback(string strMensagem);
        public delegate void updateTxt(string message);
        public delegate void updateResp(string message);
        public delegate int receiveCb();

        DigiconServer DFS = new DigiconServer();
        Boolean start = false;
        #endregion

        #region Initialize

        public Form1()
        {
            InitializeComponent();


            for (int i = 0; i < 27; i++)
            {
                cbResp.Items.Add("Resp " + (i + 1));
            }
            cbResp.SelectedIndex = 0;


            label4.Text = "dfs-" + typeof(DigiconServer).Assembly.GetName().Version.ToString() + ".dll";

            ipDevice.Text = GetLocalIPAddress();
            port.Text = "3000";

            //Delegate to messages receiveds access
            MessageManager.MessageReceivedChanged += new MessageReceivedHandler(MenssageAccessReceive);

            //Delegate to messages receiveds clock
            MessageManagerClock.MessageReceivedChangedClock += new MessageReceivedHandlerClock(MenssageClockReceive);


            // Keeps control of the console tab
            LogWriter.LogHandler += new WriteLogHandler(Form1_StatusChanged);

        }

        #endregion Initialize

        #region Atualiza Log
        private void AtualizaStatus(string strMensagem)
        {
            consoleLog.AppendText(strMensagem + "\r\n");
            consoleLog.ScrollToCaret();
        }

        public void Form1_StatusChanged(WriteLogArgs args)
    {
            this.Invoke(new AtualizaStatusCallback(this.AtualizaStatus), new object[] { args.ToString() });
        }
        #endregion

        #region Console resposta
        private void AtualizaResp(string strMensagem)
        {
            consoleResp.AppendText(
                "---------------------------------------------------------------------------------------------------------------------------------------------------\n" +
                strMensagem + "\n " +
                "--------------------------------------------------------------------------------------------------------------------------------------------------\n");
            consoleResp.ScrollToCaret();
        }
        #endregion

        #region Atualiza Id
        /// <summary>
        /// Atualiza Id
        /// </summary>
        /// <param name="id"></param>
        public void atualizaId(string id)
        {
            if (id == string.Empty) lstB1.Items.Clear();
            else if (!lstB1.Items.Contains(id)) lstB1.Items.Add(id);
        }
        #endregion

        #region ***** Tratamento de Mensagens *****************
        /// <summary>
        /// Tratamento de Mensagens
        /// </summary>
        /// <param name="args"></param>

        #region Access Messages
        public void MenssageAccessReceive(MessageReceivedArgs args)
        {
            LogWriter.Instance.WriteLog(LogWriterType.DEBUG, "Delegate - Mensagem Recebida...");

            if ((args.Msg.MsgType != MessageType.MSG_ACCESS_REQUEST_CARD) &&
                 (args.Msg.MsgType != MessageType.MSG_ACCESS_REQUEST_PERSON) &&
                 (args.Msg.MsgType != MessageType.MSG_TEMPLATE_REQUEST) &&
                 (args.Msg.MsgType != MessageType.MSG_DATA_REQUEST_CARD) &&
                 (args.Msg.MsgType != MessageType.MSG_CONNECTION_REQUEST) &&
                 (args.Msg.MsgType != MessageType.MSG_DATA_REQUEST_PERSON) &&
                 (args.Msg.MsgType != MessageType.MSG_SYNC_RETURN) &&
                 !(args.Msg is EventConnChangeStatus))
            {
                LogWriter.Instance.WriteLog(LogWriterType.DEBUG, "Delegate - Enviando ACK" + args.Msg.MsgType.ToString());
                DFS.Ack(args.Msg.DeviceID);
            }

            lstB1.BeginInvoke(new updateTxt(atualizaId), new object[] { args.Msg.DeviceID.ToString() });

            #region MSG_CONNECTION_REQUEST
            if (args.Msg.MsgType == MessageType.MSG_CONNECTION_REQUEST)
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Requisição de Conexão" +
                                                                                "\nDeviceID : " + ((DeviceConnectedMessage)args.Msg).DeviceID.ToString() +
                                                                                "\nDevice Type : " + ((DeviceConnectedMessage)args.Msg).DeviceTyp.ToString() +
                                                                                 "\nFirmwareVersion : " + ((DeviceConnectedMessage)args.Msg).FirmwareVersion.ToString()});
            }
            #endregion MSG_CONNECTION_REQUEST

            #region REQUEST DATA

            #region MSG_DATA_REQUEST_CARD
            if (args.Msg.MsgType == MessageType.MSG_DATA_REQUEST_CARD)
            {

                Byte[] cardId = (Byte[])((DataRequestCard)args.Msg).CardId;

                string hex = BitConverter.ToString(cardId).Replace("-", "");
                long num = Int64.Parse(hex, System.Globalization.NumberStyles.HexNumber);
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Requisição de Cartão:.." +
                                                                "\nCardID.............................." + num.ToString() +
                                                                "\nDeviceID........................." + ((DataRequestCard)args.Msg).DeviceID.ToString() });

                ResponseDataCard rav = new ResponseDataCard();
                TemplateListPersonCard[] tmpLstPersCard = new TemplateListPersonCard[1];

                //Cartão 1
                tmpLstPersCard[0] = new TemplateListPersonCard();
                tmpLstPersCard[0].CardID = cardId;
                tmpLstPersCard[0].DeviceID = ((DataRequestCard)args.Msg).DeviceID;
                tmpLstPersCard[0].CardTec = 3;

                rav.ReturnType = 1;
                rav.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                rav.Card = tmpLstPersCard;
                rav.AppConnectionStatus = 2;
                rav.DeviceID = ((DataRequestCard)args.Msg).DeviceID;

                DFS.Execute(rav);

            }
            #endregion MSG_DATA_REQUEST_CARD

            #region MSG_DATA_REQUEST_PERSON
            if (args.Msg.MsgType == MessageType.MSG_DATA_REQUEST_PERSON)
            {
                Byte[] bt = (Byte[])((DataRequestPerson)args.Msg).PersonId;
                System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "MSG_DATA_REQUEST_PERSON: " +
                                                                  "\nPersonID.........................." +  ascii.GetString(bt) +
                                                                  "\nDeviceID.........................." + ((DataRequestPerson)args.Msg).DeviceID });

                ResponseDataCard rav = new ResponseDataCard();
                TemplateListPersonCard[] tmpLstPersCard = new TemplateListPersonCard[1];

                //Cartão 1
                tmpLstPersCard[0] = new TemplateListPersonCard();
                tmpLstPersCard[0].CardID = new Byte[] { 0x00, 0x6A, 0xA9, 0x87, 0x5E };
                tmpLstPersCard[0].DeviceID = ((DataRequestPerson)args.Msg).DeviceID;
                tmpLstPersCard[0].CardTec = 3;

                rav.Card = tmpLstPersCard;
                rav.AppConnectionStatus = 2;
                rav.DeviceID = ((DataRequestPerson)args.Msg).DeviceID;

                DFS.Execute(rav);

            }
            #endregion MSG_DATA_REQUEST_PERSON

            #region MSG_TEMPLATE_REQUEST
            if (args.Msg.MsgType == MessageType.MSG_TEMPLATE_REQUEST)
            {

                Byte[] bt = (Byte[])((DataRequestTemplate)args.Msg).PersonId;
                System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "MSG_TEMPLATE_REQUEST: " +
                                                                  "\nPersonID.........................." +  ascii.GetString(bt) +
                                                                  "\nDeviceID.........................." + ((DataRequestTemplate)args.Msg).DeviceID.ToString() });

                ResponseDataTemplate rav = new ResponseDataTemplate();
                TemplateListPersonTemplate[] tmpLstPerTemp = new TemplateListPersonTemplate[2];
                TemplateListPersonCard[] tmpLstPersCard = new TemplateListPersonCard[1];
                TemplateListPerson tmpLstPer = new TemplateListPerson();

                //biometria 1
                tmpLstPerTemp[0] = new TemplateListPersonTemplate();
                tmpLstPerTemp[0].DeviceID = ((DataRequestTemplate)args.Msg).DeviceID;
                tmpLstPerTemp[0].TamplateFactory = 1;
                tmpLstPerTemp[0].Template = new byte[500];
                //biometria 2
                tmpLstPerTemp[1] = new TemplateListPersonTemplate();
                tmpLstPerTemp[1].DeviceID = ((DataRequestTemplate)args.Msg).DeviceID;
                tmpLstPerTemp[1].TamplateFactory = 1;
                tmpLstPerTemp[1].Template = new byte[500]; ;

                //Cartão 1
                tmpLstPersCard[0] = new TemplateListPersonCard();
                tmpLstPersCard[0].CardID = new Byte[] { 0x00, 0x6A, 0xA9, 0x87, 0x5E };
                tmpLstPersCard[0].DeviceID = ((DataRequestTemplate)args.Msg).DeviceID;
                tmpLstPersCard[0].CardTec = 3;


                tmpLstPer.BioConfLevel = 75;
                tmpLstPer.DeviceID = ((DataRequestTemplate)args.Msg).DeviceID;
                tmpLstPer.Card = tmpLstPersCard;
                tmpLstPer.Template = tmpLstPerTemp;
                tmpLstPer.PersonID = ascii.GetString(bt);
                rav.Person = tmpLstPer;
                rav.AppConnectionStatus = 2;
                rav.DeviceID = ((DataRequestTemplate)args.Msg).DeviceID;

                DFS.Execute(rav);

            }
            #endregion MSG_TEMPLATE_REQUEST


            #endregion REQUEST DATA

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

            #region Requisição de Pessoa --
            if (args.Msg.MsgType == MessageType.MSG_ACCESS_REQUEST_PERSON)
            {
                int resp;

                Byte[] bt = (Byte[])((AccessRequestPerson)args.Msg).PersonId;
                System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Requisição de Pessoa: \n" + 
                                                                  "\nPersonID.........................." + ascii.GetString(bt) +
                                                                  "\nDeviceID.........................." + ((AccessRequestPerson)args.Msg).DeviceID.ToString()+
                                                                  "\nReaderID.........................." + ((AccessRequestPerson)args.Msg).ReaderId.ToString()+
                                                                  "\nData.................................." + ((AccessRequestPerson)args.Msg).TimestampRequest.ToString()});

                resp = (int)this.Invoke(new receiveCb(valor)) + 1;

                #region Allowed (1)
                if (resp == 1)
                {
                    //Inicio do Cabeçalho da Resposta
                    ResponseAccessValid rav = new ResponseAccessValid();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;
                    rav.TimeScheduleCredit = 1;
                    rav.BiometricLevel = 0;
                    rav.Frisk = 0;
                    rav.Password = "123456";
                    rav.DataUpdateFlag = 0;
                    rav.CreditControlType = 0;
                    rav.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    rav.RspEventType = ResponseEventType.RSP_EVENT_ACCESS_VALID;
                    //Fim do Cabeçalho da Resposta


                    //Inicio PayLoad da Resposta Pessoa
                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
                    rts.PersonLogicalId = new Byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    //Fim PayLoad da Resposta Pessoa

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Authorized by Authorizer (45)
                if (resp == 2)
                {
                    ResponseAccessValidAuthorizer rav = new ResponseAccessValidAuthorizer();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;
                    rav.TimeScheduleCredit = 1;
                    rav.BiometricLevel = 0;
                    rav.Frisk = 0;
                    rav.Password = "123456";
                    rav.DataUpdateFlag = 0;
                    rav.CreditControlType = 0;
                    rav.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));



                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Authorized inconditionally by Master Card (11)
                if (resp == 3)
                {
                    ResponseAccessValidMasterCard rav = new ResponseAccessValidMasterCard();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;
                    rav.TimeScheduleCredit = 1;
                    rav.BiometricLevel = 0;
                    rav.Frisk = 0;
                    rav.Password = "123456";
                    rav.DataUpdateFlag = 0;
                    rav.CreditControlType = 0;
                    rav.UserMessage = Encoding.ASCII.GetBytes("  Welcome".PadRight(32));

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied awaiting Authorized (47)
                if (resp == 4)
                {
                    ResponseAccessDeniedAwatingAuthorizer rav = new ResponseAccessDeniedAwatingAuthorizer();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;
                    rav.TimeScheduleCredit = 1;
                    rav.BiometricLevel = 0;
                    rav.Frisk = 0;
                    rav.Password = "123456";
                    rav.DataUpdateFlag = 0;
                    rav.CreditControlType = 0;
                    rav.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Permission Hourly Range (13)
                if (resp == 5)
                {
                    ResponseAccessDeniedPermissionHourlyRange rav = new ResponseAccessDeniedPermissionHourlyRange();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Person Type (9)
                if (resp == 6)
                {
                    ResponseAccessDeniedPersonType rav = new ResponseAccessDeniedPersonType();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Permission (2)
                if (resp == 7)
                {
                    ResponseAccessDeniedPermission rav = new ResponseAccessDeniedPermission();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;


                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Situation (3)
                if (resp == 8)
                {
                    ResponseAccessDeniedSituation rav = new ResponseAccessDeniedSituation();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;


                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Access Credit (7)
                if (resp == 9)
                {
                    ResponseAccessDeniedCredit rav = new ResponseAccessDeniedCredit();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonLogicalId = new byte[5];
                    rts.PersonPhisycalId = new byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Permission Hourly Range (8)
                if (resp == 10)
                {
                    ResponseAccessDeniedPersonHourlyRange rav = new ResponseAccessDeniedPersonHourlyRange();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));


                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Person Type on Exit (22)
                if (resp == 11)
                {
                    ResponseAccessDeniedPersonTypeExit rav = new ResponseAccessDeniedPersonTypeExit();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));


                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Lunch Interval (24)
                if (resp == 12)
                {
                    ResponseAccessDeniedLunchInterval rav = new ResponseAccessDeniedLunchInterval();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Fault (25)
                if (resp == 13)
                {
                    ResponseAccessDeniedFault rav = new ResponseAccessDeniedFault();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;


                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by InterJourney (32)
                if (resp == 14)
                {
                    ResponseAccessDeniedInterJourney rav = new ResponseAccessDeniedInterJourney();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;


                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Access Credit Range (49)
                if (resp == 15)
                {
                    ResponseAccessDeniedCreditRange rav = new ResponseAccessDeniedCreditRange();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Validity (4)
                if (resp == 16)
                {
                    ResponseAccessDeniedValidity rav = new ResponseAccessDeniedValidity();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;
                    rav.TagLastValidDate = Convert.ToDateTime("31/05/2012 00:00:00");

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Level Control (5)
                if (resp == 17)
                {
                    ResponseAccessDeniedLevelControl rav = new ResponseAccessDeniedLevelControl();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by AntiPassback (48)
                if (resp == 18)
                {
                    ResponseAccessDeniedAntiPassback rav = new ResponseAccessDeniedAntiPassback();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Removal (6)
                if (resp == 19)
                {
                    ResponseAccessDeniedRemoval rav = new ResponseAccessDeniedRemoval();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;
                    rav.RemovalBegin = DateTime.Now;
                    rav.RemovalEnd = Convert.ToDateTime("07:00:00 10/07/2012");



                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Card not registered (33)
                if (resp == 20)
                {
                    ResponseAccessDeniedCardNotRegistered rav = new ResponseAccessDeniedCardNotRegistered();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Min Time (41)
                if (resp == 21)
                {
                    ResponseAccessDeniedMinTime rav = new ResponseAccessDeniedMinTime();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Accompany (36)
                if (resp == 22)
                {
                    ResponseAccessDeniedAccompany rav = new ResponseAccessDeniedAccompany();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Invalid Authorizer (46)
                if (resp == 23)
                {
                    ResponseAccessDeniedinvalidAuthorizer rav = new ResponseAccessDeniedinvalidAuthorizer();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5]; rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Subsidiary (54)
                if (resp == 24)
                {
                    ResponseAccessDeniedSubsidiary rav = new ResponseAccessDeniedSubsidiary();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Card Use ID (card blocked?) (55)
                if (resp == 25)
                {
                    ResponseAccessDeniedCardUseID rav = new ResponseAccessDeniedCardUseID();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Card Copy (56)
                if (resp == 26)
                {
                    ResponseAccessDeniedCardCopy rav = new ResponseAccessDeniedCardCopy();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    ResponsePersonStructure rts = new ResponsePersonStructure();
                    rts.EventStructure = rav;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.PersonPhisycalId = new Byte[5];
                    rts.PersonLogicalId = new Byte[5];
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Message
                if (resp == 27)
                {
                    ResponseScreenMessage rts = new ResponseScreenMessage();
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.ResponseType = 3;
                    rts.MessageId = 1;
                    rts.ScreenMessage = Encoding.ASCII.GetBytes("Mensagem ".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion Denied whit Message
            }
            else
            {

            }

            #endregion

            #region Requisicao de Cartao --
            if (args.Msg.MsgType == MessageType.MSG_ACCESS_REQUEST_CARD)
            {
                int resp;
                Byte[] cardId = (Byte[])((AccessRequestCard)args.Msg).CardId;

                string hex = BitConverter.ToString(cardId).Replace("-", "");
                long num = Int64.Parse(hex, System.Globalization.NumberStyles.HexNumber);
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Requisição de Cartão:.." +
                                                                "\nCardID.............................." + num.ToString() +
                                                                "\nDeviceID........................." + ((AccessRequestCard)args.Msg).DeviceID.ToString() +
                                                                "\nReaderID........................." + ((AccessRequestCard)args.Msg).ReaderId.ToString() +
                                                                "\nData................................." + ((AccessRequestCard)args.Msg).TimestampRequest.ToString()  });
                resp = (int)this.Invoke(new receiveCb(valor)) + 1;

                #region Allowed (1)
                if (resp == 1)
                {
                    //Inicio do Cabeçalho da Resposta
                    ResponseAccessValid rav = new ResponseAccessValid();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;
                    rav.TimeScheduleCredit = 1;
                    rav.BiometricLevel = 0;
                    rav.Frisk = 0;
                    rav.Password = "123456";
                    rav.DataUpdateFlag = 0;
                    rav.CreditControlType = 0;
                    rav.UserMessage = Encoding.ASCII.GetBytes("Seja bem vindo!".PadRight(32));


                    //Fim do Cabeçalho da Resposta

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();

                    ////Pendências
                    ////1 Faixa horária
                    PendingStructure pds = new PendingStructure();
                    pds.PendingData = new PendingData[1];
                    pds.PendingData[0] = new PendingData();
                    pds.PendingData[0].DeviceID = args.Msg.DeviceID;
                    pds.PendingData[0].FieldID = 35; //campo 

                    byte[] ranges = new byte[21];
                    ranges[0] = (byte)(3 & 0X00FF);
                    ranges[1] = (byte)(((3 & 0X0F00) >> 4) | (4 & 0X0F00) >> 8);
                    ranges[2] = (byte)(4 & 0X0FF);

                    for (int i = 3; i < ranges.Length; i++) ranges[i] = 0;

                    pds.PendingData[0].Data = ranges;
                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 8;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Authorized by Authorizer (45)
                if (resp == 2)
                {
                    ResponseAccessValidAuthorizer rav = new ResponseAccessValidAuthorizer();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;
                    rav.TimeScheduleCredit = 1;
                    rav.BiometricLevel = 0;
                    rav.Frisk = 0;
                    rav.Password = "123456";
                    rav.DataUpdateFlag = 0;
                    rav.CreditControlType = 0;
                    rav.UserMessage = Encoding.ASCII.GetBytes("Seja bem vindo!".PadRight(32));


                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    //   rts.PendingStructure = pds;
                    //   rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    //sem pendências

                    rts.PendingStructure = new PendingStructure();
                    rts.PendingStructSize = 0; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Authorized inconditionally by Master Card (11)
                if (resp == 3)
                {
                    ResponseAccessValidMasterCard rav = new ResponseAccessValidMasterCard();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;
                    rav.TimeScheduleCredit = 1;
                    rav.BiometricLevel = 0;
                    rav.Frisk = 0;
                    rav.Password = "123456";
                    rav.DataUpdateFlag = 0;
                    rav.CreditControlType = 0;
                    rav.UserMessage = Encoding.ASCII.GetBytes("Seja bem vindo!".PadRight(32));


                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied awaiting Authorized (47)
                if (resp == 4)
                {
                    ResponseAccessDeniedAwatingAuthorizer rav = new ResponseAccessDeniedAwatingAuthorizer();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;
                    rav.TimeScheduleCredit = 1;
                    rav.BiometricLevel = 0;
                    rav.Frisk = 0;
                    rav.Password = "123456";
                    rav.DataUpdateFlag = 0;
                    rav.CreditControlType = 0;
                    rav.UserMessage = Encoding.ASCII.GetBytes("Seja bem vindo!".PadRight(32));


                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Permission Hourly Range (13)
                if (resp == 5)
                {
                    ResponseAccessDeniedPermissionHourlyRange rav = new ResponseAccessDeniedPermissionHourlyRange();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Person Type (9)
                if (resp == 6)
                {
                    ResponseAccessDeniedPersonType rav = new ResponseAccessDeniedPersonType();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Permission (2)
                if (resp == 7)
                {
                    ResponseAccessDeniedPermission rav = new ResponseAccessDeniedPermission();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Situation (3)
                if (resp == 8)
                {
                    ResponseAccessDeniedSituation rav = new ResponseAccessDeniedSituation();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Access Credit (7)
                if (resp == 9)
                {
                    ResponseAccessDeniedCredit rav = new ResponseAccessDeniedCredit();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Permission Hourly Range (8)
                if (resp == 10)
                {
                    ResponseAccessDeniedPersonHourlyRange rav = new ResponseAccessDeniedPersonHourlyRange();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Person Type on Exit (22)
                if (resp == 11)
                {
                    ResponseAccessDeniedPersonTypeExit rav = new ResponseAccessDeniedPersonTypeExit();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Lunch Interval (24)
                if (resp == 12)
                {
                    ResponseAccessDeniedLunchInterval rav = new ResponseAccessDeniedLunchInterval();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Fault (25)
                if (resp == 13)
                {
                    ResponseAccessDeniedFault rav = new ResponseAccessDeniedFault();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by InterJourney (32)
                if (resp == 14)
                {
                    ResponseAccessDeniedInterJourney rav = new ResponseAccessDeniedInterJourney();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Access Credit Range (49)
                if (resp == 15)
                {
                    ResponseAccessDeniedCreditRange rav = new ResponseAccessDeniedCreditRange();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Validity (4)
                if (resp == 16)
                {
                    ResponseAccessDeniedValidity rav = new ResponseAccessDeniedValidity();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Level Control (5)
                if (resp == 17)
                {
                    ResponseAccessDeniedLevelControl rav = new ResponseAccessDeniedLevelControl();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by AntiPassback (48)
                if (resp == 18)
                {
                    ResponseAccessDeniedAntiPassback rav = new ResponseAccessDeniedAntiPassback();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Removal (6)
                if (resp == 19)
                {
                    ResponseAccessDeniedRemoval rav = new ResponseAccessDeniedRemoval();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Card not registered (33)
                if (resp == 20)
                {
                    ResponseAccessDeniedCardNotRegistered rav = new ResponseAccessDeniedCardNotRegistered();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Min Time (41)
                if (resp == 21)
                {
                    ResponseAccessDeniedMinTime rav = new ResponseAccessDeniedMinTime();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Accompany (36)
                if (resp == 22)
                {
                    ResponseAccessDeniedAccompany rav = new ResponseAccessDeniedAccompany();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Invalid Authorizer (46)
                if (resp == 23)
                {
                    ResponseAccessDeniedinvalidAuthorizer rav = new ResponseAccessDeniedinvalidAuthorizer();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Subsidiary (54)
                if (resp == 24)
                {
                    ResponseAccessDeniedSubsidiary rav = new ResponseAccessDeniedSubsidiary();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Card Use ID (card blocked?) (55)
                if (resp == 25)
                {
                    ResponseAccessDeniedCardUseID rav = new ResponseAccessDeniedCardUseID();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Card Copy (56)
                if (resp == 26)
                {
                    ResponseAccessDeniedCardCopy rav = new ResponseAccessDeniedCardCopy();
                    rav.DeviceID = args.Msg.DeviceID;
                    rav.ActualLevel = 0;

                    /***Inicio  PayLoad daResposta de Cartão***/
                    //resposta
                    ResponseTagStructure rts = new ResponseTagStructure();
                    //Pendências
                    PendingStructure pds = new PendingStructure();

                    pds.DeviceID = args.Msg.DeviceID;
                    pds.LastUpdate = DateTime.Now;

                    //resposta
                    rts.PendingStructure = pds;
                    rts.PendingStructSize = pds.Bytes.Length; //tamanho

                    rts.PersonId = Encoding.ASCII.GetBytes("0000000123456789".PadLeft(23));
                    rts.PersonlogicalId = ((AccessRequestCard)args.Msg).CardId;
                    rts.CardTech = 3;
                    rts.CardType = 1;
                    rts.EventStructure = rav;
                    rts.AppConnectionStatus = 2;
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.UserMessage = Encoding.ASCII.GetBytes(" Welcome".PadRight(32));
                    /***Fim PayLoad Resposta de Cartão***/

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion

                #region Denied by Message
                if (resp == 27)
                {
                    ResponseScreenMessage rts = new ResponseScreenMessage();
                    rts.DeviceID = args.Msg.DeviceID;
                    rts.AppConnectionStatus = 2;
                    rts.ResponseType = 3;
                    rts.MessageId = 1;
                    rts.ScreenMessage = Encoding.ASCII.GetBytes("Mensagem ".PadRight(32));

                    this.DFS.Execute(rts);

                    return;
                }
                #endregion Denied whit Message
            }
            else
            {

            }
            #endregion
        }

        #endregion Access Message

        #region Clock Messages
        public void MenssageClockReceive(MessageReceivedArgsClock args)
        {
            String msg = "";
            LogWriter.Instance.WriteLog(LogWriterType.DEBUG, "Delegate - Mensagem Recebida... clock " + args.Msg.MsgType);

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
                msg = "Retorno do comando InsertUpdatePerson " +
               "\nDeviceID " + ((InsertUpdatePersonResp)args.Msg).DeviceID.ToString() +
                "\nPersonID " + ((InsertUpdatePersonResp)args.Msg).PersonID +
                "\nErrorCount " + ((InsertUpdatePersonResp)args.Msg).ErrorCount.ToString() +
                "\nErrors : \n";

                for (int i = 0; i < ((InsertUpdatePersonResp)args.Msg).ErrorCount; i++)
                {
                    msg += (ClockResultType)((InsertUpdatePersonResp)args.Msg).ErrorCodeList[i] + "\n";
                }

                DFS.Ack(args.Msg.DeviceID);

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });

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
                msg = "Retorno do comando DeletePersonResp " +
               "\nDeviceID " + ((DeletePersonResp)args.Msg).DeviceID.ToString() +
               "\nPersonID " + ((DeletePersonResp)args.Msg).PersonID +
               "\nStatus " + (ClockResultType)((DeletePersonResp)args.Msg).Status
               ;

                DFS.Ack(args.Msg.DeviceID);

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });

            }
            #endregion Manager People Resp

            #region UpdateClockResp
            else if (args.Msg is UpdateClockResp)
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] {
                "\nRetorno do comando UpdateClock " +
                "\nDeviceID " + ((UpdateClockResp)args.Msg).DeviceID.ToString() +
                "\nStatus " + (ClockResultType)((UpdateClockResp)args.Msg).Status 
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
                msg =
               "\nRetorno do comando InsertUpdateCompany " +
               "\nDeviceID " + ((ManagerCompanyResp)args.Msg).DeviceID.ToString() +
               "\nStatus " + (ClockResultType)((ManagerCompanyResp)args.Msg).Status;

                DFS.Ack(args.Msg.DeviceID);

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });

            }
            #endregion ManagerCompanyResp

            #region DeviceConnectedMessageClock
            else if (args.Msg is DeviceConnectedMessageClock)
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Requisição de Conexão" +
                                                                                "\nDeviceID : " + ((DeviceConnectedMessageClock)args.Msg).DeviceID.ToString() +
                                                                                "\nDevice Type : " + ((DeviceConnectedMessageClock)args.Msg).DeviceType.ToString() +
                                                                                 "\nFirmwareVersion : " + ((DeviceConnectedMessageClock)args.Msg).FirmwareVersion.ToString()});


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

                msg = "\nEvento....= RequestLogResp " +
                    "\nDeviceID.......= " + ((RequestLogResp)args.Msg).DeviceID.ToString() +
                   "\nTot Event.....= " + ((RequestLogResp)args.Msg).TotEvent.ToString() + "\n";

                DFS.Ack(args.Msg.DeviceID);

                for (int i = 0, n = 1; i < (int)((RequestLogResp)args.Msg).TotEvent; i++, n++)
                {
                    msg += "\nEvento nº " + n.ToString() +
                        "\nId Evento  = " + (EventClockType)((RequestLogResp)args.Msg).Events[i].EventID +
                              "\nDate    = " + ((RequestLogResp)args.Msg).Events[i].TimeEvent.ToString() + "\n";

                    if ((EventClockType)((RequestLogResp)args.Msg).Events[i].EventID == EventClockType.REGISTERED_POINT)
                    {
                        msg += "\nNSR " + ((EventRegisteredPoint)((RequestLogResp)args.Msg).Events[i]).Nsr.ToString() +
                          "\nPis " + ((EventRegisteredPoint)((RequestLogResp)args.Msg).Events[i]).Pis.ToString() +
                          "\nTec reader " + (CardTechType)((EventRegisteredPoint)((RequestLogResp)args.Msg).Events[i]).Technology + "\n";
                    }

                    msg += "\n";
                }

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
            }
            #endregion  RequestLogResp

            #region RequestBackupLogResp
            else if (args.Msg is RequestBackupLogResp)
            {
                msg = "\nEvento....= RequestBackupLogResp " +
                    "\nDeviceID.......= " + ((RequestBackupLogResp)args.Msg).DeviceID.ToString() +
                   "\nTot Event.....= " + ((RequestBackupLogResp)args.Msg).TotEvent.ToString() + "\n";

                DFS.Ack(args.Msg.DeviceID);

                for (int i = 0, n = 1; i < (int)((RequestBackupLogResp)args.Msg).TotEvent; i++, n++)
                {
                    msg += "\nEvento nº " + n.ToString() +
                        "\nId Evento  = " + (EventClockType)((RequestBackupLogResp)args.Msg).Events[i].EventID +
                              "\nDate    = " + ((RequestBackupLogResp)args.Msg).Events[i].TimeEvent.ToString() + "\n";

                    if ((EventClockType)((RequestBackupLogResp)args.Msg).Events[i].EventID == EventClockType.REGISTERED_POINT)
                    {
                        msg += "\nNSR " + ((EventRegisteredPoint)((RequestBackupLogResp)args.Msg).Events[i]).Nsr.ToString() +
                          "\nPis " + ((EventRegisteredPoint)((RequestBackupLogResp)args.Msg).Events[i]).Pis.ToString() +
                          "\nTec reader " + (CardTechType)((EventRegisteredPoint)((RequestBackupLogResp)args.Msg).Events[i]).Technology + "\n";
                    }

                    msg += "\n";
                }
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { msg });
            }

        }
            #endregion RequestBackupLogResp

        #endregion

        #endregion

        #region **** Load Lists ****

        #region PermissionList ok
        public void PermissionListTest()
        {

            //Lista de Permissões         
            PermissionListReaderPermissionTime[] perLstRdPermTime = new PermissionListReaderPermissionTime[2];
            PermissionListReaderPermission[] perLstRdPermss = new PermissionListReaderPermission[2];
            PermissionListReader[] perLstReader = new PermissionListReader[1];
            PermissionList permLst = new PermissionList();

            perLstRdPermTime[0] = new PermissionListReaderPermissionTime();
            perLstRdPermTime[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            perLstRdPermTime[0].InitTime = 0001;
            perLstRdPermTime[0].EndTime = 0020;
            perLstRdPermTime[1] = new PermissionListReaderPermissionTime();
            perLstRdPermTime[1].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            perLstRdPermTime[1].InitTime = 0003;
            perLstRdPermTime[1].EndTime = 0040;

            perLstRdPermss[0] = new PermissionListReaderPermission();
            perLstRdPermss[0].PermissionCode = 1;
            perLstRdPermss[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            perLstRdPermss[0].Time = perLstRdPermTime;
            perLstRdPermss[1] = new PermissionListReaderPermission();
            perLstRdPermss[1].PermissionCode = 2;
            perLstRdPermss[1].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            perLstRdPermss[1].Time = perLstRdPermTime;

            perLstReader[0] = new PermissionListReader();
            perLstReader[0].ReaderCode = 1;
            perLstReader[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());

            perLstReader[0].Permission = perLstRdPermss;
            permLst.Readers = perLstReader;

            permLst.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());

            LoadList loadl = new LoadList();
            loadl.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            loadl.CommStatus = 2;
            loadl.SeqCmd = 100;
            loadl.Type = 1;
            loadl.List = permLst;
            loadl.OperationType = 0;

            if (DFS.Execute(loadl) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }
        }
        #endregion

        #region TemplateList --
        public void TemplateListTest()
        {

            TemplateListPersonTemplate[] tmpLstPerTemp = new TemplateListPersonTemplate[2];
            TemplateListPersonCard[] tmpLstPersCard = new TemplateListPersonCard[1];
            TemplateListPerson[] tmpLstPers = new TemplateListPerson[1];
            TemplateList tmplLst = new TemplateList();

            tmplLst.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            // Primeira pessoa

            //biometria 1
            tmpLstPerTemp[0] = new TemplateListPersonTemplate();
            tmpLstPerTemp[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            tmpLstPerTemp[0].TamplateFactory = 1;
            tmpLstPerTemp[0].Template = biometric1;// new Byte[1024];

            //biometria 2
            tmpLstPerTemp[1] = new TemplateListPersonTemplate();
            tmpLstPerTemp[1].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            tmpLstPerTemp[1].TamplateFactory = 1;
            tmpLstPerTemp[1].Template = biometric2; // new Byte[1024];

            tmpLstPersCard[0] = new TemplateListPersonCard();
            tmpLstPersCard[0].CardID = new Byte[] { 0x00, 0x1C, 0x8F, 0xC3, 0x0C };
            tmpLstPersCard[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            tmpLstPersCard[0].CardTec = 1;


            tmpLstPers[0] = new TemplateListPerson();
            tmpLstPers[0].BioConfLevel = 1;
            tmpLstPers[0].DeviceID = 1;
            tmpLstPers[0].Card = tmpLstPersCard;
            tmpLstPers[0].Template = tmpLstPerTemp;
            tmpLstPers[0].PersonID = "0000000123456789".PadLeft(23);

            tmplLst.Person = tmpLstPers;

            LoadList loadl = new LoadList();
            loadl.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            loadl.CommStatus = 2;
            loadl.SeqCmd = 100;
            loadl.Type = 4;
            loadl.List = tmplLst;
            loadl.OperationType = 0;

            if (DFS.Execute(loadl) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }


        }
        #endregion

        #region PwdList --
        public void PwdListTest()
        {
            //Lista de senhas
            PwdList pwdLst = new PwdList();
            PwdListValues[] pwValue = new PwdListValues[3];
            LoadList loadLst = new LoadList();

            pwValue[0] = new PwdListValues();
            pwValue[0].CardID = new Byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            pwValue[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            pwValue[0].Pwd = "123456";

            pwValue[1] = new PwdListValues();
            pwValue[1].CardID = new Byte[] { 0x00, 0x02, 0x03, 0x04, 0x00 };
            pwValue[1].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            pwValue[1].Pwd = "432156";

            pwValue[2] = new PwdListValues();
            pwValue[2].CardID = new Byte[] { 0x00, 0x02, 0x03, 0x04, 0x00 };
            pwValue[2].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            pwValue[2].Pwd = "227788";

            pwdLst.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            pwdLst.Values = pwValue;


            loadLst.CommStatus = 2;
            loadLst.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            loadLst.SeqCmd = 123;
            loadLst.List = pwdLst;
            loadLst.Type = 8;

            if (DFS.Execute(loadLst) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " accessLst Comando enviado com sucesso" });
            }

        }
        #endregion

        #region BlockList --
        public void BlockListTest()
        {
            BlockList blockLst = new BlockList();
            AccessListValues[] accessValue = new AccessListValues[2];
            blockLst.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            // Primeira pessoa
            accessValue[0] = new AccessListValues();
            accessValue[0].ActivatedReaders = "1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1";
            accessValue[0].CardType = 1;
            accessValue[0].CardID = new Byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            accessValue[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            // Segunda pessoa
            accessValue[1] = new AccessListValues();
            accessValue[1].ActivatedReaders = "1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1";
            accessValue[1].CardType = 1;
            accessValue[1].CardID = new Byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            accessValue[1].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());

            blockLst.Values = accessValue;

            LoadList loadl = new LoadList();
            loadl.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            loadl.CommStatus = 2;
            loadl.SeqCmd = 10;
            loadl.Type = 2;
            loadl.List = blockLst;
            loadl.OperationType = 0;

            if (DFS.Execute(loadl) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " accessLst Comando enviado com sucesso" });
            }
        }
        #endregion

        #region EmployeeList --
        public void EmployeeListTest()
        {
            //Lista de Empregados
            EmployeeList empLst = new EmployeeList();
            EmployeeListPerson[] empLstPers = new EmployeeListPerson[2];
            EmployeeListPersonCard[] empLstPCard = new EmployeeListPersonCard[2];
            EmployeeListPersonCard[] empLstPCard1 = new EmployeeListPersonCard[2];

            empLst.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            // Primeiro empregado
            empLstPers[0] = new EmployeeListPerson();
            empLstPers[0].BioConfLevel = 0;
            empLstPers[0].PersonID = new string('*', 23);

            empLstPCard[0] = new EmployeeListPersonCard();
            empLstPCard[1] = new EmployeeListPersonCard();

            empLstPers[0].Card = empLstPCard;
            empLstPers[0].Card[0].CardID = new Byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            empLstPers[0].Card[0].CardTec = 2;
            empLstPers[0].Card[1].CardTec = 2;
            empLstPers[0].Card[1].CardID = new Byte[] { 0x01, 0x02, 0x03, 0x04, 0x06 };

            // Segundo empregado
            empLstPers[1] = new EmployeeListPerson();
            empLstPers[1].BioConfLevel = 0;
            empLstPers[1].PersonID = new string('x', 23);

            empLstPCard1[0] = new EmployeeListPersonCard();
            empLstPCard1[1] = new EmployeeListPersonCard();

            empLstPers[1].Card = empLstPCard1;
            empLstPers[1].Card[0].CardID = new Byte[] { 0x01, 0x02, 0x03, 0x04, 0x07 };
            empLstPers[1].Card[0].CardTec = 2;
            empLstPers[1].Card[1].CardTec = 2;
            empLstPers[1].Card[1].CardID = new Byte[] { 0x01, 0x02, 0x03, 0x04, 0x08 };

            empLst.Person = empLstPers;
            LoadList loadl = new LoadList();
            loadl.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            loadl.CommStatus = 2;
            loadl.SeqCmd = 10;
            loadl.Type = 6;
            loadl.List = empLst;
            loadl.OperationType = 0;

            if (DFS.Execute(loadl) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " empLst  SnackTimeList ,Comando enviado com sucesso" });
            }

        }
        #endregion

        #region SnackTimeList --
        public void SnackTimeListTest()
        {
            //Lista de Refeitorio
            SnackTimeList snkLst = new SnackTimeList();
            SnackTimeListValues[] snkValue = new SnackTimeListValues[4];
            SnackTimeListReservation[] scnkReser = new SnackTimeListReservation[2];

            snkLst.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());

            scnkReser[0] = new SnackTimeListReservation();
            scnkReser[0].InitConsume = 4;
            scnkReser[0].EndConsume = 5;
            scnkReser[0].InitReservation = 6;
            scnkReser[0].EndReservation = 7;
            scnkReser[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());

            scnkReser[1] = new SnackTimeListReservation();
            scnkReser[1].InitConsume = 8;
            scnkReser[1].EndConsume = 9;
            scnkReser[1].InitReservation = 10;
            scnkReser[1].EndReservation = 11;
            scnkReser[1].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());

            snkValue[0] = new SnackTimeListValues();
            snkValue[0].DayType = 0;
            snkValue[0] = new SnackTimeListValues();
            snkValue[0].Reservation = scnkReser;

            snkValue[1] = new SnackTimeListValues();
            snkValue[1].DayType = 1;
            snkValue[1] = new SnackTimeListValues();
            snkValue[1].Reservation = scnkReser;

            snkValue[2] = new SnackTimeListValues();
            snkValue[2].DayType = 2;
            snkValue[2] = new SnackTimeListValues();
            snkValue[2].Reservation = scnkReser;

            snkValue[3] = new SnackTimeListValues();
            snkValue[3].DayType = 3;
            snkValue[3] = new SnackTimeListValues();
            snkValue[3].Reservation = scnkReser;

            snkLst.Values = snkValue;

            LoadList loadl = new LoadList();
            loadl.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            loadl.CommStatus = 2;
            loadl.SeqCmd = 10;
            loadl.Type = 7;
            loadl.List = snkLst;
            loadl.OperationType = 0;
            if (DFS.Execute(loadl) > 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "  SnackTimeList ,Comando enviado com sucesso" });
            }

        }
        #endregion

        #region HolidaysList --
        public void HolidaysListTest()
        {
            HolidaysList holLst = new HolidaysList();
            HolidaysListValues[] holValue = new HolidaysListValues[4];  //tamanho da lista
            holValue[0] = new HolidaysListValues();
            holValue[0].Date = "01/01/2012 00:00:00";
            holValue[1] = new HolidaysListValues();
            holValue[1].Date = "02/02/2012 00:00:00";
            holValue[2] = new HolidaysListValues();
            holValue[2].Date = "03/01/2012 00:00:00";
            holValue[3] = new HolidaysListValues();
            holValue[3].Date = "04/02/2012 00:00:00";
            holLst.Values = holValue;

            LoadList loadl = new LoadList();
            loadl.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            loadl.CommStatus = 2;
            loadl.SeqCmd = 10;
            loadl.Type = 2;
            loadl.List = holLst;
            loadl.OperationType = 0;

            if (DFS.Execute(loadl) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Comando enviado com sucesso" });
            }

        }
        #endregion

        #region AccessList --
        public void AccessListTest()
        {

            //Lista de Acessos
            AccessList accessLst = new AccessList();
            AccessListValues[] accessValue = new AccessListValues[2];
            accessLst.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            // Primeira pessoa
            accessValue[0] = new AccessListValues();
            accessValue[0].ActivatedReaders = "1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1";
            accessValue[0].CardType = 1;
            accessValue[0].CardID = new Byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            accessValue[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            // Segunda pessoa
            accessValue[1] = new AccessListValues();
            accessValue[1].ActivatedReaders = "1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1";
            accessValue[1].CardType = 1;
            accessValue[1].CardID = new Byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 };
            accessValue[1].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());

            accessLst.Values = accessValue;

            LoadList loadl = new LoadList();
            loadl.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            loadl.CommStatus = 2;
            loadl.SeqCmd = 10;
            loadl.Type = 2;
            loadl.List = accessLst;
            loadl.OperationType = 0;

            if (DFS.Execute(loadl) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " accessLst Comando enviado com sucesso" });
            }

        }
        #endregion

        #region AccessList2
        public void AccessListTest2()
        {
            AccessList al = new AccessList();
            AccessListValues[] alv = new AccessListValues[1];

            alv[0] = new AccessListValues();
            alv[1] = new AccessListValues();

            alv[0].ActivatedReaders = "1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1";
            alv[0].CardID = new Byte[] { 0x00, 0x1C, 0x8F, 0xC3, 0x0c };
            alv[0].CardType = 2;
            alv[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            alv[1].ActivatedReaders = "1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1";
            alv[1].CardID = new Byte[] { 0x00, 0x1C, 0x8F, 0xC3, 0x0c };
            alv[1].CardType = 2;
            alv[1].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());

            al.Values = new AccessListValues[] { alv[0], alv[1] };

            LoadList ll = new LoadList();

            ll.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            ll.List = al;
            ll.CommStatus = 2;
            ll.SeqCmd = 1;
            ll.Type = 3;
            ll.OperationType = 0;

            int retorno = DFS.Execute(ll);

        }
        #endregion

        #region Employee
        public void EmployeeTest()
        {
            EmployeeListClock employeeListsClock = new EmployeeListClock();
            Person person = new Person();

            //CardClock[] cardClock = new CardClock[1];

            //cardClock[0] = new CardClock(); 
            ///cardClock[0].CardTec = 3; cardClock[0].CardID = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };
            //cardClock[0].CardTec = 1;
            //cardClock[0].CardID = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x01, 0x02, 0x03, 0x04 };
            
            //person.CardClock = cardClock;
            person.Name = "Mariany De Souza Leite";
            person.PersonID = "13181174423".PadRight(30, ' ');
            person.Pis = 13181174423;
            person.RecordKeyboard = 1;
            person.TypeOfValidation = 2;

            employeeListsClock.Person = person;

            LoadList loadList = new LoadList();
            loadList.DeviceID = 5433; ///Convert.ToInt32(lstB1.SelectedItem.ToString());
            loadList.CommStatus = 2;
            loadList.SeqCmd = 1;
            loadList.Type = 9;
            loadList.List = employeeListsClock;
            loadList.OperationType = 2;

            if (DFS.Execute(loadList) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Comando enviado com sucesso" });
            }

            employeeListsClock = new EmployeeListClock();
            person = new Person();

            //cardClock = new CardClock[1];
            //cardClock[0] = new CardClock(); 
            //cardClock[0].CardTec = 1;
            //cardClock[0].CardID = new byte[] { 0x00, 0x00, 0x00, 0x00,  0x00, 0x05, 0x03, 0x08 };

            //person.CardClock = cardClock;

            person.Name = "Izabel Cristina Galvão";
            person.PersonID = "10697105722".PadRight(30, ' ');
            person.Pis = 10697105722;
            person.RecordKeyboard = 1;
            person.TypeOfValidation = 2;

            employeeListsClock.Person = person;

            loadList = new LoadList();
            loadList.DeviceID = 5433; ///Convert.ToInt32(lstB1.SelectedItem.ToString());
            loadList.CommStatus = 2;
            loadList.SeqCmd = 10;
            loadList.Type = 9;
            loadList.List = employeeListsClock;
            loadList.OperationType = 2;

            if (DFS.Execute(loadList) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Comando enviado com sucesso" });
            }

        }
        #endregion Employee


        #endregion

        #region **** Access Commands  ****

        #region UpdateDateTime --
        public void UpdateDateTimeTest()
        {
            UpdateDateTime udt = new UpdateDateTime();
            //udt.Time = maskedTxt1.Text;
            udt.Time = "05/11/2013 11:00:00";
            udt.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            udt.SeqCmd = 2;
            udt.CommStatus = 1;
            if (DFS.Execute(udt) == 0)
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Data Atualizada" });
            }
        }
        #endregion

        #region LoadSmartMap --
        public void LoadSmartMapTest()
        {

            LoadSmartMap obj = new LoadSmartMap();
            obj.CommStatus = 2;
            obj.DeviceID =
            obj.SeqCmd = 123;
            obj.Map = new Byte[1024];

            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }
        }
        #endregion

        #region BackupAccess --
        public void BackupAccessTest()
        {

            BackupAccess obj = new BackupAccess();
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 123;
            obj.EventInitDateTime = "25/01/2012 00:00:00";
            obj.EventEndDateTime = "26/07/2012 20:00:00";
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando BackupAccess enviado com sucesso" });
            }

        }
        #endregion

        #region LoadBadgeID --
        public void LoadBadgeIDSTest()
        {
            BadgeIDReg[] bgId = new BadgeIDReg[2];
            LoadBadgeIDs obj = new LoadBadgeIDs();

            obj.Reg = bgId;

            obj.Reg[0] = new BadgeIDReg();
            obj.Reg[0].Type = 1;
            obj.Reg[0].Id = 2;

            obj.Reg[1] = new BadgeIDReg();
            obj.Reg[1].Type = 3;
            obj.Reg[1].Id = 4;

            obj.SeqCmd = 1;
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());

            if (DFS.Execute(obj) < 0) { /*erro*/}
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Comando Enviado com sucesso" });
            }
        }
        #endregion

        #region UpdateBio --
        public void UpdateBioTest()
        {
            UpdateBio obj = new UpdateBio();
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 128;
            obj.Location = 10;

            Operation[] operation = new Operation[3];

            //Operação 1
            operation[1] = new Operation();
            operation[1].Type = 1;
            operation[1].TemplateListPerson = new TemplateListPerson();
            operation[1].TemplateListPerson.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            operation[1].TemplateListPerson.PersonID = "1A23456789abcdef".PadRight(23);
            operation[1].TemplateListPerson.Card = new TemplateListPersonCard[1];
            operation[1].TemplateListPerson.Card[0] = new TemplateListPersonCard();
            operation[1].TemplateListPerson.Card[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            operation[1].TemplateListPerson.Card[0].CardID = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04 };
            operation[1].TemplateListPerson.Card[0].CardTec = 3;
            operation[1].TemplateListPerson.Template = new TemplateListPersonTemplate[1];
            operation[1].TemplateListPerson.Template[0] = new TemplateListPersonTemplate();
            operation[1].TemplateListPerson.Template[0].TamplateFactory = 1;
            operation[1].TemplateListPerson.Template[0].Template = new byte[1024];
            operation[1].TemplateListPerson.Template[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());

            //Operação 2
            operation[0] = new Operation();
            operation[0].Type = 2;
            operation[0].EmployeeListPerson.PersonID = "1A23456789abcdef".PadRight(23);
            operation[0].EmployeeListPerson.BioConfLevel = 75;
            operation[0].EmployeeListPerson.Card = new EmployeeListPersonCard[1];
            operation[0].EmployeeListPerson.Card[0] = new EmployeeListPersonCard();
            operation[0].EmployeeListPerson.Card[0].CardID = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04 };
            operation[0].EmployeeListPerson.Card[0].CardTec = 3;
            operation[0].EmployeeListPerson.Card[0].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());

            //Operação 3
            operation[2] = new Operation();
            operation[2].Type = 3;
            operation[2].PersonId.PersonID = "1A23456789abcdef".PadRight(23);
            operation[2].DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());

            obj.Operation = operation;

            if (DFS.Execute(obj) < 0) { /*erro*/}
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Comando  UpdateBio enviado com sucesso" });
            }

        }
        #endregion

        #region  DigitalInStatus --
        public void DigitalInStatusTest()
        {
            DigitalInStatus obj = new DigitalInStatus();
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 128;
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }
        }
        #endregion

        #region HandkeyCalibrate --
        public void HandkeyCalibrateTest()
        {
            HandkeyCalibrate obj = new HandkeyCalibrate();
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 128;
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }

        }
        #endregion

        #region  BackupAlarm --
        public void BackupAlarmTest()
        {
            BackupAlarm obj = new BackupAlarm();
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 126;
            obj.EventInitDateTime = "08/07/2012 00:00:00";
            obj.EventEndDateTime = "25/07/2012 20:00:00";
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }

        }
        #endregion

        #region UpdateDFSServer --
        public void UpdateDFSServerTest()
        {
            UpdateDFSServer obj = new UpdateDFSServer();
            obj.CommStatus = 2;
            obj.DeviceID = 1;
            obj.SeqCmd = 128;
            obj.Ip = "162.121.111.001";
            obj.Port = 3232;
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }
        }
        #endregion

        #region EnableEmergency --
        public void EnableEmergencyTest()
        {
            EnableEmergency obj = new EnableEmergency();
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 128;
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }
        }
        #endregion

        #region DisableEmergency --
        public void DisableEmergencyTest()
        {
            DisableEmergency obj = new DisableEmergency();
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 128;
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }
        }
        #endregion

        #region UpdateSummerTime --
        public void UpdateSummerTimeTest()
        {
            UpdateSummerTime obj = new UpdateSummerTime();
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 128;
            obj.InitSummerTime = "01/04/2012";
            obj.EndSummerTime = "30/07/2012";
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }
        }
        #endregion

        #region Device Status --
        public void DeviceStatusTest()
        {
            DeviceStatus obj = new DeviceStatus();
            obj.CommStatus = 2;
            obj.DeviceID = 5433;
            //Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 128;
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }

        }
        #endregion

        #region UpdateFirmware --
        public void UpdateFirmwareTest()
        {
            UpdateFirmware obj = new UpdateFirmware();
            obj.CommStatus = 2;
            obj.DeviceID = 1;
            obj.SeqCmd = 128;
            obj.Firmware = "C:\firmware-digicon.tar.gz";
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }

        }
        #endregion

        #region BlockDevice --
        public void BlockDeviceTest()
        {
            BlockDevice obj = new BlockDevice();
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 1;
            obj.CommStatus = 1;
            obj.Readers = "1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1";
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }

        }
        #endregion

        #region ListStatus --
        public void ListStatusTest()
        {

            ListStatus obj = new ListStatus();
            obj.Type = 5;
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 123;
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }
        }
        #endregion

        #region CheckBioList --
        public void CheckBioListTest()
        {
            CheckBioList obj = new CheckBioList();
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 128;
            obj.Code = 3;
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }
        }
        #endregion

        #region DeletList --
        public void DeletListTest()
        {
            DeletList obj = new DeletList();
            obj.Type = 3;
            obj.CommStatus = 2;
            obj.DeviceID = 5433; //Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 1;
            if (DFS.Execute(obj) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }
        }
        #endregion

        #region UnblockDevice --
        public void UnblockDeviceTest()
        {
            UnblockDevice bd = new UnblockDevice();
            bd.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            bd.SeqCmd = 1;
            bd.CommStatus = 1;
            bd.Readers = "1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1";
            if (DFS.Execute(bd) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { " Comando enviado com sucesso" });
            }
        }
        #endregion

        #region AutomaticProcess --
        public void AutomaticProcessTest()
        {
            AutomaticProcess autoProc = new AutomaticProcess();
            autoProc.CommStatus = 2;
            autoProc.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            autoProc.SeqCmd = 10;
            // buzzer
            autoProc.Process.Actuates.Buzzer.ExitAction = 1;
            autoProc.Process.Actuates.Buzzer.ActionTime = 10000;
            autoProc.Process.Actuates.Buzzer.DeviceID = 1;

            //Saidas
            autoProc.Process.Actuates.Saidas[0].DeviceID = 1;
            autoProc.Process.Actuates.Saidas[0].EnterDependence = 0;
            autoProc.Process.Actuates.Saidas[0].ActionTime = 10000;
            autoProc.Process.Actuates.Saidas[0].ExitAction = 1;
            //...//
            autoProc.Process.Actuates.Saidas[7].DeviceID = 1;
            autoProc.Process.Actuates.Saidas[7].EnterDependence = 0;
            autoProc.Process.Actuates.Saidas[7].ActionTime = 10000;
            autoProc.Process.Actuates.Saidas[7].ExitAction = 1;

            //data
            autoProc.Process.DateLastExec = "10/09/2012 00:00:00";
            autoProc.Process.DateInitExec = "12/09/2012 00:00:00";
            autoProc.Process.DateEndExec = "13/10/2012 00:00:00";
            autoProc.Process.DaysOfTheWeek = "0;0;0;0;0;0;0;0";
            autoProc.Process.Periodicity = 1;
            autoProc.Process.ExecHolidays = 1;
            autoProc.Process.ProcessID = 0;



            if (DFS.Execute(autoProc) < 0) { }
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Comando enviado com sucesso" });
            }

        }
        #endregion

        #region DisableDigitalOuT --
        public void DisableDigitalOuTest()
        {

            DisableDigitalOut obj = new DisableDigitalOut();
            for (int i = 1; i <= 13; i++)
            {
                obj.CommStatus = 2;
                obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
                obj.SeqCmd = 128;
                obj.Code = (byte)i;
                if (DFS.Execute(obj) < 0)
                { /*erro*/}
                else
                {
                    consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Comando EnableDigitalOutenviado com sucesso " });
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
        #endregion

        #region EnableDigitalOut --
        public void EnableDigitalOutTest()
        {
            //     DisableDigitalOuTest();
            EnableDigitalOut obj = new EnableDigitalOut();
            for (int i = 1; i <= 8; i++)
            {
                obj.CommStatus = 2;
                obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
                obj.SeqCmd = 128;
                obj.Code = (byte)i;
                obj.Time = 10000;
                if (DFS.Execute(obj) < 0)
                { /*erro*/}
                else
                {
                    consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Comando enviado com sucesso – " });

                }
                System.Threading.Thread.Sleep(500);
            }
            //   DigitalOutStatusTest();
            //    System.Threading.Thread.Sleep(1000);
            //    DisableDigitalOuTest();
            //  System.Threading.Thread.Sleep(1000);
            //  DigitalOutStatusTest();
        }
        #endregion

        #region DigitalOutStatus --
        public void DigitalOutStatusTest()
        {
            DigitalOutStatus obj = new DigitalOutStatus();
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 128;
            if (DFS.Execute(obj) < 0)
            { /*erro*/}
            else
            {

                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Comando DigitalOutStatus enviado com sucesso – Aguarde retorno!" });
            }
        }
        #endregion

        #region IsAlive --
        public void IsAliveTest()
        {
            IsAlive obj = new IsAlive();
            obj.CommStatus = 2;
            obj.DeviceID = Convert.ToInt32(lstB1.SelectedItem.ToString());
            obj.SeqCmd = 12;

            int retorno = DFS.Execute(obj);

            if (retorno == 0) { /* MessageBox.Show("Enviado  biolist."); */}
            else
            {
                consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { "Erro. ISALIVE" });
            }
        }
        #endregion

        #region ConfigFirmware
        public void ConfigFirmwareTest()
        {

            ConfigFirmware obj = new ConfigFirmware();

            obj.NewDeviceID = 2;
            obj.NewDeviceIP = "10.104.5.123";
            obj.NewNetGw = "10.104.10.1";
            obj.NewNetMask = "255.255.0.0";
            obj.ConnMode = 2;
            obj.ServerPort = 3232;
            obj.ServerIP = "10.104.1.59";
            obj.InitSummerTime = "01/01/0001 00:00:00";
            obj.EndSummerTime = "01/01/0001 00:00:00";
            obj.Utc = -3;
            obj.SubsidiaryID = 0;
            obj.StandardMsg = "ID2 Vs 10.1.1.0";
            obj.UseDisplay = 1;
            obj.KeyBlock = 0;
            obj.KeyBlock = 0;
            obj.CardExpirationDays = 0;
            obj.KeepAlive = 3000;
            obj.CommTimeout = 1500;

            Function[] func = new Function[10];
            obj.Functions = func;
            obj.Functions[0].ExeActuator = "1;0";

            obj.StandardDigitalOut = "0;0;0;0;0;0;0;0;0;0;0;0;0";
            obj.BurlaTime = 0;
            obj.RfidType = 0;
            obj.InitParityBit = 0;
            obj.EndParityBit = 0;
            obj.RfidNumComp = 0;


            obj.AuthorizerTimeout = 0;
            obj.SmartCardValidation = 0;

            obj.CommStatus = 2;
            obj.SeqCmd = 123;
            obj.DeviceID = 2;


        }
        #endregion

        #endregion

        #region **** Clock Commands  ****

        public void manageperson(int devId)
        {
            LoadList loadL = new LoadList();
            EmployeeListClock obj = new EmployeeListClock();
            loadL.OperationType = 0;

            obj.Person.CardClock = new CardClock[1];
            obj.Person.CardClock[0] = new CardClock();
            obj.Person.CardClock[0].CardID = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01 };
            obj.Person.CardClock[0].CardTec = 2;
            obj.Person.CardClock[0].DeviceID = devId;

            obj.Person.DeviceID = devId;
            obj.Person.Name = "Jesus";
            obj.Person.PersonID = "000123456789".PadLeft(30, '0');
            obj.Person.Pis = 000000000000;
            obj.Person.RecordKeyboard = 1;
            obj.Person.TypeOfValidation = 2;
            obj.Person.Templates = new Templates[2];
            obj.Person.Templates[0] = new Templates();
            obj.Person.Templates[0].Template = new byte[200];
            obj.Person.Templates[1] = new Templates();
            obj.Person.Templates[1].Template = new byte[200];

            loadL.List = obj;

            DFS.Execute(loadL);
        }

        public void requestLog(int devId)
        {
            RequestLog obj = new RequestLog();
            obj.DeviceID = devId;

            DFS.Execute(obj);
        }

        public void requestPersonList(int devId)
        {
            RequestPersonList obj = new RequestPersonList();
            obj.DeviceID = devId;
            obj.PersonID = "Jesus".PadRight(30, ' ');
            obj.RequisitionType = 1;
            DFS.Execute(obj);

        }

        public void requestDeviceStatus(int devId)
        {
            DeviceStatus obj = new DeviceStatus();
            obj.DeviceID = devId;
        }

        public void requestBackupLog(int devId)
        {
            RequestBackupLog obj = new RequestBackupLog();
            obj.DeviceID = devId;

            DFS.Execute(obj);
        }

        public void ConfigDeviceFunc(int devId)
        {
            DeviceConfig obj = new DeviceConfig();
            obj.DeviceID = devId;
            obj.KeepAlive = 10000;
            obj.BiometricValidation = 2;
            obj.Configuration("C:\\CONFIG.xml");

            DFS.Execute(obj);
        }

        public void managerCompany(int devId)
        {
            ManagerCompany obj = new ManagerCompany();
            obj.DeviceID = devId;
            obj.Company.Cei = "000000000000";
            obj.Company.Identifier = "000000000";
            obj.Company.SocialName = "Empresa";
            obj.Company.Local = "São Paulo";

            DFS.Execute(obj);
        }


        #endregion **** Clock Commands  ****

        #region Functions Calls

        private void btnExec_Click(object sender, EventArgs e)
        {
            UpdateDateTimeTest();
        }

        #region Server start/stop
        // <summary>
        /// Starts the communication with the remote device.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOnOff_Click(object sender, EventArgs e)
        {
            if (start)
            {
                start = false;
                DFS.Stop();
                this.start = false;
                btnOnOff.Text = "Start";
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lstB1.BeginInvoke(new updateTxt(atualizaId), new object[] { "" });
            }
            else
            {
                if (DFS.Start(ipDevice.Text.Replace(" ", "").Replace(",", "."), Convert.ToInt32(port.Text), 2) == 0)
                {
                    start = true;
                    lblStatus.ForeColor = System.Drawing.Color.Blue;
                    btnOnOff.Text = "Stop";
                    lblStatus.Text = "OnLine";
                    StatusTerminal();
                }
                else
                {
                    MessageBox.Show("Porta ainda em uso, aguarde...");
                    lstB1.BeginInvoke(new updateTxt(atualizaId), new object[] { "" });
                }
            }

        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            DFS.Stop();
            System.Threading.Thread.Sleep(500);
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeviceStatusTest();
            if (lstB1.SelectedItem != null)
            {
                BackupAccessTest();
                UpdateBioTest();
                manageperson(Convert.ToInt32(ipDevice.Text));
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lstB1.SelectedItem != null)
                UnblockDeviceTest();
            //BackupAccessTest();
            //DigitalOutStatusTest();
            //    ListStatusTest();
            //UpdateSummerTimeTest();
            //  CheckBioListTest();
            //AutomaticProcessTest();
            // BackupAlarmTest();
            //EnableEmergencyTest();
            //BlockDeviceTest();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (lstB1.SelectedItem != null)
                //DeletListTest();
            //     CheckBioListTest();
            //   DigitalInStatusTest();
            //EnableDigitalOutTest();
            //DeviceStatusTest();
            //DigitalOutStatusTest();
            //EnableDigitalOutTest();
            // HolidaysListTest();
            //AccessListTest();
            //AccessListTest();
            //EmployeeListTest();
            EmployeeTest();
            //SnackTimeListTest();
            //BlockListTest();
            //PwdListTest();
            //PermissionListTest();
            //  TemplateListTest();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (lstB1.SelectedItem != null)
                //  BlockDeviceTest();
                //  BackupAlarmTest(); 
                DigitalInStatusTest();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            consoleLog.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (lstB1.SelectedItem != null)
                DigitalInStatusTest();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            consoleResp.Clear();
        }

        #endregion

        #region Form close

        /// <summary>
        /// Closes the program, stopping the middleware before
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DFS.Stop();
            Environment.Exit(0);
        }

        #endregion Form close

        private void button8_Click(object sender, EventArgs e)
        {
            StatusTerminal();
        }

        private void StatusTerminal()
        {
            Session[] sessions = DFS.Session();
            String stat = "";
            foreach (Session session in sessions)
            {
                stat += "\nDev Id = " + session.DeviceID.ToString(); stat += "\nDev Type = " + session.DevType; stat += "\nDev Serial = " + session.Serial.ToString(); stat += "\nDev Ip = " + session.IpDevice; stat += "\nDev Fwr vs = " + session.FirmwareVersion.ToString(); stat += "\nDev Protocol = " + session.Protocol; stat += "\nDev Date Login = " + session.Login.ToString(); stat += "\nDev Status = " + session.Status.ToString() + "\n";
            }
            consoleLog.BeginInvoke(new updateResp(AtualizaResp), new object[] { stat });
        }

        private void cbResp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ipDevice_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
