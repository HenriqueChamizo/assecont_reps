
using System.Windows.Forms;
using REP_Prototipo;
using System;
using System.IO;
using Wr.Classes;
using System.Collections.Generic;

namespace Gertec
{
    public class Rede
    {
        string IP;
        public Log log;
        clEnvioDll envioDll = new clEnvioDll();

        public Rede(string ip, TextBox textbox)
        {
            this.IP = ip;
            log = new Log(textbox);
            log.Prefixo = ip;
        }

        public void LogMensagem(int Erro, bool DisplayIfOk = true)
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

        public bool Gertec_Conectar()
        {
            int Result = envioDll.ConectaREP(IP, 1);
            LogMensagem(Result, false);

            return (Result == 0);
        }

        public bool Gertec_Desconectar()
        {
            int Result = envioDll.ConectaREP(IP, 0);
            LogMensagem(Result, false);

            return (Result == 0);
        }

        public bool Gertec_EnviaEmpresa(string IdentificadorTipo, string Identificador, string Nome, string Cei, string Endereco)
        {
            int resultado = -1;
            char cTipo;

            cTipo = (IdentificadorTipo == "J") ? '1' : '2';

            log.AddLog(Consts.EMPREGADOR_ENVIANDO);

            if (Gertec_Conectar())
            {
                dllREP.REP_GravaCadastroEmpregador(
                IP,
                '0',
                cTipo,
                Identificador + '\0',
                Cei + '\0',
                Nome + '\0',
                Endereco + '\0',
                ref resultado);

                LogMensagem(resultado);
            }

            Gertec_Desconectar();

            return (resultado == 0);
        }

        public bool Gertec_EnviaFuncionario(string Nome, string Pis, string Proximidade, string CodigoBarras, string Teclado)
        {
            int resultado = -1;
            string Biometria = "0";
            byte grupo = 0;

            log.AddLog(String.Format(Consts.FUNCIONARIO_ENVIANDO, Nome));

            if (Proximidade != "")
            {
                Proximidade = Convert.ToString(Convert.ToInt64(Proximidade), 16);
            }

            if (Teclado.Length > 8) Teclado = "";

            dllREP.REP_GravaCadastroFuncionario
               (IP,
                '0',
                Nome + '\0',
                Pis + '\0',
                Proximidade + '\0',
                CodigoBarras + '\0',
                Teclado + '\0',
                Biometria + '\0',
                grupo,
                ref resultado);

            LogMensagem(resultado);
            return (resultado == 0);
        }

        public bool Gertec_ExcluirFuncionario(string Nome, string Pis)
        {
            int resultado = -1;
            log.AddLog(String.Format(Consts.FUNCIONARIOS_EXCLUINDO, Nome));
            dllREP.REP_ExcluiCadastroFuncionario(IP, Pis, ref resultado);
            LogMensagem(resultado);
            return (resultado == 0);
        }

        public bool Gertec_AtualizaDataHora()
        {
            int resultado = -1;

            if (Gertec_Conectar())
            {
                log.AddLog(Consts.DATA_HORA_ENVIANDO);

                int Ano = Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2));
                byte Mes = Convert.ToByte(DateTime.Now.Month);
                byte Dia = Convert.ToByte(DateTime.Now.Day);
                byte Hora = Convert.ToByte(DateTime.Now.Hour);
                byte Minuto = Convert.ToByte(DateTime.Now.Minute);
                byte Segundo = 0;
                byte DiaSemana = Convert.ToByte(DateTime.Now.DayOfWeek);

                dllREP.REP_Tempo(IP, 1, ref Ano, ref Mes, ref Dia, ref Hora, ref Minuto, ref Segundo, ref DiaSemana, ref resultado);
                LogMensagem(resultado);
                Gertec_Desconectar();
            }

            return (resultado == 0);
        }

        public bool Gertec_ImportarMarcacoes(int Terminal, string TerminalNome, Marcacoes marcacoes)
        {
            bool Result = false;
            DBApp db = new DBApp();
            int ProximoNsr = db.getLastNsr(Terminal) + 1;

            string diretoriomarcacoes = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Asseponto\\Marcacoes\\" + TerminalNome;
            Wr.Classes.Files.ForceDirectories(diretoriomarcacoes);
            
            string ArquivoTemp = Path.GetTempFileName();
            System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");

            int Contador = 0;

            if (Gertec_Conectar())
            {
                int resultado = envioDll.LeCadastro(IP, 5, ProximoNsr.ToString(), ArquivoTemp);
                if (resultado == 0)
                {
                    FileStream abreArquivoTxt = new FileStream(ArquivoTemp, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(abreArquivoTxt, iso_8859_1);
                    string line = "";

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] linhaReg = line.Split(';');
                        if (linhaReg[1] == "3")
                        {
                            string Pis = linhaReg[4];
                            bool Erro = false;
                            DateTime DataHora;

                            try
                            {
                                DataHora = new DateTime(
                                    Convert.ToInt32(linhaReg[2].Substring(4, 4)),
                                    Convert.ToInt32(linhaReg[2].Substring(2, 2)),
                                    Convert.ToInt32(linhaReg[2].Substring(0, 2)),
                                    Convert.ToInt32(linhaReg[3].Substring(0, 2)),
                                    Convert.ToInt32(linhaReg[3].Substring(2, 2)),
                                    0
                                    );

                                int Nsr = Convert.ToInt32(linhaReg[0]);

                                if (Nsr > ProximoNsr)
                                    ProximoNsr = Nsr;

                                marcacoes.Add(Pis, DataHora, Nsr);
                                Contador++;
                            }
                            catch
                            {
                                log.AddLog(line);
                                Erro = true;
                            }
                        }
                    }

                    sr.Close();

                    File.Delete(ArquivoTemp);

                    Gertec_Desconectar();

                    if (Contador > 0)
                    {
                        string Arquivo = marcacoes.SaveToFile();
                        log.AddLog(String.Format(Consts.ARQUIVO_GERADO, Arquivo));

                        Result = true;
                    }
                    else
                    {
                        log.AddLog(Consts.SEM_MARCACOES);
                    }

                    if (ProximoNsr > 0)
                    {
                        db.setLastNsr(Terminal, ProximoNsr);
                    }
                }
                else
                {
                    Gertec_Desconectar();
                    LogMensagem(resultado);
                }
            }

            return Result;
        }
    }
}
