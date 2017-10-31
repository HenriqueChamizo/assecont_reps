using System.Windows.Forms;
using System;
using AssepontoRep;
using System.Collections.Generic;
using System.Net;

namespace Controlid
{
    public class Bridge : AssepontoRep.Bridge
    {
        RepCid _rep;
        private TextBox consoleLog;

        private const string conectaRep = "CONECTADO COM O REP...";
        private const string falhaAutenticacao = "ERRO: FALHA NA AUTENTICACAO";
        private const string falhaConexao = "ERRO: CONEXAO COM O REP";
        private const string falhaRepEmUso = "ERRO: REP EM USO";
        private const string fileNameUsuarios = "\\usuarios.dat";
        private const string fileNameDigitais = "";

        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
            consoleLog = edLog;
        }

        #region Overrides Abstract
        public override bool getPin()
        {
            return false;
        }
        public override int getPortaPadrao() { return 1818; }
        public override string getRepFabricante() { return "Control ID"; }
        public override string getArquivoUsb() { return ""; }
        public override bool getGerarUsb() { return true; }
        public override bool getGerarBackup() { return false; }
        public override bool getGerarBiometria() { return false; }
        public override bool getEnviarHorarioVerao() { return true; }
        public override bool getEnviarHorarioVeraoUsb() { return false; }
        public override bool getFuncionariosAlteradosUsb() { return false; }
        public override bool getGerarUsbEmpregador() { return false; }
        public override bool getCadastroTerminalSupervisor() { return false; }
        public override bool getNumeroSerieREP() { return false; }
        public override bool getContemChaveAcessoREP() { return true; }
        public override bool getBoxFuncoes() { return false; }
        public override bool getCadastroTerminalResponsavel() { return false; }
        public override bool getAutenticacao() { return false; }
        public override List<Types.Permissao> getPermissoes() { return null; }
        public override bool getColumnId(){ return false; }
        public override bool getDisconnectOnExit() { return false; }
        #endregion

        #region CONEXAO
        private void Connect()
        {
            if (_rep != null)
                _rep.Desconectar();

            _rep = new RepCid();
            //string con = "";
            //if (TerminalDados.IP.Replace(" ", "").Contains("dns:"))
            //{
            //    IPAddress[] ip = Dns.GetHostAddresses(TerminalDados.IP.Replace(" ", "").Replace("dns:", ""));
            //    con = ip[0].ToString();
            //}
            //else
            //    con = TerminalDados.IP;

            LogErro(_rep.Conectar(TerminalDados.IP, TerminalDados.Porta, 0));//, Convert.ToUInt32(string.IsNullOrEmpty(TerminalDados.SupervisorCodigo) ? "0" : TerminalDados.SupervisorCodigo)));
        }
        #endregion

        #region logErro Conexao
        private void LogErro(RepCid.ErrosRep Erro)
        {
            switch (Erro)
            {
                case RepCid.ErrosRep.OK:
                    //log.AddLog(conectaRep);
                    break;

                case RepCid.ErrosRep.ErroAutenticacao:
                    log.AddLog(falhaAutenticacao);
                    break;

                case RepCid.ErrosRep.ErroConexao:
                    log.AddLog(falhaConexao);
                    break;

                case RepCid.ErrosRep.ErroNaoOcioso:
                    log.AddLog(falhaRepEmUso);
                    break;

                default:
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    break;
            }
        }
        #endregion

        #region Overrides ENVIAR DATA E HORA
        public override bool sendDataHora(int Terminal)
        {
            bool Result = base.sendDataHora(Terminal);

            Connect();
            // Data e Horario
            try
            {
                if (_rep.GravarDataHora(DateTime.Now))
                {
                    log.AddLog(Consts.DATA_HORA_ATUALIZADA_SUCESSO);
                    Result = true;
                }
                else
                {
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                }
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
                Result = false;
            }
            finally
            {
                _rep.Desconectar();
            }
            return Result;
        }
        #endregion

        #region Overrides ENVIAR EMPREGADOR
        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            bool Result = base.sendInfoEmpresa(Terminal, out EmpregadorDados);

            try
            {
                EmpregadorDados.Cei = "00000";

                int nTipo = (int)EmpregadorDados.PessoaTipo;
                Connect();

                if (_rep.GravarEmpregador(EmpregadorDados.Pessoa, nTipo, EmpregadorDados.Cei, EmpregadorDados.Nome, EmpregadorDados.Endereco, out Result))
                    log.AddLog(Consts.EMPREGADOR_ENVIADO_SUCESSO);
                else
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
                Result = false;
            }
            finally
            {
                _rep.Desconectar();
            }

