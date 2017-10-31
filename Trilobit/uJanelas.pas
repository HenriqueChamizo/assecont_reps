unit uJanelas;

interface

uses Forms, SysUtils, Controls;

function Abrir_Rep_Edicao(Indice: integer; Grupo: integer; PortaDefault: integer = 0): boolean;

implementation

uses pRepEdicao;

function Abrir_Rep_Edicao(Indice: integer; Grupo: integer; PortaDefault: integer = 0): boolean;
begin
Application.CreateForm(TfrmRepEdicao, frmRepEdicao);
frmRepEdicao.WFormTable1.Indice := Indice;

if Indice = 0 then
   if PortaDefault <> 0 then
      frmRepEdicao.edPorta.Text := IntToStr(PortaDefault);

Result := frmRepEdicao.ShowModal = mrOk;
FreeAndNil(frmRepEdicao);
end;

end.
