package br.com.rwtech.simulador.controller;

import br.com.rwtech.simulador.view.screen.Menu;

public class Launcher {

	public static void main(String[] args) {
		try {
			new Menu();
		} catch (Exception e) {
			e.printStackTrace();
		}

	}

}
