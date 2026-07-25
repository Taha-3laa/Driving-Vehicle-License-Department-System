using System;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using DLVDBusinessLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Linq;

namespace Drives_and_Vehicles_License
{
    public static class ClsGlobal 
    {
        static string CurrentUserKey;
        public static ClsUser CurrentUser { get; set; } 
        static ClsGlobal () 
        {
            CurrentUserKey = @"HKEY_CURRENT_USER\SOFTWARE\DLVD";
        }       
        public static void DeleteFile ()
        {
            try
            {
                string Subkey = CurrentUserKey.Substring(@"HKEY_CURRENT_USER\".Length);
                // Open the registry key in read/write mode with explicit registry view
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {

                    using (RegistryKey key = baseKey.OpenSubKey(Subkey, true))
                    {
                        if (key != null)
                        {
                            // Delete the specified value
                            key.DeleteValue("User");
                            key.DeleteValue("Password");
                        }
                        else
                        {
                            Console.WriteLine($"Registry key '{CurrentUserKey}' not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            //  File.Delete(FilePath);
        }
        public static bool RememberUserNamePassword (string UserName,string Password) 
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                return false;

            string UserData = "User",PasswordData="Password";
            try
            {
                Registry.SetValue(CurrentUserKey, UserData, UserName);
                Registry.SetValue(CurrentUserKey, PasswordData, Password);
                return true; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public static bool GetUserNamePassword (ref string UserName,ref string Password) 
        {
            string UserData = "User", PasswordData = "Password";
            try 
            {
                UserName = Registry.GetValue(CurrentUserKey, UserData, null) as string;
                Password = Registry.GetValue(CurrentUserKey, PasswordData, null)as string;
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return false;   
            }
        
        }
    }
}
