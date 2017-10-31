unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, ExtCtrls;

type
  TForm1 = class(TForm)
    Button1: TButton;
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

 uses dll1510;

{$R *.DFM}

procedure TForm1.Button1Click(Sender: TObject);
 VAR
  ip, porta, end_dev, hora, end_con, resp, cpf_enviado, hash : string;
  com: integer;
begin
  Application.ProcessMessages;
  form1.update;

  ip := '10.0.2.241';
  porta := '1001' ;
  com := 2;
  end_dev := '0';

  end_con := end_dev;
  end_dev := 'U,' + end_dev;
  hora := formatdatetime('dd/mm/yyyy hh:MM:ss', now);


  resp := configura('AR', pchar(ip), '', pchar(porta), pchar(end_dev), pchar(hora),
          pchar('0'), pchar('0'), pchar('0'), pchar('0'), com, 1, 1, 0, 0, 1, 1, 9600);

  if (resp = end_con + ' 0') then
     showmessage('Rel�gio ajustado com sucesso! ')
    else
     showmessage('Rel�gio n�o ajustado! ');
end;



end.
