object Form2: TForm2
  Left = 0
  Top = 0
  Caption = 'Form2'
  ClientHeight = 369
  ClientWidth = 554
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 32
    Top = 64
    Width = 86
    Height = 13
    Caption = 'Arquivo da Coleta'
  end
  object Label2: TLabel
    Left = 32
    Top = 112
    Width = 10
    Height = 13
    Caption = 'Ip'
  end
  object Porta: TLabel
    Left = 32
    Top = 160
    Width = 26
    Height = 13
    Caption = 'Porta'
  end
  object lbMensagem: TLabel
    Left = 32
    Top = 264
    Width = 59
    Height = 13
    Caption = 'lbMensagem'
  end
  object Button1: TButton
    Left = 32
    Top = 8
    Width = 75
    Height = 25
    Caption = 'Conectar'
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 113
    Top = 8
    Width = 75
    Height = 25
    Caption = 'Importar'
    TabOrder = 1
    OnClick = Button2Click
  end
  object edArquivo: TEdit
    Left = 32
    Top = 83
    Width = 121
    Height = 21
    TabOrder = 2
    Text = 'c:\teste.txt'
  end
  object edIp: TEdit
    Left = 32
    Top = 131
    Width = 121
    Height = 21
    TabOrder = 3
    Text = '192.168.1.93'
  end
  object edPorta: TEdit
    Left = 32
    Top = 179
    Width = 121
    Height = 21
    TabOrder = 4
    Text = '1001'
  end
  object Kernel1: TKernel
    AutoConnect = False
    ConnectKind = ckNewInstance
    OnProgresso = Kernel1Progresso
    OnColetaEventos = Kernel1ColetaEventos
    Left = 360
    Top = 120
  end
end
