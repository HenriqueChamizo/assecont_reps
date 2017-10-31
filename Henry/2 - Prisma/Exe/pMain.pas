unit pMain;

interface

uses
  Windows, Forms, SysUtils, pPrincipal, Graphics, OleServer, ImgList, Controls, cxGraphics,
  WRegistry, dxBar, cxClasses, StdCtrls, rmLabel, WPanel, Grids, WStringGrid, ScktComp,
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
    procedure ExcluirnoTerminal1Click(Sender: TObject);
    procedure btEnviarEmpregadorClick(Sender: TObject);
    procedure AssociarcomoFuncionrio1Click(Sender: TObject);
    procedure btDataHoraClick(Sender: TObject);
    procedure Kernel1Progresso(ASender: TObject; pThreadIndex, pByte, pByteMax,
      pBuffer, pBufferMax: Integer);
    procedure btImportarMarcacoesClick(Sender: TObject);
    procedure Kernel1ColetaEventos(ASender: TObject; pThreadIndex: Integer;
      pResultado: WordBool; pQtdeEventosColetados: Integer;
      const pPathAFD: WideString);
    procedure EnviarFuncionriosparaoTerminal1Click(Sender: TObject);
    procedure ExcluirnoTerminal2Click(Sender: TObject);
  private
    CaminhoAfd: string;
    IndexTerminal: integer;
    procedure Conectar;
    //procedure LerTerminal;
    //procedure PreencherStringListIdsTerminais; override;

    procedure EnviarEmpregador(RazaoSocial, Cnpj, Cei, Endereco: string);
    procedure Importar_Afd(Local: string; Data: TDateTime; ImportarTudo: boolean);
    procedure Abrir_Progresso_Importacao;
    //procedure Enviar_Terminal(EhDigital: boolean);
    function Excluir_Registro_Terminal: boolean;
    procedure EnviarPacote(Pacote: string);
    function stringToBytes(pPackage : String) : TByteArray;
  public
    procedure PararColeta;
  end;

  TClienteGPRS = class(TClientSocket)
  public
    // overload: evita stack overflow
    constructor Create; reintroduce; overload;

    // Executa ao conectar com o servidor
    procedure doOnConnect(Sender : TObject; Socket : TCustomWinSocket);

    // Executa quando o servidor respondeu à uma requisição
    procedure doOnRead(Sender : TObject; Socket : TCustomWinSocket);

  end;

var
  frmMain: TfrmMain;
  Cliente : TClienteGPRS;
  _gPacoteResponse : String;

implementation

uses pProgressoImportacao;

{$R *.dfm}

procedure TfrmMain.Conectar;
begin
if TerminalConectado = GetIndiceSelecao then Exit;

SetMensagem(GetIpSelecao, 'CONECTANDO...');
Cliente.Host := GetIpSelecao;
Cliente.Port := GetPortaSelecao;
Cliente.Active := True;
TerminalConectado := GetIndiceSelecao
end;

{procedure TfrmMain.Enviar_Terminal(EhDigital: boolean);
var
  Matricula: string;
  R: boolean;
  UsuarioEquipamento: SUsuarioEquipamento;
  Func: integer;
  Nome, NomeNoTerminal, Pis: string;
begin
inherited;

if EhDigital then
   Matricula := GetMatriculaSelecao
else
    Matricula := IntToStr(GetFuncionarioMatriculaSelecao);

if slTerminalMatriculas.IndexOf(Matricula) > -1 then
   NomeNoTerminal := slTerminalNomes[slTerminalMatriculas.IndexOf(Matricula)]
else
    NomeNoTerminal := '';

Func := GetFuncionarioIndiceSelecao;
Nome := GetFuncionarioNomeSelecao;
Pis := GetPisFuncionario(Func);

if Pis = '' then
   ErrorMessage('O PIS do funcionário ' + Nome + ' não está cadastrado', True);

if NomeNoTerminal = '' then
   begin
   SetMensagem(GetIpSelecao, 'ASSOCIANDO MATRÍCULA ' + Matricula + ': ' + Nome + '...');

   with UsuarioEquipamento do
        begin
        Matriculas := Matricula;
        PIS := GetPisFuncionario(Func);
        Nome := GetFuncionarioNomeSelecao;
        VerificaDigital := EhDigital;
        TipoOperacao := couAdicao;
        end;

   R := Kernel1.EnviaUsuarioEquipamento[IndexTerminal, UsuarioEquipamento];
   end
else
    R := True;

SetMensagem(GetIpSelecao, R);

if R then
   IncluirMatricula(Func, Matricula, Nome);
end;}

