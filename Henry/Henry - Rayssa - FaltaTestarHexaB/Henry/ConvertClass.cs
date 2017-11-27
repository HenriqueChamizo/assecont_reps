using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexaB
{
    class ConvertClass
    {
        public byte CheckSum(string S)
        {
            int Result = 0;

            for (int i = 0; i < S.Length; i++)
            {
                Result = Result ^ S[i];
            }

            Result = Result ^ (S.Length & 0xFF);
            Result = Result ^ ((S.Length >> 8) & 0xFF);

            return (byte)Result;

        }

        //public static byte[] ConvertStringToBytes(string Pacote)
        //{
        //    List<byte> Result = new List<byte>();

        //    const byte CONST_START_BYTE = 02; // Byte inicial.
        //    const byte CONST_END_BYTE = 03; // Byte final.

        //    Result.Add(CONST_START_BYTE);
        //    Result.Add((byte)(Pacote.Length & 0xFF));
        //    Result.Add((byte)((byte)((byte)(Pacote.Length) >> 8) & 0xFF));

        //    for (int i = 0; i < Pacote.Length; i++)
        //    {
        //        Result.Add(System.Convert.ToByte(Pacote[i]));
        //    }

        //    Result.Add(CheckSum(Pacote));
        //    Result.Add(CONST_END_BYTE);

        //    return Result.ToArray();
        //}
    }
}