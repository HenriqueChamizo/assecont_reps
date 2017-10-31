using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using AssepontoRep;
using Sdk_Inner_Rep;

namespace InnerRepPlus
{
    public class Bridge : AssepontoRep.Bridge
    {
        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
        }

        private InnerRepPlusSDK innerRep;

        #region Override Abstract
        public override int getPortaPadrao()
        {
            return 3000;
        }

        public override bool getAutenticacao()
        {
            return false;
        }

        public override bool getPin()
        {
            return false;
        }

        public override string getRepFabricante()
        {
            return "InnerRepPlus";
        }

        public override string getArquivoUsb()
        {
            return "";
        }

        public override bool getGerarUsb()
        {
            return false;
        }

        public override bool getGerarBackup()
        {
            return false;
        }

        public override bool getGerarBiometria()
        {
            return false;
        }

        public override bool getEnviarHorarioVerao()
        {
            return false;
        }

        public override bool getEnviarHorarioVeraoUsb()
        {
            return false;
        }

        public override bool getNumeroSerieREP()
        {
            return false;
        }

        public override bool getGerarUsbEmpregador()
        {
            return false;
        }

        public override bool getCadastroTerminalSupervisor()
        {
            return false;
        }

        public override bool getFuncionariosAlteradosUsb()
        {
            return false;
        }

        public override bool getContemChaveAcessoREP()
        {
            return false;
        }

        public override bool getBoxFuncoes()
        {
            return false;
        }

        public override bool getCadastroTerminalResponsavel()
        {
            return true;
        }
        #endregion

        #region Enviar Data e Hora
        public override bool sendDataHora(int Terminal)
        {
            base.sendDataHora(Terminal);
            bool Result = false;
            try
            {
                if (Connect(Terminal))
                {
                    int resposta = innerRep.EnviarRelogio();
                    if (resposta == 0)
                    {
                        log.AddLog(AssepontoRep.Consts.DATA_HORA_ATUALIZADA_SUCESSO);
                        Result = true;
                    }
                    else
                        MensagemResultadoComunicacao(resposta);
                }
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }

            return Result;
        }
        #endregion

        #region Enviar Empresa
        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            base.sendInfoEmpresa(Terminal, out EmpregadorDados);
            bool Result = false;

            try
            {
                if (Connect(Terminal))
                {
                    innerRep.DefinirLocal(EmpregadorDados.Endereco);
                    innerRep.DefinirEmpregador(EmpregadorDados.Nome, EmpregadorDados.Pessoa, EmpregadorDados.Cei, (int)EmpregadorDados.PessoaTipo);

                    int resposta = innerRep.EnviarEmpregador();
                    if (resposta == 0)
                    {
                        log.AddLog(AssepontoRep.Consts.EMPREGADOR_ENVIADO_SUCESSO);
                        Result = true;
                    }
                    else
                        MensagemResultadoComunicacao(resposta);
                }
                Result = true;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }

