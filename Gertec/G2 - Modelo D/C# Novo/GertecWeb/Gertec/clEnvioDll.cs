/*****************************************************************************
* Aplicativo : 	???????	                                                     *
* Autor	     : Ana Lúcia S. Melo (para esta classe)    	     			     *
* Referência : Programa para coleta do REP                                   *
* Data       : 26/08/11 						                             *
* Ambiente   : Visual C# 2008                    						     *
* Fonte      : clEnvioDll                   		  	                     *
* Objetivo   : Envia ou recebe informações para o REP através da DLL         *
******************************************************************************/

namespace REP_Prototipo
{
    public unsafe class clEnvioDll
    {
        dllREP repDll = new dllREP();

        
        //******************************************************************************/
        // public: LeCadastro                                                          *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 18/08/2011
        // Descrição : Recebe o AFD do REP contendo os registros
        // Retorno: int
        //
        //******************************************************************************      
        public int LeCadastro(string ip, byte filtro, string parametro, string arquivo)
        {
            int encontrado = 0, resultado = 0;
            dllREP.REP_LeCadastro(ip, filtro, parametro, arquivo, ref encontrado, ref resultado);
            return resultado;
        }

   
        //******************************************************************************/
        // public: ConectaREP                                                          *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 17/08/2011
        // Descrição : Conecta ao REP
        // Retorno: int
        //
        //******************************************************************************  
        public int ConectaREP(string ip, int onOff)
        {
            int resultado = 0;
            string[] teste = new string[3];
            
            dllREP.REP_Conexao_Simples(ip, onOff, ref resultado);
            if (resultado == 10005) resultado = 0;
            return resultado;
        }
        
        //******************************************************************************/
        // public: TempoREP                                                            *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 17/08/2011
        // Descrição : Recebe a data e hora atual do REP
        // Retorno: string[]
        //
        //******************************************************************************           
        public string[] TempoREP(string ip)
        {
            byte mes = 0, dia = 0, hora = 0, minuto = 0, segundo = 0, diaSemana = 0;
            int ano = 0, resultado = 0;
            string[] retorno = new string[7];
            //Função para receber a data e hora
            dllREP.REP_Tempo(ip, 0, ref ano, ref mes, ref dia, ref hora, ref minuto, ref segundo, ref diaSemana, ref resultado);
            retorno[0] = resultado.ToString();
            retorno[1] = dia.ToString();
            retorno[2] = mes.ToString();
            retorno[3] = ano.ToString();
            retorno[4] = hora.ToString();
            retorno[5] = minuto.ToString();
            retorno[6] = segundo.ToString();

            return retorno;
        }


        //******************************************************************************/
        // public: enviaTempoREP                                                       *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 17/08/2011
        // Descrição : Envia uma nova data e hora para o REP
        // Retorno: int
        //
        //******************************************************************************           
        public int enviaTempoREP(string ip,byte dia, byte mes, int ano, byte hora, byte minuto, byte segundo, byte diaSemana)
        {
            int resultado = 0;
            dllREP.REP_Tempo(ip, 1, ref ano, ref mes, ref dia, ref hora, ref  minuto, ref segundo, ref diaSemana, ref resultado);
            return resultado;
        }


        //******************************************************************************/
        // public: espacoMemoria                                                       *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 17/08/2011
        // Descrição : Recebe o tamanho do espaço livre e usado da memoria do REP
        // Retorno: int[]
        //
        //******************************************************************************      
     
        public int[] espacoMemoria(string ip)
        {
            int memUsada = 0, memLivre = 0, resultado = 0;
            int[] resposta = new int[3];
            dllREP.REP_LeEspacoMemoria(ip, ref memUsada, ref memLivre, ref resultado);
            resposta[0] = resultado;
            resposta[1] = memUsada;
            resposta[2] = memLivre;
            return resposta;
        }

        //******************************************************************************/
        // public: LeFinger                                                         *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade   
        // Data de Criação : 27/10/2011
        // Descrição : Recebe as biometrias cadastradas
        // Retorno: int
        //
        //******************************************************************************      
        public int LeFinger(string ip, string arquivos)
        {
            int resultado = 0;
            dllREP.REP_LeFinger(ip, arquivos, ref resultado);
            return resultado;
        }

        //******************************************************************************/
        // public: LeFingerID                                                         *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade   
        // Data de Criação : 31/10/2011
        // Descrição : Recebe a biometrias de um determinado PIS cadastrado
        // Retorno: int
        //
        //******************************************************************************      
        public int LeFingerID(string ip, string pis)
        {
            int resultado = 0;
            dllREP.REP_LeFingerID(ip, pis, ref resultado);
            return resultado;
        }

        //******************************************************************************/
        // public: RemoverFingerID                                                         *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade   
        // Data de Criação : 03/11/2011
        // Descrição : Exclui a biometrias de um determinado PIS cadastrado
        // Retorno: int
        //
        //******************************************************************************      
        public int RemoverFingerID(string ip, string pis)
        {
            int resultado = 0;
            dllREP.REP_RemoverFingerID(ip, pis, ref resultado);
            return resultado;
        }
        //******************************************************************************/
        // public: LeIntervaloNSR                                                          *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 03/11/2011
        // Descrição : Recebe o registro de acordo com o intervalo especificado.
        // Retorno: int
        //
        //******************************************************************************      
        public int LeIntervaloNSR(string ip, string NSRInicial, string NSRFinal, string arquivo)
        {
            int encontrado = 0, resultado = 0;
            dllREP.REP_LeIntervaloNSR(ip, NSRInicial, NSRFinal, arquivo, ref encontrado, ref resultado);
            return resultado;
        }
        /*public int InfoTerminais()
        {
            
            
            
            return resultado;
        }*/
    }
}