package br.com.rwtech.simulador.protocolorep.comandos;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.LinkedList;
import java.util.List;

import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.pojo.Funcionario;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.Conversor;

public class LeFuncionario extends Comando {

	private Funcionario funcionarioLido;
	private String hash;
	private Tcp tcp;
	private FlagErroComando flagErroComando;
	private List<Funcionario> listaFuncionariosLidos;
	private List<String> listaPisNaoEncontrados;
	private int qtdPisASerLido;

	public LeFuncionario(String hash) {
		this.hash = hash;
	}
	
	public FlagErroComando execute(String ip, String porta, List<String> listPIS) {
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
				listaFuncionariosLidos = new ArrayList<>();
				if (listPIS == null || listPIS.isEmpty()) {
					leTodosFuncionarios();
				} else {
					listaPisNaoEncontrados = new LinkedList<>();
					qtdPisASerLido = listPIS.size();
					int i = 0;
					while (i < qtdPisASerLido) {
						leFuncionarioPorPIS(listPIS.get(i));
						i++;
					}
					listPIS.clear();
					listPIS.addAll(listaPisNaoEncontrados);
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
	
	private void leFuncionarioPorPIS(String pis) throws Exception {
		byte[] buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.LER_FUNCIONARIO, 
													 Conversor.pisToByte(pis), new byte[2], new byte[6], (byte) 0x00, CodigosComandos.END);
		Comando.enviaBuffer(buffer, true, tcp, hash);
		// Lê e trata 1º cabeçalho
		byte[] respostaREP = this.lerEtratarResposta(Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
		if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {
			flagErroComando.setErro(FlagErroComando.DADOS_OK);
			// Lê e trata cabeçalho dos dados
			respostaREP = this.lerEtratarResposta(Protocolo.QTD_BYTES_FUNCIONARIO);
			if (flagErroComando.getErro() == FlagErroComando.DADOS_OK) {
				// Lê os dados do funcionário 
				respostaREP = Arrays.copyOfRange(respostaREP, Protocolo.QTD_BYTES_CABECALHO_DADOS, respostaREP.length);
				int qtdBytesRecebidos = -1;
				if (respostaREP != null) {
					qtdBytesRecebidos = respostaREP.length;
				}
				if (Protocolo.QTD_BYTES_FUNCIONARIO - Protocolo.QTD_BYTES_CABECALHO_DADOS == qtdBytesRecebidos) {
					flagErroComando = this.tratarResposta(0, respostaREP, 0, 0);
					// Envia pacote de confirmação OK da leitura de funcionário
					buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.LER_FUNCIONARIO, 
															      new byte[] {0x00, (byte) 1, 0x00, (byte) 1}, new byte[4], 
															      new byte[6], (byte) FlagErroComando.DADOS_OK, CodigosComandos.END);
					Comando.enviaBuffer(buffer, true, tcp, hash);
					if (FlagErroComando.SUCESSO == flagErroComando.getErro() || FlagErroComando.DADOS_OK == flagErroComando.getErro()) {
						listaFuncionariosLidos.add(funcionarioLido);
					}
				}
			}
		} else if (flagErroComando.getErro() == FlagErroComando.PIS_INEXISTENTE) {
			listaPisNaoEncontrados.add(pis);
		}
	}
	
	private void leTodosFuncionarios() throws Exception {
		byte[] buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.LER_FUNCIONARIO, new byte[6], 
													 new byte[2], new byte[6], (byte) 0x00, CodigosComandos.END);
		Comando.enviaBuffer(buffer, true, tcp, hash);
		// Lê e trata 1º cabeçalho
		byte[] respostaREP = this.lerEtratarResposta(Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
		if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {
			int qtdFuncionarios = Conversor.ByteArrayToint(Arrays.copyOfRange(respostaREP, 
																				   Protocolo.INDICE_INICIO_PARAMETRO, 
																				   Protocolo.INDICE_FIM_PARAMETRO+1));
			int i = 1;
			byte[] total;
			byte[] atual;
			while(i <= qtdFuncionarios 
					&& (FlagErroComando.SUCESSO == flagErroComando.getErro() || FlagErroComando.DADOS_OK == flagErroComando.getErro())) {
				// Lê e trata cabeçalho dos dados
				respostaREP = this.lerEtratarResposta(Protocolo.QTD_BYTES_FUNCIONARIO);
				if (flagErroComando.getErro() == FlagErroComando.DADOS_OK) {
					// Lê os dados do funcionário 
					respostaREP = Arrays.copyOfRange(respostaREP, Protocolo.QTD_BYTES_CABECALHO_DADOS, respostaREP.length);
					int qtdBytesRecebidos = -1;
					if (respostaREP != null) {
						qtdBytesRecebidos = respostaREP.length;
					}
					if (Protocolo.QTD_BYTES_FUNCIONARIO - Protocolo.QTD_BYTES_CABECALHO_DADOS == qtdBytesRecebidos) {
						flagErroComando = this.tratarResposta(0, respostaREP, 0, 0);
						if (FlagErroComando.SUCESSO == flagErroComando.getErro() || FlagErroComando.DADOS_OK == flagErroComando.getErro()) {
							listaFuncionariosLidos.add(funcionarioLido);
						}
					}
					// Envia pacote de confirmação OK da leitura de funcionário
					total = Conversor.intToByteArray2(qtdFuncionarios);
					atual = Conversor.intToByteArray2(i);
					buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.LER_FUNCIONARIO, 
															      new byte[] {total[0], total[1], atual[0], atual[1]}, new byte[4], 
															      new byte[6], (byte) FlagErroComando.DADOS_OK, CodigosComandos.END);
					Comando.enviaBuffer(buffer, true, tcp, hash);
				}
				i++;
			}
		}
	}
	
