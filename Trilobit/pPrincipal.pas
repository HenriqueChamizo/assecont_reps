unit pPrincipal;

interface

uses
  Windows, Forms, StdCtrls, Controls, Graphics, SysUtils, Classes, ExtCtrls, Activex, RepTrilobit_TLB,
  Grids, WStringGrid, WLabel, WRegistry, WPanel, cxPC, rAdo, rDialogs,
  cxControls, rParametros, rFiles, uFuncionarios, uJanelas, uConsts, uVars,
  WComboEdit, WComboBox, cxClasses, dxBar;

type
  TfrmRepTrilobitPrincipal = class(TForm)
    WRegistry1: TWRegistry;
    sgFuncionarios: TWStringGrid;
    dxBarManager1: TdxBarManager;
    dxBarManager1Bar1: TdxBar;
    Panel1: TScrollBox;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    lbRazaoSocial: TWLabel;
    lbLocal: TWLabel;
    lbCnpj: TWLabel;
    sgTerminais: TWStringGrid;
    Label5: TLabel;
    Label6: TLabel;
    dxBarButton1: TdxBarButton;
    dxBarButton2: TdxBarButton;
    dxBarButton3: TdxBarButton;
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure sgTerminaisDblClick(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure sgFuncionariosDrawCellFinalize(Sender: TObject; aCol,
      aRow: Integer; var Rect: TRect; State: TGridDrawState);
  private
    meuREP: REP;
    procedure Conectar;
    procedure Preencher_Terminais;
    procedure Preencher_Empregador;
    function Get_Grupo: integer;
    procedure Ler_Funcionarios_Terminal;
    procedure Inicializar;
  public
  end;

var
  frmRepTrilobitPrincipal: TfrmRepTrilobitPrincipal;

const
  COL_TRM_NUMERO    = 0;
  COL_TRM_DESCRICAO = 1;
  COL_TRM_IP        = 2;
  COL_TRM_PORTA     = 3;
  COL_TRM_IND       = 4;
  COL_TRM_SENHA     = 5;

  PAGE_COMUNICACAO  = 0;
  PAGE_EMPREGADOR   = 1;
  PAGE_TERMINAIS    = 2;
  PAGE_FUNCIONARIOS = 3;

implementation

uses pDataModule;

{$R *.dfm}

procedure TfrmRepTrilobitPrincipal.Button1Click(Sender: TObject);
var
  TipoDoc: TOleEnum;
  R: integer;
  Ip: string;
  Porta, Senha: integer;
begin
TipoDoc := eTipoDocumento_CNPJ;

for R := 1 to sgTerminais.RowCount - 1 do
    begin
    Ip := sgTerminais.Cells[COL_TRM_IP, R];
    Porta := StrToInt(sgTerminais.Cells[COL_TRM_PORTA, R]);
    Senha := StrToInt(sgTerminais.Cells[COL_TRM_SENHA, R]);

    meuREP.CadastrarEmpregador(
                               Ip, Porta, Senha, TipoDoc,
                               lbCnpj.Caption, '0',
                               lbRazaoSocial.Caption, lbLocal.Caption
                               );
    end;
end;

procedure TfrmRepTrilobitPrincipal.Button2Click(Sender: TObject);
begin
if Abrir_Rep_Edicao(0, Grupo) then
   Preencher_Terminais;
end;

procedure TfrmRepTrilobitPrincipal.FormCreate(Sender: TObject);
begin
meuREP := CoREP.Create;
sgFuncionarios.ColGroup := SGFUNCS_COL_TERMINAL_DESCRICAO;
sgFuncionarios.ColGroupDisplay := SGFUNCS_COL_NOME;
end;

procedure TfrmRepTrilobitPrincipal.FormShow(Sender: TObject);
begin
Conectar;
Grupo := Get_Grupo;

if Grupo = 0 then
   Application.Terminate;

Inicializar;
end;

procedure TfrmRepTrilobitPrincipal.Conectar;
var
  R: integer;
begin
R := Connecting(DM.Conn, WRegistry1);

if R = CONX_ERRO then
   begin
   ErrorMessage('Ocorreu um erro no acesso ao banco de dados'#13#13'Verifique a configuração de conexão no Asseponto');
   Application.Terminate;
   end;
end;

procedure TfrmRepTrilobitPrincipal.Ler_Funcionarios_Terminal;
var
  sLista: WideString;
  slFuncsTerminais: TStringList;
begin
{slFuncsTerminais := TStringList.Create;

try
  meuREP.LerEmpregados_ViaLista(edIP.Text, StrToInt(edPorta.Text), StrToInt(edSenha.Text), sLista, ';', '|');
  slFuncsTerminais.StrictDelimiter := true;
  slFuncsTerminais.Delimiter := '|';
  slFuncsTerminais.DelimitedText := sLista;
  slFuncsTerminais.SaveToFile('c:\progs\terminais.txt');
finally
  slFuncsTerminais.Free;
  end;}
end;
{var
  Arquivo: string;
begin
//Arquivo := GetTemporaryFileName;
Arquivo := 'C:\Progs\arquivo.txt';
try
  meuREP.LerEmpregados(edIp.Text, StrToInt(edPorta.Text), StrToInt(edSenha.Text), Arquivo, False);
  slFuncsTerminais.LoadFromFile(Arquivo);
finally
  DeleteFile(Arquivo);
  end;
end;}

procedure TfrmRepTrilobitPrincipal.Preencher_Empregador;
begin
ReadTable(DM.Conn, 'SELECT %CAMPOS% FROM Grupos WHERE GRU_IND = ' + IntToStr(Grupo), Self, 1);
end;

procedure TfrmRepTrilobitPrincipal.Preencher_Terminais;
begin
Ler_Query_Grid(DM.Conn, 'SELECT TRM_NUMERO, TRM_DESCRICAO, TRM_IP, TRM_PORTA, TRM_IND, TRM_SENHA FROM Terminais WHERE TRM_GRUPO = ' + IntToStr(Grupo) + ' ORDER BY TRM_NUMERO', sgTerminais);
//FillCombo(DM.Q, edTerminal, 'Terminais', 'TRM_DESCRICAO', 'TRM_IND', '', 'TRM_DESCRICAO', 'TRM_GRUPO = ' + IntToStr(GRUPO));
end;

procedure TfrmRepTrilobitPrincipal.sgFuncionariosDrawCellFinalize(
  Sender: TObject; aCol, aRow: Integer; var Rect: TRect; State: TGridDrawState);
var
  Descricao, Pis: string;
begin
with (Sender as TWStringGrid) do with Canvas do
     begin
     if Cells[SGFUNCS_COL_FUNC, aRow] = '' then Exit;

     Font.Color := clGray;
     Font.Style := [];
     Descricao := Cells[SGFUNCS_COL_CARGO, aRow];
     Pis := Cells[SGFUNCS_COL_PIS, aRow];
     {
     case AgrupamentoTipo of
          AGRUPAMENTO_FUNCS_NENHUM: Descricao := Cells[SGFUNCS_COL_CARGO, aRow];
          AGRUPAMENTO_FUNCS_DEPTO : Descricao := Cells[SGFUNCS_COL_CARGO, aRow];
          AGRUPAMENTO_FUNCS_CARGOS: Descricao := Cells[SGFUNCS_COL_DEPTONOME, aRow];
          AGRUPAMENTO_FUNCS_LOCAL : Descricao := Cells[SGFUNCS_COL_LOCAL, aRow];
          end;}

     //if Cargo = '' then Cargo := 'Cargo não definido';
     //StretchDraw(Types.Rect(5, 5, 28, 32), Image1.Picture.Graphic);
     TextOut(Rect.Left + 5, Rect.Top + 16, Descricao);
     TextOut(Rect.Left + 5, Rect.Top + 32, 'Pis:' + Pis);
     //Canvas_LineH(Canvas, Rect.Left, Rect.Right, Rect.Bottom - 1, clSilver, psDot);
     end;
end;

procedure TfrmRepTrilobitPrincipal.sgTerminaisDblClick(Sender: TObject);
begin
if Abrir_Rep_Edicao(sgTerminais.GetIntegerCellSelected(COL_TRM_IND), Grupo) then
   Preencher_Terminais;
end;

function TfrmRepTrilobitPrincipal.Get_Grupo: integer;
var
  Cnpj: string;
begin
Result := 0;
Cnpj := Parametro_Linha_Comando_Variavel('CNPJ');

if Cnpj = '' then
   begin
   ErrorMessage('O CNPJ deve ser informado como parâmetro');
   Exit;
   end;

Result := StrToIntDef(GetValueField(DM.Q, 'SELECT GRU_IND FROM Grupos WHERE GRU_CNPJ = ' + QuotedStr(Cnpj)), 0);

if Result = 0 then
   ErrorMessage('O CNPJ informado é inválido');
end;

procedure TfrmRepTrilobitPrincipal.Inicializar;
begin
Ler_Funcionarios_Terminal;
Preencher_Funcionarios(sgFuncionarios);
Preencher_Terminais;
Preencher_Empregador;
end;

end.
