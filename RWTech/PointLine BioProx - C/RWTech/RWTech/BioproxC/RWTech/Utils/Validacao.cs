using System;
using BioproxC.RWTech.Common;
using System.Windows.Forms;
using BioproxC.RWTech.Criptografia;
using System.Text.RegularExpressions;

namespace BioproxC.RWTech.Utils
{
    public static class Validacao
    {
        public static bool isDadosConexaoOK(String ip, String porta)
        {
            if (!isIpPortaOK(ip, porta))
            {
                MessageBox.Show("IP/Porta inválido(s)!");
                return false;
            }
            else
                return true;
        }

        public static bool isChaveCriptograficaOK()
        {
            String chaveCriptografica = EquipamentoManager.getInstance().getEquipamento().getChaveCriptografica();
            return chaveCriptografica != null && chaveCriptografica.Length == AES.QTD_CARACTERES_CHAVE;
        }

        public static bool isIpPortaOK(String ip, String porta)
        {
            return !string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(porta);
        }

        public static bool isStringDataOK(String str, int tamanho)
        {
            if (!string.IsNullOrEmpty(str) && str.Length != tamanho)
                return true;
            else
                return false;
        }

        public static bool isStringObrigatoriaOK(String str, int tamanhoMaximo)
        {
            if (!string.IsNullOrEmpty(str) && str.Length <= tamanhoMaximo)
                return true;
            else
                return false;
        }

        public static bool isStringOpcionalOK(String str, int tamanhoMaximo)
        {
            if (string.IsNullOrEmpty(str) || str.Length <= tamanhoMaximo)
                return true;
            else
                return false;
        }

        public static bool isCpfOK(String cpf)
        { // formato xxx.xxx.xxx-xx ou somente números
            if (!string.IsNullOrEmpty(cpf))
            {
                try
                {
                    cpf = cpf.Replace("\\D", "");

                    char[] digits = cpf.ToCharArray();

                    if (digits.Length != 11) return false;
                    
                    Regex rx = new Regex("^" + digits[0] + "{11}$");
                    if (rx.Matches(cpf).Count > 0) return false;

                    int j, n, i;

                    for (j = 10, n = 0, i = 0; j >= 2; n += Convert.ToInt32(digits[i++]) * j--) ;

                    if (Convert.ToInt32(digits[9]) != (((n %= 11) < 2) ? 0 : 11 - n)) return false;

                    for (j = 11, n = 0, i = 0; j >= 2; n += Convert.ToInt32(digits[i++]) * j--) ;

                    if (Convert.ToInt32(digits[10]) != (((n %= 11) < 2) ? 0 : 11 - n)) return false;

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro! Motivo: " + e.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool isCnpjOK(String cnpj)
        { // formato xx.xxx.xxx/xxxx-xx ou somente números
            if (!string.IsNullOrEmpty(cnpj))
            {
                try
                {
                    cnpj = cnpj.Replace("\\D", "");

                    char[] digits = cnpj.ToCharArray();

                    if (digits.Length != 14) return false;

                    Regex rx = new Regex("^" + digits[0] + "{11}$");
                    if (rx.Matches(cnpj).Count > 0) return false;

                    int j, n;

                    int[] factors = new int[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                    for (j = 0, n = 0; j < 12; n += Convert.ToInt32(digits[j++]) * factors[j]) ;

                    if (Convert.ToInt32(digits[12]) != (((n %= 11) < 2) ? 0 : 11 - n)) return false;

                    for (j = 0, n = 0; j <= 12; n += Convert.ToInt32(digits[j]) * factors[j++]) ;

                    if (Convert.ToInt32(digits[13]) != (((n %= 11) < 2) ? 0 : 11 - n)) return false;

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro! Motivo: " + e.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool isPisOK(String pis, int tamanhoMaximo)
        {
            try
            {
                pis = pis.Replace("/", "").Replace("\\.", "");
                if (isStringObrigatoriaOK(pis, tamanhoMaximo))
                {
                    if (pis.Length == 12)
                    {
                        pis = pis.Substring(1);
                    }
                    char[] numeros = pis.ToCharArray();
                    int[] multiplicadores = { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                    int somatoria = 0;
                    for (int i = 0; i < multiplicadores.Length; i++)
                    {
                        somatoria += multiplicadores[i] * Convert.ToInt32(Convert.ToString(numeros[i]));
                    }
                    int digito = somatoria % 11;
                    if (digito < 2)
                        digito = 0;
                    else
                        digito = 11 - digito;

                    if (digito == Convert.ToInt32(Convert.ToString(numeros[numeros.Length - 1])))
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool isPeriodoDataOK(DateTime dataInicio, DateTime dataFim)
        {
            if (dataInicio.CompareTo(dataFim) < 0)
                return false;
            else
                return true;
        }
    }
}
