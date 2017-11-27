using System;
using zkemkeeper;

namespace REP100
{
    class Rep100Class
    {
        public Rep100Class() { }

        IZKEM bioRep = new CZKEMClass();

        public int AccGroup { get; set; }
        public int BASE64 { get; set; }
        public int CommPort { get; set; }
        public int ConvertBIG5 { get; set; }
        public int MachineNumber { get; set; }
        public uint PIN2 { get; set; }
        public int PINWidth { get; set; }
        public int PullMode { get; set; }
        public bool ReadMark { get; set; }
        public int SSRPin { get; set; }

        public bool ACUnlock(int dwMachineNumber, int Delay) { return bioRep.ACUnlock(dwMachineNumber, Delay); }
        public bool BackupData(string DataFile) { return bioRep.BackupData(DataFile); }
        public bool BatchUpdate(int dwMachineNumber) { return bioRep.BatchUpdate(dwMachineNumber); }
        public bool Beep(int DelayMS) { return bioRep.Beep(DelayMS); }
        public bool BeginBatchUpdate(int dwMachineNumber, int UpdateFlag) { return bioRep.BeginBatchUpdate(dwMachineNumber, UpdateFlag); }
        public bool CancelBatchUpdate(int dwMachineNumber) { return bioRep.CancelBatchUpdate(dwMachineNumber); }
        public bool CancelOperation() { return bioRep.CancelOperation(); }
        public bool CaptureImage(bool FullImage, ref int Width, ref int Height, ref byte Image, string ImageFile) { return bioRep.CaptureImage(FullImage, ref Width, ref Height, ref Image, ImageFile); }
        public bool ClearAdministrators(int dwMachineNumber) { return bioRep.ClearAdministrators(dwMachineNumber); }
        public bool ClearData(int dwMachineNumber, int DataFlag) { return bioRep.ClearData(dwMachineNumber, DataFlag); }
        public bool ClearGLog(int dwMachineNumber) { return bioRep.ClearGLog(dwMachineNumber); }
        public bool ClearKeeperData(int dwMachineNumber) { return bioRep.ClearKeeperData(dwMachineNumber); }
        public bool ClearLCD() { return bioRep.ClearLCD(); }
        public bool ClearSLog(int dwMachineNumber) { return bioRep.ClearSLog(dwMachineNumber); }
        public bool ClearSMS(int dwMachineNumber) { return bioRep.ClearSMS(dwMachineNumber); }
        public bool ClearUserSMS(int dwMachineNumber) { return bioRep.ClearUserSMS(dwMachineNumber); }
        public bool ClearWorkCode() { return bioRep.ClearWorkCode(); }
        public bool Connect_Com(int ComPort, int MachineNumber, int BaudRate) { return bioRep.Connect_Com(CommPort, MachineNumber, BaudRate); }
        public bool Connect_Modem(int ComPort, int MachineNumber, int BaudRate, string Telephone) { return bioRep.Connect_Modem(CommPort, MachineNumber, BaudRate, Telephone); }
        public bool Connect_Net(string IPAdd, int Port)
        {
            return bioRep.Connect_Net(IPAdd, Port);
        }
        public bool Connect_USB(int MachineNumber) { return bioRep.Connect_USB(MachineNumber); }
        public void ConvertPassword(int dwSrcPSW, ref int dwDestPSW, int dwLength) { bioRep.ConvertPassword(dwSrcPSW, dwDestPSW, dwLength); }
        public bool DelCustomizeAttState(int dwMachineNumber, int StateID) { return bioRep.DelCustomizeAttState(dwMachineNumber, StateID); }
        public bool DelCustomizeVoice(int dwMachineNumber, int VoiceID) { return bioRep.DelCustomizeVoice(dwMachineNumber, VoiceID); }
        public bool DeleteEnrollData(int dwMachineNumber, int dwEnrollNumber, int dwEMachineNumber, int dwBackupNumber) { return bioRep.DeleteEnrollData(dwMachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber); }
        public bool DeleteSMS(int dwMachineNumber, int ID) { return bioRep.DeleteSMS(dwMachineNumber, ID); }
        public bool DeleteUserInfoEx(int dwMachineNumber, int dwEnrollNumber) { return bioRep.DeleteUserInfoEx(dwMachineNumber, dwEnrollNumber); }
        public bool DeleteUserSMS(int dwMachineNumber, int dwEnrollNumber, int SMSID) { return bioRep.DeleteUserSMS(dwMachineNumber, dwEnrollNumber, SMSID); }
        public bool DeleteWorkCode(int WorkCodeID) { return bioRep.DeleteWorkCode(WorkCodeID); }
        public bool DelUserFace(int dwMachineNumber, string dwEnrollNumber, int dwFaceIndex) { return bioRep.DelUserFace(dwMachineNumber, dwEnrollNumber, dwFaceIndex); }
        public bool DelUserTmp(int dwMachineNumber, int dwEnrollNumber, int dwFingerIndex) { return bioRep.DelUserTmp(dwMachineNumber, dwEnrollNumber, dwFingerIndex); }
        public bool DisableDeviceWithTimeOut(int dwMachineNumber, int TimeOutSec) { return bioRep.DisableDeviceWithTimeOut(dwMachineNumber, TimeOutSec); }
        public void Disconnect()
        {
            bioRep.Disconnect();
        }
        public bool EmptyCard(int dwMachineNumber) { return bioRep.EmptyCard(dwMachineNumber); }
        public bool EnableClock(int Enabled) { return bioRep.EnableClock(Enabled); }
        public bool EnableCustomizeAttState(int dwMachineNumber, int StateID, int Enable) { return bioRep.EnableCustomizeAttState(dwMachineNumber, StateID, Enable); }
        public bool EnableCustomizeVoice(int dwMachineNumber, int VoiceID, int Enable) { return bioRep.EnableCustomizeVoice(dwMachineNumber, VoiceID, Enable); }
        public bool EnableDevice(int dwMachineNumber, bool bFlag) { return bioRep.EnableDevice(dwMachineNumber, bFlag); }
        public bool EnableUser(int dwMachineNumber, int dwEnrollNumber, int dwEMachineNumber, int dwBackupNumber, bool bFlag) { return bioRep.EnableUser(dwMachineNumber, dwEnrollNumber, dwEMachineNumber, dwBackupNumber, bFlag); }
        public bool FPTempConvert(ref byte TmpData1, ref byte TmpData2, ref int Size) { return bioRep.FPTempConvert(ref TmpData1, ref TmpData2, ref Size); }
        public bool FPTempConvertNew(ref byte TmpData1, ref byte TmpData2, ref int Size) { return bioRep.FPTempConvertNew(ref TmpData1, ref TmpData2, ref Size); }
        public bool FPTempConvertNewStr(string TmpData1, ref string TmpData2, ref int Size) { return bioRep.FPTempConvertNewStr(TmpData1, ref TmpData2, ref Size); }
        public bool FPTempConvertStr(string TmpData1, ref string TmpData2, ref int Size) { return bioRep.FPTempConvertStr(TmpData1, ref TmpData2, ref Size); }
        public int get_AccTimeZones(int Index) { return bioRep.get_AccTimeZones(Index); }
        public int get_CardNumber(int Index) { return bioRep.get_CardNumber(Index); }
        public string get_STR_CardNumber(int Index) { return bioRep.get_STR_CardNumber(Index); }
        public bool GetACFun(ref int ACFun) { return bioRep.GetACFun(ref ACFun); }
        public bool GetAllGLogData(int dwMachineNumber, ref int dwTMachineNumber, ref int dwEnrollNumber, ref int dwEMachineNumber, ref int dwVerifyMode, ref int dwInOutMode, ref int dwYear, ref int dwMonth, ref int dwDay, ref int dwHour, ref int dwMinute)
        {
            return bioRep.GetAllGLogData(dwEMachineNumber, ref dwTMachineNumber, ref dwEnrollNumber, ref dwEMachineNumber, ref dwVerifyMode, ref dwInOutMode, ref dwYear, dwMonth, ref dwDay, ref dwHour, ref dwMinute);
        }
        public bool GetAllSLogData(int dwMachineNumber, ref int dwTMachineNumber, ref int dwSEnrollNumber, ref int Params4, ref int Params1, ref int Params2, ref int dwManipulation, ref int Params3, ref int dwYear, ref int dwMonth, ref int dwDay, ref int dwHour, ref int dwMinute)
        {
            return bioRep.GetAllSLogData(dwMachineNumber, ref dwTMachineNumber, ref dwSEnrollNumber, ref Params4, ref Params1, ref Params2, ref dwManipulation, Params3, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute);
        }
        public bool GetAllUserID(int dwMachineNumber, ref int dwEnrollNumber, ref int dwEMachineNumber, ref int dwBackupNumber, ref int dwMachinePrivilege, ref int dwEnable)
        {
            return bioRep.GetAllUserID(dwMachineNumber, ref dwEnrollNumber, ref dwEMachineNumber, ref dwBackupNumber, ref dwMachinePrivilege, ref dwEnable);
        }
        public bool GetAllUserInfo(int dwMachineNumber, ref int dwEnrollNumber, ref string Name, ref string Password, ref int Privilege, ref bool Enabled)
        {
            return bioRep.GetAllUserInfo(dwMachineNumber, ref dwEnrollNumber, ref Name, ref Password, ref Privilege, ref Enabled);
        }
        public bool GetAttLogs(int dwMachineNumber, out string nsr, out string pis, out int dwYear, out int dwMonth, out int dwDay, out int dwHour, out int dwMinute, out int dwSecond)
        {
            return bioRep.GetAttLogs(dwMachineNumber, out nsr, out pis, out dwYear, out dwMonth, out dwDay, out dwHour, out dwMinute, out dwSecond);
        }
        public int GetBackupNumber(int dwMachineNumber) { return bioRep.GetBackupNumber(dwMachineNumber); }
        public bool GetBZ400FirmwareVersion(int dwMachineNumber, ref string strVersion) { return bioRep.GetBZ400FirmwareVersion(dwMachineNumber, ref strVersion); }
        public bool GetCardFun(int dwMachineNumber, ref int CardFun) { return bioRep.GetCardFun(dwMachineNumber, ref CardFun); }
        public bool GetDataFile(int dwMachineNumber, int DataFlag, string FileName) { return bioRep.GetDataFile(dwMachineNumber, DataFlag, FileName); }
        public bool GetDatetimeOpLog(int dwMachineNumber, out string nsr, out string OldDatetime, out string NewDatetime) { return bioRep.GetDatetimeOpLog(dwMachineNumber, out nsr, out OldDatetime, out NewDatetime); }
        public bool GetDaylight(int dwMachineNumber, ref int Support, ref string BeginTime, ref string EndTime) { return bioRep.GetDaylight(dwMachineNumber, ref Support, ref BeginTime, ref EndTime); }
        public bool GetDeviceInfo(int dwMachineNumber, int dwInfo, ref int dwValue) { return bioRep.GetDeviceInfo(dwMachineNumber, dwInfo, dwValue); }
        public bool GetDeviceIP(int dwMachineNumber, ref string IPAddr) { return bioRep.GetDeviceIP(dwMachineNumber, ref IPAddr); }
        public bool GetDeviceMAC(int dwMachineNumber, ref string sMAC) { return bioRep.GetDeviceMAC(dwMachineNumber, sMAC); }
        public bool GetDeviceStatus(int dwMachineNumber, int dwStatus, ref int dwValue) { return bioRep.GetDeviceStatus(dwMachineNumber, dwStatus, ref dwValue); }
        public bool GetDeviceStrInfo(int dwMachineNumber, int dwInfo, out string Value) { return bioRep.GetDeviceStrInfo(dwMachineNumber, dwInfo, out Value); }
        public bool GetDeviceTime(int dwMachineNumber, ref int dwYear, ref int dwMonth, ref int dwDay, ref int dwHour, ref int dwMinute, ref int dwSecond) { return false; }
        public bool GetDoorState(int MachineNumber, ref int State) { return false; }
        public bool GetEmployeeOpLog(int dwMachineNumber, out string nsr, out int Year, out int Month, out int Day, out int Hour, out int min, out int sec, out string OpType, out string pis, out string Name)
        {
            return bioRep.GetEmployeeOpLog(dwMachineNumber, out nsr, out Year, out Month, out Day, out Hour, out min, out sec, out OpType, out pis, out Name);
        }
        public bool GetEmployer(int dwMachineNumber, out string Name, out string CertType, out string Tax, out string cei, out string Addr)
        {
            return bioRep.GetEmployer(dwMachineNumber, out Name, out CertType, out Tax, out cei, out Addr);
        }
        public bool GetEmployerOpLog(int dwMachineNumber, out string nsr, out int Year, out int Month, out int Day, out int Hour, out int min, out int sec, out string cnpj_cpf, out int identtype, out string cei, out string Name, out string address, out string OpType)
        {
            return bioRep.GetEmployerOpLog(dwMachineNumber, out nsr, out Year, out Month, out Day, out Hour, out min, out sec, out cnpj_cpf, out identtype, out cei, out Name, out address, out OpType);
        }
        public bool GetEnrollData(int dwMachineNumber, int dwEnrollNumber, int dwEMachineNumber, int dwBackupNumber, ref int dwMachinePrivilege, ref int dwEnrollData, ref int dwPassWord) { return false; }
        public bool GetEnrollDataStr(int dwMachineNumber, int dwEnrollNumber, int dwEMachineNumber, int dwBackupNumber, ref int dwMachinePrivilege, ref string dwEnrollData, ref int dwPassWord) { return false; }
        public bool GetFirmwareVersion(int dwMachineNumber, ref string strVersion) { return false; }
        public int GetFPTempLength(ref byte dwEnrollData) { return bioRep.GetFPTempLength(ref dwEnrollData); }
        public int GetFPTempLengthStr(string dwEnrollData) { return bioRep.GetFPTempLengthStr(dwEnrollData); }
        public bool GetGeneralExtLogData(int dwMachineNumber, ref int dwEnrollNumber, ref int dwVerifyMode, ref int dwInOutMode, ref int dwYear, ref int dwMonth, ref int dwDay, ref int dwHour, ref int dwMinute, ref int dwSecond, ref int dwWorkCode, ref int dwReserved) { return false; }
        public bool GetGeneralLogData(int dwMachineNumber, ref int dwTMachineNumber, ref int dwEnrollNumber, ref int dwEMachineNumber, ref int dwVerifyMode, ref int dwInOutMode, ref int dwYear, ref int dwMonth, ref int dwDay, ref int dwHour, ref int dwMinute) { return false; }
        public bool GetGeneralLogDataStr(int dwMachineNumber, ref int dwEnrollNumber, ref int dwVerifyMode, ref int dwInOutMode, ref string TimeStr) { return false; }
        public bool GetGroupTZs(int dwMachineNumber, int GroupIndex, ref int TZs) { return false; }
        public bool GetGroupTZStr(int dwMachineNumber, int GroupIndex, ref string TZs) { return false; }
        public bool GetHIDEventCardNumAsStr(out string strHIDEventCardNum) { return bioRep.GetHIDEventCardNumAsStr(out strHIDEventCardNum); }
        public bool GetHoliday(int dwMachineNumber, ref string Holiday) { return false; }
        public void GetLastError(ref int dwErrorCode)
        {
            bioRep.GetLastError(ref dwErrorCode);
        }
        public bool GetMRPTotal(int dwMachineNumber, int MRPType, ref int Value) { return false; }
        public bool GetPIN2(int UserID, ref int PIN2) { return false; }
        public bool GetPlatform(int dwMachineNumber, ref string Platform) { return false; }
        public bool GetProductCode(int dwMachineNumber, out string lpszProductCode) { return bioRep.GetProductCode(dwMachineNumber, out lpszProductCode); }
        public bool GetRealFirmwareVersion(int dwMachineNumber, ref string strVersion) { return false; }
        public bool GetRTLog(int dwMachineNumber) { return false; }
        public bool GetSDKVersion(ref string strVersion) { return false; }
        public bool GetSensorSN(int dwMachineNumber, ref string SensorSN) { return false; }
        public bool GetSerialNumber(int dwMachineNumber, out string dwSerialNumber) { return bioRep.GetSerialNumber(dwMachineNumber, out dwSerialNumber); }
        public bool GetSMS(int dwMachineNumber, int ID, ref int Tag, ref int ValidMinutes, ref string StartTime, ref string Content) { return false; }
        public bool GetStrCardNumber(out string ACardNumber) { return bioRep.GetStrCardNumber(out ACardNumber); }
        public bool GetSuperLogData(int dwMachineNumber, ref int dwTMachineNumber, ref int dwSEnrollNumber, ref int Params4, ref int Params1, ref int Params2, ref int dwManipulation, ref int Params3, ref int dwYear, ref int dwMonth, ref int dwDay, ref int dwHour, ref int dwMinute) { return false; }
        public bool GetSuperLogData2(int dwMachineNumber, ref int dwTMachineNumber, ref int dwSEnrollNumber, ref int Params4, ref int Params1, ref int Params2, ref int dwManipulation, ref int Params3, ref int dwYear, ref int dwMonth, ref int dwDay, ref int dwHour, ref int dwMinute, ref int dwSecs) { return false; }
        public bool GetSuperLogDataEx(int dwMachineNumber, ref string EnrollNumber, ref int Params4, ref int Params1, ref int Params2, ref int dwManipulation, ref int Params3, ref int dwYear, ref int dwMonth, ref int dwDay, ref int dwHour, ref int dwMinute, ref int dwSecond) { return false; }
        public bool GetSysOption(int dwMachineNumber, string Option, out string Value) { return bioRep.GetSysOption(dwMachineNumber, Option, out Value); }
        public bool GetTZInfo(int dwMachineNumber, int TZIndex, ref string TZ) { return false; }
        public bool GetUnlockGroups(int dwMachineNumber, ref string Grps) { return false; }
        public bool GetUnlockPwd(string LockSerialNumber, ref string UnlockPwd) { return false; }
        public bool GetUserFace(int dwMachineNumber, string dwEnrollNumber, int dwFaceIndex, ref byte TmpData, ref int TmpLength) { return false; }
        public bool GetUserFaceStr(int dwMachineNumber, string dwEnrollNumber, int dwFaceIndex, ref string TmpData, ref int TmpLength) { return false; }
        public bool GetUserGroup(int dwMachineNumber, int dwEnrollNumber, ref int UserGrp) { return false; }
        public bool GetUserInfo(int dwMachineNumber, int dwEnrollNumber, ref string Name, ref string Password, ref int Privilege, ref bool Enabled) { return false; }
        public bool GetUserInfoByCard(int dwMachineNumber, ref string Name, ref string Password, ref int Privilege, ref bool Enabled) { return false; }
        public bool GetUserInfoByPIN2(int dwMachineNumber, ref string Name, ref string Password, ref int Privilege, ref bool Enabled) { return false; }
        public bool GetUserInfoEx(int dwMachineNumber, int dwEnrollNumber, out int VerifyStyle, out byte Reserved) { return bioRep.GetUserInfoEx(dwMachineNumber, dwEnrollNumber, out VerifyStyle, out Reserved); }
        public bool GetUserTmp(int dwMachineNumber, int dwEnrollNumber, int dwFingerIndex, ref byte TmpData, ref int TmpLength) { return false; }
        public bool GetUserTmpEx(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex, out int Flag, out byte TmpData, out int TmpLength) { return bioRep.GetUserTmpEx(dwMachineNumber, dwEnrollNumber, dwFingerIndex, out Flag, out TmpData, out TmpLength); }
        public bool GetUserTmpEx_BZ400(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex, out int Flag, out byte TmpData, out int TmpLength) { return bioRep.GetUserTmpEx_BZ400(dwMachineNumber, dwEnrollNumber, dwFingerIndex, out Flag, out TmpData, out TmpLength); }
        public bool GetUserTmpExStr(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex, out int Flag, out string TmpData, out int TmpLength) { return bioRep.GetUserTmpExStr(dwMachineNumber, dwEnrollNumber, dwFingerIndex, out Flag, out TmpData, out TmpLength); }
        public bool GetUserTmpExStr_BZ400(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex, out int Flag, out string TmpData, out int TmpLength) { return bioRep.GetUserTmpExStr_BZ400(dwMachineNumber, dwEnrollNumber, dwFingerIndex, out Flag, out TmpData, out TmpLength); }
        public bool GetUserTmpStr(int dwMachineNumber, int dwEnrollNumber, int dwFingerIndex, ref string TmpData, ref int TmpLength) { return false; }
        public bool GetUserTZs(int dwMachineNumber, int dwEnrollNumber, ref int TZs) { return false; }
        public bool GetUserTZStr(int dwMachineNumber, int dwEnrollNumber, ref string TZs) { return false; }
        public bool GetVendor(ref string strVendor) { return false; }
        public bool GetWiegandFmt(int dwMachineNumber, ref string sWiegandFmt) { return false; }
        public bool GetWorkCode(int WorkCodeID, out int AWorkCode) { return bioRep.GetWorkCode(WorkCodeID, out AWorkCode); }
        public bool IsTFTMachine(int dwMachineNumber) { return false; }
        public bool MergeTemplate(IntPtr Templates, int FingerCount, ref byte TemplateDest, ref int FingerSize) { return false; }
        public bool ModifyPrivilege(int dwMachineNumber, int dwEnrollNumber, int dwEMachineNumber, int dwBackupNumber, int dwMachinePrivilege) { return false; }
        public bool PlayVoice(int Position, int Length) { return false; }
        public bool PlayVoiceByIndex(int Index) { return false; }
        public bool PowerOffDevice(int dwMachineNumber) { return false; }
        public void PowerOnAllDevice() { bioRep.PowerOnAllDevice(); }
        public bool QueryState(ref int State) { return false; }
        public bool ReadAllGLogData(int dwMachineNumber) { return false; }
        public bool ReadAllSLogData(int dwMachineNumber) { return false; }
        public bool ReadAllTemplate(int dwMachineNumber) { return false; }
        public bool ReadAllUserID(int dwMachineNumber) { return false; }
        public bool ReadAOptions(string AOption, out string AValue) { return bioRep.ReadAOptions(AOption, out AValue); }
        public bool ReadAttRule(int dwMachineNumber) { return false; }
        public bool ReadCustData(int dwMachineNumber, ref string CustData) { return false; }
        public bool ReadDPTInfo(int dwMachineNumber) { return false; }
        public bool ReadFile(int dwMachineNumber, string FileName, string FilePath) { return false; }
        public bool ReadGeneralLogData(int dwMachineNumber) { return false; }
        public bool ReadLastestLogData(int dwMachineNumber, int NewLog, int dwYear, int dwMonth, int dwDay, int dwHour, int dwMinute, int dwSecond) { return false; }
        public bool ReadRTLog(int dwMachineNumber) { return false; }
        public bool ReadSuperLogData(int dwMachineNumber) { return false; }
        public bool ReadSuperLogDataEx(int dwMachineNumber, int dwYear_S, int dwMonth_S, int dwDay_S, int dwHour_S, int dwMinute_S, int dwSecond_S, int dwYear_E, int dwMonth_E, int dwDay_E, int dwHour_E, int dwMinute_E, int dwSecond_E, int dwLogIndex) { return false; }
        public bool ReadTurnInfo(int dwMachineNumber) { return false; }
        public bool ReadUserAllTemplate(int dwMachineNumber, string dwEnrollNumber) { return false; }
        public bool RefreshData(int dwMachineNumber) { return false; }
        public bool RefreshOptions(int dwMachineNumber) { return false; }
        public bool RegEvent(int dwMachineNumber, int EventMask) { return false; }
        public bool RestartDevice(int dwMachineNumber) { return false; }
        public bool RestoreData(string DataFile) { return false; }
        public bool SaveTheDataToFile(int dwMachineNumber, string TheFilePath, int FileFlag) { return false; }
        public bool SendCMDMsg(int dwMachineNumber, int Param1, int Param2) { return false; }
        public bool SendFile(int dwMachineNumber, string FileName) { return false; }
        public void set_AccTimeZones(int Index, int pVal) { bioRep.set_AccTimeZones(Index, pVal); }
        public void set_CardNumber(int Index, int pVal) { bioRep.set_CardNumber(Index, pVal); }
        public void set_STR_CardNumber(int Index, string pVal) { bioRep.set_STR_CardNumber(Index, pVal); }
        public bool SetCommPassword(int CommKey) { return false; }
        public bool SetCustomizeAttState(int dwMachineNumber, int StateID, int NewState) { return false; }
        public bool SetCustomizeVoice(int dwMachineNumber, int VoiceID, string FileName) { return false; }
        public bool SetDaylight(int dwMachineNumber, int Support, string BeginTime, string EndTime) { return false; }
        public bool SetDeviceCommPwd(int dwMachineNumber, int CommKey) { return false; }
        public bool SetDeviceInfo(int dwMachineNumber, int dwInfo, int dwValue) { return false; }
        public bool SetDeviceIP(int dwMachineNumber, string IPAddr) { return false; }
        public bool SetDeviceMAC(int dwMachineNumber, string sMAC) { return false; }
        public bool SetDeviceTime(int dwMachineNumber) { return false; }
        public bool SetDeviceTime2(int dwMachineNumber, int dwYear, int dwMonth, int dwDay, int dwHour, int dwMinute, int dwSecond)
        {
            return bioRep.SetDeviceTime2(dwMachineNumber, dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond);
        }
        public bool SetEmployer(int dwMachineNumber, string Name, int CertType, string Tax, string cei, string Addr)
        {
            return bioRep.SetEmployer(dwMachineNumber, Name, CertType, Tax, cei, Addr);
        }
        public bool SetEnrollData(int dwMachineNumber, int dwEnrollNumber, int dwEMachineNumber, int dwBackupNumber, int dwMachinePrivilege, ref int dwEnrollData, int dwPassWord) { return false; }
        public bool SetEnrollDataStr(int dwMachineNumber, int dwEnrollNumber, int dwEMachineNumber, int dwBackupNumber, int dwMachinePrivilege, string dwEnrollData, int dwPassWord) { return false; }
        public bool SetGroupTZs(int dwMachineNumber, int GroupIndex, ref int TZs) { return false; }
        public bool SetGroupTZStr(int dwMachineNumber, int GroupIndex, string TZs) { return false; }
        public bool SetHoliday(int dwMachineNumber, string Holiday) { return false; }
        public bool SetLanguageByID(int dwMachineNumber, int LanguageID, string Language) { return false; }
        public bool SetLastCount(int count) { return false; }
        public bool SetOptionCommPwd(int dwMachineNumber, string CommKey) { return false; }
        public bool SetSeekPosition(int dwMachineNumber, int Position) { return false; }
        public bool SetSMS(int dwMachineNumber, int ID, int Tag, int ValidMinutes, string StartTime, string Content) { return false; }
        public bool SetStrCardNumber(string ACardNumber) { return false; }
        public bool SetSysOption(int dwMachineNumber, string Option, string Value) { return false; }
        public bool SetTZInfo(int dwMachineNumber, int TZIndex, string TZ) { return false; }
        public bool SetUnlockGroups(int dwMachineNumber, string Grps) { return false; }
        public bool SetUserFace(int dwMachineNumber, string dwEnrollNumber, int dwFaceIndex, ref byte TmpData, int TmpLength) { return false; }
        public bool SetUserFaceStr(int dwMachineNumber, string dwEnrollNumber, int dwFaceIndex, string TmpData, int TmpLength) { return false; }
        public bool SetUserGroup(int dwMachineNumber, int dwEnrollNumber, int UserGrp) { return false; }
        public bool SetUserInfo(int dwMachineNumber, int dwEnrollNumber, string Name, string Password, int Privilege, bool Enabled) { return false; }
        public bool SetUserInfoEx(int dwMachineNumber, int dwEnrollNumber, int VerifyStyle, ref byte Reserved) { return false; }
        public bool SetUserSMS(int dwMachineNumber, int dwEnrollNumber, int SMSID) { return false; }
        public bool SetUserTmp(int dwMachineNumber, int dwEnrollNumber, int dwFingerIndex, ref byte TmpData) { return false; }
        public bool SetUserTmpEx(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex, int Flag, ref byte TmpData) { return false; }
        public bool SetUserTmpEx_BZ400(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex, int Flag, ref byte TmpData) { return false; }
        public bool SetUserTmpExStr(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex, int Flag, string TmpData) { return false; }
        public bool SetUserTmpExStr_BZ400(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex, int Flag, string TmpData) { return false; }
        public bool SetUserTmpStr(int dwMachineNumber, int dwEnrollNumber, int dwFingerIndex, string TmpData) { return false; }
        public bool SetUserTZs(int dwMachineNumber, int dwEnrollNumber, ref int TZs) { return false; }
        public bool SetUserTZStr(int dwMachineNumber, int dwEnrollNumber, string TZs) { return false; }
        public bool SetWiegandFmt(int dwMachineNumber, string sWiegandFmt) { return false; }
        public bool SetWorkCode(int WorkCodeID, int AWorkCode) { return false; }
        public bool SplitTemplate(ref byte Template, IntPtr Templates, ref int FingerCount, ref int FingerSize) { return false; }
        public bool SSR_ClearWorkCode() { return false; }
        public bool SSR_DeleteEnrollData(int dwMachineNumber, string dwEnrollNumber, int dwBackupNumber) { return false; }
        public bool SSR_DeleteEnrollDataExt(int dwMachineNumber, string dwEnrollNumber, int dwBackupNumber) { return false; }
        public bool SSR_DeleteEnrollDataExt_BZ400(int dwMachineNumber, string dwEnrollNumber, int dwBackupNumber) { return false; }
        public bool SSR_DeleteUserSMS(int dwMachineNumber, string dwEnrollNumber, int SMSID) { return false; }
        public bool SSR_DeleteWorkCode(int PIN) { return false; }
        public bool SSR_DelUserTmp(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex) { return false; }
        public bool SSR_DelUserTmpExt(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex) { return false; }
        public bool SSR_EnableUser(int dwMachineNumber, string dwEnrollNumber, bool bFlag) { return false; }
        public bool SSR_GetAllUserInfo(int dwMachineNumber, out string dwEnrollNumber, out string Name, out string Password, out int Privilege, out bool Enabled)
        {
            return bioRep.SSR_GetAllUserInfo(dwMachineNumber, out dwEnrollNumber, out Name, out Password, out Privilege, out Enabled);
        }
        public bool SSR_GetAllUserInfoEx(int dwMachineNumber, out string dwEnrollNumber, out string Name, out string Password, out int Privilege, out bool Enabled, out string pis, out string CPF)
        {
            return bioRep.SSR_GetAllUserInfoEx(dwMachineNumber, out dwEnrollNumber, out Name, out Password, out Privilege, out Enabled, out pis, out CPF);
        }
        public bool SSR_GetGeneralLogData(int dwMachineNumber, out string dwEnrollNumber, out int dwVerifyMode, out int dwInOutMode, out int dwYear, out int dwMonth, out int dwDay, out int dwHour, out int dwMinute, out int dwSecond, ref int dwWorkCode)
        {
            return bioRep.SSR_GetGeneralLogData(dwMachineNumber, out dwEnrollNumber, out dwVerifyMode, out dwInOutMode, out dwYear, out dwMonth, out dwDay, out dwHour, out dwMinute, out dwSecond, ref dwWorkCode);
        }
        public bool SSR_GetGroupTZ(int dwMachineNumber, int GroupNo, ref int Tz1, ref int Tz2, ref int Tz3, ref int VaildHoliday, ref int VerifyStyle) { return false; }
        public bool SSR_GetHoliday(int dwMachineNumber, int HolidayID, ref int BeginMonth, ref int BeginDay, ref int EndMonth, ref int EndDay, ref int TimeZoneID) { return false; }
        public bool SSR_GetShortkey(int ShortKeyID, ref int ShortKeyFun, ref int StateCode, ref string StateName, ref int AutoChange, ref string AutoChangeTime) { return false; }
        public bool SSR_GetSuperLogData(int MachineNumber, out int Number, out string Admin, out string User, out int Manipulation, out string Time, out int Params1, out int Params2, out int Params3)
        {
            return bioRep.SSR_GetSuperLogData(MachineNumber, out Number, out Admin, out User, out Manipulation, out Time, out Params1, out Params2, out Params3);
        }
        public bool SSR_GetUnLockGroup(int dwMachineNumber, int CombNo, ref int Group1, ref int Group2, ref int Group3, ref int Group4, ref int Group5) { return false; }
        public bool SSR_GetUserInfo(int dwMachineNumber, string dwEnrollNumber, out string Name, out string Password, out int Privilege, out bool Enabled)
        {
            return bioRep.SSR_GetUserInfo(dwMachineNumber, dwEnrollNumber, out Name, out Password, out Privilege, out Enabled);
        }
        public bool SSR_GetUserTmp(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex, out byte TmpData, out int TmpLength) { return bioRep.SSR_GetUserTmp(dwMachineNumber, dwEnrollNumber, dwFingerIndex, out TmpData, out TmpLength); }
        public bool SSR_GetUserTmpStr(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex, out string TmpData, out int TmpLength) { return bioRep.SSR_GetUserTmpStr(dwMachineNumber, dwEnrollNumber, dwFingerIndex, out TmpData, out TmpLength); }
        public bool SSR_GetWorkCode(int AWorkCode, out string Name) { return bioRep.SSR_GetWorkCode(AWorkCode, out Name); }
        public bool SSR_OutPutHTMLRep(int dwMachineNumber, string dwEnrollNumber, string AttFile, string UserFile, string DeptFile, string TimeClassFile, string AttruleFile, int BYear, int BMonth, int BDay, int BHour, int BMinute, int BSecond, int EYear, int EMonth, int EDay, int EHour, int EMinute, int ESecond, string TempPath, string OutFileName, int HTMLFlag, int resv1, string resv2) { return false; }
        public bool SSR_SetGroupTZ(int dwMachineNumber, int GroupNo, int Tz1, int Tz2, int Tz3, int VaildHoliday, int VerifyStyle) { return false; }
        public bool SSR_SetHoliday(int dwMachineNumber, int HolidayID, int BeginMonth, int BeginDay, int EndMonth, int EndDay, int TimeZoneID) { return false; }
        public bool SSR_SetShortkey(int ShortKeyID, int ShortKeyFun, int StateCode, string StateName, int StateAutoChange, string StateAutoChangeTime) { return false; }
        public bool SSR_SetUnLockGroup(int dwMachineNumber, int CombNo, int Group1, int Group2, int Group3, int Group4, int Group5) { return false; }
        public bool SSR_SetUserInfo(int dwMachineNumber, string dwEnrollNumber, string Name, string Password, int Privilege, bool Enabled) { return false; }
        public bool SSR_SetUserInfoEx(int dwMachineNumber, string dwEnrollNumber, string Name, string Password, int Privilege, bool Enabled, string pis, string CPF) { return false; }
        public bool SSR_SetUserSMS(int dwMachineNumber, string dwEnrollNumber, int SMSID) { return false; }
        public bool SSR_SetUserTmp(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex, ref byte TmpData) { return false; }
        public bool SSR_SetUserTmpExt(int dwMachineNumber, int IsDeleted, string dwEnrollNumber, int dwFingerIndex, ref byte TmpData) { return false; }
        public bool SSR_SetUserTmpStr(int dwMachineNumber, string dwEnrollNumber, int dwFingerIndex, string TmpData) { return false; }
        public bool SSR_SetWorkCode(int AWorkCode, string Name) { return false; }
        public bool StartEnroll(int UserID, int FingerID) { return false; }
        public bool StartEnrollEx(string UserID, int FingerID, int Flag) { return false; }
        public bool StartIdentify() { return false; }
        public bool StartVerify(int UserID, int FingerID) { return false; }
        public bool UnlockREP(int dwMachineNumber) { return false; }
        public bool UpdateFile(string FileName) { return false; }
        public bool UpdateFirmware(string FirmwareFile) { return false; }
        public bool UpdateZKOS(int dwMachineNumber, string FileName) { return false; }
        public bool UseGroupTimeZone() { return false; }
        public bool WriteCard(int dwMachineNumber, int dwEnrollNumber, int dwFingerIndex1, ref byte TmpData1, int dwFingerIndex2, ref byte TmpData2, int dwFingerIndex3, ref byte TmpData3, int dwFingerIndex4, ref byte TmpData4) { return false; }
        public bool WriteCustData(int dwMachineNumber, string CustData) { return false; }
        public bool WriteLCD(int Row, int Col, string Text) { return false; }
        public bool GetUserIDByPIN2(int PIN2, ref int UserID) { return false; }
    }
}