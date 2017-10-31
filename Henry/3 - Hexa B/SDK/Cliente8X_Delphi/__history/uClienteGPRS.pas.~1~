unit uClienteGPRS;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, Buttons, ExtCtrls, ScktComp, ComCtrls, Math, cUtils,
  cCipher, cCipherRandom, cCipherRSA;
  
type
  TfrmClienteGPRS = class(TForm)
    Panel1: TPanel;
    edtPorta: TEdit;
    edtIP: TEdit;
    Panel2: TPanel;
    mmRecebidos: TMemo;
    stbrStatus: TStatusBar;
    lblRecebidosBytes: TLabel;
    lblRecebidosAscii: TLabel;
    lblIPServidor: TLabel;
    lstAdicionar: TListBox;
    lblComandos: TLabel;
    mmRecebidosAscii: TMemo;
    lblEnviados: TLabel;
    mmEnviados: TMemo;
    btnLimpar: TButton;
    btnLimparEnviados: TButton;
    mmEnviadosString: TMemo;
    lblStringEnviada: TLabel;
    lblPorta: TLabel;
    Panel3: TPanel;
    Panel4: TPanel;
    edtConfirmarEnvio: TEdit;
    lblAEnviar: TLabel;
    btnEnviarPacote: TButton;
    rdbtnReceber: TRadioButton;
    rdbtnEnviar: TRadioButton;
    mmInfo: TMemo;
    lblDescricao: TLabel;
    lblParametro: TLabel;
    btnConectar: TButton;
    rdgrParametroQts: TRadioGroup;
    rdgrParametroReg: TRadioGroup;
    Panel5: TPanel;
    edtMsg: TEdit;
    edtSeg: TEdit;
    chbxRele1: TCheckBox;
    chbxRele3: TCheckBox;
    chbxRele2: TCheckBox;
    lblMensagem: TLabel;
    lblRele1: TLabel;
    lblRele2: TLabel;
    lblRele3: TLabel;
    lblSeg: TLabel;
    lblTempo: TLabel;
    Panel6: TPanel;
    rdbtnLiberado: TRadioButton;
    rdbtnNegado: TRadioButton;
    btnFile: TButton;
    edtFile: TEdit;
    lblDiretorio: TLabel;
    Panel7: TPanel;
    chbxLoopInfinito: TCheckBox;
    btnLoop: TButton;
    edtQtsLoop: TEdit;
    lblLoopInfinito: TLabel;
    Label1: TLabel;
    ChkConexaoSegura: TCheckBox;
    procedure FormActivate(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure lstAdicionarClick(Sender: TObject);
    procedure rdbtnEnviarMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure rdbtnReceberMouseDown(Sender: TObject; Button: TMouseButton;
      Shift: TShiftState; X, Y: Integer);
    procedure rdgrParametroRegClick(Sender: TObject);
    procedure rdgrParametroQtsClick(Sender: TObject);
    procedure btnConectarClick(Sender: TObject);
    procedure btnEnviarPacoteClick(Sender: TObject);
    procedure btnFileClick(Sender: TObject);
    procedure btnLimparClick(Sender: TObject);
    procedure btnLimparEnviadosClick(Sender: TObject);
    procedure btnLoopClick(Sender: TObject);
    procedure edtQtsLoopKeyPress(Sender: TObject; var Key: Char);
    procedure edtSegKeyPress(Sender: TObject; var Key: Char);
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
  TByteArray = array of byte;

  // Procedures e Functions públicos
  function fncLerComando(pTipoPacote : String; pMetodoEnvio : String) : String;
  procedure proEnviarPacote;
  procedure Split(const Delimiter: Char; Input: string; const Strings: TStrings);
  procedure Delay(MSec: Cardinal);

  // Métodos para criptografar e descriptografar os dados
  function GenerateKeyAES(KeySize: Integer): String;
  function EncryptAES(const s_Key: String; s_Data: String): String;
  function DecryptAES(const s_Key: String; s_Data: String): String;
  function EncryptRSA(const s_Modulus, s_Exponent, s_Plain: String): String;

// Constantes globais
const
  AES_KEY_SIZE = 16;
  AES_MODE = cmCBC;

  RSA_KEY_SIZE = 1024;
  RSA_ENCRYPT_SCHEME = rsaetPKCS1;

// Variáveis e classes globais
var
  frmClienteGPRS: TfrmClienteGPRS;
  Cliente : TClienteGPRS;
  _gPacoteResponse : String;
  _gTipoEnvio : String; // Guarda o tipo de envio, é carregado quando o usuário aperta o botão "Enviar".
  _gKeyAES: String;
  _gModulus: String;
  _gExpoent: String;
  _gEnabledAES: Boolean;
  _gUsuario: String;
  _gSenha: String;

implementation

uses ufrmLogin;

{$R *.dfm}

procedure Split(const Delimiter: Char; Input: string; const Strings: TStrings);
begin
   Assert(Assigned(Strings)) ;
   Strings.Clear;
   Strings.Delimiter := Delimiter;
   Strings.DelimitedText := Input;
end;

procedure TfrmClienteGPRS.FormActivate(Sender: TObject);
{ Inicia os parâmetros principais da classe Cliente }
begin
  Cliente := TClienteGPRS.Create;
  Cliente.ClientType := ctNonBlocking;
  Cliente.OnConnect := Cliente.doOnConnect;
  Cliente.OnRead := Cliente.doOnRead;
  edtIP.MaxLength := 15;
  edtIP.SetFocus;
  stbrStatus.SimpleText := 'Nenhuma conexão ativa.';
end;

procedure TfrmClienteGPRS.FormClose(Sender: TObject; var Action: TCloseAction);
{ Finaliza o aplicativo fechando o socket e liberando a memória alocada. }
begin
  Cliente.Active := False;
  Cliente.Free;
end;

procedure TfrmClienteGPRS.lstAdicionarClick(Sender: TObject);
{ Carrega as informações sobre o pacote selecionado na TMemo à direita e, se a
  opção de envio estiver selecionada, carrega o pacote no TEditBox de envio. }
var
  i : Integer;
begin
  for i := lstAdicionar.Items.Count -1 downto 0 do
    if lstAdicionar.Selected[i] then
    begin
      mmInfo.Clear;
      rdgrParametroQts.Visible := False;
      rdgrParametroReg.Visible := False;
      frmClienteGPRS.Panel5.Visible := False;
      lblParametro.Visible := False;
      Panel6.Visible := False;
      edtFile.Visible := False;
      btnFile.Visible := False;
      Panel7.Visible := False;
      lblDiretorio.Visible := False;
      if lstAdicionar.Selected[0] then
      begin
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia configurações ao equipamento.');
        mmInfo.Lines.Add('Exemplo: 01+EC+00+HAB_TECLADO[H] Habilita o teclado.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe configurações.');
        mmInfo.Lines.Add('Exemplo: 01+RC+00+IP Recebe IP do equipamento.');
      end
      else if lstAdicionar.Selected[1] then
      begin
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia um empregador ao equipamento.');
        mmInfo.Lines.Add('Exemplo: 01+EE+00+2]00000000001]]Empresa Teste]P' +
          'inhais Define o empregador como: Empresa Teste e o local como: ' +
          'Pinhais.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe o empregador do equipamento.');
        mmInfo.Lines.Add('Exemplo: 01+RE+00.');
      end
      else if lstAdicionar.Selected[2] then
      begin
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia uma lista de usuários.');
        mmInfo.Lines.Add('Exemplo: 01+EU+00+1+I[123123123123[TESTE[0[1[000023' +
          ' Inclui um usuário.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe uma lista de usuários.');
        mmInfo.Lines.Add('Exemplo: 01+RU+00+3]1 Recebe 3 usuários cadastrados à' +
          ' partir do índice 1.');
      end
      else if lstAdicionar.Selected[3] then
      begin
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia data e hora ao equipamento.');
        mmInfo.Lines.Add('Exemplo: 01+EH+00+'+FormatDateTime('dd/mm/yy hh:mm:ss', Now)+']00/00/00]00/00/00' +
          ' Envia a data e hora atual sem horário de verão.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe data e hora do equipamento.');
        mmInfo.Lines.Add('Exemplo: 01+RH+00');
      end
      else if lstAdicionar.Selected[4] then
      begin
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia uma lista de digitais.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe uma lista de digitais.');
        mmInfo.Lines.Add('Exemplo: 01+RD+00+L]2}0 Recebe uma listagem de 2 usuá' +
          'rios à paritr do índice 0 que possui digitais cadastradas.');
      end
      else if lstAdicionar.Selected[5] then
      begin
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe lista de registros.');
        rdgrParametroReg.Visible := True;
        lblParametro.Visible :=True;
        rdgrParametroReg.ItemIndex := -1;
      end
      else if lstAdicionar.Selected[6] then
      begin
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe quantidade ou status.');
        rdgrParametroQts.Visible := True;
        lblParametro.Visible :=True;
        rdgrParametroQts.ItemIndex := -1;
      end
      else if lstAdicionar.Selected[7] then
      begin
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia uma lista de cartões.');
        mmInfo.Lines.Add('Exemplo: 01+ECAR+00+1+I[23[23[09/07/2012 08:00:01[09/' +
          '07/2012 17:00:01[1[1[0[123[321[[BM[2[1[1[0[[0 Inclui o cartão.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe uma lista de cartões.');
        mmInfo.Lines.Add('Exemplo: 01+RCAR+00+2]0 Recebe 2 cartões à partir do ' +
          'índice 0.');
      end
      else if lstAdicionar.Selected[8] then
      begin
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia uma lista de grupos de acesso.');
        mmInfo.Lines.Add('Exemplo: 01+EGA+00+1+I[000023[Grupo Equipe Suporte[01' +
          '/01/2010 00:00:01[30/12/2012 23:59:59[2[1[1[[[0[[0[[0[[');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe uma lista de grupos de acesso.');
        mmInfo.Lines.Add('Exemplo: 01+RGA+00+2]0 Recebe 2 grupos de acesso à pa' +
          'rtir do índice 0.');
      end
      else if lstAdicionar.Selected[9] then
      begin
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia cartões de grupo de acesso.');
        mmInfo.Lines.Add('Exemplo: 01+ECGA+00+1+I[000023[1');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe cartões de grupo de acesso.');
        mmInfo.Lines.Add('Exemplo: 01+RCGA+00+2]0 Recebe 2 cartões de grupos de' +
          ' acesso à partir do índice 0.');
      end
      else if lstAdicionar.Selected[10] then
      begin
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia uma lista de acionamentos.');
        mmInfo.Lines.Add('Exemplo: 01+EACI+00+1+I[13[Sirene Almoço[12:00:00[1[5' +
          '[23456 Inclui o acionamento de 12:00:00 do rele 1 por 5 segundos de ' +
          'segunda à sexta.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe uma lista de acionamentos.');
        mmInfo.Lines.Add('Exemplo: 01+RACI+00+2]0 Recebe 2 acionamentos à parti' +
          'r do índice 0.');
      end
      else if lstAdicionar.Selected[11] then
      begin
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia uma lista de períodos.');
        mmInfo.Lines.Add('Exemplo: 01+EPER+00+1+I[13[13:00:00[19:00:00[234567 I' +
          'nclui um período.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe uma lista de períodos.');
        mmInfo.Lines.Add('Exemplo: 01+RPER+00+2]0 Recebe 2 peíodos à partir do ' +
          'índice 0.');
      end
      else if lstAdicionar.Selected[12] then
      begin
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia uma lista de horários.');
        mmInfo.Lines.Add('Exemplo: 01+EHOR+00+1+I[13[Horário da Tarde[1[13 Incl' +
          'ui um horário.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe uma lista de horários.');
        mmInfo.Lines.Add('Exemplo: 01+RPER+00+2]0 Recebe 2 horários à partir do' +
          ' índice 0.');
      end
      else if lstAdicionar.Selected[13] then
      begin
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia uma lista de feriados.');
        mmInfo.Lines.Add('Exemplo: 01+EFER+00+1+I[01/01 Inclui dia 01/01 como f' +
          'eriado.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe uma lista de feriados.');
        mmInfo.Lines.Add('Exemlo: +RFER+00+1+0/1 Solicita a recepção de todos o' +
          's feriados do mês 1.');
      end
      else if lstAdicionar.Selected[14] then
      begin
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Envia as mensagens padrões de entrada e saída.');
        mmInfo.Lines.Add('Exemplo: 01+EMSG+00+2[Bem Vindo[5[[3[[5[ Envia a mens' +
          'agem de entrada como: Bem Vindo e o nome do funcionário, e a de saíd' +
          'a como: Saudação e o nome do do funcionário.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Recebe as mensagens padrões de entrada e saída.');
        mmInfo.Lines.Add('Exemplo: 01+RMSG+00');
      end
      else if lstAdicionar.Selected[15] then
      begin
        frmClienteGPRS.Panel5.Visible := True;
        Panel6.Visible := True;
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('O evento online se ativará quando receber aguardando ' +
          'liberação do aparelho.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Liberado: Indica que o evento foi validado e liberado' +
          '. Em caso de catraca, libera ambos os lados.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Negado: Acesso negado.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Mensagem: Descrição da mensagem que será mostrada no ' +
          'equipamento.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Tempo Rele / Msg: Tempo em que o equipamento mostra a' +
          ' mensagem e ativa o rele em segundos.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Rele: Seleciona um grupo de rele que será ativado no ' +
          'aparelho.');
      end
      else if lstAdicionar.Selected[16] then
      begin
        edtFile.Visible := True;
        btnFile.Visible := True;
        Panel7.Visible := True;
        lblDiretorio.Visible := True;
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Diretório: Endereço \ nome do arquivo.');
        mmInfo.Lines.Add('Exemplo: C:\Arquivos de programas\Arquivo.txt');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Ler Arquivo: Começa a leitura do arquivo selecionado.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Loop: Começa o processo de looping da leitura do arqu' +
          'ivo selecionado, o número de repetições é igual a Quantidade de Loop' +
          ' ou infinita se caso o campo esteja marcado. Para parar o loop preci' +
          'one ESC.');
      end;
      Break;
    end;
    if rdbtnReceber.Checked then
      edtConfirmarEnvio.Text := fncLerComando(lstAdicionar.Items[i], 'Receber')
    else if rdbtnEnviar.Checked then
      edtConfirmarEnvio.Text := fncLerComando(lstAdicionar.Items[i], 'Enviar');
