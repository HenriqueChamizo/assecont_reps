unit UDimepTeste;

interface

uses
  REP_DIMEP, WatchComm_TLB,
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, ActnList, Buttons, ActiveX, ExtCtrls, Mask,
  ToolWin, DB, DBClient, Grids, DBGrids;

type
  TFDimepTeste = class(TForm)
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    TabSheet2: TTabSheet;
    ActionList1: TActionList;
    actEmpregadorRegistrar: TAction;
    actEmpregadorLer: TAction;
    mmEmpregador: TMemo;
    Panel1: TPanel;
    Label7: TLabel;
    Label8: TLabel;
    Label9: TLabel;
    Label10: TLabel;
    Label11: TLabel;
    edEmpresaNome: TEdit;
    edEmpresaCNPJ: TEdit;
    edEmpresaCEI: TEdit;
    edEmpresaEndereco: TEdit;
    cmbEmpresaTipo: TComboBox;
    SpeedButton1: TSpeedButton;
    SpeedButton2: TSpeedButton;
    TabSheet3: TTabSheet;
    edFuncionarioPIS: TEdit;
    Label16: TLabel;
    Label15: TLabel;
    edFuncionarioNome: TEdit;
    Label1: TLabel;
    edFuncionarioCracha: TEdit;
    SpeedButton3: TSpeedButton;
    actEmpregadoRegistrar: TAction;
    MMMensagem: TMemo;
    actConfiguracaoLer: TAction;
    MMConfiguracao: TMemo;
    Panel2: TPanel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    edIP: TEdit;
    edPorta: TEdit;
    edTimeOut: TEdit;
    SpeedButton4: TSpeedButton;
    mskHoraVeraoInicio: TMaskEdit;
    mskHoraVeraoFim: TMaskEdit;
    Label5: TLabel;
    Label6: TLabel;
    SpeedButton5: TSpeedButton;
    actConfiguracaoHoraVeraoRegistrar: TAction;
    mskDataHoraAtual: TMaskEdit;
    Label12: TLabel;
    SpeedButton6: TSpeedButton;
    actConfiguracaoDataAtual: TAction;
    SpeedButton7: TSpeedButton;
    actConfiguracaoDataHoraAtualRegistrar: TAction;
    Label13: TLabel;
    Label14: TLabel;
    TabSheet4: TTabSheet;
    mskAFDArquivoInicio: TMaskEdit;
    mskAFDArquivoFim: TMaskEdit;
    Label17: TLabel;
    Label18: TLabel;
    SpeedButton8: TSpeedButton;
    Label19: TLabel;
    edAFDArquivo: TEdit;
    actAFDGerar: TAction;
    actAFDWideString: TAction;
    Panel3: TPanel;
    Label20: TLabel;
    RichEdit1: TRichEdit;
    ToolBar1: TToolBar;
    ToolButton1: TToolButton;
    Label21: TLabel;
    edNSR: TEdit;
    SpeedButton9: TSpeedButton;
    actEmpregadoExcluir: TAction;
    TabSheet5: TTabSheet;
    dsBIO: TDataSource;
    DBGrid1: TDBGrid;
    ToolBar2: TToolBar;
    ToolButton2: TToolButton;
    actBioCapturar: TAction;
    chkBiometria: TCheckBox;
    ToolButton3: TToolButton;
    ToolButton4: TToolButton;
    ToolButton5: TToolButton;
    actBioRemover: TAction;
    SpeedButton10: TSpeedButton;
    Panel4: TPanel;
    RichEdit2: TRichEdit;
    procedure FormCreate(Sender: TObject);
    procedure FormDestroy(Sender: TObject);
    procedure actEmpregadorRegistrarExecute(Sender: TObject);
    procedure actEmpregadorLerExecute(Sender: TObject);
    procedure actEmpregadoRegistrarExecute(Sender: TObject);
    procedure actConfiguracaoLerExecute(Sender: TObject);
    procedure actConfiguracaoHoraVeraoRegistrarExecute(Sender: TObject);
    procedure actConfiguracaoDataAtualExecute(Sender: TObject);
    procedure actConfiguracaoDataHoraAtualRegistrarExecute(
      Sender: TObject);
    procedure actAFDGerarExecute(Sender: TObject);
    procedure actAFDWideStringExecute(Sender: TObject);
    procedure actEmpregadoExcluirExecute(Sender: TObject);
    procedure actBioCapturarExecute(Sender: TObject);
    procedure actBioRemoverExecute(Sender: TObject);
    procedure SpeedButton10Click(Sender: TObject);
  private
    { Private declarations }
    Dimep: TREPDIMEP;
    procedure DimepSetConfigurar;
    procedure GetMensagem;
  public
    { Public declarations }
  end;

