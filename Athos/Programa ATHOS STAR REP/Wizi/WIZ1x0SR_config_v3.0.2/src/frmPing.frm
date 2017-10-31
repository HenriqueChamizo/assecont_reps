VERSION 5.00
Begin VB.Form frmPing 
   BorderStyle     =   1  '단일 고정
   Caption         =   "PING"
   ClientHeight    =   5085
   ClientLeft      =   45
   ClientTop       =   435
   ClientWidth     =   5970
   Icon            =   "frmPing.frx":0000
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   5085
   ScaleWidth      =   5970
   StartUpPosition =   1  '소유자 가운데
   Begin VB.CommandButton btnClose 
      Caption         =   "&Close"
      Height          =   435
      Left            =   2160
      TabIndex        =   5
      Top             =   4500
      Width           =   1875
   End
   Begin VB.Frame Frame2 
      Caption         =   "PING"
      Height          =   855
      Left            =   60
      TabIndex        =   1
      Top             =   3480
      Width           =   5835
      Begin VB.CommandButton btnPing 
         Caption         =   "PING"
         Height          =   375
         Left            =   4740
         TabIndex        =   4
         Top             =   300
         Width           =   915
      End
      Begin VB.TextBox txtIpAddr 
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   1680
         TabIndex        =   3
         Top             =   300
         Width           =   2895
      End
      Begin VB.Label Label1 
         Caption         =   "Host IP Address: "
         Height          =   315
         Left            =   120
         TabIndex        =   2
         Top             =   360
         Width           =   1455
      End
   End
   Begin VB.Frame Frame1 
      Height          =   3315
      Left            =   60
      TabIndex        =   0
      Top             =   60
      Width           =   5835
      Begin VB.TextBox txtPing 
         BeginProperty Font 
            Name            =   "Arial"
            Size            =   9
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   3015
         Left            =   120
         Locked          =   -1  'True
         MultiLine       =   -1  'True
         TabIndex        =   6
         Top             =   180
         Width           =   5595
      End
   End
End
Attribute VB_Name = "frmPing"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub btnClose_Click()
    Me.Hide
    
End Sub

Private Sub btnPing_Click()

    txtPing.Text = ""
    
    'check IP address
    Dim strIp As String
    strIp = Trim(txtIpAddr.Text)
    Dim s() As String
    Dim i As Integer
    
    If Len(strIp) > 0 Then
        s = Split(strIp, ".")
        If UBound(s) = 3 Then
            
            For i = 0 To 2
                If CInt(s(i)) >= 0 And CInt(s(i)) <= 255 Then
                    
                Else
                    txtPing.Text = "Please input a correct IP address. " & vbCrLf & vbCrLf & _
                            "ex: 192.168.11.3"
                    txtIpAddr.SetFocus
                    
                    txtIpAddr.SelStart = 0
                    txtIpAddr.SelLength = Len(txtIpAddr.Text)
                    Exit Sub
                End If

            Next i
            'disable other controls
            txtIpAddr.Enabled = False
            btnPing.Enabled = False
            
            'ping and show reply
            txtPing.Text = "Pinging " & strIp & " with 32 bytes of data: " & vbCrLf & vbCrLf
            DoEvents
            
            Dim reply As ICMP_ECHO_REPLY
            
            Dim lngSuccess As Long
            Dim iSuccess As Integer
            Dim iFailed As Integer
            
            If SocketsInitialize() Then
                For i = 0 To 4
                    DoEvents
                    Sleep (1000)
                    lngSuccess = ping(strIp, reply)
               
                    If lngSuccess = ICMP_SUCCESS Then
                    'show detailed message
                        If reply.RoundTripTime = 0 Then
                            reply.RoundTripTime = 1
                        End If
                        
                        txtPing.Text = txtPing.Text & "Reply from " & strIp & " : bytes=32 time<= " & reply.RoundTripTime & "ms TTL=" & reply.Options.Ttl & vbCrLf
                        iSuccess = iSuccess + 1
                    Else
                        txtPing.Text = txtPing.Text & EvaluatePingResponse(lngSuccess) & vbCrLf
                        iFailed = iFailed + 1
                    End If
                Next i
                
                txtPing.Text = txtPing.Text & vbCrLf & "Packets: Sent = 5, Received = " & iSuccess & ", Lost = " & iFailed
                
                
            Else
                txtPing.Text = WINSOCK_ERROR
            End If
            txtIpAddr.Enabled = True
            btnPing.Enabled = True
            'clean up socktes
            SocketsCleanup
            
        Else
            txtPing.Text = "Please input a correct IP address. " & vbCrLf & vbCrLf & _
                            "ex: 192.168.11.3"
            txtIpAddr.SetFocus
            
            txtIpAddr.SelStart = 0
            txtIpAddr.SelLength = Len(txtIpAddr.Text)
        End If
        
        
    Else
        txtPing.Text = "Please input IP adress."
        txtIpAddr.SetFocus
        
    End If
    
   
    
    
End Sub


Private Sub Form_Activate()
    txtIpAddr.SetFocus
End Sub


Private Sub txtIpAddr_KeyPress(KeyAscii As Integer)
 
    If (KeyAscii = 8) Or (KeyAscii = 46) Or (KeyAscii >= 48 And KeyAscii <= 57) Then
    ' backspace or . or 0~9
    ElseIf KeyAscii = 13 Then 'enter
        Call btnPing_Click
     Else
        KeyAscii = 0
    End If
End Sub
