{==============================================================================|
|   HENRY EQUIPAMENTOS E SISTEMAS                                              |
|                                                                              |
|   COMUNICAÇÃO COM O MÓDULO BIOMÉTRICO DO EQUIPAMENTO                         |
|                                                                              |
|   Autor : Eduardo de Andrade                                                 |
|   Data : 25/04/2008                                                          |
|   Versão :  1.0                                                              |
|                                                                              |
|   Este exemplo demonstra como realizar a comunicação com o módulo biométrico |
|  e receber suas configurações.                                               |
|                                                                              |
===============================================================================}

unit uD10SF01A;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ComCtrls, StdCtrls, ExtCtrls, Mask,

  Kernel7x_TLB;

type
  TfrmComunicaDSP = class(TForm)
    stbrHint: TStatusBar;
    Panel2: TPanel;
    Panel3: TPanel;
    Panel4: TPanel;
    lblCapac: TLabel;
    lblQtdeMat: TLabel;
    Label13: TLabel;
    Label14: TLabel;
    Bevel5: TBevel;
    Label15: TLabel;
    trbSensibilidade: TTrackBar;
    lblSensibilidade: TLabel;
    Label17: TLabel;
    Label18: TLabel;
    trbQualidade: TTrackBar;
    lblQualidade: TLabel;
    trbModoRapido: TTrackBar;
    lblModoRapido: TLabel;
    Label21: TLabel;
    Label22: TLabel;
    lblSeguranca: TLabel;
    trbSeguranca: TTrackBar;
    Label24: TLabel;
    rbInterno: TRadioButton;
    rbExterno: TRadioButton;
    Bevel6: TBevel;
    Label25: TLabel;
    Panel1: TPanel;
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
    btnReceber: TButton;
    btnEnviar: TButton;
    Panel5: TPanel;
    Label5: TLabel;
    procedure trbSensibilidadeChange(Sender: TObject);
    procedure trbQualidadeChange(Sender: TObject);
    procedure trbModoRapidoChange(Sender: TObject);
    procedure trbSegurancaChange(Sender: TObject);
    procedure btnReceberClick(Sender: TObject);
    procedure btnEnviarClick(Sender: TObject);
    procedure btnDesconectarClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure btnConectarClick(Sender: TObject);
  private
   { Private declarations }
    _uThreadIndex: integer;  //Armazenará o index da thread
    _uDSPConfig: SDspcfg_S;  //Armazenará as configurações

    procedure proCapturaDados;         //Captura configurações da interface
    procedure proAtualizaInterface;    //Atualiza configurações na interface
    function fncListaPortasSeriais : TStrings; //Listando portas seriais do PC
  public
    { Public declarations }

  end;

var
  frmComunicaDSP: TfrmComunicaDSP;
  _gKernel : TKernel;

implementation

{$R *.dfm}



procedure TfrmComunicaDSP.FormCreate(Sender: TObject);
var
  _rComPorts : TStrings;
  _rInd : integer;
begin
  _gKernel := TKernel.Create(Self);   //Instanciando kernel

  _rComPorts:= fncListaPortasSeriais; //Criando lista de portas seriais
  cbxPortaCom.Items.Clear;
  cbxPortaCom.Items.BeginUpdate;
  for _rInd := 0 to _rComPorts.Count-1 do
    cbxPortaCom.Items.Add(_rComPorts[_rInd]);
  cbxPortaCom.Items.EndUpdate;  

end;


function TfrmComunicaDSP.fncListaPortasSeriais: TStrings;
var
  _rStrAux, _rPorta : string;
begin
  Result := TStringList.Create;
  _rStrAux := Trim(_gKernel.ListaPortasSeriais);
  while Length(_rStrAux) > 0 do
  begin
    _rPorta := '';
    while (_rStrAux[1] <> '-') do
    begin
      _rPorta := _rPorta + _rStrAux[1];
      _rStrAux := Copy(_rStrAux, 2, Length(_rStrAux) - 1);
      if Length(_rStrAux) = 0 then
        Break;
    end;
    if Length(_rStrAux) > 0 then
      if _rStrAux[1] = '-' then
        _rStrAux := Copy(_rStrAux, 2, Length(_rStrAux) - 1);    
    Result.Add(_rPorta);
  end;
end;



procedure TfrmComunicaDSP.btnConectarClick(Sender: TObject);
var
  _rComConfig : SComConfig;   //Armazenará as configurações de conexão
  _rIdxEquip : Integer;      //Armazenará a id da thread
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
         end;

      1: begin
           TipoComunicacao := ctcSerial;
           Serial.NumeroRelogio := StrToInt(edtNrelogio.Text);
           Serial.Porta := cbxPortaCom.Text;
         end;
    end;
  end;
  _gKernel.AdicionaCard[_rComConfig, _rIdxEquip];

