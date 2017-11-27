library rwtrep;

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
  Windows,                       // Para usar o "Sleep".
  Classes,
  SysUtils,                      // Para usar rotinas de utilidades gerais do Delphi.
  uHash in '..\uHash.pas',       // Para usar a criptografia.
  uUtil in '..\uUtil.pas',       // Para usar as rotinas de utilidade.
  uCodErro in '..\uCodErro.pas', // Para usar os códigos de erro.
  Dialogs;                       // Para usar o "ShowMessage".

const
  TAM_DIGITAL = 800;
  TAM_DIGITAL_CRIPT = 816;
  TAM_DIGITAL_CABECALHO = 3;
  TAM_PACOTE_CABECALHO = 23;
  TAM_PACOTE_CS = 1;
  TAM_PIS_BYTES = 6;
  TAM_PACOTE_DIGITAL = TAM_PACOTE_CABECALHO + TAM_DIGITAL_CABECALHO + TAM_DIGITAL + TAM_PACOTE_CS;
  COD_ERRO_OK = 0;
var
  BytesEnviados: Integer;
  BytesRecebidos: Integer;
  BufferDeRecepcao: array[0..(TAM_PACOTE_DIGITAL - 1)] of byte;
  BufferDeRecepcaoDescrip: array[0..(TAM_PACOTE_DIGITAL - 1 + (BlkLenMax - 1))] of byte; // Acrescenta bytes para garantir espaço para todos os blocos.
  BufferDeEnvio: array[0..(TAM_PACOTE_DIGITAL - 1)] of byte;
  BufferDeEnvioCrip: array[0..(TAM_PACOTE_DIGITAL - 1 + (BlkLenMax - 1))] of byte; // Acrescenta bytes para garantir espaço para todos os blocos.
  BufferSaidaQde: integer = 0;
  BufferSaidaCru: array[0..(BlkLenMax - 1)] of byte;
  BufferSaidaPronto: array[0..(BlkLenMax - 1)] of byte;
  HashEmBytes: array[0..(KeyLenMax - 1)] of byte;
  TimeoutComunicacaoMs: integer;
  PisEmBytes: array[0..(TAM_PIS_BYTES - 1)] of byte;
  DigitalEmBytes: array[0..(TAM_DIGITAL - 1)] of byte;
  ParametroAtual: byte;
  ParametroTotal: byte;

function AbrirComunicacao(ip: PChar; porta: integer; timeoutMs: integer): integer; stdcall; external 'rwttcp.dll';
function ComunicacaoAberta: boolean; stdcall; external 'rwttcp.dll';
function FecharComunicacao: integer; stdcall; external 'rwttcp.dll';
function ReceberBytes(var buffer: array of byte; qdeDesejada: integer; var qdeRecebida: integer): integer; stdcall; external 'rwttcp.dll';
function EnviarBytes(var buffer: array of byte; qde: integer; var qdeEnviada: integer): integer; stdcall; external 'rwttcp.dll';

{$R *.RES}

function montaCabecalhoPacoteGbx(total: byte; pis: array of byte; var saida: array of byte): integer;
var
  qdeBytes: integer;

begin
  ParametroTotal := total;

  if (Length(saida) < TAM_PACOTE_CABECALHO) then // Não há espaço suficiente no array "saida"?
  begin
    qdeBytes := 0;
  end
  else // Espaço suficiente no array "saida"?
  begin

    try
      // Cabeçalho da comunicação de modo geral ------------------------------
      // Start.
      saida[0] := $0A;
      // Endereço.
      saida[1] := $00;
      saida[2] := $00;
      saida[3] := $00;
      saida[4] := $00;
      // Comando (GBx).
      saida[5] := $BF;
      // Número de digitais.
      saida[6] := $00;
      saida[7] := $00;
      saida[8] := $00;
      saida[9] := total;     
      // Código do usuário.
      saida[10] := $00;
      saida[11] := $00;
      saida[12] := $00;
      saida[13] := $00;
      // PIS (em BCD).
      saida[14] := pis[0];
      saida[15] := pis[1];
      saida[16] := pis[2];
      saida[17] := pis[3];
      saida[18] := pis[4];
      saida[19] := pis[5];
      // Flag/Error.
      if (1 = total) then
      begin
        saida[20] := $07; // UNICA_TEMPLATE_USUARIO.
      end
      else
      begin
        saida[20] := $06; // Se for mais de uma template.
      end;
      // BCC.
      saida[21] := calculaBcc(saida, 0, 21);
      // End.
      saida[22] := $40;

      qdeBytes := TAM_PACOTE_CABECALHO;
    except
      qdeBytes := 0;
    end;
  end;
  result := qdeBytes;
