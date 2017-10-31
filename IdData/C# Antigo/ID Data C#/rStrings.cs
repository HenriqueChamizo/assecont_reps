using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdData
{
    public class rStrings
    {
        public static string CryptString(string Palavra, int Add)
        {
            int[] Adds = new int[20] { 2, 3, 1, 2, 1, 2, 0, 2, 1, 3, 4, 1, 2, 3, 0, 2, 2, 2, 3, 3 };
            string ret;
            int Pos;
            char C;
            int CC;

            if (Palavra == "")
            {
                return "";
            }

            if (Add > 0) Palavra = Palavra + "LOKI";

            Pos = 0;
            ret = "";

            for (int i = 0; i <= Palavra.Length - 1; i++)
            {
                C = Palavra[i];
                CC = Asc(Palavra[i]);
                CC += Adds[Pos] * Add;
                C = Chara(CC);
                ret += C;
                Pos++;
                if (Pos == Adds.Length) Pos = 0;
            }

            string s = ret.Substring(ret.Length - 4, 4);

            if ((Add < 0) && (ret.Substring(ret.Length - 4, 4) == "LOKI"))
                ret = ret.Remove(ret.Length - 4, 4).Trim();

            return ret;
        }

        public static int Asc(char Letra)
        {
            string Alfa = @" !" + "\"" + @"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyzáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙãõÃÕäëïöüÄËÏÖÜâêîôûÂÊÎÔÛ'";

            return 31 + Alfa.IndexOf(Letra) + 1;
        }

        public static char Chara(int Letra)
        {
            string Alfa = @" !" + "\"" + @"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\]^_`abcdefghijklmnopqrstuvwxyzáéíóúÁÉÍÓÚàèìòùÀÈÌÒÙãõÃÕäëïöüÄËÏÖÜâêîôûÂÊÎÔÛ'";
            string C;
            C = Alfa.Substring(Letra - 32, 1);
            return Convert.ToChar(C);
        }

        public static string FormataCnpj(string Cnpj)
        {
            string res = "";

            for (int i = 0; i <= Cnpj.Length - 1; i++)
            {
                if (
                    (Cnpj[i] != Convert.ToChar(".")) &&
                    (Cnpj[i] != Convert.ToChar("/")) &&
                    (Cnpj[i] != Convert.ToChar("-")) &&
                    (Cnpj[i] != Convert.ToChar(" "))
                    )
                    res = res + Cnpj[i];
            }

            return res;
        }

        public static string FormatString(string p_strValue, string p_strMask)
        {
            string p_strValueFormated = "";

            StringBuilder strbValue = new StringBuilder();
            // remove caracteres nao numericos
            foreach (char c in p_strValue)
            {
                if (Char.IsNumber(c))
                    strbValue.Append(c);
            }

            int idxMask = p_strMask.Length;
            int idxValue = strbValue.Length;

            for (; idxValue > 0 && idxMask > 0; )
            {
                if (p_strMask[--idxMask] == '#')
                {
                    idxValue--;
                }
            }

            StringBuilder strbReturn = new StringBuilder();
            for (; idxMask < p_strMask.Length; idxMask++)
            {
                strbReturn.Append((p_strMask[idxMask] == '#') ? strbValue[idxValue++] : p_strMask[idxMask]);
            }
            p_strValueFormated = strbReturn.ToString();

            return p_strValueFormated;
        }

    }
}
