program MensagemPadrao;

uses
  Forms,
  Unit1 in 'Unit1.pas' {Form1},
  Kernel7x_TLB in '..\..\..\..\Arquivos de programas\Borland\Delphi7\Imports\Kernel7x_TLB.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
