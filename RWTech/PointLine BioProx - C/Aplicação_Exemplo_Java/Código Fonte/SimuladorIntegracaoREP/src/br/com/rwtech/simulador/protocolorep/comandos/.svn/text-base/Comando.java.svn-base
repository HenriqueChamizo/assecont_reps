package br.com.rwtech.simulador.protocolorep.comandos;

import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.criptografia.AES;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.Conversor;
import br.com.rwtech.simulador.utils.ProtocoloUtils;

public abstract class Comando {
	
	@SuppressWarnings("static-access")
	public byte[] lerResposta(Tcp tcp, int qtdBytesBuffer, String hash) throws Exception {
		byte[] retorno = new byte[qtdBytesBuffer];
		int qdeRecebida = tcp.recebeBytes(retorno, 0, qtdBytesBuffer, Protocolo.TIMEOUT);
		if (qdeRecebida == -1) {
			return null;
		} else if (qdeRecebida != qtdBytesBuffer) {
			return new byte[0];
		}
		LogManager logManager = LogManager.getInstance();
		if (!logManager.ocultarBytesCriptografados) {
			logManager.bytesRecebidos.add(logManager.bytesToStringHex("Pacote criptografado retornado pelo REP", retorno));
		}
		
		retorno = AES.descriptografar(retorno, hash);
		if (!logManager.ocultarBytesDescriptografados) {
			logManager.bytesRecebidos.add(logManager.bytesToStringHex("Pacote descriptografado retornado pelo REP", retorno));
		}
		return retorno;
	}
	
	public FlagErroComando tratarResposta(int codigoComandoEsperado, byte[] retornoReal, int qdeRecebida, int qdeEsperada) {
		FlagErroComando flagErroComando = new FlagErroComando();
		
		if (qdeEsperada == qdeRecebida) {
			if (!validarComando(retornoReal, codigoComandoEsperado)) {
				return new FlagErroComando(FlagErroComando.RETORNO_INCONSISTENTE);
			}
			if (!validarBCC(retornoReal)) {
				return new FlagErroComando(FlagErroComando.ERRO_BCC);
			}
			flagErroComando.setErro(retornoReal[Protocolo.INDICE_FLAG_RETORNO]);
		} else {
			if ((-1) == qdeRecebida) {
				flagErroComando.setErro(FlagErroComando.COMUNICACAO_NAO_ESTABELECIDA);
			} else {
				flagErroComando.setErro(FlagErroComando.RETORNO_INCONSISTENTE);
			}
		}
		return flagErroComando;
	}
	
	private static boolean validarComando(byte[] pacote, int codigoComandoEsperado) {
		if (Conversor.byteToInt(pacote[Protocolo.INDICE_COMANDO_RETORNO]) != codigoComandoEsperado) {
			return false;
		}
		return true;
	}
	
	private static boolean validarBCC(byte[] pacote) {
		byte bccPacote = 0;
		byte bccRecebido = 0;
		bccRecebido = pacote[Protocolo.QTD_BYTES_CABECALHO_DADOS-2];
		for (int i = 0; i < (Protocolo.QTD_BYTES_CABECALHO_DADOS-2); i++) {
			bccPacote ^= pacote[i];
		}
		
		if (bccRecebido == bccPacote) {
			return true;
		} else {
			return false;
		}
	}
	
	
	@SuppressWarnings("static-access")
	public static void enviaBuffer(byte[] buffer, boolean enviarTudo, Tcp tcp, String hash) throws Exception {
		LogManager logManager = LogManager.getInstance();
		if (!logManager.ocultarBytesDescriptografados) {
			logManager.bytesEnviados.add(logManager.bytesToStringHex("Pacote descriptografado enviado pro REP", buffer));
		}
		byte[] bufferCriptografado = AES.criptografar(buffer, hash);
		if (!logManager.ocultarBytesCriptografados) {
			logManager.bytesEnviados.add(logManager.bytesToStringHex("Pacote criptografado enviado pro REP", bufferCriptografado));
		}
		tcp.enviaBytes(bufferCriptografado);
	}

	public static byte[] criarPacoteCabecalho(int start, int codigoComando, byte[] parametro, byte[] tamanho, byte[] cpfPis, byte flag, int end) {
		try {
			byte[] requisicao = { (byte) start // Campo Start 1 byte
								  , 0x00, 0x00, 0x00, 0x00 // Campo Endereço 4 bytes
								  , (byte) codigoComando }; // Campo Comando 1 byte
			requisicao = ProtocoloUtils.merge(requisicao, parametro); // Campo Parâmetro 4 bytes
			requisicao = ProtocoloUtils.merge(requisicao, tamanho); // Campo Tamanho 4 bytes
			requisicao = ProtocoloUtils.merge(requisicao, cpfPis); // Campo CPF/PIS 6 bytes
			requisicao = ProtocoloUtils.merge(requisicao, new byte[] { flag }); // Campo Flag/Erro 1 byte																	
			requisicao = ProtocoloUtils.calcularChecksum(requisicao); // Campo BCC 1 byte
			requisicao = ProtocoloUtils.merge(requisicao, new byte[] { (byte) end }); // Campo Flag/Erro 1 byte
			return requisicao;
		} catch (Exception e) {
			return null;
		}
	}

}