end;

procedure proSelecionaPacote(pTipo : String);
{ Carrega o pacote selecionado na TEditBox de envio. }
var
  i : Integer;
begin
  _gTipoEnvio := pTipo;
  for i := frmClienteGPRS.lstAdicionar.Items.Count -1 downto 0 do
  begin
    if frmClienteGPRS.lstAdicionar.Selected[i] then
    begin
      frmClienteGPRS.edtConfirmarEnvio.Text :=
        fncLerComando(frmClienteGPRS.lstAdicionar.Items[i], pTipo);
      Break;
    end;
  end;
end;

procedure TfrmClienteGPRS.rdbtnEnviarMouseDown(Sender: TObject;
  Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
{ Chama o método de seleção de pacotes, com o parâmetro de envio. E caso for
  escolhido Registros ou Quantidade e Status, os TRadioGroup correspondentes
  habilitará a visibilidade. }
begin
  proSelecionaPacote('Enviar');
  rdgrParametroReg.Enabled := False;
  rdgrParametroQts.Enabled := False;
  rdgrParametroReg.ItemIndex := -1;
  rdgrParametroQts.ItemIndex := -1;
end;

procedure TfrmClienteGPRS.rdbtnReceberMouseDown(Sender: TObject;
  Button: TMouseButton; Shift: TShiftState; X, Y: Integer);
{ Chama o método de seleção de pacotes, com o parâmetro de recebimento. E caso
  for escolhido Registros ou Quantidade e Status, os TRadioGroup correspondentes
  habilitará a visibilidade. }
begin
  proSelecionaPacote('Receber');
  rdgrParametroReg.Enabled := True;
  rdgrParametroQts.Enabled := True;
end;

procedure TfrmClienteGPRS.rdgrParametroRegClick(Sender: TObject);
{ Habilita o grupo radio do Registro com o pré-preenchimento do parâmetro
  selecionado, e mosrta a descrição de cada parâmetro no TMemo mmInfo. }
begin
  if rdbtnReceber.Checked then
    case rdgrParametroReg.ItemIndex of
      0 : begin
        edtConfirmarEnvio.Text := '01+RR+00+M]3]0';
        mmInfo.Clear;
        mmInfo.Lines.Add('                        Prisma / Primme');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Solicita a recepção de registros à partir de' +
          ' um endereço de memória.');
        mmInfo.Lines.Add('Exemplo: 01+RR+00+M]2]0 Recebe 3 eventos à partir do ' +
          'endereço de memória 0.');
      end;
      1 : begin
        edtConfirmarEnvio.Text := '01+RR+00+N]5]1';
        mmInfo.Clear;
        mmInfo.Lines.Add('                        Prisma / Primme');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Solicita a recepção de registros à partir de' +
          ' um NSR.');
        mmInfo.Lines.Add('Exemplo: 01+RR+00+N]5]1 Recebe 5 eventos contados à p' +
          'artir do NSR 1.');
      end;
      2 : begin
        edtConfirmarEnvio.Text := '01+RR+00+D]2]10/07/2012 08:00:01]';
        mmInfo.Clear;
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Solicita a recepção de registros à partir de' +
          ' uma data.');
        mmInfo.Lines.Add('Exemplo: 01+RR+00+D]2]10/07/2012 08:00:01] Recebe 2 e' +
          'ventos contados à partir do primeiro evento ocorrido depois de 10/07' +
          '/2012 08:00:01.');
        mmInfo.Lines.Add('Observação: Para o PrimmeAcesso deve conter a data e ' +
          'hora inicial e final.');
      end;
      3 : begin
        edtConfirmarEnvio.Text := '01+RR+00+T]5]1';
        mmInfo.Clear;
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Solicita a recepção dos registros.');
        mmInfo.Lines.Add('Exemplo: 01+RR+00+T]5]1 Coletando 5 eventos à partir ' +
          'do índice 1.');
      end;
      4 : begin
        edtConfirmarEnvio.Text := '01+RR+00+C]5]0';
        mmInfo.Clear;
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Solicita a recepção de registros que ainda n' +
          'ão foram coletados.');
        mmInfo.Lines.Add('Exemplo: 01+RR+00+C]5]0 Coletando apenas os 5 eventos' +
          ' que ainda foram coletados à partir do índice 0.');
      end;
    end;
