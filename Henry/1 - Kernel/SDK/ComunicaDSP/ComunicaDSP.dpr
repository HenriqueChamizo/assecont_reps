program ComunicaDSP;

uses
  Forms,
  uD10SF01A in 'uD10SF01A.pas' {frmComunicaDSP};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmComunicaDSP, frmComunicaDSP);
  Application.Run;
end.
