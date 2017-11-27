package br.com.rwtech.simulador.protocolorep.comandos;

import java.io.IOException;
import java.util.Arrays;

import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.Conversor;

public class VerificaPapelEnroscado extends Comando {

	private String hash;
	private FlagErroComando flagErroComando;
	private Tcp tcp;
	
	public VerificaPapelEnroscado(String hash) {
		this.hash = hash;
	}
	
	public FlagErroComando execute(String ip, String porta) {
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
				byte[] buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.VERIFICAR_PAPEL_ENROSCADO, 
						 									 new byte[4], new byte[4], new byte[6], 
						 									 (byte) 0x00, CodigosComandos.END);
				Comando.enviaBuffer(buffer, true, tcp, hash);
				// Lê e trata 1º cabeçalho
				byte[] respostaREP = this.lerEtratarResposta(Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
				if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {
					flagErroComando = this.tratarResposta(0, respostaREP, 0, 0);
				}
				if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {
					// Envia pacote de confirmação OK da leitura da chave
					buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.VERIFICAR_PAPEL_ENROSCADO, 
															      new byte[] {0x00, (byte) 1, 0x00, (byte) 1}, new byte[4], 
															      new byte[6], (byte) FlagErroComando.DADOS_OK, CodigosComandos.END);
					Comando.enviaBuffer(buffer, true, tcp, hash);
					if (FlagErroComando.SUCESSO == flagErroComando.getErro() || FlagErroComando.DADOS_OK == flagErroComando.getErro()) {
						tcp.finalizaConexao();
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
		flagErroComando = super.tratarResposta(CodigosComandos.VERIFICAR_PAPEL_ENROSCADO, respostaREP, qtdBytesRecebidos, qtdBytesEsperada);
		return respostaREP;
	}
		
	@Override
	public FlagErroComando tratarResposta(int codigoComandoEsperado,
										  byte[] dados, int qdeRecebida, int qdeEsperada) {
		FlagErroComando flagErroComando = new FlagErroComando(FlagErroComando.SUCESSO);
		try {
			dados = Arrays.copyOfRange(dados, Protocolo.INDICE_INICIO_PARAMETRO, Protocolo.INDICE_FIM_PARAMETRO+1);
			String resultado = "";
			if (Conversor.ByteArrayToint(dados) > 0) {
				resultado = "Papel ESTÁ enroscado!";
			} else {
				resultado = "Papel NÃO ESTÁ enroscado!";
			}
			LogManager.getInstance().bytesRecebidos.add(resultado);
		} catch (Exception e) {
			flagErroComando.setErro(FlagErroComando.RETORNO_INCONSISTENTE);
		}
		
		return flagErroComando;
	}
}