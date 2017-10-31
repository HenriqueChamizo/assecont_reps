using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wr
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

    }
}
