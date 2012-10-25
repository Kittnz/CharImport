'****************************************************************************************
'****************************************************************************************
'***************************** CharImport- Conntect *************************************
'****************************************************************************************
'..................Status
'...................Code:       80%
'...................Design:     95%
'...................Functions:  60%
'****************************************************************************************
'****************************************************************************************
'..................Last modified: 06.01.12
'****************************************************************************************
'****************************************************************************************
'..................Comments:
'

Imports System.Globalization
Imports MySql.Data.MySqlClient
Imports System.Threading

Public Class Connect
    Dim runfunction As New Functions
    Dim armoryproc As New prozedur_armory
    Dim trinitycorecheck As New Core_Check_Trinity
    Dim mangoscorecheck As New Core_Check_Mangos
    Dim arcemucorecheck As New Core_Check_ArcEmu
    Dim xpansion As String
    Dim reporttext As RichTextBox = Process_Status.processreport
    Dim procstatus As New Process_Status
    Dim ServerString As String = ""
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Dim SQLConnectionRealmd As MySqlConnection = New MySqlConnection
    Dim quelltext As String = ""
    Dim talentpage As String = ""
    Dim sectalentpage As String = ""
    'Dim datacharname As String = ""
    Dim guid As String = ""
    Dim accguid As String = ""
    Dim lastnumber As String = ""

    Dim _
        finalstring As String =
            "kopf 0 hals 0 schulter 0 hemd 0 brust 0 guertel 0 beine 0 stiefel 0 handgelenke 0 haende 0 finger1 0 finger2 0 schmuck1 0 schmuck2 0 ruecken 0 haupt 0 off 0 distanz 0 wappenrock 0 "

    Dim errorcount As Integer = 0
    Dim newcharguid As Integer
    Dim spellitemtext As String
    Dim spellgemtext As String
    Dim trinitycore1 As New Trinity_core
    Dim mangoscore As New Mangos_core
    Dim arcemucore As New ArcEmu_core
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
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN

    Public Sub New()
        MyBase.New()

        Thread.CurrentThread.CurrentUICulture = New CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        runfunction.writelog("Standard login info call")
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
                        MsgBox(localeDE.armory2database_txt1)
                    Else
                        MsgBox(localeEN.armory2database_txt1)
                    End If
                    Exit Sub
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
                                            Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text &
                                                                      ";User id=" & user.Text & ";Password=" &
                                                                      password.Text & ";Database=" & auth.Text
                                        End If
                                    Else
                                        Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text &
                                                                  ";User id=" & user.Text & ";Password=" & password.Text &
                                                                  ";Database=op_realm"
                                    End If
                                Else
                                    Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text &
                                                              ";User id=" & user.Text & ";Password=" & password.Text &
                                                              ";Database=logon"
                                End If
                            Else
                                Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                          user.Text & ";Password=" & password.Text & ";Database=auth"
                            End If
                        Else
                            Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                      user.Text & ";Password=" & password.Text & ";Database=realm"
                        End If
                    Else
                        Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                  user.Text & ";Password=" & password.Text & ";Database=realmd"
                    End If
                    runfunction.writelog("Could find character db and auth db")
                    Main.ServerString = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                        ";Password=" & password.Text & ";Database=character"
                    Main.characterdbname = "character"
                    Main.ServerStringCheck = Main.ServerString
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.armory2database_txt5)
                    Else
                        MsgBox(localeEN.armory2database_txt5)
                    End If

                    CheckBox1.Enabled = True
                    CheckBox2.Enabled = True
                    CheckBox3.Enabled = True
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
                                        Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text &
                                                                  ";User id=" & user.Text & ";Password=" & password.Text &
                                                                  ";Database=" & auth.Text
                                    End If
                                Else
                                    Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text &
                                                              ";User id=" & user.Text & ";Password=" & password.Text &
                                                              ";Database=op_realm"
                                End If
                            Else
                                Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                          user.Text & ";Password=" & password.Text & ";Database=logon"
                            End If
                        Else
                            Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                      user.Text & ";Password=" & password.Text & ";Database=auth"
                        End If
                    Else
                        Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" &
                                                  user.Text & ";Password=" & password.Text & ";Database=realm"
                    End If
                Else
                    Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                              ";Password=" & password.Text & ";Database=realmd"
                End If
                Main.ServerString = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                    ";Password=" & password.Text & ";Database=characters"
                Main.ServerStringCheck = Main.ServerString
                Main.characterdbname = "characters"
                runfunction.writelog("Could find character db and auth db")
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.armory2database_txt5)
                Else
                    MsgBox(localeEN.armory2database_txt5)
                End If

                CheckBox1.Enabled = True
                CheckBox2.Enabled = True
                CheckBox3.Enabled = True
            End If
        Else
            runfunction.writelog("Connect request with manually checked")
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
                    Main.ServerStringRealmd = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                              ";Password=" & password.Text & ";Database=" & auth.Text
                    Main.ServerString = "server=" & address.Text & ";Port=" & port.Text & ";User id=" & user.Text &
                                        ";Password=" & password.Text & ";Database=" & characters.Text
                    Main.characterdbname = characters.Text
                    Main.ServerStringCheck = Main.ServerString
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.armory2database_txt5)
                    Else
                        MsgBox(localeEN.armory2database_txt5)
                    End If

                    CheckBox1.Enabled = True
                    CheckBox2.Enabled = True
                    CheckBox3.Enabled = True
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
        Panel4.Location = Panel3.Location
    End Sub

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

    Private Sub Connect_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If My.Settings.shellclose = True Then
            Starter.Show()

        Else

        End If
    End Sub

    Private Sub Connect_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        '  My.Settings.shellclose = True
        My.Settings.Save()
        runfunction.writelog("Connect_Load call")
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size
        charerrorlabel.Text = ""
        newcharerrorlabel.Text = ""
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
        If Main.progressmode = 0 Then
            spellitemtext = My.Resources.SpellItemEnchantmentCata
            '   erfolge.Enabled = False
            talents.Enabled = False
            '  erfolge.Enabled = False
            '  skills.Enabled = False
            zauber.Enabled = False
            pvp.Enabled = False
            '  ruf.Enabled = False
            inventar.Enabled = False
            gold.Enabled = False
        ElseIf Main.progressmode = 1 Then
            spellitemtext = My.Resources.SpellItemEnchantmentCata
            '  erfolge.Enabled = False
            talents.Enabled = False
            '   erfolge.Enabled = False
            '   skills.Enabled = False
            zauber.Enabled = False
            pvp.Enabled = False
            '  ruf.Enabled = False
            inventar.Enabled = False
            gold.Enabled = False
        Else

            spellitemtext = My.Resources.SpellItemEnchantment
        End If
      spellitemtext = My.Resources.VZ_ID_wotlk
        spellgemtext = My.Resources.GEM_ID_wotlk
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        runfunction.writelog("Standard login info call")
        address.Text = My.Settings.address
        port.Text = My.Settings.port
        user.Text = My.Settings.user
        password.Text = My.Settings.pass
    End Sub

    Private Sub items_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles items.CheckedChanged
        If items.Checked = True Then
            sockets.Enabled = True
            vzs.Enabled = True

        Else
            sockets.Enabled = False
            vzs.Enabled = False
            sockets.Checked = False
            vzs.Checked = False
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        If items.Checked = True Then
            Button2.Enabled = True
        ElseIf glyphs.Checked = True Then
            Button2.Enabled = True
        ElseIf talents.Checked = True Then
            Button2.Enabled = True
        ElseIf level.Checked = True Then
            Button2.Enabled = True
        ElseIf talents.Checked = True Then
            Button2.Enabled = True
        ElseIf alternatelevellabel.Checked = True Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs)
        If CheckBox1.Checked = True Then
            If Not level.Checked = True Then
                If Not alternatelevellabel.Checked = True Then
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.connect_txt6, MsgBoxStyle.Critical, localeDE.connect_txt7)
                    Else
                        MsgBox(localeEN.connect_txt6, MsgBoxStyle.Critical, localeEN.connect_txt7)
                    End If
                    Exit Sub
                ElseIf male.Checked = False And female.Checked = False Then
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.connect_txt8, MsgBoxStyle.Critical, localeDE.connect_txt9)
                    Else
                        MsgBox(localeEN.connect_txt8, MsgBoxStyle.Critical, localeEN.connect_txt9)
                    End If
                Else
                    If alternateleveltext.Text = "" Then
                        If My.Settings.language = "de" Then
                            MsgBox(localeDE.connect_txt10, MsgBoxStyle.Critical, localeDE.connect_txt11)
                        Else
                            MsgBox(localeEN.connect_txt10, MsgBoxStyle.Critical, localeEN.connect_txt11)
                        End If
                        Exit Sub
                    Else

                    End If
                End If
            End If
        End If
        errorcount = 0
        Button2.Enabled = True
        If arcemu.Checked = True Then
            guid = runcommand("SELECT guid FROM characters WHERE name = '" & charname.Text & "'", "guid",
                              Main.ServerString)
            ' lastnumber = runcommand("SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)", "guid", Main.ServerString)
            newcharguid =
                CInt(Val(runcommand("SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", "guid",
                                    Main.ServerString))) + 1

        Else
            guid = runcommand("SELECT guid FROM characters WHERE name = '" & charname.Text & "'", "guid",
                              Main.ServerString)
            lastnumber = runcommand("SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)",
                                    "guid", Main.ServerString)
            newcharguid =
                CInt(Val(runcommand("SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", "guid",
                                    Main.ServerString))) + 1

        End If
    End Sub

    Private Function runcommand(ByVal command As String, ByVal spalte As String,
                                Optional ByVal unbrauchbar As String = "nottouse") As String
        Dim conn As MySqlConnection
        'connect to DB
        conn = New MySqlConnection()
        conn.ConnectionString = ServerString
        'see if connection failed.
        Try
            conn.Open()
        Catch myerror As MySqlException

        End Try
        'sql query


        Try
            Dim myAdapter As New MySqlDataAdapter

            'Dim sqlquery = Txtstring.text
            Dim myCommand As New MySqlCommand()
            Dim reader_ol As MySqlDataReader
            myCommand.Connection = conn
            myCommand.CommandText = command
            'start query
            myAdapter.SelectCommand = myCommand
            reader_ol = myCommand.ExecuteReader()
            reader_ol.Read()

            Return (reader_ol(spalte)).ToString

            reader_ol.Close()
            conn.Close()
        Catch ex As Exception
            conn.Close()

            Return "error"
        End Try
    End Function

    Private Function runcommand2(ByVal command As String, ByVal spalte As String, ByVal serverstring2 As String) _
        As String
        Dim conn As MySqlConnection
        'connect to DB
        conn = New MySqlConnection()
        conn.ConnectionString = serverstring2
        'see if connection failed.
        Try
            conn.Open()
        Catch myerror As MySqlException

        End Try
        'sql query


        Try
            Dim myAdapter As New MySqlDataAdapter

            'Dim sqlquery = Txtstring.text
            Dim myCommand As New MySqlCommand()
            Dim reader_ol As MySqlDataReader
            myCommand.Connection = conn
            myCommand.CommandText = command
            'start query
            myAdapter.SelectCommand = myCommand
            reader_ol = myCommand.ExecuteReader()
            reader_ol.Read()
            reader_ol.Close()
            conn.Close()
            Return (reader_ol(spalte)).ToString


        Catch ex As Exception
            conn.Close()

            Return "error"
        End Try
    End Function

    Private Sub checkbox1check()
        If checkrow("SELECT * FROM characters WHERE name = '" & newcharname.Text & "'", Main.ServerString) = False Then
            'Charakter darf nicht existieren
            If _
                checkrow("SELECT * FROM account WHERE username = '" & accname.Text.ToUpper & "'",
                         Main.ServerStringRealmd) = True Then
                If My.Settings.language = "de" Then
                    newcharerrorlabel.Text = localeDE.connect_txt15
                Else
                    newcharerrorlabel.Text = localeEN.connect_txt15
                End If

                items.Enabled = True
                level.Enabled = True
                glyphs.Enabled = True
                '  talents.Enabled = True
                Button2.Enabled = True
                race.Enabled = True
                playerclass.Enabled = True
                alternatelevellabel.Enabled = True
                alternateleveltext.Enabled = True
                goldlabel.Enabled = True
                goldtext.Enabled = True
                male.Checked = True
                male.Enabled = True
                female.Enabled = True
                Main.char_name = newcharname.Text
                ' datacharname = charname.Text
                If arcemu.Checked = True Then
                    accguid = runcommand2("SELECT acct FROM accounts WHERE login='" & accname.Text.ToUpper & "'", "acct",
                                          Main.ServerStringRealmd)
                Else
                    accguid = runcommand2("SELECT `id` FROM `account` WHERE username='" & accname.Text.ToUpper & "'",
                                          "id", Main.ServerStringRealmd)
                End If
            ElseIf _
                checkrow("SELECT * FROM accounts WHERE login = '" & accname.Text.ToUpper & "'", Main.ServerStringRealmd) =
                True Then
                If My.Settings.language = "de" Then
                    newcharerrorlabel.Text = localeDE.connect_txt15
                Else
                    newcharerrorlabel.Text = localeEN.connect_txt15
                End If
                items.Enabled = True
                level.Enabled = True
                glyphs.Enabled = True
                '  talents.Enabled = True
                Button2.Enabled = True
                race.Enabled = True
                playerclass.Enabled = True
                alternatelevellabel.Enabled = True
                alternateleveltext.Enabled = True
                goldlabel.Enabled = True
                goldtext.Enabled = True
                male.Checked = True
                male.Enabled = True
                female.Enabled = True
                Panel5.Location = Panel3.Location
                Main.char_name = newcharname.Text
                '  datacharname = charname.Text
                If arcemu.Checked = True Then
                    accguid = runcommand2("SELECT acct FROM accounts WHERE login='" & accname.Text.ToUpper & "'", "acct",
                                          Main.ServerStringRealmd)
                Else
                    accguid = runcommand2("SELECT `id` FROM `account` WHERE username='" & accname.Text.ToUpper & "'",
                                          "id", Main.ServerStringRealmd)
                End If
            Else
                If My.Settings.language = "de" Then
                    newcharerrorlabel.Text = localeDE.connect_txt14
                Else
                    newcharerrorlabel.Text = localeEN.connect_txt14
                End If
            End If
        Else
            If My.Settings.language = "de" Then
                newcharerrorlabel.Text = localeDE.connect_txt16
            Else
                newcharerrorlabel.Text = localeEN.connect_txt16
            End If
        End If
    End Sub

    Private Sub checkbox2check()
        If checkrow("SELECT * FROM characters WHERE name = '" & charname.Text & "'", Main.ServerString) = True Then
            If My.Settings.language = "de" Then
                charerrorlabel.Text = localeDE.connect_txt20
            Else
                charerrorlabel.Text = localeEN.connect_txt20
            End If
            items.Enabled = True
            level.Enabled = True
            glyphs.Enabled = True
            '    talents.Enabled = True
            Button2.Enabled = True
            race.Enabled = True
            playerclass.Enabled = True
            alternatelevellabel.Enabled = True
            alternateleveltext.Enabled = True
            goldlabel.Enabled = True
            goldtext.Enabled = True
            male.Enabled = True
            female.Enabled = True
            Panel5.Location = Panel3.Location
            '  datacharname = charname.Text
        Else
            If My.Settings.language = "de" Then
                charerrorlabel.Text = localeDE.connect_txt19
            Else
                charerrorlabel.Text = localeEN.connect_txt19
            End If
        End If
    End Sub

    Private Sub checkbox3check()
        Dim sLines() As String = accnames.Lines
        For i As Integer = 0 To sLines.Length - 1
            If sLines(i) = "" Then
            Else
                If _
                    checkrow("SELECT * FROM account WHERE username = '" & sLines(i).ToUpper & "'",
                             Main.ServerStringRealmd) = True Then
                    If My.Settings.language = "de" Then
                        newcharerrorlabel.Text = localeDE.connect_txt23
                    Else
                        newcharerrorlabel.Text = localeEN.connect_txt23
                    End If
                    items.Enabled = True
                    level.Enabled = True
                    glyphs.Enabled = True
                    '  talents.Enabled = True
                    Button2.Enabled = True
                    race.Enabled = True
                    playerclass.Enabled = True
                    alternatelevellabel.Enabled = True
                    alternateleveltext.Enabled = True
                    goldlabel.Enabled = True
                    goldtext.Enabled = True
                    male.Checked = True
                    male.Enabled = True
                    female.Enabled = True

                    '   datacharname = charname.Text
                ElseIf _
                    checkrow("SELECT * FROM accounts WHERE login = '" & sLines(i).ToUpper & "'", Main.ServerStringRealmd) =
                    True Then
                    If My.Settings.language = "de" Then
                        newcharerrorlabel.Text = localeDE.connect_txt23
                    Else
                        newcharerrorlabel.Text = localeEN.connect_txt23
                    End If
                    items.Enabled = True
                    level.Enabled = True
                    glyphs.Enabled = True
                    '  talents.Enabled = True
                    Button2.Enabled = True
                    race.Enabled = True
                    playerclass.Enabled = True
                    alternatelevellabel.Enabled = True
                    alternateleveltext.Enabled = True
                    goldlabel.Enabled = True
                    goldtext.Enabled = True
                    male.Checked = True
                    male.Enabled = True
                    female.Enabled = True

                    '  datacharname = charname.Text
                Else
                    If My.Settings.language = "de" Then
                        newcharerrorlabel.Text = localeDE.connect_txt21 & " " & sLines(i) & localeDE.connect_txt22
                    Else
                        newcharerrorlabel.Text = localeEN.connect_txt21 & " " & sLines(i) & localeEN.connect_txt22
                    End If
                    Exit Sub
                End If
            End If
        Next
        Panel5.Location = Panel3.Location
    End Sub

    Private Function checkrow(ByVal query As String, ByVal connstring As String) As Boolean
        Dim mysqlconnection As New MySqlConnection
        mysqlconnection.ConnectionString = connstring
        Try
            mysqlconnection.Close()
            mysqlconnection.Dispose()
        Catch ex As Exception

        End Try
        Try
            mysqlconnection.Open()
            Try
                Dim myAdapter As New MySqlDataAdapter
                Dim myCommand As New MySqlCommand()
                myCommand.Connection = mysqlconnection
                myCommand.CommandText = query
                'start query
                myAdapter.SelectCommand = myCommand
                Dim myData As MySqlDataReader
                myData = myCommand.ExecuteReader()
                If CInt(myData.HasRows) = 0 Then
                    Try
                        mysqlconnection.Close()
                        mysqlconnection.Dispose()
                    Catch ex As Exception

                    End Try
                    Return False
                Else
                    Try
                        mysqlconnection.Close()
                        mysqlconnection.Dispose()
                    Catch ex As Exception

                    End Try
                    Return True
                End If
            Catch ex As Exception
                Try
                    mysqlconnection.Close()
                    mysqlconnection.Dispose()
                Catch

                End Try
                Return False
            End Try
        Catch ex As Exception
            Try
                mysqlconnection.Close()
                mysqlconnection.Dispose()
            Catch

            End Try
            Return False
        End Try
    End Function

    Private Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        newcharerrorlabel.Text = ""
        charerrorlabel.Text = ""
        runfunction.writelog("Continue (Button5) clicked")
        If genderstay.Enabled = False Then
            male.Checked = True
            genderstay.Checked = False
        End If
        Try
            SQLConnectionRealmd.Close()
            SQLConnectionRealmd.Dispose()
            SQLConnection.Close()
            SQLConnection.Dispose()
        Catch ex As Exception

        End Try

        If CheckBox1.Checked = True Then

            If accname.Text = "" Then
                If My.Settings.language = "de" Then
                    newcharerrorlabel.Text = localeDE.connect_txt12
                Else
                    newcharerrorlabel.Text = localeEN.connect_txt12
                End If

            ElseIf newcharname.Text = "" Then
                If My.Settings.language = "de" Then
                    newcharerrorlabel.Text = localeDE.connect_txt13
                Else
                    newcharerrorlabel.Text = localeEN.connect_txt13
                End If
            Else
                checkbox1check()
            End If
        ElseIf CheckBox2.Checked = True Then

            If charname.Text = "" Then
                If My.Settings.language = "de" Then
                    charerrorlabel.Text = localeDE.connect_txt18
                Else
                    charerrorlabel.Text = localeEN.connect_txt18
                End If

            Else
                checkbox2check()
            End If
        ElseIf CheckBox3.Checked = True Then
            If accnames.Text = "" Then
                If My.Settings.language = "de" Then
                    newcharerrorlabel.Text = localeDE.connect_txt12
                Else
                    newcharerrorlabel.Text = localeEN.connect_txt12
                End If
            Else
                checkbox3check()
            End If
        Else
            If My.Settings.language = "de" Then
                newcharerrorlabel.Text = "Du hast keine Auswahl getroffen!"
            Else
                newcharerrorlabel.Text = "You did not select any option!"
            End If
            Exit Sub
        End If
        If Not Main.progressmode = 0 Or Not Main.progressmode = 1 Then
            genderstay.Checked = True
        Else
            genderstay.Checked = False
        End If
        If genderstay.Enabled = False Then
            male.Checked = True
            genderstay.Checked = False
        End If
    End Sub

    Public Sub NewUser(ByRef SQLStatement As String)

        Dim cmd As MySqlCommand = New MySqlCommand

        With cmd
            .CommandText = SQLStatement
            .CommandType = CommandType.Text
            .Connection = SQLConnection
            .ExecuteNonQuery()


        End With

        SQLConnection.Close()

        SQLConnection.Dispose()
    End Sub

    Private Sub Button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button6.Click
        runfunction.writelog("Connect_closing call")
        If Main.showstarter = True Then
            Main.showstarter = False
            Starter.Show()

        Else

        End If
    End Sub

    Public Sub button2click()
        For Each link As String In Main.linklist

            armoryproc.prozedur(link, 1, False)
        Next
        trinitycore1.opensql()
        If trinity1.Checked = True Then

            Main.outputcore = "trinity1"
            If Main.progressmode = 0 Or Main.progressmode = 1 Then
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
                If CheckBox1.Checked = True Then
                    trinitycore1.addchars(accname.Text, newcharname.Text, False)
                    ' trinitycore1.updatechars(accname.Text, Main.char_name, False)
                    If male.Checked = True Then
                        trinitycore1.setgender("0")
                    ElseIf female.Checked = True Then
                        trinitycore1.setgender("1")
                    ElseIf genderstay.Checked = True Then
                        trinitycore1.setgender(Main.char_gender.ToString)
                    End If
                    If level.Checked = True Then trinitycore1.setlevel()
                    If race.Checked = True Then trinitycore1.setrace()
                    If playerclass.Checked = True Then trinitycore1.setclass()
                    If alternatelevellabel.Checked = True Then trinitycore1.setalternatelevel(alternateleveltext.Text)
                    If goldlabel.Checked = True Then trinitycore1.setgold(goldtext.Text)
                    If items.Checked = True Then trinitycore1.additems()
                    If glyphs.Checked = True Then trinitycore1.addglyphs(xpansion)
                    If sockets.Checked = True Then trinitycore1.addgems()
                    If vzs.Checked = True Then trinitycore1.addenchantments()
                    If erfolge.Checked = True Then trinitycore1.addachievements()
                    If skills.Checked = True Then trinitycore1.addskills()
                    If ruf.Checked = True Then trinitycore1.addreputation()
                   Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    End If
                    If CheckBox2.Checked = True Then
                        trinitycore1.getguidfromname(charname.Text)

                        If male.Checked = True Then
                            trinitycore1.setgender("0")
                        ElseIf female.Checked = True Then
                            trinitycore1.setgender("1")
                        ElseIf genderstay.Checked = True Then
                            trinitycore1.setgender(Main.char_gender.ToString)
                        End If
                        If level.Checked = True Then trinitycore1.setlevel()
                        If race.Checked = True Then trinitycore1.setrace()
                        If playerclass.Checked = True Then trinitycore1.setclass()
                        If alternatelevellabel.Checked = True Then trinitycore1.setalternatelevel(alternateleveltext.Text)
                        If goldlabel.Checked = True Then trinitycore1.setgold(goldtext.Text)
                        If items.Checked = True Then trinitycore1.additems()
                        If glyphs.Checked = True Then trinitycore1.addglyphs(xpansion)
                        If sockets.Checked = True Then trinitycore1.addgems()
                    If vzs.Checked = True Then trinitycore1.addenchantments()
                    If erfolge.Checked = True Then trinitycore1.addachievements()
                    If skills.Checked = True Then trinitycore1.addskills()
                    If ruf.Checked = True Then trinitycore1.addreputation()
                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    End If

                    If CheckBox3.Checked = True Then
                        Dim sLines() As String = accnames.Lines
                        For i As Integer = 0 To sLines.Length - 1
                            If sLines(i) = "" Then
                            Else
                                trinitycore1.addchars(sLines(i), Main.char_name, False)
                                '  trinitycore1.updatechars(sLines(i), Main.char_name, False)
                                If items.Checked = True Then trinitycore1.additems()
                                If sockets.Checked = True Then trinitycore1.addgems()
                                If vzs.Checked = True Then trinitycore1.addenchantments()
                                If glyphs.Checked = True Then trinitycore1.addglyphs(xpansion)
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
                            If ruf.Checked = True Then trinitycore1.addreputation()
                                Process_Status.processreport.AppendText(
                                    Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                            End If

                        Next
                    End If
                Else
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
                    If CheckBox1.Checked = True Then

                        trinitycore1.adddetailedchar(accname.Text, newcharname.Text, False)
                        If items.Checked = True Then trinitycore1.additems()
                        If sockets.Checked = True And vzs.Checked = True Then
                            trinitycore1.addench()
                        ElseIf sockets.Checked = True Then
                            trinitycore1.addgems()
                        ElseIf vzs.Checked = True Then
                            trinitycore1.addenchantments()
                        Else
                        End If
                        If glyphs.Checked = True Then trinitycore1.addglyphs(xpansion)
                        If talents.Checked = True Then trinitycore1.addtalents()
                        If male.Checked = True Then trinitycore1.setgender("0")
                        If female.Checked = True Then trinitycore1.setgender("1")
                        If genderstay.Checked = True Then trinitycore1.setgender(Main.char_gender.ToString)
                        If level.Checked = True Then trinitycore1.setlevel()
                        If alternatelevellabel.Checked = True Then trinitycore1.setalternatelevel(alternateleveltext.Text)
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
                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    End If
                    If CheckBox2.Checked = True Then
                        trinitycore1.getguidfromname(charname.Text)


                        If items.Checked = True Then trinitycore1.additems()
                        If sockets.Checked = True And vzs.Checked = True Then
                            trinitycore1.addench()
                        ElseIf sockets.Checked = True Then
                            trinitycore1.addgems()
                        ElseIf vzs.Checked = True Then
                            trinitycore1.addenchantments()
                        Else
                        End If
                        If glyphs.Checked = True Then trinitycore1.addglyphs(xpansion)
                        If talents.Checked = True Then trinitycore1.addtalents()
                        If male.Checked = True Then trinitycore1.setgender("0")
                        If female.Checked = True Then trinitycore1.setgender("1")
                        If genderstay.Checked = True Then trinitycore1.setgender(Main.char_gender.ToString)
                        If level.Checked = True Then trinitycore1.setlevel()
                        If alternatelevellabel.Checked = True Then trinitycore1.setalternatelevel(alternateleveltext.Text)
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
                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    End If

                    If CheckBox3.Checked = True Then
                        Dim sLines() As String = accnames.Lines
                        For i As Integer = 0 To sLines.Length - 1
                            If sLines(i) = "" Then
                            Else

                                trinitycore1.adddetailedchar(sLines(i), Main.char_name, False)
                                If items.Checked = True Then trinitycore1.additems()
                                If sockets.Checked = True And vzs.Checked = True Then
                                    trinitycore1.addench()
                                ElseIf sockets.Checked = True Then
                                    trinitycore1.addgems()
                                ElseIf vzs.Checked = True Then
                                    trinitycore1.addenchantments()
                                Else
                                End If
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
                                Process_Status.processreport.AppendText(
                                    Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                            End If

                        Next
                    End If
                End If

            ElseIf mangos.Checked = True Then
                Main.outputcore = "mangos"
                If Main.progressmode = 0 Or Main.progressmode = 1 Then
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
                    If CheckBox1.Checked = True Then
                        mangoscore.addchars(accname.Text, newcharname.Text, False)
                        ' mangoscore.updatechars(accname.Text, Main.char_name, False)
                        If male.Checked = True Then
                            mangoscore.setgender("0")
                        ElseIf female.Checked = True Then
                            mangoscore.setgender("1")
                        ElseIf genderstay.Checked = True Then
                            mangoscore.setgender(Main.char_gender.ToString)
                        End If
                        If level.Checked = True Then mangoscore.setlevel()
                        If race.Checked = True Then mangoscore.setrace()
                        If playerclass.Checked = True Then mangoscore.setclass()
                        If alternatelevellabel.Checked = True Then mangoscore.setalternatelevel(alternateleveltext.Text)
                        If goldlabel.Checked = True Then mangoscore.setgold(goldtext.Text)
                        If items.Checked = True Then mangoscore.additems()
                        If glyphs.Checked = True Then mangoscore.addglyphs(xpansion)
                        If sockets.Checked = True Then mangoscore.addgems()
                    If vzs.Checked = True Then mangoscore.addenchantments()
                    If erfolge.Checked = True Then mangoscore.addachievements()
                    If skills.Checked = True Then mangoscore.addskills()
                    If ruf.Checked = True Then mangoscore.addreputation()
                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    End If
                    If CheckBox2.Checked = True Then
                        mangoscore.getguidfromname(charname.Text)
                        If male.Checked = True Then
                            mangoscore.setgender("0")
                        ElseIf female.Checked = True Then
                            mangoscore.setgender("1")
                        ElseIf genderstay.Checked = True Then
                            mangoscore.setgender(Main.char_gender.ToString)
                        End If
                        If level.Checked = True Then mangoscore.setlevel()
                        If race.Checked = True Then mangoscore.setrace()
                        If playerclass.Checked = True Then mangoscore.setclass()
                        If alternatelevellabel.Checked = True Then mangoscore.setalternatelevel(alternateleveltext.Text)
                        If goldlabel.Checked = True Then mangoscore.setgold(goldtext.Text)
                        If items.Checked = True Then mangoscore.additems()
                        If glyphs.Checked = True Then mangoscore.addglyphs(xpansion)
                        If sockets.Checked = True Then mangoscore.addgems()
                    If vzs.Checked = True Then mangoscore.addenchantments()
                    If erfolge.Checked = True Then mangoscore.addachievements()
                    If skills.Checked = True Then mangoscore.addskills()
                    If ruf.Checked = True Then mangoscore.addreputation()
                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    End If

                    If CheckBox3.Checked = True Then
                        Dim sLines() As String = accnames.Lines
                        For i As Integer = 0 To sLines.Length - 1
                            If sLines(i) = "" Then
                            Else
                                mangoscore.addchars(sLines(i), Main.char_name, False)
                                '  mangoscore.updatechars(sLines(i), Main.char_name, False)
                                If items.Checked = True Then mangoscore.additems()
                                If sockets.Checked = True Then mangoscore.addgems()
                                If vzs.Checked = True Then mangoscore.addenchantments()
                                If glyphs.Checked = True Then mangoscore.addglyphs(xpansion)
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
                            If ruf.Checked = True Then mangoscore.addreputation()
                                Process_Status.processreport.AppendText(
                                    Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                            End If

                        Next
                    End If
                Else
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
                    If CheckBox1.Checked = True Then

                        mangoscore.adddetailedchar(accname.Text, newcharname.Text, False)
                        If items.Checked = True Then mangoscore.additems()
                        If sockets.Checked = True And vzs.Checked = True Then
                            mangoscore.addench()
                        ElseIf sockets.Checked = True Then
                            mangoscore.addgems()
                        ElseIf vzs.Checked = True Then
                            mangoscore.addenchantments()
                        Else
                        End If
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
                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    End If
                    If CheckBox2.Checked = True Then
                        mangoscore.getguidfromname(charname.Text)


                        If items.Checked = True Then mangoscore.additems()
                        If sockets.Checked = True And vzs.Checked = True Then
                            mangoscore.addench()
                        ElseIf sockets.Checked = True Then
                            mangoscore.addgems()
                        ElseIf vzs.Checked = True Then
                            mangoscore.addenchantments()
                        Else
                        End If
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
                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    End If

                    If CheckBox3.Checked = True Then
                        Dim sLines() As String = accnames.Lines
                        For i As Integer = 0 To sLines.Length - 1
                            If sLines(i) = "" Then
                            Else

                                mangoscore.adddetailedchar(sLines(i), Main.char_name, False)
                                If items.Checked = True Then mangoscore.additems()
                                If sockets.Checked = True And vzs.Checked = True Then
                                    mangoscore.addench()
                                ElseIf sockets.Checked = True Then
                                    mangoscore.addgems()
                                ElseIf vzs.Checked = True Then
                                    mangoscore.addenchantments()
                                Else
                                End If
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
                                Process_Status.processreport.AppendText(
                                    Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                            End If

                        Next
                    End If
                End If
            ElseIf arcemu.Checked = True Then
                Main.outputcore = "arcemu"
                If Main.progressmode = 0 Or Main.progressmode = 1 Then
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
                    If CheckBox1.Checked = True Then
                        arcemucore.addchars(accname.Text, newcharname.Text, False)
                        ' arcemucore.updatechars(accname.Text, Main.char_name, False)
                        If male.Checked = True Then
                            arcemucore.setgender("0")
                        ElseIf female.Checked = True Then
                            arcemucore.setgender("1")
                        ElseIf genderstay.Checked = True Then
                            arcemucore.setgender(Main.char_gender.ToString)
                    End If
                    If items.Checked = True Then arcemucore.additems()
                    If sockets.Checked = True Then arcemucore.addgems()
                    If vzs.Checked = True Then arcemucore.addenchantments()
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
                Process_Status.processreport.AppendText(
                          Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
            End If
                    If CheckBox2.Checked = True Then
                        arcemucore.getguidfromname(charname.Text)

                        If male.Checked = True Then
                            arcemucore.setgender("0")
                        ElseIf female.Checked = True Then
                            arcemucore.setgender("1")
                        ElseIf genderstay.Checked = True Then
                            arcemucore.setgender(Main.char_gender.ToString)
                    End If
                   If items.Checked = True Then arcemucore.additems()
                    If sockets.Checked = True Then arcemucore.addgems()
                    If vzs.Checked = True Then arcemucore.addenchantments()
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

                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    End If

                    If CheckBox3.Checked = True Then
                        Dim sLines() As String = accnames.Lines
                        For i As Integer = 0 To sLines.Length - 1
                            If sLines(i) = "" Then
                            Else
                                arcemucore.addchars(sLines(i), Main.char_name, False)
                            '  arcemucore.updatechars(sLines(i), Main.char_name, False)
                            If items.Checked = True Then arcemucore.additems()
                            If sockets.Checked = True Then arcemucore.addgems()
                            If vzs.Checked = True Then arcemucore.addenchantments()
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
                                Process_Status.processreport.AppendText(
                                    Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                            End If

                        Next
                    End If
                Else
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
                    If CheckBox1.Checked = True Then

                        arcemucore.adddetailedchar(accname.Text, newcharname.Text, False)
                        If items.Checked = True Then arcemucore.additems()
                        If sockets.Checked = True And vzs.Checked = True Then
                            arcemucore.addench()
                        ElseIf sockets.Checked = True Then
                            arcemucore.addgems()
                        ElseIf vzs.Checked = True Then
                            arcemucore.addenchantments()
                        Else
                    End If
                    If items.Checked = True Then arcemucore.additems()
                    If sockets.Checked = True And vzs.Checked = True Then
                        arcemucore.addench()
                    ElseIf sockets.Checked = True Then
                        arcemucore.addgems()
                    ElseIf vzs.Checked = True Then
                        arcemucore.addenchantments()
                    Else
                    End If
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
                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    End If
                    If CheckBox2.Checked = True Then
                        arcemucore.getguidfromname(charname.Text)


                        If items.Checked = True Then arcemucore.additems()
                        If sockets.Checked = True And vzs.Checked = True Then
                            arcemucore.addench()
                        ElseIf sockets.Checked = True Then
                            arcemucore.addgems()
                        ElseIf vzs.Checked = True Then
                            arcemucore.addenchantments()
                        Else
                        End If
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
                        Process_Status.processreport.AppendText(
                            Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
                    End If

                    If CheckBox3.Checked = True Then
                        Dim sLines() As String = accnames.Lines
                        For i As Integer = 0 To sLines.Length - 1
                            If sLines(i) = "" Then
                            Else

                                arcemucore.adddetailedchar(sLines(i), Main.char_name, False)
                                If items.Checked = True Then arcemucore.additems()
                                If sockets.Checked = True And vzs.Checked = True Then
                                    arcemucore.addench()
                                ElseIf sockets.Checked = True Then
                                    arcemucore.addgems()
                                ElseIf vzs.Checked = True Then
                                    arcemucore.addenchantments()
                                Else
                                End If
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
                                Process_Status.processreport.AppendText(
                                    Now.TimeOfDay.ToString & "// Character is completed!" & vbNewLine)
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
            Process_Status.Button1.Enabled = True
            Application.DoEvents()
            Main.Close()
            Starter.Show()
            Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        If CheckBox1.Checked = True Then
            If Not level.Checked = True Then
                If Not alternatelevellabel.Checked = True Then
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.connect_txt6, MsgBoxStyle.Critical, localeDE.connect_txt7)
                    Else
                        MsgBox(localeEN.connect_txt6, MsgBoxStyle.Critical, localeEN.connect_txt7)
                    End If
                    Exit Sub
                ElseIf male.Checked = False And female.Checked = False Then
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.connect_txt8, MsgBoxStyle.Critical, localeDE.connect_txt9)
                    Else
                        MsgBox(localeEN.connect_txt8, MsgBoxStyle.Critical, localeEN.connect_txt9)
                    End If
                Else
                    If alternateleveltext.Text = "" Then
                        If My.Settings.language = "de" Then
                            MsgBox(localeDE.connect_txt10, MsgBoxStyle.Critical, localeDE.connect_txt11)
                        Else
                            MsgBox(localeEN.connect_txt10, MsgBoxStyle.Critical, localeEN.connect_txt11)
                        End If
                        Exit Sub
                    Else

                    End If
                End If
            End If
        End If
        errorcount = 0
        Button2.Enabled = True
        If arcemu.Checked = True Then
            guid = runcommand("SELECT guid FROM characters WHERE name = '" & charname.Text & "'", "guid",
                              Main.ServerString)
            ' lastnumber = runcommand("SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)", "guid", Main.ServerString)
            newcharguid =
                CInt(Val(runcommand("SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", "guid",
                                    Main.ServerString))) + 1

        Else
            guid = runcommand("SELECT guid FROM characters WHERE name = '" & charname.Text & "'", "guid",
                              Main.ServerString)
            lastnumber = runcommand("SELECT guid FROM item_instance WHERE guid=(SELECT MAX(guid) FROM item_instance)",
                                    "guid", Main.ServerString)
            newcharguid =
                CInt(Val(runcommand("SELECT guid FROM characters WHERE guid=(SELECT MAX(guid) FROM characters)", "guid",
                                    Main.ServerString))) + 1

        End If
        Main.ServerStringCheck = Main.ServerString
        Me.UseWaitCursor = True
        Process_Status.Close()
        Process_Status.Dispose()
        Process_Status.Show()
        Application.DoEvents()

        If wotlk.Checked = True Then
            Main.xpac = 3
            xpansion = "wotlk"
        ElseIf cata.Checked = True Then
            Main.xpac = 4
            xpansion = "cata"
        ElseIf classic.Checked = True Then
            Main.xpac = 1
            xpansion = "classic"
        ElseIf tbc.Checked = True Then
            Main.xpac = 2
            xpansion = "tbc"
        End If
        runfunction.writelog("Start corecheck request")
        If trinity1.Checked = True Then
            trinitycorecheck.begincheck(42)
        ElseIf mangos.Checked = True Then
            mangoscorecheck.begincheck(42)
        ElseIf arcemu.Checked = True Then
            arcemucorecheck.begincheck(42)
        Else

        End If
    End Sub

    Private Sub normalsqlcommand(ByVal command As String)
        Try
            SQLConnection.Close()
            SQLConnection.Dispose()
        Catch ex As Exception

        End Try
        SQLConnection.ConnectionString = ServerString

        Try

            If SQLConnection.State = ConnectionState.Closed Then
                SQLConnection.Open()
                NewUser(command)

            Else
                SQLConnection.Close()

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PictureBox6_Click(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then
            runfunction.writelog("Create new character selected")
            CheckBox2.Checked = False

            accname.Enabled = True
            accname.Text = ""
            newcharname.Enabled = True
            newcharname.Text = ""
            Button5.Enabled = True
            CheckBox3.Checked = False
        Else
            'CheckBox2.Checked = True
            charname.Enabled = True
            charname.Text = ""

            Button5.Enabled = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            runfunction.writelog("Overwrite existing character selected")
            CheckBox1.Checked = False
            charname.Enabled = True
            charname.Text = ""

            Button5.Enabled = True
            CheckBox3.Checked = False
        Else


            accname.Enabled = True
            accname.Text = ""
            newcharname.Enabled = True
            newcharname.Text = ""
            Button5.Enabled = True
        End If
    End Sub

    Private Sub charname_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles charname.TextChanged
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Sub alternatelevellabel_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles alternatelevellabel.CheckedChanged
        If alternatelevellabel.Checked = True Then
            level.Checked = False
        Else
            alternateleveltext.Text = ""
        End If
    End Sub

    Private Sub level_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles level.CheckedChanged
        If level.Checked = True Then
            alternatelevellabel.Checked = False
            alternateleveltext.Text = ""
        Else

        End If
    End Sub

    Private Sub male_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles male.CheckedChanged
        If male.Checked = True Then
            female.Checked = False
            genderstay.Checked = False
        End If
    End Sub

    Private Sub female_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles female.CheckedChanged
        If female.Checked = True Then
            male.Checked = False
            genderstay.Checked = False
        End If
    End Sub

    Private Sub tbc_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tbc.CheckedChanged
        If tbc.Checked = True Then
            runfunction.writelog("tbc checked")
            glyphs.Enabled = False
            erfolge.Enabled = False

        End If
    End Sub

    Private Sub classic_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles classic.CheckedChanged
        If classic.Checked = True Then
            runfunction.writelog("classic checked")
            glyphs.Enabled = False
            erfolge.Enabled = False
        End If
    End Sub

    Private Sub wotlk_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles wotlk.CheckedChanged
        If wotlk.Checked = True Then
            runfunction.writelog("wotlk checked")
            glyphs.Enabled = True
            If Main.progressmode = 0 Then
            ElseIf Main.progressmode = 1 Then
            Else
                erfolge.Enabled = True
            End If
        End If
    End Sub

    Private Sub cata_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cata.CheckedChanged
        If cata.Checked = True Then
            runfunction.writelog("cata checked")
            glyphs.Enabled = True
            If Main.progressmode = 0 Then
            ElseIf Main.progressmode = 1 Then
            Else
                erfolge.Enabled = True
            End If
        End If
    End Sub

    Private Sub goldlabel_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles goldlabel.CheckedChanged
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Sub goldtext_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles goldtext.TextChanged
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            runfunction.writelog("Create new character on multiple accounts selected")
            CheckBox2.Checked = False
            charname.Enabled = False
            charname.Text = ""
            accname.Enabled = True
            accname.Text = ""
            newcharname.Enabled = True
            newcharname.Text = ""
            Button5.Enabled = True
            CheckBox1.Checked = False

        Else
            'CheckBox2.Checked = True
            charname.Enabled = True
            charname.Text = ""

            Button5.Enabled = True
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) _
        Handles LinkLabel1.LinkClicked
        If Main.progressmode = 2 Or Main.progressmode = 3 Then
            items.Checked = True
            sockets.Checked = True
            vzs.Checked = True
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
        Else
            erfolge.Checked = True
            skills.Checked = True
            ruf.Checked = True
            items.Checked = True
            sockets.Checked = True
            vzs.Checked = True
            glyphs.Checked = True
            level.Checked = True
            race.Checked = True
            playerclass.Checked = True
        End If
        If tbc.Checked = True Then
            glyphs.Checked = False
            erfolge.Checked = False
        ElseIf classic.Checked = True Then
            glyphs.Checked = False
            erfolge.Checked = False
        Else

        End If
    End Sub

    Private Sub genderstay_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles genderstay.CheckedChanged
        If genderstay.Checked = True Then
            male.Checked = False
            female.Checked = False
        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button7.Click
        runfunction.writelog("Set standard login info call")
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

    Private Sub xlabel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles xlabel.Click
    End Sub

    Private Sub GroupBox9_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles GroupBox9.Enter
    End Sub

    Private Sub trinity1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles trinity1.CheckedChanged
        If trinity1.Checked = True Then runfunction.writelog("Trinity checked")
    End Sub

    Private Sub mangos_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles mangos.CheckedChanged
        If mangos.Checked = True Then
            runfunction.writelog("mangos checked")
        End If
    End Sub

    Private Sub arcemu_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles arcemu.CheckedChanged
        If arcemu.Checked = True Then runfunction.writelog("ArcEmu checked")
    End Sub

    Private Sub Panel3_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Panel3.Paint
    End Sub

    Private Sub erfolge_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles erfolge.CheckedChanged

    End Sub
End Class