using System;
using System.Text;
using System.Text.RegularExpressions;

namespace BioproxC.RWTech.Utils
{
    public static class Conversor
    {
        private static char[] hexArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        private static int FF_HEX = 0xFF;
        private static long PESO_1 = 256L;
        private static long PESO_2 = 65536L;
        private static long PESO_3 = 16777216L;
        private static long PESO_4 = 4294967296L;
        private static long PESO_5 = 1099511627776L;

        public static byte[] cpfToByte(String cpf)
        {
            try
            {
                return longToByteArray(Convert.ToInt64(cpf), 6);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static byte[] cnpjToByte(String cnpj)
        {
            try
            {
                return longToByteArray(Convert.ToInt64(cnpj), 6);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static byte[] ceiToByte(String cei)
        {
            try
            {
                if (!string.IsNullOrEmpty(cei)) {
                    cei = "0";
                    return longToByteArray(Convert.ToInt64(cei), 5);
                }
                throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static byte[] pisToByte(String pis)
        {
            try
            {
                return DecToBCDArray(Convert.ToInt64(pis));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static String BCDtoString(byte bcd)
        {
            StringBuilder sb = new StringBuilder();
            //StringBuffer sb = new StringBuffer();

            byte high = (byte)(bcd & 0xf0);
            high >>= (byte)4;
            high = (byte)(high & 0x0f);
            byte low = (byte)(bcd & 0x0f);

            sb.Append(high);
            sb.Append(low);

            return sb.ToString();
        }

        public static String BCDtoString(byte[] bcd)
        {
            StringBuilder sb = new StringBuilder();
            //StringBuffer sb = new StringBuffer();

            for (int i = 0; i < bcd.Length; i++)
            {
                sb.Append(BCDtoString(bcd[i]));
            }

            return sb.ToString();
        }

        /**
         * Mantém somente os caracteres de 0 a 9 na string.
         *
         * @param s  String a ser limpa.
         * @return   Retorna a string limpa (somente os caracteres de 0 a 9).
         */
        public static String SomenteNumeros(String s)
        {
            if (s != null)
                return Regex.Replace(s, "[^0-9]", "");
            else
                return "";
        }

        /**
         * Mantém somente os caracteres de 0 a 9 na string, preenchendo com zeros
         * à  esquerda para completar o tamanho especificado.
         *
         * @param s            String a ser limpa.
         * @param qtdeDigitos  Quantidade mínima de dígitos que o número deve possuir.
         * @return   Retorna a string limpa (somente os caracteres de 0 a 9) e com
         *   zero(s) à  esquerda (se a string for menor que o especificado).
         */
        public static String SomenteNumeros(String s, int qtdeDigitos)
        {
            String str = SomenteNumeros(s);

            while (str.Length < qtdeDigitos)
            {
                str = "0" + str; //Preenche com zeros a esquerda, se necessÃ¡rio
            }

            return str;
        }

        /**
         * Converte uma string em representação hexadecimal em uma string de bytes
         * com os valores convertidos para inteiros.
         * 
         * @param s String com a representação em hexadecimal a ser convertida.
         * @return bytes jÃ¡ convertidos.
         */
        public static byte[] hexStringToByteArray(String s)
        {
            int len = s.Length;
            byte[] data = new byte[len / 2];
            for (int i = 0; i < len; i += 2)
            {
                data[i / 2] = (byte)((Convert.ToInt32(s[i].ToString(), 16) << 4) + Convert.ToInt32(s[i + 1].ToString(), 16));
            }
            return data;
        }

        //    /**
        //     * Concatena 2 arrays de bytes (byte []) e retorna novo array de bytes com o
        //     * resultado da concatenação.
        //     * 
        //     * @param array1 Primeiro array de bytes (Ã  esquerda).
        //     * @param array2 Segundo array de bytes (Ã  direita).
        //     * 
        //     * @return Resultado da concatenação dos 2 arrays (array1 + array2).
        //     */
        //    public static byte[] concatArraysBytes(byte []array1, byte []array2) {
        //
        //        byte [] resultado = new byte [array1.length + array2.length];
        //        System.arraycopy(array1, 0, resultado, 0, array2.length);
        //        System.arraycopy(array2, 0, resultado, array1.length, array2.length);
        //        
        //        return resultado;
        //    }
        //    
        /**
         * Converte um array de bytes em uma string com a notação dos bytes em 
         * hexadecimal.
         * 
         * @param bytes Array de bytes a ser convertido para a string.
         * @return Nova string com o resultado da conversÃ£o.
         */
        public static String bytesToStringHex(byte[] bytes)
        {
            char[] hexChars = new char[bytes.Length * 2];
            int v;
            for (int j = 0; j < bytes.Length; j++)
            {
                v = bytes[j] & 0xFF;
                hexChars[j * 2] = hexArray[v >> 4];
                hexChars[j * 2 + 1] = hexArray[v & 0x0F];
            }
            return new String(hexChars);
        }

        public static byte[] longToByteArray(long value, int size)
        {
            String resultado = value.ToString();
            while (resultado.Length < size * 2)
            {
                resultado = "0" + resultado;
            }
            byte[] retorno = new byte[size];
            for (int i = 0, j = 0; i < size; i++, j += 2)
            {
                string v = resultado.Substring(j, 2);
                ulong c = Convert.ToUInt64(v, 16);
                byte b = (byte)c;
                retorno[i] = b;
            }
            return retorno;
        }

        public static DateTime strToDate(String data)
        {
            try
            {
                if (Validacao.isStringDataOK(data, 8))
                    return Convert.ToDateTime(data);
            }
            catch (Exception e)
            {
                throw e;
            }
            return new DateTime();
        }

        public static byte stringHexToByte(String strHex, int Base)
        {
            int valor = (Convert.ToInt32(strHex, Base) & FF_HEX);
            if (valor < 0)
            {
                valor = convertByteNegative(valor);
            }
            return (byte)valor;
        }

        public static String bytesIdentificadorToString(byte[] bytes)
        {
            int qtdBytes = bytes.Length;
            int[] dadosInt = new int[qtdBytes];
            for (int i = 0; i < qtdBytes; i++)
            {
                dadosInt[i] = byteToInt(bytes[i]);
            }
            return Convert.ToString(dadosInt[0] * PESO_5 + dadosInt[1] * PESO_4 + dadosInt[2] * PESO_3 + dadosInt[3] * PESO_2 + dadosInt[4] * PESO_1 + dadosInt[5]);
        }

        public static String bytesCEIToString(byte[] bytes)
        {
            int qtdBytes = bytes.Length;
            int[] dadosInt = new int[qtdBytes];
            for (int i = 0; i < qtdBytes; i++)
            {
                dadosInt[i] = byteToInt(bytes[i]);
            }
            return Convert.ToString(dadosInt[0] * PESO_4 + dadosInt[1] * PESO_3 + dadosInt[2] * PESO_2 + dadosInt[3] * PESO_1 + dadosInt[4]);
        }

        public static String bytesPISToString(byte[] bytes)
        {
            int qtdBytes = bytes.Length;
            int[] dadosInt = new int[qtdBytes];
            for (int i = 0; i < qtdBytes; i++)
            {
                dadosInt[i] = byteToInt(bytes[i]);
            }
            return Convert.ToString(dadosInt[0] * PESO_4 + dadosInt[1] * PESO_3 + dadosInt[2] * PESO_2 + dadosInt[3] * PESO_1 + dadosInt[4]);
        }

        public static String bytesIDBioToString(byte[] bytes)
        {
            int qtdBytes = bytes.Length;
            int[] dadosInt = new int[qtdBytes];
            for (int i = 0; i < qtdBytes; i++)
            {
                dadosInt[i] = byteToInt(bytes[i]);
            }
            return Convert.ToString(dadosInt[0] * PESO_3 + dadosInt[1] * PESO_2 + dadosInt[2] * PESO_1 + dadosInt[3]);
        }

        public static String bytesCodigoToString(byte[] bytes)
        {
            int qtdBytes = bytes.Length;
            int[] dadosInt = new int[qtdBytes];
            for (int i = 0; i < qtdBytes; i++)
            {
                dadosInt[i] = byteToInt(bytes[i]);
            }
            return Convert.ToString(dadosInt[0] * PESO_2 + dadosInt[1] * PESO_1 + dadosInt[2]);
        }

        public static String bytesASCIIToString(byte[] bytes)
        {
            String str = "";
            int qdeBytes;

            qdeBytes = bytes.Length;

            /* Converte e transfere o conteúdo de "bytes" para a "string" */
            int i;
            char aux;
            for (i = 0; (bytes[i] != 0) && (i <= qdeBytes); i++)
            {
                aux = (char)((int)bytes[i] & 0xFF);
                str += aux;
            }

            return str;
        }

        static public bool byteToBoolean(byte b)
        {
            return (bool)(((b != '0') && (b != 0)) ? true : false);
        }

        static public byte[] intToByteArray2(int valor)
        {
            byte[] array = new byte[2];
            array[0] = (byte)((valor >> 8) & 0xFF);
            array[1] = (byte)(valor & 0xFF);
            return array;
        }

        static public byte[] intToByteArray(int valor, int tamanhoArray)
        {
            byte[] bytes = new byte[tamanhoArray];
            bytes[0] = Convert.ToByte(valor);
            return bytes;
        }

        static public byte[] stringParaBytes(String str, int qdeBytes)
        {
            byte[] bytes = new byte[qdeBytes];
            int i;
            int strLength = str.Length;

            /* Reliza a conversão de "String" para "byte []" */
            for (i = 0; i < strLength; i++)
            {
                bytes[i] = (byte)str.ToCharArray()[i];
            }

            return bytes;
        }

        /**
	     * Transforma um array de int em um array de byte.
	     * 
	     * @param string
	     *            String a ser transformada em array
	     * @return Array de int.
	     */
        public static byte[] intArrayToByteArray(int[] array)
        {
            byte[] arrayAux = new byte[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                arrayAux[i] = (byte)array[i];
            }
            return arrayAux;
        }

        /**
	     * Transforma uma String em um array de byte.
	     * 
	     * @param string
	     *            String a ser transformada em array
	     * @return Array de byte.
	     */
        public static byte[] stringToByteArray(String str, int size)
        {
            byte[] retorno = new byte[size];
            for (int i = 0; i < str.Length; i++)
            {
                retorno[i] = (byte)str.ToCharArray()[i];
            }
            return retorno;
        }

        /**
	     * Transforma um vetor de byte em um valor inteiro
	     * 
	     * @param array
	     *            Vetor de byte
	     * @return Valor inteiro
	     */
        public static int ByteArrayToint(byte[] array)
        {
            if (array.Length == 4)
                return array[0] << 24 | (array[1] & 0xff) << 16 | (array[2] & 0xff) << 8 | (array[3] & 0xff);
            else if (array.Length == 2)
                return 0x00 << 24 | 0x00 << 16 | (array[0] & 0xff) << 8 | (array[1] & 0xff);

            return 0;
        }

        /**
	     * Transforma um byte em um valor inteiro
	     * 
	     * @param valor
	     *            byte
	     * @return Valor inteiro
	     */
        public static int byteToInt(byte valor)
        {
            return (int)valor & 0xFF;
        }

        public static int convertByteNegative(int valor)
        {
            return ((valor * -1) ^ 0xFF) + 1;
        }

        /**
	     * Converte um número interio em um array de inteiros, no padrao BCD.
	     * 
	     * @param num
	     *            Número a ser convertido
	     * 
	     * @return array com o número representado em arra de inteiros
	     */
        public static byte[] DecToBCDArray(long num)
        {
            int digits = 0;
            long temp = num;
            while (temp != 0)
            {
                digits++;
                temp /= 10;
            }
            int byteLen = digits % 2 == 0 ? digits / 2 : (digits + 1) / 2;
            bool isOdd = digits % 2 != 0;
            byte[] bcd = new byte[byteLen];
            for (int i = 0; i < digits; i++)
            {
                byte tmp = (byte)(num % 10);

                if (i == digits - 1 && isOdd)
                    bcd[i / 2] = tmp;
                else if (i % 2 == 0)
                    bcd[i / 2] = tmp;
                else
                {
                    byte foo = (byte)(tmp << 4);
                    bcd[i / 2] |= foo;
                }
                num /= 10;
            }
            for (int i = 0; i < byteLen / 2; i++)
            {
                byte tmp = bcd[i];
                bcd[i] = bcd[byteLen - i - 1];
                bcd[byteLen - i - 1] = tmp;
            }
            if (bcd.Length == 1)
            {
                bcd = ProtocoloUtils.merge(new byte[][] { new byte[] { 0, 0 }, bcd });
            }
            else if (bcd.Length == 2)
            {
                bcd = ProtocoloUtils.merge(new byte[][] { new byte[] { 0 }, bcd });
            }
            return bcd;
        }
    }
}
