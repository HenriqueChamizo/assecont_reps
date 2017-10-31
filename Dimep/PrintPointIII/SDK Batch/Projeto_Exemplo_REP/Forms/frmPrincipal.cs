using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ExemploREP.DataSet;
using org.cesar.dmplight.watchComm.business;
using org.cesar.dmplight.watchComm.impl.printpoint;
using org.cesar.dmplight.watchComm.impl;
using System.Net.Sockets;
using System.IO;

namespace ExemploREP.Forms
{
    #region " ---------------------> Enums <---------------------"
    public enum ETipoOrigemComando
    {
        Software,
        Relogio
    }

    public enum ETipoComando
    {
        PedeStatusEquipamento,
        PedeStatusImediatoEquipamento,
        ReposicionaPonteiroLeituraMRP,
        AtualizaDataHora,
        Configuracoes,
        ConfiguracoesParciais,
        InicioProgramacaoTotalListaFuncionarios,
        FimProgramacaoTotalListaFuncionarios,
        InsereFuncionarioLista,
        ExcluiFuncionarioLista,
        LimpezaListaCredenciais,
        InsereCredencialLista,
        ExcluiCredencialLista,
        EnviaTemplate,
        ExcluiTemplate,
        ExcluiTodosTemplates,
        ExcluiTodosTemplatesSemPIS,
        SolicitaTemplate,
        SolicitaRegistroMRP,
        SolicitaRegistroEvento,
        ConfirmaRecebimentoRegistrosMRP,
        ConfirmaRecebimentoRegistrosEvento,
        SolicitaListaFuncionarios,
        ConfirmaRecebimentoListaFuncionarios,
        ConfirmaRecebimentoTemplate,
        LimpaListaSupervisores,
        InsereSupervisorLista,
        EnvioMensagemDisplayRelogio,
        LimpezaMensagemDisplay,
        AlteracaoEmpregador,
        PedeEmpregador,
        AtualizacaoFirmare,
        SolicitaListaCredenciais,
        ConfirmaRecebimentoListaCredenciais,
        AtualizacaoUsuarioSenhaComunicacao,
        EnviarConfiguracaoConexaoClient,
        ObterConfiguracaoConexaoClient,
        SolicitaRegistrosEventoSistema,
        ConfirmaRecebimentoRegistrosEventoSistema
    }

    public enum ETipoHistorico
    {
        Erro,
        ConfirmacaoRecebimento,
        Envio
    }
    #endregion

    public partial class frmPrincipal : Form
    {
        #region " ---------------------> Atributos <---------------------"

        private org.cesar.dmplight.watchComm.impl.WatchComm _watchComm;
        private string _chavePublica_RSA = "";
        private string _expoente_RSA = "";
        private dsSDKREP _dsREP;
        private dsSDKREP.dtHistoricoComandosDataTable _dtHistoricoComandos;
        private dsSDKREP.dtMensagensDisplayDataTable _dtMensagensDisplay;
        private dsSDKREP.dtFuncionariosDataTable _dtFuncionarios;
        private dsSDKREP.dtFuncionariosCompletosDataTable _dtFuncionariosCompletos;
        private dsSDKREP.dtCredenciaisDataTable _dtCredenciais;
        private dsSDKREP.dtTemplatesRecebidoDataTable _dtTemplatesRecebidos;
        private dsSDKREP.dtTemplatesParaEnvioDataTable _dtTemplatesParaEnvio;
        private dsSDKREP.dtSupervisoresDataTable _dtSupervisores;
        private dsSDKREP.dtMRPAlteracaoEmpregadorDataTable _dtAlteracaoEmpregador;
        private dsSDKREP.dtEventosDataTable _dtEventos;
        private dsSDKREP.dtMRPMarcacaoDataTable _dtMarcacoes;
        private dsSDKREP.dtMRPEmpregadoDataTable _dtAlteracaoEmpregado;
        private dsSDKREP.dtMRPDataHoraDataTable _dtAlteracaoDataHora;
        private dsSDKREP.dtMRPEventosSensiveisDataTable _dtEventosSensiveis;
        private dsSDKREP.dtStatusDataTable _dtStatus;
        private dsSDKREP.dtEventosSistemaDataTable _dtEventosSistema;
        #endregion

        #region " ---------------------> Construtor <---------------------"
        public frmPrincipal()
        {
            InitializeComponent();

            this._dsREP = new dsSDKREP();
            BindingDataTables();

            SetDefaults();

            AddHandlers();
        }
        #endregion

        #region " ---------------------> Métodos Privados <---------------------"

        private void BindingDataTables()
        {
            this._dtHistoricoComandos = this._dsREP.dtHistoricoComandos;
            this._dtMensagensDisplay = this._dsREP.dtMensagensDisplay;
            this._dtFuncionarios = this._dsREP.dtFuncionarios;
            this._dtFuncionariosCompletos = this._dsREP.dtFuncionariosCompletos;
            this._dtCredenciais = this._dsREP.dtCredenciais;
            this._dtTemplatesRecebidos = this._dsREP.dtTemplatesRecebido;
            this._dtTemplatesParaEnvio = this._dsREP.dtTemplatesParaEnvio;
            this._dtSupervisores = this._dsREP.dtSupervisores;
            this._dtAlteracaoEmpregador = this._dsREP.dtMRPAlteracaoEmpregador;
            this._dtMarcacoes = this._dsREP.dtMRPMarcacao;
            this._dtAlteracaoEmpregado = this._dsREP.dtMRPEmpregado;
            this._dtAlteracaoDataHora = this._dsREP.dtMRPDataHora;
            this._dtEventosSensiveis = this._dsREP.dtMRPEventosSensiveis;
            this._dtStatus = this._dsREP.dtStatus;
            this._dtEventos = this._dsREP.dtEventos;
            this._dtEventosSistema = this._dsREP.dtEventosSistema;
        }

        private void BindingDataGrids()
        {
            this.dtgHistoricoComandos.DataSource = this._dtHistoricoComandos;
            this.dtgMensagensDisplay.DataSource = this._dtMensagensDisplay;
            this.dtgListaFuncionarios.DataSource = this._dtFuncionarios;
            this.dtgListaFuncionariosCompletos.DataSource = this._dtFuncionariosCompletos;
            this.dtgListaCredenciais.DataSource = this._dtCredenciais;
            this.dtgEnvioTemplates.DataSource = this._dtTemplatesParaEnvio;
            this.dtgRecebimentoTemplates.DataSource = this._dtTemplatesRecebidos;
            this.dtgSupervisores.DataSource = this._dtSupervisores;
            this.dtgAlteracoesEmpregador.DataSource = this._dtAlteracaoEmpregador;
            this.dtgMarcacoes.DataSource = this._dtMarcacoes;
            this.dtgAlteracaoEmpregado.DataSource = this._dtAlteracaoEmpregado;
            this.dtgAlteracaoDataHora.DataSource = this._dtAlteracaoDataHora;
            this.dtgEventosSensiveis.DataSource = this._dtEventosSensiveis;
            this.dtgEventos.DataSource = this._dtEventos;
            this.dtgEventosSistema.DataSource = this._dtEventosSistema;
        }

        private void SetDefaults()
        {
            this.dtpData.Value = DateTime.Now;
            this.dtpHora.Value = DateTime.Now;
            this.rdbDataHoraAtual.Checked = true;
            this.dtpDataReposicionamentoMRP.Value = DateTime.Now;

            DataRow drMensagemDisplay = this._dtMensagensDisplay.NewRow();
            drMensagemDisplay["Codigo"] = 1;
            drMensagemDisplay["Mensagem"] = "Teste";
            this._dtMensagensDisplay.Rows.Add(drMensagemDisplay);

            BindingDataGrids();

            this.cboTipoPessoa.SelectedIndex = 0;
        }

        private void AddHistoricoComando(ETipoOrigemComando tipoOrigemComando,
                                         ETipoComando tipoComando,
                                         ETipoHistorico tipoHistorico,
                                         String descricao)
        {
            DataRow dr = this._dtHistoricoComandos.NewRow();

            dr["DataHora"] = DateTime.Now;

            if (tipoOrigemComando == ETipoOrigemComando.Relogio)
                dr["Comando"] = "<-- ";
            else if (tipoOrigemComando == ETipoOrigemComando.Software)
                dr["Comando"] = "--> ";

            if (tipoHistorico == ETipoHistorico.ConfirmacaoRecebimento)
                dr["Comando"] += "OK ";
            else if (tipoHistorico == ETipoHistorico.Erro)
                dr["Comando"] += "ERRO ";

            dr["Comando"] += DescricaoComando(tipoComando);

            dr["Descricao"] = descricao;

            this._dtHistoricoComandos.Rows.Add(dr);
        }

        private String DescricaoComando(ETipoComando comando)
        {
            switch (comando)
            {
                case ETipoComando.AlteracaoEmpregador:
                    return "Alteração de Empregador";
                case ETipoComando.AtualizacaoFirmare:
                    return "Atualização de Firmware";
                case ETipoComando.AtualizaDataHora:
                    return "Atualização de Data e Hora";
                case ETipoComando.Configuracoes:
                    return "Configurações";
                case ETipoComando.ConfiguracoesParciais:
                    return "Configurações Parciais";
                case ETipoComando.ConfirmaRecebimentoRegistrosMRP:
                    return "Confirmação Recebimento Registro da MRP";
                case ETipoComando.ConfirmaRecebimentoTemplate:
                    return "Confirmação Recebimento Template";
                case ETipoComando.ConfirmaRecebimentoRegistrosEvento:
                    return "Confirmação Recebimento Registro Evento";
                case ETipoComando.EnviaTemplate:
                    return "Envia Template";
                case ETipoComando.ExcluiTemplate:
                    return "Exclusão de Template.";
                case ETipoComando.ExcluiTodosTemplates:
                    return "Exclusão de Todos os Templates.";
                case ETipoComando.ExcluiTodosTemplatesSemPIS:
                    return "Exclusão de Todos os Templates sem PIS.";
                case ETipoComando.EnvioMensagemDisplayRelogio:
                    return "Envio Mensagem Display";
                case ETipoComando.InicioProgramacaoTotalListaFuncionarios:
                    return "Início Programação Total de Funcionários";
                case ETipoComando.FimProgramacaoTotalListaFuncionarios:
                    return "Fim Programação Total de Funcionários";
                case ETipoComando.InsereCredencialLista:
                    return "Insere Credencial";
                case ETipoComando.ExcluiCredencialLista:
                    return "Exclusão de Credencial";
                case ETipoComando.InsereFuncionarioLista:
                    return "Insere Funcionário";
                case ETipoComando.ExcluiFuncionarioLista:
                    return "Exclusão de Funcionário";
                case ETipoComando.InsereSupervisorLista:
                    return "Insere Supervisor";
                case ETipoComando.LimpaListaSupervisores:
                    return "Limpa Lista Supervisores";
                case ETipoComando.LimpezaListaCredenciais:
                    return "Limpa Lista Credenciais";
                case ETipoComando.LimpezaMensagemDisplay:
                    return "Limpa Mensagens Display";
                case ETipoComando.PedeEmpregador:
                    return "Solicita Empregador";
                case ETipoComando.PedeStatusEquipamento:
                    return "Solicita Status";
                case ETipoComando.PedeStatusImediatoEquipamento:
                    return "Solicita Status Imediato";
                case ETipoComando.ReposicionaPonteiroLeituraMRP:
                    return "Reposiciona Ponteiro Leitura MRP";
                case ETipoComando.SolicitaRegistroMRP:
                    return "Solicita Registros MRP";
                case ETipoComando.SolicitaTemplate:
                    return "Solicita Template";
                case ETipoComando.SolicitaListaFuncionarios:
                    return "Solicita a lista de funcionários";
                case ETipoComando.SolicitaListaCredenciais:
                    return "Solicita a lista de credenciais";
                case ETipoComando.SolicitaRegistroEvento:
                    return "Solicita Registros de Evento";
                case ETipoComando.ConfirmaRecebimentoListaFuncionarios:
                    return "Confirmação de recebimento da lista de funcionários";
                case ETipoComando.ConfirmaRecebimentoListaCredenciais:
                    return "Confirmação de recebimento da lista de credenciais";
                case ETipoComando.AtualizacaoUsuarioSenhaComunicacao:
                    return "Atualização de usuário e senha da comunicação";
                case ETipoComando.SolicitaRegistrosEventoSistema:
                    return "Solicita Registros de Evento de Sistema";
                case ETipoComando.ConfirmaRecebimentoRegistrosEventoSistema:
                    return "Confirmação Recebimento Registros Evento de Sistema";
            }

            return "";
        }

        private String CaminhoEXE()
        {
            String nomeEXE = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath).InternalName;
            String retorno = System.Windows.Forms.Application.ExecutablePath.ToUpper().Replace(nomeEXE.ToUpper(), "");

            return retorno;
        }

        private string AbreArquivoChaveComunicacao()
        {
            string key = "";

            if (System.IO.File.Exists(this.CaminhoEXE() + "\\key.txt"))
            {
                System.IO.StreamReader streamReader = System.IO.File.OpenText(this.CaminhoEXE() + "\\key.txt");
                key = streamReader.ReadLine();

                streamReader.Close();
            }

            return key;
        }

        private void AbreArquivoChaveRSA()
        {
            string[] arquivos = System.IO.Directory.GetFiles(this.CaminhoEXE(), "CHAVE*");

            if (arquivos != null && arquivos.Length > 0)
            {
                System.IO.StreamReader streamReader = System.IO.File.OpenText(arquivos[0]);

                _chavePublica_RSA = streamReader.ReadLine();
                _expoente_RSA = streamReader.ReadLine();

                streamReader.Close();
            }
        }

        private bool InstanciaWatchComm()
        {
            try
            {
                string key = AbreArquivoChaveComunicacao();

                AbreArquivoChaveRSA();

                if (rdbClient.Checked)
                {
                    org.cesar.dmplight.watchComm.api.TCPComm tcpComm =
                        new org.cesar.dmplight.watchComm.api.TCPComm(this.txtIPRelogio.Text, 3000);

                    tcpComm.SetTimeOut(15000);

                    if (cboModelo.SelectedIndex == 0)
                    {
                        this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.PrintPoint,
                                                                                          tcpComm,
                                                                                          1,
                                                                                          key,
                                                                                          org.cesar.dmplight.watchComm.api.WatchConnectionType.ConnectedMode,
                                                                                          this.txtVersaoFirmware.Text);
                    }
                    else if (cboModelo.SelectedIndex == 1)
                    {
                        this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.MiniPrint,
                                                                                          tcpComm,
                                                                                          1,
                                                                                          key,
                                                                                          org.cesar.dmplight.watchComm.api.WatchConnectionType.ConnectedMode,
                                                                                          this.txtVersaoFirmware.Text);
                    }
                    else
                    {
                        if (txtUsuarioComunicacao.Text.Trim().Length.Equals(0) || txtSenhaComunicacao.Text.Trim().Length.Equals(0))
                        {
                            throw new Exception("Usuário e Senha da comunicação devem ser informados.");
                        }

                        this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.PrintPointIII,
                                                                                          tcpComm,
                                                                                          1,
                                                                                          key,
                                                                                          org.cesar.dmplight.watchComm.api.WatchConnectionType.ConnectedMode,
                                                                                          this.txtVersaoFirmware.Text,
                                                                                          _chavePublica_RSA,
                                                                                          _expoente_RSA,
                                                                                          txtUsuarioComunicacao.Text,
                                                                                          txtSenhaComunicacao.Text);
                    }

