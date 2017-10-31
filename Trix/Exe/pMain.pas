unit pMain;

interface

uses
  Windows, Forms, SysUtils, pPrincipal, Graphics, OleServer, ImgList, Controls, cxGraphics,
  WRegistry, dxBar, cxClasses, StdCtrls, rmLabel, WPanel, Grids, WStringGrid,
  ExtCtrls, Classes, rRotinas, rStrings, rStringsWords, WFormState, Menus, uJanelas, rFiles, rSystem, rDialogs;

type
  TfrmMain = class(TfrmConfigTerminal)
    pmFuncionarios: TPopupMenu;
    EnviarFuncionriosparaoTerminal1: TMenuItem;
    N1: TMenuItem;
    RegistrarDLL1: TMenuItem;
    ExcluirnoTerminal2: TMenuItem;
    procedure FormCreate(Sender: TObject);
    procedure btConectarClick(Sender: TObject);
    procedure btEnviarEmpregadorClick(Sender: TObject);
    procedure btDataHoraClick(Sender: TObject);
    procedure btImportarMarcacoesClick(Sender: TObject);
    procedure ExcluirnoTerminal2Click(Sender: TObject);
  private
    CaminhoAfd: string;
    IndexTerminal: integer;
    Ret: shortstring;
    procedure Conectar;

    procedure EnviarEmpregador(RazaoSocial, Cnpj, Cei, Endereco: string);
    procedure Importar_Afd(Local: string; Data: TDateTime; ImportarTudo: boolean);
  public
  end;

var
  frmMain: TfrmMain;

implementation

function InicializaComunicacao(Ip: shortstring; Porta: integer): shortstring; external 'Trix.dll';
function CadastraEmpresa(RazaoSocial, Cnpj, Localidade: shortstring): shortstring; external 'Trix.dll';
function EnviaDataHora(DataHora: TDateTime): shortstring; external 'Trix.dll';
function ImportarMarcacoes(Local: shortstring; Data: TDateTime; ImportarTudo: boolean): shortstring; external 'Trix.dll';

{$R *.dfm}

procedure TfrmMain.Conectar;
var
  Ip: shortstring;
  Porta: integer;
begin
Ip := GetIpSelecao;
Porta := GetPortaSelecao;
Ret := InicializaComunicacao(Ip, Porta);
SetMensagem(Ip, Ret);
end;

procedure TfrmMain.btConectarClick(Sender: TObject);
begin
inherited;

Conectar;
PreencherStringListIdsTerminais;
end;

procedure TfrmMain.btDataHoraClick(Sender: TObject);
var
  Ret: shortstring;
begin
Conectar;

inherited;

Ret := EnviaDataHora(Now);
SetMensagem(GetIpSelecao, Ret);
end;

procedure TfrmMain.btEnviarEmpregadorClick(Sender: TObject);
begin
Conectar;
//SetMensagem(GetIpSelecao, 'LENDO DADOS DO EMPREGADOR...');

//R:= Kernel1.RecebeDadosEmpregador[IndexTerminal, Empregador];

//if R then
   Abrir_Dados_Empregador(EnviarEmpregador, '', '', '', '');

//SetMensagem(GetIpSelecao, R);
end;

procedure TfrmMain.btImportarMarcacoesClick(Sender: TObject);
var
  sData, sLocal: string;
begin
sData := WRegistry1.Read('Opcoes', 'Ultimo filtro de data', '');

if sData = '' then
   sData := DateToStr(IncMonth(Date, -1));

//sLocal := WRegistry1.Read('Opcoes', 'Ultimo arquivo afd', '');
//if sLocal = '' then
//   begin
   sLocal := GetDocumentsDir + 'Asseponto\Arquivos pendentes\' + FormatDateTime('yyyymmdd', Date) + '.txt';

   if not DirectoryExists(ExtractFilePath(sLocal)) then
      ForceDirectories(ExtractFilePath(sLocal));
//   end;

Abrir_Importar_Afd(Importar_Afd, sLocal, StrToDate(sData), True);
end;

procedure TfrmMain.Importar_Afd(Local: string; Data: TDateTime; ImportarTudo: boolean);
var
  R: boolean;
begin
WRegistry1.Write('Opcoes', 'Ultimo filtro de data', Data);
WRegistry1.Write('Opcoes', 'Ultimo arquivo afd', Local);
Conectar;

SetMensagem(GetIpSelecao, 'IMPORTANDO MARCAÇÕES APARTIR DE ' + DateToStr(Data) + '...');

CaminhoAfd := Local;

ImportarMarcacoes(Local, Data, ImportarTudo);

//SetMensagem(GetIpSelecao, 'AFD SALVO EM ' + CaminhoAfd);
//WRegistry1.Write('Apontamento', 'Ultimo arquivo de importacao', CaminhoAfd);
//SetMensagem(GetIpSelecao, 'QUANTIDADE DE MARCAÇÕES: ' + IntToStr(pQtdeEventosColetados));
end;

procedure TfrmMain.EnviarEmpregador(RazaoSocial, Cnpj, Cei, Endereco: string);
begin
inherited;

Ret := CadastraEmpresa(RazaoSocial, Cnpj, Endereco);
SetMensagem(GetIpSelecao, Ret);
end;

procedure TfrmMain.ExcluirnoTerminal2Click(Sender: TObject);
var
  Matricula, Func: integer;
begin
Matricula := GetFuncionarioMatriculaSelecao;
Func := GetFuncionarioIndiceSelecao;

///if Excluir_Registro_Terminal then
//   ExcluirMatricula(Matricula, Func);
end;

procedure TfrmMain.FormCreate(Sender: TObject);
begin
TerminalConectado := 0;
Fabricante := 'Trix';

inherited;
end;

end.