            return Result;
        }
        #endregion

        #region Overrides ENVIAR FUNCIONARIO
        public override bool sendFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            bool Result = base.sendFuncionario(Funcionario);
            byte privilegios = 0;

            try
            {
                Connect();

                if (!_rep.GravarUsuario(Convert.ToInt64(Funcionario.Pis), Funcionario.Nome, metodTeclado(Funcionario.Teclado), "",
                     Funcionario.Barras + '\0', metodProximidade(Funcionario.Proximidade), privilegios, out Result))
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
                Result = false;
            }
            finally
            {
                _rep.Desconectar();
            }

            return Result;
        }

        public int metodTeclado(string Cracha)
        {
            return Cracha == string.Empty && Cracha.Length <= 8 ? 0 : Convert.ToInt32(Cracha);
        }

        public int metodProximidade(string Cracha) //wiegand: w no cracha
        {
            if (Cracha == string.Empty)
                return 0;
            else
                return Utilitarios.gerarWiegand(Cracha);
        }
        #endregion

        #region Overrides DELETAR FUNCIONARIO
        public override bool deleteFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            bool Result = base.deleteFuncionario(Funcionario);

            try
            {
                Connect();

                if (!(_rep.RemoverUsuario(Convert.ToInt64(Funcionario.Pis), out Result)))
                {
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                }
                else
                {
                    log.AddLog(Consts.OPERACAO_FINALIZADA);
                    log.AddLineBreak();
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
                Result = false;
            }
            finally
            {
                _rep.Desconectar();
            }

            return Result;
        }
        #endregion

        #region HORARIO DE VERÃO
        public override bool EnviarHorarioVerao(int Terminal, string Inicio, string Fim)
        {
            bool Result = base.EnviarHorarioVerao(Terminal, "", "");

            try
            {
                Connect();

                DateTime inicio = Convert.ToDateTime(Inicio);
                DateTime final = Convert.ToDateTime(Fim);

                if (!_rep.GravarConfigHVerao(inicio.Year, inicio.Month, inicio.Day,
                    final.Year, final.Month, final.Day, out Result))
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                else
                    log.AddLog(Consts.HORARIO_VERAO_ENVIADO_SUCESSO);
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
                Result = false;
            }
            finally
            {
                _rep.Desconectar();
            }

            return Result;
        }
        #endregion

        #region IMPORTA MARCACOES
        public override bool LerMarcacoes(Marcacoes marcacoes, AssepontoRep.Bridge.TipoImportacaoMarcacoes tipoimportacao)
        {
            bool Result = false;
            int NRegistrosLidos = 0;
           
            Connect();

            try
            {
                DBApp db = new DBApp();
                int ProximoNsr = db.getLastNsr(TerminalDados.Indice) + 1;
               

                if (!_rep.BuscarAFD(ProximoNsr))
                {
                    log.AddLog(Consts.MARCACOES_NAO_HA_NOVAS);
                }
                else
                {
                    string cLinha;
                    while (_rep.LerAFD(out cLinha))
                    {
                        AssepontoRep.Marcacoes.Marcacao mrc = new AssepontoRep.Marcacoes.Marcacao();

                        if (marcacoes.InterpretarRegistroAfd(cLinha, out mrc))
                        {
                            if (mrc.Tipo == Marcacoes.TiposRegistroAfd.Marcacao)
                                marcacoes.Add(mrc);
                        }
                        
                        //if (Convert.ToInt32(cLinha.Substring(9, 1)) == (int)(Marcacoes.TiposRegistroAfd.Marcacao))
                        //{
                        //    string PIS = cLinha.Substring(22, 12).Replace("\n", "").Replace("\t", "").Replace("\r", "");
                        //    string DataHora = cLinha.Substring(10, 12);
                        //    string NSR = cLinha.Substring(0, 9);

                        //    marcacoes.Add(PIS, extrairDateTime(DataHora), Convert.ToInt32(NSR));
                        //}

                        NRegistrosLidos++;
                    }

                    if (NRegistrosLidos == 0)
                    {
                        log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    }

                    if (marcacoes.Count == 0)
                    {
                        log.AddLog(Consts.MARCACOES_NAO_HA_NOVAS);
                    }

                    Result = true;
                }
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
                Result = false;
            }
            finally
            {
                _rep.Desconectar();
            }

            return Result;
        }

        //public DateTime extrairDateTime(string data)
        //{
        //    data = data.Substring(0, 2) + "/" + data.Substring(2, 2) + "/" + data.Substring(4, 4) + " " + data.Substring(8, 2) + ":" + data.Substring(10, 2);

        //    return Convert.ToDateTime(data);
        //}
        #endregion

        #region USB
        private RepCidUsb repUsb;

        public override void UsbFileInicializacao(string Folder)
        {
            try
            {
                Wr.Classes.Files.WriteFile(Folder + getRepFabricante() + fileNameUsuarios, (new List<string>()));

                repUsb = new RepCidUsb();
                repUsb.IniciaGravacao();
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
            }
        }

        public override void UsbFileFuncionario(Types.Funcionario Funcionario, Types.Opcao Opcao, ref string Line)
        {
            try
            {
                if (!repUsb.AdicionarUsuario(Convert.ToInt64(Funcionario.Pis), Funcionario.Nome, Convert.ToUInt32(metodTeclado(Funcionario.Teclado)), "",
                        Funcionario.Barras + '\0', Convert.ToUInt32(metodProximidade(Funcionario.Proximidade)), 0))
                {
                    log.AddLog("Erro em adicionar " + Funcionario.Nome + " :" + repUsb.GetLastError());
                }
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
            }
        }

        public override void UsbFileFinalizacao(string Folder)
        {
            try
            {
                Folder += getRepFabricante();

                if (!repUsb.FinalizarGravacao(Folder + fileNameUsuarios, Folder + fileNameDigitais))
                {
                    log.AddLog("Erro em finalizar: " + repUsb.GetLastError());
                }
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
            }
        }

        #endregion
    }
}
