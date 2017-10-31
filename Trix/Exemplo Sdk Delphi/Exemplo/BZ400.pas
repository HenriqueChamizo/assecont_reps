unit BZ400;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, OleCtrls, zkemkeeper_TLB, ExtCtrls, ComCtrls, Menus;

type
  TFormBZ400 = class(TForm)
    CZKEM1: TCZKEM;
    PopupMenu1: TPopupMenu;
    LoadFingerprint1: TMenuItem;
    OpenDialog1: TOpenDialog;
    SaveFingerprint1: TMenuItem;
    SaveDialog1: TSaveDialog;
    Panel1: TPanel;
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    Panel2: TPanel;
    lblName: TLabel;
    lblAddr: TLabel;
    lblTax: TLabel;
    lblCEI: TLabel;
    lblCert: TLabel;
    lblCount: TLabel;
    Label1: TLabel;
    btnDownload: TButton;
    btnCancel: TButton;
    edtName: TEdit;
    edtAddr: TEdit;
    edtTax: TEdit;
    rbCPF: TRadioButton;
    rbCNPJ: TRadioButton;
    btnSet: TButton;
    edtCEI: TEdit;
    btnGetEmployer: TButton;
    btnOpTimeLog: TButton;
    btnEmployerLog: TButton;
    btnEmployeeLog: TButton;
    edtPosition: TEdit;
    btnPosition: TButton;
    btnEnable: TButton;
    chkEnable: TCheckBox;
    btnClearAdmin: TButton;
    TabSheet2: TTabSheet;
    Panel3: TPanel;
    Label2: TLabel;
    edtIP: TEdit;
    Label3: TLabel;
    edtPwd: TEdit;
    btnConnect: TButton;
    TabSheet3: TTabSheet;
    lblUserNo: TLabel;
    edtUserNo: TEdit;
    btnGetUser: TButton;
    btnDownloadTemplate: TButton;
    btnUploadTemplate: TButton;
    lblUserName: TLabel;
    edtUserName: TEdit;
    lblPIS: TLabel;
    edtPIS: TEdit;
    btnSetUserEx: TButton;
    btnDelUser: TButton;
    btnFingerIndex: TLabel;
    edtIndex: TEdit;
    UpDown1: TUpDown;
    btnSSR_DelUserTmpExt: TButton;
    lblCPF: TLabel;
    edtCPF: TEdit;
    edtCount: TEdit;
    btnTest: TButton;
    Panel4: TPanel;
    ListBox1: TListBox;
    ProgressBar1: TProgressBar;
    btnGetGeneralLogDataStr: TButton;
    btnGetAllUserInfo: TButton;
    btnGetUserTmpStr: TButton;
    btnSetUserInfo: TButton;
    btnDelUserTmp: TButton;
    btnSetUserTmpStr: TButton;
    btnDeleteEnrollData: TButton;
    edtUserID: TEdit;
    edtBWName: TEdit;
    edtBWPassword: TEdit;
    edtBWCardNo: TEdit;
    lblBWCardNo: TLabel;
    lblBWPassword: TLabel;
    lblBWName: TLabel;
    lblUserID: TLabel;
    Label4: TLabel;
    edtBWIndex: TEdit;
    UpDown2: TUpDown;
    btnClearList: TButton;
    btnSetDeviceTime: TButton;
    edtUnlockSerialNumber: TEdit;
    btnGetUnlockPwd: TButton;
    Label5: TLabel;
    Label6: TLabel;
    edtParamName: TEdit;
    lblParamValue: TLabel;
    edtParaValue: TEdit;
    btnGetSysOptions: TButton;
    btnSetSysOptions: TButton;
    TabSheet4: TTabSheet;
    btnGetMRPTotal: TButton;
    Label7: TLabel;
    Label8: TLabel;
    edtMRPTotal: TEdit;
    ComboBox1: TComboBox;
    Label9: TLabel;
    Label10: TLabel;
    Label11: TLabel;
    Label12: TLabel;
    btnSetDaylight: TButton;
    Label13: TLabel;
    Label14: TLabel;
    Label15: TLabel;
    Label16: TLabel;
    Label17: TLabel;
    Label18: TLabel;
    edtM1: TEdit;
    edtM2: TEdit;
    edtD1: TEdit;
    edtD2: TEdit;
    CheckBox1: TCheckBox;
    btnGetDaylight: TButton;
    UpDown3: TUpDown;
    UpDown4: TUpDown;
    UpDown5: TUpDown;
    UpDown6: TUpDown;
    edtTime1: TEdit;
    Label19: TLabel;
    Label20: TLabel;
    edtTime2: TEdit;
    edtTime3: TEdit;
    edtTime4: TEdit;
    Label21: TLabel;
    btnRefreshOptions: TButton;
    btnTesting: TButton;
    btnUnlock: TButton;
    TabSheet5: TTabSheet;
    btnGetUserFace: TButton;
    btnSetUserFace: TButton;
    edtUserFace: TEdit;
    Label22: TLabel;
    Button1: TButton;
    procedure btnDownloadClick(Sender: TObject);
    procedure btnGetUserClick(Sender: TObject);
    procedure btnCancelClick(Sender: TObject);
    procedure btnSetClick(Sender: TObject);
    procedure btnSetUserExClick(Sender: TObject);
    procedure btnDelUserClick(Sender: TObject);
    procedure btnGetEmployerClick(Sender: TObject);
    procedure btnOpTimeLogClick(Sender: TObject);
    procedure btnEmployerLogClick(Sender: TObject);
    procedure btnEmployeeLogClick(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure btnDownloadTemplateClick(Sender: TObject);
    procedure btnTestClick(Sender: TObject);
    procedure btnConnectClick(Sender: TObject);
    procedure btnPositionClick(Sender: TObject);
    procedure btnDelFingerClick(Sender: TObject);
    procedure btnUploadTemplateClick(Sender: TObject);
    procedure LoadFingerprint1Click(Sender: TObject);
    procedure SaveFingerprint1Click(Sender: TObject);
    procedure btnEnableClick(Sender: TObject);
    procedure btnClearAdminClick(Sender: TObject);
    procedure btnGetGeneralLogDataStrClick(Sender: TObject);
    procedure btnGetAllUserInfoClick(Sender: TObject);
    procedure btnSetUserInfoClick(Sender: TObject);
    procedure btnDeleteEnrollDataClick(Sender: TObject);
    procedure btnGetUserTmpStrClick(Sender: TObject);
    procedure btnDelUserTmpClick(Sender: TObject);
    procedure btnClearListClick(Sender: TObject);
    procedure btnSetUserTmpStrClick(Sender: TObject);
    procedure btnSetDeviceTimeClick(Sender: TObject);
    procedure btnGetUnlockPwdClick(Sender: TObject);
    procedure btnGetSysOptionsClick(Sender: TObject);
    procedure btnSetSysOptionsClick(Sender: TObject);
    procedure btnGetMRPTotalClick(Sender: TObject);
    procedure btnSetDaylightClick(Sender: TObject);
    procedure btnGetDaylightClick(Sender: TObject);
    procedure btnRefreshOptionsClick(Sender: TObject);
    procedure btnTestingClick(Sender: TObject);
    procedure btnUnlockClick(Sender: TObject);
    procedure btnSetUserFaceClick(Sender: TObject);
    procedure btnGetUserFaceClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);

  private
    { Private declarations }
    bStop: Boolean;
    Template:TStringList;
    bConnected: Boolean;
    function CheckConnect: Boolean;
  public
    { Public declarations }
  end;

