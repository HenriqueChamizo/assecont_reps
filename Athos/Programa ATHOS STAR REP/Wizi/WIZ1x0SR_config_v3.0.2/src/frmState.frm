VERSION 5.00
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "mswinsck.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Begin VB.Form frmState 
   BorderStyle     =   1  '단일 고정
   Caption         =   "Status Window"
   ClientHeight    =   1275
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   4665
   ControlBox      =   0   'False
   ForeColor       =   &H00000000&
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1275
   ScaleWidth      =   4665
   StartUpPosition =   1  '소유자 가운데
   Begin VB.Timer Timer3 
      Enabled         =   0   'False
      Interval        =   3000
      Left            =   960
      Top             =   0
   End
   Begin VB.CommandButton cmdOK 
      Caption         =   "Close"
      Default         =   -1  'True
      Height          =   375
      Left            =   1560
      TabIndex        =   3
      Top             =   720
      Width           =   1575
   End
   Begin VB.Timer Timer1 
      Left            =   480
      Top             =   0
   End
   Begin VB.Timer Timer2 
      Enabled         =   0   'False
      Interval        =   100
      Left            =   0
      Top             =   0
   End
   Begin VB.PictureBox frameProgress 
      Appearance      =   0  '평면
      BackColor       =   &H00FFFFFF&
      ForeColor       =   &H80000008&
      Height          =   495
      Left            =   120
      ScaleHeight     =   465
      ScaleWidth      =   4425
      TabIndex        =   1
      Top             =   720
      Width           =   4455
      Begin MSComctlLib.ProgressBar ProgressFile 
         Height          =   255
         Left            =   120
         TabIndex        =   2
         ToolTipText     =   "Transfer File Progress Bar"
         Top             =   120
         Width           =   4215
         _ExtentX        =   7435
         _ExtentY        =   450
         _Version        =   393216
         Appearance      =   0
      End
   End
   Begin MSWinsockLib.Winsock winsockUP 
      Left            =   0
      Top             =   0
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
   Begin VB.Label lblTxt 
      Alignment       =   2  '가운데 맞춤
      Appearance      =   0  '평면
      BackColor       =   &H80000005&
      BackStyle       =   0  '투명
      Caption         =   "Waiting ..."
      BeginProperty Font 
         Name            =   "Arial Black"
         Size            =   14.25
         Charset         =   0
         Weight          =   900
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000008&
      Height          =   495
      Left            =   120
      MousePointer    =   11  '모래 시계
      TabIndex        =   0
      Top             =   120
      Width           =   4455
   End
End
Attribute VB_Name = "frmState"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Public ten As Integer
Public sockerr_en As Boolean


''''''''''''''''''''''''''''''''''''''''''''''''
' Show the message according to current state
'
''''''''''''''''''''''''''''''''''''''''''''''''
Public Sub ShowMsg()

    Dim s_interval As Integer
    
    Select Case ToolMode
    Case modeNone
            Call CloseWindow
            Exit Sub
    Case modeSearching
            lblTxt.Caption = "Processing ... "
            s_interval = 1000
    Case modeSetting
            lblTxt.Caption = "Processing ..."
            s_interval = 1000
    Case modeSettingComplete
            Call CloseWindow
            Exit Sub
    Case modeUploading
            lblTxt.Caption = "Processing ..."
            sockerr_en = True
            winsockUP.RemoteHost = destIP
            winsockUP.RemotePort = 1470
            winsockUP.Connect
            s_interval = 0
    Case modeUploadingComplete
            lblTxt.Caption = "Waiting ..."
'            s_interval = 30000
            s_interval = 100
    Case Else
    End Select
    
    cmdOK.Visible = False
    frameProgress.Visible = True
    ten = 0
    ProgressFile.Max = 10
    ProgressFile.Min = 0
    ProgressFile.Value = 0
    Timer1.Interval = s_interval
    Timer2.Interval = s_interval / 10
    Timer1.Enabled = True
    Timer2.Enabled = True
   
