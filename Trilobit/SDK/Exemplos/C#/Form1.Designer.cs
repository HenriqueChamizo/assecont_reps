namespace RepExemplo
{
    partial class frmExemplo
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtCEI = new System.Windows.Forms.MaskedTextBox();
            this.txtDocumento = new System.Windows.Forms.MaskedTextBox();
            this.cboTipoDoc = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnCadastrarEmpregador = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLocal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRazao = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkPossuiBio = new System.Windows.Forms.CheckBox();
            this.txtCracha = new System.Windows.Forms.TextBox();
            this.txtPIS = new System.Windows.Forms.TextBox();
            this.btnExcluirEmpregado = new System.Windows.Forms.Button();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.btnCadastrarEmpregado = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtArquivoEmpregados = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.chkAddCabecalho = new System.Windows.Forms.CheckBox();
            this.LerEmpregadosTXT = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LerEmpregadosDT = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cboParamSet = new System.Windows.Forms.ComboBox();
            this.txtParamSet = new System.Windows.Forms.TextBox();
            this.btnParamSet = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cboParamGet = new System.Windows.Forms.ComboBox();
            this.btnParamGet = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtParamGet = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtArquivo = new System.Windows.Forms.TextBox();
            this.lblDataInicial = new System.Windows.Forms.Label();
            this.lblDestino = new System.Windows.Forms.Label();
            this.btnLerAFD = new System.Windows.Forms.Button();
            this.dtpDataFinal = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpDataInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtNSR = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnLerAFD_DataTable = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPorta = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(3, 41);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(479, 389);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox8);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(471, 363);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Empregador";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtCEI);
            this.groupBox8.Controls.Add(this.txtDocumento);
            this.groupBox8.Controls.Add(this.cboTipoDoc);
            this.groupBox8.Controls.Add(this.label18);
            this.groupBox8.Controls.Add(this.btnCadastrarEmpregador);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.txtLocal);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.txtRazao);
            this.groupBox8.Controls.Add(this.label10);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Location = new System.Drawing.Point(6, 0);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(459, 358);
            this.groupBox8.TabIndex = 25;
            this.groupBox8.TabStop = false;
            // 
            // txtCEI
            // 
            this.txtCEI.Location = new System.Drawing.Point(178, 74);
            this.txtCEI.Mask = "99,999,99999/99";
            this.txtCEI.Name = "txtCEI";
            this.txtCEI.Size = new System.Drawing.Size(115, 20);
            this.txtCEI.TabIndex = 26;
            this.txtCEI.Text = "000000000000";
            // 
            // txtDocumento
            // 
            this.txtDocumento.Location = new System.Drawing.Point(178, 48);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(115, 20);
            this.txtDocumento.TabIndex = 4;
            this.txtDocumento.Text = "12.345.678/9012-34";
            this.txtDocumento.Enter += new System.EventHandler(this.txtDocumento_Enter);
            // 
            // cboTipoDoc
            // 
            this.cboTipoDoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoDoc.FormattingEnabled = true;
            this.cboTipoDoc.Items.AddRange(new object[] {
            "CNPJ",
            "CPF"});
            this.cboTipoDoc.Location = new System.Drawing.Point(178, 22);
            this.cboTipoDoc.Name = "cboTipoDoc";
            this.cboTipoDoc.Size = new System.Drawing.Size(115, 21);
            this.cboTipoDoc.TabIndex = 3;
            this.cboTipoDoc.SelectedIndexChanged += new System.EventHandler(this.cboTipoDoc_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(115, 28);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 13);
            this.label18.TabIndex = 25;
            this.label18.Text = "Tipo Doc. :";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCadastrarEmpregador
            // 
            this.btnCadastrarEmpregador.Location = new System.Drawing.Point(169, 152);
            this.btnCadastrarEmpregador.Name = "btnCadastrarEmpregador";
            this.btnCadastrarEmpregador.Size = new System.Drawing.Size(133, 26);
            this.btnCadastrarEmpregador.TabIndex = 8;
            this.btnCadastrarEmpregador.Text = "Cadastrar";
            this.btnCadastrarEmpregador.UseVisualStyleBackColor = true;
            this.btnCadastrarEmpregador.Click += new System.EventHandler(this.btnCadastrarEmpregador_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(136, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Local :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLocal
            // 
            this.txtLocal.Location = new System.Drawing.Point(178, 126);
            this.txtLocal.MaxLength = 100;
            this.txtLocal.Name = "txtLocal";
            this.txtLocal.Size = new System.Drawing.Size(188, 20);
            this.txtLocal.TabIndex = 7;
            this.txtLocal.Text = "RUA ALVARENGA, 1377";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(99, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Razão Social :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRazao
            // 
            this.txtRazao.Location = new System.Drawing.Point(178, 100);
            this.txtRazao.MaxLength = 150;
            this.txtRazao.Name = "txtRazao";
            this.txtRazao.Size = new System.Drawing.Size(188, 20);
            this.txtRazao.TabIndex = 6;
            this.txtRazao.Text = "TRILOBIT";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(145, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "CEI :";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(107, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Documento :";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(471, 363);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Empregado";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkPossuiBio);
            this.groupBox1.Controls.Add(this.txtCracha);
            this.groupBox1.Controls.Add(this.txtPIS);
            this.groupBox1.Controls.Add(this.btnExcluirEmpregado);
            this.groupBox1.Controls.Add(this.txtNome);
            this.groupBox1.Controls.Add(this.btnCadastrarEmpregado);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(6, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 133);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "PIS :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkPossuiBio
            // 
            this.chkPossuiBio.AutoSize = true;
            this.chkPossuiBio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPossuiBio.Location = new System.Drawing.Point(112, 81);
            this.chkPossuiBio.Name = "chkPossuiBio";
            this.chkPossuiBio.Size = new System.Drawing.Size(109, 17);
            this.chkPossuiBio.TabIndex = 11;
            this.chkPossuiBio.Text = "Possui Biometria :";
            this.chkPossuiBio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPossuiBio.UseVisualStyleBackColor = true;
            // 
            // txtCracha
            // 
            this.txtCracha.Location = new System.Drawing.Point(209, 60);
            this.txtCracha.MaxLength = 20;
            this.txtCracha.Name = "txtCracha";
            this.txtCracha.Size = new System.Drawing.Size(100, 20);
            this.txtCracha.TabIndex = 10;
            this.txtCracha.Text = "123";
            // 
            // txtPIS
            // 
            this.txtPIS.Location = new System.Drawing.Point(209, 14);
            this.txtPIS.MaxLength = 12;
            this.txtPIS.Name = "txtPIS";
            this.txtPIS.Size = new System.Drawing.Size(100, 20);
            this.txtPIS.TabIndex = 8;
            this.txtPIS.Text = "123456789012";
            // 
            // btnExcluirEmpregado
            // 
            this.btnExcluirEmpregado.Location = new System.Drawing.Point(76, 101);
            this.btnExcluirEmpregado.Name = "btnExcluirEmpregado";
            this.btnExcluirEmpregado.Size = new System.Drawing.Size(151, 26);
            this.btnExcluirEmpregado.TabIndex = 12;
            this.btnExcluirEmpregado.Text = "Excluir";
            this.btnExcluirEmpregado.UseVisualStyleBackColor = true;
            this.btnExcluirEmpregado.Click += new System.EventHandler(this.btnExcluirEmpregado_Click);
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(209, 37);
            this.txtNome.MaxLength = 52;
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(100, 20);
            this.txtNome.TabIndex = 9;
            this.txtNome.Text = "Adonis";
            // 
            // btnCadastrarEmpregado
            // 
            this.btnCadastrarEmpregado.Location = new System.Drawing.Point(234, 101);
            this.btnCadastrarEmpregado.Name = "btnCadastrarEmpregado";
            this.btnCadastrarEmpregado.Size = new System.Drawing.Size(151, 26);
            this.btnCadastrarEmpregado.TabIndex = 13;
            this.btnCadastrarEmpregado.Text = "Cadastrar";
            this.btnCadastrarEmpregado.UseVisualStyleBackColor = true;
            this.btnCadastrarEmpregado.Click += new System.EventHandler(this.btnCadastrarEmpregado_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(159, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Crachá :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Nome :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtArquivoEmpregados);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.chkAddCabecalho);
            this.groupBox2.Controls.Add(this.LerEmpregadosTXT);
            this.groupBox2.Location = new System.Drawing.Point(6, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(459, 88);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            // 
            // txtArquivoEmpregados
            // 
            this.txtArquivoEmpregados.Location = new System.Drawing.Point(144, 16);
            this.txtArquivoEmpregados.Name = "txtArquivoEmpregados";
            this.txtArquivoEmpregados.Size = new System.Drawing.Size(228, 20);
            this.txtArquivoEmpregados.TabIndex = 14;
            this.txtArquivoEmpregados.Text = "C:\\Empregados.txt";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(91, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 13);
            this.label17.TabIndex = 30;
            this.label17.Text = "Destino :";
            // 
            // chkAddCabecalho
            // 
            this.chkAddCabecalho.AutoSize = true;
            this.chkAddCabecalho.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAddCabecalho.Location = new System.Drawing.Point(28, 39);
            this.chkAddCabecalho.Name = "chkAddCabecalho";
            this.chkAddCabecalho.Size = new System.Drawing.Size(130, 17);
            this.chkAddCabecalho.TabIndex = 15;
            this.chkAddCabecalho.Text = "Adicionar Cabeçalho :";
            this.chkAddCabecalho.UseVisualStyleBackColor = true;
            // 
            // LerEmpregadosTXT
            // 
            this.LerEmpregadosTXT.Location = new System.Drawing.Point(161, 57);
            this.LerEmpregadosTXT.Name = "LerEmpregadosTXT";
            this.LerEmpregadosTXT.Size = new System.Drawing.Size(151, 26);
            this.LerEmpregadosTXT.TabIndex = 16;
            this.LerEmpregadosTXT.Text = "Ler Empregados (TXT)";
            this.LerEmpregadosTXT.UseVisualStyleBackColor = true;
            this.LerEmpregadosTXT.Click += new System.EventHandler(this.LerEmpregadosTXT_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LerEmpregadosDT);
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Location = new System.Drawing.Point(6, 218);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(459, 141);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            // 
            // LerEmpregadosDT
            // 
            this.LerEmpregadosDT.Location = new System.Drawing.Point(161, 109);
            this.LerEmpregadosDT.Name = "LerEmpregadosDT";
            this.LerEmpregadosDT.Size = new System.Drawing.Size(151, 26);
            this.LerEmpregadosDT.TabIndex = 17;
            this.LerEmpregadosDT.Text = "Ler Empregados (DataTable)";
            this.LerEmpregadosDT.UseVisualStyleBackColor = true;
            this.LerEmpregadosDT.Click += new System.EventHandler(this.LerEmpregadosDT_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(2, 9);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(455, 97);
            this.dataGridView2.TabIndex = 28;
            this.dataGridView2.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(471, 363);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Configurações";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cboParamSet);
            this.groupBox4.Controls.Add(this.txtParamSet);
            this.groupBox4.Controls.Add(this.btnParamSet);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(6, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(459, 181);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            // 
            // cboParamSet
            // 
            this.cboParamSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParamSet.FormattingEnabled = true;
            this.cboParamSet.Location = new System.Drawing.Point(117, 55);
            this.cboParamSet.Name = "cboParamSet";
            this.cboParamSet.Size = new System.Drawing.Size(237, 21);
            this.cboParamSet.TabIndex = 18;
            // 
            // txtParamSet
            // 
            this.txtParamSet.Location = new System.Drawing.Point(117, 80);
            this.txtParamSet.Name = "txtParamSet";
            this.txtParamSet.Size = new System.Drawing.Size(237, 20);
            this.txtParamSet.TabIndex = 19;
            // 
            // btnParamSet
            // 
            this.btnParamSet.Location = new System.Drawing.Point(153, 106);
            this.btnParamSet.Name = "btnParamSet";
            this.btnParamSet.Size = new System.Drawing.Size(151, 26);
            this.btnParamSet.TabIndex = 20;
            this.btnParamSet.Text = "Enviar";
            this.btnParamSet.UseVisualStyleBackColor = true;
            this.btnParamSet.Click += new System.EventHandler(this.btnParamSet_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Parâmetro :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(74, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Valor :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cboParamGet);
            this.groupBox5.Controls.Add(this.btnParamGet);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txtParamGet);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Location = new System.Drawing.Point(6, 180);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(459, 181);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            // 
            // cboParamGet
            // 
            this.cboParamGet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParamGet.FormattingEnabled = true;
            this.cboParamGet.Location = new System.Drawing.Point(117, 58);
            this.cboParamGet.Name = "cboParamGet";
            this.cboParamGet.Size = new System.Drawing.Size(237, 21);
            this.cboParamGet.TabIndex = 21;
            // 
            // btnParamGet
            // 
            this.btnParamGet.Location = new System.Drawing.Point(155, 109);
            this.btnParamGet.Name = "btnParamGet";
            this.btnParamGet.Size = new System.Drawing.Size(151, 26);
            this.btnParamGet.TabIndex = 23;
            this.btnParamGet.Text = "Ler";
            this.btnParamGet.UseVisualStyleBackColor = true;
            this.btnParamGet.Click += new System.EventHandler(this.btnParamGet_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(74, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Valor :";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtParamGet
            // 
            this.txtParamGet.Location = new System.Drawing.Point(117, 83);
            this.txtParamGet.Name = "txtParamGet";
            this.txtParamGet.Size = new System.Drawing.Size(237, 20);
            this.txtParamGet.TabIndex = 22;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(50, 61);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "Parâmetro :";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox7);
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(471, 363);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "AFD";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtArquivo);
            this.groupBox7.Controls.Add(this.lblDataInicial);
            this.groupBox7.Controls.Add(this.lblDestino);
            this.groupBox7.Controls.Add(this.btnLerAFD);
            this.groupBox7.Controls.Add(this.dtpDataFinal);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.dtpDataInicial);
            this.groupBox7.Location = new System.Drawing.Point(6, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(459, 113);
            this.groupBox7.TabIndex = 31;
            this.groupBox7.TabStop = false;
            // 
            // txtArquivo
            // 
            this.txtArquivo.Location = new System.Drawing.Point(178, 58);
            this.txtArquivo.Name = "txtArquivo";
            this.txtArquivo.Size = new System.Drawing.Size(228, 20);
            this.txtArquivo.TabIndex = 26;
            this.txtArquivo.Text = "C:\\AFD.txt";
            // 
            // lblDataInicial
            // 
            this.lblDataInicial.AutoSize = true;
            this.lblDataInicial.Location = new System.Drawing.Point(110, 14);
            this.lblDataInicial.Name = "lblDataInicial";
            this.lblDataInicial.Size = new System.Drawing.Size(66, 13);
            this.lblDataInicial.TabIndex = 22;
            this.lblDataInicial.Text = "Data Inicial :";
            // 
            // lblDestino
            // 
            this.lblDestino.AutoSize = true;
            this.lblDestino.Location = new System.Drawing.Point(127, 60);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(49, 13);
            this.lblDestino.TabIndex = 24;
            this.lblDestino.Text = "Destino :";
            // 
            // btnLerAFD
            // 
            this.btnLerAFD.Location = new System.Drawing.Point(164, 85);
            this.btnLerAFD.Name = "btnLerAFD";
            this.btnLerAFD.Size = new System.Drawing.Size(151, 26);
            this.btnLerAFD.TabIndex = 27;
            this.btnLerAFD.Text = "Ler AFD (TXT)";
            this.btnLerAFD.UseVisualStyleBackColor = true;
            this.btnLerAFD.Click += new System.EventHandler(this.btnLerAFD_Click);
            // 
            // dtpDataFinal
            // 
            this.dtpDataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataFinal.Location = new System.Drawing.Point(178, 34);
            this.dtpDataFinal.Name = "dtpDataFinal";
            this.dtpDataFinal.Size = new System.Drawing.Size(101, 20);
            this.dtpDataFinal.TabIndex = 25;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(115, 37);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(61, 13);
            this.label15.TabIndex = 23;
            this.label15.Text = "Data Final :";
            // 
            // dtpDataInicial
            // 
            this.dtpDataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataInicial.Location = new System.Drawing.Point(178, 10);
            this.dtpDataInicial.Name = "dtpDataInicial";
            this.dtpDataInicial.Size = new System.Drawing.Size(101, 20);
            this.dtpDataInicial.TabIndex = 24;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtNSR);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.dataGridView1);
            this.groupBox6.Controls.Add(this.btnLerAFD_DataTable);
            this.groupBox6.Location = new System.Drawing.Point(5, 117);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(459, 244);
            this.groupBox6.TabIndex = 30;
            this.groupBox6.TabStop = false;
            // 
            // txtNSR
            // 
            this.txtNSR.Location = new System.Drawing.Point(213, 192);
            this.txtNSR.Name = "txtNSR";
            this.txtNSR.Size = new System.Drawing.Size(83, 20);
            this.txtNSR.TabIndex = 28;
            this.txtNSR.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(175, 195);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(36, 13);
            this.label16.TabIndex = 29;
            this.label16.Text = "NSR :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(2, 9);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(455, 177);
            this.dataGridView1.TabIndex = 27;
            this.dataGridView1.TabStop = false;
            // 
            // btnLerAFD_DataTable
            // 
            this.btnLerAFD_DataTable.Location = new System.Drawing.Point(165, 215);
            this.btnLerAFD_DataTable.Name = "btnLerAFD_DataTable";
            this.btnLerAFD_DataTable.Size = new System.Drawing.Size(151, 26);
            this.btnLerAFD_DataTable.TabIndex = 29;
            this.btnLerAFD_DataTable.Text = "Ler AFD (DataTable)";
            this.btnLerAFD_DataTable.UseVisualStyleBackColor = true;
            this.btnLerAFD_DataTable.Click += new System.EventHandler(this.btnLerAFD_DataTable_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label13);
            this.groupBox9.Controls.Add(this.txtSenha);
            this.groupBox9.Controls.Add(this.label2);
            this.groupBox9.Controls.Add(this.label1);
            this.groupBox9.Controls.Add(this.txtPorta);
            this.groupBox9.Controls.Add(this.txtIP);
            this.groupBox9.Location = new System.Drawing.Point(3, -2);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(478, 39);
            this.groupBox9.TabIndex = 21;
            this.groupBox9.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(321, 16);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Senha :";
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(368, 13);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(100, 20);
            this.txtSenha.TabIndex = 2;
            this.txtSenha.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Porta :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "IP :";
            // 
            // txtPorta
            // 
            this.txtPorta.Location = new System.Drawing.Point(203, 13);
            this.txtPorta.Name = "txtPorta";
            this.txtPorta.Size = new System.Drawing.Size(100, 20);
            this.txtPorta.TabIndex = 1;
            this.txtPorta.Text = "19001";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(37, 13);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 20);
            this.txtIP.TabIndex = 0;
            this.txtIP.Text = "127.0.0.1";
            // 
            // frmExemplo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 433);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmExemplo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trilobit REP - Exemplo de uso";
            this.Load += new System.EventHandler(this.frmExemplo_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLocal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRazao;
        private System.Windows.Forms.Button btnCadastrarEmpregador;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtParamSet;
        private System.Windows.Forms.ComboBox cboParamSet;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnParamGet;
        private System.Windows.Forms.Button btnParamSet;
        private System.Windows.Forms.Button btnLerAFD;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtParamGet;
        private System.Windows.Forms.ComboBox cboParamGet;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtArquivo;
        private System.Windows.Forms.Label lblDestino;
        private System.Windows.Forms.DateTimePicker dtpDataFinal;
        private System.Windows.Forms.DateTimePicker dtpDataInicial;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblDataInicial;
        private System.Windows.Forms.Button btnLerAFD_DataTable;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtNSR;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtArquivoEmpregados;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox chkAddCabecalho;
        private System.Windows.Forms.Button LerEmpregadosTXT;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkPossuiBio;
        private System.Windows.Forms.TextBox txtCracha;
        private System.Windows.Forms.TextBox txtPIS;
        private System.Windows.Forms.Button btnExcluirEmpregado;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Button btnCadastrarEmpregado;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button LerEmpregadosDT;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox cboTipoDoc;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPorta;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.MaskedTextBox txtDocumento;
        private System.Windows.Forms.MaskedTextBox txtCEI;
    }
}

