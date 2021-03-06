﻿
namespace Platform
{

    // superseeded by 
    // D:\username\Documents\Visual Studio 2017\Projects\libWkHtml2X\libWkHtmlToX\Loader
    public class SharedLibrary
    {

        [System.Runtime.InteropServices.DllImport("kernel32.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall
            , ExactSpelling = true, SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        protected static extern System.IntPtr LoadLibraryW(string lpFileName);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall 
            , ExactSpelling = true, SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        protected static extern System.IntPtr GetProcAddress(System.IntPtr hModule, string procName);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall 
            , ExactSpelling = true, SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        protected static extern bool FreeLibrary(System.IntPtr hModule);
        



        // See http://mpi4py.googlecode.com/svn/trunk/src/dynload.h
        protected const int RTLD_LAZY = 1; // for dlopen's flags
        protected const int RTLD_NOW = 2; // for dlopen's flags

        [System.Runtime.InteropServices.DllImport("libdl", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl
            , CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        protected static extern System.IntPtr dlopen(string filename, int flags);

        [System.Runtime.InteropServices.DllImport("libdl", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl
            , CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        protected static extern System.IntPtr dlsym(System.IntPtr handle, string symbol);

        [System.Runtime.InteropServices.DllImport("libdl", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl
            , CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        protected static extern int dlclose(System.IntPtr handle);

        [System.Runtime.InteropServices.DllImport("libdl", CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl
            , CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        protected static extern string dlerror();



        public static T LoadSymbol<T>(System.IntPtr handle, string symbol)
        {
            System.IntPtr function = System.IntPtr.Zero;
            
            
            if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
            {
                function = dlsym(handle, symbol);
                if (function == System.IntPtr.Zero)
                {
                    string message = dlerror();
                    System.Console.WriteLine(message);
                } // End if (hSO == System.IntPtr.Zero) 
            }
            else
            {
                function = GetProcAddress(handle, symbol);
                if (function == System.IntPtr.Zero)
                {
                    System.ComponentModel.Win32Exception ex = new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
                    System.Console.WriteLine(ex.Message);
                } // End if (hSO == System.IntPtr.Zero) 
            }


            if (function == System.IntPtr.Zero)
            {
                throw new System.ApplicationException("Cannot find symbol \"" + symbol + "\".");
            } // End if (hSO == IntPtr.Zero)

            System.Type type = typeof(T);

            if(!type.IsSubclassOf(typeof(System.Delegate)))
            {
                throw new System.InvalidOperationException($"Type \"{type.Name}\" is not a delegate type");
            }

            System.Delegate delegateInstance = System.Runtime.InteropServices.Marshal
                .GetDelegateForFunctionPointer(function, typeof(T));
            
            return (T) (object) delegateInstance;
        }



        // Note: Not thread safe, but OK on app-start / end
        protected static System.Collections.Generic.Dictionary<string, System.IntPtr> m_dict_LoadedDlls =
            new System.Collections.Generic.Dictionary<string, System.IntPtr>();

        public static System.IntPtr Load(string strFileName)
        {
            System.IntPtr hSO = System.IntPtr.Zero;

            try
            {

                if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
                {
                    hSO = dlopen(strFileName, RTLD_NOW);
                    if (hSO == System.IntPtr.Zero)
                    {
                        string message = dlerror();
                        System.Console.WriteLine(message);
                    } // End if (hSO == System.IntPtr.Zero) 
                }
                else
                {
                    hSO = LoadLibraryW(strFileName);
                    if (hSO == System.IntPtr.Zero)
                    {
                        System.ComponentModel.Win32Exception ex = new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());
                        System.Console.WriteLine(ex.Message);
                    } // End if (hSO == System.IntPtr.Zero) 

                } // End if (Environment.OSVersion.Platform == PlatformID.Unix)

            } // End Try
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            } // End Catch

            if (hSO == System.IntPtr.Zero)
            {
                throw new System.ApplicationException("Cannot open " + strFileName);
            } // End if (hSO == IntPtr.Zero)

            m_dict_LoadedDlls.Add(strFileName, hSO);

            return hSO;
        } // End Function LoadSharedObject


        public static bool Unload(System.IntPtr hSO)
        {
            bool bError = true;

            if (hSO == System.IntPtr.Zero)
            {
                throw new System.ArgumentNullException("hSO");
            } // End if (hSO == IntPtr.Zero)

            try
            {

                if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
                {
                    // If the referenced object was successfully closed, dlclose() shall return 0. 
                    // If the object could not be closed, or if handle does not refer to an open object, 
                    // dlclose() shall return a non-zero value. 
                    // More detailed diagnostic information shall be available through dlerror().

                    // http://stackoverflow.com/questions/956640/linux-c-error-undefined-reference-to-dlopen
                    if(dlclose(hSO) == 0)
                        bError = false;

                    if (bError)
                        throw new System.ApplicationException("Error unloading handle " + hSO.ToInt64().ToString()
                            + System.Environment.NewLine + "System error message: " + dlerror());
                }
                else
                {
                    // FreeLibrary: If the function succeeds, the return value is nonzero.
                    // If the function fails, the return value is zero. 
                    // To get extended error information, call the GetLastError function.
                    bError = !FreeLibrary(hSO);

                    if(bError)
                        throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()); 

                } // End if (Environment.OSVersion.Platform == PlatformID.Unix)

            } // End Try
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            } // End Catch

            if (bError)
            {
                throw new System.ApplicationException("Cannot unload handle " + hSO.ToInt64().ToString());
            } // End if (hExe == IntPtr.Zero)

            return bError;
        } // End Function Unload


        public static void UnloadAllLoadedDlls()
        {
            foreach (string strKey in m_dict_LoadedDlls.Keys)
            {
                try
                {
                    Unload(m_dict_LoadedDlls[strKey]);
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error unloading \"" + strKey + "\".");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

            } // Next strKey

        } // End Sub UnloadAllLoadedDlls


    } // End Class SharedLibrary


} // End Namespace Platform
