
namespace TelSearch
{


    class Program
    {

#if true 
        private const string SHARED_OBJECT_EXTENSION = @".dll";
        private const string JULIA_HOME = @"D:\Programme\Julia-0.6.3\";
        
        private const string JULIA_CPP_DLL = JULIA_HOME + @"bin\libstdc++-6.dll";
        private const string JULIA_LLVM_DLL = JULIA_HOME + @"bin\LLVM.dll";
        private const string JULIA_DLL = JULIA_HOME + @"bin\libjulia.dll";

#else
        private const string SHARED_OBJECT_EXTENSION = @".so";
        private const string JULIA_HOME = "/root/Downloads/julia-d55cadc350/";
        
        private const string JULIA_CPP_DLL = JULIA_HOME + "lib/julia/libstdc++.so.6";
        private const string JULIA_LLVM_DLL = JULIA_HOME + "lib/julia/libLLVM.so";
        private const string JULIA_DLL = JULIA_HOME + "lib/libjulia.so";

#endif


        delegate void jl_init_t();
        delegate void jl_eval_string_t(string message);
        delegate void jl_atexit_hook_t(int status);


        //private const string DLL = "libjulia.so";
        //private const string DLL = "/usr/lib/x86_64-linux-gnu/julia/libjulia.so";
        private const string DLL = "/root/Downloads/julia-d55cadc350/lib/libjulia.so";


        enum JL_IMAGE_SEARCH : int 
        {
            JL_IMAGE_CWD = 0,
            JL_IMAGE_JULIA_HOME = 1,
            //JL_IMAGE_LIBJULIA = 2,
        }


        // typedef enum { JL_IMAGE_CWD = 0, JL_IMAGE_JULIA_HOME = 1, //JL_IMAGE_LIBJULIA = 2, } JL_IMAGE_SEARCH;
        // JL_DLLEXPORT void julia_init(JL_IMAGE_SEARCH rel);
        [System.Runtime.InteropServices.DllImport(DLL)]
        static extern void jl_init(JL_IMAGE_SEARCH rel);

        // JL_DLLEXPORT void jl_init_with_image(const char* julia_home_dir, const char* image_relative_path);
        [System.Runtime.InteropServices.DllImport(DLL)]
        static extern void jl_init(string julia_home_dir, string image_relative_path);

        // no function with such signature in julia...
        // [System.Runtime.InteropServices.DllImport(DLL)]
        // public static extern void jl_init(string julia_home_dir);

        // JL_DLLEXPORT void jl_init(void);
        [System.Runtime.InteropServices.DllImport(DLL)]
        static extern void jl_init();

        // JL_DLLEXPORT jl_value_t *jl_eval_string(const char *str);
        [System.Runtime.InteropServices.DllImport(DLL)]
        static extern void jl_eval_string(string message);


        // JL_DLLEXPORT void jl_atexit_hook(int status);
        [System.Runtime.InteropServices.DllImport(DLL)]
        static extern void jl_atexit_hook(int status);


