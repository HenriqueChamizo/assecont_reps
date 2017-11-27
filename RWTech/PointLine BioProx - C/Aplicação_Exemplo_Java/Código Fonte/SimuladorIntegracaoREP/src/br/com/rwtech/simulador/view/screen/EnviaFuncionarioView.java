package br.com.rwtech.simulador.view.screen;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.List;

import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JTextField;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.pojo.Funcionario;
import br.com.rwtech.simulador.protocolorep.comandos.EnviaFuncionario;
import br.com.rwtech.simulador.protocolorep.comandos.FlagErroComando;
import br.com.rwtech.simulador.utils.Validacao;

public class EnviaFuncionarioView extends DefaultView {

	private static final long serialVersionUID = 1L;
	
	private JLabel labelNome;
	private JTextField nome;
	private JLabel labelPis;
	private JTextField pis;
	private JLabel labelCartao;
	private JTextField cartao;
	private JLabel labelCodigo;
	private JTextField codigo;
	private JLabel labelTipoUsuario;
	private JComboBox<String> tiposUsuario;
	private JLabel labelSenha;
	private JTextField senha;
	private JLabel labelVerificar;
	private JComboBox<String> verificar;
	private static final int TAMANHO_MAXIMO_NOME = 52;
	private static final int TAMANHO_MAXIMO_PIS = 12;
	private static final int TAMANHO_MAXIMO_CARTAO = 20;
	private static final int TAMANHO_MAXIMO_CODIGO = 6;
	private static final int TAMANHO_MAXIMO_SENHA = 6;

	public EnviaFuncionarioView(String title) {
		super(title);
	}

	@Override
	protected void configureButtonEnviar() {
		this.add(enviar);
		this.enviar.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				if (Validacao.isDadosConexaoOK(ip.getText(), porta.getText())) {
					Funcionario funcionario = new Funcionario();
					if (Validacao.isStringObrigatoriaOK(nome.getText(), TAMANHO_MAXIMO_NOME)) {
						funcionario.setNome(nome.getText());
						if (Validacao.isPisOK(pis.getText(), TAMANHO_MAXIMO_PIS)) {
							funcionario.setPis(pis.getText());
							if (Validacao.isStringOpcionalOK(cartao.getText(), TAMANHO_MAXIMO_CARTAO)) {
								funcionario.setCartao(cartao.getText());
								boolean codigoOK = false;
								if (senha.getText().isEmpty()) {
									if (Validacao.isStringOpcionalOK(codigo.getText(), TAMANHO_MAXIMO_CODIGO)) {
										codigoOK = true;
									}
								} else {
									if (Validacao.isStringObrigatoriaOK(codigo.getText(), TAMANHO_MAXIMO_CODIGO)) {
										codigoOK = true;
									}
								}
								if (codigoOK) {
									funcionario.setCodigo(codigo.getText());
									if (Validacao.isStringOpcionalOK(senha.getText(), TAMANHO_MAXIMO_SENHA)) {
										funcionario.setSenha(senha.getText());
										
										LogManager logManager = LogManager.getInstance();
										logManager.ocultarBytesCriptografados = ocultarBytesCriptografados.isSelected();
										logManager.ocultarBytesDescriptografados = ocultarBytesDescriptografados.isSelected();
										List<Funcionario> funcionarios = new ArrayList<>();
										funcionarios.add(funcionario);
										EnviaFuncionario enviaFuncionario = new EnviaFuncionario(EquipamentoManager.getInstance().getEquipamento().getChaveCriptografica(), 
																								 EquipamentoManager.getInstance().getEquipamento().getCpf());
										FlagErroComando resultado = enviaFuncionario.execute(ip.getText(), porta.getText(), funcionarios);
										logManager.resultados.add(resultado.getMensagem() + "(C�digo: " + resultado.getErroStrHex() + ")");
									} else {
										JOptionPane.showMessageDialog(null, "Senha inv�lida! Tamanho m�ximo: " + TAMANHO_MAXIMO_SENHA + " d�gitos.");
									}
								} else {
									JOptionPane.showMessageDialog(null, "C�digo inv�lido! Tamanho m�ximo: " + TAMANHO_MAXIMO_CODIGO + " d�gitos.");
								}
							} else {
								JOptionPane.showMessageDialog(null, "Cart�o inv�lido! Tamanho m�ximo: " + TAMANHO_MAXIMO_CARTAO + " caracteres.");
							}
						} else {
							JOptionPane.showMessageDialog(null, "PIS inv�lido!");
						}
					} else {
						JOptionPane.showMessageDialog(null, "Nome inv�lido! Tamanho m�ximo: " + TAMANHO_MAXIMO_NOME + " caracteres.");
					}
				}
			}
		});
	}

	@Override
	protected void adicionarComponentesEspecificos() {
		labelNome = new JLabel("*Nome:");
		labelNome.setPreferredSize(labelDimension);
		dataPanel.add(labelNome);
		nome = new JTextField("");
		nome.setPreferredSize(textDimension);
		dataPanel.add(nome);
		
		labelPis = new JLabel("*PIS (Somente N�meros):");
		labelPis.setPreferredSize(labelDimension);
		dataPanel.add(labelPis);
		pis = new JTextField("");
		pis.setPreferredSize(textDimension);
		dataPanel.add(pis);
		
		labelTipoUsuario = new JLabel("*Tipo de Usu�rio:");
		labelTipoUsuario.setPreferredSize(labelDimension);
		dataPanel.add(labelTipoUsuario);
		tiposUsuario = new JComboBox<String>(new String[] { "0 - Comum", "1 - Mestre" });
		tiposUsuario.setPreferredSize(textDimension);
		dataPanel.add(tiposUsuario);
		
		labelCartao = new JLabel("Cart�o:");
		labelCartao.setPreferredSize(labelDimension);
		dataPanel.add(labelCartao);
		cartao = new JTextField("");
		cartao.setPreferredSize(textDimension);
		dataPanel.add(cartao);
		
		labelCodigo = new JLabel("C�digo:");
		labelCodigo.setPreferredSize(labelDimension);
		dataPanel.add(labelCodigo);
		codigo = new JTextField("");
		codigo.setPreferredSize(textDimension);
		dataPanel.add(codigo);
		
		labelSenha = new JLabel("Senha:");
		labelSenha.setPreferredSize(labelDimension);
		dataPanel.add(labelSenha);
		senha = new JTextField("");
		senha.setPreferredSize(textDimension);
		dataPanel.add(senha);
		
		labelVerificar = new JLabel("Verificar:");
		labelVerificar.setPreferredSize(labelDimension);
		dataPanel.add(labelVerificar);
		verificar = new JComboBox<String>(new String[] { "0 - 1/N", "1 - 1/1" });
		verificar.setPreferredSize(textDimension);
		dataPanel.add(verificar);
		
		panelDataDimension.setSize(220, 330);
		this.add(dataPanel);
		dialogDimension.setSize(240, 580);
	}

}
