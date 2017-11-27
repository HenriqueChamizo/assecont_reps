unit uCodErro;

interface

const
  // Códigos padrões da DLL
  COD_ERRO_OK                  = 0;
  COD_ERRO_BCC_INVALIDO        = 1;
  COD_ERRO_TIMEOUT             = 300;
  COD_ERRO_INCONSISTENCIA      = 301;
  COD_ERRO_COMUNICACAO_INATIVA = 302;
  COD_ERRO_RESPOSTA_DO_REP     = 303;
  COD_ERRO_DESCRIPTOGRAFIA     = 304;
  COD_ERRO_MONTAGEM_DADOS      = 305;
  COD_ERRO_CONEXAO             = 306;
  COD_ERRO_ENVIO               = 307;
  COD_ERRO_FORMATO_DOS_DADOS   = 308;
  COD_ERRO_INESPERADO          = 320;
  // Códigos adicionais
{
public static final int SUCESSO = 0;
public static final int BCC_INVALIDO = 1;
public static final int HORA_INVALIDA = 2;
public static final int PARAMETRO_TAMANHO_FLAGERROR_NAO_SUPORTADOS = 3;
public static final int COMANDO_NAO_SUPORTADO_OU_DESCONHECIDO = 4;
public static final int ERRO_NAO_ESPECIFICADO = 5;
public static final int PIS_INEXISTENTE = 8;
public static final int IDENTIFICADOR_INCONSISTENTE = 10;
public static final int IDENTIFICADOR_RECUSADO = 11;
public static final int CODIGO_RECUSADO = 12;
public static final int ESPACO_INSUFICIENTE = 13;
public static final int FRAME_COM_ERRO = 96;
/* CÃ³digos adicionais */
public static final int COMUNICACAO_NAO_ESTABELECIDA = 200;
public static final int EMPREGADOR_NAO_CADASTRADO = 201;
public static final int OCORREU_EXCECAO = 202;
public static final int RELOGIO_NAO_AJUSTADO = 203;
public static final int HVERAO_NAO_AJUSTADO = 204;
public static final int NENHUM_FUNCIONARIO_CADASTRADO = 205;
public static final int NENHUMA_MARCACAO_PONTO = 206;
public static final int RETORNO_INCONSISTENTE = NENHUMA_MARCACAO_PONTO + 1;
} 

implementation

end.