var
  FormBZ400: TFormBZ400;

implementation

uses Math;

{$R *.dfm}

procedure DelayTime(Second: Cardinal);
Var
  dwLast, dwInterval: Cardinal;
begin
  dwInterval := 1000 * Second;
  dwLast := GetTickCount;
  while True do
  begin
    Application.ProcessMessages;
    if  (GetTickCount - dwLast) > dwInterval then Break;
  end;
end;

procedure TFormBZ400.btnDownloadClick(Sender: TObject);
Var
  dwYear: Integer;
  dwMonth: Integer;
  dwDay: Integer;
  dwHour: Integer;
  dwMinute: Integer;
  dwSecond: Integer;

  NSR: WideString;
  PIS: WideString;

  time1, time2: string;
  iCount:Cardinal;
begin
  if CheckConnect then
  begin
    ProgressBar1.Max := 2000;
    ProgressBar1.Position := 0;

    ListBox1.Clear;
    bStop := False;
    time1 := FormatDateTime('hh:mm:ss', Now);
    iCount:= 0;


    //CZKEM1.EnableDevice(1, False);
    while (Not bStop) and CZKEM1.GetAttLogs(1, NSR, PIS, dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond) do
    begin
      ListBox1.Items.Add(Format('NSR:%0.9d         PIS:%s        %0.4d-%0.2d-%0.2d %0.2d:%0.2d:%0.2d', [StrToInt(NSR),PIS,dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond]));
      Inc(iCount);
      ProgressBar1.Position := iCount;
      lblCount.Caption := Format('Count:%d', [iCount]);
      Application.ProcessMessages;
      if ListBox1.Items.Count >= 10000 then ListBox1.Items.Clear;
    end;


    ProgressBar1.Position := ProgressBar1.Max;
    //ShowMessage('CZKEM1.EnableDevice(1, False);');
    CZKEM1.EnableDevice(1, TRUE);
    time2:= FormatDateTime('hh:mm:ss', Now);
    ListBox1.Items.Add(Format('Count:%d, %s-%s', [iCount, time1, time2]));
    //CZKEM1.Disconnect();
    bStop := False;
  end;
