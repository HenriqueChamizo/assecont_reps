object Frm_MSGXREP: TFrm_MSGXREP
  Left = 379
  Top = 237
  Caption = 'Envia MSG XREP'
  ClientHeight = 254
  ClientWidth = 428
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
    Left = 8
    Top = 204
    Width = 86
    Height = 16
    Caption = 'Endere'#231'o IP'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object Label2: TLabel
    Left = 136
    Top = 204
    Width = 38
    Height = 16
    Caption = 'Porta'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = [fsBold]
    ParentFont = False
  end
  object bt_EnviaMsg: TButton
    Left = 208
    Top = 216
    Width = 81
    Height = 25
    Caption = 'Envia '
    TabOrder = 2
    OnClick = bt_EnviaMsgClick
  end
  object mm_Log: TMemo
    Left = 0
    Top = 0
    Width = 425
    Height = 193
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'Courier New'
    Font.Style = [fsBold]
    ParentFont = False
    ScrollBars = ssBoth
    TabOrder = 3
  end
  object edt_IP: TEdit
    Left = 8
    Top = 220
    Width = 121
    Height = 21
    TabOrder = 0
    Text = '192.168.1.91'
    OnKeyPress = edt_IPKeyPress
  end
  object edt_Porta: TEdit
    Left = 136
    Top = 220
    Width = 57
    Height = 21
    TabOrder = 1
    Text = '2101'
    OnKeyPress = edt_PortaKeyPress
  end
  object CS_XREP: TIdTCPClient
    ConnectTimeout = 0
    IPVersion = Id_IPv4
    Port = 0
    ReadTimeout = 0
    Left = 208
    Top = 72
  end
end
