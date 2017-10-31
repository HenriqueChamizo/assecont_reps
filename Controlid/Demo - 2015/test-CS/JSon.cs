using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controlid;
using Controlid.iDClass;

namespace RepTestAPI
{
    [TestClass]
    public class JSon
    {
        [TestMethod, TestCategory("API")]
        public void TestSimpleJson()
        {
            String cResult = RestJSON.SendJsonSimple("https://192.168.0.145/login.fcgi", "{\"login\":\"admin\",\"password\":\"admin\"}");
            Console.WriteLine(cResult);
            Assert.IsTrue(cResult.Contains("session"), "Erro ao fazer o login");
        }
    }
}