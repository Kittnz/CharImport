'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* Core_Check_ArcEmu contains several methods to check if the connected
'* database is a valid ArcEmu database and makes adjustments
'*
'* Developed by Alcanmage/megasus

Imports MySql.Data.MySqlClient

Public Class Core_Check_ArcEmu
    Dim reporttext As RichTextBox = Database_Check.report
    Dim reporttext2 As RichTextBox = Process_Status.processreport

    Dim ServerString2 As String = Main.ServerStringCheck
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim errorstring As String = ""
    Dim tmpstring As String = ""

    Public Sub begincheck(ByVal startcond As Integer)
        Main.tableschema = ""
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Begin Core Check..." & vbNewLine)
        Application.DoEvents()
        Main.nowgoon = False
        Main.startcond = startcond
        check_characters()
        If Main.xpac >= 3 Then
            check_character_achievement()

        End If
        check_character_queststatus()
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

            Database_Check.Show()
            errorstring = "Determined core: ArcEmu" & vbNewLine & errorstring
            reporttext.Text = errorstring

            Application.DoEvents()
        End If
    End Sub

    Private Sub check_characters()
        tmpstring = ""
        If columnexist("guid", "characters") = False Then _
            tmpstring = tmpstring & "// Column guid in characters does not exist!" & vbNewLine
        If columnexist("acct", "characters") = False Then _
            tmpstring = tmpstring & "// Column acct in characters does not exist!" & vbNewLine
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
        If columnexist("gold", "characters") = False Then _
            tmpstring = tmpstring & "// Column gold in characters does not exist!" & vbNewLine
        If columnexist("skills", "characters") = False Then _
            tmpstring = tmpstring & "// Column skills in characters does not exist!" & vbNewLine
        If columnexist("bytes", "characters") = False Then _
            tmpstring = tmpstring & "// Column bytes in characters does not exist!" & vbNewLine
        If columnexist("player_bytes", "characters") = False Then _
            tmpstring = tmpstring & "// Column player_bytes in characters does not exist!" & vbNewLine
        If columnexist("positionX", "characters") = False Then _
            tmpstring = tmpstring & "// Column positionX in characters does not exist!" & vbNewLine
        If columnexist("positionY", "characters") = False Then _
            tmpstring = tmpstring & "// Column positionY in characters does not exist!" & vbNewLine
        If columnexist("positionZ", "characters") = False Then _
            tmpstring = tmpstring & "// Column positionZ in characters does not exist!" & vbNewLine
        If columnexist("mapId", "characters") = False Then _
            tmpstring = tmpstring & "// Column mapId in characters does not exist!" & vbNewLine
        If columnexist("orientation", "characters") = False Then _
            tmpstring = tmpstring & "// Column orientation in characters does not exist!" & vbNewLine
        If columnexist("taximask", "characters") = False Then _
            tmpstring = tmpstring & "// Column taximask in characters does not exist!" & vbNewLine
        If columnexist("zoneId", "characters") = False Then _
            tmpstring = tmpstring & "// Column zoneId in characters does not exist!" & vbNewLine
         If columnexist("playedtime", "characters") = False Then _
            tmpstring = tmpstring & "// Column playedtime in characters does not exist!" & vbNewLine
        If columnexist("first_login", "characters") = False Then _
            tmpstring = tmpstring & "// Column first_login in characters does not exist!" & vbNewLine
        If columnexist("forced_rename_pending", "characters") = False Then _
            tmpstring = tmpstring & "// Column forced_rename_pending in characters does not exist!" & vbNewLine
        If columnexist("spells", "characters") = False Then _
            tmpstring = tmpstring & "// Column spells in characters does not exist!" & vbNewLine
        If columnexist("reputation", "characters") = False Then _
            tmpstring = tmpstring & "// Column reputation in characters does not exist!" & vbNewLine
        If columnexist("actions1", "characters") = False Then _
            tmpstring = tmpstring & "// Column actions1 in characters does not exist!" & vbNewLine
        If columnexist("actions2", "characters") = False Then _
            tmpstring = tmpstring & "// Column actions2 in characters does not exist!" & vbNewLine
        If columnexist("honorPoints", "characters") = False Then _
            tmpstring = tmpstring & "// Column honorPoints in characters does not exist!" & vbNewLine
        If columnexist("killsLifeTime", "characters") = False Then _
            tmpstring = tmpstring & "// Column killsLifeTime in characters does not exist!" & vbNewLine
        If columnexist("finished_quests", "characters") = False Then _
            tmpstring = tmpstring & "// Column finished_quests in characters does not exist!" & vbNewLine
        If columnexist("glyphs1", "characters") = False Then _
            tmpstring = tmpstring & "// Column glyphs1 in characters does not exist!" & vbNewLine
        If columnexist("talents1", "characters") = False Then _
            tmpstring = tmpstring & "// Column talents1 in characters does not exist!" & vbNewLine
        If columnexist("glyphs2", "characters") = False Then _
            tmpstring = tmpstring & "// Column glyphs2 in characters does not exist!" & vbNewLine
        If columnexist("talents2", "characters") = False Then _
            tmpstring = tmpstring & "// Column talents2 in characters does not exist!" & vbNewLine
        If columnexist("numspecs", "characters") = False Then _
            tmpstring = tmpstring & "// Column numspecs in characters does not exist!" & vbNewLine
        If columnexist("currentspec", "characters") = False Then _
            tmpstring = tmpstring & "// Column currentspec in characters does not exist!" & vbNewLine
         If Not tmpstring = "" Then gettableschema("characters")
        errorstring = errorstring & tmpstring
    End Sub

    Private Sub check_item_instance()
        tmpstring = ""
        If columnexist("guid", "playeritems") = False Then _
            tmpstring = tmpstring & "// Column guid in playeritems does not exist!" & vbNewLine
        If columnexist("ownerguid", "playeritems") = False Then _
            tmpstring = tmpstring & "// Column ownerguid in playeritems does not exist!" & vbNewLine
        If columnexist("entry", "playeritems") = False Then _
            tmpstring = tmpstring & "// Column entry in playeritems does not exist!" & vbNewLine
        If columnexist("count", "playeritems") = False Then _
           tmpstring = tmpstring & "// Column count in playeritems does not exist!" & vbNewLine
        If columnexist("containerslot", "playeritems") = False Then _
            tmpstring = tmpstring & "// Column containerslot in playeritems does not exist!" & vbNewLine
        If columnexist("slot", "playeritems") = False Then _
            tmpstring = tmpstring & "// Column slot in playeritems does not exist!" & vbNewLine
        If columnexist("enchantments", "playeritems") = False Then _
          tmpstring = tmpstring & "// Column enchantments in playeritems does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("playeritems")
        errorstring = errorstring & tmpstring
    End Sub






    Private Sub check_character_queststatus()
        tmpstring = ""
        If columnexist("player_guid", "questlog") = False Then _
            tmpstring = tmpstring & "// Column player_guid in questlog does not exist!" & vbNewLine
        If columnexist("quest_id", "questlog") = False Then _
            tmpstring = tmpstring & "// Column quest_id in questlog does not exist!" & vbNewLine
        If columnexist("slot", "questlog") = False Then _
            tmpstring = tmpstring & "// Column slot in questlog does not exist!" & vbNewLine
        If columnexist("explored_area1", "questlog") = False Then _
            tmpstring = tmpstring & "// Column explored_area1 in questlog does not exist!" & vbNewLine
        If columnexist("time_left", "questlog") = False Then _
            tmpstring = tmpstring & "// Column time_left in questlog does not exist!" & vbNewLine
        If columnexist("completed", "questlog") = False Then _
           tmpstring = tmpstring & "// Column completed in questlog does not exist!" & vbNewLine
        If Not tmpstring = "" Then gettableschema("questlog")
        errorstring = errorstring & tmpstring
    End Sub




    Private Sub check_character_achievement()
        tmpstring = ""
        If columnexist("guid", "character_achievement") = False Then _
            tmpstring = tmpstring & "// Column guid in character_achievement does not exist!" & vbNewLine
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
