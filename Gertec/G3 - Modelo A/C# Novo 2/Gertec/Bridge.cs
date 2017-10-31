using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AssepontoRep;
using System.IO;
using RepProtocolTestSuite;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Gertec
{
    class Bridge : AssepontoRep.Bridge
    {
        RepProtocol repProtocol;
        Form Principal;


        private const string FILE_CONFIGURATION = "wifi.cfg";
        private const string FILE_EMPLOYER = "empregador.bd";
        private const string FILE_STAFF = "cadastro.bd";
        private string FILE_PATH = Folders.folderArquivoUsb("Gertec");

        #region Consts
        string MSGOK = "OK";
        #endregion

        #region Constructor
        public Bridge(TextBox edLog, Form Principal)
            : base(Consts.OFFLINE, edLog)
        {
            this.Principal = Principal;

            repProtocol = new RepProtocol(edLog);
        }
        #endregion

        #region Abstract Override
        public override string getRepFabricante()
        {
            return "Gertec";
        }

        public override int getPortaPadrao()
        {
            return 5290;
        }

        public override string getArquivoUsb()
        {
            return "cadastro.bd";
        }

        public override bool getGerarBackup()
        {
            return true;
        }

        public override bool getGerarUsb()
        {
            return true;
        }

        public override bool getNumeroSerieREP()
        {
            return false;
        }

        public override bool getGerarUsbEmpregador()
        {
            return true;
        }

        public override bool getGerarBiometria()
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

        public override bool getEnviarHorarioVeraoUsb()
        {
            return false;
        }

        public override bool getContemChaveAcessoREP()
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

        public override bool getBoxFuncoes()
        {
            return false;
        }

        public override bool getAutenticacao()
        {
            return true;
        }
        #endregion

        #region Override
        public override bool sendDataHora(int Terminal)
        {
            base.sendDataHora(Terminal);

            Connect(Terminal);
            string msg = repProtocol.SetTime(DateTime.Now);
            LogMensagem(msg);

            return (msg == MSGOK);
        }

        public override bool deleteFuncionario(Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);

            Connect(TerminalDados.Indice);
            string msg = repProtocol.RemoveEmployee(Funcionario.Pis);
            LogMensagem(msg);

            return (msg == MSGOK);
        }

        public override bool sendFuncionario(Types.Funcionario Funcionario)
        {
            base.sendFuncionario(Funcionario);

            RepProtocol.Empregado empregado = new RepProtocol.Empregado();
            empregado.CNTLS = Convert.ToInt64(Funcionario.Proximidade).ToString("X");
            empregado.ID = Funcionario.Crachas[0].ToString();
            empregado.KBD = Funcionario.Crachas[0].ToString();
            empregado.Nome = Funcionario.Nome;
            empregado.PIS = Funcionario.Pis;

            AssepontoRep.DBApp bd = new DBApp();

            string msg = "";

            if (bd.FuncionarioEstaNoTerminal(TerminalDados.Indice, Funcionario.Ind))
            {
                msg = repProtocol.EditEmployee(empregado);
            }
            else
            {
                msg = repProtocol.SetEmployee(empregado);
            }

            LogMensagem(msg);

            return (msg == MSGOK);
        }

        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            base.sendInfoEmpresa(Terminal, out EmpregadorDados);

            Connect(Terminal);

            RepProtocol.Empregador empregador = new RepProtocol.Empregador();
            empregador.CEI = EmpregadorDados.Cei;
            empregador.CNPJ = EmpregadorDados.Pessoa;
            empregador.Endereco = EmpregadorDados.Endereco;
            empregador.Nome = EmpregadorDados.Nome;
            empregador.Tipo = ((int)EmpregadorDados.PessoaTipo).ToString();

            if (
                System.Text.RegularExpressions.Regex.Replace(empregador.Nome, @"\s+", "").Length == 0 ||
                System.Text.RegularExpressions.Regex.Replace(empregador.Endereco, @"\s+", "").Length == 0 ||
                System.Text.RegularExpressions.Regex.Replace(empregador.CNPJ, @"\s+", "").Length == 0
                )
            {
                throw new Exception("Campos em branco");
            }
            string msg = repProtocol.EditEmployer(empregador);
            LogMensagem(msg);

            return (msg == MSGOK);
        }

        public override bool LerMarcacoes(Marcacoes marcacoes, AssepontoRep.Bridge.TipoImportacaoMarcacoes tipoimportacao)
        {
            DBApp db = new DBApp();

            Connect(TerminalDados.Indice);

            string from = "";
            string to = "";

            RepProtocol.FiltroRegistro f;

            if (tipoimportacao == TipoImportacaoMarcacoes.OnlyNew)
            {
                f = RepProtocol.FiltroRegistro.NsrRange;
                RepProtocol.MrpStatus st = repProtocol.GetMrpStatus();

                int ProximoNsr = db.getLastNsr(TerminalDados.Indice) + 1;

                from = ProximoNsr.ToString().PadLeft(9, '0');
                to = st.LastNSR.ToString();
            }
            else
            {
                f = RepProtocol.FiltroRegistro.All;
            }

            List<string> r = repProtocol.GetRegs(f, from, to);

            foreach (string linha in r)
            {
                Marcacoes.Marcacao marcacao = new Marcacoes.Marcacao();

                marcacoes.InterpretarRegistroAfd(linha, out marcacao);

                if (marcacao.Tipo == Marcacoes.TiposRegistroAfd.Marcacao)
                    marcacoes.Add(marcacao);
            }

            //Wr.Classes.Files.WriteFile(@"c:\arquivo.txt", r);
            return (marcacoes.Count > 0);
        }

        public override void UsbFileFuncionario(Types.Funcionario Funcionario, Types.Opcao Opcao, List<string> Line)
        {
            if (Funcionario.Proximidade != String.Empty)
            {
                Line.Add(String.Format("{0},{1},{2},{3},{4};", Funcionario.Nome, Funcionario.Pis, Funcionario.Crachas[0], Funcionario.Crachas[0], Convert.ToInt64(Funcionario.Proximidade).ToString("X")));
            }
            else
            {
                foreach (long cracha in Funcionario.Crachas)
                {
                    Line.Add(String.Format("{0},{1},{2},{3},{4};", Funcionario.Nome, Funcionario.Pis, cracha, cracha, String.Empty));
                };
            }
        }

        //public override bool GerarArquivoEmpregador(int Terminal, Types.Opcao Opcao, out Types.Empregador EmpregadorDados)
        //{
        //    bool result = base.GerarArquivoEmpregador(Terminal, Opcao, out EmpregadorDados);

        //    return result;
        //}

        public override bool Connect(int Terminal)
        {
            Authenticate(Terminal);
            return true;
        }
        #endregion

        #region Private
        void Authenticate(int Terminal)
        {
            AssepontoRep.DBApp bd = new AssepontoRep.DBApp();

            string Cpf = bd.getFieldValueString(String.Format("SELECT TRM_AUTENTICACAO_CPF FROM Terminais WHERE TRM_IND = {0}", Terminal));
            string Senha = bd.getFieldValueString(String.Format("SELECT TRM_AUTENTICACAO_SENHA FROM Terminais WHERE TRM_IND = {0}", Terminal));
            string Host = bd.getFieldValueString(String.Format("SELECT TRM_IP FROM Terminais WHERE TRM_IND = {0}", Terminal));

            if (Cpf != String.Empty && Senha != String.Empty && Host != String.Empty)
            {
                repProtocol.SetHost(Host);
                repProtocol.SetAuth(System.Text.RegularExpressions.Regex.Replace(Cpf, "[^0-9]", ""), Senha);
            }
        }

        private void LogMensagem(string Msg, bool DisplayIfOk = true)
        {
            if (Msg == MSGOK)
            {
                if (DisplayIfOk) log.LogOk();
            }
            else
            {
                log.AddLog(Msg, true);
            }
        }
        #endregion

        //Comandos USB
        #region USB Funcionario
        public override bool createUsbFile(int Terminal, Types.Opcao Opcao, List<Types.Funcionario> listFuncionarios)
        {
            char option = Opcao == Types.Opcao.Inclusao ? 'I' : Opcao == Types.Opcao.Alteracao ? 'A' : 'E';

            try
            {
                log.AddLog(Consts.USB_GERANDO_ARQUIVO);
                deleteArquivo(FILE_PATH, FILE_STAFF);
                List<string> Novo = new List<string>();
                string linha;
                foreach (Types.Funcionario func in listFuncionarios)
                {
                    linha = "";
                    if (!Wr.Classes.Validadores.ValidaPis(func.Pis))
                    {
                        log.AddLog(func.Nome);
                        log.AddLog("    PIS INVALIDO");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(func.Barras) && string.IsNullOrEmpty(func.Proximidade) && string.IsNullOrEmpty(func.Teclado))
                        {
                            log.AddLog(func.Nome);
                            log.AddLog("    FUNCIONÁRIO SEM CRACHÁ");
                        }
                        else
                        {
                            linha = func.Nome.Replace(",", "") + "," + func.Pis.Replace(",", "") + "," + func.Ind.ToString().Replace(",", "") + "," +
                                (func.Matricula == null ? "" : func.Matricula.Replace(",", "")) + "," + func.Proximidade.Replace(",", "") + ";";
                            Novo.Add(linha);
                        }
                    }
                }

                if (Novo.Count == 0)
                {
                    log.AddLog(Consts.USB_SEM_FUNCIONARIOS);
                    return false;
                }

                Wr.Classes.Files.WriteFile(FILE_PATH + FILE_STAFF, Novo);
                log.AddLog(string.Format(Consts.USB_ARQUIVO_GERADO_SUCESSO, FILE_PATH));
                return true;
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }
        }
        #endregion

        #region USB Empregador
        public override bool GerarArquivoEmpregador(int Terminal, Types.Opcao Opcao, out Types.Empregador EmpregadorDados)
        {
            if (!base.sendInfoEmpresa(Terminal, out EmpregadorDados)) return false;

            try
            {
                log.AddLog(Consts.USB_GERANDO_ARQUIVO);
                List<string> Novo = new List<string>();
                int PessoaTipo = 0;
                if (EmpregadorDados.PessoaTipo == Types.PessoaTipo.Cnpj)
                    PessoaTipo = 1;
                else if (EmpregadorDados.PessoaTipo == Types.PessoaTipo.Cpf)
                    PessoaTipo = 2;
                else
                    PessoaTipo = 0;

                deleteArquivo(FILE_PATH, FILE_EMPLOYER);
                Novo.Add(string.Format("{0},{1},{2},{3},{4};", EmpregadorDados.Pessoa.Replace(",", ""), EmpregadorDados.Cei.Replace(",", ""),
                                EmpregadorDados.Nome.Replace(",", ""), EmpregadorDados.Endereco.Replace(",", ""), PessoaTipo.ToString().Replace(",", "")));

                Wr.Classes.Files.WriteFile(FILE_PATH + FILE_EMPLOYER, Novo);
                log.AddLog(Consts.USB_EMPEGADOR_SUCESSO);
                return true;
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }
        }
        #endregion

        #region Util
        private void deleteArquivo(string filePath, string Name)
        {
            List<string> Arquivos = new List<string>();

            Wr.Classes.Files.DirSearch(filePath, Name, Arquivos);

            foreach (string arquivo in Arquivos)
            {
                File.Delete(arquivo);
            }
        }
        #endregion
    }
}
