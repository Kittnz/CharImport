'****************************************************************************************
'****************************************************************************************
'***************************** CharImpor - prozedur_armory *****************************************
'****************************************************************************************
'..................Status
'...................Code:       70%
'...................Design:     85%
'...................Functions:  70%
'****************************************************************************************
'****************************************************************************************
'..................Last modified: 24.02.12
'****************************************************************************************
'****************************************************************************************
'..................Comments:
'Talent-Tree needs to be loaded
'Race/Class Information

Imports System.Net
Imports System.Text

Public Class prozedur_armory
    Public test6 As String
    Public Sub prozedur()
        Main.BringToFront()

        Dim xslot As Integer = 0
        Dim gemslot As Integer = 0
        Dim vzslot As Integer = 0
        Try
            Glyphs.Close()
        Catch ex As Exception

        End Try

        Try
            Dim quellclient As New WebClient
            Main.quelltext = quellclient.DownloadString(Main.TextBox1.Text)
            Dim s As String = Main.quelltext
            Dim b() As Byte = Encoding.Default.GetBytes(s)
            Dim s1 As String = System.Text.Encoding.UTF8.GetString(b)
            Main.quelltext = s1
            If Main.quelltext.Contains("#39;") Then Main.quelltext = Main.quelltext.Replace("#39;", "'")
            If Main.quelltext.Contains("Ã¼") Then Main.quelltext = Main.quelltext.Replace("Ã¼", "ü")
            If Main.quelltext.Contains("Ã¤") Then Main.quelltext = Main.quelltext.Replace("Ã¤", "ä")
            If Main.quelltext.Contains("Ã¶") Then Main.quelltext = Main.quelltext.Replace("Ã¶", "ö")
            If Main.quelltext.Contains("ÃŸ") Then Main.quelltext = Main.quelltext.Replace("ÃŸ", "ß")
            Dim yquellclient As New WebClient
            Main.talentpage = yquellclient.DownloadString(Main.TextBox1.Text.Replace("advanced", "talent/primary"))
            If Main.talentpage.Contains("Ã¼") Then Main.talentpage = Main.talentpage.Replace("Ã¼", "ü")
            If Main.talentpage.Contains("Ã¤") Then Main.talentpage = Main.talentpage.Replace("Ã¤", "ä")
            If Main.talentpage.Contains("Ã¶") Then Main.talentpage = Main.talentpage.Replace("Ã¶", "ö")
            If Main.talentpage.Contains("ÃŸ") Then Main.talentpage = Main.talentpage.Replace("ÃŸ", "ß")
            Dim zquellclient As New WebClient
            Main.sectalentpage = zquellclient.DownloadString(Main.TextBox1.Text.Replace("advanced", "talent/secondary"))
            If Main.sectalentpage.Contains("Ã¼") Then Main.sectalentpage = Main.sectalentpage.Replace("Ã¼", "ü")
            If Main.sectalentpage.Contains("Ã¤") Then Main.sectalentpage = Main.sectalentpage.Replace("Ã¤", "ä")
            If Main.sectalentpage.Contains("Ã¶") Then Main.sectalentpage = Main.sectalentpage.Replace("Ã¶", "ö")
            If Main.sectalentpage.Contains("ÃŸ") Then Main.sectalentpage = Main.sectalentpage.Replace("ÃŸ", "ß")
        Catch ex As Exception
            MsgBox("Es konnte keine Verbindung zum Server hergestellt werden. Überprüfe deine Internetverbindung.", MsgBoxStyle.Critical, "Fehler")
            Exit Sub
        End Try
        Main.ProgressBar1.Value = 0
        My.Application.DoEvents()
        'status.Text = ""
        Main.xstatus.Text = "Loading Items..."
        My.Application.DoEvents()
        goitem()
        Main.xstatus.Text = "Loading Gems..."
        Main.ProgressBar2.Value = 30
        My.Application.DoEvents()

        Main.ProgressBar1.Value = 0
        My.Application.DoEvents()
        gogems()
        Main.xstatus.Text = "Loading Enchants..."
        Main.ProgressBar2.Value = 50
        My.Application.DoEvents()

        Main.ProgressBar1.Value = 0
        My.Application.DoEvents()
        govz()

        Main.xstatus.Text = "Loading Prime Glyphs..."
        My.Application.DoEvents()
        Main.ProgressBar2.Value = 70
        Main.ProgressBar1.Value = 0
        getglyph("<div class=""character-glyphs-column glyphs-prime"">", "<div class=""character-glyphs-column glyphs-", 1, Main.TextBox1.Text.Replace("advanced", "talent/primary"))
        Main.xstatus.Text = "Loading Major Glyphs..."
        My.Application.DoEvents()
        Main.ProgressBar1.Value = 0
        My.Application.DoEvents()
        getglyph("<div class=""character-glyphs-column glyphs-major"">", "<div class=""character-glyphs-column glyphs-", 2, Main.TextBox1.Text.Replace("advanced", "talent/primary"))
        Main.xstatus.Text = "Loading Minor Glyphs..."
        My.Application.DoEvents()
        Main.ProgressBar1.Value = 0
        My.Application.DoEvents()
        getglyph("<div class=""character-glyphs-column glyphs-minor"">", "<script type=""text/javascript"">", 3, Main.TextBox1.Text.Replace("advanced", "talent/primary"))
        Main.xstatus.Text = "Loading Character Level!"
        Main.ProgressBar2.Value = 90
        getsecglyph("<div class=""character-glyphs-column glyphs-prime"">", "<div class=""character-glyphs-column glyphs-", 1, Main.TextBox1.Text.Replace("advanced", "talent/secondary"))
        Main.xstatus.Text = "Loading Major Glyphs..."
        My.Application.DoEvents()
        Main.ProgressBar1.Value = 0
        My.Application.DoEvents()
        getsecglyph("<div class=""character-glyphs-column glyphs-major"">", "<div class=""character-glyphs-column glyphs-", 2, Main.TextBox1.Text.Replace("advanced", "talent/secondary"))
        Main.xstatus.Text = "Loading Minor Glyphs..."
        My.Application.DoEvents()
        Main.ProgressBar1.Value = 0
        My.Application.DoEvents()
        getsecglyph("<div class=""character-glyphs-column glyphs-minor"">", "<script type=""text/javascript"">", 3, Main.TextBox1.Text.Replace("advanced", "talent/secondary"))
        Main.xstatus.Text = "Loading Character Level!"
        My.Application.DoEvents()
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = Main.quelltext
            Dim anfangyx88 As String = "<meta property=""og:title"" content="""
            Dim endeyx88 As String = " @ "
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.char_name = quellcodeSplityx88
            Main.level.Text = quellcodeSplityx88 & ", "
        Catch ex As Exception
         
            My.Application.DoEvents()
            Main.level.Visible = True
        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = Main.quelltext
            Dim anfangyx88 As String = "<span class=""level""><strong>"
            Dim endeyx88 As String = "</strong></span>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.level.Text = Main.level.Text & quellcodeSplityx88 & ", "
            Main.level.Visible = True
            Main.levelid = quellcodeSplityx88
            Main.char_level = Int(Val(quellcodeSplityx88))
        Catch ex As Exception
            Main.level.Text = "Fehler beim Laden!"
            Main.char_level = 80
            My.Application.DoEvents()
            Main.level.Visible = True
        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = Main.quelltext
            Dim anfangyx88 As String = "/game/race/"
            Dim endeyx88 As String = """ class="
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.level.Text = Main.level.Text & quellcodeSplityx88.ToUpper & ", "
            If quellcodeSplityx88 = "human" Then quellcodeSplityx88 = "1"
            If quellcodeSplityx88 = "orc" Then quellcodeSplityx88 = "2"
            If quellcodeSplityx88 = "dwarf" Then quellcodeSplityx88 = "3"
            If quellcodeSplityx88 = "night-elf" Then quellcodeSplityx88 = "4"
            If quellcodeSplityx88 = "undead" Then quellcodeSplityx88 = "5"
            If quellcodeSplityx88 = "tauren" Then quellcodeSplityx88 = "6"
            If quellcodeSplityx88 = "gnome" Then quellcodeSplityx88 = "7"
            If quellcodeSplityx88 = "troll" Then quellcodeSplityx88 = "8"
            If quellcodeSplityx88 = "goblin" Then quellcodeSplityx88 = "9"
            If quellcodeSplityx88 = "blood-elf" Then quellcodeSplityx88 = "10"
            If quellcodeSplityx88 = "draenei" Then quellcodeSplityx88 = "11"
            Main.char_race = Int(Val(quellcodeSplityx88))
            Connect.race.Visible = True
        Catch ex As Exception

            Main.char_race = 1
            My.Application.DoEvents()
            Connect.race.Visible = False
        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = Main.quelltext
            Dim anfangyx88 As String = "/game/class/"
            Dim endeyx88 As String = """ class="
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.level.Text = Main.level.Text & quellcodeSplityx88.ToUpper
            If quellcodeSplityx88 = "warrior" Then quellcodeSplityx88 = "1"
            If quellcodeSplityx88 = "paladin" Then quellcodeSplityx88 = "2"
            If quellcodeSplityx88 = "hunter" Then quellcodeSplityx88 = "3"
            If quellcodeSplityx88 = "rogue" Then quellcodeSplityx88 = "4"
            If quellcodeSplityx88 = "priest" Then quellcodeSplityx88 = "5"
            If quellcodeSplityx88 = "death-knight" Then quellcodeSplityx88 = "6"
            If quellcodeSplityx88 = "shaman" Then quellcodeSplityx88 = "7"
            If quellcodeSplityx88 = "mage" Then quellcodeSplityx88 = "8"
            If quellcodeSplityx88 = "warlock" Then quellcodeSplityx88 = "9"
            If quellcodeSplityx88 = "druid" Then quellcodeSplityx88 = "11"
            Main.char_class = Int(Val(quellcodeSplityx88))
            Connect.playerclass.Visible = True
        Catch ex As Exception

            Main.char_class = 1
            My.Application.DoEvents()
            Connect.playerclass.Visible = False
        End Try
        Main.xstatus.Text = "Character loaded!"
        Main.ProgressBar2.Value = 100

        My.Application.DoEvents()

        saveglyphs()
        Main.Panel21.Location = New System.Drawing.Point(5000, 5000)
        Main.UseWaitCursor = False
        Application.DoEvents()
    End Sub

    Private Sub goitem()
        getitem(1)
        getitem(2)
        getitem(3)
        getitem(4)
        getitem(5)
        getitem(6)
        getitem(7)
        getitem(8)
        getitem(9)
        getitem(10)
        getitem(11)
        getitem(12)
        getitem(13)
        getitem(14)
        getitem(15)
        getitem(16)
        getitem(17)
        getitem(18)
        getitem(19)
    End Sub
    Private Sub gogems()
        Main.xstatus.Text = "Loading Items..."
        getgem(1)
        getgem(2)
        getgem(3)
        getgem(4)
        getgem(5)
        getgem(6)
        getgem(7)
        getgem(8)
        getgem(9)
        getgem(10)
        getgem(11)
        getgem(12)
        getgem(13)
        getgem(14)
        getgem(15)
        getgem(16)
        getgem(17)
        getgem(18)
        getgem(19)
    End Sub
    Private Sub govz()
        getvz(1)
        getvz(2)
        getvz(3)
        getvz(4)
        getvz(5)
        getvz(6)
        getvz(7)
        getvz(8)
        getvz(9)
        getvz(10)
        getvz(11)
        getvz(12)
        getvz(13)
        getvz(14)
        getvz(15)
        getvz(16)
        getvz(17)
        getvz(18)
        getvz(19)

    End Sub
    Private Sub gettalents()
        Main.talentlist = Main.emptylist
        Main.talentlist.Clear()
        Try

            Dim quellcodeyx88 As String = Main.talentpage
            Dim anfangyx88 As String = "build: """
            Dim endeyx88 As String = ""","
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.talentstring = quellcodeSplityx88

        Catch ex As Exception

        End Try
        Try

            Dim quellcodeyx88 As String = Main.talentpage
            Dim anfangyx88 As String = "<a href=""/wow/de/game/class/"
            Dim endeyx88 As String = """ class=""class"">"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.charclass = quellcodeSplityx88

        Catch ex As Exception

        End Try
        If Main.charclass = "mage" Then
            Main.talentlist = Main.magetalentprogress.progress(Main.talentstring)
        End If



        ' Dim ressource2 As String = quellcodeSplit88.Remove(0, ressource1 + 14)
    End Sub
    Private Sub getimage(ByVal itemid As String, ByVal picbox As PictureBox)
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://wowdata.buffed.de/?i=" & itemid)
            Dim anfangyx88 As String = "href=""/?i=" & itemid & """><img src="""
            Dim endeyx88 As String = """></a></td>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = "http://wowdata.buffed.de" & Split(quellcodeSplityx88, endeyx88, 6)(0)
            LoadImageFromUrl(quellcodeSplityx88, picbox)
        Catch ex As Exception

        End Try
    End Sub
    Private Function getspellidfromitem(ByVal itemid As String)
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & itemid)
            Dim anfangyx88 As String = "<a href=""/spell="
            Dim endeyx88 As String = """"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)

            Return Int(Val(quellcodeSplityx88))
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Function getnamefromid(ByVal itemid As String)
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://wowdata.buffed.de/?i=" & itemid)
            Dim anfangyx88 As String = "<tr><td><h1 class=""headline1"">"
            Dim endeyx88 As String = "</h1></td><td valign="
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
          
            Dim s As String = quellcodeSplityx88
            Dim b() As Byte = Encoding.Default.GetBytes(s)
            Dim s1 As String = System.Text.Encoding.UTF8.GetString(b)

            Return s1
        Catch ex As Exception
            Return "Fehler"
        End Try
    End Function

    Private Function getspellnamefromid(ByVal spellid As String)
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://wowdata.buffed.de/?s=" & spellid)
            Dim anfangyx88 As String = "<tr><td><h1 class=""headline1"">"
            Dim endeyx88 As String = "</h1></td><td valign="
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
           
            Dim s As String = quellcodeSplityx88
            Dim b() As Byte = Encoding.Default.GetBytes(s)
            Dim s1 As String = System.Text.Encoding.UTF8.GetString(b)

            Return s1
        Catch ex As Exception
            Return "Fehler"
        End Try
    End Function
    Public Function getvzeffectname(ByVal vzid As String)
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://de.wowhead.com/spell=" & vzid)
            Dim anfangyx88 As String = "Enchant Item: [<span class=""q2"">"
            Dim endeyx88 As String = "</span>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            If quellcodeSplityx88.Contains("<a href=") Then
                Dim quellcodeyx882 As String = quellcodeSplityx88
                Dim anfangyx882 As String = "<a href="
                Dim endeyx882 As String = """>"
                Dim quellcodeSplityx882 As String
                quellcodeSplityx882 = Split(quellcodeyx882, anfangyx882, 5)(1)
                quellcodeSplityx882 = Split(quellcodeSplityx882, endeyx882, 6)(0)
                quellcodeSplityx88 = quellcodeSplityx88.Replace("<a href=" & quellcodeSplityx882 & """>", "")
                If quellcodeSplityx88.Contains("</a>") Then quellcodeSplityx88 = quellcodeSplityx88.Replace("</a>", "")

            End If
            Dim b() As Byte = Encoding.Default.GetBytes(quellcodeSplityx88)
            quellcodeSplityx88 = System.Text.Encoding.UTF8.GetString(b)
            Return quellcodeSplityx88
        Catch ex As Exception
            
            Return ""
        End Try
    End Function
    Public Function getsocketeffectname(ByVal socketid As String)
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://de.wowhead.com/item=" & socketid)
            Dim anfangyx88 As String = "<span class=""q1"">"
            Dim endeyx88 As String = "</span>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            If quellcodeSplityx88.Contains("<a href=") Then
                Dim quellcodeyx882 As String = quellcodeSplityx88
                Dim anfangyx882 As String = "<a href="
                Dim endeyx882 As String = """>"
                Dim quellcodeSplityx882 As String
                quellcodeSplityx882 = Split(quellcodeyx882, anfangyx882, 5)(1)
                quellcodeSplityx882 = Split(quellcodeSplityx882, endeyx882, 6)(0)
                quellcodeSplityx88 = quellcodeSplityx88.Replace("<a href=" & quellcodeSplityx882 & """>", "")
                If quellcodeSplityx88.Contains("</a>") Then quellcodeSplityx88 = quellcodeSplityx88.Replace("</a>", "")

            End If
            Dim b() As Byte = Encoding.Default.GetBytes(quellcodeSplityx88)
            quellcodeSplityx88 = System.Text.Encoding.UTF8.GetString(b)
            Return quellcodeSplityx88
        Catch ex As Exception
           
            Return ""
        End Try
    End Function
    Public Sub checkpatchversion(ByVal version As String)
        Filtern.Close()

        'Items
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.kopfid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Kopf.Visible = False
                Main.kopfid = Nothing
                Main.kopfpic.Image = My.Resources.empty
                Main.kopfsocket1.Visible = False
                Main.kopfsocket2.Visible = False
                Main.kopfsocket3.Visible = False
                Main.kopfvz.Visible = False
                Main.kopfsocket1id = Nothing
                Main.kopfsocket2id = Nothing
                Main.kopfsocket3id = Nothing
                Main.kopfvzid = Nothing
                Main.Kopf.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.halsid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Hals.Visible = False
                Main.halsid = Nothing
                Main.Halspic.Image = My.Resources.empty
                Main.halssocket1.Visible = False
                Main.halssocket2.Visible = False
                Main.halssocket3.Visible = False
                Main.halsvz.Visible = False
                Main.halssocket1id = Nothing
                Main.halssocket2id = Nothing
                Main.halssocket3id = Nothing
                Main.halsvzid = Nothing
                Main.Hals.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.schulterid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Schulter.Visible = False
                Main.schulterid = Nothing
                Main.Schulterpic.Image = My.Resources.empty
                Main.schultersocket1.Visible = False
                Main.schultersocket2.Visible = False
                Main.schultersocket3.Visible = False
                Main.schultervz.Visible = False
                Main.schultersocket1id = Nothing
                Main.schultersocket2id = Nothing
                Main.schultersocket3id = Nothing
                Main.schultervzid = Nothing
                Main.Schulter.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.rueckenid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Ruecken.Visible = False
                Main.rueckenid = Nothing
                Main.Rueckenpic.Image = My.Resources.empty
                Main.rueckensocket1.Visible = False
                Main.rueckensocket2.Visible = False
                Main.rueckensocket3.Visible = False
                Main.rueckenvz.Visible = False
                Main.rueckensocket1id = Nothing
                Main.rueckensocket2id = Nothing
                Main.rueckensocket3id = Nothing
                Main.rueckenvzid = Nothing
                Main.Ruecken.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.brustid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Brust.Visible = False
                Main.brustid = Nothing
                Main.Brustpic.Image = My.Resources.empty
                Main.brustsocket1.Visible = False
                Main.brustsocket2.Visible = False
                Main.brustsocket3.Visible = False
                Main.brustvz.Visible = False
                Main.brustsocket1id = Nothing
                Main.brustsocket2id = Nothing
                Main.brustsocket3id = Nothing
                Main.brustvzid = Nothing
                Main.Brust.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.hemdid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then


            Else
                Main.Hemd.Visible = False
                Main.hemdid = Nothing
                Main.Hemdpic.Image = My.Resources.empty
                Main.Hemd.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.wappenrockid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Wappenrock.Visible = False
                Main.wappenrockid = Nothing
                Main.Wappenrockpic.Image = My.Resources.empty
                Main.Wappenrock.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.handgelenkeid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Handgelenke.Visible = False
                Main.handgelenkeid = Nothing
                Main.Handgelenkepic.Image = My.Resources.empty
                Main.Handgelenkesocket1.Visible = False
                Main.handgelenkesocket2.Visible = False
                Main.Handgelenkesocket3.Visible = False
                Main.handgelenkevz.Visible = False
                Main.handgelenkesocket1id = Nothing
                Main.handgelenkesocket2id = Nothing
                Main.handgelenkesocket3id = Nothing
                Main.handgelenkevzid = Nothing
                Main.Handgelenke.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.hauptid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Haupt.Visible = False
                Main.hauptid = Nothing
                Main.Hauptpic.Image = My.Resources.empty
                Main.Hauptsocket1.Visible = False
                Main.Hauptsocket2.Visible = False
                Main.hauptsocket3.Visible = False
                Main.hauptvz.Visible = False
                Main.hauptsocket1id = Nothing
                Main.hauptsocket2id = Nothing
                Main.hauptsocket3id = Nothing
                Main.hauptvzid = Nothing
                Main.Haupt.Text = "Platz leer"
                Main.hauptvzlabel2.Visible = False
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.offid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Off.Visible = False
                Main.offid = Nothing
                Main.Offpic.Image = My.Resources.empty
                Main.Offsocket1.Visible = False
                Main.Offsocket2.Visible = False
                Main.offsocket3.Visible = False
                Main.offvz.Visible = False
                Main.offsocket1id = Nothing
                Main.offsocket2id = Nothing
                Main.offsocket3id = Nothing
                Main.offvzid = Nothing
                Main.Off.Text = "Platz leer"
                Main.offvzlabel2.Visible = False
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.distanzid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Distanz.Visible = False
                Main.distanzid = Nothing
                Main.Distanzpic.Image = My.Resources.empty
                Main.Distanzsocket1.Visible = False
                Main.Distanzsocket2.Visible = False
                Main.distanzsocket3.Visible = False
                Main.distanzvz.Visible = False
                Main.distanzsocket1id = Nothing
                Main.distanzsocket2id = Nothing
                Main.distanzsocket3id = Nothing
                Main.distanzvzid = Nothing
                Main.Distanz.Text = "Platz leer"
                Main.distanzvzlabel2.Visible = False
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.haendeid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Haende.Visible = False
                Main.haendeid = Nothing
                Main.Haendepic.Image = My.Resources.empty
                Main.haendesocket1.Visible = False
                Main.haendesocket2.Visible = False
                Main.haendesocket3.Visible = False
                Main.haendevz.Visible = False
                Main.haendesocket1id = Nothing
                Main.haendesocket2id = Nothing
                Main.haendesocket3id = Nothing
                Main.haendevzid = Nothing
                Main.Haende.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.guertelid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Guertel.Visible = False
                Main.guertelid = Nothing
                Main.Guertelpic.Image = My.Resources.empty
                Main.guertelsocket1.Visible = False
                Main.guertelsocket2.Visible = False
                Main.guertelsocket3.Visible = False
                Main.guertelvz.Visible = False
                Main.guertelsocket1id = Nothing
                Main.guertelsocket2id = Nothing
                Main.guertelsocket3id = Nothing
                Main.guertelvzid = Nothing
                Main.Guertel.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.beineid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Beine.Visible = False
                Main.beineid = Nothing
                Main.Beinepic.Image = My.Resources.empty
                Main.beinesocket1.Visible = False
                Main.beinesocket2.Visible = False
                Main.beinesocket3.Visible = False
                Main.beinevz.Visible = False
                Main.beinesocket1id = Nothing
                Main.beinesocket2id = Nothing
                Main.beinesocket3id = Nothing
                Main.beinevzid = Nothing
                Main.Beine.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.stiefelid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Stiefel.Visible = False
                Main.stiefelid = Nothing
                Main.Stiefelpic.Image = My.Resources.empty
                Main.stiefelsocket1.Visible = False
                Main.stiefelsocket2.Visible = False
                Main.stiefelsocket3.Visible = False
                Main.stiefelvz.Visible = False
                Main.stiefelsocket1id = Nothing
                Main.stiefelsocket2id = Nothing
                Main.stiefelsocket3id = Nothing
                Main.stiefelvzid = Nothing
                Main.Stiefel.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.ring1id)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Ring1.Visible = False
                Main.ring1id = Nothing
                Main.Ring1pic.Image = My.Resources.empty
                Main.Ring1socket1.Visible = False
                Main.ring1socket2.Visible = False
                Main.ring1socket3.Visible = False
                Main.ring1vz.Visible = False
                Main.ring1socket1id = Nothing
                Main.ring1socket2id = Nothing
                Main.ring1socket3id = Nothing
                Main.ring1vzid = Nothing
                Main.Ring1.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.ring2id)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Ring2.Visible = False
                Main.ring2id = Nothing
                Main.Ring2pic.Image = My.Resources.empty
                Main.ring2socket1.Visible = False
                Main.ring2socket2.Visible = False
                Main.ring2socket3.Visible = False
                Main.ring2vz.Visible = False
                Main.ring2socket1id = Nothing
                Main.ring2socket2id = Nothing
                Main.ring2socket3id = Nothing
                Main.ring2vzid = Nothing
                Main.Ring2.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.schmuck1id)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Schmuck1.Visible = False
                Main.schmuck1id = Nothing
                Main.Schmuck1pic.Image = My.Resources.empty

                Main.schmuck1vz.Visible = False
                Main.schmuck1socket1id = Nothing
                Main.schmuck1socket2id = Nothing
                Main.schmuck1socket3id = Nothing
                Main.schmuck1vzid = Nothing
                Main.Schmuck1.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.schmuck2id)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.Schmuck2.Visible = False
                Main.schmuck2id = Nothing
                Main.Schmuck2pic.Image = My.Resources.empty

                Main.schmuck2vz.Visible = False
                Main.schmuck2socket1id = Nothing
                Main.schmuck2socket2id = Nothing
                Main.schmuck2socket3id = Nothing
                Main.schmuck2vzid = Nothing
                Main.Schmuck2.Text = "Platz leer"
            End If
        Catch ex As Exception

        End Try
        'Glyphen
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.primeglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.glyphpic1 = My.Resources.empty
                Main.textprimeglyph1 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.primeglyph2)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.glyphpic2 = My.Resources.empty
                Main.textprimeglyph2 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.primeglyph3)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.glyphpic3 = My.Resources.empty
                Main.textprimeglyph3 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.majorglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.glyphpic4 = My.Resources.empty
                Main.textmajorglyph1 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.majorglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.glyphpic5 = My.Resources.empty
                Main.textmajorglyph2 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.majorglyph3)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.glyphpic6 = My.Resources.empty
                Main.textmajorglyph3 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.minorglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.glyphpic7 = My.Resources.empty
                Main.textminorglyph1 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.minorglyph2)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.glyphpic8 = My.Resources.empty
                Main.textminorglyph2 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.minorglyph3)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.glyphpic9 = My.Resources.empty
                Main.textminorglyph3 = ""
            End If
        Catch ex As Exception

        End Try



        'SekundärGlyphen
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.secprimeglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.secglyphpic1 = My.Resources.empty
                Main.sectextprimeglyph1 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.secprimeglyph2)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.secglyphpic2 = My.Resources.empty
                Main.sectextprimeglyph2 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.secprimeglyph3)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.secglyphpic3 = My.Resources.empty
                Main.sectextprimeglyph3 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.secmajorglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.secglyphpic4 = My.Resources.empty
                Main.sectextmajorglyph1 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.secmajorglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.secglyphpic5 = My.Resources.empty
                Main.sectextmajorglyph2 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.secmajorglyph3)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.secglyphpic6 = My.Resources.empty
                Main.sectextmajorglyph3 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.secminorglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.secglyphpic7 = My.Resources.empty
                Main.sectextminorglyph1 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.secminorglyph2)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.secglyphpic8 = My.Resources.empty
                Main.sectextminorglyph2 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.secminorglyph3)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.secglyphpic9 = My.Resources.empty
                Main.sectextminorglyph3 = ""
            End If
        Catch ex As Exception

        End Try
        Main.UseWaitCursor = False
        Application.DoEvents()
        wait.Close()
        MsgBox("Gegenstände wurden gefiltert!", MsgBoxStyle.Information, "Info")
        Main.BringToFront()
    End Sub
    Private Sub getglyph(ByVal attribut As String, ByVal ending As String, ByVal type As Integer, ByVal priority As String)
        Dim glyph As String = ""

        Try


            Dim quellcode88 As String = Main.talentpage
            Dim anfang88 As String = attribut
            Dim ende88 As String = ending
            Dim quellcodeSplit88 As String
            quellcodeSplit88 = Split(quellcode88, anfang88, 5)(1)
            quellcodeSplit88 = Split(quellcodeSplit88, ende88, 6)(0)
            If quellcodeSplit88.Contains("<span class=""name"">Glyphe &") Then
                'Enthält Glyphen
                Try
                    '1. glyph

                    Dim quellcodeyx88 As String = quellcodeSplit88
                    Dim anfangyx88 As String = "<a href=""/wow/de/item/"
                    Dim endeyx88 As String = """ class="""
                    Dim quellcodeSplityx88 As String
                    quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
                    quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)

                    Dim uquellcodeyx88 As String = quellcodeSplit88
                    Dim uanfangyx88 As String = "<span class=""name"">Glyphe &#39;"
                    Dim uendeyx88 As String = "&#39;</span>"
                    Dim uquellcodeSplityx88 As String
                    uquellcodeSplityx88 = Split(uquellcodeyx88, uanfangyx88, 5)(1)
                    uquellcodeSplityx88 = Split(uquellcodeSplityx88, uendeyx88, 6)(0)
                
                    Select Case type
                        Case 1
                            Glyphs.prim1.Text = uquellcodeSplityx88
                            Main.primeglyph1 = quellcodeSplityx88

                            Glyphs.prim1.Visible = True
                        Case 2
                            Glyphs.erheb1.Text = uquellcodeSplityx88
                            Main.majorglyph1 = quellcodeSplityx88
                            Glyphs.erheb1.Visible = True
                        Case 3
                            Glyphs.gering1.Text = uquellcodeSplityx88
                            Main.minorglyph1 = quellcodeSplityx88
                            Glyphs.gering1.Visible = True
                    End Select

                    Try
                        'getimage
                        Select Case type
                            Case 1
                                getimage(quellcodeSplityx88, Glyphs.prim1pic)
                            Case 2
                                getimage(quellcodeSplityx88, Glyphs.erheb1pic)
                            Case 3
                                getimage(quellcodeSplityx88, Glyphs.gering1pic)
                        End Select
                        Main.ProgressBar1.Value = 33
                        My.Application.DoEvents()
                    Catch ex As Exception

                    End Try
                Catch ex As Exception

                End Try
                Try
                    '2. glyph

                    Dim ressource1 As Integer = quellcodeSplit88.IndexOf(""" class=""color-")
                    Dim ressource2 As String = quellcodeSplit88.Remove(0, ressource1 + 15)
                    Dim ressource3 As Integer = ressource2.IndexOf(""" class=""color-")
                    Dim ressource4 As String = ressource2.Remove(0, ressource3 + 15)


                    Dim xXquellcodeyx88 As String = ressource2
                    Dim xXanfangyx88 As String = "<a href=""/wow/de/item/"
                    Dim xXendeyx88 As String = """ class=""color-"
                    Dim xXquellcodeSplityx88 As String
                    xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                    xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)

                    Dim uquellcodeyx88 As String = ressource4
                    Dim uanfangyx88 As String = "<span class=""name"">Glyphe &#39;"
                    Dim uendeyx88 As String = "&#39;</span>"
                    Dim uquellcodeSplityx88 As String
                    uquellcodeSplityx88 = Split(uquellcodeyx88, uanfangyx88, 5)(1)
                    uquellcodeSplityx88 = Split(uquellcodeSplityx88, uendeyx88, 6)(0)
                   
                    Select Case type
                        Case 1
                            Glyphs.prim2.Text = uquellcodeSplityx88
                            Main.primeglyph2 = xXquellcodeSplityx88
                            Glyphs.prim2.Visible = True
                        Case 2
                            Glyphs.erheb2.Text = uquellcodeSplityx88
                            Main.majorglyph2 = xXquellcodeSplityx88
                            Glyphs.erheb2.Visible = True
                        Case 3
                            Glyphs.gering2.Text = uquellcodeSplityx88
                            Main.minorglyph2 = xXquellcodeSplityx88
                            Glyphs.gering2.Visible = True
                    End Select
                    Try
                        'getimage
                        Select Case type
                            Case 1
                                getimage(xXquellcodeSplityx88, Glyphs.prim2pic)
                            Case 2
                                getimage(xXquellcodeSplityx88, Glyphs.erheb2pic)
                            Case 3
                                getimage(xXquellcodeSplityx88, Glyphs.gering2pic)
                        End Select
                        Main.ProgressBar1.Value = 66
                        My.Application.DoEvents()
                    Catch ex As Exception

                    End Try
                Catch ex As Exception

                End Try
                Try
                    '3. glyph
                    Dim ressource1 As Integer = quellcodeSplit88.IndexOf(""" class=""color-")
                    Dim ressource2 As String = quellcodeSplit88.Remove(0, ressource1 + 15)
                    Dim ressource3 As Integer = ressource2.IndexOf(""" class=""color-")
                    Dim ressource4 As String = ressource2.Remove(0, ressource3 + 15)
                    Dim ressource5 As Integer = ressource4.IndexOf(""" class=""color-")
                    Dim ressource6 As String = ressource4.Remove(0, ressource5 + 15)

                    Dim xXquellcodeyx88 As String = ressource4
                    Dim xXanfangyx88 As String = "<a href=""/wow/de/item/"
                    Dim xXendeyx88 As String = """ class=""color-"
                    Dim xXquellcodeSplityx88 As String
                    xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                    xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)

                    Dim uquellcodeyx88 As String = ressource6
                    Dim uanfangyx88 As String = "<span class=""name"">Glyphe &#39;"
                    Dim uendeyx88 As String = "&#39;</span>"
                    Dim uquellcodeSplityx88 As String
                    uquellcodeSplityx88 = Split(uquellcodeyx88, uanfangyx88, 5)(1)
                    uquellcodeSplityx88 = Split(uquellcodeSplityx88, uendeyx88, 6)(0)
                  
                    Select Case type
                        Case 1
                            Glyphs.prim3.Text = uquellcodeSplityx88
                            Main.primeglyph3 = xXquellcodeSplityx88
                            Glyphs.prim3.Visible = True
                        Case 2
                            Glyphs.erheb3.Text = uquellcodeSplityx88
                            Main.majorglyph3 = xXquellcodeSplityx88
                            Glyphs.erheb3.Visible = True
                        Case 3
                            Glyphs.gering3.Text = uquellcodeSplityx88
                            Main.minorglyph3 = xXquellcodeSplityx88
                            Glyphs.gering3.Visible = True
                    End Select
                    Try
                        'getimage
                        Select Case type
                            Case 1
                                getimage(xXquellcodeSplityx88, Glyphs.prim3pic)
                            Case 2
                                getimage(xXquellcodeSplityx88, Glyphs.erheb3pic)
                            Case 3
                                getimage(xXquellcodeSplityx88, Glyphs.gering3pic)
                        End Select
                        Main.ProgressBar1.Value = 100
                        My.Application.DoEvents()
                    Catch ex As Exception

                    End Try
                Catch ex As Exception

                End Try
            Else

            End If
        Catch

        End Try
    End Sub
    Private Sub getsecglyph(ByVal attribut As String, ByVal ending As String, ByVal type As Integer, ByVal priority As String)
        Dim glyph As String = ""
        Try


            Dim quellcode88 As String = Main.sectalentpage
            Dim anfang88 As String = attribut
            Dim ende88 As String = ending
            Dim quellcodeSplit88 As String
            quellcodeSplit88 = Split(quellcode88, anfang88, 5)(1)
            quellcodeSplit88 = Split(quellcodeSplit88, ende88, 6)(0)
            If quellcodeSplit88.Contains("<span class=""name"">Glyphe &") Then
                'Enthält Glyphen
                Try
                    '1. glyph

                    Dim quellcodeyx88 As String = quellcodeSplit88
                    Dim anfangyx88 As String = "<a href=""/wow/de/item/"
                    Dim endeyx88 As String = """ class="""
                    Dim quellcodeSplityx88 As String
                    quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
                    quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)

                    Dim uquellcodeyx88 As String = quellcodeSplit88
                    Dim uanfangyx88 As String = "<span class=""name"">Glyphe &#39;"
                    Dim uendeyx88 As String = "&#39;</span>"
                    Dim uquellcodeSplityx88 As String
                    uquellcodeSplityx88 = Split(uquellcodeyx88, uanfangyx88, 5)(1)
                    uquellcodeSplityx88 = Split(uquellcodeSplityx88, uendeyx88, 6)(0)
                   
                    Select Case type
                        Case 1
                            Glyphs.secprim1.Text = uquellcodeSplityx88
                            Main.secprimeglyph1 = quellcodeSplityx88
                            Glyphs.secprim1.Visible = True
                        Case 2
                            Glyphs.secerheb1.Text = uquellcodeSplityx88
                            Main.secmajorglyph1 = quellcodeSplityx88
                            Glyphs.secerheb1.Visible = True
                        Case 3
                            Glyphs.secgering1.Text = uquellcodeSplityx88
                            Main.secminorglyph1 = quellcodeSplityx88
                            Glyphs.secgering1.Visible = True
                    End Select

                    Try
                        'getimage
                        Select Case type
                            Case 1
                                getimage(quellcodeSplityx88, Glyphs.secprim1pic)
                            Case 2
                                getimage(quellcodeSplityx88, Glyphs.secerheb1pic)
                            Case 3
                                getimage(quellcodeSplityx88, Glyphs.secgering1pic)
                        End Select
                        Main.ProgressBar1.Value = 33
                        My.Application.DoEvents()
                    Catch ex As Exception

                    End Try
                Catch ex As Exception

                End Try
                Try
                    '2. glyph

                    Dim ressource1 As Integer = quellcodeSplit88.IndexOf(""" class=""color-")
                    Dim ressource2 As String = quellcodeSplit88.Remove(0, ressource1 + 15)
                    Dim ressource3 As Integer = ressource2.IndexOf(""" class=""color-")
                    Dim ressource4 As String = ressource2.Remove(0, ressource3 + 15)


                    Dim xXquellcodeyx88 As String = ressource2
                    Dim xXanfangyx88 As String = "<a href=""/wow/de/item/"
                    Dim xXendeyx88 As String = """ class=""color-"
                    Dim xXquellcodeSplityx88 As String
                    xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                    xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)

                    Dim uquellcodeyx88 As String = ressource4
                    Dim uanfangyx88 As String = "<span class=""name"">Glyphe &#39;"
                    Dim uendeyx88 As String = "&#39;</span>"
                    Dim uquellcodeSplityx88 As String
                    uquellcodeSplityx88 = Split(uquellcodeyx88, uanfangyx88, 5)(1)
                    uquellcodeSplityx88 = Split(uquellcodeSplityx88, uendeyx88, 6)(0)
                   
                    Select Case type
                        Case 1
                            Glyphs.secprim2.Text = uquellcodeSplityx88
                            Main.secprimeglyph2 = xXquellcodeSplityx88
                            Glyphs.secprim2.Visible = True
                        Case 2
                            Glyphs.secerheb2.Text = uquellcodeSplityx88
                            Main.secmajorglyph2 = xXquellcodeSplityx88
                            Glyphs.secerheb2.Visible = True
                        Case 3
                            Glyphs.secgering2.Text = uquellcodeSplityx88
                            Main.secminorglyph2 = xXquellcodeSplityx88
                            Glyphs.secgering2.Visible = True
                    End Select
                    Try
                        'getimage
                        Select Case type
                            Case 1
                                getimage(xXquellcodeSplityx88, Glyphs.secprim2pic)
                            Case 2
                                getimage(xXquellcodeSplityx88, Glyphs.secerheb2pic)
                            Case 3
                                getimage(xXquellcodeSplityx88, Glyphs.secgering2pic)
                        End Select
                        Main.ProgressBar1.Value = 66
                        My.Application.DoEvents()
                    Catch ex As Exception

                    End Try
                Catch ex As Exception

                End Try
                Try
                    '3. glyph
                    Dim ressource1 As Integer = quellcodeSplit88.IndexOf(""" class=""color-")
                    Dim ressource2 As String = quellcodeSplit88.Remove(0, ressource1 + 15)
                    Dim ressource3 As Integer = ressource2.IndexOf(""" class=""color-")
                    Dim ressource4 As String = ressource2.Remove(0, ressource3 + 15)
                    Dim ressource5 As Integer = ressource4.IndexOf(""" class=""color-")
                    Dim ressource6 As String = ressource4.Remove(0, ressource5 + 15)

                    Dim xXquellcodeyx88 As String = ressource4
                    Dim xXanfangyx88 As String = "<a href=""/wow/de/item/"
                    Dim xXendeyx88 As String = """ class=""color-"
                    Dim xXquellcodeSplityx88 As String
                    xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                    xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)

                    Dim uquellcodeyx88 As String = ressource6
                    Dim uanfangyx88 As String = "<span class=""name"">Glyphe &#39;"
                    Dim uendeyx88 As String = "&#39;</span>"
                    Dim uquellcodeSplityx88 As String
                    uquellcodeSplityx88 = Split(uquellcodeyx88, uanfangyx88, 5)(1)
                    uquellcodeSplityx88 = Split(uquellcodeSplityx88, uendeyx88, 6)(0)
                   
                    Select Case type
                        Case 1
                            Glyphs.secprim3.Text = uquellcodeSplityx88
                            Main.secprimeglyph3 = xXquellcodeSplityx88
                            Glyphs.secprim3.Visible = True
                        Case 2
                            Glyphs.secerheb3.Text = uquellcodeSplityx88
                            Main.secmajorglyph3 = xXquellcodeSplityx88
                            Glyphs.secerheb3.Visible = True
                        Case 3
                            Glyphs.secgering3.Text = uquellcodeSplityx88
                            Main.secminorglyph3 = xXquellcodeSplityx88
                            Glyphs.secgering3.Visible = True
                    End Select
                    Try
                        'getimage
                        Select Case type
                            Case 1
                                getimage(xXquellcodeSplityx88, Glyphs.secprim3pic)
                            Case 2
                                getimage(xXquellcodeSplityx88, Glyphs.secerheb3pic)
                            Case 3
                                getimage(xXquellcodeSplityx88, Glyphs.secgering3pic)
                        End Select
                        Main.ProgressBar1.Value = 100
                        My.Application.DoEvents()
                    Catch ex As Exception

                    End Try
                Catch ex As Exception

                End Try
            Else

            End If
        Catch

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
    Private Sub getvz(ByVal slot As Integer)
        Main.xstatus.Text = "Loading Enchants..."
        Dim starting As String = ""
        Dim ending = "<div data-id="""
        '    Dim labelname
        Select Case slot
            Case 1
                starting = "<div data-id=""0"" data-type="
            Case 2
                starting = "<div data-id=""1"" data-type="
            Case 3
                starting = "<div data-id=""2"" data-type="
            Case 4
                starting = "<div data-id=""14"" data-type="
            Case 5
                starting = "<div data-id=""4"" data-type="
            Case 6
                starting = "<div data-id=""3"" data-type="
            Case 7
                starting = "<div data-id=""18"" data-type="
            Case 8
                starting = "<div data-id=""8"" data-type="
            Case 9
                starting = "<div data-id=""15"" data-type="
            Case 10
                starting = "<div data-id=""16"" data-type="
            Case 11
                starting = "<div data-id=""17"" data-type="
                ending = " <script type=""text/javascript"">"
            Case 12
                starting = "<div data-id=""9"" data-type="
            Case 13
                starting = "<div data-id=""5"" data-type="
            Case 14
                starting = "<div data-id=""6"" data-type="
            Case 15
                starting = "<div data-id=""7"" data-type="
            Case 16
                starting = "<div data-id=""10"" data-type="
            Case 17
                starting = "<div data-id=""11"" data-type="
            Case 18
                starting = "<div data-id=""12"" data-type="
            Case 19
                starting = "<div data-id=""13"" data-type="

        End Select
        Try

            Dim quellcode88 As String = Main.quelltext
            Dim anfang88 As String = starting
            Dim ende88 As String = ending
            Dim quellcodeSplit88 As String
            quellcodeSplit88 = Split(quellcode88, anfang88, 5)(1)
            quellcodeSplit88 = Split(quellcodeSplit88, ende88, 6)(0)
            If quellcodeSplit88.Contains("<span class=""enchant-") Then
                'verzaubert

                Dim quellcodeyx88 As String = quellcodeSplit88
                Dim anfangyx88 As String = "<span class=""enchant-"
                Dim endeyx88 As String = "<span class=""level"">"
                Dim quellcodeSplityx88 As String
                quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
                quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)

                If quellcodeSplityx88.Contains("<a href=""/wow/de/item/") Then


                    Dim xXquellcodeyx88 As String = quellcodeSplityx88
                    Dim xXanfangyx88 As String = "<a href=""/wow/de/item/"
                    Dim xXendeyx88 As String = """>"
                    Dim xXquellcodeSplityx88 As String
                    xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                    xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                    If xXquellcodeSplityx88.Contains("Ã¼") Then xXquellcodeSplityx88.Replace("Ã¼", "ü")
                    If xXquellcodeSplityx88.Contains("Ã¤") Then xXquellcodeSplityx88.Replace("Ã¤", "ä")
                    If xXquellcodeSplityx88.Contains("Ã¶") Then xXquellcodeSplityx88.Replace("Ã¶", "")
                    If xXquellcodeSplityx88.Contains("ÃŸ") Then xXquellcodeSplityx88.Replace("ÃŸ", "")
                    Dim vzid As String = Int(Val(xXquellcodeSplityx88))
                    Select Case slot
                        Case 1
                            With Main.kopfvz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.kopfvzid = getspellidfromitem(vzid)
                        Case 2
                            With Main.halsvz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.halsvzid = getspellidfromitem(vzid)
                        Case 3
                            With Main.schultervz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.schultervzid = getspellidfromitem(vzid)
                        Case 4
                            With Main.rueckenvz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.rueckenvzid = getspellidfromitem(vzid)
                        Case 5
                            With Main.brustvz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.brustvzid = getspellidfromitem(vzid)
                        Case 6

                        Case 7

                        Case 8
                            With Main.handgelenkevz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.handgelenkevzid = getspellidfromitem(vzid)
                        Case 9
                            With Main.hauptvz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            With Main.hauptvzlabel2
                                .Visible = True
                                .Text = Main.hauptvz.Text
                            End With
                            Main.hauptvzid = getspellidfromitem(vzid)
                        Case 10
                            With Main.offvz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            With Main.offvzlabel2
                                .Visible = True
                                .Text = Main.offvz.Text
                            End With
                            Main.offvzid = getspellidfromitem(vzid)
                        Case 11
                            With Main.distanzvz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            With Main.distanzvzlabel2
                                .Visible = True
                                .Text = Main.distanzvz.Text
                            End With
                            Main.distanzvzid = getspellidfromitem(vzid)
                        Case 12
                            With Main.haendevz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.haendevzid = getspellidfromitem(vzid)
                        Case 13
                            With Main.guertelvz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.guertelvzid = getspellidfromitem(vzid)
                        Case 14
                            With Main.beinevz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.beinevzid = getspellidfromitem(vzid)
                        Case 15
                            With Main.stiefelvz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.stiefelvzid = getspellidfromitem(vzid)
                        Case 16
                            With Main.ring1vz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.ring1vzid = getspellidfromitem(vzid)
                        Case 17
                            With Main.ring2vz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.ring2vzid = getspellidfromitem(vzid)
                        Case 18
                            With Main.schmuck1vz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.schmuck1vzid = getspellidfromitem(vzid)
                        Case 19
                            With Main.schmuck2vz
                                .Visible = True
                                .Text = getvzeffectname(getspellidfromitem(vzid))
                            End With
                            Main.schmuck2vzid = getspellidfromitem(vzid)
                    End Select


                ElseIf quellcodeSplit88.Contains("<span class=""tip""") Then
                    'verzaubert

                    Dim xXquellcodeyx88 As String = quellcodeSplityx88
                    Dim xXanfangyx88 As String = "<span class=""tip"" data-spell="""
                    Dim xXendeyx88 As String = """>"
                    Dim xXquellcodeSplityx88 As String
                    xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                    xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                    If xXquellcodeSplityx88.Contains("Ã¼") Then xXquellcodeSplityx88.Replace("Ã¼", "ü")
                    If xXquellcodeSplityx88.Contains("Ã¤") Then xXquellcodeSplityx88.Replace("Ã¤", "ä")
                    If xXquellcodeSplityx88.Contains("Ã¶") Then xXquellcodeSplityx88.Replace("Ã¶", "")
                    If xXquellcodeSplityx88.Contains("ÃŸ") Then xXquellcodeSplityx88.Replace("ÃŸ", "")
                    Dim vzid As String = Int(Val(xXquellcodeSplityx88))
                    Select Case slot
                        Case 1
                            With Main.kopfvz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.kopfvzid = vzid
                        Case 2
                            With Main.halsvz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.halsvzid = vzid
                        Case 3
                            With Main.schultervz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.schultervzid = vzid
                        Case 4
                            With Main.rueckenvz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.rueckenvzid = vzid
                        Case 5
                            With Main.brustvz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.brustvzid = vzid
                        Case 6

                        Case 7

                        Case 8
                            With Main.handgelenkevz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.handgelenkevzid = vzid
                        Case 9
                            With Main.hauptvz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.hauptvzid = vzid
                        Case 10
                            With Main.offvz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.offvzid = vzid
                        Case 11
                            With Main.distanzvz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.distanzvzid = vzid
                        Case 12
                            With Main.haendevz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.haendevzid = vzid
                        Case 13
                            With Main.guertelvz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.guertelvzid = vzid
                        Case 14
                            With Main.beinevz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.beinevzid = vzid
                        Case 15
                            With Main.stiefelvz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.stiefelvzid = vzid
                        Case 16
                            With Main.ring1vz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.ring1vzid = vzid
                        Case 17
                            With Main.ring2vz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.ring2vzid = vzid
                        Case 18
                            With Main.schmuck1vz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.schmuck1vzid = vzid
                        Case 19
                            With Main.schmuck2vz
                                .Visible = True
                                .Text = getspellnamefromid(vzid)
                            End With
                            Main.schmuck2vzid = vzid
                    End Select
                Else

                End If
            Else

            End If
        Catch

        End Try
        Main.ProgressBar1.Value += 100 / 19
        My.Application.DoEvents()
    End Sub
    Private Sub getgem(ByVal slot As Integer)
        Main.xstatus.Text = "Loading Gems..."
        Dim starting As String = ""
        Dim ending = "<div data-id="""
        '   Dim labelname
        Select Case slot
            Case 1
                starting = "<div data-id=""0"" data-type="
            Case 2
                starting = "<div data-id=""1"" data-type="
            Case 3
                starting = "<div data-id=""2"" data-type="
            Case 4
                starting = "<div data-id=""14"" data-type="
            Case 5
                starting = "<div data-id=""4"" data-type="
            Case 6
                starting = "<div data-id=""3"" data-type="
            Case 7
                starting = "<div data-id=""18"" data-type="
            Case 8
                starting = "<div data-id=""8"" data-type="
            Case 9
                starting = "<div data-id=""15"" data-type="
            Case 10
                starting = "<div data-id=""16"" data-type="
            Case 11
                starting = "<div data-id=""17"" data-type="
                ending = " <script type=""text/javascript"">"
            Case 12
                starting = "<div data-id=""9"" data-type="
            Case 13
                starting = "<div data-id=""5"" data-type="
            Case 14
                starting = "<div data-id=""6"" data-type="
            Case 15
                starting = "<div data-id=""7"" data-type="
            Case 16
                starting = "<div data-id=""10"" data-type="
            Case 17
                starting = "<div data-id=""11"" data-type="
            Case 18
                starting = "<div data-id=""12"" data-type="
            Case 19
                starting = "<div data-id=""13"" data-type="

        End Select
        Try

            Dim quellcode88 As String = Main.quelltext
            Dim anfang88 As String = starting
            Dim ende88 As String = ending
            Dim quellcodeSplit88 As String
            quellcodeSplit88 = Split(quellcode88, anfang88, 5)(1)
            quellcodeSplit88 = Split(quellcodeSplit88, ende88, 6)(0)
            If quellcodeSplit88.Contains("<span class=""sockets"">") Then
                'Item gesockelt
                Dim socket1 As String = ""
                Dim socket2 As String = ""
                Dim socket3 As String = ""
                Dim quellcodey88 As String = quellcodeSplit88
                Dim anfangy88 As String = "<span class=""icon-socket socket-"
                Dim endey88 As String = "</a>"
                Dim quellcodeSplity88 As String
                quellcodeSplity88 = Split(quellcodey88, anfangy88, 5)(1)
                quellcodeSplity88 = Split(quellcodeSplity88, endey88, 6)(0)


                Dim quellcodeyx88 As String = quellcodeSplity88
                Dim anfangyx88 As String = "<a href=""/wow/de/item/"
                Dim endeyx88 As String = """ class=""gem"">"
                Dim quellcodeSplityx88 As String
                quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
                quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
                If quellcodeSplityx88.Contains("Ã¼") Then quellcodeSplityx88.Replace("Ã¼", "ü")
                If quellcodeSplityx88.Contains("Ã¤") Then quellcodeSplityx88.Replace("Ã¤", "ä")
                If quellcodeSplityx88.Contains("Ã¶") Then quellcodeSplityx88.Replace("Ã¶", "")
                If quellcodeSplityx88.Contains("ÃŸ") Then quellcodeSplityx88.Replace("ÃŸ", "")
                socket1 = quellcodeSplityx88
                Dim sockettext As String = "Platz leer"
                Select Case slot
                    Case 1
                        With Main.kopfsocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.kopfsocket1id = socket1
                    Case 2
                        With Main.halssocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.halssocket1id = socket1
                    Case 3
                        With Main.schultersocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.schultersocket1id = socket1
                    Case 4
                        With Main.rueckensocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.rueckensocket1id = socket1
                    Case 5
                        With Main.brustsocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.brustsocket1id = socket1
                    Case 6

                    Case 7

                    Case 8
                        With Main.Handgelenkesocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.handgelenkesocket1id = socket1
                    Case 9
                        With Main.Hauptsocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.hauptsocket1id = socket1
                    Case 10
                        With Main.Offsocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.offsocket1id = socket1
                    Case 11
                        With Main.Distanzsocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.distanzsocket1id = socket1
                    Case 12
                        With Main.haendesocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.haendesocket1id = socket1
                    Case 13
                        With Main.guertelsocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.guertelsocket1id = socket1
                    Case 14
                        With Main.beinesocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.beinesocket1id = socket1
                    Case 15
                        With Main.stiefelsocket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.stiefelsocket1id = socket1
                    Case 16
                        With Main.Ring1socket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.ring1socket1id = socket1
                    Case 17
                        With Main.ring2socket1
                            .Visible = True
                            .Text = getsocketeffectname(socket1)
                        End With
                        Main.ring2socket1id = socket1
                    Case 18

                    Case 19

                End Select



                Dim xquellcodey88 As String = quellcodeSplit88
                Dim xanfangy88 As String = "<span class=""sockets"">"
                Dim xendey88 As String = "</div>"
                Dim xquellcodeSplity88 As String
                xquellcodeSplity88 = Split(xquellcodey88, xanfangy88, 5)(1)
                xquellcodeSplity88 = Split(xquellcodeSplity88, xendey88, 6)(0)

                Dim yxquellcodey88 As String = xquellcodeSplity88
                Dim yxanfangy88 As String = """ alt="""" />"
                Dim yxendey88 As String = "</div>"
                Dim yxquellcodeSplity88 As String
                yxquellcodeSplity88 = Split(yxquellcodey88, yxanfangy88, 5)(1)
                yxquellcodeSplity88 = Split(yxquellcodeSplity88, yxendey88, 6)(0)

                If yxquellcodeSplity88.Contains("<span class=""icon-socket socket-") Then
                    '2 Sockel

                    Dim Xquellcodeyx88 As String = yxquellcodeSplity88
                    Dim Xanfangyx88 As String = "<a href=""/wow/de/item/"
                    Dim Xendeyx88 As String = """ class=""gem"">"
                    Dim XquellcodeSplityx88 As String
                    XquellcodeSplityx88 = Split(Xquellcodeyx88, Xanfangyx88, 5)(1)
                    XquellcodeSplityx88 = Split(XquellcodeSplityx88, Xendeyx88, 6)(0)

                    If XquellcodeSplityx88.Contains("Ã¼") Then XquellcodeSplityx88.Replace("Ã¼", "ü")
                    If XquellcodeSplityx88.Contains("Ã¤") Then XquellcodeSplityx88.Replace("Ã¤", "ä")
                    If XquellcodeSplityx88.Contains("Ã¶") Then XquellcodeSplityx88.Replace("Ã¶", "")
                    If XquellcodeSplityx88.Contains("ÃŸ") Then XquellcodeSplityx88.Replace("ÃŸ", "")
                    socket2 = XquellcodeSplityx88
                    Select Case slot
                        Case 1
                            With Main.kopfsocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.kopfsocket2id = socket2
                        Case 2
                            With Main.halssocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.halssocket2id = socket2
                        Case 3
                            With Main.schultersocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.schultersocket2id = socket2
                        Case 4
                            With Main.rueckensocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.rueckensocket2id = socket2
                        Case 5
                            With Main.brustsocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.brustsocket2id = socket2
                        Case 6

                        Case 7

                        Case 8
                            With Main.handgelenkesocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.handgelenkesocket2id = socket2
                        Case 9
                            With Main.Hauptsocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.hauptsocket2id = socket2
                        Case 10
                            With Main.Offsocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.offsocket2id = socket2
                        Case 11
                            With Main.Distanzsocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.distanzsocket2id = socket2
                        Case 12
                            With Main.haendesocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.haendesocket2id = socket2
                        Case 13
                            With Main.guertelsocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.guertelsocket2id = socket2
                        Case 14
                            With Main.beinesocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.beinesocket2id = socket2
                        Case 15
                            With Main.stiefelsocket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.stiefelsocket2id = socket2
                        Case 16
                            With Main.ring1socket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.ring1socket2id = socket2
                        Case 17
                            With Main.ring2socket2
                                .Visible = True
                                .Text = getsocketeffectname(socket2)
                            End With
                            Main.ring2socket2id = socket2
                        Case 18

                        Case 19

                    End Select
                    Dim ressource1 As Integer = quellcodeSplit88.IndexOf(""" class=""gem"">")
                    Dim ressource2 As String = quellcodeSplit88.Remove(0, ressource1 + 14)
                    Dim ressource3 As Integer = ressource2.IndexOf(""" class=""gem"">")
                    Dim ressource4 As String = ressource2.Remove(0, ressource3 + 14)

                    If ressource4.Contains("<span class=""icon-socket socket-") Then
                        Dim xXquellcodeyx88 As String = ressource4
                        Dim xXanfangyx88 As String = "<a href=""/wow/de/item/"
                        Dim xXendeyx88 As String = """ class=""gem"">"
                        Dim xXquellcodeSplityx88 As String
                        xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                        xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                        If xXquellcodeSplityx88.Contains("Ã¼") Then xXquellcodeSplityx88.Replace("Ã¼", "ü")
                        If xXquellcodeSplityx88.Contains("Ã¤") Then xXquellcodeSplityx88.Replace("Ã¤", "ä")
                        If xXquellcodeSplityx88.Contains("Ã¶") Then xXquellcodeSplityx88.Replace("Ã¶", "")
                        If xXquellcodeSplityx88.Contains("ÃŸ") Then xXquellcodeSplityx88.Replace("ÃŸ", "")
                        socket3 = xXquellcodeSplityx88
                        Select Case slot
                            Case 1
                                With Main.kopfsocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.kopfsocket3id = socket3
                            Case 2
                                With Main.halssocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.halssocket3id = socket3
                            Case 3
                                With Main.schultersocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.schultersocket3id = socket3
                            Case 4
                                With Main.rueckensocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.rueckensocket3id = socket3
                            Case 5
                                With Main.brustsocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.brustsocket3id = socket3
                            Case 6

                            Case 7

                            Case 8
                                With Main.Handgelenkesocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.handgelenkesocket3id = socket3
                            Case 9
                                With Main.hauptsocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.hauptsocket3id = socket3
                            Case 10
                                With Main.offsocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.offsocket3id = socket3
                            Case 11
                                With Main.distanzsocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.distanzsocket3id = socket3
                            Case 12
                                With Main.haendesocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.haendesocket3id = socket3
                            Case 13
                                With Main.guertelsocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.guertelsocket3id = socket3
                            Case 14
                                With Main.beinesocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.beinesocket3id = socket3
                            Case 15
                                With Main.stiefelsocket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.stiefelsocket3id = socket3
                            Case 16
                                With Main.ring1socket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.ring1socket3id = socket3
                            Case 17
                                With Main.ring2socket3
                                    .Visible = True
                                    .Text = getsocketeffectname(socket3)
                                End With
                                Main.ring2socket3id = socket3
                            Case 18

                            Case 19

                        End Select
                    Else


                    End If
                Else

                End If

            Else

            End If





        Catch

        End Try
        Main.ProgressBar1.Value += 100 / 19
        My.Application.DoEvents()
    End Sub
    Private Sub getitem(ByVal slot As Integer)
        Main.xstatus.Text = "Loading Items..."
        Dim starting As String = ""
        '    Dim labelname
        Select Case slot
            Case 1
                starting = "<div data-id=""0"" data-type="
            Case 2
                starting = "<div data-id=""1"" data-type="
            Case 3
                starting = "<div data-id=""2"" data-type="
            Case 4
                starting = "<div data-id=""14"" data-type="
            Case 5
                starting = "<div data-id=""4"" data-type="
            Case 6
                starting = "<div data-id=""3"" data-type="
            Case 7
                starting = "<div data-id=""18"" data-type="
            Case 8
                starting = "<div data-id=""8"" data-type="
            Case 9
                starting = "<div data-id=""15"" data-type="
            Case 10
                starting = "<div data-id=""16"" data-type="
            Case 11
                starting = "<div data-id=""17"" data-type="
            Case 12
                starting = "<div data-id=""9"" data-type="
            Case 13
                starting = "<div data-id=""5"" data-type="
            Case 14
                starting = "<div data-id=""6"" data-type="
            Case 15
                starting = "<div data-id=""7"" data-type="
            Case 16
                starting = "<div data-id=""10"" data-type="
            Case 17
                starting = "<div data-id=""11"" data-type="
            Case 18
                starting = "<div data-id=""12"" data-type="
            Case 19
                starting = "<div data-id=""13"" data-type="

        End Select
        Try

            Dim quellcode88 As String = Main.quelltext
            Dim anfang88 As String = starting
            Dim ende88 As String = "<span class=""name color-q"
            Dim quellcodeSplit88 As String
            quellcodeSplit88 = Split(quellcode88, anfang88, 5)(1)
            quellcodeSplit88 = Split(quellcodeSplit88, ende88, 6)(0)
            If quellcodeSplit88.Contains("<a href=""javascript:;"" class=""empty""") Then
                Dim quellcodeSplity88 As String = "Platz leer"
                Select Case slot
                    Case 1
                        Main.Kopf.Text = quellcodeSplity88

                    Case 2
                        Main.Hals.Text = quellcodeSplity88

                    Case 3
                        Main.Schulter.Text = quellcodeSplity88

                    Case 4
                        Main.Ruecken.Text = quellcodeSplity88

                    Case 5
                        Main.Brust.Text = quellcodeSplity88

                    Case 6
                        Main.Hemd.Text = quellcodeSplity88

                    Case 7
                        Main.Wappenrock.Text = quellcodeSplity88

                    Case 8
                        Main.Handgelenke.Text = quellcodeSplity88

                    Case 9
                        Main.Haupt.Text = quellcodeSplity88

                    Case 10
                        Main.Off.Text = quellcodeSplity88

                    Case 11
                        Main.Distanz.Text = quellcodeSplity88

                    Case 12
                        Main.Haende.Text = quellcodeSplity88

                    Case 13
                        Main.Guertel.Text = quellcodeSplity88

                    Case 14
                        Main.Beine.Text = quellcodeSplity88

                    Case 15
                        Main.Stiefel.Text = quellcodeSplity88

                    Case 16
                        Main.Ring1.Text = quellcodeSplity88

                    Case 17
                        Main.Ring2.Text = quellcodeSplity88

                    Case 18
                        Main.Schmuck1.Text = quellcodeSplity88

                    Case 19
                        Main.Schmuck2.Text = quellcodeSplity88

                End Select
            Else
                Dim quellcodey88 As String = quellcodeSplit88
                Dim anfangy88 As String = "/item/"
                Dim endey88 As String = """ class=""item"""
                Dim quellcodeSplity88 As String
                quellcodeSplity88 = Split(quellcodey88, anfangy88, 5)(1)
                quellcodeSplity88 = Split(quellcodeSplity88, endey88, 6)(0)
                quellcodeSplity88 = Int(Val(quellcodeSplity88))
                Dim quellcodey89 As String = quellcodeSplit88
                Dim anfangy89 As String = "><img src="""
                Dim endey89 As String = ".jpg"""
                Dim quellcodeSplity89 As String
                quellcodeSplity89 = Split(quellcodey89, anfangy89, 5)(1)
                quellcodeSplity89 = Split(quellcodeSplity89, endey89, 6)(0) & ".jpg"
                Dim xquellcodesplity89 As String = ""
                Try
                    Dim oxquellcodey89 As String = quellcodeSplit88
                    Dim oxanfangy89 As String = "<span class=""name-shadow"">"
                    Dim oxendey89 As String = "</span>"
                    Dim oxquellcodeSplity89 As String
                    oxquellcodeSplity89 = Split(oxquellcodey89, oxanfangy89, 5)(1)
                    oxquellcodeSplity89 = Split(oxquellcodeSplity89, oxendey89, 6)(0)
                    xquellcodesplity89 = oxquellcodeSplity89
                    If xquellcodesplity89.Contains("Ã¼") Then xquellcodesplity89 = xquellcodesplity89.Replace("Ã¼", "ü")
                    If xquellcodesplity89.Contains("Ã¤") Then xquellcodesplity89 = xquellcodesplity89.Replace("Ã¤", "ä")
                    If xquellcodesplity89.Contains("Ã¶") Then xquellcodesplity89 = xquellcodesplity89.Replace("Ã¶", "ö")
                    If xquellcodesplity89.Contains("ÃŸ") Then xquellcodesplity89 = xquellcodesplity89.Replace("ÃŸ", "ß")
                Catch ex As Exception
                    xquellcodesplity89 = "Fehler"
                End Try


                '     Dim clienyx88 As New WebClient
                '     Dim quellcodeyx88 As String = clienyx88.DownloadString("http://wowdata.buffed.de/?i=" & quellcodeSplity88)
                '   Dim anfangyx88 As String = "href=""/?i=" & quellcodeSplity88 & """><img src="""
                '   Dim endeyx88 As String = """></a></td>"
                '  Dim quellcodeSplityx88 As String
                '    quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
                ' "http://wowdata.buffed.de" & Split(quellcodeSplityx88, endeyx88, 6)(0)

                Dim quellcodeSplityx88 = quellcodeSplity89
                Select Case slot
                    Case 1
                        Main.Kopf.Text = xquellcodesplity89
                        Main.Kopf.Visible = True
                        Main.kopfid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.kopfpic)
                    Case 2
                        Main.Hals.Text = xquellcodesplity89
                        Main.Hals.Visible = True
                        Main.halsid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Halspic)
                    Case 3
                        Main.Schulter.Text = xquellcodesplity89
                        Main.Schulter.Visible = True
                        Main.schulterid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Schulterpic)
                    Case 4
                        Main.Ruecken.Text = xquellcodesplity89
                        Main.Ruecken.Visible = True
                        Main.rueckenid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Rueckenpic)
                    Case 5
                        Main.Brust.Text = xquellcodesplity89
                        Main.Brust.Visible = True
                        Main.brustid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Brustpic)
                    Case 6
                        Main.Hemd.Text = xquellcodesplity89
                        Main.Hemd.Visible = True
                        Main.hemdid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Hemdpic)
                    Case 7
                        Main.Wappenrock.Text = xquellcodesplity89
                        Main.Wappenrock.Visible = True
                        Main.wappenrockid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Wappenrockpic)
                    Case 8
                        Main.Handgelenke.Text = xquellcodesplity89
                        Main.Handgelenke.Visible = True
                        Main.handgelenkeid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Handgelenkepic)
                    Case 9
                        Main.Haupt.Text = xquellcodesplity89
                        Main.Haupt.Visible = True
                        Main.hauptid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Hauptpic)
                        getweapontype(quellcodeSplity88)
                    Case 10
                        Main.Off.Text = xquellcodesplity89
                        Main.Off.Visible = True
                        Main.offid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Offpic)
                        getweapontype(quellcodeSplity88)
                    Case 11
                        Main.Distanz.Text = xquellcodesplity89
                        Main.Distanz.Visible = True
                        Main.distanzid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Distanzpic)
                        getweapontype(quellcodeSplity88)
                    Case 12
                        Main.Haende.Text = xquellcodesplity89
                        Main.Haende.Visible = True
                        Main.haendeid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Haendepic)
                    Case 13
                        Main.Guertel.Text = xquellcodesplity89
                        Main.Guertel.Visible = True
                        Main.guertelid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Guertelpic)
                    Case 14
                        Main.Beine.Text = xquellcodesplity89
                        Main.Beine.Visible = True
                        Main.beineid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Beinepic)
                    Case 15
                        Main.Stiefel.Text = xquellcodesplity89
                        Main.Stiefel.Visible = True
                        Main.stiefelid = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Stiefelpic)
                    Case 16
                        Main.Ring1.Text = xquellcodesplity89
                        Main.Ring1.Visible = True
                        Main.ring1id = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Ring1pic)
                    Case 17
                        Main.Ring2.Text = xquellcodesplity89
                        Main.Ring2.Visible = True
                        Main.ring2id = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Ring2pic)
                    Case 18
                        Main.Schmuck1.Text = xquellcodesplity89
                        Main.Schmuck1.Visible = True
                        Main.schmuck1id = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Schmuck1pic)
                    Case 19
                        Main.Schmuck2.Text = xquellcodesplity89
                        Main.Schmuck2.Visible = True
                        Main.schmuck2id = quellcodeSplity88
                        LoadImageFromUrl(quellcodeSplityx88, Main.Schmuck2pic)
                End Select
            End If


        Catch ex As Exception
            MsgBox(ex.ToString)
            Select Case slot
                Case 1
                    Main.Kopf.Text = "Fehler! =)"
                Case 2
                    Main.Hals.Text = "Fehler! =)"
                Case 3
                    Main.Schulter.Text = "Fehler! =)"
                Case 4
                    Main.Ruecken.Text = "Fehler! =)"
                Case 5
                    Main.Brust.Text = "Fehler! =)"
                Case 6
                    Main.Hemd.Text = "Fehler! =)"
                Case 7
                    Main.Wappenrock.Text = "Fehler! =)"
                Case 8
                    Main.Handgelenke.Text = "Fehler! =)"
                Case 9
                    Main.Haupt.Text = "Fehler! =)"
                Case 10
                    Main.Off.Text = "Fehler! =)"
                Case 11
                    Main.Distanz.Text = "Fehler! =)"
                Case 12
                    Main.Haende.Text = "Fehler! =)"
                Case 13
                    Main.Guertel.Text = "Fehler! =)"
                Case 14
                    Main.Beine.Text = "Fehler! =)"
                Case 15
                    Main.Stiefel.Text = "Fehler! =)"
                Case 16
                    Main.Ring1.Text = "Fehler! =)"
                Case 17
                    Main.Ring2.Text = "Fehler! =)"
                Case 18
                    Main.Schmuck1.Text = "Fehler! =)"
                Case 19
                    Main.Schmuck2.Text = "Fehler! =)"
            End Select
        End Try
        Main.ProgressBar1.Value += 100 / 19
        My.Application.DoEvents()
    End Sub
    Public Sub LoadImageFromUrl(ByRef url As String, ByVal pb As PictureBox)
        Dim request As Net.HttpWebRequest = DirectCast(Net.HttpWebRequest.Create(url), Net.HttpWebRequest)
        Dim response As Net.HttpWebResponse = DirectCast(request.GetResponse, Net.HttpWebResponse)
        Dim img As Image = Image.FromStream(response.GetResponseStream())
        response.Close()
        pb.SizeMode = PictureBoxSizeMode.StretchImage
        pb.Image = img
    End Sub

    Public Sub getweapontype(ByVal itemid As Integer)
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & itemid.ToString)
            Dim anfangyx88 As String = "description"" content="""
            Dim endeyx88 As String = """ /><meta"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)

            Select Case True
                Case quellcodeSplityx88.ToLower.Contains(" crossbow ")
                    Main.specialspells.Add("5011")
                    Main.specialskills.Add("226")
                Case quellcodeSplityx88.ToLower.Contains(" bow ")
                    Main.specialspells.Add("264")
                    Main.specialskills.Add("45")
                Case quellcodeSplityx88.ToLower.Contains(" gun ")
                    Main.specialspells.Add("266")
                    Main.specialskills.Add("46")
                Case quellcodeSplityx88.ToLower.Contains(" thrown ")
                    Main.specialspells.Add("2764")
                    Main.specialspells.Add("2567")
                    Main.specialskills.Add("176")
                Case quellcodeSplityx88.ToLower.Contains(" wands ")
                    Main.specialspells.Add("5009")
                    Main.specialspells.Add("5019")
                    Main.specialskills.Add("228")
                Case quellcodeSplityx88.ToLower.Contains(" sword ")
                    If quellcodeSplityx88.ToLower.Contains(" one-handed ") Then
                        Main.specialspells.Add("201")
                        Main.specialskills.Add("43")
                    Else
                        Main.specialspells.Add("201")
                        Main.specialskills.Add("43")
                        Main.specialspells.Add("202")
                        Main.specialskills.Add("55")
                    End If
                Case quellcodeSplityx88.ToLower.Contains(" dagger ")
                    Main.specialspells.Add("1180")
                    Main.specialskills.Add("173")
                Case quellcodeSplityx88.ToLower.Contains(" axe ")
                    If quellcodeSplityx88.ToLower.Contains(" one-handed ") Then
                        Main.specialspells.Add("196")
                        Main.specialskills.Add("44")
                    Else
                        Main.specialspells.Add("197")
                        Main.specialskills.Add("44")
                        Main.specialspells.Add("196")
                        Main.specialskills.Add("142")
                    End If
                Case quellcodeSplityx88.ToLower.Contains(" mace ")
                    If quellcodeSplityx88.ToLower.Contains(" one-handed ") Then
                        Main.specialspells.Add("198")
                        Main.specialskills.Add("54")
                    Else
                        Main.specialskills.Add("54")
                        Main.specialspells.Add("198")
                        Main.specialskills.Add("160")
                        Main.specialspells.Add("199")
                    End If
                Case quellcodeSplityx88.ToLower.Contains(" polearm ")
                    Main.specialspells.Add("200")
                    Main.specialskills.Add("229")
                Case quellcodeSplityx88.ToLower.Contains(" staff ")
                    Main.specialspells.Add("227")
                    Main.specialskills.Add("136")
             
                Case Else

            End Select
        Catch ex As Exception

        End Try

    End Sub

End Class
