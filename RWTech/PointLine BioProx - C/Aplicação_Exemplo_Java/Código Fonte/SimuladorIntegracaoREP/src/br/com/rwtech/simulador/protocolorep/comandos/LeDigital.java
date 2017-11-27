package br.com.rwtech.simulador.protocolorep.comandos;

import java.io.IOException;
import java.util.Arrays;

import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.Conversor;

public class LeDigital extends Comando {

	private FlagErroComando flagErroComando;
	private String hash;
	private Tcp tcp;
	
	public LeDigital(String hash) {
		this.hash = hash;
	}

	public FlagErroComando execute(String ip, String porta, String pis) {
		flagErroComando = new FlagErroComando();
		tcp = null;
		try {
			tcp = new Tcp(ip, Integer.parseInt(porta));
		} catch (NumberFormatException | IOException e1) {
			flagErroComando.setErro(FlagErroComando.COMUNICACAO_NAO_ESTABELECIDA);
			return flagErroComando;
		}
		try {
			try {
				byte[] buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.LER_DIGITAL, 
																     Conversor.pisToByte(pis), new byte[2], 
																     new byte[6], (byte) 0x00, CodigosComandos.END);
				Comando.enviaBuffer(buffer, true, tcp, hash);
				// Lê e trata 1º cabeçalho
				byte[] respostaREP = this.lerEtratarResposta(Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
				if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {
					int qtdDigitais = respostaREP[Protocolo.INDICE_QTD_DIGITAIS_A_SEREM_LIDAS];
					String template1 = "";
					String template2 = "";
					LogManager logManager = LogManager.getInstance();
					flagErroComando.setErro(FlagErroComando.DADOS_OK);
					for (int i=1; i <= qtdDigitais && FlagErroComando.DADOS_OK == flagErroComando.getErro(); i++) {
						// Lê e trata cabeçalho dos dados
						respostaREP = this.lerEtratarResposta(Protocolo.QTD_BYTES_DIGITAL);
						if (flagErroComando.getErro() == FlagErroComando.DADOS_OK) {
							// Lê a digital (2 templates)
							respostaREP = Arrays.copyOfRange(respostaREP, Protocolo.QTD_BYTES_CABECALHO_DADOS, respostaREP.length);
							int qtdBytesRecebidos = -1;
							if (respostaREP != null) {
								qtdBytesRecebidos = respostaREP.length;
							}
							if (Protocolo.QTD_BYTES_DIGITAL - Protocolo.QTD_BYTES_CABECALHO_DADOS == qtdBytesRecebidos) {
								// Trata as templates
								if (0 == respostaREP[0]) {
									i = qtdDigitais; // Força a finalização da comunicação porque não há mais digital a ser lida
								} else {
									template1 = String.format("%02x%02x%02x", respostaREP[0], respostaREP[1], respostaREP[2]);
									template1 += Conversor.bytesToStringHex(Arrays.copyOfRange(respostaREP, Protocolo.QTD_BYTES_FIXOS_DIGITAL, 
																										   Protocolo.QTD_BYTES_PRIMEIRA_TEMPLATE));
									logManager.bytesRecebidos.add("Template 1: " + template1);
									template2 = Conversor.bytesToStringHex(Arrays.copyOfRange(respostaREP, Protocolo.QTD_BYTES_PRIMEIRA_TEMPLATE, 
																										   Protocolo.QTD_BYTES_TEMPLATES));
									logManager.bytesRecebidos.add("Template 2: " + template2);
								}
							} else {
								flagErroComando.setErro(FlagErroComando.COMUNICACAO_NAO_ESTABELECIDA);
							}
							if (flagErroComando.getErro() == FlagErroComando.DADOS_OK) {
								/**
								 * Envia pacote de confirmação OK da leitura da digital
								 * Nota : campo "total" fixado em 0x0A, porque o REP sempre retorna essa quantidade de digitais.
								 */
								buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.LER_DIGITAL, 
																		      new byte[] {0x00, 0x0A, 0x00, (byte) i}, new byte[4],  
																		      new byte[6], (byte) FlagErroComando.DADOS_OK, CodigosComandos.END);
								Comando.enviaBuffer(buffer, true, tcp, hash);
							}
						}
					}
				}
			} catch (Exception e) {
				flagErroComando.setErro(FlagErroComando.OCORREU_EXCECAO);
				e.printStackTrace();
			}
			tcp.finalizaConexao();
		} catch (Exception e) {
			flagErroComando.setErro(FlagErroComando.ERRO_FINALIZAR_CONEXAO);
			e.printStackTrace();
		}
		
		return flagErroComando;
	}
	
	private byte[] lerEtratarResposta(int qtdBytesEsperada) throws Exception {
		// Lê resposta do REP
		byte[] respostaREP = super.lerResposta(tcp, qtdBytesEsperada, hash);
		int qtdBytesRecebidos = -1;
		if (respostaREP != null) {
			qtdBytesRecebidos = respostaREP.length;
		}
		// Trata a resposta do REP
		flagErroComando = super.tratarResposta(CodigosComandos.LER_DIGITAL, respostaREP, qtdBytesRecebidos, qtdBytesEsperada);
		return respostaREP;
	}
}
