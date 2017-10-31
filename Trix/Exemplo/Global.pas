unit Global;

interface
  function IntToHexa(Num: Word): String;
  Function GeraCRC(S:String) : String;
  function AlinhaEsq(Texto :String; Casas : Byte; Carac : String):string;

implementation

Uses
  StrUtils, Windows, SysUtils;

Function Tira_Acento(Texto : String) : String;
Var Contador : Word;
Begin
  For Contador := 1 to Length(Texto) do
  Begin
     If Texto[Contador] = 'â' Then Texto[Contador] := 'a';
     If Texto[Contador] = 'ê' Then Texto[Contador] := 'e';
     If Texto[Contador] = 'î' Then Texto[Contador] := 'i';
     If Texto[Contador] = 'ô' Then Texto[Contador] := 'o';
     If Texto[Contador] = 'û' Then Texto[Contador] := 'u';
     If Texto[Contador] = 'ã' Then Texto[Contador] := 'a';
     If Texto[Contador] = 'õ' Then Texto[Contador] := 'o';
     If Texto[Contador] = 'ä' Then Texto[Contador] := 'a';
     If Texto[Contador] = 'ë' Then Texto[Contador] := 'e';
     If Texto[Contador] = 'ï' Then Texto[Contador] := 'i';
     If Texto[Contador] = 'ö' Then Texto[Contador] := 'o';
     If Texto[Contador] = 'ü' Then Texto[Contador] := 'u';
     If Texto[Contador] = 'á' Then Texto[Contador] := 'a';
     If Texto[Contador] = 'é' Then Texto[Contador] := 'e';
     If Texto[Contador] = 'í' Then Texto[Contador] := 'i';
     If Texto[Contador] = 'ó' Then Texto[Contador] := 'o';
     If Texto[Contador] = 'ú' Then Texto[Contador] := 'u';
     If Texto[Contador] = 'à' Then Texto[Contador] := 'a';
     If Texto[Contador] = 'è' Then Texto[Contador] := 'e';
     If Texto[Contador] = 'ì' Then Texto[Contador] := 'i';
     If Texto[Contador] = 'ò' Then Texto[Contador] := 'o';
     If Texto[Contador] = 'ù' Then Texto[Contador] := 'u';
     If Texto[Contador] = 'ç' Then Texto[Contador] := 'c';

     If Texto[Contador] = 'Â' Then Texto[Contador] := 'A';
     If Texto[Contador] = 'Ê' Then Texto[Contador] := 'E';
     If Texto[Contador] = 'Î' Then Texto[Contador] := 'I';
     If Texto[Contador] = 'Ô' Then Texto[Contador] := 'O';
     If Texto[Contador] = 'Û' Then Texto[Contador] := 'U';
     If Texto[Contador] = 'Ã' Then Texto[Contador] := 'A';
     If Texto[Contador] = 'Õ' Then Texto[Contador] := 'O';
     If Texto[Contador] = 'Ä' Then Texto[Contador] := 'A';
     If Texto[Contador] = 'Ë' Then Texto[Contador] := 'E';
     If Texto[Contador] = 'Ï' Then Texto[Contador] := 'I';
     If Texto[Contador] = 'Ö' Then Texto[Contador] := 'O';
     If Texto[Contador] = 'Ü' Then Texto[Contador] := 'U';
     If Texto[Contador] = 'Á' Then Texto[Contador] := 'A';
     If Texto[Contador] = 'É' Then Texto[Contador] := 'E';
     If Texto[Contador] = 'Í' Then Texto[Contador] := 'I';
     If Texto[Contador] = 'Ó' Then Texto[Contador] := 'O';
     If Texto[Contador] = 'Ú' Then Texto[Contador] := 'U';
     If Texto[Contador] = 'À' Then Texto[Contador] := 'A';
     If Texto[Contador] = 'È' Then Texto[Contador] := 'E';
     If Texto[Contador] = 'Ì' Then Texto[Contador] := 'I';
     If Texto[Contador] = 'Ò' Then Texto[Contador] := 'O';
     If Texto[Contador] = 'Ù' Then Texto[Contador] := 'U';
     If Texto[Contador] = 'Ç' Then Texto[Contador] := 'C';

     If Texto[Contador] = 'º' Then Texto[Contador] := '.';
     If Texto[Contador] = 'ª' Then Texto[Contador] := '.';
  End;
  Result := Texto;
End;

function AlinhaEsq(Texto :String; Casas : Byte; Carac : String):string;
var Tamanho, Contador:integer;
Begin
  Texto := Trim(Texto);
  Tamanho := length(Texto);
  If not(Tamanho > Casas) Then
  Begin
    for Contador:= 1 to (Casas - Tamanho) do
      Texto := Texto + Carac;
  End;
  result := Copy(Tira_Acento(Texto),1,Casas);
End;

function IntToHexa(Num: Word): String;
//Converte um numero em Hexadecimal
Var
  L : string[16];
  BHi,BLo : byte;
Begin
  L := '0123456789ABCDEF';
  BHi := Hi(Num);
  BLo := Lo(Num);
  result := copy(L,succ(BHi shr 4),1) +
            copy(L,succ(BHi and 15),1) +
            copy(L,succ(BLo shr 4),1) +
            copy(L,succ(BLo and 15),1);
end;

Function GeraCRC(S:String) : String;
Var
  I, Int   : Integer;
  CRC : String;
Begin
  Int := 0;
  for I := 1 to Length(S) do
    Int := Int + Ord(S[I]);
  Int := Int mod 256;     //1220
  CRC := IntToHexa(Int);
  while (Copy(CRC,1,1) = '0') or (Copy(CRC,1,1) = ' ')do
    CRC := Copy(CRC,2,10);
  Result := S + CRC;
End;

end.
