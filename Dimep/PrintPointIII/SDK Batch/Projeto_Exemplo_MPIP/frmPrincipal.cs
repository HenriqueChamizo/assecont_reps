using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ExemploMPIP.DataSet;
using org.cesar.dmplight.watchComm.api;
using org.cesar.dmplight.watchComm.business;
using org.cesar.dmplight.watchComm.impl.micropointip;

namespace ExemploMPIP
{
    public partial class frmPrincipal : Form
    {
        #region " ---------------------> Atributos.

        private org.cesar.dmplight.watchComm.impl.WatchComm _watchComm;
        private dsSDK _dsMIP;
        private dsSDK.dtMarcacaoDataTable _dtMarcacoes;
        private dsSDK.dtMarcacaoBackupDataTable _dtMarcacoesBackup;
        private dsSDK.dtStatusDataTable _dtStatus;
        private dsSDK.dtFuncoesDataTable _dtFuncoes;
        private dsSDK.dtCredenciaisDataTable _dtCredenciais;

        #endregion

        #region " ---------------------> Construtor.

        public frmPrincipal()
        {
            InitializeComponent();

            this._dsMIP = new dsSDK();
            BindingDataTables();

            SetDefaults();

            for (int i = 0; i < 20; i++)
            {
                DataRow dr = this._dtFuncoes.NewRow();
                dr["Codigo"] = i + 1;
                this._dtFuncoes.Rows.Add(dr);
            }
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
            this.dtgMarcacoes.DataSource = this._dtMarcacoes;
            this.dtgMarcacoesBackup.DataSource = this._dtMarcacoesBackup;
            this.dtgFuncoes.DataSource = this._dtFuncoes;
            this.dtgListaFuncionarios.DataSource = this._dtCredenciais;
        }

        private void BindingDataTables()
        {
            this._dtMarcacoes = this._dsMIP.dtMarcacao;
            this._dtMarcacoesBackup = this._dsMIP.dtMarcacaoBackup;
            this._dtStatus = this._dsMIP.dtStatus;
            this._dtFuncoes = this._dsMIP.dtFuncoes;
            this._dtCredenciais = this._dsMIP.dtCredenciais;
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

            this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.MicroPointIp,
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

            cboLimpeza.SelectedIndex = 0;
            cboSentidoEntrada.SelectedIndex = 0;
            cboAcionamento.SelectedIndex = 0;
            cboNumeroDeLeitorasCartao.SelectedIndex = 0;
            cboDiaDaSemanaSinaleiro.SelectedIndex = 0;
            cboModoOperacao.SelectedIndex = 0;
            cboLeitoraCartaoAtuacaoLista.SelectedIndex = 0;
            cboLeitoraCartaoAtuacaoSenha.SelectedIndex = 0;
            cboModoLiberacao.SelectedIndex = 0;
        }

        #endregion

        #region " ---------------------> Leitura e gravação das informações contidas nos grids.

