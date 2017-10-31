unit uFuncionarios;

interface

uses SysUtils, WStringGrid, rAdo, uVars, uConsts;

procedure Preencher_Funcionarios(sgFuncionarios: TWStringGrid; Filtro: string = '');

implementation

uses pDataModule;

//------------------------------------------------------------------------
procedure Preencher_Funcionarios(sgFuncionarios: TWStringGrid; Filtro: string = '');
var
  S: string;
begin
S := 'SELECT FUNC_NOME, ' +
     '(CASE WHEN CAR_DESCRICAO IS NOT NULL THEN CAR_DESCRICAO ELSE ##Cargo não definido## END), ' +
     'FUNC_IND, ' +
     'EMP_NOME, ' +
     '(CASE WHEN FUNC_PIS IS NULL THEN ##Não Cadastrado## ELSE FUNC_PIS END), ' +
     'TRM_DESCRICAO, TRM_IND ' +
     'FROM Funcionarios ' +
     'INNER JOIN Empresas ON EMP_IND = FUNC_EMPRESA ' +
     'INNER JOIN TerminaisFuncionarios ON TRMF_FUNC = FUNC_IND ' +
     'INNER JOIN Terminais ON TRM_IND = TRMF_TERMINAL ' +
     'LEFT JOIN Cargos ON CAR_IND = FUNC_CARGO ' +
     'WHERE EMP_GRUPO = ' + IntToStr(GRUPO) + ' ' +
     'AND FUNC_DT_DEM IS NULL ';

if Filtro <> '' then
   S := S + ' AND FUNC_NOME LIKE ' + QuotedStr('%' + Filtro + '%');

S := S + ' ORDER BY TRM_DESCRICAO, FUNC_NOME';

Ler_Query_Grid(DM.Conn, S, sgFuncionarios);
end;

end.
