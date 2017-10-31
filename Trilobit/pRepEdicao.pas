unit pRepEdicao;

interface

uses
  Forms, pDialogoVista_Parent, SysUtils, WIndice, StdCtrls, WCheckBox, WEdit, Controls,
  WLabel, Classes, ExtCtrls, WButtonsDialog, rAdo, WFieldsValues, WSpinEdit,
  WFormFocusMainForm, WFormTable;

type
  TfrmRepEdicao = class(TfrmDialogoVista_Parent)
    lblIP: TLabel;
    lblPorta: TLabel;
    lblSenha: TLabel;
    edIp: TWEdit;
    edPorta: TWEdit;
    edSenha: TWEdit;
    Label1: TLabel;
    Edit1: TWEdit;
    WCheckBox1: TWCheckBox;
    WCheckBox2: TWCheckBox;
    WCheckBox3: TWCheckBox;
    WFieldsValues1: TWFieldsValues;
    Label2: TLabel;
    edNumero: TWSpinEdit;
    WCheckBox4: TWCheckBox;
    WFormTable1: TWFormTable;
    procedure WButtonsDialog1Ok(Sender: TObject; var Validate: Boolean);
  private
  public
    Grupo: integer;
    procedure Ler;
  end;

var
  frmRepEdicao: TfrmRepEdicao;

implementation

uses pDataModule, pPrincipal;

{$R *.dfm}

procedure TfrmRepEdicao.WButtonsDialog1Ok(Sender: TObject;
  var Validate: Boolean);
begin
inherited;
if WFormTable1.Indice = 0 then
   WFieldsValues1['TRM_GRUPO'] := IntToStr(Grupo);

//SaveTable(DM.Conn, 'Terminais', Self);
end;

procedure TfrmRepEdicao.Ler;
begin
//ReadTable(DM.Conn, 'SELECT %CAMPOS% FROM Terminais WHERE %CAMPOINDICE% = %VALORINDICE%', Self);
end;

end.
