using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using AssepontoRep;

namespace Athos
{
    public class Bridge : AssepontoRep.Bridge
    {
        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
        }

        //Firmware 3.0.000.031114
        private StarREP.clsComunica starRep;
        private short numeroTentativas = 3;
        private int tempoEsperaMilissegundos = 5000;

        #region Override Abstract
        public override int getPortaPadrao()
        {
            return 2101;
        }

        public override string getRepFabricante()
        {
            return "Athos";
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
            return false;
        }
        public override bool getPin()
        {
            return false;
        }
        public override bool getDisconnectOnExit()
        {
            return false;
        }

        public override bool getAutenticacao()
        {
            return false;
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
                    short porta = (short)TerminalDados.Porta;
                    string data = DateTime.Now.ToString("ddMMyyyy");
                    string hora = DateTime.Now.ToString("HHmm");

                    if (starRep.EnviarDataHora(TerminalDados.IP, porta, numeroTentativas, tempoEsperaMilissegundos, data, hora))
                    {
                        log.AddLog(AssepontoRep.Consts.DATA_HORA_ATUALIZADA_SUCESSO);
                        Result = true;
                    }
                    else
                        log.AddLog(AssepontoRep.Consts.ERRO_ENVIO_COMANDO);
                }
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }
            finally
            {
                starRep.Fecha_Comunicacao();
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
                    short porta = (short)TerminalDados.Porta;
                    short tipo = (short)EmpregadorDados.PessoaTipo;

                    short resposta = starRep.EnviarEmpresa(TerminalDados.IP, porta, numeroTentativas, tempoEsperaMilissegundos, tipo,
                        EmpregadorDados.Pessoa, (EmpregadorDados.Cei == string.Empty ? "000000000000" : EmpregadorDados.Cei), EmpregadorDados.Nome, EmpregadorDados.Endereco);

                    if (resposta == 1 || resposta == 2)
                    {
                        log.AddLog(AssepontoRep.Consts.EMPREGADOR_ENVIADO_SUCESSO);
                        Result = true;
                    }
                    else
                        log.AddLog(AssepontoRep.Consts.ERRO_ENVIO_COMANDO);
                }
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }
            finally
            {
                starRep.Fecha_Comunicacao();
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
                    short porta = (short)TerminalDados.Porta;

                    short resposta = starRep.EnviarFuncionario(TerminalDados.IP, porta, numeroTentativas, tempoEsperaMilissegundos, Funcionario.Pis, Funcionario.Nome, Funcionario.Barras);

                    if (resposta == 1 || resposta == 2)
                    {
                        log.AddLog(string.Format(AssepontoRep.Consts.FUNCIONARIO_ENVIADO_COM_SUCESSO, Funcionario.Nome));
                        Result = true;
                    }
                    else
                        log.AddLog(AssepontoRep.Consts.ERRO_ENVIO_COMANDO);
                }

                return Result;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }
            finally
            {
                starRep.Fecha_Comunicacao();
            }
            return Result;
        }
        #endregion

        #region Ler Marcacoes
        public override bool LerMarcacoes(Marcacoes marcacoes, TipoImportacaoMarcacoes tipoimportacao)
        {
            base.LerMarcacoes(marcacoes, tipoimportacao);
            bool Result = false;
            DBApp db = new DBApp();

            int ProximoNsr = db.getLastNsr(TerminalDados.Indice);
            int NRegistrosLidos = 0;
            short porta = (short)TerminalDados.Porta;

            if (Connect(0))
            {
                //string Ponteir = starRep.VerificarNSRPonteiro(TerminalDados.IP, porta, numeroTentativas, tempoEsperaMilissegundos);
                //int ultimoNSR = Convert.ToInt32(starRep.VerificarUltimoNSR(TerminalDados.IP, porta, numeroTentativas, tempoEsperaMilissegundos));
                try
                {
                    string resposta = starRep.ColetarMarcacoes(TerminalDados.IP, porta, numeroTentativas, tempoEsperaMilissegundos, ProximoNsr + 1, "|");

                    while (!string.IsNullOrEmpty(resposta) && !resposta.ToLower().Equals("erro"))
                    {
                        string[] respostas = resposta.Split('|');
                        NRegistrosLidos = Convert.ToInt32(respostas[0]);

                        for (int i = 1; i < NRegistrosLidos + 1; i++)
                        {
                            string b = respostas[i].Substring(0, 9);
                            string c = b + 3 + respostas[i].Substring(9);

                            AssepontoRep.Marcacoes.Marcacao mrc = new AssepontoRep.Marcacoes.Marcacao();
                            if (marcacoes.InterpretarRegistroAfd(c, out mrc))
                            {
                                if (mrc.Tipo == Marcacoes.TiposRegistroAfd.Marcacao)
                                    marcacoes.Add(mrc);
                            }
                        }
                        resposta = string.Empty;
                    }

                    if (marcacoes.Count == 0)
                        log.AddLog(Consts.MARCACOES_NAO_HA_NOVAS);

                    Result = true;
                }
                catch (Exception ex)
                {
                    log.AddLog(ex.Message);
                }
                finally
                {
                    starRep.Fecha_Comunicacao();
                }
            }
            return Result;
        }
        #endregion

        #region Deletar Funcionario
        public override bool deleteFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);
            bool Result = false;
            try
            {
                if (Connect(0))
                {
                    short porta = (short)TerminalDados.Porta;

                    if (starRep.ExcluirFuncionario(TerminalDados.IP, porta, numeroTentativas, tempoEsperaMilissegundos, Funcionario.Pis))
                    {
                        log.AddLog("OK");
                        Result = true;
                    }
                    else
                        log.AddLog(AssepontoRep.Consts.ERRO_ENVIO_COMANDO);
                }

                return Result;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }
            finally
            {
                starRep.Fecha_Comunicacao();
            }
            return Result;
        }
        #endregion

        //Não em horário de verão.
        //#region Enviar Horario de Verao
        //public override bool EnviarHorarioVerao(int Terminal, string inicio, string fim)
        //{
        //}
        //#endregion

        public override bool Connect(int Terminal)
        {
            base.Connect(Terminal);

            if (starRep == null)
                starRep = new StarREP.clsComunica();

            return true;
        }
    }
}
