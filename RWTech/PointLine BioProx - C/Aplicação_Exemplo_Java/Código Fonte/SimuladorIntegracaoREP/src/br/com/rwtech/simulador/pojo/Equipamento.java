package br.com.rwtech.simulador.pojo;

public class Equipamento {

	private String ip;
	private String porta;
	private String chaveCriptografica;
	private String cpf;
	
	private static final String CPF_PADRAO = "11111111111";

	public Equipamento() {
	}

	public Equipamento(String ip, String porta) {
		this.ip = ip;
		this.porta = porta;
	}
	
	public String getIp() {
		return ip;
	}

	public void setIp(String ip) {
		this.ip = ip;
	}

	public String getPorta() {
		return porta;
	}

	public void setPorta(String porta) {
		this.porta = porta;
	}

	public String getChaveCriptografica() {
		return chaveCriptografica;
	}

	public void setChaveCriptografica(String chaveCriptografica) {
		this.chaveCriptografica = chaveCriptografica;
	}

	public String getCpf() {
		if (cpf == null || cpf.isEmpty()) {
			return CPF_PADRAO;
		}
		return cpf;
	}

	public void setCpf(String cpf) {
		this.cpf = cpf;
	}

}
