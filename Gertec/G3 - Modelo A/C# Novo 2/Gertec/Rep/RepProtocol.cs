using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace RepProtocolTestSuite
{
    class RepProtocolException : Exception
    {
        public RepProtocol.ErrorCodes ErrorCode { get; private set; }

        public RepProtocolException(RepProtocol.ErrorCodes errorCode)
            : base(errorCode.ToString())
        {
            ErrorCode = errorCode;

        }
    }

    class RepProtocol
    {
        TextBox edLog;
    
    public enum ErrorCodes
        {
            REMOTE_PROTOCOL_OK = 0,
            REMOTE_PROTOCOL_PROCESSING,
            REMOTE_PROTOCOL_INVALID_URI,
            REMOTE_PROTOCOL_REQUEST_NOT_FOUND,
            REMOTE_PROTOCOL_UNKNOW_COMMAND,
            REMOTE_PROTOCOL_BUSY,
            REMOTE_PROTOCOL_GENERAL_ERROR,

            MT_RES_OK = 200,

            MT_RES_ERROR_ENROLLED_EMPLOYER,
            MT_RES_ERROR_NOT_ENROLLED_EMPLOYER,
            MT_RES_ERROR_ENROLLED_PIS,
            MT_RES_ERROR_ENROLLED_ID,
            MT_RES_ERROR_NOT_ENROLLED_EMPLOYEE,
            MT_RES_ERROR_ENROLLED_CONTACTLESS,
            MT_RES_ERROR_ENROLLED_BARCODE,
            MT_RES_ERROR_ENROLLED_KEYBOARD,
            MT_RES_ERROR_RECORD,
            MT_RES_ERROR_FORMAT,
            MT_RES_ERROR_FLASH_SIZE,

            MT_RES_ERROR_INVALID_NAME,
            MT_RES_ERROR_INVALID_PIS,
            MT_RES_ERROR_INVALID_ID,
            MT_RES_ERROR_INVALID_BIOID,
            MT_RES_ERROR_INVALID_CONTACTLESS,
            MT_RES_ERROR_INVALID_BARCODE,
            MT_RES_ERROR_INVALID_KEYBOARD,

            MT_RES_ERROR_PARAMETER,
            MT_RES_ERROR_NO_ID_DATA,
            MT_RES_ERROR_INVALID_DATA,
            MT_RES_ERROR_INVALID_HASH_ADDRESS,
            MT_RES_ERROR_NOT_IN_USE_RECORD,
            MT_RES_ERROR_INVALID_CNPJ,
            MT_RES_ERROR_INVALID_CPF,
            MT_RES_ERROR_INVALID_CEI,
            MT_RES_ERROR_INVALID_EMPLOYER_TYPE,
            MT_RES_ERROR_UNENROLL_EMPLOYEE,
            MT_RES_ERROR_MAX_BIO_IDS_ENROLLED,
            MT_RES_ERROR_NO_FREE_SPACE,
            MT_RES_ERROR_INIT,

            ERROR_UNKNOWN = 9999
        }

        public enum GetFilter { 
        
            GetAll,
            GetLast24hs,
            GetNSRRange,
            GetDateRange
        
        }

        public enum FiltroEmpregado
        {
            PIS,
            ID,
            CNTLS,
            KBD,
            BIO
        }

        public enum FiltroRegistro
        {
            All,
            Last24hs,
            DateRange,
            NsrRange
        }

        public class Empregado
        {
            public string Nome;
            public string PIS;
            public string CNTLS;
            public string KBD;
            public string ID;

            public static List<Empregado> Importar(string arquivo)
            {
                List<Empregado> ret = new List<Empregado>();
                byte[] fData = System.IO.File.ReadAllBytes(arquivo);
                String fText = Utils.DetectEncoding(fData).GetString(fData);
                foreach (string l in fText.Split(new char[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!l.TrimStart(new char[] { ' ', '\t' }).StartsWith("//"))
                    {
                        string[] f = l.TrimEnd(new char[] { ';' }).Split(new char[] { ',' });
                        if (f.Length == 5)
                        {
                            Empregado e = new Empregado();
                            e.Nome = f[0];
                            e.PIS = f[1];
                            e.ID = f[2];
                            e.CNTLS = f[3];
                            e.KBD = f[4];
                            ret.Add(e);
                        }
                    }
                }
                return ret;
            }
        }

        //public enum TipoEmpregador
        //{
        //    PessoaJuridica = 1,
        //    PessoaFisica = 2
        //}

        public class Empregador
        {
            public string CNPJ;
            public string CEI;
            public string Nome;
            public string Endereco;
            public string Tipo;
        }

        public class MrpStatus
        {
            public string NFR;
            public string FreeSize;
            public string UsedSize;
            public string Version;
            public string Build;
            public string Flags;
            public string FirstReg;
            public string LastReg;
            public string LastNSR;
        }

        public class MtStatus
        {
            public string EnrolledEmployees;
            public string FreeEmployees;
            public string AvailableMemory;
            public string EnrolledEmployer;
        }

        public class NetworkConfig
        {
            public IPAddress ip;
            public IPAddress mask;
            public IPAddress gateway;
            public PhysicalAddress mac;
        }

        public class WifiConfig
        {
            public string SSID;
            public string Security;
            public string Key;
        }

        string baseAddress;

        RestServices server;

        bool cancelOperation = false;

        public int Timeout
        {
            get { return server.Timeout; }
            set { server.Timeout = value; }
        }

        public int Retries
        {
            get { return server.Retries; }
            set { server.Retries = value; }
        }

        public bool CancelOperation
        {
            get { return cancelOperation; }
            set { cancelOperation = value; }
        }

        public RepProtocol(TextBox edLog) {

            server = new RestServices();
            this.Timeout = RestServices.DefaultTimeout;
            this.edLog = edLog;
            //this.baseAddress = "http://" + host + "/";
        }

        public void SetHost(string host)
        {
            this.baseAddress = "http://" + host + "/";
        }

        public void SetAuth(string cpf, string password)
        {
            server.SetAuth(cpf, password);
        }

        public void Cancel()
        {
            cancelOperation = true;
        }

        public DateTime GetTime()
        {
            string resp = server.DoGetRequest(baseAddress + "MRP/RTC/GetTime");
            DateTime ret = DateTime.Parse(resp, CultureInfo.GetCultureInfo("pt-BR"));
            return ret;
        }

        public NetworkConfig GetNetworkConfig()
        {
            server.Timeout = 10000;
            string resp = server.DoGetRequest(baseAddress + "Net/GetConfig");
            server.Timeout = RestServices.DefaultTimeout;
            NetworkConfig ret = new NetworkConfig();
            ret.ip = IPAddress.Parse(Utils.FixRepIpAddressString(Utils.GetRepResponseValue(resp, "IP", "0.0.0.0")));
            ret.mask = IPAddress.Parse(Utils.FixRepIpAddressString(Utils.GetRepResponseValue(resp, "MASK", "0.0.0.0")));
            ret.gateway = IPAddress.Parse(Utils.FixRepIpAddressString(Utils.GetRepResponseValue(resp, "GAT", "0.0.0.0")));
            ret.mac = PhysicalAddress.Parse(Utils.GetRepResponseValue(resp, "MAC", "00-00-00-00-00-00").ToUpper());
            return ret;
        }

        public void SetNetworkConfig(NetworkConfig config)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("IP", config.ip.ToString());
            postData.Add("Mask", config.mask.ToString());
            postData.Add("Gateway", config.gateway.ToString());
            server.Timeout = 10000;
            string resp = server.DoPostRequest(baseAddress + "Net/SetConfig", postData, this, null, false);
            server.Timeout = RestServices.DefaultTimeout;
        }

        public int GetNetworkInterface()
        {
            server.Timeout = 10000;
            string resp = server.DoGetRequest(baseAddress + "Net/GetInterface");
            server.Timeout = RestServices.DefaultTimeout;
            return int.Parse(resp);
        }

        public void SetNetworkInterface(int iface)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("Interface", iface.ToString());
            string resp = server.DoPostRequest(baseAddress + "Net/SetInterface", postData, this, null, false);
        }

        public WifiConfig GetWifiConfig()
        {
            server.Timeout = 10000;
            string resp = server.DoGetRequest(baseAddress + "Net/GetWifi");
            server.Timeout = RestServices.DefaultTimeout;
            WifiConfig ret = new WifiConfig();
            ret.Key = Utils.GetRepResponseValue(resp, "Key");
            ret.Security = Utils.GetRepResponseValue(resp, "Security");
            ret.SSID = Utils.GetRepResponseValue(resp, "SSID");
            return ret;
        }

        public void SetWifiConfig(WifiConfig config)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("Key", config.Key);
            postData.Add("Security", config.Security);
            postData.Add("SSID", config.SSID);
            string resp = server.DoPostRequest(baseAddress + "Net/SetWifi", postData, this, null, false);
        }

        public int GetPrinterStatus()
        {
            string resp = server.DoGetRequest(baseAddress + "Printer/GetStatus");
            return int.Parse(resp);
        }

        public void GetBatteryInfo(ref int status, ref int percent)
        {
            string resp = server.DoGetRequest(baseAddress + "Battery/GetInfo");
            status = int.Parse(Utils.GetRepResponseValue(resp, "STATUS", "-1"));
            percent = int.Parse(Utils.GetRepResponseValue(resp, "LEVEL", "-1"));
        }

        public Empregador GetEmployer()
        {
            string uri = baseAddress + "MT/Employer";
            string resp = server.DoGetRequest(uri);
            Empregador ret = new Empregador();
            ret.CEI = Utils.GetRepResponseValue(resp, "CEI");
            ret.CNPJ = Utils.GetRepResponseValue(resp, "CNPJ");
            ret.Endereco = Utils.GetRepResponseValue(resp, "END");
            ret.Nome = Utils.GetRepResponseValue(resp, "NOME");
            ret.Tipo = Utils.GetRepResponseValue(resp, "TIPO");
            return ret;
        }

        public Empregado GetEmployee(FiltroEmpregado filtro, string valor)
        {
            string uri = baseAddress + "MT/Employee/MtGetEmployee/" + filtro.ToString() + "/" + valor;
            string resp = server.DoGetRequest(uri);
            Empregado ret = new Empregado();
            ret.CNTLS = Utils.GetRepResponseValue(resp, "CNTLS");
            ret.ID = Utils.GetRepResponseValue(resp, "ID");
            ret.KBD = Utils.GetRepResponseValue(resp, "KBD");
            ret.Nome = Utils.GetRepResponseValue(resp, "NOME");
            ret.PIS = Utils.GetRepResponseValue(resp, "PIS");
            return ret;
        }

        public MrpStatus GetMrpStatus()
        {
            string uri = baseAddress + "MRP/Status";
            string resp = server.DoGetRequest(uri);
            MrpStatus ret = new MrpStatus();
            ret.Build = Utils.GetRepResponseValue(resp, "Build");
            ret.FirstReg = Utils.GetRepResponseValue(resp, "First Reg");
            ret.Flags = Utils.GetRepResponseValue(resp, "Flags");
            ret.FreeSize = Utils.GetRepResponseValue(resp, "Free Size");
            ret.LastNSR = Utils.GetRepResponseValue(resp, "Last NSR");
            ret.LastReg = Utils.GetRepResponseValue(resp, "Last Reg");
            ret.NFR = Utils.GetRepResponseValue(resp, "NFR");
            ret.UsedSize = Utils.GetRepResponseValue(resp, "Used Size");
            ret.Version = Utils.GetRepResponseValue(resp, "Version");
            return ret;
        }

        public List<string> GetRegs(FiltroRegistro filtro, string rangeFrom, string rangeTo)
        {
            cancelOperation = false;
            string uri = baseAddress + "MRP/Regs/" + filtro.ToString();
            if (filtro == FiltroRegistro.DateRange || filtro == FiltroRegistro.NsrRange)
            {
                uri += "/FROM/" + rangeFrom + "/TO/" + rangeTo;
            }
            string resp = new RestServices(10000).DoGetRequest(uri);
            uint readSize = 0;
            uint totalSize = uint.Parse(Utils.GetRepResponseValue(resp, "TotalSize"));
            List<string> ret = new List<string>();
            while (readSize < totalSize)
            {
                if (cancelOperation)
                {
                    new RestServices(10000).DoGetRequest(baseAddress + "MRP/Regs/Finish");
                    throw new Exception("Operação cancelada");
                }

                edLog.Text += readSize + " " + totalSize;

                uint blockSize = totalSize - readSize;
                Trace.WriteLine(DateTime.Now.ToLongTimeString() + ": Before request to " + baseAddress + "MRP/Regs/NextReg");
                resp = new RestServices(10000).DoGetRequest(baseAddress + "MRP/Regs/NextReg");
                Trace.WriteLine(DateTime.Now.ToLongTimeString() + ": After response, before parse");
                foreach (string kvp in resp.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (kvp.Contains(':'))
                    {
                        string k = kvp.Substring(0, kvp.IndexOf(':')).Trim();
                        string v = kvp.Substring(kvp.IndexOf(':') + 1).Trim();
                        switch (k)
                        {
                            case "BlockSize": blockSize = uint.Parse(v); break;
                            default: break;
                        }
                    }
                    else
                    {
                        string regs = kvp.TrimStart(new char[] { '{' }).TrimEnd(new char[] { '}' });
                        foreach (string r in regs.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            ret.Add(r);
                        }
                    }
                }
                Trace.WriteLine(DateTime.Now.ToLongTimeString() + ": After parse");
                readSize += blockSize;
            }
            new RestServices(10000).DoGetRequest(baseAddress + "MRP/Regs/Finish");
            return ret;
        }

        public byte[] GetTemplateFile(string ID)
        {
            string uri = baseAddress + "FP/Template/GetTemplate/ID/" + ID;
            string resp = server.DoGetRequest(uri);
            if (!resp.StartsWith("TEMPLATE:"))
            {
                throw new Exception("Resposta inválida");
            }
            string b64Template = resp.Substring("TEMPLATE:".Length).TrimStart(new char[] { '{' }).TrimEnd(new char[] { '}', ';' });
            return Convert.FromBase64String(b64Template);
        }

        public List<string> GetTemplateEnroll(string PIS)
        {
            string uri = baseAddress + "FP/Template/GetEnroll/PIS/" + PIS;
            string resp = server.DoGetRequest(uri);
            List<string> ret = new List<string>();
            foreach (string kvp in resp.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (kvp.Contains(':'))
                {
                    string k = kvp.Substring(0, kvp.IndexOf(':')).Trim();
                    string v = kvp.Substring(kvp.IndexOf(':') + 1).Trim();
                    switch (k)
                    {
                        case "ID":
                            ret.Add(v);
                            break;
                        default:
                            break;
                    }
                }
            }
            return ret;
        }

        public void MtFormat()
        {
            string resp = server.DoPostRequest(baseAddress + "MT/Format", null, this);
        }

        public MtStatus GetMtStatus()
        {
            string resp = server.DoGetRequest(baseAddress + "MT/GetStatus");
            MtStatus ret = new MtStatus();
            foreach (string kvp in resp.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (kvp.Contains(':'))
                {
                    string k = kvp.Substring(0, kvp.IndexOf(':')).Trim();
                    string v = kvp.Substring(kvp.IndexOf(':') + 1).Trim();
                    switch (k)
                    {
                        case "ENROLLED EMPLOYEES": ret.EnrolledEmployees = v; break;
                        case "AVAIABLE MEMORY": ret.AvailableMemory = v; break;
                        case "FREE EMPLOYEES": ret.FreeEmployees= v; break;
                        case "ENROLLED EMPLOYER": ret.EnrolledEmployer = v; break;
                        default: break;
                    }
                }
            }
            return ret;
        }

        public string RemoveEmployee(string PIS)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("PIS", PIS);
            return server.DoPostRequest(baseAddress + "MT/Employee/RemoveEmployee", postData, this, new ErrorCodes[] { ErrorCodes.MT_RES_ERROR_NOT_ENROLLED_EMPLOYEE });
        }

        public string SetEmployee(Empregado empregado)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("PIS", empregado.PIS);
            postData.Add("ID", empregado.ID);
            postData.Add("NOME", empregado.Nome);
            postData.Add("CNTLS", empregado.CNTLS);
            postData.Add("KBD", empregado.KBD);
            return server.DoPostRequest(baseAddress + "MT/Employee/SetEmployee", postData, this, new ErrorCodes[] { ErrorCodes.MT_RES_ERROR_ENROLLED_ID, ErrorCodes.MT_RES_ERROR_ENROLLED_KEYBOARD, ErrorCodes.MT_RES_ERROR_ENROLLED_PIS });
        }

        public string EditEmployee(Empregado empregado)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("PIS", empregado.PIS);
            postData.Add("ID", empregado.ID);
            postData.Add("NOME", empregado.Nome);
            postData.Add("CNTLS", empregado.CNTLS);
            postData.Add("KBD", empregado.KBD);
            return server.DoPostRequest(baseAddress + "MT/Employee/EditEmployee", postData, this);
        }

        public string SetTime(DateTime time)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("HORA", time.ToString("HH"));
            postData.Add("MIN", time.ToString("mm"));
            postData.Add("SEG", time.ToString("ss"));
            postData.Add("DIA", time.ToString("dd"));
            postData.Add("MES", time.ToString("MM"));
            postData.Add("ANO", time.ToString("yy"));
            return server.DoPostRequest(baseAddress + "MRP/RTC/SetTime", postData, this);
        }

        public void SetNFR(string NFR)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("NFR", NFR);
            string resp = server.DoPostRequest(baseAddress + "MRP/SetNFR", postData, this, null, false);
        }

        public void RemoveEmployer()
        {
            string resp = server.DoPostRequest(baseAddress + "MT/Employer/RemoveEmployer", null, this);
        }

        public string SetEmployer(Empregador empregador)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("CNPJ", empregador.CNPJ);
            postData.Add("CEI", empregador.CEI);
            postData.Add("TIPO", empregador.Tipo);
            postData.Add("NOME", empregador.Nome);
            postData.Add("END", empregador.Endereco);
            return server.DoPostRequest(baseAddress + "MT/Employer/SetEmployer", postData, this);
        }

        public string EditEmployer(Empregador empregador)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("CNPJ", empregador.CNPJ);
            postData.Add("CEI", empregador.CEI);
            postData.Add("TIPO", empregador.Tipo);
            postData.Add("NOME", empregador.Nome);
            postData.Add("END", empregador.Endereco);
            return server.DoPostRequest(baseAddress + "MT/Employer/EditEmployer", postData, this);
        }

        public void SetPassword(string password)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("PSSWD", password);
           server.DoPostRequest(baseAddress + "USER/SetPasswd", postData, this);
        }

        public List<Empregado> GetAllEmployee(Action<int, int> progressCallback)
        {
            List<Empregado> ret = new List<Empregado>();
            cancelOperation = false;
            string resp = server.DoGetRequest(baseAddress + "MT/Employee/GetAll");
            Match m = Regex.Match(resp, @"ENROLLED EMPLOYEES:(\d+);");
            if (!m.Success) {
                throw new Exception("Resposta inválida");
            }
            int total = int.Parse(m.Groups[1].Value);
            int read = 0;
            bool stop = false;
            int tries = 0;
            int maxTries = 3;
            while (!stop)
            {
                try
                {
                    if (cancelOperation)
                    {
                        tries = maxTries;
                        throw new Exception("Operação cancelada");
                    }
                    string r = server.DoGetRequest(baseAddress + "MT/Employee/GetNext");
                    int employees = 0;
                    foreach (string l in r.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        m = Regex.Match(l, @"(.+),(\d+),(\d+),(\d+),([0-9a-fA-F]+);");
                        if (m.Success)
                        {
                            Empregado e = new Empregado();
                            e.Nome = m.Groups[1].Value;
                            e.PIS = m.Groups[2].Value;
                            e.ID = m.Groups[3].Value;
                            e.KBD = m.Groups[4].Value;
                            e.CNTLS = m.Groups[5].Value;
                            ret.Add(e);
                            employees++;
                            read++;
                        }
                    }
                    if (progressCallback != null)
                    {
                        progressCallback(read, total);
                    }
                    stop = employees == 0;
                    tries = 0;
                }
                catch (Exception ex)
                {
                    tries++;
                    if (tries > maxTries)
                    {
                        throw ex;
                    }
                }
            }
            return ret;
        }

        public void SetMultiplier(int amount)
        {
            throw new NotImplementedException();
        }

    }
}
