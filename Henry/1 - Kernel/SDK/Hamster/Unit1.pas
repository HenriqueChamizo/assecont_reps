unit Unit1;

interface

uses
  threaddinamic,
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, OleServer, Kernel7x_TLB, ExtCtrls;

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
    procedure btnCapturarClick(Sender: TObject);
    procedure btnVerificarClick(Sender: TObject);
    procedure btnCarregarClick(Sender: TObject);
    procedure btnProcurarClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  _uTemplate : WideString;

implementation

{$R *.dfm}

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
begin
  btnVerificar.Enabled := False;
  with pnlDig2 do
    Hamster1.SetImagem(Handle, Top, Left, Height, Width);
  if Hamster1.Verify[_uTemplate] then
  begin
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

procedure TForm1.Button1Click(Sender: TObject);
begin
  with pnlDig1 do
    Hamster1.SetImagem(Handle, Top, Left, Height, Width);
  if Hamster1.CaptureContinuous[_uTemplate] then
  begin
    ShowMessage('Captura OK');
    btnVerificar.Enabled := True;
  end
  else
    ShowMessage('Falha ao capturar');
end;

end.
