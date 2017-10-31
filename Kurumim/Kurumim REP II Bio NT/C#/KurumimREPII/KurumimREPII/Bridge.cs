using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using AssepontoRep;
using Wr.Classes;
using System.Text.RegularExpressions;

namespace KurumimREPII
{
    public class Bridge : AssepontoRep.Bridge
    {
        private TextBox consoleLog;
        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
            consoleLog = edLog;
        }

        //Testado no Firmware: v1.0.00

        private const string FILE_CONFIGURATION = "ConfiguracaoREP.ini";
        private const string FILE_EMPLOYER = "Empresa.prv";
        private const string FILE_STAFF = "Funcionarios.prv";
        private string FILE_PATH = Folders.folderArquivoUsb("KurumimREPII");

        /*16001: Mensagem de Alerta, não realizou a operação.
          16002: Mensagem de Erro: falha ao inicializar o comunicador.
          16003: Mensagem de informação.
          16004: NR não informado.
          16005: Serial Invalido
          16006: Parametros Invalido*/

        #region Overrides Abstract
        public override int     getPortaPadrao()                { return 1818; }
        public override string  getRepFabricante()              { return "Kurumim REP II - Firmware v1.0.00"; }
        public override string  getArquivoUsb()                 { return "KurumimREPII"; }
        public override bool    getGerarUsb()                   { return true;  }
        public override bool    getGerarBackup()                { return false; }
        public override bool    getGerarBiometria()             { return false; }
        public override bool    getEnviarHorarioVerao()         { return true;  }
        public override bool    getEnviarHorarioVeraoUsb()      { return true;  }
        public override bool    getFuncionariosAlteradosUsb()   { return false; }
        public override bool    getGerarUsbEmpregador()         { return true;  }
        public override bool    getCadastroTerminalSupervisor() { return true; }
        public override bool    getNumeroSerieREP()             { return true;  }
        public override bool    getContemChaveAcessoREP()       { return true;  }
        public override bool    getCadastroTerminalResponsavel() { return false; }
        public override bool    getBoxFuncoes() { return false; }
        public override bool    getAutenticacao()
        {
            return false;
        }
        #endregion

        //Gerar Parametros
        #region Args
        private string Args()
        {
            if (string.IsNullOrEmpty(TerminalDados.IP))
                throw new Exception("IP não informado.");

            return " /net /toi /nr" + TerminalDados.Serial + " /s" + TerminalDados.SupervisorSenha + " /ip" + TerminalDados.IP;
        }

        private string ArgsUsb()
        {
            return " /nr" + TerminalDados.Serial + " /s" + TerminalDados.SupervisorCodigo;
        }
        #endregion

        //Enviar Parametros para o executavel da Proveu (CKRep.exe)
        #region Executar
        public bool processStart(string comando, string parametros)
        {
            if (string.IsNullOrEmpty(TerminalDados.Serial))
                throw new Exception("Número de série não informado.");

            ProcessStartInfo info = new ProcessStartInfo("CKRep.exe");
            string arguments = (comando + parametros);//.Replace("ç", "c");
            //arguments = arguments.Replace("á", "a").Replace("à", "a").Replace("â", "a").Replace("ã", "a");
            //arguments = arguments.Replace("é", "e").Replace("è", "e").Replace("ê", "e");
            //arguments = arguments.Replace("í", "i").Replace("ì", "i").Replace("î", "i");
            //arguments = arguments.Replace("ó", "o").Replace("ò", "o").Replace("ô", "o").Replace("õ", "o");
            //arguments = arguments.Replace("ú", "u").Replace("ù", "u").Replace("û", "u");
            info.Arguments = arguments;
            info.UseShellExecute = false;

            Process myProcess = Process.Start(info);
            myProcess.WaitForExit();

            return myProcess.ExitCode == 0 || myProcess.ExitCode == 16003;
        }
        #endregion

        //Comandos Via Rede
        #region Enviar Data/Hora
        public override bool sendDataHora(int Terminal)
        {
            try
            {
                base.sendDataHora(Terminal);
                bool Result = processStart("/er", Args());
                getLog();
                return Result;
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }
        }
        #endregion

