unit RepTrilobit;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, RepTrilobit_TLB, ActiveX, ComCtrls, StdCtrls, ExtCtrls;

type
  TfrmRepTrilobit = class(TForm)
    pgcTab: TPageControl;
    tabEmp: TTabSheet;
    tabPes: TTabSheet;
    tabCfg: TTabSheet;
    tabAFD: TTabSheet;
    grbCadastrarEmpregador: TGroupBox;
    lblTipoDoc: TLabel;
    lblDocumento: TLabel;
    lblCEI: TLabel;
    lblRazaoSocial: TLabel;
    lblLocal: TLabel;
    cboTipoDoc: TComboBox;
    txtDocumento: TEdit;
    txtCEI: TEdit;
    txtRazaoSocial: TEdit;
    txtLocal: TEdit;
    cmdCadastrarEmpregador: TButton;
    grbCadastrarEmpregado: TGroupBox;
    lblPIS: TLabel;
    lblNome: TLabel;
    lblCracha: TLabel;
    lblPossuiBio: TLabel;
    txtPIS: TEdit;
    txtNome: TEdit;
    txtCracha: TEdit;
    chkPossuiBio: TCheckBox;
    cmdExcluirEmpregado: TButton;
    cmdCadastrarEmpregado: TButton;
    grbLerEmpregadosTXT: TGroupBox;
    lblDestinoEmp: TLabel;
    lblAddCabecalho: TLabel;
    txtArquivoEmp: TEdit;
    chkAddCabecalho: TCheckBox;
    cmdLerEmpregadosTXT: TButton;
    grbLerEmpregadosDT: TGroupBox;
    lstEmpregados: TListBox;
    cmdLerEmpregadosDT: TButton;
    grbParamSet: TGroupBox;
    lblParamSet: TLabel;
    lblValorSet: TLabel;
    cboParamSet: TComboBox;
    txtParamSet: TEdit;
    cmdParamSet: TButton;
    grbParamGet: TGroupBox;
    lblParamGet: TLabel;
    lblValorGet: TLabel;
    cboParamGet: TComboBox;
    txtParamGet: TEdit;
    cmdParamGet: TButton;
    grbLerAFD_TXT: TGroupBox;
    lblDataInicial: TLabel;
    lblDataFinal: TLabel;
    lblArquivoAFD: TLabel;
    txtDataInicial: TEdit;
    txtDataFinal: TEdit;
    txtArquivoAFD: TEdit;
    cmdLerAFD_TXT: TButton;
    grbLerAFD_DT: TGroupBox;
    lstAFD: TListBox;
    cmdLerAFD_DT: TButton;
    txtNSR: TEdit;
    lblNSR: TLabel;
    gbrIP: TGroupBox;
    txtIP: TEdit;
    txtPorta: TEdit;
    txtSenha: TEdit;
    lblIP: TLabel;
    lblPorta: TLabel;
    lblSenha: TLabel;
    function ValidarData(const aData: string): Boolean;
    procedure AtualizarIP();
    procedure CarregarComboSetConfig();
    procedure CarregarComboGetConfig();
    procedure FormCreate(Sender: TObject);
    procedure cmdCadastrarEmpregadorClick(Sender: TObject);
    procedure cmdCadastrarEmpregadoClick(Sender: TObject);
    procedure cmdExcluirEmpregadoClick(Sender: TObject);
    procedure cmdLerEmpregadosTXTClick(Sender: TObject);
    procedure cmdLerEmpregadosDTClick(Sender: TObject);
    procedure cmdParamSetClick(Sender: TObject);
    procedure cmdParamGetClick(Sender: TObject);
    procedure cmdLerAFD_TXTClick(Sender: TObject);
    procedure cmdLerAFD_DTClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmRepTrilobit: TfrmRepTrilobit;
  meuREP: REP;
  IP: WideString;
  Porta: Integer;
  Senha: Integer;

