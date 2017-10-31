unit Unit3;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, ExtCtrls;

type
  TForm3 = class(TForm)
    Button1: TButton;
    Button2: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form3: TForm3;

implementation

 uses dll1510;

{$R *.dfm}

procedure TForm3.Button1Click(Sender: TObject);
VAR
  ip, porta, end_dev, hora, end_con, resp, cpf_enviado, hash : string;
  com: integer;
begin
  Application.ProcessMessages;
  form3.update;

  ip := '192.168.1.199';
  porta := '1001' ;
  com := 2;
  end_dev := '0';

  end_con := end_dev;
  end_dev := 'U,' + end_dev;
  hora := formatdatetime('dd/mm/yyyy hh:MM:ss', now);


  resp := configura('AR', pchar(ip), '', pchar(porta), pchar(end_dev), pchar(hora),
          pchar('0'), pchar('0'), pchar('0'), pchar('0'), com, 1, 1, 0, 0, 1, 1, 9600);

  if (resp = end_con + ' 0') then
     showmessage('Relógio ajustado com sucesso! ')
    else
     showmessage('Relógio não ajustado! ');

end;

procedure TForm3.Button2Click(Sender: TObject);
VAR
  ip, porta, end_dev, end_con, resp, cpf_enviado, hash : string;
  com: integer;
begin
  Application.ProcessMessages;
  form3.update;

  ip := '192.168.1.199';
  porta := '1001' ;
  com := 2;
  end_dev := '0';

  end_con := end_dev;
  end_dev := 'U,' + end_dev;


  resp := configura('LR', pchar(ip), '', pchar(porta), pchar(end_dev), pchar('0'),
          pchar('0'), pchar('0'), pchar('0'), pchar('0'), com, 0, 0, 0, 0, 0, 0, 9600);

  if (resp <> '') then
     showmessage('Relógio ajustado com sucesso! \n' +
                  'Resposta: ' + resp)
    else
     showmessage('Relógio não ajustado! ');
end;

end.
