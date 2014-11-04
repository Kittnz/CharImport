'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* ArcEmu_core contains several functions to implement the character
'* and account information into an ArcEmu compatible database.
'*
'* Developed by Alcanmage/megasus


Imports MySql.Data.MySqlClient

Public Class ArcEmu_core
    Dim runfunction As New Functions
    Dim talentid As String = ""
    Dim rank As String = ""
    Dim rank2 As String = ""
    Dim sdatatable As New DataTable
    Dim core_check As New Core_Check_Trinity

    Dim characterguid As Integer
    Dim reporttext As RichTextBox = Process_Status.processreport

    Dim quelltext As String = ""
    Dim talentpage As String = ""
    Dim sectalentpage As String = ""
    Dim datacharname As String = ""
    Dim guid As String = ""
    Dim accguid As String = ""
    Dim lastnumber As String = ""

    Dim _
        finalstring As String =
            "kopf 0 hals 0 schulter 0 hemd 0 brust 0 guertel 0 beine 0 stiefel 0 handgelenke 0 haende 0 finger1 0 finger2 0 schmuck1 0 schmuck2 0 ruecken 0 haupt 0 off 0 distanz 0 wappenrock 0 "

    ' Dim newcharguid As String
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
            Main.MainInstance.GLOBALconn.Close()
            Main.MainInstance.GLOBALconn.Dispose()
        End Try
        Try
            Main.MainInstance.GLOBALconnRealmd.Open()
        Catch ex As Exception
            Main.MainInstance.GLOBALconnRealmd.Close()
            Main.MainInstance.GLOBALconnRealmd.Dispose()
        End Try
    End Sub

    Public Sub closesql()

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

    Public Sub getallchars()

        runfunction.writelog("getallchars_call @arcemu")
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
                    Dim _
                        da As _
                            New MySqlDataAdapter(
                                "SELECT `login` FROM `accounts` WHERE lastlogin>='" & My.Settings.lastlogindate1 &
                                "' AND lastlogin<='" & My.Settings.lastlogindate2 & "' AND gm='" &
                                My.Settings.gmlevel.ToString & "'", Main.MainInstance.GLOBALconnRealmd)
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
                Else
                    Dim _
                        da As _
                            New MySqlDataAdapter(
                                "SELECT `login` FROM `accounts` WHERE lastlogin>='" & My.Settings.lastlogindate1 &
                                "' AND lastlogin<='" & My.Settings.lastlogindate2 & "'", Main.MainInstance.GLOBALconnRealmd)
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
                                "SELECT `login` FROM `accounts` WHERE gm='" & My.Settings.gmlevel.ToString & "'",
                                Main.MainInstance.GLOBALconnRealmd)
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


        Try
            guidlist.Clear()
        Catch ex As Exception

        End Try

        guidlist = New List(Of String)

        '  Dim conn As New MySqlConnection(Main.MainInstance.ServerString)

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


        Dim da As New MySqlDataAdapter("SELECT guid FROM characters WHERE name='" & charname & "'", Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
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
    Public Function accountexist(ByVal accountname As String, ByVal connectionstring As String) As Boolean
        Dim quickconn As New MySqlConnection
        quickconn.ConnectionString = connectionstring
        Try
            quickconn.Open()
        Catch ex As Exception

        End Try
        Dim da As New MySqlDataAdapter("SELECT `acct` FROM accounts WHERE `login`='" & accountname & "'", quickconn)
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
        Dim da As New MySqlDataAdapter("SELECT guid FROM characters WHERE name='" & charname & "'", quickconn)
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


    Public Sub getallcharsfromaccount(ByVal accountname As String)
        Try
            guidlist.Clear()
        Catch ex As Exception

        End Try
        Dim accid As String =
                runfunction.runcommandRealmd("SELECT `acct` FROM accounts WHERE `login`='" & accountname & "'", "acct")
        guidlist = New List(Of String)


        Dim da As New MySqlDataAdapter("SELECT guid FROM characters WHERE `acct`='" & accid & "'", Main.MainInstance.GLOBALconn)
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
        For Each guid As String In guidlist
            GetCharFromDatabase(guid)
        Next
    End Sub

    Public Sub GetCharFromDatabase(ByVal charguid As String)
        '****************************************************************************************
        '****************************************************************************************
        'Get Character Guid


        '****************************************************************************************
        '****************************************************************************************
        'Get Main Character atributes

        'Character Race
        Main.MainInstance.setallempty()
        Main.MainInstance.anzahldurchlaufe += 1
        Main.MainInstance.char_guid = CInt(Val(charguid))
        characterguid = CInt(Val(charguid))

        Process_Status.processreport.appendText(
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
        Process_Status.processreport.appendText(
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
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Gender from Database..." & vbNewLine)
        Application.DoEvents()
        Try
            Main.MainInstance.char_gender =
                CInt(Val(runfunction.runcommand("SELECT gender FROM characters WHERE guid='" & charguid & "'", "gender")))
        Catch ex As Exception

        End Try

        'Character level
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Level from Database..." & vbNewLine)
        Application.DoEvents()
        Try
            Main.MainInstance.char_level =
                CInt(Val(runfunction.runcommand("SELECT level FROM characters WHERE guid='" & charguid & "'", "level")))

        Catch ex As Exception

        End Try
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "/ Loading Character Table..." & vbNewLine)
        Application.DoEvents()

        Main.MainInstance.char_name = runfunction.runcommand("SELECT name FROM characters WHERE guid='" & charguid & "'", "name")

        Main.MainInstance.accountid = CInt(Val(runfunction.runcommand("SELECT acct FROM characters WHERE guid='" & charguid & "'",
                                                         "acct")))
        Main.MainInstance.char_xp = CInt(Val(runfunction.runcommand("SELECT xp FROM characters WHERE guid='" & charguid & "'", "xp")))
        Main.MainInstance.player_money = CInt(Val(runfunction.runcommand("SELECT gold FROM characters WHERE guid='" & charguid & "'",
                                                            "gold")))
        Main.MainInstance.playerBytes = CInt(Val(runfunction.runcommand("SELECT bytes FROM characters WHERE guid='" & charguid & "'",
                                                           "bytes")))
        Main.MainInstance.playerBytes2 =
            CInt(Val(runfunction.runcommand("SELECT bytes2 FROM characters WHERE guid='" & charguid & "'", "bytes2")))
        Main.MainInstance.playerFlags =
            CInt(Val(runfunction.runcommand("SELECT player_flags FROM characters WHERE guid='" & charguid & "'",
                                            "player_flags")))
        Main.MainInstance.position_x = CDbl((runfunction.runcommand("SELECT positionX FROM characters WHERE guid='" & charguid & "'",
                                                 "positionX")))
        Main.MainInstance.position_y = CDbl((runfunction.runcommand("SELECT positionY FROM characters WHERE guid='" & charguid & "'",
                                                 "positionY")))
        Main.MainInstance.position_z = CDbl((runfunction.runcommand("SELECT positionZ FROM characters WHERE guid='" & charguid & "'",
                                                 "positionZ")))
        Main.MainInstance.map = CInt(Val(runfunction.runcommand("SELECT mapId FROM characters WHERE guid='" & charguid & "'", "mapId")))
        Main.MainInstance.instance_id =
            CInt(Val(runfunction.runcommand("SELECT instance_id FROM characters WHERE guid='" & charguid & "'",
                                            "instance_id")))
        Main.MainInstance.orientation = CDbl((runfunction.runcommand("SELECT orientation FROM characters WHERE guid='" & charguid & "'",
                                                  "orientation")))
        Main.MainInstance.taximask = runfunction.runcommand("SELECT taximask FROM characters WHERE guid='" & charguid & "'",
                                               "taximask")
        Main.MainInstance.cinematic =
            CInt(Val(runfunction.runcommand("SELECT first_login FROM characters WHERE guid='" & charguid & "'",
                                            "first_login")))
        Dim leveltime As String =
                runfunction.runcommand("SELECT playedtime FROM characters WHERE guid='" & charguid & "'", "playedtime")
        Dim parts() As String = leveltime.Split(" "c)
        Try
            Main.MainInstance.totaltime = CInt(Val(parts(0)))
        Catch ex As Exception

        End Try

        Main.MainInstance.stable_slots =
            runfunction.runcommand("SELECT totalstableslots FROM characters WHERE guid='" & charguid & "'",
                                   "totalstableslots")
        Main.MainInstance.zone = CInt(Val(runfunction.runcommand("SELECT zoneId FROM characters WHERE guid='" & charguid & "'",
                                                    "zoneId")))
        Main.MainInstance.arenaPoints =
            CInt(Val(runfunction.runcommand("SELECT arenaPoints FROM characters WHERE guid='" & charguid & "'",
                                            "arenaPoints")))
        Main.MainInstance.totalHonorPoints =
            CInt(Val(runfunction.runcommand("SELECT honorPoints FROM characters WHERE guid='" & charguid & "'",
                                            "honorPoints")))
        Main.MainInstance.totalKills =
            CInt(Val(runfunction.runcommand("SELECT killsLifeTime FROM characters WHERE guid='" & charguid & "'",
                                            "killsLifeTime")))
        Main.MainInstance.chosenTitle =
            runfunction.runcommand("SELECT selected_pvp_title  FROM characters WHERE guid='" & charguid & "'",
                                   "selected_pvp_title ")
        Main.MainInstance.watchedFaction =
            runfunction.runcommand("SELECT watched_faction_index  FROM characters WHERE guid='" & charguid & "'",
                                   "watched_faction_index ")
        Main.MainInstance.health = CInt(Val(runfunction.runcommand("SELECT current_hp FROM characters WHERE guid='" & charguid & "'",
                                                      "current_hp")))
        Main.MainInstance.speccount = CInt(Val(runfunction.runcommand("SELECT NumSpecs FROM characters WHERE guid='" & charguid & "'",
                                                         "NumSpecs")))
        Main.MainInstance.activespec =
            CInt(Val(runfunction.runcommand("SELECT Currentspec FROM characters WHERE guid='" & charguid & "'",
                                            "Currentspec")))
        Main.MainInstance.exploredZones =
            runfunction.runcommand("SELECT exploration_data FROM characters WHERE guid='" & charguid & "'",
                                   "exploration_data")
        Main.MainInstance.knownTitles =
            runfunction.runcommand("SELECT available_pvp_titles FROM characters WHERE guid='" & charguid & "'",
                                   "available_pvp_titles")
        Main.MainInstance.arcemu_talentpoints =
            runfunction.runcommand("SELECT talentpoints FROM characters WHERE guid='" & charguid & "'", "talentpoints")
        Main.MainInstance.finished_quests =
            runfunction.runcommand("SELECT finished_quests FROM characters WHERE guid='" & charguid & "'",
                                   "finished_quests")
        Main.MainInstance.accountname =
            runfunction.runcommandRealmd("SELECT login FROM accounts WHERE acct='" & Main.MainInstance.accountid.ToString & "'",
                                         "login")
        Main.MainInstance.arcemu_pass =
            runfunction.runcommandRealmd("SELECT password FROM accounts WHERE acct='" & Main.MainInstance.accountid.ToString & "'",
                                         "password")

        Main.MainInstance.sha_pass_hash = runfunction.runcommandRealmd("SELECT encrypted_password FROM accounts WHERE `acct`='" & Main.MainInstance.accountid.ToString & "'", "encrypted_password")
        If Main.MainInstance.sha_pass_hash = "" Then
            Main.MainInstance.sha_pass_hash = runfunction.SHA1StringHash((Main.MainInstance.accountname & ":" & Main.MainInstance.arcemu_pass).ToUpper)
        End If
        Main.MainInstance.email =
            runfunction.runcommandRealmd("SELECT email FROM accounts WHERE acct='" & Main.MainInstance.accountid.ToString & "'",
                                         "email")
        Dim tmpflags As String = runfunction.runcommandRealmd("SELECT flags FROM accounts WHERE acct='" & Main.MainInstance.accountid.ToString & "'", "flags")
        Select Case tmpflags
            Case "0"
                Main.MainInstance.expansion = 0
            Case "8"
                Main.MainInstance.expansion = 1
            Case "16"
                Main.MainInstance.expansion = 2
            Case "24"
                Main.MainInstance.expansion = 2
            Case "32"
                Main.MainInstance.expansion = 3
            Case Else : End Select
        Main.MainInstance.locale =
            CInt(
                Val(
                    runfunction.runcommandRealmd(
                        "SELECT forceLanguage FROM accounts WHERE acct='" & Main.MainInstance.accountid.ToString & "'",
                        "forceLanguage")))
        Main.MainInstance.arcemu_gmlevel = runfunction.runcommandRealmd("SELECT gm FROM accounts WHERE acct='" & Main.MainInstance.accountid.ToString & "'", "gm")
        Select Case Main.MainInstance.arcemu_gmlevel
            Case "AZ"
                Main.MainInstance.account_access_gmlevel = 4
            Case "A"
                Main.MainInstance.account_access_gmlevel = 3
            Case "0"
                Main.MainInstance.account_access_gmlevel = 0
            Case Else
                Main.MainInstance.account_access_gmlevel = 0
        End Select

        Main.MainInstance.account_access_RealmID = 1
        Main.MainInstance.custom_faction =
            runfunction.runcommand("SELECT custom_faction FROM characters WHERE guid='" & charguid & "'",
                                   "custom_faction ")
        Main.MainInstance.level.Text = Main.MainInstance.char_name & ", " & Main.MainInstance.char_level & ", " & Main.MainInstance.char_race & ", " & Main.MainInstance.char_class
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Homebind from Database..." & vbNewLine)
        Main.MainInstance.character_homebind =
            ("<map>" &
             runfunction.runcommand("SELECT bindmapId FROM characters WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                    "bindmapId") & "</map><zone>" &
             runfunction.runcommand("SELECT bindzoneId FROM characters WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                    "bindzoneId") & "</zone><position_x>" &
             runfunction.runcommand("SELECT bindpositionX FROM characters WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                    "bindpositionX") & "</position_x><position_y>" &
             runfunction.runcommand("SELECT bindpositionY FROM characters WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                    "bindpositionY") & "</position_y><position_z>" &
             runfunction.runcommand("SELECT bindpositionZ FROM characters WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                    "bindpositionZ") & "</position_z>")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Character Spells from Database..." & vbNewLine)
        Application.DoEvents()
        Main.MainInstance.level.Text = Main.MainInstance.char_name & ", " & Main.MainInstance.char_level & ", "
        Select Case Main.MainInstance.char_race
            Case 1
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Mensch" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Human"
            Case 2
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Orc" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Orc"
            Case 3
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Zwerg" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Dwarf"
            Case 4
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Nachtelf" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Night Elf"
            Case 5
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Untot" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Undead"
            Case 6
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Tauren" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Tauren"
            Case 7
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Gnom" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Gnome"
            Case 8
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Troll" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Troll"
            Case 9
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Goblin" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Goblin"
            Case 10
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Blutelf" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Blood Elf"
            Case 11
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Draenei" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Draenei"
            Case 22
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Worgen" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Worgen"
            Case Else

        End Select
        Main.MainInstance.level.Text = Main.MainInstance.level.Text & ", "
        Select Case Main.MainInstance.char_class
            Case 1
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Krieger" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Warrior"
            Case 2
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Paladin" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Paladin"
            Case 3
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Jäger" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Hunter"
            Case 4
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Schurke" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Rogue"
            Case 5
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Priester" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Priest"
            Case 6
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Todesritter" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Death Knight"
            Case 7
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Schamane" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Shaman"
            Case 8
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Magier" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Mage"
            Case 9
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Hexenmeister" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Warlock"
            Case 11
                If My.Settings.language = "de" Then Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Druide" Else Main.MainInstance.level.Text = Main.MainInstance.level.Text & "Druid"
            Case Else

        End Select
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

    Public Sub getspells_old()

        Dim _
            da As _
                New MySqlDataAdapter("SELECT SpellID FROM playerspells WHERE GUID='" & Main.MainInstance.char_guid.ToString & "'",
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

                    Main.MainInstance.character_spells.Add("<spell>" & spell & "</spell><active>1</active><disabled>0</disabled>")

                    count += 1
                Loop Until count = lastcount

            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub getspells()
        Dim _
            da As _
                New MySqlDataAdapter("SELECT spells FROM characters WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                     Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
        Catch ex As Exception
            Exit Sub
        End Try

        Try
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim excounter As Integer = UBound(readedcode.Split(CChar(",")))
                    Dim partscounter As Integer = 0
                    Do
                        Dim parts() As String = readedcode.Split(","c)
                        Dim spell As String = parts(partscounter).ToString
                        partscounter += 1
                        Main.MainInstance.character_spells.Add("<spell>" & spell & "</spell><active>1</active><disabled>0</disabled>")
                    Loop Until partscounter = excounter - 1


                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub gettalents()
        sdatatable.Clear()
        sdatatable.Dispose()
        sdatatable = gettable()

        Dim talentstring As String =
                runfunction.runcommand("SELECT talents1 FROM characters WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                       "talents1")
        If talentstring.Contains(",") Then

            Dim excounter As Integer = UBound(talentstring.Split(CChar(",")))
            Dim startcounter As Integer = 0
            Do
                Dim parts() As String = talentstring.Split(","c)
                Dim ctalentid As String = parts(startcounter)
                startcounter += 1
                Dim rurrentrank As String = (CInt(parts(startcounter)) + 1).ToString()
                startcounter += 1
                Main.MainInstance.character_talent_list.Add("<spell>" & checkfield2(ctalentid, rurrentrank) & "</spell><spec>0</spec>")
            Loop Until startcounter = excounter
        End If
        Dim talentstring2 As String =
                runfunction.runcommand("SELECT talents2 FROM characters WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                       "talents2")
        If talentstring2.Contains(",") Then
            Dim excounter As Integer = UBound(talentstring2.Split(CChar(",")))
            Dim startcounter As Integer = 0
            Do
                Dim parts() As String = talentstring2.Split(","c)
                Dim ctalentid As String = parts(startcounter)
                startcounter += 1
                Dim rurrentrank As String = (CInt(parts(startcounter)) + 1).ToString()
                startcounter += 1
                Main.MainInstance.character_talent_list.Add("<spell>" & checkfield2(ctalentid, rurrentrank) & "</spell><spec>1</spec>")
            Loop Until startcounter = excounter
        End If
    End Sub

    Private Function checkfield2(ByVal lID As String, ByVal rank As String) As String
        If rank = "0" Then
            Return lID & "clear"
        ElseIf Not executex2("TalentId", lID, CInt(Val(rank))) = "-" Then

            Return executex2("TalentId", lID, CInt(Val(rank)))
        Else
            Return "0"
        End If
    End Function

    Private Function executex2(ByVal field As String, ByVal sID As String, ByVal rank As Integer) As String
        Try
            Dim foundRows() As DataRow

            foundRows = sdatatable.Select(field & " = '" & sID & "'")
            If foundRows.Length = 0 Then
                Return "-"

            Else
                Dim i As Integer
                Dim tmpreturn As String = "-"
                For i = 0 To foundRows.GetUpperBound(0)
                    tmpreturn = (foundRows(i)(rank)).ToString

                Next i
                Return tmpreturn
            End If

        Catch ex As Exception
            Return "-"
        End Try
    End Function

    Public Sub getqueststatus()

        Dim _
            da As _
                New MySqlDataAdapter("SELECT quest_id FROM questlog WHERE player_guid='" & Main.MainInstance.char_guid.ToString & "'",
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
                                "SELECT completed FROM questlog WHERE quest_id='" & quest & "' AND player_guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "completed")
                    Dim explored As String =
                            runfunction.runcommand(
                                "SELECT explored_area1 FROM questlog WHERE quest_id='" & quest & "' AND player_guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "explored_area1")
                    Dim timer As String =
                            runfunction.runcommand(
                                "SELECT expirytimy FROM questlog WHERE quest_id='" & quest & "' AND player_guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "expirytimy")
                    Dim slot As String =
                            runfunction.runcommand(
                                "SELECT slot FROM questlog WHERE quest_id='" & quest & "' AND player_guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "slot")
                    Main.MainInstance.character_queststatus.Add(
                        "<quest>" & quest & "</quest><status>" & status & "</status><explored>" & explored &
                        "</explored><timer>" & timer & "</timer><slot>" & slot & "</slot>")

                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getskills_old()

        Dim _
            da As _
                New MySqlDataAdapter("SELECT SkillID FROM playerskills WHERE GUID='" & Main.MainInstance.char_guid.ToString & "'",
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
                                "SELECT CurrentValue FROM playerskills WHERE SkillID='" & skill & "' AND GUID='" &
                                Main.MainInstance.char_guid.ToString & "'", "CurrentValue")
                    Dim max As String =
                            runfunction.runcommand(
                                "SELECT MaximumValue FROM playerskills WHERE SkillID='" & skill & "' AND GUID='" &
                                Main.MainInstance.char_guid.ToString & "'", "MaximumValue")

                    Main.MainInstance.character_skills_list.Add(
                        "<skill>" & skill & "</skill><value>" & value & "</value><max>" & max & "</max>")

                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getskills()
        Dim _
            da As _
                New MySqlDataAdapter("SELECT skills FROM characters WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                     Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable

        da.Fill(dt)
        Try
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim excounter As Integer = UBound(readedcode.Split(CChar(";")))
                    Dim loopcounter As Integer = 0
                    Dim finalcounter As Integer = CInt(excounter / 3)
                    Dim partscounter As Integer = 0
                    Do
                        Dim parts() As String = readedcode.Split(","c)
                        Dim skill As String = parts(partscounter).ToString
                        partscounter += 1
                        Dim value As String = parts(partscounter).ToString
                        partscounter += 1
                        Dim max As String = parts(partscounter).ToString
                        partscounter += 1
                        Main.MainInstance.character_skills_list.Add(
                            "<skill>" & skill & "</skill><value>" & value & "</value><max>" & max & "</max>")
                        loopcounter += 1
                    Loop Until loopcounter = finalcounter


                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getREPlists_old()

        Dim _
            da As _
                New MySqlDataAdapter(
                    "SELECT faction FROM playerreputations WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                    Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable

        da.Fill(dt)
        Try
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
                                "SELECT flag FROM playerreputations WHERE faction='" & faction & "' AND guid='" &
                                Main.MainInstance.char_guid.ToString & "'", "flag FROM")

                    Main.MainInstance.character_reputatuion_list.Add(
                        "<faction>" & faction & "</faction><standing>" & standing & "</standing><flags>" & flags &
                        "</flags>")

                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getREPlists()
        Dim _
            da As _
                New MySqlDataAdapter("SELECT reputation FROM characters WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                     Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable

        da.Fill(dt)
        Try
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim excounter As Integer = UBound(readedcode.Split(CChar(",")))
                    Dim loopcounter As Integer = 0
                    Dim finalcounter As Integer = CInt(excounter / 4)
                    Dim partscounter As Integer = 0
                    Do
                        Dim parts() As String = readedcode.Split(","c)
                        Dim faction As String = parts(partscounter).ToString
                        partscounter += 1
                        Dim flags As String = parts(partscounter).ToString
                        partscounter += 1
                        Dim standing As String = parts(partscounter).ToString
                        partscounter += 2
                        Main.MainInstance.character_reputatuion_list.Add(
                            "<faction>" & faction & "</faction><standing>" & standing & "</standing><flags>" & flags &
                            "</flags>")
                        loopcounter += 1
                    Loop Until loopcounter = finalcounter


                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getactionlist()
        'needs to be tested!
        Main.MainInstance.arcemu_action1 =
            runfunction.runcommand("SELECT actions1 FROM characters WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                   "actions1")
        Main.MainInstance.arcemu_action2 =
            runfunction.runcommand("SELECT actions2 FROM characters WHERE guid='" & Main.MainInstance.char_guid.ToString & "'",
                                   "actions2")

        Dim readedcode As String = Main.MainInstance.arcemu_action1
        Dim excounter As Integer = UBound(readedcode.Split(CChar(",")))
        Dim loopcounter As Integer = 0
        Dim finalcounter As Integer = CInt(excounter / 3)
        Dim partscounter As Integer = 0
        Do
            Dim parts() As String = readedcode.Split(","c)
            Dim Action As String = parts(partscounter).ToString
            Dim gbutton As String = (loopcounter + 1).ToString
            partscounter += 3
            Main.MainInstance.character_action_list.Add(
                "<action>" & Action & "</action><spec>0</spec><button>" & gbutton & "</button><type>0</type>")
            loopcounter += 1
        Loop Until loopcounter = finalcounter
        Dim readedcode2 As String = Main.MainInstance.arcemu_action2
        Dim excounter2 As Integer = UBound(readedcode2.Split(CChar(",")))
        Dim loopcounter2 As Integer = 0
        Dim finalcounter2 As Integer = CInt(excounter2 / 3)
        Dim partscounter2 As Integer = 0
        Do
            Dim parts() As String = readedcode2.Split(","c)
            Dim Action As String = parts(partscounter2).ToString
            Dim gbutton As String = (loopcounter2 + 1).ToString
            partscounter2 += 3
            Main.MainInstance.character_action_list.Add(
                "<action>" & Action & "</action><spec>1</spec><button>" & gbutton & "</button><type>0</type>")
            loopcounter2 += 1
        Loop Until loopcounter2 = finalcounter2
    End Sub

    Public Sub getavlists()

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

        Dim tmpext As Integer
        Dim slotlist As String = ""
        Dim _
            da As _
                New MySqlDataAdapter("SELECT slot FROM playeritems WHERE ownerguid='" & Main.MainInstance.char_guid.ToString & "'",
                                     Main.MainInstance.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    If Not slotlist.Contains("#" & readedcode & "#") Then
                        slotlist = slotlist & "#" & readedcode & "#"
                        tmpext = CInt(Val(readedcode))
                        Dim numresults As Integer =
                                runfunction.returncountresults(
                                    "SELECT containerslot FROM playeritems WHERE ownerguid='" & Main.MainInstance.char_guid.ToString &
                                    "' AND slot='" & tmpext.ToString & "'", "containerslot")
                        If numresults = 1 Then
                            Dim containerslot As String =
                                    runfunction.runcommand(
                                        "SELECT containerslot FROM playeritems WHERE ownerguid='" &
                                        Main.MainInstance.char_guid.ToString & "' AND slot='" & tmpext.ToString & "'",
                                        "containerslot")
                            Dim bagguid As String = "-1"
                            If containerslot = "-1" Then

                            Else
                                bagguid =
                                    runfunction.runcommand(
                                        "SELECT guid FROM playeritems WHERE ownerguid='" & Main.MainInstance.char_guid.ToString &
                                        "' AND slot='" & containerslot & "' AND containerslot='-1'", "guid")

                            End If

                            If bagguid = "-1" Then
                                If tmpext > 18 Then
                                    Dim bag As String = "0"
                                    Dim item As String = "0"
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String = "1"

                                    bag = bagguid


                                    item =
                                        runfunction.runcommand(
                                            "SELECT guid FROM playeritems WHERE ownerguid='" & Main.MainInstance.char_guid.ToString &
                                            "' AND slot='" & tmpext.ToString & "' AND containerslot='-1'", "guid")
                                    entryid =
                                        runfunction.runcommand(
                                            "SELECT entry FROM playeritems WHERE guid = '" & item & "'", "entry")
                                    enchantments =
                                        runfunction.runcommand(
                                            "SELECT enchantments FROM playeritems WHERE guid='" & item & "'",
                                            "enchantments")
                                    itemcount =
                                        runfunction.runcommand("SELECT count FROM playeritems WHERE guid='" & item & "'",
                                                               "count")
                                    Main.MainInstance.character_inventoryzero_list.Add(
                                        "<slot>" & tmpext.ToString & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid &
                                        "</bagguid><item>" & entryid & "</item><enchant>" & enchantments &
                                        "</enchant><count>" & itemcount & "</count><container>-1</container>" &
                                        "<oldguid>" & item & "</oldguid>")
                                End If
                            Else
                                Dim bag As String = "0"
                                Dim item As String = "0"
                                Dim entryid As String
                                Dim enchantments As String
                                Dim itemcount As String = "1"
                                bag =
                                    runfunction.runcommand(
                                        "SELECT entry FROM playeritems WHERE guid = '" & bagguid & "'", "entry")


                                item =
                                    runfunction.runcommand(
                                        "SELECT guid FROM playeritems WHERE ownerguid='" & Main.MainInstance.char_guid.ToString &
                                        "' AND slot='" & tmpext.ToString & "'", "guid")
                                entryid =
                                    runfunction.runcommand("SELECT entry FROM playeritems WHERE guid = '" & item & "'",
                                                           "entry")
                                enchantments =
                                    runfunction.runcommand(
                                        "SELECT enchantments FROM playeritems WHERE guid='" & item & "'", "enchantments")
                                itemcount =
                                    runfunction.runcommand("SELECT count FROM playeritems WHERE guid='" & item & "'",
                                                           "count")
                                Main.MainInstance.character_inventory_list.Add(
                                    "<slot>" & tmpext.ToString & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid &
                                    "</bagguid><item>" & entryid & "</item><enchant>" & enchantments &
                                     "</enchant><count>" & itemcount & "</count><container>-1</container>" &
                                        "<oldguid>" & item & "</oldguid>")
                            End If
                        Else
                            Dim containerslot As String =
                                    runfunction.returnresultwithrow(
                                        "SELECT containerslot FROM playeritems WHERE ownerguid='" &
                                        Main.MainInstance.char_guid.ToString & "' AND slot='" & tmpext.ToString & "'",
                                        "containerslot", 0)
                            Dim bagguid As String = "-1"
                            If containerslot = "-1" Then

                            Else
                                bagguid =
                                    runfunction.runcommand(
                                        "SELECT guid FROM playeritems WHERE ownerguid='" & Main.MainInstance.char_guid.ToString &
                                        "' AND slot='" & containerslot & "' AND containerslot='-1'", "guid")

                            End If

                            If bagguid = "-1" Then
                                If tmpext > 18 Then
                                    Dim bag As String = "0"
                                    Dim item As String = "0"
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String = "1"

                                    bag = bagguid


                                    item =
                                        runfunction.runcommand(
                                            "SELECT guid FROM playeritems WHERE ownerguid='" & Main.MainInstance.char_guid.ToString &
                                            "' AND slot='" & tmpext.ToString & "' AND containerslot='-1'", "guid")
                                    entryid =
                                        runfunction.runcommand(
                                            "SELECT entry FROM playeritems WHERE guid = '" & item & "'", "entry")
                                    enchantments =
                                        runfunction.runcommand(
                                            "SELECT enchantments FROM playeritems WHERE guid='" & item & "'",
                                            "enchantments")
                                    itemcount =
                                        runfunction.runcommand("SELECT count FROM playeritems WHERE guid='" & item & "'",
                                                               "count")
                                    Main.MainInstance.character_inventoryzero_list.Add(
                                        "<slot>" & tmpext.ToString & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid &
                                        "</bagguid><item>" & entryid & "</item><enchant>" & enchantments &
                                        "</enchant><count>" & itemcount & "</count><container>-1</container>")
                                End If
                            Else
                                Dim bag As String = "0"
                                Dim item As String = "0"
                                Dim entryid As String
                                Dim enchantments As String
                                Dim itemcount As String = "1"
                                bag =
                                    runfunction.runcommand(
                                        "SELECT entry FROM playeritems WHERE guid = '" & bagguid & "'", "entry")


                                item =
                                    runfunction.returnresultwithrow(
                                        "SELECT guid FROM playeritems WHERE ownerguid='" & Main.MainInstance.char_guid.ToString &
                                        "' AND slot='" & tmpext.ToString & "'", "guid", 1)
                                entryid =
                                    runfunction.runcommand("SELECT entry FROM playeritems WHERE guid = '" & item & "'",
                                                           "entry")
                                enchantments =
                                    runfunction.runcommand(
                                        "SELECT enchantments FROM playeritems WHERE guid='" & item & "'", "enchantments")
                                itemcount =
                                    runfunction.runcommand("SELECT count FROM playeritems WHERE guid='" & item & "'",
                                                           "count")
                                Main.MainInstance.character_inventory_list.Add(
                                    "<slot>" & tmpext.ToString & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid &
                                    "</bagguid><item>" & entryid & "</item><enchant>" & enchantments &
                                    "</enchant><count>" & itemcount & "</count><container>-1</container>" &
                                        "<oldguid>" & item & "</oldguid>")
                            End If
                            Dim containerslot2 As String =
                                    runfunction.returnresultwithrow(
                                        "SELECT containerslot FROM playeritems WHERE ownerguid='" &
                                        Main.MainInstance.char_guid.ToString & "' AND slot='" & tmpext.ToString & "'",
                                        "containerslot", 1)
                            Dim bagguid2 As String = "-1"
                            If containerslot2 = "-1" Then

                            Else
                                bagguid2 =
                                    runfunction.runcommand(
                                        "SELECT guid FROM playeritems WHERE ownerguid='" & Main.MainInstance.char_guid.ToString &
                                        "' AND slot='" & containerslot2 & "' AND containerslot='-1'", "guid")

                            End If

                            If bagguid2 = "-1" Then
                                If tmpext > 18 Then
                                    Dim bag As String = "0"
                                    Dim item As String = "0"
                                    Dim entryid As String
                                    Dim enchantments As String
                                    Dim itemcount As String = "1"

                                    bag = bagguid2


                                    item =
                                        runfunction.returnresultwithrow(
                                            "SELECT guid FROM playeritems WHERE ownerguid='" & Main.MainInstance.char_guid.ToString &
                                            "' AND slot='" & tmpext.ToString & "' AND containerslot='-1'", "guid", 1)
                                    entryid =
                                        runfunction.runcommand(
                                            "SELECT entry FROM playeritems WHERE guid = '" & item & "'", "entry")
                                    enchantments =
                                        runfunction.runcommand(
                                            "SELECT enchantments FROM playeritems WHERE guid='" & item & "'",
                                            "enchantments")
                                    itemcount =
                                        runfunction.runcommand("SELECT count FROM playeritems WHERE guid='" & item & "'",
                                                               "count")
                                    Main.MainInstance.character_inventoryzero_list.Add(
                                        "<slot>" & tmpext.ToString & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid &
                                        "</bagguid><item>" & entryid & "</item><enchant>" & enchantments &
                                        "</enchant><count>" & itemcount & "</count><container>-1</container>")
                                End If
                            Else
                                Dim bag As String = "0"
                                Dim item As String = "0"
                                Dim entryid As String
                                Dim enchantments As String
                                Dim itemcount As String = "1"
                                bag =
                                    runfunction.runcommand(
                                        "SELECT entry FROM playeritems WHERE guid = '" & bagguid2 & "'", "entry")


                                item =
                                    runfunction.returnresultwithrow(
                                        "SELECT guid FROM playeritems WHERE ownerguid='" & Main.MainInstance.char_guid.ToString &
                                        "' AND slot='" & tmpext.ToString & "'", "guid", 1)
                                entryid =
                                    runfunction.runcommand("SELECT entry FROM playeritems WHERE guid = '" & item & "'",
                                                           "entry")
                                enchantments =
                                    runfunction.runcommand(
                                        "SELECT enchantments FROM playeritems WHERE guid='" & item & "'", "enchantments")
                                itemcount =
                                    runfunction.runcommand("SELECT count FROM playeritems WHERE guid='" & item & "'",
                                                           "count")
                                Main.MainInstance.character_inventory_list.Add(
                                    "<slot>" & tmpext.ToString & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid2 &
                                    "</bagguid><item>" & entryid & "</item><enchant>" & enchantments &
                                    "</enchant><count>" & itemcount & "</count><container>" & containerslot2 &
                                    "</container>" &
                                        "<oldguid>" & item & "</oldguid>")
                            End If
                        End If


                        count += 1
                    Else
                        count += 1
                    End If
                Loop Until count = lastcount
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub getglyphs()


        Dim glyphid As Integer
        Dim glyphname As String = ""
        Dim glyphpic As Image = My.Resources.empty

        Dim glyphstring As String =
                runfunction.runcommand("SELECT glyphs1 from characters WHERE guid='" & characterguid.ToString & "'",
                                       "glyphs1")

        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(0)))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.erheb1.Text = glyphname
                Main.MainInstance.majorglyph1 = glyphid.ToString
                Glyphs.erheb1.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.erheb1pic)
            End If
        Catch ex As Exception
            Glyphs.erheb1pic.Image = My.Resources.empty
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(3)))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.erheb2.Text = glyphname
                Main.MainInstance.majorglyph2 = glyphid.ToString
                Glyphs.erheb2.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.erheb2pic)
            End If
        Catch ex As Exception
            Glyphs.erheb2pic.Image = My.Resources.empty
        End Try

        Try

            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(5)))
            Application.DoEvents()
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                Application.DoEvents()
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.erheb3.Text = glyphname
                Main.MainInstance.majorglyph3 = glyphid.ToString
                Glyphs.erheb3.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.erheb3pic)
            End If
        Catch ex As Exception

            Glyphs.erheb3pic.Image = My.Resources.empty
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(1)))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.gering1.Text = glyphname
                Main.MainInstance.minorglyph1 = glyphid.ToString
                Glyphs.gering1.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.gering1pic)
            End If
        Catch ex As Exception
            Glyphs.gering1pic.Image = My.Resources.empty
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(2)))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.gering2.Text = glyphname
                Main.MainInstance.minorglyph2 = glyphid.ToString
                Glyphs.gering2.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.gering2pic)
            End If
        Catch ex As Exception
            Glyphs.gering2pic.Image = My.Resources.empty
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(4)))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.gering3.Text = glyphname
                Main.MainInstance.minorglyph3 = glyphid.ToString
                Glyphs.gering3.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.gering3pic)
            End If
        Catch ex As Exception
            Glyphs.gering3pic.Image = My.Resources.empty
        End Try
        Glyphs.prim1pic.Image = My.Resources.empty

        Glyphs.prim2pic.Image = My.Resources.empty

        Glyphs.prim3pic.Image = My.Resources.empty
    End Sub

    Public Sub getsecglyphs()

        Dim glyphid As Integer
        Dim glyphname As String = ""
        Dim glyphpic As Image = My.Resources.empty
        Dim glyphstring As String =
                runfunction.runcommand("SELECT glyphs2 from characters WHERE guid='" & characterguid.ToString & "'",
                                       "glyphs2")
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(0)))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.secerheb1.Text = glyphname
                Main.MainInstance.secmajorglyph1 = glyphid.ToString
                Glyphs.secerheb1.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secerheb1pic)
            End If
        Catch ex As Exception
            Glyphs.secerheb1pic.Image = My.Resources.empty
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(3)))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.secerheb2.Text = glyphname
                Main.MainInstance.secmajorglyph2 = glyphid.ToString
                Glyphs.secerheb2.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secerheb2pic)
            End If
        Catch ex As Exception
            Glyphs.secerheb2pic.Image = My.Resources.empty
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(5)))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.secerheb3.Text = glyphname
                Main.MainInstance.secmajorglyph3 = glyphid.ToString
                Glyphs.secerheb3.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secerheb3pic)
            End If
        Catch ex As Exception
            Glyphs.secerheb3pic.Image = My.Resources.empty
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(1)))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.secgering1.Text = glyphname
                Main.MainInstance.secminorglyph1 = glyphid.ToString
                Glyphs.secgering1.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secgering1pic)
            End If
        Catch ex As Exception
            Glyphs.secgering1pic.Image = My.Resources.empty
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(2)))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.secgering2.Text = glyphname
                Main.MainInstance.secminorglyph2 = glyphid.ToString
                Glyphs.secgering2.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secgering2pic)
            End If
        Catch ex As Exception
            Glyphs.secgering2pic.Image = My.Resources.empty
        End Try
        Try
            Dim parts() As String = glyphstring.Split(","c)
            Dim prevglyphid As Integer = CInt(Val(parts(4)))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.MainInstance.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.secgering3.Text = glyphname
                Main.MainInstance.secminorglyph3 = glyphid.ToString
                Glyphs.secgering3.Visible = True
                If Main.MainInstance.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secgering3pic)
            End If
        Catch ex As Exception
            Glyphs.secgering3pic.Image = My.Resources.empty
        End Try

        Glyphs.secprim1pic.Image = My.Resources.empty

        Glyphs.secprim2pic.Image = My.Resources.empty

        Glyphs.secprim3pic.Image = My.Resources.empty
    End Sub

    Private Sub saveglyphs()
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
                                "SELECT guid FROM playeritems WHERE ownerguid = '" & characterguid & "' AND slot = '" &
                                xslot & "' AND containerslot='-1'", "guid")))
                realxentryid =
                    CInt(
                        Val(
                            runfunction.runcommand(
                                "SELECT entry FROM playeritems WHERE guid = '" & xentryid.ToString & "'", "playeritems")))
                If Main.MainInstance.anzahldurchlaufe = 1 Then itemname = runfunction.getnamefromid(realxentryid)
                Dim wartemal As String = ""
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
        '///// Maybe bonus at position 38 will cause trouble!
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.kopfvz.Text = splitstringvz(Main.MainInstance.kopfench, Main.MainInstance.kopfvzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.kopfsocket1.Text = splitstringgem(Main.MainInstance.kopfench, Main.MainInstance.kopfsocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.kopfsocket2.Text = splitstringgem(Main.MainInstance.kopfench, Main.MainInstance.kopfsocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.kopfsocket3.Text = splitstringgem(Main.MainInstance.kopfench, Main.MainInstance.kopfsocket3id, 35)
        Main.MainInstance.kopfvz.Visible = True


        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.halsvz.Text = splitstringvz(Main.MainInstance.halsench, Main.MainInstance.halsvzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.halssocket1.Text = splitstringgem(Main.MainInstance.halsench, Main.MainInstance.halssocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.halssocket2.Text = splitstringgem(Main.MainInstance.halsench, Main.MainInstance.halssocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.halssocket3.Text = splitstringgem(Main.MainInstance.halsench, Main.MainInstance.halssocket3id, 35)
        Main.MainInstance.halsvz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.schultervz.Text = splitstringvz(Main.MainInstance.schulterench, Main.MainInstance.schultervzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.schultersocket1.Text = splitstringgem(Main.MainInstance.schulterench, Main.MainInstance.schultersocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.schultersocket2.Text = splitstringgem(Main.MainInstance.schulterench, Main.MainInstance.schultersocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.schultersocket3.Text = splitstringgem(Main.MainInstance.schulterench, Main.MainInstance.schultersocket3id, 35)
        Main.MainInstance.schultervz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.rueckenvz.Text = splitstringvz(Main.MainInstance.rueckenench, Main.MainInstance.rueckenvzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.rueckensocket1.Text = splitstringgem(Main.MainInstance.rueckenench, Main.MainInstance.rueckensocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.rueckensocket2.Text = splitstringgem(Main.MainInstance.rueckenench, Main.MainInstance.rueckensocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.rueckensocket3.Text = splitstringgem(Main.MainInstance.rueckenench, Main.MainInstance.rueckensocket3id, 35)
        Main.MainInstance.rueckenvz.Visible = True

        Main.MainInstance.brustvz.Text = splitstringvz(Main.MainInstance.brustench, Main.MainInstance.brustvzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.brustsocket1.Text = splitstringgem(Main.MainInstance.brustench, Main.MainInstance.brustsocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.brustsocket2.Text = splitstringgem(Main.MainInstance.brustench, Main.MainInstance.brustsocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.brustsocket3.Text = splitstringgem(Main.MainInstance.brustench, Main.MainInstance.brustsocket3id, 35)
        Main.MainInstance.brustvz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.handgelenkevz.Text = splitstringvz(Main.MainInstance.handgelenkeench, Main.MainInstance.handgelenkevzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.Handgelenkesocket1.Text = splitstringgem(Main.MainInstance.handgelenkeench, Main.MainInstance.handgelenkesocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.handgelenkesocket2.Text = splitstringgem(Main.MainInstance.handgelenkeench, Main.MainInstance.handgelenkesocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.Handgelenkesocket3.Text = splitstringgem(Main.MainInstance.handgelenkeench, Main.MainInstance.handgelenkesocket3id, 35)
        Main.MainInstance.handgelenkevz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.hauptvz.Text = splitstringvz(Main.MainInstance.hauptench, Main.MainInstance.hauptvzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.Hauptsocket1.Text = splitstringgem(Main.MainInstance.hauptench, Main.MainInstance.hauptsocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.Hauptsocket2.Text = splitstringgem(Main.MainInstance.hauptench, Main.MainInstance.hauptsocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.hauptsocket3.Text = splitstringgem(Main.MainInstance.hauptench, Main.MainInstance.hauptsocket3id, 35)
        Main.MainInstance.hauptvz.Visible = True
        Main.MainInstance.hauptvzlabel2.Visible = True
        Main.MainInstance.hauptvzlabel2.Text = Main.MainInstance.hauptvz.Text

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.offvz.Text = splitstringvz(Main.MainInstance.offench, Main.MainInstance.offvzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.Offsocket1.Text = splitstringgem(Main.MainInstance.offench, Main.MainInstance.offsocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.Offsocket2.Text = splitstringgem(Main.MainInstance.offench, Main.MainInstance.offsocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.offsocket3.Text = splitstringgem(Main.MainInstance.offench, Main.MainInstance.offsocket3id, 35)
        Main.MainInstance.offvz.Visible = True
        Main.MainInstance.offvzlabel2.Visible = True
        Main.MainInstance.offvzlabel2.Text = Main.MainInstance.offvz.Text

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.distanzvz.Text = splitstringvz(Main.MainInstance.distanzench, Main.MainInstance.distanzvzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.Distanzsocket1.Text = splitstringgem(Main.MainInstance.distanzench, Main.MainInstance.distanzsocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.Distanzsocket2.Text = splitstringgem(Main.MainInstance.distanzench, Main.MainInstance.distanzsocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.distanzsocket3.Text = splitstringgem(Main.MainInstance.distanzench, Main.MainInstance.distanzsocket3id, 35)
        Main.MainInstance.distanzvz.Visible = True
        Main.MainInstance.distanzvzlabel2.Visible = True
        Main.MainInstance.distanzvzlabel2.Text = Main.MainInstance.distanzvz.Text

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.haendevz.Text = splitstringvz(Main.MainInstance.haendeench, Main.MainInstance.haendevzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.haendesocket1.Text = splitstringgem(Main.MainInstance.haendeench, Main.MainInstance.haendesocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.haendesocket2.Text = splitstringgem(Main.MainInstance.haendeench, Main.MainInstance.haendesocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.haendesocket3.Text = splitstringgem(Main.MainInstance.haendeench, Main.MainInstance.haendesocket3id, 35)
        Main.MainInstance.haendevz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.guertelvz.Text = splitstringvz(Main.MainInstance.guertelench, Main.MainInstance.guertelvzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.guertelsocket1.Text = splitstringgem(Main.MainInstance.guertelench, Main.MainInstance.guertelsocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.guertelsocket2.Text = splitstringgem(Main.MainInstance.guertelench, Main.MainInstance.guertelsocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.guertelsocket3.Text = splitstringgem(Main.MainInstance.guertelench, Main.MainInstance.guertelsocket3id, 35)
        If Main.MainInstance.anzahldurchlaufe = 1 Then
            If splitstringgem(Main.MainInstance.guertelench, Main.MainInstance.trashvalue, 38) = "" Then

            Else
                Main.MainInstance.guertelschnalle = CInt(splitstringgem(Main.MainInstance.guertelench, Main.MainInstance.trashvalue, 38))
            End If

        End If

        Main.MainInstance.guertelvz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.beinevz.Text = splitstringvz(Main.MainInstance.beineench, Main.MainInstance.beinevzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.beinesocket1.Text = splitstringgem(Main.MainInstance.beineench, Main.MainInstance.beinesocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.beinesocket2.Text = splitstringgem(Main.MainInstance.beineench, Main.MainInstance.beinesocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.beinesocket3.Text = splitstringgem(Main.MainInstance.beineench, Main.MainInstance.beinesocket3id, 35)
        Main.MainInstance.beinevz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.stiefelvz.Text = splitstringvz(Main.MainInstance.stiefelench, Main.MainInstance.stiefelvzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.stiefelsocket1.Text = splitstringgem(Main.MainInstance.stiefelench, Main.MainInstance.stiefelsocket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.stiefelsocket2.Text = splitstringgem(Main.MainInstance.stiefelench, Main.MainInstance.stiefelsocket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.stiefelsocket3.Text = splitstringgem(Main.MainInstance.stiefelench, Main.MainInstance.stiefelsocket3id, 35)
        Main.MainInstance.stiefelvz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.ring1vz.Text = splitstringvz(Main.MainInstance.ring1ench, Main.MainInstance.ring1vzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.Ring1socket1.Text = splitstringgem(Main.MainInstance.ring1ench, Main.MainInstance.ring1socket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.ring1socket2.Text = splitstringgem(Main.MainInstance.ring1ench, Main.MainInstance.ring1socket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.ring1socket3.Text = splitstringgem(Main.MainInstance.ring1ench, Main.MainInstance.ring1socket3id, 35)
        Main.MainInstance.ring1vz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.ring2vz.Text = splitstringvz(Main.MainInstance.ring2ench, Main.MainInstance.ring2vzid, 23)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.ring2socket1.Text = splitstringgem(Main.MainInstance.ring2ench, Main.MainInstance.ring2socket1id, 29)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.ring2socket2.Text = splitstringgem(Main.MainInstance.ring2ench, Main.MainInstance.ring2socket2id, 32)
        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.ring2socket3.Text = splitstringgem(Main.MainInstance.ring2ench, Main.MainInstance.ring2socket3id, 35)
        Main.MainInstance.ring2vz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.schmuck1vz.Text = splitstringvz(Main.MainInstance.schmuck1ench, Main.MainInstance.schmuck1vzid, 23)
        Main.MainInstance.schmuck1vz.Visible = True

        If Main.MainInstance.anzahldurchlaufe = 1 Then Main.MainInstance.schmuck2vz.Text = splitstringvz(Main.MainInstance.schmuck2ench, Main.MainInstance.schmuck2vzid, 23)
        Main.MainInstance.schmuck2vz.Visible = True
    End Sub

    Public Function splitstringvz(ByVal input As String, ByRef obvalue As Integer, ByVal position As Integer) As String
        Dim xpacressource As String
        Select Case Main.MainInstance.xpac
            Case 3
                xpacressource = My.Resources.VZ_ID_wotlk2
            Case 4
                xpacressource = My.Resources.VZ_ID_cata2
            Case Else
                xpacressource = My.Resources.VZ_ID_wotlk2
        End Select
        Try
            If input.Contains(";") Then
                Dim parts() As String = input.Split(";"c)
                If parts(0).Contains("0,0") Then
                    Dim parts2() As String = parts(0).Split(","c)
                    obvalue = CInt(parts2(0))
                    Return runfunction.geteffectnameofeffectid(obvalue)
                ElseIf parts(1).Contains("0,0") Then
                    Dim parts2() As String = parts(1).Split(","c)
                    obvalue = CInt(parts2(0))
                    Return runfunction.geteffectnameofeffectid(obvalue)
                ElseIf parts(2).Contains("0,0") Then
                    Dim parts2() As String = parts(2).Split(","c)
                    obvalue = CInt(parts2(0))
                    Return runfunction.geteffectnameofeffectid(obvalue)
                ElseIf parts(3).Contains("0,0") Then
                    Dim parts2() As String = parts(3).Split(","c)
                    obvalue = CInt(parts2(0))
                    Return runfunction.geteffectnameofeffectid(obvalue)
                ElseIf parts(4).Contains("0,0") Then
                    Dim parts2() As String = parts(4).Split(","c)
                    obvalue = CInt(parts2(0))
                    Return runfunction.geteffectnameofeffectid(obvalue)
                ElseIf parts(5).Contains("0,0") Then
                    Dim parts2() As String = parts(5).Split(","c)
                    obvalue = CInt(parts2(0))
                    Return runfunction.geteffectnameofeffectid(obvalue)
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
        Select Case Main.MainInstance.xpac
            Case 3
                xpacressource = My.Resources.GEM_ID_wotlk2
            Case 4
                xpacressource = My.Resources.GEM_ID_cata2
            Case Else
                xpacressource = My.Resources.GEM_ID_wotlk2
        End Select
        Try
            Dim parts() As String = input.Split(";"c)
            Dim xvalue As String = ""
            If position = 23 Then
                xvalue = "0,1"
            ElseIf position = 29 Then
                xvalue = "0,2"
            ElseIf position = 32 Then
                xvalue = "0,3"
            ElseIf position = 35 Then
                xvalue = "0,4"
            ElseIf position = 38 Then
                xvalue = "0,6"
            Else
                xvalue = "0,1"
            End If
            If parts(0).Contains(xvalue) Then
                Dim parts2() As String = parts(0).Split(","c)
                obvalue = CInt(parts2(0))
                If xvalue = "0,6" Then Return parts2(0)
                Return runfunction.geteffectnameofeffectid(obvalue)
            ElseIf parts(1).Contains(xvalue) Then
                Dim parts2() As String = parts(1).Split(","c)
                obvalue = CInt(parts2(0))
                If xvalue = "0,6" Then Return parts2(0)
                Return runfunction.geteffectnameofeffectid(obvalue)
            ElseIf parts(2).Contains(xvalue) Then
                Dim parts2() As String = parts(2).Split(","c)
                obvalue = CInt(parts2(0))
                If xvalue = "0,6" Then Return parts2(0)
                Return runfunction.geteffectnameofeffectid(obvalue)
            ElseIf parts(3).Contains(xvalue) Then
                Dim parts2() As String = parts(3).Split(","c)
                obvalue = CInt(parts2(0))
                If xvalue = "0,6" Then Return parts2(0)
                Return runfunction.geteffectnameofeffectid(obvalue)
            ElseIf parts(4).Contains(xvalue) Then
                Dim parts2() As String = parts(4).Split(","c)
                obvalue = CInt(parts2(0))
                If xvalue = "0,6" Then Return parts2(0)
                Return runfunction.geteffectnameofeffectid(obvalue)
            ElseIf parts(5).Contains(xvalue) Then
                Dim parts2() As String = parts(5).Split(","c)
                obvalue = CInt(parts2(0))
                If xvalue = "0,6" Then Return parts2(0)
                Return runfunction.geteffectnameofeffectid(obvalue)
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Public Function getcharguid(ByVal charname As String) As Integer
        Try
            Return _
                CInt(Val(runfunction.runcommand("SELECT guid FROM characters WHERE name = '" & charname & "'", "guid")))

        Catch ex As Exception
            Return -1
        End Try
    End Function


    Public Sub getitemstats(ByVal itemguid As Integer, ByRef slotvar As String)
        Try
            slotvar =
                runfunction.runcommand("SELECT enchantments FROM playeritems WHERE guid='" & itemguid.ToString & "'",
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
    '##############################################################################################################################################################################
    '##############################################################################################################################################################################
    '##############################################################################################################################################################################
    '##############################################################################################################################################################################
    '##############################################################################################################################################################################
    '##############################################################################################################################################################################
    '##############################################################################################################################################################################
    '##############################################################################################################################################################################


    Public Sub createnewaccounts(ByVal writestring As String)

        runfunction.normalsqlcommandRealmd(writestring)
    End Sub

    Public Sub create_new_account_if_not_exist(ByVal accname As String, ByVal command As String, ByVal accguid As String)


        Try

            '  ("SELECT * FROM characters WHERE name = '" & charname.Text & "'")
            Dim myAdapter As New MySqlDataAdapter

            Dim sqlquery = "SELECT * FROM characters WHERE username = '" & accname & "'"
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = Main.MainInstance.GLOBALconn
            myCommand.CommandText = sqlquery
            'start query
            myAdapter.SelectCommand = myCommand
            Dim myData As MySqlDataReader
            myData = myCommand.ExecuteReader()
            If CInt(myData.HasRows) = 0 Then
                myData.Close()
                runfunction.normalsqlcommandRealmd(command)

            Else
                myData.Close()

            End If

        Catch
            runfunction.normalsqlcommandRealmd(command)
        End Try
    End Sub

    Public Sub addchars(ByVal targetaccount As String, ByVal charactername As String,
                        ByVal namechangeeverytime As Boolean)

        Dim newcharguid As Integer =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", "guid"))) + 1
        guid = newcharguid.ToString
        Main.MainInstance.coreguid = newcharguid.ToString
        targetaccount = runfunction.runcommandRealmd("SELECT acct FROM accounts WHERE login='" & targetaccount & "'",
                                                     "acct")
        If namechangeeverytime = True Then
            Process_Status.processreport.AppendText(
                Now.TimeOfDay.ToString & "// Player will be asked to change character name! : reason-nce=true" &
                vbNewLine)
            runfunction.normalsqlcommand(
                "INSERT INTO characters ( `acct`, `guid`, `name`, `race`, `class`, `gender`, `level`, `xp`, `gold`, current_hp, `bytes`, `positionX`, positionY, positionZ, orientation, mapId, taximask, playedtime ) VALUES ( '" &
                targetaccount & "', '" & newcharguid.ToString & "', '" & charactername &
                "', '0', '0', '0', '1', '0', '0', '1000', '" & Main.MainInstance.playerBytes & "', '-14305.7', '514.08', '10', '4.30671', '0', '0 0 0 0 0 0 0 0 0 0 0 0 ', '98 98 5 ' )")

            runfunction.normalsqlcommand(
                "UPDATE characters SET forced_rename_pending='1' WHERE guid='" & newcharguid.ToString & "'")
        Else
            If charexist(charactername) = True Then
                Process_Status.processreport.AppendText(
                    Now.TimeOfDay.ToString & "// Player will be asked to change charactername!" & vbNewLine)
                runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `acct`, `guid`, `name`, `race`, `class`, `gender`, `level`, `xp`, `gold`, current_hp, `bytes`, `positionX`, positionY, positionZ, orientation, mapId, taximask, playedtime ) VALUES ( '" &
                    targetaccount & "', '" & newcharguid.ToString & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '1000', '" & Main.MainInstance.playerBytes & "', '-14305.7', '514.08', '10', '4.30671', '0', '0 0 0 0 0 0 0 0 0 0 0 0 ', '98 98 5 ' )")
                runfunction.normalsqlcommand(
                    "UPDATE characters SET forced_rename_pending ='1' WHERE guid='" & newcharguid.ToString & "'")
            Else
                runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `acct`, `guid`, `name`, `race`, `class`, `gender`, `level`, `xp`, `gold`, current_hp, `bytes`, `positionX`, positionY, positionZ, orientation, mapId, taximask, playedtime ) VALUES ( '" &
                    targetaccount & "', '" & newcharguid.ToString & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '1000', '" & Main.MainInstance.playerBytes & "', '-14305.7', '514.08', '10', '4.30671', '0', '0 0 0 0 0 0 0 0 0 0 0 0 ', '98 98 5 ' )")
                runfunction.normalsqlcommand(
                    "UPDATE characters SET forced_rename_pending='0' WHERE guid='" & newcharguid.ToString & "'")
            End If

        End If
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Creating Hearthstone for Character: " & Main.MainInstance.char_name & vbNewLine)

        Dim newguid As String =
                ((CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT guid FROM playeritems WHERE guid=(SELECT MAX(guid) FROM playeritems)", "guid")))) +
                 1).ToString

        runfunction.normalsqlcommand(
            "INSERT INTO playeritems ( ownerguid, guid, entry, flags, containerslot, slot ) VALUES ( '" & Main.MainInstance.coreguid &
            "', '" & newguid & "', '6948', '1', '-1', '23' )")
        addsinglespell(6603) 'auto attack
        If Main.MainInstance.char_race = 1 Then
            If Main.MainInstance.char_class = 1 Then
                addmultipleskills("26;1;1;43;1;5;54;1;5;55;1;5;95;1;5;162;1;5;413;1;1;414;1;1;415;1;1;433;1;1;754;1;1;")
                addmultiplespell(
                    "78,81,107,196,198,201,202,203,204,522,668,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20597,20598,20599,20864,21651,21652,22027,22810,58985,59752,")
            ElseIf Main.MainInstance.char_class = 2 Then
                addmultipleskills("54;1;5;95;1;5;160;1;5;162;1;5;413;1;1;414;1;1;415;1;1;433;1;1;594;1;1;754;1;1;")
                addmultiplespell(
                    "81,107,198,199,203,204,522,635,668,2382,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20597,20598,20599,20864,21084,21651,21652,22027,22810,27762,34082,58985,59752,")
            ElseIf Main.MainInstance.char_class = 4 Then
                addmultipleskills("38;1;1;95;1;5;162;1;5;173;1;5;176;1;5;253;1;1;414;1;1;415;1;1;754;1;1;")
                addmultiplespell(
                    "81,203,204,522,668,674,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,16092,20597,20598,20599,20864,21184,21651,21652,22027,22810,58985,59752,")
            ElseIf Main.MainInstance.char_class = 5 Then
                addmultipleskills("54;1;5;56;1;1;95;1;5;136;1;5;162;1;5;228;1;5;415;1;1;754;1;1;")
                addmultiplespell(
                    "81,198,203,204,227,522,585,668,2050,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,20597,20598,20599,20864,21651,21652,22027,22810,58985,59752,")
            ElseIf Main.MainInstance.char_class = 6 Then
                addmultipleskills(
                    "43;270;275;44;270;275;55;270;275;95;270;275;129;270;300;162;270;275;172;270;275;229;270;275;293;1;1;413;1;1;414;1;1;415;1;1;754;1;1;762;150;150;770;1;1;771;1;1;772;1;1;")
                addmultiplespell(
                    "81,196,197,200,201,202,203,204,522,668,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20597,20598,20599,20864,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,58985,59752,59879,59921,61455,")
            ElseIf Main.MainInstance.char_class = 8 Then
                addmultipleskills(
                    "43;270;275;44;270;275;55;270;275;95;270;275;118;1;1;129;270;300;162;270;275;172;270;275;183;1;1;229;270;275;293;1;1;413;1;1;414;1;1;415;1;1;754;1;1;762;150;150;770;1;1;771;1;1;772;1;1;")
                addmultiplespell(
                    "81,196,197,200,201,202,203,204,522,668,669,670,671,672,674,750,813,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,17737,18629,18630,20597,20598,20599,20864,21651,21652,22027,22810,29932,33391,45462,45477,45902,47541,48266,49410,49576,52665,58985,59752,59879,59921,61455,")
            ElseIf Main.MainInstance.char_class = 9 Then
                addmultipleskills(
                    "43;270;275;44;270;275;55;270;275;95;270;275;118;1;1;129;270;300;162;270;275;172;270;275;183;1;1;229;270;275;293;1;1;413;1;1;414;1;1;415;1;1;754;1;1;762;150;150;770;1;1;771;1;1;772;1;1;")
                addmultiplespell(
                    "81,196,197,200,201,202,203,204,522,668,669,670,671,672,674,750,813,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,17737,18629,18630,20597,20598,20599,20864,21651,21652,22027,22810,29932,33391,45462,45477,45902,47541,48266,49410,49576,52665,58985,59752,59879,59921,61455,")
            End If
        ElseIf Main.MainInstance.char_race = 2 Then
            If Main.MainInstance.char_class = 1 Then
                addmultipleskills(
                    "26;1;1;43;1;5;44;1;5;55;1;5;95;1;5;125;1;1;162;1;5;172;1;5;183;1;1;413;1;1;414;1;1;415;1;1;433;1;1;")
                addmultiplespell(
                    "78,81,107,196,197,201,202,203,204,522,668,669,670,671,672,813,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,8737,9077,9078,9116,9125,17737,20572,20573,20574,21651,21652,22027,22810,29932,54562,")
            ElseIf Main.MainInstance.char_class = 3 Then
                addmultipleskills("44;1;5;45;1;5;51;1;1;95;1;5;125;1;1;162;1;5;163;1;1;172;1;5;414;1;1;415;1;1;")
                addmultiplespell(
                    "78,81,107,196,197,201,202,203,204,522,668,669,670,671,672,813,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,8737,9077,9078,9116,9125,17737,20572,20573,20574,21651,21652,22027,22810,29932,54562,")
            ElseIf Main.MainInstance.char_class = 4 Then
                addmultipleskills("38;1;1;95;1;5;125;1;1;162;1;5;173;1;5;176;1;5;253;1;1;414;1;1;415;1;1;")
                addmultiplespell(
                    "81,203,204,522,669,674,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,16092,20572,20573,20574,21184,21651,21652,22027,22810,54562,")
            ElseIf Main.MainInstance.char_class = 6 Then
                addmultipleskills(
                    "43;270;275;44;270;275;55;270;275;95;270;275;125;1;1;129;270;300;162;270;275;172;270;275;229;270;275;293;1;1;413;1;1;414;1;1;415;1;1;762;150;150;770;1;1;771;1;1;772;1;1;")
                addmultiplespell(
                    "81,196,197,200,201,202,203,204,522,669,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20572,20573,20574,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,54562,59879,59921,61455,")
            ElseIf Main.MainInstance.char_class = 7 Then
                addmultipleskills("54;1;5;95;1;5;125;1;1;136;1;5;162;1;5;375;1;1;414;1;1;415;1;1;433;1;1;573;1;1;")
                addmultiplespell(
                    "81,107,198,203,204,227,331,403,522,669,2382,2479,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9116,9125,20573,20574,21651,21652,22027,22810,27763,33697,54562,")
            ElseIf Main.MainInstance.char_class = 9 Then
                addmultipleskills("95;1;5;125;1;1;136;1;5;162;1;5;173;1;5;228;1;5;354;1;1;415;1;1;593;1;1;")
                addmultiplespell(
                    "81,203,204,227,522,669,686,687,1180,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,20573,20574,21651,21652,22027,22810,33702,54562,")
            End If
        ElseIf Main.MainInstance.char_race = 3 Then
            If Main.MainInstance.char_class = 1 Then
                addmultipleskills(
                    "26;1;1;44;1;5;54;1;5;55;1;5;95;1;5;101;1;1;162;1;5;172;1;5;413;1;1;414;1;1;415;1;1;433;1;1;")
                addmultiplespell(
                    "78,81,107,196,197,198,202,203,204,522,668,672,2382,2457,2479,2481,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20594,20595,20596,21651,21652,22027,22810,59224,")
            ElseIf Main.MainInstance.char_class = 2 Then
                addmultipleskills("54;1;5;95;1;5;101;1;1;160;1;5;162;1;5;413;1;1;414;1;1;415;1;1;433;1;1;594;1;1;")
                addmultiplespell(
                    "81,107,198,199,203,204,522,635,668,672,2382,2479,2481,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20594,20595,20596,21084,21651,21652,22027,22810,27762,34082,59224,")
            ElseIf Main.MainInstance.char_class = 3 Then
                addmultipleskills("44;1;5;46;1;5;51;1;1;95;1;5;101;1;1;162;1;5;163;1;1;172;1;5;414;1;1;415;1;1;")
                addmultiplespell(
                    "75,81,196,197,203,204,266,522,668,672,2382,2479,2481,2973,3018,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,13358,20594,20595,20596,21651,21652,22027,22810,24949,59224,")
            ElseIf Main.MainInstance.char_class = 4 Then
                addmultipleskills("38;1;1;95;1;5;101;1;1;162;1;5;173;1;5;176;1;5;253;1;1;414;1;1;415;1;1;")
                addmultiplespell(
                    "81,203,204,522,668,672,674,1180,1752,2098,2382,2479,2481,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,16092,20594,20595,20596,21184,21651,21652,22027,22810,59224,")
            ElseIf Main.MainInstance.char_class = 5 Then
                addmultipleskills("54;1;5;56;1;1;95;1;5;101;1;1;136;1;5;162;1;5;228;1;5;415;1;1;")
                addmultiplespell(
                    "81,198,203,204,227,522,585,668,672,2050,2382,2479,2481,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,20594,20595,20596,21651,21652,22027,22810,59224,")
            ElseIf Main.MainInstance.char_class = 6 Then
                addmultipleskills(
                    "43;270;275;44;270;275;55;270;275;95;270;275;101;1;1;129;270;300;162;270;275;172;270;275;229;270;275;293;1;1;413;1;1;414;1;1;415;1;1;762;150;150;770;1;1;771;1;1;772;1;1;")
                addmultiplespell(
                    "81,196,197,200,201,202,203,204,522,668,672,674,750,2382,2479,2481,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20594,20595,20596,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,59224,59879,59921,61455,")
            End If
        ElseIf Main.MainInstance.char_race = 4 Then
            If Main.MainInstance.char_class = 1 Then
                addmultipleskills(
                    "26;1;1;43;1;5;54;1;5;55;1;5;95;1;5;126;1;1;162;1;5;173;1;5;413;1;1;414;1;1;415;1;1;433;1;1;")
                addmultiplespell(
                    "78,81,107,198,201,202,203,204,522,668,671,1180,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20582,20583,20585,21009,21651,21652,22027,22810,58984,")
            ElseIf Main.MainInstance.char_class = 3 Then
                addmultipleskills("45;1;5;51;1;1;95;1;5;126;1;1;162;1;5;163;1;1;172;1;5;173;1;5;414;1;1;415;1;1;")
                addmultiplespell(
                    "75,81,197,203,204,264,522,668,671,1180,2382,2479,2973,3018,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,13358,20582,20583,20585,21009,21651,21652,22027,22810,24949,58984,")
            ElseIf Main.MainInstance.char_class = 4 Then
                addmultipleskills("38;1;1;95;1;5;126;1;1;162;1;5;173;1;5;176;1;5;253;1;1;414;1;1;415;1;1;")
                addmultiplespell(
                    "81,203,204,522,668,671,674,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,16092,20582,20583,20585,21009,21184,21651,21652,22027,22810,58984,")
            ElseIf Main.MainInstance.char_class = 5 Then
                addmultipleskills("54;1;5;56;1;1;95;1;5;126;1;1;136;1;5;162;1;5;228;1;5;415;1;1;")
                addmultiplespell(
                    "81,198,203,204,227,522,585,668,671,2050,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,20582,20583,20585,21009,21651,21652,22027,22810,58984,")
            ElseIf Main.MainInstance.char_class = 6 Then
                addmultipleskills(
                    "43;270;275;44;270;275;55;270;275;95;270;275;126;1;1;129;270;300;162;270;275;172;270;275;229;270;275;293;1;1;413;1;1;414;1;1;415;1;1;762;150;150;770;1;1;771;1;1;772;1;1;")
                addmultiplespell(
                    "81,196,197,200,201,202,203,204,522,668,671,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20582,20583,20585,21009,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,58984,59879,59921,61455,")
            ElseIf Main.MainInstance.char_class = 11 Then
                addmultipleskills("95;1;5;126;1;1;136;1;5;162;1;5;173;1;5;414;1;1;415;1;1;573;1;1;574;1;1;")
                addmultiplespell(
                    "81,203,204,227,522,668,671,1180,2382,2479,3050,3127,3365,5176,5185,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,20582,20583,20585,21009,21651,21652,22027,22810,27764,58984,")
            End If
        ElseIf Main.MainInstance.char_race = 5 Then
            If Main.MainInstance.char_class = 1 Then
                addmultipleskills("26;1;1;43;1;5;55;1;5;95;1;5;162;1;5;173;1;5;220;1;1;413;1;1;414;1;1;415;1;1;433;1;1;")
                addmultiplespell(
                    "78,81,107,201,202,203,204,522,669,1180,2382,2457,2479,3050,3127,3365,5227,5301,6233,6246,6247,6477,6478,6603,7266,7267,7355,7744,8386,8737,9077,9078,9116,9125,17737,20577,20579,21651,21652,22027,22810,")
            ElseIf Main.MainInstance.char_class = 4 Then
                addmultipleskills("38;1;1;95;1;5;162;1;5;173;1;5;176;1;5;220;1;1;253;1;1;414;1;1;415;1;1;")
                addmultiplespell(
                    "81,203,204,522,669,674,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,5227,6233,6246,6247,6477,6478,6603,7266,7267,7355,7744,8386,9077,9078,9125,16092,17737,20577,20579,21184,21651,21652,22027,22810,")
            ElseIf Main.MainInstance.char_class = 5 Then
                addmultipleskills("54;1;5;56;1;1;95;1;5;136;1;5;162;1;5;220;1;1;228;1;5;415;1;1;")
                addmultiplespell(
                    "81,198,203,204,227,522,585,669,2050,2382,2479,3050,3127,3365,5009,5019,5227,6233,6246,6247,6477,6478,6603,7266,7267,7355,7744,8386,9078,9125,17737,20577,20579,21651,21652,22027,22810,")
            ElseIf Main.MainInstance.char_class = 6 Then
                addmultipleskills(
                    "43;270;275;44;270;275;55;270;275;95;270;275;129;270;300;162;270;275;172;270;275;220;1;1;229;270;275;293;1;1;413;1;1;414;1;1;415;1;1;762;150;150;770;1;1;771;1;1;772;1;1;")
                addmultiplespell(
                    "81,196,197,200,201,202,203,204,522,669,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,5227,6233,6246,6247,6477,6478,6603,7266,7267,7355,7744,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,17737,18629,18630,20577,20579,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,59879,59921,61455,")
            ElseIf Main.MainInstance.char_class = 8 Then
                addmultipleskills("6;1;1;8;1;1;95;1;5;136;1;5;162;1;5;220;1;1;228;1;5;415;1;1;")
                addmultiplespell(
                    "81,133,168,203,204,227,522,669,2382,2479,3050,3127,3365,5009,5019,5227,6233,6246,6247,6477,6478,6603,7266,7267,7355,7744,8386,9078,9125,17737,20577,20579,21651,21652,22027,22810,")
            ElseIf Main.MainInstance.char_class = 9 Then
                addmultipleskills("95;1;5;136;1;5;162;1;5;173;1;5;220;1;1;228;1;5;354;1;1;415;1;1;593;1;1;")
                addmultiplespell(
                    "81,203,204,227,522,669,686,687,1180,2382,2479,3050,3127,3365,5009,5019,5227,6233,6246,6247,6477,6478,6603,7266,7267,7355,7744,8386,9078,9125,17737,20577,20579,21651,21652,22027,22810,")
            End If
        ElseIf Main.MainInstance.char_race = 6 Then
            If Main.MainInstance.char_class = 1 Then
                addmultipleskills(
                    "26;1;1;44;1;5;54;1;5;55;1;5;95;1;5;124;1;1;160;1;5;162;1;5;413;1;1;414;1;1;415;1;1;433;1;1;")
                addmultiplespell(
                    "78,81,107,196,198,199,202,203,204,522,669,670,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20549,20550,20551,20552,21651,21652,22027,22810,")
            ElseIf Main.MainInstance.char_class = 3 Then
                addmultipleskills("44;1;5;46;1;5;51;1;1;95;1;5;124;1;1;162;1;5;163;1;1;172;1;5;414;1;1;415;1;1;")
                addmultiplespell(
                    "75,81,196,197,203,204,266,522,669,670,2382,2479,2973,3018,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,13358,20549,20550,20551,20552,21651,21652,22027,22810,24949,")
            ElseIf Main.MainInstance.char_class = 6 Then
                addmultipleskills(
                    "43;270;275;44;270;275;55;270;275;95;270;275;124;1;1;129;270;300;162;270;275;172;270;275;229;270;275;293;1;1;413;1;1;414;1;1;415;1;1;762;150;150;770;1;1;771;1;1;772;1;1;")
                addmultiplespell(
                    "81,196,197,200,201,202,203,204,522,669,670,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20549,20550,20551,20552,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,59879,59921,61455,")
            ElseIf Main.MainInstance.char_class = 7 Then
                addmultipleskills("54;1;5;95;1;5;124;1;1;136;1;5;162;1;5;375;1;1;414;1;1;415;1;1;433;1;1;573;1;1;")
                addmultiplespell(
                    "81,107,198,203,204,227,331,403,522,669,670,2382,2479,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9116,9125,20549,20550,20551,20552,21651,21652,22027,22810,27763,")
            ElseIf Main.MainInstance.char_class = 11 Then
                addmultipleskills("54;1;5;95;1;5;124;1;1;136;1;5;162;1;5;414;1;1;415;1;1;573;1;1;574;1;1;")
                addmultiplespell(
                    "81,198,203,204,227,522,669,670,2382,2479,3050,3127,3365,5176,5185,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,20549,20550,20551,20552,21651,21652,22027,22810,27764,")
            End If
        ElseIf Main.MainInstance.char_race = 7 Then
            If Main.MainInstance.char_class = 1 Then
                addmultipleskills(
                    "26;1;1;43;1;5;54;1;5;55;1;5;95;1;5;162;1;5;173;1;5;413;1;1;414;1;1;415;1;1;433;1;1;753;1;1;")
                addmultiplespell(
                    "78,81,107,198,201,202,203,204,522,668,1180,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7340,7355,8386,8737,9077,9078,9116,9125,20589,20591,20592,20593,21651,21652,22027,22810,")
            ElseIf Main.MainInstance.char_class = 4 Then
                addmultipleskills("38;1;1;95;1;5;162;1;5;173;1;5;176;1;5;253;1;1;414;1;1;415;1;1;753;1;1;")
                addmultiplespell(
                    "81,203,204,522,668,674,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7340,7355,8386,9077,9078,9125,16092,20589,20591,20592,20593,21184,21651,21652,22027,22810,")
            ElseIf Main.MainInstance.char_class = 6 Then
                addmultipleskills(
                    "43;270;275;44;270;275;55;270;275;95;270;275;129;270;300;162;270;275;172;270;275;229;270;275;293;1;1;413;1;1;414;1;1;415;1;1;753;1;1;762;150;150;770;1;1;771;1;1;772;1;1;")
                addmultiplespell(
                    "81,196,197,200,201,202,203,204,522,668,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7340,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20589,20591,20592,20593,21651,21652,22027,22810,33391,45462,45477,45902,47541,48266,49410,49576,52665,59879,59921,61455,")
            ElseIf Main.MainInstance.char_class = 8 Then
                addmultipleskills("6;1;1;8;1;1;95;1;5;136;1;5;162;1;5;228;1;5;415;1;1;753;1;1;")
                addmultiplespell(
                    "81,133,168,203,204,227,522,668,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7340,7355,8386,9078,9125,20589,20591,20592,20593,21651,21652,22027,22810,")
            ElseIf Main.MainInstance.char_class = 9 Then
                addmultipleskills("95;1;5;136;1;5;162;1;5;173;1;5;228;1;5;354;1;1;415;1;1;593;1;1;753;1;1;")
                addmultiplespell(
                    "81,203,204,227,522,668,686,687,1180,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7340,7355,8386,9078,9125,20589,20591,20592,20593,21651,21652,22027,22810,")
            End If
        ElseIf Main.MainInstance.char_race = 8 Then
            If Main.MainInstance.char_class = 1 Then
                addmultipleskills(
                    "26;1;1;44;1;5;55;1;5;95;1;5;162;1;5;173;1;5;176;1;5;413;1;1;414;1;1;415;1;1;433;1;1;733;1;1;")
                addmultiplespell(
                    "78,81,107,196,202,203,204,522,669,1180,2382,2457,2479,2567,2764,3050,3127,3365,5301,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,8737,9077,9078,9116,9125,20555,20557,20558,21651,21652,22027,22810,26290,26297,58943,")
            ElseIf Main.MainInstance.char_class = 3 Then
                addmultipleskills("44;1;5;45;1;5;51;1;1;95;1;5;162;1;5;163;1;1;172;1;5;414;1;1;415;1;1;733;1;1;")
                addmultiplespell(
                    "75,81,196,197,203,204,264,522,669,2382,2479,2973,3018,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,9077,9078,9125,13358,20555,20557,20558,21651,21652,22027,22810,24949,26290,26297,58943,")
            ElseIf Main.MainInstance.char_class = 4 Then
                addmultipleskills("38;1;1;95;1;5;162;1;5;173;1;5;176;1;5;253;1;1;414;1;1;415;1;1;733;1;1;")
                addmultiplespell(
                    "81,203,204,522,669,674,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,9077,9078,9125,16092,20555,20557,20558,21184,21651,21652,22027,22810,26290,26297,58943,")
            ElseIf Main.MainInstance.char_class = 5 Then
                addmultipleskills("54;1;5;56;1;1;95;1;5;136;1;5;162;1;5;228;1;5;415;1;1;733;1;1;")
                addmultiplespell(
                    "81,198,203,204,227,522,585,669,2050,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,9078,9125,20555,20557,20558,21651,21652,22027,22810,26290,26297,58943,")
            ElseIf Main.MainInstance.char_class = 6 Then
                addmultipleskills(
                    "43;270;275;44;270;275;55;270;275;95;270;275;129;270;300;162;270;275;172;270;275;229;270;275;293;1;1;413;1;1;414;1;1;415;1;1;733;1;1;762;150;150;770;1;1;771;1;1;772;1;1;")
                addmultiplespell(
                    "81,196,197,200,201,202,203,204,522,669,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20555,20557,20558,21651,21652,22027,22810,26290,26297,33391,45462,45477,45902,47541,48266,49410,49576,52665,58943,59879,59921,61455,")
            ElseIf Main.MainInstance.char_class = 7 Then
                addmultipleskills("54;1;5;95;1;5;136;1;5;162;1;5;375;1;1;414;1;1;415;1;1;573;1;1;733;1;1;")
                addmultiplespell(
                    "81,107,198,203,204,227,331,403,522,669,2382,2479,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,9077,9078,9116,9125,20555,20557,20558,21651,21652,22027,22810,26290,26297,27763,58943,")
            ElseIf Main.MainInstance.char_class = 8 Then
                addmultipleskills("6;1;1;8;1;1;95;1;5;136;1;5;162;1;5;228;1;5;415;1;1;733;1;1;")
                addmultiplespell(
                    "81,133,168,203,204,227,522,669,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7341,7355,8386,9078,9125,20555,20557,20558,21651,21652,22027,22810,26290,26297,58943,")
            End If
        ElseIf Main.MainInstance.char_race = 10 Then
            If Main.MainInstance.char_class = 2 Then
                addmultipleskills("43;1;5;55;1;5;95;1;5;162;1;5;413;1;1;414;1;1;415;1;1;433;1;1;594;1;1;756;1;1;")
                addmultiplespell(
                    "81,107,201,202,203,204,522,635,669,813,822,2382,2479,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,21084,21651,21652,22027,22810,25046,27762,28877,34082,")
            ElseIf Main.MainInstance.char_class = 3 Then
                addmultipleskills("45;1;5;51;1;1;95;1;5;162;1;5;163;1;1;172;1;5;173;1;5;414;1;1;415;1;1;756;1;1;")
                addmultiplespell(
                    "75,81,197,203,204,264,522,669,813,822,1180,2382,2479,2973,3018,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,13358,21651,21652,22027,22810,24949,25046,28877,")
            ElseIf Main.MainInstance.char_class = 4 Then
                addmultipleskills("38;1;1;95;1;5;162;1;5;173;1;5;176;1;5;253;1;1;414;1;1;415;1;1;756;1;1;")
                addmultiplespell(
                    "81,203,204,522,669,674,813,822,1180,1752,2098,2382,2479,2567,2764,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9125,16092,21184,21651,21652,22027,22810,25046,28877,")
            ElseIf Main.MainInstance.char_class = 5 Then
                addmultipleskills("54;1;5;56;1;1;95;1;5;136;1;5;162;1;5;228;1;5;415;1;1;756;1;1;")
                addmultiplespell(
                    "81,198,203,204,227,522,585,669,813,822,2050,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,21651,21652,22027,22810,28730,28877,")
            ElseIf Main.MainInstance.char_class = 6 Then
                addmultipleskills(
                    "43;270;275;44;270;275;55;270;275;95;270;275;129;270;300;162;270;275;172;270;275;229;270;275;293;1;1;413;1;1;414;1;1;415;1;1;756;1;1;762;150;150;770;1;1;771;1;1;772;1;1;")
                addmultiplespell(
                    "81,196,197,200,201,202,203,204,522,669,674,750,813,822,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,21651,21652,22027,22810,28877,33391,45462,45477,45902,47541,48266,49410,49576,50613,52665,59879,59921,61455,")
            ElseIf Main.MainInstance.char_class = 8 Then
                addmultipleskills("6;1;1;8;1;1;95;1;5;136;1;5;162;1;5;228;1;5;415;1;1;756;1;1;")
                addmultiplespell(
                    "81,133,168,203,204,227,522,669,813,822,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,21651,21652,22027,22810,28730,28877,")
            ElseIf Main.MainInstance.char_class = 9 Then
                addmultipleskills("95;1;5;136;1;5;162;1;5;173;1;5;228;1;5;354;1;1;415;1;1;593;1;1;756;1;1;")
                addmultiplespell(
                    "81,203,204,227,522,669,686,687,813,822,1180,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,21651,21652,22027,22810,28730,28877,")
            End If
        ElseIf Main.MainInstance.char_race = 11 Then
            If Main.MainInstance.char_class = 1 Then
                addmultipleskills("26;1;1;43;1;5;54;1;5;55;1;5;95;1;5;162;1;5;413;1;1;414;1;1;415;1;1;433;1;1;760;1;1;")
                addmultiplespell(
                    "78,81,107,198,201,202,203,204,522,668,2382,2457,2479,3050,3127,3365,5301,6233,6246,6247,6477,6478,6562,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20579,21651,21652,22027,22810,28875,28880,29932,32215,")
            ElseIf Main.MainInstance.char_class = 2 Then
                addmultipleskills("54;1;5;95;1;5;160;1;5;162;1;5;413;1;1;414;1;1;415;1;1;433;1;1;594;1;1;760;1;1;")
                addmultiplespell(
                    "81,107,198,199,203,204,522,635,668,2382,2479,3050,3127,3365,6233,6246,6247,6477,6478,6562,6603,7266,7267,7355,8386,8737,9077,9078,9116,9125,20579,21084,21651,21652,22027,22810,27762,28875,28880,29932,34082,")
            ElseIf Main.MainInstance.char_class = 3 Then
                addmultipleskills("43;1;5;51;1;1;95;1;5;162;1;5;163;1;1;172;1;5;226;1;5;414;1;1;415;1;1;760;1;1;")
                addmultiplespell(
                    "75,81,197,201,203,204,522,668,2382,2479,2973,3018,3050,3127,3365,5011,6233,6246,6247,6477,6478,6562,6603,7266,7267,7355,8386,9077,9078,9125,13358,20579,21651,21652,22027,22810,24949,28875,28880,29932,")
            ElseIf Main.MainInstance.char_class = 5 Then
                addmultipleskills("54;1;5;56;1;1;95;1;5;136;1;5;162;1;5;228;1;5;415;1;1;760;1;1;")
                addmultiplespell(
                    "81,198,203,204,227,522,585,668,2050,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,20579,21651,21652,22027,22810,28875,28878,28880,29932,")
            ElseIf Main.MainInstance.char_class = 6 Then
                addmultipleskills(
                    "43;270;275;44;270;275;55;270;275;95;270;275;129;270;300;162;270;275;172;270;275;229;270;275;293;1;1;413;1;1;414;1;1;415;1;1;760;1;1;762;150;150;770;1;1;771;1;1;772;1;1;")
                addmultiplespell(
                    "81,196,197,200,201,202,203,204,522,668,674,750,2382,2479,3050,3127,3275,3276,3277,3278,3365,6233,6246,6247,6477,6478,6562,6603,7266,7267,7355,7928,7929,7934,8386,8737,9077,9078,9125,10840,10841,10846,18629,18630,20579,21651,21652,22027,22810,28875,28880,29932,33391,45462,45477,45902,47541,48266,49410,49576,52665,59879,59921,61455,")
            ElseIf Main.MainInstance.char_class = 7 Then
                addmultipleskills("54;1;5;95;1;5;136;1;5;162;1;5;375;1;1;414;1;1;415;1;1;433;1;1;573;1;1;760;1;1;")
                addmultiplespell(
                    "81,107,198,203,204,227,331,403,522,668,2382,2479,3050,3127,3365,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9077,9078,9116,9125,20579,21651,21652,22027,22810,27763,28875,28878,28880,29932,")
            ElseIf Main.MainInstance.char_class = 8 Then
                addmultipleskills("6;1;1;8;1;1;95;1;5;136;1;5;162;1;5;228;1;5;415;1;1;760;1;1;")
                addmultiplespell(
                    "81,133,168,203,204,227,522,668,2382,2479,3050,3127,3365,5009,5019,6233,6246,6247,6477,6478,6603,7266,7267,7355,8386,9078,9125,20579,21651,21652,22027,22810,28875,28878,28880,29932,")
            End If
        End If
        '#NEW#
        'Setting tutorials
        runfunction.normalsqlcommand("INSERT INTO `tutorials` ( playerId ) VALUES ( " & Main.MainInstance.coreguid & " )")
        addfinishedquests()
        ' additems() // SHOULD NOT BE INVOKED HERE!!!
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Created Character " & charactername & "!" & vbNewLine)
        Application.DoEvents()
    End Sub

    Public Sub addmultipleskills(ByVal skillstring As String)

        Dim excounter As Integer = UBound(skillstring.Split(CChar(";")))
        Dim startcounter As Integer = 0
        Do
            Dim parts() As String = skillstring.Split(";"c)
            Dim skillid As Integer = CInt(parts(startcounter))
            startcounter += 1
            Dim standing As Integer = CInt(parts(startcounter))
            startcounter += 1
            Dim basestanding As Integer = CInt(parts(startcounter))
            startcounter += 1
            addsingleskill(skillid, standing, basestanding)

        Loop Until startcounter = excounter
    End Sub

    Public Sub addmultiplespell(ByVal spells As String)
        Dim excounter As Integer = UBound(spells.Split(CChar(",")))
        Dim startcounter As Integer = 0
        Do
            Dim parts() As String = spells.Split(","c)
            Dim spellid As Integer = CInt(parts(startcounter))
            addsinglespell(spellid)
            startcounter += 1
        Loop Until startcounter = excounter
    End Sub

    Public Sub addsingleskill(ByVal skillid As Integer, Optional ByVal value As Integer = 1,
                              Optional ByVal max As Integer = 1)
        Dim skillstring As String =
                runfunction.runcommand("SELECT `skills` FROM characters WHERE `guid`='" & Main.MainInstance.coreguid & "'", "skills")
        runfunction.normalsqlcommand(
            "UPDATE characters SET skills='" & skillstring & skillid.ToString & ";" & value.ToString & ";" &
            max.ToString & ";' WHERE guid='" & Main.MainInstance.coreguid & "'")
    End Sub

    Public Sub addsinglespell(ByVal spellid As Integer)
        Dim skillstring As String =
                runfunction.runcommand("SELECT `spells` FROM characters WHERE `guid`='" & Main.MainInstance.coreguid & "'", "spells")
        runfunction.normalsqlcommand(
            "UPDATE characters SET spells='" & skillstring & spellid.ToString & ",' WHERE guid='" & Main.MainInstance.coreguid & "'")
    End Sub

    Public Sub sethome()
        Dim tmpstring As String = Main.MainInstance.character_homebind
        runfunction.normalsqlcommand(
            "UPDATE characters SET bindpositionX='" & splitlist(Main.MainInstance.character_homebind, "position_x") &
            "' WHERE guid='" & Main.MainInstance.coreguid & "'")
        runfunction.normalsqlcommand(
            "UPDATE characters SET bindpositionY='" & splitlist(Main.MainInstance.character_homebind, "position_y") &
            "' WHERE guid='" & Main.MainInstance.coreguid & "'")
        runfunction.normalsqlcommand(
            "UPDATE characters SET bindpositionZ='" & splitlist(Main.MainInstance.character_homebind, "position_z") &
            "' WHERE guid='" & Main.MainInstance.coreguid & "'")
        runfunction.normalsqlcommand(
            "UPDATE characters SET bindmapId='" & splitlist(Main.MainInstance.character_homebind, "map") & "' WHERE guid='" &
            Main.MainInstance.coreguid & "'")
        runfunction.normalsqlcommand(
            "UPDATE characters SET bindzoneId='" & splitlist(Main.MainInstance.character_homebind, "zone") & "' WHERE guid='" &
            Main.MainInstance.coreguid & "'")
    End Sub

    Public Sub adddetailedchar(ByVal targetaccount As String, ByVal charactername As String,
                               ByVal namechangeeverytime As Boolean)

        Dim newcharguid As Integer =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", "guid"))) + 1
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Creating Character " & charactername & "!" & vbNewLine)
        guid = newcharguid.ToString
        Main.MainInstance.coreguid = newcharguid.ToString
        targetaccount = runfunction.runcommandRealmd("SELECT acct FROM accounts WHERE login='" & targetaccount & "'",
                                                     "acct")
        If namechangeeverytime = True Then
            Process_Status.processreport.AppendText(
                Now.TimeOfDay.ToString & "// Player will be asked to change charactername! : reason-nce=true" &
                vbNewLine)
            runfunction.normalsqlcommand(
                "INSERT INTO characters ( `guid`, `acct`, `name`, `race`, `class`, `gender`, `level`, `xp`, `gold`, `bytes`, `bytes2`, `player_flags`, `positionX`, positionY, positionZ, mapId, orientation, taximask, playedtime, totalstableslots, zoneId, selected_pvp_title, watched_faction_index, current_hp, numspecs, currentspec, exploration_data, available_pvp_titles ) VALUES ( '" &
                newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                "', '0', '0', '0', '1', '0', '0', '" & Main.MainInstance.playerBytes.ToString & "', '" & Main.MainInstance.playerBytes2.ToString &
                "', '" & Main.MainInstance.playerFlags.ToString & "', '" & Main.MainInstance.position_x.ToString & "', '" &
                Main.MainInstance.position_y.ToString & "', '" & (CInt(Main.MainInstance.position_z) + 1).ToString & "', '" & Main.MainInstance.map.ToString &
                "', '4,40671', '" & Main.MainInstance.taximask & "', '0 0 0 ', '" & Main.MainInstance.stable_slots & "', '" & Main.MainInstance.zone.ToString &
                "', '" & Main.MainInstance.chosenTitle & "', '" & Main.MainInstance.watchedFaction & "', '1000', '" & Main.MainInstance.speccount.ToString &
                "', '" & Main.MainInstance.activespec.ToString & "', '" & Main.MainInstance.exploredZones & "', '" & Main.MainInstance.knownTitles & "' )")
            'PlayerBytes column might not be correct! check player_bytes, bytes, bytes2
            'playedtime format might be different: playedtime, leveltime
            runfunction.normalsqlcommand(
                "UPDATE characters SET forced_rename_pending='1' WHERE guid='" & newcharguid.ToString & "'")
        Else
            If charexist(charactername) = True Then
                Process_Status.processreport.AppendText(
                    Now.TimeOfDay.ToString & "// Player will be asked to change charactername!" & vbNewLine)
                runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `guid`, `acct`, `name`, `race`, `class`, `gender`, `level`, `xp`, `gold`, `bytes`, `bytes2`, `player_flags`, `positionX`, positionY, positionZ, mapId, orientation, taximask, playedtime, totalstableslots, zoneId, selected_pvp_title, watched_faction_index, current_hp, numspecs, currentspec, exploration_data, available_pvp_titles ) VALUES ( '" &
                    newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '" & Main.MainInstance.playerBytes.ToString & "', '" &
                    Main.MainInstance.playerBytes2.ToString & "', '" & Main.MainInstance.playerFlags.ToString & "', '" & Main.MainInstance.position_x.ToString &
                    "', '" & Main.MainInstance.position_y.ToString & "', '" & (CInt(Main.MainInstance.position_z) + 1).ToString & "', '" &
                    Main.MainInstance.map.ToString & "', '4,40671', '" & Main.MainInstance.taximask & "', '0 0 0 ', '" & Main.MainInstance.stable_slots &
                    "', '" & Main.MainInstance.zone.ToString & "', '" & Main.MainInstance.chosenTitle & "', '" & Main.MainInstance.watchedFaction &
                    "', '1000', '" & Main.MainInstance.speccount.ToString & "', '" & Main.MainInstance.activespec.ToString & "', '" &
                    Main.MainInstance.exploredZones & "', '" & Main.MainInstance.knownTitles & "' )")
                runfunction.normalsqlcommand(
                    "UPDATE characters SET forced_rename_pending='1' WHERE guid='" & newcharguid.ToString & "'")
            Else
                runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `guid`, `acct`, `name`, `race`, `class`, `gender`, `level`, `xp`, `gold`, `bytes`, `bytes2`, `player_flags`, `positionX`, positionY, positionZ, mapId, orientation, taximask, playedtime, totalstableslots, zoneId, selected_pvp_title, watched_faction_index, current_hp, numspecs, currentspec, exploration_data, available_pvp_titles ) VALUES ( '" &
                    newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '" & Main.MainInstance.playerBytes.ToString & "', '" &
                    Main.MainInstance.playerBytes2.ToString & "', '" & Main.MainInstance.playerFlags.ToString & "', '" & Main.MainInstance.position_x.ToString &
                    "', '" & Main.MainInstance.position_y.ToString & "', '" & (CInt(Main.MainInstance.position_z) + 1).ToString & "', '" &
                    Main.MainInstance.map.ToString & "', '4,40671', '" & Main.MainInstance.taximask & "', '0 0 0 ', '" & Main.MainInstance.stable_slots &
                    "', '" & Main.MainInstance.zone.ToString & "', '" & Main.MainInstance.chosenTitle & "', '" & Main.MainInstance.watchedFaction &
                    "', '1000', '" & Main.MainInstance.speccount.ToString & "', '" & Main.MainInstance.activespec.ToString & "', '" &
                    Main.MainInstance.exploredZones & "', '" & Main.MainInstance.knownTitles & "' )")

            End If

        End If
        addsinglespell(6603) 'auto attack
        If Not Main.MainInstance.custom_faction = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE characters SET custom_faction='" & Main.MainInstance.custom_faction & "' WHERE guid='" & newcharguid.ToString &
                "'")
        'additems() // SHOULD NOT BE INVOKED HERE
        '#NEW#
        'Setting tutorials
        runfunction.normalsqlcommand("INSERT INTO `tutorials` ( playerId ) VALUES ( " & Main.MainInstance.coreguid & " )")
        addfinishedquests()
        sethome()
        addaction()
        setqueststatus()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Created Character " & charactername & "!" & vbNewLine)
        Application.DoEvents()
    End Sub

    Public Sub requestnamechange(ByVal charname As String)
        runfunction.normalsqlcommand("UPDATE characters SET forced_rename_pending='1' WHERE name='" & charname & "'")
    End Sub

    Public Function charexist(ByVal charname As String) As Boolean

        Try

            '  ("SELECT * FROM characters WHERE name = '" & charname.Text & "'")
            Dim myAdapter As New MySqlDataAdapter

            Dim sqlquery = "SELECT * FROM characters WHERE name = '" & charname & "'"
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
        '  Dim accguid As String = runfunction.runcommandRealmd("SELECT id FROM account WHERE username='" & accountname & "'", "id")

        runfunction.normalsqlcommand("UPDATE characters SET forced_rename_pending='1' WHERE name='" & charname & "'")
    End Sub
    Public Sub addfinishedquests()
        runfunction.normalsqlcommand("UPDATE characters SET finished_quests='" & Main.MainInstance.finished_quests & "' WHERE guid='" & Main.MainInstance.coreguid & "'")
    End Sub
    Public Sub getguidfromname(ByVal charactername As String)
        guid = runfunction.runcommand("SELECT guid FROM characters WHERE name = '" & charactername & "'", "guid")
        Main.MainInstance.coreguid = guid
        addaction()
    End Sub

    Public Sub additems()
        guid = Main.MainInstance.coreguid
        finalstring =
            "kopf 0 hals 0 schulter 0 hemd 0 brust 0 guertel 0 beine 0 stiefel 0 handgelenke 0 haende 0 finger1 0 finger2 0 schmuck1 0 schmuck2 0 ruecken 0 haupt 0 off 0 distanz 0 wappenrock 0 "
        lastnumber =
            runfunction.runcommand("SELECT guid FROM playeritems WHERE guid=(SELECT MAX(guid) FROM playeritems)", "guid")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Creating Items for Character: " & Main.MainInstance.char_name & vbNewLine)
        Application.DoEvents()
        If Main.MainInstance.char_class = 1 Or Main.MainInstance.char_class = 2 Or Main.MainInstance.char_class = 6 Then
            addsinglespell(750)
            addsingleskill(293)
        ElseIf _
            Main.MainInstance.char_class = 1 Or Main.MainInstance.char_class = 2 Or Main.MainInstance.char_class = 3 Or Main.MainInstance.char_class = 6 Or
            Main.MainInstance.char_class = 7 Then
            addsinglespell(8737)
            addsingleskill(413)
        ElseIf _
            Main.MainInstance.char_class = 1 Or Main.MainInstance.char_class = 2 Or Main.MainInstance.char_class = 3 Or Main.MainInstance.char_class = 4 Or
            Main.MainInstance.char_class = 6 Or Main.MainInstance.char_class = 7 Or Main.MainInstance.char_class = 11 Then
            addsinglespell(9077)
            addsingleskill(414)
        Else

        End If
        Dim specialskill As String
        Dim specialspell As String
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Creating special spells and skills for relevant items..." & vbNewLine)
        Dim spellcounter As Integer = 0
        Dim skillcounter As Integer = 0
        For Each specialskill In Main.MainInstance.specialskills
            Try
                addsingleskill(CInt(specialskill))
            Catch ex As Exception

            End Try
            skillcounter += 1
        Next
        For Each specialspell In Main.MainInstance.specialspells
            Try
                addsinglespell(CInt(specialspell))
            Catch ex As Exception

            End Try
            spellcounter += 1
        Next
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Created " & spellcounter.ToString & " spells and " & skillcounter.ToString &
            " skills!" & vbNewLine)

        If Not Main.MainInstance.kopfid = Nothing Then

            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            kopfwearguid = CInt(lastnumber)
            makestring(Main.MainInstance.kopfid, "kopf", False)
            checkexist_anddelete(guid, "0", lastnumber, Main.MainInstance.kopfid)
        End If

        If Not Main.MainInstance.halsid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            halswearguid = CInt(lastnumber)
            makestring(Main.MainInstance.halsid, "hals", False)
            checkexist_anddelete(guid, "1", lastnumber, Main.MainInstance.halsid)
        End If
        If Not Main.MainInstance.schulterid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            schulterwearguid = CInt(lastnumber)
            makestring(Main.MainInstance.schulterid, "schulter", False)
            checkexist_anddelete(guid, "2", lastnumber, Main.MainInstance.schulterid)
        End If

        If Not Main.MainInstance.rueckenid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            rueckenwearguid = CInt(lastnumber)
            makestring(Main.MainInstance.rueckenid, "ruecken", False)
            checkexist_anddelete(guid, "14", lastnumber, Main.MainInstance.rueckenid)
        End If
        If Not Main.MainInstance.brustid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            brustwearguid = CInt(lastnumber)
            makestring(Main.MainInstance.brustid, "brust", False)
            checkexist_anddelete(guid, "4", lastnumber, Main.MainInstance.brustid)
        End If
        If Not Main.MainInstance.hemdid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            hemdwearguid = CInt(lastnumber)
            makestring(Main.MainInstance.hemdid, "hemd", False)
            checkexist_anddelete(guid, "3", lastnumber, Main.MainInstance.hemdid)
        End If
        If Not Main.MainInstance.wappenrockid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            wappenrockwearguid = CInt(lastnumber)
            makestring(Main.MainInstance.wappenrockid, "wappenrock", False)
            checkexist_anddelete(guid, "18", lastnumber, Main.MainInstance.wappenrockid)
        End If
        If Not Main.MainInstance.handgelenkeid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            handgelenkewearguid = CInt(lastnumber)
            makestring(Main.MainInstance.handgelenkeid, "handgelenke", False)
            checkexist_anddelete(guid, "8", lastnumber, Main.MainInstance.handgelenkeid)
        End If
        If Not Main.MainInstance.hauptid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            hauptwearguid = CInt(lastnumber)
            makestring(Main.MainInstance.hauptid, "haupt", False)
            checkexist_anddelete(guid, "15", lastnumber, Main.MainInstance.hauptid)
        End If
        If Not Main.MainInstance.offid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            offwearguid = CInt(lastnumber)
            makestring(Main.MainInstance.offid, "off", False)
            checkexist_anddelete(guid, "16", lastnumber, Main.MainInstance.offid)
        End If
        If Not Main.MainInstance.distanzid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            distanzwearguid = CInt(lastnumber)
            makestring(Main.MainInstance.distanzid, "distanz", False)
            checkexist_anddelete(guid, "17", lastnumber, Main.MainInstance.distanzid)
        End If
        If Not Main.MainInstance.haendeid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            haendewearguid = CInt(lastnumber)
            makestring(Main.MainInstance.haendeid, "haende", False)
            checkexist_anddelete(guid, "9", lastnumber, Main.MainInstance.haendeid)
        End If
        If Not Main.MainInstance.guertelid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            guertelwearguid = CInt(lastnumber)
            makestring(Main.MainInstance.guertelid, "guertel", False)
            checkexist_anddelete(guid, "5", lastnumber, Main.MainInstance.guertelid)
        End If
        If Not Main.MainInstance.beineid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            beinewearguid = CInt(lastnumber)
            makestring(Main.MainInstance.beineid, "beine", False)
            checkexist_anddelete(guid, "6", lastnumber, Main.MainInstance.beineid)
        End If
        If Not Main.MainInstance.stiefelid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            stiefelwearguid = CInt(lastnumber)
            makestring(Main.MainInstance.stiefelid, "stiefel", False)
            checkexist_anddelete(guid, "7", lastnumber, Main.MainInstance.stiefelid)
        End If
        If Not Main.MainInstance.ring1id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            ring1wearguid = CInt(lastnumber)
            makestring(Main.MainInstance.ring1id, "finger1", False)
            checkexist_anddelete(guid, "10", lastnumber, Main.MainInstance.ring1id)
        End If
        If Not Main.MainInstance.ring2id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            ring2wearguid = CInt(lastnumber)
            makestring(Main.MainInstance.ring2id, "finger2", False)
            checkexist_anddelete(guid, "11", lastnumber, Main.MainInstance.ring2id)
        End If
        If Not Main.MainInstance.schmuck1id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            schmuck1wearguid = CInt(lastnumber)
            makestring(Main.MainInstance.schmuck1id, "schmuck1", False)
            checkexist_anddelete(guid, "12", lastnumber, Main.MainInstance.schmuck1id)
        End If
        If Not Main.MainInstance.schmuck2id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            schmuck2wearguid = CInt(lastnumber)
            makestring(Main.MainInstance.schmuck2id, "schmuck2", False)
            checkexist_anddelete(guid, "13", lastnumber, Main.MainInstance.schmuck2id)
        End If
        makestring(0, "", True)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Created Items!" & vbNewLine)
    End Sub

    Public Sub addglyphs(ByVal expansion As String)

        guid = Main.MainInstance.coreguid
        checkglyphsanddelete(Main.MainInstance.coreguid)
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding Glyphs to Character: " & Main.MainInstance.char_name & vbNewLine)
        Dim glyphstring1 As String = "major1,minor1,minor2,major2,minor3,major3,"
        Dim glyphstring2 As String = "major1,minor1,minor2,major2,minor3,major3,"

        glyphstring1 = glyphstring1.Replace("minor1", (runfunction.getglyphid2(Main.MainInstance.minorglyph1)).ToString)
        glyphstring1 = glyphstring1.Replace("minor2", (runfunction.getglyphid2(Main.MainInstance.minorglyph2)).ToString)
        glyphstring1 = glyphstring1.Replace("minor3", (runfunction.getglyphid2(Main.MainInstance.minorglyph3)).ToString)
        glyphstring1 = glyphstring1.Replace("major1", (runfunction.getglyphid2(Main.MainInstance.majorglyph1)).ToString)
        glyphstring1 = glyphstring1.Replace("major2", (runfunction.getglyphid2(Main.MainInstance.majorglyph2)).ToString)
        glyphstring1 = glyphstring1.Replace("major3", (runfunction.getglyphid2(Main.MainInstance.majorglyph3)).ToString)

        glyphstring2 = glyphstring2.Replace("minor1", (runfunction.getglyphid2(Main.MainInstance.secminorglyph1)).ToString)
        glyphstring2 = glyphstring2.Replace("minor2", (runfunction.getglyphid2(Main.MainInstance.secminorglyph2)).ToString)
        glyphstring2 = glyphstring2.Replace("minor3", (runfunction.getglyphid2(Main.MainInstance.secminorglyph3)).ToString)
        glyphstring2 = glyphstring2.Replace("major1", (runfunction.getglyphid2(Main.MainInstance.secmajorglyph1)).ToString)
        glyphstring2 = glyphstring2.Replace("major2", (runfunction.getglyphid2(Main.MainInstance.secmajorglyph2)).ToString)
        glyphstring2 = glyphstring2.Replace("major3", (runfunction.getglyphid2(Main.MainInstance.secmajorglyph3)).ToString)


        runfunction.normalsqlcommand(
            "UPDATE characters SET glyphs1='" & glyphstring1 & "' WHERE guid='" & Main.MainInstance.coreguid & "'")
        runfunction.normalsqlcommand(
            "UPDATE characters SET glyphs2='" & glyphstring2 & "' WHERE guid='" & Main.MainInstance.coreguid & "'")
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Added Glyphs!" & vbNewLine)
    End Sub

    Public Sub setgender(ByVal gender As String)
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting gender for Character: " & Main.MainInstance.char_name & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand("UPDATE `characters` SET gender='" & gender & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setlevel()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting Level for Character: " & Main.MainInstance.char_name & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET level='" & Main.MainInstance.char_level.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setrace()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting race for Character: " & Main.MainInstance.char_name & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET race='" & Main.MainInstance.char_race.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setclass()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting class for Character: " & Main.MainInstance.char_name & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET `class`='" & Main.MainInstance.char_class.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setalternatelevel(ByVal alternatelevel As String)
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting alternative level for Character: " & Main.MainInstance.char_name & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand("UPDATE `characters` SET level='" & alternatelevel & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setgold(ByVal amount As String)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding gold..." & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET gold='" & (CInt(Val(amount)) * 10000).ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub addgold(ByVal amount As Integer)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding gold..." & vbNewLine)
        guid = Main.MainInstance.coreguid
        runfunction.normalsqlcommand("UPDATE `characters` SET gold='" & amount.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub addtalents()
        rank = ""
        rank2 = ""
        sdatatable.Clear()
        sdatatable.Dispose()
        sdatatable = gettable()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting Talents for Character: " & Main.MainInstance.char_name & vbNewLine)
        Dim talentlist As String = ""
        Dim talentlist2 As String = ""
        Dim finaltalentstring As String = ""
        Dim finaltalentstring2 As String = ""
        'talentid/rank
        For Each talentstring As String In Main.MainInstance.character_talent_list
            Dim spellid As String = splitlist(talentstring, "spell")
            If spellid.Contains("clear") Then
                talentid = spellid.Replace("clear", "")
                Dim spec As String = splitlist(talentstring, "spec")
                If spec = "0" Then
                    finaltalentstring = finaltalentstring & talentid & ",0,"
                Else
                    finaltalentstring2 = finaltalentstring2 & talentid & ",0,"
                End If
            Else
                talentid = checkfield(spellid)
                Dim spec As String = splitlist(talentstring, "spec")
                If spec = "0" Then
                    If talentlist.Contains(talentid) Then
                        If talentlist.Contains(talentid & "rank5") Then

                        ElseIf talentlist.Contains(talentid & "rank4") Then
                            If CInt(Val(rank)) <= 4 Then
                            Else
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",0",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",1",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",2",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",3",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",4",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try
                                talentlist = talentlist & " " & talentid & "rank" & rank
                            End If
                        ElseIf talentlist.Contains(talentid & "rank3") Then
                            If CInt(Val(rank)) <= 3 Then
                            Else
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",0",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",1",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",2",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",3",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try
                                talentlist = talentlist & " " & talentid & "rank" & rank
                            End If
                        ElseIf talentlist.Contains(talentid & "rank2") Then
                            If CInt(Val(rank)) <= 2 Then
                            Else
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",0",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",1",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",2",
                                                                                  (CInt(Val(rank)) - 1).ToString)
                                Catch ex As Exception

                                End Try
                                talentlist = talentlist & " " & talentid & "rank" & rank
                            End If
                        ElseIf talentlist.Contains(talentid & "rank1") Then
                            If CInt(Val(rank)) <= 1 Then
                            Else
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",0",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring = finaltalentstring.Replace(talentid & ",1",
                                                                                  (CInt(Val(rank))).ToString)
                                Catch ex As Exception

                                End Try

                                talentlist = talentlist & " " & talentid & "rank" & rank
                            End If
                        Else

                        End If
                    Else
                        finaltalentstring = finaltalentstring & talentid & "," & (CInt(Val(rank))).ToString & ","
                        talentlist = talentlist & " " & talentid & "rank" & rank

                    End If
                Else
                    'spec 1

                    If talentlist2.Contains(talentid) Then
                        If talentlist2.Contains(talentid & "rank5") Then

                        ElseIf talentlist2.Contains(talentid & "rank4") Then
                            If CInt(Val(rank2)) <= 4 Then
                            Else
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",0",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",1",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",2",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",3",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",4",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try
                                talentlist2 = talentlist2 & " " & talentid & "rank" & rank
                            End If
                        ElseIf talentlist2.Contains(talentid & "rank3") Then
                            If CInt(Val(rank2)) <= 3 Then
                            Else
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",0",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",1",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",2",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",3",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try

                                talentlist2 = talentlist2 & " " & talentid & "rank" & rank
                            End If
                        ElseIf talentlist2.Contains(talentid & "rank2") Then
                            If CInt(Val(rank2)) <= 2 Then
                            Else
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",0",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",1",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",2",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try

                                talentlist2 = talentlist2 & " " & talentid & "rank" & rank
                            End If
                        ElseIf talentlist2.Contains(talentid & "rank1") Then
                            If CInt(Val(rank2)) <= 1 Then
                            Else
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",0",
                                                                                    (CInt(Val(rank2))).ToString)
                                Catch ex As Exception

                                End Try
                                Try
                                    finaltalentstring2 = finaltalentstring2.Replace(talentid & ",1",
                                                                                    (CInt(Val(rank2))).ToString)
                                    'rank - 1 ???
                                Catch ex As Exception

                                End Try

                                talentlist2 = talentlist2 & " " & talentid & "rank" & rank
                            End If
                        Else

                        End If
                    Else
                        finaltalentstring2 = finaltalentstring2 & talentid & "," & (CInt(Val(rank2))).ToString & ","
                        'rank - 1 ???
                        talentlist2 = talentlist2 & " " & talentid & "rank" & rank

                    End If
                End If
            End If


            runfunction.normalsqlcommand(
                "UPDATE characters SET talents1='" & finaltalentstring & "' WHERE guid='" & Main.MainInstance.coreguid & "'")
            runfunction.normalsqlcommand(
                "UPDATE characters SET talents2='" & finaltalentstring2 & "' WHERE guid='" & Main.MainInstance.coreguid & "'")
            '  ("<spell>" & spell & "</spell><spec>" & spec & "</spec>")
        Next
    End Sub

    Private Function checkfield(ByVal lID As String) As String
        If Not executex("Rang1", lID) = "-" Then
            rank = "0"
            rank2 = "0"
            Return (executex("Rang1", lID))
        ElseIf Not executex("Rang2", lID) = "-" Then
            rank = "1"
            rank2 = "1"
            Return (executex("Rang2", lID))
        ElseIf Not executex("Rang3", lID) = "-" Then
            rank = "2"
            rank2 = "2"
            Return (executex("Rang3", lID))
        ElseIf Not executex("Rang4", lID) = "-" Then
            rank = "3"
            rank2 = "3"
            Return (executex("Rang4", lID))
        ElseIf Not executex("Rang5", lID) = "-" Then
            rank = "4"
            rank2 = "4"
            Return (executex("Rang5", lID))
        Else
            rank = "0"
            rank2 = "0"
            Return "0"
        End If
    End Function

    Private Function executex(ByVal field As String, ByVal sID As String) As String
        Try
            Dim foundRows() As DataRow

            foundRows = sdatatable.Select(field & " = '" & sID & "'")
            If foundRows.Length = 0 Then
                Return "-"

            Else
                Dim i As Integer
                Dim tmpreturn As String = "-"
                For i = 0 To foundRows.GetUpperBound(0)
                    tmpreturn = (foundRows(i)(0)).ToString

                Next i
                Return tmpreturn
            End If

        Catch ex As Exception
            Return "-"
        End Try
    End Function

    Private Function gettable() As DataTable
        Try
            Dim dt As New DataTable()
            Dim stext As String = My.Resources.Talent

            Dim a() As String

            Dim strArray As String()

            a = Split(stext, vbNewLine)

            For i = 0 To UBound(a)
                strArray = a(i).Split(CChar(";"))
                If i = 0 Then
                    For Each value As String In strArray
                        dt.Columns.Add(value.Trim())
                    Next
                Else
                    Dim dr As DataRow = dt.NewRow()


                    dt.Rows.Add(strArray)

                End If

            Next i
            Return dt
        Catch ex As Exception
            Return New DataTable
        End Try
    End Function

    Public Sub setqueststatus()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting queststatus for Character: " & Main.MainInstance.char_name & vbNewLine)
        Dim lastslot As Integer =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT slot FROM questlog WHERE player_guid='" & Main.MainInstance.coreguid &
                            "' AND slot=(SELECT MAX(slot) FROM characters)", "slot"))) + 1
        For Each queststring As String In Main.MainInstance.character_queststatus
            Dim explored As String = splitlist(queststring, "explored")
            If explored = Nothing Then explored = ""
            Dim tmpcommand As String = ""
            tmpcommand =
                "INSERT INTO questlog ( player_guid, quest_id, slot, `completed`, `explored_area1` ) VALUES ( '" &
                Main.MainInstance.coreguid & "', '" & splitlist(queststring, "quest") & "', '" & lastslot.ToString & "', '" &
                splitlist(queststring, "status") & "',"
            tmpcommand = tmpcommand & " '" & explored & "')"
            runfunction.normalsqlcommand(tmpcommand)
            lastslot += 1

        Next
        If Not Main.MainInstance.finished_quests = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE characters SET finished_quests='" & Main.MainInstance.finished_quests & "' WHERE guid='" & Main.MainInstance.coreguid &
                "'")
    End Sub

    Public Sub addachievements()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding achievements for Character: " & Main.MainInstance.char_name & vbNewLine)
        For Each avstring As String In Main.MainInstance.character_achievement_list
            runfunction.normalsqlcommand(
                "INSERT INTO character_achievement ( guid, achievement, date ) VALUES ( '" & Main.MainInstance.coreguid & "', '" &
                splitlist(avstring, "av") & "', '" & splitlist(avstring, "date") & "')")

            ' "<av>" & avid & "</av><date>" & xdate & "</date>"
        Next
    End Sub

    Public Sub addskills()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting skills for Character: " & Main.MainInstance.char_name & vbNewLine)
        For Each skill As String In Main.MainInstance.character_skills_list
            Dim skillstring As String =
                    runfunction.runcommand("SELECT `skills` FROM characters WHERE `guid`='" & Main.MainInstance.coreguid & "'",
                                           "skills")
            runfunction.normalsqlcommand(
                "UPDATE characters SET skills='" & skillstring & splitlist(skill, "skill") & ";" &
                splitlist(skill, "value") & ";" & splitlist(skill, "max") & ";' WHERE guid='" & Main.MainInstance.coreguid & "'")
            ' "<skill>" & skill & "</skill><value>" & value & "</value><max>" & max & "</max>"
        Next
    End Sub

    Public Sub addspells()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Teaching spells for Character: " & Main.MainInstance.char_name & vbNewLine)
        For Each spell As String In Main.MainInstance.character_spells
            'runfunction.normalsqlcommand("INSERT INTO character_spell ( GUID, SpellID ) VALUES ( '" & Main.MainInstance.coreguid & "', '" & splitlist(spell, "spell") & "' )")
            Dim spellstring As String =
                    runfunction.runcommand("SELECT `spells` FROM characters WHERE `guid`='" & Main.MainInstance.coreguid & "'",
                                           "spells")
            runfunction.normalsqlcommand(
                "UPDATE characters SET spells='" & spellstring & splitlist(spell, "spell") & ",' WHERE guid='" &
                Main.MainInstance.coreguid & "'")
            ' "<spell>" & spell & "</spell><active>" & active & "</active><disabled>" & disabled & "</disabled>"
        Next
    End Sub

    Public Sub addreputation()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding reputation for Character: " & Main.MainInstance.char_name & vbNewLine)
        For Each repstring As String In Main.MainInstance.character_reputatuion_list
            '  runfunction.normalsqlcommand("INSERT INTO playerreputations ( guid, faction, `standing`, `flag` ) VALUES ( '" & Main.MainInstance.coreguid & "', '" & splitlist(repstring, "faction") & "', '" & splitlist(repstring, "standing") & "', '" & splitlist(repstring, "flags") & "')")
            Dim selectrepstring As String =
                    runfunction.runcommand("SELECT `reputation` FROM characters WHERE `guid`='" & Main.MainInstance.coreguid & "'",
                                           "reputation")
            runfunction.normalsqlcommand(
                "UPDATE characters SET reputation='" & selectrepstring & splitlist(repstring, "faction") & "," &
                splitlist(repstring, "flags") & ",0," &
                splitlist(repstring, "standing") & ",' WHERE guid='" & Main.MainInstance.coreguid & "'")
            ' "<faction>" & faction & "</faction><standing>" & standing & "</standing><flags>" & flags & "</flags>"
        Next
    End Sub

    Public Sub addaction()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting up actionbars for Character: " & Main.MainInstance.char_name & vbNewLine)
        For Each actionstring As String In Main.MainInstance.character_action_list
            If splitlist(actionstring, "spec") = "0" Then
                Dim selectactionstring As String =
                        runfunction.runcommand("SELECT `actions1` FROM characters WHERE `guid`='" & Main.MainInstance.coreguid & "'",
                                               "actions1")
                runfunction.normalsqlcommand(
                    "UPDATE characters SET actions1='" & selectactionstring & splitlist(actionstring, "action") &
                    ",0,0,' WHERE guid='" & Main.MainInstance.coreguid & "'")
            Else
                Dim selectactionstring As String =
                        runfunction.runcommand("SELECT `actions2` FROM characters WHERE `guid`='" & Main.MainInstance.coreguid & "'",
                                               "actions2")
                runfunction.normalsqlcommand(
                    "UPDATE characters SET actions2='" & selectactionstring & splitlist(actionstring, "action") &
                    ",0,0,' WHERE guid='" & Main.MainInstance.coreguid & "'")
            End If
        Next
    End Sub

    Public Sub addinventory()
        'only arcemu
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding Items to inventory for Character: " & Main.MainInstance.char_name & vbNewLine)
        Dim bagexist As List(Of String) = New List(Of String)
        Dim bagstring As String = ""
        bagexist.Clear()
        For Each inventorystring As String In Main.MainInstance.character_inventoryzero_list
            Dim bagguid As String = splitlist(inventorystring, "bagguid")
            Dim bag As String = splitlist(inventorystring, "bag")
            Dim newguid As String =
                    ((CInt(
                        Val(
                            runfunction.runcommand(
                                "SELECT guid FROM playeritems WHERE guid=(SELECT MAX(guid) FROM playeritems)", "guid")))) +
                     1).ToString
            Dim itemcount As String = splitlist(inventorystring, "count")
            Dim containerslot As String = splitlist(inventorystring, "container")
            containerslot = "-1"                                                                                                       '// should always be -1!?
            Dim slotcase As String = splitlist(inventorystring, "slot")
            Select Case slotcase
                Case "19", "20", "21", "22", "67", "68", "69", "70", "71", "72", "73"
                    'Item is a bag and has to be registered
                    bagstring = bagstring & "oldguid:" & splitlist(inventorystring, "oldguid") & ";slot:" & slotcase & ";"
                Case Else : End Select
            runfunction.normalsqlcommand(
                "INSERT INTO playeritems ( guid, ownerguid, entry, `count`, containerslot, slot, enchantments ) VALUES ( '" &
                newguid & "', '" & Main.MainInstance.coreguid & "', '" & splitlist(inventorystring, "item") & "', '" & itemcount &
                "', '" & containerslot & "', '" & splitlist(inventorystring, "slot") & "', '" &
                splitenchstring(splitlist(inventorystring, "enchant"), newguid, splitlist(inventorystring, "item")) &
                "' )")


        Next
        For Each inventorystring As String In Main.MainInstance.character_inventory_list
            Dim bagguid As String = splitlist(inventorystring, "bagguid")
            Dim bag As String = splitlist(inventorystring, "bag")
            Dim newguid As String =
                    ((CInt(
                        Val(
                            runfunction.runcommand(
                                "SELECT guid FROM playeritems WHERE guid=(SELECT MAX(guid) FROM playeritems)", "guid")))) +
                     1).ToString
            Dim itemcount As String = splitlist(inventorystring, "count")
            Dim containerslot As String = splitlist(inventorystring, "container")
            Dim newbagguid As String =
                    runfunction.runcommand(
                        "SELECT item FROM playeritems WHERE item_template='" & bag & "' AND guid='" & Main.MainInstance.coreguid &
                        "'", "item")
            If containerslot = "" Then
                Select Case splitlist(inventorystring, "slot")
                    Case "19", "20", "21", "22", "67", "68", "69", "70", "71", "72", "73"
                    Case Else
                        Dim beginsplit As String = "oldguid:" & bagguid & ";slot:"
                        Dim endsplit As String = ";"
                        containerslot = Split(bagstring, beginsplit, 5)(1)
                        containerslot = Split(containerslot, endsplit, 6)(0)
                End Select
            End If
            runfunction.normalsqlcommand(
                "INSERT INTO playeritems ( guid, ownerguid, entry, `count`, containerslot, slot, enchantments ) VALUES ( '" &
                newguid & "', '" & Main.MainInstance.coreguid & "', '" & splitlist(inventorystring, "item") & "', '" & itemcount &
                "', '" & containerslot & "', '" & splitlist(inventorystring, "slot") & "', '" &
                splitenchstring(splitlist(inventorystring, "enchant"), newguid, splitlist(inventorystring, "item")) &
                "' )")


            ' <slot>" & tmpext.ToString & "</slot><bag>" & bag & "</bag><item>" & entryid & "</item><enchant>" & enchantments & "</enchant><count>" & itemcount & "</count><container></container>"
        Next
    End Sub

    Private Function splitenchstring(ByVal enchstring As String, ByVal guid As String, ByVal entry As String) As String
        Dim anzahl As Integer = 0
        Try
            anzahl = UBound(enchstring.Split(CChar(" ")))
        Catch ex As Exception

        End Try

        If enchstring.Contains(",") Then
            'ARCEMU
            Return enchstring
        Else
            'Trinity/Mangos
            If anzahl > 45 Then
                'mangos
                Dim arcenchstring As String = ""
                Dim input As String = enchstring
                Dim parts() As String = input.Split(" "c)
                If Not parts(22) = "0" Then arcenchstring = arcenchstring & parts(22) & ",0,5;"
                If Not parts(28) = "0" Then arcenchstring = arcenchstring & parts(28) & ",0,2;"
                If Not parts(31) = "0" Then arcenchstring = arcenchstring & parts(31) & ",0,3;"
                If Not parts(34) = "0" Then arcenchstring = arcenchstring & parts(34) & ",0,4;"
                Return arcenchstring

            ElseIf enchstring = "" Then
                Return ""
            Else

                'trinity
                Dim arcenchstring As String = ""
                Dim input As String = enchstring
                Dim parts() As String = input.Split(" "c)
                If Not parts(0) = "0" Then arcenchstring = arcenchstring & parts(0) & ",0,5;"
                If Not parts(6) = "0" Then arcenchstring = arcenchstring & parts(6) & ",0,2;"
                If Not parts(9) = "0" Then arcenchstring = arcenchstring & parts(9) & ",0,3;"
                If Not parts(12) = "0" Then arcenchstring = arcenchstring & parts(12) & ",0,4;"
                Return arcenchstring
            End If
        End If
    End Function

    Private Function splitlist(ByVal tstring As String, ByVal category As String) As String
        Try

            Dim xXquellcodeyx88 As String = tstring
            Dim xXanfangyx88 As String = "<" & category & ">"
            Dim xXendeyx88 As String = "</" & category & ">"
            Dim xXquellcodeSplityx88 As String
            xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
            xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
            Return xXquellcodeSplityx88
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub addench()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding item enchantments..." & vbNewLine)
        Application.DoEvents()
        If Not Main.MainInstance.kopfench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.kopfench, kopfwearguid.ToString, Main.MainInstance.kopfid.ToString) & "' WHERE guid='" &
                kopfwearguid.ToString & "'")
        If Not Main.MainInstance.halsench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.halsench, halswearguid.ToString, Main.MainInstance.halsid.ToString) & "' WHERE guid='" &
                halswearguid.ToString & "'")
        If Not Main.MainInstance.schulterench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.schulterench, schulterwearguid.ToString, Main.MainInstance.schulterid.ToString) &
                "' WHERE guid='" & schulterwearguid.ToString & "'")
        If Not Main.MainInstance.rueckenench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.rueckenench, rueckenwearguid.ToString, Main.MainInstance.rueckenid.ToString) & "' WHERE guid='" &
                rueckenwearguid.ToString & "'")
        If Not Main.MainInstance.brustench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.brustench, brustwearguid.ToString, Main.MainInstance.brustid.ToString) & "' WHERE guid='" &
                brustwearguid.ToString & "'")
        If Not Main.MainInstance.hemdench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.hemdench, hemdwearguid.ToString, Main.MainInstance.hemdid.ToString) & "' WHERE guid='" &
                hemdwearguid.ToString & "'")
        If Not Main.MainInstance.wappenrockench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.wappenrockench, wappenrockwearguid.ToString, Main.MainInstance.wappenrockid.ToString) &
                "' WHERE guid='" & wappenrockwearguid.ToString & "'")
        If Not Main.MainInstance.handgelenkeench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.handgelenkeench, handgelenkewearguid.ToString, Main.MainInstance.handgelenkeid.ToString) &
                "' WHERE guid='" & handgelenkewearguid.ToString & "'")
        If Not Main.MainInstance.haendeench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.haendeench, haendewearguid.ToString, Main.MainInstance.haendeid.ToString) & "' WHERE guid='" &
                haendewearguid.ToString & "'")
        If Not Main.MainInstance.hauptench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.hauptench, hauptwearguid.ToString, Main.MainInstance.hauptid.ToString) & "' WHERE guid='" &
                hauptwearguid.ToString & "'")
        If Not Main.MainInstance.offench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.offench, offwearguid.ToString, Main.MainInstance.offid.ToString) & "' WHERE guid='" &
                offwearguid.ToString & "'")
        If Not Main.MainInstance.distanzench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.distanzench, distanzwearguid.ToString, Main.MainInstance.distanzid.ToString) & "' WHERE guid='" &
                distanzwearguid.ToString & "'")
        If Not Main.MainInstance.guertelench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.guertelench, guertelwearguid.ToString, Main.MainInstance.guertelid.ToString) & "' WHERE guid='" &
                guertelwearguid.ToString & "'")
        If Not Main.MainInstance.beineench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.beineench, beinewearguid.ToString, Main.MainInstance.beineid.ToString) & "' WHERE guid='" &
                beinewearguid.ToString & "'")
        If Not Main.MainInstance.stiefelench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.stiefelench, stiefelwearguid.ToString, Main.MainInstance.stiefelid.ToString) & "' WHERE guid='" &
                stiefelwearguid.ToString & "'")
        If Not Main.MainInstance.ring1ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.ring1ench, ring1wearguid.ToString, Main.MainInstance.ring1id.ToString) & "' WHERE guid='" &
                ring1wearguid.ToString & "'")
        If Not Main.MainInstance.ring2ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.ring2ench, ring2wearguid.ToString, Main.MainInstance.ring2id.ToString) & "' WHERE guid='" &
                ring2wearguid.ToString & "'")
        If Not Main.MainInstance.schmuck1ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.schmuck1ench, schmuck1wearguid.ToString, Main.MainInstance.schmuck1id.ToString) &
                "' WHERE guid='" & schmuck1wearguid.ToString & "'")
        If Not Main.MainInstance.schmuck2ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                splitenchstring(Main.MainInstance.schmuck2ench, schmuck2wearguid.ToString, Main.MainInstance.schmuck2id.ToString) &
                "' WHERE guid='" & schmuck2wearguid.ToString & "'")
    End Sub

    Public Sub addgems()
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
            beltinsert("3729", guertelwearguid.ToString, 41)
            '41 may be wrong!...
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
            beltinsert("3729", guertelwearguid.ToString, 41)
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
            beltinsert("3729", guertelwearguid.ToString, 41)
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
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding character enchantments..." & vbNewLine)
        If Main.MainInstance.kopfvzid > 0 Then vzinsert(Main.MainInstance.kopfvzid, kopfwearguid, 1)
        If Main.MainInstance.halsvzid > 0 Then vzinsert(Main.MainInstance.halsvzid, halswearguid, 1)
        If Main.MainInstance.schultervzid > 0 Then vzinsert(Main.MainInstance.schultervzid, schulterwearguid, 1)
        If Main.MainInstance.rueckenvzid > 0 Then vzinsert(Main.MainInstance.rueckenvzid, rueckenwearguid, 1)
        If Main.MainInstance.brustvzid > 0 Then vzinsert(Main.MainInstance.brustvzid, brustwearguid, 1)
        If Main.MainInstance.handgelenkevzid > 0 Then vzinsert(Main.MainInstance.handgelenkevzid, handgelenkewearguid, 1)
        If Main.MainInstance.haendevzid > 0 Then vzinsert(Main.MainInstance.haendevzid, haendewearguid, 1)
        If Main.MainInstance.guertelvzid > 0 Then vzinsert(Main.MainInstance.guertelvzid, guertelwearguid, 1)
        If Main.MainInstance.beinevzid > 0 Then vzinsert(Main.MainInstance.beinevzid, beinewearguid, 1)
        If Main.MainInstance.stiefelvzid > 0 Then vzinsert(Main.MainInstance.stiefelvzid, stiefelwearguid, 1)
        If Main.MainInstance.ring1vzid > 0 Then vzinsert(Main.MainInstance.ring1vzid, ring1wearguid, 1)
        If Main.MainInstance.ring2vzid > 0 Then vzinsert(Main.MainInstance.ring2vzid, ring2wearguid, 1)
        If Main.MainInstance.schmuck1vzid > 0 Then vzinsert(Main.MainInstance.schmuck1vzid, schmuck1wearguid, 1)
        If Main.MainInstance.schmuck2vzid > 0 Then vzinsert(Main.MainInstance.schmuck2vzid, schmuck2wearguid, 1)
        If Main.MainInstance.hauptvzid > 0 Then vzinsert(Main.MainInstance.hauptvzid, hauptwearguid, 1)
        If Main.MainInstance.offvzid > 0 Then vzinsert(Main.MainInstance.offvzid, offwearguid, 1)
        If Main.MainInstance.distanzvzid > 0 Then vzinsert(Main.MainInstance.distanzvzid, distanzwearguid, 1)
    End Sub

    Public Sub addpvp()

        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting character honor/kills..." & vbNewLine)

        runfunction.normalsqlcommand(
            "UPDATE `characters` SET arenaPoints='" & Main.MainInstance.arenaPoints.ToString & "' WHERE guid='" & Main.MainInstance.coreguid & "'")
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET honorPoints='" & Main.MainInstance.totalHonorPoints.ToString & "' WHERE guid='" & Main.MainInstance.coreguid &
            "'")
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET killsLifeTime='" & Main.MainInstance.totalKills.ToString & "' WHERE guid='" & Main.MainInstance.coreguid &
            "'")
    End Sub

    Public Sub socketinsert(ByVal socketid As String, ByVal itemguid As String, ByVal position As Integer)

        Dim xvalue As String = ""
        If position = 29 Then
            xvalue = "0,1"
        ElseIf position = 32 Then
            xvalue = "0,2"
        ElseIf position = 35 Then
            xvalue = "0,3"
        Else
            xvalue = "0,1"
        End If

        Try
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" & runfunction.getgemeffectid(socketid).ToString & "," & xvalue &
                ",' WHERE guid = '" & itemguid & "'")
        Catch ex As Exception

        End Try
    End Sub

    Public Sub beltinsert(ByVal beltid As String, ByVal itemguid As String, ByVal position As Integer)
        'TO DO

        'Dim enchantmenttext As String
        'Try
        '    enchantmenttext = runfunction.runcommand("SELECT enchantments FROM item_instance WHERE guid='" & itemguid & "'", "data")
        '    Dim input As String = enchantmenttext
        '    Dim parts() As String = input.Split(" "c)
        '    Dim output As String
        '    parts(position) = beltid
        '    output = String.Join(" ", parts)
        '    Try
        '        runfunction.normalsqlcommand("UPDATE `item_instance` SET data='" & output & "' WHERE guid = '" & itemguid & "'")
        '    Catch ex As Exception

        '    End Try
        'Catch ex As Exception

        'End Try
    End Sub

    Public Sub vzinsert(ByVal vzid As Integer, ByVal itemguid As Integer, ByVal position As Integer)


        Try
            runfunction.normalsqlcommand(
                "UPDATE `playeritems` SET enchantments='" &
                runfunction.getvzeffectid(runfunction.getvzeffectname(vzid)).ToString & ",0,5' WHERE guid = '" &
                itemguid.ToString & "'")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub checkexist_anddelete(ByVal xguid As String, ByVal xslot As String, ByVal item As String,
                                     ByVal itementry As Integer)
        '"SELECT `columname` FROM `tabelle`"

        Try

            '  ("SELECT * FROM characters WHERE name = '" & charname.Text & "'")
            Dim myAdapter As New MySqlDataAdapter

            Dim sqlquery = "SELECT * FROM playeritems WHERE guid = '" & xguid & "' AND ownerguid='" & Main.MainInstance.coreguid &
                           "' AND slot = '" & xslot & "' AND containerslot='-1'"
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
                    "INSERT INTO playeritems ( guid, ownerguid, entry, containerslot, slot) VALUES ( '" & lastnumber &
                    "', '" & guid & "', '" & itementry.ToString & "', '-1', '" & xslot & "' )")

            Else
                myData.Close()
                runfunction.normalsqlcommand(
                    "DELETE FROM playeritems WHERE guid = '" & xguid & "' AND slot = '" & xslot & "'", False)
                runfunction.normalsqlcommand(
                    "INSERT INTO playeritems ( guid, ownerguid, entry, containerslot, slot) VALUES ( '" & lastnumber &
                    "', '" & guid & "', '" & itementry.ToString & "', '-1', '" & xslot & "' )")
            End If

        Catch

        End Try
    End Sub

    Private Sub checkglyphsanddelete(ByVal playerguid As String)

        Try
            runfunction.normalsqlcommand("DELETE glyphs1 FROM characters WHERE guid = '" & playerguid & "'", False)
        Catch ex As Exception

        End Try

        Try
            runfunction.normalsqlcommand("DELETE glyphs2 FROM characters WHERE guid = '" & playerguid & "'", False)
        Catch ex As Exception

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
            '  runfunction.normalsqlcommand("UPDATE characters SET equipmentCache='" & finalstring & "' WHERE (guid='" & guid & "')")
        End If
    End Sub
End Class