procedure TfrmMain.AssociarcomoFuncionrio1Click(Sender: TObject);
begin
inherited;

//Enviar_Terminal(True);
end;

procedure TfrmMain.btConectarClick(Sender: TObject);
begin
inherited;

Conectar;
//PreencherStringListIdsTerminais;
end;

procedure TfrmMain.btDataHoraClick(Sender: TObject);
var
  R: boolean;
begin
Conectar;

inherited;

//R := Kernel1.EnviaDataHora[IndexTerminal, Now];
SetMensagem(GetIpSelecao, R);
end;

procedure TfrmMain.EnviarPacote(Pacote: string);
{ Envia o pacote selecionado ao equipamento. }
var
  _rPacoteBytes : TByteArray;
  _rPacoteHex : string;
  i : Integer;
begin
try
  _rPacoteBytes := stringToBytes(Pacote);

  for i := 0 to Length(_rPacoteBytes) -1 do
      _rPacoteHex := _rPacoteHex + IntToHex(_rPacoteBytes[i], 2) + ' ';

  Cliente.Socket.SendBuf(Pointer(_rPacoteBytes)^, Length(_rPacoteBytes));
Except
  SetMensagem(GetIpSelecao, 'ERRO AO ENVIAR COMANDO');
  end;
end;

{
procedure TfrmMain.LerTerminal;
var
  R: boolean;
  Usuario: SUsuarioEquipamento;
  UsuarioEx: SUsuarioBioEx;
  Matriculas: string;
  I: integer;

  procedure AddUsuario(Matricula, Nome, Id: string);
  begin
  Matricula := TrimZeros(Matricula);
  Nome := Trim(Nome);

  if slTerminalMatriculas.IndexOf(Matricula) = -1 then
     begin
     slTerminalMatriculas.Add(Matricula);
     slTerminalIds.Add(Id);
     slTerminalNomes.Add(Nome);
     end;
  end;

  procedure AddUsuarioEx(Matricula, Id: string);
  var
    I: integer;
  begin
  Matricula := TrimZeros(Matricula);
  Id := TrimZeros(Id);

  I := slTerminalMatriculas.IndexOf(Matricula);

  if I = -1 then
     AddUsuario(Matricula, '', Id)
  else
      slTerminalIds[I] := Id;
  end;

begin
SetMensagem(GetIpSelecao, 'LENDO FUNCIONÁRIOS...');
Screen.Cursor := crHourGlass;

try
  R := Kernel1.RecebeListaUsuarioEquipamento[IndexTerminal];

  slTerminalMatriculas.Clear;
  slTerminalIds.Clear;
  slTerminalNomes.Clear;

  while Kernel1.Rec_UsuarioEquipamento[IndexTerminal, Usuario] do
        begin
        Matriculas := Usuario.Matriculas;

        if Pos(';', Matriculas) > 0 then
           begin
           for I := 1 to WordCount(Matriculas) do
               AddUsuario(ExtractWord(I, Matriculas), Usuario.Nome, '');
           end
        else
            AddUsuario(Matriculas, Usuario.Nome, '');
        end;
finally
  Screen.Cursor := crDefault;
  end;

Screen.Cursor := crHourGlass;

try
  R := Kernel1.Bio_RecListaUsuarios[IndexTerminal];

  if R then
     while Kernel1.Bio_GetUsuario[IndexTerminal, UsuarioEx] do
           begin
           AddUsuarioEx(UsuarioEx.Matricula, UsuarioEx.Id);
           end;

finally
  Screen.Cursor := crDefault;
  end;

SetMensagem(GetIpSelecao, True);
sgFuncionarios.Invalidate;
end;
}
procedure TfrmMain.btEnviarEmpregadorClick(Sender: TObject);
{var
  Empregador: SEmpregador;
  R: boolean;}
begin
Conectar;
SetMensagem(GetIpSelecao, 'LENDO DADOS DO EMPREGADOR...');

//R:= Kernel1.RecebeDadosEmpregador[IndexTerminal, Empregador];

//if R then
{   Abrir_Dados_Empregador(
                       EnviarEmpregador,
                       Trim(Empregador.RazaoSocial),
                       Trim(Empregador.Documento),
                       Trim(Empregador.CEI),
                       Trim(Empregador.Local)
                       );}

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
   sLocal := GetDocumentsDir + 'Asseponto\Marcacoes\' + FormatDateTime('yyyymmdd', Date) + '.txt';

   if not DirectoryExists(ExtractFilePath(sLocal)) then
      ForceDirectories(ExtractFilePath(sLocal));
