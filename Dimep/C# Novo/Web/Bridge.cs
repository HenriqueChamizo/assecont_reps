using System;
using System.Windows.Forms;
using AssepontoRep;
using org.cesar.dmplight.watchComm.business;

namespace Dimep
{
    class Bridge : AssepontoRep.Bridge
    {
        private org.cesar.dmplight.watchComm.impl.WatchComm _watchComm;

        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
        }

        #region Override Abstract
        public override int getPortaPadrao()
        {
            return 3000;
        }

        public override bool getPin()
        {
            return false;
        }

        public override bool getGerarUsbEmpregador()
        {
            return false;
        }

        public override bool getFuncionariosAlteradosUsb()
        {
            return false;
        }

        public override bool getEnviarHorarioVerao()
        {
            return false;
        }

        public override bool getNumeroSerieREP()
        {
            return false;
        }

        public override bool getBoxFuncoes()
        {
            return false;
        }

        public override bool getGerarBiometria()
        {
            return false;
        }

        public override bool getContemChaveAcessoREP()
        {
            return false;
        }

        public override bool getAutenticacao()
        {
            return false;
        }

        public override bool getCadastroTerminalResponsavel()
        {
            return false;
        }

        public override bool getCadastroTerminalSupervisor()
        {
            return false;
        }

        public override bool getEnviarHorarioVeraoUsb()
        {
            return false;
        }

        public override string getRepFabricante()
        {
            return "Dimep";
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
            return true;
        }
        #endregion
        #region Override
        public override bool sendDataHora(int Terminal)
        {
            base.sendDataHora(Terminal);
            bool Result = false;

            DateTime dtEnvio = DateTime.Now;

            try
            {
                Dimep_Conectar();
                _watchComm.SetDateTime(dtEnvio);
                log.AddLog(AssepontoRep.Consts.DATA_HORA_ATUALIZADA_SUCESSO);
                Result = true;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }
            finally
            {
                Dimep_Desconectar();
            }

            return Result;
        }

        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            base.sendInfoEmpresa(Terminal, out EmpregadorDados);

            bool Result = false;

            org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType tipoEmpregador =
                (org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType)(int)(EmpregadorDados.PessoaTipo);
                //(IdentificadorTipo == "J") ? org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CNPJ : org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CPF;

            log.AddLog(AssepontoRep.Consts.EMPREGADOR_ENVIANDO);

            try
            {
                Dimep_Conectar();
                _watchComm.ChangeEmployer(tipoEmpregador, EmpregadorDados.Pessoa, EmpregadorDados.Cei, EmpregadorDados.Nome, EmpregadorDados.Endereco);

                log.AddLog(AssepontoRep.Consts.EMPREGADOR_ENVIADO_SUCESSO);
                Result = true;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }
            finally
            {
                Dimep_Desconectar();
            }

            Dimep_InsereFuncionarioMasterListaEnvio(TerminalDados.SupervisorPis, TerminalDados.SupervisorCodigo.ToString(), TerminalDados.SupervisorSenha.ToString());
            Dimep_EnviaFuncionariosMasterListaEnvio();

            return Result;
        }

        public override bool Connect(int Terminal)
        {
            base.Connect(Terminal);
            return Dimep_Conectar();
        }

        public override bool Disconnect(int Terminal)
        {
            base.Disconnect(Terminal);
            Dimep_Desconectar();
            return true;
        }

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

        public override bool LerMarcacoes(Marcacoes marcacoes, TipoImportacaoMarcacoes tipoimportacao)
        {
            base.LerMarcacoes(marcacoes, tipoimportacao);

            bool Result = false;
            DBApp db = new DBApp();
            int ProximoNsr = db.getLastNsr(TerminalDados.Indice) + 1;

            MRPRecord[] registrosMRP;

            if (Dimep_Conectar())
            {
                try
                {
                    _watchComm.RepositioningMRPRecordsPointer(ProximoNsr.ToString());

                    registrosMRP = this._watchComm.InquiryMRPRecords(false, false, true, false);

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

                                if (Nsr > ProximoNsr)
                                    ProximoNsr = Nsr;
                            }
                        }

                        registrosMRP = _watchComm.ConfirmationReceiptMRPRecords();
                    }
                }
                catch (Exception ex)
                {
                    log.AddLog(ex.Message);
                }
                finally
                {
                    Dimep_Desconectar();
                }
            }

            return Result;
        }

        public override bool deleteFuncionario(Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);

            bool Result = false;

            try
            {
                _watchComm.AddEmployee(Funcionario.Pis);
                this._watchComm.ExcludeEmployeesList();
                log.AddLog(String.Format(AssepontoRep.Consts.FUNCIONARIO_EXCLUIDO, Funcionario.Nome));
                Result = true;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
            }

            return Result;
        }
        #endregion

        #region Private
        private void InstanciaWatchComm()
        {
            org.cesar.dmplight.watchComm.api.TCPComm tcpComm =
                new org.cesar.dmplight.watchComm.api.TCPComm(TerminalDados.IP, TerminalDados.Porta);

            tcpComm.SetTimeOut(15000);

            _watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.PrintPoint,
                tcpComm, 1, "", org.cesar.dmplight.watchComm.api.WatchConnectionType.ConnectedMode, "01.00.0000");
        }

        public bool Dimep_Conectar()
        {
            if (_watchComm == null)
                InstanciaWatchComm();

            try
            {
                _watchComm.OpenConnection();
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
            }
            return true;
        }

        public bool Dimep_Desconectar()
        {
            try
            {
                _watchComm.CloseConnection();
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
            }
            return true;
        }

        public void Dimep_InsereFuncionarioMasterListaEnvio(string Pis, string Cracha, string Senha)
        {
            log.AddLog(String.Format(AssepontoRep.Consts.SUPERVISOR_ENVIANDO, Pis));
            _watchComm.AddMaster(Pis, Cracha, Senha, true, true, true, true);
        }

        public bool Dimep_EnviaFuncionariosListaEnvio()
        {
            bool Result = false;

            try
            {
                _watchComm.IncludeEmployeesList(true, false);
                _watchComm.IncludeCredentialList(true);
                log.LogOk();
                Result = true;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }

            return Result;
        }

        public bool Dimep_EnviaFuncionariosMasterListaEnvio()
        {
            bool Result = false;

            try
            {
                Dimep_Conectar();
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
                Dimep_Desconectar();
            }

            return Result;
        }

        #endregion
    }
}
