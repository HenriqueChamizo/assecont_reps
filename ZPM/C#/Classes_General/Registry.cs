using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Wr
{
    class WinRegistry
    {
        RegistryKey key;
        string Company = "";
        string App = "";

        public WinRegistry(string Company, string App)
        {
            this.Company = Company;
            this.App = App;
        }

        private string getAppKeyString()
        {
            return String.Format("Software\\{0}\\{1}", Company, App);
        }

        public void setValue(string Section, string Key, string Value)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(getAppKeyString() + "\\" + Section, RegistryKeyPermissionCheck.ReadWriteSubTree);

            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(getAppKeyString() + "\\" + Section, RegistryKeyPermissionCheck.ReadWriteSubTree);
            }

            key.SetValue(Key, Value);
            key.Close();
        }

        public string getValue(string Section, string Key, string Default = "")
        {
            string Result;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(String.Format("Software\\{0}\\{1}\\{2}", Company, App, Section));
            
            if (key != null)
            {
                Result = (string)key.GetValue(Key);

                if (Result == null) Result = Default;
                key.Close();
            }
            else
            {
                Result = Default;
            }

            return Result;
        }
    }
}
