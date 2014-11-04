'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* The class CIUFile will store character and account information into a template
'* file and can also read them.
'*
'* Developed by Alcanmage/megasus

Imports System.IO
Imports System.Drawing.Imaging
Imports System.Text

Public Class CIUFile
    '   Dim writestring As String
    Dim clsConvert As New clsConvert
    Dim writepath As String
    Dim readstring As String
    Dim strread As StreamReader
    Dim fileextract As String
    Dim runfunction As New Functions
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN
    Private Const hexDigits As String = "0123456789ABCDEF"
    Public Sub createfile(ByVal xpath As String)
        writepath = xpath
        nowwrite()
        End Sub
    Public Shared Function HexStringToBytes(ByVal str As String) As Byte()
        ' Determine the number of bytes
        Dim bytes(str.Length >> 1 - 1) As Byte
        For i As Integer = 0 To str.Length - 1 Step 2
            Dim highDigit As Integer = hexDigits.IndexOf(Char.ToUpperInvariant(str.Chars(i)))
            Dim lowDigit As Integer = hexDigits.IndexOf(Char.ToUpperInvariant(str.Chars(i + 1)))
            If highDigit = -1 OrElse lowDigit = -1 Then
                Throw New ArgumentException("The string contains an invalid digit.", "s")
            End If
            bytes(i >> 1) = CByte((highDigit << 4) Or lowDigit)
        Next i
        Return bytes
    End Function
    Public Sub nowread()
       Try
            strread = New StreamReader(Main.MainInstance.tmplpath, Encoding.Default)
            'Dim strreaded As String = strread.ReadLine()
            Dim strreaded As String = ByteArrayToTextString(HexStringToBytes(strread.ReadLine()))
            Dim b() As Byte = Encoding.Default.GetBytes(strreaded)
            strreaded = Encoding.UTF8.GetString(b)
            strread.Close()
            strread.Dispose()
            If Not strreaded.Contains("<<requires>>") Or fileoutdated(strreaded) = True Then
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.templateoutdated, MsgBoxStyle.Critical, localeDE.errornotification)
                    Exit Sub
                Else
                    MsgBox(localeEN.templateoutdated, MsgBoxStyle.Critical, localeEN.errornotification)
                    Exit Sub
                End If
            End If
            Starter.Hide()
            Dim xXquellcodeyx88 As String = strreaded
            Dim xXanfangyx88 As String = "<<datasets>>"
            Dim xXendeyx88 As String = "<</datasets>>"
            Dim xXquellcodeSplityx88 As String
            xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
            xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
            If xXquellcodeSplityx88 = "1" Then
                wait.Show()
                Application.DoEvents()
                Dim xXanfangyx888 As String = "<<importmode>>"
                Dim xXendeyx888 As String = "<</importmode>>"
                Dim xXquellcodeSplityx888 As String
                xXquellcodeSplityx888 = Split(xXquellcodeyx88, xXanfangyx888, 5)(1)
                xXquellcodeSplityx888 = Split(xXquellcodeSplityx888, xXendeyx888, 6)(0)
                Main.MainInstance.importmode = CInt(Val(xXquellcodeSplityx888))
                Main.MainInstance.progressmode = CInt(Val(xXquellcodeSplityx888))
                Dim xXanfangyx8888 As String = "<startdataset1>"
                Dim xXendeyx8888 As String = "<enddataset1>"
                Dim xXquellcodeSplityx8888 As String
                xXquellcodeSplityx8888 = Split(xXquellcodeyx88, xXanfangyx8888, 5)(1)
                xXquellcodeSplityx8888 = Split(xXquellcodeSplityx8888, xXendeyx8888, 6)(0)
                fileextract = xXquellcodeSplityx8888
                getfile()


                Application.DoEvents()
                Main.MainInstance.Show()
                Main.MainInstance.Panel21.Location = New Point(5000, 5000)
                Main.MainInstance.Panel21.Size = New Size(0, 0)
                wait.Close()
                Application.DoEvents()

            Else
                Dim xXanfangyx888 As String = "<<importmode>>"
                Dim xXendeyx888 As String = "<</importmode>>"
                Dim xXquellcodeSplityx888 As String
                xXquellcodeSplityx888 = Split(xXquellcodeyx88, xXanfangyx888, 5)(1)
                xXquellcodeSplityx888 = Split(xXquellcodeSplityx888, xXendeyx888, 6)(0)
                Main.MainInstance.importmode = CInt(Val(xXquellcodeSplityx888))
                Main.MainInstance.progressmode = CInt(Val(xXquellcodeSplityx888))

                Main.MainInstance.cuisets = CInt(Val(xXquellcodeSplityx88))
                If xXquellcodeSplityx888 = "1" Then
                    Main.MainInstance.ausgangsformat = 2
                    Armory2Database.Show()
                ElseIf xXquellcodeSplityx888 = "2" Then
                    Main.MainInstance.ausgangsformat = 2
                    Database2Database.Show()
                ElseIf xXquellcodeSplityx888 = "3" Then
                    Main.MainInstance.ausgangsformat = 2
                    Database2Database.Show()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Function fileoutdated(ByVal filestring As String) As Boolean
        Try
            Dim xXquellcodeyx88 As String = filestring
            Dim xXanfangyx88 As String = "<<requires>>"
            Dim xXendeyx88 As String = "<</requires>>"
            Dim xXquellcodeSplityx88 As String
            xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
            xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
            If CInt(xXquellcodeSplityx88) < Starter.required_template_version Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return True
        End Try
    End Function
    Public Sub readtempdataset(ByVal dataset As Integer)
        Try

            Dim xXquellcodeyx88 As String = My.Settings.savecontent
            Dim xXanfangyx88 As String = "<startdataset" & dataset.ToString & ">"
            Dim xXendeyx88 As String = "<enddataset" & dataset.ToString & ">"
            Dim xXquellcodeSplityx88 As String
            xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
            xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
            fileextract = xXquellcodeSplityx88
            getfile()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub readspecial(ByVal dataset As Integer)
        Try
            strread = New StreamReader(Main.MainInstance.tmplpath, Encoding.Default)
            Dim strreaded As String = ByteArrayToTextString(HexStringToBytes(strread.ReadLine()))
            Dim b() As Byte = Encoding.Default.GetBytes(strreaded)
            strreaded = Encoding.UTF8.GetString(b)
            strread.Close()
            strread.Dispose()
            Dim xXquellcodeyx88 As String = strreaded
            Dim xXanfangyx88 As String = "<startdataset" & dataset.ToString & ">"
            Dim xXendeyx88 As String = "<enddataset" & dataset.ToString & ">"
            Dim xXquellcodeSplityx88 As String
            xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
            xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
            fileextract = xXquellcodeSplityx88
            getfile()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub adddataset()
        prepare_new_dataset(Main.MainInstance.datasets.ToString)
        writeit("importmode", Main.MainInstance.progressmode.ToString, False)
        writeit("charlevel", Main.MainInstance.char_level.ToString, False)
        writeit("charclass", Main.MainInstance.char_class.ToString, False)
        writeit("charrace", Main.MainInstance.char_race.ToString, False)
        writeit("chargender", Main.MainInstance.char_gender.ToString, False)
        writeit("charname", Main.MainInstance.char_name.ToString, False)
        writeit("characterspells", list2string(Main.MainInstance.character_spells), False)
        writeit("specialspells", list2string(Main.MainInstance.specialspells), False)
        writeit("specialskills", list2string(Main.MainInstance.specialskills), False)
        writeit("talents", list2string(Main.MainInstance.talentlist), False)
        writeit("inventoryzero", list2string(Main.MainInstance.character_inventoryzero_list), False)
        writeit("queststatus", list2string(Main.MainInstance.character_queststatus), False)
        writeit("finished_quests", Main.MainInstance.finished_quests, False)
        writeit("action1", Main.MainInstance.arcemu_action1, False)
        writeit("action2", Main.MainInstance.arcemu_action2, False)
        writeit("leveltext", Main.MainInstance.level.Text, False)
        writeit("custom_faction", Main.MainInstance.custom_faction, False)
        'Main IDs


        writeit("kopfid", Main.MainInstance.kopfid.ToString, False)
        writeit("schulterid", Main.MainInstance.schulterid.ToString, False)
        writeit("halsid", Main.MainInstance.halsid.ToString, False)
        writeit("rueckenid", Main.MainInstance.rueckenid.ToString, False)
        writeit("beineid", Main.MainInstance.beineid.ToString, False)
        writeit("wappenrockid", Main.MainInstance.wappenrockid.ToString, False)
        writeit("hemdid", Main.MainInstance.hemdid.ToString, False)
        writeit("handgelenkeid", Main.MainInstance.handgelenkeid.ToString, False)
        writeit("haendeid", Main.MainInstance.haendeid.ToString, False)
        writeit("brustid", Main.MainInstance.brustid.ToString, False)
        writeit("guertelid", Main.MainInstance.guertelid.ToString, False)
        writeit("stiefelid", Main.MainInstance.stiefelid.ToString, False)
        writeit("ring1id", Main.MainInstance.ring1id.ToString, False)
        writeit("ring2id", Main.MainInstance.ring2id.ToString, False)
        writeit("schmuck1id", Main.MainInstance.schmuck1id.ToString, False)
        writeit("schmuck2id", Main.MainInstance.schmuck2id.ToString, False)
        writeit("hauptid", Main.MainInstance.hauptid.ToString, False)
        writeit("offid", Main.MainInstance.offid.ToString, False)
        writeit("distanzid", Main.MainInstance.distanzid.ToString, False)


        'Main names

        writeit("kopfname", Main.MainInstance.Kopf.Text, False)
        writeit("schultername", Main.MainInstance.Schulter.Text, False)
        writeit("halsname", Main.MainInstance.Hals.Text, False)
        writeit("rueckenname", Main.MainInstance.Ruecken.Text, False)
        writeit("beinename", Main.MainInstance.Beine.Text, False)
        writeit("wappenrockname", Main.MainInstance.Wappenrock.Text, False)
        writeit("hemdname", Main.MainInstance.Hemd.Text, False)
        writeit("handgelenkename", Main.MainInstance.Handgelenke.Text, False)
        writeit("haendename", Main.MainInstance.Haende.Text, False)
        writeit("brustname", Main.MainInstance.Brust.Text, False)
        writeit("guertelname", Main.MainInstance.Guertel.Text, False)
        writeit("stiefelname", Main.MainInstance.Stiefel.Text, False)
        writeit("ring1name", Main.MainInstance.Ring1.Text, False)
        writeit("ring2name", Main.MainInstance.Ring2.Text, False)
        writeit("schmuck1name", Main.MainInstance.Schmuck1.Text, False)
        writeit("schmuck2name", Main.MainInstance.Schmuck2.Text, False)
        writeit("hauptname", Main.MainInstance.Haupt.Text, False)
        writeit("offname", Main.MainInstance.Off.Text, False)
        writeit("distanzname", Main.MainInstance.Distanz.Text, False)

        'Item Vz IDs

        writeit("kopfvzench", Main.MainInstance.kopfench.ToString, False)
        writeit("schultervzench", Main.MainInstance.schulterench.ToString, False)
        writeit("halsvzench", Main.MainInstance.halsench.ToString, False)
        writeit("rueckenvzench", Main.MainInstance.rueckenench.ToString, False)
        writeit("beinevzench", Main.MainInstance.beineench.ToString, False)
        writeit("handgelenkevzench", Main.MainInstance.handgelenkeench.ToString, False)
        writeit("haendevzench", Main.MainInstance.haendeench.ToString, False)
        writeit("brustvzench", Main.MainInstance.brustench.ToString, False)
        writeit("guertelvzench", Main.MainInstance.guertelench.ToString, False)
        writeit("stiefelvzench", Main.MainInstance.stiefelench.ToString, False)
        writeit("ring1vzench", Main.MainInstance.ring1ench.ToString, False)
        writeit("ring2vzench", Main.MainInstance.ring2ench.ToString, False)
        writeit("schmuck1vzench", Main.MainInstance.schmuck1ench.ToString, False)
        writeit("schmuck2vzench", Main.MainInstance.schmuck2ench.ToString, False)
        writeit("hauptvzench", Main.MainInstance.hauptench.ToString, False)
        writeit("offvzench", Main.MainInstance.offench.ToString, False)
        writeit("distanzvzench", Main.MainInstance.distanzench.ToString, False)

        '  writeit("vzlist", list2string(Main.MainInstance.vzlist), False)
        writeit("kopfvzid", Main.MainInstance.kopfvzid.ToString, False)
        writeit("schultervzid", Main.MainInstance.schultervzid.ToString, False)
        writeit("halsvzid", Main.MainInstance.halsvzid.ToString, False)
        writeit("rueckenvzid", Main.MainInstance.rueckenvzid.ToString, False)
        writeit("beinevzid", Main.MainInstance.beinevzid.ToString, False)
        writeit("handgelenkevzid", Main.MainInstance.handgelenkevzid.ToString, False)
        writeit("haendevzid", Main.MainInstance.haendevzid.ToString, False)
        writeit("brustvzid", Main.MainInstance.brustvzid.ToString, False)
        writeit("guertelvzid", Main.MainInstance.guertelvzid.ToString, False)
        writeit("stiefelvzid", Main.MainInstance.stiefelvzid.ToString, False)
        writeit("ring1vzid", Main.MainInstance.ring1vzid.ToString, False)
        writeit("ring2vzid", Main.MainInstance.ring2vzid.ToString, False)
        writeit("schmuck1vzid", Main.MainInstance.schmuck1vzid.ToString, False)
        writeit("schmuck2vzid", Main.MainInstance.schmuck2vzid.ToString, False)
        writeit("hauptvzid", Main.MainInstance.hauptvzid.ToString, False)
        writeit("offvzid", Main.MainInstance.offvzid.ToString, False)
        writeit("distanzvzid", Main.MainInstance.distanzvzid.ToString, False)


        'Item Vz Names

        writeit("kopfvzname", Main.MainInstance.kopfvz.Text, False)
        writeit("schultervzname", Main.MainInstance.schultervz.Text, False)
        writeit("halsvzname", Main.MainInstance.halsvz.Text, False)
        writeit("rueckenvzname", Main.MainInstance.rueckenvz.Text, False)
        writeit("beinevzname", Main.MainInstance.beinevz.Text, False)
        writeit("handgelenkevzname", Main.MainInstance.handgelenkevz.Text, False)
        writeit("haendevzname", Main.MainInstance.haendevz.Text, False)
        writeit("brustvzname", Main.MainInstance.brustvz.Text, False)
        writeit("guertelvzname", Main.MainInstance.guertelvz.Text, False)
        writeit("stiefelvzname", Main.MainInstance.stiefelvz.Text, False)
        writeit("ring1vzname", Main.MainInstance.ring1vz.Text, False)
        writeit("ring2vzname", Main.MainInstance.ring2vz.Text, False)
        writeit("schmuck1vzname", Main.MainInstance.schmuck1vz.Text, False)
        writeit("schmuck2vzname", Main.MainInstance.schmuck2vz.Text, False)
        writeit("hauptvzname", Main.MainInstance.hauptvz.Text, False)
        writeit("offvzname", Main.MainInstance.offvz.Text, False)
        writeit("distanzvzname", Main.MainInstance.distanzvz.Text, False)

        'Item Socket IDs

        '     writeit("gemlist", list2string(Main.MainInstance.gemlist), False)
        writeit("kopfsocket1id", Main.MainInstance.kopfsocket1id.ToString, False)
        writeit("schultersocket1id", Main.MainInstance.schultersocket1id.ToString, False)
        writeit("halssocket1id", Main.MainInstance.halssocket1id.ToString, False)
        writeit("rueckensocket1id", Main.MainInstance.rueckensocket1id.ToString, False)
        writeit("beinesocket1id", Main.MainInstance.beinesocket1id.ToString, False)
        writeit("handgelenkesocket1id", Main.MainInstance.handgelenkesocket1id.ToString, False)
        writeit("haendesocket1id", Main.MainInstance.haendesocket1id.ToString, False)
        writeit("brustsocket1id", Main.MainInstance.brustsocket1id.ToString, False)
        writeit("guertelsocket1id", Main.MainInstance.guertelsocket1id.ToString, False)
        writeit("stiefelsocket1id", Main.MainInstance.stiefelsocket1id.ToString, False)
        writeit("ring1socket1id", Main.MainInstance.ring1socket1id.ToString, False)
        writeit("ring2socket1id", Main.MainInstance.ring2socket1id.ToString, False)
        writeit("hauptsocket1id", Main.MainInstance.hauptsocket1id.ToString, False)
        writeit("offsocket1id", Main.MainInstance.offsocket1id.ToString, False)
        writeit("distanzsocket1id", Main.MainInstance.distanzsocket1id.ToString, False)

        writeit("kopfsocket2id", Main.MainInstance.kopfsocket2id.ToString, False)
        writeit("schultersocket2id", Main.MainInstance.schultersocket2id.ToString, False)
        writeit("halssocket2id", Main.MainInstance.halssocket2id.ToString, False)
        writeit("rueckensocket2id", Main.MainInstance.rueckensocket2id.ToString, False)
        writeit("beinesocket2id", Main.MainInstance.beinesocket2id.ToString, False)
        writeit("handgelenkesocket2id", Main.MainInstance.handgelenkesocket2id.ToString, False)
        writeit("haendesocket2id", Main.MainInstance.haendesocket2id.ToString, False)
        writeit("brustsocket2id", Main.MainInstance.brustsocket2id.ToString, False)
        writeit("guertelsocket2id", Main.MainInstance.guertelsocket2id.ToString, False)
        writeit("stiefelsocket2id", Main.MainInstance.stiefelsocket2id.ToString, False)
        writeit("ring2socket2id", Main.MainInstance.ring2socket2id.ToString, False)
        writeit("ring2socket2id", Main.MainInstance.ring2socket2id.ToString, False)
        writeit("hauptsocket2id", Main.MainInstance.hauptsocket2id.ToString, False)
        writeit("offsocket2id", Main.MainInstance.offsocket2id.ToString, False)
        writeit("distanzsocket2id", Main.MainInstance.distanzsocket2id.ToString, False)

        writeit("kopfsocket3id", Main.MainInstance.kopfsocket3id.ToString, False)
        writeit("schultersocket3id", Main.MainInstance.schultersocket3id.ToString, False)
        writeit("halssocket3id", Main.MainInstance.halssocket3id.ToString, False)
        writeit("rueckensocket3id", Main.MainInstance.rueckensocket3id.ToString, False)
        writeit("beinesocket3id", Main.MainInstance.beinesocket3id.ToString, False)
        writeit("handgelenkesocket3id", Main.MainInstance.handgelenkesocket3id.ToString, False)
        writeit("haendesocket3id", Main.MainInstance.haendesocket3id.ToString, False)
        writeit("brustsocket3id", Main.MainInstance.brustsocket3id.ToString, False)
        writeit("guertelsocket3id", Main.MainInstance.guertelsocket3id.ToString, False)
        writeit("stiefelsocket3id", Main.MainInstance.stiefelsocket3id.ToString, False)
        writeit("hauptsocket3id", Main.MainInstance.hauptsocket3id.ToString, False)
        writeit("offsocket3id", Main.MainInstance.offsocket3id.ToString, False)
        writeit("distanzsocket3id", Main.MainInstance.distanzsocket3id.ToString, False)

        'Item Socket Names

        writeit("kopfsocket1name", Main.MainInstance.kopfsocket1.Text, False)
        writeit("schultersocket1name", Main.MainInstance.schultersocket1.Text, False)
        writeit("halssocket1name", Main.MainInstance.halssocket1.Text, False)
        writeit("rueckensocket1name", Main.MainInstance.rueckensocket1.Text, False)
        writeit("beinesocket1name", Main.MainInstance.beinesocket1.Text, False)
        writeit("handgelenkesocket1name", Main.MainInstance.Handgelenkesocket1.Text, False)
        writeit("haendesocket1name", Main.MainInstance.haendesocket1.Text, False)
        writeit("brustsocket1name", Main.MainInstance.brustsocket1.Text, False)
        writeit("guertelsocket1name", Main.MainInstance.guertelsocket1.Text, False)
        writeit("stiefelsocket1name", Main.MainInstance.stiefelsocket1.Text, False)
        writeit("ring1socket1name", Main.MainInstance.Ring1socket1.Text, False)
        writeit("ring2socket1name", Main.MainInstance.ring2socket1.Text, False)
        writeit("hauptsocket1name", Main.MainInstance.Hauptsocket1.Text, False)
        writeit("offsocket1name", Main.MainInstance.Offsocket1.Text, False)
        writeit("distanzsocket1name", Main.MainInstance.Distanzsocket1.Text, False)

        writeit("kopfsocket2name", Main.MainInstance.kopfsocket2.Text, False)
        writeit("schultersocket2name", Main.MainInstance.schultersocket2.Text, False)
        writeit("halssocket2name", Main.MainInstance.halssocket2.Text, False)
        writeit("rueckensocket2name", Main.MainInstance.rueckensocket2.Text, False)
        writeit("beinesocket2name", Main.MainInstance.beinesocket2.Text, False)
        writeit("handgelenkesocket2name", Main.MainInstance.handgelenkesocket2.Text, False)
        writeit("haendesocket2name", Main.MainInstance.haendesocket2.Text, False)
        writeit("brustsocket2name", Main.MainInstance.brustsocket2.Text, False)
        writeit("guertelsocket2name", Main.MainInstance.guertelsocket2.Text, False)
        writeit("stiefelsocket2name", Main.MainInstance.stiefelsocket2.Text, False)
        writeit("ring2socket2name", Main.MainInstance.ring2socket2.Text, False)
        writeit("ring2socket2name", Main.MainInstance.ring2socket2.Text, False)
        writeit("hauptsocket2name", Main.MainInstance.Hauptsocket2.Text, False)
        writeit("offsocket2name", Main.MainInstance.Offsocket2.Text, False)
        writeit("distanzsocket2name", Main.MainInstance.Distanzsocket2.Text, False)

        writeit("kopfsocket3name", Main.MainInstance.kopfsocket3.Text, False)
        writeit("schultersocket3name", Main.MainInstance.schultersocket3.Text, False)
        writeit("halssocket3name", Main.MainInstance.halssocket3.Text, False)
        writeit("rueckensocket3name", Main.MainInstance.rueckensocket3.Text, False)
        writeit("beinesocket3name", Main.MainInstance.beinesocket3.Text, False)
        writeit("handgelenkesocket3name", Main.MainInstance.Handgelenkesocket3.Text, False)
        writeit("haendesocket3name", Main.MainInstance.haendesocket3.Text, False)
        writeit("brustsocket3name", Main.MainInstance.brustsocket3.Text, False)
        writeit("guertelsocket3name", Main.MainInstance.guertelsocket3.Text, False)
        writeit("stiefelsocket3name", Main.MainInstance.stiefelsocket3.Text, False)
        writeit("hauptsocket3name", Main.MainInstance.hauptsocket3.Text, False)
        writeit("offsocket3name", Main.MainInstance.offsocket3.Text, False)
        writeit("distanzsocket3name", Main.MainInstance.distanzsocket3.Text, False)

        'Glyph IDs

        '   writeit("glyphlist", list2string(Main.MainInstance.glyphlist), False)
        writeit("primeglyph1id", Main.MainInstance.primeglyph1.ToString, False)
        writeit("primeglyph2id", Main.MainInstance.primeglyph2.ToString, False)
        writeit("primeglyph3id", Main.MainInstance.primeglyph3.ToString, False)
        writeit("majorglyph1id", Main.MainInstance.majorglyph1.ToString, False)
        writeit("majorglyph2id", Main.MainInstance.majorglyph2.ToString, False)
        writeit("majorglyph3id", Main.MainInstance.majorglyph3.ToString, False)
        writeit("minorglyph1id", Main.MainInstance.minorglyph1.ToString, False)
        writeit("minorglyph2id", Main.MainInstance.minorglyph2.ToString, False)
        writeit("minorglyph3id", Main.MainInstance.minorglyph3.ToString, False)
        writeit("secprimeglyph1id", Main.MainInstance.secprimeglyph1.ToString, False)
        writeit("secprimeglyph2id", Main.MainInstance.secprimeglyph2.ToString, False)
        writeit("secprimeglyph3id", Main.MainInstance.secprimeglyph3.ToString, False)
        writeit("secmajorglyph1id", Main.MainInstance.secmajorglyph1.ToString, False)
        writeit("secmajorglyph2id", Main.MainInstance.secmajorglyph2.ToString, False)
        writeit("secmajorglyph3id", Main.MainInstance.secmajorglyph3.ToString, False)
        writeit("secminorglyph1id", Main.MainInstance.secminorglyph1.ToString, False)
        writeit("secminorglyph2id", Main.MainInstance.secminorglyph2.ToString, False)
        writeit("secminorglyph3id", Main.MainInstance.secminorglyph3.ToString, False)
        'Glyph Names

        writeit("primeglyph1", Main.MainInstance.textprimeglyph1, False)
        writeit("primeglyph2", Main.MainInstance.textprimeglyph2, False)
        writeit("primeglyph3", Main.MainInstance.textprimeglyph3, False)
        writeit("majorglyph1", Main.MainInstance.textmajorglyph1, False)
        writeit("majorglyph2", Main.MainInstance.textmajorglyph2, False)
        writeit("majorglyph3", Main.MainInstance.textmajorglyph3, False)
        writeit("minorglyph1", Main.MainInstance.textminorglyph1, False)
        writeit("minorglyph2", Main.MainInstance.textminorglyph2, False)
        writeit("minorglyph3", Main.MainInstance.textminorglyph3, False)
        writeit("secprimeglyph1", Main.MainInstance.sectextprimeglyph1, False)
        writeit("secprimeglyph2", Main.MainInstance.sectextprimeglyph2, False)
        writeit("secprimeglyph3", Main.MainInstance.sectextprimeglyph3, False)
        writeit("secmajorglyph1", Main.MainInstance.sectextmajorglyph1, False)
        writeit("secmajorglyph2", Main.MainInstance.sectextmajorglyph2, False)
        writeit("secmajorglyph3", Main.MainInstance.sectextmajorglyph3, False)
        writeit("secminorglyph1", Main.MainInstance.sectextminorglyph1, False)
        writeit("secminorglyph2", Main.MainInstance.sectextminorglyph2, False)
        writeit("secminorglyph3", Main.MainInstance.sectextminorglyph3, False)

        writeit("accountid", Main.MainInstance.accountid.ToString, False)
        writeit("char_guid", Main.MainInstance.char_guid.ToString, False)
        writeit("char_xp", Main.MainInstance.char_xp.ToString, False)
        writeit("player_money", Main.MainInstance.player_money.ToString, False)
        writeit("playerBytes", Main.MainInstance.playerBytes.ToString, False)
        writeit("playerBytes2", Main.MainInstance.playerBytes2.ToString, False)
        writeit("playerFlags", Main.MainInstance.playerFlags.ToString, False)
        writeit("position_x", CStr(Main.MainInstance.position_x), False)
        writeit("position_y", CStr(Main.MainInstance.position_y), False)
        writeit("position_z", CStr(Main.MainInstance.position_z), False)
        writeit("map", Main.MainInstance.map.ToString, False)
        writeit("instance_id", Main.MainInstance.instance_id.ToString, False)
        writeit("instance_mode_mask", Main.MainInstance.instance_mode_mask, False)
        writeit("orientation", CStr(Main.MainInstance.orientation), False)
        writeit("taximask", Main.MainInstance.taximask, False)
        writeit("cinematic", Main.MainInstance.cinematic.ToString, False)
        writeit("totaltime", Main.MainInstance.totaltime.ToString, False)
        writeit("leveltime", Main.MainInstance.leveltime.ToString, False)
        writeit("extra_flags", Main.MainInstance.extra_flags, False)
        writeit("stable_slots", Main.MainInstance.stable_slots, False)
        writeit("at_login", Main.MainInstance.at_login, False)
        writeit("zone", Main.MainInstance.zone.ToString, False)
        writeit("arenaPoints", Main.MainInstance.arenaPoints.ToString, False)
        writeit("totalHonorPoints", Main.MainInstance.totalHonorPoints.ToString, False)
        writeit("totalKills", Main.MainInstance.totalKills.ToString, False)
        writeit("chosenTitle", Main.MainInstance.chosenTitle, False)
        writeit("knownCurrencies", Main.MainInstance.knownCurrencies, False)
        writeit("watchedFaction", Main.MainInstance.watchedFaction, False)
        writeit("health", Main.MainInstance.health.ToString, False)
        writeit("speccount", Main.MainInstance.speccount.ToString, False)
        writeit("activespec", Main.MainInstance.activespec.ToString, False)
        writeit("exploredZones", Main.MainInstance.exploredZones, False)
        writeit("knownTitles", Main.MainInstance.knownTitles, False)
        writeit("actionBars", Main.MainInstance.actionBars, False)
        writeit("character_homebind", Main.MainInstance.character_homebind, False)
        'character_achievement Table

        writeit("character_achievement_list", list2string(Main.MainInstance.character_achievement_list), False)
        'character_action Table


        writeit("character_action_list", list2string(Main.MainInstance.character_action_list), False)

        writeit("character_inventory_list", list2string(Main.MainInstance.character_inventory_list), False)
        writeit("character_reputatuion_list", list2string(Main.MainInstance.character_reputatuion_list), False)
        writeit("character_skills_list", list2string(Main.MainInstance.character_skills_list), False)
        writeit("character_talent_list", list2string(Main.MainInstance.character_talent_list), False)

        writeit("accountname", Main.MainInstance.accountname, False)
        writeit("sha_pass_hash", Main.MainInstance.sha_pass_hash, False)
        writeit("sessionkey", Main.MainInstance.sessionkey, False)
        writeit("account_v", Main.MainInstance.account_v, False)
        writeit("account_s", Main.MainInstance.account_s, False)
        writeit("email", Main.MainInstance.email, False)
        writeit("joindate", Main.MainInstance.joindate, False)
        writeit("expansion", Main.MainInstance.expansion.ToString, False)
        writeit("locale", Main.MainInstance.locale.ToString, False)

        writeit("account_access_gmlevel", Main.MainInstance.account_access_gmlevel.ToString, False)
        writeit("account_access_RealmID", Main.MainInstance.account_access_RealmID.ToString, False)
        If Not Main.MainInstance.datasets >= 2 Then
            'Item Pics

            writeit("kopfpic", ConvertImageToString(Main.MainInstance.kopfpic.Image), False)
            writeit("halspic", ConvertImageToString(Main.MainInstance.Halspic.Image), False)
            writeit("schulterpic", ConvertImageToString(Main.MainInstance.Schulterpic.Image), False)
            writeit("wappenrockpic", ConvertImageToString(Main.MainInstance.Wappenrockpic.Image), False)
            writeit("hemdpic", ConvertImageToString(Main.MainInstance.Hemdpic.Image), False)
            writeit("rueckenpic", ConvertImageToString(Main.MainInstance.Rueckenpic.Image), False)
            writeit("handgelenkepic", ConvertImageToString(Main.MainInstance.Handgelenkepic.Image), False)
            writeit("haendepic", ConvertImageToString(Main.MainInstance.Haendepic.Image), False)
            writeit("guertelpic", ConvertImageToString(Main.MainInstance.Guertelpic.Image), False)
            writeit("stiefelpic", ConvertImageToString(Main.MainInstance.Stiefelpic.Image), False)
            writeit("beinepic", ConvertImageToString(Main.MainInstance.Beinepic.Image), False)
            writeit("ring1pic", ConvertImageToString(Main.MainInstance.Ring1pic.Image), False)
            writeit("ring2pic", ConvertImageToString(Main.MainInstance.Ring2pic.Image), False)
            writeit("schmuck1pic", ConvertImageToString(Main.MainInstance.Schmuck1pic.Image), False)
            writeit("schmuck2pic", ConvertImageToString(Main.MainInstance.Schmuck2pic.Image), False)
            writeit("hauptpic", ConvertImageToString(Main.MainInstance.Hauptpic.Image), False)
            writeit("offpic", ConvertImageToString(Main.MainInstance.Offpic.Image), False)
            writeit("distanzpic", ConvertImageToString(Main.MainInstance.Distanzpic.Image), False)
            writeit("brustpic", ConvertImageToString(Main.MainInstance.Brustpic.Image), False)

            'Glyph Pics

            writeit("prim1pic", ConvertImageToString(Main.MainInstance.glyphpic1), False)
            writeit("prim2pic", ConvertImageToString(Main.MainInstance.glyphpic2), False)
            writeit("prim3pic", ConvertImageToString(Main.MainInstance.glyphpic3), False)
            writeit("erheb1pic", ConvertImageToString(Main.MainInstance.glyphpic4), False)
            writeit("erheb2pic", ConvertImageToString(Main.MainInstance.glyphpic5), False)
            writeit("erheb3pic", ConvertImageToString(Main.MainInstance.glyphpic6), False)
            writeit("gering1pic", ConvertImageToString(Main.MainInstance.glyphpic7), False)
            writeit("gering2pic", ConvertImageToString(Main.MainInstance.glyphpic8), False)
            writeit("gering3pic", ConvertImageToString(Main.MainInstance.glyphpic9), False)

            writeit("secprim1pic", ConvertImageToString(Main.MainInstance.secglyphpic1), False)
            writeit("secprim2pic", ConvertImageToString(Main.MainInstance.secglyphpic2), False)
            writeit("secprim3pic", ConvertImageToString(Main.MainInstance.secglyphpic3), False)
            writeit("secerheb1pic", ConvertImageToString(Main.MainInstance.secglyphpic4), False)
            writeit("secerheb2pic", ConvertImageToString(Main.MainInstance.secglyphpic5), False)
            writeit("secerheb3pic", ConvertImageToString(Main.MainInstance.secglyphpic6), False)
            writeit("secgering1pic", ConvertImageToString(Main.MainInstance.secglyphpic7), False)
            writeit("secgering2pic", ConvertImageToString(Main.MainInstance.secglyphpic8), False)
            writeit("secgering3pic", ConvertImageToString(Main.MainInstance.secglyphpic9), False)
        End If
        writeit("last", "go", True)
        end_dataset(Main.MainInstance.datasets.ToString)
    End Sub

    Public Sub getfile()

        readitINT("charlevel", Main.MainInstance.char_level, False, True)
        readitINT("charclass", Main.MainInstance.char_class, False, True)
        readitINT("charrace", Main.MainInstance.char_race, False, True)
        readitINT("chargender", Main.MainInstance.char_gender, False, True)
        readit("charname", Main.MainInstance.char_name, False)
        readit("finished_quests", Main.MainInstance.finished_quests, False)
        readit("action1", Main.MainInstance.arcemu_action1, False)
        readit("action2", Main.MainInstance.arcemu_action2, False)
        readit("custom_faction", Main.MainInstance.custom_faction, False)
        '      string2list("charopt", Main.MainInstance.charopt)
        '   string2list("charenchlist", Main.MainInstance.charenchlist)
        string2list("specialspells", Main.MainInstance.specialspells)
        string2list("specialskills", Main.MainInstance.specialskills)
        string2list("characterspells", Main.MainInstance.character_spells)
        string2list("talents", Main.MainInstance.talentlist)
        string2list("inventoryzero", Main.MainInstance.character_inventoryzero_list)
        string2list("queststatus", Main.MainInstance.character_queststatus)

        '     string2list("vzlist", Main.MainInstance.vzlist)
        '    string2list("gemlist", Main.MainInstance.gemlist)
        '   string2list("glyphlist", Main.MainInstance.glyphlist)
        'Main IDs
        '  string2list("itemlist", Main.MainInstance.itemlist)

        readit2("leveltext", Main.MainInstance.level, False)


        readitINT("importmode", Main.MainInstance.importmode, False, True)

        readitINT("kopfid", Main.MainInstance.kopfid, False, True)
        readitINT("schulterid", Main.MainInstance.schulterid, False, True)
        readitINT("halsid", Main.MainInstance.halsid, False, True)
        readitINT("rueckenid", Main.MainInstance.rueckenid, False, True)
        readitINT("beineid", Main.MainInstance.beineid, False, True)
        readitINT("wappenrockid", Main.MainInstance.wappenrockid, False, True)
        readitINT("hemdid", Main.MainInstance.hemdid, False, True)
        readitINT("handgelenkeid", Main.MainInstance.handgelenkeid, False, True)
        readitINT("haendeid", Main.MainInstance.haendeid, False, True)
        readitINT("brustid", Main.MainInstance.brustid, False, True)
        readitINT("guertelid", Main.MainInstance.guertelid, False, True)
        readitINT("stiefelid", Main.MainInstance.stiefelid, False, True)
        readitINT("ring1id", Main.MainInstance.ring1id, False, True)
        readitINT("ring2id", Main.MainInstance.ring2id, False, True)
        readitINT("schmuck1id", Main.MainInstance.schmuck1id, False, True)
        readitINT("schmuck2id", Main.MainInstance.schmuck2id, False, True)
        readitINT("hauptid", Main.MainInstance.hauptid, False, True)
        readitINT("offid", Main.MainInstance.offid, False, True)
        readitINT("distanzid", Main.MainInstance.distanzid, False, True)


        'Main names

        readit2("kopfname", Main.MainInstance.Kopf, False)
        readit2("schultername", Main.MainInstance.Schulter, False)
        readit2("halsname", Main.MainInstance.Hals, False)
        readit2("rueckenname", Main.MainInstance.Ruecken, False)
        readit2("beinename", Main.MainInstance.Beine, False)
        readit2("wappenrockname", Main.MainInstance.Wappenrock, False)
        readit2("hemdname", Main.MainInstance.Hemd, False)
        readit2("handgelenkename", Main.MainInstance.Handgelenke, False)
        readit2("haendename", Main.MainInstance.Haende, False)
        readit2("brustname", Main.MainInstance.Brust, False)
        readit2("guertelname", Main.MainInstance.Guertel, False)
        readit2("stiefelname", Main.MainInstance.Stiefel, False)
        readit2("ring1name", Main.MainInstance.Ring1, False)
        readit2("ring2name", Main.MainInstance.Ring2, False)
        readit2("schmuck1name", Main.MainInstance.Schmuck1, False)
        readit2("schmuck2name", Main.MainInstance.Schmuck2, False)
        readit2("hauptname", Main.MainInstance.Haupt, False)
        readit2("offname", Main.MainInstance.Off, False)
        readit2("distanzname", Main.MainInstance.Distanz, False)

        'Get correct itemname

        If Not Main.MainInstance.kopfid = 0 Then Main.MainInstance.Kopf.Text = runfunction.getnamefromitemid(Main.MainInstance.kopfid.ToString())
        If Not Main.MainInstance.schulterid = 0 Then Main.MainInstance.Schulter.Text = runfunction.getnamefromitemid(Main.MainInstance.schulterid.ToString())
        If Not Main.MainInstance.halsid = 0 Then Main.MainInstance.Hals.Text = runfunction.getnamefromitemid(Main.MainInstance.halsid.ToString())
        If Not Main.MainInstance.rueckenid = 0 Then Main.MainInstance.Ruecken.Text = runfunction.getnamefromitemid(Main.MainInstance.rueckenid.ToString())
        If Not Main.MainInstance.beineid = 0 Then Main.MainInstance.Beine.Text = runfunction.getnamefromitemid(Main.MainInstance.beineid.ToString())
        If Not Main.MainInstance.wappenrockid = 0 Then Main.MainInstance.Wappenrock.Text = runfunction.getnamefromitemid(Main.MainInstance.wappenrockid.ToString())
        If Not Main.MainInstance.hemdid = 0 Then Main.MainInstance.Hemd.Text = runfunction.getnamefromitemid(Main.MainInstance.hemdid.ToString())
        If Not Main.MainInstance.handgelenkeid = 0 Then Main.MainInstance.Handgelenke.Text = runfunction.getnamefromitemid(Main.MainInstance.handgelenkeid.ToString())
        If Not Main.MainInstance.haendeid = 0 Then Main.MainInstance.Haende.Text = runfunction.getnamefromitemid(Main.MainInstance.haendeid.ToString())
        If Not Main.MainInstance.brustid = 0 Then Main.MainInstance.Brust.Text = runfunction.getnamefromitemid(Main.MainInstance.brustid.ToString())
        If Not Main.MainInstance.guertelid = 0 Then Main.MainInstance.Guertel.Text = runfunction.getnamefromitemid(Main.MainInstance.guertelid.ToString())
        If Not Main.MainInstance.stiefelid = 0 Then Main.MainInstance.Stiefel.Text = runfunction.getnamefromitemid(Main.MainInstance.stiefelid.ToString())
        If Not Main.MainInstance.ring1id = 0 Then Main.MainInstance.Ring1.Text = runfunction.getnamefromitemid(Main.MainInstance.ring1id.ToString())
        If Not Main.MainInstance.ring2id = 0 Then Main.MainInstance.Ring2.Text = runfunction.getnamefromitemid(Main.MainInstance.ring2id.ToString())
        If Not Main.MainInstance.schmuck1id = 0 Then Main.MainInstance.Schmuck1.Text = runfunction.getnamefromitemid(Main.MainInstance.schmuck1id.ToString())
        If Not Main.MainInstance.schmuck2id = 0 Then Main.MainInstance.Schmuck2.Text = runfunction.getnamefromitemid(Main.MainInstance.schmuck2id.ToString())
        If Not Main.MainInstance.hauptid = 0 Then Main.MainInstance.Haupt.Text = runfunction.getnamefromitemid(Main.MainInstance.hauptid.ToString())
        If Not Main.MainInstance.offid = 0 Then Main.MainInstance.Off.Text = runfunction.getnamefromitemid(Main.MainInstance.offid.ToString())
        If Not Main.MainInstance.distanzid = 0 Then Main.MainInstance.Distanz.Text = runfunction.getnamefromitemid(Main.MainInstance.distanzid.ToString())

        readitINT("kopfvzid", Main.MainInstance.kopfvzid, False, True)
        readitINT("schultervzid", Main.MainInstance.schultervzid, False, True)
        readitINT("halsvzid", Main.MainInstance.halsvzid, False, True)
        readitINT("rueckenvzid", Main.MainInstance.rueckenvzid, False, True)
        readitINT("beinevzid", Main.MainInstance.beinevzid, False, True)
        readitINT("handgelenkevzid", Main.MainInstance.handgelenkevzid, False, True)
        readitINT("haendevzid", Main.MainInstance.haendevzid, False, True)
        readitINT("brustvzid", Main.MainInstance.brustvzid, False, True)
        readitINT("guertelvzid", Main.MainInstance.guertelvzid, False, True)
        readitINT("stiefelvzid", Main.MainInstance.stiefelvzid, False, True)
        readitINT("ring1vzid", Main.MainInstance.ring1vzid, False, True)
        readitINT("ring2vzid", Main.MainInstance.ring2vzid, False, True)
        readitINT("schmuck1vzid", Main.MainInstance.schmuck1vzid, False, True)
        readitINT("schmuck2vzid", Main.MainInstance.schmuck2vzid, False, True)
        readitINT("hauptvzid", Main.MainInstance.hauptvzid, False, True)
        readitINT("offvzid", Main.MainInstance.offvzid, False, True)
        readitINT("distanzvzid", Main.MainInstance.distanzvzid, False, True)
        readitINT("hauptvzid", Main.MainInstance.hauptvzid, False, True)
        readitINT("offvzid", Main.MainInstance.offvzid, False, True)
        readitINT("distanzvzid", Main.MainInstance.distanzvzid, False, True)

        'Item Vz Names

        readit2("kopfvzname", Main.MainInstance.kopfvz, False)
        readit2("schultervzname", Main.MainInstance.schultervz, False)
        readit2("halsvzname", Main.MainInstance.halsvz, False)
        readit2("rueckenvzname", Main.MainInstance.rueckenvz, False)
        readit2("beinevzname", Main.MainInstance.beinevz, False)
        readit2("handgelenkevzname", Main.MainInstance.handgelenkevz, False)
        readit2("haendevzname", Main.MainInstance.haendevz, False)
        readit2("brustvzname", Main.MainInstance.brustvz, False)
        readit2("guertelvzname", Main.MainInstance.guertelvz, False)
        readit2("stiefelvzname", Main.MainInstance.stiefelvz, False)
        readit2("ring1vzname", Main.MainInstance.ring1vz, False)
        readit2("ring2vzname", Main.MainInstance.ring2vz, False)
        readit2("schmuck1vzname", Main.MainInstance.schmuck1vz, False)
        readit2("schmuck2vzname", Main.MainInstance.schmuck2vz, False)
        readit2("hauptvzname", Main.MainInstance.hauptvz, False)
        readit2("offvzname", Main.MainInstance.offvz, False)
        readit2("distanzvzname", Main.MainInstance.distanzvz, False)
        readit2("hauptvzname", Main.MainInstance.hauptvzlabel2, False)
        readit2("offvzname", Main.MainInstance.offvzlabel2, False)
        readit2("distanzvzname", Main.MainInstance.distanzvzlabel2, False)

        'Item Socket Names
        readitINT("kopfsocket1id", Main.MainInstance.kopfsocket1id, False, True)
        readitINT("schultersocket1id", Main.MainInstance.schultersocket1id, False, True)
        readitINT("halssocket1id", Main.MainInstance.halssocket1id, False, True)
        readitINT("rueckensocket1id", Main.MainInstance.rueckensocket1id, False, True)
        readitINT("beinesocket1id", Main.MainInstance.beinesocket1id, False, True)
        readitINT("handgelenkesocket1id", Main.MainInstance.handgelenkesocket1id, False, True)
        readitINT("haendesocket1id", Main.MainInstance.haendesocket1id, False, True)
        readitINT("brustsocket1id", Main.MainInstance.brustsocket1id, False, True)
        readitINT("guertelsocket1id", Main.MainInstance.guertelsocket1id, False, True)
        readitINT("stiefelsocket1id", Main.MainInstance.stiefelsocket1id, False, True)
        readitINT("ring1socket1id", Main.MainInstance.ring1socket1id, False, True)
        readitINT("ring2socket1id", Main.MainInstance.ring2socket1id, False, True)
        readitINT("hauptsocket1id", Main.MainInstance.hauptsocket1id, False, True)
        readitINT("offsocket1id", Main.MainInstance.offsocket1id, False, True)
        readitINT("distanzsocket1id", Main.MainInstance.distanzsocket1id, False, True)

        readitINT("kopfsocket2id", Main.MainInstance.kopfsocket2id, False, True)
        readitINT("schultersocket2id", Main.MainInstance.schultersocket2id, False, True)
        readitINT("halssocket2id", Main.MainInstance.halssocket2id, False, True)
        readitINT("rueckensocket2id", Main.MainInstance.rueckensocket2id, False, True)
        readitINT("beinesocket2id", Main.MainInstance.beinesocket2id, False, True)
        readitINT("handgelenkesocket2id", Main.MainInstance.handgelenkesocket2id, False, True)
        readitINT("haendesocket2id", Main.MainInstance.haendesocket2id, False, True)
        readitINT("brustsocket2id", Main.MainInstance.brustsocket2id, False, True)
        readitINT("guertelsocket2id", Main.MainInstance.guertelsocket2id, False, True)
        readitINT("stiefelsocket2id", Main.MainInstance.stiefelsocket2id, False, True)
        readitINT("ring2socket2id", Main.MainInstance.ring2socket2id, False, True)
        readitINT("ring2socket2id", Main.MainInstance.ring2socket2id, False, True)
        readitINT("hauptsocket2id", Main.MainInstance.hauptsocket2id, False, True)
        readitINT("offsocket2id", Main.MainInstance.offsocket2id, False, True)
        readitINT("distanzsocket2id", Main.MainInstance.distanzsocket2id, False, True)

        readitINT("kopfsocket3id", Main.MainInstance.kopfsocket3id, False, True)
        readitINT("schultersocket3id", Main.MainInstance.schultersocket3id, False, True)
        readitINT("halssocket3id", Main.MainInstance.halssocket3id, False, True)
        readitINT("rueckensocket3id", Main.MainInstance.rueckensocket3id, False, True)
        readitINT("beinesocket3id", Main.MainInstance.beinesocket3id, False, True)
        readitINT("handgelenkesocket3id", Main.MainInstance.handgelenkesocket3id, False, True)
        readitINT("haendesocket3id", Main.MainInstance.haendesocket3id, False, True)
        readitINT("brustsocket3id", Main.MainInstance.brustsocket3id, False, True)
        readitINT("guertelsocket3id", Main.MainInstance.guertelsocket3id, False, True)
        readitINT("stiefelsocket3id", Main.MainInstance.stiefelsocket3id, False, True)
        readitINT("hauptsocket3id", Main.MainInstance.hauptsocket3id, False, True)
        readitINT("offsocket3id", Main.MainInstance.offsocket3id, False, True)
        readitINT("distanzsocket3id", Main.MainInstance.distanzsocket3id, False, True)

        readit("kopfvzench", Main.MainInstance.kopfench, False)
        readit("schultervzench", Main.MainInstance.schulterench, False)
        readit("halsvzench", Main.MainInstance.halsench, False)
        readit("rueckenvzench", Main.MainInstance.rueckenench, False)
        readit("beinevzench", Main.MainInstance.beineench, False)
        readit("handgelenkevzench", Main.MainInstance.handgelenkeench, False)
        readit("haendevzench", Main.MainInstance.haendeench, False)
        readit("brustvzench", Main.MainInstance.brustench, False)
        readit("guertelvzench", Main.MainInstance.guertelench, False)
        readit("stiefelvzench", Main.MainInstance.stiefelench, False)
        readit("ring1vzench", Main.MainInstance.ring1ench, False)
        readit("ring2vzench", Main.MainInstance.ring2ench, False)
        readit("schmuck1vzench", Main.MainInstance.schmuck1ench, False)
        readit("schmuck2vzench", Main.MainInstance.schmuck2ench, False)
        readit("hauptvzench", Main.MainInstance.hauptench, False)
        readit("offvzench", Main.MainInstance.offench, False)
        readit("distanzvzench", Main.MainInstance.distanzench, False)

        readit2("kopfsocket1name", Main.MainInstance.kopfsocket1, False)
        readit2("schultersocket1name", Main.MainInstance.schultersocket1, False)
        readit2("halssocket1name", Main.MainInstance.halssocket1, False)
        readit2("rueckensocket1name", Main.MainInstance.rueckensocket1, False)
        readit2("beinesocket1name", Main.MainInstance.beinesocket1, False)
        readit2("handgelenkesocket1name", Main.MainInstance.Handgelenkesocket1, False)
        readit2("haendesocket1name", Main.MainInstance.haendesocket1, False)
        readit2("brustsocket1name", Main.MainInstance.brustsocket1, False)
        readit2("guertelsocket1name", Main.MainInstance.guertelsocket1, False)
        readit2("stiefelsocket1name", Main.MainInstance.stiefelsocket1, False)
        readit2("ring1socket1name", Main.MainInstance.Ring1socket1, False)
        readit2("ring2socket1name", Main.MainInstance.ring2socket1, False)
        readit2("hauptsocket1name", Main.MainInstance.Hauptsocket1, False)
        readit2("offsocket1name", Main.MainInstance.Offsocket1, False)
        readit2("distanzsocket1name", Main.MainInstance.Distanzsocket1, False)

        readit2("kopfsocket2name", Main.MainInstance.kopfsocket2, False)
        readit2("schultersocket2name", Main.MainInstance.schultersocket2, False)
        readit2("halssocket2name", Main.MainInstance.halssocket2, False)
        readit2("rueckensocket2name", Main.MainInstance.rueckensocket2, False)
        readit2("beinesocket2name", Main.MainInstance.beinesocket2, False)
        readit2("handgelenkesocket2name", Main.MainInstance.handgelenkesocket2, False)
        readit2("haendesocket2name", Main.MainInstance.haendesocket2, False)
        readit2("brustsocket2name", Main.MainInstance.brustsocket2, False)
        readit2("guertelsocket2name", Main.MainInstance.guertelsocket2, False)
        readit2("stiefelsocket2name", Main.MainInstance.stiefelsocket2, False)
        readit2("ring2socket2name", Main.MainInstance.ring2socket2, False)
        readit2("ring2socket2name", Main.MainInstance.ring2socket2, False)
        readit2("hauptsocket2name", Main.MainInstance.Hauptsocket2, False)
        readit2("offsocket2name", Main.MainInstance.Offsocket2, False)
        readit2("distanzsocket2name", Main.MainInstance.Distanzsocket2, False)

        readit2("kopfsocket3name", Main.MainInstance.kopfsocket3, False)
        readit2("schultersocket3name", Main.MainInstance.schultersocket3, False)
        readit2("halssocket3name", Main.MainInstance.halssocket3, False)
        readit2("rueckensocket3name", Main.MainInstance.rueckensocket3, False)
        readit2("beinesocket3name", Main.MainInstance.beinesocket3, False)
        readit2("handgelenkesocket3name", Main.MainInstance.Handgelenkesocket3, False)
        readit2("haendesocket3name", Main.MainInstance.haendesocket3, False)
        readit2("brustsocket3name", Main.MainInstance.brustsocket3, False)
        readit2("guertelsocket3name", Main.MainInstance.guertelsocket3, False)
        readit2("stiefelsocket3name", Main.MainInstance.stiefelsocket3, False)
        readit2("hauptsocket3name", Main.MainInstance.hauptsocket3, False)
        readit2("offsocket3name", Main.MainInstance.offsocket3, False)
        readit2("distanzsocket3name", Main.MainInstance.distanzsocket3, False)

        'Get correct effectnames
        Dim sajdaj As String = Main.MainInstance.kopfvzid.ToString()
        If Not Main.MainInstance.kopfvzid = 0 Then Main.MainInstance.kopfvz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.kopfvzid)
        If Not Main.MainInstance.schultervzid = 0 Then Main.MainInstance.schultervz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.schultervzid)
        If Not Main.MainInstance.halsvzid = 0 Then Main.MainInstance.halsvz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.halsvzid)
        If Not Main.MainInstance.rueckenvzid = 0 Then Main.MainInstance.rueckenvz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.rueckenvzid)
        If Not Main.MainInstance.beinevzid = 0 Then Main.MainInstance.beinevz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.beinevzid)
        If Not Main.MainInstance.handgelenkevzid = 0 Then Main.MainInstance.handgelenkevz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.handgelenkevzid)
        If Not Main.MainInstance.haendevzid = 0 Then Main.MainInstance.haendevz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.haendevzid)
        If Not Main.MainInstance.brustvzid = 0 Then Main.MainInstance.brustvz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.brustvzid)
        If Not Main.MainInstance.guertelvzid = 0 Then Main.MainInstance.guertelvz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.guertelvzid)
        If Not Main.MainInstance.stiefelvzid = 0 Then Main.MainInstance.stiefelvz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.stiefelvzid)
        If Not Main.MainInstance.ring1vzid = 0 Then Main.MainInstance.ring1vz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.ring1vzid)
        If Not Main.MainInstance.ring2vzid = 0 Then Main.MainInstance.ring2vz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.ring2vzid)
        If Not Main.MainInstance.schmuck1vzid = 0 Then Main.MainInstance.schmuck1vz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.schmuck1vzid)
        If Not Main.MainInstance.schmuck2vzid = 0 Then Main.MainInstance.schmuck2vz.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.schmuck2vzid)
        If Not Main.MainInstance.hauptvzid = 0 Then Main.MainInstance.hauptvzlabel2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.hauptvzid)
        If Not Main.MainInstance.offvzid = 0 Then Main.MainInstance.offvzlabel2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.offvzid)
        If Not Main.MainInstance.distanzvzid = 0 Then Main.MainInstance.distanzvzlabel2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.distanzvzid)

        If Not Main.MainInstance.kopfsocket1id = 0 Then Main.MainInstance.kopfsocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.kopfsocket1id)
        If Not Main.MainInstance.schultersocket1id = 0 Then Main.MainInstance.schultersocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.schultersocket1id)
        If Not Main.MainInstance.halssocket1id = 0 Then Main.MainInstance.halssocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.halssocket1id)
        If Not Main.MainInstance.rueckensocket1id = 0 Then Main.MainInstance.rueckensocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.rueckensocket1id)
        If Not Main.MainInstance.beinesocket1id = 0 Then Main.MainInstance.beinesocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.beinesocket1id)
        If Not Main.MainInstance.handgelenkesocket1id = 0 Then Main.MainInstance.Handgelenkesocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.handgelenkesocket1id)
        If Not Main.MainInstance.haendesocket1id = 0 Then Main.MainInstance.haendesocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.haendesocket1id)
        If Not Main.MainInstance.brustsocket1id = 0 Then Main.MainInstance.brustsocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.brustsocket1id)
        If Not Main.MainInstance.guertelsocket1id = 0 Then Main.MainInstance.guertelsocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.guertelsocket1id)
        If Not Main.MainInstance.stiefelsocket1id = 0 Then Main.MainInstance.stiefelsocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.stiefelsocket1id)
        If Not Main.MainInstance.ring1socket1id = 0 Then Main.MainInstance.Ring1socket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.ring1socket1id)
        If Not Main.MainInstance.ring2socket1id = 0 Then Main.MainInstance.ring2socket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.ring2socket1id)
        If Not Main.MainInstance.hauptsocket1id = 0 Then Main.MainInstance.Hauptsocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.hauptsocket1id)
        If Not Main.MainInstance.offsocket1id = 0 Then Main.MainInstance.Offsocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.offsocket1id)
        If Not Main.MainInstance.distanzsocket1id = 0 Then Main.MainInstance.Distanzsocket1.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.distanzsocket1id)

        If Not Main.MainInstance.kopfsocket2id = 0 Then Main.MainInstance.kopfsocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.kopfsocket2id)
        If Not Main.MainInstance.schultersocket2id = 0 Then Main.MainInstance.schultersocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.schultersocket2id)
        If Not Main.MainInstance.halssocket2id = 0 Then Main.MainInstance.halssocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.halssocket2id)
        If Not Main.MainInstance.rueckensocket2id = 0 Then Main.MainInstance.rueckensocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.rueckensocket2id)
        If Not Main.MainInstance.beinesocket2id = 0 Then Main.MainInstance.beinesocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.beinesocket2id)
        If Not Main.MainInstance.handgelenkesocket2id = 0 Then Main.MainInstance.handgelenkesocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.handgelenkesocket2id)
        If Not Main.MainInstance.haendesocket2id = 0 Then Main.MainInstance.haendesocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.haendesocket2id)
        If Not Main.MainInstance.brustsocket2id = 0 Then Main.MainInstance.brustsocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.brustsocket2id)
        If Not Main.MainInstance.guertelsocket2id = 0 Then Main.MainInstance.guertelsocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.guertelsocket2id)
        If Not Main.MainInstance.stiefelsocket2id = 0 Then Main.MainInstance.stiefelsocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.stiefelsocket2id)
        If Not Main.MainInstance.ring2socket2id = 0 Then Main.MainInstance.ring2socket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.ring2socket2id)
        If Not Main.MainInstance.ring2socket2id = 0 Then Main.MainInstance.ring2socket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.ring2socket2id)
        If Not Main.MainInstance.hauptsocket2id = 0 Then Main.MainInstance.Hauptsocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.hauptsocket2id)
        If Not Main.MainInstance.offsocket2id = 0 Then Main.MainInstance.Offsocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.offsocket2id)
        If Not Main.MainInstance.distanzsocket2id = 0 Then Main.MainInstance.Distanzsocket2.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.distanzsocket2id)

        If Not Main.MainInstance.kopfsocket3id = 0 Then Main.MainInstance.kopfsocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.kopfsocket3id)
        If Not Main.MainInstance.schultersocket3id = 0 Then Main.MainInstance.schultersocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.schultersocket3id)
        If Not Main.MainInstance.halssocket3id = 0 Then Main.MainInstance.halssocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.halssocket3id)
        If Not Main.MainInstance.rueckensocket3id = 0 Then Main.MainInstance.rueckensocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.rueckensocket3id)
        If Not Main.MainInstance.beinesocket3id = 0 Then Main.MainInstance.beinesocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.beinesocket3id)
        If Not Main.MainInstance.handgelenkesocket3id = 0 Then Main.MainInstance.Handgelenkesocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.handgelenkesocket3id)
        If Not Main.MainInstance.haendesocket3id = 0 Then Main.MainInstance.haendesocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.haendesocket3id)
        If Not Main.MainInstance.brustsocket3id = 0 Then Main.MainInstance.brustsocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.brustsocket3id)
        If Not Main.MainInstance.guertelsocket3id = 0 Then Main.MainInstance.guertelsocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.guertelsocket3id)
        If Not Main.MainInstance.stiefelsocket3id = 0 Then Main.MainInstance.stiefelsocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.stiefelsocket3id)
        If Not Main.MainInstance.hauptsocket3id = 0 Then Main.MainInstance.hauptsocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.hauptsocket3id)
        If Not Main.MainInstance.offsocket3id = 0 Then Main.MainInstance.offsocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.offsocket3id)
        If Not Main.MainInstance.distanzsocket3id = 0 Then Main.MainInstance.distanzsocket3.Text = runfunction.geteffectnameofeffectid(Main.MainInstance.distanzsocket3id)

        'Glyph Names

        readit("primeglyph1id", Main.MainInstance.primeglyph1, False)
        readit("primeglyph2id", Main.MainInstance.primeglyph2, False)
        readit("primeglyph3id", Main.MainInstance.primeglyph3, False)
        readit("majorglyph1id", Main.MainInstance.majorglyph1, False)
        readit("majorglyph2id", Main.MainInstance.majorglyph2, False)
        readit("majorglyph3id", Main.MainInstance.majorglyph3, False)
        readit("minorglyph1id", Main.MainInstance.minorglyph1, False)
        readit("minorglyph2id", Main.MainInstance.minorglyph2, False)
        readit("minorglyph3id", Main.MainInstance.minorglyph3, False)
        readit("secprimeglyph1id", Main.MainInstance.secprimeglyph1, False)
        readit("secprimeglyph2id", Main.MainInstance.secprimeglyph2, False)
        readit("secprimeglyph3id", Main.MainInstance.secprimeglyph3, False)
        readit("secmajorglyph1id", Main.MainInstance.secmajorglyph1, False)
        readit("secmajorglyph2id", Main.MainInstance.secmajorglyph2, False)
        readit("secmajorglyph3id", Main.MainInstance.secmajorglyph3, False)
        readit("secminorglyph1id", Main.MainInstance.secminorglyph1, False)
        readit("secminorglyph2id", Main.MainInstance.secminorglyph2, False)
        readit("secminorglyph3id", Main.MainInstance.secminorglyph3, False)

        readit("primeglyph1", Main.MainInstance.textprimeglyph1, False)
        readit("primeglyph2", Main.MainInstance.textprimeglyph2, False)
        readit("primeglyph3", Main.MainInstance.textprimeglyph3, False)
        readit("majorglyph1", Main.MainInstance.textmajorglyph1, False)
        readit("majorglyph2", Main.MainInstance.textmajorglyph2, False)
        readit("majorglyph3", Main.MainInstance.textmajorglyph3, False)
        readit("minorglyph1", Main.MainInstance.textminorglyph1, False)
        readit("minorglyph2", Main.MainInstance.textminorglyph2, False)
        readit("minorglyph3", Main.MainInstance.textminorglyph3, False)
        readit("secprimeglyph1", Main.MainInstance.sectextprimeglyph1, False)
        readit("secprimeglyph2", Main.MainInstance.sectextprimeglyph2, False)
        readit("secprimeglyph3", Main.MainInstance.sectextprimeglyph3, False)
        readit("secmajorglyph1", Main.MainInstance.sectextmajorglyph1, False)
        readit("secmajorglyph2", Main.MainInstance.sectextmajorglyph2, False)
        readit("secmajorglyph3", Main.MainInstance.sectextmajorglyph3, False)
        readit("secminorglyph1", Main.MainInstance.sectextminorglyph1, False)
        readit("secminorglyph2", Main.MainInstance.sectextminorglyph2, False)
        readit("secminorglyph3", Main.MainInstance.sectextminorglyph3, False)

        'Get correct glyph name

        If Not Main.MainInstance.primeglyph1 = "" Then Main.MainInstance.textprimeglyph1 = runfunction.getnamefromitemid(Main.MainInstance.primeglyph1)
        If Not Main.MainInstance.primeglyph2 = "" Then Main.MainInstance.textprimeglyph2 = runfunction.getnamefromitemid(Main.MainInstance.primeglyph2)
        If Not Main.MainInstance.primeglyph3 = "" Then Main.MainInstance.textprimeglyph3 = runfunction.getnamefromitemid(Main.MainInstance.primeglyph3)
        If Not Main.MainInstance.majorglyph1 = "" Then Main.MainInstance.textmajorglyph1 = runfunction.getnamefromitemid(Main.MainInstance.majorglyph1)
        If Not Main.MainInstance.majorglyph2 = "" Then Main.MainInstance.textmajorglyph2 = runfunction.getnamefromitemid(Main.MainInstance.majorglyph2)
        If Not Main.MainInstance.majorglyph3 = "" Then Main.MainInstance.textmajorglyph3 = runfunction.getnamefromitemid(Main.MainInstance.majorglyph3)
        If Not Main.MainInstance.minorglyph1 = "" Then Main.MainInstance.textminorglyph1 = runfunction.getnamefromitemid(Main.MainInstance.minorglyph1)
        If Not Main.MainInstance.minorglyph2 = "" Then Main.MainInstance.textminorglyph2 = runfunction.getnamefromitemid(Main.MainInstance.minorglyph2)
        If Not Main.MainInstance.minorglyph3 = "" Then Main.MainInstance.textminorglyph3 = runfunction.getnamefromitemid(Main.MainInstance.minorglyph3)
        If Not Main.MainInstance.secprimeglyph1 = "" Then Main.MainInstance.sectextprimeglyph1 = runfunction.getnamefromitemid(Main.MainInstance.secprimeglyph1)
        If Not Main.MainInstance.secprimeglyph2 = "" Then Main.MainInstance.sectextprimeglyph2 = runfunction.getnamefromitemid(Main.MainInstance.secprimeglyph2)
        If Not Main.MainInstance.secprimeglyph3 = "" Then Main.MainInstance.sectextprimeglyph3 = runfunction.getnamefromitemid(Main.MainInstance.secprimeglyph3)
        If Not Main.MainInstance.secmajorglyph1 = "" Then Main.MainInstance.sectextmajorglyph1 = runfunction.getnamefromitemid(Main.MainInstance.secmajorglyph1)
        If Not Main.MainInstance.secmajorglyph2 = "" Then Main.MainInstance.sectextmajorglyph2 = runfunction.getnamefromitemid(Main.MainInstance.secmajorglyph2)
        If Not Main.MainInstance.secmajorglyph3 = "" Then Main.MainInstance.sectextmajorglyph3 = runfunction.getnamefromitemid(Main.MainInstance.secmajorglyph3)
        If Not Main.MainInstance.secminorglyph1 = "" Then Main.MainInstance.sectextminorglyph1 = runfunction.getnamefromitemid(Main.MainInstance.secminorglyph1)
        If Not Main.MainInstance.secminorglyph2 = "" Then Main.MainInstance.sectextminorglyph2 = runfunction.getnamefromitemid(Main.MainInstance.secminorglyph2)
        If Not Main.MainInstance.secminorglyph3 = "" Then Main.MainInstance.sectextminorglyph3 = runfunction.getnamefromitemid(Main.MainInstance.secminorglyph3)

        readitINT("accountid", Main.MainInstance.accountid, False, True)
        readitINT("char_guid", Main.MainInstance.char_guid, False, True)
        readitINT("char_xp", Main.MainInstance.char_xp, False, True)
        readitINT("player_money", Main.MainInstance.player_money, False, True)
        readitINT("playerBytes", Main.MainInstance.playerBytes, False, True)
        readitINT("playerBytes2", Main.MainInstance.playerBytes2, False, True)
        readitINT("playerFlags", Main.MainInstance.playerFlags, False, True)
        readitDBL("position_x", Main.MainInstance.position_x, False)
        readitDBL("position_y", Main.MainInstance.position_y, False)
        readitDBL("position_z", Main.MainInstance.position_z, False)
        readitINT("map", Main.MainInstance.map, False, True)
        readitINT("instance_id", Main.MainInstance.instance_id, False, True)
        readit("instance_mode_mask", Main.MainInstance.instance_mode_mask, False)
        readitDBL("orientation", Main.MainInstance.orientation, False)
        readit("taximask", Main.MainInstance.taximask, False)
        readitINT("cinematic", Main.MainInstance.cinematic, False, True)
        readitINT("totaltime", Main.MainInstance.totaltime, False, True)
        readitINT("leveltime", Main.MainInstance.leveltime, False, True)
        readit("extra_flags", Main.MainInstance.extra_flags, False)
        readit("stable_slots", Main.MainInstance.stable_slots, False)
        readit("at_login", Main.MainInstance.at_login, False)
        readitINT("zone", Main.MainInstance.zone, False, True)
        readitINT("arenaPoints", Main.MainInstance.arenaPoints, False, True)
        readitINT("totalHonorPoints", Main.MainInstance.totalHonorPoints, False, True)
        readitINT("totalKills", Main.MainInstance.totalKills, False, True)
        readit("chosenTitle", Main.MainInstance.chosenTitle, False)
        readit("knownCurrencies", Main.MainInstance.knownCurrencies, False)
        readit("watchedFaction", Main.MainInstance.watchedFaction, False)
        readitINT("health", Main.MainInstance.health, False, True)
        readitINT("speccount", Main.MainInstance.speccount, False, True)
        readitINT("activespec", Main.MainInstance.activespec, False, True)
        readit("exploredZones", Main.MainInstance.exploredZones, False)
        readit("knownTitles", Main.MainInstance.knownTitles, False)
        readit("actionBars", Main.MainInstance.actionBars, False)
        readit("character_homebind", Main.MainInstance.character_homebind, False)
        'character_achievement Table

        string2list("character_achievement_list", Main.MainInstance.character_achievement_list)
        'character_action Table


        string2list("character_action_list", Main.MainInstance.character_action_list)

        string2list("character_inventory_list", Main.MainInstance.character_inventory_list)
        string2list("character_reputatuion_list", Main.MainInstance.character_reputatuion_list)
        string2list("character_skills_list", Main.MainInstance.character_skills_list)
        string2list("character_talent_list", Main.MainInstance.character_talent_list)

        readit("accountname", Main.MainInstance.accountname, False)
        readit("sha_pass_hash", Main.MainInstance.sha_pass_hash, False)
        readit("sessionkey", Main.MainInstance.sessionkey, False)
        readit("account_v", Main.MainInstance.account_v, False)
        readit("account_s", Main.MainInstance.account_s, False)
        readit("email", Main.MainInstance.email, False)
        readit("joindate", Main.MainInstance.joindate, False)
        readitINT("expansion", Main.MainInstance.expansion, False, True)
        readitINT("locale", Main.MainInstance.locale, False, True)

        readitINT("account_access_gmlevel", Main.MainInstance.account_access_gmlevel, False, True)
        readitINT("account_access_RealmID", Main.MainInstance.account_access_RealmID, False, True)

        'Item Pics

        readimage2("kopfpic", Main.MainInstance.kopfpic, False)
        readimage2("halspic", Main.MainInstance.Halspic, False)
        readimage2("schulterpic", Main.MainInstance.Schulterpic, False)
        readimage2("wappenrockpic", Main.MainInstance.Wappenrockpic, False)
        readimage2("hemdpic", Main.MainInstance.Hemdpic, False)
        readimage2("rueckenpic", Main.MainInstance.Rueckenpic, False)
        readimage2("handgelenkepic", Main.MainInstance.Handgelenkepic, False)
        readimage2("haendepic", Main.MainInstance.Haendepic, False)
        readimage2("guertelpic", Main.MainInstance.Guertelpic, False)
        readimage2("stiefelpic", Main.MainInstance.Stiefelpic, False)
        readimage2("beinepic", Main.MainInstance.Beinepic, False)
        readimage2("ring1pic", Main.MainInstance.Ring1pic, False)
        readimage2("ring2pic", Main.MainInstance.Ring2pic, False)
        readimage2("schmuck1pic", Main.MainInstance.Schmuck1pic, False)
        readimage2("schmuck2pic", Main.MainInstance.Schmuck2pic, False)
        readimage2("hauptpic", Main.MainInstance.Hauptpic, False)
        readimage2("offpic", Main.MainInstance.Offpic, False)
        readimage2("distanzpic", Main.MainInstance.Distanzpic, False)
        readimage2("brustpic", Main.MainInstance.Brustpic, False)

        'Glyph Pics

        readimage("prim1pic", Main.MainInstance.glyphpic1, False)
        readimage("prim2pic", Main.MainInstance.glyphpic2, False)
        readimage("prim3pic", Main.MainInstance.glyphpic3, False)
        readimage("erheb1pic", Main.MainInstance.glyphpic4, False)
        readimage("erheb2pic", Main.MainInstance.glyphpic5, False)
        readimage("erheb3pic", Main.MainInstance.glyphpic6, False)
        readimage("gering1pic", Main.MainInstance.glyphpic7, False)
        readimage("gering2pic", Main.MainInstance.glyphpic8, False)
        readimage("gering3pic", Main.MainInstance.glyphpic9, False)

        readimage("secprim1pic", Main.MainInstance.secglyphpic1, False)
        readimage("secprim2pic", Main.MainInstance.secglyphpic2, False)
        readimage("secprim3pic", Main.MainInstance.secglyphpic3, False)
        readimage("secerheb1pic", Main.MainInstance.secglyphpic4, False)
        readimage("secerheb2pic", Main.MainInstance.secglyphpic5, False)
        readimage("secerheb3pic", Main.MainInstance.secglyphpic6, False)
        readimage("secgering1pic", Main.MainInstance.secglyphpic7, False)
        readimage("secgering2pic", Main.MainInstance.secglyphpic8, False)
        readimage("secgering3pic", Main.MainInstance.secglyphpic9, True)

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
            Case Else

        End Select
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
            Case Else

        End Select
        Debug.WriteLine(Main.MainInstance.position_x)
    End Sub

    Public Sub readit(ByVal schlüssel As String, ByRef nonsense As String, ByVal lastone As Boolean,
                      Optional ByVal isint As Boolean = False)

        If lastone = False Then
            Try
                Dim xXquellcodeyx88 As String = fileextract
                Dim xXanfangyx88 As String = schlüssel & "="
                Dim xXendeyx88 As String = "<++>"
                Dim xXquellcodeSplityx88 As String
                xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                Dim s As String = xXquellcodeSplityx88
                Dim b() As Byte = Encoding.Default.GetBytes(s)
                Dim s1 As String = Encoding.UTF8.GetString(b)
                If s1 = "Platz leer" Then s1 = "-"
                nonsense = s1

            Catch ex As Exception
                nonsense = ""
            End Try


        Else

        End If
    End Sub

    Public Sub readitINT(ByVal schlüssel As String, ByRef nonsense As Integer, ByVal lastone As Boolean,
                         Optional ByVal isint As Boolean = False)

        If lastone = False Then
            Try
                Dim xXquellcodeyx88 As String = fileextract
                Dim xXanfangyx88 As String = schlüssel & "="
                Dim xXendeyx88 As String = "<++>"
                Dim xXquellcodeSplityx88 As String
                xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                Dim s As String = xXquellcodeSplityx88
                Dim b() As Byte = Encoding.Default.GetBytes(s)
                Dim s1 As String = Encoding.UTF8.GetString(b)

                nonsense = CInt(s1)

            Catch ex As Exception
                nonsense = 0
            End Try


        Else

        End If
    End Sub

    Public Sub readitDBL(ByVal schlüssel As String, ByRef nonsense As Double, ByVal lastone As Boolean,
                      Optional ByVal isint As Boolean = False)

        If lastone = False Then
            Try
                Dim xXquellcodeyx88 As String = fileextract
                Dim xXanfangyx88 As String = schlüssel & "="
                Dim xXendeyx88 As String = "<++>"
                Dim xXquellcodeSplityx88 As String
                xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                Dim s As String = xXquellcodeSplityx88
                Dim b() As Byte = Encoding.Default.GetBytes(s)
                Dim s1 As String = Encoding.UTF8.GetString(b)

                nonsense = CDbl(s1)

            Catch ex As Exception
                nonsense = 0
            End Try


        Else

        End If
    End Sub

    Private Function list2string(ByVal xlist As List(Of String)) As String
        Try
            Dim builder As StringBuilder = New StringBuilder()
            For Each val As String In xlist
                builder.Append(val).Append("|")
            Next

            ' Convert to string.
            Return builder.ToString()
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub string2list(ByVal schlüssel As String, ByRef dieliste As List(Of String))
        Try

            Dim xXquellcodeyx88 As String = fileextract
            Dim xXanfangyx88 As String = schlüssel & "="
            Dim xXendeyx88 As String = "<++>"
            Dim xXquellcodeSplityx88 As String
            xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
            xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
            Dim s As String = xXquellcodeSplityx88
            Dim b() As Byte = Encoding.Default.GetBytes(s)
            Dim s1 As String = Encoding.UTF8.GetString(b)

            Dim stringlist As String() = s1.Split("|"c)
            Dim position As Integer = 0
            Dim xlist As List(Of String) = New List(Of String)
            Do
                Try

                    Dim temp As String = stringlist(position)
                    If Not temp = "" Then xlist.Add(temp)

                    position += 1
                Catch ex As Exception
                    Exit Do
                End Try
            Loop
            dieliste = xlist
        Catch ex As Exception
            Dim leerelist As List(Of String) = New List(Of String)
            dieliste = leerelist
        End Try
    End Sub

    Public Sub readit2(ByVal schlüssel As String, ByRef nonsense As Label, ByVal lastone As Boolean)
        If lastone = False Then


            Dim xXquellcodeyx88 As String = fileextract
            Dim xXanfangyx88 As String = schlüssel & "="
            Dim xXendeyx88 As String = "<++>"
            Dim xXquellcodeSplityx88 As String
            xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
            xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
            nonsense.Text = xXquellcodeSplityx88
            Dim s As String = xXquellcodeSplityx88
            Dim b() As Byte = Encoding.Default.GetBytes(s)
            Dim s1 As String = Encoding.UTF8.GetString(b)
            If s1 = "" Then
                nonsense.Visible = False
            ElseIf s1 = "-" Then

                nonsense.Visible = False
            Else
                nonsense.Visible = True
            End If
            If s1 = "Platz leer" Then s1 = "-"
            nonsense.Text = s1
            Application.DoEvents()
        Else

        End If
    End Sub

    Public Sub readimage(ByVal schlüssel As String, ByRef nonsense As Image, ByVal lastone As Boolean)
        Try
            If lastone = False Then


                Dim xXquellcodeyx88 As String = fileextract
                Dim xXanfangyx88 As String = schlüssel & "="
                Dim xXendeyx88 As String = "<++>"
                Dim xXquellcodeSplityx88 As String
                xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                nonsense = ConvertStringToImage(xXquellcodeSplityx88)
            Else

                Dim xXquellcodeyx88 As String = fileextract
                Dim xXanfangyx88 As String = schlüssel & "="
                Dim xXendeyx88 As String = "<++>"
                Dim xXquellcodeSplityx88 As String
                xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                nonsense = ConvertStringToImage(xXquellcodeSplityx88)

                Application.DoEvents()
                '  MsgBox("Template erfolgreich geladen!", MsgBoxStyle.OkOnly, "Hinweis")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub readimage2(ByVal schlüssel As String, ByRef nonsense As PictureBox, ByVal lastone As Boolean)
        Try
            If lastone = False Then


                Dim xXquellcodeyx88 As String = fileextract
                Dim xXanfangyx88 As String = schlüssel & "="
                Dim xXendeyx88 As String = "<++>"
                Dim xXquellcodeSplityx88 As String
                xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                nonsense.Image = ConvertStringToImage(xXquellcodeSplityx88)
            Else

                Dim xXquellcodeyx88 As String = fileextract
                Dim xXanfangyx88 As String = schlüssel & "="
                Dim xXendeyx88 As String = "<++>"
                Dim xXquellcodeSplityx88 As String
                xXquellcodeSplityx88 = Split(xXquellcodeyx88, xXanfangyx88, 5)(1)
                xXquellcodeSplityx88 = Split(xXquellcodeSplityx88, xXendeyx88, 6)(0)
                nonsense.Image = ConvertStringToImage(xXquellcodeSplityx88)


                Application.DoEvents()
                MsgBox("Template erfolgreich geladen!", MsgBoxStyle.OkOnly, "Hinweis")
            End If
        Catch ex As Exception

        End Try
    End Sub


    Public Function Image2ByteArray(ByVal Bild As Image, ByVal Bildformat As ImageFormat) As Byte()

        Try
            Dim MS As New MemoryStream
            Bild.Save(MS, Bildformat)
            MS.Flush()
            Return MS.ToArray
        Catch ex As Exception
            Dim MS As New MemoryStream
            My.Resources.empty.Save(MS, Bildformat)
            MS.Flush()
            Return MS.ToArray
        End Try
    End Function

    Private Sub prepare_new_dataset(ByVal dataset As String)
        My.Settings.savecontent = My.Settings.savecontent & "<startdataset" & dataset & ">"
        My.Settings.Save()
    End Sub

    Private Sub writeit(ByVal schlüssel As String, ByVal wert As String, ByVal lastone As Boolean)


        My.Settings.savecontent = My.Settings.savecontent & schlüssel & "=" & wert.ToString & "<++>"
        If lastone = True Then
            My.Settings.Save()
        End If
    End Sub

    Private Sub end_dataset(ByVal dataset As String)
        My.Settings.savecontent = My.Settings.savecontent & "<enddataset" & dataset & ">"
        My.Settings.Save()
    End Sub

    Private Sub nowwrite()
        Using fs As New FileStream(writepath, FileMode.OpenOrCreate, FileAccess.Write)
            Dim w As StreamWriter = New StreamWriter(fs)
            Dim sText As String = "This file was created with CharImport, which is developed by Alcanmage/megasus." & vbNewLine & "<<timestamp>>" & GetTimestamp(Date.UtcNow) & "<</timestamp>>" & vbNewLine & "<<requires>>" & Starter.required_template_version.ToString & "<</requires>>" & vbNewLine & "<<datasets>>" & Main.MainInstance.datasets & "<</datasets>> <<importmode>>" & Main.MainInstance.progressmode.ToString & "<</importmode>>" & My.Settings.savecontent
            Dim nBytes() As Byte = System.Text.Encoding.Default.GetBytes(sText)
            w.WriteLine(BytesToHexString(nBytes))
            w.Close()
        End Using
    End Sub
    Public Shared Function BytesToHexString(ByVal bytes() As Byte) As String
        Dim sb As New StringBuilder(bytes.Length * 2)
        For Each b As Byte In bytes
            sb.AppendFormat("{0:X2}", b)
        Next b
        Return sb.ToString()
    End Function
    Public Shared Function GetTimestamp(ByVal FromDateTime As DateTime) As Integer
        Dim spanne As TimeSpan = FromDateTime - #1/1/1970#
        Return CType(Math.Abs(spanne.TotalSeconds()), Integer)
    End Function

    Public Shared Function GetDateFromTimestamp(ByVal unixTimestamp As Integer) As DateTime
        If unixTimestamp = 0 Then Return #1/1/1970#
        Dim Span As TimeSpan = New TimeSpan(0, 0, unixTimestamp)
        Dim startDate As DateTime = #1/1/1970#
        Return Startdate.Add(Span)
    End Function
    Public Function ByteArray2Image(ByVal ByAr() As Byte) As Image
        Dim img As Image
        Dim MS As New MemoryStream(ByAr)
        Try
            img = Image.FromStream(MS)
        Catch ex As Exception
            Return Nothing
        End Try

        Return img
    End Function

    Public Function StringToByteArray(ByRef str As String) As Byte()
        Return Convert.FromBase64String(str)
    End Function

    Public Function ByteArrayToString(ByRef Barr() As Byte) As String
        Return Convert.ToBase64String(Barr)
    End Function

    '------------------Text in Bytearray und zurück-------------------
    Public Function TextStringToByteArray(ByRef str As String) As Byte()
        Dim enc As Encoding = Encoding.Default

        Return enc.GetBytes(str)
    End Function

    Public Shared Function ConvertImageToString(ByVal bild As Image) As String
        Dim Result As String = String.Empty


        Try

            Dim img As Image = bild
            Using ms As MemoryStream = New MemoryStream
                img.Save(ms, img.RawFormat)
                Dim Bytes() As Byte = ms.ToArray()
                Result = Convert.ToBase64String(Bytes)
            End Using

        Catch ex As Exception

        End Try

        Return Result
    End Function

    Public Shared Function ConvertStringToImage(ByVal Base64String As String) As Image
        Dim img As Image = Nothing
        If Base64String Is Nothing Then

        Else
            Try
                Dim Bytes() As Byte = Convert.FromBase64String(Base64String)
                img = Image.FromStream(New MemoryStream(Bytes))
            Catch ex As Exception

            End Try
        End If
        Return img
    End Function

    Public Function ByteArrayToTextString(ByRef Barr() As Byte) As String
        Try
            Dim enc As Encoding = Encoding.Default

            Return enc.GetString(Barr)
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
