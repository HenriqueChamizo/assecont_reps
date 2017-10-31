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
     If Texto[Contador] = '�' Then Texto[Contador] := 'a';
     If Texto[Contador] = '�' Then Texto[Contador] := 'e';
     If Texto[Contador] = '�' Then Texto[Contador] := 'i';
     If Texto[Contador] = '�' Then Texto[Contador] := 'o';
     If Texto[Contador] = '�' Then Texto[Contador] := 'u';
     If Texto[Contador] = '�' Then Texto[Contador] := 'a';
     If Texto[Contador] = '�' Then Texto[Contador] := 'o';
     If Texto[Contador] = '�' Then Texto[Contador] := 'a';
     If Texto[Contador] = '�' Then Texto[Contador] := 'e';
     If Texto[Contador] = '�' Then Texto[Contador] := 'i';
     If Texto[Contador] = '�' Then Texto[Contador] := 'o';
     If Texto[Contador] = '�' Then Texto[Contador] := 'u';
     If Texto[Contador] = '�' Then Texto[Contador] := 'a';
     If Texto[Contador] = '�' Then Texto[Contador] := 'e';
     If Texto[Contador] = '�' Then Texto[Contador] := 'i';
     If Texto[Contador] = '�' Then Texto[Contador] := 'o';
     If Texto[Contador] = '�' Then Texto[Contador] := 'u';
     If Texto[Contador] = '�' Then Texto[Contador] := 'a';
     If Texto[Contador] = '�' Then Texto[Contador] := 'e';
     If Texto[Contador] = '�' Then Texto[Contador] := 'i';
     If Texto[Contador] = '�' Then Texto[Contador] := 'o';
     If Texto[Contador] = '�' Then Texto[Contador] := 'u';
     If Texto[Contador] = '�' Then Texto[Contador] := 'c';

     If Texto[Contador] = '�' Then Texto[Contador] := 'A';
     If Texto[Contador] = '�' Then Texto[Contador] := 'E';
     If Texto[Contador] = '�' Then Texto[Contador] := 'I';
     If Texto[Contador] = '�' Then Texto[Contador] := 'O';
     If Texto[Contador] = '�' Then Texto[Contador] := 'U';
     If Texto[Contador] = '�' Then Texto[Contador] := 'A';
     If Texto[Contador] = '�' Then Texto[Contador] := 'O';
     If Texto[Contador] = '�' Then Texto[Contador] := 'A';
     If Texto[Contador] = '�' Then Texto[Contador] := 'E';
     If Texto[Contador] = '�' Then Texto[Contador] := 'I';
     If Texto[Contador] = '�' Then Texto[Contador] := 'O';
     If Texto[Contador] = '�' Then Texto[Contador] := 'U';
     If Texto[Contador] = '�' Then Texto[Contador] := 'A';
     If Texto[Contador] = '�' Then Texto[Contador] := 'E';
     If Texto[Contador] = '�' Then Texto[Contador] := 'I';
     If Texto[Contador] = '�' Then Texto[Contador] := 'O';
     If Texto[Contador] = '�' Then Texto[Contador] := 'U';
     If Texto[Contador] = '�' Then Texto[Contador] := 'A';
     If Texto[Contador] = '�' Then Texto[Contador] := 'E';
     If Texto[Contador] = '�' Then Texto[Contador] := 'I';
     If Texto[Contador] = '�' Then Texto[Contador] := 'O';
     If Texto[Contador] = '�' Then Texto[Contador] := 'U';
     If Texto[Contador] = '�' Then Texto[Contador] := 'C';

     If Texto[Contador] = '�' Then Texto[Contador] := '.';
     If Texto[Contador] = '�' Then Texto[Contador] := '.';
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
