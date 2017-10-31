using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Device
{
    public partial class DeviceMain : Form
    {
        public DeviceMain()
        {
            InitializeComponent();
        }

        //Cria um instância da classe utilizada no Bio REP 100.
        public zkemkeeper.CZKEMClass bioRep = new zkemkeeper.CZKEMClass();

        //Mostra a descrição dos comandos e as respostas do equipamento
        public void atualizaResposta(string com, string desc, string resposta)
        {
            comando.Text = com;
            descricao.Text = desc;
            resp.Text = resposta;
        }


#region Comunicação
        private bool bIsConnected = false;//identifica se o Bio Rep está conectado
        private int numeroRep = 1;//número serial, após conectado será modificado.
        private int erroCod = 0; //número do erro


        //botão conectar
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (txtIP.Text.Trim() == "" || txtPort.Text.Trim() == "")
            {
                MessageBox.Show("IP ou Porta em branco", "Erro");
                return;
            }
            conectarTcp();
        }

        //Conexão com o Bio REP
        private void conectarTcp()
        {
            Cursor = Cursors.WaitCursor;
            if (btnConnect.Text == "Desconectar")
            {
                //Desconecta o Bio REP 100
                bioRep.Disconnect();
                atualizaResposta("Disconnect()", "Desconecta o Bio REP 100", "Não possui retorno");
                bIsConnected = false;
                btnConnect.Text = "Conectar";
                lblState.Text = "Status: Desconectado";
                lblState.ForeColor = Color.Red;
                Cursor = Cursors.Default;
                return;               
            }
            //Efetua conexão com o Bio REP 100
            bIsConnected = bioRep.Connect_Net(txtIP.Text, Convert.ToInt32(txtPort.Text));

            if (bIsConnected == true)
            {
                atualizaResposta("Connect_Net(endIp, porta)", "Efetua conexão com o Bio REP 100", "True");
                btnConnect.Text = "Desconectar";
                btnConnect.Refresh(); 
                lblState.Text = "Status: Conectado";
                lblState.ForeColor = Color.Green;
                numeroRep = 1;//Não utilizado para TCP/IP.
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Não foi possível conectar ao BIO REP 100 \n Erro número = " + erroCod.ToString(), "Error");
            }
            Cursor = Cursors.Default;

        }

        #endregion

#region Atualizar data e hora

        //Botão enviar data e hora do windows.
        private void btnSetDeviceTime_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Conecte o Bio REP", "Erro");
                return;
            }
            enviarDataHoraWindows();
        }

        //Enviar data hora do Windows
        private void enviarDataHoraWindows()
        {
            int codErro = 0;
            Cursor = Cursors.WaitCursor;
            //Commando para enviar data hora do windows
            if (bioRep.SetDeviceTime(numeroRep))
            {
                atualizaResposta("SetDeviceTime(numeroRep)", "Envia data hora do Windows", "True");
                receberDataHora();
            }
            else
            {
                bioRep.GetLastError(ref codErro);
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }
        //Botão enviar data hora ajustada. 
        private void btnSetDeviceTime2_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
                return;
            }

            enviarDataHora();
        }
        //Enviar data e hora
        private void enviarDataHora()
        {
            try
            {
                int ano = Convert.ToInt32(data.Text.Substring(6, 4));
                int mes = Convert.ToInt32(data.Text.Substring(3, 2));
                int dia = Convert.ToInt32(data.Text.Substring(0, 2));
                int hora = Convert.ToInt32(horario.Text.Substring(0, 2));
                int min = Convert.ToInt32(horario.Text.Substring(3, 2));
                int seg = Convert.ToInt32(horario.Text.Substring(6, 2));

                Cursor = Cursors.WaitCursor;
                //Comando para ajustar data e hora passando parametros
                if (bioRep.SetDeviceTime2(numeroRep, ano, mes, dia, hora, min, seg))
                {
                    atualizaResposta("SetDeviceTime2(numeroRep, ano, mes, dia, hora, min, seg)", "Atualiza data e hora do Bio REP 100", "True");
                    bioRep.RefreshData(numeroRep);//atualiza informações
                    //receberDataHora();
                }
                else
                {
                    bioRep.GetLastError(ref erroCod);
                    MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
                }
                Cursor = Cursors.Default;
            }
            catch
            {
                MessageBox.Show("Data ou hora inválida", "Aviso");
            }
        }
        //Botao pegar hora do REP.
        private void btnGetDeviceTime_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Conecte o Bio REP", "Erro");
                return;
            }
            receberDataHora();
        }
        //Receber data e hora do Sistem
        private void receberDataHora()
        {
            int ano = 0;
            int mes = 0;
            int dia = 0;
            int hora = 0;
            int min = 0;
            int seg = 0;

            Cursor = Cursors.WaitCursor;
            //Comando para receber data e hora
            if (bioRep.GetDeviceTime(numeroRep, ref ano, ref mes, ref dia, ref hora, ref min, ref seg))
            {
                data.Text = Convert.ToDateTime(dia.ToString() + "/" + mes.ToString() + "/" + ano.ToString()).ToString("dd/MM/yyyy");
                horario.Text = Convert.ToDateTime(hora.ToString() + ":" + min.ToString() + ":" + seg.ToString()).ToString("hh/mm/ss");
                atualizaResposta("GetDeviceTime2(numeroRep, ano, mes, dia, hora, min, seg)", "Recebe data e hora do Bio REP 100", data.Text +" "+horario.Text);
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }
        //Botão pegar data hora do windows
        private void button3_Click(object sender, EventArgs e)
        {
            data.Text = DateTime.Now.ToString("dd/MM/yyyy");
            horario.Text = DateTime.Now.ToString("hh/mm/ss");
        }

#endregion

#region Receber diversos
        //Recebe o numero de série do Bio REP
        private void btnGetSerialNumber_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Conecte o Bio Rep 100", "Erro");
                return;
            }

            string numSerie = "";

            Cursor = Cursors.WaitCursor;
            if (bioRep.GetSerialNumber(numeroRep,out numSerie))
            {
                atualizaResposta("GetSerialNumber(numeroRep,out numSerie)", "Recebe o numero de série do Bio REP", numSerie);
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }

        //Receber o modelo do equipamento
        private void btnGetProductCode_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Conecte o Bio Rep 100", "Erro");
                return;
            }
               
            string modelo = "";

            Cursor = Cursors.WaitCursor;
            if (bioRep.GetProductCode(numeroRep,out modelo))
            {
                atualizaResposta("GetProductCode(numeroRep,out modelo)", "Receber o modelo do equipamento", modelo);
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }

        //Receber a versão do firmware do Bio REP 100
        private void btnGetFirmwareVersion_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Conecte o Bio Rep 100", "Erro");
                return;
            }

            string sVersion= "";

            Cursor = Cursors.WaitCursor;
            if (bioRep.GetFirmwareVersion(numeroRep,ref sVersion))
            {
                atualizaResposta("GetFirmwareVersion(numeroRep,ref sVersion)", "Receber a versão do firmware do Bio REP 100", sVersion);
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }

        //Receber a versão da SDK do Bio REP 100
        private void btnGetSDKVersion_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Conecte o Bio Rep 100", "Erro");
                return;
            }

            string versao = "";

            Cursor = Cursors.WaitCursor;
            if (bioRep.GetSDKVersion(ref versao))
            {
                atualizaResposta("GetSDKVersion(ref sVersion)", "Receber a versão da SDK do Bio REP 100",versao);
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }

        //Receber o endereço de IP do Bio REP 100
        private void btnGetDeviceIP_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Conecte o Bio Rep 100", "Erro");
                return;
            }
            
            string endIP = "";

            Cursor = Cursors.WaitCursor;
            if (bioRep.GetDeviceIP(numeroRep,ref endIP))
            {
                atualizaResposta("GetDeviceIP(numeroRep,ref endIP", "Receber o endereço de IP do Bio REP 100", endIP);
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }

        //Receber o MAC Address do Bio Rep 100
        private void btnGetDeviceMAC_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Conecte o Bio Rep 100", "Erro");
                return;
            }
            
            string macAdress = "";

            Cursor = Cursors.WaitCursor;
            if (bioRep.GetDeviceMAC(numeroRep,ref macAdress))
            {
                atualizaResposta("GetDeviceMAC(numeroRep,ref macAdress)", "Receber o MAC Address do Bio Rep 100", macAdress);
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }

        //Receber o nome do fabricante
        private void btnGetVendor_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Conecte o Bio Rep 100", "Erro");
                return;
            }

            string fabricante="";

            Cursor = Cursors.WaitCursor;
            if (bioRep.GetVendor(ref fabricante))
            {
                atualizaResposta("GetVendor(ref fabricante)", "Receber o nome do fabricante", fabricante);
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }

        //Receber a plataforma de desenvolvimento do Bio REP 100
        private void btnGetPlatform_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Conecte o Bio Rep 100", "Erro");
                return;
            }

            string plataforma = "";

            Cursor = Cursors.WaitCursor;
            if (bioRep.GetPlatform(numeroRep,ref plataforma))
            {
                atualizaResposta("GetPlatform(numeroRep,ref plataforma)", "Receber a plataforma de desenvolvimento do Bio REP 100", plataforma);
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }

