using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace RepProtocolTestSuite
{
    static class Program
    {

        public static System.Text.Encoding Encoding { get { return System.Text.Encoding.GetEncoding(1252); } }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Trace.Listeners.Add(new TextWriterTraceListener("trace.log"));
            Trace.AutoFlush = true;
            Application.Run(new Form1());
            Trace.Flush();
        }
    }
}
