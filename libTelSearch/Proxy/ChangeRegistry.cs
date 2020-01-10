using System;
using System.Collections.Generic;
using System.Text;

namespace libTelSearch.Proxy
{
    class ChangeRegistry
    {

        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        internal static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out System.IntPtr phToken);



        public static void Test()
        {

            System.Security.Principal.NTAccount ntuser = new System.Security.Principal.NTAccount(@"cor\stefan.steiger");
            System.Security.Principal.SecurityIdentifier sID = (System.Security.Principal.SecurityIdentifier)ntuser.Translate(typeof(System.Security.Principal.SecurityIdentifier));
            string strSID = sID.Value;
            // Mozilla/4.0 (compatible; MSIE 8.0; Win32)
            // https://stackoverflow.com/questions/9978193/how-to-edit-the-registry-keys-of-a-specific-user-programatically/9986068
            // HKCU is just a symbolic link for one of the keys under HKEY_USERS. 
            // If you know the SID of that user, then you can find it in there. You can get the SID as so:
            // The better option is to impersonate. The second option might work better when you don't know that user's credentials. 
            // The disadvantage is you will need administrative rights to write in someone else's account.

            //  Microsoft.Win32.Registry.Users.SetValue(strSID + @"\Software\Microsoft\Windows\CurrentVersion\Internet Settings\User Agent", "Test", Microsoft.Win32.RegistryValueKind.String);


            //using (var impersonation = new Impersonate(username, password))
            //{
            //    ChangeRegistry(keys, values);
            //}

            // https://stackoverflow.com/questions/125341/how-do-you-do-impersonation-in-net
            // https://github.com/mj1856/SimpleImpersonation
            // https://stackoverflow.com/questions/6564153/edit-registry-key-of-other-user
            System.IntPtr handle;
            LogonUser("stefan.steiger", "cor", "Inspiron1370", 2, 0, out handle);
            Microsoft.Win32.SafeHandles.SafeAccessTokenHandle safeHandle = new Microsoft.Win32.SafeHandles.SafeAccessTokenHandle(handle);


            System.Security.Principal.WindowsIdentity.RunImpersonated(safeHandle, delegate () {
                System.Console.WriteLine(System.Environment.UserName);
            });

        }

    }
}
