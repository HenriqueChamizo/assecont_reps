using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace RepProtocolTestSuite
{
    class RestServices
    {

        private static string defaultCpf = "12345678909";
        private static string defaultPassword = "1234";
        

        private string cpf;
        private string password;

        public const int DefaultTimeout = 5000;
        public const int DefaultRetries = 5;

        public int Timeout { get; set; }
        public int Retries { get; set; }
        public TimeSpan NsrCooldown { get; set; }
        //public TimeSpan NsrThreshold { get; set; }

        private DateTime lastNsrTime;

        private long lastNsrValue = 0;
        private int lastNsrLength = 9;
        private long nfrValue = 0;
        private int nfrLength = 17;
        //private bool getMrpStatus = true;
        private bool lastNfrChanged = true;

        public RestServices(int timeout = DefaultTimeout, int numtries = DefaultRetries) {

            this.Timeout = timeout;
            this.Retries = numtries;
            this.cpf = defaultCpf;
            this.password = defaultPassword;
            lastNsrTime = DateTime.Now;
            NsrCooldown = TimeSpan.FromMilliseconds(2000);
            //NsrThreshold = TimeSpan.FromMilliseconds(5000);
        }

        public static void SetDefaultAuth(string cpf, string password)
        {
            defaultCpf = cpf;
            defaultPassword = password;
        }

        public void SetAuth(string cpf, string password)
        {
            this.cpf = cpf;
            this.password = password;
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

                        if (lastNfrChanged)
                        {
                            TimeSpan timeDiff = DateTime.Now - lastNsrTime;
                            if (timeDiff < NsrCooldown)
                            {
                                System.Threading.Thread.Sleep(NsrCooldown - timeDiff);
                            }
                            Trace.WriteLine("Getting MRP Status");
                            RepProtocol.MrpStatus mrpStatus = instance.GetMrpStatus();
                            nfrValue = long.Parse(mrpStatus.NFR);
                            nfrLength = mrpStatus.NFR.Length;
                            lastNsrValue = long.Parse(mrpStatus.LastNSR);
                            lastNsrLength = mrpStatus.LastNSR.Length;
                            Trace.WriteLine("LastNfrChanged set to FALSE");
                        }
                        byte[] aesKey = System.Security.Cryptography.SHA1.Create().ComputeHash(Program.Encoding.GetBytes(bodyToDigest));
                        string authString =
                            "GERTEC\n" +
                            "MARQUE PONTO G4\n" +
                            "NFR:" + nfrValue.ToString().PadLeft(nfrLength, '0') + "\n" +
                            "NSR:" + lastNsrValue.ToString().PadLeft(lastNsrLength, '0') + "\n" +
                            "SENHA:" + password
                        ;
                        // gerar digest criptografado de autenticação
                        Trace.WriteLine("**** AUTHENTICATED POST ****");
                        Trace.WriteLine("Post Data Str.:\n" + bodyToDigest);
                        Trace.WriteLine("Post Data Bin.:\n" + BitConverter.ToString(Program.Encoding.GetBytes(bodyToDigest)).Replace("-", ""));
                        Trace.WriteLine("Auth Data Str.:\n" + authString);
                        Trace.WriteLine("Auth Data Bin.:\n" + BitConverter.ToString(Program.Encoding.GetBytes(authString)).Replace("-", ""));
                        List<byte> b;
                        b = new List<byte>(SHA1.Create().ComputeHash(Program.Encoding.GetBytes(bodyToDigest)));
                        Trace.WriteLine("Post Data SHA1: " + BitConverter.ToString(b.ToArray()).Replace("-", ""));
                        b.RemoveRange(16, 4);
                        Trace.WriteLine("Post Data SHA1 16 MSB: " + BitConverter.ToString(b.ToArray()).Replace("-", ""));
                        byte[] rawParamsSha1Msb = b.ToArray();
                        b = new List<byte>(SHA1.Create().ComputeHash(Program.Encoding.GetBytes(authString)));
                        Trace.WriteLine("Auth Data SHA1: " + BitConverter.ToString(b.ToArray()).Replace("-", ""));
                        b.RemoveRange(16, 4);
                        Trace.WriteLine("Auth Data SHA1 16 MSB: " + BitConverter.ToString(b.ToArray()).Replace("-", ""));
                        byte[] authStringSha1Msb = b.ToArray();
                        SymmetricAlgorithm aes = Aes.Create();
                        aes.Mode = CipherMode.ECB;
                        aes.KeySize = 128;
                        aes.Padding = PaddingMode.None;
                        ICryptoTransform t = aes.CreateEncryptor(authStringSha1Msb, null);
                        MemoryStream ms = new MemoryStream();
                        CryptoStream cs = new CryptoStream(ms, t, CryptoStreamMode.Write);
                        cs.Write(rawParamsSha1Msb, 0, rawParamsSha1Msb.Length);
                        cs.FlushFinalBlock();
                        byte[] encAuth = ms.ToArray();
                        Trace.WriteLine("AES(Post Data SHA1 16 MSB, Auth Data SHA1 16 MSB): " + BitConverter.ToString(encAuth).Replace("-", ""));
                        cs.Close();
                        ms.Close();
                        string encAuthString = BitConverter.ToString(encAuth).Replace("-", "");
                        rawParams += "AUT=" + cpf + ";" + encAuthString;
                    }
                    Trace.WriteLine("Post body: " + rawParams);
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
                    lastNfrChanged = false;
                    ThrowIfError(responseBody);
                    lastNsrTime = DateTime.Now;
                    lastNfrChanged = true;
                    Trace.WriteLine("Response ok. LastNfrChanged set to TRUE");
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
                        Trace.WriteLine("Auth error. Retrying...");
                    }
                    lastNfrChanged = true;
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
