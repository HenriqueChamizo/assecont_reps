using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Trilobit
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //        [STAThread]
        static void Main(string[] parametros)
        {
                for (int i = 0; i < parametros.Length; i++)
                {
                    if (parametros[i].ToLower().Contains("servidor"))
                    {
                        int start = parametros[i].IndexOf(':');
                        Global.server = parametros[i].Substring(++start);
                    }

                    if (parametros[i].ToLower().Contains("database"))
                    {
                        int start = parametros[i].IndexOf(':');
                        Global.database = parametros[i].Substring(++start);
                    }

                    if (parametros[i].ToLower().Contains("user"))
                    {
                        int start = parametros[i].IndexOf(':');
                        Global.user = parametros[i].Substring(++start);
                    }

                    if (parametros[i].ToLower().Contains("password"))
                    {
                        int start = parametros[i].IndexOf(':');
                        Global.pwd = parametros[i].Substring(++start);
                    }

                }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Principal());
        }
    }
}
