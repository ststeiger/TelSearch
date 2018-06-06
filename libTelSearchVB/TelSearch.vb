
Imports System.Collections.Generic
Imports System.Text


Namespace TelSearch


    Public Class cLink

        <System.Xml.Serialization.XmlAttribute("type")> _
        Public Type As String


        <System.Xml.Serialization.XmlAttribute("rel")> _
        Public Rel As String


        <System.Xml.Serialization.XmlAttribute("href")> _
        Public Href As String


        <System.Xml.Serialization.XmlAttribute("title")> _
        Public Title As String
    End Class


    ' <generator version="1.0" uri="http://tel.search.ch">tel.search.ch</generator>
    Public Class cGenerator

        <System.Xml.Serialization.XmlAttribute("version")> _
        Public Version As String

        <System.Xml.Serialization.XmlAttribute("uri")> _
        Public Uri As String

        <System.Xml.Serialization.XmlText()> _
        Public Value As String
    End Class ' cGenerator


    Public Class cAuthor
        <System.Xml.Serialization.XmlElement("name")> _
        Public Name As String
    End Class ' cAuthor


    Public Class cTitle
        <System.Xml.Serialization.XmlAttribute("type")> _
        Public Type As String

        <System.Xml.Serialization.XmlText()> _
        Public value As String
    End Class ' cTitle


    Public Class cContent
        <System.Xml.Serialization.XmlAttribute("type")> _
        Public Type As String

        <System.Xml.Serialization.XmlText()> _
        Public value As String
    End Class ' cContent


    Public Class cEntry
        <System.Xml.Serialization.XmlElement("id")> _
        Public Id As String

        <System.Xml.Serialization.XmlElement("updated")> _
        Public Updated As DateTime

        <System.Xml.Serialization.XmlElement("published")> _
        Public Published As DateTime

        <System.Xml.Serialization.XmlElement("title")> _
        Public Title As cTitle

        <System.Xml.Serialization.XmlElement("content")> _
        Public content As cContent

        <System.Xml.Serialization.XmlElement("author")> _
        Public author As cAuthor

        <System.Xml.Serialization.XmlElement("link")> _
        Public Links As cLink()

        <System.Xml.Serialization.XmlElement("pos", [Namespace]:=cFeed.tel)> _
        Public TelPos As Integer

        <System.Xml.Serialization.XmlElement("id", [Namespace]:=cFeed.tel)> _
        Public TelId As String

        <System.Xml.Serialization.XmlElement("type", [Namespace]:=cFeed.tel)> _
        Public type As String

        <System.Xml.Serialization.XmlElement("name", [Namespace]:=cFeed.tel)> _
        Public name As String

        <System.Xml.Serialization.XmlElement("firstname", [Namespace]:=cFeed.tel)> _
        Public firstname As String

        <System.Xml.Serialization.XmlElement("maidenname", [Namespace]:=cFeed.tel)> _
        Public maidenname As String

        <System.Xml.Serialization.XmlElement("street", [Namespace]:=cFeed.tel)> _
        Public street As String

        <System.Xml.Serialization.XmlElement("streetno", [Namespace]:=cFeed.tel)> _
        Public streetno As String

        <System.Xml.Serialization.XmlElement("zip", [Namespace]:=cFeed.tel)> _
        Public zip As String

        <System.Xml.Serialization.XmlElement("city", [Namespace]:=cFeed.tel)> _
        Public city As String

        <System.Xml.Serialization.XmlElement("canton", [Namespace]:=cFeed.tel)> _
        Public canton As String

        <System.Xml.Serialization.XmlElement("nopromo", [Namespace]:=cFeed.tel)> _
        Public nopromo As String

        <System.Xml.Serialization.XmlElement("phone", [Namespace]:=cFeed.tel)> _
        Public phone As String


        <System.Xml.Serialization.XmlElement("slaveentry", [Namespace]:=cFeed.tel)> _
        Public SlaveEntry As cSlaveEntry


        <System.Xml.Serialization.XmlElement("extra", [Namespace]:=cFeed.tel)> _
        Public Extras As Xtra()

        <System.Xml.Serialization.XmlElement("businesslink", [Namespace]:=cFeed.tel)> _
        Public businesslink As String

        <System.Xml.Serialization.XmlElement("copyright", [Namespace]:=cFeed.tel)> _
        Public copyright As String


    End Class ' cEntry


    Public Class cSlaveEntry
        <System.Xml.Serialization.XmlElement("occupation", [Namespace]:=cFeed.tel)> _
        Public Occupation As String

        <System.Xml.Serialization.XmlElement("nopromo", [Namespace]:=cFeed.tel)> _
        Public NoPromo As String

        <System.Xml.Serialization.XmlElement("extra", [Namespace]:=cFeed.tel)> _
        Public Extras As Xtra()
    End Class ' cSlaveEntry



    Public Class Xtra
        <System.Xml.Serialization.XmlAttribute("type")> _
        Public Type As String

        <System.Xml.Serialization.XmlText()> _
        Public value As String
    End Class ' Xtra



    '<openSearch:Query role="request" searchTerms="john meier " startPage="1" />
    Public Class OpenSearchQuery
        <System.Xml.Serialization.XmlAttribute("role")> _
        Public Role As String

        <System.Xml.Serialization.XmlAttribute("searchTerms")> _
        Public SearchTerms As String

        <System.Xml.Serialization.XmlAttribute("startPage")> _
        Public StartPage As Integer
    End Class ' OpenSearchQuery


    '<openSearch:Image height="1" width="1" type="image/gif">http://www.search.ch/audit/CP/tel/de/api</openSearch:Image>
    Public Class OpenSearchImage
        <System.Xml.Serialization.XmlAttribute("width")> _
        Public Width As Integer

        <System.Xml.Serialization.XmlAttribute("height")> _
        Public Height As Integer

        <System.Xml.Serialization.XmlAttribute("type")> _
        Public Type As String


        <System.Xml.Serialization.XmlText()> _
        Public Url As String
    End Class ' OpenSearchImage


    '[System.Xml.Serialization.XmlRoot(Namespace = "http://example.com/2007/ns1")]
    <System.Xml.Serialization.XmlRoot("feed", [Namespace]:="http://www.w3.org/2005/Atom")> _
    Public Class cFeed
        <System.Xml.Serialization.XmlIgnore()> _
        Public Const openSearch As String = "http://a9.com/-/spec/opensearchrss/1.0/"

        <System.Xml.Serialization.XmlIgnore()> _
        Public Const tel As String = "http://tel.search.ch/api/spec/result/1.0/"


        <System.Xml.Serialization.XmlNamespaceDeclarations()> _
        Public xmlns As New System.Xml.Serialization.XmlSerializerNamespaces()


        Public Sub New()
            xmlns.Add("openSearch", openSearch)
            xmlns.Add("tel", tel)
        End Sub

        <System.Xml.Serialization.XmlElement("id")> _
        Public Id As String

        <System.Xml.Serialization.XmlElement("title")> _
        Public Title As cTitle

        <System.Xml.Serialization.XmlElement("generator")> _
        Public Generator As cGenerator

        <System.Xml.Serialization.XmlElement("updated")> _
        Public Updated As DateTime

        <System.Xml.Serialization.XmlElement("link")> _
        Public Links As cLink()
        ' { get; set; };

        ' <tel:errorCode>403</tel:errorCode>
        <System.Xml.Serialization.XmlElement("errorCode", [Namespace]:=cFeed.tel)> _
        Public ErrorCode As String

        ' <tel:errorReason>Forbidden</tel:errorReason>
        <System.Xml.Serialization.XmlElement("errorReason", [Namespace]:=cFeed.tel)> _
        Public ErrorReason As String

        ' <tel:errorMessage>The submitted API-Key is invalid or blocked</tel:errorMessage>
        <System.Xml.Serialization.XmlElement("errorMessage", [Namespace]:=cFeed.tel)> _
        Public ErrorMessage As String





        ' Start OpenSearch
        <System.Xml.Serialization.XmlElement("totalResults", [Namespace]:=openSearch)> _
        Public totalResults As Integer

        <System.Xml.Serialization.XmlElement("startIndex", [Namespace]:=openSearch)> _
        Public startIndex As Integer

        <System.Xml.Serialization.XmlElement("itemsPerPage", [Namespace]:=openSearch)> _
        Public itemsPerPage As Integer

        <System.Xml.Serialization.XmlElement("Query", [Namespace]:=openSearch)> _
        Public Query As OpenSearchQuery

        <System.Xml.Serialization.XmlElement("Image", [Namespace]:=openSearch)> _
        Public Image As OpenSearchImage
        ' End OpenSearch


        <System.Xml.Serialization.XmlElement("entry")> _
        Public Entries As cEntry()

    End Class ' cFeed


End Namespace ' TelSearch 

