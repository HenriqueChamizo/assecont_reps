using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using org.cesar.dmplight.watchComm.business;
using org.cesar.dmplight.watchComm.impl.printpointli;
using org.cesar.dmplight.watchComm.impl.printpoint;

namespace ExemploBioLite
{
    public partial class frmPrincipal : Form
    {
        private org.cesar.dmplight.watchComm.impl.WatchComm _watchComm;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnEnviarDataHora_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.OpenConnection();

                DateTime dt = DateTime.Now;

                if (rdbDataHoraIndicada.Checked)
                    dt = new DateTime(dtpData.Value.Year, dtpData.Value.Month, dtpData.Value.Day, dtpHora.Value.Hour, dtpHora.Value.Minute, 0);

                if (rdbUsaHorarioVerao.Checked)
                {
                    DateTime dstInicio = new DateTime(dtInicioHorarioVerao.Value.Year, dtInicioHorarioVerao.Value.Month, dtInicioHorarioVerao.Value.Day);
                    DateTime dtsFim = new DateTime(dtFimHorarioVerao.Value.Year, dtFimHorarioVerao.Value.Month, dtFimHorarioVerao.Value.Day); ;
                    this._watchComm.SetDateTimeAndDST(dt, dstInicio, dtsFim);
                }
                else
                    this._watchComm.SetDateTimeAndDST(dt, null, null);

                this._watchComm.CloseConnection();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                this._watchComm.CloseConnection();

                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private String CaminhoEXE()
        {
            String nomeEXE = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Windows.Forms.Application.ExecutablePath).InternalName;
            String retorno = System.Windows.Forms.Application.ExecutablePath.ToUpper().Replace(nomeEXE.ToUpper(), "");

            return retorno;
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
                new org.cesar.dmplight.watchComm.api.TCPComm(this.txtIPRelogio.Text, 4370);

            tcpComm.SetTimeOut(15000);

            this._watchComm = new org.cesar.dmplight.watchComm.impl.WatchComm(org.cesar.dmplight.watchComm.api.WatchProtocolType.BioLite,
                                                                              tcpComm,
                                                                              1,
                                                                              key,
                                                                              org.cesar.dmplight.watchComm.api.WatchConnectionType.ConnectedMode,
                                                                              "01.00.0000");
        }