end;

function verificaRetornoCabecalhoPacoteGbx(retorno: array of byte): integer;
var
  codErro: integer;

begin
  if (Length(retorno) < TAM_PACOTE_CABECALHO) then // A quantidade de posições do "retorno" é inconsistente?
  begin
    codErro := COD_ERRO_INCONSISTENCIA;
  end
  else // A quantidade de posições do "retorno" é consistente?
  begin
    if (retorno[21] = calculaBcc(retorno, 0, 21)) then // O BCC do pacote está consistente?
    begin
      if (// Start.
        (retorno[0] = $3D)
        // Endereço.
        and (retorno[1] = $00)
        and (retorno[2] = $00)
        and (retorno[3] = $00)
        and (retorno[4] = $00)
        // Comando (GBx).
        and (retorno[5] = $BF)
        // Número de digitais.
        and (retorno[6] = $00)
        and (retorno[7] = $00)
        and (retorno[8] = $00)
        and (retorno[9] = ParametroTotal)
        // Tamanho do campo de dados.
        and (retorno[10] = $00)
        and (retorno[11] = $00)
        and (retorno[12] = $03)
        and (retorno[13] = $3B)
        // PIS. Obs.: vem tudo zerado neste momento.
        and (retorno[14] = $00)
        and (retorno[15] = $00)
        and (retorno[16] = $00)
        and (retorno[17] = $00)
        and (retorno[18] = $00)
        and (retorno[19] = $00)
        // End.
        and (retorno[22] = $40)) then // O pacote está como esperado?
      begin
        if ($54 = retorno[20]) then // O campo "Flag/Error" está sinalizando SUCESSO?
        begin
          codErro := COD_ERRO_OK;
        end
        else
        begin
          codErro := retorno[20]; // Sinaliza com o mesmo código de erro enviado pelo REP.
        end;
      end
      else // Há algum problema na formação esperada do pacote?
      begin
        codErro := COD_ERRO_FORMATO_DOS_DADOS;
      end;
    end
    else // O BCC do pacote está errado?
    begin
      codErro := COD_ERRO_BCC_INVALIDO;
    end;
  end;

  result := codErro;
end;

function montaFrameGbx(atual, total: byte; digital: array of byte; var saida: array of byte): integer;
var
  qdeBytes: integer;

begin
  ParametroAtual := atual;

  if ((Length(saida) < TAM_PACOTE_DIGITAL)
      or (Length(digital) < TAM_DIGITAL)) then // Há espaço suficiente os arrays (parâmetros de entrada e saída)?
  begin
    qdeBytes := 0;
  end
  else
  begin
    try
      // Cabeçalho do pacote -------------------------------------------------
      // Start.
      saida[0] := $0A;
      // Endereço.
      saida[1] := $00;
      saida[2] := $00;
      saida[3] := $00;
      saida[4] := $00;
      // Comando (GBx).
      saida[5] := $BF;
      // Número de digitais (digital X de Y).
      saida[6] := $00;
      saida[7] := total;
      saida[8] := $00;
      saida[9] := atual;
      // Tamanho do campo de dados.
      saida[10] := $00;
      saida[11] := $00;
      saida[12] := $03;
      saida[13] := $23; // 803 bytes.
      // PIS. Obs.: vai tudo zerado neste momento.
      saida[14] := $00;
      saida[15] := $00;
      saida[16] := $00;
      saida[17] := $00;
      saida[18] := $00;
      saida[19] := $00;
      // Flag/Error.
      saida[20] := $45; // DADOS_OK.
      // BCC.
      saida[21] := calculaBcc(saida, 0, 21);
      // End.
      saida[22] := $40;
      // Dados do pacote -------------------------------------------------
      // Digital.
      saida[23] := $01;
      saida[24] := $20;
      saida[25] := $03;
      memcpy(saida, digital, 26, TAM_DIGITAL);
      saida[TAM_PACOTE_CABECALHO + TAM_DIGITAL_CABECALHO + TAM_DIGITAL] := calculaBcc(saida, 21, 2 + TAM_DIGITAL_CABECALHO + TAM_DIGITAL);

      qdeBytes := TAM_PACOTE_CABECALHO + TAM_DIGITAL_CABECALHO + TAM_DIGITAL + 1;
    except
      qdeBytes := 0;
    end;
  end;
  result := qdeBytes;
