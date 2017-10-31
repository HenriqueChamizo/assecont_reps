
using System.Windows.Forms;
using System;
using System.IO;
using Wr.Classes;
using System.Collections.Generic;
using AssepontoRep;
using org.cesar.dmplight.watchComm.business;

namespace Dimep
{
    public class Rede
    {
        string IP;
        int Porta;
        public Log log;
        private org.cesar.dmplight.watchComm.impl.WatchComm _watchComm;

        public Rede(string ip, int Porta, TextBox textbox)
        {
            this.IP = ip;
            this.Porta = Porta;
            log = new Log(textbox, IP);
            InstanciaWatchComm();
        }

        private void InstanciaWatchComm()
        {
            org.cesar.dmplight.watchComm.api.TCPComm tcpComm =
                new org.cesar.dmplight.watchComm.api.TCPComm(IP, Porta);

            tcpComm.SetTimeOut(15000);

            _watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.PrintPoint,
                tcpComm, 1, "", org.cesar.dmplight.watchComm.api.WatchConnectionType.ConnectedMode, "01.00.0000");
        }

        public bool Dimep_Conectar()
        {
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

        public bool Dimep_EnviaEmpresa(string IdentificadorTipo, string Identificador, string Nome, string Cei, string Endereco)
        {
            bool Result = false;

            org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType tipoEmpregador =
                (IdentificadorTipo == "J") ? org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CNPJ : org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CPF;

            log.AddLog(AssepontoRep.Consts.EMPREGADOR_ENVIANDO);

            try
            {
                Dimep_Conectar();
                _watchComm.ChangeEmployer(tipoEmpregador, Identificador, Cei, Nome, Endereco);

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

            return Result;
        }

        public bool Dimep_AtualizaDataHora()
        {
            bool Result = false;

            log.AddLog(AssepontoRep.Consts.DATA_HORA_ENVIANDO);
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

        public void Dimep_InsereFuncionarioListaEnvio(string Nome, string Pis, string Cracha)
        {
            string Senha = "";

            log.AddLog(String.Format(AssepontoRep.Consts.FUNCIONARIO_ENVIANDO, Nome));
            _watchComm.AddEmployee(Pis, Nome, Senha);
            _watchComm.AddCredential(Cracha, Pis, 0);
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

        public bool Dimep_EnviaConfiguracao()
        {
            bool Result = false;

            log.AddLog(AssepontoRep.Consts.CONFIGURACAO_ENVIANDO);
            try
            {
                Dimep_Conectar();
                //_watchComm.InquiryFingerPrint(1);
                _watchComm.AddConfiguration(EConfigurationType.Format2Of5Intercalary, "IIIIII");
                //_watchComm.AddConfiguration(EConfigurationType.Enabled2of5Dimep, true); 
                //_watchComm.AddConfiguration(EConfigurationType.Format2of5Dimep, "OOOOOOOOOOOOOOIIIIII");
                //_watchComm.SendSettings();
                log.AddLog(AssepontoRep.Consts.CONFIGURACAO_ENVIADA);
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

        public bool Dimep_ExcluirFuncionario(string Nome, string Pis)
        {
            bool Result = false;
            log.AddLog(String.Format(AssepontoRep.Consts.FUNCIONARIOS_EXCLUINDO, Nome));

            try
            {
                _watchComm.AddEmployee(Pis);
                this._watchComm.ExcludeEmployeesList();
                log.AddLog(String.Format(AssepontoRep.Consts.FUNCIONARIO_EXCLUIDO, Nome));
                Result = true;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
            }

            return Result;
        }

        public bool Dimep_ImportarMarcacoes(int Terminal, string TerminalNome, Marcacoes marcacoes)
        {
            bool Result = false;
            DBApp db = new DBApp();
            int ProximoNsr = db.getLastNsr(Terminal) + 1;

            int Contador = 0;

            MRPRecord[] registrosMRP;

            log.AddLog(AssepontoRep.Consts.INICIALIZANDO_IMPORTACAO_ARQUIVO);

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
                                Contador++;

                                if (Nsr > ProximoNsr)
                                    ProximoNsr = Nsr;
                            }
                        }

                        registrosMRP = _watchComm.ConfirmationReceiptMRPRecords();
                    }

                    if (Contador > 0)
                    {
                        string Arquivo = marcacoes.SaveToFile();
                        log.AddLog(String.Format(AssepontoRep.Consts.ARQUIVO_GERADO, Arquivo));

                        Result = true;

                        db.setLastNsr(Terminal, ProximoNsr);
                    }
                    else
                    {
                        log.AddLog(AssepontoRep.Consts.SEM_MARCACOES);
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
    }
}
