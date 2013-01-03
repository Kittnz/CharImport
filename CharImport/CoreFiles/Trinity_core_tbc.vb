'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* Trinity_core_tbc contains several functions to implement the character
'* and account information into an TrinityCore tbc compatible database.
'*
'* Developed by Alcanmage/megasus

Imports MySql.Data.MySqlClient
Public Class Trinity_core_tbc
    Inherits Trinity_core

    Dim runfunction As New Functions
    Public Overrides Sub GetCharFromDatabase(ByVal charguid As String)

        runfunction.writelog("GetCharFromDatabase_call with charguid: " & charguid)
        Main.setallempty()
        Main.anzahldurchlaufe += 1
        Main.char_guid = CInt(Val(charguid))
        characterguid = CInt(Val(charguid))
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Race from Database..." & vbNewLine)
        Application.DoEvents()
        Main.char_race = CInt(Val(runfunction.runcommand("SELECT race FROM characters WHERE guid='" & charguid & "'", "race")))
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Class from Database..." & vbNewLine)
        Application.DoEvents()
        Main.char_class = CInt(Val(runfunction.runcommand("SELECT class FROM characters WHERE guid='" & charguid & "'", "class")))
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Gender from Database..." & vbNewLine)
        Application.DoEvents()
        Main.char_gender = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 242), ' ', -1) AS UNSIGNED) AS `gender` FROM `characters` WHERE guid='" & charguid & "'", "gender")))
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Level from Database..." & vbNewLine)
        Application.DoEvents()
        Main.char_level = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 35), ' ', -1) AS UNSIGNED) AS `level` FROM `characters` WHERE guid='" & charguid & "'", "level")))
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Table..." & vbNewLine)
        Application.DoEvents()
        Main.char_name = runfunction.runcommand("SELECT name FROM characters WHERE guid='" & charguid & "'", "name")
        Main.accountid = CInt(Val(runfunction.runcommand("SELECT account FROM characters WHERE guid='" & charguid & "'", "account")))
        Main.player_money = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1398), ' ', -1) AS UNSIGNED) AS `money` FROM `characters` WHERE guid='" & charguid & "'", "money")))
        Main.playerBytes = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 240), ' ', -1) AS UNSIGNED) AS `playerBytes` FROM `characters` WHERE guid='" & charguid & "'", "playerBytes")))
        Main.playerBytes2 = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 241), ' ', -1) AS UNSIGNED) AS `playerBytes2` FROM `characters` WHERE guid='" & charguid & "'", "playerBytes2")))
        Main.playerFlags = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 237), ' ', -1) AS UNSIGNED) AS `playerFlags` FROM `characters` WHERE guid='" & charguid & "'", "playerFlags")))
        Main.char_xp = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 927), ' ', -1) AS UNSIGNED) AS `xp` FROM `characters` WHERE guid='" & charguid & "'", "xp")))
        Main.position_x = runfunction.runcommand("SELECT position_x FROM characters WHERE guid='" & charguid & "'", "position_x")
        Main.position_y = runfunction.runcommand("SELECT position_y FROM characters WHERE guid='" & charguid & "'", "position_y")
        Main.position_z = runfunction.runcommand("SELECT position_z FROM characters WHERE guid='" & charguid & "'", "position_z")
        Main.map = CInt(Val(runfunction.runcommand("SELECT map FROM characters WHERE guid='" & charguid & "'", "map")))
        Main.orientation = runfunction.runcommand("SELECT orientation FROM characters WHERE guid='" & charguid & "'", "orientation")
        Main.taximask = runfunction.runcommand("SELECT taximask FROM characters WHERE guid='" & charguid & "'", "taximask")
        Main.cinematic = CInt(Val(runfunction.runcommand("SELECT cinematic FROM characters WHERE guid='" & charguid & "'", "cinematic")))
        Main.totaltime = CInt(Val(runfunction.runcommand("SELECT totaltime FROM characters WHERE guid='" & charguid & "'", "totaltime")))
        Main.leveltime = CInt(Val(runfunction.runcommand("SELECT leveltime FROM characters WHERE guid='" & charguid & "'", "leveltime")))
        Main.extra_flags = runfunction.runcommand("SELECT extra_flags FROM characters WHERE guid='" & charguid & "'", "extra_flags")
        Main.stable_slots = runfunction.runcommand("SELECT stable_slots FROM characters WHERE guid='" & charguid & "'", "stable_slots")
        Main.at_login = runfunction.runcommand("SELECT at_login FROM characters WHERE guid='" & charguid & "'", "at_login")
        Main.zone = CInt(Val(runfunction.runcommand("SELECT zone FROM characters WHERE guid='" & charguid & "'", "zone")))
        Main.arenaPoints = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1500), ' ', -1) AS UNSIGNED) AS `arenaPoints` FROM `characters` WHERE guid='" & charguid & "'", "arenaPoints")))
        Main.totalHonorPoints = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1499), ' ', -1) AS UNSIGNED) AS `totalHonorPoints` FROM `characters` WHERE guid='" & charguid & "'", "totalHonorPoints")))
        Main.totalKills = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1451), ' ', -1) AS UNSIGNED) AS `totalHonorPoints` FROM `characters` WHERE guid='" & charguid & "'", "totalKills")))
        Main.chosenTitle = runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 649), ' ', -1) AS UNSIGNED) AS `chosenTitle` FROM `characters` WHERE guid='" & charguid & "'", "chosenTitle")
        Main.watchedFaction = runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1456), ' ', -1) AS UNSIGNED) AS `watchedFaction` FROM `characters` WHERE guid='" & charguid & "'", "watchedFaction")
        Main.health = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 23), ' ', -1) AS UNSIGNED) AS `health` FROM `characters` WHERE guid='" & charguid & "'", "health")))
        Main.exploredZones = runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1333), ' ', -1) AS UNSIGNED) AS `exploredZones` FROM `characters` WHERE guid='" & charguid & "'", "exploredZones")
        Main.knownTitles = runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 925), ' ', -1) AS UNSIGNED) AS `knownTitles` FROM `characters` WHERE guid='" & charguid & "'", "knownTitles")
        Main.accountname = runfunction.runcommandRealmd("SELECT username FROM account WHERE `id`='" & Main.accountid.ToString & "'", "username")
        Main.sha_pass_hash = runfunction.runcommandRealmd("SELECT sha_pass_hash FROM account WHERE `id`='" & Main.accountid.ToString & "'", "sha_pass_hash")
        Main.sessionkey = runfunction.runcommandRealmd("SELECT sessionkey FROM account WHERE `id`='" & Main.accountid.ToString & "'", "sessionkey")
        Main.account_v = runfunction.runcommandRealmd("SELECT v FROM account WHERE `id`='" & Main.accountid.ToString & "'", "v")
        Main.account_s = runfunction.runcommandRealmd("SELECT s FROM account WHERE `id`='" & Main.accountid.ToString & "'", "s")
        Main.email = runfunction.runcommandRealmd("SELECT email FROM account WHERE `id`='" & Main.accountid.ToString & "'", "email")
        Main.joindate = runfunction.runcommandRealmd("SELECT joindate FROM account WHERE `id`='" & Main.accountid.ToString & "'", "joindate")
        Main.expansion = CInt(Val(runfunction.runcommandRealmd("SELECT expansion FROM account WHERE `id`='" & Main.accountid.ToString & "'", "expansion"))) '//2=tbc?
        Main.locale = CInt(Val(runfunction.runcommandRealmd("SELECT locale FROM account WHERE `id`='" & Main.accountid.ToString & "'", "locale")))
        Main.account_access_gmlevel = CInt(Val(runfunction.runcommandRealmd("SELECT gmlevel FROM account WHERE `id`='" & Main.accountid.ToString & "'", "gmlevel")))
        Main.level.Text = Main.char_name & ", " & Main.char_level & ", " & Main.char_race & ", " & Main.char_class
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Homebind from Database..." & vbNewLine)
        Main.level.Text = Main.char_name & ", " & Main.char_level & ", "
        Select Case Main.char_race
            Case 1
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Mensch" Else Main.level.Text = Main.level.Text & "Human"
            Case 2
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Orc" Else Main.level.Text = Main.level.Text & "Orc"
            Case 3
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Zwerg" Else Main.level.Text = Main.level.Text & "Dwarf"
            Case 4
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Nachtelf" Else Main.level.Text = Main.level.Text & "Night Elf"
            Case 5
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Untot" Else Main.level.Text = Main.level.Text & "Undead"
            Case 6
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Tauren" Else Main.level.Text = Main.level.Text & "Tauren"
            Case 7
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Gnom" Else Main.level.Text = Main.level.Text & "Gnome"
            Case 8
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Troll" Else Main.level.Text = Main.level.Text & "Troll"
            Case 9
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Goblin" Else Main.level.Text = Main.level.Text & "Goblin"
            Case 10
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Blutelf" Else Main.level.Text = Main.level.Text & "Blood Elf"
            Case 11
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Draenei" Else Main.level.Text = Main.level.Text & "Draenei"
            Case 22
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Worgen" Else Main.level.Text = Main.level.Text & "Worgen"
            Case Else : End Select
        Main.level.Text = Main.level.Text & ", "
        Select Case Main.char_class
            Case 1
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Krieger" Else Main.level.Text = Main.level.Text & "Warrior"
            Case 2
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Paladin" Else Main.level.Text = Main.level.Text & "Paladin"
            Case 3
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Jäger" Else Main.level.Text = Main.level.Text & "Hunter"
            Case 4
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Schurke" Else Main.level.Text = Main.level.Text & "Rogue"
            Case 5
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Priester" Else Main.level.Text = Main.level.Text & "Priest"
            Case 6
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Todesritter" Else Main.level.Text = Main.level.Text & "Death Knight"
            Case 7
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Schamane" Else Main.level.Text = Main.level.Text & "Shaman"
            Case 8
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Magier" Else Main.level.Text = Main.level.Text & "Mage"
            Case 9
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Hexenmeister" Else Main.level.Text = Main.level.Text & "Warlock"
            Case 11
                If My.Settings.language = "de" Then Main.level.Text = Main.level.Text & "Druide" Else Main.level.Text = Main.level.Text & "Druid"
            Case Else : End Select
        Process_Status.processreport.AppendText(
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
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Spells from Database..." & vbNewLine)
        Application.DoEvents()
        getspells()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Talents from Database..." & vbNewLine)
        Application.DoEvents()
        gettalents()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Skills from Database..." & vbNewLine)
        Application.DoEvents()
        getskills()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Reputation from Database..." & vbNewLine)
        Application.DoEvents()
        getREPlists()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Action from Database..." & vbNewLine)
        Application.DoEvents()
        getactionlist()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Questlog from Database..." & vbNewLine)
        Application.DoEvents()
        getqueststatus()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Inventory from Database..." & vbNewLine)
        Application.DoEvents()
        getinventoryitems()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Items from Database..." & vbNewLine)
        Application.DoEvents()
        getitems()
        handleenchantments()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Character loaded!..." & vbNewLine)
        Application.DoEvents()
        saveglyphs()
        Main.datasets += 1
        Dim addtataset As New CIUFile
        addtataset.adddataset()
        Application.DoEvents()
    End Sub

    Public Overrides Sub getqueststatus()
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
                    Dim status As String = runfunction.runcommand("SELECT `status` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" & Main.char_guid.ToString & "'", "status")
                    Dim explored As String = runfunction.runcommand("SELECT `explored` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" & Main.char_guid.ToString & "'", "explored")
                    Dim timer As String = runfunction.runcommand("SELECT `timer` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" & Main.char_guid.ToString & "'", "timer")
                    Dim rewarded As String = runfunction.runcommand("SELECT `rewarded` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" & Main.char_guid.ToString & "'", "rewarded")
                    If Not CInt(rewarded) = 1 Then
                        Main.character_queststatus.Add("<quest>" & quest & "</quest><status>" & status & "</status><explored>" & explored & "</explored><timer>" & timer & "</timer>")
                    Else
                        Main.finished_quests = Main.finished_quests & quest & ","
                    End If
                    count += 1
                Loop Until count = lastcount
            End If
        Catch : End Try
     End Sub
End Class
