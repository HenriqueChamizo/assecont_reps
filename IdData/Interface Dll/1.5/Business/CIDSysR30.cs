using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

using InterfaceIDSysR30.Properties;

namespace iddata.interfaceIDSysR30
{
    public enum enumCommand
    {
        None = 0x00,
        AddUser = 0x06,
        ChangeUserData = 0x07,
        DeleteUser = 0x08,
        ReadUserData = 0x09,
        SetEmployer = 0x0A,
        ReadEmployerData = 0x0B,
        SetDateTime = 0x0E,
        SetREPCommunication = 0x12,
        ReadREPCommunication = 0x16,
        RequestEventByNSR = 0x19,
        RequestNFR = 0x1A,
        RequestTotalNSR = 0x20,
        RequestTotalUsers = 0x21,
        RequestUserByIndex = 0x22,
        RequestTemperature = 0x1B,
        DumpFW = 0xA5
    }

    public enum enumIdentify_Type
    {
        CNPJ = 1,
        CPF = 2
    }
}

namespace iddata.interfaceIDSysR30.business
{
    /// <summary>
    /// Ver descrição das funções e paramêtros no manual da DLL.
    /// </summary>
    public class CIDSysR30
    {
        #region const

            public const string DLL_PATH = "";

            private const byte PRODUCT_ID_REP = 1;

            private const ushort BUFFER_HEADER_SIZE = 15;
            private const ushort BUFFER_SIZE_REGTYPE_2 = 304;
            private const ushort BUFFER_SIZE_REGTYPE_3 = 38;
            private const ushort BUFFER_SIZE_REGTYPE_4 = 33;
            private const ushort BUFFER_SIZE_REGTYPE_5 = 91;
            private const ushort BUFFER_SIZE_CMD_ADD_USER = 112;
            private const ushort BUFFER_SIZE_CMD_CHANGE_USER_DATA = 134;
            private const ushort BUFFER_SIZE_CMD_DELETE_USER = 27;
            private const ushort BUFFER_SIZE_CMD_READ_USER_DATA = 27;
            private const ushort BUFFER_SIZE_CMD_SET_EMPLOYER = 293;
            private const ushort BUFFER_SIZE_CMD_READ_EMPLOYER_DATA = 15;
            private const ushort BUFFER_SIZE_CMD_SET_DATE_TIME = 23;
            private const ushort BUFFER_SIZE_CMD_SET_REP_COMMUNICATION = 37;
            private const ushort BUFFER_SIZE_CMD_READ_REP_COMMUNICATION = 15;
            private const ushort BUFFER_SIZE_CMD_READ_USER_DATA_BY_INDEX = 20;
            private const ushort BUFFER_SIZE_CMD_REQUEST_NFR = 15;
            private const ushort BUFFER_SIZE_CMD_REQUEST_EVENT_BY_NSR = 20;
            private const ushort BUFFER_SIZE_CMD_SET_BIOMETRIC_SECURITY = 17;
            private const ushort BUFFER_SIZE_CMD_READ_BIOMETRIC_SECURITY = 15;
            private const ushort BUFFER_SIZE_CMD_SET_DST = 20;
            private const ushort BUFFER_SIZE_CMD_READ_DST = 15;
            private const ushort BUFFER_SIZE_CMD_SET_PUNCH_TICKET_INFO = 17;
            private const ushort BUFFER_SIZE_CMD_READ_PUNCH_TICKET_INFO = 15;
            private const ushort BUFFER_SIZE_CMD_SET_CONNECTION_POOL_TIMEOUT = 20;
            private const ushort BUFFER_SIZE_CMD_READ_CONNECTION_POOL_TIMEOUT = 15;

            private const ushort BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL = 15;

            private const ushort BUFFER_SIZE_CMD_REQUEST_ALARMS_STATUS = 15;
            private const ushort BUFFER_SIZE_CMD_READ_ALARM_DATA = 17;
            private const ushort BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL= 20;
            private const ushort BUFFER_SIZE_CMD_CLEAR_ALARM_DATA = 17;
            private const ushort BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL = 20;

            private const byte CHARACTER_END_STRING = 0x00;
            private const byte BLANK_SPACE = 0x00;
            private const byte CHAR_0x20 = 0x20;
            private const char END_STRING = '\0';

            private const byte FIELD_SIZE_CNPJ = 14;
            private const byte FIELD_SIZE_CPF = 11;
            private const byte FIELD_SIZE_CEI = 12;
            private const byte FIELD_SIZE_EMPLOYER_NAME = 150;
            private const byte FIELD_SIZE_EMPLOYER_ADDRESS = 100;
            private const byte FIELD_SIZE_PIS = 11;
            private const byte FIELD_SIZE_USERNAME = 52;
            private const byte FIELD_SIZE_PASSWORD = 8;
            private const byte FIELD_SIZE_BARCODE = 20;
            private const byte FIELD_SIZE_IP_EQUIPMENT = 4;
            private const byte FIELD_SIZE_SUBNET_MASK = 4;
            private const byte FIELD_SIZE_IP_GATEWAY = 4;
            private const byte FIELD_SIZE_IP_SERVER = 4;
            private const byte FIELD_SIZE_NFR = 17;
            private const ushort MAX_SIZE_BIOMETRIC_SAMPLE = 404;
            private const uint MAX_SIZE_PHOTO = 30720;
            private const byte QUANTITY_ALARMS = 29;

            private const ushort UBS_FILE_SIZE_CNPJ_CPF = 14;
            private const byte UBS_FILE_SIZE_CEI = 12;
            private const byte UBS_FILE_SIZE_EMPLOYER_NAME = 150;
            private const byte UBS_FILE_SIZE_EMPLOYER_ADDRESS = 100;
            private const byte UBS_FILE_SIZE_NFR = 17;

            private const byte UBS_FILE_SIZE_PIS = 11;
            private const byte UBS_FILE_SIZE_USERNAME = 52;
            private const byte UBS_FILE_SIZE_BARCODE = 20;
            private const byte UBS_FILE_SIZE_PASSWORD = 8;
        
        #endregion
        
        #region native methods

            static class NativeMethods
            {
                [DllImport("kernel32.dll")]
                public static extern IntPtr LoadLibrary(string dllToLoad);
                [DllImport("kernel32.dll")]
                public static extern IntPtr GetProcAddress(IntPtr hModule, string PocedureName);
                [DllImport("kernel32.dll")]
                public static extern bool FreeLibrary(IntPtr hModule);
            }

        #endregion

        #region variables

            /// <summary>
            /// Criamos um ponteiro para a DLL
            /// </summary>
            private IntPtr ptrIDSysR30;

            private static CIDSysR30 objIDSysR30; 

        #endregion

        #region Platform

        public static string Platform
        {
            get
            {
                if (IntPtr.Size == 8)
                {
                    return "x64";
                }
                else
                {
                    return "x86";
                }
            }
        }

        #endregion

        #region constructor

