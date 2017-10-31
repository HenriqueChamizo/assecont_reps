namespace ID_REP_Monitor
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrMonitor = new System.Windows.Forms.Timer(this.components);
            this.grpCommunication = new System.Windows.Forms.GroupBox();
            this.chkReconnect = new System.Windows.Forms.CheckBox();
            this.lblCommunicationMonitor = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtTemperature = new System.Windows.Forms.TextBox();
            this.chkTemperatureStatus = new System.Windows.Forms.CheckBox();
            this.chkClearAlarm = new System.Windows.Forms.CheckBox();
            this.btnClearAlarms = new System.Windows.Forms.Button();
            this.chkAllAlarms = new System.Windows.Forms.CheckBox();
            this.chkPaperStatus = new System.Windows.Forms.CheckBox();
            this.txtPaperStatus = new System.Windows.Forms.TextBox();
            this.chkBiometricSecurity = new System.Windows.Forms.CheckBox();
            this.txtBiometricSecurity = new System.Windows.Forms.TextBox();
            this.btnClearStatus = new System.Windows.Forms.Button();
            this.chkAllStatus = new System.Windows.Forms.CheckBox();
            this.chkTotalBiometricUsers = new System.Windows.Forms.CheckBox();
            this.txtTotalBiometricUsers = new System.Windows.Forms.TextBox();
            this.txtSystemVoltage = new System.Windows.Forms.TextBox();
            this.chkBiometricModuleSize = new System.Windows.Forms.CheckBox();
            this.chkSystemVoltage = new System.Windows.Forms.CheckBox();
            this.txtBiometricModuleSize = new System.Windows.Forms.TextBox();
            this.chkPrinterKM = new System.Windows.Forms.CheckBox();
            this.txtPrinterKM = new System.Windows.Forms.TextBox();
            this.txtBuzzerStatus = new System.Windows.Forms.TextBox();
            this.chkTotalCutterCuts = new System.Windows.Forms.CheckBox();
            this.chkBuzzerStatus = new System.Windows.Forms.CheckBox();
            this.txtTotalCutterCuts = new System.Windows.Forms.TextBox();
            this.txtCutterStatus = new System.Windows.Forms.TextBox();
            this.chkTotalUsers = new System.Windows.Forms.CheckBox();
            this.chkCutterStatus = new System.Windows.Forms.CheckBox();
            this.txtTotalUsers = new System.Windows.Forms.TextBox();
            this.txtTotalPrinterTickets = new System.Windows.Forms.TextBox();
            this.chkTotalNSR = new System.Windows.Forms.CheckBox();
            this.chkTotalPrinterTickets = new System.Windows.Forms.CheckBox();
            this.txtTotalNSR = new System.Windows.Forms.TextBox();
            this.lstMonitoring = new System.Windows.Forms.ListBox();
            this.chkCurrentPaperRollSize = new System.Windows.Forms.CheckBox();
            this.txtCurrentPaperRollSize = new System.Windows.Forms.TextBox();
            this.chkCurrentPaperRollKM = new System.Windows.Forms.CheckBox();
            this.txtCurrentPaperRollKM = new System.Windows.Forms.TextBox();
            this.chkCurrentPaperRollTicketsPrinted = new System.Windows.Forms.CheckBox();
            this.txtCurrentPaperRollTicketsPrinted = new System.Windows.Forms.TextBox();
            this.chkCurrentPaperRollEstimatedTickets = new System.Windows.Forms.CheckBox();
            this.txtCurrentPaperRollEstimatedTickets = new System.Windows.Forms.TextBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabStatus = new System.Windows.Forms.TabPage();
            this.chkConnectionPoolTimeout = new System.Windows.Forms.CheckBox();
            this.txtConnectionPoolTimeout = new System.Windows.Forms.TextBox();
            this.chkTotalAdminUsers = new System.Windows.Forms.CheckBox();
            this.txtTotalAdminUsers = new System.Windows.Forms.TextBox();
            this.chkTotalPasswordUsers = new System.Windows.Forms.CheckBox();
            this.txtTotalPasswordUsers = new System.Windows.Forms.TextBox();
            this.chkTotalKeyCodeUsers = new System.Windows.Forms.CheckBox();
            this.txtTotalKeyCodeUsers = new System.Windows.Forms.TextBox();
            this.chkTotalProxCardUsers = new System.Windows.Forms.CheckBox();
            this.txtTotalProxCardUsers = new System.Windows.Forms.TextBox();
            this.chkTotalBarCardUsers = new System.Windows.Forms.CheckBox();
            this.txtTotalBarCardUsers = new System.Windows.Forms.TextBox();
            this.chkPunchTicketInfo = new System.Windows.Forms.CheckBox();
            this.txtPunchTicketInfo = new System.Windows.Forms.TextBox();
            this.tabAlarms = new System.Windows.Forms.TabPage();
            this.txtReadAlarm_PaperRoll_90 = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_PaperRoll_90 = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_PaperRollChanged = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_PaperRollChanged = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_PunchTicketChanged = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_Cutter = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_PunchTicketChanged = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_Cutter = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_BiometricSecurityChanged = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_Buzzer = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_BiometricSecurityChanged = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_Buzzer = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_WatchDogReboot = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_Temperature = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_WatchDogReboot = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_Temperature = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_CommunicationChanged = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_USBFiscal = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_CommunicationChanged = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_USBFiscal = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_AdminReboot = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_USBDados = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_AdminReboot = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_USBDados = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_MasterPasswordChanged = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_NoPaper = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_MasterPasswordChanged = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_NoPaper = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_Printer_Full = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_GateOpened = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_Printer_Full = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_GateOpened = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_Printer_75 = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_24H_Pressed = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_Printer_75 = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_24H_Pressed = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_Cutter_Full = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_24H_Emitted = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_Cutter_Full = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_24H_Emitted = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_Cutter_75 = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_MRP_75 = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_Cutter_75 = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_MRP_75 = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_UnblockTried = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_MRP_Full = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_UnblockTried = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_MRP_Full = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_UnblockSuccess = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_MT_75 = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_UnblockSuccess = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_MT_75 = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_BlockViolation = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_MT_Full = new System.Windows.Forms.CheckBox();
            this.chkReadAlarm_BlockViolation = new System.Windows.Forms.CheckBox();
            this.txtReadAlarm_MT_Full = new System.Windows.Forms.TextBox();
            this.txtReadAlarm_BatteryCriticalLevel = new System.Windows.Forms.TextBox();
            this.chkReadAlarm_BatteryCriticalLevel = new System.Windows.Forms.CheckBox();
            this.pagCollect = new System.Windows.Forms.TabPage();
            this.chkRequestEventByNSR = new System.Windows.Forms.CheckBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.lstEvents = new System.Windows.Forms.ListBox();
            this.grpCommunication.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabStatus.SuspendLayout();
            this.tabAlarms.SuspendLayout();
            this.pagCollect.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrMonitor
            // 
            this.tmrMonitor.Tick += new System.EventHandler(this.tmrMonitor_Tick);
            // 
            // grpCommunication
            // 
            this.grpCommunication.Controls.Add(this.chkReconnect);
            this.grpCommunication.Controls.Add(this.lblCommunicationMonitor);
            this.grpCommunication.Controls.Add(this.btnDisconnect);
            this.grpCommunication.Controls.Add(this.btnConnect);
            this.grpCommunication.Controls.Add(this.txtPort);
            this.grpCommunication.Controls.Add(this.txtIP);
            this.grpCommunication.Controls.Add(this.lblPort);
            this.grpCommunication.Controls.Add(this.lblIP);
            this.grpCommunication.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCommunication.Location = new System.Drawing.Point(12, 12);
            this.grpCommunication.Name = "grpCommunication";
            this.grpCommunication.Size = new System.Drawing.Size(293, 112);
            this.grpCommunication.TabIndex = 1;
            this.grpCommunication.TabStop = false;
            this.grpCommunication.Text = ":: Conectar com Equipamento ::";
            // 
            // chkReconnect
            // 
            this.chkReconnect.AutoSize = true;
            this.chkReconnect.Location = new System.Drawing.Point(194, 86);
            this.chkReconnect.Name = "chkReconnect";
            this.chkReconnect.Size = new System.Drawing.Size(81, 17);
            this.chkReconnect.TabIndex = 7;
            this.chkReconnect.Text = "Reconectar";
            this.chkReconnect.UseVisualStyleBackColor = true;
            // 
            // lblCommunicationMonitor
            // 
            this.lblCommunicationMonitor.BackColor = System.Drawing.Color.Silver;
            this.lblCommunicationMonitor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommunicationMonitor.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCommunicationMonitor.Location = new System.Drawing.Point(16, 82);
            this.lblCommunicationMonitor.Name = "lblCommunicationMonitor";
            this.lblCommunicationMonitor.Size = new System.Drawing.Size(165, 23);
            this.lblCommunicationMonitor.TabIndex = 6;
            this.lblCommunicationMonitor.Text = ":: Desconectado ::";
            this.lblCommunicationMonitor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(187, 51);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(92, 23);
            this.btnDisconnect.TabIndex = 4;
            this.btnDisconnect.Text = "Desconectar";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(187, 22);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(92, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "Conectar";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(72, 53);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(109, 21);
            this.txtPort.TabIndex = 2;
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPort_KeyDown);
            // 
            // txtIP
            // 
            this.txtIP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.Location = new System.Drawing.Point(72, 24);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(109, 21);
            this.txtIP.TabIndex = 1;
            this.txtIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPort
            // 
            this.lblPort.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPort.Location = new System.Drawing.Point(16, 53);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(50, 21);
            this.lblPort.TabIndex = 1;
            this.lblPort.Text = "Porta :";
            this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIP
            // 
            this.lblIP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.Location = new System.Drawing.Point(16, 24);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(50, 21);
            this.lblIP.TabIndex = 0;
            this.lblIP.Text = "IP :";
            this.lblIP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTemperature
            // 
            this.txtTemperature.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTemperature.Location = new System.Drawing.Point(219, 41);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.ReadOnly = true;
            this.txtTemperature.Size = new System.Drawing.Size(138, 21);
            this.txtTemperature.TabIndex = 4;
            this.txtTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkTemperatureStatus
            // 
            this.chkTemperatureStatus.Location = new System.Drawing.Point(22, 41);
            this.chkTemperatureStatus.Name = "chkTemperatureStatus";
            this.chkTemperatureStatus.Size = new System.Drawing.Size(191, 21);
            this.chkTemperatureStatus.TabIndex = 6;
            this.chkTemperatureStatus.Text = "Temperatura: ";
            this.chkTemperatureStatus.UseVisualStyleBackColor = true;
            // 
            // chkClearAlarm
            // 
            this.chkClearAlarm.Location = new System.Drawing.Point(390, 14);
            this.chkClearAlarm.Name = "chkClearAlarm";
            this.chkClearAlarm.Size = new System.Drawing.Size(188, 21);
            this.chkClearAlarm.TabIndex = 86;
            this.chkClearAlarm.Text = "Limpar dados do alarme";
            this.chkClearAlarm.UseVisualStyleBackColor = true;
            // 
            // btnClearAlarms
            // 
            this.btnClearAlarms.Location = new System.Drawing.Point(144, 12);
            this.btnClearAlarms.Name = "btnClearAlarms";
            this.btnClearAlarms.Size = new System.Drawing.Size(227, 23);
            this.btnClearAlarms.TabIndex = 33;
            this.btnClearAlarms.Text = "Limpar campos";
            this.btnClearAlarms.UseVisualStyleBackColor = true;
            this.btnClearAlarms.Click += new System.EventHandler(this.btnClearAlarms_Click);
            // 
            // chkAllAlarms
            // 
            this.chkAllAlarms.Location = new System.Drawing.Point(22, 14);
            this.chkAllAlarms.Name = "chkAllAlarms";
            this.chkAllAlarms.Size = new System.Drawing.Size(116, 21);
            this.chkAllAlarms.TabIndex = 32;
            this.chkAllAlarms.Text = "Todos";
            this.chkAllAlarms.UseVisualStyleBackColor = true;
            this.chkAllAlarms.CheckedChanged += new System.EventHandler(this.chkAllAlarms_CheckedChanged);
            // 
            // chkPaperStatus
            // 
            this.chkPaperStatus.Location = new System.Drawing.Point(22, 365);
            this.chkPaperStatus.Name = "chkPaperStatus";
            this.chkPaperStatus.Size = new System.Drawing.Size(191, 21);
            this.chkPaperStatus.TabIndex = 36;
            this.chkPaperStatus.Text = "Papel: ";
            this.chkPaperStatus.UseVisualStyleBackColor = true;
            // 
            // txtPaperStatus
            // 
            this.txtPaperStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaperStatus.Location = new System.Drawing.Point(219, 365);
            this.txtPaperStatus.Name = "txtPaperStatus";
            this.txtPaperStatus.ReadOnly = true;
            this.txtPaperStatus.Size = new System.Drawing.Size(138, 21);
            this.txtPaperStatus.TabIndex = 35;
            this.txtPaperStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkBiometricSecurity
            // 
            this.chkBiometricSecurity.Location = new System.Drawing.Point(22, 338);
            this.chkBiometricSecurity.Name = "chkBiometricSecurity";
            this.chkBiometricSecurity.Size = new System.Drawing.Size(191, 21);
            this.chkBiometricSecurity.TabIndex = 33;
            this.chkBiometricSecurity.Text = "Niv. seg. bio: ";
            this.chkBiometricSecurity.UseVisualStyleBackColor = true;
            // 
            // txtBiometricSecurity
            // 
            this.txtBiometricSecurity.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBiometricSecurity.Location = new System.Drawing.Point(219, 338);
            this.txtBiometricSecurity.Name = "txtBiometricSecurity";
            this.txtBiometricSecurity.ReadOnly = true;
            this.txtBiometricSecurity.Size = new System.Drawing.Size(138, 21);
            this.txtBiometricSecurity.TabIndex = 32;
            this.txtBiometricSecurity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnClearStatus
            // 
            this.btnClearStatus.Location = new System.Drawing.Point(219, 12);
            this.btnClearStatus.Name = "btnClearStatus";
            this.btnClearStatus.Size = new System.Drawing.Size(138, 23);
            this.btnClearStatus.TabIndex = 31;
            this.btnClearStatus.Text = "Limpar";
            this.btnClearStatus.UseVisualStyleBackColor = true;
            this.btnClearStatus.Click += new System.EventHandler(this.btnClearStatus_Click);
            // 
            // chkAllStatus
            // 
            this.chkAllStatus.Location = new System.Drawing.Point(22, 14);
            this.chkAllStatus.Name = "chkAllStatus";
            this.chkAllStatus.Size = new System.Drawing.Size(116, 21);
            this.chkAllStatus.TabIndex = 30;
            this.chkAllStatus.Text = "Todos";
            this.chkAllStatus.UseVisualStyleBackColor = true;
            this.chkAllStatus.CheckedChanged += new System.EventHandler(this.chkAllStatus_CheckedChanged);
            // 
            // chkTotalBiometricUsers
            // 
            this.chkTotalBiometricUsers.Location = new System.Drawing.Point(22, 311);
            this.chkTotalBiometricUsers.Name = "chkTotalBiometricUsers";
            this.chkTotalBiometricUsers.Size = new System.Drawing.Size(191, 21);
            this.chkTotalBiometricUsers.TabIndex = 29;
            this.chkTotalBiometricUsers.Text = "Usuários com Biometria: ";
            this.chkTotalBiometricUsers.UseVisualStyleBackColor = true;
            // 
            // txtTotalBiometricUsers
            // 
            this.txtTotalBiometricUsers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBiometricUsers.Location = new System.Drawing.Point(219, 311);
            this.txtTotalBiometricUsers.Name = "txtTotalBiometricUsers";
            this.txtTotalBiometricUsers.ReadOnly = true;
            this.txtTotalBiometricUsers.Size = new System.Drawing.Size(138, 21);
            this.txtTotalBiometricUsers.TabIndex = 28;
            this.txtTotalBiometricUsers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSystemVoltage
            // 
            this.txtSystemVoltage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSystemVoltage.Location = new System.Drawing.Point(219, 149);
            this.txtSystemVoltage.Name = "txtSystemVoltage";
            this.txtSystemVoltage.ReadOnly = true;
            this.txtSystemVoltage.Size = new System.Drawing.Size(138, 21);
            this.txtSystemVoltage.TabIndex = 8;
            this.txtSystemVoltage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkBiometricModuleSize
            // 
            this.chkBiometricModuleSize.Location = new System.Drawing.Point(22, 284);
            this.chkBiometricModuleSize.Name = "chkBiometricModuleSize";
            this.chkBiometricModuleSize.Size = new System.Drawing.Size(191, 21);
            this.chkBiometricModuleSize.TabIndex = 27;
            this.chkBiometricModuleSize.Text = "Módulo biométrico: ";
            this.chkBiometricModuleSize.UseVisualStyleBackColor = true;
            // 
            // chkSystemVoltage
            // 
            this.chkSystemVoltage.Location = new System.Drawing.Point(22, 149);
            this.chkSystemVoltage.Name = "chkSystemVoltage";
            this.chkSystemVoltage.Size = new System.Drawing.Size(191, 21);
            this.chkSystemVoltage.TabIndex = 9;
            this.chkSystemVoltage.Text = "Tensão: ";
            this.chkSystemVoltage.UseVisualStyleBackColor = true;
            // 
            // txtBiometricModuleSize
            // 
            this.txtBiometricModuleSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBiometricModuleSize.Location = new System.Drawing.Point(219, 284);
            this.txtBiometricModuleSize.Name = "txtBiometricModuleSize";
            this.txtBiometricModuleSize.ReadOnly = true;
            this.txtBiometricModuleSize.Size = new System.Drawing.Size(138, 21);
            this.txtBiometricModuleSize.TabIndex = 26;
            this.txtBiometricModuleSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkPrinterKM
            // 
            this.chkPrinterKM.Location = new System.Drawing.Point(22, 257);
            this.chkPrinterKM.Name = "chkPrinterKM";
            this.chkPrinterKM.Size = new System.Drawing.Size(191, 21);
            this.chkPrinterKM.TabIndex = 25;
            this.chkPrinterKM.Text = "KM impressora: ";
            this.chkPrinterKM.UseVisualStyleBackColor = true;
            // 
            // txtPrinterKM
            // 
            this.txtPrinterKM.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrinterKM.Location = new System.Drawing.Point(219, 257);
            this.txtPrinterKM.Name = "txtPrinterKM";
            this.txtPrinterKM.ReadOnly = true;
            this.txtPrinterKM.Size = new System.Drawing.Size(138, 21);
            this.txtPrinterKM.TabIndex = 24;
            this.txtPrinterKM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBuzzerStatus
            // 
            this.txtBuzzerStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuzzerStatus.Location = new System.Drawing.Point(219, 68);
            this.txtBuzzerStatus.Name = "txtBuzzerStatus";
            this.txtBuzzerStatus.ReadOnly = true;
            this.txtBuzzerStatus.Size = new System.Drawing.Size(138, 21);
            this.txtBuzzerStatus.TabIndex = 12;
            this.txtBuzzerStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkTotalCutterCuts
            // 
            this.chkTotalCutterCuts.Location = new System.Drawing.Point(22, 230);
            this.chkTotalCutterCuts.Name = "chkTotalCutterCuts";
            this.chkTotalCutterCuts.Size = new System.Drawing.Size(191, 21);
            this.chkTotalCutterCuts.TabIndex = 23;
            this.chkTotalCutterCuts.Text = "Cortes do cutter: ";
            this.chkTotalCutterCuts.UseVisualStyleBackColor = true;
            // 
            // chkBuzzerStatus
            // 
            this.chkBuzzerStatus.Location = new System.Drawing.Point(22, 68);
            this.chkBuzzerStatus.Name = "chkBuzzerStatus";
            this.chkBuzzerStatus.Size = new System.Drawing.Size(191, 21);
            this.chkBuzzerStatus.TabIndex = 13;
            this.chkBuzzerStatus.Text = "Buzzer: ";
            this.chkBuzzerStatus.UseVisualStyleBackColor = true;
            // 
            // txtTotalCutterCuts
            // 
            this.txtTotalCutterCuts.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCutterCuts.Location = new System.Drawing.Point(219, 230);
            this.txtTotalCutterCuts.Name = "txtTotalCutterCuts";
            this.txtTotalCutterCuts.ReadOnly = true;
            this.txtTotalCutterCuts.Size = new System.Drawing.Size(138, 21);
            this.txtTotalCutterCuts.TabIndex = 22;
            this.txtTotalCutterCuts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCutterStatus
            // 
            this.txtCutterStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCutterStatus.Location = new System.Drawing.Point(219, 95);
            this.txtCutterStatus.Name = "txtCutterStatus";
            this.txtCutterStatus.ReadOnly = true;
            this.txtCutterStatus.Size = new System.Drawing.Size(138, 21);
            this.txtCutterStatus.TabIndex = 14;
            this.txtCutterStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkTotalUsers
            // 
            this.chkTotalUsers.Location = new System.Drawing.Point(22, 203);
            this.chkTotalUsers.Name = "chkTotalUsers";
            this.chkTotalUsers.Size = new System.Drawing.Size(191, 21);
            this.chkTotalUsers.TabIndex = 21;
            this.chkTotalUsers.Text = "Total usuários: ";
            this.chkTotalUsers.UseVisualStyleBackColor = true;
            // 
            // chkCutterStatus
            // 
            this.chkCutterStatus.Location = new System.Drawing.Point(22, 95);
            this.chkCutterStatus.Name = "chkCutterStatus";
            this.chkCutterStatus.Size = new System.Drawing.Size(191, 21);
            this.chkCutterStatus.TabIndex = 15;
            this.chkCutterStatus.Text = "Cutter: ";
            this.chkCutterStatus.UseVisualStyleBackColor = true;
            // 
            // txtTotalUsers
            // 
            this.txtTotalUsers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalUsers.Location = new System.Drawing.Point(219, 203);
            this.txtTotalUsers.Name = "txtTotalUsers";
            this.txtTotalUsers.ReadOnly = true;
            this.txtTotalUsers.Size = new System.Drawing.Size(138, 21);
            this.txtTotalUsers.TabIndex = 20;
            this.txtTotalUsers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTotalPrinterTickets
            // 
            this.txtTotalPrinterTickets.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPrinterTickets.Location = new System.Drawing.Point(219, 122);
            this.txtTotalPrinterTickets.Name = "txtTotalPrinterTickets";
            this.txtTotalPrinterTickets.ReadOnly = true;
            this.txtTotalPrinterTickets.Size = new System.Drawing.Size(138, 21);
            this.txtTotalPrinterTickets.TabIndex = 16;
            this.txtTotalPrinterTickets.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkTotalNSR
            // 
            this.chkTotalNSR.Location = new System.Drawing.Point(22, 176);
            this.chkTotalNSR.Name = "chkTotalNSR";
            this.chkTotalNSR.Size = new System.Drawing.Size(191, 21);
            this.chkTotalNSR.TabIndex = 19;
            this.chkTotalNSR.Text = "Total NSR: ";
            this.chkTotalNSR.UseVisualStyleBackColor = true;
            // 
            // chkTotalPrinterTickets
            // 
            this.chkTotalPrinterTickets.Location = new System.Drawing.Point(22, 122);
            this.chkTotalPrinterTickets.Name = "chkTotalPrinterTickets";
            this.chkTotalPrinterTickets.Size = new System.Drawing.Size(191, 21);
            this.chkTotalPrinterTickets.TabIndex = 17;
            this.chkTotalPrinterTickets.Text = "Tickets impressos: ";
            this.chkTotalPrinterTickets.UseVisualStyleBackColor = true;
            // 
            // txtTotalNSR
            // 
            this.txtTotalNSR.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalNSR.Location = new System.Drawing.Point(219, 176);
            this.txtTotalNSR.Name = "txtTotalNSR";
            this.txtTotalNSR.ReadOnly = true;
            this.txtTotalNSR.Size = new System.Drawing.Size(138, 21);
            this.txtTotalNSR.TabIndex = 18;
            this.txtTotalNSR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lstMonitoring
            // 
            this.lstMonitoring.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMonitoring.FormattingEnabled = true;
            this.lstMonitoring.HorizontalScrollbar = true;
            this.lstMonitoring.ItemHeight = 15;
            this.lstMonitoring.Location = new System.Drawing.Point(311, 19);
            this.lstMonitoring.Name = "lstMonitoring";
            this.lstMonitoring.Size = new System.Drawing.Size(467, 109);
            this.lstMonitoring.TabIndex = 4;
            // 
            // chkCurrentPaperRollSize
            // 
            this.chkCurrentPaperRollSize.Location = new System.Drawing.Point(379, 95);
            this.chkCurrentPaperRollSize.Name = "chkCurrentPaperRollSize";
            this.chkCurrentPaperRollSize.Size = new System.Drawing.Size(191, 21);
            this.chkCurrentPaperRollSize.TabIndex = 38;
            this.chkCurrentPaperRollSize.Text = "Tam. bobina atual: ";
            this.chkCurrentPaperRollSize.UseVisualStyleBackColor = true;
            // 
            // txtCurrentPaperRollSize
            // 
            this.txtCurrentPaperRollSize.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentPaperRollSize.Location = new System.Drawing.Point(576, 95);
            this.txtCurrentPaperRollSize.Name = "txtCurrentPaperRollSize";
            this.txtCurrentPaperRollSize.ReadOnly = true;
            this.txtCurrentPaperRollSize.Size = new System.Drawing.Size(138, 21);
            this.txtCurrentPaperRollSize.TabIndex = 37;
            this.txtCurrentPaperRollSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkCurrentPaperRollKM
            // 
            this.chkCurrentPaperRollKM.Location = new System.Drawing.Point(379, 122);
            this.chkCurrentPaperRollKM.Name = "chkCurrentPaperRollKM";
            this.chkCurrentPaperRollKM.Size = new System.Drawing.Size(191, 21);
            this.chkCurrentPaperRollKM.TabIndex = 40;
            this.chkCurrentPaperRollKM.Text = "KM bobina atual: ";
            this.chkCurrentPaperRollKM.UseVisualStyleBackColor = true;
            // 
            // txtCurrentPaperRollKM
            // 
            this.txtCurrentPaperRollKM.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentPaperRollKM.Location = new System.Drawing.Point(576, 122);
            this.txtCurrentPaperRollKM.Name = "txtCurrentPaperRollKM";
            this.txtCurrentPaperRollKM.ReadOnly = true;
            this.txtCurrentPaperRollKM.Size = new System.Drawing.Size(138, 21);
            this.txtCurrentPaperRollKM.TabIndex = 39;
            this.txtCurrentPaperRollKM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkCurrentPaperRollTicketsPrinted
            // 
            this.chkCurrentPaperRollTicketsPrinted.Location = new System.Drawing.Point(379, 151);
            this.chkCurrentPaperRollTicketsPrinted.Name = "chkCurrentPaperRollTicketsPrinted";
            this.chkCurrentPaperRollTicketsPrinted.Size = new System.Drawing.Size(191, 21);
            this.chkCurrentPaperRollTicketsPrinted.TabIndex = 42;
            this.chkCurrentPaperRollTicketsPrinted.Text = "Tickets bobina atual: ";
            this.chkCurrentPaperRollTicketsPrinted.UseVisualStyleBackColor = true;
            // 
            // txtCurrentPaperRollTicketsPrinted
            // 
            this.txtCurrentPaperRollTicketsPrinted.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentPaperRollTicketsPrinted.Location = new System.Drawing.Point(576, 151);
            this.txtCurrentPaperRollTicketsPrinted.Name = "txtCurrentPaperRollTicketsPrinted";
            this.txtCurrentPaperRollTicketsPrinted.ReadOnly = true;
            this.txtCurrentPaperRollTicketsPrinted.Size = new System.Drawing.Size(138, 21);
            this.txtCurrentPaperRollTicketsPrinted.TabIndex = 41;
            this.txtCurrentPaperRollTicketsPrinted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkCurrentPaperRollEstimatedTickets
            // 
            this.chkCurrentPaperRollEstimatedTickets.Location = new System.Drawing.Point(379, 178);
            this.chkCurrentPaperRollEstimatedTickets.Name = "chkCurrentPaperRollEstimatedTickets";
            this.chkCurrentPaperRollEstimatedTickets.Size = new System.Drawing.Size(191, 21);
            this.chkCurrentPaperRollEstimatedTickets.TabIndex = 44;
            this.chkCurrentPaperRollEstimatedTickets.Text = "Estimativa tickets: ";
            this.chkCurrentPaperRollEstimatedTickets.UseVisualStyleBackColor = true;
            // 
            // txtCurrentPaperRollEstimatedTickets
            // 
            this.txtCurrentPaperRollEstimatedTickets.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentPaperRollEstimatedTickets.Location = new System.Drawing.Point(576, 178);
            this.txtCurrentPaperRollEstimatedTickets.Name = "txtCurrentPaperRollEstimatedTickets";
            this.txtCurrentPaperRollEstimatedTickets.ReadOnly = true;
            this.txtCurrentPaperRollEstimatedTickets.Size = new System.Drawing.Size(138, 21);
            this.txtCurrentPaperRollEstimatedTickets.TabIndex = 43;
            this.txtCurrentPaperRollEstimatedTickets.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabMain
            // 
            this.tabMain.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabMain.Controls.Add(this.tabStatus);
            this.tabMain.Controls.Add(this.tabAlarms);
            this.tabMain.Controls.Add(this.pagCollect);
            this.tabMain.Location = new System.Drawing.Point(12, 134);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(770, 541);
            this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabMain.TabIndex = 93;
            // 
            // tabStatus
            // 
            this.tabStatus.Controls.Add(this.chkConnectionPoolTimeout);
            this.tabStatus.Controls.Add(this.txtConnectionPoolTimeout);
            this.tabStatus.Controls.Add(this.chkTotalAdminUsers);
            this.tabStatus.Controls.Add(this.txtTotalAdminUsers);
            this.tabStatus.Controls.Add(this.chkTotalPasswordUsers);
            this.tabStatus.Controls.Add(this.txtTotalPasswordUsers);
            this.tabStatus.Controls.Add(this.chkTotalKeyCodeUsers);
            this.tabStatus.Controls.Add(this.txtTotalKeyCodeUsers);
            this.tabStatus.Controls.Add(this.chkTotalProxCardUsers);
            this.tabStatus.Controls.Add(this.txtTotalProxCardUsers);
            this.tabStatus.Controls.Add(this.chkTotalBarCardUsers);
            this.tabStatus.Controls.Add(this.txtTotalBarCardUsers);
            this.tabStatus.Controls.Add(this.chkPunchTicketInfo);
            this.tabStatus.Controls.Add(this.txtPunchTicketInfo);
            this.tabStatus.Controls.Add(this.chkCurrentPaperRollEstimatedTickets);
            this.tabStatus.Controls.Add(this.txtCurrentPaperRollEstimatedTickets);
            this.tabStatus.Controls.Add(this.chkAllStatus);
            this.tabStatus.Controls.Add(this.chkCurrentPaperRollTicketsPrinted);
            this.tabStatus.Controls.Add(this.txtTotalNSR);
            this.tabStatus.Controls.Add(this.txtCurrentPaperRollTicketsPrinted);
            this.tabStatus.Controls.Add(this.chkTotalPrinterTickets);
            this.tabStatus.Controls.Add(this.chkCurrentPaperRollKM);
            this.tabStatus.Controls.Add(this.chkTotalNSR);
            this.tabStatus.Controls.Add(this.txtCurrentPaperRollKM);
            this.tabStatus.Controls.Add(this.txtTotalPrinterTickets);
            this.tabStatus.Controls.Add(this.chkCurrentPaperRollSize);
            this.tabStatus.Controls.Add(this.txtTotalUsers);
            this.tabStatus.Controls.Add(this.txtCurrentPaperRollSize);
            this.tabStatus.Controls.Add(this.chkCutterStatus);
            this.tabStatus.Controls.Add(this.chkPaperStatus);
            this.tabStatus.Controls.Add(this.chkTotalUsers);
            this.tabStatus.Controls.Add(this.txtPaperStatus);
            this.tabStatus.Controls.Add(this.txtCutterStatus);
            this.tabStatus.Controls.Add(this.txtTotalCutterCuts);
            this.tabStatus.Controls.Add(this.chkBiometricSecurity);
            this.tabStatus.Controls.Add(this.chkBuzzerStatus);
            this.tabStatus.Controls.Add(this.txtBiometricSecurity);
            this.tabStatus.Controls.Add(this.chkTotalCutterCuts);
            this.tabStatus.Controls.Add(this.btnClearStatus);
            this.tabStatus.Controls.Add(this.txtBuzzerStatus);
            this.tabStatus.Controls.Add(this.txtPrinterKM);
            this.tabStatus.Controls.Add(this.txtTemperature);
            this.tabStatus.Controls.Add(this.chkTotalBiometricUsers);
            this.tabStatus.Controls.Add(this.chkPrinterKM);
            this.tabStatus.Controls.Add(this.chkTemperatureStatus);
            this.tabStatus.Controls.Add(this.txtTotalBiometricUsers);
            this.tabStatus.Controls.Add(this.txtBiometricModuleSize);
            this.tabStatus.Controls.Add(this.txtSystemVoltage);
            this.tabStatus.Controls.Add(this.chkSystemVoltage);
            this.tabStatus.Controls.Add(this.chkBiometricModuleSize);
            this.tabStatus.Location = new System.Drawing.Point(4, 25);
            this.tabStatus.Name = "tabStatus";
            this.tabStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tabStatus.Size = new System.Drawing.Size(762, 512);
            this.tabStatus.TabIndex = 0;
            this.tabStatus.Text = "Monitoramento Status";
            this.tabStatus.UseVisualStyleBackColor = true;
            // 
            // chkConnectionPoolTimeout
            // 
            this.chkConnectionPoolTimeout.Location = new System.Drawing.Point(379, 205);
            this.chkConnectionPoolTimeout.Name = "chkConnectionPoolTimeout";
            this.chkConnectionPoolTimeout.Size = new System.Drawing.Size(191, 21);
            this.chkConnectionPoolTimeout.TabIndex = 58;
            this.chkConnectionPoolTimeout.Text = "Timeout de conexão: ";
            this.chkConnectionPoolTimeout.UseVisualStyleBackColor = true;
            // 
            // txtConnectionPoolTimeout
            // 
            this.txtConnectionPoolTimeout.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnectionPoolTimeout.Location = new System.Drawing.Point(576, 205);
            this.txtConnectionPoolTimeout.Name = "txtConnectionPoolTimeout";
            this.txtConnectionPoolTimeout.ReadOnly = true;
            this.txtConnectionPoolTimeout.Size = new System.Drawing.Size(138, 21);
            this.txtConnectionPoolTimeout.TabIndex = 57;
            this.txtConnectionPoolTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkTotalAdminUsers
            // 
            this.chkTotalAdminUsers.Location = new System.Drawing.Point(379, 41);
            this.chkTotalAdminUsers.Name = "chkTotalAdminUsers";
            this.chkTotalAdminUsers.Size = new System.Drawing.Size(191, 21);
            this.chkTotalAdminUsers.TabIndex = 56;
            this.chkTotalAdminUsers.Text = "Usuários Administradores: ";
            this.chkTotalAdminUsers.UseVisualStyleBackColor = true;
            // 
            // txtTotalAdminUsers
            // 
            this.txtTotalAdminUsers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAdminUsers.Location = new System.Drawing.Point(576, 41);
            this.txtTotalAdminUsers.Name = "txtTotalAdminUsers";
            this.txtTotalAdminUsers.ReadOnly = true;
            this.txtTotalAdminUsers.Size = new System.Drawing.Size(138, 21);
            this.txtTotalAdminUsers.TabIndex = 55;
            this.txtTotalAdminUsers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkTotalPasswordUsers
            // 
            this.chkTotalPasswordUsers.Location = new System.Drawing.Point(22, 473);
            this.chkTotalPasswordUsers.Name = "chkTotalPasswordUsers";
            this.chkTotalPasswordUsers.Size = new System.Drawing.Size(191, 21);
            this.chkTotalPasswordUsers.TabIndex = 54;
            this.chkTotalPasswordUsers.Text = "Usuários com Senha: ";
            this.chkTotalPasswordUsers.UseVisualStyleBackColor = true;
            // 
            // txtTotalPasswordUsers
            // 
            this.txtTotalPasswordUsers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPasswordUsers.Location = new System.Drawing.Point(219, 473);
            this.txtTotalPasswordUsers.Name = "txtTotalPasswordUsers";
            this.txtTotalPasswordUsers.ReadOnly = true;
            this.txtTotalPasswordUsers.Size = new System.Drawing.Size(138, 21);
            this.txtTotalPasswordUsers.TabIndex = 53;
            this.txtTotalPasswordUsers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkTotalKeyCodeUsers
            // 
            this.chkTotalKeyCodeUsers.Location = new System.Drawing.Point(22, 446);
            this.chkTotalKeyCodeUsers.Name = "chkTotalKeyCodeUsers";
            this.chkTotalKeyCodeUsers.Size = new System.Drawing.Size(191, 21);
            this.chkTotalKeyCodeUsers.TabIndex = 52;
            this.chkTotalKeyCodeUsers.Text = "Usuários com Código: ";
            this.chkTotalKeyCodeUsers.UseVisualStyleBackColor = true;
            // 
            // txtTotalKeyCodeUsers
            // 
            this.txtTotalKeyCodeUsers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalKeyCodeUsers.Location = new System.Drawing.Point(219, 446);
            this.txtTotalKeyCodeUsers.Name = "txtTotalKeyCodeUsers";
            this.txtTotalKeyCodeUsers.ReadOnly = true;
            this.txtTotalKeyCodeUsers.Size = new System.Drawing.Size(138, 21);
            this.txtTotalKeyCodeUsers.TabIndex = 51;
            this.txtTotalKeyCodeUsers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkTotalProxCardUsers
            // 
            this.chkTotalProxCardUsers.Location = new System.Drawing.Point(22, 419);
            this.chkTotalProxCardUsers.Name = "chkTotalProxCardUsers";
            this.chkTotalProxCardUsers.Size = new System.Drawing.Size(191, 21);
            this.chkTotalProxCardUsers.TabIndex = 50;
            this.chkTotalProxCardUsers.Text = "Usuários com Prox.: ";
            this.chkTotalProxCardUsers.UseVisualStyleBackColor = true;
            // 
            // txtTotalProxCardUsers
            // 
            this.txtTotalProxCardUsers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalProxCardUsers.Location = new System.Drawing.Point(219, 419);
            this.txtTotalProxCardUsers.Name = "txtTotalProxCardUsers";
            this.txtTotalProxCardUsers.ReadOnly = true;
            this.txtTotalProxCardUsers.Size = new System.Drawing.Size(138, 21);
            this.txtTotalProxCardUsers.TabIndex = 49;
            this.txtTotalProxCardUsers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkTotalBarCardUsers
            // 
            this.chkTotalBarCardUsers.Location = new System.Drawing.Point(22, 392);
            this.chkTotalBarCardUsers.Name = "chkTotalBarCardUsers";
            this.chkTotalBarCardUsers.Size = new System.Drawing.Size(191, 21);
            this.chkTotalBarCardUsers.TabIndex = 48;
            this.chkTotalBarCardUsers.Text = "Usuários com Barras: ";
            this.chkTotalBarCardUsers.UseVisualStyleBackColor = true;
            // 
            // txtTotalBarCardUsers
            // 
            this.txtTotalBarCardUsers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalBarCardUsers.Location = new System.Drawing.Point(219, 392);
            this.txtTotalBarCardUsers.Name = "txtTotalBarCardUsers";
            this.txtTotalBarCardUsers.ReadOnly = true;
            this.txtTotalBarCardUsers.Size = new System.Drawing.Size(138, 21);
            this.txtTotalBarCardUsers.TabIndex = 47;
            this.txtTotalBarCardUsers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkPunchTicketInfo
            // 
            this.chkPunchTicketInfo.Location = new System.Drawing.Point(379, 68);
            this.chkPunchTicketInfo.Name = "chkPunchTicketInfo";
            this.chkPunchTicketInfo.Size = new System.Drawing.Size(191, 21);
            this.chkPunchTicketInfo.TabIndex = 46;
            this.chkPunchTicketInfo.Text = "Alerta impressão: ";
            this.chkPunchTicketInfo.UseVisualStyleBackColor = true;
            // 
            // txtPunchTicketInfo
            // 
            this.txtPunchTicketInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPunchTicketInfo.Location = new System.Drawing.Point(576, 68);
            this.txtPunchTicketInfo.Name = "txtPunchTicketInfo";
            this.txtPunchTicketInfo.ReadOnly = true;
            this.txtPunchTicketInfo.Size = new System.Drawing.Size(138, 21);
            this.txtPunchTicketInfo.TabIndex = 45;
            this.txtPunchTicketInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabAlarms
            // 
            this.tabAlarms.Controls.Add(this.txtReadAlarm_PaperRoll_90);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_PaperRoll_90);
            this.tabAlarms.Controls.Add(this.chkAllAlarms);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_PaperRollChanged);
            this.tabAlarms.Controls.Add(this.btnClearAlarms);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_PaperRollChanged);
            this.tabAlarms.Controls.Add(this.chkClearAlarm);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_PunchTicketChanged);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_Cutter);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_PunchTicketChanged);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_Cutter);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_BiometricSecurityChanged);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_Buzzer);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_BiometricSecurityChanged);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_Buzzer);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_WatchDogReboot);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_Temperature);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_WatchDogReboot);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_Temperature);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_CommunicationChanged);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_USBFiscal);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_CommunicationChanged);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_USBFiscal);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_AdminReboot);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_USBDados);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_AdminReboot);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_USBDados);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_MasterPasswordChanged);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_NoPaper);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_MasterPasswordChanged);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_NoPaper);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_Printer_Full);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_GateOpened);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_Printer_Full);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_GateOpened);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_Printer_75);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_24H_Pressed);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_Printer_75);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_24H_Pressed);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_Cutter_Full);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_24H_Emitted);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_Cutter_Full);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_24H_Emitted);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_Cutter_75);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_MRP_75);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_Cutter_75);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_MRP_75);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_UnblockTried);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_MRP_Full);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_UnblockTried);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_MRP_Full);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_UnblockSuccess);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_MT_75);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_UnblockSuccess);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_MT_75);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_BlockViolation);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_MT_Full);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_BlockViolation);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_MT_Full);
            this.tabAlarms.Controls.Add(this.txtReadAlarm_BatteryCriticalLevel);
            this.tabAlarms.Controls.Add(this.chkReadAlarm_BatteryCriticalLevel);
            this.tabAlarms.Location = new System.Drawing.Point(4, 25);
            this.tabAlarms.Name = "tabAlarms";
            this.tabAlarms.Padding = new System.Windows.Forms.Padding(3);
            this.tabAlarms.Size = new System.Drawing.Size(762, 512);
            this.tabAlarms.TabIndex = 1;
            this.tabAlarms.Text = "Monitoramento Alarmes";
            this.tabAlarms.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_PaperRoll_90
            // 
            this.txtReadAlarm_PaperRoll_90.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_PaperRoll_90.Location = new System.Drawing.Point(512, 336);
            this.txtReadAlarm_PaperRoll_90.Name = "txtReadAlarm_PaperRoll_90";
            this.txtReadAlarm_PaperRoll_90.ReadOnly = true;
            this.txtReadAlarm_PaperRoll_90.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_PaperRoll_90.TabIndex = 91;
            // 
            // chkReadAlarm_PaperRoll_90
            // 
            this.chkReadAlarm_PaperRoll_90.Location = new System.Drawing.Point(390, 336);
            this.chkReadAlarm_PaperRoll_90.Name = "chkReadAlarm_PaperRoll_90";
            this.chkReadAlarm_PaperRoll_90.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_PaperRoll_90.TabIndex = 92;
            this.chkReadAlarm_PaperRoll_90.Text = "Bobina 90%: ";
            this.chkReadAlarm_PaperRoll_90.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_PaperRollChanged
            // 
            this.txtReadAlarm_PaperRollChanged.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_PaperRollChanged.Location = new System.Drawing.Point(512, 309);
            this.txtReadAlarm_PaperRollChanged.Name = "txtReadAlarm_PaperRollChanged";
            this.txtReadAlarm_PaperRollChanged.ReadOnly = true;
            this.txtReadAlarm_PaperRollChanged.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_PaperRollChanged.TabIndex = 89;
            // 
            // chkReadAlarm_PaperRollChanged
            // 
            this.chkReadAlarm_PaperRollChanged.Location = new System.Drawing.Point(390, 309);
            this.chkReadAlarm_PaperRollChanged.Name = "chkReadAlarm_PaperRollChanged";
            this.chkReadAlarm_PaperRollChanged.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_PaperRollChanged.TabIndex = 90;
            this.chkReadAlarm_PaperRollChanged.Text = "Bobina trocada: ";
            this.chkReadAlarm_PaperRollChanged.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_PunchTicketChanged
            // 
            this.txtReadAlarm_PunchTicketChanged.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_PunchTicketChanged.Location = new System.Drawing.Point(512, 282);
            this.txtReadAlarm_PunchTicketChanged.Name = "txtReadAlarm_PunchTicketChanged";
            this.txtReadAlarm_PunchTicketChanged.ReadOnly = true;
            this.txtReadAlarm_PunchTicketChanged.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_PunchTicketChanged.TabIndex = 87;
            // 
            // chkReadAlarm_Cutter
            // 
            this.chkReadAlarm_Cutter.Location = new System.Drawing.Point(22, 41);
            this.chkReadAlarm_Cutter.Name = "chkReadAlarm_Cutter";
            this.chkReadAlarm_Cutter.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_Cutter.TabIndex = 35;
            this.chkReadAlarm_Cutter.Text = "Cutter: ";
            this.chkReadAlarm_Cutter.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_PunchTicketChanged
            // 
            this.chkReadAlarm_PunchTicketChanged.Location = new System.Drawing.Point(390, 282);
            this.chkReadAlarm_PunchTicketChanged.Name = "chkReadAlarm_PunchTicketChanged";
            this.chkReadAlarm_PunchTicketChanged.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_PunchTicketChanged.TabIndex = 88;
            this.chkReadAlarm_PunchTicketChanged.Text = "Info ticket: ";
            this.chkReadAlarm_PunchTicketChanged.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_Cutter
            // 
            this.txtReadAlarm_Cutter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_Cutter.Location = new System.Drawing.Point(144, 41);
            this.txtReadAlarm_Cutter.Name = "txtReadAlarm_Cutter";
            this.txtReadAlarm_Cutter.ReadOnly = true;
            this.txtReadAlarm_Cutter.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_Cutter.TabIndex = 34;
            // 
            // txtReadAlarm_BiometricSecurityChanged
            // 
            this.txtReadAlarm_BiometricSecurityChanged.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_BiometricSecurityChanged.Location = new System.Drawing.Point(512, 255);
            this.txtReadAlarm_BiometricSecurityChanged.Name = "txtReadAlarm_BiometricSecurityChanged";
            this.txtReadAlarm_BiometricSecurityChanged.ReadOnly = true;
            this.txtReadAlarm_BiometricSecurityChanged.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_BiometricSecurityChanged.TabIndex = 84;
            // 
            // chkReadAlarm_Buzzer
            // 
            this.chkReadAlarm_Buzzer.Location = new System.Drawing.Point(22, 67);
            this.chkReadAlarm_Buzzer.Name = "chkReadAlarm_Buzzer";
            this.chkReadAlarm_Buzzer.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_Buzzer.TabIndex = 37;
            this.chkReadAlarm_Buzzer.Text = "Buzzer: ";
            this.chkReadAlarm_Buzzer.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_BiometricSecurityChanged
            // 
            this.chkReadAlarm_BiometricSecurityChanged.Location = new System.Drawing.Point(390, 255);
            this.chkReadAlarm_BiometricSecurityChanged.Name = "chkReadAlarm_BiometricSecurityChanged";
            this.chkReadAlarm_BiometricSecurityChanged.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_BiometricSecurityChanged.TabIndex = 85;
            this.chkReadAlarm_BiometricSecurityChanged.Text = "Nivel seg. bio: ";
            this.chkReadAlarm_BiometricSecurityChanged.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_Buzzer
            // 
            this.txtReadAlarm_Buzzer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_Buzzer.Location = new System.Drawing.Point(144, 67);
            this.txtReadAlarm_Buzzer.Name = "txtReadAlarm_Buzzer";
            this.txtReadAlarm_Buzzer.ReadOnly = true;
            this.txtReadAlarm_Buzzer.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_Buzzer.TabIndex = 36;
            // 
            // txtReadAlarm_WatchDogReboot
            // 
            this.txtReadAlarm_WatchDogReboot.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_WatchDogReboot.Location = new System.Drawing.Point(512, 228);
            this.txtReadAlarm_WatchDogReboot.Name = "txtReadAlarm_WatchDogReboot";
            this.txtReadAlarm_WatchDogReboot.ReadOnly = true;
            this.txtReadAlarm_WatchDogReboot.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_WatchDogReboot.TabIndex = 82;
            // 
            // chkReadAlarm_Temperature
            // 
            this.chkReadAlarm_Temperature.Location = new System.Drawing.Point(22, 94);
            this.chkReadAlarm_Temperature.Name = "chkReadAlarm_Temperature";
            this.chkReadAlarm_Temperature.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_Temperature.TabIndex = 39;
            this.chkReadAlarm_Temperature.Text = "Temperatura: ";
            this.chkReadAlarm_Temperature.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_WatchDogReboot
            // 
            this.chkReadAlarm_WatchDogReboot.Location = new System.Drawing.Point(390, 228);
            this.chkReadAlarm_WatchDogReboot.Name = "chkReadAlarm_WatchDogReboot";
            this.chkReadAlarm_WatchDogReboot.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_WatchDogReboot.TabIndex = 83;
            this.chkReadAlarm_WatchDogReboot.Text = "WatchDog reinic.: ";
            this.chkReadAlarm_WatchDogReboot.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_Temperature
            // 
            this.txtReadAlarm_Temperature.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_Temperature.Location = new System.Drawing.Point(144, 94);
            this.txtReadAlarm_Temperature.Name = "txtReadAlarm_Temperature";
            this.txtReadAlarm_Temperature.ReadOnly = true;
            this.txtReadAlarm_Temperature.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_Temperature.TabIndex = 38;
            // 
            // txtReadAlarm_CommunicationChanged
            // 
            this.txtReadAlarm_CommunicationChanged.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_CommunicationChanged.Location = new System.Drawing.Point(512, 202);
            this.txtReadAlarm_CommunicationChanged.Name = "txtReadAlarm_CommunicationChanged";
            this.txtReadAlarm_CommunicationChanged.ReadOnly = true;
            this.txtReadAlarm_CommunicationChanged.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_CommunicationChanged.TabIndex = 80;
            // 
            // chkReadAlarm_USBFiscal
            // 
            this.chkReadAlarm_USBFiscal.Location = new System.Drawing.Point(22, 121);
            this.chkReadAlarm_USBFiscal.Name = "chkReadAlarm_USBFiscal";
            this.chkReadAlarm_USBFiscal.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_USBFiscal.TabIndex = 41;
            this.chkReadAlarm_USBFiscal.Text = "USB Fiscal: ";
            this.chkReadAlarm_USBFiscal.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_CommunicationChanged
            // 
            this.chkReadAlarm_CommunicationChanged.Location = new System.Drawing.Point(390, 202);
            this.chkReadAlarm_CommunicationChanged.Name = "chkReadAlarm_CommunicationChanged";
            this.chkReadAlarm_CommunicationChanged.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_CommunicationChanged.TabIndex = 81;
            this.chkReadAlarm_CommunicationChanged.Text = "Comunic. alt.: ";
            this.chkReadAlarm_CommunicationChanged.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_USBFiscal
            // 
            this.txtReadAlarm_USBFiscal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_USBFiscal.Location = new System.Drawing.Point(144, 121);
            this.txtReadAlarm_USBFiscal.Name = "txtReadAlarm_USBFiscal";
            this.txtReadAlarm_USBFiscal.ReadOnly = true;
            this.txtReadAlarm_USBFiscal.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_USBFiscal.TabIndex = 40;
            // 
            // txtReadAlarm_AdminReboot
            // 
            this.txtReadAlarm_AdminReboot.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_AdminReboot.Location = new System.Drawing.Point(512, 175);
            this.txtReadAlarm_AdminReboot.Name = "txtReadAlarm_AdminReboot";
            this.txtReadAlarm_AdminReboot.ReadOnly = true;
            this.txtReadAlarm_AdminReboot.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_AdminReboot.TabIndex = 78;
            // 
            // chkReadAlarm_USBDados
            // 
            this.chkReadAlarm_USBDados.Location = new System.Drawing.Point(22, 148);
            this.chkReadAlarm_USBDados.Name = "chkReadAlarm_USBDados";
            this.chkReadAlarm_USBDados.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_USBDados.TabIndex = 43;
            this.chkReadAlarm_USBDados.Text = "USB Dados: ";
            this.chkReadAlarm_USBDados.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_AdminReboot
            // 
            this.chkReadAlarm_AdminReboot.Location = new System.Drawing.Point(390, 175);
            this.chkReadAlarm_AdminReboot.Name = "chkReadAlarm_AdminReboot";
            this.chkReadAlarm_AdminReboot.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_AdminReboot.TabIndex = 79;
            this.chkReadAlarm_AdminReboot.Text = "Reinic. adm.: ";
            this.chkReadAlarm_AdminReboot.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_USBDados
            // 
            this.txtReadAlarm_USBDados.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_USBDados.Location = new System.Drawing.Point(144, 148);
            this.txtReadAlarm_USBDados.Name = "txtReadAlarm_USBDados";
            this.txtReadAlarm_USBDados.ReadOnly = true;
            this.txtReadAlarm_USBDados.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_USBDados.TabIndex = 42;
            // 
            // txtReadAlarm_MasterPasswordChanged
            // 
            this.txtReadAlarm_MasterPasswordChanged.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_MasterPasswordChanged.Location = new System.Drawing.Point(512, 148);
            this.txtReadAlarm_MasterPasswordChanged.Name = "txtReadAlarm_MasterPasswordChanged";
            this.txtReadAlarm_MasterPasswordChanged.ReadOnly = true;
            this.txtReadAlarm_MasterPasswordChanged.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_MasterPasswordChanged.TabIndex = 76;
            // 
            // chkReadAlarm_NoPaper
            // 
            this.chkReadAlarm_NoPaper.Location = new System.Drawing.Point(22, 175);
            this.chkReadAlarm_NoPaper.Name = "chkReadAlarm_NoPaper";
            this.chkReadAlarm_NoPaper.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_NoPaper.TabIndex = 45;
            this.chkReadAlarm_NoPaper.Text = "Sem papel: ";
            this.chkReadAlarm_NoPaper.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_MasterPasswordChanged
            // 
            this.chkReadAlarm_MasterPasswordChanged.Location = new System.Drawing.Point(390, 148);
            this.chkReadAlarm_MasterPasswordChanged.Name = "chkReadAlarm_MasterPasswordChanged";
            this.chkReadAlarm_MasterPasswordChanged.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_MasterPasswordChanged.TabIndex = 77;
            this.chkReadAlarm_MasterPasswordChanged.Text = "Senha mestre alt.: ";
            this.chkReadAlarm_MasterPasswordChanged.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_NoPaper
            // 
            this.txtReadAlarm_NoPaper.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_NoPaper.Location = new System.Drawing.Point(144, 175);
            this.txtReadAlarm_NoPaper.Name = "txtReadAlarm_NoPaper";
            this.txtReadAlarm_NoPaper.ReadOnly = true;
            this.txtReadAlarm_NoPaper.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_NoPaper.TabIndex = 44;
            // 
            // txtReadAlarm_Printer_Full
            // 
            this.txtReadAlarm_Printer_Full.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_Printer_Full.Location = new System.Drawing.Point(512, 122);
            this.txtReadAlarm_Printer_Full.Name = "txtReadAlarm_Printer_Full";
            this.txtReadAlarm_Printer_Full.ReadOnly = true;
            this.txtReadAlarm_Printer_Full.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_Printer_Full.TabIndex = 74;
            // 
            // chkReadAlarm_GateOpened
            // 
            this.chkReadAlarm_GateOpened.Location = new System.Drawing.Point(22, 202);
            this.chkReadAlarm_GateOpened.Name = "chkReadAlarm_GateOpened";
            this.chkReadAlarm_GateOpened.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_GateOpened.TabIndex = 47;
            this.chkReadAlarm_GateOpened.Text = "Porta aberta: ";
            this.chkReadAlarm_GateOpened.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_Printer_Full
            // 
            this.chkReadAlarm_Printer_Full.Location = new System.Drawing.Point(390, 122);
            this.chkReadAlarm_Printer_Full.Name = "chkReadAlarm_Printer_Full";
            this.chkReadAlarm_Printer_Full.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_Printer_Full.TabIndex = 75;
            this.chkReadAlarm_Printer_Full.Text = "Impressora full: ";
            this.chkReadAlarm_Printer_Full.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_GateOpened
            // 
            this.txtReadAlarm_GateOpened.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_GateOpened.Location = new System.Drawing.Point(144, 202);
            this.txtReadAlarm_GateOpened.Name = "txtReadAlarm_GateOpened";
            this.txtReadAlarm_GateOpened.ReadOnly = true;
            this.txtReadAlarm_GateOpened.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_GateOpened.TabIndex = 46;
            // 
            // txtReadAlarm_Printer_75
            // 
            this.txtReadAlarm_Printer_75.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_Printer_75.Location = new System.Drawing.Point(512, 95);
            this.txtReadAlarm_Printer_75.Name = "txtReadAlarm_Printer_75";
            this.txtReadAlarm_Printer_75.ReadOnly = true;
            this.txtReadAlarm_Printer_75.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_Printer_75.TabIndex = 72;
            // 
            // chkReadAlarm_24H_Pressed
            // 
            this.chkReadAlarm_24H_Pressed.Location = new System.Drawing.Point(22, 229);
            this.chkReadAlarm_24H_Pressed.Name = "chkReadAlarm_24H_Pressed";
            this.chkReadAlarm_24H_Pressed.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_24H_Pressed.TabIndex = 49;
            this.chkReadAlarm_24H_Pressed.Text = "24H pressionado: ";
            this.chkReadAlarm_24H_Pressed.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_Printer_75
            // 
            this.chkReadAlarm_Printer_75.Location = new System.Drawing.Point(390, 95);
            this.chkReadAlarm_Printer_75.Name = "chkReadAlarm_Printer_75";
            this.chkReadAlarm_Printer_75.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_Printer_75.TabIndex = 73;
            this.chkReadAlarm_Printer_75.Text = "Impressora 75%: ";
            this.chkReadAlarm_Printer_75.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_24H_Pressed
            // 
            this.txtReadAlarm_24H_Pressed.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_24H_Pressed.Location = new System.Drawing.Point(144, 229);
            this.txtReadAlarm_24H_Pressed.Name = "txtReadAlarm_24H_Pressed";
            this.txtReadAlarm_24H_Pressed.ReadOnly = true;
            this.txtReadAlarm_24H_Pressed.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_24H_Pressed.TabIndex = 48;
            // 
            // txtReadAlarm_Cutter_Full
            // 
            this.txtReadAlarm_Cutter_Full.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_Cutter_Full.Location = new System.Drawing.Point(512, 68);
            this.txtReadAlarm_Cutter_Full.Name = "txtReadAlarm_Cutter_Full";
            this.txtReadAlarm_Cutter_Full.ReadOnly = true;
            this.txtReadAlarm_Cutter_Full.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_Cutter_Full.TabIndex = 70;
            // 
            // chkReadAlarm_24H_Emitted
            // 
            this.chkReadAlarm_24H_Emitted.Location = new System.Drawing.Point(22, 256);
            this.chkReadAlarm_24H_Emitted.Name = "chkReadAlarm_24H_Emitted";
            this.chkReadAlarm_24H_Emitted.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_24H_Emitted.TabIndex = 51;
            this.chkReadAlarm_24H_Emitted.Text = "24H emitido: ";
            this.chkReadAlarm_24H_Emitted.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_Cutter_Full
            // 
            this.chkReadAlarm_Cutter_Full.Location = new System.Drawing.Point(390, 68);
            this.chkReadAlarm_Cutter_Full.Name = "chkReadAlarm_Cutter_Full";
            this.chkReadAlarm_Cutter_Full.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_Cutter_Full.TabIndex = 71;
            this.chkReadAlarm_Cutter_Full.Text = "Cutter full: ";
            this.chkReadAlarm_Cutter_Full.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_24H_Emitted
            // 
            this.txtReadAlarm_24H_Emitted.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_24H_Emitted.Location = new System.Drawing.Point(144, 256);
            this.txtReadAlarm_24H_Emitted.Name = "txtReadAlarm_24H_Emitted";
            this.txtReadAlarm_24H_Emitted.ReadOnly = true;
            this.txtReadAlarm_24H_Emitted.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_24H_Emitted.TabIndex = 50;
            // 
            // txtReadAlarm_Cutter_75
            // 
            this.txtReadAlarm_Cutter_75.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_Cutter_75.Location = new System.Drawing.Point(512, 41);
            this.txtReadAlarm_Cutter_75.Name = "txtReadAlarm_Cutter_75";
            this.txtReadAlarm_Cutter_75.ReadOnly = true;
            this.txtReadAlarm_Cutter_75.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_Cutter_75.TabIndex = 68;
            // 
            // chkReadAlarm_MRP_75
            // 
            this.chkReadAlarm_MRP_75.Location = new System.Drawing.Point(22, 283);
            this.chkReadAlarm_MRP_75.Name = "chkReadAlarm_MRP_75";
            this.chkReadAlarm_MRP_75.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_MRP_75.TabIndex = 53;
            this.chkReadAlarm_MRP_75.Text = "MRP 75%: ";
            this.chkReadAlarm_MRP_75.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_Cutter_75
            // 
            this.chkReadAlarm_Cutter_75.Location = new System.Drawing.Point(390, 41);
            this.chkReadAlarm_Cutter_75.Name = "chkReadAlarm_Cutter_75";
            this.chkReadAlarm_Cutter_75.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_Cutter_75.TabIndex = 69;
            this.chkReadAlarm_Cutter_75.Text = "Cutter 75%: ";
            this.chkReadAlarm_Cutter_75.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_MRP_75
            // 
            this.txtReadAlarm_MRP_75.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_MRP_75.Location = new System.Drawing.Point(144, 283);
            this.txtReadAlarm_MRP_75.Name = "txtReadAlarm_MRP_75";
            this.txtReadAlarm_MRP_75.ReadOnly = true;
            this.txtReadAlarm_MRP_75.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_MRP_75.TabIndex = 52;
            // 
            // txtReadAlarm_UnblockTried
            // 
            this.txtReadAlarm_UnblockTried.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_UnblockTried.Location = new System.Drawing.Point(144, 472);
            this.txtReadAlarm_UnblockTried.Name = "txtReadAlarm_UnblockTried";
            this.txtReadAlarm_UnblockTried.ReadOnly = true;
            this.txtReadAlarm_UnblockTried.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_UnblockTried.TabIndex = 66;
            // 
            // chkReadAlarm_MRP_Full
            // 
            this.chkReadAlarm_MRP_Full.Location = new System.Drawing.Point(22, 310);
            this.chkReadAlarm_MRP_Full.Name = "chkReadAlarm_MRP_Full";
            this.chkReadAlarm_MRP_Full.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_MRP_Full.TabIndex = 55;
            this.chkReadAlarm_MRP_Full.Text = "MRP cheia: ";
            this.chkReadAlarm_MRP_Full.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_UnblockTried
            // 
            this.chkReadAlarm_UnblockTried.Location = new System.Drawing.Point(22, 472);
            this.chkReadAlarm_UnblockTried.Name = "chkReadAlarm_UnblockTried";
            this.chkReadAlarm_UnblockTried.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_UnblockTried.TabIndex = 67;
            this.chkReadAlarm_UnblockTried.Text = "Tent. desbloqueio: ";
            this.chkReadAlarm_UnblockTried.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_MRP_Full
            // 
            this.txtReadAlarm_MRP_Full.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_MRP_Full.Location = new System.Drawing.Point(144, 310);
            this.txtReadAlarm_MRP_Full.Name = "txtReadAlarm_MRP_Full";
            this.txtReadAlarm_MRP_Full.ReadOnly = true;
            this.txtReadAlarm_MRP_Full.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_MRP_Full.TabIndex = 54;
            // 
            // txtReadAlarm_UnblockSuccess
            // 
            this.txtReadAlarm_UnblockSuccess.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_UnblockSuccess.Location = new System.Drawing.Point(144, 445);
            this.txtReadAlarm_UnblockSuccess.Name = "txtReadAlarm_UnblockSuccess";
            this.txtReadAlarm_UnblockSuccess.ReadOnly = true;
            this.txtReadAlarm_UnblockSuccess.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_UnblockSuccess.TabIndex = 64;
            // 
            // chkReadAlarm_MT_75
            // 
            this.chkReadAlarm_MT_75.Location = new System.Drawing.Point(22, 337);
            this.chkReadAlarm_MT_75.Name = "chkReadAlarm_MT_75";
            this.chkReadAlarm_MT_75.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_MT_75.TabIndex = 57;
            this.chkReadAlarm_MT_75.Text = "MT 75%: ";
            this.chkReadAlarm_MT_75.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_UnblockSuccess
            // 
            this.chkReadAlarm_UnblockSuccess.Location = new System.Drawing.Point(22, 445);
            this.chkReadAlarm_UnblockSuccess.Name = "chkReadAlarm_UnblockSuccess";
            this.chkReadAlarm_UnblockSuccess.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_UnblockSuccess.TabIndex = 65;
            this.chkReadAlarm_UnblockSuccess.Text = "Desbloqueio: ";
            this.chkReadAlarm_UnblockSuccess.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_MT_75
            // 
            this.txtReadAlarm_MT_75.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_MT_75.Location = new System.Drawing.Point(144, 337);
            this.txtReadAlarm_MT_75.Name = "txtReadAlarm_MT_75";
            this.txtReadAlarm_MT_75.ReadOnly = true;
            this.txtReadAlarm_MT_75.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_MT_75.TabIndex = 56;
            // 
            // txtReadAlarm_BlockViolation
            // 
            this.txtReadAlarm_BlockViolation.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_BlockViolation.Location = new System.Drawing.Point(144, 418);
            this.txtReadAlarm_BlockViolation.Name = "txtReadAlarm_BlockViolation";
            this.txtReadAlarm_BlockViolation.ReadOnly = true;
            this.txtReadAlarm_BlockViolation.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_BlockViolation.TabIndex = 62;
            // 
            // chkReadAlarm_MT_Full
            // 
            this.chkReadAlarm_MT_Full.Location = new System.Drawing.Point(22, 364);
            this.chkReadAlarm_MT_Full.Name = "chkReadAlarm_MT_Full";
            this.chkReadAlarm_MT_Full.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_MT_Full.TabIndex = 59;
            this.chkReadAlarm_MT_Full.Text = "MT cheia: ";
            this.chkReadAlarm_MT_Full.UseVisualStyleBackColor = true;
            // 
            // chkReadAlarm_BlockViolation
            // 
            this.chkReadAlarm_BlockViolation.Location = new System.Drawing.Point(22, 418);
            this.chkReadAlarm_BlockViolation.Name = "chkReadAlarm_BlockViolation";
            this.chkReadAlarm_BlockViolation.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_BlockViolation.TabIndex = 63;
            this.chkReadAlarm_BlockViolation.Text = "Bloq. violação: ";
            this.chkReadAlarm_BlockViolation.UseVisualStyleBackColor = true;
            // 
            // txtReadAlarm_MT_Full
            // 
            this.txtReadAlarm_MT_Full.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_MT_Full.Location = new System.Drawing.Point(144, 364);
            this.txtReadAlarm_MT_Full.Name = "txtReadAlarm_MT_Full";
            this.txtReadAlarm_MT_Full.ReadOnly = true;
            this.txtReadAlarm_MT_Full.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_MT_Full.TabIndex = 58;
            // 
            // txtReadAlarm_BatteryCriticalLevel
            // 
            this.txtReadAlarm_BatteryCriticalLevel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReadAlarm_BatteryCriticalLevel.Location = new System.Drawing.Point(144, 391);
            this.txtReadAlarm_BatteryCriticalLevel.Name = "txtReadAlarm_BatteryCriticalLevel";
            this.txtReadAlarm_BatteryCriticalLevel.ReadOnly = true;
            this.txtReadAlarm_BatteryCriticalLevel.Size = new System.Drawing.Size(227, 21);
            this.txtReadAlarm_BatteryCriticalLevel.TabIndex = 60;
            // 
            // chkReadAlarm_BatteryCriticalLevel
            // 
            this.chkReadAlarm_BatteryCriticalLevel.Location = new System.Drawing.Point(22, 391);
            this.chkReadAlarm_BatteryCriticalLevel.Name = "chkReadAlarm_BatteryCriticalLevel";
            this.chkReadAlarm_BatteryCriticalLevel.Size = new System.Drawing.Size(116, 21);
            this.chkReadAlarm_BatteryCriticalLevel.TabIndex = 61;
            this.chkReadAlarm_BatteryCriticalLevel.Text = "Bateria crítica: ";
            this.chkReadAlarm_BatteryCriticalLevel.UseVisualStyleBackColor = true;
            // 
            // pagCollect
            // 
            this.pagCollect.Controls.Add(this.lstEvents);
            this.pagCollect.Controls.Add(this.chkRequestEventByNSR);
            this.pagCollect.Controls.Add(this.btnClearLog);
            this.pagCollect.Location = new System.Drawing.Point(4, 25);
            this.pagCollect.Name = "pagCollect";
            this.pagCollect.Padding = new System.Windows.Forms.Padding(3);
            this.pagCollect.Size = new System.Drawing.Size(762, 512);
            this.pagCollect.TabIndex = 2;
            this.pagCollect.Text = "Coleta de Eventos";
            this.pagCollect.UseVisualStyleBackColor = true;
            // 
            // chkRequestEventByNSR
            // 
            this.chkRequestEventByNSR.Location = new System.Drawing.Point(22, 14);
            this.chkRequestEventByNSR.Name = "chkRequestEventByNSR";
            this.chkRequestEventByNSR.Size = new System.Drawing.Size(116, 21);
            this.chkRequestEventByNSR.TabIndex = 34;
            this.chkRequestEventByNSR.Text = "Coletar Eventos";
            this.chkRequestEventByNSR.UseVisualStyleBackColor = true;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(144, 12);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(227, 23);
            this.btnClearLog.TabIndex = 35;
            this.btnClearLog.Text = "Limpar LOG";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // lstEvents
            // 
            this.lstEvents.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstEvents.FormattingEnabled = true;
            this.lstEvents.ItemHeight = 14;
            this.lstEvents.Location = new System.Drawing.Point(6, 41);
            this.lstEvents.Name = "lstEvents";
            this.lstEvents.Size = new System.Drawing.Size(750, 452);
            this.lstEvents.TabIndex = 36;
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(795, 683);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.lstMonitoring);
            this.Controls.Add(this.grpCommunication);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ID REP Monitor";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.grpCommunication.ResumeLayout(false);
            this.grpCommunication.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabStatus.ResumeLayout(false);
            this.tabStatus.PerformLayout();
            this.tabAlarms.ResumeLayout(false);
            this.tabAlarms.PerformLayout();
            this.pagCollect.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrMonitor;
        private System.Windows.Forms.GroupBox grpCommunication;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblCommunicationMonitor;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtTemperature;
        private System.Windows.Forms.CheckBox chkTemperatureStatus;
        private System.Windows.Forms.CheckBox chkSystemVoltage;
        private System.Windows.Forms.TextBox txtSystemVoltage;
        private System.Windows.Forms.CheckBox chkCutterStatus;
        private System.Windows.Forms.TextBox txtCutterStatus;
        private System.Windows.Forms.CheckBox chkBuzzerStatus;
        private System.Windows.Forms.TextBox txtBuzzerStatus;
        private System.Windows.Forms.CheckBox chkTotalPrinterTickets;
        private System.Windows.Forms.TextBox txtTotalPrinterTickets;
        private System.Windows.Forms.CheckBox chkTotalNSR;
        private System.Windows.Forms.TextBox txtTotalNSR;
        private System.Windows.Forms.CheckBox chkTotalUsers;
        private System.Windows.Forms.TextBox txtTotalUsers;
        private System.Windows.Forms.CheckBox chkTotalCutterCuts;
        private System.Windows.Forms.TextBox txtTotalCutterCuts;
        private System.Windows.Forms.CheckBox chkPrinterKM;
        private System.Windows.Forms.TextBox txtPrinterKM;
        private System.Windows.Forms.CheckBox chkBiometricModuleSize;
        private System.Windows.Forms.TextBox txtBiometricModuleSize;
        private System.Windows.Forms.CheckBox chkTotalBiometricUsers;
        private System.Windows.Forms.TextBox txtTotalBiometricUsers;
        private System.Windows.Forms.CheckBox chkAllStatus;
        private System.Windows.Forms.Button btnClearStatus;
        private System.Windows.Forms.Button btnClearAlarms;
        private System.Windows.Forms.CheckBox chkAllAlarms;
        private System.Windows.Forms.CheckBox chkClearAlarm;
        private System.Windows.Forms.ListBox lstMonitoring;
        private System.Windows.Forms.CheckBox chkBiometricSecurity;
        private System.Windows.Forms.TextBox txtBiometricSecurity;
        private System.Windows.Forms.CheckBox chkPaperStatus;
        private System.Windows.Forms.TextBox txtPaperStatus;
        private System.Windows.Forms.CheckBox chkReconnect;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabStatus;
        private System.Windows.Forms.TabPage tabAlarms;
        private System.Windows.Forms.CheckBox chkCurrentPaperRollEstimatedTickets;
        private System.Windows.Forms.TextBox txtCurrentPaperRollEstimatedTickets;
        private System.Windows.Forms.CheckBox chkCurrentPaperRollTicketsPrinted;
        private System.Windows.Forms.TextBox txtCurrentPaperRollTicketsPrinted;
        private System.Windows.Forms.CheckBox chkCurrentPaperRollKM;
        private System.Windows.Forms.TextBox txtCurrentPaperRollKM;
        private System.Windows.Forms.CheckBox chkCurrentPaperRollSize;
        private System.Windows.Forms.TextBox txtCurrentPaperRollSize;
        private System.Windows.Forms.TextBox txtReadAlarm_PaperRoll_90;
        private System.Windows.Forms.CheckBox chkReadAlarm_PaperRoll_90;
        private System.Windows.Forms.TextBox txtReadAlarm_PaperRollChanged;
        private System.Windows.Forms.CheckBox chkReadAlarm_PaperRollChanged;
        private System.Windows.Forms.TextBox txtReadAlarm_PunchTicketChanged;
        private System.Windows.Forms.CheckBox chkReadAlarm_Cutter;
        private System.Windows.Forms.CheckBox chkReadAlarm_PunchTicketChanged;
        private System.Windows.Forms.TextBox txtReadAlarm_Cutter;
        private System.Windows.Forms.TextBox txtReadAlarm_BiometricSecurityChanged;
        private System.Windows.Forms.CheckBox chkReadAlarm_Buzzer;
        private System.Windows.Forms.CheckBox chkReadAlarm_BiometricSecurityChanged;
        private System.Windows.Forms.TextBox txtReadAlarm_Buzzer;
        private System.Windows.Forms.TextBox txtReadAlarm_WatchDogReboot;
        private System.Windows.Forms.CheckBox chkReadAlarm_Temperature;
        private System.Windows.Forms.CheckBox chkReadAlarm_WatchDogReboot;
        private System.Windows.Forms.TextBox txtReadAlarm_Temperature;
        private System.Windows.Forms.TextBox txtReadAlarm_CommunicationChanged;
        private System.Windows.Forms.CheckBox chkReadAlarm_USBFiscal;
        private System.Windows.Forms.CheckBox chkReadAlarm_CommunicationChanged;
        private System.Windows.Forms.TextBox txtReadAlarm_USBFiscal;
        private System.Windows.Forms.TextBox txtReadAlarm_AdminReboot;
        private System.Windows.Forms.CheckBox chkReadAlarm_USBDados;
        private System.Windows.Forms.CheckBox chkReadAlarm_AdminReboot;
        private System.Windows.Forms.TextBox txtReadAlarm_USBDados;
        private System.Windows.Forms.TextBox txtReadAlarm_MasterPasswordChanged;
        private System.Windows.Forms.CheckBox chkReadAlarm_NoPaper;
        private System.Windows.Forms.CheckBox chkReadAlarm_MasterPasswordChanged;
        private System.Windows.Forms.TextBox txtReadAlarm_NoPaper;
        private System.Windows.Forms.TextBox txtReadAlarm_Printer_Full;
        private System.Windows.Forms.CheckBox chkReadAlarm_GateOpened;
        private System.Windows.Forms.CheckBox chkReadAlarm_Printer_Full;
        private System.Windows.Forms.TextBox txtReadAlarm_GateOpened;
        private System.Windows.Forms.TextBox txtReadAlarm_Printer_75;
        private System.Windows.Forms.CheckBox chkReadAlarm_24H_Pressed;
        private System.Windows.Forms.CheckBox chkReadAlarm_Printer_75;
        private System.Windows.Forms.TextBox txtReadAlarm_24H_Pressed;
        private System.Windows.Forms.TextBox txtReadAlarm_Cutter_Full;
        private System.Windows.Forms.CheckBox chkReadAlarm_24H_Emitted;
        private System.Windows.Forms.CheckBox chkReadAlarm_Cutter_Full;
        private System.Windows.Forms.TextBox txtReadAlarm_24H_Emitted;
        private System.Windows.Forms.TextBox txtReadAlarm_Cutter_75;
        private System.Windows.Forms.CheckBox chkReadAlarm_MRP_75;
        private System.Windows.Forms.CheckBox chkReadAlarm_Cutter_75;
        private System.Windows.Forms.TextBox txtReadAlarm_MRP_75;
        private System.Windows.Forms.TextBox txtReadAlarm_UnblockTried;
        private System.Windows.Forms.CheckBox chkReadAlarm_MRP_Full;
        private System.Windows.Forms.CheckBox chkReadAlarm_UnblockTried;
        private System.Windows.Forms.TextBox txtReadAlarm_MRP_Full;
        private System.Windows.Forms.TextBox txtReadAlarm_UnblockSuccess;
        private System.Windows.Forms.CheckBox chkReadAlarm_MT_75;
        private System.Windows.Forms.CheckBox chkReadAlarm_UnblockSuccess;
        private System.Windows.Forms.TextBox txtReadAlarm_MT_75;
        private System.Windows.Forms.TextBox txtReadAlarm_BlockViolation;
        private System.Windows.Forms.CheckBox chkReadAlarm_MT_Full;
        private System.Windows.Forms.CheckBox chkReadAlarm_BlockViolation;
        private System.Windows.Forms.TextBox txtReadAlarm_MT_Full;
        private System.Windows.Forms.TextBox txtReadAlarm_BatteryCriticalLevel;
        private System.Windows.Forms.CheckBox chkReadAlarm_BatteryCriticalLevel;
        private System.Windows.Forms.CheckBox chkPunchTicketInfo;
        private System.Windows.Forms.TextBox txtPunchTicketInfo;
        private System.Windows.Forms.CheckBox chkTotalAdminUsers;
        private System.Windows.Forms.TextBox txtTotalAdminUsers;
        private System.Windows.Forms.CheckBox chkTotalPasswordUsers;
        private System.Windows.Forms.TextBox txtTotalPasswordUsers;
        private System.Windows.Forms.CheckBox chkTotalKeyCodeUsers;
        private System.Windows.Forms.TextBox txtTotalKeyCodeUsers;
        private System.Windows.Forms.CheckBox chkTotalProxCardUsers;
        private System.Windows.Forms.TextBox txtTotalProxCardUsers;
        private System.Windows.Forms.CheckBox chkTotalBarCardUsers;
        private System.Windows.Forms.TextBox txtTotalBarCardUsers;
        private System.Windows.Forms.CheckBox chkConnectionPoolTimeout;
        private System.Windows.Forms.TextBox txtConnectionPoolTimeout;
        private System.Windows.Forms.TabPage pagCollect;
        private System.Windows.Forms.CheckBox chkRequestEventByNSR;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.ListBox lstEvents;
    }
}

