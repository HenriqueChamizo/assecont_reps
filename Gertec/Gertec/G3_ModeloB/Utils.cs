using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using G3_ModeloB;

namespace RepProtocolTestSuite
{
    class Utils
    {

        // http://stackoverflow.com/questions/90838/how-can-i-detect-the-encoding-codepage-of-a-text-file
        public static Encoding DetectEncoding(byte[] fileContent)
        {
            if (fileContent == null)
                throw new ArgumentNullException();

            if (fileContent.Length < 2)
                return Program.Encoding;      // Default fallback

            if (fileContent[0] == 0xff
                && fileContent[1] == 0xfe
                && (fileContent.Length < 4
                    || fileContent[2] != 0
                    || fileContent[3] != 0
                    )
                )
                return Encoding.Unicode;

            if (fileContent[0] == 0xfe
                && fileContent[1] == 0xff
                )
                return Encoding.BigEndianUnicode;

            if (fileContent.Length < 3)
                return Program.Encoding;

            if (fileContent[0] == 0xef && fileContent[1] == 0xbb && fileContent[2] == 0xbf)
                return Encoding.UTF8;

            if (fileContent[0] == 0x2b && fileContent[1] == 0x2f && fileContent[2] == 0x76)
                return Encoding.UTF7;

            if (fileContent.Length < 4)
                return null;

            if (fileContent[0] == 0xff && fileContent[1] == 0xfe && fileContent[2] == 0 && fileContent[3] == 0)
                return Encoding.UTF32;

            if (fileContent[0] == 0 && fileContent[1] == 0 && fileContent[2] == 0xfe && fileContent[3] == 0xff)
                return Encoding.GetEncoding(12001);

            String probe;
            int len = fileContent.Length;

            if (fileContent.Length >= 128) len = 128;
            probe = Program.Encoding.GetString(fileContent, 0, len);

            MatchCollection mc = Regex.Matches(probe, "^<\\?xml[^<>]*encoding[ \\t\\n\\r]?=[\\t\\n\\r]?['\"]([A-Za-z]([A-Za-z0-9._]|-)*)", RegexOptions.Singleline);
            // Add '[0].Groups[1].Value' to the end to test regex

            if (mc.Count == 1 && mc[0].Groups.Count >= 2)
            {
                // Typically picks up 'UTF-8' string
                Encoding enc = null;

                try
                {
                    enc = Encoding.GetEncoding(mc[0].Groups[1].Value);
                }
                catch (Exception) { }

                if (enc != null)
                    return enc;
            }

            return Program.Encoding;      // Default fallback
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;

        }

        public static string FixRepIpAddressString(string ip)
        {
            string ret = "";
            foreach (string s in ip.Split(new char[] { '.' }))
            {
                ret += int.Parse(s).ToString() + ".";
            }
            return ret.TrimEnd(new char[] { '.' });
        }

        private static Dictionary<string, string> ParseRepResponse(string response)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            foreach (string kvp in response.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (kvp.Contains(':'))
                {
                    string k = kvp.Substring(0, kvp.IndexOf(':')).Trim();
                    string v = kvp.Substring(kvp.IndexOf(':') + 1).Trim();
                    ret.Remove(k);
                    ret.Add(k, v);
                }
            }
            return ret;
        }
        public static string GetRepResponseValue(string response, string name, string defaultValue = null)
        {
            Dictionary<string, string> dic = ParseRepResponse(response);
            if (dic.ContainsKey(name))
            {
                return dic[name];
            } else
            {
                return defaultValue;
            }
        }
    }
}
