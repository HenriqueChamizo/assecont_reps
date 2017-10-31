unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, OleServer, Kernel7x_TLB, StdCtrls, ExtCtrls, Spin, ComCtrls, Buttons,
  CheckLst;

type
  TForm1 = class(TForm)
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    Label4: TLabel;
    Label5: TLabel;
    btnAddSerial: TButton;
    ePorta: TEdit;
    eNumero: TEdit;
    Button2: TButton;
    TabSheet2: TTabSheet;
    Label1: TLabel;
    eIP: TEdit;
    btnAddTcp: TButton;
    Button3: TButton;
    btnColetar: TButton;
    Memo1: TMemo;
    GroupBox1: TGroupBox;
    Label2: TLabel;
    Label3: TLabel;
    Label6: TLabel;
    eMsg: TEdit;
    speTempo: TSpinEdit;
    cbxAcesso: TComboBox;
    TabSheet3: TTabSheet;
    Label7: TLabel;
    edtPorta: TEdit;
    btnAdd485: TButton;
    Label8: TLabel;
    spdNumRel: TSpeedButton;
    Label9: TLabel;
    cbxVelocidade: TComboBox;
    cbxRel: TComboBox;
    procedure spdNumRelClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure btnAddSerialClick(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure btnColetarClick(Sender: TObject);
    procedure Kernel1Registro(ASender: TObject; pThreadIndex: Integer);
  private
    procedure proAdicionaRelogioSerial485(pOnline : Boolean);
    procedure proAdicionaRelogioSerial(pOnline : Boolean);
    procedure proAdicionaRelogioTcp(pOnline : Boolean);
    { Private declarations }
  public
    _uNewIndex : Integer;
    { Public declarations }
  end;

var
  Form1: TForm1;
  Kernel1 : TKernel;

implementation

uses ComObj, Unit2;

{$R *.dfm}

procedure TForm1.proAdicionaRelogioTcp(pOnline : Boolean);
var
  _rConfig : SComConfig;
begin
//Adição do equipamento
  _rConfig.IsCatraca := False;
  _rConfig.TipoComunicacao := ctcTcpIp;
  if pOnline then
    _rConfig.ModoComunicacao := cmcOnline
  else
    _rConfig.ModoComunicacao := cmcOffline;
  _rConfig.Tcp.Ip := eIP.Text;
  _rConfig.Tcp.MAC := '';
  _rConfig.Tcp.Porta := 3000;
  if Kernel1.AdicionaCard[_rConfig, _uNewIndex] then
  begin
    Kernel1.ThreadPrioridade[_uNewIndex] := cpBaixa;
    Kernel1.SetSincronizar(_uNewIndex, False);
    Memo1.Lines.Add ('Novo Relógio Tcp-IP');
  end;
end;

procedure TForm1.spdNumRelClick(Sender: TObject);
begin
  if Form2 = nil then
    Form2 := TForm2.Create(nil);
  form2.ShowModal;
end;

procedure TForm1.proAdicionaRelogioSerial485(pOnline: Boolean);
var
  _rConfig : SComConfig;
  i : byte;
begin
_rConfig.Tcp.Porta := 0;


  _rConfig.IsCatraca := false;
  _rConfig.TipoComunicacao := ctcSerial485;
  if pOnline then
    _rConfig.ModoComunicacao := cmcOnline
  else
    _rConfig.ModoComunicacao := cmcOffline;
  _rConfig.Serial.Porta := edtPorta.Text;
  for i := 0 to Form2.cbx485.Items.Count - 1 do
    if Form2.cbx485.Checked[i] then
    begin
      _rConfig.Serial.NumeroRelogio := i + 1;
      case cbxVelocidade.ItemIndex of
        0 : _rConfig.Serial.Velocidade := cv9600;
        1 : _rConfig.Serial.Velocidade := cv19200;
        2 : _rConfig.Serial.Velocidade := cv57600;
        3 : _rConfig.Serial.Velocidade := cv115200;
      else
        Memo1.Lines.Add('Escolha uma velocidade');
      end;
      if Kernel1.AdicionaCard[_rConfig, _uNewIndex] then
      begin
        Kernel1.ThreadPrioridade[_uNewIndex] := cpBaixa;
        Kernel1.SetSincronizar(_uNewIndex, False);
        Memo1.Lines.Add ('Novo Relógio Serial485 ' + 
                  FormatFloat('00', _rConfig.Serial.NumeroRelogio));
        cbxRel.Items.Add(FormatFloat('00', _rConfig.Serial.NumeroRelogio));
        cbxRel.ItemIndex := 0;
      end;
    end;
end;

procedure TForm1.proAdicionaRelogioSerial(pOnline : Boolean);
var
  _rConfig : SComConfig;
begin
//Adição do equipamento
  _rConfig.IsCatraca := False;
  _rConfig.TipoComunicacao := ctcSerial;
  if pOnline then
    _rConfig.ModoComunicacao := cmcOnline
  else
    _rConfig.ModoComunicacao := cmcOffline;
  _rConfig.Serial.Porta := ePorta.Text;
  _rConfig.Serial.NumeroRelogio := StrtoInt(eNumero.Text);
  _rConfig.Serial.Velocidade := cv115200;
  if Kernel1.AdicionaCard[_rConfig, _uNewIndex] then
  begin
    Kernel1.ThreadPrioridade[_uNewIndex] := cpBaixa;
    Kernel1.SetSincronizar(_uNewIndex, False);
    Memo1.Lines.Add ('Novo Relógio Serial');
  end;
end;

procedure TForm1.btnAddSerialClick(Sender: TObject);
var
  _rOnline : Boolean;
begin
  _rOnline := (MessageDlg('Deseja comunicar online?', mtConfirmation, [mbYes, mbNo], 0) = ID_YES);
  case TButton(Sender).Tag of
    0 : proAdicionaRelogioSerial(_rOnline);
    1 : proAdicionaRelogioTcp(_rOnline);
    3 : proAdicionaRelogioSerial485(_rOnline);
  end;  
  TButton(Sender).Enabled := False;
end;

procedure TForm1.Button2Click(Sender: TObject);
var
  _rBaudRate : SVelocidade;
  _rNumRel : integer;
begin
{
  Detecta velocidade de comunicação do equipamento Serial
  Necessita do número e porta corretos
}
  TButton(Sender).Enabled := False;
  if cbxRel.ItemIndex > -1 then
  begin
    _rNumRel := StrToInt(cbxRel.Text);
    Kernel1.Set485OffNumber[_uNewIndex, _rNumRel];
  end;
  if Kernel1.DetectarVelocidade[_uNewIndex, _rBaudRate] then
    Memo1.Lines.Add ('Velocidade Detectada')
  else
    ShowMessage('Não foi detectada a velocidade');
  TButton(Sender).Enabled := True;    
end;

procedure TForm1.Button3Click(Sender: TObject);
var
  _rQtRegs : Integer;
  _rNumRel : integer;
begin
  if cbxRel.ItemIndex > -1 then
  begin
    _rNumRel := StrToInt(cbxRel.Text);
    Kernel1.Set485OffNumber[_uNewIndex, _rNumRel];
  end;
  btnColetar.Enabled := Kernel1.RecebeQtRegistros[_uNewIndex, _rQtRegs]; 
  if btnColetar.Enabled then
    Memo1.Lines.Add(Format('Registros existentes: %d', [_rQtRegs]));
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  Kernel1 := TKernel.Create(Self);
  Kernel1.OnRegistro := Kernel1Registro;
end;

procedure TForm1.btnColetarClick(Sender: TObject);
var
  _rRegistro : SRegistro;
begin
  TButton(Sender).Enabled := True;
  while Kernel1.RecebePacote[_uNewIndex] do
  begin
    while Kernel1.RegistroOff[_uNewIndex, _rRegistro] do
      Memo1.Lines.Add('Matrícula: ' + _rRegistro.Matricula);
    if not Kernel1.ApagaUltimoPacote[_uNewIndex] then
    begin
      Memo1.Lines.Add('Falha ao excluir');
      Break;      
    end
    else
      Memo1.Lines.Add('Pacote excluido');
  end;
end;

procedure TForm1.Kernel1Registro(ASender: TObject; pThreadIndex: Integer);
var
  _rRegistro : SRegistro;
  _rResposta : SResposta;
begin
//Pegando registro
  Kernel1.RegistroOn(pThreadIndex, _rRegistro);
//Processamento do registro
  Memo1.Lines.Add('Matrícula: ' + _rRegistro.Matricula);
//Determinando resposta
  _rResposta.Mensagem := eMsg.Text;
  _rResposta.Tempo := speTempo.Value;
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

end.
