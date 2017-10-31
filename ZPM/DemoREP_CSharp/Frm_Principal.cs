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
    public partial class Frm_Principal : Form
    {
        StringBuilder Versao = new StringBuilder(10);
        
        public Frm_Principal()
        {
            InitializeComponent();
        }

        private void iniciaDriverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_IniciaDriver IniciaDriver = new Frm_IniciaDriver();
            IniciaDriver.ShowDialog();
        }

        private void encerraDriverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_EncerraDriver(REPZPM_DLL.Handle);

            if (REPZPM_DLL.Retorno == 1)
            {
                MessageBox.Show("Driver encerrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro no encerramento do Driver. Favor verificar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void versãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            REPZPM_DLL.DLLREP_Versao(Versao);

            MessageBox.Show("Versao da DLL: " + Versao, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void defineIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_DefineModoIP DefineModoIP = new Frm_DefineModoIP();
            DefineModoIP.ShowDialog();
        }

        private void defineModoArquivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_DefineModoArquivo DefineModoArquivo = new Frm_DefineModoArquivo();
            DefineModoArquivo.ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void leModoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_LeModo(REPZPM_DLL.Handle);

            if (REPZPM_DLL.Retorno == 1)
            {
                MessageBox.Show("Modo IP configurado", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (REPZPM_DLL.Retorno == 2)
                {
                    MessageBox.Show("Modo Arquivo configurado", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nenhum Modo configurado!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void defineTimeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_DefineTimeout DefineTimeout = new Frm_DefineTimeout();
            DefineTimeout.ShowDialog();
        }

        private void leTimeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_LeTimeout(REPZPM_DLL.Handle);

            if (REPZPM_DLL.Retorno == -1)
            {
                MessageBox.Show("Erro na leitura do Timeout!","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Timeout definido: " + REPZPM_DLL.Retorno, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void configuraCódigoDeBarrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ConfiguraCodigoBarras ConfiguraCodigoBarras = new Frm_ConfiguraCodigoBarras();
            ConfiguraCodigoBarras.ShowDialog();
        }

        private void empregadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_CadastraEmpregador CadastraEmpregador = new Frm_CadastraEmpregador();
            CadastraEmpregador.ShowDialog();
        }

        private void funcionárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_CadastraFuncionario CadastraFuncionario = new Frm_CadastraFuncionario();
            CadastraFuncionario.ShowDialog();
        }

        private void atualizaDataHoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_AtualizaDataHora AtualizaDataHora = new Frm_AtualizaDataHora();
            AtualizaDataHora.ShowDialog();
        }

        private void ajustaHorárioDeVerãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_AjustaHorarioVerao AjustaHorarioVerao = new Frm_AjustaHorarioVerao();
            AjustaHorarioVerao.ShowDialog();
        }

        private void empregadorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Frm_LeEmpregador LeEmpregador = new Frm_LeEmpregador();
            LeEmpregador.ShowDialog();
        }

        private void todosOsFuncionáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LeTodosFuncionarios LeTodosFuncionarios = new Frm_LeTodosFuncionarios();
            LeTodosFuncionarios.ShowDialog();
        }

        private void buscaPontoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_BuscaPonto BuscaPonto = new Frm_BuscaPonto();
            BuscaPonto.ShowDialog();
        }

        private void buscaPontoAFDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_BuscaPontoAFD BuscaPontoAFD = new Frm_BuscaPontoAFD();
            BuscaPontoAFD.ShowDialog();
        }

        private void buscaStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LeStatus LeStatus = new Frm_LeStatus();
            LeStatus.ShowDialog();
        }

        private void buscaHorárioVerãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder DataInicio = new StringBuilder(10);
            StringBuilder DataFim = new StringBuilder(10);
            StringBuilder MensagemErro = new StringBuilder(256);
            int TotalRegistros;

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaHorarioVerao(REPZPM_DLL.Handle);

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
                                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoHorarioVerao(REPZPM_DLL.Handle, DataInicio, DataFim);

                                    /*Sucesso na execução do comando*/
                                    if (REPZPM_DLL.Retorno == 1)
                                    {
                                        if ((Convert.ToString(DataInicio) == "0") && (Convert.ToString(DataFim) == "0"))
                                        {
                                            MessageBox.Show("Não há nenhum Horário de Verão definido!","Sucesso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                            return;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Data Início: " + DataInicio + "\nData Fim: " + DataFim,"Sucesso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                            return;
                                        }
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
                ***************************************************************************************************************************/
                if (REPZPM_DLL.Modo == 0)
                {
                    /*Executa o comando de leitura*/
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoHorarioVerao(REPZPM_DLL.Handle, DataInicio, DataFim);

                    /*Sucesso na execução*/
                    if (REPZPM_DLL.Retorno == 1)
                    {
                        if ((Convert.ToString(DataInicio) == "0") && (Convert.ToString(DataFim) == "0"))
                        {
                            MessageBox.Show("Não há nenhum Horário de Verão definido!","Sucesso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Data Início: " + DataInicio + "\n    DataFim: " + DataFim,"Sucesso",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        /*Trata o erro da execução do comando*/
                        REPZPM_DLL.Retorno = REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno);
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

        private void limpaComandosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_LimpaComandos(REPZPM_DLL.Handle);

            if (REPZPM_DLL.Retorno == 1)
            {
                MessageBox.Show("Comando executado com sucesso!","Sucesso",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Erro na execução do comando!","Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Frm_Principal_Load(object sender, EventArgs e)
        {

        }
    }
}
