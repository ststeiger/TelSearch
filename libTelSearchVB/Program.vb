
Module Program


    ' http://tel.search.ch/api/?was=john+meier&key=IhrSchluessel
    ' cor: 
    ' http://tel.search.ch/api/help
    ' http://admin.tel.search.ch/api/getkey#terms

    ' http://blog.unto.net/add-opensearch-to-your-site-in-five-minutes.html
    Sub Main(args As String())
        Dim loc As String = System.Reflection.Assembly.GetExecutingAssembly().Location
        loc = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(loc), "..\..\")
        loc = System.IO.Path.GetFullPath(loc)

        Dim fn As String = System.IO.Path.Combine(loc, "JohnMeier.xml")
        fn = System.IO.Path.Combine(loc, "MeierError.xml")

        Dim pers As TelSearch.Person = New TelSearch.Person()
        Tools.XML.Serialization.SerializeToXml(Of TelSearch.Person)(pers, System.Console.Out)


        'Dim mytelsearch As TelSearch.cFeed = Tools.XML.Serialization.DeserializeXmlFromFile(Of TelSearch.cFeed)(fn)

        Dim ApiKey As String = SecretManager.GetSecret(Of String)("tel.search.ch API Key")
        Dim name As String = "John Meier"

        Dim strURL As String = "http://tel.search.ch/api/?was=john+meier&key=API_KEY_HERE"
        strURL = String.Format("http://tel.search.ch/api/?was={0}&key={1}", System.Web.HttpUtility.UrlEncode(name), ApiKey)
        Dim strText As String = TelSearch.HttpRequest.HTTP_Get(strURL)
        'Dim strText As String = System.IO.File.ReadAllText(fn, System.Text.Encoding.UTF8)

        Dim mytelsearch As TelSearch.cFeed = Tools.XML.Serialization.DeserializeXmlFromString(Of TelSearch.cFeed)(strText)

        Tools.XML.Serialization.SerializeToXml(Of TelSearch.cFeed)(mytelsearch, System.Console.Out)
        Tools.XML.Serialization.SerializeToXml(Of TelSearch.cFeed)(mytelsearch, "d:\lol.xml")

        ' TrashMain.Main(args)

        Console.WriteLine(Environment.NewLine)
        Console.WriteLine(" --- Press any key to continue --- ")
        Console.ReadKey()
    End Sub


End Module
