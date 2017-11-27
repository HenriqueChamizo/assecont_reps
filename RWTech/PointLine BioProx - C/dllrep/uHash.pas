unit uHash;

interface

uses  SysUtils, uUtil;

const
  BlkLenMax = 16;
  KeyLenMax = 32;
  KeySchLenMax = 128;

type
  AESctx = packed record
    Ekey : packed array[0..KeySchLenMax-1] of Integer;
    Nrnd : Integer;
    Ncol : Integer;
  end;

  KeyBlk = packed record
    K : packed array[0..KeyLenMax-1] of Byte;
  end;

  IoBlk = packed record
    IO : packed array[0..BlkLenMax-1] of Byte;
  end;

function AesBlkLen (N : Integer; var C : AESctx) : Integer; stdcall; external 'aes.dll' name '_aes_blk_len@8';  //--No creo que sea stdcall... pero ver...
function AesEncKey (var K : KeyBlk; N : Integer; var C : AESctx) : Integer; stdcall; external 'aes.dll' name '_aes_enc_key@12';
function AesDecKey (var K : KeyBlk; N : Integer; var C : AESctx) : Integer; stdcall; external 'aes.dll' name '_aes_dec_key@12';
function AesEncBlk (var Ib : IoBlk; var Ob : IoBlk; var C : AESctx) : Integer; stdcall; external 'aes.dll' name '_aes_enc_blk@12';
function AesDecBlk (var Ib : IoBlk; var Ob : IoBlk; var C : AESctx) : Integer; stdcall; external 'aes.dll' name '_aes_dec_blk@12';

function Criptografar(sKey: array of byte; DadosCrip: array of byte; var Saida: array of byte; qde: integer): boolean;
function Descriptografar(sKey: array of byte; DadosDescrip: array of byte; var Saida: array of byte; qde: integer): boolean;
function CalculaTamBlocoNecessario(qdeDados: integer): integer;

implementation

var
  BlkLen : integer = 0;
  KeyLen : integer = 0;
  
procedure ZeroAESctx(var C : AESctx);
begin
  FillChar(C, sizeof(AESctx), 0); // Seta todos os  "sizeof(AESctx)" bytes de "C" com 0
end;

function Criptografar(sKey: array of byte; DadosCrip: array of byte; var Saida: array of byte; qde: integer): boolean;
var
  Key: KeyBlk;
  Ib: IoBlk;
  Ob: IoBlk;
  Cx: AESctx;
  i: integer;
  j: integer;
  hexa: string;
  sucesso: boolean;
  tamDadosCrip: integer;
  tamDadosSaida: integer;

begin
  sucesso := true;

  try
    // Calcula se o parâmetro "Saida" possui o tamanho suficiente para os dados a serem armazenados nele.
    tamDadosSaida := qde;
    tamDadosSaida := CalculaTamBlocoNecessario(tamDadosSaida);
    if ((Length(Saida) < tamDadosSaida) or (qde > Length(DadosCrip))) then // O tamanho do bufferes está OK?
    begin
      sucesso := false;
    end;

    if (true = sucesso) then
    begin
      BlkLen := BlkLenMax;
      KeyLen := KeyLenMax;
      ZeroAESctx(Cx);

      // Converte a chave hash em bytes e armazena no campo destinado a isso - o "Key.k".
      memcpy(key.k, sKey, 0, KeyLenMax);

      AesEncKey(Key, KeyLen, Cx);

      FillChar(Ob, BlkLen, 0); // Seta todos os  "BlkLen" bytes de "Ob" com 0.

      // Faz a criptografia de cada bloco e a transfere para o buffer de saída.
      tamDadosCrip := qde;
      j := 0;
      while (j < tamDadosCrip) do
      begin
        // Copia bloco dos dados a serem criptografados para o "Ib.io".
        for i := 0 to (BlkLenMax - 1) do
        begin
          if ((j + i) < tamDadosCrip) then // Ainda há dados a serem criptografados no buffer de entrada?
          begin
            Ib.io[i] := DadosCrip[j + i];
          end
          else // Não há mais dados a serem criptografados no buffer de entrada?
          begin
            Ib.io[i] := $FF;
          end
        end;

        AesEncBlk(Ib, Ob, Cx);

        // Copia o bloco dos dados, já criptografados, para o parâmetro de saída.
        tamDadosSaida := Length(Ob.io);
        for i := 0 to (BlkLenMax - 1) do
        begin
          Saida[j + i] := Ob.io[i];
        end;

        j := j + BlkLenMax;
      end;
    end;
  except
    sucesso := false;
  end;

  result := sucesso;
end;

function Descriptografar(sKey: array of byte; DadosDescrip: array of byte; var Saida: array of byte; qde: integer): boolean;
var
  hexa: string;
  Key: KeyBlk;
  Ib: IoBlk;
  Ob: IoBlk;
  Cx: AESctx;
  i: integer;
  j: integer;
  tamDadosDescrip: integer;
  tamDadosSaida: integer;
  sucesso: boolean;

begin
  sucesso := true;

  try
    // Calcula se o parâmetro "Saida" possui o tamanho suficiente para os dados a serem armazenados nele.
    tamDadosSaida := qde;
    tamDadosSaida := CalculaTamBlocoNecessario(tamDadosSaida);
    if ((Length(Saida) < tamDadosSaida) or (qde > Length(DadosDescrip))) then // O tamanho dos bufferes está OK?
    begin
      sucesso := false;
    end;

    if (true = sucesso) then
    begin
      BlkLen := BlkLenMax;
      KeyLen := KeyLenMax;
      ZeroAESctx(Cx);

      // Converte a chave hash em bytes e armazena no campo destinado a isso - o "Key.k".
      memcpy(key.k, sKey, 0, KeyLenMax);

      AesDecKey(Key, KeyLen, Cx);

      // Faz a descriptografia de cada bloco e o transfere para o buffer de saída.
      tamDadosDescrip := qde;
      j := 0;
      while (j < tamDadosDescrip) do
      begin
        // Copia um bloco de dados a serem descriptografados para o "Ib.io".
        for i := 0 to (BlkLenMax - 1) do
        begin
          if ((j + i) < tamDadosDescrip) then // Ainda há dados no buffer de entrada?
          begin
            Ib.io[i] := DadosDescrip[j + i];
          end
          else // Não há mais dados no buffer de entrada.
          begin
            Ib.io[i] := $FF;
          end;
        end;

        AesDecBlk(Ib, Ob, Cx);

        // Copia os dados, já criptografados, para o parâmetro de saída.
        for i := 0 to (BlkLenMax - 1) do
        begin
          Saida[j + i] := Ob.io[i];
        end;

        j := j + BlkLenMax;
      end;
    end;
  except
    sucesso := true;
  end;

  result := sucesso;
end;

function CalculaTamBlocoNecessario(qdeDados: integer): integer;
var
  tamBlocoNecessario: integer;
begin

  tamBlocoNecessario := qdeDados;
  while ((tamBlocoNecessario mod BlkLenMax) <> 0) do inc(tamBlocoNecessario);

  result := tamBlocoNecessario;
end;


end.