var
  FDimepTeste: TFDimepTeste;

implementation

{$R *.dfm}

procedure TFDimepTeste.DimepSetConfigurar;
begin
  Dimep.IP := edIP.Text;
  Dimep.Porta := StrToInt(edPorta.Text);
  Dimep.TimeOut := StrToInt(edTimeOut.Text);
end;

procedure TFDimepTeste.FormCreate(Sender: TObject);
begin
  if Dimep = nil then
    Dimep := TREPDIMEP.Create(nil);

  DimepSetConfigurar;
end;

procedure TFDimepTeste.FormDestroy(Sender: TObject);
begin
  if Dimep <> nil then
    Dimep.Destroy;
  Dimep := nil;
end;

procedure TFDimepTeste.actEmpregadorRegistrarExecute(Sender: TObject);
var
  TipoDoc: TOleEnum;
begin
  DimepSetConfigurar;

  mmEmpregador.Lines.Clear;

  case cmbEmpresaTipo.ItemIndex of
    0: TipoDoc := EmployeerType_CNPJ;
    1: TipoDoc := EmployeerType_CPF;
  end;

  if not Dimep.AtualizarEmpregador(TipoDoc, edEmpresaCNPJ.Text, edEmpresaCEI.Text, edEmpresaNome.Text, edEmpresaEndereco.Text) then
    mmEmpregador.Lines.Add('Erro ao alterar empregador.')
  else
    actEmpregadorLer.Execute;

  GetMensagem;
end;

procedure TFDimepTeste.actEmpregadorLerExecute(Sender: TObject);
begin
  DimepSetConfigurar;

  mmEmpregador.Lines.Clear;

  if not Dimep.LerEmpregador then
  begin
    mmEmpregador.Lines.Add(Dimep.MSG);
    Exit;
  end;

  mmEmpregador.Lines.Add('Raz�o Social : ' + Dimep.GetEmpregador);
  mmEmpregador.Lines.Add('Endere�o     : ' + Dimep.GetLocal);
  mmEmpregador.Lines.Add('Documento    : ' + Dimep.GetDocumento);
  mmEmpregador.Lines.Add('Tipo Doc.    : ' + Dimep.GetTipoDocumento);
  mmEmpregador.Lines.Add('CEI          : ' + Dimep.GetCEI);

  GetMensagem;
end;

procedure TFDimepTeste.actEmpregadoRegistrarExecute(Sender: TObject);
begin
  DimepSetConfigurar;

  if not Dimep.CadastrarEmpregado(edFuncionarioPIS.Text, edFuncionarioNome.Text, edFuncionarioCracha.Text, False) then
    ShowMessage(Dimep.MSG);

  GetMensagem;
end;

procedure TFDimepTeste.GetMensagem;
begin
  MMMensagem.Lines.Clear;
  MMMensagem.Lines.Add(Dimep.MSG);
end;

