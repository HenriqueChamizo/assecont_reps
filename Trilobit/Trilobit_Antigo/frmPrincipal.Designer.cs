namespace Trilobit
{
    partial class Principal {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.menuTerminais = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.propriedadesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excluirTerminalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.listFuncionarios = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuFuncionarios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btAssociarMatricula = new System.Windows.Forms.ToolStripMenuItem();
            this.enviarFuncionáriosSelecionadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excluirFuncionárioDoTerminalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listRelogios = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.edLog = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.empresaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cadastrarNovoTerminalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.enviarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminalSelecionadoToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.todosOsTerminaisToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.manutençãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enviarDataEHoraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminalSelecionadoToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.todosOsTerminaisToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.importaçãoTodasAsMarcaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminalSelecionadoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.todosOsTerminaisToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.abrirÚltimoArquivoImportadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lerConfiguraçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.funcionárioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enviarCadastroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.funcionárioSelecionadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enviarHorarioDeVerãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTerminais.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menuFuncionarios.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuTerminais
            // 
            this.menuTerminais.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.propriedadesToolStripMenuItem,
            this.excluirTerminalToolStripMenuItem});
            this.menuTerminais.Name = "menuTerminais";
            this.menuTerminais.Size = new System.Drawing.Size(159, 48);
            // 
            // propriedadesToolStripMenuItem
            // 
            this.propriedadesToolStripMenuItem.Name = "propriedadesToolStripMenuItem";
            this.propriedadesToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.propriedadesToolStripMenuItem.Text = "Propriedades...";
            this.propriedadesToolStripMenuItem.Click += new System.EventHandler(this.listRelogios_DoubleClick);
            // 
            // excluirTerminalToolStripMenuItem
            // 
            this.excluirTerminalToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("excluirTerminalToolStripMenuItem.Image")));
            this.excluirTerminalToolStripMenuItem.Name = "excluirTerminalToolStripMenuItem";
            this.excluirTerminalToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.excluirTerminalToolStripMenuItem.Text = "Excluir Terminal";
            this.excluirTerminalToolStripMenuItem.Click += new System.EventHandler(this.excluirTerminalToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.listRelogios);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(7);
            this.panel1.Size = new System.Drawing.Size(874, 228);
            this.panel1.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.listFuncionarios);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(347, 7);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.panel3.Size = new System.Drawing.Size(520, 214);
            this.panel3.TabIndex = 13;
            // 
            // listFuncionarios
            // 
            this.listFuncionarios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader8,
            this.columnHeader7,
            this.columnHeader9});
            this.listFuncionarios.ContextMenuStrip = this.menuFuncionarios;
            this.listFuncionarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listFuncionarios.FullRowSelect = true;
            this.listFuncionarios.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listFuncionarios.Location = new System.Drawing.Point(7, 0);
            this.listFuncionarios.Name = "listFuncionarios";
            this.listFuncionarios.Size = new System.Drawing.Size(513, 214);
            this.listFuncionarios.TabIndex = 14;
            this.listFuncionarios.UseCompatibleStateImageBehavior = false;
            this.listFuncionarios.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Nome";
            this.columnHeader5.Width = 135;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Função";
            this.columnHeader6.Width = 101;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Matrícula";
            this.columnHeader8.Width = 90;
            // 
            // columnHeader7
            // 
            this.columnHeader7.DisplayIndex = 4;
            this.columnHeader7.Text = "Pis";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader9
            // 
            this.columnHeader9.DisplayIndex = 3;
            this.columnHeader9.Text = "Indice*";
            this.columnHeader9.Width = 0;
            // 
            // menuFuncionarios
            // 
            this.menuFuncionarios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btAssociarMatricula,
            this.enviarFuncionáriosSelecionadosToolStripMenuItem,
            this.excluirFuncionárioDoTerminalToolStripMenuItem});
            this.menuFuncionarios.Name = "menuFuncionarios";
            this.menuFuncionarios.Size = new System.Drawing.Size(250, 70);
            // 
            // btAssociarMatricula
            // 
            this.btAssociarMatricula.Name = "btAssociarMatricula";
            this.btAssociarMatricula.Size = new System.Drawing.Size(249, 22);
            this.btAssociarMatricula.Text = "Associar Número de Matrícula...";
            this.btAssociarMatricula.Click += new System.EventHandler(this.btAssociarMatricula_Click);
            // 
            // enviarFuncionáriosSelecionadosToolStripMenuItem
            // 
            this.enviarFuncionáriosSelecionadosToolStripMenuItem.Name = "enviarFuncionáriosSelecionadosToolStripMenuItem";
            this.enviarFuncionáriosSelecionadosToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.enviarFuncionáriosSelecionadosToolStripMenuItem.Text = "Enviar Funcionários Selecionados";
            this.enviarFuncionáriosSelecionadosToolStripMenuItem.Click += new System.EventHandler(this.funcionárioSelecionadoToolStripMenuItem_Click);
            // 
            // excluirFuncionárioDoTerminalToolStripMenuItem
            // 
            this.excluirFuncionárioDoTerminalToolStripMenuItem.Name = "excluirFuncionárioDoTerminalToolStripMenuItem";
            this.excluirFuncionárioDoTerminalToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.excluirFuncionárioDoTerminalToolStripMenuItem.Text = "Excluir Funcionário do Terminal";
            this.excluirFuncionárioDoTerminalToolStripMenuItem.Click += new System.EventHandler(this.excluirFuncionárioDoTerminalToolStripMenuItem_Click);
            // 
            // listRelogios
            // 
            this.listRelogios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listRelogios.ContextMenuStrip = this.menuTerminais;
            this.listRelogios.Dock = System.Windows.Forms.DockStyle.Left;
            this.listRelogios.FullRowSelect = true;
            this.listRelogios.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listRelogios.Location = new System.Drawing.Point(7, 7);
            this.listRelogios.Name = "listRelogios";
            this.listRelogios.Size = new System.Drawing.Size(340, 214);
            this.listRelogios.TabIndex = 12;
            this.listRelogios.UseCompatibleStateImageBehavior = false;
            this.listRelogios.View = System.Windows.Forms.View.Details;
            this.listRelogios.Click += new System.EventHandler(this.listRelogios_Click);
            this.listRelogios.DoubleClick += new System.EventHandler(this.listRelogios_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Descrição";
            this.columnHeader1.Width = 135;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Ip";
            this.columnHeader2.Width = 101;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Porta";
            this.columnHeader3.Width = 82;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.edLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 252);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(7, 0, 7, 7);
            this.panel2.Size = new System.Drawing.Size(874, 167);
            this.panel2.TabIndex = 14;
            // 
            // edLog
            // 
            this.edLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edLog.Location = new System.Drawing.Point(7, 0);
            this.edLog.Multiline = true;
            this.edLog.Name = "edLog";
            this.edLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.edLog.Size = new System.Drawing.Size(860, 160);
            this.edLog.TabIndex = 15;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.empresaToolStripMenuItem,
            this.manutençãoToolStripMenuItem,
            this.funcionárioToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(874, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // empresaToolStripMenuItem
            // 
            this.empresaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastrarNovoTerminalToolStripMenuItem,
            this.toolStripMenuItem1,
            this.enviarToolStripMenuItem});
            this.empresaToolStripMenuItem.Name = "empresaToolStripMenuItem";
            this.empresaToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.empresaToolStripMenuItem.Text = "Empregador";
            this.empresaToolStripMenuItem.Click += new System.EventHandler(this.empresaToolStripMenuItem_Click);
            // 
            // cadastrarNovoTerminalToolStripMenuItem
            // 
            this.cadastrarNovoTerminalToolStripMenuItem.Name = "cadastrarNovoTerminalToolStripMenuItem";
            this.cadastrarNovoTerminalToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.cadastrarNovoTerminalToolStripMenuItem.Text = "Cadastrar Novo Terminal...";
            this.cadastrarNovoTerminalToolStripMenuItem.Click += new System.EventHandler(this.cadastrarNovoTerminalToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(212, 6);
            // 
            // enviarToolStripMenuItem
            // 
            this.enviarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.terminalSelecionadoToolStripMenuItem3,
            this.todosOsTerminaisToolStripMenuItem3});
            this.enviarToolStripMenuItem.Name = "enviarToolStripMenuItem";
            this.enviarToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.enviarToolStripMenuItem.Text = "Enviar";
            // 
            // terminalSelecionadoToolStripMenuItem3
            // 
            this.terminalSelecionadoToolStripMenuItem3.Name = "terminalSelecionadoToolStripMenuItem3";
            this.terminalSelecionadoToolStripMenuItem3.Size = new System.Drawing.Size(188, 22);
            this.terminalSelecionadoToolStripMenuItem3.Text = "Terminal Selecionado";
            this.terminalSelecionadoToolStripMenuItem3.Click += new System.EventHandler(this.terminalSelecionadoToolStripMenuItem3_Click);
            // 
            // todosOsTerminaisToolStripMenuItem3
            // 
            this.todosOsTerminaisToolStripMenuItem3.Name = "todosOsTerminaisToolStripMenuItem3";
            this.todosOsTerminaisToolStripMenuItem3.Size = new System.Drawing.Size(188, 22);
            this.todosOsTerminaisToolStripMenuItem3.Text = "Todos os Terminais";
            this.todosOsTerminaisToolStripMenuItem3.Click += new System.EventHandler(this.todosOsTerminaisToolStripMenuItem3_Click);
            // 
            // manutençãoToolStripMenuItem
            // 
            this.manutençãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enviarDataEHoraToolStripMenuItem,
            this.toolStripMenuItem4,
            this.importaçãoTodasAsMarcaçõesToolStripMenuItem,
            this.lerConfiguraçãoToolStripMenuItem,
            this.enviarHorarioDeVerãoToolStripMenuItem});
            this.manutençãoToolStripMenuItem.Name = "manutençãoToolStripMenuItem";
            this.manutençãoToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.manutençãoToolStripMenuItem.Text = "Manutenção";
            // 
            // enviarDataEHoraToolStripMenuItem
            // 
            this.enviarDataEHoraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.terminalSelecionadoToolStripMenuItem2,
            this.todosOsTerminaisToolStripMenuItem2});
            this.enviarDataEHoraToolStripMenuItem.Name = "enviarDataEHoraToolStripMenuItem";
            this.enviarDataEHoraToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.enviarDataEHoraToolStripMenuItem.Text = "Enviar Data e Hora";
            // 
            // terminalSelecionadoToolStripMenuItem2
            // 
            this.terminalSelecionadoToolStripMenuItem2.Name = "terminalSelecionadoToolStripMenuItem2";
            this.terminalSelecionadoToolStripMenuItem2.Size = new System.Drawing.Size(188, 22);
            this.terminalSelecionadoToolStripMenuItem2.Text = "Terminal Selecionado";
            this.terminalSelecionadoToolStripMenuItem2.Click += new System.EventHandler(this.terminalSelecionadoToolStripMenuItem2_Click);
            // 
            // todosOsTerminaisToolStripMenuItem2
            // 
            this.todosOsTerminaisToolStripMenuItem2.Name = "todosOsTerminaisToolStripMenuItem2";
            this.todosOsTerminaisToolStripMenuItem2.Size = new System.Drawing.Size(188, 22);
            this.todosOsTerminaisToolStripMenuItem2.Text = "Todos os Terminais";
            this.todosOsTerminaisToolStripMenuItem2.Click += new System.EventHandler(this.todosOsTerminaisToolStripMenuItem2_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(289, 6);
            // 
            // importaçãoTodasAsMarcaçõesToolStripMenuItem
            // 
            this.importaçãoTodasAsMarcaçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.terminalSelecionadoToolStripMenuItem1,
            this.todosOsTerminaisToolStripMenuItem1,
            this.toolStripMenuItem2,
            this.abrirÚltimoArquivoImportadoToolStripMenuItem});
            this.importaçãoTodasAsMarcaçõesToolStripMenuItem.Name = "importaçãoTodasAsMarcaçõesToolStripMenuItem";
            this.importaçãoTodasAsMarcaçõesToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.importaçãoTodasAsMarcaçõesToolStripMenuItem.Text = "Importação de Marcações";
            // 
            // terminalSelecionadoToolStripMenuItem1
            // 
            this.terminalSelecionadoToolStripMenuItem1.Name = "terminalSelecionadoToolStripMenuItem1";
            this.terminalSelecionadoToolStripMenuItem1.Size = new System.Drawing.Size(243, 22);
            this.terminalSelecionadoToolStripMenuItem1.Text = "Terminal Selecionado";
            this.terminalSelecionadoToolStripMenuItem1.Click += new System.EventHandler(this.terminalSelecionadoToolStripMenuItem1_Click);
            // 
            // todosOsTerminaisToolStripMenuItem1
            // 
            this.todosOsTerminaisToolStripMenuItem1.Name = "todosOsTerminaisToolStripMenuItem1";
            this.todosOsTerminaisToolStripMenuItem1.Size = new System.Drawing.Size(243, 22);
            this.todosOsTerminaisToolStripMenuItem1.Text = "Todos os Terminais";
            this.todosOsTerminaisToolStripMenuItem1.Click += new System.EventHandler(this.todosOsTerminaisToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(240, 6);
            // 
            // abrirÚltimoArquivoImportadoToolStripMenuItem
            // 
            this.abrirÚltimoArquivoImportadoToolStripMenuItem.Name = "abrirÚltimoArquivoImportadoToolStripMenuItem";
            this.abrirÚltimoArquivoImportadoToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.abrirÚltimoArquivoImportadoToolStripMenuItem.Text = "Abrir Último Arquivo Importado";
            this.abrirÚltimoArquivoImportadoToolStripMenuItem.Click += new System.EventHandler(this.abrirÚltimoArquivoImportadoToolStripMenuItem_Click);
            // 
            // lerConfiguraçãoToolStripMenuItem
            // 
            this.lerConfiguraçãoToolStripMenuItem.Name = "lerConfiguraçãoToolStripMenuItem";
            this.lerConfiguraçãoToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.lerConfiguraçãoToolStripMenuItem.Text = "Ler Configuração Terminais Selecionados";
            this.lerConfiguraçãoToolStripMenuItem.Visible = false;
            this.lerConfiguraçãoToolStripMenuItem.Click += new System.EventHandler(this.lerConfiguraçãoToolStripMenuItem_Click);
            // 
            // funcionárioToolStripMenuItem
            // 
            this.funcionárioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enviarCadastroToolStripMenuItem});
            this.funcionárioToolStripMenuItem.Name = "funcionárioToolStripMenuItem";
            this.funcionárioToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.funcionárioToolStripMenuItem.Text = "Funcionário";
            // 
            // enviarCadastroToolStripMenuItem
            // 
            this.enviarCadastroToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem,
            this.todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem,
            this.funcionárioSelecionadoToolStripMenuItem});
            this.enviarCadastroToolStripMenuItem.Name = "enviarCadastroToolStripMenuItem";
            this.enviarCadastroToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.enviarCadastroToolStripMenuItem.Text = "Enviar Cadastro";
            // 
            // todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem
            // 
            this.todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem.Name = "todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem";
            this.todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem.Size = new System.Drawing.Size(313, 22);
            this.todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem.Text = "Todos os Funcionários, Terminal Selecionado";
            this.todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem.Click += new System.EventHandler(this.todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem_Click);
            // 
            // todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem
            // 
            this.todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem.Name = "todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem";
            this.todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem.Size = new System.Drawing.Size(313, 22);
            this.todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem.Text = "Todos os Funcionários, Todos os Terminais";
            this.todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem.Click += new System.EventHandler(this.todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem_Click);
            // 
            // funcionárioSelecionadoToolStripMenuItem
            // 
            this.funcionárioSelecionadoToolStripMenuItem.Name = "funcionárioSelecionadoToolStripMenuItem";
            this.funcionárioSelecionadoToolStripMenuItem.Size = new System.Drawing.Size(313, 22);
            this.funcionárioSelecionadoToolStripMenuItem.Text = "Funcionários Selecionados";
            this.funcionárioSelecionadoToolStripMenuItem.Click += new System.EventHandler(this.funcionárioSelecionadoToolStripMenuItem_Click);
            // 
            // enviarHorarioDeVerãoToolStripMenuItem
            // 
            this.enviarHorarioDeVerãoToolStripMenuItem.Name = "enviarHorarioDeVerãoToolStripMenuItem";
            this.enviarHorarioDeVerãoToolStripMenuItem.Size = new System.Drawing.Size(292, 22);
            this.enviarHorarioDeVerãoToolStripMenuItem.Text = "Enviar Horario de Verão";
            this.enviarHorarioDeVerãoToolStripMenuItem.Click += new System.EventHandler(this.enviarHorarioDeVerãoToolStripMenuItem_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 419);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comunicação Trilobit";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.menuTerminais.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.menuFuncionarios.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip menuTerminais;
        private System.Windows.Forms.ToolStripMenuItem excluirTerminalToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listRelogios;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox edLog;
        private System.Windows.Forms.ContextMenuStrip menuFuncionarios;
        private System.Windows.Forms.ToolStripMenuItem btAssociarMatricula;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem empresaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cadastrarNovoTerminalToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem manutençãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem funcionárioToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem importaçãoTodasAsMarcaçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terminalSelecionadoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem todosOsTerminaisToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem propriedadesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enviarDataEHoraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terminalSelecionadoToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem todosOsTerminaisToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem enviarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terminalSelecionadoToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem todosOsTerminaisToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem enviarCadastroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem todosOsFuncionáriosTerminalSelecionadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem todosOsFuncionáriosTodosOsTerminaisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem funcionárioSelecionadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excluirFuncionárioDoTerminalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enviarFuncionáriosSelecionadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem abrirÚltimoArquivoImportadoToolStripMenuItem;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView listFuncionarios;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ToolStripMenuItem lerConfiguraçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enviarHorarioDeVerãoToolStripMenuItem;
    }
}