        // https://stackoverflow.com/questions/35994959/embedding-julia-in-c-sharp-passing-and-returning-arugments
        [System.Runtime.InteropServices.DllImport(DLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
         static extern object jl_get_function(object m, string name);

        [System.Runtime.InteropServices.DllImport(DLL, CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl)]
        static extern object jl_call0(object m);


        public static void SetPath()
        {
            bool isPosixOs = System.Environment.OSVersion.Platform == System.PlatformID.Unix;
            // System.PlatformID.Unix = true for Linux & Mac & Unix 
            string envSeparator = isPosixOs ? ":" : ";";
            char pathSeparator = System.IO.Path.DirectorySeparatorChar;

            string[] paths = new string[]
            {
                 JULIA_HOME 
                ,System.IO.Path.Combine(JULIA_HOME, "bin") 
                ,System.IO.Path.Combine(JULIA_HOME, "lib") 
                ,System.IO.Path.Combine(JULIA_HOME, "lib", "julia") 
            };

            for (int i = 0; i < paths.Length; ++i)
            {
                if (paths[i].EndsWith(pathSeparator))
                {
                    paths[i] = paths[i].Substring(0, paths[i].Length - 1);
                } // End if (paths[i].EndsWith(System.IO.Path.DirectorySeparatorChar)) 

            } // Next i 
            
            string additional_paths = string.Join(envSeparator, paths);

            string path = System.Environment.GetEnvironmentVariable("PATH");
            if (!path.EndsWith(envSeparator))
            {
                if (!string.IsNullOrEmpty(additional_paths))
                {
                    path += envSeparator;
                    path += additional_paths;
                    System.Environment.SetEnvironmentVariable("PATH", path, System.EnvironmentVariableTarget.Process);
                    
                    if (isPosixOs)
                    {
                        //System.Environment.SetEnvironmentVariable("LD_LIBRARY_PATH", "/usr/lib/x86_64-linux-gnu/julia"); //, System.EnvironmentVariableTarget.Process);
                        System.Environment.SetEnvironmentVariable("LD_LIBRARY_PATH", additional_paths);
                    } // End if (isPosixOs) 

                } // End if (!string.IsNullOrEmpty(additional_paths)) 

            } // End if (!path.EndsWith(envSeparator))

            if (isPosixOs)
                System.Environment.SetEnvironmentVariable("LD_PRELOAD", DLL);
        } // End Sub SetPath 


        public static void Ancient()
        {
            // string home = "/usr/lib/x86_64-linux-gnu/julia";
            // string home = "/usr/lib/x86_64-linux-gnu";
            // string home = "/usr/lib";
            // System.IO.Directory.SetCurrentDirectory("/usr/bin");
            // System.IO.Directory.SetCurrentDirectory("/root/Downloads/julia-d55cadc350/bin");

            // string home = "/usr/bin";
            // string home = "/root/Downloads/julia-d55cadc350";
            // string home = "/root/Downloads/julia-d55cadc350/lib/julia";
            // string home = "/root/Downloads/julia-d55cadc350/bin";

            // jl_init(null);
            // jl_init(home);

            jl_init();
            jl_eval_string("println(sqrt(2.0))");
            jl_atexit_hook(0);
        } // End Sub Ancient 
        

        public static void WithImage()
        {
            jl_init(@"C:\Users\steven liekens.BRAIN2\AppData\Local\julia-e44b593905\bin", "");
        }


        // export LD_PRELOAD="/root/Downloads/julia-d55cadc350/lib/libjulia.so"
        // dotnet TelSearch.dll 
        static void Main(string[] args)
        {
            SetPath();
            
            // D:\Programme\Julia-0.6.3\bin\libstdc++-6.dll
            // D:\Programme\Julia-0.6.3\bin\libstdc++-6.dll

            // System.IntPtr hmoduleCPP = Platform.SharedLibrary.Load(JULIA_CPP_DLL);
            // System.IntPtr hmoduleLLVM = Platform.SharedLibrary.Load(JULIA_LLVM_DLL);
            System.IntPtr hmodule = Platform.SharedLibrary.Load(JULIA_DLL);
            
            jl_init_t init = Platform.SharedLibrary.LoadSymbol<jl_init_t>(hmodule, "jl_init");
            jl_eval_string_t eval_string = Platform.SharedLibrary.LoadSymbol<jl_eval_string_t>(hmodule, "jl_eval_string");
            jl_atexit_hook_t atexit_hook = Platform.SharedLibrary.LoadSymbol<jl_atexit_hook_t>(hmodule, "jl_atexit_hook");

            init();
            eval_string("println(sqrt(2.0))");
            atexit_hook(0);
            

            Platform.SharedLibrary.Unload(hmodule);
            // Platform.SharedLibrary.Unload(hmoduleLLVM);
            // Platform.SharedLibrary.Unload(hmoduleCPP);

            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class Program 
    

} // End Namespace TelSearch 
