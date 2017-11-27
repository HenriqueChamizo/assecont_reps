using System;
using BioproxC.RWTech.Conexao;
using BioproxC.RWTech.Common;
using BioproxC.RWTech.Criptografia;
using BioproxC.RWTech.Utils;

namespace BioproxC.RWTech.ProtocoloREP.Comandos
{
    public abstract class Comando
    {
        public byte[] lerResposta(Tcp tcp, int qtdBytesBuffer, String hash, ref LogManager logManager)
        {
            byte[] retorno = new byte[qtdBytesBuffer];
            try
            {
                int qdeRecebida = tcp.recebeBytes(retorno, 0, qtdBytesBuffer, Protocolo.TIMEOUT);
                if (qdeRecebida == -1)
                    return null;
                else if (qdeRecebida != qtdBytesBuffer)
                    return new byte[0];
                
                if (!logManager.ocultarBytesCriptografados)
                    logManager.bytesRecebidos.Add(LogManager.bytesToStringHex("Pacote criptografado retornado pelo REP", retorno));

                retorno = AES.descriptografar(retorno, hash);
                if (!logManager.ocultarBytesDescriptografados)
                    logManager.bytesRecebidos.Add(LogManager.bytesToStringHex("Pacote descriptografado retornado pelo REP", retorno));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
		    return retorno;
	    }
	
	    public FlagErroComando tratarResposta(int codigoComandoEsperado, byte[] retornoReal, int qdeRecebida, int qdeEsperada)
        {
            FlagErroComando flagErroComando = new FlagErroComando();

            if (qdeEsperada == qdeRecebida)
            {
                if (!validarComando(retornoReal, codigoComandoEsperado))
                {
                    return new FlagErroComando(FlagErroComando.RETORNO_INCONSISTENTE);
                }
                if (!validarBCC(retornoReal))
                {
                    return new FlagErroComando(FlagErroComando.ERRO_BCC);
                }
                flagErroComando.setErro(retornoReal[Protocolo.INDICE_FLAG_RETORNO]);
            }
            else
            {
                if ((-1) == qdeRecebida)
                {
                    flagErroComando.setErro(FlagErroComando.COMUNICACAO_NAO_ESTABELECIDA);
                }
                else
                {
                    flagErroComando.setErro(FlagErroComando.RETORNO_INCONSISTENTE);
                }
            }
            return flagErroComando;
        }

        private static bool validarComando(byte[] pacote, int codigoComandoEsperado)
        {
            if (Conversor.byteToInt(pacote[Protocolo.INDICE_COMANDO_RETORNO]) != codigoComandoEsperado)
            {
                return false;
            }
            return true;
        }

        private static bool validarBCC(byte[] pacote)
        {
            byte bccPacote = 0;
            byte bccRecebido = 0;
            bccRecebido = pacote[Protocolo.QTD_BYTES_CABECALHO_DADOS - 2];
            for (int i = 0; i < (Protocolo.QTD_BYTES_CABECALHO_DADOS - 2); i++)
            {
                bccPacote ^= pacote[i];
            }

            if (bccRecebido == bccPacote)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        //@SuppressWarnings("static-access")

        public static void enviaBuffer(byte[] buffer, bool enviarTudo, Tcp tcp, String hash, ref LogManager logManager)
        {
            try
            {
                if (!logManager.ocultarBytesDescriptografados)
                    logManager.bytesEnviados.Add(LogManager.bytesToStringHex("Pacote descriptografado enviado pro REP", buffer));
                byte[] bufferCriptografado = AES.criptografar(buffer, hash);
                if (!logManager.ocultarBytesCriptografados)
                    logManager.bytesEnviados.Add(LogManager.bytesToStringHex("Pacote criptografado enviado pro REP", bufferCriptografado));
                tcp.enviaBytes(bufferCriptografado);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static byte[] criarPacoteCabecalho(int start, int codigoComando, byte[] parametro, byte[] tamanho, byte[] cpfPis, byte flag, int end)
        {
            try
            {
                byte[] requisicao = { (byte) start // Campo Start 1 byte
		                  , 0x00, 0x00, 0x00, 0x00 // Campo Endereço 4 bytes
						 , (byte) codigoComando }; // Campo Comando 1 byte
                requisicao = ProtocoloUtils.merge(new byte[][] { requisicao, parametro }); // Campo Parâmetro 4 bytes
                requisicao = ProtocoloUtils.merge(new byte[][] { requisicao, tamanho }); // Campo Tamanho 4 bytes
                requisicao = ProtocoloUtils.merge(new byte[][] { requisicao, cpfPis }); // Campo CPF/PIS 6 bytes
                requisicao = ProtocoloUtils.merge(new byte[][] { requisicao, new byte[] { flag } }); // Campo Flag/Erro 1 byte																	
                requisicao = ProtocoloUtils.calcularChecksum(requisicao); // Campo BCC 1 byte
                requisicao = ProtocoloUtils.merge(new byte[][] { requisicao, new byte[] { (byte)end } }); // Campo Flag/Erro 1 byte
                return requisicao;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
