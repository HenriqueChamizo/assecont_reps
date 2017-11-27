using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace HexaB
{
    class StateObject
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

    enum TypesRespostas
    {
        Nulo = 0,
        DataHora = 1,
        Empregador = 2,
        Funcionario = 3,
        Marcacoes = 4
    }
}