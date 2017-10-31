object Form1: TForm1
  Left = 375
  Top = 167
  BorderStyle = bsSingle
  Caption = 'Exemplo de Mensagem Padr'#227'o'
  ClientHeight = 330
  ClientWidth = 348
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object lbstatus: TLabel
    Left = 14
    Top = 472
    Width = 3
    Height = 13
    Color = clBtnFace
    ParentColor = False
  end
  object GroupBox1: TGroupBox
    Left = 0
    Top = 0
    Width = 348
    Height = 81
    Align = alTop
    Caption = 'Comunica'#231#227'o'
    TabOrder = 0
    object Label1: TLabel
      Left = 32
      Top = 24
      Width = 10
      Height = 13
      Caption = 'IP'
    end
    object eIP: TEdit
      Left = 32
      Top = 40
      Width = 129
      Height = 21
      TabOrder = 0
    end
    object Button1: TButton
      Left = 200
      Top = 24
      Width = 105
      Height = 41
      Caption = 'Adiciona'
      TabOrder = 1
      OnClick = Button1Click
    end
  end
  object Button4: TButton
    Left = 240
    Top = 280
    Width = 105
    Height = 41
    Caption = 'Enviar'
    TabOrder = 1
    OnClick = Button4Click
  end
  object PageControl1: TPageControl
    Left = 0
    Top = 81
    Width = 348
    Height = 193
    ActivePage = TabSheet1
    Align = alTop
    TabOrder = 2
    object TabSheet1: TTabSheet
      Caption = 'Padr'#227'o'
      object GroupBox3: TGroupBox
        Left = 8
        Top = 12
        Width = 321
        Height = 121
        Caption = 'Mensagem Padr'#227'o'
        TabOrder = 0
        object Label2: TLabel
          Left = 8
          Top = 72
          Width = 35
          Height = 13
          Caption = 'Linha 1'
        end
        object Label3: TLabel
          Left = 8
          Top = 96
          Width = 35
          Height = 13
          Caption = 'Linha 2'
        end
        object cbxMsg: TComboBox
          Left = 8
          Top = 32
          Width = 145
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          Items.Strings = (
            'DataHora'
            'Personalizada')
        end
        object eLin1: TEdit
          Left = 64
          Top = 64
          Width = 137
          Height = 21
          MaxLength = 16
          TabOrder = 1
        end
        object eLin2: TEdit
          Left = 64
          Top = 88
          Width = 137
          Height = 21
          MaxLength = 16
          TabOrder = 2
        end
      end
    end
    object TabSheet2: TTabSheet
      Caption = 'Entrada'
      ImageIndex = 1
      object GroupBox2: TGroupBox
        Left = 8
        Top = 12
        Width = 321
        Height = 113
        Caption = 'Entrada'
        TabOrder = 0
        object Label4: TLabel
          Left = 8
          Top = 64
          Width = 35
          Height = 13
          Caption = 'Linha 1'
        end
        object Label5: TLabel
          Left = 8
          Top = 88
          Width = 35
          Height = 13
          Caption = 'Linha 2'
        end
        object cbxEntrada: TComboBox
          Left = 8
          Top = 24
          Width = 145
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          Items.Strings = (
            'Matricula'
            'DataHora'
            'Personalizada')
        end
        object eEntLin1: TEdit
          Left = 64
          Top = 56
          Width = 137
          Height = 21
          MaxLength = 16
          TabOrder = 1
        end
        object eEntLin2: TEdit
          Left = 64
          Top = 80
          Width = 137
          Height = 21
          MaxLength = 16
          TabOrder = 2
        end
      end
    end
    object TabSheet3: TTabSheet
      Caption = 'Sa'#237'da'
      ImageIndex = 2
      object GroupBox4: TGroupBox
        Left = 8
        Top = 4
        Width = 321
        Height = 121
        Caption = 'Saida'
        TabOrder = 0
        object Label6: TLabel
          Left = 8
          Top = 72
          Width = 35
          Height = 13
          Caption = 'Linha 1'
        end
        object Label7: TLabel
          Left = 8
          Top = 96
          Width = 35
          Height = 13
          Caption = 'Linha 2'
        end
        object cbxSaida: TComboBox
          Left = 8
          Top = 24
          Width = 145
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          Items.Strings = (
            'Matricula'
            'DataHora'
            'Personalizada')
        end
        object eSaiLin1: TEdit
          Left = 72
          Top = 64
          Width = 129
          Height = 21
          MaxLength = 16
          TabOrder = 1
        end
        object eSaiLin2: TEdit
          Left = 72
          Top = 88
          Width = 129
          Height = 21
          MaxLength = 16
          TabOrder = 2
        end
      end
    end
  end
  object Kernel1: TKernel
    AutoConnect = False
    ConnectKind = ckRunningOrNew
    Left = 176
  end
end
