using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using org.cesar.dmplight.watchComm.business;
using org.cesar.dmplight.watchComm.impl.printpoint;
using ExemploBIO.DataSet;
using org.cesar.dmplight.watchComm.api;
using org.cesar.dmplight.watchComm.impl.biopoint;

namespace ExemploBIO
{
    public partial class frmPrincipal : Form
    {
        #region " ---------------------> Atributos.                                              

        private org.cesar.dmplight.watchComm.impl.WatchComm _watchComm;
        private dsSDK _dsBIO;
        private dsSDK.dtMarcacaoDataTable _dtMarcacoes;
        private dsSDK.dtMarcacaoBackupDataTable _dtMarcacoesBackup;
        private dsSDK.dtStatusDataTable _dtStatus;        
        private dsSDK.dtFuncionariosDataTable _dtFuncionarios;
        private dsSDK.dtTemplatesRecebidoDataTable _dtTemplatesRecebidos;
        private dsSDK.dtTemplatesParaEnvioDataTable _dtTemplatesParaEnvio;
        private dsSDK.dtSupervisoresDataTable _dtSupervisores;
        private dsSDK.dtCodigosAlternativosDataTable _dtCodigosAlternativos;
        #endregion

        #region " ---------------------> Construtor.                                             

        public frmPrincipal()
        {
            InitializeComponent();

            this._dsBIO = new dsSDK();
            BindingDataTables();

            SetDefaults();
        }
        #endregion

        #region " ---------------------> Métodos Privados.                                       

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
            this.dtgMarcacoesBackup.DataSource = this._dtMarcacoesBackup;
            this.dtgSupervisores.DataSource = this._dtSupervisores;
            this.dtgCodigosAlternativos.DataSource = this._dtCodigosAlternativos;
        }

        private void BindingDataTables()
        {
            this._dtFuncionarios = this._dsBIO.dtFuncionarios;
            this._dtTemplatesRecebidos = this._dsBIO.dtTemplatesRecebido;
            this._dtTemplatesParaEnvio = this._dsBIO.dtTemplatesParaEnvio;
            this._dtMarcacoes = this._dsBIO.dtMarcacao;
            this._dtMarcacoesBackup = this._dsBIO.dtMarcacaoBackup;
            this._dtStatus = this._dsBIO.dtStatus;
            this._dtSupervisores = this._dsBIO.dtSupervisores;
            this._dtCodigosAlternativos = this._dsBIO.dtCodigosAlternativos;
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

        private void ApenasNumeros(object sender, KeyPressEventArgs e)
        {
            if (!((Encoding.Default.GetBytes(e.KeyChar.ToString())[0] >= (int)Keys.D0) &&
                (Encoding.Default.GetBytes(e.KeyChar.ToString())[0] <= (int)Keys.D9) ||
                (Encoding.Default.GetBytes(e.KeyChar.ToString())[0] == (int)Keys.Back)))
            {
                e.Handled = true;
                e.KeyChar = (Char)Keys.Cancel;
            }
        }
        #endregion

        #region " ---------------------> Leitura e gravação do endereço IP informado.            

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

            cboTipoSinaleiro.SelectedIndex = 0;
            cboLimpeza.SelectedIndex = 0;
            cboAcionamento.SelectedIndex = 0;
            chkUtilizaCriptografia.Checked = false;
        }
        #endregion

        #region " ---------------------> Leitura e gravação das informações contidas nos grids.  
     
