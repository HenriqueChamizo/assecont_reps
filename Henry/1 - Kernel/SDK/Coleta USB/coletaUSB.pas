{*******************************************************************************
*                   HENRY EQUIPAMENTOS E SISTEMAS                              *
*                                                                              *
*                       EXEMPLO DE COLETA USB                                  *
*                                                                              *
* Programador : Jefferson Martins                                              *
* Data : 14/07/2008                                                            *
* Exemplo criado para mostrar o funcionamento da coleta  de registro offline   *
* via USB, como verificar a existencia de registros offline, a recuperação dos *
* registros, remoção com segurança do cartucho USB, como gravar os registros   *
* coletados e progresso durante as ações do kernel.                            *
*                                                                              *
********************************************************************************}

unit coletaUSB;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, OleServer, Kernel7x_TLB, ComCtrls;

type
    TfColeta = class(TForm)
    _kernel7x: TKernel;
    bAddEquipamento: TButton;
    mInf: TMemo;
    bColeta: TButton;
    bVerifica: TButton;
    bRecuperaRegistros: TButton;
    stbStatus: TStatusBar;
    bRemove: TButton;
    prgbProgresso: TProgressBar;
    procedure FormCreate(Sender: TObject);
    procedure bAddEquipamentoClick(Sender: TObject);
    procedure bColetaClick(Sender: TObject);
    procedure bVerificaClick(Sender: TObject);
    procedure bRecuperaRegistrosClick(Sender: TObject);
    procedure bRemoveClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    procedure proProgresso(pSender: TObject; pThreadIndex, pByte, pByteMax,
      pBuffer, pBufferMax: integer);
  end;

var
  fColeta: TfColeta;
  exist : WordBool;
  _rIdxEquip : integer;
  _rConfig : SComConfig;

implementation

{$R *.dfm}


procedure TfColeta.FormCreate(Sender: TObject);
begin
  _kernel7x.OnProgresso := proProgresso;
end;

//Procedimento responsável por capturar o progresso das ações no kernel
procedure TfColeta.proProgresso(pSender: TObject; pThreadIndex, pByte, pByteMax,
  pBuffer, pBufferMax: integer);
begin
  prgbProgresso.Max := pByteMax;     //tamanho máximo do buffer
  prgbProgresso.Position := pByte;   //posição atual
  Application.ProcessMessages;       //atualizando interface
  if pByte >= pByteMax then
    prgbProgresso.Position := 0;
end;

procedure TfColeta.bAddEquipamentoClick(Sender: TObject);
begin
  TButton(Sender).Enabled := false;
  bVerifica.Enabled := true;

  // criando configurações do equipamento
  _rConfig.IsCatraca := false;
  _rConfig.TipoComunicacao := cmcOffline;
  _rConfig.TipoComunicacao := ctcUsb;
   
 // adicionando equipamento no Kernel
  if  not _kernel7x.AdicionaCard[_rConfig, _rIdxEquip] then
    begin
      mInf.Lines.Add('Não foi possivel adicionar o equipamento');
      TButton(Sender).Enabled := true;
      exit;
    end
    else
      begin
        mInf.Lines.Add( 'Equipamento adiconado');
        stbStatus.Panels[0].Text := 'Equipamento adicionado...';
        TButton(Sender).Enabled := true;
      end;
end;

procedure TfColeta.bColetaClick(Sender: TObject);
var
  indice: integer;
  vetor: array  of SRegistro;
  arquivo : string;
  arquivoSaida: TextFile;
begin 
  TButton(sender).Enabled := false;
  fColeta.Cursor := crHourGlass;

  //Pegando diretório onde está rodando o programa
  arquivo := ExtractFilePath(Application.ExeName) + 'Coleta.txt';   //**********
  
  AssignFile(arquivoSaida, arquivo);
  SetLength(vetor, 1);// setando o tamanho do vetor

  if not FileExists(arquivo) then   // verificando se arquivo existe para
    reWrite(arquivoSaida)           // gravar os registros
  else
      Append(arquivoSaida);

  indice := 0; // zerando indice
  
  try
  stbStatus.Panels[0].Text := 'Coletando...';
  // recebendo pacote do kernel
  if _kernel7x.RecebePacote[_rIdxEquip] then
   while _kernel7x.RegistroOff[_rIdxEquip, vetor[indice]] do // coletando
    begin
      Writeln(arquivoSaida,DateTimeToStr(vetor[indice].DataHora) + ' ' + vetor[indice].matricula); //escrevendo matricula coletada
      Inc(indice); // incrementado indice
      SetLength(vetor, indice+1);  // adicionado uma posição no vetor
    end; 
      
  MessageDlg('Registos coletados',mtInformation, [mbOK],0);
  
  finally
    CloseFile(arquivoSaida);// fechado arquivo
  end;

  TButton(sender).Enabled := true;
  fColeta.Cursor := crDefault;
  stbStatus.Panels[0].Text := 'Registros coletados...';
end;

procedure TfColeta.bVerificaClick(Sender: TObject);
begin
  stbStatus.Panels[0].Text := 'verificando cartucho...';
    
  //Verificando existencias de registros
  if _kernel7x.ExistemRegistros[_rIdxEquip, exist] then
    if exist then
      begin
        mInf.Lines.Add('existem registros');
        bColeta.Enabled := true;
      end
    else
      begin
        mInf.Lines.Add('sem registros');
        bRecuperaRegistros.Enabled := true;
      end;

      stbStatus.Panels[0].Text := 'Pronto...';
end;


//Ao recuperar registros deve-se coletar logo em seguida pois a flag que indica
// a recuperação de registros só existe em tempo de execução
procedure TfColeta.bRecuperaRegistrosClick(Sender: TObject);
begin
  stbStatus.Panels[0].Text := 'Recuperando registros...';
  
  // recuperando registros
  if _kernel7x.RecuperaRegistros[_rIdxEquip] then
    mInf.lines.Add('Registros recuperados')
  else
    mInf.Lines.Add('Não foi possivel recuperar os registros');

    bColeta.Enabled := true;

end;

procedure TfColeta.bRemoveClick(Sender: TObject);
begin
   _kernel7x.USB_Remove // removendo cartucho USB
end;

end.
