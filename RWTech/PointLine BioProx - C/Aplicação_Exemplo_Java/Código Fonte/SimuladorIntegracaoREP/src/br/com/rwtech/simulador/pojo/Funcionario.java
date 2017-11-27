package br.com.rwtech.simulador.pojo;

import java.util.ArrayList;
import java.util.List;

import br.com.rwtech.simulador.utils.Conversor;
import br.com.rwtech.simulador.utils.MaskProcessor;

public class Funcionario {
	
	private String codigo;
	private String nome;
	private String pis;
	private String cartao;
	private Integer tipoUsuario;
	private String senha;
	private Integer verifica;
	private List<Digital> digitais = new ArrayList<Digital>();
	
	public Funcionario() {}

	public Funcionario(String codigo, String nome, String pis,
								  String tp, Integer tipoUsuario, String senha) {
		this.codigo = codigo;
		this.nome = nome;
		this.setPis(pis);
		this.cartao = tp;
		this.tipoUsuario = tipoUsuario;
		this.senha = senha;
	}

	public String getCodigo() {
		return codigo;
	}

	public void setCodigo(String codigo) {
		this.codigo = MaskProcessor.removerCaracteresEspeciais(codigo);
	}
	
	public String getNome() {
		return nome;
	}

	public void setNome(String nome) {
		this.nome = MaskProcessor.removerCaracteresEspeciaisExcetoEspaco(nome);
	}

	public String getPis() {
		return MaskProcessor.removeMaskPIS(pis);
	}

	public void setPis(String pis) {
		this.pis = MaskProcessor.removeMaskPIS(pis);
	}

	public String getCartao() {
		return cartao;
	}

	public void setCartao(String cartao) {
		this.cartao = MaskProcessor.removerCaracteresEspeciais(cartao);
	}

	public Integer getTipoUsuario() {
		return tipoUsuario;
	}

	public void setTipoUsuario(Integer tipoUsuario) {
		this.tipoUsuario = tipoUsuario;
	}

	public String getSenha() {
		return senha;
	}

	public void setSenha(String senha) {
		this.senha = MaskProcessor.removerCaracteresEspeciais(senha);
	}
	
	public Integer getVerifica() {
		return verifica;
	}

	public void setVerifica(Integer verifica) {
		this.verifica = verifica;
	}

	public List<Digital> getDigitais() {
		return digitais;
	}

	public String getDigitaisToString() {
		StringBuilder ret = new StringBuilder();
		for (Digital d : digitais) {
			ret.append(d.toString());
		}
		return ret.toString();
	}

	public void setDigitais(List<Digital> digitais) {
		this.digitais.clear();
		this.digitais.addAll(digitais);
	}

	public byte[] getCodigoArray() {
		if (codigo != null && !codigo.isEmpty()) {
			return Conversor.longToByteArray(Long.parseLong(codigo), 3);
		}
		return new byte[] { 0, 0, 0 };
	}

	public byte[] getSenhaArray() {
		if (senha != null && !senha.isEmpty()) {
			return Conversor.DecToBCDArray(Long.parseLong(senha));
		}
		return new byte[] { 0, 0, 0 };
	}
}