end;

procedure TfrmClienteGPRS.rdgrParametroQtsClick(Sender: TObject);
{ Habilita o grupo radio da Quantidade e Status com o preenchimento do
  parâmetro selecionado, e mosrta a descrição de cada parâmetro no TMemo mmInfo.}
begin
  if rdbtnReceber.Checked then
    case rdgrParametroQts.ItemIndex of
      0 : begin
        edtConfirmarEnvio.Text := '01+RQ+00+U';
        mmInfo.Clear;
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Retorna a quantidade de usuários cadastrados.');
      end;
      1 :  begin
        edtConfirmarEnvio.Text := '01+RQ+00+D';
        mmInfo.Clear;
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Retorna a quantidade de digitais cadastradas.');
      end;
      2 : begin
        edtConfirmarEnvio.Text := '01+RQ+00+TD';
        mmInfo.Clear;
        mmInfo.Lines.Add('                     Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Retorna a quantidade total de digitais que o' +
          ' módulo suporta.');
      end;
      3 : begin
        edtConfirmarEnvio.Text := '01+RQ+00+R';
        mmInfo.Clear;
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Retorna a quantidade de registros na memória.');
      end;
      4 : begin
        edtConfirmarEnvio.Text := '01+RQ+00+RNC';
        mmInfo.Clear;
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Retorna a quantidade de registros não coleta' +
          'dos na memória.');
      end;
      5 : begin
        edtConfirmarEnvio.Text := '01+RQ+00+C';
        mmInfo.Clear;
        mmInfo.Lines.Add('                         PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Retorna a quantidade de cartões cadastrados.');
      end;
      6 : begin
        edtConfirmarEnvio.Text := '01+RQ+00+TP';
        mmInfo.Clear;
        mmInfo.Lines.Add('                Prisma / Primme / PrimmeAcesso');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Informa se o equipamento está bloqueado.');
      end;
      7 : begin
        edtConfirmarEnvio.Text := '01+RQ+00+MRPE';
        mmInfo.Clear;
        mmInfo.Lines.Add('                        Prisma / Primme');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Informa se há erro ao comunicar com a MRP.');
      end;
      8 : begin
        edtConfirmarEnvio.Text := '01+RQ+00+SEMP';
        mmInfo.Clear;
        mmInfo.Lines.Add('                        Prisma / Primme');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Indica se não há empregador cadastrado.');
      end;
      9 : begin
        edtConfirmarEnvio.Text := '01+RQ+00+PP';
        mmInfo.Clear;
        mmInfo.Lines.Add('                               Prisma');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Informa se o sensor de pouco papel está ativado.');
      end;
      10 : begin
        edtConfirmarEnvio.Text := '01+RQ+00+SP';
        mmInfo.Clear;
        mmInfo.Lines.Add('                               Prisma');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Informa se o equipamento está sem papel.');
      end;
      11 : begin
        edtConfirmarEnvio.Text := '01+RQ+00+QP';
        mmInfo.Clear;
        mmInfo.Lines.Add('                               Prisma');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Enviar: Não possui função de envio.');
        mmInfo.Lines.Add('');
        mmInfo.Lines.Add('Receber: Retorna a quantidade de tickets que podem se' +
          'r impressos, o tamanho atual da bobina e o tamanho total da bobina.');
      end;
  end;