end;

function verificaRetornoFrameGbx(retorno: array of byte): integer;
var
  codErro: integer;
begin
  if (Length(retorno) < TAM_PACOTE_CABECALHO) then // A quantidade de posições do "retorno" é inconsistente?
  begin
    codErro := COD_ERRO_INCONSISTENCIA;
  end
  else // A quantidade de posições do "retorno" é consistente?
  begin
    if (retorno[21] = calculaBcc(retorno, 0, 21)) then // O BCC do pacote está consistente?
    begin
      if (// Start.
            (retorno[0] = $3D)
        // Endereço.
        and (retorno[1] = $00)
        and (retorno[2] = $00)
        and (retorno[3] = $00)
        and (retorno[4] = $00)
        // Comando (GBx).
        and (retorno[5] = $BF)
        // Número de digitais.
        and (retorno[6] = $00)
        and (retorno[7] = ParametroTotal)
        and (retorno[8] = $00)
        and (retorno[9] = ParametroAtual)
        // Tamanho do campo de dados.
        and (retorno[10] = $00)
        and (retorno[11] = $00)
        and (retorno[12] = $03)
        and (retorno[13] = $23)
        // PIS. Obs.: vem tudo zerado neste momento.
        and (retorno[14] = $00)
        and (retorno[15] = $00)
        and (retorno[16] = $00)
        and (retorno[17] = $00)
        and (retorno[18] = $00)
        and (retorno[19] = $00)
        // End.
        and (retorno[22] = $40)) then // O pacote está como esperado?
      begin
        if ($45 = retorno[20]) then // O REP sinalizou sucesso SUCESSO?
        begin
          codErro := COD_ERRO_OK;
        end
        else // O REP sinalizou algum erro?
        begin
          codErro := retorno[20]; // Sinaliza com o mesmo código de erro enviado pelo REP.
        end;
      end
      else // Há algum problema na formação esperada do pacote?
      begin
        codErro := COD_ERRO_FORMATO_DOS_DADOS;
      end;
    end
    else // O BCC do pacote está errado?
    begin
      codErro := COD_ERRO_BCC_INVALIDO;
    end;
  end;

  result := codErro;
end;

procedure EnviarBuffer(buffer: array of byte; tam: integer; enviarTudo: boolean);
var
  i: integer;
  qdeEnviada: integer;

begin
  for i := 0 to (tam - 1) do
  begin
    BufferSaidaCru[BufferSaidaQde] := buffer[i];
    inc(BufferSaidaQde);
    if (BufferSaidaQde >= BlkLenMax) then
    begin
      BufferSaidaQde := 0;
      Criptografar(HashEmBytes, BufferSaidaCru, BufferSaidaPronto, BlkLenMax);
      EnviarBytes(BufferSaidaPronto, BlkLenMax, qdeEnviada);
    end;
  end;

  if ((BufferSaidaQde > 0) and enviarTudo) then
  begin
    while (BufferSaidaQde < BlkLenMax) do
    begin
      BufferSaidaCru[BufferSaidaQde] := $FF;
      inc(BufferSaidaQde);
    end;

    BufferSaidaQde := 0;
    Criptografar(HashEmBytes, BufferSaidaCru, BufferSaidaPronto, BlkLenMax);
    EnviarBytes(BufferSaidaPronto, BlkLenMax, qdeEnviada);
  end;
end;

procedure sincronizaPacotes(qdeDeDadosDoUltimoEnvio: integer);
var
  tamComplemento: integer;
  bytesEnviadosAMais: integer;
  i: integer;

begin
  bytesEnviadosAMais := BlkLenMax - (qdeDeDadosDoUltimoEnvio mod BlkLenMax);
  tamComplemento := TAM_PACOTE_CABECALHO - bytesEnviadosAMais;

  if (tamComplemento >= TAM_PACOTE_CABECALHO) then
  begin
    tamComplemento := 0;
  end;

  if (tamComplemento > 0) then
  begin
    for i := 0 to (tamComplemento - 1) do // Inicializa o buffer antes de enviá-lo.
    begin
      BufferDeEnvio[i] := $FF;
    end;

    EnviarBuffer(BufferDeEnvio, tamComplemento, false);
  end;
