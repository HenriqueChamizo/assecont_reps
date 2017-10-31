using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controlid;
using Controlid.iDClass;

namespace RepTestAPI
{
    [TestClass]
    public class AFD
    {
        RepCid rep;

        [TestInitialize]
        public void Conectar()
        {
            rep = Config.ConectarREP();
        }

        [TestMethod, TestCategory("RepCid")]
        public void AFD_Completo()
        {
            if (rep.BuscarAFD(15000))
            {
                string sLinha;
                int n = 0;
                while (rep.LerAFD(out sLinha))
                {
                    n++;
                    Console.WriteLine(sLinha);
                }
                Console.WriteLine("\nTotal: " + n);
            }
            else
            {
                Console.WriteLine(rep.LastLog());
                Assert.Fail("Erro ao Buscar AFD");
            }
                
        }
    }
}