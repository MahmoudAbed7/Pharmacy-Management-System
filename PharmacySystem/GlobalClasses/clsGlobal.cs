using Microsoft.Win32;
using PharmacyBusinessLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.GlobalClasses
{
    public class clsGlobal
    {
        public static clsUser CurrentUser;
        private static string KeyPath = @"HKEY_Current_User\SOFTWARE\PharmacyApp";
        private static string sourceName = "Pharmacy_App";
     
        public static bool RememberUserNameAndPassword(string UserName, string Password)
        {
            string ValueName = "PharmacyApp_LoginInfo";
            string ValueData = UserName + "/##/" + Password;
            try
            {
                Registry.SetValue(KeyPath, ValueName, ValueData, RegistryValueKind.String);
                EventLog.WriteEntry(sourceName, "UserName & Password Saved On Registry", EventLogEntryType.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                EventLog.WriteEntry(sourceName, ex.Message, EventLogEntryType.Error);
                return false;
            }
        }

        public static bool GetStoredCredentialFromRegistry(ref string UserName, ref string Password)
        {
            string ValueName = "PharmacyApp_LoginInfo";
            try
            {
                string StoredData = Registry.GetValue(KeyPath, ValueName, null) as string;
                if (StoredData != null)
                {
                    string[] Line = StoredData.Split(new string[] { "/##/" }, StringSplitOptions.None);
                    UserName = Line[0];
                    Password = Line[1];
                }

                EventLog.WriteEntry(sourceName, "UserName & Password Restored From Registry", EventLogEntryType.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                EventLog.WriteEntry(sourceName, ex.Message, EventLogEntryType.Error);
                return false;
            }
        }

        public static bool AccessPermission(clsUser.enPermission permission)
        {
            return clsGlobal.CurrentUser.checkPermission((int)permission);
        }
    }
}