end;

procedure TFormBZ400.btnGetUserClick(Sender: TObject);
Var
  dwEnrollNumber: WideString;
  Name: WideString;
  Password: WideString;
  Privilege: Integer;
  Enabled: WordBool;
  PIS: WideString;
  CPF: WideString; 
begin
  if CheckConnect then
  begin
    ListBox1.Clear;



    while CZKEM1.SSR_GetAllUserInfoEx(1, dwEnrollNumber, Name, Password, Privilege, Enabled, PIS, CPF) do
    begin
      ListBox1.Items.Add(Format('UserNo:%0.24s, Name:%0.52s, Pwd:%0.8s, Privilege=%d, PIS:%0.12s, CPF:%0.14s',
        [dwEnrollNumber, Name, Password, Privilege, PIS, CPF]));
    end;


    lblCount.Caption := Format('Count:%d', [ListBox1.Count]);
    //CZKEM1.Disconnect();
    ShowMessage('finish');
  end;
end;

procedure TFormBZ400.btnCancelClick(Sender: TObject);
begin
  if Application.MessageBox('Stop downloading?', 'Download logs', MB_YESNO+MB_ICONQUESTION) = IDYES then
  begin
    bStop := True;
  end;
end;

procedure TFormBZ400.btnSetClick(Sender: TObject);
Var
  i: Integer;
begin
  i := IfThen(rbCPF.Checked, 0, 1);
  if CheckConnect then
  begin

    if CZKEM1.SetEmployer(1, edtName.Text, i, edtTax.Text, edtCEI.Text, edtAddr.Text) then
      ShowMessage('Successfully!')
    else
      ShowMessage('Failed to set');
    //CZKEM1.Disconnect();


  end;
end;

procedure TFormBZ400.btnSetUserExClick(Sender: TObject);
Var
  error:Integer;
begin
  if CheckConnect then
  begin
    if edtPIS.Text = '' then
    begin
      ShowMessage('Please input PIS');
      edtPIS.SetFocus;
      Exit;
    end;

    if edtUserName.Text = '' then
    begin
      ShowMessage('Please Input name');
      edtUserName.SetFocus;
      Exit;
    end;
    //CZKEM1.SetStrCardNumber(edtCPF.Text);

    if CZKEM1.SSR_SetUserInfoEx(1, edtUserNo.Text, edtUserName.Text, '1', 0, True,  edtPIS.Text, edtCPF.Text) then
      ShowMessage('Successfully!')
    else
      begin
        CZKEM1.GetLastError(error);
        ShowMessage(IntToStr(error));
      end;


    //CZKEM1.Disconnect();
  end;
end;

procedure TFormBZ400.btnDelUserClick(Sender: TObject);
begin
  if edtUserNo.Text='' then
  begin
    ShowMessage('Please input user number.');
    edtUserNo.SetFocus;
    exit;
  end;
  
  if CheckConnect then
  begin
    if CZKEM1.SSR_DeleteEnrollDataExt_BZ400(1, edtUserNo.Text, 12) then
      ShowMessage('Successfully!')
    else ShowMessage('Failed!');
    //CZKEM1.Disconnect();
  end;
end;

procedure TFormBZ400.btnGetEmployerClick(Sender: TObject);
Var
  EmpName: WideString;
  CertType: WideString;
  Tax: WideString;
  CEI: WideString;
  Addr: WideString;
begin
  //i := IfThen(rbCPF.Checked, 0, 1);
  if CheckConnect then
  begin

    if CZKEM1.GetEmployer(1, EmpName, CertType, Tax, CEI, Addr) then
    begin
      edtName.Text := EmpName;
      edtTax.Text := Tax;
      edtCEI.Text := CEI;
      edtAddr.Text := Addr;
      ShowMessage(Format('CertType=%s, name=%s', [CertType, EmpName]));
    end
    else
      ShowMessage('Failed to get information');
    //CZKEM1.Disconnect();


  end;
