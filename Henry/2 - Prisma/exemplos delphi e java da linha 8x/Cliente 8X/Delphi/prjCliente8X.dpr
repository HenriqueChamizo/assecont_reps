program prjCliente8X;

uses
  Forms,
  uClienteGPRS in 'uClienteGPRS.pas' {frmClienteGPRS};

{$R *.res}

begin
  Application.Initialize;
  Application.Title := 'Cliente 8X';
  Application.CreateForm(TfrmClienteGPRS, frmClienteGPRS);
  Application.Run;
end.
