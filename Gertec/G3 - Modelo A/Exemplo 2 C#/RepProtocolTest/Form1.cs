using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RepProtocolTestSuite
{
    public partial class Form1 : Form
    {
        [DllImport("Iphlpapi")]
        private static extern UInt32 GetBestInterface(UInt32 destAddr, ref UInt32 bestIfIndex);


        int countOK;
        int countError;

        private LogProtocol log;
        private RepProtocol repProtocol;

        private byte[] downloadedFirmware;
        DateTime downloadedFirmwareDate;

        public Form1()
        {
            InitializeComponent();

            Text += " [v. " + Application.ProductVersion + "]";

            cbEmployeeGetFilter.Items.Clear();
            cbEmployeeGetFilter.Items.Add(RepProtocol.FiltroEmpregado.PIS);
            cbEmployeeGetFilter.Items.Add(RepProtocol.FiltroEmpregado.ID);
            cbEmployeeGetFilter.Items.Add(RepProtocol.FiltroEmpregado.CNTLS);
            cbEmployeeGetFilter.Items.Add(RepProtocol.FiltroEmpregado.KBD);
            cbEmployeeGetFilter.Items.Add(RepProtocol.FiltroEmpregado.BIO);

            cbGetRegsFilter.Items.Clear();
            cbGetRegsFilter.Items.Add(RepProtocol.FiltroRegistro.All);
            cbGetRegsFilter.Items.Add(RepProtocol.FiltroRegistro.Last24hs);
            cbGetRegsFilter.Items.Add(RepProtocol.FiltroRegistro.DateRange);
            cbGetRegsFilter.Items.Add(RepProtocol.FiltroRegistro.NsrRange);

            cbGetRegsFilter.SelectedIndex = 0;
            cbEmployerType.SelectedIndex = 0;
            cbEmployeeGetFilter.SelectedIndex = 0;

            this.log = new LogProtocol(this.lbLog, this.ipaddr.Text);
            this.repProtocol = new RepProtocol(this.ipaddr.Text);
            bEmployerFillExample.Enabled = true;
            bEmployeeFillExample.Enabled = true;
            tcCommandGroups.Enabled = true;
            gbAuth.Enabled = true;

#if DEBUG
            ipaddr.Text = "192.168.1.188";
            mtbAuthCpf.Text = "69604699610";
            tbAuthPwd.Text = "1234";
#else
            ipaddr.Text = "";
            mtbAuthCpf.Text = "";
            tbAuthPwd.Text = "";
#endif
        }

        private void SetProgress(int current, int total)
        {
            Invoke(delegate()
            {
                bCancelOperation.Enabled = true;
                bCancelOperation.Visible = true;
                pbWorking.Style = ProgressBarStyle.Continuous;
                if (current < 0) { current = 0; }
                pbWorking.Minimum = 0;
                pbWorking.Maximum = total;
                pbWorking.Value = current;
            });
        }

        private void SetProgress(bool working)
        {
            Invoke(delegate()
            {
                bCancelOperation.Visible = false;
                if (working)
                {
                    pbWorking.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    pbWorking.Value = 0;
                    pbWorking.Style = ProgressBarStyle.Continuous;
                }
            });
        }

        private void BtnClick(Control btn, Action func)
        {
            bwTask.DoWork += new DoWorkEventHandler(bwTask_DoWork);
            bwTask.RunWorkerAsync(new object[] { btn, func });
        }

        private void Invoke(Action func)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate()
                {
                    Invoke(func);
                }));
            }
            else
            {
                func();
            }
        }

        void bwTask_DoWork(object sender, DoWorkEventArgs e)
        {
            Control btn = (Control)((object[])(e.Argument))[0];
            Action func = (Action)((object[])(e.Argument))[1];
            Invoke(new MethodInvoker(delegate()
            {
                Cursor = Cursors.AppStarting;
                panelAll.Enabled = false;
                epSuccess.Clear();
                epError.Clear();
            }));
            SetProgress(true);
            try
            {
                func();
                Invoke(new MethodInvoker(delegate()
                {
                    epSuccess.SetError(btn, "OK");
                }));
            }
            catch (Exception ex)
            {
                Invoke(new MethodInvoker(delegate()
                {
                    epError.SetError(btn, ex.Message);
                }));
            }
            SetProgress(false);
            Invoke(new MethodInvoker(delegate()
            {
                panelAll.Enabled = true;
                Cursor = Cursors.Default;
            }));
            bwTask.DoWork -= new DoWorkEventHandler(bwTask_DoWork);
        }

        //private void button2_Click(object sender, EventArgs e)
        //{

        //    string localIP = "?";
        //    IPAddress netMask = null;

        //    if (this.ipaddr.Text != "")
        //    {
        //        UInt32 bestIndex = 0;
        //        UInt32 ret = UInt32.MaxValue;
        //        try
        //        {
        //            ret = GetBestInterface(BitConverter.ToUInt32(IPAddress.Parse(ipaddr.Text).GetAddressBytes(), 0), ref bestIndex);
        //        }
        //        catch
        //        {
        //            MessageBox.Show("Endereço inválido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }
        //        if (ret != 0)
        //        {
        //            MessageBox.Show("Não há uma conexão disponível com o endereço digitado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }
        //        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        //        {
        //            if (nic.GetIPProperties().GetIPv4Properties().Index == bestIndex)
        //            {
        //                foreach (UnicastIPAddressInformation uip in nic.GetIPProperties().UnicastAddresses)
        //                {
        //                    if (uip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        //                    {
        //                        localIP = uip.Address.ToString();
        //                        netMask = uip.IPv4Mask;
        //                        break;
        //                    }
        //                }
        //            }
        //            if (localIP != "?")
        //            {
        //                break;
        //            }
        //        }
        //        if (localIP != "?" /* && IPAddressExtensions.IsInSameSubnet(IPAddress.Parse(localIP), IPAddress.Parse(ipaddr.Text), netMask) */)
        //        {
        //            this.log = new LogProtocol(this.lbLog, this.ipaddr.Text, localIP);
        //            this.log.LogMessagePrint("Programa Inicializado! => REP IP = " + this.ipaddr.Text + " / LOCAL IP = " + localIP);
        //            this.repProtocol = new RepProtocol(this.ipaddr.Text);
        //            bEmployerFillExample.Enabled = true;
        //            bEmployeeFillExample.Enabled = true;
        //            tcCommandGroups.Enabled = true;
        //            gbAuth.Enabled = true;
        //        }
        //        else
        //        {
        //            MessageBox.Show("O endereço do REP não foi encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Insira o Endereço IP do Equipamento", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void clearLog_Click(object sender, EventArgs e)
        {
            this.pbWorking.Value = 0;
            this.pbWorking.Refresh();
            this.lbLog.Items.Clear();
        }

        private void btGetTime_Click(object sender, EventArgs e)
        {
            BtnClick(btGetTime, delegate()
            {
                Invoke(delegate() { lTime.Text = ""; });
                DateTime r = repProtocol.GetTime();
                Invoke(delegate()
                {
                    lTime.Text = r.ToShortDateString() + " " + r.ToLongTimeString();
                });
            });
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            BtnClick(bGetMrpStatus, delegate()
            {
                RepProtocol.MrpStatus r = repProtocol.GetMrpStatus();
                Invoke(delegate()
                {
                    ObjectDumper.Write(r, 0, log);
                });
            });
        }

        private void btGetRegs_Click(object sender, EventArgs e)
        {
            BtnClick(btGetRegs, delegate()
            {
                RepProtocol.FiltroRegistro f = RepProtocol.FiltroRegistro.All;
                string from = "";
                string to = "";
                Invoke(delegate()
                {
                    f = (RepProtocol.FiltroRegistro)cbGetRegsFilter.SelectedItem;
                    switch (f)
                    {
                        case RepProtocol.FiltroRegistro.DateRange:
                            from = dtpFilterFrom.Value.AddDays(-1).ToString("ddMMyy");
                            to = dtpFilterTo.Value.ToString("ddMMyy");
                            break;
                        case RepProtocol.FiltroRegistro.NsrRange:
                            from = ((int)nudFilterFrom.Value).ToString().PadLeft(9, '0');
                            to = ((int)nudFilterTo.Value).ToString().PadLeft(9, '0');
                            break;
                        default:
                            break;
                    }
                });
                List<string> r = repProtocol.GetRegs(f, from, to, delegate(int cur, int tot)
                {
                    SetProgress(cur, tot);
                });
                Invoke(delegate()
                {
                    lbRegs.Items.Clear();
                    lbRegs.Items.AddRange(r.ToArray());
                });
                //Invoke(delegate() { ObjectDumper.Write(r, 0, log); });
            });
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RepProtocol.Empregador empregador = new RepProtocol.Empregador();
            empregador.CEI = tbEmployerCEI.Text;
            empregador.CNPJ = tbEmployerCNPJ.Text;
            empregador.Endereco = tbEmployerAddress.Text;
            empregador.Nome = tbEmployerName.Text;
            empregador.Tipo = (cbEmployerType.SelectedIndex + 1).ToString();
            BtnClick(bEmployerSet, delegate()
            {
                if (
                    System.Text.RegularExpressions.Regex.Replace(empregador.Nome, @"\s+", "").Length == 0 ||
                    System.Text.RegularExpressions.Regex.Replace(empregador.Endereco, @"\s+", "").Length == 0 ||
                    System.Text.RegularExpressions.Regex.Replace(empregador.CNPJ, @"\s+", "").Length == 0
                    )
                {
                    throw new Exception("Campos em branco");
                }
                repProtocol.SetEmployer(empregador);
            });
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            int rnd = 0;

            if (clbStressCommands.CheckedIndices.Count == 0)
            {
                clbStressCommands.SetItemChecked(0, true);
            }

            do
            {
                rnd = new Random().Next(clbStressCommands.Items.Count);
            } while (!clbStressCommands.CheckedIndices.Contains(rnd));

            try
            {
                string[] msg = new string[] { "GetTime", "GetMrpStatus", "GetRegs(All)", "GetRegs(Last24hs)", "GetEmployer" };
                object r;
                if (rnd > msg.Length) { rnd = 0; }
                Invoke(delegate() { log.Write(msg[rnd]); });
                switch (rnd)
                {
                    case 1:
                        r = repProtocol.GetMrpStatus();
                        break;
                    case 2:
                        r = repProtocol.GetRegs(RepProtocol.FiltroRegistro.All, "", "", delegate(int cur, int tot) {
                            SetProgress(cur, tot);
                        });
                        break;
                    case 3:
                        r = repProtocol.GetRegs(RepProtocol.FiltroRegistro.Last24hs, "", "", delegate(int cur, int tot)
                        {
                            SetProgress(cur, tot);
                        });
                        break;
                    case 4:
                        r = repProtocol.GetEmployer();
                        break;
                    default:
                        rnd = 0;
                        r = repProtocol.GetTime();
                        break;
                }
                Invoke(delegate() { log.WriteLine(": OK"); });
                Invoke(delegate() { ObjectDumper.Write(r, 0, log); });
                countOK++;
            }
            catch (Exception)
            {
                Invoke(delegate() { log.WriteLine(": Falha"); });
                countError++;
            }
            this.label14.Text = countOK.ToString();
            this.label13.Text = countError.ToString();
            this.timer1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tcCommandGroups.Selecting += new TabControlCancelEventHandler(tabControl1_Selecting);
            clbStressCommands.Enabled = false;
            //bInitialize.Enabled = false;
            bStartStressTest.Enabled = false;
            bStopStressTest.Enabled = true;
            countOK = 0;
            countError = 0;
            this.label14.Text = countOK.ToString();
            this.label13.Text = countError.ToString();
            timer1_Tick(null, null);
        }

        void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tcCommandGroups.Selecting -= new TabControlCancelEventHandler(tabControl1_Selecting);
            clbStressCommands.Enabled = true;
            //bInitialize.Enabled = true;
            bStartStressTest.Enabled = true;
            bStopStressTest.Enabled = false;
            gbAuth.Enabled = true;
            this.timer1.Enabled = false;
        }

        private void btGetEmployer_Click(object sender, EventArgs e)
        {
            
            BtnClick(bEmployerGet, delegate()
            {
                RepProtocol.Empregador empregador = new RepProtocol.Empregador();
                empregador = repProtocol.GetEmployer();
                Invoke(delegate()
                {
                    tbEmployerCEI.Text = empregador.CEI;
                    tbEmployerCNPJ.Text = empregador.CNPJ;
                    tbEmployerAddress.Text = empregador.Endereco;
                    tbEmployerName.Text = empregador.Nome;
                    cbEmployerType.SelectedIndex = int.Parse(empregador.Tipo) - 1;
                });
            });
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RepProtocol.Empregado empregado = new RepProtocol.Empregado();
            empregado.CNTLS = tbEmployeeCNTLS.Text;
            empregado.ID = tbEmployeeID.Text;
            empregado.KBD = tbEmployeeKBD.Text;
            empregado.Nome = tbEmployeeName.Text;
            empregado.PIS = tbEmployeePIS.Text;
            BtnClick(bEmployeeSet, delegate()
            {
                repProtocol.SetEmployee(empregado);
            });
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BtnClick(bEmployeeGet, delegate()
            {
                RepProtocol.Empregado empregado = new RepProtocol.Empregado();
                RepProtocol.FiltroEmpregado f = RepProtocol.FiltroEmpregado.PIS;
                Invoke(delegate() { f = (RepProtocol.FiltroEmpregado)cbEmployeeGetFilter.SelectedItem; });
                empregado = repProtocol.GetEmployee(f, tbEmployeeGetFilter.Text);
                Invoke(delegate()
                {
                    tbEmployeeCNTLS.Text = empregado.CNTLS;
                    tbEmployeeID.Text = empregado.ID;
                    tbEmployeeKBD.Text = empregado.KBD;
                    tbEmployeeName.Text = empregado.Nome;
                    tbEmployeePIS.Text = empregado.PIS;
                });
            });
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tbEmployerCNPJ.Text = "03654119000176";
            tbEmployerCEI.Text = "01234567890";
            tbEmployerName.Text = "GERTEC BRASIL LTDA";
            tbEmployerAddress.Text = "AV. JABAQUARA, 3060 - SAO PAULO";
            cbEmployerType.SelectedIndex = 0;
        }

        private void button13_Click(object sender, EventArgs e)
        {

            this.tbEmployeePIS.Text = PisUtils.RndPis();
            this.tbEmployeeName.Text = "Funcionário " + new Random().Next(1, 100000).ToString().PadLeft(5, '0');
            this.tbEmployeeID.Text = new Random().Next(1, 10000000).ToString().PadLeft(7, '0');
            this.tbEmployeeKBD.Text = new Random().Next(10000).ToString().PadLeft(4, '0');
            byte[] rndCl = new byte[5];
            new Random().NextBytes(rndCl);
            this.tbEmployeeCNTLS.Text = BitConverter.ToString(rndCl).Replace("-", "");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            RepProtocol.Empregador empregador = new RepProtocol.Empregador();
            empregador.CEI = tbEmployerCEI.Text;
            empregador.CNPJ = tbEmployerCNPJ.Text;
            empregador.Endereco = tbEmployerAddress.Text;
            empregador.Nome = tbEmployerName.Text;
            empregador.Tipo = (cbEmployerType.SelectedIndex + 1).ToString();
            BtnClick(bEmployerEdit, delegate()
            {
                if (
                    System.Text.RegularExpressions.Regex.Replace(empregador.Nome, @"\s+", "").Length == 0 ||
                    System.Text.RegularExpressions.Regex.Replace(empregador.Endereco, @"\s+", "").Length == 0 ||
                    System.Text.RegularExpressions.Regex.Replace(empregador.CNPJ, @"\s+", "").Length == 0
                    )
                {
                    throw new Exception("Campos em branco");
                }
                repProtocol.EditEmployer(empregador);
            });
        }

        private void button15_Click(object sender, EventArgs e)
        {
            BtnClick(bSetTime, delegate()
            {
                repProtocol.SetTime(DateTime.Now);
            });
        }


        private void button16_Click(object sender, EventArgs e)
        {
            string nfr = textNFR.Text;
            BtnClick(bSetNFR, delegate()
            {
                RepProtocol.MrpStatus status = repProtocol.GetMrpStatus();
                if (status.NFR.Length > 0) {
                    throw new Exception("NFR já cadastrada");
                }
                repProtocol.SetNFR(nfr);
            });
        }

        private void button17_Click(object sender, EventArgs e)
        {
            BtnClick(bEmployeeErase, delegate()
            {
                string pisToErase = "";
                Invoke(delegate() { pisToErase = tbEmployeeErasePIS.Text; });
                repProtocol.RemoveEmployee(pisToErase);
            });
        }

        private void button18_Click(object sender, EventArgs e)
        {
            BtnClick(button18, delegate()
            {
                string pis = "";
                Invoke(delegate() { pis = fingerPIS.Text; });
                List<string> fpids = repProtocol.GetTemplateEnroll(pis);
                Invoke(delegate()
                {
                    cbFingerIds.Items.Clear();
                    cbFingerIds.Items.AddRange(fpids.ToArray());
                    if (cbFingerIds.Items.Count > 0)
                    {
                        cbFingerIds.Text = cbFingerIds.Items[0].ToString();
                    }
                });

            });
        }

        private void button19_Click(object sender, EventArgs e)
        {

            BtnClick(button19, delegate()
            {
                string id = "";
                Invoke(delegate() { id = cbFingerIds.Text; });
                byte[] template = repProtocol.GetTemplateFile(id);
                Invoke(delegate()
                {
                    Exception error;
                    do
                    {
                        error = null;
                        if (sfdFingerTemplate.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            try
                            {
                                System.IO.File.WriteAllBytes(sfdFingerTemplate.FileName, template);
                            }
                            catch (Exception ex)
                            {
                                if (MessageBox.Show(this, "Erro ao salvar o arquivo: \r\n\r\n " + ex.Message, "Erro", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                                {
                                    error = ex;
                                }
                            }
                        }
                    } while (error != null);
                });
            });
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((RepProtocol.FiltroRegistro)cbGetRegsFilter.SelectedItem)
            {
                case RepProtocol.FiltroRegistro.NsrRange:
                    nudFilterFrom.Visible = nudFilterTo.Visible = true;
                    dtpFilterFrom.Visible = dtpFilterTo.Visible = false;
                    lFilterFrom.Visible = lFilterTo.Visible = true;
                    break;
                case RepProtocol.FiltroRegistro.DateRange:
                    dtpFilterFrom.Visible = dtpFilterTo.Visible = true;
                    nudFilterFrom.Visible = nudFilterTo.Visible = false;
                    lFilterFrom.Visible = lFilterTo.Visible = true;
                    break;
                default:
                    dtpFilterFrom.Visible = dtpFilterTo.Visible = false;
                    nudFilterFrom.Visible = nudFilterTo.Visible = false;
                    lFilterFrom.Visible = lFilterTo.Visible = false;
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tbEmployerCEI.Text = "";
            tbEmployerCNPJ.Text = "";
            tbEmployerAddress.Text = "";
            tbEmployerName.Text = "";
            cbEmployerType.SelectedIndex = 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tbEmployeeName.Text = "";
            tbEmployeePIS.Text = "";
            tbEmployeeID.Text = "";
            tbEmployeeKBD.Text = "";
            tbEmployeeCNTLS.Text = "";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            lbRegs.Items.Clear();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            RepProtocol.Empregado empregado = new RepProtocol.Empregado();
            empregado.CNTLS = tbEmployeeCNTLS.Text;
            empregado.ID = tbEmployeeID.Text;
            empregado.KBD = tbEmployeeKBD.Text;
            empregado.Nome = tbEmployeeName.Text;
            empregado.PIS = tbEmployeePIS.Text;
            BtnClick(bEmployeeEdit, delegate()
            {
                repProtocol.EditEmployee(empregado);
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!panelAll.Enabled)
            {
                if (MessageBox.Show("Sair antes de concluir a operação atual?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void bImportEmployee_Click(object sender, EventArgs e)
        {
            if (ofdImportEmployee.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                repProtocol.CancelOperation = false;
                try
                {
                    List<RepProtocol.Empregado> empregados = RepProtocol.Empregado.Importar(ofdImportEmployee.FileName);
                    if (empregados.Count == 0)
                    {
                        MessageBox.Show("Não há registros válidos no arquivo selecionado", "Importação de registros", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (MessageBox.Show("Foram encontrados " + empregados.Count + " registros no arquivo. Continuar?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            BtnClick(bEmployeeImport, delegate()
                            {
                                int importCount = 0;
                                int totalCount = 0;
                                repProtocol.Timeout = 5000;
                                foreach (RepProtocol.Empregado emp in empregados)
                                {
                                    if (repProtocol.CancelOperation)
                                    {
                                        throw new Exception("Operação cancelada");
                                    }
                                    try
                                    {
                                        repProtocol.SetEmployee(emp);
                                        importCount++;
                                    }
                                    catch (WebException ex)
                                    {
                                        if (ex.Status == WebExceptionStatus.Timeout)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            throw ex;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Trace.WriteLine("Import Exception: " + ex.Message);
                                    }
                                    totalCount++;
                                    SetProgress(totalCount, empregados.Count);
                                }
                                repProtocol.Timeout = RestServices.DefaultTimeout;
                                if (importCount == 0)
                                {
                                    throw new Exception("Nenhum registro importado");
                                }
                                else
                                {
                                    Invoke(delegate()
                                    {
                                        MessageBox.Show(this, +importCount + " registros importados de um total de " + empregados.Count + " encontrados no arquivo", "Resultado da importação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    });
                                }
                            });
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Impossível abrir arquivo", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveLog_Click(object sender, EventArgs e)
        {
            if (sfdLog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    List<string> lines = new List<string>();
                    foreach (object i in lbLog.Items)
                    {
                        lines.Add(i.ToString());
                    }
                    System.IO.File.WriteAllLines(sfdLog.FileName, lines);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar o log: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tbAuthPwd_TextChanged(object sender, EventArgs e)
        {
            RestServices.SetDefaultAuth(System.Text.RegularExpressions.Regex.Replace(mtbAuthCpf.Text, "[^0-9]", ""), tbAuthPwd.Text);
            repProtocol.SetAuth(System.Text.RegularExpressions.Regex.Replace(mtbAuthCpf.Text, "[^0-9]", ""), tbAuthPwd.Text);
        }

        private void bExportRegs_Click(object sender, EventArgs e)
        {
            if (sfdRegs.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    List<string> lines = new List<string>();
                    foreach (object i in lbRegs.Items)
                    {
                        lines.Add(i.ToString());
                    }
                    System.IO.File.WriteAllLines(sfdRegs.FileName, lines);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao exportar registros: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bMtGetStatus_Click(object sender, EventArgs e)
        {
            BtnClick(bMtGetStatus, delegate()
            {
                RepProtocol.MtStatus status = repProtocol.GetMtStatus();
                Invoke(delegate()
                {
                    ObjectDumper.Write(status, 0, log);
                });
            });
        }

        private void bChangePassword_Click(object sender, EventArgs e)
        {
            epError.Clear();
            if (tbNewPass.Text.Length < 4 || tbNewPass.Text.Length > 8)
            {
                epError.SetError(tbNewPass, "Senha deve ter entre 4 e 8 dígitos");
                return;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(tbNewPass.Text, "[^0-9]"))
            {
                epError.SetError(tbNewPass, "Senha deve conter apenas números");
                return;
            }
            if (tbNewPass.Text != tbNewPassConf.Text)
            {
                epError.SetError(tbNewPassConf, "A confirmação da senha não confere");
                return;
            }
            BtnClick(bChangePassword, delegate()
            {
                string newPass = "";
                Invoke(delegate() { newPass = tbNewPass.Text; });
                repProtocol.SetPassword(newPass);
            });
        }

        private void cbEmployerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbEmployerType.SelectedIndex == 0)
            {
                lCpfCnpj.Text = "CNPJ";
                tbEmployerCNPJ.MaxLength = 14;
            }
            else
            {
                lCpfCnpj.Text = "CPF";
                tbEmployerCNPJ.MaxLength = 11;
                if (tbEmployerCNPJ.Text.Length > 11)
                {
                    tbEmployerCNPJ.Text = tbEmployerCNPJ.Text.Substring(0, 11);
                }
            }
        }

        private void bSetMultiplier_Click(object sender, EventArgs e)
        {
            int amount = (int)nudMultiplier.Value;
            BtnClick(bMtGetStatus, delegate()
            {
                repProtocol.SetMultiplier(amount);
            });
        }

        private void ipaddr_TextChanged(object sender, EventArgs e)
        {
            repProtocol.SetHost(ipaddr.Text);
        }

        private void bCancelOperation_Click(object sender, EventArgs e)
        {
            bCancelOperation.Enabled = false;
            repProtocol.Cancel();
        }

        private void bEmployeeExport_Click(object sender, EventArgs e)
        {
            BtnClick(bEmployeeExport, delegate()
            {
                List<RepProtocol.Empregado> l = repProtocol.GetAllEmployee(delegate(int cur, int tot)
                {
                    SetProgress(cur, tot);
                });
                if (l.Count == 0)
                {
                    Invoke(delegate()
                    {
                        MessageBox.Show("Nenhum empregado cadastrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    });
                }
                else
                {
                    Invoke(delegate()
                    {
                        bool retry;
                        do
                        {
                            retry = false;
                            if (sfdExportEmployee.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                try
                                {
                                    StringBuilder txtOut = new StringBuilder();
                                    foreach (RepProtocol.Empregado emp in l)
                                    {
                                        txtOut.AppendLine(emp.Nome + "," + emp.PIS + "," + emp.ID + "," + emp.KBD + "," + emp.CNTLS + ";");
                                    }
                                    System.IO.File.WriteAllText(sfdExportEmployee.FileName, txtOut.ToString(), Program.Encoding);
                                    MessageBox.Show("Arquivo com " + l.Count + " empregados exportado com sucesso.", "Exportação bem sucedida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    retry = false;
                                }
                                catch (Exception ex)
                                {
                                    if (MessageBox.Show("Não foi possível salvar o arquivo: " + ex.Message, "Erro", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                                    {
                                        retry = true;
                                    }
                                }
                            }
                        } while (retry);
                    });

                }
            });
        }

        private void bNetworkConfigGet_Click(object sender, EventArgs e)
        {
            BtnClick(bNetworkConfigGet, delegate ()
            {
                RepProtocol.NetworkConfig config = repProtocol.GetNetworkConfig();
                Invoke(() =>
                {
                    tbNetConfigIP.Text = config.ip.ToString();
                    tbNetConfigMask.Text = config.mask.ToString();
                    tbNetConfigGateway.Text = config.gateway.ToString();
                    tbNetConfigMAC.Text = Regex.Replace(config.mac.ToString(), "(.{2})(?!$)", m => m.Groups[1].Value + "-");
                });
            });
        }

        private void bNetworkConfigSet_Click(object sender, EventArgs e)
        {
            BtnClick(bNetworkConfigSet, () =>
            {
                RepProtocol.NetworkConfig config = new RepProtocol.NetworkConfig();
                    
                config.ip = IPAddress.Parse(Utils.FixRepIpAddressString(tbNetConfigIP.Text));
                config.mask = IPAddress.Parse(Utils.FixRepIpAddressString(tbNetConfigMask.Text));
                config.gateway = IPAddress.Parse(Utils.FixRepIpAddressString(tbNetConfigGateway.Text));
                repProtocol.SetNetworkConfig(config);
            });
        }

        private void bNetworkInterfaceGet_Click(object sender, EventArgs e)
        {
            BtnClick(bNetworkInterfaceGet, () =>
            {
                int iface = repProtocol.GetNetworkInterface();
                Invoke(() =>
                {
                    if (iface < 0 || iface > cbNetInterface.Items.Count)
                    {
                        throw new Exception("Interface inválida");
                    }
                    cbNetInterface.SelectedIndex = iface;
                });
            });
        }

        private void bNetworkInterfaceSet_Click(object sender, EventArgs e)
        {
            
            BtnClick(bNetworkInterfaceSet, () =>
            {
                int iface = -1;

                Invoke(() =>
                {
                    iface = cbNetInterface.SelectedIndex;
                    if (cbNetInterface.SelectedIndex < 0)
                    {
                        throw new Exception("Interface inválida");
                    }
                });
                
                repProtocol.SetNetworkInterface(iface);
            });
        }

        private void bPrinterStatus_Click(object sender, EventArgs e)
        {
            lPrinterStatus.Text = "";
            BtnClick(bPrinterStatus, () =>
            {
                string[] status = new string[] { "Operando normalmente", "Impressora não responde", "Pouco papel na impressora", "Impressora sem papel", "Tampa da impressora aberta", "Guilhotina desabilitada" };
                int idx = repProtocol.GetPrinterStatus();
                if (idx < 0 || idx >= status.Length)
                {
                    throw new Exception("Status inválido");
                }
                Invoke(() =>
                {
                    lPrinterStatus.Text = status[idx];
                });
            });
        }

        private void bBatteryStatus_Click(object sender, EventArgs e)
        {
            lBatteryStatus.Text = "";
            BtnClick(bBatteryStatus, () =>
            {
                string[] status = new string[] { "Carregando", "Descarregando", "Carregada", "Carregador desligado", "Erro na bateria" };
                int idx = -1, percent = -1;
                repProtocol.GetBatteryInfo(ref idx, ref percent);
                if (idx < 0 || idx >= status.Length)
                {
                    throw new Exception("Status inválido");
                }
                Invoke(() =>
                {
                    lBatteryStatus.Text = status[idx];
                    if (idx != 4)
                    {
                        lBatteryStatus.Text += ", " + percent + "% de carga";
                    }
                    
                });
            });
        }

        private void bWifiConfigGet_Click(object sender, EventArgs e)
        {
            BtnClick(bWifiConfigGet, () =>
            {
                RepProtocol.WifiConfig config = repProtocol.GetWifiConfig();
                Invoke(() =>
                {
                    int idx = cbWifiSecurity.Items.IndexOf(config.Security);
                    if (idx < 0)
                    {
                        throw new Exception("Configuração inválida");
                    }
                    tbWifiKey.Text = config.Key;
                    cbWifiSecurity.SelectedIndex = idx;
                    tbWifiSSID.Text = config.SSID;
                });
            });
        }

        private void bWifiConfigSet_Click(object sender, EventArgs e)
        {
            RepProtocol.WifiConfig config = new RepProtocol.WifiConfig();
            config.Key = tbWifiKey.Text;
            config.Security = cbWifiSecurity.Text;
            config.SSID = tbWifiSSID.Text;
            BtnClick(bWifiConfigSet, () =>
            {
                repProtocol.SetWifiConfig(config);
            });
        }
    }

}
