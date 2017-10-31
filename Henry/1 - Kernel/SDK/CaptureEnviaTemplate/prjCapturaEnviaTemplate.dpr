program prjCapturaEnviaTemplate;

uses
  Forms,
  uCapturaEnviaTemplate in 'uCapturaEnviaTemplate.pas' {frmCapturaImagem};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmCapturaImagem, frmCapturaImagem);
  Application.Run;
end.
