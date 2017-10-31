unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, OleServer, Kernel7x_TLB, ExtCtrls, Spin, Buttons,
  ComCtrls;

type
  TForm1 = class(TForm)
    Hamster1: THamster;
    btnCapturar: TButton;
    btnVerificar: TButton;
    pnlDig1: TPanel;
    pnlDig2: TPanel;
    GroupBox1: TGroupBox;
    btnCarregar: TButton;
    Label1: TLabel;
    eMatricula: TEdit;
    chkMaster: TCheckBox;
    Kernel1: TKernel;
    btnProcurar: TButton;
    Button1: TButton;
    speTempo: TSpinEdit;
    cbxAcesso: TComboBox;
    Label6: TLabel;
    Label3: TLabel;
    SpeedButton1: TSpeedButton;
    Edit1: TEdit;
    Label2: TLabel;
    StatusBar1: TStatusBar;
    BitBtn1: TBitBtn;
    procedure btnCapturarClick(Sender: TObject);
    procedure btnVerificarClick(Sender: TObject);
    procedure btnCarregarClick(Sender: TObject);
    procedure btnProcurarClick(Sender: TObject);
    Procedure proAdicionaRelogioTcp(pOnline : Boolean);
    Function  Bio_EnvTemplate(pThreadIndex: Integer; const pTemplate: WideString):WordBool;
    procedure Kernel1Registro(ASender: TObject; pThreadIndex: Integer);
    procedure Button1Click(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure SpeedButton1Click(Sender: TObject);
    procedure BitBtn1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  _uTemplate : WideString;
 _uNewIndex : Integer;
implementation

{$R *.dfm}



procedure TForm1.Kernel1Registro(ASender: TObject; pThreadIndex: Integer);
var
  _rRegistro : SRegistro;
  _rResposta : SResposta;
begin
//Pegando registro
  Kernel1.RegistroOn(pThreadIndex, _rRegistro);
//Processamento do registro
 // Memo1.Lines.Add('Matrícula: ' + _rRegistro.Matricula);
//Determinando resposta
  _rResposta.Mensagem := _rRegistro.Matricula;//eMsg.Text;
  Edit1.Text:= _rRegistro.Matricula;
  _rResposta.Tempo :=  3;//speTempo.Value;
  case cbxAcesso.ItemIndex of
    0 : _rResposta.Acesso := canNegado;
    1 : _rResposta.Acesso := canLibEntrada;
    2 : _rResposta.Acesso := canLibSaida;
    3 : _rResposta.Acesso := canRevista;
    4 : _rResposta.Acesso := canAmbosLados;
  end;
//Determinando resposta
  Kernel1.RespostaOn(pThreadIndex, _rResposta);
end;


procedure TForm1.proAdicionaRelogioTcp(pOnline : Boolean);
var
  _rConfig : SComConfig;
begin
//Adição do equipamento
  _rConfig.IsCatraca := False;
  _rConfig.TipoComunicacao := ctcTcpIp;
  if pOnline then
    _rConfig.ModoComunicacao := cmcOnOff //cmcOnline
  else
    _rConfig.ModoComunicacao := cmcOffline;
  _rConfig.Tcp.Ip := '192.168.0.200'; //eIP.Text;
  _rConfig.Tcp.MAC := '';
  _rConfig.Tcp.Porta := 3000;
  if Kernel1.AdicionaCard[_rConfig, _uNewIndex] then
  begin
    Kernel1.ThreadPrioridade[_uNewIndex] := cpBaixa;
    Kernel1.SetSincronizar(_uNewIndex, False);
 //   Memo1.Lines.Add ('Novo Relógio Tcp-IP');
  end;
end;

procedure TForm1.btnCapturarClick(Sender: TObject);
begin
  with pnlDig1 do
    Hamster1.SetImagem(Handle, Top, Left, Height, Width);
  if Hamster1.Capture[_uTemplate] then
  begin
    ShowMessage('Captura OK');
    btnVerificar.Enabled := True;
  end
  else
    ShowMessage('Falha ao capturar');
end;

procedure TForm1.btnVerificarClick(Sender: TObject);
var
existe : Boolean;
Ret : String;
WWW: wordbool;
begin
  btnVerificar.Enabled := False;
  with pnlDig2 do
    Hamster1.SetImagem(Handle, Top, Left, Height, Width);
  if Hamster1.Verify[_uTemplate] then
  begin
  Ret := Kernel1.Bio_GeraUserID[cb_S, '00003456',1,False];
   ShowMessage(Ret);

   WWW := True;
    existe:= Kernel1.Bio_UsuarioExiste[_uNewIndex,Ret,WWW];

//    Bio_UsuarioExiste(pThreadIndex: Integer; const pUsuarioID: WideString; out pExiste: WordBool): WordBool

  //  if  existe = True  Then
  //   ShowMessage('Existe')
  //   else
    ShowMessage('Verificação OK');
  end
  else
    ShowMessage('Digital não confere');
end;

procedure TForm1.btnCarregarClick(Sender: TObject);
const
//Extraido da documentação
  CARD7X_ERRO_BIO_KEY_OVERLIMIT = 601;
begin
{
  Utilizar este método para enviar a digital ao equipamento
  ou locar para match 1N
}
  _uTemplate := Hamster1.SetUser[_uTemplate, eMatricula.Text, chkMaster.Checked];
//  ShowMessage(_uTemplate);
//Armazenar em banco a variavel _uTemplate neste momento
//Adicionando template ao kernel para match
  if Kernel1.Bio_CarregaTemplate[_uTemplate] then
  begin
    ShowMessage('Template carregada');
  end
  else
    if Kernel1.KernelLastError = CARD7X_ERRO_BIO_KEY_OVERLIMIT then
      ShowMessage('Limite de carga atingido')
    else
      ShowMessage('Falha ao carregar template');
end;

procedure TForm1.btnProcurarClick(Sender: TObject);
var
  _rMatricula : string;
  _rTemplate1N : WideString;
begin
  _rMatricula := '';
  if Hamster1.Capture[_rTemplate1N] then
    _rMatricula := Kernel1.Bio_ProcuraTemplate[_rMatricula, _rTemplate1N];
  eMatricula.Text := _rMatricula;
end;

Function TForm1.Bio_EnvTemplate(pThreadIndex: Integer; const pTemplate: WideString):WordBool;
var
numero : Integer;
Template: WideString;
www : Boolean;
Begin
proAdicionaRelogioTcp(True);
//numero := pThreadIndex;
www := Kernel1.EnviaBeep[_uNewIndex, cbLiberado];

end;

procedure TForm1.Button1Click(Sender: TObject);
Var
aaa : WordBool;
bbb: Integer;
www : Boolean;
begin
Kernel1.BeginLargeTransfer(_uNewIndex);
aaa:= Kernel1.Bio_EnvTemplate[_uNewIndex,_uTemplate];
Kernel1.EndLargeTransfer(_uNewIndex);
www := Kernel1.EnviaBeep[_uNewIndex, cbLiberado];
bbb:= Kernel1.ThreadLastError[_uNewIndex];

end;
procedure TForm1.FormShow(Sender: TObject);
var
_rOnline : Boolean;
begin
_rOnline:= True;
//eIP.Text:= '192.168.0.200';
//192.168.0.200

proAdicionaRelogioTcp(_rOnline);
Kernel1.OnRegistro := Kernel1Registro;
end;
procedure TForm1.SpeedButton1Click(Sender: TObject);
var
_rOnOffline : Boolean;
bbb: Integer;
www : Boolean;
Ret : String;
begin

Ret := Kernel1.Bio_GeraUserID[cb_S, '00003456',1,False];
ShowMessage(Ret);

Kernel1.BeginLargeTransfer(_uNewIndex);
_rOnOffline:= Kernel1.Bio_DelTemplate[_uNewIndex,Ret,True];
//_rOnOffline:= Kernel1.Bio_DelTemplateTodas[_uNewIndex];
www := Kernel1.EnviaBeep[_uNewIndex, cbLiberado];
Kernel1.EndLargeTransfer(_uNewIndex);

//bbb:= Kernel1.ThreadLastError[_uNewIndex];
///ShowMessage(IntToStr(bbb));
end;
procedure TForm1.BitBtn1Click(Sender: TObject);
begin
Close;
end;
end.
