using System;
using System.Collections.Generic;
using System.Text;

namespace TRXHumanResource
{
    public class Comando
    {
        #region Atributos
        private string virgula = ",";
        private string modo;
        private string tipoComando;
        private string mensagem;
        private string crc;
        private string tamanhoMensagem;

        #endregion

        #region Propriedades

        public string CRC
        {
            get { return crc; }
        }

        public string TipoComando
        {
            get { return tipoComando; }
        }

        public string Modo
        {
            get { return modo; }
        }

        public string Mensagem
        {
            get { return mensagem; }
            
        }
        #endregion

        #region Construtores
        public Comando(string resposta)
        {
            string[] campos = resposta.Split(',');

            tipoComando = campos[0];
            modo = campos[1];
            tamanhoMensagem = campos[2];
            mensagem = campos[3];
            for (int i = tamanhoMensagem.Length; i < 3; i++)
                tamanhoMensagem = "0" + tamanhoMensagem;
            CalculaCRC();
        }

        public Comando(string pTipoComando, string pModo, string pMensagem)
        {
            tipoComando = pTipoComando;
            modo = pModo;
            mensagem = pMensagem;
            tamanhoMensagem = mensagem.Length.ToString();
            for (int i = tamanhoMensagem.Length; i < 3; i++)
                tamanhoMensagem = "0" + tamanhoMensagem;
            CalculaCRC();
        }



        #endregion



        #region Métodos

        public string InfoEmpresa()
        {
            string tipoIdentificador = mensagem.Substring(0, 1);
            string identificador = mensagem.Substring(1, 14);
            identificador = identificador.Trim();
            string cei = mensagem.Substring(15, 12);
            cei = cei.Trim();
            string razao = mensagem.Substring(27, 150);
            razao = razao.Trim();
            string local = mensagem.Substring(177, 100);
            local = local.Trim();

            if(tipoIdentificador.Equals("1"))
                tipoIdentificador = "Jurídica";
            if(tipoIdentificador.Equals("2"))
                tipoIdentificador = "Física";

            string res = "Razão social: " + razao + "\n" +
                         "Local: " + local + "\n" +
                         "Tipo: " + tipoIdentificador + "\n" +
                         "CNPJ/CPF: " + identificador + "\n" +
                         "CEI: " + cei + "\n";

            return res;
        }

        public string InfoDataHora()
        {
            string dia = mensagem.Substring(0, 2);
            string mes = mensagem.Substring(2, 2);
            string ano = mensagem.Substring(4, 4);
            string hora = mensagem.Substring(8, 2);
            string minuto = mensagem.Substring(10, 2);

            string data = dia + "/" + mes + "/" + ano;
            string horario = hora + ":" + minuto;

            string res = "Data: " + data + "\nHora: " + horario;

            return res;
        }

        public string InfoFuncionario()
        {
            string cracha = mensagem.Substring(0, 15);
            string pis = mensagem.Substring(15, 12);

            return pis;
        }

        public void CalculaCRC()
        {
            string nBytes = mensagem.Length.ToString();
            for (int i = nBytes.Length; i < 3; i++)
                nBytes = "0" + nBytes;
            string comando = tipoComando + virgula + modo + virgula + nBytes + 
                             virgula + mensagem + virgula;

            int soma = 0;
            foreach (char c in comando)
            {
                soma = soma + c;
            }
            int res = soma % 256;

            string crc = res.ToString("X");
            if (crc.Length < 2)
                crc = "0" + crc;
            this.crc = crc;
        }

        public static bool VerificaCRC(string resposta)
        {
            string crcRecebido = resposta.Substring(resposta.Length - 2, 2);
            string corpo = resposta.Substring(0, resposta.Length - 2);
            int soma = 0;
            foreach (char c in corpo)
            {
                soma = soma + c;
            }
            int res = soma % 256;
            string amount = res.ToString("X");
            if (amount.Length < 2)
                amount = "0" + amount;
            if (amount.Equals(crcRecebido))
                return true;
            else
                return false;

        }

        

        public string ToString()
        {
            string res = tipoComando + virgula + modo + virgula + tamanhoMensagem +
                         virgula + mensagem + virgula + crc;

            return res;


        }

        public byte[] ToByte()
        {
            string dados = this.ToString();
            int tam = dados.Length;
            Byte[] envio = new Byte[tam];
            for (int i = 0; i < tam; i++)
                envio[i] = (Byte)Convert.ToChar(dados.Substring(i, 1));

            return envio;

        }
        #endregion
    }

    public class Protocolo
    {
        public const string CMD_EMPRESA = "!01";
        public const string CMD_DATA_HORA = "!02";
        public const string CMD_FUNCIONARIO = "!03";
        public const string CMD_BIOMETRIA = "!05";
        public const string CMD_EXCLUSAO = "!04";
        public const string CMD_MARCACAO = "!06";
        public const string CMD_BOBINA = "!07";

        public const string READ = "R";
        public const string SET = "S";
        public const string INFO = "I";

        public static string ConverteByteToString(Byte[] bytes)
        {
            string res = string.Empty;
            foreach (byte b in bytes)
            {
                res = res + Convert.ToChar(b);
            }

            return res;
        }


    }
}
