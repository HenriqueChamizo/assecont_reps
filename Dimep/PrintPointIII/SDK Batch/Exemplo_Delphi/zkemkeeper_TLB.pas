unit zkemkeeper_TLB;

// ************************************************************************ //
// WARNING                                                                    
// -------                                                                    
// The types declared in this file were generated from data read from a       
// Type Library. If this type library is explicitly or indirectly (via        
// another type library referring to this type library) re-imported, or the   
// 'Refresh' command of the Type Library Editor activated while editing the   
// Type Library, the contents of this file will be regenerated and all        
// manual modifications will be lost.                                         
// ************************************************************************ //

// PASTLWTR : 1.2
// File generated on 19/01/2012 09:43:31 from Type Library described below.

// ************************************************************************  //
// Type Lib: C:\WINDOWS\system32\zkemkeeper.dll (1)
// LIBID: {FE9DED34-E159-408E-8490-B720A5E632C7}
// LCID: 0
// Helpfile: 
// HelpString: ZKEMKeeper 6.0 Control
// DepndLst: 
//   (1) v2.0 stdole, (C:\WINDOWS\system32\stdole2.tlb)
// Parent TypeLibrary:
//   (0) v2.2 WatchComm, (C:\Documents and Settings\Administrator\Desktop\Exemplo_Delphi\WatchComm.tlb)
// ************************************************************************ //
// *************************************************************************//
// NOTE:                                                                      
// Items guarded by $IFDEF_LIVE_SERVER_AT_DESIGN_TIME are used by properties  
// which return objects that may need to be explicitly created via a function 
// call prior to any access via the property. These items have been disabled  
// in order to prevent accidental use from within the object inspector. You   
// may enable them by defining LIVE_SERVER_AT_DESIGN_TIME or by selectively   
// removing them from the $IFDEF blocks. However, such items must still be    
// programmatically created via a method of the appropriate CoClass before    
// they can be used.                                                          
{$TYPEDADDRESS OFF} // Unit must be compiled without type-checked pointers. 
{$WARN SYMBOL_PLATFORM OFF}
{$WRITEABLECONST ON}
{$VARPROPSETTER ON}
interface

uses Windows, ActiveX, Classes, Graphics, OleCtrls, OleServer, StdVCL, Variants;
  


// *********************************************************************//
// GUIDS declared in the TypeLibrary. Following prefixes are used:        
//   Type Libraries     : LIBID_xxxx                                      
//   CoClasses          : CLASS_xxxx                                      
//   DISPInterfaces     : DIID_xxxx                                       
//   Non-DISP interfaces: IID_xxxx                                        
// *********************************************************************//
const
  // TypeLibrary Major and minor versions
  zkemkeeperMajorVersion = 1;
  zkemkeeperMinorVersion = 0;

  LIBID_zkemkeeper: TGUID = '{FE9DED34-E159-408E-8490-B720A5E632C7}';

  DIID__IZKEMEvents: TGUID = '{CF83B580-5D32-4C65-B44E-BEDC750CDFA8}';
  IID_IZKEM: TGUID = '{102F4206-E43D-4FC9-BAB0-331CFFE4D25B}';
  CLASS_CZKEM: TGUID = '{00853A19-BD51-419B-9269-2DABE57EB61F}';
type

// *********************************************************************//
// Forward declaration of types defined in TypeLibrary                    
// *********************************************************************//
  _IZKEMEvents = dispinterface;
  IZKEM = interface;
  IZKEMDisp = dispinterface;

// *********************************************************************//
// Declaration of CoClasses defined in Type Library                       
// (NOTE: Here we map each CoClass to its Default Interface)              
// *********************************************************************//
  CZKEM = IZKEM;


// *********************************************************************//
// Declaration of structures, unions and aliases.                         
// *********************************************************************//
  PShortint1 = ^Shortint; {*}
  PInteger1 = ^Integer; {*}
  PByte1 = ^Byte; {*}


// *********************************************************************//
// DispIntf:  _IZKEMEvents
// Flags:     (4096) Dispatchable
// GUID:      {CF83B580-5D32-4C65-B44E-BEDC750CDFA8}
// *********************************************************************//
  _IZKEMEvents = dispinterface
    ['{CF83B580-5D32-4C65-B44E-BEDC750CDFA8}']
    procedure OnAttTransaction(EnrollNumber: Integer; IsInValid: Integer; AttState: Integer; 
                               VerifyMethod: Integer; Year: Integer; Month: Integer; Day: Integer; 
                               Hour: Integer; Minute: Integer; Second: Integer); dispid 1;
    procedure OnKeyPress(Key: Integer); dispid 2;
    procedure OnEnrollFinger(EnrollNumber: Integer; FingerIndex: Integer; ActionResult: Integer; 
                             TemplateLength: Integer); dispid 3;
    procedure OnNewUser(EnrollNumber: Integer); dispid 4;
    procedure OnEMData(DataType: Integer; DataLen: Integer; var DataBuffer: {??Shortint}OleVariant); dispid 5;
    procedure OnConnected; dispid 6;
    procedure OnDisConnected; dispid 7;
    procedure OnFinger; dispid 8;
    procedure OnVerify(UserID: Integer); dispid 9;
    procedure OnFingerFeature(Score: Integer); dispid 10;
    procedure OnHIDNum(CardNumber: Integer); dispid 11;
    procedure OnDoor(EventType: Integer); dispid 12;
    procedure OnAlarm(AlarmType: Integer; EnrollNumber: Integer; Verified: Integer); dispid 13;
    procedure OnWriteCard(EnrollNumber: Integer; ActionResult: Integer; Length: Integer); dispid 14;
    procedure OnEmptyCard(ActionResult: Integer); dispid 15;
    procedure OnDeleteTemplate(EnrollNumber: Integer; FingerIndex: Integer); dispid 16;
    procedure OnAttTransactionEx(const EnrollNumber: WideString; IsInValid: Integer; 
                                 AttState: Integer; VerifyMethod: Integer; Year: Integer; 
                                 Month: Integer; Day: Integer; Hour: Integer; Minute: Integer; 
                                 Second: Integer; WorkCode: Integer); dispid 17;
    procedure OnEnrollFingerEx(const EnrollNumber: WideString; FingerIndex: Integer; 
                               ActionResult: Integer; TemplateLength: Integer); dispid 18;
  end;

