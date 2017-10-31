/*****************************************************************************
* Aplicativo : 	???????	                                     *
* Autor	     : Ana Lúcia S. Melo (para esta classe)    	     			     *
* Referência : Programa para coleta do REP                                   *
* Data       : 18/05/11						                                 *
* Ambiente   : Visual C# 2008                    						     *
* Fonte      : txtFiles                   		  	                         *
* Objetivo   : Responsável pelo tratamento do txt.                           *
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace REP_Prototipo
{
    public class txtFiles
    {
        public csEncryptDecrypt csEncrypt = new csEncryptDecrypt();
        public int contador = 0;

        //******************************************************************************/
        // public: exportaArquivo                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 16/05/2011
        // Descrição : Exporta o arquivo referente a coleta de registro
        // Retorno: void
        //
        //******************************************************************************         
        public void exportaArquivo(string CaminhoArq, string CaminhoSalvar, string separador, 
            string dataInicial, string dataFinal, string horaInicial, string horaFinal)
        {
            try
            {
                //Recebe informações sobre o arquivo que será lido e armazena no abreArquivoTxt
                FileInfo abreArquivoTxt = new FileInfo(CaminhoArq);
                //Sera salvo com o nome igual a data atual
                DateTime agora = DateTime.Now;
                string datahora = agora.Day.ToString() + agora.Month.ToString() + agora.Year.ToString();
                //Verifica se há o arquivo na pasta com o mesmo nome
                if (File.Exists(CaminhoSalvar + "\\" + agora.Day + agora.Month + agora.Year + ".txt"))
                    //Caso tenha o arquivo com o mesmo nome apaga o arquivo
                    File.Delete(CaminhoSalvar + "\\" + datahora + ".txt");
                //Cria o arquivo no local onde deseja salva-lo
                FileStream salvaArquivoTxt = new FileStream(CaminhoSalvar + "\\" + datahora + ".txt", FileMode.Create);
                //Abre o arquivo para escrita
                StreamWriter salvandoArquivoTxt = new StreamWriter(salvaArquivoTxt);
                string linha;
                //Abre o arquivo que será lido em modo de leitura
                using (StreamReader lendoArquivoTxt = abreArquivoTxt.OpenText())
                {
                    //Verifica todas as linhas existentes
                    while ((linha = lendoArquivoTxt.ReadLine()) != null)
                    {
                        if (linha != "")
                        {
                            string[] lineArq = linha.Split(';');
                            string data = lineArq[2].Substring(0, 2) + "/" + lineArq[2].Substring(2, 2) + "/" +
                                lineArq[2].Substring(4, 4);
                            string hora = lineArq[3].Substring(0, 2) + ":" + lineArq[3].Substring(2, 2);

                            DateTime dataArq, horaArq;
                            dataArq = Convert.ToDateTime(data);
                            horaArq = Convert.ToDateTime(hora);

                            //Verifica a  data
                            if ((DateTime.Compare(dataArq, Convert.ToDateTime(dataInicial)) >= 0) && 
                                (DateTime.Compare(dataArq, Convert.ToDateTime(dataFinal)) <= 0))
                                if ((DateTime.Compare(horaArq, Convert.ToDateTime(horaInicial)) >= 0) && 
                                    (DateTime.Compare(horaArq, Convert.ToDateTime(horaFinal)) <= 0))
                                    //Se for marcação de ponto adiciona ao txt novo
                                    salvandoArquivoTxt.WriteLine(data + separador + hora + separador + lineArq[4]);
                        }
                    }                   
                    //Fecha os arquivos para salva-lo e liberar para utilização
                    salvandoArquivoTxt.Flush();
                    salvandoArquivoTxt.Close();
                    salvaArquivoTxt.Close();
                    lendoArquivoTxt.Dispose();
                    lendoArquivoTxt.Close();
                }
            }
            catch
            {
                
            }
        }

        //******************************************************************************/
        // public: consultaTxtPwd                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 18/05/2011
        // Descrição : consulta no arquivo pwd se existe o usuario
        // Retorno: string
        //
        //******************************************************************************         
        public string consultaTxtPwd(string arquivo, string consultar)//", string setor")
        {
            //Le o arquivo 
            FileInfo abreArquivoTxt = new FileInfo(arquivo);
            StreamReader lendoArquivoTxt = abreArquivoTxt.OpenText();

            // setor = csEncrypt.criptografaSenha(setor);
            try
            {
                string str;

                //Separa o que será consultado em colunas removendo o separador
                string[] criptografa = consultar.Split(';');
                //criptografa o que será consultado para poder fazer a comparação
                string retorno = csEncrypt.criptografaSenha(criptografa[0]);
                //Le linha a linha do arquivo
                while ((str = lendoArquivoTxt.ReadLine()) != null)
                {
                    //Separa a linha em colunas removendo o separador
                    string[] linha = str.Split(';');
                    //Verifica se ja existe
                    if ((linha[0].Equals(retorno)) /*& (linha[3].Equals(setor))*/)
                    {
                        lendoArquivoTxt.Dispose();
                        lendoArquivoTxt.Close();
                        return str; //retorno str: existe   
                    }
                }
                lendoArquivoTxt.Dispose();
                lendoArquivoTxt.Close();

                return "0";//retorno 0: não existe o registro                
            }
            catch
            {
                lendoArquivoTxt.Dispose();
                lendoArquivoTxt.Close();
                return "2";//retorno 2: erro
            }
        }


        //******************************************************************************/
        // public: adicionaTxtPwd                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 18/05/2011
        // Descrição : Adiciona novo usuario ao pwd
        // Retorno: byte
        //
        //******************************************************************************         
        public byte adicionaTxtPwd(string arquivo, string user, string pass, string permissao, string setor)
        {
            //Abre o arquivo para escrita
            //StreamWriter salvandoArquivoTxt = new StreamWriter(arquivo);
            TextWriter salvandoArquivoTxt = File.AppendText(arquivo);
            try
            {
                //Se for marcação de ponto adiciona ao txt novo
                salvandoArquivoTxt.WriteLine(user + ";" + pass + ";" + permissao + ";" + setor);
                //Fecha o arquivo para salvar
                // salvandoArquivoTxt.Flush();
                salvandoArquivoTxt.Close();
                return 0; //retorno 0 esquecadastrado sem erro
            }
            catch
            {
                salvandoArquivoTxt.Close();
                return 1;//retorno 1 erro
            }
        }

        //******************************************************************************/
        // public: consultaNoPwd                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 18/05/2011
        // Descrição : Consultas no pwd
        // Retorno: string
        //
        //******************************************************************************         
        public string consultaNoPwd(string arquivo, string consultar, int tipoConsulta)
        {
            //Le o arquivo 
            if (File.Exists(arquivo))
            {
                //Abre o arquivo
                FileInfo abreArquivoTxt = new FileInfo(arquivo);
                //Faz a leitura
                StreamReader lendoArquivoTxt = abreArquivoTxt.OpenText();
                string infoPWD = "";
                try
                {
                    string str;
                    //Verifica linha a linha do arquivo
                    while ((str = lendoArquivoTxt.ReadLine()) != null)
                    {
                        string[] linha = str.Split(';');
                        //descriptografa a linha 
                        string retorno = csEncrypt.descriptografaSenha(linha[tipoConsulta]);
                        //Verifica se existe
                        if (retorno.Equals(consultar))
                        {
                            infoPWD += csEncrypt.descriptografaSenha(linha[0]) + ";";
                        }
                    }
                    //Fecha o arquivo para que ele nao se apague
                    lendoArquivoTxt.Dispose();
                    lendoArquivoTxt.Close();
                    //Retorna as informações
                    if (infoPWD != "")
                        return infoPWD.Substring(0, infoPWD.Length - 1);//retorno 
                    else
                        return "";
                }
                catch
                {
                    //Fecha o arquivo
                    lendoArquivoTxt.Dispose();
                    lendoArquivoTxt.Close();
                    return "2";//retorno 2: erro
                }
            }
            return "";
        }
        //******************************************************************************/
        // public: adicionaTxtREPCad                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 18/05/2011
        // Descrição : Adiciona novo REP ao REPCad
        // Retorno: byte
        //
        //******************************************************************************         
        public byte adicionaTxtREPCad(string arquivo, string ip, string mask, string nome)
        {
            //Abre o arquivo para escrita
            //StreamWriter salvandoArquivoTxt = new StreamWriter(arquivo);
            TextWriter salvandoArquivoTxt = File.AppendText(arquivo);
            try
            {
                //Se for marcação de ponto adiciona ao txt novo
                salvandoArquivoTxt.WriteLine(ip + ";" + mask + ";" + nome );
                //Fecha o arquivo para salvar
                // salvandoArquivoTxt.Flush();
                salvandoArquivoTxt.Close();
                return 0; //retorno 0 cadastrado sem erro
            }
            catch
            {
                salvandoArquivoTxt.Close();
                return 1;//retorno 1 erro
            }
        }
        //******************************************************************************/
        // public: consultaTxtPwd                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 18/05/2011
        // Descrição : consulta no arquivo REPCad se existe o REP
        // Retorno: string
        //
        //******************************************************************************         
        public string consultaTxtREPCad(string arquivo, string consultar)
        {
            //Le o arquivo 
            FileInfo abreArquivoTxt = new FileInfo(arquivo);
            StreamReader lendoArquivoTxt = abreArquivoTxt.OpenText();

            // setor = csEncrypt.criptografaSenha(setor);
            try
            {
                string str;

                //Separa o que será consultado em colunas removendo o separador
                string[] ListaRep = consultar.Split(';');
                //Recebe o que será consultado para poder fazer a comparação
                string retorno = ListaRep[0];
                //Le linha a linha do arquivo
                while ((str = lendoArquivoTxt.ReadLine()) != null)
                {
                    //Separa a linha em colunas removendo o separador
                    string[] linha = str.Split(';');
                    //Verifica se ja existe
                    if ((linha[0].Equals(retorno)) /*& (linha[3].Equals(setor))*/)
                    {
                        lendoArquivoTxt.Dispose();
                        lendoArquivoTxt.Close();
                        return str; //retorno str: existe   
                    }
                }
                lendoArquivoTxt.Dispose();
                lendoArquivoTxt.Close();

                return "0";//retorno 0: não existe o registro                
            }
            catch
            {
                lendoArquivoTxt.Dispose();
                lendoArquivoTxt.Close();
                return "2";//retorno 2: erro
            }
        }
    }
}