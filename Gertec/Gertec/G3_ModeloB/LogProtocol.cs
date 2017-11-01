using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace RepProtocolTestSuite
{
    class LogProtocol : TextWriter
    {

        System.Windows.Forms.ListBox log;
        string ipRemote;
        /**
         * Class Contructor
         */
        public LogProtocol(System.Windows.Forms.ListBox listBox,string ipRemote) {
            
            this.ipRemote = ipRemote;
            this.log = listBox;
        
        }

        private string LogGetTime(){
            return DateTime.Now.ToString("hh:mm:ss.fff"); 
        }
         
        public void LogMessagePrint(string msg) {

            if (log.InvokeRequired)
            {
                log.Invoke(new MethodInvoker(delegate()
                {
                    LogMessagePrint(msg);
                }));
            }
            else
            {
                log.Items.Add("[" + LogGetTime() + "] : " + msg);
                log.Refresh();
                log.SelectedIndex = log.Items.Count - 1;
            }
        }

        // TextWriter implementation

        StringBuilder buffer = new StringBuilder();

        public override Encoding Encoding
        {
            get { return Encoding.GetEncoding(1252); }
        }

        public override void Write(string value)
        {
            foreach (char c in value)
            {
                if (c == '\n')
                {
                    LogMessagePrint(buffer.ToString());
                    buffer.Clear();
                }
                else
                {
                    buffer.Append(c);
                }
            }
        }

        public override void WriteLine()
        {
            LogMessagePrint(buffer.ToString());
            buffer.Clear();            
        }

        public override void WriteLine(string value)
        {
            Write(value);
            WriteLine();
        }

    }
}
