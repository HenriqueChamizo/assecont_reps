object frmMain: TfrmMain
  Left = 441
  Top = 279
  BorderIcons = [biSystemMenu]
  BorderStyle = bsSingle
  Caption = 'Acionamento'
  ClientHeight = 283
  ClientWidth = 362
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object stbMain: TStatusBar
    Left = 0
    Top = 264
    Width = 362
    Height = 19
    Panels = <
      item
        Style = psOwnerDraw
        Width = 90
      end>
    OnDrawPanel = stbMainDrawPanel
  end
  object PageControl: TPageControl
    Left = 0
    Top = 0
    Width = 362
    Height = 264
    ActivePage = TabSheet1
    Align = alClient
    TabOrder = 1
    object TabSheet1: TTabSheet
      Caption = 'Serial'
      OnShow = TabSheet1Show
      object Panel: TPanel
        Left = 0
        Top = 0
        Width = 354
        Height = 236
        Align = alClient
        TabOrder = 0
        object Label4: TLabel
          Left = 8
          Top = 16
          Width = 30
          Height = 13
          Caption = 'Serial:'
        end
        object Label7: TLabel
          Left = 208
          Top = 16
          Width = 59
          Height = 13
          Caption = 'Num Rel'#243'gio'
        end
        object Label8: TLabel
          Left = 208
          Top = 48
          Width = 55
          Height = 13
          Caption = 'Velocidade:'
        end
        object lblVelocidade: TLabel
          Left = 274
          Top = 41
          Width = 71
          Height = 21
          AutoSize = False
          Caption = 'cv9600'
          Color = clBtnHighlight
          Font.Charset = DEFAULT_CHARSET
          Font.Color = clWindowText
          Font.Height = -11
          Font.Name = 'Tahoma'
          Font.Style = [fsBold, fsItalic]
          ParentColor = False
          ParentFont = False
          Transparent = False
          Layout = tlBottom
        end
        object GroupBox: TGroupBox
          Left = 8
          Top = 40
          Width = 193
          Height = 193
          Caption = 'Detalhes'
          TabOrder = 0
          object Label5: TLabel
            Left = 8
            Top = 37
            Width = 35
            Height = 13
            Caption = 'Hor'#225'rio'
          end
          object Label6: TLabel
            Left = 123
            Top = 36
            Width = 47
            Height = 13
            Caption = 'Segundos'
          end
          object dtkTimeSerial: TDateTimePicker
            Left = 8
            Top = 56
            Width = 65
            Height = 21
            Date = 39463.363578194440000000
            Format = 'HH:mm'
            Time = 39463.363578194440000000
            Kind = dtkTime
            TabOrder = 0
          end
          object speTempoSerial: TSpinEdit
            Left = 123
            Top = 55
            Width = 54
            Height = 22
            MaxValue = 255
            MinValue = 1
            TabOrder = 1
            Value = 1
          end
          object chklSemanaSerial: TCheckListBox
            Left = 8
            Top = 96
            Width = 169
            Height = 65
            Columns = 2
            ItemHeight = 13
            Items.Strings = (
              'Domingo'
              'Segunda'
              'Ter'#231'a'
              'Quarta'
              'Quinta'
              'Sexta'
              'S'#225'bado'
              'Feriados')
            TabOrder = 2
          end
          object btnEnviarSerial: TButton
            Left = 102
            Top = 167
            Width = 75
            Height = 25
            Caption = 'Enviar'
            Enabled = False
            TabOrder = 3
            OnClick = btnEnviarClick
          end
          object btnIncluirSerial: TButton
            Left = 9
            Top = 167
            Width = 75
            Height = 25
            Caption = 'Incluir'
            Enabled = False
            TabOrder = 4
            OnClick = Incluir
          end
        end
        object edtSerial: TEdit
          Left = 40
          Top = 13
          Width = 73
          Height = 21
          TabOrder = 1
          Text = 'COM1'
        end
        object btnOkSerial: TButton
          Left = 123
          Top = 9
          Width = 75
          Height = 25
          Caption = 'Ok'
          TabOrder = 2
          OnClick = btnOkClick
        end
        object edtNumRel: TEdit
          Left = 272
          Top = 12
          Width = 73
          Height = 21
          TabOrder = 3
          Text = '1'
        end
      end
    end
    object TabSheet2: TTabSheet
      Caption = 'TcpIp'
      ImageIndex = 1
      OnShow = TabSheet2Show
      object Panel1: TPanel
        Left = 0
        Top = 0
        Width = 354
        Height = 236
        Align = alClient
        TabOrder = 0
        object Label3: TLabel
          Left = 8
          Top = 16
          Width = 14
          Height = 13
          Caption = 'Ip:'
        end
        object GroupBox1: TGroupBox
          Left = 8
          Top = 40
          Width = 193
          Height = 193
          Caption = 'Detalhes'
          TabOrder = 0
          object Label1: TLabel
            Left = 8
            Top = 37
            Width = 35
            Height = 13
            Caption = 'Hor'#225'rio'
          end
          object Label2: TLabel
            Left = 123
            Top = 36
            Width = 47
            Height = 13
            Caption = 'Segundos'
          end
          object dtkTime: TDateTimePicker
            Left = 8
            Top = 56
            Width = 65
            Height = 21
            Date = 39463.363578194440000000
            Format = 'HH:mm'
            Time = 39463.363578194440000000
            Kind = dtkTime
            TabOrder = 0
          end
          object speTempo: TSpinEdit
            Left = 123
            Top = 55
            Width = 54
            Height = 22
            MaxValue = 255
            MinValue = 1
            TabOrder = 1
            Value = 1
          end
          object chklSemana: TCheckListBox
            Left = 8
            Top = 96
            Width = 169
            Height = 65
            Columns = 2
            ItemHeight = 13
            Items.Strings = (
              'Domingo'
              'Segunda'
              'Ter'#231'a'
              'Quarta'
              'Quinta'
              'Sexta'
              'S'#225'bado'
              'Feriados')
            TabOrder = 2
          end
          object btnEnviar: TButton
            Left = 102
            Top = 167
            Width = 75
            Height = 25
            Caption = 'Enviar'
            Enabled = False
            TabOrder = 3
            OnClick = btnEnviarClick
          end
          object btnIncluir: TButton
            Left = 8
            Top = 167
            Width = 75
            Height = 25
            Caption = 'Incluir'
            Enabled = False
            TabOrder = 4
            OnClick = Incluir
          end
        end
        object edtIp: TEdit
          Left = 28
          Top = 13
          Width = 85
          Height = 21
          TabOrder = 1
          Text = '192.168.0.123'
        end
        object btnOk: TButton
          Left = 125
          Top = 9
          Width = 75
          Height = 25
          Caption = 'Ok'
          TabOrder = 2
          OnClick = btnOkClick
        end
      end
    end
  end
  object ProgressBar1: TProgressBar
    Left = 168
    Top = 144
    Width = 193
    Height = 17
    TabOrder = 2
    Visible = False
  end
  object Timer1: TTimer
    Enabled = False
    Interval = 500
    OnTimer = Timer1Timer
    Left = 120
    Top = 72
  end
end
