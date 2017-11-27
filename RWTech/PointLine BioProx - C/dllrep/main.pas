unit main;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  Menus, StdCtrls, Buttons, ScktComp, ExtCtrls, ComCtrls, uRep, uHash, uUtil, uCodErro;

type
  TChatForm = class(TForm)
    MainMenu1: TMainMenu;
    File1: TMenuItem;
    Exit1: TMenuItem;
    FileConnectItem: TMenuItem;
    FileListenItem: TMenuItem;
    StatusBar1: TStatusBar;
    Bevel1: TBevel;
    Panel1: TPanel;
    Memo2: TMemo;
    N1: TMenuItem;
    SpeedButton1: TSpeedButton;
    Disconnect1: TMenuItem;
    ServerSocket: TServerSocket;
    gpbx_Config: TGroupBox;
    lblIp: TLabel;
    edtIp1: TEdit;
    edtPorta: TEdit;
    lblPorta: TLabel;
    btnConectar: TButton;
    btnDesconectar: TButton;
    gpbxEnvio: TGroupBox;
    lblDigital: TLabel;
    mmDigital: TMemo;
    btnEnviar: TButton;
    EdtHash: TEdit;
    lblHash: TLabel;
    LblCpf: TLabel;
    EdtCpf: TEdit;
    LblPis: TLabel;
    EdtPis: TEdit;
    chkbxHashHabilitado: TCheckBox;
    Button1: TButton;
    edtIp2: TEdit;
    edtIp3: TEdit;
    edtIp4: TEdit;
    procedure FileListenItemClick(Sender: TObject);
    procedure FileConnectItemClick(Sender: TObject);
    procedure Exit1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure ServerSocketError(Sender: TObject; Number: Smallint;
      var Description: string; Scode: Integer; const Source,
      HelpFile: string; HelpContext: Integer; var CancelDisplay: Wordbool);
    procedure Disconnect1Click(Sender: TObject);
    procedure ServerSocketClientRead(Sender: TObject;
      Socket: TCustomWinSocket);
    procedure ServerSocketAccept(Sender: TObject;
      Socket: TCustomWinSocket);
    procedure ServerSocketClientConnect(Sender: TObject;
      Socket: TCustomWinSocket);
    procedure ServerSocketClientDisconnect(Sender: TObject;
      Socket: TCustomWinSocket);
    procedure btnEnviarClick(Sender: TObject);
    procedure chkbxHashHabilitadoClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
  protected
    IsServer: Boolean;
  end;

const
  TAM_DIGITAL = 800;
  TAM_PIS_BYTES = 6; 

var
  ChatForm: TChatForm;
  Server: String;
  Porta: Integer;
  DigitalEmBytes: array[0..(TAM_DIGITAL - 1)] of byte;
  PisEmBytes: array[0..(TAM_PIS_BYTES - 1)] of byte;

implementation

{$R *.DFM}

procedure TChatForm.FileListenItemClick(Sender: TObject);
begin
  FileListenItem.Checked := not FileListenItem.Checked;
  if FileListenItem.Checked then
  begin
    FecharComunicacaoComRep;
    ServerSocket.Active := True;
    Statusbar1.Panels[0].Text := 'Listening...';
  end
  else
  begin
    if ServerSocket.Active then
      ServerSocket.Active := False;
    Statusbar1.Panels[0].Text := '';
  end;
end;

procedure TChatForm.FileConnectItemClick(Sender: TObject);
var
  hash: array[0..(KeyLenMax - 1)] of byte;
  ipBytes: array[0..3] of byte;
begin
  ipBytes[0] := strToInt(edtIp1.Text);
  ipBytes[1] := strToInt(edtIp2.Text);
  ipBytes[2] := strToInt(edtIp3.Text);
  ipBytes[3] := strToInt(edtIp4.Text);

  Server := edtIp1.Text + '.' + edtIp2.Text + '.' + edtIp3.Text + '.' + edtIp4.Text;
  Porta := strToInt(edtPorta.Text);

  if ComunicacaoAbertaComRep then FecharComunicacaoComRep;

  if Length(Server) > 0 then
  begin
//    strHexToArrayByte(edtHash.Text, hash, KeyLenMax);
    if (AbrirComunicacaoComRep(PChar(Server), Porta, 10000, PChar(edtHash.Text)) = COD_ERRO_OK) then
    begin
        Statusbar1.Panels[0].Text := 'Connected to: ' + Server + ':' + intToStr(Porta);
    end;
    FileListenItem.Checked := False;
  end;

  if ServerSocket.Active then ServerSocket.Active := False;
  if Length(Server) > 0 then
    with ServerSocket do
    begin
      Port := Porta;
      Active := True;
    end;
end;

procedure TChatForm.Exit1Click(Sender: TObject);
begin
  ServerSocket.Close;
  FecharComunicacaoComRep;
  Close;
end;

procedure TChatForm.FormCreate(Sender: TObject);
begin
  FileListenItemClick(nil);
end;

procedure TChatForm.ServerSocketError(Sender: TObject; Number: Smallint;
  var Description: string; Scode: Integer; const Source, HelpFile: string;
  HelpContext: Integer; var CancelDisplay: Wordbool);
begin
  ShowMessage(Description);
end;

procedure TChatForm.Disconnect1Click(Sender: TObject);
begin
  FecharComunicacaoComRep;
  ServerSocket.Active := True;
  Statusbar1.Panels[0].Text := 'Listening...';
end;

procedure TChatForm.ServerSocketClientRead(Sender: TObject;
  Socket: TCustomWinSocket);
