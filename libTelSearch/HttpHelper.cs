
namespace TelSearch
{


    public class HttpRequest
    {

        private static string logFileLocation;


        public static void InitLogFile(string abc)
        {
            logFileLocation = abc;
        }


        public static void LogFile(string content)
        {
            try
            {
                System.IO.File.AppendAllText(logFileLocation, content, System.Text.Encoding.UTF8);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Kann Logfile nicht schreiben.");
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.StackTrace);
            }
            
        }


        public static void Log(object line)
        {
            if (line == null)
            {
                System.Console.Write("");
                return;
            }
                

            System.Console.Write(line.ToString());
            LogFile(line.ToString());
        }




        public static void LogLine(object line)
        {
            if (line == null)
            {
                System.Console.WriteLine();
                LogFile(System.Environment.NewLine);
                return;
            }
                

            System.Console.WriteLine(line.ToString());
            LogFile(line.ToString());
            LogFile(System.Environment.NewLine);
        }




        public static string HTTP_Post(string strInputString)
        {
            if (string.IsNullOrEmpty(strInputString) || strInputString.Trim() == "")
                throw new System.ArgumentNullException("strInputString is NULL/empty.");

            // Add reference to System.Web
            //string strStringToTranslate = System.Web.HttpUtility.UrlEncode(strInputString);
            System.Uri RequestURI = new System.Uri(strInputString, System.UriKind.Absolute);
            string strURL = RequestURI.AbsoluteUri;

            int iQuestionMarkPos = strURL.IndexOf('?');
            if (iQuestionMarkPos != -1)
                strURL = strURL.Substring(0, iQuestionMarkPos);
            
            string strQueryString = RequestURI.Query;
            
            if(strQueryString != null && strQueryString.StartsWith("?") && strQueryString.Length > 1)
            {
                strQueryString = strQueryString.Substring(1);
            }

            long lngNoCache = System.DateTime.Now.ToFileTimeUtc();

            //string strURL = "http://ajax.googleapis.com/ajax/services/language/detect?v=1.0&q=" + strStringToTranslate;
            //string strURL = "http://localhost:XXX/cgi-bin/GetData.ashx?NoCache=" + lngNoCache.ToString();


            // *** Establish the request
            System.Net.HttpWebRequest wrHTTPrequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strURL);


            //System.Web.HttpRequest.HttpMethod.
            // *** Set properties
            //wrHTTPrequest.Method = "POST";
            wrHTTPrequest.Method = System.Net.WebRequestMethods.Http.Post;

            //wrHTTPrequest.Timeout = 10000; // 10 secs
            wrHTTPrequest.Timeout = 5 * 60 * 1000; // 5 min
            wrHTTPrequest.UserAgent = "Lord Vishnu/Transcendental (Vaikuntha;Supreme Personality of Godness)";
            //wrHTTPrequest.UserAgent = ".NET VeloConnect Client 1.0";
            //wrHTTPrequest.Headers.Add("Accept-Language:" + System.Globalization.CultureInfo.CurrentCulture.Name);
            wrHTTPrequest.Headers.Add("Accept-Language", System.Globalization.CultureInfo.CurrentCulture.Name);
            //wrHTTPrequest.ContentType = "text/html";
            //wrHTTPrequest.ContentType = "text/xml";
            wrHTTPrequest.ContentType = "application/x-www-form-urlencoded";
            //wrHTTPrequest.ContentType = "application/x-www-form-urlencoded";
            // xhReq.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            //xhReq.setRequestHeader("Content-length", params.length);
            //xhReq.setRequestHeader("Connection", "close");
            wrHTTPrequest.Connection = "close";

            System.Text.Encoding enc = System.Text.Encoding.UTF8;


            // http://stackoverflow.com/questions/4088625/net-simplest-way-to-send-post-with-data-and-read-response
            byte[] bytes = enc.GetBytes(strQueryString);
            wrHTTPrequest.ContentLength = bytes.Length;
            System.IO.Stream rs = wrHTTPrequest.GetRequestStream();
            rs.Write(bytes, 0, bytes.Length); //Push it out there
            rs.Close();


            //wrHTTPrequest.GetRequestStream().Write(new byte[100], 0, 100)
            //wrHTTPrequest.ContentLength = wrHTTPrequest.GetRequestStream().Length;

            // *** Retrieve request info headers
            System.Net.HttpWebResponse wrHTTPresponse = (System.Net.HttpWebResponse)wrHTTPrequest.GetResponse();

