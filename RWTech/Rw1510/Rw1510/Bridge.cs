using System.Windows.Forms;
using AssepontoRep;
using System;
using System.Text;
using Wr.Classes;
using System.Collections.Generic;
using System.IO;



namespace Rw1510
{
    public class Bridge : AssepontoRep.Bridge
    {
        private TextBox consoleLog;

        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
            consoleLog = edLog;
        }

        #region Override Abstract
        public override string getArquivoUsb()
        {
            return "Trilobit";
        }
        public override string getRepFabricante()
        {
            return "Trilobit";
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

        public override int getPortaPadrao()
        {
            return 1001;
        }
        #endregion

        #region CONEXAO
        private bool Connect(string Ip, int Porta)
        {
            //char* a = '0';

            char a = Authotelcom.configura("AR", "192.168.1.181", null, 1001, '0', "01/04/2014 15:30:00", "01/08/2014 15:30:00", null, null, null, 2, 0, 0, 0, 0, 0, 0, 9600);


            return true;
        }

        public override bool sendDataHora(int Terminal)
        {
            if(!base.sendDataHora(Terminal)) return false;

            Connect(TerminalDados.IP, TerminalDados.Porta);

            return Connect(Terminal);
        }
        #endregion
    }
}
