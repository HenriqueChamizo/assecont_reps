using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trix
{
    public class ProgressString
    {
        string Mensagem;
        int Max;
        int Position;
        int TenPercentPosition;
        int Percent;

        public ProgressString(string mensagem, int max)
        {
            Mensagem = mensagem;
            Max = max;
            Position = 0;
            Percent = 0;
            TenPercentPosition = Max / 10;
        }

        public void Increment()
        {
            Position++;

            if (Position >= TenPercentPosition)
            {
                Position = 0;
                Percent = Percent + 10;
                ShowProgress();
            }
        }

        private string ShowProgress()
        {
            return Mensagem + " " + Percent.ToString() + "%";
        }
    }
}
