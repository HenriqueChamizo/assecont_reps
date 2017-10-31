object Form1: TForm1
  Left = 0
  Top = 0
  Caption = 'Form1'
  ClientHeight = 367
  ClientWidth = 761
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  Menu = MainMenu1
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object MainMenu1: TMainMenu
    object Empregador1: TMenuItem
      Caption = 'Empregador'
      object Cadastrarnovoterminal1: TMenuItem
        Caption = 'Cadastra Novo Terminal...'
      end
      object Enviar1: TMenuItem
        Caption = 'Enviar'
      end
    end
    object Manuteno1: TMenuItem
      Caption = 'Manuten'#231#227'o'
      object EnviarDataeHora1: TMenuItem
        Caption = 'Enviar Data e Hora'
      end
      object ImportaodeMarcao1: TMenuItem
        Caption = 'Importa'#231#227'o de Marca'#231#227'o'
      end
    end
    object Funconario1: TMenuItem
      Caption = 'Func'#237'onario'
      object EnviarCadastro1: TMenuItem
        Caption = 'Enviar Cadastro'
      end
    end
  end
end
