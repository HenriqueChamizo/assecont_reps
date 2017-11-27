package br.com.rwtech.simulador.utils;

import java.nio.ByteBuffer;
import java.text.SimpleDateFormat;
import java.util.Date;


/**
 * Recursos para realizar conversões de dados.
 *
 */
public class Conversor {

    final protected static char[] hexArray = {'0','1','2','3','4','5','6','7','8','9','a','b','c','d','e','f'};
    private static final int FF_HEX = 0xFF;
	private static long PESO_1 = 256l;
	private static long PESO_2 = 65536l;
	private static long PESO_3 = 16777216l;
	private static long PESO_4 = 4294967296l;
	private static long PESO_5 = 1099511627776l;
	
	public static byte[] cpfToByte(String cpf) throws NumberFormatException {
		return longToByteArray(Long.parseLong(cpf), 6);
	}
	
	public static byte[] cnpjToByte(String cnpj) throws NumberFormatException {
		return longToByteArray(Long.parseLong(cnpj), 6);
	}
	
	public static byte[] ceiToByte(String cei) throws NumberFormatException {
		if (cei == null || cei.isEmpty()) {
			cei = "0";
		}
		return longToByteArray(Long.parseLong(cei), 5);
	}
	
	public static byte[] pisToByte(String pis) throws NumberFormatException {
		return DecToBCDArray(Long.parseLong(pis));
	}
	
	public static String BCDtoString(byte bcd) {
        StringBuffer sb = new StringBuffer();

        byte high = (byte) (bcd & 0xf0);
        high >>>= (byte) 4; 
        high = (byte) (high & 0x0f);
        byte low = (byte) (bcd & 0x0f);

        sb.append(high);
        sb.append(low);

        return sb.toString();
    }

    public static String BCDtoString(byte[] bcd) {

        StringBuffer sb = new StringBuffer();

        for (int i = 0; i < bcd.length; i++) {
            sb.append(BCDtoString(bcd[i]));
        }

        return sb.toString();
    }

