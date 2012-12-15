'Copyright (C) 2011-2012 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* Core_Check_Trinity contains several methods to check if the connected
'* database is a valid TrinityCore database and makes adjustments
'*
'* Developed by Alcanmage/megasus

Imports MySql.Data.MySqlClient

Public Class Core_Check_Trinity
    Dim reporttext As RichTextBox = Database_Check.report
    Dim reporttext2 As RichTextBox = Process_Status.processreport

    Dim ServerString2 As String = Main.ServerStringCheck
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim errorstring As String = ""
    Dim tmpstring As String = ""

    Public Sub begincheck(ByVal startcond As Integer)
        Main.tableschema = ""
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "// Begin Core Check..." & vbNewLine)
        Application.DoEvents()
        Main.nowgoon = False
        Main.startcond = startcond
        check_characters()
        If Main.xpac >= 3 Then
            check_character_achievement()

        End If
        check_character_action()
        check_character_homebind()
        check_character_inventory()
        check_character_queststatus()
        check_character_reputation()
        check_character_skills()
        check_character_spell()
        If Main.xpac >= 3 Then
            check_character_talent()
        Else : End If
        check_item_instance()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Core Check completed!" & vbNewLine)
        Application.DoEvents()
        If errorstring = "" Then
            Main.nowgoon = True
            If Main.startcond = 14 Then
                Armory2Database.button4click()
            ElseIf Main.startcond = 23 Then
                Database_Interface.button3click()
            ElseIf Main.startcond = 24 Then
                Database_Interface.button4click()
            ElseIf Main.startcond = 34 Then
                Database2Database.button4click()
            ElseIf Main.startcond = 42 Then
                Connect.button2click()
            ElseIf Main.startcond = 22 Then

                Main.ausgangsformat = 1
                Database2Database.Show()
            Else
            End If
        Else
            errorstring = "Determined core: TrinityCore" & vbNewLine & errorstring
            Database_Check.Show()
            reporttext.Text = errorstring

            Application.DoEvents()
        End If
    End Sub

    Private Sub check_characters()
        tmpstring = ""
        If columnexist("guid", "characters") = False Then _
            tmpstring = tmpstring & "// Column guid in characters does not exist!" & vbNewLine
        If columnexist("account", "characters") = False Then _
            tmpstring = tmpstring & "// Column account in characters does not exist!" & vbNewLine
        If columnexist("name", "characters") = False Then _
            tmpstring = tmpstring & "// Column name in characters does not exist!" & vbNewLine
        If columnexist("race", "characters") = False Then _
            tmpstring = tmpstring & "// Column race in characters does not exist!" & vbNewLine
        If columnexist("class", "characters") = False Then _
            tmpstring = tmpstring & "// Column class in characters does not exist!" & vbNewLine
        If columnexist("gender", "characters") = False Then _
            tmpstring = tmpstring & "// Column gender in characters does not exist!" & vbNewLine
        If columnexist("level", "characters") = False Then _
            tmpstring = tmpstring & "// Column level in characters does not exist!" & vbNewLine
        If columnexist("money", "characters") = False Then _
            tmpstring = tmpstring & "// Column money in characters does not exist!" & vbNewLine
        If columnexist("playerBytes", "characters") = False Then _
            tmpstring = tmpstring & "// Column playerBytes in characters does not exist!" & vbNewLine
        If columnexist("playerBytes2", "characters") = False Then _
            tmpstring = tmpstring & "// Column playerBytes2 in characters does not exist!" & vbNewLine
        If columnexist("playerFlags", "characters") = False Then _
            tmpstring = tmpstring & "// Column playerFlags in characters does not exist!" & vbNewLine
        If columnexist("position_x", "characters") = False Then _
            tmpstring = tmpstring & "// Column position_x in characters does not exist!" & vbNewLine
        If columnexist("position_y", "characters") = False Then _
            tmpstring = tmpstring & "// Column position_y in characters does not exist!" & vbNewLine
        If columnexist("position_z", "characters") = False Then _
            tmpstring = tmpstring & "// Column position_z in characters does not exist!" & vbNewLine
        If columnexist("map", "characters") = False Then _
            tmpstring = tmpstring & "// Column map in characters does not exist!" & vbNewLine
        If columnexist("orientation", "characters") = False Then _
            tmpstring = tmpstring & "// Column orientation in characters does not exist!" & vbNewLine
        If columnexist("taximask", "characters") = False Then _
            tmpstring = tmpstring & "// Column taximask in characters does not exist!" & vbNewLine
        If columnexist("online", "characters") = False Then _
            tmpstring = tmpstring & "// Column online in characters does not exist!" & vbNewLine
        If columnexist("cinematic", "characters") = False Then _
            tmpstring = tmpstring & "// Column cinematic in characters does not exist!" & vbNewLine
        If columnexist("totaltime", "characters") = False Then _
            tmpstring = tmpstring & "// Column totaltime in characters does not exist!" & vbNewLine
        If columnexist("leveltime", "characters") = False Then _
            tmpstring = tmpstring & "// Column leveltime in characters does not exist!" & vbNewLine
        If columnexist("extra_flags", "characters") = False Then _
            tmpstring = tmpstring & "// Column extra_flags in characters does not exist!" & vbNewLine
        If columnexist("stable_slots", "characters") = False Then _
            tmpstring = tmpstring & "// Column stable_slots in characters does not exist!" & vbNewLine
        If columnexist("at_login", "characters") = False Then _
            tmpstring = tmpstring & "// Column at_login in characters does not exist!" & vbNewLine
        If columnexist("zone", "characters") = False Then _
            tmpstring = tmpstring & "// Column zone in characters does not exist!" & vbNewLine
        If Main.xpac < 4 Then
            If columnexist("arenaPoints", "characters") = False Then _
                tmpstring = tmpstring & "// Column arenaPoints in characters does not exist!" & vbNewLine
            If columnexist("totalHonorPoints", "characters") = False Then _
                tmpstring = tmpstring & "// Column totalHonorPoints in characters does not exist!" & vbNewLine
        End If
        If columnexist("totalKills", "characters") = False Then _
            tmpstring = tmpstring & "// Column totalKills in characters does not exist!" & vbNewLine
        If columnexist("chosenTitle", "characters") = False Then _
            tmpstring = tmpstring & "// Column chosenTitle in characters does not exist!" & vbNewLine
        If columnexist("knownCurrencies", "characters") = False Then _
            tmpstring = tmpstring & "// Column knownCurrencies in characters does not exist!" & vbNewLine
        If columnexist("watchedFaction", "characters") = False Then _
            tmpstring = tmpstring & "// Column watchedFaction in characters does not exist!" & vbNewLine
        If columnexist("specCount", "characters") = False Then _
            tmpstring = tmpstring & "// Column specCount in characters does not exist!" & vbNewLine
        If columnexist("activeSpec", "characters") = False Then _
            tmpstring = tmpstring & "// Column activeSpec in characters does not exist!" & vbNewLine
        If columnexist("exploredZones", "characters") = False Then _
            tmpstring = tmpstring & "// Column exploredZones in characters does not exist!" & vbNewLine
        If columnexist("equipmentCache", "characters") = False Then _
            tmpstring = tmpstring & "// Column equipmentCache in characters does not exist!" & vbNewLine
        If columnexist("knownTitles", "characters") = False Then _
            tmpstring = tmpstring & "// Column knownTitles in characters does not exist!" & vbNewLine
        If columnexist("actionBars", "characters") = False Then _
            tmpstring = tmpstring & "// Column actionBars in characters does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("characters")
        errorstring = errorstring & tmpstring
    End Sub

    Private Sub check_item_instance()
        tmpstring = ""
        If columnexist("itemEntry", "item_instance") = False Then _
            tmpstring = tmpstring & "// Column itemEntry in item_instance does not exist!" & vbNewLine
        If columnexist("owner_guid", "item_instance") = False Then _
            tmpstring = tmpstring & "// Column owner_guid in item_instance does not exist!" & vbNewLine
        If columnexist("enchantments", "item_instance") = False Then _
            tmpstring = tmpstring & "// Column enchantments in item_instance does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("item_instance")
        errorstring = errorstring & tmpstring
    End Sub

    Private Sub check_character_talent()
        tmpstring = ""
        If columnexist("spell", "character_talent") = False Then _
            tmpstring = tmpstring & "// Column spell in character_talent does not exist!" & vbNewLine
        If columnexist("spec", "character_talent") = False Then _
            tmpstring = tmpstring & "// Column spec in character_talent does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_talent")
        errorstring = errorstring & tmpstring
    End Sub

    Private Sub check_character_spell()
        tmpstring = ""
        If columnexist("spell", "character_spell") = False Then _
            tmpstring = tmpstring & "// Column spell in character_spell does not exist!" & vbNewLine
        If columnexist("active", "character_spell") = False Then _
            tmpstring = tmpstring & "// Column active in character_spell does not exist!" & vbNewLine
        If columnexist("disabled", "character_spell") = False Then _
            tmpstring = tmpstring & "// Column disabled in character_spell does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_spell")
        errorstring = errorstring & tmpstring
    End Sub

    Private Sub check_character_skills()
        tmpstring = ""
        If columnexist("skill", "character_skills") = False Then _
            tmpstring = tmpstring & "// Column skill in character_skills does not exist!" & vbNewLine
        If columnexist("value", "character_skills") = False Then _
            tmpstring = tmpstring & "// Column value in character_skills does not exist!" & vbNewLine
        If columnexist("max", "character_skills") = False Then _
            tmpstring = tmpstring & "// Column max in character_skills does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_skills")
        errorstring = errorstring & tmpstring
    End Sub

    Private Sub check_character_reputation()
        tmpstring = ""
        If columnexist("faction", "character_reputation") = False Then _
            tmpstring = tmpstring & "// Column faction in character_reputation does not exist!" & vbNewLine
        If columnexist("standing", "character_reputation") = False Then _
            tmpstring = tmpstring & "// Column standing in character_reputation does not exist!" & vbNewLine
        If columnexist("flags", "character_reputation") = False Then _
            tmpstring = tmpstring & "// Column flags in character_reputation does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_reputation")
        errorstring = errorstring & tmpstring
    End Sub

    Private Sub check_character_queststatus()
        tmpstring = ""
        If columnexist("quest", "character_queststatus") = False Then _
            tmpstring = tmpstring & "// Column quest in character_queststatus does not exist!" & vbNewLine
        If columnexist("status", "character_queststatus") = False Then _
            tmpstring = tmpstring & "// Column status in character_queststatus does not exist!" & vbNewLine
        If columnexist("explored", "character_queststatus") = False Then _
            tmpstring = tmpstring & "// Column explored in character_queststatus does not exist!" & vbNewLine
        If columnexist("timer", "character_queststatus") = False Then _
            tmpstring = tmpstring & "// Column timer in character_queststatus does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_queststatus")
        errorstring = errorstring & tmpstring
    End Sub

    Private Sub check_character_inventory()
        tmpstring = ""
        If columnexist("bag", "character_inventory") = False Then _
            tmpstring = tmpstring & "// Column bag in character_inventory does not exist!" & vbNewLine
        If columnexist("slot", "character_inventory") = False Then _
            tmpstring = tmpstring & "// Column slot in character_inventory does not exist!" & vbNewLine
        If columnexist("item", "character_inventory") = False Then _
            tmpstring = tmpstring & "// Column item in character_inventory does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_inventory")
        errorstring = errorstring & tmpstring
    End Sub

    Private Sub check_character_homebind()
        tmpstring = ""
        Main.homebind_map = "map"
        If columnexist("map", "character_homebind") = False Then
            Main.homebind_map = "mapId"
            If columnexist("mapId", "character_homebind") = False Then
                tmpstring = tmpstring & "// Column map/mapId in character_homebind does not exist!" & vbNewLine
            End If
        End If
        Main.homebind_zone = "zone"
        If columnexist("zone", "character_homebind") = False Then
            Main.homebind_zone = "zoneId"
            If columnexist("zoneId", "character_homebind") = False Then
                tmpstring = tmpstring & "// Column zone/zoneId in character_homebind does not exist!" & vbNewLine
            End If
        End If
        Main.homebind_posx = "position_x"
        If columnexist("position_x", "character_homebind") = False Then
            Main.homebind_posx = "posX"
            If columnexist("posX", "character_homebind") = False Then
                tmpstring = tmpstring & "// Column position_x/posX in character_homebind does not exist!" & vbNewLine
            End If
        End If
        Main.homebind_posy = "position_y"
        If columnexist("position_y", "character_homebind") = False Then
            Main.homebind_posy = "posY"
            If columnexist("posY", "character_homebind") = False Then
                tmpstring = tmpstring & "// Column position_y/posY in character_homebind does not exist!" & vbNewLine
            End If
        End If
        Main.homebind_posz = "position_z"
        If columnexist("position_z", "character_homebind") = False Then
            Main.homebind_posz = "posZ"
            If columnexist("posZ", "character_homebind") = False Then
                tmpstring = tmpstring & "// Column position_z/posZ in character_homebind does not exist!" & vbNewLine
            End If
        End If
        If Not tmpstring = "" Then gettableschema("character_homebind")
        errorstring = errorstring & tmpstring
    End Sub

    Private Sub check_character_action()
        tmpstring = ""
        If columnexist("spec", "character_action") = False Then _
            tmpstring = tmpstring & "// Column spec in character_action does not exist!" & vbNewLine
        If columnexist("button", "character_action") = False Then _
            tmpstring = tmpstring & "// Column button in character_action does not exist!" & vbNewLine
        If columnexist("action", "character_action") = False Then _
            tmpstring = tmpstring & "// Column action in character_action does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_action")
        errorstring = errorstring & tmpstring
    End Sub

    Private Sub check_character_achievement()
        tmpstring = ""
        If columnexist("achievement", "character_achievement") = False Then _
            tmpstring = tmpstring & "// Column achievement in character_achievement does not exist!" & vbNewLine
        If columnexist("date", "character_achievement") = False Then _
            tmpstring = tmpstring & "// Column date in character_achievement does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_achievement")
        errorstring = errorstring & tmpstring
    End Sub

    Private Function columnexist(ByVal spalte As String, ByVal table As String) As Boolean
        Try
            SQLConnection.Close()

            SQLConnection.Dispose()
        Catch ex As Exception

        End Try
        ServerString2 = Main.ServerStringCheck
        Dim myAdapter As New MySqlDataAdapter
        SQLConnection.ConnectionString = ServerString2
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

    Private Sub gettableschema(ByVal table As String)
        Main.tableschema = Main.tableschema & "######## " & table & " ########" & vbNewLine
        Dim conn As New MySqlConnection(Main.ServerStringInfo)
        Dim _
            da As _
                New MySqlDataAdapter(
                    "SELECT COLUMN_NAME FROM COLUMNS WHERE TABLE_NAME='" & table & "' AND TABLE_SCHEMA='" &
                    Main.characterdbname & "'", conn)
        Dim dt As New DataTable
        Try
            conn.Open()
        Catch ex As Exception
            conn.Close()
            conn.Dispose()
        End Try
        Try
            da.Fill(dt)
            Try
                conn.Close()
                conn.Dispose()
            Catch :
            End Try
            Dim lastcount As Integer = CInt(Val(dt.Rows.Count.ToString))
            Dim count As Integer = 0
            If Not lastcount = 0 Then
                Do
                    Dim readedcode As String = (dt.Rows(count).Item(0)).ToString
                    Dim column As String = readedcode
                    Main.tableschema = Main.tableschema & column & vbNewLine
                    count += 1
                Loop Until count = lastcount
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
