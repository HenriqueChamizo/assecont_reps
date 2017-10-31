using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RepTrilobit;

namespace RepExemplo
{
    public partial class frmExemplo : Form
    {
        //Criar um objeto da classe REP
        private REP _REP;

        //Variáveis para armazenar IP, Porta e Senha do REP
        private string IP;
        private Int32 Porta;
        private Int32 Senha;

        public frmExemplo()
        {
            InitializeComponent();
        }

        private void frmExemplo_Load(object sender, EventArgs e)
        {
            //Instanciar o objeto.
            _REP = new REP();

            CarregarComboSetConfig();
            CarregarComboGetConfig();
            cboTipoDoc.SelectedIndex = 0;
            cboTipoDoc_SelectedIndexChanged(cboTipoDoc, new EventArgs());
        }

        #region "Preencher combos e atualizar IP"
        private void AtualizarIP()
        {
            try
            {
                IP = txtIP.Text;
                Porta = Int32.Parse(txtPorta.Text);
                Senha = Int32.Parse(txtSenha.Text);
            }
            catch
            {
                throw;
            }
        }

        private void CarregarComboSetConfig()
        {
            foreach (REP.eParamSetConfig ParamSet in Enum.GetValues(typeof(REP.eParamSetConfig)))
                cboParamSet.Items.Add(ParamSet.ToString());
        }

        private void CarregarComboGetConfig()
        {
            foreach (REP.eParamGetConfig ParamGet in Enum.GetValues(typeof(REP.eParamGetConfig)))
                cboParamGet.Items.Add(ParamGet.ToString());
        }
        #endregion

