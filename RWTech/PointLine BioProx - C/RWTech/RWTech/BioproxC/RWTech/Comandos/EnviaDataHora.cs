using BioproxC.RWTech.Common;
using BioproxC.RWTech.Conexao;
using BioproxC.RWTech.ProtocoloREP;
using BioproxC.RWTech.ProtocoloREP.Comandos;
using BioproxC.RWTech.Utils;
using System;

namespace BioproxC.RWTech.Comandos
{
    public class EnviaDataHora : Comando
    {
        public FlagErroComando execute(String ip, String porta, String cpf, String hash, ref LogManager logManager)
        {
            FlagErroComando flagErroComando = new FlagErroComando();
            Tcp tcp = null;
            try
            {
                tcp = new Tcp(ip, Convert.ToInt32(porta));
            }
            catch {
                flagErroComando.setErro(FlagErroComando.COMUNICACAO_NAO_ESTABELECIDA);
                return flagErroComando;
            }

            byte[] buffer = criarPacoteCabecalho(CodigosComandos.START_PC, CodigosComandos.ENVIAR_DATA_HORA,
                                                 ProtocoloUtils.data(), ProtocoloUtils.horario(), Conversor.cpfToByte(cpf), (byte)0x00, CodigosComandos.END);

            //0A 00 00 00 00 91 00 18 B7 E1 00 36 04 0F 00 02 96 46 19 C7 00 E4 40

            try
            {
                enviaBuffer(buffer, true, tcp, hash, ref logManager);
                try
                {
                    // Lê resposta do REP
                    byte[] respostaREP = lerResposta(tcp, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO, hash, ref logManager);
                    int qtdBytesRecebidos = -1;
                    if (respostaREP != null)
                    {
                        qtdBytesRecebidos = respostaREP.Length;
                    }
                    // Trata a resposta do REP
                    flagErroComando = tratarResposta(CodigosComandos.ENVIAR_DATA_HORA, respostaREP, qtdBytesRecebidos, Protocolo.QTD_BYTES_CABECALHO_CRIPTOGRAFADO);
                }
                catch (Exception e)
                {
                    throw e;
                }
                tcp.finalizaConexao();
            }
            catch (Exception e)
            {
                flagErroComando.setErro(FlagErroComando.OCORREU_EXCECAO);
                throw e;
            }
            return flagErroComando;
        }
    }
}
