/*****************************************************************************
* Aplicativo : 	???????	                                     *
* Autor	     : Ana Lúcia S. Melo (para esta classe)    	     			     *
* Referência : Programa para coleta do REP                                   *
* Data       : 17/05/11						                                 *
* Ambiente   : Visual C# 2008                    						     *
* Fonte      : TrataPwd                		  	                             *
* Objetivo   : Responsável pelo tratamento do arquivo PWD                    *
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace REP_Prototipo
{
    class TrataPwd
    {
        public txtFiles txtfiles = new txtFiles();
        csEncryptDecrypt criptografa = new csEncryptDecrypt();

        
        //******************************************************************************/
        // public: adicionaUsuario                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 17/05/2011
        // Descrição : Adiciona um novo usuário ao PWD
        // Retorno: byte
        //
        //******************************************************************************         
        public byte adicionaUsuario(string user, string pass, string permissao, string setor)
        {
            try
            {
                //Recebe o caminho do PWD
                string path = Application.StartupPath + "\\pwd.txt";
                //Recebe informações sobre o arquivo
                //FileInfo arquivoTxt = new FileInfo(path);
                //Verifica se o usuario já existe
                if (conferUserExist(user) == 0)
                {
                    //Se nao existir adiciona ao txt
                    if (txtfiles.adicionaTxtPwd(path, criptografa.criptografaSenha(user), criptografa.criptografaSenha(pass), criptografa.criptografaSenha(permissao), criptografa.criptografaSenha(setor)) == 0)
                        return 0;
                    else return 1;
                }
                else return 1;
            }
            catch
            {
                return 2;
            }
        }

        //******************************************************************************/
        // public: consultaLogin                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 17/05/2011
        // Descrição : Consulta o login
        // Retorno: byte
        //
        //******************************************************************************         
        public byte consultaLogin(string path, string user, string setor)
        {
            //string path = Application.StartupPath + "\\pwd.txt";
            string[] informacao = new string[4];
            //Consulta o usuário
            string[] infoUser = txtfiles.consultaTxtPwd(path, user).Split(';');
            //Descriptografa 
            for (int i = 0; i <= infoUser.Length - 1; i++)
                informacao[i] = criptografa.descriptografaSenha(infoUser[i]);
            //Verifica se o login é de administrador
            if ((informacao[2] == "Administrador") & (informacao[3] == setor))
                return 0;
            //Verifica se o login é de Usuario
            else if ((informacao[2] == "Usuario") & (informacao[3] == setor))
                return 1;
            else return 2;
        }

        //******************************************************************************/
        // public: alteraPermissao                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 17/05/2011
        // Descrição : Alteração da permissão do login indicado
        // Retorno: byte
        //
        //******************************************************************************         
        public byte alteraPermissao(string path, string user, string permissao, string setor)
        {
            string arg = criptografa.criptografaSenha(user);//Criptografa o login
            string arg2 = criptografa.criptografaSenha(permissao);//criptografa a permissao
            setor = criptografa.criptografaSenha(setor);//criptografa o setor
            try
            {
                //Le e abre o arquivo para leitura e escrita
                FileStream srcFile = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(srcFile);
                FileStream trgFile = new FileStream(path + ".tmp", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(trgFile);
                string line = "";
                //Le linha a linha
                while ((line = sr.ReadLine()) != null)
                {
                    //Separa a linha em coluna
                    string[] auxLine = line.Split(';');
                    //Verifica se o login e o setor são iguais aos solicitados, caso seja, adiciona os 
                    //dados a variavel line
                    if ((auxLine[0] == arg) & (auxLine[3] == setor))
                        line = auxLine[0] + ";" + auxLine[1] + ";" + arg2 + ";" + auxLine[3];
                    //Escreve a linha no txt
                    sw.WriteLine(line);

                }
                //Fecha o arquivo TXT para que salve as informações adicionadas a ele
                sw.Flush();
                sw.Close();
                sr.Close();
                srcFile.Close();
                trgFile.Close();
                //Apaga o arquivo temporário criado
                File.Delete(path);
                File.Move(path + ".tmp", path);
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        //******************************************************************************/
        // public: alteraLogin                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 17/05/2011
        // Descrição : Altera os dados do login
        // Retorno: byte
        //
        //******************************************************************************         
        public byte alteraLogin(string path, string user, string pass, string newUser, string newPass, string setor)
        {
            try
            {
                //Confere o login e senha
                if (conferUserPass(user, pass) == 0)
                {
                    //Abre o txt para leitura 
                    FileStream srcFile = new FileStream(path, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(srcFile);
                    //Abre outro para gravação
                    FileStream trgFile = new FileStream(path + ".tmp", FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(trgFile);
                    //Criptografa os dados para consulta 
                    user = criptografa.criptografaSenha(user);
                    pass = criptografa.criptografaSenha(pass);
                    newUser = criptografa.criptografaSenha(newUser);
                    newPass = criptografa.criptografaSenha(newPass);
                    setor = criptografa.criptografaSenha(setor);                   
                    string line = "";
                    //Lê o TXT linha a linha
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Separa a linha em coluna
                        string[] auxLine = line.Split(';');
                        //Verifica se as informações conferem com as solicitadas
                        if ((auxLine[0] == user) & ((auxLine[3] == setor)))
                        {
                            //Adiciona a informação com novo login e senha na variavel line
                            line = newUser + ";" + newPass + ";" + auxLine[2] + ";" + auxLine[3];
                            //Escreve no txt
                            sw.WriteLine(line);
                            //Fecha o txt para que salve as informações
                            sw.Flush();
                            sw.Close();
                            sr.Close();
                            srcFile.Close();
                            trgFile.Close();
                            //Apaga o arquivo temporário
                            File.Delete(path);
                            File.Move(path + ".tmp", path);
                            //Retorna 0 - adicionado
                            return 0;
                        }
                        
                    }
                    //Para o caso de nao encontrar retorna 2
                    sw.Flush();
                    sw.Close();
                    sr.Close();
                    srcFile.Close();
                    trgFile.Close();

                    File.Delete(path);
                    File.Move(path + ".tmp", path);
                    return 2;
                }
                else
                    //Senha e/ou login incorretos
                    return 1;
            }
            catch
            {
                return 2;
            }
        }


        //******************************************************************************/
        // public: excluirUsuario                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 17/05/2011
        // Descrição : Remove o usuario 
        // Retorno: byte
        //
        //******************************************************************************         
        public byte exluirUsuario(string path, string user, string setor)
        {
            try
            {
                //Verifica se o login existe e criptografa ele
                string arg = criptografa.criptografaSenha(txtfiles.consultaNoPwd(path, user, 0));
                setor = criptografa.criptografaSenha(setor);//Criptografa o setor
                int i = 0;
                //Lê as linhas do arquivo
                foreach (string line in File.ReadAllLines(path))
                {
                    //Separa em conluna
                    string[] linha = line.Split(';');
                    //Verifica se as linha lida contem as informações solicitadas de logine  setor
                    if ((linha[0] == arg) & (linha[3] == setor))
                    {
                        //Remove a linha
                        var file = new List<string>(System.IO.File.ReadAllLines(path));
                        file.RemoveAt(i);
                        File.WriteAllLines(path, file.ToArray());
                        return 0;//Retorna removido
                    }
                    i++;
                }
                //Nao removeu
                return 1;
            }
            catch
            {
                //ERRO
                return 2;
            }
        }

        //******************************************************************************/
        // public: conferUserExist                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 17/05/2011
        // Descrição : Confere se o usuario existe
        // Retorno: byte
        //
        //******************************************************************************         
        public byte conferUserExist(string user)//, string setor)
        {
            try
            {
                string path = Application.StartupPath + "\\pwd.txt";
                //Verifica se o arquivo de usuarios existe
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    return 0;
                }
                //Verifica se o usuario já existe
                if (txtfiles.consultaTxtPwd(path, user/*, setor */) == "0")
                    return 0; //Nao existe
                else return 1; //Já existe
            }
            catch
            {
                return 2; //Em caso de erro retorna 2
            }
        }


        //******************************************************************************/
        // public: consferUserPass                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 17/05/2011
        // Descrição : Confere o login e senha
        // Retorno: byte
        //
        //******************************************************************************         
        public byte conferUserPass(string user, string pass/*, string setor*/)
        {
            try
            {
                string path = Application.StartupPath + "\\pwd.txt";
                //Verifica se o arquivo de usuarios existe
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    return 0;
                }
                //Verifica se o usuario já existe
                string resultado = txtfiles.consultaTxtPwd(path, user);
                if (resultado == "0")
                    return 1; //Nao existe
                else
                {
                    string[] info = resultado.Split(';');
                    if (info[1] == criptografa.criptografaSenha(pass)) return 0;
                    else return 1;
                }
            }
            catch
            {
                return 2; //Em caso de erro retorna 2
            }
        }

        //******************************************************************************/
        // public: conectaUserPass                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 17/05/2011
        // Descrição : Verifica o tipo de login e conecta ao programa
        // Retorno: string
        //
        //******************************************************************************         
        public string conectaUserPass(string user, string pass)
        {
            try
            {
                string path = Application.StartupPath + "\\pwd.txt";
                string MasterUser = "", MasterPass = "";
                byte usernotfound = 0;
                //Verifica se o arquivo de usuarios existe
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                    MasterUser = "masteruser";
                    MasterPass = "masterpass";
                    usernotfound = 1;
                }
                //Verifica se o usuario já existe
                else if (txtfiles.consultaTxtPwd(path, user) == "0")
                {
                    //Nao existe
                    MasterUser = "masteruser";
                    MasterPass = "masterpass";
                    usernotfound = 1;
                }

                if (usernotfound == 0)
                {
                    //Verifica se a senha está correta
                    //Le o arquivo 
                    FileInfo abreArquivoTxt = new FileInfo(path);
                    StreamReader lendoArquivoTxt = abreArquivoTxt.OpenText();
                    try
                    {
                        string str;
                        while ((str = lendoArquivoTxt.ReadLine()) != null)
                        {
                            //Separa em colunas as informações do txt
                            string[] linha = str.Split(';');
                            //Separa as informações do user em colunas
                            string[] cripto = user.Split(';');
                            //criptografa o login
                            string retorno = criptografa.criptografaSenha(cripto[0]);
                            //Verifica se ja existe
                            if (linha[0].Equals(retorno))
                            {
                                //criptografa a senha  para poder ver se confere com a do txt
                                retorno = criptografa.criptografaSenha(pass);
                                if (retorno == linha[1])
                                {
                                    //retorna as informações do login
                                    retorno = user + ";" + pass + ";" + criptografa.descriptografaSenha(linha[2]) + ";" + criptografa.descriptografaSenha(linha[3]);
                                    lendoArquivoTxt.Dispose();
                                    lendoArquivoTxt.Close();

                                    return retorno; //retorno str: existe   
                                }
                                else
                                {
                                    //Senha incorreta
                                    lendoArquivoTxt.Dispose();
                                    lendoArquivoTxt.Close();

                                    return "1;x;x;x";
                                }
                            }
                        }
                        //nao há o login que procura
                        return "1;x;x;x";
                    }

                    catch
                    {
                        //ERRO
                        lendoArquivoTxt.Dispose();
                        lendoArquivoTxt.Close();
                        return "2;x;x;x";//retorno 2: erro
                    }
                }
                else
                {
                    //Caso a senha seja a padrão retorna as informações de administrador master
                    if ((user == MasterUser) & (pass == MasterPass))
                    {
                        string retorno = (user + ";" + pass + ";Administrador" + ";master");
                        return retorno;
                    }
                    else
                        return "1;x;x;x";
                }
            }
            catch
            {
                return "2;x;x;x"; //Em caso de erro retorna 2
            }
        }

    }
}