end;

function stringToBytes(pPackage : String) : TByteArray;
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

function fncLerComando(pTipoPacote : String; pMetodoEnvio : String) : String;
{ Lê o tipo de pacote recebido e o método de envio. Seleciona e retorna a
  string correspondente. }
begin
  if(pMetodoEnvio = 'Receber') then
  begin
    if(pTipoPacote = 'Configurações') then
      Result := '01+RC+00+IP'
    else if(pTipoPacote = 'Empregador') then
      Result := '01+RE+00'
    else if(pTipoPacote = 'Usuários') then
      Result := '01+RU+00+3]1'
    else if(pTipoPacote = 'Data Hora') then
      Result := '01+RH+00'
    else if(pTipoPacote = 'Biometria') then
      Result := '01+RD+00+L]2}0'
    else if(pTipoPacote = 'Registros') then
      Result := '01+RR+00+'
    else if(pTipoPacote = 'Quantidades e Status') then
      Result := '01+RQ+00+'
    else if(pTipoPacote = 'Cartão') then
      Result := '01+RCAR+00+2]0'
    else if(pTipoPacote = 'Grupo de Acesso') then
      Result := '01+RGA+00+2]0'
    else if(pTipoPacote = 'Cartão Grupo de Acesso') then
      Result := '01+RCGA+00+2]0'
    else if(pTipoPacote = 'Acionamento') then
      Result := '01+RACI+00+2]0'
    else if(pTipoPacote = 'Período') then
      Result := '01+RPER+00+2]0'
    else if(pTipoPacote = 'Horário') then
      Result := '01+RHOR+00+2]0'
    else if(pTipoPacote = 'Feriado') then
      Result := '01+RFER+00+1+0/1'
    else if(pTipoPacote = 'Mensagem') then
      Result := '01+RMSG+00';
  end
  else
  begin
    if(pTipoPacote = 'Configurações') then
      Result := '01+EC+00+HAB_TECLADO[H]'
    else if(pTipoPacote = 'Empregador') then
      Result := '01+EE+00+2]00000000001]]Empresa Teste]Pinhais'
    else if(pTipoPacote = 'Usuários') then
      Result := '01+EU+00+1+I[123123123123[TESTE[0[1[000023'
    else if(pTipoPacote = 'Data Hora') then
      Result := '01+EH+00+'+FormatDateTime('dd/mm/yy hh:mm:ss', Now)+']00/00/00]00/00/00'
    else if(pTipoPacote = 'Biometria') then
      Result := '01+ED+00+'
    else if(pTipoPacote = 'Cartão') then
      Result := '01+ECAR+00+1+A[1[1[09/07/2012 08:00:01[09/07/2012 17:00:01[1[1' +
        '[0[123[321[[BM[2[1[1[0[[0'
    else if(pTipoPacote = 'Grupo de Acesso') then
      Result := '01+EGA+00+1+I[000023[Grupo Equipe Suporte[01/01/2010 00:00:01[' +
        '30/12/2012 23:59:59[2[1[1[[[0[[0[[0[['
    else if(pTipoPacote = 'Cartão Grupo de Acesso') then
      Result := '01+ECGA+00+1+I[000023[1'
    else if(pTipoPacote = 'Acionamento') then
      Result := '01+EACI+00+1+I[13[Sirene Almoço[12:00:00[1[5[23456'
    else if(pTipoPacote = 'Período') then
      Result := '01+EPER+00+1+I[13[13:00:00[19:00:00[234567'
    else if(pTipoPacote = 'Horário') then
      Result := '01+EHOR+00+1+I[13[Horário da Tarde[1[13'
    else if(pTipoPacote = 'Feriado') then
      Result := '01+EFER+00+1+I[01/01'
    else if(pTipoPacote = 'Mensagem') then
      Result := '01+EMSG+00+2[Bem Vindo[5[[3[[5['
    else
      Result := '';
  end;
