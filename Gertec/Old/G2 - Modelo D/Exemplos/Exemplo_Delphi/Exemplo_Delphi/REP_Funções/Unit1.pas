{$A+,B-,C+,D+,E-,F-,G+,H+,I+,J+,K-,L+,M-,N+,O+,P+,Q-,R-,S-,T-,U-,V+,W-,X+,Y+,Z1}
{$MINSTACKSIZE $00004000}
{$MAXSTACKSIZE $00100000}
{$IMAGEBASE $00400000}
{$APPTYPE GUI}
//******************************************************************************
// Autor: Débora A. Andrade
// Data: 06/08/2011
// Referencia: Software para coleta de Registro Eletronico de Ponto(REP)
// Ambiente: Delphi 5.0
//******************************************************************************
unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  ExtCtrls, ComCtrls, StdCtrls, OleCtrls, ShellAPI, ShlObj, CommCtrl;

type
  TForm1 = class(TForm)
    panel2: TPageControl;
    TabSheet6: TTabSheet;
    GroupBox13: TGroupBox;
    Label53: TLabel;
    cbTIAba2REP: TComboBox;
    GroupBox14: TGroupBox;
    Label54: TLabel;
    Label55: TLabel;
    dtTIAba2DataREP: TDateTimePicker;
    dtTIAba2HoraREP: TDateTimePicker;
    btnTIAba2EnviarDataHora: TButton;
    btnTIAba2ReceberDataHora: TButton;
    GroupBox15: TGroupBox;
    Label56: TLabel;
    Label57: TLabel;
    txtTIAba2MemoriaUsada: TEdit;
    txtTIAba2MemoriaLivre: TEdit;
    btnTIAba2ReceberEspacoMemoria: TButton;
    TabSheet7: TTabSheet;
    TabSheet1: TTabSheet;
    GroupBox2: TGroupBox;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    cbRHAba1NomeREP1: TComboBox;
    cbRHAba1Identificador: TComboBox;
    txtRHAba1CEI: TEdit;
    txtRHAba1Endereco: TEdit;
    btnRHAba1Cadastrar: TButton;
    btnRHAba1Consultar: TButton;
    btnRHAba1Alterar: TButton;
    TabSheet8: TTabSheet;
    PageControl2: TPageControl;
    tabPage4: TTabSheet;
    GroupBox3: TGroupBox;
    Label7: TLabel;
    cbRHAba2REP: TComboBox;
    GroupBox4: TGroupBox;
    Label8: TLabel;
    Label9: TLabel;
    Label10: TLabel;
    Label11: TLabel;
    Label12: TLabel;
    Label14: TLabel;
    txtRHAba2Nome: TEdit;
    txtRHAba2Grupo: TEdit;
    txtRHAba2Teclado: TEdit;
    txtRHAba2RFID: TEdit;
    btnRHAba2Cadastrar: TButton;
    btnRHAba2Excluir: TButton;
    btnRHAba2Alterar: TButton;
    TabSheet2: TTabSheet;
    TabSheet3: TTabSheet;
    GroupBox10: TGroupBox;
    Label37: TLabel;
    txtRHAba4SalvarEm: TEdit;
    btnRHAba4Abrir: TButton;
    btnRHAba3Coletar: TButton;
    GroupBox16: TGroupBox;
    Label58: TLabel;
    Label60: TLabel;
    Label61: TLabel;
    Label63: TLabel;
    Label64: TLabel;
    cbTIAba3REPsCadastrados: TComboBox;
    txtTIAba3IPREP: TEdit;
    txtTIAba3MascaraRede: TEdit;
    txtTIAba3NomeREP: TEdit;
    txtTIAba3NS: TEdit;
    btnTIAba3Cadastrar: TButton;
    btnTIAba3Consultar: TButton;
    btnTIAba3Alterar: TButton;
    GroupBox1: TGroupBox;
    Label26: TLabel;
    Label27: TLabel;
    Label28: TLabel;
    txtTIAba2Temperatura: TEdit;
    txtTIAba2Impressora: TEdit;
    btnTIAba2ReceberTemp: TButton;
    btnTIAba2ReceberImpress: TButton;
    GroupBox7: TGroupBox;
    Label29: TLabel;
    Label30: TLabel;
    Label31: TLabel;
    txtTIAba2RegistroInicial: TEdit;
    txtTIAba2RegistroFinal: TEdit;
    txtTIAba2NomeArquivo: TEdit;
    Label32: TLabel;
    btnTIAba2ReceberRegistro: TButton;
    txtRHAba1CNPJCPF: TEdit;
    txtRHAba1RazaoSocial: TEdit;
    txtRHAba2CodigoBarras: TEdit;
    Label13: TLabel;
    GroupBox6: TGroupBox;
    Label24: TLabel;
    btnRHAba2Abrir2: TButton;
    btnRHAba2Receber: TButton;
    GroupBox5: TGroupBox;
    cbRHAba3REP: TComboBox;
    Label15: TLabel;
    txtRHAba2Caminho: TEdit;
    GroupBox8: TGroupBox;
    Label16: TLabel;
    cbRHAba2REP3: TComboBox;
    GroupBox9: TGroupBox;
    Label17: TLabel;
    btnRHAba2AbrirBio: TButton;
    btnRHAba2EnviarBio: TButton;
    GroupBox11: TGroupBox;
    Label18: TLabel;
    txtRHAba2Caminho2: TEdit;
    btnRHAba2ReceberBio: TButton;
    Label19: TLabel;
    txtRHAba2PIS3: TEdit;
    btnRHAba2SalvarBio: TButton;
    btnRHAba2ExcluirBio: TButton;
    btnRHAba2FormataBio: TButton;
    txtRHAba2Arquivo2: TMemo;
    OpenDialog1: TOpenDialog;
    txtRHAba2PIS: TEdit;

    procedure btnRHAba1CadastrarClick(Sender: TObject);
    procedure cbRHAba1NomeREP1Change(Sender: TObject);
    procedure txtRHAba1RazaoSocialChange(Sender: TObject);
    procedure txtRHAba1CNPJCPFKeyPress(Sender: TObject; var Key: Char);
    procedure txtRHAba1CNPJCPFChange(Sender: TObject);
    procedure txtRHAba1CEIKeyPress(Sender: TObject; var Key: Char);
    procedure txtRHAba1EnderecoChange(Sender: TObject);
    procedure btnTIAba3CadastrarClick(Sender: TObject);
    procedure btnTIAba3AlterarClick(Sender: TObject);
    procedure btnTIAba2EnviarDataHoraClick(Sender: TObject);
    procedure btnTIAba3ConsultarClick(Sender: TObject);
    procedure btnTIAba2ReceberEspacoMemoriaClick(Sender: TObject);
    procedure btnTIAba2ReceberTempClick(Sender: TObject);
    procedure btnTIAba2ReceberImpressClick(Sender: TObject);
    procedure btnTIAba2ReceberRegistroClick(Sender: TObject);
    procedure btnRHAba1ConsultarClick(Sender: TObject);
    procedure btnRHAba1AlterarClick(Sender: TObject);
    procedure btnRHAba2CadastrarClick(Sender: TObject);
    procedure btnRHAba2ExcluirClick(Sender: TObject);
    procedure btnRHAba2AlterarClick(Sender: TObject);
    procedure cbRHAba2REPChange(Sender: TObject);
    procedure cbTIAba2REPChange(Sender: TObject);
    procedure btnRHAba2ReceberClick(Sender: TObject);
    procedure btnRHAba2Abrir2Click(Sender: TObject);
    procedure btnRHAba4AbrirClick(Sender: TObject);
    procedure btnRHAba3ColetarClick(Sender: TObject);
    procedure btnRHAba2AbrirBioClick(Sender: TObject);
    procedure btnRHAba2EnviarBioClick(Sender: TObject);
    procedure btnRHAba2FormataBioClick(Sender: TObject);
    procedure btnRHAba2ExcluirBioClick(Sender: TObject);
    procedure btnRHAba2SalvarBioClick(Sender: TObject);
    procedure btnRHAba2ReceberBioClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure btnTIAba2ReceberDataHoraClick(Sender: TObject);

  private
    { Private declarations }
  public
    { Public declarations }
  end;
  type
  TStringArray = array of string;

  function Split(const Str: string; Delimiter : Char; Max : integer = 0): TStringArray;
var
  Form1: TForm1;
  ip: String;

  implementation

{$R *.DFM}

{ Declaração da DLL para comunicação do Registro Eletronico de Ponto (R.E.P)}