//   end;

Abrir_Importar_Afd(Importar_Afd, sLocal, StrToDateDef(sData, Date), False);
end;

procedure TfrmMain.Importar_Afd(Local: string; Data: TDateTime; ImportarTudo: boolean);
{var
  R: boolean;
  Empregador: SEmpregador;}
begin
{Abrir_Progresso_Importacao;

WRegistry1.Write('Opcoes', 'Ultimo filtro de data', Data);
WRegistry1.Write('Opcoes', 'Ultimo arquivo afd', Local);
Conectar;

SetMensagem(frmMain.GetIpSelecao, 'IMPORTANDO MARCAÇÕES APARTIR DE ' + DateToStr(Data) + '...');

CaminhoAfd := Local;

R := Kernel1.ColetaEventosEx[IndexTerminal, Local, Data, Empregador];
SetMensagem(frmMain.GetIpSelecao, IIFString(R, 'OK', 'NENHUM REGISTRO'));}
end;

procedure TfrmMain.EnviarEmpregador(RazaoSocial, Cnpj, Cei, Endereco: string);
{var
  Empregador: SEmpregador;
  R: boolean;}
begin
inherited;

{Empregador.IdEmpregador := cieCNPJ;
Empregador.Documento := Cnpj;
Empregador.CEI := Cei;
Empregador.RazaoSocial := RazaoSocial;
Empregador.Local := Endereco;

R := Kernel1.EnviaDadosEmpregador[IndexTerminal, Empregador];

SetMensagem(GetIpSelecao, R);}
end;

procedure TfrmMain.EnviarFuncionriosparaoTerminal1Click(Sender: TObject);
begin
//Enviar_Terminal(False);
end;

procedure TfrmMain.ExcluirnoTerminal1Click(Sender: TObject);
{var
  Matricula, Id: string;
  R: boolean;
  UsuarioEquipamento: SUsuarioEquipamento;
  I: integer;
  slIdsExclusao, slMatriculasExclusao: TStringList;}
begin
inherited;

{slIdsExclusao := TStringList.Create;
slMatriculasExclusao := TStringList.Create;

try
  for I := 0 to sgIds.RowsSelected.Count - 1 do
      begin
      Matricula := ExtractWord(1, sgIds.GetStringCellSelected(0, I), ' ');
      slMatriculasExclusao.Add(Matricula);
      slIdsExclusao.Add(GetIdFromMatricula(Matricula));
      end;

  for I := 0 to slIdsExclusao.Count - 1 do
      begin
      Id := slIdsExclusao[I];
      Matricula := slMatriculasExclusao[I];

      SetMensagem(GetIpSelecao, 'EXCLUINDO MATRICULA ' + Matricula + '...');

      UsuarioEquipamento.Matriculas := Matricula;
      UsuarioEquipamento.TipoOperacao := couExclusao;

      R := Kernel1.Bio_DelTemplate[IndexTerminal, Id, True];
      R := Kernel1.EnviaUsuarioEquipamento[IndexTerminal, UsuarioEquipamento];

      SetMensagem(GetIpSelecao, R);

      if R then
         ExcluirMatricula(StrToInt(Matricula));
      end;
finally
  slIdsExclusao.Free;
  slMatriculasExclusao.Free;
  end;}
end;

procedure TfrmMain.ExcluirnoTerminal2Click(Sender: TObject);
var
  Matricula, Func: integer;
begin
Matricula := GetFuncionarioMatriculaSelecao;
Func := GetFuncionarioIndiceSelecao;

if Excluir_Registro_Terminal then
   ExcluirMatricula(Matricula, Func);
end;

procedure TfrmMain.FormCreate(Sender: TObject);
begin
TerminalConectado := 0;
Fabricante := 'Henry';

inherited;
end;

procedure TfrmMain.Kernel1ColetaEventos(ASender: TObject; pThreadIndex: Integer;
  pResultado: WordBool; pQtdeEventosColetados: Integer;
  const pPathAFD: WideString);
begin
if frmProgressoImportacao <> NIL then
   begin
   frmProgressoImportacao.Terminou := True;
   frmProgressoImportacao.Close;
   end;

