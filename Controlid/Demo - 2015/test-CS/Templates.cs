using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Controlid;
using System.Drawing;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Controlid.iDClass;

namespace RepTestAPI
{
    [TestClass]
    public class Templates
    {
        RepCid rep;

        [TestInitialize]
        public void Conectar()
        {
            rep = Config.ConectarREP();
        }

        [TestMethod, TestCategory("RepCid")]
        public void Template_ExtractJoin()
        {
            byte[][] btResult = new byte[3][];
            for (int i = 1; i <= 3; i++)
            {
                Bitmap digital = new System.Drawing.Bitmap(@"..\..\dedo" + i + ".bmp");
                byte[] btRequest = RepCid.GetBytes(digital);

                if (!rep.ExtractTemplate(btRequest, digital.Width, digital.Height, out btResult[i - 1]))
                {
                    Console.WriteLine(rep.LastLog());
                    Assert.Fail("Erro ao extrair Template " + i);
                }
                Console.WriteLine("LastQuality: " + RestJSON.LastQuality);
                Console.WriteLine("Template: " + Convert.ToBase64String(btResult[i - 1]));
            }
            byte[] btJoin;
            rep.JoinTemplates(btResult[0], btResult[1], btResult[2], out btJoin);
            Console.WriteLine("Template: " + Convert.ToBase64String(btJoin));
            //Console.WriteLine(string.Format("Código: {0}\nErro: {1}\nQualidade: {2}\nTemplate: {3}", tr.code, tr.error, tr.Qualidate, tr.Template));  
        }
    }
}