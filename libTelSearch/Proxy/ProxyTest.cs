
namespace PacProxyUsage
{


    /// <summary>
    /// Summary description for ProxyTest.
    /// </summary>
    public class ProxyTest
    {


        public static void Test()
        {
            System.IO.Stream RequestStream = null;
            string DestinationUrl = "http://www.google.com";
            string PacUri = "http://localhost/Proxy.pac";

            // Create test request 
            System.Net.WebRequest TestRequest = System.Net.WebRequest.Create(DestinationUrl);

            // Optain Proxy address for the URL 
            string ProxyAddresForUrl = Proxy.GetProxyForUrlUsingPac(DestinationUrl, PacUri);
            if (ProxyAddresForUrl != null)
            {
                System.Console.WriteLine("Found Proxy: {0}", ProxyAddresForUrl);
                TestRequest.Proxy = new System.Net.WebProxy(ProxyAddresForUrl);
            }
            else
            {
                System.Console.WriteLine("Proxy Not Found. Send request directly.");
            }

            try
            {
                System.Net.WebResponse TestResponse = TestRequest.GetResponse();
                System.Console.WriteLine("Request was successful");
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error: {0}", ex.Message);
            }
            finally
            {
                if (RequestStream != null)
                {
                    RequestStream.Close();
                }
            }

            System.Console.ReadLine();
        }


    }


}
