/*****************************************************************************
* Aplicativo : 	???????	                                                     *
* Autor	     : Ana Lúcia S. Melo (para esta classe)    	     			     *
* Referência : Programa para coleta do REP                                   *
* Data       : //2011						                                 *
* Ambiente   : Visual C# 2008                    						     *
* Fonte      : clRetornaErro                   		  	                     *
* Objetivo   : Responsável pela informação especifica do erro retornado pela * 
*              dll.                                                          *
******************************************************************************/
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace REP_Prototipo
{
    class clRetornaErro
    {
        public string retornoREP(int valor)
        {
            string retorno = "";

            switch (valor)
            {
                //--------------------------------------------------------------
                // Serial Port Errors
                //--------------------------------------------------------------
                case 101: retorno = "Erro: Porta serial aberta."; break;
                case 102: retorno = "Erro: Porta serial fechada."; break;
                case 103: retorno = "Erro ao definir máscara da porta serial."; break;
                case 104: retorno = "Erro ao obter máscara da porta serial."; break;
                case 105: retorno = "Erro ao obter o estado da porta."; break;
                case 106: retorno = "Erro ao definir o estado da porta."; break;
                case 107: retorno = "Erro ao obter tempo limite da porta."; break;
                case 108: retorno = "Erro ao definir tempo limite da porta."; break;
                case 109: retorno = "Erro: Tamanho dos bytes."; break;
                case 110: retorno = "Erro: Paridade."; break;
                case 111: retorno = "Erro: Bits de parada."; break;
                case 112: retorno = "Erro ao limpar porta serial."; break;
                case 113: retorno = "Erro ao definir os bytes da porta."; break;
                case 114: retorno = "Erro ao receber os bytes da porta."; break;
                case 115: retorno = "Erro ao definir sequencia."; break;
                case 116: retorno = "Erro ao obter sequencia."; break;
                case 117: retorno = "Erro: Tempo expirado."; break;
                case 118: retorno = "Erro: A serial não foi aberta."; break;
                case 119: retorno = "Erro: A serial já está aberta."; break;
                case 120: retorno = "Erro: Numero da porta serial inválido."; break;                           

                //--------------------------------------------------------------
                //Erro de protocolo
                case 201: retorno = "Erro no tamanho da mensagem."; break;
                case 202: retorno = "Erro na mensagem do protocolo."; break;
                case 203: retorno = "Mensagem cancelada."; break;
                case 204: retorno = "Erro ao receber mensagem."; break;
                //Erros desconhecidos
                case 1021: retorno = " Operação sem sucesso."; break;
                case 1022: retorno = " Resposta desconhecida."; break;

                //Erros de paramentros/ funções
                case 1030: retorno = " Erro na manipulação do arquivo."; break;
                case 1031: retorno = " Filtro não executado."; break;
                /*case 1050: retorno = " Tamanho do 1o. parâmetro é inválido."; break;
                case 1051: retorno = " Tamanho do 2o. parâmetro é inválido."; break;
                case 1052: retorno = " Tamanho do 3o. parâmetro é inválido."; break;
                case 1053: retorno = " Tamanho do 4o. parâmetro é inválido."; break;
                case 1054: retorno = " Tamanho do 5o. parâmetro é inválido."; break;
                case 1055: retorno = " Tamanho do 6o. parâmetro é inválido."; break;
                case 1056: retorno = " Tamanho do 7o. parâmetro é inválido."; break;
                case 1057: retorno = " Tamanho do 8o. parâmetro é inválido."; break;
                case 1058: retorno = " Tamanho do 9o. parâmetro é inválido."; break;
                case 1059: retorno = " Tamanho do case 10o. parâmetro é inválido."; break;
                case 1060: retorno = " Tamanho do 11o. parâmetro é inválido."; break;
                case 1061: retorno = " Tamanho do 12o. parâmetro é inválido."; break;
                case 1062: retorno = " Tamanho do 13o. parâmetro é inválido."; break;
                case 1063: retorno = " Tamanho do 14o. parâmetro é inválido."; break;
                case 1064: retorno = " Tamanho do 15o. parâmetro é inválido."; break;
                case 1065: retorno = " Tamanho do 16o. parâmetro é inválido."; break;
                case 1066: retorno = " Tamanho do 17o. parâmetro é inválido."; break;
                case 1067: retorno = " Tamanho do 18o. parâmetro é inválido."; break;
                case 1068: retorno = " Tamanho do 19o. parâmetro é inválido."; break;
                case 1069: retorno = " Tamanho do 20o. parâmetro é inválido."; break;
                case 1070: retorno = " Tamanho do 21o. parâmetro é inválido."; break;
                case 1071: retorno = " Tamanho do 22o. parâmetro é inválido."; break;
                case 1072: retorno = " Tamanho do 23o. parâmetro é inválido."; break;
                case 1073: retorno = " Tamanho do 24o. parâmetro é inválido."; break;
                case 1074: retorno = " Tamanho do 25o. parâmetro é inválido."; break;
                case 1075: retorno = " Valor do 1o. parâmetro é inválido."; break;
                case 1076: retorno = " Valor do 2o. parâmetro é inválido."; break;
                case 1077: retorno = " Valor do 3o. parâmetro é inválido."; break;
                case 1078: retorno = " Valor do 4o. parâmetro é inválido."; break;
                case 1079: retorno = " Valor do 5o. parâmetro é inválido."; break;
                case 1080: retorno = " Valor do 6o. parâmetro é inválido."; break;
                case 1081: retorno = " Valor do 7o. parâmetro é inválido."; break;
                case 1082: retorno = " Valor do 8o. parâmetro é inválido."; break;
                case 1083: retorno = " Valor do 9o. parâmetro é inválido."; break;
                case 1084: retorno = " Valor do 10o. parâmetro é inválido."; break;
                case 1085: retorno = " Valor do 11o. parâmetro é inválido."; break;
                case 1086: retorno = " Valor do 12o. parâmetro é inválido."; break;
                case 1087: retorno = " Valor do 13o. parâmetro é inválido."; break;
                case 1088: retorno = " Valor do 14o. parâmetro é inválido."; break;
                case 1089: retorno = " Valor do 15o. parâmetro é inválido."; break;
                case 1090: retorno = " Valor do 16o. parâmetro é inválido."; break;
                case 1091: retorno = " Valor do 17o. parâmetro é inválido."; break;
                case 1092: retorno = " Valor do 18o. parâmetro é inválido."; break;
                case 1093: retorno = " Valor do 19o. parâmetro é inválido."; break;
                case 1094: retorno = " Valor do 20o. parâmetro é inválido."; break;
                case 1095: retorno = " Valor do 21o. parâmetro é inválido."; break;
                case 1096: retorno = " Valor do 22o. parâmetro é inválido."; break;
                case 1097: retorno = " Valor do 23o. parâmetro é inválido."; break;
                case 1098: retorno = " Valor do 24o. parâmetro é inválido."; break;
                case 1099: retorno = " Valor do 25o. parâmetro é inválido."; break;*/
                //--------------------------------------------------------------
                // Erros de Empregador
                //--------------------------------------------------------------
                case 2000: retorno = " Nome do empregador inválido."; break;
                case 2001: retorno = " Local inválido."; break;
                case 2002: retorno = " CNPJ inválido."; break;
                case 2003: retorno = " CEI inválido."; break;

                case 2010: retorno = " Tamanho do nome empregador inválido."; break;
                case 2011: retorno = " Tamanho do local inválido."; break;
                case 2012: retorno = " Tamanho do CNPJ inválido."; break;
                case 2013: retorno = " Tamanho do CEI inválido."; break;

                case 2020: retorno = " Empregador já cadastrado."; break;
                case 2021: retorno = " Empregador não cadastrado."; break;
                //--------------------------------------------------------------
                // Erros de Funcionario
                //--------------------------------------------------------------
                case 3000: retorno = " Nome do funcionário inválido."; break;
                case 3001: retorno = " PIS inválido."; break;
                case 3002: retorno = " Contactless inválido."; break;
                case 3003: retorno = " Código de barras inválido."; break;
                case 3004: retorno = " Teclado inválido."; break;
                case 3005: retorno = " Biometria inválida"; break;
                case 3006: retorno = " Grupo inválido."; break;
                
                case 3010: retorno = " Tamanho do nome do funcionário inválido."; break;
                case 3011: retorno = " Tamanho do PIS inválido."; break;
                case 3012: retorno = " Tamanho do contactless inválido."; break;
                case 3013: retorno = " Tamanho do código de barras inválido."; break;
                case 3014: retorno = " Tamanho do teclado inválido."; break;
                case 3015: retorno = " Tamanho da biometria inválida."; break;

                case 3021: retorno = " PIS já cadastrado."; break;
                case 3022: retorno = " Contactless já cadastrado."; break;
                case 3023: retorno = " Código de barras já cadastrado."; break;
                case 3024: retorno = " Teclado já cadastrado."; break;
                case 3025: retorno = " Biometria já cadastrada."; break;

                case 3030: retorno = " Funcionário já cadastrado."; break;
                case 3031: retorno = " PIS não encontrado."; break;

                case 3101: retorno = " Tamanho do PIS inválido."; break;
                case 3102: retorno = " Tamanho do CNPJ inválido."; break;
                case 3103: retorno = " Tamanho do CPF inválido."; break;
                case 3104: retorno = " Tamanho do CEI inválido."; break;
                case 3105: retorno = " Tamanho do Contactless inválido."; break;
                case 3106: retorno = " Tamanho do Código de barras inválido."; break;
                case 3107: retorno = " Tamanho do Teclado inválido."; break;
                case 3108: retorno = " Tamanho do Biometria inválido."; break;
                case 3109: retorno = " Tamanho do Nome do empregador inválido."; break;
                case 3110: retorno = " Tamanho do Nome do funcionário inválido."; break;
                case 3111: retorno = " Tamanho do Local do empregador inválido."; break;
                //Cadastro
                case 3201: retorno = " PIS já cadastrado."; break;
                case 3202: retorno = " Contactless já cadastrado."; break;
                case 3203: retorno = " Código de barras já cadastrado."; break;
                case 3204: retorno = " Teclado inválido."; break;
                case 3205: retorno = " Biometria já cadastrado."; break;
                case 3206: retorno = " Funcionário já cadastrado."; break;
                case 3207: retorno = " Empregador já cadastrado."; break;

                //--------------------------------------------------------------
                // Erros de Empregador ou Funcionário
                //--------------------------------------------------------------
                case 3300: retorno = " CPF inválido."; break;
                case 3301: retorno = " Tamanho do CPF inválido."; break;

              /*  case 3301: retorno = " PIS não encontrado."; break;
                case 3302: retorno = " Empregador não cadastrado."; break;
                case 3303: retorno = " Funcionário não cadastrado."; break;*/
                //--------------------------------------------------------------
                // Erros de Gravação
                //--------------------------------------------------------------
                case 3400: retorno = " NFR inválido."; break;
                case 3401: retorno = " Tamanho do NFR inválido."; break;
                
                //--------------------------------------------------------------
                // Erros de Data
                //--------------------------------------------------------------
                case 4000: retorno = " Dia inválido."; break;
                case 4001: retorno = " Mês inválido."; break;
                case 4002: retorno = " Ano inválido."; break;
                case 4010: retorno = " Hora inválida."; break;
                case 4011: retorno = " Minuto inválido."; break;
                case 4012: retorno = " Segundo inválido."; break;
                case 4020: retorno = " Dia da semana inválido."; break;

                //--------------------------------------------------------------
                // Erros Genéricos do REP
                //--------------------------------------------------------------
                case 4050: retorno = " Senha inválida."; break;
                case 4051: retorno = " Tamanho da senha inválido."; break;
                case 4060: retorno = " Mascará de rede inválida."; break;
                case 4070: retorno = " Tempo inválido."; break;
                case 4071: retorno = " Tamanho do tempo inválido."; break;
                case 4080: retorno = " Tipo de registro inválido."; break;
                case 4090: retorno = " Tipo da ação inválido."; break;

                //--------------------------------------------------------------
                // Erros de memória
                //--------------------------------------------------------------
                case 5000: retorno = " Memória cheia."; break;
                case 5001: retorno = " Erro ao gravar registro."; break;
               
                //Erros ethernet
                case 10001: retorno = "Erro ao iniciar Thread."; break;
                case 10002: retorno = "Terminal não encontrado."; break;
                case 10003: retorno = "ID não definido."; break;
                case 10004: retorno = "Modo definido inválido."; break;
                case 10005: retorno = "REP já conectado"; break;
                case 10011: retorno = "Falha ao iniciar servidor."; break;
                case 10012: retorno = "Falha na conexão Cliente."; break;
                case 10013: retorno = "Falha ao abrir servidor."; break;
                case 10014: retorno = "Servidor cheio."; break;
                case 10020: retorno = "Estouro do buffer."; break;
                case 10021: retorno = "Esgotado tempo limite."; break;
                case 10022: retorno = "Erro de socket."; break;
                case 10023: retorno = "Erro ao enviar dados."; break;
                case 10024: retorno = "Erro ao receber dados."; break;
                case 10031: retorno = "Ip inválido."; break;
                case 10032: retorno = "Máscara de rede inválida."; break;            }

            return retorno;
        }


    }
}
