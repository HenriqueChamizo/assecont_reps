program prjParticionamento;

uses
  Forms,
  uParticionamento in 'uParticionamento.pas' {frmCapturaImagem};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmCapturaImagem, frmCapturaImagem);
  Application.Run;
end.