                    this._watchComm.MonitoringSendReceiveMessageEvent += new WatchComm.MonitoringSendReceiveMessage(MonitoringSendReceiveMessageListenerClient);
                }
                else
                {
                    if (_watchConnection == null)
                    {
                        MessageBox.Show("Não existe conexão ativa.", "SDK REP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (this._watchComm == null)
                        {
                            if (cboModelo.SelectedIndex == 0)
                            {
                                this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.PrintPoint,
                                                                                                  _watchConnection,
                                                                                                  1,
                                                                                                  key,
                                                                                                  org.cesar.dmplight.watchComm.api.WatchConnectionType.ConnectedMode,
                                                                                                  this.txtVersaoFirmware.Text);
                            }
                            else if (cboModelo.SelectedIndex == 1)
                            {
                                this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.MiniPrint,
                                                                                                  _watchConnection,
                                                                                                  1,
                                                                                                  key,
                                                                                                  org.cesar.dmplight.watchComm.api.WatchConnectionType.ConnectedMode,
                                                                                                  this.txtVersaoFirmware.Text);
                            }
                            else
                            {
                                if (txtUsuarioComunicacao.Text.Trim().Length.Equals(0) || txtSenhaComunicacao.Text.Trim().Length.Equals(0))
                                {
                                    throw new Exception("Usuário e Senha da comunicação devem ser informados.");
                                }

                                this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.PrintPointIII,
                                                                                                  _watchConnection,
                                                                                                  1,
                                                                                                  key,
                                                                                                  org.cesar.dmplight.watchComm.api.WatchConnectionType.ConnectedMode,
                                                                                                  this.txtVersaoFirmware.Text,
                                                                                                  _chavePublica_RSA,
                                                                                                  _expoente_RSA,
                                                                                                  txtUsuarioComunicacao.Text,
                                                                                                  txtSenhaComunicacao.Text);
                            }

                            this._watchComm.MonitoringSendReceiveMessageEvent += new WatchComm.MonitoringSendReceiveMessage(MonitoringSendReceiveMessageListenerServer);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
                return false;
            }
        }

        private void ComandoRecepcionadoComSucesso()
        {
            MessageBox.Show("Comando recepcionado com sucesso pelo relógio!", Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ErroDuranteRecepcaoDoComando(Exception ex)
        {
            if (ex.GetType().Equals(typeof(InvalidMessageException)))
            {
                InvalidMessageException invalidMessage = (InvalidMessageException)ex;

                String mensagemErro = ConverteEnumErroPrintPoint((ex as InvalidMessageException).InvalidMessageType);

                if (invalidMessage.FieldNumber > 0 || invalidMessage.ParameterNumber > 0)
                {
                    mensagemErro += "\r\n";
                    mensagemErro += "Campo: " + invalidMessage.FieldNumber;
                    mensagemErro += "\r\n";
                    mensagemErro += "Parâmetro: " + invalidMessage.ParameterNumber;
                }

                MessageBox.Show("Ocorreu um erro durante a tentativa de envio do comando!" +
                                "\nErro: " + mensagemErro,
                                Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Ocorreu um erro durante a tentativa de envio do comando!" +
                                "\nErro: " + ex.Message, Application.ProductName,
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (ex.GetType().Equals(typeof(org.cesar.dmplight.watchComm.api.DeviceConnectionException)) && rdbServer.Checked == true)
            {
                if (_watchListener != null)
                {
                    try
                    {
                        _watchListener.watchConnectEvent -= _watchListener_watchConnectEvent;
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

                        _watchListener = new org.cesar.dmplight.watchComm.impl.WatchListener(Int32.Parse(txtPorta.Text), Int16.Parse(txtIdRelogio.Text));
                        _watchListener.watchConnectEvent += new org.cesar.dmplight.watchComm.impl.WatchListener.DelWatchConnect(_watchListener_watchConnectEvent);
                        _watchListener.Listening();

                        AtualizaStatusModoServer(StatusModoServer.Aguardando);
                    }
                    catch (Exception ex2)
                    {
                        MessageBox.Show("Não foi possível inicializar o listener. \n" +
                                        "Ocorreu o seguinte erro: " + ex2.Message, "Erro",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private String ConverteEnumErroPrintPoint(InvalidMessageType invalidMessageType)
        {
            switch (invalidMessageType)
            {
                case InvalidMessageType.BiometricModuleReturnedError:
                    return "O módulo biométrico retornou erro.";
                case InvalidMessageType.CapacityExceededOfBiometricModule:
                    return "A capacidade do módulo biométrico foi excedida.";
                case InvalidMessageType.CardWithMoreDigitsThanProgrammed:
                    return "Cartão com mais dígitos que o programado.";
                case InvalidMessageType.DuplicateFingerprint:
                    return "Digital duplicada.";
                case InvalidMessageType.EquipmentAlreadyActivated:
                    return "O equipamento já está ativado.";
                case InvalidMessageType.EquipmentNotViolated:
                    return "O equipamento não está violado.";
                case InvalidMessageType.EquipmentWasNotActivated:
                    return "O equipamento não foi ativado.";
                case InvalidMessageType.EquipmentWithoutEmployer:
                    return "O equipamento está sem empregador.";
                case InvalidMessageType.FailureBiometricModule:
                    return "Falha no módulo biométrico.";
                case InvalidMessageType.FieldNotInformed:
                    return "Campo não fornecido.";
                case InvalidMessageType.FunctionNotPermitted:
                    return "Função não permitida.";
                case InvalidMessageType.InvalidData:
                    return "Dados inválidos.";
                case InvalidMessageType.InvalidFingerprint:
                    return "Digital inválida.";
                case InvalidMessageType.InvalidPassword:
                    return "Contrasenha inválida.";
                case InvalidMessageType.InvalidProtocol:
                    return "Protocolo inválido.";
                case InvalidMessageType.MRPReturnedError:
                    return "A MRP retornou erro.";
                case InvalidMessageType.OutOfMemory:
                    return "Memória cheia.";
                case InvalidMessageType.UnknownFunction:
                    return "Função desconhecida.";
                case InvalidMessageType.WithoutDataToBeTransmitted:
                    return "Não existem dados para serem transmitidos.";
                case InvalidMessageType.ProcessingCleaningFingerprints:
                    return "Processando limpeza de digitais.";
                case InvalidMessageType.ProcessingCleaningCredentials:
                    return "Processando limpeza de credenciais.";
                case InvalidMessageType.WaitingDateAndTime:
                    return "Aguardando atualização de Data e Hora.";
                case InvalidMessageType.ListSizeUninitialized:
                    return "Tamanho da Lista não Inicializada.";
                case InvalidMessageType.PPIII_UnknownFunction:
                    return "Função desconhecida.";
                case InvalidMessageType.PPIII_InvalidData:
                    return "Dados inválidos.";
                case InvalidMessageType.PPIII_OutOfMemory:
                    return "Memória cheia.";
                case InvalidMessageType.PPIII_REPBusy:
                    return "REP Ocupado.";
                case InvalidMessageType.PPIII_CardWithManyDigits:
                    return "Cartão com muitos dígitos.";
                case InvalidMessageType.PPIII_ErrorAllocatingMemory:
                    return "Erro de alocação de memória.";
                case InvalidMessageType.PPIII_InvalidBiometric:
                    return "Biometria inválida.";
                case InvalidMessageType.PPIII_BiometricsDuplicated:
                    return "Biometria duplicada.";
                case InvalidMessageType.PPIII_FieldNotAvaiable:
                    return "Campo não disponível.";
                case InvalidMessageType.PPIII_InvalidProtocol:
                    return "Protocolo inválido.";
                case InvalidMessageType.PPIII_EquipmentWithoutActivation:
                    return "Equipamento sem ativação.";
                case InvalidMessageType.PPIII_EquipmentAlreadyActivated:
                    return "Equipamento já ativado.";
                case InvalidMessageType.PPIII_FunctionNotPermitted:
                    return "Função não permitida.";
                case InvalidMessageType.PPIII_EquipmentWithoutEmployer:
                    return "Equipamento sem empregador.";
                case InvalidMessageType.PPIII_EquipmentNotViolated:
                    return "Equipamento não violado.";
                case InvalidMessageType.PPIII_InvalidPassword:
                    return "Erro de contrasenha.";
                case InvalidMessageType.PPIII_FullBiometricModule:
                    return "Módulo biométrico cheio.";
                case InvalidMessageType.PPIII_BiometricModuleError:
                    return "Erro no módulo biométrico.";
                case InvalidMessageType.PPIII_MRPError:
                    return "Erro na MRP.";
                case InvalidMessageType.PPIII_WithoutData:
                    return "Sem dados.";
                case InvalidMessageType.PPIII_BiometricCleaningProcessRunning:
                    return "Processo de apagamento de biometria em execução.";
                case InvalidMessageType.PPIII_CredentialsCleaningProcessRunning:
                    return "Processo de apagamento de credenciais em execução.";
                case InvalidMessageType.PPIII_SDCardError:
                    return "Falha no SDCard.";
                case InvalidMessageType.PPIII_WaitingDateTimeChange:
                    return "Aguardando alteração de data e hora.";
                case InvalidMessageType.PPIII_SerialNumberNotInitialized:
                    return "Número de fabricação não inicializado.";
                case InvalidMessageType.PPIII_MRPExchanged:
                    return "MRP trocada.";
                case InvalidMessageType.PPIII_RandomNumberNotReceived:
                    return "Número randômico não recebido.";
                case InvalidMessageType.PPIII_ErrorReadingSerialNumber:
                    return "Falha ao ler o número de fabricação.";
                case InvalidMessageType.PPIII_ViolatedCabinet:
                    return "Gabinete violado.";
                case InvalidMessageType.PPIII_MRP_WithoutData:
                    return "MRP - Sem dados";
                case InvalidMessageType.PPIII_MRP_Error:
                    return "MRP - Erro";
                case InvalidMessageType.PPIII_MRP_SerialNumberNotConfigured:
                    return "MRP - Número de fabricação não configurado.";
                case InvalidMessageType.PPIII_MRP_EmployerNotConfigured:
                    return "MRP - Empregador não configurado.";
                case InvalidMessageType.PPIII_MRP_SerialNumberAlreadyConfigured:
                    return "MRP - Número de fabricação já configurado.";
                case InvalidMessageType.PPIII_MRP_InvalidData:
                    return "MRP - Dados inválidos.";
                case InvalidMessageType.PPIII_MRP_HeaderNotFound:
                    return "MRP - Leitor não encontrado.";
                case InvalidMessageType.PPIII_MRP_WithoutReg:
                    return "MRP - Sem registros.";
                case InvalidMessageType.PPIII_MRP_FullMemory:
                    return "MRP - Memória cheia.";
                case InvalidMessageType.PPIII_MRP_WriteError:
                    return "MRP - Erro de escrita.";
                case InvalidMessageType.PPIII_MRP_ReadyError:
                    return "MRP - Erro de leitura.";
                case InvalidMessageType.PPIII_MRP_NotFound:
                    return "MRP - Não encontrado.";
                case InvalidMessageType.PPIII_MRP_InvalidParameter:
                    return "MRP - Parâmetro inválido.";
                case InvalidMessageType.PPIII_MRP_RTCError:
                    return "MRP - Erro no RTC.";
                case InvalidMessageType.PPIII_MRP_SDCardError:
                    return "MRP - Erro no SDCard.";
                case InvalidMessageType.PPIII_MRP_GenericError:
                    return "MRP - Erro genérico.";
                case InvalidMessageType.PPIII_REP_Initializing:
                    return "REP - Inicializando.";
                case InvalidMessageType.PPIII_REP_WithoutData:
                    return "REP - Sem dados.";
                case InvalidMessageType.PPIII_REP_Error:
                    return "REP - Erro.";
                case InvalidMessageType.PPIII_REP_SerialNumberNotConfigured:
                    return "REP - Número de fabricação não configurado.";
                case InvalidMessageType.PPIII_REP_EmployerNotConfigured:
                    return "REP - Empregador não configurado.";
                case InvalidMessageType.PPIII_REP_InvalidData:
                    return "REP - Dados inválidos.";
                case InvalidMessageType.PPIII_REP_FullMemory:
                    return "REP - Memória cheia.";
                case InvalidMessageType.PPIII_REP_ReadyError:
                    return "REP - Erro de leitura.";
                case InvalidMessageType.PPIII_REP_NotFound:
                    return "REP - Não encontrado.";
                case InvalidMessageType.PPIII_REP_WithoutChanges:
                    return "REP - Sem alterações.";
                case InvalidMessageType.PPIII_REP_SameEmployer:
                    return "REP - Mesmo empregador.";
                case InvalidMessageType.PPIII_REP_EmployerNotFound:
                    return "REP - Empregador não encontrado.";
                case InvalidMessageType.PPIII_REP_CommunicationInStandBy:
                    return "REP - Comunicação em espera.";
                case InvalidMessageType.PPIII_REP_CommunicationError:
                    return "REP - Erro de comunicação.";
                case InvalidMessageType.PPIII_REP_InProcess:
                    return "REP - Em processo.";
                case InvalidMessageType.PPIII_REP_GenericError:
                    return "REP - Erro genérico.";
                case InvalidMessageType.PPIII_AESKeyFailed:
                    return "Chave AES falhou";
                case InvalidMessageType.PPIII_LoginNotAutorized:
                    return "Login não autorizado";
                case InvalidMessageType.PPIII_SaveLoginFailed:
                    return "Salvar login falhou";
                case InvalidMessageType.PPIII_NoneUserLogged:
                    return "Nenhum usuário logado";
                case InvalidMessageType.PPIII_InvalidEquipamentModel:
                    return "Modelo do equipamento inválido";
                case InvalidMessageType.PPIII_AESInvalidPassword:
                    return "Senha inválida";
                case InvalidMessageType.PPIII_180DegreesBiometricRecognitionNotPermitted:
                    return "Reconhecimento biométrico 180° não permitido";
                case InvalidMessageType.PPIII_PisNotRegistered:
                    return "Pis não cadastrado";
                case InvalidMessageType.PPIII_MicroSd_NotWorking:
                    return "MICROSD - Não está funcionando";
                case InvalidMessageType.PPIII_MicroSd_ADDRMemoryInvalid:
                    return "MICROSD - Memóraia ADDR inválida";
                case InvalidMessageType.PPIII_MicroSd_MemoryOverflow:
                    return "MICROSD - Estouro de memória";
                case InvalidMessageType.PPIII_MicroSd_ErasePageFailed:
                    return "MICROSD - Falha ao apagar página";
                case InvalidMessageType.PPIII_MicroSd_ReadingFailure:
                    return "MICROSD - Falha na leitura";
                case InvalidMessageType.PPIII_MicroSd_WritingFailure:
                    return "MICROSD - Falha na escrita";
                case InvalidMessageType.PPIII_MicroSd_ReadingPageFailure:
                    return "MICROSD - Falha ao ler página";
                case InvalidMessageType.PPIII_MicroSd_ComparisonFailure:
                    return "MICROSD - Falha na comparação";
                case InvalidMessageType.PPIII_MicroSd_MovingPageFailure:
                    return "MICROSD - Falha ao mover página";
                case InvalidMessageType.PPIII_MicroSd_InitializationFailure:
                    return "MICROSD - Falha na inicialização";
                case InvalidMessageType.PPIII_MicroSd_ReadingConfigurationFailure:
                    return "MICROSD - Falha na leitura de configuração";
                case InvalidMessageType.PPIII_MicroSd_BadSector:
                    return "MICROSD - Setor danificado";
                case InvalidMessageType.PPIII_InvalidEventSequencialNumber:
                    return "Número sequencial do evento inválido";
                case InvalidMessageType.PPIII_SensorCaseUninitialized:
                    return "Sensor de Gabinete não inicializado";
                case InvalidMessageType.PPIII_ClientConnectionActive:
                    return "Conexão client ativa";
                case InvalidMessageType.PPIII_FirmwareDownload_InvalidBinaryKey:
                    return "Chave do binário inválida";
                case InvalidMessageType.PPIII_FirmwareDownload_ChecksumErrorBinaryHeader:
                    return "Erro no checksum do cabeçalho do binário";
                case InvalidMessageType.PPIII_FirmwareDownload_ProjectOrBoardNameLimitExceeded:
                    return "Nome do projeto ou nome da placa excedeu o limite";
                case InvalidMessageType.PPIII_FirmwareDownload_InvalidBoardName:
                    return "Nome da placa inválido";
                case InvalidMessageType.PPIII_FirmwareDownload_BinarySizeLimitExceeded:
                    return "Tamanho do binário excedeu o limite";
                case InvalidMessageType.PPIII_FirmwareDownload_ErrorBinarySignature:
                    return "Erro na assinatura do binário";
                default:
                    return "Erro desconhecido - Número: " + (Int32)invalidMessageType;
            }
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

        private void AddHandlers()
        {
            this.txtVetor1.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtVerificador1.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtVetor2.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtVerificador2.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtCodigoPersonalizacao.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtSetor.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtBloco.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtOffSet.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtQtdDigitosMatriculaSmart.KeyPress += new KeyPressEventHandler(ApenasNumeros);
            this.txtDigitosExibicaoCredencial.KeyPress += new KeyPressEventHandler(ApenasNumeros);
        }

        private void cboModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboModelo.SelectedIndex == 1) // Mini REP.
            {
                txtNomeEmpresa1.MaxLength = txtNomeEmpresa2.MaxLength = txtNomeEmpresa3.MaxLength = txtNomeEmpresa4.MaxLength = 36;
                txtNomeEmpresa5.MaxLength = 6;

                txtEnderecoEmpresa1.MaxLength = txtEnderecoEmpresa2.MaxLength = 36;
                txtEnderecoEmpresa3.MaxLength = 28;
            }
            else
            {
                txtNomeEmpresa1.MaxLength = txtNomeEmpresa2.MaxLength = txtNomeEmpresa3.MaxLength = txtNomeEmpresa4.MaxLength = 35;
                txtNomeEmpresa5.MaxLength = 10;

                txtEnderecoEmpresa1.MaxLength = txtEnderecoEmpresa2.MaxLength = 35;
                txtEnderecoEmpresa3.MaxLength = 30;
            }

            // Controles visíveis apenas para REP III.
            chkEventosSensiveis.Visible = cboModelo.SelectedIndex == 2;
            btnExcluiCredenciaisAssociadasPIS.Visible = cboModelo.SelectedIndex == 2;
            btnExcluiTodosFuncionarios.Visible = cboModelo.SelectedIndex == 2;
            txtPISExclusaoCredenciais.Visible = cboModelo.SelectedIndex == 2;
            lblPISExclusaoCredenciais.Visible = cboModelo.SelectedIndex == 2;
            grbBuscaCredenciais.Visible = cboModelo.SelectedIndex == 2;
            gbConfiguracoesREPIII.Visible = cboModelo.SelectedIndex == 2;
            grbAlteracaoUsuarioSenhaComunicacao.Visible = cboModelo.SelectedIndex == 2;

            // Controles não visíveis para REP III.
            btnREPManutencao.Visible = cboModelo.SelectedIndex != 2;
            btnLimparVariaveisEssenciais.Visible = cboModelo.SelectedIndex != 2;
            btnReconstruirTabelaFuncionarios.Visible = cboModelo.SelectedIndex != 2;
            btnColetarLogEventosMRP.Visible = cboModelo.SelectedIndex != 2;
            grbCapacidadeMaxLista.Visible = cboModelo.SelectedIndex != 2;
            grbColetaEventos.Visible = cboModelo.SelectedIndex != 2;

            // Visibilidade da tab de funcionários completos (deve ser visível apenas para REP III).
            // Ajuste da posição do botão Ativar bootloader.
            if (cboModelo.SelectedIndex == 2)
            {
                if (!this.tabPrincipal.TabPages.Contains(tbpFuncionariosCompletos))
                    this.tabPrincipal.TabPages.Add(tbpFuncionariosCompletos);

                if (!this.tabPrincipal.TabPages.Contains(tbpConfiguracoesClient))
                    this.tabPrincipal.TabPages.Add(tbpConfiguracoesClient);

                if (!this.tabPrincipal.TabPages.Contains(tbpEventosSistema))
                    this.tabPrincipal.TabPages.Add(tbpEventosSistema);

                btnAtivarBootLoader.Top = 117;
            }
            else
            {
                if (this.tabPrincipal.TabPages.Contains(tbpFuncionariosCompletos))
                    this.tabPrincipal.TabPages.Remove(tbpFuncionariosCompletos);

                if (this.tabPrincipal.TabPages.Contains(tbpConfiguracoesClient))
                    this.tabPrincipal.TabPages.Remove(tbpConfiguracoesClient);

                if (this.tabPrincipal.TabPages.Contains(tbpEventosSistema))
                    this.tabPrincipal.TabPages.Remove(tbpEventosSistema);

                btnAtivarBootLoader.Top = 207;
            }
        }

        private void MonitoringSendReceiveMessageListener(MonitoringSendReceiveType monitoringSendReceiveType, String monitoring, String filenamePrefix)
        {
            try
            {
                FileStream fsArquivo;
                string mensagem;
                byte[] mensagemParaEscrita;

                System.Console.WriteLine(monitoringSendReceiveType.ToString() + " - " + monitoring);

                if (chkArmazenaMonitoracao.Checked)
                {
                    // Verifica se deve criar a pasta para armazenar o arquivo de monitoração.
                    if (!Directory.Exists(CaminhoEXE() + "\\Monitoração"))
                    {
                        Directory.CreateDirectory(CaminhoEXE() + "\\Monitoração");
                    }

                    // Escreve no arquivo de monitoração.
                    string nomeArquivo = CaminhoEXE() + "\\Monitoração\\" + filenamePrefix + String.Format("{0:ddMMyyyy}", DateTime.Now) + ".txt";

                    fsArquivo = new FileStream(nomeArquivo, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                    if (monitoringSendReceiveType == MonitoringSendReceiveType.Send)
                        mensagem = string.Format("Enviado..{0}: ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
                    else
                        mensagem = string.Format("Recebido.{0}: ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));

                    mensagem += monitoring + "\r\n";

                    mensagemParaEscrita = System.Text.Encoding.Default.GetBytes(mensagem);

                    fsArquivo.Write(mensagemParaEscrita, 0, mensagemParaEscrita.Length);

                    fsArquivo.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro durante a escrita no arquivo de log de monitoração.\r\nErro: " + ex.Message);
            }
        }

        private void MonitoringSendReceiveMessageListenerClient(MonitoringSendReceiveType monitoringSendReceiveType, String monitoring)
        {
            MonitoringSendReceiveMessageListener(monitoringSendReceiveType, monitoring, "Log_Monitoracao_Client_");
        }

        private void MonitoringSendReceiveMessageListenerServer(MonitoringSendReceiveType monitoringSendReceiveType, String monitoring)
        {
            MonitoringSendReceiveMessageListener(monitoringSendReceiveType, monitoring, "Log_Monitoracao_Server_");
        }

        #endregion

        #region " ---------------------> Leitura e gravação das informações contidas nos grids <---------------------"
        private void btnGravarXML_Click(object sender, EventArgs e)
        {
            this._dsREP.WriteXml(this.CaminhoEXE() + "\\DataDSREP.xml");

            MessageBox.Show("XML gravado com sucesso!", Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnLerXML_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(this.CaminhoEXE() + "\\DataDSREP.xml"))
            {
                this._dsREP = new dsSDKREP();
                this._dsREP.ReadXml(this.CaminhoEXE() + "\\DataDSREP.xml");

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

        #region " ---------------------> Event Handles do Form. <---------------------"

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(CaminhoEXE() + "\\EnderecoIP.txt");
            streamWriter.Write(this.txtIPRelogio.Text);
            streamWriter.Close();

            try
            {
                _watchListener.StopListening();
            }
            catch { }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(CaminhoEXE() + "\\EnderecoIP.txt"))
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(CaminhoEXE() + "\\EnderecoIP.txt");
                this.txtIPRelogio.Text = streamReader.ReadLine();
                streamReader.Close();
            }

            this.cboSensor.SelectedIndex = 0;
            this.cboModelo.SelectedIndex = 0;
            this.cboEnergiaImpressora.SelectedIndex = 0;
            this.rdbClient.Checked = true;
            this.grbPorta.Location = this.grbIp.Location;
            this.tabPrincipal.TabPages.Remove(tbpFabrica);
        }

        private void tabPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift == true && e.KeyCode == Keys.F2)
                if (this.tabPrincipal.TabPages.Contains(tbpFabrica))
                    this.tabPrincipal.TabPages.Remove(tbpFabrica);
                else
                    this.tabPrincipal.TabPages.Add(tbpFabrica);

            if (e.Shift == true && e.KeyCode == Keys.F12)
            {
                if (this.cboModelo.SelectedIndex == 2) // REP III.
                {
                    bool visibility = !btnREPManutencao.Visible;

                    btnREPManutencao.Visible = visibility;
                    btnLimparVariaveisEssenciais.Visible = visibility;
                    btnReconstruirTabelaFuncionarios.Visible = visibility;
                    btnColetarLogEventosMRP.Visible = visibility;
                    grbCapacidadeMaxLista.Visible = visibility;
                    grbColetaEventos.Visible = visibility;

                    btnAtivarBootLoader.Top = (visibility) ? 207 : 117;
                }
            }
        }

        private void chkArmazenaMonitoracao_CheckedChanged(object sender, EventArgs e)
        {
            if (chkArmazenaMonitoracao.Checked)
            {
                MessageBox.Show("O arquivo de monitoração será criado no diretório: " + CaminhoEXE() + "Monitoração");
            }
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Histórico de Comandos <---------------------"
        private void btnApagarHistorico_Click(object sender, EventArgs e)
        {
            this._dtHistoricoComandos = new dsSDKREP.dtHistoricoComandosDataTable();
            this.dtgHistoricoComandos.DataSource = this._dtHistoricoComandos;
            this.dtgHistoricoComandos.Refresh();
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Geral <---------------------"

        private void btnAlteracaoUsuarioSenhaComunicacao_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.AtualizacaoUsuarioSenhaComunicacao,
                                    ETipoHistorico.Envio, "");

                this._watchComm.OpenConnection();

                this._watchComm.UpdateCommunicationUser(txtNovoUsuarioComunicacao.Text, txtNovaSenhaComunicacao.Text);

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.AtualizacaoUsuarioSenhaComunicacao,
                                    ETipoHistorico.ConfirmacaoRecebimento,
                                    "");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.AtualizacaoUsuarioSenhaComunicacao,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnEnviarDataHora_Click(object sender, EventArgs e)
        {
            DateTime dtEnvio;

            if (!InstanciaWatchComm()) return;

            if (this.rdbDataHoraAtual.Checked == true)

                dtEnvio = DateTime.Now;
            else

                dtEnvio = new DateTime(this.dtpData.Value.Year,
                                       this.dtpData.Value.Month,
                                       this.dtpData.Value.Day,
                                       this.dtpHora.Value.Hour,
                                       this.dtpHora.Value.Minute,
                                       this.dtpHora.Value.Second);

            AddHistoricoComando(ETipoOrigemComando.Software,
                                ETipoComando.AtualizaDataHora,
                                ETipoHistorico.Envio,
                                dtEnvio.ToShortDateString() + " " + dtEnvio.ToShortTimeString());

            try
            {
                this._watchComm.OpenConnection();

                if (cboModelo.SelectedIndex == 2) // REP III
                    this._watchComm.SetDateTime(dtEnvio, txtCPFResponsavel.Text);
                else
                    this._watchComm.SetDateTime(dtEnvio);

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.AtualizaDataHora,
                                    ETipoHistorico.ConfirmacaoRecebimento,
                                    "");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.AtualizaDataHora,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void rdbDataHoraIndicada_CheckedChanged(object sender, EventArgs e)
        {
            this.dtpData.Enabled = rdbDataHoraIndicada.Checked;
            this.dtpHora.Enabled = rdbDataHoraIndicada.Checked;
        }

        private void btnReposicionamentoPonteiroNSR_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                if (this.rdbReposicionamentoMRPPrimeiroRegistro.Checked == true)
                {
                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.ReposicionaPonteiroLeituraMRP,
                                        ETipoHistorico.Envio,
                                        "Reposicionar a partir do primeiro registro.");

                    this._watchComm.RepositioningMRPRecordsPointer();
                }
                else if (this.rdbReposicionamentoMRPPorData.Checked == true)
                {
                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.ReposicionaPonteiroLeituraMRP,
                                        ETipoHistorico.Envio,
                                        "Reposicionar a partir da data: " + this.dtpDataReposicionamentoMRP.Value.ToShortDateString());

                    this._watchComm.RepositioningMRPRecordsPointer(this.dtpDataReposicionamentoMRP.Value);
                }
                else if (this.rdbReposicionamentoMRPPorNSR.Checked == true)
                {
                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.ReposicionaPonteiroLeituraMRP,
                                        ETipoHistorico.Envio,
                                        "Reposicionar a partir do NSR: " + this.txtReposicionamentoNSR.Text);

                    this._watchComm.RepositioningMRPRecordsPointer(this.txtReposicionamentoNSR.Text);
                }

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ReposicionaPonteiroLeituraMRP,
                                    ETipoHistorico.ConfirmacaoRecebimento,
                                    "");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ReposicionaPonteiroLeituraMRP,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void rdbData_CheckedChanged(object sender, EventArgs e)
        {
            this.dtpDataReposicionamentoMRP.Enabled = this.rdbReposicionamentoMRPPorData.Checked;
        }

        private void rdbNSR_CheckedChanged(object sender, EventArgs e)
        {
            this.txtReposicionamentoNSR.Enabled = this.rdbReposicionamentoMRPPorNSR.Checked;
        }

        private void btnEnviarComandoMensagensDisplay_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            foreach (DataRow dr in this._dtMensagensDisplay)
            {
                try
                {
                    this._watchComm.OpenConnection();

                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.EnvioMensagemDisplayRelogio,
                                        ETipoHistorico.Envio,
                                        dr["Codigo"].ToString() + " - " + dr["Mensagem"].ToString());

                    this._watchComm.SendDisplayMessage(Int16.Parse(dr["Codigo"].ToString()),
                                                       dr["Mensagem"].ToString());

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.EnvioMensagemDisplayRelogio,
                                        ETipoHistorico.ConfirmacaoRecebimento, "");
                }
                catch (Exception ex)
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.EnvioMensagemDisplayRelogio,
                                        ETipoHistorico.Erro,
                                        ex.Message);

                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
                finally
                {
                    if (this._watchComm != null)
                        this._watchComm.CloseConnection();
                }
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnEnviaComandoApagaMensagemDisplay_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.LimpezaMensagemDisplay,
                                    ETipoHistorico.Envio, "");

                this._watchComm.ClearDisplayMessage();

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.LimpezaMensagemDisplay,
                                    ETipoHistorico.ConfirmacaoRecebimento, "");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.LimpezaMensagemDisplay,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void btnREPManutencao_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.REPPlacesInMaintenance();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnLimparVariaveisEssenciais_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.CleanEssentialVariables();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnReconstruirTabelaFuncionarios_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.RebuildEmployeesTable();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
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

            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.GeneralTimeout = 15000;

                this._watchComm.OpenConnection();

                this._watchComm.UpdateFirmware(this.txtArquivoDownloadFirmware.Text);

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnAtivarBootLoader_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.ActivateBootLoader();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnColetarEventos_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            PrintPointEvent printPointEvent;

            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.SolicitaRegistroEvento,
                                    ETipoHistorico.Envio, "");

                this._watchComm.OpenConnection();

                printPointEvent = this._watchComm.InquiryPrintPointEvent();

                if (printPointEvent == null)

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.SolicitaRegistroEvento,
                                        ETipoHistorico.ConfirmacaoRecebimento,
                                        "O relógio não possui eventos.");
                else
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.SolicitaRegistroEvento,
                                        ETipoHistorico.ConfirmacaoRecebimento,
                                        "Evento coletados...");

                    while (printPointEvent != null)
                    {
                        DataRow dr = this._dtEventos.NewRow();

                        dr["DataHoraEvento"] = printPointEvent.EventDateTime.ToShortDateString() + " - " + printPointEvent.EventDateTime.ToShortTimeString() + ":" + printPointEvent.EventDateTime.Second.ToString();

                        switch (printPointEvent.EventType)
                        {
                            case PrintPointEventType.LinkConnected:
                                dr["Evento"] = "Link conectado";
                                break;
                            case PrintPointEventType.LinkDisconnected:
                                dr["Evento"] = "Link desconectado";
                                break;
                            case PrintPointEventType.ViolationSensorWithLink:
                                dr["Evento"] = "Sensor de violação com link presente";
                                break;
                            case PrintPointEventType.ViolationSensorWithoutLink:
                                dr["Evento"] = "Sensor de violação sem o link presente";
                                break;
                            case PrintPointEventType.ViolationSensorWithClockOff:
                                dr["Evento"] = "Sensor de violação com o relógio desligado";
                                break;
                            default:
                                dr["Evento"] = "Desconhecido";
                                break;
                        }

                        this._dtEventos.Rows.Add(dr);

                        AddHistoricoComando(ETipoOrigemComando.Software,
                                            ETipoComando.ConfirmaRecebimentoRegistrosMRP,
                                            ETipoHistorico.Envio, "");

                        printPointEvent = this._watchComm.ConfirmReceiptPrintPointEvent();

                        if (printPointEvent == null)

                            AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                ETipoComando.ConfirmaRecebimentoRegistrosEvento,
                                                ETipoHistorico.ConfirmacaoRecebimento,
                                                "O relógio não possui evento.");
                        else

                            AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                ETipoComando.ConfirmaRecebimentoRegistrosEvento,
                                                ETipoHistorico.ConfirmacaoRecebimento,
                                                "Evento coletado...");
                    }
                }

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.SolicitaRegistroEvento,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnLimparGridEventos_Click(object sender, EventArgs e)
        {
            _dtEventos = new dsSDKREP.dtEventosDataTable();
            this.dtgEventos.DataSource = _dtEventos;
        }

        private string BytesToHex(byte[] bytes)
        {
            StringBuilder hexString = new StringBuilder(bytes.Length);
            for (int i = 0; i < bytes.Length; i++)
            {
                hexString.Append(bytes[i].ToString("X2"));
            }
            return hexString.ToString();
        }

        private void btnColetarLogEventosMRP_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                PrintPointMRPEventLog printPointMRPEventLog = this._watchComm.InquiryPrintPointMRPEventLog();

                String nomeArquivo = this.CaminhoEXE() +
                                    "LOG_EVENTOS_MRP_" +
                                    DateTime.Now.ToShortDateString().Replace("/", "") + "_" +
                                    DateTime.Now.ToLongTimeString().Replace(":", "") + ".txt";

                System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(nomeArquivo, true);

                streamWriter.Write(this.BytesToHex(printPointMRPEventLog.Return));

                streamWriter.Close();
                streamWriter.Dispose();
                streamWriter = null;

                System.Diagnostics.Process shell = new System.Diagnostics.Process();
                shell.StartInfo.UseShellExecute = true;
                shell.StartInfo.FileName = "notepad.exe";
                shell.StartInfo.Arguments = nomeArquivo;
                shell.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                shell.Start();

                shell.Dispose();
                shell = null;

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnEnviarCapacidadeLista_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                this._watchComm.ProgramListCapability(Int32.Parse(txtCapacidadeMaxLista.Text));

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnUpgradeCapLista_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                RandomNumberResponse RandomNumber = this._watchComm.InquiryRandomNumberForUpdateListCapability();
                Int64 NumberSerialRep = long.Parse(this._watchComm.InquirySerialNumberOfREPAndMemory().SerialNumber);

                this._watchComm.SendUpdateListCapability(NumberSerialRep, RandomNumber, Int32.Parse(txtCapacidadeMaxLista.Text));

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Funcionários / Credenciais <---------------------"

        private void btnObterFuncionarios_Click(object sender, EventArgs e)
        {
            this._dtFuncionarios.Rows.Clear();

            if (!InstanciaWatchComm()) return;

            PrintPointEmployee[] employees;

            try
            {
                this._watchComm.OpenConnection();

                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.SolicitaRegistroMRP,
                                    ETipoHistorico.Envio, "");

                employees = this._watchComm.InquiryEmployeeList();

                if (employees == null)

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.SolicitaListaFuncionarios,
                                        ETipoHistorico.ConfirmacaoRecebimento,
                                        "O relógio não possui funcionários.");
                else
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.SolicitaListaFuncionarios,
                                        ETipoHistorico.ConfirmacaoRecebimento,
                                        "Funcionários coletados...");

                    while (employees != null)
                    {
                        foreach (PrintPointEmployee employee in employees)
                        {
                            DataRow dr = this._dtFuncionarios.NewRow();
                            dr["PIS"] = employee.Pis;
                            dr["Nome"] = employee.Name;
                            dr["Senha"] = employee.Password;

                            this._dtFuncionarios.Rows.Add(dr);
                        }

                        AddHistoricoComando(ETipoOrigemComando.Software,
                                            ETipoComando.ConfirmaRecebimentoListaFuncionarios,
                                            ETipoHistorico.Envio, "");

                        employees = this._watchComm.ConfirmationReceiptEmployeeList();

                        if (employees == null)

                            AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                ETipoComando.ConfirmaRecebimentoListaFuncionarios,
                                                ETipoHistorico.ConfirmacaoRecebimento,
                                                "O relógio não possui funcionários.");
                        else

                            AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                ETipoComando.ConfirmaRecebimentoListaFuncionarios,
                                                ETipoHistorico.ConfirmacaoRecebimento,
                                                "Funcionários coletados...");
                    }

                    this.dtgListaFuncionarios.DataSource = null;
                    this.dtgListaFuncionarios.DataSource = this._dtFuncionarios;
                }

                ComandoRecepcionadoComSucesso();

                this.lblTotalFuncionarios.Text = "Total de Funcionários: " + this._dtFuncionarios.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.SolicitaListaFuncionarios,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnIncluirFuncionarios_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                // Insere os funcionários na lista do componente.
                foreach (DataRow dr in this._dtFuncionarios)
                {
                    try
                    {
                        this._watchComm.AddEmployee(dr["PIS"].ToString(), dr["Nome"].ToString(), dr["Senha"].ToString());
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }

                this._watchComm.OpenConnection();

                // Solicita ao componente o envio da lista de funcionários.
                try
                {
                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.InsereFuncionarioLista,
                                        ETipoHistorico.Envio,
                                        "Envio da lista de funcionários");

                    if (cboModelo.SelectedIndex == 2) // REP III
                        this._watchComm.IncludeEmployeesList(true, false, txtCPFResponsavel.Text);
                    else
                        this._watchComm.IncludeEmployeesList(true, false);

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.InsereFuncionarioLista,
                                        ETipoHistorico.ConfirmacaoRecebimento, "");
                }
                catch (Exception ex)
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.InsereFuncionarioLista,
                                        ETipoHistorico.Erro,
                                        ex.Message);

                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnExcluirFuncionarios_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                // Insere os funcionários na lista do componente.
                foreach (DataRow dr in this._dtFuncionarios)
                {
                    try
                    {
                        this._watchComm.AddEmployee(dr["PIS"].ToString());
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
                    this._watchComm.OpenConnection();

                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.ExcluiFuncionarioLista,
                                        ETipoHistorico.Envio,
                                        "Exclusão da lista de funcionários");

                    if (cboModelo.SelectedIndex == 2) // REP III
                        this._watchComm.ExcludeEmployeesList(txtCPFResponsavel.Text);
                    else
                        this._watchComm.ExcludeEmployeesList();

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.ExcluiFuncionarioLista,
                                        ETipoHistorico.ConfirmacaoRecebimento, "");
                }
                catch (Exception ex)
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.ExcluiFuncionarioLista,
                                        ETipoHistorico.Erro,
                                        ex.Message);

                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnExcluiTodosFuncionarios_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            // Solicita ao componente a exclusão de todos os funcionários do relógio.
            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.ExcluiFuncionarioLista,
                                    ETipoHistorico.Envio,
                                    "Exclusão de todos os funcionários do relógio.");

                this._watchComm.OpenConnection();

                // Comando suportado apenas pelo REP III.
                this._watchComm.ClearClockEmployeesList(txtCPFResponsavel.Text);

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ExcluiFuncionarioLista,
                                    ETipoHistorico.ConfirmacaoRecebimento, "");
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ExcluiFuncionarioLista,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);

                return;
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnEnviaListaCredenciais_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                // Insere as credenciais na lista do componente.
                foreach (DataRow dr in this._dtCredenciais)
                {
                    try
                    {
                        byte via = (byte)Int16.Parse((dr["Via"].ToString() == "" ? "0" : dr["Via"].ToString()).ToString());
                        this._watchComm.AddCredential(dr["Credencial"].ToString(), dr["PIS"].ToString(), via);
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }

                this._watchComm.OpenConnection();

                // Solicita ao componente o envio da lista de credenciais.
                try
                {
                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.InsereCredencialLista,
                                        ETipoHistorico.Envio,
                                        "Inclusão da lista de credenciais");

                    if (cboModelo.SelectedIndex == 2) // REP III
                        this._watchComm.IncludeCredentialList(true, txtCPFResponsavel.Text);
                    else
                        this._watchComm.IncludeCredentialList(true);

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.InsereCredencialLista,
                                        ETipoHistorico.ConfirmacaoRecebimento, "");
                }
                catch (Exception ex)
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.InsereCredencialLista,
                                        ETipoHistorico.Erro,
                                        ex.Message);

                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnApagaListaCredenciais_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                // Insere as credenciais na lista do componente.
                foreach (DataRow dr in this._dtCredenciais)
                {
                    try
                    {
                        byte via = (byte)Int16.Parse((dr["Via"].ToString() == "" ? "0" : dr["Via"].ToString()).ToString());
                        this._watchComm.AddCredential(dr["Credencial"].ToString(), dr["PIS"].ToString(), via);
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }

                this._watchComm.OpenConnection();

                // Solicita ao componente a exclusão da lista de credenciais.
                try
                {
                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.ExcluiCredencialLista,
                                        ETipoHistorico.Envio,
                                        "Exclusão das credenciais listadas no grid.");

                    if (cboModelo.SelectedIndex == 2) // REP III
                        this._watchComm.ExcludeCredentialList(txtCPFResponsavel.Text);
                    else
                        this._watchComm.ExcludeCredentialList();

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.ExcluiCredencialLista,
                                        ETipoHistorico.ConfirmacaoRecebimento, "");
                }
                catch (Exception ex)
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.ExcluiCredencialLista,
                                        ETipoHistorico.Erro,
                                        ex.Message);

                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnExcluiTodasCredenciais_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            // Solicita ao componente a exclusão de todas as credenciais do relógio.
            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.ExcluiCredencialLista,
                                    ETipoHistorico.Envio,
                                    "Exclusão de todas as credenciais do relógio.");

                this._watchComm.OpenConnection();

                if (cboModelo.SelectedIndex == 2) // REP III
                    this._watchComm.ClearClockCredentialsList(txtCPFResponsavel.Text);
                else
                    this._watchComm.ClearClockCredentialsList();

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ExcluiCredencialLista,
                                    ETipoHistorico.ConfirmacaoRecebimento, "");
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ExcluiCredencialLista,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);

                return;
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnExcluiCredenciaisAssociadasPIS_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            // Solicita ao componente a exclusão de todas as credenciais do relógio.
            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.ExcluiCredencialLista,
                                    ETipoHistorico.Envio,
                                    "Exclusão de todas as credenciais associadas a um PIS em específico.");

                this._watchComm.OpenConnection();

                this._watchComm.ClearCredentialsListOfSpecificEmployee(txtPISExclusaoCredenciais.Text, txtCPFResponsavel.Text);

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ExcluiCredencialLista,
                                    ETipoHistorico.ConfirmacaoRecebimento, "");
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ExcluiCredencialLista,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);

                return;
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnObterCredenciais_Click(object sender, EventArgs e)
        {
            this._dtCredenciais.Rows.Clear();

            if (!InstanciaWatchComm()) return;

            List<PrintPointCredential> credenciais;

            try
            {
                this._watchComm.OpenConnection();

                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.SolicitaListaCredenciais,
                                    ETipoHistorico.Envio, "");

                if (rdbObterCredenciais_Todas_OrdenadasPorPIS.Checked)
                    credenciais = this._watchComm.InquiryCredentialsList(InquiryCredentialListSearchType.AllOrderedByPIS);
                else if (rdbObterCredenciais_Todas_OrdenadasPorCredencial.Checked)
                    credenciais = this._watchComm.InquiryCredentialsList(InquiryCredentialListSearchType.AllOrderedByCredential);
                else
                    credenciais = this._watchComm.InquiryCredentialsList(txtPISObterCredenciais.Text);

                if (credenciais == null)

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.SolicitaListaCredenciais,
                                        ETipoHistorico.ConfirmacaoRecebimento,
                                        "O relógio não possui credenciais.");
                else
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.SolicitaListaCredenciais,
                                        ETipoHistorico.ConfirmacaoRecebimento,
                                        "Credenciais coletadas.");

                    while (credenciais != null)
                    {
                        foreach (PrintPointCredential credencial in credenciais)
                        {
                            DataRow dr = this._dtCredenciais.NewRow();

                            dr["PIS"] = credencial.Pis;
                            dr["Credencial"] = credencial.Credential;
                            dr["Via"] = credencial.Via_version.ToString();

                            this._dtCredenciais.Rows.Add(dr);
                        }

                        AddHistoricoComando(ETipoOrigemComando.Software,
                                            ETipoComando.ConfirmaRecebimentoListaCredenciais,
                                            ETipoHistorico.Envio, "");

                        credenciais = this._watchComm.ConfirmationReceiptCredentialsList();

                        if (credenciais == null)

                            AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                ETipoComando.ConfirmaRecebimentoListaCredenciais,
                                                ETipoHistorico.ConfirmacaoRecebimento,
                                                "O relógio não possui credenciais.");
                        else

                            AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                ETipoComando.ConfirmaRecebimentoListaCredenciais,
                                                ETipoHistorico.ConfirmacaoRecebimento,
                                                "Credenciais coletadas.");
                    }

                    this.dtgListaCredenciais.DataSource = null;
                    this.dtgListaCredenciais.DataSource = this._dtCredenciais;
                }

                ComandoRecepcionadoComSucesso();

                this.lblTotalFuncionarios.Text = "Total de Funcionários: " + this._dtFuncionarios.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.SolicitaListaCredenciais,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Templates <---------------------"

        private void btnInclusaoTemplates_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            _watchComm.OpenConnection();

            foreach (DataRow dr in this._dtTemplatesParaEnvio)
            {
                try
                {
                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.EnviaTemplate,
                                        ETipoHistorico.Envio,
                                        "Envio do template do funcionário: " + dr["PIS"].ToString());

                    EfingerPrintSensor fingerPrintSensor;

                    if (this.cboSensor.SelectedIndex == 0)
                        fingerPrintSensor = EfingerPrintSensor.Sagem;
                    else if (this.cboSensor.SelectedIndex == 1)
                        fingerPrintSensor = EfingerPrintSensor.Suprema;
                    else if (this.cboSensor.SelectedIndex == 2)
                        fingerPrintSensor = EfingerPrintSensor.Fugitsu;
                    else if (this.cboSensor.SelectedIndex == 3)
                        fingerPrintSensor = EfingerPrintSensor.Ilock;
                    else
                        fingerPrintSensor = EfingerPrintSensor.Virdi;

                    if (cboModelo.SelectedIndex == 2) // REP III
                    {
                        if (fingerPrintSensor == EfingerPrintSensor.Fugitsu)
                        {
                            this._watchComm.IncludeFingerPrint(dr["PIS"].ToString(),
                                                               dr["Template"].ToString(),
                                                               (EFingerPrintHand)Int16.Parse(dr["Dedo1"].ToString()),
                                                               (EFingerPrintHand)Int16.Parse(dr["Dedo2"].ToString()),
                                                               txtCPFResponsavel.Text);
                        }
                        else
                        {
                            this._watchComm.IncludeFingerPrint(dr["PIS"].ToString(),
                                                               dr["Template"].ToString(),
                                                               (EFingerPrintType)Int16.Parse(dr["Dedo1"].ToString()),
                                                               (EFingerPrintType)Int16.Parse(dr["Dedo2"].ToString()),
                                                               fingerPrintSensor,
                                                               txtCPFResponsavel.Text);
                        }
                    }
                    else
                    {
                        if (fingerPrintSensor == EfingerPrintSensor.Fugitsu)
                        {
                            this._watchComm.IncludeFingerPrint(dr["PIS"].ToString(),
                                                               dr["Template"].ToString(),
                                                               (EFingerPrintHand)Int16.Parse(dr["Dedo1"].ToString()),
                                                               (EFingerPrintHand)Int16.Parse(dr["Dedo2"].ToString()));
                        }
                        else
                        {
                            this._watchComm.IncludeFingerPrint(dr["PIS"].ToString(),
                                                               dr["Template"].ToString(),
                                                               (EFingerPrintType)Int16.Parse(dr["Dedo1"].ToString()),
                                                               (EFingerPrintType)Int16.Parse(dr["Dedo2"].ToString()),
                                                               fingerPrintSensor);
                        }
                    }

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.EnviaTemplate,
                                        ETipoHistorico.ConfirmacaoRecebimento, "");
                }
                catch (Exception ex)
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.EnviaTemplate,
                                        ETipoHistorico.Erro,
                                        ex.Message);

                    ErroDuranteRecepcaoDoComando(ex);

                    _watchComm.CloseConnection();

                    return;
                }
            }

            _watchComm.CloseConnection();

            if (this._dtTemplatesParaEnvio.Rows.Count > 0)
                ComandoRecepcionadoComSucesso();
        }

        private void btnExclusaoTotalTemplates_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.ExcluiTodosTemplates,
                                    ETipoHistorico.Envio,
                                    "Exclusão de todos os templates do relógio.");

                _watchComm.OpenConnection();

                if (cboModelo.SelectedIndex == 2) // REP III
                    _watchComm.ExcludeFingerPrintList(txtCPFResponsavel.Text);
                else
                    _watchComm.ExcludeFingerPrintList();

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ExcluiTodosTemplates,
                                    ETipoHistorico.ConfirmacaoRecebimento, "");
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ExcluiTodosTemplates,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);

                return;
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnExclusaoTemplatesSemPIS_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.ExcluiTodosTemplatesSemPIS,
                                    ETipoHistorico.Envio,
                                    "Exclusão de todos os templates que não possuem PIS cadastrado no relógio.");

                this._watchComm.OpenConnection();

                _watchComm.ExcludeFingerPrintWithoutEmployee();

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ExcluiTodosTemplatesSemPIS,
                                    ETipoHistorico.ConfirmacaoRecebimento, "");
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ExcluiTodosTemplatesSemPIS,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);

                return;
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnExclusaoTemplates_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            _watchComm.OpenConnection();

            foreach (DataRow dr in this._dtTemplatesParaEnvio)
            {
                try
                {
                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.ExcluiTemplate,
                                        ETipoHistorico.Envio,
                                        "Exclusão do template do funcionário: " + dr["PIS"].ToString());

                    EfingerPrintSensor fingerPrintSensor;

                    if (this.cboSensor.SelectedIndex == 0)
                        fingerPrintSensor = EfingerPrintSensor.Sagem;
                    else if (this.cboSensor.SelectedIndex == 1)
                        fingerPrintSensor = EfingerPrintSensor.Suprema;
                    else if (this.cboSensor.SelectedIndex == 2)
                        fingerPrintSensor = EfingerPrintSensor.Fugitsu;
                    else if (this.cboSensor.SelectedIndex == 3)
                        fingerPrintSensor = EfingerPrintSensor.Ilock;
                    else
                        fingerPrintSensor = EfingerPrintSensor.Virdi;

                    if (cboModelo.SelectedIndex == 2) // REP III
                        this._watchComm.ExcludeFingerPrint(dr["PIS"].ToString(), fingerPrintSensor, txtCPFResponsavel.Text);
                    else
                    {
                        if (this.cboSensor.SelectedIndex == 0)
                            this._watchComm.ExcludeFingerPrint(dr["PIS"].ToString());
                        else
                            this._watchComm.ExcludeFingerPrint(dr["PIS"].ToString(), fingerPrintSensor);
                    }

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.ExcluiTemplate,
                                        ETipoHistorico.ConfirmacaoRecebimento, "");
                }
                catch (Exception ex)
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.ExcluiTemplate,
                                        ETipoHistorico.Erro,
                                        ex.Message);

                    ErroDuranteRecepcaoDoComando(ex);

                    _watchComm.CloseConnection();

                    return;
                }
            }

            _watchComm.CloseConnection();

            if (this._dtTemplatesParaEnvio.Rows.Count > 0)
                ComandoRecepcionadoComSucesso();
        }

        private void btnObterTemplates_Click(object sender, EventArgs e)
        {
            this._dtTemplatesRecebidos.Rows.Clear();

            org.cesar.dmplight.watchComm.impl.printpoint.PrintPointFingerPrintMessage template;

            if (!InstanciaWatchComm()) return;

            try
            {
                _watchComm.GeneralTimeout = 10000;
                _watchComm.OpenConnection();

                if (this.rdbObterApenasNovosTemplates.Checked)
                {
                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.SolicitaTemplate,
                                        ETipoHistorico.Envio,
                                        "Apenas os templates ainda não recolhidos.");

                    if (this.cboSensor.SelectedIndex == 3)
                        template = this._watchComm.InquiryIris(org.cesar.dmplight.watchComm.impl.printpoint.InquiryFingerPrintType.OnlyNew);
                    else
                        template = this._watchComm.InquiryFingerPrint(org.cesar.dmplight.watchComm.impl.printpoint.InquiryFingerPrintType.OnlyNew);
                }
                else
                {
                    if (this.cboSensor.SelectedIndex == 3)
                        template = this._watchComm.InquiryIris(org.cesar.dmplight.watchComm.impl.printpoint.InquiryFingerPrintType.All);
                    else
                        template = this._watchComm.InquiryFingerPrint(org.cesar.dmplight.watchComm.impl.printpoint.InquiryFingerPrintType.All);

                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.SolicitaTemplate,
                                        ETipoHistorico.Envio,
                                        "Todos os templates do relógio.");
                }

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.SolicitaTemplate,
                                    ETipoHistorico.ConfirmacaoRecebimento, "");

                if (template == null)

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.ConfirmaRecebimentoTemplate,
                                        ETipoHistorico.ConfirmacaoRecebimento,
                                        "Nenhum template recebido.");
                else
                {
                    while (template != null)
                    {
                        if (template.IsValid)
                        {
                            DataRow dr = this._dtTemplatesRecebidos.NewRow();
                            dr["PIS"] = template.PIS;
                            dr["Dedo1"] = (Int16)template.FingerprintTypeOne;
                            dr["Dedo2"] = (Int16)template.FingerprintTypeTwo;
                            dr["Template"] = template.FingerPrint;

                            this._dtTemplatesRecebidos.Rows.Add(dr);

                            // Solicita o próximo template.
                            AddHistoricoComando(ETipoOrigemComando.Software,
                                                ETipoComando.ConfirmaRecebimentoTemplate,
                                                ETipoHistorico.Envio, "");
                        }
                        else
                        {
                            Console.WriteLine("Digital Inválida!\n" + template.ToString());
                        }

                        template = this._watchComm.ConfirmationReceiptFingerPrint();

                        if (template == null)
                            AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                ETipoComando.ConfirmaRecebimentoTemplate,
                                                ETipoHistorico.ConfirmacaoRecebimento,
                                                "Fim do recebimento de templates.");
                        else
                            AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                ETipoComando.ConfirmaRecebimentoTemplate,
                                                ETipoHistorico.ConfirmacaoRecebimento,
                                                "Template recebido.");
                    }

                    this.dtgRecebimentoTemplates.DataSource = null;
                    this.dtgRecebimentoTemplates.DataSource = this._dtTemplatesRecebidos;
                }
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.SolicitaTemplate,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);

                return;
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnCopiarTemplatesParaGridEnvio_Click(object sender, EventArgs e)
        {
            foreach (DataRow drTemplateRecebido in this._dtTemplatesRecebidos.Rows)
            {
                DataRow drTemplateParaEnvio = this._dtTemplatesParaEnvio.NewRow();

                drTemplateParaEnvio["PIS"] = drTemplateRecebido["PIS"];
                drTemplateParaEnvio["Dedo1"] = drTemplateRecebido["Dedo1"];
                drTemplateParaEnvio["Dedo2"] = drTemplateRecebido["Dedo2"];
                drTemplateParaEnvio["Template"] = drTemplateRecebido["Template"];

                this._dtTemplatesParaEnvio.Rows.Add(drTemplateParaEnvio);
            }

            this.dtgEnvioTemplates.DataSource = null;
            this.dtgEnvioTemplates.DataSource = this._dtTemplatesParaEnvio;
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Supervisores <---------------------"
        private void btnApagarSupervisoresRelogio_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.LimpaListaSupervisores,
                                    ETipoHistorico.Envio,
                                    "Apagar toda a lista de supervisores do relógio.");

                _watchComm.OpenConnection();

                this._watchComm.ClearMasterList();

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.LimpaListaSupervisores,
                                    ETipoHistorico.ConfirmacaoRecebimento, "");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.LimpaListaSupervisores,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnEnviaComandoInclusaoSupervisores_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            foreach (DataRow dr in this._dtSupervisores)
            {
                try
                {
                    if (dr["ProgramacoesTecnicas"].ToString() == "False" &&
                        dr["AlteraDataHora"].ToString() == "False" &&
                        dr["ProgramaPenDrive"].ToString() == "False" &&
                        dr["AlteracaoBobina"].ToString() == "False")
                    {
                        MessageBox.Show("Deve-se informar ao menos uma permissão para cada um dos supervisores incluídos!",
                                        Application.ProductName,
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        return;
                    }

                    if (cboModelo.SelectedIndex == 2) // REP III
                    {
                        this._watchComm.AddMaster(dr["PIS"].ToString(),
                                                  dr["Credencial"].ToString(),
                                                  dr["Senha"].ToString(),
                                                  dr["ProgramacoesTecnicas"].ToString() == "True" ? true : false,
                                                  dr["AlteraDataHora"].ToString() == "True" ? true : false,
                                                  dr["ProgramaPenDrive"].ToString() == "True" ? true : false,
                                                  dr["AlteracaoBobina"].ToString() == "True" ? true : false,
                                                  dr["CPF"].ToString());
                    }
                    else
                    {
                        this._watchComm.AddMaster(dr["PIS"].ToString(),
                                                  dr["Credencial"].ToString(),
                                                  dr["Senha"].ToString(),
                                                  dr["ProgramacoesTecnicas"].ToString() == "True" ? true : false,
                                                  dr["AlteraDataHora"].ToString() == "True" ? true : false,
                                                  dr["ProgramaPenDrive"].ToString() == "True" ? true : false,
                                                  dr["AlteracaoBobina"].ToString() == "True" ? true : false);
                    }
                }
                catch (Exception ex)
                {
                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
            }

            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.InsereSupervisorLista,
                                    ETipoHistorico.Envio, "");

                _watchComm.OpenConnection();

                this._watchComm.SendMasterList();

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.InsereSupervisorLista,
                                        ETipoHistorico.ConfirmacaoRecebimento, "");
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                       ETipoComando.InsereSupervisorLista,
                                                       ETipoHistorico.Erro,
                                                       ex.Message);

                ErroDuranteRecepcaoDoComando(ex);

                return;
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            if (this._dtSupervisores.Rows.Count > 0)
                ComandoRecepcionadoComSucesso();
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Empregador <---------------------"
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

        public bool ValidaCamposEmpregador()
        {
            bool blnRetorno = true;

            //if (this.txtNomeEmpresa1.Text.Trim().Length == 0 &
            //    this.txtNomeEmpresa2.Text.Trim().Length == 0 &
            //    this.txtNomeEmpresa3.Text.Trim().Length == 0 &
            //    this.txtNomeEmpresa4.Text.Trim().Length == 0 &
            //    this.txtNomeEmpresa5.Text.Trim().Length == 0)
            //{

            //    ErrorProvider.SetError(this.txtNomeEmpresa1, "Informação obrigatória.");
            //    ErrorProvider.SetError(this.txtNomeEmpresa2, "");
            //    ErrorProvider.SetError(this.txtNomeEmpresa3, "");
            //    ErrorProvider.SetError(this.txtNomeEmpresa4, "");
            //    ErrorProvider.SetError(this.txtNomeEmpresa5, "");

            //    blnRetorno = blnRetorno & false;
            //}
            //else
            //{
            //    ErrorProvider.SetError(this.txtNomeEmpresa1, "");
            //    blnRetorno = blnRetorno & true;
            //}

            //if (this.txtEnderecoEmpresa1.Text.Trim().Length == 0 &
            //    this.txtEnderecoEmpresa2.Text.Trim().Length == 0 &
            //    this.txtEnderecoEmpresa3.Text.Trim().Length == 0)
            //{

            //    ErrorProvider.SetError(this.txtEnderecoEmpresa1, "Informação obrigatória.");
            //    ErrorProvider.SetError(this.txtEnderecoEmpresa2, "");
            //    ErrorProvider.SetError(this.txtEnderecoEmpresa3, "");

            //    blnRetorno = blnRetorno & false;
            //}
            //else
            //{

            //    ErrorProvider.SetError(this.txtEnderecoEmpresa1, "");
            //    blnRetorno = blnRetorno & true;
            //}

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

        public void CarregaDadosEmpregador(org.cesar.dmplight.watchComm.impl.printpoint.PrintPointEmployerMessage empregador)
        {
            this.mskCEI.Text = empregador.CEI.PadLeft(12, Char.Parse("0")).Substring(0, 12);

            if (empregador.EmployerType == org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CPF)
                this.cboTipoPessoa.SelectedIndex = 1;
            else
                this.cboTipoPessoa.SelectedIndex = 0;

            if (empregador.EmployerType == org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CPF)
                this.mskCPF_CNPJ.Text = empregador.CPF_CNPJ.Substring(3, 11);
            else
                this.mskCPF_CNPJ.Text = empregador.CPF_CNPJ;

            if (cboModelo.SelectedIndex != 0) // Diferente de REP.            
            {
                this.txtNomeEmpresa1.Text = empregador.Name.Substring(0, 35);

                if (empregador.Name.Length > 36)
                    this.txtNomeEmpresa2.Text = empregador.Name.Substring(35, 35);

                if (empregador.Name.Length > 72)
                    this.txtNomeEmpresa3.Text = empregador.Name.Substring(70, 35);

                if (empregador.Name.Length > 108)
                    this.txtNomeEmpresa4.Text = empregador.Name.Substring(105, 35);

                if (empregador.Name.Length > 144)
                    this.txtNomeEmpresa5.Text = empregador.Name.Substring(140);

                this.txtEnderecoEmpresa1.Text = empregador.Address.Substring(0, 35);

                if (empregador.Address.Length > 36)
                    this.txtEnderecoEmpresa2.Text = empregador.Address.Substring(35, 35);

                if (empregador.Address.Length > 72)
                    this.txtEnderecoEmpresa3.Text = empregador.Address.Substring(70);
            }
            else
            {
                this.txtNomeEmpresa1.Text = empregador.Name.Substring(0, 36);

                if (empregador.Name.Length > 36)
                    this.txtNomeEmpresa2.Text = empregador.Name.Substring(36, 36);

                if (empregador.Name.Length > 72)
                    this.txtNomeEmpresa3.Text = empregador.Name.Substring(72, 36);

                if (empregador.Name.Length > 108)
                    this.txtNomeEmpresa4.Text = empregador.Name.Substring(108, 36);

                if (empregador.Name.Length > 144)
                    this.txtNomeEmpresa5.Text = empregador.Name.Substring(144);

                this.txtEnderecoEmpresa1.Text = empregador.Address.Substring(0, 36);

                if (empregador.Address.Length > 36)
                    this.txtEnderecoEmpresa2.Text = empregador.Address.Substring(36, 36);

                if (empregador.Address.Length > 72)
                    this.txtEnderecoEmpresa3.Text = empregador.Address.Substring(72);
            }
        }

        private void btnObterEmpregadorRelogio_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.PedeEmpregador,
                                    ETipoHistorico.Envio, "");

                _watchComm.OpenConnection();

                org.cesar.dmplight.watchComm.impl.printpoint.PrintPointEmployerMessage empregador = this._watchComm.InquiryEmployeer();

                if (empregador != null)
                {
                    CarregaDadosEmpregador(empregador);

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.PedeEmpregador,
                                        ETipoHistorico.ConfirmacaoRecebimento, "");
                }
                else
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.PedeEmpregador,
                                        ETipoHistorico.ConfirmacaoRecebimento,
                                        "Empregador em branco.");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.PedeEmpregador,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnEnviarEmpregadorRelogio_Click(object sender, EventArgs e)
        {
            String endereco;
            String razaoSocial;
            org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType tipoEmpregador;

            if (!InstanciaWatchComm()) return;

            if (!ValidaCamposEmpregador())
                return;

            try
            {
                if (cboModelo.SelectedIndex != 0) // Diferente de REP.
                {
                    endereco = this.txtEnderecoEmpresa1.Text.PadRight(35) +
                               this.txtEnderecoEmpresa2.Text.PadRight(35) +
                               this.txtEnderecoEmpresa3.Text.PadRight(30);

                    razaoSocial = this.txtNomeEmpresa1.Text.PadRight(35) +
                                  this.txtNomeEmpresa2.Text.PadRight(35) +
                                  this.txtNomeEmpresa3.Text.PadRight(35) +
                                  this.txtNomeEmpresa4.Text.PadRight(35) +
                                  this.txtNomeEmpresa5.Text.PadRight(10);
                }
                else
                {
                    endereco = this.txtEnderecoEmpresa1.Text.PadRight(36) +
                               this.txtEnderecoEmpresa2.Text.PadRight(36) +
                               this.txtEnderecoEmpresa3.Text.PadRight(28);

                    razaoSocial = this.txtNomeEmpresa1.Text.PadRight(36) +
                                  this.txtNomeEmpresa2.Text.PadRight(36) +
                                  this.txtNomeEmpresa3.Text.PadRight(36) +
                                  this.txtNomeEmpresa4.Text.PadRight(36) +
                                  this.txtNomeEmpresa5.Text.PadRight(6);
                }

                if (this.cboTipoPessoa.SelectedIndex == 0)
                    tipoEmpregador = org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CNPJ;
                else
                    tipoEmpregador = org.cesar.dmplight.watchComm.impl.printpoint.EmployeerType.CPF;

                try
                {
                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.AlteracaoEmpregador,
                                        ETipoHistorico.Envio, "");

                    _watchComm.OpenConnection();

                    if (cboModelo.SelectedIndex == 2) // REP III
                    {
                        this._watchComm.ChangeEmployer(tipoEmpregador,
                                                       Util.RetiraFormatacao(this.mskCPF_CNPJ.Text),
                                                       Util.RetiraFormatacao(this.mskCEI.Text),
                                                       razaoSocial,
                                                       endereco,
                                                       txtCPFResponsavel.Text);
                    }
                    else
                    {
                        this._watchComm.ChangeEmployer(tipoEmpregador,
                                                       Util.RetiraFormatacao(this.mskCPF_CNPJ.Text),
                                                       Util.RetiraFormatacao(this.mskCEI.Text),
                                                       razaoSocial,
                                                       endereco);
                    }

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.AlteracaoEmpregador,
                                        ETipoHistorico.ConfirmacaoRecebimento, "");

                    ComandoRecepcionadoComSucesso();
                }
                catch (Exception ex2)
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.AlteracaoEmpregador,
                                        ETipoHistorico.Erro,
                                        ex2.Message);

                    ErroDuranteRecepcaoDoComando(ex2);
                }
                finally
                {
                    if (this._watchComm != null)
                        this._watchComm.CloseConnection();
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
        #endregion

        #region " ---------------------> Event Handles da Tab Coleta <---------------------"
        private void btnObterRegistrosMRP_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            if (this.chkAlteracoesEmpregador.Checked == false &&
                this.chkDataHora.Checked == false &&
                this.chkEmpregado.Checked == false &&
                this.chkMarcacoesPonto.Checked == false &&
                this.chkEventosSensiveis.Checked == false)
            {
                MessageBox.Show("Selecione ao menos uma opção de coleta!", Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            MRPRecord[] registrosMRP;

            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.SolicitaRegistroMRP,
                                    ETipoHistorico.Envio, "");

                this._watchComm.OpenConnection();

                if (cboModelo.SelectedIndex == 2) // REP III
                {
                    registrosMRP = this._watchComm.InquiryMRPRecords(this.chkEmpregado.Checked,
                                                                     this.chkDataHora.Checked,
                                                                     this.chkMarcacoesPonto.Checked,
                                                                     this.chkAlteracoesEmpregador.Checked,
                                                                     this.chkEventosSensiveis.Checked);
                }
                else
                {
                    registrosMRP = this._watchComm.InquiryMRPRecords(this.chkEmpregado.Checked,
                                                                     this.chkDataHora.Checked,
                                                                     this.chkMarcacoesPonto.Checked,
                                                                     this.chkAlteracoesEmpregador.Checked);
                }

                if (registrosMRP == null)

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.SolicitaRegistroMRP,
                                        ETipoHistorico.ConfirmacaoRecebimento,
                                        "O relógio não possui registros.");
                else
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.SolicitaRegistroMRP,
                                        ETipoHistorico.ConfirmacaoRecebimento,
                                        "Registros coletados...");

                    while (registrosMRP != null)
                    {
                        foreach (MRPRecord registroMRP in registrosMRP)
                        {
                            if (registroMRP.IsValid)
                            {
                                if (registroMRP is MRPRecord_ChangeCompanyIdentification)
                                {
                                    DataRow dr = this._dtAlteracaoEmpregador.NewRow();
                                    dr["NSR"] = ((MRPRecord_ChangeCompanyIdentification)registroMRP).NSR;
                                    dr["DataHoraGravacao"] = ((MRPRecord_ChangeCompanyIdentification)registroMRP).RecordingDateTime;
                                    dr["CNPJ_CPF"] = ((MRPRecord_ChangeCompanyIdentification)registroMRP).Cpf_cnpj;
                                    dr["CEI"] = ((MRPRecord_ChangeCompanyIdentification)registroMRP).Cei;
                                    dr["RazaoSocial"] = ((MRPRecord_ChangeCompanyIdentification)registroMRP).Name.Trim();
                                    dr["Endereco"] = ((MRPRecord_ChangeCompanyIdentification)registroMRP).Address.Trim();
                                    dr["CPF"] = ((MRPRecord_ChangeCompanyIdentification)registroMRP).CPF.Trim();
                                    this._dtAlteracaoEmpregador.Rows.Add(dr);
                                }
                                else if (registroMRP is MRPRecord_ChangeEmployee)
                                {
                                    DataRow dr = this._dtAlteracaoEmpregado.NewRow();
                                    dr["NSR"] = ((MRPRecord_ChangeEmployee)registroMRP).NSR;
                                    dr["DataHoraAlteracao"] = ((MRPRecord_ChangeEmployee)registroMRP).DateTimeRecordingRegistry;

                                    switch (((MRPRecord_ChangeEmployee)registroMRP).ChangeEmployeeType)
                                    {
                                        case ChangeEmployeeType.Alteration:
                                            dr["TipoAlteracao"] = "Alteração";
                                            break;
                                        case ChangeEmployeeType.Exclusion:
                                            dr["TipoAlteracao"] = "Exclusão";
                                            break;
                                        case ChangeEmployeeType.Inclusion:
                                            dr["TipoAlteracao"] = "Inclusão";
                                            break;
                                    }

                                    dr["PIS"] = ((MRPRecord_ChangeEmployee)registroMRP).Pis;
                                    dr["Nome"] = ((MRPRecord_ChangeEmployee)registroMRP).Name.Trim();
                                    dr["CPF"] = ((MRPRecord_ChangeEmployee)registroMRP).CPF.Trim();
                                    this._dtAlteracaoEmpregado.Rows.Add(dr);
                                }
                                else if (registroMRP is MRPRecord_RegistrationMarkingPoint)
                                {
                                    DataRow dr = this._dtMarcacoes.NewRow();
                                    dr["NSR"] = ((MRPRecord_RegistrationMarkingPoint)registroMRP).NSR;
                                    dr["DataHoraGravacao"] = ((MRPRecord_RegistrationMarkingPoint)registroMRP).DateTimeMarkingPoint;
                                    dr["PIS"] = ((MRPRecord_RegistrationMarkingPoint)registroMRP).Pis;
                                    this._dtMarcacoes.Rows.Add(dr);
                                }
                                else if (registroMRP is MRPRecord_SettingRealTimeClock)
                                {
                                    DataRow dr = this._dtAlteracaoDataHora.NewRow();
                                    dr["NSR"] = ((MRPRecord_SettingRealTimeClock)registroMRP).NSR;
                                    dr["DataHoraAnterior"] = ((MRPRecord_SettingRealTimeClock)registroMRP).DateTimeBeforeSetting;
                                    dr["DataHoraAtual"] = ((MRPRecord_SettingRealTimeClock)registroMRP).DateTimeSetting;
                                    dr["CPF"] = ((MRPRecord_SettingRealTimeClock)registroMRP).CPF;
                                    this._dtAlteracaoDataHora.Rows.Add(dr);
                                }
                                else if (registroMRP is MRPRecord_SensitiveEvent)
                                {
                                    DataRow dr = this._dtEventosSensiveis.NewRow();
                                    dr["NSR"] = ((MRPRecord_SensitiveEvent)registroMRP).NSR;
                                    dr["DataHoraEvento"] = ((MRPRecord_SensitiveEvent)registroMRP).EventDateTime;

                                    switch (((MRPRecord_SensitiveEvent)registroMRP).SensitiveEventType)
                                    {
                                        case ESensitiveEventType.EnergyReturn:
                                            dr["TipoEvento"] = "Retorno de energia";
                                            break;
                                        case ESensitiveEventType.EntryInViolationState:
                                            dr["TipoEvento"] = "Entrada em estado de violação";
                                            break;
                                        case ESensitiveEventType.PenDriveInsertionInFiscalPort:
                                            dr["TipoEvento"] = "Inserção de PenDrive em porta fiscal";
                                            break;
                                        case ESensitiveEventType.PenDriveRemovalFromFiscalPort:
                                            dr["TipoEvento"] = "Remoção de PenDrive de porta fiscal";
                                            break;
                                        case ESensitiveEventType.RIMEmission:
                                            dr["TipoEvento"] = "Emissão de RIM";
                                            break;
                                        case ESensitiveEventType.DamagedPrinter:
                                            dr["TipoEvento"] = "Impressora danificada";
                                            break;
                                    }

                                    this._dtEventosSensiveis.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Registro Inválido" + "\n" + registroMRP.ToString());
                            }
                        }

                        AddHistoricoComando(ETipoOrigemComando.Software,
                                            ETipoComando.ConfirmaRecebimentoRegistrosMRP,
                                            ETipoHistorico.Envio, "");

                        registrosMRP = this._watchComm.ConfirmationReceiptMRPRecords();

                        if (registrosMRP == null)

                            AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                ETipoComando.ConfirmaRecebimentoRegistrosMRP,
                                                ETipoHistorico.ConfirmacaoRecebimento,
                                                "O relógio não possui registros.");
                        else

                            AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                ETipoComando.ConfirmaRecebimentoRegistrosMRP,
                                                ETipoHistorico.ConfirmacaoRecebimento,
                                                "Registros coletados...");
                    }
                }

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.SolicitaRegistroMRP,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnLimparGrids_Click(object sender, EventArgs e)
        {
            _dtAlteracaoEmpregador = new dsSDKREP.dtMRPAlteracaoEmpregadorDataTable();
            _dtMarcacoes = new dsSDKREP.dtMRPMarcacaoDataTable();
            _dtAlteracaoEmpregado = new dsSDKREP.dtMRPEmpregadoDataTable();
            _dtAlteracaoDataHora = new dsSDKREP.dtMRPDataHoraDataTable();
            _dtEventosSensiveis = new dsSDKREP.dtMRPEventosSensiveisDataTable();

            this.dtgAlteracoesEmpregador.DataSource = _dtAlteracaoEmpregador;
            this.dtgMarcacoes.DataSource = _dtMarcacoes;
            this.dtgAlteracaoEmpregado.DataSource = _dtAlteracaoEmpregado;
            this.dtgAlteracaoDataHora.DataSource = _dtAlteracaoDataHora;
            this.dtgEventosSensiveis.DataSource = _dtEventosSensiveis;
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Status <---------------------"
        private void CarregaGridStatus(PrintPointStatusMessage status)
        {
            DataRow dr;

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Data e Hora";
            dr["Valor"] = status.DataAndTime.ToShortDateString() + " " + status.DataAndTime.ToShortTimeString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Horário de Verão (Data Inicial)";
            dr["Valor"] = status.DSTStart.ToShortDateString() + " " + status.DSTStart.ToShortTimeString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Horário de Verão (Data Final)";
            dr["Valor"] = status.DSTEnd.ToShortDateString() + " " + status.DSTEnd.ToShortTimeString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Utiliza 2 de 5 Intercalado";
            dr["Valor"] = status.Enabled_2Of5Intercalary ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Utiliza 2 de 5 Dimep";
            dr["Valor"] = status.Enabled_2of5Dimep ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Utiliza 2 de 9";
            dr["Valor"] = status.Enabled_3Of9 ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Utiliza Magnético Dimep";
            dr["Valor"] = status.Enabled_MagneticDIMEP ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Utiliza ABA";
            dr["Valor"] = status.Enabled_ABA ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Utiliza Wiegand 26 Bits";
            dr["Valor"] = status.Enabled_Wiegand26Bits ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Utiliza Wiegand 34 Bits";
            dr["Valor"] = status.Enabled_Wiegand34Bits ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Utiliza Wiegand 35 Bits";
            dr["Valor"] = status.Enabled_Wiegand35Bits ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Wiegand 37 Bits";
            switch (status.Wiegand37BitsUseType)
            {
                case EPrintPointWiegand37BitsUseType.NotUse:
                    dr["Valor"] = "Não Utiliza";
                    break;
                case EPrintPointWiegand37BitsUseType.DefaultH10302:
                    dr["Valor"] = "Padrão H10302";
                    break;
                case EPrintPointWiegand37BitsUseType.DefaultH10304:
                    dr["Valor"] = "Padrão H10304";
                    break;
                case EPrintPointWiegand37BitsUseType.Special1:
                    dr["Valor"] = "Padrão Especial 1";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Habilita paridade nas leituras Wiegand";
            dr["Valor"] = status.EnabledWiegandParityRead ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Utiliza Wiegand 32 Bits Especial";
            dr["Valor"] = status.Enabled_SpecialWiegand32Bits ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "SmartCard";
            switch (status.SmartCardUseType)
            {
                case EPrintPointSmartCardUseType.NotUse:
                    dr["Valor"] = "Não Utiliza";
                    break;
                case EPrintPointSmartCardUseType.ReadID:
                    dr["Valor"] = "Utiliza - Leitura do ID";
                    break;
                case EPrintPointSmartCardUseType.ReadRegistrationID:
                    dr["Valor"] = "Utiliza - Leitura da Matrícula";
                    break;
                case EPrintPointSmartCardUseType.ReadRegistrationIDHex:
                    dr["Valor"] = "Utiliza - Hexadecimal";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            if (status.SmartCardUseType == EPrintPointSmartCardUseType.ReadRegistrationID)
            {
                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Setor";
                dr["Valor"] = status.SmartCardSector;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Bloco";
                dr["Valor"] = status.SmartCardBlock;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "OffSet";
                dr["Valor"] = status.SmartCardOffSet;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Quantidade de Dígitos";
                dr["Valor"] = status.SmartCardDigitsNumber;
                this._dtStatus.Rows.Add(dr);
            }

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Formato 2 de 5 Intercalado";
            dr["Valor"] = status.Format_2Of5Intercalary;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Formato 2 de 5 Dimep";
            dr["Valor"] = status.Format_2of5Dimep;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Formato 3 de 9";
            dr["Valor"] = status.Format_3Of9;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Formato Magnético Dimep";
            dr["Valor"] = status.Format_MagneticDIMEP;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Formato ABA";
            dr["Valor"] = status.Format_ABA;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Formato Wiegand 26 Bits";
            dr["Valor"] = status.Format_Wiegand26Bits;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Formato Wiegand 34 Bits";
            dr["Valor"] = status.Format_Wiegand34Bits;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Formato Wiegand 35 Bits";
            dr["Valor"] = status.Format_Wiegand35Bits;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Formato Wiegand 37 Bits";
            dr["Valor"] = status.Format_Wiegand37Bits;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Formato SmartCard";
            dr["Valor"] = status.Format_SmartCard;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Tipo de Criptografia";
            switch (status.EncryptionType)
            {
                case EPrintPointEncryptionType.EightDigits:
                    dr["Valor"] = "Utiliza criptografia de 8 dígitos";
                    break;
                case EPrintPointEncryptionType.NoEncryption:
                    dr["Valor"] = "Não utiliza criptografia";
                    break;
                case EPrintPointEncryptionType.TwelveDigits:
                    dr["Valor"] = "Utiliza criptografia de 12 dígitos";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Vetor 1";
            dr["Valor"] = status.Vector1;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Vetor 2";
            dr["Valor"] = status.Vector2;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Verificador 1";
            dr["Valor"] = status.Checker1;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Verificador 2";
            dr["Valor"] = status.Checker2;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Utiliza formato especial magnético";
            dr["Valor"] = status.SpecialFormatMagneticDimep1 ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Utiliza formato especial ABA";
            dr["Valor"] = status.SpecialFormatABA1 ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Tipo de Personalização";
            switch (status.PersonalizationType)
            {
                case EPrintPointPersonalizationType.Megapoint:
                    dr["Valor"] = "Megapoint";
                    break;
                case EPrintPointPersonalizationType.Micropoint:
                    dr["Valor"] = "Micropoint";
                    break;
                case EPrintPointPersonalizationType.NotUse:
                    dr["Valor"] = "Não utiliza";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            if (status.PersonalizationType != EPrintPointPersonalizationType.NotUse)
            {
                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Tamanho do cartão personalizado";
                dr["Valor"] = status.PersonalizationDigitsNumber.ToString();
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Código de personalização";
                dr["Valor"] = status.PersonalizationCode.ToString();
                this._dtStatus.Rows.Add(dr);
            }

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Cartão - Habilitado";
            dr["Valor"] = status.Card_Enabled ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Cartão - Credencial ou PIS";
            dr["Valor"] = status.Card_AccessType == EPrintPointAccessType.Credential ? "Credencial" : "PIS";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Cartão - Autenticação";
            switch (status.Card_AuthenticationType)
            {
                case EPrintPointAuthenticationType.BiometricAndPassword:
                    dr["Valor"] = "Biometria e Senha";
                    break;
                case EPrintPointAuthenticationType.BiometricOrPassword:
                    dr["Valor"] = "Biometria ou Senha";
                    break;
                case EPrintPointAuthenticationType.NoAuthentication:
                    dr["Valor"] = "Não possuí autenticação";
                    break;
                case EPrintPointAuthenticationType.OnlyBiometrics:
                    dr["Valor"] = "Apenas Biometria";
                    break;
                case EPrintPointAuthenticationType.OnlyPassword:
                    dr["Valor"] = "Apenas Senha";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Cartão - Nº de dígitos da crendecial para exibir no display";
            dr["Valor"] = dr["Valor"] = status.ShowCredentialDigitsInDisplay.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Teclado - Habilitado";
            dr["Valor"] = dr["Valor"] = status.KeyBoard_Enabled ? "Sim" : "Não"; ;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Teclado - Credencial ou PIS";
            dr["Valor"] = status.KeyBoard_AccessType == EPrintPointAccessType.Credential ? "Credencial" : "PIS";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Teclado - Autenticação";
            switch (status.KeyBoard_AuthenticationType)
            {
                case EPrintPointAuthenticationType.BiometricAndPassword:
                    dr["Valor"] = "Biometria e Senha";
                    break;
                case EPrintPointAuthenticationType.BiometricOrPassword:
                    dr["Valor"] = "Biometria ou Senha";
                    break;
                case EPrintPointAuthenticationType.NoAuthentication:
                    dr["Valor"] = "Não possuí autenticação";
                    break;
                case EPrintPointAuthenticationType.OnlyBiometrics:
                    dr["Valor"] = "Apenas Biometria";
                    break;
                case EPrintPointAuthenticationType.OnlyPassword:
                    dr["Valor"] = "Apenas Senha";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Identificação - 1 p/ N Habilitado";
            dr["Valor"] = status.Identification_Enabled ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Identificação - Autenticação";
            switch (status.Identification_AuthenticationTypeIdentification)
            {
                case EPrintPointAuthenticationTypeIdentification.NoAuthentication:
                    dr["Valor"] = "Não pede autenticação";
                    break;
                case EPrintPointAuthenticationTypeIdentification.OnlyPassword:
                    dr["Valor"] = "Apenas senha";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Autenticação";
            switch (status.Authentication)
            {
                case EPrintPointBiometricAuthenticationType.Always:
                    dr["Valor"] = "Sempre";
                    break;
                case EPrintPointBiometricAuthenticationType.Partial:
                    dr["Valor"] = "Parcial";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade de Funcionários";
            dr["Valor"] = status.EmployeesCapacity.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Ocupação de Funcionários";
            dr["Valor"] = status.EmployeesOccupation.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade de Credenciais";
            dr["Valor"] = status.CredentialsCapacity.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade do Módulo Biométrico";
            dr["Valor"] = status.BiometricModuleCapacity.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Ocupação de Credenciais";
            dr["Valor"] = status.CredentialsOccupation.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade de Templates";
            dr["Valor"] = status.FingerPrintCapacity.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Ocupação de Templates";
            dr["Valor"] = status.FingerPrintOccupation.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Ocupação da MRP em % de Cluster";
            dr["Valor"] = status.MRPOccupationInClustersPercentage.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Último NSR";
            dr["Valor"] = status.FinallyNSR;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Total de registros de ponto";
            dr["Valor"] = status.TotalRecordsPoint;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Quantidade de registros de ponto não recolhidos";
            dr["Valor"] = status.RecordsPointToCollect;
            this._dtStatus.Rows.Add(dr);
            
            // REP III
            if (cboModelo.SelectedIndex == 2)
            {
                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Número sequencial do evento de sistema atual";
                dr["Valor"] = status.CurrentSystemEventSequentialNumber;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Número sequencial do último evento de sistema recolhido";
                dr["Valor"] = status.LastSystemEventSequentialNumberCollected;
                this._dtStatus.Rows.Add(dr);
            }

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Capacidade de Supervisores";
            dr["Valor"] = status.MasterCapacity.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Ocupação de Supervisores";
            dr["Valor"] = status.MasterOccupation.ToString();
            this._dtStatus.Rows.Add(dr);

            // REP III
            if (cboModelo.SelectedIndex == 2)
            {
                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Umidade compartimento de papel";
                dr["Valor"] = status.PaperCompartmentHumidity;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Temperatura compartimento de papel";
                dr["Valor"] = status.PaperCompartmentTemperature;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Temperatura placa";
                dr["Valor"] = status.BoardTemperature;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Aceleração eixo x";
                dr["Valor"] = status.XAxisAcceleration;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Aceleração eixo y";
                dr["Valor"] = status.YAxisAcceleration;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Aceleração eixo z";
                dr["Valor"] = status.ZAxisAcceleration;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Status Sensor de Temperatura e Umidade do Papel";
                switch (status.PaperCompartmentHumidityTemperatureSensor)
                {
                    case EEnabledSensorType.NotConfigured:
                        dr["Valor"] = "Não configurado";
                        break;
                    case EEnabledSensorType.Enabled:
                        dr["Valor"] = "Habilitado";
                        break;
                    case EEnabledSensorType.Disabled:
                        dr["Valor"] = "Desabilitado";
                        break;
                    default:
                        dr["Valor"] = "-";
                        break;
                }
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Status Sensor de Temperatura da Placa";
                switch (status.BoardTemperatureSensor)
                {
                    case EEnabledSensorType.NotConfigured:
                        dr["Valor"] = "Não configurado";
                        break;
                    case EEnabledSensorType.Enabled:
                        dr["Valor"] = "Habilitado";
                        break;
                    case EEnabledSensorType.Disabled:
                        dr["Valor"] = "Desabilitado";
                        break;
                    default:
                        dr["Valor"] = "-";
                        break;
                }
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Status Acelerômetro";
                switch (status.AccelerationSensor)
                {
                    case EEnabledSensorType.NotConfigured:
                        dr["Valor"] = "Não configurado";
                        break;
                    case EEnabledSensorType.Enabled:
                        dr["Valor"] = "Habilitado";
                        break;
                    case EEnabledSensorType.Disabled:
                        dr["Valor"] = "Desabilitado";
                        break;
                    default:
                        dr["Valor"] = "-";
                        break;
                }
                this._dtStatus.Rows.Add(dr);
            }

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Nº de Série do REP";
            dr["Valor"] = status.SerialNumber;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Nº de Série da Placa";
            dr["Valor"] = status.SerialNumberPlate;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Nº de Série da MRP";
            dr["Valor"] = status.MRPSerialNumber;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Nº de Lacre da MRP";
            dr["Valor"] = status.MRPSealNumber;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Versão do Firmware";
            dr["Valor"] = status.FirmwareVersion;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Versão do Firmware da MRP";
            dr["Valor"] = status.MRPFirmwareVersion;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "MAC Address";
            dr["Valor"] = status.MACAddress;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Tamanho do Avanço de Impressão";
            switch (status.PrinterAdvanceSize)
            {
                case EPrintPointAdvanceSizeType.Long:
                    dr["Valor"] = "Longo";
                    break;
                case EPrintPointAdvanceSizeType.Medium:
                    dr["Valor"] = "Médio";
                    break;
                case EPrintPointAdvanceSizeType.Small:
                    dr["Valor"] = "Pequeno";
                    break;
            }

            this._dtStatus.Rows.Add(dr);


            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Nível de Segurança Suprema";
            dr["Valor"] = status.SecurityLevelSuprema;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Nível de Segurança Sagem";
            dr["Valor"] = status.SecurityLevelSagem;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Reconhecimendo Biométrico a 180º (Sagem)";
            dr["Valor"] = status.BiometricRecognition180 ? "Sim" : "Não";
            this._dtStatus.Rows.Add(dr);
            
            // REP III
            if (cboModelo.SelectedIndex == 2)
            {
                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Nível de Segurança Virdi";
                dr["Valor"] = status.SecurityLevelVirdi;
                this._dtStatus.Rows.Add(dr);
            }

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Energia Impressora";
            switch (status.EnergyPrinter)
            {
                case EPrintPointEnergyPrinter.Regular:
                    dr["Valor"] = "Normal";
                    break;
                case EPrintPointEnergyPrinter.Reduced:
                    dr["Valor"] = "Reduzida";
                    break;
                case EPrintPointEnergyPrinter.Elevated:
                    dr["Valor"] = "Elevada";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Tamanho da Bobina";
            dr["Valor"] = status.LenghtBobbin;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Nivel da Bateria";
            switch (status.BatteryLevel)
            {
                case BatteryLevel.Regular:
                    dr["Valor"] = "Regular";
                    break;
                case BatteryLevel.Small:
                    dr["Valor"] = "Pequeno";
                    break;
                case BatteryLevel.VerySmall:
                    dr["Valor"] = "Muito Pequeno";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Tempo de uso da bateria.";
            dr["Valor"] = status.TimeUtilyBattery.ToString() + " hs";
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Tipo do Corte";
            switch (status.PrinterCutType)
            {
                case EPrintPointCutType.Partial:
                    dr["Valor"] = "Parcial";
                    break;
                case EPrintPointCutType.Total:
                    dr["Valor"] = "Total";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Estado da Bobina";
            switch (status.BobbinState)
            {
                case BobbinStateType.LittlePaper:
                case BobbinStateType.VeryLittlePaper:
                    dr["Valor"] = "Pouco Papel";
                    break;
                case BobbinStateType.NoPaper:
                    dr["Valor"] = "Sem Papel";
                    break;
                case BobbinStateType.OK:
                    dr["Valor"] = "OK";
                    break;
                default:
                    dr["Valor"] = "Desconhecido";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Estado da MRP";
            switch (status.MRPState)
            {
                case MRPStateType.Error:
                    dr["Valor"] = "Erro";
                    break;
                case MRPStateType.Initializing:
                    dr["Valor"] = "Inicializando";
                    break;
                case MRPStateType.NormalOperation:
                    dr["Valor"] = "Operação normal";
                    break;
                case MRPStateType.WithoutEmployer:
                    dr["Valor"] = "Sem empregador";
                    break;
                case MRPStateType.WithoutInitialActivation:
                    dr["Valor"] = "Sem ativação inicial";
                    break;
                case MRPStateType.SDCardError:
                    dr["Valor"] = "MRP com erro no SDCard";
                    break;
                default:
                    dr["Valor"] = "Desconhecido";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Estado do REP";
            switch (status.REPState)
            {
                case REPStateType.Maintenance:
                    dr["Valor"] = "Em manutenção";
                    break;
                case REPStateType.NormalOperation:
                    dr["Valor"] = "Operação Normal";
                    break;
                default:
                    dr["Valor"] = "Desconhecido";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Possui marcações?";
            dr["Valor"] = (status.HasMarkingPoints == true ? "Sim" : "Não");
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Possui novos templates?";
            dr["Valor"] = (status.HasNewFingerprints == true ? "Sim" : "Não");
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Gerando AFD?";
            dr["Valor"] = (status.AFDGeneration == AFDGenerationType.Generating ? "Sim" : "Não");
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Gerando RIM?";
            dr["Valor"] = (status.RIMGeneration == RIMGenerationType.Generating ? "Sim" : "Não");
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Tipo de Alimentação";
            switch (status.AlimentationType)
            {
                case AlimentationType.ACAlimentation:
                    dr["Valor"] = "Alimentação AC";
                    break;
                case AlimentationType.BatteryAlimentation:
                    dr["Valor"] = "Alimentação por Bateria";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Modelo do Firmware";
            switch (status.FirmwareType)
            {
                case EFirmwareType.REP:
                    dr["Valor"] = "REP";
                    break;
                case EFirmwareType.MiniREPSagem:
                    dr["Valor"] = "Mini REP Sagem";
                    break;
                case EFirmwareType.MiniREPSuprema:
                    dr["Valor"] = "Mini REP Suprema";
                    break;
                case EFirmwareType.REPHome:
                    dr["Valor"] = "REP Home";
                    break;
                case EFirmwareType.REPIII:
                    dr["Valor"] = "REP III";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Inicio registro AFD 1";
            dr["Valor"] = status.LastAFDStart1;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Fim registro AFD 1";
            dr["Valor"] = status.LastAFDEnd1;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Inicio registro AFD 2";
            dr["Valor"] = status.LastAFDStart2;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Fim registro AFD 2";
            dr["Valor"] = status.LastAFDEnd2;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Inicio registro AFD 3";
            dr["Valor"] = status.LastAFDStart3;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Fim registro AFD 3";
            dr["Valor"] = status.LastAFDEnd3;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Inicio registro RIM 1";
            dr["Valor"] = status.LastRIMStart1;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Fim registro RIM 1";
            dr["Valor"] = status.LastRIMEnd1;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Inicio registro RIM 2";
            dr["Valor"] = status.LastRIMStart2;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Fim registro RIM 2";
            dr["Valor"] = status.LastRIMEnd2;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Inicio registro RIM 3";
            dr["Valor"] = status.LastRIMStart3;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Fim registro RIM 3";
            dr["Valor"] = status.LastRIMEnd3;
            this._dtStatus.Rows.Add(dr);

            // REP III
            if (cboModelo.SelectedIndex == 2)
            {
                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Tensão de alimentação";
                dr["Valor"] = status.PowerSupplyVoltage;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Fonte de alimentação";
                switch (status.PowerSupplyType)
                {
                    case EPowerSupplyType.PowerGrid:
                        dr["Valor"] = "Alimentação na rede elétrica";
                        break;
                    case EPowerSupplyType.NoBreak:
                        dr["Valor"] = "Alimentação por nobreak";
                        break;
                }
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Tensão na bateria do nobreak";
                dr["Valor"] = status.NoBreakBatteryVoltage;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Percentual de bateria restante (nobreak)";
                dr["Valor"] = status.NoBreakBatteryPercentage;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Estimativa em minutos do tempo de uso da bateria do RTC";
                dr["Valor"] = status.RtcBatteryMinutesUsage;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "REP em manutenção";
                dr["Valor"] = (status.InMaintenance ? "Sim" : "Não");
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Violação por queda de energia no microcontrolador do sensor de gabinete";
                dr["Valor"] = (status.ViolationPowerOutageSensorCaseMicrocontroller ? "Sim" : "Não");
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Violação por reset no pino SWIM (gravação do firmware)";
                dr["Valor"] = (status.ViolationResetSWIMPin ? "Sim" : "Não");
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Violação forçada pelo microcontrolador principal";
                dr["Valor"] = (status.ViolationForcedMainMicrocontroller ? "Sim" : "Não");
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Violação pelo sensor 1";
                dr["Valor"] = (status.ViolationSensor1 ? "Sim" : "Não");
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Violação pelo sensor 2";
                dr["Valor"] = (status.ViolationSensor2 ? "Sim" : "Não");
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Violação por primeira execução do firmware no microcontrolador do sensor de gabinete";
                dr["Valor"] = (status.ViolationFirstExecutionSensorCaseMicrocontroller ? "Sim" : "Não");
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Violação por queda de comunicação entre o microcontrolador principal e o microcontrolador do sensor de gabinete";
                dr["Valor"] = (status.ViolationLostCommunicationMainMicrocontrollerSensorCaseMicrocontroller ? "Sim" : "Não");
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Status do sensor de violação 1";
                dr["Valor"] = (status.Sensor1Violated ? "Aberto" : "Fechado");
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Status do sensor de violação 2";
                dr["Valor"] = (status.Sensor2Violated ? "Aberto" : "Fechado");
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Sensor Biométrico";
                switch (status.BiometricSensor)
                {
                    case EBiometricSensorType.Undefined:
                        dr["Valor"] = "Não definido";
                        break;
                    case EBiometricSensorType.None:
                        dr["Valor"] = "Nenhum";
                        break;
                    default:
                        dr["Valor"] = status.BiometricSensor.ToString();
                        break;
                }
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Versão do firmware do BootLoader";
                dr["Valor"] = status.BootLoaderFirmwareVersion;
                this._dtStatus.Rows.Add(dr);

                dr = this._dtStatus.NewRow();
                dr["Propriedade"] = "Versão do firmware do Sensor Biométrico";
                dr["Valor"] = status.BiometricSensorFirmwareVersion;
                this._dtStatus.Rows.Add(dr);
            }
        }

        private void CarregaGridImmediateStatus(ImmediateStatusResponse immediateStatus)
        {
            DataRow dr;

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Estado da Bobina";
            switch (immediateStatus.BobbinState)
            {
                case BobbinStateType.LittlePaper:
                    dr["Valor"] = "Pouco Papel";
                    break;
                case BobbinStateType.NoPaper:
                    dr["Valor"] = "Sem Papel";
                    break;
                case BobbinStateType.OK:
                    dr["Valor"] = "OK";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Estado da MRP";
            switch (immediateStatus.MRPState)
            {
                case MRPStateType.NormalOperation:
                    dr["Valor"] = "Operação Normal";
                    break;
                case MRPStateType.WithoutEmployer:
                    dr["Valor"] = "Sem empregador";
                    break;
                case MRPStateType.WithoutInitialActivation:
                    dr["Valor"] = "Sem ativação inicial";
                    break;
                case MRPStateType.Error:
                    dr["Valor"] = "Erro";
                    break;
                case MRPStateType.Initializing:
                    dr["Valor"] = "Inicializando";
                    break;
                case MRPStateType.SDCardError:
                    dr["Valor"] = "MRP com erro no SDCard";
                    break;
                case MRPStateType.Unknown:
                    dr["Valor"] = "Desconhecido";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Estado do REP";
            switch (immediateStatus.RepState)
            {
                case REPStateType.Maintenance:
                    dr["Valor"] = "Em manutenção";
                    break;
                case REPStateType.NormalOperation:
                    dr["Valor"] = "Operação Normal";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Possui marcações?";
            dr["Valor"] = (immediateStatus.HasMarkingPoints == true ? "Sim" : "Não");
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Possui novos templates?";
            dr["Valor"] = (immediateStatus.HasNewFingerprints == true ? "Sim" : "Não");
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Gerando AFD?";
            dr["Valor"] = (immediateStatus.AFDGeneration == AFDGenerationType.Generating ? "Sim" : "Não");
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Gerando RIM?";
            dr["Valor"] = (immediateStatus.RIMGeneration == RIMGenerationType.Generating ? "Sim" : "Não");
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Tipo de Alimentação";
            switch (immediateStatus.AlimentationType)
            {
                case AlimentationType.ACAlimentation:
                    dr["Valor"] = "Alimentação AC";
                    break;
                case AlimentationType.BatteryAlimentation:
                    dr["Valor"] = "Alimentação por Bateria";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Nivel da Bateria";
            switch (immediateStatus.BatteryLevel)
            {
                case BatteryLevel.Regular:
                    dr["Valor"] = "Regular";
                    break;
                case BatteryLevel.Small:
                    dr["Valor"] = "Pequeno";
                    break;
                case BatteryLevel.VerySmall:
                    dr["Valor"] = "Muito Pequeno";
                    break;
            }

            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Tempo de utilização da bateria";
            dr["Valor"] = immediateStatus.TimeUtilyBattery.ToString();
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Modelo do Firmware";
            switch (immediateStatus.FirmwareType)
            {
                case EFirmwareType.REP:
                    dr["Valor"] = "REP";
                    break;
                case EFirmwareType.MiniREPSagem:
                    dr["Valor"] = "Mini REP Sagem";
                    break;
                case EFirmwareType.MiniREPSuprema:
                    dr["Valor"] = "Mini REP Suprema";
                    break;
                case EFirmwareType.REPHome:
                    dr["Valor"] = "REP Home";
                    break;
                case EFirmwareType.REPIII:
                    dr["Valor"] = "REP III";
                    break;
            }
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Inicio registro AFD 1";
            dr["Valor"] = immediateStatus.LastAFDStart1;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Fim registro AFD 1";
            dr["Valor"] = immediateStatus.LastAFDEnd1;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Inicio registro AFD 2";
            dr["Valor"] = immediateStatus.LastAFDStart2;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Fim registro AFD 2";
            dr["Valor"] = immediateStatus.LastAFDEnd2;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Inicio registro AFD 3";
            dr["Valor"] = immediateStatus.LastAFDStart3;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Fim registro AFD 3";
            dr["Valor"] = immediateStatus.LastAFDEnd3;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Inicio registro RIM 1";
            dr["Valor"] = immediateStatus.LastRIMStart1;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Fim registro RIM 1";
            dr["Valor"] = immediateStatus.LastRIMEnd1;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Inicio registro RIM 2";
            dr["Valor"] = immediateStatus.LastRIMStart2;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Fim registro RIM 2";
            dr["Valor"] = immediateStatus.LastRIMEnd2;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Inicio registro RIM 3";
            dr["Valor"] = immediateStatus.LastRIMStart3;
            this._dtStatus.Rows.Add(dr);

            dr = this._dtStatus.NewRow();
            dr["Propriedade"] = "Fim registro RIM 3";
            dr["Valor"] = immediateStatus.LastRIMEnd3;
            this._dtStatus.Rows.Add(dr);
        }

        private void btnObterStatus_Click(object sender, EventArgs e)
        {
            this._dtStatus = new dsSDKREP.dtStatusDataTable();
            this.dtgStatus.DataSource = this._dtStatus;

            if (!InstanciaWatchComm()) return;

            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.PedeStatusEquipamento,
                                    ETipoHistorico.Envio, "");

                this._watchComm.OpenConnection();

                PrintPointStatusMessage status = this._watchComm.GetPrintPointStatus();

                if (status == null)
                {
                    MessageBox.Show("O comando de status não foi recepcionado corretamente!", Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

                CarregaGridStatus(status);

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.PedeStatusEquipamento,
                                    ETipoHistorico.ConfirmacaoRecebimento, "");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.PedeStatusEquipamento,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnObterStatusImediato_Click(object sender, EventArgs e)
        {
            this._dtStatus = new dsSDKREP.dtStatusDataTable();
            this.dtgStatus.DataSource = this._dtStatus;

            if (!InstanciaWatchComm()) return;

            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.PedeStatusImediatoEquipamento,
                                    ETipoHistorico.Envio, "");

                this._watchComm.OpenConnection();

                ImmediateStatusResponse immediateStatus = this._watchComm.GetImmediateStatus();

                if (immediateStatus == null)
                {
                    MessageBox.Show("O comando de status imediato não foi recepcionado corretamente!", Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

                CarregaGridImmediateStatus(immediateStatus);

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.PedeStatusImediatoEquipamento,
                                    ETipoHistorico.ConfirmacaoRecebimento, "");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.PedeStatusEquipamento,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Configurações <---------------------"
        private void chk2de5Intercalado_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();

            this.chkUtilizaCriptografia.Enabled = this.chk2de5Intercalado.Checked;

            this.txtVetor1.Enabled = this.txtVerificador1.Enabled = this.txtVetor2.Enabled = this.txtVerificador2.Enabled =
                this.optCriptografia8Digitos.Enabled = this.optCriptografia12Digitos.Enabled = this.optCriptografia10Digitos.Enabled = (this.chkUtilizaCriptografia.Checked && this.chk2de5Intercalado.Checked);
        }

        private void chk2de5Dimep_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();
        }

        private void chk3de9_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();
        }

        private void chkMagneticoDimep_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();
            this.chkFormatoEspecialMagneticoDimep.Enabled = this.chkMagneticoDimep.Checked;
        }

        private void chkABA_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();
            this.chkFormatoEspecialABA.Enabled = this.chkABA.Checked;
        }

        private void chkWiegand_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();
        }

        private void chkWiegand34Bits_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();
        }

        private void chkEan13_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();
        }

        private void chkSmartCard_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();
            this.optLeID.Enabled = this.optLeMatricula.Enabled = this.chkSmartCard.Checked;

            this.txtSetor.Enabled = this.txtBloco.Enabled = this.txtOffSet.Enabled = this.txtQtdDigitosMatriculaSmart.Enabled =
               this.txtChaveSmart.Enabled = (this.optLeMatricula.Checked && this.chkSmartCard.Checked);
        }

        private void chkWiegand35Bits_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();
        }

        private void chkWiegand37Bits_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();

            this.HabilitaDesabilitaFormatoCartao();
            this.rdoPadraoH10302.Enabled = this.rdoPadraoH10304.Enabled = this.rdoPadraoW37Especial1.Enabled = this.chkWiegand37Bits.Checked;
        }

        private void chkFormatoEspecialMagneticoDimep_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();
        }

        private void chkFormatoEspecialABA_CheckedChanged(object sender, EventArgs e)
        {
            this.HabilitaDesabilitaFormatoCartao();
        }

        private void HabilitaDesabilitaFormatoCartao()
        {
            this.btnFormato2de5Intercalado.Enabled = this.chk2de5Intercalado.Checked;
            this.btnFormato2de5Dimep.Enabled = this.chk2de5Dimep.Checked;
            this.btnFormato3de9.Enabled = this.chk3de9.Checked;
            this.btnFormatoMagneticoDimep.Enabled = (this.chkMagneticoDimep.Checked && this.chkFormatoEspecialMagneticoDimep.Checked == false);
            this.btnFormatoABA.Enabled = (this.chkABA.Checked && this.chkFormatoEspecialABA.Checked == false);
            this.btnFormatoWiegand26Bits.Enabled = this.chkWiegand26Bits.Checked;
            this.btnFormatoSmartCard.Enabled = this.chkSmartCard.Checked;
            this.btnFormatoWiegand34Bits.Enabled = this.chkWiegand34Bits.Checked;
            this.btnFormatoEan13.Enabled = this.chkEan13.Checked;
            this.btnFormatoWiegand35Bits.Enabled = this.chkWiegand35Bits.Checked;
            this.btnFormatoWiegand37Bits.Enabled = this.chkWiegand37Bits.Checked;

        }

        private void chkUtilizaCriptografia_CheckedChanged(object sender, EventArgs e)
        {
            this.txtVetor1.Enabled = this.txtVerificador1.Enabled = this.txtVetor2.Enabled = this.txtVerificador2.Enabled =
                this.optCriptografia8Digitos.Enabled = this.optCriptografia12Digitos.Enabled = this.optCriptografia10Digitos.Enabled = (this.chkUtilizaCriptografia.Checked && this.chk2de5Intercalado.Checked);
        }

        private void chkLeituraViaTecladoHabilitada_CheckedChanged(object sender, EventArgs e)
        {
            this.optTeclado_Credencial.Enabled = this.optTeclado_Pis.Enabled = this.optTeclado_NaoPedeAutenticacao.Enabled =
            this.optTeclado_ApenasSenha.Enabled = this.optTeclado_ApenasBiometria.Enabled = this.optTeclado_BiometriaOuSenha.Enabled =
            this.optTeclado_Ambos.Enabled = this.chkLeituraViaTecladoHabilitada.Checked;
        }

        private void chkLeituraViaCartaoHabilitada_CheckedChanged(object sender, EventArgs e)
        {
            this.optCartao_Credencial.Enabled = this.optCartao_Pis.Enabled = this.optCartao_NaoPedeAutenticacao.Enabled =
            this.optCartao_ApenasSenha.Enabled = this.optCartao_ApenasBiometria.Enabled = this.optCartao_BiometriaOuSenha.Enabled =
            this.optCartao_Ambos.Enabled = this.chkLeituraViaCartaoHabilitada.Checked;
        }

        private void chkHabilita1ParaN_CheckedChanged(object sender, EventArgs e)
        {
            this.optIdentificacao_NaoPedeAutenticacao.Enabled = this.optIdentificacao_ApenasSenha.Enabled =
            this.chkHabilita1ParaN.Checked;
        }

        private void btnFormato2de5Intercalado_Click(object sender, EventArgs e)
        {
            frmFormatoCartao frmFormatoCartao = new frmFormatoCartao(this.txtFormato2de5Intercalado.Text);
            frmFormatoCartao.ShowDialog();
            this.txtFormato2de5Intercalado.Text = frmFormatoCartao.FormatoCartao;
            frmFormatoCartao.Dispose();
            frmFormatoCartao = null;
        }

        private void btnFormato2de5Dimep_Click(object sender, EventArgs e)
        {
            frmFormatoCartao frmFormatoCartao = new frmFormatoCartao(this.txtFormato2de5Dimep.Text);
            frmFormatoCartao.ShowDialog();
            this.txtFormato2de5Dimep.Text = frmFormatoCartao.FormatoCartao;
            frmFormatoCartao.Dispose();
            frmFormatoCartao = null;
        }

        private void btnFormato3de9_Click(object sender, EventArgs e)
        {
            frmFormatoCartao frmFormatoCartao = new frmFormatoCartao(this.txtFormato3de9.Text);
            frmFormatoCartao.ShowDialog();
            this.txtFormato3de9.Text = frmFormatoCartao.FormatoCartao;
            frmFormatoCartao.Dispose();
            frmFormatoCartao = null;
        }

        private void btnFormatoMagneticoDimep_Click(object sender, EventArgs e)
        {
            frmFormatoCartao frmFormatoCartao = new frmFormatoCartao(this.txtFormatoMagneticoDimep.Text);
            frmFormatoCartao.ShowDialog();
            this.txtFormatoMagneticoDimep.Text = frmFormatoCartao.FormatoCartao;
            frmFormatoCartao.Dispose();
            frmFormatoCartao = null;
        }

        private void btnFormatoABA_Click(object sender, EventArgs e)
        {
            frmFormatoCartao frmFormatoCartao = new frmFormatoCartao(this.txtFormatoABA.Text);
            frmFormatoCartao.ShowDialog();
            this.txtFormatoABA.Text = frmFormatoCartao.FormatoCartao;
            frmFormatoCartao.Dispose();
            frmFormatoCartao = null;
        }

        private void btnFormatoWiegand_Click(object sender, EventArgs e)
        {
            frmFormatoCartao frmFormatoCartao = new frmFormatoCartao(this.txtFormatoWiegand26Bits.Text);
            frmFormatoCartao.ShowDialog();
            this.txtFormatoWiegand26Bits.Text = frmFormatoCartao.FormatoCartao;
            frmFormatoCartao.Dispose();
            frmFormatoCartao = null;
        }

        private void btnFormatoWiegand34Bits_Click(object sender, EventArgs e)
        {
            frmFormatoCartao frmFormatoCartao = new frmFormatoCartao(this.txtFormatoWiegand34Bits.Text);
            frmFormatoCartao.ShowDialog();
            this.txtFormatoWiegand34Bits.Text = frmFormatoCartao.FormatoCartao;
            frmFormatoCartao.Dispose();
            frmFormatoCartao = null;
        }

        private void btnFormatoEan13_Click(object sender, EventArgs e)
        {
            frmFormatoCartao frmFormatoCartao = new frmFormatoCartao(this.txtFormatoEan13.Text);
            frmFormatoCartao.ShowDialog();
            this.txtFormatoEan13.Text = frmFormatoCartao.FormatoCartao;
            frmFormatoCartao.Dispose();
            frmFormatoCartao = null;
        }

        private void btnFormatoSmartCard_Click(object sender, EventArgs e)
        {
            frmFormatoCartao frmFormatoCartao = new frmFormatoCartao(this.txtFormatoSmartCard.Text);
            frmFormatoCartao.ShowDialog();
            this.txtFormatoSmartCard.Text = frmFormatoCartao.FormatoCartao;
            frmFormatoCartao.Dispose();
            frmFormatoCartao = null;
        }

        private void btnFormatoWiegand35Bits_Click(object sender, EventArgs e)
        {
            frmFormatoCartao frmFormatoCartao = new frmFormatoCartao(this.txtFormatoWiegand35Bits.Text);
            frmFormatoCartao.ShowDialog();
            this.txtFormatoWiegand35Bits.Text = frmFormatoCartao.FormatoCartao;
            frmFormatoCartao.Dispose();
            frmFormatoCartao = null;
        }

        private void btnFormatoWiegand37Bits_Click(object sender, EventArgs e)
        {
            frmFormatoCartao frmFormatoCartao = new frmFormatoCartao(this.txtFormatoWiegand37Bits.Text);
            frmFormatoCartao.ShowDialog();
            this.txtFormatoWiegand37Bits.Text = frmFormatoCartao.FormatoCartao;
            frmFormatoCartao.Dispose();
            frmFormatoCartao = null;
        }

        private void CarregaTelaConfiguracoes(PrintPointStatusMessage status)
        {
            // Tipo de Leitura.
            this.chk2de5Intercalado.Checked = status.Enabled_2Of5Intercalary;

            if (status.Enabled_2Of5Intercalary)
                this.txtFormato2de5Intercalado.Text = status.Format_2Of5Intercalary;

            this.chk2de5Dimep.Checked = status.Enabled_2of5Dimep;

            if (status.Enabled_2of5Dimep)
                this.txtFormato2de5Dimep.Text = status.Format_2of5Dimep;

            this.chk3de9.Checked = status.Enabled_3Of9;

            if (status.Enabled_3Of9)
                this.txtFormato3de9.Text = status.Format_3Of9;

            this.chkMagneticoDimep.Checked = status.Enabled_MagneticDIMEP;

            if (status.Enabled_MagneticDIMEP)
                this.txtFormatoMagneticoDimep.Text = status.Format_MagneticDIMEP;

            this.chkFormatoEspecialMagneticoDimep.Checked = status.SpecialFormatMagneticDimep1;

            this.chkABA.Checked = status.Enabled_ABA;

            if (status.Enabled_ABA)
                this.txtFormatoABA.Text = status.Format_ABA;

            this.chkFormatoEspecialABA.Checked = status.SpecialFormatABA1;

            this.chkWiegand26Bits.Checked = status.Enabled_Wiegand26Bits;

            if (status.Enabled_Wiegand26Bits)
                this.txtFormatoWiegand26Bits.Text = status.Format_Wiegand26Bits;

            this.chkWiegand34Bits.Checked = status.Enabled_Wiegand34Bits;

            if (status.Enabled_Wiegand34Bits)
                this.txtFormatoWiegand34Bits.Text = status.Format_Wiegand34Bits;

            this.chkEan13.Checked = status.Enabled_Ean13;

            if (status.Enabled_Ean13)
                this.txtFormatoEan13.Text = status.Format_Ean13;

            this.chkWiegand35Bits.Checked = status.Enabled_Wiegand35Bits;

            if (status.Enabled_Wiegand35Bits)
                this.txtFormatoWiegand35Bits.Text = status.Format_Wiegand35Bits;

            this.chkWiegand32BitsEspecial.Checked = status.Enabled_SpecialWiegand32Bits;

            if (status.Wiegand37BitsUseType == EPrintPointWiegand37BitsUseType.NotUse)
                this.chkWiegand37Bits.Checked = false;
            else
            {
                this.chkWiegand37Bits.Checked = true;

                if (status.Wiegand37BitsUseType == EPrintPointWiegand37BitsUseType.DefaultH10302)
                    this.rdoPadraoH10302.Checked = true;
                else if (status.Wiegand37BitsUseType == EPrintPointWiegand37BitsUseType.DefaultH10304)
                    this.rdoPadraoH10304.Checked = true;
                else if (status.Wiegand37BitsUseType == EPrintPointWiegand37BitsUseType.Special1)
                    this.rdoPadraoW37Especial1.Checked = true;

                this.txtFormatoWiegand37Bits.Text = status.Format_Wiegand37Bits;
            }

            this.chkHabilitaParidadeNasLeiturasWiegand.Checked = status.EnabledWiegandParityRead;

            switch (status.SmartCardUseType)
            {
                case EPrintPointSmartCardUseType.NotUse:
                    this.chkSmartCard.Checked = false;
                    break;
                case EPrintPointSmartCardUseType.ReadID:
                    this.chkSmartCard.Checked = true;
                    this.optLeID.Checked = true;
                    break;
                case EPrintPointSmartCardUseType.ReadRegistrationID:
                    this.chkSmartCard.Checked = true;
                    this.optLeMatricula.Checked = true;
                    break;
                case EPrintPointSmartCardUseType.ReadRegistrationIDHex:
                    this.chkSmartCard.Checked = true;
                    this.optHexadecimal.Checked = true;
                    break;
            }

            this.txtSetor.Text = status.SmartCardSector == 0 ? "" : status.SmartCardSector.ToString();
            this.txtBloco.Text = status.SmartCardBlock == 0 ? "" : status.SmartCardBlock.ToString();
            this.txtOffSet.Text = status.SmartCardOffSet == 0 ? "" : status.SmartCardOffSet.ToString();
            this.txtQtdDigitosMatriculaSmart.Text = status.SmartCardDigitsNumber == 0 ? "" : status.SmartCardDigitsNumber.ToString();

            if (this.chkSmartCard.Checked)
                this.txtFormatoSmartCard.Text = status.Format_SmartCard;

            switch (status.EncryptionType)
            {
                case EPrintPointEncryptionType.NoEncryption:

                    this.chkUtilizaCriptografia.Checked = false;
                    this.txtVetor1.Text = "";
                    this.txtVerificador1.Text = "";
                    this.txtVetor2.Text = "";
                    this.txtVerificador2.Text = "";

                    break;
                default:

                    this.chkUtilizaCriptografia.Checked = true;
                    this.txtVetor1.Text = status.Vector1;
                    this.txtVerificador1.Text = status.Checker1;
                    this.txtVetor2.Text = status.Vector2;
                    this.txtVerificador2.Text = status.Checker2;

                    switch (status.EncryptionType)
                    {
                        case EPrintPointEncryptionType.EightDigits:
                            this.optCriptografia8Digitos.Checked = true;
                            break;
                        case EPrintPointEncryptionType.TenDigits:
                            this.optCriptografia10Digitos.Checked = true;
                            break;
                        case EPrintPointEncryptionType.TwelveDigits:
                            this.optCriptografia12Digitos.Checked = true;
                            break;
                    }

                    break;
            }

            // Teclado.
            this.chkLeituraViaTecladoHabilitada.Checked = status.KeyBoard_Enabled;

            if (status.KeyBoard_Enabled)
            {
                this.optTeclado_Credencial.Checked = (status.KeyBoard_AccessType == EPrintPointAccessType.Credential);
                this.optTeclado_Pis.Checked = (status.KeyBoard_AccessType == EPrintPointAccessType.PIS);

                this.optTeclado_Ambos.Checked = (status.KeyBoard_AuthenticationType == EPrintPointAuthenticationType.BiometricAndPassword);
                this.optTeclado_BiometriaOuSenha.Checked = (status.KeyBoard_AuthenticationType == EPrintPointAuthenticationType.BiometricOrPassword);
                this.optTeclado_NaoPedeAutenticacao.Checked = (status.KeyBoard_AuthenticationType == EPrintPointAuthenticationType.NoAuthentication);
                this.optTeclado_ApenasBiometria.Checked = (status.KeyBoard_AuthenticationType == EPrintPointAuthenticationType.OnlyBiometrics);
                this.optTeclado_ApenasSenha.Checked = (status.KeyBoard_AuthenticationType == EPrintPointAuthenticationType.OnlyPassword);
            }

            // Cartão.
            this.chkLeituraViaCartaoHabilitada.Checked = status.Card_Enabled;

            if (status.Card_Enabled)
            {
                this.optCartao_Credencial.Checked = (status.Card_AccessType == EPrintPointAccessType.Credential);
                this.optCartao_Pis.Checked = (status.Card_AccessType == EPrintPointAccessType.PIS);

                this.optCartao_Ambos.Checked = (status.Card_AuthenticationType == EPrintPointAuthenticationType.BiometricAndPassword);
                this.optCartao_BiometriaOuSenha.Checked = (status.Card_AuthenticationType == EPrintPointAuthenticationType.BiometricOrPassword);
                this.optCartao_NaoPedeAutenticacao.Checked = (status.Card_AuthenticationType == EPrintPointAuthenticationType.NoAuthentication);
                this.optCartao_ApenasBiometria.Checked = (status.Card_AuthenticationType == EPrintPointAuthenticationType.OnlyBiometrics);
                this.optCartao_ApenasSenha.Checked = (status.Card_AuthenticationType == EPrintPointAuthenticationType.OnlyPassword);
            }

            // Nº de dígitos da crendecial para exibir no display no momento da marcação.
            this.txtDigitosExibicaoCredencial.Text = status.ShowCredentialDigitsInDisplay.ToString();

            // Horário de Verão.
            this.dtpHorarioVeraoInicio.Value = status.DSTStart;
            this.dtpHorarioVeraoFim.Value = status.DSTEnd;

            // Identificação.
            this.chkHabilita1ParaN.Checked = status.Identification_Enabled;

            if (status.Identification_Enabled)
            {
                this.optIdentificacao_NaoPedeAutenticacao.Checked = (status.Identification_AuthenticationTypeIdentification == EPrintPointAuthenticationTypeIdentification.NoAuthentication);
                this.optIdentificacao_ApenasSenha.Checked = (status.Identification_AuthenticationTypeIdentification == EPrintPointAuthenticationTypeIdentification.OnlyPassword);
            }

            // Autenticação.
            this.optAutenticacao_Sempre.Checked = (status.Authentication == EPrintPointBiometricAuthenticationType.Always);
            this.optAutenticacao_Parcial.Checked = (status.Authentication == EPrintPointBiometricAuthenticationType.Partial);

            // Impressora - Avanço.
            this.optImpressora_Avanco_Pequeno.Checked = (status.PrinterAdvanceSize == EPrintPointAdvanceSizeType.Small);
            this.optImpressora_Avanco_Medio.Checked = (status.PrinterAdvanceSize == EPrintPointAdvanceSizeType.Medium);
            this.optImpressora_Avanco_Longo.Checked = (status.PrinterAdvanceSize == EPrintPointAdvanceSizeType.Long);

            //Nivel Segurança Sagem
            this.nudNivelSegurancaSagem.Value = status.SecurityLevelSagem;

            // Reconhecimento Biométrico a 180º.
            this.chkReconhecimentoBiometrico180.Checked = status.BiometricRecognition180;

            //Tamanho do bobina
            this.nudTamanhoBobina.Value = status.LenghtBobbin;

            //Nivel Segurança Suprema
            this.nudNivelSegurancaSuprema.Value = status.SecurityLevelSuprema;

            if (cboModelo.SelectedIndex == 2) // REP III
            {
                //Nivel Segurança Virdi
                this.nudNivelSegurancaVirdi.Value = status.SecurityLevelVirdi;
            }

            //Energia Impressora
            switch (status.EnergyPrinter)
            {
                case EPrintPointEnergyPrinter.Regular:
                    cboEnergiaImpressora.SelectedIndex = 0;
                    break;
                case EPrintPointEnergyPrinter.Elevated:
                    cboEnergiaImpressora.SelectedIndex = 1;
                    break;
                case EPrintPointEnergyPrinter.Reduced:
                    cboEnergiaImpressora.SelectedIndex = 2;
                    break;
            }
            this.nudTamanhoBobina.Value = status.LenghtBobbin;

            // Impressora - Tipo de Corte.
            this.optImpressora_TipoCorte_Parcial.Checked = (status.PrinterCutType == EPrintPointCutType.Partial);
            this.optImpressora_TipoCorte_Total.Checked = (status.PrinterCutType == EPrintPointCutType.Total);

            // Tipo de Personalização.
            this.optNaoTemPersonalizacao.Checked = (status.PersonalizationType == EPrintPointPersonalizationType.NotUse);
            this.optPersonalizacaoMicropoint.Checked = (status.PersonalizationType == EPrintPointPersonalizationType.Micropoint);
            this.optPersonalizacaoMegaPoint.Checked = (status.PersonalizationType == EPrintPointPersonalizationType.Megapoint);
            this.txtCodigoPersonalizacao.Text = status.PersonalizationCode.ToString();
            this.txtNumeroDigitosPersonalizacao.Text = status.PersonalizationDigitsNumber.ToString();

            if (cboModelo.SelectedIndex == 2) // REP III
            {
                //Sensor de Temperatura e Umidade do Papel
                switch (status.PaperCompartmentHumidityTemperatureSensor)
                {
                    case EEnabledSensorType.NotConfigured:
                        cboSensorTemperaturaUmidadePapel.SelectedIndex = 0;
                        break;
                    case EEnabledSensorType.Enabled:
                        cboSensorTemperaturaUmidadePapel.SelectedIndex = 1;
                        break;
                    case EEnabledSensorType.Disabled:
                        cboSensorTemperaturaUmidadePapel.SelectedIndex = 2;
                        break;
                }

                //Sensor de Temperatura da Placa
                switch (status.BoardTemperatureSensor)
                {
                    case EEnabledSensorType.NotConfigured:
                        cboSensorTemperaturaPlaca.SelectedIndex = 0;
                        break;
                    case EEnabledSensorType.Enabled:
                        cboSensorTemperaturaPlaca.SelectedIndex = 1;
                        break;
                    case EEnabledSensorType.Disabled:
                        cboSensorTemperaturaPlaca.SelectedIndex = 2;
                        break;
                }

                //Acelerômetro
                switch (status.AccelerationSensor)
                {
                    case EEnabledSensorType.NotConfigured:
                        cboAcelerometro.SelectedIndex = 0;
                        break;
                    case EEnabledSensorType.Enabled:
                        cboAcelerometro.SelectedIndex = 1;
                        break;
                    case EEnabledSensorType.Disabled:
                        cboAcelerometro.SelectedIndex = 2;
                        break;
                }
            }
        }

        private void btnObterConfiguracao_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.PedeStatusEquipamento,
                                    ETipoHistorico.Envio,
                                    "Solicitação de Status para carregar a tela de configurações.");

                PrintPointStatusMessage status = this._watchComm.GetPrintPointStatus();

                if (status == null)
                {
                    MessageBox.Show("O comando de status não foi recepcionado corretamente!", Application.ProductName,
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

                CarregaTelaConfiguracoes(status);

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.PedeStatusEquipamento,
                                    ETipoHistorico.ConfirmacaoRecebimento,
                                    "Solicitação de Status para carregar a tela de configurações.");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.PedeStatusEquipamento,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnEnviarConfiguracao_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            // Adiciona as configurações.
            try
            {
                // Horário de Verão.
                if (cboModelo.SelectedIndex == 2) // REP III
                {
                    this._watchComm.AddConfiguration(EConfigurationType.DST,
                                                     this.dtpHorarioVeraoInicio.Value,
                                                     this.dtpHorarioVeraoFim.Value,
                                                     this.txtCPFResponsavel.Text);
                }
                else
                {
                    this._watchComm.AddConfiguration(EConfigurationType.DST,
                                                     this.dtpHorarioVeraoInicio.Value,
                                                     this.dtpHorarioVeraoFim.Value);
                }

                // 2 de 5 Intercalado.
                this._watchComm.AddConfiguration(EConfigurationType.Enabled2Of5Intercalary,
                                                 this.chk2de5Intercalado.Checked);

                if (this.chk2de5Intercalado.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.Format2Of5Intercalary,
                                                     this.txtFormato2de5Intercalado.Text);

                // 2 de 5 Dimep.
                this._watchComm.AddConfiguration(EConfigurationType.Enabled2of5Dimep,
                                                 this.chk2de5Dimep.Checked);

                if (this.chk2de5Dimep.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.Format2of5Dimep,
                                                     this.txtFormato2de5Dimep.Text);

                // 3 de 9.
                this._watchComm.AddConfiguration(EConfigurationType.Enabled3Of9,
                                                 this.chk3de9.Checked);

                if (this.chk3de9.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.Format3Of9,
                                                     this.txtFormato3de9.Text);

                // Magnético Dimep.
                this._watchComm.AddConfiguration(EConfigurationType.EnabledMagneticDIMEP,
                                                 this.chkMagneticoDimep.Checked);

                if (this.chkMagneticoDimep.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.FormatMagneticDIMEP,
                                                     this.txtFormatoMagneticoDimep.Text);

                this._watchComm.AddConfiguration(EConfigurationType.SpecialFormatMagneticDIMEP1,
                                                 this.chkFormatoEspecialMagneticoDimep.Checked);

                // ABA.
                this._watchComm.AddConfiguration(EConfigurationType.EnabledABA,
                                                 this.chkABA.Checked);

                if (this.chkABA.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.FormatABA,
                                                     this.txtFormatoABA.Text);

                this._watchComm.AddConfiguration(EConfigurationType.SpecialFormatABA1,
                                                 this.chkFormatoEspecialABA.Checked);

                // Wiegand 26 Bits.
                this._watchComm.AddConfiguration(EConfigurationType.EnabledWiegand26Bits,
                                                 this.chkWiegand26Bits.Checked);

                if (this.chkWiegand26Bits.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.FormatWiegand26Bits,
                                                     this.txtFormatoWiegand26Bits.Text);

                // Wiegand 34 Bits.
                this._watchComm.AddConfiguration(EConfigurationType.EnabledWiegand34Bits,
                                                 this.chkWiegand34Bits.Checked);

                if (this.chkWiegand34Bits.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.FormatWiegand34Bits,
                                                     this.txtFormatoWiegand34Bits.Text);

                // Ean 13.
                this._watchComm.AddConfiguration(EConfigurationType.EnabledEan13,
                                                 this.chkEan13.Checked);

                if (this.chkEan13.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.FormatEan13,
                                                     this.txtFormatoEan13.Text);

                // Wiegand 35 Bits.
                this._watchComm.AddConfiguration(EConfigurationType.EnabledWiegand35Bits,
                                                 this.chkWiegand35Bits.Checked);

                if (this.chkWiegand35Bits.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.FormatWiegand35Bits,
                                                     this.txtFormatoWiegand35Bits.Text);

                // Wiegand 37 Bits.
                if (!this.chkWiegand37Bits.Checked)
                {
                    this._watchComm.AddConfiguration(EConfigurationType.Wiegand37BitsUseType,
                                                     EPrintPointWiegand37BitsUseType.NotUse);
                }
                else
                {
                    if (this.rdoPadraoH10302.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.Wiegand37BitsUseType,
                                                         EPrintPointWiegand37BitsUseType.DefaultH10302);
                    else if (this.rdoPadraoH10304.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.Wiegand37BitsUseType,
                                                         EPrintPointWiegand37BitsUseType.DefaultH10304);
                    else if (this.rdoPadraoW37Especial1.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.Wiegand37BitsUseType,
                                                         EPrintPointWiegand37BitsUseType.Special1);

                    this._watchComm.AddConfiguration(EConfigurationType.FormatWiegand37Bits,
                                                     this.txtFormatoWiegand37Bits.Text);
                }

                // Habilita Paridade nas Leituras Wiegand.
                this._watchComm.AddConfiguration(EConfigurationType.EnabledWiegandParityRead,
                                                 this.chkHabilitaParidadeNasLeiturasWiegand.Checked);

                // Wiegand 32 Bits Especial.
                this._watchComm.AddConfiguration(EConfigurationType.EnabledSpecialWiegand32Bits,
                                                 this.chkWiegand32BitsEspecial.Checked);

                // SmartCard.
                if (!this.chkSmartCard.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.SmartCardUseType,
                                                     EPrintPointSmartCardUseType.NotUse);
                else
                {
                    if (this.optLeID.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardUseType,
                                                         EPrintPointSmartCardUseType.ReadID);
                    else if (this.optLeMatricula.Checked)
                    {
                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardUseType,
                                                         EPrintPointSmartCardUseType.ReadRegistrationID);

                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardSector,
                            Int16.Parse(this.txtSetor.Text == "" ? "0" : this.txtSetor.Text));

                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardBlock,
                            Int16.Parse(this.txtBloco.Text == "" ? "0" : this.txtBloco.Text));

                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardOffSet,
                            Int16.Parse(this.txtOffSet.Text == "" ? "0" : this.txtOffSet.Text));

                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardDigitsNumber,
                            Int16.Parse(this.txtQtdDigitosMatriculaSmart.Text == "" ? "0" : this.txtQtdDigitosMatriculaSmart.Text));

                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardEncryptedKey,
                            this.txtChaveSmart.Text == "" ? "0" : this.txtChaveSmart.Text);
                    }

                    else if (this.optHexadecimal.Checked)
                    {
                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardUseType,
                                                         EPrintPointSmartCardUseType.ReadRegistrationIDHex);

                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardSector,
                            Int16.Parse(this.txtSetor.Text == "" ? "0" : this.txtSetor.Text));

                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardBlock,
                            Int16.Parse(this.txtBloco.Text == "" ? "0" : this.txtBloco.Text));

                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardOffSet,
                            Int16.Parse(this.txtOffSet.Text == "" ? "0" : this.txtOffSet.Text));

                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardDigitsNumber,
                            Int16.Parse(this.txtQtdDigitosMatriculaSmart.Text == "" ? "0" : this.txtQtdDigitosMatriculaSmart.Text));

                        this._watchComm.AddConfiguration(EConfigurationType.SmartCardEncryptedKey,
                            this.txtChaveSmart.Text == "" ? "0" : this.txtChaveSmart.Text);
                    }

                    this._watchComm.AddConfiguration(EConfigurationType.FormatSmartCard,
                                                     this.txtFormatoSmartCard.Text);
                }

                // Criptografia.
                if (this.chkUtilizaCriptografia.Checked && this.chkUtilizaCriptografia.Enabled)
                {
                    EPrintPointEncryptionType printPointEncryptionType;

                    if (this.optCriptografia8Digitos.Checked)
                    {
                        printPointEncryptionType = EPrintPointEncryptionType.EightDigits;
                    }
                    else
                    {
                        if (this.optCriptografia10Digitos.Checked)
                        {
                            printPointEncryptionType = EPrintPointEncryptionType.TenDigits;

                            this.txtVetor1.Text = "0";
                            this.txtVetor2.Text = "0";
                            this.txtVerificador1.Text = "0";
                            this.txtVerificador2.Text = "0";
                        }
                        else
                        {
                            printPointEncryptionType = EPrintPointEncryptionType.TwelveDigits;
                        }
                    }

                    this._watchComm.AddConfiguration(EConfigurationType.EncryptionType,
                                                     printPointEncryptionType,
                                                     Int64.Parse(this.txtVetor1.Text),
                                                     Int64.Parse(this.txtVetor2.Text),
                                                     Int16.Parse(this.txtVerificador1.Text),
                                                     Int16.Parse(this.txtVerificador2.Text));
                }
                else
                    this._watchComm.AddConfiguration(EConfigurationType.EncryptionType,
                                                     EPrintPointEncryptionType.NoEncryption);

                // Teclado.
                this._watchComm.AddConfiguration(EConfigurationType.KeyBoardEnabled,
                                                 this.chkLeituraViaTecladoHabilitada.Checked);

                if (this.chkLeituraViaTecladoHabilitada.Checked)
                {
                    // Código Utilizado.
                    if (this.optTeclado_Credencial.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.keyBoardAccessType,
                                                         EPrintPointAccessType.Credential);
                    else
                        this._watchComm.AddConfiguration(EConfigurationType.keyBoardAccessType,
                                                         EPrintPointAccessType.PIS);

                    // Autenticação.
                    if (this.optTeclado_NaoPedeAutenticacao.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.keyBoardAuthenticationType,
                                                         EPrintPointAuthenticationType.NoAuthentication);
                    else if (this.optTeclado_ApenasSenha.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.keyBoardAuthenticationType,
                                                         EPrintPointAuthenticationType.OnlyPassword);
                    else if (this.optTeclado_ApenasBiometria.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.keyBoardAuthenticationType,
                                                         EPrintPointAuthenticationType.OnlyBiometrics);
                    else if (this.optTeclado_BiometriaOuSenha.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.keyBoardAuthenticationType,
                                                         EPrintPointAuthenticationType.BiometricOrPassword);
                    else if (this.optTeclado_Ambos.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.keyBoardAuthenticationType,
                                                         EPrintPointAuthenticationType.BiometricAndPassword);
                }

                // Cartão.
                this._watchComm.AddConfiguration(EConfigurationType.CardEnabled,
                                                 this.chkLeituraViaCartaoHabilitada.Checked);

                if (this.chkLeituraViaCartaoHabilitada.Checked)
                {
                    // Código Utilizado.
                    if (this.optCartao_Credencial.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.CardAccessType,
                                                         EPrintPointAccessType.Credential);
                    else
                        this._watchComm.AddConfiguration(EConfigurationType.CardAccessType,
                                                         EPrintPointAccessType.PIS);

                    // Autenticação.
                    if (this.optCartao_NaoPedeAutenticacao.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.CardAuthenticationType,
                                                         EPrintPointAuthenticationType.NoAuthentication);
                    else if (this.optCartao_ApenasSenha.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.CardAuthenticationType,
                                                         EPrintPointAuthenticationType.OnlyPassword);
                    else if (this.optCartao_ApenasBiometria.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.CardAuthenticationType,
                                                         EPrintPointAuthenticationType.OnlyBiometrics);
                    else if (this.optCartao_BiometriaOuSenha.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.CardAuthenticationType,
                                                         EPrintPointAuthenticationType.BiometricOrPassword);
                    else if (this.optCartao_Ambos.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.CardAuthenticationType,
                                                         EPrintPointAuthenticationType.BiometricAndPassword);
                }

                // Quantidade de dígitos para exibição da credencial no display do relógio no momento da marcação de ponto.
                this._watchComm.AddConfiguration(EConfigurationType.ShowCredentialInDisplay,
                                                 Int16.Parse(this.txtDigitosExibicaoCredencial.Text));

                // Identificação.
                this._watchComm.AddConfiguration(EConfigurationType.IdentificationEnabled,
                                                 this.chkHabilita1ParaN.Checked);

                if (this.chkHabilita1ParaN.Checked)
                    if (this.optIdentificacao_NaoPedeAutenticacao.Checked)
                        this._watchComm.AddConfiguration(EConfigurationType.IdentificationAuthenticationTypeIdentification,
                                                         EPrintPointAuthenticationTypeIdentification.NoAuthentication);
                    else
                        this._watchComm.AddConfiguration(EConfigurationType.IdentificationAuthenticationTypeIdentification,
                                                         EPrintPointAuthenticationTypeIdentification.OnlyPassword);

                // Autenticação.
                if (this.optAutenticacao_Sempre.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.Authentication,
                                                     EPrintPointBiometricAuthenticationType.Always);
                else
                    this._watchComm.AddConfiguration(EConfigurationType.Authentication,
                                                     EPrintPointBiometricAuthenticationType.Partial);

                // Avanço da impressora.
                if (this.optImpressora_Avanco_Pequeno.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.PrinterAdvanceSize,
                                                     EPrintPointAdvanceSizeType.Small);
                else if (this.optImpressora_Avanco_Medio.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.PrinterAdvanceSize,
                                                     EPrintPointAdvanceSizeType.Medium);
                else if (this.optImpressora_Avanco_Longo.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.PrinterAdvanceSize,
                                                     EPrintPointAdvanceSizeType.Long);

                //Security Level Sagem
                this._watchComm.AddConfiguration(EConfigurationType.SecurityLevelSagem,
                                                 (byte)nudNivelSegurancaSagem.Value);

                //Security Level Suprema
                this._watchComm.AddConfiguration(EConfigurationType.SecurityLevelSuprema,
                                                (byte)nudNivelSegurancaSuprema.Value);

                if (cboModelo.SelectedIndex == 2) // REP III
                {
                    //Security Level Virdi
                    this._watchComm.AddConfiguration(EConfigurationType.SecurityLevelVirdi,
                                                    (byte)nudNivelSegurancaVirdi.Value);
                }

                // Reconhecimento biométrico a 180º.
                this._watchComm.AddConfiguration(EConfigurationType.BiometricRecognition180,
                                                 chkReconhecimentoBiometrico180.Checked);

                //Energia da Impressora
                if (cboEnergiaImpressora.SelectedIndex == 0)
                    this._watchComm.AddConfiguration(EConfigurationType.EnergyPrinter,
                                                     EPrintPointEnergyPrinter.Regular);
                else if (cboEnergiaImpressora.SelectedIndex == 1)
                    this._watchComm.AddConfiguration(EConfigurationType.EnergyPrinter,
                                                     EPrintPointEnergyPrinter.Elevated);
                else
                    this._watchComm.AddConfiguration(EConfigurationType.EnergyPrinter,
                                                     EPrintPointEnergyPrinter.Reduced);
                //Tamanho do bobina
                this._watchComm.AddConfiguration(EConfigurationType.LenghtBobbin,
                                                (byte)nudTamanhoBobina.Value);

                // Tipo de corte.
                if (this.optImpressora_TipoCorte_Parcial.Checked)
                    this._watchComm.AddConfiguration(EConfigurationType.PrinterCutType,
                                                     EPrintPointCutType.Partial);
                else
                    this._watchComm.AddConfiguration(EConfigurationType.PrinterCutType,
                                                     EPrintPointCutType.Total);

                // Tipo de Personalização.
                if (this.optNaoTemPersonalizacao.Checked == true)
                {
                    this._watchComm.AddConfiguration(EConfigurationType.PersonalizationType,
                                                     EPrintPointPersonalizationType.NotUse);
                }
                else
                {
                    this._watchComm.AddConfiguration(EConfigurationType.PersonalizationType,
                                                     (this.optPersonalizacaoMegaPoint.Checked ? EPrintPointPersonalizationType.Megapoint : EPrintPointPersonalizationType.Micropoint),
                                                      Int32.Parse(this.txtNumeroDigitosPersonalizacao.Text),
                                                      Int32.Parse(this.txtCodigoPersonalizacao.Text));
                }

                if (cboModelo.SelectedIndex == 2) // REP III
                {
                    if (cboSensorTemperaturaUmidadePapel.SelectedIndex == 0)
                        this._watchComm.AddConfiguration(EConfigurationType.PaperCompartmentHumidityTemperatureSensor, EEnabledSensorType.NotConfigured);
                    else if (cboSensorTemperaturaUmidadePapel.SelectedIndex == 1)
                        this._watchComm.AddConfiguration(EConfigurationType.PaperCompartmentHumidityTemperatureSensor, EEnabledSensorType.Enabled);
                    else
                        this._watchComm.AddConfiguration(EConfigurationType.PaperCompartmentHumidityTemperatureSensor, EEnabledSensorType.Disabled);

                    if (cboSensorTemperaturaPlaca.SelectedIndex == 0)
                        this._watchComm.AddConfiguration(EConfigurationType.BoardTemperatureSensor, EEnabledSensorType.NotConfigured);
                    else if (cboSensorTemperaturaPlaca.SelectedIndex == 1)
                        this._watchComm.AddConfiguration(EConfigurationType.BoardTemperatureSensor, EEnabledSensorType.Enabled);
                    else
                        this._watchComm.AddConfiguration(EConfigurationType.BoardTemperatureSensor, EEnabledSensorType.Disabled);

                    if (cboAcelerometro.SelectedIndex == 0)
                        this._watchComm.AddConfiguration(EConfigurationType.AccelerationSensor, EEnabledSensorType.NotConfigured);
                    else if (cboAcelerometro.SelectedIndex == 1)
                        this._watchComm.AddConfiguration(EConfigurationType.AccelerationSensor, EEnabledSensorType.Enabled);
                    else
                        this._watchComm.AddConfiguration(EConfigurationType.AccelerationSensor, EEnabledSensorType.Disabled);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro durante a leitura dos parâmetros informados!" +
                                "\nErro: " + ex.Message, Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // Envia a configuração para o relógio.
            try
            {
                this._watchComm.OpenConnection();

                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.Configuracoes,
                                    ETipoHistorico.Envio, "");

                this._watchComm.SendSettings();

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.Configuracoes,
                                    ETipoHistorico.ConfirmacaoRecebimento, "");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.Configuracoes,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnEnviarConfiguracaoParcial_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            // Adiciona as configurações.
            try
            {
                // Horário de Verão.
                if (cboModelo.SelectedIndex == 2) // REP III
                {
                    this._watchComm.AddParcialConfiguration(ParcialConfigurationType.DST,
                                                            this.dtpHorarioVeraoInicio.Value,
                                                            this.dtpHorarioVeraoFim.Value,
                                                            this.txtCPFResponsavel.Text);
                }
                else
                {
                    this._watchComm.AddParcialConfiguration(ParcialConfigurationType.DST,
                                                            this.dtpHorarioVeraoInicio.Value,
                                                            this.dtpHorarioVeraoFim.Value);
                }

                // Avanço da impressora.
                if (this.optImpressora_Avanco_Pequeno.Checked)
                    this._watchComm.AddParcialConfiguration(ParcialConfigurationType.PrinterAdvanceSize,
                                                            EPrintPointAdvanceSizeType.Small);
                else if (this.optImpressora_Avanco_Medio.Checked)
                    this._watchComm.AddParcialConfiguration(ParcialConfigurationType.PrinterAdvanceSize,
                                                            EPrintPointAdvanceSizeType.Medium);
                else if (this.optImpressora_Avanco_Longo.Checked)
                    this._watchComm.AddParcialConfiguration(ParcialConfigurationType.PrinterAdvanceSize,
                                                            EPrintPointAdvanceSizeType.Long);

                // Tipo de corte.
                if (this.optImpressora_TipoCorte_Parcial.Checked)
                    this._watchComm.AddParcialConfiguration(ParcialConfigurationType.PrinterCutType,
                                                            EPrintPointCutType.Partial);
                else
                    this._watchComm.AddParcialConfiguration(ParcialConfigurationType.PrinterCutType,
                                                            EPrintPointCutType.Total);

                // Energia da Impressora.
                if (cboEnergiaImpressora.SelectedIndex == 0)
                    this._watchComm.AddParcialConfiguration(ParcialConfigurationType.EnergyPrinter,
                                                            EPrintPointEnergyPrinter.Regular);
                else if (cboEnergiaImpressora.SelectedIndex == 1)
                    this._watchComm.AddParcialConfiguration(ParcialConfigurationType.EnergyPrinter,
                                                            EPrintPointEnergyPrinter.Elevated);
                else
                    this._watchComm.AddParcialConfiguration(ParcialConfigurationType.EnergyPrinter,
                                                            EPrintPointEnergyPrinter.Reduced);

                // Tamanho do bobina.
                this._watchComm.AddParcialConfiguration(ParcialConfigurationType.LenghtBobbin,
                                                        (byte)nudTamanhoBobina.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro durante a leitura dos parâmetros informados!" +
                                "\nErro: " + ex.Message, Application.ProductName,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // Envia a configuração para o relógio.
            try
            {
                this._watchComm.OpenConnection();

                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.ConfiguracoesParciais,
                                    ETipoHistorico.Envio, "");

                this._watchComm.SendParcialSettings();

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ConfiguracoesParciais,
                                    ETipoHistorico.ConfirmacaoRecebimento, "");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.Configuracoes,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void optNaoTemPersonalizacao_CheckedChanged(object sender, EventArgs e)
        {
            this.txtCodigoPersonalizacao.Enabled = !this.optNaoTemPersonalizacao.Checked;
            this.txtNumeroDigitosPersonalizacao.Enabled = !this.optNaoTemPersonalizacao.Checked;
        }

        private void optLeMatricula_CheckedChanged(object sender, EventArgs e)
        {
            this.txtSetor.Enabled = this.txtBloco.Enabled = this.txtOffSet.Enabled = this.txtQtdDigitosMatriculaSmart.Enabled =
                this.txtChaveSmart.Enabled = this.optLeMatricula.Checked;
        }

        private void txtChaveSmart_TextChanged(object sender, EventArgs e)
        {
            this.txtChaveSmart.Text = this.txtChaveSmart.Text.ToUpper();
            this.txtChaveSmart.Select(this.txtChaveSmart.Text.Length, 0);
        }

        private void optHexadecimal_CheckedChanged(object sender, EventArgs e)
        {
            this.txtSetor.Enabled = this.txtBloco.Enabled = this.txtOffSet.Enabled = this.txtQtdDigitosMatriculaSmart.Enabled =
                this.txtChaveSmart.Enabled = this.optHexadecimal.Checked;
        }
        #endregion

        #region " ---------------------> Funcionalidades WatchComm Server <---------------------"

        private WatchListener _watchListener;
        private WatchListener _watchListener2;
        private TcpClient _watchConnection;
        private delegate void DelAtualizaStatusConectado();

        private void rdbClient_CheckedChanged(object sender, EventArgs e)
        {
            VisibleGroups();
        }

        private void rdbServer_CheckedChanged(object sender, EventArgs e)
        {
            VisibleGroups();
        }

        private void VisibleGroups()
        {
            this.grbPorta.Visible = rdbServer.Checked;
            this.grbIp.Visible = rdbClient.Checked;

            this.pnlServer.Visible = rdbServer.Checked;

            if (rdbServer.Checked)
            {
                this.Size = new Size(this.Size.Width, 709);
                this.tabPrincipal.Location = new Point(6, 110);
            }
            else
            {
                this.Size = new Size(this.Size.Width, 656);
                this.tabPrincipal.Location = new Point(6, 56);
            }
        }

        private void btnAguardar_Cancelar_Click(object sender, EventArgs e)
        {
            if (_watchListener == null)
            {
                try
                {
                    _watchListener = new org.cesar.dmplight.watchComm.impl.WatchListener(Int32.Parse(txtPorta.Text), Int16.Parse(txtIdRelogio.Text));
                    _watchListener.watchConnectEvent += new org.cesar.dmplight.watchComm.impl.WatchListener.DelWatchConnect(_watchListener_watchConnectEvent);
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
                _watchListener.watchConnectEvent -= _watchListener_watchConnectEvent;
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

                _watchComm = null;

                AtualizaStatusModoServer(StatusModoServer.Cancelado);
            }
        }

        void _watchListener_watchConnectEvent(short watchID, System.Net.Sockets.TcpClient watchConnection)
        {
            DelAtualizaStatusConectado delAtualizaStatusConectado = new DelAtualizaStatusConectado(AtualizaStatusConectado);
            try
            {
                _watchConnection = watchConnection;
                _watchComm = null;

                this.BeginInvoke(delAtualizaStatusConectado);
            }
            catch { }
        }

        private void btnEncerrarConexaoModem_Click(object sender, EventArgs e)
        {
            this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.Modem,
                                                                              _watchConnection,
                                                                              1,
                                                                              "",
                                                                              org.cesar.dmplight.watchComm.api.WatchConnectionType.ConnectedMode,
                                                                              this.txtVersaoFirmware.Text);

            try
            {
                this._watchComm.TerminateModemConnection();

                _watchComm = null;

                AtualizaStatusModoServer(StatusModoServer.Inativo);

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                ErroDuranteRecepcaoDoComando(ex);
            }
        }

        private void AtualizaStatusConectado()
        {
            AtualizaStatusModoServer(StatusModoServer.Conectado);
        }

        private enum StatusModoServer
        {
            Conectado,
            Aguardando,
            Inativo,
            Cancelado
        }

        private void AtualizaStatusModoServer(StatusModoServer statusModoServer)
        {
            switch (statusModoServer)
            {
                case StatusModoServer.Conectado:

                    this.lblStatus.Text = "Conectado";
                    this.lblStatus.ForeColor = Color.Green;
                    this.btnEncerrarConexaoModem.Enabled = true;

                    break;
                case StatusModoServer.Aguardando:

                    this.btnAguardar_Cancelar.Text = "Cancelar";
                    this.lblStatus.Text = "Aguardando Conexão";
                    this.lblStatus.ForeColor = Color.Yellow;
                    this.btnEncerrarConexaoModem.Enabled = false;

                    break;
                case StatusModoServer.Inativo:

                    this.lblStatus.Text = "Aguardando Conexão";
                    this.lblStatus.ForeColor = Color.Yellow;
                    this.btnEncerrarConexaoModem.Enabled = false;

                    break;
                case StatusModoServer.Cancelado:

                    this.btnAguardar_Cancelar.Text = "Aguardar Conexão";
                    this.lblStatus.Text = "Inativo";
                    this.lblStatus.ForeColor = Color.White;
                    this.btnEncerrarConexaoModem.Enabled = false;

                    break;
                default:
                    break;
            }
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Funcionários Completos <---------------------"

        private void btnExclusaoFuncionariosCompletos_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                // Insere os funcionários na lista do componente.
                foreach (DataRow dr in this._dtFuncionariosCompletos)
                {
                    try
                    {
                        this._watchComm.AddFullEmployee(dr["PIS"].ToString(), "", "", null, null);
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
                    this._watchComm.OpenConnection();

                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.ExcluiFuncionarioLista,
                                        ETipoHistorico.Envio,
                                        "Exclusão da lista de funcionários");

                    this._watchComm.ExcludeFullEmployeesList(txtCPFResponsavel.Text);

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.ExcluiFuncionarioLista,
                                        ETipoHistorico.ConfirmacaoRecebimento, "");
                }
                catch (Exception ex)
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.ExcluiFuncionarioLista,
                                        ETipoHistorico.Erro,
                                        ex.Message);

                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }

        private void btnInclusaoFuncionariosCompletos_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                // Insere os funcionários na lista do componente.
                foreach (DataRow dr in this._dtFuncionariosCompletos)
                {
                    try
                    {
                        // Obs: Os arrays abaixo poderiam ser criados com tamanho ilimitado. Estou criando com apenas 1 item a título de exemplo.
                        PrintPointCredential[] arrayCredenciais = new PrintPointCredential[1];
                        PrintPointFingerPrintMessage[] arrayTemplates = new PrintPointFingerPrintMessage[1];

                        arrayCredenciais[0] = new PrintPointCredential();
                        arrayCredenciais[0].Credential = dr["Credencial"].ToString();
                        arrayCredenciais[0].Via_version = short.Parse(dr["Via"].ToString() == "" ? "0" : dr["Via"].ToString());

                        arrayTemplates[0] = new PrintPointFingerPrintMessage();
                        arrayTemplates[0].FingerPrint = dr["Template"].ToString();

                        if (this.cboSensor.SelectedIndex == 0)
                            arrayTemplates[0].FingerPrintSensor = EfingerPrintSensor.Sagem;
                        else if (this.cboSensor.SelectedIndex == 1)
                            arrayTemplates[0].FingerPrintSensor = EfingerPrintSensor.Suprema;
                        else if (this.cboSensor.SelectedIndex == 2)
                            arrayTemplates[0].FingerPrintSensor = EfingerPrintSensor.Fugitsu;
                        else
                            arrayTemplates[0].FingerPrintSensor = EfingerPrintSensor.Ilock;

                        if (arrayTemplates[0].FingerPrintSensor == EfingerPrintSensor.Fugitsu)
                        {
                            arrayTemplates[0].FingerPrintHandOne = (EFingerPrintHand)Int16.Parse(dr["Dedo1"].ToString());
                            arrayTemplates[0].FingerPrintHandTwo = (EFingerPrintHand)Int16.Parse(dr["Dedo2"].ToString());
                        }
                        else
                        {
                            arrayTemplates[0].FingerprintTypeOne = (EFingerPrintType)Int16.Parse(dr["Dedo1"].ToString());
                            arrayTemplates[0].FingerprintTypeTwo = (EFingerPrintType)Int16.Parse(dr["Dedo2"].ToString());
                        }

                        this._watchComm.AddFullEmployee(dr["PIS"].ToString(), dr["Nome"].ToString(), dr["Senha"].ToString(), arrayCredenciais,
                            arrayTemplates);
                    }
                    catch (Exception ex)
                    {
                        ErroDuranteRecepcaoDoComando(ex);

                        return;
                    }
                }

                this._watchComm.OpenConnection();

                // Solicita ao componente o envio da lista de funcionários.
                try
                {
                    AddHistoricoComando(ETipoOrigemComando.Software,
                                        ETipoComando.InsereFuncionarioLista,
                                        ETipoHistorico.Envio,
                                        "Envio da lista de funcionários");

                    this._watchComm.IncludeFullEmployeesList(true, txtCPFResponsavel.Text);

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.InsereFuncionarioLista,
                                        ETipoHistorico.ConfirmacaoRecebimento, "");
                }
                catch (Exception ex)
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.InsereFuncionarioLista,
                                        ETipoHistorico.Erro,
                                        ex.Message);

                    ErroDuranteRecepcaoDoComando(ex);

                    return;
                }
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }

            ComandoRecepcionadoComSucesso();
        }
        #endregion

        #region " ---------------------> Event Handles da Tab Conexão Client <---------------------"
        private void btnObterConfiguracaoClient_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.ObterConfiguracaoConexaoClient,
                                    ETipoHistorico.Envio,
                                    "Solicitação dos parâmetros da conexão client para carregar a tela de configurações client.");

                PrintPointClientConnectionConfigurationResponseMessage response = this._watchComm.GetClientConnectionConfiguration();
                
                if (response == null)
                {
                    MessageBox.Show("O comando de solicitação dos parâmetros da conexão client não foi recepcionado corretamente!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }

                chkHabilitaConexaUsuario.Checked = response.UserEnabledConnection;
                txtTempoConexaoUsuario.Text = response.UserReconnectionTimeout.ToString();
                txtTempoDesconexaoUsuario.Text = response.UserIddleTimeout.ToString();
                chkHabilitaDNSUsuario.Checked = response.UserEnabledDNS;
                txtServidorDNSPrimarioUsuario.Text = response.UserPrimaryDNSServer;
                txtServidorDNSSecundarioUsuario.Text = response.UserSecundaryDNSServer;
                txtEnderecoConexaoDNSUsuario.Text = response.UserConnectionAddressDNS;
                txtEnderecoConexaoIPUsuario.Text = response.UserConnectionAddressIP;
                txtPortaConexaoIPUsuario.Text = response.UserConnectionTcpPort.ToString();
                txtIdentificacaoEquipamentoUsuario.Text = response.UserWatchIdentification.ToString();

                chkHabilitaConexaMonitoramento.Checked = response.MonitorEnabledConnection;
                txtTempoConexaoMonitoramento.Text = response.MonitorReconnectionTimeout.ToString();
                txtTempoDesconexaoMonitoramento.Text = response.MonitorIddleTimeout.ToString();
                chkHabilitaDNSMonitoramento.Checked = response.MonitorEnabledDNS;
                txtServidorDNSPrimarioMonitoramento.Text = response.MonitorPrimaryDNSServer;
                txtServidorDNSSecundarioMonitoramento.Text = response.MonitorSecundaryDNSServer;
                txtEnderecoConexaoDNSMonitoramento.Text = response.MonitorConnectionAddressDNS;
                txtEnderecoConexaoIPMonitoramento.Text = response.MonitorConnectionAddressIP;
                txtPortaConexaoIPMonitoramento.Text = response.MonitorConnectionTcpPort.ToString();
                txtIdentificacaoEquipamentoMonitoramento.Text = response.MonitorWatchIdentification.ToString();

                txtIdentificacaoCliente.Text = response.ClientIdentification;
                
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ObterConfiguracaoConexaoClient,
                                    ETipoHistorico.ConfirmacaoRecebimento,
                                    "Solicitação dos parâmetros da conexão client para carregar a tela de configurações client.");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.ObterConfiguracaoConexaoClient,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnEnviarConfiguracaoClientUsuario_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.EnviarConfiguracaoConexaoClient,
                                    ETipoHistorico.Envio,
                                    "Envio dos parâmetros da conexão client.");

                this._watchComm.SendClientConnectionConfiguration(EClientConnectionType.User,
                    chkHabilitaConexaUsuario.Checked,
                    int.Parse(txtTempoConexaoUsuario.Text),
                    int.Parse(txtTempoDesconexaoUsuario.Text),
                    chkHabilitaDNSUsuario.Checked,
                    txtServidorDNSPrimarioUsuario.Text,
                    txtServidorDNSSecundarioUsuario.Text,
                    txtEnderecoConexaoDNSUsuario.Text,
                    txtEnderecoConexaoIPUsuario.Text,
                    int.Parse(txtPortaConexaoIPUsuario.Text),
                    long.Parse(txtIdentificacaoEquipamentoUsuario.Text));

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.EnviarConfiguracaoConexaoClient,
                                    ETipoHistorico.ConfirmacaoRecebimento,
                                    "Envio dos parâmetros da conexão client.");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.EnviarConfiguracaoConexaoClient,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnEnviarConfiguracaoClientMonitoramento_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.EnviarConfiguracaoConexaoClient,
                                    ETipoHistorico.Envio,
                                    "Envio dos parâmetros da conexão client.");

                this._watchComm.SendClientConnectionConfiguration(EClientConnectionType.Monitor,
                    chkHabilitaConexaMonitoramento.Checked,
                    int.Parse(txtTempoConexaoMonitoramento.Text),
                    int.Parse(txtTempoDesconexaoMonitoramento.Text),
                    chkHabilitaDNSMonitoramento.Checked,
                    txtServidorDNSPrimarioMonitoramento.Text,
                    txtServidorDNSSecundarioMonitoramento.Text,
                    txtEnderecoConexaoDNSMonitoramento.Text,
                    txtEnderecoConexaoIPMonitoramento.Text,
                    int.Parse(txtPortaConexaoIPMonitoramento.Text),
                    long.Parse(txtIdentificacaoEquipamentoMonitoramento.Text));

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.EnviarConfiguracaoConexaoClient,
                                    ETipoHistorico.ConfirmacaoRecebimento,
                                    "Envio dos parâmetros da conexão client.");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.EnviarConfiguracaoConexaoClient,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnEnviarIdentificacaoCliente_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            try
            {
                this._watchComm.OpenConnection();

                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.EnviarConfiguracaoConexaoClient,
                                    ETipoHistorico.Envio,
                                    "Envio dos parâmetros da conexão client.");

                this._watchComm.SendClientIdentification(txtIdentificacaoCliente.Text);

                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.EnviarConfiguracaoConexaoClient,
                                    ETipoHistorico.ConfirmacaoRecebimento,
                                    "Envio dos parâmetros da conexão client.");

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.EnviarConfiguracaoConexaoClient,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        #endregion

        #region " ---------------------> Event Handles da Tab Eventos Sistema <--------------------"
        private void rdbEventosSistemaAPartirNumeroSequencial_CheckedChanged(object sender, EventArgs e)
        {
            txtEventosSistemaNumeroSequencial.Enabled = rdbEventosSistemaAPartirNumeroSequencial.Checked;
        }

        private void rdbEventosSistemaUltimos_CheckedChanged(object sender, EventArgs e)
        {
            txtEventosSistemaUltimos.Enabled = rdbEventosSistemaUltimos.Checked;
        }

        private void btnObterEventosSistema_Click(object sender, EventArgs e)
        {
            if (!InstanciaWatchComm()) return;

            SystemEventRecord[] registrosEventoSistema = null;

            try
            {
                AddHistoricoComando(ETipoOrigemComando.Software,
                                    ETipoComando.SolicitaRegistrosEventoSistema,
                                    ETipoHistorico.Envio, "");

                this._watchComm.OpenConnection();

                if (rdbEventosSistemaNovos.Checked)
                    registrosEventoSistema = this._watchComm.InquirySystemEventRecords(ESystemEventInquiryType.New);
                else if (rdbEventosSistemaTodos.Checked)
                    registrosEventoSistema = this._watchComm.InquirySystemEventRecords(ESystemEventInquiryType.All);
                else if (rdbEventosSistemaAPartirNumeroSequencial.Checked)
                    registrosEventoSistema = this._watchComm.InquirySystemEventRecords(ESystemEventInquiryType.FromSequencialNumber, int.Parse(txtEventosSistemaNumeroSequencial.Text));
                else if (rdbEventosSistemaUltimos.Checked)
                    registrosEventoSistema = this._watchComm.InquirySystemEventRecords(ESystemEventInquiryType.LastXEvents, int.Parse(txtEventosSistemaUltimos.Text));
                
                if (registrosEventoSistema == null)

                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.SolicitaRegistrosEventoSistema,
                                        ETipoHistorico.ConfirmacaoRecebimento,
                                        "O relógio não possui registros.");
                else
                {
                    AddHistoricoComando(ETipoOrigemComando.Relogio,
                                        ETipoComando.SolicitaRegistrosEventoSistema,
                                        ETipoHistorico.ConfirmacaoRecebimento,
                                        "Registros coletados...");

                    DataRow dr;
                    String detalhes;
                    while (registrosEventoSistema != null)
                    {
                        foreach (SystemEventRecord registroEventoSistema in registrosEventoSistema)
                        {
                            dr = this._dtEventosSistema.NewRow();
                            dr["NumeroSequencial"] = registroEventoSistema.SequencialNumber;
                            dr["DataHora"] = registroEventoSistema.DateTime.ToShortDateString() + " " + registroEventoSistema.DateTime.ToShortTimeString();

                            if (registroEventoSistema is SystemEventRecord_Temperature)
                            {
                                SystemEventRecord_Temperature registroEventoSistemaTemperatura = (SystemEventRecord_Temperature)registroEventoSistema;
                                dr["Tipo"] = "Evento de temperatura";

                                detalhes = "Id do sensor: ";
                                switch (registroEventoSistemaTemperatura.SensorId)
                                {
                                    case ESensorId.Board:
                                        detalhes += "Sensor da placa; ";
                                        break;
                                    case ESensorId.PaperCompartment:
                                        detalhes += "Sensor do compartimento de papel; ";
                                        break;
                                }

                                detalhes += string.Format("Temperatura lida em graus celsius * 10: {0}; ", registroEventoSistemaTemperatura.Temperature);
                                
                                detalhes += "Status de alerta: ";

                                switch (registroEventoSistemaTemperatura.StatusLevel)
                                {
                                    case EStatusLevel.EnteredAttentionLevel:
                                        detalhes += "Entrada em nível de atenção;";
                                        break;
                                    case EStatusLevel.LeftAttentionLevel:
                                        detalhes += "Saída do nível de atenção;";
                                        break;
                                    case EStatusLevel.PersistenceAttentionLevel:
                                        detalhes += "Persistência do nível de atenção;";
                                        break;
                                    case EStatusLevel.EnteredAlarmLevel:
                                        detalhes += "Entrada em nível de alarme;";
                                        break;
                                    case EStatusLevel.LeftAlarmLevel:
                                        detalhes += "Saída do nível de alarme;";
                                        break;
                                    case EStatusLevel.PersistenceAlarmLevel:
                                        detalhes += "Persistência do nível de alarme;";
                                        break;
                                    default:
                                        break;
                                }

                                dr["Detalhes"] = detalhes;
                            }
                            else if (registroEventoSistema is SystemEventRecord_Humidity)
                            {
                                SystemEventRecord_Humidity registroEventoSistemaUmidade = (SystemEventRecord_Humidity)registroEventoSistema;
                                dr["Tipo"] = "Evento de umidade";

                                detalhes = "Id do sensor: ";
                                switch (registroEventoSistemaUmidade.SensorId)
                                {
                                    case ESensorId.Board:
                                        detalhes += "Sensor da placa; ";
                                        break;
                                    case ESensorId.PaperCompartment:
                                        detalhes += "Sensor do compartimento de papel; ";
                                        break;
                                }

                                detalhes += string.Format("Umidade relativa lida: {0}; ", registroEventoSistemaUmidade.RelativeHumidity);

                                detalhes += "Status de alerta: ";

                                switch (registroEventoSistemaUmidade.StatusLevel)
                                {
                                    case EStatusLevel.EnteredAttentionLevel:
                                        detalhes += "Entrada em nível de atenção;";
                                        break;
                                    case EStatusLevel.LeftAttentionLevel:
                                        detalhes += "Saída do nível de atenção;";
                                        break;
                                    case EStatusLevel.PersistenceAttentionLevel:
                                        detalhes += "Persistência do nível de atenção;";
                                        break;
                                    case EStatusLevel.EnteredAlarmLevel:
                                        detalhes += "Entrada em nível de alarme;";
                                        break;
                                    case EStatusLevel.LeftAlarmLevel:
                                        detalhes += "Saída do nível de alarme;";
                                        break;
                                    case EStatusLevel.PersistenceAlarmLevel:
                                        detalhes += "Persistência do nível de alarme;";
                                        break;
                                    default:
                                        break;
                                }

                                dr["Detalhes"] = detalhes;
                            }
                            else if (registroEventoSistema is SystemEventRecord_Voltage)
                            {
                                SystemEventRecord_Voltage registroEventoSistemaTensao = (SystemEventRecord_Voltage)registroEventoSistema;
                                dr["Tipo"] = "Tensão";

                                detalhes = "Ponto de tensão medido: ";
                                switch (registroEventoSistemaTensao.VoltageReadPoint)
                                {
                                    case EVoltageReadPoint.DCIN:
                                        detalhes += "Tensão de entrada DC; ";
                                        break;
                                    case EVoltageReadPoint.NoBreakBattery:
                                        detalhes += "Tensão de bateria do Nobreak; ";
                                        break;
                                }

                                detalhes += string.Format("Valor da tensão medida * 10: {0}; ", registroEventoSistemaTensao.Voltage);

                                detalhes += "Status de alerta: ";

                                switch (registroEventoSistemaTensao.StatusLevel)
                                {
                                    case EStatusLevel.EnteredAttentionLevel:
                                        detalhes += "Entrada em nível de atenção;";
                                        break;
                                    case EStatusLevel.LeftAttentionLevel:
                                        detalhes += "Saída do nível de atenção;";
                                        break;
                                    case EStatusLevel.PersistenceAttentionLevel:
                                        detalhes += "Persistência do nível de atenção;";
                                        break;
                                    case EStatusLevel.EnteredAlarmLevel:
                                        detalhes += "Entrada em nível de alarme;";
                                        break;
                                    case EStatusLevel.LeftAlarmLevel:
                                        detalhes += "Saída do nível de alarme;";
                                        break;
                                    case EStatusLevel.PersistenceAlarmLevel:
                                        detalhes += "Persistência do nível de alarme;";
                                        break;
                                    default:
                                        break;
                                }

                                dr["Detalhes"] = detalhes;
                            }
                            else if (registroEventoSistema is SystemEventRecord_PowerSupply)
                            {
                                SystemEventRecord_PowerSupply registroEventoSistemaFonteAlimentacao = (SystemEventRecord_PowerSupply)registroEventoSistema;
                                dr["Tipo"] = "Fonte de alimentação";

                                detalhes = "Fonte de alimentação do equipamento: ";
                                switch (registroEventoSistemaFonteAlimentacao.PowerSupplyType)
                                {
                                    case ESystemEventPowerSupplyType.PowerGrid:
                                        detalhes += "Fonte de alimentação;";
                                        break;
                                    case ESystemEventPowerSupplyType.NoBreak:
                                        detalhes += "Nobreak;";
                                        break;
                                }

                                dr["Detalhes"] = detalhes;
                            }
                            else if (registroEventoSistema is SystemEventRecord_Acceleration)
                            {
                                SystemEventRecord_Acceleration registroEventoSistemaAceleracao = (SystemEventRecord_Acceleration)registroEventoSistema;
                                dr["Tipo"] = "Aceleração";

                                detalhes = "Tipo de alteração detectada: ";
                                switch (registroEventoSistemaAceleracao.MotionType)
                                {
                                    case EMotionType.Impact:
                                        detalhes += "Impacto; ";
                                        break;
                                    case EMotionType.PositionChange:
                                        detalhes += "Mudança de posição; ";
                                        break;
                                    case EMotionType.Movement:
                                        detalhes += "Movimentação; ";
                                        break;
                                }

                                detalhes += string.Format("Valor máximo da aceleração medida: {0};", registroEventoSistemaAceleracao.Acceleration);

                                dr["Detalhes"] = detalhes;
                            }
                            else if (registroEventoSistema is SystemEventRecord_RTCBatteryUsage)
                            {
                                SystemEventRecord_RTCBatteryUsage registroEventoSistemaUsoBateriaRTC = (SystemEventRecord_RTCBatteryUsage)registroEventoSistema;
                                dr["Tipo"] = "Uso de bateria RTC";

                                detalhes = string.Format("Quantidade aproximada em minutos de consumo de bateria: {0};", registroEventoSistemaUsoBateriaRTC.RtcBatteryMinutesUsage);

                                dr["Detalhes"] = detalhes;
                            }
                            else if (registroEventoSistema is SystemEventRecord_RTCBatteryChange)
                            {
                                SystemEventRecord_RTCBatteryChange registroEventoSistemaTrocaBateriaRTC = (SystemEventRecord_RTCBatteryChange)registroEventoSistema;
                                dr["Tipo"] = "Troca de bateria RTC";

                                detalhes = string.Format("Quantidade aproximada em minutos de consumo de bateria: {0};", registroEventoSistemaTrocaBateriaRTC.RtcBatteryMinutesUsage);

                                dr["Detalhes"] = detalhes;
                            }

                            this._dtEventosSistema.Rows.Add(dr);
                        }

                        registrosEventoSistema = this._watchComm.ConfirmationReceiptSystemEventRecords();

                        if (registrosEventoSistema == null)

                            AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                ETipoComando.ConfirmaRecebimentoRegistrosEventoSistema,
                                                ETipoHistorico.ConfirmacaoRecebimento,
                                                "O relógio não possui registros.");
                        else

                            AddHistoricoComando(ETipoOrigemComando.Relogio,
                                                ETipoComando.ConfirmaRecebimentoRegistrosEventoSistema,
                                                ETipoHistorico.ConfirmacaoRecebimento,
                                                "Registros coletados...");
                    }
                }

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                AddHistoricoComando(ETipoOrigemComando.Relogio,
                                    ETipoComando.SolicitaRegistrosEventoSistema,
                                    ETipoHistorico.Erro,
                                    ex.Message);

                ErroDuranteRecepcaoDoComando(ex);
            }
            finally
            {
                if (this._watchComm != null)
                    this._watchComm.CloseConnection();
            }
        }

        private void btnLimparGridEventosSistema_Click(object sender, EventArgs e)
        {
            _dtEventosSistema = new dsSDKREP.dtEventosSistemaDataTable();
            this.dtgEventosSistema.DataSource = _dtEventosSistema;
        }
        #endregion
    }
}