end;

{ Envia o pacote selecionado ao equipamento. }
procedure proEnviarPacote;
var
  _rPacoteBytes : TByteArray;
  _rPacoteHex, _rAux: String;
  i : Integer;
begin
  try
    _rAux := frmClienteGPRS.edtConfirmarEnvio.Text;

    frmClienteGPRS.mmEnviadosString.Lines.Add(_rAux);
    frmClienteGPRS.mmEnviadosString.Lines.Add('');

    if (_gEnabledAES) then
       begin
         _rAux := EncryptAES(_gKeyAES, _rAux);
       end;

    _rPacoteBytes := stringToBytes(_rAux);

    for i := 0 to Length(_rPacoteBytes) -1 do
      _rPacoteHex := _rPacoteHex + IntToHex(_rPacoteBytes[i], 2) + ' ';

    frmClienteGPRS.mmEnviados.Lines.Add(_rPacoteHex);
    frmClienteGPRS.mmEnviados.Lines.Add('');

    Cliente.Socket.SendBuf(Pointer(_rPacoteBytes)^, Length(_rPacoteBytes));
  except
    ShowMessage('Erro ao enviar dados.');
  end;
end;

function fncIsIP(pIP : string) : boolean;
var
  _rSplitIP : TStringList;
  i : integer;
begin
  _rSplitIP := TStringList.Create;
  Split('.', pIP, _rSplitIP);
  for i := 0 to _rSplitIp.Count - 1 do
    if StrToInt(_rSplitIP[i]) > 255 then
    begin
      Result := false;
      Exit;
    end;
  Result := true;
end;

procedure TfrmClienteGPRS.btnConectarClick(Sender: TObject);
{ Conecta ou desconecta do servidor. }
var
  _rDados, _rMensagem: String;
