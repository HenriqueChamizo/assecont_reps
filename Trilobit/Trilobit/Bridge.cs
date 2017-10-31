using AssepontoRep;
using RepTrilobit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trilobit
{
    public class Bridge : AssepontoRep.Bridge
    {
        public REP Rep;
        private TextBox consoleLog;
        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
            consoleLog = edLog;
        }
        #region Override Abstract
        public override int getPortaPadrao() { return 19001; }
        public override string getRepFabricante() { return "Trilobit"; }
        public override string getArquivoUsb() { return ""; }
        public override bool getGerarUsb() { return true; }
        public override bool getGerarBackup() { return false; }
        public override bool getGerarBiometria() { return false; }
        public override bool getEnviarHorarioVerao() { return true; }
        public override bool getEnviarHorarioVeraoUsb() { return false; }
        public override bool getFuncionariosAlteradosUsb() { return false; }
        public override bool getGerarUsbEmpregador() { return false; }
        public override bool getCadastroTerminalSupervisor() { return true; }
        public override bool getNumeroSerieREP() { return false; }
        public override bool getContemChaveAcessoREP() { return true; }
        public override bool getBoxFuncoes() { return false; }
        public override bool getCadastroTerminalResponsavel() { return false; }
        public override bool getAutenticacao() { return false; }
        public override bool getPin() { return false; }
        public override bool getDisconnectOnExit() { return false; }
        public override bool getColumnId() { return false; }
        public override List<Types.Permissao> getPermissoes() { return null; }
        #endregion

        public void LogErro()
        {
            log.AddLog(Rep.ErrorException.Message, true);
        }

        #region CONEXAO
        public override bool Connect(int Terminal)
        {
            Rep = new REP();
            return true;
        }
        #endregion

        #region Overrides ENVIAR DATA E HORA
        public override bool sendDataHora(int Terminal)
        {
            base.sendDataHora(Terminal);
            Connect(Terminal);
            string DataHoraFormatada = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
            //string DataHoraFormatada = String.Format("{0:yyyyMMddhhmmss}", new DateTime(2011, 05, 09, 10, 0, 0));

            if (Rep.EnviarConfiguracao(TerminalDados.IP, TerminalDados.Porta, Convert.ToInt32(TerminalDados.SupervisorSenha), REP.eParamSetConfig.AjusteRelogio, DataHoraFormatada))
            {
                log.AddLog("DATA E HORA ATUALIZADA COM SUCESSO");
                return true;
            }
            else
            {
                LogErro();
                return false;
            }
        }
        #endregion

        #region Overrides ENVIAR EMPREGADOR
        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            base.sendInfoEmpresa(Terminal, out EmpregadorDados);
            log.AddLog("ENVIANDO CADASTRO DO EMPREGADOR");
            Connect(Terminal);

            REP.eTipoDocumento IdentificadorTipo;
            string Identificador;

            if (EmpregadorDados.PessoaTipo == Types.PessoaTipo.Cnpj)
            {
                IdentificadorTipo = REP.eTipoDocumento.CNPJ;
                Identificador = EmpregadorDados.Pessoa;
            }
            else
            {
                IdentificadorTipo = REP.eTipoDocumento.CPF;
                Identificador = EmpregadorDados.Pessoa;
            }

            if (Rep.CadastrarEmpregador(TerminalDados.IP, TerminalDados.Porta, Convert.ToInt32(TerminalDados.SupervisorSenha), IdentificadorTipo, Identificador, EmpregadorDados.Cei, EmpregadorDados.Nome, EmpregadorDados.Endereco))
            {
                log.AddLog("CADASTRO DO EMPREGADOR ENVIADO COM SUCESSO");
                return true;
            }
            else
            {
                log.AddLog("ERRO NO ENVIO DO CADASTRO DO EMPREGADOR");
                return true;
            }
        }
        #endregion

        #region Overrides ENVIAR FUNCIONARIO
        public override bool sendFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            base.sendFuncionario(Funcionario);
            //Cracha = Cracha.PadLeft(CartaoDigitos, '0');

            log.AddLog("ENVIANDO FUNCIONÁRIO PIS: " + Funcionario.Pis + " CRACHÁ: " + Funcionario.Crachas[0]);

            if (Rep.CadastrarEmpregado(TerminalDados.IP, TerminalDados.Porta, Convert.ToInt32(TerminalDados.SupervisorSenha), Funcionario.Pis, Funcionario.Nome, Funcionario.Crachas[0].ToString(), false))
            {
                log.AddLog("FUNCIONÁRIO ENVIADO COM SUCESSO");
                return true;
            }
            else
            {
                LogErro();
                return false;
            }
        }
        #endregion

        #region Overrides DELETAR FUNCIONARIO
        public override bool deleteFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);

            log.AddLog("EXCLUINDO FUNCIONÁRIO PIS: " + Funcionario.Pis);

            if (Rep.ExcluirEmpregado(TerminalDados.IP, TerminalDados.Porta, Convert.ToInt32(TerminalDados.SupervisorSenha), Funcionario.Pis))
            {
                log.AddLog("FUNCIONÁRIO EXCLUIDO COM SUCESSO");
                return true;
            }
            else
            {
                LogErro();
                return false;
            }
        }
        #endregion

        #region IMPORTA MARCACOES
        public override bool LerMarcacoes(Marcacoes marcacoes, AssepontoRep.Bridge.TipoImportacaoMarcacoes tipoimportacao)
        {
            base.LerMarcacoes(marcacoes, tipoimportacao);
            Connect(TerminalDados.Indice);
            //log.AddLog("INICIANDO IMPORTAÇÂO DE MARCAÇÔES");

            DBApp bd = new DBApp();
            int ultimonsr = bd.getLastNsr(TerminalDados.Indice);
            DateTime inicial = Convert.ToDateTime("01/01/2010");
            DateTime final = DateTime.Now;
            DataTable table = new DataTable();
            string lista = "";

            if (Rep.LerAFD_ViaLista(TerminalDados.IP, TerminalDados.Porta, Convert.ToInt32(TerminalDados.SupervisorSenha), inicial.ToString("yyyyMMdd"), final.ToString("yyyyMMdd"), ref lista, ultimonsr, "|", ";"))
            {
                string[] registros = lista.Split(new string[] { ";" }, StringSplitOptions.None);
                string[] campos;
                foreach (string registro in registros)
                {
                    campos = registro.Split(new string[] { "|" }, StringSplitOptions.None);
                    string nsr = campos[0];
                    string tipo = campos[1];
                    if (tipo.Trim() == "3")
                    {
                        string data = campos[2];
                        string hora = campos[3];
                        string pis = campos[4];
                        DateTime datetime = Convert.ToDateTime(data.Substring(0, 2) + "/" + data.Substring(2, 2) + "/" + data.Substring(4, 4) + " " + hora.Substring(0, 2) + ":" + hora.Substring(2, 2));
                        marcacoes.Add(pis, datetime, Convert.ToInt32(nsr));
                    }
                }
                return true;
            }
            else
            {
                LogErro();
                return false;
            }
        }
        #endregion
    }
}
