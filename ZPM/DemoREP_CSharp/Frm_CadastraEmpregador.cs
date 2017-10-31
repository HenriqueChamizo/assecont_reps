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
    public partial class Frm_CadastraEmpregador : Form
    {

        private int Operacao;
        private System.String Pessoa;
        //private int Cod_Erro;
        private int Total_Retornos;
        StringBuilder MensagemErro = new StringBuilder(256);
        
                        
        public Frm_CadastraEmpregador()
        {
            InitializeComponent();
            Operacao = 1;
            Pessoa = "J";
        }
                
        private void Btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void Btn_Confirmar_Click(object sender, EventArgs e)
        {
            /******************************************************************************************************************************
             * INCLUSÃO DE EMPREGADOR                                                                                                     *
             * ***************************************************************************************************************************/
            /*Operação de Inclusão de Empregador*/
            if (Operacao == 1)
            {
                REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Empregador(REPZPM_DLL.Handle, Operacao, Pessoa, Txt_CNPJCPF.Text, Txt_CEI.Text, Txt_RazaoSocial.Text, Txt_LocalTrabalho.Text);

                /*Sucesso na execução do comando*/
                if (REPZPM_DLL.ID_Comando > 0)
                {
                    /**********************************************************************************************************************
                     * MODO IP                                                                                                            *
                     * *******************************************************************************************************************/
                    if (REPZPM_DLL.Modo == 0)
                    {
                        /*Retorna a quantidade de retornos do comando enviado*/
                        Total_Retornos = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, Total_Retornos);

                        /*Houve erro*/
                        if (REPZPM_DLL.Retorno != 0)
                        {
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);

                            MessageBox.Show(Convert.ToString(MensagemErro), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            this.Close();
                        }
                        /*Houve sucesso no envio do comando*/
                        else
                        {
                            MessageBox.Show("Inclusão de Empregador realizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }

                    /**********************************************************************************************************************
                     * MODO PENDRIVE                                                                                                      *
                     * *******************************************************************************************************************/
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
                                    MessageBox.Show("Comando executado com sucesso via pendrive!","Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                }
                /*Trata o erro retornado pela DLL*/
                else
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando);
                }
            }
        }

        private void Rdb_Inclusao_CheckedChanged(object sender, EventArgs e)
        {
            if (Rdb_Inclusao.Checked == true)
            {
                Operacao = 1;
            }
        }

        private void Rdb_Alteracao_CheckedChanged(object sender, EventArgs e)
        {
            if (Rdb_Alteracao.Checked == true)
            {
                Operacao = 2;
            }
        }

        private void Rdb_Juridica_CheckedChanged(object sender, EventArgs e)
        {
            if (Rdb_Juridica.Checked == true)
            {
                Pessoa = "J";
            }
        }

        private void Rdb_Fisica_CheckedChanged(object sender, EventArgs e)
        {
            if (Rdb_Fisica.Checked == true)
            {
                Pessoa = "F";
            }
        }
    }
}
