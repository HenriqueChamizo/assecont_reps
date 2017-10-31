/*****************************************************************************
* Aplicativo : 	???????	                                                     *
* Autor	     : Ana Lúcia S. Melo (para esta classe)    	     			     *
* Referência : Programa para coleta do REP                                   *
* Data       : 07/07/2011						                                 *
* Ambiente   : Visual C# 2008                    						     *
* Fonte      : clTrataREP                     		  	                     *
* Objetivo   : Tratamento do REP, verifica se usuario ou REP existe para po- *
*              der iniciar o programa                                        *
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace REP_Prototipo
{
    class clTrataREP
    {
        public txtFiles txtfiles = new txtFiles();

        //******************************************************************************/
        // public: adicionaREP                                                         *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 07/07/2011
        // Descrição : Adiciona um novo REP ao REPCad
        // Retorno: byte
        //
        //******************************************************************************         
        public byte adicionaREP(string ip, string mask, string nome)
        {
            try
            {
                //Recebe o caminho do PWD
                string path = Application.StartupPath + "\\REPCad.txt";

                //Recebe informações sobre o arquivo
                //FileInfo arquivoTxt = new FileInfo(path);
                //Verifica se o usuario já existe
                if (conferREPExist(ip) == 0)
                    //Se nao existir adiciona ao txt
                    if (txtfiles.adicionaTxtREPCad(path, ip, mask, nome) == 0)
                        return 0;
                    else return 1;

                else return 1;
            }
            catch
            {
                return 2;
            }
        }
        //******************************************************************************/
        // public: conferUserExist                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 07/07/2011
        // Descrição : Confere se o usuario existe
        // Retorno: byte
        //
        //******************************************************************************         
        public byte conferREPExist(string ip)//, string setor)
        {
            try
            {
                string path = Application.StartupPath + "\\REPCad.txt";
                //Verifica se o arquivo de usuarios existe
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    return 0;
                }
                //Verifica se o REP já existe
                if (txtfiles.consultaTxtREPCad(path, ip) == "0")
                    return 0; //Nao existe
                else return 1; //Já existe
            }
            catch
            {
                return 2; //Em caso de erro retorna 2
            }
        }

        //******************************************************************************/
        // public: ConsultaREPCadastrado                                               *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 07/07/2011
        // Descrição : Alteração da permissão do login indicado
        // Retorno: byte
        //
        //******************************************************************************         
        public string ConsultaREPCadastrado(string path, int nInfo)
        {
            try
            {
                string retornoREP = "";
                //Le e abre o arquivo para leitura e escrita
                FileStream srcFile = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(srcFile);
                // FileStream trgFile = new FileStream(path + ".tmp", FileMode.Create, FileAccess.Write);
                //  StreamWriter sw = new StreamWriter(trgFile);
                string line = "";
                //Le linha a linha
                while ((line = sr.ReadLine()) != null)
                {
                    //Separa a linha em coluna
                    string[] auxLine = line.Split(';');
                    retornoREP += auxLine[nInfo] + ";";
                }
                //Fecha o arquivo TXT             
                sr.Close();
                srcFile.Close();

                return retornoREP;
            }
            catch
            {
                return "";
            }
        }
        //08/07/2011
        public string consultaTxt(string path, int local, string busca, int retorna)
        {
            try
            {
                //Le e abre o arquivo para leitura e escrita
                FileStream srcFile = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(srcFile);
                string line = "";
                //Le linha a linha
                while ((line = sr.ReadLine()) != null)
                {
                    //Separa a linha em coluna
                    string[] auxLine = line.Split(';');
                    if (auxLine[local] == busca)
                        return auxLine[retorna];
                }
                //Fecha o arquivo TXT             
                sr.Close();
                srcFile.Close();

                return "";
            }
            catch
            {
                return "";
            }

        }
    }
}
