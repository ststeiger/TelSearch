
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Diagnostics


Namespace Tools.XML


    ' http://www.switchonthecode.com/tutorials/csharp-tutorial-xml-serialization
    ' http://www.codeproject.com/KB/XML/xml_serializationasp.aspx
    Public Class Serialization


        Public Shared Sub SerializeToXml(Of T)(ThisTypeInstance As T, strFileNameAndPath As String)
            SerializeToXml(Of T)(ThisTypeInstance, New System.IO.StreamWriter(strFileNameAndPath))
        End Sub ' SerializeToXml


        Public Shared Function SerializeToXml(Of T)(ThisTypeInstance As T) As String
            Dim sb As New System.Text.StringBuilder()
            Dim strReturnValue As String = Nothing

            SerializeToXml(Of T)(ThisTypeInstance, New System.IO.StringWriter(sb))

            strReturnValue = sb.ToString()
            sb = Nothing

            Return strReturnValue
        End Function ' SerializeToXml


        Public Shared Sub SerializeToXml(Of T)(ThisTypeInstance As T, tw As System.IO.TextWriter)
            Dim serializer As New System.Xml.Serialization.XmlSerializer(GetType(T))

            'Try
            Using twTextWriter As System.IO.TextWriter = tw
                Dim ns As New System.Xml.Serialization.XmlSerializerNamespaces
                ns.Add("", String.Empty)

                serializer.Serialize(twTextWriter, ThisTypeInstance) ', ns)
                twTextWriter.Close()
            End Using
            'Catch ex As Exception
            '    Using twTextWriter As System.IO.TextWriter = tw
            '        serializer.Serialize(twTextWriter, ThisTypeInstance) ', ns)
            '        twTextWriter.Close()
            '    End Using
            'End Try



            ' End Using twTextWriter
            serializer = Nothing
        End Sub ' SerializeToXml


        Public Shared Function DeserializeXmlFromFile(Of T)(strFileNameAndPath As String) As T
            Dim tReturnValue As T = Nothing

            Using fstrm As New System.IO.FileStream(strFileNameAndPath, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                tReturnValue = DeserializeXmlFromStream(Of T)(fstrm)
                fstrm.Close()
            End Using
            ' End Using fstrm
            Return tReturnValue
        End Function ' DeserializeXmlFromFile


        Public Shared Function DeserializeXmlFromEmbeddedRessource(Of T)(strRessourceName As String) As T
            Dim tReturnValue As T = Nothing

            Dim ass As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()


            Using fstrm As System.IO.Stream = ass.GetManifestResourceStream(strRessourceName)
                tReturnValue = DeserializeXmlFromStream(Of T)(fstrm)
                fstrm.Close()
            End Using
            ' End Using fstrm
            Return tReturnValue
        End Function ' DeserializeXmlFromEmbeddedRessource


        Public Shared Function DeserializeXmlFromString(Of T)(s As String) As T
            Dim tReturnValue As T = Nothing

            Using stream As New System.IO.MemoryStream()
                Using writer As New System.IO.StreamWriter(stream)
                    writer.Write(s)
                    writer.Flush()
                    stream.Position = 0

                    tReturnValue = DeserializeXmlFromStream(Of T)(stream)
                    ' End Using writer
                End Using
            End Using
            ' End Using stream
            Return tReturnValue
        End Function ' DeserializeXmlFromString 


        Public Shared Function DeserializeXmlFromStream(Of T)(strm As System.IO.Stream) As T
            Dim deserializer As New System.Xml.Serialization.XmlSerializer(GetType(T))
            Dim ThisType As T = Nothing

            Using srEncodingReader As New System.IO.StreamReader(strm, System.Text.Encoding.UTF8)
                ThisType = DirectCast(deserializer.Deserialize(srEncodingReader), T)
                srEncodingReader.Close()
            End Using
            ' End Using srEncodingReader
            deserializer = Nothing

            Return ThisType
        End Function ' DeserializeXmlFromStream


#If notneeded Then

		Public Shared Sub SerializeToXML(Of T)(ThisTypeInstance As System.Collections.Generic.List(Of T), strConfigFileNameAndPath As String)
			Dim serializer As New System.Xml.Serialization.XmlSerializer(GetType(System.Collections.Generic.List(Of T)))

			Using textWriter As System.IO.TextWriter = New System.IO.StreamWriter(strConfigFileNameAndPath)
				serializer.Serialize(textWriter, ThisTypeInstance)
				textWriter.Close()
			End Using

			serializer = Nothing
		End Sub ' SerializeToXML


		Public Shared Function DeserializeXmlFromFileAsList(Of T)(strFileNameAndPath As String) As System.Collections.Generic.List(Of T)
			Dim deserializer As New System.Xml.Serialization.XmlSerializer(GetType(System.Collections.Generic.List(Of T)))
			Dim ThisTypeList As System.Collections.Generic.List(Of T) = Nothing

			Using srEncodingReader As New System.IO.StreamReader(strFileNameAndPath, System.Text.Encoding.UTF8)
				ThisTypeList = DirectCast(deserializer.Deserialize(srEncodingReader), System.Collections.Generic.List(Of T))
				srEncodingReader.Close()
			End Using

			deserializer = Nothing

			Return ThisTypeList
		End Function ' DeserializeXmlFromFileAsList

#End If


    End Class ' Serialization


End Namespace ' COR.Tools.XML 
