'****************************************************************************************
'****************************************************************************************
'***************************** CharImport- Mangos_core **********************************
'****************************************************************************************
'..................Status
'...................Code:       90%
'...................Design:     95%
'...................Functions:  80%
'****************************************************************************************
'****************************************************************************************
'..................Last modified: 20.04.12
'****************************************************************************************
'****************************************************************************************
'..................Comments:
Imports MySql.Data.MySqlClient

Public Class Mangos_core
    Dim runfunction As New Functions
    Dim talentid As String = ""
    Dim rank As String = ""
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
        runfunction.writelog("Open SQL connection @mangos")
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
            runfunction.writelog("Failed to open SQL connection @mangos // errmsg: " & ex.ToString)
            Main.GLOBALconn.Close()
            Main.GLOBALconn.Dispose()
        End Try
        Try
            Main.GLOBALconnRealmd.Open()
        Catch ex As Exception
            runfunction.writelog("Failed to open SQL connection Realmd @mangos // errmsg: " & ex.ToString)
            Main.GLOBALconnRealmd.Close()
            Main.GLOBALconnRealmd.Dispose()
        End Try
    End Sub

    Public Sub closesql()
        runfunction.writelog("Close SQL connection @mangos")

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

    Public Sub getallchars()

        runfunction.writelog("getallchars_call @mangos")
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
                    Dim _
                        da As _
                            New MySqlDataAdapter(
                                "SELECT `username` FROM `account` WHERE last_login>='" & My.Settings.lastlogindate1 &
                                "' AND last_login<='" & My.Settings.lastlogindate2 & "' AND gmlevel='" &
                                My.Settings.gmlevel.ToString & "'", Main.GLOBALconnRealmd)
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
                                "SELECT `username` FROM `account` WHERE gmlevel='" & My.Settings.gmlevel.ToString & "'",
                                Main.GLOBALconnRealmd)
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

    Public Sub getthischar(ByVal charname As String)


        runfunction.writelog("getthischars_call @mangos for charname: " & charname)

        Dim da As New MySqlDataAdapter("SELECT guid FROM characters WHERE name='" & charname & "'", Main.GLOBALconn)
        Dim dt As New DataTable
        runfunction.writelog(
            "connection: " & Main.GLOBALconn.ConnectionString & " // connectionstate: " & Main.GLOBALconn.State.ToString)
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
            Else
                runfunction.writelog("Character not found!" & charname)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getallcharsfromaccount(ByVal accountname As String)
        runfunction.writelog("getallcharsfromaccount_call @mangos for accountname: " & accountname)
        Try
            guidlist.Clear()
        Catch ex As Exception

        End Try
        Dim accid As String =
                runfunction.runcommandRealmd("SELECT `id` FROM account WHERE `username`='" & accountname & "'", "id")
        guidlist = New List(Of String)

        runfunction.writelog("AccountId is: " & accid)

        If My.Settings.levelrangeactive = True Then

            Dim _
                da As _
                    New MySqlDataAdapter(
                        "SELECT guid FROM characters WHERE `account`='" & accid & "' AND level>='" &
                        My.Settings.levelrangemin.ToString & "' AND level<='" & My.Settings.levelrangemax & "'",
                        Main.GLOBALconn)
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
                Else
                    runfunction.writelog("No characters found")
                End If
            Catch ex As Exception

            End Try
        Else
            Dim _
                da As _
                    New MySqlDataAdapter("SELECT guid FROM characters WHERE `account`='" & accid & "'", Main.GLOBALconn)
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
                Else
                    runfunction.writelog("No characters found")
                End If
            Catch ex As Exception

            End Try
        End If
        gochars()
    End Sub

    Private Sub gochars()
        For Each guid As String In guidlist
            runfunction.writelog("gochars_call @mangos for guid: " & guid)
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
        runfunction.writelog("GetCharFromDatabase_call @mangos for charguid: " & charguid)
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
                        "SELECT gmlevel FROM account WHERE `id`='" & Main.accountid.ToString & "'", "gmlevel")))
        Main.account_access_RealmID =
            CInt(
                Val(
                    runfunction.runcommandRealmd(
                        "SELECT active_realm_id FROM account WHERE `id`='" & Main.accountid.ToString & "'", "RealmID")))
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
        runfunction.writelog("getspells_call @mangos for charguid: " & Main.char_guid.ToString)
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
                                Main.char_guid.ToString & "'", "active")
                    Dim disabled As String =
                            runfunction.runcommand(
                                "SELECT `disabled` FROM character_spell WHERE spell='" & spell & "' AND guid='" &
                                Main.char_guid.ToString & "'", "disabled")

                    Main.character_spells.Add(
                        "<spell>" & spell & "</spell><active>" & active & "</active><disabled>" & disabled &
                        "</disabled>")

                    count += 1
                Loop Until count = lastcount
            Else
                runfunction.writelog("No spells found!")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub gettalents()

        runfunction.writelog("gettalents_call @mangos")
        sdatatable.Clear()
        sdatatable.Dispose()
        sdatatable = gettable()
        Try
            Dim _
                da As _
                    New MySqlDataAdapter(
                        "SELECT talent_id FROM character_talent WHERE guid='" & Main.char_guid.ToString &
                        "' AND spec='0'", Main.GLOBALconn)
            Dim dt As New DataTable

            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim idtalent As String = readedcode
                    Dim rurrentrank As String =
                            runfunction.runcommand(
                                "SELECT current_rank FROM character_talent WHERE talent_id='" & idtalent &
                                "' AND guid='" & Main.char_guid.ToString & "' AND spec='0'", "current_rank")
                    Main.character_talent_list.Add(
                        "<spell>" & checkfield2(idtalent, rurrentrank) & "</spell><spec>0</spec>")

                    count += 1
                Loop Until count = lastcount
            Else
                runfunction.writelog("No talents found!")
            End If
            '  ("<spell>" & spell & "</spell><spec>" & spec & "</spec>")

        Catch ex As Exception

        End Try
        getspec1()
    End Sub

    Private Sub getspec1()
        runfunction.writelog("getspec1_call @mangos")
        sdatatable.Clear()
        sdatatable.Dispose()
        sdatatable = gettable()
        Try
            Dim _
                da As _
                    New MySqlDataAdapter(
                        "SELECT talent_id FROM character_talent WHERE guid='" & Main.char_guid.ToString &
                        "' AND spec='1'", Main.GLOBALconn)
            Dim dt As New DataTable

            da.Fill(dt)

            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim idtalent As String = readedcode
                    Dim rurrentrank As String =
                            runfunction.runcommand(
                                "SELECT current_rank FROM character_talent WHERE talent_id='" & idtalent &
                                "' AND guid='" & Main.char_guid.ToString & "' AND spec='1'", "current_rank")
                    Main.character_talent_list.Add(
                        "<spell>" & checkfield2(idtalent, rurrentrank) & "</spell><spec>1</spec>")

                    count += 1
                Loop Until count = lastcount
            Else
                runfunction.writelog("Talents (spec1) not found!")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function checkfield2(ByVal lID As String, ByVal rank As String) As String
        If Not executex2("TalentId", lID, CInt(Val(rank))) = "-" Then

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
        runfunction.writelog("getqueststatus_call @mangos")
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
            Else
                runfunction.writelog("No queststatus found!")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getskills()
        runfunction.writelog("getskills_call @mangos")
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
            Else
                runfunction.writelog("No skills found!")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getREPlists()
        runfunction.writelog("getREPlists_call @mangos")
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
            Else
                runfunction.writelog("Reputation could not be found!")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getactionlist()

        runfunction.writelog("getactionlist_call @mangos")
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
            Else
                runfunction.writelog("Action list not found!")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getavlists()
        runfunction.writelog("getavlists_call @mangos")
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
            Else
                runfunction.writelog("No achievements found!")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getinventoryitems()
        runfunction.writelog("getinventoryitems_call @mangos")
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
                                runfunction.runcommand(
                                    "SELECT item_template FROM character_inventory WHERE guid = '" &
                                    Main.char_guid.ToString & "' AND slot='" & tmpext.ToString & "' AND item='" & item &
                                    "'", "item_template")
                            enchantments =
                                runfunction.runcommand("SELECT `data` FROM item_instance WHERE guid = '" & item & "'",
                                                       "data")
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
                            runfunction.runcommand(
                                "SELECT item_template FROM character_inventory WHERE guid = '" & Main.char_guid.ToString &
                                "' AND slot='" & tmpext.ToString & "' AND item='" & bagguid.ToString & "'", "itemEntry")
                        enchantments =
                            runfunction.runcommand("SELECT `data` FROM item_instance WHERE guid = '" & item & "'",
                                                   "data")
                        Main.character_inventory_list.Add(
                            "<slot>" & tmpext.ToString & "</slot><bag>" & bag & "</bag><bagguid>" & bagguid &
                            "</bagguid><item>" & entryid & "</item><enchant>" & enchantments & "</enchant>")
                    End If


                    count += 1
                Loop Until count = lastcount
            Else
                runfunction.writelog("No inventory_items found!")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getglyphs()
        runfunction.writelog("getglyphs_call @mangos")
        Dim prevglyphid As Integer
        Dim glyphid As Integer
        Dim glyphname As String = ""
        Dim glyphpic As Image = My.Resources.empty
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='0' AND slot='0'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='0' AND slot='3'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='0' AND slot='5'", "glyph")))
            Application.DoEvents()
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                Application.DoEvents()
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='0' AND slot='4'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='0' AND slot='1'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='0' AND slot='2'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.gering3.Text = glyphname
                Main.minorglyph3 = glyphid.ToString
                Glyphs.gering3.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.gering3pic)
            End If
        Catch ex As Exception
            Glyphs.gering3pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='0' AND slot='6'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='0' AND slot='7'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='0' AND slot='8'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.prim3.Text = glyphname
                Main.primeglyph3 = glyphid.ToString
                Glyphs.prim3.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.prim3pic)
            End If
        Catch ex As Exception
            Glyphs.prim3pic.Image = My.Resources.empty
        End Try
    End Sub

    Public Sub getsecglyphs()
        Dim prevglyphid As Integer
        Dim glyphid As Integer
        Dim glyphname As String = ""
        Dim glyphpic As Image = My.Resources.empty
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='1' AND slot='0'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='1' AND slot='3'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='1' AND slot='5'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='1' AND slot='4'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='1' AND slot='1'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='1' AND slot='2'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.secgering3.Text = glyphname
                Main.secminorglyph3 = glyphid.ToString
                Glyphs.secgering3.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secgering3pic)
            End If
        Catch ex As Exception
            Glyphs.secgering3pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='1' AND slot='6'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='1' AND slot='7'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
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
                            "SELECT glyph from character_glyphs WHERE guid='" & characterguid.ToString &
                            "' AND spec='1' AND slot='8'", "glyph")))
            If prevglyphid > 1 Then
                glyphid = runfunction.getglyphid(prevglyphid)
                If Main.anzahldurchlaufe = 1 Then glyphname = runfunction.getnamefromid(glyphid)
                Glyphs.secprim3.Text = glyphname
                Main.secprimeglyph3 = glyphid.ToString
                Glyphs.secprim3.Visible = True
                If Main.anzahldurchlaufe = 1 Then runfunction.getimage(glyphid, Glyphs.secprim3pic)
            End If
        Catch ex As Exception
            Glyphs.secprim3pic.Image = My.Resources.empty
        End Try
    End Sub

    Private Sub saveglyphs()
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
        runfunction.writelog("getitems_call @mangos")
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
                        Val(
                            runfunction.runcommand(
                                "SELECT item_template FROM character_inventory WHERE item = '" & xentryid & "'",
                                "item_template")))
                If Main.anzahldurchlaufe = 1 Then itemname = runfunction.getnamefromid(realxentryid)
                Dim wartemal As String = ""
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
        '///// Maybe bonus at position 38 will cause trouble!
        If Main.anzahldurchlaufe = 1 Then Main.kopfvz.Text = splitstringvz(Main.kopfench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.kopfsocket1.Text = splitstringgem(Main.kopfench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.kopfsocket2.Text = splitstringgem(Main.kopfench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.kopfsocket3.Text = splitstringgem(Main.kopfench, 35)
        Main.kopfvz.Visible = True


        If Main.anzahldurchlaufe = 1 Then Main.halsvz.Text = splitstringvz(Main.halsench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.halssocket1.Text = splitstringgem(Main.halsench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.halssocket2.Text = splitstringgem(Main.halsench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.halssocket3.Text = splitstringgem(Main.halsench, 35)
        Main.halsvz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.schultervz.Text = splitstringvz(Main.schulterench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.schultersocket1.Text = splitstringgem(Main.schulterench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.schultersocket2.Text = splitstringgem(Main.schulterench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.schultersocket3.Text = splitstringgem(Main.schulterench, 35)
        Main.schultervz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.rueckenvz.Text = splitstringvz(Main.rueckenench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.rueckensocket1.Text = splitstringgem(Main.rueckenench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.rueckensocket2.Text = splitstringgem(Main.rueckenench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.rueckensocket3.Text = splitstringgem(Main.rueckenench, 35)
        Main.rueckenvz.Visible = True

        Main.brustvz.Text = splitstringvz(Main.brustench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.brustsocket1.Text = splitstringgem(Main.brustench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.brustsocket2.Text = splitstringgem(Main.brustench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.brustsocket3.Text = splitstringgem(Main.brustench, 35)
        Main.brustvz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.handgelenkevz.Text = splitstringvz(Main.handgelenkeench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.Handgelenkesocket1.Text = splitstringgem(Main.handgelenkeench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.handgelenkesocket2.Text = splitstringgem(Main.handgelenkeench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.Handgelenkesocket3.Text = splitstringgem(Main.handgelenkeench, 35)
        Main.handgelenkevz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.hauptvz.Text = splitstringvz(Main.hauptench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.Hauptsocket1.Text = splitstringgem(Main.hauptench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.Hauptsocket2.Text = splitstringgem(Main.hauptench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.hauptsocket3.Text = splitstringgem(Main.hauptench, 35)
        Main.hauptvz.Visible = True
        Main.hauptvzlabel2.Visible = True
        Main.hauptvzlabel2.Text = Main.hauptvz.Text

        If Main.anzahldurchlaufe = 1 Then Main.offvz.Text = splitstringvz(Main.offench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.Offsocket1.Text = splitstringgem(Main.offench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.Offsocket2.Text = splitstringgem(Main.offench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.offsocket3.Text = splitstringgem(Main.offench, 35)
        Main.offvz.Visible = True
        Main.offvzlabel2.Visible = True
        Main.offvzlabel2.Text = Main.offvz.Text

        If Main.anzahldurchlaufe = 1 Then Main.distanzvz.Text = splitstringvz(Main.distanzench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.Distanzsocket1.Text = splitstringgem(Main.distanzench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.Distanzsocket2.Text = splitstringgem(Main.distanzench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.distanzsocket3.Text = splitstringgem(Main.distanzench, 35)
        Main.distanzvz.Visible = True
        Main.distanzvzlabel2.Visible = True
        Main.distanzvzlabel2.Text = Main.distanzvz.Text

        If Main.anzahldurchlaufe = 1 Then Main.haendevz.Text = splitstringvz(Main.haendeench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.haendesocket1.Text = splitstringgem(Main.haendeench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.haendesocket2.Text = splitstringgem(Main.haendeench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.haendesocket3.Text = splitstringgem(Main.haendeench, 35)
        Main.haendevz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.guertelvz.Text = splitstringvz(Main.guertelench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.guertelsocket1.Text = splitstringgem(Main.guertelench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.guertelsocket2.Text = splitstringgem(Main.guertelench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.guertelsocket3.Text = splitstringgem(Main.guertelench, 35)
        Main.guertelvz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.beinevz.Text = splitstringvz(Main.beineench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.beinesocket1.Text = splitstringgem(Main.beineench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.beinesocket2.Text = splitstringgem(Main.beineench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.beinesocket3.Text = splitstringgem(Main.beineench, 35)
        Main.beinevz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.stiefelvz.Text = splitstringvz(Main.stiefelench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.stiefelsocket1.Text = splitstringgem(Main.stiefelench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.stiefelsocket2.Text = splitstringgem(Main.stiefelench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.stiefelsocket3.Text = splitstringgem(Main.stiefelench, 35)
        Main.stiefelvz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.ring1vz.Text = splitstringvz(Main.ring1ench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.Ring1socket1.Text = splitstringgem(Main.ring1ench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.ring1socket2.Text = splitstringgem(Main.ring1ench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.ring1socket3.Text = splitstringgem(Main.ring1ench, 35)
        Main.ring1vz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.ring2vz.Text = splitstringvz(Main.ring2ench, 23)
        If Main.anzahldurchlaufe = 1 Then Main.ring2socket1.Text = splitstringgem(Main.ring2ench, 29)
        If Main.anzahldurchlaufe = 1 Then Main.ring2socket2.Text = splitstringgem(Main.ring2ench, 32)
        If Main.anzahldurchlaufe = 1 Then Main.ring2socket3.Text = splitstringgem(Main.ring2ench, 35)
        Main.ring2vz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.schmuck1vz.Text = splitstringvz(Main.schmuck1ench, 23)
        Main.schmuck1vz.Visible = True

        If Main.anzahldurchlaufe = 1 Then Main.schmuck2vz.Text = splitstringvz(Main.schmuck2ench, 23)
        Main.schmuck2vz.Visible = True
    End Sub

    Public Function splitstringvz(ByVal input As String, ByVal position As Integer) As String
        Dim xpacressource As String
        Select Case Main.xpac
            Case 3
                xpacressource = My.Resources.VZ_ID_wotlk2
            Case 4
                xpacressource = My.Resources.VZ_ID_cata2
            Case Else
                xpacressource = My.Resources.VZ_ID_wotlk2
        End Select
        Try
            If input.Contains(" ") Then
                Dim parts() As String = input.Split(" "c)
                If Not parts(position - 1) = "0" Then
                    Dim quellcodeyx88 As String = xpacressource
                    Dim anfangyx88 As String = parts(position - 1) & ";"

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
        Select Case Main.xpac
            Case 3
                xpacressource = My.Resources.VZ_ID_wotlk2
            Case 4
                xpacressource = My.Resources.VZ_ID_cata2
            Case Else
                xpacressource = My.Resources.VZ_ID_wotlk2
        End Select
        Try
            Dim parts() As String = input.Split(" "c)
            If Not parts(position - 1) = "0" Then
                Dim quellcodeyx88 As String = xpacressource
                Dim anfangyx88 As String = parts(position - 1) & ";"
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

    Public Function getcharguid(ByVal charname As String) As Integer
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
        Try
            slotvar = runfunction.runcommand("SELECT `data` FROM item_instance WHERE guid='" & itemguid & "'", "data")
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
        runfunction.writelog("createnewaccounts_call @mangos with writestring: " & writestring)
        runfunction.normalsqlcommandRealmd(writestring)
    End Sub

    Public Sub create_new_account_if_not_exist(ByVal accname As String, ByVal command As String, ByVal accguid As String)
        runfunction.writelog(
            "create_new_account_if_not_exist_call @mangos with accname: " & accname & ", command: " & command &
            ", accguid : " & accguid)
        Try

            '  ("SELECT * FROM characters WHERE name = '" & charname.Text & "'")
            Dim myAdapter As New MySqlDataAdapter

            Dim sqlquery = "SELECT * FROM characters WHERE username = '" & accname & "'"
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = Main.GLOBALconn
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
        runfunction.writelog(
            "addchars_call @mangos with targetaccount: " & targetaccount & ", charactername: " & charactername &
            ", namechangeeverytime : " & namechangeeverytime.ToString)
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
                "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `position_x`, position_y, position_z, map, orientation, taximask `health` ) VALUES ( '" &
                newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                "', '0', '0', '0', '1', '0', '0', '-14305.7', '514.08', '10', '0', '4.30671', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ','1000' )")

            runfunction.normalsqlcommand("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'")
        Else
            If charexist(charactername) = True Then
                Process_Status.processreport.AppendText(
                    Now.TimeOfDay.ToString & "// Player will be asked to change charactername!" & vbNewLine)
                runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `position_x`, position_y, position_z, map, orientation, taximask, `health` ) VALUES ( '" &
                    newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '-14305.7', '514.08', '10', '0', '4.30671', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
                runfunction.normalsqlcommand(
                    "UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'")
            Else
                runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `position_x`, position_y, position_z, map, orientation, taximask, `health` ) VALUES ( '" &
                    newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '-14305.7', '514.08', '10', '0', '4.30671', '0 0 0 0 0 0 0 0 0 0 0 0 0 0 ', '1000' )")
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
        If Main.xpac >= 3 Then
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, owner_guid, data ) VALUES ( '" & newguid & "', '" & Main.coreguid &
                "', '" & newguid &
                " 1191182336 3 6948 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ' )")
        Else
            'MaNGOS 2.4.3 Core: Problem with data length, to long, remove 3 positions
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, owner_guid, data ) VALUES ( '" & newguid & "', '" & Main.coreguid &
                "', '" & newguid &
                " 1073741824 3 6948 1065353216 0 8 0 8 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ' )")
        End If
        runfunction.normalsqlcommand(
            "INSERT INTO character_inventory ( guid, bag, slot, item, item_template ) VALUES ( '" & Main.coreguid &
            "', '0', '23', '" & newguid & "', '6948')")

        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Created Character " & charactername & "!" & vbNewLine)
        Application.DoEvents()
    End Sub

    Public Sub sethome()
        runfunction.writelog("sethome_call @mangos")
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
            "adddetailedchar_call @mangos with targetaccount: " & targetaccount & ", charactername: " & charactername &
            ", namechangeeverytime : " & namechangeeverytime.ToString)
        Dim newcharguid As Integer =
                CInt(
                    Val(
                        runfunction.runcommand(
                            "SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", "guid"))) + 1
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Creating Character " & charactername & "!" & vbNewLine)
        guid = newcharguid.ToString
        Main.coreguid = newcharguid.ToString
        targetaccount = runfunction.runcommandRealmd("SELECT id FROM account WHERE username='" & targetaccount & "'",
                                                     "id")
        If namechangeeverytime = True Then
            Process_Status.processreport.AppendText(
                Now.TimeOfDay.ToString & "// Player will be asked to change charactername! : reason-nce=true" &
                vbNewLine)
            runfunction.normalsqlcommand(
                "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, `playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, watchedFaction, `health`, exploredZones, knownTitles, actionBars ) VALUES ( '" &
                newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                "', '0', '0', '0', '1', '0', '0', '" & Main.playerBytes.ToString & "', '" & Main.playerBytes2.ToString &
                "', '" & Main.playerFlags.ToString & "', '" & Main.position_x.ToString & "', '" &
                Main.position_y.ToString & "', '" & (CInt(Main.position_z) + 1).ToString & "', '" & Main.map.ToString &
                "', '4,40671', '" & Main.taximask & "', '" & Main.totaltime.ToString & "', '" & Main.leveltime.ToString &
                "', '" & Main.extra_flags & "', '" & Main.stable_slots & "', '" & Main.at_login & "', '" &
                Main.zone.ToString & "', '" & Main.chosenTitle & "', '" & Main.watchedFaction & "', '1000', '" &
                Main.exploredZones & "', '" & Main.knownTitles & "', '" & Main.actionBars & "' )")

            runfunction.normalsqlcommand("UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'")
        Else
            If charexist(charactername) = True Then
                Process_Status.processreport.AppendText(
                    Now.TimeOfDay.ToString & "// Player will be asked to change charactername!" & vbNewLine)
                runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, `playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, watchedFaction, `health`, exploredZones, knownTitles, actionBars ) VALUES ( '" &
                    newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '" & Main.playerBytes.ToString & "', '" &
                    Main.playerBytes2.ToString & "', '" & Main.playerFlags.ToString & "', '" & Main.position_x.ToString &
                    "', '" & Main.position_y.ToString & "', '" & (CInt(Main.position_z) + 1).ToString & "', '" &
                    Main.map.ToString & "', '4,40671', '" & Main.taximask & "', '" & Main.totaltime.ToString & "', '" &
                    Main.leveltime.ToString & "', '" & Main.extra_flags & "', '" & Main.stable_slots & "', '" &
                    Main.at_login & "', '" & Main.zone.ToString & "', '" & Main.chosenTitle & "', '" &
                    Main.watchedFaction & "', '1000', '" & Main.exploredZones & "', '" & Main.knownTitles & "', '" &
                    Main.actionBars & "' )")
                runfunction.normalsqlcommand(
                    "UPDATE characters SET at_login='1' WHERE guid='" & newcharguid.ToString & "'")
            Else
                runfunction.normalsqlcommand(
                    "INSERT INTO characters ( `guid`, `account`, `name`, `race`, `class`, `gender`, `level`, `xp`, `money`, `playerBytes`, `playerBytes2`, `playerFlags`, `position_x`, position_y, position_z, map, orientation, taximask, totaltime, leveltime, extra_flags, stable_slots, at_login, zone, chosenTitle, watchedFaction, `health`, exploredZones, knownTitles, actionBars ) VALUES ( '" &
                    newcharguid.ToString & "', '" & targetaccount & "', '" & charactername &
                    "', '0', '0', '0', '1', '0', '0', '" & Main.playerBytes.ToString & "', '" &
                    Main.playerBytes2.ToString & "', '" & Main.playerFlags.ToString & "', '" & Main.position_x.ToString &
                    "', '" & Main.position_y.ToString & "', '" & (CInt(Main.position_z) + 1).ToString & "', '" &
                    Main.map.ToString & "', '4,40671', '" & Main.taximask & "', '" & Main.totaltime.ToString & "', '" &
                    Main.leveltime.ToString & "', '" & Main.extra_flags & "', '" & Main.stable_slots & "', '" &
                    Main.at_login & "', '" & Main.zone.ToString & "', '" & Main.chosenTitle & "', '" &
                    Main.watchedFaction & "', '1000', '" & Main.exploredZones & "', '" & Main.knownTitles & "', '" &
                    Main.actionBars & "' )")

            End If

        End If
        setmissingcolumns(newcharguid)
        sethome()
        addaction()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Created Character " & charactername & "!" & vbNewLine)
        Application.DoEvents()
    End Sub

    Public Sub setmissingcolumns(ByVal guid As Integer)
        runfunction.normalsqlcommand(
            "UPDATE characters SET knownCurrencies='" & Main.knownCurrencies & "' WHERE guid='" & guid.ToString & "'")
        runfunction.normalsqlcommand(
            "UPDATE characters SET speccount='" & Main.speccount.ToString & "' WHERE guid='" & guid.ToString & "'")
        runfunction.normalsqlcommand(
            "UPDATE characters SET activespec='" & Main.activespec.ToString & "' WHERE guid='" & guid.ToString & "'")
    End Sub

    Public Sub requestnamechange(ByVal charname As String)
        runfunction.writelog("requestnamechange_call @mangos for character: " & charname)
        runfunction.normalsqlcommand("UPDATE characters SET at_login='1' WHERE name='" & charname & "'")
    End Sub

    Public Function charexist(ByVal charname As String) As Boolean
        runfunction.writelog("charexist_call @mangos for character: " & charname)
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
        '  Dim accguid As String = runfunction.runcommandRealmd("SELECT id FROM account WHERE username='" & accountname & "'", "id")
        runfunction.writelog("updatechars_call @mangos for character: " & charname)
        runfunction.normalsqlcommand("UPDATE characters SET at_login='1' WHERE name='" & charname & "'")
    End Sub

    Public Sub getguidfromname(ByVal charactername As String)
        runfunction.writelog("getguidfromname_call @mangos for character: " & charactername)
        guid = runfunction.runcommand("SELECT guid FROM characters WHERE name = '" & charactername & "'", "guid")
        Main.coreguid = guid
        addaction()
    End Sub

    Public Sub additems()
        runfunction.writelog("additems_call @mangos")
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
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.kopfid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.kopfid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.kopfid, "kopf", False)
            checkexist_anddelete(guid, "0", lastnumber, Main.kopfid)
        End If

        If Not Main.halsid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            halswearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.halsid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.halsid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.halsid, "hals", False)
            checkexist_anddelete(guid, "1", lastnumber, Main.halsid)
        End If
        If Not Main.schulterid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            schulterwearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.schulterid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.schulterid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.schulterid, "schulter", False)
            checkexist_anddelete(guid, "2", lastnumber, Main.schulterid)
        End If

        If Not Main.rueckenid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            rueckenwearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.rueckenid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.rueckenid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.rueckenid, "ruecken", False)
            checkexist_anddelete(guid, "14", lastnumber, Main.rueckenid)
        End If
        If Not Main.brustid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            brustwearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.brustid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.brustid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.brustid, "brust", False)
            checkexist_anddelete(guid, "4", lastnumber, Main.brustid)
        End If
        If Not Main.hemdid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            hemdwearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.hemdid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.hemdid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.hemdid, "hemd", False)
            checkexist_anddelete(guid, "3", lastnumber, Main.hemdid)
        End If
        If Not Main.wappenrockid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            wappenrockwearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.wappenrockid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.wappenrockid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.wappenrockid, "wappenrock", False)
            checkexist_anddelete(guid, "18", lastnumber, Main.wappenrockid)
        End If
        If Not Main.handgelenkeid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            handgelenkewearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.handgelenkeid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.handgelenkeid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.handgelenkeid, "handgelenke", False)
            checkexist_anddelete(guid, "8", lastnumber, Main.handgelenkeid)
        End If
        If Not Main.hauptid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            hauptwearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.hauptid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.hauptid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.hauptid, "haupt", False)
            checkexist_anddelete(guid, "15", lastnumber, Main.hauptid)
        End If
        If Not Main.offid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            offwearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.offid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.offid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.offid, "off", False)
            checkexist_anddelete(guid, "16", lastnumber, Main.offid)
        End If
        If Not Main.distanzid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            distanzwearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.distanzid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.distanzid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.distanzid, "distanz", False)
            checkexist_anddelete(guid, "17", lastnumber, Main.distanzid)
        End If
        If Not Main.haendeid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            haendewearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.haendeid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.haendeid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.haendeid, "haende", False)
            checkexist_anddelete(guid, "9", lastnumber, Main.haendeid)
        End If
        If Not Main.guertelid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            guertelwearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.guertelid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.guertelid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.guertelid, "guertel", False)
            checkexist_anddelete(guid, "5", lastnumber, Main.guertelid)
        End If
        If Not Main.beineid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            beinewearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.beineid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.beineid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.beineid, "beine", False)
            checkexist_anddelete(guid, "6", lastnumber, Main.beineid)
        End If
        If Not Main.stiefelid = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            stiefelwearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.stiefelid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.stiefelid.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.stiefelid, "stiefel", False)
            checkexist_anddelete(guid, "7", lastnumber, Main.stiefelid)
        End If
        If Not Main.ring1id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            ring1wearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.ring1id.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.ring1id.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.ring1id, "finger1", False)
            checkexist_anddelete(guid, "10", lastnumber, Main.ring1id)
        End If
        If Not Main.ring2id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            ring2wearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.ring2id.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.ring2id.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.ring2id, "finger2", False)
            checkexist_anddelete(guid, "11", lastnumber, Main.ring2id)
        End If
        If Not Main.schmuck1id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            schmuck1wearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.schmuck1id.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.schmuck1id.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.schmuck1id, "schmuck1", False)
            checkexist_anddelete(guid, "12", lastnumber, Main.schmuck1id)
        End If
        If Not Main.schmuck2id = Nothing Then
            lastnumber = (CInt(Val(lastnumber)) + 1).ToString
            schmuck2wearguid = CInt(lastnumber)
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.schmuck2id.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            Else
                'MaNGOS_one has problems with data length
                runfunction.normalsqlcommand(
                    "INSERT INTO item_instance ( guid, owner_guid, data) VALUES ( '" & lastnumber & "', '" & guid &
                    "', '" & lastnumber & " 1191182336 3 " & Main.schmuck2id.ToString &
                    " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 ')")
                '3462 0 0 3448 0 0 3535 0 0 3753
            End If
            makestring(Main.schmuck2id, "schmuck2", False)
            checkexist_anddelete(guid, "13", lastnumber, Main.schmuck2id)
        End If
        makestring(0, "", True)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Created Items!" & vbNewLine)
    End Sub

    Public Sub addglyphs(ByVal expansion As String)
        runfunction.writelog("addglyphs_call @mangos expansion: " & expansion)
        newcharguid = Main.coreguid
        guid = Main.coreguid
        checkglyphsanddelete(newcharguid)
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding Glyphs to Character: " & Main.char_name & vbNewLine)
        If expansion = "cata" Then
            If Not Main.minorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '4', '" &
                    runfunction.getglyphid2(Main.minorglyph1) & "' )")
            End If
            If Not Main.minorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '1', '" &
                    runfunction.getglyphid2(Main.minorglyph2) & "' )")
            End If
            If Not Main.minorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '2', '" &
                    runfunction.getglyphid2(Main.minorglyph3) & "' )")
            End If
            If Not Main.majorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '0', '" &
                    runfunction.getglyphid2(Main.majorglyph1) & "' )")
            End If
            If Not Main.majorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '3', '" &
                    runfunction.getglyphid2(Main.majorglyph2) & "' )")
            End If
            If Not Main.majorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '5', '" &
                    runfunction.getglyphid2(Main.majorglyph3) & "' )")
            End If
            If Not Main.primeglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '6', '" &
                    runfunction.getglyphid2(Main.primeglyph1) & "' )")
            End If
            If Not Main.primeglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '7', '" &
                    runfunction.getglyphid2(Main.primeglyph2) & "' )")
            End If
            If Not Main.primeglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '8', '" &
                    runfunction.getglyphid2(Main.primeglyph3) & "' )")
            End If


            If Not Main.secminorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '4', '" &
                    runfunction.getglyphid2(Main.secminorglyph1) & "' )")
            End If
            If Not Main.secminorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '1', '" &
                    runfunction.getglyphid2(Main.secminorglyph2) & "' )")
            End If
            If Not Main.secminorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '2', '" &
                    runfunction.getglyphid2(Main.secminorglyph3) & "' )")
            End If
            If Not Main.secmajorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '0', '" &
                    runfunction.getglyphid2(Main.secmajorglyph1) & "' )")
            End If
            If Not Main.secmajorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '3', '" &
                    runfunction.getglyphid2(Main.secmajorglyph2) & "' )")
            End If
            If Not Main.secmajorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '5', '" &
                    runfunction.getglyphid2(Main.secmajorglyph3) & "' )")
            End If
            If Not Main.secprimeglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '6', '" &
                    runfunction.getglyphid2(Main.secprimeglyph1) & "' )")
            End If
            If Not Main.secprimeglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '7', '" &
                    runfunction.getglyphid2(Main.secprimeglyph2) & "' )")
            End If
            If Not Main.secprimeglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '8', '" &
                    runfunction.getglyphid2(Main.secprimeglyph3) & "' )")
            End If
        Else
            If Not Main.minorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '4', '" &
                    runfunction.getglyphid2(Main.minorglyph1) & "' )")
            End If
            If Not Main.minorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '1', '" &
                    runfunction.getglyphid2(Main.minorglyph2) & "' )")
            End If
            If Not Main.minorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '2', '" &
                    runfunction.getglyphid2(Main.minorglyph3) & "' )")
            End If
            If Not Main.majorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '0', '" &
                    runfunction.getglyphid2(Main.majorglyph1) & "' )")
            End If
            If Not Main.majorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '3', '" &
                    runfunction.getglyphid2(Main.majorglyph2) & "' )")
            End If
            If Not Main.majorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '5', '" &
                    runfunction.getglyphid2(Main.majorglyph3) & "' )")
            End If


            If Not Main.secminorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '4', '" &
                    runfunction.getglyphid2(Main.secminorglyph1) & "' )")
            End If
            If Not Main.secminorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '1', '" &
                    runfunction.getglyphid2(Main.secminorglyph2) & "' )")
            End If
            If Not Main.secminorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '2', '" &
                    runfunction.getglyphid2(Main.secminorglyph3) & "' )")
            End If
            If Not Main.secmajorglyph1 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '0', '" &
                    runfunction.getglyphid2(Main.secmajorglyph1) & "' )")
            End If
            If Not Main.secmajorglyph2 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '3', '" &
                    runfunction.getglyphid2(Main.secmajorglyph2) & "' )")
            End If
            If Not Main.secmajorglyph3 = "" Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_glyphs ( guid, spec, slot, glyph ) VALUES ( '" & guid & "', '0', '5', '" &
                    runfunction.getglyphid2(Main.secmajorglyph3) & "' )")
            End If

        End If
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Added Glyphs!" & vbNewLine)
    End Sub

    Public Sub setgender(ByVal gender As String)
        runfunction.writelog("setgender_call @mangos gender: " & gender)
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting gender for Character: " & Main.char_name & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand("UPDATE `characters` SET gender='" & gender & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setlevel()
        runfunction.writelog("setlevel_call @mangos")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting Level for Character: " & Main.char_name & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET level='" & Main.char_level.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setrace()
        runfunction.writelog("setrace_call @mangos")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting race for Character: " & Main.char_name & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET race='" & Main.char_race.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setclass()
        runfunction.writelog("setclass_call @mangos")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting class for Character: " & Main.char_name & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET `class`='" & Main.char_class.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setalternatelevel(ByVal alternatelevel As String)
        runfunction.writelog("setalternatelevel_call @mangos with a.level: " & alternatelevel)
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting alternative level for Character: " & Main.char_name & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand("UPDATE `characters` SET level='" & alternatelevel & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub setgold(ByVal amount As String)
        runfunction.writelog("setgold_call @mangos with amount: " & amount)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding gold..." & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand(
            "UPDATE `characters` SET money='" & (CInt(Val(amount))*10000).ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub addgold(ByVal amount As Integer)
        runfunction.writelog("addgold_call @mangos with amount: " & amount)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding gold..." & vbNewLine)
        guid = Main.coreguid
        runfunction.normalsqlcommand("UPDATE `characters` SET money='" & amount.ToString & "' WHERE guid='" & guid & "'")
    End Sub

    Public Sub addtalents()
        runfunction.writelog("addtalents_call @mangos")
        sdatatable.Clear()
        sdatatable.Dispose()
        sdatatable = gettable()
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting Talents for Character: " & Main.char_name & vbNewLine)
        Dim talentlist As String = ""
        Dim talentlist2 As String = ""
        'talentid/rank
        For Each talentstring As String In Main.character_talent_list
            talentid = checkfield(splitlist(talentstring, "spell"))
            Dim spec As String = splitlist(talentstring, "spec")
            If spec = "0" Then
                If talentlist.Contains(talentid) Then
                    If talentlist.Contains(talentid & "rank5") Then

                    ElseIf talentlist.Contains(talentid & "rank4") Then
                        If CInt(Val(rank)) <= 4 Then
                        Else
                            runfunction.normalsqlcommand(
                                "UPDATE character_talent SET current_rank='" & rank & "' WHERE guid='" & Main.coreguid &
                                "' AND talent_id='" & talentid & "' AND spec='0'")
                            talentlist = talentlist & " " & talentid & "rank" & rank
                        End If
                    ElseIf talentlist.Contains(talentid & "rank3") Then
                        If CInt(Val(rank)) <= 3 Then
                        Else
                            runfunction.normalsqlcommand(
                                "UPDATE character_talent SET current_rank='" & rank & "' WHERE guid='" & Main.coreguid &
                                "' AND talent_id='" & talentid & "' AND spec='0'")
                            talentlist = talentlist & " " & talentid & "rank" & rank
                        End If
                    ElseIf talentlist.Contains(talentid & "rank2") Then
                        If CInt(Val(rank)) <= 2 Then
                        Else
                            runfunction.normalsqlcommand(
                                "UPDATE character_talent SET current_rank='" & rank & "' WHERE guid='" & Main.coreguid &
                                "' AND talent_id='" & talentid & "' AND spec='0'")
                            talentlist = talentlist & " " & talentid & "rank" & rank
                        End If
                    ElseIf talentlist.Contains(talentid & "rank1") Then
                        If CInt(Val(rank)) <= 1 Then
                        Else
                            runfunction.normalsqlcommand(
                                "UPDATE character_talent SET current_rank='" & rank & "' WHERE guid='" & Main.coreguid &
                                "' AND talent_id='" & talentid & "' AND spec='0'")
                            talentlist = talentlist & " " & talentid & "rank" & rank
                        End If
                    Else

                    End If
                Else
                    runfunction.normalsqlcommand(
                        "INSERT INTO character_talent ( guid, talent_id, current_rank, spec ) VALUES ( '" &
                        Main.coreguid & "', '" & talentid & "', '" & rank & "', '0' )")

                    talentlist = talentlist & " " & talentid & "rank" & rank

                End If
            Else
                'spec 1

                If talentlist2.Contains(talentid) Then
                    If talentlist2.Contains(talentid & "rank5") Then

                    ElseIf talentlist2.Contains(talentid & "rank4") Then
                        If CInt(Val(rank)) <= 4 Then
                        Else
                            runfunction.normalsqlcommand(
                                "UPDATE character_talent SET current_rank='" & rank & "' WHERE guid='" & Main.coreguid &
                                "' AND talent_id='" & talentid & "' AND spec='1'")
                            talentlist2 = talentlist2 & " " & talentid & "rank" & rank
                        End If
                    ElseIf talentlist2.Contains(talentid & "rank3") Then
                        If CInt(Val(rank)) <= 3 Then
                        Else
                            runfunction.normalsqlcommand(
                                "UPDATE character_talent SET current_rank='" & rank & "' WHERE guid='" & Main.coreguid &
                                "' AND talent_id='" & talentid & "' AND spec='1'")
                            talentlist2 = talentlist2 & " " & talentid & "rank" & rank
                        End If
                    ElseIf talentlist2.Contains(talentid & "rank2") Then
                        If CInt(Val(rank)) <= 2 Then
                        Else
                            runfunction.normalsqlcommand(
                                "UPDATE character_talent SET current_rank='" & rank & "' WHERE guid='" & Main.coreguid &
                                "' AND talent_id='" & talentid & "' AND spec='1'")
                            talentlist2 = talentlist2 & " " & talentid & "rank" & rank
                        End If
                    ElseIf talentlist2.Contains(talentid & "rank1") Then
                        If CInt(Val(rank)) <= 1 Then
                        Else
                            runfunction.normalsqlcommand(
                                "UPDATE character_talent SET current_rank='" & rank & "' WHERE guid='" & Main.coreguid &
                                "' AND talent_id='" & talentid & "' AND spec='1'")
                            talentlist2 = talentlist2 & " " & talentid & "rank" & rank
                        End If
                    Else

                    End If
                Else
                    runfunction.normalsqlcommand(
                        "INSERT INTO character_talent ( guid, talent_id, current_rank, spec ) VALUES ( '" &
                        Main.coreguid & "', '" & talentid & "', '" & rank & "', '1' )")

                    talentlist2 = talentlist2 & " " & talentid & "rank" & rank

                End If
            End If


            '  ("<spell>" & spell & "</spell><spec>" & spec & "</spec>")
        Next
    End Sub

    Private Function checkfield(ByVal lID As String) As String
        If Not executex("Rang1", lID) = "-" Then
            rank = "1"
            Return (executex("Rang1", lID))
        ElseIf Not executex("Rang2", lID) = "-" Then
            rank = "2"
            Return (executex("Rang2", lID))
        ElseIf Not executex("Rang3", lID) = "-" Then
            rank = "3"
            Return (executex("Rang3", lID))
        ElseIf Not executex("Rang4", lID) = "-" Then
            rank = "4"
            Return (executex("Rang4", lID))
        ElseIf Not executex("Rang5", lID) = "-" Then
            rank = "5"
            Return (executex("Rang5", lID))
        Else
            rank = "0"
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
        runfunction.writelog("setqueststatus_call @mangos")
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
        runfunction.writelog("addachievements_call @mangos")
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
        runfunction.writelog("addskills_call @mangos")
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
        runfunction.writelog("addspells_call @mangos")
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
        runfunction.writelog("addreputation_call @mangos")
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
        runfunction.writelog("addaction_call @mangos")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Setting up actionbars for Character: " & Main.char_name & vbNewLine)
        For Each actionstring As String In Main.character_action_list
            If Main.xpac >= 3 Then
                runfunction.normalsqlcommand(
                    "INSERT INTO character_action ( guid, spec, `button`, `action`, `type` ) VALUES ( '" & Main.coreguid &
                    "', '" & splitlist(actionstring, "spec") & "', '" & splitlist(actionstring, "button") & "', '" &
                    splitlist(actionstring, "action") & "', '" & splitlist(actionstring, "type") & "')")

            Else
                runfunction.normalsqlcommand(
                    "INSERT INTO character_action ( guid, `button`, `action`, `type` ) VALUES ( '" & Main.coreguid &
                    "', '" & splitlist(actionstring, "button") & "', '" & splitlist(actionstring, "action") & "', '" &
                    splitlist(actionstring, "type") & "')")

            End If

            ' "<action>" & action & "</action><spec>" & spec & "</spec><button>" & gbutton & "</button><type>" & atype & "</atype>"
        Next
    End Sub

    Public Sub addinventory()
        runfunction.writelog("addinventory_call @mangos")
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
                "INSERT INTO item_instance ( guid, owner_guid, data ) VALUES ( '" & newguid & "', '" & Main.coreguid &
                "', '" &
                splitenchstring(splitlist(inventorystring, "enchant"), CInt(newguid), splitlist(inventorystring, "item")) &
                "' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_inventory ( guid, bag, slot, item, item_template ) VALUES ( '" & Main.coreguid &
                "', '" & bag & "', '" & splitlist(inventorystring, "slot") & "', '" & newguid & "', '" &
                splitlist(inventorystring, "item") & "')")


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
                        "SELECT item FROM character_inventory WHERE item_template='" & bag & "' AND guid='" &
                        Main.coreguid & "'", "item")
            runfunction.normalsqlcommand(
                "INSERT INTO item_instance ( guid, owner_guid, data ) VALUES ( '" & newguid & "', '" & Main.coreguid &
                "', '" &
                splitenchstring(splitlist(inventorystring, "enchant"), CInt(newguid), splitlist(inventorystring, "item")) &
                "' )")
            runfunction.normalsqlcommand(
                "INSERT INTO character_inventory ( guid, bag, slot, item, item_template ) VALUES ( '" & Main.coreguid &
                "', '" & newbagguid & "', '" & splitlist(inventorystring, "slot") & "', '" & newguid & "', '" &
                splitlist(inventorystring, "item") & "')")


            ' <slot>" & tmpext.ToString & "</slot><bag>" & bag & "</bag><item>" & entryid & "</item><enchant>" & enchantments & "</enchant>
        Next
    End Sub

    Private Function splitenchstring(ByVal enchstring As String, ByVal guid As Integer, ByVal entry As String) As String
        Dim Anzahl As Integer = UBound(enchstring.Split(CChar(" ")))
        Dim normalenchstring As String =
                "0 1191182336 3 0 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 3753 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 "
        If Main.xpac < 3 Then

            normalenchstring =
                "0 1191182336 3 0 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1 0 0 0 0 0 0 0 0 0 0 0 0 3753 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 "
            'May cause error: data length

        End If
        '0:guid 3:entry 22:vz 28:gem1 31:gem2 34 gem3
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

                    parts3(22) = parts2(0)
                    normalenchstring = String.Join(" ", parts3)
                ElseIf parts2(2) = "1" Then
                    'gem1
                    Dim input As String = normalenchstring
                    Dim parts3() As String = input.Split(" "c)

                    parts3(28) = parts2(0)
                    normalenchstring = String.Join(" ", parts3)
                ElseIf parts2(2) = "2" Then
                    'gem2
                    Dim input As String = normalenchstring
                    Dim parts3() As String = input.Split(" "c)

                    parts3(31) = parts2(0)
                    normalenchstring = String.Join(" ", parts3)
                ElseIf parts2(2) = "3" Then
                    'gem3
                    Dim input As String = normalenchstring
                    Dim parts3() As String = input.Split(" "c)

                    parts3(34) = parts2(0)
                    normalenchstring = String.Join(" ", parts3)
                End If


                startcounter += 1
            Loop Until startcounter = excounter
            Dim input2 As String = normalenchstring
            Dim parts4() As String = input2.Split(" "c)
            Dim output2 As String
            parts4(0) = guid.ToString
            parts4(4) = entry.ToString
            output2 = String.Join(" ", parts4)
            Return output2
        Else
            If Anzahl > 45 Then
                'mangos
                If Main.xpac >= 3 Then
                    If Anzahl > 63 Then
                        'Sourcecore: WotLK/Cata
                        Dim input As String = enchstring
                        Dim parts() As String = input.Split(" "c)
                        Dim output As String
                        parts(0) = guid.ToString
                        parts(4) = entry.ToString
                        output = String.Join(" ", parts)
                        Return output
                    Else
                        'Sourcecore: TBC/Vanilla
                        Dim input As String = enchstring
                        Dim parts() As String = input.Split(" "c)
                        Dim output As String
                        parts(0) = guid.ToString
                        parts(4) = entry.ToString
                        parts(40) = "0 0 0"
                        'needs validation
                        output = String.Join(" ", parts)
                        Return output
                    End If
                Else
                    If Anzahl > 63 Then
                        'Sourcecore: WotLK/Cata
                        Dim input As String = enchstring
                        input = input.Replace("0 0 0 0 0 0 0 0", "0 0 0 0 0")
                        'needs validation
                        Dim parts() As String = input.Split(" "c)
                        Dim output As String
                        parts(0) = guid.ToString
                        parts(4) = entry.ToString
                        output = String.Join(" ", parts)
                        Return output
                    Else
                        'Sourcecore: TBC/Vanilla
                        Dim input As String = enchstring
                        Dim parts() As String = input.Split(" "c)
                        Dim output As String
                        parts(0) = guid.ToString
                        parts(4) = entry.ToString
                        output = String.Join(" ", parts)
                        Return output
                    End If
                End If


            Else
                'trinity
                Dim input As String = "0 " & enchstring
                Dim parts() As String = input.Split(" "c)
                Dim output As String
                parts(0) = "<start>"
                parts(14) = "<end>"
                output = String.Join(" ", parts)

                Dim xXquellcodeyx88 As String = output
                Dim xXanfangyx88 As String = "<start>"
                Dim xXendeyx88 As String = "<end>"
                Dim xXquellcodeSplityx88 As String
                xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                Dim resultString As String
                If Main.xpac >= 3 Then
                    resultString = guid & " 1191182336 3 " & entry & " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1" &
                                   xXquellcodeSplityx88 &
                                   "0 0 3753 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 "
                Else
                    resultString = guid & " 1191182336 3 " & entry & " 1065353216 0 1 0 1 0 0 0 0 0 1 0 0 0 0 0 0 1" &
                                   xXquellcodeSplityx88 & "0 0 3753 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 100 100 0 0 "
                    ' may cause error: data length
                End If

                Return resultString
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
        runfunction.writelog("addench_call @mangos")
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Adding item enchantments..." & vbNewLine)
        Application.DoEvents()
        If Not Main.kopfench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" & splitenchstring(Main.kopfench, kopfwearguid, Main.kopfid.ToString) &
                "' WHERE guid='" & kopfwearguid & "'")
        If Not Main.halsench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" & splitenchstring(Main.halsench, halswearguid, Main.halsid.ToString) &
                "' WHERE guid='" & halswearguid & "'")
        If Not Main.schulterench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.schulterench, schulterwearguid, Main.schulterid.ToString) & "' WHERE guid='" &
                schulterwearguid & "'")
        If Not Main.rueckenench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.rueckenench, rueckenwearguid, Main.rueckenid.ToString) & "' WHERE guid='" &
                rueckenwearguid & "'")
        If Not Main.brustench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.brustench, brustwearguid, Main.brustid.ToString) & "' WHERE guid='" & brustwearguid &
                "'")
        If Not Main.hemdench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" & splitenchstring(Main.hemdench, hemdwearguid, Main.hemdid.ToString) &
                "' WHERE guid='" & hemdwearguid & "'")
        If Not Main.wappenrockench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.wappenrockench, wappenrockwearguid, Main.wappenrockid.ToString) & "' WHERE guid='" &
                wappenrockwearguid & "'")
        If Not Main.handgelenkeench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.handgelenkeench, handgelenkewearguid, Main.handgelenkeid.ToString) &
                "' WHERE guid='" & handgelenkewearguid & "'")
        If Not Main.haendeench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.haendeench, haendewearguid, Main.haendeid.ToString) & "' WHERE guid='" &
                haendewearguid & "'")
        If Not Main.hauptench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.hauptench, hauptwearguid, Main.hauptid.ToString) & "' WHERE guid='" & hauptwearguid &
                "'")
        If Not Main.offench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" & splitenchstring(Main.offench, offwearguid, Main.offid.ToString) &
                "' WHERE guid='" & offwearguid & "'")
        If Not Main.distanzench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.distanzench, distanzwearguid, Main.distanzid.ToString) & "' WHERE guid='" &
                distanzwearguid & "'")
        If Not Main.guertelench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.guertelench, guertelwearguid, Main.guertelid.ToString) & "' WHERE guid='" &
                guertelwearguid & "'")
        If Not Main.beineench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.beineench, beinewearguid, Main.beineid.ToString) & "' WHERE guid='" & beinewearguid &
                "'")
        If Not Main.stiefelench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.stiefelench, stiefelwearguid, Main.stiefelid.ToString) & "' WHERE guid='" &
                stiefelwearguid & "'")
        If Not Main.ring1ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.ring1ench, ring1wearguid, Main.ring1id.ToString) & "' WHERE guid='" & ring1wearguid &
                "'")
        If Not Main.ring2ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.ring2ench, ring2wearguid, Main.ring2id.ToString) & "' WHERE guid='" & ring2wearguid &
                "'")
        If Not Main.schmuck1ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.schmuck1ench, schmuck1wearguid, Main.schmuck1id.ToString) & "' WHERE guid='" &
                schmuck1wearguid & "'")
        If Not Main.schmuck2ench = "" Then _
            runfunction.normalsqlcommand(
                "UPDATE `item_instance` SET data='" &
                splitenchstring(Main.schmuck2ench, schmuck2wearguid, Main.schmuck2id.ToString) & "' WHERE guid='" &
                schmuck2wearguid & "'")
    End Sub

    Public Sub addgems()
        runfunction.writelog("addgems_call @mangos")
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
            beltinsert("3729", guertelwearguid.ToString, 41)
            '41 may be wrong!...
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
            beltinsert("3729", guertelwearguid.ToString, 41)
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
            beltinsert("3729", guertelwearguid.ToString, 41)
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
        runfunction.writelog("addenchantments_call @mangos")
        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "// Adding character enchantments..." & vbNewLine)
        If Main.kopfvzid > 0 Then vzinsert(Main.kopfvzid, kopfwearguid, 1)
        If Main.halsvzid > 0 Then vzinsert(Main.halsvzid, halswearguid, 1)
        If Main.schultervzid > 0 Then vzinsert(Main.schultervzid, schulterwearguid, 1)
        If Main.rueckenvzid > 0 Then vzinsert(Main.rueckenvzid, rueckenwearguid, 1)
        If Main.brustvzid > 0 Then vzinsert(Main.brustvzid, brustwearguid, 1)
        If Main.handgelenkevzid > 0 Then vzinsert(Main.handgelenkevzid, handgelenkewearguid, 1)
        If Main.haendevzid > 0 Then vzinsert(Main.haendevzid, haendewearguid, 1)
        If Main.guertelvzid > 0 Then vzinsert(Main.guertelvzid, guertelwearguid, 1)
        If Main.beinevzid > 0 Then vzinsert(Main.beinevzid, beinewearguid, 1)
        If Main.stiefelvzid > 0 Then vzinsert(Main.stiefelvzid, stiefelwearguid, 1)
        If Main.ring1vzid > 0 Then vzinsert(Main.ring1vzid, ring1wearguid, 1)
        If Main.ring2vzid > 0 Then vzinsert(Main.ring2vzid, ring2wearguid, 1)
        If Main.schmuck1vzid > 0 Then vzinsert(Main.schmuck1vzid, schmuck1wearguid, 1)
        If Main.schmuck2vzid > 0 Then vzinsert(Main.schmuck2vzid, schmuck2wearguid, 1)
        If Main.hauptvzid > 0 Then vzinsert(Main.hauptvzid, hauptwearguid, 1)
        If Main.offvzid > 0 Then vzinsert(Main.offvzid, offwearguid, 1)
        If Main.distanzvzid > 0 Then vzinsert(Main.distanzvzid, distanzwearguid, 1)
    End Sub

    Public Sub addpvp()
        runfunction.writelog("addpvp_call @mangos")
        Process_Status.processreport.AppendText(
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
        Dim enchantmenttext As String
        Try
            enchantmenttext = runfunction.runcommand("SELECT data FROM item_instance WHERE guid='" & itemguid & "'",
                                                     "data")
            Dim input As String = enchantmenttext
            Dim parts() As String = input.Split(" "c)
            Dim output As String
            parts(position + 21) = (runfunction.getgemeffectid(socketid)).ToString
            output = String.Join(" ", parts)
            Try
                runfunction.normalsqlcommand(
                    "UPDATE `item_instance` SET data='" & output & "' WHERE guid = '" & itemguid & "'")
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Public Sub beltinsert(ByVal beltid As String, ByVal itemguid As String, ByVal position As Integer)
        Dim enchantmenttext As String
        Try
            enchantmenttext = runfunction.runcommand("SELECT data FROM item_instance WHERE guid='" & itemguid & "'",
                                                     "data")
            Dim input As String = enchantmenttext
            Dim parts() As String = input.Split(" "c)
            Dim output As String
            parts(position) = beltid
            output = String.Join(" ", parts)
            Try
                runfunction.normalsqlcommand(
                    "UPDATE `item_instance` SET data='" & output & "' WHERE guid = '" & itemguid & "'")
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Public Sub vzinsert(ByVal vzid As Integer, ByVal itemguid As Integer, ByVal position As Integer)
        Dim enchantmenttext As String
        Try
            enchantmenttext = runfunction.runcommand("SELECT data FROM item_instance WHERE guid='" & itemguid & "'",
                                                     "data")
            Dim input As String = enchantmenttext
            Dim parts() As String = input.Split(" "c)
            Dim output As String
            parts(position + 21) = (runfunction.getvzeffectid(runfunction.getvzeffectname2(vzid))).ToString
            output = String.Join(" ", parts)
            Try
                runfunction.normalsqlcommand(
                    "UPDATE `item_instance` SET data='" & output & "' WHERE guid = '" & itemguid & "'")
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub checkexist_anddelete(ByVal xguid As String, ByVal xslot As String, ByVal item As String,
                                     ByVal itementry As Integer)
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
                    "INSERT INTO character_inventory ( guid, bag, slot, item, item_template ) VALUES ( '" & xguid &
                    "', '0', '" & xslot & "', '" & item & "', '" & itementry.ToString & "' )")

            Else
                myData.Close()
                runfunction.normalsqlcommand(
                    "DELETE FROM character_inventory WHERE guid = '" & xguid & "' AND slot = '" & xslot & "'", False)
                runfunction.normalsqlcommand(
                    "INSERT INTO character_inventory ( guid, bag, slot, item, item_template ) VALUES ( '" & xguid &
                    "', '0', '" & xslot & "', '" & item & "', '" & itementry.ToString & "' )")
            End If

        Catch

        End Try
    End Sub

    Private Sub checkglyphsanddelete(ByVal playerguid As String)

        Try
            runfunction.normalsqlcommand("DELETE FROM character_glyphs WHERE guid = '" & playerguid & "", False)
        Catch ex As Exception

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
