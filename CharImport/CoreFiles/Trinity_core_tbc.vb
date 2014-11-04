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
        Main.MainInstance.setallempty()
        Main.MainInstance.anzahldurchlaufe += 1
        Main.MainInstance.char_guid = CInt(Val(charguid))
        characterguid = CInt(Val(charguid))
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Race from Database..." & vbNewLine)
        Application.DoEvents()
        Main.MainInstance.char_race = CInt(Val(runfunction.runcommand("SELECT race FROM characters WHERE guid='" & charguid & "'", "race")))
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Class from Database..." & vbNewLine)
        Application.DoEvents()
        Main.MainInstance.char_class = CInt(Val(runfunction.runcommand("SELECT class FROM characters WHERE guid='" & charguid & "'", "class")))
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Gender from Database..." & vbNewLine)
        Application.DoEvents()
        Main.MainInstance.char_gender = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 242), ' ', -1) AS UNSIGNED) AS `gender` FROM `characters` WHERE guid='" & charguid & "'", "gender")))
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Level from Database..." & vbNewLine)
        Application.DoEvents()
        Main.MainInstance.char_level = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 35), ' ', -1) AS UNSIGNED) AS `level` FROM `characters` WHERE guid='" & charguid & "'", "level")))
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Table..." & vbNewLine)
        Application.DoEvents()
        Main.MainInstance.char_name = runfunction.runcommand("SELECT name FROM characters WHERE guid='" & charguid & "'", "name")
        Main.MainInstance.accountid = CInt(Val(runfunction.runcommand("SELECT account FROM characters WHERE guid='" & charguid & "'", "account")))
        Main.MainInstance.player_money = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1398), ' ', -1) AS UNSIGNED) AS `money` FROM `characters` WHERE guid='" & charguid & "'", "money")))
        Main.MainInstance.playerBytes = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 240), ' ', -1) AS UNSIGNED) AS `playerBytes` FROM `characters` WHERE guid='" & charguid & "'", "playerBytes")))
        Main.MainInstance.playerBytes2 = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 241), ' ', -1) AS UNSIGNED) AS `playerBytes2` FROM `characters` WHERE guid='" & charguid & "'", "playerBytes2")))
        Main.MainInstance.playerFlags = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 237), ' ', -1) AS UNSIGNED) AS `playerFlags` FROM `characters` WHERE guid='" & charguid & "'", "playerFlags")))
        Main.MainInstance.char_xp = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 927), ' ', -1) AS UNSIGNED) AS `xp` FROM `characters` WHERE guid='" & charguid & "'", "xp")))
        Main.MainInstance.position_x = CDbl((runfunction.runcommand("SELECT position_x FROM characters WHERE guid='" & charguid & "'", "position_x")))
        Main.MainInstance.position_y = CDbl((runfunction.runcommand("SELECT position_y FROM characters WHERE guid='" & charguid & "'", "position_y")))
        Main.MainInstance.position_z = CDbl((runfunction.runcommand("SELECT position_z FROM characters WHERE guid='" & charguid & "'", "position_z")))
        Main.MainInstance.map = CInt(Val(runfunction.runcommand("SELECT map FROM characters WHERE guid='" & charguid & "'", "map")))
        Main.MainInstance.orientation = CDbl((runfunction.runcommand("SELECT orientation FROM characters WHERE guid='" & charguid & "'", "orientation")))
        Main.MainInstance.taximask = runfunction.runcommand("SELECT taximask FROM characters WHERE guid='" & charguid & "'", "taximask")
        Main.MainInstance.cinematic = CInt(Val(runfunction.runcommand("SELECT cinematic FROM characters WHERE guid='" & charguid & "'", "cinematic")))
        Main.MainInstance.totaltime = CInt(Val(runfunction.runcommand("SELECT totaltime FROM characters WHERE guid='" & charguid & "'", "totaltime")))
        Main.MainInstance.leveltime = CInt(Val(runfunction.runcommand("SELECT leveltime FROM characters WHERE guid='" & charguid & "'", "leveltime")))
        Main.MainInstance.extra_flags = runfunction.runcommand("SELECT extra_flags FROM characters WHERE guid='" & charguid & "'", "extra_flags")
        Main.MainInstance.stable_slots = runfunction.runcommand("SELECT stable_slots FROM characters WHERE guid='" & charguid & "'", "stable_slots")
        Main.MainInstance.at_login = runfunction.runcommand("SELECT at_login FROM characters WHERE guid='" & charguid & "'", "at_login")
        Main.MainInstance.zone = CInt(Val(runfunction.runcommand("SELECT zone FROM characters WHERE guid='" & charguid & "'", "zone")))
        Main.MainInstance.arenaPoints = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1500), ' ', -1) AS UNSIGNED) AS `arenaPoints` FROM `characters` WHERE guid='" & charguid & "'", "arenaPoints")))
        Main.MainInstance.totalHonorPoints = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1499), ' ', -1) AS UNSIGNED) AS `totalHonorPoints` FROM `characters` WHERE guid='" & charguid & "'", "totalHonorPoints")))
        Main.MainInstance.totalKills = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1451), ' ', -1) AS UNSIGNED) AS `totalHonorPoints` FROM `characters` WHERE guid='" & charguid & "'", "totalKills")))
        Main.MainInstance.chosenTitle = runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 649), ' ', -1) AS UNSIGNED) AS `chosenTitle` FROM `characters` WHERE guid='" & charguid & "'", "chosenTitle")
        Main.MainInstance.watchedFaction = runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1456), ' ', -1) AS UNSIGNED) AS `watchedFaction` FROM `characters` WHERE guid='" & charguid & "'", "watchedFaction")
        Main.MainInstance.health = CInt(Val(runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 23), ' ', -1) AS UNSIGNED) AS `health` FROM `characters` WHERE guid='" & charguid & "'", "health")))
        Main.MainInstance.exploredZones = runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 1333), ' ', -1) AS UNSIGNED) AS `exploredZones` FROM `characters` WHERE guid='" & charguid & "'", "exploredZones")
        Main.MainInstance.knownTitles = runfunction.runcommand("SELECT CAST(SUBSTRING_INDEX(SUBSTRING_INDEX(`data`, ' ', 925), ' ', -1) AS UNSIGNED) AS `knownTitles` FROM `characters` WHERE guid='" & charguid & "'", "knownTitles")
        Main.MainInstance.accountname = runfunction.runcommandRealmd("SELECT username FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "username")
        Main.MainInstance.sha_pass_hash = runfunction.runcommandRealmd("SELECT sha_pass_hash FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "sha_pass_hash")
        Main.MainInstance.sessionkey = runfunction.runcommandRealmd("SELECT sessionkey FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "sessionkey")
        Main.MainInstance.account_v = runfunction.runcommandRealmd("SELECT v FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "v")
        Main.MainInstance.account_s = runfunction.runcommandRealmd("SELECT s FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "s")
        Main.MainInstance.email = runfunction.runcommandRealmd("SELECT email FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "email")
        Main.MainInstance.joindate = runfunction.runcommandRealmd("SELECT joindate FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "joindate")
        Main.MainInstance.expansion = CInt(Val(runfunction.runcommandRealmd("SELECT expansion FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "expansion"))) '//2=tbc?
        Main.MainInstance.locale = CInt(Val(runfunction.runcommandRealmd("SELECT locale FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "locale")))
        Main.MainInstance.account_access_gmlevel = CInt(Val(runfunction.runcommandRealmd("SELECT gmlevel FROM account WHERE `id`='" & Main.MainInstance.accountid.ToString & "'", "gmlevel")))
        Main.MainInstance.level.Text = Main.MainInstance.char_name & ", " & Main.MainInstance.char_level & ", " & Main.MainInstance.char_race & ", " & Main.MainInstance.char_class
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading Character Homebind from Database..." & vbNewLine)
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
            Case Else : End Select
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
            Case Else : End Select
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
        Main.MainInstance.datasets += 1
        Dim addtataset As New CIUFile
        addtataset.adddataset()
        Application.DoEvents()
    End Sub

    Public Overrides Sub getqueststatus()
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
                    Dim status As String = runfunction.runcommand("SELECT `status` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" & Main.MainInstance.char_guid.ToString & "'", "status")
                    Dim explored As String = runfunction.runcommand("SELECT `explored` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" & Main.MainInstance.char_guid.ToString & "'", "explored")
                    Dim timer As String = runfunction.runcommand("SELECT `timer` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" & Main.MainInstance.char_guid.ToString & "'", "timer")
                    Dim rewarded As String = runfunction.runcommand("SELECT `rewarded` FROM character_queststatus WHERE quest='" & quest & "' AND guid='" & Main.MainInstance.char_guid.ToString & "'", "rewarded")
                    If Not CInt(rewarded) = 1 Then
                        Main.MainInstance.character_queststatus.Add("<quest>" & quest & "</quest><status>" & status & "</status><explored>" & explored & "</explored><timer>" & timer & "</timer>")
                    Else
                        Main.MainInstance.finished_quests = Main.MainInstance.finished_quests & quest & ","
                    End If
                    count += 1
                Loop Until count = lastcount
            End If
        Catch : End Try
    End Sub
End Class