end;

procedure EnviaBlocoVazio;
var
  i: integer;
  qdeEnviada: integer;
begin
  for i := 0 to (BlkLenMax - 1) do
  begin
    BufferSaidaCru[i] := $FF;
  end;

  EnviarBytes(BufferSaidaCru, BlkLenMax, qdeEnviada);

  //@todo Implementar o retorno de um erro aqui, em vez de imprimir na tela o problema.
  //if (qdeEnviada <> BlkLenMax) then ShowMessage('EnviaBlocoVazio (enviou somente +' + intToStr(qdeEnviada) + ' bytes)');
end;

function EnviarDigital(pis: PChar; atual, total: byte; digital: PChar): integer; stdcall;
var
  qdeBytes: integer;
  qdeEnviada: integer;
  qdeBytesRecebidos: integer;
  DadosRecebidos: String;
  i: integer;
  codErro: integer;
begin
  codErro := COD_ERRO_OK;

  if (1 = atual) then // Está iniciando um novo envio de digitais?
  begin
    strHexToArrayByte(pis, PisEmBytes, TAM_PIS_BYTES);
    qdeBytes := montaCabecalhoPacoteGbx(total, PisEmBytes, BufferDeEnvio);

    if (qdeBytes > 0) then // Tudo OK até aqui?
    begin
      EnviarBuffer(BufferDeEnvio, qdeBytes, true);
      EnviaBlocoVazio; // Necessário para o REP reconhecer tudo corretamente (visto empiricamente).

      codErro := ReceberBytes(BufferDeRecepcao, TAM_PACOTE_CABECALHO, qdeBytesRecebidos);
      if (COD_ERRO_OK = codErro) then // Recebeu a quantidade de bytes esperada do REP?
      begin
        Descriptografar(HashEmBytes, BufferDeRecepcao, BufferDeRecepcaoDescrip, qdeBytesRecebidos);

        codErro := verificaRetornoCabecalhoPacoteGbx(BufferDeRecepcaoDescrip);

        if (COD_ERRO_OK = codErro) then // O pacote recebido está OK?
        begin
          sincronizaPacotes(TAM_PACOTE_CABECALHO);
        end
        else
        begin
        end;
      end;
    end
    else // Erro durante a montagem do cabeçalho do pacote?
    begin
      codErro := COD_ERRO_MONTAGEM_DADOS;
    end;
  end;

  if (COD_ERRO_OK = codErro) then // Tudo certo até aqui?
  begin
    codErro := COD_ERRO_RESPOSTA_DO_REP; // @todo Melhorar essa forma de sinalizar o erro final.

    strHexToArrayByte(digital, DigitalEmBytes, TAM_DIGITAL);

    qdeBytes := montaFrameGbx(atual, total, DigitalEmBytes, BufferDeEnvio);

    if (qdeBytes > 0) then // Tudo OK até aqui?
    begin
      EnviarBuffer(BufferDeEnvio, qdeBytes, true);

      codErro := ReceberBytes(BufferDeRecepcao, TAM_PACOTE_CABECALHO, qdeBytesRecebidos);

      if (COD_ERRO_OK = codErro) then // Recebeu os dados aguardados do REP?
      begin
        Descriptografar(HashEmBytes, BufferDeRecepcao, BufferDeRecepcaoDescrip, qdeBytesRecebidos);

        codErro := verificaRetornoFrameGbx(BufferDeRecepcaoDescrip);

        if (COD_ERRO_OK <> codErro) then // O retorno do frame não está OK?
        begin
        end;
      end
      else
      begin
        codErro := COD_ERRO_RESPOSTA_DO_REP; // @todo Melhorar essa forma de sinalizar o erro final.
      end;
    end;
  end;

  result := codErro;
end;

function AbrirComunicacaoComRep(ip: PChar; porta: integer; timeoutMs: integer; hash: PChar): integer; stdcall;
begin
  strHexToArrayByte(hash, HashEmBytes, KeyLenMax);
  TimeoutComunicacaoMs := timeoutMs;

  result := AbrirComunicacao(ip, porta, TimeoutComunicacaoMs);
