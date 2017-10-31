using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AssepontoRep;
using REP_Prototipo;
using System.IO;

namespace Gertec
{
    class Bridge : AssepontoRep.Bridge
    {
        clEnvioDll envioDll = new clEnvioDll();

        #region Constructor
        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
        }
        #endregion

        private const string FILE_CONFIGURATION = "wifi.cfg";
        private const string FILE_EMPLOYER = "empregador.gsv";
        private const string FILE_STAFF = "empregados.gsv";
        private string FILE_PATH = Folders.folderArquivoUsb("Gertec");

        #region Abstract Override
        public override string getRepFabricante()
        {
            return "Gertec";
        }

        public override int getPortaPadrao()
        {
            return 7000;
        }

        public override string getArquivoUsb()
        {
            return "";
        }

        public override bool getGerarBackup()
        {
            return false;
        }

        public override bool getGerarUsb()
        {
            return true;
        }

        public override bool getAutenticacao()
        {
            return false;
        }

        public override bool getBoxFuncoes()
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

        public override bool getEnviarHorarioVerao()
        {
            return false;
        }

        public override bool getEnviarHorarioVeraoUsb()
        {
            return false;
        }

        public override bool getFuncionariosAlteradosUsb()
        {
            return false;
        }

        public override bool getGerarBiometria()
        {
            return false;
        }

        public override bool getGerarUsbEmpregador()
        {
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool getNumeroSerieREP()
        {
            return false;
        }

        public override bool Connect(int Terminal)
        {
            int Result = envioDll.ConectaREP(TerminalDados.IP, 1);
            LogMensagem(Result, false);

            return (Result == 0);
        }

        public override bool Disconnect(int Terminal)
        {
            int Result = envioDll.ConectaREP(TerminalDados.IP, 0);
            LogMensagem(Result, false);

            return (Result == 0);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void UsbFileFinalizacao(string Folder)
        {
            base.UsbFileFinalizacao(Folder);
        }

        public override void UsbFileFuncionario(Types.Funcionario Funcionario, Types.Opcao Opcao, List<string> Line)
        {
            base.UsbFileFuncionario(Funcionario, Opcao, Line);
        }

        public override void UsbFileFuncionario(Types.Funcionario Funcionario, Types.Opcao Opcao, ref string Line)
        {
            base.UsbFileFuncionario(Funcionario, Opcao, ref Line);
        }

        public override void UsbFileInicializacao(string Folder)
        {
            base.UsbFileInicializacao(Folder);
        }
        #endregion

        #region Override
        public override bool sendDataHora(int Terminal)
        {
            base.sendDataHora(Terminal);

            int resultado = -1;

            if (Connect(Terminal))
            {
                int Ano = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2));
                byte Mes = Convert.ToByte(DateTime.Now.Month);
                byte Dia = Convert.ToByte(DateTime.Now.Day);
                byte Hora = Convert.ToByte(DateTime.Now.Hour);
                byte Minuto = Convert.ToByte(DateTime.Now.Minute);
                byte Segundo = 0;
                byte DiaSemana = Convert.ToByte(DateTime.Now.DayOfWeek);

                dllREP.REP_Tempo(TerminalDados.IP, 1, ref Ano, ref Mes, ref Dia, ref Hora, ref Minuto, ref Segundo, ref DiaSemana, ref resultado);
                LogMensagem(resultado);
                Disconnect(Terminal);
            }

            return (resultado == 0);
        }

        public override bool deleteFuncionario(Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);

