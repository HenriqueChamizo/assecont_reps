program Acionamento;


uses
  Forms, windows,
  U_principal in 'U_principal.pas' {frmMain};

{$R *.res}

begin
  CreateMutex(nil, false, 'ExemploDeAcionamento');
  if GetLastError = ERROR_ALREADY_EXISTS then
  begin
    Halt(0);
  end;
  Application.Initialize;
  Application.CreateForm(TfrmMain, frmMain);
  Application.Run;
end.
