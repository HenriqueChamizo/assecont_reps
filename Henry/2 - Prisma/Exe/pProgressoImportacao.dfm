object frmProgressoImportacao: TfrmProgressoImportacao
  Left = 0
  Top = 0
  BorderIcons = [biSystemMenu]
  BorderStyle = bsSingle
  Caption = 'Importa'#231#227'o'
  ClientHeight = 191
  ClientWidth = 437
  Color = clWhite
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  FormStyle = fsStayOnTop
  OldCreateOrder = False
  Position = poMainFormCenter
  OnClose = FormClose
  OnCloseQuery = FormCloseQuery
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object lbMensagem: TLabel
    Left = 8
    Top = 141
    Width = 93
    Height = 13
    Caption = 'Importando Bloco 1'
    Visible = False
  end
  object ProgressBar1: TProgressBar
    Left = 8
    Top = 160
    Width = 337
    Height = 17
    TabOrder = 0
  end
  object Button1: TButton
    Left = 352
    Top = 155
    Width = 75
    Height = 25
    Caption = 'Cancelar'
    TabOrder = 1
    OnClick = Button1Click
  end
end
