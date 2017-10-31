unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, Menus;

type
  TForm1 = class(TForm)
    MainMenu1: TMainMenu;
    Empregador1: TMenuItem;
    Manuteno1: TMenuItem;
    Funconario1: TMenuItem;
    Cadastrarnovoterminal1: TMenuItem;
    Enviar1: TMenuItem;
    EnviarDataeHora1: TMenuItem;
    ImportaodeMarcao1: TMenuItem;
    EnviarCadastro1: TMenuItem;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

end.
