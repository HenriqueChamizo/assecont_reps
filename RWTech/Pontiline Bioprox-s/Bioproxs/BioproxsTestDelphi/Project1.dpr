program Project1;

uses
  Forms,
  Unit3 in 'Unit3.pas' {Form3},
  dll1510 in 'dll1510.pas';

{$R *.RES}

begin
  Application.Initialize;
  Application.CreateForm(TForm3, Form3);
  Application.Run;
end.
