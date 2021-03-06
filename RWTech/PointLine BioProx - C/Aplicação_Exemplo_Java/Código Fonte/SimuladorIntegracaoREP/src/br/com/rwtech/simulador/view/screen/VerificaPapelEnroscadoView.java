package br.com.rwtech.simulador.view.screen;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.protocolorep.comandos.FlagErroComando;
import br.com.rwtech.simulador.protocolorep.comandos.VerificaPapelEnroscado;
import br.com.rwtech.simulador.utils.Validacao;

public class VerificaPapelEnroscadoView extends DefaultView {

	private static final long serialVersionUID = 1L;

	public VerificaPapelEnroscadoView(String title) {
		super(title);
	}

	@Override
	protected void configureButtonEnviar() {
		this.add(enviar);
		this.enviar.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				if (Validacao.isDadosConexaoOK(ip.getText(), porta.getText())) {
					LogManager logManager = LogManager.getInstance();
					logManager.ocultarBytesCriptografados = ocultarBytesCriptografados.isSelected();
					logManager.ocultarBytesDescriptografados = ocultarBytesDescriptografados.isSelected();
					VerificaPapelEnroscado verificaPapelEnroscado = new VerificaPapelEnroscado(EquipamentoManager.getInstance().getEquipamento().getChaveCriptografica());
					FlagErroComando resultado = verificaPapelEnroscado.execute(ip.getText(), porta.getText());
					logManager.resultados.add(resultado.getMensagem() + "(C�digo: " + resultado.getErroStrHex() + ")");
				}
			}
		});
	}

	@Override
	protected void adicionarComponentesEspecificos() {}
}
