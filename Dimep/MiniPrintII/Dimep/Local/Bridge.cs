using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AssepontoRep;
using org.cesar.dmplight.watchComm.business;
using org.cesar.dmplight.watchComm.impl.printpoint;
using Wr.Classes;

namespace Dimep
{
    public class Bridge : AssepontoRep.Bridge
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

        public override bool getGerarBiometria()
        {
            return true;
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

            //log.AddLog(AssepontoRep.Consts.EMPREGADOR_ENVIANDO);

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

        public override bool sendFuncionarios(int Terminal, List<AssepontoRep.Types.Funcionario> listFuncionarios)
        {
            if (base.sendFuncionarios(Terminal, listFuncionarios))
            {
                return (Dimep_EnviaFuncionariosListaEnvio());

            }
            else
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
            return false;
        }

        private bool Dimep_EnviaFuncionariosListaEnvio()
        {
            bool Result = false;

            try
            {
                Dimep_Conectar();
                _watchComm.IncludeEmployeesList(true, false);
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
                Dimep_Desconectar();
            }

            return Result;
        }

        public override bool LerMarcacoes(Marcacoes marcacoes, TipoImportacaoMarcacoes tipoimportacao)
        {
            base.LerMarcacoes(marcacoes, tipoimportacao);

            bool Result = false;
            DBApp db = new DBApp();
            int ProximoNsr = db.getLastNsr(TerminalDados.Indice) + 1;
            int Contador = 0;

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
                Dimep_Conectar();
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
                Dimep_Desconectar();
            }

            return Result;
        }

        public override bool GerarArquivoBiometria(int Terminal)
        {
            base.GerarArquivoBiometria(Terminal);
            List<AssepontoRep.Types.Biometria> listBiometrias = new List<Types.Biometria>();
            Types.Biometria Biometria = new Types.Biometria();
            string folder = AssepontoRep.Folders.folderArquivoBiometria(getRepFabricante());

            PrintPointFingerPrintMessage template;

            try
            {
                Dimep_Conectar();
                template = this._watchComm.InquiryFingerPrint(InquiryFingerPrintType.All);

                if (template == null)
                {
                    log.AddLog("Nenhum template recebido");
                    return false;
                }
                else
                {
                    while (template != null)
                    {
                        Biometria.Funcionario.Pis = template.PIS;
                        Biometria.Dedo1 = (Int16)template.FingerprintTypeOne;
                        Biometria.Dedo2 = (Int16)template.FingerprintTypeTwo;
                        Biometria.template = template.FingerPrint;

                        template = this._watchComm.ConfirmationReceiptFingerPrint();

                        listBiometrias.Add(Biometria);

                        if (template == null)
                        {
                            FinalizarArquivoBiometria(folder, listBiometrias);
                            log.AddLog(Consts.BIOMETRIA_ARQUIVO_GERADO_SUCESSO);
                            return true;
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                return false;
            }
            finally
            {
                Dimep_Desconectar();
            }
        }

        public override void FinalizarArquivoBiometria(string Folder, List<AssepontoRep.Types.Biometria> listBiometrias)
        {
            List<string> Arquivos = new List<string>();
            Files.DirSearch(Folder, "*.*", Arquivos);

            foreach (string arquivo in Arquivos)
            {
                File.Delete(arquivo);
            }

            Arquivos = new List<string>();

            foreach (Types.Biometria arquivo in listBiometrias)
            {
                Arquivos.Add(arquivo.Funcionario.Pis);
                Arquivos.Add(Convert.ToString(arquivo.Dedo1));
                Arquivos.Add(Convert.ToString(arquivo.Dedo2));
                Arquivos.Add(arquivo.template);
                Arquivos.Add("+");
            }

            Files.WriteFile(Folder + "biometria.txt", Arquivos);
        }

        public override bool EnviarArquivoBiometria(int Terminal)
        {
            base.EnviarArquivoBiometria(Terminal);

            int contadoLinha = 0;
            List<string> Arquivos = new List<string>();
            List<AssepontoRep.Types.Biometria> listBiometrias = new List<Types.Biometria>();
            Types.Biometria Biometria = new Types.Biometria();

            string folder = AssepontoRep.Folders.folderArquivoBiometria(getRepFabricante());
            Files.ReadFile(folder + "biometria.txt", Arquivos);

            try
            {
                foreach (string linha in Arquivos)
                {
                    contadoLinha++;
                    if (contadoLinha == 1)
                    {
                        Biometria.Funcionario.Pis = linha.Substring(0, 11);
                    }
                    if (contadoLinha == 2)
                    {
                        Biometria.Dedo1 = Convert.ToInt16(linha.Substring(0, 1));
                    }
                    if (contadoLinha == 3)
                    {
                        Biometria.Dedo2 = Convert.ToInt16(linha.Substring(0, 1));
                    }
                    if (contadoLinha == 4)
                    {
                        Biometria.template = linha.Substring(0, 1544);
                    }
                    if (linha.Substring(0, 1) == "+")
                    {
                        listBiometrias.Add(Biometria);
                        contadoLinha = 0;
                    }
                }

                Dimep_Conectar();

                EfingerPrintSensor fingerPrintSensor;
                fingerPrintSensor = EfingerPrintSensor.Suprema;
                if (fingerPrintSensor != EfingerPrintSensor.Fugitsu)
                {
                    foreach (Types.Biometria arquivo in listBiometrias)
                    {
                        this._watchComm.IncludeFingerPrint(arquivo.Funcionario.Pis,
                                    arquivo.template,
                                    (EFingerPrintType)Int16.Parse((arquivo.Dedo1).ToString()),
                                    (EFingerPrintType)Int16.Parse((arquivo.Dedo2).ToString()),
                                    fingerPrintSensor);
                    }

                    log.AddLog(Consts.BIOMETRIA_ARQUIVO_ENVIADO_SUCESSO);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                return false;
            }
            finally
            {
                Dimep_Desconectar();
            }
        }

        public override bool EnviarHorarioVerao(int Terminal, string inicio, string fim)
        {
            try
            {
                base.EnviarHorarioVerao(Terminal, inicio, fim);
                
                Dimep_Conectar();

                DateTime Inicio = Convert.ToDateTime(inicio);
                DateTime Fim = Convert.ToDateTime(fim);

                inicio = Inicio.ToString("dd/MM/yyyy HH:mm");
                fim = Fim.ToString("dd/MM/yyyy HH:mm");

                this._watchComm.AddParcialConfiguration(ParcialConfigurationType.DST,
                                                            inicio,
                                                            fim);
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
                Dimep_Desconectar();
            }
        }
        #endregion

        #region Private
        private void InstanciaWatchComm()
        {
            org.cesar.dmplight.watchComm.api.TCPComm tcpComm =
                new org.cesar.dmplight.watchComm.api.TCPComm(TerminalDados.IP, TerminalDados.Porta);

            tcpComm.SetTimeOut(15000);

            _watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.MiniPrint,
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
