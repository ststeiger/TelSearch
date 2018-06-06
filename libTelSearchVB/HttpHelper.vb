
Imports System.Collections.Generic
Imports System.Text


Namespace TelSearch


    Public Class HttpRequest


        Public Shared Function HTTP_Post(strInputString As String) As String
            If String.IsNullOrEmpty(strInputString) OrElse strInputString.Trim() = "" Then
                Throw New ArgumentNullException("strInputString is NULL/empty.")
            End If

            ' Add reference to System.Web
            'string strStringToTranslate = System.Web.HttpUtility.UrlEncode(strInputString);
            Dim RequestURI As New Uri(strInputString, UriKind.Absolute)
            Dim strURL As String = RequestURI.AbsoluteUri

            Dim iQuestionMarkPos As Integer = strURL.IndexOf("?"c)
            If iQuestionMarkPos <> -1 Then
                strURL = strURL.Substring(0, iQuestionMarkPos)
            End If

            Dim strQueryString As String = RequestURI.Query

            If strQueryString IsNot Nothing AndAlso strQueryString.StartsWith("?") AndAlso strQueryString.Length > 1 Then
                strQueryString = strQueryString.Substring(1)
            End If

            Dim lngNoCache As Long = DateTime.Now.ToFileTimeUtc()

            'string strURL = "http://ajax.googleapis.com/ajax/services/language/detect?v=1.0&q=" + strStringToTranslate;
            'string strURL = "http://localhost:XXX/cgi-bin/GetData.ashx?NoCache=" + lngNoCache.ToString();


            ' *** Establish the request
            Dim wrHTTPrequest As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(strURL), System.Net.HttpWebRequest)


            'System.Web.HttpRequest.HttpMethod.
            ' *** Set properties
            'wrHTTPrequest.Method = "POST";
            wrHTTPrequest.Method = System.Net.WebRequestMethods.Http.Post

            'wrHTTPrequest.Timeout = 10000; // 10 secs
            wrHTTPrequest.Timeout = 5 * 60 * 1000
            ' 5 min
            wrHTTPrequest.UserAgent = "Lord Vishnu/Transcendental (Vaikuntha;Supreme Personality of Godness)"
            'wrHTTPrequest.UserAgent = ".NET VeloConnect Client 1.0";
            'wrHTTPrequest.Headers.Add("Accept-Language:" + System.Globalization.CultureInfo.CurrentCulture.Name);
            wrHTTPrequest.Headers.Add("Accept-Language", System.Globalization.CultureInfo.CurrentCulture.Name)
            'wrHTTPrequest.ContentType = "text/html";
            'wrHTTPrequest.ContentType = "text/xml";
            wrHTTPrequest.ContentType = "application/x-www-form-urlencoded"
            'wrHTTPrequest.ContentType = "application/x-www-form-urlencoded";
            ' xhReq.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
            'xhReq.setRequestHeader("Content-length", params.length);
            'xhReq.setRequestHeader("Connection", "close");
            wrHTTPrequest.Connection = "close"

            Dim enc As System.Text.Encoding = System.Text.Encoding.UTF8


            ' http://stackoverflow.com/questions/4088625/net-simplest-way-to-send-post-with-data-and-read-response
            Dim bytes As Byte() = enc.GetBytes(strQueryString)
            wrHTTPrequest.ContentLength = bytes.Length
            Dim rs As System.IO.Stream = wrHTTPrequest.GetRequestStream()
            rs.Write(bytes, 0, bytes.Length)
            'Push it out there
            rs.Close()


            'wrHTTPrequest.GetRequestStream().Write(new byte[100], 0, 100)
            'wrHTTPrequest.ContentLength = wrHTTPrequest.GetRequestStream().Length;

            ' *** Retrieve request info headers
            Dim wrHTTPresponse As System.Net.HttpWebResponse = DirectCast(wrHTTPrequest.GetResponse(), System.Net.HttpWebResponse)

            ' My Windows' default code-Page
            'System.Text.Encoding enc = System.Text.Encoding.GetEncoding(1252);

            ' Google's code-page


            Dim srResponseStream As New System.IO.StreamReader(wrHTTPresponse.GetResponseStream(), enc)

            Dim strResponse As String = srResponseStream.ReadToEnd()
            wrHTTPresponse.Close()
            srResponseStream.Close()


            'MsgBox(strResponse)
            If String.IsNullOrEmpty(strResponse) Then
                Return Nothing
            End If
            ' null;
            Return strResponse
        End Function ' HTTP_Post



        Public Shared Function HTTP_Get(strInputString As String) As String
            If String.IsNullOrEmpty(strInputString) OrElse strInputString.Trim() = "" Then
                Throw New ArgumentNullException("strInputString is NULL/empty.")
            End If

            ' Add reference to System.Web
            'string strStringToTranslate = System.Web.HttpUtility.UrlEncode(strInputString);

            'long lngNoCache = DateTime.Now.ToFileTimeUtc();
            Dim lngNoCache As Long = DateTime.Now.Ticks

            'string strURL = "http://ajax.googleapis.com/ajax/services/language/detect?v=1.0&q=" + strStringToTranslate;
            'string strURL = "http://localhost:XXX/cgi-bin/GetData.ashx?NoCache=" + lngNoCache.ToString();
            Dim strURL As String = strInputString

            ' *** Establish the request
            Dim wrHTTPrequest As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create(strURL), System.Net.HttpWebRequest)


            ' *** Set properties
            wrHTTPrequest.Method = "GET"
            wrHTTPrequest.Method = System.Net.WebRequestMethods.Http.[Get]
            wrHTTPrequest.Timeout = 5 * 60 * 1000
            ' 5 min
            wrHTTPrequest.UserAgent = "Lord Vishnu/Transcendental (Vaikuntha;Supreme Personality of Godness)"
            'wrHTTPrequest.UserAgent = ".NET VeloConnect Client 1.0";
            wrHTTPrequest.Headers.Add("Accept-Language:" + System.Globalization.CultureInfo.CurrentCulture.Name)
            'wrHTTPrequest.ContentType = "text/html";

            ' http://www.grauw.nl/blog/entry/489
            'wrHTTPrequest.ContentType = "text/xml";
            wrHTTPrequest.ContentType = "application/xml"



            ' *** Retrieve request info headers
            Dim wrHTTPresponse As System.Net.HttpWebResponse = DirectCast(wrHTTPrequest.GetResponse(), System.Net.HttpWebResponse)

            ' My Windows' default code-Page
            'System.Text.Encoding enc = System.Text.Encoding.GetEncoding(1252);

            ' Google's code-page
            Dim enc As System.Text.Encoding = System.Text.Encoding.UTF8

            Dim srResponseStream As New System.IO.StreamReader(wrHTTPresponse.GetResponseStream(), enc)

            Dim strResponse As String = srResponseStream.ReadToEnd()
            wrHTTPresponse.Close()
            srResponseStream.Close()

            'MsgBox(strResponse)

            If String.IsNullOrEmpty(strResponse) Then
                Return Nothing
            End If

            Return strResponse
        End Function ' HTTP_Get


    End Class ' cHttpHandler


End Namespace ' TelSearch