end;

procedure TFormBZ400.btnOpTimeLogClick(Sender: TObject);
var
  nsr:WideString;
  date1, date2: WideString;
begin
  if CheckConnect then
  begin
    ListBox1.Clear;

    while CZKEM1.GetDatetimeOpLog(1, nsr, date1, date2) do
    begin
      ListBox1.Items.Add(Format('%s  %s  %s', [nsr, date1, date2]));
      lblCount.Caption := Format('Count:%d', [ListBox1.Count]);
      Application.ProcessMessages;
    end;

    lblCount.Caption := Format('Count:%d', [ListBox1.Count]);
    Application.ProcessMessages;
    ShowMessage('finish');
    //CZKEM1.Disconnect();
  end;
end;

procedure TFormBZ400.btnEmployerLogClick(Sender: TObject);
var
  NSR: WideString;
   Year,Month,Day,Hour,min,sec: Integer;
   cnpj_cpf: WideString;
   identtype: Integer;
   CEI: WideString;
   Name: WideString;
   address, Optype: WideString;
begin
  if CheckConnect then
  begin
    ListBox1.Clear;

    while CZKEM1.GetEmployerOpLog(1, NSR, Year,Month,Day,Hour,min,sec, cnpj_cpf,identtype,CEI, Name, address, Optype) do
    begin
      ListBox1.Items.Add(Format('%s  %04d-%02d-%02d %02d:%02d:%02d %s  %d  %s  %s  %s %s',
        [NSR, Year,Month,Day,Hour,min,sec, cnpj_cpf,identtype,CEI, Name, address, Optype]));
      lblCount.Caption := Format('Count:%d', [ListBox1.Count]);
      Application.ProcessMessages;
    end;

    lblCount.Caption := Format('Count:%d', [ListBox1.Count]);
    Application.ProcessMessages;
    ShowMessage('finish'); 
  end;
end;

procedure TFormBZ400.btnEmployeeLogClick(Sender: TObject);
var
  NSR: WideString;
   Year,Month,Day,Hour,min,sec, icount: Integer;
   optype: WideString;
   PIS: WideString;
   Name: WideString;

begin
  if CheckConnect then
  begin
    ListBox1.Clear;
    icount := 0;

    while CZKEM1.GetEmployeeOpLog(1, NSR, Year,Month,Day,Hour,min,sec, optype,pis,name) do
    begin
      ListBox1.Items.Add(Format('%s  %04d-%02d-%02d %02d:%02d:%02d %s  %s  %s',
        [NSR, Year,Month,Day,Hour,min,sec, optype,pis,name]));
        Inc(icount);
        lblCount.Caption := Format('Count:%d', [icount]);
      Application.ProcessMessages;
    end;

    lblCount.Caption := Format('Count:%d', [ListBox1.Count]);
    //CZKEM1.Disconnect();
    ShowMessage('finish');
  end;
end;

procedure TFormBZ400.FormCreate(Sender: TObject);
begin
  Template:= TStringList.Create;
  
end;

procedure TFormBZ400.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  if bConnected then CZKEM1.Disconnect;
  Template.Free;
end;

procedure TFormBZ400.btnDownloadTemplateClick(Sender: TObject);
var
  strTemp:WideString;
  len, i, flag:Integer;

begin
  if edtUserNo.Text = '' then
  begin
    ShowMessage('Input User No.');
    Exit;
  end;

  if CheckConnect then
  begin
    ListBox1.Clear;
    for i := 0 to 9 do
    begin
      if CZKEM1.GetUserTmpExStr_BZ400(1, edtUserNo.Text, i, flag, strTemp, len) then
      begin
        ListBox1.Items.Add(strTemp);
        Application.ProcessMessages;
      end;
      lblCount.Caption := Format('Count:%d', [ListBox1.Count]);
    end;
  end;
  ShowMessage('finish');
end;

procedure TFormBZ400.btnTestClick(Sender: TObject);
Var
  i, j, k: Integer;  
begin
  if CheckConnect then
  begin
    j := StrToInt(edtUserNo.Text);
    k := j+StrToInt(edtCount.Text);
    ProgressBar1.Max := StrToInt(edtCount.Text);
    ProgressBar1.Position := 0;

    for i:=j to k do
    begin
      if Not CZKEM1.SSR_SetUserInfoEx(1, IntToStr(i), edtUserName.Text, '1', 0, True,  edtPIS.Text, edtCPF.Text) then
        break;
      ProgressBar1.StepIt;
      Application.ProcessMessages;
    end;

    ShowMessage('finish');
    //CZKEM1.Disconnect();
  end;
