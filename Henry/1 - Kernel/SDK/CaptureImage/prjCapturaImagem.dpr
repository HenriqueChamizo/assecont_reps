program prjCapturaImagem;

uses
  Forms,
  uCapturaImagem in 'uCapturaImagem.pas' {frmCapturaImagem};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmCapturaImagem, frmCapturaImagem);
  Application.Run;
end.
