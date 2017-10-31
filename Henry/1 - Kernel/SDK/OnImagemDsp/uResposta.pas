unit uResposta;

interface

uses
  SysUtils, Forms, Classes, ComCtrls, Kernel7x_TLB;

type
  //Exemplo de thread para validação online
  TThreadResposta = class(TThread)
  private
    kernel7x : TKernel;
    ThreadIndex : integer;
    liberado : boolean;
  protected
    procedure Execute; override;
    procedure proEnviaResposta;
  public
    constructor Create(pKernel : TKernel; pThreadIndex : integer;
      pLiberado : boolean);
  end;

implementation

uses
  uPrincipal;


constructor TThreadResposta.Create(pKernel : TKernel; pThreadIndex : Integer;
  pLiberado : boolean);
begin
  Inherited Create(False);
  Priority := tpLowest;
  kernel7x := pKernel;
  ThreadIndex := pThreadIndex;
  liberado := pLiberado;
  FreeOnTerminate := False;
end;

procedure TThreadResposta.Execute;
begin
  Synchronize(proEnviaResposta);
end;


procedure TThreadResposta.proEnviaResposta;
var
  _rResposta : SResposta;
begin


  //Realize seu tratamento da imagem e devolva a resposta online
  with _rResposta do
  begin
    if (liberado) then
      Acesso := canLibEntrada
    else
      Acesso := canNegado;
    Mensagem := 'Evento OnImagem';
    Tempo    := 3;
    IDControlador := 0;
    TempoRele1 := 0;
    TempoRele2 := 0;
    TempoRele3 := 0;    
  end;
  kernel7x.RespostaOn(ThreadIndex, _rResposta);
end;


end.
