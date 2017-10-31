inherited frmMain: TfrmMain
  Caption = 'Comunica'#231#227'o Henry'
  PixelsPerInch = 96
  TextHeight = 13
  inherited pnRodape: TPanel
    Color = 16107953
    ParentBackground = False
  end
  inherited Panel1: TPanel
    Color = 16107953
    ParentBackground = False
    inherited Panel2: TPanel
      Color = 16107953
      ParentBackground = False
      inherited Shape1: TShape
        ExplicitHeight = 389
      end
    end
    inherited Panel3: TPanel
      Color = 16107953
      ParentBackground = False
      inherited Panel5: TPanel
        Width = 385
        ExplicitWidth = 385
        inherited sgFuncionarios: TWStringGrid
          Width = 381
          ExplicitWidth = 381
          ColWidths = (
            364
            0
            0
            0
            0
            0
            0)
        end
      end
      inherited pnOrfaos: TPanel
        Left = 388
        Width = 240
        ExplicitLeft = 388
        ExplicitWidth = 240
        inherited sgIds: TWStringGrid
          Width = 236
          FontGroup.Height = -11
          FontGroup.Style = [fsBold]
          RowGroupHeight = 18
          ExplicitWidth = 236
          ColWidths = (
            219
            0)
        end
      end
    end
  end
  inherited pnDisplay: TPanel
    Color = 16107953
    ParentBackground = False
    inherited mmDisplay: TMemo
      Font.Name = 'Lucida Console'
      ParentFont = False
    end
  end
  inherited dxBarManager1: TdxBarManager
    Categories.ItemsVisibles = (
      2)
    Categories.Visibles = (
      True)
    DockControlHeights = (
      0
      0
      26
      0)
    inherited btEnviarEmpregador: TdxBarButton
      OnClick = btEnviarEmpregadorClick
    end
    inherited btImportarMarcacoes: TdxBarButton
      OnClick = btImportarMarcacoesClick
    end
  end
  inherited pmIds: TPopupMenu
    Images = cxImageList16x16
    inherited ExcluirnoTerminal1: TMenuItem
      ImageIndex = 8
    end
  end
  inherited pmTerminais: TPopupMenu
    object N1: TMenuItem
      Caption = '-'
    end
    object RegistrarDLL1: TMenuItem
      Caption = 'Registrar DLL'
    end
  end
  object Kernel1: TKernel
    AutoConnect = False
    ConnectKind = ckRunningOrNew
    OnProgresso = Kernel1Progresso
    OnColetaEventos = Kernel1ColetaEventos
    Left = 432
    Top = 144
  end
  object pmFuncionarios: TPopupMenu
    Left = 312
    Top = 304
    object EnviarFuncionriosparaoTerminal1: TMenuItem
      Caption = 'Enviar Funcion'#225'rios para o Terminal'
      OnClick = EnviarFuncionriosparaoTerminal1Click
    end
    object ExcluirnoTerminal2: TMenuItem
      Caption = 'Excluir do Terminal'
      OnClick = ExcluirnoTerminal2Click
    end
  end
end
