
Class corSettings
    Public Shared Function GetValueByCode(arg As String) As String
        Return "TS_API"
    End Function
End Class



Class Control
    Public Text As String
End Class



Class OMG


    Public ts_Vorname As Control = New Control()
    Public ts_Name As Control = New Control()
    Public ts_telefon As Control = New Control()

    Public ts_strasse As Control = New Control()
    Public ts_plz As Control = New Control()
    Public ts_ort As Control = New Control()



    ''Raiffeisen will das unbedingt wieder ha (16.01.2017)
    Private Sub btn_Search_Click(sender As Object, e As EventArgs)
        Dim webClient As New System.Net.WebClient
        Dim url As String = "http://tel.search.ch/api/?"

        webClient.Encoding = System.Text.Encoding.GetEncoding("UTF-8")

        Dim paramsWas As String = ""
        If Not Me.ts_Vorname.Text = "" Then
            paramsWas &= Me.ts_Vorname.Text & "+"
        End If
        If Not Me.ts_Name.Text = "" Then
            paramsWas &= Me.ts_Name.Text & "+"
        End If
        If Not Me.ts_telefon.Text = "" Then
            Dim tel As String = Me.ts_telefon.Text
            tel = tel.Replace(" ", "")
            tel = tel.Replace("/", "")
            tel = tel.Replace(".", "")
            tel = tel.Replace("(", "")
            tel = tel.Replace(")", "")
            paramsWas &= tel & "+"
        End If
        If Not paramsWas = "" Then
            paramsWas = "was=" & paramsWas.Substring(0, paramsWas.Length - 1)
        End If

        Dim paramsWo As String = ""
        If Not Me.ts_strasse.Text = "" Then
            paramsWo &= Me.ts_strasse.Text & "+"
        End If
        If Not Me.ts_plz.Text = "" Then
            paramsWo &= Me.ts_plz.Text & "+"
        End If
        If Not Me.ts_ort.Text = "" Then
            paramsWo &= Me.ts_ort.Text & "+"
        End If
        If Not paramsWo = "" Then
            paramsWo = "&wo=" & paramsWo.Substring(0, paramsWo.Length - 1)
        End If

        Dim paramsP As String = ""
        Dim paramsF As String = ""

        Dim ApiKey As String = corSettings.GetValueByCode("TS_API")
        Dim maxRes As String = corSettings.GetValueByCode("TS_MaxResult")

        url &= paramsWas & paramsWo & paramsF & paramsP & "&maxnum=" & maxRes & "&key=" & ApiKey

        Dim result As String = webClient.DownloadString(url)

        Dim doc As System.Xml.XmlDocument = New System.Xml.XmlDocument()
        Dim rootNode As System.Xml.XmlElement

        If Not result.Contains("Request limit exceeded") Then

            doc.LoadXml(result)
            rootNode = doc.DocumentElement


            Dim myTable As New System.Data.DataTable
            myTable.Columns.Add("TS_UID", GetType(Guid))
            myTable.Columns.Add("TS_Name", GetType(String))
            myTable.Columns.Add("TS_Vorname", GetType(String))
            myTable.Columns.Add("TS_VornameName", GetType(String))
            myTable.Columns.Add("TS_Strasse", GetType(String))
            myTable.Columns.Add("TS_Plz", GetType(String))
            myTable.Columns.Add("TS_Ort", GetType(String))
            myTable.Columns.Add("TS_Adresse", GetType(String))
            myTable.Columns.Add("TS_Tel", GetType(String))

            If rootNode.HasChildNodes() Then

                Dim name As String = ""
                Dim vorname As String = ""
                Dim fullname As String = ""
                Dim strasse As String = ""
                Dim strassenr As String = ""
                Dim plz As String = ""
                Dim ort As String = ""
                Dim tel As String = ""
                Dim adresse As String = ""

                For Each item As System.Xml.XmlNode In rootNode.GetElementsByTagName("entry")
                    For Each subitem As System.Xml.XmlNode In item.ChildNodes
                        If subitem.Name = "tel:name" Then
                            name = subitem.InnerText
                        End If
                        If subitem.Name = "tel:firstname" Then
                            vorname = subitem.InnerText
                        End If
                        If subitem.Name = "tel:street" Then
                            strasse = subitem.InnerText
                        End If
                        If subitem.Name = "tel:streetno" Then
                            strasse &= " " & subitem.InnerText
                        End If
                        If subitem.Name = "tel:zip" Then
                            plz = subitem.InnerText
                        End If
                        If subitem.Name = "tel:streetno" Then
                            plz = subitem.InnerText
                        End If
                        If subitem.Name = "tel:city" Then
                            ort = subitem.InnerText
                        End If
                        If subitem.Name = "tel:phone" Then
                            tel = subitem.InnerText
                        End If

                        fullname = vorname & " " & name
                        adresse = strasse & "<br>" & plz & " " & ort
                    Next
                    myTable.Rows.Add(System.Guid.NewGuid, name, vorname, fullname, strasse, plz, ort, adresse, tel)
                Next

                'Me.GridView2.PageIndex = 1
                'Me.GridView2.PagerSettings.Mode = PagerButtons.NumericFirstLast

                'Me.GridView2.DataSource = myTable
                'Me.GridView2.DataBind()

                'Me.GridView2.Columns(0).Visible = False

            End If

            'Me.lb_results.Text = Me.lb_results.Text.Replace("@anzResult", myTable.Rows.Count.ToString) ''R.L. (18.03.2014): Funktioniert nur beim ersten Mal, danach gibt es @anzResult nicht mehr.
            'Me.divResults.Visible = True

            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "showpos", "openSearch()", True)
        End If
    End Sub
End Class



Module Program


    Sub Main(args As String())
        Console.WriteLine("Hello World!")
    End Sub


End Module
