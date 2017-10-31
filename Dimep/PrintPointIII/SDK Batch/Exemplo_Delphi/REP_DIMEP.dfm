object REPDIMEP: TREPDIMEP
  OldCreateOrder = False
  OnCreate = DataModuleCreate
  OnDestroy = DataModuleDestroy
  Left = 214
  Top = 177
  Height = 214
  Width = 306
  object cdsBIO: TClientDataSet
    Aggregates = <>
    Params = <>
    Left = 20
    Top = 8
    object cdsBIOPIS: TStringField
      FieldName = 'PIS'
      Size = 50
    end
    object cdsBIOBIO: TWideStringField
      DisplayWidth = 4000
      FieldName = 'BIO'
      Size = 4000
    end
    object cdsBIOTamanho: TIntegerField
      FieldName = 'Tamanho'
    end
  end
end
