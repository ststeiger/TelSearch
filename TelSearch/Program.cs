
namespace TelSearch
{
    class Program
    {
        
        private const string JULIA_CPP_DLL = "/root/Downloads/julia-d55cadc350/lib/julia/libstdc++.so.6";
        private const string JULIA_LLVM_DLL = "/root/Downloads/julia-d55cadc350/lib/julia/libLLVM.so";
        private const string JULIA_DLL = "/root/Downloads/julia-d55cadc350/lib/libjulia.so.0.6.3";
    
        
        
        //private const string DLL = "libjulia.so";
        private const string DLL = "/root/Downloads/julia-d55cadc350/lib/libjulia.so";
        //private const string DLL = "/usr/lib/x86_64-linux-gnu/julia/libjulia.so";
        

        
        // no function with such signature in julia...
        // [System.Runtime.InteropServices.DllImport(DLL)]
        // public static extern void jl_init(string julia_home_dir);
        
        [System.Runtime.InteropServices.DllImport(DLL)]
        static extern void jl_init();
        
        [System.Runtime.InteropServices.DllImport(DLL)]
        static extern void jl_eval_string(string message);
        
        
        
        delegate void jl_init_t();
        delegate void jl_eval_string_t(string message);

        
        
        
        public static void SetPath()
        {
            string[] paths = new string[]
            {
                "/root/Downloads/julia-d55cadc350",
                "/root/Downloads/julia-d55cadc350/bin",
                "/root/Downloads/julia-d55cadc350/lib",
                "/root/Downloads/julia-d55cadc350/lib/julia"
            };

            string additional_paths = string.Join(":", paths);
            
            
            string path = System.Environment.GetEnvironmentVariable("PATH");
            if (!path.EndsWith(":"))
                path += ":";
            
            //path += "/usr/lib/x86_64-linux-gnu/julia/libjulia.so";
            // path += "/usr/lib/x86_64-linux-gnu/julia";
            // path += "/root/Downloads/julia-d55cadc350/lib";
            path += additional_paths;
            
            // path += ":";
            System.Environment.SetEnvironmentVariable("PATH", path, System.EnvironmentVariableTarget.Process);
            //System.Environment.SetEnvironmentVariable("LD_LIBRARY_PATH", "/usr/lib/x86_64-linux-gnu/julia"); //, System.EnvironmentVariableTarget.Process);
            
            // System.Environment.SetEnvironmentVariable("LD_LIBRARY_PATH", additional_paths);
            string ld_preload = "/root/Downloads/julia-d55cadc350/lib/libjulia.so";
            System.Environment.SetEnvironmentVariable("LD_PRELOAD", ld_preload);
        }
        
        
        public static void ancient()
        {
            SetPath();
            
            // string home = "/usr/lib/x86_64-linux-gnu/julia";
            // string home = "/usr/lib/x86_64-linux-gnu";
            //string home = "/usr/lib";
            // System.IO.Directory.SetCurrentDirectory("/usr/bin");
            // System.IO.Directory.SetCurrentDirectory("/root/Downloads/julia-d55cadc350/bin");
            
            // string home = "/usr/bin";
            // string home = "/root/Downloads/julia-d55cadc350";
            // string home = "/root/Downloads/julia-d55cadc350/lib/julia";
            //string home = "/root/Downloads/julia-d55cadc350/bin";
            
            // jl_init(null);
            // jl_init(home);
        }

        // export LD_PRELOAD="/root/Downloads/julia-d55cadc350/lib/libjulia.so"
        // dotnet TelSearch.dll 
        static void Main(string[] args)
        {
            System.IntPtr hmoduleCPP = Platform.SharedLibrary.Load(JULIA_CPP_DLL);
            System.IntPtr hmoduleLLVM = Platform.SharedLibrary.Load(JULIA_LLVM_DLL);
            System.IntPtr hmodule = Platform.SharedLibrary.Load(JULIA_DLL);
            
            jl_init_t init = Platform.SharedLibrary.LoadSymbol<jl_init_t>(hmodule, "jl_init");
            jl_eval_string_t eval_string = Platform.SharedLibrary.LoadSymbol<jl_eval_string_t>(hmodule, "jl_eval_string");
            
            init();
            eval_string("println(sqrt(2.0))");
            
            // jl_init();
            // jl_eval_string("println(sqrt(2.0))");
            System.Console.WriteLine("Hello World!");
            
            Platform.SharedLibrary.Unload(hmodule);
            Platform.SharedLibrary.Unload(hmoduleLLVM);
            Platform.SharedLibrary.Unload(hmoduleCPP);
        }
        
        
    }
    
    
}
