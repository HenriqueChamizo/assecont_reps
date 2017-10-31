using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using org.cesar.dmplight.watchComm.business;
using org.cesar.dmplight.watchComm.impl.printpointli;
using org.cesar.dmplight.watchComm.impl.printpoint;
using ExemploBIO.DataSet;
using System.Collections;
using org.cesar.dmplight.watchComm.api;
using org.cesar.dmplight.watchComm.business;
using org.cesar.dmplight.watchComm.impl.biopoint;

namespace ExemploBIO
{
    public partial class frmPrincipal : Form
    {
        #region " ---------------------> Atributos <---------------------"

        private org.cesar.dmplight.watchComm.impl.WatchComm _watchComm;
        private dsSDK _dsREP;
        private dsSDK.dtMarcacaoDataTable _dtMarcacoes;
        private dsSDK.dtStatusDataTable _dtStatus;
        private dsSDK.dtStatusDataTable _dtFormatoMemoria;
        private dsSDK.dtFuncionariosDataTable _dtFuncionarios;
        private dsSDK.dtTemplatesRecebidoDataTable _dtTemplatesRecebidos;
        private dsSDK.dtTemplatesParaEnvioDataTable _dtTemplatesParaEnvio;
        private dsSDK.dtSupervisoresDataTable _dtSupervisores;
        #endregion

        #region " ---------------------> Construtor <---------------------"

        public frmPrincipal()
        {
            InitializeComponent();

            this._dsREP = new dsSDK();
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
        }

        private void BindingDataGrids()
        {
            this.dtgListaFuncionarios.DataSource = this._dtFuncionarios;
            this.dtgEnvioTemplates.DataSource = this._dtTemplatesParaEnvio;
            this.dtgRecebimentoTemplates.DataSource = this._dtTemplatesRecebidos;
            this.dtgMarcacoes.DataSource = this._dtMarcacoes;
            this.dtgSupervisores.DataSource = this._dtSupervisores;
        }