        private void btnGravarXML_Click(object sender, EventArgs e)
        {
            this._dsBIO.WriteXml(this.CaminhoEXE() + "\\DataDSREPCH.xml");

            MessageBox.Show("XML gravado com sucesso!", Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnLerXML_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(this.CaminhoEXE() + "\\DataDSREPCH.xml"))
            {
                this._dsBIO = new dsSDK();
                this._dsBIO.ReadXml(this.CaminhoEXE() + "\\DataDSREPCH.xml");

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

        #region " ---------------------> Event Handles da Tab Geral.                             

        private void txtDuracaoSinaleiro_Leave(object sender, EventArgs e)
        {
            if (!txtDuracaoSinaleiro.Text.Equals(""))
            {
                if (Int16.Parse(txtDuracaoSinaleiro.Text) > 15)
                    txtDuracaoSinaleiro.Text = "15";
            }
        }

        private void btnEnviarSinaleiro_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                AlarmRingsCollection alarmRingsCollection = new AlarmRingsCollection();
                AlarmRings alarmRings = new AlarmRings();

                alarmRings.Id = Byte.Parse(txtCodigoSinaleiro.Text);
                alarmRings.TimeAlarm = dtpHorarioSinaleiro.Value.Hour.ToString() + ":" + dtpHorarioSinaleiro.Value.Minute.ToString();
                alarmRings.Duration = byte.Parse(txtDuracaoSinaleiro.Text);

                if (cboTipoSinaleiro.SelectedIndex == 0)
                    alarmRings.TypeRing = AlarmRings.TypeAlarm.Inside;
                else
                    alarmRings.TypeRing = AlarmRings.TypeAlarm.Outside;

                alarmRings.RingSunday = chkDomingo.Checked ? AlarmRings.TypeRinging.Ring : AlarmRings.TypeRinging.NoRing;
                alarmRings.RingMonday = chkSegunda.Checked ? AlarmRings.TypeRinging.Ring : AlarmRings.TypeRinging.NoRing;
                alarmRings.RingTuesday = chkTerca.Checked ? AlarmRings.TypeRinging.Ring : AlarmRings.TypeRinging.NoRing;
                alarmRings.RingWednesday = chkQuarta.Checked ? AlarmRings.TypeRinging.Ring : AlarmRings.TypeRinging.NoRing;
                alarmRings.RingThursday = chkQuinta.Checked ? AlarmRings.TypeRinging.Ring : AlarmRings.TypeRinging.NoRing;
                alarmRings.RingFriday = chkSexta.Checked ? AlarmRings.TypeRinging.Ring : AlarmRings.TypeRinging.NoRing;
                alarmRings.RingSaturday = chkSabado.Checked ? AlarmRings.TypeRinging.Ring : AlarmRings.TypeRinging.NoRing;
                alarmRings.RingHoliday = chkFeriado.Checked ? AlarmRings.TypeRinging.Ring : AlarmRings.TypeRinging.NoRing;

                alarmRingsCollection.Add(alarmRings);

                this._watchComm.AlarmRing(alarmRingsCollection);

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

        private void btnEnviarFeriado_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                HolidayCollection holidayCollection = new HolidayCollection();
                Holiday holiday = new Holiday();

                holiday.Day = Byte.Parse(dtpFeriado.Value.Day.ToString());
                holiday.Month = Byte.Parse(dtpFeriado.Value.Month.ToString());

                holidayCollection.Add(holiday);

                this._watchComm.setHoliday(holidayCollection);

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

        private void btnEnviarLimpeza_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                switch (cboLimpeza.SelectedIndex)
                {
                    case 0:
                        this._watchComm.ClearProgramming(TypeClearProgramming.Master);
                        break;
                    case 1:
                        this._watchComm.ClearProgramming(TypeClearProgramming.RingBell);
                        break;
                    case 2:
                        this._watchComm.ClearProgramming(TypeClearProgramming.CardAndAlternativeCode);
                        break;
                    case 3:
                        this._watchComm.ClearProgramming(TypeClearProgramming.Holiday);
                        break;
                    case 4:
                        this._watchComm.ClearProgramming(TypeClearProgramming.Turn);
                        break;
                    case 5:
                        this._watchComm.ClearProgramming(TypeClearProgramming.Jornadas);
                        break;
                    case 6:
                        this._watchComm.ClearProgramming(TypeClearProgramming.AlternativeCode);
                        break;
                    case 7:
                        this._watchComm.ClearProgramming(TypeClearProgramming.MessageFunctions);
                        break;
                    case 8:
                        this._watchComm.ClearProgramming(TypeClearProgramming.All);
                        break;
                    default:
                        MessageBox.Show("Selecione uma das opções.", "Envio de Limpeza", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }

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
        #endregion        

        #region " ---------------------> Event Handles da Tab Coleta.                            

        private void btnObterRegistrosRelogio_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.GeneralTimeout = 15000;

                org.cesar.dmplight.watchComm.api.AbstractMessage marcacao;
                
                marcacao = this._watchComm.GetCurrentPunch();

                while (marcacao != null)
                {
                    AbstractPunchMessage marcacaoBio = (AbstractPunchMessage)marcacao;

                    if (marcacaoBio.IsValid)
                    {
                        DataRow dr = this._dtMarcacoes.NewRow();

                        dr["Cartao"] = marcacaoBio.ID;
                        dr["DataHoraGravacao"] = marcacaoBio.Date;
                        dr["Evento"] = marcacaoBio.WatchEvent;

                        this._dtMarcacoes.Rows.Add(dr);
                    }
                    else
                    {
                        Console.WriteLine("Registro Inválido: " + marcacaoBio.ToString());
                    }                    
                    
                    marcacao = this._watchComm.RemoveCurrentPunch();
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

        private void btnLimparGrids_Click(object sender, EventArgs e)
        {
            _dtMarcacoes = new dsSDK.dtMarcacaoDataTable();

            this.dtgMarcacoes.DataSource = _dtMarcacoes;
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Coleta Backup.                     

        private void btnEnviarColetaBackup_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();
                
                org.cesar.dmplight.watchComm.api.AbstractMessage marcacao;

                // Salva o ponteiro.
                this._watchComm.SaveBufferPoint();

                // Inicia a coleta backup.
                if (rdbColetaParcial.Checked)
                    this._watchComm.StartBackupCollect(dtpColetaParcial.Value);
                else
                    this._watchComm.StartBackupCollect();

                // Pede status no formato antigo (para contornar problema do firmware).
                this._watchComm.GetOldStatus();

                marcacao = this._watchComm.CollectWithoutRemoving();

                while (marcacao != null)
                {
                    if (marcacao != null)
                    {
                        DataRow dr = this._dtMarcacoesBackup.NewRow();

                        dr["Cartao"] = ((org.cesar.dmplight.watchComm.api.AbstractPunchMessage)(marcacao)).ID;
                        dr["DataHoraGravacao"] = ((org.cesar.dmplight.watchComm.api.AbstractPunchMessage)(marcacao)).Date;
                        dr["Evento"] = ((org.cesar.dmplight.watchComm.api.AbstractPunchMessage)(marcacao)).WatchEvent;

                        this._dtMarcacoesBackup.Rows.Add(dr);
                    }

                    marcacao = this._watchComm.RemoveCurrentPunch();
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

        private void btnLimparGridColetaBackup_Click(object sender, EventArgs e)
        {
            _dtMarcacoesBackup = new dsSDK.dtMarcacaoBackupDataTable();

            this.dtgMarcacoesBackup.DataSource = _dtMarcacoesBackup;
        }

        private void rdbColetaTotal_CheckedChanged(object sender, EventArgs e)
        {
            dtpColetaParcial.Enabled = rdbColetaParcial.Checked;
        }

        private void rdbColetaParcial_CheckedChanged(object sender, EventArgs e)
        {
            dtpColetaParcial.Enabled = rdbColetaParcial.Checked;
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Status.                            

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
            dr["Propriedade"] = "Quantidade de Funcionários";
            dr["Valor"] = status.CardsInList.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade Mínima de Digitos do Cartão";
            dr["Valor"] = status.MinDigitCard.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade Máxima de Digitos do Cartão";
            dr["Valor"] = status.MaxDigitCard.ToString();
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

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Taxa de Amostragem";
            dr["Valor"] = status.LotterySampleRate.ToString();
            this._dtStatus.Rows.Add(dr);
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Cartões.                           

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
                        cartao.Document = dr["Documento"].ToString();

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
                    this._watchComm.setCardList(colecaoCartoes, chkUtilizaImpressora.Checked);

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

        #region " ---------------------> Event Handles da Tab Templates.                         

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
                    if (fingerPrint.IsValid)
                    {
                        DataRow dr = this._dtTemplatesRecebidos.NewRow();

                        dr["Cartao"] = fingerPrint.Card;
                        dr["Template"] = fingerPrint.FingerPrint;
                        dr["Dedo1"] = (Int16)fingerPrint.FingerprintTypeOne;
                        dr["Dedo2"] = (Int16)fingerPrint.FingerprintTypeTwo;

                        this._dtTemplatesRecebidos.Rows.Add(dr);
                    }
                    else
                    {
                        Console.WriteLine("Digital Inválida!\n" + fingerPrint.ToString());
                    }

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
        }   
        #endregion
        
        #region " ---------------------> Event Handles da Tab Supervisores.                      

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
        #endregion

        #region " ---------------------> Event Handles da Tab Configurações.                     

        private void btnEnviaMensagemUsuario_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();
                this._watchComm.ConfigureMessage(org.cesar.dmplight.watchComm.business.TypeMessageConfigurantion.User, (byte)(cboMensagemUsuario.SelectedIndex + 1), txtMessageUser.Text);

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

        private void btnEnviaMensagemSistema_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                //this._watchComm.ConfigureMessage(org.cesar.dmplight.watchComm.business.TypeMessageConfigurantion.User, (byte)(cboMensagemUsuario.SelectedIndex + 1), txtMessageUser.Text);

                org.cesar.dmplight.watchComm.business.SystemTypeMessages MySystemTypeMessages;

                MySystemTypeMessages = (org.cesar.dmplight.watchComm.business.SystemTypeMessages)(Int16.Parse(cboMensagemSistema.Items[cboMensagemSistema.SelectedIndex].ToString().Substring(0, 2)));
                this._watchComm.ConfigureMessage(org.cesar.dmplight.watchComm.business.TypeMessageConfigurantion.System, (byte)MySystemTypeMessages, txtMessageSystem.Text);

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

        private void btnEnviaMensagemFuncao_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {

                this._watchComm.OpenConnection();

                org.cesar.dmplight.watchComm.business.MessageFunctionType tipoMensagemFuncao;
                org.cesar.dmplight.watchComm.business.TypeActionFunction tipoAcaoFuncao;

                tipoAcaoFuncao.ActiveFunction = chkFuncaoAtiva.Checked;
                tipoAcaoFuncao.CheckJourney = chkVerificaJornada.Checked;
                tipoAcaoFuncao.CheckList = chkVerificaLista.Checked;
                tipoAcaoFuncao.RequestsMaster = chkSolicitaSupervisor.Checked;
                tipoAcaoFuncao.RequesTypingKeyboard = chkPermiteDigitacaoViaTeclado.Checked;
                tipoAcaoFuncao.StoresRecordBlocked = chkArmazenaRegistroBloqueado.Checked;
                tipoAcaoFuncao.TriggerOut = chkAcionaDispositivoExterno.Checked;
                tipoAcaoFuncao.StoresRecordFreed = chkArmazenaRegistroPermitido.Checked;

                tipoMensagemFuncao = (org.cesar.dmplight.watchComm.business.MessageFunctionType)(Int16.Parse(cboMensagemFuncao.Items[cboMensagemFuncao.SelectedIndex].ToString().Substring(0, 2)));

                this._watchComm.ConfigureMessage(org.cesar.dmplight.watchComm.business.TypeMessageConfigurantion.Function, (byte)tipoMensagemFuncao, txtMessageFunction.Text, tipoAcaoFuncao);

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

        private void CarregaTelaFormatoMemoria(MemoryFormat memoria)
        {
            chkTemJornada.Checked = memoria.HasWork;
            chkTemMensagem.Checked = memoria.HasMessage;
            chkTemVia.Checked = memoria.HasWay;
            chkTemSenha.Checked = memoria.HasPassword;

            nudMaximoDigitosCartao.Value = memoria.MaxDigitCard;
            nudMinimoDigitosCartao.Value = memoria.MinDigitCard;
            nudContadoresAcesso.Value = memoria.CounterAccess;
            nudMaximoCartoes.Value = memoria.QuantityMaxCard;
            nudMaximoCodigoAlternativo.Value = memoria.QuantityMaxAlternativeId;
            nudMaximoJornadasSemanais.Value = memoria.QuantityMaxWeeklyWork;
            nudMaximoJornadasMensais.Value = memoria.QuantityMaxMonthlyWork;
            nudMaximoJornadasPeriodicas.Value = memoria.QuantityMaxPeriodicWork;
            nudMaximoTurnos.Value = memoria.QuantityMaxShiftTable;
            nudMaximoSinaleiros.Value = memoria.QuantityMaxAlarmRing;
            nudMaximoFeriados.Value = memoria.QuantityMaxHoliday;
            nudMaximoFuncoes.Value = memoria.QuantityMaxFunction;
            nudMaximoMensagensUsuario.Value = memoria.MaxMessageUser;

            if (memoria.TypeCheck == TypeCheckCard.NoCheck)
                rdoSemChecagem.Checked = true;
            else if (memoria.TypeCheck == TypeCheckCard.Modulo10)
                rdoModulo10.Checked = true;
            else
                rdoModulo11.Checked = true;
        }

        private void btnEnviarFormatoMemoria_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                MemoryFormat memoryFormat = new MemoryFormat();

                memoryFormat.CounterAccess = (byte)nudContadoresAcesso.Value;
                memoryFormat.HasMessage = chkTemMensagem.Checked;
                memoryFormat.HasPassword = chkTemSenha.Checked;
                memoryFormat.HasWork = chkTemJornada.Checked;
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

        private void btnObterFormatoMemoria_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

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

                CarregaTelaFormatoMemoria(memory.MemoryFormat);

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

        private void btnEnviarConfiguracoesGerais_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                // Criptografia.
                if (chkUtilizaCriptografia.Checked)
                    this._watchComm.CardEncryption(rdo8Digitos.Checked ? org.cesar.dmplight.watchComm.impl.EGenericEncryptionType.EightDigits : org.cesar.dmplight.watchComm.impl.EGenericEncryptionType.TwelveDigits, Convert.ToInt64(txtVetor1.Text), Convert.ToInt64(txtVetor2.Text), Convert.ToInt32(txtVerificador1.Text), Convert.ToInt32(txtVerificador2.Text));
                else
                    this._watchComm.CardEncryption(org.cesar.dmplight.watchComm.impl.EGenericEncryptionType.Disabled, 0, 0, 0, 0);

                // Acionamento.
                if (mskTempoAcionamento.Enabled)
                    this._watchComm.ProgramTriggerType(ConverteAcionamento(), Int32.Parse((Double.Parse(mskTempoAcionamento.Text) * 10).ToString()));
                else
                    this._watchComm.ProgramTriggerType(ConverteAcionamento(), 8);

                // Taxa de amostragem.
                this._watchComm.ProgramLotterySampleRate(Byte.Parse((Int32.Parse(txtTaxaAmostragem.Text) / 10).ToString()), 0); // O segundo parâmetro não é utilizado para esse modelo de equipamento.

                // Terminal ativado.
                this._watchComm.Activation(chkTerminalAtivado.Checked, chkAcessoControlado.Checked);

                // Leitura da Digital.
                this._watchComm.ProgramBiometricReaderUse(chkPrimeiroLeitor.Checked, chkSegundoLeitor.Checked);

                // Registra bloqueados.
                this._watchComm.EnableLogDeniedAccess(chkRegistraBloqueados.Checked);

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

        private byte ConverteAcionamento()
        {
            switch (cboAcionamento.SelectedIndex)
            {
                case 0:
                    return 0;
                case 1:
                    return 2;
                case 2:
                    return 8;
                case 3:
                    return 9;
                case 4:
                    return 10;
                case 5:
                    return 11;
                case 6:
                    return 13;
                case 7:
                    return 14;
                case 8:
                    return 17;
                case 9:
                    return 18;
                case 10:
                    return 19;
                default:
                    return 0;
            }
        }

        private void chkUtilizaCriptografia_CheckedChanged(object sender, EventArgs e)
        {
            grpCriptografia.Enabled = chkUtilizaCriptografia.Checked;
        }

        private void btnObterFormatoCartao_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                txtFormatoCartao.Text = this._watchComm.InquiryCardFormat();

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

        private void btnEnviarFormatoCartao_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.ConfigureCard(this.txtFormatoCartao.Text);

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

        private void btnFormatoCartao_Click(object sender, EventArgs e)
        {
            frmFormatoCartao frmFormatoCartao = new frmFormatoCartao(this.txtFormatoCartao.Text);
            frmFormatoCartao.ShowDialog();
            this.txtFormatoCartao.Text = frmFormatoCartao.FormatoCartao;
            frmFormatoCartao.Dispose();
            frmFormatoCartao = null;
        }

        private void cboAcionamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboAcionamento.SelectedIndex)
            {
                case 0:

                    chkAcessoControlado.Enabled = false;
                    mskTempoAcionamento.Enabled = false;

                    break;
                case 1:

                    chkAcessoControlado.Enabled = true;
                    mskTempoAcionamento.Enabled = true;

                    break;
                case 2:

                    chkAcessoControlado.Enabled = true;
                    mskTempoAcionamento.Enabled = false;

                    break;
                case 3:

                    chkAcessoControlado.Enabled = true;
                    mskTempoAcionamento.Enabled = false;

                    break;
                case 4:

                    chkAcessoControlado.Enabled = true;
                    mskTempoAcionamento.Enabled = false;

                    break;
                case 5:

                    chkAcessoControlado.Enabled = true;
                    mskTempoAcionamento.Enabled = false;

                    break;
                case 6:

                    chkAcessoControlado.Enabled = true;
                    mskTempoAcionamento.Enabled = false;

                    break;
                case 7:

                    chkAcessoControlado.Enabled = true;
                    mskTempoAcionamento.Enabled = false;

                    break;
                case 8:

                    chkAcessoControlado.Enabled = true;
                    mskTempoAcionamento.Enabled = true;

                    break;
                case 9:

                    chkAcessoControlado.Enabled = true;
                    mskTempoAcionamento.Enabled = true;

                    break;
                case 10:

                    chkAcessoControlado.Enabled = true;
                    mskTempoAcionamento.Enabled = true;

                    break;
            }
        }
        #endregion                       
        
        #region " ---------------------> Event Handles da Tab Códigos Alternativos.              

        private void btnInclusaoCodigosAlternativos_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                // Insere os funcionários na lista do componente.
                foreach (DataRow dr in this._dtCodigosAlternativos.Rows)
                {
                    try
                    {
                        this._watchComm.setAlternativeCode(Int32.Parse(dr["CodigoAlternativo"].ToString()), dr["Cartao"].ToString());
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }

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

        private void btnExclusaoCodigosAlternativos_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                // Insere os funcionários na lista do componente.
                foreach (DataRow dr in this._dtCodigosAlternativos.Rows)
                {
                    try
                    {
                        this._watchComm.removeAlternativeCode(Int32.Parse(dr["CodigoAlternativo"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }

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
        #endregion

        #region " ---------------------> Event Handles da Tab Faixas Horárias.                   

        private void btnEnviarFaixaHoraria_Click(object sender, EventArgs e)
        {
            ShiftTableCollection shiftTableCollection;
            ShiftTable shift;

            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                shiftTableCollection = new ShiftTableCollection();

                shiftTableCollection.Id = Int16.Parse(txtCodigoFaixaHoraria.Text);

                shift = new ShiftTable();

                if (dtpPrimeiroIntervalo1.Checked && dtpPrimeiroIntervalo2.Checked)
                {
                    shift.Inicio = dtpPrimeiroIntervalo1.Value.ToString("HHmm");
                    shift.Fim = dtpPrimeiroIntervalo2.Value.ToString("HHmm");
                }
                else
                {
                    shift.Inicio = "0000";
                    shift.Fim = "0000";
                }

                shiftTableCollection.Add(shift);

                shift = new ShiftTable();

                if (dtpSegundoIntervalo1.Checked && dtpSegundoIntervalo2.Checked)
                {
                    shift.Inicio = dtpSegundoIntervalo1.Value.ToString("HHmm");
                    shift.Fim = dtpSegundoIntervalo2.Value.ToString("HHmm");
                }
                else
                {
                    shift.Inicio = "0000";
                    shift.Fim = "0000";
                }

                shiftTableCollection.Add(shift);

                shift = new ShiftTable();

                if (dtpTerceiroIntervalo1.Checked && dtpTerceiroIntervalo2.Checked)
                {
                    shift.Inicio = dtpTerceiroIntervalo1.Value.ToString("HHmm");
                    shift.Fim = dtpTerceiroIntervalo2.Value.ToString("HHmm");
                }
                else
                {
                    shift.Inicio = "0000";
                    shift.Fim = "0000";
                }

                shiftTableCollection.Add(shift);

                shift = new ShiftTable();

                if (dtpQuartoIntervalo1.Checked && dtpQuartoIntervalo2.Checked)
                {
                    shift.Inicio = dtpQuartoIntervalo1.Value.ToString("HHmm");
                    shift.Fim = dtpQuartoIntervalo2.Value.ToString("HHmm");
                }
                else
                {
                    shift.Inicio = "0000";
                    shift.Fim = "0000";
                }

                shiftTableCollection.Add(shift);

                shift = new ShiftTable();

                if (dtpQuintoIntervalo1.Checked && dtpQuintoIntervalo2.Checked)
                {
                    shift.Inicio = dtpQuintoIntervalo1.Value.ToString("HHmm");
                    shift.Fim = dtpQuintoIntervalo2.Value.ToString("HHmm");
                }
                else
                {
                    shift.Inicio = "0000";
                    shift.Fim = "0000";
                }

                shiftTableCollection.Add(shift);

                shift = new ShiftTable();

                if (dtpSextoIntervalo1.Checked && dtpSextoIntervalo2.Checked)
                {
                    shift.Inicio = dtpSextoIntervalo1.Value.ToString("HHmm");
                    shift.Fim = dtpSextoIntervalo2.Value.ToString("HHmm");
                }
                else
                {
                    shift.Inicio = "0000";
                    shift.Fim = "0000";
                }

                shiftTableCollection.Add(shift);

                this._watchComm.UpdateShiftTable(shiftTableCollection);

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

        private void txtCodigoFaixaHoraria_Leave(object sender, EventArgs e)
        {
            if (!txtCodigoFaixaHoraria.Text.Equals(""))
            {
                if (Int16.Parse(txtCodigoFaixaHoraria.Text) > 255)
                    txtCodigoFaixaHoraria.Text = "255";
            }
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Jornadas.                          

        private void Maximo255(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (!txt.Text.Equals(""))
            {
                if (Int16.Parse(txt.Text) > 255)
                    txt.Text = "255";
            }
        }

        private void btnEnviarJornadaSemanal_Click(object sender, EventArgs e)
        {
            WeeklyJourneyWorking jornada;

            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                jornada = new WeeklyJourneyWorking();

                jornada.Id = Int16.Parse(txtCodigoJornadaSemanal.Text);
                jornada.TypeWorking = TypeWorking.Weekly;

                jornada.Sunday = Int16.Parse(txtJSemanalDomingo.Text);
                jornada.Monday = Int16.Parse(txtJSemanalSegunda.Text);
                jornada.Tuesday = Int16.Parse(txtJSemanalTerca.Text);
                jornada.Wednesday = Int16.Parse(txtJSemanalQuarta.Text);
                jornada.Thursday = Int16.Parse(txtJSemanalQuinta.Text);
                jornada.Friday = Int16.Parse(txtJSemanalSexta.Text);
                jornada.Saturday = Int16.Parse(txtJSemanalSabado.Text);
                jornada.Holiday = Int16.Parse(txtJSemanalDomingo.Text);

                this._watchComm.setJourneyWorking(jornada);

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

        private void btnEnviarJornadaMensal_Click(object sender, EventArgs e)
        {
            MonthlyJourneyWorking jornada;

            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                jornada = new MonthlyJourneyWorking();

                jornada.Id = Int16.Parse(txtCodigoJornadaMensal.Text);
                jornada.TypeWorking = TypeWorking.Monthly;

                jornada.Turno.Add(Int16.Parse(txtJMensal1.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal2.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal3.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal4.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal5.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal6.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal7.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal8.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal9.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal10.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal11.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal12.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal13.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal14.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal15.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal16.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal17.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal18.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal19.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal20.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal21.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal22.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal23.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal24.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal25.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal26.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal27.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal28.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal29.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal30.Text));
                jornada.Turno.Add(Int16.Parse(txtJMensal31.Text));
                jornada.Turno.Add(Int16.Parse(txtJFeriado.Text));
                this._watchComm.setJourneyWorking(jornada);

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

        private void txtDuracao_Leave(object sender, EventArgs e)
        {
            Control text;
            Int16 dias;

            if (!txtDuracao.Text.Equals(""))
            {
                if (Int16.Parse(txtDuracao.Text) > 60)
                    txtDuracao.Text = "60";
                else if (Int16.Parse(txtDuracao.Text) < 2)
                    txtDuracao.Text = "2";

                dias = Int16.Parse(txtDuracao.Text);

                foreach (var item in tbpJornadaPeriodica.Controls)
                {
                    text = (Control)item;

                    if (text.Name.Contains("txtJPeriodica"))
                    {
                        if (Int16.Parse(text.Name.Replace("txtJPeriodica", "")) <= dias)
                            text.Enabled = true;
                        else
                            text.Enabled = false;
                    }
                }
            }
        }

        private void btnEnviarJornadaPeriodica_Click(object sender, EventArgs e)
        {
            PeriodicJourneyWorking jornada;

            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                jornada = new PeriodicJourneyWorking();

                jornada.Id = Int16.Parse(txtCodigoJornadaPeriodica.Text);
                jornada.TypeWorking = TypeWorking.Periodic;
                jornada.DataInicio = dtpInicioJornadaPeriodica.Value;

                if (txtJPeriodica1.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica1.Text));

                if (txtJPeriodica2.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica2.Text));

                if (txtJPeriodica3.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica3.Text));

                if (txtJPeriodica4.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica4.Text));

                if (txtJPeriodica5.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica5.Text));

                if (txtJPeriodica6.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica6.Text));

                if (txtJPeriodica7.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica7.Text));

                if (txtJPeriodica8.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica8.Text));

                if (txtJPeriodica9.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica9.Text));

                if (txtJPeriodica10.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica10.Text));

                if (txtJPeriodica11.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica11.Text));

                if (txtJPeriodica12.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica12.Text));

                if (txtJPeriodica13.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica13.Text));

                if (txtJPeriodica14.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica14.Text));

                if (txtJPeriodica15.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica15.Text));

