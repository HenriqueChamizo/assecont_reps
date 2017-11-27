package br.com.rwtech.simulador.utils;

import org.joda.time.DateTime;


public abstract class ProtocoloUtils {

	/**
	 * Respons·vel por calcular o Checksum e o tamanho do pacote
	 * 
	 * @param dados
	 *            Array de byte sem o checksum
	 * @return Array de byte com adi√ß√£o do checksum.
	 */
	public static byte[] calcularChecksum(byte[] dados) {
		byte cs = 0;
		for (int i = 0; i < dados.length; i++) {
			// XOR
			cs ^= dados[i];
		}
		dados = merge(dados, new byte[] { cs });
		return dados;
	}
	
	/**
	 * Respons·vel por calcular o Checksum
	 * 
	 * @param dados
	 *            Array de byte sem o checksum
	 * @return Byte referente ao checksum.
	 */
	public static byte getChecksum(byte[] dados) {
		byte cs = 0;
		for (int i = 0; i < dados.length; i++) {
			// XOR
			cs ^= dados[i];
		}
		return cs;
	}

	/**
	 * Respons·vel em unir n arrays em um.
	 * 
	 * @param arrays
	 *            Quantidade de array separados por ','
	 * @return Array com a uni„o dos par‚metros.
	 */
	public static byte[] merge(byte[]... arrays) {
		int count = 0;
		for (byte[] array : arrays) {
			count += array.length;
		}
		byte[] mergedArray = new byte[count];
		int start = 0;
		for (byte[] array : arrays) {
			System.arraycopy(array, 0, mergedArray, start, array.length);
			start += array.length;
		}
		return mergedArray;
	}

	/**
	 * Respons·vel por obter a data do sistema e transformar os dados em um array de byte.
	 * 
	 * @return Array de byte com a data do sistema.
	 */
	public static byte[] data() {
		byte[] data = new byte[4];
		DateTime dt = new DateTime();
		
		data[0] = 0;
		
		data[1] = (byte) Integer.parseInt(Integer.toBinaryString(dt.getDayOfMonth()), 2);		
		
		String bitsMes = Integer.toBinaryString(dt.getMonthOfYear());
		while(bitsMes.length() < 4) {
			bitsMes = "0" + bitsMes;
		}
		String bitsAnoFull = Integer.toBinaryString(dt.getYear());
		String bitsAnoAux = bitsAnoFull.substring(bitsAnoFull.length()-8, bitsAnoFull.length());
		bitsAnoFull = bitsAnoFull.substring(0, bitsAnoFull.length()-bitsAnoAux.length());
		while(bitsAnoFull.length() < 4) {
			bitsAnoFull = "0" + bitsAnoFull;
		}
		data[2] = (byte) Integer.parseInt(bitsMes+bitsAnoFull, 2);
		
		data[3] = (byte) Integer.parseInt(bitsAnoAux, 2);
		return data;
	}
	
	/**
	 * Respons·vel por obter o hor·rio do sistema e transformar os dados em um array de byte.
	 * 
	 * @return Array de byte com o hor·rio do sistema.
	 */
	public static byte[] horario() {
		int[] hora = new int[4];

		DateTime dt = new DateTime();
		hora[0] = 0;
		hora[1] = dt.getSecondOfMinute();
		hora[2] = dt.getMinuteOfHour();
		hora[3] = dt.getHourOfDay();

		return Conversor.intArrayToByteArray(hora);
	}
    
}
