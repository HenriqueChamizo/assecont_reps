unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, OleServer, Kernel7x_TLB;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    Label2: TLabel;
    edtIp: TEdit;
    edtPorta: TEdit;
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Kernel1: TKernel;
    procedure Button1Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    tIndex: Integer;
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
var
  _rConfig : SComConfig;
begin
  _rConfig.IsCatraca := False;
    _rConfig.TipoComunicacao := ctcTcpIp;
    _rConfig.ModoComunicacao := cmcOffline;
    _rConfig.Tcp.Ip := edtIp.Text;
    _rConfig.Tcp.MAC := '';
    _rConfig.Tcp.Porta := StrToInt(edtPorta.Text);
    if Kernel1.AdicionaCard[_rConfig, tIndex] then
    begin
      Kernel1.ThreadPrioridade[tIndex] := cpBaixa;
      Kernel1.SetSincronizar(tIndex, False);
      Button1.Enabled := false;
      Button2.Enabled := true;
      Button3.Enabled := true;
    end;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
  Kernel1.RemoveCard[tIndex];
  Button1.Enabled := true;
  Button2.Enabled := false;
  Button3.Enabled := false;
end;

procedure TForm1.Button2Click(Sender: TObject);
var
  arq: String;
begin
  arq := ExtractFilePath(Application.ExeName)+'registros.txt';
  if Kernel1.ColetaEventos[tIndex, arq] then
    showmessage('baixou');
end;

end.
