using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Wr;

namespace Zpm
{
    class Marcacoes
    {
        public struct Marcacao
        {
            public string Pis;
            public DateTime DataHora;
            public int NSR;
        }

        private string Arquivo;
        public List<Marcacao> listMarcacoes = new List<Marcacao>();

        public Marcacoes(string Arquivo)
        {
            this.Arquivo = Arquivo;

            if (File.Exists(Arquivo))
            {
                ReadFromFile(Arquivo, listMarcacoes);
            }
        }

        public void Add(string Pis, DateTime DataHora, int NSR)
        {
            Marcacao marcacao = new Marcacao();

            marcacao.Pis = Pis;
            marcacao.DataHora = DataHora;
            marcacao.NSR = NSR;

            listMarcacoes.Add(marcacao);
        }

        public void SaveToFile()
        {
            Files.ForceDirectories(Directory.GetParent(Arquivo).FullName);

            using (StreamWriter file = new StreamWriter(Arquivo))
                foreach (Marcacao m in listMarcacoes)
                {
                    file.WriteLine(String.Format("{0:-12:x} {1} {2:-10x}", m.Pis, m.DataHora.ToString("dd/MM/yyyy HH:mm"), m.NSR.ToString()));
                }
        }

        public void ReadFromFile(string Arquivo, List<Marcacao> Marcacoes)
        {
            if (!File.Exists(Arquivo)) return;

            Marcacao marcacao;

            using (StreamReader r = new StreamReader(Arquivo))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    marcacao.Pis = line.Substring(0, 11);
                    marcacao.DataHora = Convert.ToDateTime(line.Substring(12, 16));
                    marcacao.NSR = Convert.ToInt32(line.Substring(29).Trim());
                    Marcacoes.Add(marcacao);
                }

                r.Close();
            }
        }
    }
}
