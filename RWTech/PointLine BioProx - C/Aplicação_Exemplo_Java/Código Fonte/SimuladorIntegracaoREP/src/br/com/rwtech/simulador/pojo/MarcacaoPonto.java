package br.com.rwtech.simulador.pojo;

public class MarcacaoPonto {

    private String PIS;
    private String data;
    private String hora;
    private int numEvento;
    private int nsr;

    public String getPIS() {
        return PIS;
    }

    public void setPIS(String PIS) {
        this.PIS = PIS;
    }

    public String getData() {
        return data;
    }

    public void setData(String data) {
        this.data = data;
    }

    public String getHora() {
        return hora;
    }

    public void setHora(String hora) {
        this.hora = hora;
    }

    public int getNumEvento() {
        return numEvento;
    }

    public void setNumEvento(int numEvento) {
        this.numEvento = numEvento;
    }

    public int getNsr() {
		return nsr;
	}
    
	public void setNsr(int nsr) {
		this.nsr = nsr;
	}

}
