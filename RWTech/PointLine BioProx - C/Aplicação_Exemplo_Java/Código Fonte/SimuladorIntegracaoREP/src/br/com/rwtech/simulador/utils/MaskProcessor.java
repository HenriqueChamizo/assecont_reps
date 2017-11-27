package br.com.rwtech.simulador.utils;

import javax.swing.text.MaskFormatter;

public class MaskProcessor {
	
	public static final int CNPJ_LENGTH = 14;
	public static final int CPF_LENGTH = 11;
	public static final int PIS_LENGTH = 12;
	
	public static String removeMaskCNPJ(String cnpj) {
		if (cnpj != null && !cnpj.isEmpty()) {
			cnpj = cnpj.replaceAll("\\.", "").replace("/", "").replace("-", "");
			if (cnpj.length() < CNPJ_LENGTH) {
				StringBuilder cnpjStrBuilder = new StringBuilder(cnpj);
				for (int i = 0; i < CNPJ_LENGTH - cnpjStrBuilder.length(); i++)
					cnpjStrBuilder.insert(0, "0");
				cnpj = cnpjStrBuilder.toString();
			}
		}
		return cnpj;
	}
	
	public static String removeMaskCPF(String cpf) {
		if (cpf != null && !cpf.isEmpty()) {
			cpf = cpf.replaceAll("\\.", "").replace("-", "");
			if (cpf.length() < CPF_LENGTH) {
				StringBuilder cpfStrBuilder = new StringBuilder(cpf);
				for (int i = 0; i < CPF_LENGTH - cpfStrBuilder.length(); i++)
					cpfStrBuilder.insert(0, "0");
				cpf = cpfStrBuilder.toString();
			}
		}
		return cpf;
	}
	
	public static String zeroFill(String str, int length) {
		if (str != null && !str.isEmpty() && str.length() < length) {
			StringBuilder cpfStrBuilder = new StringBuilder(str);
			for (int i = 0; i < length - cpfStrBuilder.length(); i++)
				cpfStrBuilder.insert(0, "0");
			str = cpfStrBuilder.toString();
		}
		return str;
	}

	public static String removeMaskPIS(String pis) {
		if (pis != null && !pis.isEmpty()) {
			pis = pis.replaceAll("\\.", "").replace("/", "");
			if (pis.length() < PIS_LENGTH) {
				StringBuilder pisStrBuilder = new StringBuilder(pis);
				for (int i = 0; i < PIS_LENGTH - pisStrBuilder.length(); i++)
					pisStrBuilder.insert(0, "0");
				pis = pisStrBuilder.toString();
			}
		}
		return pis;
	}
	
	public static String removerCaracteresEspeciais(String valor) {
		if (valor != null)
			valor = valor.replaceAll("[^0-9A-Za-z]", "");
		return valor;
	}
	
	public static String removerCaracteresEspeciaisExcetoEspaco(String valor) {
		if (valor != null)
			valor = valor.replaceAll("[^ 0-9A-Za-z]", "");
		return valor;
	}
		
	public static MaskFormatter getMask(String mascara, String preenchimento) {
		MaskFormatter maskFormatter = new MaskFormatter();
		try {
			maskFormatter.setMask(mascara);
			maskFormatter.setPlaceholder(preenchimento);
		} catch (Exception e) {
		}
		return maskFormatter;
	}
}