            /// <summary>
            /// No construtor da classe, inicializamos o ponteiro para a DLL e para as funções
            /// </summary>
            private CIDSysR30()
            {
                if (this.LoadDLL() == false)
                {
                    throw new Exception("Não foi possível carregar a DLL IDSysR30.\nVerifique se o arquivo está na pasta do executavel ou se a versão está atualizada.");
                }

                try
                {
                    string strReturn = this.InitializeDLL();

                    if (strReturn != "")
                    {
                        throw new Exception("Erro ao carregar a função " + strReturn + " da DLL IDSysR30.");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Não foi possível carregar a DLL IDSysR30.\nVerifique se o arquivo está na pasta do executavel ou se a versão está atualizada.");
                }
            }

            /// <sumary>
            /// Singleton
            /// </sumary>
            public static CIDSysR30 GetInstance()
            {
                if (objIDSysR30 == null)
                {
                    objIDSysR30 = new CIDSysR30();
                }

                return objIDSysR30;
            }

        #endregion

        #region destructor

            /// <summary>
            /// No destrutor da classe, a DLL é liberada da memória
            /// </summary>
            ~CIDSysR30()
            {
                this.UnloadDLL();
            }

        #endregion

        #region access dll

            // 
            // Para cada função da DLL, criamos aqui um Delegate, que seria uma espécie de ponteiro para uma função.
            // Criamos em seguida uma variavel do tipo delegate definida logo acima.
            // 

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TAddUser(byte byAddress, byte byProduct, ref byte rgbyBuffer, ref byte rgbyPIS, ref byte rgbyUserName, uint uiKeyCode, ref byte rgbyBarCode, byte byFacilityCode, uint uiProxCode, byte byStatus, byte byUserType, ref byte rgbyPassword, ushort usPhotoSize, ref byte rgbyPhoto, ushort usBiometricsSize, byte byQuantitySamples, ref byte rgbyBiometrics);
            TAddUser FAddUser;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TChangeUserData(byte byAddress, byte byProduct, ref byte rgbyBuffer, ref byte rgbyPIS, ref byte rgbyNewPIS, ref byte rgbyUserName, ref byte rgbyBarCode, byte byFacilityCode, uint uiProxCode, byte byStatus, byte byUserType, ref byte rgbyPassword, ushort usPhotoSize, ref byte rgbyPhoto, ushort usBiometricsSize, byte byQuantitySamples, ref byte rgbyBiometrics, uint uiKeyCode);
            TChangeUserData FChangeUserData;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TDeleteUser(byte byAddress, byte byProduct, ref byte rgbyBuffer, ref byte rgbyPIS);
            TDeleteUser FDeleteUser;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadUserData(byte byAddress, byte byProduct, ref byte rgbyBuffer, ref byte rgbyPIS);
            TReadUserData FReadUserData;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TSetEmployer(byte byAddress, byte byProduct, ref byte rgbyBuffer, byte byIdentifyType, ref byte rgbyCNPJ_CPF, ref byte rgbyCEI, ref byte rgbyEmployerName, ref byte rgbyEmployerAddress);
            TSetEmployer FSetEmployer;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadEmployerData(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadEmployerData FReadEmployerData;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TSetDateTime(byte byAddress, byte byProduct, ref byte rgbyBuffer, byte byDay, byte byMonth, ushort usYear, byte byHour, byte byMinute, byte bySecond);
            TSetDateTime FSetDateTime;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TSetREPCommunication(byte byAddress, byte byProduct, ref byte rgbyBuffer, byte byCommunicationType, ref byte rgbyIPEquipment, ref byte rgbySubnetMask, ref byte rgbyIPGateway, uint uiTCPPort_Comm, uint uiTCPPort_Alarm, byte byBaudrate, byte bySerialAddress, byte byMulticastAddress, byte byBroadcastAddress);
            TSetREPCommunication FSetREPCommunication;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadREPCommunication(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadREPCommunication FReadREPCommunication;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestEventByNSR(byte byAddress, byte byProduct, ref byte rgbyBuffer, uint uiNSR);
            TRequestEventByNSR FRequestEventByNSR;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestNFR(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TRequestNFR FRequestNFR;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestUserByIndex(byte byAddress, byte byProduct, ref byte rgbyBuffer, uint uiIndex);
            TRequestUserByIndex FRequestUserByIndex;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TSetBiometricSecurity(byte byAddress, byte byProduct, ref byte rgbyBuffer, byte bySecurityLevel);
            TSetBiometricSecurity FSetBiometricSecurity;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadBiometricSecurity(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadBiometricSecurity FReadBiometricSecurity;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TSetDST(byte byAddress, byte byProduct, ref byte rgbyBuffer, byte byStartDay, byte byStartMonth, byte byEndDay, byte byEndMonth);
            TSetDST FSetDST;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadDST(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadDST FReadDST;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TSetPunchTicketInfo(byte byAddress, byte byProduct, ref byte rgbyBuffer, byte byPuchTicketInfo);
            TSetPunchTicketInfo FSetPunchTicketInfo;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadPunchTicketInfo(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadPunchTicketInfo FReadPunchTicketInfo;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TSetConnectionPoolTimeout(byte byAddress, byte byProduct, ref byte rgbyBuffer, int iPoolTimeout);
            TSetConnectionPoolTimeout FSetConnectionPoolTimeout;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadConnectionPoolTimeout(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadConnectionPoolTimeout FReadConnectionPoolTimeout;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestTemperature(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TRequestTemperature FRequestTemperature;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestBuzzerStatus(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestBuzzerStatus FRequestBuzzerStatus;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestCutterStatus(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TRequestCutterStatus FRequestCutterStatus;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestTotalPrinterTickets(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestTotalPrinterTickets FRequestTotalPrinterTickets;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestSystemVoltage(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TRequestSystemVoltage FRequestSystemVoltage;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestTotalNSR(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TRequestTotalNSR FRequestTotalNSR;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestTotalUsers(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestTotalUsers FRequestTotalUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestTotalCutterCuts(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TRequestTotalCutterCuts FRequestTotalCutterCuts;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestPrinterKM(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestPrinterKM FRequestPrinterKM;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestBiometricModuleSize(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TRequestBiometricModuleSize FRequestBiometricModuleSize;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestTotalBiometricUsers(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestTotalBiometricUsers FRequestTotalBiometricUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestPaperStatus(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestPaperStatus FRequestPaperStatus;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestTotalBarCardUsers(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestTotalBarCardUsers FRequestTotalBarCardUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestTotalProxCardUsers(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestTotalProxCardUsers FRequestTotalProxCardUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestTotalKeyCodeUsers(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestTotalKeyCodeUsers FRequestTotalKeyCodeUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestTotalPasswordUsers(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestTotalPasswordUsers FRequestTotalPasswordUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestTotalAdminUsers(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestTotalAdminUsers FRequestTotalAdminUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestCurrentPaperRollSize(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestCurrentPaperRollSize FRequestCurrentPaperRollSize;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestCurrentPaperRollKM(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestCurrentPaperRollKM FRequestCurrentPaperRollKM;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestCurrentPaperRollTicketsPrinted(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestCurrentPaperRollTicketsPrinted FRequestCurrentPaperRollTicketsPrinted;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestCurrentPaperRollEstimatedTickets(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestCurrentPaperRollEstimatedTickets FRequestCurrentPaperRollEstimatedTickets;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestAlarmsStatus(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TRequestAlarmsStatus FRequestAlarmsStatus;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData(byte byAddress, byte byProduct, ref byte rgbyBuffer, ref byte byAlarm);
            TReadAlarmData FReadAlarmData;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_Cutter(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_Cutter FReadAlarmData_Cutter;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_Buzzer(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_Buzzer FReadAlarmData_Buzzer;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_Temperature(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_Temperature FReadAlarmData_Temperature;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_USBFiscal(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_USBFiscal FReadAlarmData_USBFiscal;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_USBDados(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_USBDados FReadAlarmData_USBDados;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_NoPaper(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_NoPaper FReadAlarmData_NoPaper;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_GateOpened(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_GateOpened FReadAlarmData_GateOpened;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_24H_Pressed(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_24H_Pressed FReadAlarmData_24H_Pressed;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_24H_Emitted(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_24H_Emitted FReadAlarmData_24H_Emitted;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_MPR_75(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_MPR_75 FReadAlarmData_MPR_75;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_MPR_Full(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_MPR_Full FReadAlarmData_MPR_Full;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_MT_75(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_MT_75 FReadAlarmData_MT_75;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_MT_Full(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_MT_Full FReadAlarmData_MT_Full;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_BatteryCriticalLevel(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_BatteryCriticalLevel FReadAlarmData_BatteryCriticalLevel;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_BlockViolation(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_BlockViolation FReadAlarmData_BlockViolation;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_UnblockSuccess(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_UnblockSuccess FReadAlarmData_UnblockSuccess;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_UnblockTried(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_UnblockTried FReadAlarmData_UnblockTried;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_Cutter_75(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_Cutter_75 FReadAlarmData_Cutter_75;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_Cutter_Full(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_Cutter_Full FReadAlarmData_Cutter_Full;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_Printer_75(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_Printer_75 FReadAlarmData_Printer_75;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_Printer_Full(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_Printer_Full FReadAlarmData_Printer_Full;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_MasterPasswordChanged(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_MasterPasswordChanged FReadAlarmData_MasterPasswordChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_AdminReboot(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_AdminReboot FReadAlarmData_AdminReboot;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_CommunicationChanged(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_CommunicationChanged FReadAlarmData_CommunicationChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_WatchDogReboot(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_WatchDogReboot FReadAlarmData_WatchDogReboot;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_BiometricSecurityChanged(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_BiometricSecurityChanged FReadAlarmData_BiometricSecurityChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_PunchTicketInfoChanged(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_PunchTicketInfoChanged FReadAlarmData_PunchTicketInfoChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_PaperRollChanged(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_PaperRollChanged FReadAlarmData_PaperRollChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TReadAlarmData_PaperRoll_90(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TReadAlarmData_PaperRoll_90 FReadAlarmData_PaperRoll_90;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData(byte byAddress, byte byProduct, ref byte rgbyBuffer, ref byte byAlarm);
            TClearAlarmData FClearAlarmData;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_Cutter(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_Cutter FClearAlarmData_Cutter;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_Buzzer(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_Buzzer FClearAlarmData_Buzzer;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_Temperature(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_Temperature FClearAlarmData_Temperature;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_USBFiscal(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_USBFiscal FClearAlarmData_USBFiscal;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_USBDados(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_USBDados FClearAlarmData_USBDados;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_NoPaper(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_NoPaper FClearAlarmData_NoPaper;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_GateOpened(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_GateOpened FClearAlarmData_GateOpened;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_24H_Pressed(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_24H_Pressed FClearAlarmData_24H_Pressed;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_24H_Emitted(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_24H_Emitted FClearAlarmData_24H_Emitted;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_MPR_75(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_MPR_75 FClearAlarmData_MPR_75;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_MPR_Full(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_MPR_Full FClearAlarmData_MPR_Full;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_MT_75(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_MT_75 FClearAlarmData_MT_75;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_MT_Full(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_MT_Full FClearAlarmData_MT_Full;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_BatteryCriticalLevel(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_BatteryCriticalLevel FClearAlarmData_BatteryCriticalLevel;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_BlockViolation(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_BlockViolation FClearAlarmData_BlockViolation;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_UnblockSuccess(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_UnblockSuccess FClearAlarmData_UnblockSuccess;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_UnblockTried(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_UnblockTried FClearAlarmData_UnblockTried;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_Cutter_75(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_Cutter_75 FClearAlarmData_Cutter_75;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_Cutter_Full(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_Cutter_Full FClearAlarmData_Cutter_Full;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_Printer_75(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_Printer_75 FClearAlarmData_Printer_75;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_Printer_Full(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_Printer_Full FClearAlarmData_Printer_Full;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_MasterPasswordChanged(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_MasterPasswordChanged FClearAlarmData_MasterPasswordChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_AdminReboot(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_AdminReboot FClearAlarmData_AdminReboot;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_CommunicationChanged(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_CommunicationChanged FClearAlarmData_CommunicationChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_WatchDogReboot(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_WatchDogReboot FClearAlarmData_WatchDogReboot;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_BiometricSecurityChanged(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_BiometricSecurityChanged FClearAlarmData_BiometricSecurityChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_PunchTicketInfoChanged(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_PunchTicketInfoChanged FClearAlarmData_PunchTicketInfoChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_PaperRollChanged(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_PaperRollChanged FClearAlarmData_PaperRollChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TClearAlarmData_PaperRoll_90(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TClearAlarmData_PaperRoll_90 FClearAlarmData_PaperRoll_90;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TPacketAvail(ref byte Buffer);
            TPacketAvail FPacketAvail;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetCommand(ref byte byCommand);
            TGetCommand FGetCommand;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetUserData(ref byte rgbyPIS, ref byte rgbyUsername, ref uint uiKeyCode, ref byte rgbyBarCode, ref byte byFacilityCode, ref ulong ulProxCode, ref byte byStatus, ref byte byUserType, ref byte rgbyPassword, ref ushort usPhotoSize, ref byte rgbyPhoto, ref ushort usBiometricSize, ref byte byQuantitySamples, ref byte rgbyBiometric_Sample1, ref byte rgbyBiometric_Sample2);
            TGetUserData FGetUserData;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetEmployerData(ref byte byIdentifyType, ref byte rgbyCNPJ_CPF, ref byte rgbyCEI, ref byte rgbyEmployerName, ref byte rgbyEmployerAddress);
            TGetEmployerData FGetEmployerData;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetLogType2(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref byte byIdentifyType, ref byte rgbyCNPJ_CPF, ref byte rgbyCEI, ref byte rgbyEmployerName, ref byte rgbyEmployerAddress);
            TGetLogType2 FGetLogType2;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetLogType3(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref byte rgbyPIS);
            TGetLogType3 FGetLogType3;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetLogType4(ref uint uiNSR, ref byte byRegType, ref byte byDayBeforeAdjust, ref byte byMonthBeforeAdjust, ref ushort usYearBeforeAdjust, ref byte byHourBeforeAdjust, ref byte byMinuteBeforeAdjust, ref byte byDayAfterAdjust, ref byte byMonthAfterAdjust, ref ushort usYearAfterAdjust, ref byte byHourAfterAdjust, ref byte byMinuteAfterAdjust);
            TGetLogType4 FGetLogType4;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetLogType5(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref byte byOpType, ref byte rgbyPIS, ref byte rgbyUserName);
            TGetLogType5 FGetLogType5;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetLogType6(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref byte rgbyPIS, ref byte byEvent);
            TGetLogType6 FGetLogType6;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetREPCommunication(ref byte byCommunicationType, ref byte rgbyIPEquipment, ref byte rgbySubnetMask, ref byte rgbyIPGateway, ref ushort usTCPPort_Comm, ref ushort usTCPPort_Alarm, ref byte byBaudrate, ref byte bySerialAddress, ref byte byMulticastAddress, ref byte byBroadcastAddress);
            TGetREPCommunication FGetREPCommunication;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetNFR(ref byte rgbyNFR);
            TGetNFR FGetNFR;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetBiometricSecurity(ref byte bySecurityLevel);
            TGetBiometricSecurity FGetBiometricSecurity;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetDST(ref byte byStartDay, ref byte byStartMonth, ref byte byEndDay, ref byte byEndMonth);
            TGetDST FGetDST;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetPunchTicketInfo(ref int iPuchTicketInfo);
            TGetPunchTicketInfo FGetPunchTicketInfo;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetConnectionPoolTimeout(ref int iPoolTimeout);
            TGetConnectionPoolTimeout FGetConnectionPoolTimeout;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTemperature(ref int iTemperature);
            TGetTemperature FGetTemperature;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetBuzzerStatus(ref int iBuzzerStatus);
            TGetBuzzerStatus FGetBuzzerStatus;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetCutterStatus(ref int iCutterStatus);
            TGetCutterStatus FGetCutterStatus;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTotalPrinterTickets(ref int iTotalPrinterTickets);
            TGetTotalPrinterTickets FGetTotalPrinterTickets;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetSystemVoltage(ref int iSystemVoltage);
            TGetSystemVoltage FGetSystemVoltage;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTotalNSR(ref uint uiTotalNSR);
            TGetTotalNSR FGetTotalNSR;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTotalUsers(ref int iTotalUsers);
            TGetTotalUsers FGetTotalUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTotalCutterCuts(ref int iTotalCutterCuts);
            TGetTotalCutterCuts FGetTotalCutterCuts;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetPrinterKM(ref int iPrinterKM);
            TGetPrinterKM FGetPrinterKM;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetBiometricModuleSize(ref int iBiometricModuleSize);
            TGetBiometricModuleSize FGetBiometricModuleSize;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTotalBiometricUsers(ref int uiTotalBiometricUsers);
            TGetTotalBiometricUsers FGetTotalBiometricUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetPaperStatus(ref int uiPaperStatus);
            TGetPaperStatus FGetPaperStatus;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTotalBarCardUsers(ref int iTotalBarCardUsers);
            TGetTotalBarCardUsers FGetTotalBarCardUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTotalProxCardUsers(ref int iTotalProxCardUsers);
            TGetTotalProxCardUsers FGetTotalProxCardUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTotalKeyCodeUsers(ref int iTotalKeyCodeUsers);
            TGetTotalKeyCodeUsers FGetTotalKeyCodeUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTotalPasswordUsers(ref int iTotalPasswordUsers);
            TGetTotalPasswordUsers FGetTotalPasswordUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTotalAdminUsers(ref int iTotalAdminUsers);
            TGetTotalAdminUsers FGetTotalAdminUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetCurrentPaperRollSize(ref int iCurrentPaperRollSize);
            TGetCurrentPaperRollSize FGetCurrentPaperRollSize;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetCurrentPaperRollKM(ref int iCurrentPaperRollKM);
            TGetCurrentPaperRollKM FGetCurrentPaperRollKM;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetCurrentPaperRollTicketsPrinted(ref int iCurrentPaperRollTicketsPrinted);
            TGetCurrentPaperRollTicketsPrinted FGetCurrentPaperRollTicketsPrinted;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetCurrentPaperRollEstimatedTickets(ref int iCurrentPaperRollEstimatedTickets);
            TGetCurrentPaperRollEstimatedTickets FGetCurrentPaperRollEstimatedTickets;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmsStatus(ref byte rgbyAlarmsStatus);
            TGetAlarmsStatus FGetAlarmsStatus;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData FGetAlarmData;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_Cutter(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_Cutter FGetAlarmData_Cutter;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_Buzzer(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_Buzzer FGetAlarmData_Buzzer;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_Temperature(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_Temperature FGetAlarmData_Temperature;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_USBFiscal(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_USBFiscal FGetAlarmData_USBFiscal;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_USBDados(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_USBDados FGetAlarmData_USBDados;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_NoPaper(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_NoPaper FGetAlarmData_NoPaper;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_GateOpened(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_GateOpened FGetAlarmData_GateOpened;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_24H_Pressed(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_24H_Pressed FGetAlarmData_24H_Pressed;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_24H_Emitted(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_24H_Emitted FGetAlarmData_24H_Emitted;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_MPR_75(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_MPR_75 FGetAlarmData_MPR_75;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_MPR_Full(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_MPR_Full FGetAlarmData_MPR_Full;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_MT_75(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_MT_75 FGetAlarmData_MT_75;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_MT_Full(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_MT_Full FGetAlarmData_MT_Full;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_BatteryCriticalLevel(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_BatteryCriticalLevel FGetAlarmData_BatteryCriticalLevel;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_BlockViolation(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_BlockViolation FGetAlarmData_BlockViolation;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_UnblockSuccess(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_UnblockSuccess FGetAlarmData_UnblockSuccess;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_UnblockTried(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_UnblockTried FGetAlarmData_UnblockTried;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_Cutter_75(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_Cutter_75 FGetAlarmData_Cutter_75;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_Cutter_Full(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_Cutter_Full FGetAlarmData_Cutter_Full;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_Printer_75(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_Printer_75 FGetAlarmData_Printer_75;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_Printer_Full(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_Printer_Full FGetAlarmData_Printer_Full;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_MasterPasswordChanged(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_MasterPasswordChanged FGetAlarmData_MasterPasswordChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_AdminReboot(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_AdminReboot FGetAlarmData_AdminReboot;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_CommunicationChanged(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_CommunicationChanged FGetAlarmData_CommunicationChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_WatchDogReboot(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_WatchDogReboot FGetAlarmData_WatchDogReboot;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_BiometricSecurityChanged(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_BiometricSecurityChanged FGetAlarmData_BiometricSecurityChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_PunchTicketInfoChanged(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_PunchTicketInfoChanged FGetAlarmData_PunchTicketInfoChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_PaperRollChanged(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_PaperRollChanged FGetAlarmData_PaperRollChanged;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAlarmData_PaperRoll_90(ref byte byAlarmStatus, ref byte byAlarmDate_Day, ref byte byAlarmDate_Month, ref ushort usAlarmDate_Year, ref byte byAlarmTime_Hour, ref byte byAlarmTime_Min, ref byte byAlarmTime_Sec);
            TGetAlarmData_PaperRoll_90 FGetAlarmData_PaperRoll_90;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate bool TIsHamsterConnected();
            TIsHamsterConnected FIsHamsterConnected;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TGetHamsterType();
            TGetHamsterType FGetHamsterType;
            

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate bool TGetTemplates_FIM01(ref byte rgbySample404_1, ref byte byFingerPID_1, ref byte bySampleID_1, ref byte byQuality_1, ref byte rgbySample404_2, ref byte byFingerPID_2, ref byte bySampleID_2, ref byte byQuality_2, byte byRotateSamples);
            TGetTemplates_FIM01 FGetTemplates_FIM01;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate bool TGetTemplates_FIM10(ref byte rgbySample400_1, ref byte byFingerPID_1, ref byte bySampleID_1, ref byte byQuality_1, ref byte rgbySample400_2, ref byte byFingerPID_2, ref byte bySampleID_2, ref byte byQuality_2, byte byRotateSamples);
            TGetTemplates_FIM10 FGetTemplates_FIM10;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate bool TGetTemplates_Virdi(ref byte byFingerPID, ref byte rgbySample400_1, ref byte rgbySample400_2);
            TGetTemplates_Virdi FGetTemplates_Virdi;            

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TFIM01_To_FIM10(ref byte rgbyTemplate404, ref byte rgbyTemplate400);
            TFIM01_To_FIM10 FFIM01_To_FIM10;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TFIM10_To_FIM01(ref byte rgbyTemplate400, ref byte rgbyTemplate404);
            TFIM10_To_FIM01 FFIM10_To_FIM01;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TCreateUBSFile(ref byte rgbyUBSFilePath, uint uiQuantityUsers, ref byte rgbyEmployerName, ref byte rgbyCNPJ_CPF, ref byte rgbyCEI, ref byte rgbyEmployerAddress, ref byte rgbyNFR);
            TCreateUBSFile FCreateUBSFile;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TOpenUBSFile(ref byte rgbyUBSFilePath, ref uint uiQuantityUsers, ref byte rgbyEmployerName, ref byte rgbyCNPJ_CPF, ref byte rgbyCEI, ref byte rgbyEmployerAddress, ref byte rgbyNFR);
            TOpenUBSFile FOpenUBSFile;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TEncryptBuffer(ref byte rgbyBuffer, uint uiSize);
            TEncryptBuffer FEncryptBuffer;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TDecryptBuffer(ref byte rgbyBuffer, uint uiSize);
            TDecryptBuffer FDecryptBuffer;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TExportUBS_User(ref byte rgbyPIS, ref byte rgbyUserName, uint uiKeyCode, ref byte rgbyBarCode, uint ulProxCode, byte byStatus, ref byte rgbyPassword, byte byAccessType, ref byte rgbyBiometrics, uint uiSizeSample);
            TExportUBS_User FExportUBS_User;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TImportUBS_User(ref byte rgbyPIS, ref byte rgbyUserName, ref uint uiKeyCode, ref byte rgbyBarCode, ref uint ulProxCode, ref byte byUserType, ref byte rgbyPassword, ref byte rgbyBiometrics, ref uint uiSizeSample, ref byte byAccessType);
            TImportUBS_User FImportUBS_User;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TCloseUBSFile();
            TCloseUBSFile FCloseUBSFile;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TOpenAFDFile(ref byte rgbyAFDFilePath);
            TOpenAFDFile FOpenAFDFile;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TImportAFD_Register();
            TImportAFD_Register FImportAFD_Register;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAFD_Header(ref uint uiNSR, ref byte byRegType, ref byte byIdentifyType, ref byte rgbyCNPJ_CPF, ref byte rgbyCEI, ref byte rgbyEmployerName, ref byte rgbyNFR, ref byte byStartDateDay, ref byte byStartDateMonth, ref ushort usStartDateYear, ref byte byEndDateDay, ref byte byEndDateMonth, ref ushort usEndDateYear, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin);
            TGetAFD_Header FGetAFD_Header;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetAFD_Trailer(ref uint uiNSR, ref byte byRegType, ref uint uiQuantityRegType2, ref uint uiQuantityRegType3, ref uint uiQuantityRegType4, ref uint uiQuantityRegType5);
            TGetAFD_Trailer FGetAFD_Trailer;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TCloseAFDFile();
            TCloseAFDFile FCloseAFDFile;

        #endregion

        #region load dll

            private bool LoadDLL()
            {
                try
                {
                    this.ptrIDSysR30 = IntPtr.Zero;

                    if (Platform == "x64")
                    {
                        this.ptrIDSysR30 = NativeMethods.LoadLibrary("IDSysR31.dll");
                    }
                    else
                    {
                        this.ptrIDSysR30 = NativeMethods.LoadLibrary("IDSysR30.dll");
                    }                    

                    if (this.ptrIDSysR30 == IntPtr.Zero)
                    {
                        return false;
                    }

                    return true;
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

        #endregion

        #region initialize dll

            public string InitializeDLL()
            {
                string strReturn = "";

                if ((this.FAddUser = (TAddUser)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "AddUser"), typeof(TAddUser))) == null) return "AddUser";
                if ((this.FChangeUserData = (TChangeUserData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ChangeUserData"), typeof(TChangeUserData))) == null) return "ChangeUserData";
                if ((this.FDeleteUser = (TDeleteUser)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "DeleteUser"), typeof(TDeleteUser))) == null) return "DeleteUser";
                if ((this.FReadUserData = (TReadUserData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadUserData"), typeof(TReadUserData))) == null) return "ReadUserData";
                if ((this.FSetEmployer = (TSetEmployer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "SetEmployer"), typeof(TSetEmployer))) == null) return "SetEmployer";
                if ((this.FReadEmployerData = (TReadEmployerData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadEmployerData"), typeof(TReadEmployerData))) == null) return "ReadEmployerData";
                if ((this.FSetDateTime = (TSetDateTime)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "SetDateTime"), typeof(TSetDateTime))) == null) return "SetDateTime";
                if ((this.FSetREPCommunication = (TSetREPCommunication)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "SetREPCommunication"), typeof(TSetREPCommunication))) == null) return "SetREPCommunication";
                if ((this.FReadREPCommunication = (TReadREPCommunication)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadREPCommunication"), typeof(TReadREPCommunication))) == null) return "ReadREPCommunication";
                if ((this.FRequestEventByNSR = (TRequestEventByNSR)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestEventByNSR"), typeof(TRequestEventByNSR))) == null) return "RequestEventByNSR";
                if ((this.FRequestNFR = (TRequestNFR)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestNFR"), typeof(TRequestNFR))) == null) return "RequestNFR";
                if ((this.FRequestUserByIndex = (TRequestUserByIndex)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestUserByIndex"), typeof(TRequestUserByIndex))) == null) return "RequestUserByIndex";
                if ((this.FSetBiometricSecurity = (TSetBiometricSecurity)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "SetBiometricSecurity"), typeof(TSetBiometricSecurity))) == null) return "SetBiometricSecurity";
                if ((this.FReadBiometricSecurity = (TReadBiometricSecurity)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadBiometricSecurity"), typeof(TReadBiometricSecurity))) == null) return "ReadBiometricSecurity";
                if ((this.FSetDST = (TSetDST)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "SetDST"), typeof(TSetDST))) == null) return "SetDST";
                if ((this.FReadDST = (TReadDST)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadDST"), typeof(TReadDST))) == null) return "ReadDST";
                if ((this.FSetPunchTicketInfo = (TSetPunchTicketInfo)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "SetPunchTicketInfo"), typeof(TSetPunchTicketInfo))) == null) return "SetPunchTicketInfo";
                if ((this.FReadPunchTicketInfo = (TReadPunchTicketInfo)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadPunchTicketInfo"), typeof(TReadPunchTicketInfo))) == null) return "ReadPunchTicketInfo";
                if ((this.FSetConnectionPoolTimeout = (TSetConnectionPoolTimeout)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "SetConnectionPoolTimeout"), typeof(TSetConnectionPoolTimeout))) == null) return "SetConnectionPoolTimeout";
                if ((this.FReadConnectionPoolTimeout = (TReadConnectionPoolTimeout)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadConnectionPoolTimeout"), typeof(TReadConnectionPoolTimeout))) == null) return "ReadConnectionPoolTimeout";

                if ((this.FRequestTemperature = (TRequestTemperature)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestTemperature"), typeof(TRequestTemperature))) == null) return "RequestTemperature";
                if ((this.FRequestBuzzerStatus = (TRequestBuzzerStatus)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestBuzzerStatus"), typeof(TRequestBuzzerStatus))) == null) return "RequestBuzzerStatus";
                if ((this.FRequestCutterStatus = (TRequestCutterStatus)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestCutterStatus"), typeof(TRequestCutterStatus))) == null) return "RequestCutterStatus";
                if ((this.FRequestTotalPrinterTickets = (TRequestTotalPrinterTickets)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestTotalPrinterTickets"), typeof(TRequestTotalPrinterTickets))) == null) return "RequestTotalPrinterTickets";
                if ((this.FRequestSystemVoltage = (TRequestSystemVoltage)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestSystemVoltage"), typeof(TRequestSystemVoltage))) == null) return "RequestSystemVoltage";
                if ((this.FRequestTotalNSR = (TRequestTotalNSR)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestTotalNSR"), typeof(TRequestTotalNSR))) == null) return "RequestTotalNSR";
                if ((this.FRequestTotalUsers = (TRequestTotalUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestTotalUsers"), typeof(TRequestTotalUsers))) == null) return "RequestTotalUsers";
                if ((this.FRequestTotalCutterCuts = (TRequestTotalCutterCuts)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestTotalCutterCuts"), typeof(TRequestTotalCutterCuts))) == null) return "RequestTotalCutterCuts";
                if ((this.FRequestPrinterKM = (TRequestPrinterKM)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestPrinterKM"), typeof(TRequestPrinterKM))) == null) return "RequestPrinterKM";
                if ((this.FRequestBiometricModuleSize = (TRequestBiometricModuleSize)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestBiometricModuleSize"), typeof(TRequestBiometricModuleSize))) == null) return "RequestBiometricModuleSize";
                if ((this.FRequestTotalBiometricUsers = (TRequestTotalBiometricUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestTotalBiometricUsers"), typeof(TRequestTotalBiometricUsers))) == null) return "RequestTotalBiometricUsers";
                if ((this.FRequestPaperStatus = (TRequestPaperStatus)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestPaperStatus"), typeof(TRequestPaperStatus))) == null) return "RequestPaperStatus";
                if ((this.FRequestTotalBarCardUsers = (TRequestTotalBarCardUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestTotalBarCardUsers"), typeof(TRequestTotalBarCardUsers))) == null) return "RequestTotalBarCardUsers";
                if ((this.FRequestTotalProxCardUsers = (TRequestTotalProxCardUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestTotalProxCardUsers"), typeof(TRequestTotalProxCardUsers))) == null) return "RequestTotalProxCardUsers";
                if ((this.FRequestTotalKeyCodeUsers = (TRequestTotalKeyCodeUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestTotalKeyCodeUsers"), typeof(TRequestTotalKeyCodeUsers))) == null) return "RequestTotalKeyCodeUsers";
                if ((this.FRequestTotalPasswordUsers = (TRequestTotalPasswordUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestTotalPasswordUsers"), typeof(TRequestTotalPasswordUsers))) == null) return "RequestTotalPasswordUsers";
                if ((this.FRequestTotalAdminUsers = (TRequestTotalAdminUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestTotalAdminUsers"), typeof(TRequestTotalAdminUsers))) == null) return "RequestTotalAdminUsers";
                if ((this.FRequestCurrentPaperRollSize = (TRequestCurrentPaperRollSize)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestCurrentPaperRollSize"), typeof(TRequestCurrentPaperRollSize))) == null) return "RequestCurrentPaperRollSize";
                if ((this.FRequestCurrentPaperRollKM = (TRequestCurrentPaperRollKM)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestCurrentPaperRollKM"), typeof(TRequestCurrentPaperRollKM))) == null) return "RequestCurrentPaperRollKM";
                if ((this.FRequestCurrentPaperRollTicketsPrinted = (TRequestCurrentPaperRollTicketsPrinted)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestCurrentPaperRollTicketsPrinted"), typeof(TRequestCurrentPaperRollTicketsPrinted))) == null) return "RequestCurrentPaperRollTicketsPrinted";
                if ((this.FRequestCurrentPaperRollEstimatedTickets = (TRequestCurrentPaperRollEstimatedTickets)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestCurrentPaperRollEstimatedTickets"), typeof(TRequestCurrentPaperRollEstimatedTickets))) == null) return "RequestCurrentPaperRollEstimatedTickets";

                if ((this.FRequestAlarmsStatus = (TRequestAlarmsStatus)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "RequestAlarmsStatus"), typeof(TRequestAlarmsStatus))) == null) return "RequestAlarmsStatus";
                if ((this.FReadAlarmData = (TReadAlarmData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData"), typeof(TReadAlarmData))) == null) return "ReadAlarmData";
                if ((this.FReadAlarmData_Cutter = (TReadAlarmData_Cutter)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_Cutter"), typeof(TReadAlarmData_Cutter))) == null) return "ReadAlarmData_Cutter";
                if ((this.FReadAlarmData_Buzzer = (TReadAlarmData_Buzzer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_Buzzer"), typeof(TReadAlarmData_Buzzer))) == null) return "ReadAlarmData_Buzzer";
                if ((this.FReadAlarmData_Temperature = (TReadAlarmData_Temperature)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_Temperature"), typeof(TReadAlarmData_Temperature))) == null) return "ReadAlarmData_Temperature";
                if ((this.FReadAlarmData_USBFiscal = (TReadAlarmData_USBFiscal)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_USBFiscal"), typeof(TReadAlarmData_USBFiscal))) == null) return "ReadAlarmData_USBFiscal";
                if ((this.FReadAlarmData_USBDados = (TReadAlarmData_USBDados)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_USBDados"), typeof(TReadAlarmData_USBDados))) == null) return "ReadAlarmData_USBDados";
                if ((this.FReadAlarmData_NoPaper = (TReadAlarmData_NoPaper)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_NoPaper"), typeof(TReadAlarmData_NoPaper))) == null) return "ReadAlarmData_NoPaper";
                if ((this.FReadAlarmData_GateOpened = (TReadAlarmData_GateOpened)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_GateOpened"), typeof(TReadAlarmData_GateOpened))) == null) return "ReadAlarmData_GateOpened";
                if ((this.FReadAlarmData_24H_Pressed = (TReadAlarmData_24H_Pressed)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_24H_Pressed"), typeof(TReadAlarmData_24H_Pressed))) == null) return "ReadAlarmData_24H_Pressed";
                if ((this.FReadAlarmData_24H_Emitted = (TReadAlarmData_24H_Emitted)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_24H_Emitted"), typeof(TReadAlarmData_24H_Emitted))) == null) return "ReadAlarmData_24H_Emitted";
                if ((this.FReadAlarmData_MPR_75 = (TReadAlarmData_MPR_75)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_MPR_75"), typeof(TReadAlarmData_MPR_75))) == null) return "ReadAlarmData_MPR_75";
                if ((this.FReadAlarmData_MPR_Full = (TReadAlarmData_MPR_Full)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_MPR_Full"), typeof(TReadAlarmData_MPR_Full))) == null) return "ReadAlarmData_MPR_Full";
                if ((this.FReadAlarmData_MT_75 = (TReadAlarmData_MT_75)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_MT_75"), typeof(TReadAlarmData_MT_75))) == null) return "ReadAlarmData_MT_75";
                if ((this.FReadAlarmData_MT_Full = (TReadAlarmData_MT_Full)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_MT_Full"), typeof(TReadAlarmData_MT_Full))) == null) return "ReadAlarmData_MT_Full";
                if ((this.FReadAlarmData_BatteryCriticalLevel = (TReadAlarmData_BatteryCriticalLevel)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_BatteryCriticalLevel"), typeof(TReadAlarmData_BatteryCriticalLevel))) == null) return "ReadAlarmData_BatteryCriticalLevel";
                if ((this.FReadAlarmData_BlockViolation = (TReadAlarmData_BlockViolation)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_BlockViolation"), typeof(TReadAlarmData_BlockViolation))) == null) return "ReadAlarmData_BlockViolation";
                if ((this.FReadAlarmData_UnblockSuccess = (TReadAlarmData_UnblockSuccess)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_UnblockSuccess"), typeof(TReadAlarmData_UnblockSuccess))) == null) return "ReadAlarmData_UnblockSuccess";
                if ((this.FReadAlarmData_UnblockTried = (TReadAlarmData_UnblockTried)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_UnblockTried"), typeof(TReadAlarmData_UnblockTried))) == null) return "ReadAlarmData_UnblockTried";
                if ((this.FReadAlarmData_Cutter_75 = (TReadAlarmData_Cutter_75)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_Cutter_75"), typeof(TReadAlarmData_Cutter_75))) == null) return "ReadAlarmData_Cutter_75";
                if ((this.FReadAlarmData_Cutter_Full = (TReadAlarmData_Cutter_Full)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_Cutter_Full"), typeof(TReadAlarmData_Cutter_Full))) == null) return "ReadAlarmData_Cutter_Full";
                if ((this.FReadAlarmData_Printer_75 = (TReadAlarmData_Printer_75)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_Printer_75"), typeof(TReadAlarmData_Printer_75))) == null) return "ReadAlarmData_Printer_75";
                if ((this.FReadAlarmData_Printer_Full = (TReadAlarmData_Printer_Full)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_Printer_Full"), typeof(TReadAlarmData_Printer_Full))) == null) return "ReadAlarmData_Printer_Full";
                if ((this.FReadAlarmData_MasterPasswordChanged = (TReadAlarmData_MasterPasswordChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_MasterPasswordChanged"), typeof(TReadAlarmData_MasterPasswordChanged))) == null) return "ReadAlarmData_MasterPasswordChanged";
                if ((this.FReadAlarmData_AdminReboot = (TReadAlarmData_AdminReboot)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_AdminReboot"), typeof(TReadAlarmData_AdminReboot))) == null) return "ReadAlarmData_AdminReboot";
                if ((this.FReadAlarmData_CommunicationChanged = (TReadAlarmData_CommunicationChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_CommunicationChanged"), typeof(TReadAlarmData_CommunicationChanged))) == null) return "ReadAlarmData_CommunicationChanged";
                if ((this.FReadAlarmData_WatchDogReboot = (TReadAlarmData_WatchDogReboot)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_WatchDogReboot"), typeof(TReadAlarmData_WatchDogReboot))) == null) return "ReadAlarmData_WatchDogReboot";
                if ((this.FReadAlarmData_BiometricSecurityChanged = (TReadAlarmData_BiometricSecurityChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_BiometricSecurityChanged"), typeof(TReadAlarmData_BiometricSecurityChanged))) == null) return "ReadAlarmData_BiometricSecurityChanged";
                if ((this.FReadAlarmData_PunchTicketInfoChanged = (TReadAlarmData_PunchTicketInfoChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_PunchTicketInfoChanged"), typeof(TReadAlarmData_PunchTicketInfoChanged))) == null) return "ReadAlarmData_PunchTicketInfoChanged";
                if ((this.FReadAlarmData_PaperRollChanged = (TReadAlarmData_PaperRollChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_PaperRollChanged"), typeof(TReadAlarmData_PaperRollChanged))) == null) return "ReadAlarmData_PaperRollChanged";
                if ((this.FReadAlarmData_PaperRoll_90 = (TReadAlarmData_PaperRoll_90)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ReadAlarmData_PaperRoll_90"), typeof(TReadAlarmData_PaperRoll_90))) == null) return "ReadAlarmData_PaperRoll_90";
                
                if ((this.FClearAlarmData = (TClearAlarmData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData"), typeof(TClearAlarmData))) == null) return "ClearAlarmData";
                if ((this.FClearAlarmData_Cutter = (TClearAlarmData_Cutter)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_Cutter"), typeof(TClearAlarmData_Cutter))) == null) return "ClearAlarmData_Cutter";
                if ((this.FClearAlarmData_Buzzer = (TClearAlarmData_Buzzer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_Buzzer"), typeof(TClearAlarmData_Buzzer))) == null) return "ClearAlarmData_Buzzer";
                if ((this.FClearAlarmData_Temperature = (TClearAlarmData_Temperature)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_Temperature"), typeof(TClearAlarmData_Temperature))) == null) return "ClearAlarmData_Temperature";
                if ((this.FClearAlarmData_USBFiscal = (TClearAlarmData_USBFiscal)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_USBFiscal"), typeof(TClearAlarmData_USBFiscal))) == null) return "ClearAlarmData_USBFiscal";
                if ((this.FClearAlarmData_USBDados = (TClearAlarmData_USBDados)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_USBDados"), typeof(TClearAlarmData_USBDados))) == null) return "ClearAlarmData_USBDados";
                if ((this.FClearAlarmData_NoPaper = (TClearAlarmData_NoPaper)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_NoPaper"), typeof(TClearAlarmData_NoPaper))) == null) return "ClearAlarmData_NoPaper";
                if ((this.FClearAlarmData_GateOpened = (TClearAlarmData_GateOpened)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_GateOpened"), typeof(TClearAlarmData_GateOpened))) == null) return "ClearAlarmData_GateOpened";
                if ((this.FClearAlarmData_24H_Pressed = (TClearAlarmData_24H_Pressed)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_24H_Pressed"), typeof(TClearAlarmData_24H_Pressed))) == null) return "ClearAlarmData_24H_Pressed";
                if ((this.FClearAlarmData_24H_Emitted = (TClearAlarmData_24H_Emitted)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_24H_Emitted"), typeof(TClearAlarmData_24H_Emitted))) == null) return "ClearAlarmData_24H_Emitted";
                if ((this.FClearAlarmData_MPR_75 = (TClearAlarmData_MPR_75)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_MPR_75"), typeof(TClearAlarmData_MPR_75))) == null) return "ClearAlarmData_MPR_75";
                if ((this.FClearAlarmData_MPR_Full = (TClearAlarmData_MPR_Full)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_MPR_Full"), typeof(TClearAlarmData_MPR_Full))) == null) return "ClearAlarmData_MPR_Full";
                if ((this.FClearAlarmData_MT_75 = (TClearAlarmData_MT_75)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_MT_75"), typeof(TClearAlarmData_MT_75))) == null) return "ClearAlarmData_MT_75";
                if ((this.FClearAlarmData_MT_Full = (TClearAlarmData_MT_Full)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_MT_Full"), typeof(TClearAlarmData_MT_Full))) == null) return "ClearAlarmData_MT_Full";
                if ((this.FClearAlarmData_BatteryCriticalLevel = (TClearAlarmData_BatteryCriticalLevel)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_BatteryCriticalLevel"), typeof(TClearAlarmData_BatteryCriticalLevel))) == null) return "ClearAlarmData_BatteryCriticalLevel";
                if ((this.FClearAlarmData_BlockViolation = (TClearAlarmData_BlockViolation)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_BlockViolation"), typeof(TClearAlarmData_BlockViolation))) == null) return "ClearAlarmData_BlockViolation";
                if ((this.FClearAlarmData_UnblockSuccess = (TClearAlarmData_UnblockSuccess)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_UnblockSuccess"), typeof(TClearAlarmData_UnblockSuccess))) == null) return "ClearAlarmData_UnblockSuccess";
                if ((this.FClearAlarmData_UnblockTried = (TClearAlarmData_UnblockTried)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_UnblockTried"), typeof(TClearAlarmData_UnblockTried))) == null) return "ClearAlarmData_UnblockTried";
                if ((this.FClearAlarmData_Cutter_75 = (TClearAlarmData_Cutter_75)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_Cutter_75"), typeof(TClearAlarmData_Cutter_75))) == null) return "ClearAlarmData_Cutter_75";
                if ((this.FClearAlarmData_Cutter_Full = (TClearAlarmData_Cutter_Full)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_Cutter_Full"), typeof(TClearAlarmData_Cutter_Full))) == null) return "ClearAlarmData_Cutter_Full";
                if ((this.FClearAlarmData_Printer_75 = (TClearAlarmData_Printer_75)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_Printer_75"), typeof(TClearAlarmData_Printer_75))) == null) return "ClearAlarmData_Printer_75";
                if ((this.FClearAlarmData_Printer_Full = (TClearAlarmData_Printer_Full)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_Printer_Full"), typeof(TClearAlarmData_Printer_Full))) == null) return "ClearAlarmData_Printer_Full";
                if ((this.FClearAlarmData_MasterPasswordChanged = (TClearAlarmData_MasterPasswordChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_MasterPasswordChanged"), typeof(TClearAlarmData_MasterPasswordChanged))) == null) return "ClearAlarmData_MasterPasswordChanged";
                if ((this.FClearAlarmData_AdminReboot = (TClearAlarmData_AdminReboot)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_AdminReboot"), typeof(TClearAlarmData_AdminReboot))) == null) return "ClearAlarmData_AdminReboot";
                if ((this.FClearAlarmData_CommunicationChanged = (TClearAlarmData_CommunicationChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_CommunicationChanged"), typeof(TClearAlarmData_CommunicationChanged))) == null) return "ClearAlarmData_CommunicationChanged";
                if ((this.FClearAlarmData_WatchDogReboot = (TClearAlarmData_WatchDogReboot)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_WatchDogReboot"), typeof(TClearAlarmData_WatchDogReboot))) == null) return "ClearAlarmData_WatchDogReboot";
                if ((this.FClearAlarmData_BiometricSecurityChanged = (TClearAlarmData_BiometricSecurityChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_BiometricSecurityChanged"), typeof(TClearAlarmData_BiometricSecurityChanged))) == null) return "ClearAlarmData_BiometricSecurityChanged";
                if ((this.FClearAlarmData_PunchTicketInfoChanged = (TClearAlarmData_PunchTicketInfoChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_PunchTicketInfoChanged"), typeof(TClearAlarmData_PunchTicketInfoChanged))) == null) return "ClearAlarmData_PunchTicketInfoChanged";
                if ((this.FClearAlarmData_PaperRollChanged = (TClearAlarmData_PaperRollChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_PaperRollChanged"), typeof(TClearAlarmData_PaperRollChanged))) == null) return "ClearAlarmData_PaperRollChanged";
                if ((this.FClearAlarmData_PaperRoll_90 = (TClearAlarmData_PaperRoll_90)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ClearAlarmData_PaperRoll_90"), typeof(TClearAlarmData_PaperRoll_90))) == null) return "ClearAlarmData_PaperRoll_90";
                    
                if ((this.FPacketAvail = (TPacketAvail)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "PacketAvail"), typeof(TPacketAvail))) == null) return "PacketAvail";

                if ((this.FGetCommand = (TGetCommand)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetCommand"), typeof(TGetCommand))) == null) return "GetCommand";                           
                if ((this.FGetUserData = (TGetUserData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetUserData"), typeof(TGetUserData))) == null) return "GetUserData";
                if ((this.FGetEmployerData = (TGetEmployerData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetEmployerData"), typeof(TGetEmployerData))) == null) return "GetEmployerData";
                if ((this.FGetLogType2 = (TGetLogType2)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetLogType2"), typeof(TGetLogType2))) == null) return "GetLogType2";
                if ((this.FGetLogType3 = (TGetLogType3)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetLogType3"), typeof(TGetLogType3))) == null) return "GetLogType3";
                if ((this.FGetLogType4 = (TGetLogType4)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetLogType4"), typeof(TGetLogType4))) == null) return "GetLogType4";
                if ((this.FGetLogType5 = (TGetLogType5)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetLogType5"), typeof(TGetLogType5))) == null) return "GetLogType5";
                if ((this.FGetLogType6 = (TGetLogType6)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetLogType6"), typeof(TGetLogType6))) == null) return "GetLogType6";
                if ((this.FGetREPCommunication = (TGetREPCommunication)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetREPCommunication"), typeof(TGetREPCommunication))) == null) return "GetREPCommunication";
                if ((this.FGetNFR = (TGetNFR)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetNFR"), typeof(TGetNFR))) == null) return "GetNFR";
                if ((this.FGetBiometricSecurity = (TGetBiometricSecurity)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetBiometricSecurity"), typeof(TGetBiometricSecurity))) == null) return "GetBiometricSecurity";
                if ((this.FGetDST = (TGetDST)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetDST"), typeof(TGetDST))) == null) return "GetDST";
                if ((this.FGetPunchTicketInfo = (TGetPunchTicketInfo)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetPunchTicketInfo"), typeof(TGetPunchTicketInfo))) == null) return "GetPunchTicketInfo";
                if ((this.FGetConnectionPoolTimeout = (TGetConnectionPoolTimeout)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetConnectionPoolTimeout"), typeof(TGetConnectionPoolTimeout))) == null) return "GetConnectionPoolTimeout";

                if ((this.FGetTemperature = (TGetTemperature)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTemperature"), typeof(TGetTemperature))) == null) return "GetTemperature";
                if ((this.FGetBuzzerStatus = (TGetBuzzerStatus)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetBuzzerStatus"), typeof(TGetBuzzerStatus))) == null) return "GetBuzzerStatus";
                if ((this.FGetCutterStatus = (TGetCutterStatus)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetCutterStatus"), typeof(TGetCutterStatus))) == null) return "GetCutterStatus";
                if ((this.FGetTotalPrinterTickets = (TGetTotalPrinterTickets)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTotalPrinterTickets"), typeof(TGetTotalPrinterTickets))) == null) return "GetTotalPrinterTickets";
                if ((this.FGetSystemVoltage = (TGetSystemVoltage)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetSystemVoltage"), typeof(TGetSystemVoltage))) == null) return "GetSystemVoltage";
                if ((this.FGetTotalNSR = (TGetTotalNSR)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTotalNSR"), typeof(TGetTotalNSR))) == null) return "GetTotalNSR";
                if ((this.FGetTotalUsers = (TGetTotalUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTotalUsers"), typeof(TGetTotalUsers))) == null) return "GetTotalUsers";
                if ((this.FGetTotalCutterCuts = (TGetTotalCutterCuts)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTotalCutterCuts"), typeof(TGetTotalCutterCuts))) == null) return "GetTotalCutterCuts";
                if ((this.FGetPrinterKM = (TGetPrinterKM)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetPrinterKM"), typeof(TGetPrinterKM))) == null) return "GetPrinterKM";
                if ((this.FGetBiometricModuleSize = (TGetBiometricModuleSize)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetBiometricModuleSize"), typeof(TGetBiometricModuleSize))) == null) return "GetBiometricModuleSize";
                if ((this.FGetTotalBiometricUsers = (TGetTotalBiometricUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTotalBiometricUsers"), typeof(TGetTotalBiometricUsers))) == null) return "GetTotalBiometricUsers";
                if ((this.FGetPaperStatus = (TGetPaperStatus)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetPaperStatus"), typeof(TGetPaperStatus))) == null) return "GetPaperStatus";
                if ((this.FGetTotalBarCardUsers = (TGetTotalBarCardUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTotalBarCardUsers"), typeof(TGetTotalBarCardUsers))) == null) return "GetTotalBarCardUsers";
                if ((this.FGetTotalProxCardUsers = (TGetTotalProxCardUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTotalProxCardUsers"), typeof(TGetTotalProxCardUsers))) == null) return "GetTotalProxCardUsers";
                if ((this.FGetTotalKeyCodeUsers = (TGetTotalKeyCodeUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTotalKeyCodeUsers"), typeof(TGetTotalKeyCodeUsers))) == null) return "GetTotalKeyCodeUsers";
                if ((this.FGetTotalPasswordUsers = (TGetTotalPasswordUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTotalPasswordUsers"), typeof(TGetTotalPasswordUsers))) == null) return "GetTotalPasswordUsers";
                if ((this.FGetTotalAdminUsers = (TGetTotalAdminUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTotalAdminUsers"), typeof(TGetTotalAdminUsers))) == null) return "GetTotalAdminUsers";
                if ((this.FGetCurrentPaperRollSize = (TGetCurrentPaperRollSize)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetCurrentPaperRollSize"), typeof(TGetCurrentPaperRollSize))) == null) return "GetCurrentPaperRollSize";
                if ((this.FGetCurrentPaperRollKM = (TGetCurrentPaperRollKM)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetCurrentPaperRollKM"), typeof(TGetCurrentPaperRollKM))) == null) return "GetCurrentPaperRollKM";
                if ((this.FGetCurrentPaperRollTicketsPrinted = (TGetCurrentPaperRollTicketsPrinted)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetCurrentPaperRollTicketsPrinted"), typeof(TGetCurrentPaperRollTicketsPrinted))) == null) return "GetCurrentPaperRollTicketsPrinted";
                if ((this.FGetCurrentPaperRollEstimatedTickets = (TGetCurrentPaperRollEstimatedTickets)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetCurrentPaperRollEstimatedTickets"), typeof(TGetCurrentPaperRollEstimatedTickets))) == null) return "GetCurrentPaperRollEstimatedTickets";

                if ((this.FGetAlarmsStatus = (TGetAlarmsStatus)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmsStatus"), typeof(TGetAlarmsStatus))) == null) return "GetAlarmsStatus";
                if ((this.FGetAlarmData = (TGetAlarmData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData"), typeof(TGetAlarmData))) == null) return "GetAlarmData";
                if ((this.FGetAlarmData_Cutter = (TGetAlarmData_Cutter)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_Cutter"), typeof(TGetAlarmData_Cutter))) == null) return "GetAlarmData_Cutter";
                if ((this.FGetAlarmData_Buzzer = (TGetAlarmData_Buzzer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_Buzzer"), typeof(TGetAlarmData_Buzzer))) == null) return "GetAlarmData_Buzzer";
                if ((this.FGetAlarmData_Temperature = (TGetAlarmData_Temperature)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_Temperature"), typeof(TGetAlarmData_Temperature))) == null) return "GetAlarmData_Temperature";
                if ((this.FGetAlarmData_USBFiscal = (TGetAlarmData_USBFiscal)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_USBFiscal"), typeof(TGetAlarmData_USBFiscal))) == null) return "GetAlarmData_USBFiscal";
                if ((this.FGetAlarmData_USBDados = (TGetAlarmData_USBDados)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_USBDados"), typeof(TGetAlarmData_USBDados))) == null) return "GetAlarmData_USBDados";
                if ((this.FGetAlarmData_NoPaper = (TGetAlarmData_NoPaper)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_NoPaper"), typeof(TGetAlarmData_NoPaper))) == null) return "GetAlarmData_NoPaper";
                if ((this.FGetAlarmData_GateOpened = (TGetAlarmData_GateOpened)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_GateOpened"), typeof(TGetAlarmData_GateOpened))) == null) return "GetAlarmData_GateOpened";
                if ((this.FGetAlarmData_24H_Pressed = (TGetAlarmData_24H_Pressed)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_24H_Pressed"), typeof(TGetAlarmData_24H_Pressed))) == null) return "GetAlarmData_24H_Pressed";
                if ((this.FGetAlarmData_24H_Emitted = (TGetAlarmData_24H_Emitted)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_24H_Emitted"), typeof(TGetAlarmData_24H_Emitted))) == null) return "GetAlarmData_24H_Emitted";
                if ((this.FGetAlarmData_MPR_75 = (TGetAlarmData_MPR_75)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_MPR_75"), typeof(TGetAlarmData_MPR_75))) == null) return "GetAlarmData_MPR_75";
                if ((this.FGetAlarmData_MPR_Full = (TGetAlarmData_MPR_Full)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_MPR_Full"), typeof(TGetAlarmData_MPR_Full))) == null) return "GetAlarmData_MPR_Full";
                if ((this.FGetAlarmData_MT_75 = (TGetAlarmData_MT_75)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_MT_75"), typeof(TGetAlarmData_MT_75))) == null) return "GetAlarmData_MT_75";
                if ((this.FGetAlarmData_MT_Full = (TGetAlarmData_MT_Full)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_MT_Full"), typeof(TGetAlarmData_MT_Full))) == null) return "GetAlarmData_MT_Full";
                if ((this.FGetAlarmData_BatteryCriticalLevel = (TGetAlarmData_BatteryCriticalLevel)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_BatteryCriticalLevel"), typeof(TGetAlarmData_BatteryCriticalLevel))) == null) return "GetAlarmData_BatteryCriticalLevel";
                if ((this.FGetAlarmData_BlockViolation = (TGetAlarmData_BlockViolation)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_BlockViolation"), typeof(TGetAlarmData_BlockViolation))) == null) return "GetAlarmData_BlockViolation";
                if ((this.FGetAlarmData_UnblockSuccess = (TGetAlarmData_UnblockSuccess)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_UnblockSuccess"), typeof(TGetAlarmData_UnblockSuccess))) == null) return "GetAlarmData_UnblockSuccess";
                if ((this.FGetAlarmData_UnblockTried = (TGetAlarmData_UnblockTried)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_UnblockTried"), typeof(TGetAlarmData_UnblockTried))) == null) return "GetAlarmData_UnblockTried";
                if ((this.FGetAlarmData_Cutter_75 = (TGetAlarmData_Cutter_75)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_Cutter_75"), typeof(TGetAlarmData_Cutter_75))) == null) return "GetAlarmData_Cutter_75";
                if ((this.FGetAlarmData_Cutter_Full = (TGetAlarmData_Cutter_Full)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_Cutter_Full"), typeof(TGetAlarmData_Cutter_Full))) == null) return "GetAlarmData_Cutter_Full";
                if ((this.FGetAlarmData_Printer_75 = (TGetAlarmData_Printer_75)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_Printer_75"), typeof(TGetAlarmData_Printer_75))) == null) return "GetAlarmData_Printer_75";
                if ((this.FGetAlarmData_Printer_Full = (TGetAlarmData_Printer_Full)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_Printer_Full"), typeof(TGetAlarmData_Printer_Full))) == null) return "GetAlarmData_Printer_Full";
                if ((this.FGetAlarmData_MasterPasswordChanged = (TGetAlarmData_MasterPasswordChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_MasterPasswordChanged"), typeof(TGetAlarmData_MasterPasswordChanged))) == null) return "GetAlarmData_MasterPasswordChanged";
                if ((this.FGetAlarmData_AdminReboot = (TGetAlarmData_AdminReboot)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_AdminReboot"), typeof(TGetAlarmData_AdminReboot))) == null) return "GetAlarmData_AdminReboot";
                if ((this.FGetAlarmData_CommunicationChanged = (TGetAlarmData_CommunicationChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_CommunicationChanged"), typeof(TGetAlarmData_CommunicationChanged))) == null) return "GetAlarmData_CommunicationChanged";
                if ((this.FGetAlarmData_WatchDogReboot = (TGetAlarmData_WatchDogReboot)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_WatchDogReboot"), typeof(TGetAlarmData_WatchDogReboot))) == null) return "GetAlarmData_WatchDogReboot";
                if ((this.FGetAlarmData_BiometricSecurityChanged = (TGetAlarmData_BiometricSecurityChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_BiometricSecurityChanged"), typeof(TGetAlarmData_BiometricSecurityChanged))) == null) return "GetAlarmData_BiometricSecurityChanged";
                if ((this.FGetAlarmData_PunchTicketInfoChanged = (TGetAlarmData_PunchTicketInfoChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_PunchTicketInfoChanged"), typeof(TGetAlarmData_PunchTicketInfoChanged))) == null) return "GetAlarmData_PunchTicketInfoChanged";
                if ((this.FGetAlarmData_PaperRollChanged = (TGetAlarmData_PaperRollChanged)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_PaperRollChanged"), typeof(TGetAlarmData_PaperRollChanged))) == null) return "GetAlarmData_PaperRollChanged";
                if ((this.FGetAlarmData_PaperRoll_90 = (TGetAlarmData_PaperRoll_90)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAlarmData_PaperRoll_90"), typeof(TGetAlarmData_PaperRoll_90))) == null) return "GetAlarmData_PaperRoll_90";

                if ((this.FIsHamsterConnected = (TIsHamsterConnected)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "IsHamsterConnected"), typeof(TIsHamsterConnected))) == null) return "IsHamsterConnected";
                if ((this.FGetHamsterType = (TGetHamsterType)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetHamsterType"), typeof(TGetHamsterType))) == null) return "GetHamsterType";
                if ((this.FGetTemplates_FIM01 = (TGetTemplates_FIM01)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTemplates_FIM01"), typeof(TGetTemplates_FIM01))) == null) return "GetTemplates_FIM01";
                if ((this.FGetTemplates_FIM10 = (TGetTemplates_FIM10)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTemplates_FIM10"), typeof(TGetTemplates_FIM10))) == null) return "GetTemplates_FIM10";
                if ((this.FGetTemplates_Virdi = (TGetTemplates_Virdi)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetTemplates_Virdi"), typeof(TGetTemplates_Virdi))) == null) return "GetTemplates_Virdi";
                

                if ((this.FFIM01_To_FIM10 = (TFIM01_To_FIM10)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "FIM01_To_FIM10"), typeof(TFIM01_To_FIM10))) == null) return "FIM01_To_FIM10";
                if ((this.FFIM10_To_FIM01 = (TFIM10_To_FIM01)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "FIM10_To_FIM01"), typeof(TFIM10_To_FIM01))) == null) return "FIM10_To_FIM01";

                if ((this.FCreateUBSFile = (TCreateUBSFile)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "CreateUBSFile"), typeof(TCreateUBSFile))) == null) return "CreateUBSFile";
                if ((this.FOpenUBSFile = (TOpenUBSFile)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "OpenUBSFile"), typeof(TOpenUBSFile))) == null) return "OpenUBSFile";
                if ((this.FExportUBS_User = (TExportUBS_User)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ExportUBS_User"), typeof(TExportUBS_User))) == null) return "ExportUBS_User";
                if ((this.FImportUBS_User = (TImportUBS_User)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ImportUBS_User"), typeof(TImportUBS_User))) == null) return "ImportUBS_User";
                if ((this.FCloseUBSFile = (TCloseUBSFile)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "CloseUBSFile"), typeof(TCloseUBSFile))) == null) return "CloseUBSFile";
                    
                if ((this.FEncryptBuffer = (TEncryptBuffer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "EncryptBuffer"), typeof(TEncryptBuffer))) == null) return "EncryptBuffer";
                if ((this.FDecryptBuffer = (TDecryptBuffer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "DecryptBuffer"), typeof(TDecryptBuffer))) == null) return "DecryptBuffer";
                    
                if ((this.FOpenAFDFile = (TOpenAFDFile)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "OpenAFDFile"), typeof(TOpenAFDFile))) == null) return "OpenAFDFile";
                if ((this.FImportAFD_Register = (TImportAFD_Register)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "ImportAFD_Register"), typeof(TImportAFD_Register))) == null) return "ImportAFD_Register";
                if ((this.FGetAFD_Header = (TGetAFD_Header)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAFD_Header"), typeof(TGetAFD_Header))) == null) return "GetAFD_Header";
                if ((this.FGetAFD_Trailer = (TGetAFD_Trailer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "GetAFD_Trailer"), typeof(TGetAFD_Trailer))) == null) return "GetAFD_Trailer";
                if ((this.FCloseAFDFile = (TCloseAFDFile)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(ptrIDSysR30, "CloseAFDFile"), typeof(TCloseAFDFile))) == null) return "CloseAFDFile";

                return strReturn;
            }

        #endregion

        #region unload dll

            public bool UnloadDLL()
            {
                try
                {
                    return NativeMethods.FreeLibrary(this.ptrIDSysR30);
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

        #endregion

        #region functions

            /// <summary>
            /// Monta o buffer com o comando que solicita a inclusão de um novo usuário no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] AddUser(string strPIS, string strUserName, uint uiKeyCode, string strBarCode, byte byFacilityCode, ulong ulProxCode, byte byUserType, int iAccessType, string strPassword, System.IO.MemoryStream msPhoto, ushort usSizeSample, byte byQuantitySamples, byte[] rgbyBiometrics)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer;
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];
                byte[] rgbyUsername = new byte[FIELD_SIZE_USERNAME];
                byte[] rgbyBarCode = new byte[FIELD_SIZE_BARCODE];
                byte[] rgbyPassword = new byte[FIELD_SIZE_PASSWORD];
                ushort usSizePhoto = 0;
                byte[] rgbyPhoto = new byte[MAX_SIZE_PHOTO];
                byte byAux = 1;

                strPIS = strPIS.Trim();

                for (int iIdx = 0; iIdx < rgbyPIS.Length; iIdx++)
                {
                    if (iIdx < strPIS.ToString().Length)
                    {
                        rgbyPIS[iIdx] = (byte)strPIS.ToString()[iIdx];
                    }
                    else
                    {
                        rgbyPIS[iIdx] = (byte)END_STRING;
                    }
                }

                strUserName = strUserName.Trim();

                for (int iIdx = 0; iIdx < rgbyUsername.Length; iIdx++)
                {
                    if (iIdx < strUserName.Length)
                    {
                        rgbyUsername[iIdx] = (byte)strUserName[iIdx];
                    }
                    else
                    {
                        rgbyUsername[iIdx] = (byte)END_STRING;
                    }
                }

                if (strBarCode != null)
                {
                    strBarCode = strBarCode.PadLeft(rgbyBarCode.Length, '0');

                    for (int iIdx = 0; iIdx < strBarCode.Length; iIdx++)
                    {
                        rgbyBarCode[iIdx] = (byte)strBarCode[iIdx];
                    }
                }
                else
                {
                    for (int iIdx = 0; iIdx < rgbyBarCode.Length; iIdx++)
                    {
                        rgbyBarCode[iIdx] = (byte)END_STRING;
                    }
                }

                usSizePhoto = 0;
                Array.Resize(ref rgbyPhoto, 1);

                if (byQuantitySamples == 0 || usSizeSample == 0)
                {
                    byQuantitySamples = 0;
                    usSizeSample = 0;
                    byAux = 0;
                    Array.Resize(ref rgbyBiometrics, 1);
                }

                rgbyBuffer = new byte[BUFFER_SIZE_CMD_ADD_USER + usSizePhoto + (byQuantitySamples * usSizeSample) + byAux];

                // invertendo tipo de usuário e status
                byte byAccessType = (byte)iAccessType;

                strPassword = strPassword.Trim();

                for (int iIdx = 0; iIdx < rgbyPassword.Length; iIdx++)
                {
                    if (iIdx < strPassword.Length)
                    {
                        rgbyPassword[iIdx] = (byte)strPassword[iIdx];
                    }
                    else
                    {
                        rgbyPassword[iIdx] = (byte)END_STRING;
                    }
                }

                uint uiProx = (uint)ulProxCode;

                //this.FAddUser(byAddress, byProduct, ref rgbyBuffer[0], ref rgbyPIS[0], ref rgbyUsername[0], uiKeyCode, ref rgbyBarCode[0], byFacilityCode, uiProx, byUserType, byAccessType, ref rgbyPassword[0], usSizePhoto, ref rgbyPhoto[0], usSizeSample, byQuantitySamples, ref rgbyBiometrics[0]);
                this.FAddUser(byAddress, byProduct, ref rgbyBuffer[0], ref rgbyPIS[0], ref rgbyUsername[0], uiKeyCode, ref rgbyBarCode[0], byFacilityCode, uiProx, byUserType, byAccessType, ref rgbyPassword[0], 0x00, ref rgbyPhoto[0], usSizeSample, byQuantitySamples, ref rgbyBiometrics[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a alteração de um usuário no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ChangeUserData(string strPIS, string strNewPIS, string strUserName, uint uiKeyCode, string strBarCode, byte byFacilityCode, ulong ulProxCode, byte byUserType, int iAccessType, string strPassword, System.IO.MemoryStream msPhoto, ushort usSizeSample, byte byQuantitySamples, byte[] rgbyBiometrics)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer;
                byte byAux = 1;
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];
                byte[] rgbyNewPIS = new byte[FIELD_SIZE_PIS];
                byte[] rgbyUserName = new byte[FIELD_SIZE_USERNAME];
                byte[] rgbyBarCode = new byte[FIELD_SIZE_BARCODE];
                byte[] rgbyPassword = new byte[FIELD_SIZE_PASSWORD];
                ushort usSizePhoto;
                byte[] rgbyPhoto = new byte[1];

                strPIS = strPIS.Trim();

                for (int iIdx = 0; iIdx < rgbyPIS.Length; iIdx++)
                {
                    if (iIdx < strPIS.ToString().Length)
                    {
                        rgbyPIS[iIdx] = (byte)strPIS.ToString()[iIdx];
                    }
                    else
                    {
                        rgbyPIS[iIdx] = (byte)END_STRING;
                    }
                }

                strNewPIS = strNewPIS.Trim();

                for (int iIdx = 0; iIdx < rgbyNewPIS.Length; iIdx++)
                {
                    if (iIdx < strNewPIS.ToString().Length)
                    {
                        rgbyNewPIS[iIdx] = (byte)strNewPIS.ToString()[iIdx];
                    }
                    else
                    {
                        rgbyNewPIS[iIdx] = (byte)END_STRING;
                    }
                }

                strUserName = strUserName.Trim();

                for (int iIdx = 0; iIdx < rgbyUserName.Length; iIdx++)
                {
                    if (iIdx < strUserName.Length)
                    {
                        rgbyUserName[iIdx] = (byte)strUserName[iIdx];
                    }
                    else
                    {
                        rgbyUserName[iIdx] = (byte)END_STRING;
                    }
                }

                if (strBarCode != null)
                {
                    strBarCode = strBarCode.PadLeft(rgbyBarCode.Length, '0');

                    for (int iIdx = 0; iIdx < strBarCode.Length; iIdx++)
                    {
                        rgbyBarCode[iIdx] = (byte)strBarCode[iIdx];
                    }
                }
                else
                {
                    for (int iIdx = 0; iIdx < rgbyBarCode.Length; iIdx++)
                    {
                        rgbyBarCode[iIdx] = (byte)END_STRING;
                    }
                }

                usSizePhoto = 0;
                Array.Resize(ref rgbyPhoto, 1);

                if (byQuantitySamples == 0 || usSizeSample == 0)
                {
                    byQuantitySamples = 0;
                    usSizeSample = 0;
                    byAux = 0;
                    Array.Resize(ref rgbyBiometrics, 1);
                }

                rgbyBuffer = new byte[BUFFER_SIZE_CMD_CHANGE_USER_DATA + usSizePhoto + (byQuantitySamples * usSizeSample) + byAux];

                // invertendo tipo de usuário e status
                byte byAccessType = (byte)iAccessType;

                strPassword = strPassword.Trim();

                for (int iIdx = 0; iIdx < rgbyPassword.Length; iIdx++)
                {
                    if (iIdx < strPassword.Length)
                    {
                        rgbyPassword[iIdx] = (byte)strPassword[iIdx];
                    }
                    else
                    {
                        rgbyPassword[iIdx] = (byte)END_STRING;
                    }
                }

                uint uiProx = (uint)ulProxCode;

                this.FChangeUserData(byAddress, byProduct, ref rgbyBuffer[0], ref rgbyPIS[0], ref rgbyNewPIS[0], ref rgbyUserName[0], ref rgbyBarCode[0], byFacilityCode, uiProx, byUserType, byAccessType, ref rgbyPassword[0], usSizePhoto, ref rgbyPhoto[0], usSizeSample, byQuantitySamples, ref rgbyBiometrics[0], uiKeyCode);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a exclusão de um usuário no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] DeleteUser(string strPIS)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer;
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];

                strPIS = strPIS.Trim();

                for (int iIdx = 0; iIdx < rgbyPIS.Length; iIdx++)
                {
                    if (iIdx < strPIS.ToString().Length)
                    {
                        rgbyPIS[iIdx] = (byte)strPIS.ToString()[iIdx];
                    }
                    else
                    {
                        rgbyPIS[iIdx] = (byte)END_STRING;
                    }
                }

                rgbyBuffer = new byte[BUFFER_SIZE_CMD_DELETE_USER];

                this.FDeleteUser(byAddress, byProduct, ref rgbyBuffer[0], ref rgbyPIS[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a leitura dos dados de um usuário no equipamento através do PIS
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadUserData(string strPIS)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer;
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];

                strPIS = strPIS.Trim();

                for (int iIdx = 0; iIdx < rgbyPIS.Length; iIdx++)
                {
                    if (iIdx < strPIS.ToString().Length)
                    {
                        rgbyPIS[iIdx] = (byte)strPIS.ToString()[iIdx];
                    }
                    else
                    {
                        rgbyPIS[iIdx] = (byte)END_STRING;
                    }
                }

                rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_USER_DATA];

                this.FReadUserData(byAddress, byProduct, ref rgbyBuffer[0], ref rgbyPIS[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que configura os dados da empresa/empregador no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetEmployer(byte byIdentifyType, string strCNPJ_CPF, ulong ulCEI, string strEmployerName, string strEmployerAddress)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_SET_EMPLOYER];
                byte[] rgbyCNPJ_CPF = new byte[FIELD_SIZE_CNPJ];
                byte[] rgbyCEI = new byte[FIELD_SIZE_CEI];
                byte[] rgbyEmployerName = new byte[FIELD_SIZE_EMPLOYER_NAME];
                byte[] rgbyEmployerAddress = new byte[FIELD_SIZE_EMPLOYER_ADDRESS];
                string strCEI = "";

                strCNPJ_CPF = strCNPJ_CPF.Trim();

                for (int iIdx = 0; iIdx < rgbyCNPJ_CPF.Length; iIdx++)
                {
                    if (iIdx < strCNPJ_CPF.Length)
                    {
                        rgbyCNPJ_CPF[iIdx] = (byte)strCNPJ_CPF[iIdx];
                    }
                    else
                    {
                        rgbyCNPJ_CPF[iIdx] = (byte)END_STRING;
                    }
                }

                if (ulCEI == 0)
                {
                    for (int iIdx = 0; iIdx < rgbyCEI.Length; iIdx++)
                    {
                         rgbyCEI[iIdx] = (byte)END_STRING;
                    }
                }
                else
                {
                    strCEI = ulCEI.ToString();

                    for (int iIdx = 0; iIdx < rgbyCEI.Length; iIdx++)
                    {
                        if (iIdx < strCEI.Length)
                        {
                            rgbyCEI[iIdx] = (byte)strCEI[iIdx];
                        }
                        else
                        {
                            rgbyCEI[iIdx] = (byte)END_STRING;
                        }
                    }
                }

                strEmployerName = strEmployerName.Trim();

                for (int iIdx = 0; iIdx < rgbyEmployerName.Length; iIdx++)
                {
                    if (iIdx < strEmployerName.Length)
                    {
                        rgbyEmployerName[iIdx] = (byte)strEmployerName[iIdx];
                    }
                    else
                    {
                        rgbyEmployerName[iIdx] = (byte)END_STRING;
                    }
                }

                strEmployerAddress = strEmployerAddress.Trim();

                for (int iIdx = 0; iIdx < rgbyEmployerAddress.Length; iIdx++)
                {
                    if (iIdx < strEmployerAddress.Length)
                    {
                        rgbyEmployerAddress[iIdx] = (byte)strEmployerAddress[iIdx];
                    }
                    else
                    {
                        rgbyEmployerAddress[iIdx] = (byte)END_STRING;
                    }
                }
                
                this.FSetEmployer(byAddress, byProduct, ref rgbyBuffer[0], byIdentifyType, ref rgbyCNPJ_CPF[0], ref rgbyCEI[0], ref rgbyEmployerName[0], ref rgbyEmployerAddress[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a leitura dos dados da empresa/empregador do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadEmployerData()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_EMPLOYER_DATA];

                this.FReadEmployerData(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita alteração na data e na hora do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetDateTime(DateTime dtmDateTime)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_SET_DATE_TIME];

                byte byDay = (byte)dtmDateTime.Date.Day;
                byte byMonth = (byte)dtmDateTime.Date.Month;
                ushort usYear = (ushort)dtmDateTime.Date.Year;
                byte byHour = (byte)dtmDateTime.Hour;
                byte byMinute = (byte)dtmDateTime.Minute;
                byte bySecond = (byte)dtmDateTime.Second;

                this.FSetDateTime(byAddress, byProduct, ref rgbyBuffer[0], byDay, byMonth, usYear, byHour, byMinute, bySecond);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que configura os dados de comunicação do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetREPCommunication(byte byCommunicationType, string strIPEquipment, string strSubnetMask, string strIPGateway, ushort usTCPPort_Comm, ushort usTCPPort_Alarm, byte byBaudrate, byte bySerialAddress, byte byMulticastAddress, byte byBroadcastAddress)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_SET_REP_COMMUNICATION];

                byte[] rgbyIPEquipment = new byte[FIELD_SIZE_IP_EQUIPMENT];
                byte[] rgbySubnetMask = new byte[FIELD_SIZE_SUBNET_MASK];
                byte[] rgbyIPGateway = new byte[FIELD_SIZE_IP_GATEWAY];

                if (strIPEquipment != "")
                {
                    string[] rgstrIPEquipment = strIPEquipment.Split('.');

                    for (int iIdx = 0; iIdx < FIELD_SIZE_IP_EQUIPMENT; iIdx++)
                    {
                        rgbyIPEquipment[iIdx] = Convert.ToByte(rgstrIPEquipment[iIdx]);
                    }
                }

                if (strSubnetMask != "")
                {
                    string[] rgstrSubnetMask = strSubnetMask.Split('.');

                    for (int iIdx = 0; iIdx < FIELD_SIZE_SUBNET_MASK; iIdx++)
                    {
                        rgbySubnetMask[iIdx] = Convert.ToByte(rgstrSubnetMask[iIdx]);
                    }
                }

                if (strIPGateway != "")
                {
                    string[] rgstrIPGateway = strIPGateway.Split('.');

                    for (int iIdx = 0; iIdx < FIELD_SIZE_IP_GATEWAY; iIdx++)
                    {
                        rgbyIPGateway[iIdx] = Convert.ToByte(rgstrIPGateway[iIdx]);
                    }
                }

                this.FSetREPCommunication(byAddress, byProduct, ref rgbyBuffer[0], byCommunicationType, ref rgbyIPEquipment[0], ref rgbySubnetMask[0], ref rgbyIPGateway[0], usTCPPort_Comm, usTCPPort_Alarm, byBaudrate, bySerialAddress, byMulticastAddress, byBroadcastAddress);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a leitura dos dados de comunicação do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadREPCommunication()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_REP_COMMUNICATION];

                this.FReadREPCommunication(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o um evento gravado no equipamento através do NSR
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestEventByNSR(uint uiNSR)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_EVENT_BY_NSR];

                this.FRequestEventByNSR(byAddress, byProduct, ref rgbyBuffer[0], uiNSR);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o NFR do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestNFR()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_NFR];

                this.FRequestNFR(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a leitura dos dados de um usuário no equipamento através de um índice
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestUserByIndex(uint uiIndex)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_USER_DATA_BY_INDEX];

                this.FRequestUserByIndex(byAddress, byProduct, ref rgbyBuffer[0], uiIndex);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que envia o nível de segurança do módulo biometrico para o equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetBiometricSecurity(byte bySecurityLevel)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_SET_BIOMETRIC_SECURITY];

                this.FSetBiometricSecurity(byAddress, byProduct, ref rgbyBuffer[0], bySecurityLevel);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita o nível de segurança do módulo biometrico do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadBiometricSecurity()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_BIOMETRIC_SECURITY];

                this.FReadBiometricSecurity(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita alteração na data do horário de verão do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetDST(DateTime dtmDST_Start, DateTime dtmDST_End)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_SET_DST];

                byte byDST_StartDay = (byte)dtmDST_Start.Date.Day;
                byte byDST_StartMonth = (byte)dtmDST_Start.Date.Month;
                byte byDST_EndDay = (byte)dtmDST_End.Date.Day;
                byte byDST_EndMonth = (byte)dtmDST_End.Date.Month;

                this.FSetDST(byAddress, byProduct, ref rgbyBuffer[0], byDST_StartDay, byDST_StartMonth, byDST_EndDay, byDST_EndMonth);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a verificação da data do horário de verão do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadDST()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_DST];

                this.FReadDST(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }
        
            /// <summary>
            /// Monta o buffer com o comando que solicita 
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetPunchTicketInfo(byte byPunchTicketInfo)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_SET_PUNCH_TICKET_INFO];

                this.FSetPunchTicketInfo(byAddress, byProduct, ref rgbyBuffer[0], byPunchTicketInfo);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita status do alerta durante impressão do ticket
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadPunchTicketInfo()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_PUNCH_TICKET_INFO];

                this.FReadPunchTicketInfo(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }
        
            /// <summary>
            /// Monta o buffer com o comando que solicita 
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetConnectionPoolTimeout(int iPoolTimeout)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_SET_CONNECTION_POOL_TIMEOUT];

                this.FSetConnectionPoolTimeout(byAddress, byProduct, ref rgbyBuffer[0], iPoolTimeout);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita 
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadConnectionPoolTimeout()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_CONNECTION_POOL_TIMEOUT];

                this.FReadConnectionPoolTimeout(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita a temperatura do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTemperature()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestTemperature(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o estado atual do buzzer do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestBuzzerStatus()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestBuzzerStatus(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o estado atual do cutter do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestCutterStatus()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestCutterStatus(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de tickets emitidos pelo equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalPrinterTickets()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestTotalPrinterTickets(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o valor atual da tensão do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestSystemVoltage()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestSystemVoltage(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de eventos (NSR) gravadas no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalNSR()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestTotalNSR(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários gravadas no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalUsers()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestTotalUsers(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de vezes que o cutter foi acionado pelo equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalCutterCuts()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestTotalCutterCuts(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total da quilometragem da impressora do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestPrinterKM()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestPrinterKM(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o tamanho do módulo biométrico do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestBiometricModuleSize()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestBiometricModuleSize(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários com biometria do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalBiometricUsers()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestTotalBiometricUsers(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o status do papel do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestPaperStatus()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestPaperStatus(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários com cartão de código de barras do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalBarCardUsers()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestTotalBarCardUsers(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários com cartão de proximidade do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalProxCardUsers()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestTotalProxCardUsers(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários com código de acesso do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalKeyCodeUsers()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestTotalKeyCodeUsers(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários com password do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalPasswordUsers()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestTotalPasswordUsers(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários administradores do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalAdminUsers()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestTotalAdminUsers(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o tamanho da bobina de papel atual do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestCurrentPaperRollSize()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestCurrentPaperRollSize(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita a quilometragem da bobina de papel atual do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestCurrentPaperRollKM()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestCurrentPaperRollKM(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita a quantidade de tickets impressos da bobina de papel atual do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestCurrentPaperRollTicketsPrinted()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestCurrentPaperRollTicketsPrinted(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita a estimativa de tickets da bobina de papel atual do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestCurrentPaperRollEstimatedTickets()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_STATUS_GENERAL];

                this.FRequestCurrentPaperRollEstimatedTickets(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os status dos alarmes do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestAlarmsStatus()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_ALARMS_STATUS];

                this.FRequestAlarmsStatus(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados de um alarme do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData(byte byAlarm)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA];

                this.FReadAlarmData(byAddress, byProduct, ref rgbyBuffer[0], ref byAlarm);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de Cutter do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Cutter()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_Cutter(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de Buzzer do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Buzzer()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_Buzzer(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de Temperatura do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Temperature()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_Temperature(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme da USB Fiscal do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_USBFiscal()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_USBFiscal(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme da USB Dados do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_USBDados()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_USBDados(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de SEM PAPEL no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_NoPaper()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_NoPaper(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados de PORTA DA IMPRESSORA ABERTA do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_GateOpened()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_GateOpened(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de Relatório de 24 horas pressionado do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_24H_Pressed()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_24H_Pressed(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de Relatório de 24 horas emitido do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_24H_Emitted()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_24H_Emitted(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de MRP atingiu 75% da capacidade do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_MRP_75()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_MPR_75(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de MRP cheia do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_MRP_Full()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_MPR_Full(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme MT atingiu 75% da capacidade do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_MT_75()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_MT_75(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de MT cheia do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_MT_Full()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_MT_Full(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de Bateria em nível crítico do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_BatteryCriticalLevel()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_BatteryCriticalLevel(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme equipamento bloqueado por violação
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_BlockViolation()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_BlockViolation(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de equipamento desbloqueado com sucesso
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_UnblockSuccess()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_UnblockSuccess(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de tentativa de desbloqueio do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_UnblockTried()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_UnblockTried(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de cutter atingiu 75% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Cutter_75()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_Cutter_75(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados de cutter atingiu 100% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Cutter_Full()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_Cutter_Full(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de impressora atingiu 75% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Printer_75()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_Printer_75(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados de impressora atingiu 100% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Printer_Full()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_Printer_Full(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de senha mestre alterada
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_MasterPasswordChanged()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_MasterPasswordChanged(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de equipamento reiniciado por administrador
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_AdminReboot()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_AdminReboot(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de dados de comunicação alterados
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_CommunicationChanged()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_CommunicationChanged(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de equipamento reiniciado por watchdog
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_WatchDogReboot()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_WatchDogReboot(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de nível de segurança do equipamento alterado
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_BiometricSecurityChanged()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_BiometricSecurityChanged(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de alteração do estado do alerta de impressão de ticket do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_PunchTicketInfoChanged()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_PunchTicketInfoChanged(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de alteração da bobina de papel do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_PaperRollChanged()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_PaperRollChanged(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de 90% da bobina de papel do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_PaperRoll_90()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FReadAlarmData_PaperRoll_90(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados de um alarme do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData(byte byAlarm)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA];

                this.FClearAlarmData(byAddress, byProduct, ref rgbyBuffer[0], ref byAlarm);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de Cutter do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Cutter()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_Cutter(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de Buzzer do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Buzzer()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_Buzzer(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de Temperatura do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Temperature()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_Temperature(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme da USB Fiscal do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_USBFiscal()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_USBFiscal(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme da USB Dados do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_USBDados()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_USBDados(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de SEM PAPEL no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_NoPaper()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_NoPaper(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados de PORTA DA IMPRESSORA ABERTA do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_GateOpened()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_GateOpened(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de Relatório de 24 horas pressionado do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_24H_Pressed()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_24H_Pressed(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de Relatório de 24 horas emitido do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_24H_Emitted()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_24H_Emitted(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de MRP atingiu 75% da capacidade do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_MRP_75()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_MPR_75(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de MRP cheia do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_MRP_Full()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_MPR_Full(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme MT atingiu 75% da capacidade do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_MT_75()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_MT_75(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de MT cheia do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_MT_Full()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_MT_Full(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de Bateria em nível crítico do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_BatteryCriticalLevel()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_BatteryCriticalLevel(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme equipamento bloqueado por violação
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_BlockViolation()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_BlockViolation(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de equipamento desbloqueado com sucesso
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_UnblockSuccess()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_UnblockSuccess(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de tentativa de desbloqueio do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_UnblockTried()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_UnblockTried(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de cutter atingiu 75% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Cutter_75()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_Cutter_75(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados de cutter atingiu 100% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Cutter_Full()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_Cutter_Full(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de impressora atingiu 75% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Printer_75()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_Printer_75(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados de impressora atingiu 100% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Printer_Full()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_Printer_Full(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de senha mestre alterada
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_MasterPasswordChanged()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_MasterPasswordChanged(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de equipamento reiniciado por administrador
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_AdminReboot()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_AdminReboot(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de dados de comunicação alterados
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_CommunicationChanged()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_CommunicationChanged(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de equipamento reiniciado por watchdog
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_WatchDogReboot()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_WatchDogReboot(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de nível de segurança do equipamento alterado
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_BiometricSecurityChanged()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_CLEAR_ALARM_DATA_GENERAL];

                this.FClearAlarmData_BiometricSecurityChanged(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de alteração do estado do alerta de impressão de ticket do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_PunchTicketInfoChanged()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FClearAlarmData_PunchTicketInfoChanged(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de alteração da bobina de papel do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_PaperRollChanged()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FClearAlarmData_PaperRollChanged(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de 90% da bobina de papel do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_PaperRoll_90()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_READ_ALARM_DATA_GENERAL];

                this.FClearAlarmData_PaperRoll_90(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Solicita a avaliação do buffer devolvido pelo equipamento
            /// </summary>
            /// <returns>Ver tabela de retorno</returns>
            public int PacketAvail(byte[] rgbyBuffer)
            {
                return this.FPacketAvail(ref rgbyBuffer[0]);
            }

            public string VerifyReturnDLL(int iReturn)
            {
                switch (iReturn)
                {
                    case 0: return Resources.RETURN_0;
                    case 1: return Resources.RETURN_1;
                    case 2: return Resources.RETURN_2;
                    case 3: return Resources.RETURN_3;
                    case 4: return Resources.RETURN_4;
                    case 5: return Resources.RETURN_5;
                    case -1: return Resources.RETURN_NEG_1;
                    case -2: return Resources.RETURN_NEG_2;
                    case -3: return Resources.RETURN_NEG_3;
                    case -4: return Resources.RETURN_NEG_4;
                    case -5: return Resources.RETURN_NEG_5;
                    case -6: return Resources.RETURN_NEG_6;
                    case -7: return Resources.RETURN_NEG_7;
                    case -8: return Resources.RETURN_NEG_8;
                    case -9: return Resources.RETURN_NEG_9;
                    case -10: return Resources.RETURN_NEG_10;
                    case -11: return Resources.RETURN_NEG_11;
                    case -12: return Resources.RETURN_NEG_12;
                    case -13: return Resources.RETURN_NEG_13;
                    case -14: return Resources.RETURN_NEG_14;
                    case -15: return Resources.RETURN_NEG_15;
                    case -16: return Resources.RETURN_NEG_16;
                    case -17: return Resources.RETURN_NEG_17;
                    case -18: return Resources.RETURN_NEG_18;
                    case -19: return Resources.RETURN_NEG_19;
                    case -20: return Resources.RETURN_NEG_20;
                    case -21: return Resources.RETURN_NEG_21;
                    case -22: return Resources.RETURN_NEG_22;
                    case -23: return Resources.RETURN_NEG_23;
                    case -24: return Resources.RETURN_NEG_24;
                    case -25: return Resources.RETURN_NEG_25;
                    case -26: return Resources.RETURN_NEG_26;
                    case -27: return Resources.RETURN_NEG_27;
                    case -28: return Resources.RETURN_NEG_28;
                    case -29: return Resources.RETURN_NEG_29;
                    case -30: return Resources.RETURN_NEG_30;
                    case -31: return Resources.RETURN_NEG_31;
                    case -100: return Resources.RETURN_NEG_100;
                    case -101: return Resources.RETURN_NEG_101;
                    case -102: return Resources.RETURN_NEG_102;
                    case -103: return Resources.RETURN_NEG_103;
                    case -104: return Resources.RETURN_NEG_104;
                    case -105: return Resources.RETURN_NEG_105;
                    case -200: return Resources.RETURN_NEG_200;
                    case -201: return Resources.RETURN_NEG_201;
                    case -202: return Resources.RETURN_NEG_202;
                    case -203: return Resources.RETURN_NEG_203;
                    case -204: return Resources.RETURN_NEG_204;
                    case -205: return Resources.RETURN_NEG_205;
                    case -206: return Resources.RETURN_NEG_206;
                    case -207: return Resources.RETURN_NEG_207;
                    case -208: return Resources.RETURN_NEG_208;
                    case -209: return Resources.RETURN_NEG_209;
                    case -210: return Resources.RETURN_NEG_210;
                    default: return Resources.RETURN_UNKNOW;
                }
            }

            /// <summary>
            /// Obtem o comando enviado / recebido do equipamento
            /// </summary>
            /// <returns></returns>
            public void GetCommand(ref byte byCommand)
            {
                this.FGetCommand(ref byCommand);
            }

            /// <summary>
            /// Obtem os dados do usuário solicitado no comando ReadUserData
            /// </summary>
            /// <returns></returns>
            public void GetUserData(ref string strPIS, ref string strUserName, ref uint uiKeyCode, ref string strBarCode, ref byte byFacilityCode, ref ulong ulProxCode, ref byte byUserType, ref int iAccessType, ref string strPassword, ref ushort usSizePhoto, ref System.IO.MemoryStream msPhoto, ref ushort usSizeSample, ref byte byQuantitySamples, ref byte[] rgbyBiometric_Sample1, ref byte[] rgbyBiometric_Sample2)
            {
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];
                byte[] rgbyUserName = new byte[FIELD_SIZE_USERNAME];
                byte[] rgbyPassword = new byte[FIELD_SIZE_PASSWORD];
                byte[] rgbyBarCode = new byte[FIELD_SIZE_BARCODE];
                byte[] rgbyPhoto = new byte[1];

                byte byAccessType = 0;

                this.FGetUserData(ref rgbyPIS[0], ref rgbyUserName[0], ref uiKeyCode, ref rgbyBarCode[0], ref byFacilityCode, ref ulProxCode, ref byAccessType, ref byUserType, ref rgbyPassword[0], ref usSizePhoto, ref rgbyPhoto[0], ref usSizeSample, ref byQuantitySamples, ref rgbyBiometric_Sample1[0], ref rgbyBiometric_Sample2[0]);

                strPIS = "";

                for (int iIdx = 0; iIdx < rgbyPIS.Length; iIdx++)
                {
                    if ((char)rgbyPIS[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strPIS += (char)rgbyPIS[iIdx];
                }

                strPIS = strPIS.Trim();

                strUserName = "";

                for (int iIdx = 0; iIdx < rgbyUserName.Length; iIdx++)
                {
                    if ((char)rgbyUserName[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strUserName += (char)rgbyUserName[iIdx];
                }

                strUserName = strUserName.Trim();

                strBarCode = "";

                for (int iIdx = 0; iIdx < rgbyBarCode.Length; iIdx++)
                {
                    strBarCode += rgbyBarCode[iIdx];
                }

                if (strBarCode == "00000000000000000000")
                {
                    strBarCode = "";
                }

                strPassword = "";

                for (int iIdx = 0; iIdx < rgbyPassword.Length; iIdx++)
                {
                    if ((char)rgbyPassword[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strPassword += (char)rgbyPassword[iIdx];
                }

                strPassword = strPassword.Trim();

                if (usSizePhoto != 0)
                {
                    Array.Resize(ref rgbyPhoto, usSizePhoto);
                    msPhoto = new System.IO.MemoryStream(rgbyPhoto);
                }

                iAccessType = byAccessType;
            }

            /// <summary>
            /// Obtem os dados da empresa/empregador solicitado no comando ReadEmployerData
            /// </summary>
            /// <returns></returns>
            public void GetEmployerData(ref byte byIdentifyType, ref string strCNPJ_CPF, ref ulong ulCEI, ref string strEmployerName, ref string strEmployerAddress)
            {
                byte[] rgbyCNPJ_CPF = new byte[FIELD_SIZE_CNPJ];
                byte[] rgbyCEI = new byte[FIELD_SIZE_CEI];
                byte[] rgbyEmployerName = new byte[FIELD_SIZE_EMPLOYER_NAME];
                byte[] rgbyEmployerAddress = new byte[FIELD_SIZE_EMPLOYER_ADDRESS];

                this.FGetEmployerData(ref byIdentifyType, ref rgbyCNPJ_CPF[0], ref rgbyCEI[0], ref rgbyEmployerName[0], ref rgbyEmployerAddress[0]);
                
                string strCEI = "";

                for (int iIdx = 0; iIdx < rgbyCNPJ_CPF.Length; iIdx++)
                {
                    if ((char)rgbyCNPJ_CPF[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strCNPJ_CPF += (char)rgbyCNPJ_CPF[iIdx];
                }

                strCNPJ_CPF = strCNPJ_CPF.Trim();

                for (int iIdx = 0; iIdx < rgbyCEI.Length; iIdx++)
                {
                    if ((char)rgbyCEI[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strCEI += (char)rgbyCEI[iIdx];
                }

                strCEI = strCEI.Trim();

                if (strCEI == "")
                {
                    ulCEI = 0;
                }
                else
                {
                    ulCEI = ulong.Parse(strCEI);
                }

                for (int iIdx = 0; iIdx < rgbyEmployerName.Length; iIdx++)
                {
                    if ((char)rgbyEmployerName[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strEmployerName += (char)rgbyEmployerName[iIdx];
                }

                strEmployerName = strEmployerName.Trim();

                for (int iIdx = 0; iIdx < rgbyEmployerAddress.Length; iIdx++)
                {
                    if ((char)rgbyEmployerAddress[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strEmployerAddress += (char)rgbyEmployerAddress[iIdx];
                }

                strEmployerAddress = strEmployerAddress.Trim();
            }

            /// <summary>
            /// Obtem os dados do registro tipo 2 solicitado no comando RequestEventByNSR
            /// </summary>
            /// <returns></returns>
            public void GetLogType2(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref byte byIdentifyType, ref string strCNPJ_CPF, ref ulong ulCEI, ref string strEmployerName, ref string strEmployerAddress)
            {
                byte[] rgbyCNPJ_CPF = new byte[FIELD_SIZE_CNPJ];
                byte[] rgbyCEI = new byte[FIELD_SIZE_CEI];
                byte[] rgbyEmployerName = new byte[FIELD_SIZE_EMPLOYER_NAME];
                byte[] rgbyEmployerAddress = new byte[FIELD_SIZE_EMPLOYER_ADDRESS];

                this.FGetLogType2(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref byIdentifyType, ref rgbyCNPJ_CPF[0], ref rgbyCEI[0], ref rgbyEmployerName[0], ref rgbyEmployerAddress[0]);

                string strCEI = "";
                
                strCNPJ_CPF = "";

                for (int iIdx = 0; iIdx < rgbyCNPJ_CPF.Length; iIdx++)
                {
                    if ((char)rgbyCNPJ_CPF[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strCNPJ_CPF += (char)rgbyCNPJ_CPF[iIdx];
                }

                strCNPJ_CPF = strCNPJ_CPF.Trim();

                for (int iIdx = 0; iIdx < rgbyCEI.Length; iIdx++)
                {
                    if ((char)rgbyCEI[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strCEI += (char)rgbyCEI[iIdx];
                }

                strCEI = strCEI.Trim();

                if (strCEI == "")
                {
                    ulCEI = 0;
                }
                else
                {
                    ulCEI = ulong.Parse(strCEI);
                }

                strEmployerName = "";

                for (int iIdx = 0; iIdx < rgbyEmployerName.Length; iIdx++)
                {
                    if ((char)rgbyEmployerName[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strEmployerName += (char)rgbyEmployerName[iIdx];
                }

                strEmployerName = strEmployerName.Trim();

                strEmployerAddress = "";

                for (int iIdx = 0; iIdx < rgbyEmployerAddress.Length; iIdx++)
                {
                    if ((char)rgbyEmployerAddress[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strEmployerAddress += (char)rgbyEmployerAddress[iIdx];
                }

                strEmployerAddress = strEmployerAddress.Trim();
            }

            /// <summary>
            /// Obtem os dados do registro tipo 3 solicitado no comando RequestEventByNSR
            /// </summary>
            /// <returns></returns>
            public void GetLogType3(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref string strPIS)
            {
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];

                this.FGetLogType3(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref rgbyPIS[0]);

                strPIS = "";

                for (int iIdx = 0; iIdx < rgbyPIS.Length; iIdx++)
                {
                    if ((char)rgbyPIS[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strPIS += (char)rgbyPIS[iIdx];
                }

                strPIS = strPIS.Trim();
            }

            /// <summary>
            /// Obtem os dados do registro tipo 4 solicitado no comando RequestEventByNSR
            /// </summary>
            /// <returns></returns>
            public void GetLogType4(ref uint uiNSR, ref byte byRegType, ref byte byDayBeforeAdjust, ref byte byMonthBeforeAdjust, ref ushort usYearBeforeAdjust, ref byte byHourBeforeAdjust, ref byte byMinuteBeforeAdjust, ref byte byDayAfterAdjust, ref byte byMonthAfterAdjust, ref ushort usYearAfterAdjust, ref byte byHourAfterAdjust, ref byte byMinuteAfterAdjust)
            {
                this.FGetLogType4(ref uiNSR, ref byRegType, ref byDayBeforeAdjust, ref byMonthBeforeAdjust, ref usYearBeforeAdjust, ref byHourBeforeAdjust, ref byMinuteBeforeAdjust, ref byDayAfterAdjust, ref byMonthAfterAdjust, ref usYearAfterAdjust, ref byHourAfterAdjust, ref byMinuteAfterAdjust);
            }

            /// <summary>
            /// Obtem os dados do registro tipo 5 solicitado no comando RequestEventByNSR
            /// </summary>
            /// <returns></returns>
            public void GetLogType5(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref byte byOpType, ref string strPIS, ref string strUsername)
            {
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];
                byte[] rgbyUserName = new byte[FIELD_SIZE_USERNAME];

                this.FGetLogType5(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref byOpType, ref rgbyPIS[0], ref rgbyUserName[0]);

                strPIS = "";

                for (int iIdx = 0; iIdx < rgbyPIS.Length; iIdx++)
                {
                    if ((char)rgbyPIS[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strPIS += (char)rgbyPIS[iIdx];
                }

                strPIS = strPIS.Trim();

                strUsername = "";

                for (int iIdx = 0; iIdx < rgbyUserName.Length; iIdx++)
                {
                    if ((char)rgbyUserName[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strUsername += (char)rgbyUserName[iIdx];
                }

                strUsername = strUsername.Trim();
            }

            /// <summary>
            /// Obtem os dados do registro tipo 3 solicitado no comando RequestEventByNSR
            /// </summary>
            /// <returns></returns>
            public void GetLogType6(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref string strPIS, ref char cEvent)
            {
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];
                byte byEvent = 0;

                this.FGetLogType6(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref rgbyPIS[0], ref byEvent);

                strPIS = "";

                for (int iIdx = 0; iIdx < rgbyPIS.Length; iIdx++)
                {
                    if ((char)rgbyPIS[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strPIS += (char)rgbyPIS[iIdx];
                }

                strPIS = strPIS.Trim();

                cEvent = (char)byEvent;
            }

            /// <summary>
            /// Obtem os dados da comunicação do equipamento solicitado no comando ReadREPCommunication
            /// </summary>
            /// <returns></returns>
            public void GetREPCommunication(ref byte byCommunicationType, ref string strIPEquipment, ref string strSubnetMask, ref string strIPGateway, ref ushort usTCPPort_Comm, ref ushort usTCPPort_Alarm, ref byte byBaudrate, ref byte bySerialAddress, ref byte byMulticastAddress, ref byte byBroadcastAddress)
            {
                byte[] rgbyIPEquipment = new byte[FIELD_SIZE_IP_EQUIPMENT];
                byte[] rgbySubnetMask = new byte[FIELD_SIZE_SUBNET_MASK];
                byte[] rgbyIPGateway = new byte[FIELD_SIZE_IP_GATEWAY];

                this.FGetREPCommunication(ref byCommunicationType, ref rgbyIPEquipment[0], ref rgbySubnetMask[0], ref rgbyIPGateway[0], ref usTCPPort_Comm, ref usTCPPort_Alarm, ref byBaudrate, ref bySerialAddress, ref byMulticastAddress, ref byBroadcastAddress);

                strIPEquipment = "";
                strIPEquipment += rgbyIPEquipment[0] + ".";
                strIPEquipment += rgbyIPEquipment[1] + ".";
                strIPEquipment += rgbyIPEquipment[2] + ".";
                strIPEquipment += rgbyIPEquipment[3];

                strSubnetMask = "";
                strSubnetMask += rgbySubnetMask[0] + ".";
                strSubnetMask += rgbySubnetMask[1] + ".";
                strSubnetMask += rgbySubnetMask[2] + ".";
                strSubnetMask += rgbySubnetMask[3];

                strIPGateway = "";
                strIPGateway += rgbyIPGateway[0] + ".";
                strIPGateway += rgbyIPGateway[1] + ".";
                strIPGateway += rgbyIPGateway[2] + ".";
                strIPGateway += rgbyIPGateway[3];
            }

            /// <summary>
            /// Obtem os dados do NFR do equipamento solicitado no comando RequestNFR
            /// </summary>
            /// <returns></returns>
            public void GetNFR(ref string strNFR)
            {
                byte[] rgbyNFR = new byte[FIELD_SIZE_NFR];

                this.FGetNFR(ref rgbyNFR[0]);

                for (int iIdx = 0; iIdx < rgbyNFR.Length; iIdx++)
                {
                    strNFR += (char)rgbyNFR[iIdx];
                }
            }

            /// <summary>
            /// Obtem o nível de segurança do módulo biométrico do equipamento solicitado no comando ReadBiometricSecurity
            /// </summary>
            /// <returns></returns>
            public void GetBiometricSecurity(ref byte bySecurityLevel)
            {
                this.FGetBiometricSecurity(ref bySecurityLevel);
            }

            /// <summary>
            /// Obtem o horário de verão do equipamento solicitado no comando ReadDST
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public void GetDST(ref DateTime dtmDST_Start, ref DateTime dtmDST_End)
            {
                byte byDST_StartDay = 0;
                byte byDST_StartMonth = 0;
                byte byDST_EndDay = 0;
                byte byDST_EndMonth = 0;

                this.FGetDST(ref byDST_StartDay, ref byDST_StartMonth, ref byDST_EndDay, ref byDST_EndMonth);

                if (byDST_StartDay != 0 && byDST_StartMonth != 0)
                {
                    dtmDST_Start = new DateTime(DateTime.Now.Year, (int)byDST_StartMonth, (int)byDST_StartDay);
                }

                if (byDST_EndDay != 0 && byDST_EndMonth != 0)
                {
                    dtmDST_End = new DateTime(DateTime.Now.Year, (int)byDST_EndMonth, (int)byDST_EndDay);
                }
            }

            /// <summary>
            /// Obtem o  do equipamento solicitado no comando ReadPunchTicketInfo
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public void GetPunchTicketInfo(ref int iPunchTicketInfo)
            {
                this.FGetPunchTicketInfo(ref iPunchTicketInfo);
            }

            /// <summary>
            /// Obtem o timeout de pool da conexão do equipamento solicitado no comando ReadPunchTicketInfo
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public void GetConnectionPoolTimeout(ref int iPoolTimeout)
            {
                this.FGetConnectionPoolTimeout(ref iPoolTimeout);
            }

            /// <summary>
            /// Obtem a temperatura do equipamento solicitado no comando RequestTemperature
            /// </summary>
            /// <returns></returns>
            public void GetTemperature(ref int iTemperature)
            {
                this.FGetTemperature(ref iTemperature);
            }

            /// <summary>
            /// Obtem o estado do Buzzer requisitado na função RequestBuzzerStatus
            /// </summary>
            /// <returns></returns>
            public void GetBuzzerStatus(ref int iBuzzerStatus)
            {
                this.FGetBuzzerStatus(ref iBuzzerStatus);
            }

            /// <summary>
            /// Obtem o estado do Cutter requisitado na função RequestCutterStatus
            /// </summary>
            /// <returns></returns>
            public void GetCutterStatus(ref int iCutterStatus)
            {
                this.FGetCutterStatus(ref iCutterStatus);
            }

            /// <summary>
            /// Obtem o total de tickets impressos pelo equipamento solicitado na função RequestTotalPrinterTickets
            /// </summary>
            /// <returns></returns>
            public void GetTotalPrinterTickets(ref int iTotalPrinterTickets)
            {
                this.FGetTotalPrinterTickets(ref iTotalPrinterTickets);
            }

            /// <summary>
            /// Obtem a tensão atual do equipamento solicitada na função RequestSystemVoltage
            /// </summary>
            /// <returns></returns>
            public void GetSystemVoltage(ref int iSystemVoltage)
            {
                this.FGetSystemVoltage(ref iSystemVoltage);
            }

            /// <summary>
            /// Obtem o total de eventos gravados no equipamento solicitado na função RequestTotalNSR
            /// </summary>
            /// <returns></returns>
            public void GetTotalNSR(ref uint uiTotalNSR)
            {
                this.FGetTotalNSR(ref uiTotalNSR);
            }

            /// <summary>
            /// Obtem o total de usuários gravados no equipamento solicitado na função RequestTotalUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalUsers(ref int iTotalUsers)
            {
                this.FGetTotalUsers(ref iTotalUsers);
            }

            /// <summary>
            /// Obtem o total de acionamentos realizados pelo Cutter do equipamento solicitado na função RequestTotalCutterCuts
            /// </summary>
            /// <returns></returns>
            public void GetTotalCutterCuts(ref int iTotalCutterCuts)
            {
                this.FGetTotalCutterCuts(ref iTotalCutterCuts);
            }

            /// <summary>
            /// Obtem a quilometragem da impressora do equipamento solicitado na função RequestPrinterKM
            /// </summary>
            /// <returns></returns>
            public void GetPrinterKM(ref int iPrinterKM)
            {
                this.FGetPrinterKM(ref iPrinterKM);
            }

            /// <summary>
            /// Obtem o tamanho do módulo biometrico (quantidade máxima de usuários) do equipamento solicitado na função RequestBiometricModuleSize
            /// </summary>
            /// <returns></returns>
            public void GetBiometricModuleSize(ref int iBiometricModuleSize)
            {
                this.FGetBiometricModuleSize(ref iBiometricModuleSize);
            }

            /// <summary>
            /// Obtem a quantidade de usuários cadastrados no módulo biometrico do equipamento solicitado na função RequestTotalBiometricUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalBiometricUsers(ref int iTotalBiometricUsers)
            {
                this.FGetTotalBiometricUsers(ref iTotalBiometricUsers);
            }

            /// <summary>
            /// Obtem a status do papel do equipamento solicitado na função RequestPaperStatus
            /// </summary>
            /// <returns></returns>
            public void GetPaperStatus(ref int iPaperStatus)
            {
                this.FGetPaperStatus(ref iPaperStatus);
            }

            /// <summary>
            /// Obtem a quantidade de usuários cadastrados com cartão de código de baras no equipamento solicitado na função RequestTotalBarCardUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalBarCardUsers(ref int iTotalBarCardUsers)
            {
                this.FGetTotalBarCardUsers(ref iTotalBarCardUsers);
            }

            /// <summary>
            /// Obtem a quantidade de usuários cadastrados com cartão de proximidade no equipamento solicitado na função RequestTotalProxCardUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalProxCardUsers(ref int iTotalProxCardUsers)
            {
                this.FGetTotalProxCardUsers(ref iTotalProxCardUsers);
            }

            /// <summary>
            /// Obtem a quantidade de usuários cadastrados com código de acesso no equipamento solicitado na função RequestTotalKeyCodeUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalKeyCodeUsers(ref int iTotalKeyCodeUsers)
            {
                this.FGetTotalKeyCodeUsers(ref iTotalKeyCodeUsers);
            }

            /// <summary>
            /// Obtem a quantidade de usuários cadastrados com senha no equipamento solicitado na função RequestTotalPasswordUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalPasswordUsers(ref int iTotalPasswordUsers)
            {
                this.FGetTotalPasswordUsers(ref iTotalPasswordUsers);
            }

            /// <summary>
            /// Obtem a quantidade de usuários cadastrados como administradores no equipamento solicitado na função RequestTotalAdminUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalAdminUsers(ref int iTotalAdminUsers)
            {
                this.FGetTotalAdminUsers(ref iTotalAdminUsers);
            }

            /// <summary>
            /// Obtem o tamanho da bobina de papel atual do equipamento solicitado na função RequestCurrentPaperRollSize
            /// </summary>
            /// <returns></returns>
            public void GetCurrentPaperRollSize(ref int iCurrentPaperRollSize)
            {
                this.FGetCurrentPaperRollSize(ref iCurrentPaperRollSize);
            }

            /// <summary>
            /// Obtem a quilometragem da bobina de papel atual do equipamento solicitado na função RequestCurrentPaperRollKM
            /// </summary>
            /// <returns></returns>
            public void GetCurrentPaperRollKM(ref int iCurrentPaperRollKM)
            {
                this.FGetCurrentPaperRollKM(ref iCurrentPaperRollKM);
            }

            /// <summary>
            /// Obtem a quantidade de tickets impressos da bobina de papel atual do equipamento solicitado na função RequestCurrentPaperRollTicketsPrinted
            /// </summary>
            /// <returns></returns>
            public void GetCurrentPaperRollTicketsPrinted(ref int iCurrentPaperRollTicketsPrinted)
            {
                this.FGetCurrentPaperRollTicketsPrinted(ref iCurrentPaperRollTicketsPrinted);
            }

            /// <summary>
            /// Obtem a estimativa de tickets da bobina de papel atual do equipamento solicitado na função RequestCurrentPaperRollTicketsPrinted
            /// </summary>
            /// <returns></returns>
            public void GetCurrentPaperRollEstimatedTickets(ref int iCurrentPaperRollEstimatedTickets)
            {
                this.FGetCurrentPaperRollEstimatedTickets(ref iCurrentPaperRollEstimatedTickets);
            }

            /// <summary>
            /// Obtem os estados dos alarmes do equipamento solicitado na função RequestAlarmsStatus
            /// </summary>
            /// <returns></returns>
            public void GetAlarmsStatus(ref byte byAlarmsStatus_Cutter_Changed, ref byte byAlarmsStatus_Buzzer_Changed, ref byte byAlarmsStatus_Reserved_1, ref byte byAlarmsStatus_USBFiscal_Activated, ref byte byAlarmsStatus_USBDados_Activated, ref byte byAlarmsStatus_PrinterNoPaper, ref byte byAlarmsStatus_PrinterOpenGate, ref byte byAlarmsStatus_Report24H_Pressioned, ref byte byAlarmsStatus_Report24H_Emitted, ref byte byAlarmsStatus_MPR_75Percent, ref byte byAlarmsStatus_MRP_Full, ref byte byAlarmsStatus_MT_75Percent, ref byte byAlarmsStatus_MT_Full, ref byte byAlarmsStatus_Battery_CriticalLevel, ref byte byAlarmsStatus_BlockViolation, ref byte byAlarmsStatus_UnblockSuccess, ref byte byAlarmsStatus_UnblockTried, ref byte byAlarmsStatus_Cutter_75Percent, ref byte byAlarmsStatus_Cutter_Full, ref byte byAlarmsStatus_Printer_75Percent, ref byte byAlarmsStatus_Printer_Full, ref byte byAlarmsStatus_MasterPasswordChanged, ref byte byAlarmsStatus_AdminReboot, ref byte byAlarmsStatus_TCP_IP_Changed)
            {
                byte[] rgbyAlarmsStatus = new byte[QUANTITY_ALARMS];

                this.FGetAlarmsStatus(ref rgbyAlarmsStatus[0]);

                byAlarmsStatus_Cutter_Changed = rgbyAlarmsStatus[0];
                byAlarmsStatus_Buzzer_Changed = rgbyAlarmsStatus[1];
                byAlarmsStatus_Reserved_1 = rgbyAlarmsStatus[2];
                byAlarmsStatus_USBFiscal_Activated = rgbyAlarmsStatus[3];
                byAlarmsStatus_USBDados_Activated = rgbyAlarmsStatus[4];
                byAlarmsStatus_PrinterNoPaper = rgbyAlarmsStatus[5];
                byAlarmsStatus_PrinterOpenGate = rgbyAlarmsStatus[6];
                byAlarmsStatus_Report24H_Pressioned = rgbyAlarmsStatus[7];
                byAlarmsStatus_Report24H_Emitted = rgbyAlarmsStatus[8];
                byAlarmsStatus_MPR_75Percent = rgbyAlarmsStatus[9];
                byAlarmsStatus_MRP_Full = rgbyAlarmsStatus[10];
                byAlarmsStatus_MT_75Percent = rgbyAlarmsStatus[11];
                byAlarmsStatus_MT_Full = rgbyAlarmsStatus[12];
                byAlarmsStatus_Battery_CriticalLevel = rgbyAlarmsStatus[13];
                byAlarmsStatus_BlockViolation = rgbyAlarmsStatus[14];
                byAlarmsStatus_UnblockSuccess = rgbyAlarmsStatus[15];
                byAlarmsStatus_UnblockTried = rgbyAlarmsStatus[16];
                byAlarmsStatus_Cutter_75Percent = rgbyAlarmsStatus[17];
                byAlarmsStatus_Cutter_Full = rgbyAlarmsStatus[18];
                byAlarmsStatus_Printer_75Percent = rgbyAlarmsStatus[19];
                byAlarmsStatus_Printer_Full = rgbyAlarmsStatus[20];
                byAlarmsStatus_MasterPasswordChanged = rgbyAlarmsStatus[21];
                byAlarmsStatus_AdminReboot = rgbyAlarmsStatus[22];
                byAlarmsStatus_TCP_IP_Changed = rgbyAlarmsStatus[23];
            }

            /// <summary>
            /// Obtem os dados de um alarme do equipamento solicitado na função RequestAlarmData
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Cutter
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Cutter(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_Cutter(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Buzzer
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Buzzer(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_Buzzer(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Temperature
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Temperature(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_Temperature(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_USBFiscal
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_USBFiscal(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_USBFiscal(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_USBDados
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_USBDados(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_USBDados(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_NoPaper
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_NoPaper(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_NoPaper(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_GateOpened
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_GateOpened(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_GateOpened(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_24H_Pressed
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_24H_Pressed(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_24H_Pressed(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_24H_Emitted
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_24H_Emitted(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_24H_Emitted(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_MPR_75
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_MRP_75(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_MPR_75(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_MPR_Full
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_MRP_Full(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_MPR_Full(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_MT_75
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_MT_75(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_MT_75(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_MT_Full
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_MT_Full(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_MT_Full(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_BatteryCriticalLevel
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_BatteryCriticalLevel(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_BatteryCriticalLevel(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_BlockViolation
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_BlockViolation(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_BlockViolation(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_UnblockSuccess
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_UnblockSuccess(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_UnblockSuccess(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_UnblockTried
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_UnblockTried(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_UnblockTried(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Cutter_75
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Cutter_75(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_Cutter_75(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Cutter_Full
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Cutter_Full(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_Cutter_Full(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Printer_75
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Printer_75(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_Printer_75(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Printer_Full
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Printer_Full(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_Printer_Full(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_MasterPasswordChanged
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_MasterPasswordChanged(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_MasterPasswordChanged(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_AdminReboot
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_AdminReboot(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_AdminReboot(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_CommunicationChanged
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_CommunicationChanged(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_CommunicationChanged(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_WatchDogReboot
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_WatchDogReboot(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_WatchDogReboot(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_BiometricSecurityChanged
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_BiometricSecurityChanged(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_BiometricSecurityChanged(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_PunchTicketChanged
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_PunchTicketInfoChanged(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_PunchTicketInfoChanged(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_PaperRollChanged
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_PaperRollChanged(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_PaperRollChanged(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_PaperRoll_90
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_PaperRoll_90(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                byte byAlarmDate_Day = 0;
                byte byAlarmDate_Month = 0;
                ushort usAlarmDate_Year = 0;
                byte byAlarmTime_Hour = 0;
                byte byAlarmTime_Min = 0;
                byte byAlarmTime_Sec = 0;

                this.FGetAlarmData_PaperRoll_90(ref byAlarmStatus, ref byAlarmDate_Day, ref byAlarmDate_Month, ref usAlarmDate_Year, ref byAlarmTime_Hour, ref byAlarmTime_Min, ref byAlarmTime_Sec);

                if (usAlarmDate_Year != 0)
                {
                    dtmAlarmDateTime = new DateTime((int)usAlarmDate_Year, (int)byAlarmDate_Month, (int)byAlarmDate_Day, (int)byAlarmTime_Hour, (int)byAlarmTime_Min, (int)byAlarmTime_Sec);
                }
            }

            /// <summary>
            /// Verifica se o periférico Hamster está conectado ao PC
            /// </summary>
            /// <returns></returns>
            public bool IsHamsterConnected()
            {
                return this.FIsHamsterConnected();
            }

            /// <summary>
            /// Obtém o tipo de hamster conectado ao computador
            /// </summary>
            /// <returns>0 = Hamster não encontrado, 1 = NITGEN, 2 = VIRDI</returns>
            public int GetHamsterType()
            {
                return this.FGetHamsterType();
            }

            /// <summary>
            /// Obtém os templates das biometrias para módulos FIM01 (1000 e 4000 usuários)
            /// </summary>
            /// <param name="rgbySample404_1">Template da amostra do dedo 1</param>
            /// <param name="byFingerPID_1">Id do primeiro dedo no qual foi obtida as amostras</param>
            /// <param name="bySampleID_1">Qual amostra foi utilizada</param>
            /// <param name="byQuality_1">Qualidade da amostra</param>
            /// <param name="rgbySample404_2">Template da amostra do dedo 2</param>
            /// <param name="byFingerPID_2">Id do segundo dedo no qual foi obtida as amostras</param>
            /// <param name="bySampleID_2">Qual amostra foi utilizada</param>
            /// <param name="byQuality_2">Qualidade da amostra</param>
            /// <param name="bRotateSamples">O template será rotacionado</param>
            public bool GetTemplates_FIM01(ref byte[] rgbySample404_1, ref byte byFingerPID_1, ref byte bySampleID_1, ref byte byQuality_1, ref byte[] rgbySample404_2, ref byte byFingerPID_2, ref byte bySampleID_2, ref byte byQuality_2, bool bRotateSamples)
            {
                byte byRotateTemplates = 0;

                if (bRotateSamples)
                {
                    byRotateTemplates = 1;
                }

                return this.FGetTemplates_FIM01(ref rgbySample404_1[0], ref byFingerPID_1, ref bySampleID_1, ref byQuality_1, ref rgbySample404_2[0], ref byFingerPID_2, ref bySampleID_2, ref byQuality_2, byRotateTemplates);
            }

            /// <summary>
            /// Obtém os templates das biometrias para módulos FIM10 (100 usuários)
            /// </summary>
            /// <param name="rgbySample400_1">Template da amostra do dedo 1</param>
            /// <param name="byFingerPID_1">Id do primeiro dedo no qual foi obtida as amostras</param>
            /// <param name="bySampleID_1">Qual amostra foi utilizada</param>
            /// <param name="byQuality_1">Qualidade da amostra</param>
            /// <param name="rgbySample400_2">Template da amostra do dedo 2</param>
            /// <param name="byFingerPID_2">Id do segundo dedo no qual foi obtida as amostras</param>
            /// <param name="bySampleID_2">Qual amostra foi utilizada</param>
            /// <param name="byQuality_2">Qualidade da amostra</param>
            /// <param name="bRotateSamples">O template será rotacionado</param>
            public bool GetTemplates_FIM10(ref byte[] rgbySample400_1, ref byte byFingerPID_1, ref byte bySampleID_1, ref byte byQuality_1, ref byte[] rgbySample400_2, ref byte byFingerPID_2, ref byte bySampleID_2, ref byte byQuality_2, bool bRotateSamples)
            {
                byte byRotateTemplates = 0;

                if (bRotateSamples)
                {
                    byRotateTemplates = 1;
                }

                return this.FGetTemplates_FIM10(ref rgbySample400_1[0], ref byFingerPID_1, ref bySampleID_1, ref byQuality_1, ref rgbySample400_2[0], ref byFingerPID_2, ref bySampleID_2, ref byQuality_2, byRotateTemplates);
            }

            /// <summary>
            /// Obtém os templates das biometrias para módulos VIRDI
            /// </summary>
            /// <param name="byFingerPID"></param>
            /// <param name="rgbySample400_1"></param>
            /// <param name="rgbySample400_2"></param>
            /// <returns></returns>
            public bool GetTemplates_Virdi(ref byte byFingerPID, ref byte[] rgbySample400_1, ref byte[] rgbySample400_2)
            {
                return this.FGetTemplates_Virdi(ref byFingerPID, ref rgbySample400_1[0], ref rgbySample400_2[0]);
            }

            /// <summary>
            /// Converte o template da biometria de 400 bytes para o de 404 bytes
            /// </summary>
            /// <param name="rgbyTemplate400"></param>
            /// <param name="rgbyTemplate404"></param>
            /// <returns>Verdadeiro se a conversão foi realizada com sucesso</returns>
            public bool ConvertTemplate400ToTemplate404(byte[] rgbyTemplate400, byte[] rgbyTemplate404)
            {
                int iRet = 0;

                iRet = this.FFIM10_To_FIM01(ref rgbyTemplate400[0], ref rgbyTemplate404[0]);

                if (iRet > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// Converte o template da biometria de 404 bytes para o de 400 bytes
            /// </summary>
            /// <param name="rgbyTemplate404"></param>
            /// <param name="rgbyTemplate400"></param>
            /// <returns>Verdadeiro se a conversão foi realizada com sucesso</returns>
            public bool ConvertTemplate404ToTemplate400(byte[] rgbyTemplate404, byte[] rgbyTemplate400)
            {
                int iRet = 0;

                iRet = this.FFIM01_To_FIM10(ref rgbyTemplate404[0], ref rgbyTemplate400[0]);

                if (iRet > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void EncryptBuffer(ref byte[] rgbyBuffer, uint uiSize)
            {
                this.FEncryptBuffer(ref rgbyBuffer[0], uiSize);
            }

            public void DecryptBuffer(ref byte[] rgbyBuffer, uint uiSize)
            {
                this.FDecryptBuffer(ref rgbyBuffer[0], uiSize);
            }
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="strUBSFilePath"></param>
            /// <param name="uiQuantityUsers"></param>
            /// <param name="strEmployerName"></param>
            /// <param name="strCNPJ_CPF"></param>
            /// <param name="strCEI"></param>
            /// <param name="strEmployerAddress"></param>
            /// <param name="strNFR"></param>
            /// <returns></returns>
            public int CreateUBSFile(string strUBSFilePath, uint uiQuantityUsers, string strEmployerName, string strCNPJ_CPF, string strCEI, string strEmployerAddress, string strNFR)
            {
                byte[] rgbyUBSFilePath = new byte[strUBSFilePath.Length];
                byte[] rgbyEmployerName = new byte[UBS_FILE_SIZE_EMPLOYER_NAME];
                byte[] rgbyCNPJ_CPF = new byte[UBS_FILE_SIZE_CNPJ_CPF];
                byte[] rgbyCEI = new byte[UBS_FILE_SIZE_CEI];
                byte[] rgbyEmployerAddress = new byte[UBS_FILE_SIZE_EMPLOYER_ADDRESS];
                byte[] rgbyNFR = new byte[UBS_FILE_SIZE_NFR];

                for (int iIdx = 0; iIdx < strUBSFilePath.Length; iIdx++)
                {
                    rgbyUBSFilePath[iIdx] = (byte)strUBSFilePath[iIdx];
                }

                for (int iIdx = 0; iIdx < rgbyEmployerName.Length; iIdx++)
                {
                    if (iIdx < strEmployerName.Length)
                    {
                        rgbyEmployerName[iIdx] = (byte)strEmployerName[iIdx];
                    }
                    else
                    {
                        rgbyEmployerName[iIdx] = (byte)END_STRING;
                    }
                }

                for (int iIdx = 0; iIdx < rgbyCNPJ_CPF.Length; iIdx++)
                {
                    if (iIdx < strCNPJ_CPF.Length)
                    {
                        rgbyCNPJ_CPF[iIdx] = (byte)strCNPJ_CPF[iIdx];
                    }
                    else
                    {
                        rgbyCNPJ_CPF[iIdx] = (byte)END_STRING;
                    }
                }

                for (int iIdx = 0; iIdx < rgbyCEI.Length; iIdx++)
                {
                    if (iIdx < strCEI.Length)
                    {
                        rgbyCEI[iIdx] = (byte)strCEI[iIdx];
                    }
                    else
                    {
                        rgbyCEI[iIdx] = (byte)END_STRING;
                    }
                }

                for (int iIdx = 0; iIdx < rgbyEmployerAddress.Length; iIdx++)
                {
                    if (iIdx < strEmployerAddress.Length)
                    {
                        rgbyEmployerAddress[iIdx] = (byte)strEmployerAddress[iIdx];
                    }
                    else
                    {
                        rgbyEmployerAddress[iIdx] = (byte)END_STRING;
                    }
                }

                if (strNFR == "")
                {
                    for (int iIdx = 0; iIdx < rgbyNFR.Length; iIdx++)
                    {
                        rgbyNFR[iIdx] = (byte)END_STRING;
                    }
                }
                else
                {
                    strNFR = strNFR.PadLeft(rgbyNFR.Length, '0');

                    for (int iIdx = 0; iIdx < rgbyNFR.Length; iIdx++)
                    {
                        rgbyNFR[iIdx] = (byte)strNFR[iIdx];
                    }
                }

                return this.FCreateUBSFile(ref rgbyUBSFilePath[0], uiQuantityUsers, ref rgbyEmployerName[0], ref rgbyCNPJ_CPF[0], ref rgbyCEI[0], ref rgbyEmployerAddress[0], ref rgbyNFR[0]);
            }

            /// <summary>
            /// Abre um arquivo .ubs informado e lê seu cabeçalho
            /// </summary>
            /// <param name="strUBSFilePath">Endereço do arquivo .ubs</param>
            /// <param name="uiQuantityUsers">Quantidade de usuários contidos no arquivo</param>
            /// <param name="strEmployerName">Nome da empresa/empregador do equipamento de onde foram exportados os usuários</param>
            /// <param name="strCNPJ_CPF">CNPJ ou CPF da empresa/empregador do equipamento de onde foram exportados os usuários</param>
            /// <param name="ulCEI">CEI da empresa/empregador do equipamento de onde foram exportados os usuários</param>
            /// <param name="strEmployerAddress">Endereço da empresa/empregador do equipamento de onde foram exportados os usuários</param>
            /// <param name="strNFR">NFR do equipamento de onde foram exportados os usuários</param>
            /// <returns></returns>
            public int OpenUBSFile(string strUBSFilePath, ref uint uiQuantityUsers, ref string strEmployerName, ref string strCNPJ_CPF, ref ulong ulCEI, ref string strEmployerAddress, ref string strNFR)
            {
                int iReturn = 0;

                byte[] rgbyUBSFilePath = new byte[strUBSFilePath.Length];
                byte[] rgbyEmployerName = new byte[UBS_FILE_SIZE_EMPLOYER_NAME];
                byte[] rgbyCNPJ_CPF = new byte[UBS_FILE_SIZE_CNPJ_CPF];
                byte[] rgbyCEI = new byte[UBS_FILE_SIZE_CEI];
                byte[] rgbyEmployerAddress = new byte[UBS_FILE_SIZE_EMPLOYER_ADDRESS];
                byte[] rgbyNFR = new byte[UBS_FILE_SIZE_NFR];

                for (int iIdx = 0; iIdx < strUBSFilePath.Length; iIdx++)
                {
                    rgbyUBSFilePath[iIdx] = (byte)strUBSFilePath[iIdx];
                }

                iReturn = this.FOpenUBSFile(ref rgbyUBSFilePath[0], ref uiQuantityUsers, ref rgbyEmployerName[0], ref rgbyCNPJ_CPF[0], ref rgbyCEI[0], ref rgbyEmployerAddress[0], ref rgbyNFR[0]);

                if (iReturn != 0)
                {
                    return iReturn;
                }

                string strCEI = "";

                for (int iIdx = 0; iIdx < rgbyCNPJ_CPF.Length; iIdx++)
                {
                    if (rgbyCNPJ_CPF[iIdx] == (byte)END_STRING)
                    {
                        break;
                    }

                    strCNPJ_CPF += (char)rgbyCNPJ_CPF[iIdx];
                }

                strCNPJ_CPF = strCNPJ_CPF.Trim();

                for (int iIdx = 0; iIdx < rgbyCEI.Length; iIdx++)
                {
                    if (rgbyCEI[iIdx] == (byte)END_STRING)
                    {
                        break;
                    }

                    strCEI += (char)rgbyCEI[iIdx];
                }

                strCEI = strCEI.Trim();

                if (strCEI == "")
                {
                    ulCEI = 0;
                }
                else
                {
                    ulCEI = ulong.Parse(strCEI);
                }

                for (int iIdx = 0; iIdx < rgbyEmployerName.Length; iIdx++)
                {
                    if (rgbyEmployerName[iIdx] == (byte)END_STRING)
                    {
                        break;
                    }

                    strEmployerName += (char)rgbyEmployerName[iIdx];
                }

                strEmployerName = strEmployerName.Trim();

                for (int iIdx = 0; iIdx < rgbyEmployerAddress.Length; iIdx++)
                {
                    if (rgbyEmployerAddress[iIdx] == (byte)END_STRING)
                    {
                        break;
                    }

                    strEmployerAddress += (char)rgbyEmployerAddress[iIdx];
                }

                strEmployerAddress = strEmployerAddress.Trim();

                for (int iIdx = 0; iIdx < rgbyNFR.Length; iIdx++)
                {
                    if (rgbyNFR[iIdx] == (byte)END_STRING)
                    {
                        break;
                    }

                    strNFR += (char)rgbyNFR[iIdx];
                }

                return iReturn;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="strPIS"></param>
            /// <param name="strUserName"></param>
            /// <param name="uiKeyCode"></param>
            /// <param name="strBarCode"></param>
            /// <param name="uiProxCode"></param>
            /// <param name="byStatus"></param>
            /// <param name="strPassword"></param>
            /// <param name="rgbyBiometrics"></param>
            /// <param name="uiSizeSample"></param>
            /// <returns></returns>
            public int ExportUBS_User(string strPIS, string strUserName, uint uiKeyCode, string strBarCode, ulong ulProxCode, byte byStatus, string strPassword, byte byAccessType, byte[] rgbyBiometrics, uint uiSizeSample)
            {
                byte[] rgbyPIS = new byte[UBS_FILE_SIZE_PIS];
                byte[] rgbyUserName = new byte[UBS_FILE_SIZE_USERNAME];
                byte[] rgbyBarCode = new byte[UBS_FILE_SIZE_BARCODE];
                byte[] rgbyPassword = new byte[UBS_FILE_SIZE_PASSWORD];

                for (int iIdx = 0; iIdx < rgbyPIS.Length; iIdx++)
                {
                    if (iIdx < strPIS.Length)
                    {
                        rgbyPIS[iIdx] = (byte)strPIS[iIdx];
                    }
                    else
                    {
                        rgbyPIS[iIdx] = (byte)END_STRING;
                    }
                }

                for (int iIdx = 0; iIdx < rgbyUserName.Length; iIdx++)
                {
                    if (iIdx < strUserName.Length)
                    {
                        rgbyUserName[iIdx] = (byte)strUserName[iIdx];
                    }
                    else
                    {
                        rgbyUserName[iIdx] = (byte)END_STRING;
                    }
                }

                if (strBarCode != null)
                {
                    strBarCode = strBarCode.PadLeft(rgbyBarCode.Length, '0');

                    for (int iIdx = 0; iIdx < strBarCode.Length; iIdx++)
                    {
                        rgbyBarCode[iIdx] = (byte)strBarCode[iIdx];
                    }
                }
                else
                {
                    for (int i = 0; i < rgbyBarCode.Length; i++)
                    {
                        rgbyBarCode[i] = (byte)END_STRING;
                    }
                }

                if (strPassword == "0" || strPassword == " ")
                {
                    for (int iIdx = 0; iIdx < 8; iIdx++)
                    {
                        rgbyPassword[iIdx] = (byte)END_STRING;
                    }
                }
                else
                {
                    for (int iIdx = 0; iIdx < 8; iIdx++)
                    {
                        if (iIdx < strPassword.Length)
                        {
                            rgbyPassword[iIdx] = (byte)strPassword[iIdx];
                        }
                        else
                        {
                            rgbyPassword[iIdx] = (byte)END_STRING;
                        }
                    }
                }

                uint uiProx = (uint)ulProxCode;

                return this.FExportUBS_User(ref rgbyPIS[0], ref rgbyUserName[0], uiKeyCode, ref rgbyBarCode[0], uiProx, byStatus, ref rgbyPassword[0], byAccessType, ref rgbyBiometrics[0], uiSizeSample);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="strPIS"></param>
            /// <param name="strUserName"></param>
            /// <param name="uiKeyCode"></param>
            /// <param name="strBarCode"></param>
            /// <param name="uiProxCode"></param>
            /// <param name="byUserType"></param>
            /// <param name="strPassword"></param>
            /// <param name="rgbyBiometrics"></param>
            /// <param name="uiSizeSample"></param>
            /// <param name="byAccessType"></param>
            /// <returns></returns>
            public int ImportUBS_User(ref string strPIS, ref string strUserName, ref uint uiKeyCode, ref string strBarCode, ref ulong ulProxCode, ref byte byUserType, ref string strPassword, ref byte[] rgbyBiometrics, ref uint uiSizeSample, ref byte byAccessType)
            {
                int iReturn = 0;

                byte[] rgbyPIS = new byte[UBS_FILE_SIZE_PIS];
                byte[] rgbyUserName = new byte[UBS_FILE_SIZE_USERNAME];
                byte[] rgbyBarCode = new byte[UBS_FILE_SIZE_BARCODE];
                byte[] rgbyPassword = new byte[UBS_FILE_SIZE_PASSWORD];

                uint uiProx = 0;

                iReturn = this.FImportUBS_User(ref rgbyPIS[0], ref rgbyUserName[0], ref uiKeyCode, ref rgbyBarCode[0], ref uiProx, ref byUserType, ref rgbyPassword[0], ref rgbyBiometrics[0], ref uiSizeSample, ref byAccessType);

                if (iReturn != 0)
                {
                    return iReturn;
                }

                ulProxCode = uiProx;

                strPIS = "";

                for (int iIdx = 0; iIdx < rgbyPIS.Length; iIdx++)
                {
                    if (rgbyPIS[iIdx] == (byte)END_STRING)
                    {
                        break;
                    }

                    strPIS += (char)rgbyPIS[iIdx];
                }

                strPIS = strPIS.Trim();

                strUserName = "";

                for (int iIdx = 0; iIdx < rgbyUserName.Length; iIdx++)
                {
                    if (rgbyUserName[iIdx] == (byte)END_STRING)
                    {
                        break;
                    }

                    strUserName += (char)rgbyUserName[iIdx];
                }

                strUserName = strUserName.Trim();

                strBarCode = "";

                for (int iIdx = 0; iIdx < rgbyBarCode.Length; iIdx++)
                {
                    strBarCode += (char)rgbyBarCode[iIdx];
                }

                if (strBarCode == "00000000000000000000")
                {
                    strBarCode = "";
                }

                strPassword = "";

                for (int iIdx = 0; iIdx < rgbyPassword.Length; iIdx++)
                {
                    if (rgbyPassword[iIdx] == (byte)END_STRING)
                    {
                        break;
                    }

                    strPassword += (char)rgbyPassword[iIdx];
                }

                strPassword = strPassword.Trim();

                return iReturn;
            }

            /// <summary>
            /// Fecha um arquivo UBS
            /// </summary>
            /// <returns></returns>
            public int CloseUBSFile()
            {
                return this.FCloseUBSFile();
            }

            /// <summary>
            /// Abre um arquivo AFD
            /// </summary>
            /// <param name="strAFDFilePath">Endereço do arquivo AFD</param>
            /// <returns></returns>
            public int OpenAFDFile(string strAFDFilePath)
            {
                byte[] rgbyPath = new byte[strAFDFilePath.Length];

                for (int iIdx = 0; iIdx < strAFDFilePath.Length; iIdx++)
                {
                    rgbyPath[iIdx] = (byte)strAFDFilePath[iIdx];
                }

                return this.FOpenAFDFile(ref rgbyPath[0]);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public int ImportAFD_Register()
            {
                return this.FImportAFD_Register();
            }

            /// <summary>
            /// Obtém os dados do Header de um arquivo AFD
            /// </summary>
            /// <param name="uiNSR">Receberá o NSR</param>
            /// <param name="byRegType">Receberá o tipo do registro</param>
            /// <param name="byIdentifyType">Receberá o tipo de identificador da empresa/empregador (CNPJ ou CPJ) do equipamento de onde foi extraído o AFD</param>
            /// <param name="strCNPJ_CPF">Receberá o identificador da empresa/empregador (CNPJ ou CPJ) do equipamento de onde foi extraído o AFD</param>
            /// <param name="ulCEI">Receberá o CEI da empresa/empregador do equipamento de onde foi extraído o AFD</param>
            /// <param name="strEmployerName">Receberá o nome da empresa/empregador (CNPJ ou CPJ) do equipamento de onde foi extraído o AFD</param>
            /// <param name="strNFR">Receberá o NFR do equipamento de onde foi extraído o AFD</param>
            /// <param name="dtmStartDate">Receberá a data do primeiro registro do AFD</param>
            /// <param name="dtmEndDate">Receberá a data do último registro do AFD</param>
            /// <param name="dtmRegDateTime">Receberá a data de geração do AFD</param>
            public void GetAFD_Header(ref uint uiNSR, ref byte byRegType, ref byte byIdentifyType, ref string strCNPJ_CPF, ref ulong ulCEI, ref string strEmployerName, ref string strNFR, ref DateTime dtmStartDate, ref DateTime dtmEndDate, ref DateTime dtmRegDateTime)
            {
                byte[] rgbyCNPJ_CPF = new byte[FIELD_SIZE_CNPJ];
                byte[] rgbyCEI = new byte[FIELD_SIZE_CEI];
                byte[] rgbyEmployerName = new byte[FIELD_SIZE_EMPLOYER_NAME];
                byte[] rgbyNFR = new byte[FIELD_SIZE_NFR];
                byte byStartDateDay = 0;
                byte byStartDateMonth = 0;
                ushort usStartDateYear = 0;
                byte byEndDateDay = 0;
                byte byEndDateMonth = 0;
                ushort usEndDateYear = 0;
                byte byRegDateDay = 0;
                byte byRegDateMonth = 0;
                ushort usRegDateYear = 0;
                byte byRegTimeHour = 0;
                byte byRegTimeMin = 0;

                try
                {
                    this.FGetAFD_Header(ref uiNSR, ref byRegType, ref byIdentifyType, ref rgbyCNPJ_CPF[0], ref rgbyCEI[0], ref rgbyEmployerName[0], ref rgbyNFR[0], ref byStartDateDay, ref byStartDateMonth, ref usStartDateYear, ref byEndDateDay, ref byEndDateMonth, ref usEndDateYear, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin);
                }
                catch (Exception exError)
                {
                    throw exError;
                }

                for (int iIdx = 0; iIdx < rgbyCNPJ_CPF.Length; iIdx++)
                {
                    if ((char)rgbyCNPJ_CPF[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strCNPJ_CPF += (char)rgbyCNPJ_CPF[iIdx];
                }

                if (byIdentifyType == (byte)enumIdentify_Type.CNPJ)
                {
                    strCNPJ_CPF = strCNPJ_CPF.PadLeft(FIELD_SIZE_CNPJ, '0');
                }
                else
                {
                    strCNPJ_CPF = strCNPJ_CPF.PadLeft(FIELD_SIZE_CPF, '0');
                }

                string strCEI = "";

                for (int iIdx = 0; iIdx < rgbyCEI.Length; iIdx++)
                {
                    if ((char)rgbyCEI[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strCEI += (char)rgbyCEI[iIdx];
                }

                if (strCEI == "")
                {
                    ulCEI = 0;
                }
                else
                {
                    ulCEI = ulong.Parse(strCEI);
                }

                for (int iIdx = 0; iIdx < rgbyEmployerName.Length; iIdx++)
                {
                    string strTemp = "";
                    
                    if ((char)rgbyEmployerName[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strTemp += (char)rgbyEmployerName[iIdx];

                    strEmployerName += strTemp;
                }

                strEmployerName = strEmployerName.Trim();

                //StreamWriter swFileOut = null;
                //swFileOut = new StreamWriter("DebugLog_Default.txt", false, Encoding.Default);
                //swFileOut.WriteLine(strEmployerName);
                //swFileOut.Close();

                for (int iIdx = 0; iIdx < rgbyNFR.Length; iIdx++)
                {
                    strNFR += (char)rgbyNFR[iIdx];
                }

                if (usStartDateYear == 0 || byStartDateMonth == 0 || byStartDateDay == 0)
                {
                    dtmStartDate = new DateTime();
                }
                else
                {
                    dtmStartDate = new DateTime((int)usStartDateYear, (int)byStartDateMonth, (int)byStartDateDay);
                }

                if (usEndDateYear == 0 || byEndDateMonth == 0 || byEndDateDay == 0)
                {
                    dtmEndDate = new DateTime();
                }
                else
                {
                    dtmEndDate = new DateTime((int)usEndDateYear, (int)byEndDateMonth, (int)byEndDateDay);
                }

                if (usRegDateYear == 0 || byRegDateMonth == 0 || byRegDateDay == 0)
                {
                    dtmRegDateTime = new DateTime();
                }
                else
                {
                    dtmRegDateTime = new DateTime((int)usRegDateYear, (int)byRegDateMonth, (int)byRegDateDay, (int)byRegTimeHour, (int)byRegTimeMin, 0);
                }
            }

            /// <summary>
            /// Obtém os dados do Trailer de um arquivo AFD
            /// </summary>
            /// <param name="uiNSR">Receberá o NSR</param>
            /// <param name="byRegType">Receberá o tipo do registro</param>
            /// <param name="uiQuantityRegType2">Receberá a quantidade de registros do tipo 2</param>
            /// <param name="uiQuantityRegType3">Receberá a quantidade de registros do tipo 3</param>
            /// <param name="uiQuantityRegType4">Receberá a quantidade de registros do tipo 4</param>
            /// <param name="uiQuantityRegType5">Receberá a quantidade de registros do tipo 5</param>
            public void GetAFD_Trailer(ref uint uiNSR, ref byte byRegType, ref uint uiQuantityRegType2, ref uint uiQuantityRegType3, ref uint uiQuantityRegType4, ref uint uiQuantityRegType5)
            {
                try
                {
                    this.FGetAFD_Trailer(ref uiNSR, ref byRegType, ref uiQuantityRegType2, ref uiQuantityRegType3, ref uiQuantityRegType4, ref uiQuantityRegType5);
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            /// <summary>
            /// Fecha um arquivo AFD
            /// </summary>
            /// <returns></returns>
            public int CloseAFDFile()
            {
                return this.FCloseAFDFile();
            }

        #endregion
    }
}

