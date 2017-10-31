using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using iddata.interfaceIDSysR30.business;

namespace iddata.interfaceIDSysR30.controller
{
    public class CCtrl_IDSysR30
    {
        #region Attributes

            private CIDSysR30 objIDSysR30;

        #endregion

        #region Constructor

            public CCtrl_IDSysR30()
            {
                this.objIDSysR30 = CIDSysR30.GetInstance();
            }

        #endregion

        #region Methods

            public bool UnloadDLL()
            {
                return this.objIDSysR30.UnloadDLL();
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a inclusão de um novo usuário no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] AddUser(string strPIS, string strUserName, uint uiKeyCode, string strBarCode, byte byFacilityCode, ulong ulProxCode, byte byUserType, int iAccessType, string strPassword, System.IO.MemoryStream msPhoto, ushort usSizeSample, byte byQuantitySamples, byte[] rgbyBiometrics)
            {
                return this.objIDSysR30.AddUser(strPIS, strUserName, uiKeyCode, strBarCode, byFacilityCode, ulProxCode, byUserType, iAccessType, strPassword, msPhoto, usSizeSample, byQuantitySamples, rgbyBiometrics);
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a alteração de um usuário no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ChangeUserData(string strPIS, string strNewPIS, string strUserName, uint uiKeyCode, string strBarCode, byte byFacilityCode, ulong ulProxCode, byte byUserType, int iAccessType, string strPassword, System.IO.MemoryStream msPhoto, ushort usSizeSample, byte byQuantitySamples, byte[] rgbyBiometrics)
            {
                return this.objIDSysR30.ChangeUserData(strPIS, strNewPIS, strUserName, uiKeyCode, strBarCode, byFacilityCode, ulProxCode, byUserType, iAccessType, strPassword, msPhoto, usSizeSample, byQuantitySamples, rgbyBiometrics);
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a exclusão de um usuário no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] DeleteUser(string strPIS)
            {
                return this.objIDSysR30.DeleteUser(strPIS);
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a leitura dos dados de um usuário no equipamento através do PIS
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadUserData(string strPIS)
            {
                return this.objIDSysR30.ReadUserData(strPIS);
            }

            /// <summary>
            /// Monta o buffer com o comando que configura os dados da empresa/empregador no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetEmployer(byte byIdentifyType, string strCNPJ_CPF, ulong ulCEI, string strEmployerName, string strEmployerAddress)
            {
                return this.objIDSysR30.SetEmployer(byIdentifyType, strCNPJ_CPF, ulCEI, strEmployerName, strEmployerAddress);
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a leitura dos dados da empresa/empregador do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadEmployerData()
            {
                return this.objIDSysR30.ReadEmployerData();
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita alteração na data e na hora do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetDateTime(DateTime dtmDateTime)
            {
                return this.objIDSysR30.SetDateTime(dtmDateTime);
            }

            /// <summary>
            /// Monta o buffer com o comando que configura os dados de comunicação do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetREPCommunication(byte byCommunicationType, string strIPEquipment, string strSubnetMask, string strIPGateway, ushort usTCPPort_Comm, ushort usTCPPort_Alarm, byte byBaudrate, byte bySerialAddress, byte byMulticastAddress, byte byBroadcastAddress)
            {
                return this.objIDSysR30.SetREPCommunication(byCommunicationType, strIPEquipment, strSubnetMask, strIPGateway, usTCPPort_Comm, usTCPPort_Alarm, byBaudrate, bySerialAddress, byMulticastAddress, byBroadcastAddress);
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a leitura dos dados de comunicação do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadREPCommunication()
            {
                return this.objIDSysR30.ReadREPCommunication();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o um evento gravado no equipamento através do NSR
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestEventByNSR(uint uiNSR)
            {
                return this.objIDSysR30.RequestEventByNSR(uiNSR);
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o NFR do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestNFR()
            {
                return this.objIDSysR30.RequestNFR();
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita a leitura dos dados de um usuário no equipamento através de um índice
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestUserByIndex(uint uiIndex)
            {
                return this.objIDSysR30.RequestUserByIndex(uiIndex);
            }

            /// <summary>
            /// Monta o buffer com o comando que envia o nível de segurança do módulo biometrico para o equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetBiometricSecurity(byte bySecurityLevel)
            {
                return this.objIDSysR30.SetBiometricSecurity(bySecurityLevel);
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita o nível de segurança do módulo biometrico do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadBiometricSecurity()
            {
                return this.objIDSysR30.ReadBiometricSecurity();
            }
            
            /// <summary>
            /// Monta o buffer com o comando que altera o status do alerta durante impressão do ticket
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetPunchTicketInfo(byte byPunchTicketInfo)
            {
                return this.objIDSysR30.SetPunchTicketInfo(byPunchTicketInfo);
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita status do alerta durante impressão do ticket
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadPunchTicketInfo()
            {
                return this.objIDSysR30.ReadPunchTicketInfo();
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita 
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] SetConnectionPoolTimeout(int iPoolTimeout)
            {
                return this.objIDSysR30.SetConnectionPoolTimeout(iPoolTimeout);
            }

            /// <summary>
            /// Monta o buffer com o comando que solicita 
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadConnectionPoolTimeout()
            {
                return this.objIDSysR30.ReadConnectionPoolTimeout();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita a temperatura do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTemperature()
            {
                return this.objIDSysR30.RequestTemperature();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o estado atual do buzzer do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestBuzzerStatus()
            {
                return this.objIDSysR30.RequestBuzzerStatus();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o estado atual do cutter do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestCutterStatus()
            {
                return this.objIDSysR30.RequestCutterStatus();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de tickets emitidos pelo equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalPrinterTickets()
            {
                return this.objIDSysR30.RequestTotalPrinterTickets();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o valor atual da tensão do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestSystemVoltage()
            {
                return this.objIDSysR30.RequestSystemVoltage();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de eventos (NSR) gravadas no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalNSR()
            {
                return this.objIDSysR30.RequestTotalNSR();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários gravadas no equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalUsers()
            {
                return this.objIDSysR30.RequestTotalUsers();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de vezes que o cutter foi acionado pelo equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalCutterCuts()
            {
                return this.objIDSysR30.RequestTotalCutterCuts();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total da quilometragem da impressora do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestPrinterKM()
            {
                return this.objIDSysR30.RequestPrinterKM();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o tamanho do módulo biométrico do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestBiometricModuleSize()
            {
                return this.objIDSysR30.RequestBiometricModuleSize();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários com biometria do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalBiometricUsers()
            {
                return this.objIDSysR30.RequestTotalBiometricUsers();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o status do papel do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestPaperStatus()
            {
                return this.objIDSysR30.RequestPaperStatus();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários com cartão de código de barras do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalBarCardUsers()
            {
                return this.objIDSysR30.RequestTotalBarCardUsers();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários com cartão de código de barras do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalProxCardUsers()
            {
                return this.objIDSysR30.RequestTotalProxCardUsers();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários com código de acesso do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalKeyCodeUsers()
            {
                return this.objIDSysR30.RequestTotalKeyCodeUsers();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários com senha de acesso do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalPasswordUsers()
            {
                return this.objIDSysR30.RequestTotalPasswordUsers();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o total de usuários administradores do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestTotalAdminUsers()
            {
                return this.objIDSysR30.RequestTotalAdminUsers();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita o tamanho da bobina de papel atual do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestCurrentPaperRollSize()
            {
                return this.objIDSysR30.RequestCurrentPaperRollSize();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita a quilometragem da bobina de papel atual do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestCurrentPaperRollKM()
            {
                return this.objIDSysR30.RequestCurrentPaperRollKM();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita a quantidade de tickets impressos da bobina de papel atual do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestCurrentPaperRollTicketsPrinted()
            {
                return this.objIDSysR30.RequestCurrentPaperRollTicketsPrinted();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita a estimativa de tickets da bobina de papel atual do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestCurrentPaperRollEstimatedTickets()
            {
                return this.objIDSysR30.RequestCurrentPaperRollEstimatedTickets();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os status dos alarmes do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] RequestAlarmsStatus()
            {
                return this.objIDSysR30.RequestAlarmsStatus();
            }
        
            /// <summary>
            /// Monta buffer com o comando que solicita os dados de um alarme do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData(byte byAlarm)
            {
                return this.objIDSysR30.ReadAlarmData(byAlarm);
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme Cutter
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Cutter()
            {
                return this.objIDSysR30.ReadAlarmData_Cutter();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme Buzzer
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Buzzer()
            {
                return this.objIDSysR30.ReadAlarmData_Buzzer();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme Temperatura
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Temperature()
            {
                return this.objIDSysR30.ReadAlarmData_Temperature();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme da USB Fiscal do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_USBFiscal()
            {
                return this.objIDSysR30.ReadAlarmData_USBFiscal();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme da USB Dados do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_USBDados()
            {
                return this.objIDSysR30.ReadAlarmData_USBDados();
            }
        
            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme Impressora sem Papel
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_NoPaper()
            {
                return this.objIDSysR30.ReadAlarmData_NoPaper();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados de PORTA DA IMPRESSORA ABERTA do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_GateOpened()
            {
                return this.objIDSysR30.ReadAlarmData_GateOpened();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de Relatório de 24 horas pressionado do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_24H_Pressed()
            {
                return this.objIDSysR30.ReadAlarmData_24H_Pressed();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de Relatório de 24 horas emitido do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_24H_Emitted()
            {
                return this.objIDSysR30.ReadAlarmData_24H_Emitted();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de MRP atingiu 75% da capacidade do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_MRP_75()
            {
                return this.objIDSysR30.ReadAlarmData_MRP_75();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de MRP cheia do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_MRP_Full()
            {
                return this.objIDSysR30.ReadAlarmData_MRP_Full();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme MT atingiu 75% da capacidade do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_MT_75()
            {
                return this.objIDSysR30.ReadAlarmData_MT_75();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de MT cheia do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_MT_Full()
            {
                return this.objIDSysR30.ReadAlarmData_MT_Full();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de Bateria em nível crítico do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_BatteryCriticalLevel()
            {
                return this.objIDSysR30.ReadAlarmData_BatteryCriticalLevel();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme equipamento bloqueado por violação
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_BlockViolation()
            {
                return this.objIDSysR30.ReadAlarmData_BlockViolation();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de equipamento desbloqueado com sucesso
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_UnblockSuccess()
            {
                return this.objIDSysR30.ReadAlarmData_UnblockSuccess();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de tentativa de desbloqueio do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_UnblockTried()
            {
                return this.objIDSysR30.ReadAlarmData_UnblockTried();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de cutter atingiu 75% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Cutter_75()
            {
                return this.objIDSysR30.ReadAlarmData_Cutter_75();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados de cutter atingiu 100% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Cutter_Full()
            {
                return this.objIDSysR30.ReadAlarmData_Cutter_Full();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de impressora atingiu 75% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Printer_75()
            {
                return this.objIDSysR30.ReadAlarmData_Printer_75();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados de impressora atingiu 100% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_Printer_Full()
            {
                return this.objIDSysR30.ReadAlarmData_Printer_Full();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de senha mestre alterada
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_MasterPasswordChanged()
            {
                return this.objIDSysR30.ReadAlarmData_MasterPasswordChanged();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de equipamento reiniciado por administrador
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_AdminReboot()
            {
                return this.objIDSysR30.ReadAlarmData_AdminReboot(); ;
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de dados de comunicação alterados
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_CommunicationChanged()
            {
                return this.objIDSysR30.ReadAlarmData_CommunicationChanged();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de equipamento reiniciado por watchdog
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_WatchDogReboot()
            {
                return this.objIDSysR30.ReadAlarmData_WatchDogReboot();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de nível de segurança do equipamento alterado
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_BiometricSecurityChanged()
            {
                return this.objIDSysR30.ReadAlarmData_BiometricSecurityChanged();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de alteração do estado do alerta de impressão de ticket do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_PunchTicketInfoChanged()
            {
                return this.objIDSysR30.ReadAlarmData_PunchTicketInfoChanged();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de alteração da bobina de papel do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_PaperRollChanged()
            {
                return this.objIDSysR30.ReadAlarmData_PaperRollChanged();
            }

            /// <summary>
            /// Monta buffer com o comando que solicita os dados do alarme de 90% da bobina de papel do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ReadAlarmData_PaperRoll_90()
            {
                return this.objIDSysR30.ReadAlarmData_PaperRoll_90();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados de um alarme do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData(byte byAlarm)
            {
                return this.objIDSysR30.ReadAlarmData(byAlarm);
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de cutter
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Cutter()
            {
                return this.objIDSysR30.ClearAlarmData_Cutter();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de buzzer
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Buzzer()
            {
                return this.objIDSysR30.ClearAlarmData_Buzzer();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de Temperatura do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Temperature()
            {
                return this.objIDSysR30.ClearAlarmData_Temperature();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme da USB Fiscal do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_USBFiscal()
            {
                return this.objIDSysR30.ClearAlarmData_USBFiscal();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme da USB Dados do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_USBDados()
            {
                return this.objIDSysR30.ClearAlarmData_USBDados();
            }
        
            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme Impressora sem Papel
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_NoPaper()
            {
                return this.objIDSysR30.ClearAlarmData_NoPaper();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados de PORTA DA IMPRESSORA ABERTA do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_GateOpened()
            {
                return this.objIDSysR30.ClearAlarmData_GateOpened(); ;
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de Relatório de 24 horas pressionado do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_24H_Pressed()
            {
                return this.objIDSysR30.ClearAlarmData_24H_Pressed();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de Relatório de 24 horas emitido do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_24H_Emitted()
            {
                return this.objIDSysR30.ClearAlarmData_24H_Emitted();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de MRP atingiu 75% da capacidade do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_MRP_75()
            {
                return this.objIDSysR30.ClearAlarmData_MRP_75();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de MRP cheia do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_MRP_Full()
            {
                return this.objIDSysR30.ClearAlarmData_MRP_Full();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme MT atingiu 75% da capacidade do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_MT_75()
            {
                return this.objIDSysR30.ClearAlarmData_MT_75();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de MT cheia do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_MT_Full()
            {
                return this.objIDSysR30.ClearAlarmData_MT_Full();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de Bateria em nível crítico do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_BatteryCriticalLevel()
            {
                return this.objIDSysR30.ClearAlarmData_BatteryCriticalLevel();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme equipamento bloqueado por violação
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_BlockViolation()
            {
                return this.objIDSysR30.ClearAlarmData_BlockViolation();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de equipamento desbloqueado com sucesso
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_UnblockSuccess()
            {
                return this.objIDSysR30.ClearAlarmData_UnblockSuccess();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de tentativa de desbloqueio do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_UnblockTried()
            {
                return this.objIDSysR30.ClearAlarmData_UnblockTried();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de cutter atingiu 75% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Cutter_75()
            {
                return this.objIDSysR30.ClearAlarmData_Cutter_75();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados de cutter atingiu 100% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Cutter_Full()
            {
                return this.objIDSysR30.ClearAlarmData_Cutter_Full();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de impressora atingiu 75% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Printer_75()
            {
                return this.objIDSysR30.ClearAlarmData_Printer_75();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados de impressora atingiu 100% da vida útil
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_Printer_Full()
            {
                return this.objIDSysR30.ClearAlarmData_Printer_Full();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de senha mestre alterada
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_MasterPasswordChanged()
            {
                return this.objIDSysR30.ClearAlarmData_MasterPasswordChanged();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de equipamento reiniciado por administrador
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_AdminReboot()
            {
                return this.objIDSysR30.ClearAlarmData_AdminReboot();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de dados de comunicação alterados
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_CommunicationChanged()
            {
                return this.objIDSysR30.ClearAlarmData_CommunicationChanged();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de equipamento reiniciado por watchdog
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_WatchDogReboot()
            {
                return this.objIDSysR30.ClearAlarmData_WatchDogReboot();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de nível de segurança do equipamento alterado
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_BiometricSecurityChanged()
            {
                return this.objIDSysR30.ClearAlarmData_BiometricSecurityChanged();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de alteração do estado do alerta de impressão de ticket do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_PunchTicketInfoChanged()
            {
                return this.objIDSysR30.ClearAlarmData_PunchTicketInfoChanged();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de alteração da bobina de papel do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_PaperRollChanged()
            {
                return this.objIDSysR30.ClearAlarmData_PaperRollChanged();
            }

            /// <summary>
            /// Monta buffer com o comando que limpa os dados do alarme de 90% da bobina de papel do equipamento
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public byte[] ClearAlarmData_PaperRoll_90()
            {
                return this.objIDSysR30.ClearAlarmData_PaperRoll_90();
            }

            /// <summary>
            /// Solicita a avaliação do buffer devolvido pelo equipamento
            /// </summary>
            /// <returns>Ver tabela de retorno</returns>
            public int PacketAvail(byte[] rgbyBuffer)
            {
                return this.objIDSysR30.PacketAvail(rgbyBuffer);
            }

            public string VerifyReturnDLL(int iReturn)
            {
                return this.objIDSysR30.VerifyReturnDLL(iReturn);
            }

            /// <summary>
            /// Obtem os dados do usuário solicitado no comando ReadUserData
            /// </summary>
            /// <returns></returns>
            public void GetUserData(ref string strPIS, ref string strUserName, ref uint uiKeyCode, ref string strBarCode, ref byte byFacilityCode, ref ulong ulProxCode, ref byte byUserType, ref int iAccessType, ref string strPassword, ref ushort usSizePhoto, ref System.IO.MemoryStream msPhoto, ref ushort usSizeSample, ref byte byQuantitySamples, ref byte[] rgbyBiometric_Sample1, ref byte[] rgbyBiometric_Sample2)
            {
                this.objIDSysR30.GetUserData(ref strPIS, ref strUserName, ref uiKeyCode, ref strBarCode, ref byFacilityCode, ref ulProxCode, ref byUserType, ref iAccessType, ref strPassword, ref usSizePhoto, ref msPhoto, ref usSizeSample, ref byQuantitySamples, ref rgbyBiometric_Sample1, ref rgbyBiometric_Sample2);
            }

            /// <summary>
            /// Obtem os dados da empresa/empregador solicitado no comando ReadEmployerData
            /// </summary>
            /// <returns></returns>
            public void GetEmployerData(ref byte byIdentifyType, ref string strCNPJ_CPF, ref ulong ulCEI, ref string strEmployerName, ref string strEmployerAddress)
            {
                this.objIDSysR30.GetEmployerData(ref byIdentifyType, ref strCNPJ_CPF, ref ulCEI, ref strEmployerName, ref strEmployerAddress);
            }

            /// <summary>
            /// Obtem os dados do registro tipo 2 solicitado no comando RequestEventByNSR
            /// </summary>
            /// <returns></returns>
            public void GetLogType2(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref byte byIdentifyType, ref string strCNPJ_CPF, ref ulong ulCEI, ref string strEmployerName, ref string strEmployerAddress)
            {
                this.objIDSysR30.GetLogType2(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref byIdentifyType, ref strCNPJ_CPF, ref ulCEI, ref strEmployerName, ref strEmployerAddress);
            }

            /// <summary>
            /// Obtem os dados do registro tipo 3 solicitado no comando RequestEventByNSR
            /// </summary>
            /// <returns></returns>
            public void GetLogType3(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref string strPIS)
            {
                this.objIDSysR30.GetLogType3(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref strPIS);
            }

            /// <summary>
            /// Obtem os dados do registro tipo 4 solicitado no comando RequestEventByNSR
            /// </summary>
            /// <returns></returns>
            public void GetLogType4(ref uint uiNSR, ref byte byRegType, ref byte byDayBeforeAdjust, ref byte byMonthBeforeAdjust, ref ushort usYearBeforeAdjust, ref byte byHourBeforeAdjust, ref byte byMinuteBeforeAdjust, ref byte byDayAfterAdjust, ref byte byMonthAfterAdjust, ref ushort usYearAfterAdjust, ref byte byHourAfterAdjust, ref byte byMinuteAfterAdjust)
            {
                this.objIDSysR30.GetLogType4(ref uiNSR, ref byRegType, ref byDayBeforeAdjust, ref byMonthBeforeAdjust, ref usYearBeforeAdjust, ref byHourBeforeAdjust, ref byMinuteBeforeAdjust, ref byDayAfterAdjust, ref byMonthAfterAdjust, ref usYearAfterAdjust, ref byHourAfterAdjust, ref byMinuteAfterAdjust);
            }

            /// <summary>
            /// Obtem os dados do registro tipo 5 solicitado no comando RequestEventByNSR
            /// </summary>
            /// <returns></returns>
            public void GetLogType5(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref byte byOpType, ref string strPIS, ref string strUserName)
            {
                this.objIDSysR30.GetLogType5(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref byOpType, ref strPIS, ref strUserName);
            }

            /// <summary>
            /// Obtem os dados do registro tipo 3 solicitado no comando RequestEventByNSR
            /// </summary>
            /// <returns></returns>
            public void GetLogType6(ref uint uiNSR, ref byte byRegType, ref byte byRegDateDay, ref byte byRegDateMonth, ref ushort usRegDateYear, ref byte byRegTimeHour, ref byte byRegTimeMin, ref string strPIS, ref char cEvent)
            {
                this.objIDSysR30.GetLogType6(ref uiNSR, ref byRegType, ref byRegDateDay, ref byRegDateMonth, ref usRegDateYear, ref byRegTimeHour, ref byRegTimeMin, ref strPIS, ref cEvent);
            }

            /// <summary>
            /// Obtem os dados da comunicação do equipamento solicitado no comando ReadREPCommunication
            /// </summary>
            /// <returns></returns>
            public void GetREPCommunication(ref byte byCommunicationType, ref string strIPEquipment, ref string strSubnetMask, ref string strIPGateway, ref ushort usTCPPort_Comm, ref ushort usTCPPort_Alarm, ref byte byBaudrate, ref byte bySerialAddress, ref byte byMulticastAddress, ref byte byBroadcastAddress)
            {
                this.objIDSysR30.GetREPCommunication(ref byCommunicationType, ref strIPEquipment, ref strSubnetMask, ref strIPGateway, ref usTCPPort_Comm, ref usTCPPort_Alarm, ref byBaudrate, ref bySerialAddress, ref byMulticastAddress, ref byBroadcastAddress);
            }

            /// <summary>
            /// Obtem os dados do NFR do equipamento solicitado no comando RequestNFR
            /// </summary>
            /// <returns></returns>
            public void GetNFR(ref string strNFR)
            {
                this.objIDSysR30.GetNFR(ref strNFR);
            }

            /// <summary>
            /// Obtem o nível de segurança do módulo biométrico do equipamento solicitado no comando ReadBiometricSecurity
            /// </summary>
            /// <returns></returns>
            public void GetBiometricSecurity(ref byte bySecurityLevel)
            {
                this.objIDSysR30.GetBiometricSecurity(ref bySecurityLevel);
            }

            /// <summary>
            /// Obtem o horário de verão do equipamento solicitado no comando ReadDST
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public void GetDST(ref DateTime dtmDST_Start, ref DateTime dtmDST_End)
            {
                this.objIDSysR30.GetDST(ref dtmDST_Start, ref dtmDST_End);
            }

            /// <summary>
            /// Obtem o  do equipamento solicitado no comando ReadPunchTicketInfo
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public void GetPunchTicketInfo(ref int iPunchTicketInfo)
            {
                this.objIDSysR30.GetPunchTicketInfo(ref iPunchTicketInfo);
            }

            /// <summary>
            /// Obtem o timeout de pool da conexão do equipamento solicitado no comando ReadPunchTicketInfo
            /// </summary>
            /// <returns>Buffer a ser enviado para o equipamento</returns>
            public void GetConnectionPoolTimeout(ref int iPoolTimeout)
            {
                this.objIDSysR30.GetConnectionPoolTimeout(ref iPoolTimeout);
            }

            /// <summary>
            /// Obtem os dados a temperatura do equipamento solicitado no comando RequestTemperature
            /// </summary>
            /// <returns></returns>
            public void GetTemperature(ref int iTemperature)
            {
                this.objIDSysR30.GetTemperature(ref iTemperature);
            }

            /// <summary>
            /// Obtem o estado do Buzzer requisitado na função RequestBuzzerStatus
            /// </summary>
            /// <returns></returns>
            public void GetBuzzerStatus(ref int iBuzzerStatus)
            {
                this.objIDSysR30.GetBuzzerStatus(ref iBuzzerStatus);
            }

            /// <summary>
            /// Obtem o estado do Cutter requisitado na função RequestCutterStatus
            /// </summary>
            /// <returns></returns>
            public void GetCutterStatus(ref int iCutterStatus)
            {
                this.objIDSysR30.GetCutterStatus(ref iCutterStatus);
            }

            /// <summary>
            /// Obtem o total de tickets impressos pelo equipamento solicitado na função RequestTotalPrinterTickets
            /// </summary>
            /// <returns></returns>
            public void GetTotalPrinterTickets(ref int iTotalPrinterTickets)
            {
                this.objIDSysR30.GetTotalPrinterTickets(ref iTotalPrinterTickets);
            }

            /// <summary>
            /// Obtem a tensão atual do equipamento solicitada na função RequestSystemVoltage
            /// </summary>
            /// <returns></returns>
            public void GetSystemVoltage(ref int iSystemVoltage)
            {
                this.objIDSysR30.GetSystemVoltage(ref iSystemVoltage);
            }

            /// <summary>
            /// Obtem o total de eventos gravados no equipamento solicitado na função RequestTotalNSR
            /// </summary>
            /// <returns></returns>
            public void GetTotalNSR(ref uint uiTotalNSR)
            {
                this.objIDSysR30.GetTotalNSR(ref uiTotalNSR);
            }

            /// <summary>
            /// Obtem o total de usuários gravados no equipamento solicitado na função RequestTotalUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalUsers(ref int iTotalUsers)
            {
                this.objIDSysR30.GetTotalUsers(ref iTotalUsers);
            }

            /// <summary>
            /// Obtem o total de acionamentos realizados pelo Cutter do equipamento solicitado na função RequestTotalCutterCuts
            /// </summary>
            /// <returns></returns>
            public void GetTotalCutterCuts(ref int iTotalCutterCuts)
            {
                this.objIDSysR30.GetTotalCutterCuts(ref iTotalCutterCuts);
            }

            /// <summary>
            /// Obtem a quilometragem da impressora do equipamento solicitado na função RequestPrinterKM
            /// </summary>
            /// <returns></returns>
            public void GetPrinterKM(ref int iPrinterKM)
            {
                this.objIDSysR30.GetPrinterKM(ref iPrinterKM);
            }

            /// <summary>
            /// Obtem o tamanho do módulo biometrico (quantidade máxima de usuários) do equipamento solicitado na função RequestBiometricModuleSize
            /// </summary>
            /// <returns></returns>
            public void GetBiometricModuleSize(ref int iBiometricModuleSize)
            {
                this.objIDSysR30.GetBiometricModuleSize(ref iBiometricModuleSize);
            }

            /// <summary>
            /// Obtem a quantidade de usuários cadastrados no módulo biometrico do equipamento solicitado na função RequestTotalBiometricUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalBiometricUsers(ref int iTotalBiometricUsers)
            {
                this.objIDSysR30.GetTotalBiometricUsers(ref iTotalBiometricUsers);
            }

            /// <summary>
            /// Obtem a status do papel do equipamento solicitado na função RequestPaperStatus
            /// </summary>
            /// <returns></returns>
            public void GetPaperStatus(ref int iPaperStatus)
            {
                this.objIDSysR30.GetPaperStatus(ref iPaperStatus);
            }

            /// <summary>
            /// Obtem a quantidade de usuários cadastrados com cartão de código de baras no equipamento solicitado na função RequestTotalBarCardUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalBarCardUsers(ref int iTotalBarCardUsers)
            {
                this.objIDSysR30.GetTotalBarCardUsers(ref iTotalBarCardUsers);
            }

            /// <summary>
            /// Obtem a quantidade de usuários cadastrados com cartão de proximidade no equipamento solicitado na função RequestTotalProxCardUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalProxCardUsers(ref int iTotalProxCardUsers)
            {
                this.objIDSysR30.GetTotalProxCardUsers(ref iTotalProxCardUsers);
            }

            /// <summary>
            /// Obtem a quantidade de usuários cadastrados com código de acesso no equipamento solicitado na função RequestTotalKeyCodeUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalKeyCodeUsers(ref int iTotalKeyCodeUsers)
            {
                this.objIDSysR30.GetTotalKeyCodeUsers(ref iTotalKeyCodeUsers);
            }

            /// <summary>
            /// Obtem a quantidade de usuários cadastrados com senha no equipamento solicitado na função RequestTotalPasswordUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalPasswordUsers(ref int iTotalPasswordUsers)
            {
                this.objIDSysR30.GetTotalPasswordUsers(ref iTotalPasswordUsers);
            }

            /// <summary>
            /// Obtem a quantidade de usuários cadastrados como administradores no equipamento solicitado na função RequestTotalAdminUsers
            /// </summary>
            /// <returns></returns>
            public void GetTotalAdminUsers(ref int iTotalAdminUsers)
            {
                this.objIDSysR30.GetTotalAdminUsers(ref iTotalAdminUsers);
            }

            /// <summary>
            /// Obtem o tamanho da bobina de papel atual do equipamento solicitado na função RequestCurrentPaperRollSize
            /// </summary>
            /// <returns></returns>
            public void GetCurrentPaperRollSize(ref int iCurrentPaperRollSize)
            {
                this.objIDSysR30.GetCurrentPaperRollSize(ref iCurrentPaperRollSize);
            }

            /// <summary>
            /// Obtem a quilometragem da bobina de papel atual do equipamento solicitado na função RequestCurrentPaperRollKM
            /// </summary>
            /// <returns></returns>
            public void GetCurrentPaperRollKM(ref int iCurrentPaperRollKM)
            {
                this.objIDSysR30.GetCurrentPaperRollKM(ref iCurrentPaperRollKM);
            }

            /// <summary>
            /// Obtem a quantidade de tickets impressos da bobina de papel atual do equipamento solicitado na função RequestCurrentPaperRollTicketsPrinted
            /// </summary>
            /// <returns></returns>
            public void GetCurrentPaperRollTicketsPrinted(ref int iCurrentPaperRollTicketsPrinted)
            {
                this.objIDSysR30.GetCurrentPaperRollTicketsPrinted(ref iCurrentPaperRollTicketsPrinted);
            }

            /// <summary>
            /// Obtem a estimativa de tickets da bobina de papel atual do equipamento solicitado na função RequestCurrentPaperRollTicketsPrinted
            /// </summary>
            /// <returns></returns>
            public void GetCurrentPaperRollEstimatedTickets(ref int iCurrentPaperRollEstimatedTickets)
            {
                this.objIDSysR30.GetCurrentPaperRollEstimatedTickets(ref iCurrentPaperRollEstimatedTickets);
            }

            /// <summary>
            /// Obtem os estados dos alarmes do equipamento solicitado na função RequestAlarmsStatus
            /// </summary>
            /// <returns></returns>
            public void GetAlarmsStatus(ref byte byAlarmsStatus_Cutter_Changed, ref byte byAlarmsStatus_Buzzer_Changed, ref byte byAlarmsStatus_Reserved_1, ref byte byAlarmsStatus_USBFiscal_Activated, ref byte byAlarmsStatus_USBDados_Activated, ref byte byAlarmsStatus_PrinterNoPaper, ref byte byAlarmsStatus_PrinterOpenGate, ref byte byAlarmsStatus_Report24H_Pressioned, ref byte byAlarmsStatus_Report24H_Emitted, ref byte byAlarmsStatus_MPR_75Percent, ref byte byAlarmsStatus_MRP_Full, ref byte byAlarmsStatus_MT_75Percent, ref byte byAlarmsStatus_MT_Full, ref byte byAlarmsStatus_Battery_CriticalLevel, ref byte byAlarmsStatus_BlockViolation, ref byte byAlarmsStatus_UnblockSuccess, ref byte byAlarmsStatus_UnblockTried, ref byte byAlarmsStatus_Cutter_75Percent, ref byte byAlarmsStatus_Cutter_Full, ref byte byAlarmsStatus_Printer_75Percent, ref byte byAlarmsStatus_Printer_Full, ref byte byAlarmsStatus_MasterPasswordChanged, ref byte byAlarmsStatus_AdminReboot, ref byte byAlarmsStatus_TCP_IP_Changed)
            {
                this.objIDSysR30.GetAlarmsStatus(ref byAlarmsStatus_Cutter_Changed, ref byAlarmsStatus_Buzzer_Changed, ref byAlarmsStatus_Reserved_1, ref byAlarmsStatus_USBFiscal_Activated, ref byAlarmsStatus_USBDados_Activated, ref byAlarmsStatus_PrinterNoPaper, ref byAlarmsStatus_PrinterOpenGate, ref byAlarmsStatus_Report24H_Pressioned, ref byAlarmsStatus_Report24H_Emitted, ref byAlarmsStatus_MPR_75Percent, ref byAlarmsStatus_MRP_Full, ref byAlarmsStatus_MT_75Percent, ref byAlarmsStatus_MT_Full, ref byAlarmsStatus_Battery_CriticalLevel, ref byAlarmsStatus_BlockViolation, ref byAlarmsStatus_UnblockSuccess, ref byAlarmsStatus_UnblockTried, ref byAlarmsStatus_Cutter_75Percent, ref byAlarmsStatus_Cutter_Full, ref byAlarmsStatus_Printer_75Percent, ref byAlarmsStatus_Printer_Full, ref byAlarmsStatus_MasterPasswordChanged, ref byAlarmsStatus_AdminReboot, ref byAlarmsStatus_TCP_IP_Changed);
            }

            /// <summary>
            /// Obtem os dados de um alarme do equipamento solicitado na função RequestAlarmData
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Cutter
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Cutter(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_Cutter(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Buzzer
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Buzzer(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_Buzzer(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Temperature
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Temperature(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_Temperature(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_USBFiscal
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_USBFiscal(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_USBFiscal(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_USBDados
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_USBDados(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_USBDados(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_NoPaper
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_NoPaper(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_NoPaper(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_GateOpened
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_GateOpened(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_GateOpened(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_24H_Pressed
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_24H_Pressed(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_24H_Pressed(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_24H_Emitted
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_24H_Emitted(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_24H_Emitted(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_MPR_75
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_MRP_75(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_MRP_75(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_MPR_Full
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_MRP_Full(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_MRP_Full(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_MPR_75
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_MT_75(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_MT_75(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_MPR_Full
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_MT_Full(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_MT_Full(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_BatteryCriticalLevel
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_BatteryCriticalLevel(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_BatteryCriticalLevel(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_BlockViolation
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_BlockViolation(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_BlockViolation(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_UnblockSuccess
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_UnblockSuccess(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_UnblockSuccess(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_UnblockTried
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_UnblockTried(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_UnblockTried(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Cutter_75
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Cutter_75(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_Cutter_75(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Cutter_Full
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Cutter_Full(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_Cutter_Full(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Printer_75
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Printer_75(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_Printer_75(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_Printer_Full
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_Printer_Full(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_Printer_Full(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_MasterPasswordChanged
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_MasterPasswordChanged(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_MasterPasswordChanged(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_AdminReboot
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_AdminReboot(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_AdminReboot(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_CommunicationChanged
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_CommunicationChanged(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_CommunicationChanged(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_WatchDogReboot
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_WatchDogReboot(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_WatchDogReboot(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_BiometricSecurityChanged
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_BiometricSecurityChanged(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_BiometricSecurityChanged(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_PunchTicketChanged
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_PunchTicketInfoChanged(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_PunchTicketInfoChanged(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_PaperRollChanged
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_PaperRollChanged(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_PaperRollChanged(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Obtem os dados do alarme solicitado na função RequestAlarmData_PaperRoll_90
            /// </summary>
            /// <returns></returns>
            public void GetAlarmData_PaperRoll_90(ref byte byAlarmStatus, ref DateTime dtmAlarmDateTime)
            {
                this.objIDSysR30.GetAlarmData_PaperRoll_90(ref byAlarmStatus, ref dtmAlarmDateTime);
            }

            /// <summary>
            /// Verifica se o periférico Hamster está conectado ao PC
            /// </summary>
            /// <returns></returns>
            public bool IsHamsterConnected()
            {
                return this.objIDSysR30.IsHamsterConnected();
            }

            /// <summary>
            /// Obtém o tipo de hamster conectado ao computador
            /// </summary>
            /// <returns>0 = Hamster não encontrado, 1 = NITGEN, 2 = VIRDI</returns>
            public int GetHamsterType()
            {
                return this.objIDSysR30.GetHamsterType();
            }

            /// <summary>
            /// Obtém os templates das biometrias para módulos FIM01 (1000 e 4000 usuários)
            /// </summary>
            /// <param name="rgbySample404_1">Template da amostra do dedo 1</param>
            /// <param name="byFingerPID_1">Id do primeiro dedo no qual foi obtida as amostras</param>
            /// <param name="bySampleID_1">Qual amostra foi utilizada</param>
            /// <param name="byQuality_1">Qualidade da amostra</param>
            /// <param name="rgbySample404_2">Template da amostra do dedo 2</param>
            /// <param name="byFingerPID_2">Id do segundo dedo no qual foi obtida as amostras</param>
            /// <param name="bySampleID_2">Qual amostra foi utilizada</param>
            /// <param name="byQuality_2">Qualidade da amostra</param>
            /// <param name="bRotateSamples">O template será rotacionado</param>
            public bool GetTemplates_FIM01(ref byte[] rgbySample404_1, ref byte byFingerPID_1, ref byte bySampleID_1, ref byte byQuality_1, ref byte[] rgbySample404_2, ref byte byFingerPID_2, ref byte bySampleID_2, ref byte byQuality_2, bool bRotateSamples)
            {
                return this.objIDSysR30.GetTemplates_FIM01(ref rgbySample404_1, ref byFingerPID_1, ref bySampleID_1, ref byQuality_1, ref rgbySample404_2, ref byFingerPID_2, ref bySampleID_2, ref byQuality_2, bRotateSamples);
            }

            /// <summary>
            /// Obtém os templates das biometrias para módulos FIM10 (100 usuários)
            /// </summary>
            /// <param name="rgbySample400_1">Template da amostra do dedo 1</param>
            /// <param name="byFingerPID_1">Id do primeiro dedo no qual foi obtida as amostras</param>
            /// <param name="bySampleID_1">Qual amostra foi utilizada</param>
            /// <param name="byQuality_1">Qualidade da amostra</param>
            /// <param name="rgbySample400_2">Template da amostra do dedo 2</param>
            /// <param name="byFingerPID_2">Id do segundo dedo no qual foi obtida as amostras</param>
            /// <param name="bySampleID_2">Qual amostra foi utilizada</param>
            /// <param name="byQuality_2">Qualidade da amostra</param>
            /// <param name="bRotateSamples">O template será rotacionado</param>
            public bool GetTemplates_FIM10(ref byte[] rgbySample400_1, ref byte byFingerPID_1, ref byte bySampleID_1, ref byte byQuality_1, ref byte[] rgbySample400_2, ref byte byFingerPID_2, ref byte bySampleID_2, ref byte byQuality_2, bool bRotateSamples)
            {
                return this.objIDSysR30.GetTemplates_FIM10(ref rgbySample400_1, ref byFingerPID_1, ref bySampleID_1, ref byQuality_1, ref rgbySample400_2, ref byFingerPID_2, ref bySampleID_2, ref byQuality_2, bRotateSamples);
            }

            /// <summary>
            /// Obtém os templates das biometrias para módulos VIRDI
            /// </summary>
            /// <param name="byFingerPID"></param>
            /// <param name="rgbySample400_1"></param>
            /// <param name="rgbySample400_2"></param>
            /// <returns></returns>
            public bool GetTemplates_Virdi(ref byte byFingerPID, ref byte[] rgbySample400_1, ref byte[] rgbySample400_2)
            {
                return this.objIDSysR30.GetTemplates_Virdi(ref byFingerPID, ref rgbySample400_1, ref rgbySample400_2);
            }

            /// <summary>
            /// Converte o template da biometria de 400 bytes para o de 404 bytes
            /// </summary>
            /// <param name="rgbyTemplate400"></param>
            /// <param name="rgbyTemplate404"></param>
            /// <returns>Verdadeiro se a conversão foi realizada com sucesso</returns>
            public bool ConvertTemplate400ToTemplate404(byte[] rgbyTemplate400, byte[] rgbyTemplate404)
            {
                return this.objIDSysR30.ConvertTemplate400ToTemplate404(rgbyTemplate400, rgbyTemplate404);
            }

            /// <summary>
            /// Converte o template da biometria de 404 bytes para o de 400 bytes
            /// </summary>
            /// <param name="rgbyTemplate404"></param>
            /// <param name="rgbyTemplate400"></param>
            /// <returns>Verdadeiro se a conversão foi realizada com sucesso</returns>
            public bool ConvertTemplate404ToTemplate400(byte[] rgbyTemplate404, byte[] rgbyTemplate400)
            {
                return this.objIDSysR30.ConvertTemplate404ToTemplate400(rgbyTemplate404, rgbyTemplate400);
            }

            public void EncryptBuffer(ref byte[] rgbyBuffer, uint uiSize)
            {
                this.objIDSysR30.EncryptBuffer(ref rgbyBuffer, uiSize);
            }

            public void DecryptBuffer(ref byte[] rgbyBuffer, uint uiSize)
            {
                this.objIDSysR30.DecryptBuffer(ref rgbyBuffer, uiSize);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="strUBSFilePath"></param>
            /// <param name="uiQuantityUsers"></param>
            /// <param name="strEmployerName"></param>
            /// <param name="strCNPJ_CPF"></param>
            /// <param name="strCEI"></param>
            /// <param name="strEmployerAddress"></param>
            /// <param name="strNFR"></param>
            /// <returns></returns>
            public int CreateUBSFile(string strUBSFilePath, uint uiQuantityUsers, string strEmployerName, string strCNPJ_CPF, string strCEI, string strEmployerAddress, string strNFR)
            {
                return this.objIDSysR30.CreateUBSFile(strUBSFilePath, uiQuantityUsers, strEmployerName, strCNPJ_CPF, strCEI, strEmployerAddress, strNFR);
            }

            /// <summary>
            /// Abre um arquivo .ubs informado e lê seu cabeçalho
            /// </summary>
            /// <param name="strUBSFilePath">Endereço do arquivo .ubs</param>
            /// <param name="uiQuantityUsers">Quantidade de usuários contidos no arquivo</param>
            /// <param name="strEmployerName">Nome da empresa/empregador do equipamento de onde foram exportados os usuários</param>
            /// <param name="strCNPJ_CPF">CNPJ ou CPF da empresa/empregador do equipamento de onde foram exportados os usuários</param>
            /// <param name="ulCEI">CEI da empresa/empregador do equipamento de onde foram exportados os usuários</param>
            /// <param name="strEmployerAddress">Endereço da empresa/empregador do equipamento de onde foram exportados os usuários</param>
            /// <param name="strNFR">NFR do equipamento de onde foram exportados os usuários</param>
            /// <returns></returns>
            public int OpenUBSFile(string strUBSFilePath, ref uint uiQuantityUsers, ref string strEmployerName, ref string strCNPJ_CPF, ref ulong ulCEI, ref string strEmployerAddress, ref string strNFR)
            {
                return this.objIDSysR30.OpenUBSFile(strUBSFilePath, ref uiQuantityUsers, ref strEmployerName, ref strCNPJ_CPF, ref ulCEI, ref strEmployerAddress, ref strNFR);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="strPIS"></param>
            /// <param name="strUserName"></param>
            /// <param name="uiKeyCode"></param>
            /// <param name="strBarCode"></param>
            /// <param name="uiProxCode"></param>
            /// <param name="byAccessType"></param>
            /// <param name="strPassword"></param>
            /// <param name="rgbyBiometrics"></param>
            /// <param name="uiSizeSample"></param>
            /// <returns></returns>
            public int ExportUBS_User(string strPIS, string strUserName, uint uiKeyCode, string strBarCode, ulong ulProxCode, byte byStatus, string strPassword, byte byAccessType, byte[] rgbyBiometrics, uint uiSizeSample)
            {
                return this.objIDSysR30.ExportUBS_User(strPIS, strUserName, uiKeyCode, strBarCode, ulProxCode, byStatus, strPassword, byAccessType, rgbyBiometrics, uiSizeSample);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="strPIS"></param>
            /// <param name="strUserName"></param>
            /// <param name="uiKeyCode"></param>
            /// <param name="strBarCode"></param>
            /// <param name="uiProxCode"></param>
            /// <param name="byUserType"></param>
            /// <param name="strPassword"></param>
            /// <param name="rgbyBiometrics"></param>
            /// <param name="uiSizeSample"></param>
            /// <param name="byAccessType"></param>
            /// <returns></returns>
            public int ImportUBS_User(ref string strPIS, ref string strUserName, ref uint uiKeyCode, ref string strBarCode, ref ulong ulProxCode, ref byte byUserType, ref string strPassword, ref byte[] rgbyBiometrics, ref uint uiSizeSample, ref byte byAccessType)
            {
                return this.objIDSysR30.ImportUBS_User(ref strPIS, ref strUserName, ref uiKeyCode, ref strBarCode, ref ulProxCode, ref byUserType, ref strPassword, ref rgbyBiometrics, ref uiSizeSample, ref byAccessType);
            }

            /// <summary>
            /// Fecha um arquivo UBS
            /// </summary>
            /// <returns></returns>
            public int CloseUBSFile()
            {
                return this.objIDSysR30.CloseUBSFile();
            }

            /// <summary>
            /// Abre um arquivo AFD
            /// </summary>
            /// <param name="strAFDFilePath">Endereço do arquivo AFD</param>
            /// <returns></returns>
            public int OpenAFDFile(string strAFDFilePath)
            {
                return this.objIDSysR30.OpenAFDFile(strAFDFilePath);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public int ImportAFD_Register()
            {
                return this.objIDSysR30.ImportAFD_Register();
            }

            /// <summary>
            /// Obtém os dados do Header de um arquivo AFD
            /// </summary>
            /// <param name="uiNSR">Receberá o NSR</param>
            /// <param name="byRegType">Receberá o tipo do registro</param>
            /// <param name="byIdentifyType">Receberá o tipo de identificador da empresa/empregador (CNPJ ou CPJ) do equipamento de onde foi extraído o AFD</param>
            /// <param name="strCNPJ_CPF">Receberá o identificador da empresa/empregador (CNPJ ou CPJ) do equipamento de onde foi extraído o AFD</param>
            /// <param name="ulCEI">Receberá o CEI da empresa/empregador do equipamento de onde foi extraído o AFD</param>
            /// <param name="strEmployerName">Receberá o nome da empresa/empregador (CNPJ ou CPJ) do equipamento de onde foi extraído o AFD</param>
            /// <param name="strNFR">Receberá o NFR do equipamento de onde foi extraído o AFD</param>
            /// <param name="dtmStartDate">Receberá a data do primeiro registro do AFD</param>
            /// <param name="dtmEndDate">Receberá a data do último registro do AFD</param>
            /// <param name="dtmRegDateTime">Receberá a data de geração do AFD</param>
            public void GetAFD_Header(ref uint uiNSR, ref byte byRegType, ref byte byIdentifyType, ref string strCNPJ_CPF, ref ulong ulCEI, ref string strEmployerName, ref string strNFR, ref DateTime dtmStartDate, ref DateTime dtmEndDate, ref DateTime dtmRegDateTime)
            {
                this.objIDSysR30.GetAFD_Header(ref uiNSR, ref byRegType, ref byIdentifyType, ref strCNPJ_CPF, ref ulCEI, ref strEmployerName, ref strNFR, ref dtmStartDate, ref dtmEndDate, ref dtmRegDateTime);
            }

            /// <summary>
            /// Obtém os dados do Trailer de um arquivo AFD
            /// </summary>
            /// <param name="uiNSR">Receberá o NSR</param>
            /// <param name="byRegType">Receberá o tipo do registro</param>
            /// <param name="uiQuantityRegType2">Receberá a quantidade de registros do tipo 2</param>
            /// <param name="uiQuantityRegType3">Receberá a quantidade de registros do tipo 3</param>
            /// <param name="uiQuantityRegType4">Receberá a quantidade de registros do tipo 4</param>
            /// <param name="uiQuantityRegType5">Receberá a quantidade de registros do tipo 5</param>
            public void GetAFD_Trailer(ref uint uiNSR, ref byte byRegType, ref uint uiQuantityRegType2, ref uint uiQuantityRegType3, ref uint uiQuantityRegType4, ref uint uiQuantityRegType5)
            {
                try
                {
                    this.objIDSysR30.GetAFD_Trailer(ref uiNSR, ref byRegType, ref uiQuantityRegType2, ref uiQuantityRegType3, ref uiQuantityRegType4, ref uiQuantityRegType5);
                }
                catch (Exception exError)
                {
                    throw exError;
                }
            }

            /// <summary>
            /// Fecha um arquivo AFD
            /// </summary>
            /// <returns></returns>
            public int CloseAFDFile()
            {
                return this.objIDSysR30.CloseAFDFile();
            }

        #endregion
    }
}
