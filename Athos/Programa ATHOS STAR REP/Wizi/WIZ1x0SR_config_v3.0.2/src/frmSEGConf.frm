VERSION 5.00
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "mswinsck.ocx"
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCTL.OCX"
Object = "{F9043C88-F6F2-101A-A3C9-08002B2F49FB}#1.2#0"; "COMDLG32.OCX"
Begin VB.Form frmSEGConf 
   BorderStyle     =   1  '단일 고정
   ClientHeight    =   6945
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   8730
   BeginProperty Font 
      Name            =   "Courier New"
      Size            =   9
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   Icon            =   "frmSEGConf.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   463
   ScaleMode       =   3  '픽셀
   ScaleWidth      =   582
   StartUpPosition =   2  '화면 가운데
   Begin VB.TextBox txtConnect 
      Alignment       =   2  '가운데 맞춤
      Appearance      =   0  '평면
      BackColor       =   &H00FFFFFF&
      Enabled         =   0   'False
      Height          =   330
      Left            =   6360
      MaxLength       =   15
      TabIndex        =   35
      Top             =   120
      Width           =   2280
   End
   Begin VB.Frame FrameTab 
      BorderStyle     =   0  '없음
      BeginProperty Font 
         Name            =   "Courier New"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   3975
      Index           =   3
      Left            =   0
      TabIndex        =   22
      Top             =   7200
      Visible         =   0   'False
      Width           =   5535
      Begin VB.Frame Frame5 
         Caption         =   "Password (TCP Server)"
         Height          =   735
         Left            =   120
         TabIndex        =   72
         Top             =   2520
         Width           =   5295
         Begin VB.TextBox txtTCPPass 
            Alignment       =   2  '가운데 맞춤
            Height          =   270
            Left            =   2640
            MaxLength       =   8
            TabIndex        =   75
            Top             =   360
            Width           =   1215
         End
         Begin VB.CheckBox chkTCPPass 
            Caption         =   "Enable"
            Height          =   225
            Left            =   360
            TabIndex        =   73
            Top             =   375
            Width           =   975
         End
         Begin VB.Label Label28 
            Caption         =   "(Max 8 Bytes)"
            ForeColor       =   &H000000FF&
            Height          =   255
            Left            =   3840
            TabIndex        =   76
            Top             =   360
            Width           =   1395
         End
         Begin VB.Label Label27 
            Caption         =   "Password"
            Height          =   255
            Left            =   1740
            TabIndex        =   74
            Top             =   360
            Width           =   975
         End
      End
      Begin VB.Frame Frame3 
         Caption         =   "Serial Configuration"
         Height          =   735
         Left            =   120
         TabIndex        =   41
         Top             =   3240
         Width           =   5295
         Begin VB.TextBox TextSCfg 
            Alignment       =   2  '가운데 맞춤
            Height          =   240
            Index           =   2
            Left            =   3480
            MaxLength       =   2
            TabIndex        =   46
            Top             =   360
            Width           =   375
         End
         Begin VB.TextBox TextSCfg 
            Alignment       =   2  '가운데 맞춤
            Height          =   240
            Index           =   1
            Left            =   3000
            MaxLength       =   2
            TabIndex        =   45
            Top             =   360
            Width           =   375
         End
         Begin VB.TextBox TextSCfg 
            Alignment       =   2  '가운데 맞춤
            Height          =   240
            Index           =   0
            Left            =   2520
            MaxLength       =   2
            TabIndex        =   44
            Top             =   360
            Width           =   375
         End
         Begin VB.CheckBox CheckSCfg 
            Caption         =   "Enable"
            Height          =   255
            Left            =   360
            TabIndex        =   42
            Top             =   360
            Width           =   1095
         End
         Begin VB.Label Label29 
            Caption         =   "(in Hex)"
            ForeColor       =   &H000000FF&
            Height          =   255
            Left            =   3900
            TabIndex        =   77
            Top             =   360
            Width           =   1155
         End
         Begin VB.Label Label22 
            Caption         =   "Code"
            Height          =   255
            Left            =   1740
            TabIndex        =   43
            Top             =   360
            Width           =   615
         End
      End
      Begin VB.Frame Frame1 
         Caption         =   "Data Packing Condition"
         Height          =   1455
         Left            =   120
         TabIndex        =   28
         Top             =   1080
         Width           =   5295
         Begin VB.TextBox txtDChar 
            Alignment       =   1  '오른쪽 맞춤
            Height          =   270
            Left            =   1320
            MaxLength       =   2
            TabIndex        =   19
            Top             =   1080
            Width           =   840
         End
         Begin VB.TextBox txtDSize 
            Alignment       =   1  '오른쪽 맞춤
            Height          =   270
            Left            =   1320
            MaxLength       =   15
            TabIndex        =   18
            Top             =   720
            Width           =   840
         End
         Begin VB.TextBox txtDTime 
            Alignment       =   1  '오른쪽 맞춤
            Height          =   270
            Left            =   1320
            MaxLength       =   15
            TabIndex        =   17
            Top             =   360
            Width           =   840
         End
         Begin VB.Label Label11 
            Caption         =   "(0 ~ 65535 ms)"
            Height          =   255
            Left            =   2280
            TabIndex        =   34
            Top             =   360
            Width           =   1575
         End
         Begin VB.Label Label20 
            Caption         =   "(Hexacode)"
            Height          =   255
            Left            =   2280
            TabIndex        =   33
            Top             =   1080
            Width           =   1215
         End
         Begin VB.Label Label23 
            Caption         =   "(0 ~ 255 Byte)"
            Height          =   255
            Left            =   2280
            TabIndex        =   32
            Top             =   720
            Width           =   1575
         End
         Begin VB.Label Label19 
            Caption         =   "Char"
            BeginProperty Font 
               Name            =   "Courier New"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   720
            TabIndex        =   31
            Top             =   1080
            Width           =   495
         End
         Begin VB.Label Label17 
            Caption         =   "Size"
            BeginProperty Font 
               Name            =   "Courier New"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   720
            TabIndex        =   30
            Top             =   720
            Width           =   495
         End
         Begin VB.Label Label16 
            Caption         =   "Time"
            BeginProperty Font 
               Name            =   "Courier New"
               Size            =   9.75
               Charset         =   0
               Weight          =   400
               Underline       =   0   'False
               Italic          =   0   'False
               Strikethrough   =   0   'False
            EndProperty
            Height          =   255
            Left            =   720
            TabIndex        =   29
            Top             =   360
            Width           =   495
         End
      End
      Begin VB.TextBox txtITime 
         Alignment       =   1  '오른쪽 맞춤
         Height          =   270
         Left            =   2160
         MaxLength       =   15
         TabIndex        =   16
         Top             =   120
         Width           =   840
      End
      Begin VB.Label Label25 
         Caption         =   "(0 ~ 65535 sec)"
         Height          =   255
         Left            =   3120
         TabIndex        =   27
         Top             =   120
         Width           =   1575
      End
      Begin VB.Label Label21 
         Caption         =   "* Closes socket connection, if there is      no transmission during this time."
         Height          =   495
         Left            =   120
         TabIndex        =   26
         Top             =   480
         Width           =   4455
      End
      Begin VB.Label Label12 
         Caption         =   "Inactivity time"
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   120
         TabIndex        =   25
         Top             =   120
         Width           =   1935
      End
   End
   Begin VB.Frame FrameTab 
      BorderStyle     =   0  '없음
      BeginProperty Font 
         Name            =   "Courier New"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   4935
      Index           =   1
      Left            =   3000
      TabIndex        =   6
      Top             =   960
      Visible         =   0   'False
      Width           =   5535
      Begin VB.Frame Frame4 
         Caption         =   "IP Configuration Method"
         Height          =   735
         Left            =   0
         TabIndex        =   68
         Top             =   120
         Width           =   5415
         Begin VB.OptionButton Option1 
            Caption         =   "Static"
            Height          =   225
            Left            =   240
            TabIndex        =   71
            Top             =   360
            Value           =   -1  'True
            Width           =   975
         End
         Begin VB.OptionButton Option2 
            Caption         =   "DHCP"
            Height          =   225
            Left            =   1800
            TabIndex        =   70
            Top             =   360
            Width           =   735
         End
         Begin VB.OptionButton Option3 
            Caption         =   "PPPoE"
            Height          =   225
            Left            =   3120
            TabIndex        =   69
            Top             =   360
            Width           =   855
         End
      End
      Begin VB.TextBox txtServerIP 
         Height          =   285
         Left            =   1560
         MaxLength       =   15
         TabIndex        =   66
         Top             =   3000
         Width           =   2040
      End
      Begin VB.TextBox txtGW 
         Height          =   285
         Left            =   1560
         MaxLength       =   15
         TabIndex        =   65
         Top             =   1680
         Width           =   2040
      End
      Begin VB.TextBox txtSubnet 
         Height          =   285
         Left            =   1560
         MaxLength       =   15
         TabIndex        =   64
         Top             =   1320
         Width           =   2040
      End
      Begin VB.TextBox txtIP 
         Height          =   285
         Left            =   1560
         MaxLength       =   15
         TabIndex        =   63
         Top             =   960
         Width           =   2040
      End
      Begin VB.TextBox txtPass 
         BackColor       =   &H80000011&
         Enabled         =   0   'False
         Height          =   285
         IMEMode         =   3  '사용 못함
         Left            =   1560
         PasswordChar    =   "*"
         TabIndex        =   62
         Top             =   2520
         Width           =   2055
      End
      Begin VB.TextBox txtID 
         BackColor       =   &H80000011&
         Enabled         =   0   'False
         Height          =   285
         Left            =   1560
         TabIndex        =   61
         Top             =   2160
         Width           =   2055
      End
      Begin VB.CheckBox chkUDPMode 
         Appearance      =   0  '평면
         Caption         =   "Use UDP mode"
         ForeColor       =   &H80000008&
         Height          =   255
         Left            =   3600
         TabIndex        =   58
         Top             =   3720
         Width           =   1575
      End
      Begin VB.Frame Frame2 
         Caption         =   "Operation Mode"
         Height          =   735
         Left            =   0
         TabIndex        =   54
         Top             =   3360
         Width           =   5415
         Begin VB.OptionButton optClientMode 
            Caption         =   "Client"
            Height          =   225
            Index           =   0
            Left            =   120
            TabIndex        =   57
            Top             =   360
            Width           =   975
         End
         Begin VB.OptionButton optClientMode 
            Caption         =   "Mixed"
            Height          =   225
            Index           =   1
            Left            =   2280
            TabIndex        =   56
            Top             =   360
            Value           =   -1  'True
            Width           =   975
         End
         Begin VB.OptionButton optClientMode 
            Caption         =   "Server"
            Height          =   225
            Index           =   2
            Left            =   1200
            TabIndex        =   55
            Top             =   360
            Width           =   975
         End
      End
      Begin VB.TextBox txtServerPort 
         Height          =   285
         Left            =   4560
         MaxLength       =   15
         TabIndex        =   48
         Top             =   3000
         Width           =   840
      End
      Begin VB.TextBox txtPort 
         Height          =   285
         Left            =   4560
         MaxLength       =   15
         TabIndex        =   47
         Top             =   960
         Width           =   840
      End
      Begin VB.CheckBox ChkDNS 
         Appearance      =   0  '평면
         Caption         =   "Use DNS"
         ForeColor       =   &H80000008&
         Height          =   255
         Left            =   120
         TabIndex        =   40
         Top             =   4200
         Width           =   1215
      End
      Begin VB.TextBox txtServer_Domain 
         Enabled         =   0   'False
         Height          =   285
         Left            =   1560
         TabIndex        =   37
         Top             =   4560
         Width           =   3855
      End
      Begin VB.TextBox txt_DNS_ServerIP 
         Enabled         =   0   'False
         Height          =   285
         Left            =   3360
         TabIndex        =   36
         Top             =   4200
         Width           =   2055
      End
      Begin VB.Label Label10 
         Caption         =   "Server IP "
         Height          =   255
         Left            =   120
         TabIndex        =   67
         Top             =   3015
         Width           =   1215
      End
      Begin VB.Label Label26 
         Caption         =   "Password"
         Height          =   255
         Left            =   120
         TabIndex        =   60
         Top             =   2520
         Width           =   975
      End
      Begin VB.Label Label24 
         Caption         =   "PPPoE ID"
         Height          =   255
         Left            =   120
         TabIndex        =   59
         Top             =   2160
         Width           =   975
      End
      Begin VB.Label Label14 
         Alignment       =   2  '가운데 맞춤
         Caption         =   "Port"
         Height          =   255
         Left            =   3840
         TabIndex        =   53
         Top             =   3015
         Width           =   615
      End
      Begin VB.Label Label13 
         Alignment       =   2  '가운데 맞춤
         Caption         =   "Port"
         Height          =   255
         Left            =   3840
         TabIndex        =   52
         Top             =   975
         Width           =   615
      End
      Begin VB.Label Label1 
         Caption         =   "Local IP "
         Height          =   255
         Left            =   120
         TabIndex        =   51
         Top             =   975
         Width           =   1335
      End
      Begin VB.Label Label3 
         Caption         =   "Subnet "
         Height          =   255
         Left            =   120
         TabIndex        =   50
         Top             =   1335
         Width           =   1335
      End
      Begin VB.Label Label4 
         Caption         =   "Gateway "
         Height          =   255
         Left            =   120
         TabIndex        =   49
         Top             =   1695
         Width           =   1335
      End
      Begin VB.Label Label18 
         Caption         =   "Domain Name"
         Enabled         =   0   'False
         Height          =   255
         Left            =   120
         TabIndex        =   39
         Top             =   4560
         Width           =   1335
      End
      Begin VB.Label Label15 
         Caption         =   "DNS Server IP"
         Enabled         =   0   'False
         Height          =   255
         Left            =   1920
         TabIndex        =   38
         Top             =   4200
         Width           =   1455
      End
   End
   Begin VB.Frame FrameTab 
      BorderStyle     =   0  '없음
      BeginProperty Font 
         Name            =   "Courier New"
         Size            =   9.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   3975
      Index           =   2
      Left            =   3000
      TabIndex        =   7
      Top             =   1200
      Visible         =   0   'False
      Width           =   5535
      Begin VB.ComboBox cboDataBits 
         Appearance      =   0  '평면
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Left            =   1320
         Style           =   2  '드롭다운 목록
         TabIndex        =   9
         Top             =   840
         Width           =   1620
      End
      Begin VB.ComboBox cboParity 
         Appearance      =   0  '평면
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Left            =   1320
         Style           =   2  '드롭다운 목록
         TabIndex        =   10
         Top             =   1440
         Width           =   1620
      End
      Begin VB.ComboBox cboStopBits 
         Appearance      =   0  '평면
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Left            =   1320
         Style           =   2  '드롭다운 목록
         TabIndex        =   11
         Top             =   2040
         Width           =   1620
      End
      Begin VB.ComboBox cboSpeed 
         Appearance      =   0  '평면
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         ItemData        =   "frmSEGConf.frx":0E42
         Left            =   1320
         List            =   "frmSEGConf.frx":0E44
         Style           =   2  '드롭다운 목록
         TabIndex        =   8
         Top             =   240
         Width           =   1620
      End
      Begin VB.ComboBox cboFlow 
         Appearance      =   0  '평면
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   360
         Left            =   1320
         Style           =   2  '드롭다운 목록
         TabIndex        =   13
         Top             =   2640
         Width           =   1620
      End
      Begin VB.Label Label9 
         Alignment       =   1  '오른쪽 맞춤
         Caption         =   "DataBit"
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   300
         Left            =   120
         TabIndex        =   21
         Top             =   840
         Width           =   1005
      End
      Begin VB.Label Label8 
         Alignment       =   1  '오른쪽 맞춤
         Caption         =   "Parity"
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   300
         Left            =   120
         TabIndex        =   20
         Top             =   1440
         Width           =   1005
      End
      Begin VB.Label Label7 
         Alignment       =   1  '오른쪽 맞춤
         Caption         =   "Stop Bit"
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   300
         Left            =   120
         TabIndex        =   15
         Top             =   2040
         Width           =   1005
      End
      Begin VB.Label Label6 
         Alignment       =   1  '오른쪽 맞춤
         Caption         =   "Speed"
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   300
         Left            =   120
         TabIndex        =   14
         Top             =   240
         Width           =   1005
      End
      Begin VB.Label Label2 
         Alignment       =   1  '오른쪽 맞춤
         Caption         =   "Flow"
         BeginProperty Font 
            Name            =   "Courier New"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   300
         Left            =   600
         TabIndex        =   12
         Top             =   2640
         Width           =   525
      End
   End
   Begin VB.CheckBox chkDirect 
      Appearance      =   0  '평면
      Caption         =   "Direct IP Search"
      ForeColor       =   &H80000008&
      Height          =   255
      Left            =   180
      TabIndex        =   24
      Top             =   6120
      Width           =   2415
   End
   Begin VB.TextBox txtDirectIP 
      Height          =   360
      Left            =   180
      MaxLength       =   15
      TabIndex        =   23
      Top             =   6480
      Width           =   2475
   End
   Begin MSWinsockLib.Winsock WinsockDirect 
      Left            =   9120
      Top             =   240
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
   Begin MSComctlLib.ImageList imlListView 
      Left            =   9120
      Top             =   2040
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   16
      ImageHeight     =   16
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   1
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmSEGConf.frx":0E46
            Key             =   ""
         EndProperty
      EndProperty
   End
   Begin MSComDlg.CommonDialog OpenLog 
      Left            =   9120
      Top             =   2760
      _ExtentX        =   847
      _ExtentY        =   847
      _Version        =   393216
   End
   Begin VB.CheckBox chkDebug 
      Appearance      =   0  '평면
      Caption         =   "Enable Serial Debug Mode"
      ForeColor       =   &H80000008&
      Height          =   255
      Left            =   2880
      TabIndex        =   3
      Top             =   120
      Width           =   3375
   End
   Begin VB.TextBox txtVersion 
      Alignment       =   2  '가운데 맞춤
      Appearance      =   0  '평면
      BackColor       =   &H00FFFFFF&
      Enabled         =   0   'False
      Height          =   330
      Left            =   1440
      MaxLength       =   15
      TabIndex        =   1
      Top             =   120
      Width           =   1080
   End
   Begin MSComctlLib.ImageList imlToolbarIcons 
      Left            =   5760
      Top             =   60
      _ExtentX        =   1005
      _ExtentY        =   1005
      BackColor       =   -2147483643
      ImageWidth      =   32
      ImageHeight     =   32
      MaskColor       =   12632256
      _Version        =   393216
      BeginProperty Images {2C247F25-8591-11D1-B16A-00C0F0283628} 
         NumListImages   =   7
         BeginProperty ListImage1 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmSEGConf.frx":1720
            Key             =   "IMG1"
         EndProperty
         BeginProperty ListImage2 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmSEGConf.frx":1FFA
            Key             =   "IMG2"
         EndProperty
         BeginProperty ListImage3 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmSEGConf.frx":28D4
            Key             =   "IMG3"
         EndProperty
         BeginProperty ListImage4 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmSEGConf.frx":31AE
            Key             =   "IMG4"
         EndProperty
         BeginProperty ListImage5 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmSEGConf.frx":3A88
            Key             =   "IMG5"
         EndProperty
         BeginProperty ListImage6 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmSEGConf.frx":41B7
            Key             =   "IMG6"
         EndProperty
         BeginProperty ListImage7 {2C247F27-8591-11D1-B16A-00C0F0283628} 
            Picture         =   "frmSEGConf.frx":AA19
            Key             =   "IMG7"
         EndProperty
      EndProperty
   End
   Begin MSWinsockLib.Winsock WinsockUDP 
      Left            =   9120
      Top             =   720
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
      Protocol        =   1
   End
   Begin MSComctlLib.Toolbar Toolbar1 
      Height          =   765
      Left            =   3720
      TabIndex        =   4
      Top             =   6120
      Width           =   4875
      _ExtentX        =   8599
      _ExtentY        =   1349
      ButtonWidth     =   1349
      ButtonHeight    =   1349
      AllowCustomize  =   0   'False
      Style           =   1
      ImageList       =   "imlToolbarIcons"
      _Version        =   393216
      BeginProperty Buttons {66833FE8-8583-11D1-B16A-00C0F0283628} 
         NumButtons      =   8
         BeginProperty Button1 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "Search"
            Key             =   "SearchBoard"
            Object.ToolTipText     =   "Search Board"
            ImageKey        =   "IMG1"
         EndProperty
         BeginProperty Button2 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "Setting"
            Key             =   "SettingBoard"
            Object.ToolTipText     =   "Setting Board Information"
            ImageKey        =   "IMG2"
         EndProperty
         BeginProperty Button3 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "Upload"
            Key             =   "Upload"
            Object.ToolTipText     =   "Upload Firmware"
            ImageKey        =   "IMG3"
         EndProperty
         BeginProperty Button4 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button5 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "Ping"
            Key             =   "ping"
            Object.ToolTipText     =   "Ping command"
            ImageKey        =   "IMG6"
         EndProperty
         BeginProperty Button6 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "Firewall"
            Key             =   "firewall"
            Description     =   "Windows firewall"
            Object.ToolTipText     =   "Windows Firewall Setting"
            ImageKey        =   "IMG7"
         EndProperty
         BeginProperty Button7 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Style           =   3
         EndProperty
         BeginProperty Button8 {66833FEA-8583-11D1-B16A-00C0F0283628} 
            Caption         =   "Exit"
            Key             =   "Exit"
            Object.ToolTipText     =   "Exit"
            ImageKey        =   "IMG4"
         EndProperty
      EndProperty
   End
   Begin MSComctlLib.ListView ListBoards 
      Height          =   5415
      Left            =   120
      TabIndex        =   0
      Top             =   600
      Width           =   2655
      _ExtentX        =   4683
      _ExtentY        =   9551
      View            =   3
      LabelEdit       =   1
      LabelWrap       =   -1  'True
      HideSelection   =   0   'False
      FullRowSelect   =   -1  'True
      _Version        =   393217
      SmallIcons      =   "imlListView"
      ForeColor       =   -2147483640
      BackColor       =   -2147483643
      BorderStyle     =   1
      Appearance      =   0
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Courier New"
         Size            =   9
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      NumItems        =   0
   End
   Begin MSComctlLib.TabStrip TabStrip1 
      Height          =   5415
      Left            =   2880
      TabIndex        =   5
      Top             =   600
      Width           =   5775
      _ExtentX        =   10186
      _ExtentY        =   9551
      _Version        =   393216
      BeginProperty Tabs {1EFB6598-857C-11D1-B16A-00C0F0283628} 
         NumTabs         =   3
         BeginProperty Tab1 {1EFB659A-857C-11D1-B16A-00C0F0283628} 
            Caption         =   "Network"
            Key             =   "Network"
            Object.Tag             =   "Network"
            Object.ToolTipText     =   "Network Configuration"
            ImageVarType    =   2
         EndProperty
         BeginProperty Tab2 {1EFB659A-857C-11D1-B16A-00C0F0283628} 
            Caption         =   "Serial"
            Key             =   "Serial"
            Object.Tag             =   "Serial"
            Object.ToolTipText     =   "Serial Configuration"
            ImageVarType    =   2
         EndProperty
         BeginProperty Tab3 {1EFB659A-857C-11D1-B16A-00C0F0283628} 
            Caption         =   "Option"
            Key             =   "Option"
            Object.Tag             =   "Option"
            Object.ToolTipText     =   "Others Configuration"
            ImageVarType    =   2
         EndProperty
      EndProperty
   End
   Begin VB.Label Label5 
      Alignment       =   2  '가운데 맞춤
      Caption         =   "Version"
      Height          =   255
      Left            =   120
      TabIndex        =   2
      Top             =   120
      Width           =   1215
   End
