unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, OleServer, Kernel7x_TLB, XPMan, ExtCtrls, ComCtrls;

type
  TForm1 = class(TForm)
    GroupBox1: TGroupBox;
    eIP: TEdit;
    Label1: TLabel;
    Button1: TButton;
    Kernel1: TKernel;
    lbstatus: TLabel;
    Button4: TButton;
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    TabSheet2: TTabSheet;
    TabSheet3: TTabSheet;
    GroupBox3: TGroupBox;
    Label2: TLabel;
    Label3: TLabel;
    cbxMsg: TComboBox;
    eLin1: TEdit;
    eLin2: TEdit;
    GroupBox2: TGroupBox;
    Label4: TLabel;
    Label5: TLabel;
    cbxEntrada: TComboBox;
    eEntLin1: TEdit;
    eEntLin2: TEdit;
    GroupBox4: TGroupBox;
    Label6: TLabel;
    Label7: TLabel;
    cbxSaida: TComboBox;
    eSaiLin1: TEdit;
    eSaiLin2: TEdit;
    procedure Button1Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);

  private
    { Private declarations }
  public
   _uNewIndex : Integer;
    { Public declarations }
  end;

var
  Form1: TForm1;
implementation
{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
var
 _rConfig : SComConfig;
begin
//esses são os parametros de configuração para comunicação com TCP/IP
  _rConfig.IsCatraca := False;
  _rConfig.TipoComunicacao := ctcTcpIp;
  _rConfig.ModoComunicacao := cmcOffline;
  _rConfig.Tcp.Ip := eIP.Text;
  _rConfig.Tcp.MAC := '';
  _rConfig.Tcp.Porta := 3000;
  if kernel1.AdicionaCard[_rConfig, _uNewIndex] then
  begin
    lbstatus.Caption := 'Equipamento adicionado   IP: '+ eIP.Text;
  end;
end;

procedure TForm1.Button4Click(Sender: TObject);
var
 _rMsgPadrao : Boolean;
 _rMsgTipo : SMsgPadrao;
begin

  case cbxMsg.ItemIndex of
  0: begin //estilo data hora, msg padrão
     _rMsgTipo.Padrao.Estilo := cmeDataHora;
     _rMsgTipo.Padrao.Linha2 := eLin2.Text;
     end;
  1: begin //estilo personalizada, msg padrão
     _rMsgTipo.Padrao.Estilo := cmePersonalizada;
     _rMsgTipo.Padrao.Linha1 := eLin1.Text;
     _rMsgTipo.Padrao.Linha2 := eLin2.Text;
     end;
  end;

  case cbxEntrada.ItemIndex of
  0: begin //estilo matricula, registro de entrada
     _rMsgTipo.Entrada.Estilo := cmeMatricula;
     _rMsgTipo.Entrada.Linha2 := eEntLin2.Text;
     _rMsgTipo.Entrada.Tempo := 3;
     end;
  1: begin //estilo data hora, registro de entrada
     _rMsgTipo.Entrada.Estilo := cmeDataHora;
     _rMsgTipo.Entrada.Linha2 := eEntLin2.Text;
     _rMsgTipo.Entrada.Tempo := 3;
     end;
  2: begin //estilo personalizada, registro de entrada
     _rMsgTipo.Entrada.Estilo := cmePersonalizada;
     _rMsgTipo.Entrada.Linha1 := eEntLin1.Text;
     _rMsgTipo.Entrada.Linha2 := eEntLin2.Text;
     _rMsgTipo.Entrada.Tempo := 3;
     end;
  end;

  case cbxSaida.ItemIndex of
  0: begin //estilo matricula, registro de saida
     _rMsgTipo.Saida.Estilo := cmeMatricula;
     _rMsgTipo.Saida.Linha2 := eSaiLin2.Text;
     _rMsgTipo.Saida.Tempo := 3;
     end;
  1: begin //estilo data hora, registro de saida
     _rMsgTipo.Saida.Estilo := cmeDataHora;
     _rMsgTipo.Saida.Linha2 := eSaiLin2.Text;
     _rMsgTipo.Saida.Tempo := 3;
     end;
  2: begin //estilo personalizada, registro de saida
     _rMsgTipo.Saida.Estilo := cmePersonalizada;
     _rMsgTipo.Saida.Linha1 := eSaiLin1.Text;
     _rMsgTipo.Saida.Linha2 := eSaiLin2.Text;
     _rMsgTipo.Saida.Tempo := 3;
     end;
  end;

 _rMsgPadrao := Kernel1.EnviaMsgPadrao[_uNewIndex, _rMsgTipo];
 if _rMsgPadrao then
 begin
  lbstatus.Caption := 'Mensagens enviadas';
 end;
end;
end.

