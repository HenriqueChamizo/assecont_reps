package br.com.rwtech.simulador.common;

import java.util.concurrent.ConcurrentLinkedQueue;

import br.com.rwtech.simulador.utils.Conversor;

public class LogManager {
	
	private static LogManager instance;
	public boolean ocultarBytesCriptografados;
	public boolean ocultarBytesDescriptografados;
	
	public static LogManager getInstance() {
		if (instance == null) {
			instance = new LogManager();
		}
		return instance;
	}
	
	public ConcurrentLinkedQueue<String> bytesEnviados = new ConcurrentLinkedQueue<>();
	public String getNextBytesEnviados() {
		if (!bytesEnviados.isEmpty()) {
			return bytesEnviados.poll();
		}
		return null;
	}
	
	public ConcurrentLinkedQueue<String> bytesRecebidos = new ConcurrentLinkedQueue<>();
	public String getBytesRecebidos() {
		if (!bytesRecebidos.isEmpty()) {
			return bytesRecebidos.poll();
		}
		return null;
	}
	
	public ConcurrentLinkedQueue<String> resultados = new ConcurrentLinkedQueue<>();
	public String getResultado() {
		if (!resultados.isEmpty()) {
			return resultados.poll();
		}
		return null;
	}

	public static String bytesToStringHex(String tituloMsg, byte[] bytes) {
    	StringBuilder str = new StringBuilder();
		for (int j = 0; j < bytes.length; j++) {
			String hex = Integer.toHexString(Conversor.byteToInt(bytes[j]));
			if (hex.length() < 2) {
				hex = "0" + hex;
			}
			str.append(hex.toUpperCase() + " ");
		}
		return tituloMsg + ": " + str.toString();
    }
}
