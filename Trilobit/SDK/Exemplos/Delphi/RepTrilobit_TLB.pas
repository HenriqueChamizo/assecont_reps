unit RepTrilobit_TLB;

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

// $Rev: 5081 $
// File generated on 3/3/2010 17:51:52 from Type Library described below.

// ************************************************************************  //
// Type Lib: RepTrilobit.tlb (1)
// LIBID: {92D5DFBE-1086-4568-AC2B-08B72A64CD73}
// LCID: 0
// Helpfile: 
// HelpString: API RepTrilobit
// DepndLst: 
//   (1) v2.0 stdole, (C:\WINDOWS\system32\stdole2.tlb)
//   (2) v2.0 mscorlib, (C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\mscorlib.tlb)
// ************************************************************************ //
{$TYPEDADDRESS OFF} // Unit must be compiled without type-checked pointers. 
{$WARN SYMBOL_PLATFORM OFF}
{$WRITEABLECONST ON}
{$VARPROPSETTER ON}
interface

uses Windows, ActiveX, Classes, Graphics, mscorlib_TLB, OleServer, StdVCL, Variants;
  


// *********************************************************************//
// GUIDS declared in the TypeLibrary. Following prefixes are used:        
//   Type Libraries     : LIBID_xxxx                                      
//   CoClasses          : CLASS_xxxx                                      
//   DISPInterfaces     : DIID_xxxx                                       
//   Non-DISP interfaces: IID_xxxx                                        
// *********************************************************************//
const
  // TypeLibrary Major and minor versions
  RepTrilobitMajorVersion = 1;
  RepTrilobitMinorVersion = 0;

  LIBID_RepTrilobit: TGUID = '{92D5DFBE-1086-4568-AC2B-08B72A64CD73}';

  IID__REP: TGUID = '{EFBCA06A-4D4B-33EA-B79F-457B10EBE380}';
  CLASS_REP: TGUID = '{8B0EBD60-7499-482D-B1CC-07AFBCEF4231}';

// *********************************************************************//
// Declaration of Enumerations defined in Type Library                    
// *********************************************************************//
// Constants for enum eParamSetConfig
type
  eParamSetConfig = TOleEnum;
const
  eParamSetConfig_EnderecoIP = $00000001;
  eParamSetConfig_PortaUDP = $00000002;
  eParamSetConfig_MascaraRede = $00000003;
  eParamSetConfig_Roteador = $00000004;
  eParamSetConfig_TipoIdentificacao = $00000005;
  eParamSetConfig_InicioHorarioVerao = $00000006;
  eParamSetConfig_FimHorarioVerao = $00000007;
  eParamSetConfig_Senha = $00000008;
  eParamSetConfig_Reiniciar = $00000009;
  eParamSetConfig_AjusteRelogio = $0000000A;

// Constants for enum eParamGetConfig
type
  eParamGetConfig = TOleEnum;
const
  eParamGetConfig_EnderecoIP = $00000065;
  eParamGetConfig_PortaUDP = $00000066;
  eParamGetConfig_MascaraRede = $00000067;
  eParamGetConfig_Roteador = $00000068;
  eParamGetConfig_TipoIdentificacao = $00000069;
  eParamGetConfig_InicioHorarioVerao = $0000006A;
  eParamGetConfig_FimHorarioVerao = $0000006B;
  eParamGetConfig_HorarioAtual = $0000006E;
  eParamGetConfig_NumeroSerie = $000000C9;
  eParamGetConfig_Empregador = $000000CA;
  eParamGetConfig_Documento = $000000CB;
  eParamGetConfig_Local = $000000CC;
  eParamGetConfig_QtdeEmpregados = $000000CD;
  eParamGetConfig_QtdeLancamentos = $000000CE;
  eParamGetConfig_InicioOperacao = $000000CF;
  eParamGetConfig_UltimoRegistro = $000000D0;
  eParamGetConfig_NsrAtual = $000000D1;
  eParamGetConfig_RegistrosLivres = $000000D2;
  eParamGetConfig_TamanhoMRP = $000000D3;
  eParamGetConfig_EspacoLivre = $000000D4;
  eParamGetConfig_MacAddress = $000000D5;
  eParamGetConfig_NumeroModelo = $000000D6;
  eParamGetConfig_NumeroFabricante = $000000D7;
  eParamGetConfig_CEI = $000000D8;
  eParamGetConfig_TipoDocumento = $000000D9;
  eParamGetConfig_StatusPapel = $000000DA;
  eParamGetConfig_NumeroSerieCompleto = $000000DB;

