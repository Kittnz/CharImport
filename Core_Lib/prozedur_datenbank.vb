'****************************************************************************************
'****************************************************************************************
'***************************** CharImpor - prozedur_datenbank ***************************
'****************************************************************************************
'..................Status
'...................Code:       0%
'...................Design:     0%
'...................Functions:  0%
'****************************************************************************************
'****************************************************************************************
'..................Last modified: 24.02.12
'****************************************************************************************
'****************************************************************************************
'..................Comments:

Imports MySql.Data.MySqlClient
Imports System.Net
Imports System.Text

Public Class prozedur_datenbank
    Dim ServerString As String = Main.ServerString
    Dim characterguid As Integer


    'Atribute




    Public Sub prozess()
        '****************************************************************************************
        '****************************************************************************************
        'Get Character Guid

        characterguid = getcharguid(Main.datacharname)
        If characterguid = -1 Then
            Starter.Show()
            Main.Close()
            Exit Sub
        End If

        '****************************************************************************************
        '****************************************************************************************
        'Get Main Character atributes

        'Character Race
        Try
            Main.char_race = Int(Val(runcommand("SELECT race FROM characters WHERE guid='" & characterguid.ToString & "'", "race")))
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
        Try
            Main.char_class = Int(Val(runcommand("SELECT class FROM characters WHERE guid='" & characterguid.ToString & "'", "class")))
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
        Try
            Main.char_gender = Int(Val(runcommand("SELECT gender FROM characters WHERE guid='" & characterguid.ToString & "'", "gender")))
        Catch ex As Exception

        End Try

        'Character level
        Try
            Main.char_level = Int(Val(runcommand("SELECT level FROM characters WHERE guid='" & characterguid.ToString & "'", "level")))
            Main.level.Text = Main.char_level
        Catch ex As Exception

        End Try


        'GET ITEMS
        getitems()

        'GET GLYPHS
        getglyphs()
        getsecglyphs()
        handleenchantments()
        Main.xstatus.Text = "Character loaded!"
        My.Application.DoEvents()

        saveglyphs()
        Main.Panel23.Location = New System.Drawing.Point(5000, 5000)
        Application.DoEvents()
    End Sub
    Public Sub getglyphs()
        Dim prevglyphid As Integer
        Dim glyphid As Integer
        Dim glyphname As String = ""
        Dim glyphpic As Image = My.Resources.empty
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph7 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'", "glyph7")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.prim1.Text = glyphname
                Main.primeglyph1 = glyphid
                Glyphs.prim1.Visible = True
                getimage(glyphid, Glyphs.prim1pic)
            End If
        Catch ex As Exception
            Glyphs.prim1pic.Image = My.Resources.empty
        End Try


        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph8 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'", "glyph8")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.prim2.Text = glyphname
                Main.primeglyph2 = glyphid
                Glyphs.prim2.Visible = True
                getimage(glyphid, Glyphs.prim2pic)
            End If
        Catch ex As Exception
            Glyphs.prim2pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph9 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'", "glyph9")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.prim3.Text = glyphname
                Main.primeglyph3 = glyphid
                Glyphs.prim3.Visible = True
                getimage(glyphid, Glyphs.prim3pic)
            End If
        Catch ex As Exception
            Glyphs.prim3pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph1 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'", "glyph1")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.erheb1.Text = glyphname
                Main.majorglyph1 = glyphid
                Glyphs.erheb1.Visible = True
                getimage(glyphid, Glyphs.erheb1pic)
            End If
        Catch ex As Exception
            Glyphs.erheb1pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph4 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'", "glyph4")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.erheb2.Text = glyphname
                Main.majorglyph2 = glyphid
                Glyphs.erheb2.Visible = True
                getimage(glyphid, Glyphs.erheb2pic)
            End If
        Catch ex As Exception
            Glyphs.erheb2pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph6 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'", "glyph6")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.erheb3.Text = glyphname
                Main.majorglyph3 = glyphid
                Glyphs.erheb3.Visible = True
                getimage(glyphid, Glyphs.erheb3pic)
            End If
        Catch ex As Exception
            Glyphs.erheb3pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph2 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'", "glyph2")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.gering1.Text = glyphname
                Main.minorglyph1 = glyphid
                Glyphs.gering1.Visible = True
                getimage(glyphid, Glyphs.gering1pic)
            End If
        Catch ex As Exception
            Glyphs.gering1pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph3 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'", "glyph3")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.gering2.Text = glyphname
                Main.minorglyph2 = glyphid
                Glyphs.gering2.Visible = True
                getimage(glyphid, Glyphs.gering2pic)
            End If
        Catch ex As Exception
            Glyphs.gering2pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph5 from character_glyphs WHERE guid='" & characterguid & "' AND spec='0'", "glyph5")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.gering3.Text = glyphname
                Main.minorglyph3 = glyphid
                Glyphs.gering3.Visible = True
                getimage(glyphid, Glyphs.gering3pic)
            End If
        Catch ex As Exception
            Glyphs.gering3pic.Image = My.Resources.empty
        End Try

    End Sub
    Public Sub getsecglyphs()
        Dim prevglyphid As Integer
        Dim glyphid As Integer
        Dim glyphname As String = ""
        Dim glyphpic As Image = My.Resources.empty
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph7 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'", "glyph7")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.secprim1.Text = glyphname
                Main.secprimeglyph1 = glyphid
                Glyphs.secprim1.Visible = True
                getimage(glyphid, Glyphs.secprim1pic)
            End If
        Catch ex As Exception
            Glyphs.secprim1pic.Image = My.Resources.empty
        End Try


        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph8 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'", "glyph8")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.secprim2.Text = glyphname
                Main.secprimeglyph2 = glyphid
                Glyphs.secprim2.Visible = True
                getimage(glyphid, Glyphs.secprim2pic)
            End If
        Catch ex As Exception
            Glyphs.secprim2pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph9 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'", "glyph9")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.secprim3.Text = glyphname
                Main.secprimeglyph3 = glyphid
                Glyphs.secprim3.Visible = True
                getimage(glyphid, Glyphs.secprim3pic)
            End If
        Catch ex As Exception
            Glyphs.secprim3pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph1 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'", "glyph1")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.secerheb1.Text = glyphname
                Main.secmajorglyph1 = glyphid
                Glyphs.secerheb1.Visible = True
                getimage(glyphid, Glyphs.secerheb1pic)
            End If
        Catch ex As Exception
            Glyphs.secerheb1pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph4 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'", "glyph4")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.secerheb2.Text = glyphname
                Main.secmajorglyph2 = glyphid
                Glyphs.secerheb2.Visible = True
                getimage(glyphid, Glyphs.secerheb2pic)
            End If
        Catch ex As Exception
            Glyphs.secerheb2pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph6 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'", "glyph6")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.secerheb3.Text = glyphname
                Main.secmajorglyph3 = glyphid
                Glyphs.secerheb3.Visible = True
                getimage(glyphid, Glyphs.secerheb3pic)
            End If
        Catch ex As Exception
            Glyphs.secerheb3pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph2 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'", "glyph2")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.secgering1.Text = glyphname
                Main.secminorglyph1 = glyphid
                Glyphs.secgering1.Visible = True
                getimage(glyphid, Glyphs.secgering1pic)
            End If
        Catch ex As Exception
            Glyphs.secgering1pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph3 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'", "glyph3")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.secgering2.Text = glyphname
                Main.secminorglyph2 = glyphid
                Glyphs.secgering2.Visible = True
                getimage(glyphid, Glyphs.secgering2pic)
            End If
        Catch ex As Exception
            Glyphs.secgering2pic.Image = My.Resources.empty
        End Try
        Try
            prevglyphid = Int(Val(runcommand("SELECT glyph5 from character_glyphs WHERE guid='" & characterguid & "' AND spec='1'", "glyph5")))
            If prevglyphid > 1 Then
                glyphid = getglyphid(prevglyphid.ToString)
                glyphname = getnamefromid(glyphid.ToString)
                Glyphs.secgering3.Text = glyphname
                Main.secminorglyph3 = glyphid
                Glyphs.secgering3.Visible = True
                getimage(glyphid, Glyphs.secgering3pic)
            End If
        Catch ex As Exception
            Glyphs.secgering3pic.Image = My.Resources.empty
        End Try

    End Sub
    Private Function getglyphid(ByVal glyphid As String)
        Try

            Dim zclienyx88 As New WebClient
            Dim zquellcodeyx88 As String = zclienyx88.DownloadString("http://www.trinitycore.info/GlyphProperties.dbc_tc2")
            Dim zanfangyx88 As String = "<tr><td>" & glyphid & "</td><td>"
            Dim zendeyx88 As String = "/td><td>"
            Dim zquellcodeSplityx88 As String
            zquellcodeSplityx88 = Split(zquellcodeyx88, zanfangyx88, 6)(0)
            zquellcodeSplityx88 = Split(zquellcodeSplityx88, zendeyx88, 6)(0)

            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://www.wowhead.com/spell=" & zquellcodeSplityx88)
            Dim anfangyx88 As String = ",""id"":"
            Dim endeyx88 As String = ",""level"""
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)


            Return quellcodeSplityx88
        Catch ex As Exception
            Return "0"
        End Try
    End Function
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
        'Get Instance
        Dim xslot As Integer = 0
        Dim xentryid As Integer
        Dim itemname As String = ""
        Dim realxentryid As Integer
        Do
            Try
                xentryid = Int(Val(runcommand("SELECT item FROM character_inventory WHERE guid = '" & characterguid & "' AND slot = '" & xslot & "'", "item")))
                realxentryid = Int(Val(runcommand("SELECT itemEntry FROM item_instance WHERE guid = '" & xentryid & "'", "itemEntry")))
                itemname = getnamefromid(realxentryid)
            Catch ex As Exception

            End Try
            Select Case xslot
                Case 0
                    Main.Kopf.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Kopf.Visible = True
                    Main.kopfid = realxentryid
                    Main.kopfname = itemname
                    getimage(realxentryid, Main.kopfpic)
                    getitemstats(xentryid, Main.kopfench)
                Case 1
                    Main.Hals.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Hals.Visible = True
                    Main.halsid = realxentryid
                    Main.halsname = itemname
                    getimage(realxentryid, Main.Halspic)
                    getitemstats(xentryid, Main.halsench)
                Case 2
                    Main.Schulter.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Schulter.Visible = True
                    Main.schulterid = realxentryid
                    Main.schultername = itemname
                    getimage(realxentryid, Main.Schulterpic)
                    getitemstats(xentryid, Main.schulterench)
                Case 3
                    Main.Hemd.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Hemd.Visible = True
                    Main.hemdid = realxentryid
                    Main.hemdname = itemname
                    getimage(realxentryid, Main.Hemdpic)
                    getitemstats(xentryid, Main.hemdench)
                Case 4
                    Main.Brust.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Brust.Visible = True
                    Main.brustid = realxentryid
                    Main.brustname = itemname
                    getimage(realxentryid, Main.Brustpic)
                    getitemstats(xentryid, Main.brustench)
                Case 5
                    Main.Guertel.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Guertel.Visible = True
                    Main.guertelid = realxentryid
                    Main.guertelname = itemname
                    getimage(realxentryid, Main.Guertelpic)
                    getitemstats(xentryid, Main.guertelench)
                Case 6
                    Main.Beine.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Beine.Visible = True
                    Main.beineid = realxentryid
                    Main.beinename = itemname
                    getimage(realxentryid, Main.Beinepic)
                    getitemstats(xentryid, Main.beineench)
                Case 7
                    Main.Stiefel.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Stiefel.Visible = True
                    Main.stiefelid = realxentryid
                    Main.stiefelname = itemname
                    getimage(realxentryid, Main.Stiefelpic)
                    getitemstats(xentryid, Main.stiefelench)
                Case 8
                    Main.Handgelenke.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Handgelenke.Visible = True
                    Main.handgelenkeid = realxentryid
                    Main.handgelenkename = itemname
                    getimage(realxentryid, Main.Handgelenkepic)
                    getitemstats(xentryid, Main.handgelenkeench)
                Case 9
                    Main.Haende.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Haende.Visible = True
                    Main.haendeid = realxentryid
                    Main.haendename = itemname
                    getimage(realxentryid, Main.Haendepic)
                    getitemstats(xentryid, Main.haendeench)
                Case 10
                    Main.Ring1.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Ring1.Visible = True
                    Main.ring1id = realxentryid
                    Main.ring1name = itemname
                    getimage(realxentryid, Main.Ring1pic)
                    getitemstats(xentryid, Main.ring1ench)
                Case 11
                    Main.Ring2.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Ring2.Visible = True
                    Main.ring2id = realxentryid
                    Main.ring2name = itemname
                    getimage(realxentryid, Main.Ring2pic)
                    getitemstats(xentryid, Main.ring2ench)
                Case 12
                    Main.Schmuck1.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Schmuck1.Visible = True
                    Main.schmuck1id = realxentryid
                    Main.schmuck1name = itemname
                    getimage(realxentryid, Main.Schmuck1pic)
                    getitemstats(xentryid, Main.schmuck1ench)
                Case 13
                    Main.Schmuck2.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Schmuck2.Visible = True
                    Main.schmuck2id = realxentryid
                    Main.schmuck2name = itemname
                    getimage(realxentryid, Main.Schmuck2pic)
                    getitemstats(xentryid, Main.schmuck2ench)
                Case 14
                    Main.Ruecken.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Ruecken.Visible = True
                    Main.rueckenid = realxentryid
                    Main.rueckenname = itemname
                    getimage(realxentryid, Main.Rueckenpic)
                    getitemstats(xentryid, Main.rueckenench)
                Case 15
                    Main.Haupt.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Haupt.Visible = True
                    Main.hauptid = realxentryid
                    Main.hauptname = itemname
                    getimage(realxentryid, Main.Hauptpic)
                    getitemstats(xentryid, Main.hauptench)
                Case 16
                    Main.Off.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Off.Visible = True
                    Main.offid = realxentryid
                    Main.offname = itemname
                    getimage(realxentryid, Main.Offpic)
                    getitemstats(xentryid, Main.offench)
                Case 17
                    Main.Distanz.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Distanz.Visible = True
                    Main.distanzid = realxentryid
                    Main.distanzname = itemname
                    getimage(realxentryid, Main.Distanzpic)
                    getitemstats(xentryid, Main.distanzench)
                Case 18
                    Main.Wappenrock.Text = itemname
                    If Not itemname = "Platz leer" Then Main.Wappenrock.Visible = True
                    Main.wappenrockid = realxentryid
                    Main.wappenrockname = itemname
                    getimage(realxentryid, Main.Wappenrockpic)
                    getitemstats(xentryid, Main.wappenrockench)
                Case Else
            End Select
            xslot += 1
        Loop Until xslot = 19

    End Sub
    Public Sub handleenchantments()
        Main.kopfvz.Text = splitstringvz(Main.kopfench, 0)
        Main.kopfsocket1.Text = splitstringgem(Main.kopfench, 6)
        Main.kopfsocket2.Text = splitstringgem(Main.kopfench, 9)
        Main.kopfsocket3.Text = splitstringgem(Main.kopfench, 12)
        Main.kopfvz.Visible = True


        Main.halsvz.Text = splitstringvz(Main.halsench, 0)
        Main.halssocket1.Text = splitstringgem(Main.halsench, 6)
        Main.halssocket2.Text = splitstringgem(Main.halsench, 9)
        Main.halssocket3.Text = splitstringgem(Main.halsench, 12)
        Main.halsvz.Visible = True

        Main.schultervz.Text = splitstringvz(Main.schulterench, 0)
        Main.schultersocket1.Text = splitstringgem(Main.schulterench, 6)
        Main.schultersocket2.Text = splitstringgem(Main.schulterench, 9)
        Main.schultersocket3.Text = splitstringgem(Main.schulterench, 12)
        Main.schultervz.Visible = True

        Main.rueckenvz.Text = splitstringvz(Main.rueckenench, 0)
        Main.rueckensocket1.Text = splitstringgem(Main.rueckenench, 6)
        Main.rueckensocket2.Text = splitstringgem(Main.rueckenench, 9)
        Main.rueckensocket3.Text = splitstringgem(Main.rueckenench, 12)
        Main.rueckenvz.Visible = True

        Main.brustvz.Text = splitstringvz(Main.brustench, 0)
        Main.brustsocket1.Text = splitstringgem(Main.brustench, 6)
        Main.brustsocket2.Text = splitstringgem(Main.brustench, 9)
        Main.brustsocket3.Text = splitstringgem(Main.brustench, 12)
        Main.brustvz.Visible = True

        Main.handgelenkevz.Text = splitstringvz(Main.handgelenkeench, 0)
        Main.Handgelenkesocket1.Text = splitstringgem(Main.handgelenkeench, 6)
        Main.handgelenkesocket2.Text = splitstringgem(Main.handgelenkeench, 9)
        Main.Handgelenkesocket3.Text = splitstringgem(Main.handgelenkeench, 12)
        Main.handgelenkevz.Visible = True

        Main.hauptvz.Text = splitstringvz(Main.hauptench, 0)
        Main.Hauptsocket1.Text = splitstringgem(Main.hauptench, 6)
        Main.Hauptsocket2.Text = splitstringgem(Main.hauptench, 9)
        Main.hauptsocket3.Text = splitstringgem(Main.hauptench, 12)
        Main.hauptvz.Visible = True
        Main.hauptvzlabel2.Visible = True
        Main.hauptvzlabel2.Text = Main.hauptvz.Text

        Main.offvz.Text = splitstringvz(Main.offench, 0)
        Main.Offsocket1.Text = splitstringgem(Main.offench, 6)
        Main.Offsocket2.Text = splitstringgem(Main.offench, 9)
        Main.offsocket3.Text = splitstringgem(Main.offench, 12)
        Main.offvz.Visible = True
        Main.offvzlabel2.Visible = True
        Main.offvzlabel2.Text = Main.offvz.Text

        Main.distanzvz.Text = splitstringvz(Main.distanzench, 0)
        Main.Distanzsocket1.Text = splitstringgem(Main.distanzench, 6)
        Main.Distanzsocket2.Text = splitstringgem(Main.distanzench, 9)
        Main.distanzsocket3.Text = splitstringgem(Main.distanzench, 12)
        Main.distanzvz.Visible = True
        Main.distanzvzlabel2.Visible = True
        Main.distanzvzlabel2.Text = Main.distanzvz.Text

        Main.haendevz.Text = splitstringvz(Main.haendeench, 0)
        Main.haendesocket1.Text = splitstringgem(Main.haendeench, 6)
        Main.haendesocket2.Text = splitstringgem(Main.haendeench, 9)
        Main.haendesocket3.Text = splitstringgem(Main.haendeench, 12)
        Main.haendevz.Visible = True

        Main.guertelvz.Text = splitstringvz(Main.guertelench, 0)
        Main.guertelsocket1.Text = splitstringgem(Main.guertelench, 6)
        Main.guertelsocket2.Text = splitstringgem(Main.guertelench, 9)
        Main.guertelsocket3.Text = splitstringgem(Main.guertelench, 12)
        Main.guertelvz.Visible = True

        Main.beinevz.Text = splitstringvz(Main.beineench, 0)
        Main.beinesocket1.Text = splitstringgem(Main.beineench, 6)
        Main.beinesocket2.Text = splitstringgem(Main.beineench, 9)
        Main.beinesocket3.Text = splitstringgem(Main.beineench, 12)
        Main.beinevz.Visible = True

        Main.stiefelvz.Text = splitstringvz(Main.stiefelench, 0)
        Main.stiefelsocket1.Text = splitstringgem(Main.stiefelench, 6)
        Main.stiefelsocket2.Text = splitstringgem(Main.stiefelench, 9)
        Main.stiefelsocket3.Text = splitstringgem(Main.stiefelench, 12)
        Main.stiefelvz.Visible = True

        Main.ring1vz.Text = splitstringvz(Main.ring1ench, 0)
        Main.Ring1socket1.Text = splitstringgem(Main.ring1ench, 6)
        Main.ring1socket2.Text = splitstringgem(Main.ring1ench, 9)
        Main.ring1socket3.Text = splitstringgem(Main.ring1ench, 12)
        Main.ring1vz.Visible = True

        Main.ring2vz.Text = splitstringvz(Main.ring2ench, 0)
        Main.ring2socket1.Text = splitstringgem(Main.ring2ench, 6)
        Main.ring2socket2.Text = splitstringgem(Main.ring2ench, 9)
        Main.ring2socket3.Text = splitstringgem(Main.ring2ench, 12)
        Main.ring2vz.Visible = True

        Main.schmuck1vz.Text = splitstringvz(Main.schmuck1ench, 0)
        Main.schmuck1vz.Visible = True

        Main.schmuck2vz.Text = splitstringvz(Main.schmuck2ench, 0)
        Main.schmuck2vz.Visible = True



    End Sub
    Public Function splitstringvz(ByVal input As String, ByVal position As Integer)
        Try
            Dim parts() As String = input.Split(" "c)
            If Not parts(position) = "0" Then
                Dim quellcodeyx88 As String = My.Resources.VZ_ID_wotlk2
                Dim anfangyx88 As String = parts(position) & ";"
                Dim endeyx88 As String = ";xxxx"
                Dim quellcodeSplityx88 As String
                quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
                quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)

                Return quellcodeSplityx88
            Else
                Return ""
            End If

        Catch ex As Exception
            Return ""
        End Try

    End Function
    Public Function splitstringgem(ByVal input As String, ByVal position As Integer)
        Try
            Dim parts() As String = input.Split(" "c)
            If Not parts(position) = "0" Then
                Dim quellcodeyx88 As String = My.Resources.GEM_ID_wotlk2
                Dim anfangyx88 As String = parts(position) & ";"
                Dim endeyx88 As String = ";xxxx"
                Dim quellcodeSplityx88 As String
                quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
                quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)

                Return getsocketeffectname(quellcodeSplityx88)
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
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
    Public Function getentrybyiteminstance(ByVal instanceid As Integer)
        Try
            Return Int(Val(getentrybyiteminstance(getentrybyiteminstance(Int(Val(runcommand("SELECT itemEntry FROM character_instance WHERE guid = '" & instanceid & "'", "itemEntry")))))))

        Catch ex As Exception
            Return 0
        End Try
    End Function
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
            picbox.Image = My.Resources.empty
        End Try
    End Sub
    Public Sub LoadImageFromUrl(ByRef url As String, ByVal pb As PictureBox)
        Dim request As Net.HttpWebRequest = DirectCast(Net.HttpWebRequest.Create(url), Net.HttpWebRequest)
        Dim response As Net.HttpWebResponse = DirectCast(request.GetResponse, Net.HttpWebResponse)
        Dim img As Image = Image.FromStream(response.GetResponseStream())
        response.Close()
        pb.SizeMode = PictureBoxSizeMode.StretchImage
        pb.Image = img
    End Sub
    Private Function getnamefromid(ByVal itemid As String)
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String = clienyx88.DownloadString("http://wowdata.buffed.de/?i=" & itemid)
            Dim anfangyx88 As String = "<td><h1 class=""headline1"">"
            Dim endeyx88 As String = "</h1></td>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            If quellcodeSplityx88.Contains("Ã¼") Then quellcodeSplityx88 = quellcodeSplityx88.Replace("Ã¼", "ü")
            If quellcodeSplityx88.Contains("Ã¤") Then quellcodeSplityx88 = quellcodeSplityx88.Replace("Ã¤", "ä")
            If quellcodeSplityx88.Contains("Ã¶") Then quellcodeSplityx88 = quellcodeSplityx88.Replace("Ã¶", "ö")
            If quellcodeSplityx88.Contains("ÃŸ") Then quellcodeSplityx88 = quellcodeSplityx88.Replace("ÃŸ", "ß")
            Return quellcodeSplityx88
        Catch ex As Exception
            Return "Platz leer"
        End Try
    End Function
    Public Function getcharguid(ByVal charname As String)
        Try
            Return Int(Val(runcommand("SELECT guid FROM characters WHERE name = '" & charname & "'", "guid")))

        Catch ex As Exception
            MsgBox("Charakter Guid konnte nicht gelesen werden! Überprüfe die Datanbankeintragungen.", MsgBoxStyle.Critical, "Fehler")
            Return -1
        End Try


    End Function


    Public Sub getitemstats(ByVal itemguid As Integer, ByRef slotvar As Object)
        Try
            slotvar = runcommand("SELECT enchantments FROM item_instance WHERE guid='" & itemguid & "'", "enchantments")
            Dim lol As String = ""
        Catch ex As Exception
            slotvar = "-1"
        End Try


    End Sub
    Private Function runcommand(ByVal command As String, ByVal spalte As String)
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
            Dim reader_ol
            myCommand.Connection = conn
            myCommand.CommandText = command
            'start query
            myAdapter.SelectCommand = myCommand
            reader_ol = myCommand.ExecuteReader()
            reader_ol.Read()

            Return reader_ol(spalte)

            reader_ol.Close()
            conn.Close()
        Catch ex As Exception
            conn.Close()
            Return "error"
        End Try
    End Function

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

    Private Function SQLConnection() As Object
        Throw New NotImplementedException
    End Function
    Public Sub NewUser(ByRef SQLStatement As String)

        Dim cmd As MySqlCommand = New MySqlCommand

        With cmd
            .CommandText = SQLStatement
            .CommandType = CommandType.Text
            .Connection = SQLConnection()
            .ExecuteNonQuery()


        End With

        SQLConnection.Close()

        SQLConnection.Dispose()


    End Sub
End Class