implementation

{$R *.dfm}


procedure TfrmRepTrilobit.cmdCadastrarEmpregadoClick(Sender: TObject);
var
  PIS: WideString;
  Nome: WideString;
  Cracha: WideString;
  PossuiBio: WordBool;

begin
  //Atualiza o valor das variáveis IP, Porta e Senha
  AtualizarIP;

  //Converter os valores da tela em variáveis do
  //tipo criado pela TLB.
  PIS := txtPIS.Text;
  Nome := txtNome.Text;
  Cracha := txtCracha.Text;
  PossuiBio := chkPossuiBio.Checked;

  //Chamar o método que cadastra o empregado no REP.
  //Caso o retorno seja FALSE, significa que ocorreu um erro.
  //Uma descrição do erro ocorrido estará disponível na
  //propriedade ErrorException.
  if false = meuREP.CadastrarEmpregado(IP, Porta, Senha, PIS, Nome, Cracha, PossuiBio) then
    ShowMessage(meuREP.ErrorException.Message)
  else
    ShowMessage('Cadastro enviado com sucesso!');
end;

procedure TfrmRepTrilobit.cmdCadastrarEmpregadorClick(Sender: TObject);
var
  TipoDoc: TOleEnum;
  Documento: WideString;
  CEI: WideString;
  RazaoSocial: WideString;
  Local: WideString;

begin
  //Atualiza o valor das variáveis IP, Porta e Senha
  AtualizarIP;

  //Converter os valores da tela em variáveis do
  //tipo criado pela TLB.
  if cboTipoDoc.Text = 'CNPJ' then
    TipoDoc := eTipoDocumento_CNPJ
  else
    TipoDoc := eTipoDocumento_CPF;

  Documento := txtDocumento.Text;
  CEI := txtCEI.Text;
  RazaoSocial := txtRazaoSocial.Text;
  Local := txtLocal.Text;

  //Chamar o método que cadastra o empregado no REP.
  //Caso o retorno seja FALSE, significa que ocorreu um erro.
  //Uma descrição do erro ocorrido estará disponível na
  //propriedade ErrorException.
  if false = meuREP.CadastrarEmpregador(IP, Porta, Senha, TipoDoc, Documento, CEI, RazaoSocial, Local) then
    ShowMessage(meuREP.ErrorException.Message)
  else
    ShowMessage('Cadastro enviado com sucesso!');
end;

procedure TfrmRepTrilobit.cmdExcluirEmpregadoClick(Sender: TObject);
var
  PIS: WideString;

begin
  //Atualiza o valor das variáveis IP, Porta e Senha
  AtualizarIP;

  //Converter os valores da tela em variáveis do
  //tipo criado pela TLB.
  PIS := txtPIS.Text;

  //Chamar o método que exclui o empregado no REP.
  //Caso o retorno seja FALSE, significa que ocorreu um erro.
  //Uma descrição do erro ocorrido estará disponível na
  //propriedade ErrorException.
  if false = meuREP.ExcluirEmpregado(IP, Porta, Senha, PIS) then
    ShowMessage(meuREP.ErrorException.Message)
  else
    ShowMessage('Exclusão realizada com sucesso!');
end;

procedure TfrmRepTrilobit.cmdLerAFD_DTClick(Sender: TObject);
var
  sLista: WideString;
  sDataInicial: WideString;
  sDataFinal: WideString;
  lNSR: Integer;

  registros: TStrings;
  i: Integer;

