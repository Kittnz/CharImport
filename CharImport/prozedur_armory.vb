'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
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
    Dim ServerString As String = Main.MainInstance.ServerString
    Dim characterguid As Integer
    Dim reporttext As RichTextBox = Process_Status.processreport
    Dim charnumber As Integer
    Dim xoverview As Boolean
    Dim runfunction As New Functions
    Public Sub prozedur(ByVal armory_link As String, ByVal cnumber As Integer, ByVal overview As Boolean)
        Process_Status.BringToFront()

        xoverview = overview
        Main.MainInstance.setallempty()
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

        Main.MainInstance.armoryrun = True
        Dim anfang As String = "/character/"
        Dim ende As String = "/"
        Dim realm As String
        realm = Split(armory_link, anfang, 5)(1)
        Main.MainInstance.realmname = Split(realm, ende, 6)(0)

        Dim anfang2 As String = "http://"
        Dim ende2 As String = ".battle"
        Dim loc_region As String
        loc_region = Split(armory_link, anfang2, 5)(1)
        loc_region = Split(loc_region, ende2, 6)(0)
        Main.MainInstance.battlenet_region = loc_region
        Try
            Dim quellclient As New WebClient
            Main.MainInstance.quelltext = quellclient.DownloadString(armory_link)
            Dim s As String = Main.MainInstance.quelltext
            Dim b() As Byte = Encoding.Default.GetBytes(s)
            Dim s1 As String = Encoding.UTF8.GetString(b)
            Main.MainInstance.quelltext = s1
            If Main.MainInstance.quelltext.Contains("#39;") Then Main.MainInstance.quelltext = Main.MainInstance.quelltext.Replace("#39;", "'")
            If Main.MainInstance.quelltext.Contains("Ã¼") Then Main.MainInstance.quelltext = Main.MainInstance.quelltext.Replace("Ã¼", "ü")
            If Main.MainInstance.quelltext.Contains("Ã¤") Then Main.MainInstance.quelltext = Main.MainInstance.quelltext.Replace("Ã¤", "ä")
            If Main.MainInstance.quelltext.Contains("Ã¶") Then Main.MainInstance.quelltext = Main.MainInstance.quelltext.Replace("Ã¶", "ö")
            If Main.MainInstance.quelltext.Contains("ÃŸ") Then Main.MainInstance.quelltext = Main.MainInstance.quelltext.Replace("ÃŸ", "ß")
            Dim yquellclient As New WebClient
            '  Main.talentpage = yquellclient.DownloadString(armory_link.Replace("advanced", "talent/primary"))
            If Main.MainInstance.talentpage.Contains("Ã¼") Then Main.MainInstance.talentpage = Main.MainInstance.talentpage.Replace("Ã¼", "ü")
            If Main.MainInstance.talentpage.Contains("Ã¤") Then Main.MainInstance.talentpage = Main.MainInstance.talentpage.Replace("Ã¤", "ä")
            If Main.MainInstance.talentpage.Contains("Ã¶") Then Main.MainInstance.talentpage = Main.MainInstance.talentpage.Replace("Ã¶", "ö")
            If Main.MainInstance.talentpage.Contains("ÃŸ") Then Main.MainInstance.talentpage = Main.MainInstance.talentpage.Replace("ÃŸ", "ß")
            Dim zquellclient As New WebClient
            '  Main.sectalentpage = zquellclient.DownloadString(armory_link.Replace("advanced", "talent/secondary"))
            If Main.MainInstance.sectalentpage.Contains("Ã¼") Then Main.MainInstance.sectalentpage = Main.MainInstance.sectalentpage.Replace("Ã¼", "ü")
            If Main.MainInstance.sectalentpage.Contains("Ã¤") Then Main.MainInstance.sectalentpage = Main.MainInstance.sectalentpage.Replace("Ã¤", "ä")
            If Main.MainInstance.sectalentpage.Contains("Ã¶") Then Main.MainInstance.sectalentpage = Main.MainInstance.sectalentpage.Replace("Ã¶", "ö")
            If Main.MainInstance.sectalentpage.Contains("ÃŸ") Then Main.MainInstance.sectalentpage = Main.MainInstance.sectalentpage.Replace("ÃŸ", "ß")
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


            Dim quellcodeyx88 As String = Main.MainInstance.quelltext
            Dim anfangyx88 As String = "<div class=""name""><a href="
            Dim endeyx88 As String = "</div>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Dim anfangyx88y As String = "rel=""np"">"
            Dim endeyx88y As String = "</a>"
            Dim quellcodeSplityx88y As String
            quellcodeSplityx88y = Split(quellcodeSplityx88, anfangyx88y, 5)(1)
            quellcodeSplityx88y = Split(quellcodeSplityx88y, endeyx88y, 6)(0)
            Main.MainInstance.char_name = quellcodeSplityx88y
            Main.MainInstance.level.Text = quellcodeSplityx88y & ", "
            Main.MainInstance.charopt.Add("name" & charnumber.ToString & "=" & quellcodeSplityx88y)
            Process_Status.processreport.AppendText(
                Now.TimeOfDay.ToString & "// Got Character Name: " & quellcodeSplityx88y & vbNewLine)
            My.Application.DoEvents()
        Catch ex As Exception
            Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Error: " & ex.ToString & vbNewLine)
            Main.MainInstance.errorcount += 1
            My.Application.DoEvents()
            Main.MainInstance.level.Visible = True
        End Try
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "// Loading Character Level..." & vbNewLine)
        My.Application.DoEvents()
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = Main.MainInstance.quelltext
            Dim anfangyx88 As String = "<span class=""level""><strong>"
            Dim endeyx88 As String = "</strong></span>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.MainInstance.level.Text = Main.MainInstance.level.Text & quellcodeSplityx88 & ", "
            Main.MainInstance.level.Visible = True
            Main.MainInstance.levelid = CInt(Val(quellcodeSplityx88))
            Main.MainInstance.char_level = CInt(Val(quellcodeSplityx88))
            Main.MainInstance.charopt.Add("level" & charnumber.ToString & "=" & quellcodeSplityx88)
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Got Character Level: " & quellcodeSplityx88 & vbNewLine)
            My.Application.DoEvents()
        Catch ex As Exception
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Error (Set Level to 80 instead): " & ex.ToString & vbNewLine)
            Main.MainInstance.errorcount += 1

            Main.MainInstance.level.Text = "Failed to load!"
            Main.MainInstance.char_level = 80
            Main.MainInstance.charopt.Add("level" & charnumber.ToString & "=80")
            My.Application.DoEvents()
            Main.MainInstance.level.Visible = True
        End Try
        Try
            '### NEW ### Get Gender
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String =
                    clienyx88.DownloadString(
                        "http://" & Main.MainInstance.battlenet_region & ".battle.net/api/wow/character/" & Main.MainInstance.realmname & "/" &
                        Main.MainInstance.char_name)
            Dim anfangyx88 As String = """gender"":"
            Dim endeyx88 As String = ","""
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.MainInstance.char_gender = CInt(quellcodeSplityx88)
            Process_Status.processreport.AppendText(
                Now.TimeOfDay.ToString & "// Got Character Gender: " & quellcodeSplityx88 & vbNewLine)
            My.Application.DoEvents()
        Catch ex As Exception
            Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Error: " & ex.ToString & vbNewLine)
            Main.MainInstance.errorcount += 1
            My.Application.DoEvents()
        End Try
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "// Loading Character Race..." & vbNewLine)
        My.Application.DoEvents()
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = Main.MainInstance.quelltext
            Dim anfangyx88 As String = "/game/race/"
            Dim endeyx88 As String = """ class="
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.MainInstance.level.Text = Main.MainInstance.level.Text & quellcodeSplityx88.ToUpper & ", "
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
            Main.MainInstance.char_race = CInt(Val(quellcodeSplityx88))
            Main.MainInstance.charopt.Add("race" & charnumber.ToString & "=" & quellcodeSplityx88)
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Got Character Race: " & quellcodeSplityx88 & vbNewLine)
            My.Application.DoEvents()
            Connect.race.Visible = True
        Catch ex As Exception
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Error (Set race to human instead): " & ex.ToString & vbNewLine)
            Main.MainInstance.errorcount += 1
            Main.MainInstance.char_race = 1
            My.Application.DoEvents()
            Connect.race.Visible = False
        End Try
        Process_Status.processreport.appendText(Now.TimeOfDay.ToString & "// Loading Character Class..." & vbNewLine)
        My.Application.DoEvents()
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = Main.MainInstance.quelltext
            Dim anfangyx88 As String = "/game/class/"
            Dim endeyx88 As String = """ class="
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.MainInstance.level.Text = Main.MainInstance.level.Text & quellcodeSplityx88.ToUpper
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
            Main.MainInstance.char_class = CInt(Val(quellcodeSplityx88))
            Main.MainInstance.charopt.Add("class" & charnumber.ToString & "=" & quellcodeSplityx88)
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Got Character Class: " & quellcodeSplityx88 & vbNewLine)
            My.Application.DoEvents()
            Connect.playerclass.Visible = True
        Catch ex As Exception
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Error (Set class to warrior instead): " & ex.ToString & vbNewLine)
            Main.MainInstance.errorcount += 1
            Main.MainInstance.char_class = 1
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
            addtoglyphlist("primeglyph1", Main.MainInstance.primeglyph1)
            addtoglyphlist("primeglyph2", Main.MainInstance.primeglyph2)
            addtoglyphlist("primeglyph3", Main.MainInstance.primeglyph3)
            addtoglyphlist("majorglyph1", Main.MainInstance.majorglyph1)
            addtoglyphlist("majorglyph2", Main.MainInstance.majorglyph2)
            addtoglyphlist("majorglyph3", Main.MainInstance.majorglyph3)
            addtoglyphlist("minorglyph1", Main.MainInstance.minorglyph1)
            addtoglyphlist("minorglyph2", Main.MainInstance.minorglyph2)
            addtoglyphlist("minorglyph3", Main.MainInstance.minorglyph3)

            addtoglyphlist("secprimeglyph1", Main.MainInstance.secprimeglyph1)
            addtoglyphlist("secprimeglyph2", Main.MainInstance.secprimeglyph2)
            addtoglyphlist("secprimeglyph3", Main.MainInstance.secprimeglyph3)
            addtoglyphlist("secmajorglyph1", Main.MainInstance.secmajorglyph1)
            addtoglyphlist("secmajorglyph2", Main.MainInstance.secmajorglyph2)
            addtoglyphlist("secmajorglyph3", Main.MainInstance.secmajorglyph3)
            addtoglyphlist("secminorglyph1", Main.MainInstance.secminorglyph1)
            addtoglyphlist("secminorglyph2", Main.MainInstance.secminorglyph2)
            addtoglyphlist("secminorglyph3", Main.MainInstance.secminorglyph3)
        End If
        addtoitemlist("kopf", Main.MainInstance.kopfid.ToString)
        addtovzlist("kopfvz", Main.MainInstance.kopfvzid.ToString)
        addtogemlist("kopfsocket1", Main.MainInstance.kopfsocket1id.ToString)
        addtogemlist("kopfsocket2", Main.MainInstance.kopfsocket2id.ToString)
        addtogemlist("kopfsocket3", Main.MainInstance.kopfsocket3id.ToString)

        addtoitemlist("hals", Main.MainInstance.halsid.ToString)
        addtovzlist("halsvz", Main.MainInstance.halsvzid.ToString)
        addtogemlist("halssocket1", Main.MainInstance.halssocket1id.ToString)
        addtogemlist("halssocket2", Main.MainInstance.halssocket2id.ToString)
        addtogemlist("halssocket3", Main.MainInstance.halssocket3id.ToString)

        addtoitemlist("schulter", Main.MainInstance.schulterid.ToString)
        addtovzlist("schultervz", Main.MainInstance.schultervzid.ToString)
        addtogemlist("schultersocket1", Main.MainInstance.schultersocket1id.ToString)
        addtogemlist("schultersocket2", Main.MainInstance.schultersocket2id.ToString)
        addtogemlist("schultersocket3", Main.MainInstance.schultersocket3id.ToString)

        addtoitemlist("ruecken", Main.MainInstance.rueckenid.ToString)
        addtovzlist("rueckenvz", Main.MainInstance.rueckenvzid.ToString)
        addtogemlist("rueckensocket1", Main.MainInstance.rueckensocket1id.ToString)
        addtogemlist("rueckensocket2", Main.MainInstance.rueckensocket2id.ToString)
        addtogemlist("rueckensocket3", Main.MainInstance.rueckensocket3id.ToString)

        addtoitemlist("brust", Main.MainInstance.brustid.ToString)
        addtovzlist("brustvz", Main.MainInstance.brustvzid.ToString)
        addtogemlist("brustsocket1", Main.MainInstance.brustsocket1id.ToString)
        addtogemlist("brustsocket2", Main.MainInstance.brustsocket2id.ToString)
        addtogemlist("brustsocket3", Main.MainInstance.brustsocket3id.ToString)

        addtoitemlist("hemd", Main.MainInstance.hemdid.ToString)

        addtoitemlist("wappenrock", Main.MainInstance.wappenrockid.ToString)

        addtoitemlist("haupt", Main.MainInstance.hauptid.ToString)
        addtovzlist("hauptvz", Main.MainInstance.hauptvzid.ToString)
        addtogemlist("hauptsocket1", Main.MainInstance.hauptsocket1id.ToString)
        addtogemlist("hauptsocket2", Main.MainInstance.hauptsocket2id.ToString)
        addtogemlist("hauptsocket3", Main.MainInstance.hauptsocket3id.ToString)

        addtoitemlist("off", Main.MainInstance.offid.ToString)
        addtovzlist("offvz", Main.MainInstance.offvzid.ToString)
        addtogemlist("offsocket1", Main.MainInstance.offsocket1id.ToString)
        addtogemlist("offsocket2", Main.MainInstance.offsocket2id.ToString)
        addtogemlist("offsocket3", Main.MainInstance.offsocket3id.ToString)

        addtoitemlist("distanz", Main.MainInstance.distanzid.ToString)
        addtovzlist("distanzvz", Main.MainInstance.distanzvzid.ToString)
        addtogemlist("distanzsocket1", Main.MainInstance.distanzsocket1id.ToString)
        addtogemlist("distanzsocket2", Main.MainInstance.distanzsocket2id.ToString)
        addtogemlist("distanzsocket3", Main.MainInstance.distanzsocket3id.ToString)

        addtoitemlist("handgelenke", Main.MainInstance.handgelenkeid.ToString)
        addtovzlist("handgelenkevz", Main.MainInstance.handgelenkevzid.ToString)
        addtogemlist("handgelenkesocket1", Main.MainInstance.handgelenkesocket1id.ToString)
        addtogemlist("handgelenkesocket2", Main.MainInstance.handgelenkesocket2id.ToString)
        addtogemlist("handgelenkesocket3", Main.MainInstance.handgelenkesocket3id.ToString)

        addtoitemlist("haende", Main.MainInstance.haendeid.ToString)
        addtovzlist("haendevz", Main.MainInstance.haendevzid.ToString)
        addtogemlist("haendesocket1", Main.MainInstance.haendesocket1id.ToString)
        addtogemlist("haendesocket2", Main.MainInstance.haendesocket2id.ToString)
        addtogemlist("haendesocket3", Main.MainInstance.haendesocket3id.ToString)

        addtoitemlist("guertel", Main.MainInstance.guertelid.ToString)
        addtovzlist("guertelvz", Main.MainInstance.guertelvzid.ToString)
        addtogemlist("guertelsocket1", Main.MainInstance.guertelsocket1id.ToString)
        addtogemlist("guertelsocket2", Main.MainInstance.guertelsocket2id.ToString)
        addtogemlist("guertelsocket3", Main.MainInstance.guertelsocket3id.ToString)

        addtoitemlist("beine", Main.MainInstance.beineid.ToString)
        addtovzlist("beinevz", Main.MainInstance.beinevzid.ToString)
        addtogemlist("beinesocket1", Main.MainInstance.beinesocket1id.ToString)
        addtogemlist("beinesocket2", Main.MainInstance.beinesocket2id.ToString)
        addtogemlist("beinesocket3", Main.MainInstance.beinesocket3id.ToString)

        addtoitemlist("stiefel", Main.MainInstance.stiefelid.ToString)
        addtovzlist("stiefelvz", Main.MainInstance.stiefelvzid.ToString)
        addtogemlist("stiefelsocket1", Main.MainInstance.stiefelsocket1id.ToString)
        addtogemlist("stiefelsocket2", Main.MainInstance.stiefelsocket2id.ToString)
        addtogemlist("stiefelsocket3", Main.MainInstance.stiefelsocket3id.ToString)

        addtoitemlist("ring1", Main.MainInstance.ring1id.ToString)
        addtovzlist("ring1vz", Main.MainInstance.ring1vzid.ToString)
        addtogemlist("ring1socket1", Main.MainInstance.ring1socket1id.ToString)
        addtogemlist("ring1socket2", Main.MainInstance.ring1socket2id.ToString)
        addtogemlist("ring1socket3", Main.MainInstance.ring1socket3id.ToString)

        addtoitemlist("ring2", Main.MainInstance.ring2id.ToString)
        addtovzlist("ring2vz", Main.MainInstance.ring2vzid.ToString)
        addtogemlist("ring2socket1", Main.MainInstance.ring2socket1id.ToString)
        addtogemlist("ring2socket2", Main.MainInstance.ring2socket2id.ToString)
        addtogemlist("ring2socket3", Main.MainInstance.ring2socket3id.ToString)

        addtoitemlist("schmuck1", Main.MainInstance.schmuck1id.ToString)
        addtovzlist("schmuck1vz", Main.MainInstance.schmuck1vzid.ToString)
        addtogemlist("schmuck1socket1", Main.MainInstance.schmuck1socket1id.ToString)
        addtogemlist("schmuck1socket2", Main.MainInstance.schmuck1socket2id.ToString)
        addtogemlist("schmuck1socket3", Main.MainInstance.schmuck1socket3id.ToString)

        addtoitemlist("schmuck2", Main.MainInstance.schmuck2id.ToString)
        addtovzlist("schmuck2vz", Main.MainInstance.schmuck2vzid.ToString)
        addtogemlist("schmuck2socket1", Main.MainInstance.schmuck2socket1id.ToString)
        addtogemlist("schmuck2socket2", Main.MainInstance.schmuck2socket2id.ToString)
        addtogemlist("schmuck2socket3", Main.MainInstance.schmuck2socket3id.ToString)
        Main.MainInstance.datasets += 1
        Dim addtataset As New CIUFile
        addtataset.adddataset()
        Main.MainInstance.Panel21.Location = New Point(5000, 5000)
        Main.MainInstance.UseWaitCursor = False
        'Starter.Close()
        Application.DoEvents()
    End Sub

    Private Sub addtoglyphlist(ByVal key As String, ByVal value As String)
        Main.MainInstance.glyphlist.Add(charnumber & key & "=" & value)
    End Sub

    Private Sub addtovzlist(ByVal key As String, ByVal value As String)
        Main.MainInstance.vzlist.Add(charnumber & key & "=" & value)
    End Sub

    Private Sub addtogemlist(ByVal key As String, ByVal value As String)
        Main.MainInstance.gemlist.Add(charnumber & key & "=" & value)
    End Sub

    Private Sub addtoitemlist(ByVal key As String, ByVal value As String)
        Main.MainInstance.itemlist.Add(charnumber & key & "=" & value)
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
        Main.MainInstance.talentlist = Main.MainInstance.emptylist
        Main.MainInstance.talentlist.Clear()
        Try

            Dim quellcodeyx88 As String = Main.MainInstance.talentpage
            Dim anfangyx88 As String = "build: """
            Dim endeyx88 As String = ""","
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.MainInstance.talentstring = quellcodeSplityx88

        Catch ex As Exception

        End Try
        Try

            Dim quellcodeyx88 As String = Main.MainInstance.talentpage
            Dim anfangyx88 As String = "<a href=""/wow/de/game/class/"
            Dim endeyx88 As String = """ class=""class"">"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            Main.MainInstance.charclass = quellcodeSplityx88

        Catch ex As Exception

        End Try
        If Main.MainInstance.charclass = "mage" Then
            '  Main.MainInstance.talentlist = Main.MainInstance.magetalentprogress.progress(Main.MainInstance.talentstring)
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
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.kopfid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Kopf.Visible = False
                Main.MainInstance.kopfid = Nothing
                Main.MainInstance.kopfpic.Image = My.Resources.empty
                Main.MainInstance.kopfsocket1.Visible = False
                Main.MainInstance.kopfsocket2.Visible = False
                Main.MainInstance.kopfsocket3.Visible = False
                Main.MainInstance.kopfvz.Visible = False
                Main.MainInstance.kopfsocket1id = Nothing
                Main.MainInstance.kopfsocket2id = Nothing
                Main.MainInstance.kopfsocket3id = Nothing
                Main.MainInstance.kopfvzid = Nothing
                Main.MainInstance.Kopf.Text = "-"
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.halsid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Hals.Visible = False
                Main.MainInstance.halsid = Nothing
                Main.MainInstance.Halspic.Image = My.Resources.empty
                Main.MainInstance.halssocket1.Visible = False
                Main.MainInstance.halssocket2.Visible = False
                Main.MainInstance.halssocket3.Visible = False
                Main.MainInstance.halsvz.Visible = False
                Main.MainInstance.halssocket1id = Nothing
                Main.MainInstance.halssocket2id = Nothing
                Main.MainInstance.halssocket3id = Nothing
                Main.MainInstance.halsvzid = Nothing
                Main.MainInstance.Hals.Text = "-"
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.schulterid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Schulter.Visible = False
                Main.MainInstance.schulterid = Nothing
                Main.MainInstance.Schulterpic.Image = My.Resources.empty
                Main.MainInstance.schultersocket1.Visible = False
                Main.MainInstance.schultersocket2.Visible = False
                Main.MainInstance.schultersocket3.Visible = False
                Main.MainInstance.schultervz.Visible = False
                Main.MainInstance.schultersocket1id = Nothing
                Main.MainInstance.schultersocket2id = Nothing
                Main.MainInstance.schultersocket3id = Nothing
                Main.MainInstance.schultervzid = Nothing
                Main.MainInstance.Schulter.Text = "-"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.rueckenid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Ruecken.Visible = False
                Main.MainInstance.rueckenid = Nothing
                Main.MainInstance.Rueckenpic.Image = My.Resources.empty
                Main.MainInstance.rueckensocket1.Visible = False
                Main.MainInstance.rueckensocket2.Visible = False
                Main.MainInstance.rueckensocket3.Visible = False
                Main.MainInstance.rueckenvz.Visible = False
                Main.MainInstance.rueckensocket1id = Nothing
                Main.MainInstance.rueckensocket2id = Nothing
                Main.MainInstance.rueckensocket3id = Nothing
                Main.MainInstance.rueckenvzid = Nothing
                Main.MainInstance.Ruecken.Text = "-"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.brustid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Brust.Visible = False
                Main.MainInstance.brustid = Nothing
                Main.MainInstance.Brustpic.Image = My.Resources.empty
                Main.MainInstance.brustsocket1.Visible = False
                Main.MainInstance.brustsocket2.Visible = False
                Main.MainInstance.brustsocket3.Visible = False
                Main.MainInstance.brustvz.Visible = False
                Main.MainInstance.brustsocket1id = Nothing
                Main.MainInstance.brustsocket2id = Nothing
                Main.MainInstance.brustsocket3id = Nothing
                Main.MainInstance.brustvzid = Nothing
                Main.MainInstance.Brust.Text = "-"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.hemdid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then


            Else
                Main.MainInstance.Hemd.Visible = False
                Main.MainInstance.hemdid = Nothing
                Main.MainInstance.Hemdpic.Image = My.Resources.empty
                Main.MainInstance.Hemd.Text = "-"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.wappenrockid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Wappenrock.Visible = False
                Main.MainInstance.wappenrockid = Nothing
                Main.MainInstance.Wappenrockpic.Image = My.Resources.empty
                Main.MainInstance.Wappenrock.Text = "-"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.handgelenkeid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Handgelenke.Visible = False
                Main.MainInstance.handgelenkeid = Nothing
                Main.MainInstance.Handgelenkepic.Image = My.Resources.empty
                Main.MainInstance.Handgelenkesocket1.Visible = False
                Main.MainInstance.handgelenkesocket2.Visible = False
                Main.MainInstance.Handgelenkesocket3.Visible = False
                Main.MainInstance.handgelenkevz.Visible = False
                Main.MainInstance.handgelenkesocket1id = Nothing
                Main.MainInstance.handgelenkesocket2id = Nothing
                Main.MainInstance.handgelenkesocket3id = Nothing
                Main.MainInstance.handgelenkevzid = Nothing
                Main.MainInstance.Handgelenke.Text = "-"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.hauptid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Haupt.Visible = False
                Main.MainInstance.hauptid = Nothing
                Main.MainInstance.Hauptpic.Image = My.Resources.empty
                Main.MainInstance.Hauptsocket1.Visible = False
                Main.MainInstance.Hauptsocket2.Visible = False
                Main.MainInstance.hauptsocket3.Visible = False
                Main.MainInstance.hauptvz.Visible = False
                Main.MainInstance.hauptsocket1id = Nothing
                Main.MainInstance.hauptsocket2id = Nothing
                Main.MainInstance.hauptsocket3id = Nothing
                Main.MainInstance.hauptvzid = Nothing
                Main.MainInstance.Haupt.Text = "-"
                Main.MainInstance.hauptvzlabel2.Visible = False
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.offid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Off.Visible = False
                Main.MainInstance.offid = Nothing
                Main.MainInstance.Offpic.Image = My.Resources.empty
                Main.MainInstance.Offsocket1.Visible = False
                Main.MainInstance.Offsocket2.Visible = False
                Main.MainInstance.offsocket3.Visible = False
                Main.MainInstance.offvz.Visible = False
                Main.MainInstance.offsocket1id = Nothing
                Main.MainInstance.offsocket2id = Nothing
                Main.MainInstance.offsocket3id = Nothing
                Main.MainInstance.offvzid = Nothing
                Main.MainInstance.Off.Text = "-"
                Main.MainInstance.offvzlabel2.Visible = False
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.distanzid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Distanz.Visible = False
                Main.MainInstance.distanzid = Nothing
                Main.MainInstance.Distanzpic.Image = My.Resources.empty
                Main.MainInstance.Distanzsocket1.Visible = False
                Main.MainInstance.Distanzsocket2.Visible = False
                Main.MainInstance.distanzsocket3.Visible = False
                Main.MainInstance.distanzvz.Visible = False
                Main.MainInstance.distanzsocket1id = Nothing
                Main.MainInstance.distanzsocket2id = Nothing
                Main.MainInstance.distanzsocket3id = Nothing
                Main.MainInstance.distanzvzid = Nothing
                Main.MainInstance.Distanz.Text = "-"
                Main.MainInstance.distanzvzlabel2.Visible = False
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.haendeid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Haende.Visible = False
                Main.MainInstance.haendeid = Nothing
                Main.MainInstance.Haendepic.Image = My.Resources.empty
                Main.MainInstance.haendesocket1.Visible = False
                Main.MainInstance.haendesocket2.Visible = False
                Main.MainInstance.haendesocket3.Visible = False
                Main.MainInstance.haendevz.Visible = False
                Main.MainInstance.haendesocket1id = Nothing
                Main.MainInstance.haendesocket2id = Nothing
                Main.MainInstance.haendesocket3id = Nothing
                Main.MainInstance.haendevzid = Nothing
                Main.MainInstance.Haende.Text = "-"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.guertelid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Guertel.Visible = False
                Main.MainInstance.guertelid = Nothing
                Main.MainInstance.Guertelpic.Image = My.Resources.empty
                Main.MainInstance.guertelsocket1.Visible = False
                Main.MainInstance.guertelsocket2.Visible = False
                Main.MainInstance.guertelsocket3.Visible = False
                Main.MainInstance.guertelvz.Visible = False
                Main.MainInstance.guertelsocket1id = Nothing
                Main.MainInstance.guertelsocket2id = Nothing
                Main.MainInstance.guertelsocket3id = Nothing
                Main.MainInstance.guertelvzid = Nothing
                Main.MainInstance.Guertel.Text = "-"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.beineid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Beine.Visible = False
                Main.MainInstance.beineid = Nothing
                Main.MainInstance.Beinepic.Image = My.Resources.empty
                Main.MainInstance.beinesocket1.Visible = False
                Main.MainInstance.beinesocket2.Visible = False
                Main.MainInstance.beinesocket3.Visible = False
                Main.MainInstance.beinevz.Visible = False
                Main.MainInstance.beinesocket1id = Nothing
                Main.MainInstance.beinesocket2id = Nothing
                Main.MainInstance.beinesocket3id = Nothing
                Main.MainInstance.beinevzid = Nothing
                Main.MainInstance.Beine.Text = "-"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.stiefelid)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Stiefel.Visible = False
                Main.MainInstance.stiefelid = Nothing
                Main.MainInstance.Stiefelpic.Image = My.Resources.empty
                Main.MainInstance.stiefelsocket1.Visible = False
                Main.MainInstance.stiefelsocket2.Visible = False
                Main.MainInstance.stiefelsocket3.Visible = False
                Main.MainInstance.stiefelvz.Visible = False
                Main.MainInstance.stiefelsocket1id = Nothing
                Main.MainInstance.stiefelsocket2id = Nothing
                Main.MainInstance.stiefelsocket3id = Nothing
                Main.MainInstance.stiefelvzid = Nothing
                Main.MainInstance.Stiefel.Text = "-"
            End If
        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.ring1id)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Ring1.Visible = False
                Main.MainInstance.ring1id = Nothing
                Main.MainInstance.Ring1pic.Image = My.Resources.empty
                Main.MainInstance.Ring1socket1.Visible = False
                Main.MainInstance.ring1socket2.Visible = False
                Main.MainInstance.ring1socket3.Visible = False
                Main.MainInstance.ring1vz.Visible = False
                Main.MainInstance.ring1socket1id = Nothing
                Main.MainInstance.ring1socket2id = Nothing
                Main.MainInstance.ring1socket3id = Nothing
                Main.MainInstance.ring1vzid = Nothing
                Main.MainInstance.Ring1.Text = "-"
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.ring2id)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Ring2.Visible = False
                Main.MainInstance.ring2id = Nothing
                Main.MainInstance.Ring2pic.Image = My.Resources.empty
                Main.MainInstance.ring2socket1.Visible = False
                Main.MainInstance.ring2socket2.Visible = False
                Main.MainInstance.ring2socket3.Visible = False
                Main.MainInstance.ring2vz.Visible = False
                Main.MainInstance.ring2socket1id = Nothing
                Main.MainInstance.ring2socket2id = Nothing
                Main.MainInstance.ring2socket3id = Nothing
                Main.MainInstance.ring2vzid = Nothing
                Main.MainInstance.Ring2.Text = "-"
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.schmuck1id)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Schmuck1.Visible = False
                Main.MainInstance.schmuck1id = Nothing
                Main.MainInstance.Schmuck1pic.Image = My.Resources.empty

                Main.MainInstance.schmuck1vz.Visible = False
                Main.MainInstance.schmuck1socket1id = Nothing
                Main.MainInstance.schmuck1socket2id = Nothing
                Main.MainInstance.schmuck1socket3id = Nothing
                Main.MainInstance.schmuck1vzid = Nothing
                Main.MainInstance.Schmuck1.Text = "-"
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.schmuck2id)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.Schmuck2.Visible = False
                Main.MainInstance.schmuck2id = Nothing
                Main.MainInstance.Schmuck2pic.Image = My.Resources.empty

                Main.MainInstance.schmuck2vz.Visible = False
                Main.MainInstance.schmuck2socket1id = Nothing
                Main.MainInstance.schmuck2socket2id = Nothing
                Main.MainInstance.schmuck2socket3id = Nothing
                Main.MainInstance.schmuck2vzid = Nothing
                Main.MainInstance.Schmuck2.Text = "-"
            End If
        Catch ex As Exception

        End Try
        'Glyphen
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.primeglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.glyphpic1 = My.Resources.empty
                Main.MainInstance.textprimeglyph1 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.primeglyph2)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.glyphpic2 = My.Resources.empty
                Main.MainInstance.textprimeglyph2 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.primeglyph3)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.glyphpic3 = My.Resources.empty
                Main.MainInstance.textprimeglyph3 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.majorglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.glyphpic4 = My.Resources.empty
                Main.MainInstance.textmajorglyph1 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.majorglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.glyphpic5 = My.Resources.empty
                Main.MainInstance.textmajorglyph2 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.majorglyph3)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.glyphpic6 = My.Resources.empty
                Main.MainInstance.textmajorglyph3 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.minorglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.glyphpic7 = My.Resources.empty
                Main.MainInstance.textminorglyph1 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.minorglyph2)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.glyphpic8 = My.Resources.empty
                Main.MainInstance.textminorglyph2 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.minorglyph3)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.glyphpic9 = My.Resources.empty
                Main.MainInstance.textminorglyph3 = ""
            End If
        Catch ex As Exception

        End Try


        'SekundärGlyphen
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.secprimeglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.secglyphpic1 = My.Resources.empty
                Main.MainInstance.sectextprimeglyph1 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.secprimeglyph2)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.secglyphpic2 = My.Resources.empty
                Main.MainInstance.sectextprimeglyph2 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.secprimeglyph3)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.secglyphpic3 = My.Resources.empty
                Main.MainInstance.sectextprimeglyph3 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.secmajorglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.secglyphpic4 = My.Resources.empty
                Main.MainInstance.sectextmajorglyph1 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.secmajorglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.secglyphpic5 = My.Resources.empty
                Main.MainInstance.sectextmajorglyph2 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.secmajorglyph3)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.secglyphpic6 = My.Resources.empty
                Main.MainInstance.sectextmajorglyph3 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.secminorglyph1)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.secglyphpic7 = My.Resources.empty
                Main.MainInstance.sectextminorglyph1 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.secminorglyph2)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.secglyphpic8 = My.Resources.empty
                Main.MainInstance.sectextminorglyph2 = ""
            End If
        Catch ex As Exception

        End Try
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/item=" & Main.MainInstance.secminorglyph3)
            Dim anfangyx88 As String = "</li><li>Added in patch "
            Dim endeyx88 As String = "</li></ul>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            quellcodeSplityx88 = quellcodeSplityx88.Replace(".", "")
            If quellcodeSplityx88.Length = 3 Then quellcodeSplityx88 = quellcodeSplityx88 & "0"
            If CInt(Val(version)) >= CInt(Val(quellcodeSplityx88)) Then

            Else
                Main.MainInstance.secglyphpic9 = My.Resources.empty
                Main.MainInstance.sectextminorglyph3 = ""
            End If
        Catch ex As Exception

        End Try
        Main.MainInstance.UseWaitCursor = False
        Application.DoEvents()
        wait.Close()
        MsgBox("Gegenstände wurden gefiltert!", MsgBoxStyle.Information, "Info")
        Main.MainInstance.BringToFront()
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
                        "http://" & Main.MainInstance.battlenet_region & ".battle.net/api/wow/character/" & Main.MainInstance.realmname & "/" &
                        Main.MainInstance.char_name & "?fields=appearance")
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
            Main.MainInstance.playerBytes = CInt(CLng("&H" & bytestring).ToString)
        Catch
        End Try
        Try
            Dim feature As String = Hex$(Long.Parse(tmpfeature))
            If feature.Length = 1 Then feature = "0" & feature

        Catch ex As Exception

        End Try
        ' value?
    End Sub

    ' Main.MainInstance.character_reputatuion_list.Add("<faction>" & faction & "</faction><standing>" & standing & "</standing><flags>" & flags & "</flags>")
    Private Sub getquests()
        Dim queststring As String = ""
        Try
            Dim client As New WebClient
            Dim quellcode As String =
                    client.DownloadString(
                        "http://" & Main.MainInstance.battlenet_region & ".battle.net/api/wow/character/" & Main.MainInstance.realmname & "/" &
                        Main.MainInstance.char_name & "?fields=quests")
            Dim anfangyx88 As String = """quests"":["
            Dim endeyx88 As String = "]}"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcode, anfangyx88, 5)(1)
            queststring = Split(quellcodeSplityx88, endeyx88, 6)(0) & ","
            Main.MainInstance.finished_quests = queststring
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
                    "http://" & Main.MainInstance.battlenet_region & ".battle.net/api/wow/character/" & Main.MainInstance.realmname & "/" &
                    Main.MainInstance.char_name & "?fields=reputation")
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
                Main.MainInstance.character_reputatuion_list.Add(
                    "<faction>" & factionid & "</faction><standing>" & standing.ToString & "</standing><flags>1</flags>")
            Loop Until loopcounter = excounter

        End If
    End Sub

    '     Main.MainInstance.character_achievement_list.Add("<av>" & avid & "</av><date>" & xdate & "</date>")
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
                    "http://" & Main.MainInstance.battlenet_region & ".battle.net/api/wow/character/" & Main.MainInstance.realmname & "/" &
                    Main.MainInstance.char_name & "?fields=achievements")
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
                    Main.MainInstance.character_achievement_list.Add("<av>" & avid & "</av><date>" & timestamp & "</date>")
                Loop Until loopcounter = excounter
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub getglyph()
        Dim glyphid As String = ""
        Dim glyphstring As String = ""
        Dim glyphname As String = ""
        Dim xname As String = Main.MainInstance.realmname
        Dim zname As String = Main.MainInstance.char_name
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String =
                    clienyx88.DownloadString(
                        "http://" & Main.MainInstance.battlenet_region & ".battle.net/api/wow/character/" & Main.MainInstance.realmname & "/" &
                        Main.MainInstance.char_name & "?fields=talents")
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
                            Main.MainInstance.majorglyph1 = glyphid
                            Glyphs.erheb1.Visible = True
                            getimage(glyphid, Glyphs.erheb1pic)
                        Else
                            Main.MainInstance.majorglyph1 = glyphid
                            Main.MainInstance.glyphlist.Add(charnumber & "majorglyph1=" & glyphid)
                        End If

                    ElseIf startcounter = 1 Then
                        If xoverview = True Then
                            Glyphs.erheb2.Text = glyphname
                            Main.MainInstance.majorglyph2 = glyphid
                            Glyphs.erheb2.Visible = True
                            getimage(glyphid, Glyphs.erheb2pic)
                        Else
                            Main.MainInstance.majorglyph2 = glyphid
                            Main.MainInstance.glyphlist.Add(charnumber & "majorglyph2=" & glyphid)
                        End If

                    ElseIf startcounter = 2 Then
                        If xoverview = True Then
                            Glyphs.erheb3.Text = glyphname
                            Main.MainInstance.majorglyph3 = glyphid
                            Glyphs.erheb3.Visible = True
                            getimage(glyphid, Glyphs.erheb3pic)
                        Else
                            Main.MainInstance.majorglyph3 = glyphid
                            Main.MainInstance.glyphlist.Add(charnumber & "majorglyph3=" & glyphid)
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
                            Main.MainInstance.minorglyph1 = glyphid
                            Glyphs.gering1.Visible = True
                            getimage(glyphid, Glyphs.gering1pic)
                        Else
                            Main.MainInstance.minorglyph1 = glyphid
                            Main.MainInstance.glyphlist.Add(charnumber & "minorglyph1=" & glyphid)
                        End If

                    ElseIf startcounter = 1 Then
                        If xoverview = True Then
                            Glyphs.gering2.Text = glyphname
                            Main.MainInstance.minorglyph2 = glyphid
                            Glyphs.gering2.Visible = True
                            getimage(glyphid, Glyphs.gering2pic)
                        Else
                            Main.MainInstance.minorglyph2 = glyphid
                            Main.MainInstance.glyphlist.Add(charnumber & "minorglyph2=" & glyphid)
                        End If

                    ElseIf startcounter = 2 Then
                        If xoverview = True Then
                            Glyphs.gering3.Text = glyphname
                            Main.MainInstance.minorglyph3 = glyphid
                            Glyphs.gering3.Visible = True
                            getimage(glyphid, Glyphs.gering3pic)
                        Else
                            Main.MainInstance.minorglyph3 = glyphid
                            Main.MainInstance.glyphlist.Add(charnumber & "minorglyph3=" & glyphid)
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
                        "http://" & Main.MainInstance.battlenet_region & ".battle.net/api/wow/character/" & Main.MainInstance.realmname & "/" &
                        Main.MainInstance.char_name & "?fields=talents")
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
                            Main.MainInstance.secmajorglyph1 = glyphid
                            Glyphs.secerheb1.Visible = True
                            getimage(glyphid, Glyphs.secerheb1pic)
                        Else
                            Main.MainInstance.secmajorglyph1 = glyphid
                            Main.MainInstance.glyphlist.Add(charnumber & "secmajorglyph1=" & glyphid)
                        End If

                    ElseIf startcounter = 1 Then
                        If xoverview = True Then
                            Glyphs.secerheb2.Text = glyphname
                            Main.MainInstance.secmajorglyph2 = glyphid
                            Glyphs.secerheb2.Visible = True
                            getimage(glyphid, Glyphs.secerheb2pic)
                        Else
                            Main.MainInstance.secmajorglyph2 = glyphid
                            Main.MainInstance.glyphlist.Add(charnumber & "secmajorglyph2=" & glyphid)
                        End If

                    ElseIf startcounter = 2 Then
                        If xoverview = True Then
                            Glyphs.secerheb3.Text = glyphname
                            Main.MainInstance.secmajorglyph3 = glyphid
                            Glyphs.secerheb3.Visible = True
                            getimage(glyphid, Glyphs.secerheb3pic)
                        Else
                            Main.MainInstance.secmajorglyph3 = glyphid
                            Main.MainInstance.glyphlist.Add(charnumber & "secmajorglyph3=" & glyphid)
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
                            Main.MainInstance.secminorglyph1 = glyphid
                            Glyphs.secgering1.Visible = True
                            getimage(glyphid, Glyphs.secgering1pic)
                        Else
                            Main.MainInstance.secminorglyph1 = glyphid
                            Main.MainInstance.glyphlist.Add(charnumber & "secminorglyph1=" & glyphid)
                        End If

                    ElseIf startcounter = 1 Then
                        If xoverview = True Then
                            Glyphs.secgering2.Text = glyphname
                            Main.MainInstance.secminorglyph2 = glyphid
                            Glyphs.secgering2.Visible = True
                            getimage(glyphid, Glyphs.secgering2pic)
                        Else
                            Main.MainInstance.secminorglyph2 = glyphid
                            Main.MainInstance.glyphlist.Add(charnumber & "secminorglyph2=" & glyphid)
                        End If

                    ElseIf startcounter = 2 Then
                        If xoverview = True Then
                            Glyphs.secgering3.Text = glyphname
                            Main.MainInstance.secminorglyph3 = glyphid
                            Glyphs.secgering3.Visible = True
                            getimage(glyphid, Glyphs.secgering3pic)
                        Else
                            Main.MainInstance.secminorglyph3 = glyphid
                            Main.MainInstance.glyphlist.Add(charnumber & "secminorglyph3=" & glyphid)
                        End If
                    End If
                    startcounter += 1


                Loop Until startcounter = excounter

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub saveglyphs()
        Main.MainInstance.textprimeglyph1 = Glyphs.prim1.Text
        Main.MainInstance.textprimeglyph2 = Glyphs.prim2.Text
        Main.MainInstance.textprimeglyph3 = Glyphs.prim3.Text
        Main.MainInstance.textmajorglyph1 = Glyphs.erheb1.Text
        Main.MainInstance.textmajorglyph2 = Glyphs.erheb2.Text
        Main.MainInstance.textmajorglyph3 = Glyphs.erheb3.Text
        Main.MainInstance.textminorglyph1 = Glyphs.gering1.Text
        Main.MainInstance.textminorglyph2 = Glyphs.gering2.Text
        Main.MainInstance.textminorglyph3 = Glyphs.gering3.Text

        Main.MainInstance.glyphpic1 = Glyphs.prim1pic.Image
        Main.MainInstance.glyphpic2 = Glyphs.prim2pic.Image
        Main.MainInstance.glyphpic3 = Glyphs.prim3pic.Image
        Main.MainInstance.glyphpic4 = Glyphs.erheb1pic.Image
        Main.MainInstance.glyphpic5 = Glyphs.erheb2pic.Image
        Main.MainInstance.glyphpic6 = Glyphs.erheb3pic.Image
        Main.MainInstance.glyphpic7 = Glyphs.gering1pic.Image
        Main.MainInstance.glyphpic8 = Glyphs.gering2pic.Image
        Main.MainInstance.glyphpic9 = Glyphs.gering3pic.Image

        Main.MainInstance.sectextprimeglyph1 = Glyphs.secprim1.Text
        Main.MainInstance.sectextprimeglyph2 = Glyphs.secprim2.Text
        Main.MainInstance.sectextprimeglyph3 = Glyphs.secprim3.Text
        Main.MainInstance.sectextmajorglyph1 = Glyphs.secerheb1.Text
        Main.MainInstance.sectextmajorglyph2 = Glyphs.secerheb2.Text
        Main.MainInstance.sectextmajorglyph3 = Glyphs.secerheb3.Text
        Main.MainInstance.sectextminorglyph1 = Glyphs.secgering1.Text
        Main.MainInstance.sectextminorglyph2 = Glyphs.secgering2.Text
        Main.MainInstance.sectextminorglyph3 = Glyphs.secgering3.Text

        Main.MainInstance.secglyphpic1 = Glyphs.secprim1pic.Image
        Main.MainInstance.secglyphpic2 = Glyphs.secprim2pic.Image
        Main.MainInstance.secglyphpic3 = Glyphs.secprim3pic.Image
        Main.MainInstance.secglyphpic4 = Glyphs.secerheb1pic.Image
        Main.MainInstance.secglyphpic5 = Glyphs.secerheb2pic.Image
        Main.MainInstance.secglyphpic6 = Glyphs.secerheb3pic.Image
        Main.MainInstance.secglyphpic7 = Glyphs.secgering1pic.Image
        Main.MainInstance.secglyphpic8 = Glyphs.secgering2pic.Image
        Main.MainInstance.secglyphpic9 = Glyphs.secgering3pic.Image
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

            Dim quellcode88 As String = Main.MainInstance.quelltext
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
                            With Main.MainInstance.kopfvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.kopfvzid = runfunction.getvzeffectid(.Text)
                            End With


                        Case 2
                            With Main.MainInstance.halsvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.halsvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 3
                            With Main.MainInstance.schultervz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.schultervzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 4
                            With Main.MainInstance.rueckenvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.rueckenvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 5
                            With Main.MainInstance.brustvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.brustvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 6

                        Case 7

                        Case 8
                            With Main.MainInstance.handgelenkevz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.handgelenkevzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 9
                            With Main.MainInstance.hauptvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.hauptvzid = runfunction.getvzeffectid(.Text)
                                End With
                            With Main.MainInstance.hauptvzlabel2
                                .Visible = True
                                If xoverview = True Then .Text = Main.MainInstance.hauptvz.Text
                            End With

                        Case 10
                            With Main.MainInstance.offvz
                                .Visible = False
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.offvzid = runfunction.getvzeffectid(.Text)
                            End With
                            With Main.MainInstance.offvzlabel2
                                .Visible = True
                                If xoverview = True Then .Text = Main.MainInstance.offvz.Text
                            End With

                        Case 11
                            With Main.MainInstance.distanzvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.distanzvzid = runfunction.getvzeffectid(.Text)
                            End With
                            With Main.MainInstance.distanzvzlabel2
                                .Visible = True
                                .Text = Main.MainInstance.distanzvz.Text
                            End With

                        Case 12
                            With Main.MainInstance.haendevz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.haendevzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 13
                            With Main.MainInstance.guertelvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.guertelvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 14
                            With Main.MainInstance.beinevz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.beinevzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 15
                            With Main.MainInstance.stiefelvz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.stiefelvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 16
                            With Main.MainInstance.ring1vz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.ring1vzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 17
                            With Main.MainInstance.ring2vz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.ring2vzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 18
                            With Main.MainInstance.schmuck1vz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.schmuck1vzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 19
                            With Main.MainInstance.schmuck2vz
                                .Visible = True
                                If xoverview = True Then .Text = getvzeffectname(getspellidfromitem(vzid).ToString)
                                Main.MainInstance.schmuck2vzid = runfunction.getvzeffectid(.Text)
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
                            With Main.MainInstance.kopfvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.kopfvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 2
                            With Main.MainInstance.halsvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.halsvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 3
                            With Main.MainInstance.schultervz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.schultervzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 4
                            With Main.MainInstance.rueckenvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.rueckenvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 5
                            With Main.MainInstance.brustvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.brustvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 6

                        Case 7

                        Case 8
                            With Main.MainInstance.handgelenkevz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.handgelenkevzid = runfunction.getvzeffectid(.Text)
                            End With

                            '[ Section changed 03/09/12 -  Reason: Label not displayed!
                        Case 9
                            With Main.MainInstance.hauptvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.hauptvzid = runfunction.getvzeffectid(.Text)
                            End With
                            With Main.MainInstance.hauptvzlabel2
                                .Visible = True
                                If xoverview = True Then .Text = Main.MainInstance.hauptvz.Text
                            End With

                        Case 10
                            With Main.MainInstance.offvz
                                .Visible = False
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.offvzid = runfunction.getvzeffectid(.Text)
                            End With
                            With Main.MainInstance.offvzlabel2
                                .Visible = True
                                If xoverview = True Then .Text = Main.MainInstance.offvz.Text
                            End With

                        Case 11
                            With Main.MainInstance.distanzvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.distanzvzid = runfunction.getvzeffectid(.Text)
                            End With
                            With Main.MainInstance.distanzvzlabel2
                                .Visible = True
                                .Text = Main.MainInstance.distanzvz.Text
                            End With

                            'End Section ]
                        Case 12
                            With Main.MainInstance.haendevz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.haendevzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 13
                            With Main.MainInstance.guertelvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.guertelvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 14
                            With Main.MainInstance.beinevz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.beinevzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 15
                            With Main.MainInstance.stiefelvz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.stiefelvzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 16
                            With Main.MainInstance.ring1vz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.ring1vzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 17
                            With Main.MainInstance.ring2vz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.ring2vzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 18
                            With Main.MainInstance.schmuck1vz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.schmuck1vzid = runfunction.getvzeffectid(.Text)
                            End With

                        Case 19
                            With Main.MainInstance.schmuck2vz
                                .Visible = True
                                If xoverview = True Then .Text = getspellnamefromid(vzid)
                                Main.MainInstance.schmuck2vzid = runfunction.getvzeffectid(.Text)
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

            Dim quellcode88 As String = Main.MainInstance.quelltext
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
                        With Main.MainInstance.kopfsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.kopfsocket1id = runfunction.getgemeffectid(socket1)
                    Case 2
                        With Main.MainInstance.halssocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.halssocket1id = runfunction.getgemeffectid(socket1)
                    Case 3
                        With Main.MainInstance.schultersocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.schultersocket1id = runfunction.getgemeffectid(socket1)
                    Case 4
                        With Main.MainInstance.rueckensocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.rueckensocket1id = runfunction.getgemeffectid(socket1)
                    Case 5
                        With Main.MainInstance.brustsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.brustsocket1id = runfunction.getgemeffectid(socket1)
                    Case 6

                    Case 7

                    Case 8
                        With Main.MainInstance.Handgelenkesocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.handgelenkesocket1id = runfunction.getgemeffectid(socket1)
                    Case 9
                        With Main.MainInstance.Hauptsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.hauptsocket1id = runfunction.getgemeffectid(socket1)
                    Case 10
                        With Main.MainInstance.Offsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.offsocket1id = runfunction.getgemeffectid(socket1)
                    Case 11
                        With Main.MainInstance.Distanzsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.distanzsocket1id = runfunction.getgemeffectid(socket1)
                    Case 12
                        With Main.MainInstance.haendesocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.haendesocket1id = runfunction.getgemeffectid(socket1)
                    Case 13
                        With Main.MainInstance.guertelsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.guertelsocket1id = runfunction.getgemeffectid(socket1)
                    Case 14
                        With Main.MainInstance.beinesocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.beinesocket1id = runfunction.getgemeffectid(socket1)
                    Case 15
                        With Main.MainInstance.stiefelsocket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.stiefelsocket1id = runfunction.getgemeffectid(socket1)
                    Case 16
                        With Main.MainInstance.Ring1socket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.ring1socket1id = runfunction.getgemeffectid(socket1)
                    Case 17
                        With Main.MainInstance.ring2socket1
                            .Visible = True
                            If xoverview = True Then .Text = getsocketeffectname(socket1)
                        End With
                        Main.MainInstance.ring2socket1id = runfunction.getgemeffectid(socket1)
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
                            With Main.MainInstance.kopfsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.kopfsocket2id = runfunction.getgemeffectid(socket2)
                        Case 2
                            With Main.MainInstance.halssocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.halssocket2id = runfunction.getgemeffectid(socket2)
                        Case 3
                            With Main.MainInstance.schultersocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.schultersocket2id = runfunction.getgemeffectid(socket2)
                        Case 4
                            With Main.MainInstance.rueckensocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.rueckensocket2id = runfunction.getgemeffectid(socket2)
                        Case 5
                            With Main.MainInstance.brustsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.brustsocket2id = runfunction.getgemeffectid(socket2)
                        Case 6

                        Case 7

                        Case 8
                            With Main.MainInstance.handgelenkesocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.handgelenkesocket2id = runfunction.getgemeffectid(socket2)
                        Case 9
                            With Main.MainInstance.Hauptsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.hauptsocket2id = runfunction.getgemeffectid(socket2)
                        Case 10
                            With Main.MainInstance.Offsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.offsocket2id = runfunction.getgemeffectid(socket2)
                        Case 11
                            With Main.MainInstance.Distanzsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.distanzsocket2id = runfunction.getgemeffectid(socket2)
                        Case 12
                            With Main.MainInstance.haendesocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.haendesocket2id = runfunction.getgemeffectid(socket2)
                        Case 13
                            With Main.MainInstance.guertelsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.guertelsocket2id = runfunction.getgemeffectid(socket2)
                        Case 14
                            With Main.MainInstance.beinesocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.beinesocket2id = runfunction.getgemeffectid(socket2)
                        Case 15
                            With Main.MainInstance.stiefelsocket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.stiefelsocket2id = runfunction.getgemeffectid(socket2)
                        Case 16
                            With Main.MainInstance.ring1socket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.ring1socket2id = runfunction.getgemeffectid(socket2)
                        Case 17
                            With Main.MainInstance.ring2socket2
                                .Visible = True
                                If xoverview = True Then .Text = getsocketeffectname(socket2)
                            End With
                            Main.MainInstance.ring2socket2id = runfunction.getgemeffectid(socket2)
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
                                With Main.MainInstance.kopfsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.kopfsocket3id = runfunction.getgemeffectid(socket3)
                            Case 2
                                With Main.MainInstance.halssocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.halssocket3id = runfunction.getgemeffectid(socket3)
                            Case 3
                                With Main.MainInstance.schultersocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.schultersocket3id = runfunction.getgemeffectid(socket3)
                            Case 4
                                With Main.MainInstance.rueckensocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.rueckensocket3id = runfunction.getgemeffectid(socket3)
                            Case 5
                                With Main.MainInstance.brustsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.brustsocket3id = runfunction.getgemeffectid(socket3)
                            Case 6

                            Case 7

                            Case 8
                                With Main.MainInstance.Handgelenkesocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.handgelenkesocket3id = runfunction.getgemeffectid(socket3)
                            Case 9
                                With Main.MainInstance.hauptsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.hauptsocket3id = runfunction.getgemeffectid(socket3)
                            Case 10
                                With Main.MainInstance.offsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.offsocket3id = runfunction.getgemeffectid(socket3)
                            Case 11
                                With Main.MainInstance.distanzsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.distanzsocket3id = runfunction.getgemeffectid(socket3)
                            Case 12
                                With Main.MainInstance.haendesocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.haendesocket3id = runfunction.getgemeffectid(socket3)
                            Case 13
                                With Main.MainInstance.guertelsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.guertelsocket3id = runfunction.getgemeffectid(socket3)
                            Case 14
                                With Main.MainInstance.beinesocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.beinesocket3id = runfunction.getgemeffectid(socket3)
                            Case 15
                                With Main.MainInstance.stiefelsocket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.stiefelsocket3id = runfunction.getgemeffectid(socket3)
                            Case 16
                                With Main.MainInstance.ring1socket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.ring1socket3id = runfunction.getgemeffectid(socket3)
                            Case 17
                                With Main.MainInstance.ring2socket3
                                    .Visible = True
                                    If xoverview = True Then .Text = getsocketeffectname(socket3)
                                End With
                                Main.MainInstance.ring2socket3id = runfunction.getgemeffectid(socket3)
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

            Dim quellcode88 As String = Main.MainInstance.quelltext
            Dim anfang88 As String = starting
            Dim ende88 As String = "<span class=""name color-q"
            Dim quellcodeSplit88 As String
            quellcodeSplit88 = Split(quellcode88, anfang88, 5)(1)
            quellcodeSplit88 = Split(quellcodeSplit88, ende88, 6)(0)
            If quellcodeSplit88.Contains("<a href=""javascript:;"" class=""empty""") Then
                Dim quellcodeSplity88 As String = "-"
                Select Case slot
                    Case 1
                        Main.MainInstance.Kopf.Text = quellcodeSplity88

                    Case 2
                        Main.MainInstance.Hals.Text = quellcodeSplity88

                    Case 3
                        Main.MainInstance.Schulter.Text = quellcodeSplity88

                    Case 4
                        Main.MainInstance.Ruecken.Text = quellcodeSplity88

                    Case 5
                        Main.MainInstance.Brust.Text = quellcodeSplity88

                    Case 6
                        Main.MainInstance.Hemd.Text = quellcodeSplity88

                    Case 7
                        Main.MainInstance.Wappenrock.Text = quellcodeSplity88

                    Case 8
                        Main.MainInstance.Handgelenke.Text = quellcodeSplity88

                    Case 9
                        Main.MainInstance.Haupt.Text = quellcodeSplity88

                    Case 10
                        Main.MainInstance.Off.Text = quellcodeSplity88

                    Case 11
                        Main.MainInstance.Distanz.Text = quellcodeSplity88

                    Case 12
                        Main.MainInstance.Haende.Text = quellcodeSplity88

                    Case 13
                        Main.MainInstance.Guertel.Text = quellcodeSplity88

                    Case 14
                        Main.MainInstance.Beine.Text = quellcodeSplity88

                    Case 15
                        Main.MainInstance.Stiefel.Text = quellcodeSplity88

                    Case 16
                        Main.MainInstance.Ring1.Text = quellcodeSplity88

                    Case 17
                        Main.MainInstance.Ring2.Text = quellcodeSplity88

                    Case 18
                        Main.MainInstance.Schmuck1.Text = quellcodeSplity88

                    Case 19
                        Main.MainInstance.Schmuck2.Text = quellcodeSplity88

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
                        Main.MainInstance.Kopf.Text = xquellcodesplity89
                        Main.MainInstance.Kopf.Visible = True
                        Main.MainInstance.kopfid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.kopfpic)
                    Case 2
                        Main.MainInstance.Hals.Text = xquellcodesplity89
                        Main.MainInstance.Hals.Visible = True
                        Main.MainInstance.halsid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Halspic)
                    Case 3
                        Main.MainInstance.Schulter.Text = xquellcodesplity89
                        Main.MainInstance.Schulter.Visible = True
                        Main.MainInstance.schulterid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Schulterpic)
                    Case 4
                        Main.MainInstance.Ruecken.Text = xquellcodesplity89
                        Main.MainInstance.Ruecken.Visible = True
                        Main.MainInstance.rueckenid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Rueckenpic)
                    Case 5
                        Main.MainInstance.Brust.Text = xquellcodesplity89
                        Main.MainInstance.Brust.Visible = True
                        Main.MainInstance.brustid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Brustpic)
                    Case 6
                        Main.MainInstance.Hemd.Text = xquellcodesplity89
                        Main.MainInstance.Hemd.Visible = True
                        Main.MainInstance.hemdid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Hemdpic)
                    Case 7
                        Main.MainInstance.Wappenrock.Text = xquellcodesplity89
                        Main.MainInstance.Wappenrock.Visible = True
                        Main.MainInstance.wappenrockid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Wappenrockpic)
                    Case 8
                        Main.MainInstance.Handgelenke.Text = xquellcodesplity89
                        Main.MainInstance.Handgelenke.Visible = True
                        Main.MainInstance.handgelenkeid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Handgelenkepic)
                    Case 9
                        Main.MainInstance.Haupt.Text = xquellcodesplity89
                        Main.MainInstance.Haupt.Visible = True
                        Main.MainInstance.hauptid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Hauptpic)
                        getweapontype(quellcodeSplity88)
                    Case 10
                        Main.MainInstance.Off.Text = xquellcodesplity89
                        Main.MainInstance.Off.Visible = True
                        Main.MainInstance.offid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Offpic)
                        getweapontype(quellcodeSplity88)
                    Case 11
                        Main.MainInstance.Distanz.Text = xquellcodesplity89
                        Main.MainInstance.Distanz.Visible = True
                        Main.MainInstance.distanzid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Distanzpic)
                        getweapontype(quellcodeSplity88)
                    Case 12
                        Main.MainInstance.Haende.Text = xquellcodesplity89
                        Main.MainInstance.Haende.Visible = True
                        Main.MainInstance.haendeid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Haendepic)
                    Case 13
                        Main.MainInstance.Guertel.Text = xquellcodesplity89
                        Main.MainInstance.Guertel.Visible = True
                        Main.MainInstance.guertelid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Guertelpic)
                    Case 14
                        Main.MainInstance.Beine.Text = xquellcodesplity89
                        Main.MainInstance.Beine.Visible = True
                        Main.MainInstance.beineid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Beinepic)
                    Case 15
                        Main.MainInstance.Stiefel.Text = xquellcodesplity89
                        Main.MainInstance.Stiefel.Visible = True
                        Main.MainInstance.stiefelid = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Stiefelpic)
                    Case 16
                        Main.MainInstance.Ring1.Text = xquellcodesplity89
                        Main.MainInstance.Ring1.Visible = True
                        Main.MainInstance.ring1id = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Ring1pic)
                    Case 17
                        Main.MainInstance.Ring2.Text = xquellcodesplity89
                        Main.MainInstance.Ring2.Visible = True
                        Main.MainInstance.ring2id = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Ring2pic)
                    Case 18
                        Main.MainInstance.Schmuck1.Text = xquellcodesplity89
                        Main.MainInstance.Schmuck1.Visible = True
                        Main.MainInstance.schmuck1id = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Schmuck1pic)
                    Case 19
                        Main.MainInstance.Schmuck2.Text = xquellcodesplity89
                        Main.MainInstance.Schmuck2.Visible = True
                        Main.MainInstance.schmuck2id = quellcodeSplity88
                        If xoverview = True Then LoadImageFromUrl(quellcodeSplityx88, Main.MainInstance.Schmuck2pic)
                End Select
            End If


        Catch ex As Exception
            Process_Status.processreport.appendText(
                Now.TimeOfDay.ToString & "// Error while setting up Itemslot: " & slot.ToString & vbNewLine)
            My.Application.DoEvents()
            Select Case slot
                Case 1
                    Main.MainInstance.Kopf.Text = "Error"
                Case 2
                    Main.MainInstance.Hals.Text = "Error"
                Case 3
                    Main.MainInstance.Schulter.Text = "Error"
                Case 4
                    Main.MainInstance.Ruecken.Text = "Error"
                Case 5
                    Main.MainInstance.Brust.Text = "Error"
                Case 6
                    Main.MainInstance.Hemd.Text = "Error"
                Case 7
                    Main.MainInstance.Wappenrock.Text = "Error"
                Case 8
                    Main.MainInstance.Handgelenke.Text = "Error"
                Case 9
                    Main.MainInstance.Haupt.Text = "Error"
                Case 10
                    Main.MainInstance.Off.Text = "Error"
                Case 11
                    Main.MainInstance.Distanz.Text = "Error"
                Case 12
                    Main.MainInstance.Haende.Text = "Error"
                Case 13
                    Main.MainInstance.Guertel.Text = "Error"
                Case 14
                    Main.MainInstance.Beine.Text = "Error"
                Case 15
                    Main.MainInstance.Stiefel.Text = "Error"
                Case 16
                    Main.MainInstance.Ring1.Text = "Error"
                Case 17
                    Main.MainInstance.Ring2.Text = "Error"
                Case 18
                    Main.MainInstance.Schmuck1.Text = "Error"
                Case 19
                    Main.MainInstance.Schmuck2.Text = "Error"
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