// Constants for enum eTipoDocumento
type
  eTipoDocumento = TOleEnum;
const
  eTipoDocumento_CNPJ = $00000001;
  eTipoDocumento_CPF = $00000002;

type

// *********************************************************************//
// Forward declaration of types defined in TypeLibrary                    
// *********************************************************************//
  _REP = interface;
  _REPDisp = dispinterface;

// *********************************************************************//
// Declaration of CoClasses defined in Type Library                       
// (NOTE: Here we map each CoClass to its Default Interface)              
// *********************************************************************//
  REP = _REP;


// *********************************************************************//
// Interface: _REP
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {EFBCA06A-4D4B-33EA-B79F-457B10EBE380}
// *********************************************************************//
  _REP = interface(IDispatch)
    ['{EFBCA06A-4D4B-33EA-B79F-457B10EBE380}']
    function Get_ToString: WideString; safecall;
    function Equals(obj: OleVariant): WordBool; safecall;
    function GetHashCode: Integer; safecall;
    function GetType: _Type; safecall;
    procedure Dispose; safecall;
    function Get_ErrorException: _Exception; safecall;
    procedure _Set_ErrorException(const pRetVal: _Exception); safecall;
    function CadastrarEmpregador(const IP: WideString; Porta: Integer; Senha: Integer; 
                                 TipoDoc: eTipoDocumento; const Documento: WideString; 
                                 const CEI: WideString; const RazaoSocial: WideString; 
                                 const Local: WideString): WordBool; safecall;
    function CadastrarEmpregado(const IP: WideString; Porta: Integer; Senha: Integer; 
                                const PIS: WideString; const Nome: WideString; 
                                const Cracha: WideString; PossuiBiometria: WordBool): WordBool; safecall;
    function ExcluirEmpregado(const IP: WideString; Porta: Integer; Senha: Integer; 
                              const PIS: WideString): WordBool; safecall;
    function LerAFD(const IP: WideString; Porta: Integer; Senha: Integer; 
                    const DataInicial: WideString; const DataFinal: WideString; 
                    const ArquivoAFD: WideString): WordBool; safecall;
    function LerEmpregados(const IP: WideString; Porta: Integer; Senha: Integer; 
                           const Arquivo: WideString; AdicionarCabecalho: WordBool): WordBool; safecall;
    function EnviarConfiguracao(const IP: WideString; Porta: Integer; Senha: Integer; 
                                Parametro: eParamSetConfig; const Valor: WideString): WordBool; safecall;
    function LerConfiguracao(const IP: WideString; Porta: Integer; Senha: Integer; 
                             Parametro: eParamGetConfig; var Valor: WideString): WordBool; safecall;
    function LerAFD_ViaLista(const IP: WideString; Porta: Integer; Senha: Integer; 
                             const DataInicial: WideString; const DataFinal: WideString; 
                             var Lista: WideString; NSR: Integer; const SeparadorCampo: WideString; 
                             const SeparadorRegistro: WideString): WordBool; safecall;
    function LerEmpregados_ViaLista(const IP: WideString; Porta: Integer; Senha: Integer; 
                                    var Lista: WideString; const SeparadorCampo: WideString; 
                                    const SeparadorRegistro: WideString): WordBool; safecall;
    function LerConfiguracoes(const IP: WideString; Porta: Integer; Senha: Integer; 
                              const Arquivo: WideString; AdicionarCabecalho: WordBool): WordBool; safecall;
    function LerConfiguracoes_ViaLista(const IP: WideString; Porta: Integer; Senha: Integer; 
                                       var Lista: WideString; const SeparadorCampo: WideString; 
                                       const SeparadorRegistro: WideString): WordBool; safecall;
    function LerEmpregado_PeloCracha(const IP: WideString; Porta: Integer; Senha: Integer; 
                                     const Cracha: WideString; var PIS: WideString): WordBool; safecall;
    property ToString: WideString read Get_ToString;
    property ErrorException: _Exception read Get_ErrorException write _Set_ErrorException;
  end;

