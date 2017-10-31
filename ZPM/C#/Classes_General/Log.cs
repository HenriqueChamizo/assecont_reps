using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Wr
{
    class Log
    {
        TextBox textbox;
        string prefixo;

        public Log(TextBox textbox, string Prefixo = "")
        {
            this.textbox = textbox;

            if (Prefixo == "")
            {
                this.prefixo = Prefixo;
            }
            else
            {
                this.prefixo = Prefixo + ": ";
            }
        }

        public void AddLog(string Mensagem, bool NewLine = false, bool ShowOk = false)
        {
            if (Mensagem == "") return;

            if (prefixo == "")
            {
                AddLogUnformatted(Mensagem);
                return;
            }

            if (textbox.Text.Length > 0)
            {
                textbox.AppendText(Environment.NewLine);
            }

            textbox.AppendText(prefixo + String.Format("{0:dd/MM HH:mm}", DateTime.Now) + " " + Mensagem.ToUpper());

            if (NewLine)
            {
                if (ShowOk)
                {
                    AddLog("Ok");
                }

                textbox.AppendText(Environment.NewLine);
            }
        }

        public void AddLogUnformatted(string Mensagem)
        {
            if (textbox.Text.Length > 0) textbox.AppendText(Environment.NewLine);
            textbox.AppendText(Mensagem);
        }

        public void LogOk()
        {
            this.AddLog("Ok", true);
        }

        public void LogSucesso()
        {
            this.AddLog("Comando enviado com sucesso", true);
        }

        public void LogErro()
        {
            this.AddLog("Erro no REP ao receber comando", true);
        }

        public void AddLineBreak()
        {
            this.AddLogUnformatted("");
        }
    }
}
