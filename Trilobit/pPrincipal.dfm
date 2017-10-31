object frmRepTrilobitPrincipal: TfrmRepTrilobitPrincipal
  Left = 0
  Top = 0
  BorderIcons = [biSystemMenu]
  Caption = 'Comunica'#231#227'o'
  ClientHeight = 528
  ClientWidth = 904
  Color = 16107953
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Trebuchet MS'
  Font.Style = []
  OldCreateOrder = False
  Position = poDesktopCenter
  OnCreate = FormCreate
  OnShow = FormShow
  PixelsPerInch = 96
  TextHeight = 16
  object sgFuncionarios: TWStringGrid
    AlignWithMargins = True
    Left = 8
    Top = 34
    Width = 270
    Height = 486
    Margins.Left = 8
    Margins.Top = 8
    Margins.Right = 8
    Margins.Bottom = 8
    Align = alLeft
    BevelInner = bvLowered
    BevelKind = bkFlat
    BorderStyle = bsNone
    Color = clWhite
    ColCount = 7
    DefaultRowHeight = 50
    DefaultDrawing = False
    FixedCols = 0
    RowCount = 5
    FixedRows = 0
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'Tahoma'
    Font.Style = [fsBold]
    Options = [goRangeSelect, goDrawFocusSelected, goThumbTracking]
    ParentFont = False
    TabOrder = 0
    AlignTitles = True
    AdjustLastCol = True
    ColIndice = -1
    ColsTitles.Strings = (
      'Nome'
      'Cargo'
      'Func'
      'DeptoNome'
      'Pis'
      'TerminalDescricao*'
      'TerminalInd*')
    ColsWidth.Strings = (
      '100%'
      '0'
      '0'
      '0'
      '0'
      '0'
      '0')
    ColsAlignment.Strings = (
      'L')
    ColOrder = 0
    ColRowColor = -1
    ColGroup = -1
    ColSubGroup = -1
    ColGroupFlagStart = -1
    ColGroupDisplay = -1
    Color2 = clWhite
    ColorTitleGradient1 = 16767935
    ColorTitleGradient2 = 16708835
    ColorLineGroup = clWhite
    ColorLineTitle = clSilver
    ColorLineCol = 13290186
    ColorLineRow = 13290186
    ColorSelection = 16043961
    ColorFontSelection = clBlack
    DrawArrowSelection = False
    FontGroup.Charset = DEFAULT_CHARSET
    FontGroup.Color = clNavy
    FontGroup.Height = -16
    FontGroup.Name = 'Tahoma'
    FontGroup.Style = []
    FontTitle.Charset = DEFAULT_CHARSET
    FontTitle.Color = clWindowText
    FontTitle.Height = -11
    FontTitle.Name = 'Tahoma'
    FontTitle.Style = []
    RowTitleHeight = 50
    RowGroupHeight = 25
    UseNewDrawCell = True
    TitlePosTop = -1
    DetailPosTop = 3
    PaintNewTitle = True
    PrinterOrientation = poPortrait
    PrinterColDesignLine = -1
    RoundSelection = True
    SaveColumns = True
    OnDrawCellFinalize = sgFuncionariosDrawCellFinalize
    DrawHorizontalLine = False
    DrawHorizontalLineGroup = False
    DrawVerticalLine = False
    ColWidths = (
      268
      0
      0
      0
      0
      0
      0)
    RowHeights = (
      50
      50
      50
      50
      50)
  end
  object Panel1: TScrollBox
    AlignWithMargins = True
    Left = 286
    Top = 34
    Width = 610
    Height = 486
    Margins.Left = 0
    Margins.Top = 8
    Margins.Right = 8
    Margins.Bottom = 8
    VertScrollBar.Tracking = True
    Align = alClient
    BevelInner = bvLowered
    BevelKind = bkFlat
    BorderStyle = bsNone
    Color = clWhite
    ParentColor = False
    TabOrder = 5
    object Label1: TLabel
      Left = 10
      Top = 30
      Width = 61
      Height = 16
      Caption = 'Raz'#227'o Social'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clGray
      Font.Height = -11
      Font.Name = 'Trebuchet MS'
      Font.Style = []
      ParentFont = False
    end
    object Label2: TLabel
      Left = 10
      Top = 71
      Width = 26
      Height = 16
      Caption = 'Local'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clGray
      Font.Height = -11
      Font.Name = 'Trebuchet MS'
      Font.Style = []
      ParentFont = False
    end
    object Label3: TLabel
      Left = 10
      Top = 107
      Width = 25
      Height = 16
      Caption = 'CNPJ'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clGray
      Font.Height = -11
      Font.Name = 'Trebuchet MS'
      Font.Style = []
      ParentFont = False
    end
    object lbRazaoSocial: TWLabel
      Left = 10
      Top = 45
      Width = 264
      Height = 16
      Caption = 'Xxxxx xxxxx xxxxx xxxxx xxxxx xxxxx xxxxx'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Trebuchet MS'
      Font.Style = [fsBold]
      ParentFont = False
      FieldNotNull = False
      FontOver.Charset = DEFAULT_CHARSET
      FontOver.Color = clBlue
      FontOver.Height = -11
      FontOver.Name = 'Tahoma'
      FontOver.Style = [fsUnderline]
      Group = 1
      ProcessMouseOver = False
      LabelFormat = lfNone
      TagField = 'GRU_DESCRICAO'
      ColorEnabled = clWindowText
      ColorDisabled = clGrayText
      DrawBorder = False
      DrawLineBottom = False
      DrawLineRight = False
      ColorBorder = clSilver
    end
    object lbLocal: TWLabel
      Left = 10
      Top = 85
      Width = 70
      Height = 16
      Caption = 'Rua Abc, 100'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Trebuchet MS'
      Font.Style = [fsBold]
      ParentFont = False
      FieldNotNull = False
      FontOver.Charset = DEFAULT_CHARSET
      FontOver.Color = clBlue
      FontOver.Height = -11
      FontOver.Name = 'Tahoma'
      FontOver.Style = [fsUnderline]
      Group = 1
      ProcessMouseOver = False
      LabelFormat = lfNone
      TagField = 'GRU_ENDERECO'
      ColorEnabled = clWindowText
      ColorDisabled = clGrayText
      DrawBorder = False
      DrawLineBottom = False
      DrawLineRight = False
      ColorBorder = clSilver
    end
    object lbCnpj: TWLabel
      Left = 10
      Top = 125
      Width = 100
      Height = 16
      Caption = '22.555.222/0001-22'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Trebuchet MS'
      Font.Style = [fsBold]
      ParentFont = False
      FieldNotNull = False
      FontOver.Charset = DEFAULT_CHARSET
      FontOver.Color = clBlue
      FontOver.Height = -11
      FontOver.Name = 'Tahoma'
      FontOver.Style = [fsUnderline]
      Group = 1
      ProcessMouseOver = False
      LabelFormat = lfNone
      TagField = 'GRU_CNPJ'
      ColorEnabled = clWindowText
      ColorDisabled = clGrayText
      DrawBorder = False
      DrawLineBottom = False
      DrawLineRight = False
      ColorBorder = clSilver
    end
    object Label5: TLabel
      Left = 10
      Top = 8
      Width = 86
      Height = 22
      Caption = 'Empregador'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clNavy
      Font.Height = -16
      Font.Name = 'Trebuchet MS'
      Font.Style = []
      ParentFont = False
    end
    object Label6: TLabel
      Left = 10
      Top = 170
      Width = 69
      Height = 22
      Caption = 'Terminais'
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clNavy
      Font.Height = -16
      Font.Name = 'Trebuchet MS'
      Font.Style = []
      ParentFont = False
    end
    object sgTerminais: TWStringGrid
      AlignWithMargins = True
      Left = 10
      Top = 194
      Width = 342
      Height = 220
      Margins.Left = 6
      Margins.Top = 86
      Margins.Right = 6
      Margins.Bottom = 6
      BevelInner = bvNone
      BorderStyle = bsNone
      Color = clWhite
      ColCount = 6
      DefaultRowHeight = 18
      DefaultDrawing = False
      FixedCols = 0
      RowCount = 5
      Options = [goDrawFocusSelected, goThumbTracking]
      TabOrder = 0
      OnDblClick = sgTerminaisDblClick
      AlignTitles = True
      AdjustLastCol = True
      ColIndice = -1
      ColsTitles.Strings = (
        'N'#176
        'Descri'#231#227'o'
        'Ip'
        'Porta'
        'Ind*'
        'Senha*')
      ColsWidth.Strings = (
        '50'
        '100%'
        '130'
        '60'
        '0'
        '0')
      ColOrder = 0
      ColRowColor = -1
      ColGroup = -1
      ColSubGroup = -1
      ColGroupFlagStart = -1
      ColGroupDisplay = 0
      Color2 = clWhite
      ColorTitleGradient1 = 16043961
      ColorTitleGradient2 = 16043961
      ColorLineGroup = 13290186
      ColorLineTitle = clWhite
      ColorLineCol = 13290186
      ColorLineRow = 13290186
      ColorSelection = 5884409
      ColorFontSelection = clBlack
      DrawArrowSelection = False
      FontGroup.Charset = DEFAULT_CHARSET
      FontGroup.Color = clWindowText
      FontGroup.Height = -11
      FontGroup.Name = 'Tahoma'
      FontGroup.Style = []
      FontTitle.Charset = DEFAULT_CHARSET
      FontTitle.Color = clWindowText
      FontTitle.Height = -11
      FontTitle.Name = 'Tahoma'
      FontTitle.Style = []
      RowTitleHeight = 22
      RowGroupHeight = 20
      UseNewDrawCell = True
      TitlePosTop = -1
      DetailPosTop = -1
      PaintNewTitle = True
      PrinterOrientation = poPortrait
      PrinterColDesignLine = -1
      RoundSelection = False
      SaveColumns = True
      DrawHorizontalLine = False
      DrawHorizontalLineGroup = False
      DrawVerticalLine = False
      ColWidths = (
        50
        102
        130
        60
        0
        0)
      RowHeights = (
        22
        18
        18
        18
        18)
    end
  end
  object WRegistry1: TWRegistry
    RootName = 'Assecont\Asseponto4'
    Left = 844
    Top = 74
  end
  object dxBarManager1: TdxBarManager
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -12
    Font.Name = 'Segoe UI'
    Font.Style = []
    Categories.Strings = (
      'Default')
    Categories.ItemsVisibles = (
      2)
    Categories.Visibles = (
      True)
    PopupMenuLinks = <>
    Style = bmsOffice11
    UseSystemFont = True
    Left = 798
    Top = 126
    DockControlHeights = (
      0
      0
      26
      0)
    object dxBarManager1Bar1: TdxBar
      Caption = 'Custom 1'
      CaptionButtons = <>
      DockedDockingStyle = dsTop
      DockedLeft = 0
      DockedTop = 0
      DockingStyle = dsTop
      FloatLeft = 800
      FloatTop = 8
      FloatClientWidth = 0
      FloatClientHeight = 0
      ItemLinks = <
        item
          Visible = True
          ItemName = 'dxBarButton1'
        end
        item
          Visible = True
          ItemName = 'dxBarButton3'
        end
        item
          Visible = True
          ItemName = 'dxBarButton2'
        end>
      OneOnRow = True
      Row = 0
      UseOwnFont = False
      Visible = True
      WholeRow = False
    end
    object dxBarButton1: TdxBarButton
      Caption = 'Cadastrar REP'
      Category = 0
      Hint = 'Cadastrar REP'
      Visible = ivAlways
      OnClick = Button2Click
    end
    object dxBarButton2: TdxBarButton
      Caption = 'Excluir'
      Category = 0
      Hint = 'Excluir'
      Visible = ivAlways
    end
    object dxBarButton3: TdxBarButton
      Caption = 'Enviar Cadastro Empregador'
      Category = 0
      Hint = 'Enviar Cadastro Empregador'
      Visible = ivAlways
      OnClick = Button1Click
    end
  end
end
