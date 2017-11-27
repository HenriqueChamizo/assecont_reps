using AssepontoRep;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using zkemkeeper;
using System.IO;

namespace REP100
{
    class Bridge : AssepontoRep.Bridge
    {
        Rep100Class bioRep = new Rep100Class();
        private bool bIsConnected = false;//identifica se o Bio Rep está conectado
        private int numeroRep = 1;//número serial, após conectado será modificado.
        private int erroCod = 0; //número do erro

        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
        }

        #region Abstract override
        public override string getArquivoUsb()
        {
            return "";
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

        public override bool getGerarUsbEmpregador()
        {
            return false;
        }

        public override bool getNumeroSerieREP()
        {
            return false;
        }

        public override int getPortaPadrao()
        {
            return 4370;
        }

        public override string getRepFabricante()
        {
            return "Trix";
        }

        public override bool getPin()
        {
            return false;
        }

        public override bool getColumnId()
        {
            return false;
        }

        public override bool getDisconnectOnExit()
        {
            return false;
        }

        public override List<Types.Permissao> getPermissoes()
        {
            return null;
        }
        #endregion

        #region Override
        public override bool Connect(int Terminal)
        {
            base.Connect(Terminal);
            log.AddLog(Consts.SERVIDOR_INICIALIZANDO);
            int i = 0;
            do
            {
                bIsConnected = bioRep.Connect_Net(TerminalDados.IP, TerminalDados.Porta);
                if (!bIsConnected)
                    bioRep.Disconnect();
                i++;
            } while (!bIsConnected && i <= 10);

            if (bIsConnected)
            {
                log.AddLog(Consts.SERVIDOR_ONLINE);
                numeroRep = 1;
            }
            else
                log.AddLog(Consts.SERVIDOR_OFFLINE);

            return bIsConnected;
        }

        public override bool Disconnect(int Terminal)
        {
            base.Disconnect(Terminal);
            if (bIsConnected)
            {
                bIsConnected = false;
                bioRep.Disconnect();
                log.AddLog(Consts.SERVIDOR_PARANDO);
            }
            return !bIsConnected;
        }

        public override bool sendDataHora(int Terminal)
        {
            base.sendDataHora(Terminal);
            if (Connect(Terminal))
            {
                int ano = DateTime.Now.Year;
                int mes = DateTime.Now.Month;
                int dia = DateTime.Now.Day;
                int hora = DateTime.Now.Hour;
                int min = DateTime.Now.Minute;
                int seg = DateTime.Now.Second;

                if (bioRep.SetDeviceTime2(numeroRep, ano, mes, dia, hora, min, seg))
                {
                    log.AddLog(Consts.DATA_HORA_ATUALIZADA_SUCESSO);
                    return true;
                }
                else
                {
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    bioRep.GetLastError(ref erroCod);
                    return false;
                }
            }
            else
                return false;
        }

        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            base.sendInfoEmpresa(Terminal, out EmpregadorDados);
            if (Connect(Terminal))
            {
                if (bioRep.SetEmployer((int)numeroRep,
                                       (string)EmpregadorDados.Nome,
                                       (int)EmpregadorDados.PessoaTipo == 1 ? 0 : 1,
                                       (string)EmpregadorDados.Pessoa,
                                       (string)EmpregadorDados.Cei,
                                       (string)EmpregadorDados.Endereco))
                {
                    log.AddLog(Consts.EMPREGADOR_ENVIADO_SUCESSO);
                    Disconnect(Terminal);
                    return true;
                }
                else
                {
                    bioRep.GetLastError(ref erroCod);
                    log.AddLog(String.Format(Consts.ERRO_ENVIO_COMANDO_CODIGO, erroCod));
                    Disconnect(Terminal);
                    return false;
                }
            }
            else
                return false;
        }

        public override bool sendFuncionario(Types.Funcionario Funcionario)
        {
            base.sendFuncionario(Funcionario);
            if (bioRep.SSR_SetUserInfoEx(numeroRep, Funcionario.Ind.ToString(), (string)Funcionario.Nome, (string)Funcionario.Teclado, 0, true, "0" + (string)Funcionario.Pis, ""))
            {
                log.AddLog(String.Format(Consts.FUNCIONARIO_ENVIADO_COM_SUCESSO, Funcionario.Nome));
                return true;
            }
            else
                return false;
        }

