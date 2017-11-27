using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioproxC.RWTech.ProtocoloREP
{
    public static class Protocolo
    {
        public static int LIMITE_TENTATIVAS = 3;
        public static int SEGUNDOS_INTERVALO_TENTATIVAS = 5;

        public static int INDICE_COMANDO_RETORNO = 5;
        public static int INDICE_INICIO_PARAMETRO = 6;
        public static int INDICE_FIM_PARAMETRO = 9;
        public static int INDICE_INICIO_TAMANHO = 10;
        public static int INDICE_FIM_TAMANHO = 13;
        public static int INDICE_FLAG_RETORNO = 20;
        public static int INDICE_BCC = 21;
        public static int QTD_BYTES_CABECALHO_CRIPTOGRAFADO = 32;
        public static int QTD_BYTES_CABECALHO_DADOS = 23;
        // Constantes para Empregador 
        public static int QTD_BYTES_RETORNO_LEITURA_EMPREGADOR = 320;
        public static int INDICE_TIPO_IDENTIFICADOR_EMPREGADOR = 55;
        public static int INDICE_IDENTIFICADOR_EMPREGADOR = 56;
        public static int TAMANHO_IDENTIFICADOR_EMPREGADOR = 6;
        public static int INDICE_CEI_EMPREGADOR = 62;
        public static int TAMANHO_CEI_EMPREGADOR = 5;
        public static int INDICE_RAZAO_SOCIAL_EMPREGADOR = 67;
        public static int TAMANHO_RAZAO_SOCIAL_EMPREGADOR = 150;
        public static int INDICE_LOCAL_EMPREGADOR = 217;
        public static int TAMANHO_LOCAL_EMPREGADOR = 100;
        // Constantes para Digital
        public static int QTD_BYTES_TEMPLATES = 803;
        public static int QTD_BYTES_PRIMEIRA_TEMPLATE = 403;
        public static int QTD_BYTES_SEGUNDA_TEMPLATE = 400;
        public static int QTD_BYTES_ADICIONAIS_DIGITAL = 2;
        public static int QTD_BYTES_DIGITAL = 832;
        public static int QTD_BYTES_FIXOS_DIGITAL = 3;
        public static int INDICE_QTD_DIGITAIS_A_SEREM_LIDAS = 9;
        // Constantes para Funcionário
        public static int QTD_BYTES_FUNCIONARIO = 128;
        public static int INDICE_NOME_FUNCIONARIO = 0;
        public static int TAMANHO_NOME_FUNCIONARIO = 52;
        public static int INDICE_PIS_FUNCIONARIO = 52;
        public static int TAMANHO_PIS_FUNCIONARIO = 6;
        public static int INDICE_ID_BIO = 58;
        public static int TAMANHO_ID_BIO = 4;
        public static int INDICE_CARTAO_FUNCIONARIO = 62;
        public static int TAMANHO_CARTAO_FUNCIONARIO = 20;
        public static int INDICE_CODIGO_FUNCIONARIO = 82;
        public static int TAMANHO_CODIGO_FUNCIONARIO = 3;
        public static int INDICE_MESTRE_FUNCIONARIO = 85;
        public static int TAMANHO_MESTRE_FUNCIONARIO = 1;
        public static int INDICE_SENHA_FUNCIONARIO = 86;
        public static int TAMANHO_SENHA_FUNCIONARIO = 3;
        public static int INDICE_VERIFICAR_1_PRA_N = 89;
        public static int TAMANHO_VERIFICAR_1_PRA_N_FUNCIONARIO = 1;
        // Constantes para Marcação de Ponto
        public static int TAMANHO_PACOTE_MARCACOES = 10;
        public static int QTD_BYTES_MARCACAO = 19;
        public static int QTD_BYTES_PACOTES_MARCACOES = 224;

        public static int TIMEOUT = 10000;
    }
}
