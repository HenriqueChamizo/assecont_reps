unit Principal;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, IdBaseComponent, IdComponent, IdTCPConnection,
  IdTCPClient;

type
  TFrm_MSGXREP = class(TForm)
    bt_EnviaMsg: TButton;
    mm_Log: TMemo;
    edt_IP: TEdit;
    edt_Porta: TEdit;
    Label1: TLabel;
    Label2: TLabel;
    CS_XREP: TIdTCPClient;
    procedure bt_EnviaMsgClick(Sender: TObject);
    procedure edt_PortaKeyPress(Sender: TObject; var Key: Char);
    procedure edt_IPKeyPress(Sender: TObject; var Key: Char);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Frm_MSGXREP: TFrm_MSGXREP;

implementation

uses Global;

{$R *.dfm}

procedure TFrm_MSGXREP.bt_EnviaMsgClick(Sender: TObject);
Var
  Teste : String;
  I : Integer;
begin
  If Trim(edt_IP.Text) = '' then
  begin
    MessageBox(Handle,'Preencha o endereço IP','XREP MSG',MB_OK+MB_ICONEXCLAMATION);
    exit;
  end;

  If Trim(edt_Porta.Text) = '' then
  begin
    MessageBox(Handle,'Preencha a Porta','XREP MSG',MB_OK+MB_ICONEXCLAMATION);
    exit;
  end;

  mm_Log.Text := '';
  //Calcula CRC da mensagem
  Teste := GeraCRC('!01,S,277,'+'1'+'54481502000100'+'000000000000'+AlinhaEsq('TRIX-1',150,' ')+AlinhaEsq('Rua Taciano Abaurre #225...',100,' ')+',');

  CS_XREP.Host := edt_IP.Text;
  CS_XREP.Port := StrToInt(edt_Porta.Text);
  //Ativa Conexão
  //CS_XREP.Active := true;
  CS_XREP.Connect;
  //Aguardo tempo para o equipamento responder
  Sleep(2000);
  //Caso tenha falhado na conexão, retenta por 3x
  if not CS_XREP.Socket.Connected then
  begin
    for I := 1 to 3 do
    begin
      //CS_XREP.Active := False;
      CS_XREP.Disconnect;
      mm_Log.Lines.Add('Falha na conexão');
      mm_Log.Lines.Add('Tentativa '+IntToStr(I)+'/3');
      //CS_XREP.Active := true;
      CS_XREP.Connect;
      Sleep(2000);
      if CS_XREP.Socket.Connected then
      begin
        Break;
      end;
    end;
    mm_Log.Lines.Add('Tentativas excedidas...');
    //CS_XREP.Active := False;
    CS_XREP.Disconnect;
    exit;
  end
  else
  begin
    mm_Log.Lines.Add('Conectado...');
    //Se estiver conectado, envia mensagem
    //Envia byte a byte da msg
    for I := 1 to Length(Teste) do
    begin
      //CS_XREP.Socket.SendText(Teste[I]);
      CS_XREP.Socket.Write(Teste[I]);
    end;
    //Exibe a mensagem enviada
    mm_Log.Lines.Add('Mensagem Enviada... "'+Teste+'"');
    //Aguarda para o equipamento retornar algo
    Sleep(1000);
    //mm_Log.Lines.Add(CS_XREP.Socket.ReceiveText); //Aqui que trava, caso não tenha nenhum retorno
    mm_Log.Lines.Add(CS_XREP.Socket.ReadString(15)); //Aqui que trava, caso não tenha nenhum retorno
    exit;
  end;

end;

procedure TFrm_MSGXREP.edt_PortaKeyPress(Sender: TObject; var Key: Char);
begin
  if (Key = #13) then
  begin
    SelectNext(Sender as tWinControl, True, True );
    Key := #0;
  end;

  if not (Key in ['0'..'9', #8]) Then
    Key := #0;
end;

procedure TFrm_MSGXREP.edt_IPKeyPress(Sender: TObject; var Key: Char);
begin
  if (Key = #13) then
  begin
    SelectNext(Sender as tWinControl, True, True );
    Key := #0;
  end;

  if not (Key in ['0'..'9', #8, #46]) Then
    Key := #0;
end;

end.
