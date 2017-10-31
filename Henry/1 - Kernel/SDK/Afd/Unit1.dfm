object Form1: TForm1
  Left = 192
  Top = 114
  Caption = 'Form1'
  ClientHeight = 132
  ClientWidth = 331
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 32
    Top = 24
    Width = 13
    Height = 13
    Caption = 'IP:'
  end
  object Label2: TLabel
    Left = 24
    Top = 56
    Width = 28
    Height = 13
    Caption = 'Porta:'
  end
  object edtIp: TEdit
    Left = 64
    Top = 16
    Width = 121
    Height = 21
    TabOrder = 0
    Text = '192.168.1.93'
  end
  object edtPorta: TEdit
    Left = 64
    Top = 48
    Width = 121
    Height = 21
    TabOrder = 1
    Text = '1001'
  end
  object Button1: TButton
    Left = 240
    Top = 16
    Width = 75
    Height = 25
    Caption = 'Conectar'
    TabOrder = 2
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 240
    Top = 48
    Width = 75
    Height = 25
    Caption = 'Baixar'
    Enabled = False
    TabOrder = 3
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 240
    Top = 80
    Width = 75
    Height = 25
    Caption = 'Desconectar'
    Enabled = False
    TabOrder = 4
    OnClick = Button3Click
  end
  object Kernel1: TKernel
    AutoConnect = False
    ConnectKind = ckRunningOrNew
    Left = 64
    Top = 80
  end
end
