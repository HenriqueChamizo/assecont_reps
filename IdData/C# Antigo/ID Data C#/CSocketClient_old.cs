using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace IdData
{
    #region enum

        public enum enuConnectionState
        {
            None,
            Connected,
            Disconnected,
            ConnectionAttempt,
            ConnectionAttemptFailed,
            ConnectionError,
            DataSent,
            ReceivedData
        };

    #endregion

    public class CSocketClient
    {
        public const uint BUFFER_SIZE = 16720;

        #region Delegates

            //public delegate void ConnectionDelegate(Socket soc);
            public delegate void ConnectionDelegate();
            public delegate void ErrorDelegate(string ErroMessage, int ErroCode);
            public delegate void BufferEventHandler(byte[] rgbyBuffer);

        #endregion

        #region Eventos

            public event ConnectionDelegate OnConnect;
            public event ConnectionDelegate OnDisconnect;
            public event ConnectionDelegate OnWrite;
            public event ErrorDelegate OnError;
            public event BufferEventHandler OnRead;

        #endregion

        #region Variaveis

            private AsyncCallback WorkerCallBack;
            private Socket sckMain = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            private IPEndPoint ipLocal;
            private int iTCPPort = 0;
            private byte[] rgbyBuffer = new byte[BUFFER_SIZE];
            private byte[] rgbyReceivedBytes;
            private byte[] rgbyReceivedData;
            private string strReceivedText = "";
            private string strSentText = "";
            // private string strRemoteAddress = "";
            private string strRemoteHost = "";
        
            public enuConnectionState _CurrentConnectionState;
        
        #endregion

        #region Properties

            /// <summary>
            /// Porta para conexão com o Servidor
            /// </summary>
            public int Port
            {
                get
                {
                    return (iTCPPort);
                }
            }

            /// <summary>
            /// Bytes que chegaram ao Socket
            /// </summary>
            public byte[] ReceivedBytes
            {
                get
                {
                    byte[] temp = null;
                    if (this.rgbyReceivedBytes != null)
                    {
                        temp = this.rgbyReceivedBytes;
                        this.rgbyReceivedBytes = null;
                    }
                    return (temp);
                }
            }

            /// <summary>
            /// Bytes que chegaram ao Socket
            /// </summary>
            public byte[] ReceivedData
            {
                get
                {
                    return (this.rgbyReceivedData);
                }
            }

            /// <summary>
            /// Messagem que chegou ao Socket
            /// </summary>
            public string ReceivedText
            {
                get
                {
                    string temp = this.strReceivedText;
                    this.strReceivedText = "";
                    return (temp);
                }
            }

            /// <summary>
            /// Messagem enviada pelo Socket
            /// </summary>
            public string WriteText
            {
                get
                {
                    string temp = this.strSentText;
                    this.strSentText = "";
                    return (temp);
                }
            }

            ///// <summary>
            ///// IP do Servidor
            ///// </summary>
            //public string RemoteAddress
            //{
            //    get
            //    {
            //        if (this.sckMain.Connected)
            //        {
            //            return (this.strRemoteAddress);
            //        }
            //        else
            //        {
            //            return "";
            //        }
            //    }
            //}

            /// <summary>
            /// Host do Servidor
            /// </summary>
            public string RemoteHost
            {
                get
                {
                    if (this.sckMain.Connected)
                        return (this.strRemoteHost);
                    else
                        return "";
                }
            }

            /// <summary>
            /// Retorna true se o ClientSocket estiver conectado a um Servidor
            /// </summary>
            public bool Connected
            {
                get
                {
                    return (this.sckMain.Connected);
                }
            }

            public enuConnectionState CurrentConnectionState
            {
                get
                {
                    return this._CurrentConnectionState;
                }
            }

        #endregion

        #region Construtor

            public CSocketClient(string _strIPAddress, int _iTCPPort)
            {
                try
                {
                    iTCPPort = _iTCPPort;
                    IPAddress ipAddress = IPAddress.Parse(_strIPAddress);
                    this.ipLocal = new IPEndPoint(ipAddress, this.iTCPPort);

                    this._CurrentConnectionState = enuConnectionState.None;
                    this.OnConnect += new ConnectionDelegate(SocketClient_OnConnect);
                    this.OnDisconnect += new ConnectionDelegate(SocketClient_OnDisconnect);
                    this.OnWrite += new ConnectionDelegate(SocketClient_OnWrite);
                    this.OnRead += new BufferEventHandler(SocketClient_OnRead);
                    this.OnError += new ErrorDelegate(SocketClient_OnError);
                }
                catch (Exception se)
                {
                    if (OnError != null) OnError(se.Message, 0);
                }
            }

        #endregion

        #region Methods and Events

            /// <summary>
            /// Conecta-se ao IP e Porta configurados
            /// </summary>
            public bool Connect()
            {
                try
                {
                    //Connect to the server
                    this.sckMain.BeginConnect(ipLocal, new AsyncCallback(ConfirmConnect), null);
                    return true;
                }
                catch (ArgumentException se)
                {
                    if (OnError != null) OnError(se.Message, 0);
                    return false;
                }
                catch (InvalidOperationException se)
                {
                    if (OnError != null) OnError(se.Message, 0);
                    return false;
                }
                catch (SocketException se)
                {
                    if (OnError != null) OnError(se.Message, se.ErrorCode);
                    return false;
                }
            }

            /// <summary>
            /// Desfaz a conexão com o Servidor
            /// </summary>
            public bool Disconnect()
            {
                this.sckMain.Close();

                if (!sckMain.Connected)
                {
                    if (OnDisconnect != null) OnDisconnect();
                    return true;
                }
                else
                    return false;
            }

            /// <summary>
            /// Desabilita a recepção e envio que estão processando
            /// </summary>
            public void ShutDown()
            {
                this.sckMain.Shutdown(SocketShutdown.Both);
            }

            private void ConfirmConnect(IAsyncResult asyn)
            {
                try
                {
                    this.sckMain.EndConnect(asyn);
                    WaitForData(sckMain);

                    if (OnConnect != null) OnConnect();
                }
                catch (ObjectDisposedException se)
                {
                    if (OnError != null) OnError(se.Message, 0);
                }
                catch (SocketException se)
                {
                    if (OnError != null) OnError(se.Message, 0);
                }
            }

            private void WaitForData(Socket soc)
            {
                try
                {
                    if (this.WorkerCallBack == null)
                    {
                        this.WorkerCallBack = new AsyncCallback(OnDataReceived);
                    }

                    Array.Clear(this.rgbyBuffer, 0, this.rgbyBuffer.Length);

                    if (soc != null && soc.Connected)
                    {
                        soc.BeginReceive(this.rgbyBuffer, 0, this.rgbyBuffer.Length, SocketFlags.None, this.WorkerCallBack, null);
                    }
                }
                catch (SocketException se)
                {
                    if (OnError != null) OnError(se.Message, se.ErrorCode);
                }
            }

            private void OnDataReceived(IAsyncResult asyn)
            {
                try
                {
                    int iRx = sckMain.EndReceive(asyn);

                    if (iRx < 1)
                    {
                        this.sckMain.Shutdown(SocketShutdown.Both);
                        this.sckMain.Close();
                        if (this.sckMain.Connected == false)
                        {
                            if (OnDisconnect != null) { OnDisconnect(); }
                        }
                    }
                    else
                    {
                        this.rgbyReceivedBytes = new byte[iRx];
                        Array.Copy(this.rgbyBuffer, this.rgbyReceivedBytes, iRx);

                        char[] chars = new char[iRx + 1];
                        Decoder d = Encoding.UTF8.GetDecoder();
                        d.GetChars(this.rgbyBuffer, 0, iRx, chars, 0);
                        this.strReceivedText = new String(chars);

                        if (OnRead != null) OnRead(this.rgbyReceivedBytes);

                        this.WaitForData(this.sckMain);
                    }
                }
                catch (ArgumentException se)
                {
                    if (OnError != null) OnError(se.Message, 0);
                }
                catch (InvalidOperationException se)
                {
                    this.sckMain.Close();

                    if (this.sckMain.Connected == false)
                    {
                        if (OnDisconnect != null) OnDisconnect();
                    }
                    if (OnError != null) OnError(se.Message, 0);
                }
                catch (SocketException se)
                {
                    if (OnError != null) OnError(se.Message, se.ErrorCode);

                    if (this.sckMain.Connected == false)
                    {
                        if (OnDisconnect != null) OnDisconnect();
                    }
                }
                catch (Exception ex)
                {
                    if (OnError != null) OnError(ex.Message, 0);
                }
            }

            /// <summary>
            /// Envia um vetor de bytes pela conexão
            /// </summary>
            /// <param name="byData">Buffer de dados a serem transmitidos</param>
            /// <param name="iNumBytes">Quantidade de bytes a serem enviados</param>
            /// <returns></returns>
            public bool SendBuffer(byte[] _rgbyDataBuffer)
            {
                try
                {
                    int NumBytes = this.sckMain.Send(_rgbyDataBuffer, _rgbyDataBuffer.Length, SocketFlags.None);

                    if (NumBytes == _rgbyDataBuffer.Length)
                    {
                        if (OnWrite != null)
                        {
                            strSentText = _rgbyDataBuffer.ToString();
                            OnWrite();
                        }
                        return true;
                    }
                    else
                        return false;
                }
                catch (ArgumentException se)
                {
                    if (OnError != null) OnError(se.Message, 0);
                    return false;
                }
                catch (ObjectDisposedException se)
                {
                    if (OnError != null) OnError(se.Message, 0);
                    return false;
                }
                catch (SocketException se)
                {
                    if (OnError != null) OnError(se.Message, se.ErrorCode);
                    return false;
                }
            }

        #endregion

        #region Events

            private void SocketClient_OnConnect()
            {
                this._CurrentConnectionState = enuConnectionState.Connected;
            }

            private void SocketClient_OnDisconnect()
            {
                if (this._CurrentConnectionState == enuConnectionState.ConnectionAttempt)
                {
                    this._CurrentConnectionState = enuConnectionState.ConnectionAttemptFailed;
                }
                else
                {
                    this._CurrentConnectionState = enuConnectionState.Disconnected;
                }
            }

            private void SocketClient_OnWrite()
            {
                this._CurrentConnectionState = enuConnectionState.DataSent;
            }

            private void SocketClient_OnRead(byte[] rgbyBuffer)
            {
                this.rgbyReceivedData = rgbyBuffer;
                this._CurrentConnectionState = enuConnectionState.ReceivedData;
            }

            private void SocketClient_OnError(string strErrorMessage, int iErrorCode)
            {
                if (this._CurrentConnectionState == enuConnectionState.ConnectionAttempt)
                {
                    this._CurrentConnectionState = enuConnectionState.ConnectionAttemptFailed;
                }
            }

        #endregion
    }
}
