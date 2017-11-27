package br.com.rwtech.simulador.protocolorep.comandos;

import java.io.IOException;

import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.Conversor;
import br.com.rwtech.simulador.utils.ProtocoloUtils;

public class EnviaDataHora extends Comando {
	
	public FlagErroComando execute(String ip, String porta, String cpf, String hash) {
		FlagErroComando flagErroComando = new FlagErroComando();
		Tcp tcp = null;
		try {
			tcp = new Tcp(ip, Integer.parseInt(porta));
		} catch (NumberFormatException | IOException e1) {
			flagErroComando.setErro(FlagErroComando.COMUNICACAO_NAO_ESTABELECIDA);
			return flagErroComando;
		}
		byte[] buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.ENVIAR_DATA_HORA, 
													 ProtocoloUtils.data(), ProtocoloUtils.horario(), Conversor.cpfToByte(cpf), 
													 (byte) 0x00, CodigosComandos.END);
		try {
			Comando.enviaBuffer(buffer, true, tcp, hash);
			try {
				// Lê resposta do REP
				byte[] respostaREP = super.lerResposta(tcp, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO, hash);
				int qtdBytesRecebidos = -1;
				if (respostaREP != null) {
					qtdBytesRecebidos = respostaREP.length;
				}
				// Trata a resposta do REP
				flagErroComando = super.tratarResposta(CodigosComandos.ENVIAR_DATA_HORA, respostaREP, qtdBytesRecebidos, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
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
	
}
