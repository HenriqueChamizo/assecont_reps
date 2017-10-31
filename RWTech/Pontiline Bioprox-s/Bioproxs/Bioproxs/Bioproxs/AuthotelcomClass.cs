using System; using System.Collections.Generic; using System.Linq; using System.Runtime.InteropServices;
using System.Text;  namespace Bioproxs {     public enum TModelo     {         POINTLineCard = 1,         POINTLineBIOProx = 2,         POINTLineDuoCard = 3,          POINTLineDuoCardBio = 4,          POINTLineProxS = 5,          POINTLineBioProxS = 6,          POINTLineBioProxBS = 7,          POINTLineBioMBC = 8,          POINTLineBioProxBC = 9,          POINTLineBioProxC = 10     }      public class MtdConfigura
    {
        public string tipo;
        public string ip;
        public string host = "";
        public string porta;
        public string end_dev;
        public string datahorai;
        public string datahoraf = "";
        public string diassem = "";
        public string diassemf = "";
        public string inf = "";
    } } 