using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Rw1510
{
    public class Authotelcom
    {
        [DllImport("Authotelcom.dll")]
        public static extern unsafe char configura(string tipo, string ip, string host, int porta, char end_dev, string datahorai, string datahoraf, string diasSem,
            string diasSemF, string info, int com, int total, int atual, int duracaoToque, int flag, int config, int qtde_rel, int baud);
    }
}
