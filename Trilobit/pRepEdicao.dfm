inherited frmRepEdicao: TfrmRepEdicao
  Caption = 'REP'
  ClientHeight = 348
  ClientWidth = 369
  ExplicitWidth = 375
  ExplicitHeight = 376
  PixelsPerInch = 96
  TextHeight = 13
  object lblIP: TLabel [0]
    Left = 13
    Top = 148
    Width = 10
    Height = 13
    Alignment = taRightJustify
    Caption = 'IP'
  end
  object lblPorta: TLabel [1]
    Left = 126
    Top = 148
    Width = 26
    Height = 13
    Alignment = taRightJustify
    Caption = 'Porta'
  end
  object lblSenha: TLabel [2]
    Left = 190
    Top = 148
    Width = 30
    Height = 13
    Alignment = taRightJustify
    Caption = 'Senha'
  end
  object Label1: TLabel [3]
    Left = 13
    Top = 94
    Width = 46
    Height = 13
    Caption = 'Descri'#231#227'o'
  end
  object Label2: TLabel [4]
    Left = 289
    Top = 94
    Width = 37
    Height = 13
    Caption = 'N'#250'mero'
  end
  inherited WButtonsDialog1: TWButtonsDialog
    Top = 309
    Width = 369
    TabOrder = 9
    OnOk = WButtonsDialog1Ok
    ExplicitTop = 309
    ExplicitWidth = 369
  end
  inherited Panel1: TPanel
    Width = 369
    TabOrder = 10
    ExplicitWidth = 369
    inherited lbTituloVista: TWLabel
      Width = 349
      Caption = 'Cadastrar REP'
      ExplicitWidth = 100
    end
  end
  object edIp: TWEdit [7]
    Left = 13
    Top = 164
    Width = 106
    Height = 21
    Enabled = True
    TabOrder = 2
    Text = '127.0.0.1'
    Characters = chALL
    FieldNotNull = False
    Group = 0
    ColorDisabled = clBtnFace
    ColorEnabled = clWindow
    TagField = 'TRM_IP'
    AcceptTab = False
    TextEncrypted = '127.0.0.1'
    EncryptFactor = 0
  end
  object edPorta: TWEdit [8]
    Left = 124
    Top = 164
    Width = 62
    Height = 21
    Enabled = True
    TabOrder = 3
    Text = '19001'
    Characters = chALL
    FieldNotNull = False
    Group = 0
    ColorDisabled = clBtnFace
    ColorEnabled = clWindow
    TagField = 'TRM_PORTA'
    AcceptTab = False
    TextEncrypted = '19001'
    EncryptFactor = 0
  end
  object edSenha: TWEdit [9]
    Left = 191
    Top = 164
    Width = 93
    Height = 21
    Enabled = True
    TabOrder = 4
    Characters = chALL
    FieldNotNull = False
    Group = 0
    ColorDisabled = clBtnFace
    ColorEnabled = clWindow
    TagField = 'TRM_SENHA'
    AcceptTab = False
    EncryptFactor = 5
  end
  object Edit1: TWEdit [10]
    Left = 13
    Top = 110
    Width = 271
    Height = 21
    Enabled = True
    TabOrder = 0
    Characters = chALL
    FieldNotNull = False
    Group = 0
    ColorDisabled = clBtnFace
    ColorEnabled = clWindow
    TagField = 'TRM_DESCRICAO'
    AcceptTab = False
    EncryptFactor = 0
  end
  object WCheckBox1: TWCheckBox [11]
    Left = 16
    Top = 234
    Width = 97
    Height = 17
    Caption = 'Liberar Teclado'
    TabOrder = 6
    TagField = 'TRM_TECLADO'
    Group = 0
    ValueChecked = 'True'
    ValueUnchecked = 'False'
    Value = 'False'
  end
  object WCheckBox2: TWCheckBox [12]
    Left = 16
    Top = 252
    Width = 97
    Height = 17
    Caption = 'Usar Crach'#225's'
    TabOrder = 7
    TagField = 'TRM_CRACHA'
    Group = 0
    ValueChecked = 'True'
    ValueUnchecked = 'False'
    Value = 'False'
  end
  object WCheckBox3: TWCheckBox [13]
    Left = 16
    Top = 270
    Width = 97
    Height = 17
    Caption = 'Usar Biometria'
    TabOrder = 8
    TagField = 'TRM_BIOMETRIA'
    Group = 0
    ValueChecked = 'True'
    ValueUnchecked = 'False'
    Value = 'False'
  end
  object edNumero: TWSpinEdit [14]
    Left = 289
    Top = 110
    Width = 66
    Height = 21
    Group = 0
    Position = 1
    Max = 100
    Min = 1
    TabOrder = 1
    TagField = 'TRM_NUMERO'
    ZeroIsInfinite = False
  end
  object WCheckBox4: TWCheckBox [15]
    Left = 16
    Top = 216
    Width = 123
    Height = 17
    Caption = 'Terminal Habilitado'
    Checked = True
    State = cbChecked
    TabOrder = 5
    TagField = 'TRM_HABILITADO'
    Group = 0
    ValueChecked = 'True'
    ValueUnchecked = 'False'
    Value = 'True'
  end
  object WFieldsValues1: TWFieldsValues
    Fields.Strings = (
      'TRM_GRUPO')
    Group = 0
    Left = 192
    Top = 18
  end
  object WFormTable1: TWFormTable
    Connection = DM.Conn
    Indice = 0
    TagField = 'TRM_IND'
    Group = 0
    IgnoreNulls = False
    SaveWLabels = False
    SaveOnlyChanged = False
    ResetFormAfterSave = True
    Table = 'Terminais'
    Left = 240
    Top = 224
  end
end
