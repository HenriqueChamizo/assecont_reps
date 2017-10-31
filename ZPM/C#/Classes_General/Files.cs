using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Wr
{
    class Files
    {
    /*
        public static void ReadFile(string Arquivo, List<string> Linhas)
        {
            if (!File.Exists(Arquivo)) return;

            using (StreamReader r = new StreamReader(Arquivo))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    Linhas.Add(line);
                }

                r.Close();
            }
        }

        public static void WriteFile(string arquivo, List<string> linhas)
        {
            ForceDirectories(Directory.GetParent(arquivo).FullName);

            using (StreamWriter file = new StreamWriter(arquivo))
                foreach (string s in linhas)
                {
                    file.WriteLine(s);
                }
        }
        */
        public static void ForceDirectories(string diretorio)
        {
            if (!Directory.Exists(diretorio)) Directory.CreateDirectory(diretorio);
        }

        public static void OpenFolderInExplorer(string Folder)
        {
            if (!Directory.Exists(Folder)) return;

            string windir = Environment.GetEnvironmentVariable("WINDIR");
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = windir + @"\explorer.exe";
            prc.StartInfo.Arguments = Folder;
            prc.Start();
        }

        public static string IncludeTrailingBackslash(string Dir)
        {
            if (Dir.EndsWith(@"\") ) 
            {
                return Dir;
            }
            else
            {
                return Dir + @"\";
            }
        }

        public static void DirSearch(string sDir, string sSpec, List<string> arquivos)
        {
            foreach (string f in Directory.GetFiles(sDir, sSpec))
            {
                arquivos.Add(f);
            }

            foreach (string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetFiles(d, sSpec))
                {
                    arquivos.Add(f);
                }
                DirSearch(d, sSpec, arquivos);
            }
        }

        public static void ExecuteFile(string Arquivo)
        {
            System.Diagnostics.Process.Start(Arquivo);
        }
    }
}