// *********************************************************************//
// Interface: IZKEM
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {102F4206-E43D-4FC9-BAB0-331CFFE4D25B}
// *********************************************************************//
  IZKEM = interface(IDispatch)
    ['{102F4206-E43D-4FC9-BAB0-331CFFE4D25B}']
    function Get_ReadMark: WordBool; safecall;
    procedure Set_ReadMark(pVal: WordBool); safecall;
    function Get_CommPort: Integer; safecall;
    procedure Set_CommPort(pVal: Integer); safecall;
    function ClearAdministrators(dwMachineNumber: Integer): WordBool; safecall;
    function DeleteEnrollData(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                              dwEMachineNumber: Integer; dwBackupNumber: Integer): WordBool; safecall;
    function ReadSuperLogData(dwMachineNumber: Integer): WordBool; safecall;
    function ReadAllSLogData(dwMachineNumber: Integer): WordBool; safecall;
    function ReadGeneralLogData(dwMachineNumber: Integer): WordBool; safecall;
    function ReadAllGLogData(dwMachineNumber: Integer): WordBool; safecall;
    function EnableUser(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                        dwEMachineNumber: Integer; dwBackupNumber: Integer; bFlag: WordBool): WordBool; safecall;
    function EnableDevice(dwMachineNumber: Integer; bFlag: WordBool): WordBool; safecall;
    function GetDeviceStatus(dwMachineNumber: Integer; dwStatus: Integer; var dwValue: Integer): WordBool; safecall;
    function GetDeviceInfo(dwMachineNumber: Integer; dwInfo: Integer; var dwValue: Integer): WordBool; safecall;
    function SetDeviceInfo(dwMachineNumber: Integer; dwInfo: Integer; dwValue: Integer): WordBool; safecall;
    function SetDeviceTime(dwMachineNumber: Integer): WordBool; safecall;
    procedure PowerOnAllDevice; safecall;
    function PowerOffDevice(dwMachineNumber: Integer): WordBool; safecall;
    function ModifyPrivilege(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                             dwEMachineNumber: Integer; dwBackupNumber: Integer; 
                             dwMachinePrivilege: Integer): WordBool; safecall;
    procedure GetLastError(var dwErrorCode: Integer); safecall;
    function GetEnrollData(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                           dwEMachineNumber: Integer; dwBackupNumber: Integer; 
                           var dwMachinePrivilege: Integer; var dwEnrollData: Integer; 
                           var dwPassWord: Integer): WordBool; safecall;
    function SetEnrollData(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                           dwEMachineNumber: Integer; dwBackupNumber: Integer; 
                           dwMachinePrivilege: Integer; var dwEnrollData: Integer; 
                           dwPassWord: Integer): WordBool; safecall;
    function GetDeviceTime(dwMachineNumber: Integer; var dwYear: Integer; var dwMonth: Integer; 
                           var dwDay: Integer; var dwHour: Integer; var dwMinute: Integer; 
                           var dwSecond: Integer): WordBool; safecall;
    function GetGeneralLogData(dwMachineNumber: Integer; var dwTMachineNumber: Integer; 
                               var dwEnrollNumber: Integer; var dwEMachineNumber: Integer; 
                               var dwVerifyMode: Integer; var dwInOutMode: Integer; 
                               var dwYear: Integer; var dwMonth: Integer; var dwDay: Integer; 
                               var dwHour: Integer; var dwMinute: Integer): WordBool; safecall;
    function GetSuperLogData(dwMachineNumber: Integer; var dwTMachineNumber: Integer; 
                             var dwSEnrollNumber: Integer; var Params4: Integer; 
                             var Params1: Integer; var Params2: Integer; 
                             var dwManipulation: Integer; var Params3: Integer; 
                             var dwYear: Integer; var dwMonth: Integer; var dwDay: Integer; 
                             var dwHour: Integer; var dwMinute: Integer): WordBool; safecall;
    function GetAllSLogData(dwMachineNumber: Integer; var dwTMachineNumber: Integer; 
                            var dwSEnrollNumber: Integer; var Params4: Integer; 
                            var Params1: Integer; var Params2: Integer; 
                            var dwManipulation: Integer; var Params3: Integer; var dwYear: Integer; 
                            var dwMonth: Integer; var dwDay: Integer; var dwHour: Integer; 
                            var dwMinute: Integer): WordBool; safecall;
    function GetAllGLogData(dwMachineNumber: Integer; var dwTMachineNumber: Integer; 
                            var dwEnrollNumber: Integer; var dwEMachineNumber: Integer; 
                            var dwVerifyMode: Integer; var dwInOutMode: Integer; 
                            var dwYear: Integer; var dwMonth: Integer; var dwDay: Integer; 
                            var dwHour: Integer; var dwMinute: Integer): WordBool; safecall;
    procedure ConvertPassword(dwSrcPSW: Integer; var dwDestPSW: Integer; dwLength: Integer); safecall;
    function ReadAllUserID(dwMachineNumber: Integer): WordBool; safecall;
    function GetAllUserID(dwMachineNumber: Integer; var dwEnrollNumber: Integer; 
                          var dwEMachineNumber: Integer; var dwBackupNumber: Integer; 
                          var dwMachinePrivilege: Integer; var dwEnable: Integer): WordBool; safecall;
    function GetSerialNumber(dwMachineNumber: Integer; out dwSerialNumber: WideString): WordBool; safecall;
    function ClearKeeperData(dwMachineNumber: Integer): WordBool; safecall;
    function GetBackupNumber(dwMachineNumber: Integer): Integer; safecall;
    function GetProductCode(dwMachineNumber: Integer; out lpszProductCode: WideString): WordBool; safecall;
    function GetFirmwareVersion(dwMachineNumber: Integer; var strVersion: WideString): WordBool; safecall;
    function GetSDKVersion(var strVersion: WideString): WordBool; safecall;
    function ClearGLog(dwMachineNumber: Integer): WordBool; safecall;
    function GetFPTempLength(var dwEnrollData: Byte): Integer; safecall;
    function Connect_Com(ComPort: Integer; MachineNumber: Integer; BaudRate: Integer): WordBool; safecall;
    function Connect_Net(const IPAdd: WideString; Port: Integer): WordBool; safecall;
    procedure Disconnect; safecall;
    function SetUserInfo(dwMachineNumber: Integer; dwEnrollNumber: Integer; const Name: WideString; 
                         const Password: WideString; Privilege: Integer; Enabled: WordBool): WordBool; safecall;
    function GetUserInfo(dwMachineNumber: Integer; dwEnrollNumber: Integer; var Name: WideString; 
                         var Password: WideString; var Privilege: Integer; var Enabled: WordBool): WordBool; safecall;
    function SetDeviceIP(dwMachineNumber: Integer; const IPAddr: WideString): WordBool; safecall;
    function GetDeviceIP(dwMachineNumber: Integer; var IPAddr: WideString): WordBool; safecall;
    function GetUserTmp(dwMachineNumber: Integer; dwEnrollNumber: Integer; dwFingerIndex: Integer; 
                        var TmpData: Byte; var TmpLength: Integer): WordBool; safecall;
    function SetUserTmp(dwMachineNumber: Integer; dwEnrollNumber: Integer; dwFingerIndex: Integer; 
                        var TmpData: Byte): WordBool; safecall;
    function GetAllUserInfo(dwMachineNumber: Integer; var dwEnrollNumber: Integer; 
                            var Name: WideString; var Password: WideString; var Privilege: Integer; 
                            var Enabled: WordBool): WordBool; safecall;
    function DelUserTmp(dwMachineNumber: Integer; dwEnrollNumber: Integer; dwFingerIndex: Integer): WordBool; safecall;
    function RefreshData(dwMachineNumber: Integer): WordBool; safecall;
    function FPTempConvert(var TmpData1: Byte; var TmpData2: Byte; var Size: Integer): WordBool; safecall;
    function SetCommPassword(CommKey: Integer): WordBool; safecall;
    function GetUserGroup(dwMachineNumber: Integer; dwEnrollNumber: Integer; var UserGrp: Integer): WordBool; safecall;
    function SetUserGroup(dwMachineNumber: Integer; dwEnrollNumber: Integer; UserGrp: Integer): WordBool; safecall;
    function GetTZInfo(dwMachineNumber: Integer; TZIndex: Integer; var TZ: WideString): WordBool; safecall;
    function SetTZInfo(dwMachineNumber: Integer; TZIndex: Integer; const TZ: WideString): WordBool; safecall;
    function GetUnlockGroups(dwMachineNumber: Integer; var Grps: WideString): WordBool; safecall;
    function SetUnlockGroups(dwMachineNumber: Integer; const Grps: WideString): WordBool; safecall;
    function GetGroupTZs(dwMachineNumber: Integer; GroupIndex: Integer; var TZs: Integer): WordBool; safecall;
    function SetGroupTZs(dwMachineNumber: Integer; GroupIndex: Integer; var TZs: Integer): WordBool; safecall;
    function GetUserTZs(dwMachineNumber: Integer; dwEnrollNumber: Integer; var TZs: Integer): WordBool; safecall;
    function SetUserTZs(dwMachineNumber: Integer; dwEnrollNumber: Integer; var TZs: Integer): WordBool; safecall;
    function ACUnlock(dwMachineNumber: Integer; Delay: Integer): WordBool; safecall;
    function GetACFun(var ACFun: Integer): WordBool; safecall;
    function Get_ConvertBIG5: Integer; safecall;
    procedure Set_ConvertBIG5(pVal: Integer); safecall;
    function GetGeneralLogDataStr(dwMachineNumber: Integer; var dwEnrollNumber: Integer; 
                                  var dwVerifyMode: Integer; var dwInOutMode: Integer; 
                                  var TimeStr: WideString): WordBool; safecall;
    function GetUserTmpStr(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                           dwFingerIndex: Integer; var TmpData: WideString; var TmpLength: Integer): WordBool; safecall;
    function SetUserTmpStr(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                           dwFingerIndex: Integer; const TmpData: WideString): WordBool; safecall;
    function GetEnrollDataStr(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                              dwEMachineNumber: Integer; dwBackupNumber: Integer; 
                              var dwMachinePrivilege: Integer; var dwEnrollData: WideString; 
                              var dwPassWord: Integer): WordBool; safecall;
    function SetEnrollDataStr(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                              dwEMachineNumber: Integer; dwBackupNumber: Integer; 
                              dwMachinePrivilege: Integer; const dwEnrollData: WideString; 
                              dwPassWord: Integer): WordBool; safecall;
    function GetGroupTZStr(dwMachineNumber: Integer; GroupIndex: Integer; var TZs: WideString): WordBool; safecall;
    function SetGroupTZStr(dwMachineNumber: Integer; GroupIndex: Integer; const TZs: WideString): WordBool; safecall;
    function GetUserTZStr(dwMachineNumber: Integer; dwEnrollNumber: Integer; var TZs: WideString): WordBool; safecall;
    function SetUserTZStr(dwMachineNumber: Integer; dwEnrollNumber: Integer; const TZs: WideString): WordBool; safecall;
    function FPTempConvertStr(const TmpData1: WideString; var TmpData2: WideString; 
                              var Size: Integer): WordBool; safecall;
    function GetFPTempLengthStr(const dwEnrollData: WideString): Integer; safecall;
    function Get_BASE64: Integer; safecall;
    procedure Set_BASE64(pVal: Integer); safecall;
    function Get_PIN2: LongWord; safecall;
    procedure Set_PIN2(pVal: LongWord); safecall;
    function Get_AccGroup: Integer; safecall;
    procedure Set_AccGroup(pVal: Integer); safecall;
    function Get_AccTimeZones(Index: Integer): Integer; safecall;
    procedure Set_AccTimeZones(Index: Integer; pVal: Integer); safecall;
    function GetUserInfoByPIN2(dwMachineNumber: Integer; var Name: WideString; 
                               var Password: WideString; var Privilege: Integer; 
                               var Enabled: WordBool): WordBool; safecall;
    function GetUserInfoByCard(dwMachineNumber: Integer; var Name: WideString; 
                               var Password: WideString; var Privilege: Integer; 
                               var Enabled: WordBool): WordBool; safecall;
    function Get_CardNumber(Index: Integer): Integer; safecall;
    procedure Set_CardNumber(Index: Integer; pVal: Integer); safecall;
    function CaptureImage(FullImage: WordBool; var Width: Integer; var Height: Integer; 
                          var Image: Byte; const ImageFile: WideString): WordBool; safecall;
    function UpdateFirmware(const FirmwareFile: WideString): WordBool; safecall;
    function StartEnroll(UserID: Integer; FingerID: Integer): WordBool; safecall;
    function StartVerify(UserID: Integer; FingerID: Integer): WordBool; safecall;
    function StartIdentify: WordBool; safecall;
    function CancelOperation: WordBool; safecall;
    function QueryState(var State: Integer): WordBool; safecall;
    function BackupData(const DataFile: WideString): WordBool; safecall;
    function RestoreData(const DataFile: WideString): WordBool; safecall;
    function WriteLCD(Row: Integer; Col: Integer; const Text: WideString): WordBool; safecall;
    function ClearLCD: WordBool; safecall;
    function Beep(DelayMS: Integer): WordBool; safecall;
    function PlayVoice(Position: Integer; Length: Integer): WordBool; safecall;
    function PlayVoiceByIndex(Index: Integer): WordBool; safecall;
    function EnableClock(Enabled: Integer): WordBool; safecall;
    function GetUserIDByPIN2(PIN2: Integer; var UserID: Integer): WordBool; safecall;
    function Get_PINWidth: Integer; safecall;
    function GetPIN2(UserID: Integer; var PIN2: Integer): WordBool; safecall;
    function FPTempConvertNew(var TmpData1: Byte; var TmpData2: Byte; var Size: Integer): WordBool; safecall;
    function FPTempConvertNewStr(const TmpData1: WideString; var TmpData2: WideString; 
                                 var Size: Integer): WordBool; safecall;
    function ReadAllTemplate(dwMachineNumber: Integer): WordBool; safecall;
    function DisableDeviceWithTimeOut(dwMachineNumber: Integer; TimeOutSec: Integer): WordBool; safecall;
    function SetDeviceTime2(dwMachineNumber: Integer; dwYear: Integer; dwMonth: Integer; 
                            dwDay: Integer; dwHour: Integer; dwMinute: Integer; dwSecond: Integer): WordBool; safecall;
    function ClearSLog(dwMachineNumber: Integer): WordBool; safecall;
    function RestartDevice(dwMachineNumber: Integer): WordBool; safecall;
    function GetDeviceMAC(dwMachineNumber: Integer; var sMAC: WideString): WordBool; safecall;
    function SetDeviceMAC(dwMachineNumber: Integer; const sMAC: WideString): WordBool; safecall;
    function GetWiegandFmt(dwMachineNumber: Integer; var sWiegandFmt: WideString): WordBool; safecall;
    function SetWiegandFmt(dwMachineNumber: Integer; const sWiegandFmt: WideString): WordBool; safecall;
    function ClearSMS(dwMachineNumber: Integer): WordBool; safecall;
    function GetSMS(dwMachineNumber: Integer; ID: Integer; var Tag: Integer; 
                    var ValidMinutes: Integer; var StartTime: WideString; var Content: WideString): WordBool; safecall;
    function SetSMS(dwMachineNumber: Integer; ID: Integer; Tag: Integer; ValidMinutes: Integer; 
                    const StartTime: WideString; const Content: WideString): WordBool; safecall;
    function DeleteSMS(dwMachineNumber: Integer; ID: Integer): WordBool; safecall;
    function SetUserSMS(dwMachineNumber: Integer; dwEnrollNumber: Integer; SMSID: Integer): WordBool; safecall;
    function DeleteUserSMS(dwMachineNumber: Integer; dwEnrollNumber: Integer; SMSID: Integer): WordBool; safecall;
    function GetCardFun(dwMachineNumber: Integer; var CardFun: Integer): WordBool; safecall;
    function ClearUserSMS(dwMachineNumber: Integer): WordBool; safecall;
    function Get_MachineNumber: Integer; safecall;
    procedure Set_MachineNumber(pVal: Integer); safecall;
    function SetDeviceCommPwd(dwMachineNumber: Integer; CommKey: Integer): WordBool; safecall;
    function GetDoorState(MachineNumber: Integer; var State: Integer): WordBool; safecall;
    function GetVendor(var strVendor: WideString): WordBool; safecall;
    function GetSensorSN(dwMachineNumber: Integer; var SensorSN: WideString): WordBool; safecall;
    function ReadCustData(dwMachineNumber: Integer; var CustData: WideString): WordBool; safecall;
    function WriteCustData(dwMachineNumber: Integer; const CustData: WideString): WordBool; safecall;
    function BeginBatchUpdate(dwMachineNumber: Integer; UpdateFlag: Integer): WordBool; safecall;
    function BatchUpdate(dwMachineNumber: Integer): WordBool; safecall;
    function ClearData(dwMachineNumber: Integer; DataFlag: Integer): WordBool; safecall;
    function GetDataFile(dwMachineNumber: Integer; DataFlag: Integer; const FileName: WideString): WordBool; safecall;
    function WriteCard(dwMachineNumber: Integer; dwEnrollNumber: Integer; dwFingerIndex1: Integer; 
                       var TmpData1: Byte; dwFingerIndex2: Integer; var TmpData2: Byte; 
                       dwFingerIndex3: Integer; var TmpData3: Byte; dwFingerIndex4: Integer; 
                       var TmpData4: Byte): WordBool; safecall;
    function GetGeneralExtLogData(dwMachineNumber: Integer; var dwEnrollNumber: Integer; 
                                  var dwVerifyMode: Integer; var dwInOutMode: Integer; 
                                  var dwYear: Integer; var dwMonth: Integer; var dwDay: Integer; 
                                  var dwHour: Integer; var dwMinute: Integer; 
                                  var dwSecond: Integer; var dwWorkCode: Integer; 
                                  var dwReserved: Integer): WordBool; safecall;
    function EmptyCard(dwMachineNumber: Integer): WordBool; safecall;
    function GetDeviceStrInfo(dwMachineNumber: Integer; dwInfo: Integer; out Value: WideString): WordBool; safecall;
    function GetSysOption(dwMachineNumber: Integer; const Option: WideString; out Value: WideString): WordBool; safecall;
    function SetUserInfoEx(dwMachineNumber: Integer; dwEnrollNumber: Integer; VerifyStyle: Integer; 
                           var Reserved: Byte): WordBool; safecall;
    function GetUserInfoEx(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                           out VerifyStyle: Integer; out Reserved: Byte): WordBool; safecall;
    function DeleteUserInfoEx(dwMachineNumber: Integer; dwEnrollNumber: Integer): WordBool; safecall;
    function SSR_GetGeneralLogData(dwMachineNumber: Integer; out dwEnrollNumber: WideString; 
                                   out dwVerifyMode: Integer; out dwInOutMode: Integer; 
                                   out dwYear: Integer; out dwMonth: Integer; out dwDay: Integer; 
                                   out dwHour: Integer; out dwMinute: Integer; 
                                   out dwSecond: Integer; var dwWorkCode: Integer): WordBool; safecall;
    function SSR_GetAllUserInfo(dwMachineNumber: Integer; out dwEnrollNumber: WideString; 
                                out Name: WideString; out Password: WideString; 
                                out Privilege: Integer; out Enabled: WordBool): WordBool; safecall;
    function SSR_GetUserInfo(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                             out Name: WideString; out Password: WideString; 
                             out Privilege: Integer; out Enabled: WordBool): WordBool; safecall;
    function SSR_GetUserTmp(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            dwFingerIndex: Integer; out TmpData: Byte; out TmpLength: Integer): WordBool; safecall;
    function SSR_GetUserTmpStr(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                               dwFingerIndex: Integer; out TmpData: WideString; 
                               out TmpLength: Integer): WordBool; safecall;
    function SSR_DeleteEnrollData(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                                  dwBackupNumber: Integer): WordBool; safecall;
    function SSR_SetUserInfo(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                             const Name: WideString; const Password: WideString; 
                             Privilege: Integer; Enabled: WordBool): WordBool; safecall;
    function SSR_SetUserTmp(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            dwFingerIndex: Integer; var TmpData: Byte): WordBool; safecall;
    function SSR_SetUserTmpStr(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                               dwFingerIndex: Integer; const TmpData: WideString): WordBool; safecall;
    function SSR_DelUserTmp(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            dwFingerIndex: Integer): WordBool; safecall;
    function Get_STR_CardNumber(Index: Integer): WideString; safecall;
    procedure Set_STR_CardNumber(Index: Integer; const pVal: WideString); safecall;
    function SetWorkCode(WorkCodeID: Integer; AWorkCode: Integer): WordBool; safecall;
    function GetWorkCode(WorkCodeID: Integer; out AWorkCode: Integer): WordBool; safecall;
    function DeleteWorkCode(WorkCodeID: Integer): WordBool; safecall;
    function ClearWorkCode: WordBool; safecall;
    function ReadAttRule(dwMachineNumber: Integer): WordBool; safecall;
    function ReadDPTInfo(dwMachineNumber: Integer): WordBool; safecall;
    function SaveTheDataToFile(dwMachineNumber: Integer; const TheFilePath: WideString; 
                               FileFlag: Integer): WordBool; safecall;
    function ReadTurnInfo(dwMachineNumber: Integer): WordBool; safecall;
    function SSR_OutPutHTMLRep(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                               const AttFile: WideString; const UserFile: WideString; 
                               const DeptFile: WideString; const TimeClassFile: WideString; 
                               const AttruleFile: WideString; BYear: Integer; BMonth: Integer; 
                               BDay: Integer; BHour: Integer; BMinute: Integer; BSecond: Integer; 
                               EYear: Integer; EMonth: Integer; EDay: Integer; EHour: Integer; 
                               EMinute: Integer; ESecond: Integer; const TempPath: WideString; 
                               const OutFileName: WideString; HTMLFlag: Integer; resv1: Integer; 
                               const resv2: WideString): WordBool; safecall;
    function ReadAOptions(const AOption: WideString; out AValue: WideString): WordBool; safecall;
    function ReadRTLog(dwMachineNumber: Integer): WordBool; safecall;
    function GetRTLog(dwMachineNumber: Integer): WordBool; safecall;
    function GetHIDEventCardNumAsStr(out strHIDEventCardNum: WideString): WordBool; safecall;
    function GetStrCardNumber(out ACardNumber: WideString): WordBool; safecall;
    function SetStrCardNumber(const ACardNumber: WideString): WordBool; safecall;
    function RegEvent(dwMachineNumber: Integer; EventMask: Integer): WordBool; safecall;
    function CancelBatchUpdate(dwMachineNumber: Integer): WordBool; safecall;
    function SetSysOption(dwMachineNumber: Integer; const Option: WideString; 
                          const Value: WideString): WordBool; safecall;
    function Connect_Modem(ComPort: Integer; MachineNumber: Integer; BaudRate: Integer; 
                           const Telephone: WideString): WordBool; safecall;
    function UseGroupTimeZone: WordBool; safecall;
    function SetHoliday(dwMachineNumber: Integer; const Holiday: WideString): WordBool; safecall;
    function GetHoliday(dwMachineNumber: Integer; var Holiday: WideString): WordBool; safecall;
    function SetDaylight(dwMachineNumber: Integer; Support: Integer; const BeginTime: WideString; 
                         const EndTime: WideString): WordBool; safecall;
    function GetDaylight(dwMachineNumber: Integer; var Support: Integer; var BeginTime: WideString; 
                         var EndTime: WideString): WordBool; safecall;
    function SSR_SetUnLockGroup(dwMachineNumber: Integer; CombNo: Integer; Group1: Integer; 
                                Group2: Integer; Group3: Integer; Group4: Integer; Group5: Integer): WordBool; safecall;
    function SSR_GetUnLockGroup(dwMachineNumber: Integer; CombNo: Integer; var Group1: Integer; 
                                var Group2: Integer; var Group3: Integer; var Group4: Integer; 
                                var Group5: Integer): WordBool; safecall;
    function SSR_SetGroupTZ(dwMachineNumber: Integer; GroupNo: Integer; Tz1: Integer; Tz2: Integer; 
                            Tz3: Integer; VaildHoliday: Integer; VerifyStyle: Integer): WordBool; safecall;
    function SSR_GetGroupTZ(dwMachineNumber: Integer; GroupNo: Integer; var Tz1: Integer; 
                            var Tz2: Integer; var Tz3: Integer; var VaildHoliday: Integer; 
                            var VerifyStyle: Integer): WordBool; safecall;
    function SSR_GetHoliday(dwMachineNumber: Integer; HolidayID: Integer; var BeginMonth: Integer; 
                            var BeginDay: Integer; var EndMonth: Integer; var EndDay: Integer; 
                            var TimeZoneID: Integer): WordBool; safecall;
    function SSR_SetHoliday(dwMachineNumber: Integer; HolidayID: Integer; BeginMonth: Integer; 
                            BeginDay: Integer; EndMonth: Integer; EndDay: Integer; 
                            TimeZoneID: Integer): WordBool; safecall;
    function GetPlatform(dwMachineNumber: Integer; var Platform: WideString): WordBool; safecall;
    function SSR_SetUserSMS(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            SMSID: Integer): WordBool; safecall;
    function SSR_DeleteUserSMS(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                               SMSID: Integer): WordBool; safecall;
    function IsTFTMachine(dwMachineNumber: Integer): WordBool; safecall;
    function SSR_EnableUser(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            bFlag: WordBool): WordBool; safecall;
    function Get_SSRPin: Integer; safecall;
    function SendCMDMsg(dwMachineNumber: Integer; Param1: Integer; Param2: Integer): WordBool; safecall;
    function SendFile(dwMachineNumber: Integer; const FileName: WideString): WordBool; safecall;
    function SetLanguageByID(dwMachineNumber: Integer; LanguageID: Integer; 
                             const Language: WideString): WordBool; safecall;
    function ReadFile(dwMachineNumber: Integer; const FileName: WideString; 
                      const FilePath: WideString): WordBool; safecall;
    function SetLastCount(count: Integer): WordBool; safecall;
    function SetCustomizeAttState(dwMachineNumber: Integer; StateID: Integer; NewState: Integer): WordBool; safecall;
    function DelCustomizeAttState(dwMachineNumber: Integer; StateID: Integer): WordBool; safecall;
    function EnableCustomizeAttState(dwMachineNumber: Integer; StateID: Integer; Enable: Integer): WordBool; safecall;
    function SetCustomizeVoice(dwMachineNumber: Integer; VoiceID: Integer; 
                               const FileName: WideString): WordBool; safecall;
    function DelCustomizeVoice(dwMachineNumber: Integer; VoiceID: Integer): WordBool; safecall;
    function EnableCustomizeVoice(dwMachineNumber: Integer; VoiceID: Integer; Enable: Integer): WordBool; safecall;
    function StartEnrollEx(const UserID: WideString; FingerID: Integer; Flag: Integer): WordBool; safecall;
    function SSR_SetUserTmpExt(dwMachineNumber: Integer; IsDeleted: Integer; 
                               const dwEnrollNumber: WideString; dwFingerIndex: Integer; 
                               var TmpData: Byte): WordBool; safecall;
    function SSR_DelUserTmpExt(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                               dwFingerIndex: Integer): WordBool; safecall;
    function SSR_DeleteEnrollDataExt(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                                     dwBackupNumber: Integer): WordBool; safecall;
    function SSR_GetWorkCode(AWorkCode: Integer; out Name: WideString): WordBool; safecall;
    function SSR_SetWorkCode(AWorkCode: Integer; const Name: WideString): WordBool; safecall;
    function SSR_DeleteWorkCode(PIN: Integer): WordBool; safecall;
    function SSR_ClearWorkCode: WordBool; safecall;
    function SSR_GetSuperLogData(MachineNumber: Integer; out Number: Integer; 
                                 out Admin: WideString; out User: WideString; 
                                 out Manipulation: Integer; out Time: WideString; 
                                 out Params1: Integer; out Params2: Integer; out Params3: Integer): WordBool; safecall;
    function SSR_SetShortkey(ShortKeyID: Integer; ShortKeyFun: Integer; StateCode: Integer; 
                             const StateName: WideString; StateAutoChange: Integer; 
                             const StateAutoChangeTime: WideString): WordBool; safecall;
    function SSR_GetShortkey(ShortKeyID: Integer; var ShortKeyFun: Integer; var StateCode: Integer; 
                             var StateName: WideString; var AutoChange: Integer; 
                             var AutoChangeTime: WideString): WordBool; safecall;
    function Connect_USB(MachineNumber: Integer): WordBool; safecall;
    function GetSuperLogData2(dwMachineNumber: Integer; var dwTMachineNumber: Integer; 
                              var dwSEnrollNumber: Integer; var Params4: Integer; 
                              var Params1: Integer; var Params2: Integer; 
                              var dwManipulation: Integer; var Params3: Integer; 
                              var dwYear: Integer; var dwMonth: Integer; var dwDay: Integer; 
                              var dwHour: Integer; var dwMinute: Integer; var dwSecs: Integer): WordBool; safecall;
    function GetUserFace(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                         dwFaceIndex: Integer; var TmpData: Byte; var TmpLength: Integer): WordBool; safecall;
    function SetUserFace(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                         dwFaceIndex: Integer; var TmpData: Byte; TmpLength: Integer): WordBool; safecall;
    function DelUserFace(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                         dwFaceIndex: Integer): WordBool; safecall;
    function GetUserFaceStr(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            dwFaceIndex: Integer; var TmpData: WideString; var TmpLength: Integer): WordBool; safecall;
    function SetUserFaceStr(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            dwFaceIndex: Integer; const TmpData: WideString; TmpLength: Integer): WordBool; safecall;
    function GetUserTmpEx(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                          dwFingerIndex: Integer; out Flag: Integer; out TmpData: Byte; 
                          out TmpLength: Integer): WordBool; safecall;
    function GetUserTmpExStr(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                             dwFingerIndex: Integer; out Flag: Integer; out TmpData: WideString; 
                             out TmpLength: Integer): WordBool; safecall;
    function SetUserTmpEx(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                          dwFingerIndex: Integer; Flag: Integer; var TmpData: Byte): WordBool; safecall;
    function SetUserTmpExStr(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                             dwFingerIndex: Integer; Flag: Integer; const TmpData: WideString): WordBool; safecall;
    function MergeTemplate(var Templates: PByte1; FingerCount: Integer; var TemplateDest: Byte; 
                           var FingerSize: Integer): WordBool; safecall;
    function SplitTemplate(var Template: Byte; var Templates: PByte1; var FingerCount: Integer; 
                           var FingerSize: Integer): WordBool; safecall;
    function Get_PullMode: Integer; safecall;
    procedure Set_PullMode(pVal: Integer); safecall;
    function ReadUserAllTemplate(dwMachineNumber: Integer; const dwEnrollNumber: WideString): WordBool; safecall;
    function UpdateFile(const FileName: WideString): WordBool; safecall;
    function ReadLastestLogData(dwMachineNumber: Integer; NewLog: Integer; dwYear: Integer; 
                                dwMonth: Integer; dwDay: Integer; dwHour: Integer; 
                                dwMinute: Integer; dwSecond: Integer): WordBool; safecall;
    function SetOptionCommPwd(dwMachineNumber: Integer; const CommKey: WideString): WordBool; safecall;
    function ReadSuperLogDataEx(dwMachineNumber: Integer; dwYear_S: Integer; dwMonth_S: Integer; 
                                dwDay_S: Integer; dwHour_S: Integer; dwMinute_S: Integer; 
                                dwSecond_S: Integer; dwYear_E: Integer; dwMonth_E: Integer; 
                                dwDay_E: Integer; dwHour_E: Integer; dwMinute_E: Integer; 
                                dwSecond_E: Integer; dwLogIndex: Integer): WordBool; safecall;
    function GetSuperLogDataEx(dwMachineNumber: Integer; var EnrollNumber: WideString; 
                               var Params4: Integer; var Params1: Integer; var Params2: Integer; 
                               var dwManipulation: Integer; var Params3: Integer; 
                               var dwYear: Integer; var dwMonth: Integer; var dwDay: Integer; 
                               var dwHour: Integer; var dwMinute: Integer; var dwSecond: Integer): WordBool; safecall;
    function GetBZ400FirmwareVersion(dwMachineNumber: Integer; var strVersion: WideString): WordBool; safecall;
    function UnlockREP(dwMachineNumber: Integer): WordBool; safecall;
    function RefreshOptions(dwMachineNumber: Integer): WordBool; safecall;
    function GetMRPTotal(dwMachineNumber: Integer; MRPType: Integer; var Value: Integer): WordBool; safecall;
    function GetUnlockPwd(const LockSerialNumber: WideString; var UnlockPwd: WideString): WordBool; safecall;
    function SetSeekPosition(dwMachineNumber: Integer; Position: Integer): WordBool; safecall;
    function GetDatetimeOpLog(dwMachineNumber: Integer; out nsr: WideString; 
                              out OldDatetime: WideString; out NewDatetime: WideString): WordBool; safecall;
    function GetEmployerOpLog(dwMachineNumber: Integer; out nsr: WideString; out Year: Integer; 
                              out Month: Integer; out Day: Integer; out Hour: Integer; 
                              out min: Integer; out sec: Integer; out cnpj_cpf: WideString; 
                              out identtype: Integer; out cei: WideString; out Name: WideString; 
                              out address: WideString; out OpType: WideString): WordBool; safecall;
    function GetEmployeeOpLog(dwMachineNumber: Integer; out nsr: WideString; out Year: Integer; 
                              out Month: Integer; out Day: Integer; out Hour: Integer; 
                              out min: Integer; out sec: Integer; out OpType: WideString; 
                              out pis: WideString; out Name: WideString): WordBool; safecall;
    function GetEmployer(dwMachineNumber: Integer; out Name: WideString; out CertType: WideString; 
                         out Tax: WideString; out cei: WideString; out Addr: WideString): WordBool; safecall;
    function SetEmployer(dwMachineNumber: Integer; const Name: WideString; CertType: Integer; 
                         const Tax: WideString; const cei: WideString; const Addr: WideString): WordBool; safecall;
    function GetAttLogs(dwMachineNumber: Integer; out nsr: WideString; out pis: WideString; 
                        out dwYear: Integer; out dwMonth: Integer; out dwDay: Integer; 
                        out dwHour: Integer; out dwMinute: Integer; out dwSecond: Integer): WordBool; safecall;
    function SSR_GetAllUserInfoEx(dwMachineNumber: Integer; out dwEnrollNumber: WideString; 
                                  out Name: WideString; out Password: WideString; 
                                  out Privilege: Integer; out Enabled: WordBool; 
                                  out pis: WideString; out CPF: WideString): WordBool; safecall;
    function SSR_SetUserInfoEx(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                               const Name: WideString; const Password: WideString; 
                               Privilege: Integer; Enabled: WordBool; const pis: WideString; 
                               const CPF: WideString): WordBool; safecall;
    function GetUserTmpEx_BZ400(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                                dwFingerIndex: Integer; out Flag: Integer; out TmpData: Byte; 
                                out TmpLength: Integer): WordBool; safecall;
    function GetUserTmpExStr_BZ400(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                                   dwFingerIndex: Integer; out Flag: Integer; 
                                   out TmpData: WideString; out TmpLength: Integer): WordBool; safecall;
    function SetUserTmpEx_BZ400(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                                dwFingerIndex: Integer; Flag: Integer; var TmpData: Byte): WordBool; safecall;
    function SetUserTmpExStr_BZ400(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                                   dwFingerIndex: Integer; Flag: Integer; const TmpData: WideString): WordBool; safecall;
    function SSR_DeleteEnrollDataExt_BZ400(dwMachineNumber: Integer; 
                                           const dwEnrollNumber: WideString; dwBackupNumber: Integer): WordBool; safecall;
    function GetRealFirmwareVersion(dwMachineNumber: Integer; var strVersion: WideString): WordBool; safecall;
    property ReadMark: WordBool read Get_ReadMark write Set_ReadMark;
    property CommPort: Integer read Get_CommPort write Set_CommPort;
    property ConvertBIG5: Integer read Get_ConvertBIG5 write Set_ConvertBIG5;
    property BASE64: Integer read Get_BASE64 write Set_BASE64;
    property PIN2: LongWord read Get_PIN2 write Set_PIN2;
    property AccGroup: Integer read Get_AccGroup write Set_AccGroup;
    property AccTimeZones[Index: Integer]: Integer read Get_AccTimeZones write Set_AccTimeZones;
    property CardNumber[Index: Integer]: Integer read Get_CardNumber write Set_CardNumber;
    property PINWidth: Integer read Get_PINWidth;
    property MachineNumber: Integer read Get_MachineNumber write Set_MachineNumber;
    property STR_CardNumber[Index: Integer]: WideString read Get_STR_CardNumber write Set_STR_CardNumber;
    property SSRPin: Integer read Get_SSRPin;
    property PullMode: Integer read Get_PullMode write Set_PullMode;
  end;

