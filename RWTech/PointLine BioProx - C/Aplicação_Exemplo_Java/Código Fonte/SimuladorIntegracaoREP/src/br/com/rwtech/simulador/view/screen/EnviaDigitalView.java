package br.com.rwtech.simulador.view.screen;

import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.List;

import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JSpinner;
import javax.swing.JTextArea;
import javax.swing.JTextField;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.pojo.Digital;
import br.com.rwtech.simulador.protocolorep.comandos.EnviaDigital;
import br.com.rwtech.simulador.protocolorep.comandos.FlagErroComando;
import br.com.rwtech.simulador.utils.Validacao;
import br.com.rwtech.simulador.view.utils.UIUtils;

public class EnviaDigitalView extends DefaultView {

	private static final long serialVersionUID = 1L;
	
	private JLabel labelTemplate1;
	private JTextArea template1;
	private JLabel labelTemplate2;
	private JTextArea template2;
	private JLabel labelPis;
	private JTextField pis;
	private JLabel labelQtdDigitais;
	private JSpinner qtdDigitais; // Utilizada para possibilitar a simulação do envio com mais de uma digital, ainda que seja com as mesmas templates.
	
	private static final int TAMANHO_MAXIMO_PIS = 12;
	private static final int TAMANHO_TEMPLATE1 = 806;
	private static final int TAMANHO_TEMPLATE2 = 800;
	
	public EnviaDigitalView(String title) {
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
						if (Validacao.isStringObrigatoriaOK(template1.getText(), TAMANHO_TEMPLATE1)) {
							if (Validacao.isStringObrigatoriaOK(template1.getText(), TAMANHO_TEMPLATE1)) {
								LogManager logManager = LogManager.getInstance();
								logManager.ocultarBytesCriptografados = ocultarBytesCriptografados.isSelected();
								logManager.ocultarBytesDescriptografados = ocultarBytesDescriptografados.isSelected();
								
								EnviaDigital enviaDigital = new EnviaDigital(EquipamentoManager.getInstance().getEquipamento().getChaveCriptografica());
								List<Digital> digitais = new ArrayList<>();
								Digital digital = new Digital(null, 1, template1.getText(), template2.getText());
								// Possibilidade de simular o envio com mais de uma digital, ainda que seja com as mesmas templates 
								int qtdDigitaisAEnviar = (int) qtdDigitais.getValue();
								if (qtdDigitaisAEnviar > 0 && qtdDigitaisAEnviar < 11) {
									do {
										digitais.add(digital);
									} while (digitais.size() < qtdDigitaisAEnviar);
									FlagErroComando resultado = enviaDigital.execute(ip.getText(), porta.getText(), digitais, pis.getText());
									String mensagem = resultado.getMensagem();
									String erroHex = resultado.getErroStrHex();
									// Isto foi necessário devido ao REP não responder quando tenta-se enviar um PIS não cadastrado.
									if (resultado.getErro() == FlagErroComando.COMUNICACAO_NAO_ESTABELECIDA) {
										mensagem += " Ou " + resultado.getMensagem(FlagErroComando.PIS_INEXISTENTE);
										erroHex += " ou " + resultado.getErroStrHex(FlagErroComando.PIS_INEXISTENTE);
									}
									logManager.resultados.add(mensagem + "(Código: " + erroHex + ")");
								} else {
									JOptionPane.showMessageDialog(null, "Quantidade inválida! Mínimo de 1 e Máximo de 10 vezes!");
								}
							} else {
								JOptionPane.showMessageDialog(null, "2ª Template inválida! Tamanho máximo de " + TAMANHO_TEMPLATE2 + " caracteres.");
							}
						} else {
							JOptionPane.showMessageDialog(null, "1ª Template inválida! Tamanho máximo de " + TAMANHO_TEMPLATE1 + " caracteres.");
						}
					} else {
						JOptionPane.showMessageDialog(null, "PIS inválido!");
					}
				}
			}
		});
	}

	@Override
	protected void adicionarComponentesEspecificos() {
		Dimension textAreaDimension = new Dimension(800, 120);
		
		labelTemplate1 = new JLabel("*1ª Template (806 caracteres):");
		labelTemplate1.setPreferredSize(labelDimension);
		dataPanel.add(labelTemplate1);
		template1 = new JTextArea();
		template1.setRows(5);
		template1.setLineWrap(true);
		template1.setPreferredSize(textAreaDimension);
		dataPanel.add(template1);
		
		labelTemplate2 = new JLabel("*2ª Template (800 caracteres):");
		labelTemplate2.setPreferredSize(labelDimension);
		dataPanel.add(labelTemplate2);
		template2 = new JTextArea();
		template2.setRows(5);
		template2.setLineWrap(true);
		template2.setPreferredSize(textAreaDimension);
		dataPanel.add(template2);
		
		labelPis = new JLabel("*PIS (Somente Números):");
		labelPis.setPreferredSize(new Dimension(150, 20));
		dataPanel.add(labelPis);
		pis = new JTextField("");
		pis.setPreferredSize(new Dimension(100, 20));
		dataPanel.add(pis);
		
		labelQtdDigitais = new JLabel("Quantidade de vezes que esta digital deve ser enviada:");
		labelQtdDigitais.setPreferredSize(new Dimension(315, 20));
		labelQtdDigitais.setToolTipText("Utilizada para simular o envio com mais de uma digital, ainda que seja com as mesmas templates.");
		dataPanel.add(labelQtdDigitais);
		
		qtdDigitais = UIUtils.createJSpinner(1, 1, 10, 1);
		dataPanel.add(qtdDigitais);
		
		panelDataDimension.setSize(870, 340);
		this.add(dataPanel);
		dialogDimension.setSize(890, 590);
	}

}