// *********************************************************************//
// DispIntf:  _REPDisp
// Flags:     (4560) Hidden Dual NonExtensible OleAutomation Dispatchable
// GUID:      {EFBCA06A-4D4B-33EA-B79F-457B10EBE380}
// *********************************************************************//
  _REPDisp = dispinterface
    ['{EFBCA06A-4D4B-33EA-B79F-457B10EBE380}']
    property ToString: WideString readonly dispid 0;
    function Equals(obj: OleVariant): WordBool; dispid 1610743809;
    function GetHashCode: Integer; dispid 1610743810;
    function GetType: _Type; dispid 1610743811;
    procedure Dispose; dispid 1610743812;
    property ErrorException: _Exception dispid 1610743813;
    function CadastrarEmpregador(const IP: WideString; Porta: Integer; Senha: Integer; 
                                 TipoDoc: eTipoDocumento; const Documento: WideString; 
                                 const CEI: WideString; const RazaoSocial: WideString; 
                                 const Local: WideString): WordBool; dispid 1;
    function CadastrarEmpregado(const IP: WideString; Porta: Integer; Senha: Integer; 
                                const PIS: WideString; const Nome: WideString; 
                                const Cracha: WideString; PossuiBiometria: WordBool): WordBool; dispid 2;
    function ExcluirEmpregado(const IP: WideString; Porta: Integer; Senha: Integer; 
                              const PIS: WideString): WordBool; dispid 3;
    function LerAFD(const IP: WideString; Porta: Integer; Senha: Integer; 
                    const DataInicial: WideString; const DataFinal: WideString; 
                    const ArquivoAFD: WideString): WordBool; dispid 4;
    function LerEmpregados(const IP: WideString; Porta: Integer; Senha: Integer; 
                           const Arquivo: WideString; AdicionarCabecalho: WordBool): WordBool; dispid 5;
    function EnviarConfiguracao(const IP: WideString; Porta: Integer; Senha: Integer; 
                                Parametro: eParamSetConfig; const Valor: WideString): WordBool; dispid 6;
    function LerConfiguracao(const IP: WideString; Porta: Integer; Senha: Integer; 
                             Parametro: eParamGetConfig; var Valor: WideString): WordBool; dispid 7;
    function LerAFD_ViaLista(const IP: WideString; Porta: Integer; Senha: Integer; 
                             const DataInicial: WideString; const DataFinal: WideString; 
                             var Lista: WideString; NSR: Integer; const SeparadorCampo: WideString; 
                             const SeparadorRegistro: WideString): WordBool; dispid 8;
    function LerEmpregados_ViaLista(const IP: WideString; Porta: Integer; Senha: Integer; 
                                    var Lista: WideString; const SeparadorCampo: WideString; 
                                    const SeparadorRegistro: WideString): WordBool; dispid 9;
    function LerConfiguracoes(const IP: WideString; Porta: Integer; Senha: Integer; 
                              const Arquivo: WideString; AdicionarCabecalho: WordBool): WordBool; dispid 10;
    function LerConfiguracoes_ViaLista(const IP: WideString; Porta: Integer; Senha: Integer; 
                                       var Lista: WideString; const SeparadorCampo: WideString; 
                                       const SeparadorRegistro: WideString): WordBool; dispid 11;
    function LerEmpregado_PeloCracha(const IP: WideString; Porta: Integer; Senha: Integer; 
                                     const Cracha: WideString; var PIS: WideString): WordBool; dispid 12;
  end;

// *********************************************************************//
// The Class CoREP provides a Create and CreateRemote method to          
// create instances of the default interface _REP exposed by              
// the CoClass REP. The functions are intended to be used by             
// clients wishing to automate the CoClass objects exposed by the         
// server of this typelibrary.                                            
// *********************************************************************//
  CoREP = class
    class function Create: _REP;
    class function CreateRemote(const MachineName: string): _REP;
  end;

implementation

uses ComObj;

class function CoREP.Create: _REP;
begin
  Result := CreateComObject(CLASS_REP) as _REP;
end;

class function CoREP.CreateRemote(const MachineName: string): _REP;
begin
  Result := CreateRemoteComObject(MachineName, CLASS_REP) as _REP;
end;

end.
