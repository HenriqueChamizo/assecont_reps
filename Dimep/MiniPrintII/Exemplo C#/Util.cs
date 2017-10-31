using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExemploREP
{
    class Util
    {
        public static string RetiraFormatacao(string parTexto)
        {
            string strTexto = null;            
            
            strTexto = parTexto.Replace(".", "");
            strTexto = strTexto.Replace("-", "");
            strTexto = strTexto.Replace("_", "");
            strTexto = strTexto.Replace("/", "");
            strTexto = strTexto.Replace(",", "");

            return strTexto;
        }        
    }
}
