using AssepontoRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using zkemkeeperbz900;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.IO;
using Wr.Classes;

namespace Rep200
{
    public class Bridge : AssepontoRep.Bridge
    {
        CBZ900Class bioRep = new CBZ900Class();
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
            return true;
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
            return "RHJ";
        }

        public override bool getPin()
        {
            return false;
        }

        public override bool getDisconnectOnExit()
        {
            return true;
        }

        public override bool getColumnId()
        {
            return true;
        }

        public override List<Types.Permissao> getPermissoes()
        {
            return new List<Types.Permissao>() {
                new Types.Permissao(1, "Definir como Administrador", "Admin")
            };
        }
        #endregion

        #region Override
        public override bool ValidarPermissao(int Codigo, int Terminal, List<Types.Funcionario> listFuncionarios, ref string Mensagem)
        {
            foreach(Types.Funcionario f in listFuncionarios)
            {
                string cpf = (new DBApp().getFieldValueString(string.Format(@"select isnull(replace(replace(b.REG_CPF_NUMERO, '.', ''), '-', ''), '') as CPF 
                                                                            from Funcionarios a 
                                                                            inner join Registro b on a.FUNC_IND = b.REG_FUNC 
                                                                            where a.FUNC_IND = {0} ", f.Ind)));
                if (string.IsNullOrEmpty(cpf))
                {
                    //MessageBox.Show("O funcionario precisa de CPF cadatrado no registro eletronico para ser Admin.", "Erro no processo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Mensagem = "O funcionario precisa de CPF cadastrado no registro eletronico para ser Admin.";
                    return false;
                }
            }
            return true;
        }

        public override bool Connect(int Terminal)
        {
            if (!bIsConnected)
            {
                base.Connect(Terminal);
                log.AddLog(Consts.SERVIDOR_INICIALIZANDO);
                int i = 0;
                do
                {
                    bioRep.SetCommPassword(Convert.ToInt32(TerminalDados.OperadorSenha));
                    //bioRep.SetAesPassword("trix2016");
                    bioRep.SetAesPassword("PONTORHJ05305232000");
                    bIsConnected = bioRep.Connect_Net(TerminalDados.IP, TerminalDados.Porta);
                    if (!bIsConnected)
                    {
                        bioRep.Disconnect();
                        bioRep = new CBZ900Class();
                    }
                    else
                    {
                        bioRep.RegEvent(numeroRep, 65535);
                        if (bioRep.SetOprateCPF(numeroRep, TerminalDados.OperadorCpf))
                            log.AddLog("Conectado, CPF: " + TerminalDados.OperadorCpf);
                        else
                        {
                            int erro = 0;
                            bioRep.GetLastError(ref erro);
                            log.AddLog("CPF inválido,coloque um CPF válido: " + erro.ToString() + "Erro");
                        }
                    }
                    i++;
                } while (!bIsConnected && i <= 10);

                if (bIsConnected)
                {
                    log.AddLog(Consts.SERVIDOR_ONLINE);
                    numeroRep = 1;
                }
                else
                    log.AddLog(Consts.SERVIDOR_OFFLINE);
            }

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
                bioRep.EnableDevice(numeroRep, false);
                if (bioRep.SetDeviceTime(numeroRep))
                {
                    log.AddLog(Consts.DATA_HORA_ATUALIZADA_SUCESSO);
                    bioRep.EnableDevice(numeroRep, true);
                    return true;
                }
                else
                {
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    bioRep.GetLastError(ref erroCod);
                    bioRep.EnableDevice(numeroRep, true);
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
                bioRep.EnableDevice(numeroRep, false);
                if (bioRep.SetEmployer(numeroRep, EmpregadorDados.Nome, Convert.ToInt32(EmpregadorDados.PessoaTipo), EmpregadorDados.Pessoa, EmpregadorDados.Cei, EmpregadorDados.Endereco))
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
            bioRep.EnableDevice(numeroRep, false);
            string cpf = (new DBApp().getFieldValueString(string.Format(@"select isnull(replace(replace(b.REG_CPF_NUMERO, '.', ''), '-', ''), '') as CPF 
                                                                            from Funcionarios a 
                                                                            inner join Registro b on a.FUNC_IND = b.REG_FUNC 
                                                                            where a.FUNC_IND = {0} ", Funcionario.Ind)));
            int privilege = 0;
            if (!string.IsNullOrEmpty(cpf) && (Funcionario.Permissao != null && Funcionario.Permissao.Value == 1))
                privilege = Funcionario.Permissao.Value;
            if (bioRep.SSR_SetUserInfoEx_BZ900(numeroRep, Funcionario.Ind.ToString(), Funcionario.Nome, Funcionario.Teclado, privilege, "0" + Funcionario.Pis, cpf, Funcionario.Barras))
            {
                log.AddLog(String.Format(Consts.FUNCIONARIO_ENVIADO_COM_SUCESSO, Funcionario.Nome));
                bioRep.EnableDevice(numeroRep, true);
                return true;
            }
            else
            {
                int idwErrorCode = 0;
                bioRep.GetLastError(ref idwErrorCode);
                log.AddLog(string.Format(Consts.ERRO_ENVIO_COMANDO_CODIGO, idwErrorCode.ToString()));
                bioRep.EnableDevice(numeroRep, true);
                return false;
            }
        }

        public override bool deleteFuncionario(Types.Funcionario Funcionario)
        {
            base.deleteFuncionario(Funcionario);
            bioRep.EnableDevice(numeroRep, false);
            if (bioRep.SSR_DeleteEnrollDataExt_BZ900(numeroRep, Funcionario.Ind.ToString(), 12))
            {
                bioRep.RefreshData(numeroRep);
                log.AddLog(String.Format(Consts.FUNCIONARIO_EXCLUIDO, Funcionario.Nome));
                bioRep.EnableDevice(numeroRep, true);
                return true;
            }
            else
            {
                int idwErrorCode = 0;
                bioRep.GetLastError(ref idwErrorCode);
                if (idwErrorCode == 0)
                {
                    log.AddLog(String.Format(Consts.FUNCIONARIO_EXCLUIDO, Funcionario.Nome));
                    bioRep.EnableDevice(numeroRep, true);
                    return true;
                }
                else
                {
                    log.AddLog(string.Format(Consts.ERRO_ENVIO_COMANDO_CODIGO, idwErrorCode.ToString()));
                    bioRep.EnableDevice(numeroRep, true);
                    return false;
                }
            }
        }

        public override bool LerMarcacoes(Marcacoes marcacoes, TipoImportacaoMarcacoes tipoimportacao)
        {
            bool Result = false;
            base.LerMarcacoes(marcacoes, tipoimportacao);

            string nsr = "";
            string pis = "";
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int min = 0;
            int sec = 0;
            int iIndex = 0;
            int idwErrorCode = -9;
            DBApp db = new DBApp();
            int ProximoNsr = db.getLastNsr(TerminalDados.Indice) + 1;

            bool connect = Connect(TerminalDados.Indice);
            //bioRep.SetSeekPosition(TerminalDados.Indice, ProximoNsr);

            if (connect)
            {
                log.AddLog("PROCESSANDO MARCAÇÕES...", true);
                bioRep.EnableDevice(numeroRep, false);
                while (bioRep.GetAttLogs_BZ900(numeroRep, out nsr, out pis, out year, out month, out day, out hour, out min, out sec))
                {
                    DateTime DataHora = new DateTime(year, month, day, hour, min, sec);

                    marcacoes.Add(pis, DataHora, Convert.ToInt32(nsr));
                    if (iIndex % 50 == 0)
                        System.Threading.Thread.Sleep(200);
                    iIndex++;
                    Result = true;

                }
                bioRep.GetLastError(ref idwErrorCode);
            }

            if (iIndex == 0)
                Result = false;

            bioRep.EnableDevice(numeroRep, true);
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
