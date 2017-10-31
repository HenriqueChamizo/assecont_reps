using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controlid;
using Controlid.iDClass;

namespace RepTestAPI
{
    [TestClass]
    public class Usuarios
    {
        RepCid rep;

        [TestInitialize]
        public void Conectar()
        {
            rep = Config.ConectarREP();
        }

        private static Int64 PisTemplate;
        [TestMethod, TestCategory("RepCid")]
        public void Usuario_List()
        {
            int qtd;
            if (!rep.CarregarUsuarios(true, out qtd))
                Assert.Fail("Erro ao carregar usuários");

            Console.WriteLine("Usuários:" + qtd);

            Int64 pis;
            string nome;
            int codigo;
            string senha;
            string barras;
            int rfid;
            int privilegios;
            int ndig;
            int nCount = 0;
            PisTemplate = 0;
            while (rep.LerUsuario(out pis, out nome, out codigo, out senha, out barras, out rfid, out privilegios, out ndig))
            {
                nCount++;
                Console.WriteLine(string.Format("{0:D4}: {1} {2:D12}-{3} \t {4} \t {5}{6}{7}",
                    nCount, privilegios == 1 ? "A" : "U", pis, nome,
                    codigo == 0 && senha == "" ? "" : (codigo + "/" + senha),
                    barras == "" ? "" : (" CB:" + barras),
                    rfid == 0 ? "" : (" RF:" + rfid),
                    ndig == 0 ? "" : (" BIO:" + ndig)));
                if (PisTemplate == 0 && ndig > 0)
                    PisTemplate = pis;
            }

            if (PisTemplate > 0)
                Console.WriteLine("\r\nPisTemplate para teste: " + PisTemplate);
        }

        [TestMethod, TestCategory("RepCid")]
        public void Usuario_GetTemplate()
        {
            if (PisTemplate==0)
                PisTemplate = 012468202319; // PIS: Fabio Ferreira

            int num_tmpls;
            if (rep.CarregarTemplatesUsuario(PisTemplate, out num_tmpls))
            {
                Console.WriteLine(PisTemplate +": " + num_tmpls + " templates localizados.");
                byte[] tmpl_bin;
                while (rep.LerTemplate(out tmpl_bin))
                    Console.WriteLine("\t" + Convert.ToBase64String(tmpl_bin));
            }
            else
                Assert.Inconclusive("Não há PIS registrado para obter um template");
        }

        [TestMethod, TestCategory("RepCid")]
        public void Usuario_CRUD()
        {
            bool gravou;

            Int64 pis = 22334455;

            string nome1, nome2;
            int codigo1, codigo2;
            string senha1, senha2;
            string barras1, barras2;
            int rfid1, rfid2;
            int privilegios1, privilegios2;

            // Inclusão
            if (!(rep.GravarUsuario(pis, nome1 = "Auto-Test: Incluido", codigo1 = 112233, senha1 = "222111", barras1 = "134567", rfid1 = 6543219, privilegios1 = 1, out gravou) && gravou))
            {
                Console.WriteLine(rep.LastLog());
                Assert.Fail("Erro ao Incluir");
            }
            Console.WriteLine("Usuário Adicionado");

            // Valida inclusão
            if (!rep.LerDadosUsuario(pis, out nome2, out codigo2, out senha2, out barras2, out rfid2, out privilegios2))
            {
                Console.WriteLine(rep.LastLog());
                Assert.Fail("Erro ao Ler usuário incluido");
            }
            if (nome1 != nome2)
                Assert.Fail("Dados lidos não conferem na alteração: Nome");
            else if (codigo1 != codigo2)
                Assert.Fail("Dados lidos não conferem na alteração: Código");
            else if (senha1 != senha2)
                Assert.Fail("Dados lidos não conferem na alteração: Senha");
            else if (barras1 != barras2)
                Assert.Fail("Dados lidos não conferem na alteração: Barras");
            else if (rfid1 != rfid2)
                Assert.Fail("Dados lidos não conferem na alteração: RFID");
            else if (privilegios1 != privilegios2)
                Assert.Fail("Dados lidos não conferem na alteração: Privilegios");

            // Alteração
            if (!(rep.GravarUsuario(pis, nome1 = "Auto-Test: Alterado", codigo1 = 221133, senha1 = "112233", barras1 = "1232349", rfid1 = 9234234, privilegios1 = 0, out gravou) && gravou))
            {
                Console.WriteLine(rep.LastLog());
                Assert.Fail("Erro ao Alterar");
            }
            Console.WriteLine("Usuário Alterado");

            // Valida alteração
            if (!rep.LerDadosUsuario(pis, out nome2, out codigo2, out senha2, out barras2, out rfid2, out privilegios2))
            {
                Console.WriteLine(rep.LastLog());
                Assert.Fail("Erro ao Ler usuário alterado");
            }

            if (nome1 != nome2)
                Assert.Fail("Dados lidos não conferem na alteração: Nome");
            else if (codigo1 != codigo2)
                Assert.Fail("Dados lidos não conferem na alteração: Código");
            else if (senha1 != senha2)
                Assert.Fail("Dados lidos não conferem na alteração: Senha");
            else if (barras1 != barras2)
                Assert.Fail("Dados lidos não conferem na alteração: Barras");
            else if (rfid1 != rfid2)
                Assert.Fail("Dados lidos não conferem na alteração: RFID");
            else if (privilegios1 != privilegios2)
                Assert.Fail("Dados lidos não conferem na alteração: Privilegios");

            // Exclusão
            if (!(rep.RemoverUsuario(pis, out gravou) && gravou))
            {
                Console.WriteLine(rep.LastLog());
                Assert.Fail("Erro ao Excluir");
            }
            Console.WriteLine("Usuário Excluido");
        }

        [TestMethod, TestCategory("Rep iDClass")]
        public void Usuario_SaveTemplate()
        {
            //UserUpdateRequest uur = new UserUpdateRequest();
            UserAddRequest uur = new UserAddRequest();

            uur.Usuario[0].PIS = 108;
            uur.Usuario[0].Nome = "Teste Add Modify Template";
            // Digital TXAI indicador direito
            uur.Usuario[0].Templates = new string[] { "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"};

            StatusResult st = (StatusResult)RestJSON.SendJson(rep.IP, uur, typeof(StatusResult), rep.iDClassSession);
            if (!st.isOK)
                Assert.Inconclusive(st.Status);

        }

        [TestMethod, TestCategory("Rep iDClass")]
        public void Usuario_ChangePIS()
        {
            UserUpdateRequest uur = new UserUpdateRequest();

            uur.Usuario[0].PIS = 108;
            uur.Usuario[0].PIS2 = 223344;
            uur.Usuario[0].Nome = "Teste Change PIS (com template)";
            
            StatusResult st = (StatusResult)RestJSON.SendJson(rep.IP, uur, typeof(StatusResult), rep.iDClassSession);
            if (!st.isOK)
                Assert.Inconclusive(st.Status);

        }
    }
}