            int resultado = -1;
            dllREP.REP_ExcluiCadastroFuncionario(TerminalDados.IP, Funcionario.Pis, ref resultado);
            LogMensagem(resultado);
            return (resultado == 0);
        }

        public override bool sendFuncionario(Types.Funcionario Funcionario)
        {
            bool Result = true;
            base.sendFuncionario(Funcionario);

            int resultado = -1;
            string Biometria = "0";
            byte grupo = 0;

            foreach (long Cracha in Funcionario.Crachas)
            {
                dllREP.REP_GravaCadastroFuncionario(
                    TerminalDados.IP,
                    '0',
                    Funcionario.Nome + '\0',
                    Funcionario.Pis + '\0',
                    Cracha.ToString() + '\0',
                    Cracha.ToString() + '\0',
                    "" + '\0',
                    Biometria + '\0',
                    grupo,
                    ref resultado);
                LogMensagem(resultado);

                if (resultado == -1)
                    Result = false;
            }

            return Result;
        }

        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            base.sendInfoEmpresa(Terminal, out EmpregadorDados);

            int resultado = -1;
            //, encontrado = 0;
            char cTipo, cAcao;

            cTipo = ((int)EmpregadorDados.PessoaTipo == 1 ? '1' : '2');

            //string[] aux = new string[] { "al", "alalal" };
            string folder = Folders.folderArquivoUsb(getRepFabricante());

            Wr.Classes.Files.WriteFile(folder + "empresa.txt", new List<string>());

            if (Connect(Terminal))
            {

                if (envioDll.LeCadastro(TerminalDados.IP, 0, "", folder + "empresa.txt") == 0)
                {
                    cAcao = '1';
                }
                else
                {
                    cAcao = '0';
                }

                dllREP.REP_GravaCadastroEmpregador(
                TerminalDados.IP,
                cAcao,
                cTipo,
                EmpregadorDados.Pessoa + '\0',
                EmpregadorDados.Cei + '\0',
                EmpregadorDados.Nome + '\0',
                EmpregadorDados.Endereco + '\0',
                ref resultado);

                LogMensagem(resultado);
            }

            Disconnect(Terminal);

            return (resultado == 0);
        }

        public override bool LerMarcacoes(Marcacoes marcacoes, AssepontoRep.Bridge.TipoImportacaoMarcacoes tipoimportacao)
        {
            bool Result = base.LerMarcacoes(marcacoes, tipoimportacao);

            DBApp db = new DBApp();
            int ProximoNsr = db.getLastNsr(TerminalDados.Indice) + 1;

            string ArquivoTemp = Path.GetTempFileName();

            int Contador = 0;

            if (Connect(TerminalDados.Indice))
            {
                int resultado = envioDll.LeCadastro(TerminalDados.IP, 5, ProximoNsr.ToString(), ArquivoTemp);

                if (resultado == 0)
                {
                    List<string> arquivo = new List<string>();
                    Wr.Classes.Files.ReadFile(ArquivoTemp, arquivo);

                    foreach (string S in arquivo)
                    {
                        string[] linhaReg = S.Split(';');
                        if (linhaReg[1] == Convert.ToInt32(Marcacoes.TiposRegistroAfd.Marcacao).ToString())
                        {
                            string Pis = linhaReg[4];

                            try
                            {
                                DateTime DataHora = new DateTime(
                                    Convert.ToInt32(linhaReg[2].Substring(4, 4)),
                                    Convert.ToInt32(linhaReg[2].Substring(2, 2)),
                                    Convert.ToInt32(linhaReg[2].Substring(0, 2)),
                                    Convert.ToInt32(linhaReg[3].Substring(0, 2)),
                                    Convert.ToInt32(linhaReg[3].Substring(2, 2)),
                                    0
                                    );

                                int Nsr = Convert.ToInt32(linhaReg[0]);

                                marcacoes.Add(Pis, DataHora, Nsr);
                                Contador++;
                                Result = true;
                            }
                            catch
                            {
                                log.AddLog(S);
                                Result = false;
                            }
                        }
                    }

                    File.Delete(ArquivoTemp);

                    Disconnect(TerminalDados.Indice);
                }
                else
                {
                    Disconnect(TerminalDados.Indice);
                    LogMensagem(resultado);
                }
            }

            return Result;
        }
        #endregion

        #region Private
        private void deleteArquivo(string filePath, string Name)
        {
            List<string> Arquivos = new List<string>();

            Wr.Classes.Files.DirSearch(filePath, Name, Arquivos);

            foreach (string arquivo in Arquivos)
            {
                File.Delete(arquivo);
            }
        }

        private void LogMensagem(int Erro, bool DisplayIfOk = true)
        {
            if (Erro == 0)
            {
                if (DisplayIfOk) log.LogOk();
            }
            else
            {
                log.AddLog(clRetornaErro.retornoREP(Erro), true);
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
                            #region Nome
                            string Nome = func.Nome.Replace(";", "");
                            do
                            {
                                if (Nome.Length < 52)
                                {
                                    for (int i = Nome.Length; i < 52; i++)
                                    {
                                        Nome = Nome + " ";
                                    }
                                }
                            } while (Nome.Length < 52);
                            #endregion
                            #region Pis
                            string Pis = "0" + func.Pis.Replace(";", "");
                            do
                            {
                                if (Pis.Length < 12)
                                {
                                    for (int i = Pis.Length; i < 12; i++)
                                    {
                                        Pis = Pis + " ";
                                    }
                                }
                            } while (Pis.Length < 12);
                            #endregion
                            #region Contactless
                            string Contactless = func.Proximidade.Replace(";", "");
                            do
                            {
                                if (Contactless.Length < 16)
                                {
                                    for (int i = Contactless.Length; i < 16; i++)
                                    {
                                        Contactless = Contactless + " ";
                                    }
                                }
                            } while (Contactless.Length < 16);
                            #endregion
                            #region Matricula
                            string Matricula = (func.Matricula == null) ? "" : func.Matricula.Replace(";", "");
                            do
                            {
                                if (Matricula.Length < 16)
                                {
                                    for (int i = Matricula.Length; i < 16; i++)
                                    {
                                        Matricula = Matricula + " ";
                                    }
                                }
                            } while (Matricula.Length < 16);
                            #endregion
                            #region Teclado
                            string Teclado = func.Teclado.Replace(";", "");
                            do
                            {
                                if (Teclado.Length < 8)
                                {
                                    for (int i = Teclado.Length; i < 8; i++)
                                    {
                                        Teclado = Teclado + " ";
                                    }
                                }
                            } while (Teclado.Length < 8);
                            #endregion
                            linha = Nome + ";" + Pis + ";" + Contactless + ";" + Matricula + ";" + Teclado + ";\r\n";
                            Novo.Add(linha);
                        }
                    }
                }

                if (Novo.Count == 0)
                {
                    log.AddLog(Consts.USB_SEM_FUNCIONARIOS);
                    return false;
                }

                string count;
                bool primeiro = true;
                StreamWriter sw = new StreamWriter(FILE_PATH + FILE_STAFF, false, Encoding.GetEncoding(1252));
                foreach (string lin in Novo)
                {
                    if (primeiro)
                    {
                        count = Novo.Count.ToString();
                        if (count.Length < 5)
                        {
                            for (int i = count.Length; i < 5; i++)
                            {
                                count = "0" + count;
                            }
                            sw.Write(count + "\r\n");
                        }
                        else
                            sw.Write(count + "\r\n");

                        sw.Flush();
                        primeiro = false;
                    }
                    sw.Write(Wr.Classes.Strings.RemoveAccents(lin));
                    sw.Flush();
                }

                log.AddLog(string.Format(Consts.USB_ARQUIVO_GERADO_SUCESSO, FILE_PATH));
                sw.Close();
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
                #region Pessoa
                string Pessoa = EmpregadorDados.Pessoa.Replace(";", "");
                do
                {
                    if (Pessoa.Length < 16)
                    {
                        for (int i = Pessoa.Length; i < 16; i++)
                        {
                            Pessoa = Pessoa + " ";
                        }
                    }
                } while (Pessoa.Length < 16);
                #endregion
                #region Cei
                string Cei = EmpregadorDados.Cei.Replace(";", "");
                do
                {
                    if (Cei.Length < 12)
                    {
                        for (int i = Cei.Length; i < 12; i++)
                        {
                            Cei = Cei + " ";
                        }
                    }
                } while (Cei.Length < 12);
                #endregion
                #region Nome
                string Nome = EmpregadorDados.Nome.Replace(";", "");
                do
                {
                    if (Nome.Length < 152)
                    {
                        for (int i = Nome.Length; i < 152; i++)
                        {
                            Nome = Nome + " ";
                        }
                    }
                } while (Nome.Length < 152);
                #endregion
                #region Endereco
                string Endereco = EmpregadorDados.Endereco.Replace(";", "").Replace("- ", "").Replace(". ", "").Replace("-", "").Replace(".", "");
                do
                {
                    if (Endereco.Length < 100)
                    {
                        for (int i = Endereco.Length; i < 100; i++)
                        {
                            Endereco = Endereco + " ";
                        }
                    }
                } while (Endereco.Length < 100);
                #endregion
                #region PessoaTipo
                int PessoaTipo = 0;
                if (EmpregadorDados.PessoaTipo == Types.PessoaTipo.Cnpj)
                    PessoaTipo = 1;
                else if (EmpregadorDados.PessoaTipo == Types.PessoaTipo.Cpf)
                    PessoaTipo = 2;
                else
                    PessoaTipo = 0;
                #endregion

                deleteArquivo(FILE_PATH, FILE_EMPLOYER);
                Novo.Add(string.Format("{0};{1};{2};{3};{4};\r\n", Pessoa, Cei, Nome, Endereco, PessoaTipo));

                StreamWriter sw = new StreamWriter(FILE_PATH + FILE_EMPLOYER, false, Encoding.GetEncoding(1252));
                foreach (string lin in Novo)
                {
                    sw.Write(Wr.Classes.Strings.RemoveAccents(lin));
                    sw.Flush();
                }

                log.AddLog(Consts.USB_EMPEGADOR_SUCESSO);
                sw.Close();
                return true;
            }
            catch (Exception e) { log.AddLog(e.Message); return false; }
        }
        #endregion
    }
}
