package br.com.rwtech.simulador.view.utils;

import java.awt.Dimension;
import java.awt.LayoutManager;

import javax.swing.JCheckBox;
import javax.swing.JFormattedTextField;
import javax.swing.JPanel;
import javax.swing.JSpinner;
import javax.swing.SpinnerModel;
import javax.swing.SpinnerNumberModel;
import javax.swing.border.Border;

import br.com.rwtech.simulador.utils.MaskProcessor;

public class UIUtils {
	
	public static JFormattedTextField createJFormattedTextField(Dimension dimension, String mascara, String preenchimento) {
		JFormattedTextField formattedTextField = new JFormattedTextField(MaskProcessor.getMask(mascara, preenchimento));
		formattedTextField.setPreferredSize(dimension);
		return formattedTextField;
	}
	
	public static JSpinner createJSpinner(int valorInicial, int valorMinimo, int valorMaximo, int step) {
		SpinnerModel spinnerModel = new SpinnerNumberModel(valorInicial, valorMinimo, valorMaximo, step);
		JSpinner spinner = new JSpinner(spinnerModel);
		return spinner;
	}

	public static JCheckBox createJCheckBox(String value, Dimension dimension) {
		JCheckBox checbox = new JCheckBox(value);
		checbox.setPreferredSize(dimension);
		return checbox;
	}

	public static JPanel createJPanel(LayoutManager layout, Border border, Dimension dimension) {
		JPanel panel = new JPanel();
		if (dimension != null)
			panel.setPreferredSize(dimension);
		if (layout != null)
			panel.setLayout(layout);
		if (border != null)
			panel.setBorder(border);
		return panel;
	}

}
