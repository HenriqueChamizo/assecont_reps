using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ExemploComunicacaoCSharp
{
    public partial class Form1 : Form
    {
        private static String response = String.Empty;
        private const int port = 3000;
        // ManualResetEvent instances signal completion.
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        private Socket client;

        private byte[] chaveAes = new byte[16];
        private byte[] bufferBytes = new byte[1024];

        byte[] biometriaExemplo = new byte[384];

        private static int quantBytesRec = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try {
                // Establish the remote endpoint for the socket.
                // The name of the 
                // remote device is "host.contoso.com".
                IPHostEntry ipHostInfo = Dns.Resolve(txtIP.Text);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
                string command = "";
                string preCommand = "";
                int idxByte = 0;
                string strModulo = "";
                string strExpodente = "";
                string strRec = "";
                byte chkSum = 0;
                string strComandoComCriptografia = "";
                string strAux = "";

                int i = 0;
                                
                if (client == null)
                {
                    client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                }
                // Create a TCP/IP socket.
                bool conectado = client.Connected;

                if (conectado == false)
                {
                    // Connect to the remote endpoint.
                    client.BeginConnect(remoteEP,
                        new AsyncCallback(ConnectCallback), client);
                    connectDone.WaitOne();
                }
                

                Random rnd = new Random();

                chaveAes[0] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[1] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[2] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[3] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[4] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[5] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[6] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[7] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[8] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[9] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[10] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[11] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[12] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[13] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[14] = Convert.ToByte(rnd.Next(1, 256));
                chaveAes[15] = Convert.ToByte(rnd.Next(1, 256));

                command = "";
                command = command + (char)(2);
                // start byte

                preCommand = preCommand + (char)(7);
                // tamanho do comando
                preCommand = preCommand + (char)(0);
                // tamanho do comando
                preCommand = preCommand + "1+RA+00";
                chkSum = calcCheckSumString(preCommand);

                command = command + preCommand;
                command = command + Convert.ToChar(chkSum);
                // checksum
                // end byte
                command = command + (char)(3);


                // Send test data to the remote device.
                Send(client, command);
                sendDone.WaitOne();

                quantBytesRec = client.Receive(bufferBytes);

                response = "";
                while (i < quantBytesRec)
                {
                    response = response + (char)bufferBytes[i];

                    i = i + 1;
                }



                while (idxByte < quantBytesRec)
                {
                    if (idxByte >= 3)
                    {
                        if (idxByte <= quantBytesRec - 3)
                        {
                            strRec = strRec + response.ElementAt(idxByte);
                        }
                    }
                    idxByte = idxByte + 1;
                }
                strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1 );
                strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
                strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

                strModulo = Mid(strRec, 1, strRec.IndexOf("]"));
                strExpodente = Trim(Mid(strRec, strRec.IndexOf("]") + 2, strRec.Length - strRec.IndexOf("]") - 1));

                strAux = "1]" + txtUsuario.Text + "]" + txtSenha.Text + "]" + System.Convert.ToBase64String(chaveAes);
                
                RSAPersistKeyInCSP(strModulo);
                byte[] dataToEncrypt = Encoding.Default.GetBytes(strAux);
                byte[] encryptedData = null;

                RSAParameters RSAKeyInfo = new RSAParameters();

                RSAKeyInfo.Modulus = System.Convert.FromBase64String(strModulo);
                RSAKeyInfo.Exponent = System.Convert.FromBase64String(strExpodente);

                encryptedData = RSAEncrypt(dataToEncrypt, RSAKeyInfo, false );

                strAux = System.Convert.ToBase64String(encryptedData);


                strComandoComCriptografia = "2+EA+00+" + strAux;

                preCommand = "";
                command = "";
                command = command + Convert.ToChar(2);
                // start byte
                preCommand = preCommand + Convert.ToChar(strComandoComCriptografia.Length);
                // tamanho do comando
                preCommand = preCommand + Convert.ToChar(0);
                // tamanho do comando
                preCommand = preCommand + strComandoComCriptografia;
                chkSum = calcCheckSumString(preCommand);

                command = command + preCommand;
                command = command + Convert.ToChar(chkSum);
                // checksum

                command = command + Convert.ToChar(3);
                // end byte
                Send(client, command);
                sendDone.WaitOne();

                quantBytesRec = client.Receive(bufferBytes);
                
                response = "";
	            i = 0;
	            while (i < quantBytesRec) {
		            
                    response = response + Convert.ToChar(bufferBytes[i]);
		            i = i + 1;
	            }

	            strRec = "";
	            idxByte = 0;
	            while (idxByte < quantBytesRec) {
		            if (idxByte >= 3) {
			            if (idxByte <= quantBytesRec - 3) {
				            strRec = strRec + response.ElementAt(idxByte);
			            }
		            }
		            idxByte = idxByte + 1;
	            }
                strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
                strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

	            if (strRec == "000") {
		            // Write the response to the console.
		            MessageBox.Show("Autenticado");
	            } else {
                    MessageBox.Show("Não autenticado.(" + strRec + ")");
		            return;
	            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                //Import the RSA Key information. This only needs
                //toinclude the public key information.
                RSA.ImportParameters(RSAKeyInfo);

                //Encrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                return RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        static public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                //Import the RSA Key information. This needs
                //to include the private key information.
                RSA.ImportParameters(RSAKeyInfo);

                //Decrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                return RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }

        public static void RSAPersistKeyInCSP(string ContainerName)
        {
            try
            {
                // Create a new instance of CspParameters.  Pass
                // 13 to specify a DSA container or 1 to specify
                // an RSA container.  The default is 1.
                CspParameters cspParams = new CspParameters();

                // Specify the container name using the passed variable.
                cspParams.KeyContainerName = ContainerName;

                //Create a new instance of RSACryptoServiceProvider to generate
                //a new key pair.  Pass the CspParameters class to persist the 
                //key in the container.  The PersistKeyInCsp property is true by 
                //default, allowing the key to be persisted. 
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);

                //Indicate that the key was persisted.
                Console.WriteLine("The RSA key was persisted in the container, \"{0}\".", ContainerName);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

            }
        }

        public static void RSADeleteKeyInCSP(string ContainerName)
        {
            try
            {
                // Create a new instance of CspParameters.  Pass
                // 13 to specify a DSA container or 1 to specify
                // an RSA container.  The default is 1.
                CspParameters cspParams = new CspParameters();

                // Specify the container name using the passed variable.
                cspParams.KeyContainerName = ContainerName;

                //Create a new instance of RSACryptoServiceProvider. 
                //Pass the CspParameters class to use the 
                //key in the container.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);

                //Explicitly set the PersistKeyInCsp property to false
                //to delete the key entry in the container.
                RSAalg.PersistKeyInCsp = false;

                //Call Clear to release resources and delete the key from the container.
                RSAalg.Clear();

                //Indicate that the key was persisted.
                Console.WriteLine("The RSA key was deleted from the container, \"{0}\".", ContainerName);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

            }
        }
        public static string Trim(string s)
        {
            return s.Trim();
        }
        public static string Mid(string s, int a, int b)
        {
            string temp = s.Substring(a - 1, b);
            return temp;
        }
        public byte calcCheckSumString(string data)
        {
            String strBuf = "";
            String strAux = "";
            byte cks = 0;
            int i = 0;

            while (i < data.Length)
            {
                strAux = ((byte)(data.ElementAt(i))).ToString("X2");
                strBuf = strBuf + strAux;
                cks = (byte)(cks ^ (byte)(data.ElementAt(i)));
                i = i + 1;
            }
            return cks;
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Receive(Socket client)
        {
            try
            {
                // Create the state object.
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket 
                // from the asynchronous state object.
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Send(Socket client, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.Default.GetBytes(data);

            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void Send2(Socket client, byte[] byteData)
        {
            
            // Begin sending the data to the remote device.
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key,byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                aesAlg.Padding = PaddingMode.None;
                aesAlg.Mode = CipherMode.CBC;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);

                            int quant = plainText.Length;
                            
                            if (quant > 16)
                            {
                                quant = quant % 16;
                            }
                            else
                            {

                            }
                            quant = 16 - quant;
                            while (quant < 16 && quant != 0)
                            {
                                swEncrypt.Write(Convert.ToChar(Convert.ToByte("0")));
                                quant = quant - 1;
                            }
                                                        

                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        static byte[] EncryptStringToBytes_Aes2(String plainText, byte[] plainText2, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (plainText2 == null || plainText2.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                aesAlg.Padding = PaddingMode.None;
                aesAlg.Mode = CipherMode.CBC;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] bytesStr = Encoding.Default.GetBytes(plainText);
                        csEncrypt.Write(bytesStr, 0, bytesStr.Length);
                        csEncrypt.Write(plainText2, 0, plainText2.Length);
                        int quant = bytesStr.Length + plainText2.Length;

                        if (quant > 16)
                        {
                            quant = quant % 16;
                        }
                        quant = 16 - quant;
                        byte[] bytesZeros = new byte[16] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                                                          0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};
                        if (quant < 16 && quant != 0)
                        {
                            csEncrypt.Write(bytesZeros, 0, quant);
                            quant = quant - 1;
                        }
                        //using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        //{
                            
                            //swEncrypt.Write(plainText);

                            
                            //swEncrypt.BaseStream.Write(plainText2, 0, plainText2.Length);
                            
                            /*
                            int x = 0;
                            while (x < plainText2.Length) {
                                swEncrypt.Write(Convert.ToChar(plainText2.ElementAt(x)));
                                //swEncrypt.BaseStream.Write(plainText2, x, 1);
                                x = x + 1;
                            }
                            /**/
                            
                        //}
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }
        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                aesAlg.Padding = PaddingMode.None;
                aesAlg.Mode = CipherMode.CBC;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

        static byte[] DecryptStringFromBytes_Aes2(byte[] cipherText, byte[] Key, byte[] IV)
        {

            byte[] bufferDecrypt = new byte[cipherText.Length];

            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                aesAlg.Padding = PaddingMode.None;
                aesAlg.Mode = CipherMode.CBC;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        csDecrypt.Read(bufferDecrypt, 0, bufferDecrypt.Length);

                    }
                }

            }

            return bufferDecrypt;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            string strComandoComCriptografia = "01+RH+00";
            int i = 0;
            int chkSum = 0;
            string strRec = "";
            int idxByte = 0;

            
            byte[] IV = new byte[16];
            IV[0] = Convert.ToByte(rnd.Next(1, 256));
            IV[1] = Convert.ToByte(rnd.Next(1, 256));
            IV[2] = Convert.ToByte(rnd.Next(1, 256));
            IV[3] = Convert.ToByte(rnd.Next(1, 256));
            IV[4] = Convert.ToByte(rnd.Next(1, 256));
            IV[5] = Convert.ToByte(rnd.Next(1, 256));
            IV[6] = Convert.ToByte(rnd.Next(1, 256));
            IV[7] = Convert.ToByte(rnd.Next(1, 256));
            IV[8] = Convert.ToByte(rnd.Next(1, 256));
            IV[9] = Convert.ToByte(rnd.Next(1, 256));
            IV[10] = Convert.ToByte(rnd.Next(1, 256));
            IV[11] = Convert.ToByte(rnd.Next(1, 256));
            IV[12] = Convert.ToByte(rnd.Next(1, 256));
            IV[13] = Convert.ToByte(rnd.Next(1, 256));
            IV[14] = Convert.ToByte(rnd.Next(1, 256));
            IV[15] = Convert.ToByte(rnd.Next(1, 256));


            int tamanhoPacote = 32;
            byte[] comandoByte = new byte[37];
            int IdxComandoByte = 3;
            comandoByte[0] = 2;
            // start byte
            comandoByte[1] = (byte)(tamanhoPacote & 0xff);
            // tamanho
            comandoByte[2] = (byte)((tamanhoPacote >> 8) & 0xff);
            // tamanho

            byte[] cmdCrypt = Encoding.Default.GetBytes(Encoding.Default.GetChars(EncryptStringToBytes_Aes(strComandoComCriptografia, chaveAes, IV)));
            chkSum = 0;
            i = 0;
            while (i < IV.Length)
            {
                comandoByte[IdxComandoByte] = IV[i];

                IdxComandoByte = IdxComandoByte + 1;
                i = i + 1;
            }

            i = 0;
            while (i < cmdCrypt.Length)
            {
                comandoByte[IdxComandoByte] = cmdCrypt[i];

                IdxComandoByte = IdxComandoByte + 1;
                i = i + 1;
            }
            i = 1;
            while (i < IdxComandoByte)
            {
                chkSum = chkSum ^ comandoByte[i];
                i = i + 1;
            }
            comandoByte[IdxComandoByte] = (byte)chkSum;
            IdxComandoByte = IdxComandoByte + 1;
            comandoByte[IdxComandoByte] = 3;

            string strAux = "";
            i = 0;
            while (i < IdxComandoByte)
            {
                strAux = strAux + Convert.ToChar(comandoByte[i]);
                i = i + 1;
            }
            Send2(client, comandoByte);
            
            sendDone.WaitOne();

            
            quantBytesRec = client.Receive(bufferBytes);

            response = "";
            i = 0;
            while (i < quantBytesRec)
            {
                
                response = response + (char)bufferBytes[i];

                i = i + 1;
            }

            i = 0;
            strRec = "";
            idxByte = 0;
            byte[] byteData = new byte[quantBytesRec - 5];
            while (idxByte < quantBytesRec)
            {
                if (idxByte >= 3)
                {
                    if (idxByte <= quantBytesRec - 3)
                    {
                        byteData[i] = Convert.ToByte(response.ElementAt(idxByte));
                        i = i + 1;
                        strRec = strRec + response.ElementAt(idxByte);
                    }
                }
                idxByte = idxByte + 1;
            }
            i = 0;
            while (i < 16)
            {
                IV[i] = byteData[i];
                i = i + 1;
            }

            byte[] byteData2 = new byte[quantBytesRec - 16 - 5];
            i = 0;

            while (i < byteData.Length - 16)
            {
                byteData2[i] = byteData[i + 16];
                i = i + 1;
            }

            strRec = DecryptStringFromBytes_Aes(byteData2, chaveAes, IV);
            strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
            strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

            strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

            txtDataHora.Text = strRec;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            string strComandoComCriptografia = "04+RD+00+D]" + txtMatricula.Text ;
            int i = 0;
            int chkSum = 0;
            string strRec = "";
            int idxByte = 0;


            byte[] IV = new byte[16];
            IV[0] = Convert.ToByte(rnd.Next(1, 256));
            IV[1] = Convert.ToByte(rnd.Next(1, 256));
            IV[2] = Convert.ToByte(rnd.Next(1, 256));
            IV[3] = Convert.ToByte(rnd.Next(1, 256));
            IV[4] = Convert.ToByte(rnd.Next(1, 256));
            IV[5] = Convert.ToByte(rnd.Next(1, 256));
            IV[6] = Convert.ToByte(rnd.Next(1, 256));
            IV[7] = Convert.ToByte(rnd.Next(1, 256));
            IV[8] = Convert.ToByte(rnd.Next(1, 256));
            IV[9] = Convert.ToByte(rnd.Next(1, 256));
            IV[10] = Convert.ToByte(rnd.Next(1, 256));
            IV[11] = Convert.ToByte(rnd.Next(1, 256));
            IV[12] = Convert.ToByte(rnd.Next(1, 256));
            IV[13] = Convert.ToByte(rnd.Next(1, 256));
            IV[14] = Convert.ToByte(rnd.Next(1, 256));
            IV[15] = Convert.ToByte(rnd.Next(1, 256));


            int tamanhoPacote = 32;
            byte[] comandoByte = new byte[37];
            int IdxComandoByte = 3;
            comandoByte[0] = 2;
            // start byte
            comandoByte[1] = (byte)(tamanhoPacote & 0xff);
            // tamanho
            comandoByte[2] = (byte)((tamanhoPacote >> 8) & 0xff);
            // tamanho

            byte[] cmdCrypt = Encoding.Default.GetBytes(Encoding.Default.GetChars(EncryptStringToBytes_Aes(strComandoComCriptografia, chaveAes, IV)));
            chkSum = 0;
            i = 0;
            while (i < IV.Length)
            {
                comandoByte[IdxComandoByte] = IV[i];

                IdxComandoByte = IdxComandoByte + 1;
                i = i + 1;
            }

            i = 0;
            while (i < cmdCrypt.Length)
            {
                comandoByte[IdxComandoByte] = cmdCrypt[i];

                IdxComandoByte = IdxComandoByte + 1;
                i = i + 1;
            }
            i = 1;
            while (i < IdxComandoByte)
            {
                chkSum = chkSum ^ comandoByte[i];
                i = i + 1;
            }
            comandoByte[IdxComandoByte] = (byte)chkSum;
            IdxComandoByte = IdxComandoByte + 1;
            comandoByte[IdxComandoByte] = 3;

            string strAux = "";
            i = 0;
            while (i < IdxComandoByte)
            {
                strAux = strAux + Convert.ToChar(comandoByte[i]);
                i = i + 1;
            }
            Send2(client, comandoByte);

            sendDone.WaitOne();


            quantBytesRec = client.Receive(bufferBytes);

            response = "";
            i = 0;
            while (i < quantBytesRec)
            {

                response = response + (char)bufferBytes[i];

                i = i + 1;
            }

            i = 0;
            strRec = "";
            idxByte = 0;
            byte[] byteData = new byte[quantBytesRec - 5];
            while (idxByte < quantBytesRec)
            {
                if (idxByte >= 3)
                {
                    if (idxByte <= quantBytesRec - 3)
                    {
                        byteData[i] = Convert.ToByte(response.ElementAt(idxByte));
                        i = i + 1;
                        strRec = strRec + response.ElementAt(idxByte);
                    }
                }
                idxByte = idxByte + 1;
            }
            i = 0;
            while (i < 16)
            {
                IV[i] = byteData[i];
                i = i + 1;
            }

            byte[] byteData2 = new byte[quantBytesRec - 16 - 5];
            i = 0;

            while (i < byteData.Length - 16)
            {
                byteData2[i] = byteData[i + 16];
                i = i + 1;
            }

            byte[] bufferRecDecrypt = DecryptStringFromBytes_Aes2(byteData2, chaveAes, IV);
            i = 0;
            while (i < bufferRecDecrypt.Length)
            {
                if (Convert.ToChar(bufferRecDecrypt[i]) == '{')
                {
                    break;
                }
                i = i + 1;
            }
            i = i + 1;

            int x = 0;
            while (x < 384)
            {
                biometriaExemplo[x] = bufferRecDecrypt[i + x];
                x = x + 1;
            }
             

            MessageBox.Show("Biometria " + txtMatricula.Text + " recebida.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            string strComandoComCriptografia = "05+ED+00+D]" + txtMatricula.Text + "}1}1{";
            int i = 0;
            

                        
            int chkSum = 0;
            string strRec = "";
            int idxByte = 0;

            byte[] IV = new byte[16];
            IV[0] = Convert.ToByte(rnd.Next(1, 256));
            IV[1] = Convert.ToByte(rnd.Next(1, 256));
            IV[2] = Convert.ToByte(rnd.Next(1, 256));
            IV[3] = Convert.ToByte(rnd.Next(1, 256));
            IV[4] = Convert.ToByte(rnd.Next(1, 256));
            IV[5] = Convert.ToByte(rnd.Next(1, 256));
            IV[6] = Convert.ToByte(rnd.Next(1, 256));
            IV[7] = Convert.ToByte(rnd.Next(1, 256));
            IV[8] = Convert.ToByte(rnd.Next(1, 256));
            IV[9] = Convert.ToByte(rnd.Next(1, 256));
            IV[10] = Convert.ToByte(rnd.Next(1, 256));
            IV[11] = Convert.ToByte(rnd.Next(1, 256));
            IV[12] = Convert.ToByte(rnd.Next(1, 256));
            IV[13] = Convert.ToByte(rnd.Next(1, 256));
            IV[14] = Convert.ToByte(rnd.Next(1, 256));
            IV[15] = Convert.ToByte(rnd.Next(1, 256));


            
            byte[] comandoByte;
            int IdxComandoByte = 3;
            
            byte[] cmdCrypt = null;

            i = 0;
            
            cmdCrypt = Encoding.Default.GetBytes(Encoding.Default.GetChars(EncryptStringToBytes_Aes2(strComandoComCriptografia, biometriaExemplo, chaveAes, IV)));
            
            comandoByte = new byte[cmdCrypt.Length + 21];
            int tamanhoPacote = cmdCrypt.Length + 21 - 5;
            
            comandoByte[0] = 2;
            // start byte
            comandoByte[1] = (byte)(tamanhoPacote & 0xff);
            // tamanho
            comandoByte[2] = (byte)((tamanhoPacote >> 8) & 0xff);
            // tamanho

            chkSum = 0;
            i = 0;
            while (i < IV.Length)
            {
                comandoByte[IdxComandoByte] = IV[i];

                IdxComandoByte = IdxComandoByte + 1;
                i = i + 1;
            }

            i = 0;
            while (i < cmdCrypt.Length)
            {
                comandoByte[IdxComandoByte] = cmdCrypt[i];

                IdxComandoByte = IdxComandoByte + 1;
                i = i + 1;
            }
            i = 1;
            while (i < IdxComandoByte)
            {
                chkSum = chkSum ^ comandoByte[i];
                i = i + 1;
            }
            comandoByte[IdxComandoByte] = (byte)chkSum;
            IdxComandoByte = IdxComandoByte + 1;
            comandoByte[IdxComandoByte] = 3;

            string strAux = "";
            i = 0;
            while (i < IdxComandoByte)
            {
                strAux = strAux + Convert.ToChar(comandoByte[i]);
                i = i + 1;
            }
            Send2(client, comandoByte);

            sendDone.WaitOne();


            quantBytesRec = client.Receive(bufferBytes);

            response = "";
            i = 0;
            while (i < quantBytesRec)
            {

                response = response + (char)bufferBytes[i];

                i = i + 1;
            }

            i = 0;
            strRec = "";
            idxByte = 0;
            byte[] byteData = new byte[quantBytesRec - 5];
            while (idxByte < quantBytesRec)
            {
                if (idxByte >= 3)
                {
                    if (idxByte <= quantBytesRec - 3)
                    {
                        byteData[i] = Convert.ToByte(response.ElementAt(idxByte));
                        i = i + 1;
                        strRec = strRec + response.ElementAt(idxByte);
                    }
                }
                idxByte = idxByte + 1;
            }
            i = 0;
            while (i < 16)
            {
                IV[i] = byteData[i];
                i = i + 1;
            }

            byte[] byteData2 = new byte[quantBytesRec - 16 - 5];
            i = 0;

            while (i < byteData.Length - 16)
            {
                byteData2[i] = byteData[i + 16];
                i = i + 1;
            }

            strRec = DecryptStringFromBytes_Aes(byteData2, chaveAes, IV);
            strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
            strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

            strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

            
            MessageBox.Show("Biometria " + txtMatricula.Text + " enviada para o equipamento.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            #region Pré Send
            Random rnd = new Random();

            string dia = DateTime.Now.Day <= 9 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month <= 9 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string ano = (DateTime.Now.Year - 2000).ToString();
            string hora = DateTime.Now.Hour <= 9 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            string min = DateTime.Now.Minute <= 9 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
            string strComandoSemCriptografia = "01+EH+00+";
            string strComandoComCriptografia = String.Format("{0}/{1}/{2} {3}:{4}:00]00/00/00]00/00/00", dia, mes, ano, hora, min);
            int chkSum = 0;
            string strRec = "";
            int idxByte = 0;


            byte[] IV = new byte[16];
            IV[0] = Convert.ToByte(rnd.Next(1, 256));
            IV[1] = Convert.ToByte(rnd.Next(1, 256));
            IV[2] = Convert.ToByte(rnd.Next(1, 256));
            IV[3] = Convert.ToByte(rnd.Next(1, 256));
            IV[4] = Convert.ToByte(rnd.Next(1, 256));
            IV[5] = Convert.ToByte(rnd.Next(1, 256));
            IV[6] = Convert.ToByte(rnd.Next(1, 256));
            IV[7] = Convert.ToByte(rnd.Next(1, 256));
            IV[8] = Convert.ToByte(rnd.Next(1, 256));
            IV[9] = Convert.ToByte(rnd.Next(1, 256));
            IV[10] = Convert.ToByte(rnd.Next(1, 256));
            IV[11] = Convert.ToByte(rnd.Next(1, 256));
            IV[12] = Convert.ToByte(rnd.Next(1, 256));
            IV[13] = Convert.ToByte(rnd.Next(1, 256));
            IV[14] = Convert.ToByte(rnd.Next(1, 256));
            IV[15] = Convert.ToByte(rnd.Next(1, 256));
            #endregion

            #region Send Bytes
            string strAux = "";
            byte[] cmd = Encoding.Default.GetBytes(strComandoSemCriptografia);
            byte[] Crypt = Encoding.Default.GetBytes(Encoding.Default.GetChars(EncryptStringToBytes_Aes(strComandoComCriptografia, chaveAes, IV)));
            byte[] cmdCrypt = new byte[cmd.Length + Crypt.Length];
            for (int i = 0; i < cmd.Length; i++)
            {
                cmdCrypt[i] = cmd[i];
            }
            for (int i = cmd.Length; i - cmd.Length < Crypt.Length; i++)
            {
                cmdCrypt[i] = Crypt[i - cmd.Length];
            }

            int tamanhoPacote = cmdCrypt.Length;
            byte[] comandoByte = new byte[tamanhoPacote + 5];
            int IdxComandoByte = 3;
            comandoByte[0] = 2;
            // start byte
            comandoByte[1] = (byte)(tamanhoPacote & 0xff);
            // tamanho
            comandoByte[2] = (byte)((tamanhoPacote >> 8) & 0xff);
            // tamanho
            chkSum = 0;

            //for (int i = 0; i < IV.Length; i++)
            //{
            //    comandoByte[IdxComandoByte] = IV[i];
            //    IdxComandoByte = IdxComandoByte + 1;
            //}

            for (int i = 0; i < cmdCrypt.Length; i++)
            {
                comandoByte[IdxComandoByte] = cmdCrypt[i];
                IdxComandoByte = IdxComandoByte + 1;
            }

            for (int i = 1; i < IdxComandoByte; i++)
            {
                chkSum = chkSum ^ comandoByte[i];
            }
            comandoByte[IdxComandoByte] = (byte)chkSum;
            IdxComandoByte = IdxComandoByte + 1;
            comandoByte[IdxComandoByte] = 3;

            for (int i = 0; i < IdxComandoByte; i++)
            {
                strAux = strAux + Convert.ToChar(comandoByte[i]);
            }
            Send2(client, comandoByte);
            #endregion

            #region Send String (Comentado)
            //string preCommand;
            //string command;
            //byte[] Crypt = Encoding.Default.GetBytes(Encoding.Default.GetChars(EncryptStringToBytes_Aes(strComandoComCriptografia, chaveAes, IV)));
            //for (int i = 0; i < Crypt.Length; i++)
            //{
            //    strComandoSemCriptografia = strComandoSemCriptografia + (char)Crypt[i];
            //}
            //preCommand = "";
            //command = "";
            //command = command + Convert.ToChar(2);
            //// start byte
            //preCommand = preCommand + Convert.ToChar(strComandoSemCriptografia.Length);
            //// tamanho do comando
            //preCommand = preCommand + Convert.ToChar(0);
            //// tamanho do comando
            //preCommand = preCommand + strComandoSemCriptografia;

            //byte[] comandoByte = Encoding.Default.GetBytes(preCommand);
            //for (int i = 0; i < comandoByte.Length; i++)
            //{
            //    chkSum = chkSum ^ comandoByte[i];
            //}

            ////chkSum = calcCheckSumString(preCommand);

            //command = command + preCommand;
            //command = command + Convert.ToChar(chkSum);
            //// checksum

            //command = command + Convert.ToChar(3);
            //// end byte
            //Send(client, command);
            #endregion

            #region Pós Send
            sendDone.WaitOne();

            quantBytesRec = client.Receive(bufferBytes);

            response = "";
            for (int i = 0; i < quantBytesRec; i++)
            {
                response = response + (char)bufferBytes[i];
            }

            strRec = "";
            idxByte = 0;
            byte[] byteData = new byte[quantBytesRec - 5];
            for (int i = 0; i < quantBytesRec; i++)
            {
                if (i >= 3)
                {
                    if (i <= quantBytesRec - 3)
                    {
                        byteData[idxByte] = Convert.ToByte(response.ElementAt(i));
                        idxByte++;
                        strRec = strRec + response.ElementAt(i);
                    }
                }
            }

            for (int i = 0; i < 16; i++)
            {
                IV[i] = byteData[i];
            }

            byte[] byteData2 = new byte[quantBytesRec - 16 - 5];

            for (int i = 0; i < byteData.Length - 16; i++)
            {
                byteData2[i] = byteData[i + 16];
            }

            strRec = DecryptStringFromBytes_Aes(byteData2, chaveAes, IV);
            strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
            strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);

            strRec = Mid(strRec, strRec.IndexOf("+") + 2, strRec.Length - strRec.IndexOf("+") - 1);
            #endregion 

            txtDataHora.Text = strRec;
        }
    }
    // State object for receiving data from remote device.
    public class StateObject
    {
        // Client socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 256;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }


}
