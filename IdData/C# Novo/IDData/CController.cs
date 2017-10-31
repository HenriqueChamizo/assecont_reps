using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace IdData
{
    public class CController
    {
        #region Variables

            private ECommand _eCurrentCommand;
            private EConnectionState _eCurrentConnectionState;
            private CIDSysR30 _objIDSysR30;
            private CSocketClient _objSocketClient;
            private List<string> _lstrEventData;

            private string _strCommand_Error_Message;

            public TextBox Log;
            public string IP;

        #endregion

        #region Constructor / Destructor

            public CController()
            {
                this._eCurrentCommand = ECommand.None;
                this._eCurrentConnectionState = EConnectionState.None;

                this._objSocketClient = null;
                this._objIDSysR30 = null;

                this._lstrEventData = new List<string>();

                this._strCommand_Error_Message = "";
                Log = new TextBox();
            }

        #endregion

        #region Generic Functions

            /// <summary>
            /// Formata a string
            /// </summary>
            /// <param name="p_strValue"></param>
            /// <param name="p_strMask"></param>
            /// <param name="p_strValueFormated"></param>
            private void FormatString(string p_strValue, string p_strMask, ref string p_strValueFormated)
            {
                StringBuilder strbValue = new StringBuilder();
                // remove caracteres nao numericos
                foreach (char c in p_strValue)
                {
                    if (Char.IsNumber(c))
                        strbValue.Append(c);
                }

                int idxMask = p_strMask.Length;
                int idxValue = strbValue.Length;

                for (; idxValue > 0 && idxMask > 0; )
                {
                    if (p_strMask[--idxMask] == '#')
                    {
                        idxValue--;
                    }
                }

                StringBuilder strbReturn = new StringBuilder();
                for (; idxMask < p_strMask.Length; idxMask++)
                {
                    strbReturn.Append((p_strMask[idxMask] == '#') ? strbValue[idxValue++] : p_strMask[idxMask]);
                }
                p_strValueFormated = strbReturn.ToString();
            }

        #endregion

        #region Connection to equipment

            public EConnectionState GetConnectionState()
            {
                EConnectionState eAux = this._eCurrentConnectionState;

                if (eAux == EConnectionState.DataReceivedError)
                {
                    this._eCurrentConnectionState = EConnectionState.Connected;
                }

                return eAux;
            }

            public void SetConnectionState(EConnectionState eNewConnectionState)
            {
                this._eCurrentConnectionState = eNewConnectionState;
            }

            public bool ConnectServer(string strIPAddress, int iTCPPort)
            {
                bool bReturn = false;

                try
                {
                    this._objSocketClient = new CSocketClient(strIPAddress, iTCPPort);
                    this._objSocketClient.OnRead += new CSocketClient.BufferEventHandler(SocketClient_OnRead);
                    this._objSocketClient.OnConnect += new CSocketClient.ConnectionDelegate(SocketClient_OnConnect);
                    this._objSocketClient.OnDisconnect += new CSocketClient.ConnectionDelegate(SocketClient_OnDisconnect);
                    this._objSocketClient.OnError += new CSocketClient.ErrorDelegate(SocketClient_OnError);

                    this._eCurrentConnectionState = EConnectionState.AttemptConnection;

                    bReturn = this._objSocketClient.Connect();
                }
                catch (Exception exError)
                {
                    throw exError;
                }

                return bReturn;
            }

            public bool DisconnectServer()
            {
                bool bReturn = false;

                try
                {
                    if (this._objSocketClient != null)
                    {
                        if (this._objSocketClient.Connected)
                        {
                            bReturn = this._objSocketClient.Disconnect();
                        }
                        else
                        {
                            bReturn = true;
                        }
                    }
                    else
                    {
                        bReturn = true;
                    }

                    this._objSocketClient = null;
                    this._eCurrentConnectionState = EConnectionState.Disconnected;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
                
                return bReturn;
            }

            public bool IsConnected()
            {
                try
                {
                    bool bReturn = false;

                    if (this._objSocketClient != null)
                    {
                        bReturn = this._objSocketClient.Connected;
                    }

                    return bReturn;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            private bool SendBuffer(byte[] rgbyTXBuffer)
            {
                try
                {
                    bool bReturn = false;

                    if (this._objSocketClient != null)
                    {
                        bReturn = this._objSocketClient.SendBuffer(rgbyTXBuffer);
                    }

                    return bReturn;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            private void SocketClient_OnConnect()
            {
                this._eCurrentConnectionState = EConnectionState.Connected;
            }

            private void SocketClient_OnDisconnect()
            {
                this._eCurrentConnectionState = EConnectionState.Disconnected;
            }
        
            private void SocketClient_OnRead(byte[] rgbyTXBuffer)
            {
                try
                {
                    int iCommand_Error_Code = this._objIDSysR30.PacketAvail(rgbyTXBuffer);

                    switch (iCommand_Error_Code)
                    {
                        case 0:
                            break;
                        case 2:
                            this.GetLogType2();
                            break;
                        case 3:
                            this.GetLogType3();
                            break;
                        case 4:
                            this.GetLogType4();
                            break;
                        case 5:
                            this.GetLogType5();
                            break;
                        case -1:
                            AddLog("Cartão de Proximidade já cadastrado para outro usuário.");
                            break;
                        case -2:
                            AddLog("Cartão de Código de Barras já cadastrado para outro usuário.");
                            break;
                        case -3:
                            AddLog("PIS já cadastrado para outro usuário.");
                            break;
                        case -4:
                            AddLog("Código individual já cadastrado para outro usuário.");
                            break;
                        case -5:
                            AddLog("Erro na memória MRP.");
                            break;
                        case -6:
                            AddLog("Erro na memória MT.");
                            break;
                        case -7:
                            AddLog("Erro na memória RAM.");
                            break;
                        case -8:
                            AddLog("Dados enviados inválidos.");
                            break;
                        case -9:
                            AddLog("ID REP não possui trabalhadores cadastrados.");
                            break;
                        case -10:
                            AddLog("Trabalhador não cadastrado.");
                            break;
                        case -11:
                            AddLog("ID REP não possui o cadastro do empregador.");
                            break;
                        case -12:
                            AddLog("Dados do empregador inválidos: CPF / CNPJ.");
                            break;
                        case -13:
                            AddLog("Dados do empregador inválidos: Nome / Razão Social.");
                            break;
                        case -14:
                            AddLog("Dados do empregador inválidos: Endereço.");
                            break;
                        case -15:
                            AddLog("Data e/ou hora inválida(s).");
                            break;
                        case -16:
                            AddLog("Erro no módulo biométrico: ERROR.");
                            break;
                        case -17:
                            AddLog("Erro no módulo biométrico: TIMEOUT.");
                            break;
                        case -18:
                            AddLog("Dados de comunicação inválidos: Endereço IP.");
                            break;
                        case -19:
                            AddLog("Dados de comunicação inválidos: Máscara de sub-rede.");
                            break;
                        case -20:
                            AddLog("Dados de comunicação inválidos: IP Gateway.");
                            break;
                        case -21:
                            AddLog("Não existem eventos.");
                            break;
                        case -22:
                            AddLog("Erro no módulo biométrico: CHEIO.");
                            break;
                        case -23:
                            AddLog("Erro na leitura do módulo biométrico: ERROR.");
                            break;
                        case -24:
                            AddLog("Erro na leitura do módulo biométrico: TIMEOUT.");
                            break;
                        case -25:
                            AddLog("Erro de checksum da área de dados.");
                            break;
                        case -26:
                            AddLog("Dados do empregador inválidos: CEI.");
                            break;
                        case -27:
                            AddLog("Equipamento bloqueado.");
                            break;
                        case -100:
                            AddLog("Erro no checksum do cabeçalho do pacote (Verificação na DLL).");
                            break;
                        case -101:
                            AddLog("Erro no checksum dos dados do pacote (Verificação na DLL).");
                            break;
                        case -102:
                            AddLog("Comando inválido (Verificação na DLL).");
                            break;
                        case -103:
                            AddLog("Erro pacote inválido (Verificação na DLL).");
                            break;
                        case -104:
                            AddLog("Erro no tamanho do pacote: pacote vazio (Verificação na DLL).");
                            break;
                        case -105:
                            AddLog("Erro no tamanho dos dados (Verificação na DLL).");
                            break;
                        default:
                            AddLog("Erro desconhecido.");
                            break;
                    }

                    if (iCommand_Error_Code >= 0)
                    {
                        this._eCurrentConnectionState = EConnectionState.DataReceived;

                        switch (this._eCurrentCommand)
                        {
                            case ECommand.ReadUserData:
                                {
                                    this.GetUserData();
                                }
                                break;
                            case ECommand.ReadEmployerData:
                                {
                                    this.GetEmployerData();
                                }
                                break;
                            case ECommand.ReadREPCommunication:
                                {
                                    this.GetREPCommunication();
                                }
                                break;
                            case ECommand.RequestNFR:
                                {
                                    this.GetNFR();
                                }
                                break;
                            case ECommand.RequestTotalNSR:
                                {
                                    this.GetTotalNSR();
                                }
                                break;
                            case ECommand.RequestTotalUsers:
                                {
                                    this.GetTotalUsers();
                                }
                                break;
                            default:
                                {
                                    this.CommandOK();
                                }
                                break;
                        }
                    }
                    else
                    {
                        this._eCurrentConnectionState = EConnectionState.DataReceivedError;

                        this._lstrEventData.Add(" ");
                        this._lstrEventData.Add("Erro : " + iCommand_Error_Code.ToString() + " - " + _strCommand_Error_Message);
                    }
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            private void SocketClient_OnError(string strErroMessage, int iErroCode)
            {
                if (this._eCurrentConnectionState == EConnectionState.AttemptConnection)
                {
                    this._eCurrentConnectionState = EConnectionState.AttemptConnectionFail;
                }
                else
                {
                    this._eCurrentConnectionState = EConnectionState.ConnectionError;
                }

                this._lstrEventData.Add(" ");
                this._lstrEventData.Add("Erro : " + iErroCode.ToString() + " - " + strErroMessage);
            }

        #endregion

        #region DLL

            public bool LoadDLL()
            {
                try
                {
                    this._objIDSysR30 = new CIDSysR30();
                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

        #endregion

        #region Commands

            public void SetCommand(ECommand eNewCommand)
            {
                this._eCurrentCommand = eNewCommand;
            }

            public ECommand GetCommand()
            {
                return this._eCurrentCommand;
            }

            public bool AddUser(string strPIS, string strUserName, uint uiKeyCode, string strBarCode, byte byFacilityCode, ushort usProxCode, byte byUserType, string strPassword, System.IO.MemoryStream msPhoto, ushort usSizeSample, byte byQuantitySamples, byte[] rgbyBiometrics)
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.AddUser(strPIS, strUserName, uiKeyCode, strBarCode, byFacilityCode, usProxCode, byUserType, strPassword, msPhoto, usSizeSample, byQuantitySamples, rgbyBiometrics);

                    if (this.SendBuffer(rgbyBuffer) == false)
                    {
                        return false;
                    }

                    this._eCurrentCommand = ECommand.AddUser;
                    this._eCurrentConnectionState = EConnectionState.SendingData;

                    string strMask = "";
                    string strFormatedValue = "";

                    this._lstrEventData.Clear();
                    this._lstrEventData.Add("--------------------------------------------------");
                    this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                    this._lstrEventData.Add("Dados do Usuário Enviados");
                    this._lstrEventData.Add("--------------------------------------------------");
                    this._lstrEventData.Add("");

                    strMask = "###.#####.##-#";
                    this.FormatString(strPIS, strMask, ref strFormatedValue);
                    this._lstrEventData.Add("PIS : " + strFormatedValue);

                    this._lstrEventData.Add("Nome do Trabalhador : " + strUserName);
                    this._lstrEventData.Add("Tipo (UserType) : " + byUserType.ToString());
                    this._lstrEventData.Add("Modo de Marcação (Status) : " + byUserType.ToString());
                    this._lstrEventData.Add("Código (KeyCode) : " + uiKeyCode.ToString());
                    this._lstrEventData.Add("Senha (Password) : " + strPassword);
                    this._lstrEventData.Add("Proximidade (ProxCode) : " + usProxCode.ToString());
                    this._lstrEventData.Add("Barras (BarCode) : " + strBarCode);
                    this._lstrEventData.Add("Tamanho da Foto (SizePhoto) : 0");
                    this._lstrEventData.Add("Quant. Amostras (QuantitySamples) : " + byQuantitySamples.ToString());
                    this._lstrEventData.Add("Tamanho Amostras (SizeSample) : " + usSizeSample.ToString());

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool ChangeUserData(string strPIS, string strNewPIS, string strUserName, uint uiKeyCode, string strBarCode, byte byFacilityCode, ushort usProxCode, byte byUserType, string strPassword, System.IO.MemoryStream msPhoto, ushort usSizeSample, byte byQuantitySamples, byte[] rgbyBiometrics)
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.ChangeUserData(strPIS, strNewPIS, strUserName, uiKeyCode, strBarCode, byFacilityCode, usProxCode, byUserType, strPassword, msPhoto, usSizeSample, byQuantitySamples, rgbyBiometrics);

                    if (this.SendBuffer(rgbyBuffer) == false)
                    {
                        return false;
                    }

                    this._eCurrentCommand = ECommand.ChangeUserData;
                    this._eCurrentConnectionState = EConnectionState.SendingData;

                    string strMask = "";
                    string strFormatedValue = "";

                    this._lstrEventData.Clear();
                    this._lstrEventData.Add("--------------------------------------------------");
                    this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                    this._lstrEventData.Add("Dados do Usuário Enviados");
                    this._lstrEventData.Add("--------------------------------------------------");
                    this._lstrEventData.Add("");

                    strMask = "###.#####.##-#";
                    this.FormatString(strPIS, strMask, ref strFormatedValue);
                    this._lstrEventData.Add("PIS : " + strFormatedValue);

                    this.FormatString(strNewPIS, strMask, ref strFormatedValue);
                    this._lstrEventData.Add("Novo PIS : " + strFormatedValue);

                    this._lstrEventData.Add("Nome do Trabalhador : " + strUserName);
                    this._lstrEventData.Add("Tipo (UserType) : " + byUserType.ToString());
                    this._lstrEventData.Add("Modo de Marcação (Status) : " + byUserType.ToString());
                    this._lstrEventData.Add("Código (KeyCode) : " + uiKeyCode.ToString());
                    this._lstrEventData.Add("Senha (Password) : " + strPassword);
                    this._lstrEventData.Add("Proximidade (ProxCode) : " + usProxCode.ToString());
                    this._lstrEventData.Add("Barras (BarCode) : " + strBarCode);
                    this._lstrEventData.Add("Tamanho da Foto (SizePhoto) : 0");
                    this._lstrEventData.Add("Quant. Amostras (QuantitySamples) : " + byQuantitySamples.ToString());
                    this._lstrEventData.Add("Tamanho Amostras (SizeSample) : " + usSizeSample.ToString());

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool DeleteUser(string strPIS)
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.DeleteUser(strPIS);

                    if (this.SendBuffer(rgbyBuffer))
                    {
                        this._eCurrentCommand = ECommand.DeleteUser;
                        this._eCurrentConnectionState = EConnectionState.SendingData;

                        string strMask = "";
                        string strFormatedValue = "";

                        this._lstrEventData.Clear();
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("");

                        strMask = "###.#####.##-#";
                        this.FormatString(strPIS, strMask, ref strFormatedValue);
                        this._lstrEventData.Add("PIS : " + strFormatedValue);

                        return true;
                    }

                    return false;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool ReadUserData(string strPIS)
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.ReadUserData(strPIS);

                    if (this.SendBuffer(rgbyBuffer))
                    {
                        this._eCurrentCommand = ECommand.ReadUserData;
                        this._eCurrentConnectionState = EConnectionState.SendingData;

                        string strMask = "";
                        string strFormatedValue = "";

                        this._lstrEventData.Clear();
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("");

                        strMask = "###.#####.##-#";
                        this.FormatString(strPIS, strMask, ref strFormatedValue);
                        this._lstrEventData.Add("PIS : " + strFormatedValue);

                        return true;
                    }

                    return false;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool SetEmployer(byte byIdentifyType, string strCNPJ_CPF, string strCEI, string strEmployerName, string strEmployerAddress)
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.SetEmployer(byIdentifyType, strCNPJ_CPF, strCEI, strEmployerName, strEmployerAddress);

                    if (this.SendBuffer(rgbyBuffer))
                    {
                        this._eCurrentCommand = ECommand.SetEmployer;
                        this._eCurrentConnectionState = EConnectionState.SendingData;

                        string strMask = "";
                        string strFormatedValue = "";

                        this._lstrEventData.Clear();
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                        this._lstrEventData.Add("Dados da Empresa Enviados");
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("");

                        this._lstrEventData.Add("Identificador : " + byIdentifyType.ToString() + " (1 - CNPJ / 2 - CPF )");

                        if (byIdentifyType == 1)
                        {
                            strMask = "##.###.###/####-##";
                            this.FormatString(strCNPJ_CPF, strMask, ref strFormatedValue);
                            this._lstrEventData.Add("CNPJ : " + strFormatedValue);
                        }
                        else
                        {
                            strMask = "###.###.###-##";
                            this.FormatString(strCNPJ_CPF, strMask, ref strFormatedValue);
                            this._lstrEventData.Add("CPF : " + strFormatedValue);
                        }

                        this._lstrEventData.Add("CEI : " + strCEI);
                        this._lstrEventData.Add("Nome da Empresa/Empregador : " + strEmployerName.ToString());
                        this._lstrEventData.Add("Endereço da Empresa/Empregador : " + strEmployerAddress.ToString());

                        return true;
                    }

                    return false;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool ReadEmployerData()
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.ReadEmployerData();

                    if (this.SendBuffer(rgbyBuffer))
                    {
                        this._eCurrentCommand = ECommand.ReadEmployerData;
                        this._eCurrentConnectionState = EConnectionState.SendingData;

                        this._lstrEventData.Clear();
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("");

                        return true;
                    }

                    return false;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool SetDateTime(byte byDay, byte byMonth, ushort usYear, byte byHour, byte byMinute, byte bySecond)
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.SetDateTime(byDay, byMonth, usYear, byHour, byMinute, bySecond);

                    if (this.SendBuffer(rgbyBuffer))
                    {
                        this._eCurrentCommand = ECommand.SetDateTime;
                        this._eCurrentConnectionState = EConnectionState.SendingData;

                        this._lstrEventData.Clear();
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("");

                        return true;
                    }

                    return false;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool SetREPCommunication(byte byCommunicationType, string strIPEquipment, string strSubnetMask, string strIPGateway, ushort usTCPPort_Comm, ushort usTCPPort_Alarm, byte byBaudrate, byte bySerialAddress, byte byMulticastAddress, byte byBroadcastAddress)
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.SetREPCommunication(byCommunicationType, strIPEquipment, strSubnetMask, strIPGateway, usTCPPort_Comm, usTCPPort_Alarm, byBaudrate, bySerialAddress, byMulticastAddress, byBroadcastAddress);

                    if (this.SendBuffer(rgbyBuffer))
                    {
                        this._eCurrentCommand = ECommand.SetREPCommunication;
                        this._eCurrentConnectionState = EConnectionState.SendingData;

                        string strIPMode = "IP Fixo";

                        if (strIPEquipment == "0.0.0.0" || strIPEquipment == "")
                        {
                            strIPMode = "DHCP";
                        }

                        this._lstrEventData.Add("Obter Endereço IP : " + strIPMode);
                        this._lstrEventData.Add("Endereço IP : " + strIPEquipment);
                        this._lstrEventData.Add("Máscara de sub-rede : " + strSubnetMask);
                        this._lstrEventData.Add("Gateway padrão : " + strIPGateway);

                        this._lstrEventData.Add("Porta de Comunicação : " + usTCPPort_Comm.ToString());
                        this._lstrEventData.Add("");
                        this._lstrEventData.Add("Baudrate : " + byBaudrate.ToString());
                        this._lstrEventData.Add("Endreço Serial : " + bySerialAddress.ToString());
                        this._lstrEventData.Add("Endreço Multicast : " + byMulticastAddress.ToString());
                        this._lstrEventData.Add("Endreço Broadcast : " + byBroadcastAddress.ToString());

                        return true;
                    }

                    return false;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool ReadREPCommunication()
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.ReadREPCommunication();

                    if (this.SendBuffer(rgbyBuffer))
                    {
                        this._eCurrentCommand = ECommand.ReadREPCommunication;
                        this._eCurrentConnectionState = EConnectionState.SendingData;

                        this._lstrEventData.Clear();
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("");

                        return true;
                    }

                    return false;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool RequestEventByNSR(uint uiNSR)
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.RequestEventByNSR(uiNSR);

                    if (this.SendBuffer(rgbyBuffer))
                    {
                        this._eCurrentCommand = ECommand.RequestEventByNSR;
                        this._eCurrentConnectionState = EConnectionState.SendingData;

                        this._lstrEventData.Clear();
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("");
                        this._lstrEventData.Add("NSR : " + uiNSR.ToString());

                        return true;
                    }

                    return false;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool RequestNFR()
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.RequestNFR();

                    if (this.SendBuffer(rgbyBuffer))
                    {
                        this._eCurrentCommand = ECommand.RequestNFR;
                        this._eCurrentConnectionState = EConnectionState.SendingData;

                        this._lstrEventData.Clear();
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("");

                        return true;
                    }

                    return false;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool RequestTotalNSR()
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.RequestTotalNSR();

                    if (this.SendBuffer(rgbyBuffer))
                    {
                        this._eCurrentCommand = ECommand.RequestTotalNSR;
                        this._eCurrentConnectionState = EConnectionState.SendingData;

                        this._lstrEventData.Clear();
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("");

                        return true;
                    }

                    return false;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool RequestTotalUsers()
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.RequestTotalUsers();

                    if (this.SendBuffer(rgbyBuffer))
                    {
                        this._eCurrentCommand = ECommand.RequestTotalUsers;
                        this._eCurrentConnectionState = EConnectionState.SendingData;

                        this._lstrEventData.Clear();
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("");

                        return true;
                    }

                    return false;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool RequestUserByIndex(uint uiIndex)
            {
                try
                {
                    byte[] rgbyBuffer = this._objIDSysR30.RequestUserByIndex(uiIndex);

                    if (this.SendBuffer(rgbyBuffer))
                    {
                        this._eCurrentCommand = ECommand.ReadUserData;
                        this._eCurrentConnectionState = EConnectionState.SendingData;

                        this._lstrEventData.Clear();
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("Comando " + this._eCurrentCommand.ToString());
                        this._lstrEventData.Add("--------------------------------------------------");
                        this._lstrEventData.Add("");
                        this._lstrEventData.Add("Índice : " + uiIndex.ToString());

                        return true;
                    }

                    return false;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool GetUserData()
            {
                try
                {
                    string strPIS = "";
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

                    this._objIDSysR30.GetUserData(ref strPIS, ref strUserName, ref uiKeyCode, ref strBarCode, ref byFacilityCode, ref usProxCode, ref byUserType, ref strPassword, ref usSizePhoto, ref msPhoto, ref usSizeSample, ref byQuantitySamples, ref rgbyBiometric_Sample1, ref rgbyBiometric_Sample2);

                    string strMask = "";
                    string strFormatedValue = "";

                    this._lstrEventData.Add("Dados do Usuário");
                    this._lstrEventData.Add("");

                    strMask = "###.#####.##-#";
                    this.FormatString(strPIS, strMask, ref strFormatedValue);
                    this._lstrEventData.Add("PIS : " + strFormatedValue);

                    this._lstrEventData.Add("Nome do Trabalhador : " + strUserName);
                    this._lstrEventData.Add("Tipo (UserType) : " + byUserType.ToString());
                    this._lstrEventData.Add("Modo de Marcação (Status) : " + byUserType.ToString());
                    this._lstrEventData.Add("Código (KeyCode) : " + uiKeyCode.ToString());
                    this._lstrEventData.Add("Senha (Password) : " + strPassword.ToString());
                    this._lstrEventData.Add("Tamanho da Foto (SizePhoto) : " + usSizePhoto.ToString());
                    this._lstrEventData.Add("Quant. Amostras (QuantitySamples) : " + byQuantitySamples.ToString());
                    this._lstrEventData.Add("Tamanho Amostras (SizeSample) : " + usSizeSample.ToString());

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool GetEmployerData()
            {
                try
                {
                    byte byIdentifyType = 0;
                    string strCNPJ_CPF = "";
                    string strCEI = "";
                    string strEmployerName = "";
                    string strEmployerAddress = "";

                    this._objIDSysR30.GetEmployerData(ref byIdentifyType, ref strCNPJ_CPF, ref strCEI, ref strEmployerName, ref strEmployerAddress);

                    this._lstrEventData.Add("Dados da Empresa/Empregador");
                    this._lstrEventData.Add("");

                    this._lstrEventData.Add("Identificador : " + byIdentifyType.ToString() + " (1 - CNPJ / 2 - CPF )");

                    string strMask = "";
                    string strFormatedValue = "";

                    if (byIdentifyType == 1)
                    {
                        strMask = "##.###.###/####-##";
                        this.FormatString(strCNPJ_CPF, strMask, ref strFormatedValue);
                        this._lstrEventData.Add("CNPJ : " + strFormatedValue);
                    }
                    else
                    {
                        strMask = "###.###.###-##";
                        this.FormatString(strCNPJ_CPF, strMask, ref strFormatedValue);
                        this._lstrEventData.Add("CPF : " + strFormatedValue);
                    }

                    this._lstrEventData.Add("CEI : " + strCEI);
                    this._lstrEventData.Add("Nome da Empresa/Empregador : " + strEmployerName.ToString());
                    this._lstrEventData.Add("Endereço da Empresa/Empregador : " + strEmployerAddress.ToString());

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool GetLogType2()
            {
                try
                {
                    uint uiNSR = 0;
                    byte byRegType = 0;
                    byte byRegDateDay = 0;
                    byte byRegDateMonth = 0;
                    ushort usRegDateYear = 0;
                    byte byRegTimeHour = 0;
                    byte byRegTimeMin = 0;
                    byte byIdentifyType = 0;
                    string strCNPJ_CPF = "";
                    string strCEI = "";
                    string strEmployerName = "";
                    string strEmployerAddress = "";

                    this._objIDSysR30.GetLogType2(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref byIdentifyType, ref strCNPJ_CPF, ref strCEI, ref strEmployerName, ref strEmployerAddress);

                    string strMask = "";
                    string strFormatedValue = "";

                    this._lstrEventData.Add("Evento Tipo 2 (Inclusão/alteraçao dos dados do Empregador)");
                    this._lstrEventData.Add("");
                    this._lstrEventData.Add("NSR : " + uiNSR.ToString());
                    this._lstrEventData.Add("Tipo do Registro : " + byRegType.ToString());
                    this._lstrEventData.Add("Data do Registro : " + byRegDateDay.ToString("00") + "/" + byRegDateMonth.ToString("00") + "/" + usRegDateYear.ToString("0000"));
                    this._lstrEventData.Add("Hora do Registro : " + byRegTimeHour.ToString("00") + ":" + byRegTimeMin.ToString("00"));
                    this._lstrEventData.Add("Identificador : " + byIdentifyType.ToString() + " (1 - CNPJ / 2 - CPF )");

                    if (byIdentifyType == 1)
                    {
                        strMask = "##.###.###/####-##";
                        this.FormatString(strCNPJ_CPF, strMask, ref strFormatedValue);
                        this._lstrEventData.Add("CNPJ : " + strFormatedValue);
                    }
                    else
                    {
                        strMask = "###.###.###-##";
                        this.FormatString(strCNPJ_CPF, strMask, ref strFormatedValue);
                        this._lstrEventData.Add("CPF : " + strFormatedValue);
                    }

                    this._lstrEventData.Add("CEI : " + strCEI);
                    this._lstrEventData.Add("Nome da Empresa/Empregador : " + strEmployerName.ToString());
                    this._lstrEventData.Add("Endereço da Empresa/Empregador : " + strEmployerAddress.ToString());

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool GetLogType3()
            {
                try
                {
                    uint uiNSR = 0;
                    byte byRegType = 0;
                    byte byRegDateDay = 0;
                    byte byRegDateMonth = 0;
                    ushort usRegDateYear = 0;
                    byte byRegTimeHour = 0;
                    byte byRegTimeMin = 0;
                    string strPIS = "";

                    this._objIDSysR30.GetLogType3(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref strPIS);

                    string strMask = "";
                    string strFormatedValue = "";

                    this._lstrEventData.Add("Evento Tipo 3 (Marcação do Ponto)");
                    this._lstrEventData.Add("");
                    this._lstrEventData.Add("NSR : " + uiNSR.ToString());
                    this._lstrEventData.Add("Tipo do Registro : " + byRegType.ToString());
                    this._lstrEventData.Add("Data do Registro : " + byRegDateDay.ToString("00") + "/" + byRegDateMonth.ToString("00") + "/" + usRegDateYear.ToString("0000"));
                    this._lstrEventData.Add("Hora do Registro : " + byRegTimeHour.ToString("00") + ":" + byRegTimeMin.ToString("00"));

                    strMask = "###.#####.##-#";
                    this.FormatString(strPIS.ToString().PadLeft(11, '0'), strMask, ref strFormatedValue);
                    this._lstrEventData.Add("PIS : " + strFormatedValue);

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool GetLogType4()
            {
                try
                {
                    uint uiNSR = 0;
                    byte byRegType = 0;
                    byte byDayBeforeAdjust = 0;
                    byte byMonthBeforeAdjust = 0;
                    ushort usYearBeforeAdjust = 0;
                    byte byHourBeforeAdjust = 0;
                    byte byMinuteBeforeAdjust = 0;
                    byte byDayAfterAdjust = 0;
                    byte byMonthAfterAdjust = 0;
                    ushort usYearAfterAdjust = 0;
                    byte byHourAfterAdjust = 0;
                    byte byMinuteAfterAdjust = 0;

                    this._objIDSysR30.GetLogType4(ref uiNSR, ref byRegType, ref byDayBeforeAdjust, ref byMonthBeforeAdjust, ref usYearBeforeAdjust, ref byHourBeforeAdjust, ref byMinuteBeforeAdjust, ref byDayAfterAdjust, ref byMonthAfterAdjust, ref usYearAfterAdjust, ref byHourAfterAdjust, ref byMinuteAfterAdjust);

                    this._lstrEventData.Add("Evento Tipo 4 (Alteraçao de Data e Hora)");
                    this._lstrEventData.Add("");
                    this._lstrEventData.Add("NSR : " + uiNSR.ToString());
                    this._lstrEventData.Add("Tipo do Registro : " + byRegType.ToString());
                    this._lstrEventData.Add("Data Antes da Alteração : " + byDayBeforeAdjust.ToString("00") + "/" + byMonthBeforeAdjust.ToString("00") + "/" + byHourBeforeAdjust.ToString("0000"));
                    this._lstrEventData.Add("Hora Antes da Alteração : " + byHourBeforeAdjust.ToString("00") + ":" + byMinuteBeforeAdjust.ToString("00"));
                    this._lstrEventData.Add("Data Depois da Alteração : " + byDayAfterAdjust.ToString("00") + "/" + byMonthAfterAdjust.ToString("00") + "/" + usYearAfterAdjust.ToString("0000"));
                    this._lstrEventData.Add("Hora Depois da Alteração : " + byHourAfterAdjust.ToString("00") + ":" + byMinuteAfterAdjust.ToString("00"));

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool GetLogType5()
            {
                try
                {
                    uint uiNSR = 0;
                    byte byRegType = 0;
                    byte byRegDateDay = 0;
                    byte byRegDateMonth = 0;
                    ushort usRegDateYear = 0;
                    byte byRegTimeHour = 0;
                    byte byRegTimeMin = 0;
                    byte byOpType = 0;
                    string strPIS = "";
                    string strUsername = "";

                    this._objIDSysR30.GetLogType5(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref byOpType, ref strPIS, ref strUsername);

                    string strMask = "";
                    string strFormatedValue = "";

                    this._lstrEventData.Add("Evento Tipo 5 (Inclusão/alteraçao/exclusão de Usuário)");
                    this._lstrEventData.Add("");
                    this._lstrEventData.Add("NSR : " + uiNSR.ToString());
                    this._lstrEventData.Add("Tipo do Registro : " + byRegType.ToString());
                    this._lstrEventData.Add("Data do Registro : " + byRegDateDay.ToString("00") + "/" + byRegDateMonth.ToString("00") + "/" + usRegDateYear.ToString("0000"));
                    this._lstrEventData.Add("Hora do Registro : " + byRegTimeHour.ToString("00") + ":" + byRegTimeMin.ToString("00"));
                    this._lstrEventData.Add("Operação : " + (char)byOpType + " ( I - Inclusão / A - Alteração / E - Exclusão )");

                    strMask = "###.#####.##-#";
                    this.FormatString(strPIS, strMask, ref strFormatedValue);
                    this._lstrEventData.Add("PIS : " + strFormatedValue);

                    this._lstrEventData.Add("Nome do Trabalhador : " + strUsername);

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool GetREPCommunication()
            {
                try
                {
                    byte byCommunicationType = 0;
                    string strIPEquipment = "";
                    string strSubnetMask = "";
                    string strIPGateway = "";
                    ushort usTCPPort_Comm = 0;
                    ushort usTCPPort_Alarm = 0;
                    byte byBaudrate = 0;
                    byte bySerialAddress = 0;
                    byte byMulticastAddress = 0;
                    byte byBroadcastAddress = 0;

                    this._objIDSysR30.GetREPCommunication(ref byCommunicationType, ref strIPEquipment, ref strSubnetMask, ref strIPGateway, ref usTCPPort_Comm, ref usTCPPort_Alarm, ref byBaudrate, ref bySerialAddress, ref byMulticastAddress, ref byBroadcastAddress);

                    this._lstrEventData.Add("Dados da Configuração de Comunicação do Equipamento");
                    this._lstrEventData.Add("");

                    string strIPMode = "IP Fixo";

                    if (strIPEquipment == "0.0.0.0" || strIPEquipment == "")
                    {
                        strIPMode = "DHCP";
                    }

                    this._lstrEventData.Add("Obter Endereço IP : " + strIPMode);
                    this._lstrEventData.Add("Endereço IP : " + strIPEquipment);
                    this._lstrEventData.Add("Máscara de sub-rede : " + strSubnetMask);
                    this._lstrEventData.Add("Gateway padrão : " + strIPGateway);

                    this._lstrEventData.Add("Porta de Comunicação : " + usTCPPort_Comm.ToString());
                    this._lstrEventData.Add("");
                    this._lstrEventData.Add("Baudrate : " + byBaudrate.ToString());
                    this._lstrEventData.Add("Endreço Serial : " + bySerialAddress.ToString());
                    this._lstrEventData.Add("Endreço Multicast : " + byMulticastAddress.ToString());
                    this._lstrEventData.Add("Endreço Broadcast : " + byBroadcastAddress.ToString());

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool GetNFR()
            {
                try
                {
                    string strNFR = "";

                    this._objIDSysR30.GetNFR(ref strNFR);

                    this._lstrEventData.Add("NFR do Equipamento");
                    this._lstrEventData.Add("");

                    this._lstrEventData.Add("NFR : " + strNFR);

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool GetTotalNSR()
            {
                try
                {
                    uint uiTotalNSR = 0;

                    this._objIDSysR30.GetTotalNSR(ref uiTotalNSR);

                    this._lstrEventData.Add("Total de Eventos Registrados no Equipamento");
                    this._lstrEventData.Add("");

                    this._lstrEventData.Add("Total : " + uiTotalNSR.ToString());

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public bool GetTotalUsers()
            {
                try
                {
                    uint uiTotalUsers = 0;

                    this._objIDSysR30.GetTotalUsers(ref uiTotalUsers);

                    this._lstrEventData.Add("Total de Usuários Registrados no Equipamento");
                    this._lstrEventData.Add("");

                    this._lstrEventData.Add("Total : " + uiTotalUsers.ToString());

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            public void CommandOK()
            {
                this._lstrEventData.Add("");
                this._lstrEventData.Add("Resposta do comando " + this.GetCommand().ToString() + " recebida com sucesso.");
                this._lstrEventData.Add("");
            }

            public List<string> GetEventData()
            {
                return this._lstrEventData;
            }

        #endregion

        #region Other Functions

            public bool IsHamsterConnected()
            {
                return this._objIDSysR30.IsHamsterConnected();
            }

            public bool GetTemplates_FIM01(ref byte[] rgbySample404_1, ref byte byFingerPID_1, ref byte bySampleID_1, ref byte byQuality_1, ref byte[] rgbySample404_2, ref byte byFingerPID_2, ref byte bySampleID_2, ref byte byQuality_2, bool bRotateSamples)
            {
                if (this._objIDSysR30.GetTemplates_FIM01(ref rgbySample404_1, ref byFingerPID_1, ref bySampleID_1, ref byQuality_1, ref rgbySample404_2, ref byFingerPID_2, ref bySampleID_2, ref byQuality_2, bRotateSamples) == false)
                {
                    return false;
                }

                this._lstrEventData.Clear();
                this._lstrEventData.Add("Função GetTemplates_FIM01");
                this._lstrEventData.Add("");

                this._lstrEventData.Add("Amostra 1 - Template.....: OK");
                this._lstrEventData.Add("Amostra 1 - Dedo.........: " + byFingerPID_1.ToString());
                this._lstrEventData.Add("Amostra 1 - Qual amostra.: " + bySampleID_1.ToString());
                this._lstrEventData.Add("Amostra 1 - Qualidade....: " + bySampleID_1.ToString());
                this._lstrEventData.Add("Amostra 2 - Template.....: OK");
                this._lstrEventData.Add("Amostra 2 - Dedo.........: " + byFingerPID_2.ToString());
                this._lstrEventData.Add("Amostra 2 - Qual amostra.: " + bySampleID_2.ToString());
                this._lstrEventData.Add("Amostra 2 - Qualidade....: " + bySampleID_2.ToString());

                if (bRotateSamples)
                {
                    this._lstrEventData.Add("Rotacionar amostras......: Sim");
                }
                else
                {
                    this._lstrEventData.Add("Rotacionar amostras......: Não");
                }

                return true;
            }

            public bool GetTemplates_FIM10(ref byte[] rgbySample400_1, ref byte byFingerPID_1, ref byte bySampleID_1, ref byte byQuality_1, ref byte[] rgbySample400_2, ref byte byFingerPID_2, ref byte bySampleID_2, ref byte byQuality_2, bool bRotateSamples)
            {
                if (this._objIDSysR30.GetTemplates_FIM10(ref rgbySample400_1, ref byFingerPID_1, ref bySampleID_1, ref byQuality_1, ref rgbySample400_2, ref byFingerPID_2, ref bySampleID_2, ref byQuality_2, bRotateSamples) == false)
                {
                    return false;
                }

                this._lstrEventData.Clear();
                this._lstrEventData.Add("Função GetTemplates_FIM10");
                this._lstrEventData.Add("");

                this._lstrEventData.Add("Amostra 1 - Template.....: OK");
                this._lstrEventData.Add("Amostra 1 - Dedo.........: " + byFingerPID_1.ToString());
                this._lstrEventData.Add("Amostra 1 - Qual amostra.: " + bySampleID_1.ToString());
                this._lstrEventData.Add("Amostra 1 - Qualidade....: " + bySampleID_1.ToString());
                this._lstrEventData.Add("Amostra 2 - Template.....: OK");
                this._lstrEventData.Add("Amostra 2 - Dedo.........: " + byFingerPID_2.ToString());
                this._lstrEventData.Add("Amostra 2 - Qual amostra.: " + bySampleID_2.ToString());
                this._lstrEventData.Add("Amostra 2 - Qualidade....: " + bySampleID_2.ToString());
                
                if (bRotateSamples)
                {
                    this._lstrEventData.Add("Rotacionar amostras......: Sim");
                }
                else
                {
                    this._lstrEventData.Add("Rotacionar amostras......: Não");
                }
                return true;
            }

            public bool ConvertTemplate404ToTemplate400(byte[] rgbyTemplate404, ref byte[] rgbyTemplate400)
            {
                return this._objIDSysR30.ConvertTemplate404ToTemplate400(rgbyTemplate404, ref rgbyTemplate400);
            }

            public bool ConvertTemplate400ToTemplate404(byte[] rgbyTemplate400, ref byte[] rgbyTemplate404)
            {
                return this._objIDSysR30.ConvertTemplate400ToTemplate404(rgbyTemplate400, ref rgbyTemplate404);
            }

        #endregion

        public void AddLog(string Mensagem, bool NewLine = false)
            {
                if (Log.Text.Length > 0) Log.AppendText(Environment.NewLine);
                Log.AppendText(Mensagem.ToUpper());
                if (NewLine == true) Log.AppendText(Environment.NewLine);
            }


    }
}
