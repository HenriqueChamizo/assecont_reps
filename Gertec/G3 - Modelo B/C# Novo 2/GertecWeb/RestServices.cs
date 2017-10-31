using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Gertec;

namespace RepProtocolTestSuite
{
    class RestServices
    {

        private static string defaultCpf = "12345678909";
        private static string defaultPassword = "1234";
        private static byte[] defaultMasterKey = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        public byte[] masterKeys = defaultMasterKey;

        private string cpf;
        private string password;
        private byte[] masterKey;

        public const int DefaultTimeout = 50000000;
        public const int DefaultRetries = 5;

        public int Timeout { get; set; }
        public int Retries { get; set; }

        public RestServices(int timeout = DefaultTimeout, int numtries = DefaultRetries) {

            this.Timeout = timeout;
            this.Retries = numtries;
            this.cpf = defaultCpf;
            this.password = defaultPassword;
            this.masterKey = defaultMasterKey;
        }

        public static void SetDefaultAuth(string cpf, string password, byte[] masterKey)
        {
            defaultCpf = cpf;
            defaultPassword = password;
            defaultMasterKey = masterKey;
        }

        public void SetAuth(string cpf, string password, byte[] masterKey)
        {
            this.cpf = cpf;
            this.password = password;
            this.masterKey = masterKey;
        }

        private void ThrowIfError(string responseBody)
        {
            if (responseBody.StartsWith("ERROR:"))
            {
                int err;
                if (!int.TryParse(responseBody.Split(new char[] { ':' })[1], out err))
                {
                    err = (int)RepProtocol.ErrorCodes.ERROR_UNKNOWN;
                }
                throw new RepProtocolException(((RepProtocol.ErrorCodes)err));
            }
        }

        private string EscapeUrl(string url)
        {
            if (url == null) { return null; }
            string ret = "";
            foreach (char c in url)
            {
                if ((c >= '0' && c <= '9') ||
                    (c >= 'A' && c <= 'Z') ||
                    (c >= 'a' && c <= 'z')
                    )
                {
                    ret += c;
                }
                else
                {
                    ret += "%" + BitConverter.ToString(new byte[] { (byte)c });
                }
            }
            return ret;
        }

        public byte[] EncryptByMasterKey(string text)
        {
            SymmetricAlgorithm aes = Aes.Create();
            aes.Mode = CipherMode.ECB;
            aes.KeySize = 256;
            aes.Padding = PaddingMode.None;

            int blockSize = aes.BlockSize / 8;
            int blockCount = (text.Length + 1) / blockSize;
            if ((text.Length + 1) % blockSize > 0)
            {
                blockCount++;
            }
            byte[] data = new byte[blockCount * blockSize];
            RandomNumberGenerator.Create().GetBytes(data);
            data[text.Length] = 0;
            Array.Copy(Program.Encoding.GetBytes(text), data, text.Length);
            ICryptoTransform t = aes.CreateEncryptor(masterKey, null);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, t, CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
            cs.FlushFinalBlock();
            byte[] ret = ms.ToArray();
            cs.Close();
            ms.Close();
            return ret;
        }

        public string DoGetRequest(string uri)
        {
            Exception e = new TimeoutException();
            for (int i = 0; i < Retries; i++)
            {
                try
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                    req.Method = "GET";
                    req.Timeout = Timeout;
                    Trace.WriteLine(DateTime.Now.ToLongTimeString() + ": Before req.GetResponse()");
                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                    Trace.WriteLine(DateTime.Now.ToLongTimeString() + ": After req.GetResponse(), before Stream read");
                    StreamReader rsr = new StreamReader(resp.GetResponseStream(), Program.Encoding);
                    string responseBody = rsr.ReadToEnd();
                    Trace.WriteLine(DateTime.Now.ToLongTimeString() + ": After Stream read");
                    rsr.Close();
                    resp.Close();
                    ThrowIfError(responseBody);
                    return responseBody;

                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.Timeout)
                    {
                        i = Retries;
                        e = ex;
                    }
                    else
                    {
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    System.Threading.Thread.Sleep(500);
                    e = ex;
                }
            }
            throw e;
        }

