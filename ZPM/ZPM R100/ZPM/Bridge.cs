using System.Windows.Forms;
using AssepontoRep;
using DemoREP_CSharp;
using System;
using System.Text;
using Wr.Classes;
using System.Collections.Generic;
using System.IO;

namespace ZPM
{
    public class Bridge : AssepontoRep.Bridge
    {
        private StringBuilder MensagemErro = new StringBuilder(256);
        private TextBox consoleLog;
        private int NRegistros;

        WinRegistry Registro = new WinRegistry("Assecont", "Asseponto4");

        private const int opExclusao = 0;
        private const int opInclusao = 1;
        private const int opAlteracao = 2;

        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
            consoleLog = edLog;
        }

        #region Overrides Abstract
        public override int     getPortaPadrao()                { return 5000; }
        public override string  getRepFabricante()              { return "ZPM"; }
        public override string  getArquivoUsb()                 { return "ZPM"; }
        public override bool    getGerarUsb()                   { return true; }
        public override bool    getGerarBackup()                { return false; }
        public override bool    getEnviarHorarioVerao()         { return true; }
        public override bool    getGerarBiometria()             { return true; }
        public override bool    getEnviarHorarioVeraoUsb()      { return true; }
        public override bool    getCadastroTerminalSupervisor() { return false; }
        public override bool    getFuncionariosAlteradosUsb()   { return false; }
        public override bool    getGerarUsbEmpregador()         { return true; }
        public override bool    getNumeroSerieREP()             { return true; }
        public override bool    getContemChaveAcessoREP()       { return false; }
        public override bool    getCadastroTerminalResponsavel() { return false; }
        public override bool    getBoxFuncoes() { return false; }
        public override bool getAutenticacao() { return false; }
        public override bool getPin(){ return false; }
        public override bool getDisconnectOnExit(){ return false; }
        #endregion

        #region CONEXAO
        public override bool Connect(int Terminal)
        {
            base.Connect(Terminal);
            REPZPM_DLL.Handle = REPZPM_DLL.DLLREP_IniciaDriver(TerminalDados.Serial); //verifica o numero de fabricação

            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_DefineModoIP(REPZPM_DLL.Handle, TerminalDados.IP, TerminalDados.Porta); //Conecta o ip e poa
            int ret = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, REPZPM_DLL.Retorno);

            string Mensagem;

