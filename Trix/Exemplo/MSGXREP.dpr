program MSGXREP;

uses
  Forms,
  Principal in 'Principal.pas' {Frm_MSGXREP},
  Global in 'Global.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TFrm_MSGXREP, Frm_MSGXREP);
  Application.Run;
end.
