Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json


Public Class Panel

    Public charVar As List(Of List(Of CharacterInfo))
    Public clubVar As List(Of PirateClub)
    Public clubApplyVar As List(Of PirateClub)
    Public charClubVar As List(Of PirateClub)
    Public charOwnVar As List(Of PirateClub)
    Public charName As String
    Public charId As Integer

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

    Private Sub Panel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getChar()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ComboBox1.Items.Clear()
        getChar()
        ComboBox1.Text = ""
        Label8.Text = ""
        Label9.Text = ""
        Label10.Text = ""
        Label11.Text = ""
        Label12.Text = ""
        Label13.Text = ""
        Label20.Text = ""
        Label21.Text = ""
        Label22.Text = ""
        Label23.Text = ""
        Label24.Text = ""
        ResetPirateEnable()


    End Sub

    Function ResetPirateEnable()
        ComboBox2.Enabled = False
        ComboBox2.Items.Clear()
        ComboBox2.Text = ""
        ComboBox3.Enabled = False
        ComboBox3.Items.Clear()
        ComboBox3.Text = ""
        ComboBox4.Enabled = False
        ComboBox4.Items.Clear()
        ComboBox4.Text = ""

        Button3.Enabled = False
        Button4.Enabled = False
        Button5.Enabled = False
        Button6.Enabled = False
        Button7.Enabled = False
        Button8.Enabled = False
        Button10.Enabled = False
        Button11.Enabled = False
        Button12.Enabled = False
        Button13.Enabled = False
        Button14.Enabled = False
        Button15.Enabled = False
        Button16.Enabled = False
        Button17.Enabled = False
        Button18.Enabled = False
        Button19.Enabled = False
        Button20.Enabled = False
        Button21.Enabled = False
        Button22.Enabled = False
        Button23.Enabled = False

        Label25.Enabled = False
        Label26.Enabled = False
        Label27.Enabled = False
        Label28.Enabled = False
        Label29.Enabled = False
        Label30.Enabled = False
        Label31.Enabled = False
        Label32.Enabled = False
        Label32.Text = ""
        Label33.Enabled = False
        Label33.Text = ""
        Label34.Enabled = False
        Label34.Text = ""
        Label36.Enabled = False
        Label37.Enabled = False
        Label38.Enabled = False
        Label39.Enabled = False
        Label40.Enabled = False
        Label40.Text = ""
        Label41.Enabled = False
        Label41.Text = ""
        Label42.Enabled = False
        Label42.Text = ""
        Label43.Enabled = False
        Label44.Enabled = False
        Label45.Enabled = False
        Label46.Enabled = False
        Label47.Enabled = False
        Label48.Enabled = False
        Label49.Enabled = False
        Label50.Enabled = False
        Label51.Enabled = False
        Label52.Enabled = False
        Label53.Enabled = False
        Label54.Enabled = False
        Label55.Enabled = False
        Label56.Enabled = False
        Label57.Enabled = False
        Label58.Enabled = False
        Label59.Enabled = False
        Label60.Enabled = False
        Label61.Enabled = False
        Label62.Enabled = False
        Label63.Enabled = False
        Label64.Text = ""
        Label64.Enabled = False
        Label65.Text = ""
        Label65.Enabled = False
        Label66.Text = ""
        Label66.Enabled = False
        Label67.Text = ""
        Label67.Enabled = False
        Label68.Text = ""
        Label68.Enabled = False
        Label69.Text = ""
        Label69.Enabled = False

        TextBox1.Text = ""
        TextBox1.Enabled = False
        TextBox2.Text = ""
        TextBox2.Enabled = False
        TextBox3.Text = ""
        TextBox3.Enabled = False



        Return True

    End Function

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ResetPirateEnable()
        Me.Text = Me.Text & " - " & ComboBox1.SelectedItem
        Dim character As List(Of CharacterInfo)
        For Each data As List(Of CharacterInfo) In charVar
            character = data.FindAll(AddressOf FindChar)
            If character IsNot Nothing Then
                For Each data2 As CharacterInfo In character

                    Label8.Text = data2.job
                    Label9.Text = data2.degree
                    Label10.Text = data2.gd
                    Label11.Text = data2.map
                    Label12.Text = data2.kb_capacity
                    Label13.Text = data2.credit
                    Label20.Text = data2.str
                    Label21.Text = data2.con
                    Label22.Text = data2.agi
                    Label23.Text = data2.dex
                    Label24.Text = data2.sta
                    Button9.Enabled = True
                Next
            End If
        Next

    End Sub

    Function getClubs()
        Dim response As Boolean = False
        Dim url As String = "http://" & Login.serviceIp & ":" & Login.servicePort & "/getClubs"

        Dim peticion As HttpWebRequest = WebRequest.Create(url)
        peticion.ContentType = "application/json; charset=utf-8"
        peticion.Method = "GET"
        peticion.Timeout = 60000
        peticion.KeepAlive = False
        Dim postresponse As HttpWebResponse
        Dim dataStream As Stream

        Try
            postresponse = peticion.GetResponse

            If CInt(postresponse.StatusCode) = 200 Then

                dataStream = postresponse.GetResponseStream()
                Dim reader As New StreamReader(dataStream)
                Dim responseFromServer As String = reader.ReadToEnd()
                clubApplyVar = JsonConvert.DeserializeObject(Of List(Of PirateClub))(responseFromServer)

                For Each data As PirateClub In clubApplyVar
                    If data.club_lvl = 45 Then
                        ComboBox2.Items.Add(data.club_name)

                    ElseIf data.club_lvl = 55 Then
                        ComboBox3.Items.Add(data.club_name)

                    Else
                        ComboBox4.Items.Add(data.club_name)

                    End If
                Next
                reader.Close()
                postresponse.Close()
                response = True
            Else
                Dim reader As New StreamReader(postresponse.GetResponseStream())
                Dim responseFromServer As String = reader.ReadToEnd
                reader.Close()
                MsgBox(responseFromServer)
                postresponse.Close()
                Me.Hide()
            End If
        Catch ex As Exception
            MsgBox("Cannot connect to server")

        End Try
        Return response
    End Function

    Private Function FindChar(ByVal pj As CharacterInfo) As Boolean
        If pj.cha_name = ComboBox1.SelectedItem Then
            charName = pj.cha_name
            charId = pj.cha_id

            Return True
        Else
            Return Nothing
        End If
    End Function

    Public Function getChar()
        Dim response As Boolean = False
        Dim url As String = "http://" & Login.serviceIp & ":" & Login.servicePort & "/getCharacters"

        Dim peticion As HttpWebRequest = WebRequest.Create(url)
        peticion.ContentType = "application/json; charset=utf-8"
        peticion.Method = "GET"
        peticion.Timeout = 60000
        peticion.KeepAlive = False
        peticion.Headers.Add("Key", Login.apiKey)
        peticion.Headers.Add("User", Login.loginUser)
        peticion.Headers.Add("Pass", Login.loginPass)
        Dim postresponse As HttpWebResponse


        Try
            postresponse = peticion.GetResponse
            Dim reader As New StreamReader(postresponse.GetResponseStream())
            Dim responseFromServer As String = reader.ReadToEnd
            If CInt(postresponse.StatusCode) = 200 Then

                postresponse.Close()

                charVar = JsonConvert.DeserializeObject(Of List(Of List(Of CharacterInfo)))(responseFromServer)
                For Each data As List(Of CharacterInfo) In charVar
                    For Each data2 As CharacterInfo In data
                        ComboBox1.Items.Add(data2.cha_name)
                    Next
                Next

            Else
                MsgBox(responseFromServer)
                postresponse.Close()
            End If
            Return True

        Catch ex As Exception
            MsgBox("Failed to connect with server")
            Return False
        End Try
    End Function

    Public Function getCharClubs()
        Dim response As Boolean = False
        If ComboBox1.SelectedIndex > -1 Then
            Dim charId As Integer = Nothing
            Dim charLvl As Integer
            Dim character As List(Of CharacterInfo)
            For Each data As List(Of CharacterInfo) In charVar
                character = data.FindAll(AddressOf FindChar)
                If character IsNot Nothing Then
                    For Each data2 As CharacterInfo In character
                        charId = data2.cha_id
                        charLvl = data2.degree
                    Next
                End If
            Next

            Dim processData As New ApplyClub With {
               .charId = charId}
            Dim postData As String = JsonConvert.SerializeObject(processData)
            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)

            Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/getCharClubs"), HttpWebRequest)
            postReq.Method = "POST"
            postReq.KeepAlive = False
            postReq.ContentType = "application/json"
            postReq.ContentLength = byteData.Length
            postReq.Timeout = 60000
            postReq.Headers.Add("User", Login.loginUser)
            postReq.Headers.Add("Pass", Login.loginPass)
            postReq.Headers.Add("Key", Login.apiKey)
            Try
                Dim postreqstream As Stream = postReq.GetRequestStream()
                postreqstream.Write(byteData, 0, byteData.Length)
                postreqstream.Close()
                Dim postresponse As HttpWebResponse
                postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                Dim responseFromServer As String = postreqreader.ReadToEnd
                If CInt(postresponse.StatusCode) = 200 Then

                    clubVar = JsonConvert.DeserializeObject(Of List(Of PirateClub))(responseFromServer)

                    For Each data As PirateClub In clubVar

                        If data.club_lvl = 45 Then
                            Label32.Text = data.club_name
                            If charLvl >= data.club_lvl Then
                                Button6.Enabled = True
                            End If
                        ElseIf data.club_lvl = 55 Then
                            Label33.Text = data.club_name

                            If charLvl >= data.club_lvl Then
                                Button7.Enabled = True
                            End If
                        ElseIf data.club_lvl = 65 Then
                            Label34.Text = data.club_name
                            If charLvl >= data.club_lvl Then
                                Button8.Enabled = True
                            End If
                        End If
                    Next
                    postresponse.Close()
                    response = True
                Else
                    MsgBox(responseFromServer)
                    postresponse.Close()
                End If


            Catch ex As Exception
                MsgBox("Failed to connect with server")

            End Try
        Else
            MsgBox("You need to select a Character")
        End If

        Return response
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ComboBox1.SelectedIndex > -1 And Not ComboBox2.Text = "" Then

            Dim charId As Integer

            Dim clubId As Integer = Nothing

            Dim clubLvl As Integer = 45

            Dim character As List(Of CharacterInfo)
            For Each data As List(Of CharacterInfo) In charVar
                character = data.FindAll(AddressOf FindChar)
                If character IsNot Nothing Then
                    For Each data2 As CharacterInfo In character
                        charId = data2.cha_id

                    Next
                End If
            Next
            For Each data As PirateClub In clubApplyVar
                If data.club_name = ComboBox2.Text Then
                    clubId = data.club_id
                End If
            Next
            If Not clubId = Nothing Then
                Dim processData As New ApplyClub With {
               .charId = charId,
               .clubId = clubId,
               .clubLv = clubLvl}
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/applyClub"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
                postReq.Headers.Add("User", Login.loginUser)
                postReq.Headers.Add("Pass", Login.loginPass)
                postReq.Headers.Add("Key", Login.apiKey)
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
                        GetAppliedClubs()
                    Else
                        MsgBox(sessionID)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try
            Else
                MsgBox("Cannot find Pirate Club: " & ComboBox2.Text & ", please reload Clubs")
            End If



        Else
            MsgBox("You need to select a Character and a Club")
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ComboBox1.SelectedIndex > -1 And Not ComboBox3.Text = "" Then

            Dim charId As Integer
            Dim clubId As Integer = Nothing
            Dim clubLvl As Integer = 55
            Dim character As List(Of CharacterInfo)
            For Each data As List(Of CharacterInfo) In charVar
                character = data.FindAll(AddressOf FindChar)
                If character IsNot Nothing Then
                    For Each data2 As CharacterInfo In character
                        charId = data2.cha_id

                    Next
                End If
            Next
            For Each data As PirateClub In clubApplyVar
                If data.club_name = ComboBox3.Text Then
                    clubId = data.club_id
                End If
            Next

            If Not clubId = Nothing Then
                Dim processData As New ApplyClub With {
               .charId = charId,
               .clubId = clubId,
               .clubLv = clubLvl}
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/applyClub"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
                postReq.Headers.Add("User", Login.loginUser)
                postReq.Headers.Add("Pass", Login.loginPass)
                postReq.Headers.Add("Key", Login.apiKey)
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
                        GetAppliedClubs()
                    Else
                        MsgBox(sessionID)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try
            Else
                MsgBox("Cannot find Pirate Club: " & ComboBox3.Text & ", please reload Clubs")
            End If



        Else
            MsgBox("You need to select a Character and a Club")
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If ComboBox1.SelectedIndex > -1 And Not ComboBox4.Text = "" Then

            Dim charId As Integer
            Dim clubId As Integer = Nothing
            Dim clubLvl As Integer = 65


            Dim character As List(Of CharacterInfo)
            For Each data As List(Of CharacterInfo) In charVar
                character = data.FindAll(AddressOf FindChar)
                If character IsNot Nothing Then
                    For Each data2 As CharacterInfo In character
                        charId = data2.cha_id

                    Next
                End If
            Next
            For Each data As PirateClub In clubApplyVar
                If data.club_name = ComboBox4.Text Then
                    clubId = data.club_id


                End If
            Next

            If Not clubId = Nothing Then
                Dim processData As New ApplyClub With {
               .charId = charId,
               .clubId = clubId,
               .clubLv = clubLvl
               }
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/applyClub"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
                postReq.Headers.Add("User", Login.loginUser)
                postReq.Headers.Add("Pass", Login.loginPass)
                postReq.Headers.Add("Key", Login.apiKey)
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
                        GetAppliedClubs()
                    Else
                        MsgBox(sessionID)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try
            Else
                MsgBox("Cannot find Pirate Club: " & ComboBox4.Text & ", please reload Clubs")
            End If



        Else
            MsgBox("You need to select a Character and a Club")
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ResetPirateEnable()

        If getClubs() = True Then
            Button10.Enabled = True
            Button14.Enabled = True
            Label25.Enabled = True
            Label29.Enabled = True
            Label30.Enabled = True
            Label31.Enabled = True
            Label32.Enabled = True
            Label33.Enabled = True
            Label34.Enabled = True
            Label36.Enabled = True
            Label37.Enabled = True
            Label38.Enabled = True
            Label39.Enabled = True

            If Label9.Text <= 44 Then
                ComboBox2.Enabled = True
                Button3.Enabled = True
                Label26.Enabled = True
                Label46.Enabled = True
                Label47.Enabled = True
                Label58.Enabled = True
                Label59.Enabled = True
            ElseIf Label9.Text >= 46 Then
                Label43.Enabled = True
                TextBox1.Enabled = True
                Button18.Enabled = True
                Label52.Enabled = True
                Label53.Enabled = True
                Label64.Enabled = True
                Label65.Enabled = True
            End If

            If Label9.Text <= 54 Then
                ComboBox3.Enabled = True
                Button4.Enabled = True
                Label27.Enabled = True
                Label48.Enabled = True
                Label49.Enabled = True
                Label60.Enabled = True
                Label61.Enabled = True
            ElseIf Label9.Text >= 56 Then
                Label44.Enabled = True
                TextBox2.Enabled = True
                Button19.Enabled = True
                Label54.Enabled = True
                Label55.Enabled = True
                Label66.Enabled = True
                Label67.Enabled = True
            End If

            If Label9.Text <= 64 Then
                ComboBox4.Enabled = True
                Button5.Enabled = True
                Label28.Enabled = True
                Label50.Enabled = True
                Label51.Enabled = True
                Label62.Enabled = True
                Label63.Enabled = True
            ElseIf Label9.Text >= 66 Then

                Label45.Enabled = True
                TextBox3.Enabled = True
                Button20.Enabled = True
                Label56.Enabled = True
                Label57.Enabled = True
                Label68.Enabled = True
                Label69.Enabled = True
            End If


        End If

    End Sub

    Function GetAppliedClubs()
        Label32.Text = ""
        Label33.Text = ""
        Label34.Text = ""
        If getCharClubs() = True Then
            If Label32.Text = "" Then
                Label32.Text = "NONE"
            Else
                Button11.Enabled = True

            End If
            If Label33.Text = "" Then
                Label33.Text = "NONE"
            Else
                Button12.Enabled = True
            End If
            If Label34.Text = "" Then
                Label34.Text = "NONE"
            Else

                Button13.Enabled = True
            End If
        End If
        Return True

    End Function

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        GetAppliedClubs()
    End Sub

    Private Sub Label35_Click(sender As Object, e As EventArgs) Handles Label35.Click
        Login.Show()
        Me.Close()


    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        For Each data As PirateClub In clubVar
            If data.club_name = Label32.Text Then
                Dim charId As Integer = Nothing

                Dim character As List(Of CharacterInfo)
                For Each data2 As List(Of CharacterInfo) In charVar
                    character = data2.FindAll(AddressOf FindChar)
                    If character IsNot Nothing Then
                        For Each data3 As CharacterInfo In character
                            charId = data3.cha_id

                        Next
                    End If
                Next

                Dim processData As New ApplyClub With {
                       .charId = charId,
                       .clubId = data.club_id}
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/leaveClub"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
                postReq.Headers.Add("User", Login.loginUser)
                postReq.Headers.Add("Pass", Login.loginPass)
                postReq.Headers.Add("Key", Login.apiKey)
                Try
                    Dim postreqstream As Stream = postReq.GetRequestStream()
                    postreqstream.Write(byteData, 0, byteData.Length)
                    postreqstream.Close()
                    Dim postresponse As HttpWebResponse
                    postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                    Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                    Dim responseFromServer As String = postreqreader.ReadToEnd
                    If CInt(postresponse.StatusCode) = 200 Then

                        MsgBox(responseFromServer)
                        Label32.Text = "NONE"
                        Button6.Enabled = False
                        Button11.Enabled = False
                        postresponse.Close()

                    Else
                        MsgBox(responseFromServer)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try

            End If

        Next
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        For Each data As PirateClub In clubVar
            If data.club_name = Label33.Text Then
                Dim charId As Integer = Nothing

                Dim character As List(Of CharacterInfo)
                For Each data2 As List(Of CharacterInfo) In charVar
                    character = data2.FindAll(AddressOf FindChar)
                    If character IsNot Nothing Then
                        For Each data3 As CharacterInfo In character
                            charId = data3.cha_id

                        Next
                    End If
                Next

                Dim processData As New ApplyClub With {
                       .charId = charId,
                       .clubId = data.club_id}
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/leaveClub"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
                postReq.Headers.Add("User", Login.loginUser)
                postReq.Headers.Add("Pass", Login.loginPass)
                postReq.Headers.Add("Key", Login.apiKey)
                Try
                    Dim postreqstream As Stream = postReq.GetRequestStream()
                    postreqstream.Write(byteData, 0, byteData.Length)
                    postreqstream.Close()
                    Dim postresponse As HttpWebResponse
                    postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                    Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                    Dim responseFromServer As String = postreqreader.ReadToEnd
                    If CInt(postresponse.StatusCode) = 200 Then

                        MsgBox(responseFromServer)
                        Label33.Text = "NONE"
                        Button7.Enabled = False
                        Button12.Enabled = False
                        postresponse.Close()

                    Else
                        MsgBox(responseFromServer)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try

            End If

        Next
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        For Each data As PirateClub In clubVar
            If data.club_name = Label34.Text Then
                Dim charId As Integer = Nothing

                Dim character As List(Of CharacterInfo)
                For Each data2 As List(Of CharacterInfo) In charVar
                    character = data2.FindAll(AddressOf FindChar)
                    If character IsNot Nothing Then
                        For Each data3 As CharacterInfo In character
                            charId = data3.cha_id

                        Next
                    End If
                Next

                Dim processData As New ApplyClub With {
                       .charId = charId,
                       .clubId = data.club_id}
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/leaveClub"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
                postReq.Headers.Add("User", Login.loginUser)
                postReq.Headers.Add("Pass", Login.loginPass)
                postReq.Headers.Add("Key", Login.apiKey)
                Try
                    Dim postreqstream As Stream = postReq.GetRequestStream()
                    postreqstream.Write(byteData, 0, byteData.Length)
                    postreqstream.Close()
                    Dim postresponse As HttpWebResponse
                    postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                    Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                    Dim responseFromServer As String = postreqreader.ReadToEnd
                    If CInt(postresponse.StatusCode) = 200 Then

                        MsgBox(responseFromServer)
                        Label34.Text = "NONE"
                        Button8.Enabled = False
                        Button13.Enabled = False
                        postresponse.Close()

                    Else
                        MsgBox(responseFromServer)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try
            End If

        Next
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        For Each data As PirateClub In clubVar
            If data.club_name = Label32.Text Then
                Dim charId As Integer = Nothing

                Dim character As List(Of CharacterInfo)
                For Each data2 As List(Of CharacterInfo) In charVar
                    character = data2.FindAll(AddressOf FindChar)
                    If character IsNot Nothing Then
                        For Each data3 As CharacterInfo In character
                            charId = data3.cha_id

                        Next
                    End If
                Next

                Dim processData As New ApplyClub With {
                       .charId = charId,
                       .clubId = data.club_id}
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/redeemClub"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
                postReq.Headers.Add("User", Login.loginUser)
                postReq.Headers.Add("Pass", Login.loginPass)
                postReq.Headers.Add("Key", Login.apiKey)
                Try
                    Dim postreqstream As Stream = postReq.GetRequestStream()
                    postreqstream.Write(byteData, 0, byteData.Length)
                    postreqstream.Close()
                    Dim postresponse As HttpWebResponse
                    postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                    Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                    Dim responseFromServer As String = postreqreader.ReadToEnd
                    If CInt(postresponse.StatusCode) = 200 Then

                        MsgBox(responseFromServer)
                        Label32.Text = "NONE"
                        Button6.Enabled = False
                        Button11.Enabled = False
                        postresponse.Close()

                    Else
                        MsgBox(responseFromServer)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try

            End If

        Next
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        For Each data As PirateClub In clubVar
            If data.club_name = Label33.Text Then
                Dim charId As Integer = Nothing

                Dim character As List(Of CharacterInfo)
                For Each data2 As List(Of CharacterInfo) In charVar
                    character = data2.FindAll(AddressOf FindChar)
                    If character IsNot Nothing Then
                        For Each data3 As CharacterInfo In character
                            charId = data3.cha_id

                        Next
                    End If
                Next

                Dim processData As New ApplyClub With {
                       .charId = charId,
                       .clubId = data.club_id}
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/redeemClub"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
                postReq.Headers.Add("User", Login.loginUser)
                postReq.Headers.Add("Pass", Login.loginPass)
                postReq.Headers.Add("Key", Login.apiKey)
                Try
                    Dim postreqstream As Stream = postReq.GetRequestStream()
                    postreqstream.Write(byteData, 0, byteData.Length)
                    postreqstream.Close()
                    Dim postresponse As HttpWebResponse
                    postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                    Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                    Dim responseFromServer As String = postreqreader.ReadToEnd
                    If CInt(postresponse.StatusCode) = 200 Then

                        MsgBox(responseFromServer)
                        Label33.Text = "NONE"
                        Button7.Enabled = False
                        Button12.Enabled = False
                        postresponse.Close()

                    Else
                        MsgBox(responseFromServer)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try

            End If

        Next
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        For Each data As PirateClub In clubVar
            If data.club_name = Label34.Text Then
                Dim charId As Integer = Nothing

                Dim character As List(Of CharacterInfo)
                For Each data2 As List(Of CharacterInfo) In charVar
                    character = data2.FindAll(AddressOf FindChar)
                    If character IsNot Nothing Then
                        For Each data3 As CharacterInfo In character
                            charId = data3.cha_id

                        Next
                    End If
                Next

                Dim processData As New ApplyClub With {
                       .charId = charId,
                       .clubId = data.club_id}
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/redeemClub"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
                postReq.Headers.Add("User", Login.loginUser)
                postReq.Headers.Add("Pass", Login.loginPass)
                postReq.Headers.Add("Key", Login.apiKey)
                Try
                    Dim postreqstream As Stream = postReq.GetRequestStream()
                    postreqstream.Write(byteData, 0, byteData.Length)
                    postreqstream.Close()
                    Dim postresponse As HttpWebResponse
                    postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                    Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                    Dim responseFromServer As String = postreqreader.ReadToEnd


                    If CInt(postresponse.StatusCode) = 200 Then

                        MsgBox(responseFromServer)
                        Label34.Text = "NONE"
                        Button8.Enabled = False
                        Button13.Enabled = False
                        postresponse.Close()

                    Else
                        MsgBox(responseFromServer)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try
            End If

        Next
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If Label9.Text > 45 Then
            getOwnedClubs()
        Else
            MsgBox("You need to be at least Lv46 to Manage Pirate Clubs")
        End If

    End Sub

    Function getOwnedClubs()
        Dim charId As Integer = Nothing
        Dim character As List(Of CharacterInfo)
        For Each data2 As List(Of CharacterInfo) In charVar
            character = data2.FindAll(AddressOf FindChar)
            If character IsNot Nothing Then
                For Each data3 As CharacterInfo In character
                    charId = data3.cha_id

                Next
            End If
        Next
        Dim processData As New ApplyClub With {
                .charId = charId}
        Dim postData As String = JsonConvert.SerializeObject(processData)
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/getOwnedClubs"), HttpWebRequest)
        postReq.Method = "POST"
        postReq.KeepAlive = False
        postReq.ContentType = "application/json"
        postReq.ContentLength = byteData.Length
        postReq.Timeout = 60000
        postReq.Headers.Add("User", Login.loginUser)
        postReq.Headers.Add("Pass", Login.loginPass)
        postReq.Headers.Add("Key", Login.apiKey)
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
                charOwnVar = JsonConvert.DeserializeObject(Of List(Of PirateClub))(sessionID)

                For Each data As PirateClub In charOwnVar
                    If data.club_lvl = 45 Then
                        Label40.Enabled = True
                        Label40.Text = data.club_name
                        Button15.Enabled = True
                        Label64.Text = data.club_gradChar
                        Label64.Enabled = True
                        Label65.Text = data.club_gradPend
                        Label65.Enabled = True
                        If data.club_gradPend > 0 Then
                            Button21.Enabled = True
                        Else
                            Button21.Enabled = False
                        End If
                    ElseIf data.club_lvl = 55 Then
                        Label41.Enabled = True
                        Label41.Text = data.club_name
                        Button16.Enabled = True
                        Label66.Text = data.club_gradChar
                        Label66.Enabled = True
                        Label67.Text = data.club_gradPend
                        Label67.Enabled = True
                        If data.club_gradPend > 0 Then
                            Button22.Enabled = True
                        Else
                            Button22.Enabled = False
                        End If
                    ElseIf data.club_lvl = 65 Then
                        Label42.Enabled = True
                        Label42.Text = data.club_name
                        Button17.Enabled = True
                        Label68.Text = data.club_gradChar
                        Label68.Enabled = True
                        Label69.Text = data.club_gradPend
                        Label69.Enabled = True
                        If data.club_gradPend > 0 Then
                            Button23.Enabled = True
                        Else
                            Button23.Enabled = False
                        End If
                    End If
                Next
                If Label40.Text = "" Then
                    Label40.Text = "NONE"
                    Label40.Enabled = True
                End If
                If Label41.Text = "" Then
                    Label41.Text = "NONE"
                    Label41.Enabled = True

                End If
                If Label42.Text = "" Then
                    Label42.Text = "NONE"
                    Label42.Enabled = True

                End If


            ElseIf CInt(postresponse.StatusCode) = 201 Then
                MsgBox(sessionID)
                Label40.Enabled = True
                Label40.Text = "NONE"
                Label41.Enabled = True
                Label41.Text = "NONE"
                Label42.Enabled = True
                Label42.Text = "NONE"
                postresponse.Close()
            Else
                MsgBox(sessionID)
                postresponse.Close()
            End If


        Catch ex As Exception
            MsgBox("Failed to connect with server")

        End Try
        Return True

    End Function

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Dim charId As Integer = Nothing
        Dim clubId As Integer
        For Each data As PirateClub In charOwnVar
            If data.club_name = Label40.Text Then
                clubId = data.club_id
            End If
        Next
        Dim character As List(Of CharacterInfo)
        For Each data2 As List(Of CharacterInfo) In charVar
            character = data2.FindAll(AddressOf FindChar)
            If character IsNot Nothing Then
                For Each data3 As CharacterInfo In character
                    charId = data3.cha_id

                Next
            End If
        Next
        Dim processData As New ApplyClub With {
                .charId = charId,
                .clubId = clubId
        }
        Dim postData As String = JsonConvert.SerializeObject(processData)
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/deleteClub"), HttpWebRequest)
        postReq.Method = "POST"
        postReq.KeepAlive = False
        postReq.ContentType = "application/json"
        postReq.ContentLength = byteData.Length
        postReq.Timeout = 60000
        postReq.Headers.Add("User", Login.loginUser)
        postReq.Headers.Add("Pass", Login.loginPass)
        postReq.Headers.Add("Key", Login.apiKey)
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

                Label40.Text = "NONE"

                Label64.Text = ""
                Label65.Text = ""
                Button15.Enabled = False

            Else
                MsgBox(sessionID)
                postresponse.Close()
            End If


        Catch ex As Exception
            MsgBox("Failed to connect with server")

        End Try
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim charId As Integer = Nothing
        Dim clubId As Integer
        For Each data As PirateClub In charOwnVar
            If data.club_name = Label41.Text Then
                clubId = data.club_id
            End If
        Next
        Dim character As List(Of CharacterInfo)
        For Each data2 As List(Of CharacterInfo) In charVar
            character = data2.FindAll(AddressOf FindChar)
            If character IsNot Nothing Then
                For Each data3 As CharacterInfo In character
                    charId = data3.cha_id

                Next
            End If
        Next
        Dim processData As New ApplyClub With {
                .charId = charId,
                .clubId = clubId
        }
        Dim postData As String = JsonConvert.SerializeObject(processData)
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/deleteClub"), HttpWebRequest)
        postReq.Method = "POST"
        postReq.KeepAlive = False
        postReq.ContentType = "application/json"
        postReq.ContentLength = byteData.Length
        postReq.Timeout = 60000
        postReq.Headers.Add("User", Login.loginUser)
        postReq.Headers.Add("Pass", Login.loginPass)
        postReq.Headers.Add("Key", Login.apiKey)
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

                Label41.Text = "NONE"
                Label67.Text = ""
                Label66.Text = ""
                Button16.Enabled = False

            Else
                MsgBox(sessionID)
                postresponse.Close()
            End If


        Catch ex As Exception
            MsgBox("Failed to connect with server")

        End Try
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim charId As Integer = Nothing
        Dim clubId As Integer
        For Each data As PirateClub In charOwnVar
            If data.club_name = Label42.Text Then
                clubId = data.club_id
            End If
        Next
        Dim character As List(Of CharacterInfo)
        For Each data2 As List(Of CharacterInfo) In charVar
            character = data2.FindAll(AddressOf FindChar)
            If character IsNot Nothing Then
                For Each data3 As CharacterInfo In character
                    charId = data3.cha_id

                Next
            End If
        Next
        Dim processData As New ApplyClub With {
                .charId = charId,
                .clubId = clubId
        }
        Dim postData As String = JsonConvert.SerializeObject(processData)
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/deleteClub"), HttpWebRequest)
        postReq.Method = "POST"
        postReq.KeepAlive = False
        postReq.ContentType = "application/json"
        postReq.ContentLength = byteData.Length
        postReq.Timeout = 60000
        postReq.Headers.Add("User", Login.loginUser)
        postReq.Headers.Add("Pass", Login.loginPass)
        postReq.Headers.Add("Key", Login.apiKey)
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
                Label42.Text = "NONE"
                Label68.Text = ""
                Label69.Text = ""
                Button17.Enabled = False
            Else
                MsgBox(sessionID)
                postresponse.Close()
            End If


        Catch ex As Exception
            MsgBox("Failed to connect with server")

        End Try
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        If Not TextBox1.Text = "" Then
            Dim charId As Integer = Nothing
            Dim charName As String = Nothing
            Dim clubLv As Integer = 45
            Dim clubName As String = TextBox1.Text


            Dim character As List(Of CharacterInfo)
            For Each data2 As List(Of CharacterInfo) In charVar
                character = data2.FindAll(AddressOf FindChar)
                If character IsNot Nothing Then
                    For Each data3 As CharacterInfo In character
                        charId = data3.cha_id
                        charName = data3.cha_name
                    Next
                End If
            Next
            Dim processData As New PirateClub With {
                    .club_leaderid = charId,
                    .club_leadername = charName,
                    .club_lvl = clubLv,
                    .club_name = clubName
            }
            Dim postData As String = JsonConvert.SerializeObject(processData)
            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)

            Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/createClub"), HttpWebRequest)
            postReq.Method = "POST"
            postReq.KeepAlive = False
            postReq.ContentType = "application/json"
            postReq.ContentLength = byteData.Length
            postReq.Timeout = 60000
            postReq.Headers.Add("User", Login.loginUser)
            postReq.Headers.Add("Pass", Login.loginPass)
            postReq.Headers.Add("Key", Login.apiKey)
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
                    getOwnedClubs()
                    TextBox1.Text = ""
                Else
                    MsgBox(sessionID)
                    postresponse.Close()
                End If


            Catch ex As Exception
                MsgBox("Failed to connect with server")

            End Try
        Else
            MsgBox("You need to put a name to your Club")
        End If

    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        If Not TextBox2.Text = "" Then
            Dim charId As Integer = Nothing
            Dim charName As String = Nothing
            Dim clubLv As Integer = 55
            Dim clubName As String = TextBox2.Text


            Dim character As List(Of CharacterInfo)
            For Each data2 As List(Of CharacterInfo) In charVar
                character = data2.FindAll(AddressOf FindChar)
                If character IsNot Nothing Then
                    For Each data3 As CharacterInfo In character
                        charId = data3.cha_id
                        charName = data3.cha_name
                    Next
                End If
            Next
            Dim processData As New PirateClub With {
                    .club_leaderid = charId,
                    .club_leadername = charName,
                    .club_lvl = clubLv,
                    .club_name = clubName
            }
            Dim postData As String = JsonConvert.SerializeObject(processData)
            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)

            Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/createClub"), HttpWebRequest)
            postReq.Method = "POST"
            postReq.KeepAlive = False
            postReq.ContentType = "application/json"
            postReq.ContentLength = byteData.Length
            postReq.Timeout = 60000
            postReq.Headers.Add("User", Login.loginUser)
            postReq.Headers.Add("Pass", Login.loginPass)
            postReq.Headers.Add("Key", Login.apiKey)

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
                    getOwnedClubs()
                    TextBox2.Text = ""
                Else
                    MsgBox(sessionID)
                    postresponse.Close()
                End If


            Catch ex As Exception
                MsgBox("Failed to connect with server")

            End Try
        Else
            MsgBox("You need to put a name to your Club")
        End If

    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        If Not TextBox3.Text = "" Then
            Dim charId As Integer = Nothing
            Dim charName As String = Nothing
            Dim clubLv As Integer = 65
            Dim clubName As String = TextBox3.Text


            Dim character As List(Of CharacterInfo)
            For Each data2 As List(Of CharacterInfo) In charVar
                character = data2.FindAll(AddressOf FindChar)
                If character IsNot Nothing Then
                    For Each data3 As CharacterInfo In character
                        charId = data3.cha_id
                        charName = data3.cha_name
                    Next
                End If
            Next
            Dim processData As New PirateClub With {
                    .club_leaderid = charId,
                    .club_leadername = charName,
                    .club_lvl = clubLv,
                    .club_name = clubName
            }
            Dim postData As String = JsonConvert.SerializeObject(processData)
            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)

            Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/createClub"), HttpWebRequest)
            postReq.Method = "POST"
            postReq.KeepAlive = False
            postReq.ContentType = "application/json"
            postReq.ContentLength = byteData.Length
            postReq.Timeout = 60000
            postReq.Headers.Add("User", Login.loginUser)
            postReq.Headers.Add("Pass", Login.loginPass)
            postReq.Headers.Add("Key", Login.apiKey)
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
                    getOwnedClubs()
                    TextBox3.Text = ""
                Else
                    MsgBox(sessionID)
                    postresponse.Close()
                End If


            Catch ex As Exception
                MsgBox("Failed to connect with server")

            End Try
        Else
            MsgBox("You need to put a name to your Club")
        End If

    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        For Each data As PirateClub In clubApplyVar
            If data.club_name = ComboBox2.Text Then
                Label58.Text = data.club_leadername
                Label59.Text = data.club_gradChar
            End If
        Next
    End Sub

    Private Sub ComboBox3_TextChanged(sender As Object, e As EventArgs) Handles ComboBox3.TextChanged
        For Each data As PirateClub In clubApplyVar
            If data.club_name = ComboBox3.Text Then
                Label60.Text = data.club_leadername
                Label61.Text = data.club_gradChar
            End If
        Next
    End Sub

    Private Sub ComboBox4_TextChanged(sender As Object, e As EventArgs) Handles ComboBox4.TextChanged
        For Each data As PirateClub In clubApplyVar
            If data.club_name = ComboBox4.Text Then
                Label62.Text = data.club_leadername
                Label63.Text = data.club_gradChar
            End If
        Next
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        For Each data As PirateClub In charOwnVar
            If data.club_name = Label40.Text Then

                Dim charId As Integer = Nothing

                Dim character As List(Of CharacterInfo)
                For Each data2 As List(Of CharacterInfo) In charVar
                    character = data2.FindAll(AddressOf FindChar)
                    If character IsNot Nothing Then
                        For Each data3 As CharacterInfo In character
                            charId = data3.cha_id

                        Next
                    End If
                Next

                Dim processData As New ApplyClub With {
                       .charId = charId,
                       .clubId = data.club_id}
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/redeemPend"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
                postReq.Headers.Add("User", Login.loginUser)
                postReq.Headers.Add("Pass", Login.loginPass)
                postReq.Headers.Add("Key", Login.apiKey)
                Try
                    Dim postreqstream As Stream = postReq.GetRequestStream()
                    postreqstream.Write(byteData, 0, byteData.Length)
                    postreqstream.Close()
                    Dim postresponse As HttpWebResponse
                    postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                    Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                    Dim responseFromServer As String = postreqreader.ReadToEnd
                    If CInt(postresponse.StatusCode) = 200 Then

                        MsgBox(responseFromServer)

                        postresponse.Close()
                        getOwnedClubs()
                    Else
                        MsgBox(responseFromServer)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try

            End If

        Next
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        For Each data As PirateClub In charOwnVar
            If data.club_name = Label41.Text Then

                Dim charId As Integer = Nothing

                Dim character As List(Of CharacterInfo)
                For Each data2 As List(Of CharacterInfo) In charVar
                    character = data2.FindAll(AddressOf FindChar)
                    If character IsNot Nothing Then
                        For Each data3 As CharacterInfo In character
                            charId = data3.cha_id

                        Next
                    End If
                Next

                Dim processData As New ApplyClub With {
                       .charId = charId,
                       .clubId = data.club_id}
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/redeemPend"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
                postReq.Headers.Add("User", Login.loginUser)
                postReq.Headers.Add("Pass", Login.loginPass)
                postReq.Headers.Add("Key", Login.apiKey)
                Try
                    Dim postreqstream As Stream = postReq.GetRequestStream()
                    postreqstream.Write(byteData, 0, byteData.Length)
                    postreqstream.Close()
                    Dim postresponse As HttpWebResponse
                    postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                    Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                    Dim responseFromServer As String = postreqreader.ReadToEnd
                    If CInt(postresponse.StatusCode) = 200 Then

                        MsgBox(responseFromServer)

                        postresponse.Close()
                        getOwnedClubs()
                    Else
                        MsgBox(responseFromServer)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try

            End If

        Next
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        For Each data As PirateClub In charOwnVar
            If data.club_name = Label42.Text Then

                Dim charId As Integer = Nothing

                Dim character As List(Of CharacterInfo)
                For Each data2 As List(Of CharacterInfo) In charVar
                    character = data2.FindAll(AddressOf FindChar)
                    If character IsNot Nothing Then
                        For Each data3 As CharacterInfo In character
                            charId = data3.cha_id

                        Next
                    End If
                Next

                Dim processData As New ApplyClub With {
                       .charId = charId,
                       .clubId = data.club_id}
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/redeemPend"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
                postReq.Headers.Add("User", Login.loginUser)
                postReq.Headers.Add("Pass", Login.loginPass)
                postReq.Headers.Add("Key", Login.apiKey)
                Try
                    Dim postreqstream As Stream = postReq.GetRequestStream()
                    postreqstream.Write(byteData, 0, byteData.Length)
                    postreqstream.Close()
                    Dim postresponse As HttpWebResponse
                    postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
                    Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
                    Dim responseFromServer As String = postreqreader.ReadToEnd
                    If CInt(postresponse.StatusCode) = 200 Then

                        MsgBox(responseFromServer)

                        postresponse.Close()
                        getOwnedClubs()
                    Else
                        MsgBox(responseFromServer)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try

            End If

        Next
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

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not Login._allowedChars.Contains(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub




End Class


Public Class CharacterInfo
    Public cha_id As String
    Public cha_name As String
    Public motto As String
    Public job As String
    Public degree As String
    Public gd As String
    Public map As String
    Public kb_capacity As String
    Public credit As String
    Public str As String
    Public agi As String
    Public dex As String
    Public con As String
    Public sta As String
End Class

Public Class PirateClub
    Public club_leadername As String
    Public club_lvl As Integer
    Public club_id As Integer
    Public club_leaderid As Integer
    Public club_name As String
    Public club_gradChar As Integer
    Public club_gradPend As Integer
End Class

Public Class ApplyClub
    Public charId As Integer
    Public clubId As Integer
    Public clubLv As Integer
End Class