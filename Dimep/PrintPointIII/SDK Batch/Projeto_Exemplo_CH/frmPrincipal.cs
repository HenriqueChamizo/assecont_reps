using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using org.cesar.dmplight.watchComm.business;
using ExemploREPCH.DataSet;
using org.cesar.dmplight.watchComm.impl.printpointli;
using org.cesar.dmplight.watchComm.impl.printpoint;

namespace ExemploREPCH
{
    public partial class frmPrincipal : Form
    {
        #region " ---------------------> Atributos <---------------------"
        
        private org.cesar.dmplight.watchComm.impl.WatchComm _watchComm;
        private dsSDKREP _dsREP;
        private dsSDKREP.dtMRPMarcacaoDataTable _dtMarcacoes;
        private dsSDKREP.dtStatusDataTable _dtStatus;
        private dsSDKREP.dtFuncionariosDataTable _dtFuncionarios;
        private dsSDKREP.dtTemplatesRecebidoDataTable _dtTemplatesRecebidos;
        private dsSDKREP.dtTemplatesParaEnvioDataTable _dtTemplatesParaEnvio;
        #endregion

        #region " ---------------------> Construtor <---------------------"
        
        public frmPrincipal()
        {
            InitializeComponent();

            this._dsREP = new dsSDKREP();
            BindingDataTables();

            SetDefaults();
        }
        #endregion

        #region " ---------------------> Métodos Privados <---------------------"
        
        private void SetDefaults()
        {
            this.dtpData.Value = DateTime.Now;
            this.dtpHora.Value = DateTime.Now;
            this.rdbDataHoraAtual.Checked = true;

            BindingDataGrids();

            this.cboTipoPessoa.SelectedIndex = 0;
        }

        private void BindingDataGrids()
        {
            this.dtgListaFuncionarios.DataSource = this._dtFuncionarios;
            this.dtgEnvioTemplates.DataSource = this._dtTemplatesParaEnvio;
            this.dtgRecebimentoTemplates.DataSource = this._dtTemplatesRecebidos;
            this.dtgMarcacoes.DataSource = this._dtMarcacoes;
        }

        private void BindingDataTables()
        {                        
            this._dtFuncionarios = this._dsREP.dtFuncionarios;
            this._dtTemplatesRecebidos = this._dsREP.dtTemplatesRecebido;
            this._dtTemplatesParaEnvio = this._dsREP.dtTemplatesParaEnvio;
            this._dtMarcacoes = this._dsREP.dtMRPMarcacao;
            this._dtStatus = this._dsREP.dtStatus;
        }

        private void InstanciaWatchComm()
        {
            string key = "";

            if (System.IO.File.Exists(this.CaminhoEXE() + "\\key.txt"))
            {
                System.IO.StreamReader streamReader = System.IO.File.OpenText(this.CaminhoEXE() + "\\key.txt");
                key = streamReader.ReadLine();

                streamReader.Close();
            }

            org.cesar.dmplight.watchComm.api.TCPComm tcpComm =
                new org.cesar.dmplight.watchComm.api.TCPComm(this.txtIPRelogio.Text, 4370);

            tcpComm.SetTimeOut(15000);

            this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.PrintPointLi,
                                                                              tcpComm,
                                                                              1,
                                                                              key,
                                                                              org.cesar.dmplight.watchComm.api.WatchConnectionType.ConnectedMode,
                                                                              "01.00.0000");
        }

        private String CaminhoEXE()
        {
            String nomeEXE = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath).InternalName;
            String retorno = System.Windows.Forms.Application.ExecutablePath.ToUpper().Replace(nomeEXE.ToUpper(), "");

