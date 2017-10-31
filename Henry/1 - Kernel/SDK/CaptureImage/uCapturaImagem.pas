{===============================================================================
|                                                                              |
|   HENRY EQUIPAMENTOS E SISTEMAS LTDA.                                        |
|                                                                              |
|   Data : 15/05/2009                                                          |
|   Autor : Eduardo de Andrade                                                 |
|                                                                              |
|   Este exemplo demonstra a utilização da função de extração da imagem da     |
| digital capturada.                                                           |
|                                                                              |
===============================================================================}

unit uCapturaImagem;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, kernel7x_tlb, StdCtrls, ExtCtrls, ComCtrls;

const
  K_BMP = '.bmp';

type
  TfrmCapturaImagem = class(TForm)
    pnlFundo: TPanel;
    btnCapturar: TButton;
    btnSalvar: TButton;
    pnlDig: TPanel;
    stbrHint: TStatusBar;
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure FormShow(Sender: TObject);
    procedure btnSalvarClick(Sender: TObject);
    procedure btnCapturarClick(Sender: TObject);
  private
    { Private declarations }
    hamster : THamster;

    procedure proAtualizaStatus(pMensagem: string);
    function fncSelecionaArquivo() : string;
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

function TfrmCapturaImagem.fncSelecionaArquivo: string;
var
  _rDialog : TSaveDialog;
begin
  Result := '';
  //Mostrando janela para usuário salvar
  _rDialog := TSaveDialog.Create(Application);
  with TOpenDialog(_rDialog) do
  begin
    DefaultExt := '*' + K_BMP;
    Filter := Format('Arquivo %s (*%s) |*%s', [K_BMP, K_BMP, K_BMP]);
    InitialDir := ExtractFilePath(Application.ExeName);
    FileName := 'Template.bmp';
    ForceDirectories(InitialDir);
    Title := 'Salvar imagem';
    if (Execute) then
      Result := FileName;
  end;
  FreeAndNil(_rDialog);
end;                     


procedure TfrmCapturaImagem.FormShow(Sender: TObject);
begin
  hamster := THamster.Create(self);
end;

procedure TfrmCapturaImagem.FormClose(Sender: TObject;
  var Action: TCloseAction);
begin
  FreeAndNil(hamster);
end;

procedure TfrmCapturaImagem.btnCapturarClick(Sender: TObject);
var
  _rTemplate : WideString;
begin
  proAtualizaStatus('Posicione o dedo no hamster.');
  //indica onde a digital será desenhada na tela
  with pnlDig do
    hamster.SetImagem(Handle, Top, Left, Height, Width);
  //Capturando imagem da digital
  if (hamster.Capture[_rTemplate]) then
  begin
    sleep(200);
    //Realizando verificação com a digital capturada
    if (hamster.Verify[_rTemplate]) then
    begin
      proAtualizaStatus('Digital capturada.');
    end
    else
      proAtualizaStatus('Falha na verificação da digital.');
  end
  else
    proAtualizaStatus('Falha ao capturar digital');
end;

procedure TfrmCapturaImagem.btnSalvarClick(Sender: TObject);
var
  _rTemplate : WideString;
  _rStatus : WordBool;  
  _rTemp : string;
  _rTextFile : TextFile;
begin
  //Esta função retornará a digital capturada em "hamster.Capture"
  hamster.CaptureImage(_rTemplate, _rStatus);
  if (_rStatus) then
  begin
    //Obtendo endereço do arquivo onde será salva a imagem
    _rTemp := fncSelecionaArquivo;
    if (_rTemp <> '') then
    begin
      //escrevendo template no arquivo e salvando com extensão .bmp
      AssignFile(_rTextFile, _rTemp);
      Rewrite(_rTextFile);
      Append(_rTextFile);
      Writeln(_rTextFile, _rTemplate);
      CloseFile(_rTextFile);
    end
    else
      proAtualizaStatus('Falha ao escolher arquivo.');
  end
  else
    proAtualizaStatus('Falha ao capturar imagem da digital.');
end;




end.
