using System.Windows.Forms;
using AssepontoRep;
using System;
//using iddata.async_communication.business.client;
using iddata.interfaceIDSysR30.business;
using iddata.interfaceIDSysR30.controller;
using IDSysR30_CSharp.Controller;
using IDSysR30_CSharp;
using System.Collections.Generic;

namespace IdData
{
    public class Bridge : AssepontoRep.Bridge
    {
        private TextBox consoleLog;
        //private CSocketClient objSocketClient;
        private CIDSysR30 objIDSysR30;
        private CController _objController;
        private bool _bDLL_Loaded;
        private bool _bConnected;

        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
            consoleLog = edLog;
        }

        #region Overrides Abstract
        public override int getPortaPadrao()
        {
            return 7000;
        }

        public override bool getFuncionariosAlteradosUsb()
        {
            return false;
        }

        public override bool getAutenticacao()
        {
            return false;
        }

        public override bool getGerarUsbEmpregador()
        {
            return false;
        }

        public override bool getBoxFuncoes()
        {
            return false;
        }

        public override bool getNumeroSerieREP()
        {
            return false;
        }

        public override bool getPin()
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

        public override bool getContemChaveAcessoREP()
        {
            return false;
        }

        public override string getRepFabricante()
        {
            return "Id Data";
            //return String.Format("Id Data: {0}", Wr.Classes.Net.getLocalIPAddress());
        }
        public override string getArquivoUsb()
        {
            return "Id Data";
        }
        public override bool getEnviarHorarioVerao()
        {
            return false;
        }
        public override bool getEnviarHorarioVeraoUsb()
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
        public override bool getGerarUsb()
        {
            return false;
        }
        #endregion

