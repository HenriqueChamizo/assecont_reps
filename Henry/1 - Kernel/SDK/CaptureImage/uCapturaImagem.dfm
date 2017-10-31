object frmCapturaImagem: TfrmCapturaImagem
  Left = 0
  Top = 0
  Caption = 'Captura Imagem'
  ClientHeight = 186
  ClientWidth = 270
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 13
  object pnlFundo: TPanel
    Left = 0
    Top = 0
    Width = 270
    Height = 186
    Align = alClient
    TabOrder = 0
    object btnCapturar: TButton
      Left = 153
      Top = 41
      Width = 80
      Height = 25
      Caption = 'Capturar'
      TabOrder = 0
      OnClick = btnCapturarClick
    end
    object btnSalvar: TButton
      Left = 153
      Top = 72
      Width = 80
      Height = 25
      Caption = 'Salvar'
      TabOrder = 1
      OnClick = btnSalvarClick
    end
    object pnlDig: TPanel
      Left = 16
      Top = 19
      Width = 105
      Height = 129
      TabOrder = 2
    end
    object stbrHint: TStatusBar
      Left = 1
      Top = 166
      Width = 268
      Height = 19
      Panels = <>
    end
  end
end
