package br.com.rwtech.simulador.view.screen;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.Date;

import javax.swing.JFormattedTextField;
import javax.swing.JLabel;
import javax.swing.JOptionPane;

import br.com.rwtech.simulador.common.EquipamentoManager;
import br.com.rwtech.simulador.common.LogManager;
import br.com.rwtech.simulador.protocolorep.comandos.EnviaHorarioVerao;
import br.com.rwtech.simulador.protocolorep.comandos.FlagErroComando;
import br.com.rwtech.simulador.utils.Conversor;
import br.com.rwtech.simulador.utils.Validacao;
import br.com.rwtech.simulador.view.utils.UIUtils;

public class EnviaHorarioVeraoView extends DefaultView {

	private static final long serialVersionUID = 1L;
	
	private JLabel labelDataInicio;
	private JFormattedTextField dataInicio;
	private JLabel labelDataFim;
	private JFormattedTextField dataFim;
	
	public EnviaHorarioVeraoView(String title) {
		super(title);
	}

	@Override
	protected void configureButtonEnviar() {
		this.add(enviar);
		this.enviar.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				if (Validacao.isDadosConexaoOK(ip.getText(), porta.getText())) {
					Date dataInicioHV = Conversor.strToDate(dataInicio.getText());
					Date dataFimHV = Conversor.strToDate(dataFim.getText());
					if (dataInicioHV != null && dataFimHV != null && Validacao.isPeriodoDataOK(dataInicioHV, dataFimHV)) {
						LogManager logManager = LogManager.getInstance();
						logManager.ocultarBytesCriptografados = ocultarBytesCriptografados.isSelected();
						logManager.ocultarBytesDescriptografados = ocultarBytesDescriptografados.isSelected();
						
						EnviaHorarioVerao enviaHorarioVerao = new EnviaHorarioVerao(ip.getText(), porta.getText(), 
																					EquipamentoManager.getInstance().getEquipamento().getCpf(),
																					EquipamentoManager.getInstance().getEquipamento().getChaveCriptografica());	
						FlagErroComando resultado = enviaHorarioVerao.execute(dataInicio.getText(), dataFim.getText());
						logManager.resultados.add(resultado.getMensagem() + "(Código: " + resultado.getErroStrHex() + ")");
							
					} else {
						JOptionPane.showMessageDialog(null, "Data inválida!");
					}
				}
			}
		});
	}

	@Override
	protected void adicionarComponentesEspecificos() {
		labelDataInicio = new JLabel("Data Inicial:");
		labelDataInicio.setPreferredSize(labelDimension);
		dataPanel.add(labelDataInicio);
		dataInicio = UIUtils.createJFormattedTextField(textDimension, "##/##/####", "_");
		dataPanel.add(dataInicio);
		
		labelDataFim = new JLabel("Data Final:");
		labelDataFim.setPreferredSize(labelDimension);
		dataPanel.add(labelDataFim);
		dataFim = UIUtils.createJFormattedTextField(textDimension, "##/##/####", "_");
		dataPanel.add(dataFim);
			
		panelDataDimension.setSize(220, 120);
		this.add(dataPanel);
		dialogDimension.setSize(240, 370);
	}

}
