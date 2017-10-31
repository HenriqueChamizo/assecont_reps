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
    public partial class Frm_LeEmpregador : Form
    {
        public Frm_LeEmpregador()
        {
            InitializeComponent();
        }

        private void Btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Confirmar_Click(object sender, EventArgs e)
        {
            StringBuilder Tipo = new StringBuilder(1);
            StringBuilder Identificacao = new StringBuilder(14);
            StringBuilder CEI = new StringBuilder(12);
            StringBuilder RazaoSocial = new StringBuilder(150);
            StringBuilder LocalTrabalho = new StringBuilder(100);
            StringBuilder MensagemErro = new StringBuilder(256);

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaEmpregador(REPZPM_DLL.Handle);

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

                                /*Busca as informações do empregador*/
                                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoEmpregador(REPZPM_DLL.Handle, Tipo, Identificacao, CEI, RazaoSocial, LocalTrabalho);

                                /*Sucesso na leitura do empregador*/
                                if (REPZPM_DLL.Retorno == 1)
                                {
                                    /*Mostra as informações do empregador*/
                                    Lst_Empregador.Items.Add("Tipo de Pessoa: " + Tipo);
                                    Lst_Empregador.Items.Add("CNPJ / CPF: " + Identificacao);
                                    Lst_Empregador.Items.Add("CEI: " + CEI);
                                    Lst_Empregador.Items.Add("Razão Social: " + RazaoSocial);
                                    Lst_Empregador.Items.Add("Local Trabalho: " + LocalTrabalho);
                                    return;
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

                /**************************************************************************************************************************
                *MODO IP                                                                                                                  *
                ***************************************************************************************************************************/
                if (REPZPM_DLL.Modo == 0)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoEmpregador(REPZPM_DLL.Handle, Tipo, Identificacao, CEI, RazaoSocial, LocalTrabalho);

                    /*Sucesso na leitura do empregador*/
                    if (REPZPM_DLL.Retorno == 1)
                    {
                        /*Mostra as informações do Empregador*/
                        Lst_Empregador.Items.Add("Tipo de Pessoa: " + Tipo);
                        Lst_Empregador.Items.Add("CNPJ / CPF: " + Identificacao);
                        Lst_Empregador.Items.Add("CEI: " + CEI);
                        Lst_Empregador.Items.Add("Razão Social: " + RazaoSocial);
                        Lst_Empregador.Items.Add("Local Trabalho: " + LocalTrabalho);
                    }
                    else
                    {
                        /*Houve erro na leitura do empregador*/
                        REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno);
                        return;
                    }
                }
            }
            else
            {
                /*Trata o erro na inicialização do Handle*/
                REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando);
                return;
            }
        }
    }
}