begin
  if (edtIP.Text <> '') then
    begin
      if (edtPorta.Text <> '') then
        begin
          if fncIsIP(edtIP.Text) then
            begin
              if btnConectar.Caption = 'Conectar' then
                begin
                  // Habilita conexão com o equipamento.
                  edtIP.Enabled := False;
                  edtPorta.Enabled := False;
                  ChkConexaoSegura.Enabled := False;
                  Cliente.Host := edtIP.Text;
                  Cliente.Port := StrToInt(edtPorta.Text);
                  Cliente.Active := True;
                end
              else
                begin
                  // Fecha a conexão com o equipamento.
                  if (_gEnabledAES) then
                    begin
                      _rDados := Format('%d]%s]%s]%s', [0, _gUsuario, _gSenha, MIMEBase64Encode(_gKeyAES)]);

                      _rMensagem := EncryptRSA(_gModulus, _gExpoent, _rDados);

                      frmClienteGPRS.edtConfirmarEnvio.Text := '01+EA+00+'+_rMensagem;
                      proEnviarPacote;
                      frmClienteGPRS.edtConfirmarEnvio.Clear;
                    end;

                  frmClienteGPRS.stbrStatus.SimpleText := 'Desconectado de ' + Cliente.Host;

                  Cliente.Socket.Disconnect(Cliente.Socket.SocketHandle);
                  Cliente.Socket.Close;
                  Cliente.Active := False;

                  _gEnabledAES := False;
                  _gUsuario := '';
                  _gSenha := '';

                  edtIP.Enabled := True;
                  edtPorta.Enabled := True;
                  ChkConexaoSegura.Enabled := True;
                  
                  btnConectar.Caption := 'Conectar';
                end;
            end
          else
            begin
              ShowMessage('IP inválido.');
            end;
        end
      else
        begin
          ShowMessage('Campo "Porta:" precisa estar povoado.');
        end;
    end
  else
    begin
      ShowMessage('Campo "IP Servidor:" precisa estar povoado.');
    end;
end;

procedure TfrmClienteGPRS.btnEnviarPacoteClick(Sender: TObject);
{ Chama o procedimento para enviar o Pacote selecionado caso o
  Cliente esteja ativo. }
begin
  if Cliente.Active = True then
  begin
    if edtConfirmarEnvio.Text = '' then
      ShowMessage('Campo "Pacote a ser enviado:" precisa estar povoado.')
    else
      proEnviarPacote;
  end
   else
    ShowMessage('Precisa estar conectado para efetuar essa operação.');
end;

procedure TfrmClienteGPRS.btnFileClick(Sender: TObject);
{ Envia ao equipamento os protocolos escritos no arquivo. }
var
  f_Response, f_File: TextFile;
  r_Line: String;
begin
  if Cliente.Active = True then
  begin
    if fileexists(frmClienteGPRS.edtFile.Text) then
    begin
      AssignFile(f_File, frmClienteGPRS.edtFile.Text);
      Reset(f_File );
      AssignFile(f_Response, 'Resposta.txt');
      Rewrite(f_Response);
      while not Eof ( f_File ) do
      begin
        ReadLn(f_File, r_Line );
        r_Line := Trim(r_Line);
        if not('//' = Copy(r_Line,1,2)) and not('' = Trim(r_Line))then
        begin
          if not('*pause*' = Copy(r_Line,1,7)) then
          begin
            if not('*sleep*' = Copy(r_Line,1,7)) then
            begin
              WriteLn(f_Response,'Mensagem:  01+'+ r_Line);
              frmClienteGPRS.edtConfirmarEnvio.Clear;
              frmClienteGPRS.edtConfirmarEnvio.Text := '01+' + r_Line;
              btnEnviarPacoteClick(Self);
              proEnviarPacote;
              Delay(200);
              WriteLn(f_Response,'Resposta:  '+ _gPacoteResponse);
            end
            else
             Delay(StrToInt(Trim((Copy(r_Line,8,100)))));
          end
          else
            ShowMessage('Pause');
        end
        else
        begin
          WriteLn(f_Response, r_Line);
        end;
      end;
      CloseFile(f_File);
      CloseFile(f_Response);
    end
    else
      Showmessage('Arquivo inexistente.');
  end
  else
    ShowMessage('Precisa estar conectado para efetuar essa operação.');
end;

procedure Delay(MSec: Cardinal);
{ Determina o tempo que o programa irá ficar parado. }
var
  Start: Cardinal;
begin
  Start := GetTickCount;
  repeat
    Application.ProcessMessages;
  until (GetTickCount - Start) >= MSec;
end;

function GenerateKeyAES(KeySize: Integer): String;
var
  i_KeySizeBits: Integer;
begin
  i_KeySizeBits := KeySize * 8;

  if (i_KeySizeBits <> 128) and
     (i_KeySizeBits <> 192) and
     (i_KeySizeBits <> 256) then
     begin
       raise Exception.Create('Invalid AES key length');
     end;

  while (KeySize > 0) do
    begin
      //Result := Result + '1'; // 1111111111111111 (MTExMTExMTExMTExMTExMQ==)

      Randomize;
      Result := Result + IntToStr(RandomRange(0, 9));

      Dec(KeySize);
    end;
end;

function EncryptAES(const s_Key: String; s_Data: String): String;
var
  i_KeySize, i_BytesRemainder: Integer;
  s_BytesRemainder, s_InitVector, s_MsgEncrypt: String;
