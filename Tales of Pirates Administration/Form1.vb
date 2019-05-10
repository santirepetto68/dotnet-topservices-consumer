Imports System.Text
Imports System.IO
Imports Newtonsoft.Json
Imports System.Net
Imports System.Security.Cryptography

Public Class Form1


    Public charLb As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not TextBox1.Text = "" And Not TextBox2.Text = "" And Not TextBox3.Text = "" And Not ComboBox1.SelectedIndex = -1 Then

            Dim gmLevel As Integer
            If ComboBox1.SelectedItem = "GM" Then
                gmLevel = 99
            ElseIf ComboBox1.SelectedItem = "Normal" Then
                gmLevel = 0
            End If
            Dim processData As New AccCreate With {
                .user = TextBox1.Text,
                .pass = MD5(TextBox2.Text),
                .oldPass = TextBox2.Text,
                .email = TextBox3.Text,
                .gmLevel = gmLevel}
            Dim postData As String = JsonConvert.SerializeObject(processData)
            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)

            Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & muleIp & ":" & mulePort & "/create"), HttpWebRequest)
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
                If CInt(postresponse.StatusCode) = 200 Then


                    Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                    Dim sessionID As String = postreqreader.ReadToEnd
                    MsgBox("Account Created")
                    If sessionID.Length > 1 Then
                        Dim json As AccInfo = JsonConvert.DeserializeObject(Of AccInfo)(sessionID)
                        TextBox5.Text = json.idCode
                        TextBox6.Text = json.name
                        TextBox7.Text = json.password
                        TextBox8.Text = json.originalPassword
                        Dim status As String
                        If json.loginStatus = 0 Then
                            status = "OFF"
                        Else
                            status = "ON"
                        End If
                        TextBox9.Text = status
                        TextBox10.Text = json.lastLoginTime
                        TextBox11.Text = json.lastLogoutTime
                        TextBox12.Text = json.lastLoginIp
                        Dim banned As String
                        If json.ban = 0 Then
                            banned = "Unbanned"
                        Else
                            banned = "Banned"
                        End If
                        TextBox13.Text = banned
                        TextBox14.Text = json.email
                        postresponse.Close()
                    End If
                    postresponse.Close()


                ElseIf CInt(postresponse.StatusCode) = 201 Then
                    MsgBox("Error creating account")
                    postresponse.Close()
                End If


            Catch ex As Exception
                MsgBox("Error creating account")

            End Try
        Else
            MsgBox("Debe poner un Usuario, Contraseña, Email y GM Lvl")
        End If
    End Sub

    Public Function MD5(ByRef strText As String) As String
        Dim MD5Servuce As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim Bytes() As Byte = MD5Servuce.ComputeHash(System.Text.Encoding.ASCII.GetBytes(strText))
        Dim s As String = ""
        For Each By As Byte In Bytes
            s += By.ToString("x2").ToUpper
        Next
        Return s
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not TextBox1.Text = "" And Not TextBox2.Text = "" And Not TextBox4.Text = "" Then

            Dim processData As New Others With {
                .userMod = TextBox4.Text}
            Dim postData As String = JsonConvert.SerializeObject(processData)
            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)

            Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & muleIp & ":" & mulePort & "/getGM"), HttpWebRequest)
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
                If CInt(postresponse.StatusCode) = 200 Then


                    Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                    Dim sessionID As String = postreqreader.ReadToEnd


                    If sessionID.Length > 1 Then
                        Dim json As AccInfo = JsonConvert.DeserializeObject(Of AccInfo)(sessionID)
                        TextBox5.Text = json.idCode
                        TextBox6.Text = json.name
                        TextBox7.Text = json.password
                        TextBox8.Text = json.originalPassword
                        Dim status As String
                        If json.loginStatus = 0 Then
                            status = "OFF"
                        Else
                            status = "ON"
                        End If
                        TextBox9.Text = status
                        TextBox10.Text = json.lastLoginTime
                        TextBox11.Text = json.lastLogoutTime
                        TextBox12.Text = json.lastLoginIp
                        Dim banned As String
                        If json.ban = 0 Then
                            banned = "Unbanned"
                        Else
                            banned = "Banned"
                        End If
                        TextBox13.Text = banned
                        TextBox14.Text = json.email
                        postresponse.Close()
                    End If




                Else
                    MsgBox(CInt(postresponse.StatusCode))
                    postresponse.Close()
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            MsgBox("Debe poner un Usuario a buscar")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim processData As New Others With {
            .charName = TextBox15.Text}
        Dim postData As String = JsonConvert.SerializeObject(processData)
        Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & muleIp & ":" & mulePort & "/getChar"), HttpWebRequest)
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
                If CInt(postresponse.StatusCode) = 200 Then


                    Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                    Dim sessionID As String = postreqreader.ReadToEnd
                    If sessionID.Length > 1 Then
                        Dim json As CharInfo = JsonConvert.DeserializeObject(Of CharInfo)(sessionID)
                        TextBox16.Text = json.coinsValue
                        TextBox17.Text = json.strValue
                        TextBox18.Text = json.dexValue
                        TextBox19.Text = json.agiValue
                        TextBox20.Text = json.conValue
                        TextBox21.Text = json.staValue
                        TextBox22.Text = json.lukValue
                    TextBox23.Text = json.kbCapacity
                    TextBox24.Text = json.repValue

                    charLb = json.idCode
                    Button4.Enabled = True
                        postresponse.Close()
                    End If
                    postresponse.Close()


                ElseIf CInt(postresponse.StatusCode) = 201 Then
                    MsgBox("Error creating account")
                    postresponse.Close()
                End If


            Catch ex As Exception
                MsgBox("Error creating account")

            End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim processData As New CharInfo With {
            .idCode = charLb,
            .coinsValue = TextBox16.Text,
            .strValue = TextBox17.Text,
            .dexValue = TextBox18.Text,
            .agiValue = TextBox19.Text,
            .conValue = TextBox20.Text,
            .staValue = TextBox21.Text,
            .lukValue = TextBox22.Text,
            .kbCapacity = TextBox23.Text,
            .repValue = TextBox24.Text,
            .charName = TextBox15.Text
        }
        Dim postData As String = JsonConvert.SerializeObject(processData)
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & muleIp & ":" & mulePort & "/giveMoney"), HttpWebRequest)
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



                MsgBox(sessionID)
                postresponse.Close()


            ElseIf CInt(postresponse.StatusCode) = 201 Then
                MsgBox(sessionID)
                postresponse.Close()
            End If


        Catch ex As Exception
            MsgBox("Error editing character")

        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox15_TextChanged(sender As Object, e As EventArgs) Handles TextBox15.TextChanged
        Button4.Enabled = False

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Login.Show()
    End Sub
End Class

Public Class CharInfo
    Public idCode As Integer
    Public coinsValue As String
    Public strValue As String
    Public dexValue As String
    Public agiValue As String
    Public conValue As String
    Public staValue As String
    Public lukValue As String
    Public kbCapacity As String
    Public repValue As String
    Public charName As String
End Class

Public Class AccInfo
    Public idCode As Integer
    Public name As String
    Public password As String
    Public originalPassword As String
    Public loginStatus As String
    Public lastLoginTime As String
    Public lastLogoutTime As String
    Public lastLoginIp As String
    Public ban As String
    Public email As String
End Class

Public Class AccCreate
    Public user As String
    Public pass As String
    Public oldPass As String
    Public email As String
    Public gmLevel As Integer
End Class

Public Class Others
    Public userMod As String
    Public charName As String
End Class


