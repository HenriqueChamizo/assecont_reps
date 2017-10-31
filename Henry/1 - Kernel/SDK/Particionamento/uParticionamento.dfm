object frmCapturaImagem: TfrmCapturaImagem
  Left = 0
  Top = 0
  Caption = 'Captura Imagem'
  ClientHeight = 315
  ClientWidth = 263
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
    Width = 263
    Height = 315
    Align = alClient
    TabOrder = 0
    object stbrHint: TStatusBar
      Left = 1
      Top = 295
      Width = 261
      Height = 19
      Panels = <>
    end
    object Panel3: TPanel
      Left = 1
      Top = 1
      Width = 261
      Height = 294
      Align = alLeft
      TabOrder = 1
      object Panel4: TPanel
        Left = 1
        Top = 216
        Width = 259
        Height = 77
        Align = alBottom
        TabOrder = 0
        object btnConectar: TButton
          Left = 4
          Top = 6
          Width = 82
          Height = 25
          Caption = 'Conectar'
          TabOrder = 0
          OnClick = btnConectarClick
        end
        object btnDesconectar: TButton
          Left = 173
          Top = 6
          Width = 82
          Height = 25
          Caption = 'Desconectar'
          TabOrder = 1
          OnClick = btnDesconectarClick
        end
        object btnFormatar: TButton
          Left = 92
          Top = 6
          Width = 75
          Height = 25
          Caption = 'Formatar'
          TabOrder = 2
          OnClick = btnFormatarClick
        end
      end
      object pgctrlConexao: TPageControl
        Left = 1
        Top = 41
        Width = 259
        Height = 175
        ActivePage = tsTcpIP
        Align = alClient
        TabOrder = 1
        object tsTcpIP: TTabSheet
          Caption = 'TcpIP'
          object Label7: TLabel
            Left = 22
            Top = 69
            Width = 33
            Height = 13
            Caption = 'Porta: '
          end
          object Label6: TLabel
            Left = 38
            Top = 37
            Width = 17
            Height = 13
            Caption = 'IP: '
          end
          object edtPortaTCP: TEdit
            Left = 61
            Top = 66
            Width = 52
            Height = 21
            TabOrder = 0
            Text = '3000'
          end
          object edtIP: TMaskEdit
            Left = 61
            Top = 32
            Width = 98
            Height = 21
            EditMask = '999.999.999.999;1; '
            MaxLength = 15
            TabOrder = 1
            Text = '   .   .   .   '
          end
        end
        object tsSerial: TTabSheet
          Caption = 'Serial'
          ImageIndex = 1
          ExplicitLeft = 0
          ExplicitTop = 0
          ExplicitWidth = 231
          ExplicitHeight = 151
          object Label8: TLabel
            Left = 11
            Top = 32
            Width = 54
            Height = 13
            Caption = 'N'#186' Rel'#243'gio:'
          end
          object Label9: TLabel
            Left = 10
            Top = 72
            Width = 33
            Height = 13
            Caption = 'Porta: '
          end
          object edtNrelogio: TEdit
            Left = 69
            Top = 28
            Width = 47
            Height = 21
            TabOrder = 0
          end
          object cbxPortaCom: TComboBox
            Left = 44
            Top = 68
            Width = 72
            Height = 21
            ItemHeight = 0
            TabOrder = 1
          end
          object rgVelocidade: TRadioGroup
            Left = 125
            Top = 24
            Width = 69
            Height = 105
            Caption = 'Velocidade'
            ItemIndex = 0
            Items.Strings = (
              '9600'
              '19200'
              '57600'
              '115200')
            TabOrder = 2
          end
        end
      end
      object Panel5: TPanel
        Left = 1
        Top = 1
        Width = 259
        Height = 40
        Align = alTop
        TabOrder = 2
        object Label5: TLabel
          Left = 14
          Top = 16
          Width = 49
          Height = 13
          Caption = 'Conex'#227'o'
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'Tahoma'
          Font.Style = [fsBold]
          ParentFont = False
        end
      end
    end
  end
end
