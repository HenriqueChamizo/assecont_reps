object frmRepTrilobit: TfrmRepTrilobit
  Left = 0
  Top = 0
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = 'Trilobit REP - Exemplo de Uso'
  ClientHeight = 442
  ClientWidth = 491
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object pgcTab: TPageControl
    Left = 1
    Top = 55
    Width = 489
    Height = 386
    ActivePage = tabPes
    TabOrder = 0
    object tabEmp: TTabSheet
      Caption = 'Empregador'
      object grbCadastrarEmpregador: TGroupBox
        Left = 0
        Top = 3
        Width = 478
        Height = 352
        TabOrder = 0
        object lblTipoDoc: TLabel
          Left = 111
          Top = 14
          Width = 48
          Height = 13
          Alignment = taRightJustify
          Caption = 'Tipo Doc :'
        end
        object lblDocumento: TLabel
          Left = 98
          Top = 41
          Width = 61
          Height = 13
          Alignment = taRightJustify
          Caption = 'Documento :'
        end
        object lblCEI: TLabel
          Left = 135
          Top = 68
          Width = 24
          Height = 13
          Alignment = taRightJustify
          Caption = 'CEI :'
        end
        object lblRazaoSocial: TLabel
          Left = 92
          Top = 95
          Width = 67
          Height = 13
          Alignment = taRightJustify
          Caption = 'Raz'#227'o Social :'
        end
        object lblLocal: TLabel
          Left = 128
          Top = 122
          Width = 31
          Height = 13
          Alignment = taRightJustify
          Caption = 'Local :'
        end
        object cboTipoDoc: TComboBox
          Left = 165
          Top = 11
          Width = 145
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          Items.Strings = (
            'CNPJ'
            'CPF')
        end
        object txtDocumento: TEdit
          Left = 165
          Top = 38
          Width = 145
          Height = 21
          MaxLength = 18
          TabOrder = 1
          Text = '12345678901234'
        end
        object txtCEI: TEdit
          Left = 165
          Top = 65
          Width = 145
          Height = 21
          MaxLength = 15
          TabOrder = 2
          Text = '0'
        end
        object txtRazaoSocial: TEdit
          Left = 165
          Top = 92
          Width = 209
          Height = 21
          MaxLength = 150
          TabOrder = 3
          Text = 'TRILOBIT'
        end
        object txtLocal: TEdit
          Left = 165
          Top = 119
          Width = 209
          Height = 21
          MaxLength = 100
          TabOrder = 4
          Text = 'RUA ALVARENGA, 1377'
        end
        object cmdCadastrarEmpregador: TButton
          Left = 146
          Top = 146
          Width = 177
          Height = 25
          Caption = 'Cadastrar'
          TabOrder = 5
          OnClick = cmdCadastrarEmpregadorClick
        end
      end
    end
    object tabPes: TTabSheet
      Caption = 'Empregados'
      ImageIndex = 1
      object grbCadastrarEmpregado: TGroupBox
        Left = 0
        Top = 3
        Width = 477
        Height = 129
        TabOrder = 0
        object lblPIS: TLabel
          Left = 174
          Top = 6
          Width = 23
          Height = 13
          Alignment = taRightJustify
          Caption = 'PIS :'
        end
        object lblNome: TLabel
          Left = 163
          Top = 33
          Width = 34
          Height = 13
          Alignment = taRightJustify
          Caption = 'Nome :'
        end
        object lblCracha: TLabel
          Left = 156
          Top = 60
          Width = 41
          Height = 13
          Alignment = taRightJustify
          Caption = 'Crach'#225' :'
        end
        object lblPossuiBio: TLabel
          Left = 113
          Top = 82
          Width = 84
          Height = 13
          Alignment = taRightJustify
          Caption = 'Possui Biometria :'
        end
        object txtPIS: TEdit
          Left = 203
          Top = 3
          Width = 121
          Height = 21
          MaxLength = 12
          TabOrder = 0
          Text = '123456789012'
        end
        object txtNome: TEdit
          Left = 203
          Top = 30
          Width = 121
          Height = 21
          MaxLength = 52
          TabOrder = 1
          Text = 'Adonis'
        end
        object txtCracha: TEdit
          Left = 203
          Top = 57
          Width = 121
          Height = 21
          MaxLength = 20
          TabOrder = 2
          Text = '123'
        end
        object chkPossuiBio: TCheckBox
          Left = 203
          Top = 81
          Width = 23
          Height = 17
          TabOrder = 3
        end
        object cmdExcluirEmpregado: TButton
          Left = 76
          Top = 99
          Width = 137
          Height = 25
          Caption = 'Excluir'
          TabOrder = 4
          OnClick = cmdExcluirEmpregadoClick
        end
        object cmdCadastrarEmpregado: TButton
          Left = 232
          Top = 99
          Width = 137
          Height = 25
          Caption = 'Cadastrar'
          TabOrder = 5
          OnClick = cmdCadastrarEmpregadoClick
        end
      end
      object grbLerEmpregadosTXT: TGroupBox
        Left = 0
        Top = 137
        Width = 478
        Height = 88
        TabOrder = 1
        object lblDestinoEmp: TLabel
          Left = 107
          Top = 11
          Width = 43
          Height = 13
          Alignment = taRightJustify
          Caption = 'Destino :'
        end
        object lblAddCabecalho: TLabel
          Left = 46
          Top = 36
          Width = 104
          Height = 13
          Alignment = taRightJustify
          Caption = 'Adicionar Cabe'#231'alho :'
        end
        object txtArquivoEmp: TEdit
          Left = 156
          Top = 8
          Width = 269
          Height = 21
          TabOrder = 0
          Text = 'C:\Empregados.txt'
        end
        object chkAddCabecalho: TCheckBox
          Left = 156
          Top = 35
          Width = 21
          Height = 17
          TabOrder = 1
        end
        object cmdLerEmpregadosTXT: TButton
          Left = 164
          Top = 54
          Width = 173
          Height = 25
          Caption = 'Ler Empregados (TXT)'
          TabOrder = 2
          OnClick = cmdLerEmpregadosTXTClick
        end
      end
      object grbLerEmpregadosDT: TGroupBox
        Left = 0
        Top = 230
        Width = 478
        Height = 125
        TabOrder = 2
        object lstEmpregados: TListBox
          Left = 5
          Top = 7
          Width = 470
          Height = 81
          ItemHeight = 13
          TabOrder = 0
        end
        object cmdLerEmpregadosDT: TButton
          Left = 164
          Top = 94
          Width = 173
          Height = 25
          Caption = 'Ler Empregados (Lista)'
          TabOrder = 1
          OnClick = cmdLerEmpregadosDTClick
        end
      end
    end
    object tabCfg: TTabSheet
      Caption = 'Configura'#231#245'es'
      ImageIndex = 2
      object grbParamSet: TGroupBox
        Left = 1
        Top = 1
        Width = 477
        Height = 175
        TabOrder = 0
        object lblParamSet: TLabel
          Left = 65
          Top = 48
          Width = 57
          Height = 13
          Alignment = taRightJustify
          Caption = 'Par'#226'metro :'
        end
        object lblValorSet: TLabel
          Left = 91
          Top = 75
          Width = 31
          Height = 13
          Alignment = taRightJustify
          Caption = 'Valor :'
        end
        object cboParamSet: TComboBox
          Left = 128
          Top = 45
          Width = 257
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object txtParamSet: TEdit
          Left = 128
          Top = 72
          Width = 257
          Height = 21
          TabOrder = 1
        end
        object cmdParamSet: TButton
          Left = 139
          Top = 99
          Width = 190
          Height = 25
          Caption = 'Enviar'
          TabOrder = 2
          OnClick = cmdParamSetClick
        end
      end
      object grbParamGet: TGroupBox
        Left = 1
        Top = 181
        Width = 477
        Height = 175
        TabOrder = 1
        object lblParamGet: TLabel
          Left = 65
          Top = 48
          Width = 57
          Height = 13
          Alignment = taRightJustify
          Caption = 'Par'#226'metro :'
        end
        object lblValorGet: TLabel
          Left = 91
          Top = 75
          Width = 31
          Height = 13
          Alignment = taRightJustify
          Caption = 'Valor :'
        end
        object cboParamGet: TComboBox
          Left = 128
          Top = 45
          Width = 257
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object txtParamGet: TEdit
          Left = 128
          Top = 72
          Width = 257
          Height = 21
          TabOrder = 1
        end
        object cmdParamGet: TButton
          Left = 139
          Top = 99
          Width = 190
          Height = 25
          Caption = 'Ler'
          TabOrder = 2
          OnClick = cmdParamGetClick
        end
      end
    end
    object tabAFD: TTabSheet
      Caption = 'AFD'
      ImageIndex = 3
      object grbLerAFD_TXT: TGroupBox
        Left = 0
        Top = 2
        Width = 478
        Height = 175
        TabOrder = 0
        object lblDataInicial: TLabel
          Left = 98
          Top = 35
          Width = 60
          Height = 13
          Alignment = taRightJustify
          Caption = 'Data Inicial :'
        end
        object lblDataFinal: TLabel
          Left = 103
          Top = 62
          Width = 55
          Height = 13
          Alignment = taRightJustify
          Caption = 'Data Final :'
        end
        object lblArquivoAFD: TLabel
          Left = 114
          Top = 89
          Width = 44
          Height = 13
          Alignment = taRightJustify
          Caption = 'Arquivo :'
        end
        object txtDataInicial: TEdit
          Left = 164
          Top = 32
          Width = 121
          Height = 21
          TabOrder = 0
        end
        object txtDataFinal: TEdit
          Left = 164
          Top = 59
          Width = 121
          Height = 21
          TabOrder = 1
        end
        object txtArquivoAFD: TEdit
          Left = 164
          Top = 86
          Width = 230
          Height = 21
          TabOrder = 2
          Text = 'C:\AFD.txt'
        end
        object cmdLerAFD_TXT: TButton
          Left = 154
          Top = 121
          Width = 167
          Height = 25
          Caption = 'Ler AFD (TXT)'
          TabOrder = 3
          OnClick = cmdLerAFD_TXTClick
        end
      end
      object grbLerAFD_DT: TGroupBox
        Left = 0
        Top = 181
        Width = 478
        Height = 175
        TabOrder = 1
        object lblNSR: TLabel
          Left = 183
          Top = 123
          Width = 27
          Height = 13
          Alignment = taRightJustify
          Caption = 'NSR :'
        end
        object lstAFD: TListBox
          Left = 5
          Top = 7
          Width = 470
          Height = 107
          ItemHeight = 13
          TabOrder = 0
        end
        object cmdLerAFD_DT: TButton
          Left = 154
          Top = 147
          Width = 167
          Height = 25
          Caption = 'Ler AFD (Lista)'
          TabOrder = 1
          OnClick = cmdLerAFD_DTClick
        end
        object txtNSR: TEdit
          Left = 216
          Top = 120
          Width = 85
          Height = 21
          TabOrder = 2
          Text = '0'
        end
      end
    end
  end
  object gbrIP: TGroupBox
    Left = 5
    Top = 4
    Width = 478
    Height = 45
    TabOrder = 1
    object lblIP: TLabel
      Left = 13
      Top = 16
      Width = 17
      Height = 13
      Alignment = taRightJustify
      Caption = 'IP :'
    end
    object lblPorta: TLabel
      Left = 165
      Top = 16
      Width = 33
      Height = 13
      Alignment = taRightJustify
      Caption = 'Porta :'
    end
    object lblSenha: TLabel
      Left = 324
      Top = 16
      Width = 37
      Height = 13
      Alignment = taRightJustify
      Caption = 'Senha :'
    end
    object txtIP: TEdit
      Left = 36
      Top = 13
      Width = 106
      Height = 21
      TabOrder = 0
      Text = '127.0.0.1'
    end
    object txtPorta: TEdit
      Left = 204
      Top = 13
      Width = 90
      Height = 21
      TabOrder = 1
      Text = '19001'
    end
    object txtSenha: TEdit
      Left = 367
      Top = 13
      Width = 93
      Height = 21
      TabOrder = 2
      Text = '1'
    end
  end
end