procedure TFDimepTeste.actConfiguracaoLerExecute(Sender: TObject);
begin
  MMMensagem.Lines.Clear;
  MMConfiguracao.Lines.Clear;

  Dimep.ObterListaConfiguracao;

  MMConfiguracao.Lines.Add('..:: Dados do Empregador ::..');
  MMConfiguracao.Lines.Add('Empregador        : ' + Dimep.GetEmpregador);
  MMConfiguracao.Lines.Add('Tipo de Documento : ' + Dimep.GetTipoDocumento);
  MMConfiguracao.Lines.Add('Documento         : ' + Dimep.GetDocumento);
  MMConfiguracao.Lines.Add('Endere�o          : ' + Dimep.GetLocal);
  MMConfiguracao.Lines.Add('CEI               : ' + Dimep.GetCEI);

  MMConfiguracao.Lines.Add('');
  MMConfiguracao.Lines.Add('..:: Data/Hora ::..');
  MMConfiguracao.Lines.Add('Hor. de Ver�o INI : ' + Dimep.GetInicioHorarioVerao);
  MMConfiguracao.Lines.Add('Hor. de Ver�o FIM : ' + Dimep.GetFimHorarioVerao);

  MMConfiguracao.Lines.Add('');
  MMConfiguracao.Lines.Add('..:: Dados do Rel�gio ::..');
  MMConfiguracao.Lines.Add('N� S�rie completo : ' + Dimep.GetNumeroSerieCompleto);
  MMConfiguracao.Lines.Add('N� Modelo         : ' + Dimep.GetNumeroModelo);
  MMConfiguracao.Lines.Add('N� Fabricante     : ' + Dimep.GetNumeroFabricante);
  MMConfiguracao.Lines.Add('N� S�rie          : ' + Dimep.GetNumeroSerie);  

  MMConfiguracao.Lines.Add('');
  MMConfiguracao.Lines.Add('..:: Dados do Arquivo MRP ::..');
  MMConfiguracao.Lines.Add('Tamanho em Bytes  : ' + Dimep.GetTamanhoMRP);

  MMMensagem.Lines.Add(Dimep.MSG);
end;

procedure TFDimepTeste.actConfiguracaoHoraVeraoRegistrarExecute(
  Sender: TObject);
var
  msg: string;
begin
  MMMensagem.Lines.Clear;

  Dimep.RegistrarHorarioDeVerao(StrToDateTime(mskHoraVeraoInicio.Text), StrToDateTime(mskHoraVeraoFim.Text));
  msg := Dimep.MSG;

  actConfiguracaoLer.Execute;

  MMMensagem.Lines.Clear;
  MMMensagem.Lines.Add(msg);
end;

procedure TFDimepTeste.actConfiguracaoDataAtualExecute(Sender: TObject);
begin
  mskDataHoraAtual.Text := FormatDateTime('DD/MM/YYYY HH:MM:SS', Now);
end;

procedure TFDimepTeste.actConfiguracaoDataHoraAtualRegistrarExecute(
  Sender: TObject);
var
  msg: string;
begin
  MMMensagem.Lines.Clear;

  Dimep.RegistrarDataHoraAtual(StrToDateTime(mskDataHoraAtual.Text));
  msg := Dimep.MSG;

  MMMensagem.Lines.Clear;
  MMMensagem.Lines.Add(msg);
end;

procedure TFDimepTeste.actAFDGerarExecute(Sender: TObject);
begin
  Dimep.LerAFD(mskAFDArquivoInicio.Text, mskAFDArquivoFim.Text, edAFDArquivo.Text);

  MMMensagem.Lines.Clear;
  MMMensagem.Lines.Add(Dimep.MSG);
end;

procedure TFDimepTeste.actAFDWideStringExecute(Sender: TObject);
var
  arquivo: WideString;
begin
  Dimep.LerAFDViaLista(mskAFDArquivoInicio.Text, mskAFDArquivoFim.Text, arquivo, StrToIntDef(edNSR.Text, 0));

  MMMensagem.Lines.Clear;
  MMMensagem.Lines.Add(Dimep.MSG);


  RichEdit1.Lines.Text := arquivo;
end;

procedure TFDimepTeste.actEmpregadoExcluirExecute(Sender: TObject);
begin
  Dimep.ExcluirEmpregado(edFuncionarioPIS.Text, edFuncionarioCracha.Text);
end;

procedure TFDimepTeste.actBioCapturarExecute(Sender: TObject);
begin
  Dimep.CapturarBiometria(chkBiometria.Checked);
end;

procedure TFDimepTeste.actBioRemoverExecute(Sender: TObject);
begin
  if Dimep.cdsBIO.IsEmpty then
    Exit;
  Dimep.RemoverBiometria(Dimep.cdsBIOPIS.Value);
end;

procedure TFDimepTeste.SpeedButton10Click(Sender: TObject);
var
  funcionarios: WideString;
begin
  Dimep.LerFuncionarios(funcionarios);

  MMMensagem.Lines.Clear;
  MMMensagem.Lines.Add(Dimep.MSG);

  RichEdit2.Lines.Text := funcionarios;
end;

end.