            return Result;
        }
        #endregion

        #region Enviar Funcionario
        public override bool sendFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            base.sendFuncionario(Funcionario);
            bool Result = false;
            try
            {
                if (Connect(0))
                {
                    innerRep.InicializarListaEmpregados();
                    int resposta = innerRep.IncluirEmpregadosLista(Funcionario.Nome, Funcionario.Pis, crachas(Funcionario.Barras), crachas(Funcionario.Proximidade), crachas(Funcionario.Teclado), "", "1234", true);

                    if (resposta == 0)
                    {
                        resposta = innerRep.EnviarListaIndividualEmpregados();
                        resposta = innerRep.LerResultado(0);
                        if (resposta == 0)
                        {
                            Result = MensagemComandoFuncionario(Funcionario.Nome);
                            if (Result) log.AddLog(string.Format(Consts.FUNCIONARIO_ENVIADO_COM_SUCESSO, Funcionario.Nome));
                        }
                        else
                            MensagemResultadoComunicacao(resposta);
                    }
                    else
                        MensagemCadastroFuncionario(resposta);
                }

                return Result;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }

            return Result;
        }

        private string crachas(string cracha)
        {
            if (string.IsNullOrEmpty(cracha))
                return "0";
            else
                return cracha;
        }
        #endregion

        #region Ler Marcacoes
        public override bool LerMarcacoes(Marcacoes marcacoes, TipoImportacaoMarcacoes tipoimportacao)
        {
            base.LerMarcacoes(marcacoes, tipoimportacao);

            bool Result = false;
            DBApp db = new DBApp();
            int ProximoNsr = db.getLastNsr(TerminalDados.Indice);
            int NRegistrosLidos = 0, statusColeta = 0, nsr = 0, ret = 0, tentativas = 0;
            bool recebeuUltimoBilhete = false;
            string dadosRegistro = string.Empty;

            if (Connect(0))
            {
                try
                {
                    nsr = ProximoNsr;
                    while (!recebeuUltimoBilhete)
                    {
                        nsr++;
                        ret = innerRep.SolicitarRegistroNsr(nsr);
                        //string UltimoRegistro = innerRep.LerNumUltimoNsr();

                        if (ret == 0)
                        {
                            statusColeta = innerRep.LerStatusColeta();
                            tentativas = 0;
                            while (statusColeta < 2 && tentativas < 1000)
                            {
                                Thread.Sleep(10);
                                Application.DoEvents();
                                Thread.Sleep(10);
                                statusColeta = innerRep.LerStatusColeta();
                                tentativas++;
                            }

                            if ((statusColeta == (int)Sdk_Inner_Rep.InnerRepPlusSDK.StatusLeitura.FINALIZADA_COM_ULTIMO_REGISTRO) ||
                                (statusColeta == (int)Sdk_Inner_Rep.InnerRepPlusSDK.StatusLeitura.FINALIZADA_COM_REGISTRO))
                            {
                                string numSerie = innerRep.LerNumSerieRep();
                                statusColeta = innerRep.LerResultadoColeta();

                                dadosRegistro = innerRep.LerRegistroLinha();

                                if (dadosRegistro != "" && dadosRegistro != null)
                                {
                                    AssepontoRep.Marcacoes.Marcacao mrc = new AssepontoRep.Marcacoes.Marcacao();

                                    if (marcacoes.InterpretarRegistroAfd(dadosRegistro, out mrc))
                                    {
                                        if (mrc.Tipo == Marcacoes.TiposRegistroAfd.Marcacao)
                                            marcacoes.Add(mrc);
                                    }
                                }


                                NRegistrosLidos++;
                            }
                            if ((statusColeta == (int)Sdk_Inner_Rep.InnerRepPlusSDK.StatusLeitura.FINALIZADA_SEM_REGISTRO) ||
                            (statusColeta == (int)Sdk_Inner_Rep.InnerRepPlusSDK.StatusLeitura.FINALIZADA_COM_ULTIMO_REGISTRO))
                            {
                                recebeuUltimoBilhete = true;
                               
                            }
                        }
                    }

                    if (marcacoes.Count == 0)
                        log.AddLog(Consts.MARCACOES_NAO_HA_NOVAS);

                    innerRep.FinalizarLeitura();
                    Result = true;
                }
                catch (Exception ex)
                {
                    log.AddLog(ex.Message);
                }
            }
            return Result;
        }
        #endregion

        #region Deletar Funcionario
        public override bool deleteFuncionario(Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);
            bool Result = false;
            try
            {
                if (Connect(0))
                {
                    innerRep.InicializarListaEmpregados();
                    int resposta = innerRep.IncluirPisEmpregadoListaExclusao(Funcionario.Pis);
                    resposta = innerRep.EnviarListaExclusaoEmpregados();
                    resposta = innerRep.LerResultado(0);

                    if (resposta == 0)
                    {
                        Result = MensagemComandoFuncionario(Funcionario.Nome);
                        log.AddLineBreak();
                        if (Result) log.AddLog(string.Format(Consts.FUNCIONARIO_EXCLUIDO, Funcionario.Nome));
                    }
                    else
                        MensagemResultadoComunicacao(resposta);
                }

                return Result;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }

            return Result;
        }
        #endregion

        #region Enviar Horario de Verao
        //public override bool EnviarHorarioVerao(int Terminal, string inicio, string fim)
        //{
        //    try
        //    {
        //        base.EnviarHorarioVerao(Terminal, inicio, fim);

        //        InnerRepPlus_Conectar();

        //        this._watchComm.AddParcialConfiguration(ParcialConfigurationType.DST, Convert.ToDateTime(inicio), Convert.ToDateTime(fim), TerminalDados.OperadorCpf);
        //        this._watchComm.SendParcialSettings();

        //        log.AddLog(Consts.HORARIO_VERAO_ENVIADO_SUCESSO);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.AddLog(Consts.ERRO_ENVIO_COMANDO);
        //        return false;
        //    }
        //    finally
        //    {
        //        InnerRepPlus_Desconectar();
        //    }
        //}
        #endregion

        public override bool Connect(int Terminal)
        {
            // int result = innerRep.DefinirParametrosComunicacao("823.078.710-71", TerminalDados.IP, "**AUTENTICACAO**");

            base.Connect(Terminal);
            string cpf = string.Empty;

            if (innerRep == null)
                innerRep = new Sdk_Inner_Rep.InnerRepPlusSDK();

            //innerRep.DefinirModeloRede((int)Sdk_Inner_Rep.InnerRepSdk.ModeloRede.REDE_LOCAL);
            try
            {
                cpf = String.Format(@"{0:000\.000\.000\-00}", Convert.ToDouble(TerminalDados.OperadorCpf));
            }
            catch (Exception ex)
            {
                log.AddLog(Consts.ERRO_OPERADOR_CPF);
            }

            int result = innerRep.DefinirParametrosComunicacao(cpf, TerminalDados.IP, "**AUTENTICACAO**");
            MensagemParametrosComunicacao(result);
            return result == 0;
        }

        #region Avisos/Alertas
        private void MensagemParametrosComunicacao(int result)
        {
            switch ((Sdk_Inner_Rep.InnerRepPlusSDK.ParametrosComunicacao)result)
            {
                case Sdk_Inner_Rep.InnerRepPlusSDK.ParametrosComunicacao.SUCESSO:
                    log.AddLog("Iniciado Comunicação... ");
                    break;

                case Sdk_Inner_Rep.InnerRepPlusSDK.ParametrosComunicacao.ERRO_IP:
                    log.AddLog(Consts.ERRO_IP);
                    break;

                case Sdk_Inner_Rep.InnerRepPlusSDK.ParametrosComunicacao.ERRO_CHAVE_COMUNICACAO:
                    log.AddLog(Consts.ERRO_CHAVE_COMUNICACAO);
                    break;

                case Sdk_Inner_Rep.InnerRepPlusSDK.ParametrosComunicacao.ERRO_PORTA_COMUNICACAO:
                    log.AddLog(Consts.ERRO_PORTA_COMUNICACAO);
                    break;

                case Sdk_Inner_Rep.InnerRepPlusSDK.ParametrosComunicacao.ERRO_MASCARA_REDE:
                    log.AddLog(Consts.ERRO_MASCARA_REDE);
                    break;

                case Sdk_Inner_Rep.InnerRepPlusSDK.ParametrosComunicacao.ERRO_GATEWAY:
                    log.AddLog(Consts.ERRO_GATEWAY);
                    break;

                case Sdk_Inner_Rep.InnerRepPlusSDK.ParametrosComunicacao.ERRO_CPF_RESPONSAVEL:
                    log.AddLog(Consts.ERRO_OPERADOR_CPF);
                    break;

                default:
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    break;
            }
        }

        private void MensagemResultadoComunicacao(int result)
        {
            switch (result)
            {
                case (int)Sdk_Inner_Rep.InnerRepPlusSDK.ResultadoComunicacao.AGUARDANDO:
                    log.AddLog("Aguardado Comunicação... ");
                    break;

                case (int)Sdk_Inner_Rep.InnerRepPlusSDK.ResultadoComunicacao.SUCESSO:
                    break;

                case (int)Sdk_Inner_Rep.InnerRepPlusSDK.ResultadoComunicacao.TIMEOUT:
                    log.AddLog(Consts.TIMEOUT_COMUNICACAO);
                    break;

                default:
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    break;
            }
        }

        private bool MensagemComandoFuncionario(string nome)
        {
            switch (innerRep.LerStatus())
            {
                case (int)Sdk_Inner_Rep.InnerRepPlusSDK.ResultadoAlteracaoEmpregados.INCLUIDO_SUCESSO:
                    {
                        //log.AddLog(string.Format(AssepontoRep.Consts.FUNCIONARIO_ENVIADO_COM_SUCESSO, nome));
                        return true;
                    }

                case (int)Sdk_Inner_Rep.InnerRepPlusSDK.ResultadoAlteracaoEmpregados.ALTERADO_SUCESSO:
                    {
                        //log.AddLog("Funcionario alterado com sucesso");
                        return true;
                    }

                case (int)Sdk_Inner_Rep.InnerRepPlusSDK.ResultadoAlteracaoEmpregados.BASE_CHEIA:
                    {
                        //log.AddLog("Base Cheia");
                        return false;
                    }

                case (int)Sdk_Inner_Rep.InnerRepPlusSDK.ResultadoAlteracaoEmpregados.USUARIO_SEM_ALTERACOES:
                    {
                        log.AddLog("O funcionário não contém dados para serem atualizados");
                        return true;
                    }

                case (int)Sdk_Inner_Rep.InnerRepPlusSDK.ResultadoAlteracaoEmpregados.REP_SEM_EMPREGADOR_CADASTRADO:
                    {
                        log.AddLog("REP sem empregador cadastrado");
                        return false;
                    }

                default:
                    {
                        //log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                        return false;
                    }
            }
        }

        private bool MensagemCadastroFuncionario(int resposta)
        {
            switch (resposta)
            {
                case 1:
                    {
                        log.AddLog("Nome inválido");
                        return true;
                    }

                //case 2:
                //    {
                //        log.AddLog("Pis inválido");
                //        return false;
                //    }

                case 3:
                    {
                        log.AddLog("Cartão inválido");
                        return false;
                    }

                //case 4:
                //    {
                //        log.AddLog("O funcionário não contém dados para serem atualizados");
                //        return true;
                //    }

                case 5:
                    {
                        log.AddLog("teclado inválido");
                        return false;
                    }

                case 6:
                    {
                        log.AddLog("Senha do teclado inválida");
                        return false;
                    }

                //case 7:
                //    {
                //        log.AddLog("REP sem empregador cadastrado");
                //        return false;
                //    }

                case 8:
                    {
                        log.AddLog("Pis já existente");
                        return false;
                    }

                case 9:
                    {
                        log.AddLog("Cartão já existente");
                        return false;
                    }

                case 10:
                    {
                        log.AddLog("Limite de funcionarios atingido");
                        return false;
                    }

                case 11:
                    {
                        log.AddLog("Erro de conversão");
                        return false;
                    }

                case 12:
                case 13:
                    {
                        log.AddLog("Caracteres inválidos no nome");
                        return false;
                    }

                default:
                    {
                        //log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                        return false;
                    }
            }
        }
        #endregion
    }
}
