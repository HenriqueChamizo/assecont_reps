using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using iddata.interfaceIDSysR30;
using iddata.interfaceIDSysR30.controller;

using ID_DATA.Framework.Communication.CommSocket;
using ID_DATA.Framework.Communication.CommSocket.Controller.Ctrl_SocketClient;

namespace ID_REP_Monitor
{
    public partial class FormMain : Form
    {
        #region enum
        
            private enum enumStep
            {
                None,
                Connect,
                Disconnect,
                Load,
                Read,
                ReceivedResponse,
                WaitingConnection,
                WaitingResponse
            }

            private enum enumMonitor
            {
                RequestEventByNSR,
                Temperature,
                BuzzerStatus,
                CutterStatus,
                TotalPrinterTickets,
                SystemVoltage,
                TotalNSR,
                TotalUsers,
                TotalCutterCuts,
                PrinterKM,
                BiometricModuleSize,
                TotalBiometricUsers,
                BiometricSecurity,
                PaperStatus,
                TotalBarCardUsers,
                TotalProxCardUsers,
                TotalKeyCodeUsers,
                TotalPasswordUsers,
                TotalAdminUsers,
                PunchTicketInfo,
                ConnectionPoolTimeout,
                CurrentPaperRollSize,
                CurrentPaperRollKM,
                CurrentPaperRollTicktesPrinted,
                CurrentPaperRollEstimatedTickets,
                ReadAlarm_Cutter,
                ReadAlarm_Buzzer,
                ReadAlarm_Temperature,
                ReadAlarm_USBFiscal,
                ReadAlarm_USBDados,
                ReadAlarm_NoPaper,
                ReadAlarm_GateOpened,
                ReadAlarm_24H_Pressed,
                ReadAlarm_24H_Emitted,
                ReadAlarm_MRP_75,
                ReadAlarm_MRP_Full,
                ReadAlarm_MT_75,
                ReadAlarm_MT_Full,
                ReadAlarm_BatteryCriticalLevel,
                ReadAlarm_BlockViolation,
                ReadAlarm_UnblockSuccess,
                ReadAlarm_UnblockTried,
                ReadAlarm_Cutter_75,
                ReadAlarm_Cutter_Full,
                ReadAlarm_Printer_75,
                ReadAlarm_Printer_Full,
                ReadAlarm_MasterPasswordChanged,
                ReadAlarm_AdminReboot,
                ReadAlarm_CommunicationChanged,
                ReadAlarm_WatchDogReboot,
                ReadAlarm_BiometricSecurityChanged,
                ReadAlarm_PunchTicketChanged,
                ReadAlarm_PaperRollChanged,
                ReadAlarm_PaperRoll_90,
                ClearAlarm_Cutter,
                ClearAlarm_Buzzer,
                ClearAlarm_Temperature,
                ClearAlarm_USBFiscal,
                ClearAlarm_USBDados,
                ClearAlarm_NoPaper,
                ClearAlarm_GateOpened,
                ClearAlarm_24H_Pressed,
                ClearAlarm_24H_Emitted,
                ClearAlarm_MRP_75,
                ClearAlarm_MRP_Full,
                ClearAlarm_MT_75,
                ClearAlarm_MT_Full,
                ClearAlarm_BatteryCriticalLevel,
                ClearAlarm_BlockViolation,
                ClearAlarm_UnblockSuccess,
                ClearAlarm_UnblockTried,
                ClearAlarm_Cutter_75,
                ClearAlarm_Cutter_Full,
                ClearAlarm_Printer_75,
                ClearAlarm_Printer_Full,
                ClearAlarm_MasterPasswordChanged,
                ClearAlarm_AdminReboot,
                ClearAlarm_CommunicationChanged,
                ClearAlarm_WatchDogReboot,
                ClearAlarm_BiometricSecurityChanged,
                ClearAlarm_PunchTicketChanged,
                ClearAlarm_PaperRollChanged,
                ClearAlarm_PaperRoll_90
            }

        #endregion

        #region const

            private const int TIMEOUT = 5; // segundos
            
        #endregion

        #region attributes

            private CCtrl_IDSysR30 objCtrl_IDSysR30;
            private CCtrl_SocketClient objCtrl_SocketClient;

            private enumStep enuProcessingStep;
            private DateTime dtmTimeOut;

            private enumConnectionState connCurrent;
            private enumConnectionState connLast;

            private int iDisconnectedCount;

            private string strIP;
            private int iPort;

            private int iLastNSR;

            private Queue<enumMonitor> queueMonitoring;

        #endregion

        #region constructor
            
            public FormMain()
            {
                InitializeComponent();

                try
                {
                    this.objCtrl_IDSysR30 = new CCtrl_IDSysR30();
                }
                catch (Exception exError)
                {
                    this.grpCommunication.Enabled = false;

                    MessageBox.Show(exError.Message);
                }

                this.enuProcessingStep = enumStep.None;
                this.connCurrent = enumConnectionState.None;
                this.connLast = enumConnectionState.None;
                this.strIP = "";
                this.iPort = 0;

                this.iLastNSR = 0;

                this.queueMonitoring = new Queue<enumMonitor>();

                this.iDisconnectedCount = 0;
            }

        #endregion

        #region events

            private void FormMain_Load(object sender, EventArgs e)
            {
                
            }

            private void btnConnect_Click(object sender, EventArgs e)
            {
                try
                {
                    this.strIP = this.txtIP.Text;
                    int.TryParse(this.txtPort.Text, out this.iPort);

                    this.enuProcessingStep = enumStep.Connect;
                    this.tmrMonitor.Enabled = true;
                }
                catch (Exception exError)
                {
                    MessageBox.Show(exError.Message);
                }
            }

            private void btnDisconnect_Click(object sender, EventArgs e)
            {
                this.enuProcessingStep = enumStep.Disconnect;
            }

