package br.com.rwtech.simulador.criptografia;

import java.security.NoSuchAlgorithmException;
import javax.crypto.Cipher;
import javax.crypto.NoSuchPaddingException;
import javax.crypto.spec.SecretKeySpec;

import br.com.rwtech.simulador.utils.Conversor;

public class AES {

	public final static int QTD_BYTES_BLOCO = 16;
	public final static int QTD_CARACTERES_CHAVE = 64;
	
	private static final String ALGORITHM = "AES";
	private static final String BLOCK_MODE = "ECB";
	private static final String PADDING = "NoPadding";
	private static Cipher cipher;

	public static SecretKeySpec getKey(String chave) throws NoSuchAlgorithmException {
		return new SecretKeySpec(Conversor.hexStringToByteArray(chave), ALGORITHM);
	}
	
	private static Cipher getInstanceCipher() throws NoSuchAlgorithmException, NoSuchPaddingException {
		if (cipher == null) {
			cipher = Cipher.getInstance(ALGORITHM + "/" + BLOCK_MODE + "/" + PADDING);
		}
		return cipher;
	}
	
	private static byte[] encrypt(byte[] pacoteOriginal, SecretKeySpec key) throws Exception {
		cipher = getInstanceCipher();
		cipher.init(Cipher.ENCRYPT_MODE, key);
		return cipher.doFinal(pacoteOriginal);
	}
	
	public static byte[] criptografar(byte[] bufferNaoCriptografado, String hash) throws Exception {
		int indiceBufferCru = 0;
		int tamanhoPacoteOriginal = bufferNaoCriptografado.length;
		int tamanhoPacoteCriptografado = tamanhoPacoteOriginal;
		if (tamanhoPacoteOriginal % 16 > 0) {
			tamanhoPacoteCriptografado += AES.QTD_BYTES_BLOCO - tamanhoPacoteOriginal % 16;
		}
		byte[] bufferCriptografado = new byte[tamanhoPacoteCriptografado];
		byte[] bufferCru = new byte[AES.QTD_BYTES_BLOCO];
		byte[] blocoBufferCriptografado;
		SecretKeySpec key = AES.getKey(hash);
		int ultimoIndiceBufferCriptografado = 0;
		for (int i=0; i < tamanhoPacoteOriginal; i++) {
			bufferCru[indiceBufferCru] = bufferNaoCriptografado[i];
			indiceBufferCru++;
			if (indiceBufferCru == AES.QTD_BYTES_BLOCO) {
				indiceBufferCru = 0;
				blocoBufferCriptografado = AES.encrypt(bufferCru, key);
				for (int j=0; j < blocoBufferCriptografado.length; j++) {
					bufferCriptografado[ultimoIndiceBufferCriptografado] = blocoBufferCriptografado[j];
					ultimoIndiceBufferCriptografado++;
				}
			}
		}
		
		if (indiceBufferCru > 0) {
			while (indiceBufferCru < AES.QTD_BYTES_BLOCO) {
				bufferCru[indiceBufferCru] = (byte) 0xFF;
				indiceBufferCru++;
			}
			indiceBufferCru = 0;
			blocoBufferCriptografado = AES.encrypt(bufferCru, key);
			for (int j=0, i=ultimoIndiceBufferCriptografado; j < blocoBufferCriptografado.length; j++, i++) {
				try {
					bufferCriptografado[i] = blocoBufferCriptografado[j];
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		}
		return bufferCriptografado;
	}
	
	public static int defineTamanhoPacote(int tamanhoPacoteOriginal) {
		int tamanhoPacoteCriptografado = tamanhoPacoteOriginal;
		if (tamanhoPacoteOriginal % AES.QTD_BYTES_BLOCO > 0) {
			tamanhoPacoteCriptografado += AES.QTD_BYTES_BLOCO - tamanhoPacoteOriginal % 16;
		}
		return tamanhoPacoteCriptografado;
	}
	
	public static byte[] descriptografar(byte[] bufferCriptografado, String hash) throws Exception {
		int indiceBufferCru = 0;
		int tamanhoPacoteOriginal = bufferCriptografado.length;
		int tamanhoPacoteCriptografado = tamanhoPacoteOriginal;
		if (tamanhoPacoteOriginal % AES.QTD_BYTES_BLOCO > 0) {
			tamanhoPacoteCriptografado += AES.QTD_BYTES_BLOCO - tamanhoPacoteOriginal % 16;
		}
		byte[] bufferDescriptografado = new byte[tamanhoPacoteCriptografado];
		byte[] bufferCru = new byte[AES.QTD_BYTES_BLOCO];
		byte[] blocoBufferDescriptografado;
		SecretKeySpec key = AES.getKey(hash);
		int ultimoIndiceBufferDescriptografado = 0;
		for (int i=0; i < tamanhoPacoteOriginal; i++) {
			bufferCru[indiceBufferCru] = bufferCriptografado[i];
			indiceBufferCru++;
			if (indiceBufferCru == AES.QTD_BYTES_BLOCO) {
				indiceBufferCru = 0;
				blocoBufferDescriptografado = AES.decrypt(bufferCru, key);
				for (int j=0; j < blocoBufferDescriptografado.length; j++) {
					bufferDescriptografado[ultimoIndiceBufferDescriptografado] = blocoBufferDescriptografado[j];
					ultimoIndiceBufferDescriptografado++;
				}
			}
		}
		
		if (indiceBufferCru > 0) {
			while (indiceBufferCru < AES.QTD_BYTES_BLOCO) {
				bufferCru[indiceBufferCru] = (byte) 0xFF;
				indiceBufferCru++;
			}
			indiceBufferCru = 0;
			blocoBufferDescriptografado = AES.decrypt(bufferCru, key);
			for (int j=0, i=ultimoIndiceBufferDescriptografado; j < blocoBufferDescriptografado.length; j++, i++) {
				try {
					bufferDescriptografado[i] = blocoBufferDescriptografado[j];
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		}
		return bufferDescriptografado;
	}
	
	public static byte[] decrypt(byte[] pacoteEncriptado, SecretKeySpec key) throws Exception {
		cipher = getInstanceCipher();
		cipher.init(Cipher.DECRYPT_MODE, key);
		return cipher.doFinal(pacoteEncriptado);
	}

}