            if (ret != 0)
            {
                Mensagem = Convert.ToString(REPZPM_DLL.Trata_Retorno_REP(ret)); //ERRO
                REPZPM_DLL.Modo = 0;
                log.AddLog(Mensagem);
                return false;
            }
            else
            {
                Mensagem = Convert.ToString(REPZPM_DLL.Trata_Retorno_REP(ret)); //CONECTADO
                log.AddLog("CONEXÃO INICIADA...");
                return true;
            }
        }

        public override bool Disconnect(int Terminal)
        {
            base.Disconnect(Terminal);
            int ret = REPZPM_DLL.DLLREP_EncerraDriver(REPZPM_DLL.Handle);
            if (ret == 1)
                return true;
            else
                return false;
        }

        //private bool Connect(string Ip, int Porta)
        //{
        //    REPZPM_DLL.Handle = REPZPM_DLL.DLLREP_IniciaDriver(TerminalDados.Serial); //verifica o numero de fabricação

        //    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_DefineModoIP(REPZPM_DLL.Handle, Ip, Porta); //Conecta o ip e poa
        //    string Mensagem;

        //    if (REPZPM_DLL.Retorno == 1)
        //    {
        //        Mensagem = Convert.ToString(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.Retorno)); //ERRO
        //        REPZPM_DLL.Modo = 0;
        //        return false;
        //    }
        //    else
        //    {
        //        Mensagem = Convert.ToString(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.Retorno)); //CONECTADO
        //        return true;
        //    }

        //    log.AddLog(Mensagem);
        //}
        #endregion

        #region Override ENVIAR DATA E HORA
        public override bool sendDataHora(int Terminal)
        {
            if (!base.sendDataHora(Terminal)) return false;

            Connect(Terminal);  //IP E PORTA DO PAI)

            int conta;
            string DataHora = DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss");

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_AtualizaDataHora(REPZPM_DLL.Handle, DataHora);

            if (REPZPM_DLL.ID_Comando > 0)
            {
                NRegistros = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);
                conta = 1;

                while (conta <= NRegistros)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, conta);

                    if (REPZPM_DLL.Retorno != 0)
                    {
                        log.AddLog(Convert.ToString(REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno)), true);
                        Disconnect(Terminal);
                        return false;
                    }
                    conta++;
                }

                if (REPZPM_DLL.Retorno != 0)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    Disconnect(Terminal);
                    return false;
                }
                else
                {
                    log.AddLog(Consts.DATA_HORA_ATUALIZADA_SUCESSO, true, true);
                }
            }
            else
            {
                log.AddLog(Convert.ToString(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando)));
            }
            Disconnect(Terminal);
            return true;

        }
        #endregion

        #region Override ENVIAR EMPREGADOR
        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            if (!base.sendInfoEmpresa(Terminal, out EmpregadorDados)) return false;

            Connect(Terminal);

            string tempPessoaTipo = "";
            string tempCnpj = "";
            string tempNome = "";
            string tempCei = "";
            string tempEndereco = "";
            int Operacao;

            getEmpregador(out tempPessoaTipo, out tempCnpj, out tempNome, out tempCei, out tempEndereco);

            //getEmpregador(out EmpregadorDados.PessoaTipo, out EmpregadorDados.Pessoa, out EmpregadorDados.Nome, out EmpregadorDados.Cei, out EmpregadorDados.Endereco);

            Operacao = EmpregadorDados.Nome != "" ? opAlteracao : opInclusao;
            log.AddLog(Consts.EMPREGADOR_ENVIANDO);

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Empregador(REPZPM_DLL.Handle, Operacao, Convert.ToString((int)EmpregadorDados.PessoaTipo == 1 ? 'j' : 'F'), EmpregadorDados.Pessoa, EmpregadorDados.Cei, EmpregadorDados.Nome, EmpregadorDados.Endereco);

            int ret = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, REPZPM_DLL.ID_Comando);

            /*Sucesso na execução do comando*/
            if (ret == 0)
            {
                /*Retorna a quantidade de retornos do comando enviado*/
                NRegistros = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, NRegistros);

                /*Houve erro*/
                if (REPZPM_DLL.Retorno != 0)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                    Disconnect(Terminal);
                    return false;
                }
                /*Houve sucesso no envio do comando*/
                else
                {
                    log.AddLog(Consts.EMPREGADOR_ENVIADO_SUCESSO);
                }

            }
            /*Trata o erro retornado pela DLL*/
            else
            {
                log.AddLog(REPZPM_DLL.Trata_Retorno_REP(ret), true);
            }
            Disconnect(Terminal);
            return true;
        }

        private void getEmpregador(out string PessoaTipo, out string Cnpj, out string Nome, out string Cei, out string Endereco)
        {
            PessoaTipo = "";
            Cnpj = "";
            Nome = "";
            Cei = "";
            Endereco = "";

            Types.Empregador EmpregadorDados;

            StringBuilder Tipo = new StringBuilder(1);
            StringBuilder Identificacao = new StringBuilder(14);
            StringBuilder CEI = new StringBuilder(12);
            StringBuilder RazaoSocial = new StringBuilder(150);
            StringBuilder LocalTrabalho = new StringBuilder(100);
            StringBuilder MensagemErro = new StringBuilder(256);

            //Connect(TerminalDados.Indice);

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaEmpregador(REPZPM_DLL.Handle);

            if (REPZPM_DLL.ID_Comando > 0)
            {
                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoEmpregador(REPZPM_DLL.Handle, Tipo, Identificacao, CEI, RazaoSocial, LocalTrabalho);

                int tipo = Tipo.ToString() == 'J'.ToString() ? 1 : 2;

                /*Sucesso na leitura do empregador*/
                if (REPZPM_DLL.Retorno == 1)
                {
                    /*Mostra as informações do Empregador*/
                    EmpregadorDados.PessoaTipo = (Types.PessoaTipo)tipo;
                    EmpregadorDados.Pessoa = Identificacao.ToString();
                    EmpregadorDados.Nome = RazaoSocial.ToString();
                    EmpregadorDados.Cei = CEI.ToString();
                    EmpregadorDados.Endereco = LocalTrabalho.ToString();
                }
                else
                {
                    /*Houve erro na leitura do empregador*/
                    log.AddLog(REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno), true);
                }
            }
            else
            {
                /*Trata o erro na inicialização do Handle*/
                log.AddLog(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando));
            }
        }
        #endregion

        #region Override ENVIAR FUNCIONARIO
        public override bool sendFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {
            bool Result = base.sendFuncionario(Funcionario);

            //Connect(TerminalDados.Indice);

            int Operacao;
            bool Ok = false;
            bool Teclado = true;

            string Habilitar_Teclado = Teclado ? "S" : "N";
            string tempFuncionarioNome = "";
            string tempCracha = "";
            bool tempTeclado = false;
            string tempCodigoTeclado = "";
            string tempCodigoBarras = "";
            string tempCodigoMifare = "";
            string tempCodigoTag = "";

            string CodigoMifare = "";
            string CodigoTag = "";

            getFuncionario(Funcionario.Pis, out tempFuncionarioNome, out tempCracha, out tempTeclado,
                out tempCodigoTeclado, out tempCodigoBarras, out tempCodigoMifare, out tempCodigoTag);

            Operacao = tempFuncionarioNome != String.Empty ? opAlteracao : opInclusao;

            /*Prepara o envio do cadastro do funcionário*/

    //        if (Funcionario.Crachas.Count > 0)
    //        {
    //            foreach (long Cracha in Funcionario.Crachas)
    //            {
    //                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, Operacao, Funcionario.Pis, Convert.ToString(Funcionario.Ind), Funcionario.Nome, ""                       ,  Habilitar_Teclado, Convert.ToString(Cracha), Convert.ToString(Cracha), CodigoMifare       , CodigoTag       );
    //                                                                        //(int Handle,        Operacao, string PIS     , string Matricula                 , string Nome     , string TemplateBiometrico, string PIS_Teclado, string CodigoTeclado    , string CodigoBarra      , string CodigoMifare, string CodigoTAG);
    //}
    //        }
    //        else
    //            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, Operacao, Funcionario.Pis, "", Funcionario.Nome, "", Habilitar_Teclado, "", "", CodigoMifare, CodigoTag);
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, Operacao, Funcionario.Pis, Funcionario.Ind.ToString(), Funcionario.Nome, "", Habilitar_Teclado, Funcionario.Teclado, Funcionario.Barras, CodigoMifare, CodigoTag);

            if (REPZPM_DLL.Retorno == 1)
            {
                REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Funcionario_Envia(REPZPM_DLL.Handle);

                if (REPZPM_DLL.ID_Comando > 0)
                {
                    if (REPZPM_DLL.Modo == 0)
                    {
                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, 1);

                        /*Se Retorno for <> de 0, então houve erro na execução do comando de cadastro de funcionário*/
                        if (REPZPM_DLL.Retorno != 0)
                        {
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                        }
                        else
                        {
                            Ok = true;
                        }
                    }
                }
            }
            else
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                return false;
            }
            return Ok;
        }

        private void getFuncionario(string Pis, out string FuncionarioNome, out string Cracha, out bool Teclado, out string CodigoTeclado,
            out string CodigoBarras, out string CodigoMifare, out string CodigoTag)
        {
            StringBuilder sPIS = new StringBuilder(11);
            StringBuilder sMatricula = new StringBuilder(20);
            StringBuilder sNomeFuncionario = new StringBuilder(52);
            StringBuilder sBiometrico = new StringBuilder(20000);    //Valor definido baseado na maior template possivel de ser gerada c/ 10 digitais cadastradas.
            StringBuilder sHabilitaTeclado = new StringBuilder(1);
            StringBuilder sCodigoTeclado = new StringBuilder(16);
            StringBuilder sCodigoBarras = new StringBuilder(20);
            StringBuilder sCodigoMIFARE = new StringBuilder(20);
            StringBuilder sCodigoTAG = new StringBuilder(20);
            StringBuilder MensagemErro = new StringBuilder(256);

            Cracha = String.Empty;
            FuncionarioNome = String.Empty;
            Teclado = false;
            CodigoTeclado = String.Empty;
            CodigoBarras = String.Empty;
            CodigoMifare = String.Empty;
            CodigoTag = String.Empty;

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaFuncionario(REPZPM_DLL.Handle, Pis);

            if (REPZPM_DLL.ID_Comando > 0)
            {
                if (REPZPM_DLL.Modo == 0)
                {
                    NRegistros = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

                    if (NRegistros > 0)
                    {
                        for (int i = 1; i <= NRegistros; i++)
                        {
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoFuncionario(REPZPM_DLL.Handle, i, sPIS, sMatricula, sNomeFuncionario, sBiometrico, sHabilitaTeclado, sCodigoTeclado, sCodigoBarras, sCodigoMIFARE, sCodigoTAG);

                            if (REPZPM_DLL.Retorno == 1)
                            {
                                log.AddLog("Funcionario Já existe no termianl");
                            }
                            else
                            {
                                log.AddLog(REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno), true);
                                return;
                            }
                        }
                    }
                    else
                    {
                        log.AddLog("Não há funcionário cadastrado no REP");
                        return;
                    }
                }
            }
            else
            {
                /*Houve erro no processamento do Handle*/
                log.AddLog(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando));
                return;
            }
        }

        #endregion

        #region Override IMPORTA MARCACOES
        public override bool LerMarcacoes(Marcacoes marcacoes, AssepontoRep.Bridge.TipoImportacaoMarcacoes tipoimportacao)
        {
            Connect(TerminalDados.Indice);

            bool Result = false;
            int NRegistrosLidos = 0;

            DateTime DataInicial;
            DateTime DataFinal;

            StringBuilder PIS = new StringBuilder(11);
            StringBuilder DataHora = new StringBuilder(19);
            StringBuilder NSR = new StringBuilder(20);
            StringBuilder MensagemErro = new StringBuilder(256);

            //Marcacoes marcacoes = new Marcacoes(TerminalDados);

            if (!getPeriodo(out DataInicial, out DataFinal)) return false;
            //REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaPonto(REPZPM_DLL.Handle, DataInicial.ToString("dd/MM/yyyy"), DataFinal.ToString("dd/MM/yyyy"));
            
            //if (REPZPM_DLL.ID_Comando == -4)
            //{
            int count = 1;
            do
            {
                REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaPonto(REPZPM_DLL.Handle, DataInicial.ToString("dd/MM/yyyy"), DataFinal.ToString("dd/MM/yyyy"));
                if (REPZPM_DLL.ID_Comando == -4)
                {
                    int timeout = REPZPM_DLL.DLLREP_LeTimeout(REPZPM_DLL.Handle);
                    if (timeout != -1)
                        REPZPM_DLL.DLLREP_DefineTimeout(REPZPM_DLL.Handle, (timeout * (count + 1)));
                    else
                        REPZPM_DLL.DLLREP_DefineTimeout(REPZPM_DLL.Handle, 100000);
                }
                count++;
            }
            while (REPZPM_DLL.ID_Comando == -4 && count <= 11);
            //}


            if (REPZPM_DLL.ID_Comando >= 0)
            {
                /*Retorna a quantidade de registros*/
                NRegistros = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

                log.AddLog(String.Format(Consts.MARCACOES_A_PROCESSAR, NRegistros));

                if (NRegistros > 0)
                {
                    for (int i = 1; i <= NRegistros; i++)
                    {
                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoPonto(REPZPM_DLL.Handle, i, PIS, DataHora, NSR);

                        if (REPZPM_DLL.Retorno > 0)
                        {
                            marcacoes.Add(PIS.ToString(), Convert.ToDateTime(DataHora.ToString()), Convert.ToInt32(NSR.ToString()));
                            log.AddLogUnformatted(PIS.ToString().PadRight(18) + DataHora + " " + NSR);
                            NRegistrosLidos++;
                        }
                        else
                        {
                            //REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                            //log.AddLog(Convert.ToString(MensagemErro));
                            int ret = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, REPZPM_DLL.Retorno);
                            log.AddLog(REPZPM_DLL.Trata_Retorno_DLL(ret));
                        }
                    }
                }
                else
                    log.AddLog(Consts.MARCACOES_NAO_HA_NOVAS);

                Result = (NRegistrosLidos > 0);

                if (Result)
                {
                    marcacoes.SaveToFile();
                    log.AddLogUnformatted(String.Format(Consts.ARQUIVO_GERADO, marcacoes.Arquivo));
                    log.AddLineBreak();
                }
                else
                    log.AddLog(Consts.MARCACOES_NAO_HA_NOVAS);
            }
            else
            {
                Result = false;
                log.AddLog(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando));
            }

            Disconnect(TerminalDados.Indice);
            return Result;
        }

        private bool getPeriodo(out DateTime DataInicial, out DateTime DataFinal)
        {
            bool Result = false;

            if (!isTask)
            {
                ImportacaoMarcacoes frmImportacaoMarcacoes = new ImportacaoMarcacoes();

                DataInicial = Convert.ToDateTime(Registro.readValue(getRepFabricante(), "Data inicial importacao", DateTime.Today.ToShortDateString()));
                DataFinal = Convert.ToDateTime(Registro.readValue(getRepFabricante(), "Data Final importacao", DateTime.Today.ToShortDateString()));

                frmImportacaoMarcacoes.edDataInicial.Value = DataInicial;
                frmImportacaoMarcacoes.edDataFinal.Value = DataFinal;

                if (frmImportacaoMarcacoes.ShowDialog() == DialogResult.OK)
                {
                    DataInicial = frmImportacaoMarcacoes.edDataInicial.Value;
                    DataFinal = frmImportacaoMarcacoes.edDataFinal.Value;

                    Registro.writeValue(getRepFabricante(), "Data inicial importacao", DataInicial.ToShortDateString());
                    Registro.writeValue(getRepFabricante(), "Data Final importacao", DataFinal.ToShortDateString());

                    Result = true;
                }
            }
            else
            {
                DataInicial = DateTime.Now.AddDays(-1);
                DataFinal = DateTime.Now;
                Result = true;
            }

            return Result;
        }
        #endregion

        #region Override DELETAR FUNCIONARIO
        public override bool deleteFuncionario(AssepontoRep.Types.Funcionario Funcionario)
        {

            bool Ok = base.deleteFuncionario(Funcionario);

            //Connect(TerminalDados.Indice);

            /******************************************************************************************************************************
            'INCLUSÃO DE FUNCIONÁRIO                                                                                                      *
            '******************************************************************************************************************************/
            /*Prepara o envio do cadastro do funcionário*/
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, opExclusao, Funcionario.Pis, "", "", "", "", "", "", "", "");

            /*Comando executado*/
            if (REPZPM_DLL.Retorno == 1)
            {
                REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Funcionario_Envia(REPZPM_DLL.Handle);

                /*Comando de cadastro de funcionário foi enviado com sucesso se for maior que 0*/
                if (REPZPM_DLL.ID_Comando > 0)
                {
                    if (REPZPM_DLL.Modo == 0)
                    {
                        /*Obtém o código de erro*/
                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, 1);

                        /*Se Retorno for <> de 0, então houve erro na execução do comando de cadastro de funcionário*/
                        if (REPZPM_DLL.Retorno != 0)
                        {
                            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                            log.AddLog(Convert.ToString(MensagemErro), true);
                        }
                        else
                        {
                            log.AddLog(REPZPM_DLL.Trata_Retorno_REP(REPZPM_DLL.Retorno), true, true);
                            Ok = true;
                        }
                    }
                }
                else
                {
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO, true);
                }
            }
            else
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO, true);
            }

            return Ok;
        }
        #endregion

        #region Override USB Funcionario
        public override void UsbFileFuncionario(Types.Funcionario Funcionario, Types.Opcao Opcao, ref string Line)
        {
            REPZPM_DLL.Handle = REPZPM_DLL.DLLREP_IniciaDriver(TerminalDados.Serial); //verifica o numero de fabricação
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_DefineModoArquivo(REPZPM_DLL.Handle, AssepontoRep.Folders.folderArquivoUsb());
            REPZPM_DLL.Modo = 1;

            bool Teclado = false;
            string Habilitar_Teclado = Teclado ? "S" : "N";
            string CodigoMifare = "";
            string CodigoTag = "";


            if (Funcionario.Crachas.Count > 0)
            {
                foreach (long Cracha in Funcionario.Crachas)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, opInclusao, Funcionario.Pis, Convert.ToString(Cracha), Funcionario.Nome, "", Habilitar_Teclado, Convert.ToString(Cracha), Convert.ToString(Cracha), CodigoMifare, CodigoTag);
                }
            }
            else
                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, opInclusao, Funcionario.Pis, "", Funcionario.Nome, "", Habilitar_Teclado, "", "", CodigoMifare, CodigoTag);


            /*Comando executado*/
            if (REPZPM_DLL.Retorno == 1)
            {
                REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Funcionario_Envia(REPZPM_DLL.Handle);

                /*Comando de cadastro de funcionário foi enviado com sucesso se for maior que 0*/
                if (REPZPM_DLL.ID_Comando > 0)
                {
                    if (REPZPM_DLL.Modo == 1)
                    {
                        /*Obtém o código de erro*/
                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_VerificaRetornoPenDrive(REPZPM_DLL.Handle, REPZPM_DLL.ID_Comando);
                    }
                    else
                    {
                        /*Erro na execução do comando de envio de funcionário pela DLL*/
                        REPZPM_DLL.Retorno = Convert.ToInt32(REPZPM_DLL.Trata_Retorno_DLL(REPZPM_DLL.ID_Comando));
                        return;
                    }
                }
                else
                {
                    log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                }
            }
            else
            {
                log.AddLog(Consts.ERRO_ENVIO_COMANDO);

            }
        }

        public override void UsbFileInicializacao(string Folder)
        {
            List<string> Arquivos = new List<string>();

            Files.DirSearch(Folder, "*.*", Arquivos);

            foreach (string arquivo in Arquivos)
            {
                File.Delete(arquivo);
            }
        }

        public override void UsbFileFinalizacao(string Folder)
        {
            List<string> Arquivos = new List<string>();
            List<string> Novo = new List<string>();
            Novo.Add("2;1");

            Files.DirSearch(Folder, "*.*", Arquivos);

            foreach (string arquivo in Arquivos)
            {
                List<string> conteudo = new List<string>();
                Files.ReadFile(arquivo, conteudo);
                string s = conteudo[1];
                Novo.Add(s);
            }

            Files.WriteFile(Folder + getRepFabricante() + "User.cmd", Novo);
        }

        #endregion

        #region Override Horario de Verão
        public override bool EnviarHorarioVerao(int Terminal, string Inicio, string Fim)
        {
            if (!base.EnviarHorarioVerao(Terminal, "", "")) return false;

            Connect(TerminalDados.Indice);

            int Total_Retornos;
            StringBuilder MensagemErro = new StringBuilder(256);
            int conta = 0;

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_AjustaHorarioVerao(REPZPM_DLL.Handle, Inicio, Fim);

            if (REPZPM_DLL.ID_Comando > 0)
            {
                Total_Retornos = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);
                conta++;

                while (conta <= Total_Retornos)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, conta);

                    if (REPZPM_DLL.Retorno != 0)
                    {
                        log.AddLog(Consts.ERRO_ENVIO_COMANDO);
                        Disconnect(Terminal);
                        return false;
                    }
                    conta++;
                }

                if (REPZPM_DLL.Retorno != 0)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                    MessageBox.Show(Convert.ToString(MensagemErro), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Disconnect(Terminal);
                    return false;
                }
                else
                {
                    log.AddLog(Consts.HORARIO_VERAO_ENVIADO_SUCESSO);
                    Disconnect(Terminal);
                    return true;
                }
            }
            log.AddLog(Consts.ERRO_ENVIO_COMANDO);
            Disconnect(Terminal);
            return false;
        }
        #endregion

        #region Override Horario de Verao USB
        public override void EnviarHorarioVeraoUsb(int Terminal, string Inicio, string Fim)
        {
            List<string> Arquivos = new List<string>();
            List<string> Novo = new List<string>();

            log.AddLog(Consts.HORARIO_VERAO_GERANDO_ARQUIVO);

            string Folder = Folders.folderArquivoHorarioVerao(getRepFabricante());

            Files.DirSearch(Folder, "*.*", Arquivos);

            foreach (string arquivo in Arquivos)
            {
                File.Delete(arquivo);
            }

            Novo.Add("8;1");
            Novo.Add(Inicio + ";" + Fim);

            Files.WriteFile(Folder + getRepFabricante() + "verao.cmd", Novo);
            //return true;
        }
        #endregion

        #region Override Recolher Biometria
        public override bool GerarArquivoBiometria(int Terminal)
        {
            List<Types.Biometria> listBiometrias = new List<Types.Biometria>();
            Types.Biometria func = new Types.Biometria();

            StringBuilder PIS = new StringBuilder(11);
            StringBuilder Matricula = new StringBuilder(20);
            StringBuilder NomeFuncionario = new StringBuilder(52);
            StringBuilder Biometrico = new StringBuilder(20000);    //Valor definido baseado na maior template possivel de ser gerada c/ 10 digitais cadastradas.
            StringBuilder HabilitaTeclado = new StringBuilder(1);
            StringBuilder CodigoTeclado = new StringBuilder(16);
            StringBuilder CodigoBarras = new StringBuilder(20);
            StringBuilder CodigoMIFARE = new StringBuilder(20);
            StringBuilder CodigoTAG = new StringBuilder(20);
            StringBuilder MensagemErro = new StringBuilder(256);

            //string sBiometrico, bio1 = "", bio2;
            //int tamanhoTemplate, tamanhoBiometrico, pos, pos_anterior;
            int i = 0;
            long cracha = 0;
            //Int64 decValue;

            if (!base.GerarArquivoBiometria(Terminal)) return false;

            Connect(TerminalDados.Indice);

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_BuscaTodosFuncionarios(REPZPM_DLL.Handle);

            int TotalRegistros = REPZPM_DLL.DLLREP_TotalRetornos(REPZPM_DLL.Handle);

            if (TotalRegistros > 0)
            {
                for (i = 1; i <= TotalRegistros; i++)
                {
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_RetornoFuncionario(REPZPM_DLL.Handle, i, PIS, Matricula, NomeFuncionario, Biometrico, HabilitaTeclado, CodigoTeclado, CodigoBarras, CodigoMIFARE, CodigoTAG);

                    if (REPZPM_DLL.Retorno == 1)
                    {
                        func.Funcionario.Pis = PIS.ToString();
                        func.Funcionario.Nome = NomeFuncionario.ToString();
                        func.template = Biometrico.ToString();
                        func.tecladoHabilitado = HabilitaTeclado.ToString();
                        func.Funcionario.Matricula = Matricula.ToString();
                        func.Funcionario.TecladoPassword = CodigoTeclado.ToString();

                        func.Funcionario.Crachas = new List<long>();

                        if (CodigoBarras.ToString() != "")
                        {
                            cracha = long.Parse(CodigoBarras.ToString());
                        }

                        if (cracha != 0)
                        {
                            func.Funcionario.Crachas.Add(cracha);
                        }

                        listBiometrias.Add(func);
                    }
                    else
                    {
                        log.AddLog(Consts.ERRO_ENVIO_COMANDO_CODIGO);
                        return false;
                    }
                }
            }
            else
            {
                log.AddLog("NÃO HÁ FUNCIONARIOS CADASTRADOS NO REP");
                return false;
            }

            string folder = Folders.folderArquivoBiometria(getRepFabricante());
            FinalizarArquivoBiometria(folder, listBiometrias);
            log.AddLog(Consts.BIOMETRIA_ARQUIVO_GERADO_SUCESSO);
            return true;
        }

        public override void FinalizarArquivoBiometria(string Folder, List<AssepontoRep.Types.Biometria> listBiometrias)
        {
            List<string> Arquivos = new List<string>();
            Files.DirSearch(Folder, "*.*", Arquivos);

            foreach (string arquivo in Arquivos)
            {
                File.Delete(arquivo);
            }

            Arquivos = new List<string>();

            foreach (Types.Biometria arquivo in listBiometrias)
            {
                Arquivos.Add(arquivo.Funcionario.Pis);
                Arquivos.Add(arquivo.Funcionario.Matricula);
                Arquivos.Add(arquivo.Funcionario.Nome);
                Arquivos.Add(arquivo.template);
                Arquivos.Add(arquivo.tecladoHabilitado);
                Arquivos.Add(arquivo.Funcionario.TecladoPassword);
                if (arquivo.Funcionario.Crachas.Count > 0)
                {
                    foreach (long cracha in arquivo.Funcionario.Crachas)
                    {
                        Arquivos.Add(cracha.ToString());
                    }
                }
                else
                    Arquivos.Add("");
                Arquivos.Add("+");
            }

            Files.WriteFile(Folder + "biometria.txt", Arquivos);
        }
        #endregion

        #region Override Enviar Biometria
        public override bool EnviarArquivoBiometria(int Terminal)
        {
            if (!base.EnviarArquivoBiometria(Terminal)) return false;

            Connect(Terminal);
            StringBuilder MensagemErro = new StringBuilder(256);
            int contadoLinha = 0, Operacao = 2;
            List<string> Arquivos = new List<string>();
            List<AssepontoRep.Types.Biometria> listBiometrias = new List<Types.Biometria>();
            Types.Biometria Biometria = new Types.Biometria();

            string folder = AssepontoRep.Folders.folderArquivoBiometria(getRepFabricante());
            Files.ReadFile(folder + "biometria.txt", Arquivos);

            foreach (string linha in Arquivos)
            {
                contadoLinha++;
                if (contadoLinha == 1)
                {
                    Biometria.Funcionario.Pis = linha.Substring(0, linha.Length);
                }
                if (contadoLinha == 2)
                {
                    Biometria.Funcionario.Matricula = linha.Substring(0, linha.Length);
                }
                if (contadoLinha == 3)
                {
                    Biometria.Funcionario.Nome = linha.Substring(0, linha.Length);
                }
                if (contadoLinha == 4)
                {
                    Biometria.template = linha.Substring(0, linha.Length);
                }
                if (contadoLinha == 5)
                {
                    Biometria.tecladoHabilitado = linha.Substring(0, 1);
                }
                if (contadoLinha == 6)
                {
                    Biometria.Funcionario.TecladoPassword = linha.Substring(0, linha.Length);
                }
                if (contadoLinha == 7)
                {
                    if (linha != "")
                    {
                        Biometria.Funcionario.Crachas = new List<long>();
                        Biometria.Funcionario.Crachas.Add(long.Parse(linha.Substring(0, linha.Length)));
                    }
                }
                if (contadoLinha == 8)
                {
                    listBiometrias.Add(Biometria);
                    contadoLinha = 0;
                }
            }

            foreach (Types.Biometria arquivo in listBiometrias)
            {
                Biometria.Funcionario.Crachas = new List<long>();
                Biometria.Funcionario.Crachas = arquivo.Funcionario.Crachas;

                if (Biometria.Funcionario.Crachas != null)
                {
                    foreach (long cracha in arquivo.Funcionario.Crachas)
                    {
                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, Operacao, arquivo.Funcionario.Pis, arquivo.Funcionario.Matricula, arquivo.Funcionario.Nome, arquivo.template, arquivo.tecladoHabilitado, arquivo.Funcionario.TecladoPassword, cracha.ToString(), cracha.ToString(), cracha.ToString());
                    }
                }
                else
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_Funcionario_Prepara(REPZPM_DLL.Handle, Operacao, arquivo.Funcionario.Pis, arquivo.Funcionario.Matricula, arquivo.Funcionario.Nome, arquivo.template, arquivo.tecladoHabilitado, arquivo.Funcionario.TecladoPassword, "", "", "");

                if (REPZPM_DLL.Retorno == 1)
                {
                    REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Funcionario_Envia(REPZPM_DLL.Handle);

                    if (REPZPM_DLL.ID_Comando > 0)
                    {
                        REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemCodigoErro(REPZPM_DLL.Handle, 1);

                        if (REPZPM_DLL.Retorno != 0)
                        {
                            log.AddLog(Consts.ERRO_ENVIO_COMANDO_CODIGO);
                            Disconnect(Terminal);
                            return false;
                        }
                    }
                }
            }
            log.AddLog(Consts.BIOMETRIA_ARQUIVO_ENVIADO_SUCESSO);
            Disconnect(Terminal);
            return true;
        }
        #endregion

        #region Override EnviarEmpregadorPendrive
        public override bool GerarArquivoEmpregador(int Terminal, Types.Opcao opcao, out Types.Empregador EmpregadorDados)
        {
            if (!base.GerarArquivoEmpregador(Terminal, opcao, out EmpregadorDados)) return false;

            REPZPM_DLL.Handle = REPZPM_DLL.DLLREP_IniciaDriver(TerminalDados.Serial); //verifica o numero de fabricação
            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_DefineModoArquivo(REPZPM_DLL.Handle, AssepontoRep.Folders.folderArquivoUsb());
            REPZPM_DLL.Modo = 1;

            string tempPessoaTipo = (int)EmpregadorDados.PessoaTipo == 1 ? "J" : "F";
            string tempCnpj = EmpregadorDados.Pessoa;
            string tempNome = EmpregadorDados.Nome;
            string tempCei = EmpregadorDados.Cei;
            string tempEndereco = EmpregadorDados.Endereco;

            REPZPM_DLL.ID_Comando = REPZPM_DLL.DLLREP_Empregador(REPZPM_DLL.Handle, (int)opcao, tempPessoaTipo, tempCnpj, tempCei, tempNome, tempEndereco);

            REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_VerificaRetornoPenDrive(REPZPM_DLL.Handle, REPZPM_DLL.ID_Comando);

            /*Verifica se o retorno já está disponível*/
            if (REPZPM_DLL.Retorno < 0)
            {
                /*Verifica no arquivo do pendrive se houve erro na execução do comando*/
                REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);

                /*Comando executado*/
                if (REPZPM_DLL.Retorno == 0)
                {
                    log.AddLog(Consts.USB_EMPEGADOR_SUCESSO);
                    return true;
                }
                else
                {
                    /*Houve erro no retorno do comando via pendrive*/
                    REPZPM_DLL.Retorno = REPZPM_DLL.DLLREP_ObtemMensagemErro(REPZPM_DLL.Handle, MensagemErro, 1);
                    return false;
                }
            }
            return false;
        }
        #endregion
    }
}
