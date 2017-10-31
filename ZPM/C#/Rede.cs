using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Wr;

namespace Zpm
{
    class Rede
    {
        private const int opExclusao = 0;
        private const int opInclusao = 1;
        private const int opAlteracao = 2;

        public Log log;
        private StringBuilder MensagemErro = new StringBuilder(256);
        private int NRegistros;

        public Rede(string IP, int Porta, string Serial, TextBox textbox)
        {
            log = new Log(textbox, IP);
            initDriver(Serial);
            initModoIp(IP, Porta);
        }

        private bool initDriver(string Serial)
        {
            bool Result = false;

            REPZPM_DLL.Handle = REPZPM_DLL.DLLREP_IniciaDriver(Serial);

            if (REPZPM_DLL.Handle == -1)
            {
                log.AddLog("Erro na inicialização do Driver");
            }
            else
            {
                Result = true;
            }

            return Result;
        }

        private void initModoIp(string IP, int Porta)
        {
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_DefineModoIP(REPZPM_DLL.Handle, IP, Porta);
            string Mensagem;

            if (REPZPM_DLL.Retorno == 1)
            {
                Mensagem = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.Retorno);
                REPZPM_DLL.Modo = 0;
            }
            else
            {
                Mensagem = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.Retorno);
            }

            log.AddLog(Mensagem);
        }

        public void sendEmpregador(string PessoaTipo, string Cnpj, string Nome, string Cei, string Endereco)
        {
            string tempPessoaTipo = "";
            string tempCnpj = "";
            string tempNome = "";
            string tempCei = "";
            string tempEndereco = "";

            int Operacao;

            getEmpregador(out tempPessoaTipo, out tempCnpj, out tempNome, out tempCei, out tempEndereco);

            Operacao = tempNome != "" ? opAlteracao : opInclusao;

            log.AddLog(Consts.EMPREGADOR_ENVIANDO);

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Empregador(REPZPM_DLL.Handle, Operacao, PessoaTipo, Cnpj, Cei, Nome, Endereco);

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

                    log.AddLog(Convert.ToString(MensagemErro), true);
                }
                /*Houve sucesso no envio do comando*/
                else
                {
                    log.AddLog(Consts.EMPREGADOR_ENVIADO_SUCESSO, true, true);
                }
            }
            /*Trata o erro retornado pela DLL*/
            else
            {
                log.AddLog(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando), true);
            }
        }

        public void sendDataHora()
        {
            StringBuilder MensagemErro = new StringBuilder(256);
            int conta;
            string DataHora = DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");
            log.AddLog(Consts.DATA_HORA_ENVIANDO);
            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_AtualizaDataHora(REPZPM_DLL.Handle, DataHora);
            //REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_AtualizaDataHora(REPZPM_DLL.Handle, "15/03/2012 15:45");

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
                        log.AddLog(REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno), true);
                        return;
                    }

                    conta++;
                }

                /*Houve erro*/
                if (REPZPM_DLL.Retorno != 0)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                    log.AddLog(Convert.ToString(MensagemErro));
                    return;
                }
                else
                {
                    log.AddLog(Consts.DATA_HORA_ATUALIZADA_SUCESSO, true, true);
                    return;
                }
            }
            else
            {
                log.AddLog(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando));
                return;
            }
        }

        public bool sendFuncionario(string Pis, string FuncionarioNome, string Cracha, bool Teclado, string CodigoTeclado, 
            string CodigoBarras, string CodigoMifare, string CodigoTag)
        {
            int Operacao;
            bool Ok = false;
            string Habilitar_Teclado = Teclado ? "S" : "N";

            string tempFuncionarioNome = "";
            string tempCracha = "";
            bool tempTeclado = false;
            string tempCodigoTeclado = "";
            string tempCodigoBarras = "";
            string tempCodigoMifare = "";
            string tempCodigoTag = "";

            getFuncionario(Pis, out tempFuncionarioNome, out tempCracha, out tempTeclado, 
                out tempCodigoTeclado, out tempCodigoBarras, out tempCodigoMifare, out tempCodigoTag);

            Operacao = tempFuncionarioNome != String.Empty ? opAlteracao : opInclusao;

            /******************************************************************************************************************************
            'INCLUSÃO DE FUNCIONÁRIO                                                                                                      *
            '******************************************************************************************************************************/
            /*Prepara o envio do cadastro do funcionário*/
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, Operacao, Pis, Cracha, FuncionarioNome, "", Habilitar_Teclado, CodigoTeclado, CodigoBarras, CodigoMifare, CodigoTag);

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

        public bool deleteFuncionario(string Pis)
        {
            bool Ok = false;

            /******************************************************************************************************************************
            'INCLUSÃO DE FUNCIONÁRIO                                                                                                      *
            '******************************************************************************************************************************/
            /*Prepara o envio do cadastro do funcionário*/
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, opExclusao, Pis, "", "", "", "", "", "", "", "");

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
                            log.AddLog(Convert.ToString(MensagemErro), true);
                        }
                        else
                        {
                            log.AddLog(REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno), true, true);
                            Ok = true;
                        }
                    }
                }
                else
                {
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO, true);
                }
            }
            else
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO, true);
            }

            return Ok;
        }

        public bool getArquivoMarcacoes(string Arquivo, int Grupo, DateTime DataInicial, DateTime DataFinal)
        {
            bool Result = false;

            int NRegistrosLidos = 0;

            StringBuilder PIS = new StringBuilder(11);
            StringBuilder DataHora = new StringBuilder(19);
            StringBuilder NSR = new StringBuilder(20);
            StringBuilder MensagemErro = new StringBuilder(256);

            Marcacoes marcacoes = new Marcacoes(Arquivo);

            log.AddLog(Consts.INICIALIZANDO_IMPORTACAO_ARQUIVO);
            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaPonto(REPZPM_DLL.Handle, DataInicial.ToString("dd/MM/yyyy"), DataFinal.ToString("dd/MM/yyyy"));

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
                    log.AddLog(Consts.SEM_MARCACOES);
                }
            }

            Result = (NRegistrosLidos > 0);

            if (Result)
            {
                marcacoes.SaveToFile();
                log.AddLogUnformatted(String.Format(Consts.ARQUIVO_GERADO, Arquivo));
                log.AddLineBreak();
            }
            else
            {
                log.AddLog(Consts.SEM_MARCACOES);
            }

            return Result;
        }

        private void getEmpregador(out string PessoaTipo, out string Cnpj, out string Nome, out string Cei, out string Endereco)
        {
            PessoaTipo = "";
            Cnpj = "";
            Nome = "";
            Cei = "";
            Endereco = "";

            StringBuilder Tipo = new StringBuilder(1);
            StringBuilder Identificacao = new StringBuilder(14);
            StringBuilder CEI = new StringBuilder(12);
            StringBuilder RazaoSocial = new StringBuilder(150);
            StringBuilder LocalTrabalho = new StringBuilder(100);
            StringBuilder MensagemErro = new StringBuilder(256);

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaEmpregador(REPZPM_DLL.Handle);

            if (REPZPM_DLL.ID_Comando > 0)
            {
                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoEmpregador(REPZPM_DLL.Handle, Tipo, Identificacao, CEI, RazaoSocial, LocalTrabalho);

                /*Sucesso na leitura do empregador*/
                if (REPZPM_DLL.Retorno == 1)
                {
                    /*Mostra as informações do Empregador*/
                    PessoaTipo = Tipo.ToString();
                    Cnpj = Identificacao.ToString();
                    Nome = RazaoSocial.ToString();
                    Cei = CEI.ToString();
                    Endereco = LocalTrabalho.ToString();
                }
                else
                {
                    /*Houve erro na leitura do empregador*/
                    log.AddLog(REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno), true);
                    return;
                }
            }
            else
            {
                /*Trata o erro na inicialização do Handle*/
                log.AddLog(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando));
                return;
            }
        }
    }
}
