package br.com.rwtech.simulador.view.screen;

import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JTextField;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.criptografia.AES;
import br.com.rwtech.simulador.pojo.Equipamento;
import br.com.rwtech.simulador.utils.MaskProcessor;

public class EquipamentoView extends JDialog {

	private static final long serialVersionUID = 1L;

	private JLabel labelIP = new JLabel("*Endereço IP (Exemplo: 10.0.1.10):");
	private JLabel labelPorta = new JLabel("*Porta (Exemplo: 1001):");
	private JLabel labelChaveCriptografica = new JLabel("*Chave Criptográfica:");
	private JLabel labelCPF = new JLabel("CPF (Somente números):");
	private JTextField ip = new JTextField("");
	private JTextField porta = new JTextField("");
	private JTextField chaveCriptografica = new JTextField("");
	private JTextField cpf = new JTextField("");
	private JButton salvar = new JButton("Salvar");
	private Dimension labelDimension = new Dimension(200, 12);
	private Dimension textDimentison = new Dimension(200, 20);

	public EquipamentoView(String title) {
		this.setResizable(false);
		this.setLayout(new FlowLayout());
		this.setPreferredSize(new Dimension(230, 240));
		this.setTitle(title);
		labelIP.setPreferredSize(labelDimension);
		labelPorta.setPreferredSize(labelDimension);
		labelChaveCriptografica.setPreferredSize(labelDimension);
		labelCPF.setPreferredSize(labelDimension);
		
		ip.setPreferredSize(textDimentison);
		porta.setPreferredSize(textDimentison);
		chaveCriptografica.setPreferredSize(textDimentison);
		cpf.setPreferredSize(textDimentison);
		this.add(labelIP);
		this.add(ip);
		
		this.add(labelPorta);
		this.add(porta);
		
		this.add(labelChaveCriptografica);
		this.add(chaveCriptografica);
		
		this.add(labelCPF);
		this.add(cpf);

		this.add(salvar);
		this.configureButtonEnviar();
	}

	public void present() {
		this.pack();
		this.setLocationRelativeTo(null);
		this.setModalityType(ModalityType.APPLICATION_MODAL);
		this.setVisible(true);
	}

	protected void configureButtonEnviar() {
		this.salvar.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				Equipamento equipamento = new Equipamento();
				if (ip.getText() == null || ip.getText().isEmpty()) {
					JOptionPane.showMessageDialog(null, "IP inválido!");
					return;
				} else {
					equipamento.setIp(ip.getText());
				}
				
				if (porta.getText() == null || porta.getText().isEmpty()) {
					JOptionPane.showMessageDialog(null, "Porta inválida!");
					return;
				} else {
					equipamento.setPorta(porta.getText());
				}
				
				if (chaveCriptografica.getText() == null || chaveCriptografica.getText().length() != AES.QTD_CARACTERES_CHAVE) {
					JOptionPane.showMessageDialog(null, "Chave Criptográfica inválida!");
					return;
				} else {
					equipamento.setChaveCriptografica(chaveCriptografica.getText());
				}
				
				if (cpf.getText() == null || cpf.getText().isEmpty() || cpf.getText().length() == MaskProcessor.CPF_LENGTH) {
					equipamento.setCpf(cpf.getText());
				} else {
					JOptionPane.showMessageDialog(null, "CPF inválido!");
					return;
				}
				
				EquipamentoManager.getInstance().setEquipamento(equipamento);
				EquipamentoView.this.setVisible(false);
			}
		});
	}
}