    /**
     * Mantém somente os caracteres de 0 a 9 na string.
     *
     * @param s  String a ser limpa.
     * @return   Retorna a string limpa (somente os caracteres de 0 a 9).
     */
    public static String SomenteNumeros(String s) {
        if (s != null) {
        	return s.replaceAll("[^0-9]", "");
        } else {
        	return "";
        }
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
    public static String SomenteNumeros(String s, int qtdeDigitos) {
        String str = new String(SomenteNumeros(s));

        while (str.length() < qtdeDigitos) {
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
    public static byte[] hexStringToByteArray(String s) {
        int len = s.length();
        byte[] data = new byte[len / 2];
        for (int i = 0; i < len; i += 2) {
            data[i / 2] = (byte) ((Character.digit(s.charAt(i), 16) << 4)
                    + Character.digit(s.charAt(i + 1), 16));
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
    public static String bytesToStringHex(byte[] bytes) {
        char[] hexChars = new char[bytes.length * 2];
        int v;
        for (int j = 0; j < bytes.length; j++) {
            v = bytes[j] & 0xFF;
            hexChars[j * 2] = hexArray[v >>> 4];
            hexChars[j * 2 + 1] = hexArray[v & 0x0F];
        }
        return new String(hexChars);
    }
    
    public static byte[] longToByteArray(long value, int size) {
		String resultado = Long.toHexString(value);
		while (resultado.length() < size * 2) {
			resultado = "0" + resultado;
		}
		byte[] retorno = new byte[size];
		for (int i = 0, j = 0; i < size; i++, j += 2) {
			retorno[i] = (byte) Integer.parseInt(resultado.substring(j, j + 2), 16);
		}
		return retorno;
	}

	public static Date strToDate(String data) {
		try {
			if (Validacao.isStringDataOK(data, 8)) {
				return new SimpleDateFormat("dd/MM/yyyy").parse(data);
			}
		} catch (Exception e) {
		}
		return null;
	}
	
	public static byte stringHexToByte(String strHex, int base) {
		int valor = (Integer.parseInt(strHex,base) & FF_HEX);
		if (valor < 0) {
			valor = convertByteNegative(valor);
		}
		return (byte) valor;
	}
		
	public static String bytesIdentificadorToString(byte[] bytes) {
    	int qtdBytes = bytes.length;
    	int [] dadosInt = new int[qtdBytes];
    	for (int i=0; i < qtdBytes; i++) {
    		dadosInt[i] = byteToInt(bytes[i]);
    	}
    	return String.valueOf(dadosInt[0] * PESO_5 + dadosInt[1] * PESO_4 + dadosInt[2] * PESO_3 + dadosInt[3] * PESO_2 + dadosInt[4] * PESO_1 + dadosInt[5]);
    }
    
    public static String bytesCEIToString(byte[] bytes) {
    	int qtdBytes = bytes.length;
    	int [] dadosInt = new int[qtdBytes];
    	for (int i=0; i < qtdBytes; i++) {
    		dadosInt[i] = byteToInt(bytes[i]);
    	}
    	return String.valueOf(dadosInt[0] * PESO_4 + dadosInt[1] * PESO_3 + dadosInt[2] * PESO_2 + dadosInt[3] * PESO_1 + dadosInt[4]);
    }
    
    public static String bytesPISToString(byte[] bytes) {
    	int qtdBytes = bytes.length;
    	int [] dadosInt = new int[qtdBytes];
    	for (int i=0; i < qtdBytes; i++) {
    		dadosInt[i] = byteToInt(bytes[i]);
    	}
    	return String.valueOf(dadosInt[0] * PESO_4 + dadosInt[1] * PESO_3 + dadosInt[2] * PESO_2 + dadosInt[3] * PESO_1 + dadosInt[4]);
    }
    
    public static String bytesIDBioToString(byte[] bytes) {
    	int qtdBytes = bytes.length;
    	int [] dadosInt = new int[qtdBytes];
    	for (int i=0; i < qtdBytes; i++) {
    		dadosInt[i] = byteToInt(bytes[i]);
    	}
    	return String.valueOf(dadosInt[0] * PESO_3 + dadosInt[1] * PESO_2 + dadosInt[2] * PESO_1 + dadosInt[3]);
    }
    
    public static String bytesCodigoToString(byte[] bytes) {
    	int qtdBytes = bytes.length;
    	int [] dadosInt = new int[qtdBytes];
    	for (int i=0; i < qtdBytes; i++) {
    		dadosInt[i] = byteToInt(bytes[i]);
    	}
    	return String.valueOf(dadosInt[0] * PESO_2 + dadosInt[1] * PESO_1 + dadosInt[2]);
    }
    
    public static String bytesASCIIToString(byte[] bytes) {
        String str = new String("");
        int qdeBytes;

        qdeBytes = bytes.length;

        /* Converte e transfere o conteúdo de "bytes" para a "string" */
        int i;
        char aux;
        for (i = 0; (bytes[i] != 0) && (i <= qdeBytes); i++) {
            aux = (char) ((int) bytes[i] & 0xFF);
            str += aux;
        }

        return str;
    }
    
    static public boolean byteToBoolean(byte b) {
        return (boolean) (((b != '0') && (b != 0)) ? true : false);
    }
    
    static public byte[] intToByteArray2(int valor) {
    	byte[] array = new byte[2];
    	array[0] = (byte) ((valor >> 8) & 0xFF);
    	array[1] = (byte) (valor & 0xFF);
    	return array;
    }
    
    static public byte[] intToByteArray(int valor, int tamanhoArray) {
    	return ByteBuffer.allocate(tamanhoArray).putInt(valor).array();
    }
    
    static public byte[] stringParaBytes(String str, int qdeBytes) {
        byte[] bytes = new byte[qdeBytes];
        int i;
        int strLength = str.length(); 

        /* Reliza a conversão de "String" para "byte []" */
        for (i = 0; i < strLength; i++) {
            bytes[i] = (byte) str.charAt(i);
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
	public static byte[] intArrayToByteArray(int[] array) {
		byte[] arrayAux = new byte[array.length];
		for (int i = 0; i < array.length; i++) {
			arrayAux[i] = (byte) array[i];
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
	public static byte[] stringToByteArray(String string, int size) {
		byte[] retorno = new byte[size];
		for (int i = 0; i < string.length(); i++) {
			retorno[i] = (byte) string.charAt(i);
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
	public static int ByteArrayToint(byte[] array) {
		if (array.length == 4)
	      return array[0] << 24 | (array[1] & 0xff) << 16 | (array[2] & 0xff) << 8 | (array[3] & 0xff);
	    else if (array.length == 2)
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
	public static int byteToInt(byte valor) {
		return (int) valor & 0xFF;
	}
	
	public static int convertByteNegative(int valor) {
		return ((valor*-1) ^ 0xFF) + 1;
	}

	/**
	 * Converte um número interio em um array de inteiros, no padrao BCD.
	 * 
	 * @param num
	 *            Número a ser convertido
	 * 
	 * @return array com o número representado em arra de inteiros
	 */
	public static byte[] DecToBCDArray(long num) {
		int digits = 0;
		long temp = num;
		while (temp != 0) {
			digits++;
			temp /= 10;
		}
		int byteLen = digits % 2 == 0 ? digits / 2 : (digits + 1) / 2;
		boolean isOdd = digits % 2 != 0;
		byte bcd[] = new byte[byteLen];
		for (int i = 0; i < digits; i++) {
			byte tmp = (byte) (num % 10);

			if (i == digits - 1 && isOdd)
				bcd[i / 2] = tmp;
			else if (i % 2 == 0)
				bcd[i / 2] = tmp;
			else {
				byte foo = (byte) (tmp << 4);
				bcd[i / 2] |= foo;
			}
			num /= 10;
		}
		for (int i = 0; i < byteLen / 2; i++) {
			byte tmp = bcd[i];
			bcd[i] = bcd[byteLen - i - 1];
			bcd[byteLen - i - 1] = tmp;
		}
		if (bcd.length == 1) {
			bcd = ProtocoloUtils.merge(new byte[] { 0, 0 }, bcd);
		} else if (bcd.length == 2) {
			bcd = ProtocoloUtils.merge(new byte[] { 0 }, bcd);
		}
		return bcd;
	}
}
