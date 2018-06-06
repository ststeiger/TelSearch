
namespace TelSearch
{


    class NoNProgram
    {


        public static string[] RemoveEmpty(string[] source)
        {
            System.Collections.Generic.List<string> temp = new System.Collections.Generic.List<string>();
            foreach (var s in source)
            {
                if (!string.IsNullOrWhiteSpace(s))
                    temp.Add(s);
            }
            source = temp.ToArray();
            temp.Clear();
            temp = null;

            return source;
        }


        public static string Concat(params string[] p)
        {
            string result = null;
            p = RemoveEmpty(p);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < p.Length; ++i)
            {
                if (i != 0)
                    sb.Append(" ");

                sb.Append(p[i]);
            }

            result = sb.ToString();
            sb.Clear();
            sb = null;

            return result;
        }
        

        public static dynamic[] RemoveEmpty(dynamic[] source)
        {
            System.Collections.Generic.List<dynamic> temp = new System.Collections.Generic.List<dynamic>();

            for (int i = 0; i < source.Length; ++i)
            {
                System.ComponentModel.PropertyDescriptor propertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(source[i])[0];
                string value = (string)propertyDescriptor.GetValue(source[i]);

                if (!string.IsNullOrWhiteSpace(value))
                    temp.Add(source[i]);

            }

            source = temp.ToArray();
            temp.Clear();
            temp = null;

            return source;
        }


        public static string ConcatQuery(string url,  params dynamic[] p)
        {
            string result = null;
            p = RemoveEmpty(p);
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(url);

            for (int i = 0; i < p.Length; ++i)
            {
                //foreach (System.ComponentModel.PropertyDescriptor propertyDescriptor in System.ComponentModel.TypeDescriptor.GetProperties(p[i]))
                //{
                //    System.Console.WriteLine(propertyDescriptor.Name);
                //    string value = (string)propertyDescriptor.GetValue(p[i]);
                //    System.Console.WriteLine(value);
                //}

                if (i != 0)
                    sb.Append("&");
                else
                    sb.Append("?");

                System.ComponentModel.PropertyDescriptor propertyDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(p[i])[0];
                string value = (string)propertyDescriptor.GetValue(p[i]);

                // sb.Append(System.Uri.EscapeDataString(propertyDescriptor.Name));
                sb.Append(System.Web.HttpUtility.UrlEncode(propertyDescriptor.Name));
                sb.Append("=");
                //sb.Append(System.Uri.EscapeDataString(value));
                sb.Append(System.Web.HttpUtility.UrlEncode(value));
            }

            result = sb.ToString();
            sb.Clear();
            sb = null;

            return result;
        }
        

        public static System.Collections.Generic.Dictionary<string, string> RemoveEmpty(System.Collections.Generic.Dictionary<string, string> source)
        {
            System.Collections.Generic.List<string> lsKeys = new System.Collections.Generic.List<string>();
            foreach (string key in source.Keys)
            {
                lsKeys.Add(key);
            }


            foreach(string key in lsKeys)
            {
                if (string.IsNullOrWhiteSpace(source[key]))
                    source.Remove(key);
            }

            lsKeys.Clear();
            lsKeys = null;

            return source;
        }


        public static string ConcatQuery(string url, System.Collections.Generic.Dictionary<string, string> p)
        {
            string result = null;
            p = RemoveEmpty(p);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(url);

            int i = 0;

            foreach (System.Collections.Generic.KeyValuePair<string, string> kvp in p)
            {
                if (i != 0)
                    sb.Append("&");
                else
                    sb.Append("?");

                sb.Append(System.Web.HttpUtility.UrlEncode(kvp.Key));
                sb.Append("=");
                sb.Append(System.Web.HttpUtility.UrlEncode(kvp.Value));

                ++i;
            }

            result = sb.ToString();
            sb.Clear();
            sb = null;

            return result;
        }


        public static System.Collections.Specialized.NameValueCollection RemoveEmpty(System.Collections.Specialized.NameValueCollection source)
        {
            foreach (string key in source.AllKeys)
            {
                if (string.IsNullOrEmpty(source[key]))
                    source.Remove(key);
            }

            return source;
        }


        public static string ConcatQuery(string url, System.Collections.Specialized.NameValueCollection p)
        {
            string result = null;
            p = RemoveEmpty(p);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(url);

            int i = 0;

            foreach (string key in p.AllKeys)
            {
                if (i != 0)
                    sb.Append("&");
                else
                    sb.Append("?");

                sb.Append(System.Web.HttpUtility.UrlEncode(key));
                sb.Append("=");
                sb.Append(System.Web.HttpUtility.UrlEncode(p[key]));

                ++i;
            }

            result = sb.ToString();
            sb.Clear();
            sb = null;

            return result;
        }

