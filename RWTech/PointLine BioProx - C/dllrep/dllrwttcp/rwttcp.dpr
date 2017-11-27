library rwttcp;

{ Important note about DLL memory management: ShareMem must be the
  first unit in your library's USES clause AND your project's (select
  Project-View Source) USES clause if your DLL exports any procedures or
  functions that pass strings as parameters or function results. This
  applies to all strings passed to and from your DLL--even those that
  are nested in records and classes. ShareMem is the interface unit to
  the BORLNDMM.DLL shared memory manager, which must be deployed along
  with your DLL. To avoid using BORLNDMM.DLL, pass string information
  using PChar or ShortString parameters. }

uses
  SysUtils,
  Classes,
  ScktComp, // Para utilizar o TClientSocket.
  extctrls, // Para utilizar o TTimer.
  forms,    // Para utilizar o TApplication.
  Dialogs,  // Para utilizar o ShowMessage.
  uCodErro in '..\uCodErro.pas';

type
  TClientSocketEvents = class
  public
    procedure ClientSocketConnect(Sender: TObject; Socket: TCustomWinSocket);
    procedure ClientSocketDisconnect(Sender: TObject; Socket: TCustomWinSocket);
    procedure ClientSocketError(Sender: TObject; Socket: TCustomWinSocket; ErrorEvent: TErrorEvent;
                                      var ErrorCode: Integer);
    procedure ClientSocketRead(Sender: TObject; Socket: TCustomWinSocket);
  end;
  
type
  TTimerEvents = class
  public
    class procedure OnTimer(Sender: TObject);
  end;
  
const
  TAM_BUFFER_DE_RECEPCAO       = 1024;
  TIMEOUT_PADRAO_MS            = 15000;
  
var
  ClientSocketEvents: TClientSocketEvents = nil;
  ClientSocket: TClientSocket = nil;
  TimerTimeout: TTimer = nil;
  OcorreuTimeout: boolean = false;
  TimeoutPadraoMs: integer = TIMEOUT_PADRAO_MS;
  ComunicacaoAtiva: boolean = false;
  QdeBytesRecebidos: integer = 0;
  BufferDeRecepcao: array[0..(TAM_BUFFER_DE_RECEPCAO - 1)] of byte;
  ErroOcorrido: integer = COD_ERRO_OK;

{$R *.RES}

function ativarDesativarComunicacao(ativar: boolean): integer;
var
  codigoDeErro: integer;  
begin
  if (nil <> ClientSocket) then // O objeto do socket j� foi instanciado?
  begin
    if (ativar = ClientSocket.Active) then // O socket j� est� no estado desejado?
    begin
      codigoDeErro := COD_ERRO_OK;
    end
    else // O estado do socket precisar� ser modificado.
    begin
      ErroOcorrido := COD_ERRO_OK;
      OcorreuTimeout := false;

      TimerTimeout.Enabled := true; // Dispara a contagem do timer.
      
      ComunicacaoAtiva := ClientSocket.Active; // Salva o estado atual do socket.
      ClientSocket.Active := ativar;     // Ativa/Desativa o socket de fato.

      while ((not OcorreuTimeout) and (ativar <> ComunicacaoAtiva) and (COD_ERRO_OK = ErroOcorrido)) do // Aguarda at� que a comunica��o seja ativada/desativada, ocorra o timeout ou um erro.
      begin
        Application.ProcessMessages; // Permite tratar outros eventos.
      end;

      TimerTimeout.Enabled := false; // Interrompe a contagem do timer.
      
      if (OcorreuTimeout) then // Ocorreu timeout?
      begin
        codigoDeErro := COD_ERRO_TIMEOUT;
      end
      else if (COD_ERRO_OK <> ErroOcorrido) then // Ocorreu algum erro?
      begin
        codigoDeErro := COD_ERRO_CONEXAO;
      end
      else if (ComunicacaoAtiva = ativar) then // Deu tudo certo?
      begin
        codigoDeErro := COD_ERRO_OK;
      end
      else // H� algo inconsistente?
      begin
        codigoDeErro := COD_ERRO_INCONSISTENCIA;

        // Coloca o socket em seu estado de descanso.
        ComunicacaoAtiva := false;
        ClientSocket.Active := false;
      end;
    end;
  end
  else // O objeto do socket ainda n�o foi instanciado?
  begin
    if (false = ativar) then // Est� mandando fechar a comunica��o?
    begin
      codigoDeErro := COD_ERRO_OK;
    end
    else // Est� mandando abrir a comunica��o?
    begin
      codigoDeErro := COD_ERRO_INCONSISTENCIA; // Sinaliza que a comunica��o n�o pode ser aberta, pois o objeto do socket ainda n�o foi instanciado.
    end;
  end;

  result := codigoDeErro;
