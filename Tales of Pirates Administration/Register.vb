Imports System.IO
Imports System.Net
Imports System.Text

Public Class Register
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox2.Text = TextBox4.Text Then
            Dim processData As New LoginStruct With {
                .user = TextBox1.Text,
                .pass = Login.MD5(TextBox2.Text),
                .email = TextBox3.Text}
            Dim postData As String = Newtonsoft.Json.JsonConvert.SerializeObject(processData)
            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)

            Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/createAccount"), HttpWebRequest)
            postReq.Method = "POST"
            postReq.KeepAlive = False
            postReq.ContentType = "application/json"
            postReq.ContentLength = byteData.Length
            postReq.Timeout = 60000
            postReq.Headers.Add("User", Login.loginUser)
            postReq.Headers.Add("Pass", Login.loginPass)
            Try
                Dim postreqstream As Stream = postReq.GetRequestStream()
                postreqstream.Write(byteData, 0, byteData.Length)
                postreqstream.Close()
                Dim postresponse As HttpWebResponse
                postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                Dim sessionID As String = postreqreader.ReadToEnd
                If CInt(postresponse.StatusCode) = 200 Then

                    postresponse.Close()

                    MsgBox(sessionID)
                    Login.TextBox1.Text = TextBox1.Text
                    Login.TextBox2.Text = TextBox2.Text
                    Me.Close()

                Else
                    MsgBox(sessionID)
                    postresponse.Close()
                End If


            Catch ex As Exception
                MsgBox("Failed to connect with server")

            End Try
        Else
            MsgBox("Passwords dont match")
        End If

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not Login._allowedChars.Contains(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not Login._allowedChars.Contains(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub


    Private _allowedEmail As String = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789-_@."
    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not _allowedEmail.Contains(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub




    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not Login._allowedChars.Contains(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub
End Class