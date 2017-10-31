using System.Windows.Forms;
using AssepontoRep;
using System;
using iddata.async_communication.business.client;

namespace IdData
{
    public class Bridge : AssepontoRep.Bridge
    {
        private TextBox consoleLog;
        private CSocketClient objSocketClient;
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
               // this.objIDSysR30 = objIDSysR30;
                this.objSocketClient = new CSocketClient(TerminalDados.IP, TerminalDados.Porta);

                this.objSocketClient.Connect();

               // return this.Connection_Verification();
            }
            catch (Exception ex)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                return false;
            }
        }

        public override bool Disconnect(int Terminal)
        {
            try
            {
                this._objController.DisconnectServer();
                return true;
            }
            catch (Exception ex)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
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

                Connect(Terminal);

                //this.Connection_Send(this.objIDSysR30.SetDateTime((byte)DateTime.Now.Day, (byte)DateTime.Now.Month, (ushort)DateTime.Now.Year, (byte)DateTime.Now.Hour, (byte)DateTime.Now.Minute, (byte)DateTime.Now.Second));

                try
                {
                    this._objController.SetDateTime(this.objIDSysR30.SetDateTime((byte)DateTime.Now.Day, (byte)DateTime.Now.Month, (ushort)DateTime.Now.Year, (byte)DateTime.Now.Hour, (byte)DateTime.Now.Minute, (byte)DateTime.Now.Second));
                    return true;
                }
                catch (Exception exError)
                {
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    return false;
                }


                if (iReturn == 0)
                {
                    log.AddLog(Consts.DATA_HORA_ATUALIZADA_SUCESSO);
                    return true;
                }
                else
                {
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    return false;
                }
            }
            catch (Exception exError)
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                return false;
            }           
        }
        #endregion
    }
}