begin
  try
    i_KeySize := Length(s_Key);

    i_BytesRemainder := Length(s_Data) mod i_KeySize;

    if (i_BytesRemainder > 0) then
       begin
         i_BytesRemainder := i_KeySize - i_BytesRemainder;
       end;

    s_BytesRemainder := '';

    while (i_BytesRemainder > 0) do
      begin
        s_BytesRemainder := s_BytesRemainder+#0;
        Dec(i_BytesRemainder);
      end;

    s_Data := s_Data+s_BytesRemainder;

    s_InitVector := SecureRandomStrA(i_KeySize);
    
    s_MsgEncrypt := Encrypt(ctAES,
                            AES_MODE,
                            cpNone,
                            (i_KeySize * 8), // Valor em bits
                            s_Key,
                            s_Data,
                            s_InitVector);

    Result := s_InitVector+s_MsgEncrypt;
  except
    on E: Exception do
      begin
        Result := '';
        ShowMessage(E.Message);
      end;
  end;
end;

function DecryptAES(const s_Key: String; s_Data: String): String;
var
  i_KeySize: Integer;
  s_InitVector, s_MsgDecrypt: String;
begin
  i_KeySize := Length(s_Key);

  s_InitVector := Copy(s_Data, 1, i_KeySize);
  Delete(s_Data, 1, i_KeySize);

  try
    s_MsgDecrypt := Decrypt(ctAES,
                            AES_MODE,
                            cpNone,
                            (i_KeySize * 8),
                            s_Key,
                            s_Data,
                            s_InitVector);

    Result := s_MsgDecrypt;
  except
    on E: Exception do
      begin
        Result := '';
        ShowMessage(E.Message);
      end;
  end;
end;

function EncryptRSA(const s_Modulus, s_Exponent, s_Plain: String): String;
var
  PublicKey: TRSAPublicKey;
  s_BinModulus, s_BinExpoent, s_MsgEncrypt: String;
begin
  try
    try
      RSAPublicKeyInit(PublicKey);

      s_BinModulus := MIMEBase64Decode(s_Modulus);
      s_BinExpoent := MIMEBase64Decode(s_Exponent);

      RSAPublicKeyAssignBufStr(PublicKey, RSA_KEY_SIZE, s_BinModulus, s_BinExpoent);

      s_MsgEncrypt := RSAEncryptStr(RSA_ENCRYPT_SCHEME, PublicKey, s_Plain);

      Result := MIMEBase64Encode(s_MsgEncrypt);
    except
      on E: Exception do
         begin
           Result := '';
           ShowMessage(E.Message);
         end;
    end;
  finally
    RSAPublicKeyFinalise(PublicKey);
  end;
end;

procedure TfrmClienteGPRS.btnLimparClick(Sender: TObject);
{ Limpa as caixas de texto com as respostas do servidor. }
begin
  mmRecebidos.Clear;
  mmRecebidosAscii.Clear;
end;

procedure TfrmClienteGPRS.btnLimparEnviadosClick(Sender: TObject);
{ Limpa as caixas de texto com os bytes do pacote enviado. }
begin
  mmEnviados.Clear;
  mmEnviadosString.Clear;
end;

procedure TfrmClienteGPRS.btnLoopClick(Sender: TObject);
{ Começa o loop, vereficando se é infinito ou não ou pela quantidade pré-difinida
  pelo usuário. }
var
  loop,i: Integer;
begin
  if Cliente.Active = True then
  begin
    if fileexists(frmClienteGPRS.edtFile.Text) then
    begin
      if chbxLoopInfinito.Checked = true then
      begin
        while chbxLoopInfinito.Checked = true do
        begin
          btnFileClick(Self);
          if GetKeyState(VK_Escape) and 128=128 then
            break;
          if Cliente.Active = False then
            chbxLoopInfinito.Checked := False;
        end;
      end
      else
        if not(edtQtsLoop.Text = '')then
        begin
          loop := StrToInt(edtQtsLoop.Text);
          for i := 1 to loop do
          begin
            btnFileClick(Self);
            if GetKeyState(VK_Escape) and 128=128 then
              break;
          end;
        end;
    end
    else
      Showmessage('Arquivo inexistente.');
  end
  else
    ShowMessage('Precisa estar conectado para efetuar essa operação.');
end;

procedure TfrmClienteGPRS.edtQtsLoopKeyPress(Sender: TObject; var Key: Char);
{ Aceite apenas caracteres numericos.}
begin
  if not (Key in['0'..'9',Chr(8)]) then
  begin
    Key:= #0
  end;
end;

procedure TfrmClienteGPRS.edtSegKeyPress(Sender: TObject; var Key: Char);
{ Aceite apenas caracteres numericos.}
begin
  if not (Key in['0'..'9',Chr(8)]) then
  begin
    Key:= #0
  end;
end;

{ TClienteGPRS }

constructor TClienteGPRS.Create;
begin
  inherited Create(nil);
end;

procedure TClienteGPRS.doOnConnect(Sender: TObject; Socket: TCustomWinSocket);
{ Muda o status da tela ao conectar com o equipamento. }
begin
  frmClienteGPRS.stbrStatus.SimpleText := 'Conectado à ' + Cliente.Host;
  frmClienteGPRS.btnConectar.Caption := 'Desconectar';

  if (frmClienteGPRS.ChkConexaoSegura.Checked) then
     begin
       frmLogin := TfrmLogin.Create(nil);

       try
         frmLogin.ShowModal;

         if (frmLogin.ModalResult = mrOk) then
           begin
             _gUsuario := Trim(frmLogin.edtUsuario.Text);
             _gSenha := Trim(frmLogin.edtSenha.Text);

             frmClienteGPRS.edtConfirmarEnvio.Text := '01+RA+00';
             proEnviarPacote;
           end
         else
           begin
             frmClienteGPRS.btnConectarClick(frmClienteGPRS.btnConectar);
           end;
       finally
         FreeAndNil(frmLogin);
       end;
     end;