        private void btnCadastrarEmpregador_Click(object sender, EventArgs e)
        {
            try
            {
                //Atualizar valor das variáveis IP, Porta e Senha
                AtualizarIP();

                //Definir tipo de documento do empregador
                REP.eTipoDocumento Tipo;
                if (cboTipoDoc.SelectedItem.Equals("CNPJ"))
                {
                    Tipo = REP.eTipoDocumento.CNPJ;
                }
                else
                {
                    Tipo = REP.eTipoDocumento.CPF;
                }

                //Enviar as configurações do empregador ao REP indicado.
                string Documento = txtDocumento.Text;
                string CEI = txtCEI.Text;
                string RazaoSocial = txtRazao.Text;
                string Local = txtLocal.Text;

                //Chamar o método que cadastra o empregador no REP.
                //Caso o retorno seja FALSE, significa que ocorreu um erro.
                //Uma descrição do erro ocorrido estará disponível na 
                //propriedade ErrorException.
                if (!_REP.CadastrarEmpregador(IP, Porta, Senha, Tipo, Documento, CEI, RazaoSocial, Local))
                {
                    MessageBox.Show(_REP.ErrorException.Message);
                }
                else
                {
                    MessageBox.Show("Cadastro enviado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCadastrarEmpregado_Click(object sender, EventArgs e)
        {
            try
            {
                //Atualizar valor das variáveis IP, Porta e Senha
                AtualizarIP();

                //Enviar as configurações do empregado ao REP indicado.
                string PIS = txtPIS.Text;
                string Nome = txtNome.Text;
                string Cracha = txtCracha.Text;
                bool PossuiBio = chkPossuiBio.Checked;

                //Chamar o método que cadastra o empregado no REP.
                //Caso o retorno seja FALSE, significa que ocorreu um erro.
                //Uma descrição do erro ocorrido estará disponível na 
                //propriedade ErrorException.
                if (!_REP.CadastrarEmpregado(IP, Porta, Senha, PIS, Nome, Cracha, PossuiBio))
                {
                    MessageBox.Show(_REP.ErrorException.Message);
                }
                else
                {
                    MessageBox.Show("Cadastro enviado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExcluirEmpregado_Click(object sender, EventArgs e)
        {
            try
            {
                //Atualizar valor das variáveis IP, Porta e Senha
                AtualizarIP();

                //Enviar as configurações do empregado ao REP indicado.
                string PIS = txtPIS.Text;

                //Chamar o método que exclui o empregado no REP.
                //Caso o retorno seja FALSE, significa que ocorreu um erro.
                //Uma descrição do erro ocorrido estará disponível na 
                //propriedade ErrorException.
                if (!_REP.ExcluirEmpregado(IP, Porta, Senha, PIS))
                {
                    MessageBox.Show(_REP.ErrorException.Message);
                }
                else
                {
                    MessageBox.Show("Exclusão realizada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnParamSet_Click(object sender, EventArgs e)
        {
            try
            {
                //Atualizar valor das variáveis IP, Porta e Senha
                AtualizarIP();

                //Recuperar o parâmetro do combo.
                REP.eParamSetConfig Parametro=0;
                foreach (REP.eParamSetConfig ParamSet in Enum.GetValues(typeof(REP.eParamSetConfig)))
                {
                    if (ParamSet.ToString() == cboParamSet.SelectedItem.ToString())
                        Parametro = ParamSet;
                }
                string Valor = txtParamSet.Text;

                //Chamar o método que envia configuração ao REP.
                //Caso o retorno seja FALSE, significa que ocorreu um erro.
                //Uma descrição do erro ocorrido estará disponível na 
                //propriedade ErrorException.
                if (!_REP.EnviarConfiguracao(IP, Porta, Senha, Parametro, Valor))
                {
                    MessageBox.Show(_REP.ErrorException.Message);
                }
                else
                {
                    MessageBox.Show("Configuração enviada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnParamGet_Click(object sender, EventArgs e)
        {
            try
            {
                //Atualizar valor das variáveis IP, Porta e Senha
                AtualizarIP();

                //Recuperar o parâmetro do combo.
                REP.eParamGetConfig Parametro = 0;
                foreach (REP.eParamGetConfig ParamGet in Enum.GetValues(typeof(REP.eParamGetConfig)))
                {
                    if (ParamGet.ToString() == cboParamGet.SelectedItem.ToString())
                        Parametro = ParamGet;
                } 
                string Valor = "";

                //Chamar o método que lê a configuração do REP.
                //Caso o retorno seja FALSE, significa que ocorreu um erro.
                //Uma descrição do erro ocorrido estará disponível na 
                //propriedade ErrorException.
                if (!_REP.LerConfiguracao(IP, Porta, Senha, Parametro, ref Valor))
                {
                    MessageBox.Show(_REP.ErrorException.Message);
                    txtParamGet.Text = "";
                }
                else
                {
                    txtParamGet.Text = Valor;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLerAFD_Click(object sender, EventArgs e)
        {
            try
            {
                //Atualizar valor das variáveis IP, Porta e Senha
                AtualizarIP();

                string Arquivo = txtArquivo.Text;
                string DataInicial =
                    dtpDataInicial.Value.Year.ToString().PadLeft(4, '0') +
                    dtpDataInicial.Value.Month.ToString().PadLeft(2, '0') +
                    dtpDataInicial.Value.Day.ToString().PadLeft(2, '0');

                string DataFinal =
                    dtpDataFinal.Value.Year.ToString().PadLeft(4, '0') +
                    dtpDataFinal.Value.Month.ToString().PadLeft(2, '0') +
                    dtpDataFinal.Value.Day.ToString().PadLeft(2, '0');

                //Chamar o método que cria o arquivo AFD lido a partir do REP.
                //Caso o retorno seja FALSE, significa que ocorreu um erro.
                //Uma descrição do erro ocorrido estará disponível na 
                //propriedade ErrorException.
                if (!_REP.LerAFD(IP, Porta, Senha, DataInicial, DataFinal, Arquivo))
                {
                    MessageBox.Show(_REP.ErrorException.Message);
                }
                else
                {
                    MessageBox.Show("Arquivo gerado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLerAFD_DataTable_Click(object sender, EventArgs e)
        {
            try
            {
                //Atualizar valor das variáveis IP, Porta e Senha
                AtualizarIP();

                string Arquivo = txtArquivo.Text;
                string DataInicial =
                    dtpDataInicial.Value.Year.ToString().PadLeft(4, '0') +
                    dtpDataInicial.Value.Month.ToString().PadLeft(2, '0') +
                    dtpDataInicial.Value.Day.ToString().PadLeft(2, '0');

                string DataFinal =
                    dtpDataFinal.Value.Year.ToString().PadLeft(4, '0') +
                    dtpDataFinal.Value.Month.ToString().PadLeft(2, '0') +
                    dtpDataFinal.Value.Day.ToString().PadLeft(2, '0');

                //Chamar o método que devolve um DataTable com dados do AFD lido a partir do REP.
                //Caso o retorno seja FALSE, significa que ocorreu um erro.
                //Uma descrição do erro ocorrido estará disponível na 
                //propriedade ErrorException.
                DataTable Tabela = null;
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();

                Int32 NSR = 0;
                Int32.TryParse(txtNSR.Text, out NSR);

                if (!_REP.LerAFD(IP, Porta, Senha, DataInicial, DataFinal, ref Tabela, NSR))
                {
                    MessageBox.Show(_REP.ErrorException.Message);
                }
                else
                {
                    if (Tabela != null)
                    {
                        //Se vierem apenas 2 registros, significa que não há
                        //dados na MRP, pois há sempre os registros de Cabeçalho
                        //e Trailler no DataTable.
                        if (Tabela.Rows.Count > 2)
                        {
                            dataGridView1.DataSource = Tabela;
                            dataGridView1.Refresh();
                            MessageBox.Show("Tabela gerada com sucesso!");
                        }
                        else
                        {
                            MessageBox.Show("Nenhum registro localizado!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nenhum registro localizado!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LerEmpregadosTXT_Click(object sender, EventArgs e)
        {
            try
            {
                //Atualizar valor das variáveis IP, Porta e Senha
                AtualizarIP();

                string Arquivo = txtArquivoEmpregados.Text;
                bool AddCabecalho = chkAddCabecalho.Checked;


                //Chamar o método que cria o arquivo AFD lido a partir do REP.
                //Caso o retorno seja FALSE, significa que ocorreu um erro.
                //Uma descrição do erro ocorrido estará disponível na 
                //propriedade ErrorException.
                if (!_REP.LerEmpregados(IP, Porta, Senha, Arquivo, AddCabecalho))
                {
                    MessageBox.Show(_REP.ErrorException.Message);
                }
                else
                {
                    MessageBox.Show("Arquivo gerado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LerEmpregadosDT_Click(object sender, EventArgs e)
        {
            try
            {
                //Atualizar valor das variáveis IP, Porta e Senha
                AtualizarIP();

                DataTable Tabela = null;

                //Chamar o método que cria o arquivo AFD lido a partir do REP.
                //Caso o retorno seja FALSE, significa que ocorreu um erro.
                //Uma descrição do erro ocorrido estará disponível na 
                //propriedade ErrorException.
                if (!_REP.LerEmpregados(IP, Porta, Senha, ref Tabela))
                {
                    MessageBox.Show(_REP.ErrorException.Message);
                }
                else
                {
                    if (Tabela != null)
                    {
                        dataGridView2.DataSource = Tabela;
                        dataGridView2.Refresh();
                        MessageBox.Show("Tabela gerada com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Nenhum registro localizado!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoDoc.Text.Equals("CPF"))
            {
                txtDocumento.Mask = "999,999,999-99";
            }
            else
            {
                txtDocumento.Mask = "99,999,999/9999-99";
            }
        }

        private void txtDocumento_Enter(object sender, EventArgs e)
        {
            txtDocumento.Focus();
            txtDocumento.SelectAll();
        }

    }

    //#region "Classe usada para armazenar pares do tipo Chave/Valor."
    //internal class ChaveValor
    //{
    //    public int Chave { get; set; }
    //    public string Valor { get; set; }

    //    public ChaveValor(int Codigo, string Descricao)
    //    {
    //        this.Chave = Codigo;
    //        this.Valor = Descricao;
    //    }

    //    public override string ToString()
    //    {
    //        return this.Valor;
    //    }
    //}
    //#endregion
}
