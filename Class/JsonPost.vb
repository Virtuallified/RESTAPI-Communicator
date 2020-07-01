'---------------------------------------------------------------------------
' This is an alternative method with (WebClient) // COMPLETE
'---------------------------------------------------------------------------

'Install Newtonsoft.json
'-----------------------
'PM> Install-Package Newtonsoft.Json -Version 6.0.8 & later

'Sample Usage
'------------
'Dim jsonPost As New JsonPost("http://127.0.0.1:3306")
'Dim dictData As New Dictionary(Of String, Object)
'dictData.Add("test_key", "test_value")
'jsonPost.postData(dictData)

Imports Newtonsoft.Json
Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Reflection

Public Class JsonPost

#Region "Variable Declaration"

    Private webClient As New WebClient()
    Private resByte As Byte()
    Private resString, resStringFormatted As String
    Private urlToPost As String = ""
    Private reqString() As Byte

#End Region

#Region "Method Declaration"

    Public Sub New(ByVal urlToPost As String)
        Me.urlToPost = urlToPost
    End Sub

    Public Function postData(ByVal dictData As Dictionary(Of String, Object)) As Object

        Try
            webClient.Headers("content-type") = "application/json"
            reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, Formatting.Indented))
            resByte = webClient.UploadData(Me.urlToPost, "post", reqString)
            resString = Encoding.Default.GetString(resByte)
            resStringFormatted = Linq.JValue.Parse(resString).ToString(Formatting.Indented)
            Console.WriteLine("status : " & GetStatusCode(webClient, ""))
            Console.WriteLine(resStringFormatted)
            webClient.Dispose()
            Return resStringFormatted
        Catch e As WebException
            Console.WriteLine("status : " & e.Status)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function

    Public Function getData()
        Try
            resString = webClient.DownloadString(Me.urlToPost)
            resStringFormatted = Linq.JValue.Parse(resString).ToString(Formatting.Indented)
            Console.WriteLine("status : " & GetStatusCode(webClient, ""))
            Console.WriteLine(resStringFormatted)
            webClient.Dispose()
            Return resStringFormatted
        Catch e As WebException
            Console.WriteLine("status : " & e.Status)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function

    Private Shared Function GetStatusCode(ByVal client As WebClient, <Out> ByRef statusDescription As String) As Integer
        Dim responseField As FieldInfo = client.[GetType]().GetField("m_WebResponse", BindingFlags.Instance Or BindingFlags.NonPublic)

        If responseField IsNot Nothing Then
            Dim response As HttpWebResponse = TryCast(responseField.GetValue(client), HttpWebResponse)

            If response IsNot Nothing Then
                statusDescription = response.StatusDescription
                Return CInt(response.StatusCode)
            End If
        End If

        statusDescription = Nothing
        Return 0
    End Function

#End Region

End Class
