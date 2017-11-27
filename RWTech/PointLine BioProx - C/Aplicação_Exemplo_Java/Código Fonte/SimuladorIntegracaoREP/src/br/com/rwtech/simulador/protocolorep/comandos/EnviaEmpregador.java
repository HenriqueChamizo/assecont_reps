package br.com.rwtech.simulador.protocolorep.comandos;

import java.io.IOException;

import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.pojo.Empregador;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.Conversor;
import br.com.rwtech.simulador.utils.ProtocoloUtils;

public class EnviaEmpregador extends Comando {

	public FlagErroComando execute(String ip, String porta, String cpf, String hash, Empregador empregador) {
		FlagErroComando flagErroComando = new FlagErroComando();
		Tcp tcp = null;
		try {
			tcp = new Tcp(ip, Integer.parseInt(porta));
		} catch (NumberFormatException | IOException e1) {
			flagErroComando.setErro(FlagErroComando.COMUNICACAO_NAO_ESTABELECIDA);
			return flagErroComando;
		}
			
		try {
			try {
				byte[] bytesCpf = Conversor.cpfToByte(cpf);
				byte[] cabecalhoDadosGravaEmpregador = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.ENVIAR_EMPREGADOR, 
																					 new byte[] { 0x00, 0x00, 0x00, 0x01 }, new byte[4], bytesCpf, 
																					 (byte) FlagErroComando.ADICIONAR_SUBSTITUIR, CodigosComandos.END);
				// Envia o cabeçalho
				Comando.enviaBuffer(cabecalhoDadosGravaEmpregador, true, tcp, hash);
				// LÃª 1ª resposta do REP
				byte[] retornoReal = super.lerResposta(tcp, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO, hash);
				int qtdBytesRecebidos = -1;
				if (retornoReal != null) {
					qtdBytesRecebidos = retornoReal.length;
				}
				// Trata a 1ª resposta do REP
				flagErroComando = super.tratarResposta(CodigosComandos.ENVIAR_EMPREGADOR, retornoReal, qtdBytesRecebidos, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
				if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {					
					cabecalhoDadosGravaEmpregador = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.ENVIAR_EMPREGADOR, 
													 							 new byte[] { 0x00, 0x01, 0x00, 0x01 }, new byte[] { 0x00, 0x00, 0x01, 0x0C },
													 							 bytesCpf, (byte) FlagErroComando.DADOS_OK, CodigosComandos.END);
					
					byte checkSumCabecalho = cabecalhoDadosGravaEmpregador[cabecalhoDadosGravaEmpregador.length-2];
					byte[] dadosGravaEmpregador = criaPacoteDados(checkSumCabecalho, bytesCpf, empregador);
					
					// Envia os dados do empregador
					Comando.enviaBuffer(ProtocoloUtils.merge(cabecalhoDadosGravaEmpregador, dadosGravaEmpregador), true, tcp, hash);
					retornoReal = new byte[Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO];
					// LÃª 2ª resposta do REP
					retornoReal = super.lerResposta(tcp, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO, hash);
					qtdBytesRecebidos = -1;
					if (retornoReal != null) {
						qtdBytesRecebidos = retornoReal.length;
					}
					// Trata a 2ª resposta do REP
					flagErroComando = super.tratarResposta(CodigosComandos.ENVIAR_EMPREGADOR, retornoReal, qtdBytesRecebidos, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
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
	
	private byte[] criaPacoteDados(byte checkSumCabecalho, byte[] cpf, Empregador empregador) {
		byte tipoId;
		byte[] identificador;
		if (empregador.getTipoIdentificador() == 1) {
			tipoId = 0x01;
			identificador = Conversor.cnpjToByte(Conversor.SomenteNumeros(Conversor.SomenteNumeros(empregador.getCNPJ()))); // Campo Identificador 6 bytes
		} else {
			tipoId = 0x02;
			identificador = Conversor.cpfToByte(Conversor.SomenteNumeros(Conversor.SomenteNumeros(empregador.getCPF()))); // Campo Identificador 6 bytes
		}
		byte[] requisicao = { tipoId }; // Campo Tipo de Identificador 1 byte
		byte[] cei = Conversor.ceiToByte(Conversor.SomenteNumeros(empregador.getCEI())); // Campo CEI 5 bytes
		byte[] razaoSocial = Conversor.stringToByteArray(empregador.getRazaoSocial(), 150); // Campo Razão Social 150 bytes
		byte[] localPrestServ = Conversor.stringToByteArray(empregador.getLocal(), 100); // Campo Local 100 bytes
		requisicao = ProtocoloUtils.merge(requisicao, identificador, cei, razaoSocial, localPrestServ, cpf);
		byte checksum = ProtocoloUtils.getChecksum(ProtocoloUtils.merge(requisicao, new byte[] { checkSumCabecalho, CodigosComandos.END }));
		requisicao = ProtocoloUtils.merge(requisicao, new byte[] { checksum });
		return requisicao;
	}
}
