package br.com.rwtech.simulador.pojo;

public class Digital {

	private int dedo;
	private String template1;
	private String template2;
	private Funcionario funcionario;

	public Digital(Funcionario funcionario, int dedo, String template1, String template2) {
		this.funcionario = funcionario;
		this.dedo = dedo;
		this.template1 = template1;
		this.template2 = template2;
	}

	public Digital(Funcionario funcionario, String string) {
		this.funcionario = funcionario;
		String[] split = string.split("\\|");
		this.dedo = Integer.parseInt(split[0]);
		this.template1 = split[1];
		this.template2 = split[2];
	}

	@Deprecated
	public Digital() {
	}

	public int getDedo() {
		return dedo;
	}

	public String getTemplate1() {
		return template1;
	}

	public void setTemplate1(String template1) {
		this.template1 = template1;
	}

	public String getTemplate2() {
		return template2;
	}

	public void setTemplate2(String template2) {
		this.template2 = template2;
	}

	public Funcionario getFuncionario() {
		return funcionario;
	}

	public void setFuncionario(Funcionario funcionario) {
		this.funcionario = funcionario;
	}

	@Override
	public String toString() {
		String template = getDedo() + "|" + getTemplate1() + "|" + getTemplate2() + "#";
		return template;
	}
	
	public byte[] toByte() {
		byte[] digitais = new byte[803];
		int i = 0;
		for (int j = 0; j < template1.length(); i++, j += 2) {
			digitais[i] = (byte) Integer.parseInt(
					template1.substring(j, j + 2), 16);
		}

		for (int j = 0; j < template2.length(); i++, j += 2) {
			digitais[i] = (byte) Integer.parseInt(
					template2.substring(j, j + 2), 16);
		}

		return digitais;
	}
}
