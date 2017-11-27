unit uRep;

interface

uses
  SysUtils, // Para usar rotinas de utilidades gerais do Delphi.
  uHash,    // Para usar a criptografia.
  uUtil,    // Para usar as rotinas de utilidade.
  uCodErro, // Para usar os códigos de erro.
  Dialogs;  // Para usar o "ShowMessage".

//function EnviarDigital(pis: array of byte; atual, total: byte; digital: array of byte): integer; stdcall; external 'rwtrep.dll';
function EnviarDigital(pis: PChar; atual, total: byte; digital: PChar): integer; stdcall; external 'rwtrep.dll';
function AbrirComunicacaoComRep(ip: PChar; porta: integer; timeoutMs: integer; hash: PChar): integer; stdcall; external 'rwtrep.dll';
function ComunicacaoAbertaComRep: boolean; stdcall; external 'rwtrep.dll';
function FecharComunicacaoComRep: integer; stdcall; external 'rwtrep.dll';

implementation


begin
end.
