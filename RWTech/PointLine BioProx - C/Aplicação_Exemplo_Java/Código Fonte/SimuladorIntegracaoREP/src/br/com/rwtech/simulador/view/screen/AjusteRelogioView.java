package br.com.rwtech.simulador.view.screen;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.protocolorep.comandos.EnviaDataHora;
import br.com.rwtech.simulador.protocolorep.comandos.FlagErroComando;
import br.com.rwtech.simulador.utils.Validacao;

public class AjusteRelogioView extends DefaultView {

	private static final long serialVersionUID = 1L;

	public AjusteRelogioView(String title) {
		super(title);
	}

	@Override
	protected void configureButtonEnviar() {
            this.enviar.addActionListener(new ActionListener() {
                    @Override
                    public void actionPerformed(ActionEvent e) {
                            if (Validacao.isDadosConexaoOK(ip.getText(), porta.getText())) {
                                    LogManager logManager = LogManager.getInstance();
                                    logManager.ocultarBytesCriptografados = ocultarBytesCriptografados.isSelected();
                                    logManager.ocultarBytesDescriptografados = ocultarBytesDescriptografados.isSelected();
                                    EnviaDataHora enviaDataHora = new EnviaDataHora();
                                    FlagErroComando resultado = enviaDataHora.execute(ip.getText(), porta.getText(), 
                                                                                                                                      EquipamentoManager.getInstance().getEquipamento().getCpf(), 
                                                                                                                                      EquipamentoManager.getInstance().getEquipamento().getChaveCriptografica());
                                    logManager.resultados.add(resultado.getMensagem() + "(Código: " + resultado.getErroStrHex() + ")");
                            }
                    }
            });
	}

	@Override
	protected void adicionarComponentesEspecificos() {}
}
