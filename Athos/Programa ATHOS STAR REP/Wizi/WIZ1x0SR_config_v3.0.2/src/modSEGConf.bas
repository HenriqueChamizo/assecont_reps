Attribute VB_Name = "modSEGConf"
Option Explicit

Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (Destination As Any, Source As Any, ByVal Length As Long)
Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)

'get system directory
Public Declare Function GetSystemDirectory Lib "kernel32" Alias "GetSystemDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long

' Board configuration data type
'--------------------------------------------------
Private Type typeBoardInfo
    mac(0 To 5) As Byte
    bserver As Byte
    ip(0 To 3) As Byte
    subnet(0 To 3) As Byte
    gw(0 To 3) As Byte
    myport(0 To 1) As Byte
    peerip(0 To 3) As Byte
    peerport(0 To 1) As Byte
    speed As Byte
    databit As Byte
    parity As Byte
    stopbit As Byte
    flow As Byte
    D_ch As Byte
    D_size(0 To 1) As Byte
    D_time(0 To 1) As Byte
    I_time(0 To 1) As Byte
    debugoff As Byte
    AppVer(0 To 1) As Byte
    DHCP As Byte
    UDP As Byte
    Connect As Byte
    DNS_Flag As Byte
    DNS_IP(0 To 3) As Byte
    D_SIP(0 To 31) As Byte
    SCfg As Byte
    SCfgStr(0 To 2) As Byte
    
    PPPoE_ID(0 To 31) As Byte
    PPPoE_Pass(0 To 31) As Byte
    
    EnTCPPass As Byte
    TCPPass(0 To 7) As Byte
    
End Type

Public Const BoardInfoSize_3_4 As Integer = 163
'Public Const BoardInfoSize_3_4 As Integer = 154
'Public Const BoardInfoSize_3_4 As Integer = 90
'Public Const BoardInfoSize_3_0 As Integer = 86


' This tool's mode
'--------------------------------------------------
Public Enum typeToolMode
    modeNone = 0
    modeSearching = 1
    modeSetting = 2
    modeSettingComplete = 3
    modeUploading = 4
    modeUploadingComplete = 5
End Enum
Public ToolMode As typeToolMode

' Total count of Boards
'--------------------------------------------------
Public intBoardNum As Integer
' Collection of Board's configuration data.
'--------------------------------------------------
Public colBoards As New Collection

' Selected Board's infomation
'--------------------------------------------------
Public BoardKey As String
Public BoardInfo As typeBoardInfo
Public bSelect As Boolean
Public bDirectUpload As Boolean

' Selected Firmware file and DestIP address for uploading
'--------------------------------------------------
Public strUploadFile As String
Public destIP As String

Sub MessageBox(msg As String)
       
    Call MsgBox(msg, vbInformation Or vbMsgBoxSetForeground, "WIZnet SEG Information")
    
End Sub
