unit pProgressoImportacao;

interface

uses
  Windows, Forms, StdCtrls, SysUtils, Controls, Classes, ComCtrls;

type
  TfrmProgressoImportacao = class(TForm)
    ProgressBar1: TProgressBar;
    lbMensagem: TLabel;
    Button1: TButton;
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure FormCloseQuery(Sender: TObject; var CanClose: Boolean);
  private
    FBloco: integer;
    procedure SetBloco(Value: integer);
  public
    Terminou: boolean;
    property Bloco: integer read FBloco write SetBloco;
  end;

var
  frmProgressoImportacao: TfrmProgressoImportacao;

implementation

uses pMain;

{$R *.dfm}

procedure TfrmProgressoImportacao.SetBloco(Value: integer);
begin
FBloco := Value;
lbMensagem.Caption := 'Importando Bloco ' + IntToStr(Bloco);
end;

procedure TfrmProgressoImportacao.Button1Click(Sender: TObject);
begin
Close;
end;

procedure TfrmProgressoImportacao.FormClose(Sender: TObject;
  var Action: TCloseAction);
begin
Action := caFree;
frmProgressoImportacao := NIL;
end;

procedure TfrmProgressoImportacao.FormCloseQuery(Sender: TObject;
  var CanClose: Boolean);
begin
if Terminou then
   CanClose := True
else
    if Application.MessageBox(
                          'Interromper a importação de dados?', 'Confirmação',
                          mb_IconQuestion + mb_YesNo
                          ) = mrYes then
       begin
       frmMain.PararColeta;
       CanClose := True;
       end;
end;

procedure TfrmProgressoImportacao.FormCreate(Sender: TObject);
begin
Bloco := 1;
Terminou := False;
end;

end.
