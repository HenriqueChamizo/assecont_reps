unit threaddinamic;

interface

uses Classes, SysUtils;

type
  TEventComunicacao = procedure of object;
  TDinamicThread = class(TThread)
  private
    FEventCom : TEventComunicacao;
    FWithInterface : Boolean;
    procedure proChamaMetodo;
  protected
    procedure Execute; override;
  public
    constructor Create(pMetodo : TEventComunicacao);
    property WithInterface : Boolean write FWithInterface;
  end;
  procedure RunInThread(pMetodo : TEventComunicacao);
  procedure RunInThreadInterface(pMetodo : TEventComunicacao);
  procedure RunInThreadInterfaceNotSync(pMetodo : TEventComunicacao);

implementation

procedure RunInThread(pMetodo : TEventComunicacao);
var
  _rThd : TDinamicThread;
begin
  _rThd := TDinamicThread.Create(pMetodo);
  _rThd.WithInterface := False;
  _rThd.Resume;
  _rThd.WaitFor;
  FreeAndNil(_rThd);
end;

procedure RunInThreadInterface(pMetodo : TEventComunicacao);
var
  _rThd : TDinamicThread;
begin
  _rThd := TDinamicThread.Create(pMetodo);
  _rThd.WithInterface := True;
  _rThd.Resume;
  _rThd.WaitFor;
  FreeAndNil(_rThd);  
end;

procedure RunInThreadInterfaceNotSync(pMetodo : TEventComunicacao);
var
  _rThd : TDinamicThread;
begin
  _rThd := TDinamicThread.Create(pMetodo);
  _rThd.WithInterface := True;
  _rThd.FreeOnTerminate := True;
  _rThd.Resume;
end;

constructor TDinamicThread.Create(pMetodo : TEventComunicacao);
begin
  inherited Create(True);
  FreeOnTerminate := False;
  Priority := tpIdle;
  FWithInterface := False;
  FEventCom := pMetodo;
end;

procedure TDinamicThread.proChamaMetodo;
begin
  FEventCom;
end;

procedure TDinamicThread.Execute;
begin
  if Assigned(FEventCom) then
    if FWithInterface then
      Synchronize(proChamaMetodo)
    else
      FEventCom;  
end;

end.
