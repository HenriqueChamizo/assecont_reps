using System;

namespace Trix
{
    public class Strings
    {
        public static string ConvertBytesToString(byte[] byBytes, int intStart, int intSize)
        {
            string Result = "";

            Result = "";
            for (int iIdx = intStart; iIdx < intStart + intSize; iIdx++)
            {
                if ((char)byBytes[iIdx] == Wr.Classes.Strings.END_STRING)
                {
                    break;
                }

                Result += (char)byBytes[iIdx];
            }

            Result = Result.Trim();
            Result = Result.PadLeft(intSize, '0');

            return Result;
        }

        public static string ConvertByteToString(Byte[] bytes)
        {
            string res = string.Empty;
            foreach (byte b in bytes)
            {
                res += Convert.ToChar(b);
            }

            return res;
        }
    }
}
