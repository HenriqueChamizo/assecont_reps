object Form1: TForm1
  Left = 360
  Top = 240
  Caption = 'Teste Online/Offline'
  ClientHeight = 395
  ClientWidth = 475
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
  object PageControl1: TPageControl
    Left = 0
    Top = 0
    Width = 475
    Height = 121
    ActivePage = TabSheet3
    Align = alTop
    TabOrder = 0
    object TabSheet3: TTabSheet
      Caption = 'Serial 485'
      ImageIndex = 2
      object Label7: TLabel
        Left = 20
        Top = 11
        Width = 54
        Height = 13
        Caption = 'Porta Serial'
      end
      object Label8: TLabel
        Left = 33
        Top = 43
        Width = 41
        Height = 13
        Caption = 'Rel'#243'gios'
      end
      object spdNumRel: TSpeedButton
        Left = 93
        Top = 40
        Width = 20
        Height = 20
        Caption = '...'
        OnClick = spdNumRelClick
      end
      object Label9: TLabel
        Left = 21
        Top = 74
        Width = 53
        Height = 13
        Caption = 'Velocidade'
      end
      object edtPorta: TEdit
        Left = 93
        Top = 8
        Width = 124
        Height = 21
        TabOrder = 0
        Text = 'COM1'
      end
      object btnAdd485: TButton
        Tag = 3
        Left = 235
        Top = 43
        Width = 89
        Height = 25
        Caption = 'Adicionar'
        Enabled = False
        TabOrder = 1
        OnClick = btnAddSerialClick
      end
      object cbxVelocidade: TComboBox
        Left = 93
        Top = 66
        Width = 124
        Height = 21
        ItemHeight = 13
        ItemIndex = 0
        TabOrder = 2
        Text = '9600'
        Items.Strings = (
          '9600'
          '19200'
          '57600'
          '115200')
      end
      object cbxRel: TComboBox
        Left = 124
        Top = 39
        Width = 93
        Height = 21
        ItemHeight = 13
        TabOrder = 3
      end
    end
    object TabSheet1: TTabSheet
      Caption = 'Serial'
      object Label4: TLabel
        Left = 16
        Top = 16
        Width = 54
        Height = 13
        Caption = 'Porta Serial'
      end
      object Label5: TLabel
        Left = 16
        Top = 40
        Width = 76
        Height = 13
        Caption = 'N'#250'mero Rel'#243'gio'
      end
      object btnAddSerial: TButton
        Left = 216
        Top = 16
        Width = 113
        Height = 25
        Caption = 'Adicionar'
        TabOrder = 0
        OnClick = btnAddSerialClick
      end
      object ePorta: TEdit
        Left = 104
        Top = 16
        Width = 97
        Height = 21
        TabOrder = 1
        Text = 'COM1'
      end
      object eNumero: TEdit
        Left = 104
        Top = 40
        Width = 97
        Height = 21
        TabOrder = 2
        Text = '1'
      end
      object Button2: TButton
        Left = 216
        Top = 48
        Width = 113
        Height = 25
        Caption = 'Detectar Velocidade'
        TabOrder = 3
        OnClick = Button2Click
      end
    end
    object TabSheet2: TTabSheet
      Caption = 'Tcp-Ip'
      ImageIndex = 1
      object Label1: TLabel
        Left = 16
        Top = 24
        Width = 13
        Height = 13
        Caption = 'IP:'
      end
      object eIP: TEdit
        Left = 40
        Top = 24
        Width = 121
        Height = 21
        TabOrder = 0
      end
      object btnAddTcp: TButton
        Tag = 1
        Left = 192
        Top = 24
        Width = 105
        Height = 25
        Caption = 'Adicionar'
        TabOrder = 1
        OnClick = btnAddSerialClick
      end
    end
  end
  object Button3: TButton
    Left = 8
    Top = 128
    Width = 113
    Height = 25
    Caption = 'Quant Registros'
    TabOrder = 1
    OnClick = Button3Click
  end
  object btnColetar: TButton
    Left = 128
    Top = 128
    Width = 113
    Height = 25
    Caption = 'Coletar Registros'
    Enabled = False
    TabOrder = 2
    OnClick = btnColetarClick
  end
  object Memo1: TMemo
    Left = 0
    Top = 298
    Width = 475
    Height = 97
    Align = alBottom
    TabOrder = 3
  end
  object GroupBox1: TGroupBox
    Left = 0
    Top = 161
    Width = 475
    Height = 137
    Align = alBottom
    Caption = 'Resposta Personalizada'
    TabOrder = 4
    object Label2: TLabel
      Left = 16
      Top = 36
      Width = 55
      Height = 13
      Caption = 'Mensagem:'
    end
    object Label3: TLabel
      Left = 16
      Top = 64
      Width = 33
      Height = 13
      Caption = 'Tempo'
    end
    object Label6: TLabel
      Left = 24
      Top = 96
      Width = 35
      Height = 13
      Caption = 'Acesso'
    end
    object eMsg: TEdit
      Left = 80
      Top = 36
      Width = 121
      Height = 21
      MaxLength = 32
      TabOrder = 0
      Text = 'Welcome to 7x'
    end
    object speTempo: TSpinEdit
      Left = 80
      Top = 64
      Width = 121
      Height = 22
      MaxValue = 255
      MinValue = 1
      TabOrder = 1
      Value = 3
    end
    object cbxAcesso: TComboBox
      Left = 80
      Top = 96
      Width = 121
      Height = 21
      Style = csDropDownList
      ItemHeight = 13
      ItemIndex = 1
      TabOrder = 2
      Text = 'Libera Entrada'
      Items.Strings = (
        'Negado'
        'Libera Entrada'
        'Libera Saida'
        'Revista'
        'AmbosLados')
    end
  end
end
