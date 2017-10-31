unit WatchComm_TLB;

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
// File generated on 19/01/2012 09:43:36 from Type Library described below.

// ************************************************************************  //
// Type Lib: C:\Documents and Settings\Administrator\Desktop\Exemplo_Delphi\WatchComm.tlb (1)
// LIBID: {C1F280E7-DA8F-4653-843F-6DDF48ED797B}
// LCID: 0
// Helpfile: 
// HelpString: 
// DepndLst: 
//   (1) v2.0 stdole, (C:\WINDOWS\system32\stdole2.tlb)
//   (2) v2.0 mscorlib, (c:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\mscorlib.tlb)
//   (3) v1.0 zkemkeeper, (C:\WINDOWS\system32\zkemkeeper.dll)
// Errors:
//   Hint: Member 'GeneralTimeout' of '_WatchComm' changed to 'GeneralTimeout1'
//   Hint: Member 'CollectTimeout' of '_WatchComm' changed to 'CollectTimeout1'
//   Hint: Member 'Protocol' of '_WatchComm' changed to 'Protocol1'
//   Hint: TypeInfo 'WatchComm' changed to 'WatchComm_'
//   Hint: Parameter 'type' of IWatchComm.ProgramTriggerType changed to 'type_'
//   Hint: Parameter 'type' of _AbstractProtocol.ProgramTriggerType changed to 'type_'
//   Hint: Symbol 'type' renamed to 'type_'
//   Hint: Parameter 'type' of _WatchComm.ProgramTriggerType changed to 'type_'
//   Hint: Member 'Function' of '_AbstractPacket' changed to 'Function_'
//   Hint: Parameter 'type' of _AbstractFactory.CreateProtocol changed to 'type_'
//   Error creating palette bitmap of (TTCPComm) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TPrintPointLiFingerPrint) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TPrintPointEmployee) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TWatchComm) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TPrintPointSendSerialNumberMessage) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TPrintPointLiStatus) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TMRPRecord) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TFaceFingerPrint) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TMiniPointStatusMessage) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TNoDataMessage) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TTemplate) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TMiniPointConfigurator) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TUtil) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TMicroPointStatusMessage) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TBioPointCardList) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TShiftTable) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TJourneyWorking) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TPeriodicJourneyWorking) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TMemoryFormat) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TBioPointStatusMessage) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TFaceEmployee) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (THoliday) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TPrintPointLiEmployee) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TParcialConfiguration) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TCardCollection) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TConcretePunchMessage) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TBioPointMemoryFormat) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TTemplateCollection) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TAlarmRings) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TCredential) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TConfiguration) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TFaceStatus) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (THolidayCollection) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TWeeklyJourneyWorking) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TMaster) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TRelogio) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TPrintPointEmployerMessage) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TMicropointCardList) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TShiftTableCollection) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TMonthlyJourneyWorking) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TAlarmRingsCollection) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TCard) : Server mscoree.dll contains no icons
//   Error creating palette bitmap of (TFaceLogRecord) : Server mscoree.dll contains no icons
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

uses Windows, ActiveX, Classes, Graphics, mscorlib_TLB, OleServer, StdVCL, Variants, 
zkemkeeper_TLB;
  

// *********************************************************************//
// GUIDS declared in the TypeLibrary. Following prefixes are used:        
//   Type Libraries     : LIBID_xxxx                                      
//   CoClasses          : CLASS_xxxx                                      
//   DISPInterfaces     : DIID_xxxx                                       
//   Non-DISP interfaces: IID_xxxx                                        
// *********************************************************************//
const
  // TypeLibrary Major and minor versions
  WatchCommMajorVersion = 2;
  WatchCommMinorVersion = 2;

  LIBID_WatchComm: TGUID = '{C1F280E7-DA8F-4653-843F-6DDF48ED797B}';

  IID__AbstractMessage: TGUID = '{8DE1B541-F000-3238-B662-6D475C47FEDC}';
  IID_IComm: TGUID = '{92EEAAD7-2D59-3ADC-A67B-06C2A0C2C1C7}';
  IID__TCPComm: TGUID = '{66C6B10E-8FA5-3B10-B01F-3F7BF9697FF7}';
  IID__AbstractProtocol: TGUID = '{E7B3D491-7351-33F9-AA10-49F8D712B86F}';
  IID__EmployeesListTransmissionProgress: TGUID = '{1EFC1A17-A151-376C-9555-F04B47FF4C17}';
  IID__CredentialListTransmissionProgress: TGUID = '{2C153EBC-1F13-309E-80FE-B44D73F25E8C}';
  IID__PrintPointInquirySerialNumberOfREPAndMemoryResponse: TGUID = '{FF3A73D0-3C44-3133-B01B-47B036BC5F09}';
  IID__PrintPointLiFingerPrint: TGUID = '{A6902D61-90B9-391D-99C8-59CE92C509A5}';
  IID__PrintPointEmployee: TGUID = '{ADFC1D78-5667-3A3A-8CE8-E3FAF826B02B}';
  IID__DeviceConnectionException: TGUID = '{E12820AE-D496-3FB7-BE6E-B648AF65C4B2}';
  IID_IWatchComm: TGUID = '{861940A2-FC90-35BD-ADDA-5F8672AD4A46}';
  IID__WatchComm: TGUID = '{12335FED-346F-3140-B4BB-4651674481ED}';
  IID__CredentialsListTransmissionProgress: TGUID = '{B4E35F36-26C8-374F-B2C5-E481E3B32F2C}';
  IID__PrintPointSendSerialNumberMessage: TGUID = '{3F62753F-3060-3AE8-881D-D2D55E6F65FD}';
  IID__PrintPointLiStatus: TGUID = '{CB3D475A-280A-3F62-BDE2-1341D0F8E15A}';
  IID__MRPRecord: TGUID = '{F0036B67-82AA-36B4-B617-6AD095B54F40}';
  IID__MRPRecord_ChangeEmployee: TGUID = '{998DA974-385F-3B62-A3EE-FD43F385E164}';
  IID__FaceFingerPrint: TGUID = '{C65B932D-291C-3FB9-90A8-11B8312DE61D}';
  IID__AbstractCardList: TGUID = '{8D6BD7AC-8AED-32D6-9A88-B8C5E2900792}';
  IID__AbstractStatusMessage: TGUID = '{46BD67A6-4154-3B24-B932-31B854D8B781}';
  IID__MiniPointStatusMessage: TGUID = '{1E3010DC-B3B4-3EB3-BF7C-19952DED0E4E}';
  IID__NoDataMessage: TGUID = '{80B6B329-5459-3562-92E9-1A134496876A}';
  IID__Template: TGUID = '{C709AC5D-461C-33C6-B135-75B626665DCD}';
  IID__MiniPointConfigurator: TGUID = '{C94B244C-FE69-3B75-BC35-3EFF5C28B364}';
  IID__Util: TGUID = '{5BCFE2B0-7597-36B6-801B-93EC25098916}';
  IID__MicroPointStatusMessage: TGUID = '{EA7AE898-8BDC-3757-B62C-E9D50CA5F4AC}';
  IID__BioPointCardList: TGUID = '{B8B4B8DB-4CDB-3B9B-A8CA-88405FC9A0A7}';
  IID__ShiftTable: TGUID = '{1F79777E-9628-326F-82D0-DB3A67DFF466}';
  IID__JourneyWorking: TGUID = '{22A9FA9D-D34A-38C6-AE62-15ACFB57A842}';
  IID__PeriodicJourneyWorking: TGUID = '{5BE9179F-FF46-311D-AB60-AD28A14668B0}';
  IID__AbstractJourneyWorking: TGUID = '{FD36CE33-4444-36F9-8EB8-D2705F5C4B98}';
  IID__AbstractPunchMessage: TGUID = '{531CA190-5419-31B8-871F-4F9270F2DAB1}';
  IID__MRPRecord_ChangeCompanyIdentification: TGUID = '{845262DE-FFD3-3CAF-BED8-2C6484BC1DFC}';
  IID__AbstractPacket: TGUID = '{5A8483F9-6890-3899-A342-7A9BFEBD2881}';
  IID__MemoryFormat: TGUID = '{CDCA2E43-A823-37CC-8262-ECF3BCC77DA3}';
  IID__PrintPointFingerPrintMessage: TGUID = '{34023D0E-D605-3B1B-9825-EA9F802AF38D}';
  IID__BioPointStatusMessage: TGUID = '{67DE066A-5095-34D2-B29F-F36E0C07311A}';
  IID__FaceEmployee: TGUID = '{8C0B6C5B-7411-3189-AE2E-31DC47B9F801}';
  IID__Holiday: TGUID = '{8AF993AB-1F73-3772-B3B2-B2AB09B689F8}';
  IID__RandomNumberResponse: TGUID = '{DAC90B55-9C21-32E2-B1F7-B459B181F365}';
  IID__PrintPointStatusMessage: TGUID = '{B498CB3E-F458-3EEB-A9F0-20A90D3D6591}';
  IID__PrintPointEvent: TGUID = '{520547F3-7D11-30D7-924C-43A4F2A0E7A2}';
  IID__PrintPointLiEmployee: TGUID = '{F4BC0867-964B-31E6-925B-09F087E712FF}';
  IID__ParcialConfiguration: TGUID = '{8C562630-EDE1-31D7-BADF-93A8D27DCE2A}';
  IID__MRPRecord_SettingRealTimeClock: TGUID = '{F5AA29CC-C17D-3FBC-9ACE-F1B6189F71DF}';
  IID__MRPRecord_RegistrationMarkingPoint: TGUID = '{02878451-8BCD-3664-9BF6-DD884ECCF910}';
  IID__CardCollection: TGUID = '{0F9FDA08-1DA7-36A1-A088-FCB452386331}';
  IID__HardwareTestCollectionResponse: TGUID = '{51718424-DFDA-3613-8CFB-D31417B8D20B}';
  IID__GetMACResponse: TGUID = '{876887A8-15B6-33D9-853C-41BA220DD16B}';
  IID__ConcretePunchMessage: TGUID = '{8A13657B-F830-356D-A578-56EA2BEDF1B7}';
  IID__AbstractMemoryFormat: TGUID = '{78B677F5-EEA9-3462-A6A5-1649A1B3B738}';
  IID__BioPointMemoryFormat: TGUID = '{42019683-A593-37DA-B9A6-7E8A30418B63}';
  IID__TemplateCollection: TGUID = '{6ED2CC35-76F4-3EEA-B441-E2229951E9D3}';
  IID__AlarmRings: TGUID = '{5B8EF58D-140B-3E95-8994-8471170E7FBD}';
  IID__Credential: TGUID = '{F100D95F-1C0D-3AA3-B69D-6FA223062BDF}';
  IID__Configuration: TGUID = '{87EB9D12-8EF8-30DE-8CB7-BE72F7E6E7D4}';
  IID__InvalidMessageException: TGUID = '{43CC521F-6982-359F-B1D4-5AC39F4DBFD7}';
  IID__FaceStatus: TGUID = '{01D965E2-CC3E-3F0E-B9D1-4DD9D8699376}';
  IID__AbstractFactory: TGUID = '{0EB89E59-CBBC-3DF5-AD86-2DC1CEF5AB01}';
  IID__HolidayCollection: TGUID = '{0800B5DB-DA43-39A1-855C-B72D5620FFB2}';
  IID__WeeklyJourneyWorking: TGUID = '{59D43438-1A3B-3374-9E63-6C09423865B1}';
  IID__ImmediateStatusResponse: TGUID = '{80F74BCB-E031-3AD2-B326-C807335B4FBC}';
  IID__PrintPointMRPEventLog: TGUID = '{CBE23118-2F4F-3F6E-B5F2-D8A5AC1CED64}';
  IID__Master: TGUID = '{5F1A4777-C778-330C-AE36-477874E8E1D4}';
  DIID_IRelogio: TGUID = '{7BD20046-DF8C-44A6-8F6B-687FAA26FA71}';
  CLASS_Relogio: TGUID = '{9E5E5FB2-219D-4EE7-AB27-E4DBED8E123E}';
  IID__PrintPointEmployerMessage: TGUID = '{BA9CC9F9-0BE1-3BB5-A34F-C43AE506B0E1}';
  IID__MicropointCardList: TGUID = '{D694DE74-E8C8-3F70-927C-F9F8913ABA7B}';
  IID__ShiftTableCollection: TGUID = '{6D1ABFB6-695D-33E5-A803-0039BEF6AE5D}';
  IID__MonthlyJourneyWorking: TGUID = '{CDBBE17C-1B38-3483-9A45-3DA2FB57EA5E}';
  IID__AlarmRingsCollection: TGUID = '{0F06197D-377B-3293-A017-6E8C39A59C02}';
  IID__SerialComm: TGUID = '{399F624C-B7E2-314F-81F1-C7FA4AC9B229}';
  IID__Card: TGUID = '{B81CD20E-0982-332F-9289-54316DB0B4F0}';
  IID__BioPointFingerPrintMessage: TGUID = '{78DE22E1-EC7C-3544-B176-CD0A746B5722}';
  IID__FaceLogRecord: TGUID = '{294C019E-7BCA-3477-9D0F-26F2C77D1252}';
  IID__EmployeesListTransmissionProgress_2: TGUID = '{F0AD011C-D37B-3EB6-AD9E-D710A3C9A47A}';
  CLASS_AbstractMessage: TGUID = '{A8ABD961-B138-405C-860A-61C336BEDA65}';
  CLASS_TCPComm: TGUID = '{C6659361-1625-4746-931C-36014B146679}';
  CLASS_AbstractProtocol: TGUID = '{639C8761-BD77-4648-9DB4-154C130707F8}';
  CLASS_EmployeesListTransmissionProgress: TGUID = '{1803139C-6C33-37D5-836E-AEBAE879F572}';
  CLASS_CredentialListTransmissionProgress: TGUID = '{D5CA8BF4-BDBA-34BE-B341-EA7A871CF4FB}';
  CLASS_PrintPointInquirySerialNumberOfREPAndMemoryResponse: TGUID = '{8417DA43-D252-4F79-9F2A-21B924D386B1}';
  CLASS_PrintPointLiFingerPrint: TGUID = '{6DDF7E80-D7C4-4B36-8971-B6DAC6C6C507}';
  CLASS_PrintPointEmployee: TGUID = '{B58BFC15-715C-43F2-9549-9E6F80A6B019}';
  CLASS_DeviceConnectionException: TGUID = '{E323D3BF-C146-333F-AF3C-76E4FD54D0C5}';
  CLASS_WatchComm_: TGUID = '{36037306-8B1F-4F82-9868-1976E8EBA4F0}';
  CLASS_EmployeesListTransmissionProgress_2: TGUID = '{32BAC28E-49AC-3067-93D6-0003DAE91C8B}';
  CLASS_CredentialsListTransmissionProgress: TGUID = '{2AC51D24-93A3-349B-8A18-B6F20B0B1A14}';
  CLASS_PrintPointSendSerialNumberMessage: TGUID = '{DDEECBF5-17DC-38DC-B220-B3CA08A3FD5B}';
  CLASS_PrintPointLiStatus: TGUID = '{01475796-3364-4A9C-8FCC-ADD713548B8E}';
  CLASS_MRPRecord: TGUID = '{79A02533-D650-40A1-B7F2-E784083D69F5}';
  CLASS_MRPRecord_ChangeEmployee: TGUID = '{A9E7FE2F-1D4F-4AFD-AC2D-E1958FD3BEF4}';
  CLASS_FaceFingerPrint: TGUID = '{45AFB8A2-CE08-40FC-9E98-6D98B9AEEEBE}';
  CLASS_AbstractCardList: TGUID = '{D971B90E-FE20-4A03-971F-6AC489931947}';
  CLASS_AbstractStatusMessage: TGUID = '{7DEF2C64-9787-4873-9628-714ABBE4CC9D}';
  CLASS_MiniPointStatusMessage: TGUID = '{51AE264B-680F-4284-A7F2-C6AD1DE7FA48}';
  CLASS_NoDataMessage: TGUID = '{64577AD7-656C-40AD-9302-215D5003DDD1}';
  CLASS_Template: TGUID = '{A4C3BA08-D406-4AF2-82B7-4334EC81EFC4}';
  CLASS_MiniPointConfigurator: TGUID = '{FE44C0F4-F6D8-4DDD-9EBF-DA19BCCCE0CA}';
  CLASS_Util: TGUID = '{C8E86A96-3598-46A2-96BF-EF7B30D78D3B}';
  CLASS_MicroPointStatusMessage: TGUID = '{269480B7-B13C-4DB1-8A42-9D0329A56A98}';
  CLASS_BioPointCardList: TGUID = '{87115F94-9FB9-47EB-8D7D-7CC385500817}';
  CLASS_ShiftTable: TGUID = '{AB55F247-D316-4787-B267-ADF4ED26A84E}';
  CLASS_JourneyWorking: TGUID = '{B7BA908B-1AE8-47A6-B490-1FFBF7B63073}';
  CLASS_PeriodicJourneyWorking: TGUID = '{3389910E-6524-3A21-A5CC-B7E16D3593E7}';
  CLASS_AbstractJourneyWorking: TGUID = '{9F699E00-2F74-423A-A065-1751A87D3275}';
  CLASS_AbstractPunchMessage: TGUID = '{55D3F1F1-403F-48B2-9522-DB1B4983C3B8}';
  CLASS_MRPRecord_ChangeCompanyIdentification: TGUID = '{C507F070-2BFD-4CA4-B466-5CD6BEA624BA}';
  CLASS_AbstractPacket: TGUID = '{6E801DE6-465D-4A88-AA28-5B21A7AA45B1}';
  CLASS_MemoryFormat: TGUID = '{DC98228F-4007-4180-8770-1A9AB4E297A2}';
  CLASS_PrintPointFingerPrintMessage: TGUID = '{A1C2D02C-C9AB-4C16-9F3C-2BC30862C8F7}';
  CLASS_BioPointStatusMessage: TGUID = '{0395C187-7E52-49A5-A22C-537504A2E1D2}';
  CLASS_FaceEmployee: TGUID = '{D9065E15-E643-4AAF-BECB-EA307B5EF536}';
  CLASS_Holiday: TGUID = '{9064A618-FC6D-4D50-8C10-AC131CF2B320}';
  CLASS_RandomNumberResponse: TGUID = '{4AA24F2A-7C09-4412-8794-EE901C0B3EA6}';
  CLASS_PrintPointStatusMessage: TGUID = '{016E643A-D99A-4864-80A2-BB07EE8EDD5F}';
  CLASS_PrintPointEvent: TGUID = '{945563DD-F996-4F96-9185-F95A8A60179A}';
  CLASS_PrintPointLiEmployee: TGUID = '{B3EE107D-25E1-4C57-B2ED-A2F277C473B0}';
  CLASS_ParcialConfiguration: TGUID = '{775A6F41-1E79-49D2-84DF-A7C4FA5304B9}';
  CLASS_MRPRecord_SettingRealTimeClock: TGUID = '{80F74117-796D-4D6B-9FF1-CE5B8C6CCF7A}';
  CLASS_MRPRecord_RegistrationMarkingPoint: TGUID = '{70A4AE95-F503-4911-B94F-3B2EED132B1F}';
  CLASS_CardCollection: TGUID = '{D558A317-4775-4633-9281-98EA2AF1744F}';
  CLASS_HardwareTestCollectionResponse: TGUID = '{3F248905-DC47-4C45-9335-7FB2D02FE288}';
  CLASS_GetMACResponse: TGUID = '{4AA24F2A-7C09-4412-8794-EF901A0B3EA6}';
  CLASS_ConcretePunchMessage: TGUID = '{9D1EB6C4-9054-3C71-A6ED-C937E3A31E74}';
  CLASS_AbstractMemoryFormat: TGUID = '{28C0852C-1003-4CF6-8839-2B8D4CF76E70}';
  CLASS_BioPointMemoryFormat: TGUID = '{3503D08C-0DA8-445C-A247-1A1BEC98E424}';
  CLASS_TemplateCollection: TGUID = '{580074E5-6B1C-420A-BB83-0F9D060BFA86}';
  CLASS_AlarmRings: TGUID = '{8C727E63-580D-4D5C-B2DE-A49CF67BEDD7}';
  CLASS_Credential: TGUID = '{CD6C8D17-870B-4BFA-A537-3C859572B16B}';
  CLASS_Configuration: TGUID = '{3706A057-2C50-478A-AA31-66CA1A9EC8B1}';
  CLASS_InvalidMessageException: TGUID = '{D1F90E19-F057-4D13-BDA4-870F4F9E7162}';
  CLASS_FaceStatus: TGUID = '{B5F1FC66-4BA9-4C58-9864-9AC0E1DF677A}';
  CLASS_AbstractFactory: TGUID = '{3DF9195D-631B-4221-914E-F9649C8FF556}';
  CLASS_HolidayCollection: TGUID = '{7AA4642F-D9A2-4AF3-A3B0-597FDE303AB3}';
  CLASS_WeeklyJourneyWorking: TGUID = '{178117DF-1E72-4A5A-8F5F-8515F8A6CF64}';
  CLASS_ImmediateStatusResponse: TGUID = '{DBF99C7C-5052-4E64-A461-9ABF0465F402}';
  CLASS_PrintPointMRPEventLog: TGUID = '{D0608FD7-01F5-4C94-9797-3F93ADC0F002}';
  CLASS_Master: TGUID = '{16B43E66-3E3B-44A1-A26A-4CFFA1264CC6}';
  CLASS_PrintPointEmployerMessage: TGUID = '{5437F9D0-F0D9-4B4B-BCC6-229396BAC2D1}';
  CLASS_MicropointCardList: TGUID = '{57FCBA2E-E11F-4479-B203-AE1A7339912A}';
  CLASS_ShiftTableCollection: TGUID = '{F3E7E810-A66D-4F08-9B3F-56342C57A064}';
  CLASS_MonthlyJourneyWorking: TGUID = '{66256A35-652D-444D-90D7-6DCBCFEA633C}';
  CLASS_AlarmRingsCollection: TGUID = '{E21378D2-FA68-4106-837E-2130AC8621E6}';
  CLASS_SerialComm: TGUID = '{26508346-1BE6-451A-AC0B-F36E7CA9C6C9}';
  CLASS_Card: TGUID = '{4EA399E7-3A75-47D2-9166-F594FC97DB03}';
  CLASS_BioPointFingerPrintMessage: TGUID = '{309458CD-FC36-4418-9E8A-DA45DC691923}';
  CLASS_FaceLogRecord: TGUID = '{98C78AAB-DD68-4A62-B4E8-D0C5FEE578E4}';

// *********************************************************************//
// Declaration of Enumerations defined in Type Library                    
// *********************************************************************//
// Constants for enum EPrintPointMRPRecordType
type
  EPrintPointMRPRecordType = TOleEnum;
const
  EPrintPointMRPRecordType_ChangeEmployed = $00000005;
  EPrintPointMRPRecordType_SettingRealTimeClock = $00000004;
  EPrintPointMRPRecordType_RegistrationMarkingPoint = $00000003;
  EPrintPointMRPRecordType_ChangeCompanyIdentification = $00000002;

// Constants for enum ChangeEmployeeType
type
  ChangeEmployeeType = TOleEnum;
const
  ChangeEmployeeType_Inclusion = $00000000;
  ChangeEmployeeType_Exclusion = $00000001;
  ChangeEmployeeType_Alteration = $00000002;

// Constants for enum MicroPointMessageType
type
  MicroPointMessageType = TOleEnum;
const
  MicroPointMessageType_InquiryStatus = $00000028;
  MicroPointMessageType_StatusResponse = $00000070;
  MicroPointMessageType_Punch = $00000000;
  MicroPointMessageType_CollectRemoving = $0000000F;
  MicroPointMessageType_CollectWithoutRemoving = $0000000E;
  MicroPointMessageType_Empty = $0000000F;
  MicroPointMessageType_Received = $0000000F;
  MicroPointMessageType_SetDateAndDST = $00000005;
  MicroPointMessageType_OK = $0000000F;
  MicroPointMessageType_Invalid = $0000000E;
  MicroPointMessageType_EnableLogDeniedAccess = $0000001B;
  MicroPointMessageType_ProgramLotterySampleRate = $0000001D;
  MicroPointMessageType_ProgramTriggerType = $00000007;
  MicroPointMessageType_ConfigureCard = $00000029;
  MicroPointMessageType_Activation = $00000024;
  MicroPointMessageType_ConfigureMessage = $00000020;
  MicroPointMessageType_Master = $00000019;
  MicroPointMessageType_ClearProgramming = $00000002;
  MicroPointMessageType_Holiday = $00000004;
  MicroPointMessageType_CardList = $000000AA;
  MicroPointMessageType_RemoveItemCardList = $00000001;
  MicroPointMessageType_ShiftTable = $00000008;
  MicroPointMessageType_AlarmRings = $00000009;
  MicroPointMessageType_MemoryFormat = $00000017;
  MicroPointMessageType_AlternativeCode = $00000013;
  MicroPointMessageType_RemoveAlternativeCode = $00000014;
  MicroPointMessageType_JourneyWorking = $00000021;

// Constants for enum TypeCheckCard
type
  TypeCheckCard = TOleEnum;
const
  TypeCheckCard_Modulo10 = $00000002;
  TypeCheckCard_Modulo11 = $00000001;

// Constants for enum EFingerPrintType
type
  EFingerPrintType = TOleEnum;
const
  EFingerPrintType_LeftMinimo = $00000000;
  EFingerPrintType_LeftRing = $00000001;
  EFingerPrintType_MiddleLeft = $00000002;
  EFingerPrintType_LeftIndicator = $00000003;
  EFingerPrintType_LeftThumb = $00000004;
  EFingerPrintType_RightMinimo = $00000005;
  EFingerPrintType_RightRing = $00000006;
  EFingerPrintType_MiddleRight = $00000007;
  EFingerPrintType_RightIndicator = $00000008;
  EFingerPrintType_RightThumb = $00000009;

// Constants for enum EFingerPrintHand
type
  EFingerPrintHand = TOleEnum;
const
  EFingerPrintHand_Without = $00000000;
  EFingerPrintHand_LeftHand = $00000001;
  EFingerPrintHand_RightHand = $00000002;

// Constants for enum EfingerPrintSensor
type
  EfingerPrintSensor = TOleEnum;
const
  EfingerPrintSensor_Sagem = $00000000;
  EfingerPrintSensor_Suprema = $00000001;
  EfingerPrintSensor_Fugitsu = $00000002;

// Constants for enum EPrintPointEncryptionType
type
  EPrintPointEncryptionType = TOleEnum;
const
  EPrintPointEncryptionType_NoEncryption = $00000000;
  EPrintPointEncryptionType_EightDigits = $00000001;
  EPrintPointEncryptionType_TwelveDigits = $00000002;
  EPrintPointEncryptionType_TenDigits = $00000003;

// Constants for enum EPrintPointAuthenticationType
type
  EPrintPointAuthenticationType = TOleEnum;
const
  EPrintPointAuthenticationType_NoAuthentication = $00000000;
  EPrintPointAuthenticationType_OnlyPassword = $00000001;
  EPrintPointAuthenticationType_OnlyBiometrics = $00000002;
  EPrintPointAuthenticationType_BiometricOrPassword = $00000003;
  EPrintPointAuthenticationType_BiometricAndPassword = $00000004;

// Constants for enum EPrintPointAccessType
type
  EPrintPointAccessType = TOleEnum;
const
  EPrintPointAccessType_Credential = $00000000;
  EPrintPointAccessType_PIS = $00000001;

// Constants for enum EPrintPointAuthenticationTypeIdentification
type
  EPrintPointAuthenticationTypeIdentification = TOleEnum;
const
  EPrintPointAuthenticationTypeIdentification_NoAuthentication = $00000000;
  EPrintPointAuthenticationTypeIdentification_OnlyPassword = $00000001;

// Constants for enum EPrintPointBiometricAuthenticationType
type
  EPrintPointBiometricAuthenticationType = TOleEnum;
const
  EPrintPointBiometricAuthenticationType_Always = $00000000;
  EPrintPointBiometricAuthenticationType_Partial = $00000001;

// Constants for enum EPrintPointAdvanceSizeType
type
  EPrintPointAdvanceSizeType = TOleEnum;
const
  EPrintPointAdvanceSizeType_Small = $00000000;
  EPrintPointAdvanceSizeType_Medium = $00000001;
  EPrintPointAdvanceSizeType_Long = $00000002;

// Constants for enum EPrintPointCutType
type
  EPrintPointCutType = TOleEnum;
const
  EPrintPointCutType_Partial = $00000000;
  EPrintPointCutType_Total = $00000001;

// Constants for enum EPrintPointEnergyPrinter
type
  EPrintPointEnergyPrinter = TOleEnum;
const
  EPrintPointEnergyPrinter_Regular = $00000000;
  EPrintPointEnergyPrinter_Elevated = $00000001;
  EPrintPointEnergyPrinter_Reduced = $00000002;

// Constants for enum EPrintPointSmartCardUseType
type
  EPrintPointSmartCardUseType = TOleEnum;
const
  EPrintPointSmartCardUseType_NotUse = $00000000;
  EPrintPointSmartCardUseType_ReadID = $00000001;
  EPrintPointSmartCardUseType_ReadRegistrationID = $00000002;

// Constants for enum EPrintPointWiegand37BitsUseType
type
  EPrintPointWiegand37BitsUseType = TOleEnum;
const
  EPrintPointWiegand37BitsUseType_NotUse = $00000000;
  EPrintPointWiegand37BitsUseType_DefaultH10302 = $00000001;
  EPrintPointWiegand37BitsUseType_DefaultH10304 = $00000002;

// Constants for enum EPrintPointPersonalizationType
type
  EPrintPointPersonalizationType = TOleEnum;
const
  EPrintPointPersonalizationType_NotUse = $00000000;
  EPrintPointPersonalizationType_Micropoint = $00000001;
  EPrintPointPersonalizationType_Megapoint = $00000002;

// Constants for enum PrintPointEventType
type
  PrintPointEventType = TOleEnum;
const
  PrintPointEventType_ViolationSensorWithLink = $00000000;
  PrintPointEventType_ViolationSensorWithoutLink = $00000001;
  PrintPointEventType_LinkConnected = $00000002;
  PrintPointEventType_LinkDisconnected = $00000003;
  PrintPointEventType_ViolationSensorWithClockOff = $00000004;

// Constants for enum ParcialConfigurationType
type
  ParcialConfigurationType = TOleEnum;
const
  ParcialConfigurationType_DST = $000003E8;
  ParcialConfigurationType_PrinterAdvanceSize = $00001B58;
  ParcialConfigurationType_PrinterCutType = $00001B62;
  ParcialConfigurationType_EnergyPrinter = $00001B6C;
  ParcialConfigurationType_LenghtBobbin = $00001B76;

// Constants for enum WatchProtocolType
type
  WatchProtocolType = TOleEnum;
const
  WatchProtocolType_BioPoint = $00000000;
  WatchProtocolType_MicroPoint = $00000001;
  WatchProtocolType_MiniPoint = $00000002;
  WatchProtocolType_BioLite = $00000003;
  WatchProtocolType_PrintPoint = $00000004;
  WatchProtocolType_PrintPointLi = $00000005;
  WatchProtocolType_MiniPrint = $00000006;
  WatchProtocolType_Face = $00000007;

// Constants for enum WatchConnectionType
type
  WatchConnectionType = TOleEnum;
const
  WatchConnectionType_ConnectedMode = $00000000;
  WatchConnectionType_DisconnectedMode = $00000001;

// Constants for enum EPrintPointHardwareTestType
type
  EPrintPointHardwareTestType = TOleEnum;
const
  EPrintPointHardwareTestType_Maker = $00000001;
  EPrintPointHardwareTestType_Receptor = $00000002;

// Constants for enum EPrintPointHardwareTestResultType
type
  EPrintPointHardwareTestResultType = TOleEnum;
const
  EPrintPointHardwareTestResultType_NotTested = $00000000;
  EPrintPointHardwareTestResultType_OK = $00000001;
  EPrintPointHardwareTestResultType_Error = $00000002;

// Constants for enum EPrintPointHardwareTestDeviceType
type
  EPrintPointHardwareTestDeviceType = TOleEnum;
const
  EPrintPointHardwareTestDeviceType_Display = $00000001;
  EPrintPointHardwareTestDeviceType_Keyboard = $00000002;
  EPrintPointHardwareTestDeviceType_USB1 = $00000003;
  EPrintPointHardwareTestDeviceType_USB2 = $00000004;
  EPrintPointHardwareTestDeviceType_Communication = $00000005;
  EPrintPointHardwareTestDeviceType_CabinetSensor = $00000006;
  EPrintPointHardwareTestDeviceType_EnergySensor = $00000007;
  EPrintPointHardwareTestDeviceType_LittlePaperSensor = $00000008;
  EPrintPointHardwareTestDeviceType_NopaperSensor = $00000009;
  EPrintPointHardwareTestDeviceType_CardReader = $0000000A;
  EPrintPointHardwareTestDeviceType_MagneticReader = $0000000B;
  EPrintPointHardwareTestDeviceType_BiometricReader = $0000000C;
  EPrintPointHardwareTestDeviceType_MRP1Memory = $0000000D;
  EPrintPointHardwareTestDeviceType_MRP2Memory = $0000000E;
  EPrintPointHardwareTestDeviceType_FRAMMemory = $0000000F;
  EPrintPointHardwareTestDeviceType_Flash1Memory = $00000010;
  EPrintPointHardwareTestDeviceType_Flash2Memory = $00000011;
  EPrintPointHardwareTestDeviceType_SDCardMemory = $00000012;

// Constants for enum MessageType
type
  MessageType = TOleEnum;
const
  MessageType_Null = $00000000;
  MessageType_StatusInquiry = $00000001;
  MessageType_StatusResponse = $00000002;
  MessageType_Punch = $00000003;
  MessageType_CollectRemoving = $00000004;
  MessageType_CollectWithoutRemoving = $00000005;
  MessageType_Received = $00000006;
  MessageType_Empty = $00000007;
  MessageType_OK = $00000008;
  MessageType_Invalid = $00000009;
  MessageType_SetDateAndDST = $0000000A;
  MessageType_SetDateTime = $0000000B;
  MessageType_SetDST = $0000000C;
  MessageType_EnableLogDeniedAccess = $0000000D;
  MessageType_ProgramLotterySampleRate = $0000000E;
  MessageType_ProgramTriggerType = $0000000F;
  MessageType_ConfigureCard = $00000010;
  MessageType_Activation = $00000011;
  MessageType_CardList = $00000012;
  MessageType_ConfigureMessage = $00000013;
  MessageType_Master = $00000014;
  MessageType_ClearProgramming = $00000015;
  MessageType_Holiday = $00000016;
  MessageType_RemoveItemCardList = $00000017;
  MessageType_ShiftTable = $00000018;
  MessageType_AlarmRings = $00000019;
  MessageType_MemoryFormat = $0000001A;
  MessageType_AlternativeCode = $0000001B;
  MessageType_RemoveAlternativeCode = $0000001C;
  MessageType_JourneyWorking = $0000001D;
  MessageType_EmployeesTotalProgrammingBegin = $0000001E;
  MessageType_EmployeesTotalProgrammingEnd = $0000001F;
  MessageType_ConfirmationReceiptMRPRecords = $00000020;
  MessageType_ConfirmationReceiptFingerPrint = $00000021;
  MessageType_ConfirmationReceiptBioPointFingerPrint = $00000022;
  MessageType_CleanCredentialsList = $00000023;
  MessageType_CleanMasterList = $00000024;
  MessageType_CleanDisplayMessage = $00000025;
  MessageType_RepositioningMRPRecordsPointer = $00000026;
  MessageType_HandlesFingerPrint = $00000027;
  MessageType_ExcludeBioPointFingerPrint = $00000028;
  MessageType_IncludeBioPointFingerPrint = $00000029;
  MessageType_HandlesFingerPrintGeneric = $0000002A;
  MessageType_HandlesCredential = $0000002B;
  MessageType_HandlesCard = $0000002C;
  MessageType_HandlesCardWithCostCenter = $0000002D;
  MessageType_SendDisplayMessage = $0000002E;
  MessageType_ChangeEmployer = $0000002F;
  MessageType_FirstActivation = $00000030;
  MessageType_PrintPointActivation = $00000031;
  MessageType_SendMACAddress = $00000032;
  MessageType_FingerPrintResponse = $00000033;
  MessageType_BioPointFingerPrintResponse = $00000034;
  MessageType_InquiryFingerPrint = $00000035;
  MessageType_InquiryBioPointFingerPrint = $00000036;
  MessageType_EndFingerPrintRegisters = $00000037;
  MessageType_InquiryEmployer = $00000038;
  MessageType_InquiryEmployerResponse = $00000039;
  MessageType_InquiryRandomNumber = $0000003A;
  MessageType_InquiryRandomNumberResponse = $0000003B;
  MessageType_InquirySerialNumberOfREPAndMemory = $0000003C;
  MessageType_InquirySerialNumberOfREPAndMemoryResponse = $0000003D;
  MessageType_InquiryMRPRegisters = $0000003E;
  MessageType_Settings = $0000003F;
  MessageType_ParcialSettings = $00000040;
  MessageType_MRPRegistersResponse = $00000041;
  MessageType_EndMRPRegisters = $00000042;
  MessageType_FirmwareTransmissionStart = $00000043;
  MessageType_SendFirmwareBlock = $00000044;
  MessageType_FirmwareTransmissionEnd = $00000045;
  MessageType_EquipmentUnavailable = $00000046;
  MessageType_Processing = $00000047;
  MessageType_Finished = $00000048;
  MessageType_ExcludeFingerPrintList = $00000049;
  MessageType_ExcludeFingerPrintWithoutEmployee = $0000004A;
  MessageType_SendSerialNumber = $0000004B;
  MessageType_InquiryImediateStatus = $0000004C;
  MessageType_InquiryImediateStatusResponse = $0000004D;
  MessageType_InquiryEmployeeList = $0000004E;
  MessageType_ConfirmationReceiptEmployeeList = $0000004F;
  MessageType_SendEmployeeRecords = $00000050;
  MessageType_EndEmployeeRecords = $00000051;
  MessageType_REPPlacesInMaintenance = $00000052;
  MessageType_CleanEssentialVariables = $00000053;
  MessageType_RebuildEmployeesTable = $00000054;
  MessageType_HardwareTestCollection = $00000055;
  MessageType_HardwareTestCollectionResponse = $00000056;
  MessageType_GetMAC = $00000057;
  MessageType_GetMACResponse = $00000058;
  MessageType_ExchangeSealREP = $00000059;
  MessageType_ActivateBootLoader = $0000005A;
  MessageType_InquiryEvent = $0000005B;
  MessageType_ConfirmEvent = $0000005C;
  MessageType_EventRegister = $0000005D;
  MessageType_InquiryMRPEventLog = $0000005E;
  MessageType_MRPEventLog = $0000005F;
  MessageType_InquiryGenericFingerprint = $00000060;
  MessageType_GenericFingerPrintResponse = $00000061;
  MessageType_SaveBufferPointer = $00000062;
  MessageType_CleanCostCenterList = $00000063;
  MessageType_HandlesCostCenter = $00000064;

// Constants for enum TypeAlarm
type
  TypeAlarm = TOleEnum;
const
  TypeAlarm_Inside = $00000000;
  TypeAlarm_Outside = $00000001;

// Constants for enum TypeRinging
type
  TypeRinging = TOleEnum;
const
  TypeRinging_NoRing = $00000000;
  TypeRinging_Ring = $00000001;

// Constants for enum EConfigurationType
type
  EConfigurationType = TOleEnum;
const
  EConfigurationType_DST = $000003E8;
  EConfigurationType_Enabled2Of5Intercalary = $000007D0;
  EConfigurationType_Enabled2of5Dimep = $000007E4;
  EConfigurationType_Enabled3Of9 = $000007EE;
  EConfigurationType_EnabledMagneticDIMEP = $000007F8;
  EConfigurationType_EnabledABA = $00000802;
  EConfigurationType_EnabledWiegand26Bits = $0000080C;
  EConfigurationType_EnabledWiegand34Bits = $0000088E;
  EConfigurationType_EnabledEan13 = $00000834;
  EConfigurationType_EnabledWiegand35Bits = $00000820;
  EConfigurationType_Wiegand37BitsUseType = $0000082A;
  EConfigurationType_EnabledSpecialWiegand32Bits = $0000080F;
  EConfigurationType_SmartCardUseType = $00000816;
  EConfigurationType_EnabledWiegandParityRead = $0000080E;
  EConfigurationType_SmartCardSector = $0000087A;
  EConfigurationType_SmartCardBlock = $0000087B;
  EConfigurationType_SmartCardOffSet = $0000087C;
  EConfigurationType_SmartCardEncryptedKey = $0000087D;
  EConfigurationType_SmartCardDigitsNumber = $0000087E;
  EConfigurationType_Format2Of5Intercalary = $000007D1;
  EConfigurationType_Format2of5Dimep = $000007E5;
  EConfigurationType_Format3Of9 = $000007EF;
  EConfigurationType_FormatMagneticDIMEP = $000007F9;
  EConfigurationType_FormatABA = $00000803;
  EConfigurationType_FormatWiegand26Bits = $0000080D;
  EConfigurationType_FormatWiegand34Bits = $0000088F;
  EConfigurationType_FormatEan13 = $00000835;
  EConfigurationType_FormatWiegand35Bits = $00000821;
  EConfigurationType_FormatWiegand37Bits = $0000082B;
  EConfigurationType_FormatSmartCard = $00000817;
  EConfigurationType_EncryptionType = $000007D2;
  EConfigurationType_SpecialFormatMagneticDIMEP1 = $000007FB;
  EConfigurationType_SpecialFormatABA1 = $00000805;
  EConfigurationType_PersonalizationType = $000007D4;
  EConfigurationType_CardEnabled = $00000BB8;
  EConfigurationType_CardAccessType = $00000BC2;
  EConfigurationType_CardAuthenticationType = $00000BCC;
  EConfigurationType_ShowCredentialInDisplay = $00000BD6;
  EConfigurationType_KeyBoardEnabled = $00000C1C;
  EConfigurationType_keyBoardAccessType = $00000C26;
  EConfigurationType_keyBoardAuthenticationType = $00000C30;
  EConfigurationType_IdentificationEnabled = $00000C80;
  EConfigurationType_IdentificationAuthenticationTypeIdentification = $00000C94;
  EConfigurationType_Authentication = $00000FA0;
  EConfigurationType_PrinterAdvanceSize = $00001B58;
  EConfigurationType_PrinterCutType = $00001B62;
  EConfigurationType_SecurityLevelSagem = $00000FA1;
  EConfigurationType_SecurityLevelSuprema = $00000FA2;
  EConfigurationType_EnergyPrinter = $00001B6C;
  EConfigurationType_LenghtBobbin = $00001B76;

// Constants for enum RepositioningMRPRecordsPointerType
type
  RepositioningMRPRecordsPointerType = TOleEnum;
const
  RepositioningMRPRecordsPointerType_Total = $00000000;
  RepositioningMRPRecordsPointerType_Date = $00000001;
  RepositioningMRPRecordsPointerType_NSR = $00000002;

// Constants for enum InquiryFingerPrintType
type
  InquiryFingerPrintType = TOleEnum;
const
  InquiryFingerPrintType_All = $00000000;
  InquiryFingerPrintType_OnlyNew = $00000001;

// Constants for enum InvalidMessageType
type
  InvalidMessageType = TOleEnum;
const
  InvalidMessageType_UnknownFunction = $00000001;
  InvalidMessageType_InvalidData = $00000002;
  InvalidMessageType_OutOfMemory = $00000003;
  InvalidMessageType_CardWithMoreDigitsThanProgrammed = $00000005;
  InvalidMessageType_FailureBiometricModule = $00000006;
  InvalidMessageType_InvalidFingerprint = $00000007;
  InvalidMessageType_DuplicateFingerprint = $00000008;
  InvalidMessageType_FieldNotInformed = $00000009;
  InvalidMessageType_InvalidProtocol = $0000000A;
  InvalidMessageType_EquipmentWasNotActivated = $0000000B;
  InvalidMessageType_EquipmentAlreadyActivated = $0000000C;
  InvalidMessageType_FunctionNotPermitted = $0000000D;
  InvalidMessageType_EquipmentWithoutEmployer = $0000000E;
  InvalidMessageType_EquipmentNotViolated = $0000000F;
  InvalidMessageType_InvalidPassword = $00000010;
  InvalidMessageType_CapacityExceededOfBiometricModule = $00000011;
  InvalidMessageType_BiometricModuleReturnedError = $00000012;
  InvalidMessageType_MRPReturnedError = $00000013;
  InvalidMessageType_WithoutDataToBeTransmitted = $00000014;

// Constants for enum BioPointMessageType
type
  BioPointMessageType = TOleEnum;
const
  BioPointMessageType_InquiryStatus = $00000028;
  BioPointMessageType_StatusResponse = $00000070;
  BioPointMessageType_Punch = $00000000;
  BioPointMessageType_CollectRemoving = $0000000F;
  BioPointMessageType_CollectWithoutRemoving = $0000000E;
  BioPointMessageType_MessageReceived = $0000000F;
  BioPointMessageType_Empty = $0000000F;
  BioPointMessageType_SetDateAndDST = $00000005;
  BioPointMessageType_OK = $0000000F;
  BioPointMessageType_Invalid = $0000000E;
  BioPointMessageType_EnableLogDeniedAccess = $0000001B;
  BioPointMessageType_ProgramLotterySampleRate = $0000001D;
  BioPointMessageType_ProgramTriggerType = $00000007;
  BioPointMessageType_ConfigureCard = $00000029;
  BioPointMessageType_Activation = $00000024;
  BioPointMessageType_ConfigureMessage = $00000020;
  BioPointMessageType_InquiryBioPointFingerPrint = $00000037;
  BioPointMessageType_BioPointFingerPrintResponseOrIncludeFingerPrint = $00000038;
  BioPointMessageType_ConfirmationReceiptBioPointFingerPrint = $0000003B;
  BioPointMessageType_ExcludeBioPointFingerPrint = $0000003E;
  BioPointMessageType_ExcludeFingerPrintList = $00000039;
  BioPointMessageType_Master = $00000019;
  BioPointMessageType_ClearProgramming = $00000002;
  BioPointMessageType_Holiday = $00000004;
  BioPointMessageType_CardList = $000000AA;
  BioPointMessageType_RemoveItemCardList = $00000001;
  BioPointMessageType_ShiftTable = $00000008;
  BioPointMessageType_AlarmRings = $00000009;
  BioPointMessageType_MemoryFormat = $00000017;
  BioPointMessageType_AlternativeCode = $00000013;
  BioPointMessageType_RemoveAlternativeCode = $00000014;
  BioPointMessageType_JourneyWorking = $00000021;
  BioPointMessageType_SaveBufferPointer = $0000000C;

// Constants for enum BobbinStateType
type
  BobbinStateType = TOleEnum;
const
  BobbinStateType_OK = $00000000;
  BobbinStateType_LittlePaper = $00000001;
  BobbinStateType_NoPaper = $00000002;
  BobbinStateType_VeryLittlePaper = $00000003;

// Constants for enum MRPStateType
type
  MRPStateType = TOleEnum;
const
  MRPStateType_NormalOperation = $00000000;
  MRPStateType_Unknown = $00000001;
  MRPStateType_WithoutEmployer = $00000002;
  MRPStateType_WithoutInitialActivation = $00000003;
  MRPStateType_Error = $00000004;
  MRPStateType_Initializing = $00000005;

// Constants for enum REPStateType
type
  REPStateType = TOleEnum;
const
  REPStateType_NormalOperation = $00000000;
  REPStateType_Maintenance = $00000001;

// Constants for enum AFDGenerationType
type
  AFDGenerationType = TOleEnum;
const
  AFDGenerationType_NotGenerating = $00000000;
  AFDGenerationType_Generating = $00000001;

// Constants for enum RIMGenerationType
type
  RIMGenerationType = TOleEnum;
const
  RIMGenerationType_NotGenerating = $00000000;
  RIMGenerationType_Generating = $00000001;

// Constants for enum AlimentationType
type
  AlimentationType = TOleEnum;
const
  AlimentationType_ACAlimentation = $00000000;
  AlimentationType_BatteryAlimentation = $00000001;

// Constants for enum BatteryLevel
type
  BatteryLevel = TOleEnum;
const
  BatteryLevel_Regular = $00000000;
  BatteryLevel_Small = $00000001;
  BatteryLevel_VerySmall = $00000002;

// Constants for enum PrintPointMessageType
type
  PrintPointMessageType = TOleEnum;
const
  PrintPointMessageType_InquiryStatus = $00000080;
  PrintPointMessageType_RepositioningMRPRecordsPointer = $000000A5;
  PrintPointMessageType_SetDate = $000000C1;
  PrintPointMessageType_Settings = $000000C2;
  PrintPointMessageType_ParcialSettings = $000000C3;
  PrintPointMessageType_EmployeesTotalProgrammingBegin = $000000C4;
  PrintPointMessageType_EmployeesTotalProgrammingEnd = $000000C5;
  PrintPointMessageType_InsertOrDeleteEmployees = $000000C6;
  PrintPointMessageType_InsertOrDeleteEmployeesWithCostNumber = $000000F0;
  PrintPointMessageType_CleanCredentialsList = $000000C7;
  PrintPointMessageType_InsertOrDeleteCredentialInList = $000000C8;
  PrintPointMessageType_HandlesFingerPrint = $000000C9;
  PrintPointMessageType_HandlesFingerPrintGeneric = $000000E1;
  PrintPointMessageType_InquiryFingerPrint = $000000CA;
  PrintPointMessageType_InquiryGenericFingerprint = $000000E2;
  PrintPointMessageType_ExcludeFingerPrintList = $000000CB;
  PrintPointMessageType_ExcludeFingerPrintWithoutEmployee = $000000CC;
  PrintPointMessageType_InquiryMRPRegisters = $000000CD;
  PrintPointMessageType_ConfirmationReceiptMRPRecords = $000000CE;
  PrintPointMessageType_CleanMasterList = $000000CF;
  PrintPointMessageType_IncludeMasterInList = $000000D0;
  PrintPointMessageType_ConfirmationReceiptFingerPrint = $000000D1;
  PrintPointMessageType_SendDisplayMessage = $000000D2;
  PrintPointMessageType_CleanDisplayMessage = $000000D3;
  PrintPointMessageType_ChangeEmployer = $000000D4;
  PrintPointMessageType_InquiryEmployer = $000000D5;
  PrintPointMessageType_FirstActivation = $000000D6;
  PrintPointMessageType_Activation = $000000D7;
  PrintPointMessageType_InquiryRandomNumber = $000000D8;
  PrintPointMessageType_InquirySerialNumberOfREPAndMemory = $000000D9;
  PrintPointMessageType_SendMACAddress = $000000DA;
  PrintPointMessageType_FirmwareTransmissionStart = $000000DC;
  PrintPointMessageType_SendFirmwareBlock = $000000DD;
  PrintPointMessageType_FirmwareTransmissionEnd = $000000DE;
  PrintPointMessageType_Finished = $00000070;
  PrintPointMessageType_SendSerialNumber = $000000DB;
  PrintPointMessageType_InquiryImmediateStatus = $00000081;
  PrintPointMessageType_InquiryEmployeeList = $00000082;
  PrintPointMessageType_ConfirmationReceiptEmployeeList = $00000083;
  PrintPointMessageType_REPPlacesInMaintenance = $000000DF;
  PrintPointMessageType_CleanEssentialVariables = $000000EA;
  PrintPointMessageType_RebuildEmployeesTable = $000000EB;
  PrintPointMessageType_HardwareTestCollection = $00000050;
  PrintPointMessageType_GetMAC = $000000D9;
  PrintPointMessageType_ExchangeSealREP = $000000EC;
  PrintPointMessageType_ActivateBootLoader = $00000060;
  PrintPointMessageType_InquiryEvent = $000000ED;
  PrintPointMessageType_ConfirmEvent = $000000EE;
  PrintPointMessageType_InquiryMRPEventLog = $000000EF;
  PrintPointMessageType_HandlesCostCenter = $000000F1;
  PrintPointMessageType_CleanCostCenterList = $000000F2;
  PrintPointMessageType_OK = $00000000;
  PrintPointMessageType_Error = $00000001;
  PrintPointMessageType_Processing = $00000002;
  PrintPointMessageType_EquipmentUnavailable = $00000003;
  PrintPointMessageType_StatusResponse = $00000020;
  PrintPointMessageType_FingerPrintResponse = $0000002A;
  PrintPointMessageType_GenericFingerPrintResponse = $00000035;
  PrintPointMessageType_EndFingerPrints = $0000002D;
  PrintPointMessageType_MRPRegistersResponse = $0000002B;
  PrintPointMessageType_EndMRPRegisters = $0000002C;
  PrintPointMessageType_EndFingerPrintRegisters = $0000002D;
  PrintPointMessageType_InquiryEmployerResponse = $0000002E;
  PrintPointMessageType_InquiryRandomNumberResponse = $0000002F;
  PrintPointMessageType_InquirySerialNumberOfREPAndMemoryResponse = $00000030;
  PrintPointMessageType_InquiryImediateStatusResponse = $00000021;
  PrintPointMessageType_SendEmployeeRecords = $00000031;
  PrintPointMessageType_EndEmployeeRecords = $00000032;
  PrintPointMessageType_HardwareTestCollectionResponse = $00000051;
  PrintPointMessageType_GetMACResponse = $00000010;
  PrintPointMessageType_EventRegister = $00000033;
  PrintPointMessageType_MRPEventLog = $00000034;

// Constants for enum CredentialListActionType
type
  CredentialListActionType = TOleEnum;
const
  CredentialListActionType_Include = $00000000;
  CredentialListActionType_Exclude = $00000001;

// Constants for enum EmployeeListActionType
type
  EmployeeListActionType = TOleEnum;
const
  EmployeeListActionType_Include = $00000000;
  EmployeeListActionType_Exclude = $00000001;
  EmployeeListActionType_TotalProgramming = $00000002;

// Constants for enum TypeDedoDigital
type
  TypeDedoDigital = TOleEnum;
const
  TypeDedoDigital_MinimoEsquerdo = $00000000;
  TypeDedoDigital_AnularEsquerdo = $00000001;
  TypeDedoDigital_MedioEsquerdo = $00000002;
  TypeDedoDigital_IndicadorEsquerdo = $00000003;
  TypeDedoDigital_PolegarEsquerdo = $00000004;
  TypeDedoDigital_MinimoDireito = $00000005;
  TypeDedoDigital_AnularDireito = $00000006;
  TypeDedoDigital_MedioDireito = $00000007;
  TypeDedoDigital_IndicadorDireito = $00000008;
  TypeDedoDigital_PolegarDireito = $00000009;

// Constants for enum TypeMasterCard
type
  TypeMasterCard = TOleEnum;
const
  TypeMasterCard_Master = $00000002;
  TypeMasterCard_Funcionario = $00000000;

// Constants for enum TypeClearProgramming
type
  TypeClearProgramming = TOleEnum;
const
  TypeClearProgramming_All = $00000000;
  TypeClearProgramming_CardAndAlternativeCode = $00000001;
  TypeClearProgramming_AlternativeCode = $00000002;
  TypeClearProgramming_Holiday = $00000003;
  TypeClearProgramming_Turn = $00000004;
  TypeClearProgramming_Jornadas = $00000005;
  TypeClearProgramming_MessageFunctions = $00000006;
  TypeClearProgramming_RingBell = $00000007;
  TypeClearProgramming_Master = $00000008;

// Constants for enum PermissionMaster
type
  PermissionMaster = TOleEnum;
const
  PermissionMaster_PenDriveProgramming = $00000001;
  PermissionMaster_DateTime = $00000002;
  PermissionMaster_ProgrammingTechnical = $00000004;

// Constants for enum TypeMessageConfigurantion
type
  TypeMessageConfigurantion = TOleEnum;
const
  TypeMessageConfigurantion_User = $00000000;
  TypeMessageConfigurantion_System = $00000001;
  TypeMessageConfigurantion_Function = $00000002;

// Constants for enum MessageUserType
type
  MessageUserType = TOleEnum;
const
  MessageUserType_MessageUser1 = $00000001;
  MessageUserType_MessageUser2 = $00000002;
  MessageUserType_MessageUser3 = $00000003;
  MessageUserType_MessageUser4 = $00000004;
  MessageUserType_MessageUser5 = $00000005;
  MessageUserType_MessageUser6 = $00000006;
  MessageUserType_MessageUser7 = $00000007;

// Constants for enum SystemTypeMessages
type
  SystemTypeMessages = TOleEnum;
const
  SystemTypeMessages_MessageHeaderRelogio = $00000000;
  SystemTypeMessages_MessageBloqueadoPorLista = $00000003;
  SystemTypeMessages_MessageBloqueadoPorHorario = $00000004;
  SystemTypeMessages_MessageExcedeuContAcessos = $00000007;
  SystemTypeMessages_MessageAcessoLeitor0 = $00000008;
  SystemTypeMessages_MessageAcessoLeitor1 = $00000009;
  SystemTypeMessages_MessageSorteadoPRevista = $0000000B;
  SystemTypeMessages_SenhaInvalida = $0000000C;
  SystemTypeMessages_AcessoPermitido = $0000000E;

// Constants for enum MessageFunctionType
type
  MessageFunctionType = TOleEnum;
const
  MessageFunctionType_MessageFunction1 = $00000001;
  MessageFunctionType_MessageFunction2 = $00000002;
  MessageFunctionType_MessageFunction3 = $00000003;
  MessageFunctionType_MessageFunction4 = $00000004;
  MessageFunctionType_MessageFunction5 = $00000005;
  MessageFunctionType_MessageFunction6 = $00000006;
  MessageFunctionType_MessageFunction7 = $00000007;
  MessageFunctionType_MessageFunction8 = $00000008;
  MessageFunctionType_MessageFunction9 = $00000009;
  MessageFunctionType_MessageFunction10 = $0000000A;
  MessageFunctionType_MessageFunction11 = $0000000B;
  MessageFunctionType_MessageFunction12 = $0000000C;
  MessageFunctionType_MessageFunction13 = $0000000D;
  MessageFunctionType_MessageFunction14 = $0000000E;
  MessageFunctionType_MessageFunction15 = $0000000F;
  MessageFunctionType_MessageFunction16 = $00000010;
  MessageFunctionType_MessageFunction17 = $00000011;
  MessageFunctionType_MessageFunction18 = $00000012;
  MessageFunctionType_MessageFunction19 = $00000013;
  MessageFunctionType_MessageFunction20 = $00000014;
  MessageFunctionType_MessageFunction21 = $00000015;
  MessageFunctionType_MessageFunction22 = $00000016;
  MessageFunctionType_MessageFunction23 = $00000017;
  MessageFunctionType_MessageFunction24 = $00000018;
  MessageFunctionType_MessageFunction25 = $00000019;
  MessageFunctionType_MessageFunction26 = $0000001A;
  MessageFunctionType_MessageFunction27 = $0000001B;
  MessageFunctionType_MessageFunction28 = $0000001C;
  MessageFunctionType_MessageFunction29 = $0000001D;
  MessageFunctionType_MessageFunction30 = $0000001E;
  MessageFunctionType_MessageFunction31 = $0000001F;
  MessageFunctionType_MessageFunction32 = $00000020;
  MessageFunctionType_MessageFunction33 = $00000021;
  MessageFunctionType_MessageFunction34 = $00000022;
  MessageFunctionType_MessageFunction35 = $00000023;
  MessageFunctionType_MessageFunction36 = $00000024;
  MessageFunctionType_MessageFunction37 = $00000025;
  MessageFunctionType_MessageFunction38 = $00000026;
  MessageFunctionType_MessageFunction39 = $00000027;
  MessageFunctionType_MessageFunction40 = $00000028;

// Constants for enum TypeWorking
type
  TypeWorking = TOleEnum;
const
  TypeWorking_Weekly = $00000000;
  TypeWorking_Monthly = $00000001;
  TypeWorking_Periodic = $00000002;

// Constants for enum EmployeerType
type
  EmployeerType = TOleEnum;
const
  EmployeerType_CNPJ = $00000001;
  EmployeerType_CPF = $00000002;

// Constants for enum Mode
type
  Mode = TOleEnum;
const
  Mode_Weekly = $00000000;
  Mode_Daly = $00000001;

// Constants for enum MiniPointMessageType
type
  MiniPointMessageType = TOleEnum;
const
  MiniPointMessageType_InquiryStatus = $0000001F;
  MiniPointMessageType_Punch = $00000000;
  MiniPointMessageType_StatusResponse = $00000000;
  MiniPointMessageType_StatusOrPuchMessage = $00000000;
  MiniPointMessageType_SetDateTime = $00000005;
  MiniPointMessageType_SetDST = $0000001E;
  MiniPointMessageType_OK = $0000000F;
  MiniPointMessageType_Invalid = $0000000E;
  MiniPointMessageType_CollectRemoving = $0000000F;
  MiniPointMessageType_CollectWithoutRemoving = $0000000E;
  MiniPointMessageType_Empty = $0000000F;
  MiniPointMessageType_ConfigureCard = $00000020;

type

// *********************************************************************//
// Forward declaration of types defined in TypeLibrary                    
// *********************************************************************//
  _AbstractMessage = interface;
  _AbstractMessageDisp = dispinterface;
  IComm = interface;
  ICommDisp = dispinterface;
  _TCPComm = interface;
  _TCPCommDisp = dispinterface;
  _AbstractProtocol = interface;
  _AbstractProtocolDisp = dispinterface;
  _EmployeesListTransmissionProgress = interface;
  _EmployeesListTransmissionProgressDisp = dispinterface;
  _CredentialListTransmissionProgress = interface;
  _CredentialListTransmissionProgressDisp = dispinterface;
  _PrintPointInquirySerialNumberOfREPAndMemoryResponse = interface;
  _PrintPointInquirySerialNumberOfREPAndMemoryResponseDisp = dispinterface;
  _PrintPointLiFingerPrint = interface;
  _PrintPointLiFingerPrintDisp = dispinterface;
  _PrintPointEmployee = interface;
  _PrintPointEmployeeDisp = dispinterface;
  _DeviceConnectionException = interface;
  _DeviceConnectionExceptionDisp = dispinterface;
  IWatchComm = interface;
  IWatchCommDisp = dispinterface;
  _WatchComm = interface;
  _WatchCommDisp = dispinterface;
  _CredentialsListTransmissionProgress = interface;
  _CredentialsListTransmissionProgressDisp = dispinterface;
  _PrintPointSendSerialNumberMessage = interface;
  _PrintPointSendSerialNumberMessageDisp = dispinterface;
  _PrintPointLiStatus = interface;
  _PrintPointLiStatusDisp = dispinterface;
  _MRPRecord = interface;
  _MRPRecordDisp = dispinterface;
  _MRPRecord_ChangeEmployee = interface;
  _MRPRecord_ChangeEmployeeDisp = dispinterface;
  _FaceFingerPrint = interface;
  _FaceFingerPrintDisp = dispinterface;
  _AbstractCardList = interface;
  _AbstractCardListDisp = dispinterface;
  _AbstractStatusMessage = interface;
  _AbstractStatusMessageDisp = dispinterface;
  _MiniPointStatusMessage = interface;
  _MiniPointStatusMessageDisp = dispinterface;
  _NoDataMessage = interface;
  _NoDataMessageDisp = dispinterface;
  _Template = interface;
  _TemplateDisp = dispinterface;
  _MiniPointConfigurator = interface;
  _MiniPointConfiguratorDisp = dispinterface;
  _Util = interface;
  _UtilDisp = dispinterface;
  _MicroPointStatusMessage = interface;
  _MicroPointStatusMessageDisp = dispinterface;
  _BioPointCardList = interface;
  _BioPointCardListDisp = dispinterface;
  _ShiftTable = interface;
  _ShiftTableDisp = dispinterface;
  _JourneyWorking = interface;
  _JourneyWorkingDisp = dispinterface;
  _PeriodicJourneyWorking = interface;
  _PeriodicJourneyWorkingDisp = dispinterface;
  _AbstractJourneyWorking = interface;
  _AbstractJourneyWorkingDisp = dispinterface;
  _AbstractPunchMessage = interface;
  _AbstractPunchMessageDisp = dispinterface;
  _MRPRecord_ChangeCompanyIdentification = interface;
  _MRPRecord_ChangeCompanyIdentificationDisp = dispinterface;
  _AbstractPacket = interface;
  _AbstractPacketDisp = dispinterface;
  _MemoryFormat = interface;
  _MemoryFormatDisp = dispinterface;
  _PrintPointFingerPrintMessage = interface;
  _PrintPointFingerPrintMessageDisp = dispinterface;
  _BioPointStatusMessage = interface;
  _BioPointStatusMessageDisp = dispinterface;
  _FaceEmployee = interface;
  _FaceEmployeeDisp = dispinterface;
  _Holiday = interface;
  _HolidayDisp = dispinterface;
  _RandomNumberResponse = interface;
  _RandomNumberResponseDisp = dispinterface;
  _PrintPointStatusMessage = interface;
  _PrintPointStatusMessageDisp = dispinterface;
  _PrintPointEvent = interface;
  _PrintPointEventDisp = dispinterface;
  _PrintPointLiEmployee = interface;
  _PrintPointLiEmployeeDisp = dispinterface;
  _ParcialConfiguration = interface;
  _ParcialConfigurationDisp = dispinterface;
  _MRPRecord_SettingRealTimeClock = interface;
  _MRPRecord_SettingRealTimeClockDisp = dispinterface;
  _MRPRecord_RegistrationMarkingPoint = interface;
  _MRPRecord_RegistrationMarkingPointDisp = dispinterface;
  _CardCollection = interface;
  _CardCollectionDisp = dispinterface;
  _HardwareTestCollectionResponse = interface;
  _HardwareTestCollectionResponseDisp = dispinterface;
  _GetMACResponse = interface;
  _GetMACResponseDisp = dispinterface;
  _ConcretePunchMessage = interface;
  _ConcretePunchMessageDisp = dispinterface;
  _AbstractMemoryFormat = interface;
  _AbstractMemoryFormatDisp = dispinterface;
  _BioPointMemoryFormat = interface;
  _BioPointMemoryFormatDisp = dispinterface;
  _TemplateCollection = interface;
  _TemplateCollectionDisp = dispinterface;
  _AlarmRings = interface;
  _AlarmRingsDisp = dispinterface;
  _Credential = interface;
  _CredentialDisp = dispinterface;
  _Configuration = interface;
  _ConfigurationDisp = dispinterface;
  _InvalidMessageException = interface;
  _InvalidMessageExceptionDisp = dispinterface;
  _FaceStatus = interface;
  _FaceStatusDisp = dispinterface;
  _AbstractFactory = interface;
  _AbstractFactoryDisp = dispinterface;
  _HolidayCollection = interface;
  _HolidayCollectionDisp = dispinterface;
  _WeeklyJourneyWorking = interface;
  _WeeklyJourneyWorkingDisp = dispinterface;
  _ImmediateStatusResponse = interface;
  _ImmediateStatusResponseDisp = dispinterface;
  _PrintPointMRPEventLog = interface;
  _PrintPointMRPEventLogDisp = dispinterface;
  _Master = interface;
  _MasterDisp = dispinterface;
  IRelogio = dispinterface;
  _PrintPointEmployerMessage = interface;
  _PrintPointEmployerMessageDisp = dispinterface;
  _MicropointCardList = interface;
  _MicropointCardListDisp = dispinterface;
  _ShiftTableCollection = interface;
  _ShiftTableCollectionDisp = dispinterface;
  _MonthlyJourneyWorking = interface;
  _MonthlyJourneyWorkingDisp = dispinterface;
  _AlarmRingsCollection = interface;
  _AlarmRingsCollectionDisp = dispinterface;
  _SerialComm = interface;
  _SerialCommDisp = dispinterface;
  _Card = interface;
  _CardDisp = dispinterface;
  _BioPointFingerPrintMessage = interface;
  _BioPointFingerPrintMessageDisp = dispinterface;
  _FaceLogRecord = interface;
  _FaceLogRecordDisp = dispinterface;
  _EmployeesListTransmissionProgress_2 = interface;
  _EmployeesListTransmissionProgress_2Disp = dispinterface;

// *********************************************************************//
// Declaration of CoClasses defined in Type Library                       
// (NOTE: Here we map each CoClass to its Default Interface)              
// *********************************************************************//
  Relogio = IRelogio;
  AbstractMessage = _AbstractMessage;
  TCPComm = _TCPComm;
  AbstractProtocol = _AbstractProtocol;
  EmployeesListTransmissionProgress = _EmployeesListTransmissionProgress;
  CredentialListTransmissionProgress = _CredentialListTransmissionProgress;
  PrintPointInquirySerialNumberOfREPAndMemoryResponse = _PrintPointInquirySerialNumberOfREPAndMemoryResponse;
  PrintPointLiFingerPrint = _PrintPointLiFingerPrint;
  PrintPointEmployee = _PrintPointEmployee;
  DeviceConnectionException = _DeviceConnectionException;
  WatchComm_ = _WatchComm;
  EmployeesListTransmissionProgress_2 = _EmployeesListTransmissionProgress_2;
  CredentialsListTransmissionProgress = _CredentialsListTransmissionProgress;
  PrintPointSendSerialNumberMessage = _PrintPointSendSerialNumberMessage;
  PrintPointLiStatus = _PrintPointLiStatus;
  MRPRecord = _MRPRecord;
  MRPRecord_ChangeEmployee = _MRPRecord_ChangeEmployee;
  FaceFingerPrint = _FaceFingerPrint;
  AbstractCardList = _AbstractCardList;
  AbstractStatusMessage = _AbstractStatusMessage;
  MiniPointStatusMessage = _MiniPointStatusMessage;
  NoDataMessage = _NoDataMessage;
  Template = _Template;
  MiniPointConfigurator = _MiniPointConfigurator;
  Util = _Util;
  MicroPointStatusMessage = _MicroPointStatusMessage;
  BioPointCardList = _BioPointCardList;
  ShiftTable = _ShiftTable;
  JourneyWorking = _JourneyWorking;
  PeriodicJourneyWorking = _PeriodicJourneyWorking;
  AbstractJourneyWorking = _AbstractJourneyWorking;
  AbstractPunchMessage = _AbstractPunchMessage;
  MRPRecord_ChangeCompanyIdentification = _MRPRecord_ChangeCompanyIdentification;
  AbstractPacket = _AbstractPacket;
  MemoryFormat = _MemoryFormat;
  PrintPointFingerPrintMessage = _PrintPointFingerPrintMessage;
  BioPointStatusMessage = _BioPointStatusMessage;
  FaceEmployee = _FaceEmployee;
  Holiday = _Holiday;
  RandomNumberResponse = _RandomNumberResponse;
  PrintPointStatusMessage = _PrintPointStatusMessage;
  PrintPointEvent = _PrintPointEvent;
  PrintPointLiEmployee = _PrintPointLiEmployee;
  ParcialConfiguration = _ParcialConfiguration;
  MRPRecord_SettingRealTimeClock = _MRPRecord_SettingRealTimeClock;
  MRPRecord_RegistrationMarkingPoint = _MRPRecord_RegistrationMarkingPoint;
  CardCollection = _CardCollection;
  HardwareTestCollectionResponse = _HardwareTestCollectionResponse;
  GetMACResponse = _GetMACResponse;
  ConcretePunchMessage = _ConcretePunchMessage;
  AbstractMemoryFormat = _AbstractMemoryFormat;
  BioPointMemoryFormat = _BioPointMemoryFormat;
  TemplateCollection = _TemplateCollection;
  AlarmRings = _AlarmRings;
  Credential = _Credential;
  Configuration = _Configuration;
  InvalidMessageException = _InvalidMessageException;
  FaceStatus = _FaceStatus;
  AbstractFactory = _AbstractFactory;
  HolidayCollection = _HolidayCollection;
  WeeklyJourneyWorking = _WeeklyJourneyWorking;
  ImmediateStatusResponse = _ImmediateStatusResponse;
  PrintPointMRPEventLog = _PrintPointMRPEventLog;
  Master = _Master;
  PrintPointEmployerMessage = _PrintPointEmployerMessage;
  MicropointCardList = _MicropointCardList;
  ShiftTableCollection = _ShiftTableCollection;
  MonthlyJourneyWorking = _MonthlyJourneyWorking;
  AlarmRingsCollection = _AlarmRingsCollection;
  SerialComm = _SerialComm;
  Card = _Card;
  BioPointFingerPrintMessage = _BioPointFingerPrintMessage;
  FaceLogRecord = _FaceLogRecord;


// *********************************************************************//
// Declaration of structures, unions and aliases.                         
// *********************************************************************//
  TypeActionFunction = packed record
    ActiveFunction: Integer;
    CheckList: Integer;
    TriggerOut: Integer;
    CheckJourney: Integer;
    RequestsMaster: Integer;
    RequesTypingKeyboard: Integer;
    StoresRecordBlocked: Integer;
    StoresRecordFreed: Integer;
  end;

  TypeCounter_Access = packed record
    Counter_Access1: Byte;
    Counter_Access2: Byte;
    Counter_Access3: Byte;
    Counter_Access4: Byte;
    Counter_Access5: Byte;
    Counter_Access6: Byte;
  end;


// *********************************************************************//
// Interface: _AbstractMessage
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {8DE1B541-F000-3238-B662-6D475C47FEDC}
// *********************************************************************//
  _AbstractMessage = interface(IDispatch)
    ['{8DE1B541-F000-3238-B662-6D475C47FEDC}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
  end;

// *********************************************************************//
// DispIntf:  _AbstractMessageDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {8DE1B541-F000-3238-B662-6D475C47FEDC}
// *********************************************************************//
  _AbstractMessageDisp = dispinterface
    ['{8DE1B541-F000-3238-B662-6D475C47FEDC}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
  end;

// *********************************************************************//
// Interface: IComm
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {92EEAAD7-2D59-3ADC-A67B-06C2A0C2C1C7}
// *********************************************************************//
  IComm = interface(IDispatch)
    ['{92EEAAD7-2D59-3ADC-A67B-06C2A0C2C1C7}']
    procedure Open; safecall;
    procedure Close; safecall;
    procedure Send(data: PSafeArray); safecall;
    function Receive(data: PSafeArray): Integer; safecall;
    procedure SetTimeOut(timeOut: Integer); safecall;
    procedure SetSendBufferSize(bufferSize: Int64); safecall;
    function GetTimeOut: LongWord; safecall;
    function Get_IPAddress: WideString; safecall;
    function Get_IPPort: Integer; safecall;
    function Get_ZkemkeeperObject: IZKEM; safecall;
    procedure _Set_ZkemkeeperObject(const pRetVal: IZKEM); safecall;
    function Get_Connected: WordBool; safecall;
    function Get_Zkemkeeper: WordBool; safecall;
    procedure Set_Zkemkeeper(pRetVal: WordBool); safecall;
    property IPAddress: WideString read Get_IPAddress;
    property IPPort: Integer read Get_IPPort;
    property ZkemkeeperObject: IZKEM read Get_ZkemkeeperObject write _Set_ZkemkeeperObject;
    property Connected: WordBool read Get_Connected;
    property Zkemkeeper: WordBool read Get_Zkemkeeper write Set_Zkemkeeper;
  end;

// *********************************************************************//
// DispIntf:  ICommDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {92EEAAD7-2D59-3ADC-A67B-06C2A0C2C1C7}
// *********************************************************************//
  ICommDisp = dispinterface
    ['{92EEAAD7-2D59-3ADC-A67B-06C2A0C2C1C7}']
    procedure Open; dispid 1610743808;
    procedure Close; dispid 1610743809;
    procedure Send(data: {??PSafeArray}OleVariant); dispid 1610743810;
    function Receive(data: {??PSafeArray}OleVariant): Integer; dispid 1610743811;
    procedure SetTimeOut(timeOut: Integer); dispid 1610743812;
    procedure SetSendBufferSize(bufferSize: {??Int64}OleVariant); dispid 1610743813;
    function GetTimeOut: LongWord; dispid 1610743814;
    property IPAddress: WideString readonly dispid 1610743815;
    property IPPort: Integer readonly dispid 1610743816;
    property ZkemkeeperObject: IZKEM dispid 1610743817;
    property Connected: WordBool readonly dispid 1610743819;
    property Zkemkeeper: WordBool dispid 1610743820;
  end;

// *********************************************************************//
// Interface: _TCPComm
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {66C6B10E-8FA5-3B10-B01F-3F7BF9697FF7}
// *********************************************************************//
  _TCPComm = interface(IDispatch)
    ['{66C6B10E-8FA5-3B10-B01F-3F7BF9697FF7}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Open; safecall;
    procedure Close; safecall;
    procedure Send(data: PSafeArray); safecall;
    function Receive(data: PSafeArray): Integer; safecall;
    procedure SetTimeOut(timeOut: Integer); safecall;
    procedure SetSendBufferSize(bufferSize: Int64); safecall;
    function GetTimeOut: LongWord; safecall;
    function Get_ZkemkeeperObject: IZKEM; safecall;
    procedure _Set_ZkemkeeperObject(const pRetVal: IZKEM); safecall;
    function Get_Zkemkeeper: WordBool; safecall;
    procedure Set_Zkemkeeper(pRetVal: WordBool); safecall;
    procedure CreateTcpComm(const ip: WideString; port: Integer); safecall;
    property ToString: WideString read Get_ToString;
    property ZkemkeeperObject: IZKEM read Get_ZkemkeeperObject write _Set_ZkemkeeperObject;
    property Zkemkeeper: WordBool read Get_Zkemkeeper write Set_Zkemkeeper;
  end;

// *********************************************************************//
// DispIntf:  _TCPCommDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {66C6B10E-8FA5-3B10-B01F-3F7BF9697FF7}
// *********************************************************************//
  _TCPCommDisp = dispinterface
    ['{66C6B10E-8FA5-3B10-B01F-3F7BF9697FF7}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    procedure Open; dispid 1610743812;
    procedure Close; dispid 1610743813;
    procedure Send(data: {??PSafeArray}OleVariant); dispid 1610743814;
    function Receive(data: {??PSafeArray}OleVariant): Integer; dispid 1610743815;
    procedure SetTimeOut(timeOut: Integer); dispid 1610743816;
    procedure SetSendBufferSize(bufferSize: {??Int64}OleVariant); dispid 1610743817;
    function GetTimeOut: LongWord; dispid 1610743818;
    property ZkemkeeperObject: IZKEM dispid 1610743819;
    property Zkemkeeper: WordBool dispid 1610743821;
    procedure CreateTcpComm(const ip: WideString; port: Integer); dispid 1610743823;
  end;

// *********************************************************************//
// Interface: _AbstractProtocol
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {E7B3D491-7351-33F9-AA10-49F8D712B86F}
// *********************************************************************//
  _AbstractProtocol = interface(IDispatch)
    ['{E7B3D491-7351-33F9-AA10-49F8D712B86F}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetStatus: _AbstractStatusMessage; safecall;
    function GetMAC: _GetMACResponse; safecall;
    function GetImmediateStatus: _ImmediateStatusResponse; safecall;
    function GetPrintPointStatus: _PrintPointStatusMessage; safecall;
    function GetPrintPointLiStatus: _PrintPointLiStatus; safecall;
    function GetFaceStatus: _FaceStatus; safecall;
    function CollectAll: _ArrayList; safecall;
    procedure GhostMethod__AbstractProtocol_72_1; safecall;
    procedure SetDST(dstBegin: TDateTime; dstEnd: TDateTime); safecall;
    procedure RemoveDST; safecall;
    procedure Set1ToN; safecall;
    procedure Set1To1; safecall;
    procedure GhostMethod__AbstractProtocol_92_2; safecall;
    procedure EmployeesTotalProgrammingBegin; safecall;
    procedure EmployeesTotalProgrammingEnd; safecall;
    procedure CleanCredentialsList; safecall;
    procedure CleanDisplayMessage; safecall;
    procedure CleanCostCenterList; safecall;
    procedure RepositioningMRPRecordsPointer; safecall;
    procedure RepositioningMRPRecordsPointer_2(date: TDateTime); safecall;
    procedure RepositioningMRPRecordsPointer_3(const nsr: WideString); safecall;
    procedure REPPlacesInMaintenance; safecall;
    procedure CleanEssentialVariables; safecall;
    procedure RebuildEmployeesTable; safecall;
    procedure HandlesFingerPrint(fingerPrintHandlesType: CredentialListActionType; 
                                 const pis: WideString; const fingerPrint: WideString; 
                                 fingerPrintTypeOne: EFingerPrintType; 
                                 fingerPrintTypeTwo: EFingerPrintType; sensor: EfingerPrintSensor); safecall;
    procedure HandlesFingerPrint_2(fingerPrintHandlesType: CredentialListActionType; 
                                   const pis: WideString; const fingerPrint: WideString; 
                                   fingerPrintHandOne: EFingerPrintHand; 
                                   fingerPrintHandTwo: EFingerPrintHand); safecall;
    procedure HandlesFingerPrint_3(fingerPrintHandlesType: CredentialListActionType; 
                                   employeeID: Integer; fingerPrintType: EFingerPrintType; 
                                   const fingerPrint: WideString); safecall;
    procedure ExcludeBioPointFingerPrint(const cardNumber: WideString); safecall;
    procedure IncludeBioPointFingerPrint(const cardNumber: WideString; 
                                         const fingerPrint: WideString; 
                                         fingerPrintTypeOne: EFingerPrintType; 
                                         fingerPrintTypeTwo: EFingerPrintType); safecall;
    procedure HandlesCostCenter(actionTypeList: CredentialListActionType; costCenterID: Integer; 
                                const costCenterDescription: WideString); safecall;
    procedure GhostMethod__AbstractProtocol_164_3; safecall;
    procedure CancelEmployeeTransmission; safecall;
    procedure GhostMethod__AbstractProtocol_172_4; safecall;
    procedure GhostMethod__AbstractProtocol_176_5; safecall;
    procedure GhostMethod__AbstractProtocol_180_6; safecall;
    procedure GhostMethod__AbstractProtocol_184_7; safecall;
    procedure CancelCredentialTransmission; safecall;
    procedure SendDisplayMessage(code: Smallint; const message: WideString); safecall;
    procedure GhostMethod__AbstractProtocol_196_8; safecall;
    procedure GhostMethod__AbstractProtocol_200_9; safecall;
    procedure GhostMethod__AbstractProtocol_204_10; safecall;
    procedure ChangeEmployer(employerType: EmployeerType; const cpf_cnpj: WideString; 
                             const cei: WideString; const name: WideString; 
                             const address: WideString); safecall;
    procedure ActivateBootLoader; safecall;
    procedure FirstActivation(const serialNumber: WideString; const password: WideString); safecall;
    procedure Activation(const password: WideString); safecall;
    procedure Activation_2; safecall;
    procedure ExchangeSealREP(const password: WideString); safecall;
    procedure SendMacAddress(const macAddressPart1: WideString; const macAddressPart2: WideString; 
                             const macAddressPart3: WideString; const macAddressPart4: WideString; 
                             const macAddressPart5: WideString; const macAddressPart6: WideString); safecall;
    function InquiryFingerPrint(InquiryFingerPrintType: InquiryFingerPrintType): _PrintPointFingerPrintMessage; safecall;
    procedure GhostMethod__AbstractProtocol_240_11; safecall;
    function InquiryBioPointFingerPrint: _BioPointFingerPrintMessage; safecall;
    procedure GhostMethod__AbstractProtocol_248_12; safecall;
    function InquiryPrintPointEvent: _PrintPointEvent; safecall;
    function ConfirmReceiptPrintPointEvent: _PrintPointEvent; safecall;
    function InquiryPrintPointMRPEventLog: _PrintPointMRPEventLog; safecall;
    function ConfirmationReceiptFingerPrint: _PrintPointFingerPrintMessage; safecall;
    function ConfirmationReceiptFingerPrint_2(totalPackets: Smallint; packetNumber: Smallint; 
                                              packetsCounter: Smallint): _BioPointFingerPrintMessage; safecall;
    procedure GhostMethod__AbstractProtocol_272_13; safecall;
    procedure GhostMethod__AbstractProtocol_276_14; safecall;
    function InquiryEmployeer: _PrintPointEmployerMessage; safecall;
    function InquiryRandomNumber: _RandomNumberResponse; safecall;
    procedure GhostMethod__AbstractProtocol_288_15; safecall;
    procedure GhostMethod__AbstractProtocol_292_16; safecall;
    function InquirySerialNumberOfREPAndMemory: _PrintPointInquirySerialNumberOfREPAndMemoryResponse; safecall;
    function InquirySerialNumber: WideString; safecall;
    procedure GhostMethod__AbstractProtocol_304_17; safecall;
    procedure GhostMethod__AbstractProtocol_308_18; safecall;
    procedure GhostMethod__AbstractProtocol_312_19; safecall;
    function InquiryMRPRecords_4(const startNSR: WideString; var errorOccurredInProcess: WordBool): PSafeArray; safecall;
    function InquiryFaceLogRecords: PSafeArray; safecall;
    procedure DeleteFaceLogRecords; safecall;
    function InquiryFaceTemplate(employeeID: Integer): WideString; safecall;
    procedure IncludeFaceTemplate(employeeID: Integer; const faceTemplate: WideString); safecall;
    procedure ExcludeFaceTemplate(employeeID: Integer); safecall;
    procedure CleanMasterList; safecall;
    procedure EnableLogDeniedAccess(enable: WordBool); safecall;
    procedure ExcludeFingerPrintList; safecall;
    procedure ExcludeFingerPrintWithoutEmployee; safecall;
    procedure ProgramLotterySampleRate(rate: Byte; inout: Byte); safecall;
    function GetCurrentPunch: _AbstractMessage; safecall;
    function RemoveCurrentPunch: _AbstractMessage; safecall;
    procedure ProgramTriggerType(type_: Byte; time: LongWord); safecall;
    function HardwareTestCollection(sequenceNumber: Smallint): _HardwareTestCollectionResponse; safecall;
    procedure ConfigureCard(idLength: LongWord; hasChecking: WordBool); safecall;
    procedure ConfigureCard_2(idLenghtMinimum: LongWord; idLenghtMaximum: LongWord; 
                              hasChecking: WordBool; way: WordBool); safecall;
    procedure Activation_3(active: WordBool; controlled: WordBool); safecall;
    procedure ConfigureMessage(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                               const Description: WideString); safecall;
    procedure ConfigureMessage_2(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                                 const Description: WideString; typeFunction: TypeActionFunction); safecall;
    procedure Master(Id: Int64; password: Integer; PenDriveProgramming: WordBool; 
                     DateTimeUpdated: WordBool; ProgrammingTechnical: WordBool); safecall;
    procedure setCardList(const myCard: _Card); safecall;
    procedure ClearProgramming(ClearProgramming: TypeClearProgramming); safecall;
    procedure setHoliday(day: Byte; month: Byte); safecall;
    procedure sendEmptyMessage; safecall;
    procedure SendSerialNumber(plateSerialNumber: Int64; mrpSerialNumber: Int64; 
                               mrpSealNumber: Int64); safecall;
    procedure SendSerialNumber_2(const serialNumber: WideString); safecall;
    procedure RemoveItemCardList(code: Int64); safecall;
    procedure UpdateShiftTable(const ShiftTableCollection: _ShiftTableCollection); safecall;
    procedure UpdateFirmware(const filePath: WideString); safecall;
    procedure AlarmRing(const AlarmRing: _AlarmRingsCollection); safecall;
    procedure setMemoryFormat(const MemoryFormat: _MemoryFormat); safecall;
    procedure setAlternativeCode(AlternativeCode: Integer; code: Int64); safecall;
    procedure removeAlternativeCode(AlternativeCode: Integer); safecall;
    procedure setJourneyWorking(const JourneyWorking: _JourneyWorking); safecall;
    procedure ClearData; safecall;
    function GetUnlockCode(const lockCode: WideString): WideString; safecall;
    function getCard: _CardCollection; safecall;
    procedure add_employeesListTransmissionProgress(const value: _EmployeesListTransmissionProgress); safecall;
    procedure remove_employeesListTransmissionProgress(const value: _EmployeesListTransmissionProgress); safecall;
    procedure add_credentialListTransmissionProgress(const value: _CredentialListTransmissionProgress); safecall;
    procedure remove_credentialListTransmissionProgress(const value: _CredentialListTransmissionProgress); safecall;
    function Get_Comm: IComm; safecall;
    function Get_WatchAddress: Integer; safecall;
    procedure Set_WatchAddress(pRetVal: Integer); safecall;
    function Get_type_: WatchProtocolType; safecall;
    function Get_TimeStampStart: Byte; safecall;
    procedure Set_TimeStampStart(pRetVal: Byte); safecall;
    property ToString: WideString read Get_ToString;
    property Comm: IComm read Get_Comm;
    property WatchAddress: Integer read Get_WatchAddress write Set_WatchAddress;
    property type_: WatchProtocolType read Get_type_;
    property TimeStampStart: Byte read Get_TimeStampStart write Set_TimeStampStart;
  end;

// *********************************************************************//
// DispIntf:  _AbstractProtocolDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {E7B3D491-7351-33F9-AA10-49F8D712B86F}
// *********************************************************************//
  _AbstractProtocolDisp = dispinterface
    ['{E7B3D491-7351-33F9-AA10-49F8D712B86F}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetStatus: _AbstractStatusMessage; dispid 1610743812;
    function GetMAC: _GetMACResponse; dispid 1610743813;
    function GetImmediateStatus: _ImmediateStatusResponse; dispid 1610743814;
    function GetPrintPointStatus: _PrintPointStatusMessage; dispid 1610743815;
    function GetPrintPointLiStatus: _PrintPointLiStatus; dispid 1610743816;
    function GetFaceStatus: _FaceStatus; dispid 1610743817;
    function CollectAll: _ArrayList; dispid 1610743818;
    procedure GhostMethod__AbstractProtocol_72_1; dispid 1610743819;
    procedure SetDST(dstBegin: TDateTime; dstEnd: TDateTime); dispid 1610743820;
    procedure RemoveDST; dispid 1610743821;
    procedure Set1ToN; dispid 1610743822;
    procedure Set1To1; dispid 1610743823;
    procedure GhostMethod__AbstractProtocol_92_2; dispid 1610743824;
    procedure EmployeesTotalProgrammingBegin; dispid 1610743825;
    procedure EmployeesTotalProgrammingEnd; dispid 1610743826;
    procedure CleanCredentialsList; dispid 1610743827;
    procedure CleanDisplayMessage; dispid 1610743828;
    procedure CleanCostCenterList; dispid 1610743829;
    procedure RepositioningMRPRecordsPointer; dispid 1610743830;
    procedure RepositioningMRPRecordsPointer_2(date: TDateTime); dispid 1610743831;
    procedure RepositioningMRPRecordsPointer_3(const nsr: WideString); dispid 1610743832;
    procedure REPPlacesInMaintenance; dispid 1610743833;
    procedure CleanEssentialVariables; dispid 1610743834;
    procedure RebuildEmployeesTable; dispid 1610743835;
    procedure HandlesFingerPrint(fingerPrintHandlesType: CredentialListActionType; 
                                 const pis: WideString; const fingerPrint: WideString; 
                                 fingerPrintTypeOne: EFingerPrintType; 
                                 fingerPrintTypeTwo: EFingerPrintType; sensor: EfingerPrintSensor); dispid 1610743836;
    procedure HandlesFingerPrint_2(fingerPrintHandlesType: CredentialListActionType; 
                                   const pis: WideString; const fingerPrint: WideString; 
                                   fingerPrintHandOne: EFingerPrintHand; 
                                   fingerPrintHandTwo: EFingerPrintHand); dispid 1610743837;
    procedure HandlesFingerPrint_3(fingerPrintHandlesType: CredentialListActionType; 
                                   employeeID: Integer; fingerPrintType: EFingerPrintType; 
                                   const fingerPrint: WideString); dispid 1610743838;
    procedure ExcludeBioPointFingerPrint(const cardNumber: WideString); dispid 1610743839;
    procedure IncludeBioPointFingerPrint(const cardNumber: WideString; 
                                         const fingerPrint: WideString; 
                                         fingerPrintTypeOne: EFingerPrintType; 
                                         fingerPrintTypeTwo: EFingerPrintType); dispid 1610743840;
    procedure HandlesCostCenter(actionTypeList: CredentialListActionType; costCenterID: Integer; 
                                const costCenterDescription: WideString); dispid 1610743841;
    procedure GhostMethod__AbstractProtocol_164_3; dispid 1610743842;
    procedure CancelEmployeeTransmission; dispid 1610743843;
    procedure GhostMethod__AbstractProtocol_172_4; dispid 1610743844;
    procedure GhostMethod__AbstractProtocol_176_5; dispid 1610743845;
    procedure GhostMethod__AbstractProtocol_180_6; dispid 1610743846;
    procedure GhostMethod__AbstractProtocol_184_7; dispid 1610743847;
    procedure CancelCredentialTransmission; dispid 1610743848;
    procedure SendDisplayMessage(code: Smallint; const message: WideString); dispid 1610743849;
    procedure GhostMethod__AbstractProtocol_196_8; dispid 1610743850;
    procedure GhostMethod__AbstractProtocol_200_9; dispid 1610743851;
    procedure GhostMethod__AbstractProtocol_204_10; dispid 1610743852;
    procedure ChangeEmployer(employerType: EmployeerType; const cpf_cnpj: WideString; 
                             const cei: WideString; const name: WideString; 
                             const address: WideString); dispid 1610743853;
    procedure ActivateBootLoader; dispid 1610743854;
    procedure FirstActivation(const serialNumber: WideString; const password: WideString); dispid 1610743855;
    procedure Activation(const password: WideString); dispid 1610743856;
    procedure Activation_2; dispid 1610743857;
    procedure ExchangeSealREP(const password: WideString); dispid 1610743858;
    procedure SendMacAddress(const macAddressPart1: WideString; const macAddressPart2: WideString; 
                             const macAddressPart3: WideString; const macAddressPart4: WideString; 
                             const macAddressPart5: WideString; const macAddressPart6: WideString); dispid 1610743859;
    function InquiryFingerPrint(InquiryFingerPrintType: InquiryFingerPrintType): _PrintPointFingerPrintMessage; dispid 1610743860;
    procedure GhostMethod__AbstractProtocol_240_11; dispid 1610743861;
    function InquiryBioPointFingerPrint: _BioPointFingerPrintMessage; dispid 1610743862;
    procedure GhostMethod__AbstractProtocol_248_12; dispid 1610743863;
    function InquiryPrintPointEvent: _PrintPointEvent; dispid 1610743864;
    function ConfirmReceiptPrintPointEvent: _PrintPointEvent; dispid 1610743865;
    function InquiryPrintPointMRPEventLog: _PrintPointMRPEventLog; dispid 1610743866;
    function ConfirmationReceiptFingerPrint: _PrintPointFingerPrintMessage; dispid 1610743867;
    function ConfirmationReceiptFingerPrint_2(totalPackets: Smallint; packetNumber: Smallint; 
                                              packetsCounter: Smallint): _BioPointFingerPrintMessage; dispid 1610743868;
    procedure GhostMethod__AbstractProtocol_272_13; dispid 1610743869;
    procedure GhostMethod__AbstractProtocol_276_14; dispid 1610743870;
    function InquiryEmployeer: _PrintPointEmployerMessage; dispid 1610743871;
    function InquiryRandomNumber: _RandomNumberResponse; dispid 1610743872;
    procedure GhostMethod__AbstractProtocol_288_15; dispid 1610743873;
    procedure GhostMethod__AbstractProtocol_292_16; dispid 1610743874;
    function InquirySerialNumberOfREPAndMemory: _PrintPointInquirySerialNumberOfREPAndMemoryResponse; dispid 1610743875;
    function InquirySerialNumber: WideString; dispid 1610743876;
    procedure GhostMethod__AbstractProtocol_304_17; dispid 1610743877;
    procedure GhostMethod__AbstractProtocol_308_18; dispid 1610743878;
    procedure GhostMethod__AbstractProtocol_312_19; dispid 1610743879;
    function InquiryMRPRecords_4(const startNSR: WideString; var errorOccurredInProcess: WordBool): {??PSafeArray}OleVariant; dispid 1610743880;
    function InquiryFaceLogRecords: {??PSafeArray}OleVariant; dispid 1610743881;
    procedure DeleteFaceLogRecords; dispid 1610743882;
    function InquiryFaceTemplate(employeeID: Integer): WideString; dispid 1610743883;
    procedure IncludeFaceTemplate(employeeID: Integer; const faceTemplate: WideString); dispid 1610743884;
    procedure ExcludeFaceTemplate(employeeID: Integer); dispid 1610743885;
    procedure CleanMasterList; dispid 1610743886;
    procedure EnableLogDeniedAccess(enable: WordBool); dispid 1610743887;
    procedure ExcludeFingerPrintList; dispid 1610743888;
    procedure ExcludeFingerPrintWithoutEmployee; dispid 1610743889;
    procedure ProgramLotterySampleRate(rate: Byte; inout: Byte); dispid 1610743890;
    function GetCurrentPunch: _AbstractMessage; dispid 1610743891;
    function RemoveCurrentPunch: _AbstractMessage; dispid 1610743892;
    procedure ProgramTriggerType(type_: Byte; time: LongWord); dispid 1610743893;
    function HardwareTestCollection(sequenceNumber: Smallint): _HardwareTestCollectionResponse; dispid 1610743894;
    procedure ConfigureCard(idLength: LongWord; hasChecking: WordBool); dispid 1610743895;
    procedure ConfigureCard_2(idLenghtMinimum: LongWord; idLenghtMaximum: LongWord; 
                              hasChecking: WordBool; way: WordBool); dispid 1610743896;
    procedure Activation_3(active: WordBool; controlled: WordBool); dispid 1610743897;
    procedure ConfigureMessage(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                               const Description: WideString); dispid 1610743898;
    procedure ConfigureMessage_2(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                                 const Description: WideString; 
                                 typeFunction: {??TypeActionFunction}OleVariant); dispid 1610743899;
    procedure Master(Id: {??Int64}OleVariant; password: Integer; PenDriveProgramming: WordBool; 
                     DateTimeUpdated: WordBool; ProgrammingTechnical: WordBool); dispid 1610743900;
    procedure setCardList(const myCard: _Card); dispid 1610743901;
    procedure ClearProgramming(ClearProgramming: TypeClearProgramming); dispid 1610743902;
    procedure setHoliday(day: Byte; month: Byte); dispid 1610743903;
    procedure sendEmptyMessage; dispid 1610743904;
    procedure SendSerialNumber(plateSerialNumber: {??Int64}OleVariant; 
                               mrpSerialNumber: {??Int64}OleVariant; 
                               mrpSealNumber: {??Int64}OleVariant); dispid 1610743905;
    procedure SendSerialNumber_2(const serialNumber: WideString); dispid 1610743906;
    procedure RemoveItemCardList(code: {??Int64}OleVariant); dispid 1610743907;
    procedure UpdateShiftTable(const ShiftTableCollection: _ShiftTableCollection); dispid 1610743908;
    procedure UpdateFirmware(const filePath: WideString); dispid 1610743909;
    procedure AlarmRing(const AlarmRing: _AlarmRingsCollection); dispid 1610743910;
    procedure setMemoryFormat(const MemoryFormat: _MemoryFormat); dispid 1610743911;
    procedure setAlternativeCode(AlternativeCode: Integer; code: {??Int64}OleVariant); dispid 1610743912;
    procedure removeAlternativeCode(AlternativeCode: Integer); dispid 1610743913;
    procedure setJourneyWorking(const JourneyWorking: _JourneyWorking); dispid 1610743914;
    procedure ClearData; dispid 1610743915;
    function GetUnlockCode(const lockCode: WideString): WideString; dispid 1610743916;
    function getCard: _CardCollection; dispid 1610743917;
    procedure add_employeesListTransmissionProgress(const value: _EmployeesListTransmissionProgress); dispid 1610743918;
    procedure remove_employeesListTransmissionProgress(const value: _EmployeesListTransmissionProgress); dispid 1610743919;
    procedure add_credentialListTransmissionProgress(const value: _CredentialListTransmissionProgress); dispid 1610743920;
    procedure remove_credentialListTransmissionProgress(const value: _CredentialListTransmissionProgress); dispid 1610743921;
    property Comm: IComm readonly dispid 1610743922;
    property WatchAddress: Integer dispid 1610743923;
    property type_: WatchProtocolType readonly dispid 1610743925;
    property TimeStampStart: Byte dispid 1610743926;
  end;

// *********************************************************************//
// Interface: _EmployeesListTransmissionProgress
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {1EFC1A17-A151-376C-9555-F04B47FF4C17}
// *********************************************************************//
  _EmployeesListTransmissionProgress = interface(IDispatch)
    ['{1EFC1A17-A151-376C-9555-F04B47FF4C17}']
  end;

// *********************************************************************//
// DispIntf:  _EmployeesListTransmissionProgressDisp
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {1EFC1A17-A151-376C-9555-F04B47FF4C17}
// *********************************************************************//
  _EmployeesListTransmissionProgressDisp = dispinterface
    ['{1EFC1A17-A151-376C-9555-F04B47FF4C17}']
  end;

// *********************************************************************//
// Interface: _CredentialListTransmissionProgress
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {2C153EBC-1F13-309E-80FE-B44D73F25E8C}
// *********************************************************************//
  _CredentialListTransmissionProgress = interface(IDispatch)
    ['{2C153EBC-1F13-309E-80FE-B44D73F25E8C}']
  end;

// *********************************************************************//
// DispIntf:  _CredentialListTransmissionProgressDisp
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {2C153EBC-1F13-309E-80FE-B44D73F25E8C}
// *********************************************************************//
  _CredentialListTransmissionProgressDisp = dispinterface
    ['{2C153EBC-1F13-309E-80FE-B44D73F25E8C}']
  end;

// *********************************************************************//
// Interface: _PrintPointInquirySerialNumberOfREPAndMemoryResponse
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {FF3A73D0-3C44-3133-B01B-47B036BC5F09}
// *********************************************************************//
  _PrintPointInquirySerialNumberOfREPAndMemoryResponse = interface(IDispatch)
    ['{FF3A73D0-3C44-3133-B01B-47B036BC5F09}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_serialNumber: WideString; safecall;
    function Get_SerialNumberPlate: WideString; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property serialNumber: WideString read Get_serialNumber;
    property SerialNumberPlate: WideString read Get_SerialNumberPlate;
  end;

// *********************************************************************//
// DispIntf:  _PrintPointInquirySerialNumberOfREPAndMemoryResponseDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {FF3A73D0-3C44-3133-B01B-47B036BC5F09}
// *********************************************************************//
  _PrintPointInquirySerialNumberOfREPAndMemoryResponseDisp = dispinterface
    ['{FF3A73D0-3C44-3133-B01B-47B036BC5F09}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property serialNumber: WideString readonly dispid 1610743816;
    property SerialNumberPlate: WideString readonly dispid 1610743817;
  end;

// *********************************************************************//
// Interface: _PrintPointLiFingerPrint
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {A6902D61-90B9-391D-99C8-59CE92C509A5}
// *********************************************************************//
  _PrintPointLiFingerPrint = interface(IDispatch)
    ['{A6902D61-90B9-391D-99C8-59CE92C509A5}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_fingerPrintType: EFingerPrintType; safecall;
    procedure Set_fingerPrintType(pRetVal: EFingerPrintType); safecall;
    function Get_fingerPrint: WideString; safecall;
    procedure Set_fingerPrint(const pRetVal: WideString); safecall;
    function Get_employeeID: Integer; safecall;
    procedure Set_employeeID(pRetVal: Integer); safecall;
    property ToString: WideString read Get_ToString;
    property fingerPrintType: EFingerPrintType read Get_fingerPrintType write Set_fingerPrintType;
    property fingerPrint: WideString read Get_fingerPrint write Set_fingerPrint;
    property employeeID: Integer read Get_employeeID write Set_employeeID;
  end;

// *********************************************************************//
// DispIntf:  _PrintPointLiFingerPrintDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {A6902D61-90B9-391D-99C8-59CE92C509A5}
// *********************************************************************//
  _PrintPointLiFingerPrintDisp = dispinterface
    ['{A6902D61-90B9-391D-99C8-59CE92C509A5}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property fingerPrintType: EFingerPrintType dispid 1610743812;
    property fingerPrint: WideString dispid 1610743814;
    property employeeID: Integer dispid 1610743816;
  end;

// *********************************************************************//
// Interface: _PrintPointEmployee
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {ADFC1D78-5667-3A3A-8CE8-E3FAF826B02B}
// *********************************************************************//
  _PrintPointEmployee = interface(IDispatch)
    ['{ADFC1D78-5667-3A3A-8CE8-E3FAF826B02B}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function CompareTo(obj: OleVariant): Integer; safecall;
    function Get_pis: WideString; safecall;
    procedure Set_pis(const pRetVal: WideString); safecall;
    function Get_name: WideString; safecall;
    procedure Set_name(const pRetVal: WideString); safecall;
    function Get_password: WideString; safecall;
    procedure Set_password(const pRetVal: WideString); safecall;
    function Get_CostCenter: Integer; safecall;
    procedure Set_CostCenter(pRetVal: Integer); safecall;
    property ToString: WideString read Get_ToString;
    property pis: WideString read Get_pis write Set_pis;
    property name: WideString read Get_name write Set_name;
    property password: WideString read Get_password write Set_password;
    property CostCenter: Integer read Get_CostCenter write Set_CostCenter;
  end;

// *********************************************************************//
// DispIntf:  _PrintPointEmployeeDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {ADFC1D78-5667-3A3A-8CE8-E3FAF826B02B}
// *********************************************************************//
  _PrintPointEmployeeDisp = dispinterface
    ['{ADFC1D78-5667-3A3A-8CE8-E3FAF826B02B}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function CompareTo(obj: OleVariant): Integer; dispid 1610743812;
    property pis: WideString dispid 1610743813;
    property name: WideString dispid 1610743815;
    property password: WideString dispid 1610743817;
    property CostCenter: Integer dispid 1610743819;
  end;

// *********************************************************************//
// Interface: _DeviceConnectionException
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {E12820AE-D496-3FB7-BE6E-B648AF65C4B2}
// *********************************************************************//
  _DeviceConnectionException = interface(IDispatch)
    ['{E12820AE-D496-3FB7-BE6E-B648AF65C4B2}']
  end;

// *********************************************************************//
// DispIntf:  _DeviceConnectionExceptionDisp
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {E12820AE-D496-3FB7-BE6E-B648AF65C4B2}
// *********************************************************************//
  _DeviceConnectionExceptionDisp = dispinterface
    ['{E12820AE-D496-3FB7-BE6E-B648AF65C4B2}']
  end;

// *********************************************************************//
// Interface: IWatchComm
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {861940A2-FC90-35BD-ADDA-5F8672AD4A46}
// *********************************************************************//
  IWatchComm = interface(IDispatch)
    ['{861940A2-FC90-35BD-ADDA-5F8672AD4A46}']
    procedure AddConfiguration(configurationType: EConfigurationType; field1: OleVariant); safecall;
    procedure AddConfiguration_2(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant); safecall;
    procedure AddConfiguration_3(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant); safecall;
    procedure AddConfiguration_4(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant; field4: OleVariant); safecall;
    procedure AddConfiguration_5(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant; field4: OleVariant; 
                                 field5: OleVariant); safecall;
    procedure AddParcialConfiguration(ParcialConfigurationType: ParcialConfigurationType; 
                                      field1: OleVariant); safecall;
    procedure AddParcialConfiguration_2(ParcialConfigurationType: ParcialConfigurationType; 
                                        field1: OleVariant; field2: OleVariant); safecall;
    procedure AddMaster(const pis: WideString; const Card: WideString; const password: WideString; 
                        hasTechniquesProgrammingPermission: WordBool; 
                        hasDataAndTimePermission: WordBool; 
                        hasPenDriveProgrammingPermisision: WordBool); safecall;
    procedure AddMaster_2(const pis: WideString; const Card: WideString; 
                          const password: WideString; hasTechniquesProgrammingPermission: WordBool; 
                          hasDataAndTimePermission: WordBool; 
                          hasPenDriveProgrammingPermisision: WordBool; 
                          hasBobbinChangePermisision: WordBool); safecall;
    procedure AddCredential(const cardCode: WideString; const pis: WideString; version: Byte); safecall;
    procedure AddEmployee(const pis: WideString); safecall;
    procedure AddEmployee_2(const pis: WideString; const name: WideString; 
                            const password: WideString); safecall;
    procedure AddEmployee_3(const pis: WideString; const name: WideString; 
                            const password: WideString; CostCenter: Integer); safecall;
    procedure AddEmployee_4(Id: Integer); safecall;
    procedure AddEmployee_5(Id: Integer; const pis: WideString; const cpf: WideString; 
                            const Credential: WideString; const name: WideString; 
                            const password: WideString; isMaster: WordBool); safecall;
    procedure AddEmployee_6(employeeID: Integer; const Credential: WideString; 
                            const name: WideString; const password: WideString; isMaster: WordBool); safecall;
    procedure CreateWatchComm(protocolType: WatchProtocolType; const Comm: IComm; 
                              WatchAddress: Integer; const firmwareVersion: WideString); safecall;
    procedure CreateWatchComm_2(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; connectionType: WatchConnectionType; 
                                const firmwareVersion: WideString); safecall;
    procedure CreateWatchComm_3(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; const accessKey: WideString; 
                                const firmwareVersion: WideString); safecall;
    procedure CreateWatchComm_4(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; const accessKey: WideString; 
                                connectionType: WatchConnectionType; 
                                const firmwareVersion: WideString); safecall;
    procedure OpenConnection; safecall;
    procedure CloseConnection; safecall;
    function GetStatus: _AbstractStatusMessage; safecall;
    function GetMAC: _GetMACResponse; safecall;
    function GetImmediateStatus: _ImmediateStatusResponse; safecall;
    function GetPrintPointStatus: _PrintPointStatusMessage; safecall;
    function GetPrintPointLiStatus: _PrintPointLiStatus; safecall;
    function GetFaceStatus: _FaceStatus; safecall;
    function CollectAll: _ArrayList; safecall;
    function GetCurrentPunch: _AbstractMessage; safecall;
    function RemoveCurrentPunch: _AbstractMessage; safecall;
    procedure GhostMethod_IWatchComm_152_1; safecall;
    procedure SetDST(dstBegin: TDateTime; dstEnd: TDateTime); safecall;
    procedure RemoveDST; safecall;
    procedure Set1ToN; safecall;
    procedure Set1To1; safecall;
    procedure SetDateTimeAndDST_2(date: TDateTime; dstBegin: TDateTime; dstEnd: TDateTime); safecall;
    procedure SetDateTime(date: TDateTime); safecall;
    procedure EmployeesTotalProgrammingBegin; safecall;
    procedure EmployeesTotalProgrammingEnd; safecall;
    procedure ClearMasterList; safecall;
    procedure ClearClockCredentialsList; safecall;
    procedure ClearDisplayMessage; safecall;
    procedure ClearCostCenterList; safecall;
    procedure EnableLogDeniedAccess(enable: WordBool); safecall;
    procedure ProgramLotterySampleRate(rate: Byte; inout: Byte); safecall;
    procedure ProgramTriggerType(type_: Byte; time: Integer); safecall;
    function HardwareTestCollection: PSafeArray; safecall;
    procedure ConfigureCard(idLength: Integer; hasChecking: WordBool); safecall;
    procedure ConfigureCard_2(idLenghtMinimum: Integer; idLenghtMaximum: Integer; 
                              hasChecking: WordBool; way: WordBool); safecall;
    procedure Activation(active: WordBool; controlled: WordBool); safecall;
    procedure setCardList(const CardCollection: _CardCollection); safecall;
    procedure ConfigureMessage(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                               const Description: WideString); safecall;
    procedure ConfigureMessage_2(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                                 const Description: WideString; typeFunction: TypeActionFunction); safecall;
    procedure Master(Id: Integer; password: Integer; PenDriveProgramming: WordBool; 
                     DateTimeUpdated: WordBool; ProgrammingTechnical: WordBool); safecall;
    procedure ClearProgramming(ClearProgramming: TypeClearProgramming); safecall;
    procedure RepositioningMRPRecordsPointer; safecall;
    procedure REPPlacesInMaintenance; safecall;
    procedure CleanEssentialVariables; safecall;
    procedure RebuildEmployeesTable; safecall;
    procedure IncludeFingerPrint(const pis: WideString; const fingerPrint: WideString; 
                                 fingerPrintTypeOne: EFingerPrintType; 
                                 fingerPrintTypeTwo: EFingerPrintType; sensor: EfingerPrintSensor); safecall;
    procedure IncludeFingerPrint_2(const pis: WideString; const fingerPrint: WideString; 
                                   fingerPrintHandOne: EFingerPrintHand; 
                                   fingerPrintHandTwo: EFingerPrintHand); safecall;
    procedure IncludeFingerPrint_3(employeeID: Integer; fingerPrintType: EFingerPrintType; 
                                   const fingerPrint: WideString); safecall;
    procedure IncludeCostCenter(costCenterID: Integer; const costCenterDescription: WideString); safecall;
    procedure IncludeCredentialList(usesVersion: WordBool); safecall;
    procedure IncludeEmployeesList(usesPassword: WordBool; isTotalProgramming: WordBool); safecall;
    procedure IncludeEmployeesListWithCostCenter; safecall;
    procedure IncludeEmployeesList_2; safecall;
    procedure SendDisplayMessage(code: Smallint; const message: WideString); safecall;
    procedure SendSettings; safecall;
    procedure SendParcialSettings; safecall;
    procedure SendMasterList; safecall;
    procedure ChangeEmployer(employerType: EmployeerType; const cpf_cnpj: WideString; 
                             const cei: WideString; const name: WideString; 
                             const address: WideString); safecall;
    procedure ActivateBootLoader; safecall;
    procedure FirstActivation(const serialNumber: WideString; const password: WideString); safecall;
    procedure Activation_2(const password: WideString); safecall;
    procedure Activation_3; safecall;
    procedure ExchangeSealREP(const password: WideString); safecall;
    procedure SendMacAddress(const macAddressPart1: WideString; const macAddressPart2: WideString; 
                             const macAddressPart3: WideString; const macAddressPart4: WideString; 
                             const macAddressPart5: WideString; const macAddressPart6: WideString); safecall;
    function InquiryFingerPrint(InquiryFingerPrintType: InquiryFingerPrintType): _PrintPointFingerPrintMessage; safecall;
    function InquiryFingerPrint_2(employeeID: Integer): PSafeArray; safecall;
    function InquiryBioPointFingerPrint: _BioPointFingerPrintMessage; safecall;
    function InquiryFaceFingerPrint(employeeID: Integer): PSafeArray; safecall;
    function InquiryPrintPointEvent: _PrintPointEvent; safecall;
    function ConfirmReceiptPrintPointEvent: _PrintPointEvent; safecall;
    function InquiryPrintPointMRPEventLog: _PrintPointMRPEventLog; safecall;
    function ConfirmationReceiptFingerPrint: _PrintPointFingerPrintMessage; safecall;
    function ConfirmationReceiptBioPointFingerPrint: _BioPointFingerPrintMessage; safecall;
    function ConfirmationReceiptMRPRecords: PSafeArray; safecall;
    function ConfirmationReceiptEmployeeList: PSafeArray; safecall;
    function InquiryEmployeer: _PrintPointEmployerMessage; safecall;
    function InquirySerialNumberOfREPAndMemory: _PrintPointInquirySerialNumberOfREPAndMemoryResponse; safecall;
    function InquirySerialNumber: WideString; safecall;
    function InquiryMRPRecords(inquiryChangeEmployed: WordBool; 
                               inquirySettingRealTimeClock: WordBool; 
                               inquiryRegistrationMarkingPoint: WordBool; 
                               inquiryChangeCompanyIdentification: WordBool): PSafeArray; safecall;
    function InquiryMRPRecords_2(startDate: TDateTime; inquiryChangeEmployed: WordBool; 
                                 inquirySettingRealTimeClock: WordBool; 
                                 inquiryRegistrationMarkingPoint: WordBool; 
                                 inquiryChangeCompanyIdentification: WordBool): PSafeArray; safecall;
    function InquiryMRPRecords_3(const startNSR: WideString; inquiryChangeEmployed: WordBool; 
                                 inquirySettingRealTimeClock: WordBool; 
                                 inquiryRegistrationMarkingPoint: WordBool; 
                                 inquiryChangeCompanyIdentification: WordBool): PSafeArray; safecall;
    function InquiryMRPRecords_4(const startNSR: WideString; var errorOccurredInProcess: WordBool): PSafeArray; safecall;
    function InquiryFaceLogRecords: PSafeArray; safecall;
    procedure DeleteFaceLogRecords; safecall;
    function InquiryFaceTemplate(employeeID: Integer): WideString; safecall;
    procedure IncludeFaceTemplate(employeeID: Integer; const faceTemplate: WideString); safecall;
    procedure ExcludeFaceTemplate(employeeID: Integer); safecall;
    function InquiryRandomNumber: _RandomNumberResponse; safecall;
    function InquiryEmployeeList: PSafeArray; safecall;
    function InquiryFaceEmployeeList: PSafeArray; safecall;
    procedure ExcludeFingerPrint(const pis: WideString); safecall;
    procedure ExcludeFingerPrint_2(employeeID: Integer; fingerPrintType: EFingerPrintType); safecall;
    procedure ExcludeCostCenter(costCenterID: Integer); safecall;
    procedure ExcludeBioPointFingerPrint(const cardNumber: WideString); safecall;
    procedure IncludeBioPointFingerPrint(const cardNumber: WideString; 
                                         const fingerPrint: WideString; 
                                         fingerPrintTypeOne: EFingerPrintType; 
                                         fingerPrintTypeTwo: EFingerPrintType); safecall;
    procedure ExcludeCredentialList; safecall;
    procedure ExcludeEmployeesList; safecall;
    procedure ExcludeEmployeesListWithCostNumber; safecall;
    procedure RepositioningMRPRecordsPointer_2(date: TDateTime); safecall;
    procedure RepositioningMRPRecordsPointer_3(const nsr: WideString); safecall;
    procedure setHoliday(day: Byte; month: Byte); safecall;
    procedure setHoliday_2(const HolidayCollection: _HolidayCollection); safecall;
    procedure sendEmptyMessage; safecall;
    procedure SendSerialNumber(plateSerialNumber: Int64; mrpSerialNumber: Int64; 
                               mrpSealNumber: Int64); safecall;
    procedure SendSerialNumber_2(const serialNumber: WideString); safecall;
    procedure RemoveItemCardList(code: Integer); safecall;
    procedure UpdateShiftTable(const ShiftTableCollection: _ShiftTableCollection); safecall;
    procedure UpdateFirmware(const filePath: WideString); safecall;
    procedure AlarmRing(const AlarmRing: _AlarmRingsCollection); safecall;
    procedure setMemoryFormat(const MemoryFormat: _MemoryFormat); safecall;
    procedure setAlternativeCode(AlternativeCode: Integer; const code: WideString); safecall;
    procedure removeAlternativeCode(AlternativeCode: Integer); safecall;
    procedure setJourneyWorking(const JourneyWorking: _JourneyWorking); safecall;
    procedure ClearData; safecall;
    function GetUnlockCode(const lockCode: WideString): WideString; safecall;
    procedure CancelEmployeeTransmission; safecall;
    procedure CancelCredentialTransmission; safecall;
    function Get_Connected: WordBool; safecall;
    procedure ClearCredencialList; safecall;
    procedure ClearEmployeeList; safecall;
    property Connected: WordBool read Get_Connected;
  end;

// *********************************************************************//
// DispIntf:  IWatchCommDisp
// Flags:     (4416) Dual OleAutomation Dispatchable
// GUID:      {861940A2-FC90-35BD-ADDA-5F8672AD4A46}
// *********************************************************************//
  IWatchCommDisp = dispinterface
    ['{861940A2-FC90-35BD-ADDA-5F8672AD4A46}']
    procedure AddConfiguration(configurationType: EConfigurationType; field1: OleVariant); dispid 1610743808;
    procedure AddConfiguration_2(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant); dispid 1610743809;
    procedure AddConfiguration_3(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant); dispid 1610743810;
    procedure AddConfiguration_4(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant; field4: OleVariant); dispid 1610743811;
    procedure AddConfiguration_5(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant; field4: OleVariant; 
                                 field5: OleVariant); dispid 1610743812;
    procedure AddParcialConfiguration(ParcialConfigurationType: ParcialConfigurationType; 
                                      field1: OleVariant); dispid 1610743813;
    procedure AddParcialConfiguration_2(ParcialConfigurationType: ParcialConfigurationType; 
                                        field1: OleVariant; field2: OleVariant); dispid 1610743814;
    procedure AddMaster(const pis: WideString; const Card: WideString; const password: WideString; 
                        hasTechniquesProgrammingPermission: WordBool; 
                        hasDataAndTimePermission: WordBool; 
                        hasPenDriveProgrammingPermisision: WordBool); dispid 1610743815;
    procedure AddMaster_2(const pis: WideString; const Card: WideString; 
                          const password: WideString; hasTechniquesProgrammingPermission: WordBool; 
                          hasDataAndTimePermission: WordBool; 
                          hasPenDriveProgrammingPermisision: WordBool; 
                          hasBobbinChangePermisision: WordBool); dispid 1610743816;
    procedure AddCredential(const cardCode: WideString; const pis: WideString; version: Byte); dispid 1610743817;
    procedure AddEmployee(const pis: WideString); dispid 1610743818;
    procedure AddEmployee_2(const pis: WideString; const name: WideString; 
                            const password: WideString); dispid 1610743819;
    procedure AddEmployee_3(const pis: WideString; const name: WideString; 
                            const password: WideString; CostCenter: Integer); dispid 1610743820;
    procedure AddEmployee_4(Id: Integer); dispid 1610743821;
    procedure AddEmployee_5(Id: Integer; const pis: WideString; const cpf: WideString; 
                            const Credential: WideString; const name: WideString; 
                            const password: WideString; isMaster: WordBool); dispid 1610743822;
    procedure AddEmployee_6(employeeID: Integer; const Credential: WideString; 
                            const name: WideString; const password: WideString; isMaster: WordBool); dispid 1610743823;
    procedure CreateWatchComm(protocolType: WatchProtocolType; const Comm: IComm; 
                              WatchAddress: Integer; const firmwareVersion: WideString); dispid 1610743824;
    procedure CreateWatchComm_2(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; connectionType: WatchConnectionType; 
                                const firmwareVersion: WideString); dispid 1610743825;
    procedure CreateWatchComm_3(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; const accessKey: WideString; 
                                const firmwareVersion: WideString); dispid 1610743826;
    procedure CreateWatchComm_4(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; const accessKey: WideString; 
                                connectionType: WatchConnectionType; 
                                const firmwareVersion: WideString); dispid 1610743827;
    procedure OpenConnection; dispid 1610743828;
    procedure CloseConnection; dispid 1610743829;
    function GetStatus: _AbstractStatusMessage; dispid 1610743830;
    function GetMAC: _GetMACResponse; dispid 1610743831;
    function GetImmediateStatus: _ImmediateStatusResponse; dispid 1610743832;
    function GetPrintPointStatus: _PrintPointStatusMessage; dispid 1610743833;
    function GetPrintPointLiStatus: _PrintPointLiStatus; dispid 1610743834;
    function GetFaceStatus: _FaceStatus; dispid 1610743835;
    function CollectAll: _ArrayList; dispid 1610743836;
    function GetCurrentPunch: _AbstractMessage; dispid 1610743837;
    function RemoveCurrentPunch: _AbstractMessage; dispid 1610743838;
    procedure GhostMethod_IWatchComm_152_1; dispid 1610743839;
    procedure SetDST(dstBegin: TDateTime; dstEnd: TDateTime); dispid 1610743840;
    procedure RemoveDST; dispid 1610743841;
    procedure Set1ToN; dispid 1610743842;
    procedure Set1To1; dispid 1610743843;
    procedure SetDateTimeAndDST_2(date: TDateTime; dstBegin: TDateTime; dstEnd: TDateTime); dispid 1610743844;
    procedure SetDateTime(date: TDateTime); dispid 1610743845;
    procedure EmployeesTotalProgrammingBegin; dispid 1610743846;
    procedure EmployeesTotalProgrammingEnd; dispid 1610743847;
    procedure ClearMasterList; dispid 1610743848;
    procedure ClearClockCredentialsList; dispid 1610743849;
    procedure ClearDisplayMessage; dispid 1610743850;
    procedure ClearCostCenterList; dispid 1610743851;
    procedure EnableLogDeniedAccess(enable: WordBool); dispid 1610743852;
    procedure ProgramLotterySampleRate(rate: Byte; inout: Byte); dispid 1610743853;
    procedure ProgramTriggerType(type_: Byte; time: Integer); dispid 1610743854;
    function HardwareTestCollection: {??PSafeArray}OleVariant; dispid 1610743855;
    procedure ConfigureCard(idLength: Integer; hasChecking: WordBool); dispid 1610743856;
    procedure ConfigureCard_2(idLenghtMinimum: Integer; idLenghtMaximum: Integer; 
                              hasChecking: WordBool; way: WordBool); dispid 1610743857;
    procedure Activation(active: WordBool; controlled: WordBool); dispid 1610743858;
    procedure setCardList(const CardCollection: _CardCollection); dispid 1610743859;
    procedure ConfigureMessage(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                               const Description: WideString); dispid 1610743860;
    procedure ConfigureMessage_2(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                                 const Description: WideString; 
                                 typeFunction: {??TypeActionFunction}OleVariant); dispid 1610743861;
    procedure Master(Id: Integer; password: Integer; PenDriveProgramming: WordBool; 
                     DateTimeUpdated: WordBool; ProgrammingTechnical: WordBool); dispid 1610743862;
    procedure ClearProgramming(ClearProgramming: TypeClearProgramming); dispid 1610743863;
    procedure RepositioningMRPRecordsPointer; dispid 1610743864;
    procedure REPPlacesInMaintenance; dispid 1610743865;
    procedure CleanEssentialVariables; dispid 1610743866;
    procedure RebuildEmployeesTable; dispid 1610743867;
    procedure IncludeFingerPrint(const pis: WideString; const fingerPrint: WideString; 
                                 fingerPrintTypeOne: EFingerPrintType; 
                                 fingerPrintTypeTwo: EFingerPrintType; sensor: EfingerPrintSensor); dispid 1610743868;
    procedure IncludeFingerPrint_2(const pis: WideString; const fingerPrint: WideString; 
                                   fingerPrintHandOne: EFingerPrintHand; 
                                   fingerPrintHandTwo: EFingerPrintHand); dispid 1610743869;
    procedure IncludeFingerPrint_3(employeeID: Integer; fingerPrintType: EFingerPrintType; 
                                   const fingerPrint: WideString); dispid 1610743870;
    procedure IncludeCostCenter(costCenterID: Integer; const costCenterDescription: WideString); dispid 1610743871;
    procedure IncludeCredentialList(usesVersion: WordBool); dispid 1610743872;
    procedure IncludeEmployeesList(usesPassword: WordBool; isTotalProgramming: WordBool); dispid 1610743873;
    procedure IncludeEmployeesListWithCostCenter; dispid 1610743874;
    procedure IncludeEmployeesList_2; dispid 1610743875;
    procedure SendDisplayMessage(code: Smallint; const message: WideString); dispid 1610743876;
    procedure SendSettings; dispid 1610743877;
    procedure SendParcialSettings; dispid 1610743878;
    procedure SendMasterList; dispid 1610743879;
    procedure ChangeEmployer(employerType: EmployeerType; const cpf_cnpj: WideString; 
                             const cei: WideString; const name: WideString; 
                             const address: WideString); dispid 1610743880;
    procedure ActivateBootLoader; dispid 1610743881;
    procedure FirstActivation(const serialNumber: WideString; const password: WideString); dispid 1610743882;
    procedure Activation_2(const password: WideString); dispid 1610743883;
    procedure Activation_3; dispid 1610743884;
    procedure ExchangeSealREP(const password: WideString); dispid 1610743885;
    procedure SendMacAddress(const macAddressPart1: WideString; const macAddressPart2: WideString; 
                             const macAddressPart3: WideString; const macAddressPart4: WideString; 
                             const macAddressPart5: WideString; const macAddressPart6: WideString); dispid 1610743886;
    function InquiryFingerPrint(InquiryFingerPrintType: InquiryFingerPrintType): _PrintPointFingerPrintMessage; dispid 1610743887;
    function InquiryFingerPrint_2(employeeID: Integer): {??PSafeArray}OleVariant; dispid 1610743888;
    function InquiryBioPointFingerPrint: _BioPointFingerPrintMessage; dispid 1610743889;
    function InquiryFaceFingerPrint(employeeID: Integer): {??PSafeArray}OleVariant; dispid 1610743890;
    function InquiryPrintPointEvent: _PrintPointEvent; dispid 1610743891;
    function ConfirmReceiptPrintPointEvent: _PrintPointEvent; dispid 1610743892;
    function InquiryPrintPointMRPEventLog: _PrintPointMRPEventLog; dispid 1610743893;
    function ConfirmationReceiptFingerPrint: _PrintPointFingerPrintMessage; dispid 1610743894;
    function ConfirmationReceiptBioPointFingerPrint: _BioPointFingerPrintMessage; dispid 1610743895;
    function ConfirmationReceiptMRPRecords: {??PSafeArray}OleVariant; dispid 1610743896;
    function ConfirmationReceiptEmployeeList: {??PSafeArray}OleVariant; dispid 1610743897;
    function InquiryEmployeer: _PrintPointEmployerMessage; dispid 1610743898;
    function InquirySerialNumberOfREPAndMemory: _PrintPointInquirySerialNumberOfREPAndMemoryResponse; dispid 1610743899;
    function InquirySerialNumber: WideString; dispid 1610743900;
    function InquiryMRPRecords(inquiryChangeEmployed: WordBool; 
                               inquirySettingRealTimeClock: WordBool; 
                               inquiryRegistrationMarkingPoint: WordBool; 
                               inquiryChangeCompanyIdentification: WordBool): {??PSafeArray}OleVariant; dispid 1610743901;
    function InquiryMRPRecords_2(startDate: TDateTime; inquiryChangeEmployed: WordBool; 
                                 inquirySettingRealTimeClock: WordBool; 
                                 inquiryRegistrationMarkingPoint: WordBool; 
                                 inquiryChangeCompanyIdentification: WordBool): {??PSafeArray}OleVariant; dispid 1610743902;
    function InquiryMRPRecords_3(const startNSR: WideString; inquiryChangeEmployed: WordBool; 
                                 inquirySettingRealTimeClock: WordBool; 
                                 inquiryRegistrationMarkingPoint: WordBool; 
                                 inquiryChangeCompanyIdentification: WordBool): {??PSafeArray}OleVariant; dispid 1610743903;
    function InquiryMRPRecords_4(const startNSR: WideString; var errorOccurredInProcess: WordBool): {??PSafeArray}OleVariant; dispid 1610743904;
    function InquiryFaceLogRecords: {??PSafeArray}OleVariant; dispid 1610743905;
    procedure DeleteFaceLogRecords; dispid 1610743906;
    function InquiryFaceTemplate(employeeID: Integer): WideString; dispid 1610743907;
    procedure IncludeFaceTemplate(employeeID: Integer; const faceTemplate: WideString); dispid 1610743908;
    procedure ExcludeFaceTemplate(employeeID: Integer); dispid 1610743909;
    function InquiryRandomNumber: _RandomNumberResponse; dispid 1610743910;
    function InquiryEmployeeList: {??PSafeArray}OleVariant; dispid 1610743911;
    function InquiryFaceEmployeeList: {??PSafeArray}OleVariant; dispid 1610743912;
    procedure ExcludeFingerPrint(const pis: WideString); dispid 1610743913;
    procedure ExcludeFingerPrint_2(employeeID: Integer; fingerPrintType: EFingerPrintType); dispid 1610743914;
    procedure ExcludeCostCenter(costCenterID: Integer); dispid 1610743915;
    procedure ExcludeBioPointFingerPrint(const cardNumber: WideString); dispid 1610743916;
    procedure IncludeBioPointFingerPrint(const cardNumber: WideString; 
                                         const fingerPrint: WideString; 
                                         fingerPrintTypeOne: EFingerPrintType; 
                                         fingerPrintTypeTwo: EFingerPrintType); dispid 1610743917;
    procedure ExcludeCredentialList; dispid 1610743918;
    procedure ExcludeEmployeesList; dispid 1610743919;
    procedure ExcludeEmployeesListWithCostNumber; dispid 1610743920;
    procedure RepositioningMRPRecordsPointer_2(date: TDateTime); dispid 1610743921;
    procedure RepositioningMRPRecordsPointer_3(const nsr: WideString); dispid 1610743922;
    procedure setHoliday(day: Byte; month: Byte); dispid 1610743923;
    procedure setHoliday_2(const HolidayCollection: _HolidayCollection); dispid 1610743924;
    procedure sendEmptyMessage; dispid 1610743925;
    procedure SendSerialNumber(plateSerialNumber: {??Int64}OleVariant; 
                               mrpSerialNumber: {??Int64}OleVariant; 
                               mrpSealNumber: {??Int64}OleVariant); dispid 1610743926;
    procedure SendSerialNumber_2(const serialNumber: WideString); dispid 1610743927;
    procedure RemoveItemCardList(code: Integer); dispid 1610743928;
    procedure UpdateShiftTable(const ShiftTableCollection: _ShiftTableCollection); dispid 1610743929;
    procedure UpdateFirmware(const filePath: WideString); dispid 1610743930;
    procedure AlarmRing(const AlarmRing: _AlarmRingsCollection); dispid 1610743931;
    procedure setMemoryFormat(const MemoryFormat: _MemoryFormat); dispid 1610743932;
    procedure setAlternativeCode(AlternativeCode: Integer; const code: WideString); dispid 1610743933;
    procedure removeAlternativeCode(AlternativeCode: Integer); dispid 1610743934;
    procedure setJourneyWorking(const JourneyWorking: _JourneyWorking); dispid 1610743935;
    procedure ClearData; dispid 1610743936;
    function GetUnlockCode(const lockCode: WideString): WideString; dispid 1610743937;
    procedure CancelEmployeeTransmission; dispid 1610743938;
    procedure CancelCredentialTransmission; dispid 1610743939;
    property Connected: WordBool readonly dispid 1610743940;
    procedure ClearCredencialList; dispid 1610743941;
    procedure ClearEmployeeList; dispid 1610743942;
  end;

// *********************************************************************//
// Interface: _WatchComm
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {12335FED-346F-3140-B4BB-4651674481ED}
// *********************************************************************//
  _WatchComm = interface(IDispatch)
    ['{12335FED-346F-3140-B4BB-4651674481ED}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure AddConfiguration(configurationType: EConfigurationType; field1: OleVariant); safecall;
    procedure AddConfiguration_2(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant); safecall;
    procedure AddConfiguration_3(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant); safecall;
    procedure AddConfiguration_4(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant; field4: OleVariant); safecall;
    procedure AddConfiguration_5(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant; field4: OleVariant; 
                                 field5: OleVariant); safecall;
    procedure AddParcialConfiguration(ParcialConfigurationType: ParcialConfigurationType; 
                                      field1: OleVariant); safecall;
    procedure AddParcialConfiguration_2(ParcialConfigurationType: ParcialConfigurationType; 
                                        field1: OleVariant; field2: OleVariant); safecall;
    procedure AddMaster(const pis: WideString; const Card: WideString; const password: WideString; 
                        hasTechniquesProgrammingPermission: WordBool; 
                        hasDataAndTimePermission: WordBool; 
                        hasPenDriveProgrammingPermisision: WordBool); safecall;
    procedure AddMaster_2(const pis: WideString; const Card: WideString; 
                          const password: WideString; hasTechniquesProgrammingPermission: WordBool; 
                          hasDataAndTimePermission: WordBool; 
                          hasPenDriveProgrammingPermisision: WordBool; 
                          hasBobbinChangePermisision: WordBool); safecall;
    procedure AddCredential(const cardCode: WideString; const pis: WideString; version: Byte); safecall;
    procedure AddEmployee(const pis: WideString); safecall;
    procedure AddEmployee_2(const pis: WideString; const name: WideString; 
                            const password: WideString); safecall;
    procedure AddEmployee_3(const pis: WideString; const name: WideString; 
                            const password: WideString; CostCenter: Integer); safecall;
    procedure AddEmployee_4(Id: Integer); safecall;
    procedure AddEmployee_5(employeeID: Integer; const pis: WideString; const cpf: WideString; 
                            const Credential: WideString; const name: WideString; 
                            const password: WideString; isMaster: WordBool); safecall;
    procedure AddEmployee_6(employeeID: Integer; const Credential: WideString; 
                            const name: WideString; const password: WideString; isMaster: WordBool); safecall;
    procedure CreateWatchComm(protocolType: WatchProtocolType; const Comm: IComm; 
                              WatchAddress: Integer; const firmwareVersion: WideString); safecall;
    procedure CreateWatchComm_2(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; connectionType: WatchConnectionType; 
                                const firmwareVersion: WideString); safecall;
    procedure CreateWatchComm_3(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; const accessKey: WideString; 
                                const firmwareVersion: WideString); safecall;
    procedure CreateWatchComm_4(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; const accessKey: WideString; 
                                connectionType: WatchConnectionType; 
                                const firmwareVersion: WideString); safecall;
    procedure OpenConnection; safecall;
    procedure CloseConnection; safecall;
    function GetStatus: _AbstractStatusMessage; safecall;
    function GetMAC: _GetMACResponse; safecall;
    function GetImmediateStatus: _ImmediateStatusResponse; safecall;
    function GetPrintPointStatus: _PrintPointStatusMessage; safecall;
    function GetPrintPointLiStatus: _PrintPointLiStatus; safecall;
    function GetFaceStatus: _FaceStatus; safecall;
    function CollectAll: _ArrayList; safecall;
    function GetCurrentPunch: _AbstractMessage; safecall;
    function RemoveCurrentPunch: _AbstractMessage; safecall;
    procedure GhostMethod__WatchComm_168_1; safecall;
    procedure SetDST(dstBegin: TDateTime; dstEnd: TDateTime); safecall;
    procedure RemoveDST; safecall;
    procedure Set1ToN; safecall;
    procedure Set1To1; safecall;
    procedure SetDateTimeAndDST_2(date: TDateTime; dstBegin: TDateTime; dstEnd: TDateTime); safecall;
    procedure SetDateTime(date: TDateTime); safecall;
    procedure EmployeesTotalProgrammingBegin; safecall;
    procedure EmployeesTotalProgrammingEnd; safecall;
    procedure ClearMasterList; safecall;
    procedure ClearClockCredentialsList; safecall;
    procedure ClearDisplayMessage; safecall;
    procedure ClearCostCenterList; safecall;
    procedure EnableLogDeniedAccess(enable: WordBool); safecall;
    procedure ProgramLotterySampleRate(rate: Byte; inout: Byte); safecall;
    procedure ProgramTriggerType(type_: Byte; time: Integer); safecall;
    function HardwareTestCollection: PSafeArray; safecall;
    procedure ConfigureCard(idLength: Integer; hasChecking: WordBool); safecall;
    procedure ConfigureCard_2(idLenghtMinimum: Integer; idLenghtMaximum: Integer; 
                              hasChecking: WordBool; way: WordBool); safecall;
    procedure Activation(active: WordBool; controlled: WordBool); safecall;
    procedure setCardList(const CardCollection: _CardCollection); safecall;
    procedure ConfigureMessage(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                               const Description: WideString); safecall;
    procedure ConfigureMessage_2(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                                 const Description: WideString; typeFunction: TypeActionFunction); safecall;
    procedure Master(Id: Integer; password: Integer; PenDriveProgramming: WordBool; 
                     DateTimeUpdated: WordBool; ProgrammingTechnical: WordBool); safecall;
    procedure ClearProgramming(ClearProgramming: TypeClearProgramming); safecall;
    procedure RepositioningMRPRecordsPointer; safecall;
    procedure REPPlacesInMaintenance; safecall;
    procedure CleanEssentialVariables; safecall;
    procedure RebuildEmployeesTable; safecall;
    procedure IncludeFingerPrint(const pis: WideString; const fingerPrint: WideString; 
                                 fingerPrintTypeOne: EFingerPrintType; 
                                 fingerPrintTypeTwo: EFingerPrintType; sensor: EfingerPrintSensor); safecall;
    procedure IncludeFingerPrint_2(const pis: WideString; const fingerPrint: WideString; 
                                   fingerPrintHandOne: EFingerPrintHand; 
                                   fingerPrintHandTwo: EFingerPrintHand); safecall;
    procedure IncludeFingerPrint_3(employeeID: Integer; fingerPrintType: EFingerPrintType; 
                                   const fingerPrint: WideString); safecall;
    procedure IncludeCostCenter(costCenterID: Integer; const costCenterDescription: WideString); safecall;
    procedure IncludeCredentialList(usesVersion: WordBool); safecall;
    procedure IncludeEmployeesList(usesPassword: WordBool; isTotalProgramming: WordBool); safecall;
    procedure IncludeEmployeesListWithCostCenter; safecall;
    procedure IncludeEmployeesList_2; safecall;
    procedure SendDisplayMessage(code: Smallint; const message: WideString); safecall;
    procedure SendSettings; safecall;
    procedure SendParcialSettings; safecall;
    procedure SendMasterList; safecall;
    procedure ChangeEmployer(employerType: EmployeerType; const cpf_cnpj: WideString; 
                             const cei: WideString; const name: WideString; 
                             const address: WideString); safecall;
    procedure ActivateBootLoader; safecall;
    procedure FirstActivation(const serialNumber: WideString; const password: WideString); safecall;
    procedure Activation_2(const password: WideString); safecall;
    procedure Activation_3; safecall;
    procedure ExchangeSealREP(const password: WideString); safecall;
    procedure SendMacAddress(const macAddressPart1: WideString; const macAddressPart2: WideString; 
                             const macAddressPart3: WideString; const macAddressPart4: WideString; 
                             const macAddressPart5: WideString; const macAddressPart6: WideString); safecall;
    function InquiryFingerPrint(InquiryFingerPrintType: InquiryFingerPrintType): _PrintPointFingerPrintMessage; safecall;
    function InquiryFingerPrint_2(employeeID: Integer): PSafeArray; safecall;
    function InquiryBioPointFingerPrint: _BioPointFingerPrintMessage; safecall;
    function InquiryFaceFingerPrint(employeeID: Integer): PSafeArray; safecall;
    function InquiryPrintPointEvent: _PrintPointEvent; safecall;
    function ConfirmReceiptPrintPointEvent: _PrintPointEvent; safecall;
    function InquiryPrintPointMRPEventLog: _PrintPointMRPEventLog; safecall;
    function ConfirmationReceiptFingerPrint: _PrintPointFingerPrintMessage; safecall;
    function ConfirmationReceiptBioPointFingerPrint: _BioPointFingerPrintMessage; safecall;
    function ConfirmationReceiptMRPRecords: PSafeArray; safecall;
    function ConfirmationReceiptEmployeeList: PSafeArray; safecall;
    function InquiryEmployeer: _PrintPointEmployerMessage; safecall;
    function InquirySerialNumberOfREPAndMemory: _PrintPointInquirySerialNumberOfREPAndMemoryResponse; safecall;
    function InquirySerialNumber: WideString; safecall;
    function InquiryMRPRecords(inquiryChangeEmployed: WordBool; 
                               inquirySettingRealTimeClock: WordBool; 
                               inquiryRegistrationMarkingPoint: WordBool; 
                               inquiryChangeCompanyIdentification: WordBool): PSafeArray; safecall;
    function InquiryMRPRecords_2(startDate: TDateTime; inquiryChangeEmployed: WordBool; 
                                 inquirySettingRealTimeClock: WordBool; 
                                 inquiryRegistrationMarkingPoint: WordBool; 
                                 inquiryChangeCompanyIdentification: WordBool): PSafeArray; safecall;
    function InquiryMRPRecords_3(const startNSR: WideString; inquiryChangeEmployed: WordBool; 
                                 inquirySettingRealTimeClock: WordBool; 
                                 inquiryRegistrationMarkingPoint: WordBool; 
                                 inquiryChangeCompanyIdentification: WordBool): PSafeArray; safecall;
    function InquiryMRPRecords_4(const startNSR: WideString; var errorOccurredInProcess: WordBool): PSafeArray; safecall;
    function InquiryFaceLogRecords: PSafeArray; safecall;
    procedure DeleteFaceLogRecords; safecall;
    function InquiryFaceTemplate(employeeID: Integer): WideString; safecall;
    procedure IncludeFaceTemplate(employeeID: Integer; const faceTemplate: WideString); safecall;
    procedure ExcludeFaceTemplate(employeeID: Integer); safecall;
    function InquiryRandomNumber: _RandomNumberResponse; safecall;
    function InquiryEmployeeList: PSafeArray; safecall;
    function InquiryFaceEmployeeList: PSafeArray; safecall;
    procedure ExcludeFingerPrint(const pis: WideString); safecall;
    procedure ExcludeFingerPrint_2(employeeID: Integer; fingerPrintType: EFingerPrintType); safecall;
    procedure ExcludeCostCenter(costCenterID: Integer); safecall;
    procedure ExcludeBioPointFingerPrint(const cardNumber: WideString); safecall;
    procedure IncludeBioPointFingerPrint(const cardNumber: WideString; 
                                         const fingerPrint: WideString; 
                                         fingerPrintTypeOne: EFingerPrintType; 
                                         fingerPrintTypeTwo: EFingerPrintType); safecall;
    procedure ExcludeCredentialList; safecall;
    procedure ExcludeEmployeesList; safecall;
    procedure ExcludeEmployeesListWithCostNumber; safecall;
    procedure RepositioningMRPRecordsPointer_2(date: TDateTime); safecall;
    procedure RepositioningMRPRecordsPointer_3(const nsr: WideString); safecall;
    procedure setHoliday(day: Byte; month: Byte); safecall;
    procedure setHoliday_2(const HolidayCollection: _HolidayCollection); safecall;
    procedure sendEmptyMessage; safecall;
    procedure SendSerialNumber(plateSerialNumber: Int64; mrpSerialNumber: Int64; 
                               mrpSealNumber: Int64); safecall;
    procedure SendSerialNumber_2(const serialNumber: WideString); safecall;
    procedure RemoveItemCardList(code: Integer); safecall;
    procedure UpdateShiftTable(const ShiftTableCollection: _ShiftTableCollection); safecall;
    procedure UpdateFirmware(const filePath: WideString); safecall;
    procedure AlarmRing(const AlarmRing: _AlarmRingsCollection); safecall;
    procedure setMemoryFormat(const MemoryFormat: _MemoryFormat); safecall;
    procedure setAlternativeCode(AlternativeCode: Integer; const code: WideString); safecall;
    procedure removeAlternativeCode(AlternativeCode: Integer); safecall;
    procedure setJourneyWorking(const JourneyWorking: _JourneyWorking); safecall;
    procedure ClearData; safecall;
    function GetUnlockCode(const lockCode: WideString): WideString; safecall;
    procedure CancelEmployeeTransmission; safecall;
    procedure CancelCredentialTransmission; safecall;
    function Get_Connected: WordBool; safecall;
    procedure ClearCredencialList; safecall;
    procedure ClearEmployeeList; safecall;
    procedure add_progressOFEmployeesListTransmission(const value: _EmployeesListTransmissionProgress_2); safecall;
    procedure remove_progressOFEmployeesListTransmission(const value: _EmployeesListTransmissionProgress_2); safecall;
    procedure add_progressOFCredentialsListTransmission(const value: _CredentialsListTransmissionProgress); safecall;
    procedure remove_progressOFCredentialsListTransmission(const value: _CredentialsListTransmissionProgress); safecall;
    function Get_GeneralTimeout: Integer; safecall;
    procedure Set_GeneralTimeout1(pRetVal: Integer); safecall;
    function Get_CollectTimeout: Integer; safecall;
    procedure Set_CollectTimeout1(pRetVal: Integer); safecall;
    function Get_Protocol: _AbstractProtocol; safecall;
    procedure _Set_Protocol1(const pRetVal: _AbstractProtocol); safecall;
    procedure ExcludeFingerPrintList; safecall;
    procedure ExcludeFingerPrintWithoutEmployee; safecall;
    procedure ExcludeFingerPrint_3(const pis: WideString; sensor: EfingerPrintSensor); safecall;
    property ToString: WideString read Get_ToString;
    property Connected: WordBool read Get_Connected;
    property GeneralTimeout: Integer read Get_GeneralTimeout;
    property GeneralTimeout1: Integer write Set_GeneralTimeout1;
    property CollectTimeout: Integer read Get_CollectTimeout;
    property CollectTimeout1: Integer write Set_CollectTimeout1;
    property Protocol: _AbstractProtocol read Get_Protocol;
  end;

// *********************************************************************//
// DispIntf:  _WatchCommDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {12335FED-346F-3140-B4BB-4651674481ED}
// *********************************************************************//
  _WatchCommDisp = dispinterface
    ['{12335FED-346F-3140-B4BB-4651674481ED}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    procedure AddConfiguration(configurationType: EConfigurationType; field1: OleVariant); dispid 1610743812;
    procedure AddConfiguration_2(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant); dispid 1610743813;
    procedure AddConfiguration_3(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant); dispid 1610743814;
    procedure AddConfiguration_4(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant; field4: OleVariant); dispid 1610743815;
    procedure AddConfiguration_5(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant; field4: OleVariant; 
                                 field5: OleVariant); dispid 1610743816;
    procedure AddParcialConfiguration(ParcialConfigurationType: ParcialConfigurationType; 
                                      field1: OleVariant); dispid 1610743817;
    procedure AddParcialConfiguration_2(ParcialConfigurationType: ParcialConfigurationType; 
                                        field1: OleVariant; field2: OleVariant); dispid 1610743818;
    procedure AddMaster(const pis: WideString; const Card: WideString; const password: WideString; 
                        hasTechniquesProgrammingPermission: WordBool; 
                        hasDataAndTimePermission: WordBool; 
                        hasPenDriveProgrammingPermisision: WordBool); dispid 1610743819;
    procedure AddMaster_2(const pis: WideString; const Card: WideString; 
                          const password: WideString; hasTechniquesProgrammingPermission: WordBool; 
                          hasDataAndTimePermission: WordBool; 
                          hasPenDriveProgrammingPermisision: WordBool; 
                          hasBobbinChangePermisision: WordBool); dispid 1610743820;
    procedure AddCredential(const cardCode: WideString; const pis: WideString; version: Byte); dispid 1610743821;
    procedure AddEmployee(const pis: WideString); dispid 1610743822;
    procedure AddEmployee_2(const pis: WideString; const name: WideString; 
                            const password: WideString); dispid 1610743823;
    procedure AddEmployee_3(const pis: WideString; const name: WideString; 
                            const password: WideString; CostCenter: Integer); dispid 1610743824;
    procedure AddEmployee_4(Id: Integer); dispid 1610743825;
    procedure AddEmployee_5(employeeID: Integer; const pis: WideString; const cpf: WideString; 
                            const Credential: WideString; const name: WideString; 
                            const password: WideString; isMaster: WordBool); dispid 1610743826;
    procedure AddEmployee_6(employeeID: Integer; const Credential: WideString; 
                            const name: WideString; const password: WideString; isMaster: WordBool); dispid 1610743827;
    procedure CreateWatchComm(protocolType: WatchProtocolType; const Comm: IComm; 
                              WatchAddress: Integer; const firmwareVersion: WideString); dispid 1610743828;
    procedure CreateWatchComm_2(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; connectionType: WatchConnectionType; 
                                const firmwareVersion: WideString); dispid 1610743829;
    procedure CreateWatchComm_3(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; const accessKey: WideString; 
                                const firmwareVersion: WideString); dispid 1610743830;
    procedure CreateWatchComm_4(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; const accessKey: WideString; 
                                connectionType: WatchConnectionType; 
                                const firmwareVersion: WideString); dispid 1610743831;
    procedure OpenConnection; dispid 1610743832;
    procedure CloseConnection; dispid 1610743833;
    function GetStatus: _AbstractStatusMessage; dispid 1610743834;
    function GetMAC: _GetMACResponse; dispid 1610743835;
    function GetImmediateStatus: _ImmediateStatusResponse; dispid 1610743836;
    function GetPrintPointStatus: _PrintPointStatusMessage; dispid 1610743837;
    function GetPrintPointLiStatus: _PrintPointLiStatus; dispid 1610743838;
    function GetFaceStatus: _FaceStatus; dispid 1610743839;
    function CollectAll: _ArrayList; dispid 1610743840;
    function GetCurrentPunch: _AbstractMessage; dispid 1610743841;
    function RemoveCurrentPunch: _AbstractMessage; dispid 1610743842;
    procedure GhostMethod__WatchComm_168_1; dispid 1610743843;
    procedure SetDST(dstBegin: TDateTime; dstEnd: TDateTime); dispid 1610743844;
    procedure RemoveDST; dispid 1610743845;
    procedure Set1ToN; dispid 1610743846;
    procedure Set1To1; dispid 1610743847;
    procedure SetDateTimeAndDST_2(date: TDateTime; dstBegin: TDateTime; dstEnd: TDateTime); dispid 1610743848;
    procedure SetDateTime(date: TDateTime); dispid 1610743849;
    procedure EmployeesTotalProgrammingBegin; dispid 1610743850;
    procedure EmployeesTotalProgrammingEnd; dispid 1610743851;
    procedure ClearMasterList; dispid 1610743852;
    procedure ClearClockCredentialsList; dispid 1610743853;
    procedure ClearDisplayMessage; dispid 1610743854;
    procedure ClearCostCenterList; dispid 1610743855;
    procedure EnableLogDeniedAccess(enable: WordBool); dispid 1610743856;
    procedure ProgramLotterySampleRate(rate: Byte; inout: Byte); dispid 1610743857;
    procedure ProgramTriggerType(type_: Byte; time: Integer); dispid 1610743858;
    function HardwareTestCollection: {??PSafeArray}OleVariant; dispid 1610743859;
    procedure ConfigureCard(idLength: Integer; hasChecking: WordBool); dispid 1610743860;
    procedure ConfigureCard_2(idLenghtMinimum: Integer; idLenghtMaximum: Integer; 
                              hasChecking: WordBool; way: WordBool); dispid 1610743861;
    procedure Activation(active: WordBool; controlled: WordBool); dispid 1610743862;
    procedure setCardList(const CardCollection: _CardCollection); dispid 1610743863;
    procedure ConfigureMessage(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                               const Description: WideString); dispid 1610743864;
    procedure ConfigureMessage_2(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                                 const Description: WideString; 
                                 typeFunction: {??TypeActionFunction}OleVariant); dispid 1610743865;
    procedure Master(Id: Integer; password: Integer; PenDriveProgramming: WordBool; 
                     DateTimeUpdated: WordBool; ProgrammingTechnical: WordBool); dispid 1610743866;
    procedure ClearProgramming(ClearProgramming: TypeClearProgramming); dispid 1610743867;
    procedure RepositioningMRPRecordsPointer; dispid 1610743868;
    procedure REPPlacesInMaintenance; dispid 1610743869;
    procedure CleanEssentialVariables; dispid 1610743870;
    procedure RebuildEmployeesTable; dispid 1610743871;
    procedure IncludeFingerPrint(const pis: WideString; const fingerPrint: WideString; 
                                 fingerPrintTypeOne: EFingerPrintType; 
                                 fingerPrintTypeTwo: EFingerPrintType; sensor: EfingerPrintSensor); dispid 1610743872;
    procedure IncludeFingerPrint_2(const pis: WideString; const fingerPrint: WideString; 
                                   fingerPrintHandOne: EFingerPrintHand; 
                                   fingerPrintHandTwo: EFingerPrintHand); dispid 1610743873;
    procedure IncludeFingerPrint_3(employeeID: Integer; fingerPrintType: EFingerPrintType; 
                                   const fingerPrint: WideString); dispid 1610743874;
    procedure IncludeCostCenter(costCenterID: Integer; const costCenterDescription: WideString); dispid 1610743875;
    procedure IncludeCredentialList(usesVersion: WordBool); dispid 1610743876;
    procedure IncludeEmployeesList(usesPassword: WordBool; isTotalProgramming: WordBool); dispid 1610743877;
    procedure IncludeEmployeesListWithCostCenter; dispid 1610743878;
    procedure IncludeEmployeesList_2; dispid 1610743879;
    procedure SendDisplayMessage(code: Smallint; const message: WideString); dispid 1610743880;
    procedure SendSettings; dispid 1610743881;
    procedure SendParcialSettings; dispid 1610743882;
    procedure SendMasterList; dispid 1610743883;
    procedure ChangeEmployer(employerType: EmployeerType; const cpf_cnpj: WideString; 
                             const cei: WideString; const name: WideString; 
                             const address: WideString); dispid 1610743884;
    procedure ActivateBootLoader; dispid 1610743885;
    procedure FirstActivation(const serialNumber: WideString; const password: WideString); dispid 1610743886;
    procedure Activation_2(const password: WideString); dispid 1610743887;
    procedure Activation_3; dispid 1610743888;
    procedure ExchangeSealREP(const password: WideString); dispid 1610743889;
    procedure SendMacAddress(const macAddressPart1: WideString; const macAddressPart2: WideString; 
                             const macAddressPart3: WideString; const macAddressPart4: WideString; 
                             const macAddressPart5: WideString; const macAddressPart6: WideString); dispid 1610743890;
    function InquiryFingerPrint(InquiryFingerPrintType: InquiryFingerPrintType): _PrintPointFingerPrintMessage; dispid 1610743891;
    function InquiryFingerPrint_2(employeeID: Integer): {??PSafeArray}OleVariant; dispid 1610743892;
    function InquiryBioPointFingerPrint: _BioPointFingerPrintMessage; dispid 1610743893;
    function InquiryFaceFingerPrint(employeeID: Integer): {??PSafeArray}OleVariant; dispid 1610743894;
    function InquiryPrintPointEvent: _PrintPointEvent; dispid 1610743895;
    function ConfirmReceiptPrintPointEvent: _PrintPointEvent; dispid 1610743896;
    function InquiryPrintPointMRPEventLog: _PrintPointMRPEventLog; dispid 1610743897;
    function ConfirmationReceiptFingerPrint: _PrintPointFingerPrintMessage; dispid 1610743898;
    function ConfirmationReceiptBioPointFingerPrint: _BioPointFingerPrintMessage; dispid 1610743899;
    function ConfirmationReceiptMRPRecords: {??PSafeArray}OleVariant; dispid 1610743900;
    function ConfirmationReceiptEmployeeList: {??PSafeArray}OleVariant; dispid 1610743901;
    function InquiryEmployeer: _PrintPointEmployerMessage; dispid 1610743902;
    function InquirySerialNumberOfREPAndMemory: _PrintPointInquirySerialNumberOfREPAndMemoryResponse; dispid 1610743903;
    function InquirySerialNumber: WideString; dispid 1610743904;
    function InquiryMRPRecords(inquiryChangeEmployed: WordBool; 
                               inquirySettingRealTimeClock: WordBool; 
                               inquiryRegistrationMarkingPoint: WordBool; 
                               inquiryChangeCompanyIdentification: WordBool): {??PSafeArray}OleVariant; dispid 1610743905;
    function InquiryMRPRecords_2(startDate: TDateTime; inquiryChangeEmployed: WordBool; 
                                 inquirySettingRealTimeClock: WordBool; 
                                 inquiryRegistrationMarkingPoint: WordBool; 
                                 inquiryChangeCompanyIdentification: WordBool): {??PSafeArray}OleVariant; dispid 1610743906;
    function InquiryMRPRecords_3(const startNSR: WideString; inquiryChangeEmployed: WordBool; 
                                 inquirySettingRealTimeClock: WordBool; 
                                 inquiryRegistrationMarkingPoint: WordBool; 
                                 inquiryChangeCompanyIdentification: WordBool): {??PSafeArray}OleVariant; dispid 1610743907;
    function InquiryMRPRecords_4(const startNSR: WideString; var errorOccurredInProcess: WordBool): {??PSafeArray}OleVariant; dispid 1610743908;
    function InquiryFaceLogRecords: {??PSafeArray}OleVariant; dispid 1610743909;
    procedure DeleteFaceLogRecords; dispid 1610743910;
    function InquiryFaceTemplate(employeeID: Integer): WideString; dispid 1610743911;
    procedure IncludeFaceTemplate(employeeID: Integer; const faceTemplate: WideString); dispid 1610743912;
    procedure ExcludeFaceTemplate(employeeID: Integer); dispid 1610743913;
    function InquiryRandomNumber: _RandomNumberResponse; dispid 1610743914;
    function InquiryEmployeeList: {??PSafeArray}OleVariant; dispid 1610743915;
    function InquiryFaceEmployeeList: {??PSafeArray}OleVariant; dispid 1610743916;
    procedure ExcludeFingerPrint(const pis: WideString); dispid 1610743917;
    procedure ExcludeFingerPrint_2(employeeID: Integer; fingerPrintType: EFingerPrintType); dispid 1610743918;
    procedure ExcludeCostCenter(costCenterID: Integer); dispid 1610743919;
    procedure ExcludeBioPointFingerPrint(const cardNumber: WideString); dispid 1610743920;
    procedure IncludeBioPointFingerPrint(const cardNumber: WideString; 
                                         const fingerPrint: WideString; 
                                         fingerPrintTypeOne: EFingerPrintType; 
                                         fingerPrintTypeTwo: EFingerPrintType); dispid 1610743921;
    procedure ExcludeCredentialList; dispid 1610743922;
    procedure ExcludeEmployeesList; dispid 1610743923;
    procedure ExcludeEmployeesListWithCostNumber; dispid 1610743924;
    procedure RepositioningMRPRecordsPointer_2(date: TDateTime); dispid 1610743925;
    procedure RepositioningMRPRecordsPointer_3(const nsr: WideString); dispid 1610743926;
    procedure setHoliday(day: Byte; month: Byte); dispid 1610743927;
    procedure setHoliday_2(const HolidayCollection: _HolidayCollection); dispid 1610743928;
    procedure sendEmptyMessage; dispid 1610743929;
    procedure SendSerialNumber(plateSerialNumber: {??Int64}OleVariant; 
                               mrpSerialNumber: {??Int64}OleVariant; 
                               mrpSealNumber: {??Int64}OleVariant); dispid 1610743930;
    procedure SendSerialNumber_2(const serialNumber: WideString); dispid 1610743931;
    procedure RemoveItemCardList(code: Integer); dispid 1610743932;
    procedure UpdateShiftTable(const ShiftTableCollection: _ShiftTableCollection); dispid 1610743933;
    procedure UpdateFirmware(const filePath: WideString); dispid 1610743934;
    procedure AlarmRing(const AlarmRing: _AlarmRingsCollection); dispid 1610743935;
    procedure setMemoryFormat(const MemoryFormat: _MemoryFormat); dispid 1610743936;
    procedure setAlternativeCode(AlternativeCode: Integer; const code: WideString); dispid 1610743937;
    procedure removeAlternativeCode(AlternativeCode: Integer); dispid 1610743938;
    procedure setJourneyWorking(const JourneyWorking: _JourneyWorking); dispid 1610743939;
    procedure ClearData; dispid 1610743940;
    function GetUnlockCode(const lockCode: WideString): WideString; dispid 1610743941;
    procedure CancelEmployeeTransmission; dispid 1610743942;
    procedure CancelCredentialTransmission; dispid 1610743943;
    property Connected: WordBool readonly dispid 1610743944;
    procedure ClearCredencialList; dispid 1610743945;
    procedure ClearEmployeeList; dispid 1610743946;
    procedure add_progressOFEmployeesListTransmission(const value: _EmployeesListTransmissionProgress_2); dispid 1610743947;
    procedure remove_progressOFEmployeesListTransmission(const value: _EmployeesListTransmissionProgress_2); dispid 1610743948;
    procedure add_progressOFCredentialsListTransmission(const value: _CredentialsListTransmissionProgress); dispid 1610743949;
    procedure remove_progressOFCredentialsListTransmission(const value: _CredentialsListTransmissionProgress); dispid 1610743950;
    property GeneralTimeout: Integer readonly dispid 1610743951;
    property GeneralTimeout1: Integer writeonly dispid 1610743952;
    property CollectTimeout: Integer readonly dispid 1610743953;
    property CollectTimeout1: Integer writeonly dispid 1610743954;
    property Protocol: _AbstractProtocol readonly dispid 1610743955;
    procedure ExcludeFingerPrintList; dispid 1610743957;
    procedure ExcludeFingerPrintWithoutEmployee; dispid 1610743958;
    procedure ExcludeFingerPrint_3(const pis: WideString; sensor: EfingerPrintSensor); dispid 1610743959;
  end;

// *********************************************************************//
// Interface: _CredentialsListTransmissionProgress
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {B4E35F36-26C8-374F-B2C5-E481E3B32F2C}
// *********************************************************************//
  _CredentialsListTransmissionProgress = interface(IDispatch)
    ['{B4E35F36-26C8-374F-B2C5-E481E3B32F2C}']
  end;

// *********************************************************************//
// DispIntf:  _CredentialsListTransmissionProgressDisp
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {B4E35F36-26C8-374F-B2C5-E481E3B32F2C}
// *********************************************************************//
  _CredentialsListTransmissionProgressDisp = dispinterface
    ['{B4E35F36-26C8-374F-B2C5-E481E3B32F2C}']
  end;

// *********************************************************************//
// Interface: _PrintPointSendSerialNumberMessage
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {3F62753F-3060-3AE8-881D-D2D55E6F65FD}
// *********************************************************************//
  _PrintPointSendSerialNumberMessage = interface(IDispatch)
    ['{3F62753F-3060-3AE8-881D-D2D55E6F65FD}']
  end;

// *********************************************************************//
// DispIntf:  _PrintPointSendSerialNumberMessageDisp
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {3F62753F-3060-3AE8-881D-D2D55E6F65FD}
// *********************************************************************//
  _PrintPointSendSerialNumberMessageDisp = dispinterface
    ['{3F62753F-3060-3AE8-881D-D2D55E6F65FD}']
  end;

// *********************************************************************//
// Interface: _PrintPointLiStatus
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {CB3D475A-280A-3F62-BDE2-1341D0F8E15A}
// *********************************************************************//
  _PrintPointLiStatus = interface(IDispatch)
    ['{CB3D475A-280A-3F62-BDE2-1341D0F8E15A}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_RecordsCapacity: Integer; safecall;
    function Get_FingerprintCapacity: Integer; safecall;
    function Get_UsersCapacity: Integer; safecall;
    function Get_MasterOccupation: Integer; safecall;
    function Get_PasswordOccupation: Integer; safecall;
    function Get_FingerprintOccupation: Integer; safecall;
    function Get_UserOccupation: Integer; safecall;
    function Get_firmwareVersion: WideString; safecall;
    function Get_MRPVersion: WideString; safecall;
    function Get_RecordsTotal: Integer; safecall;
    property ToString: WideString read Get_ToString;
    property RecordsCapacity: Integer read Get_RecordsCapacity;
    property FingerprintCapacity: Integer read Get_FingerprintCapacity;
    property UsersCapacity: Integer read Get_UsersCapacity;
    property MasterOccupation: Integer read Get_MasterOccupation;
    property PasswordOccupation: Integer read Get_PasswordOccupation;
    property FingerprintOccupation: Integer read Get_FingerprintOccupation;
    property UserOccupation: Integer read Get_UserOccupation;
    property firmwareVersion: WideString read Get_firmwareVersion;
    property MRPVersion: WideString read Get_MRPVersion;
    property RecordsTotal: Integer read Get_RecordsTotal;
  end;

// *********************************************************************//
// DispIntf:  _PrintPointLiStatusDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {CB3D475A-280A-3F62-BDE2-1341D0F8E15A}
// *********************************************************************//
  _PrintPointLiStatusDisp = dispinterface
    ['{CB3D475A-280A-3F62-BDE2-1341D0F8E15A}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property RecordsCapacity: Integer readonly dispid 1610743812;
    property FingerprintCapacity: Integer readonly dispid 1610743813;
    property UsersCapacity: Integer readonly dispid 1610743814;
    property MasterOccupation: Integer readonly dispid 1610743815;
    property PasswordOccupation: Integer readonly dispid 1610743816;
    property FingerprintOccupation: Integer readonly dispid 1610743817;
    property UserOccupation: Integer readonly dispid 1610743818;
    property firmwareVersion: WideString readonly dispid 1610743819;
    property MRPVersion: WideString readonly dispid 1610743820;
    property RecordsTotal: Integer readonly dispid 1610743821;
  end;

// *********************************************************************//
// Interface: _MRPRecord
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {F0036B67-82AA-36B4-B617-6AD095B54F40}
// *********************************************************************//
  _MRPRecord = interface(IDispatch)
    ['{F0036B67-82AA-36B4-B617-6AD095B54F40}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_nsr: WideString; safecall;
    procedure Set_nsr(const pRetVal: WideString); safecall;
    property ToString: WideString read Get_ToString;
    property nsr: WideString read Get_nsr write Set_nsr;
  end;

// *********************************************************************//
// DispIntf:  _MRPRecordDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {F0036B67-82AA-36B4-B617-6AD095B54F40}
// *********************************************************************//
  _MRPRecordDisp = dispinterface
    ['{F0036B67-82AA-36B4-B617-6AD095B54F40}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property nsr: WideString dispid 1610743812;
  end;

// *********************************************************************//
// Interface: _MRPRecord_ChangeEmployee
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {998DA974-385F-3B62-A3EE-FD43F385E164}
// *********************************************************************//
  _MRPRecord_ChangeEmployee = interface(IDispatch)
    ['{998DA974-385F-3B62-A3EE-FD43F385E164}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_nsr: WideString; safecall;
    procedure Set_nsr(const pRetVal: WideString); safecall;
    function Get_DateTimeRecordingRegistry: TDateTime; safecall;
    procedure Set_DateTimeRecordingRegistry(pRetVal: TDateTime); safecall;
    function Get_ChangeEmployeeType: ChangeEmployeeType; safecall;
    procedure Set_ChangeEmployeeType(pRetVal: ChangeEmployeeType); safecall;
    function Get_pis: WideString; safecall;
    procedure Set_pis(const pRetVal: WideString); safecall;
    function Get_name: WideString; safecall;
    procedure Set_name(const pRetVal: WideString); safecall;
    property ToString: WideString read Get_ToString;
    property nsr: WideString read Get_nsr write Set_nsr;
    property DateTimeRecordingRegistry: TDateTime read Get_DateTimeRecordingRegistry write Set_DateTimeRecordingRegistry;
    property ChangeEmployeeType: ChangeEmployeeType read Get_ChangeEmployeeType write Set_ChangeEmployeeType;
    property pis: WideString read Get_pis write Set_pis;
    property name: WideString read Get_name write Set_name;
  end;

// *********************************************************************//
// DispIntf:  _MRPRecord_ChangeEmployeeDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {998DA974-385F-3B62-A3EE-FD43F385E164}
// *********************************************************************//
  _MRPRecord_ChangeEmployeeDisp = dispinterface
    ['{998DA974-385F-3B62-A3EE-FD43F385E164}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property nsr: WideString dispid 1610743812;
    property DateTimeRecordingRegistry: TDateTime dispid 1610743814;
    property ChangeEmployeeType: ChangeEmployeeType dispid 1610743816;
    property pis: WideString dispid 1610743818;
    property name: WideString dispid 1610743820;
  end;

// *********************************************************************//
// Interface: _FaceFingerPrint
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {C65B932D-291C-3FB9-90A8-11B8312DE61D}
// *********************************************************************//
  _FaceFingerPrint = interface(IDispatch)
    ['{C65B932D-291C-3FB9-90A8-11B8312DE61D}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_fingerPrintType: EFingerPrintType; safecall;
    procedure Set_fingerPrintType(pRetVal: EFingerPrintType); safecall;
    function Get_fingerPrint: WideString; safecall;
    procedure Set_fingerPrint(const pRetVal: WideString); safecall;
    function Get_employeeID: Integer; safecall;
    procedure Set_employeeID(pRetVal: Integer); safecall;
    property ToString: WideString read Get_ToString;
    property fingerPrintType: EFingerPrintType read Get_fingerPrintType write Set_fingerPrintType;
    property fingerPrint: WideString read Get_fingerPrint write Set_fingerPrint;
    property employeeID: Integer read Get_employeeID write Set_employeeID;
  end;

// *********************************************************************//
// DispIntf:  _FaceFingerPrintDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {C65B932D-291C-3FB9-90A8-11B8312DE61D}
// *********************************************************************//
  _FaceFingerPrintDisp = dispinterface
    ['{C65B932D-291C-3FB9-90A8-11B8312DE61D}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property fingerPrintType: EFingerPrintType dispid 1610743812;
    property fingerPrint: WideString dispid 1610743814;
    property employeeID: Integer dispid 1610743816;
  end;

// *********************************************************************//
// Interface: _AbstractCardList
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {8D6BD7AC-8AED-32D6-9A88-B8C5E2900792}
// *********************************************************************//
  _AbstractCardList = interface(IDispatch)
    ['{8D6BD7AC-8AED-32D6-9A88-B8C5E2900792}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    procedure _Set_Card(const pRetVal: _Card); safecall;
    function Get_Card: _Card; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property Card: _Card read Get_Card write _Set_Card;
  end;

// *********************************************************************//
// DispIntf:  _AbstractCardListDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {8D6BD7AC-8AED-32D6-9A88-B8C5E2900792}
// *********************************************************************//
  _AbstractCardListDisp = dispinterface
    ['{8D6BD7AC-8AED-32D6-9A88-B8C5E2900792}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property Card: _Card dispid 1610743816;
  end;

// *********************************************************************//
// Interface: _AbstractStatusMessage
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {46BD67A6-4154-3B24-B932-31B854D8B781}
// *********************************************************************//
  _AbstractStatusMessage = interface(IDispatch)
    ['{46BD67A6-4154-3B24-B932-31B854D8B781}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_firmwareVersion: WideString; safecall;
    procedure Set_firmwareVersion(const pRetVal: WideString); safecall;
    function Get_RecordsSize: LongWord; safecall;
    procedure Set_RecordsSize(pRetVal: LongWord); safecall;
    function Get_RecordsCount: LongWord; safecall;
    procedure Set_RecordsCount(pRetVal: LongWord); safecall;
    function Get_CardListSize: LongWord; safecall;
    procedure Set_CardListSize(pRetVal: LongWord); safecall;
    function Get_date: TDateTime; safecall;
    procedure Set_date(pRetVal: TDateTime); safecall;
    function Get_CheckType: Byte; safecall;
    procedure Set_CheckType(pRetVal: Byte); safecall;
    function Get_RecordDeniedAccess: WordBool; safecall;
    procedure Set_RecordDeniedAccess(pRetVal: WordBool); safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property firmwareVersion: WideString read Get_firmwareVersion write Set_firmwareVersion;
    property RecordsSize: LongWord read Get_RecordsSize write Set_RecordsSize;
    property RecordsCount: LongWord read Get_RecordsCount write Set_RecordsCount;
    property CardListSize: LongWord read Get_CardListSize write Set_CardListSize;
    property date: TDateTime read Get_date write Set_date;
    property CheckType: Byte read Get_CheckType write Set_CheckType;
    property RecordDeniedAccess: WordBool read Get_RecordDeniedAccess write Set_RecordDeniedAccess;
  end;

// *********************************************************************//
// DispIntf:  _AbstractStatusMessageDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {46BD67A6-4154-3B24-B932-31B854D8B781}
// *********************************************************************//
  _AbstractStatusMessageDisp = dispinterface
    ['{46BD67A6-4154-3B24-B932-31B854D8B781}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property firmwareVersion: WideString dispid 1610743816;
    property RecordsSize: LongWord dispid 1610743818;
    property RecordsCount: LongWord dispid 1610743820;
    property CardListSize: LongWord dispid 1610743822;
    property date: TDateTime dispid 1610743824;
    property CheckType: Byte dispid 1610743826;
    property RecordDeniedAccess: WordBool dispid 1610743828;
  end;

// *********************************************************************//
// Interface: _MiniPointStatusMessage
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {1E3010DC-B3B4-3EB3-BF7C-19952DED0E4E}
// *********************************************************************//
  _MiniPointStatusMessage = interface(IDispatch)
    ['{1E3010DC-B3B4-3EB3-BF7C-19952DED0E4E}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_firmwareVersion: WideString; safecall;
    procedure Set_firmwareVersion(const pRetVal: WideString); safecall;
    function Get_RecordsSize: LongWord; safecall;
    procedure Set_RecordsSize(pRetVal: LongWord); safecall;
    function Get_RecordsCount: LongWord; safecall;
    procedure Set_RecordsCount(pRetVal: LongWord); safecall;
    function Get_CardListSize: LongWord; safecall;
    procedure Set_CardListSize(pRetVal: LongWord); safecall;
    function Get_date: TDateTime; safecall;
    procedure Set_date(pRetVal: TDateTime); safecall;
    function Get_CheckType: Byte; safecall;
    procedure Set_CheckType(pRetVal: Byte); safecall;
    function Get_RecordDeniedAccess: WordBool; safecall;
    procedure Set_RecordDeniedAccess(pRetVal: WordBool); safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property firmwareVersion: WideString read Get_firmwareVersion write Set_firmwareVersion;
    property RecordsSize: LongWord read Get_RecordsSize write Set_RecordsSize;
    property RecordsCount: LongWord read Get_RecordsCount write Set_RecordsCount;
    property CardListSize: LongWord read Get_CardListSize write Set_CardListSize;
    property date: TDateTime read Get_date write Set_date;
    property CheckType: Byte read Get_CheckType write Set_CheckType;
    property RecordDeniedAccess: WordBool read Get_RecordDeniedAccess write Set_RecordDeniedAccess;
  end;

// *********************************************************************//
// DispIntf:  _MiniPointStatusMessageDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {1E3010DC-B3B4-3EB3-BF7C-19952DED0E4E}
// *********************************************************************//
  _MiniPointStatusMessageDisp = dispinterface
    ['{1E3010DC-B3B4-3EB3-BF7C-19952DED0E4E}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property firmwareVersion: WideString dispid 1610743816;
    property RecordsSize: LongWord dispid 1610743818;
    property RecordsCount: LongWord dispid 1610743820;
    property CardListSize: LongWord dispid 1610743822;
    property date: TDateTime dispid 1610743824;
    property CheckType: Byte dispid 1610743826;
    property RecordDeniedAccess: WordBool dispid 1610743828;
  end;

// *********************************************************************//
// Interface: _NoDataMessage
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {80B6B329-5459-3562-92E9-1A134496876A}
// *********************************************************************//
  _NoDataMessage = interface(IDispatch)
    ['{80B6B329-5459-3562-92E9-1A134496876A}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
  end;

// *********************************************************************//
// DispIntf:  _NoDataMessageDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {80B6B329-5459-3562-92E9-1A134496876A}
// *********************************************************************//
  _NoDataMessageDisp = dispinterface
    ['{80B6B329-5459-3562-92E9-1A134496876A}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
  end;

// *********************************************************************//
// Interface: _Template
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {C709AC5D-461C-33C6-B135-75B626665DCD}
// *********************************************************************//
  _Template = interface(IDispatch)
    ['{C709AC5D-461C-33C6-B135-75B626665DCD}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_Digital: WideString; safecall;
    procedure Set_Digital(const pRetVal: WideString); safecall;
    function Get_DedoDigital: TypeDedoDigital; safecall;
    procedure Set_DedoDigital(pRetVal: TypeDedoDigital); safecall;
    property ToString: WideString read Get_ToString;
    property Digital: WideString read Get_Digital write Set_Digital;
    property DedoDigital: TypeDedoDigital read Get_DedoDigital write Set_DedoDigital;
  end;

// *********************************************************************//
// DispIntf:  _TemplateDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {C709AC5D-461C-33C6-B135-75B626665DCD}
// *********************************************************************//
  _TemplateDisp = dispinterface
    ['{C709AC5D-461C-33C6-B135-75B626665DCD}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property Digital: WideString dispid 1610743812;
    property DedoDigital: TypeDedoDigital dispid 1610743814;
  end;

// *********************************************************************//
// Interface: _MiniPointConfigurator
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {C94B244C-FE69-3B75-BC35-3EFF5C28B364}
// *********************************************************************//
  _MiniPointConfigurator = interface(IDispatch)
    ['{C94B244C-FE69-3B75-BC35-3EFF5C28B364}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    property ToString: WideString read Get_ToString;
  end;

// *********************************************************************//
// DispIntf:  _MiniPointConfiguratorDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {C94B244C-FE69-3B75-BC35-3EFF5C28B364}
// *********************************************************************//
  _MiniPointConfiguratorDisp = dispinterface
    ['{C94B244C-FE69-3B75-BC35-3EFF5C28B364}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
  end;

// *********************************************************************//
// Interface: _Util
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {5BCFE2B0-7597-36B6-801B-93EC25098916}
// *********************************************************************//
  _Util = interface(IDispatch)
    ['{5BCFE2B0-7597-36B6-801B-93EC25098916}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    property ToString: WideString read Get_ToString;
  end;

// *********************************************************************//
// DispIntf:  _UtilDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {5BCFE2B0-7597-36B6-801B-93EC25098916}
// *********************************************************************//
  _UtilDisp = dispinterface
    ['{5BCFE2B0-7597-36B6-801B-93EC25098916}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
  end;

// *********************************************************************//
// Interface: _MicroPointStatusMessage
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {EA7AE898-8BDC-3757-B62C-E9D50CA5F4AC}
// *********************************************************************//
  _MicroPointStatusMessage = interface(IDispatch)
    ['{EA7AE898-8BDC-3757-B62C-E9D50CA5F4AC}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_firmwareVersion: WideString; safecall;
    procedure Set_firmwareVersion(const pRetVal: WideString); safecall;
    function Get_RecordsSize: LongWord; safecall;
    procedure Set_RecordsSize(pRetVal: LongWord); safecall;
    function Get_RecordsCount: LongWord; safecall;
    procedure Set_RecordsCount(pRetVal: LongWord); safecall;
    function Get_CardListSize: LongWord; safecall;
    procedure Set_CardListSize(pRetVal: LongWord); safecall;
    function Get_date: TDateTime; safecall;
    procedure Set_date(pRetVal: TDateTime); safecall;
    function Get_CheckType: Byte; safecall;
    procedure Set_CheckType(pRetVal: Byte); safecall;
    function Get_RecordDeniedAccess: WordBool; safecall;
    procedure Set_RecordDeniedAccess(pRetVal: WordBool); safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property firmwareVersion: WideString read Get_firmwareVersion write Set_firmwareVersion;
    property RecordsSize: LongWord read Get_RecordsSize write Set_RecordsSize;
    property RecordsCount: LongWord read Get_RecordsCount write Set_RecordsCount;
    property CardListSize: LongWord read Get_CardListSize write Set_CardListSize;
    property date: TDateTime read Get_date write Set_date;
    property CheckType: Byte read Get_CheckType write Set_CheckType;
    property RecordDeniedAccess: WordBool read Get_RecordDeniedAccess write Set_RecordDeniedAccess;
  end;

// *********************************************************************//
// DispIntf:  _MicroPointStatusMessageDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {EA7AE898-8BDC-3757-B62C-E9D50CA5F4AC}
// *********************************************************************//
  _MicroPointStatusMessageDisp = dispinterface
    ['{EA7AE898-8BDC-3757-B62C-E9D50CA5F4AC}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property firmwareVersion: WideString dispid 1610743816;
    property RecordsSize: LongWord dispid 1610743818;
    property RecordsCount: LongWord dispid 1610743820;
    property CardListSize: LongWord dispid 1610743822;
    property date: TDateTime dispid 1610743824;
    property CheckType: Byte dispid 1610743826;
    property RecordDeniedAccess: WordBool dispid 1610743828;
  end;

// *********************************************************************//
// Interface: _BioPointCardList
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {B8B4B8DB-4CDB-3B9B-A8CA-88405FC9A0A7}
// *********************************************************************//
  _BioPointCardList = interface(IDispatch)
    ['{B8B4B8DB-4CDB-3B9B-A8CA-88405FC9A0A7}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    procedure _Set_Card(const pRetVal: _Card); safecall;
    function Get_Card: _Card; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property Card: _Card read Get_Card write _Set_Card;
  end;

// *********************************************************************//
// DispIntf:  _BioPointCardListDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {B8B4B8DB-4CDB-3B9B-A8CA-88405FC9A0A7}
// *********************************************************************//
  _BioPointCardListDisp = dispinterface
    ['{B8B4B8DB-4CDB-3B9B-A8CA-88405FC9A0A7}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property Card: _Card dispid 1610743816;
  end;

// *********************************************************************//
// Interface: _ShiftTable
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {1F79777E-9628-326F-82D0-DB3A67DFF466}
// *********************************************************************//
  _ShiftTable = interface(IDispatch)
    ['{1F79777E-9628-326F-82D0-DB3A67DFF466}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_Inicio: WideString; safecall;
    procedure Set_Inicio(const pRetVal: WideString); safecall;
    function Get_Fim: WideString; safecall;
    procedure Set_Fim(const pRetVal: WideString); safecall;
    property ToString: WideString read Get_ToString;
    property Inicio: WideString read Get_Inicio write Set_Inicio;
    property Fim: WideString read Get_Fim write Set_Fim;
  end;

// *********************************************************************//
// DispIntf:  _ShiftTableDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {1F79777E-9628-326F-82D0-DB3A67DFF466}
// *********************************************************************//
  _ShiftTableDisp = dispinterface
    ['{1F79777E-9628-326F-82D0-DB3A67DFF466}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property Inicio: WideString dispid 1610743812;
    property Fim: WideString dispid 1610743814;
  end;

// *********************************************************************//
// Interface: _JourneyWorking
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {22A9FA9D-D34A-38C6-AE62-15ACFB57A842}
// *********************************************************************//
  _JourneyWorking = interface(IDispatch)
    ['{22A9FA9D-D34A-38C6-AE62-15ACFB57A842}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Set_Id(pRetVal: Smallint); safecall;
    function Get_Id: Smallint; safecall;
    procedure Set_TypeWorking(pRetVal: TypeWorking); safecall;
    function Get_TypeWorking: TypeWorking; safecall;
    property ToString: WideString read Get_ToString;
    property Id: Smallint read Get_Id write Set_Id;
    property TypeWorking: TypeWorking read Get_TypeWorking write Set_TypeWorking;
  end;

// *********************************************************************//
// DispIntf:  _JourneyWorkingDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {22A9FA9D-D34A-38C6-AE62-15ACFB57A842}
// *********************************************************************//
  _JourneyWorkingDisp = dispinterface
    ['{22A9FA9D-D34A-38C6-AE62-15ACFB57A842}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property Id: Smallint dispid 1610743812;
    property TypeWorking: TypeWorking dispid 1610743814;
  end;

// *********************************************************************//
// Interface: _PeriodicJourneyWorking
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {5BE9179F-FF46-311D-AB60-AD28A14668B0}
// *********************************************************************//
  _PeriodicJourneyWorking = interface(IDispatch)
    ['{5BE9179F-FF46-311D-AB60-AD28A14668B0}']
  end;

// *********************************************************************//
// DispIntf:  _PeriodicJourneyWorkingDisp
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {5BE9179F-FF46-311D-AB60-AD28A14668B0}
// *********************************************************************//
  _PeriodicJourneyWorkingDisp = dispinterface
    ['{5BE9179F-FF46-311D-AB60-AD28A14668B0}']
  end;

// *********************************************************************//
// Interface: _AbstractJourneyWorking
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {FD36CE33-4444-36F9-8EB8-D2705F5C4B98}
// *********************************************************************//
  _AbstractJourneyWorking = interface(IDispatch)
    ['{FD36CE33-4444-36F9-8EB8-D2705F5C4B98}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_Working: _JourneyWorking; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property Working: _JourneyWorking read Get_Working;
  end;

// *********************************************************************//
// DispIntf:  _AbstractJourneyWorkingDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {FD36CE33-4444-36F9-8EB8-D2705F5C4B98}
// *********************************************************************//
  _AbstractJourneyWorkingDisp = dispinterface
    ['{FD36CE33-4444-36F9-8EB8-D2705F5C4B98}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property Working: _JourneyWorking readonly dispid 1610743816;
  end;

// *********************************************************************//
// Interface: _AbstractPunchMessage
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {531CA190-5419-31B8-871F-4F9270F2DAB1}
// *********************************************************************//
  _AbstractPunchMessage = interface(IDispatch)
    ['{531CA190-5419-31B8-871F-4F9270F2DAB1}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_Id: Largeuint; safecall;
    procedure Set_Id(pRetVal: Largeuint); safecall;
    function Get_date: TDateTime; safecall;
    procedure Set_date(pRetVal: TDateTime); safecall;
    function Get_WatchEvent: LongWord; safecall;
    procedure Set_WatchEvent(pRetVal: LongWord); safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property Id: Largeuint read Get_Id write Set_Id;
    property date: TDateTime read Get_date write Set_date;
    property WatchEvent: LongWord read Get_WatchEvent write Set_WatchEvent;
  end;

// *********************************************************************//
// DispIntf:  _AbstractPunchMessageDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {531CA190-5419-31B8-871F-4F9270F2DAB1}
// *********************************************************************//
  _AbstractPunchMessageDisp = dispinterface
    ['{531CA190-5419-31B8-871F-4F9270F2DAB1}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property Id: {??Largeuint}OleVariant dispid 1610743816;
    property date: TDateTime dispid 1610743818;
    property WatchEvent: LongWord dispid 1610743820;
  end;

// *********************************************************************//
// Interface: _MRPRecord_ChangeCompanyIdentification
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {845262DE-FFD3-3CAF-BED8-2C6484BC1DFC}
// *********************************************************************//
  _MRPRecord_ChangeCompanyIdentification = interface(IDispatch)
    ['{845262DE-FFD3-3CAF-BED8-2C6484BC1DFC}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_nsr: WideString; safecall;
    procedure Set_nsr(const pRetVal: WideString); safecall;
    function Get_RecordingDateTime: TDateTime; safecall;
    function Get_employerType: EmployeerType; safecall;
    function Get_cpf_cnpj: WideString; safecall;
    function Get_cei: WideString; safecall;
    function Get_name: WideString; safecall;
    function Get_address: WideString; safecall;
    property ToString: WideString read Get_ToString;
    property nsr: WideString read Get_nsr write Set_nsr;
    property RecordingDateTime: TDateTime read Get_RecordingDateTime;
    property employerType: EmployeerType read Get_employerType;
    property cpf_cnpj: WideString read Get_cpf_cnpj;
    property cei: WideString read Get_cei;
    property name: WideString read Get_name;
    property address: WideString read Get_address;
  end;

// *********************************************************************//
// DispIntf:  _MRPRecord_ChangeCompanyIdentificationDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {845262DE-FFD3-3CAF-BED8-2C6484BC1DFC}
// *********************************************************************//
  _MRPRecord_ChangeCompanyIdentificationDisp = dispinterface
    ['{845262DE-FFD3-3CAF-BED8-2C6484BC1DFC}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property nsr: WideString dispid 1610743812;
    property RecordingDateTime: TDateTime readonly dispid 1610743814;
    property employerType: EmployeerType readonly dispid 1610743815;
    property cpf_cnpj: WideString readonly dispid 1610743816;
    property cei: WideString readonly dispid 1610743817;
    property name: WideString readonly dispid 1610743818;
    property address: WideString readonly dispid 1610743819;
  end;

// *********************************************************************//
// Interface: _AbstractPacket
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {5A8483F9-6890-3899-A342-7A9BFEBD2881}
// *********************************************************************//
  _AbstractPacket = interface(IDispatch)
    ['{5A8483F9-6890-3899-A342-7A9BFEBD2881}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetFunction: Byte; safecall;
    function GetPacket: PSafeArray; safecall;
    function isPacketValid: WordBool; safecall;
    function GetSpecificFirstPacket(var data: PSafeArray; checkMessageNumber: Byte): PSafeArray; safecall;
    function Get_StartID: Byte; safecall;
    procedure Set_StartID(pRetVal: Byte); safecall;
    function Get_Function_: MessageType; safecall;
    procedure Set_Function_(pRetVal: MessageType); safecall;
    function Get_data: _AbstractMessage; safecall;
    procedure _Set_data(const pRetVal: _AbstractMessage); safecall;
    function Get_CheckSum: Byte; safecall;
    function Get_WatchAddress: Integer; safecall;
    procedure Set_WatchAddress(pRetVal: Integer); safecall;
    function Get_TimeStamp: Byte; safecall;
    procedure Set_TimeStamp(pRetVal: Byte); safecall;
    property ToString: WideString read Get_ToString;
    property StartID: Byte read Get_StartID write Set_StartID;
    property Function_: MessageType read Get_Function_ write Set_Function_;
    property data: _AbstractMessage read Get_data write _Set_data;
    property CheckSum: Byte read Get_CheckSum;
    property WatchAddress: Integer read Get_WatchAddress write Set_WatchAddress;
    property TimeStamp: Byte read Get_TimeStamp write Set_TimeStamp;
  end;

// *********************************************************************//
// DispIntf:  _AbstractPacketDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {5A8483F9-6890-3899-A342-7A9BFEBD2881}
// *********************************************************************//
  _AbstractPacketDisp = dispinterface
    ['{5A8483F9-6890-3899-A342-7A9BFEBD2881}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetFunction: Byte; dispid 1610743812;
    function GetPacket: {??PSafeArray}OleVariant; dispid 1610743813;
    function isPacketValid: WordBool; dispid 1610743814;
    function GetSpecificFirstPacket(var data: {??PSafeArray}OleVariant; checkMessageNumber: Byte): {??PSafeArray}OleVariant; dispid 1610743815;
    property StartID: Byte dispid 1610743816;
    property Function_: MessageType dispid 1610743818;
    property data: _AbstractMessage dispid 1610743820;
    property CheckSum: Byte readonly dispid 1610743822;
    property WatchAddress: Integer dispid 1610743823;
    property TimeStamp: Byte dispid 1610743825;
  end;

// *********************************************************************//
// Interface: _MemoryFormat
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {CDCA2E43-A823-37CC-8262-ECF3BCC77DA3}
// *********************************************************************//
  _MemoryFormat = interface(IDispatch)
    ['{CDCA2E43-A823-37CC-8262-ECF3BCC77DA3}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Set_MinDigitCard(pRetVal: Byte); safecall;
    function Get_MinDigitCard: Byte; safecall;
    procedure Set_MaxDigitCard(pRetVal: Byte); safecall;
    function Get_MaxDigitCard: Byte; safecall;
    procedure Set_HasWork(pRetVal: WordBool); safecall;
    function Get_HasWork: WordBool; safecall;
    procedure Set_HasMessage(pRetVal: WordBool); safecall;
    function Get_HasMessage: WordBool; safecall;
    procedure Set_HasWay(pRetVal: WordBool); safecall;
    function Get_HasWay: WordBool; safecall;
    procedure Set_HasPassword(pRetVal: WordBool); safecall;
    function Get_HasPassword: WordBool; safecall;
    procedure Set_CounterAccess(pRetVal: Byte); safecall;
    function Get_CounterAccess: Byte; safecall;
    procedure Set_QuantityMaxCard(pRetVal: Smallint); safecall;
    function Get_QuantityMaxCard: Smallint; safecall;
    procedure Set_QuantityMaxAlternativeId(pRetVal: Smallint); safecall;
    function Get_QuantityMaxAlternativeId: Smallint; safecall;
    procedure Set_QuantityMaxWeeklyWork(pRetVal: Smallint); safecall;
    function Get_QuantityMaxWeeklyWork: Smallint; safecall;
    procedure Set_QuantityMaxMonthlyWork(pRetVal: Smallint); safecall;
    function Get_QuantityMaxMonthlyWork: Smallint; safecall;
    procedure Set_QuantityMaxPeriodicWork(pRetVal: Smallint); safecall;
    function Get_QuantityMaxPeriodicWork: Smallint; safecall;
    procedure Set_QuantityMaxAlarmRing(pRetVal: Byte); safecall;
    function Get_QuantityMaxAlarmRing: Byte; safecall;
    procedure Set_QuantityMaxShiftTable(pRetVal: Byte); safecall;
    function Get_QuantityMaxShiftTable: Byte; safecall;
    procedure Set_QuantityMaxHoliday(pRetVal: Byte); safecall;
    function Get_QuantityMaxHoliday: Byte; safecall;
    procedure Set_QuantityMaxFunction(pRetVal: Byte); safecall;
    function Get_QuantityMaxFunction: Byte; safecall;
    procedure Set_MaxMessageUser(pRetVal: Byte); safecall;
    function Get_MaxMessageUser: Byte; safecall;
    procedure Set_TypeCheck(pRetVal: TypeCheckCard); safecall;
    function Get_TypeCheck: TypeCheckCard; safecall;
    property ToString: WideString read Get_ToString;
    property MinDigitCard: Byte read Get_MinDigitCard write Set_MinDigitCard;
    property MaxDigitCard: Byte read Get_MaxDigitCard write Set_MaxDigitCard;
    property HasWork: WordBool read Get_HasWork write Set_HasWork;
    property HasMessage: WordBool read Get_HasMessage write Set_HasMessage;
    property HasWay: WordBool read Get_HasWay write Set_HasWay;
    property HasPassword: WordBool read Get_HasPassword write Set_HasPassword;
    property CounterAccess: Byte read Get_CounterAccess write Set_CounterAccess;
    property QuantityMaxCard: Smallint read Get_QuantityMaxCard write Set_QuantityMaxCard;
    property QuantityMaxAlternativeId: Smallint read Get_QuantityMaxAlternativeId write Set_QuantityMaxAlternativeId;
    property QuantityMaxWeeklyWork: Smallint read Get_QuantityMaxWeeklyWork write Set_QuantityMaxWeeklyWork;
    property QuantityMaxMonthlyWork: Smallint read Get_QuantityMaxMonthlyWork write Set_QuantityMaxMonthlyWork;
    property QuantityMaxPeriodicWork: Smallint read Get_QuantityMaxPeriodicWork write Set_QuantityMaxPeriodicWork;
    property QuantityMaxAlarmRing: Byte read Get_QuantityMaxAlarmRing write Set_QuantityMaxAlarmRing;
    property QuantityMaxShiftTable: Byte read Get_QuantityMaxShiftTable write Set_QuantityMaxShiftTable;
    property QuantityMaxHoliday: Byte read Get_QuantityMaxHoliday write Set_QuantityMaxHoliday;
    property QuantityMaxFunction: Byte read Get_QuantityMaxFunction write Set_QuantityMaxFunction;
    property MaxMessageUser: Byte read Get_MaxMessageUser write Set_MaxMessageUser;
    property TypeCheck: TypeCheckCard read Get_TypeCheck write Set_TypeCheck;
  end;

// *********************************************************************//
// DispIntf:  _MemoryFormatDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {CDCA2E43-A823-37CC-8262-ECF3BCC77DA3}
// *********************************************************************//
  _MemoryFormatDisp = dispinterface
    ['{CDCA2E43-A823-37CC-8262-ECF3BCC77DA3}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property MinDigitCard: Byte dispid 1610743812;
    property MaxDigitCard: Byte dispid 1610743814;
    property HasWork: WordBool dispid 1610743816;
    property HasMessage: WordBool dispid 1610743818;
    property HasWay: WordBool dispid 1610743820;
    property HasPassword: WordBool dispid 1610743822;
    property CounterAccess: Byte dispid 1610743824;
    property QuantityMaxCard: Smallint dispid 1610743826;
    property QuantityMaxAlternativeId: Smallint dispid 1610743828;
    property QuantityMaxWeeklyWork: Smallint dispid 1610743830;
    property QuantityMaxMonthlyWork: Smallint dispid 1610743832;
    property QuantityMaxPeriodicWork: Smallint dispid 1610743834;
    property QuantityMaxAlarmRing: Byte dispid 1610743836;
    property QuantityMaxShiftTable: Byte dispid 1610743838;
    property QuantityMaxHoliday: Byte dispid 1610743840;
    property QuantityMaxFunction: Byte dispid 1610743842;
    property MaxMessageUser: Byte dispid 1610743844;
    property TypeCheck: TypeCheckCard dispid 1610743846;
  end;

// *********************************************************************//
// Interface: _PrintPointFingerPrintMessage
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {34023D0E-D605-3B1B-9825-EA9F802AF38D}
// *********************************************************************//
  _PrintPointFingerPrintMessage = interface(IDispatch)
    ['{34023D0E-D605-3B1B-9825-EA9F802AF38D}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_fingerPrint: WideString; safecall;
    procedure Set_fingerPrint(const pRetVal: WideString); safecall;
    function Get_fingerPrintTypeOne: EFingerPrintType; safecall;
    procedure Set_fingerPrintTypeOne(pRetVal: EFingerPrintType); safecall;
    function Get_fingerPrintTypeTwo: EFingerPrintType; safecall;
    procedure Set_fingerPrintTypeTwo(pRetVal: EFingerPrintType); safecall;
    function Get_fingerPrintHandOne: EFingerPrintHand; safecall;
    procedure Set_fingerPrintHandOne(pRetVal: EFingerPrintHand); safecall;
    function Get_fingerPrintHandTwo: EFingerPrintHand; safecall;
    procedure Set_fingerPrintHandTwo(pRetVal: EFingerPrintHand); safecall;
    function Get_FingerPrintSensor: EfingerPrintSensor; safecall;
    procedure Set_FingerPrintSensor(pRetVal: EfingerPrintSensor); safecall;
    function Get_BlockSize: Smallint; safecall;
    procedure Set_BlockSize(pRetVal: Smallint); safecall;
    function Get_pis: WideString; safecall;
    procedure Set_pis(const pRetVal: WideString); safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property fingerPrint: WideString read Get_fingerPrint write Set_fingerPrint;
    property fingerPrintTypeOne: EFingerPrintType read Get_fingerPrintTypeOne write Set_fingerPrintTypeOne;
    property fingerPrintTypeTwo: EFingerPrintType read Get_fingerPrintTypeTwo write Set_fingerPrintTypeTwo;
    property fingerPrintHandOne: EFingerPrintHand read Get_fingerPrintHandOne write Set_fingerPrintHandOne;
    property fingerPrintHandTwo: EFingerPrintHand read Get_fingerPrintHandTwo write Set_fingerPrintHandTwo;
    property FingerPrintSensor: EfingerPrintSensor read Get_FingerPrintSensor write Set_FingerPrintSensor;
    property BlockSize: Smallint read Get_BlockSize write Set_BlockSize;
    property pis: WideString read Get_pis write Set_pis;
  end;

// *********************************************************************//
// DispIntf:  _PrintPointFingerPrintMessageDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {34023D0E-D605-3B1B-9825-EA9F802AF38D}
// *********************************************************************//
  _PrintPointFingerPrintMessageDisp = dispinterface
    ['{34023D0E-D605-3B1B-9825-EA9F802AF38D}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property fingerPrint: WideString dispid 1610743816;
    property fingerPrintTypeOne: EFingerPrintType dispid 1610743818;
    property fingerPrintTypeTwo: EFingerPrintType dispid 1610743820;
    property fingerPrintHandOne: EFingerPrintHand dispid 1610743822;
    property fingerPrintHandTwo: EFingerPrintHand dispid 1610743824;
    property FingerPrintSensor: EfingerPrintSensor dispid 1610743826;
    property BlockSize: Smallint dispid 1610743828;
    property pis: WideString dispid 1610743830;
  end;

// *********************************************************************//
// Interface: _BioPointStatusMessage
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {67DE066A-5095-34D2-B29F-F36E0C07311A}
// *********************************************************************//
  _BioPointStatusMessage = interface(IDispatch)
    ['{67DE066A-5095-34D2-B29F-F36E0C07311A}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_firmwareVersion: WideString; safecall;
    procedure Set_firmwareVersion(const pRetVal: WideString); safecall;
    function Get_RecordsSize: LongWord; safecall;
    procedure Set_RecordsSize(pRetVal: LongWord); safecall;
    function Get_RecordsCount: LongWord; safecall;
    procedure Set_RecordsCount(pRetVal: LongWord); safecall;
    function Get_CardListSize: LongWord; safecall;
    procedure Set_CardListSize(pRetVal: LongWord); safecall;
    function Get_date: TDateTime; safecall;
    procedure Set_date(pRetVal: TDateTime); safecall;
    function Get_CheckType: Byte; safecall;
    procedure Set_CheckType(pRetVal: Byte); safecall;
    function Get_RecordDeniedAccess: WordBool; safecall;
    procedure Set_RecordDeniedAccess(pRetVal: WordBool); safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property firmwareVersion: WideString read Get_firmwareVersion write Set_firmwareVersion;
    property RecordsSize: LongWord read Get_RecordsSize write Set_RecordsSize;
    property RecordsCount: LongWord read Get_RecordsCount write Set_RecordsCount;
    property CardListSize: LongWord read Get_CardListSize write Set_CardListSize;
    property date: TDateTime read Get_date write Set_date;
    property CheckType: Byte read Get_CheckType write Set_CheckType;
    property RecordDeniedAccess: WordBool read Get_RecordDeniedAccess write Set_RecordDeniedAccess;
  end;

// *********************************************************************//
// DispIntf:  _BioPointStatusMessageDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {67DE066A-5095-34D2-B29F-F36E0C07311A}
// *********************************************************************//
  _BioPointStatusMessageDisp = dispinterface
    ['{67DE066A-5095-34D2-B29F-F36E0C07311A}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property firmwareVersion: WideString dispid 1610743816;
    property RecordsSize: LongWord dispid 1610743818;
    property RecordsCount: LongWord dispid 1610743820;
    property CardListSize: LongWord dispid 1610743822;
    property date: TDateTime dispid 1610743824;
    property CheckType: Byte dispid 1610743826;
    property RecordDeniedAccess: WordBool dispid 1610743828;
  end;

// *********************************************************************//
// Interface: _FaceEmployee
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {8C0B6C5B-7411-3189-AE2E-31DC47B9F801}
// *********************************************************************//
  _FaceEmployee = interface(IDispatch)
    ['{8C0B6C5B-7411-3189-AE2E-31DC47B9F801}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_employeeID: Integer; safecall;
    procedure Set_employeeID(pRetVal: Integer); safecall;
    function Get_name: WideString; safecall;
    procedure Set_name(const pRetVal: WideString); safecall;
    function Get_password: WideString; safecall;
    procedure Set_password(const pRetVal: WideString); safecall;
    function Get_Credential: WideString; safecall;
    procedure Set_Credential(const pRetVal: WideString); safecall;
    function Get_isMaster: WordBool; safecall;
    procedure Set_isMaster(pRetVal: WordBool); safecall;
    property ToString: WideString read Get_ToString;
    property employeeID: Integer read Get_employeeID write Set_employeeID;
    property name: WideString read Get_name write Set_name;
    property password: WideString read Get_password write Set_password;
    property Credential: WideString read Get_Credential write Set_Credential;
    property isMaster: WordBool read Get_isMaster write Set_isMaster;
  end;

// *********************************************************************//
// DispIntf:  _FaceEmployeeDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {8C0B6C5B-7411-3189-AE2E-31DC47B9F801}
// *********************************************************************//
  _FaceEmployeeDisp = dispinterface
    ['{8C0B6C5B-7411-3189-AE2E-31DC47B9F801}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property employeeID: Integer dispid 1610743812;
    property name: WideString dispid 1610743814;
    property password: WideString dispid 1610743816;
    property Credential: WideString dispid 1610743818;
    property isMaster: WordBool dispid 1610743820;
  end;

// *********************************************************************//
// Interface: _Holiday
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {8AF993AB-1F73-3772-B3B2-B2AB09B689F8}
// *********************************************************************//
  _Holiday = interface(IDispatch)
    ['{8AF993AB-1F73-3772-B3B2-B2AB09B689F8}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Set_day(pRetVal: Byte); safecall;
    function Get_day: Byte; safecall;
    procedure Set_month(pRetVal: Byte); safecall;
    function Get_month: Byte; safecall;
    property ToString: WideString read Get_ToString;
    property day: Byte read Get_day write Set_day;
    property month: Byte read Get_month write Set_month;
  end;

// *********************************************************************//
// DispIntf:  _HolidayDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {8AF993AB-1F73-3772-B3B2-B2AB09B689F8}
// *********************************************************************//
  _HolidayDisp = dispinterface
    ['{8AF993AB-1F73-3772-B3B2-B2AB09B689F8}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property day: Byte dispid 1610743812;
    property month: Byte dispid 1610743814;
  end;

// *********************************************************************//
// Interface: _RandomNumberResponse
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {DAC90B55-9C21-32E2-B1F7-B459B181F365}
// *********************************************************************//
  _RandomNumberResponse = interface(IDispatch)
    ['{DAC90B55-9C21-32E2-B1F7-B459B181F365}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_RandomNumber: Int64; safecall;
    function Get_plateSerialNumber: Int64; safecall;
    function Get_mrpSerialNumber: Int64; safecall;
    function Get_mrpSealNumber: Int64; safecall;
    function Get_MACAddress: WideString; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property RandomNumber: Int64 read Get_RandomNumber;
    property plateSerialNumber: Int64 read Get_plateSerialNumber;
    property mrpSerialNumber: Int64 read Get_mrpSerialNumber;
    property mrpSealNumber: Int64 read Get_mrpSealNumber;
    property MACAddress: WideString read Get_MACAddress;
  end;

// *********************************************************************//
// DispIntf:  _RandomNumberResponseDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {DAC90B55-9C21-32E2-B1F7-B459B181F365}
// *********************************************************************//
  _RandomNumberResponseDisp = dispinterface
    ['{DAC90B55-9C21-32E2-B1F7-B459B181F365}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property RandomNumber: {??Int64}OleVariant readonly dispid 1610743816;
    property plateSerialNumber: {??Int64}OleVariant readonly dispid 1610743817;
    property mrpSerialNumber: {??Int64}OleVariant readonly dispid 1610743818;
    property mrpSealNumber: {??Int64}OleVariant readonly dispid 1610743819;
    property MACAddress: WideString readonly dispid 1610743820;
  end;

// *********************************************************************//
// Interface: _PrintPointStatusMessage
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {B498CB3E-F458-3EEB-A9F0-20A90D3D6591}
// *********************************************************************//
  _PrintPointStatusMessage = interface(IDispatch)
    ['{B498CB3E-F458-3EEB-A9F0-20A90D3D6591}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_DSTStart: TDateTime; safecall;
    function Get_dstEnd: TDateTime; safecall;
    function Get_Enabled_2Of5Intercalary: WordBool; safecall;
    function Get_Enabled_2of5Dimep: WordBool; safecall;
    function Get_Enabled_3Of9: WordBool; safecall;
    function Get_Enabled_MagneticDIMEP: WordBool; safecall;
    function Get_Enabled_ABA: WordBool; safecall;
    function Get_Enabled_Wiegand26Bits: WordBool; safecall;
    function Get_Enabled_Wiegand34Bits: WordBool; safecall;
    function Get_Enabled_Ean13: WordBool; safecall;
    function Get_Enabled_Wiegand35Bits: WordBool; safecall;
    function Get_Enabled_SpecialWiegand32Bits: WordBool; safecall;
    function Get_EnabledWiegandParityRead: WordBool; safecall;
    function Get_SmartCardUseType: EPrintPointSmartCardUseType; safecall;
    function Get_Wiegand37BitsUseType: EPrintPointWiegand37BitsUseType; safecall;
    function Get_SmartCardSector: Smallint; safecall;
    function Get_SmartCardBlock: Smallint; safecall;
    function Get_SmartCardOffSet: Smallint; safecall;
    function Get_SmartCardDigitsNumber: Smallint; safecall;
    function Get_Format_2Of5Intercalary: WideString; safecall;
    function Get_Format_2of5Dimep: WideString; safecall;
    function Get_Format_3Of9: WideString; safecall;
    function Get_Format_MagneticDIMEP: WideString; safecall;
    function Get_Format_ABA: WideString; safecall;
    function Get_Format_Wiegand26Bits: WideString; safecall;
    function Get_Format_Wiegand34Bits: WideString; safecall;
    function Get_Format_Ean13: WideString; safecall;
    function Get_Format_Wiegand35Bits: WideString; safecall;
    function Get_Format_Wiegand37Bits: WideString; safecall;
    function Get_Format_SmartCard: WideString; safecall;
    function Get_EncryptionType: EPrintPointEncryptionType; safecall;
    function Get_Vector1: WideString; safecall;
    function Get_Vector2: WideString; safecall;
    function Get_Checker1: WideString; safecall;
    function Get_Checker2: WideString; safecall;
    function Get_SpecialFormatMagneticDimep1: WordBool; safecall;
    function Get_SpecialFormatABA1: WordBool; safecall;
    function Get_PersonalizationType: EPrintPointPersonalizationType; safecall;
    procedure Set_PersonalizationType(pRetVal: EPrintPointPersonalizationType); safecall;
    function Get_PersonalizationDigitsNumber: Integer; safecall;
    procedure Set_PersonalizationDigitsNumber(pRetVal: Integer); safecall;
    function Get_PersonalizationCode: Integer; safecall;
    procedure Set_PersonalizationCode(pRetVal: Integer); safecall;
    function Get_Card_Enabled: WordBool; safecall;
    function Get_Card_AccessType: EPrintPointAccessType; safecall;
    function Get_Card_AuthenticationType: EPrintPointAuthenticationType; safecall;
    function Get_ShowCredentialDigitsInDisplay: Smallint; safecall;
    function Get_KeyBoard_Enabled: WordBool; safecall;
    function Get_KeyBoard_AccessType: EPrintPointAccessType; safecall;
    function Get_KeyBoard_AuthenticationType: EPrintPointAuthenticationType; safecall;
    function Get_Identification_Enabled: WordBool; safecall;
    function Get_Identification_AuthenticationTypeIdentification: EPrintPointAuthenticationTypeIdentification; safecall;
    function Get_Authentication: EPrintPointBiometricAuthenticationType; safecall;
    function Get_EmployeesCapacity: Integer; safecall;
    function Get_EmployeesOccupation: Integer; safecall;
    function Get_CredentialsCapacity: Integer; safecall;
    function Get_CredentialsOccupation: Integer; safecall;
    function Get_FingerprintCapacity: Integer; safecall;
    function Get_FingerprintOccupation: Integer; safecall;
    function Get_BiometricModuleCapacity: Integer; safecall;
    function Get_MRPOccupationInClustersPercentage: Integer; safecall;
    function Get_FinallyNSR: WideString; safecall;
    function Get_TotalRecordsPoint: Int64; safecall;
    procedure Set_TotalRecordsPoint(pRetVal: Int64); safecall;
    function Get_RecordsPointToCollect: Int64; safecall;
    procedure Set_RecordsPointToCollect(pRetVal: Int64); safecall;
    function Get_MasterCapacity: Integer; safecall;
    function Get_MasterOccupation: Integer; safecall;
    function Get_serialNumber: WideString; safecall;
    function Get_SerialNumberPlate: WideString; safecall;
    function Get_mrpSerialNumber: WideString; safecall;
    function Get_mrpSealNumber: WideString; safecall;
    function Get_firmwareVersion: WideString; safecall;
    function Get_MACAddress: WideString; safecall;
    function Get_MRPFirmwareVersion: WideString; safecall;
    function Get_PrinterAdvanceSize: EPrintPointAdvanceSizeType; safecall;
    function Get_SecurityLevelSuprema: Byte; safecall;
    function Get_SecurityLevelSagem: Byte; safecall;
    function Get_EnergyPrinter: EPrintPointEnergyPrinter; safecall;
    function Get_LenghtBobbin: Byte; safecall;
    function Get_LevelBattery: Byte; safecall;
    function Get_TimeUtilyBattery: Integer; safecall;
    function Get_PrinterCutType: EPrintPointCutType; safecall;
    function Get_DataAndTime: TDateTime; safecall;
    function Get_BobbinState: BobbinStateType; safecall;
    function Get_HasNewFingerprints: WordBool; safecall;
    function Get_HasMarkingPoints: WordBool; safecall;
    function Get_MRPState: MRPStateType; safecall;
    function Get_REPState: REPStateType; safecall;
    function Get_AFDGeneration: AFDGenerationType; safecall;
    function Get_RIMGeneration: RIMGenerationType; safecall;
    procedure Set_RIMGeneration(pRetVal: RIMGenerationType); safecall;
    function Get_AlimentationType: AlimentationType; safecall;
    procedure Set_AlimentationType(pRetVal: AlimentationType); safecall;
    function Get_BatteryLevel: BatteryLevel; safecall;
    procedure Set_BatteryLevel(pRetVal: BatteryLevel); safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property DSTStart: TDateTime read Get_DSTStart;
    property dstEnd: TDateTime read Get_dstEnd;
    property Enabled_2Of5Intercalary: WordBool read Get_Enabled_2Of5Intercalary;
    property Enabled_2of5Dimep: WordBool read Get_Enabled_2of5Dimep;
    property Enabled_3Of9: WordBool read Get_Enabled_3Of9;
    property Enabled_MagneticDIMEP: WordBool read Get_Enabled_MagneticDIMEP;
    property Enabled_ABA: WordBool read Get_Enabled_ABA;
    property Enabled_Wiegand26Bits: WordBool read Get_Enabled_Wiegand26Bits;
    property Enabled_Wiegand34Bits: WordBool read Get_Enabled_Wiegand34Bits;
    property Enabled_Ean13: WordBool read Get_Enabled_Ean13;
    property Enabled_Wiegand35Bits: WordBool read Get_Enabled_Wiegand35Bits;
    property Enabled_SpecialWiegand32Bits: WordBool read Get_Enabled_SpecialWiegand32Bits;
    property EnabledWiegandParityRead: WordBool read Get_EnabledWiegandParityRead;
    property SmartCardUseType: EPrintPointSmartCardUseType read Get_SmartCardUseType;
    property Wiegand37BitsUseType: EPrintPointWiegand37BitsUseType read Get_Wiegand37BitsUseType;
    property SmartCardSector: Smallint read Get_SmartCardSector;
    property SmartCardBlock: Smallint read Get_SmartCardBlock;
    property SmartCardOffSet: Smallint read Get_SmartCardOffSet;
    property SmartCardDigitsNumber: Smallint read Get_SmartCardDigitsNumber;
    property Format_2Of5Intercalary: WideString read Get_Format_2Of5Intercalary;
    property Format_2of5Dimep: WideString read Get_Format_2of5Dimep;
    property Format_3Of9: WideString read Get_Format_3Of9;
    property Format_MagneticDIMEP: WideString read Get_Format_MagneticDIMEP;
    property Format_ABA: WideString read Get_Format_ABA;
    property Format_Wiegand26Bits: WideString read Get_Format_Wiegand26Bits;
    property Format_Wiegand34Bits: WideString read Get_Format_Wiegand34Bits;
    property Format_Ean13: WideString read Get_Format_Ean13;
    property Format_Wiegand35Bits: WideString read Get_Format_Wiegand35Bits;
    property Format_Wiegand37Bits: WideString read Get_Format_Wiegand37Bits;
    property Format_SmartCard: WideString read Get_Format_SmartCard;
    property EncryptionType: EPrintPointEncryptionType read Get_EncryptionType;
    property Vector1: WideString read Get_Vector1;
    property Vector2: WideString read Get_Vector2;
    property Checker1: WideString read Get_Checker1;
    property Checker2: WideString read Get_Checker2;
    property SpecialFormatMagneticDimep1: WordBool read Get_SpecialFormatMagneticDimep1;
    property SpecialFormatABA1: WordBool read Get_SpecialFormatABA1;
    property PersonalizationType: EPrintPointPersonalizationType read Get_PersonalizationType write Set_PersonalizationType;
    property PersonalizationDigitsNumber: Integer read Get_PersonalizationDigitsNumber write Set_PersonalizationDigitsNumber;
    property PersonalizationCode: Integer read Get_PersonalizationCode write Set_PersonalizationCode;
    property Card_Enabled: WordBool read Get_Card_Enabled;
    property Card_AccessType: EPrintPointAccessType read Get_Card_AccessType;
    property Card_AuthenticationType: EPrintPointAuthenticationType read Get_Card_AuthenticationType;
    property ShowCredentialDigitsInDisplay: Smallint read Get_ShowCredentialDigitsInDisplay;
    property KeyBoard_Enabled: WordBool read Get_KeyBoard_Enabled;
    property KeyBoard_AccessType: EPrintPointAccessType read Get_KeyBoard_AccessType;
    property KeyBoard_AuthenticationType: EPrintPointAuthenticationType read Get_KeyBoard_AuthenticationType;
    property Identification_Enabled: WordBool read Get_Identification_Enabled;
    property Identification_AuthenticationTypeIdentification: EPrintPointAuthenticationTypeIdentification read Get_Identification_AuthenticationTypeIdentification;
    property Authentication: EPrintPointBiometricAuthenticationType read Get_Authentication;
    property EmployeesCapacity: Integer read Get_EmployeesCapacity;
    property EmployeesOccupation: Integer read Get_EmployeesOccupation;
    property CredentialsCapacity: Integer read Get_CredentialsCapacity;
    property CredentialsOccupation: Integer read Get_CredentialsOccupation;
    property FingerprintCapacity: Integer read Get_FingerprintCapacity;
    property FingerprintOccupation: Integer read Get_FingerprintOccupation;
    property BiometricModuleCapacity: Integer read Get_BiometricModuleCapacity;
    property MRPOccupationInClustersPercentage: Integer read Get_MRPOccupationInClustersPercentage;
    property FinallyNSR: WideString read Get_FinallyNSR;
    property TotalRecordsPoint: Int64 read Get_TotalRecordsPoint write Set_TotalRecordsPoint;
    property RecordsPointToCollect: Int64 read Get_RecordsPointToCollect write Set_RecordsPointToCollect;
    property MasterCapacity: Integer read Get_MasterCapacity;
    property MasterOccupation: Integer read Get_MasterOccupation;
    property serialNumber: WideString read Get_serialNumber;
    property SerialNumberPlate: WideString read Get_SerialNumberPlate;
    property mrpSerialNumber: WideString read Get_mrpSerialNumber;
    property mrpSealNumber: WideString read Get_mrpSealNumber;
    property firmwareVersion: WideString read Get_firmwareVersion;
    property MACAddress: WideString read Get_MACAddress;
    property MRPFirmwareVersion: WideString read Get_MRPFirmwareVersion;
    property PrinterAdvanceSize: EPrintPointAdvanceSizeType read Get_PrinterAdvanceSize;
    property SecurityLevelSuprema: Byte read Get_SecurityLevelSuprema;
    property SecurityLevelSagem: Byte read Get_SecurityLevelSagem;
    property EnergyPrinter: EPrintPointEnergyPrinter read Get_EnergyPrinter;
    property LenghtBobbin: Byte read Get_LenghtBobbin;
    property LevelBattery: Byte read Get_LevelBattery;
    property TimeUtilyBattery: Integer read Get_TimeUtilyBattery;
    property PrinterCutType: EPrintPointCutType read Get_PrinterCutType;
    property DataAndTime: TDateTime read Get_DataAndTime;
    property BobbinState: BobbinStateType read Get_BobbinState;
    property HasNewFingerprints: WordBool read Get_HasNewFingerprints;
    property HasMarkingPoints: WordBool read Get_HasMarkingPoints;
    property MRPState: MRPStateType read Get_MRPState;
    property REPState: REPStateType read Get_REPState;
    property AFDGeneration: AFDGenerationType read Get_AFDGeneration;
    property RIMGeneration: RIMGenerationType read Get_RIMGeneration write Set_RIMGeneration;
    property AlimentationType: AlimentationType read Get_AlimentationType write Set_AlimentationType;
    property BatteryLevel: BatteryLevel read Get_BatteryLevel write Set_BatteryLevel;
  end;

// *********************************************************************//
// DispIntf:  _PrintPointStatusMessageDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {B498CB3E-F458-3EEB-A9F0-20A90D3D6591}
// *********************************************************************//
  _PrintPointStatusMessageDisp = dispinterface
    ['{B498CB3E-F458-3EEB-A9F0-20A90D3D6591}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property DSTStart: TDateTime readonly dispid 1610743816;
    property dstEnd: TDateTime readonly dispid 1610743817;
    property Enabled_2Of5Intercalary: WordBool readonly dispid 1610743818;
    property Enabled_2of5Dimep: WordBool readonly dispid 1610743819;
    property Enabled_3Of9: WordBool readonly dispid 1610743820;
    property Enabled_MagneticDIMEP: WordBool readonly dispid 1610743821;
    property Enabled_ABA: WordBool readonly dispid 1610743822;
    property Enabled_Wiegand26Bits: WordBool readonly dispid 1610743823;
    property Enabled_Wiegand34Bits: WordBool readonly dispid 1610743824;
    property Enabled_Ean13: WordBool readonly dispid 1610743825;
    property Enabled_Wiegand35Bits: WordBool readonly dispid 1610743826;
    property Enabled_SpecialWiegand32Bits: WordBool readonly dispid 1610743827;
    property EnabledWiegandParityRead: WordBool readonly dispid 1610743828;
    property SmartCardUseType: EPrintPointSmartCardUseType readonly dispid 1610743829;
    property Wiegand37BitsUseType: EPrintPointWiegand37BitsUseType readonly dispid 1610743830;
    property SmartCardSector: Smallint readonly dispid 1610743831;
    property SmartCardBlock: Smallint readonly dispid 1610743832;
    property SmartCardOffSet: Smallint readonly dispid 1610743833;
    property SmartCardDigitsNumber: Smallint readonly dispid 1610743834;
    property Format_2Of5Intercalary: WideString readonly dispid 1610743835;
    property Format_2of5Dimep: WideString readonly dispid 1610743836;
    property Format_3Of9: WideString readonly dispid 1610743837;
    property Format_MagneticDIMEP: WideString readonly dispid 1610743838;
    property Format_ABA: WideString readonly dispid 1610743839;
    property Format_Wiegand26Bits: WideString readonly dispid 1610743840;
    property Format_Wiegand34Bits: WideString readonly dispid 1610743841;
    property Format_Ean13: WideString readonly dispid 1610743842;
    property Format_Wiegand35Bits: WideString readonly dispid 1610743843;
    property Format_Wiegand37Bits: WideString readonly dispid 1610743844;
    property Format_SmartCard: WideString readonly dispid 1610743845;
    property EncryptionType: EPrintPointEncryptionType readonly dispid 1610743846;
    property Vector1: WideString readonly dispid 1610743847;
    property Vector2: WideString readonly dispid 1610743848;
    property Checker1: WideString readonly dispid 1610743849;
    property Checker2: WideString readonly dispid 1610743850;
    property SpecialFormatMagneticDimep1: WordBool readonly dispid 1610743851;
    property SpecialFormatABA1: WordBool readonly dispid 1610743852;
    property PersonalizationType: EPrintPointPersonalizationType dispid 1610743853;
    property PersonalizationDigitsNumber: Integer dispid 1610743855;
    property PersonalizationCode: Integer dispid 1610743857;
    property Card_Enabled: WordBool readonly dispid 1610743859;
    property Card_AccessType: EPrintPointAccessType readonly dispid 1610743860;
    property Card_AuthenticationType: EPrintPointAuthenticationType readonly dispid 1610743861;
    property ShowCredentialDigitsInDisplay: Smallint readonly dispid 1610743862;
    property KeyBoard_Enabled: WordBool readonly dispid 1610743863;
    property KeyBoard_AccessType: EPrintPointAccessType readonly dispid 1610743864;
    property KeyBoard_AuthenticationType: EPrintPointAuthenticationType readonly dispid 1610743865;
    property Identification_Enabled: WordBool readonly dispid 1610743866;
    property Identification_AuthenticationTypeIdentification: EPrintPointAuthenticationTypeIdentification readonly dispid 1610743867;
    property Authentication: EPrintPointBiometricAuthenticationType readonly dispid 1610743868;
    property EmployeesCapacity: Integer readonly dispid 1610743869;
    property EmployeesOccupation: Integer readonly dispid 1610743870;
    property CredentialsCapacity: Integer readonly dispid 1610743871;
    property CredentialsOccupation: Integer readonly dispid 1610743872;
    property FingerprintCapacity: Integer readonly dispid 1610743873;
    property FingerprintOccupation: Integer readonly dispid 1610743874;
    property BiometricModuleCapacity: Integer readonly dispid 1610743875;
    property MRPOccupationInClustersPercentage: Integer readonly dispid 1610743876;
    property FinallyNSR: WideString readonly dispid 1610743877;
    property TotalRecordsPoint: {??Int64}OleVariant dispid 1610743878;
    property RecordsPointToCollect: {??Int64}OleVariant dispid 1610743880;
    property MasterCapacity: Integer readonly dispid 1610743882;
    property MasterOccupation: Integer readonly dispid 1610743883;
    property serialNumber: WideString readonly dispid 1610743884;
    property SerialNumberPlate: WideString readonly dispid 1610743885;
    property mrpSerialNumber: WideString readonly dispid 1610743886;
    property mrpSealNumber: WideString readonly dispid 1610743887;
    property firmwareVersion: WideString readonly dispid 1610743888;
    property MACAddress: WideString readonly dispid 1610743889;
    property MRPFirmwareVersion: WideString readonly dispid 1610743890;
    property PrinterAdvanceSize: EPrintPointAdvanceSizeType readonly dispid 1610743891;
    property SecurityLevelSuprema: Byte readonly dispid 1610743892;
    property SecurityLevelSagem: Byte readonly dispid 1610743893;
    property EnergyPrinter: EPrintPointEnergyPrinter readonly dispid 1610743894;
    property LenghtBobbin: Byte readonly dispid 1610743895;
    property LevelBattery: Byte readonly dispid 1610743896;
    property TimeUtilyBattery: Integer readonly dispid 1610743897;
    property PrinterCutType: EPrintPointCutType readonly dispid 1610743898;
    property DataAndTime: TDateTime readonly dispid 1610743899;
    property BobbinState: BobbinStateType readonly dispid 1610743900;
    property HasNewFingerprints: WordBool readonly dispid 1610743901;
    property HasMarkingPoints: WordBool readonly dispid 1610743902;
    property MRPState: MRPStateType readonly dispid 1610743903;
    property REPState: REPStateType readonly dispid 1610743904;
    property AFDGeneration: AFDGenerationType readonly dispid 1610743905;
    property RIMGeneration: RIMGenerationType dispid 1610743906;
    property AlimentationType: AlimentationType dispid 1610743908;
    property BatteryLevel: BatteryLevel dispid 1610743910;
  end;

// *********************************************************************//
// Interface: _PrintPointEvent
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {520547F3-7D11-30D7-924C-43A4F2A0E7A2}
// *********************************************************************//
  _PrintPointEvent = interface(IDispatch)
    ['{520547F3-7D11-30D7-924C-43A4F2A0E7A2}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_EventDateTime: TDateTime; safecall;
    function Get_EventType: PrintPointEventType; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property EventDateTime: TDateTime read Get_EventDateTime;
    property EventType: PrintPointEventType read Get_EventType;
  end;

// *********************************************************************//
// DispIntf:  _PrintPointEventDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {520547F3-7D11-30D7-924C-43A4F2A0E7A2}
// *********************************************************************//
  _PrintPointEventDisp = dispinterface
    ['{520547F3-7D11-30D7-924C-43A4F2A0E7A2}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property EventDateTime: TDateTime readonly dispid 1610743816;
    property EventType: PrintPointEventType readonly dispid 1610743817;
  end;

// *********************************************************************//
// Interface: _PrintPointLiEmployee
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {F4BC0867-964B-31E6-925B-09F087E712FF}
// *********************************************************************//
  _PrintPointLiEmployee = interface(IDispatch)
    ['{F4BC0867-964B-31E6-925B-09F087E712FF}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function CompareTo(obj: OleVariant): Integer; safecall;
    function Get_pis: WideString; safecall;
    procedure Set_pis(const pRetVal: WideString); safecall;
    function Get_name: WideString; safecall;
    procedure Set_name(const pRetVal: WideString); safecall;
    function Get_password: WideString; safecall;
    procedure Set_password(const pRetVal: WideString); safecall;
    function Get_CostCenter: Integer; safecall;
    procedure Set_CostCenter(pRetVal: Integer); safecall;
    function Get_employeeID: Integer; safecall;
    procedure Set_employeeID(pRetVal: Integer); safecall;
    function Get_Name_2: WideString; safecall;
    procedure Set_Name_2(const pRetVal: WideString); safecall;
    function Get_Password_2: WideString; safecall;
    procedure Set_Password_2(const pRetVal: WideString); safecall;
    function Get_Credential: WideString; safecall;
    procedure Set_Credential(const pRetVal: WideString); safecall;
    function Get_cpf: WideString; safecall;
    procedure Set_cpf(const pRetVal: WideString); safecall;
    function Get_isMaster: WordBool; safecall;
    procedure Set_isMaster(pRetVal: WordBool); safecall;
    property ToString: WideString read Get_ToString;
    property pis: WideString read Get_pis write Set_pis;
    property name: WideString read Get_name write Set_name;
    property password: WideString read Get_password write Set_password;
    property CostCenter: Integer read Get_CostCenter write Set_CostCenter;
    property employeeID: Integer read Get_employeeID write Set_employeeID;
    property Name_2: WideString read Get_Name_2 write Set_Name_2;
    property Password_2: WideString read Get_Password_2 write Set_Password_2;
    property Credential: WideString read Get_Credential write Set_Credential;
    property cpf: WideString read Get_cpf write Set_cpf;
    property isMaster: WordBool read Get_isMaster write Set_isMaster;
  end;

// *********************************************************************//
// DispIntf:  _PrintPointLiEmployeeDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {F4BC0867-964B-31E6-925B-09F087E712FF}
// *********************************************************************//
  _PrintPointLiEmployeeDisp = dispinterface
    ['{F4BC0867-964B-31E6-925B-09F087E712FF}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function CompareTo(obj: OleVariant): Integer; dispid 1610743812;
    property pis: WideString dispid 1610743813;
    property name: WideString dispid 1610743815;
    property password: WideString dispid 1610743817;
    property CostCenter: Integer dispid 1610743819;
    property employeeID: Integer dispid 1610743821;
    property Name_2: WideString dispid 1610743823;
    property Password_2: WideString dispid 1610743825;
    property Credential: WideString dispid 1610743827;
    property cpf: WideString dispid 1610743829;
    property isMaster: WordBool dispid 1610743831;
  end;

// *********************************************************************//
// Interface: _ParcialConfiguration
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {8C562630-EDE1-31D7-BADF-93A8D27DCE2A}
// *********************************************************************//
  _ParcialConfiguration = interface(IDispatch)
    ['{8C562630-EDE1-31D7-BADF-93A8D27DCE2A}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_configurationType: ParcialConfigurationType; safecall;
    procedure Set_configurationType(pRetVal: ParcialConfigurationType); safecall;
    function Get_field1: OleVariant; safecall;
    procedure _Set_field1(pRetVal: OleVariant); safecall;
    function Get_field2: OleVariant; safecall;
    procedure _Set_field2(pRetVal: OleVariant); safecall;
    procedure ValidateFieldParameters; safecall;
    property ToString: WideString read Get_ToString;
    property configurationType: ParcialConfigurationType read Get_configurationType write Set_configurationType;
    property field1: OleVariant read Get_field1 write _Set_field1;
    property field2: OleVariant read Get_field2 write _Set_field2;
  end;

// *********************************************************************//
// DispIntf:  _ParcialConfigurationDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {8C562630-EDE1-31D7-BADF-93A8D27DCE2A}
// *********************************************************************//
  _ParcialConfigurationDisp = dispinterface
    ['{8C562630-EDE1-31D7-BADF-93A8D27DCE2A}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property configurationType: ParcialConfigurationType dispid 1610743812;
    property field1: OleVariant dispid 1610743814;
    property field2: OleVariant dispid 1610743816;
    procedure ValidateFieldParameters; dispid 1610743818;
  end;

// *********************************************************************//
// Interface: _MRPRecord_SettingRealTimeClock
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {F5AA29CC-C17D-3FBC-9ACE-F1B6189F71DF}
// *********************************************************************//
  _MRPRecord_SettingRealTimeClock = interface(IDispatch)
    ['{F5AA29CC-C17D-3FBC-9ACE-F1B6189F71DF}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_nsr: WideString; safecall;
    procedure Set_nsr(const pRetVal: WideString); safecall;
    function Get_DateTimeBeforeSetting: TDateTime; safecall;
    procedure Set_DateTimeBeforeSetting(pRetVal: TDateTime); safecall;
    function Get_DateTimeSetting: TDateTime; safecall;
    procedure Set_DateTimeSetting(pRetVal: TDateTime); safecall;
    property ToString: WideString read Get_ToString;
    property nsr: WideString read Get_nsr write Set_nsr;
    property DateTimeBeforeSetting: TDateTime read Get_DateTimeBeforeSetting write Set_DateTimeBeforeSetting;
    property DateTimeSetting: TDateTime read Get_DateTimeSetting write Set_DateTimeSetting;
  end;

// *********************************************************************//
// DispIntf:  _MRPRecord_SettingRealTimeClockDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {F5AA29CC-C17D-3FBC-9ACE-F1B6189F71DF}
// *********************************************************************//
  _MRPRecord_SettingRealTimeClockDisp = dispinterface
    ['{F5AA29CC-C17D-3FBC-9ACE-F1B6189F71DF}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property nsr: WideString dispid 1610743812;
    property DateTimeBeforeSetting: TDateTime dispid 1610743814;
    property DateTimeSetting: TDateTime dispid 1610743816;
  end;

// *********************************************************************//
// Interface: _MRPRecord_RegistrationMarkingPoint
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {02878451-8BCD-3664-9BF6-DD884ECCF910}
// *********************************************************************//
  _MRPRecord_RegistrationMarkingPoint = interface(IDispatch)
    ['{02878451-8BCD-3664-9BF6-DD884ECCF910}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_nsr: WideString; safecall;
    procedure Set_nsr(const pRetVal: WideString); safecall;
    function Get_DateTimeMarkingPoint: TDateTime; safecall;
    procedure Set_DateTimeMarkingPoint(pRetVal: TDateTime); safecall;
    function Get_pis: WideString; safecall;
    procedure Set_pis(const pRetVal: WideString); safecall;
    property ToString: WideString read Get_ToString;
    property nsr: WideString read Get_nsr write Set_nsr;
    property DateTimeMarkingPoint: TDateTime read Get_DateTimeMarkingPoint write Set_DateTimeMarkingPoint;
    property pis: WideString read Get_pis write Set_pis;
  end;

// *********************************************************************//
// DispIntf:  _MRPRecord_RegistrationMarkingPointDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {02878451-8BCD-3664-9BF6-DD884ECCF910}
// *********************************************************************//
  _MRPRecord_RegistrationMarkingPointDisp = dispinterface
    ['{02878451-8BCD-3664-9BF6-DD884ECCF910}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property nsr: WideString dispid 1610743812;
    property DateTimeMarkingPoint: TDateTime dispid 1610743814;
    property pis: WideString dispid 1610743816;
  end;

// *********************************************************************//
// Interface: _CardCollection
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {0F9FDA08-1DA7-36A1-A088-FCB452386331}
// *********************************************************************//
  _CardCollection = interface(IDispatch)
    ['{0F9FDA08-1DA7-36A1-A088-FCB452386331}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Add(const Card: _Card); safecall;
    function Count: Integer; safecall;
    procedure Remove(const Card: _Card); safecall;
    procedure Clear; safecall;
    property ToString: WideString read Get_ToString;
  end;

// *********************************************************************//
// DispIntf:  _CardCollectionDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {0F9FDA08-1DA7-36A1-A088-FCB452386331}
// *********************************************************************//
  _CardCollectionDisp = dispinterface
    ['{0F9FDA08-1DA7-36A1-A088-FCB452386331}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    procedure Add(const Card: _Card); dispid 1610743812;
    function Count: Integer; dispid 1610743813;
    procedure Remove(const Card: _Card); dispid 1610743814;
    procedure Clear; dispid 1610743815;
  end;

// *********************************************************************//
// Interface: _HardwareTestCollectionResponse
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {51718424-DFDA-3613-8CFB-D31417B8D20B}
// *********************************************************************//
  _HardwareTestCollectionResponse = interface(IDispatch)
    ['{51718424-DFDA-3613-8CFB-D31417B8D20B}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_LogSequence: Smallint; safecall;
    function Get_LogsCount: Smallint; safecall;
    function Get_HardwareTestType: EPrintPointHardwareTestType; safecall;
    function Get_DisplayTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_KeyBoardTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_USB1TestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_USB2TestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_CommunicationTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_CabinetSensorTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_EnergySensorTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_LittlePaperSensorTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_NoPaperSensorTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_CardReaderTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_MagneticReaderTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_BiometricReaderTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_MRP1MemoryTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_MRP2MemoryTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_FRAMMemoryTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_Flash1MemoryTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_Flash2MemoryTestResult: EPrintPointHardwareTestResultType; safecall;
    function Get_SDCardMemoryTestResult: EPrintPointHardwareTestResultType; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property LogSequence: Smallint read Get_LogSequence;
    property LogsCount: Smallint read Get_LogsCount;
    property HardwareTestType: EPrintPointHardwareTestType read Get_HardwareTestType;
    property DisplayTestResult: EPrintPointHardwareTestResultType read Get_DisplayTestResult;
    property KeyBoardTestResult: EPrintPointHardwareTestResultType read Get_KeyBoardTestResult;
    property USB1TestResult: EPrintPointHardwareTestResultType read Get_USB1TestResult;
    property USB2TestResult: EPrintPointHardwareTestResultType read Get_USB2TestResult;
    property CommunicationTestResult: EPrintPointHardwareTestResultType read Get_CommunicationTestResult;
    property CabinetSensorTestResult: EPrintPointHardwareTestResultType read Get_CabinetSensorTestResult;
    property EnergySensorTestResult: EPrintPointHardwareTestResultType read Get_EnergySensorTestResult;
    property LittlePaperSensorTestResult: EPrintPointHardwareTestResultType read Get_LittlePaperSensorTestResult;
    property NoPaperSensorTestResult: EPrintPointHardwareTestResultType read Get_NoPaperSensorTestResult;
    property CardReaderTestResult: EPrintPointHardwareTestResultType read Get_CardReaderTestResult;
    property MagneticReaderTestResult: EPrintPointHardwareTestResultType read Get_MagneticReaderTestResult;
    property BiometricReaderTestResult: EPrintPointHardwareTestResultType read Get_BiometricReaderTestResult;
    property MRP1MemoryTestResult: EPrintPointHardwareTestResultType read Get_MRP1MemoryTestResult;
    property MRP2MemoryTestResult: EPrintPointHardwareTestResultType read Get_MRP2MemoryTestResult;
    property FRAMMemoryTestResult: EPrintPointHardwareTestResultType read Get_FRAMMemoryTestResult;
    property Flash1MemoryTestResult: EPrintPointHardwareTestResultType read Get_Flash1MemoryTestResult;
    property Flash2MemoryTestResult: EPrintPointHardwareTestResultType read Get_Flash2MemoryTestResult;
    property SDCardMemoryTestResult: EPrintPointHardwareTestResultType read Get_SDCardMemoryTestResult;
  end;

// *********************************************************************//
// DispIntf:  _HardwareTestCollectionResponseDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {51718424-DFDA-3613-8CFB-D31417B8D20B}
// *********************************************************************//
  _HardwareTestCollectionResponseDisp = dispinterface
    ['{51718424-DFDA-3613-8CFB-D31417B8D20B}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property LogSequence: Smallint readonly dispid 1610743816;
    property LogsCount: Smallint readonly dispid 1610743817;
    property HardwareTestType: EPrintPointHardwareTestType readonly dispid 1610743818;
    property DisplayTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743819;
    property KeyBoardTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743820;
    property USB1TestResult: EPrintPointHardwareTestResultType readonly dispid 1610743821;
    property USB2TestResult: EPrintPointHardwareTestResultType readonly dispid 1610743822;
    property CommunicationTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743823;
    property CabinetSensorTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743824;
    property EnergySensorTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743825;
    property LittlePaperSensorTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743826;
    property NoPaperSensorTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743827;
    property CardReaderTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743828;
    property MagneticReaderTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743829;
    property BiometricReaderTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743830;
    property MRP1MemoryTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743831;
    property MRP2MemoryTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743832;
    property FRAMMemoryTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743833;
    property Flash1MemoryTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743834;
    property Flash2MemoryTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743835;
    property SDCardMemoryTestResult: EPrintPointHardwareTestResultType readonly dispid 1610743836;
  end;

// *********************************************************************//
// Interface: _GetMACResponse
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {876887A8-15B6-33D9-853C-41BA220DD16B}
// *********************************************************************//
  _GetMACResponse = interface(IDispatch)
    ['{876887A8-15B6-33D9-853C-41BA220DD16B}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_MACAddress: WideString; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property MACAddress: WideString read Get_MACAddress;
  end;

// *********************************************************************//
// DispIntf:  _GetMACResponseDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {876887A8-15B6-33D9-853C-41BA220DD16B}
// *********************************************************************//
  _GetMACResponseDisp = dispinterface
    ['{876887A8-15B6-33D9-853C-41BA220DD16B}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property MACAddress: WideString readonly dispid 1610743816;
  end;

// *********************************************************************//
// Interface: _ConcretePunchMessage
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {8A13657B-F830-356D-A578-56EA2BEDF1B7}
// *********************************************************************//
  _ConcretePunchMessage = interface(IDispatch)
    ['{8A13657B-F830-356D-A578-56EA2BEDF1B7}']
  end;

// *********************************************************************//
// DispIntf:  _ConcretePunchMessageDisp
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {8A13657B-F830-356D-A578-56EA2BEDF1B7}
// *********************************************************************//
  _ConcretePunchMessageDisp = dispinterface
    ['{8A13657B-F830-356D-A578-56EA2BEDF1B7}']
  end;

// *********************************************************************//
// Interface: _AbstractMemoryFormat
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {78B677F5-EEA9-3462-A6A5-1649A1B3B738}
// *********************************************************************//
  _AbstractMemoryFormat = interface(IDispatch)
    ['{78B677F5-EEA9-3462-A6A5-1649A1B3B738}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    procedure _Set_MemoryFormat(const pRetVal: _MemoryFormat); safecall;
    function Get_MemoryFormat: _MemoryFormat; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property MemoryFormat: _MemoryFormat read Get_MemoryFormat write _Set_MemoryFormat;
  end;

// *********************************************************************//
// DispIntf:  _AbstractMemoryFormatDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {78B677F5-EEA9-3462-A6A5-1649A1B3B738}
// *********************************************************************//
  _AbstractMemoryFormatDisp = dispinterface
    ['{78B677F5-EEA9-3462-A6A5-1649A1B3B738}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property MemoryFormat: _MemoryFormat dispid 1610743816;
  end;

// *********************************************************************//
// Interface: _BioPointMemoryFormat
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {42019683-A593-37DA-B9A6-7E8A30418B63}
// *********************************************************************//
  _BioPointMemoryFormat = interface(IDispatch)
    ['{42019683-A593-37DA-B9A6-7E8A30418B63}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    procedure _Set_MemoryFormat(const pRetVal: _MemoryFormat); safecall;
    function Get_MemoryFormat: _MemoryFormat; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property MemoryFormat: _MemoryFormat read Get_MemoryFormat write _Set_MemoryFormat;
  end;

// *********************************************************************//
// DispIntf:  _BioPointMemoryFormatDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {42019683-A593-37DA-B9A6-7E8A30418B63}
// *********************************************************************//
  _BioPointMemoryFormatDisp = dispinterface
    ['{42019683-A593-37DA-B9A6-7E8A30418B63}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property MemoryFormat: _MemoryFormat dispid 1610743816;
  end;

// *********************************************************************//
// Interface: _TemplateCollection
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {6ED2CC35-76F4-3EEA-B441-E2229951E9D3}
// *********************************************************************//
  _TemplateCollection = interface(IDispatch)
    ['{6ED2CC35-76F4-3EEA-B441-E2229951E9D3}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Add(const myTemplate: _Template); safecall;
    function Count: Integer; safecall;
    procedure Remove(const myTemplate: _Template); safecall;
    procedure Clear; safecall;
    property ToString: WideString read Get_ToString;
  end;

// *********************************************************************//
// DispIntf:  _TemplateCollectionDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {6ED2CC35-76F4-3EEA-B441-E2229951E9D3}
// *********************************************************************//
  _TemplateCollectionDisp = dispinterface
    ['{6ED2CC35-76F4-3EEA-B441-E2229951E9D3}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    procedure Add(const myTemplate: _Template); dispid 1610743812;
    function Count: Integer; dispid 1610743813;
    procedure Remove(const myTemplate: _Template); dispid 1610743814;
    procedure Clear; dispid 1610743815;
  end;

// *********************************************************************//
// Interface: _AlarmRings
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {5B8EF58D-140B-3E95-8994-8471170E7FBD}
// *********************************************************************//
  _AlarmRings = interface(IDispatch)
    ['{5B8EF58D-140B-3E95-8994-8471170E7FBD}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Set_TypeRing(pRetVal: TypeAlarm); safecall;
    function Get_TypeRing: TypeAlarm; safecall;
    procedure Set_Id(pRetVal: Byte); safecall;
    function Get_Id: Byte; safecall;
    procedure Set_TimeAlarm(const pRetVal: WideString); safecall;
    function Get_TimeAlarm: WideString; safecall;
    procedure Set_Duration(pRetVal: Byte); safecall;
    function Get_Duration: Byte; safecall;
    procedure Set_RingSunday(pRetVal: TypeRinging); safecall;
    function Get_RingSunday: TypeRinging; safecall;
    procedure Set_RingMonday(pRetVal: TypeRinging); safecall;
    function Get_RingMonday: TypeRinging; safecall;
    procedure Set_RingTuesday(pRetVal: TypeRinging); safecall;
    function Get_RingTuesday: TypeRinging; safecall;
    procedure Set_RingWednesday(pRetVal: TypeRinging); safecall;
    function Get_RingWednesday: TypeRinging; safecall;
    procedure Set_RingThursday(pRetVal: TypeRinging); safecall;
    function Get_RingThursday: TypeRinging; safecall;
    procedure Set_RingFriday(pRetVal: TypeRinging); safecall;
    function Get_RingFriday: TypeRinging; safecall;
    procedure Set_RingSaturday(pRetVal: TypeRinging); safecall;
    function Get_RingSaturday: TypeRinging; safecall;
    procedure Set_RingHoliday(pRetVal: TypeRinging); safecall;
    function Get_RingHoliday: TypeRinging; safecall;
    property ToString: WideString read Get_ToString;
    property TypeRing: TypeAlarm read Get_TypeRing write Set_TypeRing;
    property Id: Byte read Get_Id write Set_Id;
    property TimeAlarm: WideString read Get_TimeAlarm write Set_TimeAlarm;
    property Duration: Byte read Get_Duration write Set_Duration;
    property RingSunday: TypeRinging read Get_RingSunday write Set_RingSunday;
    property RingMonday: TypeRinging read Get_RingMonday write Set_RingMonday;
    property RingTuesday: TypeRinging read Get_RingTuesday write Set_RingTuesday;
    property RingWednesday: TypeRinging read Get_RingWednesday write Set_RingWednesday;
    property RingThursday: TypeRinging read Get_RingThursday write Set_RingThursday;
    property RingFriday: TypeRinging read Get_RingFriday write Set_RingFriday;
    property RingSaturday: TypeRinging read Get_RingSaturday write Set_RingSaturday;
    property RingHoliday: TypeRinging read Get_RingHoliday write Set_RingHoliday;
  end;

// *********************************************************************//
// DispIntf:  _AlarmRingsDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {5B8EF58D-140B-3E95-8994-8471170E7FBD}
// *********************************************************************//
  _AlarmRingsDisp = dispinterface
    ['{5B8EF58D-140B-3E95-8994-8471170E7FBD}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property TypeRing: TypeAlarm dispid 1610743812;
    property Id: Byte dispid 1610743814;
    property TimeAlarm: WideString dispid 1610743816;
    property Duration: Byte dispid 1610743818;
    property RingSunday: TypeRinging dispid 1610743820;
    property RingMonday: TypeRinging dispid 1610743822;
    property RingTuesday: TypeRinging dispid 1610743824;
    property RingWednesday: TypeRinging dispid 1610743826;
    property RingThursday: TypeRinging dispid 1610743828;
    property RingFriday: TypeRinging dispid 1610743830;
    property RingSaturday: TypeRinging dispid 1610743832;
    property RingHoliday: TypeRinging dispid 1610743834;
  end;

// *********************************************************************//
// Interface: _Credential
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {F100D95F-1C0D-3AA3-B69D-6FA223062BDF}
// *********************************************************************//
  _Credential = interface(IDispatch)
    ['{F100D95F-1C0D-3AA3-B69D-6FA223062BDF}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function CompareTo(obj: OleVariant): Integer; safecall;
    function Get_cardCode: WideString; safecall;
    procedure Set_cardCode(const pRetVal: WideString); safecall;
    function Get_pis: WideString; safecall;
    procedure Set_pis(const pRetVal: WideString); safecall;
    function Get_version: Byte; safecall;
    procedure Set_version(pRetVal: Byte); safecall;
    property ToString: WideString read Get_ToString;
    property cardCode: WideString read Get_cardCode write Set_cardCode;
    property pis: WideString read Get_pis write Set_pis;
    property version: Byte read Get_version write Set_version;
  end;

// *********************************************************************//
// DispIntf:  _CredentialDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {F100D95F-1C0D-3AA3-B69D-6FA223062BDF}
// *********************************************************************//
  _CredentialDisp = dispinterface
    ['{F100D95F-1C0D-3AA3-B69D-6FA223062BDF}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function CompareTo(obj: OleVariant): Integer; dispid 1610743812;
    property cardCode: WideString dispid 1610743813;
    property pis: WideString dispid 1610743815;
    property version: Byte dispid 1610743817;
  end;

// *********************************************************************//
// Interface: _Configuration
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {87EB9D12-8EF8-30DE-8CB7-BE72F7E6E7D4}
// *********************************************************************//
  _Configuration = interface(IDispatch)
    ['{87EB9D12-8EF8-30DE-8CB7-BE72F7E6E7D4}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_configurationType: EConfigurationType; safecall;
    procedure Set_configurationType(pRetVal: EConfigurationType); safecall;
    function Get_field1: OleVariant; safecall;
    procedure _Set_field1(pRetVal: OleVariant); safecall;
    function Get_field2: OleVariant; safecall;
    procedure _Set_field2(pRetVal: OleVariant); safecall;
    function Get_field3: OleVariant; safecall;
    procedure _Set_field3(pRetVal: OleVariant); safecall;
    function Get_field4: OleVariant; safecall;
    procedure _Set_field4(pRetVal: OleVariant); safecall;
    function Get_field5: OleVariant; safecall;
    procedure _Set_field5(pRetVal: OleVariant); safecall;
    procedure ValidateFieldParameters; safecall;
    property ToString: WideString read Get_ToString;
    property configurationType: EConfigurationType read Get_configurationType write Set_configurationType;
    property field1: OleVariant read Get_field1 write _Set_field1;
    property field2: OleVariant read Get_field2 write _Set_field2;
    property field3: OleVariant read Get_field3 write _Set_field3;
    property field4: OleVariant read Get_field4 write _Set_field4;
    property field5: OleVariant read Get_field5 write _Set_field5;
  end;

// *********************************************************************//
// DispIntf:  _ConfigurationDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {87EB9D12-8EF8-30DE-8CB7-BE72F7E6E7D4}
// *********************************************************************//
  _ConfigurationDisp = dispinterface
    ['{87EB9D12-8EF8-30DE-8CB7-BE72F7E6E7D4}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property configurationType: EConfigurationType dispid 1610743812;
    property field1: OleVariant dispid 1610743814;
    property field2: OleVariant dispid 1610743816;
    property field3: OleVariant dispid 1610743818;
    property field4: OleVariant dispid 1610743820;
    property field5: OleVariant dispid 1610743822;
    procedure ValidateFieldParameters; dispid 1610743824;
  end;

// *********************************************************************//
// Interface: _InvalidMessageException
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {43CC521F-6982-359F-B1D4-5AC39F4DBFD7}
// *********************************************************************//
  _InvalidMessageException = interface(IDispatch)
    ['{43CC521F-6982-359F-B1D4-5AC39F4DBFD7}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_data: IDictionary; safecall;
    procedure GetObjectData(const info: _SerializationInfo; context: StreamingContext); safecall;
    function GetType_2: _Type; safecall;
    function Get_message: WideString; safecall;
    function GetBaseException: _Exception; safecall;
    function Get_StackTrace: WideString; safecall;
    function Get_HelpLink: WideString; safecall;
    procedure Set_HelpLink(const pRetVal: WideString); safecall;
    function Get_Source: WideString; safecall;
    procedure Set_Source(const pRetVal: WideString); safecall;
    function Get_InnerException: _Exception; safecall;
    function Get_TargetSite: _MethodBase; safecall;
    function Get_InvalidMessageType: InvalidMessageType; safecall;
    property ToString: WideString read Get_ToString;
    property data: IDictionary read Get_data;
    property message: WideString read Get_message;
    property StackTrace: WideString read Get_StackTrace;
    property HelpLink: WideString read Get_HelpLink write Set_HelpLink;
    property Source: WideString read Get_Source write Set_Source;
    property InnerException: _Exception read Get_InnerException;
    property TargetSite: _MethodBase read Get_TargetSite;
    property InvalidMessageType: InvalidMessageType read Get_InvalidMessageType;
  end;

// *********************************************************************//
// DispIntf:  _InvalidMessageExceptionDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {43CC521F-6982-359F-B1D4-5AC39F4DBFD7}
// *********************************************************************//
  _InvalidMessageExceptionDisp = dispinterface
    ['{43CC521F-6982-359F-B1D4-5AC39F4DBFD7}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property data: IDictionary readonly dispid 1610743812;
    procedure GetObjectData(const info: _SerializationInfo; context: {??StreamingContext}OleVariant); dispid 1610743813;
    function GetType_2: _Type; dispid 1610743814;
    property message: WideString readonly dispid 1610743815;
    function GetBaseException: _Exception; dispid 1610743816;
    property StackTrace: WideString readonly dispid 1610743817;
    property HelpLink: WideString dispid 1610743818;
    property Source: WideString dispid 1610743820;
    property InnerException: _Exception readonly dispid 1610743822;
    property TargetSite: _MethodBase readonly dispid 1610743823;
    property InvalidMessageType: InvalidMessageType readonly dispid 1610743824;
  end;

// *********************************************************************//
// Interface: _FaceStatus
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {01D965E2-CC3E-3F0E-B9D1-4DD9D8699376}
// *********************************************************************//
  _FaceStatus = interface(IDispatch)
    ['{01D965E2-CC3E-3F0E-B9D1-4DD9D8699376}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_RecordsCapacity: Integer; safecall;
    function Get_RecordsOccupation: Integer; safecall;
    function Get_FingerprintCapacity: Integer; safecall;
    function Get_UsersCapacity: Integer; safecall;
    function Get_UsersOccupation: Integer; safecall;
    function Get_MasterOccupation: Integer; safecall;
    function Get_PasswordOccupation: Integer; safecall;
    function Get_FingerprintOccupation: Integer; safecall;
    function Get_firmwareVersion: WideString; safecall;
    function Get_DeviceDateTime: TDateTime; safecall;
    property ToString: WideString read Get_ToString;
    property RecordsCapacity: Integer read Get_RecordsCapacity;
    property RecordsOccupation: Integer read Get_RecordsOccupation;
    property FingerprintCapacity: Integer read Get_FingerprintCapacity;
    property UsersCapacity: Integer read Get_UsersCapacity;
    property UsersOccupation: Integer read Get_UsersOccupation;
    property MasterOccupation: Integer read Get_MasterOccupation;
    property PasswordOccupation: Integer read Get_PasswordOccupation;
    property FingerprintOccupation: Integer read Get_FingerprintOccupation;
    property firmwareVersion: WideString read Get_firmwareVersion;
    property DeviceDateTime: TDateTime read Get_DeviceDateTime;
  end;

// *********************************************************************//
// DispIntf:  _FaceStatusDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {01D965E2-CC3E-3F0E-B9D1-4DD9D8699376}
// *********************************************************************//
  _FaceStatusDisp = dispinterface
    ['{01D965E2-CC3E-3F0E-B9D1-4DD9D8699376}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property RecordsCapacity: Integer readonly dispid 1610743812;
    property RecordsOccupation: Integer readonly dispid 1610743813;
    property FingerprintCapacity: Integer readonly dispid 1610743814;
    property UsersCapacity: Integer readonly dispid 1610743815;
    property UsersOccupation: Integer readonly dispid 1610743816;
    property MasterOccupation: Integer readonly dispid 1610743817;
    property PasswordOccupation: Integer readonly dispid 1610743818;
    property FingerprintOccupation: Integer readonly dispid 1610743819;
    property firmwareVersion: WideString readonly dispid 1610743820;
    property DeviceDateTime: TDateTime readonly dispid 1610743821;
  end;

// *********************************************************************//
// Interface: _AbstractFactory
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {0EB89E59-CBBC-3DF5-AD86-2DC1CEF5AB01}
// *********************************************************************//
  _AbstractFactory = interface(IDispatch)
    ['{0EB89E59-CBBC-3DF5-AD86-2DC1CEF5AB01}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function CreateProtocol(type_: WatchProtocolType; const Comm: IComm; WatchAddress: Integer): _AbstractProtocol; safecall;
    function CreatePacket(protocolType: WatchProtocolType; MessageType: MessageType; 
                          WatchAddress: Integer): _AbstractPacket; safecall;
    function CreatePacket_2(protocolType: WatchProtocolType; data: PSafeArray): _AbstractPacket; safecall;
    property ToString: WideString read Get_ToString;
  end;

// *********************************************************************//
// DispIntf:  _AbstractFactoryDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {0EB89E59-CBBC-3DF5-AD86-2DC1CEF5AB01}
// *********************************************************************//
  _AbstractFactoryDisp = dispinterface
    ['{0EB89E59-CBBC-3DF5-AD86-2DC1CEF5AB01}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function CreateProtocol(type_: WatchProtocolType; const Comm: IComm; WatchAddress: Integer): _AbstractProtocol; dispid 1610743812;
    function CreatePacket(protocolType: WatchProtocolType; MessageType: MessageType; 
                          WatchAddress: Integer): _AbstractPacket; dispid 1610743813;
    function CreatePacket_2(protocolType: WatchProtocolType; data: {??PSafeArray}OleVariant): _AbstractPacket; dispid 1610743814;
  end;

// *********************************************************************//
// Interface: _HolidayCollection
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {0800B5DB-DA43-39A1-855C-B72D5620FFB2}
// *********************************************************************//
  _HolidayCollection = interface(IDispatch)
    ['{0800B5DB-DA43-39A1-855C-B72D5620FFB2}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Add(const Holiday: _Holiday); safecall;
    function Count: Integer; safecall;
    procedure Remove(const Holiday: _Holiday); safecall;
    procedure Clear; safecall;
    property ToString: WideString read Get_ToString;
  end;

// *********************************************************************//
// DispIntf:  _HolidayCollectionDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {0800B5DB-DA43-39A1-855C-B72D5620FFB2}
// *********************************************************************//
  _HolidayCollectionDisp = dispinterface
    ['{0800B5DB-DA43-39A1-855C-B72D5620FFB2}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    procedure Add(const Holiday: _Holiday); dispid 1610743812;
    function Count: Integer; dispid 1610743813;
    procedure Remove(const Holiday: _Holiday); dispid 1610743814;
    procedure Clear; dispid 1610743815;
  end;

// *********************************************************************//
// Interface: _WeeklyJourneyWorking
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {59D43438-1A3B-3374-9E63-6C09423865B1}
// *********************************************************************//
  _WeeklyJourneyWorking = interface(IDispatch)
    ['{59D43438-1A3B-3374-9E63-6C09423865B1}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Set_Id(pRetVal: Smallint); safecall;
    function Get_Id: Smallint; safecall;
    procedure Set_TypeWorking(pRetVal: TypeWorking); safecall;
    function Get_TypeWorking: TypeWorking; safecall;
    procedure Set_Sunday(pRetVal: Smallint); safecall;
    function Get_Sunday: Smallint; safecall;
    procedure Set_Monday(pRetVal: Smallint); safecall;
    function Get_Monday: Smallint; safecall;
    procedure Set_Tuesday(pRetVal: Smallint); safecall;
    function Get_Tuesday: Smallint; safecall;
    procedure Set_Wednesday(pRetVal: Smallint); safecall;
    function Get_Wednesday: Smallint; safecall;
    procedure Set_Thursday(pRetVal: Smallint); safecall;
    function Get_Thursday: Smallint; safecall;
    procedure Set_Friday(pRetVal: Smallint); safecall;
    function Get_Friday: Smallint; safecall;
    procedure Set_Saturday(pRetVal: Smallint); safecall;
    function Get_Saturday: Smallint; safecall;
    function Get_Holiday: Smallint; safecall;
    procedure Set_Holiday(pRetVal: Smallint); safecall;
    property ToString: WideString read Get_ToString;
    property Id: Smallint read Get_Id write Set_Id;
    property TypeWorking: TypeWorking read Get_TypeWorking write Set_TypeWorking;
    property Sunday: Smallint read Get_Sunday write Set_Sunday;
    property Monday: Smallint read Get_Monday write Set_Monday;
    property Tuesday: Smallint read Get_Tuesday write Set_Tuesday;
    property Wednesday: Smallint read Get_Wednesday write Set_Wednesday;
    property Thursday: Smallint read Get_Thursday write Set_Thursday;
    property Friday: Smallint read Get_Friday write Set_Friday;
    property Saturday: Smallint read Get_Saturday write Set_Saturday;
    property Holiday: Smallint read Get_Holiday write Set_Holiday;
  end;

// *********************************************************************//
// DispIntf:  _WeeklyJourneyWorkingDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {59D43438-1A3B-3374-9E63-6C09423865B1}
// *********************************************************************//
  _WeeklyJourneyWorkingDisp = dispinterface
    ['{59D43438-1A3B-3374-9E63-6C09423865B1}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property Id: Smallint dispid 1610743812;
    property TypeWorking: TypeWorking dispid 1610743814;
    property Sunday: Smallint dispid 1610743816;
    property Monday: Smallint dispid 1610743818;
    property Tuesday: Smallint dispid 1610743820;
    property Wednesday: Smallint dispid 1610743822;
    property Thursday: Smallint dispid 1610743824;
    property Friday: Smallint dispid 1610743826;
    property Saturday: Smallint dispid 1610743828;
    property Holiday: Smallint dispid 1610743830;
  end;

// *********************************************************************//
// Interface: _ImmediateStatusResponse
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {80F74BCB-E031-3AD2-B326-C807335B4FBC}
// *********************************************************************//
  _ImmediateStatusResponse = interface(IDispatch)
    ['{80F74BCB-E031-3AD2-B326-C807335B4FBC}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_BobbinState: BobbinStateType; safecall;
    function Get_HasNewFingerprints: WordBool; safecall;
    function Get_HasMarkingPoints: WordBool; safecall;
    function Get_REPState: REPStateType; safecall;
    function Get_MRPState: MRPStateType; safecall;
    function Get_AFDGeneration: AFDGenerationType; safecall;
    procedure Set_AFDGeneration(pRetVal: AFDGenerationType); safecall;
    function Get_RIMGeneration: RIMGenerationType; safecall;
    procedure Set_RIMGeneration(pRetVal: RIMGenerationType); safecall;
    function Get_AlimentationType: AlimentationType; safecall;
    procedure Set_AlimentationType(pRetVal: AlimentationType); safecall;
    function Get_BatteryLevel: BatteryLevel; safecall;
    procedure Set_BatteryLevel(pRetVal: BatteryLevel); safecall;
    function Get_TimeUtilyBattery: Integer; safecall;
    procedure Set_TimeUtilyBattery(pRetVal: Integer); safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property BobbinState: BobbinStateType read Get_BobbinState;
    property HasNewFingerprints: WordBool read Get_HasNewFingerprints;
    property HasMarkingPoints: WordBool read Get_HasMarkingPoints;
    property REPState: REPStateType read Get_REPState;
    property MRPState: MRPStateType read Get_MRPState;
    property AFDGeneration: AFDGenerationType read Get_AFDGeneration write Set_AFDGeneration;
    property RIMGeneration: RIMGenerationType read Get_RIMGeneration write Set_RIMGeneration;
    property AlimentationType: AlimentationType read Get_AlimentationType write Set_AlimentationType;
    property BatteryLevel: BatteryLevel read Get_BatteryLevel write Set_BatteryLevel;
    property TimeUtilyBattery: Integer read Get_TimeUtilyBattery write Set_TimeUtilyBattery;
  end;

// *********************************************************************//
// DispIntf:  _ImmediateStatusResponseDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {80F74BCB-E031-3AD2-B326-C807335B4FBC}
// *********************************************************************//
  _ImmediateStatusResponseDisp = dispinterface
    ['{80F74BCB-E031-3AD2-B326-C807335B4FBC}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property BobbinState: BobbinStateType readonly dispid 1610743816;
    property HasNewFingerprints: WordBool readonly dispid 1610743817;
    property HasMarkingPoints: WordBool readonly dispid 1610743818;
    property REPState: REPStateType readonly dispid 1610743819;
    property MRPState: MRPStateType readonly dispid 1610743820;
    property AFDGeneration: AFDGenerationType dispid 1610743821;
    property RIMGeneration: RIMGenerationType dispid 1610743823;
    property AlimentationType: AlimentationType dispid 1610743825;
    property BatteryLevel: BatteryLevel dispid 1610743827;
    property TimeUtilyBattery: Integer dispid 1610743829;
  end;

// *********************************************************************//
// Interface: _PrintPointMRPEventLog
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {CBE23118-2F4F-3F6E-B5F2-D8A5AC1CED64}
// *********************************************************************//
  _PrintPointMRPEventLog = interface(IDispatch)
    ['{CBE23118-2F4F-3F6E-B5F2-D8A5AC1CED64}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_Return: PSafeArray; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property Return: PSafeArray read Get_Return;
  end;

// *********************************************************************//
// DispIntf:  _PrintPointMRPEventLogDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {CBE23118-2F4F-3F6E-B5F2-D8A5AC1CED64}
// *********************************************************************//
  _PrintPointMRPEventLogDisp = dispinterface
    ['{CBE23118-2F4F-3F6E-B5F2-D8A5AC1CED64}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property Return: {??PSafeArray}OleVariant readonly dispid 1610743816;
  end;

// *********************************************************************//
// Interface: _Master
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {5F1A4777-C778-330C-AE36-477874E8E1D4}
// *********************************************************************//
  _Master = interface(IDispatch)
    ['{5F1A4777-C778-330C-AE36-477874E8E1D4}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_pis: WideString; safecall;
    procedure Set_pis(const pRetVal: WideString); safecall;
    function Get_Card: WideString; safecall;
    procedure Set_Card(const pRetVal: WideString); safecall;
    function Get_password: WideString; safecall;
    procedure Set_password(const pRetVal: WideString); safecall;
    function Get_hasTechniquesProgrammingPermission: WordBool; safecall;
    procedure Set_hasTechniquesProgrammingPermission(pRetVal: WordBool); safecall;
    function Get_hasDataAndTimePermission: WordBool; safecall;
    procedure Set_hasDataAndTimePermission(pRetVal: WordBool); safecall;
    function Get_HasPenDriveProgrammingPermission: WordBool; safecall;
    procedure Set_HasPenDriveProgrammingPermission(pRetVal: WordBool); safecall;
    function Get_HasBobbinChangePermission: WordBool; safecall;
    procedure Set_HasBobbinChangePermission(pRetVal: WordBool); safecall;
    property ToString: WideString read Get_ToString;
    property pis: WideString read Get_pis write Set_pis;
    property Card: WideString read Get_Card write Set_Card;
    property password: WideString read Get_password write Set_password;
    property hasTechniquesProgrammingPermission: WordBool read Get_hasTechniquesProgrammingPermission write Set_hasTechniquesProgrammingPermission;
    property hasDataAndTimePermission: WordBool read Get_hasDataAndTimePermission write Set_hasDataAndTimePermission;
    property HasPenDriveProgrammingPermission: WordBool read Get_HasPenDriveProgrammingPermission write Set_HasPenDriveProgrammingPermission;
    property HasBobbinChangePermission: WordBool read Get_HasBobbinChangePermission write Set_HasBobbinChangePermission;
  end;

// *********************************************************************//
// DispIntf:  _MasterDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {5F1A4777-C778-330C-AE36-477874E8E1D4}
// *********************************************************************//
  _MasterDisp = dispinterface
    ['{5F1A4777-C778-330C-AE36-477874E8E1D4}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property pis: WideString dispid 1610743812;
    property Card: WideString dispid 1610743814;
    property password: WideString dispid 1610743816;
    property hasTechniquesProgrammingPermission: WordBool dispid 1610743818;
    property hasDataAndTimePermission: WordBool dispid 1610743820;
    property HasPenDriveProgrammingPermission: WordBool dispid 1610743822;
    property HasBobbinChangePermission: WordBool dispid 1610743824;
  end;

// *********************************************************************//
// DispIntf:  IRelogio
// Flags:     (4096) Dispatchable
// GUID:      {7BD20046-DF8C-44A6-8F6B-687FAA26FA71}
// *********************************************************************//
  IRelogio = dispinterface
    ['{7BD20046-DF8C-44A6-8F6B-687FAA26FA71}']
    function setListaBrancaPorIP(const path: WideString; const name: WideString; 
                                 const ip: WideString; const Id: WideString): WordBool; dispid 1;
    function setListaBrancaPorDriver(const pathIn: WideString; const nameIn: WideString; 
                                     const pathOut: WideString; const nameOut: WideString): WordBool; dispid 2;
    function setListaNegraPorIP(const path: WideString; const name: WideString; 
                                const ip: WideString; const Id: WideString): WordBool; dispid 3;
    function setListaNegraPorDriver(const pathIn: WideString; const nameIn: WideString; 
                                    const pathOut: WideString; const nameOut: WideString): WordBool; dispid 4;
    function setDataHora(const ip: WideString; const datahora: WideString; const Id: WideString): WordBool; dispid 5;
    function coletaPorIP(const ip: WideString; const path: WideString; const name: WideString; 
                         const Id: WideString): WordBool; dispid 6;
    function coletaPorDriver(const pathIn: WideString; const nameIn: WideString; 
                             const pathOut: WideString; const nameOut: WideString): WordBool; dispid 7;
  end;

// *********************************************************************//
// Interface: _PrintPointEmployerMessage
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {BA9CC9F9-0BE1-3BB5-A34F-C43AE506B0E1}
// *********************************************************************//
  _PrintPointEmployerMessage = interface(IDispatch)
    ['{BA9CC9F9-0BE1-3BB5-A34F-C43AE506B0E1}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_employerType: EmployeerType; safecall;
    procedure Set_employerType(pRetVal: EmployeerType); safecall;
    function Get_cpf_cnpj: WideString; safecall;
    procedure Set_cpf_cnpj(const pRetVal: WideString); safecall;
    function Get_cei: WideString; safecall;
    procedure Set_cei(const pRetVal: WideString); safecall;
    function Get_name: WideString; safecall;
    procedure Set_name(const pRetVal: WideString); safecall;
    function Get_address: WideString; safecall;
    procedure Set_address(const pRetVal: WideString); safecall;
    function MakeEmployeerFile(const repSerialNumber: WideString; const windowTitle: WideString; 
                               const path: WideString): WordBool; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property employerType: EmployeerType read Get_employerType write Set_employerType;
    property cpf_cnpj: WideString read Get_cpf_cnpj write Set_cpf_cnpj;
    property cei: WideString read Get_cei write Set_cei;
    property name: WideString read Get_name write Set_name;
    property address: WideString read Get_address write Set_address;
  end;

// *********************************************************************//
// DispIntf:  _PrintPointEmployerMessageDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {BA9CC9F9-0BE1-3BB5-A34F-C43AE506B0E1}
// *********************************************************************//
  _PrintPointEmployerMessageDisp = dispinterface
    ['{BA9CC9F9-0BE1-3BB5-A34F-C43AE506B0E1}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property employerType: EmployeerType dispid 1610743816;
    property cpf_cnpj: WideString dispid 1610743818;
    property cei: WideString dispid 1610743820;
    property name: WideString dispid 1610743822;
    property address: WideString dispid 1610743824;
    function MakeEmployeerFile(const repSerialNumber: WideString; const windowTitle: WideString; 
                               const path: WideString): WordBool; dispid 1610743826;
  end;

// *********************************************************************//
// Interface: _MicropointCardList
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {D694DE74-E8C8-3F70-927C-F9F8913ABA7B}
// *********************************************************************//
  _MicropointCardList = interface(IDispatch)
    ['{D694DE74-E8C8-3F70-927C-F9F8913ABA7B}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    procedure _Set_Card(const pRetVal: _Card); safecall;
    function Get_Card: _Card; safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property Card: _Card read Get_Card write _Set_Card;
  end;

// *********************************************************************//
// DispIntf:  _MicropointCardListDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {D694DE74-E8C8-3F70-927C-F9F8913ABA7B}
// *********************************************************************//
  _MicropointCardListDisp = dispinterface
    ['{D694DE74-E8C8-3F70-927C-F9F8913ABA7B}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property Card: _Card dispid 1610743816;
  end;

// *********************************************************************//
// Interface: _ShiftTableCollection
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {6D1ABFB6-695D-33E5-A803-0039BEF6AE5D}
// *********************************************************************//
  _ShiftTableCollection = interface(IDispatch)
    ['{6D1ABFB6-695D-33E5-A803-0039BEF6AE5D}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_Id: Smallint; safecall;
    procedure Set_Id(pRetVal: Smallint); safecall;
    procedure Add(const faixa: _ShiftTable); safecall;
    function Count: Integer; safecall;
    procedure Remove(const faixa: _ShiftTable); safecall;
    procedure Clear; safecall;
    property ToString: WideString read Get_ToString;
    property Id: Smallint read Get_Id write Set_Id;
  end;

// *********************************************************************//
// DispIntf:  _ShiftTableCollectionDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {6D1ABFB6-695D-33E5-A803-0039BEF6AE5D}
// *********************************************************************//
  _ShiftTableCollectionDisp = dispinterface
    ['{6D1ABFB6-695D-33E5-A803-0039BEF6AE5D}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property Id: Smallint dispid 1610743812;
    procedure Add(const faixa: _ShiftTable); dispid 1610743814;
    function Count: Integer; dispid 1610743815;
    procedure Remove(const faixa: _ShiftTable); dispid 1610743816;
    procedure Clear; dispid 1610743817;
  end;

// *********************************************************************//
// Interface: _MonthlyJourneyWorking
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {CDBBE17C-1B38-3483-9A45-3DA2FB57EA5E}
// *********************************************************************//
  _MonthlyJourneyWorking = interface(IDispatch)
    ['{CDBBE17C-1B38-3483-9A45-3DA2FB57EA5E}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Set_Id(pRetVal: Smallint); safecall;
    function Get_Id: Smallint; safecall;
    procedure Set_TypeWorking(pRetVal: TypeWorking); safecall;
    function Get_TypeWorking: TypeWorking; safecall;
    function Get_Dia01: Smallint; safecall;
    procedure Set_Dia01(pRetVal: Smallint); safecall;
    function Get_Dia02: Smallint; safecall;
    procedure Set_Dia02(pRetVal: Smallint); safecall;
    function Get_Dia03: Smallint; safecall;
    procedure Set_Dia03(pRetVal: Smallint); safecall;
    function Get_Dia04: Smallint; safecall;
    procedure Set_Dia04(pRetVal: Smallint); safecall;
    function Get_Dia05: Smallint; safecall;
    procedure Set_Dia05(pRetVal: Smallint); safecall;
    function Get_Dia06: Smallint; safecall;
    procedure Set_Dia06(pRetVal: Smallint); safecall;
    function Get_Dia07: Smallint; safecall;
    procedure Set_Dia07(pRetVal: Smallint); safecall;
    function Get_Dia08: Smallint; safecall;
    procedure Set_Dia08(pRetVal: Smallint); safecall;
    function Get_Dia09: Smallint; safecall;
    procedure Set_Dia09(pRetVal: Smallint); safecall;
    function Get_Dia10: Smallint; safecall;
    procedure Set_Dia10(pRetVal: Smallint); safecall;
    function Get_Dia11: Smallint; safecall;
    procedure Set_Dia11(pRetVal: Smallint); safecall;
    function Get_Dia12: Smallint; safecall;
    procedure Set_Dia12(pRetVal: Smallint); safecall;
    function Get_Dia13: Smallint; safecall;
    procedure Set_Dia13(pRetVal: Smallint); safecall;
    function Get_Dia14: Smallint; safecall;
    procedure Set_Dia14(pRetVal: Smallint); safecall;
    function Get_Dia15: Smallint; safecall;
    procedure Set_Dia15(pRetVal: Smallint); safecall;
    function Get_Dia16: Smallint; safecall;
    procedure Set_Dia16(pRetVal: Smallint); safecall;
    function Get_Dia17: Smallint; safecall;
    procedure Set_Dia17(pRetVal: Smallint); safecall;
    function Get_Dia18: Smallint; safecall;
    procedure Set_Dia18(pRetVal: Smallint); safecall;
    function Get_Dia19: Smallint; safecall;
    procedure Set_Dia19(pRetVal: Smallint); safecall;
    function Get_Dia20: Smallint; safecall;
    procedure Set_Dia20(pRetVal: Smallint); safecall;
    function Get_Dia21: Smallint; safecall;
    procedure Set_Dia21(pRetVal: Smallint); safecall;
    function Get_Dia22: Smallint; safecall;
    procedure Set_Dia22(pRetVal: Smallint); safecall;
    function Get_Dia23: Smallint; safecall;
    procedure Set_Dia23(pRetVal: Smallint); safecall;
    function Get_Dia24: Smallint; safecall;
    procedure Set_Dia24(pRetVal: Smallint); safecall;
    function Get_Dia25: Smallint; safecall;
    procedure Set_Dia25(pRetVal: Smallint); safecall;
    function Get_Dia26: Smallint; safecall;
    procedure Set_Dia26(pRetVal: Smallint); safecall;
    function Get_Dia27: Smallint; safecall;
    procedure Set_Dia27(pRetVal: Smallint); safecall;
    function Get_Dia28: Smallint; safecall;
    procedure Set_Dia28(pRetVal: Smallint); safecall;
    function Get_Dia29: Smallint; safecall;
    procedure Set_Dia29(pRetVal: Smallint); safecall;
    function Get_Dia30: Smallint; safecall;
    procedure Set_Dia30(pRetVal: Smallint); safecall;
    function Get_Dia31: Smallint; safecall;
    procedure Set_Dia31(pRetVal: Smallint); safecall;
    function Get_Holiday: Smallint; safecall;
    procedure Set_Holiday(pRetVal: Smallint); safecall;
    property ToString: WideString read Get_ToString;
    property Id: Smallint read Get_Id write Set_Id;
    property TypeWorking: TypeWorking read Get_TypeWorking write Set_TypeWorking;
    property Dia01: Smallint read Get_Dia01 write Set_Dia01;
    property Dia02: Smallint read Get_Dia02 write Set_Dia02;
    property Dia03: Smallint read Get_Dia03 write Set_Dia03;
    property Dia04: Smallint read Get_Dia04 write Set_Dia04;
    property Dia05: Smallint read Get_Dia05 write Set_Dia05;
    property Dia06: Smallint read Get_Dia06 write Set_Dia06;
    property Dia07: Smallint read Get_Dia07 write Set_Dia07;
    property Dia08: Smallint read Get_Dia08 write Set_Dia08;
    property Dia09: Smallint read Get_Dia09 write Set_Dia09;
    property Dia10: Smallint read Get_Dia10 write Set_Dia10;
    property Dia11: Smallint read Get_Dia11 write Set_Dia11;
    property Dia12: Smallint read Get_Dia12 write Set_Dia12;
    property Dia13: Smallint read Get_Dia13 write Set_Dia13;
    property Dia14: Smallint read Get_Dia14 write Set_Dia14;
    property Dia15: Smallint read Get_Dia15 write Set_Dia15;
    property Dia16: Smallint read Get_Dia16 write Set_Dia16;
    property Dia17: Smallint read Get_Dia17 write Set_Dia17;
    property Dia18: Smallint read Get_Dia18 write Set_Dia18;
    property Dia19: Smallint read Get_Dia19 write Set_Dia19;
    property Dia20: Smallint read Get_Dia20 write Set_Dia20;
    property Dia21: Smallint read Get_Dia21 write Set_Dia21;
    property Dia22: Smallint read Get_Dia22 write Set_Dia22;
    property Dia23: Smallint read Get_Dia23 write Set_Dia23;
    property Dia24: Smallint read Get_Dia24 write Set_Dia24;
    property Dia25: Smallint read Get_Dia25 write Set_Dia25;
    property Dia26: Smallint read Get_Dia26 write Set_Dia26;
    property Dia27: Smallint read Get_Dia27 write Set_Dia27;
    property Dia28: Smallint read Get_Dia28 write Set_Dia28;
    property Dia29: Smallint read Get_Dia29 write Set_Dia29;
    property Dia30: Smallint read Get_Dia30 write Set_Dia30;
    property Dia31: Smallint read Get_Dia31 write Set_Dia31;
    property Holiday: Smallint read Get_Holiday write Set_Holiday;
  end;

// *********************************************************************//
// DispIntf:  _MonthlyJourneyWorkingDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {CDBBE17C-1B38-3483-9A45-3DA2FB57EA5E}
// *********************************************************************//
  _MonthlyJourneyWorkingDisp = dispinterface
    ['{CDBBE17C-1B38-3483-9A45-3DA2FB57EA5E}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property Id: Smallint dispid 1610743812;
    property TypeWorking: TypeWorking dispid 1610743814;
    property Dia01: Smallint dispid 1610743816;
    property Dia02: Smallint dispid 1610743818;
    property Dia03: Smallint dispid 1610743820;
    property Dia04: Smallint dispid 1610743822;
    property Dia05: Smallint dispid 1610743824;
    property Dia06: Smallint dispid 1610743826;
    property Dia07: Smallint dispid 1610743828;
    property Dia08: Smallint dispid 1610743830;
    property Dia09: Smallint dispid 1610743832;
    property Dia10: Smallint dispid 1610743834;
    property Dia11: Smallint dispid 1610743836;
    property Dia12: Smallint dispid 1610743838;
    property Dia13: Smallint dispid 1610743840;
    property Dia14: Smallint dispid 1610743842;
    property Dia15: Smallint dispid 1610743844;
    property Dia16: Smallint dispid 1610743846;
    property Dia17: Smallint dispid 1610743848;
    property Dia18: Smallint dispid 1610743850;
    property Dia19: Smallint dispid 1610743852;
    property Dia20: Smallint dispid 1610743854;
    property Dia21: Smallint dispid 1610743856;
    property Dia22: Smallint dispid 1610743858;
    property Dia23: Smallint dispid 1610743860;
    property Dia24: Smallint dispid 1610743862;
    property Dia25: Smallint dispid 1610743864;
    property Dia26: Smallint dispid 1610743866;
    property Dia27: Smallint dispid 1610743868;
    property Dia28: Smallint dispid 1610743870;
    property Dia29: Smallint dispid 1610743872;
    property Dia30: Smallint dispid 1610743874;
    property Dia31: Smallint dispid 1610743876;
    property Holiday: Smallint dispid 1610743878;
  end;

// *********************************************************************//
// Interface: _AlarmRingsCollection
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {0F06197D-377B-3293-A017-6E8C39A59C02}
// *********************************************************************//
  _AlarmRingsCollection = interface(IDispatch)
    ['{0F06197D-377B-3293-A017-6E8C39A59C02}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Set_ModeAlarm(pRetVal: Mode); safecall;
    function Get_ModeAlarm: Mode; safecall;
    procedure Add(const AlarmRings: _AlarmRings); safecall;
    function Count: Integer; safecall;
    procedure Remove(const AlarmRings: _AlarmRings); safecall;
    procedure Clear; safecall;
    property ToString: WideString read Get_ToString;
    property ModeAlarm: Mode read Get_ModeAlarm write Set_ModeAlarm;
  end;

// *********************************************************************//
// DispIntf:  _AlarmRingsCollectionDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {0F06197D-377B-3293-A017-6E8C39A59C02}
// *********************************************************************//
  _AlarmRingsCollectionDisp = dispinterface
    ['{0F06197D-377B-3293-A017-6E8C39A59C02}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property ModeAlarm: Mode dispid 1610743812;
    procedure Add(const AlarmRings: _AlarmRings); dispid 1610743814;
    function Count: Integer; dispid 1610743815;
    procedure Remove(const AlarmRings: _AlarmRings); dispid 1610743816;
    procedure Clear; dispid 1610743817;
  end;

// *********************************************************************//
// Interface: _SerialComm
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {399F624C-B7E2-314F-81F1-C7FA4AC9B229}
// *********************************************************************//
  _SerialComm = interface(IDispatch)
    ['{399F624C-B7E2-314F-81F1-C7FA4AC9B229}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Open; safecall;
    procedure Close; safecall;
    procedure Send(data: PSafeArray); safecall;
    function Receive(data: PSafeArray): Integer; safecall;
    procedure SetTimeOut(timeOut: Integer); safecall;
    procedure SetSendBufferSize(bufferSize: Int64); safecall;
    function GetTimeOut: LongWord; safecall;
    function Get_IPAddress: WideString; safecall;
    function Get_IPPort: Integer; safecall;
    function Get_ZkemkeeperObject: IZKEM; safecall;
    procedure _Set_ZkemkeeperObject(const pRetVal: IZKEM); safecall;
    function Get_Connected: WordBool; safecall;
    function Get_Zkemkeeper: WordBool; safecall;
    procedure Set_Zkemkeeper(pRetVal: WordBool); safecall;
    function IsOpened: WordBool; safecall;
    property ToString: WideString read Get_ToString;
    property IPAddress: WideString read Get_IPAddress;
    property IPPort: Integer read Get_IPPort;
    property ZkemkeeperObject: IZKEM read Get_ZkemkeeperObject write _Set_ZkemkeeperObject;
    property Connected: WordBool read Get_Connected;
    property Zkemkeeper: WordBool read Get_Zkemkeeper write Set_Zkemkeeper;
  end;

// *********************************************************************//
// DispIntf:  _SerialCommDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {399F624C-B7E2-314F-81F1-C7FA4AC9B229}
// *********************************************************************//
  _SerialCommDisp = dispinterface
    ['{399F624C-B7E2-314F-81F1-C7FA4AC9B229}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    procedure Open; dispid 1610743812;
    procedure Close; dispid 1610743813;
    procedure Send(data: {??PSafeArray}OleVariant); dispid 1610743814;
    function Receive(data: {??PSafeArray}OleVariant): Integer; dispid 1610743815;
    procedure SetTimeOut(timeOut: Integer); dispid 1610743816;
    procedure SetSendBufferSize(bufferSize: {??Int64}OleVariant); dispid 1610743817;
    function GetTimeOut: LongWord; dispid 1610743818;
    property IPAddress: WideString readonly dispid 1610743819;
    property IPPort: Integer readonly dispid 1610743820;
    property ZkemkeeperObject: IZKEM dispid 1610743821;
    property Connected: WordBool readonly dispid 1610743823;
    property Zkemkeeper: WordBool dispid 1610743824;
    function IsOpened: WordBool; dispid 1610743826;
  end;

// *********************************************************************//
// Interface: _Card
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {B81CD20E-0982-332F-9289-54316DB0B4F0}
// *********************************************************************//
  _Card = interface(IDispatch)
    ['{B81CD20E-0982-332F-9289-54316DB0B4F0}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_code: WideString; safecall;
    procedure Set_code(const pRetVal: WideString); safecall;
    function Get_Dummy: Byte; safecall;
    procedure Set_Dummy(pRetVal: Byte); safecall;
    function Get_message: Byte; safecall;
    procedure Set_message(pRetVal: Byte); safecall;
    function Get_way: Byte; safecall;
    procedure Set_way(pRetVal: Byte); safecall;
    function Get_Jornada: Smallint; safecall;
    procedure Set_Jornada(pRetVal: Smallint); safecall;
    function Get_CounterAccess: TypeCounter_Access; safecall;
    procedure Set_CounterAccess(pRetVal: TypeCounter_Access); safecall;
    function Get_password: Integer; safecall;
    procedure Set_password(pRetVal: Integer); safecall;
    function Get_Template: _TemplateCollection; safecall;
    procedure _Set_Template(const pRetVal: _TemplateCollection); safecall;
    function Get_isMaster: WordBool; safecall;
    procedure Set_isMaster(pRetVal: WordBool); safecall;
    function Get_name: WideString; safecall;
    procedure Set_name(const pRetVal: WideString); safecall;
    function Get_MasterCard: TypeMasterCard; safecall;
    procedure Set_MasterCard(pRetVal: TypeMasterCard); safecall;
    property ToString: WideString read Get_ToString;
    property code: WideString read Get_code write Set_code;
    property Dummy: Byte read Get_Dummy write Set_Dummy;
    property message: Byte read Get_message write Set_message;
    property way: Byte read Get_way write Set_way;
    property Jornada: Smallint read Get_Jornada write Set_Jornada;
    property CounterAccess: TypeCounter_Access read Get_CounterAccess write Set_CounterAccess;
    property password: Integer read Get_password write Set_password;
    property Template: _TemplateCollection read Get_Template write _Set_Template;
    property isMaster: WordBool read Get_isMaster write Set_isMaster;
    property name: WideString read Get_name write Set_name;
    property MasterCard: TypeMasterCard read Get_MasterCard write Set_MasterCard;
  end;

// *********************************************************************//
// DispIntf:  _CardDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {B81CD20E-0982-332F-9289-54316DB0B4F0}
// *********************************************************************//
  _CardDisp = dispinterface
    ['{B81CD20E-0982-332F-9289-54316DB0B4F0}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property code: WideString dispid 1610743812;
    property Dummy: Byte dispid 1610743814;
    property message: Byte dispid 1610743816;
    property way: Byte dispid 1610743818;
    property Jornada: Smallint dispid 1610743820;
    property CounterAccess: {??TypeCounter_Access}OleVariant dispid 1610743822;
    property password: Integer dispid 1610743824;
    property Template: _TemplateCollection dispid 1610743826;
    property isMaster: WordBool dispid 1610743828;
    property name: WideString dispid 1610743830;
    property MasterCard: TypeMasterCard dispid 1610743832;
  end;

// *********************************************************************//
// Interface: _BioPointFingerPrintMessage
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {78DE22E1-EC7C-3544-B176-CD0A746B5722}
// *********************************************************************//
  _BioPointFingerPrintMessage = interface(IDispatch)
    ['{78DE22E1-EC7C-3544-B176-CD0A746B5722}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function GetData: PSafeArray; safecall;
    function GetSize: Integer; safecall;
    procedure Set_LenghtMessage(pRetVal: Integer); safecall;
    function Get_LenghtMessage: Integer; safecall;
    function Get_fingerPrint: WideString; safecall;
    procedure Set_fingerPrint(const pRetVal: WideString); safecall;
    function Get_fingerPrintTypeOne: EFingerPrintType; safecall;
    procedure Set_fingerPrintTypeOne(pRetVal: EFingerPrintType); safecall;
    function Get_fingerPrintTypeTwo: EFingerPrintType; safecall;
    procedure Set_fingerPrintTypeTwo(pRetVal: EFingerPrintType); safecall;
    function Get_Card: WideString; safecall;
    procedure Set_Card(const pRetVal: WideString); safecall;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property fingerPrint: WideString read Get_fingerPrint write Set_fingerPrint;
    property fingerPrintTypeOne: EFingerPrintType read Get_fingerPrintTypeOne write Set_fingerPrintTypeOne;
    property fingerPrintTypeTwo: EFingerPrintType read Get_fingerPrintTypeTwo write Set_fingerPrintTypeTwo;
    property Card: WideString read Get_Card write Set_Card;
  end;

// *********************************************************************//
// DispIntf:  _BioPointFingerPrintMessageDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {78DE22E1-EC7C-3544-B176-CD0A746B5722}
// *********************************************************************//
  _BioPointFingerPrintMessageDisp = dispinterface
    ['{78DE22E1-EC7C-3544-B176-CD0A746B5722}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    function GetData: {??PSafeArray}OleVariant; dispid 1610743812;
    function GetSize: Integer; dispid 1610743813;
    property LenghtMessage: Integer dispid 1610743814;
    property fingerPrint: WideString dispid 1610743816;
    property fingerPrintTypeOne: EFingerPrintType dispid 1610743818;
    property fingerPrintTypeTwo: EFingerPrintType dispid 1610743820;
    property Card: WideString dispid 1610743822;
  end;

// *********************************************************************//
// Interface: _FaceLogRecord
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {294C019E-7BCA-3477-9D0F-26F2C77D1252}
// *********************************************************************//
  _FaceLogRecord = interface(IDispatch)
    ['{294C019E-7BCA-3477-9D0F-26F2C77D1252}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    function Get_employeeID: WideString; safecall;
    procedure Set_employeeID(const pRetVal: WideString); safecall;
    function Get_DateTimeMarkingPoint: TDateTime; safecall;
    procedure Set_DateTimeMarkingPoint(pRetVal: TDateTime); safecall;
    property ToString: WideString read Get_ToString;
    property employeeID: WideString read Get_employeeID write Set_employeeID;
    property DateTimeMarkingPoint: TDateTime read Get_DateTimeMarkingPoint write Set_DateTimeMarkingPoint;
  end;

// *********************************************************************//
// DispIntf:  _FaceLogRecordDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {294C019E-7BCA-3477-9D0F-26F2C77D1252}
// *********************************************************************//
  _FaceLogRecordDisp = dispinterface
    ['{294C019E-7BCA-3477-9D0F-26F2C77D1252}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    property employeeID: WideString dispid 1610743812;
    property DateTimeMarkingPoint: TDateTime dispid 1610743814;
  end;

// *********************************************************************//
// Interface: _EmployeesListTransmissionProgress_2
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {F0AD011C-D37B-3EB6-AD9E-D710A3C9A47A}
// *********************************************************************//
  _EmployeesListTransmissionProgress_2 = interface(IDispatch)
    ['{F0AD011C-D37B-3EB6-AD9E-D710A3C9A47A}']
  end;

// *********************************************************************//
// DispIntf:  _EmployeesListTransmissionProgress_2Disp
// Flags:     (4432) Hidden Dual OleAutomation Dispatchable
// GUID:      {F0AD011C-D37B-3EB6-AD9E-D710A3C9A47A}
// *********************************************************************//
  _EmployeesListTransmissionProgress_2Disp = dispinterface
    ['{F0AD011C-D37B-3EB6-AD9E-D710A3C9A47A}']
  end;

// *********************************************************************//
// The Class CoRelogio provides a Create and CreateRemote method to          
// create instances of the default interface IRelogio exposed by              
// the CoClass Relogio. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoRelogio = class
    class function Create: IRelogio;
    class function CreateRemote(const MachineName: string): IRelogio;
  end;

  TRelogiosetListaBrancaPorIP = procedure(ASender: TObject; const path: WideString; 
                                                            const name: WideString; 
                                                            const ip: WideString; 
                                                            const Id: WideString) of object;
  TRelogiosetListaBrancaPorDriver = procedure(ASender: TObject; const pathIn: WideString; 
                                                                const nameIn: WideString; 
                                                                const pathOut: WideString; 
                                                                const nameOut: WideString) of object;
  TRelogiosetListaNegraPorIP = procedure(ASender: TObject; const path: WideString; 
                                                           const name: WideString; 
                                                           const ip: WideString; 
                                                           const Id: WideString) of object;
  TRelogiosetListaNegraPorDriver = procedure(ASender: TObject; const pathIn: WideString; 
                                                               const nameIn: WideString; 
                                                               const pathOut: WideString; 
                                                               const nameOut: WideString) of object;
  TRelogiosetDataHora = procedure(ASender: TObject; const ip: WideString; 
                                                    const datahora: WideString; const Id: WideString) of object;
  TRelogiocoletaPorIP = procedure(ASender: TObject; const ip: WideString; const path: WideString; 
                                                    const name: WideString; const Id: WideString) of object;
  TRelogiocoletaPorDriver = procedure(ASender: TObject; const pathIn: WideString; 
                                                        const nameIn: WideString; 
                                                        const pathOut: WideString; 
                                                        const nameOut: WideString) of object;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TRelogio
// Help String      : 
// Default Interface: IRelogio
// Def. Intf. DISP? : Yes
// Event   Interface: IRelogio
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TRelogioProperties= class;
{$ENDIF}
  TRelogio = class(TOleServer)
  private
    FOnsetListaBrancaPorIP: TRelogiosetListaBrancaPorIP;
    FOnsetListaBrancaPorDriver: TRelogiosetListaBrancaPorDriver;
    FOnsetListaNegraPorIP: TRelogiosetListaNegraPorIP;
    FOnsetListaNegraPorDriver: TRelogiosetListaNegraPorDriver;
    FOnsetDataHora: TRelogiosetDataHora;
    FOncoletaPorIP: TRelogiocoletaPorIP;
    FOncoletaPorDriver: TRelogiocoletaPorDriver;
    FIntf:        IRelogio;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TRelogioProperties;
    function      GetServerProperties: TRelogioProperties;
{$ENDIF}
    function      GetDefaultInterface: IRelogio;
  protected
    procedure InitServerData; override;
    procedure InvokeEvent(DispID: TDispID; var Params: TVariantArray); override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: IRelogio);
    procedure Disconnect; override;
    function setListaBrancaPorIP(const path: WideString; const name: WideString; 
                                 const ip: WideString; const Id: WideString): WordBool;
    function setListaBrancaPorDriver(const pathIn: WideString; const nameIn: WideString; 
                                     const pathOut: WideString; const nameOut: WideString): WordBool;
    function setListaNegraPorIP(const path: WideString; const name: WideString; 
                                const ip: WideString; const Id: WideString): WordBool;
    function setListaNegraPorDriver(const pathIn: WideString; const nameIn: WideString; 
                                    const pathOut: WideString; const nameOut: WideString): WordBool;
    function setDataHora(const ip: WideString; const datahora: WideString; const Id: WideString): WordBool;
    function coletaPorIP(const ip: WideString; const path: WideString; const name: WideString; 
                         const Id: WideString): WordBool;
    function coletaPorDriver(const pathIn: WideString; const nameIn: WideString; 
                             const pathOut: WideString; const nameOut: WideString): WordBool;
    property DefaultInterface: IRelogio read GetDefaultInterface;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TRelogioProperties read GetServerProperties;
{$ENDIF}
    property OnsetListaBrancaPorIP: TRelogiosetListaBrancaPorIP read FOnsetListaBrancaPorIP write FOnsetListaBrancaPorIP;
    property OnsetListaBrancaPorDriver: TRelogiosetListaBrancaPorDriver read FOnsetListaBrancaPorDriver write FOnsetListaBrancaPorDriver;
    property OnsetListaNegraPorIP: TRelogiosetListaNegraPorIP read FOnsetListaNegraPorIP write FOnsetListaNegraPorIP;
    property OnsetListaNegraPorDriver: TRelogiosetListaNegraPorDriver read FOnsetListaNegraPorDriver write FOnsetListaNegraPorDriver;
    property OnsetDataHora: TRelogiosetDataHora read FOnsetDataHora write FOnsetDataHora;
    property OncoletaPorIP: TRelogiocoletaPorIP read FOncoletaPorIP write FOncoletaPorIP;
    property OncoletaPorDriver: TRelogiocoletaPorDriver read FOncoletaPorDriver write FOncoletaPorDriver;
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TRelogio
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TRelogioProperties = class(TPersistent)
  private
    FServer:    TRelogio;
    function    GetDefaultInterface: IRelogio;
    constructor Create(AServer: TRelogio);
  protected
  public
    property DefaultInterface: IRelogio read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoAbstractMessage provides a Create and CreateRemote method to          
// create instances of the default interface _AbstractMessage exposed by              
// the CoClass AbstractMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAbstractMessage = class
    class function Create: _AbstractMessage;
    class function CreateRemote(const MachineName: string): _AbstractMessage;
  end;

// *********************************************************************//
// The Class CoTCPComm provides a Create and CreateRemote method to          
// create instances of the default interface _TCPComm exposed by              
// the CoClass TCPComm. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoTCPComm = class
    class function Create: _TCPComm;
    class function CreateRemote(const MachineName: string): _TCPComm;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TTCPComm
// Help String      : 
// Default Interface: _TCPComm
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TTCPCommProperties= class;
{$ENDIF}
  TTCPComm = class(TOleServer)
  private
    FIntf:        _TCPComm;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TTCPCommProperties;
    function      GetServerProperties: TTCPCommProperties;
{$ENDIF}
    function      GetDefaultInterface: _TCPComm;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_ZkemkeeperObject: IZKEM;
    procedure _Set_ZkemkeeperObject(const pRetVal: IZKEM);
    function Get_Zkemkeeper: WordBool;
    procedure Set_Zkemkeeper(pRetVal: WordBool);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _TCPComm);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    procedure Open;
    procedure Close;
    procedure Send(data: PSafeArray);
    function Receive(data: PSafeArray): Integer;
    procedure SetTimeOut(timeOut: Integer);
    procedure SetSendBufferSize(bufferSize: Int64);
    function GetTimeOut: LongWord;
    procedure CreateTcpComm(const ip: WideString; port: Integer);
    property DefaultInterface: _TCPComm read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property ZkemkeeperObject: IZKEM read Get_ZkemkeeperObject write _Set_ZkemkeeperObject;
    property Zkemkeeper: WordBool read Get_Zkemkeeper write Set_Zkemkeeper;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TTCPCommProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TTCPComm
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TTCPCommProperties = class(TPersistent)
  private
    FServer:    TTCPComm;
    function    GetDefaultInterface: _TCPComm;
    constructor Create(AServer: TTCPComm);
  protected
    function Get_ToString: WideString;
    function Get_ZkemkeeperObject: IZKEM;
    procedure _Set_ZkemkeeperObject(const pRetVal: IZKEM);
    function Get_Zkemkeeper: WordBool;
    procedure Set_Zkemkeeper(pRetVal: WordBool);
  public
    property DefaultInterface: _TCPComm read GetDefaultInterface;
  published
    property Zkemkeeper: WordBool read Get_Zkemkeeper write Set_Zkemkeeper;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoAbstractProtocol provides a Create and CreateRemote method to          
// create instances of the default interface _AbstractProtocol exposed by              
// the CoClass AbstractProtocol. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAbstractProtocol = class
    class function Create: _AbstractProtocol;
    class function CreateRemote(const MachineName: string): _AbstractProtocol;
  end;

// *********************************************************************//
// The Class CoEmployeesListTransmissionProgress provides a Create and CreateRemote method to          
// create instances of the default interface _EmployeesListTransmissionProgress exposed by              
// the CoClass EmployeesListTransmissionProgress. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoEmployeesListTransmissionProgress = class
    class function Create: _EmployeesListTransmissionProgress;
    class function CreateRemote(const MachineName: string): _EmployeesListTransmissionProgress;
  end;

// *********************************************************************//
// The Class CoCredentialListTransmissionProgress provides a Create and CreateRemote method to          
// create instances of the default interface _CredentialListTransmissionProgress exposed by              
// the CoClass CredentialListTransmissionProgress. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoCredentialListTransmissionProgress = class
    class function Create: _CredentialListTransmissionProgress;
    class function CreateRemote(const MachineName: string): _CredentialListTransmissionProgress;
  end;

// *********************************************************************//
// The Class CoPrintPointInquirySerialNumberOfREPAndMemoryResponse provides a Create and CreateRemote method to          
// create instances of the default interface _PrintPointInquirySerialNumberOfREPAndMemoryResponse exposed by              
// the CoClass PrintPointInquirySerialNumberOfREPAndMemoryResponse. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoPrintPointInquirySerialNumberOfREPAndMemoryResponse = class
    class function Create: _PrintPointInquirySerialNumberOfREPAndMemoryResponse;
    class function CreateRemote(const MachineName: string): _PrintPointInquirySerialNumberOfREPAndMemoryResponse;
  end;

// *********************************************************************//
// The Class CoPrintPointLiFingerPrint provides a Create and CreateRemote method to          
// create instances of the default interface _PrintPointLiFingerPrint exposed by              
// the CoClass PrintPointLiFingerPrint. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoPrintPointLiFingerPrint = class
    class function Create: _PrintPointLiFingerPrint;
    class function CreateRemote(const MachineName: string): _PrintPointLiFingerPrint;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TPrintPointLiFingerPrint
// Help String      : 
// Default Interface: _PrintPointLiFingerPrint
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TPrintPointLiFingerPrintProperties= class;
{$ENDIF}
  TPrintPointLiFingerPrint = class(TOleServer)
  private
    FIntf:        _PrintPointLiFingerPrint;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TPrintPointLiFingerPrintProperties;
    function      GetServerProperties: TPrintPointLiFingerPrintProperties;
{$ENDIF}
    function      GetDefaultInterface: _PrintPointLiFingerPrint;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_fingerPrintType: EFingerPrintType;
    procedure Set_fingerPrintType(pRetVal: EFingerPrintType);
    function Get_fingerPrint: WideString;
    procedure Set_fingerPrint(const pRetVal: WideString);
    function Get_employeeID: Integer;
    procedure Set_employeeID(pRetVal: Integer);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _PrintPointLiFingerPrint);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _PrintPointLiFingerPrint read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property fingerPrintType: EFingerPrintType read Get_fingerPrintType write Set_fingerPrintType;
    property fingerPrint: WideString read Get_fingerPrint write Set_fingerPrint;
    property employeeID: Integer read Get_employeeID write Set_employeeID;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TPrintPointLiFingerPrintProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TPrintPointLiFingerPrint
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TPrintPointLiFingerPrintProperties = class(TPersistent)
  private
    FServer:    TPrintPointLiFingerPrint;
    function    GetDefaultInterface: _PrintPointLiFingerPrint;
    constructor Create(AServer: TPrintPointLiFingerPrint);
  protected
    function Get_ToString: WideString;
    function Get_fingerPrintType: EFingerPrintType;
    procedure Set_fingerPrintType(pRetVal: EFingerPrintType);
    function Get_fingerPrint: WideString;
    procedure Set_fingerPrint(const pRetVal: WideString);
    function Get_employeeID: Integer;
    procedure Set_employeeID(pRetVal: Integer);
  public
    property DefaultInterface: _PrintPointLiFingerPrint read GetDefaultInterface;
  published
    property fingerPrintType: EFingerPrintType read Get_fingerPrintType write Set_fingerPrintType;
    property fingerPrint: WideString read Get_fingerPrint write Set_fingerPrint;
    property employeeID: Integer read Get_employeeID write Set_employeeID;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoPrintPointEmployee provides a Create and CreateRemote method to          
// create instances of the default interface _PrintPointEmployee exposed by              
// the CoClass PrintPointEmployee. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoPrintPointEmployee = class
    class function Create: _PrintPointEmployee;
    class function CreateRemote(const MachineName: string): _PrintPointEmployee;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TPrintPointEmployee
// Help String      : 
// Default Interface: _PrintPointEmployee
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TPrintPointEmployeeProperties= class;
{$ENDIF}
  TPrintPointEmployee = class(TOleServer)
  private
    FIntf:        _PrintPointEmployee;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TPrintPointEmployeeProperties;
    function      GetServerProperties: TPrintPointEmployeeProperties;
{$ENDIF}
    function      GetDefaultInterface: _PrintPointEmployee;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_pis: WideString;
    procedure Set_pis(const pRetVal: WideString);
    function Get_name: WideString;
    procedure Set_name(const pRetVal: WideString);
    function Get_password: WideString;
    procedure Set_password(const pRetVal: WideString);
    function Get_CostCenter: Integer;
    procedure Set_CostCenter(pRetVal: Integer);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _PrintPointEmployee);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    function CompareTo(obj: OleVariant): Integer;
    property DefaultInterface: _PrintPointEmployee read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property pis: WideString read Get_pis write Set_pis;
    property name: WideString read Get_name write Set_name;
    property password: WideString read Get_password write Set_password;
    property CostCenter: Integer read Get_CostCenter write Set_CostCenter;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TPrintPointEmployeeProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TPrintPointEmployee
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TPrintPointEmployeeProperties = class(TPersistent)
  private
    FServer:    TPrintPointEmployee;
    function    GetDefaultInterface: _PrintPointEmployee;
    constructor Create(AServer: TPrintPointEmployee);
  protected
    function Get_ToString: WideString;
    function Get_pis: WideString;
    procedure Set_pis(const pRetVal: WideString);
    function Get_name: WideString;
    procedure Set_name(const pRetVal: WideString);
    function Get_password: WideString;
    procedure Set_password(const pRetVal: WideString);
    function Get_CostCenter: Integer;
    procedure Set_CostCenter(pRetVal: Integer);
  public
    property DefaultInterface: _PrintPointEmployee read GetDefaultInterface;
  published
    property pis: WideString read Get_pis write Set_pis;
    property name: WideString read Get_name write Set_name;
    property password: WideString read Get_password write Set_password;
    property CostCenter: Integer read Get_CostCenter write Set_CostCenter;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoDeviceConnectionException provides a Create and CreateRemote method to          
// create instances of the default interface _DeviceConnectionException exposed by              
// the CoClass DeviceConnectionException. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoDeviceConnectionException = class
    class function Create: _DeviceConnectionException;
    class function CreateRemote(const MachineName: string): _DeviceConnectionException;
  end;

// *********************************************************************//
// The Class CoWatchComm_ provides a Create and CreateRemote method to          
// create instances of the default interface _WatchComm exposed by              
// the CoClass WatchComm_. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoWatchComm_ = class
    class function Create: _WatchComm;
    class function CreateRemote(const MachineName: string): _WatchComm;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TWatchComm
// Help String      : 
// Default Interface: _WatchComm
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TWatchCommProperties= class;
{$ENDIF}
  TWatchComm = class(TOleServer)
  private
    FIntf:        _WatchComm;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TWatchCommProperties;
    function      GetServerProperties: TWatchCommProperties;
{$ENDIF}
    function      GetDefaultInterface: _WatchComm;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_Connected: WordBool;
    function Get_GeneralTimeout: Integer;
    procedure Set_GeneralTimeout1(pRetVal: Integer);
    function Get_CollectTimeout: Integer;
    procedure Set_CollectTimeout1(pRetVal: Integer);
    function Get_Protocol: _AbstractProtocol;
    procedure _Set_Protocol1(const pRetVal: _AbstractProtocol);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _WatchComm);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    procedure AddConfiguration(configurationType: EConfigurationType; field1: OleVariant);
    procedure AddConfiguration_2(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant);
    procedure AddConfiguration_3(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant);
    procedure AddConfiguration_4(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant; field4: OleVariant);
    procedure AddConfiguration_5(configurationType: EConfigurationType; field1: OleVariant; 
                                 field2: OleVariant; field3: OleVariant; field4: OleVariant; 
                                 field5: OleVariant);
    procedure AddParcialConfiguration(ParcialConfigurationType: ParcialConfigurationType; 
                                      field1: OleVariant);
    procedure AddParcialConfiguration_2(ParcialConfigurationType: ParcialConfigurationType; 
                                        field1: OleVariant; field2: OleVariant);
    procedure AddMaster(const pis: WideString; const Card: WideString; const password: WideString; 
                        hasTechniquesProgrammingPermission: WordBool; 
                        hasDataAndTimePermission: WordBool; 
                        hasPenDriveProgrammingPermisision: WordBool);
    procedure AddMaster_2(const pis: WideString; const Card: WideString; 
                          const password: WideString; hasTechniquesProgrammingPermission: WordBool; 
                          hasDataAndTimePermission: WordBool; 
                          hasPenDriveProgrammingPermisision: WordBool; 
                          hasBobbinChangePermisision: WordBool);
    procedure AddCredential(const cardCode: WideString; const pis: WideString; version: Byte);
    procedure AddEmployee(const pis: WideString);
    procedure AddEmployee_2(const pis: WideString; const name: WideString; 
                            const password: WideString);
    procedure AddEmployee_3(const pis: WideString; const name: WideString; 
                            const password: WideString; CostCenter: Integer);
    procedure AddEmployee_4(Id: Integer);
    procedure AddEmployee_5(employeeID: Integer; const pis: WideString; const cpf: WideString; 
                            const Credential: WideString; const name: WideString; 
                            const password: WideString; isMaster: WordBool);
    procedure AddEmployee_6(employeeID: Integer; const Credential: WideString; 
                            const name: WideString; const password: WideString; isMaster: WordBool);
    procedure CreateWatchComm(protocolType: WatchProtocolType; const Comm: IComm; 
                              WatchAddress: Integer; const firmwareVersion: WideString);
    procedure CreateWatchComm_2(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; connectionType: WatchConnectionType; 
                                const firmwareVersion: WideString);
    procedure CreateWatchComm_3(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; const accessKey: WideString; 
                                const firmwareVersion: WideString);
    procedure CreateWatchComm_4(protocolType: WatchProtocolType; const Comm: IComm; 
                                WatchAddress: Integer; const accessKey: WideString; 
                                connectionType: WatchConnectionType; 
                                const firmwareVersion: WideString);
    procedure OpenConnection;
    procedure CloseConnection;
    function GetStatus: _AbstractStatusMessage;
    function GetMAC: _GetMACResponse;
    function GetImmediateStatus: _ImmediateStatusResponse;
    function GetPrintPointStatus: _PrintPointStatusMessage;
    function GetPrintPointLiStatus: _PrintPointLiStatus;
    function GetFaceStatus: _FaceStatus;
    function CollectAll: _ArrayList;
    function GetCurrentPunch: _AbstractMessage;
    function RemoveCurrentPunch: _AbstractMessage;
    procedure SetDST(dstBegin: TDateTime; dstEnd: TDateTime);
    procedure RemoveDST;
    procedure Set1ToN;
    procedure Set1To1;
    procedure SetDateTimeAndDST_2(date: TDateTime; dstBegin: TDateTime; dstEnd: TDateTime);
    procedure SetDateTime(date: TDateTime);
    procedure EmployeesTotalProgrammingBegin;
    procedure EmployeesTotalProgrammingEnd;
    procedure ClearMasterList;
    procedure ClearClockCredentialsList;
    procedure ClearDisplayMessage;
    procedure ClearCostCenterList;
    procedure EnableLogDeniedAccess(enable: WordBool);
    procedure ProgramLotterySampleRate(rate: Byte; inout: Byte);
    procedure ProgramTriggerType(type_: Byte; time: Integer);
    function HardwareTestCollection: PSafeArray;
    procedure ConfigureCard(idLength: Integer; hasChecking: WordBool);
    procedure ConfigureCard_2(idLenghtMinimum: Integer; idLenghtMaximum: Integer; 
                              hasChecking: WordBool; way: WordBool);
    procedure Activation(active: WordBool; controlled: WordBool);
    procedure setCardList(const CardCollection: _CardCollection);
    procedure ConfigureMessage(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                               const Description: WideString);
    procedure ConfigureMessage_2(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                                 const Description: WideString; typeFunction: TypeActionFunction);
    procedure Master(Id: Integer; password: Integer; PenDriveProgramming: WordBool; 
                     DateTimeUpdated: WordBool; ProgrammingTechnical: WordBool);
    procedure ClearProgramming(ClearProgramming: TypeClearProgramming);
    procedure RepositioningMRPRecordsPointer;
    procedure REPPlacesInMaintenance;
    procedure CleanEssentialVariables;
    procedure RebuildEmployeesTable;
    procedure IncludeFingerPrint(const pis: WideString; const fingerPrint: WideString; 
                                 fingerPrintTypeOne: EFingerPrintType; 
                                 fingerPrintTypeTwo: EFingerPrintType; sensor: EfingerPrintSensor);
    procedure IncludeFingerPrint_2(const pis: WideString; const fingerPrint: WideString; 
                                   fingerPrintHandOne: EFingerPrintHand; 
                                   fingerPrintHandTwo: EFingerPrintHand);
    procedure IncludeFingerPrint_3(employeeID: Integer; fingerPrintType: EFingerPrintType; 
                                   const fingerPrint: WideString);
    procedure IncludeCostCenter(costCenterID: Integer; const costCenterDescription: WideString);
    procedure IncludeCredentialList(usesVersion: WordBool);
    procedure IncludeEmployeesList(usesPassword: WordBool; isTotalProgramming: WordBool);
    procedure IncludeEmployeesListWithCostCenter;
    procedure IncludeEmployeesList_2;
    procedure SendDisplayMessage(code: Smallint; const message: WideString);
    procedure SendSettings;
    procedure SendParcialSettings;
    procedure SendMasterList;
    procedure ChangeEmployer(employerType: EmployeerType; const cpf_cnpj: WideString; 
                             const cei: WideString; const name: WideString; 
                             const address: WideString);
    procedure ActivateBootLoader;
    procedure FirstActivation(const serialNumber: WideString; const password: WideString);
    procedure Activation_2(const password: WideString);
    procedure Activation_3;
    procedure ExchangeSealREP(const password: WideString);
    procedure SendMacAddress(const macAddressPart1: WideString; const macAddressPart2: WideString; 
                             const macAddressPart3: WideString; const macAddressPart4: WideString; 
                             const macAddressPart5: WideString; const macAddressPart6: WideString);
    function InquiryFingerPrint(InquiryFingerPrintType: InquiryFingerPrintType): _PrintPointFingerPrintMessage;
    function InquiryFingerPrint_2(employeeID: Integer): PSafeArray;
    function InquiryBioPointFingerPrint: _BioPointFingerPrintMessage;
    function InquiryFaceFingerPrint(employeeID: Integer): PSafeArray;
    function InquiryPrintPointEvent: _PrintPointEvent;
    function ConfirmReceiptPrintPointEvent: _PrintPointEvent;
    function InquiryPrintPointMRPEventLog: _PrintPointMRPEventLog;
    function ConfirmationReceiptFingerPrint: _PrintPointFingerPrintMessage;
    function ConfirmationReceiptBioPointFingerPrint: _BioPointFingerPrintMessage;
    function ConfirmationReceiptMRPRecords: PSafeArray;
    function ConfirmationReceiptEmployeeList: PSafeArray;
    function InquiryEmployeer: _PrintPointEmployerMessage;
    function InquirySerialNumberOfREPAndMemory: _PrintPointInquirySerialNumberOfREPAndMemoryResponse;
    function InquirySerialNumber: WideString;
    function InquiryMRPRecords(inquiryChangeEmployed: WordBool; 
                               inquirySettingRealTimeClock: WordBool; 
                               inquiryRegistrationMarkingPoint: WordBool; 
                               inquiryChangeCompanyIdentification: WordBool): PSafeArray;
    function InquiryMRPRecords_2(startDate: TDateTime; inquiryChangeEmployed: WordBool; 
                                 inquirySettingRealTimeClock: WordBool; 
                                 inquiryRegistrationMarkingPoint: WordBool; 
                                 inquiryChangeCompanyIdentification: WordBool): PSafeArray;
    function InquiryMRPRecords_3(const startNSR: WideString; inquiryChangeEmployed: WordBool; 
                                 inquirySettingRealTimeClock: WordBool; 
                                 inquiryRegistrationMarkingPoint: WordBool; 
                                 inquiryChangeCompanyIdentification: WordBool): PSafeArray;
    function InquiryMRPRecords_4(const startNSR: WideString; var errorOccurredInProcess: WordBool): PSafeArray;
    function InquiryFaceLogRecords: PSafeArray;
    procedure DeleteFaceLogRecords;
    function InquiryFaceTemplate(employeeID: Integer): WideString;
    procedure IncludeFaceTemplate(employeeID: Integer; const faceTemplate: WideString);
    procedure ExcludeFaceTemplate(employeeID: Integer);
    function InquiryRandomNumber: _RandomNumberResponse;
    function InquiryEmployeeList: PSafeArray;
    function InquiryFaceEmployeeList: PSafeArray;
    procedure ExcludeFingerPrint(const pis: WideString);
    procedure ExcludeFingerPrint_2(employeeID: Integer; fingerPrintType: EFingerPrintType);
    procedure ExcludeCostCenter(costCenterID: Integer);
    procedure ExcludeBioPointFingerPrint(const cardNumber: WideString);
    procedure IncludeBioPointFingerPrint(const cardNumber: WideString; 
                                         const fingerPrint: WideString; 
                                         fingerPrintTypeOne: EFingerPrintType; 
                                         fingerPrintTypeTwo: EFingerPrintType);
    procedure ExcludeCredentialList;
    procedure ExcludeEmployeesList;
    procedure ExcludeEmployeesListWithCostNumber;
    procedure RepositioningMRPRecordsPointer_2(date: TDateTime);
    procedure RepositioningMRPRecordsPointer_3(const nsr: WideString);
    procedure setHoliday(day: Byte; month: Byte);
    procedure setHoliday_2(const HolidayCollection: _HolidayCollection);
    procedure sendEmptyMessage;
    procedure SendSerialNumber(plateSerialNumber: Int64; mrpSerialNumber: Int64; 
                               mrpSealNumber: Int64);
    procedure SendSerialNumber_2(const serialNumber: WideString);
    procedure RemoveItemCardList(code: Integer);
    procedure UpdateShiftTable(const ShiftTableCollection: _ShiftTableCollection);
    procedure UpdateFirmware(const filePath: WideString);
    procedure AlarmRing(const AlarmRing: _AlarmRingsCollection);
    procedure setMemoryFormat(const MemoryFormat: _MemoryFormat);
    procedure setAlternativeCode(AlternativeCode: Integer; const code: WideString);
    procedure removeAlternativeCode(AlternativeCode: Integer);
    procedure setJourneyWorking(const JourneyWorking: _JourneyWorking);
    procedure ClearData;
    function GetUnlockCode(const lockCode: WideString): WideString;
    procedure CancelEmployeeTransmission;
    procedure CancelCredentialTransmission;
    procedure ClearCredencialList;
    procedure ClearEmployeeList;
    procedure add_progressOFEmployeesListTransmission(const value: _EmployeesListTransmissionProgress_2);
    procedure remove_progressOFEmployeesListTransmission(const value: _EmployeesListTransmissionProgress_2);
    procedure add_progressOFCredentialsListTransmission(const value: _CredentialsListTransmissionProgress);
    procedure remove_progressOFCredentialsListTransmission(const value: _CredentialsListTransmissionProgress);
    procedure ExcludeFingerPrintList;
    procedure ExcludeFingerPrintWithoutEmployee;
    procedure ExcludeFingerPrint_3(const pis: WideString; sensor: EfingerPrintSensor);
    property DefaultInterface: _WatchComm read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property Connected: WordBool read Get_Connected;
    property GeneralTimeout: Integer read Get_GeneralTimeout;
    property GeneralTimeout1: Integer write Set_GeneralTimeout1;
    property CollectTimeout: Integer read Get_CollectTimeout;
    property CollectTimeout1: Integer write Set_CollectTimeout1;
    property Protocol: _AbstractProtocol read Get_Protocol;
    property Protocol1: _AbstractProtocol write _Set_Protocol1;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TWatchCommProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TWatchComm
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TWatchCommProperties = class(TPersistent)
  private
    FServer:    TWatchComm;
    function    GetDefaultInterface: _WatchComm;
    constructor Create(AServer: TWatchComm);
  protected
    function Get_ToString: WideString;
    function Get_Connected: WordBool;
    function Get_GeneralTimeout: Integer;
    procedure Set_GeneralTimeout1(pRetVal: Integer);
    function Get_CollectTimeout: Integer;
    procedure Set_CollectTimeout1(pRetVal: Integer);
    function Get_Protocol: _AbstractProtocol;
    procedure _Set_Protocol1(const pRetVal: _AbstractProtocol);
  public
    property DefaultInterface: _WatchComm read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoEmployeesListTransmissionProgress_2 provides a Create and CreateRemote method to          
// create instances of the default interface _EmployeesListTransmissionProgress_2 exposed by              
// the CoClass EmployeesListTransmissionProgress_2. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoEmployeesListTransmissionProgress_2 = class
    class function Create: _EmployeesListTransmissionProgress_2;
    class function CreateRemote(const MachineName: string): _EmployeesListTransmissionProgress_2;
  end;

// *********************************************************************//
// The Class CoCredentialsListTransmissionProgress provides a Create and CreateRemote method to          
// create instances of the default interface _CredentialsListTransmissionProgress exposed by              
// the CoClass CredentialsListTransmissionProgress. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoCredentialsListTransmissionProgress = class
    class function Create: _CredentialsListTransmissionProgress;
    class function CreateRemote(const MachineName: string): _CredentialsListTransmissionProgress;
  end;

// *********************************************************************//
// The Class CoPrintPointSendSerialNumberMessage provides a Create and CreateRemote method to          
// create instances of the default interface _PrintPointSendSerialNumberMessage exposed by              
// the CoClass PrintPointSendSerialNumberMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoPrintPointSendSerialNumberMessage = class
    class function Create: _PrintPointSendSerialNumberMessage;
    class function CreateRemote(const MachineName: string): _PrintPointSendSerialNumberMessage;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TPrintPointSendSerialNumberMessage
// Help String      : 
// Default Interface: _PrintPointSendSerialNumberMessage
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TPrintPointSendSerialNumberMessageProperties= class;
{$ENDIF}
  TPrintPointSendSerialNumberMessage = class(TOleServer)
  private
    FIntf:        _PrintPointSendSerialNumberMessage;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TPrintPointSendSerialNumberMessageProperties;
    function      GetServerProperties: TPrintPointSendSerialNumberMessageProperties;
{$ENDIF}
    function      GetDefaultInterface: _PrintPointSendSerialNumberMessage;
  protected
    procedure InitServerData; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _PrintPointSendSerialNumberMessage);
    procedure Disconnect; override;
    property DefaultInterface: _PrintPointSendSerialNumberMessage read GetDefaultInterface;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TPrintPointSendSerialNumberMessageProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TPrintPointSendSerialNumberMessage
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TPrintPointSendSerialNumberMessageProperties = class(TPersistent)
  private
    FServer:    TPrintPointSendSerialNumberMessage;
    function    GetDefaultInterface: _PrintPointSendSerialNumberMessage;
    constructor Create(AServer: TPrintPointSendSerialNumberMessage);
  protected
  public
    property DefaultInterface: _PrintPointSendSerialNumberMessage read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoPrintPointLiStatus provides a Create and CreateRemote method to          
// create instances of the default interface _PrintPointLiStatus exposed by              
// the CoClass PrintPointLiStatus. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoPrintPointLiStatus = class
    class function Create: _PrintPointLiStatus;
    class function CreateRemote(const MachineName: string): _PrintPointLiStatus;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TPrintPointLiStatus
// Help String      : 
// Default Interface: _PrintPointLiStatus
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TPrintPointLiStatusProperties= class;
{$ENDIF}
  TPrintPointLiStatus = class(TOleServer)
  private
    FIntf:        _PrintPointLiStatus;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TPrintPointLiStatusProperties;
    function      GetServerProperties: TPrintPointLiStatusProperties;
{$ENDIF}
    function      GetDefaultInterface: _PrintPointLiStatus;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_RecordsCapacity: Integer;
    function Get_FingerprintCapacity: Integer;
    function Get_UsersCapacity: Integer;
    function Get_MasterOccupation: Integer;
    function Get_PasswordOccupation: Integer;
    function Get_FingerprintOccupation: Integer;
    function Get_UserOccupation: Integer;
    function Get_firmwareVersion: WideString;
    function Get_MRPVersion: WideString;
    function Get_RecordsTotal: Integer;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _PrintPointLiStatus);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _PrintPointLiStatus read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property RecordsCapacity: Integer read Get_RecordsCapacity;
    property FingerprintCapacity: Integer read Get_FingerprintCapacity;
    property UsersCapacity: Integer read Get_UsersCapacity;
    property MasterOccupation: Integer read Get_MasterOccupation;
    property PasswordOccupation: Integer read Get_PasswordOccupation;
    property FingerprintOccupation: Integer read Get_FingerprintOccupation;
    property UserOccupation: Integer read Get_UserOccupation;
    property firmwareVersion: WideString read Get_firmwareVersion;
    property MRPVersion: WideString read Get_MRPVersion;
    property RecordsTotal: Integer read Get_RecordsTotal;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TPrintPointLiStatusProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TPrintPointLiStatus
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TPrintPointLiStatusProperties = class(TPersistent)
  private
    FServer:    TPrintPointLiStatus;
    function    GetDefaultInterface: _PrintPointLiStatus;
    constructor Create(AServer: TPrintPointLiStatus);
  protected
    function Get_ToString: WideString;
    function Get_RecordsCapacity: Integer;
    function Get_FingerprintCapacity: Integer;
    function Get_UsersCapacity: Integer;
    function Get_MasterOccupation: Integer;
    function Get_PasswordOccupation: Integer;
    function Get_FingerprintOccupation: Integer;
    function Get_UserOccupation: Integer;
    function Get_firmwareVersion: WideString;
    function Get_MRPVersion: WideString;
    function Get_RecordsTotal: Integer;
  public
    property DefaultInterface: _PrintPointLiStatus read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoMRPRecord provides a Create and CreateRemote method to          
// create instances of the default interface _MRPRecord exposed by              
// the CoClass MRPRecord. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoMRPRecord = class
    class function Create: _MRPRecord;
    class function CreateRemote(const MachineName: string): _MRPRecord;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TMRPRecord
// Help String      : 
// Default Interface: _MRPRecord
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TMRPRecordProperties= class;
{$ENDIF}
  TMRPRecord = class(TOleServer)
  private
    FIntf:        _MRPRecord;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TMRPRecordProperties;
    function      GetServerProperties: TMRPRecordProperties;
{$ENDIF}
    function      GetDefaultInterface: _MRPRecord;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_nsr: WideString;
    procedure Set_nsr(const pRetVal: WideString);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _MRPRecord);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _MRPRecord read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property nsr: WideString read Get_nsr write Set_nsr;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TMRPRecordProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TMRPRecord
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TMRPRecordProperties = class(TPersistent)
  private
    FServer:    TMRPRecord;
    function    GetDefaultInterface: _MRPRecord;
    constructor Create(AServer: TMRPRecord);
  protected
    function Get_ToString: WideString;
    function Get_nsr: WideString;
    procedure Set_nsr(const pRetVal: WideString);
  public
    property DefaultInterface: _MRPRecord read GetDefaultInterface;
  published
    property nsr: WideString read Get_nsr write Set_nsr;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoMRPRecord_ChangeEmployee provides a Create and CreateRemote method to          
// create instances of the default interface _MRPRecord_ChangeEmployee exposed by              
// the CoClass MRPRecord_ChangeEmployee. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoMRPRecord_ChangeEmployee = class
    class function Create: _MRPRecord_ChangeEmployee;
    class function CreateRemote(const MachineName: string): _MRPRecord_ChangeEmployee;
  end;

// *********************************************************************//
// The Class CoFaceFingerPrint provides a Create and CreateRemote method to          
// create instances of the default interface _FaceFingerPrint exposed by              
// the CoClass FaceFingerPrint. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoFaceFingerPrint = class
    class function Create: _FaceFingerPrint;
    class function CreateRemote(const MachineName: string): _FaceFingerPrint;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TFaceFingerPrint
// Help String      : 
// Default Interface: _FaceFingerPrint
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TFaceFingerPrintProperties= class;
{$ENDIF}
  TFaceFingerPrint = class(TOleServer)
  private
    FIntf:        _FaceFingerPrint;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TFaceFingerPrintProperties;
    function      GetServerProperties: TFaceFingerPrintProperties;
{$ENDIF}
    function      GetDefaultInterface: _FaceFingerPrint;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_fingerPrintType: EFingerPrintType;
    procedure Set_fingerPrintType(pRetVal: EFingerPrintType);
    function Get_fingerPrint: WideString;
    procedure Set_fingerPrint(const pRetVal: WideString);
    function Get_employeeID: Integer;
    procedure Set_employeeID(pRetVal: Integer);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _FaceFingerPrint);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _FaceFingerPrint read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property fingerPrintType: EFingerPrintType read Get_fingerPrintType write Set_fingerPrintType;
    property fingerPrint: WideString read Get_fingerPrint write Set_fingerPrint;
    property employeeID: Integer read Get_employeeID write Set_employeeID;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TFaceFingerPrintProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TFaceFingerPrint
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TFaceFingerPrintProperties = class(TPersistent)
  private
    FServer:    TFaceFingerPrint;
    function    GetDefaultInterface: _FaceFingerPrint;
    constructor Create(AServer: TFaceFingerPrint);
  protected
    function Get_ToString: WideString;
    function Get_fingerPrintType: EFingerPrintType;
    procedure Set_fingerPrintType(pRetVal: EFingerPrintType);
    function Get_fingerPrint: WideString;
    procedure Set_fingerPrint(const pRetVal: WideString);
    function Get_employeeID: Integer;
    procedure Set_employeeID(pRetVal: Integer);
  public
    property DefaultInterface: _FaceFingerPrint read GetDefaultInterface;
  published
    property fingerPrintType: EFingerPrintType read Get_fingerPrintType write Set_fingerPrintType;
    property fingerPrint: WideString read Get_fingerPrint write Set_fingerPrint;
    property employeeID: Integer read Get_employeeID write Set_employeeID;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoAbstractCardList provides a Create and CreateRemote method to          
// create instances of the default interface _AbstractCardList exposed by              
// the CoClass AbstractCardList. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAbstractCardList = class
    class function Create: _AbstractCardList;
    class function CreateRemote(const MachineName: string): _AbstractCardList;
  end;

// *********************************************************************//
// The Class CoAbstractStatusMessage provides a Create and CreateRemote method to          
// create instances of the default interface _AbstractStatusMessage exposed by              
// the CoClass AbstractStatusMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAbstractStatusMessage = class
    class function Create: _AbstractStatusMessage;
    class function CreateRemote(const MachineName: string): _AbstractStatusMessage;
  end;

// *********************************************************************//
// The Class CoMiniPointStatusMessage provides a Create and CreateRemote method to          
// create instances of the default interface _MiniPointStatusMessage exposed by              
// the CoClass MiniPointStatusMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoMiniPointStatusMessage = class
    class function Create: _MiniPointStatusMessage;
    class function CreateRemote(const MachineName: string): _MiniPointStatusMessage;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TMiniPointStatusMessage
// Help String      : 
// Default Interface: _MiniPointStatusMessage
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TMiniPointStatusMessageProperties= class;
{$ENDIF}
  TMiniPointStatusMessage = class(TOleServer)
  private
    FIntf:        _MiniPointStatusMessage;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TMiniPointStatusMessageProperties;
    function      GetServerProperties: TMiniPointStatusMessageProperties;
{$ENDIF}
    function      GetDefaultInterface: _MiniPointStatusMessage;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    function Get_firmwareVersion: WideString;
    procedure Set_firmwareVersion(const pRetVal: WideString);
    function Get_RecordsSize: LongWord;
    procedure Set_RecordsSize(pRetVal: LongWord);
    function Get_RecordsCount: LongWord;
    procedure Set_RecordsCount(pRetVal: LongWord);
    function Get_CardListSize: LongWord;
    procedure Set_CardListSize(pRetVal: LongWord);
    function Get_date: TDateTime;
    procedure Set_date(pRetVal: TDateTime);
    function Get_CheckType: Byte;
    procedure Set_CheckType(pRetVal: Byte);
    function Get_RecordDeniedAccess: WordBool;
    procedure Set_RecordDeniedAccess(pRetVal: WordBool);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _MiniPointStatusMessage);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    function GetData: PSafeArray;
    function GetSize: Integer;
    property DefaultInterface: _MiniPointStatusMessage read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property firmwareVersion: WideString read Get_firmwareVersion write Set_firmwareVersion;
    property RecordsSize: LongWord read Get_RecordsSize write Set_RecordsSize;
    property RecordsCount: LongWord read Get_RecordsCount write Set_RecordsCount;
    property CardListSize: LongWord read Get_CardListSize write Set_CardListSize;
    property date: TDateTime read Get_date write Set_date;
    property CheckType: Byte read Get_CheckType write Set_CheckType;
    property RecordDeniedAccess: WordBool read Get_RecordDeniedAccess write Set_RecordDeniedAccess;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TMiniPointStatusMessageProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TMiniPointStatusMessage
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TMiniPointStatusMessageProperties = class(TPersistent)
  private
    FServer:    TMiniPointStatusMessage;
    function    GetDefaultInterface: _MiniPointStatusMessage;
    constructor Create(AServer: TMiniPointStatusMessage);
  protected
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    function Get_firmwareVersion: WideString;
    procedure Set_firmwareVersion(const pRetVal: WideString);
    function Get_RecordsSize: LongWord;
    procedure Set_RecordsSize(pRetVal: LongWord);
    function Get_RecordsCount: LongWord;
    procedure Set_RecordsCount(pRetVal: LongWord);
    function Get_CardListSize: LongWord;
    procedure Set_CardListSize(pRetVal: LongWord);
    function Get_date: TDateTime;
    procedure Set_date(pRetVal: TDateTime);
    function Get_CheckType: Byte;
    procedure Set_CheckType(pRetVal: Byte);
    function Get_RecordDeniedAccess: WordBool;
    procedure Set_RecordDeniedAccess(pRetVal: WordBool);
  public
    property DefaultInterface: _MiniPointStatusMessage read GetDefaultInterface;
  published
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property firmwareVersion: WideString read Get_firmwareVersion write Set_firmwareVersion;
    property RecordsSize: LongWord read Get_RecordsSize write Set_RecordsSize;
    property RecordsCount: LongWord read Get_RecordsCount write Set_RecordsCount;
    property CardListSize: LongWord read Get_CardListSize write Set_CardListSize;
    property date: TDateTime read Get_date write Set_date;
    property CheckType: Byte read Get_CheckType write Set_CheckType;
    property RecordDeniedAccess: WordBool read Get_RecordDeniedAccess write Set_RecordDeniedAccess;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoNoDataMessage provides a Create and CreateRemote method to          
// create instances of the default interface _NoDataMessage exposed by              
// the CoClass NoDataMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoNoDataMessage = class
    class function Create: _NoDataMessage;
    class function CreateRemote(const MachineName: string): _NoDataMessage;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TNoDataMessage
// Help String      : 
// Default Interface: _NoDataMessage
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TNoDataMessageProperties= class;
{$ENDIF}
  TNoDataMessage = class(TOleServer)
  private
    FIntf:        _NoDataMessage;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TNoDataMessageProperties;
    function      GetServerProperties: TNoDataMessageProperties;
{$ENDIF}
    function      GetDefaultInterface: _NoDataMessage;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _NoDataMessage);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    function GetData: PSafeArray;
    function GetSize: Integer;
    property DefaultInterface: _NoDataMessage read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TNoDataMessageProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TNoDataMessage
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TNoDataMessageProperties = class(TPersistent)
  private
    FServer:    TNoDataMessage;
    function    GetDefaultInterface: _NoDataMessage;
    constructor Create(AServer: TNoDataMessage);
  protected
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
  public
    property DefaultInterface: _NoDataMessage read GetDefaultInterface;
  published
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoTemplate provides a Create and CreateRemote method to          
// create instances of the default interface _Template exposed by              
// the CoClass Template. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoTemplate = class
    class function Create: _Template;
    class function CreateRemote(const MachineName: string): _Template;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TTemplate
// Help String      : 
// Default Interface: _Template
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TTemplateProperties= class;
{$ENDIF}
  TTemplate = class(TOleServer)
  private
    FIntf:        _Template;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TTemplateProperties;
    function      GetServerProperties: TTemplateProperties;
{$ENDIF}
    function      GetDefaultInterface: _Template;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_Digital: WideString;
    procedure Set_Digital(const pRetVal: WideString);
    function Get_DedoDigital: TypeDedoDigital;
    procedure Set_DedoDigital(pRetVal: TypeDedoDigital);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _Template);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _Template read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property Digital: WideString read Get_Digital write Set_Digital;
    property DedoDigital: TypeDedoDigital read Get_DedoDigital write Set_DedoDigital;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TTemplateProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TTemplate
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TTemplateProperties = class(TPersistent)
  private
    FServer:    TTemplate;
    function    GetDefaultInterface: _Template;
    constructor Create(AServer: TTemplate);
  protected
    function Get_ToString: WideString;
    function Get_Digital: WideString;
    procedure Set_Digital(const pRetVal: WideString);
    function Get_DedoDigital: TypeDedoDigital;
    procedure Set_DedoDigital(pRetVal: TypeDedoDigital);
  public
    property DefaultInterface: _Template read GetDefaultInterface;
  published
    property Digital: WideString read Get_Digital write Set_Digital;
    property DedoDigital: TypeDedoDigital read Get_DedoDigital write Set_DedoDigital;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoMiniPointConfigurator provides a Create and CreateRemote method to          
// create instances of the default interface _MiniPointConfigurator exposed by              
// the CoClass MiniPointConfigurator. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoMiniPointConfigurator = class
    class function Create: _MiniPointConfigurator;
    class function CreateRemote(const MachineName: string): _MiniPointConfigurator;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TMiniPointConfigurator
// Help String      : 
// Default Interface: _MiniPointConfigurator
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TMiniPointConfiguratorProperties= class;
{$ENDIF}
  TMiniPointConfigurator = class(TOleServer)
  private
    FIntf:        _MiniPointConfigurator;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TMiniPointConfiguratorProperties;
    function      GetServerProperties: TMiniPointConfiguratorProperties;
{$ENDIF}
    function      GetDefaultInterface: _MiniPointConfigurator;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _MiniPointConfigurator);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _MiniPointConfigurator read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TMiniPointConfiguratorProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TMiniPointConfigurator
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TMiniPointConfiguratorProperties = class(TPersistent)
  private
    FServer:    TMiniPointConfigurator;
    function    GetDefaultInterface: _MiniPointConfigurator;
    constructor Create(AServer: TMiniPointConfigurator);
  protected
    function Get_ToString: WideString;
  public
    property DefaultInterface: _MiniPointConfigurator read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoUtil provides a Create and CreateRemote method to          
// create instances of the default interface _Util exposed by              
// the CoClass Util. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoUtil = class
    class function Create: _Util;
    class function CreateRemote(const MachineName: string): _Util;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TUtil
// Help String      : 
// Default Interface: _Util
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TUtilProperties= class;
{$ENDIF}
  TUtil = class(TOleServer)
  private
    FIntf:        _Util;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TUtilProperties;
    function      GetServerProperties: TUtilProperties;
{$ENDIF}
    function      GetDefaultInterface: _Util;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _Util);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _Util read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TUtilProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TUtil
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TUtilProperties = class(TPersistent)
  private
    FServer:    TUtil;
    function    GetDefaultInterface: _Util;
    constructor Create(AServer: TUtil);
  protected
    function Get_ToString: WideString;
  public
    property DefaultInterface: _Util read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoMicroPointStatusMessage provides a Create and CreateRemote method to          
// create instances of the default interface _MicroPointStatusMessage exposed by              
// the CoClass MicroPointStatusMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoMicroPointStatusMessage = class
    class function Create: _MicroPointStatusMessage;
    class function CreateRemote(const MachineName: string): _MicroPointStatusMessage;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TMicroPointStatusMessage
// Help String      : 
// Default Interface: _MicroPointStatusMessage
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TMicroPointStatusMessageProperties= class;
{$ENDIF}
  TMicroPointStatusMessage = class(TOleServer)
  private
    FIntf:        _MicroPointStatusMessage;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TMicroPointStatusMessageProperties;
    function      GetServerProperties: TMicroPointStatusMessageProperties;
{$ENDIF}
    function      GetDefaultInterface: _MicroPointStatusMessage;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    function Get_firmwareVersion: WideString;
    procedure Set_firmwareVersion(const pRetVal: WideString);
    function Get_RecordsSize: LongWord;
    procedure Set_RecordsSize(pRetVal: LongWord);
    function Get_RecordsCount: LongWord;
    procedure Set_RecordsCount(pRetVal: LongWord);
    function Get_CardListSize: LongWord;
    procedure Set_CardListSize(pRetVal: LongWord);
    function Get_date: TDateTime;
    procedure Set_date(pRetVal: TDateTime);
    function Get_CheckType: Byte;
    procedure Set_CheckType(pRetVal: Byte);
    function Get_RecordDeniedAccess: WordBool;
    procedure Set_RecordDeniedAccess(pRetVal: WordBool);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _MicroPointStatusMessage);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    function GetData: PSafeArray;
    function GetSize: Integer;
    property DefaultInterface: _MicroPointStatusMessage read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property firmwareVersion: WideString read Get_firmwareVersion write Set_firmwareVersion;
    property RecordsSize: LongWord read Get_RecordsSize write Set_RecordsSize;
    property RecordsCount: LongWord read Get_RecordsCount write Set_RecordsCount;
    property CardListSize: LongWord read Get_CardListSize write Set_CardListSize;
    property date: TDateTime read Get_date write Set_date;
    property CheckType: Byte read Get_CheckType write Set_CheckType;
    property RecordDeniedAccess: WordBool read Get_RecordDeniedAccess write Set_RecordDeniedAccess;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TMicroPointStatusMessageProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TMicroPointStatusMessage
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TMicroPointStatusMessageProperties = class(TPersistent)
  private
    FServer:    TMicroPointStatusMessage;
    function    GetDefaultInterface: _MicroPointStatusMessage;
    constructor Create(AServer: TMicroPointStatusMessage);
  protected
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    function Get_firmwareVersion: WideString;
    procedure Set_firmwareVersion(const pRetVal: WideString);
    function Get_RecordsSize: LongWord;
    procedure Set_RecordsSize(pRetVal: LongWord);
    function Get_RecordsCount: LongWord;
    procedure Set_RecordsCount(pRetVal: LongWord);
    function Get_CardListSize: LongWord;
    procedure Set_CardListSize(pRetVal: LongWord);
    function Get_date: TDateTime;
    procedure Set_date(pRetVal: TDateTime);
    function Get_CheckType: Byte;
    procedure Set_CheckType(pRetVal: Byte);
    function Get_RecordDeniedAccess: WordBool;
    procedure Set_RecordDeniedAccess(pRetVal: WordBool);
  public
    property DefaultInterface: _MicroPointStatusMessage read GetDefaultInterface;
  published
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property firmwareVersion: WideString read Get_firmwareVersion write Set_firmwareVersion;
    property RecordsSize: LongWord read Get_RecordsSize write Set_RecordsSize;
    property RecordsCount: LongWord read Get_RecordsCount write Set_RecordsCount;
    property CardListSize: LongWord read Get_CardListSize write Set_CardListSize;
    property date: TDateTime read Get_date write Set_date;
    property CheckType: Byte read Get_CheckType write Set_CheckType;
    property RecordDeniedAccess: WordBool read Get_RecordDeniedAccess write Set_RecordDeniedAccess;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoBioPointCardList provides a Create and CreateRemote method to          
// create instances of the default interface _BioPointCardList exposed by              
// the CoClass BioPointCardList. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoBioPointCardList = class
    class function Create: _BioPointCardList;
    class function CreateRemote(const MachineName: string): _BioPointCardList;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TBioPointCardList
// Help String      : 
// Default Interface: _BioPointCardList
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TBioPointCardListProperties= class;
{$ENDIF}
  TBioPointCardList = class(TOleServer)
  private
    FIntf:        _BioPointCardList;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TBioPointCardListProperties;
    function      GetServerProperties: TBioPointCardListProperties;
{$ENDIF}
    function      GetDefaultInterface: _BioPointCardList;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    procedure _Set_Card(const pRetVal: _Card);
    function Get_Card: _Card;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _BioPointCardList);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    function GetData: PSafeArray;
    function GetSize: Integer;
    property DefaultInterface: _BioPointCardList read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property Card: _Card read Get_Card write _Set_Card;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TBioPointCardListProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TBioPointCardList
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TBioPointCardListProperties = class(TPersistent)
  private
    FServer:    TBioPointCardList;
    function    GetDefaultInterface: _BioPointCardList;
    constructor Create(AServer: TBioPointCardList);
  protected
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    procedure _Set_Card(const pRetVal: _Card);
    function Get_Card: _Card;
  public
    property DefaultInterface: _BioPointCardList read GetDefaultInterface;
  published
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoShiftTable provides a Create and CreateRemote method to          
// create instances of the default interface _ShiftTable exposed by              
// the CoClass ShiftTable. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoShiftTable = class
    class function Create: _ShiftTable;
    class function CreateRemote(const MachineName: string): _ShiftTable;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TShiftTable
// Help String      : 
// Default Interface: _ShiftTable
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TShiftTableProperties= class;
{$ENDIF}
  TShiftTable = class(TOleServer)
  private
    FIntf:        _ShiftTable;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TShiftTableProperties;
    function      GetServerProperties: TShiftTableProperties;
{$ENDIF}
    function      GetDefaultInterface: _ShiftTable;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_Inicio: WideString;
    procedure Set_Inicio(const pRetVal: WideString);
    function Get_Fim: WideString;
    procedure Set_Fim(const pRetVal: WideString);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _ShiftTable);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _ShiftTable read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property Inicio: WideString read Get_Inicio write Set_Inicio;
    property Fim: WideString read Get_Fim write Set_Fim;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TShiftTableProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TShiftTable
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TShiftTableProperties = class(TPersistent)
  private
    FServer:    TShiftTable;
    function    GetDefaultInterface: _ShiftTable;
    constructor Create(AServer: TShiftTable);
  protected
    function Get_ToString: WideString;
    function Get_Inicio: WideString;
    procedure Set_Inicio(const pRetVal: WideString);
    function Get_Fim: WideString;
    procedure Set_Fim(const pRetVal: WideString);
  public
    property DefaultInterface: _ShiftTable read GetDefaultInterface;
  published
    property Inicio: WideString read Get_Inicio write Set_Inicio;
    property Fim: WideString read Get_Fim write Set_Fim;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoJourneyWorking provides a Create and CreateRemote method to          
// create instances of the default interface _JourneyWorking exposed by              
// the CoClass JourneyWorking. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoJourneyWorking = class
    class function Create: _JourneyWorking;
    class function CreateRemote(const MachineName: string): _JourneyWorking;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TJourneyWorking
// Help String      : 
// Default Interface: _JourneyWorking
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TJourneyWorkingProperties= class;
{$ENDIF}
  TJourneyWorking = class(TOleServer)
  private
    FIntf:        _JourneyWorking;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TJourneyWorkingProperties;
    function      GetServerProperties: TJourneyWorkingProperties;
{$ENDIF}
    function      GetDefaultInterface: _JourneyWorking;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_Id(pRetVal: Smallint);
    function Get_Id: Smallint;
    procedure Set_TypeWorking(pRetVal: TypeWorking);
    function Get_TypeWorking: TypeWorking;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _JourneyWorking);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _JourneyWorking read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property Id: Smallint read Get_Id write Set_Id;
    property TypeWorking: TypeWorking read Get_TypeWorking write Set_TypeWorking;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TJourneyWorkingProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TJourneyWorking
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TJourneyWorkingProperties = class(TPersistent)
  private
    FServer:    TJourneyWorking;
    function    GetDefaultInterface: _JourneyWorking;
    constructor Create(AServer: TJourneyWorking);
  protected
    function Get_ToString: WideString;
    procedure Set_Id(pRetVal: Smallint);
    function Get_Id: Smallint;
    procedure Set_TypeWorking(pRetVal: TypeWorking);
    function Get_TypeWorking: TypeWorking;
  public
    property DefaultInterface: _JourneyWorking read GetDefaultInterface;
  published
    property Id: Smallint read Get_Id write Set_Id;
    property TypeWorking: TypeWorking read Get_TypeWorking write Set_TypeWorking;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoPeriodicJourneyWorking provides a Create and CreateRemote method to          
// create instances of the default interface _PeriodicJourneyWorking exposed by              
// the CoClass PeriodicJourneyWorking. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoPeriodicJourneyWorking = class
    class function Create: _PeriodicJourneyWorking;
    class function CreateRemote(const MachineName: string): _PeriodicJourneyWorking;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TPeriodicJourneyWorking
// Help String      : 
// Default Interface: _PeriodicJourneyWorking
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TPeriodicJourneyWorkingProperties= class;
{$ENDIF}
  TPeriodicJourneyWorking = class(TOleServer)
  private
    FIntf:        _PeriodicJourneyWorking;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TPeriodicJourneyWorkingProperties;
    function      GetServerProperties: TPeriodicJourneyWorkingProperties;
{$ENDIF}
    function      GetDefaultInterface: _PeriodicJourneyWorking;
  protected
    procedure InitServerData; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _PeriodicJourneyWorking);
    procedure Disconnect; override;
    property DefaultInterface: _PeriodicJourneyWorking read GetDefaultInterface;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TPeriodicJourneyWorkingProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TPeriodicJourneyWorking
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TPeriodicJourneyWorkingProperties = class(TPersistent)
  private
    FServer:    TPeriodicJourneyWorking;
    function    GetDefaultInterface: _PeriodicJourneyWorking;
    constructor Create(AServer: TPeriodicJourneyWorking);
  protected
  public
    property DefaultInterface: _PeriodicJourneyWorking read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoAbstractJourneyWorking provides a Create and CreateRemote method to          
// create instances of the default interface _AbstractJourneyWorking exposed by              
// the CoClass AbstractJourneyWorking. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAbstractJourneyWorking = class
    class function Create: _AbstractJourneyWorking;
    class function CreateRemote(const MachineName: string): _AbstractJourneyWorking;
  end;

// *********************************************************************//
// The Class CoAbstractPunchMessage provides a Create and CreateRemote method to          
// create instances of the default interface _AbstractPunchMessage exposed by              
// the CoClass AbstractPunchMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAbstractPunchMessage = class
    class function Create: _AbstractPunchMessage;
    class function CreateRemote(const MachineName: string): _AbstractPunchMessage;
  end;

// *********************************************************************//
// The Class CoMRPRecord_ChangeCompanyIdentification provides a Create and CreateRemote method to          
// create instances of the default interface _MRPRecord_ChangeCompanyIdentification exposed by              
// the CoClass MRPRecord_ChangeCompanyIdentification. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoMRPRecord_ChangeCompanyIdentification = class
    class function Create: _MRPRecord_ChangeCompanyIdentification;
    class function CreateRemote(const MachineName: string): _MRPRecord_ChangeCompanyIdentification;
  end;

// *********************************************************************//
// The Class CoAbstractPacket provides a Create and CreateRemote method to          
// create instances of the default interface _AbstractPacket exposed by              
// the CoClass AbstractPacket. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAbstractPacket = class
    class function Create: _AbstractPacket;
    class function CreateRemote(const MachineName: string): _AbstractPacket;
  end;

// *********************************************************************//
// The Class CoMemoryFormat provides a Create and CreateRemote method to          
// create instances of the default interface _MemoryFormat exposed by              
// the CoClass MemoryFormat. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoMemoryFormat = class
    class function Create: _MemoryFormat;
    class function CreateRemote(const MachineName: string): _MemoryFormat;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TMemoryFormat
// Help String      : 
// Default Interface: _MemoryFormat
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TMemoryFormatProperties= class;
{$ENDIF}
  TMemoryFormat = class(TOleServer)
  private
    FIntf:        _MemoryFormat;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TMemoryFormatProperties;
    function      GetServerProperties: TMemoryFormatProperties;
{$ENDIF}
    function      GetDefaultInterface: _MemoryFormat;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_MinDigitCard(pRetVal: Byte);
    function Get_MinDigitCard: Byte;
    procedure Set_MaxDigitCard(pRetVal: Byte);
    function Get_MaxDigitCard: Byte;
    procedure Set_HasWork(pRetVal: WordBool);
    function Get_HasWork: WordBool;
    procedure Set_HasMessage(pRetVal: WordBool);
    function Get_HasMessage: WordBool;
    procedure Set_HasWay(pRetVal: WordBool);
    function Get_HasWay: WordBool;
    procedure Set_HasPassword(pRetVal: WordBool);
    function Get_HasPassword: WordBool;
    procedure Set_CounterAccess(pRetVal: Byte);
    function Get_CounterAccess: Byte;
    procedure Set_QuantityMaxCard(pRetVal: Smallint);
    function Get_QuantityMaxCard: Smallint;
    procedure Set_QuantityMaxAlternativeId(pRetVal: Smallint);
    function Get_QuantityMaxAlternativeId: Smallint;
    procedure Set_QuantityMaxWeeklyWork(pRetVal: Smallint);
    function Get_QuantityMaxWeeklyWork: Smallint;
    procedure Set_QuantityMaxMonthlyWork(pRetVal: Smallint);
    function Get_QuantityMaxMonthlyWork: Smallint;
    procedure Set_QuantityMaxPeriodicWork(pRetVal: Smallint);
    function Get_QuantityMaxPeriodicWork: Smallint;
    procedure Set_QuantityMaxAlarmRing(pRetVal: Byte);
    function Get_QuantityMaxAlarmRing: Byte;
    procedure Set_QuantityMaxShiftTable(pRetVal: Byte);
    function Get_QuantityMaxShiftTable: Byte;
    procedure Set_QuantityMaxHoliday(pRetVal: Byte);
    function Get_QuantityMaxHoliday: Byte;
    procedure Set_QuantityMaxFunction(pRetVal: Byte);
    function Get_QuantityMaxFunction: Byte;
    procedure Set_MaxMessageUser(pRetVal: Byte);
    function Get_MaxMessageUser: Byte;
    procedure Set_TypeCheck(pRetVal: TypeCheckCard);
    function Get_TypeCheck: TypeCheckCard;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _MemoryFormat);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _MemoryFormat read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property MinDigitCard: Byte read Get_MinDigitCard write Set_MinDigitCard;
    property MaxDigitCard: Byte read Get_MaxDigitCard write Set_MaxDigitCard;
    property HasWork: WordBool read Get_HasWork write Set_HasWork;
    property HasMessage: WordBool read Get_HasMessage write Set_HasMessage;
    property HasWay: WordBool read Get_HasWay write Set_HasWay;
    property HasPassword: WordBool read Get_HasPassword write Set_HasPassword;
    property CounterAccess: Byte read Get_CounterAccess write Set_CounterAccess;
    property QuantityMaxCard: Smallint read Get_QuantityMaxCard write Set_QuantityMaxCard;
    property QuantityMaxAlternativeId: Smallint read Get_QuantityMaxAlternativeId write Set_QuantityMaxAlternativeId;
    property QuantityMaxWeeklyWork: Smallint read Get_QuantityMaxWeeklyWork write Set_QuantityMaxWeeklyWork;
    property QuantityMaxMonthlyWork: Smallint read Get_QuantityMaxMonthlyWork write Set_QuantityMaxMonthlyWork;
    property QuantityMaxPeriodicWork: Smallint read Get_QuantityMaxPeriodicWork write Set_QuantityMaxPeriodicWork;
    property QuantityMaxAlarmRing: Byte read Get_QuantityMaxAlarmRing write Set_QuantityMaxAlarmRing;
    property QuantityMaxShiftTable: Byte read Get_QuantityMaxShiftTable write Set_QuantityMaxShiftTable;
    property QuantityMaxHoliday: Byte read Get_QuantityMaxHoliday write Set_QuantityMaxHoliday;
    property QuantityMaxFunction: Byte read Get_QuantityMaxFunction write Set_QuantityMaxFunction;
    property MaxMessageUser: Byte read Get_MaxMessageUser write Set_MaxMessageUser;
    property TypeCheck: TypeCheckCard read Get_TypeCheck write Set_TypeCheck;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TMemoryFormatProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TMemoryFormat
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TMemoryFormatProperties = class(TPersistent)
  private
    FServer:    TMemoryFormat;
    function    GetDefaultInterface: _MemoryFormat;
    constructor Create(AServer: TMemoryFormat);
  protected
    function Get_ToString: WideString;
    procedure Set_MinDigitCard(pRetVal: Byte);
    function Get_MinDigitCard: Byte;
    procedure Set_MaxDigitCard(pRetVal: Byte);
    function Get_MaxDigitCard: Byte;
    procedure Set_HasWork(pRetVal: WordBool);
    function Get_HasWork: WordBool;
    procedure Set_HasMessage(pRetVal: WordBool);
    function Get_HasMessage: WordBool;
    procedure Set_HasWay(pRetVal: WordBool);
    function Get_HasWay: WordBool;
    procedure Set_HasPassword(pRetVal: WordBool);
    function Get_HasPassword: WordBool;
    procedure Set_CounterAccess(pRetVal: Byte);
    function Get_CounterAccess: Byte;
    procedure Set_QuantityMaxCard(pRetVal: Smallint);
    function Get_QuantityMaxCard: Smallint;
    procedure Set_QuantityMaxAlternativeId(pRetVal: Smallint);
    function Get_QuantityMaxAlternativeId: Smallint;
    procedure Set_QuantityMaxWeeklyWork(pRetVal: Smallint);
    function Get_QuantityMaxWeeklyWork: Smallint;
    procedure Set_QuantityMaxMonthlyWork(pRetVal: Smallint);
    function Get_QuantityMaxMonthlyWork: Smallint;
    procedure Set_QuantityMaxPeriodicWork(pRetVal: Smallint);
    function Get_QuantityMaxPeriodicWork: Smallint;
    procedure Set_QuantityMaxAlarmRing(pRetVal: Byte);
    function Get_QuantityMaxAlarmRing: Byte;
    procedure Set_QuantityMaxShiftTable(pRetVal: Byte);
    function Get_QuantityMaxShiftTable: Byte;
    procedure Set_QuantityMaxHoliday(pRetVal: Byte);
    function Get_QuantityMaxHoliday: Byte;
    procedure Set_QuantityMaxFunction(pRetVal: Byte);
    function Get_QuantityMaxFunction: Byte;
    procedure Set_MaxMessageUser(pRetVal: Byte);
    function Get_MaxMessageUser: Byte;
    procedure Set_TypeCheck(pRetVal: TypeCheckCard);
    function Get_TypeCheck: TypeCheckCard;
  public
    property DefaultInterface: _MemoryFormat read GetDefaultInterface;
  published
    property MinDigitCard: Byte read Get_MinDigitCard write Set_MinDigitCard;
    property MaxDigitCard: Byte read Get_MaxDigitCard write Set_MaxDigitCard;
    property HasWork: WordBool read Get_HasWork write Set_HasWork;
    property HasMessage: WordBool read Get_HasMessage write Set_HasMessage;
    property HasWay: WordBool read Get_HasWay write Set_HasWay;
    property HasPassword: WordBool read Get_HasPassword write Set_HasPassword;
    property CounterAccess: Byte read Get_CounterAccess write Set_CounterAccess;
    property QuantityMaxCard: Smallint read Get_QuantityMaxCard write Set_QuantityMaxCard;
    property QuantityMaxAlternativeId: Smallint read Get_QuantityMaxAlternativeId write Set_QuantityMaxAlternativeId;
    property QuantityMaxWeeklyWork: Smallint read Get_QuantityMaxWeeklyWork write Set_QuantityMaxWeeklyWork;
    property QuantityMaxMonthlyWork: Smallint read Get_QuantityMaxMonthlyWork write Set_QuantityMaxMonthlyWork;
    property QuantityMaxPeriodicWork: Smallint read Get_QuantityMaxPeriodicWork write Set_QuantityMaxPeriodicWork;
    property QuantityMaxAlarmRing: Byte read Get_QuantityMaxAlarmRing write Set_QuantityMaxAlarmRing;
    property QuantityMaxShiftTable: Byte read Get_QuantityMaxShiftTable write Set_QuantityMaxShiftTable;
    property QuantityMaxHoliday: Byte read Get_QuantityMaxHoliday write Set_QuantityMaxHoliday;
    property QuantityMaxFunction: Byte read Get_QuantityMaxFunction write Set_QuantityMaxFunction;
    property MaxMessageUser: Byte read Get_MaxMessageUser write Set_MaxMessageUser;
    property TypeCheck: TypeCheckCard read Get_TypeCheck write Set_TypeCheck;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoPrintPointFingerPrintMessage provides a Create and CreateRemote method to          
// create instances of the default interface _PrintPointFingerPrintMessage exposed by              
// the CoClass PrintPointFingerPrintMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoPrintPointFingerPrintMessage = class
    class function Create: _PrintPointFingerPrintMessage;
    class function CreateRemote(const MachineName: string): _PrintPointFingerPrintMessage;
  end;

// *********************************************************************//
// The Class CoBioPointStatusMessage provides a Create and CreateRemote method to          
// create instances of the default interface _BioPointStatusMessage exposed by              
// the CoClass BioPointStatusMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoBioPointStatusMessage = class
    class function Create: _BioPointStatusMessage;
    class function CreateRemote(const MachineName: string): _BioPointStatusMessage;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TBioPointStatusMessage
// Help String      : 
// Default Interface: _BioPointStatusMessage
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TBioPointStatusMessageProperties= class;
{$ENDIF}
  TBioPointStatusMessage = class(TOleServer)
  private
    FIntf:        _BioPointStatusMessage;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TBioPointStatusMessageProperties;
    function      GetServerProperties: TBioPointStatusMessageProperties;
{$ENDIF}
    function      GetDefaultInterface: _BioPointStatusMessage;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    function Get_firmwareVersion: WideString;
    procedure Set_firmwareVersion(const pRetVal: WideString);
    function Get_RecordsSize: LongWord;
    procedure Set_RecordsSize(pRetVal: LongWord);
    function Get_RecordsCount: LongWord;
    procedure Set_RecordsCount(pRetVal: LongWord);
    function Get_CardListSize: LongWord;
    procedure Set_CardListSize(pRetVal: LongWord);
    function Get_date: TDateTime;
    procedure Set_date(pRetVal: TDateTime);
    function Get_CheckType: Byte;
    procedure Set_CheckType(pRetVal: Byte);
    function Get_RecordDeniedAccess: WordBool;
    procedure Set_RecordDeniedAccess(pRetVal: WordBool);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _BioPointStatusMessage);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    function GetData: PSafeArray;
    function GetSize: Integer;
    property DefaultInterface: _BioPointStatusMessage read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property firmwareVersion: WideString read Get_firmwareVersion write Set_firmwareVersion;
    property RecordsSize: LongWord read Get_RecordsSize write Set_RecordsSize;
    property RecordsCount: LongWord read Get_RecordsCount write Set_RecordsCount;
    property CardListSize: LongWord read Get_CardListSize write Set_CardListSize;
    property date: TDateTime read Get_date write Set_date;
    property CheckType: Byte read Get_CheckType write Set_CheckType;
    property RecordDeniedAccess: WordBool read Get_RecordDeniedAccess write Set_RecordDeniedAccess;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TBioPointStatusMessageProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TBioPointStatusMessage
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TBioPointStatusMessageProperties = class(TPersistent)
  private
    FServer:    TBioPointStatusMessage;
    function    GetDefaultInterface: _BioPointStatusMessage;
    constructor Create(AServer: TBioPointStatusMessage);
  protected
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    function Get_firmwareVersion: WideString;
    procedure Set_firmwareVersion(const pRetVal: WideString);
    function Get_RecordsSize: LongWord;
    procedure Set_RecordsSize(pRetVal: LongWord);
    function Get_RecordsCount: LongWord;
    procedure Set_RecordsCount(pRetVal: LongWord);
    function Get_CardListSize: LongWord;
    procedure Set_CardListSize(pRetVal: LongWord);
    function Get_date: TDateTime;
    procedure Set_date(pRetVal: TDateTime);
    function Get_CheckType: Byte;
    procedure Set_CheckType(pRetVal: Byte);
    function Get_RecordDeniedAccess: WordBool;
    procedure Set_RecordDeniedAccess(pRetVal: WordBool);
  public
    property DefaultInterface: _BioPointStatusMessage read GetDefaultInterface;
  published
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property firmwareVersion: WideString read Get_firmwareVersion write Set_firmwareVersion;
    property RecordsSize: LongWord read Get_RecordsSize write Set_RecordsSize;
    property RecordsCount: LongWord read Get_RecordsCount write Set_RecordsCount;
    property CardListSize: LongWord read Get_CardListSize write Set_CardListSize;
    property date: TDateTime read Get_date write Set_date;
    property CheckType: Byte read Get_CheckType write Set_CheckType;
    property RecordDeniedAccess: WordBool read Get_RecordDeniedAccess write Set_RecordDeniedAccess;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoFaceEmployee provides a Create and CreateRemote method to          
// create instances of the default interface _FaceEmployee exposed by              
// the CoClass FaceEmployee. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoFaceEmployee = class
    class function Create: _FaceEmployee;
    class function CreateRemote(const MachineName: string): _FaceEmployee;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TFaceEmployee
// Help String      : 
// Default Interface: _FaceEmployee
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TFaceEmployeeProperties= class;
{$ENDIF}
  TFaceEmployee = class(TOleServer)
  private
    FIntf:        _FaceEmployee;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TFaceEmployeeProperties;
    function      GetServerProperties: TFaceEmployeeProperties;
{$ENDIF}
    function      GetDefaultInterface: _FaceEmployee;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_employeeID: Integer;
    procedure Set_employeeID(pRetVal: Integer);
    function Get_name: WideString;
    procedure Set_name(const pRetVal: WideString);
    function Get_password: WideString;
    procedure Set_password(const pRetVal: WideString);
    function Get_Credential: WideString;
    procedure Set_Credential(const pRetVal: WideString);
    function Get_isMaster: WordBool;
    procedure Set_isMaster(pRetVal: WordBool);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _FaceEmployee);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _FaceEmployee read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property employeeID: Integer read Get_employeeID write Set_employeeID;
    property name: WideString read Get_name write Set_name;
    property password: WideString read Get_password write Set_password;
    property Credential: WideString read Get_Credential write Set_Credential;
    property isMaster: WordBool read Get_isMaster write Set_isMaster;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TFaceEmployeeProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TFaceEmployee
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TFaceEmployeeProperties = class(TPersistent)
  private
    FServer:    TFaceEmployee;
    function    GetDefaultInterface: _FaceEmployee;
    constructor Create(AServer: TFaceEmployee);
  protected
    function Get_ToString: WideString;
    function Get_employeeID: Integer;
    procedure Set_employeeID(pRetVal: Integer);
    function Get_name: WideString;
    procedure Set_name(const pRetVal: WideString);
    function Get_password: WideString;
    procedure Set_password(const pRetVal: WideString);
    function Get_Credential: WideString;
    procedure Set_Credential(const pRetVal: WideString);
    function Get_isMaster: WordBool;
    procedure Set_isMaster(pRetVal: WordBool);
  public
    property DefaultInterface: _FaceEmployee read GetDefaultInterface;
  published
    property employeeID: Integer read Get_employeeID write Set_employeeID;
    property name: WideString read Get_name write Set_name;
    property password: WideString read Get_password write Set_password;
    property Credential: WideString read Get_Credential write Set_Credential;
    property isMaster: WordBool read Get_isMaster write Set_isMaster;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoHoliday provides a Create and CreateRemote method to          
// create instances of the default interface _Holiday exposed by              
// the CoClass Holiday. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoHoliday = class
    class function Create: _Holiday;
    class function CreateRemote(const MachineName: string): _Holiday;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : THoliday
// Help String      : 
// Default Interface: _Holiday
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  THolidayProperties= class;
{$ENDIF}
  THoliday = class(TOleServer)
  private
    FIntf:        _Holiday;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       THolidayProperties;
    function      GetServerProperties: THolidayProperties;
{$ENDIF}
    function      GetDefaultInterface: _Holiday;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_day(pRetVal: Byte);
    function Get_day: Byte;
    procedure Set_month(pRetVal: Byte);
    function Get_month: Byte;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _Holiday);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _Holiday read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property day: Byte read Get_day write Set_day;
    property month: Byte read Get_month write Set_month;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: THolidayProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : THoliday
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 THolidayProperties = class(TPersistent)
  private
    FServer:    THoliday;
    function    GetDefaultInterface: _Holiday;
    constructor Create(AServer: THoliday);
  protected
    function Get_ToString: WideString;
    procedure Set_day(pRetVal: Byte);
    function Get_day: Byte;
    procedure Set_month(pRetVal: Byte);
    function Get_month: Byte;
  public
    property DefaultInterface: _Holiday read GetDefaultInterface;
  published
    property day: Byte read Get_day write Set_day;
    property month: Byte read Get_month write Set_month;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoRandomNumberResponse provides a Create and CreateRemote method to          
// create instances of the default interface _RandomNumberResponse exposed by              
// the CoClass RandomNumberResponse. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoRandomNumberResponse = class
    class function Create: _RandomNumberResponse;
    class function CreateRemote(const MachineName: string): _RandomNumberResponse;
  end;

// *********************************************************************//
// The Class CoPrintPointStatusMessage provides a Create and CreateRemote method to          
// create instances of the default interface _PrintPointStatusMessage exposed by              
// the CoClass PrintPointStatusMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoPrintPointStatusMessage = class
    class function Create: _PrintPointStatusMessage;
    class function CreateRemote(const MachineName: string): _PrintPointStatusMessage;
  end;

// *********************************************************************//
// The Class CoPrintPointEvent provides a Create and CreateRemote method to          
// create instances of the default interface _PrintPointEvent exposed by              
// the CoClass PrintPointEvent. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoPrintPointEvent = class
    class function Create: _PrintPointEvent;
    class function CreateRemote(const MachineName: string): _PrintPointEvent;
  end;

// *********************************************************************//
// The Class CoPrintPointLiEmployee provides a Create and CreateRemote method to          
// create instances of the default interface _PrintPointLiEmployee exposed by              
// the CoClass PrintPointLiEmployee. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoPrintPointLiEmployee = class
    class function Create: _PrintPointLiEmployee;
    class function CreateRemote(const MachineName: string): _PrintPointLiEmployee;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TPrintPointLiEmployee
// Help String      : 
// Default Interface: _PrintPointLiEmployee
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TPrintPointLiEmployeeProperties= class;
{$ENDIF}
  TPrintPointLiEmployee = class(TOleServer)
  private
    FIntf:        _PrintPointLiEmployee;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TPrintPointLiEmployeeProperties;
    function      GetServerProperties: TPrintPointLiEmployeeProperties;
{$ENDIF}
    function      GetDefaultInterface: _PrintPointLiEmployee;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_pis: WideString;
    procedure Set_pis(const pRetVal: WideString);
    function Get_name: WideString;
    procedure Set_name(const pRetVal: WideString);
    function Get_password: WideString;
    procedure Set_password(const pRetVal: WideString);
    function Get_CostCenter: Integer;
    procedure Set_CostCenter(pRetVal: Integer);
    function Get_employeeID: Integer;
    procedure Set_employeeID(pRetVal: Integer);
    function Get_Name_2: WideString;
    procedure Set_Name_2(const pRetVal: WideString);
    function Get_Password_2: WideString;
    procedure Set_Password_2(const pRetVal: WideString);
    function Get_Credential: WideString;
    procedure Set_Credential(const pRetVal: WideString);
    function Get_cpf: WideString;
    procedure Set_cpf(const pRetVal: WideString);
    function Get_isMaster: WordBool;
    procedure Set_isMaster(pRetVal: WordBool);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _PrintPointLiEmployee);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    function CompareTo(obj: OleVariant): Integer;
    property DefaultInterface: _PrintPointLiEmployee read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property pis: WideString read Get_pis write Set_pis;
    property name: WideString read Get_name write Set_name;
    property password: WideString read Get_password write Set_password;
    property CostCenter: Integer read Get_CostCenter write Set_CostCenter;
    property employeeID: Integer read Get_employeeID write Set_employeeID;
    property Name_2: WideString read Get_Name_2 write Set_Name_2;
    property Password_2: WideString read Get_Password_2 write Set_Password_2;
    property Credential: WideString read Get_Credential write Set_Credential;
    property cpf: WideString read Get_cpf write Set_cpf;
    property isMaster: WordBool read Get_isMaster write Set_isMaster;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TPrintPointLiEmployeeProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TPrintPointLiEmployee
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TPrintPointLiEmployeeProperties = class(TPersistent)
  private
    FServer:    TPrintPointLiEmployee;
    function    GetDefaultInterface: _PrintPointLiEmployee;
    constructor Create(AServer: TPrintPointLiEmployee);
  protected
    function Get_ToString: WideString;
    function Get_pis: WideString;
    procedure Set_pis(const pRetVal: WideString);
    function Get_name: WideString;
    procedure Set_name(const pRetVal: WideString);
    function Get_password: WideString;
    procedure Set_password(const pRetVal: WideString);
    function Get_CostCenter: Integer;
    procedure Set_CostCenter(pRetVal: Integer);
    function Get_employeeID: Integer;
    procedure Set_employeeID(pRetVal: Integer);
    function Get_Name_2: WideString;
    procedure Set_Name_2(const pRetVal: WideString);
    function Get_Password_2: WideString;
    procedure Set_Password_2(const pRetVal: WideString);
    function Get_Credential: WideString;
    procedure Set_Credential(const pRetVal: WideString);
    function Get_cpf: WideString;
    procedure Set_cpf(const pRetVal: WideString);
    function Get_isMaster: WordBool;
    procedure Set_isMaster(pRetVal: WordBool);
  public
    property DefaultInterface: _PrintPointLiEmployee read GetDefaultInterface;
  published
    property pis: WideString read Get_pis write Set_pis;
    property name: WideString read Get_name write Set_name;
    property password: WideString read Get_password write Set_password;
    property CostCenter: Integer read Get_CostCenter write Set_CostCenter;
    property employeeID: Integer read Get_employeeID write Set_employeeID;
    property Name_2: WideString read Get_Name_2 write Set_Name_2;
    property Password_2: WideString read Get_Password_2 write Set_Password_2;
    property Credential: WideString read Get_Credential write Set_Credential;
    property cpf: WideString read Get_cpf write Set_cpf;
    property isMaster: WordBool read Get_isMaster write Set_isMaster;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoParcialConfiguration provides a Create and CreateRemote method to          
// create instances of the default interface _ParcialConfiguration exposed by              
// the CoClass ParcialConfiguration. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoParcialConfiguration = class
    class function Create: _ParcialConfiguration;
    class function CreateRemote(const MachineName: string): _ParcialConfiguration;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TParcialConfiguration
// Help String      : 
// Default Interface: _ParcialConfiguration
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TParcialConfigurationProperties= class;
{$ENDIF}
  TParcialConfiguration = class(TOleServer)
  private
    FIntf:        _ParcialConfiguration;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TParcialConfigurationProperties;
    function      GetServerProperties: TParcialConfigurationProperties;
{$ENDIF}
    function      GetDefaultInterface: _ParcialConfiguration;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_configurationType: ParcialConfigurationType;
    procedure Set_configurationType(pRetVal: ParcialConfigurationType);
    function Get_field1: OleVariant;
    procedure _Set_field1(pRetVal: OleVariant);
    function Get_field2: OleVariant;
    procedure _Set_field2(pRetVal: OleVariant);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _ParcialConfiguration);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    procedure ValidateFieldParameters;
    property DefaultInterface: _ParcialConfiguration read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property field1: OleVariant read Get_field1 write _Set_field1;
    property field2: OleVariant read Get_field2 write _Set_field2;
    property configurationType: ParcialConfigurationType read Get_configurationType write Set_configurationType;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TParcialConfigurationProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TParcialConfiguration
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TParcialConfigurationProperties = class(TPersistent)
  private
    FServer:    TParcialConfiguration;
    function    GetDefaultInterface: _ParcialConfiguration;
    constructor Create(AServer: TParcialConfiguration);
  protected
    function Get_ToString: WideString;
    function Get_configurationType: ParcialConfigurationType;
    procedure Set_configurationType(pRetVal: ParcialConfigurationType);
    function Get_field1: OleVariant;
    procedure _Set_field1(pRetVal: OleVariant);
    function Get_field2: OleVariant;
    procedure _Set_field2(pRetVal: OleVariant);
  public
    property DefaultInterface: _ParcialConfiguration read GetDefaultInterface;
  published
    property configurationType: ParcialConfigurationType read Get_configurationType write Set_configurationType;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoMRPRecord_SettingRealTimeClock provides a Create and CreateRemote method to          
// create instances of the default interface _MRPRecord_SettingRealTimeClock exposed by              
// the CoClass MRPRecord_SettingRealTimeClock. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoMRPRecord_SettingRealTimeClock = class
    class function Create: _MRPRecord_SettingRealTimeClock;
    class function CreateRemote(const MachineName: string): _MRPRecord_SettingRealTimeClock;
  end;

// *********************************************************************//
// The Class CoMRPRecord_RegistrationMarkingPoint provides a Create and CreateRemote method to          
// create instances of the default interface _MRPRecord_RegistrationMarkingPoint exposed by              
// the CoClass MRPRecord_RegistrationMarkingPoint. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoMRPRecord_RegistrationMarkingPoint = class
    class function Create: _MRPRecord_RegistrationMarkingPoint;
    class function CreateRemote(const MachineName: string): _MRPRecord_RegistrationMarkingPoint;
  end;

// *********************************************************************//
// The Class CoCardCollection provides a Create and CreateRemote method to          
// create instances of the default interface _CardCollection exposed by              
// the CoClass CardCollection. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoCardCollection = class
    class function Create: _CardCollection;
    class function CreateRemote(const MachineName: string): _CardCollection;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TCardCollection
// Help String      : 
// Default Interface: _CardCollection
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TCardCollectionProperties= class;
{$ENDIF}
  TCardCollection = class(TOleServer)
  private
    FIntf:        _CardCollection;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TCardCollectionProperties;
    function      GetServerProperties: TCardCollectionProperties;
{$ENDIF}
    function      GetDefaultInterface: _CardCollection;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _CardCollection);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    procedure Add(const Card: _Card);
    function Count: Integer;
    procedure Remove(const Card: _Card);
    procedure Clear;
    property DefaultInterface: _CardCollection read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TCardCollectionProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TCardCollection
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TCardCollectionProperties = class(TPersistent)
  private
    FServer:    TCardCollection;
    function    GetDefaultInterface: _CardCollection;
    constructor Create(AServer: TCardCollection);
  protected
    function Get_ToString: WideString;
  public
    property DefaultInterface: _CardCollection read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoHardwareTestCollectionResponse provides a Create and CreateRemote method to          
// create instances of the default interface _HardwareTestCollectionResponse exposed by              
// the CoClass HardwareTestCollectionResponse. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoHardwareTestCollectionResponse = class
    class function Create: _HardwareTestCollectionResponse;
    class function CreateRemote(const MachineName: string): _HardwareTestCollectionResponse;
  end;

// *********************************************************************//
// The Class CoGetMACResponse provides a Create and CreateRemote method to          
// create instances of the default interface _GetMACResponse exposed by              
// the CoClass GetMACResponse. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoGetMACResponse = class
    class function Create: _GetMACResponse;
    class function CreateRemote(const MachineName: string): _GetMACResponse;
  end;

// *********************************************************************//
// The Class CoConcretePunchMessage provides a Create and CreateRemote method to          
// create instances of the default interface _ConcretePunchMessage exposed by              
// the CoClass ConcretePunchMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoConcretePunchMessage = class
    class function Create: _ConcretePunchMessage;
    class function CreateRemote(const MachineName: string): _ConcretePunchMessage;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TConcretePunchMessage
// Help String      : 
// Default Interface: _ConcretePunchMessage
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TConcretePunchMessageProperties= class;
{$ENDIF}
  TConcretePunchMessage = class(TOleServer)
  private
    FIntf:        _ConcretePunchMessage;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TConcretePunchMessageProperties;
    function      GetServerProperties: TConcretePunchMessageProperties;
{$ENDIF}
    function      GetDefaultInterface: _ConcretePunchMessage;
  protected
    procedure InitServerData; override;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _ConcretePunchMessage);
    procedure Disconnect; override;
    property DefaultInterface: _ConcretePunchMessage read GetDefaultInterface;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TConcretePunchMessageProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TConcretePunchMessage
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TConcretePunchMessageProperties = class(TPersistent)
  private
    FServer:    TConcretePunchMessage;
    function    GetDefaultInterface: _ConcretePunchMessage;
    constructor Create(AServer: TConcretePunchMessage);
  protected
  public
    property DefaultInterface: _ConcretePunchMessage read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoAbstractMemoryFormat provides a Create and CreateRemote method to          
// create instances of the default interface _AbstractMemoryFormat exposed by              
// the CoClass AbstractMemoryFormat. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAbstractMemoryFormat = class
    class function Create: _AbstractMemoryFormat;
    class function CreateRemote(const MachineName: string): _AbstractMemoryFormat;
  end;

// *********************************************************************//
// The Class CoBioPointMemoryFormat provides a Create and CreateRemote method to          
// create instances of the default interface _BioPointMemoryFormat exposed by              
// the CoClass BioPointMemoryFormat. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoBioPointMemoryFormat = class
    class function Create: _BioPointMemoryFormat;
    class function CreateRemote(const MachineName: string): _BioPointMemoryFormat;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TBioPointMemoryFormat
// Help String      : 
// Default Interface: _BioPointMemoryFormat
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TBioPointMemoryFormatProperties= class;
{$ENDIF}
  TBioPointMemoryFormat = class(TOleServer)
  private
    FIntf:        _BioPointMemoryFormat;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TBioPointMemoryFormatProperties;
    function      GetServerProperties: TBioPointMemoryFormatProperties;
{$ENDIF}
    function      GetDefaultInterface: _BioPointMemoryFormat;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    procedure _Set_MemoryFormat(const pRetVal: _MemoryFormat);
    function Get_MemoryFormat: _MemoryFormat;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _BioPointMemoryFormat);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    function GetData: PSafeArray;
    function GetSize: Integer;
    property DefaultInterface: _BioPointMemoryFormat read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property MemoryFormat: _MemoryFormat read Get_MemoryFormat write _Set_MemoryFormat;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TBioPointMemoryFormatProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TBioPointMemoryFormat
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TBioPointMemoryFormatProperties = class(TPersistent)
  private
    FServer:    TBioPointMemoryFormat;
    function    GetDefaultInterface: _BioPointMemoryFormat;
    constructor Create(AServer: TBioPointMemoryFormat);
  protected
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    procedure _Set_MemoryFormat(const pRetVal: _MemoryFormat);
    function Get_MemoryFormat: _MemoryFormat;
  public
    property DefaultInterface: _BioPointMemoryFormat read GetDefaultInterface;
  published
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoTemplateCollection provides a Create and CreateRemote method to          
// create instances of the default interface _TemplateCollection exposed by              
// the CoClass TemplateCollection. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoTemplateCollection = class
    class function Create: _TemplateCollection;
    class function CreateRemote(const MachineName: string): _TemplateCollection;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TTemplateCollection
// Help String      : 
// Default Interface: _TemplateCollection
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TTemplateCollectionProperties= class;
{$ENDIF}
  TTemplateCollection = class(TOleServer)
  private
    FIntf:        _TemplateCollection;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TTemplateCollectionProperties;
    function      GetServerProperties: TTemplateCollectionProperties;
{$ENDIF}
    function      GetDefaultInterface: _TemplateCollection;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _TemplateCollection);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    procedure Add(const myTemplate: _Template);
    function Count: Integer;
    procedure Remove(const myTemplate: _Template);
    procedure Clear;
    property DefaultInterface: _TemplateCollection read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TTemplateCollectionProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TTemplateCollection
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TTemplateCollectionProperties = class(TPersistent)
  private
    FServer:    TTemplateCollection;
    function    GetDefaultInterface: _TemplateCollection;
    constructor Create(AServer: TTemplateCollection);
  protected
    function Get_ToString: WideString;
  public
    property DefaultInterface: _TemplateCollection read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoAlarmRings provides a Create and CreateRemote method to          
// create instances of the default interface _AlarmRings exposed by              
// the CoClass AlarmRings. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAlarmRings = class
    class function Create: _AlarmRings;
    class function CreateRemote(const MachineName: string): _AlarmRings;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TAlarmRings
// Help String      : 
// Default Interface: _AlarmRings
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TAlarmRingsProperties= class;
{$ENDIF}
  TAlarmRings = class(TOleServer)
  private
    FIntf:        _AlarmRings;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TAlarmRingsProperties;
    function      GetServerProperties: TAlarmRingsProperties;
{$ENDIF}
    function      GetDefaultInterface: _AlarmRings;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_TypeRing(pRetVal: TypeAlarm);
    function Get_TypeRing: TypeAlarm;
    procedure Set_Id(pRetVal: Byte);
    function Get_Id: Byte;
    procedure Set_TimeAlarm(const pRetVal: WideString);
    function Get_TimeAlarm: WideString;
    procedure Set_Duration(pRetVal: Byte);
    function Get_Duration: Byte;
    procedure Set_RingSunday(pRetVal: TypeRinging);
    function Get_RingSunday: TypeRinging;
    procedure Set_RingMonday(pRetVal: TypeRinging);
    function Get_RingMonday: TypeRinging;
    procedure Set_RingTuesday(pRetVal: TypeRinging);
    function Get_RingTuesday: TypeRinging;
    procedure Set_RingWednesday(pRetVal: TypeRinging);
    function Get_RingWednesday: TypeRinging;
    procedure Set_RingThursday(pRetVal: TypeRinging);
    function Get_RingThursday: TypeRinging;
    procedure Set_RingFriday(pRetVal: TypeRinging);
    function Get_RingFriday: TypeRinging;
    procedure Set_RingSaturday(pRetVal: TypeRinging);
    function Get_RingSaturday: TypeRinging;
    procedure Set_RingHoliday(pRetVal: TypeRinging);
    function Get_RingHoliday: TypeRinging;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _AlarmRings);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _AlarmRings read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property TypeRing: TypeAlarm read Get_TypeRing write Set_TypeRing;
    property Id: Byte read Get_Id write Set_Id;
    property TimeAlarm: WideString read Get_TimeAlarm write Set_TimeAlarm;
    property Duration: Byte read Get_Duration write Set_Duration;
    property RingSunday: TypeRinging read Get_RingSunday write Set_RingSunday;
    property RingMonday: TypeRinging read Get_RingMonday write Set_RingMonday;
    property RingTuesday: TypeRinging read Get_RingTuesday write Set_RingTuesday;
    property RingWednesday: TypeRinging read Get_RingWednesday write Set_RingWednesday;
    property RingThursday: TypeRinging read Get_RingThursday write Set_RingThursday;
    property RingFriday: TypeRinging read Get_RingFriday write Set_RingFriday;
    property RingSaturday: TypeRinging read Get_RingSaturday write Set_RingSaturday;
    property RingHoliday: TypeRinging read Get_RingHoliday write Set_RingHoliday;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TAlarmRingsProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TAlarmRings
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TAlarmRingsProperties = class(TPersistent)
  private
    FServer:    TAlarmRings;
    function    GetDefaultInterface: _AlarmRings;
    constructor Create(AServer: TAlarmRings);
  protected
    function Get_ToString: WideString;
    procedure Set_TypeRing(pRetVal: TypeAlarm);
    function Get_TypeRing: TypeAlarm;
    procedure Set_Id(pRetVal: Byte);
    function Get_Id: Byte;
    procedure Set_TimeAlarm(const pRetVal: WideString);
    function Get_TimeAlarm: WideString;
    procedure Set_Duration(pRetVal: Byte);
    function Get_Duration: Byte;
    procedure Set_RingSunday(pRetVal: TypeRinging);
    function Get_RingSunday: TypeRinging;
    procedure Set_RingMonday(pRetVal: TypeRinging);
    function Get_RingMonday: TypeRinging;
    procedure Set_RingTuesday(pRetVal: TypeRinging);
    function Get_RingTuesday: TypeRinging;
    procedure Set_RingWednesday(pRetVal: TypeRinging);
    function Get_RingWednesday: TypeRinging;
    procedure Set_RingThursday(pRetVal: TypeRinging);
    function Get_RingThursday: TypeRinging;
    procedure Set_RingFriday(pRetVal: TypeRinging);
    function Get_RingFriday: TypeRinging;
    procedure Set_RingSaturday(pRetVal: TypeRinging);
    function Get_RingSaturday: TypeRinging;
    procedure Set_RingHoliday(pRetVal: TypeRinging);
    function Get_RingHoliday: TypeRinging;
  public
    property DefaultInterface: _AlarmRings read GetDefaultInterface;
  published
    property TypeRing: TypeAlarm read Get_TypeRing write Set_TypeRing;
    property Id: Byte read Get_Id write Set_Id;
    property TimeAlarm: WideString read Get_TimeAlarm write Set_TimeAlarm;
    property Duration: Byte read Get_Duration write Set_Duration;
    property RingSunday: TypeRinging read Get_RingSunday write Set_RingSunday;
    property RingMonday: TypeRinging read Get_RingMonday write Set_RingMonday;
    property RingTuesday: TypeRinging read Get_RingTuesday write Set_RingTuesday;
    property RingWednesday: TypeRinging read Get_RingWednesday write Set_RingWednesday;
    property RingThursday: TypeRinging read Get_RingThursday write Set_RingThursday;
    property RingFriday: TypeRinging read Get_RingFriday write Set_RingFriday;
    property RingSaturday: TypeRinging read Get_RingSaturday write Set_RingSaturday;
    property RingHoliday: TypeRinging read Get_RingHoliday write Set_RingHoliday;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoCredential provides a Create and CreateRemote method to          
// create instances of the default interface _Credential exposed by              
// the CoClass Credential. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoCredential = class
    class function Create: _Credential;
    class function CreateRemote(const MachineName: string): _Credential;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TCredential
// Help String      : 
// Default Interface: _Credential
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TCredentialProperties= class;
{$ENDIF}
  TCredential = class(TOleServer)
  private
    FIntf:        _Credential;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TCredentialProperties;
    function      GetServerProperties: TCredentialProperties;
{$ENDIF}
    function      GetDefaultInterface: _Credential;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_cardCode: WideString;
    procedure Set_cardCode(const pRetVal: WideString);
    function Get_pis: WideString;
    procedure Set_pis(const pRetVal: WideString);
    function Get_version: Byte;
    procedure Set_version(pRetVal: Byte);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _Credential);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    function CompareTo(obj: OleVariant): Integer;
    property DefaultInterface: _Credential read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property cardCode: WideString read Get_cardCode write Set_cardCode;
    property pis: WideString read Get_pis write Set_pis;
    property version: Byte read Get_version write Set_version;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TCredentialProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TCredential
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TCredentialProperties = class(TPersistent)
  private
    FServer:    TCredential;
    function    GetDefaultInterface: _Credential;
    constructor Create(AServer: TCredential);
  protected
    function Get_ToString: WideString;
    function Get_cardCode: WideString;
    procedure Set_cardCode(const pRetVal: WideString);
    function Get_pis: WideString;
    procedure Set_pis(const pRetVal: WideString);
    function Get_version: Byte;
    procedure Set_version(pRetVal: Byte);
  public
    property DefaultInterface: _Credential read GetDefaultInterface;
  published
    property cardCode: WideString read Get_cardCode write Set_cardCode;
    property pis: WideString read Get_pis write Set_pis;
    property version: Byte read Get_version write Set_version;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoConfiguration provides a Create and CreateRemote method to          
// create instances of the default interface _Configuration exposed by              
// the CoClass Configuration. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoConfiguration = class
    class function Create: _Configuration;
    class function CreateRemote(const MachineName: string): _Configuration;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TConfiguration
// Help String      : 
// Default Interface: _Configuration
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TConfigurationProperties= class;
{$ENDIF}
  TConfiguration = class(TOleServer)
  private
    FIntf:        _Configuration;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TConfigurationProperties;
    function      GetServerProperties: TConfigurationProperties;
{$ENDIF}
    function      GetDefaultInterface: _Configuration;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_configurationType: EConfigurationType;
    procedure Set_configurationType(pRetVal: EConfigurationType);
    function Get_field1: OleVariant;
    procedure _Set_field1(pRetVal: OleVariant);
    function Get_field2: OleVariant;
    procedure _Set_field2(pRetVal: OleVariant);
    function Get_field3: OleVariant;
    procedure _Set_field3(pRetVal: OleVariant);
    function Get_field4: OleVariant;
    procedure _Set_field4(pRetVal: OleVariant);
    function Get_field5: OleVariant;
    procedure _Set_field5(pRetVal: OleVariant);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _Configuration);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    procedure ValidateFieldParameters;
    property DefaultInterface: _Configuration read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property field1: OleVariant read Get_field1 write _Set_field1;
    property field2: OleVariant read Get_field2 write _Set_field2;
    property field3: OleVariant read Get_field3 write _Set_field3;
    property field4: OleVariant read Get_field4 write _Set_field4;
    property field5: OleVariant read Get_field5 write _Set_field5;
    property configurationType: EConfigurationType read Get_configurationType write Set_configurationType;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TConfigurationProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TConfiguration
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TConfigurationProperties = class(TPersistent)
  private
    FServer:    TConfiguration;
    function    GetDefaultInterface: _Configuration;
    constructor Create(AServer: TConfiguration);
  protected
    function Get_ToString: WideString;
    function Get_configurationType: EConfigurationType;
    procedure Set_configurationType(pRetVal: EConfigurationType);
    function Get_field1: OleVariant;
    procedure _Set_field1(pRetVal: OleVariant);
    function Get_field2: OleVariant;
    procedure _Set_field2(pRetVal: OleVariant);
    function Get_field3: OleVariant;
    procedure _Set_field3(pRetVal: OleVariant);
    function Get_field4: OleVariant;
    procedure _Set_field4(pRetVal: OleVariant);
    function Get_field5: OleVariant;
    procedure _Set_field5(pRetVal: OleVariant);
  public
    property DefaultInterface: _Configuration read GetDefaultInterface;
  published
    property configurationType: EConfigurationType read Get_configurationType write Set_configurationType;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoInvalidMessageException provides a Create and CreateRemote method to          
// create instances of the default interface _InvalidMessageException exposed by              
// the CoClass InvalidMessageException. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoInvalidMessageException = class
    class function Create: _InvalidMessageException;
    class function CreateRemote(const MachineName: string): _InvalidMessageException;
  end;

// *********************************************************************//
// The Class CoFaceStatus provides a Create and CreateRemote method to          
// create instances of the default interface _FaceStatus exposed by              
// the CoClass FaceStatus. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoFaceStatus = class
    class function Create: _FaceStatus;
    class function CreateRemote(const MachineName: string): _FaceStatus;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TFaceStatus
// Help String      : 
// Default Interface: _FaceStatus
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TFaceStatusProperties= class;
{$ENDIF}
  TFaceStatus = class(TOleServer)
  private
    FIntf:        _FaceStatus;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TFaceStatusProperties;
    function      GetServerProperties: TFaceStatusProperties;
{$ENDIF}
    function      GetDefaultInterface: _FaceStatus;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_RecordsCapacity: Integer;
    function Get_RecordsOccupation: Integer;
    function Get_FingerprintCapacity: Integer;
    function Get_UsersCapacity: Integer;
    function Get_UsersOccupation: Integer;
    function Get_MasterOccupation: Integer;
    function Get_PasswordOccupation: Integer;
    function Get_FingerprintOccupation: Integer;
    function Get_firmwareVersion: WideString;
    function Get_DeviceDateTime: TDateTime;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _FaceStatus);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _FaceStatus read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property RecordsCapacity: Integer read Get_RecordsCapacity;
    property RecordsOccupation: Integer read Get_RecordsOccupation;
    property FingerprintCapacity: Integer read Get_FingerprintCapacity;
    property UsersCapacity: Integer read Get_UsersCapacity;
    property UsersOccupation: Integer read Get_UsersOccupation;
    property MasterOccupation: Integer read Get_MasterOccupation;
    property PasswordOccupation: Integer read Get_PasswordOccupation;
    property FingerprintOccupation: Integer read Get_FingerprintOccupation;
    property firmwareVersion: WideString read Get_firmwareVersion;
    property DeviceDateTime: TDateTime read Get_DeviceDateTime;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TFaceStatusProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TFaceStatus
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TFaceStatusProperties = class(TPersistent)
  private
    FServer:    TFaceStatus;
    function    GetDefaultInterface: _FaceStatus;
    constructor Create(AServer: TFaceStatus);
  protected
    function Get_ToString: WideString;
    function Get_RecordsCapacity: Integer;
    function Get_RecordsOccupation: Integer;
    function Get_FingerprintCapacity: Integer;
    function Get_UsersCapacity: Integer;
    function Get_UsersOccupation: Integer;
    function Get_MasterOccupation: Integer;
    function Get_PasswordOccupation: Integer;
    function Get_FingerprintOccupation: Integer;
    function Get_firmwareVersion: WideString;
    function Get_DeviceDateTime: TDateTime;
  public
    property DefaultInterface: _FaceStatus read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoAbstractFactory provides a Create and CreateRemote method to          
// create instances of the default interface _AbstractFactory exposed by              
// the CoClass AbstractFactory. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAbstractFactory = class
    class function Create: _AbstractFactory;
    class function CreateRemote(const MachineName: string): _AbstractFactory;
  end;

// *********************************************************************//
// The Class CoHolidayCollection provides a Create and CreateRemote method to          
// create instances of the default interface _HolidayCollection exposed by              
// the CoClass HolidayCollection. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoHolidayCollection = class
    class function Create: _HolidayCollection;
    class function CreateRemote(const MachineName: string): _HolidayCollection;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : THolidayCollection
// Help String      : 
// Default Interface: _HolidayCollection
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  THolidayCollectionProperties= class;
{$ENDIF}
  THolidayCollection = class(TOleServer)
  private
    FIntf:        _HolidayCollection;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       THolidayCollectionProperties;
    function      GetServerProperties: THolidayCollectionProperties;
{$ENDIF}
    function      GetDefaultInterface: _HolidayCollection;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _HolidayCollection);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    procedure Add(const Holiday: _Holiday);
    function Count: Integer;
    procedure Remove(const Holiday: _Holiday);
    procedure Clear;
    property DefaultInterface: _HolidayCollection read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: THolidayCollectionProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : THolidayCollection
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 THolidayCollectionProperties = class(TPersistent)
  private
    FServer:    THolidayCollection;
    function    GetDefaultInterface: _HolidayCollection;
    constructor Create(AServer: THolidayCollection);
  protected
    function Get_ToString: WideString;
  public
    property DefaultInterface: _HolidayCollection read GetDefaultInterface;
  published
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoWeeklyJourneyWorking provides a Create and CreateRemote method to          
// create instances of the default interface _WeeklyJourneyWorking exposed by              
// the CoClass WeeklyJourneyWorking. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoWeeklyJourneyWorking = class
    class function Create: _WeeklyJourneyWorking;
    class function CreateRemote(const MachineName: string): _WeeklyJourneyWorking;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TWeeklyJourneyWorking
// Help String      : 
// Default Interface: _WeeklyJourneyWorking
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TWeeklyJourneyWorkingProperties= class;
{$ENDIF}
  TWeeklyJourneyWorking = class(TOleServer)
  private
    FIntf:        _WeeklyJourneyWorking;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TWeeklyJourneyWorkingProperties;
    function      GetServerProperties: TWeeklyJourneyWorkingProperties;
{$ENDIF}
    function      GetDefaultInterface: _WeeklyJourneyWorking;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_Id(pRetVal: Smallint);
    function Get_Id: Smallint;
    procedure Set_TypeWorking(pRetVal: TypeWorking);
    function Get_TypeWorking: TypeWorking;
    procedure Set_Sunday(pRetVal: Smallint);
    function Get_Sunday: Smallint;
    procedure Set_Monday(pRetVal: Smallint);
    function Get_Monday: Smallint;
    procedure Set_Tuesday(pRetVal: Smallint);
    function Get_Tuesday: Smallint;
    procedure Set_Wednesday(pRetVal: Smallint);
    function Get_Wednesday: Smallint;
    procedure Set_Thursday(pRetVal: Smallint);
    function Get_Thursday: Smallint;
    procedure Set_Friday(pRetVal: Smallint);
    function Get_Friday: Smallint;
    procedure Set_Saturday(pRetVal: Smallint);
    function Get_Saturday: Smallint;
    function Get_Holiday: Smallint;
    procedure Set_Holiday(pRetVal: Smallint);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _WeeklyJourneyWorking);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _WeeklyJourneyWorking read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property Id: Smallint read Get_Id write Set_Id;
    property TypeWorking: TypeWorking read Get_TypeWorking write Set_TypeWorking;
    property Sunday: Smallint read Get_Sunday write Set_Sunday;
    property Monday: Smallint read Get_Monday write Set_Monday;
    property Tuesday: Smallint read Get_Tuesday write Set_Tuesday;
    property Wednesday: Smallint read Get_Wednesday write Set_Wednesday;
    property Thursday: Smallint read Get_Thursday write Set_Thursday;
    property Friday: Smallint read Get_Friday write Set_Friday;
    property Saturday: Smallint read Get_Saturday write Set_Saturday;
    property Holiday: Smallint read Get_Holiday write Set_Holiday;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TWeeklyJourneyWorkingProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TWeeklyJourneyWorking
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TWeeklyJourneyWorkingProperties = class(TPersistent)
  private
    FServer:    TWeeklyJourneyWorking;
    function    GetDefaultInterface: _WeeklyJourneyWorking;
    constructor Create(AServer: TWeeklyJourneyWorking);
  protected
    function Get_ToString: WideString;
    procedure Set_Id(pRetVal: Smallint);
    function Get_Id: Smallint;
    procedure Set_TypeWorking(pRetVal: TypeWorking);
    function Get_TypeWorking: TypeWorking;
    procedure Set_Sunday(pRetVal: Smallint);
    function Get_Sunday: Smallint;
    procedure Set_Monday(pRetVal: Smallint);
    function Get_Monday: Smallint;
    procedure Set_Tuesday(pRetVal: Smallint);
    function Get_Tuesday: Smallint;
    procedure Set_Wednesday(pRetVal: Smallint);
    function Get_Wednesday: Smallint;
    procedure Set_Thursday(pRetVal: Smallint);
    function Get_Thursday: Smallint;
    procedure Set_Friday(pRetVal: Smallint);
    function Get_Friday: Smallint;
    procedure Set_Saturday(pRetVal: Smallint);
    function Get_Saturday: Smallint;
    function Get_Holiday: Smallint;
    procedure Set_Holiday(pRetVal: Smallint);
  public
    property DefaultInterface: _WeeklyJourneyWorking read GetDefaultInterface;
  published
    property Id: Smallint read Get_Id write Set_Id;
    property TypeWorking: TypeWorking read Get_TypeWorking write Set_TypeWorking;
    property Sunday: Smallint read Get_Sunday write Set_Sunday;
    property Monday: Smallint read Get_Monday write Set_Monday;
    property Tuesday: Smallint read Get_Tuesday write Set_Tuesday;
    property Wednesday: Smallint read Get_Wednesday write Set_Wednesday;
    property Thursday: Smallint read Get_Thursday write Set_Thursday;
    property Friday: Smallint read Get_Friday write Set_Friday;
    property Saturday: Smallint read Get_Saturday write Set_Saturday;
    property Holiday: Smallint read Get_Holiday write Set_Holiday;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoImmediateStatusResponse provides a Create and CreateRemote method to          
// create instances of the default interface _ImmediateStatusResponse exposed by              
// the CoClass ImmediateStatusResponse. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoImmediateStatusResponse = class
    class function Create: _ImmediateStatusResponse;
    class function CreateRemote(const MachineName: string): _ImmediateStatusResponse;
  end;

// *********************************************************************//
// The Class CoPrintPointMRPEventLog provides a Create and CreateRemote method to          
// create instances of the default interface _PrintPointMRPEventLog exposed by              
// the CoClass PrintPointMRPEventLog. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoPrintPointMRPEventLog = class
    class function Create: _PrintPointMRPEventLog;
    class function CreateRemote(const MachineName: string): _PrintPointMRPEventLog;
  end;

// *********************************************************************//
// The Class CoMaster provides a Create and CreateRemote method to          
// create instances of the default interface _Master exposed by              
// the CoClass Master. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoMaster = class
    class function Create: _Master;
    class function CreateRemote(const MachineName: string): _Master;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TMaster
// Help String      : 
// Default Interface: _Master
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TMasterProperties= class;
{$ENDIF}
  TMaster = class(TOleServer)
  private
    FIntf:        _Master;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TMasterProperties;
    function      GetServerProperties: TMasterProperties;
{$ENDIF}
    function      GetDefaultInterface: _Master;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_pis: WideString;
    procedure Set_pis(const pRetVal: WideString);
    function Get_Card: WideString;
    procedure Set_Card(const pRetVal: WideString);
    function Get_password: WideString;
    procedure Set_password(const pRetVal: WideString);
    function Get_hasTechniquesProgrammingPermission: WordBool;
    procedure Set_hasTechniquesProgrammingPermission(pRetVal: WordBool);
    function Get_hasDataAndTimePermission: WordBool;
    procedure Set_hasDataAndTimePermission(pRetVal: WordBool);
    function Get_HasPenDriveProgrammingPermission: WordBool;
    procedure Set_HasPenDriveProgrammingPermission(pRetVal: WordBool);
    function Get_HasBobbinChangePermission: WordBool;
    procedure Set_HasBobbinChangePermission(pRetVal: WordBool);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _Master);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _Master read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property pis: WideString read Get_pis write Set_pis;
    property Card: WideString read Get_Card write Set_Card;
    property password: WideString read Get_password write Set_password;
    property hasTechniquesProgrammingPermission: WordBool read Get_hasTechniquesProgrammingPermission write Set_hasTechniquesProgrammingPermission;
    property hasDataAndTimePermission: WordBool read Get_hasDataAndTimePermission write Set_hasDataAndTimePermission;
    property HasPenDriveProgrammingPermission: WordBool read Get_HasPenDriveProgrammingPermission write Set_HasPenDriveProgrammingPermission;
    property HasBobbinChangePermission: WordBool read Get_HasBobbinChangePermission write Set_HasBobbinChangePermission;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TMasterProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TMaster
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TMasterProperties = class(TPersistent)
  private
    FServer:    TMaster;
    function    GetDefaultInterface: _Master;
    constructor Create(AServer: TMaster);
  protected
    function Get_ToString: WideString;
    function Get_pis: WideString;
    procedure Set_pis(const pRetVal: WideString);
    function Get_Card: WideString;
    procedure Set_Card(const pRetVal: WideString);
    function Get_password: WideString;
    procedure Set_password(const pRetVal: WideString);
    function Get_hasTechniquesProgrammingPermission: WordBool;
    procedure Set_hasTechniquesProgrammingPermission(pRetVal: WordBool);
    function Get_hasDataAndTimePermission: WordBool;
    procedure Set_hasDataAndTimePermission(pRetVal: WordBool);
    function Get_HasPenDriveProgrammingPermission: WordBool;
    procedure Set_HasPenDriveProgrammingPermission(pRetVal: WordBool);
    function Get_HasBobbinChangePermission: WordBool;
    procedure Set_HasBobbinChangePermission(pRetVal: WordBool);
  public
    property DefaultInterface: _Master read GetDefaultInterface;
  published
    property pis: WideString read Get_pis write Set_pis;
    property Card: WideString read Get_Card write Set_Card;
    property password: WideString read Get_password write Set_password;
    property hasTechniquesProgrammingPermission: WordBool read Get_hasTechniquesProgrammingPermission write Set_hasTechniquesProgrammingPermission;
    property hasDataAndTimePermission: WordBool read Get_hasDataAndTimePermission write Set_hasDataAndTimePermission;
    property HasPenDriveProgrammingPermission: WordBool read Get_HasPenDriveProgrammingPermission write Set_HasPenDriveProgrammingPermission;
    property HasBobbinChangePermission: WordBool read Get_HasBobbinChangePermission write Set_HasBobbinChangePermission;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoPrintPointEmployerMessage provides a Create and CreateRemote method to          
// create instances of the default interface _PrintPointEmployerMessage exposed by              
// the CoClass PrintPointEmployerMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoPrintPointEmployerMessage = class
    class function Create: _PrintPointEmployerMessage;
    class function CreateRemote(const MachineName: string): _PrintPointEmployerMessage;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TPrintPointEmployerMessage
// Help String      : 
// Default Interface: _PrintPointEmployerMessage
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TPrintPointEmployerMessageProperties= class;
{$ENDIF}
  TPrintPointEmployerMessage = class(TOleServer)
  private
    FIntf:        _PrintPointEmployerMessage;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TPrintPointEmployerMessageProperties;
    function      GetServerProperties: TPrintPointEmployerMessageProperties;
{$ENDIF}
    function      GetDefaultInterface: _PrintPointEmployerMessage;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    function Get_employerType: EmployeerType;
    procedure Set_employerType(pRetVal: EmployeerType);
    function Get_cpf_cnpj: WideString;
    procedure Set_cpf_cnpj(const pRetVal: WideString);
    function Get_cei: WideString;
    procedure Set_cei(const pRetVal: WideString);
    function Get_name: WideString;
    procedure Set_name(const pRetVal: WideString);
    function Get_address: WideString;
    procedure Set_address(const pRetVal: WideString);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _PrintPointEmployerMessage);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    function GetData: PSafeArray;
    function GetSize: Integer;
    function MakeEmployeerFile(const repSerialNumber: WideString; const windowTitle: WideString; 
                               const path: WideString): WordBool;
    property DefaultInterface: _PrintPointEmployerMessage read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property employerType: EmployeerType read Get_employerType write Set_employerType;
    property cpf_cnpj: WideString read Get_cpf_cnpj write Set_cpf_cnpj;
    property cei: WideString read Get_cei write Set_cei;
    property name: WideString read Get_name write Set_name;
    property address: WideString read Get_address write Set_address;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TPrintPointEmployerMessageProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TPrintPointEmployerMessage
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TPrintPointEmployerMessageProperties = class(TPersistent)
  private
    FServer:    TPrintPointEmployerMessage;
    function    GetDefaultInterface: _PrintPointEmployerMessage;
    constructor Create(AServer: TPrintPointEmployerMessage);
  protected
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    function Get_employerType: EmployeerType;
    procedure Set_employerType(pRetVal: EmployeerType);
    function Get_cpf_cnpj: WideString;
    procedure Set_cpf_cnpj(const pRetVal: WideString);
    function Get_cei: WideString;
    procedure Set_cei(const pRetVal: WideString);
    function Get_name: WideString;
    procedure Set_name(const pRetVal: WideString);
    function Get_address: WideString;
    procedure Set_address(const pRetVal: WideString);
  public
    property DefaultInterface: _PrintPointEmployerMessage read GetDefaultInterface;
  published
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
    property employerType: EmployeerType read Get_employerType write Set_employerType;
    property cpf_cnpj: WideString read Get_cpf_cnpj write Set_cpf_cnpj;
    property cei: WideString read Get_cei write Set_cei;
    property name: WideString read Get_name write Set_name;
    property address: WideString read Get_address write Set_address;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoMicropointCardList provides a Create and CreateRemote method to          
// create instances of the default interface _MicropointCardList exposed by              
// the CoClass MicropointCardList. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoMicropointCardList = class
    class function Create: _MicropointCardList;
    class function CreateRemote(const MachineName: string): _MicropointCardList;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TMicropointCardList
// Help String      : 
// Default Interface: _MicropointCardList
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TMicropointCardListProperties= class;
{$ENDIF}
  TMicropointCardList = class(TOleServer)
  private
    FIntf:        _MicropointCardList;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TMicropointCardListProperties;
    function      GetServerProperties: TMicropointCardListProperties;
{$ENDIF}
    function      GetDefaultInterface: _MicropointCardList;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    procedure _Set_Card(const pRetVal: _Card);
    function Get_Card: _Card;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _MicropointCardList);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    function GetData: PSafeArray;
    function GetSize: Integer;
    property DefaultInterface: _MicropointCardList read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property Card: _Card read Get_Card write _Set_Card;
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TMicropointCardListProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TMicropointCardList
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TMicropointCardListProperties = class(TPersistent)
  private
    FServer:    TMicropointCardList;
    function    GetDefaultInterface: _MicropointCardList;
    constructor Create(AServer: TMicropointCardList);
  protected
    function Get_ToString: WideString;
    procedure Set_LenghtMessage(pRetVal: Integer);
    function Get_LenghtMessage: Integer;
    procedure _Set_Card(const pRetVal: _Card);
    function Get_Card: _Card;
  public
    property DefaultInterface: _MicropointCardList read GetDefaultInterface;
  published
    property LenghtMessage: Integer read Get_LenghtMessage write Set_LenghtMessage;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoShiftTableCollection provides a Create and CreateRemote method to          
// create instances of the default interface _ShiftTableCollection exposed by              
// the CoClass ShiftTableCollection. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoShiftTableCollection = class
    class function Create: _ShiftTableCollection;
    class function CreateRemote(const MachineName: string): _ShiftTableCollection;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TShiftTableCollection
// Help String      : 
// Default Interface: _ShiftTableCollection
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TShiftTableCollectionProperties= class;
{$ENDIF}
  TShiftTableCollection = class(TOleServer)
  private
    FIntf:        _ShiftTableCollection;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TShiftTableCollectionProperties;
    function      GetServerProperties: TShiftTableCollectionProperties;
{$ENDIF}
    function      GetDefaultInterface: _ShiftTableCollection;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_Id: Smallint;
    procedure Set_Id(pRetVal: Smallint);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _ShiftTableCollection);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    procedure Add(const faixa: _ShiftTable);
    function Count: Integer;
    procedure Remove(const faixa: _ShiftTable);
    procedure Clear;
    property DefaultInterface: _ShiftTableCollection read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property Id: Smallint read Get_Id write Set_Id;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TShiftTableCollectionProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TShiftTableCollection
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TShiftTableCollectionProperties = class(TPersistent)
  private
    FServer:    TShiftTableCollection;
    function    GetDefaultInterface: _ShiftTableCollection;
    constructor Create(AServer: TShiftTableCollection);
  protected
    function Get_ToString: WideString;
    function Get_Id: Smallint;
    procedure Set_Id(pRetVal: Smallint);
  public
    property DefaultInterface: _ShiftTableCollection read GetDefaultInterface;
  published
    property Id: Smallint read Get_Id write Set_Id;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoMonthlyJourneyWorking provides a Create and CreateRemote method to          
// create instances of the default interface _MonthlyJourneyWorking exposed by              
// the CoClass MonthlyJourneyWorking. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoMonthlyJourneyWorking = class
    class function Create: _MonthlyJourneyWorking;
    class function CreateRemote(const MachineName: string): _MonthlyJourneyWorking;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TMonthlyJourneyWorking
// Help String      : 
// Default Interface: _MonthlyJourneyWorking
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TMonthlyJourneyWorkingProperties= class;
{$ENDIF}
  TMonthlyJourneyWorking = class(TOleServer)
  private
    FIntf:        _MonthlyJourneyWorking;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TMonthlyJourneyWorkingProperties;
    function      GetServerProperties: TMonthlyJourneyWorkingProperties;
{$ENDIF}
    function      GetDefaultInterface: _MonthlyJourneyWorking;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_Id(pRetVal: Smallint);
    function Get_Id: Smallint;
    procedure Set_TypeWorking(pRetVal: TypeWorking);
    function Get_TypeWorking: TypeWorking;
    function Get_Dia01: Smallint;
    procedure Set_Dia01(pRetVal: Smallint);
    function Get_Dia02: Smallint;
    procedure Set_Dia02(pRetVal: Smallint);
    function Get_Dia03: Smallint;
    procedure Set_Dia03(pRetVal: Smallint);
    function Get_Dia04: Smallint;
    procedure Set_Dia04(pRetVal: Smallint);
    function Get_Dia05: Smallint;
    procedure Set_Dia05(pRetVal: Smallint);
    function Get_Dia06: Smallint;
    procedure Set_Dia06(pRetVal: Smallint);
    function Get_Dia07: Smallint;
    procedure Set_Dia07(pRetVal: Smallint);
    function Get_Dia08: Smallint;
    procedure Set_Dia08(pRetVal: Smallint);
    function Get_Dia09: Smallint;
    procedure Set_Dia09(pRetVal: Smallint);
    function Get_Dia10: Smallint;
    procedure Set_Dia10(pRetVal: Smallint);
    function Get_Dia11: Smallint;
    procedure Set_Dia11(pRetVal: Smallint);
    function Get_Dia12: Smallint;
    procedure Set_Dia12(pRetVal: Smallint);
    function Get_Dia13: Smallint;
    procedure Set_Dia13(pRetVal: Smallint);
    function Get_Dia14: Smallint;
    procedure Set_Dia14(pRetVal: Smallint);
    function Get_Dia15: Smallint;
    procedure Set_Dia15(pRetVal: Smallint);
    function Get_Dia16: Smallint;
    procedure Set_Dia16(pRetVal: Smallint);
    function Get_Dia17: Smallint;
    procedure Set_Dia17(pRetVal: Smallint);
    function Get_Dia18: Smallint;
    procedure Set_Dia18(pRetVal: Smallint);
    function Get_Dia19: Smallint;
    procedure Set_Dia19(pRetVal: Smallint);
    function Get_Dia20: Smallint;
    procedure Set_Dia20(pRetVal: Smallint);
    function Get_Dia21: Smallint;
    procedure Set_Dia21(pRetVal: Smallint);
    function Get_Dia22: Smallint;
    procedure Set_Dia22(pRetVal: Smallint);
    function Get_Dia23: Smallint;
    procedure Set_Dia23(pRetVal: Smallint);
    function Get_Dia24: Smallint;
    procedure Set_Dia24(pRetVal: Smallint);
    function Get_Dia25: Smallint;
    procedure Set_Dia25(pRetVal: Smallint);
    function Get_Dia26: Smallint;
    procedure Set_Dia26(pRetVal: Smallint);
    function Get_Dia27: Smallint;
    procedure Set_Dia27(pRetVal: Smallint);
    function Get_Dia28: Smallint;
    procedure Set_Dia28(pRetVal: Smallint);
    function Get_Dia29: Smallint;
    procedure Set_Dia29(pRetVal: Smallint);
    function Get_Dia30: Smallint;
    procedure Set_Dia30(pRetVal: Smallint);
    function Get_Dia31: Smallint;
    procedure Set_Dia31(pRetVal: Smallint);
    function Get_Holiday: Smallint;
    procedure Set_Holiday(pRetVal: Smallint);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _MonthlyJourneyWorking);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _MonthlyJourneyWorking read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property Id: Smallint read Get_Id write Set_Id;
    property TypeWorking: TypeWorking read Get_TypeWorking write Set_TypeWorking;
    property Dia01: Smallint read Get_Dia01 write Set_Dia01;
    property Dia02: Smallint read Get_Dia02 write Set_Dia02;
    property Dia03: Smallint read Get_Dia03 write Set_Dia03;
    property Dia04: Smallint read Get_Dia04 write Set_Dia04;
    property Dia05: Smallint read Get_Dia05 write Set_Dia05;
    property Dia06: Smallint read Get_Dia06 write Set_Dia06;
    property Dia07: Smallint read Get_Dia07 write Set_Dia07;
    property Dia08: Smallint read Get_Dia08 write Set_Dia08;
    property Dia09: Smallint read Get_Dia09 write Set_Dia09;
    property Dia10: Smallint read Get_Dia10 write Set_Dia10;
    property Dia11: Smallint read Get_Dia11 write Set_Dia11;
    property Dia12: Smallint read Get_Dia12 write Set_Dia12;
    property Dia13: Smallint read Get_Dia13 write Set_Dia13;
    property Dia14: Smallint read Get_Dia14 write Set_Dia14;
    property Dia15: Smallint read Get_Dia15 write Set_Dia15;
    property Dia16: Smallint read Get_Dia16 write Set_Dia16;
    property Dia17: Smallint read Get_Dia17 write Set_Dia17;
    property Dia18: Smallint read Get_Dia18 write Set_Dia18;
    property Dia19: Smallint read Get_Dia19 write Set_Dia19;
    property Dia20: Smallint read Get_Dia20 write Set_Dia20;
    property Dia21: Smallint read Get_Dia21 write Set_Dia21;
    property Dia22: Smallint read Get_Dia22 write Set_Dia22;
    property Dia23: Smallint read Get_Dia23 write Set_Dia23;
    property Dia24: Smallint read Get_Dia24 write Set_Dia24;
    property Dia25: Smallint read Get_Dia25 write Set_Dia25;
    property Dia26: Smallint read Get_Dia26 write Set_Dia26;
    property Dia27: Smallint read Get_Dia27 write Set_Dia27;
    property Dia28: Smallint read Get_Dia28 write Set_Dia28;
    property Dia29: Smallint read Get_Dia29 write Set_Dia29;
    property Dia30: Smallint read Get_Dia30 write Set_Dia30;
    property Dia31: Smallint read Get_Dia31 write Set_Dia31;
    property Holiday: Smallint read Get_Holiday write Set_Holiday;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TMonthlyJourneyWorkingProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TMonthlyJourneyWorking
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TMonthlyJourneyWorkingProperties = class(TPersistent)
  private
    FServer:    TMonthlyJourneyWorking;
    function    GetDefaultInterface: _MonthlyJourneyWorking;
    constructor Create(AServer: TMonthlyJourneyWorking);
  protected
    function Get_ToString: WideString;
    procedure Set_Id(pRetVal: Smallint);
    function Get_Id: Smallint;
    procedure Set_TypeWorking(pRetVal: TypeWorking);
    function Get_TypeWorking: TypeWorking;
    function Get_Dia01: Smallint;
    procedure Set_Dia01(pRetVal: Smallint);
    function Get_Dia02: Smallint;
    procedure Set_Dia02(pRetVal: Smallint);
    function Get_Dia03: Smallint;
    procedure Set_Dia03(pRetVal: Smallint);
    function Get_Dia04: Smallint;
    procedure Set_Dia04(pRetVal: Smallint);
    function Get_Dia05: Smallint;
    procedure Set_Dia05(pRetVal: Smallint);
    function Get_Dia06: Smallint;
    procedure Set_Dia06(pRetVal: Smallint);
    function Get_Dia07: Smallint;
    procedure Set_Dia07(pRetVal: Smallint);
    function Get_Dia08: Smallint;
    procedure Set_Dia08(pRetVal: Smallint);
    function Get_Dia09: Smallint;
    procedure Set_Dia09(pRetVal: Smallint);
    function Get_Dia10: Smallint;
    procedure Set_Dia10(pRetVal: Smallint);
    function Get_Dia11: Smallint;
    procedure Set_Dia11(pRetVal: Smallint);
    function Get_Dia12: Smallint;
    procedure Set_Dia12(pRetVal: Smallint);
    function Get_Dia13: Smallint;
    procedure Set_Dia13(pRetVal: Smallint);
    function Get_Dia14: Smallint;
    procedure Set_Dia14(pRetVal: Smallint);
    function Get_Dia15: Smallint;
    procedure Set_Dia15(pRetVal: Smallint);
    function Get_Dia16: Smallint;
    procedure Set_Dia16(pRetVal: Smallint);
    function Get_Dia17: Smallint;
    procedure Set_Dia17(pRetVal: Smallint);
    function Get_Dia18: Smallint;
    procedure Set_Dia18(pRetVal: Smallint);
    function Get_Dia19: Smallint;
    procedure Set_Dia19(pRetVal: Smallint);
    function Get_Dia20: Smallint;
    procedure Set_Dia20(pRetVal: Smallint);
    function Get_Dia21: Smallint;
    procedure Set_Dia21(pRetVal: Smallint);
    function Get_Dia22: Smallint;
    procedure Set_Dia22(pRetVal: Smallint);
    function Get_Dia23: Smallint;
    procedure Set_Dia23(pRetVal: Smallint);
    function Get_Dia24: Smallint;
    procedure Set_Dia24(pRetVal: Smallint);
    function Get_Dia25: Smallint;
    procedure Set_Dia25(pRetVal: Smallint);
    function Get_Dia26: Smallint;
    procedure Set_Dia26(pRetVal: Smallint);
    function Get_Dia27: Smallint;
    procedure Set_Dia27(pRetVal: Smallint);
    function Get_Dia28: Smallint;
    procedure Set_Dia28(pRetVal: Smallint);
    function Get_Dia29: Smallint;
    procedure Set_Dia29(pRetVal: Smallint);
    function Get_Dia30: Smallint;
    procedure Set_Dia30(pRetVal: Smallint);
    function Get_Dia31: Smallint;
    procedure Set_Dia31(pRetVal: Smallint);
    function Get_Holiday: Smallint;
    procedure Set_Holiday(pRetVal: Smallint);
  public
    property DefaultInterface: _MonthlyJourneyWorking read GetDefaultInterface;
  published
    property Id: Smallint read Get_Id write Set_Id;
    property TypeWorking: TypeWorking read Get_TypeWorking write Set_TypeWorking;
    property Dia01: Smallint read Get_Dia01 write Set_Dia01;
    property Dia02: Smallint read Get_Dia02 write Set_Dia02;
    property Dia03: Smallint read Get_Dia03 write Set_Dia03;
    property Dia04: Smallint read Get_Dia04 write Set_Dia04;
    property Dia05: Smallint read Get_Dia05 write Set_Dia05;
    property Dia06: Smallint read Get_Dia06 write Set_Dia06;
    property Dia07: Smallint read Get_Dia07 write Set_Dia07;
    property Dia08: Smallint read Get_Dia08 write Set_Dia08;
    property Dia09: Smallint read Get_Dia09 write Set_Dia09;
    property Dia10: Smallint read Get_Dia10 write Set_Dia10;
    property Dia11: Smallint read Get_Dia11 write Set_Dia11;
    property Dia12: Smallint read Get_Dia12 write Set_Dia12;
    property Dia13: Smallint read Get_Dia13 write Set_Dia13;
    property Dia14: Smallint read Get_Dia14 write Set_Dia14;
    property Dia15: Smallint read Get_Dia15 write Set_Dia15;
    property Dia16: Smallint read Get_Dia16 write Set_Dia16;
    property Dia17: Smallint read Get_Dia17 write Set_Dia17;
    property Dia18: Smallint read Get_Dia18 write Set_Dia18;
    property Dia19: Smallint read Get_Dia19 write Set_Dia19;
    property Dia20: Smallint read Get_Dia20 write Set_Dia20;
    property Dia21: Smallint read Get_Dia21 write Set_Dia21;
    property Dia22: Smallint read Get_Dia22 write Set_Dia22;
    property Dia23: Smallint read Get_Dia23 write Set_Dia23;
    property Dia24: Smallint read Get_Dia24 write Set_Dia24;
    property Dia25: Smallint read Get_Dia25 write Set_Dia25;
    property Dia26: Smallint read Get_Dia26 write Set_Dia26;
    property Dia27: Smallint read Get_Dia27 write Set_Dia27;
    property Dia28: Smallint read Get_Dia28 write Set_Dia28;
    property Dia29: Smallint read Get_Dia29 write Set_Dia29;
    property Dia30: Smallint read Get_Dia30 write Set_Dia30;
    property Dia31: Smallint read Get_Dia31 write Set_Dia31;
    property Holiday: Smallint read Get_Holiday write Set_Holiday;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoAlarmRingsCollection provides a Create and CreateRemote method to          
// create instances of the default interface _AlarmRingsCollection exposed by              
// the CoClass AlarmRingsCollection. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoAlarmRingsCollection = class
    class function Create: _AlarmRingsCollection;
    class function CreateRemote(const MachineName: string): _AlarmRingsCollection;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TAlarmRingsCollection
// Help String      : 
// Default Interface: _AlarmRingsCollection
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TAlarmRingsCollectionProperties= class;
{$ENDIF}
  TAlarmRingsCollection = class(TOleServer)
  private
    FIntf:        _AlarmRingsCollection;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TAlarmRingsCollectionProperties;
    function      GetServerProperties: TAlarmRingsCollectionProperties;
{$ENDIF}
    function      GetDefaultInterface: _AlarmRingsCollection;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    procedure Set_ModeAlarm(pRetVal: Mode);
    function Get_ModeAlarm: Mode;
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _AlarmRingsCollection);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    procedure Add(const AlarmRings: _AlarmRings);
    function Count: Integer;
    procedure Remove(const AlarmRings: _AlarmRings);
    procedure Clear;
    property DefaultInterface: _AlarmRingsCollection read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property ModeAlarm: Mode read Get_ModeAlarm write Set_ModeAlarm;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TAlarmRingsCollectionProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TAlarmRingsCollection
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TAlarmRingsCollectionProperties = class(TPersistent)
  private
    FServer:    TAlarmRingsCollection;
    function    GetDefaultInterface: _AlarmRingsCollection;
    constructor Create(AServer: TAlarmRingsCollection);
  protected
    function Get_ToString: WideString;
    procedure Set_ModeAlarm(pRetVal: Mode);
    function Get_ModeAlarm: Mode;
  public
    property DefaultInterface: _AlarmRingsCollection read GetDefaultInterface;
  published
    property ModeAlarm: Mode read Get_ModeAlarm write Set_ModeAlarm;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoSerialComm provides a Create and CreateRemote method to          
// create instances of the default interface _SerialComm exposed by              
// the CoClass SerialComm. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoSerialComm = class
    class function Create: _SerialComm;
    class function CreateRemote(const MachineName: string): _SerialComm;
  end;

// *********************************************************************//
// The Class CoCard provides a Create and CreateRemote method to          
// create instances of the default interface _Card exposed by              
// the CoClass Card. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoCard = class
    class function Create: _Card;
    class function CreateRemote(const MachineName: string): _Card;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TCard
// Help String      : 
// Default Interface: _Card
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TCardProperties= class;
{$ENDIF}
  TCard = class(TOleServer)
  private
    FIntf:        _Card;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TCardProperties;
    function      GetServerProperties: TCardProperties;
{$ENDIF}
    function      GetDefaultInterface: _Card;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_code: WideString;
    procedure Set_code(const pRetVal: WideString);
    function Get_Dummy: Byte;
    procedure Set_Dummy(pRetVal: Byte);
    function Get_message: Byte;
    procedure Set_message(pRetVal: Byte);
    function Get_way: Byte;
    procedure Set_way(pRetVal: Byte);
    function Get_Jornada: Smallint;
    procedure Set_Jornada(pRetVal: Smallint);
    function Get_CounterAccess: TypeCounter_Access;
    procedure Set_CounterAccess(pRetVal: TypeCounter_Access);
    function Get_password: Integer;
    procedure Set_password(pRetVal: Integer);
    function Get_Template: _TemplateCollection;
    procedure _Set_Template(const pRetVal: _TemplateCollection);
    function Get_isMaster: WordBool;
    procedure Set_isMaster(pRetVal: WordBool);
    function Get_name: WideString;
    procedure Set_name(const pRetVal: WideString);
    function Get_MasterCard: TypeMasterCard;
    procedure Set_MasterCard(pRetVal: TypeMasterCard);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _Card);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _Card read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property Template: _TemplateCollection read Get_Template write _Set_Template;
    property code: WideString read Get_code write Set_code;
    property Dummy: Byte read Get_Dummy write Set_Dummy;
    property message: Byte read Get_message write Set_message;
    property way: Byte read Get_way write Set_way;
    property Jornada: Smallint read Get_Jornada write Set_Jornada;
    property CounterAccess: TypeCounter_Access read Get_CounterAccess write Set_CounterAccess;
    property password: Integer read Get_password write Set_password;
    property isMaster: WordBool read Get_isMaster write Set_isMaster;
    property name: WideString read Get_name write Set_name;
    property MasterCard: TypeMasterCard read Get_MasterCard write Set_MasterCard;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TCardProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TCard
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TCardProperties = class(TPersistent)
  private
    FServer:    TCard;
    function    GetDefaultInterface: _Card;
    constructor Create(AServer: TCard);
  protected
    function Get_ToString: WideString;
    function Get_code: WideString;
    procedure Set_code(const pRetVal: WideString);
    function Get_Dummy: Byte;
    procedure Set_Dummy(pRetVal: Byte);
    function Get_message: Byte;
    procedure Set_message(pRetVal: Byte);
    function Get_way: Byte;
    procedure Set_way(pRetVal: Byte);
    function Get_Jornada: Smallint;
    procedure Set_Jornada(pRetVal: Smallint);
    function Get_CounterAccess: TypeCounter_Access;
    procedure Set_CounterAccess(pRetVal: TypeCounter_Access);
    function Get_password: Integer;
    procedure Set_password(pRetVal: Integer);
    function Get_Template: _TemplateCollection;
    procedure _Set_Template(const pRetVal: _TemplateCollection);
    function Get_isMaster: WordBool;
    procedure Set_isMaster(pRetVal: WordBool);
    function Get_name: WideString;
    procedure Set_name(const pRetVal: WideString);
    function Get_MasterCard: TypeMasterCard;
    procedure Set_MasterCard(pRetVal: TypeMasterCard);
  public
    property DefaultInterface: _Card read GetDefaultInterface;
  published
    property code: WideString read Get_code write Set_code;
    property Dummy: Byte read Get_Dummy write Set_Dummy;
    property message: Byte read Get_message write Set_message;
    property way: Byte read Get_way write Set_way;
    property Jornada: Smallint read Get_Jornada write Set_Jornada;
    property CounterAccess: TypeCounter_Access read Get_CounterAccess write Set_CounterAccess;
    property password: Integer read Get_password write Set_password;
    property isMaster: WordBool read Get_isMaster write Set_isMaster;
    property name: WideString read Get_name write Set_name;
    property MasterCard: TypeMasterCard read Get_MasterCard write Set_MasterCard;
  end;
{$ENDIF}


// *********************************************************************//
// The Class CoBioPointFingerPrintMessage provides a Create and CreateRemote method to          
// create instances of the default interface _BioPointFingerPrintMessage exposed by              
// the CoClass BioPointFingerPrintMessage. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoBioPointFingerPrintMessage = class
    class function Create: _BioPointFingerPrintMessage;
    class function CreateRemote(const MachineName: string): _BioPointFingerPrintMessage;
  end;

// *********************************************************************//
// The Class CoFaceLogRecord provides a Create and CreateRemote method to          
// create instances of the default interface _FaceLogRecord exposed by              
// the CoClass FaceLogRecord. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoFaceLogRecord = class
    class function Create: _FaceLogRecord;
    class function CreateRemote(const MachineName: string): _FaceLogRecord;
  end;


// *********************************************************************//
// OLE Server Proxy class declaration
// Server Object    : TFaceLogRecord
// Help String      : 
// Default Interface: _FaceLogRecord
// Def. Intf. DISP? : No
// Event   Interface: 
// TypeFlags        : (2) CanCreate
// *********************************************************************//
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  TFaceLogRecordProperties= class;
{$ENDIF}
  TFaceLogRecord = class(TOleServer)
  private
    FIntf:        _FaceLogRecord;
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    FProps:       TFaceLogRecordProperties;
    function      GetServerProperties: TFaceLogRecordProperties;
{$ENDIF}
    function      GetDefaultInterface: _FaceLogRecord;
  protected
    procedure InitServerData; override;
    function Get_ToString: WideString;
    function Get_employeeID: WideString;
    procedure Set_employeeID(const pRetVal: WideString);
    function Get_DateTimeMarkingPoint: TDateTime;
    procedure Set_DateTimeMarkingPoint(pRetVal: TDateTime);
  public
    constructor Create(AOwner: TComponent); override;
    destructor  Destroy; override;
    procedure Connect; override;
    procedure ConnectTo(svrIntf: _FaceLogRecord);
    procedure Disconnect; override;
    function Equals(obj: OleVariant): WordBool;
    function GetHashCode: Integer;
    function GetType: _Type;
    property DefaultInterface: _FaceLogRecord read GetDefaultInterface;
    property ToString: WideString read Get_ToString;
    property employeeID: WideString read Get_employeeID write Set_employeeID;
    property DateTimeMarkingPoint: TDateTime read Get_DateTimeMarkingPoint write Set_DateTimeMarkingPoint;
  published
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
    property Server: TFaceLogRecordProperties read GetServerProperties;
{$ENDIF}
  end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
// *********************************************************************//
// OLE Server Properties Proxy Class
// Server Object    : TFaceLogRecord
// (This object is used by the IDE's Property Inspector to allow editing
//  of the properties of this server)
// *********************************************************************//
 TFaceLogRecordProperties = class(TPersistent)
  private
    FServer:    TFaceLogRecord;
    function    GetDefaultInterface: _FaceLogRecord;
    constructor Create(AServer: TFaceLogRecord);
  protected
    function Get_ToString: WideString;
    function Get_employeeID: WideString;
    procedure Set_employeeID(const pRetVal: WideString);
    function Get_DateTimeMarkingPoint: TDateTime;
    procedure Set_DateTimeMarkingPoint(pRetVal: TDateTime);
  public
    property DefaultInterface: _FaceLogRecord read GetDefaultInterface;
  published
    property employeeID: WideString read Get_employeeID write Set_employeeID;
    property DateTimeMarkingPoint: TDateTime read Get_DateTimeMarkingPoint write Set_DateTimeMarkingPoint;
  end;
{$ENDIF}


procedure Register;

resourcestring
  dtlServerPage = 'ActiveX';

  dtlOcxPage = 'ActiveX';

implementation

uses ComObj;

class function CoRelogio.Create: IRelogio;
begin
  Result := CreateComObject(CLASS_Relogio) as IRelogio;
end;

class function CoRelogio.CreateRemote(const MachineName: string): IRelogio;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Relogio) as IRelogio;
end;

procedure TRelogio.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{9E5E5FB2-219D-4EE7-AB27-E4DBED8E123E}';
    IntfIID:   '{7BD20046-DF8C-44A6-8F6B-687FAA26FA71}';
    EventIID:  '{7BD20046-DF8C-44A6-8F6B-687FAA26FA71}';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TRelogio.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    ConnectEvents(punk);
    Fintf:= punk as IRelogio;
  end;
end;

procedure TRelogio.ConnectTo(svrIntf: IRelogio);
begin
  Disconnect;
  FIntf := svrIntf;
  ConnectEvents(FIntf);
end;

procedure TRelogio.DisConnect;
begin
  if Fintf <> nil then
  begin
    DisconnectEvents(FIntf);
    FIntf := nil;
  end;
end;

function TRelogio.GetDefaultInterface: IRelogio;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TRelogio.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TRelogioProperties.Create(Self);
{$ENDIF}
end;

destructor TRelogio.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TRelogio.GetServerProperties: TRelogioProperties;
begin
  Result := FProps;
end;
{$ENDIF}

procedure TRelogio.InvokeEvent(DispID: TDispID; var Params: TVariantArray);
begin
  case DispID of
    -1: Exit;  // DISPID_UNKNOWN
    1: if Assigned(FOnsetListaBrancaPorIP) then
         FOnsetListaBrancaPorIP(Self,
                                Params[0] {const WideString},
                                Params[1] {const WideString},
                                Params[2] {const WideString},
                                Params[3] {const WideString});
    2: if Assigned(FOnsetListaBrancaPorDriver) then
         FOnsetListaBrancaPorDriver(Self,
                                    Params[0] {const WideString},
                                    Params[1] {const WideString},
                                    Params[2] {const WideString},
                                    Params[3] {const WideString});
    3: if Assigned(FOnsetListaNegraPorIP) then
         FOnsetListaNegraPorIP(Self,
                               Params[0] {const WideString},
                               Params[1] {const WideString},
                               Params[2] {const WideString},
                               Params[3] {const WideString});
    4: if Assigned(FOnsetListaNegraPorDriver) then
         FOnsetListaNegraPorDriver(Self,
                                   Params[0] {const WideString},
                                   Params[1] {const WideString},
                                   Params[2] {const WideString},
                                   Params[3] {const WideString});
    5: if Assigned(FOnsetDataHora) then
         FOnsetDataHora(Self,
                        Params[0] {const WideString},
                        Params[1] {const WideString},
                        Params[2] {const WideString});
    6: if Assigned(FOncoletaPorIP) then
         FOncoletaPorIP(Self,
                        Params[0] {const WideString},
                        Params[1] {const WideString},
                        Params[2] {const WideString},
                        Params[3] {const WideString});
    7: if Assigned(FOncoletaPorDriver) then
         FOncoletaPorDriver(Self,
                            Params[0] {const WideString},
                            Params[1] {const WideString},
                            Params[2] {const WideString},
                            Params[3] {const WideString});
  end; {case DispID}
end;

function TRelogio.setListaBrancaPorIP(const path: WideString; const name: WideString; 
                                      const ip: WideString; const Id: WideString): WordBool;
begin
  Result := DefaultInterface.setListaBrancaPorIP(path, name, ip, Id);
end;

function TRelogio.setListaBrancaPorDriver(const pathIn: WideString; const nameIn: WideString; 
                                          const pathOut: WideString; const nameOut: WideString): WordBool;
begin
  Result := DefaultInterface.setListaBrancaPorDriver(pathIn, nameIn, pathOut, nameOut);
end;

function TRelogio.setListaNegraPorIP(const path: WideString; const name: WideString; 
                                     const ip: WideString; const Id: WideString): WordBool;
begin
  Result := DefaultInterface.setListaNegraPorIP(path, name, ip, Id);
end;

function TRelogio.setListaNegraPorDriver(const pathIn: WideString; const nameIn: WideString; 
                                         const pathOut: WideString; const nameOut: WideString): WordBool;
begin
  Result := DefaultInterface.setListaNegraPorDriver(pathIn, nameIn, pathOut, nameOut);
end;

function TRelogio.setDataHora(const ip: WideString; const datahora: WideString; const Id: WideString): WordBool;
begin
  Result := DefaultInterface.setDataHora(ip, datahora, Id);
end;

function TRelogio.coletaPorIP(const ip: WideString; const path: WideString; const name: WideString; 
                              const Id: WideString): WordBool;
begin
  Result := DefaultInterface.coletaPorIP(ip, path, name, Id);
end;

function TRelogio.coletaPorDriver(const pathIn: WideString; const nameIn: WideString; 
                                  const pathOut: WideString; const nameOut: WideString): WordBool;
begin
  Result := DefaultInterface.coletaPorDriver(pathIn, nameIn, pathOut, nameOut);
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TRelogioProperties.Create(AServer: TRelogio);
begin
  inherited Create;
  FServer := AServer;
end;

function TRelogioProperties.GetDefaultInterface: IRelogio;
begin
  Result := FServer.DefaultInterface;
end;

{$ENDIF}

class function CoAbstractMessage.Create: _AbstractMessage;
begin
  Result := CreateComObject(CLASS_AbstractMessage) as _AbstractMessage;
end;

class function CoAbstractMessage.CreateRemote(const MachineName: string): _AbstractMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AbstractMessage) as _AbstractMessage;
end;

class function CoTCPComm.Create: _TCPComm;
begin
  Result := CreateComObject(CLASS_TCPComm) as _TCPComm;
end;

class function CoTCPComm.CreateRemote(const MachineName: string): _TCPComm;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_TCPComm) as _TCPComm;
end;

procedure TTCPComm.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{C6659361-1625-4746-931C-36014B146679}';
    IntfIID:   '{66C6B10E-8FA5-3B10-B01F-3F7BF9697FF7}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TTCPComm.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _TCPComm;
  end;
end;

procedure TTCPComm.ConnectTo(svrIntf: _TCPComm);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TTCPComm.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TTCPComm.GetDefaultInterface: _TCPComm;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TTCPComm.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TTCPCommProperties.Create(Self);
{$ENDIF}
end;

destructor TTCPComm.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TTCPComm.GetServerProperties: TTCPCommProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TTCPComm.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TTCPComm.Get_ZkemkeeperObject: IZKEM;
begin
    Result := DefaultInterface.ZkemkeeperObject;
end;

procedure TTCPComm._Set_ZkemkeeperObject(const pRetVal: IZKEM);
  { Warning: The property ZkemkeeperObject has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.ZkemkeeperObject := pRetVal;
end;

function TTCPComm.Get_Zkemkeeper: WordBool;
begin
    Result := DefaultInterface.Zkemkeeper;
end;

procedure TTCPComm.Set_Zkemkeeper(pRetVal: WordBool);
begin
  DefaultInterface.Set_Zkemkeeper(pRetVal);
end;

function TTCPComm.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TTCPComm.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TTCPComm.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

procedure TTCPComm.Open;
begin
  DefaultInterface.Open;
end;

procedure TTCPComm.Close;
begin
  DefaultInterface.Close;
end;

procedure TTCPComm.Send(data: PSafeArray);
begin
  DefaultInterface.Send(data);
end;

function TTCPComm.Receive(data: PSafeArray): Integer;
begin
  Result := DefaultInterface.Receive(data);
end;

procedure TTCPComm.SetTimeOut(timeOut: Integer);
begin
  DefaultInterface.SetTimeOut(timeOut);
end;

procedure TTCPComm.SetSendBufferSize(bufferSize: Int64);
begin
  DefaultInterface.SetSendBufferSize(bufferSize);
end;

function TTCPComm.GetTimeOut: LongWord;
begin
  Result := DefaultInterface.GetTimeOut;
end;

procedure TTCPComm.CreateTcpComm(const ip: WideString; port: Integer);
begin
  DefaultInterface.CreateTcpComm(ip, port);
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TTCPCommProperties.Create(AServer: TTCPComm);
begin
  inherited Create;
  FServer := AServer;
end;

function TTCPCommProperties.GetDefaultInterface: _TCPComm;
begin
  Result := FServer.DefaultInterface;
end;

function TTCPCommProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TTCPCommProperties.Get_ZkemkeeperObject: IZKEM;
begin
    Result := DefaultInterface.ZkemkeeperObject;
end;

procedure TTCPCommProperties._Set_ZkemkeeperObject(const pRetVal: IZKEM);
  { Warning: The property ZkemkeeperObject has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.ZkemkeeperObject := pRetVal;
end;

function TTCPCommProperties.Get_Zkemkeeper: WordBool;
begin
    Result := DefaultInterface.Zkemkeeper;
end;

procedure TTCPCommProperties.Set_Zkemkeeper(pRetVal: WordBool);
begin
  DefaultInterface.Set_Zkemkeeper(pRetVal);
end;

{$ENDIF}

class function CoAbstractProtocol.Create: _AbstractProtocol;
begin
  Result := CreateComObject(CLASS_AbstractProtocol) as _AbstractProtocol;
end;

class function CoAbstractProtocol.CreateRemote(const MachineName: string): _AbstractProtocol;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AbstractProtocol) as _AbstractProtocol;
end;

class function CoEmployeesListTransmissionProgress.Create: _EmployeesListTransmissionProgress;
begin
  Result := CreateComObject(CLASS_EmployeesListTransmissionProgress) as _EmployeesListTransmissionProgress;
end;

class function CoEmployeesListTransmissionProgress.CreateRemote(const MachineName: string): _EmployeesListTransmissionProgress;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_EmployeesListTransmissionProgress) as _EmployeesListTransmissionProgress;
end;

class function CoCredentialListTransmissionProgress.Create: _CredentialListTransmissionProgress;
begin
  Result := CreateComObject(CLASS_CredentialListTransmissionProgress) as _CredentialListTransmissionProgress;
end;

class function CoCredentialListTransmissionProgress.CreateRemote(const MachineName: string): _CredentialListTransmissionProgress;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_CredentialListTransmissionProgress) as _CredentialListTransmissionProgress;
end;

class function CoPrintPointInquirySerialNumberOfREPAndMemoryResponse.Create: _PrintPointInquirySerialNumberOfREPAndMemoryResponse;
begin
  Result := CreateComObject(CLASS_PrintPointInquirySerialNumberOfREPAndMemoryResponse) as _PrintPointInquirySerialNumberOfREPAndMemoryResponse;
end;

class function CoPrintPointInquirySerialNumberOfREPAndMemoryResponse.CreateRemote(const MachineName: string): _PrintPointInquirySerialNumberOfREPAndMemoryResponse;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_PrintPointInquirySerialNumberOfREPAndMemoryResponse) as _PrintPointInquirySerialNumberOfREPAndMemoryResponse;
end;

class function CoPrintPointLiFingerPrint.Create: _PrintPointLiFingerPrint;
begin
  Result := CreateComObject(CLASS_PrintPointLiFingerPrint) as _PrintPointLiFingerPrint;
end;

class function CoPrintPointLiFingerPrint.CreateRemote(const MachineName: string): _PrintPointLiFingerPrint;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_PrintPointLiFingerPrint) as _PrintPointLiFingerPrint;
end;

procedure TPrintPointLiFingerPrint.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{6DDF7E80-D7C4-4B36-8971-B6DAC6C6C507}';
    IntfIID:   '{A6902D61-90B9-391D-99C8-59CE92C509A5}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TPrintPointLiFingerPrint.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _PrintPointLiFingerPrint;
  end;
end;

procedure TPrintPointLiFingerPrint.ConnectTo(svrIntf: _PrintPointLiFingerPrint);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TPrintPointLiFingerPrint.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TPrintPointLiFingerPrint.GetDefaultInterface: _PrintPointLiFingerPrint;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TPrintPointLiFingerPrint.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TPrintPointLiFingerPrintProperties.Create(Self);
{$ENDIF}
end;

destructor TPrintPointLiFingerPrint.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TPrintPointLiFingerPrint.GetServerProperties: TPrintPointLiFingerPrintProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TPrintPointLiFingerPrint.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TPrintPointLiFingerPrint.Get_fingerPrintType: EFingerPrintType;
begin
    Result := DefaultInterface.fingerPrintType;
end;

procedure TPrintPointLiFingerPrint.Set_fingerPrintType(pRetVal: EFingerPrintType);
begin
  DefaultInterface.Set_fingerPrintType(pRetVal);
end;

function TPrintPointLiFingerPrint.Get_fingerPrint: WideString;
begin
    Result := DefaultInterface.fingerPrint;
end;

procedure TPrintPointLiFingerPrint.Set_fingerPrint(const pRetVal: WideString);
  { Warning: The property fingerPrint has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.fingerPrint := pRetVal;
end;

function TPrintPointLiFingerPrint.Get_employeeID: Integer;
begin
    Result := DefaultInterface.employeeID;
end;

procedure TPrintPointLiFingerPrint.Set_employeeID(pRetVal: Integer);
begin
  DefaultInterface.Set_employeeID(pRetVal);
end;

function TPrintPointLiFingerPrint.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TPrintPointLiFingerPrint.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TPrintPointLiFingerPrint.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TPrintPointLiFingerPrintProperties.Create(AServer: TPrintPointLiFingerPrint);
begin
  inherited Create;
  FServer := AServer;
end;

function TPrintPointLiFingerPrintProperties.GetDefaultInterface: _PrintPointLiFingerPrint;
begin
  Result := FServer.DefaultInterface;
end;

function TPrintPointLiFingerPrintProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TPrintPointLiFingerPrintProperties.Get_fingerPrintType: EFingerPrintType;
begin
    Result := DefaultInterface.fingerPrintType;
end;

procedure TPrintPointLiFingerPrintProperties.Set_fingerPrintType(pRetVal: EFingerPrintType);
begin
  DefaultInterface.Set_fingerPrintType(pRetVal);
end;

function TPrintPointLiFingerPrintProperties.Get_fingerPrint: WideString;
begin
    Result := DefaultInterface.fingerPrint;
end;

procedure TPrintPointLiFingerPrintProperties.Set_fingerPrint(const pRetVal: WideString);
  { Warning: The property fingerPrint has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.fingerPrint := pRetVal;
end;

function TPrintPointLiFingerPrintProperties.Get_employeeID: Integer;
begin
    Result := DefaultInterface.employeeID;
end;

procedure TPrintPointLiFingerPrintProperties.Set_employeeID(pRetVal: Integer);
begin
  DefaultInterface.Set_employeeID(pRetVal);
end;

{$ENDIF}

class function CoPrintPointEmployee.Create: _PrintPointEmployee;
begin
  Result := CreateComObject(CLASS_PrintPointEmployee) as _PrintPointEmployee;
end;

class function CoPrintPointEmployee.CreateRemote(const MachineName: string): _PrintPointEmployee;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_PrintPointEmployee) as _PrintPointEmployee;
end;

procedure TPrintPointEmployee.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{B58BFC15-715C-43F2-9549-9E6F80A6B019}';
    IntfIID:   '{ADFC1D78-5667-3A3A-8CE8-E3FAF826B02B}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TPrintPointEmployee.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _PrintPointEmployee;
  end;
end;

procedure TPrintPointEmployee.ConnectTo(svrIntf: _PrintPointEmployee);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TPrintPointEmployee.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TPrintPointEmployee.GetDefaultInterface: _PrintPointEmployee;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TPrintPointEmployee.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TPrintPointEmployeeProperties.Create(Self);
{$ENDIF}
end;

destructor TPrintPointEmployee.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TPrintPointEmployee.GetServerProperties: TPrintPointEmployeeProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TPrintPointEmployee.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TPrintPointEmployee.Get_pis: WideString;
begin
    Result := DefaultInterface.pis;
end;

procedure TPrintPointEmployee.Set_pis(const pRetVal: WideString);
  { Warning: The property pis has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.pis := pRetVal;
end;

function TPrintPointEmployee.Get_name: WideString;
begin
    Result := DefaultInterface.name;
end;

procedure TPrintPointEmployee.Set_name(const pRetVal: WideString);
  { Warning: The property name has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.name := pRetVal;
end;

function TPrintPointEmployee.Get_password: WideString;
begin
    Result := DefaultInterface.password;
end;

procedure TPrintPointEmployee.Set_password(const pRetVal: WideString);
  { Warning: The property password has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.password := pRetVal;
end;

function TPrintPointEmployee.Get_CostCenter: Integer;
begin
    Result := DefaultInterface.CostCenter;
end;

procedure TPrintPointEmployee.Set_CostCenter(pRetVal: Integer);
begin
  DefaultInterface.Set_CostCenter(pRetVal);
end;

function TPrintPointEmployee.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TPrintPointEmployee.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TPrintPointEmployee.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

function TPrintPointEmployee.CompareTo(obj: OleVariant): Integer;
begin
  Result := DefaultInterface.CompareTo(obj);
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TPrintPointEmployeeProperties.Create(AServer: TPrintPointEmployee);
begin
  inherited Create;
  FServer := AServer;
end;

function TPrintPointEmployeeProperties.GetDefaultInterface: _PrintPointEmployee;
begin
  Result := FServer.DefaultInterface;
end;

function TPrintPointEmployeeProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TPrintPointEmployeeProperties.Get_pis: WideString;
begin
    Result := DefaultInterface.pis;
end;

procedure TPrintPointEmployeeProperties.Set_pis(const pRetVal: WideString);
  { Warning: The property pis has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.pis := pRetVal;
end;

function TPrintPointEmployeeProperties.Get_name: WideString;
begin
    Result := DefaultInterface.name;
end;

procedure TPrintPointEmployeeProperties.Set_name(const pRetVal: WideString);
  { Warning: The property name has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.name := pRetVal;
end;

function TPrintPointEmployeeProperties.Get_password: WideString;
begin
    Result := DefaultInterface.password;
end;

procedure TPrintPointEmployeeProperties.Set_password(const pRetVal: WideString);
  { Warning: The property password has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.password := pRetVal;
end;

function TPrintPointEmployeeProperties.Get_CostCenter: Integer;
begin
    Result := DefaultInterface.CostCenter;
end;

procedure TPrintPointEmployeeProperties.Set_CostCenter(pRetVal: Integer);
begin
  DefaultInterface.Set_CostCenter(pRetVal);
end;

{$ENDIF}

class function CoDeviceConnectionException.Create: _DeviceConnectionException;
begin
  Result := CreateComObject(CLASS_DeviceConnectionException) as _DeviceConnectionException;
end;

class function CoDeviceConnectionException.CreateRemote(const MachineName: string): _DeviceConnectionException;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_DeviceConnectionException) as _DeviceConnectionException;
end;

class function CoWatchComm_.Create: _WatchComm;
begin
  Result := CreateComObject(CLASS_WatchComm_) as _WatchComm;
end;

class function CoWatchComm_.CreateRemote(const MachineName: string): _WatchComm;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_WatchComm_) as _WatchComm;
end;

procedure TWatchComm.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{36037306-8B1F-4F82-9868-1976E8EBA4F0}';
    IntfIID:   '{12335FED-346F-3140-B4BB-4651674481ED}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TWatchComm.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _WatchComm;
  end;
end;

procedure TWatchComm.ConnectTo(svrIntf: _WatchComm);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TWatchComm.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TWatchComm.GetDefaultInterface: _WatchComm;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TWatchComm.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TWatchCommProperties.Create(Self);
{$ENDIF}
end;

destructor TWatchComm.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TWatchComm.GetServerProperties: TWatchCommProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TWatchComm.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TWatchComm.Get_Connected: WordBool;
begin
    Result := DefaultInterface.Connected;
end;

function TWatchComm.Get_GeneralTimeout: Integer;
begin
    Result := DefaultInterface.GeneralTimeout;
end;

procedure TWatchComm.Set_GeneralTimeout1(pRetVal: Integer);
begin
  DefaultInterface.Set_GeneralTimeout1(pRetVal);
end;

function TWatchComm.Get_CollectTimeout: Integer;
begin
    Result := DefaultInterface.CollectTimeout;
end;

procedure TWatchComm.Set_CollectTimeout1(pRetVal: Integer);
begin
  DefaultInterface.Set_CollectTimeout1(pRetVal);
end;

function TWatchComm.Get_Protocol: _AbstractProtocol;
begin
    Result := DefaultInterface.Protocol;
end;

procedure TWatchComm._Set_Protocol1(const pRetVal: _AbstractProtocol);
  { Warning: The property Protocol1 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Protocol1 := pRetVal;
end;

function TWatchComm.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TWatchComm.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TWatchComm.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

procedure TWatchComm.AddConfiguration(configurationType: EConfigurationType; field1: OleVariant);
begin
  DefaultInterface.AddConfiguration(configurationType, field1);
end;

procedure TWatchComm.AddConfiguration_2(configurationType: EConfigurationType; field1: OleVariant; 
                                        field2: OleVariant);
begin
  DefaultInterface.AddConfiguration_2(configurationType, field1, field2);
end;

procedure TWatchComm.AddConfiguration_3(configurationType: EConfigurationType; field1: OleVariant; 
                                        field2: OleVariant; field3: OleVariant);
begin
  DefaultInterface.AddConfiguration_3(configurationType, field1, field2, field3);
end;

procedure TWatchComm.AddConfiguration_4(configurationType: EConfigurationType; field1: OleVariant; 
                                        field2: OleVariant; field3: OleVariant; field4: OleVariant);
begin
  DefaultInterface.AddConfiguration_4(configurationType, field1, field2, field3, field4);
end;

procedure TWatchComm.AddConfiguration_5(configurationType: EConfigurationType; field1: OleVariant; 
                                        field2: OleVariant; field3: OleVariant; field4: OleVariant; 
                                        field5: OleVariant);
begin
  DefaultInterface.AddConfiguration_5(configurationType, field1, field2, field3, field4, field5);
end;

procedure TWatchComm.AddParcialConfiguration(ParcialConfigurationType: ParcialConfigurationType; 
                                             field1: OleVariant);
begin
  DefaultInterface.AddParcialConfiguration(ParcialConfigurationType, field1);
end;

procedure TWatchComm.AddParcialConfiguration_2(ParcialConfigurationType: ParcialConfigurationType; 
                                               field1: OleVariant; field2: OleVariant);
begin
  DefaultInterface.AddParcialConfiguration_2(ParcialConfigurationType, field1, field2);
end;

procedure TWatchComm.AddMaster(const pis: WideString; const Card: WideString; 
                               const password: WideString; 
                               hasTechniquesProgrammingPermission: WordBool; 
                               hasDataAndTimePermission: WordBool; 
                               hasPenDriveProgrammingPermisision: WordBool);
begin
  DefaultInterface.AddMaster(pis, Card, password, hasTechniquesProgrammingPermission, 
                             hasDataAndTimePermission, hasPenDriveProgrammingPermisision);
end;

procedure TWatchComm.AddMaster_2(const pis: WideString; const Card: WideString; 
                                 const password: WideString; 
                                 hasTechniquesProgrammingPermission: WordBool; 
                                 hasDataAndTimePermission: WordBool; 
                                 hasPenDriveProgrammingPermisision: WordBool; 
                                 hasBobbinChangePermisision: WordBool);
begin
  DefaultInterface.AddMaster_2(pis, Card, password, hasTechniquesProgrammingPermission, 
                               hasDataAndTimePermission, hasPenDriveProgrammingPermisision, 
                               hasBobbinChangePermisision);
end;

procedure TWatchComm.AddCredential(const cardCode: WideString; const pis: WideString; version: Byte);
begin
  DefaultInterface.AddCredential(cardCode, pis, version);
end;

procedure TWatchComm.AddEmployee(const pis: WideString);
begin
  DefaultInterface.AddEmployee(pis);
end;

procedure TWatchComm.AddEmployee_2(const pis: WideString; const name: WideString; 
                                   const password: WideString);
begin
  DefaultInterface.AddEmployee_2(pis, name, password);
end;

procedure TWatchComm.AddEmployee_3(const pis: WideString; const name: WideString; 
                                   const password: WideString; CostCenter: Integer);
begin
  DefaultInterface.AddEmployee_3(pis, name, password, CostCenter);
end;

procedure TWatchComm.AddEmployee_4(Id: Integer);
begin
  DefaultInterface.AddEmployee_4(Id);
end;

procedure TWatchComm.AddEmployee_5(employeeID: Integer; const pis: WideString; 
                                   const cpf: WideString; const Credential: WideString; 
                                   const name: WideString; const password: WideString; 
                                   isMaster: WordBool);
begin
  DefaultInterface.AddEmployee_5(employeeID, pis, cpf, Credential, name, password, isMaster);
end;

procedure TWatchComm.AddEmployee_6(employeeID: Integer; const Credential: WideString; 
                                   const name: WideString; const password: WideString; 
                                   isMaster: WordBool);
begin
  DefaultInterface.AddEmployee_6(employeeID, Credential, name, password, isMaster);
end;

procedure TWatchComm.CreateWatchComm(protocolType: WatchProtocolType; const Comm: IComm; 
                                     WatchAddress: Integer; const firmwareVersion: WideString);
begin
  DefaultInterface.CreateWatchComm(protocolType, Comm, WatchAddress, firmwareVersion);
end;

procedure TWatchComm.CreateWatchComm_2(protocolType: WatchProtocolType; const Comm: IComm; 
                                       WatchAddress: Integer; connectionType: WatchConnectionType; 
                                       const firmwareVersion: WideString);
begin
  DefaultInterface.CreateWatchComm_2(protocolType, Comm, WatchAddress, connectionType, 
                                     firmwareVersion);
end;

procedure TWatchComm.CreateWatchComm_3(protocolType: WatchProtocolType; const Comm: IComm; 
                                       WatchAddress: Integer; const accessKey: WideString; 
                                       const firmwareVersion: WideString);
begin
  DefaultInterface.CreateWatchComm_3(protocolType, Comm, WatchAddress, accessKey, firmwareVersion);
end;

procedure TWatchComm.CreateWatchComm_4(protocolType: WatchProtocolType; const Comm: IComm; 
                                       WatchAddress: Integer; const accessKey: WideString; 
                                       connectionType: WatchConnectionType; 
                                       const firmwareVersion: WideString);
begin
  DefaultInterface.CreateWatchComm_4(protocolType, Comm, WatchAddress, accessKey, connectionType, 
                                     firmwareVersion);
end;

procedure TWatchComm.OpenConnection;
begin
  DefaultInterface.OpenConnection;
end;

procedure TWatchComm.CloseConnection;
begin
  DefaultInterface.CloseConnection;
end;

function TWatchComm.GetStatus: _AbstractStatusMessage;
begin
  Result := DefaultInterface.GetStatus;
end;

function TWatchComm.GetMAC: _GetMACResponse;
begin
  Result := DefaultInterface.GetMAC;
end;

function TWatchComm.GetImmediateStatus: _ImmediateStatusResponse;
begin
  Result := DefaultInterface.GetImmediateStatus;
end;

function TWatchComm.GetPrintPointStatus: _PrintPointStatusMessage;
begin
  Result := DefaultInterface.GetPrintPointStatus;
end;

function TWatchComm.GetPrintPointLiStatus: _PrintPointLiStatus;
begin
  Result := DefaultInterface.GetPrintPointLiStatus;
end;

function TWatchComm.GetFaceStatus: _FaceStatus;
begin
  Result := DefaultInterface.GetFaceStatus;
end;

function TWatchComm.CollectAll: _ArrayList;
begin
  Result := DefaultInterface.CollectAll;
end;

function TWatchComm.GetCurrentPunch: _AbstractMessage;
begin
  Result := DefaultInterface.GetCurrentPunch;
end;

function TWatchComm.RemoveCurrentPunch: _AbstractMessage;
begin
  Result := DefaultInterface.RemoveCurrentPunch;
end;

procedure TWatchComm.SetDST(dstBegin: TDateTime; dstEnd: TDateTime);
begin
  DefaultInterface.SetDST(dstBegin, dstEnd);
end;

procedure TWatchComm.RemoveDST;
begin
  DefaultInterface.RemoveDST;
end;

procedure TWatchComm.Set1ToN;
begin
  DefaultInterface.Set1ToN;
end;

procedure TWatchComm.Set1To1;
begin
  DefaultInterface.Set1To1;
end;

procedure TWatchComm.SetDateTimeAndDST_2(date: TDateTime; dstBegin: TDateTime; dstEnd: TDateTime);
begin
  DefaultInterface.SetDateTimeAndDST_2(date, dstBegin, dstEnd);
end;

procedure TWatchComm.SetDateTime(date: TDateTime);
begin
  DefaultInterface.SetDateTime(date);
end;

procedure TWatchComm.EmployeesTotalProgrammingBegin;
begin
  DefaultInterface.EmployeesTotalProgrammingBegin;
end;

procedure TWatchComm.EmployeesTotalProgrammingEnd;
begin
  DefaultInterface.EmployeesTotalProgrammingEnd;
end;

procedure TWatchComm.ClearMasterList;
begin
  DefaultInterface.ClearMasterList;
end;

procedure TWatchComm.ClearClockCredentialsList;
begin
  DefaultInterface.ClearClockCredentialsList;
end;

procedure TWatchComm.ClearDisplayMessage;
begin
  DefaultInterface.ClearDisplayMessage;
end;

procedure TWatchComm.ClearCostCenterList;
begin
  DefaultInterface.ClearCostCenterList;
end;

procedure TWatchComm.EnableLogDeniedAccess(enable: WordBool);
begin
  DefaultInterface.EnableLogDeniedAccess(enable);
end;

procedure TWatchComm.ProgramLotterySampleRate(rate: Byte; inout: Byte);
begin
  DefaultInterface.ProgramLotterySampleRate(rate, inout);
end;

procedure TWatchComm.ProgramTriggerType(type_: Byte; time: Integer);
begin
  DefaultInterface.ProgramTriggerType(type_, time);
end;

function TWatchComm.HardwareTestCollection: PSafeArray;
begin
  Result := DefaultInterface.HardwareTestCollection;
end;

procedure TWatchComm.ConfigureCard(idLength: Integer; hasChecking: WordBool);
begin
  DefaultInterface.ConfigureCard(idLength, hasChecking);
end;

procedure TWatchComm.ConfigureCard_2(idLenghtMinimum: Integer; idLenghtMaximum: Integer; 
                                     hasChecking: WordBool; way: WordBool);
begin
  DefaultInterface.ConfigureCard_2(idLenghtMinimum, idLenghtMaximum, hasChecking, way);
end;

procedure TWatchComm.Activation(active: WordBool; controlled: WordBool);
begin
  DefaultInterface.Activation(active, controlled);
end;

procedure TWatchComm.setCardList(const CardCollection: _CardCollection);
begin
  DefaultInterface.setCardList(CardCollection);
end;

procedure TWatchComm.ConfigureMessage(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                                      const Description: WideString);
begin
  DefaultInterface.ConfigureMessage(MessageType, MessageCode, Description);
end;

procedure TWatchComm.ConfigureMessage_2(MessageType: TypeMessageConfigurantion; MessageCode: Byte; 
                                        const Description: WideString; 
                                        typeFunction: TypeActionFunction);
begin
  DefaultInterface.ConfigureMessage_2(MessageType, MessageCode, Description, typeFunction);
end;

procedure TWatchComm.Master(Id: Integer; password: Integer; PenDriveProgramming: WordBool; 
                            DateTimeUpdated: WordBool; ProgrammingTechnical: WordBool);
begin
  DefaultInterface.Master(Id, password, PenDriveProgramming, DateTimeUpdated, ProgrammingTechnical);
end;

procedure TWatchComm.ClearProgramming(ClearProgramming: TypeClearProgramming);
begin
  DefaultInterface.ClearProgramming(ClearProgramming);
end;

procedure TWatchComm.RepositioningMRPRecordsPointer;
begin
  DefaultInterface.RepositioningMRPRecordsPointer;
end;

procedure TWatchComm.REPPlacesInMaintenance;
begin
  DefaultInterface.REPPlacesInMaintenance;
end;

procedure TWatchComm.CleanEssentialVariables;
begin
  DefaultInterface.CleanEssentialVariables;
end;

procedure TWatchComm.RebuildEmployeesTable;
begin
  DefaultInterface.RebuildEmployeesTable;
end;

procedure TWatchComm.IncludeFingerPrint(const pis: WideString; const fingerPrint: WideString; 
                                        fingerPrintTypeOne: EFingerPrintType; 
                                        fingerPrintTypeTwo: EFingerPrintType; 
                                        sensor: EfingerPrintSensor);
begin
  DefaultInterface.IncludeFingerPrint(pis, fingerPrint, fingerPrintTypeOne, fingerPrintTypeTwo, 
                                      sensor);
end;

procedure TWatchComm.IncludeFingerPrint_2(const pis: WideString; const fingerPrint: WideString; 
                                          fingerPrintHandOne: EFingerPrintHand; 
                                          fingerPrintHandTwo: EFingerPrintHand);
begin
  DefaultInterface.IncludeFingerPrint_2(pis, fingerPrint, fingerPrintHandOne, fingerPrintHandTwo);
end;

procedure TWatchComm.IncludeFingerPrint_3(employeeID: Integer; fingerPrintType: EFingerPrintType; 
                                          const fingerPrint: WideString);
begin
  DefaultInterface.IncludeFingerPrint_3(employeeID, fingerPrintType, fingerPrint);
end;

procedure TWatchComm.IncludeCostCenter(costCenterID: Integer; 
                                       const costCenterDescription: WideString);
begin
  DefaultInterface.IncludeCostCenter(costCenterID, costCenterDescription);
end;

procedure TWatchComm.IncludeCredentialList(usesVersion: WordBool);
begin
  DefaultInterface.IncludeCredentialList(usesVersion);
end;

procedure TWatchComm.IncludeEmployeesList(usesPassword: WordBool; isTotalProgramming: WordBool);
begin
  DefaultInterface.IncludeEmployeesList(usesPassword, isTotalProgramming);
end;

procedure TWatchComm.IncludeEmployeesListWithCostCenter;
begin
  DefaultInterface.IncludeEmployeesListWithCostCenter;
end;

procedure TWatchComm.IncludeEmployeesList_2;
begin
  DefaultInterface.IncludeEmployeesList_2;
end;

procedure TWatchComm.SendDisplayMessage(code: Smallint; const message: WideString);
begin
  DefaultInterface.SendDisplayMessage(code, message);
end;

procedure TWatchComm.SendSettings;
begin
  DefaultInterface.SendSettings;
end;

procedure TWatchComm.SendParcialSettings;
begin
  DefaultInterface.SendParcialSettings;
end;

procedure TWatchComm.SendMasterList;
begin
  DefaultInterface.SendMasterList;
end;

procedure TWatchComm.ChangeEmployer(employerType: EmployeerType; const cpf_cnpj: WideString; 
                                    const cei: WideString; const name: WideString; 
                                    const address: WideString);
begin
  DefaultInterface.ChangeEmployer(employerType, cpf_cnpj, cei, name, address);
end;

procedure TWatchComm.ActivateBootLoader;
begin
  DefaultInterface.ActivateBootLoader;
end;

procedure TWatchComm.FirstActivation(const serialNumber: WideString; const password: WideString);
begin
  DefaultInterface.FirstActivation(serialNumber, password);
end;

procedure TWatchComm.Activation_2(const password: WideString);
begin
  DefaultInterface.Activation_2(password);
end;

procedure TWatchComm.Activation_3;
begin
  DefaultInterface.Activation_3;
end;

procedure TWatchComm.ExchangeSealREP(const password: WideString);
begin
  DefaultInterface.ExchangeSealREP(password);
end;

procedure TWatchComm.SendMacAddress(const macAddressPart1: WideString; 
                                    const macAddressPart2: WideString; 
                                    const macAddressPart3: WideString; 
                                    const macAddressPart4: WideString; 
                                    const macAddressPart5: WideString; 
                                    const macAddressPart6: WideString);
begin
  DefaultInterface.SendMacAddress(macAddressPart1, macAddressPart2, macAddressPart3, 
                                  macAddressPart4, macAddressPart5, macAddressPart6);
end;

function TWatchComm.InquiryFingerPrint(InquiryFingerPrintType: InquiryFingerPrintType): _PrintPointFingerPrintMessage;
begin
  Result := DefaultInterface.InquiryFingerPrint(InquiryFingerPrintType);
end;

function TWatchComm.InquiryFingerPrint_2(employeeID: Integer): PSafeArray;
begin
  Result := DefaultInterface.InquiryFingerPrint_2(employeeID);
end;

function TWatchComm.InquiryBioPointFingerPrint: _BioPointFingerPrintMessage;
begin
  Result := DefaultInterface.InquiryBioPointFingerPrint;
end;

function TWatchComm.InquiryFaceFingerPrint(employeeID: Integer): PSafeArray;
begin
  Result := DefaultInterface.InquiryFaceFingerPrint(employeeID);
end;

function TWatchComm.InquiryPrintPointEvent: _PrintPointEvent;
begin
  Result := DefaultInterface.InquiryPrintPointEvent;
end;

function TWatchComm.ConfirmReceiptPrintPointEvent: _PrintPointEvent;
begin
  Result := DefaultInterface.ConfirmReceiptPrintPointEvent;
end;

function TWatchComm.InquiryPrintPointMRPEventLog: _PrintPointMRPEventLog;
begin
  Result := DefaultInterface.InquiryPrintPointMRPEventLog;
end;

function TWatchComm.ConfirmationReceiptFingerPrint: _PrintPointFingerPrintMessage;
begin
  Result := DefaultInterface.ConfirmationReceiptFingerPrint;
end;

function TWatchComm.ConfirmationReceiptBioPointFingerPrint: _BioPointFingerPrintMessage;
begin
  Result := DefaultInterface.ConfirmationReceiptBioPointFingerPrint;
end;

function TWatchComm.ConfirmationReceiptMRPRecords: PSafeArray;
begin
  Result := DefaultInterface.ConfirmationReceiptMRPRecords;
end;

function TWatchComm.ConfirmationReceiptEmployeeList: PSafeArray;
begin
  Result := DefaultInterface.ConfirmationReceiptEmployeeList;
end;

function TWatchComm.InquiryEmployeer: _PrintPointEmployerMessage;
begin
  Result := DefaultInterface.InquiryEmployeer;
end;

function TWatchComm.InquirySerialNumberOfREPAndMemory: _PrintPointInquirySerialNumberOfREPAndMemoryResponse;
begin
  Result := DefaultInterface.InquirySerialNumberOfREPAndMemory;
end;

function TWatchComm.InquirySerialNumber: WideString;
begin
  Result := DefaultInterface.InquirySerialNumber;
end;

function TWatchComm.InquiryMRPRecords(inquiryChangeEmployed: WordBool; 
                                      inquirySettingRealTimeClock: WordBool; 
                                      inquiryRegistrationMarkingPoint: WordBool; 
                                      inquiryChangeCompanyIdentification: WordBool): PSafeArray;
begin
  Result := DefaultInterface.InquiryMRPRecords(inquiryChangeEmployed, inquirySettingRealTimeClock, 
                                               inquiryRegistrationMarkingPoint, 
                                               inquiryChangeCompanyIdentification);
end;

function TWatchComm.InquiryMRPRecords_2(startDate: TDateTime; inquiryChangeEmployed: WordBool; 
                                        inquirySettingRealTimeClock: WordBool; 
                                        inquiryRegistrationMarkingPoint: WordBool; 
                                        inquiryChangeCompanyIdentification: WordBool): PSafeArray;
begin
  Result := DefaultInterface.InquiryMRPRecords_2(startDate, inquiryChangeEmployed, 
                                                 inquirySettingRealTimeClock, 
                                                 inquiryRegistrationMarkingPoint, 
                                                 inquiryChangeCompanyIdentification);
end;

function TWatchComm.InquiryMRPRecords_3(const startNSR: WideString; 
                                        inquiryChangeEmployed: WordBool; 
                                        inquirySettingRealTimeClock: WordBool; 
                                        inquiryRegistrationMarkingPoint: WordBool; 
                                        inquiryChangeCompanyIdentification: WordBool): PSafeArray;
begin
  Result := DefaultInterface.InquiryMRPRecords_3(startNSR, inquiryChangeEmployed, 
                                                 inquirySettingRealTimeClock, 
                                                 inquiryRegistrationMarkingPoint, 
                                                 inquiryChangeCompanyIdentification);
end;

function TWatchComm.InquiryMRPRecords_4(const startNSR: WideString; 
                                        var errorOccurredInProcess: WordBool): PSafeArray;
begin
  Result := DefaultInterface.InquiryMRPRecords_4(startNSR, errorOccurredInProcess);
end;

function TWatchComm.InquiryFaceLogRecords: PSafeArray;
begin
  Result := DefaultInterface.InquiryFaceLogRecords;
end;

procedure TWatchComm.DeleteFaceLogRecords;
begin
  DefaultInterface.DeleteFaceLogRecords;
end;

function TWatchComm.InquiryFaceTemplate(employeeID: Integer): WideString;
begin
  Result := DefaultInterface.InquiryFaceTemplate(employeeID);
end;

procedure TWatchComm.IncludeFaceTemplate(employeeID: Integer; const faceTemplate: WideString);
begin
  DefaultInterface.IncludeFaceTemplate(employeeID, faceTemplate);
end;

procedure TWatchComm.ExcludeFaceTemplate(employeeID: Integer);
begin
  DefaultInterface.ExcludeFaceTemplate(employeeID);
end;

function TWatchComm.InquiryRandomNumber: _RandomNumberResponse;
begin
  Result := DefaultInterface.InquiryRandomNumber;
end;

function TWatchComm.InquiryEmployeeList: PSafeArray;
begin
  Result := DefaultInterface.InquiryEmployeeList;
end;

function TWatchComm.InquiryFaceEmployeeList: PSafeArray;
begin
  Result := DefaultInterface.InquiryFaceEmployeeList;
end;

procedure TWatchComm.ExcludeFingerPrint(const pis: WideString);
begin
  DefaultInterface.ExcludeFingerPrint(pis);
end;

procedure TWatchComm.ExcludeFingerPrint_2(employeeID: Integer; fingerPrintType: EFingerPrintType);
begin
  DefaultInterface.ExcludeFingerPrint_2(employeeID, fingerPrintType);
end;

procedure TWatchComm.ExcludeCostCenter(costCenterID: Integer);
begin
  DefaultInterface.ExcludeCostCenter(costCenterID);
end;

procedure TWatchComm.ExcludeBioPointFingerPrint(const cardNumber: WideString);
begin
  DefaultInterface.ExcludeBioPointFingerPrint(cardNumber);
end;

procedure TWatchComm.IncludeBioPointFingerPrint(const cardNumber: WideString; 
                                                const fingerPrint: WideString; 
                                                fingerPrintTypeOne: EFingerPrintType; 
                                                fingerPrintTypeTwo: EFingerPrintType);
begin
  DefaultInterface.IncludeBioPointFingerPrint(cardNumber, fingerPrint, fingerPrintTypeOne, 
                                              fingerPrintTypeTwo);
end;

procedure TWatchComm.ExcludeCredentialList;
begin
  DefaultInterface.ExcludeCredentialList;
end;

procedure TWatchComm.ExcludeEmployeesList;
begin
  DefaultInterface.ExcludeEmployeesList;
end;

procedure TWatchComm.ExcludeEmployeesListWithCostNumber;
begin
  DefaultInterface.ExcludeEmployeesListWithCostNumber;
end;

procedure TWatchComm.RepositioningMRPRecordsPointer_2(date: TDateTime);
begin
  DefaultInterface.RepositioningMRPRecordsPointer_2(date);
end;

procedure TWatchComm.RepositioningMRPRecordsPointer_3(const nsr: WideString);
begin
  DefaultInterface.RepositioningMRPRecordsPointer_3(nsr);
end;

procedure TWatchComm.setHoliday(day: Byte; month: Byte);
begin
  DefaultInterface.setHoliday(day, month);
end;

procedure TWatchComm.setHoliday_2(const HolidayCollection: _HolidayCollection);
begin
  DefaultInterface.setHoliday_2(HolidayCollection);
end;

procedure TWatchComm.sendEmptyMessage;
begin
  DefaultInterface.sendEmptyMessage;
end;

procedure TWatchComm.SendSerialNumber(plateSerialNumber: Int64; mrpSerialNumber: Int64; 
                                      mrpSealNumber: Int64);
begin
  DefaultInterface.SendSerialNumber(plateSerialNumber, mrpSerialNumber, mrpSealNumber);
end;

procedure TWatchComm.SendSerialNumber_2(const serialNumber: WideString);
begin
  DefaultInterface.SendSerialNumber_2(serialNumber);
end;

procedure TWatchComm.RemoveItemCardList(code: Integer);
begin
  DefaultInterface.RemoveItemCardList(code);
end;

procedure TWatchComm.UpdateShiftTable(const ShiftTableCollection: _ShiftTableCollection);
begin
  DefaultInterface.UpdateShiftTable(ShiftTableCollection);
end;

procedure TWatchComm.UpdateFirmware(const filePath: WideString);
begin
  DefaultInterface.UpdateFirmware(filePath);
end;

procedure TWatchComm.AlarmRing(const AlarmRing: _AlarmRingsCollection);
begin
  DefaultInterface.AlarmRing(AlarmRing);
end;

procedure TWatchComm.setMemoryFormat(const MemoryFormat: _MemoryFormat);
begin
  DefaultInterface.setMemoryFormat(MemoryFormat);
end;

procedure TWatchComm.setAlternativeCode(AlternativeCode: Integer; const code: WideString);
begin
  DefaultInterface.setAlternativeCode(AlternativeCode, code);
end;

procedure TWatchComm.removeAlternativeCode(AlternativeCode: Integer);
begin
  DefaultInterface.removeAlternativeCode(AlternativeCode);
end;

procedure TWatchComm.setJourneyWorking(const JourneyWorking: _JourneyWorking);
begin
  DefaultInterface.setJourneyWorking(JourneyWorking);
end;

procedure TWatchComm.ClearData;
begin
  DefaultInterface.ClearData;
end;

function TWatchComm.GetUnlockCode(const lockCode: WideString): WideString;
begin
  Result := DefaultInterface.GetUnlockCode(lockCode);
end;

procedure TWatchComm.CancelEmployeeTransmission;
begin
  DefaultInterface.CancelEmployeeTransmission;
end;

procedure TWatchComm.CancelCredentialTransmission;
begin
  DefaultInterface.CancelCredentialTransmission;
end;

procedure TWatchComm.ClearCredencialList;
begin
  DefaultInterface.ClearCredencialList;
end;

procedure TWatchComm.ClearEmployeeList;
begin
  DefaultInterface.ClearEmployeeList;
end;

procedure TWatchComm.add_progressOFEmployeesListTransmission(const value: _EmployeesListTransmissionProgress_2);
begin
  DefaultInterface.add_progressOFEmployeesListTransmission(value);
end;

procedure TWatchComm.remove_progressOFEmployeesListTransmission(const value: _EmployeesListTransmissionProgress_2);
begin
  DefaultInterface.remove_progressOFEmployeesListTransmission(value);
end;

procedure TWatchComm.add_progressOFCredentialsListTransmission(const value: _CredentialsListTransmissionProgress);
begin
  DefaultInterface.add_progressOFCredentialsListTransmission(value);
end;

procedure TWatchComm.remove_progressOFCredentialsListTransmission(const value: _CredentialsListTransmissionProgress);
begin
  DefaultInterface.remove_progressOFCredentialsListTransmission(value);
end;

procedure TWatchComm.ExcludeFingerPrintList;
begin
  DefaultInterface.ExcludeFingerPrintList;
end;

procedure TWatchComm.ExcludeFingerPrintWithoutEmployee;
begin
  DefaultInterface.ExcludeFingerPrintWithoutEmployee;
end;

procedure TWatchComm.ExcludeFingerPrint_3(const pis: WideString; sensor: EfingerPrintSensor);
begin
  DefaultInterface.ExcludeFingerPrint_3(pis, sensor);
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TWatchCommProperties.Create(AServer: TWatchComm);
begin
  inherited Create;
  FServer := AServer;
end;

function TWatchCommProperties.GetDefaultInterface: _WatchComm;
begin
  Result := FServer.DefaultInterface;
end;

function TWatchCommProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TWatchCommProperties.Get_Connected: WordBool;
begin
    Result := DefaultInterface.Connected;
end;

function TWatchCommProperties.Get_GeneralTimeout: Integer;
begin
    Result := DefaultInterface.GeneralTimeout;
end;

procedure TWatchCommProperties.Set_GeneralTimeout1(pRetVal: Integer);
begin
  DefaultInterface.Set_GeneralTimeout1(pRetVal);
end;

function TWatchCommProperties.Get_CollectTimeout: Integer;
begin
    Result := DefaultInterface.CollectTimeout;
end;

procedure TWatchCommProperties.Set_CollectTimeout1(pRetVal: Integer);
begin
  DefaultInterface.Set_CollectTimeout1(pRetVal);
end;

function TWatchCommProperties.Get_Protocol: _AbstractProtocol;
begin
    Result := DefaultInterface.Protocol;
end;

procedure TWatchCommProperties._Set_Protocol1(const pRetVal: _AbstractProtocol);
  { Warning: The property Protocol1 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Protocol1 := pRetVal;
end;

{$ENDIF}

class function CoEmployeesListTransmissionProgress_2.Create: _EmployeesListTransmissionProgress_2;
begin
  Result := CreateComObject(CLASS_EmployeesListTransmissionProgress_2) as _EmployeesListTransmissionProgress_2;
end;

class function CoEmployeesListTransmissionProgress_2.CreateRemote(const MachineName: string): _EmployeesListTransmissionProgress_2;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_EmployeesListTransmissionProgress_2) as _EmployeesListTransmissionProgress_2;
end;

class function CoCredentialsListTransmissionProgress.Create: _CredentialsListTransmissionProgress;
begin
  Result := CreateComObject(CLASS_CredentialsListTransmissionProgress) as _CredentialsListTransmissionProgress;
end;

class function CoCredentialsListTransmissionProgress.CreateRemote(const MachineName: string): _CredentialsListTransmissionProgress;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_CredentialsListTransmissionProgress) as _CredentialsListTransmissionProgress;
end;

class function CoPrintPointSendSerialNumberMessage.Create: _PrintPointSendSerialNumberMessage;
begin
  Result := CreateComObject(CLASS_PrintPointSendSerialNumberMessage) as _PrintPointSendSerialNumberMessage;
end;

class function CoPrintPointSendSerialNumberMessage.CreateRemote(const MachineName: string): _PrintPointSendSerialNumberMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_PrintPointSendSerialNumberMessage) as _PrintPointSendSerialNumberMessage;
end;

procedure TPrintPointSendSerialNumberMessage.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{DDEECBF5-17DC-38DC-B220-B3CA08A3FD5B}';
    IntfIID:   '{3F62753F-3060-3AE8-881D-D2D55E6F65FD}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TPrintPointSendSerialNumberMessage.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _PrintPointSendSerialNumberMessage;
  end;
end;

procedure TPrintPointSendSerialNumberMessage.ConnectTo(svrIntf: _PrintPointSendSerialNumberMessage);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TPrintPointSendSerialNumberMessage.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TPrintPointSendSerialNumberMessage.GetDefaultInterface: _PrintPointSendSerialNumberMessage;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TPrintPointSendSerialNumberMessage.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TPrintPointSendSerialNumberMessageProperties.Create(Self);
{$ENDIF}
end;

destructor TPrintPointSendSerialNumberMessage.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TPrintPointSendSerialNumberMessage.GetServerProperties: TPrintPointSendSerialNumberMessageProperties;
begin
  Result := FProps;
end;
{$ENDIF}

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TPrintPointSendSerialNumberMessageProperties.Create(AServer: TPrintPointSendSerialNumberMessage);
begin
  inherited Create;
  FServer := AServer;
end;

function TPrintPointSendSerialNumberMessageProperties.GetDefaultInterface: _PrintPointSendSerialNumberMessage;
begin
  Result := FServer.DefaultInterface;
end;

{$ENDIF}

class function CoPrintPointLiStatus.Create: _PrintPointLiStatus;
begin
  Result := CreateComObject(CLASS_PrintPointLiStatus) as _PrintPointLiStatus;
end;

class function CoPrintPointLiStatus.CreateRemote(const MachineName: string): _PrintPointLiStatus;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_PrintPointLiStatus) as _PrintPointLiStatus;
end;

procedure TPrintPointLiStatus.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{01475796-3364-4A9C-8FCC-ADD713548B8E}';
    IntfIID:   '{CB3D475A-280A-3F62-BDE2-1341D0F8E15A}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TPrintPointLiStatus.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _PrintPointLiStatus;
  end;
end;

procedure TPrintPointLiStatus.ConnectTo(svrIntf: _PrintPointLiStatus);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TPrintPointLiStatus.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TPrintPointLiStatus.GetDefaultInterface: _PrintPointLiStatus;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TPrintPointLiStatus.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TPrintPointLiStatusProperties.Create(Self);
{$ENDIF}
end;

destructor TPrintPointLiStatus.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TPrintPointLiStatus.GetServerProperties: TPrintPointLiStatusProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TPrintPointLiStatus.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TPrintPointLiStatus.Get_RecordsCapacity: Integer;
begin
    Result := DefaultInterface.RecordsCapacity;
end;

function TPrintPointLiStatus.Get_FingerprintCapacity: Integer;
begin
    Result := DefaultInterface.FingerprintCapacity;
end;

function TPrintPointLiStatus.Get_UsersCapacity: Integer;
begin
    Result := DefaultInterface.UsersCapacity;
end;

function TPrintPointLiStatus.Get_MasterOccupation: Integer;
begin
    Result := DefaultInterface.MasterOccupation;
end;

function TPrintPointLiStatus.Get_PasswordOccupation: Integer;
begin
    Result := DefaultInterface.PasswordOccupation;
end;

function TPrintPointLiStatus.Get_FingerprintOccupation: Integer;
begin
    Result := DefaultInterface.FingerprintOccupation;
end;

function TPrintPointLiStatus.Get_UserOccupation: Integer;
begin
    Result := DefaultInterface.UserOccupation;
end;

function TPrintPointLiStatus.Get_firmwareVersion: WideString;
begin
    Result := DefaultInterface.firmwareVersion;
end;

function TPrintPointLiStatus.Get_MRPVersion: WideString;
begin
    Result := DefaultInterface.MRPVersion;
end;

function TPrintPointLiStatus.Get_RecordsTotal: Integer;
begin
    Result := DefaultInterface.RecordsTotal;
end;

function TPrintPointLiStatus.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TPrintPointLiStatus.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TPrintPointLiStatus.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TPrintPointLiStatusProperties.Create(AServer: TPrintPointLiStatus);
begin
  inherited Create;
  FServer := AServer;
end;

function TPrintPointLiStatusProperties.GetDefaultInterface: _PrintPointLiStatus;
begin
  Result := FServer.DefaultInterface;
end;

function TPrintPointLiStatusProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TPrintPointLiStatusProperties.Get_RecordsCapacity: Integer;
begin
    Result := DefaultInterface.RecordsCapacity;
end;

function TPrintPointLiStatusProperties.Get_FingerprintCapacity: Integer;
begin
    Result := DefaultInterface.FingerprintCapacity;
end;

function TPrintPointLiStatusProperties.Get_UsersCapacity: Integer;
begin
    Result := DefaultInterface.UsersCapacity;
end;

function TPrintPointLiStatusProperties.Get_MasterOccupation: Integer;
begin
    Result := DefaultInterface.MasterOccupation;
end;

function TPrintPointLiStatusProperties.Get_PasswordOccupation: Integer;
begin
    Result := DefaultInterface.PasswordOccupation;
end;

function TPrintPointLiStatusProperties.Get_FingerprintOccupation: Integer;
begin
    Result := DefaultInterface.FingerprintOccupation;
end;

function TPrintPointLiStatusProperties.Get_UserOccupation: Integer;
begin
    Result := DefaultInterface.UserOccupation;
end;

function TPrintPointLiStatusProperties.Get_firmwareVersion: WideString;
begin
    Result := DefaultInterface.firmwareVersion;
end;

function TPrintPointLiStatusProperties.Get_MRPVersion: WideString;
begin
    Result := DefaultInterface.MRPVersion;
end;

function TPrintPointLiStatusProperties.Get_RecordsTotal: Integer;
begin
    Result := DefaultInterface.RecordsTotal;
end;

{$ENDIF}

class function CoMRPRecord.Create: _MRPRecord;
begin
  Result := CreateComObject(CLASS_MRPRecord) as _MRPRecord;
end;

class function CoMRPRecord.CreateRemote(const MachineName: string): _MRPRecord;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_MRPRecord) as _MRPRecord;
end;

procedure TMRPRecord.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{79A02533-D650-40A1-B7F2-E784083D69F5}';
    IntfIID:   '{F0036B67-82AA-36B4-B617-6AD095B54F40}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TMRPRecord.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _MRPRecord;
  end;
end;

procedure TMRPRecord.ConnectTo(svrIntf: _MRPRecord);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TMRPRecord.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TMRPRecord.GetDefaultInterface: _MRPRecord;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TMRPRecord.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TMRPRecordProperties.Create(Self);
{$ENDIF}
end;

destructor TMRPRecord.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TMRPRecord.GetServerProperties: TMRPRecordProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TMRPRecord.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TMRPRecord.Get_nsr: WideString;
begin
    Result := DefaultInterface.nsr;
end;

procedure TMRPRecord.Set_nsr(const pRetVal: WideString);
  { Warning: The property nsr has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.nsr := pRetVal;
end;

function TMRPRecord.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TMRPRecord.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TMRPRecord.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TMRPRecordProperties.Create(AServer: TMRPRecord);
begin
  inherited Create;
  FServer := AServer;
end;

function TMRPRecordProperties.GetDefaultInterface: _MRPRecord;
begin
  Result := FServer.DefaultInterface;
end;

function TMRPRecordProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TMRPRecordProperties.Get_nsr: WideString;
begin
    Result := DefaultInterface.nsr;
end;

procedure TMRPRecordProperties.Set_nsr(const pRetVal: WideString);
  { Warning: The property nsr has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.nsr := pRetVal;
end;

{$ENDIF}

class function CoMRPRecord_ChangeEmployee.Create: _MRPRecord_ChangeEmployee;
begin
  Result := CreateComObject(CLASS_MRPRecord_ChangeEmployee) as _MRPRecord_ChangeEmployee;
end;

class function CoMRPRecord_ChangeEmployee.CreateRemote(const MachineName: string): _MRPRecord_ChangeEmployee;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_MRPRecord_ChangeEmployee) as _MRPRecord_ChangeEmployee;
end;

class function CoFaceFingerPrint.Create: _FaceFingerPrint;
begin
  Result := CreateComObject(CLASS_FaceFingerPrint) as _FaceFingerPrint;
end;

class function CoFaceFingerPrint.CreateRemote(const MachineName: string): _FaceFingerPrint;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_FaceFingerPrint) as _FaceFingerPrint;
end;

procedure TFaceFingerPrint.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{45AFB8A2-CE08-40FC-9E98-6D98B9AEEEBE}';
    IntfIID:   '{C65B932D-291C-3FB9-90A8-11B8312DE61D}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TFaceFingerPrint.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _FaceFingerPrint;
  end;
end;

procedure TFaceFingerPrint.ConnectTo(svrIntf: _FaceFingerPrint);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TFaceFingerPrint.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TFaceFingerPrint.GetDefaultInterface: _FaceFingerPrint;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TFaceFingerPrint.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TFaceFingerPrintProperties.Create(Self);
{$ENDIF}
end;

destructor TFaceFingerPrint.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TFaceFingerPrint.GetServerProperties: TFaceFingerPrintProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TFaceFingerPrint.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TFaceFingerPrint.Get_fingerPrintType: EFingerPrintType;
begin
    Result := DefaultInterface.fingerPrintType;
end;

procedure TFaceFingerPrint.Set_fingerPrintType(pRetVal: EFingerPrintType);
begin
  DefaultInterface.Set_fingerPrintType(pRetVal);
end;

function TFaceFingerPrint.Get_fingerPrint: WideString;
begin
    Result := DefaultInterface.fingerPrint;
end;

procedure TFaceFingerPrint.Set_fingerPrint(const pRetVal: WideString);
  { Warning: The property fingerPrint has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.fingerPrint := pRetVal;
end;

function TFaceFingerPrint.Get_employeeID: Integer;
begin
    Result := DefaultInterface.employeeID;
end;

procedure TFaceFingerPrint.Set_employeeID(pRetVal: Integer);
begin
  DefaultInterface.Set_employeeID(pRetVal);
end;

function TFaceFingerPrint.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TFaceFingerPrint.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TFaceFingerPrint.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TFaceFingerPrintProperties.Create(AServer: TFaceFingerPrint);
begin
  inherited Create;
  FServer := AServer;
end;

function TFaceFingerPrintProperties.GetDefaultInterface: _FaceFingerPrint;
begin
  Result := FServer.DefaultInterface;
end;

function TFaceFingerPrintProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TFaceFingerPrintProperties.Get_fingerPrintType: EFingerPrintType;
begin
    Result := DefaultInterface.fingerPrintType;
end;

procedure TFaceFingerPrintProperties.Set_fingerPrintType(pRetVal: EFingerPrintType);
begin
  DefaultInterface.Set_fingerPrintType(pRetVal);
end;

function TFaceFingerPrintProperties.Get_fingerPrint: WideString;
begin
    Result := DefaultInterface.fingerPrint;
end;

procedure TFaceFingerPrintProperties.Set_fingerPrint(const pRetVal: WideString);
  { Warning: The property fingerPrint has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.fingerPrint := pRetVal;
end;

function TFaceFingerPrintProperties.Get_employeeID: Integer;
begin
    Result := DefaultInterface.employeeID;
end;

procedure TFaceFingerPrintProperties.Set_employeeID(pRetVal: Integer);
begin
  DefaultInterface.Set_employeeID(pRetVal);
end;

{$ENDIF}

class function CoAbstractCardList.Create: _AbstractCardList;
begin
  Result := CreateComObject(CLASS_AbstractCardList) as _AbstractCardList;
end;

class function CoAbstractCardList.CreateRemote(const MachineName: string): _AbstractCardList;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AbstractCardList) as _AbstractCardList;
end;

class function CoAbstractStatusMessage.Create: _AbstractStatusMessage;
begin
  Result := CreateComObject(CLASS_AbstractStatusMessage) as _AbstractStatusMessage;
end;

class function CoAbstractStatusMessage.CreateRemote(const MachineName: string): _AbstractStatusMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AbstractStatusMessage) as _AbstractStatusMessage;
end;

class function CoMiniPointStatusMessage.Create: _MiniPointStatusMessage;
begin
  Result := CreateComObject(CLASS_MiniPointStatusMessage) as _MiniPointStatusMessage;
end;

class function CoMiniPointStatusMessage.CreateRemote(const MachineName: string): _MiniPointStatusMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_MiniPointStatusMessage) as _MiniPointStatusMessage;
end;

procedure TMiniPointStatusMessage.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{51AE264B-680F-4284-A7F2-C6AD1DE7FA48}';
    IntfIID:   '{1E3010DC-B3B4-3EB3-BF7C-19952DED0E4E}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TMiniPointStatusMessage.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _MiniPointStatusMessage;
  end;
end;

procedure TMiniPointStatusMessage.ConnectTo(svrIntf: _MiniPointStatusMessage);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TMiniPointStatusMessage.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TMiniPointStatusMessage.GetDefaultInterface: _MiniPointStatusMessage;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TMiniPointStatusMessage.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TMiniPointStatusMessageProperties.Create(Self);
{$ENDIF}
end;

destructor TMiniPointStatusMessage.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TMiniPointStatusMessage.GetServerProperties: TMiniPointStatusMessageProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TMiniPointStatusMessage.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TMiniPointStatusMessage.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TMiniPointStatusMessage.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

function TMiniPointStatusMessage.Get_firmwareVersion: WideString;
begin
    Result := DefaultInterface.firmwareVersion;
end;

procedure TMiniPointStatusMessage.Set_firmwareVersion(const pRetVal: WideString);
  { Warning: The property firmwareVersion has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.firmwareVersion := pRetVal;
end;

function TMiniPointStatusMessage.Get_RecordsSize: LongWord;
begin
    Result := DefaultInterface.RecordsSize;
end;

procedure TMiniPointStatusMessage.Set_RecordsSize(pRetVal: LongWord);
begin
  DefaultInterface.Set_RecordsSize(pRetVal);
end;

function TMiniPointStatusMessage.Get_RecordsCount: LongWord;
begin
    Result := DefaultInterface.RecordsCount;
end;

procedure TMiniPointStatusMessage.Set_RecordsCount(pRetVal: LongWord);
begin
  DefaultInterface.Set_RecordsCount(pRetVal);
end;

function TMiniPointStatusMessage.Get_CardListSize: LongWord;
begin
    Result := DefaultInterface.CardListSize;
end;

procedure TMiniPointStatusMessage.Set_CardListSize(pRetVal: LongWord);
begin
  DefaultInterface.Set_CardListSize(pRetVal);
end;

function TMiniPointStatusMessage.Get_date: TDateTime;
begin
    Result := DefaultInterface.date;
end;

procedure TMiniPointStatusMessage.Set_date(pRetVal: TDateTime);
begin
  DefaultInterface.Set_date(pRetVal);
end;

function TMiniPointStatusMessage.Get_CheckType: Byte;
begin
    Result := DefaultInterface.CheckType;
end;

procedure TMiniPointStatusMessage.Set_CheckType(pRetVal: Byte);
begin
  DefaultInterface.Set_CheckType(pRetVal);
end;

function TMiniPointStatusMessage.Get_RecordDeniedAccess: WordBool;
begin
    Result := DefaultInterface.RecordDeniedAccess;
end;

procedure TMiniPointStatusMessage.Set_RecordDeniedAccess(pRetVal: WordBool);
begin
  DefaultInterface.Set_RecordDeniedAccess(pRetVal);
end;

function TMiniPointStatusMessage.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TMiniPointStatusMessage.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TMiniPointStatusMessage.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

function TMiniPointStatusMessage.GetData: PSafeArray;
begin
  Result := DefaultInterface.GetData;
end;

function TMiniPointStatusMessage.GetSize: Integer;
begin
  Result := DefaultInterface.GetSize;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TMiniPointStatusMessageProperties.Create(AServer: TMiniPointStatusMessage);
begin
  inherited Create;
  FServer := AServer;
end;

function TMiniPointStatusMessageProperties.GetDefaultInterface: _MiniPointStatusMessage;
begin
  Result := FServer.DefaultInterface;
end;

function TMiniPointStatusMessageProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TMiniPointStatusMessageProperties.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TMiniPointStatusMessageProperties.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

function TMiniPointStatusMessageProperties.Get_firmwareVersion: WideString;
begin
    Result := DefaultInterface.firmwareVersion;
end;

procedure TMiniPointStatusMessageProperties.Set_firmwareVersion(const pRetVal: WideString);
  { Warning: The property firmwareVersion has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.firmwareVersion := pRetVal;
end;

function TMiniPointStatusMessageProperties.Get_RecordsSize: LongWord;
begin
    Result := DefaultInterface.RecordsSize;
end;

procedure TMiniPointStatusMessageProperties.Set_RecordsSize(pRetVal: LongWord);
begin
  DefaultInterface.Set_RecordsSize(pRetVal);
end;

function TMiniPointStatusMessageProperties.Get_RecordsCount: LongWord;
begin
    Result := DefaultInterface.RecordsCount;
end;

procedure TMiniPointStatusMessageProperties.Set_RecordsCount(pRetVal: LongWord);
begin
  DefaultInterface.Set_RecordsCount(pRetVal);
end;

function TMiniPointStatusMessageProperties.Get_CardListSize: LongWord;
begin
    Result := DefaultInterface.CardListSize;
end;

procedure TMiniPointStatusMessageProperties.Set_CardListSize(pRetVal: LongWord);
begin
  DefaultInterface.Set_CardListSize(pRetVal);
end;

function TMiniPointStatusMessageProperties.Get_date: TDateTime;
begin
    Result := DefaultInterface.date;
end;

procedure TMiniPointStatusMessageProperties.Set_date(pRetVal: TDateTime);
begin
  DefaultInterface.Set_date(pRetVal);
end;

function TMiniPointStatusMessageProperties.Get_CheckType: Byte;
begin
    Result := DefaultInterface.CheckType;
end;

procedure TMiniPointStatusMessageProperties.Set_CheckType(pRetVal: Byte);
begin
  DefaultInterface.Set_CheckType(pRetVal);
end;

function TMiniPointStatusMessageProperties.Get_RecordDeniedAccess: WordBool;
begin
    Result := DefaultInterface.RecordDeniedAccess;
end;

procedure TMiniPointStatusMessageProperties.Set_RecordDeniedAccess(pRetVal: WordBool);
begin
  DefaultInterface.Set_RecordDeniedAccess(pRetVal);
end;

{$ENDIF}

class function CoNoDataMessage.Create: _NoDataMessage;
begin
  Result := CreateComObject(CLASS_NoDataMessage) as _NoDataMessage;
end;

class function CoNoDataMessage.CreateRemote(const MachineName: string): _NoDataMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_NoDataMessage) as _NoDataMessage;
end;

procedure TNoDataMessage.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{64577AD7-656C-40AD-9302-215D5003DDD1}';
    IntfIID:   '{80B6B329-5459-3562-92E9-1A134496876A}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TNoDataMessage.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _NoDataMessage;
  end;
end;

procedure TNoDataMessage.ConnectTo(svrIntf: _NoDataMessage);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TNoDataMessage.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TNoDataMessage.GetDefaultInterface: _NoDataMessage;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TNoDataMessage.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TNoDataMessageProperties.Create(Self);
{$ENDIF}
end;

destructor TNoDataMessage.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TNoDataMessage.GetServerProperties: TNoDataMessageProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TNoDataMessage.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TNoDataMessage.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TNoDataMessage.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

function TNoDataMessage.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TNoDataMessage.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TNoDataMessage.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

function TNoDataMessage.GetData: PSafeArray;
begin
  Result := DefaultInterface.GetData;
end;

function TNoDataMessage.GetSize: Integer;
begin
  Result := DefaultInterface.GetSize;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TNoDataMessageProperties.Create(AServer: TNoDataMessage);
begin
  inherited Create;
  FServer := AServer;
end;

function TNoDataMessageProperties.GetDefaultInterface: _NoDataMessage;
begin
  Result := FServer.DefaultInterface;
end;

function TNoDataMessageProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TNoDataMessageProperties.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TNoDataMessageProperties.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

{$ENDIF}

class function CoTemplate.Create: _Template;
begin
  Result := CreateComObject(CLASS_Template) as _Template;
end;

class function CoTemplate.CreateRemote(const MachineName: string): _Template;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Template) as _Template;
end;

procedure TTemplate.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{A4C3BA08-D406-4AF2-82B7-4334EC81EFC4}';
    IntfIID:   '{C709AC5D-461C-33C6-B135-75B626665DCD}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TTemplate.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _Template;
  end;
end;

procedure TTemplate.ConnectTo(svrIntf: _Template);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TTemplate.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TTemplate.GetDefaultInterface: _Template;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TTemplate.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TTemplateProperties.Create(Self);
{$ENDIF}
end;

destructor TTemplate.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TTemplate.GetServerProperties: TTemplateProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TTemplate.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TTemplate.Get_Digital: WideString;
begin
    Result := DefaultInterface.Digital;
end;

procedure TTemplate.Set_Digital(const pRetVal: WideString);
  { Warning: The property Digital has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Digital := pRetVal;
end;

function TTemplate.Get_DedoDigital: TypeDedoDigital;
begin
    Result := DefaultInterface.DedoDigital;
end;

procedure TTemplate.Set_DedoDigital(pRetVal: TypeDedoDigital);
begin
  DefaultInterface.Set_DedoDigital(pRetVal);
end;

function TTemplate.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TTemplate.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TTemplate.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TTemplateProperties.Create(AServer: TTemplate);
begin
  inherited Create;
  FServer := AServer;
end;

function TTemplateProperties.GetDefaultInterface: _Template;
begin
  Result := FServer.DefaultInterface;
end;

function TTemplateProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TTemplateProperties.Get_Digital: WideString;
begin
    Result := DefaultInterface.Digital;
end;

procedure TTemplateProperties.Set_Digital(const pRetVal: WideString);
  { Warning: The property Digital has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Digital := pRetVal;
end;

function TTemplateProperties.Get_DedoDigital: TypeDedoDigital;
begin
    Result := DefaultInterface.DedoDigital;
end;

procedure TTemplateProperties.Set_DedoDigital(pRetVal: TypeDedoDigital);
begin
  DefaultInterface.Set_DedoDigital(pRetVal);
end;

{$ENDIF}

class function CoMiniPointConfigurator.Create: _MiniPointConfigurator;
begin
  Result := CreateComObject(CLASS_MiniPointConfigurator) as _MiniPointConfigurator;
end;

class function CoMiniPointConfigurator.CreateRemote(const MachineName: string): _MiniPointConfigurator;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_MiniPointConfigurator) as _MiniPointConfigurator;
end;

procedure TMiniPointConfigurator.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{FE44C0F4-F6D8-4DDD-9EBF-DA19BCCCE0CA}';
    IntfIID:   '{C94B244C-FE69-3B75-BC35-3EFF5C28B364}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TMiniPointConfigurator.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _MiniPointConfigurator;
  end;
end;

procedure TMiniPointConfigurator.ConnectTo(svrIntf: _MiniPointConfigurator);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TMiniPointConfigurator.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TMiniPointConfigurator.GetDefaultInterface: _MiniPointConfigurator;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TMiniPointConfigurator.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TMiniPointConfiguratorProperties.Create(Self);
{$ENDIF}
end;

destructor TMiniPointConfigurator.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TMiniPointConfigurator.GetServerProperties: TMiniPointConfiguratorProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TMiniPointConfigurator.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TMiniPointConfigurator.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TMiniPointConfigurator.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TMiniPointConfigurator.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TMiniPointConfiguratorProperties.Create(AServer: TMiniPointConfigurator);
begin
  inherited Create;
  FServer := AServer;
end;

function TMiniPointConfiguratorProperties.GetDefaultInterface: _MiniPointConfigurator;
begin
  Result := FServer.DefaultInterface;
end;

function TMiniPointConfiguratorProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

{$ENDIF}

class function CoUtil.Create: _Util;
begin
  Result := CreateComObject(CLASS_Util) as _Util;
end;

class function CoUtil.CreateRemote(const MachineName: string): _Util;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Util) as _Util;
end;

procedure TUtil.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{C8E86A96-3598-46A2-96BF-EF7B30D78D3B}';
    IntfIID:   '{5BCFE2B0-7597-36B6-801B-93EC25098916}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TUtil.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _Util;
  end;
end;

procedure TUtil.ConnectTo(svrIntf: _Util);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TUtil.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TUtil.GetDefaultInterface: _Util;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TUtil.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TUtilProperties.Create(Self);
{$ENDIF}
end;

destructor TUtil.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TUtil.GetServerProperties: TUtilProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TUtil.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TUtil.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TUtil.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TUtil.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TUtilProperties.Create(AServer: TUtil);
begin
  inherited Create;
  FServer := AServer;
end;

function TUtilProperties.GetDefaultInterface: _Util;
begin
  Result := FServer.DefaultInterface;
end;

function TUtilProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

{$ENDIF}

class function CoMicroPointStatusMessage.Create: _MicroPointStatusMessage;
begin
  Result := CreateComObject(CLASS_MicroPointStatusMessage) as _MicroPointStatusMessage;
end;

class function CoMicroPointStatusMessage.CreateRemote(const MachineName: string): _MicroPointStatusMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_MicroPointStatusMessage) as _MicroPointStatusMessage;
end;

procedure TMicroPointStatusMessage.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{269480B7-B13C-4DB1-8A42-9D0329A56A98}';
    IntfIID:   '{EA7AE898-8BDC-3757-B62C-E9D50CA5F4AC}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TMicroPointStatusMessage.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _MicroPointStatusMessage;
  end;
end;

procedure TMicroPointStatusMessage.ConnectTo(svrIntf: _MicroPointStatusMessage);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TMicroPointStatusMessage.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TMicroPointStatusMessage.GetDefaultInterface: _MicroPointStatusMessage;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TMicroPointStatusMessage.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TMicroPointStatusMessageProperties.Create(Self);
{$ENDIF}
end;

destructor TMicroPointStatusMessage.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TMicroPointStatusMessage.GetServerProperties: TMicroPointStatusMessageProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TMicroPointStatusMessage.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TMicroPointStatusMessage.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TMicroPointStatusMessage.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

function TMicroPointStatusMessage.Get_firmwareVersion: WideString;
begin
    Result := DefaultInterface.firmwareVersion;
end;

procedure TMicroPointStatusMessage.Set_firmwareVersion(const pRetVal: WideString);
  { Warning: The property firmwareVersion has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.firmwareVersion := pRetVal;
end;

function TMicroPointStatusMessage.Get_RecordsSize: LongWord;
begin
    Result := DefaultInterface.RecordsSize;
end;

procedure TMicroPointStatusMessage.Set_RecordsSize(pRetVal: LongWord);
begin
  DefaultInterface.Set_RecordsSize(pRetVal);
end;

function TMicroPointStatusMessage.Get_RecordsCount: LongWord;
begin
    Result := DefaultInterface.RecordsCount;
end;

procedure TMicroPointStatusMessage.Set_RecordsCount(pRetVal: LongWord);
begin
  DefaultInterface.Set_RecordsCount(pRetVal);
end;

function TMicroPointStatusMessage.Get_CardListSize: LongWord;
begin
    Result := DefaultInterface.CardListSize;
end;

procedure TMicroPointStatusMessage.Set_CardListSize(pRetVal: LongWord);
begin
  DefaultInterface.Set_CardListSize(pRetVal);
end;

function TMicroPointStatusMessage.Get_date: TDateTime;
begin
    Result := DefaultInterface.date;
end;

procedure TMicroPointStatusMessage.Set_date(pRetVal: TDateTime);
begin
  DefaultInterface.Set_date(pRetVal);
end;

function TMicroPointStatusMessage.Get_CheckType: Byte;
begin
    Result := DefaultInterface.CheckType;
end;

procedure TMicroPointStatusMessage.Set_CheckType(pRetVal: Byte);
begin
  DefaultInterface.Set_CheckType(pRetVal);
end;

function TMicroPointStatusMessage.Get_RecordDeniedAccess: WordBool;
begin
    Result := DefaultInterface.RecordDeniedAccess;
end;

procedure TMicroPointStatusMessage.Set_RecordDeniedAccess(pRetVal: WordBool);
begin
  DefaultInterface.Set_RecordDeniedAccess(pRetVal);
end;

function TMicroPointStatusMessage.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TMicroPointStatusMessage.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TMicroPointStatusMessage.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

function TMicroPointStatusMessage.GetData: PSafeArray;
begin
  Result := DefaultInterface.GetData;
end;

function TMicroPointStatusMessage.GetSize: Integer;
begin
  Result := DefaultInterface.GetSize;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TMicroPointStatusMessageProperties.Create(AServer: TMicroPointStatusMessage);
begin
  inherited Create;
  FServer := AServer;
end;

function TMicroPointStatusMessageProperties.GetDefaultInterface: _MicroPointStatusMessage;
begin
  Result := FServer.DefaultInterface;
end;

function TMicroPointStatusMessageProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TMicroPointStatusMessageProperties.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TMicroPointStatusMessageProperties.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

function TMicroPointStatusMessageProperties.Get_firmwareVersion: WideString;
begin
    Result := DefaultInterface.firmwareVersion;
end;

procedure TMicroPointStatusMessageProperties.Set_firmwareVersion(const pRetVal: WideString);
  { Warning: The property firmwareVersion has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.firmwareVersion := pRetVal;
end;

function TMicroPointStatusMessageProperties.Get_RecordsSize: LongWord;
begin
    Result := DefaultInterface.RecordsSize;
end;

procedure TMicroPointStatusMessageProperties.Set_RecordsSize(pRetVal: LongWord);
begin
  DefaultInterface.Set_RecordsSize(pRetVal);
end;

function TMicroPointStatusMessageProperties.Get_RecordsCount: LongWord;
begin
    Result := DefaultInterface.RecordsCount;
end;

procedure TMicroPointStatusMessageProperties.Set_RecordsCount(pRetVal: LongWord);
begin
  DefaultInterface.Set_RecordsCount(pRetVal);
end;

function TMicroPointStatusMessageProperties.Get_CardListSize: LongWord;
begin
    Result := DefaultInterface.CardListSize;
end;

procedure TMicroPointStatusMessageProperties.Set_CardListSize(pRetVal: LongWord);
begin
  DefaultInterface.Set_CardListSize(pRetVal);
end;

function TMicroPointStatusMessageProperties.Get_date: TDateTime;
begin
    Result := DefaultInterface.date;
end;

procedure TMicroPointStatusMessageProperties.Set_date(pRetVal: TDateTime);
begin
  DefaultInterface.Set_date(pRetVal);
end;

function TMicroPointStatusMessageProperties.Get_CheckType: Byte;
begin
    Result := DefaultInterface.CheckType;
end;

procedure TMicroPointStatusMessageProperties.Set_CheckType(pRetVal: Byte);
begin
  DefaultInterface.Set_CheckType(pRetVal);
end;

function TMicroPointStatusMessageProperties.Get_RecordDeniedAccess: WordBool;
begin
    Result := DefaultInterface.RecordDeniedAccess;
end;

procedure TMicroPointStatusMessageProperties.Set_RecordDeniedAccess(pRetVal: WordBool);
begin
  DefaultInterface.Set_RecordDeniedAccess(pRetVal);
end;

{$ENDIF}

class function CoBioPointCardList.Create: _BioPointCardList;
begin
  Result := CreateComObject(CLASS_BioPointCardList) as _BioPointCardList;
end;

class function CoBioPointCardList.CreateRemote(const MachineName: string): _BioPointCardList;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_BioPointCardList) as _BioPointCardList;
end;

procedure TBioPointCardList.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{87115F94-9FB9-47EB-8D7D-7CC385500817}';
    IntfIID:   '{B8B4B8DB-4CDB-3B9B-A8CA-88405FC9A0A7}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TBioPointCardList.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _BioPointCardList;
  end;
end;

procedure TBioPointCardList.ConnectTo(svrIntf: _BioPointCardList);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TBioPointCardList.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TBioPointCardList.GetDefaultInterface: _BioPointCardList;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TBioPointCardList.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TBioPointCardListProperties.Create(Self);
{$ENDIF}
end;

destructor TBioPointCardList.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TBioPointCardList.GetServerProperties: TBioPointCardListProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TBioPointCardList.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TBioPointCardList.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TBioPointCardList.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

procedure TBioPointCardList._Set_Card(const pRetVal: _Card);
  { Warning: The property Card has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Card := pRetVal;
end;

function TBioPointCardList.Get_Card: _Card;
begin
    Result := DefaultInterface.Card;
end;

function TBioPointCardList.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TBioPointCardList.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TBioPointCardList.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

function TBioPointCardList.GetData: PSafeArray;
begin
  Result := DefaultInterface.GetData;
end;

function TBioPointCardList.GetSize: Integer;
begin
  Result := DefaultInterface.GetSize;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TBioPointCardListProperties.Create(AServer: TBioPointCardList);
begin
  inherited Create;
  FServer := AServer;
end;

function TBioPointCardListProperties.GetDefaultInterface: _BioPointCardList;
begin
  Result := FServer.DefaultInterface;
end;

function TBioPointCardListProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TBioPointCardListProperties.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TBioPointCardListProperties.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

procedure TBioPointCardListProperties._Set_Card(const pRetVal: _Card);
  { Warning: The property Card has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Card := pRetVal;
end;

function TBioPointCardListProperties.Get_Card: _Card;
begin
    Result := DefaultInterface.Card;
end;

{$ENDIF}

class function CoShiftTable.Create: _ShiftTable;
begin
  Result := CreateComObject(CLASS_ShiftTable) as _ShiftTable;
end;

class function CoShiftTable.CreateRemote(const MachineName: string): _ShiftTable;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_ShiftTable) as _ShiftTable;
end;

procedure TShiftTable.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{AB55F247-D316-4787-B267-ADF4ED26A84E}';
    IntfIID:   '{1F79777E-9628-326F-82D0-DB3A67DFF466}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TShiftTable.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _ShiftTable;
  end;
end;

procedure TShiftTable.ConnectTo(svrIntf: _ShiftTable);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TShiftTable.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TShiftTable.GetDefaultInterface: _ShiftTable;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TShiftTable.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TShiftTableProperties.Create(Self);
{$ENDIF}
end;

destructor TShiftTable.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TShiftTable.GetServerProperties: TShiftTableProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TShiftTable.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TShiftTable.Get_Inicio: WideString;
begin
    Result := DefaultInterface.Inicio;
end;

procedure TShiftTable.Set_Inicio(const pRetVal: WideString);
  { Warning: The property Inicio has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Inicio := pRetVal;
end;

function TShiftTable.Get_Fim: WideString;
begin
    Result := DefaultInterface.Fim;
end;

procedure TShiftTable.Set_Fim(const pRetVal: WideString);
  { Warning: The property Fim has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Fim := pRetVal;
end;

function TShiftTable.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TShiftTable.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TShiftTable.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TShiftTableProperties.Create(AServer: TShiftTable);
begin
  inherited Create;
  FServer := AServer;
end;

function TShiftTableProperties.GetDefaultInterface: _ShiftTable;
begin
  Result := FServer.DefaultInterface;
end;

function TShiftTableProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TShiftTableProperties.Get_Inicio: WideString;
begin
    Result := DefaultInterface.Inicio;
end;

procedure TShiftTableProperties.Set_Inicio(const pRetVal: WideString);
  { Warning: The property Inicio has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Inicio := pRetVal;
end;

function TShiftTableProperties.Get_Fim: WideString;
begin
    Result := DefaultInterface.Fim;
end;

procedure TShiftTableProperties.Set_Fim(const pRetVal: WideString);
  { Warning: The property Fim has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Fim := pRetVal;
end;

{$ENDIF}

class function CoJourneyWorking.Create: _JourneyWorking;
begin
  Result := CreateComObject(CLASS_JourneyWorking) as _JourneyWorking;
end;

class function CoJourneyWorking.CreateRemote(const MachineName: string): _JourneyWorking;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_JourneyWorking) as _JourneyWorking;
end;

procedure TJourneyWorking.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{B7BA908B-1AE8-47A6-B490-1FFBF7B63073}';
    IntfIID:   '{22A9FA9D-D34A-38C6-AE62-15ACFB57A842}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TJourneyWorking.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _JourneyWorking;
  end;
end;

procedure TJourneyWorking.ConnectTo(svrIntf: _JourneyWorking);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TJourneyWorking.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TJourneyWorking.GetDefaultInterface: _JourneyWorking;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TJourneyWorking.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TJourneyWorkingProperties.Create(Self);
{$ENDIF}
end;

destructor TJourneyWorking.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TJourneyWorking.GetServerProperties: TJourneyWorkingProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TJourneyWorking.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TJourneyWorking.Set_Id(pRetVal: Smallint);
begin
  DefaultInterface.Set_Id(pRetVal);
end;

function TJourneyWorking.Get_Id: Smallint;
begin
    Result := DefaultInterface.Id;
end;

procedure TJourneyWorking.Set_TypeWorking(pRetVal: TypeWorking);
begin
  DefaultInterface.Set_TypeWorking(pRetVal);
end;

function TJourneyWorking.Get_TypeWorking: TypeWorking;
begin
    Result := DefaultInterface.TypeWorking;
end;

function TJourneyWorking.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TJourneyWorking.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TJourneyWorking.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TJourneyWorkingProperties.Create(AServer: TJourneyWorking);
begin
  inherited Create;
  FServer := AServer;
end;

function TJourneyWorkingProperties.GetDefaultInterface: _JourneyWorking;
begin
  Result := FServer.DefaultInterface;
end;

function TJourneyWorkingProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TJourneyWorkingProperties.Set_Id(pRetVal: Smallint);
begin
  DefaultInterface.Set_Id(pRetVal);
end;

function TJourneyWorkingProperties.Get_Id: Smallint;
begin
    Result := DefaultInterface.Id;
end;

procedure TJourneyWorkingProperties.Set_TypeWorking(pRetVal: TypeWorking);
begin
  DefaultInterface.Set_TypeWorking(pRetVal);
end;

function TJourneyWorkingProperties.Get_TypeWorking: TypeWorking;
begin
    Result := DefaultInterface.TypeWorking;
end;

{$ENDIF}

class function CoPeriodicJourneyWorking.Create: _PeriodicJourneyWorking;
begin
  Result := CreateComObject(CLASS_PeriodicJourneyWorking) as _PeriodicJourneyWorking;
end;

class function CoPeriodicJourneyWorking.CreateRemote(const MachineName: string): _PeriodicJourneyWorking;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_PeriodicJourneyWorking) as _PeriodicJourneyWorking;
end;

procedure TPeriodicJourneyWorking.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{3389910E-6524-3A21-A5CC-B7E16D3593E7}';
    IntfIID:   '{5BE9179F-FF46-311D-AB60-AD28A14668B0}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TPeriodicJourneyWorking.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _PeriodicJourneyWorking;
  end;
end;

procedure TPeriodicJourneyWorking.ConnectTo(svrIntf: _PeriodicJourneyWorking);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TPeriodicJourneyWorking.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TPeriodicJourneyWorking.GetDefaultInterface: _PeriodicJourneyWorking;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TPeriodicJourneyWorking.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TPeriodicJourneyWorkingProperties.Create(Self);
{$ENDIF}
end;

destructor TPeriodicJourneyWorking.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TPeriodicJourneyWorking.GetServerProperties: TPeriodicJourneyWorkingProperties;
begin
  Result := FProps;
end;
{$ENDIF}

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TPeriodicJourneyWorkingProperties.Create(AServer: TPeriodicJourneyWorking);
begin
  inherited Create;
  FServer := AServer;
end;

function TPeriodicJourneyWorkingProperties.GetDefaultInterface: _PeriodicJourneyWorking;
begin
  Result := FServer.DefaultInterface;
end;

{$ENDIF}

class function CoAbstractJourneyWorking.Create: _AbstractJourneyWorking;
begin
  Result := CreateComObject(CLASS_AbstractJourneyWorking) as _AbstractJourneyWorking;
end;

class function CoAbstractJourneyWorking.CreateRemote(const MachineName: string): _AbstractJourneyWorking;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AbstractJourneyWorking) as _AbstractJourneyWorking;
end;

class function CoAbstractPunchMessage.Create: _AbstractPunchMessage;
begin
  Result := CreateComObject(CLASS_AbstractPunchMessage) as _AbstractPunchMessage;
end;

class function CoAbstractPunchMessage.CreateRemote(const MachineName: string): _AbstractPunchMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AbstractPunchMessage) as _AbstractPunchMessage;
end;

class function CoMRPRecord_ChangeCompanyIdentification.Create: _MRPRecord_ChangeCompanyIdentification;
begin
  Result := CreateComObject(CLASS_MRPRecord_ChangeCompanyIdentification) as _MRPRecord_ChangeCompanyIdentification;
end;

class function CoMRPRecord_ChangeCompanyIdentification.CreateRemote(const MachineName: string): _MRPRecord_ChangeCompanyIdentification;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_MRPRecord_ChangeCompanyIdentification) as _MRPRecord_ChangeCompanyIdentification;
end;

class function CoAbstractPacket.Create: _AbstractPacket;
begin
  Result := CreateComObject(CLASS_AbstractPacket) as _AbstractPacket;
end;

class function CoAbstractPacket.CreateRemote(const MachineName: string): _AbstractPacket;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AbstractPacket) as _AbstractPacket;
end;

class function CoMemoryFormat.Create: _MemoryFormat;
begin
  Result := CreateComObject(CLASS_MemoryFormat) as _MemoryFormat;
end;

class function CoMemoryFormat.CreateRemote(const MachineName: string): _MemoryFormat;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_MemoryFormat) as _MemoryFormat;
end;

procedure TMemoryFormat.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{DC98228F-4007-4180-8770-1A9AB4E297A2}';
    IntfIID:   '{CDCA2E43-A823-37CC-8262-ECF3BCC77DA3}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TMemoryFormat.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _MemoryFormat;
  end;
end;

procedure TMemoryFormat.ConnectTo(svrIntf: _MemoryFormat);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TMemoryFormat.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TMemoryFormat.GetDefaultInterface: _MemoryFormat;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TMemoryFormat.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TMemoryFormatProperties.Create(Self);
{$ENDIF}
end;

destructor TMemoryFormat.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TMemoryFormat.GetServerProperties: TMemoryFormatProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TMemoryFormat.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TMemoryFormat.Set_MinDigitCard(pRetVal: Byte);
begin
  DefaultInterface.Set_MinDigitCard(pRetVal);
end;

function TMemoryFormat.Get_MinDigitCard: Byte;
begin
    Result := DefaultInterface.MinDigitCard;
end;

procedure TMemoryFormat.Set_MaxDigitCard(pRetVal: Byte);
begin
  DefaultInterface.Set_MaxDigitCard(pRetVal);
end;

function TMemoryFormat.Get_MaxDigitCard: Byte;
begin
    Result := DefaultInterface.MaxDigitCard;
end;

procedure TMemoryFormat.Set_HasWork(pRetVal: WordBool);
begin
  DefaultInterface.Set_HasWork(pRetVal);
end;

function TMemoryFormat.Get_HasWork: WordBool;
begin
    Result := DefaultInterface.HasWork;
end;

procedure TMemoryFormat.Set_HasMessage(pRetVal: WordBool);
begin
  DefaultInterface.Set_HasMessage(pRetVal);
end;

function TMemoryFormat.Get_HasMessage: WordBool;
begin
    Result := DefaultInterface.HasMessage;
end;

procedure TMemoryFormat.Set_HasWay(pRetVal: WordBool);
begin
  DefaultInterface.Set_HasWay(pRetVal);
end;

function TMemoryFormat.Get_HasWay: WordBool;
begin
    Result := DefaultInterface.HasWay;
end;

procedure TMemoryFormat.Set_HasPassword(pRetVal: WordBool);
begin
  DefaultInterface.Set_HasPassword(pRetVal);
end;

function TMemoryFormat.Get_HasPassword: WordBool;
begin
    Result := DefaultInterface.HasPassword;
end;

procedure TMemoryFormat.Set_CounterAccess(pRetVal: Byte);
begin
  DefaultInterface.Set_CounterAccess(pRetVal);
end;

function TMemoryFormat.Get_CounterAccess: Byte;
begin
    Result := DefaultInterface.CounterAccess;
end;

procedure TMemoryFormat.Set_QuantityMaxCard(pRetVal: Smallint);
begin
  DefaultInterface.Set_QuantityMaxCard(pRetVal);
end;

function TMemoryFormat.Get_QuantityMaxCard: Smallint;
begin
    Result := DefaultInterface.QuantityMaxCard;
end;

procedure TMemoryFormat.Set_QuantityMaxAlternativeId(pRetVal: Smallint);
begin
  DefaultInterface.Set_QuantityMaxAlternativeId(pRetVal);
end;

function TMemoryFormat.Get_QuantityMaxAlternativeId: Smallint;
begin
    Result := DefaultInterface.QuantityMaxAlternativeId;
end;

procedure TMemoryFormat.Set_QuantityMaxWeeklyWork(pRetVal: Smallint);
begin
  DefaultInterface.Set_QuantityMaxWeeklyWork(pRetVal);
end;

function TMemoryFormat.Get_QuantityMaxWeeklyWork: Smallint;
begin
    Result := DefaultInterface.QuantityMaxWeeklyWork;
end;

procedure TMemoryFormat.Set_QuantityMaxMonthlyWork(pRetVal: Smallint);
begin
  DefaultInterface.Set_QuantityMaxMonthlyWork(pRetVal);
end;

function TMemoryFormat.Get_QuantityMaxMonthlyWork: Smallint;
begin
    Result := DefaultInterface.QuantityMaxMonthlyWork;
end;

procedure TMemoryFormat.Set_QuantityMaxPeriodicWork(pRetVal: Smallint);
begin
  DefaultInterface.Set_QuantityMaxPeriodicWork(pRetVal);
end;

function TMemoryFormat.Get_QuantityMaxPeriodicWork: Smallint;
begin
    Result := DefaultInterface.QuantityMaxPeriodicWork;
end;

procedure TMemoryFormat.Set_QuantityMaxAlarmRing(pRetVal: Byte);
begin
  DefaultInterface.Set_QuantityMaxAlarmRing(pRetVal);
end;

function TMemoryFormat.Get_QuantityMaxAlarmRing: Byte;
begin
    Result := DefaultInterface.QuantityMaxAlarmRing;
end;

procedure TMemoryFormat.Set_QuantityMaxShiftTable(pRetVal: Byte);
begin
  DefaultInterface.Set_QuantityMaxShiftTable(pRetVal);
end;

function TMemoryFormat.Get_QuantityMaxShiftTable: Byte;
begin
    Result := DefaultInterface.QuantityMaxShiftTable;
end;

procedure TMemoryFormat.Set_QuantityMaxHoliday(pRetVal: Byte);
begin
  DefaultInterface.Set_QuantityMaxHoliday(pRetVal);
end;

function TMemoryFormat.Get_QuantityMaxHoliday: Byte;
begin
    Result := DefaultInterface.QuantityMaxHoliday;
end;

procedure TMemoryFormat.Set_QuantityMaxFunction(pRetVal: Byte);
begin
  DefaultInterface.Set_QuantityMaxFunction(pRetVal);
end;

function TMemoryFormat.Get_QuantityMaxFunction: Byte;
begin
    Result := DefaultInterface.QuantityMaxFunction;
end;

procedure TMemoryFormat.Set_MaxMessageUser(pRetVal: Byte);
begin
  DefaultInterface.Set_MaxMessageUser(pRetVal);
end;

function TMemoryFormat.Get_MaxMessageUser: Byte;
begin
    Result := DefaultInterface.MaxMessageUser;
end;

procedure TMemoryFormat.Set_TypeCheck(pRetVal: TypeCheckCard);
begin
  DefaultInterface.Set_TypeCheck(pRetVal);
end;

function TMemoryFormat.Get_TypeCheck: TypeCheckCard;
begin
    Result := DefaultInterface.TypeCheck;
end;

function TMemoryFormat.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TMemoryFormat.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TMemoryFormat.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TMemoryFormatProperties.Create(AServer: TMemoryFormat);
begin
  inherited Create;
  FServer := AServer;
end;

function TMemoryFormatProperties.GetDefaultInterface: _MemoryFormat;
begin
  Result := FServer.DefaultInterface;
end;

function TMemoryFormatProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TMemoryFormatProperties.Set_MinDigitCard(pRetVal: Byte);
begin
  DefaultInterface.Set_MinDigitCard(pRetVal);
end;

function TMemoryFormatProperties.Get_MinDigitCard: Byte;
begin
    Result := DefaultInterface.MinDigitCard;
end;

procedure TMemoryFormatProperties.Set_MaxDigitCard(pRetVal: Byte);
begin
  DefaultInterface.Set_MaxDigitCard(pRetVal);
end;

function TMemoryFormatProperties.Get_MaxDigitCard: Byte;
begin
    Result := DefaultInterface.MaxDigitCard;
end;

procedure TMemoryFormatProperties.Set_HasWork(pRetVal: WordBool);
begin
  DefaultInterface.Set_HasWork(pRetVal);
end;

function TMemoryFormatProperties.Get_HasWork: WordBool;
begin
    Result := DefaultInterface.HasWork;
end;

procedure TMemoryFormatProperties.Set_HasMessage(pRetVal: WordBool);
begin
  DefaultInterface.Set_HasMessage(pRetVal);
end;

function TMemoryFormatProperties.Get_HasMessage: WordBool;
begin
    Result := DefaultInterface.HasMessage;
end;

procedure TMemoryFormatProperties.Set_HasWay(pRetVal: WordBool);
begin
  DefaultInterface.Set_HasWay(pRetVal);
end;

function TMemoryFormatProperties.Get_HasWay: WordBool;
begin
    Result := DefaultInterface.HasWay;
end;

procedure TMemoryFormatProperties.Set_HasPassword(pRetVal: WordBool);
begin
  DefaultInterface.Set_HasPassword(pRetVal);
end;

function TMemoryFormatProperties.Get_HasPassword: WordBool;
begin
    Result := DefaultInterface.HasPassword;
end;

procedure TMemoryFormatProperties.Set_CounterAccess(pRetVal: Byte);
begin
  DefaultInterface.Set_CounterAccess(pRetVal);
end;

function TMemoryFormatProperties.Get_CounterAccess: Byte;
begin
    Result := DefaultInterface.CounterAccess;
end;

procedure TMemoryFormatProperties.Set_QuantityMaxCard(pRetVal: Smallint);
begin
  DefaultInterface.Set_QuantityMaxCard(pRetVal);
end;

function TMemoryFormatProperties.Get_QuantityMaxCard: Smallint;
begin
    Result := DefaultInterface.QuantityMaxCard;
end;

procedure TMemoryFormatProperties.Set_QuantityMaxAlternativeId(pRetVal: Smallint);
begin
  DefaultInterface.Set_QuantityMaxAlternativeId(pRetVal);
end;

function TMemoryFormatProperties.Get_QuantityMaxAlternativeId: Smallint;
begin
    Result := DefaultInterface.QuantityMaxAlternativeId;
end;

procedure TMemoryFormatProperties.Set_QuantityMaxWeeklyWork(pRetVal: Smallint);
begin
  DefaultInterface.Set_QuantityMaxWeeklyWork(pRetVal);
end;

function TMemoryFormatProperties.Get_QuantityMaxWeeklyWork: Smallint;
begin
    Result := DefaultInterface.QuantityMaxWeeklyWork;
end;

procedure TMemoryFormatProperties.Set_QuantityMaxMonthlyWork(pRetVal: Smallint);
begin
  DefaultInterface.Set_QuantityMaxMonthlyWork(pRetVal);
end;

function TMemoryFormatProperties.Get_QuantityMaxMonthlyWork: Smallint;
begin
    Result := DefaultInterface.QuantityMaxMonthlyWork;
end;

procedure TMemoryFormatProperties.Set_QuantityMaxPeriodicWork(pRetVal: Smallint);
begin
  DefaultInterface.Set_QuantityMaxPeriodicWork(pRetVal);
end;

function TMemoryFormatProperties.Get_QuantityMaxPeriodicWork: Smallint;
begin
    Result := DefaultInterface.QuantityMaxPeriodicWork;
end;

procedure TMemoryFormatProperties.Set_QuantityMaxAlarmRing(pRetVal: Byte);
begin
  DefaultInterface.Set_QuantityMaxAlarmRing(pRetVal);
end;

function TMemoryFormatProperties.Get_QuantityMaxAlarmRing: Byte;
begin
    Result := DefaultInterface.QuantityMaxAlarmRing;
end;

procedure TMemoryFormatProperties.Set_QuantityMaxShiftTable(pRetVal: Byte);
begin
  DefaultInterface.Set_QuantityMaxShiftTable(pRetVal);
end;

function TMemoryFormatProperties.Get_QuantityMaxShiftTable: Byte;
begin
    Result := DefaultInterface.QuantityMaxShiftTable;
end;

procedure TMemoryFormatProperties.Set_QuantityMaxHoliday(pRetVal: Byte);
begin
  DefaultInterface.Set_QuantityMaxHoliday(pRetVal);
end;

function TMemoryFormatProperties.Get_QuantityMaxHoliday: Byte;
begin
    Result := DefaultInterface.QuantityMaxHoliday;
end;

procedure TMemoryFormatProperties.Set_QuantityMaxFunction(pRetVal: Byte);
begin
  DefaultInterface.Set_QuantityMaxFunction(pRetVal);
end;

function TMemoryFormatProperties.Get_QuantityMaxFunction: Byte;
begin
    Result := DefaultInterface.QuantityMaxFunction;
end;

procedure TMemoryFormatProperties.Set_MaxMessageUser(pRetVal: Byte);
begin
  DefaultInterface.Set_MaxMessageUser(pRetVal);
end;

function TMemoryFormatProperties.Get_MaxMessageUser: Byte;
begin
    Result := DefaultInterface.MaxMessageUser;
end;

procedure TMemoryFormatProperties.Set_TypeCheck(pRetVal: TypeCheckCard);
begin
  DefaultInterface.Set_TypeCheck(pRetVal);
end;

function TMemoryFormatProperties.Get_TypeCheck: TypeCheckCard;
begin
    Result := DefaultInterface.TypeCheck;
end;

{$ENDIF}

class function CoPrintPointFingerPrintMessage.Create: _PrintPointFingerPrintMessage;
begin
  Result := CreateComObject(CLASS_PrintPointFingerPrintMessage) as _PrintPointFingerPrintMessage;
end;

class function CoPrintPointFingerPrintMessage.CreateRemote(const MachineName: string): _PrintPointFingerPrintMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_PrintPointFingerPrintMessage) as _PrintPointFingerPrintMessage;
end;

class function CoBioPointStatusMessage.Create: _BioPointStatusMessage;
begin
  Result := CreateComObject(CLASS_BioPointStatusMessage) as _BioPointStatusMessage;
end;

class function CoBioPointStatusMessage.CreateRemote(const MachineName: string): _BioPointStatusMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_BioPointStatusMessage) as _BioPointStatusMessage;
end;

procedure TBioPointStatusMessage.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{0395C187-7E52-49A5-A22C-537504A2E1D2}';
    IntfIID:   '{67DE066A-5095-34D2-B29F-F36E0C07311A}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TBioPointStatusMessage.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _BioPointStatusMessage;
  end;
end;

procedure TBioPointStatusMessage.ConnectTo(svrIntf: _BioPointStatusMessage);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TBioPointStatusMessage.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TBioPointStatusMessage.GetDefaultInterface: _BioPointStatusMessage;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TBioPointStatusMessage.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TBioPointStatusMessageProperties.Create(Self);
{$ENDIF}
end;

destructor TBioPointStatusMessage.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TBioPointStatusMessage.GetServerProperties: TBioPointStatusMessageProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TBioPointStatusMessage.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TBioPointStatusMessage.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TBioPointStatusMessage.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

function TBioPointStatusMessage.Get_firmwareVersion: WideString;
begin
    Result := DefaultInterface.firmwareVersion;
end;

procedure TBioPointStatusMessage.Set_firmwareVersion(const pRetVal: WideString);
  { Warning: The property firmwareVersion has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.firmwareVersion := pRetVal;
end;

function TBioPointStatusMessage.Get_RecordsSize: LongWord;
begin
    Result := DefaultInterface.RecordsSize;
end;

procedure TBioPointStatusMessage.Set_RecordsSize(pRetVal: LongWord);
begin
  DefaultInterface.Set_RecordsSize(pRetVal);
end;

function TBioPointStatusMessage.Get_RecordsCount: LongWord;
begin
    Result := DefaultInterface.RecordsCount;
end;

procedure TBioPointStatusMessage.Set_RecordsCount(pRetVal: LongWord);
begin
  DefaultInterface.Set_RecordsCount(pRetVal);
end;

function TBioPointStatusMessage.Get_CardListSize: LongWord;
begin
    Result := DefaultInterface.CardListSize;
end;

procedure TBioPointStatusMessage.Set_CardListSize(pRetVal: LongWord);
begin
  DefaultInterface.Set_CardListSize(pRetVal);
end;

function TBioPointStatusMessage.Get_date: TDateTime;
begin
    Result := DefaultInterface.date;
end;

procedure TBioPointStatusMessage.Set_date(pRetVal: TDateTime);
begin
  DefaultInterface.Set_date(pRetVal);
end;

function TBioPointStatusMessage.Get_CheckType: Byte;
begin
    Result := DefaultInterface.CheckType;
end;

procedure TBioPointStatusMessage.Set_CheckType(pRetVal: Byte);
begin
  DefaultInterface.Set_CheckType(pRetVal);
end;

function TBioPointStatusMessage.Get_RecordDeniedAccess: WordBool;
begin
    Result := DefaultInterface.RecordDeniedAccess;
end;

procedure TBioPointStatusMessage.Set_RecordDeniedAccess(pRetVal: WordBool);
begin
  DefaultInterface.Set_RecordDeniedAccess(pRetVal);
end;

function TBioPointStatusMessage.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TBioPointStatusMessage.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TBioPointStatusMessage.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

function TBioPointStatusMessage.GetData: PSafeArray;
begin
  Result := DefaultInterface.GetData;
end;

function TBioPointStatusMessage.GetSize: Integer;
begin
  Result := DefaultInterface.GetSize;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TBioPointStatusMessageProperties.Create(AServer: TBioPointStatusMessage);
begin
  inherited Create;
  FServer := AServer;
end;

function TBioPointStatusMessageProperties.GetDefaultInterface: _BioPointStatusMessage;
begin
  Result := FServer.DefaultInterface;
end;

function TBioPointStatusMessageProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TBioPointStatusMessageProperties.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TBioPointStatusMessageProperties.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

function TBioPointStatusMessageProperties.Get_firmwareVersion: WideString;
begin
    Result := DefaultInterface.firmwareVersion;
end;

procedure TBioPointStatusMessageProperties.Set_firmwareVersion(const pRetVal: WideString);
  { Warning: The property firmwareVersion has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.firmwareVersion := pRetVal;
end;

function TBioPointStatusMessageProperties.Get_RecordsSize: LongWord;
begin
    Result := DefaultInterface.RecordsSize;
end;

procedure TBioPointStatusMessageProperties.Set_RecordsSize(pRetVal: LongWord);
begin
  DefaultInterface.Set_RecordsSize(pRetVal);
end;

function TBioPointStatusMessageProperties.Get_RecordsCount: LongWord;
begin
    Result := DefaultInterface.RecordsCount;
end;

procedure TBioPointStatusMessageProperties.Set_RecordsCount(pRetVal: LongWord);
begin
  DefaultInterface.Set_RecordsCount(pRetVal);
end;

function TBioPointStatusMessageProperties.Get_CardListSize: LongWord;
begin
    Result := DefaultInterface.CardListSize;
end;

procedure TBioPointStatusMessageProperties.Set_CardListSize(pRetVal: LongWord);
begin
  DefaultInterface.Set_CardListSize(pRetVal);
end;

function TBioPointStatusMessageProperties.Get_date: TDateTime;
begin
    Result := DefaultInterface.date;
end;

procedure TBioPointStatusMessageProperties.Set_date(pRetVal: TDateTime);
begin
  DefaultInterface.Set_date(pRetVal);
end;

function TBioPointStatusMessageProperties.Get_CheckType: Byte;
begin
    Result := DefaultInterface.CheckType;
end;

procedure TBioPointStatusMessageProperties.Set_CheckType(pRetVal: Byte);
begin
  DefaultInterface.Set_CheckType(pRetVal);
end;

function TBioPointStatusMessageProperties.Get_RecordDeniedAccess: WordBool;
begin
    Result := DefaultInterface.RecordDeniedAccess;
end;

procedure TBioPointStatusMessageProperties.Set_RecordDeniedAccess(pRetVal: WordBool);
begin
  DefaultInterface.Set_RecordDeniedAccess(pRetVal);
end;

{$ENDIF}

class function CoFaceEmployee.Create: _FaceEmployee;
begin
  Result := CreateComObject(CLASS_FaceEmployee) as _FaceEmployee;
end;

class function CoFaceEmployee.CreateRemote(const MachineName: string): _FaceEmployee;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_FaceEmployee) as _FaceEmployee;
end;

procedure TFaceEmployee.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{D9065E15-E643-4AAF-BECB-EA307B5EF536}';
    IntfIID:   '{8C0B6C5B-7411-3189-AE2E-31DC47B9F801}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TFaceEmployee.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _FaceEmployee;
  end;
end;

procedure TFaceEmployee.ConnectTo(svrIntf: _FaceEmployee);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TFaceEmployee.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TFaceEmployee.GetDefaultInterface: _FaceEmployee;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TFaceEmployee.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TFaceEmployeeProperties.Create(Self);
{$ENDIF}
end;

destructor TFaceEmployee.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TFaceEmployee.GetServerProperties: TFaceEmployeeProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TFaceEmployee.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TFaceEmployee.Get_employeeID: Integer;
begin
    Result := DefaultInterface.employeeID;
end;

procedure TFaceEmployee.Set_employeeID(pRetVal: Integer);
begin
  DefaultInterface.Set_employeeID(pRetVal);
end;

function TFaceEmployee.Get_name: WideString;
begin
    Result := DefaultInterface.name;
end;

procedure TFaceEmployee.Set_name(const pRetVal: WideString);
  { Warning: The property name has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.name := pRetVal;
end;

function TFaceEmployee.Get_password: WideString;
begin
    Result := DefaultInterface.password;
end;

procedure TFaceEmployee.Set_password(const pRetVal: WideString);
  { Warning: The property password has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.password := pRetVal;
end;

function TFaceEmployee.Get_Credential: WideString;
begin
    Result := DefaultInterface.Credential;
end;

procedure TFaceEmployee.Set_Credential(const pRetVal: WideString);
  { Warning: The property Credential has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Credential := pRetVal;
end;

function TFaceEmployee.Get_isMaster: WordBool;
begin
    Result := DefaultInterface.isMaster;
end;

procedure TFaceEmployee.Set_isMaster(pRetVal: WordBool);
begin
  DefaultInterface.Set_isMaster(pRetVal);
end;

function TFaceEmployee.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TFaceEmployee.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TFaceEmployee.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TFaceEmployeeProperties.Create(AServer: TFaceEmployee);
begin
  inherited Create;
  FServer := AServer;
end;

function TFaceEmployeeProperties.GetDefaultInterface: _FaceEmployee;
begin
  Result := FServer.DefaultInterface;
end;

function TFaceEmployeeProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TFaceEmployeeProperties.Get_employeeID: Integer;
begin
    Result := DefaultInterface.employeeID;
end;

procedure TFaceEmployeeProperties.Set_employeeID(pRetVal: Integer);
begin
  DefaultInterface.Set_employeeID(pRetVal);
end;

function TFaceEmployeeProperties.Get_name: WideString;
begin
    Result := DefaultInterface.name;
end;

procedure TFaceEmployeeProperties.Set_name(const pRetVal: WideString);
  { Warning: The property name has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.name := pRetVal;
end;

function TFaceEmployeeProperties.Get_password: WideString;
begin
    Result := DefaultInterface.password;
end;

procedure TFaceEmployeeProperties.Set_password(const pRetVal: WideString);
  { Warning: The property password has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.password := pRetVal;
end;

function TFaceEmployeeProperties.Get_Credential: WideString;
begin
    Result := DefaultInterface.Credential;
end;

procedure TFaceEmployeeProperties.Set_Credential(const pRetVal: WideString);
  { Warning: The property Credential has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Credential := pRetVal;
end;

function TFaceEmployeeProperties.Get_isMaster: WordBool;
begin
    Result := DefaultInterface.isMaster;
end;

procedure TFaceEmployeeProperties.Set_isMaster(pRetVal: WordBool);
begin
  DefaultInterface.Set_isMaster(pRetVal);
end;

{$ENDIF}

class function CoHoliday.Create: _Holiday;
begin
  Result := CreateComObject(CLASS_Holiday) as _Holiday;
end;

class function CoHoliday.CreateRemote(const MachineName: string): _Holiday;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Holiday) as _Holiday;
end;

procedure THoliday.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{9064A618-FC6D-4D50-8C10-AC131CF2B320}';
    IntfIID:   '{8AF993AB-1F73-3772-B3B2-B2AB09B689F8}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure THoliday.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _Holiday;
  end;
end;

procedure THoliday.ConnectTo(svrIntf: _Holiday);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure THoliday.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function THoliday.GetDefaultInterface: _Holiday;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor THoliday.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := THolidayProperties.Create(Self);
{$ENDIF}
end;

destructor THoliday.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function THoliday.GetServerProperties: THolidayProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function THoliday.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure THoliday.Set_day(pRetVal: Byte);
begin
  DefaultInterface.Set_day(pRetVal);
end;

function THoliday.Get_day: Byte;
begin
    Result := DefaultInterface.day;
end;

procedure THoliday.Set_month(pRetVal: Byte);
begin
  DefaultInterface.Set_month(pRetVal);
end;

function THoliday.Get_month: Byte;
begin
    Result := DefaultInterface.month;
end;

function THoliday.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function THoliday.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function THoliday.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor THolidayProperties.Create(AServer: THoliday);
begin
  inherited Create;
  FServer := AServer;
end;

function THolidayProperties.GetDefaultInterface: _Holiday;
begin
  Result := FServer.DefaultInterface;
end;

function THolidayProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure THolidayProperties.Set_day(pRetVal: Byte);
begin
  DefaultInterface.Set_day(pRetVal);
end;

function THolidayProperties.Get_day: Byte;
begin
    Result := DefaultInterface.day;
end;

procedure THolidayProperties.Set_month(pRetVal: Byte);
begin
  DefaultInterface.Set_month(pRetVal);
end;

function THolidayProperties.Get_month: Byte;
begin
    Result := DefaultInterface.month;
end;

{$ENDIF}

class function CoRandomNumberResponse.Create: _RandomNumberResponse;
begin
  Result := CreateComObject(CLASS_RandomNumberResponse) as _RandomNumberResponse;
end;

class function CoRandomNumberResponse.CreateRemote(const MachineName: string): _RandomNumberResponse;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_RandomNumberResponse) as _RandomNumberResponse;
end;

class function CoPrintPointStatusMessage.Create: _PrintPointStatusMessage;
begin
  Result := CreateComObject(CLASS_PrintPointStatusMessage) as _PrintPointStatusMessage;
end;

class function CoPrintPointStatusMessage.CreateRemote(const MachineName: string): _PrintPointStatusMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_PrintPointStatusMessage) as _PrintPointStatusMessage;
end;

class function CoPrintPointEvent.Create: _PrintPointEvent;
begin
  Result := CreateComObject(CLASS_PrintPointEvent) as _PrintPointEvent;
end;

class function CoPrintPointEvent.CreateRemote(const MachineName: string): _PrintPointEvent;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_PrintPointEvent) as _PrintPointEvent;
end;

class function CoPrintPointLiEmployee.Create: _PrintPointLiEmployee;
begin
  Result := CreateComObject(CLASS_PrintPointLiEmployee) as _PrintPointLiEmployee;
end;

class function CoPrintPointLiEmployee.CreateRemote(const MachineName: string): _PrintPointLiEmployee;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_PrintPointLiEmployee) as _PrintPointLiEmployee;
end;

procedure TPrintPointLiEmployee.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{B3EE107D-25E1-4C57-B2ED-A2F277C473B0}';
    IntfIID:   '{F4BC0867-964B-31E6-925B-09F087E712FF}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TPrintPointLiEmployee.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _PrintPointLiEmployee;
  end;
end;

procedure TPrintPointLiEmployee.ConnectTo(svrIntf: _PrintPointLiEmployee);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TPrintPointLiEmployee.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TPrintPointLiEmployee.GetDefaultInterface: _PrintPointLiEmployee;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TPrintPointLiEmployee.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TPrintPointLiEmployeeProperties.Create(Self);
{$ENDIF}
end;

destructor TPrintPointLiEmployee.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TPrintPointLiEmployee.GetServerProperties: TPrintPointLiEmployeeProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TPrintPointLiEmployee.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TPrintPointLiEmployee.Get_pis: WideString;
begin
    Result := DefaultInterface.pis;
end;

procedure TPrintPointLiEmployee.Set_pis(const pRetVal: WideString);
  { Warning: The property pis has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.pis := pRetVal;
end;

function TPrintPointLiEmployee.Get_name: WideString;
begin
    Result := DefaultInterface.name;
end;

procedure TPrintPointLiEmployee.Set_name(const pRetVal: WideString);
  { Warning: The property name has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.name := pRetVal;
end;

function TPrintPointLiEmployee.Get_password: WideString;
begin
    Result := DefaultInterface.password;
end;

procedure TPrintPointLiEmployee.Set_password(const pRetVal: WideString);
  { Warning: The property password has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.password := pRetVal;
end;

function TPrintPointLiEmployee.Get_CostCenter: Integer;
begin
    Result := DefaultInterface.CostCenter;
end;

procedure TPrintPointLiEmployee.Set_CostCenter(pRetVal: Integer);
begin
  DefaultInterface.Set_CostCenter(pRetVal);
end;

function TPrintPointLiEmployee.Get_employeeID: Integer;
begin
    Result := DefaultInterface.employeeID;
end;

procedure TPrintPointLiEmployee.Set_employeeID(pRetVal: Integer);
begin
  DefaultInterface.Set_employeeID(pRetVal);
end;

function TPrintPointLiEmployee.Get_Name_2: WideString;
begin
    Result := DefaultInterface.Name_2;
end;

procedure TPrintPointLiEmployee.Set_Name_2(const pRetVal: WideString);
  { Warning: The property Name_2 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Name_2 := pRetVal;
end;

function TPrintPointLiEmployee.Get_Password_2: WideString;
begin
    Result := DefaultInterface.Password_2;
end;

procedure TPrintPointLiEmployee.Set_Password_2(const pRetVal: WideString);
  { Warning: The property Password_2 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Password_2 := pRetVal;
end;

function TPrintPointLiEmployee.Get_Credential: WideString;
begin
    Result := DefaultInterface.Credential;
end;

procedure TPrintPointLiEmployee.Set_Credential(const pRetVal: WideString);
  { Warning: The property Credential has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Credential := pRetVal;
end;

function TPrintPointLiEmployee.Get_cpf: WideString;
begin
    Result := DefaultInterface.cpf;
end;

procedure TPrintPointLiEmployee.Set_cpf(const pRetVal: WideString);
  { Warning: The property cpf has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.cpf := pRetVal;
end;

function TPrintPointLiEmployee.Get_isMaster: WordBool;
begin
    Result := DefaultInterface.isMaster;
end;

procedure TPrintPointLiEmployee.Set_isMaster(pRetVal: WordBool);
begin
  DefaultInterface.Set_isMaster(pRetVal);
end;

function TPrintPointLiEmployee.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TPrintPointLiEmployee.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TPrintPointLiEmployee.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

function TPrintPointLiEmployee.CompareTo(obj: OleVariant): Integer;
begin
  Result := DefaultInterface.CompareTo(obj);
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TPrintPointLiEmployeeProperties.Create(AServer: TPrintPointLiEmployee);
begin
  inherited Create;
  FServer := AServer;
end;

function TPrintPointLiEmployeeProperties.GetDefaultInterface: _PrintPointLiEmployee;
begin
  Result := FServer.DefaultInterface;
end;

function TPrintPointLiEmployeeProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TPrintPointLiEmployeeProperties.Get_pis: WideString;
begin
    Result := DefaultInterface.pis;
end;

procedure TPrintPointLiEmployeeProperties.Set_pis(const pRetVal: WideString);
  { Warning: The property pis has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.pis := pRetVal;
end;

function TPrintPointLiEmployeeProperties.Get_name: WideString;
begin
    Result := DefaultInterface.name;
end;

procedure TPrintPointLiEmployeeProperties.Set_name(const pRetVal: WideString);
  { Warning: The property name has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.name := pRetVal;
end;

function TPrintPointLiEmployeeProperties.Get_password: WideString;
begin
    Result := DefaultInterface.password;
end;

procedure TPrintPointLiEmployeeProperties.Set_password(const pRetVal: WideString);
  { Warning: The property password has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.password := pRetVal;
end;

function TPrintPointLiEmployeeProperties.Get_CostCenter: Integer;
begin
    Result := DefaultInterface.CostCenter;
end;

procedure TPrintPointLiEmployeeProperties.Set_CostCenter(pRetVal: Integer);
begin
  DefaultInterface.Set_CostCenter(pRetVal);
end;

function TPrintPointLiEmployeeProperties.Get_employeeID: Integer;
begin
    Result := DefaultInterface.employeeID;
end;

procedure TPrintPointLiEmployeeProperties.Set_employeeID(pRetVal: Integer);
begin
  DefaultInterface.Set_employeeID(pRetVal);
end;

function TPrintPointLiEmployeeProperties.Get_Name_2: WideString;
begin
    Result := DefaultInterface.Name_2;
end;

procedure TPrintPointLiEmployeeProperties.Set_Name_2(const pRetVal: WideString);
  { Warning: The property Name_2 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Name_2 := pRetVal;
end;

function TPrintPointLiEmployeeProperties.Get_Password_2: WideString;
begin
    Result := DefaultInterface.Password_2;
end;

procedure TPrintPointLiEmployeeProperties.Set_Password_2(const pRetVal: WideString);
  { Warning: The property Password_2 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Password_2 := pRetVal;
end;

function TPrintPointLiEmployeeProperties.Get_Credential: WideString;
begin
    Result := DefaultInterface.Credential;
end;

procedure TPrintPointLiEmployeeProperties.Set_Credential(const pRetVal: WideString);
  { Warning: The property Credential has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Credential := pRetVal;
end;

function TPrintPointLiEmployeeProperties.Get_cpf: WideString;
begin
    Result := DefaultInterface.cpf;
end;

procedure TPrintPointLiEmployeeProperties.Set_cpf(const pRetVal: WideString);
  { Warning: The property cpf has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.cpf := pRetVal;
end;

function TPrintPointLiEmployeeProperties.Get_isMaster: WordBool;
begin
    Result := DefaultInterface.isMaster;
end;

procedure TPrintPointLiEmployeeProperties.Set_isMaster(pRetVal: WordBool);
begin
  DefaultInterface.Set_isMaster(pRetVal);
end;

{$ENDIF}

class function CoParcialConfiguration.Create: _ParcialConfiguration;
begin
  Result := CreateComObject(CLASS_ParcialConfiguration) as _ParcialConfiguration;
end;

class function CoParcialConfiguration.CreateRemote(const MachineName: string): _ParcialConfiguration;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_ParcialConfiguration) as _ParcialConfiguration;
end;

procedure TParcialConfiguration.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{775A6F41-1E79-49D2-84DF-A7C4FA5304B9}';
    IntfIID:   '{8C562630-EDE1-31D7-BADF-93A8D27DCE2A}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TParcialConfiguration.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _ParcialConfiguration;
  end;
end;

procedure TParcialConfiguration.ConnectTo(svrIntf: _ParcialConfiguration);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TParcialConfiguration.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TParcialConfiguration.GetDefaultInterface: _ParcialConfiguration;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TParcialConfiguration.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TParcialConfigurationProperties.Create(Self);
{$ENDIF}
end;

destructor TParcialConfiguration.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TParcialConfiguration.GetServerProperties: TParcialConfigurationProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TParcialConfiguration.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TParcialConfiguration.Get_configurationType: ParcialConfigurationType;
begin
    Result := DefaultInterface.configurationType;
end;

procedure TParcialConfiguration.Set_configurationType(pRetVal: ParcialConfigurationType);
begin
  DefaultInterface.Set_configurationType(pRetVal);
end;

function TParcialConfiguration.Get_field1: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field1;
end;

procedure TParcialConfiguration._Set_field1(pRetVal: OleVariant);
  { Warning: The property field1 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field1 := pRetVal;
end;

function TParcialConfiguration.Get_field2: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field2;
end;

procedure TParcialConfiguration._Set_field2(pRetVal: OleVariant);
  { Warning: The property field2 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field2 := pRetVal;
end;

function TParcialConfiguration.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TParcialConfiguration.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TParcialConfiguration.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

procedure TParcialConfiguration.ValidateFieldParameters;
begin
  DefaultInterface.ValidateFieldParameters;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TParcialConfigurationProperties.Create(AServer: TParcialConfiguration);
begin
  inherited Create;
  FServer := AServer;
end;

function TParcialConfigurationProperties.GetDefaultInterface: _ParcialConfiguration;
begin
  Result := FServer.DefaultInterface;
end;

function TParcialConfigurationProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TParcialConfigurationProperties.Get_configurationType: ParcialConfigurationType;
begin
    Result := DefaultInterface.configurationType;
end;

procedure TParcialConfigurationProperties.Set_configurationType(pRetVal: ParcialConfigurationType);
begin
  DefaultInterface.Set_configurationType(pRetVal);
end;

function TParcialConfigurationProperties.Get_field1: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field1;
end;

procedure TParcialConfigurationProperties._Set_field1(pRetVal: OleVariant);
  { Warning: The property field1 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field1 := pRetVal;
end;

function TParcialConfigurationProperties.Get_field2: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field2;
end;

procedure TParcialConfigurationProperties._Set_field2(pRetVal: OleVariant);
  { Warning: The property field2 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field2 := pRetVal;
end;

{$ENDIF}

class function CoMRPRecord_SettingRealTimeClock.Create: _MRPRecord_SettingRealTimeClock;
begin
  Result := CreateComObject(CLASS_MRPRecord_SettingRealTimeClock) as _MRPRecord_SettingRealTimeClock;
end;

class function CoMRPRecord_SettingRealTimeClock.CreateRemote(const MachineName: string): _MRPRecord_SettingRealTimeClock;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_MRPRecord_SettingRealTimeClock) as _MRPRecord_SettingRealTimeClock;
end;

class function CoMRPRecord_RegistrationMarkingPoint.Create: _MRPRecord_RegistrationMarkingPoint;
begin
  Result := CreateComObject(CLASS_MRPRecord_RegistrationMarkingPoint) as _MRPRecord_RegistrationMarkingPoint;
end;

class function CoMRPRecord_RegistrationMarkingPoint.CreateRemote(const MachineName: string): _MRPRecord_RegistrationMarkingPoint;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_MRPRecord_RegistrationMarkingPoint) as _MRPRecord_RegistrationMarkingPoint;
end;

class function CoCardCollection.Create: _CardCollection;
begin
  Result := CreateComObject(CLASS_CardCollection) as _CardCollection;
end;

class function CoCardCollection.CreateRemote(const MachineName: string): _CardCollection;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_CardCollection) as _CardCollection;
end;

procedure TCardCollection.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{D558A317-4775-4633-9281-98EA2AF1744F}';
    IntfIID:   '{0F9FDA08-1DA7-36A1-A088-FCB452386331}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TCardCollection.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _CardCollection;
  end;
end;

procedure TCardCollection.ConnectTo(svrIntf: _CardCollection);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TCardCollection.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TCardCollection.GetDefaultInterface: _CardCollection;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TCardCollection.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TCardCollectionProperties.Create(Self);
{$ENDIF}
end;

destructor TCardCollection.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TCardCollection.GetServerProperties: TCardCollectionProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TCardCollection.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TCardCollection.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TCardCollection.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TCardCollection.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

procedure TCardCollection.Add(const Card: _Card);
begin
  DefaultInterface.Add(Card);
end;

function TCardCollection.Count: Integer;
begin
  Result := DefaultInterface.Count;
end;

procedure TCardCollection.Remove(const Card: _Card);
begin
  DefaultInterface.Remove(Card);
end;

procedure TCardCollection.Clear;
begin
  DefaultInterface.Clear;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TCardCollectionProperties.Create(AServer: TCardCollection);
begin
  inherited Create;
  FServer := AServer;
end;

function TCardCollectionProperties.GetDefaultInterface: _CardCollection;
begin
  Result := FServer.DefaultInterface;
end;

function TCardCollectionProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

{$ENDIF}

class function CoHardwareTestCollectionResponse.Create: _HardwareTestCollectionResponse;
begin
  Result := CreateComObject(CLASS_HardwareTestCollectionResponse) as _HardwareTestCollectionResponse;
end;

class function CoHardwareTestCollectionResponse.CreateRemote(const MachineName: string): _HardwareTestCollectionResponse;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_HardwareTestCollectionResponse) as _HardwareTestCollectionResponse;
end;

class function CoGetMACResponse.Create: _GetMACResponse;
begin
  Result := CreateComObject(CLASS_GetMACResponse) as _GetMACResponse;
end;

class function CoGetMACResponse.CreateRemote(const MachineName: string): _GetMACResponse;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_GetMACResponse) as _GetMACResponse;
end;

class function CoConcretePunchMessage.Create: _ConcretePunchMessage;
begin
  Result := CreateComObject(CLASS_ConcretePunchMessage) as _ConcretePunchMessage;
end;

class function CoConcretePunchMessage.CreateRemote(const MachineName: string): _ConcretePunchMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_ConcretePunchMessage) as _ConcretePunchMessage;
end;

procedure TConcretePunchMessage.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{9D1EB6C4-9054-3C71-A6ED-C937E3A31E74}';
    IntfIID:   '{8A13657B-F830-356D-A578-56EA2BEDF1B7}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TConcretePunchMessage.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _ConcretePunchMessage;
  end;
end;

procedure TConcretePunchMessage.ConnectTo(svrIntf: _ConcretePunchMessage);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TConcretePunchMessage.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TConcretePunchMessage.GetDefaultInterface: _ConcretePunchMessage;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TConcretePunchMessage.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TConcretePunchMessageProperties.Create(Self);
{$ENDIF}
end;

destructor TConcretePunchMessage.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TConcretePunchMessage.GetServerProperties: TConcretePunchMessageProperties;
begin
  Result := FProps;
end;
{$ENDIF}

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TConcretePunchMessageProperties.Create(AServer: TConcretePunchMessage);
begin
  inherited Create;
  FServer := AServer;
end;

function TConcretePunchMessageProperties.GetDefaultInterface: _ConcretePunchMessage;
begin
  Result := FServer.DefaultInterface;
end;

{$ENDIF}

class function CoAbstractMemoryFormat.Create: _AbstractMemoryFormat;
begin
  Result := CreateComObject(CLASS_AbstractMemoryFormat) as _AbstractMemoryFormat;
end;

class function CoAbstractMemoryFormat.CreateRemote(const MachineName: string): _AbstractMemoryFormat;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AbstractMemoryFormat) as _AbstractMemoryFormat;
end;

class function CoBioPointMemoryFormat.Create: _BioPointMemoryFormat;
begin
  Result := CreateComObject(CLASS_BioPointMemoryFormat) as _BioPointMemoryFormat;
end;

class function CoBioPointMemoryFormat.CreateRemote(const MachineName: string): _BioPointMemoryFormat;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_BioPointMemoryFormat) as _BioPointMemoryFormat;
end;

procedure TBioPointMemoryFormat.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{3503D08C-0DA8-445C-A247-1A1BEC98E424}';
    IntfIID:   '{42019683-A593-37DA-B9A6-7E8A30418B63}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TBioPointMemoryFormat.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _BioPointMemoryFormat;
  end;
end;

procedure TBioPointMemoryFormat.ConnectTo(svrIntf: _BioPointMemoryFormat);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TBioPointMemoryFormat.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TBioPointMemoryFormat.GetDefaultInterface: _BioPointMemoryFormat;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TBioPointMemoryFormat.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TBioPointMemoryFormatProperties.Create(Self);
{$ENDIF}
end;

destructor TBioPointMemoryFormat.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TBioPointMemoryFormat.GetServerProperties: TBioPointMemoryFormatProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TBioPointMemoryFormat.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TBioPointMemoryFormat.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TBioPointMemoryFormat.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

procedure TBioPointMemoryFormat._Set_MemoryFormat(const pRetVal: _MemoryFormat);
  { Warning: The property MemoryFormat has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.MemoryFormat := pRetVal;
end;

function TBioPointMemoryFormat.Get_MemoryFormat: _MemoryFormat;
begin
    Result := DefaultInterface.MemoryFormat;
end;

function TBioPointMemoryFormat.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TBioPointMemoryFormat.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TBioPointMemoryFormat.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

function TBioPointMemoryFormat.GetData: PSafeArray;
begin
  Result := DefaultInterface.GetData;
end;

function TBioPointMemoryFormat.GetSize: Integer;
begin
  Result := DefaultInterface.GetSize;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TBioPointMemoryFormatProperties.Create(AServer: TBioPointMemoryFormat);
begin
  inherited Create;
  FServer := AServer;
end;

function TBioPointMemoryFormatProperties.GetDefaultInterface: _BioPointMemoryFormat;
begin
  Result := FServer.DefaultInterface;
end;

function TBioPointMemoryFormatProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TBioPointMemoryFormatProperties.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TBioPointMemoryFormatProperties.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

procedure TBioPointMemoryFormatProperties._Set_MemoryFormat(const pRetVal: _MemoryFormat);
  { Warning: The property MemoryFormat has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.MemoryFormat := pRetVal;
end;

function TBioPointMemoryFormatProperties.Get_MemoryFormat: _MemoryFormat;
begin
    Result := DefaultInterface.MemoryFormat;
end;

{$ENDIF}

class function CoTemplateCollection.Create: _TemplateCollection;
begin
  Result := CreateComObject(CLASS_TemplateCollection) as _TemplateCollection;
end;

class function CoTemplateCollection.CreateRemote(const MachineName: string): _TemplateCollection;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_TemplateCollection) as _TemplateCollection;
end;

procedure TTemplateCollection.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{580074E5-6B1C-420A-BB83-0F9D060BFA86}';
    IntfIID:   '{6ED2CC35-76F4-3EEA-B441-E2229951E9D3}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TTemplateCollection.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _TemplateCollection;
  end;
end;

procedure TTemplateCollection.ConnectTo(svrIntf: _TemplateCollection);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TTemplateCollection.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TTemplateCollection.GetDefaultInterface: _TemplateCollection;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TTemplateCollection.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TTemplateCollectionProperties.Create(Self);
{$ENDIF}
end;

destructor TTemplateCollection.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TTemplateCollection.GetServerProperties: TTemplateCollectionProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TTemplateCollection.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TTemplateCollection.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TTemplateCollection.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TTemplateCollection.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

procedure TTemplateCollection.Add(const myTemplate: _Template);
begin
  DefaultInterface.Add(myTemplate);
end;

function TTemplateCollection.Count: Integer;
begin
  Result := DefaultInterface.Count;
end;

procedure TTemplateCollection.Remove(const myTemplate: _Template);
begin
  DefaultInterface.Remove(myTemplate);
end;

procedure TTemplateCollection.Clear;
begin
  DefaultInterface.Clear;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TTemplateCollectionProperties.Create(AServer: TTemplateCollection);
begin
  inherited Create;
  FServer := AServer;
end;

function TTemplateCollectionProperties.GetDefaultInterface: _TemplateCollection;
begin
  Result := FServer.DefaultInterface;
end;

function TTemplateCollectionProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

{$ENDIF}

class function CoAlarmRings.Create: _AlarmRings;
begin
  Result := CreateComObject(CLASS_AlarmRings) as _AlarmRings;
end;

class function CoAlarmRings.CreateRemote(const MachineName: string): _AlarmRings;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AlarmRings) as _AlarmRings;
end;

procedure TAlarmRings.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{8C727E63-580D-4D5C-B2DE-A49CF67BEDD7}';
    IntfIID:   '{5B8EF58D-140B-3E95-8994-8471170E7FBD}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TAlarmRings.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _AlarmRings;
  end;
end;

procedure TAlarmRings.ConnectTo(svrIntf: _AlarmRings);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TAlarmRings.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TAlarmRings.GetDefaultInterface: _AlarmRings;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TAlarmRings.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TAlarmRingsProperties.Create(Self);
{$ENDIF}
end;

destructor TAlarmRings.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TAlarmRings.GetServerProperties: TAlarmRingsProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TAlarmRings.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TAlarmRings.Set_TypeRing(pRetVal: TypeAlarm);
begin
  DefaultInterface.Set_TypeRing(pRetVal);
end;

function TAlarmRings.Get_TypeRing: TypeAlarm;
begin
    Result := DefaultInterface.TypeRing;
end;

procedure TAlarmRings.Set_Id(pRetVal: Byte);
begin
  DefaultInterface.Set_Id(pRetVal);
end;

function TAlarmRings.Get_Id: Byte;
begin
    Result := DefaultInterface.Id;
end;

procedure TAlarmRings.Set_TimeAlarm(const pRetVal: WideString);
  { Warning: The property TimeAlarm has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.TimeAlarm := pRetVal;
end;

function TAlarmRings.Get_TimeAlarm: WideString;
begin
    Result := DefaultInterface.TimeAlarm;
end;

procedure TAlarmRings.Set_Duration(pRetVal: Byte);
begin
  DefaultInterface.Set_Duration(pRetVal);
end;

function TAlarmRings.Get_Duration: Byte;
begin
    Result := DefaultInterface.Duration;
end;

procedure TAlarmRings.Set_RingSunday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingSunday(pRetVal);
end;

function TAlarmRings.Get_RingSunday: TypeRinging;
begin
    Result := DefaultInterface.RingSunday;
end;

procedure TAlarmRings.Set_RingMonday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingMonday(pRetVal);
end;

function TAlarmRings.Get_RingMonday: TypeRinging;
begin
    Result := DefaultInterface.RingMonday;
end;

procedure TAlarmRings.Set_RingTuesday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingTuesday(pRetVal);
end;

function TAlarmRings.Get_RingTuesday: TypeRinging;
begin
    Result := DefaultInterface.RingTuesday;
end;

procedure TAlarmRings.Set_RingWednesday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingWednesday(pRetVal);
end;

function TAlarmRings.Get_RingWednesday: TypeRinging;
begin
    Result := DefaultInterface.RingWednesday;
end;

procedure TAlarmRings.Set_RingThursday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingThursday(pRetVal);
end;

function TAlarmRings.Get_RingThursday: TypeRinging;
begin
    Result := DefaultInterface.RingThursday;
end;

procedure TAlarmRings.Set_RingFriday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingFriday(pRetVal);
end;

function TAlarmRings.Get_RingFriday: TypeRinging;
begin
    Result := DefaultInterface.RingFriday;
end;

procedure TAlarmRings.Set_RingSaturday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingSaturday(pRetVal);
end;

function TAlarmRings.Get_RingSaturday: TypeRinging;
begin
    Result := DefaultInterface.RingSaturday;
end;

procedure TAlarmRings.Set_RingHoliday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingHoliday(pRetVal);
end;

function TAlarmRings.Get_RingHoliday: TypeRinging;
begin
    Result := DefaultInterface.RingHoliday;
end;

function TAlarmRings.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TAlarmRings.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TAlarmRings.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TAlarmRingsProperties.Create(AServer: TAlarmRings);
begin
  inherited Create;
  FServer := AServer;
end;

function TAlarmRingsProperties.GetDefaultInterface: _AlarmRings;
begin
  Result := FServer.DefaultInterface;
end;

function TAlarmRingsProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TAlarmRingsProperties.Set_TypeRing(pRetVal: TypeAlarm);
begin
  DefaultInterface.Set_TypeRing(pRetVal);
end;

function TAlarmRingsProperties.Get_TypeRing: TypeAlarm;
begin
    Result := DefaultInterface.TypeRing;
end;

procedure TAlarmRingsProperties.Set_Id(pRetVal: Byte);
begin
  DefaultInterface.Set_Id(pRetVal);
end;

function TAlarmRingsProperties.Get_Id: Byte;
begin
    Result := DefaultInterface.Id;
end;

procedure TAlarmRingsProperties.Set_TimeAlarm(const pRetVal: WideString);
  { Warning: The property TimeAlarm has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.TimeAlarm := pRetVal;
end;

function TAlarmRingsProperties.Get_TimeAlarm: WideString;
begin
    Result := DefaultInterface.TimeAlarm;
end;

procedure TAlarmRingsProperties.Set_Duration(pRetVal: Byte);
begin
  DefaultInterface.Set_Duration(pRetVal);
end;

function TAlarmRingsProperties.Get_Duration: Byte;
begin
    Result := DefaultInterface.Duration;
end;

procedure TAlarmRingsProperties.Set_RingSunday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingSunday(pRetVal);
end;

function TAlarmRingsProperties.Get_RingSunday: TypeRinging;
begin
    Result := DefaultInterface.RingSunday;
end;

procedure TAlarmRingsProperties.Set_RingMonday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingMonday(pRetVal);
end;

function TAlarmRingsProperties.Get_RingMonday: TypeRinging;
begin
    Result := DefaultInterface.RingMonday;
end;

procedure TAlarmRingsProperties.Set_RingTuesday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingTuesday(pRetVal);
end;

function TAlarmRingsProperties.Get_RingTuesday: TypeRinging;
begin
    Result := DefaultInterface.RingTuesday;
end;

procedure TAlarmRingsProperties.Set_RingWednesday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingWednesday(pRetVal);
end;

function TAlarmRingsProperties.Get_RingWednesday: TypeRinging;
begin
    Result := DefaultInterface.RingWednesday;
end;

procedure TAlarmRingsProperties.Set_RingThursday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingThursday(pRetVal);
end;

function TAlarmRingsProperties.Get_RingThursday: TypeRinging;
begin
    Result := DefaultInterface.RingThursday;
end;

procedure TAlarmRingsProperties.Set_RingFriday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingFriday(pRetVal);
end;

function TAlarmRingsProperties.Get_RingFriday: TypeRinging;
begin
    Result := DefaultInterface.RingFriday;
end;

procedure TAlarmRingsProperties.Set_RingSaturday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingSaturday(pRetVal);
end;

function TAlarmRingsProperties.Get_RingSaturday: TypeRinging;
begin
    Result := DefaultInterface.RingSaturday;
end;

procedure TAlarmRingsProperties.Set_RingHoliday(pRetVal: TypeRinging);
begin
  DefaultInterface.Set_RingHoliday(pRetVal);
end;

function TAlarmRingsProperties.Get_RingHoliday: TypeRinging;
begin
    Result := DefaultInterface.RingHoliday;
end;

{$ENDIF}

class function CoCredential.Create: _Credential;
begin
  Result := CreateComObject(CLASS_Credential) as _Credential;
end;

class function CoCredential.CreateRemote(const MachineName: string): _Credential;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Credential) as _Credential;
end;

procedure TCredential.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{CD6C8D17-870B-4BFA-A537-3C859572B16B}';
    IntfIID:   '{F100D95F-1C0D-3AA3-B69D-6FA223062BDF}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TCredential.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _Credential;
  end;
end;

procedure TCredential.ConnectTo(svrIntf: _Credential);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TCredential.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TCredential.GetDefaultInterface: _Credential;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TCredential.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TCredentialProperties.Create(Self);
{$ENDIF}
end;

destructor TCredential.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TCredential.GetServerProperties: TCredentialProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TCredential.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TCredential.Get_cardCode: WideString;
begin
    Result := DefaultInterface.cardCode;
end;

procedure TCredential.Set_cardCode(const pRetVal: WideString);
  { Warning: The property cardCode has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.cardCode := pRetVal;
end;

function TCredential.Get_pis: WideString;
begin
    Result := DefaultInterface.pis;
end;

procedure TCredential.Set_pis(const pRetVal: WideString);
  { Warning: The property pis has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.pis := pRetVal;
end;

function TCredential.Get_version: Byte;
begin
    Result := DefaultInterface.version;
end;

procedure TCredential.Set_version(pRetVal: Byte);
begin
  DefaultInterface.Set_version(pRetVal);
end;

function TCredential.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TCredential.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TCredential.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

function TCredential.CompareTo(obj: OleVariant): Integer;
begin
  Result := DefaultInterface.CompareTo(obj);
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TCredentialProperties.Create(AServer: TCredential);
begin
  inherited Create;
  FServer := AServer;
end;

function TCredentialProperties.GetDefaultInterface: _Credential;
begin
  Result := FServer.DefaultInterface;
end;

function TCredentialProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TCredentialProperties.Get_cardCode: WideString;
begin
    Result := DefaultInterface.cardCode;
end;

procedure TCredentialProperties.Set_cardCode(const pRetVal: WideString);
  { Warning: The property cardCode has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.cardCode := pRetVal;
end;

function TCredentialProperties.Get_pis: WideString;
begin
    Result := DefaultInterface.pis;
end;

procedure TCredentialProperties.Set_pis(const pRetVal: WideString);
  { Warning: The property pis has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.pis := pRetVal;
end;

function TCredentialProperties.Get_version: Byte;
begin
    Result := DefaultInterface.version;
end;

procedure TCredentialProperties.Set_version(pRetVal: Byte);
begin
  DefaultInterface.Set_version(pRetVal);
end;

{$ENDIF}

class function CoConfiguration.Create: _Configuration;
begin
  Result := CreateComObject(CLASS_Configuration) as _Configuration;
end;

class function CoConfiguration.CreateRemote(const MachineName: string): _Configuration;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Configuration) as _Configuration;
end;

procedure TConfiguration.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{3706A057-2C50-478A-AA31-66CA1A9EC8B1}';
    IntfIID:   '{87EB9D12-8EF8-30DE-8CB7-BE72F7E6E7D4}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TConfiguration.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _Configuration;
  end;
end;

procedure TConfiguration.ConnectTo(svrIntf: _Configuration);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TConfiguration.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TConfiguration.GetDefaultInterface: _Configuration;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TConfiguration.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TConfigurationProperties.Create(Self);
{$ENDIF}
end;

destructor TConfiguration.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TConfiguration.GetServerProperties: TConfigurationProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TConfiguration.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TConfiguration.Get_configurationType: EConfigurationType;
begin
    Result := DefaultInterface.configurationType;
end;

procedure TConfiguration.Set_configurationType(pRetVal: EConfigurationType);
begin
  DefaultInterface.Set_configurationType(pRetVal);
end;

function TConfiguration.Get_field1: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field1;
end;

procedure TConfiguration._Set_field1(pRetVal: OleVariant);
  { Warning: The property field1 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field1 := pRetVal;
end;

function TConfiguration.Get_field2: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field2;
end;

procedure TConfiguration._Set_field2(pRetVal: OleVariant);
  { Warning: The property field2 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field2 := pRetVal;
end;

function TConfiguration.Get_field3: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field3;
end;

procedure TConfiguration._Set_field3(pRetVal: OleVariant);
  { Warning: The property field3 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field3 := pRetVal;
end;

function TConfiguration.Get_field4: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field4;
end;

procedure TConfiguration._Set_field4(pRetVal: OleVariant);
  { Warning: The property field4 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field4 := pRetVal;
end;

function TConfiguration.Get_field5: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field5;
end;

procedure TConfiguration._Set_field5(pRetVal: OleVariant);
  { Warning: The property field5 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field5 := pRetVal;
end;

function TConfiguration.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TConfiguration.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TConfiguration.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

procedure TConfiguration.ValidateFieldParameters;
begin
  DefaultInterface.ValidateFieldParameters;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TConfigurationProperties.Create(AServer: TConfiguration);
begin
  inherited Create;
  FServer := AServer;
end;

function TConfigurationProperties.GetDefaultInterface: _Configuration;
begin
  Result := FServer.DefaultInterface;
end;

function TConfigurationProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TConfigurationProperties.Get_configurationType: EConfigurationType;
begin
    Result := DefaultInterface.configurationType;
end;

procedure TConfigurationProperties.Set_configurationType(pRetVal: EConfigurationType);
begin
  DefaultInterface.Set_configurationType(pRetVal);
end;

function TConfigurationProperties.Get_field1: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field1;
end;

procedure TConfigurationProperties._Set_field1(pRetVal: OleVariant);
  { Warning: The property field1 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field1 := pRetVal;
end;

function TConfigurationProperties.Get_field2: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field2;
end;

procedure TConfigurationProperties._Set_field2(pRetVal: OleVariant);
  { Warning: The property field2 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field2 := pRetVal;
end;

function TConfigurationProperties.Get_field3: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field3;
end;

procedure TConfigurationProperties._Set_field3(pRetVal: OleVariant);
  { Warning: The property field3 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field3 := pRetVal;
end;

function TConfigurationProperties.Get_field4: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field4;
end;

procedure TConfigurationProperties._Set_field4(pRetVal: OleVariant);
  { Warning: The property field4 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field4 := pRetVal;
end;

function TConfigurationProperties.Get_field5: OleVariant;
var
  InterfaceVariant : OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  Result := InterfaceVariant.field5;
end;

procedure TConfigurationProperties._Set_field5(pRetVal: OleVariant);
  { Warning: The property field5 has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.field5 := pRetVal;
end;

{$ENDIF}

class function CoInvalidMessageException.Create: _InvalidMessageException;
begin
  Result := CreateComObject(CLASS_InvalidMessageException) as _InvalidMessageException;
end;

class function CoInvalidMessageException.CreateRemote(const MachineName: string): _InvalidMessageException;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_InvalidMessageException) as _InvalidMessageException;
end;

class function CoFaceStatus.Create: _FaceStatus;
begin
  Result := CreateComObject(CLASS_FaceStatus) as _FaceStatus;
end;

class function CoFaceStatus.CreateRemote(const MachineName: string): _FaceStatus;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_FaceStatus) as _FaceStatus;
end;

procedure TFaceStatus.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{B5F1FC66-4BA9-4C58-9864-9AC0E1DF677A}';
    IntfIID:   '{01D965E2-CC3E-3F0E-B9D1-4DD9D8699376}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TFaceStatus.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _FaceStatus;
  end;
end;

procedure TFaceStatus.ConnectTo(svrIntf: _FaceStatus);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TFaceStatus.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TFaceStatus.GetDefaultInterface: _FaceStatus;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TFaceStatus.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TFaceStatusProperties.Create(Self);
{$ENDIF}
end;

destructor TFaceStatus.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TFaceStatus.GetServerProperties: TFaceStatusProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TFaceStatus.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TFaceStatus.Get_RecordsCapacity: Integer;
begin
    Result := DefaultInterface.RecordsCapacity;
end;

function TFaceStatus.Get_RecordsOccupation: Integer;
begin
    Result := DefaultInterface.RecordsOccupation;
end;

function TFaceStatus.Get_FingerprintCapacity: Integer;
begin
    Result := DefaultInterface.FingerprintCapacity;
end;

function TFaceStatus.Get_UsersCapacity: Integer;
begin
    Result := DefaultInterface.UsersCapacity;
end;

function TFaceStatus.Get_UsersOccupation: Integer;
begin
    Result := DefaultInterface.UsersOccupation;
end;

function TFaceStatus.Get_MasterOccupation: Integer;
begin
    Result := DefaultInterface.MasterOccupation;
end;

function TFaceStatus.Get_PasswordOccupation: Integer;
begin
    Result := DefaultInterface.PasswordOccupation;
end;

function TFaceStatus.Get_FingerprintOccupation: Integer;
begin
    Result := DefaultInterface.FingerprintOccupation;
end;

function TFaceStatus.Get_firmwareVersion: WideString;
begin
    Result := DefaultInterface.firmwareVersion;
end;

function TFaceStatus.Get_DeviceDateTime: TDateTime;
begin
    Result := DefaultInterface.DeviceDateTime;
end;

function TFaceStatus.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TFaceStatus.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TFaceStatus.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TFaceStatusProperties.Create(AServer: TFaceStatus);
begin
  inherited Create;
  FServer := AServer;
end;

function TFaceStatusProperties.GetDefaultInterface: _FaceStatus;
begin
  Result := FServer.DefaultInterface;
end;

function TFaceStatusProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TFaceStatusProperties.Get_RecordsCapacity: Integer;
begin
    Result := DefaultInterface.RecordsCapacity;
end;

function TFaceStatusProperties.Get_RecordsOccupation: Integer;
begin
    Result := DefaultInterface.RecordsOccupation;
end;

function TFaceStatusProperties.Get_FingerprintCapacity: Integer;
begin
    Result := DefaultInterface.FingerprintCapacity;
end;

function TFaceStatusProperties.Get_UsersCapacity: Integer;
begin
    Result := DefaultInterface.UsersCapacity;
end;

function TFaceStatusProperties.Get_UsersOccupation: Integer;
begin
    Result := DefaultInterface.UsersOccupation;
end;

function TFaceStatusProperties.Get_MasterOccupation: Integer;
begin
    Result := DefaultInterface.MasterOccupation;
end;

function TFaceStatusProperties.Get_PasswordOccupation: Integer;
begin
    Result := DefaultInterface.PasswordOccupation;
end;

function TFaceStatusProperties.Get_FingerprintOccupation: Integer;
begin
    Result := DefaultInterface.FingerprintOccupation;
end;

function TFaceStatusProperties.Get_firmwareVersion: WideString;
begin
    Result := DefaultInterface.firmwareVersion;
end;

function TFaceStatusProperties.Get_DeviceDateTime: TDateTime;
begin
    Result := DefaultInterface.DeviceDateTime;
end;

{$ENDIF}

class function CoAbstractFactory.Create: _AbstractFactory;
begin
  Result := CreateComObject(CLASS_AbstractFactory) as _AbstractFactory;
end;

class function CoAbstractFactory.CreateRemote(const MachineName: string): _AbstractFactory;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AbstractFactory) as _AbstractFactory;
end;

class function CoHolidayCollection.Create: _HolidayCollection;
begin
  Result := CreateComObject(CLASS_HolidayCollection) as _HolidayCollection;
end;

class function CoHolidayCollection.CreateRemote(const MachineName: string): _HolidayCollection;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_HolidayCollection) as _HolidayCollection;
end;

procedure THolidayCollection.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{7AA4642F-D9A2-4AF3-A3B0-597FDE303AB3}';
    IntfIID:   '{0800B5DB-DA43-39A1-855C-B72D5620FFB2}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure THolidayCollection.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _HolidayCollection;
  end;
end;

procedure THolidayCollection.ConnectTo(svrIntf: _HolidayCollection);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure THolidayCollection.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function THolidayCollection.GetDefaultInterface: _HolidayCollection;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor THolidayCollection.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := THolidayCollectionProperties.Create(Self);
{$ENDIF}
end;

destructor THolidayCollection.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function THolidayCollection.GetServerProperties: THolidayCollectionProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function THolidayCollection.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function THolidayCollection.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function THolidayCollection.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function THolidayCollection.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

procedure THolidayCollection.Add(const Holiday: _Holiday);
begin
  DefaultInterface.Add(Holiday);
end;

function THolidayCollection.Count: Integer;
begin
  Result := DefaultInterface.Count;
end;

procedure THolidayCollection.Remove(const Holiday: _Holiday);
begin
  DefaultInterface.Remove(Holiday);
end;

procedure THolidayCollection.Clear;
begin
  DefaultInterface.Clear;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor THolidayCollectionProperties.Create(AServer: THolidayCollection);
begin
  inherited Create;
  FServer := AServer;
end;

function THolidayCollectionProperties.GetDefaultInterface: _HolidayCollection;
begin
  Result := FServer.DefaultInterface;
end;

function THolidayCollectionProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

{$ENDIF}

class function CoWeeklyJourneyWorking.Create: _WeeklyJourneyWorking;
begin
  Result := CreateComObject(CLASS_WeeklyJourneyWorking) as _WeeklyJourneyWorking;
end;

class function CoWeeklyJourneyWorking.CreateRemote(const MachineName: string): _WeeklyJourneyWorking;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_WeeklyJourneyWorking) as _WeeklyJourneyWorking;
end;

procedure TWeeklyJourneyWorking.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{178117DF-1E72-4A5A-8F5F-8515F8A6CF64}';
    IntfIID:   '{59D43438-1A3B-3374-9E63-6C09423865B1}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TWeeklyJourneyWorking.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _WeeklyJourneyWorking;
  end;
end;

procedure TWeeklyJourneyWorking.ConnectTo(svrIntf: _WeeklyJourneyWorking);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TWeeklyJourneyWorking.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TWeeklyJourneyWorking.GetDefaultInterface: _WeeklyJourneyWorking;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TWeeklyJourneyWorking.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TWeeklyJourneyWorkingProperties.Create(Self);
{$ENDIF}
end;

destructor TWeeklyJourneyWorking.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TWeeklyJourneyWorking.GetServerProperties: TWeeklyJourneyWorkingProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TWeeklyJourneyWorking.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TWeeklyJourneyWorking.Set_Id(pRetVal: Smallint);
begin
  DefaultInterface.Set_Id(pRetVal);
end;

function TWeeklyJourneyWorking.Get_Id: Smallint;
begin
    Result := DefaultInterface.Id;
end;

procedure TWeeklyJourneyWorking.Set_TypeWorking(pRetVal: TypeWorking);
begin
  DefaultInterface.Set_TypeWorking(pRetVal);
end;

function TWeeklyJourneyWorking.Get_TypeWorking: TypeWorking;
begin
    Result := DefaultInterface.TypeWorking;
end;

procedure TWeeklyJourneyWorking.Set_Sunday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Sunday(pRetVal);
end;

function TWeeklyJourneyWorking.Get_Sunday: Smallint;
begin
    Result := DefaultInterface.Sunday;
end;

procedure TWeeklyJourneyWorking.Set_Monday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Monday(pRetVal);
end;

function TWeeklyJourneyWorking.Get_Monday: Smallint;
begin
    Result := DefaultInterface.Monday;
end;

procedure TWeeklyJourneyWorking.Set_Tuesday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Tuesday(pRetVal);
end;

function TWeeklyJourneyWorking.Get_Tuesday: Smallint;
begin
    Result := DefaultInterface.Tuesday;
end;

procedure TWeeklyJourneyWorking.Set_Wednesday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Wednesday(pRetVal);
end;

function TWeeklyJourneyWorking.Get_Wednesday: Smallint;
begin
    Result := DefaultInterface.Wednesday;
end;

procedure TWeeklyJourneyWorking.Set_Thursday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Thursday(pRetVal);
end;

function TWeeklyJourneyWorking.Get_Thursday: Smallint;
begin
    Result := DefaultInterface.Thursday;
end;

procedure TWeeklyJourneyWorking.Set_Friday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Friday(pRetVal);
end;

function TWeeklyJourneyWorking.Get_Friday: Smallint;
begin
    Result := DefaultInterface.Friday;
end;

procedure TWeeklyJourneyWorking.Set_Saturday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Saturday(pRetVal);
end;

function TWeeklyJourneyWorking.Get_Saturday: Smallint;
begin
    Result := DefaultInterface.Saturday;
end;

function TWeeklyJourneyWorking.Get_Holiday: Smallint;
begin
    Result := DefaultInterface.Holiday;
end;

procedure TWeeklyJourneyWorking.Set_Holiday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Holiday(pRetVal);
end;

function TWeeklyJourneyWorking.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TWeeklyJourneyWorking.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TWeeklyJourneyWorking.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TWeeklyJourneyWorkingProperties.Create(AServer: TWeeklyJourneyWorking);
begin
  inherited Create;
  FServer := AServer;
end;

function TWeeklyJourneyWorkingProperties.GetDefaultInterface: _WeeklyJourneyWorking;
begin
  Result := FServer.DefaultInterface;
end;

function TWeeklyJourneyWorkingProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TWeeklyJourneyWorkingProperties.Set_Id(pRetVal: Smallint);
begin
  DefaultInterface.Set_Id(pRetVal);
end;

function TWeeklyJourneyWorkingProperties.Get_Id: Smallint;
begin
    Result := DefaultInterface.Id;
end;

procedure TWeeklyJourneyWorkingProperties.Set_TypeWorking(pRetVal: TypeWorking);
begin
  DefaultInterface.Set_TypeWorking(pRetVal);
end;

function TWeeklyJourneyWorkingProperties.Get_TypeWorking: TypeWorking;
begin
    Result := DefaultInterface.TypeWorking;
end;

procedure TWeeklyJourneyWorkingProperties.Set_Sunday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Sunday(pRetVal);
end;

function TWeeklyJourneyWorkingProperties.Get_Sunday: Smallint;
begin
    Result := DefaultInterface.Sunday;
end;

procedure TWeeklyJourneyWorkingProperties.Set_Monday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Monday(pRetVal);
end;

function TWeeklyJourneyWorkingProperties.Get_Monday: Smallint;
begin
    Result := DefaultInterface.Monday;
end;

procedure TWeeklyJourneyWorkingProperties.Set_Tuesday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Tuesday(pRetVal);
end;

function TWeeklyJourneyWorkingProperties.Get_Tuesday: Smallint;
begin
    Result := DefaultInterface.Tuesday;
end;

procedure TWeeklyJourneyWorkingProperties.Set_Wednesday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Wednesday(pRetVal);
end;

function TWeeklyJourneyWorkingProperties.Get_Wednesday: Smallint;
begin
    Result := DefaultInterface.Wednesday;
end;

procedure TWeeklyJourneyWorkingProperties.Set_Thursday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Thursday(pRetVal);
end;

function TWeeklyJourneyWorkingProperties.Get_Thursday: Smallint;
begin
    Result := DefaultInterface.Thursday;
end;

procedure TWeeklyJourneyWorkingProperties.Set_Friday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Friday(pRetVal);
end;

function TWeeklyJourneyWorkingProperties.Get_Friday: Smallint;
begin
    Result := DefaultInterface.Friday;
end;

procedure TWeeklyJourneyWorkingProperties.Set_Saturday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Saturday(pRetVal);
end;

function TWeeklyJourneyWorkingProperties.Get_Saturday: Smallint;
begin
    Result := DefaultInterface.Saturday;
end;

function TWeeklyJourneyWorkingProperties.Get_Holiday: Smallint;
begin
    Result := DefaultInterface.Holiday;
end;

procedure TWeeklyJourneyWorkingProperties.Set_Holiday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Holiday(pRetVal);
end;

{$ENDIF}

class function CoImmediateStatusResponse.Create: _ImmediateStatusResponse;
begin
  Result := CreateComObject(CLASS_ImmediateStatusResponse) as _ImmediateStatusResponse;
end;

class function CoImmediateStatusResponse.CreateRemote(const MachineName: string): _ImmediateStatusResponse;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_ImmediateStatusResponse) as _ImmediateStatusResponse;
end;

class function CoPrintPointMRPEventLog.Create: _PrintPointMRPEventLog;
begin
  Result := CreateComObject(CLASS_PrintPointMRPEventLog) as _PrintPointMRPEventLog;
end;

class function CoPrintPointMRPEventLog.CreateRemote(const MachineName: string): _PrintPointMRPEventLog;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_PrintPointMRPEventLog) as _PrintPointMRPEventLog;
end;

class function CoMaster.Create: _Master;
begin
  Result := CreateComObject(CLASS_Master) as _Master;
end;

class function CoMaster.CreateRemote(const MachineName: string): _Master;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Master) as _Master;
end;

procedure TMaster.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{16B43E66-3E3B-44A1-A26A-4CFFA1264CC6}';
    IntfIID:   '{5F1A4777-C778-330C-AE36-477874E8E1D4}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TMaster.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _Master;
  end;
end;

procedure TMaster.ConnectTo(svrIntf: _Master);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TMaster.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TMaster.GetDefaultInterface: _Master;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TMaster.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TMasterProperties.Create(Self);
{$ENDIF}
end;

destructor TMaster.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TMaster.GetServerProperties: TMasterProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TMaster.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TMaster.Get_pis: WideString;
begin
    Result := DefaultInterface.pis;
end;

procedure TMaster.Set_pis(const pRetVal: WideString);
  { Warning: The property pis has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.pis := pRetVal;
end;

function TMaster.Get_Card: WideString;
begin
    Result := DefaultInterface.Card;
end;

procedure TMaster.Set_Card(const pRetVal: WideString);
  { Warning: The property Card has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Card := pRetVal;
end;

function TMaster.Get_password: WideString;
begin
    Result := DefaultInterface.password;
end;

procedure TMaster.Set_password(const pRetVal: WideString);
  { Warning: The property password has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.password := pRetVal;
end;

function TMaster.Get_hasTechniquesProgrammingPermission: WordBool;
begin
    Result := DefaultInterface.hasTechniquesProgrammingPermission;
end;

procedure TMaster.Set_hasTechniquesProgrammingPermission(pRetVal: WordBool);
begin
  DefaultInterface.Set_hasTechniquesProgrammingPermission(pRetVal);
end;

function TMaster.Get_hasDataAndTimePermission: WordBool;
begin
    Result := DefaultInterface.hasDataAndTimePermission;
end;

procedure TMaster.Set_hasDataAndTimePermission(pRetVal: WordBool);
begin
  DefaultInterface.Set_hasDataAndTimePermission(pRetVal);
end;

function TMaster.Get_HasPenDriveProgrammingPermission: WordBool;
begin
    Result := DefaultInterface.HasPenDriveProgrammingPermission;
end;

procedure TMaster.Set_HasPenDriveProgrammingPermission(pRetVal: WordBool);
begin
  DefaultInterface.Set_HasPenDriveProgrammingPermission(pRetVal);
end;

function TMaster.Get_HasBobbinChangePermission: WordBool;
begin
    Result := DefaultInterface.HasBobbinChangePermission;
end;

procedure TMaster.Set_HasBobbinChangePermission(pRetVal: WordBool);
begin
  DefaultInterface.Set_HasBobbinChangePermission(pRetVal);
end;

function TMaster.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TMaster.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TMaster.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TMasterProperties.Create(AServer: TMaster);
begin
  inherited Create;
  FServer := AServer;
end;

function TMasterProperties.GetDefaultInterface: _Master;
begin
  Result := FServer.DefaultInterface;
end;

function TMasterProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TMasterProperties.Get_pis: WideString;
begin
    Result := DefaultInterface.pis;
end;

procedure TMasterProperties.Set_pis(const pRetVal: WideString);
  { Warning: The property pis has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.pis := pRetVal;
end;

function TMasterProperties.Get_Card: WideString;
begin
    Result := DefaultInterface.Card;
end;

procedure TMasterProperties.Set_Card(const pRetVal: WideString);
  { Warning: The property Card has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Card := pRetVal;
end;

function TMasterProperties.Get_password: WideString;
begin
    Result := DefaultInterface.password;
end;

procedure TMasterProperties.Set_password(const pRetVal: WideString);
  { Warning: The property password has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.password := pRetVal;
end;

function TMasterProperties.Get_hasTechniquesProgrammingPermission: WordBool;
begin
    Result := DefaultInterface.hasTechniquesProgrammingPermission;
end;

procedure TMasterProperties.Set_hasTechniquesProgrammingPermission(pRetVal: WordBool);
begin
  DefaultInterface.Set_hasTechniquesProgrammingPermission(pRetVal);
end;

function TMasterProperties.Get_hasDataAndTimePermission: WordBool;
begin
    Result := DefaultInterface.hasDataAndTimePermission;
end;

procedure TMasterProperties.Set_hasDataAndTimePermission(pRetVal: WordBool);
begin
  DefaultInterface.Set_hasDataAndTimePermission(pRetVal);
end;

function TMasterProperties.Get_HasPenDriveProgrammingPermission: WordBool;
begin
    Result := DefaultInterface.HasPenDriveProgrammingPermission;
end;

procedure TMasterProperties.Set_HasPenDriveProgrammingPermission(pRetVal: WordBool);
begin
  DefaultInterface.Set_HasPenDriveProgrammingPermission(pRetVal);
end;

function TMasterProperties.Get_HasBobbinChangePermission: WordBool;
begin
    Result := DefaultInterface.HasBobbinChangePermission;
end;

procedure TMasterProperties.Set_HasBobbinChangePermission(pRetVal: WordBool);
begin
  DefaultInterface.Set_HasBobbinChangePermission(pRetVal);
end;

{$ENDIF}

class function CoPrintPointEmployerMessage.Create: _PrintPointEmployerMessage;
begin
  Result := CreateComObject(CLASS_PrintPointEmployerMessage) as _PrintPointEmployerMessage;
end;

class function CoPrintPointEmployerMessage.CreateRemote(const MachineName: string): _PrintPointEmployerMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_PrintPointEmployerMessage) as _PrintPointEmployerMessage;
end;

procedure TPrintPointEmployerMessage.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{5437F9D0-F0D9-4B4B-BCC6-229396BAC2D1}';
    IntfIID:   '{BA9CC9F9-0BE1-3BB5-A34F-C43AE506B0E1}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TPrintPointEmployerMessage.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _PrintPointEmployerMessage;
  end;
end;

procedure TPrintPointEmployerMessage.ConnectTo(svrIntf: _PrintPointEmployerMessage);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TPrintPointEmployerMessage.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TPrintPointEmployerMessage.GetDefaultInterface: _PrintPointEmployerMessage;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TPrintPointEmployerMessage.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TPrintPointEmployerMessageProperties.Create(Self);
{$ENDIF}
end;

destructor TPrintPointEmployerMessage.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TPrintPointEmployerMessage.GetServerProperties: TPrintPointEmployerMessageProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TPrintPointEmployerMessage.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TPrintPointEmployerMessage.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TPrintPointEmployerMessage.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

function TPrintPointEmployerMessage.Get_employerType: EmployeerType;
begin
    Result := DefaultInterface.employerType;
end;

procedure TPrintPointEmployerMessage.Set_employerType(pRetVal: EmployeerType);
begin
  DefaultInterface.Set_employerType(pRetVal);
end;

function TPrintPointEmployerMessage.Get_cpf_cnpj: WideString;
begin
    Result := DefaultInterface.cpf_cnpj;
end;

procedure TPrintPointEmployerMessage.Set_cpf_cnpj(const pRetVal: WideString);
  { Warning: The property cpf_cnpj has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.cpf_cnpj := pRetVal;
end;

function TPrintPointEmployerMessage.Get_cei: WideString;
begin
    Result := DefaultInterface.cei;
end;

procedure TPrintPointEmployerMessage.Set_cei(const pRetVal: WideString);
  { Warning: The property cei has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.cei := pRetVal;
end;

function TPrintPointEmployerMessage.Get_name: WideString;
begin
    Result := DefaultInterface.name;
end;

procedure TPrintPointEmployerMessage.Set_name(const pRetVal: WideString);
  { Warning: The property name has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.name := pRetVal;
end;

function TPrintPointEmployerMessage.Get_address: WideString;
begin
    Result := DefaultInterface.address;
end;

procedure TPrintPointEmployerMessage.Set_address(const pRetVal: WideString);
  { Warning: The property address has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.address := pRetVal;
end;

function TPrintPointEmployerMessage.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TPrintPointEmployerMessage.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TPrintPointEmployerMessage.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

function TPrintPointEmployerMessage.GetData: PSafeArray;
begin
  Result := DefaultInterface.GetData;
end;

function TPrintPointEmployerMessage.GetSize: Integer;
begin
  Result := DefaultInterface.GetSize;
end;

function TPrintPointEmployerMessage.MakeEmployeerFile(const repSerialNumber: WideString; 
                                                      const windowTitle: WideString; 
                                                      const path: WideString): WordBool;
begin
  Result := DefaultInterface.MakeEmployeerFile(repSerialNumber, windowTitle, path);
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TPrintPointEmployerMessageProperties.Create(AServer: TPrintPointEmployerMessage);
begin
  inherited Create;
  FServer := AServer;
end;

function TPrintPointEmployerMessageProperties.GetDefaultInterface: _PrintPointEmployerMessage;
begin
  Result := FServer.DefaultInterface;
end;

function TPrintPointEmployerMessageProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TPrintPointEmployerMessageProperties.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TPrintPointEmployerMessageProperties.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

function TPrintPointEmployerMessageProperties.Get_employerType: EmployeerType;
begin
    Result := DefaultInterface.employerType;
end;

procedure TPrintPointEmployerMessageProperties.Set_employerType(pRetVal: EmployeerType);
begin
  DefaultInterface.Set_employerType(pRetVal);
end;

function TPrintPointEmployerMessageProperties.Get_cpf_cnpj: WideString;
begin
    Result := DefaultInterface.cpf_cnpj;
end;

procedure TPrintPointEmployerMessageProperties.Set_cpf_cnpj(const pRetVal: WideString);
  { Warning: The property cpf_cnpj has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.cpf_cnpj := pRetVal;
end;

function TPrintPointEmployerMessageProperties.Get_cei: WideString;
begin
    Result := DefaultInterface.cei;
end;

procedure TPrintPointEmployerMessageProperties.Set_cei(const pRetVal: WideString);
  { Warning: The property cei has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.cei := pRetVal;
end;

function TPrintPointEmployerMessageProperties.Get_name: WideString;
begin
    Result := DefaultInterface.name;
end;

procedure TPrintPointEmployerMessageProperties.Set_name(const pRetVal: WideString);
  { Warning: The property name has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.name := pRetVal;
end;

function TPrintPointEmployerMessageProperties.Get_address: WideString;
begin
    Result := DefaultInterface.address;
end;

procedure TPrintPointEmployerMessageProperties.Set_address(const pRetVal: WideString);
  { Warning: The property address has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.address := pRetVal;
end;

{$ENDIF}

class function CoMicropointCardList.Create: _MicropointCardList;
begin
  Result := CreateComObject(CLASS_MicropointCardList) as _MicropointCardList;
end;

class function CoMicropointCardList.CreateRemote(const MachineName: string): _MicropointCardList;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_MicropointCardList) as _MicropointCardList;
end;

procedure TMicropointCardList.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{57FCBA2E-E11F-4479-B203-AE1A7339912A}';
    IntfIID:   '{D694DE74-E8C8-3F70-927C-F9F8913ABA7B}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TMicropointCardList.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _MicropointCardList;
  end;
end;

procedure TMicropointCardList.ConnectTo(svrIntf: _MicropointCardList);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TMicropointCardList.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TMicropointCardList.GetDefaultInterface: _MicropointCardList;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TMicropointCardList.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TMicropointCardListProperties.Create(Self);
{$ENDIF}
end;

destructor TMicropointCardList.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TMicropointCardList.GetServerProperties: TMicropointCardListProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TMicropointCardList.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TMicropointCardList.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TMicropointCardList.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

procedure TMicropointCardList._Set_Card(const pRetVal: _Card);
  { Warning: The property Card has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Card := pRetVal;
end;

function TMicropointCardList.Get_Card: _Card;
begin
    Result := DefaultInterface.Card;
end;

function TMicropointCardList.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TMicropointCardList.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TMicropointCardList.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

function TMicropointCardList.GetData: PSafeArray;
begin
  Result := DefaultInterface.GetData;
end;

function TMicropointCardList.GetSize: Integer;
begin
  Result := DefaultInterface.GetSize;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TMicropointCardListProperties.Create(AServer: TMicropointCardList);
begin
  inherited Create;
  FServer := AServer;
end;

function TMicropointCardListProperties.GetDefaultInterface: _MicropointCardList;
begin
  Result := FServer.DefaultInterface;
end;

function TMicropointCardListProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TMicropointCardListProperties.Set_LenghtMessage(pRetVal: Integer);
begin
  DefaultInterface.Set_LenghtMessage(pRetVal);
end;

function TMicropointCardListProperties.Get_LenghtMessage: Integer;
begin
    Result := DefaultInterface.LenghtMessage;
end;

procedure TMicropointCardListProperties._Set_Card(const pRetVal: _Card);
  { Warning: The property Card has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Card := pRetVal;
end;

function TMicropointCardListProperties.Get_Card: _Card;
begin
    Result := DefaultInterface.Card;
end;

{$ENDIF}

class function CoShiftTableCollection.Create: _ShiftTableCollection;
begin
  Result := CreateComObject(CLASS_ShiftTableCollection) as _ShiftTableCollection;
end;

class function CoShiftTableCollection.CreateRemote(const MachineName: string): _ShiftTableCollection;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_ShiftTableCollection) as _ShiftTableCollection;
end;

procedure TShiftTableCollection.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{F3E7E810-A66D-4F08-9B3F-56342C57A064}';
    IntfIID:   '{6D1ABFB6-695D-33E5-A803-0039BEF6AE5D}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TShiftTableCollection.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _ShiftTableCollection;
  end;
end;

procedure TShiftTableCollection.ConnectTo(svrIntf: _ShiftTableCollection);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TShiftTableCollection.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TShiftTableCollection.GetDefaultInterface: _ShiftTableCollection;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TShiftTableCollection.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TShiftTableCollectionProperties.Create(Self);
{$ENDIF}
end;

destructor TShiftTableCollection.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TShiftTableCollection.GetServerProperties: TShiftTableCollectionProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TShiftTableCollection.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TShiftTableCollection.Get_Id: Smallint;
begin
    Result := DefaultInterface.Id;
end;

procedure TShiftTableCollection.Set_Id(pRetVal: Smallint);
begin
  DefaultInterface.Set_Id(pRetVal);
end;

function TShiftTableCollection.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TShiftTableCollection.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TShiftTableCollection.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

procedure TShiftTableCollection.Add(const faixa: _ShiftTable);
begin
  DefaultInterface.Add(faixa);
end;

function TShiftTableCollection.Count: Integer;
begin
  Result := DefaultInterface.Count;
end;

procedure TShiftTableCollection.Remove(const faixa: _ShiftTable);
begin
  DefaultInterface.Remove(faixa);
end;

procedure TShiftTableCollection.Clear;
begin
  DefaultInterface.Clear;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TShiftTableCollectionProperties.Create(AServer: TShiftTableCollection);
begin
  inherited Create;
  FServer := AServer;
end;

function TShiftTableCollectionProperties.GetDefaultInterface: _ShiftTableCollection;
begin
  Result := FServer.DefaultInterface;
end;

function TShiftTableCollectionProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TShiftTableCollectionProperties.Get_Id: Smallint;
begin
    Result := DefaultInterface.Id;
end;

procedure TShiftTableCollectionProperties.Set_Id(pRetVal: Smallint);
begin
  DefaultInterface.Set_Id(pRetVal);
end;

{$ENDIF}

class function CoMonthlyJourneyWorking.Create: _MonthlyJourneyWorking;
begin
  Result := CreateComObject(CLASS_MonthlyJourneyWorking) as _MonthlyJourneyWorking;
end;

class function CoMonthlyJourneyWorking.CreateRemote(const MachineName: string): _MonthlyJourneyWorking;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_MonthlyJourneyWorking) as _MonthlyJourneyWorking;
end;

procedure TMonthlyJourneyWorking.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{66256A35-652D-444D-90D7-6DCBCFEA633C}';
    IntfIID:   '{CDBBE17C-1B38-3483-9A45-3DA2FB57EA5E}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TMonthlyJourneyWorking.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _MonthlyJourneyWorking;
  end;
end;

procedure TMonthlyJourneyWorking.ConnectTo(svrIntf: _MonthlyJourneyWorking);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TMonthlyJourneyWorking.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TMonthlyJourneyWorking.GetDefaultInterface: _MonthlyJourneyWorking;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TMonthlyJourneyWorking.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TMonthlyJourneyWorkingProperties.Create(Self);
{$ENDIF}
end;

destructor TMonthlyJourneyWorking.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TMonthlyJourneyWorking.GetServerProperties: TMonthlyJourneyWorkingProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TMonthlyJourneyWorking.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TMonthlyJourneyWorking.Set_Id(pRetVal: Smallint);
begin
  DefaultInterface.Set_Id(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Id: Smallint;
begin
    Result := DefaultInterface.Id;
end;

procedure TMonthlyJourneyWorking.Set_TypeWorking(pRetVal: TypeWorking);
begin
  DefaultInterface.Set_TypeWorking(pRetVal);
end;

function TMonthlyJourneyWorking.Get_TypeWorking: TypeWorking;
begin
    Result := DefaultInterface.TypeWorking;
end;

function TMonthlyJourneyWorking.Get_Dia01: Smallint;
begin
    Result := DefaultInterface.Dia01;
end;

procedure TMonthlyJourneyWorking.Set_Dia01(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia01(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia02: Smallint;
begin
    Result := DefaultInterface.Dia02;
end;

procedure TMonthlyJourneyWorking.Set_Dia02(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia02(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia03: Smallint;
begin
    Result := DefaultInterface.Dia03;
end;

procedure TMonthlyJourneyWorking.Set_Dia03(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia03(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia04: Smallint;
begin
    Result := DefaultInterface.Dia04;
end;

procedure TMonthlyJourneyWorking.Set_Dia04(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia04(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia05: Smallint;
begin
    Result := DefaultInterface.Dia05;
end;

procedure TMonthlyJourneyWorking.Set_Dia05(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia05(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia06: Smallint;
begin
    Result := DefaultInterface.Dia06;
end;

procedure TMonthlyJourneyWorking.Set_Dia06(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia06(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia07: Smallint;
begin
    Result := DefaultInterface.Dia07;
end;

procedure TMonthlyJourneyWorking.Set_Dia07(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia07(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia08: Smallint;
begin
    Result := DefaultInterface.Dia08;
end;

procedure TMonthlyJourneyWorking.Set_Dia08(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia08(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia09: Smallint;
begin
    Result := DefaultInterface.Dia09;
end;

procedure TMonthlyJourneyWorking.Set_Dia09(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia09(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia10: Smallint;
begin
    Result := DefaultInterface.Dia10;
end;

procedure TMonthlyJourneyWorking.Set_Dia10(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia10(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia11: Smallint;
begin
    Result := DefaultInterface.Dia11;
end;

procedure TMonthlyJourneyWorking.Set_Dia11(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia11(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia12: Smallint;
begin
    Result := DefaultInterface.Dia12;
end;

procedure TMonthlyJourneyWorking.Set_Dia12(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia12(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia13: Smallint;
begin
    Result := DefaultInterface.Dia13;
end;

procedure TMonthlyJourneyWorking.Set_Dia13(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia13(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia14: Smallint;
begin
    Result := DefaultInterface.Dia14;
end;

procedure TMonthlyJourneyWorking.Set_Dia14(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia14(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia15: Smallint;
begin
    Result := DefaultInterface.Dia15;
end;

procedure TMonthlyJourneyWorking.Set_Dia15(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia15(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia16: Smallint;
begin
    Result := DefaultInterface.Dia16;
end;

procedure TMonthlyJourneyWorking.Set_Dia16(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia16(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia17: Smallint;
begin
    Result := DefaultInterface.Dia17;
end;

procedure TMonthlyJourneyWorking.Set_Dia17(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia17(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia18: Smallint;
begin
    Result := DefaultInterface.Dia18;
end;

procedure TMonthlyJourneyWorking.Set_Dia18(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia18(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia19: Smallint;
begin
    Result := DefaultInterface.Dia19;
end;

procedure TMonthlyJourneyWorking.Set_Dia19(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia19(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia20: Smallint;
begin
    Result := DefaultInterface.Dia20;
end;

procedure TMonthlyJourneyWorking.Set_Dia20(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia20(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia21: Smallint;
begin
    Result := DefaultInterface.Dia21;
end;

procedure TMonthlyJourneyWorking.Set_Dia21(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia21(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia22: Smallint;
begin
    Result := DefaultInterface.Dia22;
end;

procedure TMonthlyJourneyWorking.Set_Dia22(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia22(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia23: Smallint;
begin
    Result := DefaultInterface.Dia23;
end;

procedure TMonthlyJourneyWorking.Set_Dia23(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia23(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia24: Smallint;
begin
    Result := DefaultInterface.Dia24;
end;

procedure TMonthlyJourneyWorking.Set_Dia24(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia24(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia25: Smallint;
begin
    Result := DefaultInterface.Dia25;
end;

procedure TMonthlyJourneyWorking.Set_Dia25(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia25(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia26: Smallint;
begin
    Result := DefaultInterface.Dia26;
end;

procedure TMonthlyJourneyWorking.Set_Dia26(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia26(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia27: Smallint;
begin
    Result := DefaultInterface.Dia27;
end;

procedure TMonthlyJourneyWorking.Set_Dia27(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia27(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia28: Smallint;
begin
    Result := DefaultInterface.Dia28;
end;

procedure TMonthlyJourneyWorking.Set_Dia28(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia28(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia29: Smallint;
begin
    Result := DefaultInterface.Dia29;
end;

procedure TMonthlyJourneyWorking.Set_Dia29(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia29(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia30: Smallint;
begin
    Result := DefaultInterface.Dia30;
end;

procedure TMonthlyJourneyWorking.Set_Dia30(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia30(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Dia31: Smallint;
begin
    Result := DefaultInterface.Dia31;
end;

procedure TMonthlyJourneyWorking.Set_Dia31(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia31(pRetVal);
end;

function TMonthlyJourneyWorking.Get_Holiday: Smallint;
begin
    Result := DefaultInterface.Holiday;
end;

procedure TMonthlyJourneyWorking.Set_Holiday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Holiday(pRetVal);
end;

function TMonthlyJourneyWorking.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TMonthlyJourneyWorking.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TMonthlyJourneyWorking.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TMonthlyJourneyWorkingProperties.Create(AServer: TMonthlyJourneyWorking);
begin
  inherited Create;
  FServer := AServer;
end;

function TMonthlyJourneyWorkingProperties.GetDefaultInterface: _MonthlyJourneyWorking;
begin
  Result := FServer.DefaultInterface;
end;

function TMonthlyJourneyWorkingProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Id(pRetVal: Smallint);
begin
  DefaultInterface.Set_Id(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Id: Smallint;
begin
    Result := DefaultInterface.Id;
end;

procedure TMonthlyJourneyWorkingProperties.Set_TypeWorking(pRetVal: TypeWorking);
begin
  DefaultInterface.Set_TypeWorking(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_TypeWorking: TypeWorking;
begin
    Result := DefaultInterface.TypeWorking;
end;

function TMonthlyJourneyWorkingProperties.Get_Dia01: Smallint;
begin
    Result := DefaultInterface.Dia01;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia01(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia01(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia02: Smallint;
begin
    Result := DefaultInterface.Dia02;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia02(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia02(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia03: Smallint;
begin
    Result := DefaultInterface.Dia03;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia03(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia03(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia04: Smallint;
begin
    Result := DefaultInterface.Dia04;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia04(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia04(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia05: Smallint;
begin
    Result := DefaultInterface.Dia05;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia05(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia05(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia06: Smallint;
begin
    Result := DefaultInterface.Dia06;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia06(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia06(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia07: Smallint;
begin
    Result := DefaultInterface.Dia07;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia07(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia07(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia08: Smallint;
begin
    Result := DefaultInterface.Dia08;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia08(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia08(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia09: Smallint;
begin
    Result := DefaultInterface.Dia09;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia09(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia09(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia10: Smallint;
begin
    Result := DefaultInterface.Dia10;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia10(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia10(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia11: Smallint;
begin
    Result := DefaultInterface.Dia11;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia11(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia11(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia12: Smallint;
begin
    Result := DefaultInterface.Dia12;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia12(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia12(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia13: Smallint;
begin
    Result := DefaultInterface.Dia13;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia13(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia13(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia14: Smallint;
begin
    Result := DefaultInterface.Dia14;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia14(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia14(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia15: Smallint;
begin
    Result := DefaultInterface.Dia15;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia15(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia15(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia16: Smallint;
begin
    Result := DefaultInterface.Dia16;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia16(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia16(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia17: Smallint;
begin
    Result := DefaultInterface.Dia17;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia17(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia17(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia18: Smallint;
begin
    Result := DefaultInterface.Dia18;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia18(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia18(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia19: Smallint;
begin
    Result := DefaultInterface.Dia19;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia19(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia19(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia20: Smallint;
begin
    Result := DefaultInterface.Dia20;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia20(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia20(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia21: Smallint;
begin
    Result := DefaultInterface.Dia21;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia21(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia21(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia22: Smallint;
begin
    Result := DefaultInterface.Dia22;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia22(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia22(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia23: Smallint;
begin
    Result := DefaultInterface.Dia23;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia23(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia23(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia24: Smallint;
begin
    Result := DefaultInterface.Dia24;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia24(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia24(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia25: Smallint;
begin
    Result := DefaultInterface.Dia25;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia25(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia25(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia26: Smallint;
begin
    Result := DefaultInterface.Dia26;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia26(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia26(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia27: Smallint;
begin
    Result := DefaultInterface.Dia27;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia27(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia27(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia28: Smallint;
begin
    Result := DefaultInterface.Dia28;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia28(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia28(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia29: Smallint;
begin
    Result := DefaultInterface.Dia29;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia29(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia29(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia30: Smallint;
begin
    Result := DefaultInterface.Dia30;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia30(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia30(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Dia31: Smallint;
begin
    Result := DefaultInterface.Dia31;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Dia31(pRetVal: Smallint);
begin
  DefaultInterface.Set_Dia31(pRetVal);
end;

function TMonthlyJourneyWorkingProperties.Get_Holiday: Smallint;
begin
    Result := DefaultInterface.Holiday;
end;

procedure TMonthlyJourneyWorkingProperties.Set_Holiday(pRetVal: Smallint);
begin
  DefaultInterface.Set_Holiday(pRetVal);
end;

{$ENDIF}

class function CoAlarmRingsCollection.Create: _AlarmRingsCollection;
begin
  Result := CreateComObject(CLASS_AlarmRingsCollection) as _AlarmRingsCollection;
end;

class function CoAlarmRingsCollection.CreateRemote(const MachineName: string): _AlarmRingsCollection;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_AlarmRingsCollection) as _AlarmRingsCollection;
end;

procedure TAlarmRingsCollection.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{E21378D2-FA68-4106-837E-2130AC8621E6}';
    IntfIID:   '{0F06197D-377B-3293-A017-6E8C39A59C02}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TAlarmRingsCollection.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _AlarmRingsCollection;
  end;
end;

procedure TAlarmRingsCollection.ConnectTo(svrIntf: _AlarmRingsCollection);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TAlarmRingsCollection.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TAlarmRingsCollection.GetDefaultInterface: _AlarmRingsCollection;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TAlarmRingsCollection.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TAlarmRingsCollectionProperties.Create(Self);
{$ENDIF}
end;

destructor TAlarmRingsCollection.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TAlarmRingsCollection.GetServerProperties: TAlarmRingsCollectionProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TAlarmRingsCollection.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TAlarmRingsCollection.Set_ModeAlarm(pRetVal: Mode);
begin
  DefaultInterface.Set_ModeAlarm(pRetVal);
end;

function TAlarmRingsCollection.Get_ModeAlarm: Mode;
begin
    Result := DefaultInterface.ModeAlarm;
end;

function TAlarmRingsCollection.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TAlarmRingsCollection.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TAlarmRingsCollection.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

procedure TAlarmRingsCollection.Add(const AlarmRings: _AlarmRings);
begin
  DefaultInterface.Add(AlarmRings);
end;

function TAlarmRingsCollection.Count: Integer;
begin
  Result := DefaultInterface.Count;
end;

procedure TAlarmRingsCollection.Remove(const AlarmRings: _AlarmRings);
begin
  DefaultInterface.Remove(AlarmRings);
end;

procedure TAlarmRingsCollection.Clear;
begin
  DefaultInterface.Clear;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TAlarmRingsCollectionProperties.Create(AServer: TAlarmRingsCollection);
begin
  inherited Create;
  FServer := AServer;
end;

function TAlarmRingsCollectionProperties.GetDefaultInterface: _AlarmRingsCollection;
begin
  Result := FServer.DefaultInterface;
end;

function TAlarmRingsCollectionProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

procedure TAlarmRingsCollectionProperties.Set_ModeAlarm(pRetVal: Mode);
begin
  DefaultInterface.Set_ModeAlarm(pRetVal);
end;

function TAlarmRingsCollectionProperties.Get_ModeAlarm: Mode;
begin
    Result := DefaultInterface.ModeAlarm;
end;

{$ENDIF}

class function CoSerialComm.Create: _SerialComm;
begin
  Result := CreateComObject(CLASS_SerialComm) as _SerialComm;
end;

class function CoSerialComm.CreateRemote(const MachineName: string): _SerialComm;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_SerialComm) as _SerialComm;
end;

class function CoCard.Create: _Card;
begin
  Result := CreateComObject(CLASS_Card) as _Card;
end;

class function CoCard.CreateRemote(const MachineName: string): _Card;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_Card) as _Card;
end;

procedure TCard.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{4EA399E7-3A75-47D2-9166-F594FC97DB03}';
    IntfIID:   '{B81CD20E-0982-332F-9289-54316DB0B4F0}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TCard.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _Card;
  end;
end;

procedure TCard.ConnectTo(svrIntf: _Card);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TCard.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TCard.GetDefaultInterface: _Card;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TCard.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TCardProperties.Create(Self);
{$ENDIF}
end;

destructor TCard.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TCard.GetServerProperties: TCardProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TCard.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TCard.Get_code: WideString;
begin
    Result := DefaultInterface.code;
end;

procedure TCard.Set_code(const pRetVal: WideString);
  { Warning: The property code has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.code := pRetVal;
end;

function TCard.Get_Dummy: Byte;
begin
    Result := DefaultInterface.Dummy;
end;

procedure TCard.Set_Dummy(pRetVal: Byte);
begin
  DefaultInterface.Set_Dummy(pRetVal);
end;

function TCard.Get_message: Byte;
begin
    Result := DefaultInterface.message;
end;

procedure TCard.Set_message(pRetVal: Byte);
begin
  DefaultInterface.Set_message(pRetVal);
end;

function TCard.Get_way: Byte;
begin
    Result := DefaultInterface.way;
end;

procedure TCard.Set_way(pRetVal: Byte);
begin
  DefaultInterface.Set_way(pRetVal);
end;

function TCard.Get_Jornada: Smallint;
begin
    Result := DefaultInterface.Jornada;
end;

procedure TCard.Set_Jornada(pRetVal: Smallint);
begin
  DefaultInterface.Set_Jornada(pRetVal);
end;

function TCard.Get_CounterAccess: TypeCounter_Access;
begin
    Result := DefaultInterface.CounterAccess;
end;

procedure TCard.Set_CounterAccess(pRetVal: TypeCounter_Access);
begin
  DefaultInterface.Set_CounterAccess(pRetVal);
end;

function TCard.Get_password: Integer;
begin
    Result := DefaultInterface.password;
end;

procedure TCard.Set_password(pRetVal: Integer);
begin
  DefaultInterface.Set_password(pRetVal);
end;

function TCard.Get_Template: _TemplateCollection;
begin
    Result := DefaultInterface.Template;
end;

procedure TCard._Set_Template(const pRetVal: _TemplateCollection);
  { Warning: The property Template has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Template := pRetVal;
end;

function TCard.Get_isMaster: WordBool;
begin
    Result := DefaultInterface.isMaster;
end;

procedure TCard.Set_isMaster(pRetVal: WordBool);
begin
  DefaultInterface.Set_isMaster(pRetVal);
end;

function TCard.Get_name: WideString;
begin
    Result := DefaultInterface.name;
end;

procedure TCard.Set_name(const pRetVal: WideString);
  { Warning: The property name has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.name := pRetVal;
end;

function TCard.Get_MasterCard: TypeMasterCard;
begin
    Result := DefaultInterface.MasterCard;
end;

procedure TCard.Set_MasterCard(pRetVal: TypeMasterCard);
begin
  DefaultInterface.Set_MasterCard(pRetVal);
end;

function TCard.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TCard.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TCard.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TCardProperties.Create(AServer: TCard);
begin
  inherited Create;
  FServer := AServer;
end;

function TCardProperties.GetDefaultInterface: _Card;
begin
  Result := FServer.DefaultInterface;
end;

function TCardProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TCardProperties.Get_code: WideString;
begin
    Result := DefaultInterface.code;
end;

procedure TCardProperties.Set_code(const pRetVal: WideString);
  { Warning: The property code has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.code := pRetVal;
end;

function TCardProperties.Get_Dummy: Byte;
begin
    Result := DefaultInterface.Dummy;
end;

procedure TCardProperties.Set_Dummy(pRetVal: Byte);
begin
  DefaultInterface.Set_Dummy(pRetVal);
end;

function TCardProperties.Get_message: Byte;
begin
    Result := DefaultInterface.message;
end;

procedure TCardProperties.Set_message(pRetVal: Byte);
begin
  DefaultInterface.Set_message(pRetVal);
end;

function TCardProperties.Get_way: Byte;
begin
    Result := DefaultInterface.way;
end;

procedure TCardProperties.Set_way(pRetVal: Byte);
begin
  DefaultInterface.Set_way(pRetVal);
end;

function TCardProperties.Get_Jornada: Smallint;
begin
    Result := DefaultInterface.Jornada;
end;

procedure TCardProperties.Set_Jornada(pRetVal: Smallint);
begin
  DefaultInterface.Set_Jornada(pRetVal);
end;

function TCardProperties.Get_CounterAccess: TypeCounter_Access;
begin
    Result := DefaultInterface.CounterAccess;
end;

procedure TCardProperties.Set_CounterAccess(pRetVal: TypeCounter_Access);
begin
  DefaultInterface.Set_CounterAccess(pRetVal);
end;

function TCardProperties.Get_password: Integer;
begin
    Result := DefaultInterface.password;
end;

procedure TCardProperties.Set_password(pRetVal: Integer);
begin
  DefaultInterface.Set_password(pRetVal);
end;

function TCardProperties.Get_Template: _TemplateCollection;
begin
    Result := DefaultInterface.Template;
end;

procedure TCardProperties._Set_Template(const pRetVal: _TemplateCollection);
  { Warning: The property Template has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.Template := pRetVal;
end;

function TCardProperties.Get_isMaster: WordBool;
begin
    Result := DefaultInterface.isMaster;
end;

procedure TCardProperties.Set_isMaster(pRetVal: WordBool);
begin
  DefaultInterface.Set_isMaster(pRetVal);
end;

function TCardProperties.Get_name: WideString;
begin
    Result := DefaultInterface.name;
end;

procedure TCardProperties.Set_name(const pRetVal: WideString);
  { Warning: The property name has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.name := pRetVal;
end;

function TCardProperties.Get_MasterCard: TypeMasterCard;
begin
    Result := DefaultInterface.MasterCard;
end;

procedure TCardProperties.Set_MasterCard(pRetVal: TypeMasterCard);
begin
  DefaultInterface.Set_MasterCard(pRetVal);
end;

{$ENDIF}

class function CoBioPointFingerPrintMessage.Create: _BioPointFingerPrintMessage;
begin
  Result := CreateComObject(CLASS_BioPointFingerPrintMessage) as _BioPointFingerPrintMessage;
end;

class function CoBioPointFingerPrintMessage.CreateRemote(const MachineName: string): _BioPointFingerPrintMessage;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_BioPointFingerPrintMessage) as _BioPointFingerPrintMessage;
end;

class function CoFaceLogRecord.Create: _FaceLogRecord;
begin
  Result := CreateComObject(CLASS_FaceLogRecord) as _FaceLogRecord;
end;

class function CoFaceLogRecord.CreateRemote(const MachineName: string): _FaceLogRecord;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_FaceLogRecord) as _FaceLogRecord;
end;

procedure TFaceLogRecord.InitServerData;
const
  CServerData: TServerData = (
    ClassID:   '{98C78AAB-DD68-4A62-B4E8-D0C5FEE578E4}';
    IntfIID:   '{294C019E-7BCA-3477-9D0F-26F2C77D1252}';
    EventIID:  '';
    LicenseKey: nil;
    Version: 500);
begin
  ServerData := @CServerData;
end;

procedure TFaceLogRecord.Connect;
var
  punk: IUnknown;
begin
  if FIntf = nil then
  begin
    punk := GetServer;
    Fintf:= punk as _FaceLogRecord;
  end;
end;

procedure TFaceLogRecord.ConnectTo(svrIntf: _FaceLogRecord);
begin
  Disconnect;
  FIntf := svrIntf;
end;

procedure TFaceLogRecord.DisConnect;
begin
  if Fintf <> nil then
  begin
    FIntf := nil;
  end;
end;

function TFaceLogRecord.GetDefaultInterface: _FaceLogRecord;
begin
  if FIntf = nil then
    Connect;
  Assert(FIntf <> nil, 'DefaultInterface is NULL. Component is not connected to Server. You must call ''Connect'' or ''ConnectTo'' before this operation');
  Result := FIntf;
end;

constructor TFaceLogRecord.Create(AOwner: TComponent);
begin
  inherited Create(AOwner);
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps := TFaceLogRecordProperties.Create(Self);
{$ENDIF}
end;

destructor TFaceLogRecord.Destroy;
begin
{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
  FProps.Free;
{$ENDIF}
  inherited Destroy;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
function TFaceLogRecord.GetServerProperties: TFaceLogRecordProperties;
begin
  Result := FProps;
end;
{$ENDIF}

function TFaceLogRecord.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TFaceLogRecord.Get_employeeID: WideString;
begin
    Result := DefaultInterface.employeeID;
end;

procedure TFaceLogRecord.Set_employeeID(const pRetVal: WideString);
  { Warning: The property employeeID has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.employeeID := pRetVal;
end;

function TFaceLogRecord.Get_DateTimeMarkingPoint: TDateTime;
begin
    Result := DefaultInterface.DateTimeMarkingPoint;
end;

procedure TFaceLogRecord.Set_DateTimeMarkingPoint(pRetVal: TDateTime);
begin
  DefaultInterface.Set_DateTimeMarkingPoint(pRetVal);
end;

function TFaceLogRecord.Equals(obj: OleVariant): WordBool;
begin
  Result := DefaultInterface.Equals(obj);
end;

function TFaceLogRecord.GetHashCode: Integer;
begin
  Result := DefaultInterface.GetHashCode;
end;

function TFaceLogRecord.GetType: _Type;
begin
  Result := DefaultInterface.GetType;
end;

{$IFDEF LIVE_SERVER_AT_DESIGN_TIME}
constructor TFaceLogRecordProperties.Create(AServer: TFaceLogRecord);
begin
  inherited Create;
  FServer := AServer;
end;

function TFaceLogRecordProperties.GetDefaultInterface: _FaceLogRecord;
begin
  Result := FServer.DefaultInterface;
end;

function TFaceLogRecordProperties.Get_ToString: WideString;
begin
    Result := DefaultInterface.ToString;
end;

function TFaceLogRecordProperties.Get_employeeID: WideString;
begin
    Result := DefaultInterface.employeeID;
end;

procedure TFaceLogRecordProperties.Set_employeeID(const pRetVal: WideString);
  { Warning: The property employeeID has a setter and a getter whose
    types do not match. Delphi was unable to generate a property of
    this sort and so is using a Variant as a passthrough. }
var
  InterfaceVariant: OleVariant;
begin
  InterfaceVariant := DefaultInterface;
  InterfaceVariant.employeeID := pRetVal;
end;

function TFaceLogRecordProperties.Get_DateTimeMarkingPoint: TDateTime;
begin
    Result := DefaultInterface.DateTimeMarkingPoint;
end;

procedure TFaceLogRecordProperties.Set_DateTimeMarkingPoint(pRetVal: TDateTime);
begin
  DefaultInterface.Set_DateTimeMarkingPoint(pRetVal);
end;

{$ENDIF}

procedure Register;
begin
  RegisterComponents(dtlServerPage, [TRelogio, TTCPComm, TPrintPointLiFingerPrint, TPrintPointEmployee, 
    TWatchComm, TPrintPointSendSerialNumberMessage, TPrintPointLiStatus, TMRPRecord, TFaceFingerPrint, 
    TMiniPointStatusMessage, TNoDataMessage, TTemplate, TMiniPointConfigurator, TUtil, 
    TMicroPointStatusMessage, TBioPointCardList, TShiftTable, TJourneyWorking, TPeriodicJourneyWorking, 
    TMemoryFormat, TBioPointStatusMessage, TFaceEmployee, THoliday, TPrintPointLiEmployee, 
    TParcialConfiguration, TCardCollection, TConcretePunchMessage, TBioPointMemoryFormat, TTemplateCollection, 
    TAlarmRings, TCredential, TConfiguration, TFaceStatus, THolidayCollection, 
    TWeeklyJourneyWorking, TMaster, TPrintPointEmployerMessage, TMicropointCardList, TShiftTableCollection, 
    TMonthlyJourneyWorking, TAlarmRingsCollection, TCard, TFaceLogRecord]);
end;

end.