SetMensagem(GetIpSelecao, 'AFD SALVO EM ' + CaminhoAfd);
WRegistry1.Write('Apontamento', 'Ultimo arquivo de importacao', CaminhoAfd);
SetMensagem(GetIpSelecao, 'QUANTIDADE DE MARCAÇÕES: ' + IntToStr(pQtdeEventosColetados));
end;

procedure TfrmMain.Kernel1Progresso(ASender: TObject; pThreadIndex, pByte,
  pByteMax, pBuffer, pBufferMax: Integer);
begin
if (pByte = 0) and (pByteMax > 20) then
   begin
   frmProgressoImportacao.Bloco := frmProgressoImportacao.Bloco + 1;

   frmProgressoImportacao.ProgressBar1.Max := pByteMax;
   frmProgressoImportacao.ProgressBar1.Position := 0;
   end
else
    if (pByte > 0) and (pByteMax > 20) then
       frmProgressoImportacao.ProgressBar1.Position := frmProgressoImportacao.ProgressBar1.Position + 1;
end;

procedure TfrmMain.PararColeta;
begin
{
Kernel1.PararColetaEventos(IndexTerminal);
SetMensagem(GetIpSelecao, 'IMPORTAÇÃO INTERROMPIDA');
}
end;

{procedure TfrmMain.PreencherStringListIdsTerminais;
begin
LerTerminal;

inherited;
end;}

procedure TfrmMain.Abrir_Progresso_Importacao;
begin
Application.CreateForm(TfrmProgressoImportacao, frmProgressoImportacao);
frmProgressoImportacao.Show;
end;

function TfrmMain.Excluir_Registro_Terminal: boolean;
{var
  Matricula, Nome: string;
  UsuarioEquipamento: SUsuarioEquipamento;}
begin
{Matricula := IntToStr(GetFuncionarioMatriculaSelecao);
Nome := GetFuncionarioNomeSelecao;

with UsuarioEquipamento do
     begin
     Matriculas := Matricula;
     TipoOperacao := couExclusao;
     end;

SetMensagem(GetIpSelecao, 'EXCLUINDO MATRÍCULA ' + Matricula + ': ' + Nome + '...');
Result := Kernel1.EnviaUsuarioEquipamento[IndexTerminal, UsuarioEquipamento];
SetMensagem(GetIpSelecao, Result);}
end;

{ TClienteGPRS }

constructor TClienteGPRS.Create;
begin
  inherited Create(nil);
end;

procedure TClienteGPRS.doOnConnect(Sender: TObject; Socket: TCustomWinSocket);
{ Muda o status da tela ao conectar com o equipamento. }
begin
frmMain.SetMensagem(frmMain.GetIpSelecao, 'CONECTADO...');
end;

procedure TClienteGPRS.doOnRead(Sender: TObject; Socket: TCustomWinSocket);
{ Realiza a leitura dos dados quando o servidor responde. }
var
  _rPacoteBytes : array of byte;
  _rPacoteString : string;
  i, _rTamanhoPacote: integer;
  _rRele,_rCodREON : String;  // Grupo de String utilizado no Evento Online.
