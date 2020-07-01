'---------------------------------------------------------------------------
' This is an alternative method with (HttpWebRequest & HttpWebResponse) for 
' more options For cookies And sessions // INCOMPLETE
'---------------------------------------------------------------------------
Imports System.Net
Imports System.IO
Imports System.Text
Public Class HttpMethods
#Region "Variable Declaration"

    Dim Page_Res, Status As String
    Dim postData As String = "data:" & "{'name':'','job':''}"
    Dim logincookie As CookieContainer
    Dim tempCookies As New CookieContainer
    Dim encoding As New UTF8Encoding
    Dim postresponse As HttpWebResponse

#End Region

#Region "Method Declaration"

    Public Function sendData(ByVal dictData As Dictionary(Of String, Object)) As Boolean

        Try
            Dim byteData As Byte() = encoding.GetBytes(postData)
            Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("https://reqres.in/api/users"), HttpWebRequest)
            postReq.Method = "POST"
            postReq.KeepAlive = True
            postReq.CookieContainer = tempCookies
            postReq.ContentType = "application/x-www-form-urlencoded"
            postReq.Referer = "https://reqres.in/api/users"
            postReq.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            postReq.ContentLength = byteData.Length

            Dim postreqstream As Stream = postReq.GetRequestStream()
            postreqstream.Write(byteData, 0, byteData.Length)
            postreqstream.Close()

            postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
            tempCookies.Add(postresponse.Cookies)
            logincookie = tempCookies
            Dim postreqreader As New StreamReader(postresponse.GetResponseStream())

            ' Display the status.  
            Status = CType(postresponse, HttpWebResponse).StatusDescription
            Console.WriteLine("STATUS : " & Status)
            ' Read the content.  
            Page_Res = postreqreader.ReadToEnd
            Console.WriteLine("RESPONSE : " & Page_Res)
            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function
#End Region
End Class