                if (txtJPeriodica16.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica16.Text));

                if (txtJPeriodica17.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica17.Text));

                if (txtJPeriodica18.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica18.Text));

                if (txtJPeriodica19.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica19.Text));

                if (txtJPeriodica20.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica20.Text));

                if (txtJPeriodica21.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica21.Text));

                if (txtJPeriodica22.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica22.Text));

                if (txtJPeriodica23.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica23.Text));

                if (txtJPeriodica24.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica24.Text));

                if (txtJPeriodica25.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica25.Text));

                if (txtJPeriodica26.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica26.Text));

                if (txtJPeriodica27.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica27.Text));

                if (txtJPeriodica28.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica28.Text));

                if (txtJPeriodica29.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica29.Text));

                if (txtJPeriodica30.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica30.Text));

                if (txtJPeriodica31.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica31.Text));

                if (txtJPeriodica32.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica32.Text));

                if (txtJPeriodica33.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica33.Text));

                if (txtJPeriodica34.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica34.Text));

                if (txtJPeriodica35.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica35.Text));

                if (txtJPeriodica36.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica36.Text));

                if (txtJPeriodica37.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica37.Text));

                if (txtJPeriodica38.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica38.Text));

                if (txtJPeriodica39.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica39.Text));

                if (txtJPeriodica40.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica40.Text));

                if (txtJPeriodica41.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica41.Text));

                if (txtJPeriodica42.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica42.Text));

                if (txtJPeriodica43.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica43.Text));

                if (txtJPeriodica44.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica44.Text));

                if (txtJPeriodica45.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica45.Text));

                if (txtJPeriodica46.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica46.Text));

                if (txtJPeriodica47.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica47.Text));

                if (txtJPeriodica48.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica48.Text));

                if (txtJPeriodica49.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica49.Text));

                if (txtJPeriodica50.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica50.Text));

                if (txtJPeriodica51.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica51.Text));

                if (txtJPeriodica52.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica52.Text));

                if (txtJPeriodica53.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica53.Text));

                if (txtJPeriodica54.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica54.Text));

                if (txtJPeriodica55.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica55.Text));

                if (txtJPeriodica56.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica56.Text));

                if (txtJPeriodica57.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica57.Text));

                if (txtJPeriodica58.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica58.Text));

                if (txtJPeriodica59.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica59.Text));

                if (txtJPeriodica60.Enabled)
                    jornada.Turno.Add(Int16.Parse(txtJPeriodica60.Text));

                this._watchComm.setJourneyWorking(jornada);

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
    }
}