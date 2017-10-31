using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RepProtocolTestSuite
{
    class PisUtils
    {

		public static char GetPisDv(string pis)
		{
			int[] multiplicador = new int[10] { 3,2,9,8,7,6,5,4,3,2 };
			int soma;
			int resto;

            pis = Regex.Replace(pis, "[^0-9]", "").PadLeft(10, '0');
            if (pis.Length != 10)
            {
                return 'X';
            }
			soma = 0;
			for (int i = 0; i < 10; i++)
				soma += int.Parse(pis[i].ToString()) * multiplicador[i];

			resto = soma % 11;

			if ( resto < 2 )
				resto = 0;
			else
				resto = 11 - resto;

            return resto.ToString()[0];
		}

        public static bool CheckPisDv(string pis)
        {
            pis = Regex.Replace(pis, "[^0-9]", "").PadLeft(11, '0');
            if (pis.Length != 11)
            {
                return false;
            }
            string pisPart = pis.Substring(0, 10);
            char dv = GetPisDv(pisPart);
            return pis == pisPart + dv;
        }

        public static string RndPis()
        {
            //                                 9 digitos   9 digitos             +           1 digito
            //                                |---------| |---------|           
            string pisPart = new Random().Next(100000000, 1000000000).ToString() + new Random().Next(10).ToString();
            return pisPart + GetPisDv(pisPart);
        }
    }
}
