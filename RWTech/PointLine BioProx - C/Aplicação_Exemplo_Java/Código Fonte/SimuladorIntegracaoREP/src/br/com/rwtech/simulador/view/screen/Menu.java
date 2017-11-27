package br.com.rwtech.simulador.view.screen;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Image;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.BorderFactory;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextPane;
import javax.swing.ScrollPaneConstants;
import javax.swing.text.DefaultCaret;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;

import br.com.rwtech.simulador.common.LogManager;

public class Menu extends JFrame {

	private static final long serialVersionUID = 1L;

	private JPanel panelComandos = new JPanel();
	private JPanel panelLog = new JPanel();
	private static final String TO = "-->";
	private static final String FROM = "<--";

	private JTextPane log = new JTextPane();

	private JButton buttonCadastrarEquipamento = new JButton("Cadastrar Equipamento");
	private JButton buttonAjustarRelogio = new JButton("Ajustar Relógio");
	private JButton buttonEnviarEmpregador = new JButton("Enviar Empregador");
	private JButton buttonLerEmpregador = new JButton("Ler Empregador");
	private JButton buttonEnviarFuncionario = new JButton("Cadastrar Funcionário");
	private JButton buttonLerFuncionario = new JButton("Ler Funcionário");
	private JButton buttonExcluirFuncionario = new JButton("Excluir Funcionário");
	private JButton buttonEnviarDigital = new JButton("Enviar Digital");
	private JButton buttonLerDigital = new JButton("Ler Digital");
	private JButton buttonLerMarcacoes = new JButton("Ler Marcações");
	private JButton buttonEnviarHorarioVerao = new JButton("Enviar Horário Verão");
	private JButton buttonLerHorarioVerao = new JButton("Ler Horário Verão");
	private JButton buttonEnviarQtdPapel = new JButton("Enviar Qtd. de Papel");
	private JButton buttonLerQtdPapel = new JButton("Ler Qtd. de Papel");
	private JButton buttonEnviarQtdPoucoPapel = new JButton("Enviar Qtd. de Pouco Papel");
	private JButton buttonLerQtdPoucoPapel = new JButton("Ler Qtd. de Pouco Papel");
	private JButton buttonVerificarPapelEnroscado = new JButton("Verifica Papel Enroscado");
	private JButton buttonLimparLog = new JButton("Limpar Log");

	private SimpleAttributeSet font = new SimpleAttributeSet();

	public Menu() {
		Image im = new ImageIcon("img/rwtech.png").getImage();
		this.setIconImage(im);
		this.setTitle("Simulador Integração REP");
		this.setLayout(new BorderLayout());
		this.configurarBotoes();
		panelComandos.setLayout(new FlowLayout());
		panelComandos.setPreferredSize(new Dimension(230, 590));
		panelComandos.setBorder(BorderFactory.createTitledBorder(BorderFactory.createLineBorder(Color.BLACK), "Acões"));		
		panelComandos.add(buttonCadastrarEquipamento);
		panelComandos.add(buttonAjustarRelogio);
		panelComandos.add(buttonEnviarEmpregador);
		panelComandos.add(buttonLerEmpregador);
		panelComandos.add(buttonEnviarFuncionario);
		panelComandos.add(buttonLerFuncionario);
		panelComandos.add(buttonExcluirFuncionario);
		panelComandos.add(buttonEnviarDigital);
		panelComandos.add(buttonLerDigital);
		panelComandos.add(buttonLerMarcacoes);
		panelComandos.add(buttonEnviarHorarioVerao);
		panelComandos.add(buttonLerHorarioVerao);
		panelComandos.add(buttonEnviarQtdPapel);
		panelComandos.add(buttonLerQtdPapel);
		panelComandos.add(buttonEnviarQtdPoucoPapel);
		panelComandos.add(buttonLerQtdPoucoPapel);
		panelComandos.add(buttonVerificarPapelEnroscado);
		panelComandos.add(buttonLimparLog);

		StyleConstants.setFontSize(font, 16);
		StyleConstants.setFontFamily(font, "Courier New");
		panelLog.setLayout(new BorderLayout());
		JScrollPane scroll = new JScrollPane(log);
		scroll.setPreferredSize(new Dimension(1040, 650));
		scroll.setVerticalScrollBarPolicy(ScrollPaneConstants.VERTICAL_SCROLLBAR_ALWAYS);
		scroll.setHorizontalScrollBarPolicy(ScrollPaneConstants.HORIZONTAL_SCROLLBAR_ALWAYS);
		DefaultCaret caret = (DefaultCaret) log.getCaret();
		caret.setUpdatePolicy(DefaultCaret.ALWAYS_UPDATE);

		log.setEditable(false);
		panelLog.add(scroll, BorderLayout.CENTER);
		panelLog.setBorder(BorderFactory.createTitledBorder(BorderFactory.createLineBorder(Color.BLACK), "Log"));
		this.add(panelComandos, BorderLayout.WEST);
		this.add(panelLog, BorderLayout.EAST);
		this.setResizable(false);
		present();
		input.start();
	}

