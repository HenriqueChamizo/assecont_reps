package br.com.rwtech.simulador.view.screen;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JTextField;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.pojo.Empregador;
import br.com.rwtech.simulador.protocolorep.comandos.EnviaEmpregador;
import br.com.rwtech.simulador.protocolorep.comandos.FlagErroComando;
import br.com.rwtech.simulador.utils.Validacao;

public class EnviaEmpregadorView extends DefaultView {

	private static final long serialVersionUID = 1L;
	
	private JLabel labelRazaoSocial;
	private JTextField razaoSocial;
	private JLabel labelTipoIdentificador;
	private JComboBox<String> tiposIdentificador;
	private JLabel labelCnpjCpf;
	private JTextField cnpjCpf;
	private JLabel labelLocal;
	private JTextField local;
	private static final int TAMANHO_MAXIMO_RAZAO_SOCIAL = 150;
	private static final int TAMANHO_MAXIMO_LOCAL = 100;

	public EnviaEmpregadorView(String title) {
		super(title);
	}

	@Override
	protected void configureButtonEnviar() {
		this.add(enviar);
		this.enviar.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				if (Validacao.isDadosConexaoOK(ip.getText(), porta.getText())) {
					boolean dadosOK = true;
					Empregador empregador = new Empregador();
					int tipoIdenticador = tiposIdentificador.getSelectedIndex()+1;
					empregador.setTipoIdentificador(tipoIdenticador);
					empregador.setCEI("");
					if (tipoIdenticador == 1) {
						if (Validacao.isCnpjOK(cnpjCpf.getText())) {
							empregador.setCNPJ(cnpjCpf.getText());
						} else {
							JOptionPane.showMessageDialog(null, "CNPJ inválido!");
							dadosOK = false;
						}
					} else {
						if (Validacao.isCpfOK(cnpjCpf.getText())) {
							empregador.setCPF(cnpjCpf.getText());
						} else {
							JOptionPane.showMessageDialog(null, "CPF inválido!");
							dadosOK = false;
						}
					}
					if (dadosOK) {
						if (Validacao.isStringObrigatoriaOK(razaoSocial.getText(), TAMANHO_MAXIMO_RAZAO_SOCIAL)) {
							empregador.setRazaoSocial(razaoSocial.getText());
							if (Validacao.isStringObrigatoriaOK(local.getText(), TAMANHO_MAXIMO_LOCAL)) {
								empregador.setLocal(local.getText());
								LogManager logManager = LogManager.getInstance();
								logManager.ocultarBytesCriptografados = ocultarBytesCriptografados.isSelected();
								logManager.ocultarBytesDescriptografados = ocultarBytesDescriptografados.isSelected();
								
								EnviaEmpregador enviaEmpregador = new EnviaEmpregador();
								FlagErroComando resultado = enviaEmpregador.execute(ip.getText(), porta.getText(), 
																				    EquipamentoManager.getInstance().getEquipamento().getCpf(), 
																				    EquipamentoManager.getInstance().getEquipamento().getChaveCriptografica(), 
																				    empregador);
								logManager.resultados.add(resultado.getMensagem() + "(Código: " + resultado.getErroStrHex() + ")");
							} else {
								JOptionPane.showMessageDialog(null, "Local inválido! Tamanho máximo: " + TAMANHO_MAXIMO_LOCAL + " caracteres.");
							}
						} else {
							JOptionPane.showMessageDialog(null, "Razão Social inválida! Tamanho máximo: " + TAMANHO_MAXIMO_RAZAO_SOCIAL + " caracteres.");
						}
					}
				}
			}
		});
	}

	@Override
	protected void adicionarComponentesEspecificos() {
		labelRazaoSocial = new JLabel("*Razão Social:");
		labelRazaoSocial.setPreferredSize(labelDimension);
		dataPanel.add(labelRazaoSocial);
		razaoSocial = new JTextField("");
		razaoSocial.setPreferredSize(textDimension);
		dataPanel.add(razaoSocial);
		
		labelTipoIdentificador = new JLabel("*Tipo de Identificador:");
		labelTipoIdentificador.setPreferredSize(labelDimension);
		dataPanel.add(labelTipoIdentificador);
		tiposIdentificador = new JComboBox<String>(new String[] { "1 - CNPJ", "2 - CPF" });
		tiposIdentificador.setPreferredSize(textDimension);
		dataPanel.add(tiposIdentificador);
		
		labelCnpjCpf = new JLabel("*CNPJ/CPF:");
		labelCnpjCpf.setPreferredSize(labelDimension);
		dataPanel.add(labelCnpjCpf);
		cnpjCpf = new JTextField("");
		cnpjCpf.setPreferredSize(textDimension);
		dataPanel.add(cnpjCpf);
		
		labelLocal = new JLabel("*Local:");
		labelLocal.setPreferredSize(labelDimension);
		dataPanel.add(labelLocal);
		local = new JTextField("");
		local.setPreferredSize(textDimension);
		dataPanel.add(local);
		
		this.add(dataPanel);
		dialogDimension.setSize(240, 480);
	}

}
