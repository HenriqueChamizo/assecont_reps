object frmComunicaDSP: TfrmComunicaDSP
  Left = 0
  Top = 0
  AutoSize = True
  Caption = 'Comunica DSP'
  ClientHeight = 346
  ClientWidth = 770
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poDesktopCenter
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object stbrHint: TStatusBar
    Left = 0
    Top = 327
    Width = 770
    Height = 19
    Panels = <>
  end
  object Panel2: TPanel
    Left = 0
    Top = 286
    Width = 770
    Height = 41
    Align = alBottom
    TabOrder = 1
    object btnReceber: TButton
      Left = 189
      Top = 10
      Width = 75
      Height = 25
      Caption = 'Receber'
      Enabled = False
      TabOrder = 0
      OnClick = btnReceberClick
    end
    object btnEnviar: TButton
      Left = 74
      Top = 10
      Width = 75
      Height = 25
      Caption = 'Enviar'
      Enabled = False
      TabOrder = 1
      OnClick = btnEnviarClick
    end
  end
  object Panel3: TPanel
    Left = 0
    Top = 0
    Width = 241
    Height = 286
    Align = alLeft
    TabOrder = 2
    object Panel1: TPanel
      Left = 1
      Top = 227
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
      Height = 186
      ActivePage = tsSerial
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
          ItemHeight = 13
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
  object Panel4: TPanel
    Left = 241
    Top = 0
    Width = 529
    Height = 286
    Align = alClient
    TabOrder = 3
    object lblCapac: TLabel
      Left = 226
      Top = 255
      Width = 3
      Height = 13
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object lblQtdeMat: TLabel
      Left = 248
      Top = 228
      Width = 3
      Height = 13
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object Label13: TLabel
      Left = 56
      Top = 255
      Width = 168
      Height = 13
      Caption = 'Capacidade total do equipamento: '
    end
    object Label14: TLabel
      Left = 56
      Top = 228
      Width = 171
      Height = 13
      Caption = 'Quantidade de digitais registradas: '
    end
    object Bevel5: TBevel
      Left = 19
      Top = 190
      Width = 470
      Height = 7
      Shape = bsTopLine
    end
    object Label15: TLabel
      Left = 19
      Top = 203
      Width = 132
      Height = 13
      Caption = 'Informa'#231#245'es Adicionais'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object lblSensibilidade: TLabel
      Left = 174
      Top = 151
      Width = 16
      Height = 16
      Caption = '48'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlue
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object Label17: TLabel
      Left = 43
      Top = 151
      Width = 61
      Height = 13
      Caption = 'Sensibilidade'
    end
    object Label18: TLabel
      Left = 43
      Top = 114
      Width = 108
      Height = 13
      Caption = 'Qualidade do cadastro'
    end
    object lblQualidade: TLabel
      Left = 174
      Top = 112
      Width = 16
      Height = 16
      Caption = '48'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlue
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object lblModoRapido: TLabel
      Left = 174
      Top = 74
      Width = 16
      Height = 16
      Caption = '48'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlue
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object Label21: TLabel
      Left = 43
      Top = 76
      Width = 59
      Height = 13
      Caption = 'Modo r'#225'pido'
    end
    object Label22: TLabel
      Left = 43
      Top = 40
      Width = 121
      Height = 13
      Caption = 'Seguran'#231'a da verifica'#231#227'o'
    end
    object lblSeguranca: TLabel
      Left = 174
      Top = 38
      Width = 16
      Height = 16
      Caption = '48'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clBlue
      Font.Height = -13
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object Label24: TLabel
      Left = 19
      Top = 17
      Width = 175
      Height = 13
      Caption = 'Configura'#231#245'es do equipamento'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = [fsBold]
      ParentFont = False
    end
    object Bevel6: TBevel
      Left = 351
      Top = 62
      Width = 138
      Height = 7
      Shape = bsTopLine
    end
    object Label25: TLabel
      Left = 354
      Top = 43
      Width = 116
      Height = 13
      Caption = 'Condi'#231#245'es de ilumina'#231#227'o'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Tahoma'
      Font.Style = []
      ParentFont = False
    end
    object trbSensibilidade: TTrackBar
      Left = 190
      Top = 146
      Width = 150
      Height = 21
      Max = 55
      Min = 48
      Position = 48
      TabOrder = 0
      TickMarks = tmBoth
      TickStyle = tsNone
      OnChange = trbSensibilidadeChange
    end
    object trbQualidade: TTrackBar
      Left = 190
      Top = 112
      Width = 150
      Height = 33
      Max = 51
      Min = 48
      Position = 48
      TabOrder = 1
      TickMarks = tmBoth
      TickStyle = tsNone
      OnChange = trbQualidadeChange
    end
    object trbModoRapido: TTrackBar
      Left = 190
      Top = 75
      Width = 150
      Height = 26
      Max = 54
      Min = 48
      Position = 48
      TabOrder = 2
      TickMarks = tmBoth
      TickStyle = tsNone
      OnChange = trbModoRapidoChange
    end
    object trbSeguranca: TTrackBar
      Left = 190
      Top = 36
      Width = 150
      Height = 33
      Max = 82
      Min = 48
      Position = 48
      TabOrder = 3
      TickMarks = tmBoth
      TickStyle = tsNone
      OnChange = trbSegurancaChange
    end
    object rbInterno: TRadioButton
      Left = 371
      Top = 98
      Width = 113
      Height = 17
      Caption = 'Ambiente Interno'
      TabOrder = 4
    end
    object rbExterno: TRadioButton
      Left = 371
      Top = 75
      Width = 113
      Height = 17
      Caption = 'Ambiente Externo'
      TabOrder = 5
    end
  end
end
