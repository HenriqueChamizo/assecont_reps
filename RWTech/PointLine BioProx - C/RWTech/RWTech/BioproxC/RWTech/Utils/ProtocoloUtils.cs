using System;

namespace BioproxC.RWTech.Utils
{
    public class ProtocoloUtils
    {
        /**
	     * Responsável por calcular o Checksum e o tamanho do pacote
	     * 
	     * @param dados
	     *            Array de byte sem o checksum
	     * @return Array de byte com adiÃ§Ã£o do checksum.
	     */
        public static byte[] calcularChecksum(byte[] dados)
        {
            byte cs = 0;
            for (int i = 0; i < dados.Length; i++)
            {
                // XOR
                cs ^= dados[i];
            }
            int l = dados.Length;
            byte[][] bytes = new byte[][] { dados, new byte[] { cs } };
            dados = merge(bytes);
            return dados;
        }

        /**
         * Responsável por calcular o Checksum
         * 
         * @param dados
         *            Array de byte sem o checksum
         * @return Byte referente ao checksum.
         */
        public static byte getChecksum(byte[] dados)
        {
            byte cs = 0;
            for (int i = 0; i < dados.Length; i++)
            {
                // XOR
                cs ^= dados[i];
            }
            return cs;
        }

        /**
         * Responsável em unir n arrays em um.
         * 
         * @param arrays
         *            Quantidade de array separados por ','
         * @return Array com a união dos parâmetros.
         */
        public static byte[] merge(byte[][] arrays)
        {

            int count = 0;
            for (int x = 0; x < arrays.Length; x++)
            {
                count += arrays[x].Length;
            }

            byte[] mergedArray = new byte[count];
            int start = 0;

            for (int x = 0; x < arrays.Length; x++)
            {
                Array.Copy(arrays[x], 0, mergedArray, start, arrays[x].Length);
                start += arrays[x].Length;
            }
            return mergedArray;
        }

        /**
         * Responsável por obter a data do sistema e transformar os dados em um array de byte.
         * 
         * @return Array de byte com a data do sistema.
         */
        public static byte[] data()
        {
            byte[] data = new byte[4];
            DateTime dt = DateTime.Now;

            data[0] = 0;

            //data[1] = (byte)Integer.parseInt(Integer.toBinaryString(dt.getDayOfMonth()), 2);
            int dias = DateTime.DaysInMonth(dt.Year, dt.Month);
            string sbi = Convert.ToString(dias, 2);
            int ibi = Convert.ToInt32(sbi, 2);
            data[1] = (byte)ibi;
            //data[1] = (byte)Convert.ToInt32(Convert.ToString(DateTime.DaysInMonth(dt.Year, dt.Month), 2), 2);

            string mes = dt.ToString("MM");
            int imes = Convert.ToInt32(mes);
            String bitsMes = Convert.ToString(imes, 2);
            while (bitsMes.Length < 4)
            {
                bitsMes = "0" + bitsMes;
            }
            String bitsAnoFull = Convert.ToString(dt.Year, 2);
            while (bitsAnoFull.Length < 4)
            {
                bitsAnoFull = "0" + bitsAnoFull;
            }
            String bitsAnoAux = bitsAnoFull.Substring(bitsAnoFull.Length - 8, bitsAnoFull.Length - 8);
            bitsAnoFull = bitsAnoFull.Substring(0, bitsAnoFull.Length - bitsAnoAux.Length);
            while (bitsAnoFull.Length < 4)
            {
                bitsAnoFull = "0" + bitsAnoFull;
            }
            data[2] = (byte)Convert.ToInt32(bitsMes + bitsAnoFull, 2);

            data[3] = (byte)Convert.ToInt32(bitsAnoAux, 2);
            return data;
        }

        /**
         * Responsável por obter o horário do sistema e transformar os dados em um array de byte.
         * 
         * @return Array de byte com o horário do sistema.
         */
        public static byte[] horario()
        {
            int[] hora = new int[4];

            DateTime dt = DateTime.Now;
            hora[0] = 0;
            hora[1] = dt.Second;
            hora[2] = dt.Minute;
            hora[3] = dt.Hour;

            return Conversor.intArrayToByteArray(hora);
        }
    }
}
