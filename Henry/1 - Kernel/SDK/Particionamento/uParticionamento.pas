{===============================================================================
|                                                                              |
|   HENRY EQUIPAMENTOS E SISTEMAS LTDA.                                        |
|                                                                              |
|   Data : 15/05/2009                                                          |
|   Autor : Eduardo de Andrade                                                 |
|                                                                              |
|   Este exemplo demonstra a utiliza��o da fun��o de particionamento do kernel.|
|                                                                              |
|                                                                              |
===============================================================================}

unit uParticionamento;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, kernel7x_tlb, StdCtrls, ExtCtrls, ComCtrls, Mask;

type
  TfrmCapturaImagem = class(TForm)
    pnlFundo: TPanel;
    stbrHint: TStatusBar;
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
    btnFormatar: TButton;
    procedure btnFormatarClick(Sender: TObject);
    procedure btnDesconectarClick(Sender: TObject);
    procedure btnConectarClick(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormShow(Sender: TObject);
  private
    { Private declarations }
    _gKernel : TKernel;

    _uThreadIndex : integer;

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
  _gKernel := TKernel.Create(self);
end;

procedure TfrmCapturaImagem.FormClose(Sender: TObject;
  var Action: TCloseAction);
begin
  FreeAndNil(_gKernel);
end;

procedure TfrmCapturaImagem.btnConectarClick(Sender: TObject);
var
  _rComConfig : SComConfig;   //Armazenar� as configura��es de conex�o
  _rIdxEquip : Integer;      //Armazenar� a id da thread
  _rConfiguracao : SConfiguracao;

begin
  with _rComConfig do
  begin
     IsCatraca := False;                         //Seta configura��es
    ModoComunicacao := cmcOffline;
    case pgctrlConexao.ActivePageIndex of       //Verifica tipo de comunica��o
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
  //Adiciona thread de comunica��o
  _gKernel.AdicionaCard[_rComConfig, _rIdxEquip];

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


procedure TfrmCapturaImagem.btnFormatarClick(Sender: TObject);
var
  _rFormatacao : array[0..7] of Integer;      //vetor auxiliar
  _rConfig : SConfiguracao;                   //estrutura de configura��o
  _rIntAux : integer;                         //inteiro auxiliar
  i : integer;                                //contador
  _rParticionamento : SParticionamento;       //estrutura de particionamento
begin
  if (_gKernel.RecebeConfiguracao[_uThreadIndex, _rConfig]) then
  begin

    for i := 0 to High(_rFormatacao) do
      _rFormatacao[i] := 0;


    //C�LCULO DE QUANTIDADE PARA CADA SETOR DIMENSION�VEL


    //Obs: neste exemplo usaremos 10 itens para cada setor. Varie o valor
    //conforme a necessidade de sua aplica��o.


    //Fun��es espec�ficas
    if (_rConfig.Controles.FuncoesEspecificas) then
      _rIntAux := _gKernel.SRFuncoesEspecificas[_rConfig,
          10,  //Quantidade de fun��es espec�ficas
          10]  //Quantidade de fun��es espec�ficas por matr�cula
    else
      //Fun��es
      _rIntAux := _gKernel.SRFuncoes[_rConfig, 10];   //10 fun��es

    _rFormatacao[cpFuncoes] := _rIntAux;

    //Feriados
    //Setando com 10 Feriados
    _rFormatacao[cpFeriados] := _gKernel.SRFeriados[_rConfig, 10];

    //Acionamentos
    //Setando com 10 Acionamentos
    _rFormatacao[cpAcionamentos] := _gKernel.SRAcionamentos[_rConfig, 10];

    //Lista de Acesso
    //Setando com 10 Funcion�rios na lista de acesso
    _rFormatacao[cpListaAcesso] := _gKernel.SRListaAcesso[_rConfig, 10];

    //Escalas/Horarios e Per�odos
    //Setando com 10 per�odos
    _rFormatacao[cpPeriodos] := _gKernel.SRPeriodos[_rConfig, 10];

    //Hor�rios
    _rFormatacao[cpHorarios] := _gKernel.SRHorariosEscalas[_rConfig,
        10,  //quantidade de hor�rios
        10,  //quantidade de per�odos por hor�rios
        10,  //quantidade de escalas
        10]; //quantidade de hor�rios por escala

    //Mensagens Espec�ficas
    _rFormatacao[cpMsgEspecifica] := _gKernel.SRMsgEspecifica[_rConfig,
        10,  //Quantidade de mensagens
        10]; //Quantidade de matr�culas por mensagem




    //C�LCULO DO ENDERE�O DE MEM�RIA DE CADA SETOR

    //Endere�o inicial
    _rIntAux := CARD7X_MEMORY_START;


    //inicializando vari�vel
    FillChar(_rParticionamento, sizeof(_rParticionamento), 0);


    // -> L�gica do c�lculo
    //Verifica se o setor � utilizado (setor > 0)
    //Adiciona endere�o
    //soma ao endere�o o espa�o que ocupa


    //Fun��es
    if _rFormatacao[cpFuncoes] > 0 then
    begin
      _rParticionamento.Funcoes := _rIntAux;
      _rIntAux := _rIntAux + _rFormatacao[cpFuncoes];
    end;

    //Feriados
    if _rFormatacao[cpFeriados] > 0 then
    begin
      _rParticionamento.Feriados := _rIntAux;
      _rIntAux := _rIntAux + _rFormatacao[cpFeriados];
    end;

    //Acionamentos
    if _rFormatacao[cpAcionamentos] > 0 then
    begin
      _rParticionamento.Acionamentos := _rIntAux;
      _rIntAux := _rIntAux + _rFormatacao[cpAcionamentos];
    end;

    //Lista de acesso
    if _rFormatacao[cpListaAcesso] > 0 then
    begin
      _rParticionamento.ListaAcesso := _rIntAux;
      _rIntAux := _rIntAux + _rFormatacao[cpListaAcesso];
    end;

    //Per�odos
    if _rFormatacao[cpPeriodos] > 0 then
    begin
      _rParticionamento.Periodos := _rIntAux;
      _rIntAux := _rIntAux + _rFormatacao[cpPeriodos];
    end;

    //Hor�rios
    if _rFormatacao[cpHorarios] > 0 then
    begin
      _rParticionamento.Horarios := _rIntAux;
      _rIntAux := _rIntAux + _rFormatacao[cpHorarios];
    end;

    //Mensagens espec�ficas
    if _rFormatacao[cpMsgEspecifica] > 0 then
    begin
      _rParticionamento.MsgEspecifica := _rIntAux;
      _rIntAux := _rIntAux + _rFormatacao[cpMsgEspecifica];
    end;
     
    //Registros
    if _rIntAux < CARD7X_MEMORY_BYTES then
      _rParticionamento.Registros := _rIntAux;


    //Enviando particionamento ao equipamento
    if (_gKernel.EnviaParticionamento[_uThreadIndex, _rParticionamento]) then
      proAtualizaStatus('Particionamento enviado')
    else
      //falha, verificando mensagem do kernel
      proAtualizaStatus(_gKernel.ErrorDescription[
        _gKernel.ThreadLastError[_uThreadIndex]]);  
  end
  else
    proAtualizaStatus('Falha ao receber configura��es');
end;

end.