end;

function TFormBZ400.CheckConnect: Boolean;
begin
  if Not bConnected then
  begin
    if edtPwd.Text <> '' then
      CZKEM1.SetCommPassword(StrToInt(edtPwd.Text));
      
    bConnected := CZKEM1.Connect_Net(edtIP.Text, 4370);
    if Not bConnected then
    begin
      ShowMessage('Failed to Connect');
    end
    else
    begin
      btnConnect.Tag:= 1;
      btnConnect.Caption:='Disconnect';
    end;
  end;
  Result := bConnected;
end;

procedure TFormBZ400.btnConnectClick(Sender: TObject);
begin
  if btnConnect.Tag = 0 then
  begin
    CheckConnect;
  end
  else
  begin
    CZKEM1.Disconnect;
    btnConnect.Tag := 0;
    btnConnect.Caption := 'Connect';
    bConnected := False;
  end;
end;

procedure TFormBZ400.btnPositionClick(Sender: TObject);
begin
  if CheckConnect then
  begin

    CZKEM1.SetSeekPosition(1, StrToIntDef(edtPosition.Text, 1));

  end;
end;

procedure TFormBZ400.btnDelFingerClick(Sender: TObject);
begin
  if edtUserNo.Text='' then
  begin
    ShowMessage('Please input user number.');
    edtUserNo.SetFocus;
    exit;
  end;
  if CheckConnect then
  begin
    if CZKEM1.SSR_DelUserTmpExt(1, edtUserNo.Text, StrToInt(edtIndex.Text)) then
      ShowMessage('Successfully!')
    else ShowMessage('Failed!');
    //CZKEM1.Disconnect();
  end;
end;

procedure TFormBZ400.btnUploadTemplateClick(Sender: TObject);
var
  strTemp:WideString;
  i, count:Integer;
  err:Integer;
begin
  count := 0;
  if edtUserNo.Text = '' then
  begin
    ShowMessage('Input User No.');
    Exit;
  end;
  if ListBox1.Count = 0 then
  begin
    ShowMessage('No template');
    Exit;
  end;

  if CheckConnect then
  begin
    for i := 0 to ListBox1.Count-1 do
    begin
      strTemp := ListBox1.Items[i];
      if CZKEM1.SetUserTmpExStr_BZ400(1, edtUserNo.Text, i, 1, strTemp) then
      begin
        Inc(count);
        lblCount.Caption := Format('Count:%d', [Count]);
        ShowMessage('OK');
        Application.ProcessMessages;
      end
      else
      begin
        CZKEM1.GetLastError(err);
        ShowMessage(Format('error:%d', [err]));
      end;
    end;

  end;
end;

procedure TFormBZ400.LoadFingerprint1Click(Sender: TObject);
begin
  if OpenDialog1.Execute then
  begin
    ListBox1.Items.LoadFromFile(OpenDialog1.FileName);
    ShowMessage('sucess');
  end;
end;

procedure TFormBZ400.SaveFingerprint1Click(Sender: TObject);
begin   
  ListBox1.Items.SaveToFile('fingerprint.dat');
  ShowMessage('sucess');
end;

procedure TFormBZ400.btnEnableClick(Sender: TObject);
begin
  if CheckConnect then
  begin
      if CZKEM1.EnableDevice(1, Not chkEnable.Checked) then
      begin
        ShowMessage('OK');
      end;
  end;
end;

procedure TFormBZ400.btnClearAdminClick(Sender: TObject);
Var
  error:Integer;
begin
  if CheckConnect then
  begin
      if CZKEM1.ClearAdministrators(1) then
      begin
        ShowMessage('OK');
      end
      else
      begin
        CZKEM1.GetLastError(error);
        ShowMessage(IntToStr(error));
      end;
  end;
end;

procedure TFormBZ400.btnGetGeneralLogDataStrClick(Sender: TObject);
var
  dwEnrollNumber: Integer;
  dwVerifyMode: Integer;
  dwInOutMode: Integer;
  TimeStr: WideString; 
begin
  if CheckConnect then
  begin
    ListBox1.Items.Clear;
    while CZKEM1.GetGeneralLogDataStr(1,dwEnrollNumber,dwVerifyMode,dwInOutMode,TimeStr) do
    begin
      ListBox1.Items.Add(Format('%.09d, %d, %d, %s', [dwEnrollNumber,dwVerifyMode,dwInOutMode,TimeStr]));
    end;
  end;
