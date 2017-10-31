VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   2505
   ClientLeft      =   120
   ClientTop       =   465
   ClientWidth     =   4710
   LinkTopic       =   "Form1"
   ScaleHeight     =   2505
   ScaleWidth      =   4710
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton Command1 
      Caption         =   "Conectar"
      Height          =   495
      Left            =   240
      TabIndex        =   0
      Top             =   120
      Width           =   1575
   End
   Begin VB.Label Label1 
      Caption         =   "Label1"
      Height          =   1455
      Left            =   240
      TabIndex        =   1
      Top             =   720
      Width           =   4215
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Command1_Click()
Dim rep As New RepCid.RepCid
Dim er As ErrosRep
er = rep.Conectar_vb6("192.168.0.99", 1818, 0)
Label1.Caption = er

If er = ErrosRep_OK Then
    Dim doc As String
    Dim tipo As Long
    Dim cei As String
    Dim razao As String
    Dim endereco As String

    If rep.LerEmpregador(doc, tipo, cei, razao, endereco) Then
        Label1.Caption = "OK empregador Lido: " + doc + " - " + razao
    Else
        Label1.Caption = "Erro ao ler empregador"
    End If
    
End If

End Sub