        public string SendFile(string uri, byte[] fileData, Action<int, int> progressCallback)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = "POST";
            req.Timeout = Timeout;
            req.ContentType = "application/octet-stream";
            req.SendChunked = false;
            req.AllowWriteStreamBuffering = true;
            req.ContentLength = fileData.Length;
            req.ReadWriteTimeout = Timeout;
            BinaryWriter bw = new BinaryWriter(req.GetRequestStream());
            bw.Write(fileData);
            bw.Close();
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StreamReader rsr = new StreamReader(resp.GetResponseStream(), Program.Encoding);
            string responseBody = rsr.ReadToEnd();
            rsr.Close();
            resp.Close();
            ThrowIfError(responseBody);
            return responseBody;
        }

        public string DoPostRequest(string uri, Dictionary<string, string> param, RepProtocol instance, RepProtocol.ErrorCodes[] notRetriableErrors = null, bool authenticate = true)
        {
            Exception e = new TimeoutException();
            for (int i = 0; i < Retries; i++)
            {
                try
                {
                    string rawParams = "";
                    string bodyToDigest = "";
                    if (param != null)
                    {
                        foreach (KeyValuePair<string, string> kvp in param)
                        {
                            rawParams += EscapeUrl(kvp.Key) + "=" + EscapeUrl(kvp.Value) + "&";
                            bodyToDigest += EscapeUrl(kvp.Value);
                        }
                        // NÃO descomentar a linha abaixo - o protocolo remoto depende do & no final para validar a assinatura corretamente!
                        //rawParams = rawParams.Substring(0, rawParams.Length - 1);
                    }
                    if (authenticate)
                    {
                        byte[] payloadHash = SHA256.Create().ComputeHash(Program.Encoding.GetBytes(bodyToDigest));
                        byte[] passwordHash = SHA256.Create().ComputeHash(Program.Encoding.GetBytes(password));
                        byte[] sessionKey = new byte[payloadHash.Length];
                        
                        for (int j = 0; j < sessionKey.Length; j++)
                        {
                            sessionKey[j] = (byte)(masterKey[j] ^ passwordHash[j]);
                        }



                        // gerar digest criptografado de autenticação
                        Trace.WriteLine("**** AUTHENTICATED POST ****");
                        Trace.WriteLine("Post Data Str.:\n" + bodyToDigest);
                        Trace.WriteLine("Post Data Bin.:\n" + BitConverter.ToString(Program.Encoding.GetBytes(bodyToDigest)).Replace("-", ""));
                        Trace.WriteLine("Master key:\n" + BitConverter.ToString(masterKey).Replace("-", ""));
                        Trace.WriteLine("SHA256(password):\n" + BitConverter.ToString(passwordHash).Replace("-", ""));
                        Trace.WriteLine("Session key:\n" + BitConverter.ToString(sessionKey).Replace("-", ""));
                        Trace.WriteLine("SHA256(Post data):\n" + BitConverter.ToString(payloadHash).Replace("-", ""));
                        
                        SymmetricAlgorithm aes = Aes.Create();
                        aes.Mode = CipherMode.ECB;
                        aes.KeySize = 256;
                        aes.Padding = PaddingMode.None;
                        ICryptoTransform t = aes.CreateEncryptor(sessionKey, null);
                        MemoryStream ms = new MemoryStream();
                        CryptoStream cs = new CryptoStream(ms, t, CryptoStreamMode.Write);
                        cs.Write(payloadHash, 0, payloadHash.Length);
                        cs.FlushFinalBlock();
                        byte[] encAuth = ms.ToArray();
                        cs.Close();
                        ms.Close();
                        string encAuthString = BitConverter.ToString(encAuth).Replace("-", "");
                        Trace.WriteLine("AES(SHA256(Post data), Session key):\n" + encAuthString);
                        rawParams += "AUT=" + cpf + ";" + encAuthString;
                    }
                    Trace.WriteLine("Post body:\n" + rawParams);
                    // fazer request
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
                    req.Method = "POST";
                    req.Timeout = Timeout;
                    req.ContentType = "application/x-www-form-urlencoded";
                    StreamWriter sw = new StreamWriter(req.GetRequestStream(), Program.Encoding);
                    sw.Write(rawParams);
                    sw.Close();
                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                    StreamReader rsr = new StreamReader(resp.GetResponseStream(), Program.Encoding);
                    string responseBody = rsr.ReadToEnd();
                    rsr.Close();
                    resp.Close();
                    ThrowIfError(responseBody);
                    Trace.WriteLine("Response ok.");
                    return responseBody;
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.Timeout)
                    {
                        Trace.WriteLine("Timeout. Retrying...");
                        e = ex;
                    }
                    else
                    {
                        Trace.WriteLine("HTTP error. Retrying...");
                        throw ex;
                    }
                }
                catch (RepProtocolException ex)
                {
                    if (notRetriableErrors != null && notRetriableErrors.Contains(ex.ErrorCode))
                    {
                        Trace.WriteLine("Not retriable error.");
                        i = Retries;
                    }
                    if (ex.ErrorCode == RepProtocol.ErrorCodes.MT_RES_ERROR_INVALID_HASH_ADDRESS)
                    {
                        Trace.WriteLine("Auth error. Do not retry.");
                        i = Retries;
                    }
                    e = ex;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Unknown error. Sleeping and retrying...");
                    System.Threading.Thread.Sleep(500);
                    e = ex;
                }
            }
            throw e;
        }
    }
}
