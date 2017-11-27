package br.com.rwtech.simulador.pojo;

public class Empregador {

	private String cpf = "";
	private String cnpj = "";
	private int tipoIdentificador;
	private String cei;
	private String razaoSocial;
	private String local;

	public int getTipoIdentificador() {
		return tipoIdentificador;
	}

	public void setTipoIdentificador(int tipoIdentificador) {
		this.tipoIdentificador = tipoIdentificador;
	}

	public String getCEI() {
		return cei;
	}

	public void setCEI(String CEI) {
		this.cei = CEI;
	}

	public String getRazaoSocial() {
		return razaoSocial;
	}

	public void setRazaoSocial(String razaoSocial) {
		this.razaoSocial = razaoSocial;
	}

	public String getLocal() {
		return local;
	}

	public void setLocal(String local) {
		this.local = local;
	}

	public void setCPF(String cpf) {
		this.cpf = cpf;
	}

	public String getCPF() {
		return cpf;
	}

	public void setCNPJ(String cnpj) {
		this.cnpj = cnpj;
	}

	public String getCNPJ() {
		return cnpj;
	}
}
