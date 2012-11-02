'****************************************************************************************
'****************************************************************************************
'***************************** CharImport- Trinity_core *********************************
'****************************************************************************************
'..................Status
'...................Code:       98%
'...................Design:     98%
'...................Functions:  95%
'****************************************************************************************
'****************************************************************************************
'..................Last modified: 20.04.12
'****************************************************************************************
'****************************************************************************************
'..................Comments:
Imports MySql.Data.MySqlClient
Imports System.Net

Public Class Trinity_core
    Dim runfunction As New Functions
    Dim core_check As New Core_Check_Trinity
    ' Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim characterguid As Integer
    Dim reporttext As RichTextBox = Process_Status.processreport
    '  Dim SQLConnection As MySqlConnection = New MySqlConnection
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
        Dim teststring = Main.ServerString
        Try
            Main.GLOBALconn.Close()
            Main.GLOBALconn.Dispose()
        Catch ex As Exception

        End Try
        Try
            Main.GLOBALconnRealmd.Close()
            Main.GLOBALconnRealmd.Dispose()
        Catch ex As Exception

        End Try
        Main.GLOBALconn.ConnectionString = Main.ServerString
        Main.GLOBALconnRealmd.ConnectionString = Main.ServerStringRealmd
        Try
            Main.GLOBALconn.Open()
        Catch ex As Exception
            runfunction.writelog("Failed to open SQL connection @trinity errmsg: " & ex.ToString)
            Main.GLOBALconn.Close()
            Main.GLOBALconn.Dispose()
        End Try
        Try
            Main.GLOBALconnRealmd.Open()
        Catch ex As Exception
            runfunction.writelog("Failed to open SQL connection Realmd @trinity errmsg: " & ex.ToString)
            Main.GLOBALconnRealmd.Close()
            Main.GLOBALconnRealmd.Dispose()
        End Try
    End Sub

    Public Sub closesql()
        runfunction.writelog("closesql_call @trinity")
        Try
            Main.GLOBALconn.Close()
            Main.GLOBALconn.Dispose()
        Catch ex As Exception

        End Try
        Try
            Main.GLOBALconnRealmd.Close()
            Main.GLOBALconnRealmd.Dispose()
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

    Private Function getidswithgmlevel(ByVal gmlevel As String) As List(Of String)
        Dim _
            da As _
                New MySqlDataAdapter("SELECT `id` FROM `account_access` WHERE gmlevel='" & gmlevel & "'",
                                     Main.GLOBALconnRealmd)
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
        If My.Settings.lastloginactive = True Or My.Settings.gmlevelactive = False Then
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
                                        "' AND `id`='" & accid & "'", Main.GLOBALconnRealmd)
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
                                "' AND last_login<='" & My.Settings.lastlogindate2 & "'", Main.GLOBALconnRealmd)
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
                                Main.GLOBALconnRealmd)
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
                            "' AND level<='" & My.Settings.levelrangemax & "'", Main.GLOBALconn)
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
                Dim da As New MySqlDataAdapter("SELECT guid FROM characters", Main.GLOBALconn)
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


        Dim da As New MySqlDataAdapter("SELECT guid FROM characters", Main.GLOBALconn)
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
        Dim adljah As String = Main.GLOBALconn.ConnectionString
        Dim statussda As String = Main.GLOBALconn.State.ToString

        'Dim conn As New MySqlConnection(Main.ServerString)
        Dim da As New MySqlDataAdapter("SELECT guid FROM characters WHERE name='" & charname & "'", Main.GLOBALconn)
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


        Dim da As New MySqlDataAdapter("SELECT guid FROM characters WHERE `account`='" & accid & "'", Main.GLOBALconn)
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

    Public Sub GetCharFromDatabase(ByVal charguid As String)
        '****************************************************************************************
        '****************************************************************************************
        'Get Character Guid


        '****************************************************************************************
        '****************************************************************************************
        'Get Main Character atributes

        'Character Race
        runfunction.writelog("GetCharFromDatabase_call with charguid: " & charguid)
        Main.setallempty()
        Main.anzahldurchlaufe += 1
        Main.char_guid = CInt(Val(charguid))
        characterguid = CInt(Val(charguid))

        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Race from Database..." & vbNewLine)
        Application.DoEvents()
        Try
            Main.char_race = CInt(Val(runfunction.runcommand("SELECT race FROM characters WHERE guid='" & charguid & "'",
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
            Main.char_class =
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
            Main.char_gender =
                CInt(Val(runfunction.runcommand("SELECT gender FROM characters WHERE guid='" & charguid & "'", "gender")))
        Catch ex As Exception

        End Try

        'Character level
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Level from Database..." & vbNewLine)
        Application.DoEvents()
        Try
            Main.char_level =
                CInt(Val(runfunction.runcommand("SELECT level FROM characters WHERE guid='" & charguid & "'", "level")))

        Catch ex As Exception

        End Try
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "/ Loading Character Table..." & vbNewLine)
        Application.DoEvents()

        Main.char_name = runfunction.runcommand("SELECT name FROM characters WHERE guid='" & charguid & "'", "name")

        Main.accountid = CInt(Val(runfunction.runcommand("SELECT account FROM characters WHERE guid='" & charguid & "'",
                                                         "account")))
        Main.char_xp = CInt(Val(runfunction.runcommand("SELECT xp FROM characters WHERE guid='" & charguid & "'", "xp")))
        Main.player_money = CInt(Val(runfunction.runcommand("SELECT money FROM characters WHERE guid='" & charguid & "'",
                                                            "money")))
        Main.playerBytes =
            CInt(Val(runfunction.runcommand("SELECT playerBytes FROM characters WHERE guid='" & charguid & "'",
                                            "playerBytes")))
        Main.playerBytes2 =
            CInt(Val(runfunction.runcommand("SELECT playerBytes2 FROM characters WHERE guid='" & charguid & "'",
                                            "playerBytes2")))
        Main.playerFlags =
            CInt(Val(runfunction.runcommand("SELECT playerFlags FROM characters WHERE guid='" & charguid & "'",
                                            "playerFlags")))
        Main.position_x = runfunction.runcommand("SELECT position_x FROM characters WHERE guid='" & charguid & "'",
                                                 "position_x")
        Main.position_y = runfunction.runcommand("SELECT position_y FROM characters WHERE guid='" & charguid & "'",
                                                 "position_y")
        Main.position_z = runfunction.runcommand("SELECT position_z FROM characters WHERE guid='" & charguid & "'",
                                                 "position_z")
        Main.map = CInt(Val(runfunction.runcommand("SELECT map FROM characters WHERE guid='" & charguid & "'", "map")))
        Main.instance_id =
            CInt(Val(runfunction.runcommand("SELECT instance_id FROM characters WHERE guid='" & charguid & "'",
                                            "instance_id")))
        Main.instance_mode_mask =
            runfunction.runcommand("SELECT instance_mode_mask FROM characters WHERE guid='" & charguid & "'",
                                   "instance_mode_mask")
        Main.orientation = runfunction.runcommand("SELECT orientation FROM characters WHERE guid='" & charguid & "'",
                                                  "orientation")
        Main.taximask = runfunction.runcommand("SELECT taximask FROM characters WHERE guid='" & charguid & "'",
                                               "taximask")
        Main.cinematic =
            CInt(Val(runfunction.runcommand("SELECT cinematic FROM characters WHERE guid='" & charguid & "'",
                                            "cinematic")))
        Main.totaltime =
            CInt(Val(runfunction.runcommand("SELECT totaltime FROM characters WHERE guid='" & charguid & "'",
                                            "totaltime")))
        Main.leveltime =
            CInt(Val(runfunction.runcommand("SELECT leveltime FROM characters WHERE guid='" & charguid & "'",
                                            "leveltime")))
        Main.extra_flags = runfunction.runcommand("SELECT extra_flags FROM characters WHERE guid='" & charguid & "'",
                                                  "extra_flags")
        Main.stable_slots = runfunction.runcommand("SELECT stable_slots FROM characters WHERE guid='" & charguid & "'",
                                                   "stable_slots")
        Main.at_login = runfunction.runcommand("SELECT at_login FROM characters WHERE guid='" & charguid & "'",
                                               "at_login")
        Main.zone = CInt(Val(runfunction.runcommand("SELECT zone FROM characters WHERE guid='" & charguid & "'", "zone")))
        Main.arenaPoints =
            CInt(Val(runfunction.runcommand("SELECT arenaPoints FROM characters WHERE guid='" & charguid & "'",
                                            "arenaPoints")))
        Main.totalHonorPoints =
            CInt(Val(runfunction.runcommand("SELECT totalHonorPoints FROM characters WHERE guid='" & charguid & "'",
                                            "totalHonorPoints")))
        Main.totalKills =
            CInt(Val(runfunction.runcommand("SELECT totalKills FROM characters WHERE guid='" & charguid & "'",
                                            "totalKills")))
        Main.chosenTitle = runfunction.runcommand("SELECT chosenTitle FROM characters WHERE guid='" & charguid & "'",
                                                  "chosenTitle")
        Main.knownCurrencies =
            runfunction.runcommand("SELECT knownCurrencies FROM characters WHERE guid='" & charguid & "'",
                                   "knownCurrencies")
        Main.watchedFaction =
            runfunction.runcommand("SELECT watchedFaction FROM characters WHERE guid='" & charguid & "'",
                                   "watchedFaction")
        Main.health = CInt(Val(runfunction.runcommand("SELECT health FROM characters WHERE guid='" & charguid & "'",
                                                      "health")))
        Main.speccount =
            CInt(Val(runfunction.runcommand("SELECT speccount FROM characters WHERE guid='" & charguid & "'",
                                            "speccount")))
        Main.activespec =
            CInt(Val(runfunction.runcommand("SELECT activespec FROM characters WHERE guid='" & charguid & "'",
                                            "activespec")))
        Main.exploredZones = runfunction.runcommand("SELECT exploredZones FROM characters WHERE guid='" & charguid & "'",
                                                    "exploredZones")
        Main.knownTitles = runfunction.runcommand("SELECT knownTitles FROM characters WHERE guid='" & charguid & "'",
                                                  "knownTitles")
        Main.actionBars = runfunction.runcommand("SELECT actionBars FROM characters WHERE guid='" & charguid & "'",
                                                 "actionBars")

        Main.accountname =
            runfunction.runcommandRealmd("SELECT username FROM account WHERE `id`='" & Main.accountid.ToString & "'",
                                         "username")
        Main.sha_pass_hash =
            runfunction.runcommandRealmd(
                "SELECT sha_pass_hash FROM account WHERE `id`='" & Main.accountid.ToString & "'", "sha_pass_hash")
        Main.sessionkey =
            runfunction.runcommandRealmd("SELECT sessionkey FROM account WHERE `id`='" & Main.accountid.ToString & "'",
                                         "sessionkey")
        Main.account_v =
            runfunction.runcommandRealmd("SELECT v FROM account WHERE `id`='" & Main.accountid.ToString & "'", "v")
        Main.account_s =
            runfunction.runcommandRealmd("SELECT s FROM account WHERE `id`='" & Main.accountid.ToString & "'", "s")
        Main.email =
            runfunction.runcommandRealmd("SELECT email FROM account WHERE `id`='" & Main.accountid.ToString & "'",
                                         "email")
        Main.joindate =
            runfunction.runcommandRealmd("SELECT joindate FROM account WHERE `id`='" & Main.accountid.ToString & "'",
                                         "joindate")
        Main.expansion =
            CInt(
                Val(
                    runfunction.runcommandRealmd(
                        "SELECT expansion FROM account WHERE `id`='" & Main.accountid.ToString & "'", "expansion")))
        Main.locale =
            CInt(
                Val(
                    runfunction.runcommandRealmd(
                        "SELECT locale FROM account WHERE `id`='" & Main.accountid.ToString & "'", "locale")))
        Main.account_access_gmlevel =
            CInt(
                Val(
                    runfunction.runcommandRealmd(
                        "SELECT gmlevel FROM account_access WHERE `id`='" & Main.accountid.ToString & "'", "gmlevel")))
        Main.account_access_RealmID =
            CInt(
                Val(
                    runfunction.runcommandRealmd(
                        "SELECT RealmID FROM account_access WHERE `id`='" & Main.accountid.ToString & "'", "RealmID")))
        Main.level.Text = Main.char_name & ", " & Main.char_level & ", " & Main.char_race & ", " & Main.char_class
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Homebind from Database..." & vbNewLine)
        Main.character_homebind =
            ("<map>" &
             runfunction.runcommand(
                 "SELECT " & Main.homebind_map & " FROM character_homebind WHERE guid='" & Main.char_guid.ToString & "'",
                 Main.homebind_map) & "</map><zone>" &
             runfunction.runcommand(
                 "SELECT " & Main.homebind_zone & " FROM character_homebind WHERE guid='" & Main.char_guid.ToString &
                 "'", Main.homebind_zone) & "</zone><position_x>" &
             runfunction.runcommand(
                 "SELECT " & Main.homebind_posx & " FROM character_homebind WHERE guid='" & Main.char_guid.ToString &
                 "'", Main.homebind_posx) & "</position_x><position_y>" &
             runfunction.runcommand(
                 "SELECT " & Main.homebind_posy & " FROM character_homebind WHERE guid='" & Main.char_guid.ToString &
                 "'", Main.homebind_posy) & "</position_y><position_z>" &
             runfunction.runcommand(
                 "SELECT " & Main.homebind_posz & " FROM character_homebind WHERE guid='" & Main.char_guid.ToString &
                 "'", Main.homebind_posz) & "</position_z>")
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Spells from Database..." & vbNewLine)
        Application.DoEvents()
        getspells()
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Talents from Database..." & vbNewLine)
        Application.DoEvents()
        gettalents()
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Skills from Database..." & vbNewLine)
        Application.DoEvents()
        getskills()
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Reputation from Database..." & vbNewLine)
        Application.DoEvents()
        getREPlists()
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Action from Database..." & vbNewLine)
        Application.DoEvents()
        getactionlist()
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Achievements from Database..." & vbNewLine)
        Application.DoEvents()
        getavlists()
        Process_Status.processreport.AppendText(
           Now.TimeOfDay.ToString & "/ Loading Character Questlog from Database..." & vbNewLine)
        Application.DoEvents()
        getqueststatus()
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Inventory from Database..." & vbNewLine)
        Application.DoEvents()
        getinventoryitems()

        'GET ITEMS
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Items from Database..." & vbNewLine)
        Application.DoEvents()
        getitems()


        'GET GLYPHS
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Primary Glyphs from Database..." & vbNewLine)
        Application.DoEvents()
        getglyphs()
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "/ Loading Character Secondary Glyphs from Database..." & vbNewLine)
        Application.DoEvents()
        getsecglyphs()
        handleenchantments()
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "/ Character loaded!..." & vbNewLine)
        Application.DoEvents()

        saveglyphs()
        Main.datasets += 1
        Dim addtataset As New CIUFile
        addtataset.adddataset()

        Application.DoEvents()
    End Sub

    Public Sub getspells()
        runfunction.writelog("getspells_call @trinity")
        Dim _
            da As _
                New MySqlDataAdapter("SELECT spell FROM character_spell WHERE guid='" & Main.char_guid.ToString & "'",
                                     Main.GLOBALconn)
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
                                Main.char_guid.ToString & "'", "`active`")
                    Dim disabled As String =
                            runfunction.runcommand(
                                "SELECT `disabled` FROM character_spell WHERE spell='" & spell & "' AND guid='" &
                                Main.char_guid.ToString & "'", "`disabled`")

                    Main.character_spells.Add(
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
                    "SELECT spell FROM character_talent WHERE guid='" & Main.char_guid.ToString & "' AND spec='0'",
                    Main.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim spell As String = readedcode

                    Main.character_talent_list.Add("<spell>" & spell & "</spell><spec>0</spec>")

                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try

        getspec1()
    End Sub

    Private Sub getspec1()
        runfunction.writelog("getspec1_call (talents) @trinity")
        ' Dim conn As New MySqlConnection(Main.ServerString)
        Dim _
            da As _
                New MySqlDataAdapter(
                    "SELECT spell FROM character_talent WHERE guid='" & Main.char_guid.ToString & "' AND spec='1'",
                    Main.GLOBALconn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim spell As String = readedcode

                    Main.character_talent_list.Add("<spell>" & spell & "</spell><spec>1</spec>")

                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getqueststatus()
        runfunction.writelog("getqueststatus_call @trinity")
        Dim _
            da As _
                New MySqlDataAdapter(
                    "SELECT quest FROM character_queststatus WHERE guid='" & Main.char_guid.ToString & "'",
                    Main.GLOBALconn)
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
                                Main.char_guid.ToString & "'", "status")
                    Dim explored As String =
                            runfunction.runcommand(
                                "SELECT `explored` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" &
                                Main.char_guid.ToString & "'", "explored")
                    Dim timer As String =
                            runfunction.runcommand(
                                "SELECT `timer` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" &
                                Main.char_guid.ToString & "'", "timer")
                    Main.character_queststatus.Add(
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
                    "SELECT quest FROM character_queststatus_rewarded WHERE guid='" & Main.char_guid.ToString & "'",
                    Main.GLOBALconn)
        Dim dt2 As New DataTable
        Try
            da2.Fill(dt2)

            Dim lastcount As Integer = CInt(Val(dt2.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt2.Rows(count).Item(0)).ToString
                    Dim quest As String = readedcode
                    If Not quest = "" Then Main.finished_quests = Main.finished_quests & quest & ","
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
                New MySqlDataAdapter("SELECT skill FROM character_skills WHERE guid='" & Main.char_guid.ToString & "'",
                                     Main.GLOBALconn)
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
                                Main.char_guid.ToString & "'", "value")
                    Dim max As String =
                            runfunction.runcommand(
                                "SELECT max FROM character_skills WHERE skill='" & skill & "' AND guid='" &
                                Main.char_guid.ToString & "'", "max")

                    Main.character_skills_list.Add(
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
                    "SELECT faction FROM character_reputation WHERE guid='" & Main.char_guid.ToString & "'",
                    Main.GLOBALconn)
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
                                Main.char_guid.ToString & "'", "standing")
                    Dim flags As String =
                            runfunction.runcommand(
                                "SELECT flags FROM character_reputation WHERE faction='" & faction & "' AND guid='" &
                                Main.char_guid.ToString & "'", "flags")

                    Main.character_reputatuion_list.Add(
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
                New MySqlDataAdapter("SELECT button FROM character_action WHERE guid='" & Main.char_guid.ToString & "'",
                                     Main.GLOBALconn)
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
                                Main.char_guid.ToString & "'", "spec")
                    Dim action As String =
                            runfunction.runcommand(
                                "SELECT action FROM character_action WHERE button='" & gbutton & "' AND guid='" &
                                Main.char_guid.ToString & "'", "action")
                    Dim atype As String =
                            runfunction.runcommand(
                                "SELECT type FROM character_action WHERE button='" & gbutton & "' AND guid='" &
                                Main.char_guid.ToString & "'", "type")

                    Main.character_action_list.Add(
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
                    "SELECT achievement FROM character_achievement WHERE guid='" & Main.char_guid.ToString & "'",
                    Main.GLOBALconn)
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
                                Main.char_guid.ToString & "'", "date")
                    Main.character_achievement_list.Add("<av>" & avid & "</av><date>" & xdate & "</date>")

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
                New MySqlDataAdapter("SELECT item FROM character_inventory WHERE guid='" & Main.char_guid.ToString & "'",
                                     Main.GLOBALconn)
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
                                "SELECT bag FROM character_inventory WHERE guid='" & Main.char_guid.ToString &
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
                                runfunction.runcommand("SELECT itemEntry FROM item_instance WHERE guid = '" & item & "'", "itemEntry")
                            enchantments = runfunction.runcommand("SELECT enchantments FROM item_instance WHERE guid = '" & item & "'", "enchantments")
                            itemcount = runfunction.runcommand("Select `count` FROM item_instance WHERE guid='" & item & "'", "count")
                            slot = runfunction.runcommand("Select `slot` FROM character_inventory WHERE `item`='" & item & "'", "slot")
                            Main.character_inventoryzero_list.Add(
                                "<slot>" & slot & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid &
                                "</bagguid><item>" & entryid & "</item><enchant>" & enchantments & "</enchant><count>" & itemcount & "</count>")
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
                        itemcount = runfunction.runcommand("Select `count` FROM item_instance WHERE guid='" & item & "'", "count")
                        slot = runfunction.runcommand("Select `slot` FROM character_inventory WHERE `item`='" & item & "'", "slot")
                        Main.character_inventory_list.Add(
                            "<slot>" & slot & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid &
                            "</bagguid><item>" & entryid & "</item><enchant>" & enchantments & "</enchant><count>" & itemcount & "</count>")
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
                New MySqlDataAdapter("SELECT slot FROM character_inventory WHERE guid='" & Main.char_guid.ToString & "'",
                                     Main.GLOBALconn)
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
                                "SELECT bag FROM character_inventory WHERE guid='" & Main.char_guid.ToString &
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
                                    "SELECT item FROM character_inventory WHERE guid='" & Main.char_guid.ToString &
                                    "' AND slot='" & tmpext.ToString & "'", "item")
                            entryid =
                                runfunction.runcommand("SELECT itemEntry FROM item_instance WHERE guid = '" & item & "'",
                                                       "itemEntry")
                            enchantments =
                                runfunction.runcommand(
                                    "SELECT enchantments FROM item_instance WHERE guid = '" & item & "'", "enchantments")
                            Main.character_inventoryzero_list.Add(
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
                                "SELECT item FROM character_inventory WHERE guid='" & Main.char_guid.ToString &
                                "' AND slot='" & tmpext.ToString & "'", "item")
                        entryid =
                            runfunction.runcommand("SELECT itemEntry FROM item_instance WHERE guid = '" & item & "'",
                                                   "itemEntry")
                        enchantments =
                            runfunction.runcommand("SELECT enchantments FROM item_instance WHERE guid = '" & item & "'",
                                                   "enchantments")
                        Main.character_inventory_list.Add(
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.prim1.Text = glyphname
                Main.primeglyph1 = glyphid.ToString
                Glyphs.prim1.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.prim1pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.prim2.Text = glyphname
                Main.primeglyph2 = glyphid.ToString
                Glyphs.prim2.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.prim2pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.prim3.Text = glyphname
                Main.primeglyph3 = glyphid.ToString
                Glyphs.prim3.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.prim3pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.erheb1.Text = glyphname
                Main.majorglyph1 = glyphid.ToString
                Glyphs.erheb1.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.erheb1pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.erheb2.Text = glyphname
                Main.majorglyph2 = glyphid.ToString
                Glyphs.erheb2.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.erheb2pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.erheb3.Text = glyphname
                Main.majorglyph3 = glyphid.ToString
                Glyphs.erheb3.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.erheb3pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.gering1.Text = glyphname
                Main.minorglyph1 = glyphid.ToString
                Glyphs.gering1.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.gering1pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.gering2.Text = glyphname
                Main.minorglyph2 = glyphid.ToString
                Glyphs.gering2.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.gering2pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.gering3.Text = glyphname
                Main.minorglyph3 = glyphid.ToString
                Glyphs.gering3.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.gering3pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secprim1.Text = glyphname
                Main.secprimeglyph1 = glyphid.ToString
                Glyphs.secprim1.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secprim1pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secprim2.Text = glyphname
                Main.secprimeglyph2 = glyphid.ToString
                Glyphs.secprim2.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secprim2pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secprim3.Text = glyphname
                Main.secprimeglyph3 = glyphid.ToString
                Glyphs.secprim3.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secprim3pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secerheb1.Text = glyphname
                Main.secmajorglyph1 = glyphid.ToString
                Glyphs.secerheb1.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secerheb1pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secerheb2.Text = glyphname
                Main.secmajorglyph2 = glyphid.ToString
                Glyphs.secerheb2.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secerheb2pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secerheb3.Text = glyphname
                Main.secmajorglyph3 = glyphid.ToString
                Glyphs.secerheb3.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secerheb3pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secgering1.Text = glyphname
                Main.secminorglyph1 = glyphid.ToString
                Glyphs.secgering1.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secgering1pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secgering2.Text = glyphname
                Main.secminorglyph2 = glyphid.ToString
                Glyphs.secgering2.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secgering2pic)
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
                If Main.anzahldurchlaufe = 1 Then glyphname = getnamefromid(glyphid)
                Glyphs.secgering3.Text = glyphname
                Main.secminorglyph3 = glyphid.ToString
                Glyphs.secgering3.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secgering3pic)
            End If
        Catch ex As Exception
            Glyphs.secgering3pic.Image = My.Resources.empty
        End Try
    End Sub

    Private Sub saveglyphs()
        runfunction.writelog("saveglyphs_call @trinity")
        Main.textprimeglyph1 = Glyphs.prim1.Text
        Main.textprimeglyph2 = Glyphs.prim2.Text
        Main.textprimeglyph3 = Glyphs.prim3.Text
        Main.textmajorglyph1 = Glyphs.erheb1.Text
        Main.textmajorglyph2 = Glyphs.erheb2.Text
        Main.textmajorglyph3 = Glyphs.erheb3.Text
        Main.textminorglyph1 = Glyphs.gering1.Text
        Main.textminorglyph2 = Glyphs.gering2.Text
        Main.textminorglyph3 = Glyphs.gering3.Text

        Main.glyphpic1 = Glyphs.prim1pic.Image
        Main.glyphpic2 = Glyphs.prim2pic.Image
        Main.glyphpic3 = Glyphs.prim3pic.Image
        Main.glyphpic4 = Glyphs.erheb1pic.Image
        Main.glyphpic5 = Glyphs.erheb2pic.Image
        Main.glyphpic6 = Glyphs.erheb3pic.Image
        Main.glyphpic7 = Glyphs.gering1pic.Image
        Main.glyphpic8 = Glyphs.gering2pic.Image
        Main.glyphpic9 = Glyphs.gering3pic.Image

        Main.sectextprimeglyph1 = Glyphs.secprim1.Text
        Main.sectextprimeglyph2 = Glyphs.secprim2.Text
        Main.sectextprimeglyph3 = Glyphs.secprim3.Text
        Main.sectextmajorglyph1 = Glyphs.secerheb1.Text
        Main.sectextmajorglyph2 = Glyphs.secerheb2.Text
        Main.sectextmajorglyph3 = Glyphs.secerheb3.Text
        Main.sectextminorglyph1 = Glyphs.secgering1.Text
        Main.sectextminorglyph2 = Glyphs.secgering2.Text
        Main.sectextminorglyph3 = Glyphs.secgering3.Text

        Main.secglyphpic1 = Glyphs.secprim1pic.Image
        Main.secglyphpic2 = Glyphs.secprim2pic.Image
        Main.secglyphpic3 = Glyphs.secprim3pic.Image
        Main.secglyphpic4 = Glyphs.secerheb1pic.Image
        Main.secglyphpic5 = Glyphs.secerheb2pic.Image
        Main.secglyphpic6 = Glyphs.secerheb3pic.Image
        Main.secglyphpic7 = Glyphs.secgering1pic.Image
        Main.secglyphpic8 = Glyphs.secgering2pic.Image
        Main.secglyphpic9 = Glyphs.secgering3pic.Image
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
                If Main.anzahldurchlaufe = 1 Then itemname = getnamefromid(realxentryid)
                Dim wartemal As String = ""
                runfunction.writelog(
                    "getitems_call @trinity: /tag xentryid=" & xentryid.ToString & " realxentryid=" &
                    realxentryid.ToString)
            Catch ex As Exception

            End Try
            Select Case xslot
                Case 0
                    Main.Kopf.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Kopf.Visible = True
                    Main.kopfid = realxentryid
                    Main.kopfname = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.kopfpic)
                    getitemstats(xentryid, Main.kopfench)
                Case 1
                    Main.Hals.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Hals.Visible = True
                    Main.halsid = realxentryid
                    Main.halsname = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Halspic)
                    getitemstats(xentryid, Main.halsench)
                Case 2
                    Main.Schulter.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Schulter.Visible = True
                    Main.schulterid = realxentryid
                    Main.schultername = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Schulterpic)
                    getitemstats(xentryid, Main.schulterench)
                Case 3
                    Main.Hemd.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Hemd.Visible = True
                    Main.hemdid = realxentryid
                    Main.hemdname = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Hemdpic)
                    getitemstats(xentryid, Main.hemdench)
                Case 4
                    Main.Brust.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Brust.Visible = True
                    Main.brustid = realxentryid
                    Main.brustname = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Brustpic)
                    getitemstats(xentryid, Main.brustench)
                Case 5
                    Main.Guertel.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Guertel.Visible = True
                    Main.guertelid = realxentryid
                    Main.guertelname = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Guertelpic)
                    getitemstats(xentryid, Main.guertelench)
                Case 6
                    Main.Beine.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Beine.Visible = True
                    Main.beineid = realxentryid
                    Main.beinename = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Beinepic)
                    getitemstats(xentryid, Main.beineench)
                Case 7
                    Main.Stiefel.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Stiefel.Visible = True
                    Main.stiefelid = realxentryid
                    Main.stiefelname = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Stiefelpic)
                    getitemstats(xentryid, Main.stiefelench)
                Case 8
                    Main.Handgelenke.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Handgelenke.Visible = True
                    Main.handgelenkeid = realxentryid
                    Main.handgelenkename = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Handgelenkepic)
                    getitemstats(xentryid, Main.handgelenkeench)
                Case 9
                    Main.Haende.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Haende.Visible = True
                    Main.haendeid = realxentryid
                    Main.haendename = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Haendepic)
                    getitemstats(xentryid, Main.haendeench)
                Case 10
                    Main.Ring1.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Ring1.Visible = True
                    Main.ring1id = realxentryid
                    Main.ring1name = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Ring1pic)
                    getitemstats(xentryid, Main.ring1ench)
                Case 11
                    Main.Ring2.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Ring2.Visible = True
                    Main.ring2id = realxentryid
                    Main.ring2name = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Ring2pic)
                    getitemstats(xentryid, Main.ring2ench)
                Case 12
                    Main.Schmuck1.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Schmuck1.Visible = True
                    Main.schmuck1id = realxentryid
                    Main.schmuck1name = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Schmuck1pic)
                    getitemstats(xentryid, Main.schmuck1ench)
                Case 13
                    Main.Schmuck2.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Schmuck2.Visible = True
                    Main.schmuck2id = realxentryid
                    Main.schmuck2name = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Schmuck2pic)
                    getitemstats(xentryid, Main.schmuck2ench)
                Case 14
                    Main.Ruecken.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Ruecken.Visible = True
                    Main.rueckenid = realxentryid
                    Main.rueckenname = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Rueckenpic)
                    getitemstats(xentryid, Main.rueckenench)

                Case 15
                    Main.Haupt.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Haupt.Visible = True
                    Main.hauptid = realxentryid
                    Main.hauptname = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Hauptpic)
                    getitemstats(xentryid, Main.hauptench)
                    runfunction.getweapontype(realxentryid)
                Case 16
                    Main.Off.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Off.Visible = True
                    Main.offid = realxentryid
                    Main.offname = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Offpic)
                    getitemstats(xentryid, Main.offench)
                    runfunction.getweapontype(realxentryid)
                Case 17
                    Main.Distanz.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Distanz.Visible = True
                    Main.distanzid = realxentryid
                    Main.distanzname = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Distanzpic)
                    getitemstats(xentryid, Main.distanzench)
                    runfunction.getweapontype(realxentryid)
                Case 18
                    Main.Wappenrock.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Wappenrock.Visible = True
                    Main.wappenrockid = realxentryid
                    Main.wappenrockname = itemname
                    If Main.anzahldurchlaufe = 1 Then runfunction.getimage(realxentryid, Main.Wappenrockpic)
                    getitemstats(xentryid, Main.wappenrockench)
                Case Else
            End Select
            xslot += 1
        Loop Until xslot = 19
    End Sub

    Public Sub handleenchantments()
        runfunction.writelog("handleenchantments_call @trinity")
        If Main.anzahldurchlaufe = 1 Then Main.kopfvz.Text = splitstringvz(Main.kopfench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.kopfsocket1.Text = splitstringgem(Main.kopfench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.kopfsocket2.Text = splitstringgem(Main.kopfench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.kopfsocket3.Text = splitstringgem(Main.kopfench, 12)
        Main.kopfvz.Visible = True


        If Main.anzahldurchlaufe = 1 Then Main.halsvz.Text = splitstringvz(Main.halsench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.halssocket1.Text = splitstringgem(Main.halsench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.halssocket2.Text = splitstringgem(Main.halsench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.halssocket3.Text = splitstringgem(Main.halsench, 12)
        Main.halsvz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.schultervz.Text = splitstringvz(Main.schulterench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.schultersocket1.Text = splitstringgem(Main.schulterench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.schultersocket2.Text = splitstringgem(Main.schulterench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.schultersocket3.Text = splitstringgem(Main.schulterench, 12)
        Main.schultervz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.rueckenvz.Text = splitstringvz(Main.rueckenench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.rueckensocket1.Text = splitstringgem(Main.rueckenench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.rueckensocket2.Text = splitstringgem(Main.rueckenench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.rueckensocket3.Text = splitstringgem(Main.rueckenench, 12)
        Main.rueckenvz.Visible = True

        Main.brustvz.Text = splitstringvz(Main.brustench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.brustsocket1.Text = splitstringgem(Main.brustench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.brustsocket2.Text = splitstringgem(Main.brustench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.brustsocket3.Text = splitstringgem(Main.brustench, 12)
        Main.brustvz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.handgelenkevz.Text = splitstringvz(Main.handgelenkeench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.Handgelenkesocket1.Text = splitstringgem(Main.handgelenkeench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.handgelenkesocket2.Text = splitstringgem(Main.handgelenkeench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.Handgelenkesocket3.Text = splitstringgem(Main.handgelenkeench, 12)
        Main.handgelenkevz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.hauptvz.Text = splitstringvz(Main.hauptench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.Hauptsocket1.Text = splitstringgem(Main.hauptench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.Hauptsocket2.Text = splitstringgem(Main.hauptench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.hauptsocket3.Text = splitstringgem(Main.hauptench, 12)
        Main.hauptvz.Visible = True
        Main.hauptvzlabel2.Visible = True
        Main.hauptvzlabel2.Text = Main.hauptvz.Text

        If Main.anzahldurchlaufe = 1 Then Main.offvz.Text = splitstringvz(Main.offench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.Offsocket1.Text = splitstringgem(Main.offench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.Offsocket2.Text = splitstringgem(Main.offench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.offsocket3.Text = splitstringgem(Main.offench, 12)
        Main.offvz.Visible = True
        Main.offvzlabel2.Visible = True
        Main.offvzlabel2.Text = Main.offvz.Text

        If Main.anzahldurchlaufe = 1 Then Main.distanzvz.Text = splitstringvz(Main.distanzench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.Distanzsocket1.Text = splitstringgem(Main.distanzench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.Distanzsocket2.Text = splitstringgem(Main.distanzench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.distanzsocket3.Text = splitstringgem(Main.distanzench, 12)
        Main.distanzvz.Visible = True
        Main.distanzvzlabel2.Visible = True
        Main.distanzvzlabel2.Text = Main.distanzvz.Text

        If Main.anzahldurchlaufe = 1 Then Main.haendevz.Text = splitstringvz(Main.haendeench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.haendesocket1.Text = splitstringgem(Main.haendeench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.haendesocket2.Text = splitstringgem(Main.haendeench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.haendesocket3.Text = splitstringgem(Main.haendeench, 12)
        Main.haendevz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.guertelvz.Text = splitstringvz(Main.guertelench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.guertelsocket1.Text = splitstringgem(Main.guertelench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.guertelsocket2.Text = splitstringgem(Main.guertelench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.guertelsocket3.Text = splitstringgem(Main.guertelench, 12)
        Main.guertelvz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.beinevz.Text = splitstringvz(Main.beineench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.beinesocket1.Text = splitstringgem(Main.beineench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.beinesocket2.Text = splitstringgem(Main.beineench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.beinesocket3.Text = splitstringgem(Main.beineench, 12)
        Main.beinevz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.stiefelvz.Text = splitstringvz(Main.stiefelench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.stiefelsocket1.Text = splitstringgem(Main.stiefelench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.stiefelsocket2.Text = splitstringgem(Main.stiefelench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.stiefelsocket3.Text = splitstringgem(Main.stiefelench, 12)
        Main.stiefelvz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.ring1vz.Text = splitstringvz(Main.ring1ench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.Ring1socket1.Text = splitstringgem(Main.ring1ench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.ring1socket2.Text = splitstringgem(Main.ring1ench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.ring1socket3.Text = splitstringgem(Main.ring1ench, 12)
        Main.ring1vz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.ring2vz.Text = splitstringvz(Main.ring2ench, 0)
        If Main.anzahldurchlaufe = 1 Then Main.ring2socket1.Text = splitstringgem(Main.ring2ench, 6)
        If Main.anzahldurchlaufe = 1 Then Main.ring2socket2.Text = splitstringgem(Main.ring2ench, 9)
        If Main.anzahldurchlaufe = 1 Then Main.ring2socket3.Text = splitstringgem(Main.ring2ench, 12)
        Main.ring2vz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.schmuck1vz.Text = splitstringvz(Main.schmuck1ench, 0)
        Main.schmuck1vz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.schmuck2vz.Text = splitstringvz(Main.schmuck2ench, 0)
        Main.schmuck2vz.Visible = True
    End Sub

    Public Function splitstringvz(ByVal input As String, ByVal position As Integer) As String
        Dim xpacressource As String
        Dim xpacressource2 As String
        Select Case Main.xpac
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
                    Dim quellcodeyx88 As String = xpacressource2
                    Dim anfangyx88 As String = parts(position) & ";"
                    Dim endeyx88 As String = ";xxxx"
                    Dim quellcodeSplityx88 As String
                    quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
                    quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)

                    Return quellcodeSplityx88
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

    Public Function splitstringgem(ByVal input As String, ByVal position As Integer) As String
        Dim xpacressource As String
        Dim xpacressource2 As String
        Select Case Main.xpac
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
                Dim quellcodeyx88 As String = xpacressource2
                Dim anfangyx88 As String = parts(position) & ";"
                Dim endeyx88 As String = ";xxxx"
                Dim quellcodeSplityx88 As String
                quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
                quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)

                Return runfunction.getsocketeffectname(CInt(quellcodeSplityx88))
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

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://wowdata.buffed.de/?i=" & itemid)
            Dim anfangyx88 As String = "<td><h1 class=""headline1"">"
            Dim endeyx88 As String = "</h1></td>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            If quellcodeSplityx88.Contains("Ã¼") Then quellcodeSplityx88 = quellcodeSplityx88.Replace("Ã¼", "ü")
            If quellcodeSplityx88.Contains("Ã¤") Then quellcodeSplityx88 = quellcodeSplityx88.Replace("Ã¤", "ä")
            If quellcodeSplityx88.Contains("Ã¶") Then quellcodeSplityx88 = quellcodeSplityx88.Replace("Ã¶", "ö")
            If quellcodeSplityx88.Contains("ÃŸ") Then quellcodeSplityx88 = quellcodeSplityx88.Replace("ÃŸ", "ß")
            Return quellcodeSplityx88
        Catch ex As Exception
            Return "Platz leer"
        End Try
    End Function

    Public Function getcharguid(ByVal charname As String) As Integer
        runfunction.writelog("getcharguid_call @trinity with charname: " & charname)
        Try
            Return _
                CInt(Val(runfunction.runcommand("SELECT guid FROM characters WHERE name = '" & charname & "'", "guid")))

        Catch ex As Exception
            MsgBox("Charakter Guid konnte nicht gelesen werden! Überprüfe die Datanbankeintragungen.",
                   MsgBoxStyle.Critical, "Fehler")
            Return - 1
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

            Dim sqlquery = "SELECT * FROM account WHERE username = '" & accname & "'"
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = Main.GLOBALconnRealmd
            myCommand.CommandText = sqlquery
            'start query
            myAdapter.SelectCommand = myCommand
            Dim myData As MySqlDataReader
            myData = myCommand.ExecuteReader()
            If CInt(myData.HasRows) = 0 Then
                myData.Close()
                runfunction.normalsqlcommandRealmd(command)
                Dim realmid As Integer = Main.account_access_RealmID
                If realmid = 0 Then
                    realmid = - 1
                ElseIf realmid = Nothing Then
                    realmid = - 1
                Else


                End If
                runfunction.normalsqlcommandRealmd(
                    "INSERT INTO account_access ( id, gmlevel, RealmID ) VALUES ( '" & accguid & "', '" &
                    Main.account_access_gmlevel.ToString & "', '" & realmid.ToString & "' )")
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
        Main.coreguid = newcharguid.ToString
        targetaccount = runfunction.runcommandRealmd("SELECT id FROM account WHERE username='" & targetaccount & "'",
                                                     "id")
        If namechangeeverytime = True Then
            Process_Status.processreport.AppendText(
                Now.TimeOfDay.ToString & "// Player will be asked to change charactername! : reason-nce=true" &
                vbNewLine)
            runfunction.normalsqlcommand(
                "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `position_x`, position_y, position_z, map, orientation, taximask, `health` ) VALUES ( '" &
                newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                "', '0', '0', '0', '1', '0', '0', '" & Main.playerBytes & "', '-14306', '515', '10', '0', '5', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ','1000' )")

            runfunction.normalsqlcommand("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'")
        Else
            If charexist(charactername) = True Then
                Process_Status.processreport.AppendText(
                    Now.TimeOfDay.ToString & "// Player will be asked to change charactername!" & vbNewLine)
                runfunction.normalsqlcommand(
                   "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `position_x`, position_y, position_z, map, orientation, taximask, `health` ) VALUES ( '" &
                   newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                   "', '0', '0', '0', '1', '0', '0', '" & Main.playerBytes & "', '-14306', '515', '10', '0', '5', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ','1000' )")
                runfunction.normalsqlcommand(
                    "UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'")
            Else
                runfunction.normalsqlcommand(
                "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `position_x`, position_y, position_z, map, orientation, taximask, `health` ) VALUES ( '" &
                newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                "', '0', '0', '0', '1', '0', '0', '" & Main.playerBytes & "', '-14306', '515', '10', '0', '5', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ','1000' )")
                runfunction.normalsqlcommand(
                    "UPDATE characters SET at_login='0' WHERE guid='" & newcharguid.ToString & "'")
            End If

        End If
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Creating Hearthstone for Character: " & Main.char_name & vbNewLine)
        Dim newguid As String =
                ((CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)", "guid")))) +
                 1).ToString

        runfunction.normalsqlcommand(
            "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
            newguid & "', '6948', '" & Main.coreguid & "', '1', '0 0 0 0 0 ', '" & newguid &
            " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
        runfunction.normalsqlcommand(
            "INSERT INTO character_inventory ( guid, bag, `slot`, `item` ) VALUES ( '" & Main.coreguid &
            "', '0', '23', '" & newguid & "')")
        If Not Main.finished_quests = "" Then
            Try
                Dim parts() As String = Main.finished_quests.Split(","c)
                Dim excounter As Integer = UBound(Main.finished_quests.Split(CChar(",")))
                Dim startcounter As Integer = 0
                Do
                    Dim questid As String = parts(startcounter)
                    runfunction.normalsqlcommand("INSERT IGNORE INTO character_queststatus_rewarded ( `guid`, `quest` ) VALUES ( '" & Main.coreguid & "', '" & questid & "' )")
                    startcounter += 1
                Loop Until startcounter = excounter
            Catch : End Try
            End If

        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Created Character " & charactername & "!" & vbNewLine)
        Application.DoEvents()
    End Sub

    Public Sub sethome()
        runfunction.writelog("sethome_call @trinity")
        runfunction.normalsqlcommand(
            "INSERT INTO character_homebind ( guid, " & Main.homebind_map & ", " & Main.homebind_zone & ", " &
            Main.homebind_posx & ", " & Main.homebind_posy & ", " & Main.homebind_posz & " ) VALUES ( '" & Main.coreguid &
            "', '" & splitlist(Main.character_homebind, "map") & "', '" & splitlist(Main.character_homebind, "zone") &
            "', '" & splitlist(Main.character_homebind, "position_x") & "', '" &
            splitlist(Main.character_homebind, "position_y") & "', '" & splitlist(Main.character_homebind, "position_z") &
            "' )")
    End Sub

    Public Sub adddetailedchar(ByVal targetaccount As String, ByVal charactername As String,
                               ByVal namechangeeverytime As Boolean)
        runfunction.writelog(
            "addchars_call @trinity with targetaccount: " & targetaccount & " charactername: " & charactername &
            " namechangeeverytime: " & namechangeeverytime.ToString)
        Dim newcharguid As Integer =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", "guid"))) + 1
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "// Creating Character " & charactername & "!" & vbNewLine)
        guid = newcharguid.ToString
        Main.coreguid = newcharguid.ToString
        targetaccount = runfunction.runcommandRealmd("SELECT id FROM account WHERE username='" & targetaccount & "'",
                                                     "id")
        If namechangeeverytime = True Then
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Player will be asked to change charactername! : reason-nce=true" &
                vbNewLine)
            runfunction.normalsqlcommand(
                "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, `playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, knownCurrencies, watchedFaction, `health`, speccount, activespec, exploredZones, knownTitles, actionBars ) VALUES ( '" &
                newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                "', '0', '0', '0', '1', '0', '0', '" & Main.playerBytes.ToString & "', '" & Main.playerBytes2.ToString &
                "', '" & Main.playerFlags.ToString & "', '" & Main.position_x.ToString & "', '" &
                Main.position_y.ToString & "', '" & (CInt(Main.position_z) + 1).ToString & "', '" & Main.map.ToString &
                "', '4,40671', '" & Main.taximask & "', '" & Main.totaltime.ToString & "', '" & Main.leveltime.ToString &
                "', '" & Main.extra_flags & "', '" & Main.stable_slots & "', '" & Main.at_login & "', '" &
                Main.zone.ToString & "', '" & Main.chosenTitle & "', '" & Main.knownCurrencies & "', '" &
                Main.watchedFaction & "', '1000', '" & Main.speccount.ToString & "', '" & Main.activespec.ToString &
                "', '" & Main.exploredZones & "', '" & Main.knownTitles & "', '" & Main.actionBars & "' )")

            runfunction.normalsqlcommand("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'")
        Else
            If charexist(charactername) = True Then
                Process_Status.processreport.appendText(
                    Now.TimeOfDay.ToString & "// Player will be asked to change charactername!" & vbNewLine)
                runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, `playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, knownCurrencies, watchedFaction, `health`, speccount, activespec, exploredZones, knownTitles, actionBars ) VALUES ( '" &
                    newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '" & Main.playerBytes.ToString & "', '" &
                    Main.playerBytes2.ToString & "', '" & Main.playerFlags.ToString & "', '" & Main.position_x.ToString &
                    "', '" & Main.position_y.ToString & "', '" & (CInt(Main.position_z) + 1).ToString & "', '" &
                    Main.map.ToString & "', '4,40671', '" & Main.taximask & "', '" & Main.totaltime.ToString & "', '" &
                    Main.leveltime.ToString & "', '" & Main.extra_flags & "', '" & Main.stable_slots & "', '" &
                    Main.at_login & "', '" & Main.zone.ToString & "', '" & Main.chosenTitle & "', '" &
                    Main.knownCurrencies & "', '" & Main.watchedFaction & "', '1000', '" & Main.speccount.ToString &
                    "', '" & Main.activespec.ToString & "', '" & Main.exploredZones & "', '" & Main.knownTitles & "', '" &
                    Main.actionBars & "' )")
                runfunction.normalsqlcommand(
                    "UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'")
            Else
                runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, `playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, knownCurrencies, watchedFaction, `health`, speccount, activespec, exploredZones, knownTitles, actionBars ) VALUES ( '" &
                    newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '" & Main.playerBytes.ToString & "', '" &
                    Main.playerBytes2.ToString & "', '" & Main.playerFlags.ToString & "', '" & Main.position_x.ToString &
                    "', '" & Main.position_y.ToString & "', '" & (CInt(Main.position_z) + 1).ToString & "', '" &
                    Main.map.ToString & "', '4,40671', '" & Main.taximask & "', '" & Main.totaltime.ToString & "', '" &
                    Main.leveltime.ToString & "', '" & Main.extra_flags & "', '" & Main.stable_slots & "', '" &
                    Main.at_login & "', '" & Main.zone.ToString & "', '" & Main.chosenTitle & "', '" &
                    Main.knownCurrencies & "', '" & Main.watchedFaction & "', '1000', '" & Main.speccount.ToString &
                    "', '" & Main.activespec.ToString & "', '" & Main.exploredZones & "', '" & Main.knownTitles & "', '" &
                    Main.actionBars & "' )")

            End If

        End If
        If Not Main.finished_quests = "" Then
            Try
                Dim parts() As String = Main.finished_quests.Split(","c)
                Dim excounter As Integer = UBound(Main.finished_quests.Split(CChar(",")))
                Dim startcounter As Integer = 0
                Do
                    Dim questid As String = parts(startcounter)
                    runfunction.normalsqlcommand("INSERT IGNORE INTO character_queststatus_rewarded ( `guid`, `quest` ) VALUES ( '" & Main.coreguid & "', '" & questid & "' )")
                    startcounter += 1
                Loop Until startcounter = excounter
            Catch : End Try
        End If
        setqueststatus()
        sethome()
        addaction()
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "// Created Character " & charactername & "!" & vbNewLine)
        Application.DoEvents()
    End Sub

    Public Sub requestnamechange(ByVal charname As String)
        runfunction.writelog("sethome_call @trinity with charname: " & charname)
        runfunction.normalsqlcommand("UPDATE characters SET at_login='1' WHERE name='" & charname & "'")
    End Sub

    Public Function charexist(ByVal charname As String) As Boolean
        runfunction.writelog("charexist_call @trinity with charname: " & charname)
        Try

            '  ("SELECT * FROM characters WHERE name = '" & charname.Text & "'")
            Dim myAdapter As New MySqlDataAdapter

            Dim sqlquery = "SELECT * FROM characters WHERE name = '" & charname & "'"
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = Main.GLOBALconn
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

        runfunction.normalsqlcommand("UPDATE characters SET at_login='1' WHERE name='" & charname & "'")
    End Sub

    Public Sub getguidfromname(ByVal charactername As String)
        runfunction.writelog("getguidfromname_call @trinity with charactername: " & charactername)
        guid = runfunction.runcommand("SELECT guid FROM characters WHERE name = '" & charactername & "'", "guid")
        Main.coreguid = guid
        addaction()
    End Sub

    Public Sub additems()
        runfunction.writelog("additems_call @trinity")
        guid = Main.coreguid
        finalstring =
            "kopf 0 hals 0 schulter 0 hemd 0 brust 0 guertel 0 beine 0 stiefel 0 handgelenke 0 haende 0 finger1 0 finger2 0 schmuck1 0 schmuck2 0 ruecken 0 haupt 0 off 0 distanz 0 wappenrock 0 "
        lastnumber =
            runfunction.runcommand("SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)",
                                   "guid")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Creating Items for Character: " & Main.char_name & vbNewLine)
        Application.DoEvents()
        If Main.char_class = 1 Or Main.char_class = 2 Or Main.char_class = 6 Then
            runfunction.normalsqlcommand(
                "INSERT INTO character_spell ( guid, spell, active, disabled ) VALUES ( '" & guid &
                "', '750', '1', '0' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_skills ( guid, skill, value, max ) VALUES ( '" & guid & "', '293', '1', '1' )")
        ElseIf _
            Main.char_class = 1 Or Main.char_class = 2 Or Main.char_class = 3 Or Main.char_class = 6 Or
            Main.char_class = 7 Then
            runfunction.normalsqlcommand(
                "INSERT INTO character_spell ( guid, spell, active, disabled ) VALUES ( '" & guid &
                "', '8737', '1', '0' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_skills ( guid, skill, value, max ) VALUES ( '" & guid & "', '413', '1', '1' )")
        ElseIf _
            Main.char_class = 1 Or Main.char_class = 2 Or Main.char_class = 3 Or Main.char_class = 4 Or
            Main.char_class = 6 Or Main.char_class = 7 Or Main.char_class = 11 Then
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
        For Each specialskill In Main.specialskills
            runfunction.normalsqlcommand(
                "INSERT INTO character_skills ( guid, skill, value, max ) VALUES ( '" & guid & "', '" & specialskill &
                "', '1', '1' )")
            skillcounter += 1
        Next
        For Each specialspell In Main.specialspells
            runfunction.normalsqlcommand(
                "INSERT INTO character_spell ( guid, spell, active, disabled ) VALUES ( '" & guid & "', '" &
                specialspell & "', '1', '0' )")
            spellcounter += 1
        Next
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Created " & spellcounter.ToString & " spells and " & skillcounter.ToString &
            " skills!" & vbNewLine)

        If Not Main.kopfid = Nothing Then

            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            kopfwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.kopfid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.kopfid, "kopf", False)
            checkexist_anddelete(guid, "0", lastnumber)
        End If

        If Not Main.halsid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            halswearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.halsid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.halsid, "hals", False)
            checkexist_anddelete(guid, "1", lastnumber)
        End If
        If Not Main.schulterid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            schulterwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.schulterid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.schulterid, "schulter", False)
            checkexist_anddelete(guid, "2", lastnumber)
        End If

        If Not Main.rueckenid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            rueckenwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.rueckenid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.rueckenid, "ruecken", False)
            checkexist_anddelete(guid, "14", lastnumber)
        End If
        If Not Main.brustid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            brustwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.brustid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.brustid, "brust", False)
            checkexist_anddelete(guid, "4", lastnumber)
        End If
        If Not Main.hemdid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            hemdwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.hemdid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.hemdid, "hemd", False)
            checkexist_anddelete(guid, "3", lastnumber)
        End If
        If Not Main.wappenrockid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            wappenrockwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.wappenrockid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.wappenrockid, "wappenrock", False)
            checkexist_anddelete(guid, "18", lastnumber)
        End If
        If Not Main.handgelenkeid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            handgelenkewearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.handgelenkeid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.handgelenkeid, "handgelenke", False)
            checkexist_anddelete(guid, "8", lastnumber)
        End If
        If Not Main.hauptid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            hauptwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.hauptid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.hauptid, "haupt", False)
            checkexist_anddelete(guid, "15", lastnumber)
        End If
        If Not Main.offid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            offwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.offid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.offid, "off", False)
            checkexist_anddelete(guid, "16", lastnumber)
        End If
        If Not Main.distanzid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            distanzwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.distanzid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.distanzid, "distanz", False)
            checkexist_anddelete(guid, "17", lastnumber)
        End If
        If Not Main.haendeid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            haendewearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.haendeid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.haendeid, "haende", False)
            checkexist_anddelete(guid, "9", lastnumber)
        End If
        If Not Main.guertelid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            guertelwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.guertelid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.guertelid, "guertel", False)
            checkexist_anddelete(guid, "5", lastnumber)
        End If
        If Not Main.beineid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            beinewearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.beineid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.beineid, "beine", False)
            checkexist_anddelete(guid, "6", lastnumber)
        End If
        If Not Main.stiefelid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            stiefelwearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.stiefelid & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.stiefelid, "stiefel", False)
            checkexist_anddelete(guid, "7", lastnumber)
        End If
        If Not Main.ring1id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            ring1wearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.ring1id & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.ring1id, "finger1", False)
            checkexist_anddelete(guid, "10", lastnumber)
        End If
        If Not Main.ring2id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            ring2wearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.ring2id & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.ring2id, "finger2", False)
            checkexist_anddelete(guid, "11", lastnumber)
        End If
        If Not Main.schmuck1id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            schmuck1wearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.schmuck1id & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.schmuck1id, "schmuck1", False)
            checkexist_anddelete(guid, "12", lastnumber)
        End If
        If Not Main.schmuck2id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            schmuck2wearguid = CInt(lastnumber)
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                lastnumber & "', '" & Main.schmuck2id & "', '" & guid &
                "', '1', '0 0 0 0 0 ', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
            makestring(Main.schmuck2id, "schmuck2", False)
            checkexist_anddelete(guid, "13", lastnumber)
        End If
        makestring(0, "", True)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Created Items!" & vbNewLine)
    End Sub

    Public Sub addglyphs(ByVal expansion As String)
        runfunction.writelog("addglyphs_call @trinity with expansion: " & expansion)
        newcharguid = Main.coreguid
        guid = Main.coreguid
        checkglyphsanddelete(newcharguid)
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding Glyphs to Character: " & Main.char_name & vbNewLine)
        If expansion = "cata" Then
            runfunction.normalsqlcommand(
                "INSERT INTO character_glyphs ( guid, spec, glyph1, glyph2, glyph3, glyph4, glyph5, glyph6, glyph7, glyph8, glyph9 ) VALUES ( '" &
                newcharguid & "', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_glyphs ( guid, spec, glyph1, glyph2, glyph3, glyph4, glyph5, glyph6, glyph7, glyph8, glyph9 ) VALUES ( '" &
                newcharguid & "', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0' )")
            If Not Main.minorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph5 = '" & runfunction.getglyphid2(Main.minorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.minorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph2 = '" & runfunction.getglyphid2(Main.minorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.minorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph3 = '" & runfunction.getglyphid2(Main.minorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.majorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph1 = '" & runfunction.getglyphid2(Main.majorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.majorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph4 = '" & runfunction.getglyphid2(Main.majorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.majorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph6 = '" & runfunction.getglyphid2(Main.majorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.primeglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph7 = '" & runfunction.getglyphid2(Main.primeglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.primeglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph8 = '" & runfunction.getglyphid2(Main.primeglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.primeglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph9 = '" & runfunction.getglyphid2(Main.primeglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If


            If Not Main.secminorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph5 = '" & runfunction.getglyphid2(Main.secminorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secminorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph2 = '" & runfunction.getglyphid2(Main.secminorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secminorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph3 = '" & runfunction.getglyphid2(Main.secminorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secmajorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph1 = '" & runfunction.getglyphid2(Main.secmajorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secmajorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph4 = '" & runfunction.getglyphid2(Main.secmajorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secmajorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph6 = '" & runfunction.getglyphid2(Main.secmajorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secprimeglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph7 = '" & runfunction.getglyphid2(Main.secprimeglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secprimeglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph8 = '" & runfunction.getglyphid2(Main.secprimeglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secprimeglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph9 = '" & runfunction.getglyphid2(Main.secprimeglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
        Else
            runfunction.normalsqlcommand(
                "INSERT INTO character_glyphs ( guid, spec, glyph1, glyph2, glyph3, glyph4, glyph5, glyph6 ) VALUES ( '" &
                newcharguid & "', '0', '0', '0', '0', '0', '0', '0' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_glyphs ( guid, spec, glyph1, glyph2, glyph3, glyph4, glyph5, glyph6 ) VALUES ( '" &
                newcharguid & "', '1', '0', '0', '0', '0', '0', '0' )")
            If Not Main.minorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph5 = '" & runfunction.getglyphid2(Main.minorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.minorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph2 = '" & runfunction.getglyphid2(Main.minorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.minorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph3 = '" & runfunction.getglyphid2(Main.minorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.majorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph1 = '" & runfunction.getglyphid2(Main.majorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.majorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph4 = '" & runfunction.getglyphid2(Main.majorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If
            If Not Main.majorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph6 = '" & runfunction.getglyphid2(Main.majorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='0'")
            End If


            If Not Main.secminorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph5 = '" & runfunction.getglyphid2(Main.secminorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secminorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph2 = '" & runfunction.getglyphid2(Main.secminorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secminorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph3 = '" & runfunction.getglyphid2(Main.secminorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secmajorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph1 = '" & runfunction.getglyphid2(Main.secmajorglyph1) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secmajorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph4 = '" & runfunction.getglyphid2(Main.secmajorglyph2) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
            If Not Main.secmajorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "UPDATE `character_glyphs` SET glyph6 = '" & runfunction.getglyphid2(Main.secmajorglyph3) &
                    "' WHERE guid = '" & guid & "' AND spec='1'")
            End If
        End If
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Added Glyphs!" & vbNewLine)
    End Sub

    Public Sub setgender(ByVal gender As String)
        runfunction.writelog("setgender_call @trinity with gender: " & gender)
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting gender for Character: " & Main.char_name & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand("UPDATE `characters` SET gender='" & gender & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setlevel()
        runfunction.writelog("setlevel_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting Level for Character: " & Main.char_name & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET level='" & Main.char_level.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setrace()
        runfunction.writelog("setrace_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting race for Character: " & Main.char_name & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET race='" & Main.char_race.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setclass()
        runfunction.writelog("setclass_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting class for Character: " & Main.char_name & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET `class`='" & Main.char_class.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setalternatelevel(ByVal alternatelevel As String)
        runfunction.writelog("setalternatelevel_call @trinity with alternatelevel: " & alternatelevel)
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting alternative level for Character: " & Main.char_name & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand("UPDATE `characters` SET level='" & alternatelevel & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setgold(ByVal amount As String)
        runfunction.writelog("setgold_call @trinity with amount: " & amount)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding gold..." & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET money='" & (CInt(Val(amount))*10000).ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub addgold(ByVal amount As Integer)
        runfunction.writelog("addgold_call @trinity with amount: " & amount.ToString)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding gold..." & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand("UPDATE `characters` SET money='" & amount.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub addtalents()
        runfunction.writelog("addtalents_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting Talents for Character: " & Main.char_name & vbNewLine)
        For Each talentstring As String In Main.character_talent_list
            runfunction.normalsqlcommand(
                "INSERT INTO character_talent ( guid, spell, spec ) VALUES ( '" & Main.coreguid & "', '" &
                splitlist(talentstring, "spell") & "', '" & splitlist(talentstring, "spec") & "')")

            ' ("<spell>" & spell & "</spell><spec>" & spec & "</spec>")
        Next
    End Sub

    Public Sub setqueststatus()
        runfunction.writelog("setqueststatus_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting queststatus for Character: " & Main.char_name & vbNewLine)
        For Each queststring As String In Main.character_queststatus
            runfunction.normalsqlcommand(
                "INSERT INTO character_queststatus ( guid, quest, `status`, `explored` ) VALUES ( '" & Main.coreguid &
                "', '" & splitlist(queststring, "quest") & "', '" & splitlist(queststring, "status") & "', '" &
                splitlist(queststring, "explored") & "')")


        Next
    End Sub

    Public Sub addachievements()
        runfunction.writelog("addachievements_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding achievements for Character: " & Main.char_name & vbNewLine)
        For Each avstring As String In Main.character_achievement_list
            runfunction.normalsqlcommand(
                "INSERT INTO character_achievement ( guid, achievement, date ) VALUES ( '" & Main.coreguid & "', '" &
                splitlist(avstring, "av") & "', '" & splitlist(avstring, "date") & "')")

            ' "<av>" & avid & "</av><date>" & xdate & "</date>"
        Next
    End Sub

    Public Sub addskills()
        runfunction.writelog("addskills_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting skills for Character: " & Main.char_name & vbNewLine)
        For Each skill As String In Main.character_skills_list
            runfunction.normalsqlcommand(
                "INSERT INTO character_skills ( guid, skill, `value`, `max` ) VALUES ( '" & Main.coreguid & "', '" &
                splitlist(skill, "skill") & "', '" & splitlist(skill, "value") & "', '" & splitlist(skill, "max") & "')")

            ' "<skill>" & skill & "</skill><value>" & value & "</value><max>" & max & "</max>"
        Next
    End Sub

    Public Sub addspells()
        runfunction.writelog("addspells_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Teaching spells for Character: " & Main.char_name & vbNewLine)
        For Each spell As String In Main.character_spells
            runfunction.normalsqlcommand(
                "INSERT INTO character_spell ( guid, spell, `active`, `disabled` ) VALUES ( '" & Main.coreguid & "', '" &
                splitlist(spell, "spell") & "', '" & splitlist(spell, "active") & "', '" & splitlist(spell, "disabled") &
                "')")

            ' "<spell>" & spell & "</spell><active>" & active & "</active><disabled>" & disabled & "</disabled>"
        Next
    End Sub

    Public Sub addreputation()
        runfunction.writelog("addreputation_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding reputation for Character: " & Main.char_name & vbNewLine)
        For Each repstring As String In Main.character_reputatuion_list
            runfunction.normalsqlcommand(
                "INSERT INTO character_reputation ( guid, faction, `standing`, `flags` ) VALUES ( '" & Main.coreguid &
                "', '" & splitlist(repstring, "faction") & "', '" & splitlist(repstring, "standing") & "', '" &
                splitlist(repstring, "flags") & "')")

            ' "<faction>" & faction & "</faction><standing>" & standing & "</standing><flags>" & flags & "</flags>"
        Next
    End Sub

    Public Sub addaction()
        runfunction.writelog("addaction_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting up actionbars for Character: " & Main.char_name & vbNewLine)
        For Each actionstring As String In Main.character_action_list
            runfunction.normalsqlcommand(
                "INSERT INTO character_action ( guid, spec, `button`, `action`, `type` ) VALUES ( '" & Main.coreguid &
                "', '" & splitlist(actionstring, "spec") & "', '" & splitlist(actionstring, "button") & "', '" &
                splitlist(actionstring, "action") & "', '" & splitlist(actionstring, "type") & "')")

            ' "<action>" & action & "</action><spec>" & spec & "</spec><button>" & gbutton & "</button><type>" & atype & "</atype>"
        Next
    End Sub

    Public Sub addinventory()
        runfunction.writelog("addinventory_call @trinity")
        Dim bagstring As String = ""
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding Items to inventory for Character: " & Main.char_name & vbNewLine)
        Dim bagexist As List(Of String) = New List(Of String)
        bagexist.Clear()
        For Each inventorystring As String In Main.character_inventoryzero_list


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
                newguid & "', '" & splitlist(inventorystring, "item") & "', '" & Main.coreguid &
                "', '" & splitlist(inventorystring, "count") & "', '0 0 0 0 0 ', '" & splitlist(inventorystring, "enchant") & "', '1000' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_inventory ( guid, bag, `slot`, `item` ) VALUES ( '" & Main.coreguid & "', '" &
                bag & "', '" & splitlist(inventorystring, "slot") & "', '" & newguid & "')")


        Next
        For Each inventorystring As String In Main.character_inventory_list
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
                        "SELECT guid FROM item_instance WHERE itemEntry='" & bag & "' AND owner_guid='" & Main.coreguid &
                        "'", "guid")
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, itemEntry, owner_guid, count, charges, enchantments, durability ) VALUES ( '" &
                newguid & "', '" & splitlist(inventorystring, "item") & "', '" & Main.coreguid &
                "', '" & splitlist(inventorystring, "count") & "', '0 0 0 0 0 ', '" & splitlist(inventorystring, "enchant") & "', '1000' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_inventory ( guid, bag, `slot`, `item` ) VALUES ( '" & Main.coreguid & "', '" &
                newbagguid & "', '" & splitlist(inventorystring, "slot") & "', '" & newguid & "')")


            ' <slot>" & tmpext.ToString & "</slot><bag>" & bag & "</bag><item>" & entryid & "</item><enchant>" & enchantments & "</enchant>
        Next
    End Sub

    Private Function splitenchstring(ByVal enchstring As String) As String
        Dim Anzahl As Integer = UBound(enchstring.Split(CChar(" ")))
        Dim normalenchstring As String = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 "
        '0:vz 6:gem1 9:gem2 12:gem3
        If enchstring.Contains(",") Then
            'ARCEMU
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
                    'gem1
                    Dim input As String = normalenchstring
                    Dim parts3() As String = input.Split(" "c)

                    parts3(6) = parts2(0)
                    normalenchstring = String.Join(" ", parts3)
                ElseIf parts2(2) = "2" Then
                    'gem2
                    Dim input As String = normalenchstring
                    Dim parts3() As String = input.Split(" "c)

                    parts3(9) = parts2(0)
                    normalenchstring = String.Join(" ", parts3)
                ElseIf parts2(2) = "3" Then
                    'gem3
                    Dim input As String = normalenchstring
                    Dim parts3() As String = input.Split(" "c)

                    parts3(12) = parts2(0)
                    normalenchstring = String.Join(" ", parts3)
                End If


                startcounter += 1
            Loop Until startcounter = excounter

            Return normalenchstring
        Else
            If Anzahl > 45 Then
                'mangos
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
        runfunction.writelog("addench_call @trinity")
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding item enchantments..." & vbNewLine)
        Application.DoEvents()
        If Not Main.kopfench = "" Then _
             runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" & splitenchstring(Main.kopfench, kopfwearguid, Main.kopfid.ToString) &
                "' WHERE guid='" & kopfwearguid & "'")
        If Not Main.halsench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" & splitenchstring(Main.halsench, halswearguid, Main.halsid.ToString) &
                "' WHERE guid='" & halswearguid & "'")
        If Not Main.schulterench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.schulterench, schulterwearguid, Main.schulterid.ToString) & "' WHERE guid='" &
                schulterwearguid & "'")
        If Not Main.rueckenench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.rueckenench, rueckenwearguid, Main.rueckenid.ToString) & "' WHERE guid='" &
                rueckenwearguid & "'")
        If Not Main.brustench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.brustench, brustwearguid, Main.brustid.ToString) & "' WHERE guid='" & brustwearguid &
                "'")
        If Not Main.hemdench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" & splitenchstring(Main.hemdench, hemdwearguid, Main.hemdid.ToString) &
                "' WHERE guid='" & hemdwearguid & "'")
        If Not Main.wappenrockench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.wappenrockench, wappenrockwearguid, Main.wappenrockid.ToString) & "' WHERE guid='" &
                wappenrockwearguid & "'")
        If Not Main.handgelenkeench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.handgelenkeench, handgelenkewearguid, Main.handgelenkeid.ToString) &
                "' WHERE guid='" & handgelenkewearguid & "'")
        If Not Main.haendeench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.haendeench, haendewearguid, Main.haendeid.ToString) & "' WHERE guid='" &
                haendewearguid & "'")
        If Not Main.hauptench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.hauptench, hauptwearguid, Main.hauptid.ToString) & "' WHERE guid='" & hauptwearguid &
                "'")
        If Not Main.offench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" & splitenchstring(Main.offench, offwearguid, Main.offid.ToString) &
                "' WHERE guid='" & offwearguid & "'")
        If Not Main.distanzench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.distanzench, distanzwearguid, Main.distanzid.ToString) & "' WHERE guid='" &
                distanzwearguid & "'")
        If Not Main.guertelench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.guertelench, guertelwearguid, Main.guertelid.ToString) & "' WHERE guid='" &
                guertelwearguid & "'")
        If Not Main.beineench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.beineench, beinewearguid, Main.beineid.ToString) & "' WHERE guid='" & beinewearguid &
                "'")
        If Not Main.stiefelench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.stiefelench, stiefelwearguid, Main.stiefelid.ToString) & "' WHERE guid='" &
                stiefelwearguid & "'")
        If Not Main.ring1ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.ring1ench, ring1wearguid, Main.ring1id.ToString) & "' WHERE guid='" & ring1wearguid &
                "'")
        If Not Main.ring2ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.ring2ench, ring2wearguid, Main.ring2id.ToString) & "' WHERE guid='" & ring2wearguid &
                "'")
        If Not Main.schmuck1ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.schmuck1ench, schmuck1wearguid, Main.schmuck1id.ToString) & "' WHERE guid='" &
                schmuck1wearguid & "'")
        If Not Main.schmuck2ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET enchantments='" &
                splitenchstring(Main.schmuck2ench, schmuck2wearguid, Main.schmuck2id.ToString) & "' WHERE guid='" &
                schmuck2wearguid & "'")
    End Sub
    Private Function splitenchstring(ByVal enchstring As String, ByVal guid As Integer, ByVal entry As String) As String
        Dim numstring As Integer = 0
        Try
            numstring = UBound(enchstring.Split(CChar(" ")))
        Catch : End Try
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
                Return enchstring
            Else
                'trinity
                Return enchstring
            End If
        End If
    End Function
    Public Sub addgems()
        runfunction.writelog("addgems_call @trinity")
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding character gems..." & vbNewLine)
        If Main.kopfsocket1id > 0 Then socketinsert(Main.kopfsocket1id.ToString, kopfwearguid.ToString, 7)
        If Main.halssocket1id > 0 Then socketinsert(Main.halssocket1id.ToString, halswearguid.ToString, 7)
        If Main.schultersocket1id > 0 Then socketinsert(Main.schultersocket1id.ToString, schulterwearguid.ToString, 7)
        If Main.rueckensocket1id > 0 Then socketinsert(Main.rueckensocket1id.ToString, rueckenwearguid.ToString, 7)
        If Main.brustsocket1id > 0 Then socketinsert(Main.brustsocket1id.ToString, brustwearguid.ToString, 7)
        If Main.handgelenkesocket1id > 0 Then _
            socketinsert(Main.handgelenkesocket1id.ToString, handgelenkewearguid.ToString, 7)
        If Main.haendesocket1id > 0 Then socketinsert(Main.haendesocket1id.ToString, haendewearguid.ToString, 7)
        If Main.guertelsocket1id > 0 Then
            beltinsert("3729", guertelwearguid.ToString, 19)
            socketinsert(Main.guertelsocket1id.ToString, guertelwearguid.ToString, 7)
        End If

        If Main.beinesocket1id > 0 Then socketinsert(Main.beinesocket1id.ToString, beinewearguid.ToString, 7)
        If Main.stiefelsocket1id > 0 Then socketinsert(Main.stiefelsocket1id.ToString, stiefelwearguid.ToString, 7)
        If Main.ring1socket1id > 0 Then socketinsert(Main.ring1socket1id.ToString, ring1wearguid.ToString, 7)
        If Main.ring2socket1id > 0 Then socketinsert(Main.ring2socket1id.ToString, ring2wearguid.ToString, 7)
        If Main.schmuck1socket1id > 0 Then socketinsert(Main.schmuck1socket1id.ToString, schmuck1wearguid.ToString, 7)
        If Main.schmuck2socket1id > 0 Then socketinsert(Main.schmuck2socket1id.ToString, schmuck2wearguid.ToString, 7)
        If Main.hauptsocket1id > 0 Then socketinsert(Main.hauptsocket1id.ToString, hauptwearguid.ToString, 7)
        If Main.offsocket1id > 0 Then socketinsert(Main.offsocket1id.ToString, offwearguid.ToString, 7)
        If Main.distanzsocket1id > 0 Then socketinsert(Main.distanzsocket1id.ToString, distanzwearguid.ToString, 7)

        If Main.kopfsocket2id > 0 Then socketinsert(Main.kopfsocket2id.ToString, kopfwearguid.ToString, 10)
        If Main.halssocket2id > 0 Then socketinsert(Main.halssocket2id.ToString, halswearguid.ToString, 10)
        If Main.schultersocket2id > 0 Then socketinsert(Main.schultersocket2id.ToString, schulterwearguid.ToString, 10)
        If Main.rueckensocket2id > 0 Then socketinsert(Main.rueckensocket2id.ToString, rueckenwearguid.ToString, 10)
        If Main.brustsocket2id > 0 Then socketinsert(Main.brustsocket2id.ToString, brustwearguid.ToString, 10)
        If Main.handgelenkesocket2id > 0 Then _
            socketinsert(Main.handgelenkesocket2id.ToString, handgelenkewearguid.ToString, 10)
        If Main.haendesocket2id > 0 Then socketinsert(Main.haendesocket2id.ToString, haendewearguid.ToString, 10)
        If Main.guertelsocket2id > 0 Then
            beltinsert("3729", guertelwearguid.ToString, 19)
            socketinsert(Main.guertelsocket2id.ToString, guertelwearguid.ToString, 10)
        End If

        If Main.beinesocket2id > 0 Then socketinsert(Main.beinesocket2id.ToString, beinewearguid.ToString, 10)
        If Main.stiefelsocket2id > 0 Then socketinsert(Main.stiefelsocket2id.ToString, stiefelwearguid.ToString, 10)
        If Main.ring1socket2id > 0 Then socketinsert(Main.ring1socket2id.ToString, ring1wearguid.ToString, 10)
        If Main.ring2socket2id > 0 Then socketinsert(Main.ring2socket2id.ToString, ring2wearguid.ToString, 10)
        If Main.schmuck1socket2id > 0 Then socketinsert(Main.schmuck1socket2id.ToString, schmuck1wearguid.ToString, 10)
        If Main.schmuck2socket2id > 0 Then socketinsert(Main.schmuck2socket2id.ToString, schmuck2wearguid.ToString, 10)
        If Main.hauptsocket2id > 0 Then socketinsert(Main.hauptsocket2id.ToString, hauptwearguid.ToString, 10)
        If Main.offsocket2id > 0 Then socketinsert(Main.offsocket2id.ToString, offwearguid.ToString, 10)
        If Main.distanzsocket2id > 0 Then socketinsert(Main.distanzsocket2id.ToString, distanzwearguid.ToString, 10)

        If Main.kopfsocket3id > 0 Then socketinsert(Main.kopfsocket3id.ToString, kopfwearguid.ToString, 13)
        If Main.halssocket3id > 0 Then socketinsert(Main.halssocket3id.ToString, halswearguid.ToString, 13)
        If Main.schultersocket3id > 0 Then socketinsert(Main.schultersocket3id.ToString, schulterwearguid.ToString, 13)
        If Main.rueckensocket3id > 0 Then socketinsert(Main.rueckensocket3id.ToString, rueckenwearguid.ToString, 13)
        If Main.brustsocket3id > 0 Then socketinsert(Main.brustsocket3id.ToString, brustwearguid.ToString, 13)
        If Main.handgelenkesocket3id > 0 Then _
            socketinsert(Main.handgelenkesocket3id.ToString, handgelenkewearguid.ToString, 13)
        If Main.haendesocket3id > 0 Then socketinsert(Main.haendesocket3id.ToString, haendewearguid.ToString, 13)
        If Main.guertelsocket3id > 0 Then
            beltinsert("3729", guertelwearguid.ToString, 19)
            socketinsert(Main.guertelsocket3id.ToString, guertelwearguid.ToString, 13)
        End If


        If Main.beinesocket3id > 0 Then socketinsert(Main.beinesocket3id.ToString, beinewearguid.ToString, 13)
        If Main.stiefelsocket3id > 0 Then socketinsert(Main.stiefelsocket3id.ToString, stiefelwearguid.ToString, 13)
        If Main.ring1socket3id > 0 Then socketinsert(Main.ring1socket3id.ToString, ring1wearguid.ToString, 13)
        If Main.ring2socket3id > 0 Then socketinsert(Main.ring2socket3id.ToString, ring2wearguid.ToString, 13)
        If Main.schmuck1socket3id > 0 Then socketinsert(Main.schmuck1socket3id.ToString, schmuck1wearguid.ToString, 13)
        If Main.schmuck2socket3id > 0 Then socketinsert(Main.schmuck2socket3id.ToString, schmuck2wearguid.ToString, 13)
        If Main.hauptsocket3id > 0 Then socketinsert(Main.hauptsocket3id.ToString, hauptwearguid.ToString, 13)
        If Main.offsocket3id > 0 Then socketinsert(Main.offsocket3id.ToString, offwearguid.ToString, 13)
        If Main.distanzsocket3id > 0 Then socketinsert(Main.distanzsocket3id.ToString, distanzwearguid.ToString, 13)
    End Sub

    Public Sub addenchantments()
        runfunction.writelog("addenchantments_call @trinity")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding character enchantments..." & vbNewLine)
        If Main.kopfvzid > 0 Then vzinsert(Main.kopfvzid.ToString, kopfwearguid.ToString, 1)
        If Main.halsvzid > 0 Then vzinsert(Main.halsvzid.ToString, halswearguid.ToString, 1)
        If Main.schultervzid > 0 Then vzinsert(Main.schultervzid.ToString, schulterwearguid.ToString, 1)
        If Main.rueckenvzid > 0 Then vzinsert(Main.rueckenvzid.ToString, rueckenwearguid.ToString, 1)
        If Main.brustvzid > 0 Then vzinsert(Main.brustvzid.ToString, brustwearguid.ToString, 1)
        If Main.handgelenkevzid > 0 Then vzinsert(Main.handgelenkevzid.ToString, handgelenkewearguid.ToString, 1)
        If Main.haendevzid > 0 Then vzinsert(Main.haendevzid.ToString, haendewearguid.ToString, 1)
        If Main.guertelvzid > 0 Then vzinsert(Main.guertelvzid.ToString, guertelwearguid.ToString, 1)
        If Main.beinevzid > 0 Then vzinsert(Main.beinevzid.ToString, beinewearguid.ToString, 1)
        If Main.stiefelvzid > 0 Then vzinsert(Main.stiefelvzid.ToString, stiefelwearguid.ToString, 1)
        If Main.ring1vzid > 0 Then vzinsert(Main.ring1vzid.ToString, ring1wearguid.ToString, 1)
        If Main.ring2vzid > 0 Then vzinsert(Main.ring2vzid.ToString, ring2wearguid.ToString, 1)
        If Main.schmuck1vzid > 0 Then vzinsert(Main.schmuck1vzid.ToString, schmuck1wearguid.ToString, 1)
        If Main.schmuck2vzid > 0 Then vzinsert(Main.schmuck2vzid.ToString, schmuck2wearguid.ToString, 1)
        If Main.hauptvzid > 0 Then vzinsert(Main.hauptvzid.ToString, hauptwearguid.ToString, 1)
        If Main.offvzid > 0 Then vzinsert(Main.offvzid.ToString, offwearguid.ToString, 1)
        If Main.distanzvzid > 0 Then vzinsert(Main.distanzvzid.ToString, distanzwearguid.ToString, 1)
    End Sub

    Public Sub addpvp()
        runfunction.writelog("addpvp_call @trinity")
        Process_Status.processreport.appendText(
            Now.TimeOfDay.ToString & "// Setting character honor/kills..." & vbNewLine)

        runfunction.normalsqlcommand(
            "UPDATE `characters` SET arenaPoints='" & Main.arenaPoints.ToString & "' WHERE guid='" & Main.coreguid & "'")
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET totalHonorPoints='" & Main.totalHonorPoints.ToString & "' WHERE guid='" &
            Main.coreguid & "'")
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET totalKills='" & Main.totalKills.ToString & "' WHERE guid='" & Main.coreguid & "'")
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
            parts(position - 1) = runfunction.getvzeffectid(runfunction.getvzeffectname2(CInt(vzid))).ToString
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
            myCommand.Connection = Main.GLOBALconn
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
            myCommand.Connection = Main.GLOBALconn
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
            myCommand.Connection = Main.GLOBALconn
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
        guid = Main.coreguid
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
