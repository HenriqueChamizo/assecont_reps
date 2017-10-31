using System.Windows.Forms;
using AssepontoRep;
using DemoREP_CSharp;
using System;
using System.Text;
using Wr.Classes;

namespace ZPM
{
    public class Bridge : AssepontoRep.Bridge
    {
        private StringBuilder MensagemErro = new StringBuilder(256);
        private TextBox consoleLog;
        private int NRegistros;

        private const int opExclusao = 0;
        private const int opInclusao = 1;
        private const int opAlteracao = 2;

        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
            consoleLog = edLog;
        }

        #region Overrides Abstract
        public override int getPortaPadrao()
        {
            return 5000;
        }

        public override string getRepFabricante()
        {
            return String.Format("ZPM: {0}", Wr.Classes.Net.getLocalIPAddress());
        }
        #endregion

        #region CONEXAO
        private bool Connect(string Ip, int Porta)
        {
            REPZPM_DLL.Handle = REPZPM_DLL.DLLREP_IniciaDriver(TerminalDados.Serial); //verifica o numero de fabricação

            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_DefineModoIP(REPZPM_DLL.Handle, Ip, Porta); //Conecta o ip e poa
            string Mensagem;

            if (REPZPM_DLL.Retorno == 1)
            {
                Mensagem = Convert.ToString(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.Retorno)); //ERRO
                REPZPM_DLL.Modo = 0;
                return false;
            }
            else
            {
                Mensagem = Convert.ToString(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.Retorno)); //CONECTADO
                return true;
            }

            log.AddLog(Mensagem);
        }
        #endregion

        #region Overrides ENVIAR DATA E HORA
        public override bool sendDataHora(int Terminal)
        {
            if (!base.sendDataHora(Terminal)) return false;

            Connect(TerminalDados.IP, TerminalDados.Porta);  //IP E PORTA DO PAI)

            int conta;
            string DataHora = DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");

            log.AddLog(Consts.DATA_HORA_ENVIANDO);

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_AtualizaDataHora(REPZPM_DLL.Handle, DataHora);

            /*Houve sucesso no envio do comando*/
            if (REPZPM_DLL.ID_Comando > 0)
            {
                NRegistros = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

                conta = 1;

                while (conta <= NRegistros)
                {
                    /*Obtém o código de erro do REP*/
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, conta);

                    /*Houve erro*/
                    if (REPZPM_DLL.Retorno != 0)
                    {
                        log.AddLog(Convert.ToString(REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno)), true);
                        return false;
                    }

                    conta++;
                }

                /*Houve erro*/
                if (REPZPM_DLL.Retorno != 0)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    return false;
                }
                else
                {
                    log.AddLog(Consts.DATA_HORA_ATUALIZADA_SUCESSO, true, true);

                }

            }
            else
            {
                log.AddLog(Convert.ToString(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando)));
            }
            return true;

        }
        #endregion

        #region Overrides ENVIAR EMPREGADOR
        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            if (!base.sendInfoEmpresa(Terminal, out EmpregadorDados)) return false;

            Connect(TerminalDados.IP, TerminalDados.Porta);

            string tempPessoaTipo = "";
            string tempCnpj = "";
            string tempNome = "";
            string tempCei = "";
            string tempEndereco = "";

            int Operacao;

            getEmpregador(out tempPessoaTipo, out tempCnpj, out tempNome, out tempCei, out tempEndereco);

            //getEmpregador(out EmpregadorDados.PessoaTipo, out EmpregadorDados.Pessoa, out EmpregadorDados.Nome, out EmpregadorDados.Cei, out EmpregadorDados.Endereco);

            Operacao = EmpregadorDados.Nome != "" ? opAlteracao : opInclusao;
            log.AddLog(Consts.EMPREGADOR_ENVIANDO);

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Empregador(REPZPM_DLL.Handle, Operacao, EmpregadorDados.PessoaTipo, EmpregadorDados.Pessoa, EmpregadorDados.Cei, EmpregadorDados.Nome, EmpregadorDados.Endereco);

            /*Sucesso na execução do comando*/
            if (REPZPM_DLL.ID_Comando > 0)
            {
                /*Retorna a quantidade de retornos do comando enviado*/
                NRegistros = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, NRegistros);

                /*Houve erro*/
                if (REPZPM_DLL.Retorno != 0)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    return false;
                }
                /*Houve sucesso no envio do comando*/
                else
                {
                    log.AddLog(Consts.EMPREGADOR_ENVIADO_SUCESSO);
                }

            }
            /*Trata o erro retornado pela DLL*/
            else
            {
                log.AddLog(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando), true);
            }
            return true;
        }

        private void getEmpregador(out string PessoaTipo, out string Cnpj, out string Nome, out string Cei, out string Endereco)
        {
            PessoaTipo = "";
            Cnpj = "";
            Nome = "";
            Cei = "";
            Endereco = "";

            Types.Empregador EmpregadorDados;

            StringBuilder Tipo = new StringBuilder(1);
            StringBuilder Identificacao = new StringBuilder(14);
            StringBuilder CEI = new StringBuilder(12);
            StringBuilder RazaoSocial = new StringBuilder(150);
            StringBuilder LocalTrabalho = new StringBuilder(100);
            StringBuilder MensagemErro = new StringBuilder(256);

            Connect(TerminalDados.IP, TerminalDados.Porta);

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaEmpregador(REPZPM_DLL.Handle);

            if (REPZPM_DLL.ID_Comando > 0)
            {
                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoEmpregador(REPZPM_DLL.Handle, Tipo, Identificacao, CEI, RazaoSocial, LocalTrabalho);

                /*Sucesso na leitura do empregador*/
                if (REPZPM_DLL.Retorno == 1)
                {
                    /*Mostra as informações do Empregador*/
                    EmpregadorDados.PessoaTipo = Tipo.ToString();
                    EmpregadorDados.Pessoa = Identificacao.ToString();
                    EmpregadorDados.Nome = RazaoSocial.ToString();
                    EmpregadorDados.Cei = CEI.ToString();
                    EmpregadorDados.Endereco = LocalTrabalho.ToString();
                }
                else
                {
                    /*Houve erro na leitura do empregador*/
                    log.AddLog(REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno), true);
                }
            }
            else
            {
                /*Trata o erro na inicialização do Handle*/
                log.AddLog(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando));
            }
        }
        #endregion

        #region Overrides ENVIAR FUNCIONARIO
        public override bool sendFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            bool Result = base.sendFuncionario(Funcionario);

            Connect(TerminalDados.IP, TerminalDados.Porta);

            int Operacao;
            bool Ok = false;
            bool Teclado = false;

            string Habilitar_Teclado = Teclado ? "S" : "N";
            string tempFuncionarioNome = "";
            string tempCracha = "";
            bool tempTeclado = false;
            string tempCodigoTeclado = "";
            string tempCodigoBarras = "";
            string tempCodigoMifare = "";
            string tempCodigoTag = "";

            string CodigoMifare = "";
            string CodigoTag = "";

            getFuncionario(Funcionario.Pis, out tempFuncionarioNome, out tempCracha, out tempTeclado,
                out tempCodigoTeclado, out tempCodigoBarras, out tempCodigoMifare, out tempCodigoTag);

            Operacao = tempFuncionarioNome != String.Empty ? opAlteracao : opInclusao;

            /******************************************************************************************************************************
            'INCLUSÃO DE FUNCIONÁRIO                                                                                                      *
            '******************************************************************************************************************************/
            /*Prepara o envio do cadastro do funcionário*/

            if (Funcionario.Crachas.Count > 0)
            {
                foreach (long Cracha in Funcionario.Crachas)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, Operacao, Funcionario.Pis, Convert.ToString(Cracha), Funcionario.Nome, "", Habilitar_Teclado, Convert.ToString(Cracha), Convert.ToString(Cracha), CodigoMifare, CodigoTag);
                }
            }

            /*Comando executado*/
            if (REPZPM_DLL.Retorno == 1)
            {
                REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Funcionario_Envia(REPZPM_DLL.Handle);

                /*Comando de cadastro de funcionário foi enviado com sucesso se for maior que 0*/
                if (REPZPM_DLL.ID_Comando > 0)
                {
                    if (REPZPM_DLL.Modo == 0)
                    {
                        /*Obtém o código de erro*/
                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, 1);

                        /*Se Retorno for <> de 0, então houve erro na execução do comando de cadastro de funcionário*/
                        if (REPZPM_DLL.Retorno != 0)
                        {
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                            log.AddLog(REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno), true);
                        }
                        else
                        {
                            log.AddLog(Consts.FUNCIONARIO_ENVIANDO);
                            Ok = true;
                        }
                    }
                }
                else
                {
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                }
            }
            else
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
            }

            return Ok;
        }

        private void getFuncionario(string Pis, out string FuncionarioNome, out string Cracha, out bool Teclado, out string CodigoTeclado,
            out string CodigoBarras, out string CodigoMifare, out string CodigoTag)
        {
            StringBuilder sPIS = new StringBuilder(11);
            StringBuilder sMatricula = new StringBuilder(20);
            StringBuilder sNomeFuncionario = new StringBuilder(52);
            StringBuilder sBiometrico = new StringBuilder(20000);    //Valor definido baseado na maior template possivel de ser gerada c/ 10 digitais cadastradas.
            StringBuilder sHabilitaTeclado = new StringBuilder(1);
            StringBuilder sCodigoTeclado = new StringBuilder(16);
            StringBuilder sCodigoBarras = new StringBuilder(20);
            StringBuilder sCodigoMIFARE = new StringBuilder(20);
            StringBuilder sCodigoTAG = new StringBuilder(20);
            StringBuilder MensagemErro = new StringBuilder(256);

            Cracha = String.Empty;
            FuncionarioNome = String.Empty;
            Teclado = false;
            CodigoTeclado = String.Empty;
            CodigoBarras = String.Empty;
            CodigoMifare = String.Empty;
            CodigoTag = String.Empty;

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaFuncionario(REPZPM_DLL.Handle, Pis);

            /*Sucesso na execução do comando*/
            if (REPZPM_DLL.ID_Comando > 0)
            {
                /**************************************************************************************************************************
                *MODO IP                                                                                                                  *
                ***************************************************************************************************************************/
                if (REPZPM_DLL.Modo == 0)
                {
                    NRegistros = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

                    /*Verifica se retornaram registros*/
                    if (NRegistros > 0)
                    {
                        /*Limpa a listagem*/
                        /*Executa a busca dos Funcionários*/
                        for (int i = 1; i <= NRegistros; i++)
                        {
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoFuncionario(REPZPM_DLL.Handle, i, sPIS, sMatricula, sNomeFuncionario, sBiometrico, sHabilitaTeclado, sCodigoTeclado, sCodigoBarras, sCodigoMIFARE, sCodigoTAG);

                            /*Sucesso na execução do comando*/
                            if (REPZPM_DLL.Retorno == 1)
                            {
                                Cracha = sMatricula.ToString();
                                FuncionarioNome = sNomeFuncionario.ToString();
                                Teclado = sHabilitaTeclado.ToString() == "S" ? true : false;
                                CodigoTeclado = sCodigoTeclado.ToString();
                                CodigoBarras = sCodigoBarras.ToString();
                                CodigoMifare = sCodigoMIFARE.ToString();
                                CodigoTag = sCodigoTAG.ToString();
                            }
                            else
                            {
                                /*Trata o retorno de erro do REP*/
                                log.AddLog(REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno), true);
                                return;
                            }
                        }
                    }
                    else
                    {
                        log.AddLog("Não há funcionário cadastrado no REP");
                        return;
                    }
                }
            }
            else
            {
                /*Houve erro no processamento do Handle*/
                log.AddLog(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando));
                return;
            }
        }

        #endregion

        public override bool LerMarcacoes(Marcacoes marcacoes)
        {
            Connect(TerminalDados.IP, TerminalDados.Porta);

            bool Result = false;
            int NRegistrosLidos = 0;

            StringBuilder PIS = new StringBuilder(11);
            StringBuilder DataHora = new StringBuilder(19);
            StringBuilder NSR = new StringBuilder(20);
            StringBuilder MensagemErro = new StringBuilder(256);

            //Marcacoes marcacoes = new Marcacoes(TerminalDados);

            log.AddLog(Consts.INICIALIZANDO_IMPORTACAO_ARQUIVO);
            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaPonto(REPZPM_DLL.Handle, DataInicio: ("dd/MM/yyyy"), DataFim: ("dd/MM/yyyy"));

            if (REPZPM_DLL.ID_Comando > 0)
            {
                /*Retorna a quantidade de registros*/
                NRegistros = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

                log.AddLog(String.Format(Consts.TOTAL_MARCACOES, NRegistros));

                if (NRegistros > 0)
                {
                    for (int i = 1; i <= NRegistros; i++)
                    {
                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoPonto(REPZPM_DLL.Handle, i, PIS, DataHora, NSR);

                        if (REPZPM_DLL.Retorno == 1)
                        {
                            marcacoes.Add(PIS.ToString(), Convert.ToDateTime(DataHora.ToString()), Convert.ToInt32(NSR.ToString()));
                            log.AddLogUnformatted(PIS.ToString().PadRight(18) + DataHora + " " + NSR);
                            NRegistrosLidos++;
                        }
                        else
                        {
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                            log.AddLog(Convert.ToString(MensagemErro));
                        }
                    }
                }
                else
                {
                    log.AddLog(Consts.SEM_MARCACOES_NOVAS);
                }
            }

            Result = (NRegistrosLidos > 0);

            if (Result)
            {
                marcacoes.SaveToFile();
                log.AddLogUnformatted(String.Format(Consts.ARQUIVO_GERADO, marcacoes.Arquivo));
                log.AddLineBreak();
            }
            else
            {
                log.AddLog(Consts.SEM_MARCACOES_NOVAS);
            }

            return Result;

        }
    }
}