        public override bool LerMarcacoes(Marcacoes marcacoes, TipoImportacaoMarcacoes tipoimportacao)
        {
            bool Result = base.LerMarcacoes(marcacoes, tipoimportacao);

            string nsr = "200";
            string pis;
            int ano;
            int mes;
            int dia;
            int hora;
            int min;
            int sec;

            DBApp db = new DBApp();
            int ProximoNsr = db.getLastNsr(TerminalDados.Indice) + 1;

            string ArquivoTemp = Path.GetTempFileName();

            int Contador = 0;

            bioRep.SetSeekPosition(numeroRep, ProximoNsr);

            bool connect = Connect(TerminalDados.Indice);

            if (connect)
            {
                log.AddLog("PROCESSANDO MARCAÇÕES...", true);
                List<string> arquivo = new List<string>();
                Wr.Classes.Files.ReadFile(ArquivoTemp, arquivo);
                while (bioRep.GetAttLogs(numeroRep, out nsr, out pis, out ano, out mes, out dia, out hora, out min, out sec))
                {
                    DateTime DataHora = new DateTime(ano, mes, dia, hora, min, sec);

                    marcacoes.Add(pis, DataHora, Convert.ToInt32(nsr));
                    Contador++;
                    Result = true;
                }
            }
            return Result;
        }
        #endregion


        #region Biometria (Comentado)
        //public override bool GerarArquivoBiometria(int Terminal)
        //{
        //    base.GerarArquivoBiometria(Terminal);
        //    bool result = false;
        //    string folder = Folders.folderArquivoBiometria(getRepFabricante());

        //    Types.Funcionario func = new Types.Funcionario();
        //    List<Types.Funcionario> lstFunc = new List<Types.Funcionario>();
        //    List<string> Arquivos = new List<string>();

        //    string matricula = "";
        //    string nome = "";
        //    string senha = "";
        //    int previlegio = -1;
        //    bool ativado = false;
        //    string pis = "";
        //    string CPF = "";
        //    int flag;
        //    string biometria;
        //    int tamanho;

        //    while (bioRep.SSR_GetAllUserInfoEx(numeroRep, out matricula, out nome, out senha, out previlegio, out ativado, out pis, out CPF))
        //    {
        //        if (ativado)
        //        {
        //            func.Ind = Convert.ToInt32(matricula);
        //            func.Nome = nome;
        //            func.Teclado = senha;
        //            func.Pis = pis;

        //            if (bioRep.GetUserTmpExStr_BZ400(numeroRep, func.Ind.ToString(), 1, out flag, out biometria, out tamanho))
        //            {
        //                func.Biometria = true;
        //            }
        //            else
        //                func.Biometria = false;


        //            lstFunc.Add(new Types.Funcionario()
        //            {
        //                Ind = func.Ind,
        //                Nome = func.Nome,
        //                Teclado = func.Teclado,
        //                Pis = func.Pis,
        //                Biometria = func.Biometria
        //            });
        //        }
        //    }

        //    try
        //    {

        //        Files.WriteFile(folder + "funcionario.txt", Arquivos);

        //        DirectoryInfo dir = new DirectoryInfo(folder);
        //        foreach (FileInfo f in dir.GetFiles())
        //        {
        //            File.Delete(folder + "\\" + f.Name);
        //        }

        //        File.Create(folder + "funcionario.txt").Close();
        //        dir = new DirectoryInfo(Application.StartupPath);
        //        string path = Application.StartupPath;

        //        foreach (FileInfo f in dir.GetFiles("*.rec"))
        //        {
        //            if (File.Exists(folder + "\\" + f.Name) == false)
        //            {
        //                //log.AddLog("lala");
        //                File.Move(f.FullName, folder + "\\" + f.Name);
        //            }
        //            else if (File.Exists(folder + "\\" + f.Name) == true)
        //            {
        //                File.Delete(folder + "\\" + f.Name);
        //            }
        //        }
        //        foreach (FileInfo f in dir.GetFiles("*.rec"))
        //        {
        //            if (File.Exists(folder + "\\" + f.Name) == false)
        //            {
        //                File.Move(f.FullName, folder + "\\" + f.Name);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        log.AddLog(Consts.ERRO_ENVIO_COMANDO);
        //        return false;
        //    }
        //    finally
        //    {
        //        Disconnect(Terminal);
        //    }

        //    return result;
        //}
        #endregion
    }
}