        public static string GetSearchUrl(string vorname, string nachname, string strasse, string plz, string ort, string tel)
        {
            string ApiKey = SecretManager.GetSecret<string>("tel.search.ch API Key");

            // Example:
            // http://tel.search.ch/api/?was=DerVorname+DerName+079&wo=dieStrasse+8586+Erlen&maxnum=20&key=API_KEY_HERE 

            return ConcatQuery("http://tel.search.ch/api/",
                   new System.Collections.Specialized.NameValueCollection() {
                        { "was", Concat(vorname, nachname, tel)  }
                      , { "wo", Concat(strasse, plz, ort) }
                       , { "key", ApiKey }
                  }
            );
#if false

            return ConcatQuery("http://tel.search.ch/api/",
                  new System.Collections.Generic.Dictionary<string, string>() {
                        { "was", Concat(vorname, nachname, tel) }
                      , { "wo", Concat(strasse, plz, ort) }
                      , { "key", ApiKey }
                  }
            );

            return ConcatQuery("http://tel.search.ch/api/",
                  new { was = Concat(vorname, nachname, tel) }
                , new { wo = Concat(strasse, plz, ort) }
                , new { key = ApiKey }
            );
#endif
        }


        public static string GetSearchUrl()
        {
            string vorname = "DerVorname";
            string nachname = "DerNachname";
            string strasse = "DieStrasse";
            string plz = "90210";
            string ort = "DerOrt";
            string tel = "079";

            vorname = null;
            nachname = null;
            // tel = null;
            return GetSearchUrl(vorname, nachname, strasse, plz, ort, tel);
        }

        public static string GetSearchUrl(string was, string wo)
        {
            string vorname = was;
            string nachname = null;
            string strasse = null;
            string plz = null;
            string ort = wo;
            string tel = null;

            return GetSearchUrl(vorname, nachname, strasse, plz, ort, tel);
        }


        public static void Query(string was, string wo)
        {
            string dirName = System.IO.Path.GetDirectoryName(typeof(Program).Assembly.Location);
            string time = System.DateTime.Now.ToString("yyyyMMdd_HHmmss_fff", System.Globalization.CultureInfo.InvariantCulture);

            string logFile = System.IO.Path.Combine(dirName, time + "_Logfile.log.txt");
            string resultFile = System.IO.Path.Combine(dirName, time + "_Result.txt.xml");
            System.Console.WriteLine("Eröffne Logfile \"" + logFile + "\".");

            string url = GetSearchUrl(was, wo);
            string xmlText = TelSearch.HttpRequest.HTTP_Get(logFile, url);

            if (xmlText != null)
            {
                cFeed telsearch = Tools.XML.Serialization.DeserializeXmlFromString<cFeed>(xmlText);

                Tools.XML.Serialization.SerializeToXml<cFeed>(telsearch, System.Console.Out);
                Tools.XML.Serialization.SerializeToXml<cFeed>(telsearch, resultFile);

                System.Console.WriteLine(System.Environment.NewLine);
                System.Console.WriteLine(System.Environment.NewLine);
                System.Console.WriteLine(System.Environment.NewLine);
                System.Console.WriteLine("XML-Resultate werdenn nach \"" + resultFile + "\" geschrieben.");
            }
            else
                System.Console.WriteLine("Fehler beim Abruf von tel.search-XML.");
        }


        static void Main(string[] args)
        {
            Query("COR Managementsysteme", "Erlen");

            /*
            System.Threading.Thread.Sleep(500);
            Query("Raiffeisen E-Banking Support", "st. gallen");
            System.Threading.Thread.Sleep(500);
            Query("Raiffeisen", null);
            System.Threading.Thread.Sleep(500);
            Query(null, "Erlen");
            System.Threading.Thread.Sleep(500);
            Query("Lista", "Erlen");
            System.Threading.Thread.Sleep(500);
            Query("Lista", "8586");
            System.Threading.Thread.Sleep(500);
            Query("fasdfasadfasdfasdfsadfasdfasdfasdfasdf<>{}[]()/\\äöü@\"'€$£asdfdsafasdfasdfasdfsadfäöü+?asdfasdfasdfasdfasdfdfdfas", "fasdfasdfadsfasdfasdfasdfasdfasdf'<>asdfasdfasdfsadfadsfasdfsdafsdafasdfasdfasdfasdfdfas");
            */
            
            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Drücken Sie eine beliebige Taste um fortzufahren --- ");
            System.Console.ReadKey();
        }



        public static void NamespaceifySourceFile()
        {

            string foo = @"
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.ServiceModel;


";

            string dir = @"D:\username\Documents\visual studio 2017\Projects\AspNetCore.ReportViewer\AspNetCore.ReportViewer\AspNetCore.Report.ReportService2010_";
            dir = @"D:\username\Documents\visual studio 2017\Projects\AspNetCore.ReportViewer\AspNetCore.ReportViewer\AspNetCore.Report.ReportExecutionService";

            string[] filez = System.IO.Directory.GetFiles(dir);
            foreach (string file in filez)
            {
                string content = System.IO.File.ReadAllText(file);
                content = content.Trim(new char[] { '\r', '\n', ' ', '\t' });
                if (!content.StartsWith("using"))
                {
                    content = foo + content;
                    System.IO.File.WriteAllText(file, content, System.Text.Encoding.UTF8);
                }



            }

        }


