program RepTrilobit;

uses
  Forms,
  SysUtils,
  pPrincipal in 'pPrincipal.pas' {frmRepTrilobitPrincipal},
  pDialogoVista_Parent in '..\..\..\Componentes2007\Wr\pDialogoVista_Parent.pas' {frmDialogoVista_Parent},
  pRepEdicao in 'pRepEdicao.pas' {frmRepEdicao},
  uJanelas in 'uJanelas.pas',
  pDataModule in 'pDataModule.pas' {DM: TDataModule},
  uFuncionarios in 'uFuncionarios.pas',
  uVars in 'uVars.pas',
  rDialogs,
  rAdo,
  uConsts in 'uConsts.pas',
  RepTrilobit_TLB in '..\..\..\Componentes2007\ActiveX\RepTrilobit_TLB.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TfrmRepTrilobitPrincipal, frmRepTrilobitPrincipal);
  Application.CreateForm(TDM, DM);
  Application.ShowMainForm := True;
  Application.Run;
end.
