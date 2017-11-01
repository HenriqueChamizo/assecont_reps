
using System.Windows.Forms;
using System;
using System.IO;
using Wr.Classes;
using System.Collections.Generic;
using AssepontoRep;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace KeyPass
{
    public class Rede
    {
        string IP;
        int Porta;
        string ChaveAcesso = "0";
        public Log log;

        [DllImport("Keypass1510.dll")]
        public static extern Int32 KP1510_ConfigurarDataHoraEquipamento(char* EnderecoIpAtual, byte[] ChaveAcesso, byte[] Porta, byte[] DataHora);

        enum CodigosRetorno
        {
            [Description("Ok")]
            Ok = 1,
            [Description("Falha de comunicação com o equipamento")]
            FalhaComunicacao = -1,
            [Description("Falha ao executar comando")]
            FalhaExecucaoComando = -2,
            [Description("Parâmetro(s) inválido(s)")]
            ParametroInvalido = -5,
            [Description("Falha ao desconectar do equipamento")]
            FalhaDesconectar = -6,
            [Description("Erro desconhecido")]
            ErroDesconhecido = -10,
        }

        public Rede(string ip, int Porta, TextBox textbox)
        {
            this.IP = ip;
            this.Porta = Porta;
            log = new Log(textbox, IP);
            //InstanciaWatchComm();
        }

        public bool KeyPass_AtualizaDataHora()
        {
            bool Result = false;

            log.AddLog(AssepontoRep.Consts.DATA_HORA_ENVIANDO);

            try
            {
                string DataHora = String.Format("{0:yyyyMMddhhmmss}", DateTime.Now);

                int result = KP1510_ConfigurarDataHoraEquipamento(IP, ChaveAcesso, Porta.ToString(), DataHora);

                if (result > 0)
                {
                    log.AddLog(AssepontoRep.Consts.DATA_HORA_ATUALIZADA_SUCESSO);
                    Result = true;
                }
                else
                {
                    log.AddLog(Wr.Classes.Utils.GetDescription((CodigosRetorno)result));
                }
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message.ToString());
            }

            return Result;
        }

                //public bool KeyPass_Conectar()
                //{
                //    try
                //    {
                //        _watchComm.OpenConnection();
                //    }
                //    catch (Exception ex)
                //    {
                //        log.AddLog(ex.Message);
                //    }
                //    return true;
                //}

                //public bool KeyPass_Desconectar()
                //{
                //    try
                //    {
                //        _watchComm.CloseConnection();
                //    }
                //    catch (Exception ex)
                //    {
                //        log.AddLog(ex.Message);
                //    }
                //    return true;
                //}

                public bool KeyPass_EnviaEmpresa(string IdentificadorTipo, string Identificador, string Nome, string Cei, string Endereco)
                {
                    bool Result = false;

                    //org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType tipoEmpregador =
                    //    (IdentificadorTipo == "J") ? org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CNPJ : org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CPF;

                    //log.AddLog(Consts.EMPREGADOR_ENVIANDO);

                    //try
                    //{
                    //    KeyPass_Conectar();
                    //    _watchComm.ChangeEmployer(tipoEmpregador, Identificador, Cei, Nome, Endereco);

                    //    log.AddLog(Consts.EMPREGADOR_ENVIADO_SUCESSO);
                    //    Result = true;
                    //}
                    //catch (Exception ex)
                    //{
                    //    log.AddLog(ex.Message.ToString());
                    //}
                    //finally
                    //{
                    //    KeyPass_Desconectar();
                    //}

                    return Result;
                }

                public void KeyPass_InsereFuncionarioListaEnvio(string Nome, string Pis, string Cracha)
                {
                    string Senha = "";

                    log.AddLog(String.Format(AssepontoRep.Consts.FUNCIONARIO_ENVIANDO, Nome));
                    //_watchComm.AddEmployee(Pis, Nome, Senha);
                    //_watchComm.AddCredential(Cracha, Pis, 0);
                }

                public void KeyPass_InsereFuncionarioMasterListaEnvio(string Pis, string Cracha, string Senha)
                {
                    log.AddLog(String.Format(AssepontoRep.Consts.SUPERVISOR_ENVIANDO, Pis));
                    //_watchComm.AddMaster(Pis, Cracha, Senha, true, true, true, true);
                }

                public bool KeyPass_EnviaFuncionariosListaEnvio()
                {
                    bool Result = false;

                    //try
                    //{
                    //    _watchComm.IncludeEmployeesList(true, false);
                    //    _watchComm.IncludeCredentialList(true);
                    //    log.LogOk();
                    //    Result = true;
                    //}
                    //catch (Exception ex)
                    //{
                    //    log.AddLog(ex.Message.ToString());
                    //}

                    return Result;
                }

                public bool KeyPass_EnviaFuncionariosMasterListaEnvio()
                {
                    bool Result = false;

                    //try
                    //{
                    //    KeyPass_Conectar();
                    //    _watchComm.SendMasterList();

                    //    log.AddLog(Consts.SUPERVISOR_ENVIADO);
                    //    Result = true;
                    //}
                    //catch (Exception ex)
                    //{
                    //    log.AddLog(ex.Message.ToString());
                    //}
                    //finally
                    //{
                    //    KeyPass_Desconectar();
                    //}

                    return Result;
                }

                public bool KeyPass_EnviaConfiguracao()
                {
                    bool Result = false;

                    //log.AddLog(Consts.CONFIGURACAO_ENVIANDO);
                    //try
                    //{
                    //    KeyPass_Conectar();
                    //    //_watchComm.InquiryFingerPrint(1);
                    //    _watchComm.AddConfiguration(EConfigurationType.Format2Of5Intercalary, "IIIIII");
                    //    //_watchComm.AddConfiguration(EConfigurationType.Enabled2of5KeyPass, true); 
                    //    //_watchComm.AddConfiguration(EConfigurationType.Format2of5KeyPass, "OOOOOOOOOOOOOOIIIIII");
                    //    //_watchComm.SendSettings();
                    //    log.AddLog(Consts.CONFIGURACAO_ENVIADA);
                    //    Result = true;
                    //}
                    //catch (Exception ex)
                    //{
                    //    log.AddLog(ex.Message.ToString());
                    //}
                    //finally
                    //{
                    //    KeyPass_Desconectar();
                    //}

                    return Result;
                }

                public bool KeyPass_ExcluirFuncionario(string Nome, string Pis)
                {
                    bool Result = false;
                    log.AddLog(String.Format(AssepontoRep.Consts.FUNCIONARIOS_EXCLUINDO, Nome));

                    //try
                    //{
                    //    _watchComm.AddEmployee(Pis);
                    //    this._watchComm.ExcludeEmployeesList();
                    //    log.AddLog(String.Format(Consts.FUNCIONARIO_EXCLUIDO, Nome));
                    //    Result = true;
                    //}
                    //catch (Exception ex)
                    //{
                    //    log.AddLog(ex.Message);
                    //}

                    return Result;
                }

                public bool KeyPass_ImportarMarcacoes(int Terminal, string TerminalNome, Marcacoes marcacoes)
                {
                    bool Result = false;
                    //DBApp db = new DBApp();
                    //int ProximoNsr = db.getLastNsr(Terminal) + 1;

                    //int Contador = 0;

                    //MRPRecord[] registrosMRP;

                    //log.AddLog(Consts.INICIALIZANDO_IMPORTACAO_ARQUIVO);

                    //if (KeyPass_Conectar())
                    //{
                    //    try
                    //    {
                    //        _watchComm.RepositioningMRPRecordsPointer(ProximoNsr.ToString()); 
                    
                    //        registrosMRP = this._watchComm.InquiryMRPRecords(false, false, true, false);

                    //        while (registrosMRP != null)
                    //        {
                    //            foreach (MRPRecord registroMRP in registrosMRP)
                    //            {
                    //                if (registroMRP is MRPRecord_RegistrationMarkingPoint)
                    //                {
                    //                    string Pis = ((MRPRecord_RegistrationMarkingPoint)registroMRP).Pis;

                    //                    DateTime DataHora = ((MRPRecord_RegistrationMarkingPoint)registroMRP).DateTimeMarkingPoint;
                    //                    int Nsr = Convert.ToInt32(((MRPRecord_RegistrationMarkingPoint)registroMRP).NSR);
                    //                    marcacoes.Add(Pis, DataHora, Nsr);
                    //                    Contador++;

                    //                    if (Nsr > ProximoNsr)
                    //                        ProximoNsr = Nsr;
                    //                }
                    //            }

                    //            registrosMRP = _watchComm.ConfirmationReceiptMRPRecords();
                    //        }

                    //        if (Contador > 0)
                    //        {
                    //            string Arquivo = marcacoes.SaveToFile();
                    //            log.AddLog(String.Format(Consts.ARQUIVO_GERADO, Arquivo));

                    //            Result = true;

                    //            db.setLastNsr(Terminal, ProximoNsr);
                    //        }
                    //        else
                    //        {
                    //            log.AddLog(Consts.SEM_MARCACOES);
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        log.AddLog(ex.Message);
                    //    }
                    //    finally
                    //    {
                    //        KeyPass_Desconectar();
                    //    }
                    //}

                    return Result;
                }
    }
}
