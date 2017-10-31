Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.IO
Imports System.Security.Cryptography
Imports System.Security.AccessControl
Imports BouncyCastle.Crypto
Public Class Form1
    ' Create a TCP/IP socket.
    Dim client As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
    ' The port number for the remote device.
    Private Const port As Integer = 3000

    Private chaveAes(15) As Byte

    Private Shared connectDone As New ManualResetEvent(False)
    Private Shared sendDone As New ManualResetEvent(False)
    Private Shared receiveDone As New ManualResetEvent(False)

    Private Shared response As String = String.Empty
    Private Shared quantBytesRec As Integer = 0


    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim ipHostInfo As IPHostEntry = Dns.Resolve(txtIP.Text)
        Dim ipAddress As IPAddress = ipHostInfo.AddressList(0)
        Dim remoteEP As New IPEndPoint(ipAddress, port)
        Dim command As String
        Dim preCommand As String = ""
        Dim idxByte As Integer = 0
        Dim strModulo As String = ""
        Dim strExpodente As String = ""
        Dim strRec As String = ""
        Dim chkSum As Byte
        Dim strComandoComCriptografia As String
        Dim strAux As String

        Dim i As Integer = 0

        Dim conectado As Boolean = client.Connected

        If Not conectado Then
            client.BeginConnect(remoteEP, New AsyncCallback(AddressOf ConnectCallback), client)
        End If
        
        ' Wait for connect.
        If Not conectado Then
            connectDone.WaitOne()
        End If

        Randomize()
        ' gerando a chave aes
        chaveAes(0) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(1) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(2) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(3) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(4) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(5) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(6) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(7) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(8) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(9) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(10) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(11) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(12) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(13) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(14) = CInt(Int((255 * Rnd()) + 1))
        chaveAes(15) = CInt(Int((255 * Rnd()) + 1))

        command = ""
        command = command + Chr(2) ' start byte

        preCommand = preCommand + Chr(7) ' tamanho do comando
        preCommand = preCommand + Chr(0) ' tamanho do comando
        preCommand = preCommand + "1+RA+00"
        chkSum = calcCheckSumString(preCommand)

        command = command + preCommand
        command = command + Convert.ToChar(chkSum) ' checksum
        command = command + Chr(3) ' end byte


        Send(client, command)
        sendDone.WaitOne()

        ' Receive the response from the remote device.
        Dim state As New SocketClass()

        quantBytesRec = client.Receive(state.buffer)

        response = ""
        While i < quantBytesRec
            response = response + Convert.ToChar(state.buffer.ElementAt(i))

            i = i + 1
        End While


        While idxByte < quantBytesRec
            If idxByte >= 3 Then
                If idxByte <= quantBytesRec - 3 Then
                    strRec = strRec + response.ElementAt(idxByte)
                End If
            End If
            idxByte = idxByte + 1
        End While
        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length)
        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length)
        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length)

        strModulo = Mid(strRec, 1, strRec.IndexOf("]"))
        strExpodente = Trim(Mid(strRec, strRec.IndexOf("]") + 2, strRec.Length))



        strAux = "1]" + txtUsuario.Text + "]" + txtSenha.Text + "]" + System.Convert.ToBase64String(chaveAes)

        RSAPersistKeyInCSP(strModulo)
        Dim dataToEncrypt As Byte() = Encoding.Default.GetBytes(strAux)
        Dim encryptedData() As Byte

        encryptedData = RSAEncrypt(dataToEncrypt, strModulo, False, strModulo, strExpodente)

        strAux = System.Convert.ToBase64String(encryptedData)


        strComandoComCriptografia = "2+EA+00+" + strAux

        preCommand = ""
        command = ""
        command = command + Chr(2) ' start byte
        preCommand = preCommand + Chr(strComandoComCriptografia.Length) ' tamanho do comando
        preCommand = preCommand + Chr(0) ' tamanho do comando
        preCommand = preCommand + strComandoComCriptografia
        chkSum = calcCheckSumString(preCommand)

        command = command + preCommand
        command = command + Convert.ToChar(chkSum) ' checksum

        command = command + Chr(3)      ' end byte
        Send(client, command)
        sendDone.WaitOne()

        response = ""
        ' Receive the response from the remote device.
        state = New SocketClass()

        quantBytesRec = client.Receive(state.buffer)

        response = ""
        i = 0
        While i < quantBytesRec
            response = response + Convert.ToChar(state.buffer.ElementAt(i))

            i = i + 1
        End While

        strRec = ""
        idxByte = 0
        While idxByte < quantBytesRec
            If idxByte >= 3 Then
                If idxByte <= quantBytesRec - 3 Then
                    strRec = strRec + response.ElementAt(idxByte)
                End If
            End If
            idxByte = idxByte + 1
        End While
        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length)
        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length)

        If strRec = "000" Then
            ' Write the response to the console.
            MsgBox("Autenticado")
        Else
            MsgBox("Não autenticado.")
            Exit Sub
        End If

        

    End Sub

    Private Shared Sub ConnectCallback(ByVal ar As IAsyncResult)
        ' Retrieve the socket from the state object.
        Dim client As Socket = CType(ar.AsyncState, Socket)

        ' Complete the connection.
        client.EndConnect(ar)

        Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString())

        ' Signal that the connection has been made.
        connectDone.Set()
    End Sub 'ConnectCallback


    Private Shared Sub Receive(ByVal client As Socket)

        ' Create the state object.
        Dim state As New StateObject
        state.workSocket = client

        ' Begin receiving the data from the remote device.
        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
    End Sub 'Receive


    Private Shared Sub ReceiveCallback(ByVal ar As IAsyncResult)

        ' Retrieve the state object and the client socket 
        ' from the asynchronous state object.
        Dim state As StateObject = CType(ar.AsyncState, StateObject)
        Dim client As Socket = state.workSocket

        ' Read data from the remote device.
        Dim bytesRead As Integer = client.EndReceive(ar)
        quantBytesRec = bytesRead
        If bytesRead > 0 Then
            state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead))
            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, New AsyncCallback(AddressOf ReceiveCallback), state)
            response = state.sb.ToString()

            Dim HexValue As String = String.Empty
            For Each c As Char In response
                HexValue = HexValue + Convert.ToString(Convert.ToInt32(c), 16)
            Next c
            Console.WriteLine("Quant.:" + bytesRead.ToString + " resp:", HexValue)
            receiveDone.Set()
        End If

    End Sub 'ReceiveCallback

    Function calcCheckSumString(ByVal data As String) As Byte
        Dim cks As Byte = 0
        Dim i As Integer = 0

        While i < data.Length
            cks = cks Xor Convert.ToByte(data.ElementAt(i))
            i = i + 1
        End While
        Return cks
    End Function

    Function calcCheckSumByte(ByVal data() As Byte) As Byte
        Dim cks As Byte = 0
        Dim i As Integer

        While i < data.Length
            cks = cks Xor data.ElementAt(i)
            i = i + 1
        End While
        Return cks
    End Function

    Private Shared Sub Send(ByVal client As Socket, ByVal data As String)
        ' Convert the string data to byte data using ASCII encoding.
        Dim byteData As Byte() = Encoding.Default.GetBytes(data)
        
        ' Begin sending the data to the remote device.
        client.BeginSend(byteData, 0, byteData.Length, 0, New AsyncCallback(AddressOf SendCallback), client)
    End Sub 'Send

    Private Shared Sub Send2(ByVal client As Socket, ByVal byteData As Byte())
        
        ' Begin sending the data to the remote device.
        'client.BeginSend(byteData, 0, byteData.Length, 0, New AsyncCallback(AddressOf SendCallback), client)
        client.Send(byteData, 0, byteData.Length, 0)

    End Sub 'Send

    Private Shared Sub SendCallback(ByVal ar As IAsyncResult)
        ' Retrieve the socket from the state object.
        Dim client As Socket = CType(ar.AsyncState, Socket)

        ' Complete sending the data to the remote device.
        Dim bytesSent As Integer = client.EndSend(ar)
        Console.WriteLine("Sent {0} bytes to server.", bytesSent)

        ' Signal that all bytes have been sent.
        sendDone.Set()
    End Sub 'SendCallback

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtDataHora.TextChanged

    End Sub

    Private Shared Function EncryptStringToBytes_Aes(ByVal plainText As String, ByVal Key() As Byte, ByVal IV() As Byte) As Byte()
        ' Check arguments.
        If plainText Is Nothing OrElse plainText.Length <= 0 Then
            Throw New ArgumentNullException("plainText")
        End If
        If Key Is Nothing OrElse Key.Length <= 0 Then
            Throw New ArgumentNullException("Key")
        End If
        If IV Is Nothing OrElse IV.Length <= 0 Then
            Throw New ArgumentNullException("Key")
        End If

        Dim encrypted() As Byte

        ' Create an Aes object
        ' with the specified key and IV.
        Using aesAlg As Aes = Aes.Create("AES")

            aesAlg.Padding = PaddingMode.None
            aesAlg.Mode = CipherMode.CBC
            aesAlg.KeySize = 128
            aesAlg.BlockSize = 128

            aesAlg.Key = Key
            aesAlg.IV = IV

            ' Create a decrytor to perform the stream transform.
            Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)

            ' Create the streams used for encryption.
            Using msEncrypt As New MemoryStream()
                Using csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
                    Using swEncrypt As New StreamWriter(csEncrypt)
                        'Write all data to the stream.
                        swEncrypt.Write(plainText)
                        Dim quant As Integer = plainText.Length
                        Dim rest As Integer = 0
                        If quant < 16 Then
                            rest = 16 - quant
                        Else
                            rest = quant Mod 16
                        End If
                        While rest < 16
                            swEncrypt.Write(Convert.ToChar(Convert.ToByte("0")))
                            rest = rest + 1
                        End While
                    End Using
                    encrypted = msEncrypt.ToArray()
                End Using
            End Using
        End Using

        ' Return the encrypted bytes from the memory stream.
        Return encrypted

    End Function 'EncryptStringToBytes_Aes

    Private Shared Function DecryptStringFromBytes_Aes(ByVal cipherText() As Byte, ByVal Key() As Byte, ByVal IV() As Byte) As String
        ' Check arguments.
        If cipherText Is Nothing OrElse cipherText.Length <= 0 Then
            Throw New ArgumentNullException("cipherText")
        End If
        If Key Is Nothing OrElse Key.Length <= 0 Then
            Throw New ArgumentNullException("Key")
        End If
        If IV Is Nothing OrElse IV.Length <= 0 Then
            Throw New ArgumentNullException("Key")
        End If
        ' Declare the string used to hold
        ' the decrypted text.
        Dim plaintext As String = Nothing

        ' Create an Aes object
        ' with the specified key and IV.
        Using aesAlg As Aes = Aes.Create()
            aesAlg.Key = Key
            aesAlg.IV = IV
            aesAlg.Padding = PaddingMode.None
            aesAlg.Mode = CipherMode.CBC


            ' Create a decrytor to perform the stream transform.
            Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)

            ' Create the streams used for decryption.
            Using msDecrypt As New MemoryStream(cipherText)

                Using csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)

                    Using srDecrypt As New StreamReader(csDecrypt)


                        ' Read the decrypted bytes from the decrypting stream
                        ' and place them in a string.
                        plaintext = srDecrypt.ReadToEnd()
                    End Using
                End Using
            End Using
        End Using

        Return plaintext

    End Function 'DecryptStringFromBytes_Aes 

    Private Sub txtSenha_TextChanged(sender As Object, e As EventArgs) Handles txtSenha.TextChanged

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub txtUsuario_TextChanged(sender As Object, e As EventArgs) Handles txtUsuario.TextChanged

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub txtIP_TextChanged(sender As Object, e As EventArgs) Handles txtIP.TextChanged

    End Sub

    Sub RSAPersistKeyInCSP(ByVal ContainerName As String)
        Try
            ' Create a new instance of CspParameters.  Pass 
            ' 13 to specify a DSA container or 1 to specify 
            ' an RSA container.  The default is 1. 
            Dim cspParams As New CspParameters

            ' Specify the container name using the passed variable.
            cspParams.KeyContainerName = ContainerName

            'Create a new instance of RSACryptoServiceProvider to generate 
            'a new key pair.  Pass the CspParameters class to persist the  
            'key in the container. 
            Dim RSAalg As New RSACryptoServiceProvider(cspParams)

            'Indicate that the key was persisted.
            Console.WriteLine("The RSA key was persisted in the container, ""{0}"".", ContainerName)
        Catch e As CryptographicException
            Console.WriteLine(e.Message)
        End Try
    End Sub

    Sub RSADeleteKeyInCSP(ByVal ContainerName As String)
        Try
            ' Create a new instance of CspParameters.  Pass 
            ' 13 to specify a DSA container or 1 to specify 
            ' an RSA container.  The default is 1. 
            Dim cspParams As New CspParameters

            ' Specify the container name using the passed variable.
            cspParams.KeyContainerName = ContainerName

            'Create a new instance of RSACryptoServiceProvider.  
            'Pass the CspParameters class to use the  
            'key in the container. 
            Dim RSAalg As New RSACryptoServiceProvider(cspParams)

            'Delete the key entry in the container.
            RSAalg.PersistKeyInCsp = False

            'Call Clear to release resources and delete the key from the container.
            RSAalg.Clear()

            'Indicate that the key was persisted.
            Console.WriteLine("The RSA key was deleted from the container, ""{0}"".", ContainerName)
        Catch e As CryptographicException
            Console.WriteLine(e.Message)
        End Try
    End Sub


    Function RSAEncrypt(ByVal DataToEncrypt() As Byte, ByVal ContainerName As String, ByVal DoOAEPPadding As Boolean, ByVal modulo As String, ByVal expoente As String) As Byte()
        Try
            ' Create a new instance of CspParameters.  Pass 
            ' 13 to specify a DSA container or 1 to specify 
            ' an RSA container.  The default is 1. 
            Dim cspParams As New CspParameters

            ' Specify the container name using the passed variable.
            cspParams.KeyContainerName = ContainerName

            'Create a new instance of RSACryptoServiceProvider. 
            'Pass the CspParameters class to use the key  
            'from the key in the container. 
            Dim RSAalg As New RSACryptoServiceProvider()

            Dim parametros As RSAParameters = RSAalg.ExportParameters(False)
            parametros = New RSAParameters
            parametros.Modulus = System.Convert.FromBase64String(modulo)
            parametros.Exponent = System.Convert.FromBase64String(expoente)

            RSAalg.ImportParameters(parametros)

            'Encrypt the passed byte array and specify OAEP padding.   
            'OAEP padding is only available on Microsoft Windows XP or 
            'later.   
            Return RSAalg.Encrypt(DataToEncrypt, DoOAEPPadding)
            'Catch and display a CryptographicException   
            'to the console. 
        Catch e As CryptographicException
            Console.WriteLine(e.Message)

            Return Nothing
        End Try
    End Function


    Function RSADecrypt(ByVal DataToDecrypt() As Byte, ByVal ContainerName As String, ByVal DoOAEPPadding As Boolean) As Byte()
        Try
            ' Create a new instance of CspParameters.  Pass 
            ' 13 to specify a DSA container or 1 to specify 
            ' an RSA container.  The default is 1. 
            Dim cspParams As New CspParameters

            ' Specify the container name using the passed variable.
            cspParams.KeyContainerName = ContainerName

            'Create a new instance of RSACryptoServiceProvider. 
            'Pass the CspParameters class to use the key  
            'from the key in the container. 
            Dim RSAalg As New RSACryptoServiceProvider(cspParams)

            'Decrypt the passed byte array and specify OAEP padding.   
            'OAEP padding is only available on Microsoft Windows XP or 
            'later.   
            Return RSAalg.Decrypt(DataToDecrypt, DoOAEPPadding)
            'Catch and display a CryptographicException   
            'to the console. 
        Catch e As CryptographicException
            Console.WriteLine(e.ToString())

            Return Nothing
        End Try
    End Function

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim strComandoComCriptografia As String = "03+RH+00"
        Dim i As Integer = 0
        Dim chkSum As Integer = 0
        Dim strRec As String = ""
        Dim idxByte As Integer = 0

        Randomize()

        'strComandoComCriptografia = System.Convert.ToBase64String(EncryptStringToBytes_Aes(strComandoComCriptografia, chaveAes, chaveAes))
        Dim IV(15) As Byte
        IV(0) = CInt(Int((255 * Rnd()) + 1))
        IV(1) = CInt(Int((255 * Rnd()) + 1))
        IV(2) = CInt(Int((255 * Rnd()) + 1))
        IV(3) = CInt(Int((255 * Rnd()) + 1))
        IV(4) = CInt(Int((255 * Rnd()) + 1))
        IV(5) = CInt(Int((255 * Rnd()) + 1))
        IV(6) = CInt(Int((255 * Rnd()) + 1))
        IV(7) = CInt(Int((255 * Rnd()) + 1))
        IV(8) = CInt(Int((255 * Rnd()) + 1))
        IV(9) = CInt(Int((255 * Rnd()) + 1))
        IV(10) = CInt(Int((255 * Rnd()) + 1))
        IV(11) = CInt(Int((255 * Rnd()) + 1))
        IV(12) = CInt(Int((255 * Rnd()) + 1))
        IV(13) = CInt(Int((255 * Rnd()) + 1))
        IV(14) = CInt(Int((255 * Rnd()) + 1))
        IV(15) = CInt(Int((255 * Rnd()) + 1))

        Dim tamanhoPacote As Integer = 32
        Dim comandoByte(36) As Byte
        Dim IdxComandoByte As Integer = 3
        comandoByte(0) = 2  ' start byte
        comandoByte(1) = tamanhoPacote And &HFF ' tamanho
        comandoByte(2) = (tamanhoPacote >> 8) And &HFF  ' tamanho

        Dim cmdCrypt As Byte() = Encoding.Default.GetBytes(Encoding.Default.GetChars(EncryptStringToBytes_Aes(strComandoComCriptografia, chaveAes, IV)))
        chkSum = 0
        i = 0
        While i < IV.Length
            comandoByte(IdxComandoByte) = IV(i)

            IdxComandoByte = IdxComandoByte + 1
            i = i + 1
        End While

        i = 0
        While i < cmdCrypt.Length
            comandoByte(IdxComandoByte) = cmdCrypt(i)

            IdxComandoByte = IdxComandoByte + 1
            i = i + 1
        End While
        i = 1
        While i < IdxComandoByte
            chkSum = chkSum Xor comandoByte(i)
            i = i + 1
        End While
        comandoByte(IdxComandoByte) = chkSum
        IdxComandoByte = IdxComandoByte + 1
        comandoByte(IdxComandoByte) = 3

        Dim strAux As String = ""
        i = 0
        While i < IdxComandoByte
            strAux = strAux + Convert.ToChar(comandoByte(i))
            i = i + 1
        End While
        Send2(client, comandoByte)
        sendDone.WaitOne()

        Dim state As SocketClass = New SocketClass()

        quantBytesRec = client.Receive(state.buffer)

        response = ""
        i = 0
        While i < quantBytesRec
            response = response + Convert.ToChar(state.buffer.ElementAt(i))

            i = i + 1
        End While

        i = 0
        strRec = ""
        idxByte = 0
        Dim byteData(quantBytesRec - 5 - 1) As Byte
        While idxByte < quantBytesRec
            If idxByte >= 3 Then
                If idxByte <= quantBytesRec - 3 Then
                    byteData(i) = Convert.ToByte(response.ElementAt(idxByte))
                    i = i + 1
                    strRec = strRec + response.ElementAt(idxByte)
                End If
            End If
            idxByte = idxByte + 1
        End While
        i = 0
        While i < 16
            IV(i) = byteData(i)
            i = i + 1
        End While

        Dim byteData2(quantBytesRec - 16 - 5 - 1) As Byte
        i = 0
        While i < byteData.Length - 16

            byteData2(i) = byteData(i + 16)
            i = i + 1
        End While

        strRec = DecryptStringFromBytes_Aes(byteData2, chaveAes, IV)
        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length)
        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length)

        strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length)

        txtDataHora.Text = strRec
    End Sub
End Class


Public Class UTF8C
    Public Shared Function encode(ByVal str As String) As String
        'supply True as the construction parameter to indicate
        'that you wanted the class to emit BOM (Byte Order Mark)
        'NOTE: this BOM value is the indicator of a UTF-8 string
        Dim utf8Encoding As New System.Text.UTF8Encoding(True)
        Dim encodedString() As Byte

        encodedString = utf8Encoding.GetBytes(str)

        Return utf8Encoding.GetString(encodedString)
    End Function
End Class
Public Class StateObject
    ' Client socket.
    Public workSocket As Socket = Nothing
    ' Size of receive buffer.
    Public Const BufferSize As Integer = 256
    ' Receive buffer.
    Public buffer(BufferSize) As Byte
    ' Received data string.
    Public sb As New StringBuilder
End Class 'StateObject

Public Class SocketClass
    '   Public Cliente As System.Net.Sockets.Socket 
    Public BufferSize As Integer = 8192
    Public buffer(BufferSize) As Byte
End Class