        #region Enviar Empregador
        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            base.sendInfoEmpresa(Terminal, out EmpregadorDados);

            try
            {
                if (!arquivoEmpresa((int)EmpregadorDados.PessoaTipo, EmpregadorDados.Pessoa, EmpregadorDados.Cei, EmpregadorDados.Nome, EmpregadorDados.Endereco)) return false;
                bool Result = processStart("/a " + FILE_PATH, Args());
                if (Result) log.AddLog(Consts.EMPREGADOR_ENVIADO_SUCESSO);
                getLog();

                return Result;
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }
        }
        #endregion

        #region Enviar Funcionarios
        public override bool sendFuncionarios(int Terminal, List<Types.Funcionario> listFuncionarios)
        {
            try
            {
                List<Types.Funcionario> listTodos = (new DBApp()).getListFuncionarios(Terminal);
                prepareTerminal(Terminal);
                if (!arquivoFuncionario(listTodos)) return false;
                bool Result = processStart("/a " + FILE_PATH, Args());
                if (!Result)
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);

                getLog();

                return Result;
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }
        }
        #endregion

        #region Enviar Horário de verão
        public override bool EnviarHorarioVerao(int Terminal, string Inicio, string Fim)
        {
            try
            {
                prepareTerminal(Terminal);
                arquivoHorarioVerao(Inicio, Fim);

                bool Result = processStart("/a " + FILE_PATH, Args());
                if (!Result) log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                getLog();

                return Result;
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }
        }
        #endregion

        #region Receber Marcações
        public override bool LerMarcacoes(Marcacoes marcacoes, AssepontoRep.Bridge.TipoImportacaoMarcacoes tipoimportacao)
        {
            try
            {
                int NRegistrosLidos = 0;

                if (!File.Exists(FILE_PATH + FILE_CONFIGURATION))
                    Files.WriteFile(FILE_PATH + FILE_CONFIGURATION, (new List<string>()));

                bool Result = processStart("/co " + FILE_PATH, Args());

                if (Result)
                {
                    //int ProximoNsr = (new DBApp()).getLastNsr(TerminalDados.Indice) + 1;

                    List<string> Arquivos = new List<string>();
                    List<string> Novo = new List<string>();

                    Files.DirSearch(FILE_PATH, "*.txt", Arquivos);

                    foreach (string arquivo in Arquivos)
                    {
                        List<string> conteudo = new List<string>();
                        Files.ReadFile(arquivo, conteudo);

                        foreach (string cLinha in conteudo)
                        {
                            AssepontoRep.Marcacoes.Marcacao mrc = new AssepontoRep.Marcacoes.Marcacao();

                            if (marcacoes.InterpretarRegistroAfd(cLinha, out mrc))
                            {
                                if (mrc.Tipo == Marcacoes.TiposRegistroAfd.Marcacao)
                                    marcacoes.Add(mrc);
                            }

                            NRegistrosLidos++;
                        }
                        File.Delete(arquivo);
                    }

                    //if (marcacoes.Count == 0)
                    //{
                    //    log.AddLog(Consts.MARCACOES_NAO_HA_NOVAS);
                    //}
                }
                else
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);

                getLog();

                return Result;
            }
            catch (Exception ex)
            {
                log.AddLog(ex.Message);
            }

            return false;
        }
        #endregion

        #region Excluir Funcionarios
        public override bool deleteFuncionarios(int Terminal, List<Types.Funcionario> listFuncionarios)
        {
            try
            {
                List<Types.Funcionario> listTodos = (new DBApp()).getListFuncionarios(Terminal);
                prepareTerminal(Terminal);

                deleteArquivo(FILE_PATH, FILE_STAFF);
                List<string> Novo = new List<string>();

                foreach (Types.Funcionario func in listTodos)
                {
                    if (!findFucionario(listFuncionarios, func))
                    {
                        if (!Wr.Classes.Validadores.ValidaPis(func.Pis))
                            log.AddLog(func.Nome + "\nPIS INVALIDO");
                        else
                        {
                            Novo.Add(getFormatoArquivoFuncionario(func));
                            (new AssepontoRep.DBApp()).Atualizar_TerminaisFuncionarios(TerminalDados.Indice, func.Ind, true);
                        }
                    }
                }

                if (Novo.Count == 0)
                {
                    log.AddLog(Consts.USB_SEM_FUNCIONARIOS);
                    return false;
                }

                Files.WriteFile(FILE_PATH + FILE_STAFF, Novo);

                bool Result = processStart("/a " + FILE_PATH, Args());

                if (!Result) log.AddLog(Consts.ERRO_ENVIO_COMANDO);

                getLog();

                return Result;
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }
        }
        #endregion

        //Comandos USB
        #region USB Funcionario
        public override bool createUsbFile(int Terminal, Types.Opcao Opcao, List<AssepontoRep.Types.Funcionario> listFuncionarios)
        {
            try
            {
                List<Types.Funcionario> listTodos = (new DBApp()).getListFuncionarios(Terminal);
                prepareTerminal(Terminal);
                if (!arquivoFuncionario(listTodos)) return false;
                bool Result = processStart("/a " + FILE_PATH, ArgsUsb());

                if (!Result)
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                else
                    log.AddLog(string.Format(Consts.USB_ARQUIVO_GERADO_SUCESSO, FILE_PATH));

                getLog();
                return Result;
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }
        }
        #endregion

        #region USB Horário de verão
        public override void EnviarHorarioVeraoUsb(int Terminal, string Inicio, string Fim)
        {
            try
            {
                prepareTerminal(Terminal);
                arquivoHorarioVerao(Inicio, Fim);
                processStart("/a " + FILE_PATH, ArgsUsb());
            }
            catch (Exception e) { log.AddLog(e.Message); }
        }
        #endregion

        #region USB Empregador
        public override bool GerarArquivoEmpregador(int Terminal, Types.Opcao Opcao, out Types.Empregador EmpregadorDados)
        {
            if (!base.sendInfoEmpresa(Terminal, out EmpregadorDados)) return false;

            try
            {
                if (!arquivoEmpresa((int)EmpregadorDados.PessoaTipo, EmpregadorDados.Pessoa, EmpregadorDados.Cei, EmpregadorDados.Nome, EmpregadorDados.Endereco)) return false;
                bool Result = processStart("/a " + FILE_PATH, ArgsUsb());
                if (Result) log.AddLog(Consts.USB_EMPEGADOR_SUCESSO);
                getLog();

                return Result;
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }
        }
        #endregion

        //Funções para gerar arquivos modelos (Configuração, funcionarios, empregador e horário de verão)
        #region Arquivos
        private void arquivoHorarioVerao(string Inicio, string Fim)
        {
            List<string> Novo = new List<string>();

            Novo.Add("[HORARIOVERAO]");
            Novo.Add("InicioVerao=" + Convert.ToDateTime(Inicio).ToString("dd/MM").PadLeft(5, '0'));
            Novo.Add("FimVerao=" + Convert.ToDateTime(Fim).ToString("dd/MM").PadLeft(5, '0'));

            deleteArquivo(FILE_PATH, FILE_CONFIGURATION);
            Files.WriteFile(FILE_PATH + FILE_CONFIGURATION, Novo);
        }


        private bool arquivoFuncionario(List<Types.Funcionario> Funcionarios)
        {
            deleteArquivo(FILE_PATH, FILE_STAFF);
            List<string> Novo = new List<string>();

            foreach (Types.Funcionario func in Funcionarios)
            {
                if (!Wr.Classes.Validadores.ValidaPis(func.Pis))
                    log.AddLog(func.Nome + "\nPIS INVALIDO");
                else
                {
                    if (string.IsNullOrEmpty(func.Barras) && string.IsNullOrEmpty(func.Proximidade) && string.IsNullOrEmpty(func.Teclado))
                        log.AddLog(func.Nome + "\nFUNCIONÁRIO SEM CRACHÁ");
                    else
                    {
                        Novo.Add(FormatCaractere(getFormatoArquivoFuncionario(func)));
                        (new AssepontoRep.DBApp()).Atualizar_TerminaisFuncionarios(TerminalDados.Indice, func.Ind);
                    }
                }
            }

            if (Novo.Count == 0)
            {
                log.AddLog(Consts.USB_SEM_FUNCIONARIOS);
                return false;
            }

            Files.WriteFile(FILE_PATH + FILE_STAFF, Novo);
            return true;
        }

        private string getFormatoArquivoFuncionario(Types.Funcionario Funcionario)
        {
            return (string.IsNullOrEmpty(Funcionario.Teclado) ? string.IsNullOrEmpty(Funcionario.Barras) ? Funcionario.Proximidade : Funcionario.Barras : Funcionario.Teclado).PadLeft(16, '0') + ";" +
                     Funcionario.Pis.PadLeft(12, '0') + ";" +
                     Funcionario.Nome.PadRight(52, ' ') + ";" +
                     (Funcionario.Biometria ? "SIM" : "NAO"); //Exige biometria
        }


        private bool arquivoEmpresa(int indetificador, string cnpjCpf, string cei, string razaoSocial, string endereco)
        {
            try
            {
                List<string> Arquivos = new List<string>();
                List<string> Novo = new List<string>();

                Files.DirSearch(FILE_PATH, FILE_EMPLOYER, Arquivos);

                deleteArquivo(FILE_PATH, FILE_EMPLOYER);

                string linha = indetificador + ";";
                linha += cnpjCpf.PadLeft(14, '0') + ";";
                linha += cei.PadLeft(12, '0') + ";";
                linha += razaoSocial.PadRight(150, ' ') + ";";
                linha += endereco.PadRight(100, ' ');

                Novo.Add(FormatCaractere(linha));

                Files.WriteFile(FILE_PATH + FILE_EMPLOYER, Novo);
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }

            return true;
        }
        #endregion

        #region Util
        private void deleteArquivo(string filePath, string Name)
        {
            List<string> Arquivos = new List<string>();

            Files.DirSearch(filePath, Name, Arquivos);

            foreach (string arquivo in Arquivos)
            {
                File.Delete(arquivo);
            }
        }

        private bool findFucionario(List<Types.Funcionario> lista, Types.Funcionario funcionario)
        {
            foreach (Types.Funcionario func in lista)
                return func.Nome.Equals(funcionario.Nome);

            return false;
        }
        #endregion

        #region log
        private void getLog()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory;
            string Name = "ComunicadorREP2.log";
            string fileName = filePath + Name;

            List<string> Arquivos = new List<string>();
            List<string> Novo = new List<string>();

            Files.DirSearch(filePath, Name, Arquivos);

            foreach (string arquivo in Arquivos)
            {
                List<string> conteudo = new List<string>();
                Files.ReadFile(arquivo, conteudo);

                foreach (string msg in conteudo)
                {
                    if (msg.ToUpper().Contains("ALERTA - CONEXÃO ESTABELECIDA") || msg.ToUpper().Contains("INFO - COMUNICADOR INICIADO")
                        || msg.ToUpper().Contains("O ARQUIVO, "))
                        continue;

                    if (msg.ToUpper().Contains("INFO") || msg.ToUpper().Contains("ALERTA") || msg.ToUpper().Contains("EXCEÇÃO") || msg.ToUpper().Contains("ERRO - "))
                        log.AddLog(msg.Remove(0, 26));

                    if (msg.ToUpper().Contains("MENSAGEM: "))
                        log.AddLog(msg);
                }

                log.AddLineBreak();
                File.Delete(arquivo);
            }
        }

        public string FormatCaractere(string s)
        {
            string retorno = s.Replace("ç", "c");
            retorno = retorno.Replace("á", "a").Replace("à", "a").Replace("â", "a").Replace("ã", "a");
            retorno = retorno.Replace("é", "e").Replace("è", "e").Replace("ê", "e");
            retorno = retorno.Replace("í", "i").Replace("ì", "i").Replace("î", "i");
            retorno = retorno.Replace("ó", "o").Replace("ò", "o").Replace("ô", "o").Replace("õ", "o");
            retorno = retorno.Replace("ú", "u").Replace("ù", "u").Replace("û", "u");
            return retorno;
        }
        #endregion
    }
}
