unit dll1510;

interface

uses
  Classes;

type TMarcacao = record
    nsr: LongWord;
    cont: LongWord;
    pis: string[12];
    dia: byte;
    mes: byte;
    ano: word;
    hora: byte;
    minuto: byte;
  end;

type TControle = record
    total: Word;
    atual: Word;
    start: boolean;
    erro: Shortint;
    porta: Word;
    s_tipo: byte;
    modelo: byte;
    endereco: string[50];
    backup: ShortString;
    baudrate: integer;
  end;

type TDados = record
    adcOUSubst: ShortString;
    pin: ShortString;
    pis: ShortString;
    identificador: ShortString;
    cei: ShortString;
    razaoSocial: ShortString;
    localPrestServ: ShortString;
    tipoId: ShortString;
    nome: ShortString;
    id_bio: ShortString;
    numCartao: ShortString;
    senha: ShortString;
    mestre: ShortString;
    verifica: ShortString;
  end;

type TDigitais = record
    pin: ShortString;
    dedo: AnsiString;
  end;

type TDigital = record
    id: integer;
    amostra1: array [1..400] of byte;
    amostra2: array [1..400] of byte;
    panico: boolean;
  end;

type TDedos = record
    dedo1: AnsiString; //AnsiString;
    dedo2: AnsiString;
    dedo3: AnsiString;
    dedo4: AnsiString;
    dedo5: AnsiString;
    dedo6: AnsiString;
    dedo7: AnsiString;
    dedo8: AnsiString;
    dedo9: AnsiString;
    dedo10: AnsiString;
  end;

function configura(tipo, ip, host, porta, end_dev, datahorai, datahoraf,
         diasSem, diasSemF, info: PChar; com, total, atual, duracaoToque, flag,
         config, qtde_rel, baud: integer): pchar; stdcall; external 'authotelcom.dll';


function recebeMarcacoes(var marcacao: array of TMarcacao; var controle: Tcontrole; evento: integer):boolean; stdcall; external 'authotelcom.dll';

function recebeMarcacoesTCP(var marcacao: array of TMarcacao; var controle: Tcontrole; evento: integer):boolean; stdcall; external 'authotelcom.dll';

function enviaTrabalhador(var dados: TDados; var controle: Tcontrole):boolean; stdcall; external 'authotelcom.dll';

function enviaEmpregador(var dados: TDados; var controle: Tcontrole):boolean; stdcall; external 'authotelcom.dll';

function enviaTrabalhadorTCP(var dados: TDados; var controle: Tcontrole):boolean; stdcall; external 'authotelcom.dll';

function enviaEmpregadorTCP(var dados: TDados; var controle: Tcontrole):boolean; stdcall; external 'authotelcom.dll';

function leEmpregador(var dados: TDados; var controle: Tcontrole):boolean; stdcall; external 'authotelcom.dll';

function leTrabalhador(var dados: TDados; var controle: Tcontrole):boolean; stdcall; external 'authotelcom.dll';

function leEmpregadorTCP(var dados: TDados; var controle: Tcontrole):boolean; stdcall; external 'authotelcom.dll';

function leTrabalhadorTCP(var dados: TDados; var controle: Tcontrole):boolean; stdcall; external 'authotelcom.dll';

function backupDigitais(var controle: Tcontrole):boolean; stdcall; external 'authotelcom.dll';

function backupTCP(var controle: Tcontrole):boolean; stdcall; external 'authotelcom.dll';

function restauraBackup(var controle: Tcontrole; caminho: pchar):boolean; stdcall; external 'authotelcom.dll';

function restauraBackupTCP(var controle: Tcontrole; caminho: pchar):boolean; stdcall; external 'authotelcom.dll';

function enviaDigitais(var digitais: Tdigitais; var controle: Tcontrole): boolean; stdcall; external 'authotelcom.dll';

function enviaDigitaisTCP(var digitais: Tdigitais; var controle: Tcontrole): boolean; stdcall; external 'authotelcom.dll';

function leDigitaisTCP(var digitais: TDigitais; var controle: Tcontrole): boolean; stdcall; external 'authotelcom.dll';

function leDigitais(var digitais: TDigitais; var controle: Tcontrole): boolean; stdcall; external 'authotelcom.dll';

procedure fecharComunicacao; stdcall; external 'authotelcom.dll';

type
  (* Tipos *)
  TCriaFIR = function(const NumDigitais: Integer; var Digitais: Array of TDigital): PWideChar; cdecl;

type
  Tdll1510 = class

private


public


end;

implementation
end.
