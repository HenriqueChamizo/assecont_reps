/*****************************************************************************
* Aplicativo : 	???????	                                                     *
* Autor	     : Ana Lúcia S. Melo (para esta classe)    	     			     *
* Referência : Programa para coleta do REP                                   *
* Data       : 27/04/11						                                 *
* Ambiente   : Visual C# 2008                    						     *
* Fonte      : Form1                   		  	                             *
* Objetivo   : Responsável pela iteração usuario/sistema.                    *
******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using System.Threading;
using System.Windows;
using System.Reflection;


namespace REP_Prototipo
{
    public unsafe partial class Form1 : Form
    {
        public string REPConectado;
        public ArrayList stringList = new ArrayList();

        txtFiles TxtFiles = new txtFiles();
        TrataPwd tratapwd = new TrataPwd();
        clTrataREP trataRep = new clTrataREP();
        clRetornaErro retornoErro = new clRetornaErro();
        clEnvioDll envioDll = new clEnvioDll();
        public string usuarioAtual;
        public string status;
        public string textoAtual;
        public byte continuaTrd;

        public string nomeIp;
        public string pathArquivo;

        public Form1()
        {
            InitializeComponent();
        }

        //******************************************************************************/
        // private: button1                                                            *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 28/04/2011
        // Descrição : Conecta ao programa
        // Retorno: void
        //
        //****************************************************************************** 
        private void button1_Click(object sender, EventArgs e)
        {

            //Verifica se a senha está correta
            string[] retorno = tratapwd.conectaUserPass(textBox1.Text, textBox2.Text).Split(';');
            //Se for uma senha de TI habilita somente a página de TI, sendo que exite o login administrador e o 
            //login usuário
            if (retorno[3] == "TI")
            {
                //Preenche os campos com o nome dos REPs cadastrados
                configuraTelasTI(retorno[2], "");
                usuarioAtual = textBox1.Text;
                //Informa que está logado
                MessageBox.Show("Logado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Atualiza as paginas com as informações para usuario de TI
                txtTIAba4Login.Text = textBox1.Text;
                btnTIAba5IrParaRH.Text = "Sair";
                panel2.BringToFront();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            //Se for uma senha de RH habilita somente a página de RH, sendo que exite o login administrador e o 
            //login usuário
            else if (retorno[3] == "RH")
            {
                //Segue o mesmo padrão de TI
                usuarioAtual = textBox1.Text;
                configuraTelasRH(retorno[2], "");
                MessageBox.Show("Logado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRHAba5Login.Text = textBox1.Text;
                btnRHAba6IrParaTI.Text = "Sair";
                panel3.BringToFront();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            //Caso seja o usuario master habilita todas as abas 
            else if (retorno[3] == "master")
            {
                usuarioAtual = textBox1.Text;
                configuraTelasTI(retorno[2], "master");
                configuraTelasRH(retorno[2], "master");
                MessageBox.Show("Logado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnRHAba6IrParaTI.Text = "Ir para TI";
                btnTIAba5IrParaRH.Text = "Ir para RH";
                panel3.BringToFront();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
                MessageBox.Show("Login e Senha INVÁLIDOS!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //******************************************************************************/
        // private: btnRHAba2Cadastrar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 28/04/2011
        // Descrição : Cadastra os funcionários
        // Retorno: void
        //
        //****************************************************************************** 
        private void btnRHAba2Cadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = 0;
                string path = Application.StartupPath + "\\REPCad.txt";

                //  byte[] NomeFuncionario = new byte[52];
                string Nome;
                string NPis;
                string Contactless;
                string CodBarras;
                string Teclado;
                string Biometria = "0";
                byte grupo = 0;

                //Recebe o IP cadastrado no banco de dados                
                string ip = trataRep.consultaTxt(path, 2, cbRHAba2REP.Text, 0);
                //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                               
                //Verifica se o campo referente ao nome está preenchido
                if (txtRHAba2Nome.Text == "")
                {
                    MessageBox.Show("Favor preencher com o nome do funcionário.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRHAba2Nome.Focus();
                    return;
                }
                //Verifica se o Pis é válido
                if (Validacao.ValidaPis(txtRHAba2PIS.Text) == false)
                {
                    MessageBox.Show("O numero do PIS está incorreto.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRHAba2PIS.Focus();
                    return;
                }

                System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
                // Convert to ISO-8859-1 bytes.                
                //Nome do funcionario
                //NomeFuncionario = iso_8859_1.GetBytes(txtRHAba2Nome.Text);
                Nome = txtRHAba2Nome.Text + '\0';
                //PIS
                NPis = txtRHAba2PIS.Text + '\0';
                //Contactless
                if (txtRHAba2RFID.Text != "")
                    Contactless = (txtRHAba2RFID.Text + '\0');
                else 
                    Contactless = "0" + '\0';
                //Codigo de barras
                if (txtRHAba2CodigoBarras.Text != "")
                    CodBarras = (txtRHAba2CodigoBarras.Text + '\0');
                else
                    CodBarras = "0" + '\0';
                //Teclado
                if (txtRHAba2Teclado.Text != "")
                    Teclado = (txtRHAba2Teclado.Text + '\0');
                else
                    //NTeclado = iso_8859_1.GetBytes("0");
                    Teclado = "0" + '\0';
                //Biometria
                if (Biometria != "")
                    //NBiometria = iso_8859_1.GetBytes
                    Biometria = "0" + '\0';
                //Grupo
                if (txtRHAba2Grupo.Text == "") txtRHAba2Grupo.Text = "0";
                else
                    if (int.Parse(txtRHAba2Grupo.Text) > 256)
                    {
                        MessageBox.Show("Valor do grupo deve ser inferior a 256");
                        txtRHAba2Grupo.Focus();
                        return;
                    }
                grupo = Convert.ToByte(txtRHAba2Grupo.Text);

                //Envia o cadastro para o equipamento                    
                resultado = envioDll.ConectaREP(ip, 1);

                if (resultado != 0)
                {
                    dllREP.REP_Conexao_Simples(ip, 0, ref resultado);
                    MessageBox.Show("Não foi possivel conectar ao " + cbRHAba2REP.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    dllREP.REP_GravaCadastroFuncionario
                       (ip,
                        '0',
                        Nome,
                        NPis,
                        Contactless,
                        CodBarras,
                        Teclado,
                        Biometria,
                        grupo,
                        ref resultado);

                    //Se não ocorreu erro mostra a mensagem de cadastrado e limpa os campos
                    if (resultado == 0)
                    {
                        txtRHAba2Nome.Text = "";
                        txtRHAba2PIS.Text = "";
                        txtRHAba2Grupo.Text = "";
                        txtRHAba2CodigoBarras.Text = "";
                        txtRHAba2Teclado.Text = "";
                        txtRHAba2RFID.Text = "";
                        resultado = envioDll.ConectaREP(ip, 0);

                        MessageBox.Show("Funcionário cadastrado com sucesso.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRHAba2Nome.Focus();
                    }
                    //Se ocorreu falha mostra a mensagem e erro
                    else
                    {
                        envioDll.ConectaREP(ip, 0);
                        // MessageBox.Show(retornoErro.retornoREP(resultado));
                        MessageBox.Show("Falha ao cadastrar funcionário. " + retornoErro.retornoREP(resultado), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                }
            }
            catch
            {
                MessageBox.Show("Falha ao cadastrar funcionário.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        //******************************************************************************/
        // private: btnTIAba1Cadastrar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 28/04/2011
        // Descrição : Cadastra os dados do equipamentos para acessá-lo
        // Retorno: void
        //
        //****************************************************************************** 
        private void btnTIAba1Cadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                //Valida o IP
                if (Validacao.ValidaIp(txtTIAba1IPREP.Text) == false)
                {
                    MessageBox.Show("Ip Inválido!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtTIAba1IPREP.Focus();
                    return;
                }
                //Valida a mascara de rede
                if (Validacao.ValidaIp(txtTIAba1MascaraRede.Text) == false)
                {
                    MessageBox.Show("Máscara de Rede Incorreta!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtTIAba1MascaraRede.Focus();
                    return;
                }
                
                if (txtTIAba1NomeREP.Text == "")
                {
                    MessageBox.Show("Informe a nome do REP.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtTIAba1NomeREP.Focus();
                    return;
                }

                for (int i = 0; i < cbTIAba1REPsCadastrados.Items.Count; i++)
                    if (txtTIAba1NomeREP.Text == cbTIAba1REPsCadastrados.Items[i].ToString())
                    {
                        MessageBox.Show("Nome do REP já existe.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtTIAba1NomeREP.Focus();
                        return;
                    }

                
                int retornoExistencia = trataRep.adicionaREP(txtTIAba1IPREP.Text, txtTIAba1MascaraRede.Text, txtTIAba1NomeREP.Text);
                //Salva em um arquivo
                if (retornoExistencia == 0)
                {
                    cbTIAba1REPsCadastrados.Items.Add(txtTIAba1NomeREP.Text);
                    cbTIAba2REP.Items.Add(txtTIAba1NomeREP.Text);
                    cbRHAba1NomeREP1.Items.Add(txtTIAba1NomeREP.Text);
                    cbRHAba3REP.Items.Add(txtTIAba1NomeREP.Text);
                    cbRHAba2REP.Items.Add(txtTIAba1NomeREP.Text);
                    cbRHAba2REP2.Items.Add(txtTIAba1NomeREP.Text);
                    cbRHAba2REP3.Items.Add(txtTIAba1NomeREP.Text);
                    cbTIAba3REPsCadastrados.Items.Add(txtTIAba1NomeREP.Text);

                    txtTIAba1IPREP.Text = "";
                    txtTIAba1MascaraRede.Text = "";
                    txtTIAba1NomeREP.Text = "";

                    MessageBox.Show("Cadastro efetuado com sucesso.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (retornoExistencia == 1)
                    MessageBox.Show("Este REP já existe.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Falha na tentativa de cadastro.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
                MessageBox.Show("Ocorreu um erro na tentativa de cadastro.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //******************************************************************************/
        // private: btnRHAba2Alterar                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 28/04/2011
        // Descrição : Alterar os dados do funcionário
        // Retorno: void
        //
        //****************************************************************************** 
        private void btnRHAba2Alterar_Click(object sender, EventArgs e)
        {
            //Alter o cadastro no equipamento
            try
            {
                int resultado = 0;
                string path = Application.StartupPath + "\\REPCad.txt";
                char acao = '1';
                string Nome;
                string NPis;
                string Contactless;
                string CodBarras;
                string Teclado;
                string Biometria = "0";
                byte grupo = 0;
                //Recebe o IP cadastrado no banco de dados                
                string ip = trataRep.consultaTxt(path, 2, cbRHAba2REP.Text, 0);
                //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                //Verifica se o campo referente ao nome está preenchido
                if (txtRHAba2Nome.Text == "")
                {
                    MessageBox.Show("Favor preencher com o nome do funcionário.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRHAba2Nome.Focus();
                    return;
                }
                //Verifica se o Pis é válido
                if (Validacao.ValidaPis(txtRHAba2PIS.Text) == false)
                {
                    MessageBox.Show("O numero do PIS está incorreto.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRHAba2PIS.Focus();
                    return;
                }

                System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
                
                Nome = txtRHAba2Nome.Text + '\0';
                //PIS
                NPis = txtRHAba2PIS.Text + '\0';
                //Contactless
                if (txtRHAba2RFID.Text != "")
                    Contactless = (txtRHAba2RFID.Text + '\0');
                else 
                    Contactless = "0" + '\0';
                //Codigo de barras
                if (txtRHAba2CodigoBarras.Text != "")
                    CodBarras = (txtRHAba2CodigoBarras.Text + '\0');
                else
                    CodBarras = "0" + '\0';
                //Teclado
                if (txtRHAba2Teclado.Text != "")
                    Teclado = (txtRHAba2Teclado.Text + '\0');
                else
                    Teclado = "0" + '\0';
                //Biometria
                if (Biometria != " ")
                    Biometria = "0" + '\0';
                //Grupo
                if (txtRHAba2Grupo.Text == "") txtRHAba2Grupo.Text = "0";
                else
                    if (int.Parse(txtRHAba2Grupo.Text) > 256)
                    {
                        MessageBox.Show("Valor do grupo deve ser inferior a 256");
                        txtRHAba2Grupo.Focus();
                        return;
                    }
                grupo = Convert.ToByte(txtRHAba2Grupo.Text);

                //Envia o cadastro para o equipamento                    
                resultado = envioDll.ConectaREP(ip, 1);

                if (resultado != 0)
                {
                    dllREP.REP_Conexao_Simples(ip, 0, ref resultado);
                    MessageBox.Show("Não foi possivel conectar ao " + cbRHAba2REP.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    dllREP.REP_GravaCadastroFuncionario
                       (ip,
                        acao,
                        Nome,
                        NPis,
                        Contactless,
                        CodBarras,
                        Teclado,
                        Biometria,
                        grupo,
                        ref resultado);

                     //Se não ocorreu erro mostra a mensagem de cadastrado e limpa os campos
                    if (resultado == 0)
                    {
                        txtRHAba2Nome.Text = "";
                        txtRHAba2PIS.Text = "";
                        txtRHAba2Grupo.Text = "";
                        txtRHAba2CodigoBarras.Text = "";
                        txtRHAba2Teclado.Text = "";
                        txtRHAba2RFID.Text = "";
                        resultado = envioDll.ConectaREP(ip, 0);
                        
                        MessageBox.Show("Funcionário alterado com sucesso.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRHAba2Nome.Focus();
                    }
                    //Se ocorreu falha mostra a mensagem e erro
                    else
                    {
                        envioDll.ConectaREP(ip, 0);
                        MessageBox.Show("Falha ao alterar funcionário. " + retornoErro.retornoREP(resultado), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Falha ao cadastrar funcionário.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //******************************************************************************/
        // private: btnRHAba1Cadastrar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 28/04/2011
        // Descrição : Cadastra os dados da empresa
        // Retorno: void
        //
        //****************************************************************************** 
        private void btnRHAba1Cadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = 0;
                string path = Application.StartupPath;

                string sCPFCNPJ;
                string sCEI ;
                string bRazaoSocial;
                string bLocal;
                char cTipo;
                System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");


                //Verifica se foi selecionado CPF ou CNPJ
                switch (cbRHAba1Identificador.SelectedIndex)
                {
                    case 0:
                        //Valida o CNPJ
                        if (Validacao.ValidaCNPJ(txtRHAba1CNPJCPF.Text) == false)
                            //Se a validação retornar false verifica se o que foi digitado é um CPF
                            if (Validacao.ValidaCPF(txtRHAba1CNPJCPF.Text) == true)
                                cbRHAba1Identificador.SelectedIndex = 1;
                            else
                            {
                                MessageBox.Show("CNPJ Inválido.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        break;

                    case 1:
                        //Valida o CPF
                        if (Validacao.ValidaCPF(txtRHAba1CNPJCPF.Text) == false)
                            //Se a validação retornar false verifica se o que foi digitado é um CNPJ
                            if (Validacao.ValidaCNPJ(txtRHAba1CNPJCPF.Text) == true)
                                cbRHAba1Identificador.SelectedIndex = 0;
                            else
                            {
                                MessageBox.Show("CPF Inválido.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        break;
                    default:
                        cbRHAba1Identificador.SelectedIndex = 0;
                        goto case 1;
                }

                //Verifica se todos os campos foram preenchidos
                if (txtRHAba1RazaoSocial.Text == "")
                {
                    MessageBox.Show("Preencha o campo referente a razão social da empresa ou nome", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRHAba1RazaoSocial.Focus();
                    return;
                }

                if (txtRHAba1Endereco.Text == "")
                {
                    MessageBox.Show("Preencha o campo referente ao Endereço", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRHAba1Endereco.Focus();
                    return;
                }
                // Recebe as informações para cadastrar               
                //CNPJ/CPF - Deverá haver 14 posições
                if (cbRHAba1Identificador.SelectedIndex == 1)
                    cTipo = '2';
                else
                    cTipo = '1';
                sCPFCNPJ = txtRHAba1CNPJCPF.Text+'\0';//auxStr;
                sCEI = txtRHAba1CEI.Text+'\0';
                bRazaoSocial = txtRHAba1RazaoSocial.Text+'\0';
                bLocal = txtRHAba1Endereco.Text + '\0';
               
                //Verifica o IP
                string ip = trataRep.consultaTxt(path + "\\REPCad.txt", 2, cbRHAba1NomeREP1.Text, 0);//"192.168.0.206"; //listIp[0].ToString();
                //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                //Conecta ao Terminal
                resultado = envioDll.ConectaREP(ip, 1);
                if (resultado != 0 & resultado != 10005)
                {
                    resultado = envioDll.ConectaREP(ip, 0);

                    MessageBox.Show("Não foi possivel conectar ao " + cbRHAba1NomeREP1.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {

                    dllREP.REP_GravaCadastroEmpregador(
                        ip,
                        '0',
                        cTipo,
                        sCPFCNPJ,
                        sCEI,
                        bRazaoSocial,
                        bLocal,
                        ref resultado);

                    if (resultado == 0)
                    {
                        txtRHAba1RazaoSocial.Text = "";
                        cbRHAba1Identificador.Text = "";
                        txtRHAba1CNPJCPF.Text = "";
                        txtRHAba1CEI.Text = "";
                        txtRHAba1Endereco.Text = "";
                        resultado = envioDll.ConectaREP(ip, 0);

                        MessageBox.Show("Cadastro efetuado com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRHAba1RazaoSocial.Focus();
                        Application.DoEvents();
                    }
                    else
                    {
                        resultado = envioDll.ConectaREP(ip, 0);
                        MessageBox.Show("Não foi possivel efetuar o cadastro.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //******************************************************************************/
        // private: btnRHAba6IrParaTI                                                  *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 28/04/2011
        // Descrição : Volta para tela inicial ou troca de tela para TI (depende do usuário)
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba6IrParaTI_Click(object sender, EventArgs e)
        {
            //Volta para tela inicial
            if (btnRHAba6IrParaTI.Text == "Sair")
            {
                if (MessageBox.Show("Deseja Sair?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    panel1.BringToFront();
                }
            }
            else
                //Troca de tela
                panel2.BringToFront();
        }

        //******************************************************************************/
        // private: btnTIAba4IrParaRH                                                  *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 28/04/2011
        // Descrição : Volta para tela inicial
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba4IrParaRH_Click(object sender, EventArgs e)
        {
            //Volta para tela inicial   
            if (btnTIAba5IrParaRH.Text == "Sair")
            {
                if (MessageBox.Show("Deseja Sair?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    panel1.BringToFront();
            }
            else
            {
                //Troca de tela
                panel3.BringToFront();
            }
        }

        //******************************************************************************/
        // private: btnRHAba2Excluir                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 28/04/2011
        // Descrição : Remove o registro do funcionarios
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba2Excluir_Click(object sender, EventArgs e)
        {
            int resultado = 0;
            byte[] dados = new byte[276];
            System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
            string path = Application.StartupPath;
            try
            {
                if (MessageBox.Show("Realmente deseja apagar este registro?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Recebe o ip do REP
                    string ip = trataRep.consultaTxt(path + "\\REPCad.txt", 2, cbRHAba2REP.Text, 0);
                    //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                    //Se conecta ao REP
                    resultado = envioDll.ConectaREP(ip, 1);
                    //Caso ocorra o resultado 10005 significa que o REP já esta conectado                   
                    if (resultado != 0 & resultado != 10005)
                    {
                        resultado = envioDll.ConectaREP(ip, 0);
                        MessageBox.Show("Não foi possivel conectar ao " + cbRHAba2REP.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        //"Exclui" o cadastro do equipamento
                        dllREP.REP_ExcluiCadastroFuncionario(ip, txtRHAba2PIS.Text, ref resultado);
                        if (resultado == 0)
                        {
                            txtRHAba2Nome.Text = "";
                            txtRHAba2PIS.Text = "";
                            txtRHAba2Grupo.Text = "";
                            txtRHAba2CodigoBarras.Text = "";
                            txtRHAba2Teclado.Text = "";
                            txtRHAba2RFID.Text = "";
                            resultado = envioDll.ConectaREP(ip, 0);

                            MessageBox.Show("Registro removido com exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            resultado = envioDll.ConectaREP(ip, 0);

                            MessageBox.Show("Falha na remoção do registro", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }

            }
            catch
            {
            }

        }

        //******************************************************************************/
        // private: btnRHAba3Coletar                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 29/04/2011
        // Descrição : Recebe arquivo contendo todos os registros do REP
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba3Coletar_Click(object sender, EventArgs e)
        {
            try
            {
                nomeIp = cbRHAba3REP.Text;
                pathArquivo = txtRHAba4SalvarEm.Text;

                lbStatus.ForeColor = Color.Black;
                ColetaRegistro();
            }
            catch
            {
            }
        }

        //******************************************************************************/
        // private: Form1                                                              *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 29/04/2011
        // Descrição : Inicializa o programa com a tela de senha
        // Retorno: void
        //
        //******************************************************************************        
        private void Form1_Load(object sender, EventArgs e)
        {
            REPConectado = "";
            panel1.BringToFront();
            
            //Especifica o nome da Pasta33 a ser criada no C:.
            string activeDir = Application.StartupPath + "\\Gertec";
            // Cria uma nova subpasta na pasta atual Gertec com o nome Biometria. 
            string newPath = System.IO.Path.Combine(activeDir, "Biometria");
          
            //Cria a subpasta.
            System.IO.Directory.CreateDirectory(newPath);

            txtRHAba2Caminho2.Text = newPath;
            cbRHAba3REP.Items.Add("192.168.001.035");
        }

        //******************************************************************************/
        // private: btnRHAba4Converter                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 29/04/2011
        // Descrição : Converte o arquivo com as marcaçoes para aparecer no formato 
        //             lido pelo programa.
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba4Converter_Click(object sender, EventArgs e)
        {
            try
            {
                //Caso o arquivo seja igual ao que está no manual da portaria, deve-se ler linha por linha buscando 
                //os registros que contenha o tipo registro "3".
                //Então lê o arquivo e procura
                string pathArquivo = txtRHAba4Arquivo.Text;
                string pathSalvaEm = txtRHAba4SalvarEm2.Text;
                stPath.Default.LastDate = dtpRHAba3DataFinal.Text;
                stPath.Default.Save();
                //Adiciona ao arquivo
                TxtFiles.exportaArquivo(
                    pathArquivo,
                    pathSalvaEm,
                    txtRHAba4Separador.Text,
                    dtpRHAba3DataInicial.Text,
                    dtpRHAba3DataFinal.Text,
                    dtpRHAba3HoraInicial.Text,
                    dtpRHAba3HoraFinal.Text);
                MessageBox.Show("Conversão finalizada", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Ocorreu uma falha durante a conversão do arquivo", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //******************************************************************************/
        // private: btnRHAba4Abrir1                                                    *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 29/04/2011
        // Descrição : Abre a tela para seleção do arquivo que irá exportar.
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba4Abrir1_Click(object sender, EventArgs e)
        {
            //Abre o arquivo 
            OpenFileDialog dlg = new OpenFileDialog();
            //Filtro
            dlg.Filter = "Arquivo de texto|*.txt";
            //Escolhe o arquivo que vai exportar
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtRHAba4Arquivo.Text = dlg.FileName;
                txtRHAba4Arquivo.SelectionStart = txtRHAba4Arquivo.Text.Length + 1;
                stPath.Default.pathOldFile = dlg.FileName;
                stPath.Default.Save();
            }
        }

        //******************************************************************************/
        // private: btnRHAba4Abrir2                                                    *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 29/04/2011
        // Descrição : Abre a tela para seleção da pasta onde irá salvar o arquivo com 
        //             os registros.
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba4Abrir2_Click(object sender, EventArgs e)
        {
            //Abre a tela para escolher a pasta onde irá salvar
            FolderBrowserDialog pasta = new FolderBrowserDialog();
            //Seleciona a pasta
            if (pasta.ShowDialog() == DialogResult.OK)
            {
                //Adiciona o caminho da pasta ao text
                txtRHAba4SalvarEm2.Text = pasta.SelectedPath;
                txtRHAba4SalvarEm2.SelectionStart = txtRHAba4SalvarEm2.Text.Length + 1;
                stPath.Default.pathNewFile = pasta.SelectedPath;
                stPath.Default.Save();
            }
        }

        //******************************************************************************/
        // private: cbRHAba3REP                                                        *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 29/04/2011
        // Descrição : Habilita o botão
        // Retorno: void
        //
        //******************************************************************************
        private void cbRHAba3REP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtRHAba4SalvarEm.Text != "")
                btnRHAba3Coletar.Enabled = true;
            else btnRHAba3Coletar.Enabled = false;
        }


        //******************************************************************************/
        // private: btnRHAba1Consultar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 29/04/2011
        // Descrição : Consulta dos dados sobre a empresa cadastrada no REP 
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba1Consultar_Click(object sender, EventArgs e)
        {
            int resultado = 0;
            //Caminho onde está salvo o cadastro de esquipamentos
            string path = Application.StartupPath;
            System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");

            //Recebe o IP cadastrado no banco de dados, informando o caminho onde fica os REPs 
            // cadastrado e o nome do REP que deseja.
            string ip = trataRep.consultaTxt(path + "\\REPCad.txt", 2, cbRHAba1NomeREP1.Text, 0);
            //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
            try
            {
                //Conecta ao REP
                resultado = envioDll.ConectaREP(ip, 1);

                //Se não conectar mostra erro
                if (resultado != 0 & resultado != 10005)
                {
                    resultado = envioDll.ConectaREP(ip, 0);
               
                    MessageBox.Show(retornoErro.retornoREP(resultado));
                    MessageBox.Show("Não foi possivel conectar ao " + cbRHAba1NomeREP1.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }
                else
                {
                    //Recebe todos o cadastro do empregador que esta cadastrado no REP
                    if (envioDll.LeCadastro(ip, 0, "", path + "\\empresa.txt") == 0)
                    {
                        //Le o arquivo e mostra as empresas cadastradas
                        FileStream abreArquivoTxt = new FileStream(path + "\\empresa.txt", FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(abreArquivoTxt, iso_8859_1);
                        string line = "";

                        //Le linha a linha
                        while ((line = sr.ReadLine()) != null)
                        {
                            //Recebe a Razao social - 150 caracteres a partir da posição 49 
                            txtRHAba1RazaoSocial.Text = line.Substring(28, 151).Trim();
                            //Recebe o identificador de CNPJ ou CPF - 1 caracter a partir da posição 22                         
                            if (line.Substring((line.Length - 1), 1) == "1")
                                cbRHAba1Identificador.SelectedIndex = 0;
                            else
                                cbRHAba1Identificador.SelectedIndex = 1;
                            //Recebe o CNPJ/CPF - 14 caracteres a partir da posição 23                        
                            txtRHAba1CNPJCPF.Text = line.Substring(0, 14).Trim();
                            //Recebe o CEI - 12 caracteres a partir da posição 37                         
                            txtRHAba1CEI.Text = line.Substring(16, 12).Trim();
                            //Recebe o endereço - 100 caracteres a partir da posição  199
                            txtRHAba1Endereco.Text = line.Substring(180, 100).Trim();
                            //Desconecta do REP
                            resultado = envioDll.ConectaREP(ip, 0);

                        }
                    }
                    else if (resultado != 0) MessageBox.Show(retornoErro.retornoREP(resultado));
                }
            }
            catch
            {
                resultado = envioDll.ConectaREP(ip, 0);

                MessageBox.Show("Falha ao consultar REP.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        //******************************************************************************/
        // private: cbRHAba1Empresa                                                    *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 09/05/2011
        // Descrição : Habilita o botão para consultar
        // Retorno: void
        //
        //******************************************************************************
        private void cbRHAba1Empresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRHAba1Consultar.Enabled = true;
        }

        //******************************************************************************/
        // private: cbRHAba2REP                                                        *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 29/04/2011
        // Descrição : Habilita ou desabilita os botões
        // Retorno: void
        //
        //******************************************************************************
        private void cbRHAba2REP_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Se faltar um campo para preencher nao habilita o botão de envio de cadastro
            if (txtRHAba2Nome.Text != "" & txtRHAba2PIS.Text != "")
                if (txtRHAba2CodigoBarras.Text == "" & txtRHAba2Teclado.Text == "" & txtRHAba2RFID.Text == "")
                {
                    btnRHAba2Alterar.Enabled = false;
                    btnRHAba2Cadastrar.Enabled = false;
                }
                else
                {
                    btnRHAba2Alterar.Enabled = true;
                    btnRHAba2Cadastrar.Enabled = true;
                }
            else
            {
                btnRHAba2Alterar.Enabled = false;
                btnRHAba2Cadastrar.Enabled = false;
            }
            //Caso o pis já esteja preenchido é possivel excluir o funcionário, então pode habilitar o botão
            if (txtRHAba2PIS.Text != "")
                btnRHAba2Excluir.Enabled = true;
            else
                btnRHAba2Excluir.Enabled = false;

        }

        //******************************************************************************/
        // private: btnTIAba2ReceberDataHora                                           *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 13/05/2011 (sexta-feira)
        // Descrição : Recebe a data e hora programada no REP
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba2ReceberDataHora_Click(object sender, EventArgs e)
        {
            int resultado = 0;
            try
            {
                string path = Application.StartupPath;
                //Recebe o ip do REP
                string ip = trataRep.consultaTxt(path + "\\REPCad.txt", 2, cbTIAba2REP.Text, 0);
                //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                //Conecta ao REP
                resultado = envioDll.ConectaREP(ip, 1);

                if (resultado != 0 & resultado != 10005)
                {
                    resultado = envioDll.ConectaREP(ip, 0);

                    MessageBox.Show("Não foi possivel conectar ao " + cbTIAba2REP.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    string[] retorno = envioDll.TempoREP(ip);
                    resultado = int.Parse(retorno[0]);
                  
                    //Caso tenha recebido sem erro carrega os texts com as informações recebidas
                    if (resultado == 0)
                    {
                        dtTIAba2DataREP.Text = retorno[1] + "/" + retorno[2] + "/" + retorno[3];
                        dtTIAba2HoraREP.Text = retorno[4] + ":" + retorno[5] + ":" + retorno[6];
                        envioDll.ConectaREP(ip, 0);

                    }
                    else
                    {
                        envioDll.ConectaREP(ip, 0);
                        dllREP.REP_Conexao_Simples(ip, 0, ref resultado);
                        MessageBox.Show("Falha ao receber data e hora.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //******************************************************************************/
        // private: btnRHAba4Converter                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 13/05/2011
        // Descrição : Envia nova configuração de data e hora
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba2EnviarDataHora_Click(object sender, EventArgs e)
        {
            try
            {
                //Atualiza a data e hora atraves da função abaixo
                if (atualizaTempoREP() == 0)
                    MessageBox.Show("Data e hora atualizadas.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show("Falha na atualização de data e hora.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
                MessageBox.Show("Ocorreu um erro na tentativa de atualização de data e hora", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }           
        }

        //******************************************************************************/
        // private: cbTIAba2REP                                                        *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 13/05/2011
        // Descrição : Habilita os botoes de configuração
        // Retorno: void
        //
        //******************************************************************************
        private void cbTIAba2REP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTIAba2REP.SelectedIndex >= 0)
            {
                btnTIAba2EnviarDataHora.Enabled = true;
                btnTIAba2ReceberDataHora.Enabled = true;
                btnTIAba2ReceberEspacoMemoria.Enabled = true;
                btnTIAba2ReceberImpress.Enabled = true;
                btnTIAba2ReceberTemp.Enabled = true;
                btnTIAba2ReceberRegistro.Enabled = true;
            }

        }

        //******************************************************************************/
        // private: btnTIAba2RecebeEspacoMemoria                                       *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 15/05/2011
        // Descrição : Recebe o valor do espaço livre e ocupado da memoria do REP.
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba2ReceberEspacoMemoria_Click(object sender, EventArgs e)
        {
            // int memUsada = 0, memLivre = 0, resultado = 0;
            string path = Application.StartupPath;
            //Recebe o ip do REP           
            string ip = trataRep.consultaTxt(path + "\\REPCad.txt", 2, cbTIAba2REP.Text, 0);
            trataRep.consultaTxt(path, 2, cbRHAba2REP.Text, 2);            
            //Conecta-se ao REP            
           if (envioDll.ConectaREP(ip, 1) != 0)
            {
                envioDll.ConectaREP(ip, 0);
                MessageBox.Show("Não foi possivel conectar ao " + cbRHAba2REP.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                //Le o espaço da memoria no REP
                int[] retorno = envioDll.espacoMemoria(ip);
                txtTIAba2MemoriaUsada.Text = retorno[1].ToString();
                txtTIAba2MemoriaLivre.Text = retorno[2].ToString();

                // se não recebeu informa
                if (retorno[0] != 0)
                    MessageBox.Show("Não foi possivel receber os dados de memória.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Fecha a conexão com o REP
                envioDll.ConectaREP(ip, 0);
            }

        }

        //******************************************************************************/
        // private: cbTIAba1REPsCadastrados                                            *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 13/05/2011
        // Descrição : Habilita os botões cadastrar e consultar 
        // Retorno: void
        //
        //******************************************************************************
        private void cbTIAba1REPsCadastrados_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnTIAba1Cadastrar.Enabled = true;
            btnTIAba1Consultar.Enabled = true;
            btnTIAba1Excluir.Enabled = true;
        }

        //******************************************************************************/
        // private: cbRHAba2Empresa                                                    *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 13/05/2011
        // Descrição : Habilita os botões
        // Retorno: void
        //
        //******************************************************************************
        private void cbRHAba2Empresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // btnRHAba2Cadastrar.Enabled = true;
            btnRHAba2Excluir.Enabled = true;
            btnRHAba2Alterar.Enabled = true;
        }

        //******************************************************************************/
        // private: btnRHAba2Abrir                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 13/05/2011
        // Descrição : Abre a tela para selecionar o txt 
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba2Abrir_Click(object sender, EventArgs e)
        {
            //Abre o arquivo 
            OpenFileDialog dlg = new OpenFileDialog();
            //Filtro
            dlg.Filter = "Arquivo de texto|*.txt";
            //Escolhe o arquivo que vai exportar
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtRHAba2Arquivo.Text = dlg.FileName;
                txtRHAba2Arquivo.SelectionStart = txtRHAba2Arquivo.Text.Length + 1;
                stPath.Default.pathCadFunc = dlg.FileName;
                stPath.Default.Save();
            }
        }


        //******************************************************************************/
        // private: btnRHAba6Cadastrar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 18/05/2011
        // Descrição : Cadastrar nova senha
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba6Cadastrar_Click(object sender, EventArgs e)
        {
            //Verifica se o usuario a ser cadastrado é o mesmo ki o master
            if (txtRHAba6Login.Text == "masteruser")
            {
                MessageBox.Show("Usuário já existe.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //Verifica se a senha e a confirmação se condizem
            if (txtRHAba6Senha.Text == txtRHAba6ConfirmaSenha.Text)
            {
                //Verifica se foi informado a permissão para o login
                if (cbRHAba6Permissao1.Text == "")
                {
                    MessageBox.Show("Selecione a permissão do usuário", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //Adiciona o usuário
                byte retorno = tratapwd.adicionaUsuario(txtRHAba6Login.Text, txtRHAba6Senha.Text, cbRHAba6Permissao1.Text, "RH");
                //Caso esteja tudo Ok informa que foi cadastrado com sucesso
                if (retorno == 0)
                {
                    cbRHAba6Usuario.Items.Add(txtRHAba6Login.Text);
                    txtRHAba6Login.Text = "";
                    txtRHAba6Senha.Text = "";
                    txtRHAba6ConfirmaSenha.Text = "";
                    MessageBox.Show("Usuário cadastrado com sucesso.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (retorno == 1)
                    MessageBox.Show("Usuário já existe.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Não foi possivel cadastrar o usuário.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else MessageBox.Show("Senha e confirmação de senha diferentes", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        //******************************************************************************/
        // private: txtRHAba6Login                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 18/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba6Login_TextChanged(object sender, EventArgs e)
        {
            if ((cbRHAba6Permissao1.Text != "") & (txtRHAba6Login.Text != "") & (txtRHAba6Senha.Text != "") & (txtRHAba6ConfirmaSenha.Text != ""))
            {
                btnRHAba6Cadastrar.Enabled = true;
            }
            else btnRHAba6Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba6Senha                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 18/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba6Senha_TextChanged(object sender, EventArgs e)
        {
            if ((cbRHAba6Permissao1.Text != "") & (txtRHAba6Login.Text != "") & (txtRHAba6Senha.Text != "") & (txtRHAba6ConfirmaSenha.Text != ""))
            {
                btnRHAba6Cadastrar.Enabled = true;
            }
            else btnRHAba6Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba6ConfirmaSenha                                             *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 18/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba6ConfirmaSenha_TextChanged(object sender, EventArgs e)
        {
            if ((cbRHAba6Permissao1.Text != "") & (txtRHAba6Login.Text != "") & (txtRHAba6Senha.Text != "") & (txtRHAba6ConfirmaSenha.Text != ""))
            {
                btnRHAba6Cadastrar.Enabled = true;
            }
            else btnRHAba6Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: btnRHAba6Consultar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/05/2011
        // Descrição : Verifica se o login existe
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba6Consultar_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath + "\\pwd.txt";
                cbRHAba6Permissao2.SelectedIndex = tratapwd.consultaLogin(path, cbRHAba6Usuario.Text, "RH");
            }
            catch
            {
                MessageBox.Show("Falha ao consultar login.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //******************************************************************************/
        // private: btnRHAba6Excluir                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/05/2011
        // Descrição : Remove o usuário cadastrado
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba6Excluir_Click(object sender, EventArgs e)
        {
            //Não é possivel remover o usuário que já está On-line
            if (usuarioAtual == cbRHAba6Usuario.Text)
            {
                MessageBox.Show("Este usuário está conectado no momento e não pode ser removido.", "Usuário conectado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //Confere se realmente deseja remover o usuário
            if (MessageBox.Show("Deseja remover o usuário?", "Remover usuário", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //No caso de confirmação remove o danado
                string path = Application.StartupPath + "\\pwd.txt";
                if (tratapwd.exluirUsuario(path, cbRHAba6Usuario.Text, "RH") == 0)
                {
                    cbRHAba6Usuario.Items.Remove(cbRHAba6Usuario.SelectedItem);
                    MessageBox.Show("Usuário removido.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Falha ao remover usuário", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //******************************************************************************/
        // private: btnRHAba6Alterar                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/05/2011
        // Descrição : Altera a permissão do usuário
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba6Alterar_Click(object sender, EventArgs e)
        {
            try
            {
                //recebe a localização do pwd.txt
                string path = Application.StartupPath + "\\pwd.txt";
                //Faz a alteração da permissão
                if (tratapwd.alteraPermissao(path, cbRHAba6Usuario.Text, cbRHAba6Permissao2.Text, "RH") == 0)
                    MessageBox.Show("Usuário alterado com sucesso", "Alteração", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Não foi possivel alterar os dados", "Falha na alteração", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
                MessageBox.Show("Não foi possivel alterar os dados", "Falha na alteração", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //******************************************************************************/
        // private: btnRHAba5Alterar                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/05/2011
        // Descrição : Altera as informações sobre o usuário que está on-line
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba5Alterar_Click(object sender, EventArgs e)
        {
            //Verifica se a nova senha é igual a confirmação da senha
            if (txtRHAba5NovaSenha.Text == txtRHAba5ConfirmaSenha.Text)
            {
                //Recebe o caminho do arquivo pwd.txt
                string path = Application.StartupPath + "\\pwd.txt";
                //Faz a alteração do login/senha
                byte retorno = tratapwd.alteraLogin(path, txtRHAba5Login.Text, txtRHAba5Senha.Text, txtRHAba5NovoLogin.Text, txtRHAba5NovaSenha.Text, "RH");
                //se retornar zero limpa os campos e informa que foi alterado
                if (retorno == 0)
                {
                    txtRHAba5Login.Text = "";
                    txtRHAba5Senha.Text = "";
                    txtRHAba5NovoLogin.Text = "";
                    txtRHAba5NovaSenha.Text = "";
                    txtRHAba5ConfirmaSenha.Text = "";
                    MessageBox.Show("Alteração efetuada com sucesso", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //Se retornar 1 informa que o login/senha estão incorretos
                else if (retorno == 1)
                    MessageBox.Show("Login/senha incorreto", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    MessageBox.Show("Não foi possivel alterar os dados do usuário", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //******************************************************************************/
        // private: cbRHAba6Permissao1                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void cbRHAba6Permissao1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbRHAba6Permissao1.Text != "") & (txtRHAba6Login.Text != "") & (txtRHAba6Senha.Text != "") & (txtRHAba6ConfirmaSenha.Text != ""))
                btnRHAba6Cadastrar.Enabled = true;
            else btnRHAba6Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: btnRHAba4Cadastrar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba4Cadastrar_Click(object sender, EventArgs e)
        {
            //Verifica se o login é o mesmo do login master
            //Caso seja não pode recadastrá-lo
            if (txtTIAba5Login.Text == "masteruser")
            {
                MessageBox.Show("Usuário já existe.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //Verifica se a senha é igual a confirmação de senha
            if (txtTIAba5Senha.Text == txtTIAba5ConfirmaSenha.Text)
            {
                //Verifica se a permissão foi selecionado
                if (cbTIAba5Permissao1.Text == "")
                {
                    MessageBox.Show("Selecione a permissão do usuário.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                //Adiciona o usuário
                byte retorno = tratapwd.adicionaUsuario(txtTIAba5Login.Text, txtTIAba5Senha.Text, cbTIAba5Permissao1.Text, "TI");
                //Se o cadastro concluir corretamente limpa os campos e informa que foi concluído
                if (retorno == 0)
                {
                    cbTIAba5Usuarios.Items.Add(txtTIAba5Login.Text);
                    txtTIAba5Login.Text = "";
                    txtTIAba5Senha.Text = "";
                    txtTIAba5ConfirmaSenha.Text = "";
                    cbTIAba5Permissao1.Text = "";
                    MessageBox.Show("Usuário cadastrado com sucesso.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //Se retornar 1 informa que o usuario já existe
                else if (retorno == 1)
                    MessageBox.Show("Usuário já existe.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                    //se der erro aparece nao cadastrado
                    MessageBox.Show("Não foi possível cadastrar o usuário.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else MessageBox.Show("Senha e confirmação de senha diferentes.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //******************************************************************************/
        // private: cbRHAba6Usuario                                                    *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 18/05/2011
        // Descrição : Habilitar o botão de consultar, excluir e alterar
        // Retorno: void
        //
        //******************************************************************************
        private void cbRHAba6Usuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRHAba6Usuario.Text != "")
            {
                btnRHAba6Consultar.Enabled = true;
                btnRHAba6Excluir.Enabled = true;
                btnRHAba6Alterar.Enabled = true;
            }
            else
            {
                btnRHAba6Consultar.Enabled = false;
                btnRHAba6Excluir.Enabled = false;
                btnRHAba6Alterar.Enabled = false;
            }
        }

        //******************************************************************************/
        // private: btnTIAba4Consultar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Consulta a permissão do login solicitado
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba4Consultar_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath + "\\pwd.txt";
                cbTIAba5Permissao2.SelectedIndex = tratapwd.consultaLogin(path, cbTIAba5Usuarios.Text, "TI");
            }
            catch
            {
                MessageBox.Show("Falha ao consultar login.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //******************************************************************************/
        // private: txtTIAba4Excluir                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Remove o usuário
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba4Excluir_Click(object sender, EventArgs e)
        {
            //Verifica se o usuário que será removido é o mesmo que está on-line
            //se este estiver conectado informa que não pode excluí-lo
            if (usuarioAtual == cbTIAba5Usuarios.Text)
            {
                MessageBox.Show("Este usuário está conectado no momento e não pode ser removido.", "Usuário conectado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //Confirma se há a intenção de remover o usuário
            if (MessageBox.Show("Deseja remover o usuário?", "Remover usuário", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                //Recebe o caminho do arquivo
                string path = Application.StartupPath + "\\pwd.txt";
                //Exclui o usuário
                if (tratapwd.exluirUsuario(path, cbTIAba5Usuarios.Text, "TI") == 0)
                {
                    cbTIAba5Usuarios.Items.Remove(cbTIAba5Usuarios.SelectedItem);
                    MessageBox.Show("Usuário removido", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Falha ao remover usuário", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //******************************************************************************/
        // private: txtTIAba4Alterar                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Altera a permissao do usuario de TI
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba4Alterar_Click(object sender, EventArgs e)
        {
            try
            {
                //Recebe o caminho do arquivo de login/senha 
                string path = Application.StartupPath + "\\pwd.txt";
                //Faz a alteração da permissão
                if (tratapwd.alteraPermissao(path, cbTIAba5Usuarios.Text, cbTIAba5Permissao2.Text, "TI") == 0)
                    MessageBox.Show("Usuário alterado com sucesso", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Não foi possivel alterar os dados", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
                MessageBox.Show("Não foi possivel alterar os dados", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //******************************************************************************/
        // private: cbTIAba4Usuarios                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se há usuário selecionado e habilita os botões
        // Retorno: void
        //
        //******************************************************************************
        private void cbTIAba4Usuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTIAba5Usuarios.SelectedIndex >= 0)
            {
                btnTIAba5Consultar.Enabled = true;
                btnTIAba5Excluir.Enabled = true;
                btnTIAba5Alterar.Enabled = true;
            }
            else
            {
                btnTIAba5Consultar.Enabled = false;
                btnTIAba5Excluir.Enabled = false;
                btnTIAba5Alterar.Enabled = false;
            }
        }

        //******************************************************************************/
        // private: cbTIAba4Permissao1                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void cbTIAba4Permissao1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbTIAba5Permissao1.Text != "") & (txtTIAba5Login.Text != "") & (txtTIAba5Senha.Text != "") & (txtTIAba5ConfirmaSenha.Text != ""))
                btnTIAba5Cadastrar.Enabled = true;
            else btnTIAba5Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba4ConfirmaSenha                                             *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba4ConfirmaSenha_TextChanged(object sender, EventArgs e)
        {
            if ((cbTIAba5Permissao1.Text != "") & (txtTIAba5Login.Text != "") & (txtTIAba5Senha.Text != "") & (txtTIAba5ConfirmaSenha.Text != ""))
                btnTIAba5Cadastrar.Enabled = true;
            else btnTIAba5Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba4Senha                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba4Senha_TextChanged(object sender, EventArgs e)
        {
            if ((cbTIAba5Permissao1.Text != "") & (txtTIAba5Login.Text != "") & (txtTIAba5Senha.Text != "") & (txtTIAba5ConfirmaSenha.Text != ""))
                btnTIAba5Cadastrar.Enabled = true;
            else btnTIAba5Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba4Login                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba4Login_TextChanged(object sender, EventArgs e)
        {
            if ((cbTIAba5Permissao1.Text != "") & (txtTIAba5Login.Text != "") & (txtTIAba5Senha.Text != "") & (txtTIAba5ConfirmaSenha.Text != ""))
                btnTIAba5Cadastrar.Enabled = true;
            else btnTIAba5Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: btnTIAba3Alterar                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Altera as informações (login e senha) do usuario conectado
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba3Alterar_Click(object sender, EventArgs e)
        {
            //Verifica se a nova senha e a confirmação da senha estão iguais
            if (txtTIAba4NovaSenha.Text == txtTIAba4ConfirmaSenha.Text)
            {
                //Recebe o caminho do arquivo pwd.txt (caminho local do aplicativo)
                string path = Application.StartupPath + "\\pwd.txt";
                //Faz a alteração do usuario
                byte retorno = tratapwd.alteraLogin(path, txtTIAba4Login.Text, txtTIAba4Senha.Text, txtTIAba4NovoLogin.Text, txtTIAba4NovaSenha.Text, "TI");
                //se for executado com sucesso limpa os txt e informa que foi efetuado com sucesso
                if (retorno == 0)
                {
                    txtTIAba4Login.Text = "";
                    txtTIAba4Senha.Text = "";
                    txtTIAba4NovoLogin.Text = "";
                    txtTIAba4NovaSenha.Text = "";
                    txtTIAba4ConfirmaSenha.Text = "";
                    MessageBox.Show("Alteração efetuada com sucesso", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //Se retornou 1 informa que o login ou senha estão incorretos
                else if (retorno == 1)
                    MessageBox.Show("Login/senha incorreto", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Se ocorreu erro informa que nao foi possivel fazer a alteração
                else
                    MessageBox.Show("Não foi possível alterar os dados do usuário", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Senha e confirmação de senha não conferem", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //******************************************************************************/
        // private: btnTIAba3Login                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Altera as informações (login e senha) do usuario conectado
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba3Login_TextChanged(object sender, EventArgs e)
        {
            if ((txtTIAba4Login.Text != "") & (txtTIAba4Senha.Text != "") & (txtTIAba4NovoLogin.Text != "") & (txtTIAba4NovaSenha.Text != "") & (txtTIAba4ConfirmaSenha.Text != ""))
                btnTIAba4Alterar.Enabled = true;
            else
                btnTIAba4Alterar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba3Senha                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba3Senha_TextChanged(object sender, EventArgs e)
        {
            if ((txtTIAba4Login.Text != "") & (txtTIAba4Senha.Text != "") & (txtTIAba4NovoLogin.Text != "") & (txtTIAba4NovaSenha.Text != "") & (txtTIAba4ConfirmaSenha.Text != ""))
                btnTIAba4Alterar.Enabled = true;
            else
                btnTIAba4Alterar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba3NovoLogin                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba3NovoLogin_TextChanged(object sender, EventArgs e)
        {
            if ((txtTIAba4Login.Text != "") & (txtTIAba4Senha.Text != "") & (txtTIAba4NovoLogin.Text != "") & (txtTIAba4NovaSenha.Text != "") & (txtTIAba4ConfirmaSenha.Text != ""))
                btnTIAba4Alterar.Enabled = true;
            else
                btnTIAba4Alterar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba3NovaSenha                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba3NovaSenha_TextChanged(object sender, EventArgs e)
        {
            if ((txtTIAba4Login.Text != "") & (txtTIAba4Senha.Text != "") & (txtTIAba4NovoLogin.Text != "") & (txtTIAba4NovaSenha.Text != "") & (txtTIAba4ConfirmaSenha.Text != ""))
                btnTIAba4Alterar.Enabled = true;
            else
                btnTIAba4Alterar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba3ConfirmaSenha                                             *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba3ConfirmaSenha_TextChanged(object sender, EventArgs e)
        {
            if ((txtTIAba4Login.Text != "") & (txtTIAba4Senha.Text != "") & (txtTIAba4NovoLogin.Text != "") & (txtTIAba4NovaSenha.Text != "") & (txtTIAba4ConfirmaSenha.Text != ""))
                btnTIAba4Alterar.Enabled = true;
            else
                btnTIAba4Alterar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba5Login                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba5Login_TextChanged(object sender, EventArgs e)
        {
            if ((txtRHAba5Login.Text != "") & (txtRHAba5Senha.Text != "") & (txtRHAba5NovoLogin.Text != "") & (txtRHAba5NovaSenha.Text != "") & (txtRHAba5ConfirmaSenha.Text != ""))
                btnRHAba5Alterar.Enabled = true;
            else
                btnRHAba5Alterar.Enabled = false;
        }

        //******************************************************************************/
        // public: configuraTelasTI                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica o tipo de permissão e habilita as telas
        // Retorno: void
        //
        //******************************************************************************
        public void configuraTelasTI(string permissao, string master)
        {
            //Caminho local do aplicativo
            string path = Application.StartupPath + "\\pwd.txt";
            string pathRep = Application.StartupPath + "\\REPCad.txt";
            //Limpa os campos antes de preenche-los
            cbTIAba1REPsCadastrados.Items.Clear();
            cbTIAba2REP.Items.Clear();
            cbRHAba2REP3.Items.Clear();
            cbTIAba5Usuarios.Items.Clear();
            tabControl1.TabPages.Clear();
            //Recebe o nome dos REPs cadastrados
            stringList.Clear();
            stringList.AddRange(trataRep.ConsultaREPCadastrado(pathRep, 2).Split(';'));

            for (int i = 0; i < stringList.Count - 1; i++)
            {
                //Adiciona nos combos do RH para nao ocorrer erro                
                cbRHAba1NomeREP1.Items.Add(stringList[i].ToString());
                cbRHAba3REP.Items.Add(stringList[i].ToString());
                cbRHAba2REP.Items.Add(stringList[i].ToString());
                cbRHAba2REP2.Items.Add(stringList[i].ToString());
                cbRHAba2REP3.Items.Add(stringList[i].ToString());
                //Adiciona as informações nos combos de TI
                cbTIAba1REPsCadastrados.Items.Add(stringList[i].ToString());
                cbTIAba2REP.Items.Add(stringList[i].ToString());
                cbTIAba3REPsCadastrados.Items.Add(stringList[i].ToString());
            }
            //Verifica se o usuario existe e preenche o campo contendo o nome do usuário
            string[] retorno = TxtFiles.consultaNoPwd(path, "TI", 3).Split(';');
            if ((retorno[0] != "") & (retorno[0] != "2"))
                cbTIAba5Usuarios.Items.AddRange(retorno);

            tabControl1.TabPages.Add(tabPage1);
            tabControl1.TabPages.Add(tabPage6);
            tabControl1.TabPages.Add(tabPage11);
            //Se nao for usuario master habilita a tela para modificação de senha
            if (master != "master")
                tabControl1.TabPages.Add(tabPage2);
            //Se for administrador habilita a tela de administração de usuários
            if (permissao == "Administrador")
                tabControl1.TabPages.Add(tabPage8);

            tabControl1.SelectedTab = tabPage1;
        }

        //******************************************************************************/
        // public: configuraTelasRH                                                    *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica a permissão do usuario e habilita as telas
        // Retorno: void
        //
        //******************************************************************************        
        public void configuraTelasRH(string permissao, string master)
        {
            //Preenche os campos referente ao caminho dos arquivos de coleta
            txtRHAba4Arquivo.Text = stPath.Default.pathOldFile;//Informações sobre o arquivo de coleta
            txtRHAba4Arquivo.SelectionStart = txtRHAba4Arquivo.Text.Length + 1;
            if (txtRHAba4Arquivo.Text == "") txtRHAba4Arquivo.Text = Application.StartupPath;

            txtRHAba4SalvarEm2.Text = stPath.Default.pathNewFile;//Caminho onde será salvo esse arquivo
            txtRHAba4SalvarEm2.SelectionStart = txtRHAba4SalvarEm2.Text.Length + 1;
            if (txtRHAba4SalvarEm2.Text == "") txtRHAba4SalvarEm2.Text = Application.StartupPath;

            txtRHAba4SalvarEm.Text = stPath.Default.pathFile;//Caminho onde será salvo esse arquivo de registro
            txtRHAba4SalvarEm.SelectionStart = txtRHAba4SalvarEm.Text.Length + 1;
            if (txtRHAba4SalvarEm.Text == "") txtRHAba4SalvarEm.Text = Application.StartupPath;

            txtRHAba2Arquivo.Text = stPath.Default.pathCadFunc;
            txtRHAba2Arquivo.SelectionStart = txtRHAba2Arquivo.Text.Length + 1;
            if (txtRHAba2Arquivo.Text == "") txtRHAba2Arquivo.Text = Application.StartupPath + "\\CadFuncionarios.txt";

            txtRHAba2Caminho.Text = stPath.Default.pathSaveFunc;
            txtRHAba2Caminho.SelectionStart = txtRHAba2Caminho.Text.Length + 1;
            if (txtRHAba2Caminho.Text == "") txtRHAba2Caminho.Text = Application.StartupPath;

            //Data da ultima importação
            if (stPath.Default.LastDate != "")
                dtpRHAba3DataInicial.Text = stPath.Default.LastDate;
            dtpRHAba3DataFinal.Text = DateTime.Today.ToString();

            string path = Application.StartupPath + "\\pwd.txt";//Caminho local do programa
            string pathRep = Application.StartupPath + "\\REPCad.txt";//Caminho local do programa

            csEncryptDecrypt criptografa = new csEncryptDecrypt();//Classe cxEncryptDescrypt
            //Limpa os campos antes de utiliza-los
            stringList.Clear();
            cbRHAba1NomeREP1.Items.Clear();
            cbRHAba3REP.Items.Clear();
            cbRHAba2REP.Items.Clear();
            cbRHAba2REP2.Items.Clear();
            cbRHAba2REP3.Items.Clear();
            cbRHAba6Usuario.Items.Clear();
            tabControl2.TabPages.Clear();
            //Preenche os campos com o nome dos REPs cadastrados
            stringList.AddRange(trataRep.ConsultaREPCadastrado(pathRep, 2).Split(';'));

            for (int i = 0; i < stringList.Count - 1; i++)
            {
                cbRHAba1NomeREP1.Items.Add(stringList[i].ToString());
                cbRHAba3REP.Items.Add(stringList[i].ToString());
                cbRHAba2REP.Items.Add(stringList[i].ToString());
                cbRHAba2REP2.Items.Add(stringList[i].ToString());
                cbRHAba2REP3.Items.Add(stringList[i].ToString());
            }

            //Verifica se o usuario existe e preenche o campo contendo o nome do usuário
            string[] retorno2 = TxtFiles.consultaNoPwd(path, "RH", 3).Split(';');
            if ((retorno2[0] != "") & (retorno2[0] != "2"))
                cbRHAba6Usuario.Items.AddRange(retorno2);
            //Adiciona as abas/paginas que poderam ser vistar para cada permissão 
            tabControl2.TabPages.Add(tabPage3);
            tabControl2.TabPages.Add(tabPage9);
            tabControl2.TabPages.Add(tabPage7);

            //Aba para mudar a senha, senha master é padrão
            if (master != "master")
                tabControl2.TabPages.Add(tabPage5);
            // Se a permissao for de administrador habilita a aba para administração de usuários
            if (permissao == "Administrador")
            {
                tabControl2.TabPages.Add(tabPage10);
            }
            tabControl2.SelectedTab = tabPage3;
        }

        //******************************************************************************/
        // private: txtRHAba5Senha                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba5Senha_TextChanged(object sender, EventArgs e)
        {
            if ((txtRHAba5Login.Text != "") & (txtRHAba5Senha.Text != "") & (txtRHAba5NovoLogin.Text != "") & (txtRHAba5NovaSenha.Text != "") & (txtRHAba5ConfirmaSenha.Text != ""))
                btnRHAba5Alterar.Enabled = true;
            else
                btnRHAba5Alterar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba5NovoLogin                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba5NovoLogin_TextChanged(object sender, EventArgs e)
        {
            if ((txtRHAba5Login.Text != "") & (txtRHAba5Senha.Text != "") & (txtRHAba5NovoLogin.Text != "") & (txtRHAba5NovaSenha.Text != "") & (txtRHAba5ConfirmaSenha.Text != ""))
                btnRHAba5Alterar.Enabled = true;
            else
                btnRHAba5Alterar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba5NovaSenha                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba5NovaSenha_TextChanged(object sender, EventArgs e)
        {
            if ((txtRHAba5Login.Text != "") & (txtRHAba5Senha.Text != "") & (txtRHAba5NovoLogin.Text != "") & (txtRHAba5NovaSenha.Text != "") & (txtRHAba5ConfirmaSenha.Text != ""))
                btnRHAba5Alterar.Enabled = true;
            else
                btnRHAba5Alterar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba5ConfirmaSenha                                             *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/05/2011
        // Descrição : Verifica se todos os campos estão preenchidos para poder 
        //             habilitar o botão de cadastrar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba5ConfirmaSenha_TextChanged(object sender, EventArgs e)
        {
            if ((txtRHAba5Login.Text != "") & (txtRHAba5Senha.Text != "") & (txtRHAba5NovoLogin.Text != "") & (txtRHAba5NovaSenha.Text != "") & (txtRHAba5ConfirmaSenha.Text != ""))
                btnRHAba5Alterar.Enabled = true;
            else
                btnRHAba5Alterar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba1MascaraRede                                               *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 23/05/2011
        // Descrição : Verifica se a tecla digitada é numerico, backspace ou ponto
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba1MascaraRede_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }

        //******************************************************************************/
        // private: txtTIAba1IPREP                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 23/05/2011
        // Descrição : Verifica se a tecla digitada é numerico, backspace ou ponto
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba1IPREP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }

        //******************************************************************************/
        // private: txtTIAba1PortaComunicacao                                          *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 23/05/2011
        // Descrição : Verifica se a tecla digitada é numerico ou backspace 
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba1PortaComunicacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        //******************************************************************************/
        // private: btnTIAba1Consultar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 23/05/2011
        // Descrição : Consulta a configuração de REDE do REP
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba1Consultar_Click(object sender, EventArgs e)
        {
            string pathRep = Application.StartupPath + "\\REPCad.txt";
            try
            {
                FileStream srcFile = new FileStream(pathRep, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(srcFile);
                string line = "";
                //Le linha a linha
                while ((line = sr.ReadLine()) != null)
                {
                    //Separa a linha em coluna
                    string[] auxLine = line.Split(';');
                    //Verifica se a linha que está sendo lido é semelhante ao que desejamos consultar
                    if (auxLine[2] == cbTIAba1REPsCadastrados.Text)
                    {
                        //Preenche os campos
                        txtTIAba1IPREP.Text = auxLine[0];
                        txtTIAba1MascaraRede.Text = auxLine[1];
                        txtTIAba1NomeREP.Text = auxLine[2];
                        return;
                    }
                    else
                    {
                        //Apaga as informações que estão nos campos
                        txtTIAba1IPREP.Text = "";
                        txtTIAba1MascaraRede.Text = "";
                        txtTIAba1NomeREP.Text = "";
                    }
                }
                //Fecha o arquivo TXT             
                sr.Close();
                srcFile.Close();

            }
            catch
            {
                MessageBox.Show("Não foi possível receber a configuração", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        //******************************************************************************/
        // private: txtRHAba2Nome                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/07/2011
        // Descrição : Verifica se há algo na caixa de texto validando os campos obrigatorios
        //              para poder habilitar os botões
        // Retorno: void
        //
        //******************************************************************************      
        private void txtRHAba2Nome_TextChanged(object sender, EventArgs e)
        {
            if (cbRHAba2REP.Text != "")
            {
                if (txtRHAba2Nome.Text != "" & txtRHAba2PIS.Text != "" & (txtRHAba2CodigoBarras.Text != "" ||
                    txtRHAba2Teclado.Text != "" || txtRHAba2RFID.Text != ""))
                {
                    btnRHAba2Alterar.Enabled = true;
                    btnRHAba2Cadastrar.Enabled = true;
                }
                else
                {
                    btnRHAba2Alterar.Enabled = false;
                    btnRHAba2Cadastrar.Enabled = false;
                }
            }
        }

        //******************************************************************************/
        // private: txtRHAba2PIS                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/07/2011
        // Descrição : Verifica se há algo na caixa de texto validando os campos obrigatorios
        //              para poder habilitar os botões
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba2PIS_TextChanged(object sender, EventArgs e)
        {
            if (cbRHAba2REP.Text != "")
            {
                if (txtRHAba2Nome.Text != "" & txtRHAba2PIS.Text != "" & (txtRHAba2CodigoBarras.Text != "" ||
                    txtRHAba2Teclado.Text != "" || txtRHAba2RFID.Text != ""))
                {
                    btnRHAba2Alterar.Enabled = true;
                    btnRHAba2Cadastrar.Enabled = true;
                }
                else
                {
                    btnRHAba2Alterar.Enabled = false;
                    btnRHAba2Cadastrar.Enabled = false;
                }
                if (txtRHAba2PIS.Text != "")
                    btnRHAba2Excluir.Enabled = true;
                else btnRHAba2Excluir.Enabled = false;
            }
            else
            {
                btnRHAba2Alterar.Enabled = false;
                btnRHAba2Cadastrar.Enabled = false;
            }
        }

        //******************************************************************************/
        // private: txtRHAba2CodigoBarras                                              *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/07/2011
        // Descrição : Verifica se há algo na caixa de texto validando os campos obrigatorios
        //              para poder habilitar os botões
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba2CodigoBarras_TextChanged(object sender, EventArgs e)
        {
            if (cbRHAba2REP.Text != "")
            {
                if (txtRHAba2Nome.Text != "" & txtRHAba2PIS.Text != "" & (txtRHAba2CodigoBarras.Text != "" ||
                    txtRHAba2Teclado.Text != "" || txtRHAba2RFID.Text != ""))
                {
                    btnRHAba2Alterar.Enabled = true;
                    btnRHAba2Cadastrar.Enabled = true;
                }
                else
                {
                    btnRHAba2Alterar.Enabled = false;
                    btnRHAba2Cadastrar.Enabled = false;
                }
            }
        }

        //******************************************************************************/
        // private: txtRHAba2Mifare                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/07/2011
        // Descrição : Verifica se há algo na caixa de texto validando os campos obrigatorios
        //              para poder habilitar os botões
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba2Mifare_TextChanged(object sender, EventArgs e)
        {
            if (cbRHAba2REP.Text != "")
            {
                if (txtRHAba2Nome.Text != "" & txtRHAba2PIS.Text != "" & (txtRHAba2CodigoBarras.Text != "" ||
                    txtRHAba2Teclado.Text != "" || txtRHAba2RFID.Text != ""))
                {
                    btnRHAba2Alterar.Enabled = true;
                    btnRHAba2Cadastrar.Enabled = true;
                }
                else
                {
                    btnRHAba2Alterar.Enabled = false;
                    btnRHAba2Cadastrar.Enabled = false;
                }
            }
        }

        //******************************************************************************/
        // private: txtRHAba2RFID                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/07/2011
        // Descrição : Verifica se há algo na caixa de texto validando os campos obrigatorios
        //              para poder habilitar os botões
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba2RFID_TextChanged(object sender, EventArgs e)
        {
            if (cbRHAba2REP.Text != "")
            {
                if (txtRHAba2Nome.Text != "" & txtRHAba2PIS.Text != "" & (txtRHAba2CodigoBarras.Text != "" ||
                    txtRHAba2Teclado.Text != "" || txtRHAba2RFID.Text != ""))
                {
                    btnRHAba2Alterar.Enabled = true;
                    btnRHAba2Cadastrar.Enabled = true;
                }
                else
                {
                    btnRHAba2Alterar.Enabled = false;
                    btnRHAba2Cadastrar.Enabled = false;
                }
            }
        }

        //******************************************************************************/
        // private: txtRHAba2PIS                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/07/2011
        // Descrição : Verifica se o que foi digitado é numerico ou backspace e adiciona
        //             ao campo
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba2PIS_KeyPress(object sender, KeyPressEventArgs e)
        {
            // somente numeros e backspace
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        //******************************************************************************/
        // private: txtRHAba2Mifare                                                    *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/07/2011
        // Descrição : Verifica se o que foi digitado é numerico ou backspace e adiciona
        //             ao campo
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba2Mifare_KeyPress(object sender, KeyPressEventArgs e)
        {
            // somente numeros
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        //******************************************************************************/
        // private: txtRHAba1CNPJCPF                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/07/2011
        // Descrição : Verifica se o que foi digitado é numerico ou backspace e adiciona
        //             ao campo
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba1CNPJCPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            // somente numeros
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        //******************************************************************************/
        // private: txtRHAba1CEI                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/07/2011
        // Descrição : Verifica se o que foi digitado é numerico ou backspace e adiciona
        //             ao campo
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba1CEI_KeyPress(object sender, KeyPressEventArgs e)
        {
            // somente numeros
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        //******************************************************************************/
        // private: btnTIAba1Excluir                                                *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 14/07/2011
        // Descrição : Remove REP da lista de cadastrados
        // Retorno: void
        //
        //******************************************************************************      
        private void btnTIAba1Excluir_Click(object sender, EventArgs e)
        {
            //Caminho do arquivo
            string path = Application.StartupPath + "\\REPCad.txt";
            try
            {
                //Confirma se há a intenção de remover o REP da lista
                if (MessageBox.Show("Confirma remoção do cadastro do REP " + cbTIAba1REPsCadastrados.Text + "?", "Remover REP", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    //Armazena todas as linhas do REPCad.txt
                    var file = new List<string>(System.IO.File.ReadAllLines(path));
                    //Remove a linha que deseja
                    file.RemoveAt(cbTIAba1REPsCadastrados.SelectedIndex);
                    //Salva o REPCad sem a linha 
                    File.WriteAllLines(path, file.ToArray());
                    //Remove de todas as listas
                    cbTIAba2REP.Items.RemoveAt(cbTIAba1REPsCadastrados.SelectedIndex);
                    cbTIAba3REPsCadastrados.Items.RemoveAt(cbTIAba1REPsCadastrados.SelectedIndex);
                    cbRHAba1NomeREP1.Items.RemoveAt(cbTIAba1REPsCadastrados.SelectedIndex);
                    cbRHAba3REP.Items.RemoveAt(cbTIAba1REPsCadastrados.SelectedIndex);
                    cbRHAba2REP.Items.RemoveAt(cbTIAba1REPsCadastrados.SelectedIndex);
                    cbRHAba2REP2.Items.RemoveAt(cbTIAba1REPsCadastrados.SelectedIndex);
                    cbRHAba2REP3.Items.RemoveAt(cbTIAba1REPsCadastrados.SelectedIndex);

                    cbTIAba1REPsCadastrados.Items.RemoveAt(cbTIAba1REPsCadastrados.SelectedIndex);
                    //Limpa os campos
                    txtTIAba1IPREP.Text = "";
                    txtTIAba1MascaraRede.Text = "";
                    txtTIAba1NomeREP.Text = "";
                    MessageBox.Show("Cadastro removido");
                }
            }
            catch
            {
                MessageBox.Show("Ocorreu um erro na tentativa de remoção");
            }
        }

        //******************************************************************************/
        // private: btnRHAba2Importar                                                  *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 15/07/2011
        // Descrição : Envia os dados dos funcionários que se encontram em um txt para
        //              o REP como forma de cadastro de funcionário
        // Retorno: void
        //
        //******************************************************************************    
        private void btnRHAba2Importar_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath + "\\REPCad.txt";
                //Recebe o IP cadastrado no banco de dados                
                string ip = trataRep.consultaTxt(path, 2, cbRHAba2REP2.Text, 0);
                trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                GravarFuncionarios(ip, txtRHAba2Arquivo.Text);
            }
            catch
            {
            }

        }
        //******************************************************************************/
        // private: btnRHAba2Adicionar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 15/07/2011
        // Descrição : Adiciona as informações do funcionario em um txt para importar
        //             posteriormente
        // Retorno: void
        //
        //******************************************************************************    
        private void btnRHAba2Adicionar_Click(object sender, EventArgs e)
        {
            string RFID, CodBarras, Teclado, Biometria = "0";
            string arquivo = Application.StartupPath + "\\CadFuncionarios.txt"; //Abre o arquivo para escrita
            //StreamWriter salvandoArquivoTxt = new StreamWriter(arquivo);
            if (File.Exists(arquivo) == false)
            {
                TextWriter salvandoArquivo = File.AppendText(arquivo);
                //salvandoArquivo.WriteLine("Nome;PIS;ContactLess;Codigo de Barras;Teclado;Biometr;grupo;");
                salvandoArquivo.Close();
            }

            TextWriter salvandoArquivoTxt = File.AppendText(arquivo);
            try
            {
                if (txtRHAba2Nome2.Text == "")
                {
                    salvandoArquivoTxt.Close();
                    MessageBox.Show("Favor preencher com o nome do funcionário.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRHAba2Nome2.Focus();
                    return;
                }
                //Verifica se o Pis é válido
                if (Validacao.ValidaPis(txtRHAba2PIS2.Text) == false)
                {
                    salvandoArquivoTxt.Close();
                    MessageBox.Show("O numero do PIS está incorreto.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRHAba2PIS2.Focus();
                    return;
                }
                if (txtRHAba2RFID2.Text != "")
                    RFID = txtRHAba2RFID2.Text;
                else RFID = "0";
                if (txtRHAba2CodBarras2.Text != "")
                    CodBarras = txtRHAba2CodBarras2.Text;
                else CodBarras = "0";
                if (txtRHAba2Teclado2.Text != "")
                    Teclado = txtRHAba2Teclado2.Text;
                else Teclado = "0";
                if (Biometria != " ")
                    Biometria = "0";
                //Se for marcação de ponto adiciona ao txt novo
                salvandoArquivoTxt.WriteLine(
                    txtRHAba2Nome2.Text + ";" +
                    txtRHAba2PIS2.Text + ";" +
                    RFID + ";" +
                    CodBarras + ";" +
                    Teclado + ";" +
                    Biometria + ";" +
                    txtRHAba2Grupo2.Text);

                txtRHAba2Nome2.Text = "";
                txtRHAba2PIS2.Text = "";
                txtRHAba2Grupo2.Text = "";
                txtRHAba2CodBarras2.Text = "";
                txtRHAba2Teclado2.Text = "";
                txtRHAba2RFID2.Text = "";

                MessageBox.Show("Funcionário adicionado.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRHAba2Nome2.Focus();
                //Fecha o arquivo para salvar                
                salvandoArquivoTxt.Close();
            }
            catch
            {
                salvandoArquivoTxt.Close();
            }
        }

        //******************************************************************************/
        // private: txtRHAba1RazaoSocial                                               *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 18/07/2011
        // Descrição : Desabilita a digitação de pontuação
        // Retorno: void
        //
        //******************************************************************************    
        private void txtRHAba1RazaoSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            // somente numeros
            if (char.IsPunctuation(e.KeyChar))
                e.Handled = true;
        }

        //******************************************************************************/
        // private: txtTIAba1IPREP                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/07/2011
        // Descrição : Habilita ou desabilita o botão para cadastrar
        // Retorno: void
        //
        //******************************************************************************    
        private void txtTIAba1IPREP_TextChanged(object sender, EventArgs e)
        {
            if (txtTIAba1IPREP.Text != "" & txtTIAba1MascaraRede.Text != "" & txtTIAba1NomeREP.Text != "")
                btnTIAba1Cadastrar.Enabled = true;
            else btnTIAba1Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba1MascaraRede                                               *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/07/2011
        // Descrição : Habilita ou desabilita o botão para cadastrar
        // Retorno: void
        //
        //******************************************************************************      
        private void txtTIAba1MascaraRede_TextChanged(object sender, EventArgs e)
        {
            if (txtTIAba1IPREP.Text != "" & txtTIAba1MascaraRede.Text != "" & txtTIAba1NomeREP.Text != "")
                btnTIAba1Cadastrar.Enabled = true;
            else btnTIAba1Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba1PortaComunicacao                                          *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/07/2011
        // Descrição : Habilita ou desabilita o botão para cadastrar
        // Retorno: void
        //
        //******************************************************************************      
        private void txtTIAba1PortaComunicacao_TextChanged(object sender, EventArgs e)
        {
            if (txtTIAba1IPREP.Text != "" & txtTIAba1MascaraRede.Text != "" &txtTIAba1NomeREP.Text != "")
                btnTIAba1Cadastrar.Enabled = true;
            else btnTIAba1Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba1NomeREP                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/07/2011
        // Descrição : Habilita ou desabilita o botão para cadastrar
        // Retorno: void
        //
        //******************************************************************************      
        private void txtTIAba1NomeREP_TextChanged(object sender, EventArgs e)
        {
            if (txtTIAba1IPREP.Text != "" & txtTIAba1MascaraRede.Text != "" & txtTIAba1NomeREP.Text != "")
                btnTIAba1Cadastrar.Enabled = true;
            else btnTIAba1Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba1Senha                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/07/2011
        // Descrição : Habilita ou desabilita o botão para cadastrar
        // Retorno: void
        //
        //******************************************************************************      
        private void txtTIAba1Senha_TextChanged(object sender, EventArgs e)
        {
            if (txtTIAba1IPREP.Text != "" & txtTIAba1MascaraRede.Text != "" & txtTIAba1NomeREP.Text != "")
                btnTIAba1Cadastrar.Enabled = true;
            else btnTIAba1Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba3IPREP                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 15/07/2011
        // Descrição : Desabilita a escrita de letra
        // Retorno: void
        //
        //******************************************************************************            
        private void txtTIAba3IPREP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }

        //******************************************************************************/
        // private: txtTIAba3MascaraRede                                               *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 15/07/2011
        // Descrição : Desabilita a escrita de letra
        // Retorno: void
        //
        //******************************************************************************            
        private void txtTIAba3MascaraRede_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
        }

        //******************************************************************************/
        // private: txtTIAba3Cadastrar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/07/2011
        // Descrição : Envia uma nova configuração de IP para o REP
        // Retorno: void
        //
        //******************************************************************************            
        private void btnTIAba3Cadastrar_Click(object sender, EventArgs e)
        {
            btnTIAba3Cadastrar.Enabled = false;//Desabilita o botão
            btnTIAba3Consultar.Enabled = false;
            byte[] ipRep = new byte[16];
            byte[] maskRep = new byte[16];
            //System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
            //Recebe o ip, a mascara e a porta que irá enviar ao REP
            ipRep = iso_8859_1.GetBytes(txtTIAba3IPREP.Text);
            maskRep = iso_8859_1.GetBytes(txtTIAba3MascaraRede.Text);

            int resultado = 0;
            //string pathRep = txtRHAba2Arquivo.Text; //Application.StartupPath + "\\CadFuncionarios.txt";
            string path = Application.StartupPath + "\\REPCad.txt";
            string ip = trataRep.consultaTxt(path, 2, cbTIAba3REPsCadastrados.Text, 0);
            trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
            //Conecta ao REP
            dllREP.REP_Conexao_Simples(ip, 1, ref resultado);
            if (resultado != 0 & resultado != 10005)
            {
                dllREP.REP_Conexao_Simples(ip, 0, ref resultado);
                MessageBox.Show("Não foi possivel conectar ao " + cbTIAba3REPsCadastrados.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnTIAba3Cadastrar.Enabled = true;
                btnTIAba3Consultar.Enabled = true;
                return;
            }
            else
            {
                //Envia a configuração de IP
                dllREP.REP_Rede_Simples(ip, 1, ipRep, maskRep, ref resultado);
                // Caso de ERRO. 204 é quando o REP não responde após receber a configuração, isso é errado mas 
                // vamos considera-lo como sucesso, já que o REP não está enviando retorno (o que causa lentidão pois 
                // a função fica aguardando uma resposta).
                if (resultado != 0 & resultado != 204)
                {
                    int retorno = resultado;
                    dllREP.REP_Conexao_Simples(ip, 0, ref resultado);
                    MessageBox.Show("Falha no envio da configuração");
                    btnTIAba3Cadastrar.Enabled = true;
                    btnTIAba3Consultar.Enabled = true;
                }
                else //Caso de Sucesso
                {
                    dllREP.REP_Conexao_Simples(ip, 0, ref resultado);
                    txtTIAba3IPREP.Text = "";
                    txtTIAba3MascaraRede.Text = "";
                    txtTIAba3NomeREP.Text = "";
                    txtTIAba3NS.Text = "";
                    MessageBox.Show("Configuração enviada.");
                }
            }
        }

        //******************************************************************************/
        // private: txtTIAba3Cadastrar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 19/07/2011
        // Descrição : Habilita os botões para envio e recebimento de configurtação de
        //             IP do REP
        // Retorno: void
        //
        //******************************************************************************            
        private void cbTIAba3REPsCadastrados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTIAba3REPsCadastrados.Text != "" & txtTIAba3IPREP.Text != "" & txtTIAba3MascaraRede.Text != "")
                btnTIAba3Cadastrar.Enabled = true;
            else
                btnTIAba3Cadastrar.Enabled = false;
            btnTIAba3Consultar.Enabled = true;
        }

        //******************************************************************************/
        // private: txtTIAba3Consultar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 20/07/2011
        // Descrição : Recebe a configuração de IP do REP
        // Retorno: void
        //
        //******************************************************************************            
        private void btnTIAba3Consultar_Click(object sender, EventArgs e)
        {
            byte[] ipRep = new byte[16];
            byte[] maskRep = new byte[16];
            int resultado = 0;
            byte[] numeroSerie = new byte[16];
            byte[] nomeTerminal = new byte[20];

            string path = Application.StartupPath + "\\REPCad.txt";
            string ip = trataRep.consultaTxt(path, 2, cbTIAba3REPsCadastrados.Text, 0);
            trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
            System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
            //Conecta ao REP
            try
            {
                dllREP.REP_Conexao_Simples(ip, 1, ref resultado);
                if (resultado != 0 & resultado != 10005)
                {
                    dllREP.REP_Conexao_Simples(ip, 0, ref resultado);
                    MessageBox.Show("Não foi possivel conectar ao " + cbTIAba3REPsCadastrados.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    //Envia a configuração de IP para o REP.
                    dllREP.REP_Rede_Simples(ip, 0, ipRep, maskRep, ref resultado);
                    //Recebe o nome do terminal.
                    dllREP.REP_InfoTerminal(ip, nomeTerminal, ref resultado);
                    //Recebe o numero de Série;
                    dllREP.REP_LeNSR(ip, numeroSerie , ref resultado);

                    string numSerieFormatada = Encoding.ASCII.GetString(numeroSerie);
                    string nomeTerminalFormatado = Encoding.ASCII.GetString(nomeTerminal);

                    if (resultado != 0)//Caso de ERRO
                    {
                        int retorno = resultado;
                        dllREP.REP_Conexao_Simples(ip, 0, ref resultado);
                        MessageBox.Show(retornoErro.retornoREP(retorno));
                        txtTIAba3IPREP.Text = "";
                        txtTIAba3MascaraRede.Text = "";
                        txtTIAba3NS.Text = "";
                        txtTIAba3NomeREP.Text = "";


                    }
                    else //Caso de Sucesso
                    {
                        dllREP.REP_Conexao_Simples(ip, 0, ref resultado);
                        txtTIAba3IPREP.Text = iso_8859_1.GetString(ipRep);
                        txtTIAba3MascaraRede.Text = iso_8859_1.GetString(maskRep);
                        txtTIAba3NS.Text = numSerieFormatada;
                        txtTIAba3NomeREP.Text = nomeTerminalFormatado;
                    }
                }
            }
            catch
            {
            }
        }

        //******************************************************************************/
        // private: txtTIAba3IPREP                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 05/08/2011
        // Descrição : Verifica se os campos estão preenchidos e habilita os botao de 
        //              cadastro
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba3IPREP_TextChanged(object sender, EventArgs e)
        {
            if (cbTIAba3REPsCadastrados.Text != "" & txtTIAba3IPREP.Text != "" & txtTIAba3MascaraRede.Text != "")
                btnTIAba3Cadastrar.Enabled = true;
            else
                btnTIAba3Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba3MascaraRede                                               *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 05/08/2011
        // Descrição : Verifica se os campos estão preenchidos e habilita os botao de 
        //              cadastro
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba3MascaraRede_TextChanged(object sender, EventArgs e)
        {
            if (cbTIAba3REPsCadastrados.Text != "" & txtTIAba3IPREP.Text != "" & txtTIAba3MascaraRede.Text != "")
                btnTIAba3Cadastrar.Enabled = true;
            else
                btnTIAba3Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba3PortaREP                                                  *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 05/08/2011
        // Descrição : Verifica se os campos estão preenchidos e habilita os botao de 
        //              cadastro
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba3PortaREP_TextChanged(object sender, EventArgs e)
        {
            if (cbTIAba3REPsCadastrados.Text != "" & txtTIAba3IPREP.Text != "" & txtTIAba3MascaraRede.Text != "")
                btnTIAba3Cadastrar.Enabled = true;
            else
                btnTIAba3Cadastrar.Enabled = false;
        }

        //******************************************************************************/
        // private: cbRHAba2REP2                                                       *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 05/08/2011
        // Descrição : Habilita os botçoes enviar e receber        
        // Retorno: void
        //
        //******************************************************************************
        private void cbRHAba2REP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRHAba2Enviar.Enabled = true;
            btnRHAba2Receber.Enabled = true;

        }

        //******************************************************************************/
        // private: btnRHAba2Abrir2                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 05/08/2011
        // Descrição : Abre tela para selecionar o caminho        
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba2Abrir2_Click(object sender, EventArgs e)
        {
            //Abre o diretorio 
            FolderBrowserDialog pasta = new FolderBrowserDialog();
            if (pasta.ShowDialog() == DialogResult.OK)
            {
                txtRHAba2Caminho.Text = pasta.SelectedPath;
                txtRHAba2Caminho.SelectionStart = txtRHAba2Caminho.Text.Length + 1;
                stPath.Default.pathSaveFunc = pasta.SelectedPath;
                stPath.Default.Save();
            }
        }

        //******************************************************************************/
        // private: txtTIAba3Consultar                                                 *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/08/2011
        // Descrição : Recebe o arquivo contendo as informações de cadastro de funcionários
        // Retorno: void
        //
        //******************************************************************************          
        private void btnRHAba2Receber_Click(object sender, EventArgs e)
        {
            try
            {
                //Variavel para receber a resposta da DLL sobre o resultado do comando.
                int resultado = 0;
                //Caminho onde está salvo o cadastro de esquipamentos
                string path = Application.StartupPath + "\\REPCad.txt";
                //Recebe o IP cadastrado no banco de dados, informando o caminho onde fica os REPs 
                // cadastrado e o nome do REP que deseja.
                string ip = trataRep.consultaTxt(path, 2, cbRHAba2REP2.Text, 0);
                //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                //Desabilita os botões enquando recebe o arquivo
                btnRHAba2Enviar.Enabled = false;
                btnRHAba2Receber.Enabled = false;
                //Conecta ao REP

                //Se não conectar mostra erro
                if (envioDll.ConectaREP(ip, 1) != 0)//resultado != 0 & resultado != 10005)
                {
                    //dllREP.REP_Conexao(ip, porta, 0, senha, ref resultado);
                    envioDll.ConectaREP(ip, 0);
                    MessageBox.Show("Não foi possivel conectar ao " + cbRHAba2REP2.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnRHAba2Enviar.Enabled = true;
                    btnRHAba2Receber.Enabled = true;
                    return;
                }
                else
                {
                    lbStatus.ForeColor = Color.Black;
                    //Iniciar a Thread
                    lbStatus.Text = "Recebendo cadastros, por favor aguarde "; //Texto que será apresentado                                        


                    //Se recebido com sucesso mostra a mensagem de quantos funcionários foram encontrados e habilita os botões
                    resultado = envioDll.LeCadastro(ip, 4, "", txtRHAba2Caminho.Text + "\\funcionario.txt");

                    //dllREP.REP_Conexao(ip, porta, 0, senha, ref resultado);
                    envioDll.ConectaREP(ip, 0);
                    //MessageBox.Show("Recebido " + encontrados.ToString() + " funcionário(s) com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnRHAba2Enviar.Enabled = true;
                    btnRHAba2Receber.Enabled = true;

                    //Mostra o status
                    lbStatus.Text = ""; //Texto que será apresentado                                        
                    lbStatus.Update();
                    if (resultado == 0)
                        MessageBox.Show("Recebido com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Senão mostra erro
                    else
                        MessageBox.Show("Não foi possivel receber os funcionários. " + retornoErro.retornoREP(resultado), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                lbStatus.Text = ""; //Texto que será apresentado                                                                                                      

                MessageBox.Show("Ocorreu uma falha ao receber", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnRHAba2Enviar.Enabled = true;
                btnRHAba2Receber.Enabled = true;
            }
        }

        //******************************************************************************/
        // private: txtRHAba2Nome2                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/08/2011
        // Descrição : Verifica se os requisitos foram preenchidos habilitando o botão 
        //             adicionar
        // Retorno: void
        //
        //******************************************************************************         
        private void txtRHAba2Nome2_TextChanged(object sender, EventArgs e)
        {
            if (txtRHAba2Nome2.Text != "" & txtRHAba2PIS2.Text != "" & (txtRHAba2CodBarras2.Text != "" ||
                txtRHAba2Teclado2.Text != "" || txtRHAba2RFID2.Text != ""))
                btnRHAba2Adicionar.Enabled = true;
            else
                btnRHAba2Adicionar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba2PIS2                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/08/2011
        // Descrição : Verifica se os requisitos foram preenchidos habilitando o botão 
        //             adicionar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba2PIS2_TextChanged(object sender, EventArgs e)
        {
            if (txtRHAba2Nome2.Text != "" & txtRHAba2PIS2.Text != "" & (txtRHAba2CodBarras2.Text != "" ||
                txtRHAba2Teclado2.Text != "" || txtRHAba2RFID2.Text != ""))
                btnRHAba2Adicionar.Enabled = true;
            else
                btnRHAba2Adicionar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba2Grupo2                                                    *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/08/2011
        // Descrição : Verifica se os requisitos foram preenchidos habilitando o botão 
        //             adicionar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba2Grupo2_TextChanged(object sender, EventArgs e)
        {
            if (txtRHAba2Nome2.Text != "" & txtRHAba2PIS2.Text != "" & (txtRHAba2CodBarras2.Text != "" ||
                txtRHAba2Teclado2.Text != "" || txtRHAba2RFID2.Text != ""))
                btnRHAba2Adicionar.Enabled = true;
            else
                btnRHAba2Adicionar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba2CodBarras2                                                *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/08/2011
        // Descrição : Verifica se os requisitos foram preenchidos habilitando o botão 
        //             adicionar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba2CodBarras2_TextChanged(object sender, EventArgs e)
        {
            if (txtRHAba2Nome2.Text != "" & txtRHAba2PIS2.Text != "" & (txtRHAba2CodBarras2.Text != "" ||
                txtRHAba2Teclado2.Text != "" || txtRHAba2RFID2.Text != ""))
                btnRHAba2Adicionar.Enabled = true;
            else
                btnRHAba2Adicionar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba2Teclado2                                                  *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/08/2011
        // Descrição : Verifica se os requisitos foram preenchidos habilitando o botão 
        //             adicionar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba2Teclado2_TextChanged(object sender, EventArgs e)
        {
            if (txtRHAba2Nome2.Text != "" & txtRHAba2PIS2.Text != "" & (txtRHAba2CodBarras2.Text != "" ||
                txtRHAba2Teclado2.Text != "" || txtRHAba2RFID2.Text != ""))
                btnRHAba2Adicionar.Enabled = true;
            else
                btnRHAba2Adicionar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba2Biometria2                                                *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/08/2011
        // Descrição : Verifica se os requisitos foram preenchidos habilitando o botão 
        //             adicionar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba2Biometria2_TextChanged(object sender, EventArgs e)
        {
            if (txtRHAba2Nome2.Text != "" & txtRHAba2PIS2.Text != "" & (txtRHAba2CodBarras2.Text != "" ||
                txtRHAba2Teclado2.Text != "" || txtRHAba2RFID2.Text != ""))
                btnRHAba2Adicionar.Enabled = true;
            else
                btnRHAba2Adicionar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba2RFID2                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/08/2011
        // Descrição : Verifica se os requisitos foram preenchidos habilitando o botão 
        //             adicionar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba2RFID2_TextChanged(object sender, EventArgs e)
        {
            if (txtRHAba2Nome2.Text != "" & txtRHAba2PIS2.Text != "" & (txtRHAba2CodBarras2.Text != "" ||
                txtRHAba2Teclado2.Text != "" || txtRHAba2RFID2.Text != ""))
                btnRHAba2Adicionar.Enabled = true;
            else
                btnRHAba2Adicionar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba1RazaoSocial2                                              *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/08/2011
        // Descrição : Verifica se os requisitos foram preenchidos habilitando o botão 
        //             adicionar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba1RazaoSocial_TextChanged(object sender, EventArgs e)
        {
            if (cbRHAba1NomeREP1.Text != "" & txtRHAba1RazaoSocial.Text != "" &
                txtRHAba1CNPJCPF.Text != "" & txtRHAba1Endereco.Text != "")
            {
                btnRHAba1Cadastrar.Enabled = true;
                btnRHAba1Alterar.Enabled = true;
            }
            else
            {
                btnRHAba1Cadastrar.Enabled = false;
                btnRHAba1Alterar.Enabled = false;
            }
        }

        //******************************************************************************/
        // private: txtRHAba1NomeREP1                                                  *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/08/2011
        // Descrição : Verifica se os requisitos foram preenchidos habilitando o botão 
        //             adicionar
        // Retorno: void
        //
        //******************************************************************************
        private void cbRHAba1NomeREP1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRHAba1NomeREP1.Text != "")
            {
                btnRHAba1Consultar.Enabled = true;
                if (txtRHAba1RazaoSocial.Text != "" &
                txtRHAba1CNPJCPF.Text != "" & txtRHAba1Endereco.Text != "")
                {
                    btnRHAba1Cadastrar.Enabled = true;
                    btnRHAba1Alterar.Enabled = true;
                }
                else
                {
                    btnRHAba1Cadastrar.Enabled = false;
                    btnRHAba1Alterar.Enabled = false;
                }
            }
            else
                btnRHAba1Consultar.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba1CNPJCPF                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/08/2011
        // Descrição : Verifica se os requisitos foram preenchidos habilitando o botão 
        //             adicionar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba1CNPJCPF_TextChanged(object sender, EventArgs e)
        {
            if (cbRHAba1NomeREP1.Text != "" & txtRHAba1RazaoSocial.Text != "" &
                txtRHAba1CNPJCPF.Text != "" & txtRHAba1Endereco.Text != "")
            {
                btnRHAba1Cadastrar.Enabled = true;
                btnRHAba1Alterar.Enabled = true;
            }
            else
            {
                btnRHAba1Cadastrar.Enabled = false;
                btnRHAba1Alterar.Enabled = false;
            }

        }

        //******************************************************************************/
        // private: txtRHAba1Endereco                                                  *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 08/08/2011
        // Descrição : Verifica se os requisitos foram preenchidos habilitando o botão 
        //             adicionar
        // Retorno: void
        //
        //******************************************************************************
        private void txtRHAba1Endereco_TextChanged(object sender, EventArgs e)
        {
            if (cbRHAba1NomeREP1.Text != "" & txtRHAba1RazaoSocial.Text != "" &
                 txtRHAba1CNPJCPF.Text != "" & txtRHAba1Endereco.Text != "")
            {
                btnRHAba1Cadastrar.Enabled = true;
                btnRHAba1Alterar.Enabled = true;
            }

            else
            {
                btnRHAba1Cadastrar.Enabled = false;
                btnRHAba1Alterar.Enabled = false;
            }
        }

        //******************************************************************************/
        // public: ColetaRegistro                                                      *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 12/08/2011
        // Descrição : Recebe o arquivo contendo as informações de registro
        // Retorno: void
        //
        //******************************************************************************         
        public void ColetaRegistro()
        {
            string path = Application.StartupPath;
            string ip;

            ip = trataRep.consultaTxt(path + "\\REPCad.txt", 2, nomeIp, 0);
            //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
            System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");            

            try
            {
                if (envioDll.ConectaREP(ip, 1) == 1)
                {
                    MessageBox.Show("REP não encontrado.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    lbStatus.Text = "Coletando registro, por favor aguarde.";
                     lbStatus.Update();
                    //Recebe o arquivo do REP onde contem todos os registros (geral) existentes no REP                                        
                    //Caso receba com sucesso inicia a filtragem para obter somente os Registros de ponto
                    int resultado = envioDll.LeCadastro(ip, 5, "1", path + "\\regREP.txt");
                    if (resultado == 0)
                    {
                        //Indica ao Filestream o caminho do arquivo coletado
                        FileStream abreArquivoTxt = new FileStream(path + "\\regREP.txt", FileMode.Open, FileAccess.Read);
                        //Abre para leitura no padrão ISO 8859/1 - para não ocorrer erro com acentuação
                        StreamReader sr = new StreamReader(abreArquivoTxt, iso_8859_1);
                        //Cria o arquivo registro.txt para adicionar somente os registros de ponto encontrado
                        FileStream escreveArquivoTxt = new FileStream(pathArquivo + "\\registro.txt", FileMode.OpenOrCreate, FileAccess.Write);
                        //Abre o arquivo para escrita
                        StreamWriter reg = new StreamWriter(escreveArquivoTxt, iso_8859_1);
                        string line = "";

                        //Le linha a linha
                        while ((line = sr.ReadLine()) != null)
                        {
                            //Separa a linha em linhas, cada linha com cada informação encontrada até o ';'
                            string[] linhaReg = line.Split(';');
                            if (linhaReg[1] == "3")
                            {
                                //Reescreve o arquivo
                                reg.WriteLine(line);
                            }
                        }
                        //Fecha os arquivos
                        reg.Close();
                        sr.Close();
                        //Remove o arquivo coletado, pois nao será mais utilizado
                        File.Delete(path + "\\regREP.txt");
                        //Fecha a conexão com o REP
                        envioDll.ConectaREP(ip, 0);
                        //Informa que os arquivos foram coletados                        
                        MessageBox.Show("Arquivo coletado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lbStatus.Text = "";
                        lbStatus.Update();
                    }
                    else
                    {
                        //Fecha a conexão com o REP e mostra que ocorreu falha
                        envioDll.ConectaREP(ip, 0);
                        MessageBox.Show("Falha ao coletar arquivo. " + retornoErro.retornoREP(resultado), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        lbStatus.Text = "";
                        lbStatus.Update();
                    }
                }
            }
            catch
            {
                envioDll.ConectaREP(ip, 0);
                MessageBox.Show("Ocorreu um erro na tentativa de coletar os dados.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lbStatus.Text = "";
                lbStatus.Update();
            }
        }

        //******************************************************************************/
        // delegate: SetControlValueCallback                                           *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 12/08/2011
        // Descrição : delegate para utilização da thread com parametros
        // Retorno: void
        //
        //******************************************************************************     
        delegate void SetControlValueCallback(Control oControl, string propName, object propValue);

        //******************************************************************************/
        // private: SetControlPropertyValue                                            *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 12/08/2011
        // Descrição : Seta as informações no controle solicitado
        // Retorno: void
        //
        //****************************************************************************** 
        private void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[] { oControl, propName, propValue });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if (p.Name.ToUpper() == propName.ToUpper())
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }

        //******************************************************************************/
        // private: ThreadTarefa                                                       *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 12/08/2011
        // Descrição : Ativada como thread para atualizar a label mostrando o status
        // Retorno: void
        //
        //****************************************************************************** 
        private void ThreadTarefa()
        {
            string textoUsado = textoAtual;
            int contador = 0;
            if (textoAtual != "")
            {
                while (continuaTrd == 1)
                {
                    if (contador < 10)
                    {
                        textoUsado += ".";
                        contador++;
                    }
                    else
                    {
                        textoUsado = textoAtual;
                        contador = 0;
                    }

                    //esta linha contorna o problema do erro da thread
                    SetControlPropertyValue(lbStatus, "Text", textoUsado);

                    Thread.Sleep(1000);
                }
            }
            else
                SetControlPropertyValue(lbStatus, "Text", textoAtual);
        }

        //******************************************************************************/
        // private: btnRHAba4Abrir                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 12/08/2011
        // Descrição : Abre tela para selecionar uma pasta para receber  os registros
        // Retorno: void
        //
        //****************************************************************************** 
        private void btnRHAba4Abrir_Click(object sender, EventArgs e)
        {
            //Abre a tela para escolher a pasta onde irá salvar
            FolderBrowserDialog pasta = new FolderBrowserDialog();
            //Seleciona a pasta
            if (pasta.ShowDialog() == DialogResult.OK)
            {
                //Adiciona o caminho da pasta ao text
                txtRHAba4SalvarEm.Text = pasta.SelectedPath;
                txtRHAba4SalvarEm.SelectionStart = txtRHAba4SalvarEm.Text.Length + 1;
                stPath.Default.pathFile = pasta.SelectedPath;
                stPath.Default.Save();
            }
        }

        //******************************************************************************/
        // private: btnRHAba4SalvarEm                                                  *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 25/08/2011
        // Descrição : Habilita o botão para importar os registros do rep
        // Retorno: void
        //
        //****************************************************************************** 
        private void txtRHAba4SalvarEm_TextChanged(object sender, EventArgs e)
        {
            if (txtRHAba4SalvarEm.Text != "" & cbRHAba3REP.Text != "")
                btnRHAba3Coletar.Enabled = true;
            else
                btnRHAba3Coletar.Enabled = false;
        }

        //******************************************************************************/
        // private: btnRHAba4Arquivo                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 25/08/2011
        // Descrição : Habilita o botão para converter os registros do rep
        // Retorno: void
        //
        //****************************************************************************** 
        private void txtRHAba4Arquivo_TextChanged(object sender, EventArgs e)
        {
            if (txtRHAba4Arquivo.Text != "" & txtRHAba4SalvarEm2.Text != "")
                btnRHAba3Converter.Enabled = true;
            else
                btnRHAba3Converter.Enabled = false;
        }

        //******************************************************************************/
        // private: btnRHAba4SalvarEm2                                                  *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 25/08/2011
        // Descrição : Habilita o botão para converter os registros do rep
        // Retorno: void
        //
        //****************************************************************************** 
        private void txtRHAba4SalvarEm2_TextChanged(object sender, EventArgs e)
        {
            if (txtRHAba4Arquivo.Text != "" & txtRHAba4SalvarEm2.Text != "")
                btnRHAba3Converter.Enabled = true;
            else
                btnRHAba3Converter.Enabled = false;
        }

        //******************************************************************************/
        // private: txtRHAba2Grupo                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 25/08/2011
        // Descrição : Verifica se o valor digitado é numerico
        // Retorno: void
        //
        //****************************************************************************** 
        private void txtRHAba2Grupo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // somente numeros e backspace
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        //******************************************************************************/
        // private: txtRHAba2Grupo                                                     *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 25/08/2011
        // Descrição : Verifica o estado do campo para habilitar os botões.
        // Retorno: void
        //
        //****************************************************************************** 
        private void txtRHAba2Grupo_TextChanged(object sender, EventArgs e)
        {
            if (cbRHAba2REP.Text != "")
            {
                if (txtRHAba2Nome.Text != "" & txtRHAba2PIS.Text != "" & (txtRHAba2CodigoBarras.Text != "" ||
                    txtRHAba2Teclado.Text != "" || txtRHAba2RFID.Text != ""))
                {
                    btnRHAba2Alterar.Enabled = true;
                    btnRHAba2Cadastrar.Enabled = true;
                }
                else
                {
                    btnRHAba2Alterar.Enabled = false;
                    btnRHAba2Cadastrar.Enabled = false;
                }
            }
        }

        //******************************************************************************/
        // public: atualizaTempoREP                                                    *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 29/08/2011
        // Descrição : Atualiza data e hora do REP1.
        // Retorno: int
        //
        //****************************************************************************** 
        public int atualizaTempoREP()
        {
            int resultado = 0;
            string anoCompleto = dtTIAba2DataREP.Value.Year.ToString();
            byte diaSemana = Convert.ToByte(dtTIAba2DataREP.Value.DayOfWeek);
            diaSemana++;

            //Recebe o ip do REP
            string path = Application.StartupPath;
            string ip = trataRep.consultaTxt(path + "\\REPCad.txt", 2, cbTIAba2REP.Text, 0);
            //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
            //Conecta ao REP            
            if (envioDll.ConectaREP(ip, 1) != 0)
            {
                envioDll.ConectaREP(ip, 0);
                return 1;
            }
            //Envio para o REP
            resultado = envioDll.enviaTempoREP(ip, Convert.ToByte(dtTIAba2DataREP.Value.Day), Convert.ToByte(dtTIAba2DataREP.Value.Month),
                Convert.ToInt32(anoCompleto.Substring(2, 2)), Convert.ToByte(dtTIAba2HoraREP.Value.Hour), Convert.ToByte(dtTIAba2HoraREP.Value.Minute),
                Convert.ToByte(dtTIAba2HoraREP.Value.Second), diaSemana);
            envioDll.ConectaREP(ip, 0);
            return resultado;
        }
            
        //******************************************************************************/
        // public: adicionaREP                                                         *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 29/08/2011
        // Descrição : Habilita ou desabilita os botões do programa
        // Retorno: byte
        //
        //******************************************************************************      
        private void habilitaBotoes(bool onOff)
        {
            btnRHAba3Coletar.Enabled = onOff;
            btnRHAba3Converter.Enabled = onOff;
            btnRHAba1Cadastrar.Enabled = onOff;
            btnRHAba1Consultar.Enabled = onOff;
            btnRHAba2Adicionar.Enabled = onOff;
            btnRHAba2Enviar.Enabled = onOff;
            btnRHAba2Receber.Enabled = onOff;
            btnRHAba5Alterar.Enabled = onOff;
            btnRHAba6Cadastrar.Enabled = onOff;
            btnRHAba6Consultar.Enabled = onOff;
            btnRHAba6Excluir.Enabled = onOff;
            btnRHAba6Alterar.Enabled = onOff;
            cbRHAba2REP2.Enabled = onOff;
        }

        //******************************************************************************/
        // public: GravaFuncionarios                                                   *
        //******************************************************************************/
        //
        // Autor: Ana Lúcia S. Melo
        // Data de Criação : 30/09/2011
        // Descrição : Grava uma lista de funcionários no REP (Um a Um)
        // Retorno: void
        //
        //******************************************************************************      
        public void GravarFuncionarios(string ip, string arquivoFuncionarios)
        {
            try
            {
                //criando a codificação para receber caracteres especiais
                System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");
                int ok = 0;
                // byte[] Nome = new byte[52];
                string Nome;
                string Pis = "";
                //byte[] Contactless = new byte[16];
                string Contactless;
                //byte[] CodBarras = new byte[16];
                string CodBarras;
                //byte[] Teclado = new byte[8];
                string Teclado;
                //byte[] Biometria = new byte[8];
                string Biometria;
                byte Grupo = 0;

                int resultado = 0;
                int i = 0, contadorErro = 0, contadorSucesso = 0;

                //Arquivo que contém os funcionários 
                //  string arquivoFuncionarios = @"C:\Users\usuario\Desktop\10000 empregados.txt";

                //verificando se o arquivo com os funcionários existe                
                bool fileExists = System.IO.File.Exists(arquivoFuncionarios);

                //Arquivo para o Log
                string arquivoLog = Application.StartupPath + "\\LogFuncionarios.txt";

                bool fileExistsLog = System.IO.File.Exists(arquivoLog);

                //Se o arquivo de Log não existe, cria
                if (!fileExistsLog)
                {
                    System.IO.File.Create(arquivoLog).Close();
                }

                //Se arquivo de funcionários existe, executa a gravação de vários funcionários
                if (fileExists)
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(arquivoFuncionarios, iso_8859_1);

                    string lineAux = "";


                    dllREP.REP_Conexao_Simples(
                                       ip,
                                       1,//conectar
                                       ref resultado);

                    if (resultado != 0 && resultado != 10005) //se conectou ou já está conectado
                    {
                        lbStatus.Visible = true;
                        lbStatus.Text = "Erro - Conexão!";
                        lbStatus.ForeColor = Color.Red;
                        ok = 1;
                        //lbStatus.Refresh();
                        //labelDebug.BackColor = Color.Red;
                    }
                    else
                    {
                        System.IO.TextWriter arquivo = System.IO.File.AppendText(arquivoLog);
                        if (ckbRHAba2GeraLog.Checked)
                            arquivo.WriteLine("\r\n" + "Relatório de envio. Data de criação: " + DateTime.Now.ToString());

                        lbStatus.Visible = true;
                        lbStatus.Text = "Enviando cadastro";
                        lbStatus.ForeColor = Color.Black;
                        lbStatus.Update();
                        try
                        {
                            do
                            {
                                // line contém a linha atual
                                lineAux = sr.ReadLine();

                                if (lineAux != "")
                                {
                                    //divindo a linha com as divisões por ";" em Line
                                    string[] line = lineAux.Split(';');

                                    Nome = line[0].Trim() + '\0';
                                    Pis = line[1].Trim() + '\0';
                                    Contactless = line[2].Trim() + '\0';
                                    CodBarras = line[3].Trim() + '\0';
                                    Teclado = line[4].Trim() + '\0';
                                    Biometria = line[5].Trim() + '\0';
                                    Grupo = 0;

                                    resultado = -1; //iniciando resultado para confirmar que a função foi executada e obteve retorno

                                    dllREP.REP_GravaCadastroFuncionario(
                                                ip,
                                                '0',
                                                Nome,
                                                Pis,
                                                Contactless,
                                                CodBarras,
                                                Teclado,
                                                Biometria,
                                                Grupo,
                                                ref resultado);

                                    if (resultado == 0)
                                    {
                                        if (ckbRHAba2GeraLog.Checked)
                                            arquivo.WriteLine(Pis + " - OK");
                                        contadorSucesso++;
                                        ok = 0;
                                    }
                                    else if (resultado >= 3000 && resultado <= 3031)
                                    {
                                        if (ckbRHAba2GeraLog.Checked)
                                            arquivo.WriteLine(Pis + " - ERRO: " + retornoErro.retornoREP(resultado));
                                        contadorErro++;
                                    }
                                    else
                                    {
                                        if (ckbRHAba2GeraLog.Checked)
                                            arquivo.WriteLine(Pis + " - ERRO ********** finalizar a gravação. " + retornoErro.retornoREP(resultado));
                                        ok = 1;
                                        //Possível erro de comunicação com o REP
                                        break;
                                    }
                                }
                                i++;


                            } while (!sr.EndOfStream);
                            if (ckbRHAba2GeraLog.Checked)
                                arquivo.WriteLine("Cadastro finalizado. Cadastrados com sucesso: " + contadorSucesso.ToString() + ",  Falha no cadastro: " + contadorErro.ToString());
                            arquivo.Close();

                            /*if (ckbRHAba2GeraLog.Checked)
                                System.Diagnostics.Process.Start(Application.StartupPath + "\\LogFuncionarios.txt");
                            Application.DoEvents();*/

                        }
                        catch
                        {
                            lbStatus.Visible = true;
                            lbStatus.Text = "Erro - Exception!";
                            lbStatus.ForeColor = Color.Red;
                            //lbStatus.Refresh();
                            // if (ckbRHAba2GeraLog.Checked)
                            arquivo.Close();
                        }

                        lbStatus.Visible = true;
                        if (ok == 0)
                        {
                            lbStatus.Text = "Fim da Gravação. Enviados: " + contadorSucesso.ToString() + " Não enviados: " + contadorErro.ToString();
                            lbStatus.ForeColor = Color.Green;
                        }
                        else
                        {
                            lbStatus.Text = "Erro - Envio cancelado.";
                            lbStatus.ForeColor = Color.Red;
                        }
                        //lbStatus.Refresh();
                    }
                    if (ckbRHAba2GeraLog.Checked)
                        System.Diagnostics.Process.Start(Application.StartupPath + "\\LogFuncionarios.txt");

                }
                else
                {
                    lbStatus.Visible = true;
                    lbStatus.Text = "ERRO - Arquivo não existe!";
                    lbStatus.ForeColor = Color.Red;
                    //  lbStatus.Refresh();
                }
            }
            catch
            {
                lbStatus.Visible = true;
                lbStatus.Text = "Erro - Exception!";
                lbStatus.ForeColor = Color.Red;
                //  lbStatus.Refresh();
            }
        }

        //******************************************************************************/
        // private: btnRHAba2AbrirBio                                               *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 28/10/2011
        // Descrição : Abre o arquivo a ser enviado.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba2AbrirBio_Click(object sender, EventArgs e)
        {
            //Abre o arquivo 
            //Abre o diretorio 
            FolderBrowserDialog pasta = new FolderBrowserDialog();

            if (pasta.ShowDialog() == DialogResult.OK)
            {
                txtRHAba2Arquivo2.Text = pasta.SelectedPath;
                txtRHAba2Arquivo2.SelectionStart = txtRHAba2Arquivo2.Text.Length + 1;
                stPath.Default.pathSaveBio = pasta.SelectedPath;
                stPath.Default.Save();
            }
        }

        //******************************************************************************/
        // private: btnRHAba2AbrirBio2                                               *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 28/10/2011
        // Descrição : Abre o diretório e informa o caminho da pasta.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba2AbrirBio2_Click(object sender, EventArgs e)
        {
            //Abre o diretorio 
            FolderBrowserDialog pasta = new FolderBrowserDialog();

            if (pasta.ShowDialog() == DialogResult.OK)
            {
                txtRHAba2Caminho2.Text = pasta.SelectedPath;
                txtRHAba2Caminho2.SelectionStart = txtRHAba2Caminho2.Text.Length + 1;
                stPath.Default.pathSaveBio = pasta.SelectedPath;
                stPath.Default.Save();
            }
        }
        //******************************************************************************/
        // private: cbRHAba2REP3                                               *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 28/10/2011
        // Descrição : Seleciona o terminal.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void cbRHAba2REP3_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRHAba2EnviarBio.Enabled = true;
            btnRHAba2ReceberBio.Enabled = true;
            btnRHAba2SalvarBio.Enabled = true;
            btnRHAba2ExcluirBio.Enabled = true;
            btnRHAba2FormataBio.Enabled = true;
        }

        //******************************************************************************/
        // private: btnRHAba2EnviarBio                                               *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 28/10/2011
        // Descrição : Envia a biometria para o terminal.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba2EnviarBio_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath + "\\REPCad.txt";
                //Recebe o IP cadastrado no banco de dados                
                string ip = trataRep.consultaTxt(path, 2, cbRHAba2REP3.Text, 0);
                //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                GravarFingerPasta(ip, txtRHAba2Arquivo2.Text);
            }
            catch
            {
                MessageBox.Show("Ocorreu uma falha ao enviar as biometrias", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //******************************************************************************/
        // private: GravarFingerPasta()                                               *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 29/10/2011
        // Descrição : Seleciona somente a pasta com as biometrias.
        //              
        // Retorno: void
        //
        //******************************************************************************
        public void GravarFingerPasta(string ip, string pastaFinger)
        {
            try
            {
                int ok = 0;
                int resultado = 0;
                int contador = 0;

                //Conecta ao Rep
                dllREP.REP_Conexao_Simples(ip, 1, ref resultado);
                //Verifica a pasta que possui as fingers salvas.
                string[] pasta = Directory.GetFiles(pastaFinger);

                if (resultado != 0 && resultado != 10005) //se conectou ou já está conectado
                {
                    lbStatus.Visible = true;
                    lbStatus.Text = "Erro - Conexão!";
                    lbStatus.ForeColor = Color.Red;
                    ok = 1;
                    MessageBox.Show("Falha na conexão!", "Biometrias", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //lbStatus.Refresh();
                    //labelDebug.BackColor = Color.Red;
                }
                else
                {
                    lbStatus.Visible = true;
                    lbStatus.Text = "Enviando cadastro";
                    lbStatus.ForeColor = Color.Black;
                    lbStatus.Update();
                    try
                    {
                        foreach (string arquivo in pasta)
                        {
                            dllREP.REP_GravaFinger(ip, arquivo, ref resultado);
                            if (resultado == 3031)
                            {
                                MessageBox.Show("PIS não cadastrado!" + arquivo, "Biometrias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                contador--;
                            }

                            contador++;
                        }

                    }
                    catch
                    {
                        lbStatus.Visible = true;
                        lbStatus.Text = "Erro - Exception!";
                        lbStatus.ForeColor = Color.Red;
                    }
                    lbStatus.Visible = true;
                    if (ok == 0)
                    {
                        lbStatus.Text = "Fim da Gravação. Enviados: " + contador.ToString();
                        lbStatus.ForeColor = Color.Green;
                        if(contador == 0)
                            MessageBox.Show("PIS não encontrado!", "Biometrias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        else
                            MessageBox.Show("Biometrias enviadas com sucesso!", "Biometrias", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        lbStatus.Text = "Erro - Envio cancelado.";
                        lbStatus.ForeColor = Color.Red;
                        MessageBox.Show("Envio das Biometrias cancelado!", "Biometrias", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                lbStatus.Visible = true;
                lbStatus.Text = "Erro - Exception!";
                lbStatus.ForeColor = Color.Red;
            }
        }
     
        //******************************************************************************/
        // private: btnRHAba2SalvarBio                                               *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 09/11/2011
        // Descrição : Recebe todas as biometria cadastradas no terminal.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba2ReceberBio_Click(object sender, EventArgs e)
        {
            try
            {
                //Variavel para receber a resposta da DLL sobre o resultado do comando.
                int resultado = 0;

                //Caminho onde está salvo o cadastro de esquipamentos
                string path = Application.StartupPath + "\\REPCad.txt";
                //Recebe a informação do diretorio para mover os arquivos
                DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);
                //Recebe o IP cadastrado no banco de dados, informando o caminho onde fica os REPs cadastrado e o nome do REP que deseja.
                string ip = trataRep.consultaTxt(path, 2, cbRHAba2REP3.Text, 0);
                //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                //Desabilita os botões enquando recebe o arquivo
                btnRHAba2EnviarBio.Enabled = false;
                btnRHAba2ReceberBio.Enabled = false;
                btnRHAba2FormataBio.Enabled = false;

                //Se não conectar mostra erro
                if (envioDll.ConectaREP(ip, 1) != 0)
                {
                    //Conecta no REP
                    envioDll.ConectaREP(ip, 0);
                    MessageBox.Show("Não foi possivel conectar ao " + cbRHAba2REP3.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnRHAba2EnviarBio.Enabled = true;
                    btnRHAba2ReceberBio.Enabled = true;
                    btnRHAba2FormataBio.Enabled = true;
                    return;
                }
                else
                {
                    lbStatus.Visible = true;
                    lbStatus.ForeColor = Color.Black;
                    //Iniciar a Thread
                    lbStatus.Text = "Recebendo cadastros, por favor aguarde "; //Texto que será apresentado                                        

                    //Se recebido com sucesso mostra a mensagem de quantos funcionários foram encontrados e habilita os botões
                    resultado = envioDll.LeFinger(ip, txtRHAba2Caminho2.Text);
                    envioDll.ConectaREP(ip, 0);
     
                    foreach (FileInfo f in dir.GetFiles("*.rec"))
                    {
                        if (File.Exists(txtRHAba2Caminho2.Text + "\\" + f.Name) == false)
                        {
                            File.Move(f.FullName, txtRHAba2Caminho2.Text + "\\" + f.Name);
                        }
                        else if (File.Exists(txtRHAba2Caminho2.Text + "\\" + f.Name) == true)
                        {
                            File.Delete(txtRHAba2Caminho2.Text + "\\" + f.Name);
                        }
                    }
                    foreach (FileInfo f in dir.GetFiles("*.rec"))
                    {
                        if (File.Exists(txtRHAba2Caminho2.Text + "\\" + f.Name) == false)
                        {
                            File.Move(f.FullName, txtRHAba2Caminho2.Text + "\\" + f.Name);
                        }
                    }
                        
                    
                    btnRHAba2EnviarBio.Enabled = true;
                    btnRHAba2ReceberBio.Enabled = true;
                    btnRHAba2FormataBio.Enabled = true;

                    lbStatus.Visible = true;
                    //Mostra o status
                    lbStatus.Text = ""; //Texto que será apresentado                                        
                    lbStatus.Update();
                    if (resultado == 0)
                        MessageBox.Show("Recebido com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Senão mostra erro
                    else
                        MessageBox.Show("Não foi possivel receber as biometrias cadastradas. " + retornoErro.retornoREP(resultado), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                lbStatus.Text = ""; //Texto que será apresentado                                                                                                      

                MessageBox.Show("Ocorreu uma falha ao receber as biometrias", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnRHAba2EnviarBio.Enabled = true;
                btnRHAba2ReceberBio.Enabled = true;
                btnRHAba2FormataBio.Enabled = true;
            }
        }

        //******************************************************************************/
        // private: btnRHAba2SalvarBio                                               *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 09/11/2011
        // Descrição : Recebe a biometria de um determinado funcionário pelo PIS.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba2SalvarBio_Click(object sender, EventArgs e)
        {
            try
            {
                //Variavel para receber a resposta da DLL sobre o resultado do comando.
                int resultado = 0;
                //Caminho onde está salvo o cadastro de esquipamentos
                string path = Application.StartupPath + "\\REPCad.txt";
                //Recebe a informação do diretorio para mover os arquivos
                DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);
                //Recebe o IP cadastrado no banco de dados, informando o caminho onde fica os REPs cadastrado e o nome do REP que deseja.
                string ip = trataRep.consultaTxt(path, 2, cbRHAba2REP3.Text, 0);
                //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                //Se não conectar mostra erro
                if (envioDll.ConectaREP(ip, 1) != 0)
                {
                    //Conecta no REP
                    envioDll.ConectaREP(ip, 0);
                    MessageBox.Show("Não foi possivel conectar ao " + cbRHAba2REP3.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnRHAba2EnviarBio.Enabled = true;
                    btnRHAba2ReceberBio.Enabled = true;
                    btnRHAba2FormataBio.Enabled = true;
                    return;
                }
                else
                {
                    lbStatus.Visible = true;
                    lbStatus.ForeColor = Color.Black;
                    //Iniciar a Thread
                    lbStatus.Text = "Recebendo cadastro, por favor aguarde "; //Texto que será apresentado                                        

                    //Se recebido com sucesso mostra a mensagem de quantos funcionários foram encontrados e habilita os botões
                    resultado = envioDll.LeFingerID(ip, txtRHAba2PIS3.Text);
                    envioDll.ConectaREP(ip, 0);
                    foreach (FileInfo f in dir.GetFiles("*.rec"))
                    {
                        if (File.Exists(txtRHAba2Caminho2.Text + "\\" + f.Name) == false)
                        {
                            File.Move(f.FullName, txtRHAba2Caminho2.Text + "\\" + f.Name);
                        }
                        else if (File.Exists(txtRHAba2Caminho2.Text + "\\" + f.Name) == true)
                        {
                            File.Delete(txtRHAba2Caminho2.Text + "\\" + f.Name);
                        }
                    }
                    foreach (FileInfo f in dir.GetFiles("*.rec"))
                    {
                        if (File.Exists(txtRHAba2Caminho2.Text + "\\" + f.Name) == false)
                        {
                            File.Move(f.FullName, txtRHAba2Caminho2.Text + "\\" + f.Name);
                        }
                    }

                    btnRHAba2EnviarBio.Enabled = true;
                    btnRHAba2ReceberBio.Enabled = true;

                    lbStatus.Visible = true;
                    //Mostra o status
                    lbStatus.Text = ""; //Texto que será apresentado                                        
                    lbStatus.Update();
                    txtRHAba2PIS3.Clear();
                    if (resultado == 0)
                        MessageBox.Show("Recebido com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Senão mostra erro
                    else
                        MessageBox.Show("Não foi possivel receber a biometria cadastrada. " + retornoErro.retornoREP(resultado), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch
            {
                lbStatus.Text = ""; //Texto que será apresentado                                                                                                      

                MessageBox.Show("Ocorreu uma falha ao receber as biometrias", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnRHAba2EnviarBio.Enabled = true;
                btnRHAba2ReceberBio.Enabled = true;
            }
            
        }

        //******************************************************************************/
        // private: btnRHAba2FormataBio                                                *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 09/11/2011
        // Descrição : Formata todas as biometria cadastrada no terminal.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba2FormataBio_Click(object sender, EventArgs e)
        {
            try
            {
                //Variavel para receber a resposta da DLL sobre o resultado do comando.
                int resultado = 0;

                //Caminho onde está salvo o cadastro de esquipamentos
                string path = Application.StartupPath + "\\REPCad.txt";
                //Recebe o IP cadastrado no banco de dados, informando o caminho onde fica os REPs cadastrado e o nome do REP que deseja.
                string ip = trataRep.consultaTxt(path, 2, cbRHAba2REP3.Text, 0);
                //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                //Desabilita os botões enquando formata os arquivos das fingers.
                btnRHAba2FormataBio.Enabled = false;

                //Se não conectar mostra erro
                if (envioDll.ConectaREP(ip, 1) != 0)
                {
                    //Conecta no REP
                    envioDll.ConectaREP(ip, 0);
                    MessageBox.Show("Não foi possivel conectar ao " + cbRHAba2REP3.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnRHAba2FormataBio.Enabled = false;
                    return;
                }
                else
                {
                    lbStatus.Visible = true;
                    lbStatus.ForeColor = Color.Black;
                    //Iniciar a Thread
                    lbStatus.Text = "Formatando Biometrias, por favor aguarde "; //Texto que será apresentado                                        

                    //Se recebido com sucesso mostra a mensagem de quantos funcionários foram encontrados e habilita os botões
                    dllREP.REP_FormataFinger(ip, ref resultado);
                    envioDll.ConectaREP(ip, 0);

                    btnRHAba2EnviarBio.Enabled = true;
                    btnRHAba2ReceberBio.Enabled = true;
                    btnRHAba2FormataBio.Enabled = true;

                    lbStatus.Visible = true;
                    //Mostra o status
                    lbStatus.Text = ""; //Texto que será apresentado                                        
                    lbStatus.Update();
                    if (resultado == 0)
                        MessageBox.Show("Biometrias formatadas com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Senão mostra erro
                    else
                        MessageBox.Show("Não foi possivel formatar as biometrias cadastradas. " + retornoErro.retornoREP(resultado), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                lbStatus.Text = ""; //Texto que será apresentado                                                                                                      

                MessageBox.Show("Ocorreu uma falha ao formatar as biometrias.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnRHAba2EnviarBio.Enabled = true;
                btnRHAba2ReceberBio.Enabled = true;
                btnRHAba2FormataBio.Enabled = true;

            }
        }

        //******************************************************************************/
        // private: btnRHAba2ExcluirBio                                                *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 09/11/2011
        // Descrição : Remove a biometria de uma determinado funcionário pelo PIS.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void btnRHAba2ExcluirBio_Click(object sender, EventArgs e)
        {
            try
            {
                //Variavel para receber a resposta da DLL sobre o resultado do comando.
                int resultado = 0;
                //Caminho onde está salvo o cadastro de esquipamentos
                string path = Application.StartupPath + "\\REPCad.txt";
                //Recebe a informação do diretorio para mover os arquivos
                DirectoryInfo dir = new DirectoryInfo(Application.StartupPath + "\\Gertec\\Biometria\\");
                //Recebe o IP cadastrado no banco de dados, informando o caminho onde fica os REPs cadastrado e o nome do REP que deseja.
                string ip = trataRep.consultaTxt(path, 2, cbRHAba2REP3.Text, 0);
                //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                //Se não conectar mostra erro
                if (envioDll.ConectaREP(ip, 1) != 0)
                {
                    //Conecta no REP
                    envioDll.ConectaREP(ip, 0);
                    MessageBox.Show("Não foi possivel conectar ao " + cbRHAba2REP3.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnRHAba2EnviarBio.Enabled = true;
                    btnRHAba2ReceberBio.Enabled = true;
                    btnRHAba2FormataBio.Enabled = true;
                    btnRHAba2ExcluirBio.Enabled = true;

                    return;
                }
                else
                {
                    lbStatus.Visible = true;
                    lbStatus.ForeColor = Color.Black;
                    //Iniciar a Thread
                    lbStatus.Text = "Excluindo cadastro, por favor aguarde "; //Texto que será apresentado                                        

                    //Se recebido com sucesso mostra a mensagem de quantos funcionários foram encontrados e habilita os botões
                    resultado = envioDll.RemoverFingerID(ip, txtRHAba2PIS3.Text);
                    envioDll.ConectaREP(ip, 0);
         
                    btnRHAba2EnviarBio.Enabled = true;
                    btnRHAba2ReceberBio.Enabled = true;
                    btnRHAba2ExcluirBio.Enabled = true;
                    btnRHAba2FormataBio.Enabled = true;

                    lbStatus.Visible = true;
                    //Mostra o status
                    lbStatus.Text = ""; //Texto que será apresentado                                        
                    lbStatus.Update();
                    txtRHAba2PIS3.Clear();
                    if (resultado == 0)
                        MessageBox.Show("Excluído com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Senão mostra erro
                    else
                        MessageBox.Show("Não foi possivel excluir a biometria cadastrada. " + retornoErro.retornoREP(resultado), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch
            {
                lbStatus.Text = ""; //Texto que será apresentado                                                                                                      

                MessageBox.Show("Ocorreu uma falha ao formatar as biometrias.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnRHAba2EnviarBio.Enabled = true;
                btnRHAba2ReceberBio.Enabled = true;
                btnRHAba2FormataBio.Enabled = true;
                btnRHAba2ExcluirBio.Enabled = true;
            }
            
        }

        //******************************************************************************/
        // private: btnTIAba2ReceberTemp                                                *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 09/11/2011
        // Descrição : Recebe a temperatura do terminal.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba2ReceberTemp_Click(object sender, EventArgs e)
        {
            // int memUsada = 0, memLivre = 0, resultado = 0;
            string path = Application.StartupPath;
            byte[] temperatura = new byte[500];
            int resultado = 0;
            //Recebe o ip do REP           
            string ip = trataRep.consultaTxt(path + "\\REPCad.txt", 2, cbTIAba2REP.Text, 0);
            //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);            
            if (envioDll.ConectaREP(ip, 1) != 0)
            {
                //dllREP.REP_Conexao(ip, porta, 0, senha, ref resultado);
                envioDll.ConectaREP(ip, 0);
                MessageBox.Show("Não foi possivel conectar ao " + cbRHAba2REP.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                //Le a temperatura do REP
                dllREP.REP_LeTemperatura(ip, temperatura, ref resultado);
                txtTIAba2Temperatura.Text = temperatura[0].ToString();
                

                // se não recebeu informa
                if (temperatura[0] == 0)
                    MessageBox.Show("Não foi possivel receber a temperatura do terminal.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Fecha a conexão com o REP
                envioDll.ConectaREP(ip, 0);
                //dllREP.REP_Conexao(ip, porta, 0, senha, ref resultado);
            }

        }

        //******************************************************************************/
        // private: btnTIAba2ReceberImpress                                                  *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 09/11/2011
        // Descrição : Envia informação sobre o Status da Impressora
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba2ReceberImpress_Click(object sender, EventArgs e)
        {
            // int memUsada = 0, memLivre = 0, resultado = 0;
            string path = Application.StartupPath;
            byte[] statusImpressora = new byte[255];
            int resultado = 0;
            //Recebe o ip do REP           
            string ip = trataRep.consultaTxt(path + "\\REPCad.txt", 2, cbTIAba2REP.Text, 0);
            //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
            if (envioDll.ConectaREP(ip, 1) != 0)
            {
                envioDll.ConectaREP(ip, 0);
                MessageBox.Show("Não foi possivel conectar ao " + cbRHAba2REP.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                //Le o espaço da memoria no REP
                dllREP.REP_StatusImpressora(ip, statusImpressora, ref resultado);
                string statusImpressFormatada = Encoding.ASCII.GetString(statusImpressora);
                txtTIAba2Impressora.Text = statusImpressFormatada;

                // se não recebeu informa
                if (statusImpressora[0] == 0)
                    MessageBox.Show("Não foi possivel receber a temperatura do terminal.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //Fecha a conexão com o REP
                envioDll.ConectaREP(ip, 0);
            }
        }

        //******************************************************************************/
        // private: btnTIAba2ReceberRegistro                                                 *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 16/11/2011
        // Descrição : Filtra as informações que serão salvas todas as informações do terminal.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void btnTIAba2ReceberRegistro_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath;
            string ip;
           
            ip = trataRep.consultaTxt(path + "\\REPCad.txt", 2, cbTIAba2REP.Text, 0);
            //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
            System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");

            try
            {
                if (envioDll.ConectaREP(ip, 1) == 1)
                {
                    MessageBox.Show("REP não encontrado.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    lbStatus.Text = "Filtrando registro, por favor aguarde.";
                    lbStatus.Update();
                    //Recebe o arquivo do REP onde contem todos os registros (geral) existentes no REP                                        
                    //Caso receba com sucesso inicia a filtragem para obter somente os Registros de ponto
                    int resultado = envioDll.LeIntervaloNSR(ip, txtTIAba2RegistroInicial.Text, txtTIAba2RegistroFinal.Text, path + "\\" + txtTIAba2NomeArquivo.Text);
                    if (resultado == 0)
                    {
                        //Indica ao Filestream o caminho do arquivo coletado
                        FileStream abreArquivoTxt = new FileStream(path + "\\" + txtTIAba2NomeArquivo.Text, FileMode.Open, FileAccess.Read);
                        //Abre para leitura no padrão ISO 8859/1 - para não ocorrer erro com acentuação
                        StreamReader sr = new StreamReader(abreArquivoTxt, iso_8859_1);
                        //Cria o arquivo registro.txt para adicionar somente os registros de ponto encontrado
                        FileStream escreveArquivoTxt = new FileStream(pathArquivo + "\\" + txtTIAba2NomeArquivo.Text, FileMode.OpenOrCreate, FileAccess.Write);
                        //Abre o arquivo para escrita
                        StreamWriter reg = new StreamWriter(escreveArquivoTxt, iso_8859_1);
                        string line = "";

                        //Le linha a linha
                        while ((line = sr.ReadLine()) != null)
                        {
                            //Separa a linha em linhas, cada linha com cada informação encontrada até o ';'
                            string[] linhaReg = line.Split(';');
                            if (linhaReg[1] == "3")
                            {
                                //Reescreve o arquivo
                                reg.WriteLine(line);
                            }
                        }
                        //Fecha os arquivos
                        reg.Close();
                        sr.Close();
                        //Remove o arquivo coletado, pois nao será mais utilizado
                        //Fecha a conexão com o REP
                        envioDll.ConectaREP(ip, 0);
                        //Informa que os arquivos foram coletados                        
                        MessageBox.Show("Arquivo Salvo", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lbStatus.Text = "";
                        lbStatus.Update();
                    }
                    else
                    {
                        //Fecha a conexão com o REP e mostra que ocorreu falha
                        envioDll.ConectaREP(ip, 0);
                        MessageBox.Show("Falha ao filtrar arquivo. " + retornoErro.retornoREP(resultado), "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        lbStatus.Text = "";
                        lbStatus.Update();
                    }
                }
            }
            catch
            {
                envioDll.ConectaREP(ip, 0);
                MessageBox.Show("Ocorreu um erro na tentativa de coletar os dados.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                lbStatus.Text = "";
                lbStatus.Update();
            }
        }

        //******************************************************************************/
        // private: txtTIAba2RegistroInicial                                                 *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 16/11/2011
        // Descrição : Verifica se todos os campos estão preenchidos.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba2RegistroInicial_TextChanged(object sender, EventArgs e)
        {
            if (cbTIAba2REP.Text != "" & txtTIAba2RegistroInicial.Text != "" &
                 txtTIAba2RegistroFinal.Text != "" & txtTIAba2NomeArquivo.Text != "")

                btnTIAba2ReceberRegistro.Enabled = true;

            else
                btnTIAba2ReceberRegistro.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba2RegistroFinal                                                 *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 16/11/2011
        // Descrição : Verifica se todos os campos estão preenchidos.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba2RegistroFinal_TextChanged(object sender, EventArgs e)
        {
            if (cbTIAba2REP.Text != "" & txtTIAba2RegistroInicial.Text != "" &
                 txtTIAba2RegistroFinal.Text != "" & txtTIAba2NomeArquivo.Text != "")

                btnTIAba2ReceberRegistro.Enabled = true;

            else
                btnTIAba2ReceberRegistro.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba2NomeArquivo                                                 *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 16/11/2011
        // Descrição : Verifica se todos os campos estão preenchidos.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba2NomeArquivo_TextChanged(object sender, EventArgs e)
        {
            if (cbTIAba2REP.Text != "" & txtTIAba2RegistroInicial.Text != "" &
                 txtTIAba2RegistroFinal.Text != "" & txtTIAba2NomeArquivo.Text != "")

                btnTIAba2ReceberRegistro.Enabled = true;

            else
                btnTIAba2ReceberRegistro.Enabled = false;
        }

        //******************************************************************************/
        // private: txtTIAba2RegistroInicial                                                 *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 16/11/2011
        // Descrição : Somente completar com números.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba2RegistroInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            // somente numeros
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        //******************************************************************************/
        // private: txtTIAba2RegistroFinal                                                 *
        //******************************************************************************/
        //
        // Autor: Débora Alves de Andrade
        // Data de Criação : 16/11/2011
        // Descrição : Somente completar com números.
        //              
        // Retorno: void
        //
        //******************************************************************************
        private void txtTIAba2RegistroFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            // somente numeros
            if (!char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
                e.Handled = true;
        }

        private void btnRHAba1Alterar_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = 0;
                string path = Application.StartupPath;

                string sCPFCNPJ;
                string sCEI;
                string bRazaoSocial;
                string bLocal;
                char cTipo;
                System.Text.Encoding iso_8859_1 = System.Text.Encoding.GetEncoding("iso-8859-1");


                //Verifica se foi selecionado CPF ou CNPJ
                switch (cbRHAba1Identificador.SelectedIndex)
                {
                    case 0:
                        //Valida o CNPJ
                        if (Validacao.ValidaCNPJ(txtRHAba1CNPJCPF.Text) == false)
                            //Se a validação retornar false verifica se o que foi digitado é um CPF
                            if (Validacao.ValidaCPF(txtRHAba1CNPJCPF.Text) == true)
                                cbRHAba1Identificador.SelectedIndex = 1;
                            else
                            {
                                MessageBox.Show("CNPJ Inválido.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        break;

                    case 1:
                        //Valida o CPF
                        if (Validacao.ValidaCPF(txtRHAba1CNPJCPF.Text) == false)
                            //Se a validação retornar false verifica se o que foi digitado é um CNPJ
                            if (Validacao.ValidaCNPJ(txtRHAba1CNPJCPF.Text) == true)
                                cbRHAba1Identificador.SelectedIndex = 0;
                            else
                            {
                                MessageBox.Show("CPF Inválido.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        break;
                    default:
                        cbRHAba1Identificador.SelectedIndex = 0;
                        goto case 1;
                }

                //Verifica se todos os campos foram preenchidos
                if (txtRHAba1RazaoSocial.Text == "")
                {
                    MessageBox.Show("Preencha o campo referente a razão social da empresa ou nome", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRHAba1RazaoSocial.Focus();
                    return;
                }

                if (txtRHAba1Endereco.Text == "")
                {
                    MessageBox.Show("Preencha o campo referente ao Endereço", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtRHAba1Endereco.Focus();
                    return;
                }
                // Recebe as informações para cadastrar               
                //CNPJ/CPF - Deverá haver 14 posições
                if (cbRHAba1Identificador.SelectedIndex == 1)
                    cTipo = '2';
                else
                    cTipo = '1';
                
                sCPFCNPJ = txtRHAba1CNPJCPF.Text + '\0';//auxStr;
                sCEI = txtRHAba1CEI.Text + '\0';
                bRazaoSocial = txtRHAba1RazaoSocial.Text + '\0';
                bLocal = txtRHAba1Endereco.Text + '\0';

                //Verifica o IP
                string ip = trataRep.consultaTxt(path + "\\REPCad.txt", 2, cbRHAba1NomeREP1.Text, 0);//"192.168.0.206"; //listIp[0].ToString();
                //trataRep.consultaTxt(path, 3, cbRHAba2REP.Text, 2);
                resultado = envioDll.ConectaREP(ip, 1);
                if (resultado != 0 & resultado != 10005)
                {
                    resultado = envioDll.ConectaREP(ip, 0);

                    MessageBox.Show("Não foi possivel conectar ao " + cbRHAba1NomeREP1.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {

                    dllREP.REP_GravaCadastroEmpregador(
                        ip,
                        '1',
                        cTipo,
                        sCPFCNPJ,
                        sCEI,
                        bRazaoSocial,
                        bLocal,
                        ref resultado);

                    if (resultado == 0)
                    {
                        txtRHAba1RazaoSocial.Text = "";
                        cbRHAba1Identificador.Text = "";
                        txtRHAba1CNPJCPF.Text = "";
                        txtRHAba1CEI.Text = "";
                        txtRHAba1Endereco.Text = "";
                        resultado = envioDll.ConectaREP(ip, 0);

                        MessageBox.Show("Alteração efetuada com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtRHAba1RazaoSocial.Focus();
                        Application.DoEvents();
                    }
                    else
                    {
                        resultado = envioDll.ConectaREP(ip, 0);
                        MessageBox.Show("Não foi possivel alterar o cadastro.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

     
   }
}



