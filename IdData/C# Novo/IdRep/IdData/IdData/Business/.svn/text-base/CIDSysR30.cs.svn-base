using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace DLL_IDSysR30
{
    /// <summary>
    /// Ver descrição das funções e paramêtros no manual da DLL.
    /// </summary>
    public class CIDSysR30
    {
        #region Defines

            public const string DLL_PATH = "";

            private const byte PRODUCT_ID_REP = 1;

            private const ushort BUFFER_HEADER_SIZE = 15;
            private const ushort BUFFER_SIZE_REGTYPE_2 = 304;
            private const ushort BUFFER_SIZE_REGTYPE_3 = 38;
            private const ushort BUFFER_SIZE_REGTYPE_4 = 33;
            private const ushort BUFFER_SIZE_REGTYPE_5 = 91;
            private const ushort BUFFER_SIZE_CMD_REQUEST_OLDEST_EVENT = 15;
            private const ushort BUFFER_SIZE_CMD_MRP_POINTER_INCREMENT = 15;
            private const ushort BUFFER_SIZE_CMD_ADD_USER = 112;
            private const ushort BUFFER_SIZE_CMD_CHANGE_USER_DATA = 134;
            private const ushort BUFFER_SIZE_CMD_DELETE_USER = 27;
            private const ushort BUFFER_SIZE_CMD_READ_USER_DATA = 27;
            private const ushort BUFFER_SIZE_CMD_SET_EMPLOYER = 293;
            private const ushort BUFFER_SIZE_CMD_READ_EMPLOYER_DATA = 15;
            private const ushort BUFFER_SIZE_CMD_SET_DATE_TIME = 23;
            private const ushort BUFFER_SIZE_CMD_SET_REP_COMMUNICATION = 37;
            private const ushort BUFFER_SIZE_CMD_READ_REP_COMMUNICATION = 15;
            private const ushort BUFFER_SIZE_CMD_REQUEST_TOTAL_EVENTS = 15;
            private const ushort BUFFER_SIZE_CMD_REQUEST_EVENT_BY_NSR = 20;
            private const ushort BUFFER_SIZE_CMD_REQUEST_TOTAL_USERS = 15;
            private const ushort BUFFER_SIZE_CMD_READ_USER_DATA_BY_INDEX = 20;
            private const ushort BUFFER_SIZE_CMD_REQUEST_NFR = 15;
            private const ushort BUFFER_SIZE_NFR = 17;

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
            private const byte FIELD_SIZE_BARCODE = 10;
            private const byte FIELD_SIZE_IP_EQUIPMENT = 4;
            private const byte FIELD_SIZE_SUBNET_MASK = 4;
            private const byte FIELD_SIZE_IP_GATEWAY = 4;
            private const byte FIELD_SIZE_IP_SERVER = 4;
            private const ushort MAX_SIZE_BIOMETRIC_SAMPLE = 404;
            private const uint MAX_SIZE_PHOTO = 30720;
            private const byte FIELD_SIZE_PIS_EXPORT_FILE = 11;
            private const byte FIELD_SIZE_KEYCODE_EXPORT_FILE = 6;
            private const byte FIELD_SIZE_PASSWORD_EXPORT_FILE = 8;

            private const ushort UBS_FILE_SIZE_CNPJ_CPF = 14;
            private const byte UBS_FILE_SIZE_CEI = 12;
            private const byte UBS_FILE_SIZE_EMPLOYER_NAME = 150;
            private const byte UBS_FILE_SIZE_EMPLOYER_ADDRESS = 100;
            private const byte UBS_FILE_SIZE_NFR = 17;

        #endregion

        #region Native Methods

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
        
        #region Variables

            /// <summary>
            /// Criamos um ponteiro para a DLL
            /// </summary>
            private IntPtr DLL_IDSysR30;

        #endregion

        #region Constructor

            /// <summary>
            /// No construtor da classe, inicializamos o ponteiro para a DLL e para as funções
            /// </summary>
            public CIDSysR30()
            {
                try
                {
                    this.DLL_IDSysR30 = IntPtr.Zero;

                    string strReturn = this.InitializeDll();

                    if (strReturn != "")
                    {
                        if (strReturn == "1")
                        {
                            throw new Exception("Não foi possível carregar a DLL IDSysR30.\nVerifique se o arquivo está na pasta do executavel ou se a versão está atualizada.");
                        }
                        else
                        {
                            throw new Exception("Erro ao carregar a função " + strReturn + " da DLL IDSysR30.");
                        }
                    }
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

        #endregion

        #region Destructor

            /// <summary>
            /// No destrutor da classe, a DLL é liberada da memória
            /// </summary>
            ~CIDSysR30()
            {
                NativeMethods.FreeLibrary(DLL_IDSysR30);
            }

        #endregion

        #region AccessDll

            // 
            // Para cada função da DLL, criamos aqui um Delegate, que seria uma espécie de ponteiro para uma função.
            // Criamos em seguida uma variavel do tipo delegate definida logo acima.
            // 

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TAddUser(byte byAddress, byte byProduct, ref byte rgbyBuffer, ref byte rgbyPIS, ref byte _rgbyUsername, uint uiKeyCode, ref byte rgbyBarCode, byte byFacilityCode, uint uiProxCode, byte byStatus, byte byUserType, ref byte rgbyPassword, ushort usPhotoSize, ref byte rgbyPhoto, ushort usBiometricsSize, byte byQuantitySamples, ref byte rgbyBiometrics);
            TAddUser FAddUser;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TChangeUserData(byte byAddress, byte byProduct, ref byte rgbyBuffer, ref byte rgbyPIS, ref byte rgbyNewPIS, ref byte rgbyUsername, ref byte rgbyBarCode, byte byFacilityCode, uint uiProxCode, byte byStatus, byte byUserType, ref byte rgbyPassword, ushort usPhotoSize, ref byte rgbyPhoto, ushort usBiometricsSize, byte byQuantitySamples, ref byte rgbyBiometrics, uint uiKeyCode);
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
            private delegate void TSetREPCommunication(byte byAddress, byte byProduct, ref byte rgbyBuffer, byte byCommunicationType, ref byte rgbyIPEquipment, ref byte rgbySubnetMask, ref byte rgbyIPGateway, uint uiTCPPort_Comm, uint uiTCPPort_Alarm, byte byBaudrate, byte _bySerialAddress, byte byMulticastAddress, byte byBroadcastAddress);
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
            private delegate void TRequestTotalNSR(byte byAddress, byte byProduct, ref byte rgbyBuffer);
            TRequestTotalNSR FRequestTotalNSR;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestTotalUsers(byte byaddress, byte byproduct, ref byte rgbybuffer);
            TRequestTotalUsers FRequestTotalUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TRequestUserByIndex(byte byAddress, byte byProduct, ref byte rgbyBuffer, uint uiIndex);
            TRequestUserByIndex FRequestUserByIndex;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TPacketAvail(ref byte Buffer);
            TPacketAvail FPacketAvail;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetUserData(ref byte rgbyPIS, ref byte rgbyUsername, ref uint uiKeyCode, ref byte rgbyBarCode, ref byte byFacilityCode, ref ushort usProxCode, ref byte byStatus, ref byte byUserType, ref byte rgbyPassword, ref ushort usPhotoSize, ref byte rgbyPhoto, ref ushort usBiometricSize, ref byte byQuantitySamples, ref byte rgbyBiometric_Sample1, ref byte rgbyBiometric_Sample2);
            TGetUserData FGetUserData;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetEmployerData(ref byte byIdentifyType, ref byte rgbyCNPJ_CPF, ref byte rgbyCEI, ref byte rgbyEmployerName, ref byte rgbyEmployerAddress);
            TGetEmployerData FGetEmployerData;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetLogType2(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte _byRegTimeMin, ref byte byIdentifyType, ref byte rgbyCNPJ_CPF, ref byte rgbyCEI, ref byte rgbyEmployerName, ref byte rgbyEmployerAddress);
            TGetLogType2 FGetLogType2;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetLogType3(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref byte rgbyPIS);
            TGetLogType3 FGetLogType3;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetLogType4(ref uint uiNSR, ref byte byRegType, ref byte byDayBeforeAdjust, ref byte byMonthBeforeAdjust, ref ushort usYearBeforeAdjust, ref byte byHourBeforeAdjust, ref byte byMinuteBeforeAdjust, ref byte byDayAfterAdjust, ref byte byMonthAfterAdjust, ref ushort usYearAfterAdjust, ref byte byHourAfterAdjust, ref byte byMinuteAfterAdjust);
            TGetLogType4 FGetLogType4;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetLogType5(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref byte byOpType, ref byte rgbyPIS, ref byte rgbyUsername);
            TGetLogType5 FGetLogType5;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetLogType6(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref byte rgbyPIS, ref byte byEvent);
            TGetLogType6 FGetLogType6;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetREPCommunication(ref byte byCommunicationType, ref byte rgbyIPEquipment, ref byte rgbySubnetMask, ref byte rgbyIPGateway, ref ushort usTCPPort_Comm, ref ushort usTCPPort_Alarm, ref byte _byBaudrate, ref byte bySerialAddress, ref byte byMulticastAddress, ref byte byBroadcastAddress);
            TGetREPCommunication FGetREPCommunication;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetNFR(ref byte rgbyNFR);
            TGetNFR FGetNFR;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTotalNSR(ref uint uiTotalNSR);
            TGetTotalNSR FGetTotalNSR;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TGetTotalUsers(ref uint uiTotalUsers);
            TGetTotalUsers FGetTotalUsers;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate bool TIsHamsterConnected();
            TIsHamsterConnected FIsHamsterConnected;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate bool TGetTemplates_FIM01(ref byte rgbySample404_1, ref byte byFingerPID_1, ref byte bySampleID_1, ref byte byQuality_1, ref byte rgbySample404_2, ref byte byFingerPID_2, ref byte bySampleID_2, ref byte byQuality_2, byte byRotateSamples);
            TGetTemplates_FIM01 FGetTemplates_FIM01;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate bool TGetTemplates_FIM10(ref byte rgbySample400_1, ref byte byFingerPID_1, ref byte bySampleID_1, ref byte byQuality_1, ref byte rgbySample400_2, ref byte byFingerPID_2, ref byte bySampleID_2, ref byte byQuality_2, byte byRotateSamples);
            TGetTemplates_FIM10 FGetTemplates_FIM10;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TFIM01_To_FIM10(ref byte rgbyTemplate404, ref byte rgbyTemplate400);
            TFIM01_To_FIM10 FFIM01_To_FIM10;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TFIM10_To_FIM01(ref byte rgbyTemplate400, ref byte rgbyTemplate404);
            TFIM10_To_FIM01 FFIM10_To_FIM01;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TCreateFile(ref byte rgbyPath, uint uiAmountUsers, ref byte rgbyEmployerName, ref byte rgbyCNPJ_CPF, ref byte rgbyCEI, ref byte rgbyEmployerAddress, ref byte rgbyNFR);
            TCreateFile FCreateFile;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TFileOpen(ref byte rgbyPath, ref uint uiAmountUsers, ref byte rgbyEmployerName, ref byte rgbyCNPJ_CPF, ref byte rgbyCEI, ref byte rgbyEmployerAddress, ref byte rgbyNFR);
            TFileOpen FFileOpen;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TFileClose();
            TFileClose FFileClose;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TEncryptBuffer(ref byte rgbyBuffer, uint uiSize);
            TEncryptBuffer FEncryptBuffer;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TDecryptBuffer(ref byte rgbyBuffer, uint uiSize);
            TDecryptBuffer FDecryptBuffer;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate void TExportUbsUser(ref byte rgbyPIS, ref byte rgbyUsername, ref byte rgbyKeyCode, ref byte rgbyBarCode, uint uiProxCode, byte byUserType, ref byte rgbyPassword, ref byte rgbyBiometrics, uint uiSizeSample);
            TExportUbsUser FExportUbsUser;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TImportUbsUser(ref byte rgbyPIS, ref byte rgbyUsername, ref byte rgbyKeyCode, ref byte rgbyBarCode, ref uint uiProxCode, ref byte byUserType, ref byte rgbyPassword, ref byte rgbyBiometrics, ref uint uiSizeSample);
            TImportUbsUser FImportUbsUser;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TFileOpenAFD(ref byte path);
            TFileOpenAFD FFileOpenAFD;

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            private delegate int TImportAFD(ref byte rgbyBuffer);
            TImportAFD FImportAFD;

        #endregion

        #region InitializeDll

            public string InitializeDll()
            {
                string strReturn = "";

                try
                {
                    DLL_IDSysR30 = NativeMethods.LoadLibrary("IDSysR30.dll");

                    if (DLL_IDSysR30 == IntPtr.Zero)
                    {
                        strReturn = "1";
                    }
                    else
                    {
                        if ((this.FAddUser = (TAddUser)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "AddUser"), typeof(TAddUser))) == null) return "AddUser";
                        if ((this.FChangeUserData = (TChangeUserData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "ChangeUserData"), typeof(TChangeUserData))) == null) return "ChangeUserData";
                        if ((this.FDeleteUser = (TDeleteUser)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "DeleteUser"), typeof(TDeleteUser))) == null) return "DeleteUser";
                        if ((this.FReadUserData = (TReadUserData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "ReadUserData"), typeof(TReadUserData))) == null) return "ReadUserData";
                        if ((this.FSetEmployer = (TSetEmployer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "SetEmployer"), typeof(TSetEmployer))) == null) return "SetEmployer";
                        if ((this.FReadEmployerData = (TReadEmployerData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "ReadEmployerData"), typeof(TReadEmployerData))) == null) return "ReadEmployerData";
                        if ((this.FSetDateTime = (TSetDateTime)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "SetDateTime"), typeof(TSetDateTime))) == null) return "SetDateTime";
                        if ((this.FSetREPCommunication = (TSetREPCommunication)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "SetREPCommunication"), typeof(TSetREPCommunication))) == null) return "SetREPCommunication";
                        if ((this.FReadREPCommunication = (TReadREPCommunication)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "ReadREPCommunication"), typeof(TReadREPCommunication))) == null) return "ReadREPCommunication";
                        if ((this.FRequestEventByNSR = (TRequestEventByNSR)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "RequestEventByNSR"), typeof(TRequestEventByNSR))) == null) return "RequestEventByNSR";
                        if ((this.FRequestNFR = (TRequestNFR)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "RequestNFR"), typeof(TRequestNFR))) == null) return "RequestNFR";
                        if ((this.FRequestTotalNSR = (TRequestTotalNSR)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "RequestTotalNSR"), typeof(TRequestTotalNSR))) == null) return "RequestTotalNSR";
                        if ((this.FRequestTotalUsers = (TRequestTotalUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "RequestTotalUsers"), typeof(TRequestTotalUsers))) == null) return "RequestTotalUsers";
                        if ((this.FRequestUserByIndex = (TRequestUserByIndex)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "RequestUserByIndex"), typeof(TRequestUserByIndex))) == null) return "RequestUserByIndex";
                        if ((this.FPacketAvail = (TPacketAvail)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "PacketAvail"), typeof(TPacketAvail))) == null) return "PacketAvail";

                        if ((this.FGetUserData = (TGetUserData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetUserData"), typeof(TGetUserData))) == null) return "GetUserData";
                        if ((this.FGetEmployerData = (TGetEmployerData)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetEmployerData"), typeof(TGetEmployerData))) == null) return "GetEmployerData";
                        if ((this.FGetLogType2 = (TGetLogType2)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetLogType2"), typeof(TGetLogType2))) == null) return "GetLogType2";
                        if ((this.FGetLogType3 = (TGetLogType3)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetLogType3"), typeof(TGetLogType3))) == null) return "GetLogType3";
                        if ((this.FGetLogType4 = (TGetLogType4)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetLogType4"), typeof(TGetLogType4))) == null) return "GetLogType4";
                        if ((this.FGetLogType5 = (TGetLogType5)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetLogType5"), typeof(TGetLogType5))) == null) return "GetLogType5";
                        if ((this.FGetREPCommunication = (TGetREPCommunication)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetREPCommunication"), typeof(TGetREPCommunication))) == null) return "GetREPCommunication";
                        if ((this.FGetNFR = (TGetNFR)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetNFR"), typeof(TGetNFR))) == null) return "GetNFR";
                        if ((this.FGetTotalNSR = (TGetTotalNSR)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetTotalNSR"), typeof(TGetTotalNSR))) == null) return "GetTotalNSR";
                        if ((this.FGetTotalUsers = (TGetTotalUsers)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetTotalUsers"), typeof(TGetTotalUsers))) == null) return "GetTotalUsers";

                        if ((this.FGetLogType6 = (TGetLogType6)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetLogType6"), typeof(TGetLogType6))) == null) return "GetLogType6";

                        if ((this.FIsHamsterConnected = (TIsHamsterConnected)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "IsHamsterConnected"), typeof(TIsHamsterConnected))) == null) return "IsHamsterConnected";
                        if ((this.FGetTemplates_FIM01 = (TGetTemplates_FIM01)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetTemplates_FIM01"), typeof(TGetTemplates_FIM01))) == null) return "GetTemplates_FIM01";
                        if ((this.FGetTemplates_FIM10 = (TGetTemplates_FIM10)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "GetTemplates_FIM10"), typeof(TGetTemplates_FIM10))) == null) return "GetTemplates_FIM10";
                        if ((this.FFIM01_To_FIM10 = (TFIM01_To_FIM10)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "FIM01_To_FIM10"), typeof(TFIM01_To_FIM10))) == null) return "FIM01_To_FIM10";
                        if ((this.FFIM10_To_FIM01 = (TFIM10_To_FIM01)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "FIM10_To_FIM01"), typeof(TFIM10_To_FIM01))) == null) return "FIM10_To_FIM01";

                        if ((this.FCreateFile = (TCreateFile)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "CreateFile"), typeof(TCreateFile))) == null) return "CreateFile";
                        if ((this.FFileOpen = (TFileOpen)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "FileOpen"), typeof(TFileOpen))) == null) return "FileOpen";
                        if ((this.FFileClose = (TFileClose)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "FileClose"), typeof(TFileClose))) == null) return "FileClose";
                        if ((this.FEncryptBuffer = (TEncryptBuffer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "EncryptBuffer"), typeof(TEncryptBuffer))) == null) return "EncryptBuffer";
                        if ((this.FDecryptBuffer = (TDecryptBuffer)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "DecryptBuffer"), typeof(TDecryptBuffer))) == null) return "DecryptBuffer";
                        if ((this.FExportUbsUser = (TExportUbsUser)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "ExportUbsUser"), typeof(TExportUbsUser))) == null) return "ExportUbsUser";
                        if ((this.FImportUbsUser = (TImportUbsUser)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "ImportUbsUser"), typeof(TImportUbsUser))) == null) return "ImportUbsUser";
                        if ((this.FFileOpenAFD = (TFileOpenAFD)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "FileOpenAFD"), typeof(TFileOpenAFD))) == null) return "FileOpenAFD";
                        if ((this.FImportAFD = (TImportAFD)Marshal.GetDelegateForFunctionPointer(NativeMethods.GetProcAddress(DLL_IDSysR30, "ImportAFD"), typeof(TImportAFD))) == null) return "ImportAFD";
                    }
                }
                catch (Exception exError)
                {
                    throw exError;
                }

                return strReturn;
            }

        #endregion

        #region Functions

            private byte[] ConvertStringToArray(string _strValue, int _iStringSize, int _iArraySize)
            {

                string strValue = _strValue.PadLeft(_iStringSize, '0');
                byte[] rgbyValue = new byte[_iArraySize];

                for (int iIdx = 0; iIdx < _iArraySize; iIdx++)
                {
                    byte byMSB = byte.Parse(strValue.Substring((iIdx * 2), 1));
                    byte byLSB = byte.Parse(strValue.Substring(((iIdx * 2) + 1), 1));

                    rgbyValue[iIdx] = (byte)((byMSB << 4) | (byLSB & 0x0F));
                }

                return rgbyValue;
            }
        
            private string ConvertArrayToString(byte[] rgbyBarCode, int _iStringSize, int _iArraySize)
            {
                string strValue = string.Empty;
                byte[] rgbyValue = new byte[_iArraySize];

                byte byMSB = 0;
                byte byLSB = 0;

                for (int i = 0; i < _iArraySize; i++)
                {
                    byMSB = (byte)(rgbyBarCode[i] >> 4);
                    byLSB = (byte)(rgbyBarCode[i] & 0x0F);
                    strValue += string.Concat(byMSB.ToString(), byLSB.ToString());
                }

                ulong ulAssistent;
                if (ulong.TryParse(strValue, out ulAssistent))
                {
                    strValue = ulAssistent.ToString();
                }
                return strValue;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a inclusão de um novo usuário no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] AddUser(string strPIS, string strUserName, uint uiKeyCode, string strBarCode, byte byFacilityCode, uint uiProxCode, byte byUserType, string strPassword, System.IO.MemoryStream msPhoto, ushort usSizeSample, byte byQuantitySamples, byte[] rgbyBiometrics)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer;
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];
                byte[] rgbyUserName = new byte[FIELD_SIZE_USERNAME];
                byte[] rgbyBarCode = new byte[FIELD_SIZE_BARCODE];
                byte[] rgbyPassword = new byte[FIELD_SIZE_PASSWORD];
                ushort usSizePhoto = 0;
                byte[] rgbyPhoto = new byte[MAX_SIZE_PHOTO];
                byte byAux = 1;

                strPIS = strPIS.PadLeft(FIELD_SIZE_PIS, '0');

                for (int iIdx = 0; iIdx < FIELD_SIZE_PIS; iIdx++)
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


                for (int iIdx = 0; iIdx < FIELD_SIZE_USERNAME; iIdx++)
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

                rgbyBarCode = ConvertStringToArray(strBarCode, (FIELD_SIZE_BARCODE * 2), FIELD_SIZE_BARCODE);
            
                for (int iIdx = 0; iIdx < FIELD_SIZE_PASSWORD; iIdx++)
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
                byte byStatus = byUserType;

                this.FAddUser(byAddress, byProduct, ref rgbyBuffer[0], ref rgbyPIS[0], ref rgbyUserName[0], uiKeyCode, ref rgbyBarCode[0], byFacilityCode, uiProxCode, byStatus, byUserType, ref rgbyPassword[0], usSizePhoto, ref rgbyPhoto[0], usSizeSample, byQuantitySamples, ref rgbyBiometrics[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a alteração de um usuário no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ChangeUserData(string strPIS, string strNewPIS, string strUserName, uint uiKeyCode, string strBarCode, byte byFacilityCode, uint uiProxCode, byte byUserType, string strPassword, System.IO.MemoryStream msPhoto, ushort usSizeSample, byte byQuantitySamples, byte[] rgbyBiometrics)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer;
                byte byAux = 1;
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];
                byte[] rgbyNewPIS = new byte[FIELD_SIZE_PIS];
                byte[] rgbyUserName = new byte[FIELD_SIZE_USERNAME];
                byte[] rgbyPassword = new byte[FIELD_SIZE_PASSWORD];
                ushort usSizePhoto;
                byte[] rgbyPhoto = new byte[1];

                strPIS = strPIS.PadLeft(FIELD_SIZE_PIS, '0');

                for (int i = 0; i < FIELD_SIZE_PIS; i++)
                {
                    if (strPIS[i] != 0)
                    {
                        rgbyPIS[i] = (byte)strPIS[i];
                    }
                }

                strNewPIS = strNewPIS.PadLeft(FIELD_SIZE_PIS, '0');

                for (int i = 0; i < FIELD_SIZE_PIS; i++)
                {
                    if (strNewPIS[i] != 0)
                    {
                        rgbyNewPIS[i] = (byte)strNewPIS[i];
                    }
                }

                for (int iIdx = 0; iIdx < FIELD_SIZE_USERNAME; iIdx++)
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

                byte[] rgbyBarCode = ConvertStringToArray(strBarCode, (FIELD_SIZE_BARCODE * 2), FIELD_SIZE_BARCODE);

                if (msPhoto == null)
                {
                    usSizePhoto = 0;
                    Array.Resize(ref rgbyPhoto, 1);
                }
                else
                {
                    usSizePhoto = msPhoto.Length <= 1 ? (ushort)0 : (ushort)msPhoto.Length;

                    if (usSizePhoto > 0)
                    {
                        Array.Resize(ref rgbyPhoto, usSizePhoto);
                        rgbyPhoto = msPhoto.ToArray();
                    }
                }

                if (byQuantitySamples == 0 || usSizeSample == 0)
                {
                    byQuantitySamples = 0;
                    usSizeSample = 0;
                    byAux = 0;
                    Array.Resize(ref rgbyBiometrics, 1);
                }

                rgbyBuffer = new byte[BUFFER_SIZE_CMD_CHANGE_USER_DATA + usSizePhoto + (byQuantitySamples * usSizeSample) + byAux];

                // invertendo tipo de usuário e status
                byte byStatus = byUserType;

                for (int iIdx = 0; iIdx < FIELD_SIZE_PASSWORD; iIdx++)
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

                this.FChangeUserData(byAddress, byProduct, ref rgbyBuffer[0], ref rgbyPIS[0], ref rgbyNewPIS[0], ref rgbyUserName[0], ref rgbyBarCode[0], byFacilityCode, uiProxCode, byStatus, byUserType, ref rgbyPassword[0], usSizePhoto, ref rgbyPhoto[0], usSizeSample, byQuantitySamples, ref rgbyBiometrics[0], uiKeyCode);

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

                strPIS = strPIS.ToString().PadLeft(FIELD_SIZE_PIS, '0');

                for (int i = 0; i < FIELD_SIZE_PIS; i++)
                {
                    if (strPIS[i] != 0)
                    {
                        rgbyPIS[i] = (byte)strPIS[i];
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

                strPIS = strPIS.ToString().PadLeft(FIELD_SIZE_PIS, '0');

                for (int i = 0; i < FIELD_SIZE_PIS; i++)
                {
                    if (strPIS[i] != 0)
                    {
                        rgbyPIS[i] = (byte)strPIS[i];
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
            public byte[] SetEmployer(byte byIdentifyType, string strCNPJ_CPF, string strCEI, string strEmployerName, string strEmployerAddress)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer;

                byte[] rgbyCNPJ_CPF = new byte[FIELD_SIZE_CNPJ];

                if (byIdentifyType == 1)
                {
                    strCNPJ_CPF = strCNPJ_CPF.PadLeft(FIELD_SIZE_CNPJ, '0');
                }
                else
                {
                    strCNPJ_CPF = strCNPJ_CPF.PadLeft(FIELD_SIZE_CPF, '0');
                }

                for (int iIdx = 0; iIdx < FIELD_SIZE_CNPJ; iIdx++)
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

                byte[] rgbyCEI = new byte[FIELD_SIZE_CEI];

                for (int iIdx = 0; iIdx < FIELD_SIZE_CEI; iIdx++)
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

                byte[] rgbyEmployerName = new byte[FIELD_SIZE_EMPLOYER_NAME];

                for (int iIdx = 0; iIdx < FIELD_SIZE_EMPLOYER_NAME; iIdx++)
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

                byte[] rgbyEmployerAddress = new byte[FIELD_SIZE_EMPLOYER_ADDRESS];

                for (int iIdx = 0; iIdx < FIELD_SIZE_EMPLOYER_ADDRESS; iIdx++)
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

                rgbyBuffer = new byte[BUFFER_SIZE_CMD_SET_EMPLOYER];

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

                FReadEmployerData(byAddress, byProduct, ref rgbyBuffer[0]);

                return rgbyBuffer;
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita alteração na data e na hora do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetDateTime(byte byDay, byte byMonth, ushort usYear, byte byHour, byte byMinute, byte bySecond)
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_SET_DATE_TIME];

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
            /// Monta buffer com o comando que solicita o total de eventos (NSR) gravadas no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalNSR()
            {
                byte byAddress = 0;
                byte byProduct = PRODUCT_ID_REP;
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_TOTAL_USERS];

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
                byte[] rgbyBuffer = new byte[BUFFER_SIZE_CMD_REQUEST_TOTAL_USERS];

                this.FRequestTotalUsers(byAddress, byProduct, ref rgbyBuffer[0]);

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
            /// Solicita a avaliação do buffer devolvido pelo equipamento
            /// </summary>
            /// <returns>Ver tabela de retorno</returns>
            public int PacketAvail(byte[] rgbyBuffer)
            {
                return this.FPacketAvail(ref rgbyBuffer[0]);
            }

            /// <summary>
            /// Obtem os dados do usuário solicitado no comando ReadUserData
            /// </summary>
            /// <returns></returns>
            public void GetUserData(ref string strPIS, ref string strUserName, ref uint uiKeyCode, ref string strBarCode, ref byte byFacilityCode, ref ushort usProxCode, ref byte byUserType, ref string strPassword, ref ushort usSizePhoto, ref System.IO.MemoryStream msPhoto, ref ushort usSizeSample, ref byte byQuantitySamples, ref byte[] rgbyBiometric_Sample1, ref byte[] rgbyBiometric_Sample2)
            {
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];
                byte[] rgbyUsername = new byte[FIELD_SIZE_USERNAME];
                byte[] rgbyPassword = new byte[FIELD_SIZE_PASSWORD];
                byte[] rgbyBarCode = new byte[FIELD_SIZE_BARCODE];
                byte[] rgbyPhoto = new byte[1];

                byte byStatus = 0;

                this.FGetUserData(ref rgbyPIS[0], ref rgbyUsername[0], ref uiKeyCode, ref rgbyBarCode[0], ref byFacilityCode, ref usProxCode, ref byStatus, ref byUserType, ref rgbyPassword[0], ref usSizePhoto, ref rgbyPhoto[0], ref usSizeSample, ref byQuantitySamples, ref rgbyBiometric_Sample1[0], ref rgbyBiometric_Sample2[0]);

                strPIS = "";

                for (int iIdx = 0; iIdx < FIELD_SIZE_PIS; iIdx++)
                {
                    if ((char)rgbyPIS[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strPIS += (char)rgbyPIS[iIdx];
                }

                strUserName = "";

                for (int iIdx = 0; iIdx < FIELD_SIZE_USERNAME; iIdx++)
                {
                    if ((char)rgbyUsername[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strUserName += (char)rgbyUsername[iIdx];
                }

                strBarCode = "";
                for (int iIdx = 0; iIdx < FIELD_SIZE_BARCODE; iIdx++)
                {
                    strBarCode += (rgbyBarCode[iIdx] >> 4);
                    strBarCode += (rgbyBarCode[iIdx] & 0x0F);
                }

                if (strBarCode == "00000000000000000000")
                {
                    strBarCode = "";
                }

                strPassword = "";
                for (int iIdx = 0; iIdx < FIELD_SIZE_PASSWORD; iIdx++)
                {
                    if ((char)rgbyPassword[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strPassword += (char)rgbyPassword[iIdx];
                }

                if (usSizePhoto != 0)
                {
                    Array.Resize(ref rgbyPhoto, usSizePhoto);
                    msPhoto = new System.IO.MemoryStream(rgbyPhoto);
                }
            }

            /// <summary>
            /// Obtem os dados da empresa/empregador solicitado no comando ReadEmployerData
            /// </summary>
            /// <returns></returns>
            public void GetEmployerData(ref byte byIdentifyType, ref string strCNPJ_CPF, ref string strCEI, ref string strEmployerName, ref string strEmployerAddress)
            {
                byte[] rgbyCNPJ_CPF = new byte[FIELD_SIZE_CNPJ];
                byte[] rgbyCEI = new byte[FIELD_SIZE_CEI];
                byte[] rgbyEmployerName = new byte[FIELD_SIZE_EMPLOYER_NAME];
                byte[] rgbyEmployerAddress = new byte[FIELD_SIZE_EMPLOYER_ADDRESS];

                this.FGetEmployerData(ref byIdentifyType, ref rgbyCNPJ_CPF[0], ref rgbyCEI[0], ref rgbyEmployerName[0], ref rgbyEmployerAddress[0]);

                for (int iIdx = 0; iIdx < FIELD_SIZE_CNPJ; iIdx++)
                {
                    if ((char)rgbyCNPJ_CPF[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strCNPJ_CPF += (char)rgbyCNPJ_CPF[iIdx];
                }

                strCNPJ_CPF = strCNPJ_CPF.PadLeft(FIELD_SIZE_CNPJ, '0');

                for (int iIdx = 0; iIdx < FIELD_SIZE_CEI; iIdx++)
                {
                    if ((char)rgbyCEI[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strCEI += (char)rgbyCEI[iIdx];
                }

                strCEI = strCEI.PadLeft(FIELD_SIZE_CEI, '0');

                for (int iIdx = 0; iIdx < FIELD_SIZE_EMPLOYER_NAME; iIdx++)
                {
                    if ((char)rgbyEmployerName[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strEmployerName += (char)rgbyEmployerName[iIdx];
                }

                for (int iIdx = 0; iIdx < FIELD_SIZE_EMPLOYER_ADDRESS; iIdx++)
                {
                    if ((char)rgbyEmployerAddress[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strEmployerAddress += (char)rgbyEmployerAddress[iIdx];
                }
            }

            /// <summary>
            /// Obtem os dados do registro tipo 2 solicitado no comando RequestEventByNSR
            /// </summary>
            /// <returns></returns>
            public void GetLogType2(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref byte byIdentifyType, ref string strCNPJ_CPF, ref string strCEI, ref string strEmployerName, ref string strEmployerAddress)
            {
                byte[] rgbyCNPJ_CPF = new byte[FIELD_SIZE_CNPJ];
                byte[] rgbyCEI = new byte[FIELD_SIZE_CEI];
                byte[] rgbyEmployerName = new byte[FIELD_SIZE_EMPLOYER_NAME];
                byte[] rgbyEmployerAddress = new byte[FIELD_SIZE_EMPLOYER_ADDRESS];

                this.FGetLogType2(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref byIdentifyType, ref rgbyCNPJ_CPF[0], ref rgbyCEI[0], ref rgbyEmployerName[0], ref rgbyEmployerAddress[0]);

                for (int iIdx = 0; iIdx < FIELD_SIZE_CNPJ; iIdx++)
                {
                    if ((char)rgbyCNPJ_CPF[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strCNPJ_CPF += (char)rgbyCNPJ_CPF[iIdx];
                }

                strCNPJ_CPF = strCNPJ_CPF.PadLeft(FIELD_SIZE_CNPJ, '0');

                for (int iIdx = 0; iIdx < FIELD_SIZE_CEI; iIdx++)
                {
                    if ((char)rgbyCEI[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strCEI += (char)rgbyCEI[iIdx];
                }

                for (int iIdx = 0; iIdx < FIELD_SIZE_EMPLOYER_NAME; iIdx++)
                {
                    if ((char)rgbyEmployerName[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strEmployerName += (char)rgbyEmployerName[iIdx];
                }

                for (int iIdx = 0; iIdx < FIELD_SIZE_EMPLOYER_ADDRESS; iIdx++)
                {
                    if ((char)rgbyEmployerAddress[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strEmployerAddress += (char)rgbyEmployerAddress[iIdx];
                }
            }

            /// <summary>
            /// Obtem os dados do registro tipo 3 solicitado no comando RequestEventByNSR
            /// </summary>
            /// <returns></returns>
            public void GetLogType3(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref string strPIS)
            {
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];

                this.FGetLogType3(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref rgbyPIS[0]);
                
                for (int iIdx = 0; iIdx < FIELD_SIZE_PIS; iIdx++)
                {
                    if ((char)rgbyPIS[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strPIS += (char)rgbyPIS[iIdx];
                }

                strPIS = strPIS.PadLeft(FIELD_SIZE_PIS, '0');
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
                byte[] rgbyUsername = new byte[FIELD_SIZE_USERNAME];

                this.FGetLogType5(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref byOpType, ref rgbyPIS[0], ref rgbyUsername[0]);
                
                for (int iIdx = 0; iIdx < FIELD_SIZE_PIS; iIdx++)
                {
                    if ((char)rgbyPIS[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strPIS += (char)rgbyPIS[iIdx];
                }

                strPIS = strPIS.PadLeft(FIELD_SIZE_PIS, '0');

                for (int iIdx = 0; iIdx < FIELD_SIZE_USERNAME; iIdx++)
                {
                    if ((char)rgbyUsername[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strUsername += (char)rgbyUsername[iIdx];
                }
            }

            /// <summary>
            /// Obtem os dados do registro tipo 3 solicitado no comando RequestEventByNSR
            /// Este comando é utilizado com os ID REP do Chile, e neste comando o registro do tipo 3 possui o parâmetro que informa 
            /// de que tipo é o evento (entrada ou saída)
            /// </summary>
            /// <returns></returns>
            public void GetLogType6(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref string strPIS, ref char cEvent)
            {
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS];

                byte byEvent = 0;
                cEvent = ' ';

                this.FGetLogType6(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref rgbyPIS[0], ref byEvent);

                for (int iIdx = 0; iIdx < FIELD_SIZE_PIS; iIdx++)
                {
                    if ((char)rgbyPIS[iIdx] == END_STRING)
                    {
                        break;
                    }

                    strPIS += (char)rgbyPIS[iIdx];
                }

                strPIS = strPIS.PadLeft(FIELD_SIZE_PIS, '0');

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

                FGetREPCommunication(ref byCommunicationType, ref rgbyIPEquipment[0], ref rgbySubnetMask[0], ref rgbyIPGateway[0], ref usTCPPort_Comm, ref usTCPPort_Alarm, ref byBaudrate, ref bySerialAddress, ref byMulticastAddress, ref byBroadcastAddress);

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
                byte[] rgbyNFR = new byte[BUFFER_SIZE_NFR];

                this.FGetNFR(ref rgbyNFR[0]);

                for (int iIdx = 0; iIdx < BUFFER_SIZE_NFR; iIdx++)
                {
                    strNFR += (char)rgbyNFR[iIdx];
                }
            }

            /// <summary>
            /// Obtem o total de eventos gravados no equipamento solicitado no comando RequestTotalNSR
            /// </summary>
            /// <returns></returns>
            public void GetTotalNSR(ref uint uiTotalNSR)
            {
                this.FGetTotalNSR(ref uiTotalNSR);
            }

            /// <summary>
            /// Obtem o total de usuários gravados no equipamento solicitado no comando RequestTotalUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalUsers(ref uint uiTotalUsers)
            {
                this.FGetTotalUsers(ref uiTotalUsers);
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
            /// Converte o template da biometria de 400 bytes para o de 404 bytes
            /// </summary>
            /// <param name="rgbyTemplate400"></param>
            /// <param name="rgbyTemplate404"></param>
            /// <returns>Verdadeiro se a conversão foi realizada com sucesso</returns>
            public bool ConvertTemplate400ToTemplate404(byte[] rgbyTemplate400, ref byte[] rgbyTemplate404)
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
            public bool ConvertTemplate404ToTemplate400(byte[] rgbyTemplate404, ref byte[] rgbyTemplate400)
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

            public int FileClose()
            {
                return this.FFileClose();
            }

            public void EncryptBuffer(ref byte[] rgbyBuffer, uint uiSize)
            {
                this.FEncryptBuffer(ref rgbyBuffer[0], uiSize);
            }

            public void DecryptBuffer(ref byte[] rgbyBuffer, uint uiSize)
            {
                this.FDecryptBuffer(ref rgbyBuffer[0], uiSize);
            }

            public int FileOpen(byte[] rgbyPath, ref uint uiAmountUsers, ref byte[] rgbyNameCompany, ref byte[] rgbyIdentify, ref byte[] rgbyCei, ref byte[] rgbyAddress, ref byte[] rgbyNFR)
            {
                return this.FFileOpen(ref rgbyPath[0], ref uiAmountUsers, ref rgbyNameCompany[0], ref rgbyIdentify[0], ref rgbyCei[0], ref rgbyAddress[0], ref rgbyNFR[0]);
            }

            public int CreateFile(string strPath, uint uiAmountUsers, string strEmployerName, string strCNPJ_CPF, string strCEI, string strEmployerAddress, string strNFR)
            {
                byte[] rgbyPath = new byte[strPath.Length];
                byte[] rgbyEmployerName = new byte[UBS_FILE_SIZE_EMPLOYER_NAME];
                byte[] rgbyCNPJ_CPF = new byte[UBS_FILE_SIZE_CNPJ_CPF];
                byte[] rgbyCEI = new byte[UBS_FILE_SIZE_CEI];
                byte[] rgbyEmployerAddress = new byte[UBS_FILE_SIZE_EMPLOYER_ADDRESS];
                byte[] rgbyNFR = new byte[UBS_FILE_SIZE_NFR];

                for (int iIdx = 0; iIdx < strPath.Length; iIdx++)
                {
                    rgbyPath[iIdx] = (byte)strPath[iIdx];
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

                strNFR = strNFR.PadLeft(rgbyNFR.Length, '0');

                for (int iIdx = 0; iIdx < rgbyNFR.Length; iIdx++)
                {
                    rgbyNFR[iIdx] = (byte)strNFR[iIdx];
                }

                return this.FCreateFile(ref rgbyPath[0], uiAmountUsers, ref rgbyEmployerName[0], ref rgbyCNPJ_CPF[0], ref rgbyCEI[0], ref rgbyEmployerAddress[0], ref rgbyNFR[0]);
            }

            public void ExportUbsUser(ulong ulPIS, string strUserName, uint uiKeyCode, string strBarCode, uint uiProxCode, byte byUserType, string strPassword, byte[] rgbyBiometrics, uint uiSizeSample)
            {
                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS_EXPORT_FILE];
                byte[] rgbyUsername = new byte[FIELD_SIZE_USERNAME];
                byte[] rgbyBarCode = new byte[FIELD_SIZE_BARCODE];
                byte[] rgbyKeyCode = new byte[FIELD_SIZE_KEYCODE_EXPORT_FILE];
                byte[] rgbyPassword = new byte[FIELD_SIZE_PASSWORD_EXPORT_FILE];
                string strKeyCode = string.Empty;

                for (int iIdx = 0; iIdx < FIELD_SIZE_PIS_EXPORT_FILE; iIdx++)
                {
                    if (iIdx < ulPIS.ToString().Length)
                    {
                        rgbyPIS[iIdx] = (byte)ulPIS.ToString()[iIdx];
                    }
                    else
                    {
                        rgbyPIS[iIdx] = (byte)END_STRING;
                    }

                }

                for (int iIdx = 0; iIdx < FIELD_SIZE_USERNAME; iIdx++)
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
                    rgbyBarCode = ConvertStringToArray(strBarCode, (FIELD_SIZE_BARCODE * 2), FIELD_SIZE_BARCODE);
                }
                else
                {
                    for (int i = 0; i < FIELD_SIZE_BARCODE; i++)
                    {
                        rgbyBarCode[i] = (byte)END_STRING;
                    }
                }

                if (uiKeyCode != 0)
                {
                    strKeyCode = uiKeyCode.ToString().PadLeft(6, '0');

                    for (int iIdx = 0; iIdx < FIELD_SIZE_KEYCODE_EXPORT_FILE; iIdx++)
                    {
                        rgbyKeyCode[iIdx] = (byte)strKeyCode[iIdx];
                    }

                }
                else
                {
                    for (int iIdx = 0; iIdx < FIELD_SIZE_KEYCODE_EXPORT_FILE; iIdx++)
                    {
                        rgbyKeyCode[iIdx] = 30;
                    }
                }

                if (strPassword == "0" || strPassword == " ")
                {
                    for (int iIdx = 0; iIdx < 8; iIdx++)
                    {
                        rgbyPassword[iIdx] = 30;
                    }
                }
                else
                {
                    for (int iIdx = 0; iIdx < 8; iIdx++)
                    {
                        if (iIdx >= strPassword.Length)
                        {
                            rgbyPassword[iIdx] = (byte)END_STRING;
                        }
                        else
                        {
                            rgbyPassword[iIdx] = (byte)strPassword[iIdx];
                        }
                    }
                }

                this.FExportUbsUser(ref rgbyPIS[0], ref rgbyUsername[0], ref rgbyKeyCode[0], ref rgbyBarCode[0], uiProxCode, byUserType, ref rgbyPassword[0], ref rgbyBiometrics[0], uiSizeSample);
            }

            public int ImportUbsUser(ref ulong ulPIS, ref string strUserName, ref uint uiKeyCode, ref string strBarCode, ref uint uiProxCode, ref byte byUserType, ref string strPassword, ref byte[] rgbyBiometrics, ref uint uiSizeSample)
            {
                int iRet = 0;

                byte[] rgbyPIS = new byte[FIELD_SIZE_PIS_EXPORT_FILE];
                byte[] rgbyUsername = new byte[FIELD_SIZE_USERNAME];
                byte[] rgbyBarCode = new byte[FIELD_SIZE_BARCODE];
                byte[] rgbyKeyCode = new byte[FIELD_SIZE_KEYCODE_EXPORT_FILE];
                byte[] rgbyPassword = new byte[FIELD_SIZE_PASSWORD_EXPORT_FILE];

                iRet = this.FImportUbsUser(ref rgbyPIS[0], ref rgbyUsername[0], ref rgbyKeyCode[0], ref rgbyBarCode[0], ref uiProxCode, ref byUserType, ref rgbyPassword[0], ref rgbyBiometrics[0], ref uiSizeSample);

                string strAssistent = string.Empty;

                for (int i = 0; i < rgbyPIS.Length; i++)
                {
                    strAssistent += (char)rgbyPIS[i];
                }
            
                if (!ulong.TryParse(strAssistent, out ulPIS))
                {
                    ulPIS = 0;
                }

                for (int i = 0; i < rgbyUsername.Length; i++)
                {
                    strUserName += (char)rgbyUsername[i];
                }

                if (strUserName.IndexOf("\0") != -1)
                {
                    strUserName = strUserName.Substring(0, strUserName.IndexOf("\0"));
                }

                strAssistent = string.Empty;

                for (int i = 0; i < rgbyKeyCode.Length; i++)
                {
                    strAssistent += (char)rgbyKeyCode[i];
                }

                uiKeyCode = 0;

                uint.TryParse(strAssistent, out uiKeyCode);

                strBarCode = ConvertArrayToString(rgbyBarCode, 20, rgbyBarCode.Length);


                strAssistent = string.Empty;

                for (int i = 0; i < rgbyPassword.Length; i++)
                {
                    strAssistent += (char)rgbyPassword[i];
                }

                strPassword = strAssistent;

                return iRet;

            }
        
            public int FileOpenAFD(string strPath)
            {
                byte[] rgbyPath = new byte[strPath.Length];

                for (int iIdx = 0; iIdx < strPath.Length; iIdx++)
                {
                    rgbyPath[iIdx] = (byte)strPath[iIdx];
                }

                return this.FFileOpenAFD(ref rgbyPath[0]);
            }

            public int ImportAFD(ref byte[] rgbyBuffer)
            {
                return this.FImportAFD(ref rgbyBuffer[0]);
            }

        #endregion
    }
}

