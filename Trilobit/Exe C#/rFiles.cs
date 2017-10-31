using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Trilobit
{
    class rFiles
    {
        public static void ReadFile(string Arquivo, List<string> Linhas)
        {
            using (StreamReader r = new StreamReader(Arquivo))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    Linhas.Add(line);
                }
            }
        }
    }
}
