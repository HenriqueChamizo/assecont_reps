package br.com.rwtech.simulador.protocolorep.comandos;

import java.io.IOException;

import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.Conversor;
import br.com.rwtech.simulador.utils.ProtocoloUtils;

public class ExcluiFuncionario extends Comando {

	private Tcp tcp;
	private String hash;
	
	public ExcluiFuncionario(Tcp tcp, String hash) {
		this.tcp = tcp;
		this.hash = hash;
	}
	
	public FlagErroComando execute(String ip, String porta, String cpf, String pis) {
		boolean finalizarConexao = false;
		FlagErroComando flagErroComando = new FlagErroComando();
		if (tcp == null) {
			try {
				finalizarConexao = true;
				tcp = new Tcp(ip, Integer.parseInt(porta));
			} catch (NumberFormatException | IOException e1) {
				flagErroComando.setErro(FlagErroComando.COMUNICACAO_NAO_ESTABELECIDA);
				return flagErroComando;
			}
		}
			
		try {
			try {
				byte[] bytesCpf = Conversor.cpfToByte(cpf);
				byte[] cabecalhoDadosGravaFuncionario = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.ENVIAR_FUNCIONARIO, 
																					 new byte[] { 0x00, 0x00, 0x00, 0x01 }, new byte[4], bytesCpf, 
																					 (byte) FlagErroComando.EXCLUIR, CodigosComandos.END);
				// Envia o cabeçalho
				Comando.enviaBuffer(cabecalhoDadosGravaFuncionario, true, tcp, hash);
				// Lê 1ª resposta do REP
				byte[] retornoReal = super.lerResposta(tcp, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO, hash);
				int qtdBytesRecebidos = -1;
				if (retornoReal != null) {
					qtdBytesRecebidos = retornoReal.length;
				}
				// Trata a 1ª resposta do REP
				flagErroComando = super.tratarResposta(CodigosComandos.ENVIAR_FUNCIONARIO, retornoReal, qtdBytesRecebidos, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
				if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {					
					cabecalhoDadosGravaFuncionario = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.ENVIAR_FUNCIONARIO, 
													 							 new byte[] { 0x00, 0x01, 0x00, 0x01 }, new byte[] { 0x00, 0x00, 0x00, 0x61 },
													 							 bytesCpf, (byte) FlagErroComando.DADOS_OK, CodigosComandos.END);
					
					byte checkSumCabecalho = cabecalhoDadosGravaFuncionario[cabecalhoDadosGravaFuncionario.length-2];
					byte[] dadosGravaFuncionario = criaPacoteDados(checkSumCabecalho, bytesCpf, pis);
					
					// Envia os dados do funcionário
					Comando.enviaBuffer(ProtocoloUtils.merge(cabecalhoDadosGravaFuncionario, dadosGravaFuncionario), true, tcp, hash);
					retornoReal = new byte[Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO];
					// Lê 2ª resposta do REP
					retornoReal = super.lerResposta(tcp, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO, hash);
					qtdBytesRecebidos = -1;
					if (retornoReal != null) {
						qtdBytesRecebidos = retornoReal.length;
					}
					// Trata a 2ª resposta do REP
					flagErroComando = super.tratarResposta(CodigosComandos.ENVIAR_FUNCIONARIO, retornoReal, qtdBytesRecebidos, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
				}
			} catch (Exception e) {
				flagErroComando.setErro(FlagErroComando.OCORREU_EXCECAO);
				e.printStackTrace();
			}
			if (finalizarConexao) {
				tcp.finalizaConexao();
			}
		} catch (Exception e) {
			flagErroComando.setErro(FlagErroComando.ERRO_FINALIZAR_CONEXAO);
			e.printStackTrace();
		}
		
		return flagErroComando;
	}
	
	private byte[] criaPacoteDados(byte checkSumCabecalho, byte[] cpf, String pisString) {
		byte[] nome = Conversor.stringToByteArray("nome", 52); // Campo Nome 52 bytes;
		byte[] pis = Conversor.pisToByte(Conversor.SomenteNumeros(pisString)); // Campo PIS 6 bytes;
		byte[] reservado = new byte[4]; // Campo Reservado 4 bytes; 
		byte[] cartao = Conversor.stringParaBytes("0", 20); // Campo Cartão 20 bytes;
		byte[] codigo = Conversor.stringParaBytes("0", 3); // Campo Código 3 bytes;
		byte[] mestre = Conversor.stringParaBytes("0", 1); // Campo Mestre 1 byte;
		byte[] senha = Conversor.stringParaBytes("0", 3); // Campo Senha 3 bytes;
		byte[] verifica1ToN = new byte[1]; // Campo Verifica 1 byte;
		byte[] cmp = new byte[1]; // Campo cmp 1 byte;
		byte[] requisicao = new byte[]{};
		requisicao = ProtocoloUtils.merge(nome, pis, reservado, cartao, codigo, mestre, senha, verifica1ToN, cmp, cpf);
		byte checksum = ProtocoloUtils.getChecksum(ProtocoloUtils.merge(requisicao, new byte[] { checkSumCabecalho, CodigosComandos.END }));
		requisicao = ProtocoloUtils.merge(requisicao, new byte[] { checksum });
		return requisicao;
	}
}
