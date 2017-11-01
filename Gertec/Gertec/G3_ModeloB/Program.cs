using System;
using System.Windows.Forms;

namespace G3_ModeloB
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
            //Trace.Listeners.Add(new TextWriterTraceListener("trace.log"));
            //Trace.AutoFlush = true;
            Application.Run(new Principal() );
            //Trace.Flush();
        }
    }
}