            private void txtPort_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.btnConnect_Click(sender, e);
                }
            }

            private void chkAllStatus_CheckedChanged(object sender, EventArgs e)
            {
                this.chkTemperatureStatus.Checked = this.chkAllStatus.Checked;
                this.chkBuzzerStatus.Checked = this.chkAllStatus.Checked;
                this.chkCutterStatus.Checked = this.chkAllStatus.Checked;
                this.chkTotalPrinterTickets.Checked = this.chkAllStatus.Checked;
                this.chkSystemVoltage.Checked = this.chkAllStatus.Checked;
                this.chkTotalNSR.Checked = this.chkAllStatus.Checked;
                this.chkTotalUsers.Checked = this.chkAllStatus.Checked;
                this.chkTotalCutterCuts.Checked = this.chkAllStatus.Checked;
                this.chkPrinterKM.Checked = this.chkAllStatus.Checked;
                this.chkBiometricModuleSize.Checked = this.chkAllStatus.Checked;
                this.chkTotalBiometricUsers.Checked = this.chkAllStatus.Checked;
                this.chkBiometricSecurity.Checked = this.chkAllStatus.Checked;
                this.chkPaperStatus.Checked = this.chkAllStatus.Checked;
                this.chkTotalBarCardUsers.Checked = this.chkAllStatus.Checked;
                this.chkTotalProxCardUsers.Checked = this.chkAllStatus.Checked;
                this.chkTotalKeyCodeUsers.Checked = this.chkAllStatus.Checked;
                this.chkTotalPasswordUsers.Checked = this.chkAllStatus.Checked;
                this.chkTotalAdminUsers.Checked = this.chkAllStatus.Checked;
                this.chkPunchTicketInfo.Checked = this.chkAllStatus.Checked;
                this.chkCurrentPaperRollSize.Checked = this.chkAllStatus.Checked;
                this.chkCurrentPaperRollKM.Checked = this.chkAllStatus.Checked;
                this.chkCurrentPaperRollTicketsPrinted.Checked = this.chkAllStatus.Checked;
                this.chkCurrentPaperRollEstimatedTickets.Checked = this.chkAllStatus.Checked;
                this.chkConnectionPoolTimeout.Checked = this.chkAllStatus.Checked;
            }

            private void chkAllAlarms_CheckedChanged(object sender, EventArgs e)
            {
                this.chkReadAlarm_Cutter.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_Buzzer.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_Temperature.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_USBFiscal.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_USBDados.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_NoPaper.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_GateOpened.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_24H_Pressed.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_24H_Emitted.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_MRP_75.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_MRP_Full.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_MT_75.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_MT_Full.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_BatteryCriticalLevel.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_BlockViolation.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_UnblockSuccess.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_UnblockTried.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_Cutter_75.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_Cutter_Full.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_Printer_75.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_Printer_Full.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_MasterPasswordChanged.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_AdminReboot.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_CommunicationChanged.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_WatchDogReboot.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_BiometricSecurityChanged.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_PunchTicketChanged.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_PaperRollChanged.Checked = this.chkAllAlarms.Checked;
                this.chkReadAlarm_PaperRoll_90.Checked = this.chkAllAlarms.Checked;
            }

            private void btnClearStatus_Click(object sender, EventArgs e)
            {
                this.txtTemperature.Text = "";
                this.txtBuzzerStatus.Text = "";
                this.txtCutterStatus.Text = "";
                this.txtTotalPrinterTickets.Text = "";
                this.txtSystemVoltage.Text = "";
                this.txtTotalNSR.Text = "";
                this.txtTotalUsers.Text = "";
                this.txtTotalCutterCuts.Text = "";
                this.txtPrinterKM.Text = "";
                this.txtBiometricModuleSize.Text = "";
                this.txtTotalBiometricUsers.Text = "";
                this.txtBiometricSecurity.Text = "";
                this.txtPaperStatus.Text = "";
                this.txtTotalBarCardUsers.Text = "";
                this.txtTotalProxCardUsers.Text = "";
                this.txtTotalKeyCodeUsers.Text = "";
                this.txtTotalPasswordUsers.Text = "";
                this.txtTotalAdminUsers.Text = "";
                this.txtPunchTicketInfo.Text = "";
                this.txtCurrentPaperRollSize.Text = "";
                this.txtCurrentPaperRollKM.Text = "";
                this.txtCurrentPaperRollTicketsPrinted.Text = "";
                this.txtCurrentPaperRollEstimatedTickets.Text = "";
                this.txtConnectionPoolTimeout.Text = "";
            }

            private void btnClearAlarms_Click(object sender, EventArgs e)
            {
                this.txtReadAlarm_Cutter.Text = "";
                this.txtReadAlarm_Buzzer.Text = "";
                this.txtReadAlarm_Temperature.Text = "";
                this.txtReadAlarm_USBFiscal.Text = "";
                this.txtReadAlarm_USBDados.Text = "";
                this.txtReadAlarm_NoPaper.Text = "";
                this.txtReadAlarm_GateOpened.Text = "";
                this.txtReadAlarm_24H_Pressed.Text = "";
                this.txtReadAlarm_24H_Emitted.Text = "";
                this.txtReadAlarm_MRP_75.Text = "";
                this.txtReadAlarm_MRP_Full.Text = "";
                this.txtReadAlarm_MT_75.Text = "";
                this.txtReadAlarm_MT_Full.Text = "";
                this.txtReadAlarm_BatteryCriticalLevel.Text = "";
                this.txtReadAlarm_BlockViolation.Text = "";
                this.txtReadAlarm_UnblockSuccess.Text = "";
                this.txtReadAlarm_UnblockTried.Text = "";
                this.txtReadAlarm_Cutter_75.Text = "";
                this.txtReadAlarm_Cutter_Full.Text = "";
                this.txtReadAlarm_Printer_75.Text = "";
                this.txtReadAlarm_Printer_Full.Text = "";
                this.txtReadAlarm_MasterPasswordChanged.Text = "";
                this.txtReadAlarm_AdminReboot.Text = "";
                this.txtReadAlarm_CommunicationChanged.Text = "";
                this.txtReadAlarm_WatchDogReboot.Text = "";
                this.txtReadAlarm_BiometricSecurityChanged.Text = "";
                this.txtReadAlarm_PunchTicketChanged.Text = "";
                this.txtReadAlarm_PaperRollChanged.Text = "";
                this.txtReadAlarm_PaperRoll_90.Text = "";
            }

            private void btnClearLog_Click(object sender, EventArgs e)
            {
                this.lstEvents.Items.Clear();
            }

            private void tmrMonitor_Tick(object sender, EventArgs e)
            {
                try
                {
                    switch (this.enuProcessingStep)
                    {
                        case enumStep.Connect:
                            #region Connect
                            {
                                this.objCtrl_SocketClient = new CCtrl_SocketClient(this.strIP, this.iPort);

                                if (this.objCtrl_SocketClient.Connect())
                                {
                                    this.btnConnect.Enabled = false;
                                    this.btnDisconnect.Enabled = false;
                                }

                                this.enuProcessingStep = enumStep.WaitingConnection;
                                this.dtmTimeOut = DateTime.Now.AddSeconds(TIMEOUT);

                                break;
                            }
                            #endregion
                        case enumStep.Disconnect:
                            #region Disconnect
                            {
                                this.objCtrl_SocketClient.Disconnect();

                                this.enuProcessingStep = enumStep.WaitingConnection;
                                this.dtmTimeOut = DateTime.Now.AddSeconds(TIMEOUT);

                                break;
                            }
                            #endregion
                        case enumStep.Load:
                            #region Load
                            {
                                if (this.chkTemperatureStatus.Checked) { this.queueMonitoring.Enqueue(enumMonitor.Temperature); }
                                if (this.chkBuzzerStatus.Checked) { this.queueMonitoring.Enqueue(enumMonitor.BuzzerStatus); }
                                if (this.chkCutterStatus.Checked) { this.queueMonitoring.Enqueue(enumMonitor.CutterStatus); }
                                if (this.chkTotalPrinterTickets.Checked) { this.queueMonitoring.Enqueue(enumMonitor.TotalPrinterTickets); }
                                if (this.chkSystemVoltage.Checked) { this.queueMonitoring.Enqueue(enumMonitor.SystemVoltage); }
                                if (this.chkTotalNSR.Checked) { this.queueMonitoring.Enqueue(enumMonitor.TotalNSR); }
                                if (this.chkTotalUsers.Checked) { this.queueMonitoring.Enqueue(enumMonitor.TotalUsers); }
                                if (this.chkTotalCutterCuts.Checked) { this.queueMonitoring.Enqueue(enumMonitor.TotalCutterCuts); }
                                if (this.chkPrinterKM.Checked) { this.queueMonitoring.Enqueue(enumMonitor.PrinterKM); }
                                if (this.chkBiometricModuleSize.Checked) { this.queueMonitoring.Enqueue(enumMonitor.BiometricModuleSize); }
                                if (this.chkTotalBiometricUsers.Checked) { this.queueMonitoring.Enqueue(enumMonitor.TotalBiometricUsers); }
                                if (this.chkBiometricSecurity.Checked) { this.queueMonitoring.Enqueue(enumMonitor.BiometricSecurity); }
                                if (this.chkPaperStatus.Checked) { this.queueMonitoring.Enqueue(enumMonitor.PaperStatus); }
                                if (this.chkTotalBarCardUsers.Checked) { this.queueMonitoring.Enqueue(enumMonitor.TotalBarCardUsers); }
                                if (this.chkTotalProxCardUsers.Checked) { this.queueMonitoring.Enqueue(enumMonitor.TotalProxCardUsers); }
                                if (this.chkTotalKeyCodeUsers.Checked) { this.queueMonitoring.Enqueue(enumMonitor.TotalKeyCodeUsers); }
                                if (this.chkTotalPasswordUsers.Checked) { this.queueMonitoring.Enqueue(enumMonitor.TotalPasswordUsers); }
                                if (this.chkTotalAdminUsers.Checked) { this.queueMonitoring.Enqueue(enumMonitor.TotalAdminUsers); }
                                if (this.chkPunchTicketInfo.Checked) { this.queueMonitoring.Enqueue(enumMonitor.PunchTicketInfo); }
                                if (this.chkCurrentPaperRollSize.Checked) { this.queueMonitoring.Enqueue(enumMonitor.CurrentPaperRollSize); }
                                if (this.chkCurrentPaperRollKM.Checked) { this.queueMonitoring.Enqueue(enumMonitor.CurrentPaperRollKM); }
                                if (this.chkCurrentPaperRollTicketsPrinted.Checked) { this.queueMonitoring.Enqueue(enumMonitor.CurrentPaperRollTicktesPrinted); }
                                if (this.chkCurrentPaperRollEstimatedTickets.Checked) { this.queueMonitoring.Enqueue(enumMonitor.CurrentPaperRollEstimatedTickets); }
                                if (this.chkConnectionPoolTimeout.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ConnectionPoolTimeout); }

                                if (this.chkReadAlarm_Cutter.Checked) 
                                { 
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_Cutter);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_Cutter); }
                                }
                                if (this.chkReadAlarm_Buzzer.Checked) 
                                { 
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_Buzzer);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_Buzzer); }
                                }
                                if (this.chkReadAlarm_Temperature.Checked) 
                                { 
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_Temperature);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_Temperature); }
                                }
                                if (this.chkReadAlarm_USBFiscal.Checked) 
                                { 
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_USBFiscal);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_USBFiscal); }
                                }
                                if (this.chkReadAlarm_USBDados.Checked) 
                                { 
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_USBDados);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_USBDados); }
                                }
                                if (this.chkReadAlarm_NoPaper.Checked) 
                                { 
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_NoPaper);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_NoPaper); }
                                }
                                if (this.chkReadAlarm_GateOpened.Checked) 
                                { 
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_GateOpened);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_GateOpened); }
                                }
                                if (this.chkReadAlarm_24H_Pressed.Checked) 
                                { 
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_24H_Pressed);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_24H_Pressed); }
                                }
                                if (this.chkReadAlarm_24H_Emitted.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_24H_Emitted);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_24H_Emitted); }
                                }
                                if (this.chkReadAlarm_MRP_75.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_MRP_75);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_MRP_75); }
                                }
                                if (this.chkReadAlarm_MRP_Full.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_MRP_Full);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_MRP_Full); }
                                }
                                if (this.chkReadAlarm_MT_75.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_MT_75);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_MT_75); }
                                }
                                if (this.chkReadAlarm_MT_Full.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_MT_Full);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_MT_Full); }
                                }
                                if (this.chkReadAlarm_BatteryCriticalLevel.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_BatteryCriticalLevel);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_BatteryCriticalLevel); }
                                }
                                if (this.chkReadAlarm_BlockViolation.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_BlockViolation);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_BlockViolation); }
                                }
                                if (this.chkReadAlarm_UnblockSuccess.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_UnblockSuccess);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_UnblockSuccess); }
                                }
                                if (this.chkReadAlarm_UnblockTried.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_UnblockTried);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_UnblockTried); }
                                }
                                if (this.chkReadAlarm_Cutter_75.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_Cutter_75);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_Cutter_75); }
                                }
                                if (this.chkReadAlarm_Cutter_Full.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_Cutter_Full);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_Cutter_Full); }
                                }
                                if (this.chkReadAlarm_Printer_75.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_Printer_75);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_Printer_75); }
                                }
                                if (this.chkReadAlarm_Printer_Full.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_Printer_Full);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_Printer_Full); }
                                }
                                if (this.chkReadAlarm_MasterPasswordChanged.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_MasterPasswordChanged);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_MasterPasswordChanged); }
                                }
                                if (this.chkReadAlarm_AdminReboot.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_AdminReboot);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_AdminReboot); }
                                }
                                if (this.chkReadAlarm_CommunicationChanged.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_CommunicationChanged);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_CommunicationChanged); }
                                }
                                if (this.chkReadAlarm_WatchDogReboot.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_WatchDogReboot);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_WatchDogReboot); }
                                }
                                if (this.chkReadAlarm_BiometricSecurityChanged.Checked) 
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_BiometricSecurityChanged);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_BiometricSecurityChanged); }
                                }
                                if (this.chkReadAlarm_PunchTicketChanged.Checked)
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_PunchTicketChanged);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_PunchTicketChanged); }
                                }
                                if (this.chkReadAlarm_PaperRollChanged.Checked)
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_PaperRollChanged);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_PaperRollChanged); }
                                }
                                if (this.chkReadAlarm_PaperRoll_90.Checked)
                                {
                                    this.queueMonitoring.Enqueue(enumMonitor.ReadAlarm_PaperRoll_90);
                                    if (this.chkClearAlarm.Checked) { this.queueMonitoring.Enqueue(enumMonitor.ClearAlarm_PaperRoll_90); }
                                }

                                if (this.chkRequestEventByNSR.Checked) { this.queueMonitoring.Enqueue(enumMonitor.RequestEventByNSR); }

                                this.enuProcessingStep = enumStep.Read;

                                break;
                            }
                            #endregion
                        case enumStep.Read:
                            #region Read
                            {
                                if (this.queueMonitoring.Count == 0)
                                {
                                    this.enuProcessingStep = enumStep.Load;
                                    break;
                                }
                            
                                switch (this.queueMonitoring.First())
                                {
                                    default:
                                        {
                                            this.queueMonitoring.Dequeue();
                                            break;
                                        }
                                    case enumMonitor.Temperature:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestTemperature());
                                            this.txtTemperature.Focus();
                                            this.txtTemperature.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.BuzzerStatus:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestBuzzerStatus());
                                            this.txtBuzzerStatus.Focus();
                                            this.txtBuzzerStatus.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.CutterStatus:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestCutterStatus());
                                            this.txtCutterStatus.Focus();
                                            this.txtCutterStatus.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.TotalPrinterTickets:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestTotalPrinterTickets());
                                            this.txtTotalPrinterTickets.Focus();
                                            this.txtTotalPrinterTickets.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.SystemVoltage:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestSystemVoltage());
                                            this.txtSystemVoltage.Focus();
                                            this.txtSystemVoltage.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.TotalNSR:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestTotalNSR());
                                            this.txtTotalNSR.Focus();
                                            this.txtTotalNSR.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.TotalUsers:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestTotalUsers());
                                            this.txtTotalUsers.Focus();
                                            this.txtTotalUsers.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.TotalCutterCuts:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestTotalCutterCuts());
                                            this.txtTotalCutterCuts.Focus();
                                            this.txtTotalCutterCuts.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.PrinterKM:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestPrinterKM());
                                            this.txtPrinterKM.Focus();
                                            this.txtPrinterKM.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.BiometricModuleSize:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestBiometricModuleSize());
                                            this.txtBiometricModuleSize.Focus();
                                            this.txtBiometricModuleSize.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.TotalBiometricUsers:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestTotalBiometricUsers());
                                            this.txtTotalBiometricUsers.Focus();
                                            this.txtTotalBiometricUsers.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.BiometricSecurity:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadBiometricSecurity());
                                            this.txtBiometricSecurity.Focus();
                                            this.txtBiometricSecurity.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.PaperStatus:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestPaperStatus());
                                            this.txtPaperStatus.Focus();
                                            this.txtPaperStatus.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.TotalBarCardUsers:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestTotalBarCardUsers());
                                            this.txtTotalBarCardUsers.Focus();
                                            this.txtTotalBarCardUsers.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.TotalProxCardUsers:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestTotalProxCardUsers());
                                            this.txtTotalProxCardUsers.Focus();
                                            this.txtTotalProxCardUsers.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.TotalKeyCodeUsers:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestTotalKeyCodeUsers());
                                            this.txtTotalKeyCodeUsers.Focus();
                                            this.txtTotalKeyCodeUsers.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.TotalPasswordUsers:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestTotalPasswordUsers());
                                            this.txtTotalPasswordUsers.Focus();
                                            this.txtTotalPasswordUsers.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.TotalAdminUsers:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestTotalAdminUsers());
                                            this.txtTotalAdminUsers.Focus();
                                            this.txtTotalAdminUsers.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.PunchTicketInfo:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadPunchTicketInfo());
                                            this.txtPunchTicketInfo.Focus();
                                            this.txtPunchTicketInfo.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.CurrentPaperRollSize:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestCurrentPaperRollSize());
                                            this.txtCurrentPaperRollSize.Focus();
                                            this.txtCurrentPaperRollSize.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.CurrentPaperRollKM:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestCurrentPaperRollKM());
                                            this.txtCurrentPaperRollKM.Focus();
                                            this.txtCurrentPaperRollKM.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.CurrentPaperRollTicktesPrinted:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestCurrentPaperRollTicketsPrinted());
                                            this.txtCurrentPaperRollTicketsPrinted.Focus();
                                            this.txtCurrentPaperRollTicketsPrinted.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.CurrentPaperRollEstimatedTickets:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestCurrentPaperRollEstimatedTickets());
                                            this.txtCurrentPaperRollEstimatedTickets.Focus();
                                            this.txtCurrentPaperRollEstimatedTickets.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ConnectionPoolTimeout:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadConnectionPoolTimeout());
                                            this.txtConnectionPoolTimeout.Focus();
                                            this.txtConnectionPoolTimeout.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_Cutter:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_Cutter());
                                            this.txtReadAlarm_Cutter.Focus();
                                            this.txtReadAlarm_Cutter.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_Buzzer:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_Buzzer());
                                            this.txtReadAlarm_Buzzer.Focus();
                                            this.txtReadAlarm_Buzzer.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_Temperature:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_Temperature());
                                            this.txtReadAlarm_Temperature.Focus();
                                            this.txtReadAlarm_Temperature.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_USBFiscal:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_USBFiscal());
                                            this.txtReadAlarm_USBFiscal.Focus();
                                            this.txtReadAlarm_USBFiscal.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_USBDados:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_USBDados());
                                            this.txtReadAlarm_USBDados.Focus();
                                            this.txtReadAlarm_USBDados.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_NoPaper:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_NoPaper());
                                            this.txtReadAlarm_NoPaper.Focus();
                                            this.txtReadAlarm_NoPaper.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_GateOpened:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_GateOpened());
                                            this.txtReadAlarm_GateOpened.Focus();
                                            this.txtReadAlarm_GateOpened.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_24H_Pressed:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_24H_Pressed());
                                            this.txtReadAlarm_24H_Pressed.Focus();
                                            this.txtReadAlarm_24H_Pressed.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_24H_Emitted:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_24H_Emitted());
                                            this.txtReadAlarm_24H_Emitted.Focus();
                                            this.txtReadAlarm_24H_Emitted.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_MRP_75:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_MRP_75());
                                            this.txtReadAlarm_MRP_75.Focus();
                                            this.txtReadAlarm_MRP_75.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_MRP_Full:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_MRP_Full());
                                            this.txtReadAlarm_MRP_Full.Focus();
                                            this.txtReadAlarm_MRP_Full.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_MT_75:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_MT_75());
                                            this.txtReadAlarm_MT_75.Focus();
                                            this.txtReadAlarm_MT_75.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_MT_Full:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_MT_Full());
                                            this.txtReadAlarm_MT_Full.Focus();
                                            this.txtReadAlarm_MT_Full.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_BatteryCriticalLevel:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_BatteryCriticalLevel());
                                            this.txtReadAlarm_BatteryCriticalLevel.Focus();
                                            this.txtReadAlarm_BatteryCriticalLevel.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_BlockViolation:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_BlockViolation());
                                            this.txtReadAlarm_BlockViolation.Focus();
                                            this.txtReadAlarm_BlockViolation.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_UnblockSuccess:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_UnblockSuccess());
                                            this.txtReadAlarm_UnblockSuccess.Focus();
                                            this.txtReadAlarm_UnblockSuccess.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_UnblockTried:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_UnblockTried());
                                            this.txtReadAlarm_UnblockTried.Focus();
                                            this.txtReadAlarm_UnblockTried.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_Cutter_75:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_MT_75());
                                            this.txtReadAlarm_Cutter_75.Focus();
                                            this.txtReadAlarm_Cutter_75.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_Cutter_Full:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_Cutter_Full());
                                            this.txtReadAlarm_Cutter_Full.Focus();
                                            this.txtReadAlarm_Cutter_Full.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_Printer_75:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_MT_75());
                                            this.txtReadAlarm_Printer_75.Focus();
                                            this.txtReadAlarm_Printer_75.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_Printer_Full:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_Printer_Full());
                                            this.txtReadAlarm_Printer_Full.Focus();
                                            this.txtReadAlarm_Printer_Full.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_MasterPasswordChanged:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_MasterPasswordChanged());
                                            this.txtReadAlarm_MasterPasswordChanged.Focus();
                                            this.txtReadAlarm_MasterPasswordChanged.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_AdminReboot:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_AdminReboot());
                                            this.txtReadAlarm_AdminReboot.Focus();
                                            this.txtReadAlarm_AdminReboot.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_CommunicationChanged:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_CommunicationChanged());
                                            this.txtReadAlarm_CommunicationChanged.Focus();
                                            this.txtReadAlarm_CommunicationChanged.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_WatchDogReboot:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_WatchDogReboot());
                                            this.txtReadAlarm_WatchDogReboot.Focus();
                                            this.txtReadAlarm_WatchDogReboot.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_BiometricSecurityChanged:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_BiometricSecurityChanged());
                                            this.txtReadAlarm_BiometricSecurityChanged.Focus();
                                            this.txtReadAlarm_BiometricSecurityChanged.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_PunchTicketChanged:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_PunchTicketInfoChanged());
                                            this.txtReadAlarm_PunchTicketChanged.Focus();
                                            this.txtReadAlarm_PunchTicketChanged.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_PaperRollChanged:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_PaperRollChanged());
                                            this.txtReadAlarm_PaperRollChanged.Focus();
                                            this.txtReadAlarm_PaperRollChanged.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ReadAlarm_PaperRoll_90:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ReadAlarmData_PaperRoll_90());
                                            this.txtReadAlarm_PaperRoll_90.Focus();
                                            this.txtReadAlarm_PaperRoll_90.SelectAll();
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_Cutter:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_Cutter());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_Buzzer:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_Buzzer());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_Temperature:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_Temperature());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_USBFiscal:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_USBFiscal());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_USBDados:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_USBDados());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_NoPaper:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_NoPaper());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_GateOpened:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_GateOpened());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_24H_Pressed:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_24H_Pressed());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_24H_Emitted:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_24H_Emitted());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_MRP_75:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_MRP_75());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_MRP_Full:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_MRP_Full());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_MT_75:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_MT_75());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_MT_Full:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_MT_Full());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_BatteryCriticalLevel:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_BatteryCriticalLevel());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_BlockViolation:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_BlockViolation());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_UnblockSuccess:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_UnblockSuccess());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_UnblockTried:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_UnblockTried());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_Cutter_75:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_MT_75());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_Cutter_Full:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_Cutter_Full());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_Printer_75:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_MT_75());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_Printer_Full:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_Printer_Full());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_MasterPasswordChanged:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_MasterPasswordChanged());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_AdminReboot:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_AdminReboot());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_CommunicationChanged:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_CommunicationChanged());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_WatchDogReboot:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_WatchDogReboot());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_BiometricSecurityChanged:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_BiometricSecurityChanged());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_PunchTicketChanged:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_PunchTicketInfoChanged());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_PaperRollChanged:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_PaperRollChanged());
                                            break;
                                        }
                                    case enumMonitor.ClearAlarm_PaperRoll_90:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.ClearAlarmData_PaperRoll_90());
                                            break;
                                        }
                                    case enumMonitor.RequestEventByNSR:
                                        {
                                            this.objCtrl_SocketClient.SendBuffer(this.objCtrl_IDSysR30.RequestEventByNSR((uint)this.iLastNSR + 1));
                                            break;
                                        }
                                }

                                this.enuProcessingStep = enumStep.WaitingResponse;
                                this.dtmTimeOut = DateTime.Now.AddSeconds(TIMEOUT);
 
                                break;
                            }
                            #endregion
                        case enumStep.ReceivedResponse:
                            #region ReceivedResponse
                            {
                                int iReturn = this.objCtrl_IDSysR30.PacketAvail(this.objCtrl_SocketClient.GetReceivedData());

                                if (iReturn < 0)
                                {
                                    this.MessageMonitor("Erro: " + this.queueMonitoring.First().ToString() + " - " + this.objCtrl_IDSysR30.VerifyReturnDLL(iReturn));
                                }
                                else
                                {
                                    int iValue = 0;
                                    byte byAlarmStatus = 0;
                                    DateTime dtmAlarmDateTime = new DateTime();

                                    switch (this.queueMonitoring.First())
                                    {
                                        case enumMonitor.Temperature:
                                            {
                                                this.objCtrl_IDSysR30.GetTemperature(ref iValue);
                                                this.txtTemperature.Text = iValue.ToString() + " °C";
                                                break;
                                            }
                                        case enumMonitor.BuzzerStatus:
                                            {
                                                this.objCtrl_IDSysR30.GetBuzzerStatus(ref iValue);
                                                this.txtBuzzerStatus.Text = iValue == 0 ? "Desabilitado" : "Habilitado";
                                                break;
                                            }
                                        case enumMonitor.CutterStatus:
                                            {
                                                this.objCtrl_IDSysR30.GetCutterStatus(ref iValue);
                                                this.txtCutterStatus.Text = iValue == 0 ? "Desabilitado" : "Habilitado";
                                                break;
                                            }
                                        case enumMonitor.TotalPrinterTickets:
                                            {
                                                this.objCtrl_IDSysR30.GetTotalPrinterTickets(ref iValue);
                                                this.txtTotalPrinterTickets.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.SystemVoltage:
                                            {
                                                this.objCtrl_IDSysR30.GetSystemVoltage(ref iValue);
                                                this.txtSystemVoltage.Text = iValue.ToString() + " mV";
                                                break;
                                            }
                                        case enumMonitor.TotalNSR:
                                            {
                                                this.objCtrl_IDSysR30.GetTotalNSR(ref iValue);
                                                this.txtTotalNSR.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.TotalUsers:
                                            {
                                                this.objCtrl_IDSysR30.GetTotalUsers(ref iValue);
                                                this.txtTotalUsers.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.TotalCutterCuts:
                                            {
                                                this.objCtrl_IDSysR30.GetTotalCutterCuts(ref iValue);
                                                this.txtTotalCutterCuts.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.PrinterKM:
                                            {
                                                this.objCtrl_IDSysR30.GetPrinterKM(ref iValue);
                                                this.txtPrinterKM.Text = iValue.ToString() + " cm";
                                                break;
                                            }
                                        case enumMonitor.BiometricModuleSize:
                                            {
                                                this.objCtrl_IDSysR30.GetBiometricModuleSize(ref iValue);
                                                this.txtBiometricModuleSize.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.TotalBiometricUsers:
                                            {
                                                this.objCtrl_IDSysR30.GetTotalBiometricUsers(ref iValue);
                                                this.txtTotalBiometricUsers.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.BiometricSecurity:
                                            {
                                                byte bySecurityLevel = 0;
                                                this.objCtrl_IDSysR30.GetBiometricSecurity(ref bySecurityLevel);
                                                this.txtBiometricSecurity.Text = bySecurityLevel.ToString();
                                                break;
                                            }
                                        case enumMonitor.PaperStatus:
                                            {
                                                this.objCtrl_IDSysR30.GetPaperStatus(ref iValue);
                                                this.txtPaperStatus.Text = iValue == 0 ? "Sem papel" : "Com papel";
                                                break;
                                            }
                                        case enumMonitor.TotalBarCardUsers:
                                            {
                                                this.objCtrl_IDSysR30.GetTotalBarCardUsers(ref iValue);
                                                this.txtTotalBarCardUsers.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.TotalProxCardUsers:
                                            {
                                                this.objCtrl_IDSysR30.GetTotalProxCardUsers(ref iValue);
                                                this.txtTotalProxCardUsers.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.TotalKeyCodeUsers:
                                            {
                                                this.objCtrl_IDSysR30.GetTotalKeyCodeUsers(ref iValue);
                                                this.txtTotalKeyCodeUsers.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.TotalPasswordUsers:
                                            {
                                                this.objCtrl_IDSysR30.GetTotalPasswordUsers(ref iValue);
                                                this.txtTotalPasswordUsers.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.TotalAdminUsers:
                                            {
                                                this.objCtrl_IDSysR30.GetTotalAdminUsers(ref iValue);
                                                this.txtTotalAdminUsers.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.PunchTicketInfo:
                                            {
                                                this.objCtrl_IDSysR30.GetTotalAdminUsers(ref iValue);
                                                this.txtPunchTicketInfo.Text = iValue == 0 ? "Desabilitado" : "Habilitado";
                                                break;
                                            }
                                        case enumMonitor.CurrentPaperRollSize:
                                            {
                                                this.objCtrl_IDSysR30.GetCurrentPaperRollSize(ref iValue);
                                                this.txtCurrentPaperRollSize.Text = iValue.ToString() + " m";
                                                break;
                                            }
                                        case enumMonitor.CurrentPaperRollKM:
                                            {
                                                this.objCtrl_IDSysR30.GetCurrentPaperRollKM(ref iValue);
                                                this.txtCurrentPaperRollKM.Text = iValue.ToString() + " cm";
                                                break;
                                            }
                                        case enumMonitor.CurrentPaperRollTicktesPrinted:
                                            {
                                                this.objCtrl_IDSysR30.GetCurrentPaperRollTicketsPrinted(ref iValue);
                                                this.txtCurrentPaperRollTicketsPrinted.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.CurrentPaperRollEstimatedTickets:
                                            {
                                                this.objCtrl_IDSysR30.GetCurrentPaperRollEstimatedTickets(ref iValue);
                                                this.txtCurrentPaperRollEstimatedTickets.Text = iValue.ToString();
                                                break;
                                            }
                                        case enumMonitor.ConnectionPoolTimeout:
                                            {
                                                this.objCtrl_IDSysR30.GetConnectionPoolTimeout(ref iValue);
                                                this.txtConnectionPoolTimeout.Text = iValue.ToString() + " s";
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_Cutter:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_Cutter(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_Cutter.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_Buzzer:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_Buzzer(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_Buzzer.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_Temperature:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_Temperature(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_Temperature.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_USBFiscal:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_USBFiscal(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_USBFiscal.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_USBDados:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_USBDados(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_USBDados.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_NoPaper:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_NoPaper(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_NoPaper.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_GateOpened:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_GateOpened(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_GateOpened.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_24H_Pressed:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_24H_Pressed(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_24H_Pressed.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_24H_Emitted:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_24H_Emitted(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_24H_Emitted.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_MRP_75:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_MRP_75(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_MRP_75.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_MRP_Full:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_MRP_Full(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_MRP_Full.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_MT_75:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_MT_75(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_MT_75.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_MT_Full:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_MT_Full(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_MT_Full.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_BatteryCriticalLevel:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_BatteryCriticalLevel(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_BatteryCriticalLevel.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_BlockViolation:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_BlockViolation(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_BlockViolation.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_UnblockSuccess:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_UnblockSuccess(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_UnblockSuccess.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_UnblockTried:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_UnblockTried(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_UnblockTried.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_Cutter_75:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_Cutter_75(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_Cutter_75.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_Cutter_Full:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_Cutter_Full(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_Cutter_Full.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_Printer_75:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_Printer_75(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_Printer_75.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_Printer_Full:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_Printer_Full(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_Printer_Full.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_MasterPasswordChanged:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_MasterPasswordChanged(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_MasterPasswordChanged.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_AdminReboot:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_AdminReboot(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_AdminReboot.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_CommunicationChanged:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_CommunicationChanged(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_CommunicationChanged.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_WatchDogReboot:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_WatchDogReboot(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_WatchDogReboot.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_BiometricSecurityChanged:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_BiometricSecurityChanged(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_BiometricSecurityChanged.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_PunchTicketChanged:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_PunchTicketInfoChanged(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_PunchTicketChanged.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_PaperRollChanged:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_PaperRollChanged(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_PaperRollChanged.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ReadAlarm_PaperRoll_90:
                                            {
                                                this.objCtrl_IDSysR30.GetAlarmData_PaperRoll_90(ref byAlarmStatus, ref dtmAlarmDateTime);
                                                this.txtReadAlarm_PaperRoll_90.Text = byAlarmStatus == 0 ? "Não acionado" : "Acionado - " + dtmAlarmDateTime.ToString("dd/MM/yyyy - HH:mm:ss");
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_Cutter:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_Buzzer:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_Temperature:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_USBFiscal:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_USBDados:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_NoPaper:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_GateOpened:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_24H_Pressed:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_24H_Emitted:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_MRP_75:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_MRP_Full:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_MT_75:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_MT_Full:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_BatteryCriticalLevel:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_BlockViolation:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_UnblockSuccess:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_UnblockTried:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_Cutter_75:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_Cutter_Full:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_Printer_75:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_Printer_Full:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_MasterPasswordChanged:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_AdminReboot:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_CommunicationChanged:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_WatchDogReboot:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_BiometricSecurityChanged:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_PunchTicketChanged:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_PaperRollChanged:
                                            {
                                                break;
                                            }
                                        case enumMonitor.ClearAlarm_PaperRoll_90:
                                            {
                                                break;
                                            }
                                        case enumMonitor.RequestEventByNSR:
                                            {
                                                string strEvent = "";

                                                switch (iReturn)
                                                {
                                                    case 2:
                                                        {
                                                            uint uiNSR = 0;
                                                            byte byRegType = 0;
                                                            byte byRegDateDay = 0;
                                                            byte byRegDateMonth = 0;
                                                            ushort usRegDateYear = 0;
                                                            byte byRegTimeHour = 0;
                                                            byte byRegTimeMin = 0;
                                                            byte byIdentifyType = 0;
                                                            string strCNPJ_CPF = "";
                                                            ulong ulCEI = 0;
                                                            string strEmployerName = "";
                                                            string strEmployerAddress = "";

                                                            this.objCtrl_IDSysR30.GetLogType2(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref byIdentifyType, ref strCNPJ_CPF, ref ulCEI, ref strEmployerName, ref strEmployerAddress);

                                                            strEvent += uiNSR.ToString("000000000");
                                                            strEvent += " " + byRegType.ToString();
                                                            strEvent += " " + byRegDateDay.ToString("00");
                                                            strEvent += " " + byRegDateMonth.ToString("00");
                                                            strEvent += " " + usRegDateYear.ToString("0000");
                                                            strEvent += " " + byRegTimeHour.ToString("00");
                                                            strEvent += " " + byRegTimeMin.ToString("00");
                                                            strEvent += " " + byIdentifyType.ToString();
                                                            strEvent += " " + strCNPJ_CPF;
                                                            strEvent += " " + ulCEI.ToString("000000000000");
                                                            strEvent += " " + strEmployerName;
                                                            strEvent += " " + strEmployerAddress;

                                                            break;
                                                        }
                                                    case 3:
                                                        {
                                                            uint uiNSR = 0;
                                                            byte byRegType = 0;
                                                            byte byRegDateDay = 0;
                                                            byte byRegDateMonth = 0;
                                                            ushort usRegDateYear = 0;
                                                            byte byRegTimeHour = 0;
                                                            byte byRegTimeMin = 0;
                                                            string strPIS = "";

                                                            this.objCtrl_IDSysR30.GetLogType3(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref strPIS);

                                                            strEvent += uiNSR.ToString("000000000");
                                                            strEvent += " " + byRegType.ToString();
                                                            strEvent += " " + byRegDateDay.ToString("00");
                                                            strEvent += " " + byRegDateMonth.ToString("00");
                                                            strEvent += " " + usRegDateYear.ToString("0000");
                                                            strEvent += " " + byRegTimeHour.ToString("00");
                                                            strEvent += " " + byRegTimeMin.ToString("00");
                                                            strEvent += " " + strPIS;

                                                            break;
                                                        }
                                                    case 4:
                                                        {
                                                            uint uiNSR = 0;
                                                            byte byRegType = 0;
                                                            byte byDayBeforeAdjust = 0;
                                                            byte byMonthBeforeAdjust = 0;
                                                            ushort usYearBeforeAdjust = 0;
                                                            byte byHourBeforeAdjust = 0;
                                                            byte byMinBeforeAdjust = 0;
                                                            byte byDayAfterAdjust = 0;
                                                            byte byMonthAfterAdjust = 0;
                                                            ushort usYearAfterAdjust = 0;
                                                            byte byHourAfterAdjust = 0;
                                                            byte byMinAfterAdjust = 0;

                                                            this.objCtrl_IDSysR30.GetLogType4(ref uiNSR, ref byRegType, ref byDayBeforeAdjust, ref byMonthBeforeAdjust, ref usYearBeforeAdjust, ref byHourBeforeAdjust, ref byMinBeforeAdjust, ref byDayAfterAdjust, ref byMonthAfterAdjust, ref usYearAfterAdjust, ref byHourAfterAdjust, ref byMinAfterAdjust);

                                                            strEvent += uiNSR.ToString("000000000");
                                                            strEvent += " " + byRegType.ToString();
                                                            strEvent += " " + byDayBeforeAdjust.ToString("00");
                                                            strEvent += " " + byMonthBeforeAdjust.ToString("00");
                                                            strEvent += " " + usYearBeforeAdjust.ToString("0000");
                                                            strEvent += " " + byHourBeforeAdjust.ToString("00");
                                                            strEvent += " " + byMinBeforeAdjust.ToString("00");
                                                            strEvent += " " + byDayAfterAdjust.ToString("00");
                                                            strEvent += " " + byMonthAfterAdjust.ToString("00");
                                                            strEvent += " " + usYearAfterAdjust.ToString("0000");
                                                            strEvent += " " + byHourAfterAdjust.ToString("00");
                                                            strEvent += " " + byMinAfterAdjust.ToString("00");

                                                            break;
                                                        }
                                                    case 5:
                                                        {
                                                            uint uiNSR = 0;
                                                            byte byRegType = 0;
                                                            byte byRegDateDay = 0;
                                                            byte byRegDateMonth = 0;
                                                            ushort usRegDateYear = 0;
                                                            byte byRegTimeHour = 0;
                                                            byte byRegTimeMin = 0;
                                                            byte byOpType = 0;
                                                            string strPIS = "";
                                                            string strUserName = "";

                                                            this.objCtrl_IDSysR30.GetLogType5(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref byOpType, ref strPIS, ref strUserName);

                                                            strEvent += uiNSR.ToString("000000000");
                                                            strEvent += " " + byRegType.ToString();
                                                            strEvent += " " + byRegDateDay.ToString("00");
                                                            strEvent += " " + byRegDateMonth.ToString("00");
                                                            strEvent += " " + usRegDateYear.ToString("0000");
                                                            strEvent += " " + byRegTimeHour.ToString("00");
                                                            strEvent += " " + byRegTimeMin.ToString("00");
                                                            strEvent += " " + (char)byOpType;
                                                            strEvent += " " + strPIS;
                                                            strEvent += " " + strUserName;

                                                            break;
                                                        }
                                                }

                                                this.iLastNSR++;

                                                this.lstEvents.Items.Add(strEvent);
                                                this.lstEvents.SelectedIndex = this.lstEvents.Items.Count - 1;
                                                
                                                break;
                                            }
                                    }
                                }

                                this.queueMonitoring.Dequeue();

                                this.enuProcessingStep = enumStep.Read;

                                break;
                            }
                            #endregion
                        case enumStep.WaitingConnection:
                        case enumStep.WaitingResponse:
                            #region Waiting
                            {
                                //if (DateTime.Now >= this.dtmTimeOut)
                                //{
                                //    if (this.enuProcessingStep == enumStep.WaitingResponse)
                                //    {
                                //        //this.objCtrl_SocketClient.ShutDown();
                                //        this.queueMonitoring.Dequeue();
                                //        this.enuProcessingStep = enumStep.Monitoring;
                                //        break;
                                //    }
                                //}

                                this.connCurrent = this.objCtrl_SocketClient.GetConnectionState();

                                switch (this.objCtrl_SocketClient.GetConnectionState())
                                {
                                    case enumConnectionState.None:
                                        {
                                        }
                                        break;
                                    case enumConnectionState.Disconnected:
                                        {
                                            if (this.connCurrent == this.connLast)
                                            {
                                                break;
                                            }

                                            this.lblCommunicationMonitor.Text = ":: Desconectado ::";

                                            this.btnConnect.Enabled = true;
                                            this.btnDisconnect.Enabled = false;
                                                                                        
                                            this.iDisconnectedCount++;
                                            this.MessageMonitor(this.iDisconnectedCount.ToString() + " - Desconectado em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                                            if (this.chkReconnect.Checked)
                                            {
                                                this.enuProcessingStep = enumStep.Connect;
                                                this.MessageMonitor("Reconectando");
                                            }
                                            else
                                            {
                                                this.tmrMonitor.Enabled = false;
                                            }
                                        }
                                        break;
                                    case enumConnectionState.ConnectionAttempt:
                                        {
                                            this.lblCommunicationMonitor.Text = ":: Conectando ::";
                                        }
                                        break;
                                    case enumConnectionState.ConnectionAttemptFailed:
                                        {
                                            this.lblCommunicationMonitor.Text = ":: Falha ao conectar ::";

                                            this.btnConnect.Enabled = true;
                                            this.btnDisconnect.Enabled = false;

                                        }
                                        break;
                                    case enumConnectionState.ConnectionError:
                                        {
                                            this.lblCommunicationMonitor.Text = ":: Erro de conexão ::";
                                        }
                                        break;
                                    case enumConnectionState.Connected:
                                        {
                                            this.lblCommunicationMonitor.Text = ":: Conectado ::";
                                            this.MessageMonitor(this.iDisconnectedCount.ToString() + " - Conectado em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                                            this.btnConnect.Enabled = false;
                                            this.btnDisconnect.Enabled = true;

                                            this.enuProcessingStep = enumStep.Read;
                                        }
                                        break;
                                    case enumConnectionState.ReceivedData:
                                        {
                                            if (this.enuProcessingStep == enumStep.WaitingResponse)
                                            {
                                                this.enuProcessingStep = enumStep.ReceivedResponse;
                                            }
                                        }
                                        break;
                                }

                                this.connLast = this.connCurrent;

                                break;
                            }
                            #endregion
                    }
                }
                catch (Exception exError)
                {
                    this.MessageMonitor(exError.Message);
                }
            }

        #endregion

        #region methods

            private void MessageMonitor(string strMessage)
            {
                this.lstMonitoring.Items.Add(this.strIP + " > " + strMessage);
            }

        #endregion
    }
}