end;

function ComunicacaoAbertaComRep: boolean; stdcall;
begin
  result := ComunicacaoAberta();
end;

function FecharComunicacaoComRep: integer; stdcall;
begin
  result := FecharComunicacao();
end;

function montaComandoCc(var saida: array of byte): integer;
var
  qdeBytes: integer;

begin
  if (Length(saida) < TAM_PACOTE_CABECALHO) then // Não há espaço suficiente no array "saida"?
  begin
    qdeBytes := 0;
  end
  else // Espaço suficiente no array "saida"?
  begin

    try
      // Cabeçalho da comunicação de modo geral ------------------------------
      // Start.
      saida[0] := $0A;
      // Endereço.
      saida[1] := $00;
      saida[2] := $00;
      saida[3] := $00;
      saida[4] := $00;
      // Comando (CC).
      saida[5] := $80;
      // Parâmetro.
      saida[6] := $00;
      saida[7] := $00;
      saida[8] := $00;
      saida[9] := $00;
      // Tamanho.
      saida[10] := $00;
      saida[11] := $00;
      saida[12] := $00;
      saida[13] := $00;
      // CPF (em BCD).
      saida[14] := $00;
      saida[15] := $00;
      saida[16] := $00;
      saida[17] := $00;
      saida[18] := $00;
      saida[19] := $00;
      // Flag/Error.
      saida[20] := $00;
      // BCC.
      saida[21] := calculaBcc(saida, 0, 21);
      // End.
      saida[22] := $40;

      qdeBytes := TAM_PACOTE_CABECALHO;
    except
      qdeBytes := 0;
    end;
  end;
  result := qdeBytes;
end;

function verificaRetornoComandoCc(retorno: array of byte): integer;
var
  codErro: integer;
begin
  if (Length(retorno) < TAM_PACOTE_CABECALHO) then // A quantidade de posições do "retorno" é inconsistente?
  begin
    codErro := COD_ERRO_INCONSISTENCIA;
  end
  else // A quantidade de posições do "retorno" é consistente?
  begin
    if (retorno[21] = calculaBcc(retorno, 0, 21)) then // O BCC do pacote está consistente?
    begin
      if (// Start.
        (retorno[0] = $3D)
        // Endereço.
        and (retorno[1] = $00)
        and (retorno[2] = $00)
        and (retorno[3] = $00)
        and (retorno[4] = $00)
        // Comando (CC).
        and (retorno[5] = $80)
        // Parâmetro.
        and (retorno[6] = $00)
        and (retorno[7] = $00)
        and (retorno[8] = $00)
        and (retorno[9] = $01)
        // Tamanho.
        and (retorno[10] = $00)
        and (retorno[11] = $00)
        and (retorno[12] = $00)
        and (retorno[13] = $38)
        // CPF. Obs.: vem tudo zerado neste momento.
        and (retorno[14] = $00)
        and (retorno[15] = $00)
        and (retorno[16] = $00)
        and (retorno[17] = $00)
        and (retorno[18] = $00)
        and (retorno[19] = $00)
        // End.
        and (retorno[22] = $40)) then // O pacote está como esperado?
      begin
        if ($54 = retorno[20]) then // O campo "Flag/Error" está sinalizando SUCESSO?
        begin          
          if (retorno[53] = calculaBcc(retorno, 32, 21)) then // O BCC do pacote está consistente?
          begin
            if (// Start.
              (retorno[32] = $3D)
              // Endereço.
              and (retorno[33] = $00)
              and (retorno[34] = $00)
              and (retorno[35] = $00)
              and (retorno[36] = $00)
              // Comando (CC).
              and (retorno[37] = $80)
              // Parâmetro.
              and (retorno[38] = $00)
              and (retorno[39] = $01)
              and (retorno[40] = $00)
              and (retorno[41] = $01)
              // Tamanho.
              and (retorno[42] = $00)
              and (retorno[43] = $00)
              and (retorno[44] = $00)
              and (retorno[45] = $20)
              // CPF. Obs.: vem tudo zerado neste momento.
              and (retorno[46] = $00)
              and (retorno[47] = $00)
              and (retorno[48] = $00)
              and (retorno[49] = $00)
              and (retorno[50] = $00)
              and (retorno[51] = $00)            
              // End.
              and (retorno[54] = $40)) then // O pacote está como esperado?          
            begin
              if ($45 = retorno[52]) then // O campo "Flag/Error" está sinalizando SUCESSO?
              begin
                codErro := COD_ERRO_OK;
              end
              else
              begin
                codErro := retorno[52]; // Sinaliza com o mesmo código de erro enviado pelo REP.
              end;            
            end
            else // Há algum problema na formação esperada do pacote?
            begin
              codErro := COD_ERRO_FORMATO_DOS_DADOS;
            end;
          end
          else // O BCC do pacote está errado?
          begin
            codErro := COD_ERRO_BCC_INVALIDO;
          end;                    
        end
        else
        begin
          codErro := retorno[20]; // Sinaliza com o mesmo código de erro enviado pelo REP.
        end;
      end
      else // Há algum problema na formação esperada do pacote?
      begin
        codErro := COD_ERRO_FORMATO_DOS_DADOS;
      end;
    end
    else // O BCC do pacote está errado?
    begin
      codErro := COD_ERRO_BCC_INVALIDO;
    end;
  end;

  result := codErro;
