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
    public partial class Frm_BuscaPontoAFD : Form
    {
        public Frm_BuscaPontoAFD()
        {
            InitializeComponent();
        }

        private void Btn_Sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_BuscaPontoAFD_Load(object sender, EventArgs e)
        {
            Txt_DataInicio.Text = Convert.ToString(DateTime.Today.ToShortDateString());
            Txt_DataFim.Text = Convert.ToString(DateTime.Today.ToShortDateString());
        }

        private void Btn_Localizar_Click(object sender, EventArgs e)
        {
            int TotalRegistros;
            int i;
            StringBuilder PIS = new StringBuilder(11);
            StringBuilder DataHora = new StringBuilder(19);
            StringBuilder NSR = new StringBuilder(20);
            StringBuilder MensagemErro = new StringBuilder(256);
            StringBuilder RegistroAFD = new StringBuilder(300);

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaLeituraMRP(REPZPM_DLL.Handle, Txt_DataInicio.Text, Txt_DataFim.Text);

            if (REPZPM_DLL.ID_Comando > 0)
            {
                Lst_Marcacoes.Items.Clear();

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
                                    Lst_Marcacoes.Items.Clear();

                                    /*Executa a busca das marcações de ponto*/
                                    for (i = 1; i <= TotalRegistros; i++)
                                    {
                                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoLeituraMRP(REPZPM_DLL.Handle, i, RegistroAFD);

                                        /*Sucesso na execução do comando*/
                                        if (REPZPM_DLL.Retorno == 1)
                                        {
                                            Lst_Marcacoes.Items.Add(RegistroAFD);
                                        }
                                        else
                                        {
                                            /*Trata o retorno de erro do REP*/
                                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                                            MessageBox.Show(Convert.ToString(MensagemErro),"Erro DLL",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                            return;
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
                        }
                        else
                        {
                            /*Houve erro no processamento do arquivo do pendrive*/
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                            MessageBox.Show(Convert.ToString(MensagemErro),"Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            return;
                        }
                    }
                }


                /**************************************************************************************************************************
                *MODO IP                                                                                                                  *
                ***************************************************************************************************************************/
                if (REPZPM_DLL.Modo == 0)
                {
                    /*Retorna a quantidade de registros*/
                    TotalRegistros = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

                    /*Verifica se retornaram registros*/
                    if (TotalRegistros > 0)
                    {
                        /*Limpa a listagem*/
                        Lst_Marcacoes.Items.Clear();

                        /*Executa a busca das marcações de ponto*/
                        for (i = 1; i <= TotalRegistros; i++)
                        {
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoLeituraMRP(REPZPM_DLL.Handle, i, RegistroAFD);

                            /*Sucesso na execução do comando*/
                            if (REPZPM_DLL.Retorno == 1)
                            {
                                Lst_Marcacoes.Items.Add(RegistroAFD);
                            }
                            else
                            {
                                /*Trata o retorno de erro do REP*/
                                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                                MessageBox.Show(Convert.ToString(MensagemErro),"Erro DLL",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        /*Não há registros no período selecionado*/
                        Lst_Marcacoes.Items.Clear();
                        Lst_Marcacoes.Items.Add("Não há marcações de ponto no período selecionado!");
                        return;
                    }
                }
            }
            else
            {
                REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando);
                Lst_Marcacoes.Items.Clear();
                return;
            }
        }
    }
}