//testa conexão
  _gKernel.BeginLargeTransfer(_uThreadIndex);
  _gKernel.RecebeConfiguracao[_uThreadIndex, _rConfiguracao];
  _gKernel.EndLargeTransfer(_uThreadIndex);
//***

  if not  _gKernel.RecebeConfiguracao[_uThreadIndex, _rConfiguracao] then  //Adiciona thread de comunicação
  begin
    ShowMessage('Falha ao conectar ao equipamento.');
    _gKernel.RemoveCard[_uThreadIndex];
    Exit;
  end
  else
    begin
      stbrHint.SimpleText := 'Conectado';
      _uThreadIndex := _rIdxEquip;
      btnReceber.Enabled := true;
      btnEnviar.Enabled := true;
      pgctrlConexao.Enabled := False;
    end;
end;

procedure TfrmComunicaDSP.btnDesconectarClick(Sender: TObject);
begin
  stbrHint.SimpleText := 'Desconectado';
  _gKernel.RemoveCard[_uThreadIndex];
  btnReceber.Enabled := False;
  btnEnviar.Enabled := False;
  pgctrlConexao.Enabled := True;
end;

procedure TfrmComunicaDSP.proCapturaDados;
begin
  with _uDSPConfig do     //Pegando configurações para o DSP da interface
  begin
    LightingCondition := rbInterno.Checked;
    SecurityLevel := trbSeguranca.Position;
    FastMode := trbModoRapido.Position;
    ImageQuality := trbQualidade.Position;
    Sensitivity := trbSensibilidade.Position;
  end;
end;

//Envia configurações para o DSP
procedure TfrmComunicaDSP.btnEnviarClick(Sender: TObject);
begin
  proCapturaDados;
  stbrHint.SimpleText := 'Aguarde... Enviando dados';
  _gKernel.BeginLargeTransfer(_uThreadIndex);
  _gKernel.Bio_EnvConfiguracaoS[_uThreadIndex, _uDSPConfig];
  _gKernel.EndLargeTransfer(_uThreadIndex);
  stbrHint.SimpleText := 'Conectado';
end;

//Procedimento que atualiza os dados recebidos na interface.
procedure TfrmComunicaDSP.proAtualizaInterface;
begin
  with _uDSPConfig do
  begin
    rbInterno.Checked := LightingCondition;
    rbExterno.Checked := not(rbInterno.Checked);
    trbSeguranca.Position := SecurityLevel;
    trbModoRapido.Position := FastMode;
    trbQualidade.Position := ImageQuality;
    trbSensibilidade.Position := Sensitivity;

  end;
  if lblCapac.Caption = '0' then
    begin
      stbrHint.SimpleText := 'O Equipamento não possui biometria';
      trbSeguranca.Position := 48;
      trbModoRapido.Position := 48;
      trbQualidade.Position := 48;
      trbSensibilidade.Position := 48;
    end
  else
    stbrHint.SimpleText := 'Conectado';
end;

//Procedimento que recebe os dados do relógio.
procedure TfrmComunicaDSP.btnReceberClick(Sender: TObject);
var
  _rQtdeLivre: word;
  _rQtdeReg: word;
begin
  stbrHint.SimpleText := 'Aguarde... Recebendo dados';
  _gKernel.BeginLargeTransfer(_uThreadIndex);

  //Configurações padrão
  _gKernel.Bio_RecConfiguracaoS[_uThreadIndex, _uDSPConfig];

  //Informações adicionais
  _gKernel.Bio_UsuariosQuantLivre[_uThreadIndex, _rQtdeLivre]; //Recebe quantidade de espaço livre
  _gKernel.Bio_UsuariosQuant[_uThreadIndex, _rQtdeReg];        //Recebe quantidade de registros

  lblCapac.Caption := IntToStr(_rQtdeLivre + _rQtdeReg);
  lblQtdeMat.Caption := IntToStr(_rQtdeReg);

  _gkernel.EndLargeTransfer(_uThreadIndex);

  proAtualizaInterface;
end;


{
 Atualizando label da trackbar...
}

procedure TfrmComunicaDSP.trbModoRapidoChange(Sender: TObject);
begin
  lblModoRapido.Caption := IntToStr(trbModoRapido.position);
end;

procedure TfrmComunicaDSP.trbQualidadeChange(Sender: TObject);
begin
  lblQualidade.Caption := IntToStr(trbQualidade.position);
end;

procedure TfrmComunicaDSP.trbSegurancaChange(Sender: TObject);
begin
  lblSeguranca.Caption := IntToStr(trbSeguranca.position);
end;

procedure TfrmComunicaDSP.trbSensibilidadeChange(Sender: TObject);
begin
  lblSensibilidade.Caption := IntToStr(trbSensibilidade.position);
end;


end.
