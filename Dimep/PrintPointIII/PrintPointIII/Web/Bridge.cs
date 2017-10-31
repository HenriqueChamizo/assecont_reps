using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AssepontoRep;
using org.cesar.dmplight.watchComm.business;

namespace PrintPointIII
{
    public class Bridge : AssepontoRep.Bridge
    {
        private org.cesar.dmplight.watchComm.impl.WatchComm _watchComm;
        private string _chavePublica_RSA = "";
        private string _expoente_RSA = "";
        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
        }

        #region Override Abstract
        public override int getPortaPadrao()
        {
            return 3000;
        }

        public override string getRepFabricante()
        {
            return "PrintPointIII";
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
            return true;
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
            return true;
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

        public override bool getPin()
        {
            return false;
        }

        public override bool getCadastroTerminalResponsavel()
        {
            return true;
        }

        public override bool getAutenticacao()
        {
            return false;
        }

        public override bool getColumnId()
        {
            return false;
        }

        public override bool getDisconnectOnExit()
        {
            return false;
        }

        public override List<Types.Permissao> getPermissoes()
        {
            return null;
        }
        #endregion

        #region Envia Data e Hora
        public override bool sendDataHora(int Terminal)
        {
            base.sendDataHora(Terminal);
            bool Result = false;
            DateTime dtEnvio = DateTime.Now;
            try
            {
                if (PrintPointIII_Conectar())
                {
                    _watchComm.SetDateTime(dtEnvio, TerminalDados.OperadorCpf);
                    log.AddLog(AssepontoRep.Consts.DATA_HORA_ATUALIZADA_SUCESSO);
                    Result = true;
                }
                else
                    throw new Exception();
            }
            catch (Exception ex){   log.AddLog("Erro: Conexão não efetuada!");  }
            finally {   PrintPointIII_Desconectar();    }

            return Result;
        }
        #endregion

        #region Envia Empresa
        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            base.sendInfoEmpresa(Terminal, out EmpregadorDados);

            bool Result = false;

            org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType tipoEmpregador =
                (org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType)(int)(EmpregadorDados.PessoaTipo);

            try
            {
                PrintPointIII_Conectar();
                _watchComm.ChangeEmployer(tipoEmpregador, EmpregadorDados.Pessoa, EmpregadorDados.Cei, EmpregadorDados.Nome, EmpregadorDados.Endereco);

                log.AddLog(AssepontoRep.Consts.EMPREGADOR_ENVIADO_SUCESSO);

                //_watchComm.AddConfiguration(EConfigurationType.IdentificationEnabled, TerminalDados.Teclado);
                //_watchComm.SendSettings();

                //log.AddLog(AssepontoRep.Consts.CONFIGURACAO_ENVIADA);

                Result = true;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }
            finally
            {
                PrintPointIII_Desconectar();
            }

            
            PrintPointIII_InsereFuncionarioMasterListaEnvio(TerminalDados.SupervisorPis, TerminalDados.SupervisorCodigo, TerminalDados.SupervisorSenha, TerminalDados.OperadorCpf);
            PrintPointIII_EnviaFuncionariosMasterListaEnvio();

            return Result;
        }
        #endregion

        #region Envia Funcionarios
        public override bool sendFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            base.sendFuncionario(Funcionario);

            string Senha = "";

            foreach (int Cracha in Funcionario.Crachas)
            {
                _watchComm.AddEmployee(Funcionario.Pis, Funcionario.Nome, Senha);
                _watchComm.AddCredential(Cracha.ToString(), Funcionario.Pis, 0);
            }