        private void BindingDataTables()
        {
            this._dtFuncionarios = this._dsREP.dtFuncionarios;
            this._dtTemplatesRecebidos = this._dsREP.dtTemplatesRecebido;
            this._dtTemplatesParaEnvio = this._dsREP.dtTemplatesParaEnvio;
            this._dtMarcacoes = this._dsREP.dtMarcacao;
            this._dtStatus = this._dsREP.dtStatus;
            this._dtSupervisores = this._dsREP.dtSupervisores;
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
                new org.cesar.dmplight.watchComm.api.TCPComm(this.txtIPRelogio.Text, 3000);

            tcpComm.SetTimeOut(15000);

            this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.BioPoint,
                                                                              tcpComm,
                                                                              Convert.ToInt32(txtNumeroRelogio.Text),
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
                this._dsREP = new dsSDK();
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
            DateTime dtInicioHorarioVerao;
            DateTime dtFimHorarioVerao;

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

            if (this.rdbUsaHorarioVerao.Checked == true)
            {
                dtInicioHorarioVerao = new DateTime(this.dtInicioHorarioVerao.Value.Year,
                                                    this.dtInicioHorarioVerao.Value.Month,
                                                    this.dtInicioHorarioVerao.Value.Day,
                                                    this.dtInicioHorarioVerao.Value.Hour,
                                                    this.dtInicioHorarioVerao.Value.Minute,
                                                    this.dtInicioHorarioVerao.Value.Second);

                dtFimHorarioVerao = new DateTime(this.dtFimHorarioVerao.Value.Year,
                                                 this.dtFimHorarioVerao.Value.Month,
                                                 this.dtFimHorarioVerao.Value.Day,
                                                 this.dtFimHorarioVerao.Value.Hour,
                                                 this.dtFimHorarioVerao.Value.Minute,
                                                 this.dtFimHorarioVerao.Value.Second);
            }
            else
            {
                dtInicioHorarioVerao = new DateTime(2000, 1, 1);
                dtFimHorarioVerao = new DateTime(2000, 1, 1);
            }

            try
            {
                this._watchComm.OpenConnection();

                if (this.rdbUsaHorarioVerao.Checked == true)
                    this._watchComm.SetDateTimeAndDST(dtEnvio, dtInicioHorarioVerao, dtFimHorarioVerao);
                else
                    this._watchComm.SetDateTimeAndDST(dtEnvio,null,null);

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

        #region " ---------------------> Event Handles da Tab Coleta <---------------------"

        private void btnObterRegistrosRelogio_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            ArrayList marcacoes;

            try
            {
                this._watchComm.OpenConnection();
                                
                this._watchComm.GeneralTimeout = 15000;

                org.cesar.dmplight.watchComm.api.AbstractMessage marcacao;
                marcacao = this._watchComm.GetCurrentPunch();
                
                while (marcacao != null)
                {
                    if (marcacao != null )
                    {                       
                        DataRow dr = this._dtMarcacoes.NewRow();
                        dr["Cartao"] = ((org.cesar.dmplight.watchComm.api.AbstractPunchMessage)(marcacao)).ID;
                        dr["DataHoraGravacao"] = ((org.cesar.dmplight.watchComm.api.AbstractPunchMessage)(marcacao)).Date;
                        dr["Evento"] = ((org.cesar.dmplight.watchComm.api.AbstractPunchMessage)(marcacao)).WatchEvent;
                        this._dtMarcacoes.Rows.Add(dr);
                    
                    }
                    
                    marcacao = this._watchComm.RemoveCurrentPunch();                  

                }
                
                              

                 MessageBox.Show("Fim da coleta!");
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
            _dtMarcacoes = new dsSDK.dtMarcacaoDataTable();

            this.dtgMarcacoes.DataSource = _dtMarcacoes;
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Status <---------------------"

        private void btnObterStatus_Click(object sender, EventArgs e)
        {
            this._dtStatus = new dsSDK.dtStatusDataTable();
            this.dtgStatus.DataSource = this._dtStatus;

            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                AbstractStatusMessage status = this._watchComm.GetStatus();

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

        private void CarregaGridStatus(AbstractStatusMessage status)
        {
            DataRow dr;

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Versão do Firmware";
            dr["Valor"] = status.FirmwareVersion;
            this._dtStatus.Rows.Add(dr);            

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Data e Hora";
            dr["Valor"] = status.Date;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade de Registros";
            dr["Valor"] = status.RecordsSize.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade de Registros";
            dr["Valor"] = status.RecordsCount.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade de Funcionários";
            dr["Valor"] = status.CardListSize.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Tipo de Checagem";
            if (status.CheckType == 0)
                dr["Valor"] = "Não possui checagem";
            else if (status.CheckType == 1)
                dr["Valor"] = "Módulo 11";
            else if (status.CheckType == 2)
                dr["Valor"] = "Módulo 10";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Registra acessos bloqueados";
            dr["Valor"] = status.RecordDeniedAccess == true ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Funcionários <---------------------"

        private void btnIncluirFuncionarios_Click(object sender, EventArgs e)
        {
            CardCollection colecaoCartoes = new CardCollection();
            Card.TypeCounter_Access contadorAcesso;
            Card cartao;

            InstanciaWatchComm();

            try
            {

                BindingDataGrids();
                BindingDataTables();

                this._watchComm.OpenConnection();

                // Insere os funcionários na lista do componente.
                foreach (DataRow dr in this._dtFuncionarios.Rows)
                {
                    try
                    {
                        cartao = new Card();
                        contadorAcesso = new Card.TypeCounter_Access();

                        contadorAcesso.Counter_Access1 = Byte.Parse(dr["ContadorAcesso1"].ToString() == "" ? "9" : dr["ContadorAcesso1"].ToString());
                        contadorAcesso.Counter_Access2 = Byte.Parse(dr["ContadorAcesso2"].ToString() == "" ? "9" : dr["ContadorAcesso2"].ToString());
                        contadorAcesso.Counter_Access3 = Byte.Parse(dr["ContadorAcesso3"].ToString() == "" ? "9" : dr["ContadorAcesso3"].ToString());
                        contadorAcesso.Counter_Access4 = Byte.Parse(dr["ContadorAcesso4"].ToString() == "" ? "9" : dr["ContadorAcesso4"].ToString());
                        contadorAcesso.Counter_Access5 = Byte.Parse(dr["ContadorAcesso5"].ToString() == "" ? "9" : dr["ContadorAcesso5"].ToString());
                        contadorAcesso.Counter_Access6 = Byte.Parse(dr["ContadorAcesso6"].ToString() == "" ? "9" : dr["ContadorAcesso6"].ToString());

                        cartao.Code = dr["Cartao"].ToString();
                        cartao.PassWord = dr["Senha"].ToString() == "" ? 0 : Int32.Parse(dr["Senha"].ToString());
                        cartao.Message = Byte.Parse(dr["CodigoMensagem"].ToString() == "" ? "0" : dr["CodigoMensagem"].ToString());
                        cartao.Way = Byte.Parse(dr["Via"].ToString() == "" ? "0" : dr["Via"].ToString());
                        cartao.CounterAccess = contadorAcesso;
                        cartao.Jornada = Int16.Parse(dr["CodigoJornada"].ToString() == "" ? "0" : dr["CodigoJornada"].ToString());

                        colecaoCartoes.Add(cartao);
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
                    this._watchComm.setCardList(colecaoCartoes, false);

                    ComandoRecepcionadoComSucesso();
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
        }

        private void btnExcluirFuncionarios_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                // Solicita ao componente a exclusão da lista de funcionários.
                foreach (DataRow dr in this._dtFuncionarios)
                {
                    try
                    {
                        this._watchComm.RemoveItemCardList(Int32.Parse(dr["Cartao"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }                
            }
            finally
            {
                this._watchComm.CloseConnection();
            }

            if (this._dtFuncionarios.Rows.Count > 0)
                ComandoRecepcionadoComSucesso();
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Templates <---------------------"

        private void btnObterTemplates_Click(object sender, EventArgs e)
        {
            BioPointFingerPrintMessage fingerPrint;

            this._dtTemplatesRecebidos.Rows.Clear();

            InstanciaWatchComm();

            try
            {

                this._watchComm.GeneralTimeout = 15000;

                this._watchComm.OpenConnection();

                fingerPrint = this._watchComm.InquiryBioPointFingerPrint();

                while (fingerPrint != null)
                {
                    DataRow dr = this._dtTemplatesRecebidos.NewRow();
                    dr["Cartao"] = fingerPrint.Card;
                    dr["Template"] = fingerPrint.FingerPrint;
                    dr["Dedo1"] = (Int16)fingerPrint.FingerprintTypeOne;
                    dr["Dedo2"] = (Int16)fingerPrint.FingerprintTypeTwo;

                    this._dtTemplatesRecebidos.Rows.Add(dr);

                    fingerPrint = this._watchComm.ConfirmationReceiptBioPointFingerPrint();
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

                drTemplateParaEnvio["Cartao"] = drTemplateRecebido["Cartao"];                
                drTemplateParaEnvio["Template"] = drTemplateRecebido["Template"];
                drTemplateParaEnvio["Dedo1"] = drTemplateRecebido["Dedo1"];
                drTemplateParaEnvio["Dedo2"] = drTemplateRecebido["Dedo2"];

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

                    this._watchComm.ExcludeBioPointFingerPrint(dr["Cartao"].ToString());
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

                    this._watchComm.IncludeBioPointFingerPrint(dr["Cartao"].ToString(),
                                                               dr["Template"].ToString(),
                                                               (EFingerPrintType)(Int16)dr["Dedo1"],
                                                               (EFingerPrintType)(Int16)dr["Dedo2"]);
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

        private void btnExclusaoTotalTemplates_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                _watchComm.OpenConnection();

                this._watchComm.ExcludeFingerPrintList();

                ComandoRecepcionadoComSucesso();
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

            if (this._dtTemplatesParaEnvio.Rows.Count > 0)
                ComandoRecepcionadoComSucesso();
        }

        private void btnExclusaoTotalTemplatesBloqueados_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                _watchComm.OpenConnection();

                this._watchComm.ExcludeAllBlockedFingerPrints();

                ComandoRecepcionadoComSucesso();
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

            if (this._dtTemplatesParaEnvio.Rows.Count > 0)
                ComandoRecepcionadoComSucesso();
        }   
        #endregion       
        
        #region " ---------------------> Event Handles da Tab Supervisores <---------------------"

        private void btnInclusaoSupervisores_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                // Insere os Supervisores no relógio.
                foreach (DataRow dr in this._dtSupervisores)
                {
                    try
                    {


                        _watchComm.Master(Int32.Parse(dr["Cartao"].ToString()),
                                          Int32.Parse(dr["Senha"].ToString()),
                                          dr["PenDrivePermissao"].ToString() == "" ? false : Convert.ToBoolean(dr["PenDrivePermissao"].ToString()),
                                          dr["PermissaoDataHora"].ToString() == "" ? false : Convert.ToBoolean(dr["PermissaoDataHora"].ToString()),
                                          dr["PermissaoProgTecnicas"].ToString() == "" ? false : Convert.ToBoolean(dr["PermissaoProgTecnicas"].ToString()));
                                           
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }

                if (this._dtSupervisores.Rows.Count > 0)
                    ComandoRecepcionadoComSucesso();
            }
            finally
            {
                this._watchComm.CloseConnection();
            }            
        }

        private void btnExclusaoSupervisores_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                try
                {
                    this._watchComm.ClearProgramming(TypeClearProgramming.Master);
                }
                catch (Exception ex)
                {
                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
                
                ComandoRecepcionadoComSucesso();
            }
            finally
            {
                this._watchComm.CloseConnection();
            }
        }
        #endregion

        private void btnMessageUser_Click(object sender, EventArgs e)
        {

            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();
                this._watchComm.ConfigureMessage(org.cesar.dmplight.watchComm.business.TypeMessageConfigurantion.User, (byte)(cboMessageUserType.SelectedIndex + 1), txtMessageUser.Text);
                this._watchComm.CloseConnection();

            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);

                ComandoRecepcionadoComSucesso();

                return;

                              
            }
            finally
            {
                this._watchComm.CloseConnection();
            }

        }

        private void MessageSystem_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.ConfigureMessage(org.cesar.dmplight.watchComm.business.TypeMessageConfigurantion.User, (byte)(cboMessageUserType.SelectedIndex + 1), txtMessageUser.Text);                              
                org.cesar.dmplight.watchComm.business.SystemTypeMessages MySystemTypeMessages;                
                MySystemTypeMessages = (org.cesar.dmplight.watchComm.business.SystemTypeMessages)(Int16.Parse(cboMessageSystem.Items[cboMessageSystem.SelectedIndex].ToString().Substring(0, 2)));
                this._watchComm.ConfigureMessage(org.cesar.dmplight.watchComm.business.TypeMessageConfigurantion.System, (byte)MySystemTypeMessages, txtMessageSystem.Text);

                this._watchComm.CloseConnection();

            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);

                ComandoRecepcionadoComSucesso();

                return;


            }
            finally
            {
                this._watchComm.CloseConnection();
            }

        }

        private void SetMessageFunction_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                
                this._watchComm.OpenConnection();


                org.cesar.dmplight.watchComm.business.MessageFunctionType MyMessageFunctionType;
                org.cesar.dmplight.watchComm.business.TypeActionFunction MyTypeActionFunction;

                MyTypeActionFunction.ActiveFunction = ActionFunction.Checked;
                MyTypeActionFunction.CheckJourney = CheckJourney.Checked;
                MyTypeActionFunction.CheckList = CheckList.Checked;
                MyTypeActionFunction.RequestsMaster = RequestsMaster.Checked;
                MyTypeActionFunction.RequesTypingKeyboard = RequestTypingKeyboard.Checked;
                MyTypeActionFunction.StoresRecordBlocked = StoresRecord.Checked;
                MyTypeActionFunction.TriggerOut = TriggerOut.Checked;
                MyTypeActionFunction.StoresRecordFreed = StoresRecordFreed.Checked;

                MyMessageFunctionType = (org.cesar.dmplight.watchComm.business.MessageFunctionType) (Int16.Parse(cboMessageFunction.Items[cboMessageFunction.SelectedIndex].ToString().Substring(0, 2)));

                this._watchComm.ConfigureMessage(org.cesar.dmplight.watchComm.business.TypeMessageConfigurantion.Function, (byte)MyMessageFunctionType, txtMessageFunction.Text, MyTypeActionFunction);                             

                //this._watchComm.CloseConnection();

                ComandoRecepcionadoComSucesso();

            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
                              

                return;


            }
            finally
            {
                this._watchComm.CloseConnection();
            }
        }

        private void CarregaGridFormatoMemoria(MemoryFormat memoria)
        {
            DataRow dr;

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Máximo de digitos do cartão";
            dr["Valor"] = memoria.MaxDigitCard;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Minimo de digitos do cartão";
            dr["Valor"] = memoria.MinDigitCard;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Tem Jornada";
            dr["Valor"] = memoria.HasWork;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Tem Mensagem";
            dr["Valor"] = memoria.HasMessage;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Tem Via";
            dr["Valor"] = memoria.HasWay;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Contadores de Acesso";
            dr["Valor"] = memoria.CounterAccess;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Tem Senha";
            dr["Valor"] = memoria.HasPassword;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Quantidade Máxima de cartões na lista";
            dr["Valor"] = memoria.QuantityMaxCard;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Quantidade Máxima de cartões alternativos";
            dr["Valor"] = memoria.QuantityMaxAlternativeId;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Quantidade Máxima de jornadas semanais";
            dr["Valor"] = memoria.QuantityMaxWeeklyWork;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Quantidade Máxima de jornadas mensais";
            dr["Valor"] = memoria.QuantityMaxMonthlyWork;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Quantidade Máxima de jornadas periodicas";
            dr["Valor"] = memoria.QuantityMaxPeriodicWork;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Quantidade Máxima de turnos";
            dr["Valor"] = memoria.QuantityMaxShiftTable;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Quantidade Máxima de sirenes";
            dr["Valor"] = memoria.QuantityMaxAlarmRing;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Quantidade Máxima de feriados";
            dr["Valor"] = memoria.QuantityMaxHoliday;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Quantidade Máxima de funções";
            dr["Valor"] = memoria.QuantityMaxFunction;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Quantidade Máxima de Mensagens de Usuario";
            dr["Valor"] = memoria.MaxMessageUser;
            this._dtFormatoMemoria.Rows.Add(dr);

            dr = this._dtFormatoMemoria.NewRow();
            dr["Propriedade"] = "Tipo de Checagem";
            if (memoria.TypeCheck == TypeCheckCard.NoCheck)
                dr["Valor"] = "Sem Checagem";
            else if (memoria.TypeCheck == TypeCheckCard.Modulo10)
                dr["Valor"] = "Módulo 10";
            else
                dr["Valor"] = "Módulo 11";

            this._dtFormatoMemoria.Rows.Add(dr);
        }

        private void btnObterFormatoMemoria_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            this._dtFormatoMemoria = new dsSDK.dtStatusDataTable();
            this.dtgFormatoMemoria.DataSource = this._dtFormatoMemoria;

            try
            {
                this._watchComm.OpenConnection();

                AbstractMemoryFormat memory = this._watchComm.GetMemoryFormat();

                if (memory == null)
                {
                    MessageBox.Show("O comando de obter formato da memória não foi recepcionado corretamente!", Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

                CarregaGridFormatoMemoria(memory.MemoryFormat);

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);

                return;
            }
            finally
            {
                this._watchComm.CloseConnection();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            this._watchComm.OpenConnection();

            ShiftTableCollection tabela = new ShiftTableCollection();

            tabela.Id = 1;

            ShiftTable shift = new ShiftTable();
            shift.Inicio = "0100";
            shift.Fim = "0130";

            tabela.Add(shift);

            shift = new ShiftTable();
            shift.Inicio = "0200";
            shift.Fim = "0330";

            tabela.Add(shift);

            shift = new ShiftTable();
            shift.Inicio = "0400";
            shift.Fim = "0530";

            tabela.Add(shift);

            shift = new ShiftTable();
            shift.Inicio = "0000";
            shift.Fim = "0030";

            tabela.Add(shift);

            shift = new ShiftTable();
            shift.Inicio = "0100";
            shift.Fim = "0130";

            tabela.Add(shift);

            shift = new ShiftTable();
            shift.Inicio = "0000";
            shift.Fim = "0000";

            tabela.Add(shift);

            this._watchComm.UpdateShiftTable(tabela);

            //WeeklyJourneyWorking j = new WeeklyJourneyWorking();
            //j.Id = 1;
            //j.Sunday = 0;
            //j.Monday = 1;
            //j.Tuesday = 2;
            //j.Wednesday = 1;
            //j.Thursday = 2;
            //j.Friday = 1;
            //j.Saturday = 0;
            //j.Holiday = 0;
            //this._watchComm.setJourneyWorking(j);

            this._watchComm.CloseConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            this._watchComm.OpenConnection();

            //this._watchComm.ProgramLotterySampleRate((byte)10, (byte)10);
            this._watchComm.GetOldStatus();

            this._watchComm.CloseConnection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();
            this._watchComm.OpenConnection();

            this._watchComm.CardEncryption(org.cesar.dmplight.watchComm.impl.EGenericEncryptionType.TwelveDigits, 567890, 1234, 1, 2);

            this._watchComm.CloseConnection();
        }

        private void btnEnviarCriptografia_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();
            this._watchComm.OpenConnection();

            if (chkUtilizaCriptografia.Checked)
                this._watchComm.CardEncryption(rdo8Digitos.Checked ? org.cesar.dmplight.watchComm.impl.EGenericEncryptionType.EightDigits : org.cesar.dmplight.watchComm.impl.EGenericEncryptionType.TwelveDigits, Convert.ToInt64(txtVetor1.Text), Convert.ToInt64(txtVetor2.Text), Convert.ToInt32(txtVerificador1.Text), Convert.ToInt32(txtVerificador2.Text));
            else
                this._watchComm.CardEncryption(org.cesar.dmplight.watchComm.impl.EGenericEncryptionType.Disabled, 0, 0, 0, 0);

            this._watchComm.CloseConnection();
        }

        private void chkUtilizaCriptografia_CheckedChanged(object sender, EventArgs e)
        {
          grpCriptografia.Enabled = chkUtilizaCriptografia.Checked;
        }

        private void tabCriptografia_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void btnObterFormatoCartao_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();
            this._watchComm.OpenConnection();

            txtFormatoMemoriaRelogio.Text = this._watchComm.InquiryCardFormat();

            this._watchComm.CloseConnection();
        }

        private void btnEnviarFormatoMemoria_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            this._watchComm.OpenConnection();

            MemoryFormat memoryFormat = new MemoryFormat();

            memoryFormat.CounterAccess = (byte)nudContadoresAcesso.Value;
            memoryFormat.HasMessage = chkTemMensagem.Checked;
            memoryFormat.HasPassword = chkTemSenha.Checked;
            memoryFormat.HasWay = chkTemVia.Checked;
            memoryFormat.MaxDigitCard = (byte)nudMaximoDigitosCartao.Value;
            memoryFormat.MaxMessageUser = (byte)nudMaximoMensagensUsuario.Value;
            memoryFormat.MinDigitCard = (byte)nudMinimoDigitosCartao.Value;
            memoryFormat.QuantityMaxAlarmRing = (byte)nudMaximoSinaleiros.Value;
            memoryFormat.QuantityMaxAlternativeId = (short)nudMaximoCodigoAlternativo.Value;
            memoryFormat.QuantityMaxCard = (short)nudMaximoCartoes.Value;
            memoryFormat.QuantityMaxFunction = (byte)nudMaximoFuncoes.Value;
            memoryFormat.QuantityMaxHoliday = (byte)nudMaximoFeriados.Value;
            memoryFormat.QuantityMaxMonthlyWork = (byte)nudMaximoJornadasMensais.Value;
            memoryFormat.QuantityMaxPeriodicWork = (byte)nudMaximoJornadasPeriodicas.Value;
            memoryFormat.QuantityMaxShiftTable = (byte)nudMaximoTurnos.Value;
            memoryFormat.QuantityMaxWeeklyWork = (byte)nudMaximoJornadasSemanais.Value;

            if (rdoSemChecagem.Checked)
                memoryFormat.TypeCheck = TypeCheckCard.NoCheck;
            else if (rdoModulo10.Checked)
                memoryFormat.TypeCheck = TypeCheckCard.Modulo10;
            else
                memoryFormat.TypeCheck = TypeCheckCard.Modulo11;

            this._watchComm.setMemoryFormat(memoryFormat);

            this._watchComm.CloseConnection();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }        
    }
}
