object Form1: TForm1
  Left = 192
  Top = 114
  Caption = 'Utilizando Hamster'
  ClientHeight = 383
  ClientWidth = 608
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object btnCapturar: TButton
    Left = 40
    Top = 200
    Width = 97
    Height = 25
    Caption = 'Capturar'
    TabOrder = 0
    OnClick = btnCapturarClick
  end
  object btnVerificar: TButton
    Left = 176
    Top = 200
    Width = 97
    Height = 25
    Caption = 'Verificar'
    Enabled = False
    TabOrder = 1
    OnClick = btnVerificarClick
  end
  object pnlDig1: TPanel
    Left = 32
    Top = 32
    Width = 113
    Height = 145
    Caption = 'Digital 1'
    TabOrder = 2
  end
  object pnlDig2: TPanel
    Left = 168
    Top = 32
    Width = 113
    Height = 145
    Caption = 'Digital 2'
    TabOrder = 3
  end
  object GroupBox1: TGroupBox
    Left = 320
    Top = 32
    Width = 185
    Height = 145
    Caption = 'Determinado usu'#225'rio'
    TabOrder = 4
    object Label1: TLabel
      Left = 8
      Top = 32
      Width = 45
      Height = 13
      Caption = 'Matr'#237'cula'
    end
    object eMatricula: TEdit
      Left = 24
      Top = 48
      Width = 137
      Height = 21
      TabOrder = 0
    end
    object chkMaster: TCheckBox
      Left = 8
      Top = 96
      Width = 57
      Height = 17
      Caption = 'Master'
      TabOrder = 1
    end
  end
  object btnCarregar: TButton
    Left = 328
    Top = 200
    Width = 75
    Height = 25
    Caption = 'Carregar'
    TabOrder = 5
    OnClick = btnCarregarClick
  end
  object btnProcurar: TButton
    Left = 416
    Top = 200
    Width = 75
    Height = 25
    Caption = 'Procurar'
    TabOrder = 6
    OnClick = btnProcurarClick
  end
  object Button1: TButton
    Left = 40
    Top = 232
    Width = 97
    Height = 25
    Caption = 'Captura continua'
    TabOrder = 7
    OnClick = Button1Click
  end
  object Hamster1: THamster
    AutoConnect = False
    ConnectKind = ckRunningOrNew
    Left = 464
    Top = 264
  end
  object Kernel1: TKernel
    AutoConnect = False
    ConnectKind = ckRunningOrNew
    Left = 536
    Top = 272
  end
end
