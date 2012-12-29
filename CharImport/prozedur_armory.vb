'Copyright (C) 2011-2012 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* The class prozedur_armory provides methods and functions to parse character
'* information from the WoW Armory.
'*
'* Developed by Alcanmage/megasus

Imports System.Net
Imports System.Text

Public Class prozedur_armory
    Public test6 As String
    Dim ServerString As String = Main.ServerString
    Dim characterguid As Integer
    Dim reporttext As RichTextBox = Process_Status.processreport
    Dim charnumber As Integer
    Dim xoverview As Boolean
    Dim runfunction As New Functions
    Public Sub prozedur(ByVal armory_link As String, ByVal cnumber As Integer, ByVal overview As Boolean)
        Process_Status.BringToFront()

        xoverview = overview
        Main.setallempty()
        charnumber = cnumber
        Dim xslot As Integer = 0
        Dim gemslot As Integer = 0
        Dim vzslot As Integer = 0

        Try
            Glyphs.Close()
        Catch ex As Exception

        End Try
        Try
            armory_link = armory_link.Replace("simple", "advanced")
        Catch ex As Exception

        End Try

        If Not armory_link.Contains("simple") Then

            If Not armory_link.Contains("advanced") Then
                armory_link = armory_link & "/advanced"
            Else

            End If
        Else

        End If

        Main.armoryrun = True
        Dim anfang As String = "/character/"
        Dim ende As String = "/"
        Dim realm As String
        realm = Split(armory_link, anfang, 5)(1)
        Main.realmname = Split(realm, ende, 6)(0)

        Dim anfang2 As String = "http://"
        Dim ende2 As String = ".battle"
        Dim loc_region As String
        loc_region = Split(armory_link, anfang2, 5)(1)
        loc_region = Split(loc_region, ende2, 6)(0)
        Main.battlenet_region = loc_region
        Try
            Dim quellclient As New WebClient
            Main.quelltext = quellclient.DownloadString(armory_link)
            Dim s As String = Main.quelltext
            Dim b() As Byte = Encoding.Default.GetBytes(s)
            Dim s1 As String = Encoding.UTF8.GetString(b)
            Main.quelltext = s1
            If Main.quelltext.Contains("#39;") Then Main.quelltext = Main.quelltext.Replace("#39;", "'")
            If Main.quelltext.Contains("Ã¼") Then Main.quelltext = Main.quelltext.Replace("Ã¼", "ü")
            If Main.quelltext.Contains("Ã¤") Then Main.quelltext = Main.quelltext.Replace("Ã¤", "ä")
            If Main.quelltext.Contains("Ã¶") Then Main.quelltext = Main.quelltext.Replace("Ã¶", "ö")
            If Main.quelltext.Contains("ÃŸ") Then Main.quelltext = Main.quelltext.Replace("ÃŸ", "ß")
            Dim yquellclient As New WebClient
            '  Main.talentpage = yquellclient.DownloadString(armory_link.Replace("advanced", "talent/primary"))
            If Main.talentpage.Contains("Ã¼") Then Main.talentpage = Main.talentpage.Replace("Ã¼", "ü")
            If Main.talentpage.Contains("Ã¤") Then Main.talentpage = Main.talentpage.Replace("Ã¤", "ä")
            If Main.talentpage.Contains("Ã¶") Then Main.talentpage = Main.talentpage.Replace("Ã¶", "ö")
            If Main.talentpage.Contains("ÃŸ") Then Main.talentpage = Main.talentpage.Replace("ÃŸ", "ß")
            Dim zquellclient As New WebClient
            '  Main.sectalentpage = zquellclient.DownloadString(armory_link.Replace("advanced", "talent/secondary"))
            If Main.sectalentpage.Contains("Ã¼") Then Main.sectalentpage = Main.sectalentpage.Replace("Ã¼", "ü")
            If Main.sectalentpage.Contains("Ã¤") Then Main.sectalentpage = Main.sectalentpage.Replace("Ã¤", "ä")
            If Main.sectalentpage.Contains("Ã¶") Then Main.sectalentpage = Main.sectalentpage.Replace("Ã¶", "ö")
            If Main.sectalentpage.Contains("ÃŸ") Then Main.sectalentpage = Main.sectalentpage.Replace("ÃŸ", "ß")
        Catch ex As Exception
            MsgBox("Armory faulty! New patch?" & vbCrLf & "Application will close.", MsgBoxStyle.Critical, "Error")
            Application.Exit()
        End Try

        My.Application.DoEvents()
        'status.Text = ""

        Process_Status.processreport.AppendText(
            Now.TimeOfDay.ToString & "/ Loading Characters from Armory..." & vbNewLine)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "/ Loading new Character..." & vbNewLine)


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
            Main.charopt.Add("name" & charnumber.ToString & "=" & quellcodeSplityx88)
            Process_Status.processreport.AppendText(
                Now.TimeOfDay.ToString & "// Got Character Name: " & quellcodeSplityx88 & vbNewLine)
            My.Application.DoEvents()
        Catch ex As Exception
            Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Error: " & ex.ToString & vbNewLine)
            Main.errorcount += 1
            My.Application.DoEvents()
            Main.level.Visible = True
        End Try
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "// Loading Character Level..." & vbNewLine)
        My.Application.DoEvents()
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
            Main.levelid = CInt(Val(quellcodeSplityx88))
            Main.char_level = CInt(Val(quellcodeSplityx88))
            Main.charopt.Add("level" & charnumber.ToString & "=" & quellcodeSplityx88)
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Got Character Level: " & quellcodeSplityx88 & vbNewLine)
            My.Application.DoEvents()
        Catch ex As Exception
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Error (Set Level to 80 instead): " & ex.ToString & vbNewLine)
            Main.errorcount += 1

            Main.level.Text = "Failed to load!"
            Main.char_level = 80
            Main.charopt.Add("level" & charnumber.ToString & "=80")
            My.Application.DoEvents()
            Main.level.Visible = True
        End Try
        Try
            '### NEW ### Get Gender
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String =
                    clienyx88.DownloadString(
                        "http://" & Main.battlenet_region & ".battle.net/api/wow/character/" & Main.realmname & "/" &
                        Main.char_name)
            Dim anfangyx88 As String = """gender"":"
            Dim endeyx88 As String = ","""
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.char_gender = CInt(quellcodeSplityx88)
            Process_Status.processreport.AppendText(
                Now.TimeOfDay.ToString & "// Got Character Gender: " & quellcodeSplityx88 & vbNewLine)
            My.Application.DoEvents()
        Catch ex As Exception
            Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Error: " & ex.ToString & vbNewLine)
            Main.errorcount += 1
            My.Application.DoEvents()
        End Try
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "// Loading Character Race..." & vbNewLine)
        My.Application.DoEvents()
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
            If quellcodeSplityx88 = "worgen" Then quellcodeSplityx88 = "22"
            Main.char_race = CInt(Val(quellcodeSplityx88))
            Main.charopt.Add("race" & charnumber.ToString & "=" & quellcodeSplityx88)
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Got Character Race: " & quellcodeSplityx88 & vbNewLine)
            My.Application.DoEvents()
            Connect.race.Visible = True
        Catch ex As Exception
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Error (Set race to human instead): " & ex.ToString & vbNewLine)
            Main.errorcount += 1
            Main.char_race = 1
            My.Application.DoEvents()
            Connect.race.Visible = False
        End Try
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "// Loading Character Class..." & vbNewLine)
        My.Application.DoEvents()
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
            Main.char_class = CInt(Val(quellcodeSplityx88))
            Main.charopt.Add("class" & charnumber.ToString & "=" & quellcodeSplityx88)
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Got Character Class: " & quellcodeSplityx88 & vbNewLine)
            My.Application.DoEvents()
            Connect.playerclass.Visible = True
        Catch ex As Exception
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Error (Set class to warrior instead): " & ex.ToString & vbNewLine)
            Main.errorcount += 1
            Main.char_class = 1
            My.Application.DoEvents()
            Connect.playerclass.Visible = False
        End Try
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Loading Items..." & vbNewLine)
        My.Application.DoEvents()
        goitem()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Loading Gems..." & vbNewLine)
        My.Application.DoEvents()


        gogems()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Loading Enchantments..." & vbNewLine)
        My.Application.DoEvents()


        govz()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Loading Primary Glyphs..." & vbNewLine)
        My.Application.DoEvents()
        getglyph()
        My.Application.DoEvents()
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Loading Secondary Glyphs..." & vbNewLine)
        getsecglyph()
        getavs()
        getquests()
        getrep()
        getplayerbytes()
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "// Character loaded!" & vbNewLine)
        My.Application.DoEvents()
        saveglyphs()


        If xoverview = True Then
            addtoglyphlist("primeglyph1", Main.primeglyph1)
            addtoglyphlist("primeglyph2", Main.primeglyph2)
            addtoglyphlist("primeglyph3", Main.primeglyph3)
            addtoglyphlist("majorglyph1", Main.majorglyph1)
            addtoglyphlist("majorglyph2", Main.majorglyph2)
            addtoglyphlist("majorglyph3", Main.majorglyph3)
            addtoglyphlist("minorglyph1", Main.minorglyph1)
            addtoglyphlist("minorglyph2", Main.minorglyph2)
            addtoglyphlist("minorglyph3", Main.minorglyph3)

            addtoglyphlist("secprimeglyph1", Main.secprimeglyph1)
            addtoglyphlist("secprimeglyph2", Main.secprimeglyph2)
            addtoglyphlist("secprimeglyph3", Main.secprimeglyph3)
            addtoglyphlist("secmajorglyph1", Main.secmajorglyph1)
            addtoglyphlist("secmajorglyph2", Main.secmajorglyph2)
            addtoglyphlist("secmajorglyph3", Main.secmajorglyph3)
            addtoglyphlist("secminorglyph1", Main.secminorglyph1)
            addtoglyphlist("secminorglyph2", Main.secminorglyph2)
            addtoglyphlist("secminorglyph3", Main.secminorglyph3)
        End If
        addtoitemlist("kopf", Main.kopfid.ToString)
        addtovzlist("kopfvz", Main.kopfvzid.ToString)
        addtogemlist("kopfsocket1", Main.kopfsocket1id.ToString)
        addtogemlist("kopfsocket2", Main.kopfsocket2id.ToString)
        addtogemlist("kopfsocket3", Main.kopfsocket3id.ToString)

        addtoitemlist("hals", Main.halsid.ToString)
        addtovzlist("halsvz", Main.halsvzid.ToString)
        addtogemlist("halssocket1", Main.halssocket1id.ToString)
        addtogemlist("halssocket2", Main.halssocket2id.ToString)
        addtogemlist("halssocket3", Main.halssocket3id.ToString)

        addtoitemlist("schulter", Main.schulterid.ToString)
        addtovzlist("schultervz", Main.schultervzid.ToString)
        addtogemlist("schultersocket1", Main.schultersocket1id.ToString)
        addtogemlist("schultersocket2", Main.schultersocket2id.ToString)
        addtogemlist("schultersocket3", Main.schultersocket3id.ToString)

        addtoitemlist("ruecken", Main.rueckenid.ToString)
        addtovzlist("rueckenvz", Main.rueckenvzid.ToString)
        addtogemlist("rueckensocket1", Main.rueckensocket1id.ToString)
        addtogemlist("rueckensocket2", Main.rueckensocket2id.ToString)
        addtogemlist("rueckensocket3", Main.rueckensocket3id.ToString)

        addtoitemlist("brust", Main.brustid.ToString)
        addtovzlist("brustvz", Main.brustvzid.ToString)
        addtogemlist("brustsocket1", Main.brustsocket1id.ToString)
        addtogemlist("brustsocket2", Main.brustsocket2id.ToString)
        addtogemlist("brustsocket3", Main.brustsocket3id.ToString)

        addtoitemlist("hemd", Main.hemdid.ToString)

        addtoitemlist("wappenrock", Main.wappenrockid.ToString)

        addtoitemlist("haupt", Main.hauptid.ToString)
        addtovzlist("hauptvz", Main.hauptvzid.ToString)
        addtogemlist("hauptsocket1", Main.hauptsocket1id.ToString)
        addtogemlist("hauptsocket2", Main.hauptsocket2id.ToString)
        addtogemlist("hauptsocket3", Main.hauptsocket3id.ToString)

        addtoitemlist("off", Main.offid.ToString)
        addtovzlist("offvz", Main.offvzid.ToString)
        addtogemlist("offsocket1", Main.offsocket1id.ToString)
        addtogemlist("offsocket2", Main.offsocket2id.ToString)
        addtogemlist("offsocket3", Main.offsocket3id.ToString)

        addtoitemlist("distanz", Main.distanzid.ToString)
        addtovzlist("distanzvz", Main.distanzvzid.ToString)
        addtogemlist("distanzsocket1", Main.distanzsocket1id.ToString)
        addtogemlist("distanzsocket2", Main.distanzsocket2id.ToString)
        addtogemlist("distanzsocket3", Main.distanzsocket3id.ToString)

        addtoitemlist("handgelenke", Main.handgelenkeid.ToString)
        addtovzlist("handgelenkevz", Main.handgelenkevzid.ToString)
        addtogemlist("handgelenkesocket1", Main.handgelenkesocket1id.ToString)
        addtogemlist("handgelenkesocket2", Main.handgelenkesocket2id.ToString)
        addtogemlist("handgelenkesocket3", Main.handgelenkesocket3id.ToString)

        addtoitemlist("haende", Main.haendeid.ToString)
        addtovzlist("haendevz", Main.haendevzid.ToString)
        addtogemlist("haendesocket1", Main.haendesocket1id.ToString)
        addtogemlist("haendesocket2", Main.haendesocket2id.ToString)
        addtogemlist("haendesocket3", Main.haendesocket3id.ToString)

        addtoitemlist("guertel", Main.guertelid.ToString)
        addtovzlist("guertelvz", Main.guertelvzid.ToString)
        addtogemlist("guertelsocket1", Main.guertelsocket1id.ToString)
        addtogemlist("guertelsocket2", Main.guertelsocket2id.ToString)
        addtogemlist("guertelsocket3", Main.guertelsocket3id.ToString)

        addtoitemlist("beine", Main.beineid.ToString)
        addtovzlist("beinevz", Main.beinevzid.ToString)
        addtogemlist("beinesocket1", Main.beinesocket1id.ToString)
        addtogemlist("beinesocket2", Main.beinesocket2id.ToString)
        addtogemlist("beinesocket3", Main.beinesocket3id.ToString)

        addtoitemlist("stiefel", Main.stiefelid.ToString)
        addtovzlist("stiefelvz", Main.stiefelvzid.ToString)
        addtogemlist("stiefelsocket1", Main.stiefelsocket1id.ToString)
        addtogemlist("stiefelsocket2", Main.stiefelsocket2id.ToString)
        addtogemlist("stiefelsocket3", Main.stiefelsocket3id.ToString)

        addtoitemlist("ring1", Main.ring1id.ToString)
        addtovzlist("ring1vz", Main.ring1vzid.ToString)
        addtogemlist("ring1socket1", Main.ring1socket1id.ToString)
        addtogemlist("ring1socket2", Main.ring1socket2id.ToString)
        addtogemlist("ring1socket3", Main.ring1socket3id.ToString)

        addtoitemlist("ring2", Main.ring2id.ToString)
        addtovzlist("ring2vz", Main.ring2vzid.ToString)
        addtogemlist("ring2socket1", Main.ring2socket1id.ToString)
        addtogemlist("ring2socket2", Main.ring2socket2id.ToString)
        addtogemlist("ring2socket3", Main.ring2socket3id.ToString)

        addtoitemlist("schmuck1", Main.schmuck1id.ToString)
        addtovzlist("schmuck1vz", Main.schmuck1vzid.ToString)
        addtogemlist("schmuck1socket1", Main.schmuck1socket1id.ToString)
        addtogemlist("schmuck1socket2", Main.schmuck1socket2id.ToString)
        addtogemlist("schmuck1socket3", Main.schmuck1socket3id.ToString)

        addtoitemlist("schmuck2", Main.schmuck2id.ToString)
        addtovzlist("schmuck2vz", Main.schmuck2vzid.ToString)
        addtogemlist("schmuck2socket1", Main.schmuck2socket1id.ToString)
        addtogemlist("schmuck2socket2", Main.schmuck2socket2id.ToString)
        addtogemlist("schmuck2socket3", Main.schmuck2socket3id.ToString)
        Main.datasets += 1
        Dim addtataset As New CIUFile
        addtataset.adddataset()
        Main.Panel21.Location = New Point(5000, 5000)
        Main.UseWaitCursor = False
        'Starter.Close()
        Application.DoEvents()
    End Sub

    Private Sub addtoglyphlist(ByVal key As String, ByVal value As String)
        Main.glyphlist.Add(charnumber & key & "=" & value)
    End Sub

    Private Sub addtovzlist(ByVal key As String, ByVal value As String)
        Main.vzlist.Add(charnumber & key & "=" & value)
    End Sub

    Private Sub addtogemlist(ByVal key As String, ByVal value As String)
        Main.gemlist.Add(charnumber & key & "=" & value)
    End Sub

    Private Sub addtoitemlist(ByVal key As String, ByVal value As String)
        Main.itemlist.Add(charnumber & key & "=" & value)
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
            '  Main.talentlist = Main.magetalentprogress.progress(Main.talentstring)
        End If


        ' Dim ressource2 As String = quellcodeSplit88.Remove(0, ressource1 + 14)
    End Sub

    Private Sub getimage(ByVal itemid As String, ByVal picbox As PictureBox)
        Try
            runfunction.getimage(CInt(itemid), picbox)
        Catch : End Try
      End Sub

    Private Function getspellidfromitem(ByVal itemid As String) As Integer
        Return runfunction.getspellidfromitem(itemid)
     End Function
    Private Function getspellnamefromid(ByVal spellid As String) As String
        Return runfunction.getspellnamefromid(spellid)
    End Function

    Public Function getvzeffectname(ByVal vzid As String) As String
        Try
            Return runfunction.getvzeffectname(CInt(vzid))
        Catch
            Return "-"
        End Try
       End Function

    Public Function getsocketeffectname(ByVal socketid As String) As String
        Try
            Return runfunction.getsocketeffectnameofitemid(CInt(socketid))
        Catch
            Return "-"
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
                Main.Kopf.Text = "-"
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
                Main.Hals.Text = "-"
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
                Main.Schulter.Text = "-"
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
                Main.Ruecken.Text = "-"
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
                Main.Brust.Text = "-"
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
                Main.Hemd.Text = "-"
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
                Main.Wappenrock.Text = "-"
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
                Main.Handgelenke.Text = "-"
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
                Main.Haupt.Text = "-"
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
                Main.Off.Text = "-"
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
                Main.Distanz.Text = "-"
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
                Main.Haende.Text = "-"
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
                Main.Guertel.Text = "-"
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
                Main.Beine.Text = "-"
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
                Main.Stiefel.Text = "-"
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
                Main.Ring1.Text = "-"
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
                Main.Ring2.Text = "-"
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
                Main.Schmuck1.Text = "-"
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
                Main.Schmuck2.Text = "-"
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

    Private Sub getplayerbytes()
        Dim tmpface As String = ""
        Dim tmpskin As String = ""
        Dim tmphairStyle As String = ""
        Dim tmphairColor As String = ""
        Dim tmpfeature As String = ""
        Dim appearance As String = ""
        Try
            Dim client As New WebClient
            Dim quellcode As String =
                    client.DownloadString(
                        "http://" & Main.battlenet_region & ".battle.net/api/wow/character/" & Main.realmname & "/" &
                        Main.char_name & "?fields=appearance")
            Dim anfangyx88 As String = """faceVariation"":"
            Dim endeyx88 As String = ","
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcode, anfangyx88, 5)(1)
            tmpface = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Dim anfangyx888 As String = """skinColor"":"
            Dim endeyx888 As String = ","
            Dim quellcodeSplityx888 As String
            quellcodeSplityx888 = Split(quellcode, anfangyx888, 5)(1)
            tmpskin = Split(quellcodeSplityx888, endeyx888, 6)(0)
            Dim anfangyx8888 As String = """hairVariation"":"
            Dim endeyx8888 As String = ","
            Dim quellcodeSplityx8888 As String
            quellcodeSplityx8888 = Split(quellcode, anfangyx8888, 5)(1)
            tmphairStyle = Split(quellcodeSplityx8888, endeyx8888, 6)(0)
            Dim anfangyx88888 As String = """hairColor"":"
            Dim endeyx88888 As String = ","
            Dim quellcodeSplityx88888 As String
            quellcodeSplityx88888 = Split(quellcode, anfangyx88888, 5)(1)
            tmphairColor = Split(quellcodeSplityx88888, endeyx88888, 6)(0)
            Dim anfangyx888888 As String = """featureVariation"":"
            Dim endeyx888888 As String = ","
            Dim quellcodeSplityx888888 As String
            quellcodeSplityx888888 = Split(quellcode, anfangyx888888, 5)(1)
            tmpfeature = Split(quellcodeSplityx888888, endeyx888888, 6)(0)
        Catch
        End Try
        Try

            Dim face As String = Hex$(Long.Parse(tmpface))
            Dim skin As String = Hex$(Long.Parse(tmpface))
            Dim hairStyle As String = Hex$(Long.Parse(tmpface))
            Dim hairColor As String = Hex$(Long.Parse(tmpface))
            If face.ToString.Length = 1 Then face = 0 & face
            If skin.ToString.Length = 1 Then skin = 0 & skin
            If hairStyle.ToString.Length = 1 Then hairStyle = 0 & hairStyle
            If hairColor.ToString.Length = 1 Then hairColor = 0 & hairColor
            Dim bytestring As String = ((hairColor) & (hairStyle) & (face) & (skin)).ToString
            Main.playerBytes = CInt(CLng("&H" & bytestring).ToString)
        Catch
        End Try
        Try
            Dim feature As String = Hex$(Long.Parse(tmpfeature))
            If feature.Length = 1 Then feature = "0" & feature

        Catch ex As Exception

        End Try
        ' value?
    End Sub

    ' Main.character_reputatuion_list.Add("<faction>" & faction & "</faction><standing>" & standing & "</standing><flags>" & flags & "</flags>")
    Private Sub getquests()
        Dim queststring As String = ""
        Try
            Dim client As New WebClient
            Dim quellcode As String =
                    client.DownloadString(
                        "http://" & Main.battlenet_region & ".battle.net/api/wow/character/" & Main.realmname & "/" &
                        Main.char_name & "?fields=quests")
            Dim anfangyx88 As String = """quests"":["
            Dim endeyx88 As String = "]}"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcode, anfangyx88, 5)(1)
            queststring = Split(quellcodeSplityx88, endeyx88, 6)(0) & ","
            Main.finished_quests = queststring
        Catch ex As Exception

        End Try
    End Sub

    Private Sub getrep()
        Dim factionid As String = ""
        Dim repstring As String = ""
        Dim standing As Integer = vbEmpty
        Dim quellcode As String = ""
        Dim orgstanding As Integer = vbEmpty
        Try
            Dim client As New WebClient
            quellcode =
                client.DownloadString(
                    "http://" & Main.battlenet_region & ".battle.net/api/wow/character/" & Main.realmname & "/" &
                    Main.char_name & "?fields=reputation")
            Dim anfangyx88 As String = """reputation"":["
            Dim endeyx88 As String = "]"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcode, anfangyx88, 5)(1)
            repstring = Split(quellcodeSplityx88, endeyx88, 6)(0) & ",{"
        Catch ex As Exception

        End Try
        If Not repstring = "" Then
            Dim excounter As Integer = UBound(Split(repstring, ",{"))
            Try
                repstring = repstring.Replace(",{", "§")
            Catch
            End Try

            Dim parts() As String = repstring.Split("§"c)
            Dim loopcounter As Integer = 0
            Do
                Dim exsplit As String = parts(loopcounter)
                Dim anfangyx88 As String = """id"":"
                Dim endeyx88 As String = ","
                Dim quellcodeSplityx88 As String
                quellcodeSplityx88 = Split(exsplit, anfangyx88, 5)(1)
                factionid = Split(quellcodeSplityx88, endeyx88, 6)(0)
                Dim anfangyx888 As String = """value"":"
                Dim endeyx888 As String = ","
                Dim quellcodeSplityx888 As String
                quellcodeSplityx888 = Split(parts(loopcounter), anfangyx888, 5)(1)
                standing = CInt(Split(quellcodeSplityx888, endeyx888, 6)(0))
                Dim anfangyx8888 As String = """standing"":"
                Dim endeyx8888 As String = ","
                Dim quellcodeSplityx8888 As String
                quellcodeSplityx8888 = Split(parts(loopcounter), anfangyx8888, 5)(1)
                orgstanding = CInt(Split(quellcodeSplityx8888, endeyx8888, 6)(0))
                loopcounter += 1

                If orgstanding > 3 Then standing += 3000
                If orgstanding > 4 Then standing += 6000
                If orgstanding > 5 Then standing += 12000
                If orgstanding > 6 Then standing += 21000
                Main.character_reputatuion_list.Add(
                    "<faction>" & factionid & "</faction><standing>" & standing.ToString & "</standing><flags>1</flags>")
            Loop Until loopcounter = excounter

        End If
    End Sub

    '     Main.character_achievement_list.Add("<av>" & avid & "</av><date>" & xdate & "</date>")
    Private Sub getavs()
        Dim avid As String = ""
        Dim avstring As String = ""
        Dim timestamp As String = ""
        Dim TimeString As String = ""
        Dim quellcodeyx88 As String = ""
        Try
            Dim clienyx88 As New WebClient
            quellcodeyx88 =
                clienyx88.DownloadString(
                    "http://" & Main.battlenet_region & ".battle.net/api/wow/character/" & Main.realmname & "/" &
                    Main.char_name & "?fields=achievements")
            Dim anfangyx88 As String = "{""achievementsCompleted"":["
            Dim endeyx88 As String = "],"""
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            avstring = Split(quellcodeSplityx88, endeyx88, 6)(0) & ","

            My.Application.DoEvents()
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            If quellcodeyx88 = "" Then Exit Sub
            Dim anfangyx88 As String = """achievementsCompletedTimestamp"":["
            Dim endeyx88 As String = "],"""
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            TimeString = Split(quellcodeSplityx88, endeyx88, 6)(0) & ","
            My.Application.DoEvents()
        Catch ex As Exception

        End Try
        Try
            If Not avstring = "" Then
                Dim loopcounter As Integer = 0
                Dim excounter As Integer = UBound(Split(avstring, ","))
                Dim parts() As String = avstring.Split(","c)
                Dim parts2() As String = TimeString.Split(","c)
                Do
                    avid = parts(loopcounter)
                    timestamp = parts2(loopcounter)
                    If timestamp.Contains("000") Then
                        Try
                            timestamp = timestamp.Remove(timestamp.Length - 3, 3)
                        Catch : End Try
                    End If
                    loopcounter += 1
                    Main.character_achievement_list.Add("<av>" & avid & "</av><date>" & timestamp & "</date>")
                Loop Until loopcounter = excounter
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub getglyph()
        Dim glyphid As String = ""
        Dim glyphstring As String = ""
        Dim glyphname As String = ""
        Dim xname As String = Main.realmname
        Dim zname As String = Main.char_name
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String =
                    clienyx88.DownloadString(
                        "http://" & Main.battlenet_region & ".battle.net/api/wow/character/" & Main.realmname & "/" &
                        Main.char_name & "?fields=talents")
            If Not quellcodeyx88.Contains("""glyphs"":") Then Exit Sub
            Dim anfangyx88 As String = """glyphs"":"
            Dim endeyx88 As String = ",""spec"":"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            glyphstring = Split(quellcodeSplityx88, endeyx88, 6)(0)
            My.Application.DoEvents()
        Catch ex As Exception

        End Try
        Try
            If glyphstring.Contains("""major""") Then
                Dim anfang As String = """major"":"
                Dim ende As String = """}]"
                Dim majorglyphs As String
                majorglyphs = Split(glyphstring, anfang, 5)(1)
                majorglyphs = Split(majorglyphs, ende, 6)(0)
                Dim excounter As Integer = UBound(Split(majorglyphs, "{""glyph"""))
                Dim startcounter As Integer = 0
                Do
                    majorglyphs = majorglyphs.Replace("},", "*")
                    Dim parts() As String = majorglyphs.Split("*"c)
                    Dim oneglyph As String = parts(startcounter)
                    Dim anfang2 As String = """item"":"
                    Dim ende2 As String = ","""
                    glyphid = Split(oneglyph, anfang2, 5)(1)
                    glyphid = Split(glyphid, ende2, 6)(0)


                    Dim anfang3 As String = """name"":"
                    Dim ende3 As String = ","""
                    glyphname = Split(oneglyph, anfang3, 5)(1)
                    glyphname = Split(glyphname, ende3, 6)(0)

                    If startcounter = 0 Then
                        If xoverview = True Then
                            Glyphs.erheb1.Text = glyphname
                            Main.majorglyph1 = glyphid
                            Glyphs.erheb1.Visible = True
                            getimage(glyphid, Glyphs.erheb1pic)
                        Else
                            Main.majorglyph1 = glyphid
                            Main.glyphlist.Add(charnumber & "majorglyph1=" & glyphid)
                        End If

                    ElseIf startcounter = 1 Then
                        If xoverview = True Then
                            Glyphs.erheb2.Text = glyphname
                            Main.majorglyph2 = glyphid
                            Glyphs.erheb2.Visible = True
                            getimage(glyphid, Glyphs.erheb2pic)
                        Else
                            Main.majorglyph2 = glyphid
                            Main.glyphlist.Add(charnumber & "majorglyph2=" & glyphid)
                        End If

                    ElseIf startcounter = 2 Then
                        If xoverview = True Then
                            Glyphs.erheb3.Text = glyphname
                            Main.majorglyph3 = glyphid
                            Glyphs.erheb3.Visible = True
                            getimage(glyphid, Glyphs.erheb3pic)
                        Else
                            Main.majorglyph3 = glyphid
                            Main.glyphlist.Add(charnumber & "majorglyph3=" & glyphid)
                        End If

                    Else

                    End If
                    startcounter += 1


                Loop Until startcounter = excounter

            End If
            If glyphstring.Contains("""minor""") Then
                Dim anfang As String = """minor"":"
                Dim ende As String = """}]"
                Dim majorglyphs As String
                majorglyphs = Split(glyphstring, anfang, 5)(1)
                majorglyphs = Split(majorglyphs, ende, 6)(0)
                Dim excounter As Integer = UBound(Split(majorglyphs, "{""glyph"""))
                Dim startcounter As Integer = 0
                Do
                    majorglyphs = majorglyphs.Replace("},", "*")
                    Dim parts() As String = majorglyphs.Split("*"c)
                    Dim oneglyph As String = parts(startcounter)
                    Dim anfang2 As String = """item"":"
                    Dim ende2 As String = ","""
                    glyphid = Split(oneglyph, anfang2, 5)(1)
                    glyphid = Split(glyphid, ende2, 6)(0)


                    Dim anfang3 As String = """name"":"
                    Dim ende3 As String = ","""
                    glyphname = Split(oneglyph, anfang3, 5)(1)
                    glyphname = Split(glyphname, ende3, 6)(0)

                    If startcounter = 0 Then
                        If xoverview = True Then
                            Glyphs.gering1.Text = glyphname
                            Main.minorglyph1 = glyphid
                            Glyphs.gering1.Visible = True
                            getimage(glyphid, Glyphs.gering1pic)
                        Else
                            Main.minorglyph1 = glyphid
                            Main.glyphlist.Add(charnumber & "minorglyph1=" & glyphid)
                        End If

                    ElseIf startcounter = 1 Then
                        If xoverview = True Then
                            Glyphs.gering2.Text = glyphname
                            Main.minorglyph2 = glyphid
                            Glyphs.gering2.Visible = True
                            getimage(glyphid, Glyphs.gering2pic)
                        Else
                            Main.minorglyph2 = glyphid
                            Main.glyphlist.Add(charnumber & "minorglyph2=" & glyphid)
                        End If

                    ElseIf startcounter = 2 Then
                        If xoverview = True Then
                            Glyphs.gering3.Text = glyphname
                            Main.minorglyph3 = glyphid
                            Glyphs.gering3.Visible = True
                            getimage(glyphid, Glyphs.gering3pic)
                        Else
                            Main.minorglyph3 = glyphid
                            Main.glyphlist.Add(charnumber & "minorglyph3=" & glyphid)
                        End If
                    End If
                    startcounter += 1


                Loop Until startcounter = excounter

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub getsecglyph()
        Dim glyphid As String = ""
        Dim glyphstring As String = ""
        Dim glyphname As String = ""
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String =
                    clienyx88.DownloadString(
                        "http://" & Main.battlenet_region & ".battle.net/api/wow/character/" & Main.realmname & "/" &
                        Main.char_name & "?fields=talents")
            Dim anfangyx88 As String = ",""spec"":"
            Dim endeyx88 As String = "}]}"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            glyphstring = Split(quellcodeSplityx88, endeyx88, 6)(0)
            If Not quellcodeyx88.Contains("""glyphs"":") Then Exit Sub
            My.Application.DoEvents()
        Catch ex As Exception

        End Try
        Try
            If glyphstring.Contains("""major""") Then
                Dim anfang As String = """major"":"
                Dim ende As String = """}]"
                Dim majorglyphs As String
                majorglyphs = Split(glyphstring, anfang, 5)(1)
                majorglyphs = Split(majorglyphs, ende, 6)(0)
                Dim excounter As Integer = UBound(Split(majorglyphs, "{""glyph"""))
                Dim startcounter As Integer = 0
                Do
                    majorglyphs = majorglyphs.Replace("},", "*")
                    Dim parts() As String = majorglyphs.Split("*"c)
                    Dim oneglyph As String = parts(startcounter)
                    Dim anfang2 As String = """item"":"
                    Dim ende2 As String = ","""
                    glyphid = Split(oneglyph, anfang2, 5)(1)
                    glyphid = Split(glyphid, ende2, 6)(0)


                    Dim anfang3 As String = """name"":"
                    Dim ende3 As String = ","""
                    glyphname = Split(oneglyph, anfang3, 5)(1)
                    glyphname = Split(glyphname, ende3, 6)(0)

                    If startcounter = 0 Then
                        If xoverview = True Then
                            Glyphs.secerheb1.Text = glyphname
                            Main.secmajorglyph1 = glyphid
                            Glyphs.secerheb1.Visible = True
                            getimage(glyphid, Glyphs.secerheb1pic)
                        Else
                            Main.secmajorglyph1 = glyphid
                            Main.glyphlist.Add(charnumber & "secmajorglyph1=" & glyphid)
                        End If

                    ElseIf startcounter = 1 Then
                        If xoverview = True Then
                            Glyphs.secerheb2.Text = glyphname
                            Main.secmajorglyph2 = glyphid
                            Glyphs.secerheb2.Visible = True
                            getimage(glyphid, Glyphs.secerheb2pic)
                        Else
                            Main.secmajorglyph2 = glyphid
                            Main.glyphlist.Add(charnumber & "secmajorglyph2=" & glyphid)
                        End If

                    ElseIf startcounter = 2 Then
                        If xoverview = True Then
                            Glyphs.secerheb3.Text = glyphname
                            Main.secmajorglyph3 = glyphid
                            Glyphs.secerheb3.Visible = True
                            getimage(glyphid, Glyphs.secerheb3pic)
                        Else
                            Main.secmajorglyph3 = glyphid
                            Main.glyphlist.Add(charnumber & "secmajorglyph3=" & glyphid)
                        End If

                    Else

                    End If
                    startcounter += 1


                Loop Until startcounter = excounter

            End If
            If glyphstring.Contains("""minor""") Then
                Dim anfang As String = """minor"":"
                Dim ende As String = """}]"
                Dim majorglyphs As String
                majorglyphs = Split(glyphstring, anfang, 5)(1)
                majorglyphs = Split(majorglyphs, ende, 6)(0)
                Dim excounter As Integer = UBound(Split(majorglyphs, "{""glyph"""))
                Dim startcounter As Integer = 0
                Do
                    majorglyphs = majorglyphs.Replace("},", "*")
                    Dim parts() As String = majorglyphs.Split("*"c)
                    Dim oneglyph As String = parts(startcounter)
                    Dim anfang2 As String = """item"":"
                    Dim ende2 As String = ","""
                    glyphid = Split(oneglyph, anfang2, 5)(1)
                    glyphid = Split(glyphid, ende2, 6)(0)


                    Dim anfang3 As String = """name"":"
                    Dim ende3 As String = ","""
                    glyphname = Split(oneglyph, anfang3, 5)(1)
                    glyphname = Split(glyphname, ende3, 6)(0)

                    If startcounter = 0 Then
                        If xoverview = True Then
                            Glyphs.secgering1.Text = glyphname
                            Main.secminorglyph1 = glyphid
                            Glyphs.secgering1.Visible = True
                            getimage(glyphid, Glyphs.secgering1pic)
                        Else
                            Main.secminorglyph1 = glyphid
                            Main.glyphlist.Add(charnumber & "secminorglyph1=" & glyphid)
                        End If

                    ElseIf startcounter = 1 Then
                        If xoverview = True Then
                            Glyphs.secgering2.Text = glyphname
                            Main.secminorglyph2 = glyphid
                            Glyphs.secgering2.Visible = True
                            getimage(glyphid, Glyphs.secgering2pic)
                        Else
                            Main.secminorglyph2 = glyphid
                            Main.glyphlist.Add(charnumber & "secminorglyph2=" & glyphid)
                        End If

                    ElseIf startcounter = 2 Then
                        If xoverview = True Then
                            Glyphs.secgering3.Text = glyphname
                            Main.secminorglyph3 = glyphid
                            Glyphs.secgering3.Visible = True
                            getimage(glyphid, Glyphs.secgering3pic)
                        Else
                            Main.secminorglyph3 = glyphid
                            Main.glyphlist.Add(charnumber & "secminorglyph3=" & glyphid)
                        End If
                    End If
                    startcounter += 1


                Loop Until startcounter = excounter

            End If

        Catch ex As Exception

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

                If quellcodeSplityx88.Contains("/item/") Then


                    Dim xXquellcodeyx88 As String = quellcodeSplityx88
                    Dim xXanfangyx88 As String = "/item/"
                    Dim xXendeyx88 As String = """>"
                    Dim xXquellcodeSplityx88 As String
                    xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                    xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                    If xXquellcodeSplityx88.Contains("Ã¼") Then xXquellcodeSplityx88.Replace("Ã¼", "ü")
                    If xXquellcodeSplityx88.Contains("Ã¤") Then xXquellcodeSplityx88.Replace("Ã¤", "ä")
                    If xXquellcodeSplityx88.Contains("Ã¶") Then xXquellcodeSplityx88.Replace("Ã¶", "")
                    If xXquellcodeSplityx88.Contains("ÃŸ") Then xXquellcodeSplityx88.Replace("ÃŸ", "")
                    Dim vzid As String = xXquellcodeSplityx88
                    Process_Status.processreport.appendText(
                        Now.TimeOfDay.ToString & "// Got Enchantment for Slot: " & slot.ToString & vbNewLine)
                    My.Application.DoEvents()
                    Select Case slot
                        Case 1
                            With Main.kopfvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.kopfvzid = runfunction.getvzeffectid(.Text)
                            End With


                        Case 2
                            With Main.halsvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.halsvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 3
                            With Main.schultervz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.schultervzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 4
                            With Main.rueckenvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.rueckenvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 5
                            With Main.brustvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.brustvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 6

                        Case 7

                        Case 8
                            With Main.handgelenkevz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.handgelenkevzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 9
                            With Main.hauptvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.hauptvzid = runfunction.getvzeffectid(.Text)
                                End With
                            With Main.hauptvzlabel2
                                .Visible = True
                                If xoverview = True Then .Text = Main.hauptvz.Text
                            End With

                        Case 10
                            With Main.offvz
                                .Visible = False
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.offvzid = runfunction.getvzeffectid(.Text)
                            End With
                            With Main.offvzlabel2
                                .Visible = True
                                If xoverview = True Then .Text = Main.offvz.Text
                            End With

                        Case 11
                            With Main.distanzvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.distanzvzid = runfunction.getvzeffectid(.Text)
                            End With
                            With Main.distanzvzlabel2
                                .Visible = True
                                .Text = Main.distanzvz.Text
                            End With

                        Case 12
                            With Main.haendevz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.haendevzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 13
                            With Main.guertelvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.guertelvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 14
                            With Main.beinevz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.beinevzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 15
                            With Main.stiefelvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.stiefelvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 16
                            With Main.ring1vz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.ring1vzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 17
                            With Main.ring2vz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.ring2vzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 18
                            With Main.schmuck1vz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.schmuck1vzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 19
                            With Main.schmuck2vz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.schmuck2vzid = runfunction.getvzeffectid(.Text)
                            End With

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
                    Dim vzid As String = xXquellcodeSplityx88.ToString
                    Process_Status.processreport.appendText(
                        Now.TimeOfDay.ToString & "// Got Enchantment for slot: " & slot.ToString & vbNewLine)
                    My.Application.DoEvents()
                    Select Case slot
                        Case 1
                            With Main.kopfvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.kopfvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 2
                            With Main.halsvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.halsvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 3
                            With Main.schultervz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.schultervzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 4
                            With Main.rueckenvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.rueckenvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 5
                            With Main.brustvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.brustvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 6

                        Case 7

                        Case 8
                            With Main.handgelenkevz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.handgelenkevzid = runfunction.getvzeffectid(.Text)
                            End With

                            '[ Section changed 03/09/12 -  Reason: Label not displayed!
                        Case 9
                            With Main.hauptvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.hauptvzid = runfunction.getvzeffectid(.Text)
                            End With
                            With Main.hauptvzlabel2
                                .Visible = True
                                If xoverview = True Then .Text = Main.hauptvz.Text
                            End With

                        Case 10
                            With Main.offvz
                                .Visible = False
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.offvzid = runfunction.getvzeffectid(.Text)
                            End With
                            With Main.offvzlabel2
                                .Visible = True
                                If xoverview = True Then .Text = Main.offvz.Text
                            End With

                        Case 11
                            With Main.distanzvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.distanzvzid = runfunction.getvzeffectid(.Text)
                            End With
                            With Main.distanzvzlabel2
                                .Visible = True
                                .Text = Main.distanzvz.Text
                            End With

                            'End Section ]
                        Case 12
                            With Main.haendevz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.haendevzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 13
                            With Main.guertelvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.guertelvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 14
                            With Main.beinevz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.beinevzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 15
                            With Main.stiefelvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.stiefelvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 16
                            With Main.ring1vz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.ring1vzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 17
                            With Main.ring2vz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.ring2vzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 18
                            With Main.schmuck1vz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.schmuck1vzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 19
                            With Main.schmuck2vz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.schmuck2vzid = runfunction.getvzeffectid(.Text)
                            End With

                    End Select
                Else

                End If
            Else

            End If
        Catch

        End Try
    End Sub

    Private Sub getgem(ByVal slot As Integer)

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
                Dim anfangyx88 As String = "/item/"
                Dim endeyx88 As String = """ class=""gem"">"
                Dim quellcodeSplityx88 As String
                quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
                quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
                If quellcodeSplityx88.Contains("Ã¼") Then quellcodeSplityx88.Replace("Ã¼", "ü")
                If quellcodeSplityx88.Contains("Ã¤") Then quellcodeSplityx88.Replace("Ã¤", "ä")
                If quellcodeSplityx88.Contains("Ã¶") Then quellcodeSplityx88.Replace("Ã¶", "")
                If quellcodeSplityx88.Contains("ÃŸ") Then quellcodeSplityx88.Replace("ÃŸ", "")
                socket1 = quellcodeSplityx88
                Dim sockettext As String = "-"
                Process_Status.processreport.appendText(
                    Now.TimeOfDay.ToString & "// Loaded Socket1 for slot : " & slot.ToString & vbNewLine)
                My.Application.DoEvents()
                Select Case slot
                    Case 1
                        With Main.kopfsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.kopfsocket1id = runfunction.getgemeffectid(socket1)
                    Case 2
                        With Main.halssocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.halssocket1id = runfunction.getgemeffectid(socket1)
                    Case 3
                        With Main.schultersocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.schultersocket1id = runfunction.getgemeffectid(socket1)
                    Case 4
                        With Main.rueckensocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.rueckensocket1id = runfunction.getgemeffectid(socket1)
                    Case 5
                        With Main.brustsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.brustsocket1id = runfunction.getgemeffectid(socket1)
                    Case 6

                    Case 7

                    Case 8
                        With Main.Handgelenkesocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.handgelenkesocket1id = runfunction.getgemeffectid(socket1)
                    Case 9
                        With Main.Hauptsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.hauptsocket1id = runfunction.getgemeffectid(socket1)
                    Case 10
                        With Main.Offsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.offsocket1id = runfunction.getgemeffectid(socket1)
                    Case 11
                        With Main.Distanzsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.distanzsocket1id = runfunction.getgemeffectid(socket1)
                    Case 12
                        With Main.haendesocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.haendesocket1id = runfunction.getgemeffectid(socket1)
                    Case 13
                        With Main.guertelsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.guertelsocket1id = runfunction.getgemeffectid(socket1)
                    Case 14
                        With Main.beinesocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.beinesocket1id = runfunction.getgemeffectid(socket1)
                    Case 15
                        With Main.stiefelsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.stiefelsocket1id = runfunction.getgemeffectid(socket1)
                    Case 16
                        With Main.Ring1socket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.ring1socket1id = runfunction.getgemeffectid(socket1)
                    Case 17
                        With Main.ring2socket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.ring2socket1id = runfunction.getgemeffectid(socket1)
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
                    Dim Xanfangyx88 As String = "/item/"
                    Dim Xendeyx88 As String = """ class=""gem"">"
                    Dim XquellcodeSplityx88 As String
                    XquellcodeSplityx88 = Split(Xquellcodeyx88, Xanfangyx88, 5)(1)
                    XquellcodeSplityx88 = Split(XquellcodeSplityx88, Xendeyx88, 6)(0)

                    If XquellcodeSplityx88.Contains("Ã¼") Then XquellcodeSplityx88.Replace("Ã¼", "ü")
                    If XquellcodeSplityx88.Contains("Ã¤") Then XquellcodeSplityx88.Replace("Ã¤", "ä")
                    If XquellcodeSplityx88.Contains("Ã¶") Then XquellcodeSplityx88.Replace("Ã¶", "")
                    If XquellcodeSplityx88.Contains("ÃŸ") Then XquellcodeSplityx88.Replace("ÃŸ", "")
                    socket2 = XquellcodeSplityx88
                    Process_Status.processreport.appendText(
                        Now.TimeOfDay.ToString & "// Loaded Socket2 for slot : " & slot.ToString & vbNewLine)
                    My.Application.DoEvents()
                    Select Case slot
                        Case 1
                            With Main.kopfsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.kopfsocket2id = runfunction.getgemeffectid(socket2)
                        Case 2
                            With Main.halssocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.halssocket2id = runfunction.getgemeffectid(socket2)
                        Case 3
                            With Main.schultersocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.schultersocket2id = runfunction.getgemeffectid(socket2)
                        Case 4
                            With Main.rueckensocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.rueckensocket2id = runfunction.getgemeffectid(socket2)
                        Case 5
                            With Main.brustsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.brustsocket2id = runfunction.getgemeffectid(socket2)
                        Case 6

                        Case 7

                        Case 8
                            With Main.handgelenkesocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.handgelenkesocket2id = runfunction.getgemeffectid(socket2)
                        Case 9
                            With Main.Hauptsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.hauptsocket2id = runfunction.getgemeffectid(socket2)
                        Case 10
                            With Main.Offsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.offsocket2id = runfunction.getgemeffectid(socket2)
                        Case 11
                            With Main.Distanzsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.distanzsocket2id = runfunction.getgemeffectid(socket2)
                        Case 12
                            With Main.haendesocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.haendesocket2id = runfunction.getgemeffectid(socket2)
                        Case 13
                            With Main.guertelsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.guertelsocket2id = runfunction.getgemeffectid(socket2)
                        Case 14
                            With Main.beinesocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.beinesocket2id = runfunction.getgemeffectid(socket2)
                        Case 15
                            With Main.stiefelsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.stiefelsocket2id = runfunction.getgemeffectid(socket2)
                        Case 16
                            With Main.ring1socket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.ring1socket2id = runfunction.getgemeffectid(socket2)
                        Case 17
                            With Main.ring2socket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.ring2socket2id = runfunction.getgemeffectid(socket2)
                        Case 18

                        Case 19

                    End Select
                    Dim ressource1 As Integer = quellcodeSplit88.IndexOf(""" class=""gem"">")
                    Dim ressource2 As String = quellcodeSplit88.Remove(0, ressource1 + 14)
                    Dim ressource3 As Integer = ressource2.IndexOf(""" class=""gem"">")
                    Dim ressource4 As String = ressource2.Remove(0, ressource3 + 14)

                    If ressource4.Contains("<span class=""icon-socket socket-") Then
                        Dim xXquellcodeyx88 As String = ressource4
                        Dim xXanfangyx88 As String = "/item/"
                        Dim xXendeyx88 As String = """ class=""gem"">"
                        Dim xXquellcodeSplityx88 As String
                        xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                        xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                        If xXquellcodeSplityx88.Contains("Ã¼") Then xXquellcodeSplityx88.Replace("Ã¼", "ü")
                        If xXquellcodeSplityx88.Contains("Ã¤") Then xXquellcodeSplityx88.Replace("Ã¤", "ä")
                        If xXquellcodeSplityx88.Contains("Ã¶") Then xXquellcodeSplityx88.Replace("Ã¶", "")
                        If xXquellcodeSplityx88.Contains("ÃŸ") Then xXquellcodeSplityx88.Replace("ÃŸ", "")
                        socket3 = xXquellcodeSplityx88
                        Process_Status.processreport.appendText(
                            Now.TimeOfDay.ToString & "// Loaded Socket3 for slot : " & slot.ToString & vbNewLine)
                        My.Application.DoEvents()
                        Select Case slot
                            Case 1
                                With Main.kopfsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.kopfsocket3id = runfunction.getgemeffectid(socket3)
                            Case 2
                                With Main.halssocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.halssocket3id = runfunction.getgemeffectid(socket3)
                            Case 3
                                With Main.schultersocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.schultersocket3id = runfunction.getgemeffectid(socket3)
                            Case 4
                                With Main.rueckensocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.rueckensocket3id = runfunction.getgemeffectid(socket3)
                            Case 5
                                With Main.brustsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.brustsocket3id = runfunction.getgemeffectid(socket3)
                            Case 6

                            Case 7

                            Case 8
                                With Main.Handgelenkesocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.handgelenkesocket3id = runfunction.getgemeffectid(socket3)
                            Case 9
                                With Main.hauptsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.hauptsocket3id = runfunction.getgemeffectid(socket3)
                            Case 10
                                With Main.offsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.offsocket3id = runfunction.getgemeffectid(socket3)
                            Case 11
                                With Main.distanzsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.distanzsocket3id = runfunction.getgemeffectid(socket3)
                            Case 12
                                With Main.haendesocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.haendesocket3id = runfunction.getgemeffectid(socket3)
                            Case 13
                                With Main.guertelsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.guertelsocket3id = runfunction.getgemeffectid(socket3)
                            Case 14
                                With Main.beinesocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.beinesocket3id = runfunction.getgemeffectid(socket3)
                            Case 15
                                With Main.stiefelsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.stiefelsocket3id = runfunction.getgemeffectid(socket3)
                            Case 16
                                With Main.ring1socket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.ring1socket3id = runfunction.getgemeffectid(socket3)
                            Case 17
                                With Main.ring2socket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.ring2socket3id = runfunction.getgemeffectid(socket3)
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

        My.Application.DoEvents()
    End Sub

    Private Sub getitem(ByVal slot As Integer)

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
                Dim quellcodeSplity88 As String = "-"
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
                Dim quellcodeSplity89 As String = ""
                Dim quellcodeSplity88 As Integer = 0
                Dim quellcodeSplitNEW As String = ""
                Try
                    Dim quellcodey88 As String = quellcodeSplit88
                    Dim anfangy88 As String = "/item/"
                    Dim endey88 As String = """ class=""item"""

                    quellcodeSplitNEW = Split(quellcodey88, anfangy88, 5)(1)
                    quellcodeSplitNEW = Split(quellcodeSplitNEW, endey88, 6)(0)
                    quellcodeSplity88 = CInt(Val(quellcodeSplitNEW))
                    Dim quellcodey89 As String = quellcodeSplit88
                    Dim anfangy89 As String = "><img src="""
                    Dim endey89 As String = ".jpg"""

                    quellcodeSplity89 = Split(quellcodey89, anfangy89, 5)(1)
                    quellcodeSplity89 = Split(quellcodeSplity89, endey89, 6)(0) & ".jpg"
                Catch ex As Exception
                    Process_Status.processreport.appendText(
                        Now.TimeOfDay.ToString & "// Error while loading itemid! > " & ex.ToString & vbNewLine)
                    My.Application.DoEvents()
                End Try

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
                    Process_Status.processreport.appendText(
                        Now.TimeOfDay.ToString & "// Error while loading item with id: " & quellcodeSplity88 & " > " &
                        ex.ToString & vbNewLine)
                    My.Application.DoEvents()
                    xquellcodesplity89 = "Fehler"
                End Try


                '     Dim clienyx88 As New WebClient
                '     Dim quellcodeyx88 As String = clienyx88.DownloadString("http://wowdata.buffed.de/?i=" & quellcodeSplity88)
                '   Dim anfangyx88 As String = "href=""/?i=" & quellcodeSplity88 & """><img src="""
                '   Dim endeyx88 As String = """></a></td>"
                '  Dim quellcodeSplityx88 As String
                '    quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
                ' "http://wowdata.buffed.de" & Split(quellcodeSplityx88, endeyx88, 6)(0)
                Process_Status.processreport.appendText(
                    Now.TimeOfDay.ToString & "// Setting up Item Slot: " & slot.ToString & vbNewLine)
                My.Application.DoEvents()
                Dim quellcodeSplityx88 = quellcodeSplity89
                Select Case slot
                    Case 1
                        Main.Kopf.Text = xquellcodesplity89
                        Main.Kopf.Visible = True
                        Main.kopfid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.kopfpic)
                    Case 2
                        Main.Hals.Text = xquellcodesplity89
                        Main.Hals.Visible = True
                        Main.halsid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Halspic)
                    Case 3
                        Main.Schulter.Text = xquellcodesplity89
                        Main.Schulter.Visible = True
                        Main.schulterid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Schulterpic)
                    Case 4
                        Main.Ruecken.Text = xquellcodesplity89
                        Main.Ruecken.Visible = True
                        Main.rueckenid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Rueckenpic)
                    Case 5
                        Main.Brust.Text = xquellcodesplity89
                        Main.Brust.Visible = True
                        Main.brustid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Brustpic)
                    Case 6
                        Main.Hemd.Text = xquellcodesplity89
                        Main.Hemd.Visible = True
                        Main.hemdid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Hemdpic)
                    Case 7
                        Main.Wappenrock.Text = xquellcodesplity89
                        Main.Wappenrock.Visible = True
                        Main.wappenrockid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Wappenrockpic)
                    Case 8
                        Main.Handgelenke.Text = xquellcodesplity89
                        Main.Handgelenke.Visible = True
                        Main.handgelenkeid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Handgelenkepic)
                    Case 9
                        Main.Haupt.Text = xquellcodesplity89
                        Main.Haupt.Visible = True
                        Main.hauptid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Hauptpic)
                        getweapontype(quellcodeSplity88)
                    Case 10
                        Main.Off.Text = xquellcodesplity89
                        Main.Off.Visible = True
                        Main.offid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Offpic)
                        getweapontype(quellcodeSplity88)
                    Case 11
                        Main.Distanz.Text = xquellcodesplity89
                        Main.Distanz.Visible = True
                        Main.distanzid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Distanzpic)
                        getweapontype(quellcodeSplity88)
                    Case 12
                        Main.Haende.Text = xquellcodesplity89
                        Main.Haende.Visible = True
                        Main.haendeid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Haendepic)
                    Case 13
                        Main.Guertel.Text = xquellcodesplity89
                        Main.Guertel.Visible = True
                        Main.guertelid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Guertelpic)
                    Case 14
                        Main.Beine.Text = xquellcodesplity89
                        Main.Beine.Visible = True
                        Main.beineid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Beinepic)
                    Case 15
                        Main.Stiefel.Text = xquellcodesplity89
                        Main.Stiefel.Visible = True
                        Main.stiefelid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Stiefelpic)
                    Case 16
                        Main.Ring1.Text = xquellcodesplity89
                        Main.Ring1.Visible = True
                        Main.ring1id = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Ring1pic)
                    Case 17
                        Main.Ring2.Text = xquellcodesplity89
                        Main.Ring2.Visible = True
                        Main.ring2id = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Ring2pic)
                    Case 18
                        Main.Schmuck1.Text = xquellcodesplity89
                        Main.Schmuck1.Visible = True
                        Main.schmuck1id = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Schmuck1pic)
                    Case 19
                        Main.Schmuck2.Text = xquellcodesplity89
                        Main.Schmuck2.Visible = True
                        Main.schmuck2id = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.Schmuck2pic)
                End Select
            End If


        Catch ex As Exception
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Error while setting up Itemslot: " & slot.ToString & vbNewLine)
            My.Application.DoEvents()
            Select Case slot
                Case 1
                    Main.Kopf.Text = "Error"
                Case 2
                    Main.Hals.Text = "Error"
                Case 3
                    Main.Schulter.Text = "Error"
                Case 4
                    Main.Ruecken.Text = "Error"
                Case 5
                    Main.Brust.Text = "Error"
                Case 6
                    Main.Hemd.Text = "Error"
                Case 7
                    Main.Wappenrock.Text = "Error"
                Case 8
                    Main.Handgelenke.Text = "Error"
                Case 9
                    Main.Haupt.Text = "Error"
                Case 10
                    Main.Off.Text = "Error"
                Case 11
                    Main.Distanz.Text = "Error"
                Case 12
                    Main.Haende.Text = "Error"
                Case 13
                    Main.Guertel.Text = "Error"
                Case 14
                    Main.Beine.Text = "Error"
                Case 15
                    Main.Stiefel.Text = "Error"
                Case 16
                    Main.Ring1.Text = "Error"
                Case 17
                    Main.Ring2.Text = "Error"
                Case 18
                    Main.Schmuck1.Text = "Error"
                Case 19
                    Main.Schmuck2.Text = "Error"
            End Select
        End Try
    End Sub

    Public Sub LoadImageFromUrl(ByRef url As String, ByVal pb As PictureBox)
        Try
            Dim request As HttpWebRequest = DirectCast(HttpWebRequest.Create(url), HttpWebRequest)
            Dim response As HttpWebResponse = DirectCast(request.GetResponse, HttpWebResponse)
            Dim img As Image = Image.FromStream(response.GetResponseStream())
            response.Close()
            pb.SizeMode = PictureBoxSizeMode.StretchImage
            pb.Image = img
        Catch ex As Exception
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Error while loading Image from URL: " & url & " > " & ex.ToString &
                vbNewLine)
            My.Application.DoEvents()
        End Try
    End Sub

    Public Sub getweapontype(ByVal itemid As Integer)
        runfunction.getweapontype(itemid)
      End Sub
End Class
