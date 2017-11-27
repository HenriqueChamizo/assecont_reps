using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioproxC.RWTech.ProtocoloREP.Comandos
{
    public class CodigosComandos
    {
        public static int START_PC = 0x0A;
        public static int START_DISPOSITIVO = 0x3D;
        public static int END = 0x40;
        public static int ENVIAR_DATA_HORA = 0x91;
        public static int ENVIAR_HORARIO_VERAO = 0x93;
        public static int LER_HORARIO_VERAO = 0x94;
        public static int LER_DATA_HORA = 0x92;
        public static int LER_EMPREGADOR = 0xAB;
        public static int ENVIAR_EMPREGADOR = 0xBC;
        public static int ENVIAR_FUNCIONARIO = 0xBE;
        public static int LER_FUNCIONARIO = 0xBD;
        public static int ENVIAR_DIGITAL = 0xBF;
        public static int LER_DIGITAL = 0xAD;
        public static int LER_MARCACAO = 0xF0;
        public static int ENVIAR_QTD_PAPEL = 0xAF;
        public static int LER_QTD_PAPEL = 0xB0;
        public static int ENVIAR_QTD_POUCO_PAPEL = 0xB1;
        public static int LER_QTD_POUCO_PAPEL = 0xB2;
        public static int VERIFICAR_PAPEL_ENROSCADO = 0xFE;

    }
}
