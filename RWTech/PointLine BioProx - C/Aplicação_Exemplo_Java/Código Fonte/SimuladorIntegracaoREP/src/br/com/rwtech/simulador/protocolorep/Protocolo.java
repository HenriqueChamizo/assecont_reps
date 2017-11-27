package br.com.rwtech.simulador.protocolorep;

public class Protocolo {
	
	public static final int LIMITE_TENTATIVAS = 3;
	public static final int SEGUNDOS_INTERVALO_TENTATIVAS = 5;
	
	public static final int INDICE_COMANDO_RETORNO = 5;
	public static final int INDICE_INICIO_PARAMETRO = 6;
	public static final int INDICE_FIM_PARAMETRO = 9;
	public static final int INDICE_INICIO_TAMANHO = 10;
	public static final int INDICE_FIM_TAMANHO = 13;
	public static final int INDICE_FLAG_RETORNO = 20;
	public static final int INDICE_BCC = 21;
	public static final int QTD_BYTES_CABECALHO_CRIPTOGRAFADO = 32;
	public static final int QTD_BYTES_CABECALHO_DADOS = 23;
	// Constantes para Empregador 
	public static final int QTD_BYTES_RETORNO_LEITURA_EMPREGADOR = 320;
	public static final int INDICE_TIPO_IDENTIFICADOR_EMPREGADOR = 55;
	public static final int INDICE_IDENTIFICADOR_EMPREGADOR = 56;
	public static final int TAMANHO_IDENTIFICADOR_EMPREGADOR = 6;
	public static final int INDICE_CEI_EMPREGADOR = 62;
	public static final int TAMANHO_CEI_EMPREGADOR = 5;
	public static final int INDICE_RAZAO_SOCIAL_EMPREGADOR = 67;
	public static final int TAMANHO_RAZAO_SOCIAL_EMPREGADOR = 150;
	public static final int INDICE_LOCAL_EMPREGADOR = 217;
	public static final int TAMANHO_LOCAL_EMPREGADOR = 100;
	// Constantes para Digital
	public static final int QTD_BYTES_TEMPLATES = 803;
	public static final int QTD_BYTES_PRIMEIRA_TEMPLATE = 403;
	public static final int QTD_BYTES_SEGUNDA_TEMPLATE = 400;
	public static final int QTD_BYTES_ADICIONAIS_DIGITAL = 2;
	public static final int QTD_BYTES_DIGITAL = 832;
	public static final int QTD_BYTES_FIXOS_DIGITAL = 3; 
	public static final int INDICE_QTD_DIGITAIS_A_SEREM_LIDAS = 9;
	// Constantes para Funcionário
	public static final int QTD_BYTES_FUNCIONARIO = 128;
	public static final int INDICE_NOME_FUNCIONARIO = 0;
	public static final int TAMANHO_NOME_FUNCIONARIO = 52;
	public static final int INDICE_PIS_FUNCIONARIO = 52;
	public static final int TAMANHO_PIS_FUNCIONARIO = 6;
	public static final int INDICE_ID_BIO = 58;
	public static final int TAMANHO_ID_BIO = 4;
	public static final int INDICE_CARTAO_FUNCIONARIO = 62;
	public static final int TAMANHO_CARTAO_FUNCIONARIO = 20;
	public static final int INDICE_CODIGO_FUNCIONARIO = 82;
	public static final int TAMANHO_CODIGO_FUNCIONARIO = 3;
	public static final int INDICE_MESTRE_FUNCIONARIO = 85;
	public static final int TAMANHO_MESTRE_FUNCIONARIO = 1;
	public static final int INDICE_SENHA_FUNCIONARIO = 86;
	public static final int TAMANHO_SENHA_FUNCIONARIO = 3;
	public static final int INDICE_VERIFICAR_1_PRA_N = 89;
	public static final int TAMANHO_VERIFICAR_1_PRA_N_FUNCIONARIO = 1;
	// Constantes para Marcação de Ponto
	public static final int TAMANHO_PACOTE_MARCACOES = 10;
	public static final int QTD_BYTES_MARCACAO = 19;
	public static final int QTD_BYTES_PACOTES_MARCACOES = 224;
	
	public static final int TIMEOUT = 10000;
	
}


