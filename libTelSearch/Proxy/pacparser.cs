
namespace libTelSearch.Proxy 
{


    public class PacParser
    {


        static string getHostName(string host)
        {
            string[] a_host = host.Split(new char[] { '.' });

            return (a_host.Length > 2 ? a_host[0] : "");
        }

        static string getDnsDomain(string host)
        {
            string[] a_host = host.Split(new char[] { '.' });

            return (a_host[0]);
        }

        static string dnsResolve(string host)
        {
            System.Net.IPHostEntry hostInfo = System.Net.Dns.GetHostEntry(host);
            string s = "";

            foreach (System.Net.IPAddress ip in hostInfo.AddressList)
            {
                if (s.Length > 0)
                    s += ",";
                s += ip;
            }

            return (s);
        }


        static bool isInNet(string resolvedHost, string pattern, string mask)
        {
            string[] a_resolvedHost = resolvedHost.Split(new char[1] { '.' });
            string[] a_pattern = pattern.Split(new char[1] { '.' });
            string[] a_mask = mask.Split(new char[1] { '.' });
            int len = a_resolvedHost.Length;

            if (len != a_pattern.Length || len != a_mask.Length)
                return (false);

            for (int i = 0; i < len; i++)
                if (System.Convert.ToInt32(a_pattern[i]) != (System.Convert.ToInt32(a_resolvedHost[i]) & System.Convert.ToInt32(a_mask[i])))
                    return (false);

            return (true);
        }


        static bool isPlainHostName(string host)
        {
            return (host.IndexOf('.') == -1);
        }


        static bool localHostOrDomainIs(string host, string match)
        {
            return (host == match || host == getHostName(match));
        }


        static bool dnsDomainIs(string host, string match)
        {
            //int l_host = host.Length;
            //int l_match = match.Length;

            //if (l_host == 0 || l_match == 0 || l_host < l_match)
            //    return (false);
            //else if (l_host == l_match)
            //    return (host == match);

            //return (host.Substring(l_host - l_match) == match);

            return (getDnsDomain(host) == match);
        }


        static string myIpAddress()
        {
            System.Net.IPHostEntry host;
            string localIP = "?";

            host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (System.Net.IPAddress ip in host.AddressList)
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    localIP = ip.ToString();

            return (localIP);
        }


        public static System.Collections.Generic.List<System.Uri> GetWebProxy()
        {
            System.Collections.Generic.List<System.Uri> ls = new System.Collections.Generic.List<System.Uri>();
            System.Uri uri = new System.Uri("https://www.microsoft.com/foobar.htm");


            // string pacFile = @"D:\username\Downloads\PacProxyUsage_src\Proxy.pac";
            
            //string script = null;
            //using (System.IO.StreamReader streamReader = new System.IO.StreamReader(pacFile))
            //{
            //    script = streamReader.ReadToEnd();
            //    streamReader.Close();
            //}

            string script = @"
function FindProxyForURL(url, host) 
{  
    //if (isInNet(myIpAddress(), ""192.168.1.0"", ""255.255.255.0""))
    if (true)
        return ""PROXY 192.168.1.1:8080"";
    else
        return ""DIRECT"";
}
";


            string proxyList = null;

            Jint.Engine engine = new Jint.Engine()
                .SetValue("dnsResolve", new System.Func<string, string>(dnsResolve))
                .SetValue("isInNet", new System.Func<string, string, string, bool>(isInNet))
                .SetValue("isPlainHostName", new System.Func<string, string>(dnsResolve))
                .SetValue("localHostOrDomainIs", new System.Func<string, string>(dnsResolve))
                .SetValue("dnsDomainIs", new System.Func<string, string>(dnsResolve))
                .SetValue("myIpAddress", new System.Func<string, string>(dnsResolve))
                //.SetValue("log", new System.Action<object>(Console.WriteLine))
                // .SetValue("log", new System.Action<string>(delegate (object obj) { System.Console.WriteLine(obj); }))
                .SetValue("getProxy", new System.Action<string>(delegate (string obj) { proxyList = obj; if (proxyList != null) proxyList = proxyList.Trim(); }))
            ;

            engine.Execute(script);
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Proxy_servers_and_tunneling/Proxy_Auto-Configuration_(PAC)_file
            engine.Execute(string.Format(@"getProxy(FindProxyForURL('{0}', '{1}'));", uri.Authority, uri.Host));

            if (string.IsNullOrEmpty(proxyList))
                return ls;

            string[] proxies = proxyList.Split(';');

            for (int i = 0; i < proxies.Length; ++i)
            {
                proxies[i] = proxies[i].Trim();

                if ("DIRECT".Equals(proxies[i], System.StringComparison.OrdinalIgnoreCase))
                {
                    ls.Add(null);
                    continue;
                }

                if (proxies[i].StartsWith("proxy", System.StringComparison.OrdinalIgnoreCase))
                {
                    proxies[i] = proxies[i].Substring("proxy".Length);
                    proxies[i] = "https://" + proxies[i].Trim();

                    System.Uri retValue = new System.Uri(proxies[i], System.UriKind.RelativeOrAbsolute);
                    ls.Add(retValue);
                    continue;
                }
            }

            return ls;
        }


        public static void Test()
        {
            string pacFile = @"D:\username\Downloads\PacProxyUsage_src\Proxy.pac";
            System.Uri uri = new System.Uri("https://www.microsoft.com/foobar.htm");
            
            string proxy = null;


            Jint.Engine engine = new Jint.Engine()
                .SetValue("dnsResolve", new System.Func<string, string>(dnsResolve))
                .SetValue("isInNet", new System.Func<string, string, string, bool>(isInNet))
                .SetValue("isPlainHostName", new System.Func<string, string>(dnsResolve))
                .SetValue("localHostOrDomainIs", new System.Func<string, string>(dnsResolve))
                .SetValue("dnsDomainIs", new System.Func<string, string>(dnsResolve))
                .SetValue("myIpAddress", new System.Func<string, string>(dnsResolve))
                //.SetValue("log", new System.Action<object>(Console.WriteLine))
                // .SetValue("log", new System.Action<string>(delegate (object obj) { System.Console.WriteLine(obj); }))
                .SetValue("getProxy", new System.Action<string>(delegate (string obj) { proxy = obj; } ) )
            ;



            string script = null;
            using (System.IO.StreamReader streamReader = new System.IO.StreamReader(pacFile))
            {
                script = streamReader.ReadToEnd();
                streamReader.Close();
            }
            engine.Execute(script);
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Proxy_servers_and_tunneling/Proxy_Auto-Configuration_(PAC)_file
            engine.Execute(string.Format(@"getProxy(FindProxyForURL('{0}', '{1}'));", uri.Authority, uri.Host));
            System.Console.WriteLine(proxy);



            // engine.Execute(@"
            // log(dnsResolve('www.perdu.com'));
            // log(isInNet('198.95.249.79', '198.95.249.79', '255.255.255.255'));
            // log(isInNet('198.95.249.79', '198.95.0.0', '255.255.0.0'));
            // log(isInNet('198.95.249.12', '198.95.249.79', '255.255.255.255'));
            // log(isInNet('198.95.249.12', '198.95.0.0', '255.255.0.0'));
            // log(isInNet('198.12.249.79', '198.95.0.0', '255.255.0.0'));
            // ");

            //Console.Read();
        }


    }


}
