using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using RepTrilobit;
using System.Data;

namespace Trilobit
{
    public class Rede
    {
        public REP Rep;

        string IP;
        int Porta;
        TextBox Log;
        int Senha;

        public Rede(string ip, int porta, TextBox log, int Senha)
        {
            Rep = new REP();

            this.IP = ip;
            this.Porta = porta;
            this.Log = log;
            this.Senha = Senha;
        }

        public Rede(TextBox log)
        {
            this.Log = log;
        }

        public void AddLog(string Mensagem, bool NewLine = false)
        {
            if (Log.Text.Length > 0) Log.AppendText(Environment.NewLine);
            Log.AppendText(IP + ": " + String.Format("{0:dd/MM HH:mm}", DateTime.Now) + " " + Mensagem.ToUpper());
            if (NewLine == true) Log.AppendText(Environment.NewLine);
        }

        public void AddLogUnformatted(string Mensagem)
        {
            if (Log.Text.Length > 0) Log.AppendText(Environment.NewLine);
            Log.AppendText(Mensagem);
        }

        public void LogSucesso()
        {
            this.AddLog("Comando enviado com sucesso", true);
        }

        public void LogErro()
        {
            this.AddLog(Rep.ErrorException.Message, true);
        }

        //public void AddLogStatus(int Status)
        //{
        //    switch (Status)
        //    {
        //        case 0:
        //        case 1:
        //        case 2:
        //        case 3:
        //        case 4:
        //        case 5:
        //            break;
        //        case -1:
        //            this.AddLog("Cartão de Proximidade já cadastrado para outro usuário.");
        //            break;
        //        case -2:
        //            this.AddLog("Cartão de Código de Barras já cadastrado para outro usuário.");
        //            break;
        //        case -3:
        //            this.AddLog("PIS já cadastrado para outro usuário.");
        //            break;
        //        case -4:
        //            this.AddLog("Código individual já cadastrado para outro usuário.");
        //            break;
        //        case -5:
        //            this.AddLog("Erro na memória MRP.");
        //            break;
        //        case -6:
        //            this.AddLog("Erro na memória MT.");
        //            break;
        //        case -7:
        //            this.AddLog("Erro na memória RAM.");
        //            break;
        //        case -8:
        //            this.AddLog("Dados enviados inválidos.");
        //            break;
        //        case -9:
        //            this.AddLog("ID REP não possui trabalhadores cadastrados.");
        //            break;
        //        case -10:
        //            this.AddLog("Trabalhador não cadastrado.");
        //            break;
        //        case -11:
        //            this.AddLog("ID REP não possui o cadastro do empregador.");
        //            break;
        //        case -12:
        //            this.AddLog("Dados do empregador inválidos: CPF / CNPJ.");
        //            break;
        //        case -13:
        //            this.AddLog("Dados do empregador inválidos: Nome / Razão Social.");
        //            break;
        //        case -14:
        //            this.AddLog("Dados do empregador inválidos: Endereço.");
        //            break;
        //        case -15:
        //            this.AddLog("Data e/ou hora inválida(s).");
        //            break;
        //        case -16:
        //            this.AddLog("Erro no módulo biométrico: ERROR.");
        //            break;
        //        case -17:
        //            this.AddLog("Erro no módulo biométrico: TIMEOUT.");
        //            break;
        //        case -18:
        //            this.AddLog("Dados de comunicação inválidos: Endereço IP.");
        //            break;
        //        case -19:
        //            this.AddLog("Dados de comunicação inválidos: Máscara de sub-rede.");
        //            break;
        //        case -20:
        //            this.AddLog("Dados de comunicação inválidos: IP Gateway.");
        //            break;
        //        case -21:
        //            this.AddLog("Não existem eventos.");
        //            break;
        //        case -22:
        //            this.AddLog("Erro no módulo biométrico: CHEIO.");
        //            break;
        //        case -23:
        //            this.AddLog("Erro na leitura do módulo biométrico: ERROR.");
        //            break;
        //        case -24:
        //            this.AddLog("Erro na leitura do módulo biométrico: TIMEOUT.");
        //            break;
        //        case -25:
        //            this.AddLog("Erro de checksum da área de dados.");
        //            break;
        //        case -26:
        //            this.AddLog("Dados do empregador inválidos: CEI.");
        //            break;
        //        case -27:
        //            this.AddLog("Equipamento bloqueado.");
        //            break;
        //        case -100:
        //            this.AddLog("Erro no checksum do cabeçalho do pacote (Verificação na DLL).");
        //            break;
        //        case -101:
        //            this.AddLog("Erro no checksum dos dados do pacote (Verificação na DLL).");
        //            break;
        //        case -102:
        //            this.AddLog("Comando inválido (Verificação na DLL).");
        //            break;
        //        case -103:
        //            this.AddLog("Erro pacote inválido (Verificação na DLL).");
        //            break;
        //        case -104:
        //            this.AddLog("Erro no tamanho do pacote: pacote vazio (Verificação na DLL).");
        //            break;
        //        case -105:
        //            this.AddLog("Erro no tamanho dos dados (Verificação na DLL).");
        //            break;
        //        default:
        //            this.AddLog("Erro desconhecido.");
        //            break;
        //    }
        //}

