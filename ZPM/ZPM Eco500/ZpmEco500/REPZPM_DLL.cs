using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DemoREP_CSharp
{
    class REPZPM_DLL
    {
        /*Funções de Inicialização*/
        [DllImport("dllrep.dll")] public static extern int DLLREP_IniciaDriver(System.String NumFabricacao);
        [DllImport("dllrep.dll")] public static extern int DLLREP_EncerraDriver(int Handle);
        [DllImport("dllrep.dll")] public static extern void DLLREP_Versao(StringBuilder Versao);
        [DllImport("dllrep.dll")] public static extern int DLLREP_DefineModoIP(int Handle, System.String EnderecoIP, int Porta);
        [DllImport("dllrep.dll")] public static extern int DLLREP_DefineModoArquivo(int Handle, System.String Unidade);
        [DllImport("dllrep.dll")] public static extern int DLLREP_LeModo(int Handle);
        [DllImport("dllrep.dll")] public static extern int DLLREP_DefineTimeout(int Handle, int Timeout);
        [DllImport("dllrep.dll")] public static extern int DLLREP_LeTimeout(int Handle);
        [DllImport("dllrep.dll")] public static extern void DLLREP_ConfiguraCodigoBarra(int Tamanho);
        [DllImport("dllrep.dll")] public static extern int DLLREP_LeCodigoBarra(int Handle);
        
        /*Funções de Atualização*/
        [DllImport("dllrep.dll")] public static extern int DLLREP_Empregador(int Handle,int Operacao, System.String Tipo, System.String Identificacao, System.String CEI, System.String RazaoSocial, System.String LocalTrabalho);
        [DllImport("dllrep.dll")] public static extern int DLLREP_Funcionario_Prepara(int Handle, int Operacao, System.String PIS, System.String Matricula, System.String NomeFuncionario, System.String TemplateBiometrico, System.String PIS_Teclado, System.String CodigoTeclado, System.String CodigoBarra, System.String CodigoMifare, System.String CodigoTAG);
        [DllImport("dllrep.dll")] public static extern int DLLREP_Funcionario_Envia(int Handle);
        [DllImport("dllrep.dll")] public static extern int DLLREP_AtualizaDataHora(int Handle, System.String DataHora);    
        [DllImport("dllrep.dll")] public static extern int DLLREP_AjustaHorarioVerao(int Handle, System.String DataInicio, System.String DataFim);
        
        /*Funções de Leitura*/
        [DllImport("dllrep.dll")] public static extern int DLLREP_BuscaEmpregador(int Handle);
        [DllImport("dllrep.dll")] public static extern int DLLREP_BuscaFuncionario(int Handle, System.String PIS);
        [DllImport("dllrep.dll")] public static extern int DLLREP_BuscaTodosFuncionarios(int Handle);
        [DllImport("dllrep.dll")] public static extern int DLLREP_BuscaPonto(int Handle, System.String DataInicio, System.String DataFim);
        [DllImport("dllrep.dll")] public static extern int DLLREP_BuscaLeituraMRP(int Handle, System.String DataInicio, System.String DataFim);
        [DllImport("dllrep.dll")] public static extern int DLLREP_BuscaStatus(int Handle);
        [DllImport("dllrep.dll")] public static extern int DLLREP_BuscaHorarioVerao(int Handle);
    
        /*Funções de Transmissão de Comando*/
        [DllImport("dllrep.dll")] public static extern int DLLREP_LimpaComandos(int Handle);
        [DllImport("dllrep.dll")] public static extern int DLLREP_VerificaRetornoPenDrive(int Handle, int ID_Comando);
        [DllImport("dllrep.dll")] public static extern int DLLREP_ObtemCodigoErro(int Handle, int ID_Linha);
        [DllImport("dllrep.dll")] public static extern int DLLREP_ObtemMensagemErro(int Handle, StringBuilder sMensagemErro, int ID_Linha);
    
        /*Funções de Obtenção de Retornos*/
        [DllImport("dllrep.dll")] public static extern int DLLREP_TotalRetornos(int Handle);
        [DllImport("dllrep.dll")] public static extern int DLLREP_RetornoEmpregador(int Handle, StringBuilder Tipo, StringBuilder Identificacao, StringBuilder CEI, StringBuilder RazaoSocial, StringBuilder LocalTrabalho);
        [DllImport("dllrep.dll")] public static extern int DLLREP_RetornoFuncionario(int Handle, int IndiceLinha, StringBuilder PIS, StringBuilder Matricula, StringBuilder NomeFuncionario, StringBuilder TemplateBiometrico, StringBuilder PIS_Teclado, StringBuilder CodigoTeclado, StringBuilder CodigoBarras, StringBuilder CodigoMifare, StringBuilder CodigoTAG);
        [DllImport("dllrep.dll")] public static extern int DLLREP_RetornoPonto(int Handle, int Indicelinha, StringBuilder PIS, StringBuilder DataHora, StringBuilder NSR);
        [DllImport("dllrep.dll")] public static extern int DLLREP_RetornoLeituraMRP(int Handle, int IndiceLinha, StringBuilder RegistroAFD);
        [DllImport("dllrep.dll")] public static extern int DLLREP_RetornoStatus(int Handle, StringBuilder NumFabricacao, StringBuilder UltimaMarcacaoPIS, StringBuilder UltimaMarcacaodataHora, StringBuilder StatusPapel, StringBuilder DataHora, StringBuilder MemoriaTotalMRP, StringBuilder MemoriaUsoMRP);
        [DllImport("dllrep.dll")] public static extern int DLLREP_RetornoHorarioVerao(int Handle, StringBuilder DataInicio, StringBuilder DataFim);

        public static int Handle;
        public static int Retorno;
        public static int Modo; /*Define se IP=0 ou Arquivo=1*/
        public static int ID_Comando;

                
        /**********************************************************************************************************************************
         * TRATA OS ERROS RETORNADOS PELA DLL
         * *******************************************************************************************************************************/

        public static string Trata_Retorno_DLL(int Ret)
        {
            string Result = "";

            switch (Ret)
            {
                case 1:
                    //MessageBox.Show("Comando executado com sucesso pela DLL", "Sucesso DLL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Result = "";
                    break;

                case -1:
                    Result = "Handle inválido!";
                    break;

                case -2:
                    Result = "Erro de comunicação!";
                    break;

                case -4:
                    Result = "Erro de timeout!";
                    break;

                case -5:
                    Result = "Erro de protocolo!";
                    break;

                case -6:
                    Result = "REP retorno mensagem de Erro. Utilize Função DLLREP_ObtemCodigoErro ou DLLREP_ObtemMensagemErro, para maiores detalhes";
                    break;

                case -7:
                    Result = "Erro valor inválido / ausente";
                    break;

                case -10:
                    Result = "Erro de gravação!";
                    break;

                case -11:
                    Result = "Erro no buffer de envio. Utilize o comando LimpaComandos para liberar o buffer!";
                    break;

                case -21:
                    Result = "Formato de data inválido!";
                    break;
            }

            return Result;
        }


        /**********************************************************************************************************************************
         * TRATA OS ERROS RETORNADOS PELO REP
         * *******************************************************************************************************************************/
        
        public static string Trata_Retorno_REP(int Ret)
        {
            string Result = "";

            switch (Ret)
            {
                case 0:
                    Result = "Comando executado com sucesso pelo REP";
                    break;

                case 1:
                    Result = "Registro mal formado";
                    break;

                case 2:
                    Result = "Valor inválido";
                    break;

                case 3:
                    Result = "Falha durante a operação do comando";
                    break;

                case 4:
                    Result = "Comando inválido";
                    break;

                case 5:
                    Result = "Registro não encontrado";
                    break;

                case 6:
                    Result = "Troca de comandos via pendrive desativada";
                    break;

                case 7:
                    Result = "Arquivo de comando excede tamanho permitido";
                    break;

                case 8:
                    Result = "Espaço da memória de trabalho esgotado";
                    break;

                case 9:
                    Result = "Espaço da MRP esgotado";
                    break;

            }

            return Result;
        }

        /**********************************************************************************************************************************
         * TRATA OS ERROS RETORNADOS PELO PENDRIVE
         * *******************************************************************************************************************************/
        public static int Trata_Retorno_Pendrive(int Ret)
        {
            DialogResult resultado;

            while (Ret < 1)
            {

                switch (Ret)
                {
                    case -40:
                        resultado = MessageBox.Show("Comando ainda não processado pelo REP.\nVerificar se já houve retorno no pendrive?", "Verificação",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        
                        if (resultado == DialogResult.Yes)
                        {
                            Retorno = REPZPM_DLL.DLLREP_VerificaRetornoPenDrive(REPZPM_DLL.Handle, REPZPM_DLL.ID_Comando);

                            if (Retorno == 1)
                            {
                                return Retorno;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            return Ret;
                        }
                                                
                        
                    case -41:
                        resultado = MessageBox.Show("Arquivo de retorno não encontrado.\nDeseja tentar novamente?", "Verificação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            Retorno = REPZPM_DLL.DLLREP_VerificaRetornoPenDrive(REPZPM_DLL.Handle, REPZPM_DLL.ID_Comando);
                            
                            if (Retorno == 1)
                            {
                                return Retorno;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            return Ret;
                        }
                        

                    case -42:
                        resultado = MessageBox.Show("Não foi possível acessar a unidade.\nDeseja tentar novamente?", "Verificação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            Retorno = REPZPM_DLL.DLLREP_VerificaRetornoPenDrive(REPZPM_DLL.Handle, REPZPM_DLL.ID_Comando);
                            
                            if (Retorno == 1)
                            {
                                return Retorno;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            return Ret;
                        }
               }

            }

            return 1;
        }
    }
}
