Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json

Public Class Stall

    Public charVar As List(Of List(Of CharacterInfo))
    Public charInv As List(Of CharInventory)
    Public stallId As Integer
    Public stallItems As List(Of StallItems)
    Public allStalls As List(Of AllStalls)
    Public ownerStall As List(Of StallItems)
    Public itemsDB As List(Of ItemsDB)
    Public itemsFound As List(Of StallItems)
    Public stallStatus As Integer
    Public _allowedNumbers As String = "0123456789"


    Private Sub Stall_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If getItemsDb() = False Then
            Login.Show()
            Me.Close()
        End If
        getChar()

    End Sub

    Public Function resetChar()
        itemsFound = Nothing
        charInv = Nothing
        stallId = Nothing
        stallItems = Nothing

        Button1.Enabled = False
        Button1.Text = "Create"
        Button2.Enabled = False
        Button5.Enabled = False
        Button4.Enabled = False
        Button6.Enabled = False
        Button7.Enabled = False

        NumericUpDown1.Enabled = False
        NumericUpDown1.Value = 1
        NumericUpDown2.Enabled = False
        NumericUpDown2.Value = 1

        ComboBox2.Enabled = False
        ComboBox2.Items.Clear()
        ComboBox2.Text = ""
        ComboBox3.Enabled = False
        ComboBox3.Items.Clear()
        ComboBox3.Text = ""
        ComboBox4.Enabled = False
        ComboBox4.Items.Clear()
        ComboBox4.Text = ""
        ComboBox5.Enabled = False
        ComboBox5.Items.Clear()
        ComboBox5.Text = ""
        ComboBox6.Enabled = False
        ComboBox6.Items.Clear()
        ComboBox6.Text = ""
        ComboBox7.Enabled = False
        ComboBox7.Items.Clear()
        ComboBox7.Text = ""
        ComboBox8.Items.Clear()
        ComboBox8.Enabled = False
        ComboBox8.Text = ""
        ComboBox9.Items.Clear()


        Label2.Enabled = False
        Label2.Text = ""
        Label3.Enabled = False
        Label3.Text = ""
        Label4.Enabled = False
        Label4.Text = ""
        Label5.Text = ""

        Label6.Enabled = False
        Label6.Text = ""
        Label7.Enabled = False
        Label7.Text = ""
        Label8.Text = ""
        Label9.Text = ""
        Label8.Enabled = False
        Label9.Enabled = False

        TextBox1.Text = ""
        TextBox1.Enabled = False

        TextBox2.Text = ""
        TextBox2.Enabled = False
        TextBox3.Text = ""
        TextBox3.Enabled = False
        TextBox4.Text = ""
        TextBox4.Enabled = False
        Return True
    End Function

    Public Function getItemsDb()
        Dim response As Boolean = False
        Dim url As String = "http://" & Login.serviceIp & ":" & Login.servicePort & "/getItemsDB"

        Dim peticion As HttpWebRequest = WebRequest.Create(url)
        peticion.ContentType = "application/json; charset=utf-8"
        peticion.Method = "GET"
        peticion.Timeout = 60000
        peticion.KeepAlive = False
        peticion.Headers.Add("Key", Login.apiKey)
        Dim postresponse As HttpWebResponse


        Try
            postresponse = peticion.GetResponse
            Dim reader As New StreamReader(postresponse.GetResponseStream())
            Dim responseFromServer As String = reader.ReadToEnd
            If CInt(postresponse.StatusCode) = 200 Then
                itemsDB = JsonConvert.DeserializeObject(Of List(Of ItemsDB))(responseFromServer)
                reader.Close()
                postresponse.Close()
                response = True
            Else

                reader.Close()
                MsgBox(responseFromServer)
                postresponse.Close()
                response = False
            End If
        Catch ex As Exception
            MsgBox("Cannot connect to server")
            Login.Show()
            Me.Close()
            response = False
        End Try
        Return response
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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        getCharStall()
        If getAllStalls() = True Then
            For Each data As AllStalls In allStalls
                ComboBox5.Items.Add(data.stall_name)
            Next
            ComboBox5.Enabled = True
            TextBox3.Enabled = True

        Else
            ComboBox5.Enabled = False
            ComboBox5.Items.Clear()
            ComboBox5.Text = ""

        End If
        For Each data As List(Of CharacterInfo) In charVar
            For Each data2 As CharacterInfo In data
                If data2.cha_name = ComboBox1.SelectedItem Then
                    Label5.Text = data2.gd
                End If
            Next
        Next
    End Sub

    Public Function getAllStalls()
        Dim response As Boolean = False
        Dim url As String = "http://" & Login.serviceIp & ":" & Login.servicePort & "/getAllStalls"

        Dim peticion As HttpWebRequest = WebRequest.Create(url)
        peticion.ContentType = "application/json; charset=utf-8"
        peticion.Method = "GET"
        peticion.Timeout = 60000
        peticion.KeepAlive = False
        peticion.Headers.Add("Key", Login.apiKey)
        Dim postresponse As HttpWebResponse


        Try
            postresponse = peticion.GetResponse
            Dim reader As New StreamReader(postresponse.GetResponseStream())
            Dim responseFromServer As String = reader.ReadToEnd
            If CInt(postresponse.StatusCode) = 200 Then
                allStalls = JsonConvert.DeserializeObject(Of List(Of AllStalls))(responseFromServer)
                reader.Close()
                postresponse.Close()
                response = True
            Else

                reader.Close()
                MsgBox(responseFromServer)
                postresponse.Close()
                response = False
            End If
        Catch ex As Exception
            MsgBox("Cannot connect to server")
            response = False
        End Try
        Return response
    End Function

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Login.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If stallStatus = 0 Then
            If Not TextBox4.Text = "" Then
                Dim charId As Integer
                Dim charName As String = Nothing
                Dim stallName As String = TextBox4.Text

                For Each data As List(Of CharacterInfo) In charVar
                    For Each data3 As CharacterInfo In data
                        If data3.cha_name = ComboBox1.SelectedItem Then
                            charId = data3.cha_id
                            charName = data3.cha_name
                        End If
                    Next
                Next


                Dim processData As New Stalls With {
                        .charId = charId,
                        .charName = charName,
                        .stallName = stallName}
                Dim postData As String = JsonConvert.SerializeObject(processData)

                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/createStall"), HttpWebRequest)
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
                        Button1.Enabled = False
                        postresponse.Close()
                        getCharStall()
                        If getAllStalls() = True Then
                            For Each data As AllStalls In allStalls
                                ComboBox5.Items.Add(data.stall_name)
                            Next
                            ComboBox5.Enabled = True
                            TextBox3.Enabled = True

                        Else
                            ComboBox5.Enabled = False
                            ComboBox5.Items.Clear()
                            ComboBox5.Text = ""

                        End If
                    Else
                        MsgBox(sessionID)
                        postresponse.Close()
                    End If


                Catch ex As Exception
                    MsgBox("Failed to connect with server")
                End Try
            Else
                MsgBox("You have to name your stall")
            End If
        Else
            Dim charId As Integer

            For Each data As List(Of CharacterInfo) In charVar
                For Each data3 As CharacterInfo In data
                    If data3.cha_name = ComboBox1.SelectedItem Then
                        charId = data3.cha_id

                    End If
                Next
            Next

            Dim processData As New Stalls With {
                    .charId = charId}
            Dim postData As String = JsonConvert.SerializeObject(processData)

            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)

            Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/deleteStall"), HttpWebRequest)
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
                    resetChar()
                    getCharStall()
                    If getAllStalls() = True Then
                        For Each data As AllStalls In allStalls
                            ComboBox5.Items.Add(data.stall_name)
                        Next
                        ComboBox5.Enabled = True
                        TextBox3.Enabled = True

                    Else
                        ComboBox5.Enabled = False
                        ComboBox5.Items.Clear()
                        ComboBox5.Text = ""

                    End If
                    For Each data As List(Of CharacterInfo) In charVar
                        For Each data2 As CharacterInfo In data
                            If data2.cha_name = ComboBox1.SelectedItem Then
                                Label5.Text = data2.gd
                            End If
                        Next
                    Next
                Else
                    MsgBox(sessionID)
                    postresponse.Close()
                End If


            Catch ex As Exception
                MsgBox("Failed to connect with server")
            End Try
        End If


    End Sub

    Function getCharInv()
        Dim response As Boolean = False
        Dim charId As Integer

        For Each data As List(Of CharacterInfo) In charVar
            For Each data3 As CharacterInfo In data
                If data3.cha_name = ComboBox1.SelectedItem Then
                    charId = data3.cha_id

                End If
            Next
        Next

        Dim processData As New Stalls With {
                .charId = charId}
        Dim postData As String = JsonConvert.SerializeObject(processData)

        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/getCharInventory"), HttpWebRequest)
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
                charInv = JsonConvert.DeserializeObject(Of List(Of CharInventory))(sessionID)
                response = True
            Else
                response = False
            End If

        Catch ex As Exception
            MsgBox("Failed to connect with server")
        End Try
        Return response
    End Function

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

        Button2.Enabled = True
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        Dim found As Integer = 0
        For Each data As CharInventory In charInv

            Dim itemId As String = Nothing

            If found = 0 Then
                For Each data2 As ItemsDB In itemsDB
                    If data2.itemName = ComboBox2.SelectedItem Then
                        itemId = data2.itemId
                    End If
                Next
                If itemId = Nothing Then
                    itemId = ComboBox2.SelectedItem
                End If

                If data.itemId = itemId Then
                    Label2.Text = data.itemAmount
                    found = 1
                End If
            End If

        Next
    End Sub

    Public Function getCharStall()
        Dim response As Boolean = False
        resetChar()
        Dim charId As Integer
        For Each data As List(Of CharacterInfo) In charVar
            For Each data3 As CharacterInfo In data
                If data3.cha_name = ComboBox1.SelectedItem Then
                    charId = data3.cha_id
                End If
            Next
        Next

        Dim processData As New Stalls With {
                .charId = charId}
        Dim postData As String = JsonConvert.SerializeObject(processData)

        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/getStall"), HttpWebRequest)
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
                Button7.Enabled = True
                Button1.Enabled = True
                Button1.Text = "Delete"
                stallStatus = 1
                stallItems = JsonConvert.DeserializeObject(Of List(Of StallItems))(sessionID)
                For Each data As StallItems In stallItems
                    Dim itemId As String = Nothing

                    For Each data2 As ItemsDB In itemsDB
                        If data2.itemId = data.item_code Then
                            itemId = data2.itemName
                            ComboBox3.Items.Add(itemId)
                            ComboBox4.Items.Add(data.item_id)
                        End If
                    Next
                    'If itemId = Nothing Then
                    'itemId = data.item_code

                    'End If



                Next
                ComboBox3.Enabled = True
                ComboBox4.Enabled = True

                Label3.Enabled = True


                postresponse.Close()
                If getCharInv() = True Then
                    ComboBox2.Enabled = True
                    Label2.Enabled = True

                    Dim found As Integer = 0

                    For Each data As CharInventory In charInv
                        Dim itemId As String = Nothing
                        Dim itemFound As Integer = 0
                        For Each data2 As ItemsDB In itemsDB
                            If data2.itemId = data.itemId Then
                                itemId = data2.itemName
                                If ComboBox2.FindStringExact(itemId) > -1 Then
                                    For Each data3 As CharInventory In charInv
                                        If found = 0 Then
                                            If data.itemId = data3.itemId Then
                                                data3.itemAmount += data.itemAmount


                                                found = 1
                                            End If
                                        End If
                                    Next
                                Else

                                    ComboBox2.Items.Add(itemId)

                                End If
                            End If
                        Next

                        'If itemId = Nothing Then
                        'itemId = data.itemId
                        'End If


                        found = 0
                    Next
                Else
                    MsgBox("Your character has no items.")

                End If
            ElseIf sessionID = "You dont have a Stall. Please create one." Then

                Button1.Enabled = True
                TextBox4.Enabled = True
                TextBox4.Text = "Please name your Stall"
                Button7.Enabled = False
                stallStatus = 0
                postresponse.Close()
            Else

                Button7.Enabled = True
                Button1.Enabled = True
                Button1.Text = "Delete"
                stallStatus = 1
                postresponse.Close()
                If getCharInv() = True Then
                    ComboBox2.Enabled = True
                    Label2.Enabled = True

                    Dim found As Integer = 0

                    For Each data As CharInventory In charInv
                        Dim itemId As String = Nothing
                        Dim itemFound As Integer = 0
                        For Each data2 As ItemsDB In itemsDB
                            If data2.itemId = data.itemId Then
                                itemId = data2.itemName
                                If ComboBox2.FindStringExact(itemId) > -1 Then
                                    For Each data3 As CharInventory In charInv
                                        If found = 0 Then
                                            If data.itemId = data3.itemId Then
                                                data3.itemAmount += data.itemAmount

                                                found = 1
                                            End If
                                        End If
                                    Next
                                Else

                                    ComboBox2.Items.Add(itemId)

                                End If
                            End If
                        Next

                        'If itemId = Nothing Then
                        'itemId = data.itemId
                        'End If


                        found = 0
                    Next
                Else
                    MsgBox("Your character has no items.")
                End If
            End If


        Catch ex As Exception
            MsgBox("Failed to connect with server")
        End Try
        Return response
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox2.SelectedIndex > -1 And TextBox1.Text > 0 And TextBox2.Text > 0 Then
            Dim quantity As Integer = Int(TextBox1.Text)
            Dim price As Integer = Int(TextBox2.Text)
            If quantity > 0 And price > 0 Then
                Dim itemId As Integer
                Dim itemAmount As Integer
                Dim status As Integer = 0
                For Each data As CharInventory In charInv
                    If status = 0 Then
                        Dim itemId2 As String = Nothing
                        For Each data2 As ItemsDB In itemsDB
                            If data2.itemName = ComboBox2.SelectedItem Then
                                itemId2 = data2.itemId
                            End If
                        Next
                        If itemId2 = Nothing Then
                            itemId2 = ComboBox2.SelectedItem
                        End If

                        If data.itemId = itemId2 Then
                            itemId = itemId2
                            itemAmount = data.itemAmount
                            status = 1
                        End If
                    End If

                Next
                If TextBox1.Text <= itemAmount Then
                    Dim charId As Integer

                    For Each data As List(Of CharacterInfo) In charVar
                        For Each data3 As CharacterInfo In data
                            If data3.cha_name = ComboBox1.SelectedItem Then
                                charId = data3.cha_id
                            End If
                        Next
                    Next


                    Dim processData As New AddItem With {
                            .charId = charId,
                            .itemId = itemId,
                            .itemAmount = TextBox1.Text,
                            .stallId = stallId,
                            .itemPrice = TextBox2.Text}
                    Dim postData As String = JsonConvert.SerializeObject(processData)

                    Dim encoding As New UTF8Encoding
                    Dim byteData As Byte() = encoding.GetBytes(postData)

                    Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/addStallItems"), HttpWebRequest)
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
                            Dim index As Integer = ComboBox1.SelectedIndex

                            ComboBox1.Items.Clear()
                            ComboBox1.Text = ""

                            resetChar()

                            getChar()
                            ComboBox1.SelectedIndex = index
                            Button3.Enabled = True
                        Else
                            MsgBox(sessionID)
                            postresponse.Close()
                        End If

                    Catch ex As Exception
                        MsgBox("Failed to connect with server")
                    End Try
                Else
                    MsgBox("Not enough amount in inventory.")
                End If
            Else
                MsgBox("Please select a Quantity and Price greater than 0")
            End If

        Else
            MsgBox("Please select a Item")

        End If

    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim index As Integer = ComboBox3.SelectedIndex
        If (ComboBox4.Items.Count - 1) >= index Then
            ComboBox4.SelectedIndex = index
        End If

        For Each data As StallItems In stallItems
            If data.item_id = ComboBox4.SelectedItem Then
                Label3.Text = data.item_amount
                Label4.Text = data.item_price
                Label3.Enabled = True
                Label4.Enabled = True
            End If
        Next
        Button6.Enabled = True
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        Dim stall_id As Integer
        ComboBox6.Items.Clear()
        ComboBox6.Text = ""
        ComboBox7.Items.Clear()

        For Each data As AllStalls In allStalls
            If data.stall_name = ComboBox5.SelectedItem Then
                stall_id = data.stall_id
            End If
        Next

        Dim processData As New AllStalls With {
                .stall_id = stall_id}
        Dim postData As String = JsonConvert.SerializeObject(processData)

        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/getOwnerStall"), HttpWebRequest)
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
                ownerStall = JsonConvert.DeserializeObject(Of List(Of StallItems))(sessionID)
                For Each data As StallItems In ownerStall
                    Dim itemId As String = Nothing

                    For Each data2 As ItemsDB In itemsDB
                        If data2.itemId = data.item_code Then
                            itemId = data2.itemName
                        End If
                    Next
                    If itemId = Nothing Then
                        itemId = data.item_code

                    End If
                    ComboBox6.Items.Add(itemId)

                    ComboBox7.Items.Add(data.item_id)
                Next
                ComboBox6.Enabled = True

                postresponse.Close()
            Else
                MsgBox(sessionID)
                postresponse.Close()
            End If

        Catch ex As Exception
            MsgBox("Failed to connect with server")
        End Try
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged
        Button4.Enabled = True
        NumericUpDown2.Enabled = True
        Dim index As Integer = ComboBox6.SelectedIndex
        If (ComboBox7.Items.Count - 1) >= index Then
            ComboBox7.SelectedIndex = index
        End If
        For Each data As StallItems In ownerStall
            If data.item_id = ComboBox7.SelectedItem Then

                Label6.Text = data.item_amount
                Label7.Text = data.item_price
                Label6.Enabled = True
                Label7.Enabled = True
            End If
        Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If getAllStalls() = True Then
            For Each data As AllStalls In allStalls
                ComboBox5.Items.Add(data.stall_name)
            Next
            ComboBox5.Enabled = True
            TextBox3.Enabled = True
        Else
            ComboBox5.Enabled = False
            ComboBox5.Items.Clear()
            ComboBox5.Text = ""
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim charId As Integer
        For Each data As List(Of CharacterInfo) In charVar
            For Each data3 As CharacterInfo In data
                If data3.cha_name = ComboBox1.SelectedItem Then
                    charId = data3.cha_id
                End If
            Next
        Next

        Dim processData As New BuyItem With {
                .charId = charId,
                .stallItemsId = ComboBox7.SelectedItem,
                .itemCant = NumericUpDown2.Value}
        Dim postData As String = JsonConvert.SerializeObject(processData)
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/buyItem"), HttpWebRequest)
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
                Dim index As Integer = ComboBox1.SelectedIndex

                postresponse.Close()
                MsgBox(sessionID)
                resetChar()
                ComboBox1.Items.Clear()
                ComboBox1.Text = ""
                Label5.Text = ""
                getChar()
                ComboBox1.SelectedIndex = index
            Else
                MsgBox(sessionID)
                postresponse.Close()
            End If


        Catch ex As Exception
            MsgBox("Failed to connect with server")

        End Try
    End Sub

    Private Sub TextBox3_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox3.KeyDown
        If e.KeyCode = Keys.Enter Then
            ComboBox8.Items.Clear()
            ComboBox8.Text = ""
            ComboBox9.Items.Clear()
            Label8.Text = ""
            Label9.Text = ""
            Label8.Enabled = False
            Label9.Enabled = False
            Button5.Enabled = False
            Dim itemId As String = Nothing
            For Each data As ItemsDB In itemsDB
                If data.itemName = TextBox3.Text Then
                    itemId = data.itemId
                End If
            Next
            If Not itemId = Nothing Then
                Dim processData As New StallItems With {
                .item_code = itemId}
                Dim postData As String = JsonConvert.SerializeObject(processData)
                Dim encoding As New UTF8Encoding
                Dim byteData As Byte() = encoding.GetBytes(postData)

                Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/searchStallItems"), HttpWebRequest)
                postReq.Method = "POST"
                postReq.KeepAlive = False
                postReq.ContentType = "application/json"
                postReq.ContentLength = byteData.Length
                postReq.Timeout = 60000
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
                        itemsFound = JsonConvert.DeserializeObject(Of List(Of StallItems))(sessionID)
                        itemsFound = itemsFound.OrderBy(Function(x) x.item_amount).ToList()
                        For Each item As StallItems In itemsFound
                            Dim itemId2 As String = Nothing
                            For Each data As ItemsDB In itemsDB
                                If data.itemId = item.item_code Then
                                    itemId2 = data.itemName
                                    ComboBox8.Items.Add(itemId2)
                                    ComboBox9.Items.Add(item.item_id)
                                    ComboBox8.Enabled = True
                                End If
                            Next
                            'If itemId2 = Nothing Then
                            'itemId2 = item.item_code
                            'End If



                        Next
                    Else
                        MsgBox(sessionID)
                        ComboBox8.Enabled = False
                        Button5.Enabled = False
                        postresponse.Close()
                    End If
                    TextBox3.Text = ""
                Catch ex As Exception
                    MsgBox("Failed to connect with server")

                End Try
            Else
                MsgBox("The item you are looking for doesnt exists or it's not allowed to be sold")
            End If



        End If
    End Sub

    Private Sub ComboBox8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox8.SelectedIndexChanged
        Button5.Enabled = True
        NumericUpDown1.Enabled = True
        Dim index As Integer = ComboBox8.SelectedIndex
        If (ComboBox9.Items.Count - 1) >= index Then
            ComboBox9.SelectedIndex = index
        End If
        For Each data As StallItems In itemsFound
            If data.item_id = ComboBox9.SelectedItem Then
                Label8.Text = data.item_amount
                Label9.Text = data.item_price
                Label8.Enabled = True
                Label9.Enabled = True
            End If
        Next
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim charId As Integer
        For Each data As List(Of CharacterInfo) In charVar
            For Each data3 As CharacterInfo In data
                If data3.cha_name = ComboBox1.SelectedItem Then
                    charId = data3.cha_id
                End If
            Next
        Next

        Dim processData As New BuyItem With {
                .charId = charId,
                .stallItemsId = ComboBox9.SelectedItem,
                .itemCant = NumericUpDown1.Value}
        Dim postData As String = JsonConvert.SerializeObject(processData)
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/buyItem"), HttpWebRequest)
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
                Dim index As Integer = ComboBox1.SelectedIndex

                postresponse.Close()
                MsgBox(sessionID)
                resetChar()
                ComboBox1.Items.Clear()
                ComboBox1.Text = ""
                Label5.Text = ""
                getChar()
                ComboBox1.SelectedIndex = index

            Else
                MsgBox(sessionID)
                postresponse.Close()
            End If


        Catch ex As Exception
            MsgBox("Failed to connect with server")

        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim charId As Integer
        For Each data As List(Of CharacterInfo) In charVar
            For Each data3 As CharacterInfo In data
                If data3.cha_name = ComboBox1.SelectedItem Then
                    charId = data3.cha_id
                End If
            Next
        Next

        Dim processData As New BuyItem With {
                .charId = charId,
                .stallItemsId = ComboBox4.SelectedItem}
        Dim postData As String = JsonConvert.SerializeObject(processData)
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/removeStallItem"), HttpWebRequest)
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
                Dim index As Integer = ComboBox1.SelectedIndex

                postresponse.Close()
                MsgBox(sessionID)
                resetChar()
                ComboBox1.Items.Clear()
                ComboBox1.Text = ""
                Label5.Text = ""
                getChar()
                ComboBox1.SelectedIndex = index

            Else
                MsgBox(sessionID)
                postresponse.Close()
            End If


        Catch ex As Exception
            MsgBox("Failed to connect with server")

        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim charId As Integer
        For Each data As List(Of CharacterInfo) In charVar
            For Each data3 As CharacterInfo In data
                If data3.cha_name = ComboBox1.SelectedItem Then
                    charId = data3.cha_id
                End If
            Next
        Next

        Dim processData As New Stalls With {
                .charId = charId}
        Dim postData As String = JsonConvert.SerializeObject(processData)
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("http://" & Login.serviceIp & ":" & Login.servicePort & "/redeemStallMoney"), HttpWebRequest)
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
                Dim index As Integer = ComboBox1.SelectedIndex

                postresponse.Close()
                MsgBox(sessionID)
                resetChar()
                ComboBox1.Items.Clear()
                ComboBox1.Text = ""
                Label5.Text = ""
                getChar()
                ComboBox1.SelectedIndex = index

            Else
                MsgBox(sessionID)
                postresponse.Close()
            End If


        Catch ex As Exception
            MsgBox("Failed to connect with server")

        End Try
    End Sub


    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        Register.ShowDialog()


    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Me.Close()
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

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not Login._allowedChars.Contains(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private _allowedChars As String = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789' "
    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not _allowedChars.Contains(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not _allowedNumbers.Contains(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub



    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not _allowedNumbers.Contains(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If
    End Sub


End Class

Public Class Stalls
    Public charId As Integer
    Public charName As String
    Public stallName As String
End Class

Public Class AllStalls
    Public stall_id As Integer
    Public stall_ownerid As Integer
    Public stall_ownername As String
    Public stall_name As String
End Class

Public Class CharInventory
    Public itemId As Integer
    Public itemAmount As Integer
End Class

Public Class AddItem
    Public charId As Integer
    Public stallId As Integer
    Public itemId As Integer
    Public itemAmount As Integer
    Public itemPrice As Integer
End Class

Public Class StallItems
    Public charId As Integer
    Public item_id As Integer
    Public item_code As Integer
    Public item_amount As Integer
    Public item_price As Integer
End Class

Public Class ItemsDB
    Public itemId As Integer
    Public itemName As String
End Class

Public Class BuyItem
    Public charId As Integer
    Public stallItemsId As Integer
    Public itemCant As Integer
End Class