        public void Trilobit_EnviaEmpresa(string CNPJ, string CPF, string Nome, ulong Cei, string Endereco)
        {
            this.AddLog("ENVIANDO CADASTRO DO EMPREGADOR");

            REP.eTipoDocumento IdentificadorTipo;
            string Identificador;

            if (CNPJ != "")
            {
                IdentificadorTipo = REP.eTipoDocumento.CNPJ;
                Identificador = CNPJ;
            }
            else
            {
                IdentificadorTipo = REP.eTipoDocumento.CPF;
                Identificador = CPF;
            }

            if (Rep.CadastrarEmpregador(IP, Porta, Senha, IdentificadorTipo, Identificador, Cei.ToString(), Nome, Endereco))
                this.AddLog("CADASTRO DO EMPREGADOR ENVIADO COM SUCESSO");
            else
                LogErro();
        }

        public void Trilobit_EnviaDataHora()
        {
            string DataHoraFormatada = String.Format("{0:yyyyMMddHHmmss}", DateTime.Now);
            //string DataHoraFormatada = String.Format("{0:yyyyMMddhhmmss}", new DateTime(2011, 05, 09, 10, 0, 0));

            if (Rep.EnviarConfiguracao(IP, Porta, Senha, REP.eParamSetConfig.AjusteRelogio, DataHoraFormatada))
                this.AddLog("DATA E HORA ATUALIZADA COM SUCESSO");
            else
                LogErro();
        }

        public void Trilobit_EnviarFuncionario(string PIS, string Nome, string Cracha)
        {
            //Cracha = Cracha.PadLeft(CartaoDigitos, '0');

            this.AddLog("ENVIANDO FUNCIONÁRIO PIS: " + PIS + " CRACHÁ: " + Cracha);

            if (Rep.CadastrarEmpregado(IP, Porta, Senha, PIS, Nome, Cracha, true))
                this.AddLog("FUNCIONÁRIO ENVIADO COM SUCESSO");
            else
                LogErro();
        }

        public void Trilobit_ExcluirFuncionario(string PIS)
        {
            this.AddLog("EXCLUINDO FUNCIONÁRIO PIS: " + PIS);

            if (Rep.ExcluirEmpregado(IP, Porta, Senha, PIS))
                this.AddLog("FUNCIONÁRIO EXCLUIDO COM SUCESSO");
            else
                LogErro();
        }

        public void Trilobit_RecebeMarcacoes(string Arquivo, int Grupo, DateTime DataInicial, DateTime DataFinal)
        {
            string sDataInicial =
                DataInicial.Year.ToString().PadLeft(4, '0') +
                DataInicial.Month.ToString().PadLeft(2, '0') +
                DataInicial.Day.ToString().PadLeft(2, '0');

            string sDataFinal =
                DataFinal.Year.ToString().PadLeft(4, '0') +
                DataFinal.Month.ToString().PadLeft(2, '0') +
                DataFinal.Day.ToString().PadLeft(2, '0');

            if (Rep.LerAFD(IP, Porta, Senha, sDataInicial, sDataFinal, Arquivo))
            {
                AddLog("Arquivo gerado em " + Arquivo);
                DB db = new DB();
                db.SalvarConfiguracao(Grupo, "CFG_ULTIMA_IMPORTACAO_ARQUIVO", Arquivo);
                db.SalvarConfiguracao(Grupo, "CFG_ULTIMA_IMPORTACAO", DateTime.Now.ToString());
                ProcessarMarcacoes(db, Grupo, Arquivo);
            }
            else
                LogErro();
        }

