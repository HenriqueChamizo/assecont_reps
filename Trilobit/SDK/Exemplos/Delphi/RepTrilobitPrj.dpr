program RepTrilobitPrj;

uses
  Forms,
  RepTrilobit in 'RepTrilobit.pas' {frmRepTrilobit},
  RepTrilobit_TLB in 'RepTrilobit_TLB.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TfrmRepTrilobit, frmRepTrilobit);
  Application.Run;
end.