end;

function montaConfirmacaoComandoCc(var saida: array of byte): integer;
var
  qdeBytes: integer;

begin
  if (Length(saida) < TAM_PACOTE_CABECALHO) then // Não há espaço suficiente no array "saida"?
  begin
    qdeBytes := 0;
  end
  else // Espaço suficiente no array "saida"?
  begin

    try
      // Cabeçalho da comunicação de modo geral ------------------------------
      // Start.
      saida[0] := $0A;
      // Endereço.
      saida[1] := $00;
      saida[2] := $00;
      saida[3] := $00;
      saida[4] := $00;
      // Comando (CC).
      saida[5] := $80;
      // Num. total de frames
      saida[6] := $00;
      saida[7] := $01;
      // Num. atual de frames
      saida[8] := $00;
      saida[9] := $01;
      // Tamanho.
      saida[10] := $00;
      saida[11] := $00;
      saida[12] := $00;
      saida[13] := $20;
      // CPF (em BCD).
      saida[14] := $00;
      saida[15] := $00;
      saida[16] := $00;
      saida[17] := $00;
      saida[18] := $00;
      saida[19] := $00;
      // Flag/Error.
      saida[20] := $45;
      // BCC.
      saida[21] := calculaBcc(saida, 0, 21);
      // End.
      saida[22] := $40;

      qdeBytes := TAM_PACOTE_CABECALHO;
    except
      qdeBytes := 0;
    end;
  end;
  result := qdeBytes;
end;

function montaComandoKey(var saida: array of byte): integer;
var
  qdeBytes: integer;

begin
  if (Length(saida) < TAM_PACOTE_CABECALHO) then // Não há espaço suficiente no array "saida"?
  begin
    qdeBytes := 0;
  end
  else // Espaço suficiente no array "saida"?
  begin

    try
      // Cabeçalho da comunicação de modo geral ------------------------------
      // Start.
      saida[0] := $0A;
      // Endereço.
      saida[1] := $00;
      saida[2] := $00;
      saida[3] := $00;
      saida[4] := $00;
      // Comando (KEY).
      saida[5] := $81;
      // Parâmetro.
      saida[6] := $00;
      saida[7] := $00;
      saida[8] := $00;
      saida[9] := $00;
      // Tamanho.
      saida[10] := $00;
      saida[11] := $00;
      saida[12] := $00;
      saida[13] := $00;
      // CPF (em BCD).
      saida[14] := $00;
      saida[15] := $00;
      saida[16] := $00;
      saida[17] := $00;
      saida[18] := $00;
      saida[19] := $00;
      // Flag/Error.
      saida[20] := $00;
      // BCC.
      saida[21] := calculaBcc(saida, 0, 21);
      // End.
      saida[22] := $40;

      qdeBytes := TAM_PACOTE_CABECALHO;
    except
      qdeBytes := 0;
    end;
  end;
  result := qdeBytes;
end;

function verificaRetornoComandoKey(retorno: array of byte): integer;
var
  codErro: integer;
