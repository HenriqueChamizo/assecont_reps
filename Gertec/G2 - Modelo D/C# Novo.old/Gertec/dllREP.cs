/*****************************************************************************
* Aplicativo : 	???????	                                                     *
* Autor	     : Ana Lúcia S. Melo (para esta classe)    	     			     *
* Referência : Programa para coleta do REP                                   *
* Data       : 27/04/11						                                 *
* Ambiente   : Visual C# 2008                    						     *
* Fonte      : Form1                   		  	                             *
* Objetivo   : Responsável pela declaração da dll.                           *
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace REP_Prototipo
{
    public unsafe class dllREP
    {   
        [DllImport("REP_Gertec.dll", EntryPoint = "REP_Conexao_Simples", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_Conexao_Simples(string sIP, int bAcao, ref int dwResult);

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_InfoTerminal", ExactSpelling = true,CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_InfoTerminal(string sIP, byte[] sNome, ref int dwResult);              
        
        [DllImport("REP_Gertec.dll", EntryPoint = "REP_Tempo", ExactSpelling = true,CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_Tempo(string sIP, byte bLeGrava, ref int bAno, ref byte bMes, ref byte bDia, ref byte bHora, ref byte bMinuto, ref byte bSegundo, ref byte bDiaSemana, ref int dwResult);              

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_LeTemperatura", ExactSpelling = true,CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_LeTemperatura(string sIP, byte[] bTemperatura, ref int dwResult);              

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_Rede_Simples", ExactSpelling = true,CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_Rede_Simples(string sIP, byte bLeGrava, byte[] sIPTerminal, byte[] sIPMascara, ref int dwResult);              

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_LeCadastro", ExactSpelling = true,CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_LeCadastro(string sIP, byte bFiltro, string sParametro, string sArquivoDados,ref int dwEncontrados, ref int dwResult);

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_LeIntervaloNSR", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_LeIntervaloNSR(string sIP, string sNSRInicial, string sNSRFinal, string sArquivoDados, ref int dwEncontrados, ref int dwResult);

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_GravaCadastroEmpregador", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_GravaCadastroEmpregador(string sIP, char bAcao, char bTipo, string sCPFCNPJ, string sCEI, string sRazaoSocial, string sLocal, ref int dwResult);              

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_GravaCadastroFuncionario", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_GravaCadastroFuncionario(string sIP, char bAcao, string sNome, string sPIS, string vbContactLess, string vbCodBarras, string vbTeclado, string vbBiometria, byte bGrupo, ref int dwResult);

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_ExcluiCadastroFuncionario", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_ExcluiCadastroFuncionario(string sIP, string sPIS, ref int dwResult);              

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_LeEspacoMemoria", ExactSpelling = true,CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_LeEspacoMemoria(string sIP, ref int dwMemUsada,ref int dwMemLivre,ref int dwResult);              

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_LeNSR", ExactSpelling = true,CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_LeNSR(string sIP, byte[] sNSR, ref int dwResult);                                      

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_MensagemErro", ExactSpelling = true,CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_MensagemErro(ref int dwResult, ref byte sMensagemErro);

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_LeFinger", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_LeFinger(string sIP, string dwEncontrados, ref int dwResult);

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_LeFingerID", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_LeFingerID(string sIP, string sPis, ref int dwResult);

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_GravaFinger", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_GravaFinger(string sIP, string sArquivoDados, ref int dwResult);

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_FormataFinger", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_FormataFinger(string sIP, ref int dwResult);

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_RemoverFingerID", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_RemoverFingerID(string sIP, string sPis , ref int dwResult);

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_FormataMT", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_FormataMT(string sIP, ref int dwResult);

        [DllImport("REP_Gertec.dll", EntryPoint = "REP_StatusImpressora", ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void REP_StatusImpressora(string sIP, byte[] bSTatusImpressora, ref int dwResult);              

    }
}
