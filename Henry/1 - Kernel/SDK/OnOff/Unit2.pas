unit Unit2;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, Buttons, CheckLst;

type
  TForm2 = class(TForm)
    Panel1: TPanel;
    spdAccept: TSpeedButton;
    cbx485: TCheckListBox;
    procedure FormKeyUp(Sender: TObject; var Key: Word; Shift: TShiftState);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormCreate(Sender: TObject);
    procedure spdAcceptClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form2: TForm2;

implementation

uses Unit1;

{$R *.dfm}

procedure TForm2.FormClose(Sender: TObject; var Action: TCloseAction);
var
  i : integer;
begin
  for i := 0 to cbx485.Items.Count - 1 do
    if cbx485.Checked[i] then
      Form1.btnAdd485.Enabled := true;
end;

procedure TForm2.FormCreate(Sender: TObject);
var
  i : integer;
  _rStrAux : string;
begin
  cbx485.Items.Clear;
  for i := 1 to 85 do
  begin
    _rStrAux := FormatFloat('00', i);
    cbx485.Items.Add(_rStrAux);
  end;
end;

procedure TForm2.FormKeyUp(Sender: TObject; var Key: Word; Shift: TShiftState);
begin
  if (Key = VK_ESCAPE) or (key = VK_RETURN) then
    spdAccept.Click;
end;

procedure TForm2.spdAcceptClick(Sender: TObject);
begin
  close;
end;

end.
