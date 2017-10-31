object Form1: TForm1
  Left = 192
  Top = 114
  Caption = 'Form1'
  ClientHeight = 448
  ClientWidth = 646
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object Label6: TLabel
    Left = 136
    Top = 280
    Width = 35
    Height = 13
    Caption = 'Acesso'
  end
  object Label3: TLabel
    Left = 128
    Top = 248
    Width = 33
    Height = 13
    Caption = 'Tempo'
  end
  object SpeedButton1: TSpeedButton
    Left = 8
    Top = 393
    Width = 112
    Height = 28
    Caption = '&Excluir Template'
    OnClick = SpeedButton1Click
  end
  object Label2: TLabel
    Left = 198
    Top = 328
    Width = 92
    Height = 13
    Caption = 'Retorno do Rel'#243'gio'
  end
  object btnCapturar: TButton
    Left = 8
    Top = 248
    Width = 112
    Height = 28
    Caption = 'Capturar'
    TabOrder = 0
    OnClick = btnCapturarClick
  end
  object btnVerificar: TButton
    Left = 8
    Top = 283
    Width = 112
    Height = 28
    Caption = 'Verificar'
    Enabled = False
    TabOrder = 1
    OnClick = btnVerificarClick
  end
  object pnlDig1: TPanel
    Left = 5
    Top = 6
    Width = 229
    Height = 200
    Caption = 'Digital 1'
    TabOrder = 2
  end
  object pnlDig2: TPanel
    Left = 240
    Top = 8
    Width = 230
    Height = 197
    Caption = 'Digital 2'
    TabOrder = 3
  end
  object GroupBox1: TGroupBox
    Left = 473
    Top = 6
    Width = 167
    Height = 171
    Caption = 'Determinado usu'#225'rio'
    TabOrder = 4
    object Label1: TLabel
      Left = 24
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
      Text = '1230'
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
    Left = 8
    Top = 321
    Width = 112
    Height = 28
    Caption = 'Carregar'
    TabOrder = 5
    OnClick = btnCarregarClick
  end
  object btnProcurar: TButton
    Left = 477
    Top = 356
    Width = 112
    Height = 28
    Caption = 'Procurar'
    TabOrder = 6
    OnClick = btnProcurarClick
  end
  object Button1: TButton
    Left = 8
    Top = 360
    Width = 112
    Height = 28
    Caption = '&Envia Rel'#243'gio'
    TabOrder = 7
    OnClick = Button1Click
  end
  object speTempo: TSpinEdit
    Left = 192
    Top = 248
    Width = 121
    Height = 22
    MaxValue = 255
    MinValue = 1
    TabOrder = 8
    Value = 3
  end
  object cbxAcesso: TComboBox
    Left = 192
    Top = 280
    Width = 121
    Height = 21
    Style = csDropDownList
    ItemHeight = 13
    ItemIndex = 1
    TabOrder = 9
    Text = 'Libera Entrada'
    Items.Strings = (
      'Negado'
      'Libera Entrada'
      'Libera Saida'
      'Revista'
      'AmbosLados')
  end
  object Edit1: TEdit
    Left = 192
    Top = 344
    Width = 233
    Height = 21
    TabOrder = 10
  end
  object StatusBar1: TStatusBar
    Left = 0
    Top = 429
    Width = 646
    Height = 19
    Panels = <>
    ExplicitTop = 431
    ExplicitWidth = 654
  end
  object BitBtn1: TBitBtn
    Left = 477
    Top = 393
    Width = 112
    Height = 28
    Caption = '&Sa'#237'da'
    TabOrder = 12
    OnClick = BitBtn1Click
  end
  object Hamster1: THamster
    AutoConnect = False
    ConnectKind = ckRunningOrNew
    Left = 440
    Top = 208
  end
  object Kernel1: TKernel
    AutoConnect = False
    ConnectKind = ckRunningOrNew
    Left = 520
    Top = 216
  end
end
