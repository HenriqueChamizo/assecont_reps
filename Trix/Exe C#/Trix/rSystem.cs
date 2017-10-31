using System;
using System.Collections.Generic;
using System.Text;

namespace Trix
{
    class rSystem
    {
        public static DateTime PauseForSeconds(int SecondsToPauseFor)
        {
            System.DateTime ThisMoment = System.DateTime.Now;
            System.TimeSpan duration = new System.TimeSpan(0, 0, 0, SecondsToPauseFor, 0);
            System.DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = System.DateTime.Now;
            }

            return System.DateTime.Now;
        }
    }
}
