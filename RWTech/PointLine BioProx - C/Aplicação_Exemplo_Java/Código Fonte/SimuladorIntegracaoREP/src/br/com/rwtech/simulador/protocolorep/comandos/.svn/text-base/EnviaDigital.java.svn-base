package br.com.rwtech.simulador.protocolorep.comandos;

import java.io.IOException;
import java.util.Arrays;
import java.util.List;

import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.criptografia.AES;
import br.com.rwtech.simulador.pojo.Digital;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.Conversor;
import br.com.rwtech.simulador.utils.ProtocoloUtils;

public class EnviaDigital extends Comando {
	
	private Tcp tcp;
	private String hash;
	private FlagErroComando flagErroComando;
	
	public EnviaDigital(String hash) {
		this.hash = hash;
	}
	
	public FlagErroComando execute(String ip, String porta, List<Digital> digitais, String pis) {
		flagErroComando = new FlagErroComando();
		tcp = null;
		try {
			tcp = new Tcp(ip, Integer.parseInt(porta));
		} catch (NumberFormatException | IOException e1) {
			flagErroComando.setErro(FlagErroComando.COMUNICACAO_NAO_ESTABELECIDA);
			return flagErroComando;
		}
		int totalDigitais = 0;
		try {
			try {
				totalDigitais = digitais.size();
				int digitalAtual = 1; 
				byte flag;
				if (totalDigitais == 1) {
					flag = FlagErroComando.UNICA_TEMPLATE_USUARIO;
				} else {
					flag = FlagErroComando.TODAS_TEMPLATE_USUARIO;
				}
				byte[] cabecalhoDadosGravaDigital = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.ENVIAR_DIGITAL, 
																				 new byte[] { 0x00, 0x00, 0x00, (byte) totalDigitais }, new byte[4], 
																				 Conversor.pisToByte(pis), 
																				 flag, CodigosComandos.END);
				byte[] primeiroBlocoCabecalho = Arrays.copyOfRange(cabecalhoDadosGravaDigital, 0, 16); // primeiros 16 bytes do cabeçalho
				// Envia o cabeçalho
				Comando.enviaBuffer(primeiroBlocoCabecalho, true, tcp, hash);
				/**
				 * Envio dos bytes do cabeçalho ainda não enviados + bloco vazio (0xFF).
				 * Isto é necessário para o REP reconhecer tudo corretamente (visto empiricamente). 
				 */
				boolean incluirBytesNoInicio = true;
				enviaBlocoVazio(Arrays.copyOfRange(cabecalhoDadosGravaDigital, 16, cabecalhoDadosGravaDigital.length), incluirBytesNoInicio);
				
				// Lê 1ª resposta do REP
				lerEtratarResposta(Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
				if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {
					flagErroComando.setErro(FlagErroComando.DADOS_OK);
					incluirBytesNoInicio = false;
					for (; (digitalAtual <= totalDigitais) && (flagErroComando.getErro() == FlagErroComando.DADOS_OK); digitalAtual++) {
						cabecalhoDadosGravaDigital = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.ENVIAR_DIGITAL, 
													 							  new byte[] { 0x00, (byte) totalDigitais, 0x00, (byte) digitalAtual }, 
													 							  new byte[] { 0x00, 0x00, 0x03, 0x23 }, // 803 bytes
													 							  new byte[6], (byte) FlagErroComando.DADOS_OK, CodigosComandos.END);
						byte checkSumCabecalho = cabecalhoDadosGravaDigital[cabecalhoDadosGravaDigital.length-2];
						Digital digitalComunicador = digitais.get(digitalAtual-1);
						byte[] dadosGravaDigital = criaPacoteDados(checkSumCabecalho, digitalComunicador);
						
						/**
						 * Envio de bloco vazio:
						 * Necessário para o REP reconhecer tudo corretamente (visto empiricamente). 
						 */
						if (digitalAtual == 1) {
							enviaBlocoVazio(Arrays.copyOfRange(cabecalhoDadosGravaDigital, 0, 2), incluirBytesNoInicio);
							// Envia a digital
							Comando.enviaBuffer(ProtocoloUtils.merge(Arrays.copyOfRange(cabecalhoDadosGravaDigital, 2, cabecalhoDadosGravaDigital.length), dadosGravaDigital), true, tcp, hash);
						} else {
							primeiroBlocoCabecalho = Arrays.copyOfRange(cabecalhoDadosGravaDigital, 0, 16); // primeiros 16 bytes 
							Comando.enviaBuffer(primeiroBlocoCabecalho, true, tcp, hash);
							// Envia a digital
							Comando.enviaBuffer(ProtocoloUtils.merge(Arrays.copyOfRange(cabecalhoDadosGravaDigital, 16, cabecalhoDadosGravaDigital.length), dadosGravaDigital), true, tcp, hash);
						}
						
						// Lê e trata 2ª resposta do REP
						lerEtratarResposta(Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
						incluirBytesNoInicio = true;
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
	
	private void enviaBlocoVazio(byte[] bytes, boolean incluirBytesNoInicio) throws Exception {
		byte[] blocoVazio = new byte[AES.QTD_BYTES_BLOCO - bytes.length];
		for (int i=0; i < blocoVazio.length; i++) {
			blocoVazio[i] = (byte) 0xFF;
		}
		if (incluirBytesNoInicio) {
			blocoVazio = ProtocoloUtils.merge(bytes, blocoVazio);
			byte[] blocoVazioAux = new byte[AES.QTD_BYTES_BLOCO];
			for (int i=0; i < blocoVazioAux.length; i++) {
				blocoVazioAux[i] = (byte) 0xFF;
			}
			blocoVazio = ProtocoloUtils.merge(blocoVazio, blocoVazioAux);
		} else {
			blocoVazio = ProtocoloUtils.merge(blocoVazio, bytes);
		}
		
		Comando.enviaBuffer(blocoVazio, true, tcp, hash);
	}
	
	private byte[] lerEtratarResposta(int qtdBytesEsperada) throws Exception {
		// Lê resposta do REP
		byte[] respostaREP = super.lerResposta(tcp, qtdBytesEsperada, hash);
		int qtdBytesRecebidos = -1;
		if (respostaREP != null) {
			qtdBytesRecebidos = respostaREP.length;
		}
		// Trata a resposta do REP
		flagErroComando = super.tratarResposta(CodigosComandos.ENVIAR_DIGITAL, respostaREP, qtdBytesRecebidos, qtdBytesEsperada);
		return respostaREP;
	}
	
	private byte[] criaPacoteDados(byte checkSumCabecalho, Digital digital) {
		byte[] requisicao = new byte[] { 0x01, 0x20, 0x03 }; // bytes fixos
		String dedoAux = digital.getTemplate1().substring(6); // desconsidera os 3 primeiros bytes (fixados acima)
		dedoAux += digital.getTemplate2();
		requisicao = ProtocoloUtils.merge(requisicao, Conversor.hexStringToByteArray(dedoAux));
		byte checksum = ProtocoloUtils.getChecksum(ProtocoloUtils.merge(requisicao, new byte[] { checkSumCabecalho, CodigosComandos.END }));
		requisicao = ProtocoloUtils.merge(requisicao, new byte[] { checksum });
		return requisicao;
	}
}