#endregion

#region Enviar Diversos
        //Enviar um novo número de IP para o bio REP 100
        private void btnSetDeviceIP_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Conecte o Bio Rep 100", "Erro");
                return;
            }

            if (txtSet.Text.Trim() == "")
            {
                MessageBox.Show("Insira um número de IP válido", "Error");
                return;
            }
            
            string endIP =txtSet.Text.Trim();

            Cursor = Cursors.WaitCursor;
            if (bioRep.SetDeviceIP(numeroRep,endIP))
            {
                bioRep.RefreshData(numeroRep);//Reinicia o REP
                MessageBox.Show("Endereço de IP ajustado! IP=" + endIP, "Successo");
                atualizaResposta("axCZKEM1.SetDeviceIP(numeroRep,endIP)","Envia um novo número de IP para o bio REP 100",endIP);
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }

        //Envia um novo MAC Adress para o bio REP 100.
        private void btnSetDeviceMAC_Click(object sender, EventArgs e)
        {
            if (bIsConnected == false)
            {
                MessageBox.Show("Conecte o Bio Rep 100", "Erro");
                return;
            }

            if (txtSet.Text.Trim() == "")
            {
                MessageBox.Show("Insira um MAC Adress válido", "Erro");
                return;
            }
            
            string mac = txtSet.Text.Trim();

            Cursor = Cursors.WaitCursor;
            if (bioRep.SetDeviceMAC(numeroRep,mac))
            {
                bioRep.RefreshData(numeroRep);//reinicia o REP
                atualizaResposta("Envia um novo MAC Adress para o bio REP 100","SetDeviceMAC(numeroRep,mac)",mac);
                MessageBox.Show("MAC Adress ajustado! MAC=" + mac, "Successo");
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Erro na operação, Código do erro: " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }

#endregion

#region Informações da Empresa

        //Botão receber dados da empresa
        private void button1_Click(object sender, EventArgs e)
        {
            if (bIsConnected)
            {
                receberEmpresa();
            }
            else
                MessageBox.Show("Conecte o Bio REP", "Aviso");
        }
        //Recebendo dados da empresa
        private void receberEmpresa()
        { 
            string empR = "";
            string tipoR = "";
            string cnpjR = "";
            string ceiR = "";
            string endR = "";
            bool resultado =  bioRep.GetEmployer(numeroRep, out empR,out tipoR, out cnpjR,out ceiR, out endR);
            if (resultado)
            {
                empresa.Text = empR;
                cnpj.Text = cnpjR;
                cei.Text = ceiR;
                endereco.Text = endR;
                atualizaResposta("GetEmployer(numeroRep, out empR,out tipoR, out cnpjR,out ceiR, out endR)", "Recebe os dados da Empresa cadastrado no Bio REP 100",empR + " " + tipoR + " " + cnpjR + " " + ceiR + " " + endR);
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Não foi possível receber os dados da Empresa\n Erro número = " + erroCod.ToString(), "Error");
            }
 
        }
        //Botão Enviar Empresa
        private void button2_Click(object sender, EventArgs e)
        {
            if (bIsConnected)
                enviarEmpresa();
            else
                MessageBox.Show("Conecte o Bio REP", "Aviso");
        }
        //Enviar uma Empresa
        private void enviarEmpresa()
        {
            bool resultado = bioRep.SetEmployer(numeroRep, empresa.Text, 0, cnpj.Text, cei.Text, endereco.Text);
            if (resultado)
            {
                atualizaResposta("SetEmployer(numeroRep, empresa, 0, cnpj, cei, endereco)", "Enviar dados de uma Empresa para o Bio REP 100", "True");
                MessageBox.Show("Empresa Enviada", "Aviso");
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Não foi possível enviar os dados da Empresa\n Erro número = " + erroCod.ToString(), "Error");
            }
   
        }
#endregion

#region Funcionários

        //Botão receber funcionario
        private void button4_Click(object sender, EventArgs e)
        {
            if (bIsConnected)
                receberTodosFuncionarios();
            else
                MessageBox.Show("Conecte o Bio REP", "Aviso");
        }
        //Receber todos funcionários
        private void receberTodosFuncionarios()
        {
            string mat = "3"; 
            string nome = ""; 
            string senha = "";
            string pis = "";
            string cpf = "";
            int prev = 0;

            bioRep.RefreshData(numeroRep);
            bool ativado = false;
            Cursor = Cursors.WaitCursor;
            funcionarios.Rows.Clear();
            //Comando para receber todas as informações dos usuários/funcionários
            while (bioRep.SSR_GetAllUserInfoEx(numeroRep, out mat, out nome, out senha, out prev, out ativado, out pis, out cpf))
            {
                string teste = Convert.ToString(ativado);
                funcionarios.Rows.Add(mat, nome, pis, cpf, prev, senha, teste );
                atualizaResposta("SSR_GetAllUserInfoEx(numeroRep, out mat, out nome, out senha, out prev, out ativado, out pis, out cpf)", "Recebe todos funcionários cadastrados no Bio REP 100", mat + " " + nome + " " + senha + " " + prev + " " + ativado + " " + pis + " " + cpf);
            }
           
            Cursor = Cursors.Default;
        }
        //Evento selecionar um funcionário
        private void funcionarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            matricula.Text = funcionarios.CurrentRow.Cells[0].Value.ToString();
            funcionario.Text = funcionarios.CurrentRow.Cells[1].Value.ToString();
            pisF.Text = funcionarios.CurrentRow.Cells[2].Value.ToString();
            cpfF.Text = funcionarios.CurrentRow.Cells[3].Value.ToString();
            previlegioF.Text = funcionarios.CurrentRow.Cells[4].Value.ToString();
            senhaF.Text = funcionarios.CurrentRow.Cells[5].Value.ToString();
            ativadoF.Checked = Convert.ToBoolean(funcionarios.CurrentRow.Cells[6].Value.ToString());
        }
        //Botão receber Funcionário
        private void button5_Click(object sender, EventArgs e)
        {
            if (bIsConnected)
                if (matricula.Text != "")
                    receberFuncionario();
                else
                    MessageBox.Show("Matricula inválida", "Aviso");
            else
                MessageBox.Show("Conecte o Bio REP", "Aviso");

        }
        //Receber um funcionário
        private void receberFuncionario()
        {
            string mat = "";
            string nome = "";
            string senha = "";
            string pis = "";
            string cpf = "";
            int prev = 0;

            funcionario.Text = "";
            pisF.Text = "";
            cpfF.Text = "";
            previlegioF.Text = "";
            senhaF.Text = "";
            ativadoF.Checked = false;

            bool ativado = false;
            Cursor = Cursors.WaitCursor;

            //Comando para receber as informações de um usuário/funcionário necessário informar a matricula
            bool resultado = bioRep.SSR_GetUserInfo(numeroRep, matricula.Text, out nome, out senha, out prev, out ativado);
            if (resultado)
            {
                atualizaResposta("SSR_GetUserInfo(numeroRep, matr.Text, out nome, out senha, out prev, out ativado)", "Recebe todos funcionários cadastrados no Bio REP 100", nome + " " + senha + " " + prev + " " + ativado);
                funcionario.Text = nome;
                pisF.Text = pis;
                cpfF.Text = cpf;
                previlegioF.Text = prev.ToString();
                senhaF.Text = senha;
                ativadoF.Checked = ativado;
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Não foi possível receber o funcionário.\nErro número = " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }
        //Botão Enviar Funcionário
        private void button6_Click(object sender, EventArgs e)
        {
            if (bIsConnected)
                enviarFuncionario();
            else
                MessageBox.Show("Conecte o Bio REP", "Aviso");
        }
        //Enviar um funcionário
        private void enviarFuncionario()
        {
            string mat = matricula.Text;
            string nome = funcionario.Text;
            string senha = senhaF.Text;
            string pis = pisF.Text;
            string cpf = cpfF.Text;
            int prev = Convert.ToUInt16(previlegioF.Text);
            bool ativado = ativadoF.Checked;

            Cursor = Cursors.WaitCursor;
            //Comando para enviar as informações de um usuário/funcionário
            bool resultado = bioRep.SSR_SetUserInfoEx(numeroRep,mat,nome,senha,prev,ativado,pis,cpf);
            if (resultado)
            {
                atualizaResposta("SSR_SetUserInfoEx(numeroRep,mat,nome,senha,prev,ativado,pis,cpf)", "Envia informações do funcionário para o Bio REP 100", "True");
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Não foi enviar o funcionário.\nErro número = " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;

        }
        //Botão excluir funcionário
        private void button7_Click(object sender, EventArgs e)
        {
            if (bIsConnected)
                excluirFuncionario();
            else
                MessageBox.Show("Conecte o Bio REP", "Aviso");
        }
        //Excluir funcionário
        private void excluirFuncionario()
        {
            string mat = matricula.Text;
            Cursor = Cursors.WaitCursor;
            //Comando para excluir um funcionário
            bool resultado = bioRep.SSR_DeleteEnrollDataExt_BZ400(numeroRep, mat, 0);
            if (resultado)
            {
                MessageBox.Show("Funcionário matricula: " + mat + " excluido com sucesso", "Aviso");
                atualizaResposta("SSR_DeleteEnrollDataExt_BZ400(numeroRep, mat, 0)", "Exclui um funcionário cadastrado no REP", "True");
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Não foi enviar o funcionário.\nErro número = " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;

        }

 #endregion

#region Biometria
        //Botão receber biometria
        private void button9_Click(object sender, EventArgs e)
        {
            if (bIsConnected)
                receberBiometria();
            else
                MessageBox.Show("Conecte o Bio REP", "Aviso");
        }
        //Recebe uma biometria de um funcionário
        private void receberBiometria()
        {
            string mat = matr.Text;
            int numBio = Convert.ToInt32(bio.Text);
            int flag = 0;
            string biometria = "";
            int tamBio;
            Cursor = Cursors.WaitCursor;

            bool resultado = bioRep.GetUserTmpExStr_BZ400(numeroRep, mat, numBio, out flag, out biometria, out tamBio);
            if (resultado)
            {
                atualizaResposta("GetUserTmpExStr_BZ400(numeroRep, mat, numBio, out flag, out biometria, out tamBio)", "Recebe uma biometria de um funcionário cadastrado", flag +" "+ biometria +" " +tamBio);
                saidaBio.Clear();
                saidaBio.Text = biometria;
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Não foi possível receber a biometria.\nErro número = " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
            
        }
        //Enviar Biometria
        private void button8_Click(object sender, EventArgs e)
        {
            if (bIsConnected)
                enviarBiometria();
            else
                MessageBox.Show("Conecte o Bio REP", "Aviso");
        }

        //Envia biometria de um funcionário
        private void enviarBiometria()
        {
            string mat = matr.Text;
            int numBio = Convert.ToInt32(bio.Text);
            int flag = 0;
            string biometria = saidaBio.Text;
            Cursor = Cursors.WaitCursor;

            bool resultado = bioRep.SetUserTmpExStr_BZ400(numeroRep,mat,numBio,flag,biometria);          
            
            if (resultado)
            {
                atualizaResposta("bioRep.SetUserTmpExStr_BZ400(numeroRep,mat,numBio,flag,biometria)", "Envia uma biometria para o Bio REP 100", "True");

                saidaBio.Clear();
                saidaBio.Text = biometria;
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Não foi possível enviar a biometria.\nErro número = " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;

        }
        #endregion

#region Marcações
        //Botão receber Marcações
        private void receberMar_Click(object sender, EventArgs e)
        {
            if (bIsConnected)
                receberMarcas();
            else
                MessageBox.Show("Conecte o Bio REP", "Aviso");
            
        }
        //Receber marcações do Bio REP
        private void receberMarcas()
        {
            string nsr = "200";
            int ano;
            int dia;
            int mes;
            int hor;
            int min;
            int seg;
            string pis;
            int cont = 1;
            marcas.Rows.Clear();
            Cursor = Cursors.WaitCursor;

            //Ajustar a posição no banco do Bio REP 100
            if (bioRep.SetSeekPosition(numeroRep, Convert.ToInt32(ultimoNsr.Text)))
            {
                atualizaResposta("SetSeekPosition(numeroRep, nsr)", "Ajusta a posição no banco do Bio REP 100", "True");

                //Receber as marcações
                while (bioRep.GetAttLogs(numeroRep, out nsr, out pis, out ano, out mes, out dia, out hor, out min, out seg))
                {
                    marcas.Rows.Add(cont.ToString(), pis, nsr, dia.ToString() + "/" + mes.ToString() + "/" + ano.ToString(), hor.ToString() + ":" + min.ToString() + ":" + seg.ToString());
                    marcas.Refresh();
                    cont++;
                }
                string msg = (pis+" "+ nsr+" "+ dia.ToString() + "/" + mes.ToString() + "/" + ano.ToString()+" "+  hor.ToString() + ":" + min.ToString() + ":" + seg.ToString());
                atualizaResposta("GetAttLogs(numeroRep, out nsr, out pis, out ano, out mes, out dia, out hor, out min, out seg)", "Recebe as marcações", "msg");
                MessageBox.Show((cont - 1).ToString() + " marcações recebidas", "Sucesso");
            }
            else 
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Não foi possível enviar a biometria.\nErro número = " + erroCod.ToString(), "Erro");
            }
            Cursor = Cursors.Default;
        }
        private void apagar_Click(object sender, EventArgs e)
        {
            apagarBiometria();
        }

        private void apagarBiometria()
        {
            string mat = matr.Text;
            int numBio = Convert.ToInt32(bio.Text);
            string biometria = saidaBio.Text;

            bool resultado = bioRep.SSR_DelUserTmpExt(numeroRep, mat, numBio);
            if (resultado)
            {
                MessageBox.Show("Apagado", "Aviso");
            }
            else
            {
                bioRep.GetLastError(ref erroCod);
                MessageBox.Show("Não foi possível enviar a biometria.\nErro número = " + erroCod.ToString(), "Erro");
            }
        }
#endregion

    }
} 