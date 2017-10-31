program onImagem;

uses
  Forms,
  uPrincipal in 'uPrincipal.pas' {frmOnImagem},
  uResposta in 'uResposta.pas';

{$R *.res}


begin
  Application.Initialize;
  Application.CreateForm(TfrmOnImagem, frmOnImagem);
  Application.Run;
end.