        private void ComandoRecepcionadoComSucesso()
        {
            MessageBox.Show("Comando recepcionado com sucesso pelo relógio!", Application.ProductName,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnIncluirFuncionarios_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.OpenConnection();

                CardCollection listCartoes = new CardCollection();

                foreach (DataRow dr in dsSDKBioLite.dtFuncionarios)
                {
                    TemplateCollection templateCollection = new TemplateCollection();
                    Template template = new Template();

                    Card card = new Card();

                    card.Code = dr["Matricula"].ToString();
                    card.Name = dr["Nome"].ToString();

                    if (!dr["Cartao"].ToString().Equals(""))
                        card.Document = dr["Cartao"].ToString();

                    if (!dr["Senha"].ToString().Equals(""))
                        card.PassWord = Int32.Parse(dr["Senha"].ToString());

                    if (dr["Supervisor"].ToString().Equals("True"))
                        card.MasterCard = TypeMasterCard.Master;

                    foreach (DataRow drTemplate in dsSDKBioLite.dtTemplates)
                    {
                        if (drTemplate["Matricula"].ToString().Equals(card.Code))
                        {
                            template = new Template();
                            template.DedoDigital = (TypeDedoDigital) Int32.Parse(drTemplate["Dedo"].ToString());
                            template.Digital = drTemplate["String"].ToString();
                            templateCollection.Add(template);
                        }
                    }

                    card.Template = templateCollection;

                    listCartoes.Add(card);
                }

                this._watchComm.setCardList(listCartoes);

                this._watchComm.CloseConnection();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                this._watchComm.CloseConnection();

                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void btnExcluirFuncionarios_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.OpenConnection();

                CardCollection listCartoes = new CardCollection();

                foreach (DataRow dr in dsSDKBioLite.dtFuncionarios)
                {
                    TemplateCollection templateCollection = new TemplateCollection();
                    Template template = new Template();

                    Card card = new Card();

                    card.Code = dr["Matricula"].ToString();
                    card.Name = dr["Nome"].ToString();

                    if (!dr["Senha"].ToString().Equals(""))
                        card.PassWord = Int32.Parse(dr["Senha"].ToString());

                    if (dr["Supervisor"].ToString().Equals("True"))
                        card.MasterCard = TypeMasterCard.Master;

                    foreach (DataRow drTemplate in dsSDKBioLite.dtTemplates)
                    {
                        if (drTemplate["Matricula"].ToString().Equals(card.Code))
                        {
                            template = new Template();
                            template.DedoDigital = (TypeDedoDigital)Int32.Parse(drTemplate["Dedo"].ToString());
                            template.Digital = drTemplate["String"].ToString();
                            templateCollection.Add(template);
                        }
                    }

                    card.Template = templateCollection;

                    listCartoes.Add(card);
                }

                this._watchComm.eraseCardListBioLite(listCartoes);

                this._watchComm.CloseConnection();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                this._watchComm.CloseConnection();

                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void btnObterTodosFuncionarios_Click(object sender, EventArgs e)
        {
            try
            {
                CardCollection cardCollection;

                InstanciaWatchComm();

                this._watchComm.OpenConnection(); 

                cardCollection = this._watchComm.getCardList();

                this._watchComm.CloseConnection();

                dsSDKBioLite.dtFuncionarios.Clear();
                dsSDKBioLite.dtTemplates.Clear();

                foreach (org.cesar.dmplight.watchComm.business.Card card in cardCollection)
                {
                    DataRow dr = dsSDKBioLite.dtFuncionarios.NewRow();
                    dr["Matricula"] = card.Code;
                    dr["Nome"] = card.Name;
                    dr["Senha"] = card.PassWord;
                    dr["Supervisor"] = card.MasterCard == TypeMasterCard.Funcionario ? false : true;

                    dsSDKBioLite.dtFuncionarios.Rows.Add(dr);

                    foreach (Template template in card.Template)
                    {
                        DataRow drTemplate = dsSDKBioLite.dtTemplates.NewRow();
                        drTemplate["Matricula"] = card.Code;
                        drTemplate["Dedo"] = (int)template.DedoDigital;
                        drTemplate["String"] = template.Digital;

                        dsSDKBioLite.dtTemplates.Rows.Add(drTemplate);
                    }
                    
                }

                ComandoRecepcionadoComSucesso();

            }
            catch (Exception ex)
            {
                this._watchComm.CloseConnection();

                MessageBox.Show(ex.Message, "Erro");
            }

        }

        private void btnObterStatus_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.OpenConnection();

                org.cesar.dmplight.watchComm.impl.biolite.BioLiteStatus bioLiteStatus = this._watchComm.GetBioLiteStatus();

                dsSDKBioLite.dtStatus.Clear();

                // Data e Hora.
                DataRow dr = dsSDKBioLite.dtStatus.NewRow();
                dr["Propriedade"] = "Data e Hora";
                dr["Valor"] = bioLiteStatus.DeviceDateTime;
                dsSDKBioLite.dtStatus.Rows.Add(dr);

                // Capacidade de Digitais
                dr = dsSDKBioLite.dtStatus.NewRow();
                dr["Propriedade"] = "Capacidade de Digitais";
                dr["Valor"] = bioLiteStatus.FingerprintCapacity;
                dsSDKBioLite.dtStatus.Rows.Add(dr);

                // Ocupação de Digitais
                dr = dsSDKBioLite.dtStatus.NewRow();
                dr["Propriedade"] = "Ocupação de Digitais";
                dr["Valor"] = bioLiteStatus.FingerprintOccupation;
                dsSDKBioLite.dtStatus.Rows.Add(dr);

                // Capacidade de Registros
                dr = dsSDKBioLite.dtStatus.NewRow();
                dr["Propriedade"] = "Capacidade de Registros";
                dr["Valor"] = bioLiteStatus.RecordsCapacity;
                dsSDKBioLite.dtStatus.Rows.Add(dr);

                // Ocupação de Registros
                dr = dsSDKBioLite.dtStatus.NewRow();
                dr["Propriedade"] = "Ocupação de Registros";
                dr["Valor"] = bioLiteStatus.RecordsOccupation;
                dsSDKBioLite.dtStatus.Rows.Add(dr);

                // Capacidade de Usuários
                dr = dsSDKBioLite.dtStatus.NewRow();
                dr["Propriedade"] = "Capacidade de Usuários";
                dr["Valor"] = bioLiteStatus.UsersCapacity;
                dsSDKBioLite.dtStatus.Rows.Add(dr);

                // Ocupação de Usuários
                dr = dsSDKBioLite.dtStatus.NewRow();
                dr["Propriedade"] = "Ocupação de Usuários";
                dr["Valor"] = bioLiteStatus.UsersOccupation;
                dsSDKBioLite.dtStatus.Rows.Add(dr);

                // Ocupação de Supervisores
                dr = dsSDKBioLite.dtStatus.NewRow();
                dr["Propriedade"] = "Ocupação de Supervisores";
                dr["Valor"] = bioLiteStatus.MasterOccupation ;
                dsSDKBioLite.dtStatus.Rows.Add(dr);

                // Ocupação de Senhas
                dr = dsSDKBioLite.dtStatus.NewRow();
                dr["Propriedade"] = "Ocupação de Senhas";
                dr["Valor"] = bioLiteStatus.PasswordOccupation;
                dsSDKBioLite.dtStatus.Rows.Add(dr);

                // Versão do Firmware
                dr = dsSDKBioLite.dtStatus.NewRow();
                dr["Propriedade"] = "Versão de Firmware";
                dr["Valor"] = bioLiteStatus.FirmwareVersion;
                dsSDKBioLite.dtStatus.Rows.Add(dr);
                

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                this._watchComm.CloseConnection();

                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void btnObterColeta_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.OpenConnection();

                dsSDKBioLite.dtColeta.Clear();

                System.Collections.ArrayList arrayList = this._watchComm.CollectAll();

                foreach (org.cesar.dmplight.watchComm.api.AbstractPunchMessage marcacao in arrayList)
                {
                    DataRow dr = dsSDKBioLite.dtColeta.NewRow();
                    dr["Matricula"] = marcacao.ID;
                    dr["DataHora"] = marcacao.Date.ToString();
                    dr["EntradaSaida"] = marcacao.WatchEvent.ToString();
                    dsSDKBioLite.dtColeta.Rows.Add(dr);
                }

                ComandoRecepcionadoComSucesso();

            }
            catch (Exception ex)
            {
                this._watchComm.CloseConnection();

                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void btnApagarMarcacoes_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.OpenConnection();

                this._watchComm.EraseMarkingPoints();

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                this._watchComm.CloseConnection();

                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void btnEnviarSirenesDiarias_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.OpenConnection();

                int idAlarm = 1;
                foreach (DataRow dr in dsSDKBioLite.dtSireneDiaria.Rows)
                {
                    this._watchComm.SetBioLiteDailyAlarm(idAlarm, Int32.Parse(dr["Hora"].ToString()), Int32.Parse(dr["Minuto"].ToString()));
                    idAlarm++;

                    if (idAlarm > 50)
                    {
                        MessageBox.Show("São permitidos apenas 50 sirenes diarias.", "Erro");
                        break;
                    }
                }

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                this._watchComm.CloseConnection();

                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void btnApagarSirenesDiarias_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.OpenConnection();

                for (int i =1;i<=50;i++)
                {
                    this._watchComm.EraseBioLiteDailyAlarm(i);
                }

                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                this._watchComm.CloseConnection();

                MessageBox.Show(ex.Message, "Erro");
            }
        }

       

