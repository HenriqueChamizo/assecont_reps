object Form1: TForm1
  Left = 193
  Top = 115
  Caption = 'Form1'
  ClientHeight = 128
  ClientWidth = 346
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
    Top = 32
    Width = 89
    Height = 13
    Caption = 'IP do equipamento'
  end
  object Button1: TButton
    Left = 168
    Top = 48
    Width = 75
    Height = 25
    Caption = 'Enviar'
    TabOrder = 0
    OnClick = Button1Click
  end
  object eIp: TEdit
    Left = 32
    Top = 48
    Width = 121
    Height = 21
    TabOrder = 1
    Text = '192.168.0.113'
  end
  object Kernel1: TKernel
    AutoConnect = False
    ConnectKind = ckRunningOrNew
    Left = 256
    Top = 8
  end
end
