/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package br.com.rwtech.simulador.conexao;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 * Classe responsável por fazer a comunicação TCP do lado do cliente.
 * 
 * @author PD Soluções
 */
public class Tcp {
    private String ip = "localhost";
    private int porta = 1001;
    private Socket socket = null;
    private DataOutputStream saida = null;
    private DataInputStream entrada = null;
    private int qdeUltimoEnvio = 0;

    /**
     * Construtor com parâmetros básicos.
     * 
     * @param ip    IP a ser utilizado na comunicação.
     * @param porta Porta a ser utilizada na comunicação.
     */
    public Tcp(String ip, int porta) throws IOException {
        this.ip = ip;
        this.porta = porta;
        atualizaSocket(ip, porta);
    }
    
    /**
     * Construtor sem parâmetros.
     */
    public Tcp() throws IOException {
       atualizaSocket(ip, porta);
    }
    
    /**
     * Atualiza o socket de comunicação.
     * 
     * @param ip     Novo IP.
     * @param porta  Nova porta.
     * @throws IOException 
     */
    private void atualizaSocket(String ip, int porta) throws IOException {
        if (socket != null) {
            socket.close();
        }
        socket = new Socket(ip, porta);
        
        /* Força a reinicialização de qualquer comunicação já em progresso */
        atualizaSaida();
        atualizaEntrada();
    }
    
    /**
     * Faz a preparação necessário para que um novo envio de dados seja 
     * realizado com sucesso, segundo testes práticos.
     * 
     * @throws IOException 
     */
    public void preparaNovoEnvio() throws IOException {
        atualizaSocket(ip, porta);
    }
    
    /**
     * Renova o objeto utilizado para a saída de dados.
     * 
     * @throws IOException 
     */
    private void atualizaSaida() throws IOException {
        saida = new DataOutputStream(socket.getOutputStream());
    }
    
    /**
     * Renova o objeto utilizado para entrada de dados.
     * 
     * @throws IOException 
     */
    private void atualizaEntrada() throws IOException {
        entrada = new DataInputStream(socket.getInputStream());
    }
    
    /**
     * Retorna o IP configurado.
     * 
     * @return IP configurado.
     */
    public String getIp() {        
        return ip;
    }

    /** 
     * Altera o IP configurado já atualizando o socket para a comunicação.
     * 
     * @param ip  Novo IP a ser configurado para a comunicação.
     * @throws IOException 
     */
    public void setIp(String ip) throws IOException {
        this.ip = ip;
        atualizaSocket(ip, porta);
    }

    /**
     * Retorna a porta configurada.
     * 
     * @return Número da porta configurada.
     */
    public int getPorta() {
        return porta;
    }

    /** 
     * Altera a porta configurada já atualizando o socket para a comunicação.
     * 
     * @param porta  Nova porta a ser configurada para a comunicação.
     * @throws IOException 
     */
    public void setPorta(int porta) throws IOException {
        this.porta = porta;
        atualizaSocket(ip, porta);
    }
    
    /**
     * Envia determinada quantidade bytes.
     * 
     * @param bytes Bytes a serem enviados.
     * @param ini   ÿndice do primeiro byte a ser enviado.
     * @param qde   Quantidade de bytes a serem enviados.
     */
    public void enviaBytes(byte[] bytes, int ini, int qde) throws IOException  {
        if (qde < 1) {
            throw new IllegalArgumentException("Quantidade não permitida");
        }
        if ((ini < 0) || ((ini + qde) > bytes.length)) {
            throw new IndexOutOfBoundsException("Fora das margens: ini(" + ini 
                    + "), qde(" + qde + ")");
        }
        
        // Se algum outro envio já foi realizado ateriormente, prepara para 
        // novo envio
//        if (saida.size() > 0) {
//            preparaNovoEnvio();
//        }
        
        // Armazena o último envio programado
        qdeUltimoEnvio = qde;
        
        // Programa o envio dos bytes via TCP para o servidor
        saida.write(bytes, ini, qde);
        saida.flush();
    }
    
    /**
     * Envia a sequência de bytes sinalizada por parâmetro.
     * 
     * @param bytes Sequência de bytes a serem enviados.
     */
    public void enviaBytes(byte[] bytes) throws IOException {
        enviaBytes(bytes, 0, bytes.length);
    }

    /**
     * Lê uma sequência de dados para uma localização específica do buffer.
     * 
     * @param bytes Buffer que receberá a leitura.
     * @param ini   Primeira posição do buffer a receber os dados lidos.
     * @param qde   Quantidade máxima de leitura dos dados.
     * @return      Quantidade de bytes lidos ou -1 para sinalizar que nenhum 
     *              byte foi lido.
     * @throws IOException 
     */
    public int recebeBytes(byte[] bytes, int ini, int qde, int timeout) throws IOException {
        int bytesRecebidos = 0;
        if (qde < 1) {
            throw new IllegalArgumentException("Quantidade não permitida");
        }
        if ((ini < 0) || ((ini + qde) > bytes.length)) {
            throw new IndexOutOfBoundsException("Fora das margens: ini(" + ini 
                    + "), qde(" + qde + ")");
        }        
        
        try {
            socket.setSoTimeout(timeout);
            bytesRecebidos = entrada.read(bytes, ini, qde);
            try {
                Thread.sleep(250);
            } catch (InterruptedException ex) {
                Logger.getLogger(Tcp.class.getName()).log(Level.SEVERE, null, ex);
            }
        } catch (java.net.SocketTimeoutException e) {
            System.out.println("Timeout ocorrido!");
            bytesRecebidos = -1;
        }
        
        // Realiza a leitura e já retorna a quantidade
        return bytesRecebidos;
    }
    
    /** 
     * Sinaliza se o último envio realizado já foi concluído, de fato, ou não.
     * 
     * @return Sinalização se o último envio já foi concluído (true) ou não 
     *         (false).
     */
    public boolean envioFinalizado() {
       return ((qdeUltimoEnvio - bytesEnviados()) <= 0) ? true : false;
    }

    /**
     * Lê uma sequência de bytes lidos.
     * 
     * @param bytes Sequência dos bytes lidos.
     * @return Quantidade de bytes lidos ou -1 sinalizando que nenhum byte foi 
     *         lido.
     * @throws IOException 
     */
//    public int recebeBytes(byte[] bytes) throws IOException {
//        return recebeBytes(bytes, 0, bytes.length);
//    }
    
    /**
     * Sinaliza a quantidade de bytes já enviados no último envio de dados.
     * 
     * @return Quantidade em inteiro.
     * 
     * @see enviaBytes
     */
    public int bytesEnviados() {
        return saida.size();
    }
    
    /**
     * Finaliza a conexão com o REP.
     * 
     * @throws IOException 
     */
    public void finalizaConexao() throws IOException {
        if (socket.isConnected()) {
            entrada.close();
            saida.close();
            socket.close();
        }
    }
    
    /**
     * Finaliza a conexão quando o Garbage Collection atuar num objeto desta 
     * classe.
     * 
     * @throws Throwable 
     */
    @Override
    protected void finalize() throws Throwable {
        finalizaConexao();
        super.finalize();
    }        
}