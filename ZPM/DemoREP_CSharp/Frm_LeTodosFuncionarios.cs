using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoREP_CSharp
{
    public partial class Frm_LeTodosFuncionarios : Form
    {
        private int TotalRegistros;
        private int i;
        private int pos, pos_anterior;    /*posição do ";" na string biometrico*/
        private string bio1, bio2, bio3, bio4, bio5, bio6, bio7, bio8, bio9, bio10;
        private string sBiometrico;
        private int tamanhoTemplate, tamanhoBiometrico;
        
        public Frm_LeTodosFuncionarios()
        {
            InitializeComponent();
        }

        private void Btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Confirmar_Click(object sender, EventArgs e)
        {
            StringBuilder PIS = new StringBuilder(11);
            StringBuilder Matricula = new StringBuilder(20);
            StringBuilder NomeFuncionario = new StringBuilder(52);
            StringBuilder Biometrico = new StringBuilder(20000);    //Valor definido baseado na maior template possivel de ser gerada c/ 10 digitais cadastradas.
            StringBuilder HabilitaTeclado = new StringBuilder(1);
            StringBuilder CodigoTeclado = new StringBuilder(16);
            StringBuilder CodigoBarras = new StringBuilder(20);
            StringBuilder CodigoMIFARE = new StringBuilder(20);
            StringBuilder CodigoTAG = new StringBuilder(20);
            StringBuilder MensagemErro = new StringBuilder(256);

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaTodosFuncionarios(REPZPM_DLL.Handle);

            /*Sucesso na execução do comando*/
            if (REPZPM_DLL.ID_Comando > 0)
            {
                /**************************************************************************************************************************
                *MODO PENDRIVE                                                                                                            *
                ***************************************************************************************************************************/
                if (REPZPM_DLL.Modo == 1)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_VerificaRetornoPenDrive(REPZPM_DLL.Handle, REPZPM_DLL.ID_Comando);

                    /*Verifica se o retorno já está disponível*/
                    if (REPZPM_DLL.Retorno < 0)
                    {
                        REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_Pendrive(REPZPM_DLL.Retorno);

                        /*Verifica se o arquivo foi processado, 1 = OK*/
                        if (REPZPM_DLL.Retorno == 1)
                        {
                            /*Verifica no arquivo do pendrive se houve erro na execução do comando*/
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);

                            /*Comando executado*/
                            if (REPZPM_DLL.Retorno == 0)
                            {
                                MessageBox.Show("Comando executado com sucesso via pendrive!","Sucesso",MessageBoxButtons.OK,MessageBoxIcon.Information);

                                /*Verifica a quantidade de registros retornados*/
                                TotalRegistros = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

                                /*Verifica se retornaram registros*/
                                if (TotalRegistros > 0)
                                {
                                    /*Limpa a listagem*/
                                    Lst_Funcionarios.Items.Clear();

                                    /*Executa a busca dos Funcionários*/
                                    for (i = 1; i <= TotalRegistros; i++)
                                    {
                                        /*Busca as informações do funcionários*/
                                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoFuncionario(REPZPM_DLL.Handle, i, PIS, Matricula, NomeFuncionario, Biometrico, HabilitaTeclado, CodigoTeclado, CodigoBarras, CodigoMIFARE, CodigoTAG);

                                        /*Sucesso na leitura dos funcionários*/
                                        if (REPZPM_DLL.Retorno == 1)
                                        {
                                            /*Separa cada template*/
                                            sBiometrico = Convert.ToString(Biometrico);

                                            tamanhoBiometrico = sBiometrico.ToString().Length;

                                            if ((sBiometrico.EndsWith(";") == false) && (tamanhoBiometrico == 996))
                                            {
                                                //Insere um ";" no final da string p/ delimitar a última template
                                                sBiometrico = sBiometrico.Insert(tamanhoBiometrico, ";");
                                                tamanhoBiometrico++;
                                            }

                                            pos = sBiometrico.IndexOf(";");

                                            tamanhoTemplate = pos;  //define o tamanho da template

                                            /*Se existir biometria cadastrada, separa por template*/
                                            if (pos != -1)
                                            {
                                                bio1 = sBiometrico.Substring(0, tamanhoTemplate);
                                                pos_anterior = tamanhoTemplate;

                                                if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                {
                                                    pos = sBiometrico.IndexOf(";", pos_anterior);
                                                    if (pos != -1)
                                                    {
                                                        bio2 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                        pos_anterior = tamanhoTemplate + pos_anterior;

                                                        if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                        {
                                                            pos = sBiometrico.IndexOf(";", pos_anterior);

                                                            if (pos != -1)
                                                            {
                                                                bio3 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                pos_anterior = tamanhoTemplate + pos_anterior;

                                                                if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                                {
                                                                    pos = sBiometrico.IndexOf(";", pos_anterior);

                                                                    if (pos != -1)
                                                                    {
                                                                        bio4 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                        pos_anterior = tamanhoTemplate + pos_anterior;

                                                                        if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                                        {
                                                                            pos = sBiometrico.IndexOf(";", pos_anterior);

                                                                            if (pos != -1)
                                                                            {
                                                                                bio5 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                                pos_anterior = tamanhoTemplate + pos_anterior;

                                                                                if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                                                {
                                                                                    pos = sBiometrico.IndexOf(";", pos_anterior);

                                                                                    if (pos != -1)
                                                                                    {
                                                                                        bio6 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                                        pos_anterior = tamanhoTemplate + pos_anterior;

                                                                                        if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                                                        {
                                                                                            pos = sBiometrico.IndexOf(";", pos_anterior);

                                                                                            if (pos != -1)
                                                                                            {
                                                                                                bio7 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                                                pos_anterior = tamanhoTemplate + pos_anterior;

                                                                                                if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                                                                {
                                                                                                    pos = sBiometrico.IndexOf(";", pos_anterior);

                                                                                                    if (pos != -1)
                                                                                                    {
                                                                                                        bio8 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                                                        pos_anterior = tamanhoTemplate + pos_anterior;

                                                                                                        if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                                                                        {
                                                                                                            pos = sBiometrico.IndexOf(";", pos_anterior);

                                                                                                            if (pos != -1)
                                                                                                            {
                                                                                                                bio9 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                                                                pos_anterior = tamanhoTemplate + pos_anterior;

                                                                                                                if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                                                                                {
                                                                                                                    pos = sBiometrico.IndexOf(";", pos_anterior);

                                                                                                                    if (pos != -1)
                                                                                                                    {
                                                                                                                        bio10 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }


                                            Lst_Funcionarios.Items.Add("PIS: " + PIS);
                                            Lst_Funcionarios.Items.Add("Matricula: " + Matricula);
                                            Lst_Funcionarios.Items.Add("Nome Funcionário: " + NomeFuncionario);
                                            Lst_Funcionarios.Items.Add("Biométrico 1 : " + bio1);
                                            Lst_Funcionarios.Items.Add("Biometrico 2 : " + bio2);
                                            Lst_Funcionarios.Items.Add("Biometrico 3 : " + bio3);
                                            Lst_Funcionarios.Items.Add("Biometrico 4 : " + bio4);
                                            Lst_Funcionarios.Items.Add("Biometrico 5 : " + bio5);
                                            Lst_Funcionarios.Items.Add("Biometrico 6 : " + bio6);
                                            Lst_Funcionarios.Items.Add("Biometrico 7 : " + bio7);
                                            Lst_Funcionarios.Items.Add("Biometrico 8 : " + bio8);
                                            Lst_Funcionarios.Items.Add("Biometrico 9 : " + bio9);
                                            Lst_Funcionarios.Items.Add("Biometrico 10: " + bio10);
                                            Lst_Funcionarios.Items.Add("HabilitaTeclado: " + HabilitaTeclado);
                                            Lst_Funcionarios.Items.Add("Código Teclado: " + CodigoTeclado);
                                            Lst_Funcionarios.Items.Add("Código Barras: " + CodigoBarras);
                                            Lst_Funcionarios.Items.Add("Código MIFARE: " + CodigoMIFARE);
                                            Lst_Funcionarios.Items.Add("Código TAG: " + CodigoTAG);
                                            Lst_Funcionarios.Items.Add("\n");
                                        }
                                        else
                                        {
                                            /*Houve erro no retorno do comando via pendrive*/
                                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                                            MessageBox.Show(Convert.ToString(MensagemErro),"Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                        }
                                    }
                                }
                                else
                                {
                                    /*Houve erro no retorno do comando via pendrive*/
                                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                                    MessageBox.Show(Convert.ToString(MensagemErro),"Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            else
                            {
                                /*Houve erro no processamento do arquivo do pendrive*/
                                REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno);
                                return;
                            }
                        }
                    }
                }


                /**************************************************************************************************************************
                *MODO IP                                                                                                                  *
                ***************************************************************************************************************************/
                if (REPZPM_DLL.Modo == 0)
                {
                    TotalRegistros = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

                    /*Verifica se retornaram registros*/
                    if (TotalRegistros > 0)
                    {
                        /*Limpa a listagem*/
                        Lst_Funcionarios.Items.Clear();

                        /*Executa a busca dos Funcionários*/
                        for (i = 1; i <= TotalRegistros; i++)
                        {
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoFuncionario(REPZPM_DLL.Handle, i, PIS, Matricula, NomeFuncionario, Biometrico, HabilitaTeclado, CodigoTeclado, CodigoBarras, CodigoMIFARE, CodigoTAG);

                            /*Sucesso na execução do comando*/
                            if (REPZPM_DLL.Retorno == 1)
                            {
                                /*Separa cada template*/
                                sBiometrico = Convert.ToString(Biometrico);

                                tamanhoBiometrico = sBiometrico.ToString().Length;

                                if ((sBiometrico.EndsWith(";") == false) && (tamanhoBiometrico == 996))
                                {
                                    //Insere um ";" no final da string p/ delimitar a última template
                                    sBiometrico = sBiometrico.Insert(tamanhoBiometrico, ";");
                                    tamanhoBiometrico++;
                                }
                                                                
                                pos = sBiometrico.IndexOf(";");

                                tamanhoTemplate = pos;  //define o tamanho da template

                                /*Se existir biometria cadastrada, separa por template*/
                                if (pos != -1) 
                                {
                                    bio1 = sBiometrico.Substring(0, tamanhoTemplate);
                                    pos_anterior = tamanhoTemplate;

                                    if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                    {
                                        pos = sBiometrico.IndexOf(";", pos_anterior);
                                        if (pos != -1)
                                        {
                                            bio2 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                            pos_anterior = tamanhoTemplate + pos_anterior;
                                            
                                            if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                            {
                                                pos = sBiometrico.IndexOf(";", pos_anterior);

                                                if (pos != -1)
                                                {
                                                    bio3 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                    pos_anterior = tamanhoTemplate + pos_anterior;
                                        
                                                    if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                    {
                                                        pos = sBiometrico.IndexOf(";", pos_anterior);

                                                        if (pos != -1)
                                                        {
                                                            bio4 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                            pos_anterior = tamanhoTemplate + pos_anterior;
                                                            
                                                            if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                            {
                                                                pos = sBiometrico.IndexOf(";", pos_anterior);
                                            
                                                                if (pos != -1)
                                                                {
                                                                    bio5 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                    pos_anterior = tamanhoTemplate + pos_anterior;

                                                                    if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                                    {
                                                                        pos = sBiometrico.IndexOf(";", pos_anterior);

                                                                        if (pos != -1)
                                                                        {
                                                                            bio6 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                            pos_anterior = tamanhoTemplate + pos_anterior;

                                                                            if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                                            {
                                                                                pos = sBiometrico.IndexOf(";", pos_anterior);

                                                                                if (pos != -1)
                                                                                {
                                                                                    bio7 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                                    pos_anterior = tamanhoTemplate + pos_anterior;

                                                                                    if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                                                    {
                                                                                        pos = sBiometrico.IndexOf(";", pos_anterior);

                                                                                        if (pos != -1)
                                                                                        {
                                                                                            bio8 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                                            pos_anterior = tamanhoTemplate + pos_anterior;

                                                                                            if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                                                            {
                                                                                                pos = sBiometrico.IndexOf(";", pos_anterior);

                                                                                                if (pos != -1)
                                                                                                {
                                                                                                    bio9 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                                                    pos_anterior = tamanhoTemplate + pos_anterior;

                                                                                                    if ((pos_anterior < tamanhoBiometrico) && ((pos + 1) != tamanhoBiometrico))
                                                                                                    {
                                                                                                        pos = sBiometrico.IndexOf(";", pos_anterior);

                                                                                                        if (pos != -1)
                                                                                                        {
                                                                                                            bio10 = sBiometrico.Substring(pos + 1, tamanhoTemplate);
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                

                                Lst_Funcionarios.Items.Add("PIS: " + PIS);
                                Lst_Funcionarios.Items.Add("Matricula: " + Matricula);
                                Lst_Funcionarios.Items.Add("Nome Funcionário: " + NomeFuncionario);
                                Lst_Funcionarios.Items.Add("Biométrico 1 : " + bio1);
                                Lst_Funcionarios.Items.Add("Biometrico 2 : " + bio2);
                                Lst_Funcionarios.Items.Add("Biometrico 3 : " + bio3);
                                Lst_Funcionarios.Items.Add("Biometrico 4 : " + bio4);
                                Lst_Funcionarios.Items.Add("Biometrico 5 : " + bio5);
                                Lst_Funcionarios.Items.Add("Biometrico 6 : " + bio6);
                                Lst_Funcionarios.Items.Add("Biometrico 7 : " + bio7);
                                Lst_Funcionarios.Items.Add("Biometrico 8 : " + bio8);
                                Lst_Funcionarios.Items.Add("Biometrico 9 : " + bio9);
                                Lst_Funcionarios.Items.Add("Biometrico 10: " + bio10);
                                Lst_Funcionarios.Items.Add("HabilitaTeclado: " + HabilitaTeclado);
                                Lst_Funcionarios.Items.Add("Código Teclado: " + CodigoTeclado);
                                Lst_Funcionarios.Items.Add("Código Barras: " + CodigoBarras);
                                Lst_Funcionarios.Items.Add("Código MIFARE: " + CodigoMIFARE);
                                Lst_Funcionarios.Items.Add("Código TAG: " + CodigoTAG);
                                Lst_Funcionarios.Items.Add("\n");
                            }
                            else
                            {
                                /*Trata o retorno de erro do REP*/
                                REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não há funcionário cadastrados no REP!","Sucesso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                /*Houve erro no processamento do Handle*/
                REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando);
                return;
            }
        }
    }
}