End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' Show the message complete the action.
'
''''''''''''''''''''''''''''''''''''''''''''''''
Sub CloseWindow()
    
    Timer1.Enabled = False
    Timer2.Enabled = False
    
    Select Case ToolMode
    Case modeNone
            lblTxt.Caption = "Invalid state"
    Case modeSearching
            If intBoardNum = 0 Then
                lblTxt.Caption = "No search result"
            Else
                lblTxt.Caption = "Complete searching"
            End If
    Case modeSetting
            lblTxt.Caption = "Fail to set"
    Case modeSettingComplete
            lblTxt.Caption = "Complete setting"
    Case modeUploading
            lblTxt.Caption = "Fail to upload"
    Case modeUploadingComplete
            lblTxt.Caption = "Complete uploading"
    Case Else
    End Select
    
    cmdOK.Visible = True
    frameProgress.Visible = False
    
End Sub

Private Sub cmdOK_Click()
    
    ToolMode = modeNone
    Unload Me

End Sub

Private Sub Form_Load()
    
    cmdOK.Visible = False
    frameProgress.Visible = True
    ten = 0
    ProgressFile.Max = 10
    ProgressFile.Min = 0
    ProgressFile.Value = 0
    Timer1.Interval = 0
    Timer2.Interval = 0
    Timer1.Enabled = False
    Timer2.Enabled = False
    Timer3.Enabled = False
    sockerr_en = False
    
End Sub

Private Sub Timer1_Timer()

    Timer1.Enabled = False
    Timer2.Enabled = False
    Call CloseWindow
    
End Sub


''''''''''''''''''''''''''''''''''''''''''''''''
' Processing for Progress bar control
'
''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub Timer2_Timer()
    
    If ten >= ProgressFile.Max Then
        ten = ProgressFile.Max
    Else
        ten = ten + 1
    End If
        
    ProgressFile.Value = ten
    
End Sub

Private Sub Timer3_Timer()
    
    Timer3.Enabled = False
    winsockUP.Close
    ToolMode = modeUploadingComplete
    Call ShowMsg

End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' Name : winsockUP_Connect
' Parameter : None
'
' Ready to uploading firmware
' Open File Open Dialog box for selecting new firmware file.
' Open selected file
' Send all contents of firmware to board
' Wait socket close event from board
'
''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub winsockUP_Connect()
On Error GoTo UP_ERROR

    Dim hSend, BSize, LF&
    Dim sendD() As Byte
    Dim FileSize As Long
    
    hSend = FreeFile
    Open strUploadFile For Binary Access Read As hSend
    
    FileSize = FileLen(strUploadFile)
    ProgressFile.Max = FileSize
    ProgressFile.Min = 0
    ProgressFile.Value = 0
    
    ReDim sendD(0 To 3) As Byte
    sendD(0) = (FileSize And &HFF000000) / &H1000000
    sendD(1) = (FileSize And &HFF0000) / &H10000
    sendD(2) = (FileSize And &HFF00) / &H100
    sendD(3) = (FileSize And &HFF)
    winsockUP.SendData sendD
    DoEvents
    Sleep (300)
    Erase sendD
    
    FileSize = 0
    ' read block data as send buf size.
    BSize = 1024
    If winsockUP.State = sckConnected Then
        LF& = LOF(hSend)
        Do Until LF& = Loc(hSend)
            ' until reached EOF
            If LF& - Loc(hSend) < BSize Then
                BSize = LF& - Loc(hSend)
            End If
            ' read data block
            ReDim sendD(0 To BSize - 1) As Byte
            Get hSend, , sendD
            winsockUP.SendData sendD
            DoEvents
            Erase sendD
            FileSize = FileSize + BSize
            ProgressFile.Value = FileSize
            Sleep (600)
        Loop
    End If
    Close hSend
    sockerr_en = False
    Timer3.Enabled = True
    Exit Sub
    
UP_ERROR:
    Close hSend
    ProgressFile.Value = 0
    Call CloseWindow
    
End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' Name : winsockUP_Close
' Parameter : None
'
' Complete uploading firmware
' Close uploading socket connection.
' Wait for 10 seconds for board's initialization
'
''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub winsockUP_Close()
    
    Timer3.Enabled = False
    winsockUP.Close
    ToolMode = modeUploadingComplete
    Call ShowMsg

End Sub

Private Sub winsockUP_Error(ByVal Number As Integer, Description As String, ByVal Scode As Long, ByVal Source As String, ByVal HelpFile As String, ByVal HelpContext As Long, CancelDisplay As Boolean)
    
    If sockerr_en Then
        winsockUP.Close
        Call CloseWindow
    End If

End Sub
