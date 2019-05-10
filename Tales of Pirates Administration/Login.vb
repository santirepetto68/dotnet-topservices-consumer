Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json

Public Class Login
    '186.64.121.175
    Public serviceIp As String = "localhost"
    Public servicePort As String = "8082"

    Public loginUser As String
    Public loginPass As String
    Public apiKey As String = "2018030"
    Public _allowedChars As String = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789-"
    Public specialChars As String = "'= {}!&"



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not TextBox1.Text = "" And Not TextBox2.Text = "" Then


            Dim processData As New LoginStruct With {
                .user = TextBox1.Text,
                .pass = MD5(TextBox2.Text)}
            Dim postData As String = JsonConvert.SerializeObject(processData)
            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)

            Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & serviceIp & ":" & servicePort & "/Login"), HttpWebRequest)
            postReq.Method = "POST"
            postReq.KeepAlive = False
            postReq.ContentType = "application/json"
            postReq.ContentLength = byteData.Length
            postReq.Timeout = 60000
            Try
                Dim postreqstream As Stream = postReq.GetRequestStream()
                postreqstream.Write(byteData, 0, byteData.Length)
                postreqstream.Close()
                Dim postresponse As HttpWebResponse
                postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                Dim sessionID As String = postreqreader.ReadToEnd

                If CInt(postresponse.StatusCode) = 200 Then
                    loginUser = postresponse.Headers.Get("User")
                    loginPass = postresponse.Headers.Get("Pass")
                    postresponse.Close()
                    Panel.Show()

                    Me.Hide()
                ElseIf CInt(postresponse.StatusCode) = 201 Then
                    MsgBox(sessionID)
                    postresponse.Close()
                End If


            Catch ex As Exception
                MsgBox("Failed to connect with server")

            End Try
        Else
            MsgBox("User/Password Blank")
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Not TextBox1.Text = "" And Not TextBox2.Text = "" Then


            Dim processData As New LoginStruct With {
                .user = TextBox1.Text,
                .pass = MD5(TextBox2.Text)}
            Dim postData As String = JsonConvert.SerializeObject(processData)
            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)

            Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & serviceIp & ":" & servicePort & "/Login"), HttpWebRequest)
            postReq.Method = "POST"
            postReq.KeepAlive = False
            postReq.ContentType = "application/json"
            postReq.ContentLength = byteData.Length
            postReq.Timeout = 60000
            Try
                Dim postreqstream As Stream = postReq.GetRequestStream()
                postreqstream.Write(byteData, 0, byteData.Length)
                postreqstream.Close()
                Dim postresponse As HttpWebResponse
                postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                Dim sessionID As String = postreqreader.ReadToEnd

                If CInt(postresponse.StatusCode) = 200 Then
                    loginUser = postresponse.Headers.Get("User")


                    loginPass = postresponse.Headers.Get("Pass")
                    Stall.Show()



                    postresponse.Close()



                    Me.Hide()
                ElseIf CInt(postresponse.StatusCode) = 201 Then
                    MsgBox(sessionID)
                    postresponse.Close()
                End If


            Catch ex As Exception
                MsgBox("Failed to connect with server")

            End Try
        Else
            MsgBox("User/Password Blank")
        End If
    End Sub

    Public Function MD5(ByRef strText As String) As String
        Dim MD5Service As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim Bytes() As Byte = MD5Service.ComputeHash(System.Text.Encoding.ASCII.GetBytes(strText))
        Dim s As String = ""
        For Each By As Byte In Bytes
            s += By.ToString("x2").ToUpper
        Next
        Return s
    End Function

    Public Function Encrypt(ByVal key As String) As String
        Dim keyPass As String = "seecnrsee420"
        Dim keyLength As Integer = key.Length
        Dim passLength As Integer = keyPass.Length
        Dim result As String = ""

        Try
            For i = 1 To keyLength Step +1
                Dim character1 As Integer = Asc(key(i))
                Dim character2 As Integer = Asc(keyPass(i / passLength))
                Dim charNum As Integer = character2 - character1
                Dim character3 As Char = Chr(charNum)
                result += character3
            Next

        Catch ex As Exception

        End Try

        Return result
    End Function

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Register.ShowDialog()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Close()
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        apiKey = Encrypt(apiKey)

    End Sub

#Region " Move Form "

    ' [ Move Form ]
    '
    ' // By Elektro 

    Public MoveForm As Boolean
    Public MoveForm_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)

        If e.Button = MouseButtons.Left Then
            MoveForm = True
            Me.Cursor = Cursors.NoMove2D
            MoveForm_MousePosition = e.Location
        End If

    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)

        If MoveForm Then
            Me.Location = Me.Location + (e.Location - MoveForm_MousePosition)
        End If

    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MoveForm = False
            Me.Cursor = Cursors.Default
        End If

    End Sub




#End Region

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not _allowedChars.Contains(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not _allowedChars.Contains(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub
End Class

Public Class LoginStruct
    Public user As String
    Public pass As String
    Public email As String

End Class