	private byte[] lerEtratarResposta(int qtdBytesEsperada) throws Exception {
		// Lê resposta do REP
		byte[] respostaREP = super.lerResposta(tcp, qtdBytesEsperada, hash);
		int qtdBytesRecebidos = -1;
		if (respostaREP != null) {
			qtdBytesRecebidos = respostaREP.length;
		}
		// Trata a resposta do REP
		flagErroComando = super.tratarResposta(CodigosComandos.LER_FUNCIONARIO, respostaREP, qtdBytesRecebidos, qtdBytesEsperada);
		return respostaREP;
	}
	
	@Override
	public FlagErroComando tratarResposta(int codigoComandoEsperado,
										  byte[] dados, int qdeRecebida, int qdeEsperada) {
		FlagErroComando flagErroComando = new FlagErroComando(FlagErroComando.SUCESSO);
		try {
			funcionarioLido = new Funcionario();
			// Conversão do Nome
			byte[] bytesNome = new byte[Protocolo.TAMANHO_NOME_FUNCIONARIO];
			for (int i=0, j=Protocolo.INDICE_NOME_FUNCIONARIO; 
					 i < Protocolo.TAMANHO_NOME_FUNCIONARIO && 
					 j < Protocolo.INDICE_NOME_FUNCIONARIO+Protocolo.TAMANHO_NOME_FUNCIONARIO; 
					 i++, j++) {
				bytesNome[i] = dados[j];
			}
			funcionarioLido.setNome(Conversor.bytesASCIIToString(bytesNome));
			
			// Conversão do PIS
			byte[] bytesPIS = new byte[Protocolo.TAMANHO_PIS_FUNCIONARIO];
			for (int i=0, j=Protocolo.INDICE_PIS_FUNCIONARIO; 
					 i < Protocolo.TAMANHO_PIS_FUNCIONARIO && 
					 j < Protocolo.INDICE_PIS_FUNCIONARIO+Protocolo.TAMANHO_PIS_FUNCIONARIO; 
					 i++, j++) {
				bytesPIS[i] = dados[j];
			}
			funcionarioLido.setPis(Conversor.BCDtoString(bytesPIS));
			
			// Conversão do ID BIO
			byte[] bytesIDBio = new byte[Protocolo.TAMANHO_ID_BIO];
			for (int i=0, j=Protocolo.INDICE_ID_BIO; 
					 i < Protocolo.TAMANHO_ID_BIO && 
					 j < Protocolo.INDICE_ID_BIO+Protocolo.TAMANHO_ID_BIO; 
					 i++, j++) {
				bytesIDBio[i] = dados[j];
			}
			
			// Conversão do Cartão
			byte[] bytesCartao = new byte[Protocolo.TAMANHO_CARTAO_FUNCIONARIO];
			for (int i=0, j=Protocolo.INDICE_CARTAO_FUNCIONARIO; 
					 i < Protocolo.TAMANHO_CARTAO_FUNCIONARIO && 
					 j < Protocolo.INDICE_CARTAO_FUNCIONARIO+Protocolo.TAMANHO_CARTAO_FUNCIONARIO; 
					 i++, j++) {
				bytesCartao[i] = dados[j];
			}
			funcionarioLido.setCartao(Conversor.bytesASCIIToString(bytesCartao));
			
			// Conversão do Código
			byte[] bytesCodigo = new byte[Protocolo.TAMANHO_CODIGO_FUNCIONARIO];
			for (int i=0, j=Protocolo.INDICE_CODIGO_FUNCIONARIO; 
					 i < Protocolo.TAMANHO_CODIGO_FUNCIONARIO && 
					 j < Protocolo.INDICE_CODIGO_FUNCIONARIO+Protocolo.TAMANHO_CODIGO_FUNCIONARIO; 
					 i++, j++) {
				bytesCodigo[i] = dados[j];
			}
			funcionarioLido.setCodigo(Conversor.bytesCodigoToString(bytesCodigo));
			
			// Conversão do Mestre
			funcionarioLido.setTipoUsuario(Conversor.byteToInt(dados[Protocolo.INDICE_MESTRE_FUNCIONARIO]));
			
			// Conversão da Senha
			byte[] bytesSenha = new byte[Protocolo.TAMANHO_SENHA_FUNCIONARIO];
			for (int i=0, j=Protocolo.INDICE_SENHA_FUNCIONARIO; 
					 i < Protocolo.TAMANHO_SENHA_FUNCIONARIO && 
					 j < Protocolo.INDICE_SENHA_FUNCIONARIO+Protocolo.TAMANHO_SENHA_FUNCIONARIO; 
					 i++, j++) {
				bytesSenha[i] = dados[j];
			}
			funcionarioLido.setSenha(Conversor.BCDtoString(bytesSenha));
			
			// Conversão do Verificar 1 pra N
			funcionarioLido.setVerifica(Conversor.byteToInt(dados[Protocolo.INDICE_VERIFICAR_1_PRA_N]));
			
		} catch (Exception e) {
			flagErroComando.setErro(FlagErroComando.RETORNO_INCONSISTENTE);
			e.printStackTrace();
		}
		
		return flagErroComando;
	}
}
