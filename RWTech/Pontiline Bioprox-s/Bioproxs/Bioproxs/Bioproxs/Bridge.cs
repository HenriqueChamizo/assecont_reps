using AssepontoRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Bioproxs
{
    public class Bridge : AssepontoRep.Bridge
    {
        private TModelo modelo = TModelo.POINTLineBioProxS;
        private short baudrate = 9600;
        private byte s_tipo = Convert.ToByte(2);

        public Bridge(TextBox edLog)
            : base(Consts.OFFLINE, edLog)
        {
        }
        #region Override Abstract
        public override bool getAutenticacao() {   return false;   }
        public override bool getBoxFuncoes() { return false; }
        public override bool getCadastroTerminalResponsavel() { return false; }
        public override bool getCadastroTerminalSupervisor() { return false; }
        public override bool getContemChaveAcessoREP() { return false; }
        public override bool getEnviarHorarioVerao() { return false; }
        public override bool getEnviarHorarioVeraoUsb() { return false; }
        public override bool getFuncionariosAlteradosUsb() { return false; }
        public override bool getGerarBackup() { return false; }
        public override bool getGerarBiometria() { return false; }
        public override bool getGerarUsb() { return false; }
        public override bool getGerarUsbEmpregador() { return false; }
        public override int GetHashCode() { return base.GetHashCode();  }
        public override bool getNumeroSerieREP() { return false; }
        public override bool getPin() { return false; }
        public override int getPortaPadrao() {  return 1001;    }
        public override string getRepFabricante() { return "RWTech";    }
        public override string getArquivoUsb() {    return "";  }
        #endregion

        public string Rep(MtdConfigura configura)
        {
            string retorno = Authotelcom.configura(configura.tipo, configura.ip, configura.host, configura.porta, configura.end_dev,
                    configura.datahorai, configura.datahoraf, configura.diassem, configura.diassemf, configura.inf,
                    Convert.ToInt16(s_tipo), 0, 0, 0, 0, 0, 0, baudrate, "0");
            if (retorno == "-1")
                Authotelcom.fecharComunicacao();
            return retorno;
        }

        #region CONEXAO
        public override bool Connect(int Terminal)
        {
            MtdConfigura config = new MtdConfigura();

            string retorno;
            config.tipo =      /*TIPO      */"LR";
            config.ip =        /*IP        */TerminalDados.IP;
            config.porta =     /*PORTA     */TerminalDados.Porta.ToString();
            config.end_dev =   /*END_DEV   */"U,00";
            config.datahorai = /*DATAHORAI */"";//DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            config.inf = "01";
            try
            {
                retorno = Rep(config);
                if (retorno.Split(new string[] { " " }, StringSplitOptions.None)[0] == "0")
                    return true;
                else
                {
                    log.AddLog("Erro: " + retorno);
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.AddLog("Erro: " + ex.Message);
                return false;
            }
        }

        public override bool Disconnect(int Terminal)
        {
            try
            {
                Authotelcom.fecharComunicacao();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        public override bool sendDataHora(int Terminal)
        {
            if (!base.sendDataHora(Terminal)) return false;

            //if (Connect(Terminal))
            //{
            //Connect(Terminal);
            log.AddLog("CONECTADO...");
            MtdConfigura config = new MtdConfigura();
            string ip = TerminalDados.IP;
            string port = TerminalDados.Porta.ToString();
            config.tipo =      /*TIPO      */"AR";
            config.ip =        /*IP        */ip;
            config.porta =     /*PORTA     */port;
            config.end_dev =   /*END_DEV   */"U,00";
            config.datahorai = /*DATAHORAI */DateTime.Now.ToString("dd/MM/yyyy HH:mm:00");
            config.inf = "01";
            //config.tipo =      /*TIPO      */"AR";
            //config.ip =        /*IP        */"192.168.1.199";
            //config.porta =     /*PORTA     */"1001";
            //config.end_dev =   /*END_DEV   */"U,00";
            //config.datahorai = /*DATAHORAI */"01/12/2016 06:00:00";
            //config.inf = "01";
            try
            {

                string retorno = Rep(config);
                if (retorno.Split(new string[] { " " }, StringSplitOptions.None)[0] == "0")
                {
                    log.AddLog("DATA HORA ATUALIZADA COM SUCESSO...", true);
                    return true;
                }
                else
                {
                    log.AddLog("Erro: " + (string.IsNullOrEmpty(retorno) ? "sem retorno" : retorno), true);
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.AddLog("Erro: " + ex.Message, true);
                return false;
            }
            //}
            //else
            //{
            //    log.AddLog("NÃO FOI POSSÍVEL SE CONECTAR AO RELÓGIO...");
            //    return false;
            //}
        }

        //public int GravaEmpregador(Types.Empregador empregador)
        //{
        //    TControle controle = new TControle();
        //    controle.s_tipo = 2; // Comunicação TCP/IP
        //    controle.modelo = (byte)(int)modelo;
        //    controle.endereco = ConversaoDelphi.stringParaBytes(configuracao.getIP(), configuracao.getIP().length() + 1);
        //    controle.porta = new Short(configuracao.getPorta());
        //    controle.atual = 1; // Deve permanecer em 1
        //    controle.total = 1; // Deve permanecer em 1

        //    /* Inicia a comunicação */
        //    controle.start = ConversaoDelphi.booleanParaByte(true);
        //    dll._enviaEmpregadorTCP(dados, controle);

        //    if (controle.erro != (byte)(-1))
        //    {

        //        /* Carrega dos dados de cadastro do empregador */
        //        dados.adcOUSubst = ConversaoDelphi.stringParaBytes("W", dados.adcOUSubst.length); // Valor
        //                                                                                          // diferente
        //                                                                                          // de
        //                                                                                          // A,
        //                                                                                          // S
        //                                                                                          // e
        //                                                                                          // E,
        //                                                                                          // sinalizando
        //                                                                                          // adicionar
        //                                                                                          // ou
        //                                                                                          // substituir
        //        if (empregador.getID() == 1)
        //        {
        //            dados.identificador = ConversaoDelphi.stringParaBytes(Formatacao.SomenteNumeros(empregador.getCNPJ()), dados.identificador.length);
        //            dados.tipoId = ConversaoDelphi.stringParaBytes("1", dados.tipoId.length);
        //        }
        //        else
        //        {
        //            dados.identificador = ConversaoDelphi.stringParaBytes(Formatacao.SomenteNumeros(empregador.getCPF()), dados.identificador.length);
        //            dados.tipoId = ConversaoDelphi.stringParaBytes("2", dados.tipoId.length);
        //        }
        //        String cei = empregador.getCEI();
        //        if (cei == null || (cei != null && cei.isEmpty()))
        //        {
        //            dados.cei = ConversaoDelphi.stringParaBytes("0", dados.cei.length);
        //        }
        //        else
        //        {
        //            dados.cei = ConversaoDelphi.stringParaBytes(cei, dados.cei.length);
        //        }
        //        dados.razaoSocial = ConversaoDelphi.stringParaBytes(empregador.getRazaoSocial(), dados.razaoSocial.length);
        //        dados.localPrestServ = ConversaoDelphi.stringParaBytes(empregador.getLocal(), dados.localPrestServ.length);

        //        /* Efetiva o envio dos dados do empregador */
        //        controle.start = ConversaoDelphi.booleanParaByte(false);
        //        dll._enviaEmpregadorTCP(dados, controle);

        //        if (controle.erro == -1)
        //        {
        //            erro.setErro(ErroComunicacao.COMUNICACAO_NAO_ESTABELECIDA);
        //        }
        //        else
        //        {
        //            erro.setErro(controle.erro);
        //            // dll._fecharComunicacao(); - Gerando exceção externa
        //        }
        //    }
        //    else
        //    {
        //        erro.setErro(ErroComunicacao.COMUNICACAO_NAO_ESTABELECIDA);
        //    }
        //    return erro;
        //}

        public byte[] StringForBytes(string str, int length = 255)
        {
            byte[] strForByte = new byte[length];
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] encBytes = encoding.GetBytes(str);
            for (int i = 0; i < encBytes.Length; i++)
            {
                if (i <= strForByte.Length)
                    strForByte[i] = encBytes[i];
            }
            return strForByte;
        }

        public override bool sendInfoEmpresa(int Terminal, out Types.Empregador EmpregadorDados)
        {
            if (!base.sendInfoEmpresa(Terminal, out EmpregadorDados)) return false;

            //if (Connect(Terminal))
            //{
            TControle controle = new TControle();
            #region
            //string backup = "N";
            //controle.endereco = StringForBytes(TerminalDados.IP);
            //controle.backup = StringForBytes(backup);
            //controle.porta = Convert.ToInt16(TerminalDados.Porta);
            //controle.s_tipo = s_tipo;
            //controle.modelo = (byte)modelo;
            //controle.baudrate = baudrate;
            //controle.erro = Convert.ToByte(0);
            //controle.atual = Convert.ToByte(1);
            //controle.total = Convert.ToByte(1);
            //controle.start = Convert.ToByte(true);
            //controle.hash = StringForBytes("0", 1);
            #endregion
            controle.endereco = StringForBytes("192.168.1.199", "192.168.1.199".Length);
            controle.backup = StringForBytes("N", "N".Length);
            controle.porta = Convert.ToInt16(1001);
            controle.s_tipo = Convert.ToByte(2);
            controle.modelo = Convert.ToByte(6);
            controle.baudrate = 9600;
            controle.erro = Convert.ToByte(0);
            controle.atual = Convert.ToByte(1);
            controle.total = Convert.ToByte(1);
            controle.start = Convert.ToByte(true);
            controle.hash = StringForBytes("0", 1);

            TDados dados = new TDados();

            string tipoId = "1";
            string identificador = EmpregadorDados.Pessoa;
            string cei = string.IsNullOrEmpty(EmpregadorDados.Cei.Replace(" ", "")) ? "0" : EmpregadorDados.Cei;
            string razaoSocial = EmpregadorDados.Nome;
            string localPrestServ = EmpregadorDados.Endereco;
            string adcOUSubst = "A";

            dados.tipoId = StringForBytes(tipoId, 1);
            dados.identificador = StringForBytes(identificador);
            dados.cei = StringForBytes(cei);
            dados.razaoSocial = StringForBytes(razaoSocial, 150);
            dados.localPrestServ = StringForBytes(localPrestServ, 100);
            dados.adcOUSubst = StringForBytes(adcOUSubst, 1);
            dados.pin = StringForBytes("");
            dados.pis = StringForBytes("");
            dados.nome = StringForBytes("");
            dados.id_bio = StringForBytes("");
            dados.numCartao = StringForBytes("");
            dados.senha = StringForBytes("");
            dados.mestre = StringForBytes("");
            dados.verifica = StringForBytes("");
            dados.cpf = StringForBytes("01");
            //dados.chave = StringForBytes("0", 64);

            if (Authotelcom.enviaEmpregadorTCP(ref dados, ref controle))
            {
                if (controle.erro == 0)
                {
                    controle.start = Convert.ToByte(false);
                    if (Authotelcom.enviaEmpregadorTCP(ref dados, ref controle))
                    {
                        if (controle.erro == 0)
                        {
                            log.AddLog("EMPREGADOR ATUALIZADO COM SUCESSO...", true);
                            Authotelcom.fecharComunicacao();
                            return true;
                        }
                        else
                        {
                            log.AddLog(string.Format("ERRO ({0}) NO RELÓGIO.", controle.erro.ToString()), true);
                            Authotelcom.fecharComunicacao();
                            return false;
                        }
                    }
                    else
                    {
                        log.AddLog(string.Format("ERRO ({0}) AO ENVIAR DADOS AO RELÓGIO.", controle.erro.ToString()), true);
                        Authotelcom.fecharComunicacao();
                        return false;
                    }
                }
                else
                {
                    log.AddLog(string.Format("ERRO ({0}) AO ENVIAR DADOS AO RELÓGIO.", controle.erro.ToString()), true);
                    Authotelcom.fecharComunicacao();
                    return false;
                }
            }
            else
            {
                log.AddLog("ERRO NO ENVIO DO COMANDO.", true);
                return false;
            }
            //}
            //else
            //{
            //    return false;
            //}
        }

        public override bool sendFuncionarios(int Terminal, List<Types.Funcionario> listFuncionarios)
        {
            base.sendFuncionarios(Terminal, listFuncionarios);
            return true;
        }

        public override bool sendFuncionario(Types.Funcionario Funcionario)
        {
            TControle controle = new TControle();             TControle controle2 = new TControle();             string ip = TerminalDados.IP;             string backup = "N";             ASCIIEncoding enc = new ASCIIEncoding();             controle.endereco = new byte[ip.Length + 1];             controle.backup = new byte[backup.Length + 1];
            controle.endereco = enc.GetBytes(ip);
            controle.backup = enc.GetBytes(backup);
            controle.porta = Convert.ToInt16(TerminalDados.Porta);
            controle.s_tipo = s_tipo;
            controle.modelo = (byte)modelo;
            controle.baudrate = 9600;
            controle.erro = 0;
            controle.atual = 0;
            controle.total = 2;
            controle.start = 1;             controle2 = controle;

            TDados dados = new TDados();

            string adcOUSubst = "A";             string pin = Funcionario.Crachas[0].ToString();             string pis = Funcionario.Pis;             string nome = Funcionario.Nome.Substring(0, (Funcionario.Nome.Length >= 52 ? 52 : Funcionario.Nome.Length));
            string id_bio = "0";             string numCartao = string.IsNullOrEmpty(Funcionario.Proximidade) ? string.IsNullOrEmpty(Funcionario.Barras) ? "0" : Funcionario.Barras : Funcionario.Proximidade;             string senha = Funcionario.Crachas[0].ToString();             string mestre = "0";
            
            dados.tipoId = new byte[1];
            dados.identificador = new byte[1];
            dados.cei = new byte[1];
            dados.razaoSocial = new byte[1];
            dados.localPrestServ = new byte[1];
            dados.adcOUSubst = new byte[adcOUSubst.Length + 1];             dados.pin = new byte[pin.Length + 1];             dados.pis = new byte[pis.Length + 1];             dados.nome = new byte[nome.Length + 1];             dados.id_bio = new byte[id_bio.Length + 1];             dados.numCartao = new byte[numCartao.Length + 1];             dados.senha = new byte[senha.Length + 1];             dados.mestre = new byte[mestre.Length + 1];             dados.verifica = new byte[1];

            dados.tipoId = enc.GetBytes("");
            dados.identificador = enc.GetBytes("");
            dados.cei = enc.GetBytes("");
            dados.razaoSocial = enc.GetBytes("");
            dados.localPrestServ = enc.GetBytes("");
            dados.adcOUSubst = enc.GetBytes(adcOUSubst);             dados.pin = enc.GetBytes(pin);             dados.pis = enc.GetBytes(pis);             dados.nome = enc.GetBytes(nome);             dados.id_bio = enc.GetBytes(id_bio);             dados.numCartao = enc.GetBytes(numCartao);             dados.senha = enc.GetBytes(senha);             dados.mestre = enc.GetBytes(mestre);             dados.verifica = enc.GetBytes("");

            if (Authotelcom.enviaEmpregadorTCP(dados, controle))
            {
                controle.start = 0;
                Authotelcom.enviaEmpregadorTCP(ref dados, ref controle);

                if (controle.erro == 0)
                {
                    Authotelcom.fecharComunicacao();

                    TDados novo = new TDados();
                    novo.pis = dados.pis;
                    if (Authotelcom.leTrabalhadorTCP(novo, out controle2))
                    {
                        if (controle2.erro == 0 && controle2.total == 0 && controle2.atual == 0)
                            log.AddLog("NÃO EXISTE FUNCIONARIOS NO REP");
                        else
                        {
                            controle.start = 0;
                            Authotelcom.leTrabalhadorTCP(out dados, controle);
                        }
                    }

                    log.AddLog("FUNCIONARIO ENVIADO COM SUCESSO...");
                    Authotelcom.fecharComunicacao();
                    return true;
                }
                else
                {
                    log.AddLog("Erro: " + controle.erro.ToString());
                    Authotelcom.fecharComunicacao();
                    return false;
                }
            }
            else
            {
                log.AddLog("Erro no envio do comando.");
                return false;
            }
        }
    }
}