	private void configurarBotoes() {
		Dimension dimension = new Dimension(190, 30);
		configurarButtonCadastrarEquipamento(dimension);
		configurarButtonAjustarRelogio(dimension);
		configurarButtonEnviarEmpregador(dimension);
		configurarButtonLerEmpregador(dimension);
		configurarButtonEnviarFuncionario(dimension);
		configurarButtonLerFuncionario(dimension);
		configurarButtonExcluirFuncionario(dimension);
		configurarButtonEnviarDigital(dimension);
		configurarButtonLerDigital(dimension);
		configurarButtonLerMarcacoes(dimension);
		configurarButtonEnviarHorarioVerao(dimension);
		configurarButtonLerHorarioVerao(dimension);
		configurarButtonEnviarQtdPapel(dimension);
		configurarButtonLerQtdPapel(dimension);
		configurarButtonEnviarQtdPoucoPapel(dimension);
		configurarButtonLerQtdPoucoPapel(dimension);
		configurarButtonVerificarPapelEnroscado(dimension);
		configurarButtonLimparLog(dimension);
	}

	private void configurarButtonExcluirFuncionario(Dimension dimension) {
		buttonExcluirFuncionario.setPreferredSize(dimension);
		buttonExcluirFuncionario.addActionListener(new ActionListener() {
			private ExclusaoFuncionarioView view = new ExclusaoFuncionarioView("Excluir Funcionário");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonEnviarDigital(Dimension dimension) {
		buttonEnviarDigital.setPreferredSize(dimension);
		buttonEnviarDigital.addActionListener(new ActionListener() {
			private EnviaDigitalView view = new EnviaDigitalView("Enviar Digital");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonLerDigital(Dimension dimension) {
		buttonLerDigital.setPreferredSize(dimension);
		buttonLerDigital.addActionListener(new ActionListener() {
			private LeDigitalView view = new LeDigitalView("Ler Digital");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}

	private void configurarButtonLerMarcacoes(Dimension dimension) {
		buttonLerMarcacoes.setPreferredSize(dimension);
		buttonLerMarcacoes.addActionListener(new ActionListener() {
			private LeMarcacaoView view = new LeMarcacaoView("Ler Marcações");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonEnviarHorarioVerao(Dimension dimension) {
		buttonEnviarHorarioVerao.setPreferredSize(dimension);
		buttonEnviarHorarioVerao.addActionListener(new ActionListener() {
			private EnviaHorarioVeraoView view = new EnviaHorarioVeraoView("Enviar Horário de Verão");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonLerHorarioVerao(Dimension dimension) {
		buttonLerHorarioVerao.setPreferredSize(dimension);
		buttonLerHorarioVerao.addActionListener(new ActionListener() {
			private LeHorarioVeraoView view = new LeHorarioVeraoView("Ler Horário de Verão");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonEnviarQtdPapel(Dimension dimension) {
		buttonEnviarQtdPapel.setPreferredSize(dimension);
		buttonEnviarQtdPapel.addActionListener(new ActionListener() {
			private EnviaQtdPapelView view = new EnviaQtdPapelView("Enviar Quantidade de Papel");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonLerQtdPapel(Dimension dimension) {
		buttonLerQtdPapel.setPreferredSize(dimension);
		buttonLerQtdPapel.addActionListener(new ActionListener() {
			private LeQtdPapelView view = new LeQtdPapelView("Ler Quantidade de Papel");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonEnviarQtdPoucoPapel(Dimension dimension) {
		buttonEnviarQtdPoucoPapel.setPreferredSize(dimension);
		buttonEnviarQtdPoucoPapel.addActionListener(new ActionListener() {
			private EnviaQtdPoucoPapelView view = new EnviaQtdPoucoPapelView("Enviar Quantidade de Pouco Papel");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonLerQtdPoucoPapel(Dimension dimension) {
		buttonLerQtdPoucoPapel.setPreferredSize(dimension);
		buttonLerQtdPoucoPapel.addActionListener(new ActionListener() {
			private LeQtdPoucoPapelView view = new LeQtdPoucoPapelView("Ler Quantidade de Pouco Papel");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonVerificarPapelEnroscado(Dimension dimension) {
		buttonVerificarPapelEnroscado.setPreferredSize(dimension);
		buttonVerificarPapelEnroscado.addActionListener(new ActionListener() {
			private VerificaPapelEnroscadoView view = new VerificaPapelEnroscadoView("Ler Quantidade de Pouco Papel");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}

	private void configurarButtonCadastrarEquipamento(Dimension dimension) {
		buttonCadastrarEquipamento.setPreferredSize(dimension);
		buttonCadastrarEquipamento.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				new EquipamentoView("Cadastro de Equipamento").present();
			}
		});
	}

	private void configurarButtonAjustarRelogio(Dimension dimension) {
		buttonAjustarRelogio.setPreferredSize(dimension);
		buttonAjustarRelogio.addActionListener(new ActionListener() {
			private AjusteRelogioView view = new AjusteRelogioView("Ajuste Relógio");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonEnviarEmpregador(Dimension dimension) {
		buttonEnviarEmpregador.setPreferredSize(dimension);
		buttonEnviarEmpregador.addActionListener(new ActionListener() {
			private EnviaEmpregadorView view = new EnviaEmpregadorView("Enviar Empregador");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonLerEmpregador(Dimension dimension) {
		buttonLerEmpregador.setPreferredSize(dimension);
		buttonLerEmpregador.addActionListener(new ActionListener() {
			private LeEmpregadorView view = new LeEmpregadorView("Ler Empregador");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonEnviarFuncionario(Dimension dimension) {
		buttonEnviarFuncionario.setPreferredSize(dimension);
		buttonEnviarFuncionario.addActionListener(new ActionListener() {
			private EnviaFuncionarioView view = new EnviaFuncionarioView("Enviar Funcionário");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}
	
	private void configurarButtonLerFuncionario(Dimension dimension) {
		buttonLerFuncionario.setPreferredSize(dimension);
		buttonLerFuncionario.addActionListener(new ActionListener() {
			private LeFuncionarioView view = new LeFuncionarioView("Ler Funcionário");

			@Override
			public void actionPerformed(ActionEvent e) {
				view.present();
			}
		});
	}

	private void configurarButtonLimparLog(Dimension dimension) {
		buttonLimparLog.setPreferredSize(dimension);
		buttonLimparLog.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				log.setText("");
			}
		});
	}

	public void fechar() {
		this.dispose();
	}

	public void present() {
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		this.pack();
		this.setLocationRelativeTo(null);
		this.setVisible(true);
	}
	
	private void appendLine(String message, Color color) {
		StyleConstants.setForeground(font, color);
		appendLine(message, font);
	}

	private void appendLine(String message, SimpleAttributeSet font) {
		try {
			log.getDocument().insertString(log.getDocument().getLength(), message + "\n", font);
		} catch (Exception e) {
		}
	}
	
	private Thread input = new Thread() {
		@Override
		public void run() {
			while (true) {
				StyleConstants.setBold(font, true);
				String enviados = LogManager.getInstance().getNextBytesEnviados();
				if (enviados != null) {
					appendLine(TO + enviados, Color.BLACK);
				}
				String recebidos = LogManager.getInstance().getBytesRecebidos();
				if (recebidos != null) {
					appendLine(FROM + recebidos, Color.BLACK);
				}
				
				String resultado = LogManager.getInstance().getResultado();
				if (resultado != null) {
					Color color = Color.RED;
					if (resultado.toUpperCase().contains("SUCESSO")) {
						color = Color.GREEN;
					}
					appendLine("Resultado: " + resultado, color);
				}
				
				try {
					Thread.sleep(80);
				} catch (Exception e) {
				}
			}
		};

	};
}
