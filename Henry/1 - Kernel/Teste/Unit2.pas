unit Unit2;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, OleServer, Kernel7x_TLB, StdCtrls;

type
  TForm2 = class(TForm)
    Kernel1: TKernel;
    Button1: TButton;
    Button2: TButton;
    edArquivo: TEdit;
    Label1: TLabel;
    edIp: TEdit;
    Label2: TLabel;
    edPorta: TEdit;
    Porta: TLabel;
    lbMensagem: TLabel;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Kernel1ColetaEventos(ASender: TObject; pThreadIndex: Integer;
      pResultado: WordBool; pQtdeEventosColetados: Integer;
      const pPathAFD: WideString);
    procedure Kernel1Progresso(ASender: TObject; pThreadIndex, pByte, pByteMax,
      pBuffer, pBufferMax: Integer);
  private
    _rConfig : SComConfig;
    IndexTerminal: integer;
    procedure Conectar;
  public
  end;

var
  Form2: TForm2;

implementation

{$R *.dfm}

procedure TForm2.Button1Click(Sender: TObject);
begin
Conectar;
end;

procedure TForm2.Button2Click(Sender: TObject);
var
  R: boolean;
begin
R := Kernel1.ColetaEventos[IndexTerminal, edArquivo.Text];

if R then
   lbMensagem.Caption := 'Coletando...'
else
   lbMensagem.Caption := 'Erro';
end;

procedure TForm2.Conectar;
var
  R: boolean;
begin
_rConfig.IsCatraca := False;
_rConfig.TipoComunicacao := ctcTcpIp;
_rConfig.ModoComunicacao := cmcOffline;
_rConfig.Tcp.Ip := edIp.Text;
_rConfig.Tcp.MAC := '';
_rConfig.Tcp.Porta := StrToInt(edPorta.Text);

R := kernel1.AdicionaCard[_rConfig, IndexTerminal];

if R then
   lbMensagem.Caption := 'Ok'
else
   lbMensagem.Caption := 'Erro';
end;

procedure TForm2.Kernel1ColetaEventos(ASender: TObject; pThreadIndex: Integer;
  pResultado: WordBool; pQtdeEventosColetados: Integer;
  const pPathAFD: WideString);
begin
lbMensagem.Caption := 'Registros: ' + IntToStr(pQtdeEventosColetados) + ', Local do arquivo: ' + pPathAFD;
end;

procedure TForm2.Kernel1Progresso(ASender: TObject; pThreadIndex, pByte,
  pByteMax, pBuffer, pBufferMax: Integer);
begin
//  lbMensagem.Caption := IntToStr(pByte);
end;

end.
