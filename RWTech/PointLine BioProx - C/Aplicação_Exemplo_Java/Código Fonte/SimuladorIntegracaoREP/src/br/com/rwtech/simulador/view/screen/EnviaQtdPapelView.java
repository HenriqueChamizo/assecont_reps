package br.com.rwtech.simulador.view.screen;

import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JSpinner;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.protocolorep.comandos.EnviaQtdPapel;
import br.com.rwtech.simulador.protocolorep.comandos.FlagErroComando;
import br.com.rwtech.simulador.utils.Validacao;
import br.com.rwtech.simulador.view.utils.UIUtils;

public class EnviaQtdPapelView extends DefaultView {

	private static final long serialVersionUID = 1L;

	private JLabel labelQtdPapel;
	private JSpinner qtdPapel;
	private final static int QTD_MAXIMA_PAPEL = 360; // em metros

	public EnviaQtdPapelView(String title) {
		super(title);
	}

	@Override
	protected void adicionarComponentesEspecificos() {
		this.setPreferredSize(new Dimension(230, 160));
		labelQtdPapel = new JLabel("*Tamanho da bobina (em metros):");
		dataPanel.add(labelQtdPapel);
		
		qtdPapel = UIUtils.createJSpinner(1, 1, 360, 1);
		dataPanel.add(qtdPapel);
		
		
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
					try {
						int qtdPapelAEnviar = (int) qtdPapel.getValue();
						if (qtdPapelAEnviar > 0 && qtdPapelAEnviar <= QTD_MAXIMA_PAPEL) {
							LogManager logManager = LogManager.getInstance();
							logManager.ocultarBytesCriptografados = ocultarBytesCriptografados.isSelected();
							logManager.ocultarBytesDescriptografados = ocultarBytesDescriptografados.isSelected();
							EnviaQtdPapel enviaQtdPapel = new EnviaQtdPapel();
							FlagErroComando resultado = enviaQtdPapel.execute(ip.getText(), porta.getText(), 
																			  EquipamentoManager.getInstance().getEquipamento().getCpf(), 
																			  EquipamentoManager.getInstance().getEquipamento().getChaveCriptografica(), 
																			  qtdPapelAEnviar*1000); // em milímetros
							logManager.resultados.add(resultado.getMensagem() + "(Código: " + resultado.getErroStrHex() + ")");
						} else {
							JOptionPane.showMessageDialog(null, "Quantidade inválida!");
						}
					} catch (Exception ex) {
						JOptionPane.showMessageDialog(null, "Quantidade inválida!");
					}
				} 
			}
		});
	}
}