// *********************************************************************//
// DispIntf:  IZKEMDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {102F4206-E43D-4FC9-BAB0-331CFFE4D25B}
// *********************************************************************//
  IZKEMDisp = dispinterface
    ['{102F4206-E43D-4FC9-BAB0-331CFFE4D25B}']
    property ReadMark: WordBool dispid 1;
    property CommPort: Integer dispid 2;
    function ClearAdministrators(dwMachineNumber: Integer): WordBool; dispid 3;
    function DeleteEnrollData(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                              dwEMachineNumber: Integer; dwBackupNumber: Integer): WordBool; dispid 4;
    function ReadSuperLogData(dwMachineNumber: Integer): WordBool; dispid 5;
    function ReadAllSLogData(dwMachineNumber: Integer): WordBool; dispid 6;
    function ReadGeneralLogData(dwMachineNumber: Integer): WordBool; dispid 7;
    function ReadAllGLogData(dwMachineNumber: Integer): WordBool; dispid 8;
    function EnableUser(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                        dwEMachineNumber: Integer; dwBackupNumber: Integer; bFlag: WordBool): WordBool; dispid 9;
    function EnableDevice(dwMachineNumber: Integer; bFlag: WordBool): WordBool; dispid 10;
    function GetDeviceStatus(dwMachineNumber: Integer; dwStatus: Integer; var dwValue: Integer): WordBool; dispid 11;
    function GetDeviceInfo(dwMachineNumber: Integer; dwInfo: Integer; var dwValue: Integer): WordBool; dispid 12;
    function SetDeviceInfo(dwMachineNumber: Integer; dwInfo: Integer; dwValue: Integer): WordBool; dispid 13;
    function SetDeviceTime(dwMachineNumber: Integer): WordBool; dispid 14;
    procedure PowerOnAllDevice; dispid 15;
    function PowerOffDevice(dwMachineNumber: Integer): WordBool; dispid 16;
    function ModifyPrivilege(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                             dwEMachineNumber: Integer; dwBackupNumber: Integer; 
                             dwMachinePrivilege: Integer): WordBool; dispid 17;
    procedure GetLastError(var dwErrorCode: Integer); dispid 18;
    function GetEnrollData(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                           dwEMachineNumber: Integer; dwBackupNumber: Integer; 
                           var dwMachinePrivilege: Integer; var dwEnrollData: Integer; 
                           var dwPassWord: Integer): WordBool; dispid 19;
    function SetEnrollData(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                           dwEMachineNumber: Integer; dwBackupNumber: Integer; 
                           dwMachinePrivilege: Integer; var dwEnrollData: Integer; 
                           dwPassWord: Integer): WordBool; dispid 20;
    function GetDeviceTime(dwMachineNumber: Integer; var dwYear: Integer; var dwMonth: Integer; 
                           var dwDay: Integer; var dwHour: Integer; var dwMinute: Integer; 
                           var dwSecond: Integer): WordBool; dispid 21;
    function GetGeneralLogData(dwMachineNumber: Integer; var dwTMachineNumber: Integer; 
                               var dwEnrollNumber: Integer; var dwEMachineNumber: Integer; 
                               var dwVerifyMode: Integer; var dwInOutMode: Integer; 
                               var dwYear: Integer; var dwMonth: Integer; var dwDay: Integer; 
                               var dwHour: Integer; var dwMinute: Integer): WordBool; dispid 22;
    function GetSuperLogData(dwMachineNumber: Integer; var dwTMachineNumber: Integer; 
                             var dwSEnrollNumber: Integer; var Params4: Integer; 
                             var Params1: Integer; var Params2: Integer; 
                             var dwManipulation: Integer; var Params3: Integer; 
                             var dwYear: Integer; var dwMonth: Integer; var dwDay: Integer; 
                             var dwHour: Integer; var dwMinute: Integer): WordBool; dispid 23;
    function GetAllSLogData(dwMachineNumber: Integer; var dwTMachineNumber: Integer; 
                            var dwSEnrollNumber: Integer; var Params4: Integer; 
                            var Params1: Integer; var Params2: Integer; 
                            var dwManipulation: Integer; var Params3: Integer; var dwYear: Integer; 
                            var dwMonth: Integer; var dwDay: Integer; var dwHour: Integer; 
                            var dwMinute: Integer): WordBool; dispid 24;
    function GetAllGLogData(dwMachineNumber: Integer; var dwTMachineNumber: Integer; 
                            var dwEnrollNumber: Integer; var dwEMachineNumber: Integer; 
                            var dwVerifyMode: Integer; var dwInOutMode: Integer; 
                            var dwYear: Integer; var dwMonth: Integer; var dwDay: Integer; 
                            var dwHour: Integer; var dwMinute: Integer): WordBool; dispid 25;
    procedure ConvertPassword(dwSrcPSW: Integer; var dwDestPSW: Integer; dwLength: Integer); dispid 26;
    function ReadAllUserID(dwMachineNumber: Integer): WordBool; dispid 27;
    function GetAllUserID(dwMachineNumber: Integer; var dwEnrollNumber: Integer; 
                          var dwEMachineNumber: Integer; var dwBackupNumber: Integer; 
                          var dwMachinePrivilege: Integer; var dwEnable: Integer): WordBool; dispid 28;
    function GetSerialNumber(dwMachineNumber: Integer; out dwSerialNumber: WideString): WordBool; dispid 29;
    function ClearKeeperData(dwMachineNumber: Integer): WordBool; dispid 30;
    function GetBackupNumber(dwMachineNumber: Integer): Integer; dispid 32;
    function GetProductCode(dwMachineNumber: Integer; out lpszProductCode: WideString): WordBool; dispid 33;
    function GetFirmwareVersion(dwMachineNumber: Integer; var strVersion: WideString): WordBool; dispid 34;
    function GetSDKVersion(var strVersion: WideString): WordBool; dispid 35;
    function ClearGLog(dwMachineNumber: Integer): WordBool; dispid 36;
    function GetFPTempLength(var dwEnrollData: Byte): Integer; dispid 37;
    function Connect_Com(ComPort: Integer; MachineNumber: Integer; BaudRate: Integer): WordBool; dispid 38;
    function Connect_Net(const IPAdd: WideString; Port: Integer): WordBool; dispid 39;
    procedure Disconnect; dispid 40;
    function SetUserInfo(dwMachineNumber: Integer; dwEnrollNumber: Integer; const Name: WideString; 
                         const Password: WideString; Privilege: Integer; Enabled: WordBool): WordBool; dispid 41;
    function GetUserInfo(dwMachineNumber: Integer; dwEnrollNumber: Integer; var Name: WideString; 
                         var Password: WideString; var Privilege: Integer; var Enabled: WordBool): WordBool; dispid 42;
    function SetDeviceIP(dwMachineNumber: Integer; const IPAddr: WideString): WordBool; dispid 43;
    function GetDeviceIP(dwMachineNumber: Integer; var IPAddr: WideString): WordBool; dispid 44;
    function GetUserTmp(dwMachineNumber: Integer; dwEnrollNumber: Integer; dwFingerIndex: Integer; 
                        var TmpData: Byte; var TmpLength: Integer): WordBool; dispid 45;
    function SetUserTmp(dwMachineNumber: Integer; dwEnrollNumber: Integer; dwFingerIndex: Integer; 
                        var TmpData: Byte): WordBool; dispid 46;
    function GetAllUserInfo(dwMachineNumber: Integer; var dwEnrollNumber: Integer; 
                            var Name: WideString; var Password: WideString; var Privilege: Integer; 
                            var Enabled: WordBool): WordBool; dispid 47;
    function DelUserTmp(dwMachineNumber: Integer; dwEnrollNumber: Integer; dwFingerIndex: Integer): WordBool; dispid 48;
    function RefreshData(dwMachineNumber: Integer): WordBool; dispid 49;
    function FPTempConvert(var TmpData1: Byte; var TmpData2: Byte; var Size: Integer): WordBool; dispid 50;
    function SetCommPassword(CommKey: Integer): WordBool; dispid 51;
    function GetUserGroup(dwMachineNumber: Integer; dwEnrollNumber: Integer; var UserGrp: Integer): WordBool; dispid 52;
    function SetUserGroup(dwMachineNumber: Integer; dwEnrollNumber: Integer; UserGrp: Integer): WordBool; dispid 53;
    function GetTZInfo(dwMachineNumber: Integer; TZIndex: Integer; var TZ: WideString): WordBool; dispid 54;
    function SetTZInfo(dwMachineNumber: Integer; TZIndex: Integer; const TZ: WideString): WordBool; dispid 55;
    function GetUnlockGroups(dwMachineNumber: Integer; var Grps: WideString): WordBool; dispid 56;
    function SetUnlockGroups(dwMachineNumber: Integer; const Grps: WideString): WordBool; dispid 57;
    function GetGroupTZs(dwMachineNumber: Integer; GroupIndex: Integer; var TZs: Integer): WordBool; dispid 58;
    function SetGroupTZs(dwMachineNumber: Integer; GroupIndex: Integer; var TZs: Integer): WordBool; dispid 59;
    function GetUserTZs(dwMachineNumber: Integer; dwEnrollNumber: Integer; var TZs: Integer): WordBool; dispid 60;
    function SetUserTZs(dwMachineNumber: Integer; dwEnrollNumber: Integer; var TZs: Integer): WordBool; dispid 61;
    function ACUnlock(dwMachineNumber: Integer; Delay: Integer): WordBool; dispid 62;
    function GetACFun(var ACFun: Integer): WordBool; dispid 63;
    property ConvertBIG5: Integer dispid 64;
    function GetGeneralLogDataStr(dwMachineNumber: Integer; var dwEnrollNumber: Integer; 
                                  var dwVerifyMode: Integer; var dwInOutMode: Integer; 
                                  var TimeStr: WideString): WordBool; dispid 65;
    function GetUserTmpStr(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                           dwFingerIndex: Integer; var TmpData: WideString; var TmpLength: Integer): WordBool; dispid 66;
    function SetUserTmpStr(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                           dwFingerIndex: Integer; const TmpData: WideString): WordBool; dispid 67;
    function GetEnrollDataStr(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                              dwEMachineNumber: Integer; dwBackupNumber: Integer; 
                              var dwMachinePrivilege: Integer; var dwEnrollData: WideString; 
                              var dwPassWord: Integer): WordBool; dispid 68;
    function SetEnrollDataStr(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                              dwEMachineNumber: Integer; dwBackupNumber: Integer; 
                              dwMachinePrivilege: Integer; const dwEnrollData: WideString; 
                              dwPassWord: Integer): WordBool; dispid 69;
    function GetGroupTZStr(dwMachineNumber: Integer; GroupIndex: Integer; var TZs: WideString): WordBool; dispid 70;
    function SetGroupTZStr(dwMachineNumber: Integer; GroupIndex: Integer; const TZs: WideString): WordBool; dispid 71;
    function GetUserTZStr(dwMachineNumber: Integer; dwEnrollNumber: Integer; var TZs: WideString): WordBool; dispid 72;
    function SetUserTZStr(dwMachineNumber: Integer; dwEnrollNumber: Integer; const TZs: WideString): WordBool; dispid 73;
    function FPTempConvertStr(const TmpData1: WideString; var TmpData2: WideString; 
                              var Size: Integer): WordBool; dispid 74;
    function GetFPTempLengthStr(const dwEnrollData: WideString): Integer; dispid 75;
    property BASE64: Integer dispid 76;
    property PIN2: LongWord dispid 78;
    property AccGroup: Integer dispid 79;
    property AccTimeZones[Index: Integer]: Integer dispid 80;
    function GetUserInfoByPIN2(dwMachineNumber: Integer; var Name: WideString; 
                               var Password: WideString; var Privilege: Integer; 
                               var Enabled: WordBool): WordBool; dispid 81;
    function GetUserInfoByCard(dwMachineNumber: Integer; var Name: WideString; 
                               var Password: WideString; var Privilege: Integer; 
                               var Enabled: WordBool): WordBool; dispid 82;
    property CardNumber[Index: Integer]: Integer dispid 83;
    function CaptureImage(FullImage: WordBool; var Width: Integer; var Height: Integer; 
                          var Image: Byte; const ImageFile: WideString): WordBool; dispid 86;
    function UpdateFirmware(const FirmwareFile: WideString): WordBool; dispid 87;
    function StartEnroll(UserID: Integer; FingerID: Integer): WordBool; dispid 88;
    function StartVerify(UserID: Integer; FingerID: Integer): WordBool; dispid 89;
    function StartIdentify: WordBool; dispid 90;
    function CancelOperation: WordBool; dispid 91;
    function QueryState(var State: Integer): WordBool; dispid 92;
    function BackupData(const DataFile: WideString): WordBool; dispid 93;
    function RestoreData(const DataFile: WideString): WordBool; dispid 94;
    function WriteLCD(Row: Integer; Col: Integer; const Text: WideString): WordBool; dispid 95;
    function ClearLCD: WordBool; dispid 96;
    function Beep(DelayMS: Integer): WordBool; dispid 97;
    function PlayVoice(Position: Integer; Length: Integer): WordBool; dispid 98;
    function PlayVoiceByIndex(Index: Integer): WordBool; dispid 99;
    function EnableClock(Enabled: Integer): WordBool; dispid 100;
    function GetUserIDByPIN2(PIN2: Integer; var UserID: Integer): WordBool; dispid 101;
    property PINWidth: Integer readonly dispid 102;
    function GetPIN2(UserID: Integer; var PIN2: Integer): WordBool; dispid 103;
    function FPTempConvertNew(var TmpData1: Byte; var TmpData2: Byte; var Size: Integer): WordBool; dispid 104;
    function FPTempConvertNewStr(const TmpData1: WideString; var TmpData2: WideString; 
                                 var Size: Integer): WordBool; dispid 105;
    function ReadAllTemplate(dwMachineNumber: Integer): WordBool; dispid 106;
    function DisableDeviceWithTimeOut(dwMachineNumber: Integer; TimeOutSec: Integer): WordBool; dispid 107;
    function SetDeviceTime2(dwMachineNumber: Integer; dwYear: Integer; dwMonth: Integer; 
                            dwDay: Integer; dwHour: Integer; dwMinute: Integer; dwSecond: Integer): WordBool; dispid 108;
    function ClearSLog(dwMachineNumber: Integer): WordBool; dispid 109;
    function RestartDevice(dwMachineNumber: Integer): WordBool; dispid 110;
    function GetDeviceMAC(dwMachineNumber: Integer; var sMAC: WideString): WordBool; dispid 111;
    function SetDeviceMAC(dwMachineNumber: Integer; const sMAC: WideString): WordBool; dispid 112;
    function GetWiegandFmt(dwMachineNumber: Integer; var sWiegandFmt: WideString): WordBool; dispid 113;
    function SetWiegandFmt(dwMachineNumber: Integer; const sWiegandFmt: WideString): WordBool; dispid 114;
    function ClearSMS(dwMachineNumber: Integer): WordBool; dispid 115;
    function GetSMS(dwMachineNumber: Integer; ID: Integer; var Tag: Integer; 
                    var ValidMinutes: Integer; var StartTime: WideString; var Content: WideString): WordBool; dispid 116;
    function SetSMS(dwMachineNumber: Integer; ID: Integer; Tag: Integer; ValidMinutes: Integer; 
                    const StartTime: WideString; const Content: WideString): WordBool; dispid 117;
    function DeleteSMS(dwMachineNumber: Integer; ID: Integer): WordBool; dispid 118;
    function SetUserSMS(dwMachineNumber: Integer; dwEnrollNumber: Integer; SMSID: Integer): WordBool; dispid 119;
    function DeleteUserSMS(dwMachineNumber: Integer; dwEnrollNumber: Integer; SMSID: Integer): WordBool; dispid 120;
    function GetCardFun(dwMachineNumber: Integer; var CardFun: Integer): WordBool; dispid 121;
    function ClearUserSMS(dwMachineNumber: Integer): WordBool; dispid 122;
    property MachineNumber: Integer dispid 123;
    function SetDeviceCommPwd(dwMachineNumber: Integer; CommKey: Integer): WordBool; dispid 124;
    function GetDoorState(MachineNumber: Integer; var State: Integer): WordBool; dispid 128;
    function GetVendor(var strVendor: WideString): WordBool; dispid 129;
    function GetSensorSN(dwMachineNumber: Integer; var SensorSN: WideString): WordBool; dispid 130;
    function ReadCustData(dwMachineNumber: Integer; var CustData: WideString): WordBool; dispid 131;
    function WriteCustData(dwMachineNumber: Integer; const CustData: WideString): WordBool; dispid 132;
    function BeginBatchUpdate(dwMachineNumber: Integer; UpdateFlag: Integer): WordBool; dispid 133;
    function BatchUpdate(dwMachineNumber: Integer): WordBool; dispid 134;
    function ClearData(dwMachineNumber: Integer; DataFlag: Integer): WordBool; dispid 135;
    function GetDataFile(dwMachineNumber: Integer; DataFlag: Integer; const FileName: WideString): WordBool; dispid 136;
    function WriteCard(dwMachineNumber: Integer; dwEnrollNumber: Integer; dwFingerIndex1: Integer; 
                       var TmpData1: Byte; dwFingerIndex2: Integer; var TmpData2: Byte; 
                       dwFingerIndex3: Integer; var TmpData3: Byte; dwFingerIndex4: Integer; 
                       var TmpData4: Byte): WordBool; dispid 137;
    function GetGeneralExtLogData(dwMachineNumber: Integer; var dwEnrollNumber: Integer; 
                                  var dwVerifyMode: Integer; var dwInOutMode: Integer; 
                                  var dwYear: Integer; var dwMonth: Integer; var dwDay: Integer; 
                                  var dwHour: Integer; var dwMinute: Integer; 
                                  var dwSecond: Integer; var dwWorkCode: Integer; 
                                  var dwReserved: Integer): WordBool; dispid 138;
    function EmptyCard(dwMachineNumber: Integer): WordBool; dispid 139;
    function GetDeviceStrInfo(dwMachineNumber: Integer; dwInfo: Integer; out Value: WideString): WordBool; dispid 140;
    function GetSysOption(dwMachineNumber: Integer; const Option: WideString; out Value: WideString): WordBool; dispid 141;
    function SetUserInfoEx(dwMachineNumber: Integer; dwEnrollNumber: Integer; VerifyStyle: Integer; 
                           var Reserved: Byte): WordBool; dispid 142;
    function GetUserInfoEx(dwMachineNumber: Integer; dwEnrollNumber: Integer; 
                           out VerifyStyle: Integer; out Reserved: Byte): WordBool; dispid 143;
    function DeleteUserInfoEx(dwMachineNumber: Integer; dwEnrollNumber: Integer): WordBool; dispid 144;
    function SSR_GetGeneralLogData(dwMachineNumber: Integer; out dwEnrollNumber: WideString; 
                                   out dwVerifyMode: Integer; out dwInOutMode: Integer; 
                                   out dwYear: Integer; out dwMonth: Integer; out dwDay: Integer; 
                                   out dwHour: Integer; out dwMinute: Integer; 
                                   out dwSecond: Integer; var dwWorkCode: Integer): WordBool; dispid 145;
    function SSR_GetAllUserInfo(dwMachineNumber: Integer; out dwEnrollNumber: WideString; 
                                out Name: WideString; out Password: WideString; 
                                out Privilege: Integer; out Enabled: WordBool): WordBool; dispid 146;
    function SSR_GetUserInfo(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                             out Name: WideString; out Password: WideString; 
                             out Privilege: Integer; out Enabled: WordBool): WordBool; dispid 147;
    function SSR_GetUserTmp(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            dwFingerIndex: Integer; out TmpData: Byte; out TmpLength: Integer): WordBool; dispid 148;
    function SSR_GetUserTmpStr(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                               dwFingerIndex: Integer; out TmpData: WideString; 
                               out TmpLength: Integer): WordBool; dispid 149;
    function SSR_DeleteEnrollData(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                                  dwBackupNumber: Integer): WordBool; dispid 150;
    function SSR_SetUserInfo(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                             const Name: WideString; const Password: WideString; 
                             Privilege: Integer; Enabled: WordBool): WordBool; dispid 151;
    function SSR_SetUserTmp(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            dwFingerIndex: Integer; var TmpData: Byte): WordBool; dispid 152;
    function SSR_SetUserTmpStr(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                               dwFingerIndex: Integer; const TmpData: WideString): WordBool; dispid 153;
    function SSR_DelUserTmp(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            dwFingerIndex: Integer): WordBool; dispid 154;
    property STR_CardNumber[Index: Integer]: WideString dispid 155;
    function SetWorkCode(WorkCodeID: Integer; AWorkCode: Integer): WordBool; dispid 156;
    function GetWorkCode(WorkCodeID: Integer; out AWorkCode: Integer): WordBool; dispid 157;
    function DeleteWorkCode(WorkCodeID: Integer): WordBool; dispid 158;
    function ClearWorkCode: WordBool; dispid 159;
    function ReadAttRule(dwMachineNumber: Integer): WordBool; dispid 160;
    function ReadDPTInfo(dwMachineNumber: Integer): WordBool; dispid 161;
    function SaveTheDataToFile(dwMachineNumber: Integer; const TheFilePath: WideString; 
                               FileFlag: Integer): WordBool; dispid 162;
    function ReadTurnInfo(dwMachineNumber: Integer): WordBool; dispid 163;
    function SSR_OutPutHTMLRep(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                               const AttFile: WideString; const UserFile: WideString; 
                               const DeptFile: WideString; const TimeClassFile: WideString; 
                               const AttruleFile: WideString; BYear: Integer; BMonth: Integer; 
                               BDay: Integer; BHour: Integer; BMinute: Integer; BSecond: Integer; 
                               EYear: Integer; EMonth: Integer; EDay: Integer; EHour: Integer; 
                               EMinute: Integer; ESecond: Integer; const TempPath: WideString; 
                               const OutFileName: WideString; HTMLFlag: Integer; resv1: Integer; 
                               const resv2: WideString): WordBool; dispid 164;
    function ReadAOptions(const AOption: WideString; out AValue: WideString): WordBool; dispid 165;
    function ReadRTLog(dwMachineNumber: Integer): WordBool; dispid 166;
    function GetRTLog(dwMachineNumber: Integer): WordBool; dispid 167;
    function GetHIDEventCardNumAsStr(out strHIDEventCardNum: WideString): WordBool; dispid 168;
    function GetStrCardNumber(out ACardNumber: WideString): WordBool; dispid 169;
    function SetStrCardNumber(const ACardNumber: WideString): WordBool; dispid 170;
    function RegEvent(dwMachineNumber: Integer; EventMask: Integer): WordBool; dispid 171;
    function CancelBatchUpdate(dwMachineNumber: Integer): WordBool; dispid 172;
    function SetSysOption(dwMachineNumber: Integer; const Option: WideString; 
                          const Value: WideString): WordBool; dispid 173;
    function Connect_Modem(ComPort: Integer; MachineNumber: Integer; BaudRate: Integer; 
                           const Telephone: WideString): WordBool; dispid 174;
    function UseGroupTimeZone: WordBool; dispid 175;
    function SetHoliday(dwMachineNumber: Integer; const Holiday: WideString): WordBool; dispid 176;
    function GetHoliday(dwMachineNumber: Integer; var Holiday: WideString): WordBool; dispid 177;
    function SetDaylight(dwMachineNumber: Integer; Support: Integer; const BeginTime: WideString; 
                         const EndTime: WideString): WordBool; dispid 178;
    function GetDaylight(dwMachineNumber: Integer; var Support: Integer; var BeginTime: WideString; 
                         var EndTime: WideString): WordBool; dispid 179;
    function SSR_SetUnLockGroup(dwMachineNumber: Integer; CombNo: Integer; Group1: Integer; 
                                Group2: Integer; Group3: Integer; Group4: Integer; Group5: Integer): WordBool; dispid 180;
    function SSR_GetUnLockGroup(dwMachineNumber: Integer; CombNo: Integer; var Group1: Integer; 
                                var Group2: Integer; var Group3: Integer; var Group4: Integer; 
                                var Group5: Integer): WordBool; dispid 181;
    function SSR_SetGroupTZ(dwMachineNumber: Integer; GroupNo: Integer; Tz1: Integer; Tz2: Integer; 
                            Tz3: Integer; VaildHoliday: Integer; VerifyStyle: Integer): WordBool; dispid 182;
    function SSR_GetGroupTZ(dwMachineNumber: Integer; GroupNo: Integer; var Tz1: Integer; 
                            var Tz2: Integer; var Tz3: Integer; var VaildHoliday: Integer; 
                            var VerifyStyle: Integer): WordBool; dispid 183;
    function SSR_GetHoliday(dwMachineNumber: Integer; HolidayID: Integer; var BeginMonth: Integer; 
                            var BeginDay: Integer; var EndMonth: Integer; var EndDay: Integer; 
                            var TimeZoneID: Integer): WordBool; dispid 184;
    function SSR_SetHoliday(dwMachineNumber: Integer; HolidayID: Integer; BeginMonth: Integer; 
                            BeginDay: Integer; EndMonth: Integer; EndDay: Integer; 
                            TimeZoneID: Integer): WordBool; dispid 185;
    function GetPlatform(dwMachineNumber: Integer; var Platform: WideString): WordBool; dispid 186;
    function SSR_SetUserSMS(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            SMSID: Integer): WordBool; dispid 187;
    function SSR_DeleteUserSMS(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                               SMSID: Integer): WordBool; dispid 188;
    function IsTFTMachine(dwMachineNumber: Integer): WordBool; dispid 189;
    function SSR_EnableUser(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            bFlag: WordBool): WordBool; dispid 190;
    property SSRPin: Integer readonly dispid 191;
    function SendCMDMsg(dwMachineNumber: Integer; Param1: Integer; Param2: Integer): WordBool; dispid 192;
    function SendFile(dwMachineNumber: Integer; const FileName: WideString): WordBool; dispid 193;
    function SetLanguageByID(dwMachineNumber: Integer; LanguageID: Integer; 
                             const Language: WideString): WordBool; dispid 194;
    function ReadFile(dwMachineNumber: Integer; const FileName: WideString; 
                      const FilePath: WideString): WordBool; dispid 195;
    function SetLastCount(count: Integer): WordBool; dispid 196;
    function SetCustomizeAttState(dwMachineNumber: Integer; StateID: Integer; NewState: Integer): WordBool; dispid 197;
    function DelCustomizeAttState(dwMachineNumber: Integer; StateID: Integer): WordBool; dispid 198;
    function EnableCustomizeAttState(dwMachineNumber: Integer; StateID: Integer; Enable: Integer): WordBool; dispid 199;
    function SetCustomizeVoice(dwMachineNumber: Integer; VoiceID: Integer; 
                               const FileName: WideString): WordBool; dispid 200;
    function DelCustomizeVoice(dwMachineNumber: Integer; VoiceID: Integer): WordBool; dispid 201;
    function EnableCustomizeVoice(dwMachineNumber: Integer; VoiceID: Integer; Enable: Integer): WordBool; dispid 202;
    function StartEnrollEx(const UserID: WideString; FingerID: Integer; Flag: Integer): WordBool; dispid 203;
    function SSR_SetUserTmpExt(dwMachineNumber: Integer; IsDeleted: Integer; 
                               const dwEnrollNumber: WideString; dwFingerIndex: Integer; 
                               var TmpData: Byte): WordBool; dispid 215;
    function SSR_DelUserTmpExt(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                               dwFingerIndex: Integer): WordBool; dispid 216;
    function SSR_DeleteEnrollDataExt(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                                     dwBackupNumber: Integer): WordBool; dispid 217;
    function SSR_GetWorkCode(AWorkCode: Integer; out Name: WideString): WordBool; dispid 218;
    function SSR_SetWorkCode(AWorkCode: Integer; const Name: WideString): WordBool; dispid 219;
    function SSR_DeleteWorkCode(PIN: Integer): WordBool; dispid 220;
    function SSR_ClearWorkCode: WordBool; dispid 221;
    function SSR_GetSuperLogData(MachineNumber: Integer; out Number: Integer; 
                                 out Admin: WideString; out User: WideString; 
                                 out Manipulation: Integer; out Time: WideString; 
                                 out Params1: Integer; out Params2: Integer; out Params3: Integer): WordBool; dispid 222;
    function SSR_SetShortkey(ShortKeyID: Integer; ShortKeyFun: Integer; StateCode: Integer; 
                             const StateName: WideString; StateAutoChange: Integer; 
                             const StateAutoChangeTime: WideString): WordBool; dispid 223;
    function SSR_GetShortkey(ShortKeyID: Integer; var ShortKeyFun: Integer; var StateCode: Integer; 
                             var StateName: WideString; var AutoChange: Integer; 
                             var AutoChangeTime: WideString): WordBool; dispid 224;
    function Connect_USB(MachineNumber: Integer): WordBool; dispid 225;
    function GetSuperLogData2(dwMachineNumber: Integer; var dwTMachineNumber: Integer; 
                              var dwSEnrollNumber: Integer; var Params4: Integer; 
                              var Params1: Integer; var Params2: Integer; 
                              var dwManipulation: Integer; var Params3: Integer; 
                              var dwYear: Integer; var dwMonth: Integer; var dwDay: Integer; 
                              var dwHour: Integer; var dwMinute: Integer; var dwSecs: Integer): WordBool; dispid 226;
    function GetUserFace(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                         dwFaceIndex: Integer; var TmpData: Byte; var TmpLength: Integer): WordBool; dispid 230;
    function SetUserFace(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                         dwFaceIndex: Integer; var TmpData: Byte; TmpLength: Integer): WordBool; dispid 231;
    function DelUserFace(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                         dwFaceIndex: Integer): WordBool; dispid 232;
    function GetUserFaceStr(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            dwFaceIndex: Integer; var TmpData: WideString; var TmpLength: Integer): WordBool; dispid 233;
    function SetUserFaceStr(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                            dwFaceIndex: Integer; const TmpData: WideString; TmpLength: Integer): WordBool; dispid 234;
    function GetUserTmpEx(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                          dwFingerIndex: Integer; out Flag: Integer; out TmpData: Byte; 
                          out TmpLength: Integer): WordBool; dispid 235;
    function GetUserTmpExStr(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                             dwFingerIndex: Integer; out Flag: Integer; out TmpData: WideString; 
                             out TmpLength: Integer): WordBool; dispid 236;
    function SetUserTmpEx(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                          dwFingerIndex: Integer; Flag: Integer; var TmpData: Byte): WordBool; dispid 237;
    function SetUserTmpExStr(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                             dwFingerIndex: Integer; Flag: Integer; const TmpData: WideString): WordBool; dispid 238;
    function MergeTemplate(var Templates: {??PByte1}OleVariant; FingerCount: Integer; 
                           var TemplateDest: Byte; var FingerSize: Integer): WordBool; dispid 239;
    function SplitTemplate(var Template: Byte; var Templates: {??PByte1}OleVariant; 
                           var FingerCount: Integer; var FingerSize: Integer): WordBool; dispid 240;
    property PullMode: Integer dispid 241;
    function ReadUserAllTemplate(dwMachineNumber: Integer; const dwEnrollNumber: WideString): WordBool; dispid 242;
    function UpdateFile(const FileName: WideString): WordBool; dispid 243;
    function ReadLastestLogData(dwMachineNumber: Integer; NewLog: Integer; dwYear: Integer; 
                                dwMonth: Integer; dwDay: Integer; dwHour: Integer; 
                                dwMinute: Integer; dwSecond: Integer): WordBool; dispid 244;
    function SetOptionCommPwd(dwMachineNumber: Integer; const CommKey: WideString): WordBool; dispid 245;
    function ReadSuperLogDataEx(dwMachineNumber: Integer; dwYear_S: Integer; dwMonth_S: Integer; 
                                dwDay_S: Integer; dwHour_S: Integer; dwMinute_S: Integer; 
                                dwSecond_S: Integer; dwYear_E: Integer; dwMonth_E: Integer; 
                                dwDay_E: Integer; dwHour_E: Integer; dwMinute_E: Integer; 
                                dwSecond_E: Integer; dwLogIndex: Integer): WordBool; dispid 246;
    function GetSuperLogDataEx(dwMachineNumber: Integer; var EnrollNumber: WideString; 
                               var Params4: Integer; var Params1: Integer; var Params2: Integer; 
                               var dwManipulation: Integer; var Params3: Integer; 
                               var dwYear: Integer; var dwMonth: Integer; var dwDay: Integer; 
                               var dwHour: Integer; var dwMinute: Integer; var dwSecond: Integer): WordBool; dispid 247;
    function GetBZ400FirmwareVersion(dwMachineNumber: Integer; var strVersion: WideString): WordBool; dispid 248;
    function UnlockREP(dwMachineNumber: Integer): WordBool; dispid 249;
    function RefreshOptions(dwMachineNumber: Integer): WordBool; dispid 250;
    function GetMRPTotal(dwMachineNumber: Integer; MRPType: Integer; var Value: Integer): WordBool; dispid 251;
    function GetUnlockPwd(const LockSerialNumber: WideString; var UnlockPwd: WideString): WordBool; dispid 252;
    function SetSeekPosition(dwMachineNumber: Integer; Position: Integer): WordBool; dispid 253;
    function GetDatetimeOpLog(dwMachineNumber: Integer; out nsr: WideString; 
                              out OldDatetime: WideString; out NewDatetime: WideString): WordBool; dispid 254;
    function GetEmployerOpLog(dwMachineNumber: Integer; out nsr: WideString; out Year: Integer; 
                              out Month: Integer; out Day: Integer; out Hour: Integer; 
                              out min: Integer; out sec: Integer; out cnpj_cpf: WideString; 
                              out identtype: Integer; out cei: WideString; out Name: WideString; 
                              out address: WideString; out OpType: WideString): WordBool; dispid 255;
    function GetEmployeeOpLog(dwMachineNumber: Integer; out nsr: WideString; out Year: Integer; 
                              out Month: Integer; out Day: Integer; out Hour: Integer; 
                              out min: Integer; out sec: Integer; out OpType: WideString; 
                              out pis: WideString; out Name: WideString): WordBool; dispid 256;
    function GetEmployer(dwMachineNumber: Integer; out Name: WideString; out CertType: WideString; 
                         out Tax: WideString; out cei: WideString; out Addr: WideString): WordBool; dispid 257;
    function SetEmployer(dwMachineNumber: Integer; const Name: WideString; CertType: Integer; 
                         const Tax: WideString; const cei: WideString; const Addr: WideString): WordBool; dispid 258;
    function GetAttLogs(dwMachineNumber: Integer; out nsr: WideString; out pis: WideString; 
                        out dwYear: Integer; out dwMonth: Integer; out dwDay: Integer; 
                        out dwHour: Integer; out dwMinute: Integer; out dwSecond: Integer): WordBool; dispid 259;
    function SSR_GetAllUserInfoEx(dwMachineNumber: Integer; out dwEnrollNumber: WideString; 
                                  out Name: WideString; out Password: WideString; 
                                  out Privilege: Integer; out Enabled: WordBool; 
                                  out pis: WideString; out CPF: WideString): WordBool; dispid 260;
    function SSR_SetUserInfoEx(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                               const Name: WideString; const Password: WideString; 
                               Privilege: Integer; Enabled: WordBool; const pis: WideString; 
                               const CPF: WideString): WordBool; dispid 261;
    function GetUserTmpEx_BZ400(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                                dwFingerIndex: Integer; out Flag: Integer; out TmpData: Byte; 
                                out TmpLength: Integer): WordBool; dispid 262;
    function GetUserTmpExStr_BZ400(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                                   dwFingerIndex: Integer; out Flag: Integer; 
                                   out TmpData: WideString; out TmpLength: Integer): WordBool; dispid 263;
    function SetUserTmpEx_BZ400(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                                dwFingerIndex: Integer; Flag: Integer; var TmpData: Byte): WordBool; dispid 264;
    function SetUserTmpExStr_BZ400(dwMachineNumber: Integer; const dwEnrollNumber: WideString; 
                                   dwFingerIndex: Integer; Flag: Integer; const TmpData: WideString): WordBool; dispid 265;
    function SSR_DeleteEnrollDataExt_BZ400(dwMachineNumber: Integer; 
                                           const dwEnrollNumber: WideString; dwBackupNumber: Integer): WordBool; dispid 266;
    function GetRealFirmwareVersion(dwMachineNumber: Integer; var strVersion: WideString): WordBool; dispid 267;
  end;

implementation

uses ComObj;

end.