begin
  if false = ValidarData(txtDataInicial.Text) then
    exit;

  if false = ValidarData(txtDataFinal.Text) then
    exit;

  //Atualiza o valor das variáveis IP, Porta e Senha
  AtualizarIP;

  //Converter os valores da tela em variáveis do
  //tipo criado pela TLB.
  sDataInicial := FormatDateTime('YYYYMMDD', StrToDate(txtDataInicial.Text));
  sDataFinal := FormatDateTime('YYYYMMDD', StrToDate(txtDataFinal.Text));
  lNSR := StrToInt(txtNSR.Text);

  //Chamar o método que cria uma lista contendo o AFD lido a partir do REP.
  //Caso o retorno seja FALSE, significa que ocorreu um erro.
  //Uma descrição do erro ocorrido estará disponível na
  //propriedade ErrorException.
  if false = meuREP.LerAFD_ViaLista(IP, Porta, Senha, sDataInicial, sDataFinal, sLista, lNSR, ';', '|') then
    ShowMessage(meuREP.ErrorException.Message)
  else
  begin
    lstAFD.Clear;
    registros := TStringList.Create;
    try
      registros.Clear;
      registros.StrictDelimiter := true;
      registros.Delimiter := '|';
      registros.DelimitedText := sLista;
      for i := 0 to registros.Count - 1 do
      begin
        lstAFD.Items.Add(registros[i]);
      end;
    finally
      registros.Free;
    end;

    ShowMessage('Lista gerada com sucesso!');
  end;
end;

procedure TfrmRepTrilobit.cmdLerAFD_TXTClick(Sender: TObject);
var
  Arquivo: WideString;
  sDataInicial: WideString;
  sDataFinal: WideString;

begin
  if false = ValidarData(txtDataInicial.Text) then
    exit;

  if false = ValidarData(txtDataFinal.Text) then
    exit;

  //Atualiza o valor das variáveis IP, Porta e Senha
  AtualizarIP;

  //Converter os valores da tela em variáveis do
  //tipo criado pela TLB.
  Arquivo := txtArquivoAFD.Text;
  sDataInicial := FormatDateTime('YYYYMMDD', StrToDate(txtDataInicial.Text));
  sDataFinal := FormatDateTime('YYYYMMDD', StrToDate(txtDataFinal.Text));

  //Chamar o método que cria o arquivo AFD lido a partir do REP.
  //Caso o retorno seja FALSE, significa que ocorreu um erro.
  //Uma descrição do erro ocorrido estará disponível na
  //propriedade ErrorException.
  if false = meuREP.LerAFD(IP, Porta, Senha, sDataInicial, sDataFinal, Arquivo) then
    ShowMessage(meuREP.ErrorException.Message)
  else
    ShowMessage('Arquivo gerado com sucesso!');
end;

procedure TfrmRepTrilobit.cmdLerEmpregadosDTClick(Sender: TObject);
var
  sLista: WideString;
  registros: TStrings;
  i: Integer;

begin
  //Atualiza o valor das variáveis IP, Porta e Senha
  AtualizarIP;

  //Chamar o método que cria uma lista de empregados a partir do REP.
  //Caso o retorno seja FALSE, significa que ocorreu um erro.
  //Uma descrição do erro ocorrido estará disponível na
  //propriedade ErrorException.
  if false = meuREP.LerEmpregados_ViaLista(IP, Porta, Senha, sLista, ';', '|') then
    ShowMessage(meuREP.ErrorException.Message)
  else
  begin
    lstEmpregados.Clear;
    registros := TStringList.Create;
    try
      registros.Clear;
      registros.StrictDelimiter := true;
      registros.Delimiter := '|';
      registros.DelimitedText := sLista;
      for i := 0 to registros.Count - 1 do
      begin
        lstEmpregados.Items.Add(registros[i]);
      end;
    finally
      registros.Free;
    end;

    ShowMessage('Lista gerada com sucesso!');
  end;
end;


procedure TfrmRepTrilobit.cmdLerEmpregadosTXTClick(Sender: TObject);
var
  Arquivo: WideString;
  AdicionarCabecalho: WordBool;

