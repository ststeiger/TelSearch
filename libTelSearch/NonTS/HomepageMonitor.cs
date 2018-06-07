
// http://scienceblogs.com/gregladen/2014/04/24/10-or-20-things-to-do-after-installing-ubuntu-14-04-trusty-tahr/
namespace NginxMonitor
{

    // https://github.com/Hihaj/HealthCheck
    // https://github.com/wongatech/HealthMonitoring
    public class HomePage
    {


        public static void TestPages()
        {
            string url = null;
            try
            {
                System.Collections.Generic.List<string> ls = new System.Collections.Generic.List<string>();
                
                ls.Add("http://www.daniel-steiger.ch/Gallery");
                ls.Add("http://www.daniel-steiger.ch/gallery");
                
                ls.Add("http://www.daniel-steiger.ch/Contact");
                ls.Add("http://www.daniel-steiger.ch/contact");


                ls.Add("http://www.daniel-steiger.ch/Overview");
                ls.Add("http://www.daniel-steiger.ch/overview");

                ls.Add("http://www.daniel-steiger.ch/Profile");
                ls.Add("http://www.daniel-steiger.ch/profile");
                
                ls.Add("http://www.daniel-steiger.ch/Videos");
                ls.Add("http://www.daniel-steiger.ch/videos");
                
                for (int i = 0; i < ls.Count; ++i)
                {
                    url = ls[i];
                    string myHTML = TelSearch.HttpRequest.HTTP_Get(@"d:\log_TelSearch.log.txt", url);
                    System.Console.WriteLine(myHTML);
                } // Next i

            } // End Try
            catch (System.Net.WebException e)
            {
                // System.Net.WebExceptionStatus:

                // Zusammenfassung: Es ist kein Fehler aufgetreten.
                // Success = 0,

                // Zusammenfassung: Der Namensauflösungsdienst konnte den Hostnamen nicht auflösen.
                // NameResolutionFailure = 1, OK

                // Zusammenfassung: Auf der Transportebene konnte keine Verbindung mit dem remoten Dienstpunkt hergestellt werden.
                // ConnectFailure = 2, OK

                // Zusammenfassung: Es wurde keine vollständige Antwort vom Remoteserver empfangen.
                // ReceiveFailure = 3, OK

                // Zusammenfassung: Es konnte keine vollständige Anforderung an den Remoteserver gesendet werden.
                // SendFailure = 4, OK

                // Zusammenfassung:
                //     Die Anforderung sollte über eine Pipeline gesendet werden, und die Verbindung
                //     wurde geschlossen, bevor die Antwort empfangen wurde.
                // PipelineFailure = 5, OK 

                // Zusammenfassung:
                //     Die Anforderung wurde abgebrochen. Es wurde die System.Net.WebRequest.Abort()-Methode
                //     aufgerufen, oder ein nicht klassifizierbarer Fehler ist aufgetreten.Dies
                //     ist der Standardwert für System.Net.WebException.Status.
                // RequestCanceled = 6, OK 

                // Zusammenfassung:
                //     Die vom Server empfangene Antwort war vollständig, deutete jedoch auf einen
                //     Fehler auf Protokollebene hin.In einem HTTP-Protokollfehler, wie 401 Zugriff
                //     verweigert, wird z. B. dieser Status verwendet.
                // ProtocolError = 7, OK 

                // Zusammenfassung: Die Verbindung wurde vorzeitig getrennt.
                // ConnectionClosed = 8, OK 

                // Zusammenfassung: Ein Serverzertifikat konnte nicht überprüft werden.
                // TrustFailure = 9, OK

                // Zusammenfassung: Beim Einrichten einer Verbindung über SSL ist ein Fehler aufgetreten.
                // SecureChannelFailure = 10, OK 

                // Zusammenfassung: Die Antwort vom Server war keine gültige HTTP-Antwort.
                // ServerProtocolViolation = 11, OK 

                // Zusammenfassung:
                //     Die Verbindung für eine Anforderung, die angibt, dass der Keep-Alive-Header
                //     unerwartet geschlossen wurde.
                // KeepAliveFailure = 12, OK 

                // Zusammenfassung: Eine interne asynchrone Anforderung steht aus.
                // Pending = 13,

                // Zusammenfassung: Währen des Zeitlimits für eine Anforderung wurde keine Antwort empfangen.
                // Timeout = 14, OK 

                // Zusammenfassung: Der Namensauflösungsdienst konnte den Proxyhostnamen nicht auflösen.
                // ProxyNameResolutionFailure = 15, OK 

                // Zusammenfassung: Eine Ausnahme unbekannten Typs ist aufgetreten.
                // UnknownError = 16, OK 

                // Zusammenfassung:
                //     Es wurde eine Meldung empfangen, bei der die festgelegte Größe für das Senden
                //     einer Anforderung bzw. das Empfangen einer Antwort vom Server überschritten
                //     wurde.
                // MessageLengthLimitExceeded = 17, OK 

                // Zusammenfassung: Der angegebene Cacheeintrag wurde nicht gefunden.
                // CacheEntryNotFound = 18, OK 

                // Zusammenfassung:
                //     Die Anforderung wurde durch die Cacherichtlinie nicht zugelassen.Im Allgemeinen
                //     geschieht dies, wenn eine Anforderung nicht zwischengespeichert werden kann
                //     und das Senden der Anforderung an den Server durch die angewendete Richtlinie
                //     untersagt ist.Möglicherweise erhalten Sie diesen Status, wenn eine Anforderungsmethode
                //     einen Anforderungstext erfordert, wenn eine Anforderungsmethode eine direkte
                //     Interaktion mit dem Server erfordert oder wenn eine Anforderung einen bedingten
                //     Header enthält.
                // RequestProhibitedByCachePolicy = 19, OK 

                // Zusammenfassung: Diese Anforderung wurde nicht durch den Proxy zugelassen.
                // RequestProhibitedByProxy = 20, OK 


                // http://stackoverflow.com/questions/13338894/webclient-ignore-http-500


                // System.Diagnostics.Debug.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());


                if (e.Status == System.Net.WebExceptionStatus.NameResolutionFailure)
                {
                    System.Diagnostics.Debug.WriteLine("DNS Server Crash.");
                }
                else if (e.Status == System.Net.WebExceptionStatus.ConnectFailure)
                {
                    //throw new Exception("Cannot connect - WebServer Down / Pipeline error.");
                    System.Diagnostics.Debug.WriteLine("WebServer Down / ASP.NET Worker-Process Pipeline Crash.");
                } // End if (e.Status == System.Net.WebExceptionStatus.ConnectFailure)
                else if (e.Status == System.Net.WebExceptionStatus.TrustFailure)
                {
                    System.Diagnostics.Debug.WriteLine("Invalid SSL/Client certificate. Certificate could not be validated..");
                }
                else if (e.Status == System.Net.WebExceptionStatus.SecureChannelFailure)
                {
                    System.Diagnostics.Debug.WriteLine("SecureChannelFailure - SSL-tunnel couldn't be established.");
                }
                else if (e.Status == System.Net.WebExceptionStatus.ServerProtocolViolation)
                {
                    System.Diagnostics.Debug.WriteLine("Web-Server-Malfunction. HTTP 1.1 ? ");
                }
                else if (e.Status == System.Net.WebExceptionStatus.KeepAliveFailure)
                {
                    System.Diagnostics.Debug.WriteLine("KeepAliveFailure - KeepAlive connection unexpectedly closed...");
                }
                else if (e.Status == System.Net.WebExceptionStatus.ReceiveFailure)
                {
                    System.Diagnostics.Debug.WriteLine("Server shut down in progress or non-HTTP-Service at endpoint port.");
                }
                else if (e.Status == System.Net.WebExceptionStatus.SendFailure)
                {
                    System.Diagnostics.Debug.WriteLine("SendFailure - The authentication or decryption has failed.");
                }
                else if (e.Status == System.Net.WebExceptionStatus.PipelineFailure)
                {
                    System.Diagnostics.Debug.WriteLine("PipelineFailure - Connection closed before answer was received.");
                }
                else if (e.Status == System.Net.WebExceptionStatus.RequestCanceled)
                {
                    System.Diagnostics.Debug.WriteLine("RequestCanceled - Request aborted.");
                }
                else if (e.Status == System.Net.WebExceptionStatus.ProxyNameResolutionFailure)
                {
                    System.Diagnostics.Debug.WriteLine("ProxyNameResolutionFailure - Proxy couldn't be resolved..");
                }
                else if (e.Status == System.Net.WebExceptionStatus.UnknownError)
                {
                    System.Diagnostics.Debug.WriteLine("UnknownError - An unknown error occured..");
                }
                else if (e.Status == System.Net.WebExceptionStatus.MessageLengthLimitExceeded)
                {
                    System.Diagnostics.Debug.WriteLine("MessageLengthLimitExceeded - Received size > allowed size.");
                }
                else if (e.Status == System.Net.WebExceptionStatus.CacheEntryNotFound)
                {
                    System.Diagnostics.Debug.WriteLine("CacheEntryNotFound - Cache entry not found.");
                }
                else if (e.Status == System.Net.WebExceptionStatus.RequestProhibitedByCachePolicy)
                {
                    System.Diagnostics.Debug.WriteLine("RequestProhibitedByCachePolicy - Request couldn't be cached, but re-sending request permission is denied.");
                }
                else if (e.Status == System.Net.WebExceptionStatus.RequestProhibitedByProxy)
                {
                    System.Diagnostics.Debug.WriteLine("RequestProhibitedByProxy - Proxy denied request.");
                }
                else if (e.Status == System.Net.WebExceptionStatus.ConnectionClosed)
                {
                    System.Diagnostics.Debug.WriteLine("Connection unexpectedly closed. Firewall ? ");
                } // End  if (e.Status == System.Net.WebExceptionStatus.ConnectionClosed)
                else if (e.Status == System.Net.WebExceptionStatus.ProtocolError)
                {
                    System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)e.Response;

                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.NotFound: // 404
                            System.Diagnostics.Debug.WriteLine("404");
                            break;
                        case System.Net.HttpStatusCode.BadGateway: // 502
                            System.Diagnostics.Debug.WriteLine(@"Bad Gateway - Interal-FastCGI-Error. 
Upstream sent unexpected FastCGI record:
Check if [mod-mono-server4, fastcgi-mono-server4, xsp4] in
$PREFIX/lib/mono/{framework-version}      (Default-Prefix: /usr) 

See 
http://stackoverflow.com/questions/4239645/does-the-razor-view-engine-work-for-mono/6317712#6317712
for further information.
");
                            break;
                        case System.Net.HttpStatusCode.ServiceUnavailable: // 503
                            System.Diagnostics.Debug.WriteLine(@"Check directory/file access permission. 
Usually 644 is a good permission for files and 711 is for directories. 
If you allow directory listings, then use 755.
To limit the ownership to 755, the webserver user id should own the directory files.
chmod 777 is an extremely bad idea, security-wise. 
/default ==> chmod 755
/default/default.settings.php ==> chmod 444
/default/files ==> chmod 2775 (or 744 or 755) 
/default/themes ==> chmod 755

chgrp www-data sites/default/files
chmod g+w sites/default/files

See
http://drupal.stackexchange.com/questions/373/what-are-the-recommended-directory-permissions
for further information.
");
                            break;
                        case System.Net.HttpStatusCode.GatewayTimeout: // 504
                            System.Diagnostics.Debug.WriteLine("Mono FastCGI-Crashed.");
                            //System.Diagnostics.Process.Start("/etc/init.d/monoserve.sh", "-start");
                            break;
                        case System.Net.HttpStatusCode.InternalServerError: // 500
                            using (System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
                            {
                                string result = sr.ReadToEnd();
                                System.Console.WriteLine(result);

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
                                    System.Diagnostics.Debug.WriteLine(@"
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
                            throw;
                    } // End switch (response.StatusCode)

                } // End else if (e.Status == System.Net.WebExceptionStatus.ProtocolError)

            } // End Catch

        } // End Sub TestWebPage


    } // End Class Program 


} // End Namespace TelSearch 
