program Coleta;

uses
  Forms,
  coletaUSB in 'coletaUSB.pas' {fColeta};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfColeta, fColeta);
  Application.Run;
end.