begin
  //Atualiza o valor das variáveis IP, Porta e Senha
  AtualizarIP;

  //Converter os valores da tela em variáveis do
  //tipo criado pela TLB.
  Arquivo := txtArquivoEmp.Text;
  AdicionarCabecalho := chkAddCabecalho.Checked;

  //Chamar o método que cria o arquivo AFD lido a partir do REP.
  //Caso o retorno seja FALSE, significa que ocorreu um erro.
  //Uma descrição do erro ocorrido estará disponível na
  //propriedade ErrorException.
  if false = meuREP.LerEmpregados(IP, Porta, Senha, Arquivo, AdicionarCabecalho) then
    ShowMessage(meuREP.ErrorException.Message)
  else
    ShowMessage('Arquivo gerado com sucesso!');
end;

procedure TfrmRepTrilobit.cmdParamGetClick(Sender: TObject);
var
  Valor: WideString;
  Parametro: TOleEnum;

begin
  if cboParamGet.ItemIndex = -1 then
    exit;

  //Atualiza o valor das variáveis IP, Porta e Senha
  AtualizarIP;

  //Converter os valores da tela em variáveis do
  //tipo criado pela TLB.
  Valor := '';
  Parametro := TOleEnum(cboParamGet.Items.Objects[cboParamGet.ItemIndex]);

  //Chamar o método que lê a configuração do REP.
  //Caso o retorno seja FALSE, significa que ocorreu um erro.
  //Uma descrição do erro ocorrido estará disponível na
  //propriedade ErrorException.
  if false = meuREP.LerConfiguracao(IP, Porta, Senha, Parametro, Valor) then
  begin
    ShowMessage(meuREP.ErrorException.Message);
    txtParamGet.Text := '';
  end
  else
    txtParamGet.Text := Valor;
end;

procedure TfrmRepTrilobit.cmdParamSetClick(Sender: TObject);
var
  Valor: WideString;
  Parametro: TOleEnum;

begin
  if cboParamSet.ItemIndex = -1 then
    exit;

  //Atualiza o valor das variáveis IP, Porta e Senha
  AtualizarIP;

  //Converter os valores da tela em variáveis do
  //tipo criado pela TLB.
  Valor := txtParamSet.Text;
  Parametro := TOleEnum(cboParamSet.Items.Objects[cboParamSet.ItemIndex]);

  //Chamar o método que envia configuração ao REP.
  //Caso o retorno seja FALSE, significa que ocorreu um erro.
  //Uma descrição do erro ocorrido estará disponível na
  //propriedade ErrorException.
  if false = meuREP.EnviarConfiguracao(IP, Porta, Senha, Parametro, Valor) then
    ShowMessage(meuREP.ErrorException.Message)
  else
    ShowMessage('Configuração enviada com sucesso!');
end;

procedure TfrmRepTrilobit.FormCreate(Sender: TObject);
begin
  meuREP := CoREP.Create;
  CarregarComboSetConfig;
  CarregarComboGetConfig;
  txtDataInicial.Text := FormatDateTime('DD/MM/YYYY', Date);
  txtDataFinal.Text := FormatDateTime('DD/MM/YYYY', Date);
  cboTipoDoc.ItemIndex := 0;
end;













{
'***********************************************************************
'
'Funções de apoio, para validação e carga dos objetos da tela
'
'***********************************************************************
}
function TfrmRepTrilobit.ValidarData(const aData: string): Boolean;
begin
  try
    StrToDate(aData);
    Result := True;
  except
    on EConvertError do
    Result := False;
  end;
end;


procedure TfrmRepTrilobit.AtualizarIP();
begin
  IP := txtIP.Text;
  Porta := StrToInt(txtPorta.Text);
  Senha := StrToInt(txtSenha.Text);
end;

