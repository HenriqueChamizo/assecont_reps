using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controlid;
using Controlid.iDClass;

namespace RepTestAPI
{
    [TestClass]
    public class Empregador
    {
        RepCid rep;

        [TestInitialize]
        public void Conectar()
        {
            rep = Config.ConectarREP();
        }

        [TestMethod, TestCategory("RepCid")]
        public void Empregador_Get()
        {
            string doc;
            int tipodoc;
            string cei;
            string razsoc;
            string endereco;

            if (rep.LerEmpregador(out doc, out tipodoc, out cei, out razsoc, out endereco))
            {
                Console.WriteLine("Documento: " + doc);
                Console.WriteLine("Tipo DOC: " + tipodoc);
                Console.WriteLine("CEI: " + cei);
                Console.WriteLine("Razão Social: " + razsoc);
                Console.WriteLine("Endereço: " + endereco);
            }
            else
                Assert.Fail("Erro ao Ler o empregador");
        }

        [TestMethod, TestCategory("RepCid")]
        public void Empregador_Set()
        {
            //------------05343346000106
            string doc = "12345678901234"; // Não pode enviar com mascara! (só numeros: 14 digitos)
            int tipodoc = 1;
            string cei = "123456789";
            string razsoc = "Control iD Teste API";
            string endereco = "Rua Hungria, 574";
            bool gravou;

            if (rep.GravarEmpregador(doc, tipodoc, cei, razsoc, endereco, out gravou) && gravou)
                Console.WriteLine("OK, empregador gravado");
            else
            {
                Console.WriteLine(rep.LastLog());
                Assert.Fail("Erro ao gravar empregador");
            }
                
        }
    }
}