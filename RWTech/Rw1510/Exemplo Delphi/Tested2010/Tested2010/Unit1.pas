unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, dll1510;

type
  TForm1 = class(TForm)
    Button1: TButton;
    Label1: TLabel;
    Memo1: TMemo;
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
    procedure insere(marcacao: array of TMarcacao; controle:Tcontrole);
  end;

var
  Form1: TForm1;

implementation

{$R *.DFM}

procedure TForm1.Button1Click(Sender: TObject);
var
  end_dev: string;
  com, evento, porta: integer;
  controle: Tcontrole;
  marcacao: array [1..10] of Tmarcacao;
begin
  porta := 1001;
  end_dev := '0';
  com := 2;//1;
  evento := 0;

  controle.start := true;
  controle.modelo := 1;
  controle.porta := porta;
  controle.endereco := '192.168.1.181';//'10.0.0.32';//end_dev;
  controle.s_tipo := com;
  controle.baudrate := 9600;

  marcacao[1].nsr := 0;
  marcacao[2].nsr := 0;
  marcacao[3].nsr := 0;
  marcacao[4].nsr := 0;
  marcacao[5].nsr := 0;
  marcacao[6].nsr := 0;
  marcacao[7].nsr := 0;
  marcacao[8].nsr := 0;
  marcacao[9].nsr := 0;
  marcacao[10].nsr := 0;

  if recebeMarcacoesTCP(marcacao, controle, evento) then
  begin
    sleep(100);
    if (marcacao[1].pis <> '') then
      insere(marcacao, controle);

    controle.start := false;

    while recebeMarcacoesTCP(marcacao, controle, 0) do
    begin
      sleep(100);
      Application.ProcessMessages;

      if (marcacao[1].nsr = 0) then  //ver carioca
      begin
        exit;
      end;

      if (marcacao[1].pis <> '') then
        insere(marcacao, controle);

      label1.Caption := 'Recebendo ' + inttostr(controle.atual) + ' de ' +
                             inttostr(controle.total) + ' marcações.';

      if controle.erro <> 0 then
      begin
        showmessage('Erro!');
      end;

      marcacao[1].nsr := 0;
      marcacao[2].nsr := 0;
      marcacao[3].nsr := 0;
      marcacao[4].nsr := 0;
      marcacao[5].nsr := 0;
      marcacao[6].nsr := 0;
      marcacao[7].nsr := 0;
      marcacao[8].nsr := 0;
      marcacao[9].nsr := 0;
      marcacao[10].nsr := 0;
    end;
  end;
end;

procedure TForm1.insere(marcacao: array of TMarcacao; controle: Tcontrole);
var
  matricula, codigo, i: integer;
  p_pis, p_data, p_hora, cont, nsr: string;
  data: Tdate;
  a, m, d, h, min, z: word;
  hora: TTime;
begin
  for i := 0 to 9 do
  begin
    if (marcacao[i].nsr = 0) then
      exit;

    if (length(marcacao[i].pis) < 12) then
    begin
      while (length(marcacao[i].pis) < 12) do
        marcacao[i].pis := '0' + marcacao[i].pis;
    end;

    a := marcacao[i].ano;
    m := marcacao[i].mes;
    d := marcacao[i].dia;
    h := marcacao[i].hora;
    min := marcacao[i].minuto;
    z := 0;

    try
      data := Encodedate(a, m, d);
      hora := EncodeTime(h, min, z, z);
    except
      on e: exception do begin
        showmessage('dia ' + inttostr(d) + ' mes ' + inttostr(m) + ' ano ' + inttostr(a) + ' ' + e.Message);
      end;
    end;

    if ((marcacao[i].nsr < 0) or (marcacao[i].cont < 0){ or (matricula = 0)}) then
    begin
      exit;
    end;

    Memo1.Lines.Add(FormatDateTime('dd/mm/yyyy', data) + ' - ' + copy(timetostr(hora), 1, 5) +
                    ' - ' + marcacao[i].pis);
  end;
end;

end.
