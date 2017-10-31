unit ifc_kernel;

interface

uses Kernel7x_TLB, Dialogs, SysUtils, ComObj;

type
  TRegistro = record
    Matricula : WideString;
    Numero,
    Funcao : Byte;
    DataHora : TDateTime;
    Flag : SFlagRegistro;
    Saida,
    MasterLib,
    FuncaoLib,
    AcessoNegado : WordBool;
    FonteEnt : SFonteEntrada;
    TipoNegado : STipoNegado;  
  end;
  TIfcKernel = class
  private
    FKernel : Variant;
    FIndex : Integer;
    function fncGetRegistro(var pReg : TRegistro) : Boolean;
  public
    constructor Create; 
    procedure Coleta;
    procedure QtRegs;        
  end;

implementation

function TIfcKernel.fncGetRegistro(
  var pReg : TRegistro) : Boolean;
begin
  Result := FKernel.RegistroOff[FIndex,
    pReg.Numero,
    pReg.Funcao,
    pReg.Matricula,
    pReg.DataHora,
    pReg.Flag,
    pReg.Saida,
    pReg.MasterLib,
    pReg.FuncaoLib,
    pReg.AcessoNegado,
    pReg.FonteEnt,
    pReg.TipoNegado];
end;

constructor TIfcKernel.Create;
begin
  FKernel := CreateOleObject('Kernel7x.Kernel');
  if FKernel.AdicionaCardTcpIp['192.168.0.254', '', 3000, false,
    cmcOffline, FIndex] then
    ShowMessage('Equipamento Adicionado');
end;

procedure TIfcKernel.QtRegs;
var
  _rQtRegs : Integer;
begin
  if FKernel.RecebeQtRegistros[FIndex, _rQtRegs] then
    ShowMessage(InttoStr(_rQtRegs) + ' Registros no equipamento');
end;

procedure TIfcKernel.Coleta;
var
  _rReg : TRegistro;
begin
  while FKernel.RecebePacote[FIndex] do
  begin
    while fncGetRegistro(_rReg) do
    begin
      ShowMessage(_rReg.Matricula + ' - ' +
        DateTimetoStr(_rReg.DataHora))
    end;
    if FKernel.ApagaUltimoPacote[FIndex] then
      ShowMessage('Pacote excluido');
  end;
end;  

end.
