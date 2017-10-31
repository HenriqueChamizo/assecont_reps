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
    public partial class Frm_AjustaHorarioVerao : Form
    {
        private int conta;
        
        public Frm_AjustaHorarioVerao()
        {
            InitializeComponent();
        }

        private void Btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Confirmar_Click(object sender, EventArgs e)
        {
            int Total_Retornos;
            StringBuilder MensagemErro = new StringBuilder(256);
            
                    
            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_AjustaHorarioVerao(REPZPM_DLL.Handle, Txt_DataInicio.Text, Txt_DataFim.Text);

            /*Houve sucesso no envio do comando*/
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
                    Total_Retornos = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

                    conta++;

                    while (conta <= Total_Retornos)
                    {
                        /*Obtém o código de erro do REP*/
                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, conta);

                        /*Houve erro*/
                        if (REPZPM_DLL.Retorno != 0)
                        {
                            REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno);
                            return;
                        }

                        conta++;
                    }

                    /*Houve erro*/
                    if (REPZPM_DLL.Retorno != 0)
                    {
                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                        MessageBox.Show(Convert.ToString(MensagemErro),"Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Ajuste do Horário de Verão executado com sucesso!","Sucesso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando);
                return;
            }
        }
    }
}
