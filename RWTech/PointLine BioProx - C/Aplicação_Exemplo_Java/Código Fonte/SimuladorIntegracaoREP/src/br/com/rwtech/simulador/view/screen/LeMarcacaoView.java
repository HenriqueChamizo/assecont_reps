package br.com.rwtech.simulador.view.screen;

import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JTextField;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.protocolorep.comandos.FlagErroComando;
import br.com.rwtech.simulador.protocolorep.comandos.LeMarcacao;
import br.com.rwtech.simulador.utils.Validacao;

public class LeMarcacaoView extends DefaultView {

	private static final long serialVersionUID = 1L;
	private JLabel labelNsr;
	private JTextField nsr;

	public LeMarcacaoView(String title) {
		super(title);
	}

	@Override
	protected void adicionarComponentesEspecificos() {
		this.setPreferredSize(new Dimension(230, 160));
		labelNsr = new JLabel("*Ler marcações a partir do NSR:");
		nsr = new JTextField("0");
		labelNsr.setPreferredSize(labelDimension);
		nsr.setPreferredSize(textDimension);
		dataPanel.add(labelNsr);
		dataPanel.add(nsr);
		
		panelDataDimension.setSize(220, 75);
		this.add(dataPanel);
		dialogDimension.setSize(240, 330);
	}

	@Override
	protected void configureButtonEnviar() {
		this.add(enviar);
		this.enviar.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				if (Validacao.isDadosConexaoOK(ip.getText(), porta.getText())) {
					int nsrAux = 0;
					try {
						nsrAux = Integer.parseInt(nsr.getText());
						if (nsrAux > -1) {
							LogManager logManager = LogManager.getInstance();
							logManager.ocultarBytesCriptografados = ocultarBytesCriptografados.isSelected();
							logManager.ocultarBytesDescriptografados = ocultarBytesDescriptografados.isSelected();
							LeMarcacao leMarcacao = new LeMarcacao(EquipamentoManager.getInstance().getEquipamento().getChaveCriptografica());
							FlagErroComando resultado = leMarcacao.execute(ip.getText(), porta.getText(), nsrAux);
							logManager.resultados.add(resultado.getMensagem() + "(Código: " + resultado.getErroStrHex() + ")");
						} else {
							JOptionPane.showMessageDialog(null, "NSR inválido!");
						}
					} catch (Exception ex) {
						JOptionPane.showMessageDialog(null, "NSR inválido!");
					}
				} 
			}
		});
	}
}
