object FormBZ400: TFormBZ400
  Left = 103
  Top = 101
  Width = 972
  Height = 600
  Caption = 'FormBZ400'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnClose = FormClose
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object CZKEM1: TCZKEM
    Left = 584
    Top = 56
    Width = 33
    Height = 25
    TabOrder = 0
    Visible = False
    ControlData = {000900006903000095020000}
  end
  object Panel1: TPanel
    Left = 0
    Top = 49
    Width = 964
    Height = 524
    Align = alClient
    BevelOuter = bvNone
    TabOrder = 1
    object PageControl1: TPageControl
      Left = 0
      Top = 0
      Width = 964
      Height = 180
      ActivePage = TabSheet1
      Align = alTop
      TabOrder = 0
      object TabSheet1: TTabSheet
        Caption = 'BZ400'
        object Panel2: TPanel
          Left = 0
          Top = 0
          Width = 956
          Height = 151
          Align = alTop
          BevelOuter = bvNone
          TabOrder = 0
          object lblName: TLabel
            Left = 0
            Top = 84
            Width = 28
            Height = 13
            Caption = 'Name'
          end
          object lblAddr: TLabel
            Left = 160
            Top = 111
            Width = 22
            Height = 13
            Caption = 'Addr'
            Visible = False
          end
          object lblTax: TLabel
            Left = 300
            Top = 84
            Width = 18
            Height = 13
            Caption = 'Tax'
          end
          object lblCEI: TLabel
            Left = 8
            Top = 111
            Width = 17
            Height = 13
            Caption = 'CEI'
          end
          object lblCert: TLabel
            Left = 160
            Top = 84
            Width = 19
            Height = 13
            Caption = 'Cert'
          end
          object lblCount: TLabel
            Left = 0
            Top = 138
            Width = 956
            Height = 13
            Align = alBottom
          end
          object Label1: TLabel
            Left = 8
            Top = 12
            Width = 26
            Height = 13
            Caption = 'Index'
          end
          object Label5: TLabel
            Left = 0
            Top = 53
            Width = 99
            Height = 13
            Caption = 'Unlock serial number'
          end
          object Label6: TLabel
            Left = 367
            Top = 53
            Width = 66
            Height = 13
            AutoSize = False
            Caption = 'Param Name'
          end
          object lblParamValue: TLabel
            Left = 575
            Top = 53
            Width = 36
            Height = 13
            AutoSize = False
            Caption = 'Value'
          end
          object btnDownload: TButton
            Left = 232
            Top = 6
            Width = 97
            Height = 25
            Caption = 'Download Att Logs'
            TabOrder = 0
            OnClick = btnDownloadClick
          end
          object btnCancel: TButton
            Left = 336
            Top = 6
            Width = 97
            Height = 25
            Caption = 'Break'
            TabOrder = 1
            OnClick = btnCancelClick
          end
          object edtName: TEdit
            Left = 32
            Top = 80
            Width = 121
            Height = 21
            MaxLength = 150
            TabOrder = 2
            Text = 'ZKSOFTWARE'
          end
          object edtAddr: TEdit
            Left = 192
            Top = 107
            Width = 377
            Height = 21
            MaxLength = 100
            TabOrder = 3
            Text = 'BRAZIL'
          end
          object edtTax: TEdit
            Left = 329
            Top = 80
            Width = 96
            Height = 21
            MaxLength = 14
            TabOrder = 4
            Text = '12345678901234'
          end
          object rbCPF: TRadioButton
            Left = 188
            Top = 82
            Width = 49
            Height = 17
            Caption = 'CNPJ'
            Checked = True
            TabOrder = 5
            TabStop = True
          end
          object rbCNPJ: TRadioButton
            Left = 244
            Top = 82
            Width = 49
            Height = 17
            Caption = 'CPF'
            TabOrder = 6
          end
          object btnSet: TButton
            Left = 441
            Top = 78
            Width = 75
            Height = 25
            Caption = 'SetEmployer'
            TabOrder = 7
            OnClick = btnSetClick
          end
          object edtCEI: TEdit
            Left = 32
            Top = 107
            Width = 121
            Height = 21
            MaxLength = 12
            TabOrder = 8
            Text = '123456789012'
          end
          object btnGetEmployer: TButton
            Left = 576
            Top = 105
            Width = 75
            Height = 25
            Caption = 'GetEmployer'
            TabOrder = 9
            OnClick = btnGetEmployerClick
          end
          object btnOpTimeLog: TButton
            Left = 520
            Top = 78
            Width = 75
            Height = 25
            Caption = 'OpTimeLog'
            TabOrder = 10
            OnClick = btnOpTimeLogClick
          end
          object btnEmployerLog: TButton
            Left = 655
            Top = 105
            Width = 75
            Height = 25
            Caption = 'EmployerLog'
            TabOrder = 11
            OnClick = btnEmployerLogClick
          end
          object btnEmployeeLog: TButton
            Left = 599
            Top = 78
            Width = 113
            Height = 25
            Caption = 'EmployeeLog'
            TabOrder = 12
            OnClick = btnEmployeeLogClick
          end
          object edtPosition: TEdit
            Left = 64
            Top = 8
            Width = 65
            Height = 21
            TabOrder = 13
            Text = '0'
          end
          object btnPosition: TButton
            Left = 144
            Top = 6
            Width = 75
            Height = 25
            Caption = 'SetNSR'
            TabOrder = 14
            OnClick = btnPositionClick
          end
          object btnEnable: TButton
            Left = 536
            Top = 8
            Width = 113
            Height = 25
            Caption = 'EnableDevice'
            TabOrder = 15
            Visible = False
            OnClick = btnEnableClick
          end
          object chkEnable: TCheckBox
            Left = 448
            Top = 12
            Width = 81
            Height = 17
            Caption = 'Working'
            Checked = True
            State = cbChecked
            TabOrder = 16
            Visible = False
          end
          object btnClearAdmin: TButton
            Left = 734
            Top = 105
            Width = 113
            Height = 25
            Caption = 'ClearAdmin'
            TabOrder = 17
            OnClick = btnClearAdminClick
          end
          object btnSetDeviceTime: TButton
            Left = 721
            Top = 79
            Width = 97
            Height = 25
            Caption = 'SetDeviceTime'
            TabOrder = 18
            OnClick = btnSetDeviceTimeClick
          end
          object edtUnlockSerialNumber: TEdit
            Left = 112
            Top = 49
            Width = 121
            Height = 21
            TabOrder = 19
          end
          object btnGetUnlockPwd: TButton
            Left = 248
            Top = 47
            Width = 113
            Height = 25
            Caption = 'GetUnlockPwd'
            TabOrder = 20
            OnClick = btnGetUnlockPwdClick
          end
          object edtParamName: TEdit
            Left = 439
            Top = 49
            Width = 121
            Height = 21
            TabOrder = 21
          end
          object edtParaValue: TEdit
            Left = 623
            Top = 49
            Width = 121
            Height = 21
            TabOrder = 22
          end
          object btnGetSysOptions: TButton
            Left = 752
            Top = 47
            Width = 97
            Height = 25
            Caption = 'GetSysOptions'
            TabOrder = 23
            OnClick = btnGetSysOptionsClick
          end
          object btnSetSysOptions: TButton
            Left = 856
            Top = 47
            Width = 97
            Height = 25
            Caption = 'SetSysOptions'
            TabOrder = 24
            OnClick = btnSetSysOptionsClick
          end
          object btnUnlock: TButton
            Left = 672
            Top = 8
            Width = 89
            Height = 25
            Caption = 'Unlock'
            TabOrder = 25
            OnClick = btnUnlockClick
          end
        end
      end
      object TabSheet3: TTabSheet
        Caption = 'BZ400 User Information'
        ImageIndex = 2
        object lblUserNo: TLabel
          Left = 17
          Top = 18
          Width = 80
          Height = 13
          AutoSize = False
          Caption = 'User NO.'
        end
        object lblUserName: TLabel
          Left = 17
          Top = 40
          Width = 50
          Height = 13
          Caption = 'UserName'
        end
        object lblPIS: TLabel
          Left = 17
          Top = 69
          Width = 80
          Height = 13
          AutoSize = False
          Caption = 'PIS'
        end
        object btnFingerIndex: TLabel
          Left = 17
          Top = 100
          Width = 58
          Height = 13
          Caption = 'Finger Index'
        end
        object lblCPF: TLabel
          Left = 497
          Top = 72
          Width = 46
          Height = 13
          AutoSize = False
          Caption = 'Card No'
          Visible = False
        end
        object edtUserNo: TEdit
          Left = 75
          Top = 14
          Width = 121
          Height = 21
          MaxLength = 12
          TabOrder = 0
        end
        object btnGetUser: TButton
          Left = 228
          Top = 12
          Width = 125
          Height = 25
          Caption = 'SSR_GetAllUserInfoEx'
          TabOrder = 1
          OnClick = btnGetUserClick
        end
        object btnDownloadTemplate: TButton
          Left = 364
          Top = 12
          Width = 133
          Height = 25
          Caption = 'Download Template'
          TabOrder = 2
          OnClick = btnDownloadTemplateClick
        end
        object btnUploadTemplate: TButton
          Left = 504
          Top = 12
          Width = 97
          Height = 25
          Caption = 'UploadTemplate'
          TabOrder = 3
          OnClick = btnUploadTemplateClick
        end
        object edtUserName: TEdit
          Left = 75
          Top = 40
          Width = 518
          Height = 21
          MaxLength = 52
          TabOrder = 4
        end
        object edtPIS: TEdit
          Left = 75
          Top = 65
          Width = 121
          Height = 21
          MaxLength = 12
          TabOrder = 5
        end
        object btnSetUserEx: TButton
          Left = 216
          Top = 63
          Width = 121
          Height = 25
          Caption = 'SSR_SetUserInfoEx'
          TabOrder = 6
          OnClick = btnSetUserExClick
        end
        object btnDelUser: TButton
          Left = 344
          Top = 63
          Width = 145
          Height = 25
          Caption = 'SSR_DeleteEnrollDataExt'
          TabOrder = 7
          OnClick = btnDelUserClick
        end
        object edtIndex: TEdit
          Left = 88
          Top = 96
          Width = 121
          Height = 21
          MaxLength = 1
          TabOrder = 8
          Text = '0'
        end
        object UpDown1: TUpDown
          Left = 209
          Top = 96
          Width = 15
          Height = 21
          Associate = edtIndex
          Max = 9
          TabOrder = 9
        end
        object btnSSR_DelUserTmpExt: TButton
          Left = 232
          Top = 94
          Width = 153
          Height = 25
          Caption = 'SSR_DelUserTmpExt'
          TabOrder = 10
          OnClick = btnDelFingerClick
        end
        object edtCPF: TEdit
          Left = 539
          Top = 68
          Width = 70
          Height = 21
          MaxLength = 14
          TabOrder = 11
          Visible = False
        end
        object edtCount: TEdit
          Left = 616
          Top = 68
          Width = 49
          Height = 21
          TabOrder = 12
          Text = '1'
          Visible = False
        end
        object btnTest: TButton
          Left = 672
          Top = 65
          Width = 75
          Height = 25
          Caption = 'Add More user'
          TabOrder = 13
          Visible = False
          OnClick = btnTestClick
        end
      end
      object TabSheet2: TTabSheet
        Caption = 'Black/White'
        ImageIndex = 1
        TabVisible = False
        object lblBWCardNo: TLabel
          Left = 207
          Top = 108
          Width = 60
          Height = 13
          AutoSize = False
          Caption = 'CardNo'
        end
        object lblBWPassword: TLabel
          Left = 207
          Top = 76
          Width = 60
          Height = 13
          AutoSize = False
          Caption = 'Password'
        end
        object lblBWName: TLabel
          Left = 207
          Top = 44
          Width = 60
          Height = 13
          AutoSize = False
          Caption = 'Name'
        end
        object lblUserID: TLabel
          Left = 207
          Top = 14
          Width = 60
          Height = 13
          AutoSize = False
          Caption = 'UserID'
        end
        object Label4: TLabel
          Left = 424
          Top = 14
          Width = 58
          Height = 13
          Caption = 'Finger Index'
        end
        object btnGetGeneralLogDataStr: TButton
          Left = 16
          Top = 8
          Width = 161
          Height = 25
          Caption = 'GetGeneralLogDataStr'
          TabOrder = 0
          OnClick = btnGetGeneralLogDataStrClick
        end
        object btnGetAllUserInfo: TButton
          Left = 16
          Top = 48
          Width = 161
          Height = 25
          Caption = 'GetAllUserInfo'
          TabOrder = 1
          OnClick = btnGetAllUserInfoClick
        end
        object btnGetUserTmpStr: TButton
          Left = 608
          Top = 8
          Width = 161
          Height = 25
          Caption = 'GetUserTmpStr'
          TabOrder = 2
          OnClick = btnGetUserTmpStrClick
        end
        object btnSetUserInfo: TButton
          Left = 411
          Top = 68
          Width = 161
          Height = 25
          Caption = 'SetUserInfo'
          TabOrder = 3
          OnClick = btnSetUserInfoClick
        end
        object btnDelUserTmp: TButton
          Left = 608
          Top = 40
          Width = 161
          Height = 25
          Caption = 'DelUserTmp'
          TabOrder = 4
          OnClick = btnDelUserTmpClick
        end
        object btnSetUserTmpStr: TButton
          Left = 608
          Top = 72
          Width = 161
          Height = 25
          Caption = 'SetUserTmpStr'
          TabOrder = 5
          OnClick = btnSetUserTmpStrClick
        end
        object btnDeleteEnrollData: TButton
          Left = 411
          Top = 100
          Width = 161
          Height = 25
          Caption = 'DeleteEnrollData'
          TabOrder = 6
          OnClick = btnDeleteEnrollDataClick
        end
        object edtUserID: TEdit
          Left = 280
          Top = 10
          Width = 121
          Height = 21
          TabOrder = 7
        end
        object edtBWName: TEdit
          Left = 280
          Top = 40
          Width = 121
          Height = 21
          TabOrder = 8
        end
        object edtBWPassword: TEdit
          Left = 280
          Top = 72
          Width = 121
          Height = 21
          TabOrder = 9
        end
        object edtBWCardNo: TEdit
          Left = 280
          Top = 104
          Width = 121
          Height = 21
          TabOrder = 10
        end
        object edtBWIndex: TEdit
          Left = 496
          Top = 10
          Width = 81
          Height = 21
          TabOrder = 11
          Text = '0'
        end
        object UpDown2: TUpDown
          Left = 577
          Top = 10
          Width = 15
          Height = 21
          Associate = edtBWIndex
          Max = 9
          TabOrder = 12
        end
        object btnClearList: TButton
          Left = 16
          Top = 88
          Width = 161
          Height = 25
          Caption = 'Clear List'
          TabOrder = 13
          OnClick = btnClearListClick
        end
      end
      object TabSheet4: TTabSheet
        Caption = 'Other'
        ImageIndex = 3
        TabVisible = False
        object Label7: TLabel
          Left = 8
          Top = 29
          Width = 59
          Height = 13
          AutoSize = False
          Caption = 'MRP Type'
        end
        object Label8: TLabel
          Left = 216
          Top = 29
          Width = 33
          Height = 13
          AutoSize = False
          Caption = 'Total'
        end
        object Label9: TLabel
          Left = 4
          Top = 3
          Width = 90
          Height = 13
          AutoSize = False
          Caption = '1- Punching '
        end
        object Label10: TLabel
          Left = 76
          Top = 3
          Width = 90
          Height = 13
          AutoSize = False
          Caption = '32- Employee '
        end
        object Label11: TLabel
          Left = 156
          Top = 3
          Width = 90
          Height = 13
          AutoSize = False
          Caption = '34- Employer'
        end
        object Label12: TLabel
          Left = 236
          Top = 3
          Width = 90
          Height = 13
          AutoSize = False
          Caption = '35-Time log '
        end
        object Label13: TLabel
          Left = 16
          Top = 103
          Width = 60
          Height = 13
          AutoSize = False
          Caption = 'Start'
        end
        object Label14: TLabel
          Left = 123
          Top = 103
          Width = 9
          Height = 13
          Caption = 'M'
        end
        object Label15: TLabel
          Left = 222
          Top = 103
          Width = 8
          Height = 13
          Caption = 'D'
        end
        object Label16: TLabel
          Left = 123
          Top = 129
          Width = 9
          Height = 13
          Caption = 'M'
        end
        object Label17: TLabel
          Left = 222
          Top = 129
          Width = 8
          Height = 13
          Caption = 'D'
        end
        object Label18: TLabel
          Left = 16
          Top = 129
          Width = 19
          Height = 13
          Caption = 'End'
        end
        object Label19: TLabel
          Left = 317
          Top = 103
          Width = 37
          Height = 13
          AutoSize = False
          Caption = 'Time'
        end
        object Label20: TLabel
          Left = 317
          Top = 129
          Width = 37
          Height = 13
          AutoSize = False
          Caption = 'Time'
        end
        object Label21: TLabel
          Left = 64
          Top = 80
          Width = 649
          Height = 13
          AutoSize = False
          Caption = 
            'Format:MMDDHHSS, MM-Month, DD-Day, HH-Hour, SS-Min . For example' +
            ':2010-05-11 00:00 ---->05110000'
        end
        object btnGetMRPTotal: TButton
          Left = 396
          Top = 23
          Width = 97
          Height = 25
          Caption = 'GetMRPTotal'
          TabOrder = 0
          OnClick = btnGetMRPTotalClick
        end
        object edtMRPTotal: TEdit
          Left = 264
          Top = 25
          Width = 121
          Height = 21
          TabOrder = 1
        end
        object ComboBox1: TComboBox
          Left = 64
          Top = 25
          Width = 145
          Height = 21
          ItemHeight = 13
          ItemIndex = 0
          TabOrder = 2
          Text = '1'
          Items.Strings = (
            '1'
            '32'
            '34'
            '35')
        end
        object btnSetDaylight: TButton
          Left = 384
          Top = 97
          Width = 75
          Height = 25
          Caption = 'SetDaylight'
          TabOrder = 3
          OnClick = btnSetDaylightClick
        end
        object edtM1: TEdit
          Left = 64
          Top = 99
          Width = 41
          Height = 21
          TabOrder = 4
          Text = '11'
        end
        object edtM2: TEdit
          Left = 64
          Top = 125
          Width = 41
          Height = 21
          TabOrder = 5
          Text = '4'
        end
        object edtD1: TEdit
          Left = 154
          Top = 99
          Width = 49
          Height = 21
          TabOrder = 6
          Text = '1'
        end
        object edtD2: TEdit
          Left = 154
          Top = 125
          Width = 49
          Height = 21
          TabOrder = 7
          Text = '1'
        end
        object CheckBox1: TCheckBox
          Left = 64
          Top = 58
          Width = 241
          Height = 17
          Caption = 'Enable The Daylight Saving Time'
          Checked = True
          State = cbChecked
          TabOrder = 8
        end
        object btnGetDaylight: TButton
          Left = 384
          Top = 123
          Width = 75
          Height = 25
          Caption = 'GetDaylight'
          TabOrder = 9
          OnClick = btnGetDaylightClick
        end
        object UpDown3: TUpDown
          Left = 105
          Top = 99
          Width = 15
          Height = 21
          Associate = edtM1
          Min = 1
          Max = 12
          Position = 11
          TabOrder = 10
        end
        object UpDown4: TUpDown
          Left = 105
          Top = 125
          Width = 15
          Height = 21
          Associate = edtM2
          Min = 1
          Max = 12
          Position = 4
          TabOrder = 11
        end
        object UpDown5: TUpDown
          Left = 203
          Top = 99
          Width = 15
          Height = 21
          Associate = edtD1
          Min = 1
          Max = 31
          Position = 1
          TabOrder = 12
        end
        object UpDown6: TUpDown
          Left = 203
          Top = 125
          Width = 15
          Height = 21
          Associate = edtD2
          Min = 1
          Max = 31
          Position = 1
          TabOrder = 13
        end
        object edtTime1: TEdit
          Left = 252
          Top = 99
          Width = 31
          Height = 21
          MaxLength = 2
          TabOrder = 14
          Text = '00'
        end
        object edtTime2: TEdit
          Left = 284
          Top = 99
          Width = 31
          Height = 21
          MaxLength = 2
          TabOrder = 15
          Text = '00'
        end
        object edtTime3: TEdit
          Left = 252
          Top = 125
          Width = 31
          Height = 21
          MaxLength = 2
          TabOrder = 16
          Text = '00'
        end
        object edtTime4: TEdit
          Left = 284
          Top = 125
          Width = 31
          Height = 21
          MaxLength = 2
          TabOrder = 17
          Text = '00'
        end
        object btnRefreshOptions: TButton
          Left = 480
          Top = 120
          Width = 129
          Height = 25
          Caption = 'RefreshOptions'
          TabOrder = 18
          OnClick = btnRefreshOptionsClick
        end
      end
      object TabSheet5: TTabSheet
        Caption = 'Iface'
        ImageIndex = 4
        TabVisible = False
        object Label22: TLabel
          Left = 64
          Top = 24
          Width = 38
          Height = 13
          Caption = 'UserNO'
        end
        object btnGetUserFace: TButton
          Left = 272
          Top = 16
          Width = 105
          Height = 25
          Caption = 'GetUserFace'
          TabOrder = 0
          OnClick = btnGetUserFaceClick
        end
        object btnSetUserFace: TButton
          Left = 272
          Top = 48
          Width = 105
          Height = 25
          Caption = 'SetUserFace'
          TabOrder = 1
          OnClick = btnSetUserFaceClick
        end
        object edtUserFace: TEdit
          Left = 128
          Top = 16
          Width = 121
          Height = 21
          TabOrder = 2
        end
        object Button1: TButton
          Left = 272
          Top = 80
          Width = 201
          Height = 25
          Caption = 'SSR_GetGeneralGLog'
          TabOrder = 3
          OnClick = Button1Click
        end
      end
    end
    object Panel4: TPanel
      Left = 0
      Top = 180
      Width = 964
      Height = 344
      Align = alClient
      BevelOuter = bvNone
      TabOrder = 1
      object ListBox1: TListBox
        Left = 0
        Top = 0
        Width = 920
        Height = 344
        Align = alClient
        ItemHeight = 13
        PopupMenu = PopupMenu1
        TabOrder = 0
      end
      object ProgressBar1: TProgressBar
        Left = 920
        Top = 0
        Width = 44
        Height = 344
        Align = alRight
        Orientation = pbVertical
        Smooth = True
        Step = 1
        TabOrder = 1
      end
    end
  end
  object Panel3: TPanel
    Left = 0
    Top = 0
    Width = 964
    Height = 49
    Align = alTop
    BevelOuter = bvNone
    TabOrder = 2
    object Label2: TLabel
      Left = 11
      Top = 15
      Width = 25
      Height = 13
      AutoSize = False
      Caption = 'IP'
    end
    object Label3: TLabel
      Left = 195
      Top = 15
      Width = 43
      Height = 13
      Caption = 'com pwd'
    end
    object edtIP: TEdit
      Left = 51
      Top = 11
      Width = 121
      Height = 21
      TabOrder = 0
      Text = '192.168.2.201'
    end
    object edtPwd: TEdit
      Left = 251
      Top = 11
      Width = 121
      Height = 21
      MaxLength = 6
      TabOrder = 1
      Text = '0'
    end
    object btnConnect: TButton
      Left = 395
      Top = 9
      Width = 121
      Height = 25
      Caption = 'Connect'
      TabOrder = 2
      OnClick = btnConnectClick
    end
    object btnTesting: TButton
      Left = 544
      Top = 8
      Width = 161
      Height = 25
      Caption = 'Testing'
      TabOrder = 3
      Visible = False
      OnClick = btnTestingClick
    end
  end
  object PopupMenu1: TPopupMenu
    Left = 368
    Top = 384
    object LoadFingerprint1: TMenuItem
      Caption = 'Load Fingerprint or Face Template'
      OnClick = LoadFingerprint1Click
    end
    object SaveFingerprint1: TMenuItem
      Caption = 'Save Fingerprint or Face Template'
      OnClick = SaveFingerprint1Click
    end
  end
  object OpenDialog1: TOpenDialog
    Left = 416
    Top = 384
  end
  object SaveDialog1: TSaveDialog
    DefaultExt = 'dat'
    FileName = 'fingerprint'
    Left = 448
    Top = 384
  end
end
