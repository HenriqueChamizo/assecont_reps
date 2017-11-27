using BioproxC.RWTech.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioproxC.RWTech.Common
{
    public class LogManager
    {
        private static LogManager instance;
        public bool ocultarBytesCriptografados;
        public bool ocultarBytesDescriptografados;

        public static LogManager getInstance()
        {
            if (instance == null)
            {
                instance = new LogManager();
            }
            return instance;
        }

        public List<String> bytesEnviados = new List<String>();
        public String getNextBytesEnviados()
        {
            if (bytesEnviados != null)
                return bytesEnviados.Last();
            return null;
        }

        public List<String> bytesRecebidos = new List<String>();
        public String getBytesRecebidos()
        {
            if (bytesRecebidos != null)
                return bytesRecebidos.Last();
            return null;
        }

        public List<String> resultados = new List<String>();
        public String getResultado()
        {
            if (resultados != null)
                return resultados.Last();
            return null;
        }

        public static String bytesToStringHex(String tituloMsg, byte[] bytes)
        {
            StringBuilder str = new StringBuilder();
            for (int j = 0; j < bytes.Length; j++)
            {
                String hex = Convert.ToString(Conversor.byteToInt(bytes[j]), 16);
                if (hex.Length < 2)
                {
                    hex = "0" + hex;
                }
                str.Append(hex.ToUpper() + " ");
            }
            return tituloMsg + ": " + str.ToString();
        }
    }
}
