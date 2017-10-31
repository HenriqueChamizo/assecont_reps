unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, OleServer, Kernel7x_TLB;

type
  TForm1 = class(TForm)
    Button1: TButton;
    eIp: TEdit;
    Label1: TLabel;
    Kernel1: TKernel;
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

uses DateUtils;

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
var
  _rIdxEquip : Integer;
  _rIdxHor1, _rIdxHor2, _rIdxHor3,
  _rIdxEscala1, _rIdxEscala2 : Integer;
  _rConfig : SComConfig;
  _rPeriodo : SPeriodo;
  _rStrHor : string;
  _rEscala : SEscala;
  _rFunc : SItemAcesso;
  _rControladores : SControladores;
begin
  TButton(Sender).Enabled := False;
  
//criando thread de comunicação
  with _rConfig do
  begin
    IsCatraca := False;
    ModoComunicacao := cmcOffline;
    TipoComunicacao := ctcTcpIp;
    //inicializando setor tcp ip da estrutura
    Tcp.Ip := eIp.Text;
    Tcp.MAC := '';
    Tcp.Porta := 3000;

    //inicializando o restante da estrutura com valores nulos
    Serial.NumeroRelogio := 0;
    Serial.Porta := '';
    Serial.Velocidade := cv9600;
    Modem.Fone := '';
    Modem.Porta := '';
    GPRS.Porta := 0;
  end;
  if not Kernel1.AdicionaCard[_rConfig, _rIdxEquip] then
  begin
    ShowMessage('Falha ao adicionar equipamento');
    Exit;
  end;

  _rPeriodo.Dias.Domingo := True;  
  _rPeriodo.Dias.Segunda := True;
  _rPeriodo.Dias.Terca := True;
  _rPeriodo.Dias.Quarta := True;
  _rPeriodo.Dias.Quinta := True;
  _rPeriodo.Dias.Sexta := True;
  _rPeriodo.Dias.Sabado := True;
  _rPeriodo.Dias.Feriado := True;    

//criando e adicionando periodo 1
  _rPeriodo.Horario := StrToTime('08:00:00');
  _rPeriodo.Tolerancia := 20;
  _rPeriodo.Dias.Terca := False;
  Kernel1.Add_Periodo(_rIdxEquip, _rPeriodo); //index = 1
//criando e adicionando periodo 2
  _rPeriodo.Horario := StrToTime('09:00:00');
  _rPeriodo.Tolerancia := 20;
  _rPeriodo.Dias.Segunda := False;
  _rPeriodo.Dias.Terca := True;
  Kernel1.Add_Periodo(_rIdxEquip, _rPeriodo); //index = 2
//criando e adicionando periodo 3
  _rPeriodo.Horario := StrToTime('10:00:00');
  _rPeriodo.Tolerancia := 10;
  _rPeriodo.Dias.Segunda := True;
  _rPeriodo.Dias.Quarta := False;
  Kernel1.Add_Periodo(_rIdxEquip, _rPeriodo); //index = 3

//criando e adicionando horário somente com primeiro período
  _rStrHor := '001';
  Kernel1.Add_Horario[_rIdxEquip, _rStrHor, _rIdxHor1];
//criando e adicionando horário somente com terceiro período
  _rStrHor := '003';
  Kernel1.Add_Horario[_rIdxEquip, _rStrHor, _rIdxHor2];
//criando e adicionando horário com todos os periodos
  _rStrHor := '001002003';
  Kernel1.Add_Horario[_rIdxEquip, _rStrHor, _rIdxHor3];

//criando escala com primeiro e segundo horários
  _rEscala.Inicio := Now;
  _rEscala.Horarios := FormatFloat('000', _rIdxHor1) +
                       FormatFloat('000', _rIdxHor2);
  Kernel1.Add_Escala[_rIdxEquip, _rEscala, _rIdxEscala1];

//criando escala com primeiro horário / dia de folga / terceiro horario
  _rEscala.Inicio := Now;
  _rEscala.Horarios := FormatFloat('000', _rIdxHor1) +
                       '###' + 
                       FormatFloat('000', _rIdxHor3);
  Kernel1.Add_Escala[_rIdxEquip, _rEscala, _rIdxEscala2];

  _rFunc.IndexHorario := 0;
  _rFunc.VerificarDigital := False;
  _rFunc.Master := False;
  _rFunc.Visitante := False;
  _rFunc.PeriodoBloqueio.Habilitado := False;

  with _rControladores do
  begin
    Control1 := false;
    Control2 := false;
    Control3 := false;
    Control4 := false;
    Control5 := false;
    Control6 := false;
    Control7 := false;
    Control8 := false;
  end;
  _rFunc.Controladores := _rControladores;
  _rFunc.Reles.AcionaRele1 := False;
  _rFunc.Reles.AcionaRele2 := False;
  _rFunc.Reles.AcionaRele3 := False;
            
//adicionando funcionario Liberado Sempre
  _rFunc.Matricula := '1';
  _rFunc.Acesso := cafLiberado;
  Kernel1.Add_ItemAcesso(_rIdxEquip, _rFunc);
//adicionando funcionario Bloqueado Sempre
  _rFunc.Matricula := '2';
  _rFunc.Acesso := cafNegado;
  Kernel1.Add_ItemAcesso(_rIdxEquip, _rFunc);
//adicionando funcionario Liberado porém em Férias hoje e amanha
  _rFunc.Matricula := '3';
  _rFunc.Acesso := cafLiberado;
  _rFunc.PeriodoBloqueio.Habilitado := True;
  _rFunc.PeriodoBloqueio.Inicio := Date;
  _rFunc.PeriodoBloqueio.Final := IncDay(Date, 1);
  Kernel1.Add_ItemAcesso(_rIdxEquip, _rFunc);
//adicionando funcionario respeitando o terceiro horario
  _rFunc.Matricula := '4';
  _rFunc.PeriodoBloqueio.Habilitado := False;
  _rFunc.Acesso := cafHorario;
  _rFunc.IndexHorario := _rIdxHor3;
  Kernel1.Add_ItemAcesso(_rIdxEquip, _rFunc);
//adicionando funcionario respeitando a primeira escala
  _rFunc.Matricula := '5';
  _rFunc.Acesso := cafEscala;
  _rFunc.IndexHorario := _rIdxEscala1;
  Kernel1.Add_ItemAcesso(_rIdxEquip, _rFunc);
//adicionando funcionario respeitando a segunda escala
  _rFunc.Matricula := '6';
  _rFunc.Acesso := cafEscala;
  _rFunc.IndexHorario := _rIdxEscala2;
  Kernel1.Add_ItemAcesso(_rIdxEquip, _rFunc);  

//enviando todos os itens adicionados
  if Kernel1.EnviaPeriodos[_rIdxEquip] then
    if Kernel1.EnviaHorarios[_rIdxEquip] then
      if Kernel1.EnviaListaAcesso[_rIdxEquip] then
      begin
        ShowMessage('Todos os setores enviados');
        TButton(Sender).Enabled := True;
        Exit;
      end;

  ShowMessage('Falha ao enviar. Verifique a formatação');
  TButton(Sender).Enabled := True;  
end;

end.