            return retorno;
        }        

        private void ErroDuranteRecepcaoDoComando(Exception ex)
        {
            MessageBox.Show("Ocorreu um erro durante a tentativa de envio do comando!" +
                            "\nErro: " + ex.Message, Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }

        private void ComandoRecepcionadoComSucesso()
        {
            MessageBox.Show("Comando recepcionado com sucesso pelo relógio!", Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region " ---------------------> Leitura e gravação do endereço IP informado. <---------------------"

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(CaminhoEXE() + "\\EnderecoIP.txt");
            streamWriter.Write(this.txtIPRelogio.Text);
            streamWriter.Close();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(CaminhoEXE() + "\\EnderecoIP.txt"))
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(CaminhoEXE() + "\\EnderecoIP.txt");
                this.txtIPRelogio.Text = streamReader.ReadLine();
                streamReader.Close();
            }
        }
        #endregion

        #region " ---------------------> Leitura e gravação das informações contidas nos grids <---------------------"
        private void btnGravarXML_Click(object sender, EventArgs e)
        {
            this._dsREP.WriteXml(this.CaminhoEXE() + "\\DataDSREPCH.xml");

            MessageBox.Show("XML gravado com sucesso!", Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnLerXML_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(this.CaminhoEXE() + "\\DataDSREPCH.xml"))
            {
                this._dsREP = new dsSDKREP();
                this._dsREP.ReadXml(this.CaminhoEXE() + "\\DataDSREPCH.xml");

                BindingDataTables();
                BindingDataGrids();

                MessageBox.Show("XML lido com sucesso!", Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("O arquivo .XML utilizado para armazenar as informações do grid, não foi encontrado!",
                                Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Geral <---------------------"
        
        private void btnEnviarDataHora_Click(object sender, EventArgs e)
        {
            DateTime dtEnvio;

            InstanciaWatchComm();

            if (this.rdbDataHoraAtual.Checked == true)

                dtEnvio = DateTime.Now;
            else

                dtEnvio = new DateTime(this.dtpData.Value.Year,
                                       this.dtpData.Value.Month,
                                       this.dtpData.Value.Day,
                                       this.dtpHora.Value.Hour,
                                       this.dtpHora.Value.Minute,
                                       this.dtpHora.Value.Second);

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.SetDateTime(dtEnvio);

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {                
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                this._watchComm.CloseConnection();
            }
        }

        private void btnHorarioVerao_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                if (this.rdbUsaHorarioVerao.Checked == true)
                    this._watchComm.SetDST(new DateTime(this.dtInicioHorarioVerao.Value.Year, this.dtInicioHorarioVerao.Value.Month, this.dtInicioHorarioVerao.Value.Day), 
                                           new DateTime(this.dtFimHorarioVerao.Value.Year, this.dtFimHorarioVerao.Value.Month, this.dtFimHorarioVerao.Value.Day));
                else
                    this._watchComm.RemoveDST();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                this._watchComm.CloseConnection();
            }
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Empregador <---------------------"
        
        public bool ValidaCamposEmpregador()
        {
            bool blnRetorno = true;

            if (this.txtNomeEmpresa1.Text.Trim().Length == 0 &
                this.txtNomeEmpresa2.Text.Trim().Length == 0 &
                this.txtNomeEmpresa3.Text.Trim().Length == 0 &
                this.txtNomeEmpresa4.Text.Trim().Length == 0 &
                this.txtNomeEmpresa5.Text.Trim().Length == 0)
            {

                ErrorProvider.SetError(this.txtNomeEmpresa1, "Informação obrigatória.");
                ErrorProvider.SetError(this.txtNomeEmpresa2, "");
                ErrorProvider.SetError(this.txtNomeEmpresa3, "");
                ErrorProvider.SetError(this.txtNomeEmpresa4, "");
                ErrorProvider.SetError(this.txtNomeEmpresa5, "");

                blnRetorno = blnRetorno & false;
            }
            else
            {
                ErrorProvider.SetError(this.txtNomeEmpresa1, "");
                blnRetorno = blnRetorno & true;
            }

            if (this.txtEnderecoEmpresa1.Text.Trim().Length == 0 &
                this.txtEnderecoEmpresa2.Text.Trim().Length == 0 &
                this.txtEnderecoEmpresa3.Text.Trim().Length == 0)
            {

                ErrorProvider.SetError(this.txtEnderecoEmpresa1, "Informação obrigatória.");
                ErrorProvider.SetError(this.txtEnderecoEmpresa2, "");
                ErrorProvider.SetError(this.txtEnderecoEmpresa3, "");

                blnRetorno = blnRetorno & false;
            }
            else
            {

                ErrorProvider.SetError(this.txtEnderecoEmpresa1, "");
                blnRetorno = blnRetorno & true;
            }

            if (this.cboTipoPessoa.SelectedIndex == 1)
            {
                // CPF.

                if (this.mskCPF_CNPJ.Text.Trim().Length != 14)
                {
                    ErrorProvider.SetError(this.mskCPF_CNPJ, "Informação obrigatória.");
                    blnRetorno = blnRetorno & false;
                }
                else
                {
                    ErrorProvider.SetError(this.mskCPF_CNPJ, "");
                    blnRetorno = blnRetorno & true;
                }
            }
            else
            {
                // CNPJ.

                if (this.mskCPF_CNPJ.Text.Trim().Length != 18)
                {
                    ErrorProvider.SetError(this.mskCPF_CNPJ, "Informação obrigatória.");
                    blnRetorno = blnRetorno & false;
                }
                else
                {
                    ErrorProvider.SetError(this.mskCPF_CNPJ, "");
                    blnRetorno = blnRetorno & true;
                }
            }

            Double aux;

            if (Double.TryParse(mskCEI.Text, out aux) &&
                Double.Parse(mskCEI.Text) != 0.0)
            {
                if (Util.RetiraFormatacao(this.mskCEI.Text).Trim().Length != 0)
                {
                    if (this.mskCEI.Text.Trim().Length != 15)
                    {
                        ErrorProvider.SetError(this.mskCEI, "CEI Inválido.");
                        blnRetorno = blnRetorno & false;
                    }
                    else
                    {
                        ErrorProvider.SetError(this.mskCEI, "");
                        blnRetorno = blnRetorno & true;
                    }
                }
                else
                {
                    ErrorProvider.SetError(this.mskCEI, "");
                    blnRetorno = blnRetorno & true;
                }
            }

            return blnRetorno;
        }

        private void btnEnviarEmpregadorRelogio_Click(object sender, EventArgs e)
        {
            String endereco;
            String razaoSocial;
            org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType tipoEmpregador;

            InstanciaWatchComm();

            if (!ValidaCamposEmpregador())
                return;

            try
            {
                endereco = this.txtEnderecoEmpresa1.Text.PadRight(36) +
                           this.txtEnderecoEmpresa2.Text.PadRight(36) +
                           this.txtEnderecoEmpresa3.Text.PadRight(28);

                razaoSocial = this.txtNomeEmpresa1.Text.PadRight(36) +
                              this.txtNomeEmpresa2.Text.PadRight(36) +
                              this.txtNomeEmpresa3.Text.PadRight(36) +
                              this.txtNomeEmpresa4.Text.PadRight(36) +
                              this.txtNomeEmpresa5.Text.PadRight(6);

                if (this.cboTipoPessoa.SelectedIndex == 0)
                    tipoEmpregador = org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CNPJ;
                else
                    tipoEmpregador = org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CPF;

                try
                {
                    _watchComm.OpenConnection();

                    this._watchComm.ChangeEmployer(tipoEmpregador,
                                                   Util.RetiraFormatacao(this.mskCPF_CNPJ.Text),
                                                   Util.RetiraFormatacao(this.mskCEI.Text),
                                                   razaoSocial.Trim(),
                                                   endereco.Trim());
                    
                    ComandoRecepcionadoComSucesso();
                }
                catch (Exception ex2)
                {
                    ErroDuranteRecepcaoDoComando(ex2);
                }
                finally
                {
                    _watchComm.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao preparar as informações para enviar a alteração de empregador " +
                                "para o relógio." +
                                "\nErro: " + ex.Message, Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboTipoPessoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboTipoPessoa.SelectedIndex == 1)
            {

                this.lblCPF_CNPJ.Text = "CPF";
                this.mskCPF_CNPJ.Mask = "000,000,000-00";
            }
            else
            {
                this.lblCPF_CNPJ.Text = "CNPJ";
                this.mskCPF_CNPJ.Mask = "00,000,000/0000-00";
            }
        }        

        private void btnObterEmpregadorRelogio_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                _watchComm.OpenConnection();

                org.cesar.dmplight.watchComm.impl.printpoint.PrintPointEmployerMessage empregador =
                    this._watchComm.InquiryEmployeer();

                CarregaDadosEmpregador(empregador);                                

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                _watchComm.CloseConnection();
            }
        }

        public void CarregaDadosEmpregador(org.cesar.dmplight.watchComm.impl.printpoint.PrintPointEmployerMessage empregador)
        {
            this.mskCEI.Text = empregador.CEI.PadLeft(12, Char.Parse("0")).Substring(0, 12);

            if (empregador.EmployerType == org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CPF)
                this.cboTipoPessoa.SelectedIndex = 1;
            else
                this.cboTipoPessoa.SelectedIndex = 0;
                        
            this.mskCPF_CNPJ.Text = empregador.CPF_CNPJ;

            empregador.Name = empregador.Name.PadRight(150, Char.Parse(" "));

            this.txtNomeEmpresa1.Text = empregador.Name.Substring(0, 36);

            if (empregador.Name.Length > 36)
                this.txtNomeEmpresa2.Text = empregador.Name.Substring(36, 36);

            if (empregador.Name.Length > 72)
                this.txtNomeEmpresa3.Text = empregador.Name.Substring(72, 36);

            if (empregador.Name.Length > 108)
                this.txtNomeEmpresa4.Text = empregador.Name.Substring(108, 36);

            if (empregador.Name.Length > 144)
                this.txtNomeEmpresa5.Text = empregador.Name.Substring(144);

            empregador.Address = empregador.Address.PadRight(100, Char.Parse(" "));

            this.txtEnderecoEmpresa1.Text = empregador.Address.Substring(0, 36);

            if (empregador.Address.Length > 36)
                this.txtEnderecoEmpresa2.Text = empregador.Address.Substring(36, 36);

            if (empregador.Address.Length > 72)
                this.txtEnderecoEmpresa3.Text = empregador.Address.Substring(72);
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Coleta <---------------------"
        
        private void btnObterRegistrosMRP_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();
            Boolean ocorreuErroDuranteColeta = false;

            MRPRecord_RegistrationMarkingPoint[] marcacoes;

            try
            {
                this._watchComm.OpenConnection();

                marcacoes = this._watchComm.InquiryMRPRecords(this.txtFiltroNSR.Text == "" ? "0" : this.txtFiltroNSR.Text,
                                                              ref ocorreuErroDuranteColeta);

                if (marcacoes != null)
                {
                    foreach (MRPRecord_RegistrationMarkingPoint marcacao in marcacoes)
                    {                            
                        DataRow dr = this._dtMarcacoes.NewRow();
                        dr["NSR"] = marcacao.NSR;
                        dr["DataHoraGravacao"] = marcacao.DateTimeMarkingPoint;
                        dr["PIS"] = marcacao.Pis;
                        this._dtMarcacoes.Rows.Add(dr);                         
                    }
                }

                if (!ocorreuErroDuranteColeta)
                    MessageBox.Show("Comando recepcionado com sucesso pelo relógio!", Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Coleta parcialmente realizada! Possívelmente ainda existem registros a serem coletados", 
                                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                this._watchComm.CloseConnection();
            }
        }

        private void btnLimparGrids_Click(object sender, EventArgs e)
        {
            _dtMarcacoes = new dsSDKREP.dtMRPMarcacaoDataTable();            
            
            this.dtgMarcacoes.DataSource = _dtMarcacoes;            
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Status <---------------------"

        private void btnObterStatus_Click(object sender, EventArgs e)
        {
            this._dtStatus = new dsSDKREP.dtStatusDataTable();
            this.dtgStatus.DataSource = this._dtStatus;

            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                PrintPointLiStatus status = this._watchComm.GetPrintPointLiStatus();

                if (status == null)
                {
                    MessageBox.Show("O comando de status não foi recepcionado corretamente!", Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

                CarregaGridStatus(status);

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {                
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                this._watchComm.CloseConnection();
            }
        }

        private void CarregaGridStatus(PrintPointLiStatus status)
        {
            DataRow dr;

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Data e Hora";
            dr["Valor"] = status.DeviceDateTime;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade de Registros";
            dr["Valor"] = status.RecordsCapacity;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade Total de Registros";
            dr["Valor"] = status.RecordsTotal;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade de Digitais";
            dr["Valor"] = status.FingerprintCapacity;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade de Usuários";
            dr["Valor"] = status.UsersCapacity;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade de Supervisores";
            dr["Valor"] = status.MasterOccupation;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade de Senhas";
            dr["Valor"] = status.PasswordOccupation;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade de Digitais";
            dr["Valor"] = status.FingerprintOccupation;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade de Usuários";
            dr["Valor"] = status.UserOccupation;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Versão do Firmware";
            dr["Valor"] = status.FirmwareVersion;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Versão da MRP";
            dr["Valor"] = status.MRPVersion;
            this._dtStatus.Rows.Add(dr);
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Funcionários <---------------------"

        private void btnObterFuncionarios_Click(object sender, EventArgs e)
        {
            this._dtFuncionarios.Rows.Clear();

            InstanciaWatchComm();

            PrintPointEmployee[] employees;

            try
            {
                this._watchComm.OpenConnection();
                
                employees = this._watchComm.InquiryEmployeeList();

                if (employees != null)
                {    
                    foreach (PrintPointLiEmployee employee in employees)
                    {
                        DataRow dr = this._dtFuncionarios.NewRow();
                        dr["PIS"] = employee.Pis;
                        dr["Nome"] = employee.Name;
                        dr["Senha"] = employee.Password == "" ? "0" : employee.Password;
                        dr["ID"] = employee.EmployeeID;
                        dr["CPF"] = employee.Cpf;
                        dr["Credencial"] = employee.Credential;
                        dr["Supervisor"] = employee.IsMaster;

                        this._dtFuncionarios.Rows.Add(dr);
                    }

                    this.dtgListaFuncionarios.DataSource = null;
                    this.dtgListaFuncionarios.DataSource = this._dtFuncionarios;
                }

                ComandoRecepcionadoComSucesso();

                this.lblTotalFuncionarios.Text = "Total de Funcionários: " + this._dtFuncionarios.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                this._watchComm.CloseConnection();
            }
        }

        private void btnIncluirFuncionarios_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                // Insere os funcionários na lista do componente.
                foreach (DataRow dr in this._dtFuncionarios)
                {
                    try
                    {
                        this._watchComm.AddEmployee(Int32.Parse(dr["ID"].ToString()), 
                                                    dr["PIS"].ToString(), 
                                                    dr["CPF"].ToString(), 
                                                    dr["Credencial"].ToString(), 
                                                    dr["Nome"].ToString(), 
                                                    dr["Senha"].ToString(), 
                                                    (Boolean)dr["Supervisor"]);
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }

                // Solicita ao componente o envio da lista de funcionários.
                try
                {
                    this._watchComm.IncludeEmployeesList();
                }
                catch (Exception ex)
                {
                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
            }
            finally
            {
                this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnExcluirFuncionarios_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                // Insere os funcionários na lista do componente.
                foreach (DataRow dr in this._dtFuncionarios)
                {
                    try
                    {
                        this._watchComm.AddEmployee(Int32.Parse(dr["ID"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }

                // Solicita ao componente a exclusão da lista de funcionários.
                try
                {
                    this._watchComm.ExcludeEmployeesList();
                }
                catch (Exception ex)
                {
                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
            }
            finally
            {
                this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Templates <---------------------"
        
        private void btnObterTemplates_Click(object sender, EventArgs e)
        {
            this._dtTemplatesRecebidos.Rows.Clear();

            InstanciaWatchComm();

            PrintPointEmployee[] employees;

            try
            {
                this._watchComm.OpenConnection();

                employees = this._watchComm.InquiryEmployeeList();
                
                foreach (PrintPointLiEmployee employee in employees)
                {
                    PrintPointLiFingerPrint[] fingerPrints = this._watchComm.InquiryFingerPrint(employee.EmployeeID);

                    foreach (PrintPointLiFingerPrint fingerPrint in fingerPrints)
                    {
                        DataRow dr = this._dtTemplatesRecebidos.NewRow();                            
                        dr["ID"] = employee.EmployeeID;
                        dr["Dedo"] = (Int16)fingerPrint.FingerPrintType;
                        dr["Template"] = fingerPrint.FingerPrint;

                        this._dtTemplatesRecebidos.Rows.Add(dr);
                    }
                }

                this.dtgRecebimentoTemplates.DataSource = null;
                this.dtgRecebimentoTemplates.DataSource = this._dtTemplatesRecebidos;                

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                this._watchComm.CloseConnection();
            }
        }

        private void btnCopiarTemplatesParaGridEnvio_Click(object sender, EventArgs e)
        {
            foreach (DataRow drTemplateRecebido in this._dtTemplatesRecebidos.Rows)
            {
                DataRow drTemplateParaEnvio = this._dtTemplatesParaEnvio.NewRow();

                drTemplateParaEnvio["ID"] = drTemplateRecebido["ID"];
                drTemplateParaEnvio["Dedo"] = drTemplateRecebido["Dedo"];                
                drTemplateParaEnvio["Template"] = drTemplateRecebido["Template"];

                this._dtTemplatesParaEnvio.Rows.Add(drTemplateParaEnvio);
            }

            this.dtgEnvioTemplates.DataSource = null;
            this.dtgEnvioTemplates.DataSource = this._dtTemplatesParaEnvio;            
        }

        private void btnExclusaoTemplates_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            foreach (DataRow dr in this._dtTemplatesParaEnvio)
            {
                try
                {                   
                    _watchComm.OpenConnection();

                    this._watchComm.ExcludeFingerPrint(Int32.Parse(dr["ID"].ToString()), (EFingerPrintType)Int32.Parse(dr["Dedo"].ToString()));
                }
                catch (Exception ex)
                {
                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
                finally
                {
                    _watchComm.CloseConnection();
                }
            }

            if (this._dtTemplatesParaEnvio.Rows.Count > 0)
                ComandoRecepcionadoComSucesso();
        }

        private void btnInclusaoTemplates_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            foreach (DataRow dr in this._dtTemplatesParaEnvio)
            {
                try
                {
                    _watchComm.OpenConnection();

                    this._watchComm.IncludeFingerPrint(Int32.Parse(dr["ID"].ToString()),
                                                       (EFingerPrintType)Int32.Parse(dr["Dedo"].ToString()),
                                                       dr["Template"].ToString());
                }
                catch (Exception ex)
                {
                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
                finally
                {
                    _watchComm.CloseConnection();
                }
            }

            if (this._dtTemplatesParaEnvio.Rows.Count > 0)
                ComandoRecepcionadoComSucesso();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.Set1To1();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                this._watchComm.CloseConnection();
            }
        }
    }
}
