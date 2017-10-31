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
    public partial class Frm_CadastraFuncionario : Form
    {
        private int Operacao;

        public Frm_CadastraFuncionario()
        {
            InitializeComponent();
            Operacao = 1;
        }

        private void Btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Confirmar_Click(object sender, EventArgs e)
        {
            System.String Habilitar_Teclado;
            StringBuilder MensagemErro = new StringBuilder(256);

            if (Chk_Habilitar_Teclado.Checked)
            {
                Habilitar_Teclado = "S";
            }
            else
            {
                Habilitar_Teclado = "N";
            }


            /******************************************************************************************************************************
            'INCLUSÃO DE FUNCIONÁRIO                                                                                                      *
            '******************************************************************************************************************************/
            if (Operacao == 1)
            {
                /*Prepara o envio do cadastro do funcionário*/
                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, Operacao, Txt_PIS.Text, Txt_Matricula.Text, Txt_NomeFuncionario.Text, Txt_TemplateBiometrico.Text, Habilitar_Teclado, Txt_CodigoTeclado.Text, Txt_CodigoBarras.Text, Txt_CodigoMIFARE.Text, Txt_CodigoTAG.Text);

                /*Comando executado*/
                if (REPZPM_DLL.Retorno == 1)
                {
                    REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Funcionario_Envia(REPZPM_DLL.Handle);

                    /*Comando de cadastro de funcionário foi enviado com sucesso se for maior que 0*/
                    if (REPZPM_DLL.ID_Comando > 0)
                    {
                        /******************************************************************************************************************
                        *MODO = 0 -> IP                                                                                                   *
                        *******************************************************************************************************************/
                        if (REPZPM_DLL.Modo == 0)
                        {
                            /*Obtém o código de erro*/
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, 1);

                            /*Se Retorno for <> de 0, então houve erro na execução do comando de cadastro de funcionário*/
                            if (REPZPM_DLL.Retorno != 0)
                            {
                                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);

                                MessageBox.Show(Convert.ToString(MensagemErro), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;
                            }
                            else
                            {
                                REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno);
                                return;
                            }
                        }


                        /******************************************************************************************************************
                        *MODO = 1 -> PENDRIVE                                                                                             *
                        *******************************************************************************************************************/
                        if (REPZPM_DLL.Modo == 1)
                        {
                            /*Obtém o código de erro*/
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
                                        MessageBox.Show("Comando executado com sucesso via pendrive!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    else
                                    {
                                        /*Houve erro no retorno do comando via pendrive*/
                                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                                        MessageBox.Show(Convert.ToString(MensagemErro), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        else
                        {
                            /*Erro na execução do comando de envio de funcionário pela DLL*/
                            REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando);
                            return;
                        }
                    }
                }
            }


            /******************************************************************************************************************************
            *EXCLUSÃO DE FUNCIONÁRIO                                                                                                      *
            *******************************************************************************************************************************/
            if (Operacao == 0)
            {
                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, Operacao, Txt_PIS.Text, Txt_Matricula.Text, Txt_NomeFuncionario.Text, Txt_TemplateBiometrico.Text, Habilitar_Teclado, Txt_CodigoTeclado.Text, Txt_CodigoBarras.Text, Txt_CodigoMIFARE.Text, Txt_CodigoTAG.Text);

                /*Comando executado*/
                if (REPZPM_DLL.Retorno == 1)
                {
                    REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Funcionario_Envia(REPZPM_DLL.Handle);

                    /*Comando de cadastro de funcionário foi enviado com sucesso se for maior que 0*/
                    if (REPZPM_DLL.ID_Comando > 0)
                    {
                        /******************************************************************************************************************
                        *MODO = 0 -> IP                                                                                                   *
                        *******************************************************************************************************************/
                        if (REPZPM_DLL.Modo == 0)
                        {
                            /*Obtém o código de erro*/
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, 1);

                            /*Se Retorno for <> de 0, então houve erro na execução do comando de exclusão de funcionário*/
                            if (REPZPM_DLL.Retorno != 0)
                            {
                                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);

                                MessageBox.Show(Convert.ToString(MensagemErro), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;
                            }
                            else
                            {
                                REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno);
                                return;
                            }
                        }

                        /******************************************************************************************************************
                        *MODO = 1 -> PENDRIVE                                                                                             *
                        *******************************************************************************************************************/
                        if (REPZPM_DLL.Modo == 1)
                        {
                            /*Obtém o código de erro*/
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
                                        MessageBox.Show("Comando executado com sucesso via pendrive!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    else
                                    {
                                        /*Houve erro no retorno do comando via pendrive*/
                                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                                        MessageBox.Show(Convert.ToString(MensagemErro), "Erro PENDRIVE", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        else
                        {
                            /*Erro na execução do comando de envio de funcionário pela DLL*/
                            REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando);
                            return;
                        }
                    }
                }
            }

            /******************************************************************************************************************************
            *ALTERAÇÃO DE FUNCIONÁRIO                                                                                                     *
            *******************************************************************************************************************************/
            if (Operacao == 2)
            {
                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, Operacao, Txt_PIS.Text, Txt_Matricula.Text, Txt_NomeFuncionario.Text, Txt_TemplateBiometrico.Text, Habilitar_Teclado, Txt_CodigoTeclado.Text, Txt_CodigoBarras.Text, Txt_CodigoMIFARE.Text, Txt_CodigoTAG.Text);

                /*Comando executado*/
                if (REPZPM_DLL.Retorno == 1)
                {
                    REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Funcionario_Envia(REPZPM_DLL.Handle);

                    /*Comando de cadastro de funcionário foi enviado com sucesso se for maior que 0*/
                    if (REPZPM_DLL.ID_Comando > 0)
                    {
                        /******************************************************************************************************************
                        *MODO = 0 -> IP                                                                                                   *
                        *******************************************************************************************************************/
                        if (REPZPM_DLL.Modo == 0)
                        {
                            /*Obtém o código de erro*/
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, 1);

                            /*Se Retorno for <> de 0, então houve erro na execução do comando de alteração de funcionário*/
                            if (REPZPM_DLL.Retorno != 0)
                            {
                                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);

                                MessageBox.Show(Convert.ToString(MensagemErro), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;
                            }
                            else
                            {
                                REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno);
                                return;
                            }
                        }

                        /******************************************************************************************************************
                        *MODO = 1 -> PENDRIVE                                                                                             *
                        *******************************************************************************************************************/
                        if (REPZPM_DLL.Modo == 1)
                        {
                            /*Obtém o código de erro*/
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
                                        MessageBox.Show("Comando executado com sucesso via pendrive!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                    else
                                    {
                                        /*Houve erro no retorno do comando via pendrive*/
                                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                                        MessageBox.Show(Convert.ToString(MensagemErro), "Errp PENDRIVE", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        else
                        {
                            /*Erro na execução do comando de envio de funcionário pela DLL*/
                            REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando);
                            return;
                        }
                    }
                    else
                    {
                        REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.Retorno);
                        return;
                    }
                }
            }
        }


        private void Rdb_Inclusao_CheckedChanged(object sender, EventArgs e)
        {
            if (Rdb_Inclusao.Checked)
            {
                Operacao = 1;
            }
        }

        private void Rdb_Alteracao_CheckedChanged(object sender, EventArgs e)
        {
            if (Rdb_Alteracao.Checked)
            {
                Operacao = 2;
            }
        }

        private void Rdb_Exclusao_CheckedChanged(object sender, EventArgs e)
        {
            if (Rdb_Exclusao.Checked)
            {
                Operacao = 0;
            }
        }
                
    }
}
