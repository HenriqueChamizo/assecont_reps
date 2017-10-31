object fColeta: TfColeta
  Left = 467
  Top = 272
  BorderIcons = [biSystemMenu, biMinimize]
  Caption = 'Coleta USB...'
  ClientHeight = 317
  ClientWidth = 432
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object bAddEquipamento: TButton
    Left = 256
    Top = 24
    Width = 105
    Height = 25
    Caption = 'Adicionar'
    TabOrder = 0
    OnClick = bAddEquipamentoClick
  end
  object mInf: TMemo
    Left = 24
    Top = 24
    Width = 209
    Height = 225
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clBlue
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    ReadOnly = True
    TabOrder = 1
  end
  object bColeta: TButton
    Left = 256
    Top = 104
    Width = 105
    Height = 25
    Caption = 'Coletar'
    Enabled = False
    TabOrder = 2
    OnClick = bColetaClick
  end
  object bVerifica: TButton
    Left = 256
    Top = 64
    Width = 105
    Height = 25
    Caption = 'Verificar registros'
    Enabled = False
    TabOrder = 3
    OnClick = bVerificaClick
  end
  object bRecuperaRegistros: TButton
    Left = 256
    Top = 144
    Width = 105
    Height = 25
    Caption = 'Recuperar registros'
    Enabled = False
    TabOrder = 4
    OnClick = bRecuperaRegistrosClick
  end
  object stbStatus: TStatusBar
    Left = 0
    Top = 298
    Width = 432
    Height = 19
    Panels = <
      item
        Width = 50
      end>
  end
  object bRemove: TButton
    Left = 256
    Top = 184
    Width = 105
    Height = 25
    Caption = 'Remover USB'
    TabOrder = 6
    OnClick = bRemoveClick
  end
  object prgbProgresso: TProgressBar
    Left = 24
    Top = 269
    Width = 385
    Height = 17
    TabOrder = 7
  end
  object _kernel7x: TKernel
    AutoConnect = False
    ConnectKind = ckRunningOrNew
    Left = 328
    Top = 216
  end
end