end;

//************************************************************************************************************
{ TClientSocketEvents }
procedure TClientSocketEvents.ClientSocketConnect(Sender: TObject; Socket: TCustomWinSocket);
begin
  ComunicacaoAtiva := true;
  QdeBytesRecebidos := 0;
end;

procedure TClientSocketEvents.ClientSocketDisconnect(Sender: TObject; Socket: TCustomWinSocket);
begin
  ComunicacaoAtiva := false;
end;

procedure TClientSocketEvents.ClientSocketError(Sender: TObject; Socket: TCustomWinSocket;
                                                      ErrorEvent: TErrorEvent; var ErrorCode: Integer);
begin
  ErroOcorrido := ErrorCode;
  ErrorCode := 0;
end;

procedure TClientSocketEvents.ClientSocketRead(Sender: TObject; Socket: TCustomWinSocket);
begin
  QdeBytesRecebidos := QdeBytesRecebidos + Socket.ReceiveBuf(BufferDeRecepcao[QdeBytesRecebidos], TAM_BUFFER_DE_RECEPCAO);
end;
//************************************************************************************************************

{ TTimer }
class procedure TTimerEvents.OnTimer(Sender: TObject);
begin
  OcorreuTimeout := true;
end;

//************************************************************************************************************

function AbrirComunicacao(ip: PChar; porta: integer; timeoutMs: integer): integer; stdcall;
var
  codigoDeErro: integer;
begin
  codigoDeErro := COD_ERRO_OK;
  TimeoutPadraoMs := timeoutMs;
  if (TimeoutPadraoMs < 1) then // O timeout especificado � inv�lido?
  begin
    TimeoutPadraoMs := TIMEOUT_PADRAO_MS; // Utiliza o timeout padr�o como timeout das execu��es desta fun��o.
  end;

  if (nil = TimerTimeout) then // O timer do timeout ainda n�o foi instanciado?
  begin
    TimerTimeout          := TTimer.Create(nil);
    TimerTimeout.Enabled  := false;
    TimerTimeout.OnTimer  := TTimerEvents.OnTimer;
  end;

  TimerTimeout.Interval := TimeoutPadraoMs;

  if (nil = ClientSocketEvents) then // O objeto que implementa os eventos do socket n�o est� instanciado?
  begin
    ClientSocketEvents := TClientSocketEvents.Create;
  end;

  if (nil = ClientSocket) then // Socket ainda n�o instanciado?
  begin
    ClientSocket              := TClientSocket.Create(nil);
    ClientSocket.Active       := false; // Garante que o socket est� desabilitado antes de configurar.
    ComunicacaoAtiva          := false;
    ClientSocket.OnConnect    := ClientSocketEvents.ClientSocketConnect;
    ClientSocket.OnConnecting := nil;
    ClientSocket.OnDisconnect := ClientSocketEvents.ClientSocketDisconnect;
    ClientSocket.OnError      := ClientSocketEvents.ClientSocketError;
    ClientSocket.OnLookup     := nil;
    ClientSocket.OnRead       := ClientSocketEvents.ClientSocketRead;
    ClientSocket.OnWrite      := nil;
    ClientSocket.Address      := '';
    ClientSocket.ClientType   := ctNonBlocking;
    ClientSocket.Name         := 'ClientSocket';
    ClientSocket.Service      := '';
    ClientSocket.Tag          := 0;
    codigoDeErro              := COD_ERRO_OK;
  end
  else // Socket j� instanciado?
  begin
    if (true = ComunicacaoAtiva) then // Comunica��o ativa?
    begin
      // @todo Implementar para verificar se a configura��o j� n�o � a atual e s� reconfigurar caso necess�rio.
      codigoDeErro := ativarDesativarComunicacao(false); // Desativa a comunica��o para alterar a configura��o do socket antes de reativ�-lo.
    end;
  end;

  if (COD_ERRO_OK = codigoDeErro) then
  begin
    ClientSocket.Host   := ip; // Define o endere�o do servidor.
    ClientSocket.Port   := porta; // Define a porta a ser utilizada para a comunica��o com o servidor.
    codigoDeErro := ativarDesativarComunicacao(true);
  end;
  
  result := codigoDeErro;
