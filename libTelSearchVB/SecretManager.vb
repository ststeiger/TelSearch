
Option Strict On
Option Explicit On
Option Infer Off
Option Compare Binary
'Option Compare Text


Public Class SecretManager


    Public Shared Function GetSecret(Of T)(ByVal secretName As String) As T
        Dim asmName As String = GetType(SecretManager).Assembly.FullName
        Dim ipos As Integer = asmName.IndexOf(","c)

        If ipos <> -1 Then
            asmName = asmName.Substring(0, ipos)
        End If

        Return GetSecret(Of T)(secretName, asmName)
    End Function


    Public Shared Function GetSecret(Of T)(ByVal secretName As String, ByVal asmName As String) As T
        Dim obj As T = Nothing

        If System.Environment.OSVersion.Platform = System.PlatformID.Unix Then
            obj = SecretManagerHelper.GetEtcKey(Of T)("/etc/COR/" & asmName, secretName)
            If obj Is Nothing Then obj = SecretManagerHelper.GetEtcKey(Of T)("/etc/COR/All", secretName)
        Else
            obj = SecretManagerHelper.GetRegistryKey(Of T)("Software\COR\" & asmName, secretName)
            If obj Is Nothing Then obj = SecretManagerHelper.GetRegistryKey(Of T)("Software\COR\All", secretName)
        End If

        Return obj
    End Function


End Class


Public Class SecretManagerHelper


    Public Shared Function GetRegistryKey(Of T)(ByVal key As String, ByVal value As String) As T
        Dim obj As Object = GetRegistryKey(key, value)
        Return ObjectToGeneric(Of T)(obj)
    End Function


    Public Shared Function GetEtcKey(Of T)(ByVal path As String, ByVal value As String) As T
        Dim obj As String = Nothing
        Dim p As String = System.IO.Path.Combine(path, value)
        If System.IO.File.Exists(p) Then obj = System.IO.File.ReadAllText(p, System.Text.Encoding.Default)
        
        If obj Is Nothing Then Return ObjectToGeneric(Of T)(CObj(obj))
        
        While obj.EndsWith(vbCr) OrElse obj.EndsWith(vbLf)
            obj = obj.Substring(0, obj.Length - 1)
        End While

        Return ObjectToGeneric(Of T)(CObj(obj))
    End Function


    Private Shared Function GetRegistryKey(ByVal key As String, ByVal value As String) As Object
        Dim objReturnValue As Object = Nothing

        Using regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(key)

            If regKey IsNot Nothing Then
                objReturnValue = regKey.GetValue(value)
            End If
        End Using

        Return objReturnValue
    End Function


    Private Shared Function InlineTypeAssignHelper(Of T)(ByVal UTO As Object) As T
        If UTO Is Nothing Then
            Dim NullSubstitute As T = Nothing
            Return NullSubstitute
        End If

        Return CType(UTO, T)
    End Function


    Private Shared Function ObjectToGeneric(Of T)(ByVal objReturnValue As Object) As T
        Dim strReturnValue As String = Nothing
        Dim tReturnType As System.Type = GetType(T)

        If Not Object.ReferenceEquals(tReturnType, GetType(System.Byte())) Then
            If objReturnValue IsNot Nothing Then strReturnValue = System.Convert.ToString(objReturnValue)
        End If

        Try

            If Object.ReferenceEquals(tReturnType, GetType(Object)) Then
                Return InlineTypeAssignHelper(Of T)(objReturnValue)
            ElseIf Object.ReferenceEquals(tReturnType, GetType(String)) Then
                Return InlineTypeAssignHelper(Of T)(strReturnValue)
            ElseIf Object.ReferenceEquals(tReturnType, GetType(Boolean)) Then
                Dim bReturnValue As Boolean = False
                Dim bSuccess As Boolean = Boolean.TryParse(strReturnValue, bReturnValue)
                If bSuccess Then Return InlineTypeAssignHelper(Of T)(bReturnValue)
                If strReturnValue = "0" Then Return InlineTypeAssignHelper(Of T)(False)
                Return InlineTypeAssignHelper(Of T)(True)
            ElseIf Object.ReferenceEquals(tReturnType, GetType(Integer)) Then
                Dim iReturnValue As Integer = Integer.Parse(strReturnValue)
                Return InlineTypeAssignHelper(Of T)(iReturnValue)
            ElseIf Object.ReferenceEquals(tReturnType, GetType(UInteger)) Then
                Dim uiReturnValue As UInteger = UInteger.Parse(strReturnValue)
                Return InlineTypeAssignHelper(Of T)(uiReturnValue)
            ElseIf Object.ReferenceEquals(tReturnType, GetType(Long)) Then
                Dim lngReturnValue As Long = Long.Parse(strReturnValue)
                Return InlineTypeAssignHelper(Of T)(lngReturnValue)
            ElseIf Object.ReferenceEquals(tReturnType, GetType(ULong)) Then
                Dim ulngReturnValue As ULong = ULong.Parse(strReturnValue)
                Return InlineTypeAssignHelper(Of T)(ulngReturnValue)
            ElseIf Object.ReferenceEquals(tReturnType, GetType(Single)) Then
                Dim fltReturnValue As Single = Single.Parse(strReturnValue)
                Return InlineTypeAssignHelper(Of T)(fltReturnValue)
            ElseIf Object.ReferenceEquals(tReturnType, GetType(Double)) Then
                Dim dblReturnValue As Double = Double.Parse(strReturnValue)
                Return InlineTypeAssignHelper(Of T)(dblReturnValue)
            ElseIf Object.ReferenceEquals(tReturnType, GetType(System.Net.IPAddress)) Then
                Dim ipaAddress As System.Net.IPAddress = Nothing
                If String.IsNullOrEmpty(strReturnValue) Then Return InlineTypeAssignHelper(Of T)(ipaAddress)
                ipaAddress = System.Net.IPAddress.Parse(strReturnValue)
                Return InlineTypeAssignHelper(Of T)(ipaAddress)
            ElseIf Object.ReferenceEquals(tReturnType, GetType(System.Byte())) Then
                If objReturnValue Is System.DBNull.Value Then Return InlineTypeAssignHelper(Of T)(Nothing)
                Return InlineTypeAssignHelper(Of T)(objReturnValue)
            ElseIf Object.ReferenceEquals(tReturnType, GetType(System.Guid)) Then
                If String.IsNullOrEmpty(strReturnValue) Then Return InlineTypeAssignHelper(Of T)(Nothing)
                Return InlineTypeAssignHelper(Of T)(New System.Guid(strReturnValue))
            ElseIf Object.ReferenceEquals(tReturnType, GetType(System.DateTime)) Then
                Dim bReturnValue As System.DateTime = System.DateTime.Now
                Dim bSuccess As Boolean = System.DateTime.TryParse(strReturnValue, bReturnValue)
                If bSuccess Then Return InlineTypeAssignHelper(Of T)(bReturnValue)
                If strReturnValue = "0" Then Return InlineTypeAssignHelper(Of T)(False)
                Return InlineTypeAssignHelper(Of T)(True)
            Else
                Throw New System.NotImplementedException("ExecuteScalar<T>: This type is not yet defined.")
            End If

        Catch ex As System.Exception
            Throw
        End Try

        Return InlineTypeAssignHelper(Of T)(Nothing)
    End Function


End Class
