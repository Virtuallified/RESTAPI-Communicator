Public Class TestAPI

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim jsonPost As New JsonPost("https://reqres.in/api/users")
        Dim dictData As New Dictionary(Of String, Object)
        dictData.Add("mobile", TextBox1.Text)
        dictData.Add("message", TextBox2.Text)
        WebBrowser1.DocumentText = "<pre>" & jsonPost.postData(dictData) & "</pre>"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim jsonPost As New JsonPost("https://reqres.in/api/unknown/2")
        WebBrowser1.DocumentText = "<pre>" & jsonPost.getData() & "</pre>"
    End Sub
End Class
