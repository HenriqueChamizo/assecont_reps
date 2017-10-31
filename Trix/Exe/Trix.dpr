program Trix;

uses
  Forms,
  SysUtils,
  pPrincipal in '..\..\_Default\pPrincipal.pas' {frmConfigTerminal},
  pMain in 'pMain.pas' {frmMain},
  pDataModule in '..\..\_Default\pDataModule.pas' {DM: TDataModule},
  Kernel7x_TLB in '..\..\..\..\Componentes2007\ActiveX\Kernel7x_TLB.pas',
  pDialogoVista_Parent in '..\..\..\..\Componentes2007\Wr\pDialogoVista_Parent.pas' {frmDialogoVista_Parent};

{$R *.res}

begin
  Application.Initialize;
  ShortDateFormat := 'dd/mm/yyyy';
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TfrmMain, frmMain);
  Application.CreateForm(TDM, DM);
  Application.Run;
end.
