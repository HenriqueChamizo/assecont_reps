object DM: TDM
  OldCreateOrder = False
  Height = 150
  Width = 215
  object Conn: TADOConnection
    CommandTimeout = 3
    ConnectionString = 'driver={sql server};server=(local);database=asseponto4;uid=sa;'
    LoginPrompt = False
    AfterConnect = ConnAfterConnect
    Left = 40
    Top = 16
  end
  object Q: TADOQuery
    Connection = Conn
    CursorType = ctStatic
    Parameters = <>
    Left = 136
    Top = 56
  end
end
