'Copyright (C) 2011-2012 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* The form Database2Database allows the user to open a database connection,
'* to set the destination account/character and to select the properties
'* that are to be transferred.
'*
'* Developed by Alcanmage/megasus

Imports System.Globalization
Imports MySql.Data.MySqlClient
Imports System.Threading

Public Class Database2Database
    Dim armoryproc As New prozedur_armory
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim trinitycore1 As New Trinity_core
    Dim mangoscore As New Mangos_core
    Dim arcemucore As New ArcEmu_core
    Dim reporttext As RichTextBox = Process_Status.processreport
    Dim cui As New CIUFile
    Dim counter As Integer = 0
    Dim xnumber As Integer = 0
    Dim xpansion As String = ""
    Dim xstring As String = ""
    Dim procstatus As New Process_Status
    Dim alternatestring As String
    Dim alternaterealmdstring As String
    Dim trinitycorecheck As New Core_Check_Trinity
    Dim mangoscorecheck As New Core_Check_Mangos
    Dim arcemucorecheck As New Core_Check_ArcEmu
    Dim runfunction As New Functions
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN

    Public Sub New()
        MyBase.New()

        Thread.CurrentThread.CurrentUICulture = New CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click


        My.Settings.realmd = auth.Text
        My.Settings.characters = characters.Text
        My.Settings.Save()
        Main.ServerStringInfo = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text & ";Password=" &
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
                                            alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text &
                                                                    ";User id=" & user.Text & ";Password=" &
                                                                    password.Text & ";Database=" & auth.Text
                                        End If
                                    Else
                                        alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text &
                                                                ";User id=" & user.Text & ";Password=" & password.Text &
                                                                ";Database=op_realm"
                                    End If
                                Else
                                    alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text &
                                                            ";User id=" & user.Text & ";Password=" & password.Text &
                                                            ";Database=logon"
                                End If
                            Else
                                alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                        user.Text & ";Password=" & password.Text & ";Database=auth"
                            End If
                        Else
                            alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                    user.Text & ";Password=" & password.Text & ";Database=realm"
                        End If
                    Else
                        alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                user.Text & ";Password=" & password.Text & ";Database=realmd"
                    End If
                    runfunction.writelog("Could find character db and auth db")
                    alternatestring = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                      ";Password=" & password.Text & ";Database=character"
                    Main.characterdbname = "character"
                    Main.ServerStringCheck = alternatestring
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
                    Me.MaximumSize = New Size(5000, 5000)
                    Me.Size = New Size(676, 598)
                    optionspanel.Location = New Point(0, 0)
                    connectpanel.Location = New Point(4000, 4000)
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
                                        alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text &
                                                                ";User id=" & user.Text & ";Password=" & password.Text &
                                                                ";Database=" & auth.Text
                                    End If
                                Else
                                    alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text &
                                                            ";User id=" & user.Text & ";Password=" & password.Text &
                                                            ";Database=op_realm"
                                End If
                            Else
                                alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                        user.Text & ";Password=" & password.Text & ";Database=logon"
                            End If
                        Else
                            alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                    user.Text & ";Password=" & password.Text & ";Database=auth"
                        End If
                    Else
                        alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                user.Text & ";Password=" & password.Text & ";Database=realm"
                    End If
                Else
                    alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                            ";Password=" & password.Text & ";Database=realmd"
                End If
                alternatestring = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                  ";Password=" & password.Text & ";Database=characters"
                Main.ServerStringCheck = alternatestring
                Main.characterdbname = "characters"
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
                Me.MaximumSize = New Size(5000, 5000)
                Me.Size = New Size(676, 598)
                optionspanel.Location = New Point(0, 0)
                connectpanel.Location = New Point(4000, 4000)
            End If
        Else
            runfunction.writelog("Connect request and 'manually' checked")
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
                    alternaterealmdstring = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                            ";Password=" & password.Text & ";Database=" & auth.Text
                    alternatestring = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                      ";Password=" & password.Text & ";Database=" & characters.Text
                    Main.characterdbname = characters.Text
                    Main.ServerStringCheck = alternatestring
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
                    Me.MaximumSize = New Size(5000, 5000)
                    Me.Size = New Size(676, 598)
                    optionspanel.Location = New Point(0, 0)
                    connectpanel.Location = New Point(4000, 4000)
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
        SQLConnection.ConnectionString = Main.ServerString
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

    Private Sub Button13_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button13.Click
        address.Text = My.Settings.address
        port.Text = My.Settings.port
        user.Text = My.Settings.user
        password.Text = My.Settings.pass
    End Sub

    Private Sub Database2Database_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        runfunction.writelog("Database2Database_Load call")
        Me.MaximumSize = Me.Size
        auth.Text = My.Settings.realmd
        characters.Text = My.Settings.characters
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
        Me.BringToFront()
    End Sub

    Private Sub connectpanel_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles connectpanel.Paint
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        runfunction.writelog("Database2Database_closing call")
        Me.Close()
        Starter.Show()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    End Sub


    Private Sub male_CheckedChanged(sender As Object, e As EventArgs) Handles male.CheckedChanged
        If male.Checked = True Then
            female.Checked = False
            genderstay.Checked = False
        End If
    End Sub

    Private Sub female_CheckedChanged(sender As Object, e As EventArgs) Handles female.CheckedChanged
        If female.Checked = True Then
            male.Checked = False
            genderstay.Checked = False
        End If
    End Sub

    Private Sub tbc_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tbc.CheckedChanged
        If tbc.Checked = True Then
            runfunction.writelog("tbc checked")
            Main.xpac = 2
            glyphs.Enabled = False
            classic.Checked = False
            wotlk.Checked = False
            cata.Checked = False
        End If
    End Sub

    Private Sub classic_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles classic.CheckedChanged
        If classic.Checked = True Then
            runfunction.writelog("classic checked")
            Main.xpac = 1
            glyphs.Enabled = False
            tbc.Checked = False
            wotlk.Checked = False
            cata.Checked = False
        End If
    End Sub

    Private Sub wotlk_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles wotlk.CheckedChanged
        If wotlk.Checked = True Then
            runfunction.writelog("wotlk checked")
            Main.xpac = 3
            glyphs.Enabled = True
            tbc.Checked = False
            classic.Checked = False
            cata.Checked = False
        End If
    End Sub

    Private Sub cata_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cata.CheckedChanged
        If cata.Checked = True Then
            runfunction.writelog("cata checked")
            Main.xpac = 4
            glyphs.Enabled = True
            tbc.Checked = False
            wotlk.Checked = False
            classic.Checked = False
        End If
    End Sub

    Public Sub button4click()
        trinitycore1.opensql()
        If trinity1.Checked = True Then

            Dim xpacressource As String
            Dim xpacressource2 As String
            Select Case Main.xpac
                Case 3
                    xpacressource = My.Resources.GEM_ID_wotlk
                    xpacressource2 = My.Resources.VZ_ID_wotlk
                Case 4
                    xpacressource = My.Resources.GEM_ID_cata
                    xpacressource2 = My.Resources.VZ_ID_cata
                Case Else
                    xpacressource = My.Resources.GEM_ID_wotlk
                    xpacressource2 = My.Resources.VZ_ID_wotlk
            End Select
            trinitycore1.spellgemtext = xpacressource
            trinitycore1.spellitemtext = xpacressource2
            Main.outputcore = "trinity1"

            If CheckBox2.Checked = True Then
                If Main.ausgangsformat = 1 Then

                    If Database_Interface.CheckBox1.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            trinitycore1.getallchars()
                        ElseIf Database_Interface.mangos.Checked = True Then
                            mangoscore.getallchars()
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            arcemucore.getallchars()
                        Else

                        End If

                    ElseIf Database_Interface.CheckBox2.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            Dim sLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1

                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    trinitycore1.getallcharsfromaccount((sLines(i)).ToUpper)
                                End If

                            Next
                        ElseIf Database_Interface.mangos.Checked = True Then
                            Dim sLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1

                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    mangoscore.getallcharsfromaccount((sLines(i)).ToUpper)
                                End If

                            Next
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            Dim sLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1

                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    arcemucore.getallcharsfromaccount((sLines(i)).ToUpper)
                                End If

                            Next
                        Else

                        End If


                    ElseIf Database_Interface.CheckBox3.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            Dim sLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1
                                xstring = sLines(i)
                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    trinitycore1.getthischar(sLines(i))
                                End If

                            Next
                        ElseIf Database_Interface.mangos.Checked = True Then
                            Dim sLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1
                                xstring = sLines(i)
                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    mangoscore.getthischar(sLines(i))
                                End If

                            Next
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            Dim sLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1
                                xstring = sLines(i)
                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    arcemucore.getthischar(sLines(i))
                                End If

                            Next
                        Else

                        End If

                    End If
                    trinitycore1.closesql()
                    Main.ServerString = alternatestring
                    Main.ServerStringRealmd = alternaterealmdstring
                    trinitycore1.opensql()
                    Do
                        counter += 1
                        cui.readtempdataset(counter)

                        Application.DoEvents()
                        Dim newid As String =
                                (CInt(
                                    Val(runcommandRealmd("SELECT id FROM account WHERE id=(SELECT MAX(id) FROM account)",
                                                         "id"))) + 1).ToString
                        trinitycore1.create_new_account_if_not_exist(Main.accountname,
                                                                     "INSERT INTO account ( `id`, `username`, `sha_pass_hash`, `sessionkey`, `v`, `s`, `email`, `joindate`, `expansion`, locale ) VALUES ( '" &
                                                                     newid & "', '" & Main.accountname & "', '" &
                                                                     Main.sha_pass_hash & "', '" & Main.sessionkey &
                                                                     "', '" & Main.account_v & "', '" & Main.account_s &
                                                                     "', '" & Main.email & "', '" & Main.joindate &
                                                                     "', '" & Main.expansion & "', '" & Main.locale &
                                                                     "' )", newid)
                        trinitycore1.adddetailedchar(Main.accountname, Main.char_name, namechange1.Checked)
                        If items.Checked = True Then trinitycore1.additems()
                        If enchantments.Checked = True Then trinitycore1.addench()
                        If glyphs.Checked = True Then trinitycore1.addglyphs(xpansion)
                        If talents.Checked = True Then trinitycore1.addtalents()
                        If male.Checked = True Then trinitycore1.setgender("0")
                        If female.Checked = True Then trinitycore1.setgender("1")
                        If genderstay.Checked = True Then trinitycore1.setgender(Main.char_gender.ToString)
                        If level.Checked = True Then trinitycore1.setlevel()
                        If alternatelevellabel.Checked = True Then _
                            trinitycore1.setalternatelevel(alternateleveltext.Text)
                        If race.Checked = True Then trinitycore1.setrace()
                        If playerclass.Checked = True Then trinitycore1.setclass()
                        If goldlabel.Checked = True Then trinitycore1.setgold(goldtext.Text)
                        If erfolge.Checked = True Then trinitycore1.addachievements()
                        If skills.Checked = True Then trinitycore1.addskills()
                        If zauber.Checked = True Then trinitycore1.addspells()
                        If pvp.Checked = True Then trinitycore1.addpvp()
                        If ruf.Checked = True Then trinitycore1.addreputation()
                        If inventar.Checked = True Then trinitycore1.addinventory()
                        If gold.Checked = True Then trinitycore1.addgold(Main.player_money)
                        reporttext.AppendText(Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    Loop Until counter = Main.anzahldurchlaufe

                Else
                    trinitycore1.closesql()
                    Main.ServerString = alternatestring
                    Main.ServerStringRealmd = alternaterealmdstring
                    trinitycore1.opensql()
                    Do
                        Dim ciu As New CIUFile
                        ciu.readspecial(Main.cuisets)
                        Main.cuisets -= 1


                        Dim newid As String =
                                (CInt(
                                    Val(runcommandRealmd("SELECT id FROM account WHERE id=(SELECT MAX(id) FROM account)",
                                                         "id"))) + 1).ToString
                        trinitycore1.create_new_account_if_not_exist(Main.accountname,
                                                                     "INSERT INTO account ( `id`, `username`, `sha_pass_hash`, `sessionkey`, `v`, `s`, `email`, `joindate`, `expansion`, locale ) VALUES ( '" &
                                                                     newid & "', '" & Main.accountname & "', '" &
                                                                     Main.sha_pass_hash & "', '" & Main.sessionkey &
                                                                     "', '" & Main.account_v & "', '" & Main.account_s &
                                                                     "', '" & Main.email & "', '" & Main.joindate &
                                                                     "', '" & Main.expansion & "', '" & Main.locale &
                                                                     "' )", newid)

                        trinitycore1.adddetailedchar(Main.accountname, Main.char_name, namechange1.Checked)

                        '  trinitycore1.updatechars(sLines(i), Main.char_name, namechange2.Checked)
                        If items.Checked = True Then trinitycore1.additems()
                        If enchantments.Checked = True Then trinitycore1.addench()
                        If glyphs.Checked = True Then trinitycore1.addglyphs(xpansion)
                        If talents.Checked = True Then trinitycore1.addtalents()
                        If male.Checked = True Then trinitycore1.setgender("0")
                        If female.Checked = True Then trinitycore1.setgender("1")
                        If genderstay.Checked = True Then trinitycore1.setgender(Main.char_gender.ToString)
                        If level.Checked = True Then trinitycore1.setlevel()
                        If alternatelevellabel.Checked = True Then _
                            trinitycore1.setalternatelevel(alternateleveltext.Text)
                        If race.Checked = True Then trinitycore1.setrace()
                        If playerclass.Checked = True Then trinitycore1.setclass()
                        If goldlabel.Checked = True Then trinitycore1.setgold(goldtext.Text)
                        If erfolge.Checked = True Then trinitycore1.addachievements()
                        If skills.Checked = True Then trinitycore1.addskills()
                        If zauber.Checked = True Then trinitycore1.addspells()
                        If pvp.Checked = True Then trinitycore1.addpvp()
                        If ruf.Checked = True Then trinitycore1.addreputation()
                        If inventar.Checked = True Then trinitycore1.addinventory()
                        If gold.Checked = True Then trinitycore1.addgold(Main.player_money)
                        reporttext.AppendText(Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)


                    Loop Until Main.cuisets = 0
                End If
            End If
            If CheckBox3.Checked = True Then
                If Main.ausgangsformat = 1 Then
                    If Database_Interface.CheckBox1.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            trinitycore1.getallchars()
                        ElseIf Database_Interface.mangos.Checked = True Then
                            mangoscore.getallchars()
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            arcemucore.getallchars()
                        Else

                        End If

                    ElseIf Database_Interface.CheckBox2.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            Dim ssLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1

                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    trinitycore1.getallcharsfromaccount((ssLines(i)).ToUpper)
                                End If

                            Next
                        ElseIf Database_Interface.mangos.Checked = True Then
                            Dim ssLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1

                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    mangoscore.getallcharsfromaccount((ssLines(i)).ToUpper)
                                End If

                            Next
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            Dim ssLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1

                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    arcemucore.getallcharsfromaccount((ssLines(i)).ToUpper)
                                End If

                            Next

                        Else

                        End If


                    ElseIf Database_Interface.CheckBox3.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            Dim ssLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1
                                xstring = ssLines(i)
                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    trinitycore1.getthischar(ssLines(i))
                                End If

                            Next
                        ElseIf Database_Interface.mangos.Checked = True Then
                            Dim ssLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1
                                xstring = ssLines(i)
                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    mangoscore.getthischar(ssLines(i))
                                End If

                            Next
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            Dim ssLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1
                                xstring = ssLines(i)
                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    arcemucore.getthischar(ssLines(i))
                                End If

                            Next
                        Else

                        End If


                    End If
                    trinitycore1.closesql()
                    Main.ServerString = alternatestring
                    Main.ServerStringRealmd = alternaterealmdstring
                    trinitycore1.opensql()
                    Dim sLines() As String = accnames.Lines
                    For i As Integer = 0 To sLines.Length - 1
                        xstring = sLines(i)
                        If sLines(i) = "" Then
                        Else
                            Do
                                counter += 1
                                cui.readtempdataset(counter)

                                Application.DoEvents()
                                trinitycore1.adddetailedchar(sLines(i), Main.char_name, namechange1.Checked)
                                If items.Checked = True Then trinitycore1.additems()
                                If enchantments.Checked = True Then trinitycore1.addench()
                                If glyphs.Checked = True Then trinitycore1.addglyphs(xpansion)
                                If talents.Checked = True Then trinitycore1.addtalents()
                                If male.Checked = True Then trinitycore1.setgender("0")
                                If female.Checked = True Then trinitycore1.setgender("1")
                                If genderstay.Checked = True Then trinitycore1.setgender(Main.char_gender.ToString)
                                If level.Checked = True Then trinitycore1.setlevel()
                                If alternatelevellabel.Checked = True Then _
                                    trinitycore1.setalternatelevel(alternateleveltext.Text)
                                If race.Checked = True Then trinitycore1.setrace()
                                If playerclass.Checked = True Then trinitycore1.setclass()
                                If goldlabel.Checked = True Then trinitycore1.setgold(goldtext.Text)
                                If erfolge.Checked = True Then trinitycore1.addachievements()
                                If skills.Checked = True Then trinitycore1.addskills()
                                If zauber.Checked = True Then trinitycore1.addspells()
                                If pvp.Checked = True Then trinitycore1.addpvp()
                                If ruf.Checked = True Then trinitycore1.addreputation()
                                If inventar.Checked = True Then trinitycore1.addinventory()
                                If gold.Checked = True Then trinitycore1.addgold(Main.player_money)
                                reporttext.AppendText(Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                            Loop Until counter = Main.anzahldurchlaufe
                        End If

                    Next
                Else
                    trinitycore1.closesql()
                    Main.ServerString = alternatestring
                    Main.ServerStringRealmd = alternaterealmdstring
                    trinitycore1.opensql()
                    Dim sLines() As String = accnames.Lines
                    For i As Integer = 0 To sLines.Length - 1
                        xstring = sLines(i)
                        If sLines(i) = "" Then
                        Else
                            Do
                                Dim ciu As New CIUFile
                                ciu.readspecial(Main.cuisets)
                                Main.cuisets -= 1


                                trinitycore1.adddetailedchar(sLines(i), Main.char_name, namechange1.Checked)

                                '  trinitycore1.updatechars(sLines(i), Main.char_name, namechange2.Checked)
                                If items.Checked = True Then trinitycore1.additems()
                                If enchantments.Checked = True Then trinitycore1.addench()
                                If glyphs.Checked = True Then trinitycore1.addglyphs(xpansion)
                                If talents.Checked = True Then trinitycore1.addtalents()
                                If male.Checked = True Then trinitycore1.setgender("0")
                                If female.Checked = True Then trinitycore1.setgender("1")
                                If genderstay.Checked = True Then trinitycore1.setgender(Main.char_gender.ToString)
                                If level.Checked = True Then trinitycore1.setlevel()
                                If alternatelevellabel.Checked = True Then _
                                    trinitycore1.setalternatelevel(alternateleveltext.Text)
                                If race.Checked = True Then trinitycore1.setrace()
                                If playerclass.Checked = True Then trinitycore1.setclass()
                                If goldlabel.Checked = True Then trinitycore1.setgold(goldtext.Text)
                                If erfolge.Checked = True Then trinitycore1.addachievements()
                                If skills.Checked = True Then trinitycore1.addskills()
                                If zauber.Checked = True Then trinitycore1.addspells()
                                If pvp.Checked = True Then trinitycore1.addpvp()
                                If ruf.Checked = True Then trinitycore1.addreputation()
                                If inventar.Checked = True Then trinitycore1.addinventory()
                                If gold.Checked = True Then trinitycore1.addgold(Main.player_money)
                                reporttext.AppendText(Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)


                            Loop Until Main.cuisets = 0
                        End If
                    Next
                End If
            End If


        ElseIf mangos.Checked = True Then
            Main.outputcore = "mangos"
            Dim xpacressource As String
            Dim xpacressource2 As String
            Select Case Main.xpac
                Case 3
                    xpacressource = My.Resources.GEM_ID_wotlk
                    xpacressource2 = My.Resources.VZ_ID_wotlk
                Case 4
                    xpacressource = My.Resources.GEM_ID_cata
                    xpacressource2 = My.Resources.VZ_ID_cata
                Case Else
                    xpacressource = My.Resources.GEM_ID_wotlk
                    xpacressource2 = My.Resources.VZ_ID_wotlk
            End Select
            mangoscore.spellgemtext = xpacressource
            mangoscore.spellitemtext = xpacressource2


            If CheckBox2.Checked = True Then
                If Main.ausgangsformat = 1 Then

                    If Database_Interface.CheckBox1.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            trinitycore1.getallchars()
                        ElseIf Database_Interface.mangos.Checked = True Then
                            mangoscore.getallchars()
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            arcemucore.getallchars()
                        Else

                        End If

                    ElseIf Database_Interface.CheckBox2.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            Dim sLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1

                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    trinitycore1.getallcharsfromaccount((sLines(i)).ToUpper)
                                End If

                            Next
                        ElseIf Database_Interface.mangos.Checked = True Then
                            Dim sLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1

                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    mangoscore.getallcharsfromaccount((sLines(i)).ToUpper)
                                End If

                            Next
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            Dim sLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1

                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    arcemucore.getallcharsfromaccount((sLines(i)).ToUpper)
                                End If

                            Next
                        Else

                        End If


                    ElseIf Database_Interface.CheckBox3.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            Dim sLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1
                                xstring = sLines(i)
                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    trinitycore1.getthischar(sLines(i))
                                End If

                            Next
                        ElseIf Database_Interface.mangos.Checked = True Then
                            Dim sLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1
                                xstring = sLines(i)
                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    mangoscore.getthischar(sLines(i))
                                End If

                            Next
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            Dim sLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1
                                xstring = sLines(i)
                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    arcemucore.getthischar(sLines(i))
                                End If

                            Next
                        Else

                        End If

                    End If
                    trinitycore1.closesql()
                    Main.ServerString = alternatestring
                    Main.ServerStringRealmd = alternaterealmdstring
                    trinitycore1.opensql()
                    Do
                        counter += 1
                        cui.readtempdataset(counter)

                        Application.DoEvents()
                        Dim newid As String =
                                (CInt(
                                    Val(runcommandRealmd("SELECT id FROM account WHERE id=(SELECT MAX(id) FROM account)",
                                                         "id"))) + 1).ToString
                        mangoscore.create_new_account_if_not_exist(Main.accountname,
                                                                   "INSERT INTO account ( `id`, `username`, gmlevel,`sha_pass_hash`, `sessionkey`, `v`, `s`, `email`, `joindate`, active_realm_id, `expansion`, locale ) VALUES ( '" &
                                                                   newid & "', '" & Main.accountname & "', '" &
                                                                   Main.account_access_gmlevel.ToString & "', '" &
                                                                   Main.sha_pass_hash & "', '" & Main.sessionkey &
                                                                   "', '" & Main.account_v & "', '" & Main.account_s &
                                                                   "', '" & Main.email & "', '" & Main.joindate & "', '" &
                                                                   Main.account_access_RealmID & "', '" & Main.expansion &
                                                                   "', '" & Main.locale & "' )", newid)
                        mangoscore.adddetailedchar(Main.accountname, Main.char_name, namechange1.Checked)
                        If items.Checked = True Then mangoscore.additems()
                        If enchantments.Checked = True Then mangoscore.addench()
                        If glyphs.Checked = True Then mangoscore.addglyphs(xpansion)
                        If talents.Checked = True Then mangoscore.addtalents()
                        If male.Checked = True Then mangoscore.setgender("0")
                        If female.Checked = True Then mangoscore.setgender("1")
                        If genderstay.Checked = True Then mangoscore.setgender(Main.char_gender.ToString)
                        If level.Checked = True Then mangoscore.setlevel()
                        If alternatelevellabel.Checked = True Then mangoscore.setalternatelevel(alternateleveltext.Text)
                        If race.Checked = True Then mangoscore.setrace()
                        If playerclass.Checked = True Then mangoscore.setclass()
                        If goldlabel.Checked = True Then mangoscore.setgold(goldtext.Text)
                        If erfolge.Checked = True Then mangoscore.addachievements()
                        If skills.Checked = True Then mangoscore.addskills()
                        If zauber.Checked = True Then mangoscore.addspells()
                        If pvp.Checked = True Then mangoscore.addpvp()
                        If ruf.Checked = True Then mangoscore.addreputation()
                        If inventar.Checked = True Then mangoscore.addinventory()
                        If gold.Checked = True Then mangoscore.addgold(Main.player_money)
                        reporttext.AppendText(Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    Loop Until counter = Main.anzahldurchlaufe

                Else
                    trinitycore1.closesql()
                    Main.ServerString = alternatestring
                    Main.ServerStringRealmd = alternaterealmdstring
                    trinitycore1.opensql()
                    Do
                        Dim ciu As New CIUFile
                        ciu.readspecial(Main.cuisets)
                        Main.cuisets -= 1


                        Dim newid As String =
                                (CInt(
                                    Val(runcommandRealmd("SELECT id FROM account WHERE id=(SELECT MAX(id) FROM account)",
                                                         "id"))) + 1).ToString
                        mangoscore.create_new_account_if_not_exist(Main.accountname,
                                                                   "INSERT INTO account ( `id`, `username`, gmlevel,`sha_pass_hash`, `sessionkey`, `v`, `s`, `email`, `joindate`, active_realm_id, `expansion`, locale ) VALUES ( '" &
                                                                   newid & "', '" & Main.accountname & "', '" &
                                                                   Main.account_access_gmlevel.ToString & "', '" &
                                                                   Main.sha_pass_hash & "', '" & Main.sessionkey &
                                                                   "', '" & Main.account_v & "', '" & Main.account_s &
                                                                   "', '" & Main.email & "', '" & Main.joindate & "', '" &
                                                                   Main.account_access_RealmID & "', '" & Main.expansion &
                                                                   "', '" & Main.locale & "' )", newid)

                        mangoscore.adddetailedchar(Main.accountname, Main.char_name, namechange1.Checked)

                        '  mangoscore.updatechars(sLines(i), Main.char_name, namechange2.Checked)
                        If items.Checked = True Then mangoscore.additems()
                        If enchantments.Checked = True Then mangoscore.addench()
                        If glyphs.Checked = True Then mangoscore.addglyphs(xpansion)
                        If talents.Checked = True Then mangoscore.addtalents()
                        If male.Checked = True Then mangoscore.setgender("0")
                        If female.Checked = True Then mangoscore.setgender("1")
                        If genderstay.Checked = True Then mangoscore.setgender(Main.char_gender.ToString)
                        If level.Checked = True Then mangoscore.setlevel()
                        If alternatelevellabel.Checked = True Then mangoscore.setalternatelevel(alternateleveltext.Text)
                        If race.Checked = True Then mangoscore.setrace()
                        If playerclass.Checked = True Then mangoscore.setclass()
                        If goldlabel.Checked = True Then mangoscore.setgold(goldtext.Text)
                        If erfolge.Checked = True Then mangoscore.addachievements()
                        If skills.Checked = True Then mangoscore.addskills()
                        If zauber.Checked = True Then mangoscore.addspells()
                        If pvp.Checked = True Then mangoscore.addpvp()
                        If ruf.Checked = True Then mangoscore.addreputation()
                        If inventar.Checked = True Then mangoscore.addinventory()
                        If gold.Checked = True Then mangoscore.addgold(Main.player_money)
                        reporttext.AppendText(Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)


                    Loop Until Main.cuisets = 0
                End If
            End If
            If CheckBox3.Checked = True Then
                If Main.ausgangsformat = 1 Then
                    If Database_Interface.CheckBox1.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            trinitycore1.getallchars()
                        ElseIf Database_Interface.mangos.Checked = True Then
                            mangoscore.getallchars()
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            arcemucore.getallchars()
                        Else

                        End If

                    ElseIf Database_Interface.CheckBox2.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            Dim ssLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1

                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    trinitycore1.getallcharsfromaccount((ssLines(i)).ToUpper)
                                End If

                            Next
                        ElseIf Database_Interface.mangos.Checked = True Then
                            Dim ssLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1

                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    mangoscore.getallcharsfromaccount((ssLines(i)).ToUpper)
                                End If

                            Next
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            Dim ssLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1

                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    arcemucore.getallcharsfromaccount((ssLines(i)).ToUpper)
                                End If

                            Next
                        Else

                        End If


                    ElseIf Database_Interface.CheckBox3.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            Dim ssLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1
                                xstring = ssLines(i)
                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    trinitycore1.getthischar(ssLines(i))
                                End If

                            Next
                        ElseIf Database_Interface.mangos.Checked = True Then
                            Dim ssLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1
                                xstring = ssLines(i)
                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    mangoscore.getthischar(ssLines(i))
                                End If

                            Next
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            Dim ssLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1
                                xstring = ssLines(i)
                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    arcemucore.getthischar(ssLines(i))
                                End If

                            Next
                        Else

                        End If


                    End If
                    trinitycore1.closesql()
                    Main.ServerString = alternatestring
                    Main.ServerStringRealmd = alternaterealmdstring
                    trinitycore1.opensql()
                    Dim sLines() As String = accnames.Lines
                    For i As Integer = 0 To sLines.Length - 1
                        xstring = sLines(i)
                        If sLines(i) = "" Then
                        Else
                            Do
                                counter += 1
                                cui.readtempdataset(counter)

                                Application.DoEvents()
                                mangoscore.adddetailedchar(sLines(i), Main.char_name, namechange1.Checked)
                                If items.Checked = True Then mangoscore.additems()
                                If enchantments.Checked = True Then mangoscore.addench()
                                If glyphs.Checked = True Then mangoscore.addglyphs(xpansion)
                                If talents.Checked = True Then mangoscore.addtalents()
                                If male.Checked = True Then mangoscore.setgender("0")
                                If female.Checked = True Then mangoscore.setgender("1")
                                If genderstay.Checked = True Then mangoscore.setgender(Main.char_gender.ToString)
                                If level.Checked = True Then mangoscore.setlevel()
                                If alternatelevellabel.Checked = True Then _
                                    mangoscore.setalternatelevel(alternateleveltext.Text)
                                If race.Checked = True Then mangoscore.setrace()
                                If playerclass.Checked = True Then mangoscore.setclass()
                                If goldlabel.Checked = True Then mangoscore.setgold(goldtext.Text)
                                If erfolge.Checked = True Then mangoscore.addachievements()
                                If skills.Checked = True Then mangoscore.addskills()
                                If zauber.Checked = True Then mangoscore.addspells()
                                If pvp.Checked = True Then mangoscore.addpvp()
                                If ruf.Checked = True Then mangoscore.addreputation()
                                If inventar.Checked = True Then mangoscore.addinventory()
                                If gold.Checked = True Then mangoscore.addgold(Main.player_money)
                                reporttext.AppendText(Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                            Loop Until counter = Main.anzahldurchlaufe
                        End If

                    Next
                Else
                    trinitycore1.closesql()
                    Main.ServerString = alternatestring
                    Main.ServerStringRealmd = alternaterealmdstring
                    trinitycore1.opensql()
                    Dim sLines() As String = accnames.Lines
                    For i As Integer = 0 To sLines.Length - 1
                        xstring = sLines(i)
                        If sLines(i) = "" Then
                        Else
                            Do
                                Dim ciu As New CIUFile
                                ciu.readspecial(Main.cuisets)
                                Main.cuisets -= 1


                                mangoscore.adddetailedchar(sLines(i), Main.char_name, namechange1.Checked)

                                '  mangoscore.updatechars(sLines(i), Main.char_name, namechange2.Checked)
                                If items.Checked = True Then mangoscore.additems()
                                If enchantments.Checked = True Then mangoscore.addench()
                                If glyphs.Checked = True Then mangoscore.addglyphs(xpansion)
                                If talents.Checked = True Then mangoscore.addtalents()
                                If male.Checked = True Then mangoscore.setgender("0")
                                If female.Checked = True Then mangoscore.setgender("1")
                                If genderstay.Checked = True Then mangoscore.setgender(Main.char_gender.ToString)
                                If level.Checked = True Then mangoscore.setlevel()
                                If alternatelevellabel.Checked = True Then _
                                    mangoscore.setalternatelevel(alternateleveltext.Text)
                                If race.Checked = True Then mangoscore.setrace()
                                If playerclass.Checked = True Then mangoscore.setclass()
                                If goldlabel.Checked = True Then mangoscore.setgold(goldtext.Text)
                                If erfolge.Checked = True Then mangoscore.addachievements()
                                If skills.Checked = True Then mangoscore.addskills()
                                If zauber.Checked = True Then mangoscore.addspells()
                                If pvp.Checked = True Then mangoscore.addpvp()
                                If ruf.Checked = True Then mangoscore.addreputation()
                                If inventar.Checked = True Then mangoscore.addinventory()
                                If gold.Checked = True Then mangoscore.addgold(Main.player_money)
                                reporttext.AppendText(Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)


                            Loop Until Main.cuisets = 0
                        End If
                    Next
                End If
            End If
        ElseIf arcemu.Checked = True Then
            Main.outputcore = "arcemu"
            Dim xpacressource As String
            Dim xpacressource2 As String
            Select Case Main.xpac
                Case 3
                    xpacressource = My.Resources.GEM_ID_wotlk
                    xpacressource2 = My.Resources.VZ_ID_wotlk
                Case 4
                    xpacressource = My.Resources.GEM_ID_cata
                    xpacressource2 = My.Resources.VZ_ID_cata
                Case Else
                    xpacressource = My.Resources.GEM_ID_wotlk
                    xpacressource2 = My.Resources.VZ_ID_wotlk
            End Select
            arcemucore.spellgemtext = xpacressource
            arcemucore.spellitemtext = xpacressource2


            If CheckBox2.Checked = True Then
                If Main.ausgangsformat = 1 Then

                    If Database_Interface.CheckBox1.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            trinitycore1.getallchars()
                        ElseIf Database_Interface.mangos.Checked = True Then
                            mangoscore.getallchars()
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            arcemucore.getallchars()
                        Else

                        End If

                    ElseIf Database_Interface.CheckBox2.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            Dim sLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1

                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    trinitycore1.getallcharsfromaccount((sLines(i)).ToUpper)
                                End If

                            Next
                        ElseIf Database_Interface.mangos.Checked = True Then
                            Dim sLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1

                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    mangoscore.getallcharsfromaccount((sLines(i)).ToUpper)
                                End If

                            Next
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            Dim sLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1

                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    arcemucore.getallcharsfromaccount((sLines(i)).ToUpper)
                                End If

                            Next
                        Else

                        End If


                    ElseIf Database_Interface.CheckBox3.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            Dim sLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1
                                xstring = sLines(i)
                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    trinitycore1.getthischar(sLines(i))
                                End If

                            Next
                        ElseIf Database_Interface.mangos.Checked = True Then
                            Dim sLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1
                                xstring = sLines(i)
                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    mangoscore.getthischar(sLines(i))
                                End If

                            Next
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            Dim sLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To sLines.Length - 1
                                xstring = sLines(i)
                                If sLines(i) = "" Then
                                    removecount += 1
                                Else
                                    arcemucore.getthischar(sLines(i))
                                End If

                            Next
                        Else

                        End If

                    End If
                    trinitycore1.closesql()
                    Main.ServerString = alternatestring
                    Main.ServerStringRealmd = alternaterealmdstring
                    trinitycore1.opensql()
                    Do
                        counter += 1
                        cui.readtempdataset(counter)

                        Application.DoEvents()
                        Dim newid As String =
                                (CInt(
                                    Val(runcommandRealmd("SELECT acct FROM accounts WHERE acct=(SELECT MAX(acct) FROM accounts)",
                                                         "acct"))) + 1).ToString
                        If Main.account_access_gmlevel = 4 Then
                            If Main.arcemu_gmlevel = "" Then
                                Main.arcemu_gmlevel = "AZ"
                            End If
                        ElseIf Main.account_access_gmlevel = 0 Then
                            If Main.arcemu_gmlevel = "" Then
                                Main.arcemu_gmlevel = "0"
                            End If
                        ElseIf Main.account_access_gmlevel < 4 Then
                            If Main.arcemu_gmlevel = "" Then
                                Main.arcemu_gmlevel = "A"
                            End If
                        End If
                        Dim expflags As String = ""
                        Select Main.expansion
                            Case 0
                                expflags = "0"
                            Case 1
                                expflags = "8"
                            Case 2
                                expflags = "24"
                            Case 3
                                expflags = "32"
                            Case Else : End Select

                        arcemucore.create_new_account_if_not_exist(Main.accountname,
                                                                   "INSERT INTO accounts ( `acct`, `login`, `gm`,`encrypted_password`, `email`, flags ) VALUES ( '" &
                                                                   newid & "', '" & Main.accountname & "', '" &
                                                                   Main.arcemu_gmlevel.ToString & "', '" &
                                                                   Main.sha_pass_hash & "', '" & Main.email & "', '" & expflags & "' )", newid)
                        arcemucore.adddetailedchar(Main.accountname, Main.char_name, namechange1.Checked)
                        If items.Checked = True Then arcemucore.additems()
                        If enchantments.Checked = True Then arcemucore.addench()
                        If glyphs.Checked = True Then arcemucore.addglyphs(xpansion)
                        If talents.Checked = True Then arcemucore.addtalents()
                        If male.Checked = True Then arcemucore.setgender("0")
                        If female.Checked = True Then arcemucore.setgender("1")
                        If genderstay.Checked = True Then arcemucore.setgender(Main.char_gender.ToString)
                        If level.Checked = True Then arcemucore.setlevel()
                        If alternatelevellabel.Checked = True Then arcemucore.setalternatelevel(alternateleveltext.Text)
                        If race.Checked = True Then arcemucore.setrace()
                        If playerclass.Checked = True Then arcemucore.setclass()
                        If goldlabel.Checked = True Then arcemucore.setgold(goldtext.Text)
                        If erfolge.Checked = True Then arcemucore.addachievements()
                        If skills.Checked = True Then arcemucore.addskills()
                        If zauber.Checked = True Then arcemucore.addspells()
                        If pvp.Checked = True Then arcemucore.addpvp()
                        If ruf.Checked = True Then arcemucore.addreputation()
                        If inventar.Checked = True Then arcemucore.addinventory()
                        If gold.Checked = True Then arcemucore.addgold(Main.player_money)
                        reporttext.AppendText(Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    Loop Until counter = Main.anzahldurchlaufe

                Else
                    trinitycore1.closesql()
                    Main.ServerString = alternatestring
                    Main.ServerStringRealmd = alternaterealmdstring
                    trinitycore1.opensql()
                    Do
                        Dim ciu As New CIUFile
                        ciu.readspecial(Main.cuisets)
                        Main.cuisets -= 1


                        Dim newid As String =
                                (CInt(
                                    Val(runcommandRealmd("SELECT acct FROM accounts WHERE acct=(SELECT MAX(acct) FROM accounts)",
                                                         "acct"))) + 1).ToString
                        If Main.account_access_gmlevel = 4 Then
                            If Main.arcemu_gmlevel = "" Then
                                Main.arcemu_gmlevel = "AZ"
                            End If
                        ElseIf Main.account_access_gmlevel = 0 Then
                            If Main.arcemu_gmlevel = "" Then
                                Main.arcemu_gmlevel = "0"
                            End If
                        ElseIf Main.account_access_gmlevel < 4 Then
                            If Main.arcemu_gmlevel = "" Then
                                Main.arcemu_gmlevel = "A"
                            End If
                        End If
                        Dim expflags As String = ""
                        Select Case Main.expansion
                            Case 0
                                expflags = "0"
                            Case 1
                                expflags = "8"
                            Case 2
                                expflags = "24"
                            Case 3
                                expflags = "32"
                            Case Else : End Select

                        arcemucore.create_new_account_if_not_exist(Main.accountname,
                                                                   "INSERT INTO accounts ( `acct`, `login`, `gm`,`encrypted_password`, `email`, flags ) VALUES ( '" &
                                                                   newid & "', '" & Main.accountname & "', '" &
                                                                   Main.arcemu_gmlevel.ToString & "', '" &
                                                                   Main.sha_pass_hash & "', '" & Main.email & "', '" & expflags & "' )", newid)
                        arcemucore.adddetailedchar(Main.accountname, Main.char_name, namechange1.Checked)

                        '  arcemucore.updatechars(sLines(i), Main.char_name, namechange2.Checked)
                        If items.Checked = True Then arcemucore.additems()
                        If enchantments.Checked = True Then arcemucore.addench()
                        If glyphs.Checked = True Then arcemucore.addglyphs(xpansion)
                        If talents.Checked = True Then arcemucore.addtalents()
                        If male.Checked = True Then arcemucore.setgender("0")
                        If female.Checked = True Then arcemucore.setgender("1")
                        If genderstay.Checked = True Then arcemucore.setgender(Main.char_gender.ToString)
                        If level.Checked = True Then arcemucore.setlevel()
                        If alternatelevellabel.Checked = True Then arcemucore.setalternatelevel(alternateleveltext.Text)
                        If race.Checked = True Then arcemucore.setrace()
                        If playerclass.Checked = True Then arcemucore.setclass()
                        If goldlabel.Checked = True Then arcemucore.setgold(goldtext.Text)
                        If erfolge.Checked = True Then arcemucore.addachievements()
                        If skills.Checked = True Then arcemucore.addskills()
                        If zauber.Checked = True Then arcemucore.addspells()
                        If pvp.Checked = True Then arcemucore.addpvp()
                        If ruf.Checked = True Then arcemucore.addreputation()
                        If inventar.Checked = True Then arcemucore.addinventory()
                        If gold.Checked = True Then arcemucore.addgold(Main.player_money)
                        reporttext.AppendText(Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)


                    Loop Until Main.cuisets = 0
                End If
            End If
            If CheckBox3.Checked = True Then
                If Main.ausgangsformat = 1 Then
                    If Database_Interface.CheckBox1.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            trinitycore1.getallchars()
                        ElseIf Database_Interface.mangos.Checked = True Then
                            mangoscore.getallchars()
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            arcemucore.getallchars()
                        Else

                        End If

                    ElseIf Database_Interface.CheckBox2.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            Dim ssLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1

                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    trinitycore1.getallcharsfromaccount((ssLines(i)).ToUpper)
                                End If

                            Next
                        ElseIf Database_Interface.mangos.Checked = True Then
                            Dim ssLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1

                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    mangoscore.getallcharsfromaccount((ssLines(i)).ToUpper)
                                End If

                            Next
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            Dim ssLines() As String = Database_Interface.accnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1

                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    arcemucore.getallcharsfromaccount((ssLines(i)).ToUpper)
                                End If

                            Next
                        Else

                        End If


                    ElseIf Database_Interface.CheckBox3.Checked = True Then
                        If Database_Interface.trinity1.Checked = True Then
                            Dim ssLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1
                                xstring = ssLines(i)
                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    trinitycore1.getthischar(ssLines(i))
                                End If

                            Next
                        ElseIf Database_Interface.mangos.Checked = True Then
                            Dim ssLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1
                                xstring = ssLines(i)
                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    mangoscore.getthischar(ssLines(i))
                                End If

                            Next
                        ElseIf Database_Interface.arcemu.Checked = True Then
                            Dim ssLines() As String = Database_Interface.charnames.Lines


                            Dim removecount As Integer
                            For i As Integer = 0 To ssLines.Length - 1
                                xstring = ssLines(i)
                                If ssLines(i) = "" Then
                                    removecount += 1
                                Else
                                    arcemucore.getthischar(ssLines(i))
                                End If

                            Next
                        Else

                        End If


                    End If
                    trinitycore1.closesql()
                    Main.ServerString = alternatestring
                    Main.ServerStringRealmd = alternaterealmdstring
                    trinitycore1.opensql()
                    Dim sLines() As String = accnames.Lines
                    For i As Integer = 0 To sLines.Length - 1
                        xstring = sLines(i)
                        If sLines(i) = "" Then
                        Else
                            Do
                                counter += 1
                                cui.readtempdataset(counter)

                                Application.DoEvents()
                                arcemucore.adddetailedchar(sLines(i), Main.char_name, namechange1.Checked)
                                If items.Checked = True Then arcemucore.additems()
                                If enchantments.Checked = True Then arcemucore.addench()
                                If glyphs.Checked = True Then arcemucore.addglyphs(xpansion)
                                If talents.Checked = True Then arcemucore.addtalents()
                                If male.Checked = True Then arcemucore.setgender("0")
                                If female.Checked = True Then arcemucore.setgender("1")
                                If genderstay.Checked = True Then arcemucore.setgender(Main.char_gender.ToString)
                                If level.Checked = True Then arcemucore.setlevel()
                                If alternatelevellabel.Checked = True Then _
                                    arcemucore.setalternatelevel(alternateleveltext.Text)
                                If race.Checked = True Then arcemucore.setrace()
                                If playerclass.Checked = True Then arcemucore.setclass()
                                If goldlabel.Checked = True Then arcemucore.setgold(goldtext.Text)
                                If erfolge.Checked = True Then arcemucore.addachievements()
                                If skills.Checked = True Then arcemucore.addskills()
                                If zauber.Checked = True Then arcemucore.addspells()
                                If pvp.Checked = True Then arcemucore.addpvp()
                                If ruf.Checked = True Then arcemucore.addreputation()
                                If inventar.Checked = True Then arcemucore.addinventory()
                                If gold.Checked = True Then arcemucore.addgold(Main.player_money)
                                reporttext.AppendText(Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                            Loop Until counter = Main.anzahldurchlaufe
                        End If

                    Next
                Else
                    trinitycore1.closesql()
                    Main.ServerString = alternatestring
                    Main.ServerStringRealmd = alternaterealmdstring
                    trinitycore1.opensql()
                    Dim sLines() As String = accnames.Lines
                    For i As Integer = 0 To sLines.Length - 1
                        xstring = sLines(i)
                        If sLines(i) = "" Then
                        Else
                            Do
                                Dim ciu As New CIUFile
                                ciu.readspecial(Main.cuisets)
                                Main.cuisets -= 1


                                arcemucore.adddetailedchar(sLines(i), Main.char_name, namechange1.Checked)

                                '  arcemucore.updatechars(sLines(i), Main.char_name, namechange2.Checked)
                                If items.Checked = True Then arcemucore.additems()
                                If enchantments.Checked = True Then arcemucore.addench()
                                If glyphs.Checked = True Then arcemucore.addglyphs(xpansion)
                                If talents.Checked = True Then arcemucore.addtalents()
                                If male.Checked = True Then arcemucore.setgender("0")
                                If female.Checked = True Then arcemucore.setgender("1")
                                If genderstay.Checked = True Then arcemucore.setgender(Main.char_gender.ToString)
                                If level.Checked = True Then arcemucore.setlevel()
                                If alternatelevellabel.Checked = True Then _
                                    arcemucore.setalternatelevel(alternateleveltext.Text)
                                If race.Checked = True Then arcemucore.setrace()
                                If playerclass.Checked = True Then arcemucore.setclass()
                                If goldlabel.Checked = True Then arcemucore.setgold(goldtext.Text)
                                If erfolge.Checked = True Then arcemucore.addachievements()
                                If skills.Checked = True Then arcemucore.addskills()
                                If zauber.Checked = True Then arcemucore.addspells()
                                If pvp.Checked = True Then arcemucore.addpvp()
                                If ruf.Checked = True Then arcemucore.addreputation()
                                If inventar.Checked = True Then arcemucore.addinventory()
                                If gold.Checked = True Then arcemucore.addgold(Main.player_money)
                                reporttext.AppendText(Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)


                            Loop Until Main.cuisets = 0
                        End If
                    Next
                End If
            End If
        Else

        End If
        trinitycore1.closesql()
        If My.Settings.language = "de" Then
            MsgBox(localeDE.restartlogon, MsgBoxStyle.Information, localeDE.attention)
        Else
            MsgBox(localeEN.restartlogon, MsgBoxStyle.Information, localeEN.attention)
        End If
        reporttext.AppendText(Now.TimeOfDay.ToString & "// Transfer is completed!" & vbNewLine)

        Process_Status.Button1.Enabled = True
        Database_Interface.Close()
        Starter.Show()
        Me.Close()
        Process_Status.BringToFront()
    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        Dim errortext As String = ""
        If CheckBox3.Checked = True Then


            runfunction.writelog("Accountnames: " & accnames.Text)
            Dim sLines() As String = accnames.Lines
            Dim removecount As Integer
            For i As Integer = 0 To sLines.Length - 1

                If sLines(i) = "" Then
                    removecount += 1
                Else
                    If trinity1.Checked = True Then
                        trinitycore1.opensql()
                        If trinitycore1.accountexist((sLines(i)).ToUpper, alternaterealmdstring) = False Then _
                            errortext = errortext & "Account " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    ElseIf mangos.Checked = True Then
                        trinitycore1.opensql()
                        If mangoscore.accountexist((sLines(i)).ToUpper, alternaterealmdstring) = False Then _
                            errortext = errortext & "Account " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    Else
                        trinitycore1.opensql()
                        If arcemucore.accountexist((sLines(i)).ToUpper, alternaterealmdstring) = False Then _
                            errortext = errortext & "Account " & sLines(i) & " could not be found!" & vbNewLine
                        trinitycore1.closesql()
                    End If
                End If

            Next

        End If
        If Not errortext = "" Then
            MsgBox("The following errors occurred:" & vbNewLine & vbNewLine & errortext, MsgBoxStyle.Critical,
                   "Attention!")
            Exit Sub
        End If

        Process_Status.Show()
        runfunction.writelog("Continue request")
        counter = 0
        xnumber = 0
        runfunction.writelog("Corecheck request")
        xstring = "wotlk"
        If cata.Checked = True Then
            xpansion = "cata"

        End If

        If wotlk.Checked = True Then
            xpansion = "wotlk"
        End If

        ' Main.ServerStringCheck = alternatestring


        If trinity1.Checked = True Then
            trinitycorecheck.begincheck(34)
        ElseIf mangos.Checked = True Then
            mangoscorecheck.begincheck(34)
        ElseIf arcemu.Checked = True Then
            arcemucorecheck.begincheck(34)
        Else

        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            runfunction.writelog("Checkbox3 checked")
            CheckBox2.Checked = False
        Else
            CheckBox2.Checked = True
        End If
    End Sub

    Private Function runcommandRealmd(ByVal command As String, ByVal spalte As String) As String
        Dim conn As New MySqlConnection(Main.ServerStringRealmd)
        Dim da As New MySqlDataAdapter(command, conn)
        Dim dt As New DataTable
        Try
            conn.Open()
        Catch ex As Exception
            conn.Close()
            conn.Dispose()
        End Try
        da.Fill(dt)
        Try
            conn.Close()
            conn.Dispose()
        Catch :
        End Try
        Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))

        If Not lastcount = 0 Then
            Dim readed As String = (dt.Rows(0).Item(0)).ToString
            If readed = "DBnull" Then
                Return ""
            Else
                Return readed
            End If

        Else
            Return ""
        End If
    End Function


    Private Sub items_CheckedChanged(sender As Object, e As EventArgs) Handles items.CheckedChanged
        If items.Checked = True Then

            enchantments.Enabled = True
        Else

            enchantments.Enabled = False

            enchantments.Checked = False
        End If
    End Sub

    Private Sub namechange1_CheckedChanged(sender As Object, e As EventArgs) Handles namechange1.CheckedChanged
        If namechange1.Checked = True Then
            namechange2.Checked = False

        Else
            namechange2.Checked = True
        End If
    End Sub

    Private Sub namechange2_CheckedChanged(sender As Object, e As EventArgs) Handles namechange2.CheckedChanged
        If namechange2.Checked = True Then
            namechange1.Checked = False

        Else
            namechange1.Checked = True
        End If
    End Sub

    Private Sub optionspanel_Paint(sender As Object, e As PaintEventArgs) Handles optionspanel.Paint
    End Sub

    Private Sub CheckBox9_CheckedChanged(sender As Object, e As EventArgs) Handles pvp.CheckedChanged
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
        Handles LinkLabel1.LinkClicked
        items.Checked = True

        enchantments.Checked = True
        glyphs.Checked = True
        talents.Checked = True
        level.Checked = True
        race.Checked = True
        playerclass.Checked = True
        erfolge.Checked = True
        skills.Checked = True
        zauber.Checked = True
        pvp.Checked = True
        ruf.Checked = True
        inventar.Checked = True
        gold.Checked = True
    End Sub

    Private Sub alternatelevellabel_CheckedChanged(sender As Object, e As EventArgs) _
        Handles alternatelevellabel.CheckedChanged
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            runfunction.writelog("Checkbox2 checked")
            CheckBox3.Checked = False
        Else
            CheckBox3.Checked = True
        End If
    End Sub

    Private Sub genderstay_CheckedChanged(sender As Object, e As EventArgs) Handles genderstay.CheckedChanged
        If genderstay.Checked = True Then
            female.Checked = False
            male.Checked = False
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        runfunction.writelog("Database2Database_closing call")
        Database_Interface.Close()
        Me.Close()
        Starter.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
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
        Else :
        End If
        My.Settings.Save()
    End Sub

    Private Sub GroupBox4_Enter(sender As Object, e As EventArgs) Handles GroupBox4.Enter
    End Sub

    Private Sub trinity1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles trinity1.CheckedChanged
        If trinity1.Checked = True Then
            runfunction.writelog("Trinity checked")
        Else

        End If
    End Sub

    Private Sub mangos_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles mangos.CheckedChanged
        If mangos.Checked = True Then
            runfunction.writelog("mangos checked")
        Else

        End If
    End Sub

    Private Sub arcemu_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles arcemu.CheckedChanged
        If arcemu.Checked = True Then
            runfunction.writelog("arcemu checked")
        Else

        End If
    End Sub
End Class