using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace iddata.async_communication
{
    #region enum

        public enum enumConnState
        {
            None,
            Connected,
            Disconnected,
            ConnectionAttempt,
            ConnectionAttemptFailed,
            DisconnectionAttempt,
            ConnectionError,
            DataSent,
            SendingData,
            Waiting,
            ReceivingData,
            ProcessingData,
            ReceivedData,
            ReceivedDataWithError,
            CommandFailed,
            GeneratingAFD,
            AFDGenerated,
            AFDNoEvents,
            AFDError
        };

    #endregion
}

namespace iddata.async_communication.business.client
{
    public class CSocketClient
    {
        #region const

            private const uint BUFFER_SIZE = 16720;
            private const int TIMEOUT = 5000;

        #endregion

        #region delegates

            private delegate void ConnectionDelegate();
            private delegate void ErrorDelegate(string ErroMessage, int ErroCode);
            private delegate void BufferEventHandler(byte[] rgbyBuffer);

        #endregion

        #region events

            private event ConnectionDelegate OnConnect;
            private event ConnectionDelegate OnConnectAttempt;
            private event ConnectionDelegate OnDisconnect;
            private event ConnectionDelegate OnDisconnectAttempt;
            private event ConnectionDelegate OnWrite;
            private event BufferEventHandler OnRead;
            private event ErrorDelegate OnError;

        #endregion

        #region variables

            private AsyncCallback WorkerCallBack;
            private Socket sckMain;
            private IPEndPoint ipLocal;
            private int iPort = 0;
            private byte[] rgbyBuffer = new byte[BUFFER_SIZE];
            private byte[] rgbyReceivedData;

            private enumConnState connState;
            private string _strErrorMessage;

        #endregion

        #region properties

            public string ErrorMessage
            {
                get
                {
                    return this._strErrorMessage;
                }
            }

            public bool Connected
            {
                get
                {
                    return (this.sckMain.Connected);
                }
            }

            public enumConnState CurrentConnectionState
            {
                get
                {
                    return this.connState;
                }
            }


        #endregion

        #region construtor

            public CSocketClient(string strIP, int iPort)
            {
                try
                {
                    this.iPort = iPort;
                    IPAddress ipAddress = IPAddress.Parse(strIP);
                    this.ipLocal = new IPEndPoint(ipAddress, this.iPort);

                    this.connState = enumConnState.None;

                    this.OnConnect += new ConnectionDelegate(SocketClient_OnConnect);
                    this.OnConnectAttempt += new ConnectionDelegate(SocketClient_OnConnectAttempt);
                    this.OnDisconnect += new ConnectionDelegate(SocketClient_OnDisconnect);
                    this.OnDisconnectAttempt += new ConnectionDelegate(SocketClient_OnDisconnectAttempt);
                    this.OnWrite += new ConnectionDelegate(SocketClient_OnWrite);
                    this.OnRead += new BufferEventHandler(SocketClient_OnRead);
                    this.OnError += new ErrorDelegate(SocketClient_OnError);

                    this.sckMain = null;

                    this._strErrorMessage = "";
                }
                catch (Exception se)
                {
                    if (OnError != null) OnError(se.Message, 0);
                }
            }

        #endregion

        #region methods

            public enumConnState GetConnState()
            {
                return this.connState;
            }

            public byte[] GetReceivedData()
            {
                return this.rgbyReceivedData;
            }

            /// <summary>
            /// Conecta-se ao IP e Porta configurados
            /// </summary>
            public bool Connect()
            {
                try
                {
                    if (OnConnectAttempt != null) OnConnectAttempt();

                    this.sckMain = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    this.sckMain.ReceiveTimeout = TIMEOUT;
                    this.sckMain.SendTimeout = TIMEOUT;

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
                try
                {
                    if (OnDisconnectAttempt != null) OnDisconnectAttempt();

                    this.sckMain.Close();

                    if (this.sckMain.Connected == false)
                    {
                        if (OnDisconnect != null) OnDisconnect();
                        return true;
                    }

                    return false;
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
            /// Cancela a recepção de dados
            /// </summary>
            public void CancelReceive()
            {
                this.sckMain.Shutdown(SocketShutdown.Receive);
            }

            /// <summary>
            /// Cancela o envio de dados
            /// </summary>
            public void CancelSend()
            {
                this.sckMain.Shutdown(SocketShutdown.Send);
            }

            private void ConfirmConnect(IAsyncResult asyn)
            {
                try
                {
                    this.sckMain.EndConnect(asyn);
                    this.WaitForData(sckMain);

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

            private void WaitForData(Socket sck)
            {
                try
                {
                    if (this.WorkerCallBack == null)
                    {
                        this.WorkerCallBack = new AsyncCallback(OnDataReceived);
                    }

                    Array.Clear(this.rgbyBuffer, 0, this.rgbyBuffer.Length);

                    if(sck != null && sck.Connected)
                    {
                        sck.BeginReceive(this.rgbyBuffer, 0, this.rgbyBuffer.Length, SocketFlags.None, this.WorkerCallBack, null);
                    }
                }
                catch (SocketException se)
                {
                    if (this.OnError != null) this.OnError(se.Message, se.ErrorCode);
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
                        this.rgbyReceivedData = new byte[iRx];
                        Array.Copy(this.rgbyBuffer, this.rgbyReceivedData, iRx);

                        char[] chars = new char[iRx + 1];
                        Decoder d = Encoding.UTF8.GetDecoder();
                        d.GetChars(this.rgbyBuffer, 0, iRx, chars, 0);

                        if (OnRead != null) OnRead(this.rgbyReceivedData);

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
            public bool SendBuffer(byte[] rgbyDataBuffer)
            {
                try
                {
                    int NumBytes = this.sckMain.Send(rgbyDataBuffer, rgbyDataBuffer.Length, SocketFlags.None);
                    
                    if (NumBytes == rgbyDataBuffer.Length)
                    {
                        if (OnWrite != null) { OnWrite(); }
                        return true;
                    }

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

        #region events

            private void SocketClient_OnConnectAttempt()
            {
                this.connState = enumConnState.ConnectionAttempt;
            }

            private void SocketClient_OnConnect()
            {
                this.connState = enumConnState.Connected;
            }

            private void SocketClient_OnDisconnect()
            {
                if (this.connState == enumConnState.ConnectionAttempt)
                {
                    this.connState = enumConnState.ConnectionAttemptFailed;
                }
                else
                {
                    this.connState = enumConnState.Disconnected;
                }
            }

            private void SocketClient_OnDisconnectAttempt()
            {
                this.connState = enumConnState.DisconnectionAttempt;
            }

            private void SocketClient_OnWrite()
            {
                this.connState = enumConnState.DataSent;
            }

            private void SocketClient_OnRead(byte[] rgbyBuffer)
            {
                this.rgbyReceivedData = rgbyBuffer;
                this.connState = enumConnState.ReceivedData;
            }

            private void SocketClient_OnError(string strErrorMessage, int iErrorCode)
            {
                switch (this.connState)
                {
                    case enumConnState.ConnectionAttempt:
                        this.connState = enumConnState.ConnectionAttemptFailed;
                        break;
                    case enumConnState.DisconnectionAttempt:
                        this.connState = enumConnState.Disconnected;
                        break;
                    default:
                        if (iErrorCode != 0)
                        {
                            this.connState = enumConnState.ConnectionError;
                        }
                        break;
                }

                this._strErrorMessage = iErrorCode.ToString() + " - " + strErrorMessage;
            }

        #endregion
    }
}