End
Attribute VB_Name = "frmSEGConf"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
'--------------------------------------------------
' WIZSEGConf - WIZ1x0SR-WIZ105SR Configuration Tool
'
' Copyright (c) 2010, WIZnet Inc.
'--------------------------------------------------
Option Explicit
'''''''''''''''''''''''''''''''''''''''''''''

'''''''''''''''''''''''''''''''''''''''''''''''''''''''
Private mintCurFrame As Integer ' Current TAB Frame

Private Sub ChkDNS_Click()
  If ChkDNS.Value = 1 Then
    txt_DNS_ServerIP.Enabled = True
    txtServer_Domain.Enabled = True
    Label18.Enabled = True
    Label15.Enabled = True
    Label10.Enabled = False
    txtServerIP.Enabled = False
  End If
  
  If ChkDNS.Value = 0 Then
    txt_DNS_ServerIP.Enabled = False
    txtServer_Domain.Enabled = False
    Label18.Enabled = False
    Label15.Enabled = False
    
    Label10.Enabled = True
    txtServerIP.Enabled = True
  End If

End Sub

Private Sub chkUDPMode_Click()
    If chkUDPMode.Value = 1 Then
        optClientMode(0).Enabled = False
        optClientMode(1).Enabled = False
        optClientMode(2).Enabled = False
    Else
        optClientMode(0).Enabled = True
        optClientMode(1).Enabled = True
        optClientMode(2).Enabled = True
    End If
    
End Sub

Private Sub Option1_Click()
    
    If Option1.Value = True Then
        txtIP.Enabled = True
        txtIP.BackColor = &H80000005
        txtPort.Enabled = True
        txtPort.BackColor = &H80000005
        txtSubnet.Enabled = True
        txtSubnet.BackColor = &H80000005
        txtGW.Enabled = True
        txtGW.BackColor = &H80000005
    
        txtID.Enabled = False
        txtID.BackColor = &H80000011
        txtPass.Enabled = False
        txtPass.BackColor = &H80000011
    End If

End Sub

Private Sub Option2_Click()

    If Option2.Value = True Then
        txtIP.Enabled = False
        txtIP.BackColor = &H80000011
        'txtPort.Enabled = False
        'txtPort.BackColor = &H80000011
        txtSubnet.Enabled = False
        txtSubnet.BackColor = &H80000011
        txtGW.Enabled = False
        txtGW.BackColor = &H80000011
    
        txtID.Enabled = False
        txtID.BackColor = &H80000011
        txtPass.Enabled = False
        txtPass.BackColor = &H80000011
    End If
    
End Sub

Private Sub Option3_Click()
    
    If Option3.Value = True Then
        txtIP.Enabled = False
        txtIP.BackColor = &H80000011
        'txtPort.Enabled = False
        'txtPort.BackColor = &H80000011
        txtSubnet.Enabled = False
        txtSubnet.BackColor = &H80000011
        txtGW.Enabled = False
        txtGW.BackColor = &H80000011
        
        txtID.Enabled = True
        txtID.BackColor = &H80000005
        txtPass.Enabled = True
        txtPass.BackColor = &H80000005
    End If
    
End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' TAB select
'
''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub TabStrip1_Click()

   If TabStrip1.SelectedItem.Index = mintCurFrame Then Exit Sub
   
   ' View Selected Frame
   FrameTab(TabStrip1.SelectedItem.Index).Visible = True
   FrameTab(mintCurFrame).Visible = False
   mintCurFrame = TabStrip1.SelectedItem.Index

End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' Name : ShowMsgWindow
' Parameter : none
'
' Show the message for process the action.
'
''''''''''''''''''''''''''''''''''''''''''''''''
Sub ShowMsgWindow()
    
    Me.Enabled = False
    
    frmState.ShowMsg
    frmState.Show vbModal, Me
    
    Me.Enabled = True
    
End Sub
        

''''''''''''''''''''''''''''''''''''''''''''''''
' Name : BoardAdd
' Parameter : newStr() is string of board's configuration data
'
' Add Board with receiving new board's configuration data
' save total count of boards to "intBoardNum" variable.
' save configuration data to "colBoards" collection
'
''''''''''''''''''''''''''''''''''''''''''''''''
Sub BoardAdd(newStr() As Byte)
On Error GoTo e_go

    Dim mac As String
    Dim i As Integer
    
    ' making mac address string key
    ' ex) 00:44:34:EA:3A:F0
    mac = ""
    For i = 0 To 5
        If Len(Hex(newStr(i))) = 1 Then
            mac = mac & "0" & Hex(newStr(i)) & ":"
        Else
            mac = mac & Hex(newStr(i)) & ":"
        End If
    Next i
    mac = Left(mac, Len(mac) - 1)
    
    ' Add Board entity by using mac .
    colBoards.Add newStr, mac
    ' add list view
    frmSEGConf.ListBoards.ListItems.Add intBoardNum, mac, mac
    frmSEGConf.ListBoards.ListItems.Item(intBoardNum).SmallIcon = 1
            
    ' Automatically select the first row of ListView
    If intBoardNum = 1 Then
        Call frmSEGConf.ListBoards_FirstRowSelect
        'frmSEGConf.ListBoards.SetFocus
    End If
    
    intBoardNum = intBoardNum + 1

e_go:
End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' Name : BoardUpdate
' Parameter : newStr() is string of board's configuration data
'
' Update Board's configuration data with receiving data
' delete previous data
' add new board's data
'
''''''''''''''''''''''''''''''''''''''''''''''''
Sub BoardUpdate(newStr() As Byte)
On Error GoTo U_ERROR

    Dim newInfo() As Byte
    ReDim newInfo(0 To Len(BoardInfo) - 1) As Byte
    
    ' Verify message
    If newStr(0) = BoardInfo.mac(0) And _
        newStr(1) = BoardInfo.mac(1) And _
        newStr(2) = BoardInfo.mac(2) And _
        newStr(3) = BoardInfo.mac(3) And _
        newStr(4) = BoardInfo.mac(4) And _
        newStr(5) = BoardInfo.mac(5) Then
        
        ' Updatae the item
        colBoards.Remove BoardKey
        CopyMemory newInfo(0), BoardInfo, Len(BoardInfo)
        colBoards.Add newInfo, BoardKey
        ToolMode = modeSettingComplete
        
    End If

U_ERROR:
    Erase newInfo
    
End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' Name : BoardRemove
' Parameter : None
'
' Delete all Board's data from "colBoards" collection
' Board's ListBox clears
' "intBoardNum" variable sets '0'
'
''''''''''''''''''''''''''''''''''''''''''''''''
Sub BoardRemove()
    
    Dim num As Integer
    
    ' Set false the flag, board select
    bSelect = False
    
    ' Delete All Board information
    If intBoardNum > 1 Then
    For num = 1 To intBoardNum - 1
        colBoards.Remove frmSEGConf.ListBoards.ListItems(num).Key
    Next num
    End If
    
    frmSEGConf.ListBoards.ListItems.Clear
    
    'Clear board's count
    intBoardNum = 1
    
End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' Name : Form_Load
' Parameter : None
'
' Initialize control, variable, position
'
''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub Form_Load()

    ''''''''''''''''''''''''''''''''''''''
    
    frmSEGConf.Caption = "WIZ100SR/105SR/110SR Configuration Tool ver " & App.Major & "." & App.Minor & "." & App.Revision
    
    Dim colX As ColumnHeader
    Dim intX As Integer
    Set colX = ListBoards.ColumnHeaders.Add()
    colX.Text = "Board list"
    colX.Width = ListBoards.Width - 20
        

    WinsockUDP.RemoteHost = "255.255.255.255"
    WinsockUDP.RemotePort = 1460
    WinsockUDP.LocalPort = 5001
    WinsockUDP.Bind
    
    'Speed Value
    cboSpeed.AddItem "1200", 0
    cboSpeed.ItemData(0) = &HA0
    cboSpeed.AddItem "2400", 1
    cboSpeed.ItemData(1) = &HD0
    cboSpeed.AddItem "4800", 2
    cboSpeed.ItemData(2) = &HE8
    cboSpeed.AddItem "9600", 3
    cboSpeed.ItemData(3) = &HF4
    cboSpeed.AddItem "19200", 4
    cboSpeed.ItemData(4) = &HFA
    cboSpeed.AddItem "38400", 5
    cboSpeed.ItemData(5) = &HFD
    cboSpeed.AddItem "57600", 6
    cboSpeed.ItemData(6) = &HFE
    cboSpeed.AddItem "115200", 7
    cboSpeed.ItemData(7) = &HFF
    cboSpeed.AddItem "230400", 8
    cboSpeed.ItemData(8) = &HBB
    
    'Databit Value
    cboDataBits.AddItem "7", 0
    cboDataBits.ItemData(0) = &H7
    cboDataBits.AddItem "8", 1
    cboDataBits.ItemData(1) = &H8
    
    'Stopbit Value
    cboStopBits.AddItem "1", 0
    cboStopBits.ItemData(0) = &H1
    'cboStopBits.AddItem "2", 1
    'cboStopBits.ItemData(1) = &H2
    
    'Parity Value
    cboParity.AddItem "None", 0
    cboParity.ItemData(0) = &H0
    cboParity.AddItem "Odd", 1
    cboParity.ItemData(1) = &H1
    cboParity.AddItem "Even", 2
    cboParity.ItemData(2) = &H2
    
    cboFlow.AddItem "None", 0
    cboFlow.ItemData(0) = &H0
    cboFlow.AddItem "Xon/Xoff", 1
    cboFlow.ItemData(1) = &H1
    cboFlow.AddItem "CTS/RTS", 2
    cboFlow.ItemData(2) = &H2

    bSelect = False
    
    ToolMode = modeNone
    
    FrameTab(1).Left = TabStrip1.Left + 8
    FrameTab(1).Top = TabStrip1.Top + 24
    FrameTab(2).Left = TabStrip1.Left + 8
    FrameTab(2).Top = TabStrip1.Top + 24
    FrameTab(3).Left = TabStrip1.Left + 8
    FrameTab(3).Top = TabStrip1.Top + 24
    mintCurFrame = 1
    FrameTab(1).Visible = True
    
    txtDirectIP.Visible = False
    
    
End Sub

Private Sub Form_Unload(Cancel As Integer)
    End
End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' Name : func_SearchBoard
' Parameter : None
'
' Search available Boards.
' Send "FIND" message
' Waiting Board's reply
'
''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub func_SearchBoard()

    Dim sendD() As Byte
    
    ' First, delete all board's information
    Call BoardRemove
    
    ToolMode = modeSearching
    ' Send FIND message
    ReDim sendD(0 To 3) As Byte
    sendD(0) = Asc("F")
    sendD(1) = Asc("I")
    sendD(2) = Asc("N")
    sendD(3) = Asc("D")
    
    If chkDirect.Value = 1 Then
        WinsockDirect.RemoteHost = txtDirectIP.Text
        WinsockDirect.RemotePort = 1461
        WinsockDirect.Connect
        
    Else
        WinsockUDP.RemoteHost = "255.255.255.255"
        WinsockUDP.RemotePort = 1460
        WinsockUDP.SendData sendD
        Erase sendD
    End If
    
    Call ShowMsgWindow
    WinsockDirect.Close

End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' Name : func_SettingBoard
' Parameter : None
'
' Update the selected Board's configuration data.
' Make message with new configuration data.
' Send "SETT" message
' Waiting Board's reply
'
''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub func_SettingBoard()

On Error GoTo s_ERROR

    Dim sendD() As Byte
    Dim tmpstr() As String
    Dim ii As Integer
    
  Dim Mystring As String
    
    
    ' If exist selected board ,
    If bSelect Then
        
        ' Getting selected board's information
        ' Making SETT message
        If chkDebug.Value = 1 Then
            BoardInfo.debugoff = 0
        Else
            BoardInfo.debugoff = 1
        End If
        
        If Option1.Value = True Then
            BoardInfo.DHCP = 0
        ElseIf Option2.Value = True Then        ' DHCP
            BoardInfo.DHCP = 1
        ElseIf Option3.Value = True Then    ' PPPoE
            BoardInfo.DHCP = 2
        End If
        
        BoardInfo.UDP = chkUDPMode.Value
        BoardInfo.Connect = 0
        
        tmpstr = Split(txtIP.Text, ".")
        If UBound(tmpstr) <> 3 Then
            Call MessageBox("Invalid IP Address.")
            txtIP.SetFocus
            Exit Sub
        End If
        For ii = 0 To 3
            If tmpstr(ii) = "" Or CInt(tmpstr(ii)) > 255 Or CInt(tmpstr(ii)) < 0 Then
                Call MessageBox("Invalid IP Address.")
                txtIP.SetFocus
                Exit Sub
            End If
            BoardInfo.ip(ii) = CByte(tmpstr(ii))
        Next ii
        
        tmpstr = Split(txtSubnet.Text, ".")
        If UBound(tmpstr) <> 3 Then
            Call MessageBox("Invalid Subnet Mask.")
            txtSubnet.SetFocus
            Exit Sub
        End If
        For ii = 0 To 3
            If tmpstr(ii) = "" Or CInt(tmpstr(ii)) > 255 Or CInt(tmpstr(ii)) < 0 Then
                Call MessageBox("Invalid Subnet Mask.")
                txtSubnet.SetFocus
                Exit Sub
            End If
            BoardInfo.subnet(ii) = CByte(tmpstr(ii))
        Next ii
    
        tmpstr = Split(txtGW.Text, ".")
        If UBound(tmpstr) <> 3 Then
            Call MessageBox("Invalid Gateway Address.")
            txtGW.SetFocus
            Exit Sub
        End If
        For ii = 0 To 3
            If tmpstr(ii) = "" Or CInt(tmpstr(ii)) > 255 Or CInt(tmpstr(ii)) < 0 Then
                Call MessageBox("Invalid Gateway Address.")
                txtGW.SetFocus
                Exit Sub
            End If
            BoardInfo.gw(ii) = CByte(tmpstr(ii))
        Next ii
    
        BoardInfo.myport(0) = (CLng(txtPort.Text) And &HFF00) / &H100
        BoardInfo.myport(1) = CLng(txtPort.Text) And &HFF
        
        If optClientMode.Item(0).Value Then
            BoardInfo.bserver = 0
        ElseIf optClientMode.Item(1).Value Then
            BoardInfo.bserver = 1
        Else
            BoardInfo.bserver = 2
        End If
        
        tmpstr = Split(txtServerIP.Text, ".")
        If UBound(tmpstr) <> 3 Then
            Call MessageBox("Invalid Server IP Address.")
            txtServerIP.SetFocus
            Exit Sub
        End If
        For ii = 0 To 3
            If tmpstr(ii) = "" Or CInt(tmpstr(ii)) > 255 Or CInt(tmpstr(ii)) < 0 Then
                Call MessageBox("Invalid Server IP Address.")
                txtServerIP.SetFocus
                Exit Sub
            End If
            BoardInfo.peerip(ii) = CByte(tmpstr(ii))
        Next ii
        
        '''''''''''' DNS
        BoardInfo.DNS_Flag = ChkDNS.Value
        
        If ChkDNS.Value = 1 Then
            tmpstr = Split(txt_DNS_ServerIP.Text, ".")
            If UBound(tmpstr) <> 3 Then
                Call MessageBox("Invalid DNS Server IP Address.")
                txt_DNS_ServerIP.SetFocus
                Exit Sub
            End If
            For ii = 0 To 3
                If tmpstr(ii) = "" Or CInt(tmpstr(ii)) > 255 Or CInt(tmpstr(ii)) < 0 Then
                    Call MessageBox("Invalid DNS Server IP Address.")
                    txt_DNS_ServerIP.SetFocus
                    Exit Sub
                End If
                BoardInfo.DNS_IP(ii) = CByte(tmpstr(ii))
            Next ii
            
            Mystring = txtServer_Domain.Text
            ReDim mybytearray(0 To Len(Mystring) - 1) As Byte
            mybytearray() = StrConv(Mystring, vbFromUnicode)
            For ii = 0 To UBound(mybytearray)
              BoardInfo.D_SIP(ii) = mybytearray(ii)
            Next ii
            For ii = UBound(mybytearray) + 1 To 31
              BoardInfo.D_SIP(ii) = 0
            Next ii
'        Else
'            BoardInfo.DNS_IP(0) = 0
'            BoardInfo.DNS_IP(1) = 0
'            BoardInfo.DNS_IP(2) = 0
'            BoardInfo.DNS_IP(3) = 0
            
'            For ii = 0 To 31
'                BoardInfo.D_SIP(ii) = 0
'            Next ii
        
        End If
        
    
        BoardInfo.peerport(0) = (CLng(txtServerPort.Text) And &HFF00) / &H100
        BoardInfo.peerport(1) = CLng(txtServerPort.Text) And &HFF
        BoardInfo.I_time(0) = (CLng(txtITime.Text) And &HFF00) / &H100
        BoardInfo.I_time(1) = CLng(txtITime.Text) And &HFF
        BoardInfo.D_time(0) = (CLng(txtDTime.Text) And &HFF00) / &H100
        BoardInfo.D_time(1) = CLng(txtDTime.Text) And &HFF
        BoardInfo.D_size(0) = (CInt(txtDSize.Text) And &HFF00) / &H100
        BoardInfo.D_size(1) = CInt(txtDSize.Text) And &HFF
        BoardInfo.D_ch = CInt("&h" & txtDChar.Text)
        
        BoardInfo.speed = cboSpeed.ItemData(cboSpeed.ListIndex)
        BoardInfo.databit = cboDataBits.ItemData(cboDataBits.ListIndex)
        BoardInfo.parity = cboParity.ItemData(cboParity.ListIndex)
        BoardInfo.stopbit = cboStopBits.ItemData(cboStopBits.ListIndex)
        BoardInfo.flow = cboFlow.ItemData(cboFlow.ListIndex)
        
        If CheckSCfg.Value = 1 Then
            BoardInfo.SCfg = 1
        Else
            BoardInfo.SCfg = 0
        End If
        
        BoardInfo.SCfgStr(0) = CInt("&h" & TextSCfg(0).Text)
        BoardInfo.SCfgStr(1) = CInt("&h" & TextSCfg(1).Text)
        BoardInfo.SCfgStr(2) = CInt("&h" & TextSCfg(2).Text)
        
        If Option3.Value = True Then
            Mystring = txtID.Text
            ReDim mybytearray(0 To Len(Mystring) - 1) As Byte
            mybytearray() = StrConv(Mystring, vbFromUnicode)
            For ii = 0 To UBound(mybytearray)
              BoardInfo.PPPoE_ID(ii) = mybytearray(ii)
            Next ii
            For ii = UBound(mybytearray) + 1 To 31
              BoardInfo.PPPoE_ID(ii) = 0
            Next ii
            
            Mystring = txtPass.Text
            ReDim mybytearray(0 To Len(Mystring) - 1) As Byte
            mybytearray() = StrConv(Mystring, vbFromUnicode)
            For ii = 0 To UBound(mybytearray)
              BoardInfo.PPPoE_Pass(ii) = mybytearray(ii)
            Next ii
            For ii = UBound(mybytearray) + 1 To 31
              BoardInfo.PPPoE_Pass(ii) = 0
            Next ii
        End If

        If chkTCPPass.Value = 1 Then
            BoardInfo.EnTCPPass = 1
            
            Mystring = txtTCPPass.Text
            If Len(Mystring) > 8 Then
                ReDim mybytearray(0 To 7) As Byte
            Else
                ReDim mybytearray(0 To Len(Mystring) - 1) As Byte
            End If
            
            mybytearray() = StrConv(Mystring, vbFromUnicode)
            For ii = 0 To UBound(mybytearray)
              BoardInfo.TCPPass(ii) = mybytearray(ii)
            Next ii
            For ii = UBound(mybytearray) + 1 To 7
              BoardInfo.TCPPass(ii) = 0
            Next ii
        Else
            BoardInfo.EnTCPPass = 0
            For ii = 0 To 7
                BoardInfo.TCPPass(ii) = 0
            Next ii
        End If
        
        
        ToolMode = modeSetting

        ' Sending SETT message
        ReDim sendD(0 To Len(BoardInfo) + 3) As Byte
        sendD(0) = Asc("S")
        sendD(1) = Asc("E")
        sendD(2) = Asc("T")
        sendD(3) = Asc("T")
        CopyMemory sendD(4), BoardInfo, Len(BoardInfo)
        
        If chkDirect.Value = 1 Then
            WinsockDirect.RemoteHost = txtDirectIP.Text
            WinsockDirect.RemotePort = 1461
            WinsockDirect.Connect
            
        Else
            WinsockUDP.RemoteHost = "255.255.255.255"
            WinsockUDP.RemotePort = 1460
            WinsockUDP.SendData sendD
            Erase sendD
        End If
        
        Call ShowMsgWindow
        WinsockDirect.Close
    
    End If
    Exit Sub
    
s_ERROR:
    Call MessageBox("Invalid parameter value.")
    
End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' Name : func_Upload
' Parameter : None
'
' Uploading new firmware to selected Board.
' Send "FIRS" message for alert uploading to selected Board.
' Try to connect for making uploading socket.
' waiting until connecting Board.
'
''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub func_Upload()
    'Start of Ping, 2010.02.16; by Chen
    'check the device is in the same subnet with PC or not
    Dim reply As ICMP_ECHO_REPLY
    Dim strIp As String
    Dim lngResult As Long
    If chkDirect.Value = 1 Then
        strIp = Trim(txtDirectIP.Text)
    Else
        strIp = Trim(txtIP.Text)
    End If
    If Len(strIp) > 0 Then
        lngResult = ping(strIp, reply)
        If lngResult = 0 Then
            'go ahead, do nothing here
        Else
            MessageBox ("The destination: " & strIp & " is unreachable." & vbCrLf & _
                        "Please check if the device is in the same subnet with the PC!" & vbCrLf & vbCrLf & _
                        "---------------------------------------------------" & vbCrLf & _
                        "Ping Response Message: " & EvaluatePingResponse(lngResult))
            Exit Sub
        End If
        
    Else
        MessageBox ("Invalid IP address.")
    End If
    
    
    'end of Ping

    On Error Resume Next
    
    Dim Ret As Integer      ' Return value
    Dim sendD() As Byte
    Dim tmpstr() As String
    Dim ii As Integer
    
    If bSelect Then
        
        tmpstr = Split(txtIP.Text, ".")
        If UBound(tmpstr) <> 3 Then
            Call MessageBox("Invalid IP Address.")
            txtIP.SetFocus
            Exit Sub
        End If
        For ii = 0 To 3
            If tmpstr(ii) = "" Or CInt(tmpstr(ii)) > 255 Or CInt(tmpstr(ii)) < 0 Then
                Call MessageBox("Invalid IP Address.")
                txtIP.SetFocus
                Exit Sub
            End If
        Next ii
        
        ' Select firmware's file
        OpenLog.DialogTitle = "File Select"
        OpenLog.Filter = "Bin File (*.bin)|*.bin|All File (*.*)|*.*"
        Do
            OpenLog.CancelError = True
            OpenLog.FileName = ""
            OpenLog.ShowOpen
            If Err = cdlCancel Then
                Exit Sub
            End If
            
            strUploadFile = OpenLog.FileName
            ' if file not exist, return.
            Ret = Len(Dir$(strUploadFile))
            If Err Then
               Call MessageBox(Error$)
               Exit Sub
            End If
            If Ret Then
               Exit Do
            Else
               Call MessageBox("No existing " + strUploadFile)
            End If
        Loop
        
        ToolMode = modeUploading
        bDirectUpload = False
        
        ' Inform board uploading
        ' Send FIRS message
        ReDim sendD(0 To Len(BoardInfo) + 3) As Byte
        sendD(0) = Asc("F")
        sendD(1) = Asc("I")
        sendD(2) = Asc("R")
        sendD(3) = Asc("S")
        CopyMemory sendD(4), BoardInfo, Len(BoardInfo)
        
        If chkDirect.Value = 1 Then
            WinsockDirect.RemoteHost = txtDirectIP.Text
            WinsockDirect.RemotePort = 1461
            WinsockDirect.Connect
            
            While Not bDirectUpload
               DoEvents
            Wend
            
        Else
            WinsockUDP.RemoteHost = "255.255.255.255"
            WinsockUDP.RemotePort = 1460
            WinsockUDP.SendData sendD
            Erase sendD
        End If
        
        
        If chkDirect.Value = 1 Then
            destIP = txtDirectIP.Text
            Sleep (3000)
        Else
            destIP = txtIP.Text
            Sleep (500)
        End If
        
        'destIP = txtIP.Text
        'destIP = "211.171.137.58"
        Call ShowMsgWindow
        WinsockDirect.Close
        
    End If

End Sub


'Private Sub TextSCfg_Change(Index As Integer)
'If Len(TextSCfg(Index).Text) = TextSCfg(Index).MaxLength Then

'    Index = Index + 1
    
'    If Index > 2 Then Index = 0
    
'End If
'If TextSCfg(Index).Enabled = True Then
'TextSCfg(Index).SetFocus


'End If


'End Sub

Private Sub Toolbar1_ButtonClick(ByVal Button As MSComctlLib.Button)
    On Error Resume Next
    Select Case Button.Key
        Case "SearchBoard"
            Call func_SearchBoard
        Case "SettingBoard"
            Call func_SettingBoard
        Case "Upload"
            Call func_Upload
        Case "Exit"
            Form_Unload 0
        
        'ping & firewall
        Case "ping"
            frmPing.Show 1
            
            
        Case "firewall"
            Shell "rundll32.exe shell32.dll, Control_RunDLL FireWall.cpl", vbNormalFocus
            
            
    End Select
End Sub

Public Sub ListBoards_FirstRowSelect()
    ListBoards_ItemClick ListBoards.ListItems(1)
    ListBoards.ListItems(1).Selected = True
End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' Name : ListBoards_ItemClick
' Parameter : Item is string of selected board's key.
'
' Save key string to "BoardKey" variable
' Save configuration data to "BoardInfo" variable.
' Show configuration data of selected Board.
'
''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub ListBoards_ItemClick(ByVal Item As MSComctlLib.ListItem)
    Dim dd() As Byte
    Dim i As Long
    
On Error GoTo click_ERROR
    
    bSelect = True
    dd = colBoards.Item(Item.Key)
    BoardKey = Item.Key
    CopyMemory BoardInfo, dd(0), Len(BoardInfo)
    
    txtVersion.Text = BoardInfo.AppVer(0) & "." & BoardInfo.AppVer(1)
    If BoardInfo.debugoff = 0 Then
        chkDebug.Value = 1
    Else
        chkDebug.Value = 0
    End If
    
    If BoardInfo.DHCP = 1 Then
        Option2.Value = 1
    ElseIf BoardInfo.DHCP = 2 Then
        Option3.Value = 1
    ElseIf BoardInfo.DHCP = 0 Then
        Option1.Value = 1
    End If
    
    txtID.Text = StrConv(BoardInfo.PPPoE_ID, vbUnicode)
    txtPass.Text = StrConv(BoardInfo.PPPoE_Pass, vbUnicode)

    If BoardInfo.UDP = 1 Then
        chkUDPMode.Value = 1
    Else
        chkUDPMode.Value = 0
    End If
    If BoardInfo.Connect = 1 Then
        txtConnect.Text = "Connected"
    Else
        txtConnect.Text = "Not Connected"
    End If
        
    txtIP.Text = BoardInfo.ip(0) & "." & BoardInfo.ip(1) & "." & BoardInfo.ip(2) & "." & BoardInfo.ip(3)
    txtSubnet.Text = BoardInfo.subnet(0) & "." & BoardInfo.subnet(1) & "." & BoardInfo.subnet(2) & "." & BoardInfo.subnet(3)
    txtGW.Text = BoardInfo.gw(0) & "." & BoardInfo.gw(1) & "." & BoardInfo.gw(2) & "." & BoardInfo.gw(3)
    i = BoardInfo.myport(0)
    i = (i * &H100)
    i = i + BoardInfo.myport(1)
    txtPort.Text = CStr(i)
    
    optClientMode.Item(BoardInfo.bserver).Value = True
    
    ''DNS
    txt_DNS_ServerIP.Text = BoardInfo.DNS_IP(0) & "." & BoardInfo.DNS_IP(1) & "." & BoardInfo.DNS_IP(2) & "." & BoardInfo.DNS_IP(3)
    txtServer_Domain.Text = StrConv(BoardInfo.D_SIP, vbUnicode)
    If BoardInfo.DNS_Flag = 1 Then
       ChkDNS.Value = 1
    Else
       ChkDNS.Value = 0
    End If
    
    
    
    txtServerIP.Text = BoardInfo.peerip(0) & "." & BoardInfo.peerip(1) & "." & BoardInfo.peerip(2) & "." & BoardInfo.peerip(3)
    i = BoardInfo.peerport(0)
    i = (i * &H100)
    i = i + BoardInfo.peerport(1)
    txtServerPort.Text = CStr(i)
    
    i = BoardInfo.I_time(0)
    i = (i * &H100)
    i = i + BoardInfo.I_time(1)
    txtITime.Text = CStr(i)
    
    i = BoardInfo.D_time(0)
    i = (i * &H100)
    i = i + BoardInfo.D_time(1)
    txtDTime.Text = CStr(i)
    
    i = (BoardInfo.D_size(0) * &H100) + BoardInfo.D_size(1)
    txtDSize.Text = CStr(i)
    If BoardInfo.D_ch > 15 Then
        txtDChar.Text = Hex(BoardInfo.D_ch)
    Else
        txtDChar.Text = "0" & Hex(BoardInfo.D_ch)
    End If
    
    
    Select Case BoardInfo.speed
        Case &HBB
            cboSpeed.ListIndex = 8
        Case &HFF
            cboSpeed.ListIndex = 7
        Case &HFE
            cboSpeed.ListIndex = 6
        Case &HFD
            cboSpeed.ListIndex = 5
        Case &HFA
            cboSpeed.ListIndex = 4
        Case &HF4
            cboSpeed.ListIndex = 3
        Case &HE8
            cboSpeed.ListIndex = 2
        Case &HD0
            cboSpeed.ListIndex = 1
        Case &HA0
            cboSpeed.ListIndex = 0
        Case Else
            cboSpeed.ListIndex = 0
    End Select
    cboDataBits.Text = CStr(BoardInfo.databit)
    cboStopBits.Text = CStr(BoardInfo.stopbit)
    'cboParity.ListIndex = BoardInfo.parity
    Select Case BoardInfo.parity
        Case &H0
            cboParity.Text = "None"
        Case &H1
            cboParity.Text = "Odd"
        Case &H2
            cboParity.Text = "Even"
        Case Else
            cboParity.Text = "None"
    End Select
    cboFlow.ListIndex = BoardInfo.flow
    
    If (BoardInfo.AppVer(0) <= 1) And (BoardInfo.AppVer(1) < 2) Then        ' F/W ver. 1.2이하에서는 기능 없음.
        Frame3.Enabled = False
        CheckSCfg.Enabled = False
        CheckSCfg.Value = 0
        Label22.Enabled = False
        TextSCfg(0).Text = 0
        TextSCfg(1).Text = 0
        TextSCfg(2).Text = 0
        TextSCfg(0).Enabled = False
        TextSCfg(1).Enabled = False
        TextSCfg(2).Enabled = False
    Else
        Frame3.Enabled = True
        CheckSCfg.Enabled = True
        Label22.Enabled = True
        TextSCfg(0).Enabled = True
        TextSCfg(1).Enabled = True
        TextSCfg(2).Enabled = True
    
        If BoardInfo.SCfg = 0 Then
            CheckSCfg.Value = 0
        Else
            CheckSCfg.Value = 1
        End If
        
        If BoardInfo.SCfgStr(0) > 15 Then
            TextSCfg(0).Text = Hex(BoardInfo.SCfgStr(0))
        Else
            TextSCfg(0).Text = "0" & Hex(BoardInfo.SCfgStr(0))
        End If
        If BoardInfo.SCfgStr(1) > 15 Then
            TextSCfg(1).Text = Hex(BoardInfo.SCfgStr(1))
        Else
            TextSCfg(1).Text = "0" & Hex(BoardInfo.SCfgStr(1))
        End If
        If BoardInfo.SCfgStr(2) > 15 Then
            TextSCfg(2).Text = Hex(BoardInfo.SCfgStr(2))
        Else
            TextSCfg(2).Text = "0" & Hex(BoardInfo.SCfgStr(2))
        End If
    End If
    
    If (BoardInfo.AppVer(0) <= 2) And (BoardInfo.AppVer(1) < 1) Then        ' F/W ver. 2.1이하에서는 기능 없음.
        chkTCPPass.Enabled = False
        
        chkTCPPass.Value = 0
        
        txtTCPPass.Enabled = False
        txtTCPPass.Text = ""
        txtTCPPass.BackColor = &H80000011
    Else
        chkTCPPass.Enabled = True
        
        If BoardInfo.EnTCPPass = 0 Then
            chkTCPPass.Value = 0
        Else
            chkTCPPass.Value = 1
        End If
        
        txtTCPPass.Enabled = True
        txtTCPPass.Text = StrConv(BoardInfo.TCPPass, vbUnicode)
        txtTCPPass.BackColor = &H80000005
    End If
    
    Erase dd
    Exit Sub

click_ERROR:
    cboFlow.ListIndex = 0
    Call MessageBox("Invalid parameter's value.")
    
End Sub

Private Sub cboDataBits_Click()
    If cboDataBits.Text = "7" Then
        cboParity.Clear
        'cboParity.AddItem "None", 0
        'cboParity.ItemData(0) = &H0
        cboParity.AddItem "Odd", 0
        cboParity.ItemData(0) = &H1
        cboParity.AddItem "Even", 1
        cboParity.ItemData(1) = &H2
        cboParity.ListIndex = 0
    Else
        cboParity.Clear
        cboParity.AddItem "None", 0
        cboParity.ItemData(0) = &H0
        cboParity.AddItem "Odd", 1
        cboParity.ItemData(1) = &H1
        cboParity.AddItem "Even", 2
        cboParity.ItemData(2) = &H2
        cboParity.ListIndex = 0
    End If

End Sub



Private Sub WinsockDirect_Connect()
    Dim sendD() As Byte
    
    
    Select Case ToolMode
    Case modeSearching
            ReDim sendD(0 To 3) As Byte
            sendD(0) = Asc("F")
            sendD(1) = Asc("I")
            sendD(2) = Asc("N")
            sendD(3) = Asc("D")
    
    Case modeSetting
            ' Sending SETT message
            ReDim sendD(0 To Len(BoardInfo) + 3) As Byte
            sendD(0) = Asc("S")
            sendD(1) = Asc("E")
            sendD(2) = Asc("T")
            sendD(3) = Asc("T")
            CopyMemory sendD(4), BoardInfo, Len(BoardInfo)
    
    Case modeUploading
            ReDim sendD(0 To Len(BoardInfo) + 3) As Byte
            sendD(0) = Asc("F")
            sendD(1) = Asc("I")
            sendD(2) = Asc("R")
            sendD(3) = Asc("S")
            CopyMemory sendD(4), BoardInfo, Len(BoardInfo)
    Case Else
        WinsockDirect.Close
        Exit Sub
    End Select
    
    WinsockDirect.SendData sendD
    Erase sendD
    
    bDirectUpload = True

End Sub

Private Sub WinsockDirect_Error(ByVal Number As Integer, Description As String, ByVal Scode As Long, ByVal Source As String, ByVal HelpFile As String, ByVal HelpContext As Long, CancelDisplay As Boolean)

    WinsockDirect.Close

End Sub

Private Sub WinsockDirect_DataArrival(ByVal bytesTotal As Long)
On Error GoTo WinsockDirect_DataArrival_ERROR
'On Error Resume Next
    Dim getd() As Byte
    Dim getboard(BoardInfoSize_3_4) As Byte
    Dim getkind(3) As Byte
    
    ReDim getd(0 To (bytesTotal - 1)) As Byte
    WinsockDirect.GetData getd, vbByte, bytesTotal
    CopyMemory getkind(0), getd(0), 4
    CopyMemory getboard(0), getd(4), BoardInfoSize_3_4 - 4
    Erase getd
    
    If (getkind(0) = Asc("I")) And (getkind(1) = Asc("M")) And (getkind(2) = Asc("I")) And (getkind(3) = Asc("N")) Then
        If ToolMode = modeSearching Then
            
            Call BoardAdd(getboard)
            'ToolMode = None
            
        End If
        
    ElseIf (getkind(0) = Asc("S")) And (getkind(1) = Asc("E")) And (getkind(2) = Asc("T")) And (getkind(3) = Asc("C")) Then
        If ToolMode = modeSetting Then
            Call BoardUpdate(getboard)
        End If
    End If
    
    WinsockDirect.Close
    
    Exit Sub

WinsockDirect_DataArrival_ERROR:
    If Err Then
'        MsgBox "Retry, please"
        MsgBox Err.Description
    End If
    Erase getd

End Sub

''''''''''''''''''''''''''''''''''''''''''''''''
' Name : WinsockUDP_DataArrival
' Parameter : bytesTotal is count of receiving data from "WinsockUDP" control
'
' Receive configuration message.
' "IMIN" message => BoardAdd
' "SETC" message => BoardUpdate
'
''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub WinsockUDP_DataArrival(ByVal bytesTotal As Long)
On Error GoTo WinsockUDP_DataArrival_ERROR
'On Error Resume Next
    Dim getd() As Byte
    Dim getboard(BoardInfoSize_3_4) As Byte
    Dim getkind(3) As Byte
    
    ReDim getd(0 To (bytesTotal - 1)) As Byte
    WinsockUDP.GetData getd, vbByte, bytesTotal
    CopyMemory getkind(0), getd(0), 4
    CopyMemory getboard(0), getd(4), BoardInfoSize_3_4 - 4
    Erase getd
    
    If (getkind(0) = Asc("I")) And (getkind(1) = Asc("M")) And (getkind(2) = Asc("I")) And (getkind(3) = Asc("N")) Then
        If ToolMode = modeSearching Then
            
            If getboard(103) = 0 Or getboard(103) > 9 Then 'to distinguish 100SR from 120SR. If 120SR, the received 103th byte will be > 0 and < 10 (Firmware main version of 120SR)! 2010.05.03
                Call BoardAdd(getboard)
            End If
            
        End If
        
    ElseIf (getkind(0) = Asc("S")) And (getkind(1) = Asc("E")) And (getkind(2) = Asc("T")) And (getkind(3) = Asc("C")) Then
        If ToolMode = modeSetting Then
            Call BoardUpdate(getboard)
        End If
    End If
    Exit Sub

WinsockUDP_DataArrival_ERROR:
    If Err Then
'        MsgBox "Retry, please"
        MsgBox Err.Description
    End If
    Erase getd
    
End Sub

Private Sub chkDirect_Click()
    If chkDirect.Value = 1 Then
        txtDirectIP.Visible = True
    Else
        txtDirectIP.Visible = False
    End If
    
End Sub


''''''''''''''''''''''''''''''''''''''''''''''''
' Name : TEXT Box filtering
' Parameter : None
'
' Filtering textbox's data
' ex) Port text box support only number
'
''''''''''''''''''''''''''''''''''''''''''''''''
Private Sub txtVersion_KeyPress(KeyAscii As Integer)
    
        KeyAscii = 0

End Sub

Private Sub txtDChar_KeyPress(KeyAscii As Integer)
    
    If (KeyAscii = 8) Or (KeyAscii >= 48 And KeyAscii <= 57) Or (KeyAscii >= 65 And KeyAscii <= 70) Or (KeyAscii >= 97 And KeyAscii <= 102) Then
    ' backspace or 0~9 or A~F or a~f
    Else
    ' else ignore
        KeyAscii = 0
    End If

End Sub

Private Sub txtDTime_KeyPress(KeyAscii As Integer)
    
    If (KeyAscii = 8) Or (KeyAscii >= 48 And KeyAscii <= 57) Then
    ' backspace or 0~9
    Else
    ' else ignore
        KeyAscii = 0
    End If

End Sub

Private Sub txtITime_KeyPress(KeyAscii As Integer)
    
    If (KeyAscii = 8) Or (KeyAscii >= 48 And KeyAscii <= 57) Then
    ' backspace or 0~9
    Else
    ' else ignore
        KeyAscii = 0
    End If

End Sub

Private Sub txtDSize_KeyPress(KeyAscii As Integer)
    
    If (KeyAscii = 8) Or (KeyAscii >= 48 And KeyAscii <= 57) Then
    ' backspace or 0~9
    Else
    ' else ignore
        KeyAscii = 0
    End If

End Sub

Private Sub txtIP_KeyPress(KeyAscii As Integer)
    
    If (KeyAscii = 8) Or (KeyAscii = 46) Or (KeyAscii >= 48 And KeyAscii <= 57) Then
    ' backspace or . or 0~9
    Else
    ' else ignore
        KeyAscii = 0
    End If

End Sub

Private Sub txtDirectIP_KeyPress(KeyAscii As Integer)
    
    If (KeyAscii = 8) Or (KeyAscii = 46) Or (KeyAscii >= 48 And KeyAscii <= 57) Then
    ' backspace or . or 0~9
    Else
    ' else ignore
        KeyAscii = 0
    End If

End Sub

Private Sub txtServerIP_KeyPress(KeyAscii As Integer)
    
    If (KeyAscii = 8) Or (KeyAscii = 46) Or (KeyAscii >= 48 And KeyAscii <= 57) Then
    ' backspace or . or 0~9
    Else
    ' else ignore
        KeyAscii = 0
    End If

End Sub

Private Sub txtPort_KeyPress(KeyAscii As Integer)
    
    If (KeyAscii = 8) Or (KeyAscii >= 48 And KeyAscii <= 57) Then
    ' backspace or 0~9
    Else
    ' else ignore
        KeyAscii = 0
    End If

End Sub

Private Sub txtServerPort_KeyPress(KeyAscii As Integer)
    
    If (KeyAscii = 8) Or (KeyAscii >= 48 And KeyAscii <= 57) Then
    ' backspace or 0~9
    Else
    ' else ignore
        KeyAscii = 0
    End If

End Sub

Private Sub txtConnect_KeyPress(KeyAscii As Integer)
    KeyAscii = 0
End Sub

Private Sub txt_DNS_ServerIP_KeyPress(KeyAscii As Integer)
    If (KeyAscii = 8) Or (KeyAscii = 46) Or (KeyAscii >= 48 And KeyAscii <= 57) Then
    ' backspace or . or 0~9
    Else
    ' else ignore
        KeyAscii = 0
    End If
End Sub