end;

procedure TFormBZ400.btnGetAllUserInfoClick(Sender: TObject);
Var
  dwEnrollNumber: Integer;
  strName,Password, CardNo: WideString;
  Privilege: Integer;
  Enabled: WordBool;
begin
  if CheckConnect then
  begin
    ListBox1.Items.Clear;
    while CZKEM1.GetAllUserInfo(1, dwEnrollNumber, strName, Password,Privilege,Enabled) do
    begin
      CZKEM1.GetStrCardNumber(CardNo);
      ListBox1.Items.Add(Format('UserID:%.09d, Privilege:%d, Name:%.10s, Password:%.8s, CardNo:%.10s', [dwEnrollNumber,Privilege,strName,Password, CardNo]));
    end;
  end;
end;

procedure TFormBZ400.btnSetUserInfoClick(Sender: TObject);
begin
  if edtUserID.Text = '' then
  begin
    ShowMessage('Please Input User ID');
    edtUserID.SetFocus;
    Exit;
  end;

  try
    StrToInt(edtUserID.Text);
  except
    ShowMessage('User ID error');
    edtUserID.SetFocus;
    Exit;
  end;

  try
    StrToInt(edtBWPassword.Text);
  except
    ShowMessage('Password error');
    edtBWPassword.SetFocus;
    Exit;
  end;

  try
    StrToInt(edtBWCardNo.Text);
  except
    ShowMessage('Card No error');
    edtBWCardNo.SetFocus;
    Exit;
  end;


  if CheckConnect then
  begin
    if edtBWCardNo.Text <> '' then CZKEM1.SetStrCardNumber(edtBWCardNo.Text);
    if CZKEM1.SetUserInfo(1, StrToIntDef(edtUserID.Text, 1), edtBWName.Text, edtBWPassword.Text, 0, True) then
      ShowMessage('Uploaded Sucessfully!')
    else ShowMessage('Failed to Upload');
  end;
end;

procedure TFormBZ400.btnDeleteEnrollDataClick(Sender: TObject);
begin
  if edtUserID.Text = '' then
  begin
    ShowMessage('Please Input User ID');
    edtUserID.SetFocus;
    Exit;
  end;

  try
    StrToInt(edtUserID.Text);
  except
    ShowMessage('User ID error');
    edtUserID.SetFocus;
    Exit;
  end;

  if CheckConnect then
  begin
    if CZKEM1.DeleteEnrollData(1, StrToInt(edtUserID.Text), 1, 12) then
      ShowMessage('Deleted Sucessfully')
    else ShowMessage('Failed to delete');

  end;
end;

procedure TFormBZ400.btnGetUserTmpStrClick(Sender: TObject);
Var
  TmpData:WideString;
  TmpLength:Integer;
begin
  if edtUserID.Text = '' then
  begin
    ShowMessage('Please Input User ID');
    edtUserID.SetFocus;
    Exit;
  end;

  try
    StrToInt(edtUserID.Text);
  except
    ShowMessage('User ID error');
    edtUserID.SetFocus;
    Exit;
  end;

  if CheckConnect then
  begin
    if CZKEM1.GetUserTmpStr(1, StrToInt(edtUserID.Text), StrToInt(edtBWIndex.Text), TmpData, TmpLength) then
    begin
      ListBox1.Items.Add(TmpData);
      ShowMessage('Fingerprint template downloaded Sucessfully')
    end
    else ShowMessage('Failed to downloaded');
  end;
end;

procedure TFormBZ400.btnDelUserTmpClick(Sender: TObject);
begin
  if edtUserID.Text = '' then
  begin
    ShowMessage('Please Input User ID');
    edtUserID.SetFocus;
    Exit;
  end;

  try
    StrToInt(edtUserID.Text);
  except
    ShowMessage('User ID error');
    edtUserID.SetFocus;
    Exit;
  end;

  if CheckConnect then
  begin
    ShowMessage(Format('User ID: %s, Finger:%s will be delete', [edtUserID.Text, edtBWIndex.Text]));
    if CZKEM1.DelUserTmp(1, StrToInt(edtUserID.Text), StrToInt(edtBWIndex.Text)) then
      ShowMessage('Sucess')
    else ShowMessage('Falied to operate');  
  end;
end;

procedure TFormBZ400.btnClearListClick(Sender: TObject);
begin
  ListBox1.Items.Clear;
end;

procedure TFormBZ400.btnSetUserTmpStrClick(Sender: TObject);
Var
  strTemp:WideString;
  i, count:Integer;
