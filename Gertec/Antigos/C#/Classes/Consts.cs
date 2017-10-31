using System;
using System.Collections.Generic;
using System.Text;

namespace Gertec
{
    public static class Consts
    {
        public const int TRM_SUBITEM_DESCRICAO = 0;
        public const int TRM_SUBITEM_IP = 1;
        public const int TRM_SUBITEM_PORTA = 2;
        public const int TRM_SUBITEM_IND = 3;

        public const int FUNC_SUBITEM_NOME = 0;
        public const int FUNC_SUBITEM_FUNCAO = 1;
        public const int FUNC_SUBITEM_CRACHA = 2;
        public const int FUNC_SUBITEM_PIS = 3;
        public const int FUNC_SUBITEM_IND = 4;
        public const int FUNC_SUBITEM_ENVIADOEM = 5;

        public const string TERMINALNOME = "Gertec";
        public const int PORTAPADRAO = 0;

        public const string CANCELADO = "CANCELADO";
        public const string EMPREGADOR_ENVIANDO = "Enviando Empregador...";
        public const string EMPREGADOR_ENVIADO_SUCESSO = "Empregador enviado com sucesso";

        public const string DATA_HORA_ATUALIZADA_SUCESSO = "Data e Hora Atualizada com sucesso";
        public const string DATA_HORA_ENVIANDO = "Enviando Data e Hora...";

        public const string ERRO_ENVIO_COMANDO = "Erro no envio do comando";
        public const string FUNCIONARIOS_ENVIANDO = "Enviando {0} Registros... <ESC> para Cancelar";
        public const string FUNCIONARIO_ENVIANDO = "Enviando {0}...";
        public const string FUNCIONARIOS_EXCLUINDO = "Excluindo {0}...";
        public const string PIS_JA_EXISTE = "PIS: {0} Já Existe no Terminal";
        public const string SEM_MARCACOES = "Não há marcações de ponto no período selecionado";
        public const string TOTAL_MARCACOES = "Lendo {0} marcações...";
        public const string ARQUIVO_GERADO = "Arquivo gerado: {0}";
        public const string INICIALIZANDO_IMPORTACAO_ARQUIVO = "Inicializando importação...";
        public const string MARCACOES_PROCESSAR_ERROS = "Erros ao processar: {0}";
        public const string MARCACOES_PROCESSAR_FINALIZADO = "{0} de {1} Registro(s) processados com exito";
    }
}
