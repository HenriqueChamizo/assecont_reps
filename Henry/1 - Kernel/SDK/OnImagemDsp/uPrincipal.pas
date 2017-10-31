{==============================================================================|
|   HENRY EQUIPAMENTOS E SISTEMAS                                              |
|                                                                              |
|   Autores : Eduardo de Andrade  / Jefferson Chanan                           |
|   Data : 19/01/2009                                                          |
|                                                                              |
|                                                                              |
|                                                                              |
|   ------------------------------------------------------------------------   |
|                                                                              |
|    Coment�rios                                                               |
|                                                                              |
|   Este exemplo mostra como realizar o tratamento para a imagem recebida      |
| atrav�s do evento onImagem.                                                  |
|                                                                              |
===============================================================================}

unit uPrincipal;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, OleServer, Kernel7x_TLB, StdCtrls, ComCtrls, uResposta;

type
  TfrmOnImagem = class(TForm)
    edtIP: TEdit;
    Label1: TLabel;
    btnConectar: TButton;
    stbrStatus: TStatusBar;
    kernel7x: TKernel;
    procedure btnEnviaCfgsClick(Sender: TObject);
    procedure kernel7xImagemDsp(ASender: TObject; pThreadIndex: Integer;
      const pImagem: WideString);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure btnConectarClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmOnImagem: TfrmOnImagem;
  _gIndex : integer;
  _gThread : TThread;

implementation

{$R *.dfm}

procedure TfrmOnImagem.btnConectarClick(Sender: TObject);
var
  _rComConfig : SComConfig;
  _rConfig : SConfiguracao;
begin
  TButton(Sender).Enabled := False; //Adi��o do equipamento
  _gIndex := 0;
  with _rComConfig do                   //carregando configura��es
  begin               
    Tcp.Ip := edtIP.Text;            //inicializando valores tcp
    Tcp.MAC := '';
    Tcp.Porta := 3000;
    Serial.NumeroRelogio := 0;       //inicializando demais valores
    Serial.Porta := '';
    Serial.Velocidade := 0;
    Modem.Fone := '';
    Modem.Porta := '';
    GPRS.Porta := 0;
    TipoComunicacao := ctcTcpIp;
    ModoComunicacao := cmcOnline;
    IsCatraca := False;
  end;
  //adicionando equipamento no kernel
  if kernel7x.AdicionaCard[_rComConfig, _gIndex] then
  begin
    stbrStatus.SimpleText := 'Equipamento adicionado... Enviando configura��es, Aguarde';
    kernel7x.SetConectado(_gIndex, True);
    if (kernel7x.RecebeConfiguracao[_gIndex, _rConfig]) then
    begin
      with _rConfig do
      begin
        //altera algumas configura��es
        ModoComunicacao := cmcOnline;
        Teclado := ctLeitor1;
        Leitores.Leitor1 := clEntradaSaida;
        Leitores.Leitor2 := clSensitivo;
        Leitores.Leitor3 := clEntradaSaida;
        Controles.BiometriaOnline := True;
        Controles.BiometriaImagem := True;     //habilitando biometria imagem *
        //enviando novas configura��es
        if (kernel7x.EnviaConfiguracao[_gIndex, _rConfig]) then
          stbrStatus.SimpleText := 'Configura��es enviadas'
        else
          stbrStatus.SimpleText := 'Falha ao enviar configura��es'
      end;
    end
    else
      stbrStatus.SimpleText := 'Erro ao receber configura��es';

  end
  else
    stbrStatus.SimpleText := 'Erro ao adicionar equipamento';
  TButton(Sender).Enabled := True;
end;


//Evento ir� tratar a imagem recebida
procedure TfrmOnImagem.kernel7xImagemDsp(ASender: TObject;
  pThreadIndex: Integer; const pImagem: WideString);

  function fncReplace(pName : string) : string;
  begin
    Result := StringReplace(pName, ':', '', [rfReplaceAll]);
  end;

var
  _rImg: TStringList;
  _rImgFile: string;
  _rResposta : TThreadResposta;
  _rError : integer;
begin
  if (pImagem <> '') then
  begin
    _rImg := TStringList.Create;              //Armazena as informa��es da template
    _rImgFile := ExtractFilePath(Application.ExeName) + '\image' +
      fncReplace(TimeToStr(now)) + '.bmp';    //nome do arquivo
    with _rImg do     //obtendo as informa��es da template que acabou de capturar
    begin
      add(pImagem);//adiciona os dados que ser�o salvos no arquivo
      {Salva o arquivo com o nome declarado, pode-se definir este nome dinamicamente}
      SaveToFile(_rImgFile);
      FreeAndNil(_rImg);
    end;
  end;

  _rError := kernel7x.ThreadLastError[pThreadIndex];
  if (_rError <> 0) then
    frmOnImagem.stbrStatus.SimpleText := kernel7x.ErrorDescription[
      _rError];


  //Realize seu tratamento da imagem e devolva a resposta online
  _rResposta := TThreadResposta.Create(kernel7x, pThreadIndex, (pImagem <> ''));
  _rResposta.WaitFor;      //aguardando enviar a resposta
  FreeAndNil(_rResposta);
end;


procedure TfrmOnImagem.btnEnviaCfgsClick(Sender: TObject);
var
  _rConfig : SConfiguracao;
begin
  //enviando configura��es
  TButton(Sender).Enabled := False;
  with _rConfig do
  begin
    //recebe configura��o atual
    kernel7x.RecebeConfiguracao[_gIndex, _rConfig];

    //altera algumas configura��es
    _rConfig.ModoComunicacao := cmcOnline;
    _rConfig.Teclado := ctLeitor1;
    Leitores.Leitor1 := clEntradaSaida;
    Leitores.Leitor2 := clSensitivo;
    Leitores.Leitor3 := clEntradaSaida;
    NumDigitos := 8;
    FaixaAcesso := false;
    Orion := False;
    Senha := '';
    SenhaMenu := false;


    Controles.BloqueiaPeriodo := false;
    Controles.FuncoesEspecificas := false;
    Controles.Visitantes := False;
    Controles.Touch := False;
    Controles.Digitais11 := False;
    Controles.BiometriaOnline := True;
    Controles.BiometriaImagem := True;     //habilitando biometria imagem *
    Controles.LeitorDigitalFree := False;
    //enviando novas configura��es
    if (not(kernel7x.EnviaConfiguracao[_gIndex, _rConfig])) then
      stbrStatus.SimpleText := 'Falha ao enviar configura��es';
  end;
  TButton(Sender).Enabled := True;
end;

procedure TfrmOnImagem.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  FreeAndNil(kernel7x);
end;


end.