        private void Inicializar_TabTemp_DescontoDsr(DB db)
        {
            db.ExecuteCommand(
            "CREATE TABLE #dbtDescDsr ( " +
                "    TDSR_FUNC INT, " +
                "    TDSR_ANTERIOR SMALLDATETIME, " +
                "    TDSR_POSTERIOR SMALLDATETIME " +
                "    ) ON [PRIMARY]"
                );
        }

        private void Finalizar_TabTemp_DescontoDsr(DB db)
        {
            db.ExecuteCommand("EXEC dsr_calcular_desconto_lote");
            db.ExecuteCommand("DROP TABLE #dbtDescDsr");
        }

        private void ProcessarMarcacoes(DB db, int Grupo, string Arquivo)
        {
            List<string> Marcacoes = new List<string>();
            rFiles.ReadFile(Arquivo, Marcacoes);

            ProcessarMarcacoes(db, Grupo, Marcacoes);
        }

        private int GetNMarcacoesValidas(List<string> marcacoes)
        {
            string TipoRegistro;
            int N = 0;

            foreach (string s in marcacoes)
            {
                TipoRegistro = s.Substring(10 - 1, 1);

                if (TipoRegistro == "3")
                    N++;
            }
            return N;
        }
        private void ProcessarMarcacoes(DB db, int Grupo, List<string> marcacoes)
        {
            ProgressImportacao progressimportacao = new ProgressImportacao();

            Inicializar_TabTemp_DescontoDsr(db);

            progressimportacao.Show();
            progressimportacao.progressBar1.Maximum = marcacoes.Count;
            progressimportacao.progressBar1.Value = 0;

            string Pis;
            string Data;
            string Hora;
            string TipoRegistro;

            SqlCommand cmd = new SqlCommand("importar_marcacao", db.Conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter_return = cmd.Parameters.Add("@Return_Value", SqlDbType.Int);
            parameter_return.Direction = ParameterDirection.ReturnValue;

            SqlParameter parameter_grupo = cmd.Parameters.Add("@P_GRUPO", SqlDbType.Int);
            SqlParameter parameter_pis = cmd.Parameters.Add("@P_PIS", SqlDbType.Char);
            SqlParameter parameter_data = cmd.Parameters.Add("@P_DATA", SqlDbType.Char);
            SqlParameter parameter_hora = cmd.Parameters.Add("@P_HORA", SqlDbType.Char);

            SqlParameter parameter_nome = cmd.Parameters.Add("@R_FUNCNOME", SqlDbType.Char, 50);
            parameter_nome.Direction = ParameterDirection.Output;
            SqlParameter parameter_statusdescricao = cmd.Parameters.Add("@R_STATUSDESCRICAO", SqlDbType.Char, 25);
            parameter_statusdescricao.Direction = ParameterDirection.Output;

            parameter_grupo.Value = Grupo;

            AddLog("PROCESSANDO " + Convert.ToString(GetNMarcacoesValidas(marcacoes)) + " MARCAÇÕES");
            try
            {
                foreach (string s in marcacoes)
                {
                    TipoRegistro = s.Substring(10 - 1, 1);

                    if (TipoRegistro == "3")
                    {
                        Pis = s.Substring(23 - 1, 12).Trim();
                        Data = s.Substring(11 - 1, 2) + "/" + s.Substring(13 - 1, 2) + "/" + s.Substring(15 - 1, 4);
                        Hora = s.Substring(19 - 1, 2) + ":" + s.Substring(21 - 1, 2);

                        progressimportacao.lbMensagem.Text = String.Format("{0} {1} {2}", Pis, Data, Hora);

                        parameter_pis.Value = Pis;
                        parameter_data.Value = Data;
                        parameter_hora.Value = Hora;

                        cmd.ExecuteNonQuery();
                        //db.ExecuteCommand(String.Format("EXEC importar_marcacao @P_GRUPO = {0},@P_PIS = {1}, @P_DATA = '{2}', @P_HORA = '{3}'", Grupo, Pis, Data, Hora));

                        //AddLogUnformatted(Pis + " " + Data + " " + Hora);

                        AddLogUnformatted(String.Format("{0} {1} {2} {3} {4}", Pis, Data, Hora, parameter_statusdescricao.Value, parameter_nome.Value));
                    }
                    else
                    {
                        progressimportacao.lbMensagem.Text = String.Empty;
                    }

                    progressimportacao.progressBar1.Value++;
                    Application.DoEvents();
                }
                AddLog("FINALIZADO");
            }
            finally
            {
                progressimportacao.Close();
                Finalizar_TabTemp_DescontoDsr(db);
            }
        }

        //public void LerConfiguracao(bool Biometria)
        //{
        //    AddLog("LENDO CONFIGURAÇÃO");

        //    foreach (REP.eParamGetConfig ParamGet in Enum.GetValues(typeof(REP.eParamGetConfig)))
        //    {
        //        string Valor = "";

        //        if (ParamGet == RepTrilobit.REP.eParamGetConfig.TipoIdentificacao) continue;

        //        if ((!Biometria) &&
        //           (
        //            (ParamGet == RepTrilobit.REP.eParamGetConfig.TipoBiometria) ||
        //            (ParamGet == RepTrilobit.REP.eParamGetConfig.TipoLeitor) ||
        //            (ParamGet == RepTrilobit.REP.eParamGetConfig.QtdeBiometrias) ||
        //            (ParamGet == RepTrilobit.REP.eParamGetConfig.BiometriasLivres) ||
        //            (ParamGet == RepTrilobit.REP.eParamGetConfig.ModeloBiometrico) ||
        //            (ParamGet == RepTrilobit.REP.eParamGetConfig.VersaoBiometrico) ||
        //            (ParamGet == RepTrilobit.REP.eParamGetConfig.NumeroSerieBiometrico)
        //            )) continue;

        //        if (Rep.LerConfiguracao(IP, Porta, Senha, ParamGet, ref Valor))
        //        {
        //            switch (ParamGet)
        //            {
        //                case RepTrilobit.REP.eParamGetConfig.InicioHorarioVerao:
        //                    Valor = GetDateRepToDate(Valor);
        //                    break;
        //                case RepTrilobit.REP.eParamGetConfig.FimHorarioVerao:
        //                    Valor = GetDateRepToDate(Valor);
        //                    break;
        //                case RepTrilobit.REP.eParamGetConfig.HorarioAtual:
        //                    Valor = GetDateRepToDateTime(Valor);
        //                    break;
        //                case RepTrilobit.REP.eParamGetConfig.InicioOperacao:
        //                    Valor = GetDateRepToDateTime(Valor);
        //                    break;
        //                case RepTrilobit.REP.eParamGetConfig.UltimoRegistro:
        //                    Valor = GetDateRepToDateTime(Valor);
        //                    break;
        //                case RepTrilobit.REP.eParamGetConfig.StatusPapel:
        //                    if (Valor == "0") Valor = "SEM PAPEL";
        //                    if (Valor == "1") Valor = "PAPEL OK";
        //                    break;
        //                case RepTrilobit.REP.eParamGetConfig.TipoDocumento:
        //                    if (Valor == "1") Valor = "CNPJ";
        //                    if (Valor == "2") Valor = "CPF";
        //                    break;
        //            }
        //            AddLogUnformatted(ParamGet.ToString().PadRight(25) + " " + Valor);
        //        }
        //        else
        //            AddLogUnformatted(ParamGet.ToString().PadRight(25) + " " + Rep.ErrorException.Message);
        //    }

        //    AddLog("LEITURA DE CONFIGURAÇÃO FINALIZADA");
        //}

        private string GetDateRepToDate(string sData)
        {
            return sData.Substring(6, 2) + "/" + sData.Substring(4, 2) + "/" + sData.Substring(0, 4);
        }

        private string GetDateRepToDateTime(string sData)
        {
            return GetDateRepToDate(sData) + " " + sData.Substring(8, 2) + ":" + sData.Substring(10, 2);
        }

        public void HorarioDeVerao(string IP, int Porta, int Senha, string Valor, int parametro)
        {
            foreach (REP.eParamGetConfig ParamGet in Enum.GetValues(typeof(REP.eParamGetConfig)))
            {
                if (parametro == Convert.ToInt32(ParamGet))
                {
                    switch (ParamGet)
                    {
                        case RepTrilobit.REP.eParamGetConfig.InicioHorarioVerao:
                            Valor = GetDateRepToDate(Valor);
                            Rep.LerConfiguracao(IP, Porta, Senha, ParamGet, ref Valor);
                            break;
                        case RepTrilobit.REP.eParamGetConfig.FimHorarioVerao:
                            Valor = GetDateRepToDate(Valor);
                            Rep.LerConfiguracao(IP, Porta, Senha, ParamGet, ref Valor);
                            break;
                    }
                    AddLogUnformatted(ParamGet.ToString().PadRight(25) + " " + Valor);
                }

            }
        }
    }
}
