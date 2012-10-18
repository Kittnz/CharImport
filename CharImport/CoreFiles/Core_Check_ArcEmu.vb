Imports MySql.Data.MySqlClient
Imports System.Net
Imports System.Threading
Imports System.Text

Public Class Core_Check_ArcEmu
    Dim reporttext As RichTextBox = Database_Check.report
    Dim reporttext2 As RichTextBox = Process_Status.processreport

    Dim ServerString2 As String = Main.ServerStringCheck
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim errorstring As String = ""
    Dim tmpstring As String = ""

    Public Sub begincheck(ByVal startcond As Integer)
        Main.tableschema = ""
        'Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Begin Core Check..." & vbNewLine)
        'Application.DoEvents()
        Main.nowgoon = False
        Main.startcond = startcond
        'check_characters()
        'If Main.xpac >= 3 Then
        '    check_character_achievement()

        'End If
        'check_character_action()
        'check_character_homebind()
        'check_character_inventory()
        'check_character_queststatus()
        'check_character_reputation()
        'check_character_skills()
        'check_character_spell()
        'check_character_talent()
        'check_item_instance()
        'Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Core Check completed!" & vbNewLine)
        'Application.DoEvents()
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

            Database_Check.Show()
            reporttext.Text = errorstring

            Application.DoEvents()
        End If
    End Sub
    Private Sub check_characters()
        tmpstring = ""
        If columnexist("guid", "characters") = False Then tmpstring = tmpstring & "// Column guid in characters does not exist!" & vbNewLine
        If columnexist("account", "characters") = False Then tmpstring = tmpstring & "// Column account in characters does not exist!" & vbNewLine
        If columnexist("name", "characters") = False Then tmpstring = tmpstring & "// Column name in characters does not exist!" & vbNewLine
        If columnexist("race", "characters") = False Then tmpstring = tmpstring & "// Column race in characters does not exist!" & vbNewLine
        If columnexist("class", "characters") = False Then tmpstring = tmpstring & "// Column class in characters does not exist!" & vbNewLine
        If columnexist("gender", "characters") = False Then tmpstring = tmpstring & "// Column gender in characters does not exist!" & vbNewLine
        If columnexist("level", "characters") = False Then tmpstring = tmpstring & "// Column level in characters does not exist!" & vbNewLine
        If columnexist("money", "characters") = False Then tmpstring = tmpstring & "// Column money in characters does not exist!" & vbNewLine
        If columnexist("playerBytes", "characters") = False Then tmpstring = tmpstring & "// Column playerBytes in characters does not exist!" & vbNewLine
        If columnexist("playerBytes2", "characters") = False Then tmpstring = tmpstring & "// Column playerBytes2 in characters does not exist!" & vbNewLine
        If columnexist("playerFlags", "characters") = False Then tmpstring = tmpstring & "// Column playerFlags in characters does not exist!" & vbNewLine
        If columnexist("position_x", "characters") = False Then tmpstring = tmpstring & "// Column position_x in characters does not exist!" & vbNewLine
        If columnexist("position_y", "characters") = False Then tmpstring = tmpstring & "// Column position_y in characters does not exist!" & vbNewLine
        If columnexist("position_z", "characters") = False Then tmpstring = tmpstring & "// Column position_z in characters does not exist!" & vbNewLine
        If columnexist("map", "characters") = False Then tmpstring = tmpstring & "// Column map in characters does not exist!" & vbNewLine
        If columnexist("orientation", "characters") = False Then tmpstring = tmpstring & "// Column orientation in characters does not exist!" & vbNewLine
        If columnexist("taximask", "characters") = False Then tmpstring = tmpstring & "// Column taximask in characters does not exist!" & vbNewLine
        If columnexist("online", "characters") = False Then tmpstring = tmpstring & "// Column online in characters does not exist!" & vbNewLine
        If columnexist("cinematic", "characters") = False Then tmpstring = tmpstring & "// Column cinematic in characters does not exist!" & vbNewLine
        If columnexist("totaltime", "characters") = False Then tmpstring = tmpstring & "// Column totaltime in characters does not exist!" & vbNewLine
        If columnexist("leveltime", "characters") = False Then tmpstring = tmpstring & "// Column leveltime in characters does not exist!" & vbNewLine
        If columnexist("extra_flags", "characters") = False Then tmpstring = tmpstring & "// Column extra_flags in characters does not exist!" & vbNewLine
        If columnexist("stable_slots", "characters") = False Then tmpstring = tmpstring & "// Column stable_slots in characters does not exist!" & vbNewLine
        If columnexist("at_login", "characters") = False Then tmpstring = tmpstring & "// Column at_login in characters does not exist!" & vbNewLine
        If columnexist("zone", "characters") = False Then tmpstring = tmpstring & "// Column zone in characters does not exist!" & vbNewLine
        If columnexist("arenaPoints", "characters") = False Then tmpstring = tmpstring & "// Column arenaPoints in characters does not exist!" & vbNewLine
        If columnexist("totalHonorPoints", "characters") = False Then tmpstring = tmpstring & "// Column totalHonorPoints in characters does not exist!" & vbNewLine
        If columnexist("totalKills", "characters") = False Then tmpstring = tmpstring & "// Column totalKills in characters does not exist!" & vbNewLine
        If columnexist("chosenTitle", "characters") = False Then tmpstring = tmpstring & "// Column chosenTitle in characters does not exist!" & vbNewLine
        If columnexist("knownCurrencies", "characters") = False Then tmpstring = tmpstring & "// Column knownCurrencies in characters does not exist!" & vbNewLine
        If columnexist("watchedFaction", "characters") = False Then tmpstring = tmpstring & "// Column watchedFaction in characters does not exist!" & vbNewLine
        If columnexist("specCount", "characters") = False Then tmpstring = tmpstring & "// Column specCount in characters does not exist!" & vbNewLine
        If columnexist("activeSpec", "characters") = False Then tmpstring = tmpstring & "// Column activeSpec in characters does not exist!" & vbNewLine
        If columnexist("exploredZones", "characters") = False Then tmpstring = tmpstring & "// Column exploredZones in characters does not exist!" & vbNewLine
        If columnexist("equipmentCache", "characters") = False Then tmpstring = tmpstring & "// Column equipmentCache in characters does not exist!" & vbNewLine
        If columnexist("knownTitles", "characters") = False Then tmpstring = tmpstring & "// Column knownTitles in characters does not exist!" & vbNewLine
        If columnexist("actionBars", "characters") = False Then tmpstring = tmpstring & "// Column actionBars in characters does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("characters")
        errorstring = errorstring & tmpstring
    End Sub
    Private Sub check_item_instance()
        tmpstring = ""
        If columnexist("guid", "playeritems") = False Then tmpstring = tmpstring & "// Column guid in item_instance does not exist!" & vbNewLine
        If columnexist("owner_guid", "playeritems") = False Then tmpstring = tmpstring & "// Column owner_guid in item_instance does not exist!" & vbNewLine
        If columnexist("data", "playeritems") = False Then tmpstring = tmpstring & "// Column data in item_instance does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("playeritems")
        errorstring = errorstring & tmpstring
    End Sub
    Private Sub check_character_talent()
        tmpstring = ""
        If columnexist("guid", "character_talent") = False Then tmpstring = tmpstring & "// Column guid in character_talent does not exist!" & vbNewLine
        If columnexist("talent_id", "character_talent") = False Then tmpstring = tmpstring & "// Column talent_id in character_talent does not exist!" & vbNewLine
        If columnexist("current_rank", "character_talent") = False Then tmpstring = tmpstring & "// Column current_rank in character_talent does not exist!" & vbNewLine
        If columnexist("spec", "character_talent") = False Then tmpstring = tmpstring & "// Column spec in character_talent does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_talent")
        errorstring = errorstring & tmpstring
    End Sub
    Private Sub check_character_spell()
        tmpstring = ""
        If columnexist("guid", "playerspells") = False Then tmpstring = tmpstring & "// Column guid in character_spell does not exist!" & vbNewLine
        If columnexist("spell", "playerspells") = False Then tmpstring = tmpstring & "// Column spell in character_spell does not exist!" & vbNewLine
        If columnexist("active", "playerspells") = False Then tmpstring = tmpstring & "// Column active in character_spell does not exist!" & vbNewLine
        If columnexist("disabled", "playerspells") = False Then tmpstring = tmpstring & "// Column disabled in character_spell does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("playerspells")
        errorstring = errorstring & tmpstring
    End Sub
    Private Sub check_character_skills()
        tmpstring = ""
        If columnexist("guid", "playerskills") = False Then tmpstring = tmpstring & "// Column guid in character_skills does not exist!" & vbNewLine
        If columnexist("skill", "playerskills") = False Then tmpstring = tmpstring & "// Column skill in character_skills does not exist!" & vbNewLine
        If columnexist("value", "playerskills") = False Then tmpstring = tmpstring & "// Column value in character_skills does not exist!" & vbNewLine
        If columnexist("max", "playerskills") = False Then tmpstring = tmpstring & "// Column max in character_skills does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_skills")
        errorstring = errorstring & tmpstring
    End Sub
    Private Sub check_character_reputation()
        tmpstring = ""
        If columnexist("guid", "character_reputation") = False Then tmpstring = tmpstring & "// Column guid in character_reputation does not exist!" & vbNewLine
        If columnexist("faction", "character_reputation") = False Then tmpstring = tmpstring & "// Column faction in character_reputation does not exist!" & vbNewLine
        If columnexist("standing", "character_reputation") = False Then tmpstring = tmpstring & "// Column standing in character_reputation does not exist!" & vbNewLine
        If columnexist("flags", "character_reputation") = False Then tmpstring = tmpstring & "// Column flags in character_reputation does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_reputation")
        errorstring = errorstring & tmpstring
    End Sub
    Private Sub check_character_queststatus()
        tmpstring = ""
        If columnexist("guid", "questlog") = False Then tmpstring = tmpstring & "// Column guid in character_queststatus does not exist!" & vbNewLine
        If columnexist("quest", "questlog") = False Then tmpstring = tmpstring & "// Column quest in character_queststatus does not exist!" & vbNewLine
        If columnexist("status", "questlog") = False Then tmpstring = tmpstring & "// Column status in character_queststatus does not exist!" & vbNewLine
        If columnexist("explored", "questlog") = False Then tmpstring = tmpstring & "// Column explored in character_queststatus does not exist!" & vbNewLine
        If columnexist("timer", "questlog") = False Then tmpstring = tmpstring & "// Column timer in character_queststatus does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("questlog")
        errorstring = errorstring & tmpstring
    End Sub
    Private Sub check_character_inventory()
        tmpstring = ""
        If columnexist("guid", "character_inventory") = False Then tmpstring = tmpstring & "// Column guid in character_inventory does not exist!" & vbNewLine
        If columnexist("bag", "character_inventory") = False Then tmpstring = tmpstring & "// Column bag in character_inventory does not exist!" & vbNewLine
        If columnexist("slot", "character_inventory") = False Then tmpstring = tmpstring & "// Column slot in character_inventory does not exist!" & vbNewLine
        If columnexist("item", "character_inventory") = False Then tmpstring = tmpstring & "// Column item in character_inventory does not exist!" & vbNewLine
        If columnexist("item_template", "character_inventory") = False Then tmpstring = tmpstring & "// Column item_template in character_inventory does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_inventory")
        errorstring = errorstring & tmpstring
    End Sub
    Private Sub check_character_homebind()
        tmpstring = ""
        If columnexist("guid", "character_homebind") = False Then tmpstring = tmpstring & "// Column guid in character_homebind does not exist!" & vbNewLine
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
        If columnexist("guid", "character_action") = False Then tmpstring = tmpstring & "// Column guid in character_action does not exist!" & vbNewLine
        If columnexist("spec", "character_action") = False Then tmpstring = tmpstring & "// Column spec in character_action does not exist!" & vbNewLine
        If columnexist("button", "character_action") = False Then tmpstring = tmpstring & "// Column button in character_action does not exist!" & vbNewLine
        If columnexist("action", "character_action") = False Then tmpstring = tmpstring & "// Column action in character_action does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("character_action")
        errorstring = errorstring & tmpstring
    End Sub
    Private Sub check_character_achievement()
        tmpstring = ""
        If columnexist("guid", "character_achievement") = False Then tmpstring = tmpstring & "// Column guid in character_achievement does not exist!" & vbNewLine
        If columnexist("achievement", "character_achievement") = False Then tmpstring = tmpstring & "// Column achievement in character_achievement does not exist!" & vbNewLine
        If columnexist("date", "character_achievement") = False Then tmpstring = tmpstring & "// Column date in character_achievement does not exist!" & vbNewLine
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
        Dim da As New MySqlDataAdapter("SELECT COLUMN_NAME FROM COLUMNS WHERE TABLE_NAME='" & table & "' AND TABLE_SCHEMA='" & Main.characterdbname & "'", conn)
        Dim dt As New DataTable
        Try
            conn.Open()
        Catch ex As Exception
            conn.Close()
            conn.Dispose()
        End Try
        Try
            da.Fill(dt)
            conn.Close()
            conn.Dispose()
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
