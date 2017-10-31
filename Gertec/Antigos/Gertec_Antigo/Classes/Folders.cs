using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gertec
{
    class Folders
    {
        public static string folderMarcacoes(string TerminalNome)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Asseponto\\Marcacoes\\" + TerminalNome + "\\";
        }
    }
}
