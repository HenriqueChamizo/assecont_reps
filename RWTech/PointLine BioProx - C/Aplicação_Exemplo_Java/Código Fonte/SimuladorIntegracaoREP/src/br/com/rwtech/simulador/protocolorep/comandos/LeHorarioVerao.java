package br.com.rwtech.simulador.protocolorep.comandos;

import java.io.IOException;
import java.util.Arrays;

import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.Conversor;

public class LeHorarioVerao extends Comando {
	
	private String ip;
	private String porta;
	private String cpf;
	private String hash;
	private FlagErroComando flagErroComando;
	private Tcp tcp;
	
	public LeHorarioVerao(String ip, String porta, String cpf, String hash) {
		this.ip = ip;
		this.porta = porta;
		this.cpf = cpf;
		this.hash = hash;
	}
	
	public FlagErroComando execute() {
		flagErroComando = new FlagErroComando();
		tcp = null;
		try {
			tcp = new Tcp(ip, Integer.parseInt(porta));
		} catch (NumberFormatException | IOException e1) {
			flagErroComando.setErro(FlagErroComando.COMUNICACAO_NAO_ESTABELECIDA);
			return flagErroComando;
		}
		
		byte[] buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.LER_HORARIO_VERAO, 
													 new byte[4], new byte[4], Conversor.cpfToByte(cpf), 
													 (byte) 0x00, CodigosComandos.END);
		try {
			Comando.enviaBuffer(buffer, true, tcp, hash);
			try {
				// Lê e trata resposta do REP
				byte[] respostaREP = this.lerEtratarResposta(Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
//				System.out.println("");
//				System.out.println("Resultado do 1º cabeçalho -> " + flagErroComando.getMensagem());
				if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {
					flagErroComando = this.tratarResposta(0, respostaREP, 0, 0);
				}
				if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {
					// Envia pacote de confirmação OK da leitura
					buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.LER_HORARIO_VERAO, 
													      new byte[] {0x00, (byte) 1, 0x00, (byte) 1}, new byte[4], 
													      new byte[6], (byte) FlagErroComando.DADOS_OK, CodigosComandos.END);
					Comando.enviaBuffer(buffer, true, tcp, hash);
					if (FlagErroComando.SUCESSO == flagErroComando.getErro() || FlagErroComando.DADOS_OK == flagErroComando.getErro()) {
						tcp.finalizaConexao();
					}
				}
			} catch (IOException e) {
				e.printStackTrace();
			}
			tcp.finalizaConexao();
		} catch (Exception e) {
			flagErroComando.setErro(FlagErroComando.OCORREU_EXCECAO);
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
		flagErroComando = super.tratarResposta(CodigosComandos.LER_HORARIO_VERAO, respostaREP, qtdBytesRecebidos, qtdBytesEsperada);
		return respostaREP;
	}
	
	@Override
	public FlagErroComando tratarResposta(int codigoComandoEsperado,
										  byte[] dados, int qdeRecebida, int qdeEsperada) {
		FlagErroComando flagErroComando = new FlagErroComando(FlagErroComando.SUCESSO);
		try {
			// Início do Horário de Verão
			byte[] bytesInicioHV = Arrays.copyOfRange(dados, Protocolo.INDICE_INICIO_PARAMETRO, Protocolo.INDICE_FIM_PARAMETRO+1);
			String inicioHVHexStr = Conversor.bytesToStringHex(bytesInicioHV);
			int hora = Conversor.stringHexToByte(inicioHVHexStr.substring(0, 2), 16);
			int dia = Conversor.stringHexToByte(inicioHVHexStr.substring(2, 4), 16);
			int mes = Conversor.stringHexToByte(inicioHVHexStr.substring(4, 5), 16);
			byte[] bytesAno = new byte[] { Conversor.stringHexToByte(inicioHVHexStr.substring(5, 6), 16), Conversor.stringHexToByte(inicioHVHexStr.substring(6, 8), 16) };
			int ano = Conversor.ByteArrayToint(bytesAno);
			String diaStr = Integer.toString(dia);
			if (dia < 10) {
				diaStr = "0" + diaStr;
			}
			String mesStr = Integer.toString(mes);
			if (mes < 10) {
				mesStr = "0" + mesStr;
			}
			String resultadoFormatado = "Início do Horário de Verão: " + diaStr + "/" + mesStr + "/" + ano; 
			LogManager logManager = LogManager.getInstance();
			logManager.bytesRecebidos.add(resultadoFormatado);
			// Fim do Horário de Verão
			byte[] bytesFimHV = Arrays.copyOfRange(dados, Protocolo.INDICE_INICIO_TAMANHO, Protocolo.INDICE_FIM_TAMANHO+1);
			String fimHVHexStr = Conversor.bytesToStringHex(bytesFimHV);
			hora = Conversor.stringHexToByte(fimHVHexStr.substring(0, 2), 16);
			dia = Conversor.stringHexToByte(fimHVHexStr.substring(2, 4), 16);
			mes = Conversor.stringHexToByte(fimHVHexStr.substring(4, 5), 16);
			bytesAno = new byte[] { Conversor.stringHexToByte(fimHVHexStr.substring(5, 6), 16), Conversor.stringHexToByte(fimHVHexStr.substring(6, 8), 16) };
			ano = Conversor.ByteArrayToint(bytesAno);
			diaStr = Integer.toString(dia);
			if (dia < 10) {
				diaStr = "0" + diaStr;
			}
			mesStr = Integer.toString(mes);
			if (mes < 10) {
				mesStr = "0" + mesStr;
			}
			resultadoFormatado = "Fim do Horário de Verão: " + diaStr + "/" + mesStr + "/" + ano; 
			logManager.bytesRecebidos.add(resultadoFormatado);
		} catch (Exception e) {
			flagErroComando.setErro(FlagErroComando.RETORNO_INCONSISTENTE);
		}
		
		return flagErroComando;
	}
	
}
