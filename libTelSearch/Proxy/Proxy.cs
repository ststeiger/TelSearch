
namespace PacProxyUsage
{


    /// <summary>
    /// Summary description for Proxy.
    /// </summary>
    public class Proxy
    {


        public Proxy()
        { }


        /// <summary>
        /// Return proxy for requested Url
        /// </summary>
        /// <param name="sUrl">url</param>
        /// <param name="sPacUri">Uri to PAC file</param>
        /// <returns>proxy info</returns>
        public static string GetProxyForUrlUsingPac(string DestinationUrl, string PacUri)
        {
            System.IntPtr WinHttpSession = WinHttpAPI.WinHttpOpen("User", WinHttpAPI.WINHTTP_ACCESS_TYPE_DEFAULT_PROXY, System.IntPtr.Zero, System.IntPtr.Zero, 0);

            WinHttpAPI.WINHTTP_AUTOPROXY_OPTIONS ProxyOptions = new WinHttpAPI.WINHTTP_AUTOPROXY_OPTIONS();
            WinHttpAPI.WINHTTP_PROXY_INFO ProxyInfo = new WinHttpAPI.WINHTTP_PROXY_INFO();

            ProxyOptions.dwFlags = WinHttpAPI.WINHTTP_AUTOPROXY_CONFIG_URL;
            ProxyOptions.dwAutoDetectFlags = (WinHttpAPI.WINHTTP_AUTO_DETECT_TYPE_DHCP | WinHttpAPI.WINHTTP_AUTO_DETECT_TYPE_DNS_A);
            ProxyOptions.lpszAutoConfigUrl = PacUri;

            // Get Proxy 
            // https://en.wikipedia.org/wiki/Proxy_auto-config
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Proxy_servers_and_tunneling/Proxy_Auto-Configuration_(PAC)_file
            // https://support.microsoft.com/en-us/help/819961/how-to-configure-client-proxy-server-settings-by-using-a-registry-file
            // https://stackoverflow.com/questions/197725/programmatically-set-browser-proxy-settings-in-c-sharp
            // ContentType = "application/x-ns-proxy-autoconfig"
            bool IsSuccess = WinHttpAPI.WinHttpGetProxyForUrl(WinHttpSession, DestinationUrl, ref ProxyOptions, ref ProxyInfo);

            WinHttpAPI.WinHttpCloseHandle(WinHttpSession);

            if (IsSuccess)
            {
                return ProxyInfo.lpszProxy;
            }
            else
            {
                int err = WinHttpAPI.GetLastError();
                System.Console.WriteLine(err);
                // System.Console.WriteLine("Error: {0}", WinHttpAPI.GetLastError());

                string errorMessage = new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error()).Message;
                System.Console.WriteLine(errorMessage);

                return null;
            }
        }


    }


}
