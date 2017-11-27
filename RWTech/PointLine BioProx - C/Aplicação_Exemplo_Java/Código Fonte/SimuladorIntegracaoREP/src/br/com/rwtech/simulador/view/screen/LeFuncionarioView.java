package br.com.rwtech.simulador.view.screen;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.List;

import javax.swing.JCheckBox;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JTextField;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.protocolorep.comandos.FlagErroComando;
import br.com.rwtech.simulador.protocolorep.comandos.LeFuncionario;
import br.com.rwtech.simulador.utils.Validacao;
import br.com.rwtech.simulador.view.utils.UIUtils;

public class LeFuncionarioView extends DefaultView {

	private static final long serialVersionUID = 1L;
	
	private JLabel labelPis;
	private JTextField pis;
	private JCheckBox lerTodos;
	private static final int TAMANHO_MAXIMO_PIS = 12;
	
	public LeFuncionarioView(String title) {
		super(title);
	}

	@Override
	protected void configureButtonEnviar() {
		this.add(enviar);
		this.enviar.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				if (Validacao.isDadosConexaoOK(ip.getText(), porta.getText())) {
					if (lerTodos.isSelected() || Validacao.isPisOK(pis.getText(), TAMANHO_MAXIMO_PIS)) {
						LogManager logManager = LogManager.getInstance();
						logManager.ocultarBytesCriptografados = ocultarBytesCriptografados.isSelected();
						logManager.ocultarBytesDescriptografados = ocultarBytesDescriptografados.isSelected();
						
						LeFuncionario leFuncionario = new LeFuncionario(EquipamentoManager.getInstance().getEquipamento().getChaveCriptografica());
						List<String> listPIS = null;
						if (!lerTodos.isSelected()) {
							listPIS = new ArrayList<>();
							listPIS.add(pis.getText());
						}
						FlagErroComando resultado = leFuncionario.execute(ip.getText(), porta.getText(), listPIS);
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
		labelPis = new JLabel("PIS (Somente Números):");
		labelPis.setPreferredSize(labelDimension);
		dataPanel.add(labelPis);
		pis = new JTextField("");
		pis.setPreferredSize(textDimension);
		dataPanel.add(pis);
		
		lerTodos = UIUtils.createJCheckBox("Ler todos", textDimension);
		dataPanel.add(lerTodos);
		
		panelDataDimension.setSize(220, 100);
		this.add(dataPanel);
		dialogDimension.setSize(240, 350);
	}

}
