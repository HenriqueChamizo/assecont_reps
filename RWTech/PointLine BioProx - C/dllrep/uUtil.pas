unit uUtil;

interface

uses  SysUtils;

function isHexDigit(digit: char): boolean;
function strHexToArrayByte(str: String; var bytes: array of byte; max: integer): integer;
function calculaBcc(buffer: array of byte; desloc: integer; qde: integer): integer;
function memcpy(var destino: array of byte; origem: array of byte; inicio: integer; tamanho: integer): integer;

implementation

function isHexDigit(digit: char): boolean;
var
  retorno: boolean;
begin
  retorno := True;

  if ((digit <> '0') and (digit <> '1') and (digit <> '2') and (digit <> '3')
    and (digit <> '4') and (digit <> '5') and (digit <> '6') and (digit <> '7')
    and (digit <> '8') and (digit <> '9') and (digit <> 'A') and (digit <> 'B')
    and (digit <> 'C') and (digit <> 'D') and (digit <> 'E') and (digit <> 'F')
    and (digit <> 'a') and (digit <> 'b') and (digit <> 'c') and (digit <> 'd')
    and (digit <> 'e') and (digit <> 'f')) then retorno := False;

  result := retorno;
end;

function strHexToArrayByte(str: String; var bytes: array of byte; max: integer): integer;
var
  tamString: integer;
  qdeBytes: integer;
  i: integer;
  digitoMaisSig : char;
  digitoMenosSig: char;
begin
  tamString := Length(str);

  qdeBytes := 0;

  i := 1;
  while (i < tamString) and (qdeBytes < max) do
  begin
    digitoMaisSig  := str[i];
    digitoMenosSig := str[i + 1];
    if (isHexDigit(digitoMaisSig) and isHexDigit(digitoMenosSig))
    then // É um caractere válido?
    begin
      bytes[qdeBytes] := strToInt('$' + digitoMaisSig + digitoMenosSig);
      qdeBytes := qdeBytes + 1;
    end
    else
    begin
    end;
    i := i + 2;
  end;

  result := qdeBytes;
end;

function calculaBcc(buffer: array of byte; desloc: integer; qde: integer): integer;
var
  bcc: integer;
  i: integer;

begin
  bcc := 0;

  if ((qde + desloc) > Length(buffer)) // Não há espaço suficiente?
  then
  begin
    qde := Length(buffer) - desloc; // Garante que haveja espaço suficiente.
  end;

  for i := 0 to (qde - 1) do
  begin
    bcc := bcc xor buffer[desloc + i];
  end;

  result := bcc;
end;

function memcpy(var destino: array of byte; origem: array of byte; inicio: integer; tamanho: integer): integer;
var
  i: integer;
  qdeCopiada: integer;
begin

  qdeCopiada := tamanho;

  if ((Length(destino) - inicio) < qdeCopiada) then qdeCopiada := (Length(destino) - inicio); // Evita estouro do destino.

  for i := 0 to (qdeCopiada - 1) do
  begin
    destino[inicio + i] := origem[i];
  end;

  result := qdeCopiada;
end;

end.





