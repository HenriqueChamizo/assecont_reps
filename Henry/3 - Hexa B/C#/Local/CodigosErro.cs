using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Henry
{
    enum CodigosErro
    {
        [Description("Não há dados")]
        NaoHaDados = 1,
        [Description("Comando desconhecido")]
        ComandoDesconhecido = 10,
        [Description("Tamanho do pacote é inválido")]
        TamanhoPacoteInvalido = 11,
        [Description("Parâmetros informados são inválidos")]
        ParametrosInformadosInvalidos = 12,
        [Description("Tamanho dos parâmetros são inválidos")]
        ParametrosTamanhoInvalido = 14,
        [Description("Número da mensagem é inválido")]
        NumeroMensagemInvalido = 15,
        [Description("Equipamento não possui eventos")]
        EquipamentoNaoPossuiEventos = 28,
        [Description("Documento do empregador é inválido")]
        EmpregadorDocumentoInvalido = 30,
        [Description("Tipo do documento do empregador é inválido")]
        EmpreadorDocumentoTipoInvalido = 31,
        [Description("Ip é inválido")]
        IpInvalido = 32,
        [Description("Tipo de operação do usuário é inválida")]
        OperacaoTipoUsuarioInvalida = 33,
        [Description("PIS do empregado é inválido")]
        FuncionarioPisInvalido = 34,
        [Description("Cei do empregador é inválido")]
        EmpregadorCEIInvalido = 35,
        [Description("Referencia do empregado é inválido")]
        FuncionarioReferenciaInvalido = 36,
        [Description("Nome do empregado é inválido")]
        FuncionarioNome = 37,
        [Description("Matrícula já existe")]
        MaticulaJaExiste = 61,
        [Description("Pis já existe")]
        PisJaExiste = 62,
        [Description("Opção inválida")]
        OpcaoInvalida = 63,
        [Description("Matrícula não existe")]
        MatriculaNaoExiste = 64,
        [Description("Comando informado é inválido")]
        ComandoInformadoInvalido = 105,
        [Description("Data hora do comando é inválida")]
        DataHoraInvalida = 106,
        [Description("Evento é inválido")]
        EventoInvalido = 107,
    }
}
