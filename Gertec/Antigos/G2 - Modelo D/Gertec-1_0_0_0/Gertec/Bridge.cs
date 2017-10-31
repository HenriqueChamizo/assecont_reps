using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AssepontoRep;
using REP_Prototipo;
using System.IO;
using Wr.Classes;

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

        #region Abstract Override
        public override string getRepFabricante()
        {
            return "Gertec";
        }

        public override bool getAutenticacao()
        {
            return false;
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
        public override bool getGerarBiometria()
        {
            return true;
        }
        public override bool getCadastroTerminalSupervisor()
        {
            return false;
        }
        public override bool getCadastroTerminalResponsavel()
        {
            return false;
        }
        public override bool getFuncionariosAlteradosUsb()
        {
            return false;
        }
        public override bool getGerarUsbEmpregador()
        {
            return false;
        }
        public override bool getNumeroSerieREP()
        {
            return false;
        }
        public override bool getContemChaveAcessoREP()
        {
            return false;
        }
        public override bool getBoxFuncoes()
        {
            return false;
        }
        #endregion

        #region Override Enviar Data e Hora
        public override bool sendDataHora(int Terminal)
        {
            base.sendDataHora(Terminal);

            int resultado = -1;

            if (Gertec_Conectar())
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
                Gertec_Desconectar();
            }

            return (resultado == 0);
        }
        #endregion

        #region Override Enviar Funcionario
        public override bool sendFuncionario(Types.Funcionario Funcionario)
        {
            bool Result = base.sendFuncionario(Funcionario);

            int resultado = -1;
            string Biometria = "0";
            byte grupo = 0;

            if (Gertec_Conectar())
            {
                dllREP.REP_GravaCadastroFuncionario(
                   TerminalDados.IP,
                   '0',
                   Funcionario.Nome + '\0',
                   Funcionario.Pis + '\0',
                   Funcionario.Proximidade + '\0',
                   Funcionario.Barras + '\0',
                   Funcionario.Teclado + '\0',
                   Biometria + '\0',
                   grupo,
                   ref resultado);

                if (resultado == 0)
                {
                    Result = true;
                    log.AddLog("OK");
                }
                else
                    LogMensagem(resultado);
            }

            //Gertec_Desconectar();

            return Result;
        }
        public string Teclado(string Cracha)
        {
            return Cracha.ToString().Length < 8 ? Cracha.ToString() : "" + '\0';
        }

        public string Proximidade(string Cracha)
        {
            return Cracha != "" ? Convert.ToString(Convert.ToInt64(Cracha), 16) : "" + '\0';
        }
        #endregion

        #region Override Delete Funcionario
        public override bool deleteFuncionario(Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);

            int resultado = -1;
            if (Gertec_Conectar())
            {
                dllREP.REP_ExcluiCadastroFuncionario(TerminalDados.IP, Funcionario.Pis, ref resultado);
                LogMensagem(resultado);
            }

            //Gertec_Desconectar();
            return (resultado == 0);
        }
        #endregion

        #region Override Enviar Empregador
        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            base.sendInfoEmpresa(Terminal, out EmpregadorDados);

            int resultado = -1;
            //, encontrado = 0;
            char cTipo, cAcao;

            cTipo = ((int)EmpregadorDados.PessoaTipo == 1 ? '1' : '2');

            //string[] aux = new string[] { "al", "alalal" };
            string folder = Folders.folderArquivoUsb(getRepFabricante());

            Files.WriteFile(folder + "empresa.txt", new List<string>());

            if (Gertec_Conectar())
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

            Gertec_Desconectar();

            return (resultado == 0);
        }
        #endregion

        #region Override Importar Marcações
        public override bool LerMarcacoes(Marcacoes marcacoes, AssepontoRep.Bridge.TipoImportacaoMarcacoes tipoimportacao)
        {
            bool Result = base.LerMarcacoes(marcacoes, tipoimportacao);

            DBApp db = new DBApp();
            int ProximoNsr = db.getLastNsr(TerminalDados.Indice) + 1;

            string ArquivoTemp = Path.GetTempFileName();

            int Contador = 0;

            if (Gertec_Conectar())
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
                    Gertec_Desconectar();
                }
                else
                {
                    Gertec_Desconectar();
                    LogMensagem(resultado);
                }
            }

            return Result;
        }
        #endregion

        #region Override Recolher Biometria
        public override bool GerarArquivoBiometria(int Terminal)
        {
            base.GerarArquivoBiometria(Terminal);
            bool result = false;

            try
            {
                string folder = Folders.folderArquivoBiometria(getRepFabricante());
                List<string> Arquivos = new List<string>();

                Files.WriteFile(folder + "funcionario.txt", Arquivos);

                DirectoryInfo dir = new DirectoryInfo(folder);
                foreach (FileInfo f in dir.GetFiles())
                {
                    File.Delete(folder + "\\" + f.Name);
                }

                File.Create(folder + "funcionario.txt").Close();

                int resultado = 0;

                if (Gertec_Conectar())
                {
                    resultado = envioDll.LeCadastro(TerminalDados.IP, 4, "", folder + "funcionario.txt");
                    result = true;

                    if (resultado == 0)
                    {
                        dir = new DirectoryInfo(Application.StartupPath);
                        string path = Application.StartupPath;

                        resultado = envioDll.LeFinger(TerminalDados.IP, path);


                        foreach (FileInfo f in dir.GetFiles("*.rec"))
                        {
                            if (File.Exists(folder + "\\" + f.Name) == false)
                            {
                                //log.AddLog("lala");
                                File.Move(f.FullName, folder + "\\" + f.Name);
                            }
                            else if (File.Exists(folder + "\\" + f.Name) == true)
                            {
                                File.Delete(folder + "\\" + f.Name);
                            }
                        }
                        foreach (FileInfo f in dir.GetFiles("*.rec"))
                        {
                            if (File.Exists(folder + "\\" + f.Name) == false)
                            {
                                File.Move(f.FullName, folder + "\\" + f.Name);
                            }
                        }

                        if (resultado == 0)
                        {
                            log.AddLog(Consts.BIOMETRIA_ARQUIVO_GERADO_SUCESSO);
                        }
                        else
                        {
                            if (resultado == 4100 && result == true)
                                log.AddLog("Funcionarios não tem biometria cadastrada, arquivo gerado somente com os dados");
                            else
                                LogMensagem(resultado);
                        }
                    }
                    else
                        LogMensagem(resultado);
                }

            }
            catch
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                return false;
            }
            finally
            {
                Gertec_Desconectar();
            }

            return result;
        }
        #endregion

        #region Override Enviar Arquivo Biometria
        public override bool EnviarArquivoBiometria(int Terminal)
        {
            base.EnviarArquivoBiometria(Terminal);
            bool result = false;

            System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
            int ok = 0;
            string Nome;
            string Pis = "";
            string Contactless;
            string CodBarras;
            string Teclado;
            string Biometria;
            byte Grupo = 0;

            int resultado = 0;
            int i = 0, contadorErro = 0, contadorSucesso = 0;

            string folder = Folders.folderArquivoBiometria(getRepFabricante());
            string Caminho = folder + "funcionario.txt";

            try
            {
                if (Gertec_Conectar())
                {
                    if (File.Exists(Caminho))
                    {
                        StreamReader sr = new StreamReader(Caminho, iso_8859_1);
                        string lineAux = "";
                        try
                        {
                            do
                            {
                                lineAux = sr.ReadLine();

                                if (lineAux != "")
                                {
                                    string[] line = lineAux.Split(';');

                                    Nome = line[0].Trim() + '\0';
                                    Pis = line[1].Trim() + '\0';
                                    Contactless = line[2].Trim() + '\0';
                                    CodBarras = line[3].Trim() + '\0';
                                    Teclado = line[4].Trim() + '\0';
                                    Biometria = line[5].Trim() + '\0';
                                    Grupo = 0;

                                    resultado = -1;

                                    dllREP.REP_GravaCadastroFuncionario(
                                                TerminalDados.IP,
                                                '0',
                                                Nome,
                                                Pis,
                                                Contactless,
                                                CodBarras,
                                                Teclado,
                                                Biometria,
                                                Grupo,
                                                ref resultado);

                                    if (resultado == 0)
                                    {
                                        result = true;
                                        contadorSucesso++;
                                        ok = 0;
                                    }
                                    else if (resultado >= 3000 && resultado <= 3031)
                                    {
                                        log.AddLog(Pis);
                                        LogMensagem(resultado);
                                        contadorErro++;
                                    }
                                    else
                                    {
                                        log.AddLog(Pis);
                                        LogMensagem(resultado);
                                        ok = 1;
                                        break;
                                    }
                                }
                                i++;
                            } while (!sr.EndOfStream);

                            #region Arquivo Biometria
                            int contador = 0;
                            string[] pasta = Directory.GetFiles(folder);

                            try
                            {
                                foreach (string arquivo in pasta)
                                {
                                    dllREP.REP_GravaFinger(TerminalDados.IP, arquivo, ref resultado);
                                    if (resultado == 3031)
                                    {
                                        log.AddLog("PIS não cadastrado");
                                        contador--;
                                    }
                                    contador++;
                                }

                                result = true;
                            }
                            catch
                            {
                                log.AddLog(Consts.ERRO_ENVIO_COMANDO);

                            }
                            #endregion
                        }
                        catch
                        {
                            log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                        }

                        if (ok != 0)
                        {
                            log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                        }
                    }
                    else
                    {
                        log.AddLog("ARQUIVO NÃO EXISTE");
                    }
                }
            }
            catch
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
            }

            if (result)
                log.AddLog(Consts.BIOMETRIA_ARQUIVO_ENVIADO_SUCESSO);

            return result;
        }
        #endregion

        #region Private
        private bool Gertec_Conectar()
        {
            int tentativas = 0;
            int Result = -1;

            while (tentativas < 200 && Result != 0)
            {
                Result = envioDll.ConectaREP(TerminalDados.IP, 1);

                if (Result == 10005)
                    Result = 0;
            }

            LogMensagem(Result, false);

            return (Result == 0);
        }

        private bool Gertec_Desconectar()
        {
            int Result = envioDll.ConectaREP(TerminalDados.IP, 0);
            LogMensagem(Result, false);

            return (Result == 0);
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
    }
}
