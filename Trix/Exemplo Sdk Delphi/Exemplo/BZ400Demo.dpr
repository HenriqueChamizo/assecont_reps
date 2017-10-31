program BZ400Demo;

uses
  Forms,
  BZ400 in 'BZ400.pas' {FormBZ400};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TFormBZ400, FormBZ400);
  Application.Run;
end.
