object frmCapturaImagem: TfrmCapturaImagem
  Left = 0
  Top = 0
  Caption = 'Captura Imagem'
  ClientHeight = 300
  ClientWidth = 543
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
    Width = 543
    Height = 300
    Align = alClient
    TabOrder = 0
    object stbrHint: TStatusBar
      Left = 1
      Top = 280
      Width = 541
      Height = 19
      Panels = <>
    end
    object Panel1: TPanel
      Left = 242
      Top = 1
      Width = 300
      Height = 279
      Align = alClient
      TabOrder = 1
      object lblMat: TLabel
        Left = 40
        Top = 176
        Width = 94
        Height = 13
        Caption = 'Matr'#237'cula (8 d'#237'gitos)'
      end
      object pnlDig: TPanel
        Left = 24
        Top = 17
        Width = 105
        Height = 129
        TabOrder = 0
      end
      object btnCapturar: TButton
        Left = 169
        Top = 70
        Width = 75
        Height = 25
        Caption = 'Capturar'
        TabOrder = 1
        OnClick = btnCapturarClick
      end
      object btnEnviar: TButton
        Left = 192
        Top = 226
        Width = 75
        Height = 25
        Caption = 'Enviar'
        TabOrder = 2
        OnClick = btnEnviarClick
      end
      object chkMaster: TCheckBox
        Left = 40
        Top = 230
        Width = 73
        Height = 17
        Caption = 'Master'
        TabOrder = 3
      end
      object edtMatricula: TEdit
        Left = 40
        Top = 195
        Width = 121
        Height = 21
        TabOrder = 4
      end
    end
    object Panel3: TPanel
      Left = 1
      Top = 1
      Width = 241
      Height = 279
      Align = alLeft
      TabOrder = 2
      object Panel4: TPanel
        Left = 1
        Top = 220
        Width = 239
        Height = 58
        Align = alBottom
        TabOrder = 0
        object btnConectar: TButton
          Left = 15
          Top = 6
          Width = 82
          Height = 25
          Caption = 'Conectar'
          TabOrder = 0
          OnClick = btnConectarClick
        end
        object btnDesconectar: TButton
          Left = 129
          Top = 8
          Width = 82
          Height = 25
          Caption = 'Desconectar'
          TabOrder = 1
          OnClick = btnDesconectarClick
        end
      end
      object pgctrlConexao: TPageControl
        Left = 1
        Top = 41
        Width = 239
        Height = 179
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
          ExplicitWidth = 0
          ExplicitHeight = 0
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
        Width = 239
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
