package br.com.rwtech.simulador.protocolorep.comandos;

import java.io.IOException;

import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.Conversor;

public class EnviaHorarioVerao extends Comando {
	
	private String ip;
	private String porta;
	private String cpf;
	private String hash;
	
	public EnviaHorarioVerao(String ip, String porta, String cpf, String hash) {
		this.ip = ip;
		this.porta = porta;
		this.cpf = cpf;
		this.hash = hash;
	}
	
	public FlagErroComando execute(String inicioHorarioVerao, String fimHorarioVerao) {
		FlagErroComando flagErroComando = new FlagErroComando();
		Tcp tcp = null;
		try {
			tcp = new Tcp(ip, Integer.parseInt(porta));
		} catch (NumberFormatException | IOException e1) {
			flagErroComando.setErro(FlagErroComando.COMUNICACAO_NAO_ESTABELECIDA);
			return flagErroComando;
		}
		
		String[] inicioSplitted = inicioHorarioVerao.split("/");
		int diaInicio = Integer.parseInt(inicioSplitted[0]);
		int mesInicio = Integer.parseInt(inicioSplitted[1]);
		int anoInicio = Integer.parseInt(inicioSplitted[2]);
		int  inicioHV = 0x00000000;
		inicioHV = inicioHV | (0 << 24);
		inicioHV = inicioHV | (diaInicio << 16);
		inicioHV = inicioHV | (mesInicio << 12);
		inicioHV = inicioHV | anoInicio;
		byte[] bytesInicioHV = Conversor.intToByteArray(inicioHV, 4);
		
		String[] fimSplitted = fimHorarioVerao.split("/");
		int diaFim = Integer.parseInt(fimSplitted[0]);
		int mesFim = Integer.parseInt(fimSplitted[1]);
		int anoFim = Integer.parseInt(fimSplitted[2]);
		int  fimHV = 0x00000000;
		fimHV = fimHV | (0 << 24);
		fimHV = fimHV | (diaFim << 16);
		fimHV = fimHV | (mesFim << 12);
		fimHV = fimHV | anoFim;
		byte[] bytesFimHV = Conversor.intToByteArray(fimHV, 4);
		
		byte[] buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.ENVIAR_HORARIO_VERAO, 
													 bytesInicioHV, bytesFimHV, Conversor.cpfToByte(cpf), 
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
				flagErroComando = super.tratarResposta(CodigosComandos.ENVIAR_HORARIO_VERAO, respostaREP, qtdBytesRecebidos, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
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
