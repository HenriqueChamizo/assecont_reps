package br.com.rwtech.simulador.utils;

import java.util.Date;
import java.util.regex.Pattern;

import javax.swing.JOptionPane;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.criptografia.AES;

public class Validacao {
	
	public static boolean isDadosConexaoOK(String ip, String porta) {
		if (!isIpPortaOK(ip, porta)) {
			JOptionPane.showMessageDialog(null, "IP/Porta inválido(s)!");
			return false;
		} else if (!isChaveCriptograficaOK()) {
			JOptionPane.showMessageDialog(null, "Favor definir a Chave Criptográfica (64 caracteres) no cadastro do equipamento!");
			return false;
		} else {
			return true;
		}
	}
	
	public static boolean isChaveCriptograficaOK() {
		String chaveCriptografica = EquipamentoManager.getInstance().getEquipamento().getChaveCriptografica();
		return chaveCriptografica != null && chaveCriptografica.length() == AES.QTD_CARACTERES_CHAVE;
	}
	
	public static boolean isIpPortaOK(String ip, String porta) {
		return ip != null && !ip.isEmpty() && porta != null && !porta.isEmpty();  
	}

	public static boolean isStringDataOK(String str, int tamanho) { 
		if (str != null && !str.isEmpty() && str.length() != tamanho) {
			return true;
		} else {
			return false;
		}
	}
	
	public static boolean isStringObrigatoriaOK(String str, int tamanhoMaximo) { 
		if (str != null && !str.isEmpty() && str.length() <= tamanhoMaximo) {
			return true;
		} else {
			return false;
		}
	}
	
	public static boolean isStringOpcionalOK(String str, int tamanhoMaximo) { 
		if (str == null || str.isEmpty() || str.length() <= tamanhoMaximo) {
			return true;
		} else {
			return false;
		}
	}
	
	public static boolean isCpfOK(String cpf) { // formato xxx.xxx.xxx-xx ou somente números
		if (cpf != null && !cpf.isEmpty()) {
			try {
				cpf = cpf.replaceAll("\\D","");
		        
		        char[] digits = cpf.toCharArray();
		 
		        if (digits.length != 11) return false;
		 
		        if(Pattern.matches("^" + digits[0] + "{11}$", cpf)) return false;
		 
		        int j,n,i; 
		        
		        for(j = 10, n = 0, i = 0; j >= 2; n +=  Character.getNumericValue(digits[i++]) * j--);
		        
		        if(Character.getNumericValue(digits[9]) != (((n %= 11) < 2) ? 0 : 11 - n)) return false;
		 
		        for(j = 11, n = 0, i = 0; j >= 2; n += Character.getNumericValue(digits[i++]) * j--);
		        
		        if(Character.getNumericValue(digits[10]) != (((n %= 11) < 2) ? 0 : 11 - n)) return false;
		        
		        return true;
			} catch (Exception e) {
				System.err.println("Erro! Motivo: " + e.getMessage());
				return false;
			}
		} else {
			return false;
		}
	}

	public static boolean isCnpjOK(String cnpj) { // formato xx.xxx.xxx/xxxx-xx ou somente números
		if (cnpj != null && !cnpj.isEmpty()) {
			try {
				cnpj = cnpj.replaceAll("\\D","");
		        
		        char[] digits = cnpj.toCharArray();
		  
		        if (digits.length != 14) return false;
		  
		        if(Pattern.matches("^" + digits[0] + "{14}$", cnpj)) return false;
		  
		        int j,n; 
		        
		        int[] factors = new int[] {6,5,4,3,2,9,8,7,6,5,4,3,2};
		        
		        for (j = 0, n = 0; j < 12; n += Character.getNumericValue(digits[j++]) * factors[j]);
		        
		        if(Character.getNumericValue(digits[12]) != (((n %= 11) < 2) ? 0 : 11 - n)) return false;
		 
		        for (j = 0, n = 0; j <= 12; n += Character.getNumericValue(digits[j]) * factors[j++]);
		        
		        if(Character.getNumericValue(digits[13]) != (((n %= 11) < 2) ? 0 : 11 - n)) return false;
		                 
		        return true;
			} catch (Exception e) {
				System.err.println("Erro! Motivo: " + e.getMessage());
				return false;
			}
		} else {
			return false;
		}
	}
	
	public static boolean isPisOK(String pis, int tamanhoMaximo) {
		try {
            pis = pis.replaceAll("/", "").replaceAll("\\.", "");
            if (isStringObrigatoriaOK(pis, tamanhoMaximo)) {
            	if (pis.length() == 12) {
                	pis = pis.substring(1); 
                }
            	char[] numeros = pis.toCharArray();
                int[] multiplicadores = {3, 2, 9, 8, 7, 6, 5, 4, 3, 2};
                int somatoria = 0;
                for (int i = 0; i < multiplicadores.length; i++) {
                    somatoria += multiplicadores[i] * Integer.parseInt(String.valueOf(numeros[i]));
                }
                int digito = somatoria % 11;
                if (digito < 2) {
                    digito = 0;
                } else {
                    digito = 11 - digito;
                }
                if (digito == Integer.parseInt(String.valueOf(numeros[numeros.length - 1]))) {
                    return true;
                } else {
                    return false;
                }
            } else {
            	return false;
            }
        } catch (Exception ex) {
            return false;
        }
	}
	
	public static boolean isPeriodoDataOK(Date dataInicio, Date dataFim) {
		if (dataInicio.after(dataFim)) {
			return false;
		} else {
			return true;
		}
	}
}
