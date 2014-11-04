'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* The form Database_Interface allows the user to open a database connection and
'* to set the accounts/characters to be loaded.
'*
'* Developed by Alcanmage/megasus

Imports System.Globalization
Imports MySql.Data.MySqlClient
Imports System.Threading

Public Class Database_Interface
    Dim armoryproc As New prozedur_armory
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim trinitycore1 As New Trinity_core
    Dim mangoscore As New Mangos_core
    Dim arcemucore As New ArcEmu_core
    Dim reporttext As RichTextBox = Process_Status.processreport
    Dim procstatus As New Process_Status
    Dim writepath As String
    Dim trinitycorecheck As New Core_Check_Trinity
    Dim mangoscorecheck As New Core_Check_Mangos
    Dim arcemucorecheck As New Core_Check_ArcEmu
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN
    Dim runfunction As New Functions

    Public Sub New()
        MyBase.New()

        Thread.CurrentThread.CurrentUICulture = New CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub

    Private Sub Database_Interface_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Starter.Show()
    End Sub

    Private Sub Database_Interface_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximumSize = Me.Size
        auth.Text = My.Settings.realmd
        characters.Text = My.Settings.characters
        runfunction.writelog("Database_Interface_Load call")
        Main.MainInstance.anzahldurchlaufe = 0
        Main.MainInstance.datasets = vbEmpty
        Select Case My.Settings.favcore
            Case 1
                trinity1.Checked = True
            Case 2
                mangos.Checked = True
            Case 3
                arcemu.Checked = True
            Case Else
                trinity1.Checked = True
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        runfunction.writelog("Database_Interface_closing request")
        Starter.Show()
        Me.Close()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click

        address.Text = My.Settings.address
        port.Text = My.Settings.port
        user.Text = My.Settings.user
        password.Text = My.Settings.pass
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        My.Settings.savecontent = ""
        ' ???? 29/07 should fix: (Load char from db > load char from armory > store it > import template = first character)
        My.Settings.realmd = auth.Text
        My.Settings.characters = characters.Text
        My.Settings.Save()
        Main.MainInstance.ServerStringInfo = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" &
                                password.Text & ";Database=information_schema"

        If automatic.Checked = True Then
            runfunction.writelog("Connect request with automatic checked")
            If _
                trytoconnect(
                    "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" &
                    password.Text & ";Database=characters") = False Then
                If _
                    trytoconnect(
                        "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" &
                        password.Text & ";Database=character") = False Then
                    runfunction.writelog("Could not find character db or connection info wrong")
                    If My.Settings.language = "de" Then
                        xlabel.Text = localeDE.armory2database_txt1
                    Else
                        xlabel.Text = localeEN.armory2database_txt1
                    End If

                Else
                    If _
                        trytoconnect(
                            "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" &
                            password.Text & ";Database=realmd") = False Then
                        If _
                            trytoconnect(
                                "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" &
                                password.Text & ";Database=realm") = False Then
                            If _
                                trytoconnect(
                                    "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                    ";Password=" & password.Text & ";Database=auth") = False Then
                                If _
                                    trytoconnect(
                                        "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                        ";Password=" & password.Text & ";Database=logon") = False Then
                                    If _
                                        trytoconnect(
                                            "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                            ";Password=" & password.Text & ";Database=op_realm") = False Then
                                        If _
                                            trytoconnect(
                                                "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                user.Text & ";Password=" & password.Text & ";Database=" & auth.Text) =
                                            False Then
                                            runfunction.writelog("Could find character db but not auth db")
                                            If My.Settings.language = "de" Then
                                                MsgBox(
                                                    localeDE.armory2database_txt2 & vbNewLine &
                                                    localeDE.armory2database_txt3, MsgBoxStyle.Critical,
                                                    localeDE.armory2database_txt3)
                                            Else
                                                MsgBox(
                                                    localeEN.armory2database_txt2 & vbNewLine &
                                                    localeEN.armory2database_txt3, MsgBoxStyle.Critical,
                                                    localeEN.armory2database_txt3)
                                            End If
                                            Exit Sub
                                        Else
                                            Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text &
                                                                      ";User id=" & user.Text & ";Password=" &
                                                                      password.Text & ";Database=" & auth.Text
                                        End If
                                    Else
                                        Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text &
                                                                  ";User id=" & user.Text & ";Password=" & password.Text &
                                                                  ";Database=op_realm"
                                    End If
                                Else
                                    Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text &
                                                              ";User id=" & user.Text & ";Password=" & password.Text &
                                                              ";Database=logon"
                                End If
                            Else
                                Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                          user.Text & ";Password=" & password.Text & ";Database=auth"
                            End If
                        Else
                            Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                      user.Text & ";Password=" & password.Text & ";Database=realm"
                        End If
                    Else
                        Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                  user.Text & ";Password=" & password.Text & ";Database=realmd"
                    End If
                    runfunction.writelog("Could find character db and auth db")
                    Main.MainInstance.ServerString = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                        ";Password=" & password.Text & ";Database=character"
                    Main.MainInstance.characterdbname = "character"
                    Main.MainInstance.ServerStringCheck = Main.MainInstance.ServerString
                    If determinecore() = "arcemu" Then
                        arcemu.Checked = True
                    ElseIf determinecore() = "mangos" Then
                        mangos.Checked = True
                    ElseIf determinecore() = "trinity" Then
                        trinity1.Checked = True
                    ElseIf determinecore() = "none" Then
                        If My.Settings.language = "de" Then
                            MsgBox(localeDE.couldnotdeterminecore, MsgBoxStyle.Critical, localeDE.errornotification)
                        Else
                            MsgBox(localeEN.couldnotdeterminecore, MsgBoxStyle.Critical, localeEN.errornotification)
                        End If
                        Exit Sub
                    End If
                    If My.Settings.language = "de" Then
                        xlabel.Text = localeDE.armory2database_txt5
                    Else
                        xlabel.Text = localeEN.armory2database_txt5
                    End If

                    optionspanel.Location = New Point(0, 0)
                    connectpanel.Location = New Point(4000, 4000)
                    Me.MinimumSize = New Size(0, 0)
                    Me.MaximumSize = New Size(4000, 4000)
                    Me.Size = New Size(618, 657)
                    Me.MaximumSize = Me.Size
                    Me.MinimumSize = Me.Size
                End If
            Else
                If _
                    trytoconnect(
                        "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" &
                        password.Text & ";Database=realmd") = False Then
                    If _
                        trytoconnect(
                            "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" &
                            password.Text & ";Database=realm") = False Then
                        If _
                            trytoconnect(
                                "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" &
                                password.Text & ";Database=auth") = False Then
                            If _
                                trytoconnect(
                                    "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                    ";Password=" & password.Text & ";Database=logon") = False Then
                                If _
                                    trytoconnect(
                                        "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                        ";Password=" & password.Text & ";Database=op_realm") = False Then
                                    If _
                                        trytoconnect(
                                            "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                            ";Password=" & password.Text & ";Database=" & auth.Text) = False Then
                                        If My.Settings.language = "de" Then
                                            MsgBox(
                                                localeDE.armory2database_txt2 & vbNewLine &
                                                localeDE.armory2database_txt3, MsgBoxStyle.Critical,
                                                localeDE.armory2database_txt3)
                                        Else
                                            MsgBox(
                                                localeEN.armory2database_txt2 & vbNewLine &
                                                localeEN.armory2database_txt3, MsgBoxStyle.Critical,
                                                localeEN.armory2database_txt3)
                                        End If
                                        runfunction.writelog("Could find character db but not auth db")
                                        Exit Sub
                                    Else
                                        Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text &
                                                                  ";User id=" & user.Text & ";Password=" & password.Text &
                                                                  ";Database=" & auth.Text
                                    End If
                                Else
                                    Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text &
                                                              ";User id=" & user.Text & ";Password=" & password.Text &
                                                              ";Database=op_realm"
                                End If
                            Else
                                Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                          user.Text & ";Password=" & password.Text & ";Database=logon"
                            End If
                        Else
                            Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                      user.Text & ";Password=" & password.Text & ";Database=auth"
                        End If
                    Else
                        Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                  user.Text & ";Password=" & password.Text & ";Database=realm"
                    End If
                Else
                    Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                              ";Password=" & password.Text & ";Database=realmd"
                End If
                Main.MainInstance.ServerString = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                    ";Password=" & password.Text & ";Database=characters"
                Main.MainInstance.ServerStringCheck = Main.MainInstance.ServerString
                Main.MainInstance.characterdbname = "characters"
                runfunction.writelog("Could find character db and auth db")
                If determinecore() = "arcemu" Then
                    arcemu.Checked = True
                ElseIf determinecore() = "mangos" Then
                    mangos.Checked = True
                ElseIf determinecore() = "trinity" Then
                    trinity1.Checked = True
                ElseIf determinecore() = "none" Then
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.couldnotdeterminecore, MsgBoxStyle.Critical, localeDE.errornotification)
                    Else
                        MsgBox(localeEN.couldnotdeterminecore, MsgBoxStyle.Critical, localeEN.errornotification)
                    End If
                    Exit Sub
                End If
                If My.Settings.language = "de" Then
                    xlabel.Text = localeDE.armory2database_txt5
                Else
                    xlabel.Text = localeEN.armory2database_txt5
                End If

                optionspanel.Location = New Point(0, 0)
                connectpanel.Location = New Point(4000, 4000)
                Me.MinimumSize = New Size(0, 0)
                Me.MaximumSize = New Size(4000, 4000)
                Me.Size = New Size(618, 657)
                Me.MaximumSize = Me.Size
                Me.MinimumSize = Me.Size
            End If
        Else
            runfunction.writelog("Connect request with manually checked")
            If _
                trytoconnect(
                    "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" &
                    password.Text & ";Database=" & characters.Text) = True Then
                runfunction.writelog("Could find character db")
                If _
                    trytoconnect(
                        "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" &
                        password.Text & ";Database=" & auth.Text) = True Then
                    runfunction.writelog("Could find auth db")
                    Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                              ";Password=" & password.Text & ";Database=" & auth.Text
                    Main.MainInstance.ServerString = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                        ";Password=" & password.Text & ";Database=" & characters.Text
                    Main.MainInstance.characterdbname = characters.Text
                    Main.MainInstance.ServerStringCheck = Main.MainInstance.ServerString
                    If determinecore() = "arcemu" Then
                        arcemu.Checked = True
                    ElseIf determinecore() = "mangos" Then
                        mangos.Checked = True
                    ElseIf determinecore() = "trinity" Then
                        trinity1.Checked = True
                    ElseIf determinecore() = "none" Then
                        If My.Settings.language = "de" Then
                            MsgBox(localeDE.couldnotdeterminecore, MsgBoxStyle.Critical, localeDE.errornotification)
                        Else
                            MsgBox(localeEN.couldnotdeterminecore, MsgBoxStyle.Critical, localeEN.errornotification)
                        End If
                        Exit Sub
                    End If
                    If My.Settings.language = "de" Then
                        xlabel.Text = localeDE.armory2database_txt5
                    Else
                        xlabel.Text = localeEN.armory2database_txt5
                    End If

                    optionspanel.Location = New Point(0, 0)
                    connectpanel.Location = New Point(4000, 4000)
                    Me.MinimumSize = New Size(0, 0)
                    Me.MaximumSize = New Size(4000, 4000)
                    Me.Size = New Size(618, 657)
                    Me.MaximumSize = Me.Size
                    Me.MinimumSize = Me.Size
                Else
                    runfunction.writelog("Could find character db but not auth db")
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.armory2database_txt2 & vbNewLine & localeDE.armory2database_txt3,
                               MsgBoxStyle.Critical, localeDE.armory2database_txt3)
                    Else
                        MsgBox(localeEN.armory2database_txt2 & vbNewLine & localeEN.armory2database_txt3,
                               MsgBoxStyle.Critical, localeEN.armory2database_txt3)
                    End If
                    Exit Sub
                End If
            Else
                runfunction.writelog("Could not find character db or login info wrong")
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.armory2database_txt2 & vbNewLine & localeDE.armory2database_txt3,
                           MsgBoxStyle.Critical, localeDE.armory2database_txt3)
                Else
                    MsgBox(localeEN.armory2database_txt2 & vbNewLine & localeEN.armory2database_txt3,
                           MsgBoxStyle.Critical, localeEN.armory2database_txt3)
                End If
                Exit Sub
            End If
        End If
    End Sub
    Private Function determinecore() As String
        If columnexist("ownerguid", "playeritems") = True Then
            'arcemu
            Return "arcemu"
        ElseIf columnexist("item_template", "character_inventory") = True Then
            'mangos
            Return "mangos"
        ElseIf columnexist("quest", "character_queststatus_rewarded") = True Then
            'trinity
            Return "trinity"
        Else
            Return "none"
        End If
    End Function
    Private Function columnexist(ByVal spalte As String, ByVal table As String) As Boolean
        Try
            SQLConnection.Close()

            SQLConnection.Dispose()
        Catch ex As Exception

        End Try

        Dim myAdapter As New MySqlDataAdapter
        SQLConnection.ConnectionString = Main.MainInstance.ServerString
        Dim sqlquery = ("SELECT " & spalte & " FROM " & table)
        Dim myCommand As New MySqlCommand()
        myCommand.Connection = SQLConnection
        myCommand.CommandText = sqlquery

        'start query
        myAdapter.SelectCommand = myCommand
        Dim myData As MySqlDataReader
        Try
            SQLConnection.Close()
            SQLConnection.Dispose()
        Catch ex As Exception

        End Try
        Try
            SQLConnection.Open()
            myData = myCommand.ExecuteReader()
            If CInt(myData.HasRows) = 0 Then
                Return True
            Else
                SQLConnection.Close()

                SQLConnection.Dispose()
                Return True
            End If
        Catch ex As Exception
            SQLConnection.Close()

            SQLConnection.Dispose()
            Return False
        End Try
    End Function
    Private Function trytoconnect(ByVal connectionstring As String) As Boolean
        Try
            SQLConnection.Close()
            SQLConnection.Dispose()
        Catch ex As Exception

        End Try
        SQLConnection.ConnectionString = connectionstring
        Try

            If SQLConnection.State = ConnectionState.Closed Then
                SQLConnection.Open()
                SQLConnection.Close()
                SQLConnection.Dispose()
                Return True




            Else

                SQLConnection.Close()
                SQLConnection.Dispose()
                Return False
            End If
        Catch ex As Exception
            Try
                SQLConnection.Close()
                SQLConnection.Dispose()
            Catch

            End Try
            Return False


        End Try
    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            runfunction.writelog("Checkbox1 checked")
            Button2.Enabled = True
            Button4.Enabled = True
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            levelrangecheck.Enabled = True
            gmlevelcheck.Enabled = True
            lastlogincheck.Enabled = True
        End If
    End Sub

    Private Sub charnames_TextChanged(sender As Object, e As EventArgs) Handles charnames.TextChanged
        Dim sLines() As String = charnames.Lines
        Dim xstring As String
        Dim xcount As Integer = 0
        Dim removecount As Integer = 0
        For i As Integer = 0 To sLines.Length - 1
            xstring = sLines(i)
            If sLines(i) = "" Then
                removecount += 1
            Else

            End If
            xcount = sLines.Length
        Next
        If xcount - removecount = 1 Then
            If CheckBox3.Checked = True Then
                Button2.Enabled = True
                Button4.Enabled = True
                Button3.Enabled = True
            End If
        ElseIf xcount - removecount >= 2 Then
            If CheckBox3.Checked = True Then
                Button2.Enabled = True
                Button4.Enabled = True
                Button3.Enabled = False
            End If
        Else
            If CheckBox3.Checked = True Then
                Button3.Enabled = False
                Button4.Enabled = False
                Button2.Enabled = False
            End If
        End If
    End Sub

    Private Sub accnames_TextChanged(sender As Object, e As EventArgs) Handles accnames.TextChanged
        If CheckBox2.Checked = True Then
            If accnames.Text = "" Then
                Button3.Enabled = False
                Button4.Enabled = False
                Button2.Enabled = False
            Else
                Button2.Enabled = True
                Button4.Enabled = True
                Button3.Enabled = False
            End If
        End If
    End Sub

    Public Sub button4click()
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "// Preparing Transmission!" & vbNewLine)
        Application.DoEvents()
        trinitycore1.opensql()
        If trinity1.Checked = True Then
            If CheckBox1.Checked = True Then
                trinitycore1.getallchars()
            ElseIf CheckBox2.Checked = True Then
                Dim sLines() As String = accnames.Lines
                Dim xstring As String

                Dim removecount As Integer
                For i As Integer = 0 To sLines.Length - 1
                    xstring = sLines(i)
                    If sLines(i) = "" Then
                        removecount += 1
                    Else
                        trinitycore1.getallcharsfromaccount((sLines(i)).ToUpper)
                    End If

                Next
            ElseIf CheckBox3.Checked = True Then
                Dim sLines() As String = charnames.Lines
                Dim xstring As String

                Dim removecount As Integer
                For i As Integer = 0 To sLines.Length - 1
                    xstring = sLines(i)
                    If sLines(i) = "" Then
                        removecount += 1
                    Else
                        trinitycore1.getthischar(sLines(i))
                    End If

                Next
            End If
        ElseIf mangos.Checked = True Then
            If CheckBox1.Checked = True Then
                mangoscore.getallchars()
            ElseIf CheckBox2.Checked = True Then
                Dim sLines() As String = accnames.Lines
                Dim xstring As String

                Dim removecount As Integer
                For i As Integer = 0 To sLines.Length - 1
                    xstring = sLines(i)
                    If sLines(i) = "" Then
                        removecount += 1
                    Else
                        mangoscore.getallcharsfromaccount((sLines(i)).ToUpper)
                    End If

                Next
            ElseIf CheckBox3.Checked = True Then
                Dim sLines() As String = charnames.Lines
                Dim xstring As String

                Dim removecount As Integer
                For i As Integer = 0 To sLines.Length - 1
                    xstring = sLines(i)
                    If sLines(i) = "" Then
                        removecount += 1
                    Else
                        mangoscore.getthischar(sLines(i))
                    End If

                Next
            End If
        ElseIf arcemu.Checked = True Then
            If CheckBox1.Checked = True Then
                arcemucore.getallchars()
            ElseIf CheckBox2.Checked = True Then
                Dim sLines() As String = accnames.Lines
                Dim xstring As String

                Dim removecount As Integer
                For i As Integer = 0 To sLines.Length - 1
                    xstring = sLines(i)
                    If sLines(i) = "" Then
                        removecount += 1
                    Else
                        arcemucore.getallcharsfromaccount((sLines(i)).ToUpper)
                    End If

                Next
            ElseIf CheckBox3.Checked = True Then
                Dim sLines() As String = charnames.Lines
                Dim xstring As String

                Dim removecount As Integer
                For i As Integer = 0 To sLines.Length - 1
                    xstring = sLines(i)
                    If sLines(i) = "" Then
                        removecount += 1
                    Else
                        arcemucore.getthischar(sLines(i))
                    End If

                Next
            End If
        Else

        End If
        Dim ciu As New CIUFile
        trinitycore1.closesql()
        ciu.createfile(writepath)
        Process_Status.UseWaitCursor = False
        Application.DoEvents()
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "// Template File created!" & vbNewLine)
        Process_Status.Button1.Enabled = True
        Starter.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        Dim errortext As String = ""
        If CheckBox2.Checked = True Then
            runfunction.writelog("Accountnames: " & accnames.Text)
            Dim sLines() As String = accnames.Lines
            Dim removecount As Integer
            For i As Integer = 0 To sLines.Length - 1

                If sLines(i) = "" Then
                    removecount += 1
                Else
                    If trinity1.Checked = True Then
                        trinitycore1.opensql()
                        If trinitycore1.accountexist((sLines(i)).ToUpper, Main.MainInstance.ServerStringRealmd) = False Then _
                            errortext = errortext & "Account " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    ElseIf mangos.Checked = True Then
                        trinitycore1.opensql()
                        If mangoscore.accountexist((sLines(i)).ToUpper, Main.MainInstance.ServerStringRealmd) = False Then _
                            errortext = errortext & "Account " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    Else
                        trinitycore1.opensql()
                        If arcemucore.accountexist((sLines(i)).ToUpper, Main.MainInstance.ServerStringRealmd) = False Then _
                            errortext = errortext & "Account " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    End If
                End If

            Next

        ElseIf CheckBox3.Checked = True Then
            runfunction.writelog("Charnames: " & charnames.Text)
            Dim sLines() As String = charnames.Lines
            Dim removecount As Integer
            For i As Integer = 0 To sLines.Length - 1

                If sLines(i) = "" Then
                    removecount += 1
                Else
                    If trinity1.Checked = True Then
                        trinitycore1.opensql()
                        If trinitycore1.characterexist((sLines(i)).ToUpper, Main.MainInstance.ServerString) = False Then _
                            errortext = errortext & "Character " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    ElseIf mangos.Checked = True Then
                        trinitycore1.opensql()
                        If mangoscore.characterexist((sLines(i)).ToUpper, Main.MainInstance.ServerString) = False Then _
                            errortext = errortext & "Character " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    Else
                        trinitycore1.opensql()
                        If arcemucore.characterexist((sLines(i)).ToUpper, Main.MainInstance.ServerString) = False Then _
                            errortext = errortext & "Character " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    End If
                End If

            Next
        Else

        End If
        If Not errortext = "" Then
            MsgBox("The following errors occurred:" & vbNewLine & vbNewLine & errortext, MsgBoxStyle.Critical,
                   "Attention!")
            Exit Sub
        End If
        If levelrangecheck.Checked = True Then
            If levelmin.Text = "" Then
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.minlevelnotset, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.minlevelnotset, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If

                Exit Sub
            End If
            If levelmax.Text = "" Then
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.maxlevelnotset, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.maxlevelnotset, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub
            End If
            Try
                My.Settings.levelrangemax = CInt(levelmax.Text)
            Catch ex As Exception

                If My.Settings.language = "de" Then
                    MsgBox(localeDE.maxlevelnotset, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.maxlevelnotset, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub
            End Try
            Try
                My.Settings.levelrangemin = CInt(levelmin.Text)
            Catch ex As Exception
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.minlevelnotset, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.minlevelnotset, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub
            End Try
            Try
                If My.Settings.levelrangemax < My.Settings.levelrangemin Then
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.maxlevelsmallerminlevel, MsgBoxStyle.Critical, localeDE.invalidinput)
                    Else
                        MsgBox(localeEN.maxlevelsmallerminlevel, MsgBoxStyle.Critical, localeEN.invalidinput)
                    End If
                    Exit Sub
                End If

            Catch ex As Exception
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.maxminlevelerror, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.maxminlevelerror, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub
            End Try

            My.Settings.levelrangeactive = True
            My.Settings.Save()

        End If
        If lastlogincheck.Checked = True Then
            If datemin.Text > datemax.Text Then
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.mindatebiggermaxdate, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.mindatebiggermaxdate, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub
            Else
                My.Settings.lastlogindate1 = datemin.Text
                My.Settings.lastlogindate2 = datemax.Text
                My.Settings.lastloginactive = True
                My.Settings.Save()
            End If
        End If
        If gmlevelcheck.Checked = True Then
            If gmlevel.Text = "" Then
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.gmlevelnotset, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.gmlevelnotset, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub
            End If
            Try
                My.Settings.gmlevel = gmlevel.Text
            Catch ex As Exception
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.gmlevelnotset, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.gmlevelnotset, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub
            End Try
            My.Settings.gmlevelactive = True
            My.Settings.Save()
        End If
        runfunction.writelog("""Save character profiles in template"" request")
        My.Settings.savecontent = ""
        My.Settings.Save()
        If My.Settings.language = "de" Then
            MsgBox(localeDE.nameTemplatePrompt, MsgBoxStyle.Critical, localeDE.attention)
        Else
            MsgBox(localeEN.nameTemplatePrompt, MsgBoxStyle.Critical, localeEN.attention)
        End If
        Dim locOFD As New SaveFileDialog()

        With locOFD
            '  .Filter = "(.ciu)|.ciu"
            .Filter = "CharImport Universal files (*.ciu)|*.ciu|All files (*.*)|*.*"
            If My.Settings.language = "de" Then
                .Title = localeDE.armoryinterface_txt2
            Else
                .Title = localeEN.armoryinterface_txt2
            End If

            .FileName = "Accounts.ciu"

            .DefaultExt = "ciu"

            .CheckPathExists = True

            If (.ShowDialog() = DialogResult.OK) Then

                writepath = .FileName()
            Else
                Exit Sub

            End If
        End With
        If writepath = "" Then
            MsgBox("Ungültiger Dateiname!", MsgBoxStyle.Critical, "Fehler")
            Exit Sub
        End If
        Main.MainInstance.overview = False
        Main.MainInstance.importmode = 3
        Main.MainInstance.progressmode = 3
        Process_Status.Show()
        Process_Status.UseWaitCursor = True
        runfunction.writelog("Corecheck request")
        If trinity1.Checked = True Then
            trinitycorecheck.begincheck(24)
        ElseIf mangos.Checked = True Then

            mangoscorecheck.begincheck(24)
        ElseIf arcemu.Checked = True Then
            arcemucorecheck.begincheck(24)
        Else

        End If
    End Sub

    Public Sub button3click()
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "// Preparing Transmission!" & vbNewLine)
        Application.DoEvents()
        trinitycore1.opensql()
        Dim sLines() As String = charnames.Lines
        Dim xstring As String

        Dim removecount As Integer
        For i As Integer = 0 To sLines.Length - 1
            xstring = sLines(i)
            If sLines(i) = "" Then
                removecount += 1
            Else
                If trinity1.Checked = True Then
                    trinitycore1.getthischar((sLines(i)).ToUpper)
                ElseIf mangos.Checked = True Then
                    mangoscore.getthischar((sLines(i)).ToUpper)
                ElseIf arcemu.Checked = True Then
                    arcemucore.getthischar((sLines(i)).ToUpper)
                Else


                End If

            End If

        Next


        Main.MainInstance.Panel21.Location = New Point(9999, 9999)
        Process_Status.UseWaitCursor = False
        Application.DoEvents()
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "// Finished!" & vbNewLine)

        Process_Status.Button1.Enabled = True
        Starter.Hide()
        Main.MainInstance.UseWaitCursor = False
        trinitycore1.closesql()
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        Dim errortext As String = ""
        If CheckBox2.Checked = True Then
            runfunction.writelog("Accountnames: " & accnames.Text)
            Dim sLines() As String = accnames.Lines
            Dim removecount As Integer
            For i As Integer = 0 To sLines.Length - 1

                If sLines(i) = "" Then
                    removecount += 1
                Else
                    If trinity1.Checked = True Then
                        trinitycore1.opensql()
                        If trinitycore1.accountexist((sLines(i)).ToUpper, Main.MainInstance.ServerStringRealmd) = False Then _
                            errortext = errortext & "Account " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    ElseIf mangos.Checked = True Then
                        trinitycore1.opensql()
                        If mangoscore.accountexist((sLines(i)).ToUpper, Main.MainInstance.ServerStringRealmd) = False Then _
                            errortext = errortext & "Account " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    Else
                        trinitycore1.opensql()
                        If arcemucore.accountexist((sLines(i)).ToUpper, Main.MainInstance.ServerStringRealmd) = False Then _
                            errortext = errortext & "Account " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    End If
                End If

            Next

        ElseIf CheckBox3.Checked = True Then
            runfunction.writelog("Charnames: " & charnames.Text)
            Dim sLines() As String = charnames.Lines
            Dim removecount As Integer
            For i As Integer = 0 To sLines.Length - 1

                If sLines(i) = "" Then
                    removecount += 1
                Else
                    If trinity1.Checked = True Then
                        trinitycore1.opensql()
                        If trinitycore1.characterexist((sLines(i)).ToUpper, Main.MainInstance.ServerString) = False Then _
                            errortext = errortext & "Character " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    ElseIf mangos.Checked = True Then
                        trinitycore1.opensql()
                        If mangoscore.characterexist((sLines(i)).ToUpper, Main.MainInstance.ServerString) = False Then _
                            errortext = errortext & "Character " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    Else
                        trinitycore1.opensql()
                        If arcemucore.characterexist((sLines(i)).ToUpper, Main.MainInstance.ServerString) = False Then _
                            errortext = errortext & "Character " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    End If
                End If

            Next
        Else

        End If
        If Not errortext = "" Then
            MsgBox("The following errors occurred:" & vbNewLine & vbNewLine & errortext, MsgBoxStyle.Critical,
                   "Attention!")
            Exit Sub
        End If
        runfunction.writelog("""character overview"" request")
        Main.MainInstance.setvisible(False)
        Main.MainInstance.setallempty()
        'Main.MainInstance.ServerString = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" & password.Text & ";Database=characters"
        'Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" & password.Text & ";Database=realmd"
        'Main.MainInstance.ServerStringInfo = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" & password.Text & ";Database=information_schema"
        Main.MainInstance.ServerStringCheck = Main.MainInstance.ServerString
        Main.MainInstance.Show()
        Application.DoEvents()

        Main.MainInstance.overview = True
        Main.MainInstance.importmode = 3
        Main.MainInstance.progressmode = 3
        Process_Status.Show()
        Process_Status.UseWaitCursor = True
        If trinity1.Checked = True Then
            trinitycorecheck.begincheck(23)
        ElseIf mangos.Checked = True Then
            mangoscorecheck.begincheck(23)
        ElseIf arcemu.Checked = True Then
            arcemucorecheck.begincheck(23)
        Else

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        Dim errortext As String = ""
        If CheckBox2.Checked = True Then
            runfunction.writelog("Accountnames: " & accnames.Text)
            Dim sLines() As String = accnames.Lines
            Dim removecount As Integer
            For i As Integer = 0 To sLines.Length - 1

                If sLines(i) = "" Then
                    removecount += 1
                Else
                    If trinity1.Checked = True Then
                        trinitycore1.opensql()
                        If trinitycore1.accountexist((sLines(i)).ToUpper, Main.MainInstance.ServerStringRealmd) = False Then _
                            errortext = errortext & "Account " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    ElseIf mangos.Checked = True Then
                        trinitycore1.opensql()
                        If mangoscore.accountexist((sLines(i)).ToUpper, Main.MainInstance.ServerStringRealmd) = False Then _
                            errortext = errortext & "Account " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    Else
                        trinitycore1.opensql()
                        If arcemucore.accountexist((sLines(i)).ToUpper, Main.MainInstance.ServerStringRealmd) = False Then _
                            errortext = errortext & "Account " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    End If
                End If

            Next

        ElseIf CheckBox3.Checked = True Then
            runfunction.writelog("Charnames: " & charnames.Text)
            Dim sLines() As String = charnames.Lines
            Dim removecount As Integer
            For i As Integer = 0 To sLines.Length - 1

                If sLines(i) = "" Then
                    removecount += 1
                Else
                    If trinity1.Checked = True Then
                        trinitycore1.opensql()
                        If trinitycore1.characterexist((sLines(i)).ToUpper, Main.MainInstance.ServerString) = False Then _
                            errortext = errortext & "Character " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    ElseIf mangos.Checked = True Then
                        trinitycore1.opensql()
                        If mangoscore.characterexist((sLines(i)).ToUpper, Main.MainInstance.ServerString) = False Then _
                            errortext = errortext & "Character " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    Else
                        trinitycore1.opensql()
                        If arcemucore.characterexist((sLines(i)).ToUpper, Main.MainInstance.ServerString) = False Then _
                            errortext = errortext & "Character " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    End If
                End If

            Next
        Else

        End If
        If Not errortext = "" Then
            MsgBox("The following errors occurred:" & vbNewLine & vbNewLine & errortext, MsgBoxStyle.Critical,
                   "Attention!")
            Exit Sub
        End If
        If levelrangecheck.Checked = True Then
            If levelmin.Text = "" Then
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.minlevelnotset, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.minlevelnotset, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If

                Exit Sub
            End If
            If levelmax.Text = "" Then
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.maxlevelnotset, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.maxlevelnotset, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub
            End If
            Try
                My.Settings.levelrangemax = CInt(levelmax.Text)
            Catch ex As Exception
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.maxlevelnotset, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.maxlevelnotset, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub

            End Try
            Try
                My.Settings.levelrangemin = CInt(levelmin.Text)
            Catch ex As Exception
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.minlevelnotset, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.minlevelnotset, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If

            End Try
            Try
                If My.Settings.levelrangemax < My.Settings.levelrangemin Then
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.maxlevelsmallerminlevel, MsgBoxStyle.Critical, localeDE.invalidinput)
                    Else
                        MsgBox(localeEN.maxlevelsmallerminlevel, MsgBoxStyle.Critical, localeEN.invalidinput)
                    End If
                End If

            Catch ex As Exception
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.maxminlevelerror, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.maxminlevelerror, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub
            End Try

            My.Settings.levelrangeactive = True
            My.Settings.Save()
            Exit Sub

        End If
        If lastlogincheck.Checked = True Then
            If datemin.Text > datemax.Text Then
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.mindatebiggermaxdate, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.mindatebiggermaxdate, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub
            Else
                My.Settings.lastlogindate1 = datemin.Text
                My.Settings.lastlogindate2 = datemax.Text
                My.Settings.lastloginactive = True
                My.Settings.Save()
            End If
        End If
        If gmlevelcheck.Checked = True Then
            If gmlevel.Text = "" Then
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.gmlevelnotset, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.gmlevelnotset, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub
            End If
            Try
                My.Settings.gmlevel = gmlevel.Text
            Catch ex As Exception
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.gmlevelnotset, MsgBoxStyle.Critical, localeDE.invalidinput)
                Else
                    MsgBox(localeEN.gmlevelnotset, MsgBoxStyle.Critical, localeEN.invalidinput)
                End If
                Exit Sub
            End Try
            My.Settings.gmlevelactive = True
            My.Settings.Save()
        End If
        Main.MainInstance.ServerStringCheck = Main.MainInstance.ServerString
        runfunction.writelog("""Copy characters directly into database"" request")
        Me.Hide()
        runfunction.writelog("Corecheck request")
        If trinity1.Checked = True Then
            trinitycorecheck.begincheck(22)
        ElseIf mangos.Checked = True Then
            mangoscorecheck.begincheck(22)
        ElseIf arcemu.Checked = True Then
            arcemucorecheck.begincheck(22)
        Else

        End If
    End Sub

    Private Sub GroupBox4_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles GroupBox4.Enter
    End Sub

    Private Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click

        runfunction.writelog("Database_Interface_closing call")
        Starter.Show()
        Me.Close()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            runfunction.writelog("Checkbox2 checked")
            CheckBox1.Checked = False
            CheckBox3.Checked = False
            levelrangecheck.Enabled = True
            gmlevelcheck.Enabled = True
            lastlogincheck.Enabled = True
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            runfunction.writelog("Checkbox3 checked")
            CheckBox2.Checked = False
            CheckBox1.Checked = False
            levelrangecheck.Enabled = False
            gmlevelcheck.Enabled = False
            lastlogincheck.Enabled = False
            levelrangecheck.Checked = False
            gmlevelcheck.Checked = False
            lastlogincheck.Checked = False
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        If CheckBox1.Checked = True Then
            Button2.Enabled = True
            Button4.Enabled = True
            Button3.Enabled = False
        ElseIf CheckBox2.Checked = True Then
            If accnames.Text.Length < 2 Then
                Button2.Enabled = False
                Button4.Enabled = False
                Button3.Enabled = False
            Else
                Button2.Enabled = True
                Button4.Enabled = True
                Button3.Enabled = False
            End If

        ElseIf CheckBox3.Checked = True Then
            Dim sLines() As String = charnames.Lines
            Dim xstring As String
            Dim xcount As Integer = 0
            Dim removecount As Integer = 0
            For i As Integer = 0 To sLines.Length - 1
                xstring = sLines(i)
                If sLines(i) = "" Then
                    removecount += 1
                Else

                End If
                xcount = sLines.Length
            Next
            If xcount - removecount = 1 Then
                Button2.Enabled = True
                Button4.Enabled = True
                Button3.Enabled = True
            ElseIf charnames.Text.Length < 2 Then
                Button2.Enabled = False
                Button4.Enabled = False
                Button3.Enabled = False
            Else
                Button2.Enabled = True
                Button4.Enabled = True
                Button3.Enabled = False
            End If

        Else
            Button2.Enabled = False
            Button4.Enabled = False
            Button3.Enabled = False
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As Object, ByVal e As EventArgs)
        If trinity1.Checked = True Then
            trinitycorecheck.begincheck(1)
        ElseIf mangos.Checked = True Then
            mangoscorecheck.begincheck(1)
        ElseIf arcemu.Checked = True Then
            arcemucorecheck.begincheck(1)
        Else

        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button7.Click
        My.Settings.address = address.Text
        My.Settings.port = port.Text
        My.Settings.user = user.Text
        My.Settings.pass = password.Text
        If trinity1.Checked = True Then
            My.Settings.favcore = 1
        ElseIf mangos.Checked = True Then
            My.Settings.favcore = 2
        ElseIf arcemu.Checked = True Then
            My.Settings.favcore = 3
        Else
        End If
        My.Settings.Save()
    End Sub

    Private Sub CoreCheck_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) _
        Handles CoreCheck.LinkClicked
        'Main.MainInstance.ServerString = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" & password.Text & ";Database=characters"
        'Main.MainInstance.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" & password.Text & ";Database=auth"
        'Main.MainInstance.ServerStringInfo = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" & password.Text & ";Database=information_schema"
        Main.MainInstance.ServerStringCheck = Main.MainInstance.ServerString
        If trinity1.Checked = True Then
            trinitycorecheck.begincheck(1)
        ElseIf mangos.Checked = True Then
            mangoscorecheck.begincheck(1)
        ElseIf arcemu.Checked = True Then
            arcemucorecheck.begincheck(1)
        Else

        End If
    End Sub


    Private Sub normalsqlcommand(ByVal command As String, Optional ByVal showerror As Boolean = True)
        Try
            SQLConnection.Close()
            SQLConnection.Dispose()
        Catch ex As Exception

        End Try
        SQLConnection.ConnectionString = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                         ";Password=" & password.Text & ";Database=strawberry"


        Try

            If SQLConnection.State = ConnectionState.Closed Then
                SQLConnection.Open()
                NewUser(command)

            Else
                SQLConnection.Close()

            End If

        Catch ex As Exception
            If ex.ToString.Contains("Duplicate entry ") Then

            Else
                If showerror = True Then _
                    Process_Status.processreport.appendText(
                        Now.TimeOfDay.ToString &
                        "> ERROR WHILE EXECUTING MYSQL COMMAND (MAYBE YOU CAN IGNORE THIS): command is: " & command &
                        "| ErrMsg is:" & ex.ToString & vbNewLine)

            End If

        End Try
    End Sub

    Public Sub NewUser(ByRef SQLStatement As String)

        Dim cmd As MySqlCommand = New MySqlCommand

        With cmd
            .CommandText = SQLStatement
            .CommandType = CommandType.Text
            .Connection = SQLConnection
            .ExecuteNonQuery()


        End With

        SQLConnection.Close()

        SQLConnection.Dispose()
    End Sub

    Private Sub tbc_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tbc.CheckedChanged
        If tbc.Checked = True Then
            runfunction.writelog("tbc checked")
            Main.MainInstance.xpac = 2
            Glyphs.Enabled = False
            classic.Checked = False
            wotlk.Checked = False
            cata.Checked = False
        End If
    End Sub

    Private Sub classic_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles classic.CheckedChanged
        If classic.Checked = True Then
            runfunction.writelog("classic checked")
            Main.MainInstance.xpac = 1
            Glyphs.Enabled = False
            tbc.Checked = False
            wotlk.Checked = False
            cata.Checked = False
        End If
    End Sub

    Private Sub wotlk_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles wotlk.CheckedChanged
        If wotlk.Checked = True Then
            runfunction.writelog("wotlk checked")
            Main.MainInstance.xpac = 3
            Glyphs.Enabled = True
            tbc.Checked = False
            classic.Checked = False
            cata.Checked = False
        End If
    End Sub

    Private Sub cata_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cata.CheckedChanged
        If cata.Checked = True Then
            runfunction.writelog("cata checked")
            Main.MainInstance.xpac = 4
            Glyphs.Enabled = True
            tbc.Checked = False
            wotlk.Checked = False
            classic.Checked = False
        End If
    End Sub

    Private Sub mangos_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles mangos.CheckedChanged
        If mangos.Checked = True Then runfunction.writelog("Mangos checked")
    End Sub

    Private Sub arcemu_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles arcemu.CheckedChanged
        If arcemu.Checked = True Then runfunction.writelog("ArcEmu checked")
    End Sub

    Private Sub trinity1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles trinity1.CheckedChanged
        If trinity1.Checked = True Then runfunction.writelog("Trinity checked")
    End Sub
End Class