begin
  if (Length(retorno) < TAM_PACOTE_CABECALHO) then // A quantidade de posições do "retorno" é inconsistente?
  begin
    codErro := COD_ERRO_INCONSISTENCIA;
  end
  else // A quantidade de posições do "retorno" é consistente?
  begin
    if (retorno[21] = calculaBcc(retorno, 0, 21)) then // O BCC do pacote está consistente?
    begin
      if (// Start.
            (retorno[0] = $3D)
        // Endereço.
        and (retorno[1] = $00)
        and (retorno[2] = $00)
        and (retorno[3] = $00)
        and (retorno[4] = $00)
        // Comando (KEY).
        and (retorno[5] = $81)
        // Parâmetro
        and (retorno[6] = $00)
        and (retorno[7] = $00)
        and (retorno[8] = $00)
        and (retorno[9] = $00)
        // Tamanho
        and (retorno[10] = $00)
        and (retorno[11] = $00)
        and (retorno[12] = $00)
        and (retorno[13] = $00)
        // CPF. Obs.: vem tudo zerado neste momento.
        and (retorno[14] = $00)
        and (retorno[15] = $00)
        and (retorno[16] = $00)
        and (retorno[17] = $00)
        and (retorno[18] = $00)
        and (retorno[19] = $00)
        // End.
        and (retorno[22] = $40)) then // O pacote está como esperado?
      begin
        if ($54 = retorno[20]) then // O REP sinalizou sucesso SUCESSO?
        begin
          codErro := COD_ERRO_OK;
        end
        else // O REP sinalizou algum erro?
        begin
          codErro := retorno[20]; // Sinaliza com o mesmo código de erro enviado pelo REP.
        end;
      end
      else // Há algum problema na formação esperada do pacote?
      begin
        codErro := COD_ERRO_FORMATO_DOS_DADOS;
      end;
    end
    else // O BCC do pacote está errado?
    begin
      codErro := COD_ERRO_BCC_INVALIDO;
    end;
  end;

  result := codErro;
end;

function GerarHash: PChar; stdcall;
var
  hash: PChar;
  StringAux: String;
  qdeBytes: integer;
  qdeBytesRecebidos: integer;
  codErro: integer;