            return true;
        }

        public override bool sendFuncionarios(int Terminal, List<AssepontoRep.Types.Funcionario> listFuncionarios)
        {
            if (base.sendFuncionarios(Terminal, listFuncionarios))
            {
                return (PrintPointIII_EnviaFuncionariosListaEnvio());

            }
            else
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
            return false;
        }

        private bool PrintPointIII_EnviaFuncionariosListaEnvio()
        {
            bool Result = false;

            try
            {
                //PrintPointIII_Conectar();
                _watchComm.IncludeEmployeesList(true, false, TerminalDados.OperadorCpf);
                _watchComm.IncludeCredentialList(true);
                log.LogOk();
                Result = true;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }
            finally
            {
                PrintPointIII_Desconectar();
            }

            return Result;
        }
        #endregion

        #region LerMarcacoes
        public override bool LerMarcacoes(Marcacoes marcacoes, TipoImportacaoMarcacoes tipoimportacao)
        {
            base.LerMarcacoes(marcacoes, tipoimportacao);

            bool Result = false;
            DBApp db = new DBApp();
            int ProximoNsr = db.getLastNsr(TerminalDados.Indice) + 1;
            int Contador = 0;

            MRPRecord[] registrosMRP;

            if (PrintPointIII_Conectar())
            {
                try
                {
                    try
                    {
                        _watchComm.RepositioningMRPRecordsPointer(ProximoNsr.ToString());
                    }
                    catch (Exception ex)
                    {
                        log.AddLog(Consts.MARCACOES_NAO_HA_NOVAS);
                    }

                    registrosMRP = this._watchComm.InquiryMRPRecords(false, false, true, false, false);

                    while (registrosMRP != null)
                    {
                        foreach (MRPRecord registroMRP in registrosMRP)
                        {
                            if (registroMRP is MRPRecord_RegistrationMarkingPoint)
                            {
                                string Pis = ((MRPRecord_RegistrationMarkingPoint)registroMRP).Pis;

                                DateTime DataHora = ((MRPRecord_RegistrationMarkingPoint)registroMRP).DateTimeMarkingPoint;
                                int Nsr = Convert.ToInt32(((MRPRecord_RegistrationMarkingPoint)registroMRP).NSR);
                                marcacoes.Add(Pis, DataHora, Nsr);
                                Contador++;

                                if (Nsr > ProximoNsr)
                                    ProximoNsr = Nsr;
                            }
                        }

                        registrosMRP = _watchComm.ConfirmationReceiptMRPRecords();
                    }

                    base.FinalizarImportacaoMarcacoes(marcacoes, ProcessarMarcacoesAoImportar.ProcessarSim);
                }
                catch (Exception ex)
                {
                    log.AddLog(ex.Message);
                }
                finally
                {
                    PrintPointIII_Desconectar();
                }
            }
            return Result;
        }
        #endregion

        #region Delete Funcionario
        public override bool deleteFuncionario(Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);

            bool Result = false;

            try
            {
                PrintPointIII_Conectar();
                _watchComm.AddEmployee(Funcionario.Pis);
                this._watchComm.ExcludeEmployeesList();
                log.AddLog(String.Format(AssepontoRep.Consts.FUNCIONARIO_EXCLUIDO, Funcionario.Nome));
                Result = true;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
            }
            finally
            {
                PrintPointIII_Desconectar();
            }

            return Result;
        }
        #endregion

        public override bool EnviarResponsavel(int Terminal, String OperadorCPF, String OperadorUsuario, String OperadorSenha)
        {
            bool result = false;
            base.EnviarResponsavel(Terminal, OperadorCPF, OperadorUsuario, OperadorSenha);

            try
            {
                PrintPointIII_Conectar();

                this._watchComm.UpdateCommunicationUser(OperadorUsuario, OperadorSenha);
                (new DBApp()).updateOperadorResponsavel(Terminal, OperadorCPF, OperadorUsuario, OperadorSenha);
                log.AddLog(Consts.OPERADOR_ENVIADO);
                result = true;
            }
            catch (Exception ex)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
            }
            finally
            {
                PrintPointIII_Desconectar();
                prepareTerminal(Terminal);
                InstanciaWatchComm();
            }

            return result;
        }

        #region Envia Horario de Verao
        public override bool EnviarHorarioVerao(int Terminal, string inicio, string fim)
        {
            try
            {
                base.EnviarHorarioVerao(Terminal, inicio, fim);

                PrintPointIII_Conectar();

                this._watchComm.AddParcialConfiguration(ParcialConfigurationType.DST, Convert.ToDateTime(inicio), Convert.ToDateTime(fim), TerminalDados.OperadorCpf);
                this._watchComm.SendParcialSettings();

                log.AddLog(Consts.HORARIO_VERAO_ENVIADO_SUCESSO);
                return true;
            }
            catch (Exception ex)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                return false;
            }
            finally
            {
                PrintPointIII_Desconectar();
            }
        }
        #endregion

        public override bool Connect(int Terminal)
        {
            base.Connect(Terminal);
            return PrintPointIII_Conectar();
        }

        //public override bool Disconnect(int Terminal)
        //{
        //    base.Disconnect(Terminal);
        //    PrintPointIII_Desconectar();
        //    return true;
        //}

        public void PrintPointIII_InsereFuncionarioMasterListaEnvio(string Pis, string Cracha, string Senha, string cpf)
        {
            log.AddLog(String.Format(AssepontoRep.Consts.SUPERVISOR_ENVIANDO, Pis));
            _watchComm.AddMaster(Pis, Cracha, Senha, true, true, true, true, cpf);
        }

        public bool PrintPointIII_EnviaFuncionariosMasterListaEnvio()
        {
            bool Result = false;

            try
            {
                PrintPointIII_Conectar();
                _watchComm.SendMasterList();

                log.AddLog(AssepontoRep.Consts.SUPERVISOR_ENVIADO);
                Result = true;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }
            finally
            {
                PrintPointIII_Desconectar();
            }

            return Result;
        }

        #region Private
        private void InstanciaWatchComm()
        {
            string key = AbreArquivoChaveComunicacao();

            AbreArquivoChaveRSA();

            org.cesar.dmplight.watchComm.api.TCPComm tcpComm =
                new org.cesar.dmplight.watchComm.api.TCPComm(TerminalDados.IP, TerminalDados.Porta);

            tcpComm.SetTimeOut(15000);

            _watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.PrintPointIII,
                                                                              tcpComm,
                                                                              1,
                                                                              key,
                                                                              org.cesar.dmplight.watchComm.api.WatchConnectionType.ConnectedMode,
                                                                              "01.00.0000",
                                                                              _chavePublica_RSA,
                                                                              _expoente_RSA,
                                                                              TerminalDados.OperadorLogin,
                                                                              TerminalDados.OperadorSenha,
                                                                              TerminalDados.OperadorCpf);
        }

        private void AbreArquivoChaveRSA()
        {
            string[] arquivos = System.IO.Directory.GetFiles(this.CaminhoEXE(), TerminalDados.IP.Replace(".","") + "CHAVE*");

            if (arquivos != null && arquivos.Length > 0)
            {
                System.IO.StreamReader streamReader = System.IO.File.OpenText(arquivos[0]);

                _chavePublica_RSA = streamReader.ReadLine();
                _expoente_RSA = streamReader.ReadLine();

                streamReader.Close();
            }
        }

        private string AbreArquivoChaveComunicacao()
        {
            string key = "";

            if (System.IO.File.Exists(this.CaminhoEXE() + "\\key.txt"))
            {
                System.IO.StreamReader streamReader = System.IO.File.OpenText(this.CaminhoEXE() + "\\key.txt");
                key = streamReader.ReadLine();

                streamReader.Close();
            }

            return key;
        }

        private String CaminhoEXE()
        {
            String nomeEXE = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath).InternalName;
            String retorno = System.Windows.Forms.Application.ExecutablePath.ToUpper().Replace(nomeEXE.ToUpper(), "");

            return retorno;
        }

        public bool PrintPointIII_Conectar()
        {
            if (_watchComm == null)
                InstanciaWatchComm();

            try
            {
                _watchComm.OpenConnection();
                return true;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
                return false;
            }
        }

        public bool PrintPointIII_Desconectar()
        {
            try
            {
                _watchComm.CloseConnection();
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
            }
            _watchComm = null;
            return true;
        }
        #endregion
    }
}