var
  DadosRecebidos: String;
  QuantidadeDeBytesRecebidos: Integer;
  i: integer;
begin
(*
  QuantidadeDeBytesRecebidos := Socket.ReceiveBuf(BufferDeRecepcao, TAM_DIGITAL);

  DadosRecebidos := '';

  if (chkbxHashHabilitado.Checked) then // Com criptografia?
  begin
    Descriptografar(HashEmBytes{edtHash.Text}, BufferDeRecepcao, BufferDeRecepcaoDescrip, QuantidadeDeBytesRecebidos);

    for i := 0 to (QuantidadeDeBytesRecebidos - 1) do
    begin
      DadosRecebidos := DadosRecebidos + intToHex(BufferDeRecepcaoDescrip[i], 2);
    end;
  end
  else // Sem criptografia?
  begin
    for i := 0 to (QuantidadeDeBytesRecebidos - 1) do
    begin
      DadosRecebidos := DadosRecebidos + intToHex(BufferDeRecepcao[i], 2);
    end;
  end;

  Memo2.Lines.Add(DadosRecebidos);

  BytesRecebidos := BytesRecebidos + QuantidadeDeBytesRecebidos;
  Statusbar1.Panels[0].Text := 'Connected to: ' + Server + ':'
        + intToStr(Porta) + '. Enviados: ' + intToStr(BytesEnviados)
        + ' B. Recebidos: ' + intToStr(BytesRecebidos) + ' B.';

  // Start.
  BufferSaidaCru[0] := $3D;
  // Endereço.
  BufferSaidaCru[1] := $00;
  BufferSaidaCru[2] := $00;
  BufferSaidaCru[3] := $00;
  BufferSaidaCru[4] := $00;
  // Comando (GBx).
  BufferSaidaCru[5] := $BF;
  // Número de digitais.
  BufferSaidaCru[6] := $00;
  BufferSaidaCru[7] := $00;
  BufferSaidaCru[8] := $00;
  BufferSaidaCru[9] := $01;
  // Tamanho do campo de dados.
  BufferSaidaCru[10] := $00;
  BufferSaidaCru[11] := $00;
  BufferSaidaCru[12] := $03;
  BufferSaidaCru[13] := $3B;
  // PIS. Obs.: vem tudo zerado neste momento.
  BufferSaidaCru[14] := $00;
  BufferSaidaCru[15] := $00;

  // Manda o primeiro pacote.
  Criptografar(HashEmBytes{edtHash.Text}, BufferSaidaCru, BufferSaidaPronto, BlkLenMax);
  ServerSocket.Socket.Connections[0].SendBuf(BufferSaidaPronto, BlkLenMax);

  // PIS. Obs.: vem tudo zerado neste momento.
  BufferSaidaCru[0] := $00;
  BufferSaidaCru[1] := $00;
  BufferSaidaCru[2] := $00;
  BufferSaidaCru[3] := $00;
  // Flag/Error.
  BufferSaidaCru[4] := $54; // SUCESSO.
  // BCC.
  BufferSaidaCru[5] := $EF;
  // End.
  BufferSaidaCru[6] := $40;
  // Preenchimento.
  BufferSaidaCru[7] := $FF;
  BufferSaidaCru[8] := $FF;
  BufferSaidaCru[9] := $FF;
  BufferSaidaCru[10] := $FF;
  BufferSaidaCru[11] := $FF;
  BufferSaidaCru[12] := $FF;
  BufferSaidaCru[13] := $FF;
  BufferSaidaCru[14] := $FF;
  BufferSaidaCru[15] := $FF;

  // Manda o segundo pacote.
  Criptografar(HashEmBytes{edtHash.Text}, BufferSaidaCru, BufferSaidaPronto, BlkLenMax);
  ServerSocket.Socket.Connections[0].SendBuf(BufferSaidaPronto, BlkLenMax);
*)
end;

procedure TChatForm.ServerSocketAccept(Sender: TObject;
  Socket: TCustomWinSocket);
begin
  IsServer := True;
  Statusbar1.Panels[0].Text := 'Connected to: ' + Socket.RemoteAddress + ':' + intToStr(Porta);
end;

procedure TChatForm.ServerSocketClientConnect(Sender: TObject;
  Socket: TCustomWinSocket);
begin
  Memo2.Lines.Clear;
//  BytesEnviados := 0;
//  BytesRecebidos := 0;
end;

procedure TChatForm.ServerSocketClientDisconnect(Sender: TObject;
  Socket: TCustomWinSocket);
begin
  Statusbar1.Panels[0].Text := 'Listening...';
end;

procedure TChatForm.btnEnviarClick(Sender: TObject);

var
  qdeBytes: integer;
  qdeEnviada: integer;
  qdeBytesRecebidos: integer;
  DadosRecebidos: String;
  i: integer;
  codErro: integer;
begin
  //strHexToArrayByte(mmDigital.Text, DigitalEmBytes, TAM_DIGITAL);
  //strHexToArrayByte(edtPis.Text, PisEmBytes, TAM_PIS_BYTES);

  codErro := EnviarDigital(PChar(edtPis.Text), 1, 1, PChar(mmDigital.Text));

  ShowMessage('EnviarDigital. Erro=' + intToStr(codErro));
end;

procedure TChatForm.chkbxHashHabilitadoClick(Sender: TObject);
begin
  edtHash.Enabled := chkbxHashHabilitado.Checked;
end;

procedure TChatForm.Button1Click(Sender: TObject);
begin
{
  if IsServer then
    ServerSocket.Socket.Connections[0].SendBuf(BufferDeEnvio, 1);
}
end;

end.