begin
  hash := '0000000000000000000000000000000000000000000000000000000000000000';
  strHexToArrayByte(hash, HashEmBytes, KeyLenMax); // Define o hash global com todos os bytes zerados, pois vai gerar uma nova chave.

  qdeBytes := montaComandoCc(BufferDeEnvio);

  if (qdeBytes > 0) then // Tudo OK até aqui?
  begin
    EnviarBuffer(BufferDeEnvio, qdeBytes, true);
    //EnviaBlocoVazio; // Necessário para o REP reconhecer tudo corretamente (visto empiricamente).

    codErro := ReceberBytes(BufferDeRecepcao, (TAM_PACOTE_CABECALHO*2) + KeyLenMax + 1, qdeBytesRecebidos); // Retorna: pacote normal + hash + byte final.
    if (COD_ERRO_OK = codErro) then // Recebeu a quantidade de bytes esperada do REP?
    begin
      Descriptografar(HashEmBytes, BufferDeRecepcao, BufferDeRecepcaoDescrip, qdeBytesRecebidos);

      codErro := verificaRetornoComandoCc(BufferDeRecepcaoDescrip);

      if (COD_ERRO_OK = codErro) then // O pacote recebido está OK?
      begin
        // Extrai o hash gerado, recebido do REP, mantendo-o em "StringAux".
        StringAux := intToHex(BufferDeRecepcaoDescrip[55],2)
               + intToHex(BufferDeRecepcaoDescrip[56],2)
               + intToHex(BufferDeRecepcaoDescrip[57],2)
               + intToHex(BufferDeRecepcaoDescrip[58],2)
               + intToHex(BufferDeRecepcaoDescrip[59],2)
               + intToHex(BufferDeRecepcaoDescrip[60],2)
               + intToHex(BufferDeRecepcaoDescrip[61],2)
               + intToHex(BufferDeRecepcaoDescrip[62],2)
               + intToHex(BufferDeRecepcaoDescrip[63],2)
               + intToHex(BufferDeRecepcaoDescrip[64],2)
               + intToHex(BufferDeRecepcaoDescrip[65],2)
               + intToHex(BufferDeRecepcaoDescrip[66],2)
               + intToHex(BufferDeRecepcaoDescrip[67],2)
               + intToHex(BufferDeRecepcaoDescrip[68],2)
               + intToHex(BufferDeRecepcaoDescrip[69],2)
               + intToHex(BufferDeRecepcaoDescrip[70],2)
               + intToHex(BufferDeRecepcaoDescrip[71],2)
               + intToHex(BufferDeRecepcaoDescrip[72],2)
               + intToHex(BufferDeRecepcaoDescrip[73],2)
               + intToHex(BufferDeRecepcaoDescrip[74],2)
               + intToHex(BufferDeRecepcaoDescrip[75],2)
               + intToHex(BufferDeRecepcaoDescrip[76],2)
               + intToHex(BufferDeRecepcaoDescrip[77],2)
               + intToHex(BufferDeRecepcaoDescrip[78],2)
               + intToHex(BufferDeRecepcaoDescrip[79],2)
               + intToHex(BufferDeRecepcaoDescrip[80],2)
               + intToHex(BufferDeRecepcaoDescrip[81],2)
               + intToHex(BufferDeRecepcaoDescrip[82],2)
               + intToHex(BufferDeRecepcaoDescrip[83],2)
               + intToHex(BufferDeRecepcaoDescrip[84],2)
               + intToHex(BufferDeRecepcaoDescrip[85],2)
               + intToHex(BufferDeRecepcaoDescrip[86],2);

        // Envia a confirmação da recepção do CC para o REP.
        //sincronizaPacotes(qdeBytes);
        qdeBytes := montaConfirmacaoComandoCc(BufferDeEnvio);
        
        if (qdeBytes > 0) then // O pacote de confirmação foi montado com sucesso?
        begin
          EnviarBuffer(BufferDeEnvio, qdeBytes, false); // Bufferiza a confirmação (Ack) do CC.

          // Confirma o hash recebido.
          //sincronizaPacotes(qdeBytes);
          qdeBytes := montaComandoKey(BufferDeEnvio);

          if (qdeBytes > 0) then // Tudo OK até aqui?
          begin
            EnviarBuffer(BufferDeEnvio, qdeBytes, true); // Mandar Ack do CC e comando Key.
            //EnviaBlocoVazio; // Necessário para o REP reconhecer tudo corretamente (visto empiricamente).
            codErro := ReceberBytes(BufferDeRecepcao, TAM_PACOTE_CABECALHO, qdeBytesRecebidos);
            //sincronizaPacotes(TAM_PACOTE_CABECALHO);

            if (COD_ERRO_OK = codErro) then // Recebeu a quantidade de bytes esperada do REP?
            begin
              Descriptografar(HashEmBytes, BufferDeRecepcao, BufferDeRecepcaoDescrip, qdeBytesRecebidos);

              codErro := verificaRetornoComandoKey(BufferDeRecepcaoDescrip);

              if (COD_ERRO_OK = codErro) then // O pacote recebido está OK?
              begin
                hash := PChar(StringAux); // Define o hash a ser retornado.
                strHexToArrayByte(hash, HashEmBytes, KeyLenMax); // Define, temporariamente, pelo menos, o hash global com o hash gerado pelo REP.
              end
              else
              begin
              end;
            end
            else
            begin
              codErro := COD_ERRO_RESPOSTA_DO_REP; // O REP não retornou os bytes aguardados.
            end;
          end
          else // Erro durante a montagem do cabeçalho do pacote?
          begin
            codErro := COD_ERRO_MONTAGEM_DADOS;
          end;
          
        end
        else
        begin
          codErro := COD_ERRO_MONTAGEM_DADOS;
        end;        
      end
      else
      begin
        // Manda o pacote de confirmação (sinalizando sucesso mesmo) somente para destravar o REP.
        qdeBytes := montaConfirmacaoComandoCc(BufferDeEnvio);        
        if (qdeBytes > 0) then // O pacote de confirmação foi montado com sucesso?
        begin
          EnviarBuffer(BufferDeEnvio, qdeBytes, true);        
        end;
      end;
    end
    else
    begin
      codErro := COD_ERRO_RESPOSTA_DO_REP; // O REP não retornou os bytes aguardados.
    end;
  end
  else // Erro durante a montagem do cabeçalho do pacote?
  begin
    codErro := COD_ERRO_MONTAGEM_DADOS;
  end;

  result := hash;
end;

exports
  AbrirComunicacaoComRep,
  ComunicacaoAbertaComRep,
  EnviarDigital,
  FecharComunicacaoComRep,
  GerarHash;

begin
end.

