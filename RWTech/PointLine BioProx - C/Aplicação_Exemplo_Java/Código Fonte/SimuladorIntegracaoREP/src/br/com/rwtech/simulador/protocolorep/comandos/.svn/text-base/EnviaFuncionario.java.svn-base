package br.com.rwtech.simulador.protocolorep.comandos;

import java.io.IOException;
import java.util.List;

import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.pojo.Funcionario;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.Conversor;
import br.com.rwtech.simulador.utils.ProtocoloUtils;

public class EnviaFuncionario extends Comando {

	private String hash;
	private String cpf;
	private FlagErroComando flagErroComando;
	private Tcp tcp;
	
	public EnviaFuncionario(String hash, String cpf) {
		this.hash = hash;
		this.cpf = cpf;
	}
	
	public FlagErroComando execute(String ip, String porta, List<Funcionario> funcionarios) {
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
				int totalFuncionarios = funcionarios.size();
				int funcionarioAtual = 1;
				int flagErro = FlagErroComando.SUCESSO;
				while (funcionarioAtual <= totalFuncionarios
						 && (FlagErroComando.SUCESSO == flagErroComando.getErro() || FlagErroComando.DADOS_OK == flagErroComando.getErro())) {
					Funcionario funcionario = funcionarios.get(funcionarioAtual-1);
					// Exclusão do funcionário antes de adicioná-lo no REP
					ExcluiFuncionario excluiFuncionario = new ExcluiFuncionario(tcp, hash);
					flagErroComando = excluiFuncionario.execute(ip, porta, cpf, funcionario.getPis());
					// Envio do funcionário para o REP
					if (flagErro == FlagErroComando.SUCESSO || flagErro == FlagErroComando.IDENTIFICADOR_RECUSADO || flagErro == FlagErroComando.PIS_INEXISTENTE) {
						byte[] bytesCPF = Conversor.cpfToByte(cpf);
						byte[] cabecalhoDadosGravaFuncionario = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.ENVIAR_FUNCIONARIO, 
																							 new byte[] { 0x00, 0x00, 0x00, 0x01 }, 
																							 new byte[4], bytesCPF, 
																							 (byte) FlagErroComando.ADICIONAR, CodigosComandos.END);
						// Envia o cabeçalho
						Comando.enviaBuffer(cabecalhoDadosGravaFuncionario, true, tcp, hash);
						// Lê e trata 1º cabeçalho
						lerEtratarResposta(Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
						if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {
							cabecalhoDadosGravaFuncionario = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.ENVIAR_FUNCIONARIO,  
																						  new byte[] { 0x00, 0x01, 0x00, 0x01 },	
																						  new byte[] { 0x00, 0x00, 0x00, 0x61 },
																						  bytesCPF, (byte) FlagErroComando.DADOS_OK, CodigosComandos.END);
							
							byte checkSumCabecalho = cabecalhoDadosGravaFuncionario[Protocolo.QTD_BYTES_CABECALHO_DADOS-2];
							byte[] dadosGravaFuncionario = criaPacoteDados(checkSumCabecalho, funcionario);
							
							// Envia os dados do funcionário
							Comando.enviaBuffer(ProtocoloUtils.merge(cabecalhoDadosGravaFuncionario, dadosGravaFuncionario), true, tcp, hash);
							// Lê e trata 2ª resposta do REP
							lerEtratarResposta(Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
						}
					}
					funcionarioAtual++;
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
		flagErroComando = super.tratarResposta(CodigosComandos.ENVIAR_FUNCIONARIO, respostaREP, qtdBytesRecebidos, qtdBytesEsperada);
		return respostaREP;
	}
	
	private byte[] criaPacoteDados(byte checkSumCabecalho, Funcionario funcionario) {		
		byte[] nome = Conversor.stringToByteArray(funcionario.getNome(), 52); // Campo Nome 52 bytes;
		byte[] pis = Conversor.pisToByte(Conversor.SomenteNumeros(funcionario.getPis())); // Campo PIS 6 bytes;
		byte[] reservado = new byte[4]; // Campo Reservado 4 bytes;
		String cartaoString = funcionario.getCartao(); 
		byte[] cartao = Conversor.stringParaBytes(cartaoString != null && !cartaoString.isEmpty() ? cartaoString : "0", 20); // Campo Cartão 20 bytes;
		byte[] codigo = funcionario.getCodigoArray(); // Campo Código 3 bytes;
		byte[] mestre = new byte[1]; // Campo Mestre 1 byte;
		String senhaStr = funcionario.getSenha();
		byte[] senha = new byte[3];
		if (senhaStr != null && !senhaStr.equals("000000") && senhaStr.length() < 7) {
			senha = funcionario.getSenhaArray(); // Campo Senha 3 bytes;
		}
		byte[] verifica1ToN = new byte[1]; // Campo Verifica 1 byte;
		byte[] cmp = new byte[1]; // Campo adicional 1 byte;
		byte[] requisicao = new byte[]{};
		requisicao = ProtocoloUtils.merge(nome, pis, reservado, cartao, codigo, mestre, senha, verifica1ToN, cmp);
		byte checksum = ProtocoloUtils.getChecksum(ProtocoloUtils.merge(requisicao, new byte[] { checkSumCabecalho, CodigosComandos.END }));
		requisicao = ProtocoloUtils.merge(requisicao, new byte[] { checksum });
		return requisicao;
	}
}