        private void btnEnviarSirenesSemanais_Click(object sender, EventArgs e)
        {
            try
            {
                List<AlarmHour> alarmsDomingo = new List<AlarmHour>();
                List<AlarmHour> alarmsSegunda = new List<AlarmHour>();
                List<AlarmHour> alarmsTerca = new List<AlarmHour>();
                List<AlarmHour> alarmsQuarta = new List<AlarmHour>();
                List<AlarmHour> alarmsQuinta = new List<AlarmHour>();
                List<AlarmHour> alarmsSexta = new List<AlarmHour>();
                List<AlarmHour> alarmsSabado = new List<AlarmHour>();

                InstanciaWatchComm();

                this._watchComm.OpenConnection();

                foreach (DataRow dr in dsSDKBioLite.dtSireneSemanal.Rows)
                {
                    AlarmHour alarm = new AlarmHour();
                    alarm.Hour = Int32.Parse(dr["Hora"].ToString());
                    alarm.Minute = Int32.Parse(dr["Minuto"].ToString());

                    if (dr["Dia"].ToString() == "Domingo")
                        alarmsDomingo.Add(alarm);
                    if (dr["Dia"].ToString() == "Segunda")
                        alarmsDomingo.Add(alarm);
                    if (dr["Dia"].ToString() == "Terça")
                        alarmsDomingo.Add(alarm);
                    if (dr["Dia"].ToString() == "Quarta")
                        alarmsDomingo.Add(alarm);
                    if (dr["Dia"].ToString() == "Quinta")
                        alarmsDomingo.Add(alarm);
                    if (dr["Dia"].ToString() == "Sexta")
                        alarmsDomingo.Add(alarm);
                    if (dr["Dia"].ToString() == "Sabado")
                        alarmsDomingo.Add(alarm);
                }

                if (alarmsDomingo.Count > 0)
                    this._watchComm.SetBioLiteWeeklyAlarm(alarmsDomingo.ToArray(), AlarmHour.EWeekDay.Friday);
                if (alarmsSegunda.Count > 0)
                    this._watchComm.SetBioLiteWeeklyAlarm(alarmsSegunda.ToArray(), AlarmHour.EWeekDay.Monday);
                if (alarmsTerca.Count > 0)
                    this._watchComm.SetBioLiteWeeklyAlarm(alarmsTerca.ToArray(), AlarmHour.EWeekDay.Saturday);
                if (alarmsQuarta.Count > 0)
                    this._watchComm.SetBioLiteWeeklyAlarm(alarmsQuarta.ToArray(), AlarmHour.EWeekDay.Sunday);
                if (alarmsQuinta.Count > 0)
                    this._watchComm.SetBioLiteWeeklyAlarm(alarmsQuinta.ToArray(), AlarmHour.EWeekDay.Thursday);
                if (alarmsSexta.Count > 0)
                    this._watchComm.SetBioLiteWeeklyAlarm(alarmsSexta.ToArray(), AlarmHour.EWeekDay.Tuesday);
                if (alarmsSabado.Count > 0)
                    this._watchComm.SetBioLiteWeeklyAlarm(alarmsSabado.ToArray(), AlarmHour.EWeekDay.Wednesday);

                ComandoRecepcionadoComSucesso();

            }
            catch (Exception ex)
            {
                this._watchComm.CloseConnection();

                MessageBox.Show(ex.Message, "Erro");
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow dr = dsSDKBioLite.dtSireneSemanal.NewRow();
            dr["Dia"] = cmbDiaSemana.Text;
            dr["Hora"] = txthora.Text;
            dr["Minuto"] = txtMinuto.Text;
            dsSDKBioLite.dtSireneSemanal.Rows.Add(dr);
        }

        private void btnApagarSirenesSemanais_Click(object sender, EventArgs e)
        {
            try
            {
                InstanciaWatchComm();

                this._watchComm.OpenConnection();

                int diaSemana=0;
                int contador = 1;

                while (diaSemana < 8)
                {
                    this._watchComm.EraseBioLiteWeeklyAlarm(contador, (AlarmHour.EWeekDay)diaSemana);
                    contador++;

                    if (contador > 12)
                    {
                        contador = 1;
                        diaSemana++;
                    }
                }
                
                ComandoRecepcionadoComSucesso();
            }
            catch (Exception ex)
            {
                this._watchComm.CloseConnection();

                MessageBox.Show(ex.Message, "Erro");
            }
        }

        private void grbEnvioDataHoraHorarioVerao_Enter(object sender, EventArgs e)
        {

        }
    }
}
