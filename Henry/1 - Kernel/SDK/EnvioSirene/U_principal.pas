unit U_principal;

interface

uses
  Kernel7x_TLB,
  Windows, Messages, SysUtils,  Forms, CheckLst, StdCtrls, Controls, Spin,
  ComCtrls, ExtCtrls, Classes, dialogs;



type
  TfrmMain = class(TForm)
    stbMain: TStatusBar;
    Panel1: TPanel;
    GroupBox1: TGroupBox;
    dtkTime: TDateTimePicker;
    speTempo: TSpinEdit;
    Label1: TLabel;
    Label2: TLabel;
    chklSemana: TCheckListBox;
    btnEnviar: TButton;
    edtIp: TEdit;
    Label3: TLabel;
    btnOk: TButton;
    btnIncluir: TButton;
    Timer1: TTimer;
    ProgressBar1: TProgressBar;
    PageControl: TPageControl;
    TabSheet1: TTabSheet;
    TabSheet2: TTabSheet;
    Panel: TPanel;
    Label4: TLabel;
    GroupBox: TGroupBox;
    Label5: TLabel;
    Label6: TLabel;
    dtkTimeSerial: TDateTimePicker;
    speTempoSerial: TSpinEdit;
    chklSemanaSerial: TCheckListBox;
    btnEnviarSerial: TButton;
    btnIncluirSerial: TButton;
    edtSerial: TEdit;
    btnOkSerial: TButton;
    Label7: TLabel;
    edtNumRel: TEdit;
    Label8: TLabel;
    lblVelocidade: TLabel;
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure Incluir(Sender: TObject);
    procedure btnOkClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure btnEnviarClick(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure stbMainDrawPanel(StatusBar: TStatusBar; Panel: TStatusPanel;
      const Rect: TRect);
    procedure TabSheet1Show(Sender: TObject);
    procedure TabSheet2Show(Sender: TObject);
  private
    _uNewIndex : integer;
    procedure proIncluir;
    procedure proEnviar;
    procedure proProgresso(ASender: TObject; pThreadIndex, pByte,
      pByteMax, pBuffer, pBufferMax: Integer);
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Kernel1 : TKernel;
  frmMain: TfrmMain;

implementation

{$R *.dfm}

procedure TfrmMain.proIncluir;
var
  _rSirene : SAcionamento;
begin
  if PageControl.ActivePageIndex = 1 then
  begin
    _rSirene.Dias.Domingo := chklSemana.Checked[0];
    _rSirene.Dias.Segunda := chklSemana.Checked[1];
    _rSirene.Dias.Terca := chklSemana.Checked[2];
    _rSirene.Dias.Quarta := chklSemana.Checked[3];
    _rSirene.Dias.Quinta := chklSemana.Checked[4];
    _rSirene.Dias.Sexta := chklSemana.Checked[5];
    _rSirene.Dias.Sabado := chklSemana.Checked[6];
    _rSirene.Dias.Feriado := chklSemana.Checked[7];
    _rSirene.Horario := dtkTime.Time;
    _rSirene.Tempo := StrToInt(speTempo.Text);
    Kernel1.Add_Acionamento(_uNewIndex, _rSirene);
    stbMain.Panels[0].Style := psText;
    stbMain.Panels[0].Text := 'Incluindo...';
    Timer1.Enabled := true;
  end
  else
  begin
    _rSirene.Dias.Domingo := chklSemanaSerial.Checked[0];
    _rSirene.Dias.Segunda := chklSemanaSerial.Checked[1];
    _rSirene.Dias.Terca := chklSemanaSerial.Checked[2];
    _rSirene.Dias.Quarta := chklSemanaSerial.Checked[3];
    _rSirene.Dias.Quinta := chklSemanaSerial.Checked[4];
    _rSirene.Dias.Sexta := chklSemanaSerial.Checked[5];
    _rSirene.Dias.Sabado := chklSemanaSerial.Checked[6];
    _rSirene.Dias.Feriado := chklSemanaSerial.Checked[7];
    _rSirene.Horario := dtkTimeSerial.Time;
    _rSirene.Tempo := StrToInt(speTempoSerial.Text);
    Kernel1.Add_Acionamento(_uNewIndex, _rSirene);
    stbMain.Panels[0].Style := psText;
    stbMain.Panels[0].Text := 'Incluindo...';
    Timer1.Enabled := true;
  end;
end;

procedure TfrmMain.proEnviar;
var
  _rPart : SParticionamento;
  _rConf : SComConfig;
begin
  try
    Kernel1.BeginLargeTransfer(_uNewIndex);
    Kernel1.RecebeParticionamento[_uNewIndex, _rPart];
    if _rPart.Acionamentos >= 0 then
    begin
      if Kernel1.EnviaAcionamentos[_uNewIndex] then
      begin
        stbMain.Panels[0].Style := psText;
        stbMain.Panels[0].Text := 'Enviado com sucesso';
      end
      else
      begin
        stbMain.Panels[0].Style := psText;
        stbMain.Panels[0].Text := 'Falha ao enviar acionamentos';
        btnEnviar.Enabled := false;
        btnEnviarSerial.Enabled := btnEnviar.Enabled;
      end;
    end
    else
    begin
      ShowMessage('Seu equipamento não está formatado!');
      stbMain.Panels[0].Style := psText;
      stbMain.Panels[0].Text := '';
    end;
  except
    ShowMessage('Verifique a instalação do seu dispositivo e sua conecção!');
  end;
end;

procedure TfrmMain.btnEnviarClick(Sender: TObject);
begin
  proEnviar;
end;

procedure TfrmMain.btnOkClick(Sender: TObject);
var
  _rConfig : SComConfig;
begin
  if PageControl.ActivePageIndex = 1 then
  begin
    _rConfig.IsCatraca := true;
    _rConfig.TipoComunicacao := ctcTcpIp;
    _rConfig.ModoComunicacao := cmcOffline;
    _rConfig.Tcp.Ip := edtIp.Text;
    _rConfig.Tcp.MAC := '';
    _rConfig.Tcp.Porta := 3000;
    if Kernel1.AdicionaCard[_rConfig, _uNewIndex] then
    begin
      Kernel1.ThreadPrioridade[_uNewIndex] := cpNormal;
      Kernel1.SetSincronizar(_uNewIndex, False);
      btnIncluir.Enabled := true;
    end;
  end
  else
  begin
    _rConfig.IsCatraca := false;
    _rConfig.TipoComunicacao := ctcSerial;
    _rConfig.ModoComunicacao := cmcOffline;
    _rConfig.Serial.Porta := edtSerial.Text;
    _rConfig.Serial.NumeroRelogio := StrToInt(edtNumRel.Text);
    _rConfig.Serial.Velocidade := cv9600;
    if Kernel1.AdicionaCard[_rConfig, _uNewIndex] then
    begin
      Kernel1.ThreadPrioridade[_uNewIndex] := cpBaixa;
      Kernel1.SetSincronizar(_uNewIndex, false);
      Kernel1.OnProgresso := nil;
      stbMain.Panels[0].Style := psText;
      stbMain.Panels[0].Text := '>>>> Detectando velocidade';
    end;
    Kernel1.DetectarVelocidade[_uNewIndex, _rConfig.Serial.Velocidade];
    case _rConfig.Serial.Velocidade of
      cv9600 : lblVelocidade.Caption := 'cv9600';
      cv19200 : lblVelocidade.Caption := 'cv19200';
      cv57600 : lblVelocidade.Caption := 'cv57600';
      cv115200 : lblVelocidade.Caption := 'cv115200';
    end;
    stbMain.Panels[0].Style := psText;
    stbMain.Panels[0].Text := '';
    btnIncluirSerial.Enabled := true;
    Kernel1.OnProgresso := proProgresso;
  end;
end;

procedure TfrmMain.Incluir(Sender: TObject);
begin
  btnIncluir.Enabled := false;
  btnIncluirSerial.Enabled := btnIncluir.Enabled;
  try
    proIncluir;
  except
    btnEnviar.Enabled := false;
    btnEnviarSerial.Enabled := btnEnviar.Enabled;
  end;
end;

procedure TfrmMain.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  FreeAndNil(Kernel1);
end;

procedure TfrmMain.FormCreate(Sender: TObject);
begin
  Kernel1 := TKernel.Create(Self);
  dtkTime.Time := StrToTime('00:00:00');
  dtkTimeSerial.Time := StrToTime('00:00:00');
  Kernel1.OnProgresso := proProgresso;
end;

procedure TfrmMain.Timer1Timer(Sender: TObject);
begin
  Timer1.Enabled := false;
  btnEnviar.Enabled := true;
  btnEnviarSerial.Enabled := btnEnviar.Enabled;
  btnIncluir.Enabled := true;
  btnIncluirSerial.Enabled := btnIncluir.Enabled;
  stbMain.Panels[0].Text := '';
end;

procedure TfrmMain.proProgresso(ASender: TObject; pThreadIndex, pByte,
  pByteMax, pBuffer, pBufferMax: Integer);
begin
  stbMain.Panels[0].Style := psOwnerDraw;
  ProgressBar1.Max := pByteMax;
  ProgressBar1.Position := pByte;
  stbMain.Repaint;

  //Repinta a StatusBar para forçar a atualização visual
  stbMain.Repaint;
  Application.ProcessMessages;
end;

procedure TfrmMain.stbMainDrawPanel(StatusBar: TStatusBar;
  Panel: TStatusPanel; const Rect: TRect);
begin
  ProgressBar1.Width := Rect.Right - Rect.Left +1;
  ProgressBar1.Height := Rect.Bottom - Rect.Top +1;

  ProgressBar1.PaintTo(StatusBar.Canvas.Handle, Rect.Left, Rect.Top);
end;

procedure TfrmMain.TabSheet1Show(Sender: TObject);
begin
  frmMain.Height := 317;
  frmMain.Width := 370;
end;

procedure TfrmMain.TabSheet2Show(Sender: TObject);
begin
  frmMain.Height := 317;
  frmMain.Width := 224;
end;

end.
