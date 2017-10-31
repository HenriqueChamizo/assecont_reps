/*****************************************************************************
* Aplicativo : 	???????	                                     *
* Autor	     : Ana Lúcia S. Melo (para esta classe)    	     			     *
* Referência : Programa para coleta do REP                                   *
* Data       : 27/04/11						                                 *
* Ambiente   : Visual C# 2008                    						     *
* Fonte      : Validacao                   		  	                         *
* Objetivo   : Responsável por verificar se os dados informados são válidos. *
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace REP_Prototipo
{
    class Validacao
    {
        //******************************************************************************/
        // public: ValidaIp                                                            *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 27/04/2011
        // Descrição : Valida o IP
        // Retorno: bool
        //
        //******************************************************************************       
        public static bool ValidaIp(string addr)
        {
            //Ctrl+c.... Ctrl+v =)
            //create our match pattern
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //create our regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
            bool valid = false;
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            return valid;
        }

        //******************************************************************************/
        // public: ValidaCPF                                                           *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 28/04/2011
        // Descrição : Valida o CPF informado
        // Retorno: bool
        //
        //******************************************************************************            
        public static bool ValidaCPF(string vrCPF)
        {
            //retira a pontuação
            string valor = vrCPF.Replace(".", "");
            valor = valor.Replace("-", "");
            //Se a quantidade de digitos for diferente de 11 já está incorreto
            if (valor.Length != 11)
                return false;

            //depois comentar o resto!!!!!!!!!!!!!!!

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                    return false;
            return true;
        }


        //******************************************************************************/
        // public: ValidaCNPJ                                                          *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 28/04/2011
        // Descrição : Valida o CNPJ informado
        // Retorno: bool
        //
        //******************************************************************************            
        public static bool ValidaCNPJ(string vrCNPJ)
        {
            //Retirando a pontuação para utilizar somente os numeros
            string CNPJ = vrCNPJ.Replace(".", "");
            CNPJ = CNPJ.Replace("/", "");
            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;
            int nrDig;
            string ftmt = "6543298765432";
            bool[] CNPJOk = new bool[2];

            digitos = new int[14];
            soma = new int[2];
            soma[0] = 0;
            soma[1] = 0;
            resultado = new int[2];
            resultado[0] = 0;
            resultado[1] = 0;
            CNPJOk[0] = false;
            CNPJOk[1] = false;

            try
            {
                for (nrDig = 0; nrDig < 14; nrDig++)
                {
                    digitos[nrDig] = int.Parse(CNPJ.Substring(nrDig, 1));
                    if (nrDig <= 11)
                        soma[0] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig + 1, 1)));
                    if (nrDig <= 12)
                        soma[1] += (digitos[nrDig] * int.Parse(ftmt.Substring(nrDig, 1)));
                }
                for (nrDig = 0; nrDig < 2; nrDig++)
                {
                    resultado[nrDig] = (soma[nrDig] % 11);
                    if ((resultado[nrDig] == 0) || (resultado[nrDig] == 1))
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == 0);
                    else
                        CNPJOk[nrDig] = (digitos[12 + nrDig] == (11 - resultado[nrDig]));
                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }


        //******************************************************************************/
        // public: ValidaPis                                                           *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 28/04/2011
        // Descrição : Valida o PIS informado
        // Retorno: bool
        //
        //******************************************************************************            
        public static bool ValidaPis(string pis)
        {
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;

            if (pis.Trim().Length == 0)
                return false;

            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = (11 - resto);

            return pis.EndsWith(resto.ToString());
        }

    }
}
