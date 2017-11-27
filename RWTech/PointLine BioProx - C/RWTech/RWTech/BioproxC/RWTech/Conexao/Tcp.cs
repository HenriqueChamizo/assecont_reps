using System;
using System.Net.Sockets;
using System.Threading;

namespace BioproxC.RWTech.Conexao
{
    public class Tcp
    {
        private String ip = "localhost";
        private int porta = 1001;
        private Socket socket = null;
        private int qdeUltimoEnvio = 0;

        /**
        * Construtor com parÃ¢metros bÃ¡sicos.
        * 
        * @param ip    IP a ser utilizado na comunicaÃ§Ã£o.
        * @param porta Porta a ser utilizada na comunicaÃ§Ã£o.
        */
        public Tcp(String ip, int porta)
        {
            try
            {
                this.ip = ip;
                this.porta = porta;
                atualizaSocket(ip, porta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /**
         * Construtor sem parÃ¢metros.
         */
        public Tcp()
        {
            try
            {
                atualizaSocket(ip, porta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /**
         * Atualiza o socket de comunicaÃ§Ã£o.
         * 
         * @param ip     Novo IP.
         * @param porta  Nova porta.
         * @throws IOException 
         */
        private void atualizaSocket(String ip, int porta)
        {
            try
            {
                if (socket != null)
                    socket.Close();
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ip, porta);

                /* ForÃ§a a reinicializaÃ§Ã£o de qualquer comunicaÃ§Ã£o jÃ¡ em progresso */
                //atualizaSaida();
                //atualizaEntrada();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /**
         * Faz a preparaÃ§Ã£o necessÃ¡rio para que um novo envio de dados seja 
         * realizado com sucesso, segundo testes prÃ¡ticos.
         * 
         * @throws IOException 
         */
        public void preparaNovoEnvio()
        {
            try
            {
                atualizaSocket(ip, porta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /**
         * Renova o objeto utilizado para a saÃ­da de dados.
         * 
         * @throws IOException 
         */
        //private void atualizaSaida()
        //{
        //    try
        //    {
        //        saida = new DataOutputStream(socket.getOutputStream());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        /**
         * Renova o objeto utilizado para entrada de dados.
         * 
         * @throws IOException 
         */
        //private void atualizaEntrada()
        //{
        //    try
        //    {
        //        entrada = new DataInputStream(socket.getInputStream());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        /**
         * Retorna o IP configurado.
         * 
         * @return IP configurado.
         */
        public String getIp()
        {
            return ip;
        }

        /** 
         * Altera o IP configurado jÃ¡ atualizando o socket para a comunicaÃ§Ã£o.
         * 
         * @param ip  Novo IP a ser configurado para a comunicaÃ§Ã£o.
         * @throws IOException 
         */
        public void setIp(String ip)
        {
            try
            {
                this.ip = ip;
                atualizaSocket(ip, porta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /**
         * Retorna a porta configurada.
         * 
         * @return NÃºmero da porta configurada.
         */
        public int getPorta()
        {
            return porta;
        }

        /** 
         * Altera a porta configurada jÃ¡ atualizando o socket para a comunicaÃ§Ã£o.
         * 
         * @param porta  Nova porta a ser configurada para a comunicaÃ§Ã£o.
         * @throws IOException 
         */
        public void setPorta(int porta)
        {
            try
            {
                this.porta = porta;
                atualizaSocket(ip, porta);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /**
         * Envia determinada quantidade bytes.
         * 
         * @param bytes Bytes a serem enviados.
         * @param ini   Ã¿ndice do primeiro byte a ser enviado.
         * @param qde   Quantidade de bytes a serem enviados.
         */
        public void enviaBytes(byte[] bytes, int ini, int qde)
        {
            try
            {
                if (qde < 1)
                    throw new Exception("Quantidade não permitida");

                if ((ini < 0) || ((ini + qde) > bytes.Length))
                    throw new Exception("Fora das margens: ini(" + ini + "), qde(" + qde + ")");

                // Se algum outro envio jÃ¡ foi realizado ateriormente, prepara para 
                // novo envio
                //        if (saida.size() > 0) {
                //            preparaNovoEnvio();
                //        }

                // Armazena o Ãºltimo envio programado
                qdeUltimoEnvio = qde;

                if (!socket.Connected)
                    atualizaSocket(ip, porta);
                // Programa o envio dos bytes via TCP para o servidor
                socket.Send(bytes, ini + qde, SocketFlags.None);
                //saida.write(bytes, ini, qde);
                //saida.flush();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /**
         * Envia a sequÃªncia de bytes sinalizada por parÃ¢metro.
         * 
         * @param bytes SequÃªncia de bytes a serem enviados.
         */
        public void enviaBytes(byte[] bytes)
        {
            try
            {
                enviaBytes(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /**
         * LÃª uma sequÃªncia de dados para uma localizaÃ§Ã£o especÃ­fica do buffer.
         * 
         * @param bytes Buffer que receberÃ¡ a leitura.
         * @param ini   Primeira posiÃ§Ã£o do buffer a receber os dados lidos.
         * @param qde   Quantidade mÃ¡xima de leitura dos dados.
         * @return      Quantidade de bytes lidos ou -1 para sinalizar que nenhum 
         *              byte foi lido.
         * @throws IOException 
         */
        public int recebeBytes(byte[] bytes, int ini, int qde, int timeout)
        {
            int bytesRecebidos = 0;
            try
            {
                if (qde < 1)
                    throw new Exception("Quantidade não permitida");

                if ((ini < 0) || ((ini + qde) > bytes.Length))
                    throw new Exception("Fora das margens: ini(" + ini + "), qde(" + qde + ")");

                try
                {
                    socket.SendTimeout = timeout;
                    bytesRecebidos = socket.Receive(bytes);
                    //bytesRecebidos = entrada.read(bytes, ini, qde);
                    Thread.Sleep(250);
                }
                catch (Exception ex)
                {
                    bytesRecebidos = -1;
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            // Realiza a leitura e jÃ¡ retorna a quantidade
            return bytesRecebidos;
        }

        /** 
         * Sinaliza se o Ãºltimo envio realizado jÃ¡ foi concluÃ­do, de fato, ou nÃ£o.
         * 
         * @return SinalizaÃ§Ã£o se o Ãºltimo envio jÃ¡ foi concluÃ­do (true) ou nÃ£o 
         *         (false).
         */
        public bool envioFinalizado()
        {
            return ((qdeUltimoEnvio - bytesEnviados()) <= 0) ? true : false;
        }

        /**
         * LÃª uma sequÃªncia de bytes lidos.
         * 
         * @param bytes SequÃªncia dos bytes lidos.
         * @return Quantidade de bytes lidos ou -1 sinalizando que nenhum byte foi 
         *         lido.
         * @throws IOException 
         */
        //    public int recebeBytes(byte[] bytes) throws IOException {
        //        return recebeBytes(bytes, 0, bytes.length);
        //    }

        /**
         * Sinaliza a quantidade de bytes jÃ¡ enviados no Ãºltimo envio de dados.
         * 
         * @return Quantidade em inteiro.
         * 
         * @see enviaBytes
         */
        public int bytesEnviados()
        {
            return socket.SendBufferSize;
        }

        /**
         * Finaliza a conexÃ£o com o REP.
         * 
         * @throws IOException 
         */
        public void finalizaConexao()
        {
            try
            {
                if (socket.Connected)
                {
                    //entrada.close();
                    //saida.close();
                    socket.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /**
         * Finaliza a conexÃ£o quando o Garbage Collection atuar num objeto desta 
         * classe.
         * 
         * @throws Throwable 
         */
        protected void finalize() 
        {
            try
            {
                finalizaConexao();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
