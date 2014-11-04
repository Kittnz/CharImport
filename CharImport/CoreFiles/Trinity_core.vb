'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* Trinity_core contains several functions to implement the character
'* and account information into an TrinityCore compatible database.
'*
'* Developed by Alcanmage/megasus
Imports System.Globalization
Imports MySql.Data.MySqlClient

Public Class Trinity_core
    Dim runfunction As New Functions
    Dim core_check As New Core_Check_Trinity
    ' Dim SQLConnection As MySqlConnection = New MySqlConnection
    Public characterguid As Integer
    Dim reporttext As RichTextBox = Process_Status.processreport
    '  Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim quelltext As String = ""
    Dim talentpage As String = ""
    Dim sectalentpage As String = ""
    Dim datacharname As String = ""
    Dim guid As String = ""
    Dim accguid As String = ""
    Dim lastnumber As String = ""
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN

    Dim _
        finalstring As String =
            "kopf 0 hals 0 schulter 0 hemd 0 brust 0 guertel 0 beine 0 stiefel 0 handgelenke 0 haende 0 finger1 0 finger2 0 schmuck1 0 schmuck2 0 ruecken 0 haupt 0 off 0 distanz 0 wappenrock 0 "

    Dim newcharguid As String
    Public spellitemtext As String
    Public spellgemtext As String

    Dim kopfwearguid As Integer
    Dim halswearguid As Integer
    Dim schulterwearguid As Integer
    Dim rueckenwearguid As Integer
    Dim brustwearguid As Integer
    Dim hemdwearguid As Integer
    Dim wappenrockwearguid As Integer
    Dim handgelenkewearguid As Integer
    Dim haendewearguid As Integer
    Dim guertelwearguid As Integer
    Dim beinewearguid As Integer
    Dim stiefelwearguid As Integer
    Dim ring1wearguid As Integer
    Dim ring2wearguid As Integer
    Dim schmuck1wearguid As Integer
    Dim schmuck2wearguid As Integer
    Dim hauptwearguid As Integer
    Dim offwearguid As Integer
    Dim distanzwearguid As Integer
    Dim guidlist As List(Of String)
    Dim idlist As List(Of String)
    'Atribute
    Public Sub opensql()
        runfunction.writelog("opensql_call @trinity")
        Dim teststring = Main.MainInstance.ServerString
        Try
            Main.MainInstance.GLOBALconn.Close()
            Main.MainInstance.GLOBALconn.Dispose()
        Catch ex As Exception

        End Try
        Try
            Main.MainInstance.GLOBALconnRealmd.Close()
            Main.MainInstance.GLOBALconnRealmd.Dispose()
        Catch ex As Exception

        End Try
        Main.MainInstance.GLOBALconn.ConnectionString = Main.MainInstance.ServerString
        Main.MainInstance.GLOBALconnRealmd.ConnectionString = Main.MainInstance.ServerStringRealmd
        Try
            Main.MainInstance.GLOBALconn.Open()
        Catch ex As Exception
            runfunction.writelog("Failed to open SQL connection @trinity errmsg: " & ex.ToString)
            Main.MainInstance.GLOBALconn.Close()
            Main.MainInstance.GLOBALconn.Dispose()
        End Try
        Try
            Main.MainInstance.GLOBALconnRealmd.Open()
        Catch ex As Exception
            runfunction.writelog("Failed to open SQL connection Realmd @trinity errmsg: " & ex.ToString)
            Main.MainInstance.GLOBALconnRealmd.Close()
            Main.MainInstance.GLOBALconnRealmd.Dispose()
        End Try
    End Sub

    Public Sub closesql()
        runfunction.writelog("closesql_call @trinity")
        Try
            Main.MainInstance.GLOBALconn.Close()
            Main.MainInstance.GLOBALconn.Dispose()
        Catch ex As Exception

        End Try
        Try
            Main.MainInstance.GLOBALconnRealmd.Close()
            Main.MainInstance.GLOBALconnRealmd.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Public Function accountexist(ByVal accountname As String, ByVal connectionstring As String) As Boolean
        Dim quickconn As New MySqlConnection
        quickconn.ConnectionString = connectionstring
        Try
            quickconn.Open()
        Catch ex As Exception

        End Try
        Dim da As New MySqlDataAdapter("SELECT `id` FROM account WHERE `username`='" & accountname & "'", quickconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            quickconn.Close()
            quickconn.Dispose()
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            If lastcount = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function characterexist(ByVal charname As String, ByVal connectionstring As String) As Boolean
        Dim quickconn As New MySqlConnection
        quickconn.ConnectionString = connectionstring
        Try
            quickconn.Open()
        Catch ex As Exception

        End Try
        Dim da As New MySqlDataAdapter("SELECT guid FROM characters WHERE upper(name)='" & charname.ToUpper() & "'", quickconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            quickconn.Close()
            quickconn.Dispose()
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            If lastcount = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Function getidswithgmlevel(ByVal gmlevel As String) As List(Of String)
        Dim _
            da As _
                New MySqlDataAdapter("SELECT `id` FROM `account_access` WHERE gmlevel='" & gmlevel & "'",
                                     Main.MainInstance.GLOBALconnRealmd)
        Dim dt As New DataTable
        Dim tmplist As New List(Of String)
        tmplist.Clear()

        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            If Not lastcount = 0 Then
                Dim count As Integer = 0
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    tmplist.Add(readedcode)
                    count += 1
                Loop Until count = lastcount
                Return tmplist
            End If
            Return tmplist
        Catch ex As Exception
            Return tmplist
        End Try
    End Function

    Public Sub getallchars()
        runfunction.writelog("getallchars_call @trinity")
        Dim filteredidlist As List(Of String) = New List(Of String)
        filteredidlist.Clear()
        Try
            guidlist.Clear()
        Catch ex As Exception

        End Try
        Try
            idlist.Clear()
        Catch ex As Exception

        End Try
        guidlist = New List(Of String)
        idlist = New List(Of String)
        If My.Settings.lastloginactive = True Or My.Settings.gmlevelactive = True Then
            If My.Settings.lastloginactive = True Then
                If My.Settings.gmlevelactive = True Then
                    filteredidlist = getidswithgmlevel(My.Settings.gmlevel.ToString)
                    If Not filteredidlist.Count = 0 Then
                        For Each accid As String In filteredidlist
                            Dim _
                                da As _
                                    New MySqlDataAdapter(
                                        "SELECT `username` FROM `account` WHERE last_login>='" &
                                        My.Settings.lastlogindate1 & "' AND last_login<='" & My.Settings.lastlogindate2 &
                                        "' AND `id`='" & accid & "'", Main.MainInstance.GLOBALconnRealmd)
                            Dim dt As New DataTable
                            Try
                                da.Fill(dt)

                                Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
                                If Not lastcount = 0 Then
                                    Dim count As Integer = 0
                                    Do
                                        Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                                        idlist.Add(readedcode)
                                        count += 1
                                    Loop Until count = lastcount
                                End If
                            Catch ex As Exception

                            End Try
                        Next
                    Else

                    End If

                Else
                    Dim _
                        da As _
                            New MySqlDataAdapter(
                                "SELECT `username` FROM `account` WHERE last_login>='" & My.Settings.lastlogindate1 &
                                "' AND last_login<='" & My.Settings.lastlogindate2 & "'", Main.MainInstance.GLOBALconnRealmd)
                    Dim dt As New DataTable
                    Try
                        da.Fill(dt)

                        Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
                        If Not lastcount = 0 Then
                            Dim count As Integer = 0
                            Do
                                Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                                idlist.Add(readedcode)
                                count += 1
                            Loop Until count = lastcount
                        End If
                    Catch ex As Exception

                    End Try
                End If
            Else
                If My.Settings.gmlevelactive = True Then

                    Dim _
                        da As _
                            New MySqlDataAdapter(
                                "SELECT `id` FROM `account_access` WHERE gmlevel='" & My.Settings.gmlevel.ToString & "'",
                                Main.MainInstance.GLOBALconnRealmd)
                    Dim dt As New DataTable
                    Try
                        da.Fill(dt)

                        Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
                        If Not lastcount = 0 Then
                            Dim count As Integer = 0
                            Do
                                Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                                idlist.Add(
                                    runfunction.runcommandRealmd(
                                        "SELECT username from `account` WHERE `id`='" & readedcode & "'", "username"))
                                count += 1
                            Loop Until count = lastcount
                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If
            For Each user As String In idlist
                getallcharsfromaccount(user)
            Next
        Else
            If My.Settings.levelrangeactive = True Then

                Dim _
                    da As _
                        New MySqlDataAdapter(
                            "SELECT guid FROM characters WHERE level>='" & My.Settings.levelrangemin.ToString &
                            "' AND level<='" & My.Settings.levelrangemax & "'", Main.MainInstance.GLOBALconn)
                Dim dt As New DataTable
                Try
                    da.Fill(dt)

                    Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
                    If Not lastcount = 0 Then
                        Dim count As Integer = 0
                        Do
                            Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                            guidlist.Add(readedcode)
                            count += 1
                        Loop Until count = lastcount
                    End If
                Catch ex As Exception

                End Try
            Else
                Dim da As New MySqlDataAdapter("SELECT guid FROM characters", Main.MainInstance.GLOBALconn)
                Dim dt As New DataTable
                Try
                    da.Fill(dt)

                    Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
                    If Not lastcount = 0 Then
                        Dim count As Integer = 0
                        Do
                            Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                            guidlist.Add(readedcode)
                            count += 1
                        Loop Until count = lastcount
                    End If
                Catch ex As Exception

                End Try
            End If


            gochars()
        End If
    End Sub

    Public Sub getallchars_old()

        runfunction.writelog("getallchars_call @trinity")
        Try
            guidlist.Clear()
        Catch ex As Exception

        End Try

        guidlist = New List(Of String)


        Dim da As New MySqlDataAdapter("SELECT guid FROM characters", Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            If Not lastcount = 0 Then
                Dim count As Integer = 0
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    guidlist.Add(readedcode)
                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try

        gochars()
    End Sub

    Public Sub getthischar(ByVal charname As String)
        runfunction.writelog("getthischar_call with charname: " & charname)
        Dim adljah As String = Main.MainInstance.GLOBALconn.ConnectionString
        Dim statussda As String = Main.MainInstance.GLOBALconn.State.ToString

        'Dim conn As New MySqlConnection(Main.MainInstance.ServerString)
        Dim da As New MySqlDataAdapter("SELECT guid FROM characters WHERE upper(name)='" & charname.ToUpper() & "'", Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        'Try
        '    conn.Open()
        'Catch ex As Exception
        '    conn.Close()
        '    conn.Dispose()
        'End Try
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            If Not lastcount = 0 Then
                Dim count As Integer = 0
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    GetCharFromDatabase(readedcode)
                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getallcharsfromaccount(ByVal accountname As String)
        runfunction.writelog("getallcharsfromaccount_call with accountname: " & accountname)
        Try
            guidlist.Clear()
        Catch ex As Exception

        End Try
        Dim accid As String =
                runfunction.runcommandRealmd("SELECT `id` FROM account WHERE `username`='" & accountname & "'", "id")
        guidlist = New List(Of String)


        Dim da As New MySqlDataAdapter("SELECT guid FROM characters WHERE `account`='" & accid & "'", Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            If Not lastcount = 0 Then
                Dim count As Integer = 0
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    guidlist.Add(readedcode)
                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try

        gochars()
    End Sub

    Private Sub gochars()
        runfunction.writelog("gochars_call @trinity")
        For Each guid As String In guidlist
            GetCharFromDatabase(guid)
        Next
    End Sub

    Public Overridable Sub GetCharFromDatabase(ByVal charguid As String)
        '****************************************************************************************
        '****************************************************************************************
        'Get Character Guid


        '****************************************************************************************
        '****************************************************************************************
        'Get Main Character atributes

        'Character Race
        runfunction.writelog("GetCharFromDatabase_call with charguid: " & charguid)
        Main.MainInstance.setallempty()
        Main.MainInstance.anzahldurchlaufe += 1
        Main.MainInstance.char_guid = CInt(Val(charguid))
        characterguid = CInt(Val(charguid))

        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Race from Database..." & vbNewLine)
        Application.DoEvents()
        Try
            Main.MainInstance.char_race = CInt(Val(runfunction.runcommand("SELECT race FROM characters WHERE guid='" & charguid & "'",
                                                             "race")))
        Catch ex As Exception

        End Try

        '1 }}	Human
        '2 }}	Orc
        '3 }}	Dwarf
        '4 }}	Night Elf
        '5 }}	Undead
        '6 }}	Tauren
        '7 }}	Gnome
        '8 }}	Troll
        '9 }}	Goblin
        '10 }}	Blood Elf
        '11	}}  Draenei

        'Character Class
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Class from Database..." & vbNewLine)
        Application.DoEvents()
        Try
            Main.MainInstance.char_class =
                CInt(Val(runfunction.runcommand("SELECT class FROM characters WHERE guid='" & charguid & "'", "class")))
        Catch ex As Exception

        End Try
        '1 }}	Warrior
        '2 }}	Paladin
        '3 }}	Hunter
        '4 }}	Rogue
        '5 }}	Priest
        '6 }}	Death Knight
        '7 }}	Shaman
        '8 }}	Mage
        '9 }}	Warlock
        '11 }}	Druid

        'Character gender
        '0=male, 1=female
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Gender from Database..." & vbNewLine)
        Application.DoEvents()
        Try
            Main.MainInstance.char_gender =
                CInt(Val(runfunction.runcommand("SELECT gender FROM characters WHERE guid='" & charguid & "'", "gender")))
        Catch ex As Exception

        End Try

        'Character level
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Level from Database..." & vbNewLine)
        Application.DoEvents()
        Try
            Main.MainInstance.char_level =
                CInt(Val(runfunction.runcommand("SELECT level FROM characters WHERE guid='" & charguid & "'", "level")))

        Catch ex As Exception

        End Try
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Table..." & vbNewLine)
        Application.DoEvents()

        Main.MainInstance.char_name = runfunction.runcommand("SELECT name FROM characters WHERE guid='" & charguid & "'", "name")

        Main.MainInstance.accountid = CInt(Val(runfunction.runcommand("SELECT account FROM characters WHERE guid='" & charguid & "'",
                                                         "account")))
        Main.MainInstance.char_xp = CInt(Val(runfunction.runcommand("SELECT xp FROM characters WHERE guid='" & charguid & "'", "xp")))
        Main.MainInstance.player_money = CInt(Val(runfunction.runcommand("SELECT money FROM characters WHERE guid='" & charguid & "'",
                                                            "money")))
        Main.MainInstance.playerBytes =
            CInt(Val(runfunction.runcommand("SELECT playerBytes FROM characters WHERE guid='" & charguid & "'",
                                            "playerBytes")))
        Main.MainInstance.playerBytes2 =
            CInt(Val(runfunction.runcommand("SELECT playerBytes2 FROM characters WHERE guid='" & charguid & "'",
                                            "playerBytes2")))
        Main.MainInstance.playerFlags =
            CInt(Val(runfunction.runcommand("SELECT playerFlags FROM characters WHERE guid='" & charguid & "'",
                                            "playerFlags")))
        Main.MainInstance.position_x = CDbl((runfunction.runcommand("SELECT position_x FROM characters WHERE guid='" & charguid & "'",
                                                       "position_x")))
        Main.MainInstance.position_y = CDbl((runfunction.runcommand("SELECT position_y FROM characters WHERE guid='" & charguid & "'",
                                                       "position_y")))
        Main.MainInstance.position_z = CDbl((runfunction.runcommand("SELECT position_z FROM characters WHERE guid='" & charguid & "'",
                                                       "position_z")))
        Main.MainInstance.map = CInt(Val(runfunction.runcommand("SELECT map FROM characters WHERE guid='" & charguid & "'", "map")))
        Main.MainInstance.instance_id =
            CInt(Val(runfunction.runcommand("SELECT instance_id FROM characters WHERE guid='" & charguid & "'",
                                            "instance_id")))
        Main.MainInstance.instance_mode_mask =
            runfunction.runcommand("SELECT instance_mode_mask FROM characters WHERE guid='" & charguid & "'",
                                   "instance_mode_mask")
        Main.MainInstance.orientation = CDbl((runfunction.runcommand("SELECT orientation FROM characters WHERE guid='" & charguid & "'",
                                                        "orientation")))
        Main.MainInstance.taximask = runfunction.runcommand("SELECT taximask FROM characters WHERE guid='" & charguid & "'",
                                               "taximask")
        Main.MainInstance.cinematic =
            CInt(Val(runfunction.runcommand("SELECT cinematic FROM characters WHERE guid='" & charguid & "'",
                                            "cinematic")))
        Main.MainInstance.totaltime =
            CInt(Val(runfunction.runcommand("SELECT totaltime FROM characters WHERE guid='" & charguid & "'",
                                            "totaltime")))
        Main.MainInstance.leveltime =
            CInt(Val(runfunction.runcommand("SELECT leveltime FROM characters WHERE guid='" & charguid & "'",
                                            "leveltime")))
        Main.MainInstance.extra_flags = runfunction.runcommand("SELECT extra_flags FROM characters WHERE guid='" & charguid & "'",
                                                  "extra_flags")
        Main.MainInstance.stable_slots = runfunction.runcommand("SELECT stable_slots FROM characters WHERE guid='" & charguid & "'",
                                                   "stable_slots")
        Main.MainInstance.at_login = runfunction.runcommand("SELECT at_login FROM characters WHERE guid='" & charguid & "'",
                                               "at_login")
        Main.MainInstance.zone = CInt(Val(runfunction.runcommand("SELECT zone FROM characters WHERE guid='" & charguid & "'", "zone")))
        Main.MainInstance.arenaPoints =
            CInt(Val(runfunction.runcommand("SELECT arenaPoints FROM characters WHERE guid='" & charguid & "'",
                                            "arenaPoints")))
        Main.MainInstance.totalHonorPoints =
            CInt(Val(runfunction.runcommand("SELECT totalHonorPoints FROM characters WHERE guid='" & charguid & "'",
                                            "totalHonorPoints")))
        Main.MainInstance.totalKills =
            CInt(Val(runfunction.runcommand("SELECT totalKills FROM characters WHERE guid='" & charguid & "'",
                                            "totalKills")))
        Main.MainInstance.chosenTitle = runfunction.runcommand("SELECT chosenTitle FROM characters WHERE guid='" & charguid & "'",
                                                  "chosenTitle")
        Main.MainInstance.knownCurrencies =
            runfunction.runcommand("SELECT knownCurrencies FROM characters WHERE guid='" & charguid & "'",
                                   "knownCurrencies")
        Main.MainInstance.watchedFaction =
            runfunction.runcommand("SELECT watchedFaction FROM characters WHERE guid='" & charguid & "'",
                                   "watchedFaction")
        Main.MainInstance.health = CInt(Val(runfunction.runcommand("SELECT health FROM characters WHERE guid='" & charguid & "'",
                                                      "health")))
        Main.MainInstance.speccount =
            CInt(Val(runfunction.runcommand("SELECT speccount FROM characters WHERE guid='" & charguid & "'",
                                            "speccount")))
        Main.MainInstance.activespec =
            CInt(Val(runfunction.runcommand("SELECT activespec FROM characters WHERE guid='" & charguid & "'",
                                            "activespec")))
        Main.MainInstance.exploredZones = runfunction.runcommand("SELECT exploredZones FROM characters WHERE guid='" & charguid & "'",
                                                    "exploredZones")
        Main.MainInstance.knownTitles = runfunction.runcommand("SELECT knownTitles FROM characters WHERE guid='" & charguid & "'",
                                                  "knownTitles")
        Main.MainInstance.actionBars = runfunction.runcommand("SELECT actionBars FROM characters WHERE guid='" & charguid & "'",
                                                 "actionBars")

        Main.MainInstance.accountname =
            runfunction.runcommandRealmd("SELECT username FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'",
                                         "username")
        Main.MainInstance.sha_pass_hash =
            runfunction.runcommandRealmd(
                "SELECT sha_pass_hash FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "sha_pass_hash")
        Main.MainInstance.sessionkey =
            runfunction.runcommandRealmd("SELECT sessionkey FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'",
                                         "sessionkey")
        Main.MainInstance.account_v =
            runfunction.runcommandRealmd("SELECT v FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "v")
        Main.MainInstance.account_s =
            runfunction.runcommandRealmd("SELECT s FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "s")
        Main.MainInstance.email =
            runfunction.runcommandRealmd("SELECT email FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'",
                                         "email")
        Main.MainInstance.joindate =
            runfunction.runcommandRealmd("SELECT joindate FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'",
                                         "joindate")
        Main.MainInstance.expansion =
            CInt(
                Val(
                    runfunction.runcommandRealmd(
                        "SELECT expansion FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "expansion")))
        Main.MainInstance.locale =
            CInt(
                Val(
                    runfunction.runcommandRealmd(
                        "SELECT locale FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "locale")))
        Main.MainInstance.account_access_gmlevel =
            CInt(
                Val(
                    runfunction.runcommandRealmd(
                        "SELECT gmlevel FROM account_access WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "gmlevel")))
        Main.MainInstance.account_access_RealmID =
            CInt(
                Val(
                    runfunction.runcommandRealmd(
                        "SELECT RealmID FROM account_access WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "RealmID")))
        Main.MainInstance.level.Text = Main.MainInstance.char_name & ", " & Main.MainInstance.char_level & ", " & Main.MainInstance.char_race & ", " & Main.MainInstance.char_class
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Homebind from Database..." & vbNewLine)
        Main.MainInstance.character_homebind =
            ("<map>" &
             runfunction.runcommand(
                 "SELECT " & Main.MainInstance.homebind_map & " FROM character_homebind WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                 Main.MainInstance.homebind_map) & "</map><zone>" &
             runfunction.runcommand(
                 "SELECT " & Main.MainInstance.homebind_zone & " FROM character_homebind WHERE guid='" & Main.MainInstance.char_guid.ToString &
                 "'", Main.MainInstance.homebind_zone) & "</zone><position_x>" &
             runfunction.runcommand(
                 "SELECT " & Main.MainInstance.homebind_posx & " FROM character_homebind WHERE guid='" & Main.MainInstance.char_guid.ToString &
                 "'", Main.MainInstance.homebind_posx) & "</position_x><position_y>" &
             runfunction.runcommand(
                 "SELECT " & Main.MainInstance.homebind_posy & " FROM character_homebind WHERE guid='" & Main.MainInstance.char_guid.ToString &
                 "'", Main.MainInstance.homebind_posy) & "</position_y><position_z>" &
             runfunction.runcommand(
                 "SELECT " & Main.MainInstance.homebind_posz & " FROM character_homebind WHERE guid='" & Main.MainInstance.char_guid.ToString &
                 "'", Main.MainInstance.homebind_posz) & "</position_z>")
        Main.MainInstance.level.Text = Main.MainInstance.char_name & ", " & Main.MainInstance.char_level & ", "
        Select Case Main.MainInstance.char_race
            Case 1
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Mensch" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Human"
            Case 2
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Orc" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Orc"
            Case 3
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Zwerg" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Dwarf"
            Case 4
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Nachtelf" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Night Elf"
            Case 5
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Untot" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Undead"
            Case 6
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Tauren" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Tauren"
            Case 7
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Gnom" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Gnome"
            Case 8
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Troll" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Troll"
            Case 9
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Goblin" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Goblin"
            Case 10
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Blutelf" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Blood Elf"
            Case 11
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Draenei" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Draenei"
            Case 22
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Worgen" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Worgen"
            Case Else

        End Select
        Main.MainInstance.level.Text = Main.MainInstance.level.Text & ", "
        Select Case Main.MainInstance.char_class
            Case 1
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Krieger" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Warrior"
            Case 2
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Paladin" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Paladin"
            Case 3
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Jäger" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Hunter"
            Case 4
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Schurke" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Rogue"
            Case 5
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Priester" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Priest"
            Case 6
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Todesritter" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Death Knight"
            Case 7
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Schamane" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Shaman"
            Case 8
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Magier" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Mage"
            Case 9
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Hexenmeister" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Warlock"
            Case 11
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Druide" Else _
                    Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Druid"
            Case Else

        End Select
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Spells from Database..." & vbNewLine)
        Application.DoEvents()
        getspells()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Talents from Database..." & vbNewLine)
        Application.DoEvents()
        gettalents()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Skills from Database..." & vbNewLine)
        Application.DoEvents()
        getskills()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Reputation from Database..." & vbNewLine)
        Application.DoEvents()
        getREPlists()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Action from Database..." & vbNewLine)
        Application.DoEvents()
        getactionlist()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Achievements from Database..." & vbNewLine)
        Application.DoEvents()
        getavlists()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Questlog from Database..." & vbNewLine)
        Application.DoEvents()
        getqueststatus()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Inventory from Database..." & vbNewLine)
        Application.DoEvents()
        getinventoryitems()

        'GET ITEMS
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Items from Database..." & vbNewLine)
        Application.DoEvents()
        getitems()


        'GET GLYPHS
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Primary Glyphs from Database..." & vbNewLine)
        Application.DoEvents()
        getglyphs()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Secondary Glyphs from Database..." & vbNewLine)
        Application.DoEvents()
        getsecglyphs()
        handleenchantments()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Character loaded!..." & vbNewLine)
        Application.DoEvents()

        saveglyphs()
        Main.MainInstance.datasets += 1
        Dim addtataset As New CIUFile
        addtataset.adddataset()

        Application.DoEvents()
    End Sub

    Public Sub getspells()
        runfunction.writelog("getspells_call @trinity")
        Dim _
            da As _
                New MySqlDataAdapter("SELECT spell FROM character_spell WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                     Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim spell As String = readedcode
                    Dim active As String =
                            runfunction.runcommand(
                                "SELECT `active` FROM character_spell WHERE spell='" & spell & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "`active`")
                    Dim disabled As String =
                            runfunction.runcommand(
                                "SELECT `disabled` FROM character_spell WHERE spell='" & spell & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "`disabled`")

                    Main.MainInstance.character_spells.Add(
                        "<spell>" & spell & "</spell><active>" & active & "</active><disabled>" & disabled &
                        "</disabled>")

                    count += 1
                Loop Until count = lastcount

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub gettalents()
        runfunction.writelog("gettalents_call @trinity")
        Dim _
            da As _
                New MySqlDataAdapter(
                    "SELECT spell FROM character_talent WHERE guid='" & Main.MainInstance.char_guid.ToString & "' AND spec='0'",
                    Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim spell As String = readedcode

                    Main.MainInstance.character_talent_list.Add("<spell>" & spell & "</spell><spec>0</spec>")

                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try

        getspec1()
    End Sub

    Private Sub getspec1()
        runfunction.writelog("getspec1_call (talents) @trinity")
        ' Dim conn As New MySqlConnection(Main.MainInstance.ServerString)
        Dim _
            da As _
                New MySqlDataAdapter(
                    "SELECT spell FROM character_talent WHERE guid='" & Main.MainInstance.char_guid.ToString & "' AND spec='1'",
                    Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim spell As String = readedcode

                    Main.MainInstance.character_talent_list.Add("<spell>" & spell & "</spell><spec>1</spec>")

                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Overridable Sub getqueststatus()
        runfunction.writelog("getqueststatus_call @trinity")
        Dim _
            da As _
                New MySqlDataAdapter(
                    "SELECT quest FROM character_queststatus WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                    Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim quest As String = readedcode
                    Dim status As String =
                            runfunction.runcommand(
                                "SELECT `status` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "status")
                    Dim explored As String =
                            runfunction.runcommand(
                                "SELECT `explored` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "explored")
                    Dim timer As String =
                            runfunction.runcommand(
                                "SELECT `timer` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "timer")
                    Main.MainInstance.character_queststatus.Add(
                        "<quest>" & quest & "</quest><status>" & status & "</status><explored>" & explored &
                        "</explored><timer>" & timer & "</timer>")

                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
        Dim _
            da2 As _
                New MySqlDataAdapter(
                    "SELECT quest FROM character_queststatus_rewarded WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                    Main.MainInstance.GLOBALconn)
        Dim dt2 As New DataTable
        Try
            da2.Fill(dt2)

            Dim lastcount As Integer = CInt(Val(dt2.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt2.Rows(count).Item(0)).ToString
                    Dim quest As String = readedcode
                    If Not quest = "" Then Main.MainInstance.finished_quests = Main.MainInstance.finished_quests & quest & ","
                    count += 1
                Loop Until count = lastcount
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub getskills()
        runfunction.writelog("getskills_call @trinity")
        Dim _
            da As _
                New MySqlDataAdapter("SELECT skill FROM character_skills WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                     Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim skill As String = readedcode
                    Dim value As String =
                            runfunction.runcommand(
                                "SELECT `value` FROM character_skills WHERE skill='" & skill & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "value")
                    Dim max As String =
                            runfunction.runcommand(
                                "SELECT max FROM character_skills WHERE skill='" & skill & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "max")

                    Main.MainInstance.character_skills_list.Add(
                        "<skill>" & skill & "</skill><value>" & value & "</value><max>" & max & "</max>")

                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getREPlists()
        runfunction.writelog("getREPlists_call @trinity")
        Dim _
            da As _
                New MySqlDataAdapter(
                    "SELECT faction FROM character_reputation WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                    Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim faction As String = readedcode
                    Dim standing As String =
                            runfunction.runcommand(
                                "SELECT standing FROM character_reputation WHERE faction='" & faction & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "standing")
                    Dim flags As String =
                            runfunction.runcommand(
                                "SELECT flags FROM character_reputation WHERE faction='" & faction & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "flags")

                    Main.MainInstance.character_reputatuion_list.Add(
                        "<faction>" & faction & "</faction><standing>" & standing & "</standing><flags>" & flags &
                        "</flags>")

                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getactionlist()
        runfunction.writelog("getactionlist_call @trinity")
        Dim _
            da As _
                New MySqlDataAdapter("SELECT button FROM character_action WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                     Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim gbutton As String = readedcode
                    Dim spec As String =
                            runfunction.runcommand(
                                "SELECT spec FROM character_action WHERE button='" & gbutton & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "spec")
                    Dim action As String =
                            runfunction.runcommand(
                                "SELECT action FROM character_action WHERE button='" & gbutton & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "action")
                    Dim atype As String =
                            runfunction.runcommand(
                                "SELECT type FROM character_action WHERE button='" & gbutton & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "type")

                    Main.MainInstance.character_action_list.Add(
                        "<action>" & action & "</action><spec>" & spec & "</spec><button>" & gbutton & "</button><type>" &
                        atype & "</type>")

                    count += 1
                Loop Until count = lastcount

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getavlists()
        runfunction.writelog("getavlists_call @trinity")
        Dim _
            da As _
                New MySqlDataAdapter(
                    "SELECT achievement FROM character_achievement WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                    Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim avid As String = readedcode
                    Dim xdate As String =
                            runfunction.runcommand(
                                "SELECT date FROM character_achievement WHERE achievement='" & avid & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "date")
                    Main.MainInstance.character_achievement_list.Add("<av>" & avid & "</av><date>" & xdate & "</date>")

                    count += 1
                Loop Until count = lastcount

            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub getinventoryitems()
        runfunction.writelog("getinventoryitems_call @trinity")
        Dim tmpext As Integer

        Dim _
            da As _
                New MySqlDataAdapter("SELECT item FROM character_inventory WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                     Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    tmpext = CInt(Val(readedcode))
                    Dim bagguid As String =
                            runfunction.runcommand(
                                "SELECT bag FROM character_inventory WHERE guid='" & Main.MainInstance.char_guid.ToString &
                                "' AND item='" & tmpext.ToString & "'", "bag")
                    If CInt(bagguid) = 0 Then
                        If tmpext > 18 Then
                            Dim bag As String = "0"
                            Dim item As String = "0"
                            Dim entryid As String
                            Dim enchantments As String
                            Dim itemcount As String = "1"
                            Dim slot As String = "0"
                            bag = bagguid


                            item = tmpext.ToString()
                            entryid =
                                runfunction.runcommand("SELECT itemEntry FROM item_instance WHERE guid = '" & item & "'",
                                                       "itemEntry")
                            enchantments =
                                runfunction.runcommand(
                                    "SELECT enchantments FROM item_instance WHERE guid = '" & item & "'", "enchantments")
                            itemcount =
                                runfunction.runcommand("Select `count` FROM item_instance WHERE guid='" & item & "'",
                                                       "count")
                            slot =
                                runfunction.runcommand(
                                    "Select `slot` FROM character_inventory WHERE `item`='" & item & "'", "slot")
                            Main.MainInstance.character_inventoryzero_list.Add(
                                "<slot>" & slot & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid &
                                "</bagguid><item>" & entryid & "</item><enchant>" & enchantments & "</enchant><count>" &
                                itemcount & "</count><oldguid>" & item & "</oldguid>")
                        End If
                    Else
                        Dim bag As String = "0"
                        Dim item As String = "0"
                        Dim entryid As String
                        Dim enchantments As String
                        Dim itemcount As String = "1"
                        Dim slot As String = "0"

                        bag =
                            runfunction.runcommand("SELECT itemEntry FROM item_instance WHERE guid = '" & bagguid & "'",
                                                   "itemEntry")
                        item = tmpext.ToString
                        entryid =
                            runfunction.runcommand("SELECT itemEntry FROM item_instance WHERE guid = '" & item & "'",
                                                   "itemEntry")
                        enchantments =
                            runfunction.runcommand("SELECT enchantments FROM item_instance WHERE guid = '" & item & "'",
                                                   "enchantments")
                        itemcount = runfunction.runcommand("Select `count` FROM item_instance WHERE guid='" & item & "'",
                                                           "count")
                        slot =
                            runfunction.runcommand("Select `slot` FROM character_inventory WHERE `item`='" & item & "'",
                                                   "slot")
                        Main.MainInstance.character_inventory_list.Add(
                            "<slot>" & slot & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid &
                            "</bagguid><item>" & entryid & "</item><enchant>" & enchantments & "</enchant><count>" &
                            itemcount & "</count>")
                    End If


                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getinventoryitems_old()
        runfunction.writelog("getinventoryitems_call @trinity")
        Dim tmpext As Integer

        Dim _
            da As _
                New MySqlDataAdapter("SELECT slot FROM character_inventory WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                     Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    tmpext = CInt(Val(readedcode))
                    Dim bagguid As String =
                            runfunction.runcommand(
                                "SELECT bag FROM character_inventory WHERE guid='" & Main.MainInstance.char_guid.ToString &
                                "' AND slot='" & tmpext.ToString & "'", "bag")
                    If CInt(bagguid) = 0 Then
                        If tmpext > 18 Then
                            Dim bag As String = "0"
                            Dim item As String = "0"
                            Dim entryid As String
                            Dim enchantments As String


                            bag = bagguid


                            item =
                                runfunction.runcommand(
                                    "SELECT item FROM character_inventory WHERE guid='" & Main.MainInstance.char_guid.ToString &
                                    "' AND slot='" & tmpext.ToString & "'", "item")
                            entryid =
                                runfunction.runcommand("SELECT itemEntry FROM item_instance WHERE guid = '" & item & "'",
                                                       "itemEntry")
                            enchantments =
                                runfunction.runcommand(
                                    "SELECT enchantments FROM item_instance WHERE guid = '" & item & "'", "enchantments")
                            Main.MainInstance.character_inventoryzero_list.Add(
                                "<slot>" & tmpext.ToString & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid &
                                "</bagguid><item>" & entryid & "</item><enchant>" & enchantments & "</enchant>")
                        End If
                    Else
                        Dim bag As String = "0"
                        Dim item As String = "0"
                        Dim entryid As String
                        Dim enchantments As String

                        bag =
                            runfunction.runcommand("SELECT itemEntry FROM item_instance WHERE guid = '" & bagguid & "'",
                                                   "itemEntry")


                        item =
                            runfunction.runcommand(
                                "SELECT item FROM character_inventory WHERE guid='" & Main.MainInstance.char_guid.ToString &
                                "' AND slot='" & tmpext.ToString & "'", "item")
                        entryid =
                            runfunction.runcommand("SELECT itemEntry FROM item_instance WHERE guid = '" & item & "'",
                                                   "itemEntry")
                        enchantments =
                            runfunction.runcommand("SELECT enchantments FROM item_instance WHERE guid = '" & item & "'",
                                                   "enchantments")
                        Main.MainInstance.character_inventory_list.Add(
                            "<slot>" & tmpext.ToString & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid &
                            "</bagguid><item>" & entryid & "</item><enchant>" & enchantments & "</enchant>")
                    End If


                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getglyphs()
        runfunction.writelog("getglyphs_call @trinity")
        Dim prevglyphid As Integer
        Dim glyphid As Integer
        Dim glyphname As String = ""
        Dim glyphpic As Image = My.Resources.empty
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph7 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'",
                            "glyph7")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.prim1.Text = glyphname
                Main.MainInstance.primeglyph1 = glyphid.ToString
                Glyphs.prim1.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.prim1pic)
            End If
        Catch ex As Exception
            Glyphs.prim1pic.Image = My.Resources.empty
        End Try


        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph8 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'",
                            "glyph8")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.prim2.Text = glyphname
                Main.MainInstance.primeglyph2 = glyphid.ToString
                Glyphs.prim2.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.prim2pic)
            End If
        Catch ex As Exception
            Glyphs.prim2pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph9 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'",
                            "glyph9")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.prim3.Text = glyphname
                Main.MainInstance.primeglyph3 = glyphid.ToString
                Glyphs.prim3.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.prim3pic)
            End If
        Catch ex As Exception
            Glyphs.prim3pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph1 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'",
                            "glyph1")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.erheb1.Text = glyphname
                Main.MainInstance.majorglyph1 = glyphid.ToString
                Glyphs.erheb1.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.erheb1pic)
            End If
        Catch ex As Exception
            Glyphs.erheb1pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph4 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'",
                            "glyph4")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.erheb2.Text = glyphname
                Main.MainInstance.majorglyph2 = glyphid.ToString
                Glyphs.erheb2.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.erheb2pic)
            End If
        Catch ex As Exception
            Glyphs.erheb2pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph6 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'",
                            "glyph6")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.erheb3.Text = glyphname
                Main.MainInstance.majorglyph3 = glyphid.ToString
                Glyphs.erheb3.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.erheb3pic)
            End If
        Catch ex As Exception
            Glyphs.erheb3pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph2 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'",
                            "glyph2")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.gering1.Text = glyphname
                Main.MainInstance.minorglyph1 = glyphid.ToString
                Glyphs.gering1.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.gering1pic)
            End If
        Catch ex As Exception
            Glyphs.gering1pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph3 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'",
                            "glyph3")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.gering2.Text = glyphname
                Main.MainInstance.minorglyph2 = glyphid.ToString
                Glyphs.gering2.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.gering2pic)
            End If
        Catch ex As Exception
            Glyphs.gering2pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph5 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'",
                            "glyph5")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.gering3.Text = glyphname
                Main.MainInstance.minorglyph3 = glyphid.ToString
                Glyphs.gering3.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.gering3pic)
            End If
        Catch ex As Exception
            Glyphs.gering3pic.Image = My.Resources.empty
        End Try
    End Sub

    Public Sub getsecglyphs()
        runfunction.writelog("getsecglyphs_call @trinity")
        Dim prevglyphid As Integer
        Dim glyphid As Integer
        Dim glyphname As String = ""
        Dim glyphpic As Image = My.Resources.empty
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph7 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'",
                            "glyph7")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secprim1.Text = glyphname
                Main.MainInstance.secprimeglyph1 = glyphid.ToString
                Glyphs.secprim1.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secprim1pic)
            End If
        Catch ex As Exception
            Glyphs.secprim1pic.Image = My.Resources.empty
        End Try


        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph8 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'",
                            "glyph8")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secprim2.Text = glyphname
                Main.MainInstance.secprimeglyph2 = glyphid.ToString
                Glyphs.secprim2.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secprim2pic)
            End If
        Catch ex As Exception
            Glyphs.secprim2pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph9 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'",
                            "glyph9")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secprim3.Text = glyphname
                Main.MainInstance.secprimeglyph3 = glyphid.ToString
                Glyphs.secprim3.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secprim3pic)
            End If
        Catch ex As Exception
            Glyphs.secprim3pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph1 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'",
                            "glyph1")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secerheb1.Text = glyphname
                Main.MainInstance.secmajorglyph1 = glyphid.ToString
                Glyphs.secerheb1.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secerheb1pic)
            End If
        Catch ex As Exception
            Glyphs.secerheb1pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph4 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'",
                            "glyph4")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secerheb2.Text = glyphname
                Main.MainInstance.secmajorglyph2 = glyphid.ToString
                Glyphs.secerheb2.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secerheb2pic)
            End If
        Catch ex As Exception
            Glyphs.secerheb2pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph6 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'",
                            "glyph6")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secerheb3.Text = glyphname
                Main.MainInstance.secmajorglyph3 = glyphid.ToString
                Glyphs.secerheb3.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secerheb3pic)
            End If
        Catch ex As Exception
            Glyphs.secerheb3pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph2 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'",
                            "glyph2")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secgering1.Text = glyphname
                Main.MainInstance.secminorglyph1 = glyphid.ToString
                Glyphs.secgering1.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secgering1pic)
            End If
        Catch ex As Exception
            Glyphs.secgering1pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph3 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'",
                            "glyph3")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secgering2.Text = glyphname
                Main.MainInstance.secminorglyph2 = glyphid.ToString
                Glyphs.secgering2.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secgering2pic)
            End If
        Catch ex As Exception
            Glyphs.secgering2pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph5 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'",
                            "glyph5")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secgering3.Text = glyphname
                Main.MainInstance.secminorglyph3 = glyphid.ToString
                Glyphs.secgering3.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secgering3pic)
            End If
        Catch ex As Exception
            Glyphs.secgering3pic.Image = My.Resources.empty
        End Try
    End Sub

    Public Sub saveglyphs()
        runfunction.writelog("saveglyphs_call @trinity")
        Main.MainInstance.textprimeglyph1 = Glyphs.prim1.Text
        Main.MainInstance.textprimeglyph2 = Glyphs.prim2.Text
        Main.MainInstance.textprimeglyph3 = Glyphs.prim3.Text
        Main.MainInstance.textmajorglyph1 = Glyphs.erheb1.Text
        Main.MainInstance.textmajorglyph2 = Glyphs.erheb2.Text
        Main.MainInstance.textmajorglyph3 = Glyphs.erheb3.Text
        Main.MainInstance.textminorglyph1 = Glyphs.gering1.Text
        Main.MainInstance.textminorglyph2 = Glyphs.gering2.Text
        Main.MainInstance.textminorglyph3 = Glyphs.gering3.Text

        Main.MainInstance.glyphpic1 = Glyphs.prim1pic.Image
        Main.MainInstance.glyphpic2 = Glyphs.prim2pic.Image
        Main.MainInstance.glyphpic3 = Glyphs.prim3pic.Image
        Main.MainInstance.glyphpic4 = Glyphs.erheb1pic.Image
        Main.MainInstance.glyphpic5 = Glyphs.erheb2pic.Image
        Main.MainInstance.glyphpic6 = Glyphs.erheb3pic.Image
        Main.MainInstance.glyphpic7 = Glyphs.gering1pic.Image
        Main.MainInstance.glyphpic8 = Glyphs.gering2pic.Image
        Main.MainInstance.glyphpic9 = Glyphs.gering3pic.Image

        Main.MainInstance.sectextprimeglyph1 = Glyphs.secprim1.Text
        Main.MainInstance.sectextprimeglyph2 = Glyphs.secprim2.Text
        Main.MainInstance.sectextprimeglyph3 = Glyphs.secprim3.Text
        Main.MainInstance.sectextmajorglyph1 = Glyphs.secerheb1.Text
        Main.MainInstance.sectextmajorglyph2 = Glyphs.secerheb2.Text
        Main.MainInstance.sectextmajorglyph3 = Glyphs.secerheb3.Text
        Main.MainInstance.sectextminorglyph1 = Glyphs.secgering1.Text
        Main.MainInstance.sectextminorglyph2 = Glyphs.secgering2.Text
        Main.MainInstance.sectextminorglyph3 = Glyphs.secgering3.Text

        Main.MainInstance.secglyphpic1 = Glyphs.secprim1pic.Image
        Main.MainInstance.secglyphpic2 = Glyphs.secprim2pic.Image
        Main.MainInstance.secglyphpic3 = Glyphs.secprim3pic.Image
        Main.MainInstance.secglyphpic4 = Glyphs.secerheb1pic.Image
        Main.MainInstance.secglyphpic5 = Glyphs.secerheb2pic.Image
        Main.MainInstance.secglyphpic6 = Glyphs.secerheb3pic.Image
        Main.MainInstance.secglyphpic7 = Glyphs.secgering1pic.Image
        Main.MainInstance.secglyphpic8 = Glyphs.secgering2pic.Image
        Main.MainInstance.secglyphpic9 = Glyphs.secgering3pic.Image
    End Sub

    Public Sub getitems()
        runfunction.writelog("getitems_call @trinity")
        'Get Instance
        Dim xslot As Integer = 0
        Dim xentryid As Integer
        Dim itemname As String = ""
        Dim realxentryid As Integer
        Do
            Try
                xentryid =
                    CInt(
                        Val(
                            runfunction.runcommand(
                                "SELECT item FROM character_inventory WHERE guid = '" & characterguid & "' AND slot = '" &
                                xslot & "' AND bag='0'", "item")))
                realxentryid =
                    CInt(
                        Val(runfunction.runcommand("SELECT itemEntry FROM item_instance WHERE guid = '" & xentryid & "'",
                                                   "itemEntry")))
                If Main.MainInstance.anzahldurchlaufe = 1 Then itemname = getnamefromid(realxentryid)
                Dim wartemal As String = ""
                runfunction.writelog(
                    "getitems_call @trinity: /tag xentryid=" & xentryid.ToString & " realxentryid=" &
                    realxentryid.ToString)
            Catch ex As Exception

            End Try
            Select Case xslot
                Case 0
                    Main.MainInstance.Kopf.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Kopf.Visible = True
                    Main.MainInstance.kopfid = realxentryid
                    Main.MainInstance.kopfname = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.kopfpic)
                    getitemstats(xentryid, Main.MainInstance.kopfench)
                Case 1
                    Main.MainInstance.Hals.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Hals.Visible = True
                    Main.MainInstance.halsid = realxentryid
                    Main.MainInstance.halsname = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Halspic)
                    getitemstats(xentryid, Main.MainInstance.halsench)
                Case 2
                    Main.MainInstance.Schulter.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Schulter.Visible = True
                    Main.MainInstance.schulterid = realxentryid
                    Main.MainInstance.schultername = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Schulterpic)
                    getitemstats(xentryid, Main.MainInstance.schulterench)
                Case 3
                    Main.MainInstance.Hemd.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Hemd.Visible = True
                    Main.MainInstance.hemdid = realxentryid
                    Main.MainInstance.hemdname = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Hemdpic)
                    getitemstats(xentryid, Main.MainInstance.hemdench)
                Case 4
                    Main.MainInstance.Brust.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Brust.Visible = True
                    Main.MainInstance.brustid = realxentryid
                    Main.MainInstance.brustname = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Brustpic)
                    getitemstats(xentryid, Main.MainInstance.brustench)
                Case 5
                    Main.MainInstance.Guertel.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Guertel.Visible = True
                    Main.MainInstance.guertelid = realxentryid
                    Main.MainInstance.guertelname = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Guertelpic)
                    getitemstats(xentryid, Main.MainInstance.guertelench)
                Case 6
                    Main.MainInstance.Beine.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Beine.Visible = True
                    Main.MainInstance.beineid = realxentryid
                    Main.MainInstance.beinename = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Beinepic)
                    getitemstats(xentryid, Main.MainInstance.beineench)
                Case 7
                    Main.MainInstance.Stiefel.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Stiefel.Visible = True
                    Main.MainInstance.stiefelid = realxentryid
                    Main.MainInstance.stiefelname = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Stiefelpic)
                    getitemstats(xentryid, Main.MainInstance.stiefelench)
                Case 8
                    Main.MainInstance.Handgelenke.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Handgelenke.Visible = True
                    Main.MainInstance.handgelenkeid = realxentryid
                    Main.MainInstance.handgelenkename = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Handgelenkepic)
                    getitemstats(xentryid, Main.MainInstance.handgelenkeench)
                Case 9
                    Main.MainInstance.Haende.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Haende.Visible = True
                    Main.MainInstance.haendeid = realxentryid
                    Main.MainInstance.haendename = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Haendepic)
                    getitemstats(xentryid, Main.MainInstance.haendeench)
                Case 10
                    Main.MainInstance.Ring1.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Ring1.Visible = True
                    Main.MainInstance.ring1id = realxentryid
                    Main.MainInstance.ring1name = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Ring1pic)
                    getitemstats(xentryid, Main.MainInstance.ring1ench)
                Case 11
                    Main.MainInstance.Ring2.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Ring2.Visible = True
                    Main.MainInstance.ring2id = realxentryid
                    Main.MainInstance.ring2name = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Ring2pic)
                    getitemstats(xentryid, Main.MainInstance.ring2ench)
                Case 12
                    Main.MainInstance.Schmuck1.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Schmuck1.Visible = True
                    Main.MainInstance.schmuck1id = realxentryid
                    Main.MainInstance.schmuck1name = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Schmuck1pic)
                    getitemstats(xentryid, Main.MainInstance.schmuck1ench)
                Case 13
                    Main.MainInstance.Schmuck2.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Schmuck2.Visible = True
                    Main.MainInstance.schmuck2id = realxentryid
                    Main.MainInstance.schmuck2name = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Schmuck2pic)
                    getitemstats(xentryid, Main.MainInstance.schmuck2ench)
                Case 14
                    Main.MainInstance.Ruecken.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Ruecken.Visible = True
                    Main.MainInstance.rueckenid = realxentryid
                    Main.MainInstance.rueckenname = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Rueckenpic)
                    getitemstats(xentryid, Main.MainInstance.rueckenench)

                Case 15
                    Main.MainInstance.Haupt.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Haupt.Visible = True
                    Main.MainInstance.hauptid = realxentryid
                    Main.MainInstance.hauptname = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Hauptpic)
                    getitemstats(xentryid, Main.MainInstance.hauptench)
                    runfunction.getweapontype(realxentryid)
                Case 16
                    Main.MainInstance.Off.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Off.Visible = True
                    Main.MainInstance.offid = realxentryid
                    Main.MainInstance.offname = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Offpic)
                    getitemstats(xentryid, Main.MainInstance.offench)
                    runfunction.getweapontype(realxentryid)
                Case 17
                    Main.MainInstance.Distanz.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Distanz.Visible = True
                    Main.MainInstance.distanzid = realxentryid
                    Main.MainInstance.distanzname = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Distanzpic)
                    getitemstats(xentryid, Main.MainInstance.distanzench)
                    runfunction.getweapontype(realxentryid)
                Case 18
                    Main.MainInstance.Wappenrock.Text = itemname
                    If Not itemname = "-" Then Main.MainInstance.Wappenrock.Visible = True
                    Main.MainInstance.wappenrockid = realxentryid
                    Main.MainInstance.wappenrockname = itemname
                    If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.MainInstance.Wappenrockpic)
                    getitemstats(xentryid, Main.MainInstance.wappenrockench)
                Case Else
            End Select
            xslot += 1
        Loop Until xslot = 19
    End Sub

    Public Sub handleenchantments()
        runfunction.writelog("handleenchantments_call @trinity")
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.kopfvz.Text = splitstringvz(Main.MainInstance.kopfench, Main.MainInstance.kopfvzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.kopfsocket1.Text = splitstringgem(Main.MainInstance.kopfench, Main.MainInstance.kopfsocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.kopfsocket2.Text = splitstringgem(Main.MainInstance.kopfench, Main.MainInstance.kopfsocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.kopfsocket3.Text = splitstringgem(Main.MainInstance.kopfench, Main.MainInstance.kopfsocket3id, 12)
        Main.MainInstance.kopfvz.Visible = True


        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.halsvz.Text = splitstringvz(Main.MainInstance.halsench, Main.MainInstance.halsvzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.halssocket1.Text = splitstringgem(Main.MainInstance.halsench, Main.MainInstance.halssocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.halssocket2.Text = splitstringgem(Main.MainInstance.halsench, Main.MainInstance.halssocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.halssocket3.Text = splitstringgem(Main.MainInstance.halsench, Main.MainInstance.halssocket3id, 12)
        Main.MainInstance.halsvz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.schultervz.Text = splitstringvz(Main.MainInstance.schulterench, Main.MainInstance.schultervzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.schultersocket1.Text = splitstringgem(Main.MainInstance.schulterench, Main.MainInstance.schultersocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.schultersocket2.Text = splitstringgem(Main.MainInstance.schulterench, Main.MainInstance.schultersocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.schultersocket3.Text = splitstringgem(Main.MainInstance.schulterench, Main.MainInstance.schultersocket3id, 12)
        Main.MainInstance.schultervz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.rueckenvz.Text = splitstringvz(Main.MainInstance.rueckenench, Main.MainInstance.rueckenvzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.rueckensocket1.Text = splitstringgem(Main.MainInstance.rueckenench, Main.MainInstance.rueckensocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.rueckensocket2.Text = splitstringgem(Main.MainInstance.rueckenench, Main.MainInstance.rueckensocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.rueckensocket3.Text = splitstringgem(Main.MainInstance.rueckenench, Main.MainInstance.rueckensocket3id, 12)
        Main.MainInstance.rueckenvz.Visible = True

        Main.MainInstance.brustvz.Text = splitstringvz(Main.MainInstance.brustench, Main.MainInstance.brustvzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.brustsocket1.Text = splitstringgem(Main.MainInstance.brustench, Main.MainInstance.brustsocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.brustsocket2.Text = splitstringgem(Main.MainInstance.brustench, Main.MainInstance.brustsocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.brustsocket3.Text = splitstringgem(Main.MainInstance.brustench, Main.MainInstance.brustsocket3id, 12)
        Main.MainInstance.brustvz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.handgelenkevz.Text = splitstringvz(Main.MainInstance.handgelenkeench, Main.MainInstance.handgelenkevzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.Handgelenkesocket1.Text = splitstringgem(Main.MainInstance.handgelenkeench, Main.MainInstance.handgelenkesocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.handgelenkesocket2.Text = splitstringgem(Main.MainInstance.handgelenkeench, Main.MainInstance.handgelenkesocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.Handgelenkesocket3.Text = splitstringgem(Main.MainInstance.handgelenkeench, Main.MainInstance.handgelenkesocket3id, 12)
        Main.MainInstance.handgelenkevz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.hauptvz.Text = splitstringvz(Main.MainInstance.hauptench, Main.MainInstance.hauptvzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.Hauptsocket1.Text = splitstringgem(Main.MainInstance.hauptench, Main.MainInstance.hauptsocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.Hauptsocket2.Text = splitstringgem(Main.MainInstance.hauptench, Main.MainInstance.hauptsocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.hauptsocket3.Text = splitstringgem(Main.MainInstance.hauptench, Main.MainInstance.hauptsocket3id, 12)
        Main.MainInstance.hauptvz.Visible = True
        Main.MainInstance.hauptvzlabel2.Visible = True
        Main.MainInstance.hauptvzlabel2.Text = Main.MainInstance.hauptvz.Text

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.offvz.Text = splitstringvz(Main.MainInstance.offench, Main.MainInstance.offvzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.Offsocket1.Text = splitstringgem(Main.MainInstance.offench, Main.MainInstance.offsocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.Offsocket2.Text = splitstringgem(Main.MainInstance.offench, Main.MainInstance.offsocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.offsocket3.Text = splitstringgem(Main.MainInstance.offench, Main.MainInstance.offsocket3id, 12)
        Main.MainInstance.offvz.Visible = True
        Main.MainInstance.offvzlabel2.Visible = True
        Main.MainInstance.offvzlabel2.Text = Main.MainInstance.offvz.Text

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.distanzvz.Text = splitstringvz(Main.MainInstance.distanzench, Main.MainInstance.distanzvzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.Distanzsocket1.Text = splitstringgem(Main.MainInstance.distanzench, Main.MainInstance.distanzsocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.Distanzsocket2.Text = splitstringgem(Main.MainInstance.distanzench, Main.MainInstance.distanzsocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.distanzsocket3.Text = splitstringgem(Main.MainInstance.distanzench, Main.MainInstance.distanzsocket3id, 12)
        Main.MainInstance.distanzvz.Visible = True
        Main.MainInstance.distanzvzlabel2.Visible = True
        Main.MainInstance.distanzvzlabel2.Text = Main.MainInstance.distanzvz.Text

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.haendevz.Text = splitstringvz(Main.MainInstance.haendeench, Main.MainInstance.haendevzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.haendesocket1.Text = splitstringgem(Main.MainInstance.haendeench, Main.MainInstance.haendesocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.haendesocket2.Text = splitstringgem(Main.MainInstance.haendeench, Main.MainInstance.haendesocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.haendesocket3.Text = splitstringgem(Main.MainInstance.haendeench, Main.MainInstance.haendesocket3id, 12)
        Main.MainInstance.haendevz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.guertelvz.Text = splitstringvz(Main.MainInstance.guertelench, Main.MainInstance.guertelvzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.guertelsocket1.Text = splitstringgem(Main.MainInstance.guertelench, Main.MainInstance.guertelsocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.guertelsocket2.Text = splitstringgem(Main.MainInstance.guertelench, Main.MainInstance.guertelsocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.guertelsocket3.Text = splitstringgem(Main.MainInstance.guertelench, Main.MainInstance.guertelsocket3id, 12)
        Main.MainInstance.guertelvz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.beinevz.Text = splitstringvz(Main.MainInstance.beineench, Main.MainInstance.beinevzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.beinesocket1.Text = splitstringgem(Main.MainInstance.beineench, Main.MainInstance.beinesocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.beinesocket2.Text = splitstringgem(Main.MainInstance.beineench, Main.MainInstance.beinesocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.beinesocket3.Text = splitstringgem(Main.MainInstance.beineench, Main.MainInstance.beinesocket3id, 12)
        Main.MainInstance.beinevz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.stiefelvz.Text = splitstringvz(Main.MainInstance.stiefelench, Main.MainInstance.stiefelvzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.stiefelsocket1.Text = splitstringgem(Main.MainInstance.stiefelench, Main.MainInstance.stiefelsocket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.stiefelsocket2.Text = splitstringgem(Main.MainInstance.stiefelench, Main.MainInstance.stiefelsocket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.stiefelsocket3.Text = splitstringgem(Main.MainInstance.stiefelench, Main.MainInstance.stiefelsocket3id, 12)
        Main.MainInstance.stiefelvz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.ring1vz.Text = splitstringvz(Main.MainInstance.ring1ench, Main.MainInstance.ring1vzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.Ring1socket1.Text = splitstringgem(Main.MainInstance.ring1ench, Main.MainInstance.ring1socket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.ring1socket2.Text = splitstringgem(Main.MainInstance.ring1ench, Main.MainInstance.ring1socket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.ring1socket3.Text = splitstringgem(Main.MainInstance.ring1ench, Main.MainInstance.ring1socket3id, 12)
        Main.MainInstance.ring1vz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.ring2vz.Text = splitstringvz(Main.MainInstance.ring2ench, Main.MainInstance.ring2vzid, 0)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.ring2socket1.Text = splitstringgem(Main.MainInstance.ring2ench, Main.MainInstance.ring2socket1id, 6)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.ring2socket2.Text = splitstringgem(Main.MainInstance.ring2ench, Main.MainInstance.ring2socket2id, 9)
        If Main.MainInstance.anzahldurchlaufe = 1 Then _
            Main.MainInstance.ring2socket3.Text = splitstringgem(Main.MainInstance.ring2ench, Main.MainInstance.ring2socket3id, 12)
        Main.MainInstance.ring2vz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.schmuck1vz.Text = splitstringvz(Main.MainInstance.schmuck1ench, Main.MainInstance.schmuck1vzid, 0)
        Main.MainInstance.schmuck1vz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.schmuck2vz.Text = splitstringvz(Main.MainInstance.schmuck2ench, Main.MainInstance.schmuck2vzid, 0)
        Main.MainInstance.schmuck2vz.Visible = True
    End Sub

    Public Function splitstringvz(ByVal input As String, ByRef obvalue As Integer, ByVal position As Integer) As String
        Dim xpacressource As String
        Dim xpacressource2 As String
        Select Case Main.MainInstance.xpac
            Case 3
                xpacressource = My.Resources.GEM_ID_wotlk
                xpacressource2 = My.Resources.VZ_ID_wotlk2
            Case 4
                xpacressource = My.Resources.GEM_ID_cata
                xpacressource2 = My.Resources.VZ_ID_cata2
            Case Else
                xpacressource = My.Resources.GEM_ID_wotlk
                xpacressource2 = My.Resources.VZ_ID_wotlk2
        End Select

        Try
            If input.Contains(" ") Then
                Dim parts() As String = input.Split(" "c)
                If Not parts(position) = "0" Then
                    obvalue = CInt(parts(position))
                    Return runfunction.geteffectnameofeffectid(CInt(parts(position)))
                    'Dim quellcodeyx88 As String = xpacressource2
                    'Dim anfangyx88 As String = parts(position) & ";"
                    'Dim endeyx88 As String = ";xxxx"
                    'Dim quellcodeSplityx88 As String
                    'quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
                    'quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)

                    'Return quellcodeSplityx88
                Else
                    Return ""
                End If
            Else
                Return ""
            End If


        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function splitstringgem(ByVal input As String, ByVal obvalue As Integer, ByVal position As Integer) As String
        Dim xpacressource As String
        Dim xpacressource2 As String
        Select Case Main.MainInstance.xpac
            Case 3
                xpacressource = My.Resources.GEM_ID_wotlk
                xpacressource2 = My.Resources.VZ_ID_wotlk2
            Case 4
                xpacressource = My.Resources.GEM_ID_cata
                xpacressource2 = My.Resources.VZ_ID_cata2
            Case Else
                xpacressource = My.Resources.GEM_ID_wotlk
                xpacressource2 = My.Resources.VZ_ID_wotlk2
        End Select
        Try
            Dim parts() As String = input.Split(" "c)
            If Not parts(position) = "0" Then
                obvalue = CInt(parts(position))
                Return runfunction.geteffectnameofeffectid(CInt(parts(position)))
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function getentrybyiteminstance(ByVal instanceid As Integer) As Integer
        Try
            Return _
                CInt(
                    Val(
                        getentrybyiteminstance(
                            getentrybyiteminstance(
                                CInt(
                                    Val(
                                        runfunction.runcommand(
                                            "SELECT itemEntry FROM character_instance WHERE guid = '" & instanceid & "'",
                                            "itemEntry")))))))

        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function getnamefromid(ByVal itemid As Integer) As String
        Return runfunction.getnamefromid(itemid)
    End Function

    Public Function getcharguid(ByVal charname As String) As Integer
        runfunction.writelog("getcharguid_call @trinity with charname: " & charname)
        Try
            Return _
                CInt(Val(runfunction.runcommand("SELECT guid FROM characters WHERE upper(name)= '" & charname.ToUpper() & "'", "guid")))

        Catch ex As Exception
            Return -1
        End Try
    End Function


    Public Sub getitemstats(ByVal itemguid As Integer, ByRef slotvar As String)
        runfunction.writelog("getitemstats_call @trinity with itemguid: " & itemguid.ToString)
        Try
            slotvar = runfunction.runcommand("SELECT enchantments FROM item_instance WHERE guid='" & itemguid & "'",
                                             "enchantments")
            Dim lol As String = ""
        Catch ex As Exception
            slotvar = "-1"
        End Try
    End Sub


    '##############################################################################################################################################################################
    '##############################################################################################################################################################################
    '##############################################################################################################################################################################
    '##############################################################################################################################################################################
    '##############################################################################################################################################################################
    '##############################################################################################################################################################################


    Public Sub createnewaccounts(ByVal writestring As String)
        runfunction.writelog("createnewaccounts_call @trinity with writestring: " & writestring)
        runfunction.normalsqlcommandRealmd(writestring)
    End Sub

    Public Sub create_new_account_if_not_exist(ByVal accname As String, ByVal command As String, ByVal accguid As String)
        runfunction.writelog(
            "create_new_account_if_not_exist_call @trinity with accname: " & accname & " command: " & command &
            " accguid: " & accguid)
        Try

            '  ("SELECT * FROM characters WHERE name = '" & charname.Text & "'")
            Dim myAdapter As New MySqlDataAdapter

            Dim sqlquery = "SELECT * FROM account WHERE upper(username) = '" & accname.ToUpper() & "'"
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = Main.MainInstance.GLOBALconnRealmd
            myCommand.CommandText = sqlquery
            'start query
            myAdapter.SelectCommand = myCommand
            Dim myData As MySqlDataReader
            myData = myCommand.ExecuteReader()
            If CInt(myData.HasRows) = 0 Then
                myData.Close()
                runfunction.normalsqlcommandRealmd(command)
                Dim realmid As Integer = Main.MainInstance.account_access_RealmID
                If realmid = 0 Then
                    realmid = -1
                ElseIf realmid = Nothing Then
                    realmid = -1
                Else


                End If
                runfunction.normalsqlcommandRealmd(
                    "INSERT INTO account_access ( id, gmlevel, RealmID ) VALUES ( '" & accguid & "', '" &
                    Main.MainInstance.account_access_gmlevel.ToString & "', '" & realmid.ToString & "' )")
                'w
            Else
                myData.Close()

            End If

        Catch
            runfunction.normalsqlcommandRealmd(command)
        End Try
    End Sub

    Public Sub addchars(ByVal targetaccount As String, ByVal charactername As String,
                        ByVal namechangeeverytime As Boolean)
        'ATTENTION WITH CERTAIN REPACKS: "'4,2'" in commands will cause error > change to "'4.2'" // // Extent of damage is unknown -> please check

        runfunction.writelog(
            "addchars_call @trinity with targetaccount: " & targetaccount & " charactername: " & charactername &
            " namechangeeverytime: " & namechangeeverytime.ToString)
        Dim newcharguid As Integer =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", "guid"))) + 1
        guid = newcharguid.ToString
        Main.MainInstance.coreguid = newcharguid.ToString
        targetaccount = runfunction.runcommandRealmd("SELECT id FROM account WHERE username='" & targetaccount & "'",
                                                     "id")
        If namechangeeverytime = True Then
            Process_Status.processreport.AppendText(
                Now.TimeOfDay.ToString & "// Player will be asked to change charactername! : reason-nce=true" &
                vbNewLine)
            runfunction.normalsqlcommand(
                "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `position_x`, position_y, position_z, map, orientation, taximask, cinematic, `health` ) VALUES ( '" &
                newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                "', '0', '0', '0', '1', '0', '0', '" & Main.MainInstance.playerBytes &
                "', '-14306', '515', '10', '0', '5', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1', '1000' )")

            runfunction.normalsqlcommand("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'")
        Else
            If charexist(charactername) = True Then
                Process_Status.processreport.AppendText(
                    Now.TimeOfDay.ToString & "// Player will be asked to change charactername!" & vbNewLine)
                runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `position_x`, position_y, position_z, map, orientation, taximask, cinematic, `health` ) VALUES ( '" &
                    newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '" & Main.MainInstance.playerBytes &
                    "', '-14306', '515', '10', '0', '5', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1', '1000' )")
                runfunction.normalsqlcommand(
                    "UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'")
            Else
                runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `position_x`, position_y, position_z, map, orientation, taximask, cinematic, `health` ) VALUES ( '" &
                    newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '" & Main.MainInstance.playerBytes &
                    "', '-14306', '515', '10', '0', '5', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1', '1000' )")
                runfunction.normalsqlcommand(
                    "UPDATE characters SET at_login='0' WHERE guid='" & newcharguid.ToString & "'")
            End If

        End If
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Creating Hearthstone for Character: " & Main.MainInstance.char_name & vbNewLine)
        Dim newguid As String =
                ((CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)", "guid")))) +
                 1).ToString

        runfunction.normalsqlcommand(
            "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
            newguid & "', '6948', '" & Main.MainInstance.coreguid & "', '1', '0 0 0 0 0 ', '" & newguid &
            " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
        runfunction.normalsqlcommand(
            "INSERT INTO character_inventory ( guid, bag, `slot`, `item` ) VALUES ( '" & Main.MainInstance.coreguid &
            "', '0', '23', '" & newguid & "')")
        If Not Main.MainInstance.finished_quests = "" Then
            Try
                Dim parts() As String = Main.MainInstance.finished_quests.Split(","c)
                Dim excounter As Integer = UBound(Main.MainInstance.finished_quests.Split(CChar(",")))
                Dim startcounter As Integer = 0
                Do
                    Dim questid As String = parts(startcounter)
                    runfunction.normalsqlcommand(
                        "INSERT IGNORE INTO character_queststatus_rewarded ( `guid`, `quest` ) VALUES ( '" &
                        Main.MainInstance.coreguid & "', '" & questid & "' )")
                    startcounter += 1
                Loop Until startcounter = excounter
            Catch
            End Try
        End If

        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Created Character " & charactername & "!" & vbNewLine)
        Application.DoEvents()
    End Sub

    Public Sub sethome()
        runfunction.writelog("sethome_call @trinity")
        runfunction.normalsqlcommand(
            "INSERT INTO character_homebind ( guid, " & Main.MainInstance.homebind_map & ", " & Main.MainInstance.homebind_zone & ", " &
            Main.MainInstance.homebind_posx & ", " & Main.MainInstance.homebind_posy & ", " & Main.MainInstance.homebind_posz & " ) VALUES ( '" & Main.MainInstance.coreguid &
            "', '" & splitlist(Main.MainInstance.character_homebind, "map") & "', '" & splitlist(Main.MainInstance.character_homebind, "zone") &
            "', '" & splitlist(Main.MainInstance.character_homebind, "position_x") & "', '" &
            splitlist(Main.MainInstance.character_homebind, "position_y") & "', '" & splitlist(Main.MainInstance.character_homebind, "position_z") &
            "' )")
    End Sub

    Public Sub adddetailedchar(ByVal targetaccount As String, ByVal charactername As String,
                               ByVal namechangeeverytime As Boolean)
        Debug.WriteLine(Main.MainInstance.position_x.ToString())
        runfunction.writelog(
            "addchars_call @trinity with targetaccount: " & targetaccount & " charactername: " & charactername &
            " namechangeeverytime: " & namechangeeverytime.ToString)
        Dim newcharguid As Integer =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", "guid"))) + 1
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Creating Character " & charactername & "!" & vbNewLine)
        guid = newcharguid.ToString
        Main.MainInstance.coreguid = newcharguid.ToString
        targetaccount = runfunction.runcommandRealmd("SELECT id FROM account WHERE username='" & targetaccount & "'",
                                                     "id")
        If namechangeeverytime = True Then
            Process_Status.processreport.AppendText(
                Now.TimeOfDay.ToString & "// Player will be asked to change charactername! : reason-nce=true" &
                vbNewLine)
            If runfunction.returncountresults("SELECT `knownCurrencies` FROM characters", "knownCurrencies") >= 0 Then
                If runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, `playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, cinematic, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, knownCurrencies, watchedFaction, `health`, speccount, activespec, exploredZones, knownTitles, actionBars ) VALUES ( '" &
                    newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '" & Main.MainInstance.playerBytes.ToString & "', '" &
                    Main.MainInstance.playerBytes2.ToString &
                    "', '" & Main.MainInstance.playerFlags.ToString & "', '" & Main.MainInstance.position_x.ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" &
                    Main.MainInstance.position_y.ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" & (Main.MainInstance.position_z + 5).ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" &
                    Main.MainInstance.map.ToString &
                    "', '4.40671', '" & Main.MainInstance.taximask & "', '1', '" & Main.MainInstance.totaltime.ToString & "', '" &
                    Main.MainInstance.leveltime.ToString &
                    "', '" & Main.MainInstance.extra_flags & "', '" & Main.MainInstance.stable_slots & "', '" & Main.MainInstance.at_login & "', '" &
                    Main.MainInstance.zone.ToString & "', '" & Main.MainInstance.chosenTitle & "', '" & Main.MainInstance.knownCurrencies & "', '" &
                    Main.MainInstance.watchedFaction & "', '5000', '" & Main.MainInstance.speccount.ToString & "', '" & Main.MainInstance.activespec.ToString &
                    "', '" & Main.MainInstance.exploredZones & "', '" & Main.MainInstance.knownTitles & "', '" & Main.MainInstance.actionBars & "' )", True,
                    True) = True Then
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.characternotcreated, MsgBoxStyle.Critical,
                               localeDE.criticalerrorduringtransmission)
                    Else
                        MsgBox(localeEN.characternotcreated, MsgBoxStyle.Critical,
                               localeEN.criticalerrorduringtransmission)
                    End If
                    Connect.Close()
                    Armory2Database.Close()
                    Database2Database.Close()
                    closesql()
                    Process_Status.processreport.AppendText(
                        Now.TimeOfDay.ToString & "// Transmission failed!" & vbNewLine)
                    Process_Status.Button1.Enabled = True
                    Application.DoEvents()
                    Main.MainInstance.Close()
                    Starter.Show()
                    Exit Sub
                End If
            Else
                If runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, `playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, cinematic, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, watchedFaction, `health`, speccount, activespec, exploredZones, knownTitles, actionBars ) VALUES ( '" &
                    newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '" & Main.MainInstance.playerBytes.ToString & "', '" &
                    Main.MainInstance.playerBytes2.ToString &
                    "', '" & Main.MainInstance.playerFlags.ToString & "', '" & Main.MainInstance.position_x.ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" &
                    Main.MainInstance.position_y.ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" & (Main.MainInstance.position_z + 5).ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" &
                    Main.MainInstance.map.ToString &
                    "', '4.40671', '" & Main.MainInstance.taximask & "', '1', '" & Main.MainInstance.totaltime.ToString & "', '" &
                    Main.MainInstance.leveltime.ToString &
                    "', '" & Main.MainInstance.extra_flags & "', '" & Main.MainInstance.stable_slots & "', '" & Main.MainInstance.at_login & "', '" &
                    Main.MainInstance.zone.ToString & "', '" & Main.MainInstance.chosenTitle & "', '" &
                    Main.MainInstance.watchedFaction & "', '5000', '" & Main.MainInstance.speccount.ToString & "', '" & Main.MainInstance.activespec.ToString &
                    "', '" & Main.MainInstance.exploredZones & "', '" & Main.MainInstance.knownTitles & "', '" & Main.MainInstance.actionBars & "' )", True,
                    True) = True Then
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.characternotcreated, MsgBoxStyle.Critical,
                               localeDE.criticalerrorduringtransmission)
                    Else
                        MsgBox(localeEN.characternotcreated, MsgBoxStyle.Critical,
                               localeEN.criticalerrorduringtransmission)
                    End If
                    Connect.Close()
                    Armory2Database.Close()
                    Database2Database.Close()
                    closesql()
                    Process_Status.processreport.AppendText(
                        Now.TimeOfDay.ToString & "// Transmission failed!" & vbNewLine)
                    Process_Status.Button1.Enabled = True
                    Application.DoEvents()
                    Main.MainInstance.Close()
                    Starter.Show()
                    Exit Sub
                End If
            End If


            runfunction.normalsqlcommand("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'")
        Else
            If charexist(charactername) = True Then
                Process_Status.processreport.AppendText(
                    Now.TimeOfDay.ToString & "// Player will be asked to change charactername!" & vbNewLine)
                If runfunction.returncountresults("SELECT `knownCurrencies` FROM characters", "knownCurrencies") >= 0 _
                    Then
                    If runfunction.normalsqlcommand(
                        "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, `playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, cinematic, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, knownCurrencies, watchedFaction, `health`, speccount, activespec, exploredZones, knownTitles, actionBars ) VALUES ( '" &
                        newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                        "', '0', '0', '0', '1', '0', '0', '" & Main.MainInstance.playerBytes.ToString & "', '" &
                        Main.MainInstance.playerBytes2.ToString & "', '" & Main.MainInstance.playerFlags.ToString & "', '" &
                        Main.MainInstance.position_x.ToString(CultureInfo.InvariantCulture.NumberFormat) &
                        "', '" & Main.MainInstance.position_y.ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" & (Main.MainInstance.position_z + 5).ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" &
                        Main.MainInstance.map.ToString & "', '4.40671', '" & Main.MainInstance.taximask & "', '1', '" & Main.MainInstance.totaltime.ToString &
                        "', '" &
                        Main.MainInstance.leveltime.ToString & "', '" & Main.MainInstance.extra_flags & "', '" & Main.MainInstance.stable_slots & "', '" &
                        Main.MainInstance.at_login & "', '" & Main.MainInstance.zone.ToString & "', '" & Main.MainInstance.chosenTitle & "', '" &
                        Main.MainInstance.knownCurrencies & "', '" & Main.MainInstance.watchedFaction & "', '5000', '" & Main.MainInstance.speccount.ToString &
                        "', '" & Main.MainInstance.activespec.ToString & "', '" & Main.MainInstance.exploredZones & "', '" & Main.MainInstance.knownTitles &
                        "', '" &
                        Main.MainInstance.actionBars & "' )", True, True) = True Then
                        If My.Settings.language = "de" Then
                            MsgBox(localeDE.characternotcreated, MsgBoxStyle.Critical,
                                   localeDE.criticalerrorduringtransmission)
                        Else
                            MsgBox(localeEN.characternotcreated, MsgBoxStyle.Critical,
                                   localeEN.criticalerrorduringtransmission)
                        End If
                        Connect.Close()
                        Armory2Database.Close()
                        Database2Database.Close()
                        closesql()
                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Transmission failed!" & vbNewLine)
                        Process_Status.Button1.Enabled = True
                        Application.DoEvents()
                        Main.MainInstance.Close()
                        Starter.Show()
                        Exit Sub
                    Else
                        If runfunction.normalsqlcommand(
                            "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, `playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, cinematic, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, watchedFaction, `health`, speccount, activespec, exploredZones, knownTitles, actionBars ) VALUES ( '" &
                            newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                            "', '0', '0', '0', '1', '0', '0', '" & Main.MainInstance.playerBytes.ToString & "', '" &
                            Main.MainInstance.playerBytes2.ToString & "', '" & Main.MainInstance.playerFlags.ToString & "', '" &
                            Main.MainInstance.position_x.ToString(CultureInfo.InvariantCulture.NumberFormat) &
                            "', '" & Main.MainInstance.position_y.ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" & (Main.MainInstance.position_z + 5).ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" &
                            Main.MainInstance.map.ToString & "', '4.40671', '" & Main.MainInstance.taximask & "', '1', '" &
                            Main.MainInstance.totaltime.ToString & "', '" &
                            Main.MainInstance.leveltime.ToString & "', '" & Main.MainInstance.extra_flags & "', '" & Main.MainInstance.stable_slots & "', '" &
                            Main.MainInstance.at_login & "', '" & Main.MainInstance.zone.ToString & "', '" & Main.MainInstance.chosenTitle & "', '" &
                            Main.MainInstance.watchedFaction & "', '5000', '" & Main.MainInstance.speccount.ToString &
                            "', '" & Main.MainInstance.activespec.ToString & "', '" & Main.MainInstance.exploredZones & "', '" & Main.MainInstance.knownTitles &
                            "', '" &
                            Main.MainInstance.actionBars & "' )", True, True) = True Then
                            If My.Settings.language = "de" Then
                                MsgBox(localeDE.characternotcreated, MsgBoxStyle.Critical,
                                       localeDE.criticalerrorduringtransmission)
                            Else
                                MsgBox(localeEN.characternotcreated, MsgBoxStyle.Critical,
                                       localeEN.criticalerrorduringtransmission)
                            End If
                            Connect.Close()
                            Armory2Database.Close()
                            Database2Database.Close()
                            closesql()
                            Process_Status.processreport.AppendText(
                                Now.TimeOfDay.ToString & "// Transmission failed!" & vbNewLine)
                            Process_Status.Button1.Enabled = True
                            Application.DoEvents()
                            Main.MainInstance.Close()
                            Starter.Show()
                            Exit Sub
                        End If
                    End If
                End If

                runfunction.normalsqlcommand(
                    "UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'")
            Else
                If runfunction.returncountresults("SELECT `knownCurrencies` FROM characters", "knownCurrencies") >= 0 _
                    Then
                    If runfunction.normalsqlcommand(
                        "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, `playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, cinematic, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, knownCurrencies, watchedFaction, `health`, speccount, activespec, exploredZones, knownTitles, actionBars ) VALUES ( '" &
                        newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                        "', '0', '0', '0', '1', '0', '0', '" & Main.MainInstance.playerBytes.ToString & "', '" &
                        Main.MainInstance.playerBytes2.ToString & "', '" & Main.MainInstance.playerFlags.ToString & "', '" &
                        Main.MainInstance.position_x.ToString(CultureInfo.InvariantCulture.NumberFormat) &
                        "', '" & Main.MainInstance.position_y.ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" & (Main.MainInstance.position_z + 5).ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" &
                        Main.MainInstance.map.ToString & "', '4.40671', '" & Main.MainInstance.taximask & "', '1', '" & Main.MainInstance.totaltime.ToString &
                        "', '" &
                        Main.MainInstance.leveltime.ToString & "', '" & Main.MainInstance.extra_flags & "', '" & Main.MainInstance.stable_slots & "', '" &
                        Main.MainInstance.at_login & "', '" & Main.MainInstance.zone.ToString & "', '" & Main.MainInstance.chosenTitle & "', '" &
                        Main.MainInstance.knownCurrencies & "', '" & Main.MainInstance.watchedFaction & "', '5000', '" & Main.MainInstance.speccount.ToString &
                        "', '" & Main.MainInstance.activespec.ToString & "', '" & Main.MainInstance.exploredZones & "', '" & Main.MainInstance.knownTitles &
                        "', '" &
                        Main.MainInstance.actionBars & "' )", True, True) = True Then
                        If My.Settings.language = "de" Then
                            MsgBox(localeDE.characternotcreated, MsgBoxStyle.Critical,
                                   localeDE.criticalerrorduringtransmission)
                        Else
                            MsgBox(localeEN.characternotcreated, MsgBoxStyle.Critical,
                                   localeEN.criticalerrorduringtransmission)
                        End If
                        Connect.Close()
                        Armory2Database.Close()
                        Database2Database.Close()
                        closesql()
                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Transmission failed!" & vbNewLine)
                        Process_Status.Button1.Enabled = True
                        Application.DoEvents()
                        Main.MainInstance.Close()
                        Starter.Show()
                        Exit Sub
                    End If
                Else
                    If runfunction.normalsqlcommand(
                        "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, `playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, cinematic, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, watchedFaction, `health`, speccount, activespec, exploredZones, knownTitles, actionBars ) VALUES ( '" &
                        newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                        "', '0', '0', '0', '1', '0', '0', '" & Main.MainInstance.playerBytes.ToString & "', '" &
                        Main.MainInstance.playerBytes2.ToString & "', '" & Main.MainInstance.playerFlags.ToString & "', '" &
                        Main.MainInstance.position_x.ToString(CultureInfo.InvariantCulture.NumberFormat) &
                        "', '" & Main.MainInstance.position_y.ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" & (Main.MainInstance.position_z + 5).ToString(CultureInfo.InvariantCulture.NumberFormat) & "', '" &
                        Main.MainInstance.map.ToString & "', '4.40671', '" & Main.MainInstance.taximask & "', '1', '" & Main.MainInstance.totaltime.ToString &
                        "', '" &
                        Main.MainInstance.leveltime.ToString & "', '" & Main.MainInstance.extra_flags & "', '" & Main.MainInstance.stable_slots & "', '" &
                        Main.MainInstance.at_login & "', '" & Main.MainInstance.zone.ToString & "', '" & Main.MainInstance.chosenTitle & "', '" &
                        Main.MainInstance.watchedFaction & "', '5000', '" & Main.MainInstance.speccount.ToString &
                        "', '" & Main.MainInstance.activespec.ToString & "', '" & Main.MainInstance.exploredZones & "', '" & Main.MainInstance.knownTitles &
                        "', '" &
                        Main.MainInstance.actionBars & "' )", True, True) = True Then
                        If My.Settings.language = "de" Then
                            MsgBox(localeDE.characternotcreated, MsgBoxStyle.Critical,
                                   localeDE.criticalerrorduringtransmission)
                        Else
                            MsgBox(localeEN.characternotcreated, MsgBoxStyle.Critical,
                                   localeEN.criticalerrorduringtransmission)
                        End If
                        Connect.Close()
                        Armory2Database.Close()
                        Database2Database.Close()
                        closesql()
                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Transmission failed!" & vbNewLine)
                        Process_Status.Button1.Enabled = True
                        Application.DoEvents()
                        Main.MainInstance.Close()
                        Starter.Show()
                        Exit Sub
                    End If
                End If

            End If

        End If
        If Not Main.MainInstance.finished_quests = "" Then
            Try
                Dim parts() As String = Main.MainInstance.finished_quests.Split(","c)
                Dim excounter As Integer = UBound(Main.MainInstance.finished_quests.Split(CChar(",")))
                Dim startcounter As Integer = 0
                Do
                    Dim questid As String = parts(startcounter)
                    runfunction.normalsqlcommand(
                        "INSERT IGNORE INTO character_queststatus_rewarded ( `guid`, `quest` ) VALUES ( '" &
                        Main.MainInstance.coreguid & "', '" & questid & "' )")
                    startcounter += 1
                Loop Until startcounter = excounter
            Catch
            End Try
        End If
        setqueststatus()
        sethome()
        addaction()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Created Character " & charactername & "!" & vbNewLine)
        Application.DoEvents()
    End Sub

    Public Sub requestnamechange(ByVal charname As String)
        runfunction.writelog("sethome_call @trinity with charname: " & charname)
        runfunction.normalsqlcommand("UPDATE characters SET at_login='1' WHERE upper(name)='" & charname.ToUpper() & "'")
    End Sub

    Public Function charexist(ByVal charname As String) As Boolean
        runfunction.writelog("charexist_call @trinity with charname: " & charname)
        Try

            '  ("SELECT * FROM characters WHERE name = '" & charname.Text & "'")
            Dim myAdapter As New MySqlDataAdapter

            Dim sqlquery = "SELECT * FROM characters WHERE upper(name) = '" & charname.ToUpper() & "'"
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = Main.MainInstance.GLOBALconn
            myCommand.CommandText = sqlquery
            'start query
            myAdapter.SelectCommand = myCommand
            Dim myData As MySqlDataReader
            myData = myCommand.ExecuteReader()
            If CInt(myData.HasRows) = 0 Then
                myData.Close()
                Return False
            Else
                myData.Close()
                Return True

            End If

        Catch
            Return False
        End Try
    End Function

    Public Sub updatechars(ByVal charname As String)
        runfunction.writelog("updatechars_call @trinity with charname: " & charname)
        '  Dim accguid As String = runfunction.runcommandRealmd("SELECT id FROM account WHERE username='" & accountname & "'", "id")

        runfunction.normalsqlcommand("UPDATE characters SET at_login='1' WHERE upper(name)='" & charname.ToUpper() & "'")
    End Sub

    Public Sub getguidfromname(ByVal charactername As String)
        runfunction.writelog("getguidfromname_call @trinity with charactername: " & charactername)
        guid = runfunction.runcommand("SELECT guid FROM characters WHERE upper(name) = '" & charactername.ToUpper() & "'", "guid")
        Main.MainInstance.coreguid = guid
        addaction()
    End Sub

    Public Sub additems()
        runfunction.writelog("additems_call @trinity")
        guid = Main.MainInstance.coreguid
        finalstring =
            "kopf 0 hals 0 schulter 0 hemd 0 brust 0 guertel 0 beine 0 stiefel 0 handgelenke 0 haende 0 finger1 0 finger2 0 schmuck1 0 schmuck2 0 ruecken 0 haupt 0 off 0 distanz 0 wappenrock 0 "
        lastnumber =
            runfunction.runcommand("SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)",
                                   "guid")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Creating Items for Character: " & Main.MainInstance.char_name & vbNewLine)
        Application.DoEvents()
        If Main.MainInstance.char_class = 1 Or Main.MainInstance.char_class = 2 Or Main.MainInstance.char_class = 6 Then
            runfunction.normalsqlcommand(
                "INSERT INTO character_spell ( guid, spell, active, disabled ) VALUES ( '" & guid &
                "', '750', '1', '0' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_skills ( guid, skill, value, max ) VALUES ( '" & guid & "', '293', '1', '1' )")
        ElseIf _
            Main.MainInstance.char_class = 1 Or Main.MainInstance.char_class = 2 Or Main.MainInstance.char_class = 3 Or Main.MainInstance.char_class = 6 Or
            Main.MainInstance.char_class = 7 Then
            runfunction.normalsqlcommand(
                "INSERT INTO character_spell ( guid, spell, active, disabled ) VALUES ( '" & guid &
                "', '8737', '1', '0' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_skills ( guid, skill, value, max ) VALUES ( '" & guid & "', '413', '1', '1' )")
        ElseIf _
            Main.MainInstance.char_class = 1 Or Main.MainInstance.char_class = 2 Or Main.MainInstance.char_class = 3 Or Main.MainInstance.char_class = 4 Or
            Main.MainInstance.char_class = 6 Or Main.MainInstance.char_class = 7 Or Main.MainInstance.char_class = 11 Then
            runfunction.normalsqlcommand(
                "INSERT INTO character_spell ( guid, spell, active, disabled ) VALUES ( '" & guid &
                "', '9077', '1', '0' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_skills ( guid, skill, value, max ) VALUES ( '" & guid & "', '414', '1', '1' )")
        Else

        End If
        Dim specialskill As String
        Dim specialspell As String
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Creating special spells and skills for relevant items..." & vbNewLine)
        Dim spellcounter As Integer = 0
        Dim skillcounter As Integer = 0
        For Each specialskill In Main.MainInstance.specialskills
            runfunction.normalsqlcommand(
                "INSERT INTO character_skills ( guid, skill, value, max ) VALUES ( '" & guid & "', '" & specialskill &
                "', '1', '1' )")
            skillcounter += 1
        Next
        For Each specialspell In Main.MainInstance.specialspells
            runfunction.normalsqlcommand(
                "INSERT INTO character_spell ( guid, spell, active, disabled ) VALUES ( '" & guid & "', '" &
                specialspell & "', '1', '0' )")
            spellcounter += 1
        Next
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Created " & spellcounter.ToString & " spells and " & skillcounter.ToString &
            " skills!" & vbNewLine)

        If Not Main.MainInstance.kopfid = Nothing Then

            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            kopfwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.kopfid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.kopfid, "kopf", False)
            checkexist_anddelete(guid, "0", lastnumber)
        End If

        If Not Main.MainInstance.halsid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            halswearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.halsid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.halsid, "hals", False)
            checkexist_anddelete(guid, "1", lastnumber)
        End If
        If Not Main.MainInstance.schulterid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            schulterwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.schulterid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.schulterid, "schulter", False)
            checkexist_anddelete(guid, "2", lastnumber)
        End If

        If Not Main.MainInstance.rueckenid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            rueckenwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.rueckenid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.rueckenid, "ruecken", False)
            checkexist_anddelete(guid, "14", lastnumber)
        End If
        If Not Main.MainInstance.brustid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            brustwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.brustid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.brustid, "brust", False)
            checkexist_anddelete(guid, "4", lastnumber)
        End If
        If Not Main.MainInstance.hemdid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            hemdwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.hemdid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.hemdid, "hemd", False)
            checkexist_anddelete(guid, "3", lastnumber)
        End If
        If Not Main.MainInstance.wappenrockid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            wappenrockwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.wappenrockid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.wappenrockid, "wappenrock", False)
            checkexist_anddelete(guid, "18", lastnumber)
        End If
        If Not Main.MainInstance.handgelenkeid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            handgelenkewearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.handgelenkeid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.handgelenkeid, "handgelenke", False)
            checkexist_anddelete(guid, "8", lastnumber)
        End If
        If Not Main.MainInstance.hauptid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            hauptwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.hauptid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.hauptid, "haupt", False)
            checkexist_anddelete(guid, "15", lastnumber)
        End If
        If Not Main.MainInstance.offid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            offwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.offid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.offid, "off", False)
            checkexist_anddelete(guid, "16", lastnumber)
        End If
        If Not Main.MainInstance.distanzid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            distanzwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.distanzid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.distanzid, "distanz", False)
            checkexist_anddelete(guid, "17", lastnumber)
        End If
        If Not Main.MainInstance.haendeid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            haendewearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.haendeid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.haendeid, "haende", False)
            checkexist_anddelete(guid, "9", lastnumber)
        End If
        If Not Main.MainInstance.guertelid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            guertelwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.guertelid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.guertelid, "guertel", False)
            checkexist_anddelete(guid, "5", lastnumber)
        End If
        If Not Main.MainInstance.beineid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            beinewearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.beineid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.beineid, "beine", False)
            checkexist_anddelete(guid, "6", lastnumber)
        End If
        If Not Main.MainInstance.stiefelid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            stiefelwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.stiefelid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.stiefelid, "stiefel", False)
            checkexist_anddelete(guid, "7", lastnumber)
        End If
        If Not Main.MainInstance.ring1id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            ring1wearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.ring1id & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.ring1id, "finger1", False)
            checkexist_anddelete(guid, "10", lastnumber)
        End If
        If Not Main.MainInstance.ring2id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            ring2wearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.ring2id & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.ring2id, "finger2", False)
            checkexist_anddelete(guid, "11", lastnumber)
        End If
        If Not Main.MainInstance.schmuck1id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            schmuck1wearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.schmuck1id & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.schmuck1id, "schmuck1", False)
            checkexist_anddelete(guid, "12", lastnumber)
        End If
        If Not Main.MainInstance.schmuck2id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            schmuck2wearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.MainInstance.schmuck2id & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.MainInstance.schmuck2id, "schmuck2", False)
            checkexist_anddelete(guid, "13", lastnumber)
        End If
        makestring(0, "", True)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Created Items!" & vbNewLine)
    End Sub

    Public Sub addglyphs(ByVal expansion As String)
        runfunction.writelog("addglyphs_call @trinity with expansion: " & expansion)
        newcharguid = Main.MainInstance.coreguid
        guid = Main.MainInstance.coreguid
        checkglyphsanddelete(newcharguid)
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding Glyphs to Character: " & Main.MainInstance.char_name & vbNewLine)
        If expansion = "cata" Then
            runfunction.normalsqlcommand(
                "INSERT INTO character_glyphs ( guid, spec, glyph1, glyph2, glyph3, glyph4, glyph5, glyph6, glyph7, glyph8, glyph9 ) VALUES ( '" &
                newcharguid & "', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_glyphs ( guid, spec, glyph1, glyph2, glyph3, glyph4, glyph5, glyph6, glyph7, glyph8, glyph9 ) VALUES ( '" &
                newcharguid & "', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0' )")
            If Not Main.MainInstance.minorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph5 = '" & runfunction.getglyphid2(Main.MainInstance.minorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.minorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph2 = '" & runfunction.getglyphid2(Main.MainInstance.minorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.minorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph3 = '" & runfunction.getglyphid2(Main.MainInstance.minorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.majorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph1 = '" & runfunction.getglyphid2(Main.MainInstance.majorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.majorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph4 = '" & runfunction.getglyphid2(Main.MainInstance.majorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.majorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph6 = '" & runfunction.getglyphid2(Main.MainInstance.majorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.primeglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph7 = '" & runfunction.getglyphid2(Main.MainInstance.primeglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.primeglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph8 = '" & runfunction.getglyphid2(Main.MainInstance.primeglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.primeglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph9 = '" & runfunction.getglyphid2(Main.MainInstance.primeglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If


            If Not Main.MainInstance.secminorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph5 = '" & runfunction.getglyphid2(Main.MainInstance.secminorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secminorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph2 = '" & runfunction.getglyphid2(Main.MainInstance.secminorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secminorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph3 = '" & runfunction.getglyphid2(Main.MainInstance.secminorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secmajorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph1 = '" & runfunction.getglyphid2(Main.MainInstance.secmajorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secmajorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph4 = '" & runfunction.getglyphid2(Main.MainInstance.secmajorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secmajorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph6 = '" & runfunction.getglyphid2(Main.MainInstance.secmajorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secprimeglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph7 = '" & runfunction.getglyphid2(Main.MainInstance.secprimeglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secprimeglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph8 = '" & runfunction.getglyphid2(Main.MainInstance.secprimeglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secprimeglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph9 = '" & runfunction.getglyphid2(Main.MainInstance.secprimeglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
        Else
            runfunction.normalsqlcommand(
                "INSERT INTO character_glyphs ( guid, spec, glyph1, glyph2, glyph3, glyph4, glyph5, glyph6 ) VALUES ( '" &
                newcharguid & "', '0', '0', '0', '0', '0', '0', '0' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_glyphs ( guid, spec, glyph1, glyph2, glyph3, glyph4, glyph5, glyph6 ) VALUES ( '" &
                newcharguid & "', '1', '0', '0', '0', '0', '0', '0' )")
            If Not Main.MainInstance.minorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph5 = '" & runfunction.getglyphid2(Main.MainInstance.minorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.minorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph2 = '" & runfunction.getglyphid2(Main.MainInstance.minorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.minorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph3 = '" & runfunction.getglyphid2(Main.MainInstance.minorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.majorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph1 = '" & runfunction.getglyphid2(Main.MainInstance.majorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.majorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph4 = '" & runfunction.getglyphid2(Main.MainInstance.majorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.MainInstance.majorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph6 = '" & runfunction.getglyphid2(Main.MainInstance.majorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If


            If Not Main.MainInstance.secminorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph5 = '" & runfunction.getglyphid2(Main.MainInstance.secminorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secminorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph2 = '" & runfunction.getglyphid2(Main.MainInstance.secminorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secminorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph3 = '" & runfunction.getglyphid2(Main.MainInstance.secminorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secmajorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph1 = '" & runfunction.getglyphid2(Main.MainInstance.secmajorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secmajorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph4 = '" & runfunction.getglyphid2(Main.MainInstance.secmajorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.MainInstance.secmajorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph6 = '" & runfunction.getglyphid2(Main.MainInstance.secmajorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
        End If
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Added Glyphs!" & vbNewLine)
    End Sub

    Public Sub setgender(ByVal gender As String)
        runfunction.writelog("setgender_call @trinity with gender: " & gender)
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting gender for Character: " & Main.MainInstance.char_name & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand("UPDATE `characters` SET gender='" & gender & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setlevel()
        runfunction.writelog("setlevel_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting Level for Character: " & Main.MainInstance.char_name & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET level='" & Main.MainInstance.char_level.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setrace()
        runfunction.writelog("setrace_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting race for Character: " & Main.MainInstance.char_name & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET race='" & Main.MainInstance.char_race.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setclass()
        runfunction.writelog("setclass_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting class for Character: " & Main.MainInstance.char_name & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET `class`='" & Main.MainInstance.char_class.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setalternatelevel(ByVal alternatelevel As String)
        runfunction.writelog("setalternatelevel_call @trinity with alternatelevel: " & alternatelevel)
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting alternative level for Character: " & Main.MainInstance.char_name & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand("UPDATE `characters` SET level='" & alternatelevel & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setgold(ByVal amount As String)
        runfunction.writelog("setgold_call @trinity with amount: " & amount)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding gold..." & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET money='" & (CInt(Val(amount)) * 10000).ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub addgold(ByVal amount As Integer)
        runfunction.writelog("addgold_call @trinity with amount: " & amount.ToString)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding gold..." & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand("UPDATE `characters` SET money='" & amount.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub addtalents()
        runfunction.writelog("addtalents_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting Talents for Character: " & Main.MainInstance.char_name & vbNewLine)
        If Main.MainInstance.character_talent_list IsNot Nothing Then
            For Each talentstring As String In Main.MainInstance.character_talent_list
                runfunction.normalsqlcommand(
                    "INSERT INTO character_talent ( guid, spell, spec ) VALUES ( '" & Main.MainInstance.coreguid & "', '" &
                    splitlist(talentstring, "spell") & "', '" & splitlist(talentstring, "spec") & "')")

                ' ("<spell>" & spell & "</spell><spec>" & spec & "</spec>")
            Next
        End If

    End Sub

    Public Sub setqueststatus()
        runfunction.writelog("setqueststatus_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting queststatus for Character: " & Main.MainInstance.char_name & vbNewLine)
        For Each queststring As String In Main.MainInstance.character_queststatus
            Dim queststatus As String = splitlist(queststring, "status")
            If queststatus = "0" Then queststatus = "1"
            runfunction.normalsqlcommand(
                "INSERT INTO character_queststatus ( guid, quest, `status`, `explored` ) VALUES ( '" & Main.MainInstance.coreguid &
                "', '" & splitlist(queststring, "quest") & "', '" & queststatus & "', '" &
                splitlist(queststring, "explored") & "')")


        Next
    End Sub

    Public Sub addachievements()
        runfunction.writelog("addachievements_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding achievements for Character: " & Main.MainInstance.char_name & vbNewLine)
        If Main.MainInstance.character_achievement_list IsNot Nothing Then
            For Each avstring As String In Main.MainInstance.character_achievement_list
                runfunction.normalsqlcommand(
                    "INSERT INTO character_achievement ( guid, achievement, date ) VALUES ( '" & Main.MainInstance.coreguid & "', '" &
                    splitlist(avstring, "av") & "', '" & splitlist(avstring, "date") & "')")

                ' "<av>" & avid & "</av><date>" & xdate & "</date>"
            Next

        End If

    End Sub

    Public Sub addskills()
        runfunction.writelog("addskills_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting skills for Character: " & Main.MainInstance.char_name & vbNewLine)
        If Main.MainInstance.character_skills_list IsNot Nothing Then
            For Each skill As String In Main.MainInstance.character_skills_list
                runfunction.normalsqlcommand(
                    "INSERT INTO character_skills ( guid, skill, `value`, `max` ) VALUES ( '" & Main.MainInstance.coreguid & "', '" &
                    splitlist(skill, "skill") & "', '" & splitlist(skill, "value") & "', '" & splitlist(skill, "max") & "')")

                ' "<skill>" & skill & "</skill><value>" & value & "</value><max>" & max & "</max>"
            Next
        End If

    End Sub

    Public Sub addspells()
        runfunction.writelog("addspells_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Teaching spells for Character: " & Main.MainInstance.char_name & vbNewLine)
        If Main.MainInstance.character_spells IsNot Nothing Then
            For Each spell As String In Main.MainInstance.character_spells
                runfunction.normalsqlcommand(
                    "INSERT INTO character_spell ( guid, spell, `active`, `disabled` ) VALUES ( '" & Main.MainInstance.coreguid & "', '" &
                    splitlist(spell, "spell") & "', '" & splitlist(spell, "active") & "', '" & splitlist(spell, "disabled") &
                    "')")

                ' "<spell>" & spell & "</spell><active>" & active & "</active><disabled>" & disabled & "</disabled>"
            Next
        End If

    End Sub

    Public Sub addreputation()
        runfunction.writelog("addreputation_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding reputation for Character: " & Main.MainInstance.char_name & vbNewLine)
        If Main.MainInstance.character_reputatuion_list IsNot Nothing Then
            For Each repstring As String In Main.MainInstance.character_reputatuion_list
                runfunction.normalsqlcommand(
                    "INSERT INTO character_reputation ( guid, faction, `standing`, `flags` ) VALUES ( '" & Main.MainInstance.coreguid &
                    "', '" & splitlist(repstring, "faction") & "', '" & splitlist(repstring, "standing") & "', '" &
                    splitlist(repstring, "flags") & "')")

                ' "<faction>" & faction & "</faction><standing>" & standing & "</standing><flags>" & flags & "</flags>"
            Next
        End If

    End Sub

    Public Sub addaction()
        runfunction.writelog("addaction_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting up actionbars for Character: " & Main.MainInstance.char_name & vbNewLine)
        If Main.MainInstance.character_action_list IsNot Nothing Then
            For Each actionstring As String In Main.MainInstance.character_action_list
                runfunction.normalsqlcommand(
                    "INSERT INTO character_action ( guid, spec, `button`, `action`, `type` ) VALUES ( '" & Main.MainInstance.coreguid &
                    "', '" & splitlist(actionstring, "spec") & "', '" & splitlist(actionstring, "button") & "', '" &
                    splitlist(actionstring, "action") & "', '" & splitlist(actionstring, "type") & "')")

                ' "<action>" & action & "</action><spec>" & spec & "</spec><button>" & gbutton & "</button><type>" & atype & "</atype>"
            Next
        End If

    End Sub

    Public Sub addinventory()
        runfunction.writelog("addinventory_call @trinity")
        Dim bagstring As String = ""
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding Items to inventory for Character: " & Main.MainInstance.char_name & vbNewLine)
        Dim bagexist As List(Of String) = New List(Of String)
        bagexist.Clear()
        For Each inventorystring As String In Main.MainInstance.character_inventoryzero_list
            Dim bagguid As String = splitlist(inventorystring, "bagguid")
            Dim bag As String = splitlist(inventorystring, "bag")
            Dim newguid As String =
                    ((CInt(
                        Val(
                            runfunction.runcommand(
                                "SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)",
                                "guid")))) + 1).ToString


            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                newguid & "', '" & splitlist(inventorystring, "item") & "', '" & Main.MainInstance.coreguid &
                "', '" & splitlist(inventorystring, "count") & "', '0 0 0 0 0 ', '" &
                splitlist(inventorystring, "enchant") & "', '1000' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_inventory ( guid, bag, `slot`, `item` ) VALUES ( '" & Main.MainInstance.coreguid & "', '" &
                bag & "', '" & splitlist(inventorystring, "slot") & "', '" & newguid & "')")
            Select Case splitlist(inventorystring, "slot")
                Case "19", "20", "21", "22", "67", "68", "69", "70", "71", "72", "73"
                    'Item is a bag and has to be registered
                    bagstring = bagstring & "oldguid:" & splitlist(inventorystring, "oldguid") & ";newguid:" & newguid &
                                ";"
                Case Else
            End Select
        Next
        For Each inventorystring As String In Main.MainInstance.character_inventory_list
            Dim bagguid As String = splitlist(inventorystring, "bagguid")
            Dim bag As String = splitlist(inventorystring, "bag")
            Dim newguid As String =
                    ((CInt(
                        Val(
                            runfunction.runcommand(
                                "SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)",
                                "guid")))) + 1).ToString
            Dim newbagguid As String =
                    runfunction.runcommand(
                        "SELECT guid FROM item_instance WHERE itemEntry='" & bag & "' AND owner_guid='" & Main.MainInstance.coreguid &
                        "'", "guid")
            Select Case splitlist(inventorystring, "slot")
                Case "19", "20", "21", "22", "67", "68", "69", "70", "71", "72", "73"
                Case Else
                    Try
                        Dim beginsplit As String = "oldguid:" & splitlist(inventorystring, "bagguid") & ";newguid:"
                        Dim endsplit As String = ";"
                        newbagguid = Split(bagstring, beginsplit, 5)(1)
                        newbagguid = Split(newbagguid, endsplit, 6)(0)
                    Catch ex As Exception

                    End Try

            End Select
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                newguid & "', '" & splitlist(inventorystring, "item") & "', '" & Main.MainInstance.coreguid &
                "', '" & splitlist(inventorystring, "count") & "', '0 0 0 0 0 ', '" &
                splitlist(inventorystring, "enchant") & "', '1000' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_inventory ( guid, bag, `slot`, `item` ) VALUES ( '" & Main.MainInstance.coreguid & "', '" &
                newbagguid & "', '" & splitlist(inventorystring, "slot") & "', '" & newguid & "')")
        Next
    End Sub


    Private Function splitlist(ByVal tstring As String, ByVal category As String) As String
        Try

            Dim xXquellcodeyx88 As String = tstring
            Dim xXanfangyx88 As String = "<" & category & ">"
            Dim xXendeyx88 As String = "</" & category & ">"
            Dim xXquellcodeSplityx88 As String
            xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
            xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
            xXquellcodeSplityx88.Replace(",", ".")
            Return xXquellcodeSplityx88
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub addench()
        runfunction.writelog("addench_call @trinity")
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding item enchantments..." & vbNewLine)
        Application.DoEvents()
        Dim sdjaj As String = Main.MainInstance.kopfench
        If Not Main.MainInstance.kopfench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.kopfench, kopfwearguid, Main.MainInstance.kopfid.ToString) &
                "' WHERE guid='" & kopfwearguid & "'")
        If Not Main.MainInstance.halsench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.halsench, halswearguid, Main.MainInstance.halsid.ToString) &
                "' WHERE guid='" & halswearguid & "'")
        If Not Main.MainInstance.schulterench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.schulterench, schulterwearguid, Main.MainInstance.schulterid.ToString) & "' WHERE guid='" &
                schulterwearguid & "'")
        If Not Main.MainInstance.rueckenench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.rueckenench, rueckenwearguid, Main.MainInstance.rueckenid.ToString) & "' WHERE guid='" &
                rueckenwearguid & "'")
        If Not Main.MainInstance.brustench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.brustench, brustwearguid, Main.MainInstance.brustid.ToString) & "' WHERE guid='" & brustwearguid &
                "'")
        If Not Main.MainInstance.hemdench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.hemdench, hemdwearguid, Main.MainInstance.hemdid.ToString) &
                "' WHERE guid='" & hemdwearguid & "'")
        If Not Main.MainInstance.wappenrockench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.wappenrockench, wappenrockwearguid, Main.MainInstance.wappenrockid.ToString) & "' WHERE guid='" &
                wappenrockwearguid & "'")
        If Not Main.MainInstance.handgelenkeench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.handgelenkeench, handgelenkewearguid, Main.MainInstance.handgelenkeid.ToString) &
                "' WHERE guid='" & handgelenkewearguid & "'")
        If Not Main.MainInstance.haendeench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.haendeench, haendewearguid, Main.MainInstance.haendeid.ToString) & "' WHERE guid='" &
                haendewearguid & "'")
        If Not Main.MainInstance.hauptench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.hauptench, hauptwearguid, Main.MainInstance.hauptid.ToString) & "' WHERE guid='" & hauptwearguid &
                "'")
        If Not Main.MainInstance.offench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.offench, offwearguid, Main.MainInstance.offid.ToString) &
                "' WHERE guid='" & offwearguid & "'")
        If Not Main.MainInstance.distanzench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.distanzench, distanzwearguid, Main.MainInstance.distanzid.ToString) & "' WHERE guid='" &
                distanzwearguid & "'")
        If Not Main.MainInstance.guertelench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.guertelench, guertelwearguid, Main.MainInstance.guertelid.ToString) & "' WHERE guid='" &
                guertelwearguid & "'")
        If Not Main.MainInstance.beineench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.beineench, beinewearguid, Main.MainInstance.beineid.ToString) & "' WHERE guid='" & beinewearguid &
                "'")
        If Not Main.MainInstance.stiefelench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.stiefelench, stiefelwearguid, Main.MainInstance.stiefelid.ToString) & "' WHERE guid='" &
                stiefelwearguid & "'")
        If Not Main.MainInstance.ring1ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.ring1ench, ring1wearguid, Main.MainInstance.ring1id.ToString) & "' WHERE guid='" & ring1wearguid &
                "'")
        If Not Main.MainInstance.ring2ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.ring2ench, ring2wearguid, Main.MainInstance.ring2id.ToString) & "' WHERE guid='" & ring2wearguid &
                "'")
        If Not Main.MainInstance.schmuck1ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.schmuck1ench, schmuck1wearguid, Main.MainInstance.schmuck1id.ToString) & "' WHERE guid='" &
                schmuck1wearguid & "'")
        If Not Main.MainInstance.schmuck2ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.MainInstance.schmuck2ench, schmuck2wearguid, Main.MainInstance.schmuck2id.ToString) & "' WHERE guid='" &
                schmuck2wearguid & "'")
    End Sub

    Private Function splitenchstring(ByVal enchstring As String, ByVal guid As Integer, ByVal entry As String) As String
        Dim numstring As Integer = 0
        Try
            numstring = UBound(enchstring.Split(CChar(" ")))
        Catch
        End Try
        Dim normalenchstring As String = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 "
        If enchstring.Contains(";") Then
            'AECEMU
            Dim excounter As Integer = UBound(enchstring.Split(CChar(";")))
            Dim startcounter As Integer = 0
            Do
                Dim parts() As String = enchstring.Split(";"c)
                Dim partench As String = parts(startcounter)
                Dim parts2() As String = partench.Split(","c)
                If parts2(2) = "5" Then
                    'vz
                    Dim input As String = normalenchstring
                    Dim parts3() As String = input.Split(" "c)
                    parts3(0) = parts2(0)
                    normalenchstring = String.Join(" ", parts3)
                ElseIf parts2(2) = "1" Then
                    'unknown
                ElseIf parts2(2) = "2" Then
                    'gem1
                    Dim input As String = normalenchstring
                    Dim parts3() As String = input.Split(" "c)
                    parts3(6) = parts2(0)
                    normalenchstring = String.Join(" ", parts3)
                ElseIf parts2(2) = "3" Then
                    'gem2
                    Dim input As String = normalenchstring
                    Dim parts3() As String = input.Split(" "c)
                    parts3(9) = parts2(0)
                    normalenchstring = String.Join(" ", parts3)
                ElseIf parts2(2) = "4" Then
                    'gem3
                    Dim input As String = normalenchstring
                    Dim parts3() As String = input.Split(" "c)
                    parts3(12) = parts2(0)
                    normalenchstring = String.Join(" ", parts3)
                End If
                startcounter += 1
            Loop Until startcounter = excounter
            Dim input2 As String = normalenchstring
            Return input2
        Else
            If numstring > 45 Then
                'mangos //todo
                Dim parts2() As String = enchstring.Split(" "c)
                Dim input As String = enchstring
                Dim parts() As String = normalenchstring.Split(" "c)
                Dim output As String
                parts(0) = parts2(22)
                parts(6) = parts2(28)
                parts(9) = parts2(31)
                parts(12) = parts2(34)
                output = String.Join(" ", parts)
                Return output
            Else
                'trinity
                Return enchstring
            End If
        End If
    End Function

    Public Sub addgems()
        runfunction.writelog("addgems_call @trinity")
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding character gems..." & vbNewLine)
        If Main.MainInstance.kopfsocket1id > 0 Then socketinsert(Main.MainInstance.kopfsocket1id.ToString, kopfwearguid.ToString, 7)
        If Main.MainInstance.halssocket1id > 0 Then socketinsert(Main.MainInstance.halssocket1id.ToString, halswearguid.ToString, 7)
        If Main.MainInstance.schultersocket1id > 0 Then socketinsert(Main.MainInstance.schultersocket1id.ToString, schulterwearguid.ToString, 7)
        If Main.MainInstance.rueckensocket1id > 0 Then socketinsert(Main.MainInstance.rueckensocket1id.ToString, rueckenwearguid.ToString, 7)
        If Main.MainInstance.brustsocket1id > 0 Then socketinsert(Main.MainInstance.brustsocket1id.ToString, brustwearguid.ToString, 7)
        If Main.MainInstance.handgelenkesocket1id > 0 Then _
            socketinsert(Main.MainInstance.handgelenkesocket1id.ToString, handgelenkewearguid.ToString, 7)
        If Main.MainInstance.haendesocket1id > 0 Then socketinsert(Main.MainInstance.haendesocket1id.ToString, haendewearguid.ToString, 7)
        If Main.MainInstance.guertelsocket1id > 0 Then
            beltinsert("3729", guertelwearguid.ToString, 19)
            socketinsert(Main.MainInstance.guertelsocket1id.ToString, guertelwearguid.ToString, 7)
        End If

        If Main.MainInstance.beinesocket1id > 0 Then socketinsert(Main.MainInstance.beinesocket1id.ToString, beinewearguid.ToString, 7)
        If Main.MainInstance.stiefelsocket1id > 0 Then socketinsert(Main.MainInstance.stiefelsocket1id.ToString, stiefelwearguid.ToString, 7)
        If Main.MainInstance.ring1socket1id > 0 Then socketinsert(Main.MainInstance.ring1socket1id.ToString, ring1wearguid.ToString, 7)
        If Main.MainInstance.ring2socket1id > 0 Then socketinsert(Main.MainInstance.ring2socket1id.ToString, ring2wearguid.ToString, 7)
        If Main.MainInstance.schmuck1socket1id > 0 Then socketinsert(Main.MainInstance.schmuck1socket1id.ToString, schmuck1wearguid.ToString, 7)
        If Main.MainInstance.schmuck2socket1id > 0 Then socketinsert(Main.MainInstance.schmuck2socket1id.ToString, schmuck2wearguid.ToString, 7)
        If Main.MainInstance.hauptsocket1id > 0 Then socketinsert(Main.MainInstance.hauptsocket1id.ToString, hauptwearguid.ToString, 7)
        If Main.MainInstance.offsocket1id > 0 Then socketinsert(Main.MainInstance.offsocket1id.ToString, offwearguid.ToString, 7)
        If Main.MainInstance.distanzsocket1id > 0 Then socketinsert(Main.MainInstance.distanzsocket1id.ToString, distanzwearguid.ToString, 7)

        If Main.MainInstance.kopfsocket2id > 0 Then socketinsert(Main.MainInstance.kopfsocket2id.ToString, kopfwearguid.ToString, 10)
        If Main.MainInstance.halssocket2id > 0 Then socketinsert(Main.MainInstance.halssocket2id.ToString, halswearguid.ToString, 10)
        If Main.MainInstance.schultersocket2id > 0 Then socketinsert(Main.MainInstance.schultersocket2id.ToString, schulterwearguid.ToString, 10)
        If Main.MainInstance.rueckensocket2id > 0 Then socketinsert(Main.MainInstance.rueckensocket2id.ToString, rueckenwearguid.ToString, 10)
        If Main.MainInstance.brustsocket2id > 0 Then socketinsert(Main.MainInstance.brustsocket2id.ToString, brustwearguid.ToString, 10)
        If Main.MainInstance.handgelenkesocket2id > 0 Then _
            socketinsert(Main.MainInstance.handgelenkesocket2id.ToString, handgelenkewearguid.ToString, 10)
        If Main.MainInstance.haendesocket2id > 0 Then socketinsert(Main.MainInstance.haendesocket2id.ToString, haendewearguid.ToString, 10)
        If Main.MainInstance.guertelsocket2id > 0 Then
            beltinsert("3729", guertelwearguid.ToString, 19)
            socketinsert(Main.MainInstance.guertelsocket2id.ToString, guertelwearguid.ToString, 10)
        End If

        If Main.MainInstance.beinesocket2id > 0 Then socketinsert(Main.MainInstance.beinesocket2id.ToString, beinewearguid.ToString, 10)
        If Main.MainInstance.stiefelsocket2id > 0 Then socketinsert(Main.MainInstance.stiefelsocket2id.ToString, stiefelwearguid.ToString, 10)
        If Main.MainInstance.ring1socket2id > 0 Then socketinsert(Main.MainInstance.ring1socket2id.ToString, ring1wearguid.ToString, 10)
        If Main.MainInstance.ring2socket2id > 0 Then socketinsert(Main.MainInstance.ring2socket2id.ToString, ring2wearguid.ToString, 10)
        If Main.MainInstance.schmuck1socket2id > 0 Then socketinsert(Main.MainInstance.schmuck1socket2id.ToString, schmuck1wearguid.ToString, 10)
        If Main.MainInstance.schmuck2socket2id > 0 Then socketinsert(Main.MainInstance.schmuck2socket2id.ToString, schmuck2wearguid.ToString, 10)
        If Main.MainInstance.hauptsocket2id > 0 Then socketinsert(Main.MainInstance.hauptsocket2id.ToString, hauptwearguid.ToString, 10)
        If Main.MainInstance.offsocket2id > 0 Then socketinsert(Main.MainInstance.offsocket2id.ToString, offwearguid.ToString, 10)
        If Main.MainInstance.distanzsocket2id > 0 Then socketinsert(Main.MainInstance.distanzsocket2id.ToString, distanzwearguid.ToString, 10)

        If Main.MainInstance.kopfsocket3id > 0 Then socketinsert(Main.MainInstance.kopfsocket3id.ToString, kopfwearguid.ToString, 13)
        If Main.MainInstance.halssocket3id > 0 Then socketinsert(Main.MainInstance.halssocket3id.ToString, halswearguid.ToString, 13)
        If Main.MainInstance.schultersocket3id > 0 Then socketinsert(Main.MainInstance.schultersocket3id.ToString, schulterwearguid.ToString, 13)
        If Main.MainInstance.rueckensocket3id > 0 Then socketinsert(Main.MainInstance.rueckensocket3id.ToString, rueckenwearguid.ToString, 13)
        If Main.MainInstance.brustsocket3id > 0 Then socketinsert(Main.MainInstance.brustsocket3id.ToString, brustwearguid.ToString, 13)
        If Main.MainInstance.handgelenkesocket3id > 0 Then _
            socketinsert(Main.MainInstance.handgelenkesocket3id.ToString, handgelenkewearguid.ToString, 13)
        If Main.MainInstance.haendesocket3id > 0 Then socketinsert(Main.MainInstance.haendesocket3id.ToString, haendewearguid.ToString, 13)
        If Main.MainInstance.guertelsocket3id > 0 Then
            beltinsert("3729", guertelwearguid.ToString, 19)
            socketinsert(Main.MainInstance.guertelsocket3id.ToString, guertelwearguid.ToString, 13)
        End If


        If Main.MainInstance.beinesocket3id > 0 Then socketinsert(Main.MainInstance.beinesocket3id.ToString, beinewearguid.ToString, 13)
        If Main.MainInstance.stiefelsocket3id > 0 Then socketinsert(Main.MainInstance.stiefelsocket3id.ToString, stiefelwearguid.ToString, 13)
        If Main.MainInstance.ring1socket3id > 0 Then socketinsert(Main.MainInstance.ring1socket3id.ToString, ring1wearguid.ToString, 13)
        If Main.MainInstance.ring2socket3id > 0 Then socketinsert(Main.MainInstance.ring2socket3id.ToString, ring2wearguid.ToString, 13)
        If Main.MainInstance.schmuck1socket3id > 0 Then socketinsert(Main.MainInstance.schmuck1socket3id.ToString, schmuck1wearguid.ToString, 13)
        If Main.MainInstance.schmuck2socket3id > 0 Then socketinsert(Main.MainInstance.schmuck2socket3id.ToString, schmuck2wearguid.ToString, 13)
        If Main.MainInstance.hauptsocket3id > 0 Then socketinsert(Main.MainInstance.hauptsocket3id.ToString, hauptwearguid.ToString, 13)
        If Main.MainInstance.offsocket3id > 0 Then socketinsert(Main.MainInstance.offsocket3id.ToString, offwearguid.ToString, 13)
        If Main.MainInstance.distanzsocket3id > 0 Then socketinsert(Main.MainInstance.distanzsocket3id.ToString, distanzwearguid.ToString, 13)
    End Sub

    Public Sub addenchantments()
        runfunction.writelog("addenchantments_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding character enchantments..." & vbNewLine)
        If Main.MainInstance.kopfvzid > 0 Then vzinsert(Main.MainInstance.kopfvzid.ToString, kopfwearguid.ToString, 1)
        If Main.MainInstance.halsvzid > 0 Then vzinsert(Main.MainInstance.halsvzid.ToString, halswearguid.ToString, 1)
        If Main.MainInstance.schultervzid > 0 Then vzinsert(Main.MainInstance.schultervzid.ToString, schulterwearguid.ToString, 1)
        If Main.MainInstance.rueckenvzid > 0 Then vzinsert(Main.MainInstance.rueckenvzid.ToString, rueckenwearguid.ToString, 1)
        If Main.MainInstance.brustvzid > 0 Then vzinsert(Main.MainInstance.brustvzid.ToString, brustwearguid.ToString, 1)
        If Main.MainInstance.handgelenkevzid > 0 Then vzinsert(Main.MainInstance.handgelenkevzid.ToString, handgelenkewearguid.ToString, 1)
        If Main.MainInstance.haendevzid > 0 Then vzinsert(Main.MainInstance.haendevzid.ToString, haendewearguid.ToString, 1)
        If Main.MainInstance.guertelvzid > 0 Then vzinsert(Main.MainInstance.guertelvzid.ToString, guertelwearguid.ToString, 1)
        If Main.MainInstance.beinevzid > 0 Then vzinsert(Main.MainInstance.beinevzid.ToString, beinewearguid.ToString, 1)
        If Main.MainInstance.stiefelvzid > 0 Then vzinsert(Main.MainInstance.stiefelvzid.ToString, stiefelwearguid.ToString, 1)
        If Main.MainInstance.ring1vzid > 0 Then vzinsert(Main.MainInstance.ring1vzid.ToString, ring1wearguid.ToString, 1)
        If Main.MainInstance.ring2vzid > 0 Then vzinsert(Main.MainInstance.ring2vzid.ToString, ring2wearguid.ToString, 1)
        If Main.MainInstance.schmuck1vzid > 0 Then vzinsert(Main.MainInstance.schmuck1vzid.ToString, schmuck1wearguid.ToString, 1)
        If Main.MainInstance.schmuck2vzid > 0 Then vzinsert(Main.MainInstance.schmuck2vzid.ToString, schmuck2wearguid.ToString, 1)
        If Main.MainInstance.hauptvzid > 0 Then vzinsert(Main.MainInstance.hauptvzid.ToString, hauptwearguid.ToString, 1)
        If Main.MainInstance.offvzid > 0 Then vzinsert(Main.MainInstance.offvzid.ToString, offwearguid.ToString, 1)
        If Main.MainInstance.distanzvzid > 0 Then vzinsert(Main.MainInstance.distanzvzid.ToString, distanzwearguid.ToString, 1)
    End Sub

    Public Sub addpvp()
        runfunction.writelog("addpvp_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting character honor/kills..." & vbNewLine)

        runfunction.normalsqlcommand(
            "UPDATE `characters` SET arenaPoints='" & Main.MainInstance.arenaPoints.ToString & "' WHERE guid='" & Main.MainInstance.coreguid & "'")
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET totalHonorPoints='" & Main.MainInstance.totalHonorPoints.ToString & "' WHERE guid='" &
            Main.MainInstance.coreguid & "'")
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET totalKills='" & Main.MainInstance.totalKills.ToString & "' WHERE guid='" & Main.MainInstance.coreguid & "'")
    End Sub

    Public Sub socketinsert(ByVal socketid As String, ByVal itemguid As String, ByVal position As Integer)
        Try
            Dim enchantmenttext As String
            enchantmenttext =
                runfunction.runcommand("SELECT enchantments FROM item_instance WHERE guid='" & itemguid & "'",
                                       "enchantments")
            Dim input As String = enchantmenttext
            Dim parts() As String = input.Split(" "c)
            Dim output As String
            parts(position - 1) = runfunction.getgemeffectid(socketid).ToString
            output = String.Join(" ", parts)
            Try
                runfunction.normalsqlcommand(
                    "UPDATE `item_instance` SET enchantments='" & output & "' WHERE guid = '" & itemguid & "'")
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Public Sub beltinsert(ByVal beltid As String, ByVal itemguid As String, ByVal position As Integer)
        Try
            Dim enchantmenttext As String
            enchantmenttext =
                runfunction.runcommand("SELECT enchantments FROM item_instance WHERE guid='" & itemguid & "'",
                                       "enchantments")
            Dim input As String = enchantmenttext
            Dim parts() As String = input.Split(" "c)
            Dim output As String
            parts(position - 1) = beltid
            output = String.Join(" ", parts)
            Try
                runfunction.normalsqlcommand(
                    "UPDATE `item_instance` SET enchantments='" & output & "' WHERE guid = '" & itemguid & "'")
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Public Sub vzinsert(ByVal vzid As String, ByVal itemguid As String, ByVal position As Integer)
        Try
            Dim enchantmenttext As String
            enchantmenttext =
                runfunction.runcommand("SELECT enchantments FROM item_instance WHERE guid='" & itemguid & "'",
                                       "enchantments")
            Dim input As String = enchantmenttext
            Dim parts() As String = input.Split(" "c)
            Dim output As String
            parts(position - 1) = runfunction.getvzeffectid(runfunction.getvzeffectname(CInt(vzid))).ToString
            output = String.Join(" ", parts)
            Try
                runfunction.normalsqlcommand(
                    "UPDATE `item_instance` SET enchantments='" & output & "' WHERE guid = '" & itemguid & "'")
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub checkexist_anddelete(ByVal xguid As String, ByVal xslot As String, ByVal item As String)
        '"SELECT `columname` FROM `tabelle`"

        Try

            '  ("SELECT * FROM characters WHERE name = '" & charname.Text & "'")
            Dim myAdapter As New MySqlDataAdapter

            Dim sqlquery = "SELECT * FROM character_inventory WHERE guid = '" & xguid & "' AND slot = '" & xslot & "'"
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = Main.MainInstance.GLOBALconn
            myCommand.CommandText = sqlquery
            'start query
            myAdapter.SelectCommand = myCommand
            Dim myData As MySqlDataReader
            myData = myCommand.ExecuteReader()
            If CInt(myData.HasRows) = 0 Then
                myData.Close()
                runfunction.normalsqlcommand(
                    "INSERT INTO character_inventory ( guid, slot, item ) VALUES ( '" & xguid & "', '" & xslot & "', '" &
                    item & "' )")

            Else
                myData.Close()
                runfunction.normalsqlcommand(
                    "DELETE FROM character_inventory WHERE guid = '" & xguid & "' AND slot = '" & xslot & "'")
                runfunction.normalsqlcommand(
                    "INSERT INTO character_inventory ( guid, slot, item ) VALUES ( '" & xguid & "', '" & xslot & "', '" &
                    item & "' )")
            End If

        Catch

        End Try
    End Sub

    Private Sub checkglyphsanddelete(ByVal playerguid As String)

        Try

            '  ("SELECT * FROM characters WHERE name = '" & charname.Text & "'")
            Dim myAdapter As New MySqlDataAdapter

            Dim sqlquery = "SELECT * FROM character_glyphs WHERE guid = '" & playerguid & "' AND spec='0'"
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = Main.MainInstance.GLOBALconn
            myCommand.CommandText = sqlquery
            'start query
            myAdapter.SelectCommand = myCommand
            Dim myData As MySqlDataReader
            myData = myCommand.ExecuteReader()
            If CInt(myData.HasRows) = 0 Then
                myData.Close()

            Else
                myData.Close()
                runfunction.normalsqlcommand(
                    "DELETE FROM character_glyphs WHERE guid = '" & playerguid & "' AND spec='0'")

            End If

        Catch

        End Try
        Try

            '  ("SELECT * FROM characters WHERE name = '" & charname.Text & "'")
            Dim myAdapter As New MySqlDataAdapter

            Dim sqlquery = "SELECT * FROM character_glyphs WHERE guid = '" & playerguid & "' AND spec='1'"
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = Main.MainInstance.GLOBALconn
            myCommand.CommandText = sqlquery
            'start query
            myAdapter.SelectCommand = myCommand
            Dim myData As MySqlDataReader
            myData = myCommand.ExecuteReader()
            If CInt(myData.HasRows) = 0 Then
                myData.Close()

            Else
                myData.Close()
                runfunction.normalsqlcommand(
                    "DELETE FROM character_glyphs WHERE guid = '" & playerguid & "' AND spec='1'")

            End If

        Catch

        End Try
    End Sub


    Private Sub makestring(ByVal addstring As Integer, ByVal replace As String, ByVal last As Boolean)
        guid = Main.MainInstance.coreguid
        If last = False Then
            Try
                finalstring = finalstring.Replace(replace, addstring.ToString)
            Catch ex As Exception

            End Try
        Else
            Try
                finalstring = finalstring.Replace("kopf", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("hals", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("schulter", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("hemd", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("brust", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("guertel", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("beine", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("stiefel", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("handgelenke", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("haende", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("finger1", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("finger2", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("schmuck1", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("schmuck2", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("ruecken", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("haupt", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("off", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("distanz", "0")
            Catch ex As Exception

            End Try
            Try
                finalstring = finalstring.Replace("wappenrock", "0")
            Catch ex As Exception
            End Try
            runfunction.normalsqlcommand(
                "UPDATE characters SET equipmentCache='" & finalstring & "' WHERE (guid='" & guid & "')")
        End If
    End Sub
End Class