procedure REP_Conexao_Simples(sIP: String; bAcao: BYTE; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_InfoTerminal(sIP: STRING; sNome: PCHAR; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_LeIntervaloNSR(sIP: STRING; sNSRInicial: STRING; sNSRFinal: STRING; sArquivoDados: STRING; dwEncontrados: PINT; dwResult: PINT ); far; stdcall; external 'REP_Gertec.dll';

procedure REP_Tempo (sIP: STRING; bLeGrava: BYTE; var bAno: INTEGER; var bMes: BYTE; var bDia: BYTE; var bHora: BYTE; var bMinuto: BYTE; var bSegundo: BYTE; var bDiaSemana: BYTE; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_LeTemperatura (sIP: STRING; bTemperatura: PBYTE; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_StatusImpressora (sIP: STRING; sStatus: PCHAR; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_Rede_Simples(sIP: STRING; bLeGrava: BYTE; sIPTerminal: PCHAR; sIPMascara: PCHAR; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_LeCadastro(sIP: STRING; bFiltro: BYTE; sParametro: STRING; sArquivoDados: STRING; dwEncontrados: PINT; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_GravaCadastroEmpregador(sIP: STRING; bAcao: CHAR; bTipo: CHAR; var sCPFCNPJ: BYTE; var sCEI: BYTE; var sRazaoSocial: BYTE; var sLocal: BYTE; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_GravaCadastroFuncionario(sIP: STRING; bAcao: CHAR; var sNome: BYTE; var sPIS: BYTE; var vcContactLess: BYTE; var vbCodBarras: BYTE; var vbTeclado: BYTE; var vbBiometria: BYTE; bGrupo: BYTE; dwResult:PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_ExcluiCadastroFuncionario(sIP: STRING; var sPIS: BYTE; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_LeEspacoMemoria (sIP: STRING; dwMemUsada: PINT; dwMemLivre: PINT; dwResult: PINT) ; far; stdcall; external 'REP_Gertec.dll';

procedure REP_LeNSR (sIP: STRING; sNSR: PCHAR; dwResult: PINT ); far; stdcall; external 'REP_Gertec.dll';

procedure REP_FormataFinger(sIP: STRING; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_RemoverFingerID(sIP: STRING; var sPis: BYTE; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_LeFinger(sIP: STRING; dwEncontrados: PBYTE; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_LeFingerID (sID: STRING; var sPis: BYTE; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_GravaFinger(sIP: STRING; sArquivoDados: STRING; dwResult: PINT); far; stdcall; external 'REP_Gertec.dll';

procedure REP_MensagemErro (dwResult: DWORD ; var sMensagemErro: BYTE); far; stdcall; external 'REP_Gertec.dll';

//*****************************Funções de Validação*****************************
//--------------------------------------------------------------------------------
// function: ValidaPIS
//--------------------------------------------------------------------------------
function ValidaPIS(sPIS: String; MostraMsg: Boolean = True): Boolean;
var
    iSoma: Integer;
    i, iDigito, iDigVer, iMult: Shortint;
begin
    Result := Length(Trim(sPIS)) = 11;

    if Result then
    begin
        iDigVer := strtoint(sPIS[11]);
        iSoma := 0;
        iMult := 2;

        for i := 10 downto 1 do
        begin
            iSoma := iSoma + (iMult * strtoint(sPIS[I]));
            if iMult < 9 then
                Inc(iMult)
            else
                iMult := 2;
        end;

        iDigito := 11 - (iSoma mod 11);

        if iDigito > 9 then
            iDigito := 0;

        Result := (iDigVer = iDigito);
    end;

    if not Result and MostraMsg then
        Application.MessageBox('Número do PIS inválido!', 'Atenção', mb_TaskModal + mb_IconWarning);
end;
//**************************************FIM***************************************
//--------------------------------------------------------------------------------
// function: Split
//--------------------------------------------------------------------------------
function Split(const Str: string; Delimiter : Char; Max : integer = 0): TStringArray;
var
    Size, i: integer;
begin
    Size := 0;
    SetLength(Result, 1);
    Result[0] := '';
    for i := 1 to Length(Str) do
    begin
        if Str[i] = Delimiter then
        begin
            Inc(Size);
            if (Max <> 0) and (Size = Max) then
                Break;
                SetLength(Result, Size+1);
        end
        else
            Result[Size] := Result[Size] + Str[i];
    end;
end;

//--------------------------------------------------------------------------------
// function: bintostr
//--------------------------------------------------------------------------------
function bintostr(const bin: array of byte): string;
const HexSymbols = '0123456789ABCDEF';
var i: integer;
begin
  SetLength(Result, 2*Length(bin));
  for i :=  0 to Length(bin)-1 do begin
    Result[1 + 2*i + 0] := HexSymbols[1 + bin[i] shr 4];
    Result[1 + 2*i + 1] := HexSymbols[1 + bin[i] and $0F];
  end;
end;

//--------------------------------------------------------------------------------
// function: Replace
//--------------------------------------------------------------------------------
function Replace(str: String; char: String): String;
var
    i: Integer;
    novaString: String;
    buf1: array [0..255] of BYTE;
    buf1Aux: array [WORD] of BYTE;
    buf2: array [WORD] of BYTE;
begin
    // Convertendo o que está na variável str para um vetor de byte
    StrPCopy(@buf1,str);
    StrPCopy(@buf2,char);
    i := 0;
    while (i < Length(str)) do
    begin
        if (buf1[i] <> buf2[0]) then
        begin
            buf1Aux[0] := buf1[i];
            novaString := novaString + PChar(@buf1Aux[0]);
        end;
        i := i + 1;
    end;

    replace := novaString;
end;

//--------------------------------------------------------------------------------
// function: ValidaCPF
//--------------------------------------------------------------------------------
function ValidaCPF(vrCPF: String): Boolean;
var
    valor: String;
    igual: Boolean;
    i, soma, resultado: Integer;
    numeros: array [0..10] of Integer;
begin
    //Retira a pontuação
    valor := Replace(vrCPF, '.');
    valor := Replace(valor, '-');

    // Se a quantidade de digitos for diferente de 11 já está incorreto
    if (Length(valor) <> 11) then
        ValidaCPF := False;

    // Depois comentar o resto !!!
    igual := True;

    for i := 2 to 10 do
        if (igual = True) then
            if (valor[i] <> valor[1]) then
                igual := False;

    if ((igual = True) or (valor = '12345678909')) then
        ValidaCPF := False;

    for i := 0 to 10 do
        numeros[i] := StrToInt(valor[i + 1]);

    soma := 0;
    for i := 0 to 8 do
        soma := soma + (10 - i) * numeros[i];

    resultado := soma mod 11;

    if ((resultado = 1) or (resultado = 0)) then
    begin
        if (numeros[9] <> 0) then
            ValidaCPF := False;
    end
    else if (numeros[9] <> (11 - resultado)) then
        ValidaCPF := False;

    soma := 0;
    for i := 0 to 9 do
        soma := soma + (11 - i) * numeros[i];

    resultado := soma mod 11;
    if ((resultado = 1) or (resultado = 0)) then
    begin
        if (numeros[10] <> 0) then
            ValidaCPF := False;
    end
    else
        if (numeros[10] <> (11 - resultado)) then
            ValidaCPF := False;
    ValidaCPF := True;
end;

//--------------------------------------------------------------------------------
// function: ValidaCNPJ
//--------------------------------------------------------------------------------
function ValidaCNPJ(vrCNPJ: String): Boolean;
var
    CNPJ, ftmt: String;
    digitos: array [0..13] of Integer;
    soma: array [0..1] of Integer;
    resultado: array [0..1] of Integer;
    nrDig: Integer;
    CNPJOk: array [0..1] of Boolean;
begin
    // Retirando a pontuação para utilizar somente os numeros
    CNPJ := Replace(vrCNPJ, '.');
    CNPJ := Replace(CNPJ, '/');
    CNPJ := Replace(CNPJ, '-');

    ftmt := '6543298765432';

    soma[0] := 0;
    soma[1] := 0;

    resultado[0] := 0;
    resultado[1] := 0;

    CNPJOk[0] := False;
    CNPJOk[1] := False;

    Try
        for nrDig := 0 to 13 do
        begin
            digitos[nrDig] := StrToInt(Copy(CNPJ,nrDig, 1));
                if (nrDig <= 11) then
                    soma[0] := soma[0] + (digitos[nrDig] * StrToInt(Copy(ftmt, nrDig + 1, 1)));
                if (nrDig <= 12) then
                    soma[1] := soma[1] + (digitos[nrDig] * StrToInt(Copy(ftmt, nrDig, 1)));
        end;

        for nrDig := 0 to 1 do
        begin
            resultado[nrDig] := (soma[nrDig] mod 11);
            if ((resultado[nrDig] = 0) or (resultado[nrDig] = 1)) then
                CNPJOk[nrDig] := (digitos[12 + nrDig] = 0)
            else
                CNPJOk[nrDig] := (digitos[12 + nrDig] = (11 - resultado[nrDig]));
        end;

        ValidaCNPJ := (CNPJOk[0] and CNPJOk[1]);

    Except
         ValidaCNPJ := False;
    end;
end;


//*****************************Funções de clEnvioDLL******************************

//--------------------------------------------------------------------------------
// function: BrowseDialog
//--------------------------------------------------------------------------------
function BrowseDialog(const Title: string; const Flag: integer): string;
var
  lpItemID : PItemIDList;
  BrowseInfo : TBrowseInfo;
  DisplayName : array[0..MAX_PATH] of char;
  TempPath : array[0..MAX_PATH] of char;
begin
  Result:='';
  FillChar(BrowseInfo, sizeof(TBrowseInfo), #0);
  with BrowseInfo do
  begin
    hwndOwner := Application.Handle;
    pszDisplayName := @DisplayName;
    lpszTitle := PChar(Title);
    ulFlags := Flag;
  end;
  lpItemID := SHBrowseForFolder(BrowseInfo);
  if lpItemId <> nil then
  begin
    SHGetPathFromIDList(lpItemID, TempPath);
    Result := TempPath;
    GlobalFreePtr(lpItemID);
  end;
end;
//--------------------------------------------------------------------------------
// function: GetFileList
//--------------------------------------------------------------------------------
function GetFileList(const Path: string): TStringList ;
var
   I: Integer;
   SearchRec: TSearchRec;
begin
   Result:= TStringList.Create;
   try
     I := FindFirst(Path, 0, SearchRec);
     while I = 0 do
     begin
       Result.Add(copy(SearchRec.Name,1,Pos('.',SearchRec.Name)-1));  // alterar esta linha
       I := FindNext(SearchRec);
     end;
   except
     Result.Free;
     raise;
   end;
end;

//--------------------------------------------------------------------------------
// function: GravarFingerPasta
//--------------------------------------------------------------------------------
function GravarFingerPasta(ip: STRING; pastaFinger: STRING): STRING;
var
    resultado: PINT;
    contador: INTEGER;
    ok: INTEGER;
    pasta: Array [0..1] of String;
begin
    New(resultado);
    Try
        ok := 0;
        resultado^:=0;
        contador := 0;

        REP_Conexao_Simples(ip, 1, resultado);
        //Verifica a pasta que possui as fingers salvas.

        pasta[0] :=  pastaFinger;      //irectory.GetFiles(pastaFinger);
        //arquivo := pasta;
        if ((resultado^ <> 0) and (resultado^ <> 10005)) then //se conectou ou jÃ¡ estÃ¡ conectado
        begin
            REP_Conexao_Simples(ip, 0, resultado);
            Form1.Label13.Visible := true;
            Form1.Label13.Caption := 'Erro - Conexão!';
            Form1.Label13.Font.Color := clRed; //Vermelho
            ok := 1;
            Application.MessageBox( 'Falha na conexão!','Biometrias', mb_TaskModal + mb_IconWarning);
            Form1.Label13.Refresh();
            //labelDebug.BackColor = Color.Red;
        end
        else
        begin
             Form1.Label13.Visible := true;
             Form1.Label13.Caption := 'Enviando cadastro';
             Form1.Label13.Font.Color := clWindowText; //Preto
             Form1.Label13.Update();
             Try
                 REP_GravaFinger(ip, pasta[0], resultado);
                 //Se o PIS não estiver cadastrado.
                 if (resultado^ = 3031)then
                 begin
                     Application.MessageBox( 'PIS não cadastrado!','Biometrias', mb_TaskModal + MB_ICONEXCLAMATION);
                     contador := contador - 1;
                 end;
             //Desconecta o REP.
             REP_Conexao_Simples(ip, 0, resultado);
             contador := contador + 1;
             Except
                 Form1.Label13.Visible := true;
                 Form1.Label13.Caption := 'Erro - Exception!';
                 Form1.Label13.Font.Color := clRed; //Vermelho
             end;
             Form1.Label13.Visible := true;
             if (ok = 0)then
             begin
                 Form1.Label13.Caption := 'Fim da Gravação. Enviados: ' + IntToStr(contador);
                 Form1.Label13.Font.Color := clGreen; //Verde
                 if(contador = 0)then
                 begin
                     Application.MessageBox('PIS não cadastrado!','Biometrias', mb_TaskModal + MB_ICONEXCLAMATION)
                 end
                 else
                     Application.MessageBox('Biometrias enviadas com sucesso!','Biometrias', mb_TaskModal + MB_ICONINFORMATION);
             end
             else
             begin
                 Form1.Label13.Caption := 'Erro - Envio cancelado.';
                 Form1.Label13.Font.Color := clRed; //Vermelho
                 Application.MessageBox( 'Biometrias enviadas com sucesso!','Biometrias', mb_TaskModal + MB_ICONERROR);
             end;
        end;
        Dispose(resultado);
    Except
        Form1.Label13.Visible := true;
        Form1.Label13.Caption := 'Erro - Exception!';
        Form1.Label13.Font.Color := clRed; //Vermelho
    end;
end;
//**********************************FIM*******************************************

//*************************** Função atualizaTempoREP ****************************

procedure TForm1.btnRHAba2Abrir2Click(Sender: TObject);
begin
         txtRHAba2Caminho.text:= BrowseDialog('Selecione a pasta.',BIF_RETURNONLYFSDIRS);
end;




//**********************************FIM*******************************************


//*************************** Classe txtFiles ************************************

   
procedure TForm1.btnTIAba3ConsultarClick(Sender: TObject);
var
    resultado: PINT;
    ipRep : Array[0..12] of CHAR;
    maskRep : Array[0..17] of CHAR;
    mascara: String;

    onOff : byte;
    retorno: Integer;
    retornoExistencia: Integer;
    nomeTerminal: Array[0..16] of CHAR;
    numeroSerie: Array[0..8] of CHAR;
    numeroSerieFormatada: String;
    nomeTerminalFormatado: String;
    portaFormatada: STRING;
begin

    New(resultado);

    btnTIAba3Cadastrar.Enabled := false;//Desabilita o botão
    btnTIAba3Consultar.Enabled := false;

    resultado^:= 0;

    REP_Conexao_Simples(ip, 1, resultado);

    if ((resultado^ <> 0) and (resultado^ <> 10005)) then
    begin
        REP_Conexao_Simples(ip, 0, resultado);
        Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);

        btnTIAba3Cadastrar.Enabled := true;
        btnTIAba3Consultar.Enabled := true;
        btnTIAba3Alterar.Enabled := true;
    end
    else
    begin

        REP_Rede_Simples(ip, 0, ipRep, maskRep, resultado);

        mascara:= maskRep;
        //Recebe o nome do terminal.
        REP_InfoTerminal(ip, nomeTerminal, resultado);
        //Informa Numero de Serie do REP.
        REP_LeNSR (ip, numeroSerie, resultado);

        // Caso de ERRO. 204 é quando o REP não responde após receber a configuração, isso é errado mas
        // vamos considera-lo como sucesso, já que o REP não está enviando retorno (o que causa lentidão pois
        // a função fica aguardando uma resposta).
        if (resultado^ <> 0) then
        begin
            retorno := resultado^;
            //Desconecta
            REP_Conexao_Simples(ip, 0, resultado);

            Application.MessageBox( 'Falha ao receber a configuração', '', 65);

            txtTIAba3IPREP.Text := '';
            txtTIAba3MascaraRede.Text := '';
            txtTIAba3NS.Text := '';
            txtTIAba3NomeREP.Text := '';
        end
        else
        begin
            //Caso de sucesso preenche os campos com as informações.
            REP_Conexao_Simples(ip, 0, resultado);

            txtTIAba3MascaraRede.Text:= mascara;
            txtTIAba3IPREP.Text:= ipRep;

            txtTIAba3NomeREP.Text := nomeTerminal;
            txtTIAba3NomeREP.Enabled := False;
            txtTIAba3NomeREP.Color := clMenu;

            txtTIAba3NS.Text := numeroSerie;
            txtTIAba3NS.Enabled := False;

            Application.MessageBox( 'Configuração recebida com sucesso', '', 65);
        end;
    end;
        btnTIAba3Cadastrar.Enabled := true;
        btnTIAba3Consultar.Enabled := false;
        //Desconecta o REP.
        REP_Conexao_Simples(ip, 0, resultado);

        Dispose(resultado);
end;

//***********************************FIM******************************************





//*************************** Cadastrar Empresa ********************************
procedure TForm1.btnRHAba1CadastrarClick(Sender: TObject);
var
    resultado: PINT;

    sCPFCNPJ: Array [0..16] of BYTE;
    sCEI: Array [0..12] of BYTE;
    bRazaoSocial: Array [0..152] of BYTE;
    bLocal: Array [0..101] of BYTE;

    cTipo: CHAR;
    novoCNPJ_CPF: String;
    porta: Integer;
    senha : PBYTE;
    onOff : BYTE;
    teste: string;

begin
    New(resultado);
    New(senha);
    Try

        novoCNPJ_CPF := Replace(txtRHAba1CNPJCPF.Text, '.');
        novoCNPJ_CPF := Replace(novoCNPJ_CPF, '-');
        novoCNPJ_CPF := Replace(novoCNPJ_CPF, '/');

        // Verifica se foi selecionado CPF ou CNPJ
        case cbRHAba1Identificador.ItemIndex of

            // Valida o CNPJ
            0:
            begin
                if ((ValidaCNPJ(txtRHAba1CNPJCPF.Text)) = False) then
                    if ((ValidaCPF(txtRHAba1CNPJCPF.Text)) = True) then
                        cbRHAba1Identificador.ItemIndex := 1
                    else
                        Application.MessageBox('CNPJ Inválido.', 'Cadastrar Empresa', 64);
            end;

            // Valida o CPF
            1:
            begin
                if((ValidaCPF(txtRHAba1CNPJCPF.Text)) = False) then
                    if ((ValidaCNPJ(txtRHAba1CNPJCPF.Text)) = True) then
                        cbRHAba1Identificador.ItemIndex := 0
                    else
                        Application.MessageBox('CPF Inválido.', 'Cadastrar Empresa', 64)
            end;

            else
                cbRHAba1Identificador.ItemIndex := 0;
        end;

        // Verifica se todos os campos foram preenchidos
        if (txtRHAba1RazaoSocial.Text = '') then
        begin
            Application.MessageBox('Preencha o campo referente a Razão Social', 'Cadastrar Empresa', 64);
            txtRHAba1RazaoSocial.SetFocus();
            Exit;
        end
        else if (txtRHAba1Endereco.Text = '') then
        begin
            Application.MessageBox('Preencha o campo referente ao Endereço', 'Cadastrar Empresa', 64);
            txtRHAba1Endereco.SetFocus();
            Exit;
        end;

        // Recebe as informações para cadastrar
        // CNPJ/CPF - Deverá haver 14 posições
        if (cbRHAba1Identificador.ItemIndex = 1) then
            cTipo := '1'
        else
            cTipo := '2';

        // CPF e CNPJ - Deverá haver 12 posições
        StrPCopy(@sCPFCNPJ, txtRHAba1CNPJCPF.Text);

        // CEI - Deverá haver 12 posições
        StrPCopy(@sCEI, txtRHAba1CEI.Text);

        // Razão Social - Deverá haver 150 posições
        StrPCopy(@bRazaoSocial, txtRHAba1RazaoSocial.Text);

        // Endereço - Deverá haver 100 posições
        StrPCopy(@bLocal, txtRHAba1Endereco.Text);

        StrPCopy(@senha^,'1');
        porta:= StrToInt('5290');
        resultado^:= 0;

        REP_Conexao_Simples(ip, 1, resultado);
        if((resultado^ <> 0) and (resultado^ <> 10005))then
        begin
            REP_Conexao_Simples(ip, 0, resultado);
            Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
        end
        else
            REP_GravaCadastroEmpregador(ip, '0', cTipo, sCPFCNPJ[0], sCEI[0], bRazaoSocial[0], bLocal[0], resultado);

            teste:= IntToStr(resultado^);

            if (resultado^ = 0)then
            begin
                txtRHAba1RazaoSocial.Text := '';
                cbRHAba1Identificador.Text := '';
                txtRHAba1CNPJCPF.Text := '';
                txtRHAba1CEI.Text := '';
                txtRHAba1Endereco.Text := '';

                REP_Conexao_Simples(ip, 0, resultado);

                Application.MessageBox( 'Cadastro efetuado com sucesso!', '', 65);
            end
            else
            begin
                if(resultado^ = 2020)then
                begin
                    txtRHAba1RazaoSocial.Text := '';
                    cbRHAba1Identificador.Text := '';
                    txtRHAba1CNPJCPF.Text := '';
                    txtRHAba1CEI.Text := '';
                    txtRHAba1Endereco.Text := '';
  
                    REP_Conexao_Simples(ip, 0, resultado);

                    Application.MessageBox( 'Já possui empregador cadastrado.','', mb_TaskModal + mb_IconWarning)
                end
                else
                begin
                    REP_Conexao_Simples(ip, 0, resultado);
                    Application.MessageBox( 'Não foi possivel efetuar o cadastrado..','', mb_TaskModal + mb_IconWarning);
                end;
               Dispose(resultado);
            end;

    Except
        Application.MessageBox('ERRO.','Cadastrar Empresa',48);
    end;

end;


//Verifica se os requisitos foram preenchidos habilitando o botão cadastrar. "Cadastrar Empresa"
procedure TForm1.cbRHAba1NomeREP1Change(Sender: TObject);
begin
    if (cbRHAba1NomeREP1.Text <> '') then
    begin
        btnRHAba1Consultar.Enabled := True;
        if ((txtRHAba1RazaoSocial.Text <> '') and (txtRHAba1CNPJCPF.Text <> '') and (txtRHAba1Endereco.Text <> '')) then
         begin
            btnRHAba1Cadastrar.Enabled := True;
            btnRHAba1Alterar.Enabled := True
         end
        else
        begin
            btnRHAba1Cadastrar.Enabled := False;
            btnRHAba1Alterar.Enabled := False;
        end
    end
    else
        btnRHAba1Consultar.Enabled := False;
end;


//Verifica se os requisitos foram preenchidos habilitando o botão cadastrar. "Cadastrar Empresa" "Cadastrar Empresa"
procedure TForm1.txtRHAba1RazaoSocialChange(Sender: TObject);
begin
    if ((cbRHAba1NomeREP1.Text <> '') and (txtRHAba1RazaoSocial.Text <> '') and
    (txtRHAba1CNPJCPF.Text <> '') and (txtRHAba1Endereco.Text <> '')) then
     begin
        btnRHAba1Cadastrar.Enabled := True;
        btnRHAba1Alterar.Enabled := True
     end
    else
    begin
        btnRHAba1Cadastrar.Enabled := False;
        btnRHAba1Alterar.Enabled := False;
    end
end;


//Campo numérico, aceita somente números. "Cadastrar Empresa"
procedure TForm1.txtRHAba1CNPJCPFKeyPress(Sender: TObject; var Key: Char);
begin
    If not(Key in['0'..'9', #8]) then
        Key := #0;
end;


// Verifica se os requisitos foram preenchidos habilitando o botão cadastrar. "Cadastrar Empresa"
procedure TForm1.txtRHAba1CNPJCPFChange(Sender: TObject);
begin
    if ((cbRHAba1NomeREP1.Text <> '') and (txtRHAba1RazaoSocial.Text <> '') and
    (txtRHAba1CNPJCPF.Text <> '') and (txtRHAba1Endereco.Text <> '')) then
     begin
        btnRHAba1Cadastrar.Enabled := True;
        btnRHAba1Alterar.Enabled := True
     end
    else
    begin
        btnRHAba1Cadastrar.Enabled := False;
        btnRHAba1Alterar.Enabled := False;
    end;
end;


//Verifica se o que foi digitado é numerico ou backspace e adiciona ao campo. "Cadastrar Empresa"
procedure TForm1.txtRHAba1CEIKeyPress(Sender: TObject; var Key: Char);
begin
    If not(Key in['0'..'9', #8]) then
        Key := #0;
end;


//Verifica se os requisitos foram preenchidos habilitando o botão cadastrar. "Cadastrar Empresa"
procedure TForm1.txtRHAba1EnderecoChange(Sender: TObject);
begin
    if ((cbRHAba1NomeREP1.Text <> '') and (txtRHAba1RazaoSocial.Text <> '') and
    (txtRHAba1CNPJCPF.Text <> '') and (txtRHAba1Endereco.Text <> '')) then
     begin
        btnRHAba1Cadastrar.Enabled := True;
        btnRHAba1Alterar.Enabled := True
     end
    else
    begin
        btnRHAba1Cadastrar.Enabled := False;
        btnRHAba1Alterar.Enabled := False;
    end;
end;


procedure TForm1.btnTIAba3CadastrarClick(Sender: TObject);
var
    resultado: PINT;
    onOff : byte;
    teste: String;
begin

    New(resultado);
    resultado^:= 0;

    btnTIAba3Cadastrar.Enabled := false;//Desabilita o botão
    btnTIAba3Consultar.Enabled := false;

    if ((txtTIAba3IPREP.Text <> '') and (txtTIAba3MascaraRede.Text <> '') and (txtTIAba3NomeREP.Text <> '')) then
    begin
        //Recebe o ip, a mascara e a porta que irá enviar ao REP.
        cbTIAba2REP.Items.Add(txtTIAba3NomeREP.Text);
        cbTIAba3REPsCadastrados.Items.Add(txtTIAba3NomeREP.Text);
        cbRHAba1NomeREP1.Items.Add(txtTIAba3NomeREP.Text);
        cbRHAba2REP.Items.Add(txtTIAba3NomeREP.Text);
        cbRHAba3REP.Items.Add(txtTIAba3NomeREP.Text);
        cbRHAba2REP3.Items.Add(txtTIAba3NomeREP.Text);

        ip:= txtTIAba3IPREP.Text;
        REP_Conexao_Simples(ip, 1, resultado);
        teste := IntToStr(resultado^);

        if ((resultado^ <> 0) and (resultado^ <> 10005)) then
        begin
            REP_Conexao_Simples(ip, 0, resultado);

            Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);

            btnTIAba3Cadastrar.Enabled := true;
            btnTIAba3Consultar.Enabled := true;
         end
         else
         begin
             // Caso de ERRO. 204 é quando o REP não responde após receber a configuração, isso é errado mas
             // vamos considera-lo como sucesso, já que o REP não está enviando retorno (o que causa lentidão pois
             // a função fica aguardando uma resposta).
            if ((resultado^ <> 0) and (resultado^ <> 204)) then
            begin
                REP_Conexao_Simples(ip, 0, resultado);

                Application.MessageBox( 'Falha no cadastro do terminal', '', 65);
                btnTIAba3Cadastrar.Enabled := true;
                btnTIAba3Consultar.Enabled := true;
            end
            else
            begin
                //Caso de Sucesso
                REP_Conexao_Simples(ip, 0, resultado);

                txtTIAba3IPREP.Text := '';
                txtTIAba3MascaraRede.Text := '';
                txtTIAba3NomeREP.Text := '';

                Application.MessageBox( 'Cadastro efetuado com sucesso!', '', 65);

                btnTIAba3Consultar.Enabled := true;
            end;
         end;
    end
    else
        Application.MessageBox( 'Preencher todos os Campos!', '', 65);
    //Desconecta o Terminal
    REP_Conexao_Simples(ip, 0, resultado);
    Dispose(resultado);

    txtTIAba3IPREP.Text := '';
    txtTIAba3MascaraRede.Text := '';
    txtTIAba3NomeREP.Text := '';
end;





procedure TForm1.btnTIAba3AlterarClick(Sender: TObject);
var
    resultado: PINT;
    ipRep : Array[0..12] of CHAR;
    maskRep : Array[0..15] of CHAR;
    onOff : byte;
    retorno: Integer;
    retornoExistencia: Integer;

    teste: String;
begin

    New(resultado);

    btnTIAba3Cadastrar.Enabled := false;//Desabilita o botão
    btnTIAba3Consultar.Enabled := false;

    if ((txtTIAba3IPREP.Text <> '') and (txtTIAba3MascaraRede.Text <> '') and (txtTIAba3NomeREP.Text <> '') ) then
    begin
        //Recebe o ip, a mascara e a porta que irá enviar ao REP.

        resultado^:= 0;

        StrPCopy (@ipRep, txtTIAba3IPREP.Text);
        StrPCopy(@maskRep, txtTIAba3MascaraRede.Text);

        REP_Conexao_Simples(ip, 1, resultado);
                
        if ((resultado^ <> 0) and (resultado^ <> 10005)) then
        begin
            REP_Conexao_Simples(ip, 0, resultado);

            Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);

            btnTIAba3Cadastrar.Enabled := true;
            btnTIAba3Consultar.Enabled := true;
        end
        else
        begin
            //Envia a configuração de IP.
            REP_Rede_Simples(ip, 1, ipRep, maskRep, resultado);
            teste:= IntToStr(resultado^);
            // Caso de ERRO. 204 é quando o REP não responde após receber a configuração, isso é errado mas
            // vamos considera-lo como sucesso, já que o REP não está enviando retorno (o que causa lentidão pois
            // a função fica aguardando uma resposta).
            if ((resultado^ <> 0) and (resultado^ <> 204)) then
            begin
                retorno := resultado^;
                REP_Conexao_Simples(ip, 0, resultado);

                Application.MessageBox( 'Falha no envio da configuração', '', 65);

                btnTIAba3Cadastrar.Enabled := true;
                btnTIAba3Consultar.Enabled := true;
            end
            else
            begin
                //Caso de Sucesso
                REP_Conexao_Simples(ip, 0, resultado);
                txtTIAba3IPREP.Text := '';
                txtTIAba3MascaraRede.Text := '';

                txtTIAba3NomeREP.Text := '';
                txtTIAba3NomeREP.Enabled := True;
                txtTIAba3NomeREP.Color := clWindow;

                txtTIAba3NS.Text := '';

                Application.MessageBox( 'Configuração enviada com sucesso! Realizar novo cadastro do REP.', '', 65);
            end;
        end;
        //Desconecta o REP;
        REP_Conexao_Simples(ip, 0, resultado);
        btnTIAba3Cadastrar.Enabled := true;//Desabilita o botão
        btnTIAba3Consultar.Enabled := false;
        Dispose(resultado);
    end;

end;

procedure TForm1.btnTIAba2EnviarDataHoraClick(Sender: TObject);
var
    resultado: PINT;
    auxData: STRING;

    ano: WORD;
    mes: WORD;
    dia: WORD;
    hora: WORD;
    minuto: WORD;
    segundo: WORD;
    msegundo: WORD;

    bAno: Array[word] of INTEGER;
    bMes: Array[word] of BYTE;
    bDia: Array[word] of BYTE;
    bHora: Array[word] of BYTE;
    bMinuto: Array[word] of BYTE;
    bSegundo: Array[word] of BYTE;
    diaSemana: Array[word] of BYTE;

begin
    New(resultado);
    //Fragmanta a data  em ano, mes e dia
    DecodeDate(dtTIAba2DataREP.Date, ano, mes, dia);
    //Para enviar a função o word é convertido para string
    auxData:= IntToStr (ano);
    //O ano esta com 4 posições mas precisamos apenas das 2 ultimas
    auxData:= auxData[3] + auxData[4];
    //converte o valor da auxData de string para o vetor de inteiros.
    bAno[0]:= StrToInt(auxData);

    //armazena o mês.
    bMes[0]:= mes;
    //armazena o dia.
    bDia[0]:= dia;

    //Fragmanta a hora em hora, minuto, segundo e msegundo.
    DecodeTime(dtTIAba2HoraREP.Time, hora, minuto, segundo, msegundo);
    //Recebe a hora
    bHora[0]:= hora;
    //Recebe o minuto
    bMinuto[0]:= minuto;
    //Recebe o segundo
    bSegundo[0]:= segundo;
    //Recebe o Dia da Semana
    diaSemana[0]:= DayOfWeek(Date);
   
    resultado^:= 0;
    //Conecta o REP.
    REP_Conexao_Simples(ip, 1, resultado);

    if ( resultado^ <> 0)  then
    begin
        //Desconecta caso não encontre o terminal
        REP_Conexao_Simples(ip, 0, resultado);
        Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
    end
    else
    begin
        //Envia as informações de Data e Hora para o Terminal.
        REP_Tempo(ip, 1, bAno[0], bMes[0], bDia[0], bHora[0], bMinuto[0], bSegundo[0], diaSemana[0], resultado);

        if( resultado^ = 0 )then
             Application.MessageBox( 'Configuração enviada com sucesso!','', mb_TaskModal + MB_ICONINFORMATION)
        else
             Application.MessageBox( 'Falha ao enviar configuração!','', mb_TaskModal + MB_ICONWARNING);
        //Desconecta Terminal
        REP_Conexao_Simples(ip, 0, resultado);
    end;
    Dispose(resultado);
end;

procedure TForm1.btnTIAba2ReceberEspacoMemoriaClick(Sender: TObject);
var
    resultado: PINT;
    onOff : byte;
    memUsada: PINT;
    memLivre: PINT;

begin
    New(resultado);
    New(memUsada);
    New(memLivre);

    resultado^:= 0;
    memUsada^:= 0;
    memLivre^ := 0;

    REP_Conexao_Simples(ip, 1, resultado);

    if ( resultado^ <> 0)  then
    begin
        REP_Conexao_Simples(ip, 0, resultado);
        Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
    end
    else
    begin
        //Le o espaço da memoria no REP
        REP_LeEspacoMemoria(ip, memUsada, memLivre, resultado);

        txtTIAba2MemoriaUsada.Text := IntToStr(memUsada^);
        txtTIAba2MemoriaLivre.Text := IntToStr(memLivre^);

        // se não recebeu informa
        if ((memUsada^ = 0) and (memLivre^ = 0)) then
            Application.MessageBox( 'Não foi possivel receber os dados de memória.','', mb_TaskModal + mb_IconWarning);

        //Fecha a conexão com o REP
        REP_Conexao_Simples(ip, 0, resultado);
     end;
     Dispose(resultado);
     Dispose(memUsada);
     Dispose(memLivre);
end;

procedure TForm1.btnTIAba2ReceberTempClick(Sender: TObject);
var
    resultado: PINT;
    porta: Integer;
    senha : PBYTE;
    onOff : BYTE;
    temperatura: PBYTE;

begin
    New(resultado);
    New(temperatura);

    resultado^:= 0;

    REP_Conexao_Simples(ip, 1, resultado);
    if ( resultado^ <> 0)  then
    begin
        REP_Conexao_Simples(ip, 0, resultado);
        Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
    end
    else
        //Le a temperatura do REP
        REP_LeTemperatura(ip , temperatura, resultado);

        txtTIAba2Temperatura.Text := IntToStr(temperatura^);

        // se não recebeu informa
        if(temperatura^ = 0) then
            Application.MessageBox( 'Não foi possivel receber a temperatura do terminal.','', mb_TaskModal + mb_IconWarning);

    //Fecha a conexão com o REP
    REP_Conexao_Simples(ip, 0, resultado);

      Dispose(resultado);
      Dispose(temperatura);
end;

procedure TForm1.btnTIAba2ReceberImpressClick(Sender: TObject);
var
    resultado: PINT;
    onOff : BYTE;
    statusImpressora: Array [0..34] of CHAR;

begin
    New(resultado);

    resultado^:= 0;

    REP_Conexao_Simples(ip, 1, resultado);
    if ( resultado^ <> 0)  then
    begin
        REP_Conexao_Simples(ip, 0, resultado);
        Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
    end
    else
    begin
        //Le a temperatura do REP
        REP_StatusImpressora(ip , statusImpressora, resultado);

        txtTIAba2Impressora.Text := statusImpressora;

        // se não recebeu informa
        if(txtTIAba2Impressora.Text = '') then
            Application.MessageBox( 'Não foi possivel receber o status do terminal.','', mb_TaskModal + mb_IconWarning);

    end;
    //Fecha a conexão com o REP.
    REP_Conexao_Simples(ip, 0, resultado);
    Dispose(resultado);
    //Dispose(statusImpressora);
end;

procedure TForm1.btnTIAba2ReceberRegistroClick(Sender: TObject);
var
    resultado: PINT;
    onOff : BYTE;
    nsrInicial: STRING;
    nsrFinal: STRING;
    path: STRING;
    arquivo: TextFile;
    encontrados: PINT;

begin
    New(resultado);
    New(encontrados);

    resultado^:= 0;
    encontrados^:=0;

    path:= ExpandFileName(txtTIAba2NomeArquivo.Text);
    //Recebe as informações que serão filtradas
    nsrInicial := Form1.txtTIAba2RegistroInicial.Text;
    nsrFinal:= Form1.txtTIAba2RegistroFinal.Text;
    Try
        //Abre a conexão.
        REP_Conexao_Simples(ip, 1, resultado);
        if ( resultado^ <> 0)  then
        begin
            //Fecha a conexão caso não encontre nenhum REP
            REP_Conexao_Simples(ip, 0, resultado);
            Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
        end
        else
        begin
            Label13.Caption := 'Filtrando registro, por favor aguarde...';
            Label13.Update();

            //Le a temperatura do REP
            REP_LeIntervaloNSR(ip, nsrInicial, nsrFinal, path, encontrados, resultado);
            if(resultado^ = 0)then
            begin
                //Indica o caminho do arquivo coletado
                AssignFile(arquivo, path);
                //Abre o arquivo para a escrita
                Reset(arquivo);
                //Fecha o arquivo.
                CloseFile(arquivo);
                //Fecha a conexão com o REP.
                REP_Conexao_Simples(ip, 0, resultado);
                //Informa que os arquivos foram coletados
                Application.MessageBox( 'Arquivo salvo com sucesso!','', 65);
                //Limpa os campos
                txtTIAba2RegistroInicial.Text := '';
                txtTIAba2RegistroFinal.Text := '';
                txtTIAba2NomeArquivo.Text := '';

                Label13.Caption := '';
                Label13.Update();
            end
            else
            begin
                //Fecha a conexão com o REP.
                REP_Conexao_Simples(ip, 0, resultado);
                //Informa que ocorreu falha
                Application.MessageBox( 'Falha ao filtrar arquivo.','', 65);
                Label13.Caption := '';
                Label13.Update();
            end;
        end;

      Dispose(resultado);
      Dispose(encontrados);
    Except
      //Fecha a conexão com o REP.
      REP_Conexao_Simples(ip, 0, resultado);
      Application.MessageBox( 'Ocorreu um erro na tentativa de coletar os dados.','', 65);
      Label13.Caption := '';
      Label13.Update();
    end;
end;

procedure TForm1.btnRHAba1ConsultarClick(Sender: TObject);
var
    resultado: PINT;
    path: STRING;
    abreArquivo: TextFile;
    linha: Array[WORD] of String;
    encontrados: PINT;
    i: Integer;

begin
    New(resultado);
    New(encontrados);

    path:= ExpandFileName('empresa.txt');

    resultado^:= 0;
    encontrados^:= 0;

     TRY
        //Conecta ao REP
        REP_Conexao_Simples(ip, 1, resultado);

        //Se não conectar mostra erro
        if ((resultado^ <> 0) and (resultado^ <> 10005))then
        begin
            REP_Conexao_Simples(ip, 1, resultado);
            Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
        end
        else
            //Recebe todos o cadastro do empregador que esta cadastrado no REP
            REP_LeCadastro(ip, 0, '', path, encontrados, resultado);
            if (resultado^ = 0)then
            begin
            //Le o arquivo e mostra as empresas cadastradas
            //Le o arquivo
             AssignFile(abreArquivo, path);
             Reset(abreArquivo);
            //Le linha a linha
            while not Eof(abreArquivo) do
            begin
                //Recebe a Razao social - 150 caracteres a partir da posição 29
                Read(abreArquivo, linha[29]);
                txtRHAba1RazaoSocial.Text:= Copy(linha[29],29,152);

                //Recebe o identificador de CNPJ ou CPF - 1 caracter a partir da posição 29
                if (Copy(linha[29], 281, 1) = '1') then
                    cbRHAba1Identificador.ItemIndex := 0
                else
                    cbRHAba1Identificador.ItemIndex := 1;

                //Recebe o CNPJ/CPF - 14 caracteres a partir da posição 0
                txtRHAba1CNPJCPF.Text := Copy(linha[29],0,14);

                //Recebe o CEI - 12 caracteres a partir da posição 17
                txtRHAba1CEI.Text := Copy(linha[29], 17,12);

                //Recebe o endereço - 100 caracteres a partir da posição  181
                txtRHAba1Endereco.Text := Copy(linha[29], 181, 100);

                //Desconecta do REP
                REP_Conexao_Simples(ip, 0, resultado);

                end;
                CloseFile(abreArquivo);
            end;
    Dispose(resultado);
    Dispose(encontrados);

    Except
        Application.MessageBox( 'Falha ao consultar o REP','', mb_TaskModal + mb_IconWarning);

        end;
end;





procedure TForm1.btnRHAba1AlterarClick(Sender: TObject);
var
    resultado: PINT;

    sCPFCNPJ: Array [0..16] of BYTE;
    sCEI: Array [0..12] of BYTE;
    bRazaoSocial: Array [0..152] of BYTE;
    bLocal: Array [0..101] of BYTE;

    cTipo: CHAR;
    novoCNPJ_CPF: String;
    onOff : BYTE;
    teste: string;

begin
    New(resultado);
    Try

        novoCNPJ_CPF := Replace(txtRHAba1CNPJCPF.Text, '.');
        novoCNPJ_CPF := Replace(novoCNPJ_CPF, '-');
        novoCNPJ_CPF := Replace(novoCNPJ_CPF, '/');

        // Verifica se foi selecionado CPF ou CNPJ
        case cbRHAba1Identificador.ItemIndex of

            // Valida o CNPJ
            0:
            begin
                if ((ValidaCNPJ(txtRHAba1CNPJCPF.Text)) = False) then
                    if ((ValidaCPF(txtRHAba1CNPJCPF.Text)) = True) then
                        cbRHAba1Identificador.ItemIndex := 1
                    else
                        Application.MessageBox('CNPJ Inválido.', 'Cadastrar Empresa', 64);
            end;

            // Valida o CPF
            1:
            begin
                if((ValidaCPF(txtRHAba1CNPJCPF.Text)) = False) then
                    if ((ValidaCNPJ(txtRHAba1CNPJCPF.Text)) = True) then
                        cbRHAba1Identificador.ItemIndex := 0
                    else
                        Application.MessageBox('CPF Inválido.', 'Cadastrar Empresa', 64)
            end;

            else
                //cbRHAba1Identificador.ItemIndex := 0;
        end;

        // Verifica se todos os campos foram preenchidos
        if (txtRHAba1RazaoSocial.Text = '') then
        begin
            Application.MessageBox('Preencha o campo referente a Razão Social', 'Cadastrar Empresa', 64);
            txtRHAba1RazaoSocial.SetFocus();
            Exit;
        end
        else if (txtRHAba1Endereco.Text = '') then
        begin
            Application.MessageBox('Preencha o campo referente ao Endereço', 'Cadastrar Empresa', 64);
            txtRHAba1Endereco.SetFocus();
            Exit;
        end;

        // Recebe as informações para cadastrar
        // CNPJ/CPF - Deverá haver 14 posições
        if (cbRHAba1Identificador.ItemIndex = 1) then
            cTipo := '1'
        else
            cTipo := '2';

        // CPF e CNPJ - Deverá haver 12 posições
        StrPCopy(@sCPFCNPJ, txtRHAba1CNPJCPF.Text);

        // CEI - Deverá haver 12 posições
        StrPCopy(@sCEI, txtRHAba1CEI.Text);

        // Razão Social - Deverá haver 150 posições
        StrPCopy(@bRazaoSocial, txtRHAba1RazaoSocial.Text);

        // Endereço - Deverá haver 100 posições
        StrPCopy(@bLocal, txtRHAba1Endereco.Text);

        resultado^:= 0;

        REP_Conexao_Simples(ip, 1, resultado);
        if((resultado^ <> 0) and (resultado^ <> 10005))then
        begin
            REP_Conexao_Simples(ip, 0, resultado);
            Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
        end
        else
            REP_GravaCadastroEmpregador(ip, '1', cTipo, sCPFCNPJ[0], sCEI[0], bRazaoSocial[0], bLocal[0], resultado);

            teste:= IntToStr(resultado^);

            if (resultado^ = 0)then
            begin
                txtRHAba1RazaoSocial.Text := '';
                cbRHAba1Identificador.Text := '';
                txtRHAba1CNPJCPF.Text := '';
                txtRHAba1CEI.Text := '';
                txtRHAba1Endereco.Text := '';

                REP_Conexao_Simples(ip, 0, resultado);
                Application.MessageBox( 'Cadastro efetuado com sucesso!', '', 65);
            end
            else
            begin
                    REP_Conexao_Simples(ip, 0, resultado);
                    Application.MessageBox( 'Não foi possivel efetuar o cadastrado..','', mb_TaskModal + mb_IconWarning);
            end;
               Dispose(resultado);
    Except
        Application.MessageBox('ERRO.','Cadastrar Empresa',48);
    end;

end;



procedure TForm1.btnRHAba2CadastrarClick(Sender: TObject);
var
    resultado: PINT;
    nome: Array[0..52] of BYTE;
    nPis: Array[0..16] of BYTE;
    contactless: Array[0..16] of BYTE;
    codBarras: Array[0..16] of BYTE;
    teclado: Array[0..8] of BYTE;
    biometria: Array[0..8] of BYTE;
    grupo: BYTE;
    teste: String;

begin
    New(resultado);
    Try
        //Verifica se o campo referente ao nome está preenchido
        if (txtRHAba2Nome.Text = '') then
        begin
            Application.MessageBox( 'Favor preencher com o nome do funcionário.', '', 65);
            Form1.txtRHAba2Nome.SetFocus();
        end;
        //Verifica se o Pis é válido
        if (ValidaPIS(txtRHAba2PIS.Text) = False) then
        begin
            Application.MessageBox( 'O numero do PIS está incorreto.', '', 65);
            Form1.txtRHAba2PIS.SetFocus();
        end;
        //Nome do funcionario
        StrPCopy(@nome, txtRHAba2Nome.Text);
        //PIS
        StrPCopy(@nPis, txtRHAba2PIS.Text);
        //Contactless
        if (txtRHAba2RFID.Text <> '')then
            StrPCopy(@contactless, txtRHAba2RFID.Text)
        else
            StrPCopy(@contactless, '0');
         //Codigo de barras
        if (txtRHAba2CodigoBarras.Text <> '')then
            StrPCopy(@codBarras, txtRHAba2CodigoBarras.Text)
        else
            StrPCopy(@codBarras, '0');
        //Teclado
        if (txtRHAba2Teclado.Text <> '')then
            StrPCopy(@teclado, txtRHAba2Teclado.Text)
        else
            StrPCopy(@teclado, '0');
        //Biometria
        StrPCopy(@biometria, '0'); //48 o que equivale a 0 na tabela ASCII
                //Grupo
        if (txtRHAba2Grupo.Text = '') then
            StrPCopy(@grupo, '0')
        else
        begin
            if (StrToInt(txtRHAba2Grupo.Text) > 256)then
            begin
                Application.MessageBox( 'Valor do grupo deve ser inferior a 256', '', 65);
                txtRHAba2Grupo.SetFocus();
            end;
        end;
        resultado^:=0;

        REP_Conexao_Simples(ip, 1, resultado);
        if (resultado^ <> 0)then
        begin
             REP_Conexao_Simples(ip, 0, resultado);
             Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
        end
        else
        begin
             REP_GravaCadastroFuncionario(ip, '0', nome[0], nPis[0], contactless[0], codBarras[0], teclado[0], biometria[0], grupo, resultado);
             teste:= IntToStr(resultado^);
            //Se não ocorreu erro mostra a mensagem de cadastrado e limpa os campos
            if (resultado^ = 0)then
            begin
                txtRHAba2Nome.Text := '';
                txtRHAba2PIS.Text := '';
                txtRHAba2Grupo.Text := '';
                txtRHAba2CodigoBarras.Text := '';
                txtRHAba2Teclado.Text := '';
                txtRHAba2RFID.Text := '';
                REP_Conexao_Simples(ip, 0, resultado);

                Application.MessageBox( 'Funcionário cadastrado com sucesso!', '', 65);
                txtRHAba2Nome.SetFocus();
            end
            //Se ocorreu falha mostra a mensagem e erro
            else
            begin
                REP_Conexao_Simples(ip, 0, resultado);
                Application.MessageBox( 'Falha ao cadastrar funcionário', '', 65);
            end;
        end;
            Dispose(resultado);
    Except
        Application.MessageBox( 'Falha ao cadastrar funcionário', '', 65);
    end;
end;

procedure TForm1.btnRHAba2ExcluirClick(Sender: TObject);
var
    resultado: PINT;
    nPIS: Array[0..16] of BYTE;
    teste: String;
begin
    New(resultado);
    Try
        if (Application.MessageBox( 'Realmente deseja apagar este registro?','Confirmação',  MB_YESNO + MB_ICONQUESTION) = 6) then
        begin
            StrPCopy(@nPIS, txtRHAba2PIS.Text);

            //Recebe o IP cadastrado
            resultado^:= 0;

             //Se conecta ao REP
             REP_Conexao_Simples(ip, 1, resultado);

             //Caso ocorra o resultado 10005 significa que o REP jÃ¡ esta conectado
             if ((resultado^ <> 0) and (resultado^ <> 10005))then
             begin
                 REP_Conexao_Simples(ip, 0, resultado);
                 Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
             end
             else
                 //"Exclui" o cadastro do equipamento
                 REP_ExcluiCadastroFuncionario (ip, nPIS[0], resultado);

                 if (resultado^ = 0)then
                 begin
                     txtRHAba2Nome.Text := '';
                     txtRHAba2PIS.Text := '';
                     txtRHAba2Grupo.Text := '';
                     txtRHAba2CodigoBarras.Text := '';
                     txtRHAba2Teclado.Text := '';
                     txtRHAba2RFID.Text := '';
                     REP_Conexao_Simples(ip, 0, resultado);

                     Application.MessageBox( 'Registro removido com exito.', '', 65);
                 end
                 else
                 begin
                     REP_Conexao_Simples(ip, 0, resultado);
                     Application.MessageBox( 'Falha na remoção do registro.', '', 65);
                 end;
             end;
                 Dispose(resultado);
             Except
        end;

end;

procedure TForm1.btnRHAba2AlterarClick(Sender: TObject);
var
    resultado: PINT;
    nome: Array[0..52] of BYTE;
    nPis: Array[0..16] of BYTE;
    contactless: Array[0..16] of BYTE;
    codBarras: Array[0..16] of BYTE;
    teclado: Array[0..8] of BYTE;
    biometria: Array[0..8] of BYTE;
    grupo: BYTE;
    teste: String;

begin
    New(resultado);
    Try
        //Verifica se o campo referente ao nome está preenchido
        if (txtRHAba2Nome.Text = '') then
        begin
            Application.MessageBox( 'Favor preencher com o nome do funcionário.', '', 65);
            Form1.txtRHAba2Nome.SetFocus();
        end;
        //Verifica se o Pis é válido
        if (ValidaPIS(txtRHAba2PIS.Text) = False) then
        begin
            Application.MessageBox( 'O numero do PIS está incorreto.', '', 65);
            Form1.txtRHAba2PIS.SetFocus();
        end;
        //Nome do funcionario
        StrPCopy(@nome, txtRHAba2Nome.Text);
        //PIS
        StrPCopy(@nPis, txtRHAba2PIS.Text);
        //Contactless
        if (txtRHAba2RFID.Text <> '')then
            StrPCopy(@contactless, txtRHAba2RFID.Text)
        else
            StrPCopy(@contactless, '0');
         //Codigo de barras
        if (txtRHAba2CodigoBarras.Text <> '')then
            StrPCopy(@codBarras, txtRHAba2CodigoBarras.Text)
        else
            StrPCopy(@codBarras, '0');
        //Teclado
        if (txtRHAba2Teclado.Text <> '')then
            StrPCopy(@teclado, txtRHAba2Teclado.Text)
        else
            StrPCopy(@teclado, '0');
        //Biometria
        StrPCopy(@biometria, '0'); //48 o que equivale a 0 na tabela ASCII
                //Grupo
        if (txtRHAba2Grupo.Text = '') then
            StrPCopy(@grupo, '0')
        else
        begin
            if (StrToInt(txtRHAba2Grupo.Text) > 256)then
            begin
                Application.MessageBox( 'Valor do grupo deve ser inferior a 256', '', 65);
                txtRHAba2Grupo.SetFocus();
            end;
        end;
        resultado^:=0;

        //Recebe o IP cadastrado no banco de dados
        REP_Conexao_Simples(ip, 1, resultado);

        if (resultado^ <> 0)then
        begin
             REP_Conexao_Simples(ip, 0, resultado);
             Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
        end
        else
        begin
             REP_GravaCadastroFuncionario(ip, '1', nome[0], nPis[0], contactless[0], codBarras[0], teclado[0], biometria[0], grupo, resultado);
             teste:= IntToStr(resultado^);
            //Se não ocorreu erro mostra a mensagem de cadastrado e limpa os campos
            if (resultado^ = 0)then
            begin
                txtRHAba2Nome.Text := '';
                txtRHAba2PIS.Text := '';
                txtRHAba2Grupo.Text := '';
                txtRHAba2CodigoBarras.Text := '';
                txtRHAba2Teclado.Text := '';
                txtRHAba2RFID.Text := '';
                REP_Conexao_Simples(ip, 0, resultado);

                Application.MessageBox( 'Funcionário cadastrado com sucesso!', '', 65);
                txtRHAba2Nome.SetFocus();
            end
            //Se ocorreu falha mostra a mensagem e erro
            else
            begin
                REP_Conexao_Simples(ip, 0, resultado);
                Application.MessageBox( 'Falha ao cadastrar funcionário', '', 65);
            end;
        end;
            Dispose(resultado);
    Except
        Application.MessageBox( 'Falha ao cadastrar funcionário', '', 65);
    end;
end;

procedure TForm1.cbRHAba2REPChange(Sender: TObject);
begin
    if (cbRHAba2REP.Text <> '') then
    begin
        if ((txtRHAba2Nome.Text <> '') and (txtRHAba2PIS.Text <> '') and (txtRHAba2Grupo.Text <> '') and
                (txtRHAba2Teclado.Text <> '') and (txtRHAba2RFID.Text <> '')) then
         begin
            btnRHAba2Cadastrar.Enabled := True;
            btnRHAba2Alterar.Enabled := True;
            btnRHAba2Excluir.Enabled := True;
         end
        else
        begin
            btnRHAba2Excluir.Enabled := True;
            btnRHAba2Cadastrar.Enabled := False;
            btnRHAba2Alterar.Enabled := False;
        end
    end;
end;

procedure TForm1.cbTIAba2REPChange(Sender: TObject);
begin
    if (cbTIAba2REP.Text <> '')then
    begin
        btnTIAba2EnviarDataHora.Enabled := true;
        btnTIAba2ReceberDataHora.Enabled := true;
        btnTIAba2ReceberEspacoMemoria.Enabled := true;
        btnTIAba2ReceberImpress.Enabled := true;
        btnTIAba2ReceberTemp.Enabled := true;
        btnTIAba2ReceberRegistro.Enabled := true;
    end;
end;

procedure TForm1.btnRHAba2ReceberClick(Sender: TObject);
var
    resultado: PINT;
    encontrados: PINT;
    path: STRING;

begin
    New(resultado);
    New(encontrados);

    Try
        //Caminho onde será salvo o arquivo.
        path := txtRHAba2Caminho.Text + '\funcionario.txt';
        resultado^:=0;
        //Conecta ao REP.
        REP_Conexao_Simples(ip, 1, resultado);
        //Se não conectar mostra erro.
        if(resultado^ <> 0)then
        begin
            //Desconecta ao REP.
            REP_Conexao_Simples(ip, 0, resultado);
            Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
        end
        else
        begin
            Label13.Caption := 'Recebendo cadastros, por favor aguarde.';
            Label13.Update();
            
            REP_LeCadastro(ip, 4, '', path, encontrados, resultado);
            REP_Conexao_Simples(ip, 0, resultado);
            //Se recebido com sucesso mostra a mensagem de quantos funcionáios foram encontrados.
            Label13.Caption := 'Recebido ' + IntToStr(encontrados^) + ' funcionários com sucesso!';
            Label13.Update();

            if(resultado^ = 0)then
            begin
                Application.MessageBox( 'Recebido com sucesso!','', mb_TaskModal + MB_ICONINFORMATION);
                Label13.Caption := '';
                Label13.Update();
            end
            else
                Application.MessageBox( 'Não foi possivel receber os funcionários.','', mb_TaskModal + mb_IconWarning);
        end;
        Dispose(resultado);
        Dispose(encontrados);
    Except
        //Mostra o status
        Label13.Caption:= ''; //Texto que será apresentado
        Label13.Update();
        Application.MessageBox( 'Ocorreu uma falha ao receber funcionários.','', mb_TaskModal + MB_ICONEXCLAMATION);
    end;
end;

procedure TForm1.btnRHAba4AbrirClick(Sender: TObject);
begin
    txtRHAba4SalvarEm.text:= BrowseDialog('Selecione a pasta.',BIF_RETURNONLYFSDIRS);
end;

procedure TForm1.btnRHAba3ColetarClick(Sender: TObject);
var
    resultado: PINT;
    encontrados: PINT;
    path: STRING;
    pathReg: STRING;
    abreArquivo: TextFile;
    escreveArquivoTxt: TextFile;
    linha: STRING;
    linhaReg: TStringArray;

begin
    New(resultado);
    New(encontrados);

    Try
        path := '\regREP.txt';
        resultado^:=0;

        REP_Conexao_Simples(ip, 1, resultado);
        if(resultado^ <> 0)then
        begin
            REP_Conexao_Simples(ip, 0, resultado);
            Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
        end
        else
        begin
            Label13.Caption := 'Coletando registros, por favor aguarde.';
            Label13.Update();

            REP_LeCadastro(ip, 5, '1', path, encontrados, resultado);
            if(resultado^ = 0)then
            begin
                linha := '';

                pathReg := txtRHAba4SalvarEm.Text + '\registro.txt';
                //Le o arquivo e mostra as empresas cadastradas
                //Le o arquivo
                AssignFile(abreArquivo, path);
                Reset(abreArquivo);
                if not FileExists(pathReg) then
                begin
                    AssignFile(escreveArquivoTxt, pathReg);
                    Rewrite(escreveArquivoTxt);
                end
                else
                begin
                    AssignFile(escreveArquivoTxt, pathReg);
                    Append(escreveArquivoTxt);
                end;
                //Le linha a linha
                while not Eof(abreArquivo) do
                begin
                    Readln(abreArquivo, linha);
                    linhaReg :=  Split(linha, ';');
                    if(linhaReg[1] = '3')then
                        Writeln(escreveArquivoTxt, linha);
                 end;
                 //Fecha o arquivo da leitura.
                 CloseFile(abreArquivo);
                 //Fecha o arquivo da escrita para salvar as informações coletadas.
                 CloseFile(escreveArquivoTxt);
                 //Remove o arquivo coletado, pois nao serÃ¡ mais utilizado
                 DeleteFile(path);
                 //Fecha a conexão
                 REP_Conexao_Simples(ip, 0, resultado);

                 Application.MessageBox( 'Recebido com sucesso!','', mb_TaskModal + MB_ICONINFORMATION);
                 Label13.Caption := '';
                 Label13.Update();
            end
            else
            begin
                //Fecha a conexão
                REP_Conexao_Simples(ip, 0, resultado);
                Application.MessageBox( 'Falha ao coletar arquivo.','', mb_TaskModal + mb_IconWarning);
                Label13.Caption:= '';
                Label13.Update();
            end;
        end;
        Dispose(resultado);
        Dispose(encontrados);
    Except
        //Fecha a conexão
        REP_Conexao_Simples(ip, 0, resultado);
        Application.MessageBox( 'Ocorreu uma falha ao receber funcionários.','', mb_TaskModal + MB_ICONEXCLAMATION);
        Label13.Caption:= '';
        Label13.Update();
    end;

end;

procedure TForm1.btnRHAba2AbrirBioClick(Sender: TObject);
begin
    if OpenDialog1.Execute then // verificamos se o OpenDialog foi executado (execute)
    txtRHAba2Arquivo2.Text := OpenDialog1.FileName;
   // txtRHAba2Arquivo2.text:= BrowseDialog('Selecione a pasta.',BIF_RETURNONLYFSDIRS);
end;

procedure TForm1.btnRHAba2EnviarBioClick(Sender: TObject);
begin
    Try
      GravarFingerPasta(ip, txtRHAba2Arquivo2.Text);
    Except
      Application.MessageBox( 'Ocorreu uma falha ao enviar as biometrias.','', mb_TaskModal + MB_ICONEXCLAMATION);
    end;
end;

procedure TForm1.btnRHAba2FormataBioClick(Sender: TObject);
var
    resultado: PINT;
begin
    New(resultado);

    TRY
        resultado^:= 0;
        //Conecta o REP.
        REP_Conexao_Simples(ip, 1, resultado);

        //Desabilita os botões enquando formata os arquivos das fingers.
        btnRHAba2FormataBio.Enabled := false;

        //Se não conectar mostra erro
        if (resultado^ <> 0) then
        begin
            //Desconecta no REP
            REP_Conexao_Simples(ip, 0, resultado);
            Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
            btnRHAba2FormataBio.Enabled := false;
        end
        else
        begin
            Label13.Visible := true;
            Label13.Font.Color := clWindowText;
            //Iniciar a Thread
            Label13.Caption := 'Formatando Biometrias, por favor aguarde... '; //Texto que será apresentado

            REP_FormataFinger(ip, resultado);
            //Desconecta no REP
           REP_Conexao_Simples(ip, 0, resultado);;

            btnRHAba2EnviarBio.Enabled := true;
            btnRHAba2ReceberBio.Enabled := true;
            btnRHAba2FormataBio.Enabled := true;

            Label13.Visible := true;
            //Mostra o status
            Label13.Caption := ''; //Texto que será apresentado
            Label13.Update();
        end;
        if (resultado^ = 0)then
            Application.MessageBox( 'Biometrias formatadas com sucesso','', mb_TaskModal + MB_ICONINFORMATION )
        else
            Application.MessageBox('Não foi possivel formatar as biometrias cadastradas. ', '', mb_TaskModal + MB_ICONEXCLAMATION);

        Dispose(resultado);
    Except
        Label13.Caption := ''; //Texto que será apresentado

        Application.MessageBox( 'Ocorreu uma falha ao enviar as biometrias.','', mb_TaskModal + MB_ICONEXCLAMATION);
        btnRHAba2EnviarBio.Enabled := true;
        btnRHAba2ReceberBio.Enabled := true;
        btnRHAba2FormataBio.Enabled := true;
    end;
end;

procedure TForm1.btnRHAba2ExcluirBioClick(Sender: TObject);
var
    resultado: PINT;
    nPIS: Array[0..16] of BYTE;
begin
    New(resultado);
    TRY
        resultado^:= 0;

        StrPCopy(@nPIS, txtRHAba2PIS3.Text);
        //Conecta o REP.
        REP_Conexao_Simples(ip, 1, resultado);
        //Se não conectar mostra erro
        if (resultado^ <> 0) then
        begin
            //Desconecta no REP
            REP_Conexao_Simples(ip, 0, resultado);
            Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
            btnRHAba2EnviarBio.Enabled := true;
            btnRHAba2ReceberBio.Enabled := true;
            btnRHAba2FormataBio.Enabled := true;
            btnRHAba2ExcluirBio.Enabled := true;
        end
        else
        begin
            Label13.Visible := true;
            Label13.Font.Color := clWindowText;
            //Iniciar a Thread
            Label13.Caption := 'Excluindo cadastro, por favor aguarde '; //Texto que será apresentado

            //Se recebido com sucesso mostra a mensagem de quantos funcionÃ¡rios foram encontrados e habilita os botÃµes
            REP_RemoverFingerID(ip, nPIS[0], resultado);
            //Desconecta no REP
            REP_Conexao_Simples(ip, 0, resultado);

            btnRHAba2EnviarBio.Enabled := true;
            btnRHAba2ReceberBio.Enabled := true;
            btnRHAba2ExcluirBio.Enabled := true;
            btnRHAba2FormataBio.Enabled := true;

            Label13.Visible := true;
            //Mostra o status
            Label13.Caption := ''; //Texto que será apresentado
            Label13.Update();
            txtRHAba2PIS3.Clear();

            if (resultado^= 0)then
                Application.MessageBox('Excluído com sucesso!', '', mb_TaskModal + MB_ICONINFORMATION)
            //Se não mostra erro
            else
                Application.MessageBox('Não foi possivel excluir a biometria cadastrada. ' , '', mb_TaskModal + MB_ICONEXCLAMATION)
        end;
    Except
        Label13.Caption := ''; //Texto que será apresentado

        Application.MessageBox('Ocorreu uma falha ao formatar as biometrias.', '', mb_TaskModal + MB_ICONEXCLAMATION);
        btnRHAba2EnviarBio.Enabled := true;
        btnRHAba2ReceberBio.Enabled := true;
        btnRHAba2FormataBio.Enabled := true;
        btnRHAba2ExcluirBio.Enabled := true;
    end;
end;

procedure TForm1.btnRHAba2SalvarBioClick(Sender: TObject);
var
    resultado: PINT;
    senha: PBYTE;
    porta: INTEGER;
    nPIS: Array[0..16] of BYTE;
    pastaDestino: String;
    pastaOrigem: String;
begin
    New(resultado);
    New(senha);
    pastaDestino := ExtractFilePath(ParamStr(0)) + 'Biometria';
    pastaOrigem:= ExtractFilePath(ParamStr(0)) + txtRHAba2PIS3.Text + '.rec';
    //CreateDir(pasta);
    TRY
        resultado^:= 0;

        StrPCopy(@nPIS, txtRHAba2PIS3.Text);
        //Conecta o REP.
        REP_Conexao_Simples(ip, 1, resultado);
        //Se não conectar mostra erro
        if (resultado^ <> 0) then
        begin
            //Desconecta no REP
            REP_Conexao_Simples(ip, 0, resultado);
            Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
            btnRHAba2EnviarBio.Enabled := true;
            btnRHAba2ReceberBio.Enabled := true;
            btnRHAba2FormataBio.Enabled := true;
            btnRHAba2ExcluirBio.Enabled := true;
        end
        else
        begin
            Label13.Visible := true;
            Label13.Font.Color := clWindowText;
            //Iniciar a Thread
            Label13.Caption := 'Recebendo biometria, por favor aguarde '; //Texto que será apresentado

            //Se recebido com sucesso mostra a mensagem de quantos funcionÃ¡rios foram encontrados e habilita os botÃµes
            REP_LeFingerID(ip, nPIS[0], resultado);
            //Move o arquivo .rec que recebe na pasta do executavel para a pasta Biometrias.
            MoveFile(PCHAR(pastaOrigem), PCHAR(pastaDestino + '\' + ExtractFileName(pastaOrigem)));
            //Desconecta no REP
            REP_Conexao_Simples(ip, 0, resultado);

            btnRHAba2EnviarBio.Enabled := true;
            btnRHAba2ReceberBio.Enabled := true;
            btnRHAba2ExcluirBio.Enabled := true;
            btnRHAba2FormataBio.Enabled := true;

            Label13.Visible := true;
            //Mostra o status
            Label13.Caption := ''; //Texto que será apresentado
            Label13.Update();
            txtRHAba2PIS3.Clear();

            if (resultado^= 0)then
                Application.MessageBox('Biometria salva com sucesso!', '', mb_TaskModal + MB_ICONINFORMATION)
            //Se não mostra erro
            else
                Application.MessageBox('Não foi possivel salvar a biometria cadastrada. ' , '', mb_TaskModal + MB_ICONEXCLAMATION)
        end;
    Except
        Label13.Caption := ''; //Texto que será apresentado

        Application.MessageBox('Ocorreu uma falha ao salvar a biometria.', '', mb_TaskModal + MB_ICONEXCLAMATION);
        btnRHAba2EnviarBio.Enabled := true;
        btnRHAba2ReceberBio.Enabled := true;
        btnRHAba2FormataBio.Enabled := true;
        btnRHAba2ExcluirBio.Enabled := true;
    end;
end;

procedure TForm1.btnRHAba2ReceberBioClick(Sender: TObject);
var
    resultado: PINT;
    encontrados: PBYTE;
    pastaOrigem, pastaDestino: string;
    teste: STRING;
    I : INTEGER;
    SR: TSearchRec;
begin
    New(resultado);
    New(encontrados);

    pastaOrigem:= ExtractFilePath (ParamStr(0)) + '\*.rec';

    TRY
        resultado^:= 0;
        //Conecta o REP.
        REP_Conexao_Simples(ip, 1, resultado);
        //Se não conectar mostra erro
        if (resultado^ <> 0) then
        begin
            //Desconecta no REP
            REP_Conexao_Simples(ip, 0, resultado);
            Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
            btnRHAba2EnviarBio.Enabled := true;
            btnRHAba2ReceberBio.Enabled := true;
            btnRHAba2FormataBio.Enabled := true;
            btnRHAba2ExcluirBio.Enabled := true;
        end
        else
        begin
            Label13.Visible := true;
            Label13.Font.Color := clWindowText;
            //Iniciar a Thread
            Label13.Caption := 'Recebendo biometria, por favor aguarde '; //Texto que será apresentado
            //for i:=0 to Length(encontrados) do
            //begin

                //Se recebido com sucesso mostra a mensagem de quantos funcionÃ¡rios foram encontrados e habilita os botÃµes
            REP_LeFinger(ip, encontrados, resultado);
            I := FindFirst(pastaOrigem, faAnyFile, SR);
            while I = 0 do
            begin
                if (SR.Attr and faDirectory) <> faDirectory then
                begin
                    pastaOrigem := ExtractFilePath(ParamStr(0)) +  SR.Name;
                    pastaDestino:= ExtractFilePath (ParamStr(0)) + '\Biometria\' + SR.Name;
                    //Move o arquivo .rec que recebe na pasta do executavel para a pasta Biometrias.
                    if not MoveFile(PChar(pastaOrigem), PChar(pastaDestino)) then
                        ShowMessage('Erro ao copiar ' + pastaOrigem + ' para ' + pastaDestino);
                end;
                I := FindNext(SR);
            end;

            //Desconecta no REP
            REP_Conexao_Simples(ip, 0, resultado);

            btnRHAba2EnviarBio.Enabled := true;
            btnRHAba2ReceberBio.Enabled := true;
            btnRHAba2ExcluirBio.Enabled := true;
            btnRHAba2FormataBio.Enabled := true;

            Label13.Visible := true;
            //Mostra o status
            Label13.Caption := ''; //Texto que será apresentado
            Label13.Update();
            txtRHAba2PIS3.Clear();

            if (resultado^= 0)then
                Application.MessageBox('Biometria salva com sucesso!', '', mb_TaskModal + MB_ICONINFORMATION)
            //Se não mostra erro
            else
                Application.MessageBox('Não foi possivel salvar a biometria cadastrada. ' , '', mb_TaskModal + MB_ICONEXCLAMATION)
        end;
    Except
        Label13.Caption := ''; //Texto que será apresentado

        Application.MessageBox('Ocorreu uma falha ao salvar a biometria.', '', mb_TaskModal + MB_ICONEXCLAMATION);
        btnRHAba2EnviarBio.Enabled := true;
        btnRHAba2ReceberBio.Enabled := true;
        btnRHAba2FormataBio.Enabled := true;
        btnRHAba2ExcluirBio.Enabled := true;
    end;

end;

procedure TForm1.FormCreate(Sender: TObject);
var
    path: String;
begin
    path:= ExtractFilePath(ParamStr(0)) + 'Biometria\';
    CreateDir(path);
    txtRHAba2Caminho2.Text := path;
end;

procedure TForm1.btnTIAba2ReceberDataHoraClick(Sender: TObject);
var
    resultado: PINT;

    auxData: STRING;
    auxHora: STRING;

    dia: String;
    mes: String;
    ano: String;
    hora: String;
    minuto: String;
    segundo: String;

    bAno: Array[word] of INTEGER;
    bMes: Array[word] of BYTE;
    bDia: Array[word] of BYTE;
    bHora: Array[word] of BYTE;
    bMinuto: Array[word] of BYTE;
    bSegundo: Array[word] of BYTE;
    diaSemana: Array[word] of BYTE;
    
begin
    New(resultado);

    resultado^:= 0;

    //Conecta o REP.
    REP_Conexao_Simples(ip, 1, resultado);

    if ( resultado^ <> 0)  then
    begin
        //Desconecta caso não encontre o terminal
        REP_Conexao_Simples(ip, 0, resultado);
        Application.MessageBox( 'Não foi possivel conectar ao REP','', mb_TaskModal + mb_IconWarning);
    end
    else
    begin
        //Solicita a data e hora para o terminal.
        REP_Tempo(ip, 0, bAno[0], bMes[0], bDia[0], bHora[0], bMinuto[0], bSegundo[0], diaSemana[0], resultado);

        if( resultado^ = 0 )then
        begin
             //Converte para String.
             ano := IntToStr(bAno[0]);
             dia := IntToStr(bDia[0]);
             mes := IntToStr(bMes[0]);
             hora := IntToStr(bHora[0]);
             minuto := IntToStr(bMinuto[0]);
             segundo := IntToStr(bSegundo[0]);

             //Recebe o valor da data formatada.
             auxData := dia + '/' + mes + '/' + ano;
             dtTIAba2DataREP.Date := StrToDate(auxData);

             //Recebe o valor da hora formatada.
             auxHora := hora + ':' + minuto + ':' + segundo;
             dtTIAba2HoraREP.Time := StrToTime(auxHora);

             Application.MessageBox( 'Configuração recebida com sucesso!','', mb_TaskModal + MB_ICONINFORMATION)
        end
        else
             Application.MessageBox( 'Falha ao receber configuração!','', mb_TaskModal + MB_ICONWARNING);
        //Desconecta Terminal
        REP_Conexao_Simples(ip, 0, resultado);
    end;
    Dispose(resultado);
end;

end.
