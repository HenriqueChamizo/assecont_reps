using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using org.cesar.dmplight.watchComm.api;
using org.cesar.dmplight.watchComm.business;
using org.cesar.dmplight.watchComm.impl.faceHanvon;
using ExemploFaceHanvon.DataSet;

namespace ExemploFace
{
    public partial class frmPrincipal : Form
    {
        #region " ---------------------> Atributos"

        private org.cesar.dmplight.watchComm.impl.WatchComm _watchComm;
        private dsSDKREP _dsREP;
        private dsSDKREP.dtMarcacaoDataTable _dtMarcacoes;
        private dsSDKREP.dtSupervisoresDataTable _dtSupervisores;
        private dsSDKREP.dtStatusDataTable _dtStatus;
        private dsSDKREP.dtFuncionariosDataTable _dtFuncionarios;        
        #endregion

        #region " ---------------------> Construtor"

        public frmPrincipal()
        {
            InitializeComponent();

            this._dsREP = new dsSDKREP();
            BindingDataTables();

            SetDefaults();
        }
        #endregion

        #region " ---------------------> Métodos Privados"

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
            this.dtgMarcacoes.DataSource = this._dtMarcacoes;
            this.dtgListaSupervisores.DataSource = this._dtSupervisores;
        }

        private void BindingDataTables()
        {
            this._dtFuncionarios = this._dsREP.dtFuncionarios;            
            this._dtMarcacoes = this._dsREP.dtMarcacao;
            this._dtSupervisores = this._dsREP.dtSupervisores;
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

            TCPComm tcpComm = new TCPComm(this.txtIPRelogio.Text, 9922, txtSenhaComunicacao.Text);

            this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(WatchProtocolType.FaceHanvonF810,
                                                                              tcpComm,
                                                                              1,
                                                                              key,
                                                                              WatchConnectionType.DisconnectedMode,
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
            String mensagem;

            if (ex is FaceHanvonException)
            {
                mensagem = ConverteFaceHanvonException((FaceHanvonException)ex);
            }
            else
            {
                mensagem = ex.Message;
            }

            MessageBox.Show("Ocorreu um erro durante a tentativa de envio do comando!" +
                            "\nErro: " + mensagem, Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private String ConverteFaceHanvonException(FaceHanvonException exception)
        {
            switch (exception.FaceHanvonError)
            {
                case FaceHanvonErrorType.none:
                    return "Nenhum";
                case FaceHanvonErrorType.unknown_command:
                    return "Comando desconhecido";
                case FaceHanvonErrorType.bad_parameter:
                    return "Parâmetro inválido";
                case FaceHanvonErrorType.device_busy:
                    return "Equipamento offline";
                case FaceHanvonErrorType.employee_overflow:
                    return "Excedeu a quantidade de funcionários";
                case FaceHanvonErrorType.unknown_id:
                    return "Função desconhecida";
                case FaceHanvonErrorType.select_socket_error:
                    return "Erro de comunicação Ethernet";
                default:
                    return "";
            }
        }

        private void ComandoRecepcionadoComSucesso()
        {
            MessageBox.Show("Comando recepcionado com sucesso pelo relógio!", Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region " ---------------------> Leitura e gravação do endereço IP informado"

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

        #region " ---------------------> Leitura e gravação das informações contidas nos grids"
        private void btnGravarXML_Click(object sender, EventArgs e)
        {
            this._dsREP.WriteXml(this.CaminhoEXE() + "\\DataFaceHanvon.xml");

            MessageBox.Show("XML gravado com sucesso!", Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnLerXML_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(this.CaminhoEXE() + "\\DataFaceHanvon.xml"))
            {
                this._dsREP = new dsSDKREP();
                this._dsREP.ReadXml(this.CaminhoEXE() + "\\DataFaceHanvon.xml");

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

        #region " ---------------------> Event Handles da Tab Geral"

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
                this._watchComm.SetDateTime(dtEnvio);

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnObterDataHora_Click(object sender, EventArgs e)
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
                this._watchComm.SetDateTime(dtEnvio);

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnHorarioVerao_Click(object sender, EventArgs e)
        {
            InstanciaWatchComm();

            try
            {
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
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Coleta"

        private void btnObterRegistros_Click(object sender, EventArgs e)
        {
            FaceHanvonLogRecord[] marcacoes;                        

            try
            {
                InstanciaWatchComm();

                if (rdbColetaCompleta.Checked)
                    marcacoes = this._watchComm.InquiryFaceHanvonLogRecords();
                else
                    marcacoes = this._watchComm.InquiryFaceHanvonLogRecords(dtpInicioColeta.Value, dtpFimColeta.Value);

                if (marcacoes != null)
                {
                    foreach (FaceHanvonLogRecord marcacao in marcacoes)
                    {
                        DataRow dr = this._dtMarcacoes.NewRow();                        

                        dr["ID"] = marcacao.EmployeeId;
                        dr["DataHoraMarcacao"] = marcacao.DateTimeMarkingPoint;
                        dr["CodigoTrabalho"] = marcacao.Workcode;
                        dr["Status"] = marcacao.Status;

                        foreach (FaceHanvonMarkingPointType tipoMarcacao in marcacao.MarkingPointType)
                        {
                            if (dr["TipoMarcacao"] == DBNull.Value)
                                dr["TipoMarcacao"] = (tipoMarcacao == FaceHanvonMarkingPointType.FromDoor ? "Acesso" : "Ponto");
                            else
                                dr["TipoMarcacao"] = dr["TipoMarcacao"] + " | " + (tipoMarcacao == FaceHanvonMarkingPointType.FromDoor ? "Acesso" : "Ponto");
                        }
                        
                        this._dtMarcacoes.Rows.Add(dr);
                    }
                }

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnExcluirRegistrosRelogio_Click(object sender, EventArgs e)
        {   
            try
            {
                InstanciaWatchComm();                

                this._watchComm.DeleteFaceHanvonLogRecords();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnLimparGrids_Click(object sender, EventArgs e)
        {
            _dtMarcacoes = new dsSDKREP.dtMarcacaoDataTable();

            this.dtgMarcacoes.DataSource = _dtMarcacoes;
        }

        private void rdbTipoColeta_CheckedChanged(object sender, EventArgs e)
        {
            dtpInicioColeta.Enabled = rdbColetaPeriodo.Checked;
            dtpFimColeta.Enabled = rdbColetaPeriodo.Checked;
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Status"

        private void btnObterStatus_Click(object sender, EventArgs e)
        {
            this._dtStatus = new dsSDKREP.dtStatusDataTable();
            this.dtgStatus.DataSource = this._dtStatus;

            InstanciaWatchComm();

            try
            {
                FaceHanvonStatus status = this._watchComm.GetFaceHanvonStatus();

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
        }

        private void CarregaGridStatus(FaceHanvonStatus status)
        {
            DataRow dr;

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Horário de Verão] - Habilitado";
            dr["Valor"] = status.DstEnabled == true ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Horário de Verão] - Início";
            dr["Valor"] = status.DstStart.Day.ToString().PadLeft(2, '0') + "/" + status.DstStart.Month.ToString().PadLeft(2, '0');
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Horário de Verão] - Fim";
            dr["Valor"] = status.DstEnd.Day.ToString().PadLeft(2, '0') + "/" + status.DstEnd.Month.ToString().PadLeft(2, '0');
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Versão de Firmware]";
            dr["Valor"] = status.FirmwareVersion;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Número de Série]";
            dr["Valor"] = status.SerialNumber;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Horário Atual]";
            dr["Valor"] = status.Date;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Registros] - Capacidade de Faces";
            dr["Valor"] = status.FaceCapacity;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Registros] - Quantidade de Faces";
            dr["Valor"] = status.FaceOccupation;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Registros] - Capacidade de Usuários";
            dr["Valor"] = status.UsersCapacity;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Registros] - Quantidade de Usuários";
            dr["Valor"] = status.UsersOccupation;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Registros] - Capacidade de Marcações";
            dr["Valor"] = status.RecordsCapacity;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Registros] - Quantidade de Marcações";
            dr["Valor"] = status.RecordsOccupation;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Registros] - Capacidade de Supervisores";
            dr["Valor"] = status.MasterCapacity;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Registros] - Quantidade de Supervisores";
            dr["Valor"] = status.MasterOccupation;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Configuração de Rede] - Endereço IP";
            dr["Valor"] = status.IpAddress;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Configuração de Rede] - Máscara de Rede";
            dr["Valor"] = status.Mask;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Configuração de Rede] - Gateway";
            dr["Valor"] = status.Gateway;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Configuração de Rede] - MAC Address";
            dr["Valor"] = status.MacAddress;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Volume]";
            dr["Valor"] = status.Volume;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Intervalo entre marcações] - Tempo";
            dr["Valor"] = status.AttendanceInterval + " minutos";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Alarme de Remoção] - Alarme Habilitado";
            dr["Valor"] = status.RemoveAlarmEnabled == true ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Agendamento] - Desligar equipamento habilitado";
            dr["Valor"] = status.ScheduleTurnOffEnabled == true ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Agendamento] - Horário p/ desligar o equipamento";
            dr["Valor"] = status.ScheduleTurnOffTime.ToShortTimeString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Agendamento] - Ligar equipamento habilitado";
            dr["Valor"] = status.ScheduleTurnOnEnabled == true ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Agendamento] - Horário p/ ligar o equipamento";
            dr["Valor"] = status.ScheduleTurnOnTime.ToShortTimeString();
            this._dtStatus.Rows.Add(dr);

            foreach (FaceHanvonBell faceHanvonBell in status.Bells)
            {
                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "[Alarme Configurável] - Número do alarme";
                dr["Valor"] = faceHanvonBell.BellNumber.ToString();
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "[Alarme Configurável] - Alarme Habilitado";
                dr["Valor"] = faceHanvonBell.IsEnable == true ? "Sim" : "Não";
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "[Alarme Configurável] - Código do Som";
                dr["Valor"] = faceHanvonBell.BellSound.ToString();
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "[Alarme Configurável] - Horário do alarme";
                dr["Valor"] = faceHanvonBell.AlarmTime.ToShortTimeString();
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "[Alarme Configurável] - Quantidade de Toques";
                dr["Valor"] = faceHanvonBell.BellTimes;
                this._dtStatus.Rows.Add(dr);
            }

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Alarme Falso Rejeite] - Alarme Habilitado";
            dr["Valor"] = status.FRAEnabled == true ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Alarme Falso Rejeite] - Tentativas p/ Acionamento";
            dr["Valor"] = status.FRATimes;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Alarme Falso Rejeite] - Tempo de Acionamento";
            dr["Valor"] = status.FRAPeriod.ToString() + " segundos";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Alarme de Sensor de Porta] - Alarme Habilitado";
            dr["Valor"] = status.DSAEnabled == true ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Alarme de Sensor de Porta] - Tempo p/ Acionamento";
            dr["Valor"] = status.DSADelay.ToString() + " segundos";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Alarme de Sensor de Porta] - Tempo de Acionamento";
            dr["Valor"] = status.DSAPeriod.ToString() + " segundos";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Area Bit";
            dr["Valor"] = status.WiegandOutAreaBit;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Area Value";
            dr["Valor"] = status.WiegandOutAreaValue;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Card Bit";
            dr["Valor"] = status.WiegandOutCardBit;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Content Type";
            dr["Valor"] = status.WiegandOutContentType;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Even Start";
            dr["Valor"] = status.WiegandOutEvenStart;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Even End";
            dr["Valor"] = status.WiegandOutEvenEnd;
            this._dtStatus.Rows.Add(dr);            

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Intervalo do Sinal";
            dr["Valor"] = status.WiegandOutInterval;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Odd Start";
            dr["Valor"] = status.WiegandOutOddStart;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Odd End";
            dr["Valor"] = status.WiegandOutOddEnd;
            this._dtStatus.Rows.Add(dr);            

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Oem Bit";
            dr["Valor"] = status.WiegandOutOemBit;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Oem Value";
            dr["Valor"] = status.WiegandOutOemValue;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Pattern";
            dr["Valor"] = status.WiegandOutPatternType;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Largura do Pulso do Sinal";
            dr["Valor"] = status.WiegandOutPulseWidth;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "[Saída Wiegand] - Site Code";
            dr["Valor"] = status.WiegandOutSiteCode;
            this._dtStatus.Rows.Add(dr);
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Funcionários"
        private void btnObterFuncionarios_Click_1(object sender, EventArgs e)
        {
            FaceHanvonEmployee[] funcionarios;

            try
            {
                this._dtFuncionarios.Rows.Clear();

                InstanciaWatchComm();

                if (rdbFuncionarioRespectivoID.Checked)
                {
                    funcionarios = new FaceHanvonEmployee[1];
                    funcionarios[0] = this._watchComm.GetFaceHanvonEmployee(Int32.Parse(txtIDFuncionarioEspecifico.Text));
                }
                else
                    funcionarios = this._watchComm.GetAllFaceHanvonEmployee();

                foreach (FaceHanvonEmployee funcionario in funcionarios)
                {
                    DataRow dr = this._dtFuncionarios.NewRow();

                    dr["ID"] = funcionario.Id;
                    dr["Nome"] = funcionario.Name;
                    dr["Cartao"] = funcionario.CardNumber;
                    dr["Face"] = funcionario.Face;

                    if (funcionario.EnterType == FaceHanvonEmployeeEnterType.Card)
                        dr["TipoAutenticacao"] = "Cartão";
                    else if (funcionario.EnterType == FaceHanvonEmployeeEnterType.CardAndFace)
                        dr["TipoAutenticacao"] = "Cartão e Face";
                    else if (funcionario.EnterType == FaceHanvonEmployeeEnterType.CardAndPhoto)
                        dr["TipoAutenticacao"] = "Cartão e Foto";
                    else
                        dr["TipoAutenticacao"] = "Face";

                    if (funcionario.PermissionType == FaceHanvonEmployeePermissionType.AccessOnly)
                        dr["TipoPermissao"] = "Acesso";
                    else if (funcionario.PermissionType == FaceHanvonEmployeePermissionType.AttendanceAndAccess)
                        dr["TipoPermissao"] = "Ponto e Acesso";
                    else if (funcionario.PermissionType == FaceHanvonEmployeePermissionType.AttendanceOnly)
                        dr["TipoPermissao"] = "Ponto";

                    this._dtFuncionarios.Rows.Add(dr);
                }

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnInclusaoFuncionarios_Click(object sender, EventArgs e)
        {
            List<FaceHanvonEmployee> funcionarios = new List<FaceHanvonEmployee>();

            try
            {
                InstanciaWatchComm();

                foreach (DataRow dr in this._dtFuncionarios)
                {
                    FaceHanvonEmployee funcionario = new FaceHanvonEmployee();

                    funcionario.Id = Int32.Parse(dr["ID"].ToString());
                    funcionario.Name = dr["Nome"].ToString();
                    funcionario.CardNumber = dr["Cartao"].ToString();
                    funcionario.Face = dr["Face"].ToString();

                    if (dr["TipoAutenticacao"].ToString().ToUpper() == "Cartão".ToUpper())
                        funcionario.EnterType = FaceHanvonEmployeeEnterType.Card;
                    else if (dr["TipoAutenticacao"].ToString().ToUpper() == "Cartão e Face".ToUpper())
                        funcionario.EnterType = FaceHanvonEmployeeEnterType.CardAndFace;
                    else if (dr["TipoAutenticacao"].ToString().ToUpper() == "Cartão e Foto".ToUpper())
                        funcionario.EnterType = FaceHanvonEmployeeEnterType.CardAndPhoto;
                    else if (dr["TipoAutenticacao"].ToString().ToUpper() == "Face".ToUpper())
                        funcionario.EnterType = FaceHanvonEmployeeEnterType.Face;
                    else
                    {
                        MessageBox.Show("Tipo de Autenticação Inválida." + Environment.NewLine +
                                        "Informe uma dentre os possíveis valores:" + Environment.NewLine +
                                        "Cartão" + Environment.NewLine +
                                        "Cartão e Face" + Environment.NewLine +
                                        "Cartão e Foto" + Environment.NewLine +
                                        "Face", "Envio de Funcionários", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }

                    if (dr["TipoPermissao"].ToString().ToUpper() == "Acesso".ToUpper())
                        funcionario.PermissionType = FaceHanvonEmployeePermissionType.AccessOnly;
                    else if (dr["TipoPermissao"].ToString().ToUpper() == "Ponto e Acesso".ToUpper())
                        funcionario.PermissionType = FaceHanvonEmployeePermissionType.AttendanceAndAccess;
                    else if (dr["TipoPermissao"].ToString().ToUpper() == "Ponto".ToUpper())
                        funcionario.PermissionType = FaceHanvonEmployeePermissionType.AttendanceOnly;
                    else
                    {
                        MessageBox.Show("Tipo de Permissão Inválida." + Environment.NewLine +
                                        "Informe uma dentre os possíveis valores:" + Environment.NewLine +
                                        "Acesso" + Environment.NewLine +
                                        "Ponto e Acesso" + Environment.NewLine +
                                        "Ponto", "Envio de Funcionários", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }

                    funcionarios.Add(funcionario);
                }

                this._watchComm.SetFaceHanvonEmployee(funcionarios.ToArray());
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);

                return;
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnExclusaoFuncionarios_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                foreach (DataRow dr in this._dtFuncionarios)
                    this._watchComm.DeleteFaceHanvonEmployee(Int32.Parse(dr["ID"].ToString()));
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);

                return;
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnExclusaoTotalFuncionarios_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.DeleteAllFaceHanvonEmployees();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);

                return;
            }

            ComandoRecepcionadoComSucesso();
        }

        private void rdbFuncionarioRespectivoID_CheckedChanged(object sender, EventArgs e)
        {
            txtIDFuncionarioEspecifico.Enabled = rdbFuncionarioRespectivoID.Checked;
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Geral"
        private void btnEnviarVolume_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.SetVolume(Int16.Parse(txtVolume.Text));

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnEnviarIntervaloEntreMarcacoes_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.SetAttendanceInterval(Int16.Parse(txtIntervaloEntreMarcacoes.Text));

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnEnviarTempoAcionamento_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.SetRelayTime(Int16.Parse(txtTempoAcionamentoRele.Text));

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnEnviarConfigRede_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.SetNetInfo(txtEndereçoIP.Text, txtMascara.Text, txtGateway.Text, txtSenhaComunic.Text);

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnEnviarAgendamento_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.SetOnOffSchedule(dtpHorarioLigarEquipamento.Checked, dtpHorarioDeskigarEquipamento.Checked,
                                                 dtpHorarioLigarEquipamento.Value, dtpHorarioDeskigarEquipamento.Value);

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnAberturaPorta_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.OpenDoor();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnFormatarDados_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.InitDevice();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnEnviarMACAddress_NumeroSerie_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.SetMAC_SN(txtMACAddress.Text, txtNumeroSerie.Text);

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Alarmnes"
        private void btnHabilitaAlarmeRemocao_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.SetRemoveAlarm(chkHabilitaAlarme.Checked);

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnEnviarAlarmeFalsoRejeite_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.SetFRAlarm(chkHabilitaAlarmeFalsoRejeite.Checked, Int16.Parse(txtTentativasAcionamentoFalsoRejeite.Text),
                                           Int16.Parse(txtTempoAcionamentoFalsoRejeite.Text));

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnEnviarAlarmeAberturaPorta_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.SetMagneAlarm(chkHabilitaAlarmeAberturaPorta.Checked, Int16.Parse(txtTempoParaAcionamentoAlarmeAberturaPorta.Text),
                                              Int16.Parse(txtTempoDeAcionamentoAlarmeAberturaPorta.Text));

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void chkHabilitaAlarme_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkHabilitaAlarme = (sender as CheckBox);
                String index = chkHabilitaAlarme.Name.Substring(chkHabilitaAlarme.Name.Length - 1, 1);
                TextBox txtCodigoSomAlarme = (this.Controls.Find("txtCodigoSomAlarme" + index, true)[0] as TextBox);
                TextBox txtQtdToquesAlarme = (this.Controls.Find("txtQtdToquesAlarme" + index, true)[0] as TextBox);
                DateTimePicker dtpAlarme = (this.Controls.Find("dtpAlarme" + index, true)[0] as DateTimePicker);

                txtCodigoSomAlarme.Enabled = chkHabilitaAlarme.Checked;
                txtQtdToquesAlarme.Enabled = chkHabilitaAlarme.Checked;
                dtpAlarme.Enabled = chkHabilitaAlarme.Checked;
            }
            catch { }
        }

        private void btnEnviarAlarmesProgramaveis_Click(object sender, EventArgs e)
        {
            List<FaceHanvonBell> faceHanvonBells = new List<FaceHanvonBell>();

            try
            {
                InstanciaWatchComm();

                for (Int16 index = 1; index <= 5; index++)
                {
                    FaceHanvonBell faceHanvonBell = new FaceHanvonBell();

                    CheckBox chkHabilitaAlarme = (this.Controls.Find("chkHabilitaAlarme" + index, true)[0] as CheckBox);

                    if (chkHabilitaAlarme.Checked)
                    {
                        TextBox txtCodigoSomAlarme = (this.Controls.Find("txtCodigoSomAlarme" + index, true)[0] as TextBox);
                        TextBox txtQtdToquesAlarme = (this.Controls.Find("txtQtdToquesAlarme" + index, true)[0] as TextBox);
                        DateTimePicker dtpAlarme = (this.Controls.Find("dtpAlarme" + index, true)[0] as DateTimePicker);

                        faceHanvonBell.BellSound = Int16.Parse(txtCodigoSomAlarme.Text);
                        faceHanvonBell.BellTimes = Int16.Parse(txtQtdToquesAlarme.Text);
                        faceHanvonBell.AlarmTime = dtpAlarme.Value;
                    }

                    faceHanvonBell.BellNumber = index;
                    faceHanvonBell.IsEnable = chkHabilitaAlarme.Checked;

                    faceHanvonBells.Add(faceHanvonBell);
                }

                this._watchComm.SetBell(faceHanvonBells.ToArray());

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnEnviarConfiguracaoSaidaWiegand_Click(object sender, EventArgs e)
        {
            FaceHanvonWiegandContentType contentType;
            FaceHanvonWiegandPatternType patternType;

            try
            {
                InstanciaWatchComm();

                if (rdbIDUsuario.Checked)
                    contentType = FaceHanvonWiegandContentType.UserID;
                else
                    contentType = FaceHanvonWiegandContentType.CardNumber;

                if (rdbW26.Checked)
                    patternType = FaceHanvonWiegandPatternType.w26;
                else if (rdbW26Site.Checked)
                    patternType = FaceHanvonWiegandPatternType.w26_site;
                else if (rdbW34.Checked)
                    patternType = FaceHanvonWiegandPatternType.w34;
                else if (rdbW34Site.Checked)
                    patternType = FaceHanvonWiegandPatternType.w34_site;
                else
                    patternType = FaceHanvonWiegandPatternType.customize;

                if (rdbCustomizado.Checked)
                {
                    this._watchComm.SetWiegandOut(Int16.Parse(txtAreaBit.Text), Int16.Parse(txtAreaValue.Text), Int16.Parse(txtCardBit.Text), contentType,
                                                  Int16.Parse(txtEventEnd.Text), Int16.Parse(txtEvenStart.Text), Int16.Parse(txtIntervalo.Text),
                                                  Int16.Parse(txtOddEnd.Text), Int16.Parse(txtOddStart.Text), Int16.Parse(txtOemBit.Text),
                                                  Int16.Parse(txtOemValue.Text), patternType, Int16.Parse(txtLarguraPulso.Text),
                                                  Int16.Parse(txtSiteCode.Text));
                }
                else
                {
                    this._watchComm.SetWiegandOut(0, 0, 0, contentType, 0, 0, Int16.Parse(txtIntervalo.Text), 0, 0, 0, 0, patternType,
                                                  Int16.Parse(txtLarguraPulso.Text), 0);
                }

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void padraoWiegand_CheckedChanged(object sender, EventArgs e)
        {
            grbFormatoCustomizadoWiegand.Enabled = rdbCustomizado.Checked;
        }
        #endregion                      

        #region " ---------------------> Event Handles da Tab Supervisores"
        private void rdbObterSupervisores_CheckedChanged(object sender, EventArgs e)
        {
            txtIDSupervisorEspecifico.Enabled = rdbObbterSupervisorEspecifico.Checked;
        }

        private void btnObterSupervisores_Click(object sender, EventArgs e)
        {
            FaceHanvonMaster[] supervisores;

            try
            {
                this._dtSupervisores.Rows.Clear();                

                InstanciaWatchComm();

                if (rdbObbterSupervisorEspecifico.Checked)
                {
                    supervisores = new FaceHanvonMaster[1];
                    supervisores[0] = this._watchComm.GetFaceHanvonMaster(Int32.Parse(txtIDSupervisorEspecifico.Text));
                }
                else
                    supervisores = this._watchComm.GetAllFaceHanvonMaster();

                foreach (FaceHanvonMaster supervisor in supervisores)
                {
                    DataRow dr = this._dtSupervisores.NewRow();

                    dr["ID"] = supervisor.Id;
                    dr["Nome"] = supervisor.Name;
                    dr["Senha"] = supervisor.Password;
                    dr["Cartao"] = supervisor.CardNumber;
                    dr["Face"] = supervisor.Face;

                    if (supervisor.EnterType == FaceHanvonMasterEnterType.CardAndFace)
                        dr["TipoAutenticacao"] = "Cartão e Face";
                    else if (supervisor.EnterType == FaceHanvonMasterEnterType.IdAndFace)
                        dr["TipoAutenticacao"] = "ID e Face";
                    else
                        dr["TipoAutenticacao"] = "ID e Senha";

                    this._dtSupervisores.Rows.Add(dr);
                }

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnEnviarInclusaoSupervisores_Click(object sender, EventArgs e)
        {
            List<FaceHanvonMaster> supervisores = new List<FaceHanvonMaster>();

            try
            {
                InstanciaWatchComm();

                foreach (DataRow dr in this._dtSupervisores)
                {
                    FaceHanvonMaster supervisor = new FaceHanvonMaster();

                    supervisor.Id = Int32.Parse(dr["ID"].ToString());
                    supervisor.Name = dr["Nome"].ToString();
                    supervisor.Password = dr["Senha"].ToString();
                    supervisor.CardNumber = dr["Cartao"].ToString();
                    supervisor.Face = dr["Face"].ToString();

                    if (dr["TipoAutenticacao"].ToString().ToUpper() == "Cartão e Face".ToUpper())
                        supervisor.EnterType = FaceHanvonMasterEnterType.CardAndFace;
                    else if (dr["TipoAutenticacao"].ToString().ToUpper() == "ID e Face".ToUpper())
                        supervisor.EnterType = FaceHanvonMasterEnterType.IdAndFace;
                    else if (dr["TipoAutenticacao"].ToString().ToUpper() == "ID e Senha".ToUpper())
                        supervisor.EnterType = FaceHanvonMasterEnterType.IdAndPassword;
                    else
                    {
                        MessageBox.Show("Tipo de Autenticação Inválida." + Environment.NewLine +
                                        "Informe uma dentre os possíveis valores:" + Environment.NewLine +
                                        "Cartão e Face" + Environment.NewLine +
                                        "ID e Face" + Environment.NewLine +
                                        "ID e Senha", "Envio de Supervisores", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }

                    supervisores.Add(supervisor);
                }

                this._watchComm.SetFaceHanvonMaster(supervisores.ToArray());
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);

                return;
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnEnviarExclusaoSupervisores_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                foreach (DataRow dr in this._dtSupervisores)
                    this._watchComm.DeleteFaceHanvonMaster(Int32.Parse(dr["ID"].ToString()));
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);

                return;
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnEnviarExclusaoTotalSupervisores_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.DeleteAllFaceHanvonMaster();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);

                return;
            }

            ComandoRecepcionadoComSucesso();
        }
        #endregion                
    }
}