        #region CONEXAO
        public override bool Connect(int Terminal)
        {
            try
            {
                if (_objController == null)
                    _objController = new CController();

                string strIPAddress = TerminalDados.IP;
                int iPort = TerminalDados.Porta;
                this._objController.ConnectServer(strIPAddress, iPort);
                this._bDLL_Loaded = this._objController.LoadDLL();
                _bConnected = true;
                return true;
            }
            catch (Exception ex)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                log.AddLog("Descrição ", true);
                log.AddLog(ex.Message);
                return false;
            }
        }

        public override bool Disconnect(int Terminal)
        {
            try
            {
                if (_objController != null)
                {
                    this._objController.DisconnectServer();
                    _objController = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                log.AddLog("Descrição ", true);
                log.AddLog(ex.Message);
                _objController = null;
                return false;
            }
        }
        #endregion

        #region override Enviar Data e Hora
        public override bool sendDataHora(int Terminal)
        {
            try
            {
                if (!base.sendDataHora(Terminal)) return false;

                if (this._objController == null)
                    Connect(Terminal);

                byte byDay = (byte)DateTime.Now.Day;
                byte byMonth = (byte)DateTime.Now.Month;
                ushort usYear = (ushort)DateTime.Now.Year;
                byte byHour = (byte)DateTime.Now.Hour;
                byte byMinute = (byte)DateTime.Now.Minute;
                byte bySecond = (byte)DateTime.Now.Second;
                
                //this._objController.SetDateTime(this.objIDSysR30.SetDateTime((byte)DateTime.Now.Day, (byte)DateTime.Now.Month, (ushort)DateTime.Now.Year, (byte)DateTime.Now.Hour, (byte)DateTime.Now.Minute, (byte)DateTime.Now.Second));
                if (this._objController.SetDateTime(byDay, byMonth, usYear, byHour, byMinute, bySecond))
                {
                    log.AddLog("AGUARDE...");
                    int seg = 0;
                    System.Threading.Thread.Sleep(1000);
                    while (this._objController.GetConnectionState() == EConnectionState.SendingData)
                    {
                        //System.Threading.Thread.Sleep(1000);
                        seg++;
                    }
                    log.AddLog(Consts.DATA_HORA_ATUALIZADA_SUCESSO);
                    //log.AddLog(seg.ToString());
                    Disconnect(TerminalDados.Indice);
                    return true;
                }
                else
                {
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    Disconnect(TerminalDados.Indice);
                    return false;
                }
            }
            catch (Exception exError)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                log.AddLog("Descrição ", true);
                log.AddLog(exError.Message);
                return false;
            }           
        }
        #endregion

        #region override Enviar Info de Empregador
        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            if (!base.sendInfoEmpresa(Terminal, out EmpregadorDados)) return false;

            Disconnect(Terminal);
            Connect(Terminal);

            byte byIdentifyType = (byte)(int)EmpregadorDados.PessoaTipo;
            string strCNPJ_CPF = EmpregadorDados.Pessoa;
            string strCEI = EmpregadorDados.Cei;
            string strEmployerName = EmpregadorDados.Nome;
            string strEmployerAddress = EmpregadorDados.Endereco;

            try
            {
                this._objController.SetEmployer(byIdentifyType, strCNPJ_CPF, strCEI, strEmployerName, strEmployerAddress);
                log.AddLog("AGUARDE...");
                int seg = 0;
                System.Threading.Thread.Sleep(1000);
                while (this._objController.GetConnectionState() == EConnectionState.SendingData)
                {
                    //System.Threading.Thread.Sleep(1000);
                    seg++;
                }
                log.AddLog(Consts.EMPREGADOR_ENVIADO_SUCESSO);
                Disconnect(TerminalDados.Indice);
                return true;
            }
            catch (Exception exError)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                log.AddLog("Descrição ", true);
                log.AddLog(exError.Message);
                Disconnect(TerminalDados.Indice);
                return false;
            }
        }
        #endregion

        #region override Enviar Funcionario
        public override bool sendFuncionario(Types.Funcionario Funcionario)
        {
            Disconnect(TerminalDados.Indice);
            Connect(TerminalDados.Indice);

            string strPIS = Funcionario.Pis;
            string strUserName = Funcionario.Nome;
            uint uiKeyCode = Convert.ToUInt32(Funcionario.Crachas[0]);
            string strBarCode = Funcionario.Barras;
            byte byFacilityCode = (byte)Funcionario.Ind;
            ushort usProxCode = 0;
            byte byUserType = 2;
            string strPassword = Funcionario.Crachas[0].ToString();
            System.IO.MemoryStream msPhoto = null;
            ushort usSizeSample = 0;
            byte byQuantitySamples = 0;
            byte[] rgbyBiometrics = new byte[1];

            try
            {
                System.Threading.Thread.Sleep(1000);
                this._objController.AddUser(strPIS, strUserName, uiKeyCode, strBarCode, byFacilityCode, usProxCode, byUserType, strPassword, msPhoto, usSizeSample, byQuantitySamples, rgbyBiometrics);
                log.AddLog("AGUARDE...");
                int seg = 0;
                System.Threading.Thread.Sleep(1000);
                while (this._objController.GetConnectionState() == EConnectionState.SendingData)
                {
                    //System.Threading.Thread.Sleep(1000);
                    seg++;
                }
                log.AddLog(String.Format(Consts.FUNCIONARIO_ENVIADO_COM_SUCESSO, Funcionario.Nome));
                Disconnect(TerminalDados.Indice);
                return true;
            }
            catch (Exception exError)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                log.AddLog("Descrição ", true);
                log.AddLog(exError.Message);
                Disconnect(TerminalDados.Indice);
                return false;
            }
        }
        #endregion

        #region override Delete Funcionario
        public override bool deleteFuncionario(Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);

            Disconnect(TerminalDados.Indice);
            Connect(TerminalDados.Indice);

            string strPIS = Funcionario.Pis;

            try
            {
                System.Threading.Thread.Sleep(300);
                this._objController.DeleteUser(strPIS);
                log.AddLog("AGUARDE...");
                int seg = 0;
                System.Threading.Thread.Sleep(1000);
                while (this._objController.GetConnectionState() == EConnectionState.SendingData)
                {
                    //System.Threading.Thread.Sleep(1000);
                    seg++;
                }
                log.AddLog(String.Format(Consts.FUNCIONARIO_EXCLUIDO, Funcionario.Nome));
                Disconnect(TerminalDados.Indice);
                return true;
            }
            catch (Exception exError)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                log.AddLog("Descrição ", true);
                log.AddLog(exError.Message);
                Disconnect(TerminalDados.Indice);
                return false;
            }
        }
        #endregion

        #region override Ler Marcações
        public override bool LerMarcacoes(Marcacoes marcacoes, AssepontoRep.Bridge.TipoImportacaoMarcacoes tipoimportacao)
        {
            Disconnect(TerminalDados.Indice);
            Connect(TerminalDados.Indice);
            
            List<string> lstrEventData;
            string[] split;
            DBApp bd = new DBApp();
            int last = bd.getLastNsr(TerminalDados.Indice);
            uint uiNSR = Convert.ToUInt32(last == 0 ? 1 : last);

            try
            {
                this._objController.RequestTotalNSR();
                System.Threading.Thread.Sleep(100);
                lstrEventData = this._objController.GetEventData();
                split = lstrEventData[6].Split(new string[] { " : " }, StringSplitOptions.None);
                uint uiMaxNSR = Convert.ToUInt32(split[1]);

                log.AddLog(":: ATENÇÃO :: A IMPORTAÇÃO DESSE RELÓGIO PODE DEMORAR, NÃO FINALIZE");
                log.AddLog("              ATÉ QUE TENHA TERMINADO ...");
                log.AddLog("AGUARDE...");
                System.Threading.Thread.Sleep(100);
                for (uint i = uiNSR; i <= uiMaxNSR; i++)
                {
                    this._objController.RequestEventByNSR(i);
                    int seg = 0;
                    System.Threading.Thread.Sleep(100);
                    EConnectionState state = EConnectionState.SendingData;
                    while (state == EConnectionState.SendingData)
                    {
                        state = this._objController.GetConnectionState();
                        //System.Threading.Thread.Sleep(100);
                        seg++;
                    }
                    if (state == EConnectionState.DataReceived)
                    {
                        this._objController.SetConnectionState(EConnectionState.Connected);


                        System.Threading.Thread.Sleep(100);
                        lstrEventData = this._objController.GetEventData();

                        if (lstrEventData == null)
                        {
                            throw new Exception("Evento não encontrado.");
                        }

                        int tipo = -1;
                        for (int c = 0;c < lstrEventData.Count;c++)
                        {
                            if (lstrEventData[c].Contains("Tipo do Registro"))
                            {
                                split = lstrEventData[c].Split(new string[] { " : " }, StringSplitOptions.None);
                                tipo = Convert.ToInt32(split[1]);
                                break;
                            }
                        }
                        
                        Marcacoes.Marcacao marc;
                        if (tipo == 3)
                        {
                            marc = new Marcacoes.Marcacao();
                            marc.Tipo = Marcacoes.TiposRegistroAfd.Marcacao;
                            split = lstrEventData[7].Split(new string[] { " : " }, StringSplitOptions.None);
                            string nsr = split[1];
                            marc.NSR = Convert.ToInt32(nsr);
                            split = lstrEventData[9].Split(new string[] { " : " }, StringSplitOptions.None);
                            string datetime = split[1].Trim();
                            split = lstrEventData[10].Split(new string[] { " : " }, StringSplitOptions.None);
                            datetime = datetime + " " + split[1].Trim();
                            marc.DataHora = Convert.ToDateTime(datetime);
                            split = lstrEventData[11].Split(new string[] { " : " }, StringSplitOptions.None);
                            string pis = split[1].Replace(".", "").Replace("-", "").Trim();
                            marc.Pis = pis;

                            marcacoes.Add(marc);
                        }
                    }
                }
                bd.setLastNsr(TerminalDados.Indice, (int)uiMaxNSR);
                bool retorno = (marcacoes.Count > 0);

                Disconnect(TerminalDados.Indice);
                return retorno;
            }
            catch (Exception exError)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                log.AddLog("Descrição ", true);
                log.AddLog(exError.Message);
                Disconnect(TerminalDados.Indice);
                return false;
            }
        }
        #endregion
    }
}
