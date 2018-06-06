
Imports System.Collections.Generic
Imports System.Text



Namespace TelSearch



    <System.Xml.Serialization.XmlRoot(ElementName:="person")> _
    Public Class Person

        <System.Xml.Serialization.XmlElement([Namespace]:="http://example.com")> _
        Public fname As String = "myfname"

        <System.Xml.Serialization.XmlElement([Namespace]:="http://sample.com")> _
        Public lname As String = "mylname"

        <System.Xml.Serialization.XmlNamespaceDeclarations()> _
        Public xmlns As New System.Xml.Serialization.XmlSerializerNamespaces()

        Public Sub New()
            xmlns.Add("a", "http://example.com")
            xmlns.Add("b", "http://sample.com")
        End Sub
    End Class




End Namespace