begin
  if edtUserID.Text = '' then
  begin
    ShowMessage('Please Input User ID');
    edtUserID.SetFocus;
    Exit;
  end;

  try
    StrToInt(edtUserID.Text);
  except
    ShowMessage('User ID error');
    edtUserID.SetFocus;
    Exit;
  end;

  count := 0;
  for i := 0 to ListBox1.Count-1 do
  begin
    strTemp := ListBox1.Items[i];
    if CZKEM1.SetUserTmpStr(1, StrToInt(edtUserID.Text), i, strTemp) then
    begin
      Inc(count);
      Application.ProcessMessages;
    end;
  end;
  ShowMessage(Format('User ID: %s, already uploaded %d templates', [edtUserID.Text, count]));
end;

procedure TFormBZ400.btnSetDeviceTimeClick(Sender: TObject);
Var
  i, j, k:Integer;
begin
  j :=0;
  k := StrToInt(edtPosition.Text);
  ProgressBar1.Max := k;
  if CheckConnect then
  begin
    for i:= 0 to k do
    begin
      if CZKEM1.SetDeviceTime(1) then Inc(j);
      ProgressBar1.StepIt;
      Application.ProcessMessages;
      DelayTime(2);
    end;

    if CZKEM1.SetDeviceTime(1) then ShowMessage(Format('Sucess=%d', [j+1]))
    else ShowMessage('Failed to set');
  end;
end;

procedure TFormBZ400.btnGetUnlockPwdClick(Sender: TObject);
Var
  pwd:WideString;
begin

  if CZKEM1.GetUnlockPwd(edtUnlockSerialNumber.Text, pwd) then
  begin
    ShowMessage(pwd);
  end;

end;

procedure TFormBZ400.btnGetSysOptionsClick(Sender: TObject);
Var
  strValue:WideString;
begin
  if CheckConnect then
  begin
    if CZKEM1.GetSysOption(1, edtParamName.Text, strValue) then
    begin
      ShowMessage(strValue);
    end
    else ShowMessage('Error');
  end;
end;

procedure TFormBZ400.btnSetSysOptionsClick(Sender: TObject);
begin
  if CheckConnect then
  begin
    if CZKEM1.SetSysOption(1, edtParamName.Text, edtParaValue.Text) then
    begin
      ShowMessage('OK');
    end
    else ShowMessage('Error');
  end;
end;

procedure TFormBZ400.btnGetMRPTotalClick(Sender: TObject);
Var
  value:Integer;
begin
  value := 0;

  if CheckConnect then
  begin
    if CZKEM1.GetMRPTotal(1, StrToInt(ComboBox1.Text), value) then
    begin
      edtMRPTotal.Text := IntToStr(value);
    end
    else ShowMessage('Error');
  end;
  
end;

procedure TFormBZ400.btnSetDaylightClick(Sender: TObject);
Var
  iSupport:Integer;
  StartDate,EndDate:WideString;
begin
  try
    StartDate:= Format('%0.2d%0.2d%0.2d%0.2d', [StrToInt(edtM1.Text), StrToInt(edtD1.Text), StrToInt(edtTime1.Text),StrToInt(edtTime2.Text)]);
    EndDate:= Format('%0.2d%0.2d%0.2d%0.2d', [StrToInt(edtM2.Text), StrToInt(edtD2.Text), StrToInt(edtTime3.Text),StrToInt(edtTime4.Text)]);
  except
    ShowMessage('Format error!');
  end;

  if CheckConnect then
  begin
    if CheckBox1.Checked then iSupport:=1 else iSupport:=0;

    if CZKEM1.SetDaylight(1, iSupport, StartDate, EndDate) then ShowMessage('Success') else ShowMessage('Error');
  end;
end;

procedure TFormBZ400.btnGetDaylightClick(Sender: TObject);
Var
  iSupport:Integer;
  StartDate,EndDate:WideString;
begin
   if CheckConnect then
   begin
      if CZKEM1.GetDaylight(1, iSupport, StartDate, EndDate) then
        ShowMessage(Format('Support:%d, Start:%s, End:%s', [iSupport, StartDate, EndDate]))
      else ShowMessage('Error');
   end;
end;

procedure TFormBZ400.btnRefreshOptionsClick(Sender: TObject);
begin

  if CheckConnect then
  begin
    if CZKEM1.RefreshOptions(1) then ShowMessage('success')
    else ShowMessage('Failed');
  end;

end;

