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
    public partial class Frm_BuscaPonto : Form
    {
        int TotalRegistros;
        
        public Frm_BuscaPonto()
        {
            InitializeComponent();
        }

        private void Btn_Sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_BuscaPonto_Load(object sender, EventArgs e)
        {
            Txt_DataInicial.Text = Convert.ToString(DateTime.Today.ToShortDateString());
            Txt_DataFinal.Text = Convert.ToString(DateTime.Today.ToShortDateString());
        }

        private void Btn_Localizar_Click(object sender, EventArgs e)
        {
            int i;
            StringBuilder PIS = new StringBuilder(11);
            StringBuilder DataHora = new StringBuilder(19);
            StringBuilder NSR = new StringBuilder(20);
            StringBuilder MensagemErro = new StringBuilder(256);

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaPonto(REPZPM_DLL.Handle, Txt_DataInicial.Text, Txt_DataFinal.Text);

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
                                    Lst_Marcacoes.Items.Clear();

                                    /*Executa a busca das marcações de ponto*/
                                    for (i = 1; i <= TotalRegistros; i++)
                                    {
                                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoPonto(REPZPM_DLL.Handle, i, PIS, DataHora, NSR);

                                        /*Sucesso na execução do comando*/
                                        if (REPZPM_DLL.Retorno == 1)
                                        {
                                            Lst_Marcacoes.Items.Add("Num. de Registro: " + Convert.ToString(i) + " de " + Convert.ToString(TotalRegistros));
                                            Lst_Marcacoes.Items.Add("PIS: " + PIS);
                                            Lst_Marcacoes.Items.Add("Data/Hora: " + DataHora);
                                            Lst_Marcacoes.Items.Add("NSR: " + NSR);
                                            Lst_Marcacoes.Items.Add("\n");
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
                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoPonto(REPZPM_DLL.Handle, i, PIS, DataHora, NSR);

                        /*Sucesso na execução do comando*/
                        if (REPZPM_DLL.Retorno == 1)
                        {
                            Lst_Marcacoes.Items.Add("Num. de Registro: " + Convert.ToString(i) + " de " + Convert.ToString(TotalRegistros));
                            Lst_Marcacoes.Items.Add("PIS: " + PIS);
                            Lst_Marcacoes.Items.Add("Data/Hora: " + DataHora);
                            Lst_Marcacoes.Items.Add("NSR: " + NSR);
                            Lst_Marcacoes.Items.Add("\n");
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

    }
}
