{===============================================================================
|                                                                              |
|   HENRY EQUIPAMENTOS E SISTEMAS LTDA.                                        |
|                                                                              |
|   Data : 15/05/2009                                                          |
|   Autor : Eduardo de Andrade                                                 |
|                                                                              |
|   Este exemplo demonstra a utilização da função de captura e envio de digital|
| ao equipamento.                                                              |
|                                                                              |
===============================================================================}

unit uCapturaEnviaTemplate;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, kernel7x_tlb, StdCtrls, ExtCtrls, ComCtrls, Mask;



type
  TfrmCapturaImagem = class(TForm)
    pnlFundo: TPanel;
    stbrHint: TStatusBar;
    Panel1: TPanel;
    pnlDig: TPanel;
    Panel3: TPanel;
    Panel4: TPanel;
    btnConectar: TButton;
    btnDesconectar: TButton;
    pgctrlConexao: TPageControl;
    tsTcpIP: TTabSheet;
    Label7: TLabel;
    Label6: TLabel;
    edtPortaTCP: TEdit;
    edtIP: TMaskEdit;
    tsSerial: TTabSheet;
    Label8: TLabel;
    Label9: TLabel;
    edtNrelogio: TEdit;
    cbxPortaCom: TComboBox;
    rgVelocidade: TRadioGroup;
    Panel5: TPanel;
    Label5: TLabel;
    btnCapturar: TButton;
    btnEnviar: TButton;
    chkMaster: TCheckBox;
    edtMatricula: TEdit;
    lblMat: TLabel;
    procedure btnEnviarClick(Sender: TObject);
    procedure btnDesconectarClick(Sender: TObject);
    procedure btnConectarClick(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormShow(Sender: TObject);
    procedure btnCapturarClick(Sender: TObject);
  private
    { Private declarations }
    _gKernel : TKernel;
    _gHamster : THamster;

    _uThreadIndex : integer;
    _uTempTemplate : WideString;

    procedure proAtualizaStatus(pMensagem: string);

  public
    { Public declarations }
  end;

var
  frmCapturaImagem: TfrmCapturaImagem;

implementation

{$R *.dfm}


procedure TfrmCapturaImagem.proAtualizaStatus(pMensagem: string);
begin
  stbrHint.SimpleText := pMensagem;
end;


procedure TfrmCapturaImagem.FormShow(Sender: TObject);
begin
  //instanciando kernel e hamster
  _gHamster := THamster.Create(self);
  _gKernel := TKernel.Create(self);
end;

procedure TfrmCapturaImagem.FormClose(Sender: TObject;
  var Action: TCloseAction);
begin
  //eliminando instância dos objetos
  FreeAndNil(_gHamster);
  FreeAndNil(_gKernel);
end;

procedure TfrmCapturaImagem.btnConectarClick(Sender: TObject);
var
  _rComConfig : SComConfig;          //Armazenará as configurações de conexão
  _rIdxEquip : Integer;              //Armazenará a id da thread
  _rConfiguracao : SConfiguracao;

begin
  with _rComConfig do
  begin
    IsCatraca := False;                         //Seta configurações
    ModoComunicacao := cmcOffline;
    case pgctrlConexao.ActivePageIndex of       //Verifica tipo de comunicação
      0: begin
           TipoComunicacao := ctcTcpIp;
           Tcp.Ip := StringReplace(edtIP.Text, ' ', '', [rfReplaceAll]);
           Tcp.MAC := '';
           Tcp.Porta := StrToInt(edtPortaTCP.Text);
           Serial.NumeroRelogio := 0;
           Serial.Porta := '';
           Serial.Velocidade := cv9600;
           Modem.Fone := '';
           Modem.Porta := '';
           GPRS.Porta := 0;
         end;

      1: begin
           TipoComunicacao := ctcSerial;
           Serial.NumeroRelogio := StrToInt(edtNrelogio.Text);
           Serial.Porta := cbxPortaCom.Text;
           Serial.Velocidade := cv9600;
           Tcp.Ip := '';
           Tcp.MAC := '';
           Tcp.Porta := 0;
           Modem.Fone := '';
           Modem.Porta := '';
           GPRS.Porta := 0;
         end;
    end;
  end;
  //Adiciona thread de comunicação
  _gKernel.AdicionaCard[_rComConfig, _rIdxEquip];
  _gKernel.SetConectado(_rIdxEquip, true);

  if not _gKernel.RecebeConfiguracao[_rIdxEquip, _rConfiguracao] then
  begin
    ShowMessage('Falha ao conectar ao equipamento.');
    _gKernel.RemoveCard[_uThreadIndex];
    Exit;
  end
  else
    begin
      proAtualizaStatus('Conectado');
      _uThreadIndex := _rIdxEquip;
      pgctrlConexao.Enabled := False;
    end;
end;

procedure TfrmCapturaImagem.btnDesconectarClick(Sender: TObject);
begin
  proAtualizaStatus('Desconectado');
  _gKernel.RemoveCard[_uThreadIndex];
  pgctrlConexao.Enabled := True;
end;


procedure TfrmCapturaImagem.btnCapturarClick(Sender: TObject); 
begin
  proAtualizaStatus('Posicione o dedo no hamster.');
  //indica onde a digital será desenhada na tela
  with pnlDig do
    _gHamster.SetImagem(Handle, Top, Left, Height, Width);
  //Capturando imagem da digital
  if (_gHamster.Capture[_uTempTemplate]) then
  begin
    sleep(200);
    //Realizando verificação com a digital capturada
    if (_gHamster.Verify[_uTempTemplate]) then
    begin
      proAtualizaStatus('Digital capturada.');
    end
    else
      proAtualizaStatus('Falha na verificação da digital.');
  end
  else
    proAtualizaStatus('Falha ao capturar digital');
end;



procedure TfrmCapturaImagem.btnEnviarClick(Sender: TObject);
var
  _rTemplateAux : WideString;
  _rMaster : WordBool;
  _rMatricula : string;
begin
  _rMatricula := edtMatricula.Text;
  if (_rMatricula <> '') then
  begin
    proAtualizaStatus('Enviando...');
    _rMaster := chkMaster.Checked;
    //Setando na template informações sobre o usuário.
    _rTemplateAux := _gHamster.SetUser[_uTempTemplate,
                                       _rMatricula,
                                       _rMaster ];
    //enviando template para o equipamento
    if (_gKernel.Bio_EnvTemplate[_uThreadIndex, _rTemplateAux]) then
    begin
      proAtualizaStatus('Sucesso para enviar digital');
    end
    else
    begin
      //Verifica último erro que ocorreu no kernel
      proAtualizaStatus(_gKernel.ErrorDescription[_gKernel.ThreadLastError[
        _uThreadIndex]]);
    end;
  end;
end;



end.