end;

procedure TClienteGPRS.doOnRead(Sender: TObject; Socket: TCustomWinSocket);
{ Realiza a leitura dos dados quando o servidor responde. }
var
  _rPacoteBytes : array of byte;
  _rPacoteString, _rSubStr, _rDados, _rMensagem: string;
  i, _rTamanhoPacote, _rStatus: integer;
  _rRele,_rCodREON : String;  // Grupo de String utilizado no Evento Online.
  _rPos, _rCount: Integer;
begin
  try
    _rTamanhoPacote := Socket.ReceiveBuf(Pointer(nil)^, -1);
    SetLength(_rPacoteBytes, _rTamanhoPacote);
    Socket.ReceiveBuf(Pointer(_rPacoteBytes)^, Length(_rPacoteBytes));

    // Converte e mostra o pacote recebido em hexadecimal
    for i := 0 to _rTamanhoPacote - 1 do
      _rPacoteString := _rPacoteString + IntToHex(_rPacoteBytes[i], 2) + ' ';
    frmClienteGPRS.mmRecebidos.Lines.Add(_rPacoteString);
    frmClienteGPRS.mmRecebidos.Lines.Add('');

    // Converte e motra o pacote recebido em string
    SetLength(_rPacoteString, 0);
    for i := 3 to _rTamanhoPacote - 3 do
  	  _rPacoteString := _rPacoteString + Chr(_rPacoteBytes[i]);

    if (_gEnabledAES) then
      begin
        _rPacoteString := DecryptAES(_gKeyAES, _rPacoteString);
      end;

    _gPacoteResponse := _rPacoteString;
    frmClienteGPRS.mmRecebidosAscii.Lines.Add(_rPacoteString);
    frmClienteGPRS.mmRecebidosAscii.Lines.Add('');

    _rSubStr := '+REON+00+0';

    _rPos := Pos(_rSubStr, _rPacoteString);

    if (_rPos = 0) then
      begin
        _rSubStr := '+REON+000+0';
        _rPos := Pos(_rSubStr, _rPacoteString);
      end;

    // Verifica se existe um pedido de Aguardando Liberação do evento online.
    if (_rPos > 0) then
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

      _rPos := Pos(']', _rPacoteString);

      frmClienteGPRS.edtConfirmarEnvio.Text := Copy(_rPacoteString,1,_rPos-2)+
        _rCodREON+']'+frmClienteGPRS.edtSEG.Text+']'+frmClienteGPRS.edtMsg.Text+
        ']'+_rRele;
      proEnviarPacote;
      frmClienteGPRS.edtConfirmarEnvio.Clear;
    end
  end;

  _rSubStr := '01+RA+';
  _rPos := Pos(_rSubStr, _rPacoteString);

  {Se equipamento está retornando a Chave Pública (Módulo e Expoente), deve
  ser feita a autenticação com o equipamento.}
  if (_rPos > 0) then
    begin
      _rCount := Length(_rSubStr);
      Delete(_rPacoteString, 1, _rCount);

      _rSubStr := '+';
      _rPos := Pos(_rSubStr, _rPacoteString);

      if (_rPos > 0) then
        begin
          _rStatus := StrToIntDef(Copy(_rPacoteString, 1, _rPos-1), -1);
          Delete(_rPacoteString, 1, _rPos);
        end
      else
        begin
          _rStatus := StrToIntDef(_rPacoteString, -1);
        end;

      if (_rStatus = 0) then
        begin
          _rPos := Pos(']', _rPacoteString);

          _gModulus := Copy(_rPacoteString, 1, _rPos-1);
          Delete(_rPacoteString, 1, _rPos);

          _gExpoent := Copy(_rPacoteString, 1, Length(_rPacoteString));
          _gExpoent := StringReplace(_gExpoent, #$D#$A, '', [rfReplaceAll]);

          _gKeyAES := GenerateKeyAES(AES_KEY_SIZE);

          _rDados := Format('%d]%s]%s]%s', [1, _gUsuario, _gSenha, MIMEBase64Encode(_gKeyAES)]);

          _rMensagem := EncryptRSA(_gModulus, _gExpoent, _rDados);

          frmClienteGPRS.edtConfirmarEnvio.Text := '01+EA+00+'+_rMensagem;
          proEnviarPacote;
          frmClienteGPRS.edtConfirmarEnvio.Clear;
        end
      else
        begin
          ShowMessage('Erro ao receber chave pública do equipamento. Código do erro: '+_rPacoteString);
          frmClienteGPRS.btnConectar.Caption := 'Desconectar';
          frmClienteGPRS.btnConectarClick(frmClienteGPRS.btnConectar);
        end;
    end;

    _rSubStr := '01+EA+';
    _rPos := Pos(_rSubStr, _rPacoteString);

    if (_rPos > 0) then
       begin
         _rCount := Length(_rSubStr);
         Delete(_rPacoteString, 1, _rCount);

         _rStatus := StrToIntDef(_rPacoteString, -1);

         if (_rStatus = 0) then
            begin
              _gEnabledAES := True;
              ShowMessage('Usuário autenticado com Sucesso');
            end
         else
            begin
              ShowMessage('Erro ao autenticar usuário. Código do erro: '+_rPacoteString);
              frmClienteGPRS.btnConectar.Caption := 'Desconectar';
              frmClienteGPRS.btnConectarClick(frmClienteGPRS.btnConectar);
            end;
       end;
  except
    ShowMessage('Falha ao receber dados do equipamento.');
  end;
end;

end.

