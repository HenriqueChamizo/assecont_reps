program PDIMEPTeste;

uses
  Forms,
  UDimepTeste in 'UDimepTeste.pas' {FDimepTeste},
  REP_DIMEP in 'REP_DIMEP.pas' {REPDIMEP: TDataModule},
  WatchComm_TLB in 'WatchComm_TLB.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TFDimepTeste, FDimepTeste);
  Application.CreateForm(TREPDIMEP, REPDIMEP);
  Application.Run;
end.
