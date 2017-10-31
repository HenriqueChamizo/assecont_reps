using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExemploFace.DataSet;
using org.cesar.dmplight.watchComm.api;
using org.cesar.dmplight.watchComm.impl;
using org.cesar.dmplight.watchComm.impl.face;
using org.cesar.dmplight.watchComm.business;
using org.cesar.dmplight.watchComm.impl.printpoint;

namespace ExemploFace
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
            this.dtgEnvioTemplates.DataSource = this._dtTemplatesParaEnvio;
            this.dtgRecebimentoTemplates.DataSource = this._dtTemplatesRecebidos;
            this.dtgEnvioFaces.DataSource = this._dtFacesParaEnvio;
            this.dtgRecebimentoFaces.DataSource = this._dtFacesRecebidos;
            this.dtgMarcacoes.DataSource = this._dtMarcacoes;
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

            TCPComm tcpComm = new TCPComm(this.txtIPRelogio.Text, 4370);

            tcpComm.SetTimeOut(15000);

            this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(WatchProtocolType.Face,
                                                                              tcpComm,
                                                                              1,
                                                                              key,
                                                                              WatchConnectionType.ConnectedMode,
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

        private void btnSet1Pra11PraN_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                if (this.rdbSet1pra1.Checked)
                    this._watchComm.Set1To1();
                else
                    this._watchComm.Set1ToN();

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

        private void btnReinicializarEquipamento_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.RestartDevice();

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

            FaceLogRecord[] marcacoes;

            try
            {
                this._watchComm.OpenConnection();

                marcacoes = this._watchComm.InquiryFaceLogRecords();

                if (marcacoes != null)
                {
                    foreach (FaceLogRecord marcacao in marcacoes)
                    {
                        DataRow dr = this._dtMarcacoes.NewRow();
                        dr["ID"] = marcacao.EmployeeId;
                        dr["DataHoraGravacao"] = marcacao.DateTimeMarkingPoint;
                        this._dtMarcacoes.Rows.Add(dr);
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

                this._watchComm.DeleteFaceLogRecords();

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

                FaceStatus status = this._watchComm.GetFaceStatus();

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

        private void CarregaGridStatus(FaceStatus status)
        {
            DataRow dr;

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Data e Hora";
            dr["Valor"] = status.DeviceDateTime.ToShortDateString() + " " + status.DeviceDateTime.ToShortTimeString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade de Registros";
            dr["Valor"] = status.RecordsCapacity;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Ocupação de Registros";
            dr["Valor"] = status.RecordsOccupation;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade de Digitais";
            dr["Valor"] = status.FingerprintCapacity;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade de Digitais";
            dr["Valor"] = status.FingerprintOccupation;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade de Usuários";
            dr["Valor"] = status.UsersCapacity;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade de Usuários";
            dr["Valor"] = status.UsersOccupation;
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
            dr["Propriedade"] = "Versão do Firmware";
            dr["Valor"] = status.FirmwareVersion;
            this._dtStatus.Rows.Add(dr);
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Funcionários <---------------------"

        private void btnObterFuncionarios_Click(object sender, EventArgs e)
        {
            this._dtFuncionarios.Rows.Clear();

            InstanciaWatchComm();

            FaceEmployee[] employees;

            try
            {
                this._watchComm.OpenConnection();

                employees = this._watchComm.InquiryFaceEmployeeList();

                if (employees != null)
                {
                    foreach (FaceEmployee employee in employees)
                    {
                        DataRow dr = this._dtFuncionarios.NewRow();
                        dr["Nome"] = employee.Name;
                        dr["Senha"] = employee.Password;
                        dr["ID"] = employee.EmployeeID;
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

        #region " ---------------------> Event Handles da Tab Templates <---------------------"

        private void btnObterTemplates_Click(object sender, EventArgs e)
        {
            this._dtTemplatesRecebidos.Rows.Clear();

            InstanciaWatchComm();

            FaceEmployee[] employees;

            try
            {
                this._watchComm.OpenConnection();

                employees = this._watchComm.InquiryFaceEmployeeList();
                
                foreach (FaceEmployee employee in employees)
                {
                    FaceFingerPrint[] fingerPrints = this._watchComm.InquiryFaceFingerPrint(employee.EmployeeID);

                    foreach (FaceFingerPrint fingerPrint in fingerPrints)
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
            try
            {
                InstanciaWatchComm();

                _watchComm.OpenConnection();

                foreach (DataRow dr in this._dtTemplatesParaEnvio)
                {
                    this._watchComm.ExcludeFingerPrint(Int32.Parse(dr["ID"].ToString()), (EFingerPrintType)Int32.Parse(dr["Dedo"].ToString()));
                }

                if (this._dtTemplatesParaEnvio.Rows.Count > 0)
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
        }

        private void btnInclusaoTemplates_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                _watchComm.OpenConnection();

                foreach (DataRow dr in this._dtTemplatesParaEnvio)
                {
                    this._watchComm.IncludeFingerPrint(Int32.Parse(dr["ID"].ToString()),
                                                       (EFingerPrintType)Int32.Parse(dr["Dedo"].ToString()),
                                                       dr["Template"].ToString());
                }
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

        #region " ---------------------> Event Handles da Tab Faces <---------------------"

        private void btnObterFaces_Click(object sender, EventArgs e)
        {
            this._dtFacesRecebidos.Rows.Clear();

            InstanciaWatchComm();

            FaceEmployee[] employees;

            try
            {
                this._watchComm.OpenConnection();

                employees = this._watchComm.InquiryFaceEmployeeList();

                foreach (FaceEmployee employee in employees)
                {
                    String face = this._watchComm.InquiryFaceTemplate(employee.EmployeeID);

                    if (face != String.Empty)
                    {
                        DataRow dr = this._dtFacesRecebidos.NewRow();
                        dr["ID"] = employee.EmployeeID;
                        dr["Template"] = face;

                        this._dtFacesRecebidos.Rows.Add(dr);
                    }
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

                    this._watchComm.ExcludeFaceTemplate(Int32.Parse(dr["ID"].ToString()));
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

            //String face = "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";

            foreach (DataRow dr in this._dtFacesParaEnvio)
            {
                try
                {
                    _watchComm.OpenConnection();

                    this._watchComm.IncludeFaceTemplate(Int32.Parse(dr["ID"].ToString()),
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

            if (this._dtFacesParaEnvio.Rows.Count > 0)
                ComandoRecepcionadoComSucesso();
        }
        #endregion

    }
}