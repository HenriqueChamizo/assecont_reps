object frmOnImagem: TfrmOnImagem
  Left = 0
  Top = 0
  Caption = 'Exemplo onImagem'
  ClientHeight = 131
  ClientWidth = 320
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 28
    Top = 35
    Width = 14
    Height = 13
    Caption = 'Ip:'
  end
  object edtIP: TEdit
    Left = 48
    Top = 32
    Width = 121
    Height = 21
    TabOrder = 0
    Text = '192.168.1.93'
  end
  object btnConectar: TButton
    Left = 192
    Top = 30
    Width = 75
    Height = 25
    Caption = 'Conectar'
    TabOrder = 1
    OnClick = btnConectarClick
  end
  object stbrStatus: TStatusBar
    Left = 0
    Top = 112
    Width = 320
    Height = 19
    Panels = <>
    SimplePanel = True
  end
  object kernel7x: TKernel
    AutoConnect = False
    ConnectKind = ckRunningOrNew
    OnImagemDsp = kernel7xImagemDsp
    Left = 280
    Top = 8
  end
end