procedure TFormBZ400.btnTestingClick(Sender: TObject);
Var
  dwYear: Integer;
  dwMonth: Integer;
  dwDay: Integer;
  dwHour: Integer;
  dwMinute: Integer;
  dwSecond: Integer;

  NSR: WideString;
  PIS: WideString;

  //time1, time2: string;
  iCount, lastno:Cardinal;
  iErr: Integer;
begin

  lastno:=0;
  if CheckConnect then
  begin
    bStop := False;
    CZKEM1.SetSeekPosition(1, StrToInt(edtPosition.Text));
    while TRUE do
    begin
      ProgressBar1.Max := 2000;
      ProgressBar1.Position := 0;

      ListBox1.Clear;
      iCount:= 0;

      while (Not bStop) and CZKEM1.GetAttLogs(1, NSR, PIS, dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond) do
      begin
        ListBox1.Items.Add(Format('NSR:%0.9d         PIS:%s        %0.4d-%0.2d-%0.2d %0.2d:%0.2d:%0.2d', [StrToInt(NSR),PIS,dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond]));
        Inc(iCount);
        ProgressBar1.Position := iCount;
        lblCount.Caption := Format('Count:%d', [iCount]);
        Application.ProcessMessages;
        if ListBox1.Items.Count >= 10000 then ListBox1.Items.Clear;
        lastno := StrToInt(NSR);
      end;
      ProgressBar1.Position := ProgressBar1.Max;
      if bStop then break;

      CZKEM1.GetLastError(iErr);
      if  iErr= -2 then
      begin
        btnConnectClick(btnConnect);  //disconnect;
      end
      else  if  iErr= 0 then
      begin
        ShowMessage('No last record');
        break;
      end;

      btnConnectClick(btnConnect);  //connect again.
      Application.ProcessMessages;
      CZKEM1.SetSeekPosition(1, lastno);

    end;
  end;

end;

procedure TFormBZ400.btnUnlockClick(Sender: TObject);
begin

  if CheckConnect then
  begin
    if CZKEM1.UnlockREP(1) then ShowMessage('Sucess')
    else ShowMessage('Error');
  end;

end;

procedure TFormBZ400.btnSetUserFaceClick(Sender: TObject);
Var
  i, count, err:Integer;
  strTemp:Widestring;
begin
  count:=0;
  i:= 0;
  if CheckConnect then
  begin
      strTemp := ListBox1.Items[i];
      if CZKEM1.SetUserFaceStr(1, edtUserFace.Text, 50, strTemp, length(strTemp)) then
      begin
        Inc(count);
        Caption := Format('Count:%d', [Count]);
        ShowMessage('OK');
        Application.ProcessMessages;
      end
      else
      begin
        CZKEM1.GetLastError(err);
        ShowMessage(Format('error:%d', [err]));
      end; 
  end;
end;

procedure TFormBZ400.btnGetUserFaceClick(Sender: TObject);
var
  strTemp:WideString;
  len:Integer;
begin
  if edtUserFace.Text = '' then
  begin
    ShowMessage('Input User No.');
    Exit;
  end;

  if CheckConnect then
  begin
    ListBox1.Clear;
      if CZKEM1.GetUserFaceStr(1, edtUserFace.Text, 50, strTemp, len) then
      begin
        ListBox1.Items.Add(strTemp);
        Application.ProcessMessages;
      end;
      Caption := Format('Template size:%d, %d', [Length(strTemp), len]);
    
  end;
  ShowMessage(Caption);
end;

procedure TFormBZ400.Button1Click(Sender: TObject);
var  
  dwEnrollNumber: WideString;
  dwVerifyMode: Integer;
  dwInOutMode: Integer;
  dwYear: Integer;
  dwMonth: Integer;
  dwDay: Integer;
  dwHour: Integer;
  dwMinute: Integer;
  dwSecond: Integer;
  dwWorkcode: Integer;

begin
  if CheckConnect then
  begin
    ListBox1.Items.Clear;
    if CZKEM1.ReadGeneralLogData(1) then
    begin
      while CZKEM1.SSR_GetGeneralLogData(1,dwEnrollNumber,dwVerifyMode,dwInOutMode,dwYear,dwMonth,
                                       dwDay,dwHour,dwMinute,dwSecond,dwWorkcode) do
      begin
        ListBox1.Items.Add(Format('%04d-%02d-%02d %02d:%02d:%02d, %s', [dwYear,dwMonth,
                                       dwDay,dwHour,dwMinute,dwSecond, dwEnrollNumber]));
      end;
    end;
  end;
end;

end.
