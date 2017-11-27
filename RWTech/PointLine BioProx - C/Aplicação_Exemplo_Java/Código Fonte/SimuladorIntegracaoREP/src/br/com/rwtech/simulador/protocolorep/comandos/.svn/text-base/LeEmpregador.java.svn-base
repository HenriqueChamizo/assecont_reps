package br.com.rwtech.simulador.protocolorep.comandos;

import java.io.IOException;

import br.com.rwtech.simulador.conexao.Tcp;
import br.com.rwtech.simulador.pojo.Empregador;
import br.com.rwtech.simulador.protocolorep.Protocolo;
import br.com.rwtech.simulador.utils.Conversor;

public class LeEmpregador extends Comando {

	private Empregador empregador = new Empregador();

	public FlagErroComando execute(String ip, String porta, String cpf, String hash) {
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
				byte[] buffer = Comando.criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.LER_EMPREGADOR, 
						 									 new byte[4], new byte[4], bytesCpf, 
						 									 (byte) 0x00, CodigosComandos.END);
				Comando.enviaBuffer(buffer, true, tcp, hash);
				byte[] respostaREP = new byte[Protocolo.QTD_BYTES_RETORNO_LEITURA_EMPREGADOR];
				// LÍ resposta do REP
				respostaREP = super.lerResposta(tcp, Protocolo.QTD_BYTES_RETORNO_LEITURA_EMPREGADOR, hash);
				int qtdBytesRecebidos = -1;
				if (respostaREP != null) {
					qtdBytesRecebidos = respostaREP.length;
				}
				// Trata a resposta do REP
				flagErroComando = super.tratarResposta(CodigosComandos.LER_EMPREGADOR, respostaREP, qtdBytesRecebidos, Protocolo.QTD_BYTES_RETORNO_LEITURA_EMPREGADOR);
				if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {
					flagErroComando = this.tratarResposta(0, respostaREP, 0, 0);
//					debug(flagErroComando); // testes
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
	
//	private void debug(FlagErroComando flagErroComando) {
//		if (flagErroComando.getErro() == FlagErroComando.SUCESSO) {
//			System.out.println("CEI: " + empregador.getCEI());
//			System.out.println("CNPJ: " + empregador.getCNPJ());
//			System.out.println("CPF: " + empregador.getCPF());
//			System.out.println("ID: " + empregador.getID());
//			System.out.println("LOCAL: " + empregador.getLocal());
//			System.out.println("RAZ√ÉO SOCIAL: " + empregador.getRazaoSocial());
//		}
//	}
	
	@Override
	public FlagErroComando tratarResposta(int codigoComandoEsperado,
										  byte[] dados, int qdeRecebida, int qdeEsperada) {
		FlagErroComando flagErroComando = new FlagErroComando(FlagErroComando.SUCESSO);
		try {
			// Convers„o do Identificador (CNPJ/CPF)
			byte[] bytesIdentificador = new byte[Protocolo.TAMANHO_IDENTIFICADOR_EMPREGADOR];
			for (int i=0, j=Protocolo.INDICE_IDENTIFICADOR_EMPREGADOR; 
					 i < Protocolo.TAMANHO_IDENTIFICADOR_EMPREGADOR && 
					 j < Protocolo.INDICE_IDENTIFICADOR_EMPREGADOR+Protocolo.TAMANHO_IDENTIFICADOR_EMPREGADOR; 
					 i++, j++) {
				bytesIdentificador[i] = dados[j];
			}
			String identificador = Conversor.bytesIdentificadorToString(bytesIdentificador);
			
			int tipoIdentificador = Conversor.byteToInt(dados[Protocolo.INDICE_TIPO_IDENTIFICADOR_EMPREGADOR]);
			if (tipoIdentificador == 1) { // CNPJ
				empregador.setTipoIdentificador(1);
				empregador.setCNPJ(identificador);
				empregador.setCPF("");
			} else { // CPF
				empregador.setTipoIdentificador(0);
				empregador.setCNPJ("");
				empregador.setCPF(identificador);
			}
			
			// Convers„o do CEI
			byte[] bytesCEI = new byte[Protocolo.TAMANHO_CEI_EMPREGADOR];
			for (int i=0, j=Protocolo.INDICE_CEI_EMPREGADOR; 
					 i < Protocolo.TAMANHO_CEI_EMPREGADOR && 
					 j < Protocolo.INDICE_CEI_EMPREGADOR+Protocolo.TAMANHO_CEI_EMPREGADOR; 
					 i++, j++) {
				bytesCEI[i] = dados[j];
			}
			empregador.setCEI(Conversor.bytesCEIToString(bytesCEI));
			
			// Convers„o da Raz„o Social
			byte[] bytesRazaoSocial = new byte[Protocolo.TAMANHO_RAZAO_SOCIAL_EMPREGADOR];
			for (int i=0, j=Protocolo.INDICE_RAZAO_SOCIAL_EMPREGADOR; 
					 i < Protocolo.TAMANHO_RAZAO_SOCIAL_EMPREGADOR && 
					 j < Protocolo.INDICE_RAZAO_SOCIAL_EMPREGADOR+Protocolo.TAMANHO_RAZAO_SOCIAL_EMPREGADOR; 
					 i++, j++) {
				bytesRazaoSocial[i] = dados[j];
			}
			empregador.setRazaoSocial(Conversor.bytesASCIIToString(bytesRazaoSocial));
			
			// Convers„o do Local da PrestaÁ„o de ServiÁo
			byte[] bytesLocalPrestacaoServico = new byte[Protocolo.TAMANHO_LOCAL_EMPREGADOR];
			for (int i=0, j=Protocolo.INDICE_LOCAL_EMPREGADOR; 
					 i < Protocolo.TAMANHO_LOCAL_EMPREGADOR && 
					 j < Protocolo.INDICE_LOCAL_EMPREGADOR+Protocolo.TAMANHO_LOCAL_EMPREGADOR; 
					 i++, j++) {
				bytesLocalPrestacaoServico[i] = dados[j];
			}
			empregador.setLocal(Conversor.bytesASCIIToString(bytesLocalPrestacaoServico));
		} catch (Exception e) {
			flagErroComando.setErro(FlagErroComando.RETORNO_INCONSISTENTE);
		}
		
		return flagErroComando;
	}
}
