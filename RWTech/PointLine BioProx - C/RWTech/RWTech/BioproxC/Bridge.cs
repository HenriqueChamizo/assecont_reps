using AssepontoRep;
using BioproxC.RWTech.Comandos;
using BioproxC.RWTech.Common;
using BioproxC.RWTech.ProtocoloREP.Comandos;
using BioproxC.RWTech.Utils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BioproxC
{
    public class Bridge : AssepontoRep.Bridge
    {
        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
        }

        #region Override Abstract
        public override bool getAutenticacao() { return true; }
        public override bool getBoxFuncoes() { return false; }
        public override bool getCadastroTerminalResponsavel() { return false; }
        public override bool getCadastroTerminalSupervisor() { return false; }
        public override bool getContemChaveAcessoREP() { return false; }
        public override bool getEnviarHorarioVerao() { return false; }
        public override bool getEnviarHorarioVeraoUsb() { return false; }
        public override bool getFuncionariosAlteradosUsb() { return false; }
        public override bool getGerarBackup() { return false; }
        public override bool getGerarBiometria() { return false; }
        public override bool getGerarUsb() { return false; }
        public override bool getGerarUsbEmpregador() { return false; }
        public override int GetHashCode() { return base.GetHashCode(); }
        public override bool getNumeroSerieREP() { return false; }
        public override bool getPin() { return true; }
        public override int getPortaPadrao() { return 1001; }
        public override string getRepFabricante() { return "RWTech - Bioprox C"; }
        public override string getArquivoUsb() { return ""; }
        public override bool getColumnId() { return false; }
        public override bool getDisconnectOnExit() { return false; }
        public override List<Types.Permissao> getPermissoes() { return null; }
        #endregion

        public override bool Connect(int Terminal)
        {
            return Validacao.isDadosConexaoOK(TerminalDados.IP, TerminalDados.Porta.ToString());
        }

        public override bool sendDataHora(int Terminal)
        {
            base.sendDataHora(Terminal);
            
            if (Connect(Terminal))
            {
                LogManager logManager = LogManager.getInstance();
                logManager.ocultarBytesCriptografados = false;
                logManager.ocultarBytesDescriptografados = false;
                EnviaDataHora enviaDataHora = new EnviaDataHora();
                FlagErroComando resultado = enviaDataHora.execute(TerminalDados.IP, TerminalDados.Porta.ToString(), TerminalDados.AutenticacaoCpf, TerminalDados.Pin, ref logManager);//TerminalDados.Pin);
                logManager.resultados.Add(resultado.getMensagem() + "(Código: " + resultado.getErroStrHex() + ")");
                foreach (string r in logManager.resultados)
                {
                    log.AddLog(r);
                }
                return true;
            }
            else
            {
                Disconnect(Terminal);
                log.AddLog("SERVIDOR OFFLINE");
                log.AddLineBreak();
                return false;
            }
        }
    }
}
