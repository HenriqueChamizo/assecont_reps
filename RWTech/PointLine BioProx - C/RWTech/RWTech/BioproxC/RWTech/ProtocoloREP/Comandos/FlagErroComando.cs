using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioproxC.RWTech.ProtocoloREP.Comandos
{
    public class FlagErroComando
    {
        /* Códigos padrões do Protocolo */
        public static int SUCESSO = 0x54;
        public static int ERRO_BCC = 0x01;
        public static int DADOS_OK = 0x45;
        public static int DADOS_COM_ERRO = 0x96;
        public static int ADICIONAR = 0x80;
        public static int ADICIONAR_SUBSTITUIR = 0x57;
        public static int EXCLUIR = 0x78;
        public static int IDENTIFICADOR_INCONSISTENTE = 0x10;
        public static int IDENTIFICADOR_RECUSADO = 0x11;
        public static int CODIGO_RECUSADO = 0x12;
        public static int ESPACO_INSUFICIENTE = 0x13;
        public static int TODAS_TEMPLATE_USUARIO = 0x06;
        public static int UNICA_TEMPLATE_USUARIO = 0x07;

        /* Códigos adicionais */
        public static int HORA_INVALIDA = 2;
        public static int PARAMETRO_TAMANHO_FLAGERROR_NAO_SUPORTADOS = 3;
        public static int COMANDO_NAO_SUPORTADO_OU_DESCONHECIDO = 4;
        public static int ERRO_NAO_ESPECIFICADO = 5;
        public static int PIS_INEXISTENTE = 8;
        public static int FRAME_COM_ERRO = 96;
        public static int RELOGIO_NAO_AJUSTADO = 203;
        public static int HVERAO_NAO_AJUSTADO = 204;
        public static int NENHUM_FUNCIONARIO_CADASTRADO = 205;
        public static int NENHUMA_MARCACAO_PONTO = 206;
        public static int COMUNICACAO_NAO_ESTABELECIDA = 200;
        public static int RETORNO_INCONSISTENTE = 201;
        public static int OCORREU_EXCECAO = 202;
        public static int ERRO_FINALIZAR_CONEXAO = 400;
        public static int FIM_LEITURA_MARCACOES = 500;

        private int erro;
        private String mensagem;

        public FlagErroComando()
        {
            this.setErro(SUCESSO);
        }

        public FlagErroComando(int erro)
        {
            this.setErro(erro);
        }

        public void setErro(int erro)
        {
            this.erro = erro;
            this.setMensagem();
        }

        public int getErro()
        {
            return erro;
        }

        public String getErroStrHex()
        {
            String erroHex = Convert.ToString(erro, 16);
            if (erroHex.Length < 2)
                erroHex = "0" + erroHex;
            return "0x" + erroHex;
        }

        public String getErroStrHex(int erro)
        {
            String erroHex = Convert.ToString(erro, 16);
            if (erroHex.Length < 2)
                erroHex = "0" + erroHex;
            return "0x" + erroHex;
        }

        public String getMensagem()
        {
            return mensagem;
        }

        public String getMensagem(int erro)
        {
            ///* Códigos padrões do Protocolo */
            //public static int DADOS_COM_ERRO = 0x96;
            //public static int ADICIONAR = 0x80;
            //public static int ADICIONAR_SUBSTITUIR = 0x57;
            //public static int EXCLUIR = 0x78;
            //public static int TODAS_TEMPLATE_USUARIO = 0x06;
            //public static int UNICA_TEMPLATE_USUARIO = 0x07;
            //public static int PARAMETRO_TAMANHO_FLAGERROR_NAO_SUPORTADOS = 3;
            switch (erro)
            {
                case 0x54:
                    return "Sucesso!";

                case 500:
                    return "Sucesso!";

                case 2:
                    return "Hora inválida!";

                case 0x01:
                    return "Erro de BCC!";

                case 200:
                    return "Comunicação não estabelecida!";

                case 201:
                    return "Retorno Inconsistente!";

                case 202:
                    return "Ocorreu exceção durante a operação!";

                case 0x45:
                    return "Sucesso!";

                case 4:
                    return "Comando não suportado ou desconhecido!";

                case 5:
                    return "Erro não especificado!";

                case 8:
                    return "PIS não encontrado!";

                case 0x10:
                    return "Identificador inconsistente!";

                case 0x11:
                    return "Funcionário não cadastrado no relógio!";

                case 0x12:
                    return "Código recusado!";

                case 0x13:
                    return "Espaço insuficiente!";

                case 96:
                    return "Frame recebido com erro!";

                case 203:
                    return "Relógio não ajustado!";

                case 204:
                    return "Horário de verão não ajustado!";

                case 205:
                    return "Nenhum funcionário cadastrado!";

                case 206:
                    return "Nenhuma marcação de ponto recebida!";

                case 400:
                    return "Erro ao Finalizar Conexão!";

                default:
                    return "Erro desconhecido!";
            }
        }

        private void setMensagem()
        {
            switch (erro)
            {
                case 0x54:
                    mensagem =  "Sucesso!";
                    break;
                case 500:
                    mensagem = "Sucesso!";
                    break;
                case 2:
                    mensagem = "Hora inválida!";
                    break;

                case 0x01:
                    mensagem = "Erro de BCC!";
                    break;
                case 200:
                    mensagem = "Comunicação não estabelecida!";
                    break;
                case 201:
                    mensagem = "Retorno Inconsistente!";
                    break;
                case 202:
                    mensagem = "Ocorreu exceção durante a operação!";
                    break;
                case 0x45:
                    mensagem = "Sucesso!";
                    break;
                case 4:
                    mensagem = "Comando não suportado ou desconhecido!";
                    break;
                case 5:
                    mensagem = "Erro não especificado!";
                    break;
                case 8:
                    mensagem = "PIS não encontrado!";
                    break;
                case 0x10:
                    mensagem = "Identificador inconsistente!";
                    break;
                case 0x11:
                    mensagem = "Funcionário não cadastrado no relógio!";
                    break;
                case 0x12:
                    mensagem = "Código recusado!";
                    break;
                case 0x13:
                    mensagem = "Espaço insuficiente!";
                    break;
                case 96:
                    mensagem = "Frame recebido com erro!";
                    break;
                case 203:
                    mensagem = "Relógio não ajustado!";
                    break;
                case 204:
                    mensagem = "Horário de verão não ajustado!";
                    break;
                case 205:
                    mensagem = "Nenhum funcionário cadastrado!";
                    break;
                case 206:
                    mensagem = "Nenhuma marcação de ponto recebida!";
                    break;
                case 400:
                    mensagem = "Erro ao Finalizar Conexão!";
                    break;
                default:
                    mensagem = "Erro desconhecido!";
                    break;
            }
        }
    }
}
