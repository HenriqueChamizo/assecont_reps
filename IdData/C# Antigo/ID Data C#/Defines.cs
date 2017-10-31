using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdData
{
    public static class CDefines
    {
        public const string DLL_VERSION = "1.5.0.1";
        public const string SW_TITLE = "IDSysR30 for C#";
    }

    #region enum

        public enum ECommand
        {
            None,
            AddUser,
            ChangeUserData,
            DeleteUser,
            ReadUserData,
            ReadEmployerData,
            SetEmployer,
            SetDateTime,
            SetREPCommunication,
            ReadREPCommunication,
            RequestEventByNSR,
            RequestNFR,
            RequestTotalNSR,
            RequestTotalUsers,
            RequestUserByIndex
        }

        public enum EConnectionState
        {
            None,
            AttemptConnection,
            AttemptConnectionFail,
            Connected,
            SendingData,
            DataReceived,
            Disconnected,
            DataReceivedError,
            ConnectionError
        }

        public enum EIdentify_Type
        {
            CNPJ = 1,
            CPF = 2
        }

    #endregion

}
