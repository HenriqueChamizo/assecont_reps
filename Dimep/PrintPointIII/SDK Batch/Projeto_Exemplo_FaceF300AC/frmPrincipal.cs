using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExemploFaceF300AC.DataSet;
using org.cesar.dmplight.watchComm.api;
using org.cesar.dmplight.watchComm.impl;
using org.cesar.dmplight.watchComm.impl.face;
using org.cesar.dmplight.watchComm.business;
using org.cesar.dmplight.watchComm.impl.printpoint;
using System.Net.Sockets;
using org.cesar.dmplight.watchComm.impl.FaceF300AC;

namespace ExemploFaceF300AC
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
        private dsSDKREP.dtFacesRecebidoDataTable _dtFacesRecebidos;
        private dsSDKREP.dtFacesParaEnvioDataTable _dtFacesParaEnvio;
        private dsSDKREP.dtEventosDataTable _dtEventos; 
        private WatchListener _watchListener;
        private TcpClient _watchConnection;
        private TCPComm _tcpCommFace;
        private WatchProtocolType protocol;
        private ConfigFaceAccess _configFaceAccess;
        private delegate void DelAtualizaStatusConectado();
        private delegate void DelAtualizaStatus(string msg);
        private delegate void DelAtualizaStatusFirmware(string msg,bool houveerro);
        private delegate void DelAtualizaEventos();

        private const bool ADD_WITH_EVENTS = false;
        private const bool EXCLUDE_WITH_EVENTS = false;
        
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
        }

        private void BindingDataGrids()
        {
            this.dtgListaFuncionarios.DataSource = this._dtFuncionarios;
            this.dtgEnvioFaces.DataSource = this._dtFacesParaEnvio;
            this.dtgRecebimentoFaces.DataSource = this._dtFacesRecebidos;
            this.dtgMarcacoes.DataSource = this._dtMarcacoes;
            this.dtgEventos.DataSource = this._dtEventos;  
        }

        private void BindingDataTables()
        {
            this._dtFuncionarios = this._dsREP.dtFuncionarios;
            this._dtTemplatesRecebidos = this._dsREP.dtTemplatesRecebido;
            this._dtTemplatesParaEnvio = this._dsREP.dtTemplatesParaEnvio;
            this._dtFacesRecebidos = this._dsREP.dtFacesRecebido;
            this._dtFacesParaEnvio = this._dsREP.dtFacesParaEnvio;
            this._dtMarcacoes = this._dsREP.dtMRPMarcacao;
            this._dtStatus = this._dsREP.dtStatus;
            this._dtEventos = this._dsREP.dtEventos;   
        }

        private void AddStatusLista(string msg)
        {
            this.lblStatusLista.Text = msg;
            this.lblStatusLista.ForeColor = Color.Green;
        }

        private void AddStatusFirmware(string msg, bool houveerro)
        {
            this.lblStatusFirmware.Text = msg;

            if (houveerro)
                this.lblStatusFirmware.ForeColor = Color.Red;
            else
                this.lblStatusFirmware.ForeColor = Color.Green;
            
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
            
            if (_watchConnection == null)
            {
                MessageBox.Show("Não existe conexão ativa.", "SDK REP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (this._watchComm != null)
                {
                    this._watchComm.OnCapturePinEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelDeviceOnCapturePin(_watchComm_OnCapturePinEvent);
                    this._watchComm.OnCaptureFaceEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnCaptureFace(_watchComm_OnCaptureFaceEvent);
                    this._watchComm.OnCaptureCardEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnCaptureCard(_watchComm_OnCaptureCardEvent);
                    this._watchComm.OnTransportDisconnectEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnTransportDisconnect(_watchComm_OnTransportDisconnectEvent);
                    this._watchComm.OnTransportErrorEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnTransportError(_watchComm_OnTransportErrorEvent);
                    this._watchComm.OnPinCancelEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnPinCancelEvent(_watchComm_OnPinCancelEvent);
                    this._watchComm.OnSystemLogEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnSystemLogEvent(_watchComm_OnSystemLogEvent);
                    this._watchComm.OnVerificationLogEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnVerificationLogEvent(_watchComm_OnVerificationLogEvent);
                    this._watchComm.OnPowerKeyEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnPowerKeyEvent(_watchComm_OnPowerKeyEvent);
                    this._watchComm.OnPersonsListTransmissionProgressEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelPersonListTransmissionProgressEvent(_watchComm_OnPersonsListTransmissionProgressEvent);
                    this._watchComm.OnTranferFirmwareDataProgressEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelTranferFirmwareDataProgressEvent(_watchComm_OnTranferFirmwareDataProgressEvent);
                    this._watchComm.OnWriteFirmwareDataProgressEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelWriteFirmwareDataProgressEvent(_watchComm_OnWriteFirmwareDataProgressEvent);
                    this._watchComm.OnUpdateFirmwareCompletedEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelUpdateFirmwareCompleted(_watchComm_OnUpdateFirmwareCompletedEvent);
                    this._watchComm.OnPersonListTransmissionErrorEvent -= new WatchComm.DelPersonListTransmissionErrorEvent(_watchComm_OnPersonListTransmissionErrorEvent);
                    this._watchComm.OnPersonsListExcludeProgressEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelPersonListExcludeProgressEvent(_watchComm_OnPersonsListExcludeProgressEvent);
                    this._watchComm.OnPersonListExcludeErrorEvent -= new WatchComm.DelPersonListExcludeErrorEvent(_watchComm_OnPersonListExcludeErrorEvent);
                    this._watchComm.OnCollectorRecordsEvent -= new WatchComm.DelOnCollectorRecords(_watchComm_OnCollectorRecordsEvent);
    
                    this._watchComm = null;
                }
                this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(this.protocol, _tcpCommFace, int.Parse(txtIdRelogio.Text), key, txtChaveDeComunicacao.Text, WatchConnectionType.ConnectedMode, "1.00.00");
                this._watchComm.OnCapturePinEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelDeviceOnCapturePin(_watchComm_OnCapturePinEvent);
                this._watchComm.OnCaptureFaceEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnCaptureFace(_watchComm_OnCaptureFaceEvent);
                this._watchComm.OnCaptureCardEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnCaptureCard(_watchComm_OnCaptureCardEvent);
                this._watchComm.OnTransportDisconnectEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnTransportDisconnect(_watchComm_OnTransportDisconnectEvent);
                this._watchComm.OnTransportErrorEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnTransportError(_watchComm_OnTransportErrorEvent);
                this._watchComm.OnPinCancelEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnPinCancelEvent(_watchComm_OnPinCancelEvent);
                this._watchComm.OnPowerKeyEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnPowerKeyEvent(_watchComm_OnPowerKeyEvent);
                this._watchComm.OnSystemLogEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnSystemLogEvent(_watchComm_OnSystemLogEvent);
                this._watchComm.OnVerificationLogEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnVerificationLogEvent(_watchComm_OnVerificationLogEvent);
                this._watchComm.OnPersonsListTransmissionProgressEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelPersonListTransmissionProgressEvent(_watchComm_OnPersonsListTransmissionProgressEvent);
                this._watchComm.OnTranferFirmwareDataProgressEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelTranferFirmwareDataProgressEvent(_watchComm_OnTranferFirmwareDataProgressEvent);
                this._watchComm.OnWriteFirmwareDataProgressEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelWriteFirmwareDataProgressEvent(_watchComm_OnWriteFirmwareDataProgressEvent);
                this._watchComm.OnUpdateFirmwareCompletedEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelUpdateFirmwareCompleted(_watchComm_OnUpdateFirmwareCompletedEvent);
                this._watchComm.OnPersonListTransmissionErrorEvent += new WatchComm.DelPersonListTransmissionErrorEvent(_watchComm_OnPersonListTransmissionErrorEvent);
                this._watchComm.OnPersonsListExcludeProgressEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelPersonListExcludeProgressEvent(_watchComm_OnPersonsListExcludeProgressEvent);
                this._watchComm.OnPersonListExcludeErrorEvent += new WatchComm.DelPersonListExcludeErrorEvent(_watchComm_OnPersonListExcludeErrorEvent);
                this._watchComm.OnCollectorRecordsEvent += new WatchComm.DelOnCollectorRecords(_watchComm_OnCollectorRecordsEvent);

            }
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
            streamWriter.WriteLine (this.txtIdRelogio.Text);
            streamWriter.WriteLine(this.txtIPServer.Text);
            streamWriter.WriteLine(this.txtPorta.Text);
            streamWriter.WriteLine(this.cmbModelo.SelectedIndex);   
            streamWriter.WriteLine(this.txtChaveDeComunicacao.Text);
            streamWriter.Close();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(CaminhoEXE() + "\\EnderecoIP.txt"))
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(CaminhoEXE() + "\\EnderecoIP.txt");
                this.txtIdRelogio.Text = streamReader.ReadLine();
                this.txtIPServer.Text = streamReader.ReadLine();
                this.txtPorta.Text = streamReader.ReadLine();
                this.cmbModelo.SelectedIndex = int.Parse(streamReader.ReadLine()); 
                this.txtChaveDeComunicacao.Text = streamReader.ReadLine();
                this.tabPrincipal.TabPages.Remove(tbpFabrica);

                streamReader.Close();

                if (this.cmbModelo.SelectedIndex == 0)
                    this.protocol = WatchProtocolType.FaceAccess;

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

        private void rdbDataHoraIndicada_CheckedChanged(object sender, EventArgs e)
        {
            this.dtpData.Enabled = rdbDataHoraIndicada.Checked;
            this.dtpHora.Enabled = rdbDataHoraIndicada.Checked;
        }

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

                this._watchComm.SetDateTimeF300AC(dtEnvio);

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

        private void btnEnviarModoDeCapturaRealtime_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();
            TypeModeFaceF300AC modo = TypeModeFaceF300AC.Face;
            bool IsReset = false;

            try
            {
                this._watchComm.OpenConnection();

                switch (cmbModoCapturaRealTime.SelectedIndex)
                {
                    case 0:
                        IsReset = true;
                        break;
                    case 1:
                        modo = TypeModeFaceF300AC.Face;  
                        break;
                    case 2:
                        modo = TypeModeFaceF300AC.Card;
                        break;
                    case 3:
                        modo = TypeModeFaceF300AC.PIN;
                        break;
                    case 4:
                        modo = TypeModeFaceF300AC.FaceOrCard; 
                        break;
                    case 5:
                        modo = TypeModeFaceF300AC.FaceOrCardContiguous;
                        break;
                    case 6:
                        modo = TypeModeFaceF300AC.FaceOrPIN; 
                        break;
                    case 7:
                        modo = TypeModeFaceF300AC.CardOrPIN;  
                        break;
                    case 8:
                        modo = TypeModeFaceF300AC.FaceOrCardOrPIN;  
                        break;
                    case 9:
                        modo = TypeModeFaceF300AC.FaceIdle;  
                        break;
                    case 10:
                        modo = TypeModeFaceF300AC.CardIdle;
                        break;
                    case 11:
                        modo = TypeModeFaceF300AC.PINIdle;
                        break;
                }

                if (IsReset)
                    this._watchComm.Reset();  
                else
                    this._watchComm.InitCapture(modo,chkCapturaComPrompt.Checked);

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
        private void btnPermissaoAcesso_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();
            try
            {
                this._watchComm.OpenConnection();

                switch (cmbPermissaoAcesso.SelectedIndex) 
                {
                    case 0:
                        _watchComm.UnlockDoor(int.Parse(txtTempoPermissaoAcesso.Text));     
                        break;
                    case 1:
                        _watchComm.VerifyError(int.Parse(txtTempoPermissaoAcesso.Text));         
                        break;
                }

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

        private void btnObterRegistrosMRP_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            List<FaceLogRecordF300AC> marcacoes;



            try
            {
                this._watchComm.OpenConnection();

                /* Teste com Evento */
                this._watchComm.BeginTaskCollectorRecords(); 

                //return;

                marcacoes = this._watchComm.GetALLRegisters();  

                if (marcacoes != null)
                {
                    foreach (FaceLogRecordF300AC marcacao in marcacoes)
                    {
                        DataRow dr = this._dtMarcacoes.NewRow();
                        if (marcacao.IdPerson != null)
                        {
                            dr["ID"] = marcacao.IdPerson;
                            dr["DataHoraGravacao"] = marcacao.DateTimeRecord;
                            dr["Tipo"] = marcacao.status.ToString();
                            this._dtMarcacoes.Rows.Add(dr);
                        }
                    }
                }

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

        private void btnExcluirRegistrosRelogio_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.ClearAllRegisters();

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

                DataRow dr;
                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Idioma";
                dr["Valor"] = this._watchComm.GetLanguage().ToString();
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Volume";
                dr["Valor"] = this._watchComm.GetSoundVolume() ;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Modo Verificação";
                dr["Valor"] = this._watchComm.GetVerifyMode().ToString();
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Hibernar depois.";
                dr["Valor"] = this._watchComm.GetAutoSleep();
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Alerta Log Verif.";
                dr["Valor"] = this._watchComm.GetVerificationLogWarnings(); 
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Alerta Log Sistema";
                dr["Valor"] = this._watchComm.GetSystemLogWarnings(); 
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Tempo de Reverificação";
                dr["Valor"] = this._watchComm.GetReverifyTime(); 
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Tempo de Desbloqueio";
                dr["Valor"] = this._watchComm.GetLockReleaseTime(); 
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Limite de Tempo de Porta Aberta";
                dr["Valor"] = this._watchComm.GetDoorOpenTimeout(); 
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Tipo de Sendor de Porta";
                dr["Valor"] = this._watchComm.GetDoorSensorType().ToString();
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Assistir Interferência";
                dr["Valor"] = this._watchComm.GetWatchTamper().ToString();
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Tipo IN/OUT Wiegand";
                dr["Valor"] = this._watchComm.GetWiegandType().ToString();
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Versão do Firmware";
                dr["Valor"] = this._watchComm.GetFirmwareVersion().version_string;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Data e hora";
                dr["Valor"] = this._watchComm.GetDateTimeF300AC().ToString();
                this._dtStatus.Rows.Add(dr);

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

        #region " ---------------------> Event Handles da Tab Funcionários <---------------------"

        private void btnObterFuncionarios_Click(object sender, EventArgs e)
        {
            this._dtFuncionarios.Rows.Clear();

            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                lblStatusLista.Text = "";

                List<PersonAllDataF300AC> Persons =  this._watchComm.GetAllPersons();

                if (Persons != null)
                {
                    foreach (var person in Persons)
                    {
                        DataRow dr = this._dtFuncionarios.NewRow();
                        dr["Nome"] = person.PersonName ;
                        dr["Senha"] = person.PasswordAccess;
                        dr["ID"] = person.PersonNumber;
                        if(person.CardData != null)
                            dr["Credencial"] = person.CardData.CardId;
                        
                        if(person.TemplateData != null)  
                            dr["TemFace"] = true;

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
            ListPersonAllDataF300AC persons = new ListPersonAllDataF300AC();
            try
            {
                lblStatusLista.Text = "";

                if (this._dtFuncionarios == null)
                    return;
                
                this._watchComm.OpenConnection();

                if (ADD_WITH_EVENTS == true)
                {
                    // Insere os funcionários na lista do componente.
                    foreach (DataRow dr in this._dtFuncionarios)
                    {
                        try
                        {
                            persons.Add(new PersonAllDataF300AC()
                            {
                                PersonNumber = dr["ID"].ToString(),
                                PersonName = dr["Nome"].ToString(),
                                PasswordAccess = dr["Senha"].ToString(),
                                CardData = new CardF300AC() { CardId = long.Parse(dr["Credencial"].ToString()) },
                                ValidFrom = DateTime.Now,
                                ValidUntil = DateTime.Now.AddYears(10)

                            });

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
                        if (persons != null)
                        {
                            this._watchComm.AddPersonAllDataInList(persons);
                            this._watchComm.StartDispatchPersonInList();
                        }
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }
                else
                {
                    PersonAllDataF300AC pessoa = null;
                    DelAtualizaStatus status = new DelAtualizaStatus(AddStatusLista);
                   
                    // Insere os funcionários na lista do componente.
                    foreach (DataRow dr in this._dtFuncionarios)
                    {
                        try
                        {
                            pessoa = new PersonAllDataF300AC()
                            {
                                PersonNumber = dr["ID"].ToString(),
                                PersonName = dr["Nome"].ToString(),
                                PasswordAccess = dr["Senha"].ToString(),
                                CardData = new CardF300AC() { CardId = long.Parse(dr["Credencial"].ToString()) },
                                ValidFrom = DateTime.Now,
                                ValidUntil = DateTime.Now.AddYears(10)

                            };

                            this._watchComm.AddUserWithFace(pessoa);

                        }
                        catch (Exception ex)
                        {
                            ErroDuranteRecepcaoDoComando(ex);
                            return;
                        }
                    }
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
            ListPersonAllDataF300AC persons_exclude = new ListPersonAllDataF300AC();
            try
            {
                lblStatusLista.Text = "";

                if (this._dtFuncionarios == null)
                    return;

                this._watchComm.OpenConnection();

                if (EXCLUDE_WITH_EVENTS == true)
                {
                    // Insere os funcionários na lista do componente.
                    foreach (DataRow dr in this._dtFuncionarios)
                    {
                        try
                        {
                            persons_exclude.Add(new PersonAllDataF300AC() { PersonNumber = dr["ID"].ToString() });
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
                        if (persons_exclude != null)
                        {
                            this._watchComm.AddPersonAllDataInList(persons_exclude);
                            this._watchComm.StartExcludePersonInList();
                        }
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }
                else
                {
                    foreach (DataRow dr in this._dtFuncionarios)
                    {
                        try
                        {
                            this._watchComm.ExcludePerson(dr["ID"].ToString()); 
                        }
                        catch (Exception ex)
                        {
                            ErroDuranteRecepcaoDoComando(ex);
                            return;
                        }
                    }
                }
            }
            finally
            {
                this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnApagarListaSupervisores_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.ClearMasterList();

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


        #region " ---------------------> Event Handles da Tab Faces <---------------------"

        private void btnObterFaces_Click(object sender, EventArgs e)
        {
            this._dtFacesRecebidos.Rows.Clear();

            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                List<PersonAllDataF300AC> Persons = this._watchComm.GetAllPersons();

                if (Persons != null)
                {
                    foreach (var person in Persons)
                    {
                        if (person.TemplateData != null)
                        {
                            DataRow dr = this._dtFacesRecebidos.NewRow();    
                            dr["ID"] = person.PersonNumber;
                            dr["Template"] = person.TemplateData.data;
                            this._dtFacesRecebidos.Rows.Add(dr);
                        }
                    }

                    this.dtgRecebimentoFaces.DataSource = null;
                    this.dtgRecebimentoFaces.DataSource = this._dtFacesRecebidos;

                    ComandoRecepcionadoComSucesso();
                }

                this.dtgRecebimentoFaces.DataSource = null;
                this.dtgRecebimentoFaces.DataSource = this._dtFacesRecebidos;

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

        private void btnCopiarFacesParaGridEnvio_Click(object sender, EventArgs e)
        {
            foreach (DataRow drFaceRecebido in this._dtFacesRecebidos.Rows)
            {
                DataRow drFaceParaEnvio = this._dtFacesParaEnvio.NewRow();

                drFaceParaEnvio["ID"] = drFaceRecebido["ID"];
                drFaceParaEnvio["Template"] = drFaceRecebido["Template"];

                this._dtFacesParaEnvio.Rows.Add(drFaceParaEnvio);
            }

            this.dtgEnvioFaces.DataSource = null;
            this.dtgEnvioFaces.DataSource = this._dtFacesParaEnvio;
        }

        private void btnExclusaoFaces_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            foreach (DataRow dr in this._dtFacesParaEnvio)
            {
                try
                {
                    _watchComm.OpenConnection();

                    this._watchComm.ExcludeFaceById(dr["ID"].ToString());
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

            if (this._dtFacesParaEnvio.Rows.Count > 0)
                ComandoRecepcionadoComSucesso();
        }

        private void btnInclusaoFaces_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            foreach (DataRow dr in this._dtFacesParaEnvio)
            {
                try
                {
                    _watchComm.OpenConnection();

                    this._watchComm.IncludeFaceById(dr["ID"].ToString(),
                        new TemplateF300AC()
                            {
                                data =(byte[])dr["Template"]
                            });

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

            if (this._dtFacesParaEnvio.Rows.Count > 0)
                ComandoRecepcionadoComSucesso();
        }
        #endregion

        private void AtualizaStatusConectado()
        {
            AtualizaStatusModoServer(StatusModoServer.Conectado);
        }

        private void AtualizaStatusAguardando()
        {
            if (btnEncerrarConexao.Enabled == true)
                AtualizaStatusModoServer(StatusModoServer.Aguardando);
        }        
        
        private enum StatusModoServer
        {
            Conectado,
            Aguardando,
            Inativo
        }

        private void AtualizaGridEventos()
        {
            this.dtgEventos.Refresh();  
        }

        private void AtualizaStatusModoServer(StatusModoServer statusModoServer)
        {
            switch (statusModoServer)
            {
                case StatusModoServer.Conectado:

                    this.lblStatus.Text = "Conectado";
                    this.lblStatus.ForeColor = Color.Green;
                    this.btnEncerrarConexao.Enabled = true;

                    break;
                case StatusModoServer.Aguardando:

                    this.btnAguardar_Cancelar.Text = "Cancelar";
                    this.lblStatus.Text = "Aguardando Conexão";
                    this.lblStatus.ForeColor = Color.Yellow;
                    this.btnEncerrarConexao.Enabled = false;

                    break;
                case StatusModoServer.Inativo:

                    this.btnAguardar_Cancelar.Text = "Aguardar Conexão";
                    this.lblStatus.Text = "Inativo";
                    this.lblStatus.ForeColor = Color.White;
                    this.btnEncerrarConexao.Enabled = false;

                    break;
                default:
                    break;
            }
        }

        void _watchListener_watchFaceConnectEvent(short watchID, TCPComm tcpCommFace)
        {
            DelAtualizaStatusConectado delAtualizaStatusConectado = new DelAtualizaStatusConectado(AtualizaStatusConectado);
            try
            {
                if (this._watchComm != null)
                {
                    this._watchComm.OnCapturePinEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelDeviceOnCapturePin(_watchComm_OnCapturePinEvent);
                    this._watchComm.OnCaptureFaceEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnCaptureFace(_watchComm_OnCaptureFaceEvent);
                    this._watchComm.OnCaptureCardEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnCaptureCard(_watchComm_OnCaptureCardEvent);
                    this._watchComm.OnTransportDisconnectEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnTransportDisconnect(_watchComm_OnTransportDisconnectEvent);
                    this._watchComm.OnTransportErrorEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnTransportError(_watchComm_OnTransportErrorEvent);
                    this._watchComm.OnPinCancelEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnPinCancelEvent(_watchComm_OnPinCancelEvent);
                    this._watchComm.OnPowerKeyEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnPowerKeyEvent(_watchComm_OnPowerKeyEvent);
                    this._watchComm.OnSystemLogEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnSystemLogEvent(_watchComm_OnSystemLogEvent);
                    this._watchComm.OnVerificationLogEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnVerificationLogEvent(_watchComm_OnVerificationLogEvent);
                    this._watchComm.OnPersonsListTransmissionProgressEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelPersonListTransmissionProgressEvent(_watchComm_OnPersonsListTransmissionProgressEvent);
                    this._watchComm.OnTranferFirmwareDataProgressEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelTranferFirmwareDataProgressEvent(_watchComm_OnTranferFirmwareDataProgressEvent);
                    this._watchComm.OnWriteFirmwareDataProgressEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelWriteFirmwareDataProgressEvent(_watchComm_OnWriteFirmwareDataProgressEvent);
                    this._watchComm.OnUpdateFirmwareCompletedEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelUpdateFirmwareCompleted(_watchComm_OnUpdateFirmwareCompletedEvent);
                    this._watchComm.OnPersonsListExcludeProgressEvent -= new org.cesar.dmplight.watchComm.impl.WatchComm.DelPersonListExcludeProgressEvent(_watchComm_OnPersonsListExcludeProgressEvent);
                    this._watchComm.OnPersonListExcludeErrorEvent -= new WatchComm.DelPersonListExcludeErrorEvent(_watchComm_OnPersonListExcludeErrorEvent);


                    this._watchComm = null;
                }
                this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(this.protocol , tcpCommFace, watchID, "", txtChaveDeComunicacao.Text, WatchConnectionType.ConnectedMode, "1.00.00");
                this._watchComm.OnCapturePinEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelDeviceOnCapturePin(_watchComm_OnCapturePinEvent);
                this._watchComm.OnCaptureFaceEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnCaptureFace(_watchComm_OnCaptureFaceEvent);
                this._watchComm.OnCaptureCardEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnCaptureCard(_watchComm_OnCaptureCardEvent);
                this._watchComm.OnTransportDisconnectEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnTransportDisconnect(_watchComm_OnTransportDisconnectEvent);
                this._watchComm.OnTransportErrorEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnTransportError(_watchComm_OnTransportErrorEvent);
                this._watchComm.OnPinCancelEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnPinCancelEvent(_watchComm_OnPinCancelEvent);
                this._watchComm.OnPowerKeyEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnPowerKeyEvent(_watchComm_OnPowerKeyEvent);
                this._watchComm.OnSystemLogEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnSystemLogEvent(_watchComm_OnSystemLogEvent);
                this._watchComm.OnVerificationLogEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelOnVerificationLogEvent(_watchComm_OnVerificationLogEvent);
                this._watchComm.OnPersonsListTransmissionProgressEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelPersonListTransmissionProgressEvent(_watchComm_OnPersonsListTransmissionProgressEvent);
                this._watchComm.OnTranferFirmwareDataProgressEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelTranferFirmwareDataProgressEvent(_watchComm_OnTranferFirmwareDataProgressEvent);
                this._watchComm.OnWriteFirmwareDataProgressEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelWriteFirmwareDataProgressEvent(_watchComm_OnWriteFirmwareDataProgressEvent);
                this._watchComm.OnUpdateFirmwareCompletedEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelUpdateFirmwareCompleted(_watchComm_OnUpdateFirmwareCompletedEvent);
                this._watchComm.OnPersonsListExcludeProgressEvent += new org.cesar.dmplight.watchComm.impl.WatchComm.DelPersonListExcludeProgressEvent(_watchComm_OnPersonsListExcludeProgressEvent);
                this._watchComm.OnPersonListExcludeErrorEvent += new WatchComm.DelPersonListExcludeErrorEvent(_watchComm_OnPersonListExcludeErrorEvent);


                _watchConnection = tcpCommFace.TCPLClient;
                _tcpCommFace = tcpCommFace;

                this.BeginInvoke(delAtualizaStatusConectado);
            }
            catch { }
        }

        private void btnAguardar_Cancelar_Click(object sender, EventArgs e)
        {
            if (_watchListener == null)
            {
                try
                {
                    _watchListener = new org.cesar.dmplight.watchComm.impl.WatchListener(this.protocol, txtIPServer.Text, txtChaveDeComunicacao.Text, Int32.Parse(txtPorta.Text), Int16.Parse(txtIdRelogio.Text));
                    _watchListener.watchFaceConnectEvent += new WatchListener.DelWatchFaceConnect(_watchListener_watchFaceConnectEvent);
                    _watchListener.Listening();

                    AtualizaStatusModoServer(StatusModoServer.Aguardando);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Não foi possível inicializar o listener. \n" +
                                    "Ocorreu o seguinte erro: " + ex.Message, "Erro",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _watchListener.watchFaceConnectEvent -= _watchListener_watchFaceConnectEvent;
                _watchListener.StopListening();
                _watchListener = null;

                if (_watchConnection != null)
                {
                    try
                    {
                        _watchConnection.Close();
                    }
                    catch { }
                }

                _watchConnection = null;

                AtualizaStatusModoServer(StatusModoServer.Inativo);
            }
        }

        #region "Eventos da WatchComm"
        void _watchComm_OnPersonsListExcludeProgressEvent(int watchID, int personsTotal, int personForwarded)
        {
            DelAtualizaStatus status = new DelAtualizaStatus(AddStatusLista);
            this.BeginInvoke(status, String.Format("Equip.: {0} Excluíndo - {1} de {2}", watchID, personForwarded, personsTotal));

            if(personsTotal == personForwarded)
                this.BeginInvoke(status, String.Format("Equip.: {0} Concluído a exclusão", watchID));

        }

        void _watchComm_OnPersonListExcludeErrorEvent(int watchID, int personForwarded, FaceF300ACException error)
        {
            DelAtualizaStatus status = new DelAtualizaStatus(AddStatusLista);
            this.BeginInvoke(status, String.Format("Equip.: {0} Erro ao excluir pessoa. /n Err.:{1}", watchID,error.Message ));

            this._watchComm.StopExcludePersonInList(); 
        }

        void _watchComm_OnCollectorRecordsEvent(int watchID, int LogTo, int LogFrom, string IdPerson, string CardNumber, DateTime RecordDateTime, FaceLogRecordF300AC.VerificationStatus status)
        {
            //throw new NotImplementedException();
        }

        void _watchComm_OnPersonsListTransmissionProgressEvent(int watchID, int personsTotal, int personForwarded)
        {
            DelAtualizaStatus status = new DelAtualizaStatus(AddStatusLista);
            this.BeginInvoke(status, String.Format("Equip.: {0} Transmitindo - {1} de {2}", watchID, personForwarded, personsTotal));

            if (personsTotal == personForwarded)
                this.BeginInvoke(status, String.Format("Equip.: {0} Concluído o envia da lista", watchID));
        }
        void _watchComm_OnPersonListTransmissionErrorEvent(int watchID, int personForwarded, FaceF300ACException error)
        {
            DelAtualizaStatus status = new DelAtualizaStatus(AddStatusLista);
            this.BeginInvoke(status, String.Format("Equip.: {0} Erro ao enviar pessoa. /n Err.:{1}", watchID, error.Message));

            this._watchComm.StopDispatchPersonInList(); 
        }

        void _watchComm_OnUpdateFirmwareCompletedEvent(int watchID)
        {
            DelAtualizaStatusFirmware delAtualizaFirm = new DelAtualizaStatusFirmware(AddStatusFirmware);
            this.BeginInvoke(delAtualizaFirm, "Concluído o Envio de Firmware", false); 
        }

        void _watchComm_OnWriteFirmwareDataProgressEvent(int watchID, int dataTotal, int dataForwarded)
        {
            DelAtualizaStatusFirmware delAtualizaFirm = new DelAtualizaStatusFirmware(AddStatusFirmware);
            this.BeginInvoke(delAtualizaFirm, String.Format("Gravando... {0} de {1}", dataForwarded, dataTotal), false); 
        }

        void _watchComm_OnTranferFirmwareDataProgressEvent(int watchID, int dataTotal, int dataForwarded)
        {
            DelAtualizaStatusFirmware delAtualizaFirm = new DelAtualizaStatusFirmware(AddStatusFirmware);
            this.BeginInvoke(delAtualizaFirm, String.Format("Transmitindo... {0} de {1}", dataForwarded, dataTotal), false); 
        }

        void _watchComm_OnVerificationLogEvent(int watchID, int log_id)
        {
            try
            {
                DelAtualizaEventos delAtualizaEventos = new DelAtualizaEventos(AtualizaGridEventos);


                DataRow dr = this._dtEventos.NewRow();
                dr["Tipo"] = "VerificationLogEvent";
                dr["Descrição"] = String.Format("Equip.:{0} log_id:{1}", watchID, log_id);
                dr["DataHora"] = DateTime.Now;
                dr["Template"] = null;

                this._dtEventos.Rows.Add(dr);

                this.BeginInvoke(delAtualizaEventos);
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }
        void _watchComm_OnSystemLogEvent(int watchID, int log_id)
        {
            InstanciaWatchComm();
            try
            {
                DelAtualizaEventos delAtualizaEventos = new DelAtualizaEventos(AtualizaGridEventos);

                _watchComm.OpenConnection();

                List<SystemLogF300AC> SysLog = this._watchComm.GetSystemLogs(log_id, log_id + 1); 


                DataRow dr = this._dtEventos.NewRow();
                dr["Tipo"] = "SystemLogEvent";

                
                dr["Descrição"] = String.Format("Equip.:{0} Log Data:{1}", watchID, SysLog[0].ToString());
                dr["DataHora"] = DateTime.Now;
                dr["Template"] = null;

                this._dtEventos.Rows.Add(dr);

                this.BeginInvoke(delAtualizaEventos);
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        void _watchComm_OnPinCancelEvent(int watchID)
        {
            try
            {
                DelAtualizaEventos delAtualizaEventos = new DelAtualizaEventos(AtualizaGridEventos);

                DataRow dr = this._dtEventos.NewRow();
                dr["Tipo"] = "PinCancelEvent";
                dr["Descrição"] = String.Format("Equip.:{0}", watchID);
                dr["DataHora"] = DateTime.Now;
                dr["Template"] = null;

                this._dtEventos.Rows.Add(dr);

                this.BeginInvoke(delAtualizaEventos);
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        void _watchComm_OnPowerKeyEvent(int watchID)
        {
            try
            {
                DelAtualizaEventos delAtualizaEventos = new DelAtualizaEventos(AtualizaGridEventos);

                DataRow dr = this._dtEventos.NewRow();
                dr["Tipo"] = "PowerKeyEvent";
                dr["Descrição"] = String.Format("Equip.:{0}", watchID);
                dr["DataHora"] = DateTime.Now;
                dr["Template"] = null;

                this._dtEventos.Rows.Add(dr);

                this.BeginInvoke(delAtualizaEventos);
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        void _watchComm_OnTransportErrorEvent(int watchID, FaceF300ACException error)
        {
            try
            {
                if (error.ErrorType == FaceF300ACException.TypeErrorCode.UPDATEFIRMWARE)
                {
                    DelAtualizaStatusFirmware delAtualizaFirm = new DelAtualizaStatusFirmware(AddStatusFirmware);
                    this.BeginInvoke(delAtualizaFirm,String.Format("Erro: {0}",error.Message),true); 

                }
                else
                {
                    DelAtualizaEventos delAtualizaEventos = new DelAtualizaEventos(AtualizaGridEventos);

                    DataRow dr = this._dtEventos.NewRow();
                    dr["Tipo"] = "TransportErrorEvent";
                    dr["Descrição"] = String.Format("Equip.:{0} Erro:{1}", watchID, error.Message);
                    dr["DataHora"] = DateTime.Now;
                    dr["Template"] = null;

                    this._dtEventos.Rows.Add(dr);

                    this.BeginInvoke(delAtualizaEventos);
                }
 
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        void _watchComm_OnTransportDisconnectEvent(int watchID)
        {
            try
            {
                DelAtualizaEventos delAtualizaEventos = new DelAtualizaEventos(AtualizaGridEventos);

                DataRow dr = this._dtEventos.NewRow();
                dr["Tipo"] = "TransportDisconnectEvent";
                dr["Descrição"] = String.Format("Equip.:{0}", watchID);
                dr["DataHora"] = DateTime.Now;
                dr["Template"] = null;

                this._dtEventos.Rows.Add(dr);

                this.BeginInvoke(delAtualizaEventos);
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        void _watchComm_OnCaptureCardEvent(int watchID, CardF300AC card)
        {
            try
            {
                DelAtualizaEventos delAtualizaEventos = new DelAtualizaEventos(AtualizaGridEventos);

                DataRow dr = this._dtEventos.NewRow();
                dr["Tipo"] = "CardEvent";
                
                dr["Descrição"] = String.Format("Equip.:{0} Card ID:{1} Data HEX:{2}", watchID, card.ToStrNumber(), card.ToStringHex());
                dr["DataHora"] = DateTime.Now;
                dr["Template"] = card.data ;

                this._dtEventos.Rows.Add(dr);

                this.BeginInvoke(delAtualizaEventos);
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        void _watchComm_OnCaptureFaceEvent(int watchID, TemplateF300AC template)
        {
            try
            {
                DelAtualizaEventos delAtualizaEventos = new DelAtualizaEventos(AtualizaGridEventos);

                DataRow dr = this._dtEventos.NewRow();
                dr["Tipo"] = "FaceEvent";
                dr["Descrição"] = String.Format("Equip.:{0} Data HEX:{1}", watchID,template.ToString());
                dr["DataHora"] = DateTime.Now;
                dr["Template"] = template.data ;

                this._dtEventos.Rows.Add(dr);

                this.BeginInvoke(delAtualizaEventos);
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        void _watchComm_OnCapturePinEvent(int watchID, string pin)
        {
            try
            {
                DelAtualizaEventos delAtualizaEventos = new DelAtualizaEventos(AtualizaGridEventos);

                DataRow dr = this._dtEventos.NewRow();
                dr["Tipo"] = "PinEvent";
                dr["Descrição"] = String.Format("Equip.:{0} PIN:{1}",watchID,pin);
                dr["DataHora"] = DateTime.Now;  
                dr["Template"] = null;

                this._dtEventos.Rows.Add(dr);
                
                this.BeginInvoke(delAtualizaEventos);
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }
        #endregion

        private void btnEncerrarConexao_Click(object sender, EventArgs e)
        {
            try
            {
                _watchListener.watchFaceConnectEvent -= _watchListener_watchFaceConnectEvent;
                _watchListener.StopListening();

                AtualizaStatusModoServer(StatusModoServer.Inativo);

                _watchListener = null;

                if (_watchConnection != null)
                {
                    try
                    {
                        _watchConnection.Close();
                    }
                    catch { }
                }
                _watchConnection = null;



            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void cmbModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.protocol = WatchProtocolType.FaceAccess;
            this._configFaceAccess = new ConfigFaceAccess(); 
            pgrd_Configuracoes.SelectedObject = this._configFaceAccess;
            pgrd_Configuracoes.Refresh(); 
        }

        private void btn_LimparGridEventos_Click(object sender, EventArgs e)
        {
            _dtEventos = new dsSDKREP.dtEventosDataTable();

            this.dtgEventos.DataSource = _dtEventos;
        }

        private void btnObterConfiguracoes_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._configFaceAccess = new ConfigFaceAccess();

                this._configFaceAccess.Idioma = (ConfigFaceAccess.TipoIdioma)this._watchComm.GetLanguage();
                this._configFaceAccess.Volume = this._watchComm.GetSoundVolume();
                this._configFaceAccess.ModoVerificacaoBatch = (ConfigFaceAccess.TipoModoVerificacao)this._watchComm.GetVerifyMode();
                this._configFaceAccess.TempoHibernacao = this._watchComm.GetAutoSleep();
                this._configFaceAccess.AlertaLogVerificacao = this._watchComm.GetVerificationLogWarnings();
                this._configFaceAccess.AlertaLogSistema = this._watchComm.GetSystemLogWarnings();
                this._configFaceAccess.TempoReverificacao = this._watchComm.GetReverifyTime();
                this._configFaceAccess.TempoLimitePortaAberta = this._watchComm.GetDoorOpenTimeout();
                this._configFaceAccess.TipoSensorPorta = (ConfigFaceAccess.TipoSensor)this._watchComm.GetDoorSensorType();
                this._configFaceAccess.AssistirInterferencia = this._watchComm.GetWatchTamper();
                this._configFaceAccess.TipoLeitCartao = (ConfigFaceAccess.TipoCartao)this._watchComm.GetWiegandType();
                this._configFaceAccess.PINMaster = this._watchComm.GetMasterPin();
                this._configFaceAccess.ModoCapturaFoto = (ConfigFaceAccess.TipoModoCFoto)this._watchComm.GetPhotoShowSetting();

                pgrd_Configuracoes.SelectedObject = this._configFaceAccess;

                pgrd_Configuracoes.Refresh(); 

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

        private void btnEnviarConfiguracoes_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                if (this._configFaceAccess != null)
                {
                    this._watchComm.SetLanguage((TypeLanguage)this._configFaceAccess.Idioma);
                    this._watchComm.SetSoundVolume(this._configFaceAccess.Volume);
                    this._watchComm.SetVerifyMode((TypeStatusF300AC)this._configFaceAccess.ModoVerificacaoBatch);
                    this._watchComm.SetAutoSleep(this._configFaceAccess.TempoHibernacao);
                    this._watchComm.SetVerificationLogWarnings(this._configFaceAccess.AlertaLogVerificacao);
                    this._watchComm.SetSystemLogWarnings(this._configFaceAccess.AlertaLogSistema);
                    this._watchComm.SetReverifyTime(this._configFaceAccess.TempoReverificacao);
                    this._watchComm.SetDoorOpenTimeout(this._configFaceAccess.TempoLimitePortaAberta);
                    this._watchComm.SetDoorSensorType((TypeDoorSensor)this._configFaceAccess.TipoSensorPorta);
                    this._watchComm.SetWatchTamper(this._configFaceAccess.AssistirInterferencia);
                    this._watchComm.SetWiegandType((TypeWiegand)this._configFaceAccess.TipoLeitCartao);
                    this._watchComm.SetCameraLed(this._configFaceAccess.LigarLedsCamera);
                    this._watchComm.SetMasterPin(this._configFaceAccess.PINMaster);
                    this._watchComm.SetPhotoShowSetting((TypePhotoSetting)this._configFaceAccess.ModoCapturaFoto);  

                }

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

        private void tabPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift == true && e.KeyCode == Keys.F2)
                if (this.tabPrincipal.TabPages.Contains(tbpFabrica))
                    this.tabPrincipal.TabPages.Remove(tbpFabrica);
                else
                    this.tabPrincipal.TabPages.Add(tbpFabrica);
        }

        private void btnArquivoDownloadFirmware_Click(object sender, EventArgs e)
        {
            ofdDownloadFirmware.FileName = "";
            ofdDownloadFirmware.Title = "Selecione o arquivo para download do firmware.";

            ofdDownloadFirmware.ShowDialog();

            this.txtArquivoDownloadFirmware.Text = ofdDownloadFirmware.FileName;
        }

        private void btnDownloadFirmware_Click(object sender, EventArgs e)
        {
            if (this.txtArquivoDownloadFirmware.Text.Equals(""))
            {
                MessageBox.Show("Selecione o arquivo para download do firmware.");
                return;
            }
            else if (!System.IO.File.Exists(this.txtArquivoDownloadFirmware.Text))
            {
                MessageBox.Show("O arquivo selecionado para download do firmware é inválido ou inexistente.");
                return;
            }

            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                /* Teste com Array de Bytes.
                byte[] buff = null;
                System.IO.FileStream fs = new System.IO.FileStream(this.txtArquivoDownloadFirmware.Text,
                                               System.IO.FileMode.Open,
                                               System.IO.FileAccess.Read);
                System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                long numBytes = new System.IO.FileInfo(this.txtArquivoDownloadFirmware.Text).Length;
                buff = br.ReadBytes((int)numBytes);

                this._watchComm.UpdateFirmware(buff); 
                */

                this._watchComm.UpdateFirmware(this.txtArquivoDownloadFirmware.Text);

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

        private void dtgEventos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }



    }
}