end;

function ComunicacaoAberta: boolean; stdcall;
var
  aberta: boolean;
begin
  if (nil <> ClientSocket) then // O objeto do socket j� est� instanciado?
  begin
    if (ClientSocket.Active and ComunicacaoAtiva) then // Comunica��o aberta?
    begin
      aberta := true;
    end
    else // Comunica��o fechada?
    begin
      aberta := false;

      if (ClientSocket.Active <> ComunicacaoAtiva) then // Sinaliza��o inconsistente.
      begin
        ClientSocket.Active := false;
        ComunicacaoAtiva := false;
      end;
    end;
  end
  else // O objeto do socket ainda n�o est� instanciado?
  begin
    aberta := false;
  end;

  result := aberta;
end;

function FecharComunicacao: integer; stdcall;
var
  codigoDeErro: integer;
begin
  codigoDeErro := ativarDesativarComunicacao(false);

  FreeAndNil(ClientSocket);

  result := codigoDeErro;
end;

function ReceberBytes(var buffer: array of byte; qdeDesejada: integer; var qdeRecebida: integer): integer; stdcall;
var
  codigoDeErro: integer;
  i: integer;
begin
  codigoDeErro := COD_ERRO_OK;
  ErroOcorrido := COD_ERRO_OK;
  OcorreuTimeout := false;
  QdeBytesRecebidos := 0;
  
  if (nil <> TimerTimeout) then // O objeto do socket j� foi instanciado?
  begin
    if (qdeDesejada < 1) then // O usu�rio n�o especificou a quantidade desejada de dados a receber?
    begin
      qdeDesejada := 1;
    end;

    TimerTimeout.Enabled := true; // Dispara a contagem do timer.
    
    while ((not OcorreuTimeout) and (COD_ERRO_OK = ErroOcorrido) and (QdeBytesRecebidos < qdeDesejada)) do // Aguarda at� que cheguem os dados, ocorra algum erro ou o timeout estoure.
    begin
      Application.ProcessMessages; // Obs.: permite tratar outros eventos.
    end;
    
    TimerTimeout.Enabled := false; // Interrompe a contagem do timer.
    
    if (QdeBytesRecebidos > 0) then // Recebeu algo?
    begin
      // Copia todos os bytes recebidos para o buffer de recep��o.
      for i := 0 to (QdeBytesRecebidos - 1) do
      begin
        buffer[i] := BufferDeRecepcao[i];
      end;
      
      qdeRecebida := QdeBytesRecebidos; // Sinaliza a quantidade de bytes recebidos pelo par�metro de sa�da "qdeRecebida".

      if (qdeDesejada > qdeRecebida) then // Apesar de receber, ainda faltou algum byte para receber?
      begin
        codigoDeErro := COD_ERRO_TIMEOUT; // Sinaliza timeout para este caso.
      end;
    end
    else if (OcorreuTimeout) then // Ocorreu timeout?
    begin
      codigoDeErro := COD_ERRO_TIMEOUT;
    end
    else // Ocorreu algum erro?
    begin
      codigoDeErro := COD_ERRO_INESPERADO;
    end;
  end
  else // O objeto do socket ainda n�o foi instanciado?
  begin
    codigoDeErro := COD_ERRO_COMUNICACAO_INATIVA;
  end;
    
  result := codigoDeErro;
end;

function EnviarBytes(var buffer: array of byte; qde: integer; var qdeEnviada: integer): integer; stdcall;
var
  codigoDeErro: integer;
begin
  if (nil <> ClientSocket) then // O objeto do socket j� foi instanciado?
  begin
    qdeEnviada := ClientSocket.Socket.SendBuf(buffer, qde); // Envia os dados de fato.

    if (qde = qdeEnviada) then // Bufferizou todos os bytes para serem enviados?
    begin
      codigoDeErro := COD_ERRO_OK;
    end
    else // Houve algum problema na bufferiza��o dos bytes?
    begin
      codigoDeErro := COD_ERRO_ENVIO;
    end;
  end
  else // O objeto do socket ainda n�o foi instanciado?
  begin
    codigoDeErro := COD_ERRO_COMUNICACAO_INATIVA;
    qdeEnviada := 0;
  end;
  
  result := codigoDeErro;
end;

exports
  AbrirComunicacao,
  ComunicacaoAberta,
  FecharComunicacao,
  ReceberBytes,
  EnviarBytes;

begin
end.
