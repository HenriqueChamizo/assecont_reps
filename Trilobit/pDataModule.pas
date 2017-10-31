unit pDataModule;

interface

uses
  SysUtils, Classes, DB, ADODB, rAdo;

type
  TDM = class(TDataModule)
    Conn: TADOConnection;
    Q: TADOQuery;
    procedure ConnAfterConnect(Sender: TObject);
  private
  public
  end;

var
  DM: TDM;

implementation

uses pPrincipal;

{$R *.dfm}

procedure TDM.ConnAfterConnect(Sender: TObject);
begin
Conn.Execute('SET DATEFORMAT dmy');
end;

end.