        private void btnGravarXML_Click(object sender, EventArgs e)
        {
            this._dsMIP.WriteXml(this.CaminhoEXE() + "\\DataDSMIPCH.xml");

            MessageBox.Show("XML gravado com sucesso!", Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnLerXML_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(this.CaminhoEXE() + "\\DataDSMIPCH.xml"))
            {
                this._dsMIP = new dsSDK();
                this._dsMIP.ReadXml(this.CaminhoEXE() + "\\DataDSMIPCH.xml");

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

        private void btnConfiguracao_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.SendSettings(
                    int.Parse(nupNumeroDigitosCartao.Value.ToString()),
                    chkPossuiChecagem.Checked,
                    chkUtilizaVia.Checked,
                    (EEntryDirection)cboSentidoEntrada.SelectedIndex,
                    (ETriggerType)cboAcionamento.SelectedIndex,
                    int.Parse(nupPersonalizacao.Value.ToString()),
                    (ENumberCardReaders)cboNumeroDeLeitorasCartao.SelectedIndex);

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

        private void btnTerminalAtivo_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.Activation(chkTerminalAtivo.Checked);

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

        private void txtDuracaoSinaleiro_Leave(object sender, EventArgs e)
        {
            if (!txtDuracaoSinaleiro.Text.Equals(""))
            {
                if (Int16.Parse(txtDuracaoSinaleiro.Text) > 60)
                    txtDuracaoSinaleiro.Text = "60";
            }
        }

        private void btnEnviarSinaleiro_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                AlarmRingsCollection alarmRingsCollection = new AlarmRingsCollection();

                MicroPointIpAlarmRings alarmRings = new MicroPointIpAlarmRings();
                alarmRings.WeekDay = (EWeekDay)cboDiaDaSemanaSinaleiro.SelectedIndex + 1;
                alarmRings.Duration = byte.Parse(txtDuracaoSinaleiro.Text);
                alarmRings.TimeAlarm = dtpHorarioSinaleiro.Text;

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

        private void btnEnviarSinaleiroAtivado_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.AlarmRing(chkSinaleiroAtivo.Checked);

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
                        this._watchComm.ClearProgramming(ETypeClearProgramming.CredentialList);
                        break;
                    case 1:
                        this._watchComm.ClearProgramming(ETypeClearProgramming.ShiftTableJourneyWorking);
                        break;
                    case 2:
                        this._watchComm.ClearProgramming(ETypeClearProgramming.AlarmRing);
                        break;
                    case 3:
                        this._watchComm.ClearProgramming(ETypeClearProgramming.Message);
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

        private void btnEnviaMensagem_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.ConfigureMessage((byte)nupMensagem.Value, txtMensagem.Text);

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

        private void btnMensagensAtivo_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.Message(chkMensagensAtivo.Checked);

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

        private void btnListaAtivado_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.CredentialList(chkListaAtivado.Checked);

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

        private void btnAlteracaoIndividualConsulta_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();
            MicroPointIpCredentialListHeader credentialListHeader = new MicroPointIpCredentialListHeader();

            switch (cboModoOperacao.SelectedIndex)
            {
                case 0:
                    credentialListHeader.OperationMode = EOperationMode.WhiteList;
                    break;
                case 1:
                    credentialListHeader.OperationMode = EOperationMode.BlackList;
                    break;
                case 2:
                    credentialListHeader.OperationMode = EOperationMode.Both;
                    break;
            }

            credentialListHeader.HasCopyNumber = chkVersaoNaLista.Checked;
            credentialListHeader.HasMessage = chkMensagemNaLista.Checked;
            credentialListHeader.HasPassword = chkSenhaNaLista.Checked;
            credentialListHeader.HasShiftTableJourneyWorking = chkFaixaHorariaOuGrupoNaLista.Checked;
            credentialListHeader.HasInquiryField = chkConsultaNaLista.Checked;

            switch (cboLeitoraCartaoAtuacaoLista.SelectedIndex)
            {
                case 0:
                    credentialListHeader.CardReaderApplies = ECardReader.None;
                    break;
                case 1:
                    credentialListHeader.CardReaderApplies = ECardReader.CardReader1;
                    break;
                case 2:
                    credentialListHeader.CardReaderApplies = ECardReader.CardReader2;
                    break;
                case 3:
                    credentialListHeader.CardReaderApplies = ECardReader.Both;
                    break;
            }

            switch (cboLeitoraCartaoAtuacaoSenha.SelectedIndex)
            {
                case 0:
                    credentialListHeader.CardReaderPasswordApplies = ECardReader.None;
                    break;
                case 1:
                    credentialListHeader.CardReaderPasswordApplies = ECardReader.CardReader1;
                    break;
                case 2:
                    credentialListHeader.CardReaderPasswordApplies = ECardReader.CardReader2;
                    break;
                case 3:
                    credentialListHeader.CardReaderPasswordApplies = ECardReader.Both;
                    break;
            }

            switch (cboModoLiberacao.SelectedIndex)
            {
                case 0:
                    credentialListHeader.BlackListBothGrantType = EGrantType.Undefined;
                    break;
                case 1:
                    credentialListHeader.BlackListBothGrantType = EGrantType.NotGranted;
                    break;
                case 2:
                    credentialListHeader.BlackListBothGrantType = EGrantType.Granted;
                    break;
                case 3:
                    credentialListHeader.BlackListBothGrantType = EGrantType.GrantedCheckShiftTable;
                    break;
                case 4:
                    credentialListHeader.BlackListBothGrantType = EGrantType.GrantedCheckJourneyWorking;
                    break;
            }

            credentialListHeader.ShiftTableJourneyWorkingId = txtCodigoFaixaJornada.Text.Length > 0 ? int.Parse(txtCodigoFaixaJornada.Text) : 0;

            credentialListHeader.InquiryFieldLength = txtTamanhoCampoConsulta.Text.Length > 0 ? int.Parse(txtTamanhoCampoConsulta.Text) : 0;

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.ChangeInquiry(credentialListHeader, int.Parse(nupNumeroDigitosCartao.Value.ToString()), long.Parse(txtAlteracaoIndividualConsultaNumeroCredencial.Text), txtAlteracaoIndividualConsulta.Text);

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

        #region " ---------------------> Event Handles da Tab Funções

        private void btnEnviarFuncoes_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                FunctionCollection functionCollection = new FunctionCollection();

                foreach (DataRow dr in _dtFuncoes)
                {
                    Function function = new Function();

                    function.Id = int.Parse(dr["Codigo"].ToString());
                    function.Label = dr["Rotulo"].ToString();

                    function.Enabled = dr["Ativada"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["Ativada"].ToString());
                    function.RequesTypingKeyboard = dr["Teclado"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["Teclado"].ToString());
                    function.Protected = dr["Protegida"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["Protegida"].ToString());
                    function.CheckCredentialList = dr["ChecagemEmLista"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["ChecagemEmLista"].ToString());
                    function.TriggerOut = dr["Acionamento"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["Acionamento"].ToString());
                    function.StoresRecordBlocked = dr["ArmazenaCodigoImpedido"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["ArmazenaCodigoImpedido"].ToString());

                    function.CheckCardCopyNumber = dr["Versao"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["Versao"].ToString());
                    function.CheckMessage = dr["Mensagem"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["Mensagem"].ToString());
                    function.CheckPassword = dr["Senha"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["Senha"].ToString());
                    function.CheckJourney = dr["FaixaHoraria"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["FaixaHoraria"].ToString());
                    function.CheckInquiry = dr["Consulta"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["Consulta"].ToString());

                    function.StoresRecordInvalid = dr["ArmazenaCodigoInvalido"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["ArmazenaCodigoInvalido"].ToString());
                    function.ReturnFunction = dr["RetornoDeFuncaoTemporaria"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["RetornoDeFuncaoTemporaria"].ToString());
                    function.Permanent = dr["FuncaoPermanente"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["FuncaoPermanente"].ToString());

                    functionCollection.Add(function);
                }

                this._watchComm.setFunction(functionCollection);

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

                AbstractMessage marcacao;

                marcacao = this._watchComm.GetCurrentPunch();

                while (marcacao != null)
                {
                    MicroPointIpPunchMessage marcacaoMicro = (MicroPointIpPunchMessage)marcacao;

                    if (marcacaoMicro.IsValid)
                    {
                        DataRow dr = this._dtMarcacoes.NewRow();

                        dr["Cartao"] = marcacaoMicro.ID;
                        dr["DataHoraGravacao"] = marcacaoMicro.Date;
                        dr["Evento"] = marcacaoMicro.WatchEvent;
                        dr["Funcao"] = marcacaoMicro.Function;

                        this._dtMarcacoes.Rows.Add(dr);
                    }
                    else
                    {
                        Console.WriteLine("Registro Inválido: " + marcacaoMicro.DataArea);
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

                AbstractMessage marcacao;

                // Inicia a coleta backup.
                if (rdbColetaParcial.Checked)
                    this._watchComm.StartBackupCollect(dtpColetaParcial.Value);
                else
                    this._watchComm.StartBackupCollect();

                marcacao = this._watchComm.GetCurrentPunch();

                while (marcacao != null)
                {
                    MicroPointIpPunchMessage marcacaoMicro = (MicroPointIpPunchMessage)marcacao;

                    if (marcacaoMicro.IsValid)
                    {
                        DataRow dr = this._dtMarcacoesBackup.NewRow();

                        dr["Cartao"] = marcacaoMicro.ID;
                        dr["DataHoraGravacao"] = marcacaoMicro.Date;
                        dr["Evento"] = marcacaoMicro.WatchEvent;
                        dr["Funcao"] = marcacaoMicro.Function;

                        this._dtMarcacoesBackup.Rows.Add(dr);
                    }
                    else
                    {
                        Console.WriteLine("Registro Inválido: " + marcacaoMicro.DataArea);
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

                MicroPointIpStatusMessage status = this._watchComm.GetMicroPointIpStatus();


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

        private void CarregaGridStatus(MicroPointIpStatusMessage status)
        {
            DataRow dr;

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Data e Hora";
            dr["Valor"] = status.DateAndTime;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade de registros";
            dr["Valor"] = status.TotalRecords;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade de dígitos";
            dr["Valor"] = status.NumberDigits;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Versão do crachá";
            dr["Valor"] = status.HasCopyNumber ? "Com versão de crachá" : "Sem versão de crachá";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Módulo 11";
            dr["Valor"] = status.HasChecking ? "Com módulo 11" : "Sem módulo 11";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Sentido de entrada";
            switch (status.EntryDirection)
            {
                case EEntryDirection.LeftToRight:
                    dr["Valor"] = "Esquerda para a direita";
                    break;
                case EEntryDirection.RightToLeft:
                    dr["Valor"] = "Direita para a esquerda";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Nº de leitoras de cartão";
            switch (status.NumberCardReaders)
            {
                case ENumberCardReaders.One:
                    dr["Valor"] = "Uma leitora";
                    break;
                case ENumberCardReaders.Two:
                    dr["Valor"] = "Duas leitoras";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Acionamento";
            switch (status.TriggerType)
            {
                case ETriggerType.None:
                    dr["Valor"] = "Sem acionamento";
                    break;
                case ETriggerType.Door:
                    dr["Valor"] = "Acionamento de porta";
                    break;
                case ETriggerType.OneWayTurnstile:
                    dr["Valor"] = "Catraca unidirecional";
                    break;
                case ETriggerType.TwoWayTurnstile:
                    dr["Valor"] = "Catraca bidirecional";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Dia do último registro recolhido";
            dr["Valor"] = status.DayLastRecordInquired;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "% Memória";
            dr["Valor"] = status.MemoryUsedPercentage;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Dia da última falha AC";
            dr["Valor"] = status.LastDayPowerFailure;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Hora da última falha AC";
            dr["Valor"] = status.LastHourPowerFailure;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Minuto da última falha AC";
            dr["Valor"] = status.LastMinutePowerFailure;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Duração da falha (minutos)";
            dr["Valor"] = status.LastPowerFailureDurationMinutes;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Condição AC";
            dr["Valor"] = status.BatteryAttached ? "Com bateria" : "Sem bateria";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Ativado / Desativado";
            dr["Valor"] = status.TerminalEnabled ? "Terminal ativado" : "Terminal desativado";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Horário de verão";
            switch (status.DstStatus)
            {
                case EDSTStatus.Disabled:
                    dr["Valor"] = "Desativado";
                    break;
                case EDSTStatus.EnabledOutOfPeriod:
                    dr["Valor"] = "Ativado e fora do horário de verão";
                    break;
                case EDSTStatus.EnabledWithinPeriod:
                    dr["Valor"] = "Ativado e dentro do horário de verão";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Lista";
            switch (status.CardListStatus)
            {
                case ECardListStatus.NotLoaded:
                    dr["Valor"] = "Não carregada";
                    break;
                case ECardListStatus.LoadedEnabled:
                    dr["Valor"] = "Carregada e ativada";
                    break;
                case ECardListStatus.LoadedDisabled:
                    dr["Valor"] = "Carregada e desativada";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Faixa horária";
            dr["Valor"] = status.TimeSlotsLoaded ? "Tabela das faixas horárias carregadas" : "Tabela de faixas horárias não carregadas";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Grupo - Faixa";
            dr["Valor"] = status.TimeSlotGroupsLoaded ? "Tabela de grupo de faixas horárias carregadas" : "Tabela de grupo de faixas horárias não carregadas";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Sinaleiro";
            dr["Valor"] = status.AlarmRingsLoaded ? "Tabela de sinaleiro carregada" : "Tabela de sinaleiro não carregada";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Mensagem";
            dr["Valor"] = status.MessagesLoaded ? "Tabela de mensagem carregada" : "Tabela de mensagem não carregada";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Versão do Firmware";
            dr["Valor"] = status.FirmwareVersion;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Função Corrente";
            dr["Valor"] = status.CurrentFunction;
            this._dtStatus.Rows.Add(dr);
        }

        #endregion

        #region " ---------------------> Event Handles da Tab Cartões.

        private void btnCabecalhoDaLista_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                MicroPointIpCredentialListHeader credentialListHeader = new MicroPointIpCredentialListHeader();

                switch (cboModoOperacao.SelectedIndex)
                {
                    case 0:
                        credentialListHeader.OperationMode = EOperationMode.WhiteList;
                        break;
                    case 1:
                        credentialListHeader.OperationMode = EOperationMode.BlackList;
                        break;
                    case 2:
                        credentialListHeader.OperationMode = EOperationMode.Both;
                        break;
                }

                credentialListHeader.HasCopyNumber = chkVersaoNaLista.Checked;
                credentialListHeader.HasMessage = chkMensagemNaLista.Checked;
                credentialListHeader.HasPassword = chkSenhaNaLista.Checked;
                credentialListHeader.HasShiftTableJourneyWorking = chkFaixaHorariaOuGrupoNaLista.Checked;
                credentialListHeader.HasInquiryField = chkConsultaNaLista.Checked;

                switch (cboLeitoraCartaoAtuacaoLista.SelectedIndex)
                {
                    case 0:
                        credentialListHeader.CardReaderApplies = ECardReader.None;
                        break;
                    case 1:
                        credentialListHeader.CardReaderApplies = ECardReader.CardReader1;
                        break;
                    case 2:
                        credentialListHeader.CardReaderApplies = ECardReader.CardReader2;
                        break;
                    case 3:
                        credentialListHeader.CardReaderApplies = ECardReader.Both;
                        break;
                }

                switch (cboLeitoraCartaoAtuacaoSenha.SelectedIndex)
                {
                    case 0:
                        credentialListHeader.CardReaderPasswordApplies = ECardReader.None;
                        break;
                    case 1:
                        credentialListHeader.CardReaderPasswordApplies = ECardReader.CardReader1;
                        break;
                    case 2:
                        credentialListHeader.CardReaderPasswordApplies = ECardReader.CardReader2;
                        break;
                    case 3:
                        credentialListHeader.CardReaderPasswordApplies = ECardReader.Both;
                        break;
                }

                switch (cboModoLiberacao.SelectedIndex)
                {
                    case 0:
                        credentialListHeader.BlackListBothGrantType = EGrantType.Undefined;
                        break;
                    case 1:
                        credentialListHeader.BlackListBothGrantType = EGrantType.NotGranted;
                        break;
                    case 2:
                        credentialListHeader.BlackListBothGrantType = EGrantType.Granted;
                        break;
                    case 3:
                        credentialListHeader.BlackListBothGrantType = EGrantType.GrantedCheckShiftTable;
                        break;
                    case 4:
                        credentialListHeader.BlackListBothGrantType = EGrantType.GrantedCheckJourneyWorking;
                        break;
                }

                credentialListHeader.ShiftTableJourneyWorkingId = txtCodigoFaixaJornada.Text.Length > 0 ? int.Parse(txtCodigoFaixaJornada.Text) : 0;

                credentialListHeader.InquiryFieldLength = txtTamanhoCampoConsulta.Text.Length > 0 ? int.Parse(txtTamanhoCampoConsulta.Text) : 0;

                this._watchComm.SetCredentialListHeader(credentialListHeader);

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

        private void btnIncluirFuncionarios_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {

                BindingDataGrids();
                BindingDataTables();

                MicroPointIpCredentialListHeader credentialListHeader = new MicroPointIpCredentialListHeader();
                switch (cboModoOperacao.SelectedIndex)
                {
                    case 0:
                        credentialListHeader.OperationMode = EOperationMode.WhiteList;
                        break;
                    case 1:
                        credentialListHeader.OperationMode = EOperationMode.BlackList;
                        break;
                    case 2:
                        credentialListHeader.OperationMode = EOperationMode.Both;
                        break;
                }

                credentialListHeader.HasCopyNumber = chkVersaoNaLista.Checked;
                credentialListHeader.HasMessage = chkMensagemNaLista.Checked;
                credentialListHeader.HasPassword = chkSenhaNaLista.Checked;
                credentialListHeader.HasShiftTableJourneyWorking = chkFaixaHorariaOuGrupoNaLista.Checked;
                credentialListHeader.HasInquiryField = chkConsultaNaLista.Checked;

                switch (cboLeitoraCartaoAtuacaoLista.SelectedIndex)
                {
                    case 0:
                        credentialListHeader.CardReaderApplies = ECardReader.None;
                        break;
                    case 1:
                        credentialListHeader.CardReaderApplies = ECardReader.CardReader1;
                        break;
                    case 2:
                        credentialListHeader.CardReaderApplies = ECardReader.CardReader2;
                        break;
                    case 3:
                        credentialListHeader.CardReaderApplies = ECardReader.Both;
                        break;
                }

                switch (cboLeitoraCartaoAtuacaoSenha.SelectedIndex)
                {
                    case 0:
                        credentialListHeader.CardReaderPasswordApplies = ECardReader.None;
                        break;
                    case 1:
                        credentialListHeader.CardReaderPasswordApplies = ECardReader.CardReader1;
                        break;
                    case 2:
                        credentialListHeader.CardReaderPasswordApplies = ECardReader.CardReader2;
                        break;
                    case 3:
                        credentialListHeader.CardReaderPasswordApplies = ECardReader.Both;
                        break;
                }

                switch (cboModoLiberacao.SelectedIndex)
                {
                    case 0:
                        credentialListHeader.BlackListBothGrantType = EGrantType.Undefined;
                        break;
                    case 1:
                        credentialListHeader.BlackListBothGrantType = EGrantType.NotGranted;
                        break;
                    case 2:
                        credentialListHeader.BlackListBothGrantType = EGrantType.Granted;
                        break;
                    case 3:
                        credentialListHeader.BlackListBothGrantType = EGrantType.GrantedCheckShiftTable;
                        break;
                    case 4:
                        credentialListHeader.BlackListBothGrantType = EGrantType.GrantedCheckJourneyWorking;
                        break;
                }

                credentialListHeader.ShiftTableJourneyWorkingId = txtCodigoFaixaJornada.Text.Length > 0 ? int.Parse(txtCodigoFaixaJornada.Text) : 0;
                credentialListHeader.InquiryFieldLength = txtTamanhoCampoConsulta.Text.Length > 0 ? int.Parse(txtTamanhoCampoConsulta.Text) : 0;

                this._watchComm.OpenConnection();

                // Insere os funcionários na lista do componente.
                foreach (DataRow dr in this._dtCredenciais.Rows)
                {
                    try
                    {
                        MicroPointIpCredential credential = new MicroPointIpCredential();

                        credential.HasCopyNumber = dr["PossuiVia"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["PossuiVia"].ToString());
                        credential.HasMessage = dr["PossuiMensagem"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["PossuiMensagem"].ToString());
                        credential.HasPassword = dr["PossuiSenha"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["PossuiSenha"].ToString());
                        credential.HasShiftTableJourneyWorking = dr["PossuiFaixaHorariaOuJornada"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["PossuiFaixaHorariaOuJornada"].ToString());
                        credential.HasInquiryField = dr["PossuiCampoConsulta"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["PossuiCampoConsulta"].ToString());

                        credential.UsesValidPeriod = dr["UsaPeriodo"].ToString().Equals(string.Empty) ? false : bool.Parse(dr["UsaPeriodo"].ToString());

                        credential.OutOfPeriodBehavior = dr["ComportamentoForaDePeriodo"].ToString().StartsWith("Negra") ? ECredentialOutOfPeriodBehavior.NotGrant : ECredentialOutOfPeriodBehavior.Grant;

                        credential.StartDayPeriod = dr["DiaInicioPeriodo"].ToString().Equals(string.Empty) ? 0 : int.Parse(dr["DiaInicioPeriodo"].ToString());
                        credential.StartMonthPeriod = dr["MesInicioPeriodo"].ToString().Equals(string.Empty) ? 0 : int.Parse(dr["MesInicioPeriodo"].ToString());
                        credential.EndDayPeriod = dr["DiaFimPeriodo"].ToString().Equals(string.Empty) ? 0 : int.Parse(dr["DiaFimPeriodo"].ToString());
                        credential.EndMonthPeriod = dr["MesFimPeriodo"].ToString().Equals(string.Empty) ? 0 : int.Parse(dr["MesFimPeriodo"].ToString());

                        credential.CredentialNumber = dr["NumeroCredencial"].ToString().Equals(string.Empty) ? 0 : long.Parse(dr["NumeroCredencial"].ToString());

                        credential.CopyNumber = dr["NumeroVia"].ToString().Equals(string.Empty) ? 0 : int.Parse(dr["NumeroVia"].ToString());
                        credential.MessageCode = dr["CodigoMensagem"].ToString().Equals(string.Empty) ? 0 : int.Parse(dr["CodigoMensagem"].ToString());

                        credential.Password = dr["Senha"].ToString();

                        credential.ShiftTableOrJourneyWorking = dr["FaixaHorariaOuJornada"].ToString().StartsWith("Faixa Horária") ? ECredentialShiftTableOrJourneyWorking.ShiftTable : ECredentialShiftTableOrJourneyWorking.JourneyWorking;
                        credential.ShiftTableJourneyWorkingId = dr["NumeroFaixaHorariaOuNumeroJornada"].ToString().Equals(string.Empty) ? 0 : int.Parse(dr["NumeroFaixaHorariaOuNumeroJornada"].ToString());

                        credential.InquiryField = dr["CampoConsulta"].ToString();

                        this._watchComm.SendCredential(credentialListHeader, int.Parse(nupNumeroDigitosCartao.Value.ToString()), credential);
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }

                ComandoRecepcionadoComSucesso();
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
                foreach (DataRow dr in this._dtCredenciais.Rows)
                {
                    try
                    {
                        this._watchComm.RemoveCredentialFromList(int.Parse(nupNumeroDigitosCartao.Value.ToString()), long.Parse(dr["NumeroCredencial"].ToString()));
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

            if (this._dtCredenciais.Rows.Count > 0)
                ComandoRecepcionadoComSucesso();
        }

        private void btnBuscarFuncionario_Click(object sender, EventArgs e)
        {
            MicroPointIpCredentialListHeader credentialListHeader = new MicroPointIpCredentialListHeader();
            switch (cboModoOperacao.SelectedIndex)
            {
                case 0:
                    credentialListHeader.OperationMode = EOperationMode.WhiteList;
                    break;
                case 1:
                    credentialListHeader.OperationMode = EOperationMode.BlackList;
                    break;
                case 2:
                    credentialListHeader.OperationMode = EOperationMode.Both;
                    break;
            }

            credentialListHeader.HasCopyNumber = chkVersaoNaLista.Checked;
            credentialListHeader.HasMessage = chkMensagemNaLista.Checked;
            credentialListHeader.HasPassword = chkSenhaNaLista.Checked;
            credentialListHeader.HasShiftTableJourneyWorking = chkFaixaHorariaOuGrupoNaLista.Checked;
            credentialListHeader.HasInquiryField = chkConsultaNaLista.Checked;

            switch (cboLeitoraCartaoAtuacaoLista.SelectedIndex)
            {
                case 0:
                    credentialListHeader.CardReaderApplies = ECardReader.None;
                    break;
                case 1:
                    credentialListHeader.CardReaderApplies = ECardReader.CardReader1;
                    break;
                case 2:
                    credentialListHeader.CardReaderApplies = ECardReader.CardReader2;
                    break;
                case 3:
                    credentialListHeader.CardReaderApplies = ECardReader.Both;
                    break;
            }

            switch (cboLeitoraCartaoAtuacaoSenha.SelectedIndex)
            {
                case 0:
                    credentialListHeader.CardReaderPasswordApplies = ECardReader.None;
                    break;
                case 1:
                    credentialListHeader.CardReaderPasswordApplies = ECardReader.CardReader1;
                    break;
                case 2:
                    credentialListHeader.CardReaderPasswordApplies = ECardReader.CardReader2;
                    break;
                case 3:
                    credentialListHeader.CardReaderPasswordApplies = ECardReader.Both;
                    break;
            }

            switch (cboModoLiberacao.SelectedIndex)
            {
                case 0:
                    credentialListHeader.BlackListBothGrantType = EGrantType.Undefined;
                    break;
                case 1:
                    credentialListHeader.BlackListBothGrantType = EGrantType.NotGranted;
                    break;
                case 2:
                    credentialListHeader.BlackListBothGrantType = EGrantType.Granted;
                    break;
                case 3:
                    credentialListHeader.BlackListBothGrantType = EGrantType.GrantedCheckShiftTable;
                    break;
                case 4:
                    credentialListHeader.BlackListBothGrantType = EGrantType.GrantedCheckJourneyWorking;
                    break;
            }

            credentialListHeader.ShiftTableJourneyWorkingId = txtCodigoFaixaJornada.Text.Length > 0 ? int.Parse(txtCodigoFaixaJornada.Text) : 0;
            credentialListHeader.InquiryFieldLength = txtTamanhoCampoConsulta.Text.Length > 0 ? int.Parse(txtTamanhoCampoConsulta.Text) : 0;

            InstanciaWatchComm();

            try
            {
                this._watchComm.OpenConnection();

                MicroPointIpCredential credential;
                credential = this._watchComm.GetCredentialFromList(credentialListHeader, int.Parse(nupNumeroDigitosCartao.Value.ToString()), long.Parse(txtBuscarFuncionario.Text));

                if (credential != null)
                {
                    DataRow dr = null;
                    foreach (DataRow datarow in this._dtCredenciais.Rows)
                    {
                        if (long.Parse(datarow["NumeroCredencial"].ToString()).Equals(credential.CredentialNumber))
                        {
                            dr = datarow;
                        }
                    }

                    if (dr == null)
                    {
                        dr = this._dtCredenciais.NewRow();
                        this._dtCredenciais.Rows.Add(dr);
                    }

                    dr["PossuiVia"] = credential.HasCopyNumber;
                    dr["PossuiMensagem"] = credential.HasMessage;
                    dr["PossuiSenha"] = credential.HasPassword;
                    dr["PossuiFaixaHorariaOuJornada"] = credential.HasShiftTableJourneyWorking;
                    dr["PossuiCampoConsulta"] = credential.HasInquiryField;

                    if (credential.UsesValidPeriod)
                    {
                        dr["UsaPeriodo"] = credential.UsesValidPeriod;

                        dr["ComportamentoForaDePeriodo"] = credential.OutOfPeriodBehavior == ECredentialOutOfPeriodBehavior.Grant ? "Branca" : "Negra";

                        dr["DiaInicioPeriodo"] = credential.StartDayPeriod;
                        dr["MesInicioPeriodo"] = credential.StartMonthPeriod;
                        dr["DiaFimPeriodo"] = credential.EndDayPeriod;
                        dr["MesFimPeriodo"] = credential.EndMonthPeriod;
                    }

                    dr["NumeroCredencial"] = credential.CredentialNumber;

                    if (credential.HasCopyNumber)
                    {
                        dr["NumeroVia"] = credential.CopyNumber;
                    }

                    if (credential.HasMessage)
                    {
                        dr["CodigoMensagem"] = credential.MessageCode;
                    }

                    if (credential.HasPassword)
                    {
                        dr["Senha"] = credential.Password;
                    }

                    if (credential.HasShiftTableJourneyWorking)
                    {
                        dr["FaixaHorariaOuJornada"] = credential.ShiftTableOrJourneyWorking == ECredentialShiftTableOrJourneyWorking.ShiftTable ? "Faixa Horária" : "Jornada";
                        dr["NumeroFaixaHorariaOuNumeroJornada"] = credential.ShiftTableJourneyWorkingId;
                    }

                    if (credential.HasInquiryField)
                    {
                        dr["CampoConsulta"] = credential.InquiryField;
                    }
                }
                else
                {
                    MessageBox.Show("Credencial não encontrada!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
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

        private void btnLimparGridFuncionarios_Click(object sender, EventArgs e)
        {
            _dtCredenciais = new dsSDK.dtCredenciaisDataTable();

            this.dtgListaFuncionarios.DataSource = _dtCredenciais;
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

                if (dtpPrimeiroIntervalo1.Checked && dtpPrimeiroIntervalo2.Checked)
                {
                    shift = new ShiftTable();
                    shift.Inicio = dtpPrimeiroIntervalo1.Value.ToString("HHmm");
                    shift.Fim = dtpPrimeiroIntervalo2.Value.ToString("HHmm");
                    shiftTableCollection.Add(shift);
                }

                if (dtpSegundoIntervalo1.Checked && dtpSegundoIntervalo2.Checked)
                {
                    shift = new ShiftTable();
                    shift.Inicio = dtpSegundoIntervalo1.Value.ToString("HHmm");
                    shift.Fim = dtpSegundoIntervalo2.Value.ToString("HHmm");
                    shiftTableCollection.Add(shift);
                }

                if (dtpTerceiroIntervalo1.Checked && dtpTerceiroIntervalo2.Checked)
                {
                    shift = new ShiftTable();
                    shift.Inicio = dtpTerceiroIntervalo1.Value.ToString("HHmm");
                    shift.Fim = dtpTerceiroIntervalo2.Value.ToString("HHmm");
                    shiftTableCollection.Add(shift);
                }

                if (dtpQuartoIntervalo1.Checked && dtpQuartoIntervalo2.Checked)
                {
                    shift = new ShiftTable();
                    shift.Inicio = dtpQuartoIntervalo1.Value.ToString("HHmm");
                    shift.Fim = dtpQuartoIntervalo2.Value.ToString("HHmm");
                    shiftTableCollection.Add(shift);
                }

                if (dtpQuintoIntervalo1.Checked && dtpQuintoIntervalo2.Checked)
                {
                    shift = new ShiftTable();
                    shift.Inicio = dtpQuintoIntervalo1.Value.ToString("HHmm");
                    shift.Fim = dtpQuintoIntervalo2.Value.ToString("HHmm");
                    shiftTableCollection.Add(shift);
                }

                if (dtpSextoIntervalo1.Checked && dtpSextoIntervalo2.Checked)
                {
                    shift = new ShiftTable();
                    shift.Inicio = dtpSextoIntervalo1.Value.ToString("HHmm");
                    shift.Fim = dtpSextoIntervalo2.Value.ToString("HHmm");
                    shiftTableCollection.Add(shift);
                }


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
                if (Int16.Parse(txtCodigoFaixaHoraria.Text) > 99)
                    txtCodigoFaixaHoraria.Text = "99";
            }
        }

        #endregion

        #region " ---------------------> Event Handles da Tab Jornadas.

        private void txtCodigoJornadaSemanal_Leave(object sender, EventArgs e)
        {
            if (!txtCodigoJornadaSemanal.Text.Equals(""))
            {
                if (Int16.Parse(txtCodigoJornadaSemanal.Text) > 32)
                    txtCodigoJornadaSemanal.Text = "32";
            }
        }

        private void Maximo99(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (!txt.Text.Equals(""))
            {
                if (Int16.Parse(txt.Text) > 99)
                    txt.Text = "99";
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