        public static void otherStuff()
        {
            // TelSearch.DynamicXml.Test();
            // InvariantTest();

            NginxMonitor.HomePage.TestPages();


            string strText = System.IO.File.ReadAllText("/root/Downloads/dbworlworlddb/myfile3.sql", System.Text.Encoding.UTF8);

            string[] bla = strText.Split(new string[] { "DROP TABLE IF EXISTS" }, System.StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < bla.Length; ++i)
            {
                bla[i] = "DROP TABLE IF EXISTS" + bla[i];
                System.IO.File.WriteAllText(string.Format("/root/Downloads/dbworlworlddb/spitfile/myfile_split{0}.sql", i), bla[i], System.Text.Encoding.UTF8);
            }

            System.Console.WriteLine("Finished");
            return;




            //string 
            strText = System.IO.File.ReadAllText("/root/Downloads/dbworlworlddb/worlddb.sql", System.Text.Encoding.UTF8);
            System.Text.StringBuilder sb = new System.Text.StringBuilder(strText);
            strText = null;
            sb = sb.Replace("`", "");
            sb = sb.Replace("\\'", "''");
            sb = sb.Replace("\\r", "");
            sb = sb.Replace("\\n", "");
            strText = sb.ToString();



            string strPat = @"\)\s*ENGINE\s*=\s*InnoDB.*;";
            strText = System.Text.RegularExpressions.Regex.Replace(strText, strPat, ");", System.Text.RegularExpressions.RegexOptions.IgnoreCase);



            System.IO.File.WriteAllText("/root/Downloads/dbworlworlddb/myfile3.sql", strText, System.Text.Encoding.UTF8);
            strText = null;
            sb.Clear();


            // Not required string strSpecificName = "fu_Constaint_MitarbeitergueltigkeitDarfKostenstellenBelegungsgueltigkeitNichtUnterOderUeberschreiten_T_AP_Kontakte";

            //GetConstraintArguments("");

            NginxMonitor.HomePage.TestPages();
        }


        // http://tel.search.ch/api/?was=john+meier&key=IhrSchluessel
        // cor: 
        // http://tel.search.ch/api/help
        // http://admin.tel.search.ch/api/getkey#terms

        // http://blog.unto.net/add-opensearch-to-your-site-in-five-minutes.html
        static void Test()
        {
            string loc = System.Reflection.Assembly.GetExecutingAssembly().Location;
            loc = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(loc), @"..\..\".Replace('\\', System.IO.Path.DirectorySeparatorChar));
            loc = System.IO.Path.GetFullPath(loc);


            string fn = System.IO.Path.Combine(loc, "JohnMeier.xml");
            //fn = System.IO.Path.Combine(loc, "MeierError.xml");

            //Person pers = new Person();
            //Tools.XML.Serialization.SerializeToXml<Person>(pers, System.Console.Out);
            
            //cFeed telsearch = Tools.XML.Serialization.DeserializeXmlFromFile<cFeed>(fn);
            //string strText = System.IO.File.ReadAllText(fn, System.Text.Encoding.UTF8);

            string ApiKey = SecretManager.GetSecret<string>("tel.search.ch API Key");


            string name = "John Meier";
            
            string strURL = "http://tel.search.ch/api/?was=john+meier&key=API_KEX_HERE";
            strURL = string.Format("http://tel.search.ch/api/?was={0}&key={1}", System.Web.HttpUtility.UrlEncode(name), ApiKey);
            string strText = TelSearch.HttpRequest.HTTP_Get(@"D:\log_telsearch.log.txt", strURL);


            cFeed telsearch = Tools.XML.Serialization.DeserializeXmlFromString<cFeed>(strText);

            Tools.XML.Serialization.SerializeToXml<cFeed>(telsearch, System.Console.Out);
            Tools.XML.Serialization.SerializeToXml<cFeed>(telsearch, @"d:\lol.xml");


            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            System.Console.WriteLine(js.Serialize(telsearch));
            
            //System.Web.Script.Serialization.JavaScriptConverter jcon = new System.Web.Script.Serialization.JavaScriptConverter();
            
            // http://stackoverflow.com/questions/814001/json-net-convert-json-string-to-xml-or-xml-to-json-string
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(strText);
            

            string jsonText = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
            
            // To convert JSON text contained in string json into an XML node
            System.Xml.XmlDocument doc2 = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonText);
        } // End Sub Main 


    } // End Class Program 


} // End Namespace TelSearch 


// http://www.search.ch/jobs/engineer.html
