using System;
using System.Collections.Generic;
using System.Text;

namespace Gertec
{
    public static class Types
    {
        public struct Terminal
        {
            public string IP;
            public int Porta;
            public string Descricao;
            public string Serial;
            public int Grupo;
        }

        public struct Empregador
        {
            public string Pessoa;
            public string Nome;
            public string Cei;
            public string Endereco;
            public string PessoaTipo;
        }
    }
}
