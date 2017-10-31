object Form2: TForm2
  Left = 0
  Top = 0
  BorderStyle = bsNone
  Caption = 'Serial 485'
  ClientHeight = 179
  ClientWidth = 365
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  KeyPreview = True
  OldCreateOrder = False
  Position = poMainFormCenter
  OnClose = FormClose
  OnCreate = FormCreate
  OnKeyUp = FormKeyUp
  PixelsPerInch = 96
  TextHeight = 13
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 365
    Height = 179
    Align = alClient
    Color = clWhite
    TabOrder = 0
    object spdAccept: TSpeedButton
      Left = 323
      Top = 153
      Width = 38
      Height = 25
      AllowAllUp = True
      Caption = 'Accept'
      Flat = True
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
      OnClick = spdAcceptClick
    end
    object cbx485: TCheckListBox
      Left = 4
      Top = 0
      Width = 357
      Height = 156
      BorderStyle = bsNone
      Columns = 4
      ItemHeight = 13
      TabOrder = 0
    end
  end
end
