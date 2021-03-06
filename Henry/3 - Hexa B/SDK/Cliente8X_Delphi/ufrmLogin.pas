unit ufrmLogin;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, Buttons;

type
  TfrmLogin = class(TForm)
    Panel1: TPanel;
    Label1: TLabel;
    Label2: TLabel;
    edtUsuario: TEdit;
    edtSenha: TEdit;
    chkExibirSenha: TCheckBox;
    btnLogar: TBitBtn;
    procedure chkExibirSenhaClick(Sender: TObject);
    procedure btnLogarClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmLogin: TfrmLogin;

implementation

{$R *.dfm}

procedure TfrmLogin.chkExibirSenhaClick(Sender: TObject);
begin
  if (chkExibirSenha.Checked) then
    begin
      edtSenha.PasswordChar := #0;
    end
  else
    begin
      edtSenha.PasswordChar := '*';
    end;
end;

procedure TfrmLogin.btnLogarClick(Sender: TObject);
var
  s_MsgErro: String;
begin
  if (Trim(edtUsuario.Text) = '') then
    begin
      s_MsgErro := 'Nome de usu�rio inv�lido';
    end
  else if (Trim(edtSenha.Text) = '') then
    begin
      s_MsgErro := 'Senha inv�lida.';
    end;

  if (s_MsgErro <> '') then
    begin
      ShowMessage(s_MsgErro);
      ModalResult := mrNone;
    end;
end;

end.
