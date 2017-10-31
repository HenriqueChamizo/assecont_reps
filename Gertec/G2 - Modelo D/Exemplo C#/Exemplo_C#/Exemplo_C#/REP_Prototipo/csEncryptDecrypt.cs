/*****************************************************************************
* Aplicativo : 	???????	                                     *
* Autor	     : Ana Lúcia S. Melo (para esta classe)    	     			     *
* Referência : Programa para coleta do REP                                   *
* Data       : 18/05/11						                                 *
* Ambiente   : Visual C# 2008                    						     *
* Fonte      : csEncryptDecrypt                   		                     *
* Objetivo   : Responsável pela criptografia da senha   .                    *
******************************************************************************/
using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace REP_Prototipo
{
    public class csEncryptDecrypt
    {       
        
        //******************************************************************************/
        // public: criptografaSenha                                                    *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/05/2011
        // Descrição : Criptografa a informação que é enviada
        // Retorno: string
        //
        //******************************************************************************     
        public string criptografaSenha(string senhaCripto )
        {
            try
            {
                //Criptografia Triple DES
                TripleDESCryptoServiceProvider objcriptografaSenha = new TripleDESCryptoServiceProvider();
                //MD5
                MD5CryptoServiceProvider objcriptoMd5 = new MD5CryptoServiceProvider();

                byte[] byteHash, byteBuff;
                //carrega a chave
                string strTempKey = "116";
                //Calcula o valor de hash utilizando como chave o valor 116
                byteHash = objcriptoMd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objcriptoMd5 = null;
                //adiciona a chave ao objeto que será codificado
                objcriptografaSenha.Key = byteHash;
                //Codifica cada bloco
                objcriptografaSenha.Mode = CipherMode.ECB;
                //Passa a senha para byte
                byteBuff = ASCIIEncoding.ASCII.GetBytes(senhaCripto);
                //Retorna a senha criptografada
                return Convert.ToBase64String(objcriptografaSenha.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return "Digite os valores Corretamente." + ex.Message;
            }
        }

        //******************************************************************************/
        // public: descriptografaSenha                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/05/2011
        // Descrição : Decodifica a informação que é enviada
        // Retorno: string
        //
        //******************************************************************************     
        public string descriptografaSenha(string strCriptografada)
        {
            try
            {              
                //Objeto Triple DES
                TripleDESCryptoServiceProvider objdescriptografaSenha = new TripleDESCryptoServiceProvider();
                //Objeto MD5
                MD5CryptoServiceProvider objcriptoMd5 = new MD5CryptoServiceProvider();

                byte[] byteHash, byteBuff;
                string strTempKey = "116"; //Chave
                //Calcula o valor de hash utilizando como chave o valor 116.
                byteHash = objcriptoMd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
                objcriptoMd5 = null;
                //adiciona a chave ao objeto que será decodificado
                objdescriptografaSenha.Key = byteHash;
                objdescriptografaSenha.Mode = CipherMode.ECB;
                //Converte a palavra que será decodificada
                byteBuff = Convert.FromBase64String(strCriptografada);
                //Converte para string
                string strDecrypted = ASCIIEncoding.ASCII.GetString(objdescriptografaSenha.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
                objdescriptografaSenha = null;
                //Retorna decodificado
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Digite os valores corretamente." + ex.Message;
            }
        }

    }
}
