using System;

namespace BioproxC.RWTech.Pojo
{
    public class Equipamento
    {
        private String ip;
        private String porta;
        private String chaveCriptografica;
        private String cpf;

        private static String CPF_PADRAO = "11111111111";

	    public Equipamento()
        {
        }

        public Equipamento(String ip, String porta)
        {
            this.ip = ip;
            this.porta = porta;
        }

        public String getIp()
        {
            return ip;
        }

        public void setIp(String ip)
        {
            this.ip = ip;
        }

        public String getPorta()
        {
            return porta;
        }

        public void setPorta(String porta)
        {
            this.porta = porta;
        }

        public String getChaveCriptografica()
        {
            //return "18CF6DAB6FADE5C4B4BFDE70E69E4A143DA76E08076DC7BD92ABBC4360FFFFC9";
            return chaveCriptografica;
        }

        public void setChaveCriptografica(String chaveCriptografica)
        {
            this.chaveCriptografica = chaveCriptografica;
        }

        public String getCpf()
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return CPF_PADRAO;
            }
            return cpf;
        }

        public void setCpf(String cpf)
        {
            this.cpf = cpf;
        }
    }
}
