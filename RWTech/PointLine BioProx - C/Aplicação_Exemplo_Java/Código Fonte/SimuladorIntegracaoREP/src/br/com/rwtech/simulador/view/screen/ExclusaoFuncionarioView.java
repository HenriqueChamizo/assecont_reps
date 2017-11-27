package br.com.rwtech.simulador.view.screen;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JTextField;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.protocolorep.comandos.ExcluiFuncionario;
import br.com.rwtech.simulador.protocolorep.comandos.FlagErroComando;
import br.com.rwtech.simulador.utils.Validacao;

public class ExclusaoFuncionarioView extends DefaultView {

	private static final long serialVersionUID = 1L;
	
	private JLabel labelPis;
	private JTextField pis;
	private static final int TAMANHO_MAXIMO_PIS = 12;
	
	public ExclusaoFuncionarioView(String title) {
		super(title);
	}

	@Override
	protected void configureButtonEnviar() {
		this.add(enviar);
		this.enviar.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				if (Validacao.isDadosConexaoOK(ip.getText(), porta.getText())) {
					if (Validacao.isPisOK(pis.getText(), TAMANHO_MAXIMO_PIS)) {
						LogManager logManager = LogManager.getInstance();
						logManager.ocultarBytesCriptografados = ocultarBytesCriptografados.isSelected();
						logManager.ocultarBytesDescriptografados = ocultarBytesDescriptografados.isSelected();
						
						ExcluiFuncionario excluiFuncionario = new ExcluiFuncionario(null, EquipamentoManager.getInstance().getEquipamento().getChaveCriptografica());
						FlagErroComando resultado = excluiFuncionario.execute(ip.getText(), porta.getText(), 
																			  EquipamentoManager.getInstance().getEquipamento().getCpf(), 
																			  pis.getText());
						logManager.resultados.add(resultado.getMensagem() + "(Código: " + resultado.getErroStrHex() + ")");
							
					} else {
						JOptionPane.showMessageDialog(null, "PIS inválido!");
					}
				}
			}
		});
	}

	@Override
	protected void adicionarComponentesEspecificos() {
		labelPis = new JLabel("*PIS (Somente Números):");
		labelPis.setPreferredSize(labelDimension);
		dataPanel.add(labelPis);
		pis = new JTextField("");
		pis.setPreferredSize(textDimension);
		dataPanel.add(pis);
		
		panelDataDimension.setSize(220, 75);
		this.add(dataPanel);
		dialogDimension.setSize(240, 330);
	}

}
