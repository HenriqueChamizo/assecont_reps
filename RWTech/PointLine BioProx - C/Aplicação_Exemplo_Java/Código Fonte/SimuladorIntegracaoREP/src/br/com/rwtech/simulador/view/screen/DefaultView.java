package br.com.rwtech.simulador.view.screen;

import java.awt.Dimension;
import java.awt.FlowLayout;

import javax.swing.BorderFactory;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.view.utils.UIUtils;

public abstract class DefaultView extends JDialog {

	private static final long serialVersionUID = 1L;

	private static final int PANEL_CONNECTION_WIDTH = 220;
	private static final int PANEL_CONNECTION_HEIGHT = 170;
	private static final int PANEL_DATA_WIDTH = 220;
	protected static int PANEL_DATA_HEIGHT = 210;
	
	protected Dimension labelDimension = new Dimension(200, 12);
	protected Dimension textDimension = new Dimension(200, 20);
	protected Dimension dialogDimension = new Dimension(240, 270);
	protected Dimension panelDataDimension = new Dimension(PANEL_DATA_WIDTH, PANEL_DATA_HEIGHT);
	
	private JPanel connectionPanel = UIUtils.createJPanel(null, BorderFactory.createTitledBorder("Dados para conexão"), new Dimension(PANEL_CONNECTION_WIDTH, PANEL_CONNECTION_HEIGHT));
	public JPanel dataPanel = UIUtils.createJPanel(null, BorderFactory.createTitledBorder("Dados para envio"), panelDataDimension);
	
	private JLabel labelIP = new JLabel("*IP:");
	private JLabel labelPorta = new JLabel("*Porta:");
	protected JTextField ip = new JTextField("");
	protected JTextField porta = new JTextField("");
	protected JCheckBox ocultarBytesCriptografados;
	protected JCheckBox ocultarBytesDescriptografados;
	protected JButton enviar = new JButton("Enviar");
	
	public DefaultView(String title) {
		this.setResizable(false);
		this.setLayout(new FlowLayout());
		this.setTitle(title);
		this.configurarConnectionPanel();
		this.add(connectionPanel);
		this.adicionarComponentesEspecificos();
		this.setPreferredSize(dialogDimension);
		dataPanel.setPreferredSize(panelDataDimension);
		this.configureButtonEnviar();
	}
	
	private void configurarConnectionPanel() {
		// IP
		labelIP.setPreferredSize(labelDimension);
		connectionPanel.add(labelIP);
		ip.setPreferredSize(textDimension);
		connectionPanel.add(ip);
		// Porta
		labelPorta.setPreferredSize(labelDimension);
		connectionPanel.add(labelPorta);
		porta.setPreferredSize(textDimension);
		connectionPanel.add(porta);
		
		ocultarBytesCriptografados = UIUtils.createJCheckBox("Ocultar bytes criptografados", textDimension);
		connectionPanel.add(ocultarBytesCriptografados);
		ocultarBytesDescriptografados = UIUtils.createJCheckBox("Ocultar bytes descriptografados", textDimension);
		connectionPanel.add(ocultarBytesDescriptografados);
	}

	protected void present() {
		this.pack();
		this.setLocationRelativeTo(null);
		this.setModalityType(ModalityType.APPLICATION_MODAL);
		ip.setText(EquipamentoManager.getInstance().getEquipamento().getIp());
		porta.setText(EquipamentoManager.getInstance().getEquipamento().getPorta());
		this.setVisible(true);
	}
		
	protected abstract void configureButtonEnviar();
	
	protected abstract void adicionarComponentesEspecificos();
}