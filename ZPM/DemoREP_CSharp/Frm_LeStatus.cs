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
    public partial class Frm_LeStatus : Form
    {
        public Frm_LeStatus()
        {
            InitializeComponent();
        }

        private void Btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Confirmar_Click(object sender, EventArgs e)
        {
            StringBuilder NumeroFabricacao = new StringBuilder(17);
            StringBuilder UltimaMarcacaoPIS = new StringBuilder(12);
            StringBuilder UltimaMarcacaoDataHora = new StringBuilder(19);
            StringBuilder StatusPapel = new StringBuilder(1);
            StringBuilder DataHora = new StringBuilder(20);
            StringBuilder MemoriaTotalMRP = new StringBuilder(20);
            StringBuilder MemoriaUsoMRP = new StringBuilder(20);
            int TotalRegistros;
            StringBuilder MensagemErro = new StringBuilder(256);
                    
            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaStatus(REPZPM_DLL.Handle);

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
                                    Lst_Status.Items.Clear();

                                    /*Executa o comando de leitura do Status*/
                                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoStatus(REPZPM_DLL.Handle, NumeroFabricacao, UltimaMarcacaoPIS, UltimaMarcacaoDataHora, StatusPapel, DataHora, MemoriaTotalMRP, MemoriaUsoMRP);

                                    /*Sucesso na execução do comando*/
                                    if (REPZPM_DLL.Retorno == 1)
                                    {
                                        Lst_Status.Items.Add("Número de Série: " + NumeroFabricacao);
                                        Lst_Status.Items.Add("Última Marcação PIS: " + UltimaMarcacaoPIS);
                                        Lst_Status.Items.Add("Última Marcação DataHora: " + UltimaMarcacaoDataHora);
                                        Lst_Status.Items.Add("Status Papel: " + StatusPapel);
                                        Lst_Status.Items.Add("Data Hora: " + DataHora);
                                        Lst_Status.Items.Add("Memória Total MRP: " + MemoriaTotalMRP);
                                        Lst_Status.Items.Add("Memória Uso MRP: " + MemoriaUsoMRP);
                                    }
                                    else
                                    {
                                        /*Trata o retorno de erro do REP*/
                                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                                        MessageBox.Show(Convert.ToString(MensagemErro),"Erro DLL",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
                **************************************************************************************************************************/
                if (REPZPM_DLL.Modo == 0)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoStatus(REPZPM_DLL.Handle, NumeroFabricacao, UltimaMarcacaoPIS, UltimaMarcacaoDataHora, StatusPapel, DataHora, MemoriaTotalMRP, MemoriaUsoMRP);

                    /*Sucesso na execução do comando*/
                    if (REPZPM_DLL.Retorno == 1)
                    {
                        /*Limpa a listagem*/
                        Lst_Status.Items.Clear();

                        Lst_Status.Items.Add("Número de Série: " + NumeroFabricacao);
                        Lst_Status.Items.Add("Última Marcação PIS: " + UltimaMarcacaoPIS);
                        Lst_Status.Items.Add("Última Marcação DataHora: " + UltimaMarcacaoDataHora);
                        Lst_Status.Items.Add("Status Papel: " + StatusPapel);
                        Lst_Status.Items.Add("Data Hora: " + DataHora);
                        Lst_Status.Items.Add("Memória Total MRP: " + MemoriaTotalMRP);
                        Lst_Status.Items.Add("Memória Uso MRP: " + MemoriaUsoMRP);
                    }
                    else
                    {
                        /*Erro na execução do comando*/
                        REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno);
                        return;
                    }
                }
            }
            else
            {
                /*Trata o erro retornado pela DLL*/
                REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando);
            }
        }
    }
}