procedure TfrmRepTrilobit.CarregarComboSetConfig;
begin
  cboParamSet.AddItem('EnderecoIP', TObject(eParamSetConfig_EnderecoIP));
  cboParamSet.AddItem('PortaUDP', TObject(eParamSetConfig_PortaUDP));
  cboParamSet.AddItem('MascaraRede', TObject(eParamSetConfig_MascaraRede));
  cboParamSet.AddItem('Roteador', TObject(eParamSetConfig_Roteador));
  cboParamSet.AddItem('TipoIdentificacao', TObject(eParamSetConfig_TipoIdentificacao));
  cboParamSet.AddItem('InicioHorarioVerao', TObject(eParamSetConfig_InicioHorarioVerao));
  cboParamSet.AddItem('FimHorarioVerao', TObject(eParamSetConfig_FimHorarioVerao));
  cboParamSet.AddItem('Senha', TObject(eParamSetConfig_Senha));
  cboParamSet.AddItem('Reiniciar', TObject(eParamSetConfig_Reiniciar));
  cboParamSet.AddItem('AjusteRelogio', TObject(eParamSetConfig_AjusteRelogio));
end;

procedure TfrmRepTrilobit.CarregarComboGetConfig;
begin
  cboParamGet.AddItem('EnderecoIP', TObject(eParamGetConfig_EnderecoIP));
  cboParamGet.AddItem('PortaUDP', TObject(eParamGetConfig_PortaUDP));
  cboParamGet.AddItem('MascaraRede', TObject(eParamGetConfig_MascaraRede));
  cboParamGet.AddItem('Roteador', TObject(eParamGetConfig_Roteador));
  cboParamGet.AddItem('TipoIdentificacao', TObject(eParamGetConfig_TipoIdentificacao));
  cboParamGet.AddItem('InicioHorarioVerao', TObject(eParamGetConfig_InicioHorarioVerao));
  cboParamGet.AddItem('FimHorarioVerao', TObject(eParamGetConfig_FimHorarioVerao));
  cboParamGet.AddItem('HorarioAtual', TObject(eParamGetConfig_HorarioAtual));
  cboParamGet.AddItem('NumeroSerie', TObject(eParamGetConfig_NumeroSerie));
  cboParamGet.AddItem('Empregador', TObject(eParamGetConfig_Empregador));
  cboParamGet.AddItem('Documento', TObject(eParamGetConfig_Documento));
  cboParamGet.AddItem('Local', TObject(eParamGetConfig_Local));
  cboParamGet.AddItem('QtdeEmpregados', TObject(eParamGetConfig_QtdeEmpregados));
  cboParamGet.AddItem('QtdeLancamentos', TObject(eParamGetConfig_QtdeLancamentos));
  cboParamGet.AddItem('InicioOperacao', TObject(eParamGetConfig_InicioOperacao));
  cboParamGet.AddItem('UltimoRegistro', TObject(eParamGetConfig_UltimoRegistro));
  cboParamGet.AddItem('NsrAtual', TObject(eParamGetConfig_NsrAtual));
  cboParamGet.AddItem('RegistrosLivres', TObject(eParamGetConfig_RegistrosLivres));
  cboParamGet.AddItem('TamanhoMRP', TObject(eParamGetConfig_TamanhoMRP));
  cboParamGet.AddItem('EspacoLivre', TObject(eParamGetConfig_EspacoLivre));
  cboParamGet.AddItem('MacAddress', TObject(eParamGetConfig_MacAddress));
  cboParamGet.AddItem('NumeroModelo', TObject(eParamGetConfig_NumeroModelo));
  cboParamGet.AddItem('NumeroFabricante', TObject(eParamGetConfig_NumeroFabricante));
  cboParamGet.AddItem('CEI', TObject(eParamGetConfig_CEI));
  cboParamGet.AddItem('TipoDocumento', TObject(eParamGetConfig_TipoDocumento));
  cboParamGet.AddItem('StatusPapel', TObject(eParamGetConfig_StatusPapel));
  cboParamGet.AddItem('NumeroSerieCompleto', TObject(eParamGetConfig_NumeroSerieCompleto));
end;

end.