            // My Windows' default code-Page
            //System.Text.Encoding enc = System.Text.Encoding.GetEncoding(1252);

            // Google's code-page
            

            System.IO.StreamReader srResponseStream = new System.IO.StreamReader(wrHTTPresponse.GetResponseStream(), enc);

            string strResponse = srResponseStream.ReadToEnd();
            wrHTTPresponse.Close();
            srResponseStream.Close();


            //MsgBox(strResponse)
            if (string.IsNullOrEmpty(strResponse))
                return null; // null;

            return strResponse;
        } // End Function HTTP_Post



        public static System.Net.WebProxy ConvertWebProxy(System.Net.IWebProxy iproxy)
        {
            System.Net.WebProxy wp = null;

            try
            {
                if (object.ReferenceEquals(iproxy.GetType(), typeof(System.Net.WebProxy)))
                    wp = (System.Net.WebProxy)iproxy;
                if (wp != null)
                    return wp;

                wp = ConvertWebProxWrapper(iproxy, "WebProxyWrapper");
                if (wp != null)
                    return wp;

                wp = ConvertWebProxWrapper(iproxy, "WebProxyWrapperOpaque");
                if (wp != null)
                    return wp;
            }
            catch (System.Exception ex)
            {
                LogLine(ex.Message);
                LogLine(ex.StackTrace);
            }

            return wp;
        } // End Function ConvertWebProxy 


        public static System.Net.WebProxy ConvertWebProxWrapper(System.Net.IWebProxy iproxy, string type)
        {
            System.Net.WebProxy obj = null;
            if (iproxy == null)
                return obj;

            System.Type t = iproxy.GetType();

            while (t != null && !string.Equals(t.Name, type, System.StringComparison.InvariantCultureIgnoreCase))
            {
                t = t.BaseType;
            } // Whend 

            if (t == null)
                return obj;

            System.Reflection.FieldInfo fi = t.GetField("webProxy", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            if (fi == null)
                return obj;

            obj = (System.Net.WebProxy)fi.GetValue(iproxy);
            return obj;
        } // End Function ConvertWebProxWrapper 


        public static void LogProxy(System.Net.WebProxy proxy, string url)
        {
            Log("Proxy: ");
            if (proxy == null)
            {
                LogLine("is NULL");
                return;
            }
            else
                LogLine(proxy.GetType().AssemblyQualifiedName);


            Log("Address: ");
            if (proxy.Address == null)
                LogLine("NULL");
            else
                LogLine(proxy.Address.OriginalString);

            Log("IsBypassed: ");
            System.Uri uri = new System.Uri(url);
            LogLine(proxy.IsBypassed(uri));

            Log("BypassList: ");
            if (proxy.BypassList == null || proxy.BypassList.Length == 0)
                LogLine("NULL");
            else
                LogLine(string.Join(System.Environment.NewLine, proxy.BypassList));

            Log("BypassProxyOnLocal: ");
            LogLine(proxy.BypassProxyOnLocal);


            Log("UseDefaultCredentials: ");
            LogLine(proxy.UseDefaultCredentials);

            Log("Credentials: ");
            if (proxy.Credentials == null)
                LogLine("NULL");
            else
                LogLine(proxy.Credentials);
        }


        public static string HTTP_Get(string logFile, string url)
        {
            InitLogFile(logFile);
            // System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072; // Force TLS 1.2 // System.Net.SecurityProtocolType.Tls;



            if (string.IsNullOrEmpty(url) || url.Trim() == "")
                throw new System.ArgumentNullException("url is NULL/empty.");

            // Add reference to System.Web
            // string strStringToTranslate = System.Web.HttpUtility.UrlEncode(strInputString);

            // long lngNoCache = DateTime.Now.ToFileTimeUtc();
            // long lngNoCache = System.DateTime.Now.Ticks;

            //string strURL = "http://ajax.googleapis.com/ajax/services/language/detect?v=1.0&q=" + strStringToTranslate;
            //string strURL = "http://localhost:XXX/cgi-bin/GetData.ashx?NoCache=" + lngNoCache.ToString();

            // Establish the request
            // url = "http://localhost:10004/Kamikatze/ajax/Visualiser/foobar.ashx";
            // url = "http://localhost:10004/Kamikatze/DevTool.aspx";
            System.Net.HttpWebRequest wrHTTPrequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

            Log("URL: ");
            LogLine(url);

            Log("Domäne: ");
            LogLine(System.Environment.UserDomainName);

            Log("Verwendeter User: ");
            LogLine(System.Environment.UserName);

            Log("NetBIOS name: ");
            LogLine(System.Environment.MachineName);



            LogLine(System.Environment.NewLine);
            LogLine(System.Environment.NewLine);
            System.Net.WebProxy wp0 = ConvertWebProxy(System.Net.WebProxy.GetDefaultProxy());
            LogLine("System.Net.WebProxy.GetDefaultProxy:");
            LogProxy(wp0, url);


            LogLine(System.Environment.NewLine);
            LogLine(System.Environment.NewLine);
            System.Net.WebProxy wp1 = ConvertWebProxy(System.Net.WebRequest.DefaultWebProxy);
            LogLine("System.Net.WebRequest.DefaultWebProxy:");
            LogProxy(wp1, url);


            LogLine(System.Environment.NewLine);
            LogLine(System.Environment.NewLine);
            // But every HttpWebRequest should automatically be filled out with this information by default.
            System.Net.IWebProxy proxy2 = System.Net.WebRequest.GetSystemWebProxy();
            System.Net.WebProxy wp2 = ConvertWebProxy(proxy2);
            LogLine("System.Net.WebRequest.GetSystemWebProxy:");
            LogProxy(wp2, url);

            LogLine(System.Environment.NewLine);
            LogLine(System.Environment.NewLine);



            LogLine(System.Environment.NewLine);
            LogLine(System.Environment.NewLine);
            // But every HttpWebRequest should automatically be filled out with this information by default.
            System.Net.WebProxy wp3 = ConvertWebProxy(wrHTTPrequest.Proxy);
            LogLine("Von .NET verwendeter Proxy:");
            LogProxy(wp3, url);

            LogLine(System.Environment.NewLine);
            LogLine(System.Environment.NewLine);





            // request.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            // System.Net.WebProxy proxy = System.Net.WebProxy.GetDefaultProxy();
            // LogLine(proxy);
            // proxy.Address.AbsoluteUri

            // proxy.UseDefaultCredentials = true;
            // proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            // wrHTTPrequest.Proxy = proxy;
            

            // Set properties
            wrHTTPrequest.Method = "GET";
            wrHTTPrequest.Method = System.Net.WebRequestMethods.Http.Get;
            wrHTTPrequest.Timeout = 5 * 60 * 1000; // 5 min
            // wrHTTPrequest.UserAgent = "Lord Vishnu/Transcendental (Vaikuntha;Supreme Personality of Godness)";
            wrHTTPrequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; MDDC)";
            // wrHTTPrequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.181 Safari/537.36";
            // wrHTTPrequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko"; // IE 11
            // wrHTTPrequest.UserAgent = "Mozilla/5.0(compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)"; // IE 10
            // wrHTTPrequest.UserAgent = "Mozilla/5.0(compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)"; // IE 9
            // wrHTTPrequest.UserAgent = "Mozilla/4.0(compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)"; // IE8
            // wrHTTPrequest.UserAgent = "Mozilla/4.0(compatible; MSIE 7.0; Windows NT 6.0)"; // IE7
            // wrHTTPrequest.UserAgent = "Mozilla/4.0(compatible; MSIE 6.0; Windows NT 5.1)"; // IE6
            // wrHTTPrequest.Headers.Add("Accept-Language:" + System.Globalization.CultureInfo.CurrentCulture.Name);
            //wrHTTPrequest.ContentType = "text/html";

            // http://www.grauw.nl/blog/entry/489
            //wrHTTPrequest.ContentType = "text/xml";
            wrHTTPrequest.ContentType = "application/xml";


            string strResponse = null;

            try
            {
                // Retrieve request info headers
                System.Net.HttpWebResponse wrHTTPresponse = (System.Net.HttpWebResponse)wrHTTPrequest.GetResponse();

                // My Windows' default code-Page
                // System.Text.Encoding enc = System.Text.Encoding.GetEncoding(1252);
                // System.Text.Encoding enc = System.Text.Encoding.UTF8;
                using (System.IO.StreamReader srResponseStream = new System.IO.StreamReader(wrHTTPresponse.GetResponseStream()
                        , System.Text.Encoding.UTF8)
                    )
                {
                    strResponse = srResponseStream.ReadToEnd();
                    wrHTTPresponse.Close();
                    srResponseStream.Close();
                }
            }
            catch (System.Net.WebException webEx)
            {

                if (webEx.Status == System.Net.WebExceptionStatus.NameResolutionFailure)
                {
                    LogLine("DNS Server Crash.");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.ConnectFailure)
                {
                    //throw new Exception("Cannot connect - WebServer Down / Pipeline error.");
                    LogLine("WebServer Down / ASP.NET Worker-Process Pipeline Crash.");
                } // End if (webEx.Status == System.Net.WebExceptionStatus.ConnectFailure)
                else if (webEx.Status == System.Net.WebExceptionStatus.TrustFailure)
                {
                    LogLine("Invalid SSL/Client certificatwebEx. Certificate could not be validated..");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.SecureChannelFailure)
                {
                    LogLine("SecureChannelFailure - SSL-tunnel couldn't be established.");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.ServerProtocolViolation)
                {
                    LogLine("Web-Server-Malfunction. HTTP 1.1 ? ");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.KeepAliveFailure)
                {
                    LogLine("KeepAliveFailure - KeepAlive connection unexpectedly closed...");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.ReceiveFailure)
                {
                    LogLine("Server shut down in progress or non-HTTP-Service at endpoint port.");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.SendFailure)
                {
                    LogLine("SendFailure - The authentication or decryption has failed.");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.PipelineFailure)
                {
                    LogLine("PipelineFailure - Connection closed before answer was received.");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.RequestCanceled)
                {
                    LogLine("RequestCanceled - Request aborted.");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.ProxyNameResolutionFailure)
                {
                    LogLine("ProxyNameResolutionFailure - Proxy couldn't be resolved..");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.UnknownError)
                {
                    LogLine("UnknownError - An unknown error occured..");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.MessageLengthLimitExceeded)
                {
                    LogLine("MessageLengthLimitExceeded - Received size > allowed sizwebEx.");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.CacheEntryNotFound)
                {
                    LogLine("CacheEntryNotFound - Cache entry not found.");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.RequestProhibitedByCachePolicy)
                {
                    LogLine("RequestProhibitedByCachePolicy - Request couldn't be cached, but re-sending request permission is denied.");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.RequestProhibitedByProxy)
                {
                    LogLine("RequestProhibitedByProxy - Proxy denied request.");
                }
                else if (webEx.Status == System.Net.WebExceptionStatus.ConnectionClosed)
                {
                    LogLine("Connection unexpectedly closed. Firewall ? ");
                } // End  if (webEx.Status == System.Net.WebExceptionStatus.ConnectionClosed)
                else if (webEx.Status == System.Net.WebExceptionStatus.ProtocolError)
                {
                    System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)webEx.Response;
                    
                    Log("HTTP-Fehler ");
                    Log((int)response.StatusCode);
                    Log(": ");
                    LogLine(response.StatusDescription);


                    switch (response.StatusCode)
                    {
                        /*
                        case System.Net.HttpStatusCode.Continue: // 100
                            LogLine("100 - Continue");
                            break;

                        case System.Net.HttpStatusCode.SwitchingProtocols: // 101
                            LogLine("100 - Switching Protocols");
                            break;

                        case System.Net.HttpStatusCode.BadRequest: // 400
                            LogLine("400 - Bad request");
                            break;

                        case System.Net.HttpStatusCode.Unauthorized: // 401
                            LogLine("401 - Unauthorized");
                            break;

                        case System.Net.HttpStatusCode.PaymentRequired: // 402
                            LogLine("402 - Payment required");
                            break;

                        case System.Net.HttpStatusCode.Forbidden: // 403
                            LogLine("403 - Forbidden ");
                            break;

                        case System.Net.HttpStatusCode.NotFound: // 404
                            LogLine("404");
                            break;
                        case System.Net.HttpStatusCode.MethodNotAllowed: // 405
                            LogLine("405 - Method not allowed");
                            break;
                        case System.Net.HttpStatusCode.NotAcceptable: // 406
                            LogLine("406 - Not Acceptable");
                            break;

                        case System.Net.HttpStatusCode.ProxyAuthenticationRequired: // 407
                            LogLine("407 - Proxy authentication required");
                            break;
                            */
                        case System.Net.HttpStatusCode.BadGateway: // 502
                            LogLine(@"HTTP 502: Bad Gateway - Interal-FastCGI-Error. 
Upstream sent unexpected FastCGI record:
Check if [mod-mono-server4, fastcgi-mono-server4, xsp4] in
$PREFIX/lib/mono/{framework-version}      (Default-Prefix: /usr) 

See 
http://stackoverflow.com/questions/4239645/does-the-razor-view-engine-work-for-mono/6317712#6317712
for further information.
");
                            break;
                        case System.Net.HttpStatusCode.ServiceUnavailable: // 503
                            LogLine(@"HTTP 503: Service Unavailable 
Check directory/file access permission. 
Usually 644 is a good permission for files and 711 is for directories. 
If you allow directory listings, then use 755.
To limit the ownership to 755, the webserver user id should own the directory files.
chmod 777 is an extremely bad idea, security-wiswebEx. 
/default ==> chmod 755
/default/default.settings.php ==> chmod 444
/default/files ==> chmod 2775 (or 744 or 755) 
/default/themes ==> chmod 755

chgrp www-data sites/default/files
chmod g+w sites/default/files

See
http://drupal.stackexchangwebEx.com/questions/373/what-are-the-recommended-directory-permissions
for further information.
");
                            break;
                        // case System.Net.HttpStatusCode.GatewayTimeout: // 504
                        // LogLine("Gateway Timeout");
                        // break;
                        case System.Net.HttpStatusCode.InternalServerError: // 500
                            using (System.IO.StreamReader sr = new System.IO.StreamReader(webEx.Response.GetResponseStream()))
                            {
                                string result = sr.ReadToEnd();
                                LogLine(result);

                                if (string.IsNullOrEmpty(result))
                                    break;

                                System.Collections.Generic.List<string> lsCompileErrors = new System.Collections.Generic.List<string>();
                                lsCompileErrors.Add("System.Web.Compilation.CompilationException");
                                lsCompileErrors.Add("CS0006: Metadata file `");
                                lsCompileErrors.Add("System.InvalidOperationException");
                                lsCompileErrors.Add("System.Web.Mvc.ViewResult.FindView");
                                // lsCompileErrors.Add("");


                                bool bCompileError = false;

                                foreach (string strError in lsCompileErrors)
                                {
                                    if (result.IndexOf(strError) != -1)
                                    {
                                        bCompileError = true;
                                        break;
                                    } // End if (result.IndexOf(strError) != -1)
                                } // Next strError

                                if (bCompileError)
                                {
                                    LogLine(@"
Error compiling a resource required to service this request. Review your source file and modify it to fix this error.

xbuild YourSolution.sln
mono --aot -O=all YourSolution/bin/*.dll
killall -9 mono
ps -ef | grep ""9901""
nohup fastcgi-mono-server4 /socket=tcp:127.0.0.1:9901 /applications=/:/var/www/asp/YourSolution/ > /var/log/nginx/YourSolution.log &
");
                                    throw new System.Exception(string.Format("WebPage compile error for url \"{0}\"", url));
                                }


                            } // End Using System.IO.StreamReader sr
                            break;
                        default:
                            break;
                    } // End switch (responswebEx.StatusCode)

                } // End else if (webEx.Status == System.Net.WebExceptionStatus.ProtocolError)

                using (System.IO.StreamReader srResponseStream = new System.IO.StreamReader(webEx.Response.GetResponseStream()
                 , System.Text.Encoding.UTF8)
             )
                {
                    strResponse = srResponseStream.ReadToEnd();
                    webEx.Response.Close();
                    srResponseStream.Close();
                }
                LogLine(System.Environment.NewLine);
                LogLine(System.Environment.NewLine);
                LogLine(strResponse);
                LogLine(System.Environment.NewLine);
                LogLine(System.Environment.NewLine);

                return null;
            }
            catch (System.Exception ex)
            {

                LogLine(System.Environment.NewLine);
                LogLine(System.Environment.NewLine);

                LogLine(ex.Message);
                LogLine(ex.StackTrace);

                LogLine(System.Environment.NewLine);
                LogLine(System.Environment.NewLine);

                return null;
            }

            LogLine(System.Environment.NewLine);
            LogLine(System.Environment.NewLine);
            LogLine("Server-Antwort: ");
            LogLine(strResponse);
            LogLine(System.Environment.NewLine);
            LogLine(System.Environment.NewLine);

            if (string.IsNullOrEmpty(strResponse) || strResponse.Trim() == string.Empty)
                return null;

            // Copy IE proxy settings to WinHttp:
            // Run cmd as administrator:
            // netsh winhttp import proxy source = ie

            return strResponse;
        } // End Function HTTP_Get


    } // End Class cHttpHandler


} // End Namespace TelSearch
