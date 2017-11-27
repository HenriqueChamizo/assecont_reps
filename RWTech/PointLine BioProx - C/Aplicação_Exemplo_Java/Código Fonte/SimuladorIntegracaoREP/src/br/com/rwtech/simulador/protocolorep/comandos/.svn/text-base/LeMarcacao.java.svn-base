package br.com.rwtech.simulador.protocolorep.comandos;

import java.io.IOException;
import java.util.Arrays;
import java.util.Formatter;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Set;

import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.criptografia.AES;
import br.com.rwtech.simulador.pojo.MarcacaoPonto;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.MaskProcessor;
import br.com.rwtech.simulador.utils.Conversor;

public class LeMarcacao extends Comando {

	private String hash;
	private Tcp tcp;
	private FlagErroComando flagErroComando;
	private MarcacaoPonto marcacaoPonto;
	private List<MarcacaoPonto> listaMarcacoes;
	private Set<Integer> setNsr = new HashSet<>();
	private int totalPacotes = 0;

	public LeMarcacao(String hash) {
		this.hash = hash;
	}
	
	public List<MarcacaoPonto> getListaMarcacoes() {
		return listaMarcacoes;
	}
	
	public FlagErroComando execute(String ip, String porta, int nsr) {
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
				/** Quando é informado um NSR maior do que o último gravado no REP,
				 * o REP retorna o flag 0x96 (O Frame recebido contém erro).
				 **/
				byte[] buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.LER_MARCACAO, 
															 Conversor.intToByteArray(nsr, 4),
															 Conversor.intToByteArray(Protocolo.TAMANHO_PACOTE_MARCACOES, 4),
						 									 new byte[6], (byte) 0x00, CodigosComandos.END);
				Comando.enviaBuffer(buffer, true, tcp, hash);
				// Lê e trata 1º cabeçalho
				byte[] respostaREP = this.lerEtratarResposta(Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
				byte[] totalMarcacoes = Arrays.copyOfRange(respostaREP, Protocolo.INDICE_INICIO_PARAMETRO, Protocolo.INDICE_INICIO_PARAMETRO+4);
				int totalMarcacoesInt = Conversor.ByteArrayToint(totalMarcacoes);
				// Cálculo necessário devido ao último pacote ser de tamanho variável
				int qtdMarcacoesUltimoPacote = totalMarcacoesInt % Protocolo.TAMANHO_PACOTE_MARCACOES;
				int qtdBytesASeremLidos = 0; 
				if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {
					flagErroComando.setErro(FlagErroComando.DADOS_OK);
					int pacoteAtual = -1;
					byte[] atual;
					listaMarcacoes = new LinkedList<>();
					while(pacoteAtual < totalPacotes 
							&& (FlagErroComando.SUCESSO == flagErroComando.getErro() || FlagErroComando.DADOS_OK == flagErroComando.getErro())) {
						// Lê e trata cabeçalho dos dados
						if (totalMarcacoesInt < Protocolo.TAMANHO_PACOTE_MARCACOES 
								|| (pacoteAtual != -1 && (pacoteAtual+1) == totalPacotes && qtdMarcacoesUltimoPacote > 0)) { // último pacote a ser recebido
							qtdBytesASeremLidos = AES.defineTamanhoPacote(Protocolo.QTD_BYTES_CABECALHO_DADOS + Protocolo.QTD_BYTES_MARCACAO * qtdMarcacoesUltimoPacote);
						} else {
							qtdBytesASeremLidos = Protocolo.QTD_BYTES_PACOTES_MARCACOES;
						}
						respostaREP = this.lerEtratarResposta(qtdBytesASeremLidos);
						if (flagErroComando.getErro() == FlagErroComando.DADOS_OK) {
							// Verificando o total de pacotes a serem lidos do REP
							byte[] total = Arrays.copyOfRange(respostaREP, Protocolo.INDICE_INICIO_PARAMETRO, Protocolo.INDICE_INICIO_PARAMETRO+2);
							totalPacotes = Conversor.ByteArrayToint(total); 
							// Verificando o pacote atual
							atual = Arrays.copyOfRange(respostaREP, Protocolo.INDICE_FIM_PARAMETRO-1, Protocolo.INDICE_FIM_PARAMETRO+1);
							pacoteAtual = Conversor.ByteArrayToint(atual);
							
							// Lê os pacotes de marcação de ponto 
							respostaREP = Arrays.copyOfRange(respostaREP, Protocolo.QTD_BYTES_CABECALHO_DADOS, respostaREP.length);
							int qtdBytesRecebidos = -1;
							if (respostaREP != null) {
								qtdBytesRecebidos = respostaREP.length;
							}
							if (qtdBytesASeremLidos - Protocolo.QTD_BYTES_CABECALHO_DADOS == qtdBytesRecebidos) {
								flagErroComando = this.tratarResposta(0, respostaREP, 0, 0);
							}
							if (flagErroComando.getErro() == FlagErroComando.FIM_LEITURA_MARCACOES) {
								atual[0] = total[0];
								atual[1] = total[1];
							}
							// Envia comando de leitura do(s) próximo(s) pacote(s) de marcação de ponto
							buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.LER_MARCACAO, 
																	      new byte[] {total[0], total[1], atual[0], atual[1]}, new byte[4], 
																	      new byte[6], (byte) FlagErroComando.DADOS_OK, CodigosComandos.END);
							
							Comando.enviaBuffer(buffer, true, tcp, hash);
						}
					}
					if (flagErroComando.getErro() == FlagErroComando.DADOS_OK
							|| flagErroComando.getErro() == FlagErroComando.FIM_LEITURA_MARCACOES) {
						flagErroComando.setErro(FlagErroComando.SUCESSO);
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
		flagErroComando = super.tratarResposta(CodigosComandos.LER_MARCACAO, respostaREP, qtdBytesRecebidos, qtdBytesEsperada);
		return respostaREP;
	}
	
	@Override
	public FlagErroComando tratarResposta(int codigoComandoEsperado,
										  byte[] dados, int qdeRecebida, int qdeEsperada) {
		FlagErroComando flagErroComando = new FlagErroComando(FlagErroComando.SUCESSO);
		try {
			int inicioCampo;
			int fimCampo;
			int offset;
			int qtdBytes = dados.length;
			int ultimoByte = dados[qtdBytes-1];
			while (ultimoByte == -1 || ultimoByte == 0) {
				dados = Arrays.copyOfRange(dados, 0, qtdBytes-1);
				qtdBytes = dados.length;
				ultimoByte = dados[qtdBytes-1];
			}
			int qtdMarcacoes = qtdBytes / Protocolo.QTD_BYTES_MARCACAO;
			for (int i=0; i < qtdMarcacoes; i++) {
				marcacaoPonto = new MarcacaoPonto();
				offset = Protocolo.QTD_BYTES_MARCACAO * i;
				inicioCampo = 0 + offset;
				fimCampo = 4 + offset;
				marcacaoPonto.setNumEvento(Conversor.ByteArrayToint(Arrays.copyOfRange(dados, inicioCampo, fimCampo))); // 4 bytes
				inicioCampo = 4 + offset;
				fimCampo = 8 + offset;
				marcacaoPonto.setNsr(Conversor.ByteArrayToint(Arrays.copyOfRange(dados, inicioCampo, fimCampo))); // 4 bytes
				inicioCampo = 8 + offset;
				fimCampo = 13 + offset;
				marcacaoPonto.setPIS(MaskProcessor.zeroFill(Conversor.bytesPISToString(Arrays.copyOfRange(dados, inicioCampo, fimCampo)), 
															MaskProcessor.PIS_LENGTH)); // 5 bytes
				inicioCampo = 0 + offset;
				fimCampo = 4 + offset;
				marcacaoPonto.setData(new Formatter().format("%02d/%02d/%04d", 
								   Conversor.byteToInt(Arrays.copyOfRange(dados, 
																			   13 + offset, 
																			   14 + offset)[0]), // dia 1 byte 
								   Conversor.byteToInt(Arrays.copyOfRange(dados, 
										   									   14 + offset, 
										   									   15 + offset)[0]), // mês 1 byte 
								   Conversor.ByteArrayToint(Arrays.copyOfRange(dados, 
										   									   15 + offset, 
										   									   17 + offset))) // ano 2 bytes 
										   									   		.toString());
				marcacaoPonto.setHora(new Formatter().format("%02d:%02d:00", 
								   Conversor.byteToInt(Arrays.copyOfRange(dados, 
										   									   17 + offset, 
										   									   18 + offset)[0]), // hora 1 byte
								   Conversor.byteToInt(Arrays.copyOfRange(dados, 
										   									   18 + offset, 
										   									   19 + offset)[0])) // minuto 1 byte
										   									   		.toString());
				if (marcacaoPonto.getNsr() > 0) {
					/**
					 * Isto foi necessário pelo seguinte motivo:
					 * embora o REP retorne somente registros do tipo 3 (quantidade total de 700, por exemplo), 
					 * ele faz o loop considerando todos os tipos de registros, não só do tipo 3. 
					 * Sendo assim, quando o REP termina de enviar os registros do tipo 3,
					 * ele começa a repetir a lista de registros do tipo 3 já enviada,
					 * até completar o loop de todos os tipos de registros (quantidade total de 1000, por exemplo).
					 * Por isso foi criada uma lógica com o código "FIM_LEITURA_MARCACOES",
					 * a qual força a finalização da leitura de marcações, 
					 * eliminando um loop desnecessário (faz considerando 700 em vez de 1000).
					 * **/
					if (setNsr.contains(marcacaoPonto.getNsr())) {
						totalPacotes = -1;
						flagErroComando.setErro(FlagErroComando.FIM_LEITURA_MARCACOES);
						break;
					} else {
						setNsr.add(marcacaoPonto.getNsr());
						listaMarcacoes.add(marcacaoPonto);
					}
				}
			}
			
		} catch (Exception e) {
			flagErroComando.setErro(FlagErroComando.RETORNO_INCONSISTENTE);
			e.printStackTrace();
		}
		
		return flagErroComando;
	}
}