begin
try
  _rTamanhoPacote := Socket.ReceiveBuf(Pointer(nil)^, -1);
  SetLength(_rPacoteBytes, _rTamanhoPacote);
  Socket.ReceiveBuf(Pointer(_rPacoteBytes)^, Length(_rPacoteBytes));

  // Converte e mostra o pacote recebido em hexadecimal
  for i := 0 to _rTamanhoPacote - 1 do
    _rPacoteString := _rPacoteString + IntToHex(_rPacoteBytes[i], 2) + ' ';
  //frmClienteGPRS.mmRecebidos.Lines.Add(_rPacoteString);
  //frmClienteGPRS.mmRecebidos.Lines.Add('');

  // Converte e motra o pacote recebido em string
  SetLength(_rPacoteString, 0);
  for i := 3 to _rTamanhoPacote - 3 do
	  _rPacoteString := _rPacoteString + Chr(_rPacoteBytes[i]);
  _gPacoteResponse := _rPacoteString;
  //frmClienteGPRS.mmRecebidosAscii.Lines.Add(_rPacoteString);
  //frmClienteGPRS.mmRecebidosAscii.Lines.Add('');

  // Verifica se existe um pedido de Aguardando Liberação do evento online.
  {
  if '+REON+00+0' = Copy(_rPacoteString,3,10) then
     begin
     // Verefica qual rele esta ativado.
     if not((frmClienteGPRS.chbxRele1.Checked) and (frmClienteGPRS.chbxRele2.Checked) and (frmClienteGPRS.chbxRele3.Checked)) then
        begin
        _rRele:=''
        end;

     if (frmClienteGPRS.chbxRele1.Checked) then
        begin
        _rRele:='1'
        end;

     if (frmClienteGPRS.chbxRele2.Checked) then
        begin
        _rRele:='2'
        end;

     if (frmClienteGPRS.chbxRele3.Checked) then
        begin
        _rRele:='3'
        end;

     if (frmClienteGPRS.chbxRele1.Checked) and (frmClienteGPRS.chbxRele2.Checked) then
        begin
        _rRele:='12'
        end;

     if (frmClienteGPRS.chbxRele1.Checked) and (frmClienteGPRS.chbxRele3.Checked) then
        begin
        _rRele:='13'
        end;

     if (frmClienteGPRS.chbxRele2.Checked) and (frmClienteGPRS.chbxRele3.Checked) then
        begin
        _rRele:='23'
        end;

     if (frmClienteGPRS.chbxRele1.Checked) and (frmClienteGPRS.chbxRele2.Checked) and (frmClienteGPRS.chbxRele3.Checked) then
        begin
        _rRele:='123'
        end;

     // Verefica qual código de evento que deve ser retornado.
     if ((frmClienteGPRS.rdbtnLiberado.Checked) or (frmClienteGPRS.rdbtnNegado.Checked)) then
        begin
        if (frmClienteGPRS.rdbtnLiberado.Checked) then
           begin
           _rCodREON := '1';
           end;

        if (frmClienteGPRS.rdbtnNegado.Checked) then
           begin
           _rCodREON := '30';
           end;

        // Envia ao equipamento o pacote de resposta do evento online.
        frmClienteGPRS.edtConfirmarEnvio.Clear;
        frmClienteGPRS.edtConfirmarEnvio.Text := Copy(_rPacoteString,1,11)+
          _rCodREON+']'+frmClienteGPRS.edtSEG.Text+']'+frmClienteGPRS.edtMsg.Text+
          ']'+_rRele;
        proEnviarPacote;
        frmClienteGPRS.edtConfirmarEnvio.Clear
        end;
     end;
     }
Except
  frmMain.SetMensagem(frmMain.GetIpSelecao, 'Falha ao receber dados do equipamento.');
  end;
end;

function TfrmMain.stringToBytes(pPackage : String) : TByteArray;
{ Recebe o pacote de dados no formato texto e converte para um array de bytes,
  adicionando o byte inicial e final, calculando o checksum e convertendo cada
  caracter da string em inteiro. Retorna o array de bytes convertido. }

  procedure NextByte(var pByteArray : TByteArray; var index : Integer);
  { Disponibiliza mais um byte em um array de bytes, alocando um novo espaço e
    incrementando o índice. }
  begin
    SetLength(pByteArray, Length(pByteArray) + 1);
    inc(index);
  end;

const
  CONST_START_BYTE = 02; // Byte inicial.
  CONST_END_BYTE = 03; // Byte final.
var
  _rChecksum, i, idx : Integer;
begin
  // Inicializa o índice do array e o checksum.
  idx := 0;
  SetLength(Result, 1);
  _rChecksum := 0;

  // Carrega o byte inicial
  Result[idx] := CONST_START_BYTE;

  // Calcula e carrega o tamanho do pacote
  NextByte(Result, idx);
  Result[idx] := Length(pPackage) and $FF;
  NextByte(Result, idx);
  Result[idx] := (Length(pPackage) shr 8) and $FF;

  // Converte cada caracter da string recebida em inteiro e carrega no array
  NextByte(Result, idx);
  for i := 1 to Length(pPackage) do
  begin
    Result[idx] := Ord(pPackage[i]);
    NextByte(Result, idx);
  end;

  // Realiza o cálculo do checksum com os bytes do pacote e com o seu tamanho,
  // e após calcular ele é carregado no array
  for i := 0 to Length(pPackage) do
    _rChecksum := _rChecksum xor Ord(pPackage[i]);
  _rChecksum := _rChecksum xor (Length(pPackage) and $FF);
  _rChecksum := _rChecksum xor ((Length(pPackage) shr 8) and $FF);
  Result[idx] := _rChecksum;

  // Carrega o byte final
  NextByte(Result, idx);
  Result[idx] := CONST_END_BYTE;
end;

end.
