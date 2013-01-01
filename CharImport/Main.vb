'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* The form Main provides an overview about the items of the character
'*
'* Developed by Alcanmage/megasus

Imports System.Globalization
Imports MySql.Data.MySqlClient
Imports System.Threading

Public Class Main
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN
    'Mid(text, 1, 18) Gibt den Text von "text" zwischen Zeichen 1 bis 18 aus
    '############################## DEKLARATION DER IDs
    Public readyforreturn As Boolean = False
    Public battlenet_region As String
    Public effectname_dt As New DataTable
    Public itemname_dt As New DataTable
    'eu/us...
    Public armoryrun As Boolean = False
    Public xpac As Integer
    Public realmname As String
    Public characterdbname As String
    Public GLOBALconn As New MySqlConnection
    Public GLOBALconnRealmd As New MySqlConnection
    Public realmdname As String = ""
    Public getfromarmoryfirst As Boolean = False
    Public startcond As Integer
    Public homebind_map As String
    Public homebind_zone As String
    Public homebind_posx As String
    Public homebind_posy As String
    Public homebind_posz As String
    Public arcemu_pass As String
    Public nowgoon As Boolean = True
    Public tableschema As String = ""
    Public nowexit As Boolean = False
    Public tmplpath As String
    Public showstarter As Boolean = False
    Public trashvalue As Integer
    Public arcemu_gmlevel As String = ""
    ' Dim armoryevent As New prozedur_armory
    Dim SQLConnection As MySqlConnection = New MySqlConnection
    Public ServerStringCheck As String = ""
    Public progressmode As Integer
    Public datacharname As String = ""
    Public ServerString As String = ""
    Public ServerStringRealmd As String = ""
    Public ServerStringInfo As String = ""
    Public emptylist As List(Of String) = New List(Of String)
    Public talentlist As List(Of String) = New List(Of String)
    Public char_race As Integer
    Public char_class As Integer
    Public char_gender As Integer
    Public char_level As Integer
    Dim ciu As New CIUFile
    Public char_name As String
    Public specialskills As List(Of String) = New List(Of String)
    Public specialspells As List(Of String) = New List(Of String)
    Public importmode As Integer
    Public outputcore As String
    Public linklist As List(Of String) = New List(Of String)
    Public errorcount As Integer
    Public overview As Boolean
    Public charcount As Integer
    Public datasets As Integer
    Public coreguid As String
    Public accounts2create As List(Of String) = New List(Of String)
    Public cuisets As Integer
    Public ausgangsformat As Integer
    Public arcemu_talentpoints As String
    'Lists
    Public glyphlist As List(Of String) = New List(Of String)
    Public itemlist As List(Of String) = New List(Of String)
    Public gemlist As List(Of String) = New List(Of String)
    Public vzlist As List(Of String) = New List(Of String)
    Public charopt As List(Of String) = New List(Of String)
    Public charenchlist As List(Of String) = New List(Of String)
    Public character_spells As List(Of String) = New List(Of String)
    Public character_queststatus As List(Of String) = New List(Of String)
    'Characters Table
    Public accountid As Integer
    Public char_guid As Integer
    Public char_xp As Integer
    Public player_money As Integer
    Public playerBytes As Integer
    Public playerBytes2 As Integer
    Public playerFlags As Integer
    Public position_x As String
    Public position_y As String
    Public position_z As String
    Public map As Integer
    Public instance_id As Integer
    Public instance_mode_mask As String
    Public orientation As String
    Public taximask As String
    Public cinematic As Integer
    Public totaltime As Integer
    Public leveltime As Integer
    Public extra_flags As String
    Public stable_slots As String
    Public at_login As String
    Public zone As Integer
    Public arenaPoints As Integer
    Public totalHonorPoints As Integer
    Public totalKills As Integer
    Public chosenTitle As String
    Public knownCurrencies As String
    Public watchedFaction As String
    Public health As Integer
    Public speccount As Integer
    Public activespec As Integer
    Public exploredZones As String
    Public knownTitles As String
    Public actionBars As String
    Public arcemu_action1 As String = ""
    Public arcemu_action2 As String = ""
    Public finished_quests As String = ""
    Public custom_faction As String = ""
    'character_achievement Table
    Public character_achievement_list As List(Of String)
    'character_action Table
    Public character_action_list As List(Of String)

    Public character_homebind As String = ""
    Public character_inventoryzero_list As List(Of String)
    Public character_inventory_list As List(Of String)
    Public character_reputatuion_list As List(Of String)
    Public character_skills_list As List(Of String)
    Public character_talent_list As List(Of String)


    Public accountname As String
    Public sha_pass_hash As String
    Public sessionkey As String
    Public account_v As String
    Public account_s As String
    Public email As String
    Public joindate As String
    Public expansion As Integer
    Public locale As Integer

    Public account_access_gmlevel As Integer
    Public account_access_RealmID As Integer

    Public anzahldurchlaufe As Integer

    Public kopfid As Integer
    Public kopfsocket1id As Integer
    Public kopfsocket2id As Integer
    Public kopfsocket3id As Integer
    Public kopfvzid As Integer
    Public kopfname As String = ""
    Public kopfench As String

    Public talentstring As String = ""
    Public charclass As String = ""
    Public halsid As Integer
    Public halssocket1id As Integer
    Public halssocket2id As Integer
    Public halssocket3id As Integer
    Public halsvzid As Integer
    Public halsname As String = ""
    Public halsench As String

    Public schulterid As Integer
    Public schultersocket1id As Integer
    Public schultersocket2id As Integer
    Public schultersocket3id As Integer
    Public schultervzid As Integer
    Public schultername As String = ""
    Public schulterench As String

    Public rueckenid As Integer
    Public rueckensocket1id As Integer
    Public rueckensocket2id As Integer
    Public rueckensocket3id As Integer
    Public rueckenvzid As Integer
    Public rueckenname As String = ""
    Public rueckenench As String

    Public hemdid As Integer
    Public hemdname As String = ""
    Public hemdench As String

    Public wappenrockid As Integer
    Public wappenrockname As String = ""
    Public wappenrockench As String

    Public brustid As Integer
    Public brustsocket1id As Integer
    Public brustsocket2id As Integer
    Public brustsocket3id As Integer
    Public brustvzid As Integer
    Public brustname As String = ""
    Public brustench As String

    Public handgelenkeid As Integer
    Public handgelenkesocket1id As Integer
    Public handgelenkesocket2id As Integer
    Public handgelenkesocket3id As Integer
    Public handgelenkevzid As Integer
    Public handgelenkename As String = ""
    Public handgelenkeench As String

    Public hauptid As Integer
    Public hauptsocket1id As Integer
    Public hauptsocket2id As Integer
    Public hauptsocket3id As Integer
    Public hauptvzid As Integer
    Public hauptname As String = ""
    Public hauptench As String

    Public offid As Integer
    Public offsocket1id As Integer
    Public offsocket2id As Integer
    Public offsocket3id As Integer
    Public offvzid As Integer
    Public offname As String = ""
    Public offench As String

    Public distanzid As Integer
    Public distanzsocket1id As Integer
    Public distanzsocket2id As Integer
    Public distanzsocket3id As Integer
    Public distanzvzid As Integer
    Public distanzname As String = ""
    Public distanzench As String

    Public haendeid As Integer
    Public haendesocket1id As Integer
    Public haendesocket2id As Integer
    Public haendesocket3id As Integer
    Public haendevzid As Integer
    Public haendename As String = ""
    Public haendeench As String

    Public guertelid As Integer
    Public guertelsocket1id As Integer
    Public guertelsocket2id As Integer
    Public guertelsocket3id As Integer
    Public guertelvzid As Integer
    Public guertelname As String = ""
    Public guertelench As String
    Public guertelschnalle As Integer

    Public beineid As Integer
    Public beinesocket1id As Integer
    Public beinesocket2id As Integer
    Public beinesocket3id As Integer
    Public beinevzid As Integer
    Public beinename As String = ""
    Public beineench As String

    Public stiefelid As Integer
    Public stiefelsocket1id As Integer
    Public stiefelsocket2id As Integer
    Public stiefelsocket3id As Integer
    Public stiefelvzid As Integer
    Public stiefelname As String = ""
    Public stiefelench As String

    Public ring1id As Integer
    Public ring1socket1id As Integer
    Public ring1socket2id As Integer
    Public ring1socket3id As Integer
    Public ring1vzid As Integer
    Public ring1name As String = ""
    Public ring1ench As String

    Public ring2id As Integer
    Public ring2socket1id As Integer
    Public ring2socket2id As Integer
    Public ring2socket3id As Integer
    Public ring2vzid As Integer
    Public ring2name As String = ""
    Public ring2ench As String

    Public schmuck1id As Integer
    Public schmuck1socket1id As Integer
    Public schmuck1socket2id As Integer
    Public schmuck1socket3id As Integer
    Public schmuck1vzid As Integer
    Public schmuck1name As String = ""
    Public schmuck1ench As String

    Public schmuck2id As Integer
    Public schmuck2socket1id As Integer
    Public schmuck2socket2id As Integer
    Public schmuck2socket3id As Integer
    Public schmuck2vzid As Integer
    Public schmuck2name As String = ""
    Public schmuck2ench As String

    Public levelid As Integer

    Public primeglyph1 As String = ""
    Public primeglyph2 As String = ""
    Public primeglyph3 As String = ""

    Public majorglyph1 As String = ""
    Public majorglyph2 As String = ""
    Public majorglyph3 As String = ""

    Public minorglyph1 As String = ""
    Public minorglyph2 As String = ""
    Public minorglyph3 As String = ""

    Public textprimeglyph1 As String = ""
    Public textprimeglyph2 As String = ""
    Public textprimeglyph3 As String = ""

    Public textmajorglyph1 As String = ""
    Public textmajorglyph2 As String = ""
    Public textmajorglyph3 As String = ""

    Public textminorglyph1 As String = ""
    Public textminorglyph2 As String = ""
    Public textminorglyph3 As String = ""

    Public glyphpic1 As Image = My.Resources.empty
    Public glyphpic2 As Image = My.Resources.empty
    Public glyphpic3 As Image = My.Resources.empty
    Public glyphpic4 As Image = My.Resources.empty
    Public glyphpic5 As Image = My.Resources.empty
    Public glyphpic6 As Image = My.Resources.empty
    Public glyphpic7 As Image = My.Resources.empty
    Public glyphpic8 As Image = My.Resources.empty
    Public glyphpic9 As Image = My.Resources.empty


    'secundär

    Public secprimeglyph1 As String = ""
    Public secprimeglyph2 As String = ""
    Public secprimeglyph3 As String = ""

    Public secmajorglyph1 As String = ""
    Public secmajorglyph2 As String = ""
    Public secmajorglyph3 As String = ""

    Public secminorglyph1 As String = ""
    Public secminorglyph2 As String = ""
    Public secminorglyph3 As String = ""

    Public sectextprimeglyph1 As String = ""
    Public sectextprimeglyph2 As String = ""
    Public sectextprimeglyph3 As String = ""

    Public sectextmajorglyph1 As String = ""
    Public sectextmajorglyph2 As String = ""
    Public sectextmajorglyph3 As String = ""

    Public sectextminorglyph1 As String = ""
    Public sectextminorglyph2 As String = ""
    Public sectextminorglyph3 As String = ""

    Public secglyphpic1 As Image = My.Resources.empty
    Public secglyphpic2 As Image = My.Resources.empty
    Public secglyphpic3 As Image = My.Resources.empty
    Public secglyphpic4 As Image = My.Resources.empty
    Public secglyphpic5 As Image = My.Resources.empty
    Public secglyphpic6 As Image = My.Resources.empty
    Public secglyphpic7 As Image = My.Resources.empty
    Public secglyphpic8 As Image = My.Resources.empty
    Public secglyphpic9 As Image = My.Resources.empty


    Public list As New List(Of String)


    Public quelltext As String = ""
    Public talentpage As String = ""
    Public sectalentpage As String = ""

    Sub setallempty()
        '  My.Settings.savecontent = "" ' ???? 29/07 should fix: (Load char from db > load char from armory > store it > import template = first character)
        ' My.Settings.Save()
        armoryrun = False
        realmname = ""
        emptylist = New List(Of String)
        talentlist = New List(Of String)
        char_race = vbEmpty
        char_class = vbEmpty
        char_gender = vbEmpty
        char_level = vbEmpty

        getfromarmoryfirst = False
        tableschema = ""

        char_name = ""
        specialskills = New List(Of String)
        specialspells = New List(Of String)
        importmode = vbEmpty
        outputcore = ""
        battlenet_region = ""
        errorcount = vbEmpty
        overview = False
        charcount = vbEmpty
        '    datasets = vbEmpty

        accounts2create = New List(Of String)
        arcemu_action1 = ""
        arcemu_action2 = ""
        arcemu_talentpoints = ""
        arcemu_pass = ""
        ServerStringCheck = ""
        finished_quests = ""
        'Lists
        glyphlist = New List(Of String)
        itemlist = New List(Of String)
        gemlist = New List(Of String)
        vzlist = New List(Of String)
        charopt = New List(Of String)
        charenchlist = New List(Of String)
        nowgoon = True
        'Characters Table
        accountid = vbEmpty
        char_guid = vbEmpty
        char_xp = vbEmpty
        player_money = vbEmpty
        playerBytes = vbEmpty
        playerBytes2 = vbEmpty
        playerFlags = vbEmpty
        position_x = ""
        position_y = ""
        position_z = ""
        map = vbEmpty
        instance_id = vbEmpty
        instance_mode_mask = ""
        orientation = ""
        taximask = ""
        cinematic = vbEmpty
        totaltime = vbEmpty
        leveltime = vbEmpty
        extra_flags = ""
        stable_slots = ""
        at_login = ""
        zone = vbEmpty
        arenaPoints = vbEmpty
        totalHonorPoints = vbEmpty
        totalKills = vbEmpty
        chosenTitle = ""
        knownCurrencies = ""
        watchedFaction = ""
        health = vbEmpty
        speccount = vbEmpty
        activespec = vbEmpty
        exploredZones = ""
        knownTitles = ""
        actionBars = ""
        custom_faction = ""
        arcemu_gmlevel = ""
        'character_achievement Table
        character_achievement_list = New List(Of String)
        'character_action Table
        character_action_list = New List(Of String)
        character_inventoryzero_list = New List(Of String)
        character_homebind = ""
        character_inventory_list = New List(Of String)
        character_reputatuion_list = New List(Of String)
        character_skills_list = New List(Of String)
        character_talent_list = New List(Of String)
        character_spells = New List(Of String)
        character_queststatus = New List(Of String)

        accountname = ""
        sha_pass_hash = ""
        sessionkey = ""
        account_v = ""
        account_s = ""
        email = ""
        joindate = ""
        expansion = vbEmpty
        locale = vbEmpty

        account_access_gmlevel = vbEmpty
        account_access_RealmID = vbEmpty

        nowexit = False
        kopfid = vbEmpty
        kopfsocket1id = vbEmpty
        kopfsocket2id = vbEmpty
        kopfsocket3id = vbEmpty
        kopfvzid = vbEmpty
        kopfname = ""
        kopfench = ""

        talentstring = ""
        charclass = ""
        halsid = vbEmpty
        halssocket1id = vbEmpty
        halssocket2id = vbEmpty
        halssocket3id = vbEmpty
        halsvzid = vbEmpty
        halsname = ""
        halsench = ""

        schulterid = vbEmpty
        schultersocket1id = vbEmpty
        schultersocket2id = vbEmpty
        schultersocket3id = vbEmpty
        schultervzid = vbEmpty
        schultername = ""
        schulterench = ""

        rueckenid = vbEmpty
        rueckensocket1id = vbEmpty
        rueckensocket2id = vbEmpty
        rueckensocket3id = vbEmpty
        rueckenvzid = vbEmpty
        rueckenname = ""
        rueckenench = ""

        hemdid = vbEmpty
        hemdname = ""
        hemdench = ""

        wappenrockid = vbEmpty
        wappenrockname = ""
        wappenrockench = ""

        brustid = vbEmpty
        brustsocket1id = vbEmpty
        brustsocket2id = vbEmpty
        brustsocket3id = vbEmpty
        brustvzid = vbEmpty
        brustname = ""
        brustench = ""

        handgelenkeid = vbEmpty
        handgelenkesocket1id = vbEmpty
        handgelenkesocket2id = vbEmpty
        handgelenkesocket3id = vbEmpty
        handgelenkevzid = vbEmpty
        handgelenkename = ""
        handgelenkeench = ""

        hauptid = vbEmpty
        hauptsocket1id = vbEmpty
        hauptsocket2id = vbEmpty
        hauptsocket3id = vbEmpty
        hauptvzid = vbEmpty
        hauptname = ""
        hauptench = ""

        offid = vbEmpty
        offsocket1id = vbEmpty
        offsocket2id = vbEmpty
        offsocket3id = vbEmpty
        offvzid = vbEmpty
        offname = ""
        offench = ""

        distanzid = vbEmpty
        distanzsocket1id = vbEmpty
        distanzsocket2id = vbEmpty
        distanzsocket3id = vbEmpty
        distanzvzid = vbEmpty
        distanzname = ""
        distanzench = ""

        haendeid = vbEmpty
        haendesocket1id = vbEmpty
        haendesocket2id = vbEmpty
        haendesocket3id = vbEmpty
        haendevzid = vbEmpty
        haendename = ""
        haendeench = ""

        guertelid = vbEmpty
        guertelsocket1id = vbEmpty
        guertelsocket2id = vbEmpty
        guertelsocket3id = vbEmpty
        guertelvzid = vbEmpty
        guertelname = ""
        guertelench = ""
        guertelschnalle = 0

        beineid = vbEmpty
        beinesocket1id = vbEmpty
        beinesocket2id = vbEmpty
        beinesocket3id = vbEmpty
        beinevzid = vbEmpty
        beinename = ""
        beineench = ""

        stiefelid = vbEmpty
        stiefelsocket1id = vbEmpty
        stiefelsocket2id = vbEmpty
        stiefelsocket3id = vbEmpty
        stiefelvzid = vbEmpty
        stiefelname = ""
        stiefelench = ""

        ring1id = vbEmpty
        ring1socket1id = vbEmpty
        ring1socket2id = vbEmpty
        ring1socket3id = vbEmpty
        ring1vzid = vbEmpty
        ring1name = ""
        ring1ench = ""

        ring2id = vbEmpty
        ring2socket1id = vbEmpty
        ring2socket2id = vbEmpty
        ring2socket3id = vbEmpty
        ring2vzid = vbEmpty
        ring2name = ""
        ring2ench = ""

        schmuck1id = vbEmpty
        schmuck1socket1id = vbEmpty
        schmuck1socket2id = vbEmpty
        schmuck1socket3id = vbEmpty
        schmuck1vzid = vbEmpty
        schmuck1name = ""
        schmuck1ench = ""

        schmuck2id = vbEmpty
        schmuck2socket1id = vbEmpty
        schmuck2socket2id = vbEmpty
        schmuck2socket3id = vbEmpty
        schmuck2vzid = vbEmpty
        schmuck2name = ""
        schmuck2ench = ""

        levelid = vbEmpty

        primeglyph1 = ""
        primeglyph2 = ""
        primeglyph3 = ""

        majorglyph1 = ""
        majorglyph2 = ""
        majorglyph3 = ""

        minorglyph1 = ""
        minorglyph2 = ""
        minorglyph3 = ""

        textprimeglyph1 = ""
        textprimeglyph2 = ""
        textprimeglyph3 = ""

        textmajorglyph1 = ""
        textmajorglyph2 = ""
        textmajorglyph3 = ""

        textminorglyph1 = ""
        textminorglyph2 = ""
        textminorglyph3 = ""

        glyphpic1 = My.Resources.empty
        glyphpic2 = My.Resources.empty
        glyphpic3 = My.Resources.empty
        glyphpic4 = My.Resources.empty
        glyphpic5 = My.Resources.empty
        glyphpic6 = My.Resources.empty
        glyphpic7 = My.Resources.empty
        glyphpic8 = My.Resources.empty
        glyphpic9 = My.Resources.empty


        'secundär

        secprimeglyph1 = ""
        secprimeglyph2 = ""
        secprimeglyph3 = ""

        secmajorglyph1 = ""
        secmajorglyph2 = ""
        secmajorglyph3 = ""

        secminorglyph1 = ""
        secminorglyph2 = ""
        secminorglyph3 = ""

        sectextprimeglyph1 = ""
        sectextprimeglyph2 = ""
        sectextprimeglyph3 = ""

        sectextmajorglyph1 = ""
        sectextmajorglyph2 = ""
        sectextmajorglyph3 = ""

        sectextminorglyph1 = ""
        sectextminorglyph2 = ""
        sectextminorglyph3 = ""

        secglyphpic1 = My.Resources.empty
        secglyphpic2 = My.Resources.empty
        secglyphpic3 = My.Resources.empty
        secglyphpic4 = My.Resources.empty
        secglyphpic5 = My.Resources.empty
        secglyphpic6 = My.Resources.empty
        secglyphpic7 = My.Resources.empty
        secglyphpic8 = My.Resources.empty
        secglyphpic9 = My.Resources.empty

        '##############################
        '###### Deklaration der Talente ######

        'mage


        '##############################


        quelltext = ""
        talentpage = ""
        sectalentpage = ""
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
        MsgBox("erfolgreich angelegt!")
        SQLConnection.Dispose()
    End Sub

    Public Sub New()
        MyBase.New()

        Thread.CurrentThread.CurrentUICulture = New CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub

    Private Sub Main_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If My.Settings.shellclose = True Then

            Starter.Show()
        Else

        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Starter.Hide()

        Me.Size = New Size(1119, 642)
        Me.MaximumSize = New Size(1119, 642)
        Select Case progressmode
            Case 1
                Panel21.Location = New Point(- 6, - 1)
                Panel21.Size = New Size(1124, 741)
            Case 2
                Panel21.Location = New Point(- 6, - 1)
                Panel21.Size = New Size(1124, 741)
            Case 3
                Panel21.Location = New Point(- 6, - 1)
                Panel21.Size = New Size(1124, 741)
            Case Else

        End Select
        Application.DoEvents()
    End Sub

    Public Sub setvisible(ByVal doit As Boolean)
        Kopf.Visible = False
        kopfsocket1.Visible = False
        kopfsocket2.Visible = False
        kopfsocket3.Visible = False
        kopfvz.Visible = False

        Hals.Visible = False
        halssocket1.Visible = False
        halssocket2.Visible = False
        halssocket3.Visible = False
        halsvz.Visible = False

        Schulter.Visible = False
        schultersocket1.Visible = False
        schultersocket2.Visible = False
        schultersocket3.Visible = False
        schultervz.Visible = False

        Ruecken.Visible = False
        rueckensocket1.Visible = False
        rueckensocket2.Visible = False
        rueckensocket3.Visible = False
        rueckenvz.Visible = False

        Brust.Visible = False
        brustsocket1.Visible = False
        brustsocket2.Visible = False
        brustsocket3.Visible = False
        brustvz.Visible = False

        Hemd.Visible = False


        Wappenrock.Visible = False
        level.Visible = False

        Handgelenke.Visible = False
        Handgelenkesocket1.Visible = False
        handgelenkesocket2.Visible = False
        Handgelenkesocket3.Visible = False
        handgelenkevz.Visible = False

        Haupt.Visible = False
        Hauptsocket1.Visible = False
        Hauptsocket2.Visible = False
        hauptsocket3.Visible = False
        hauptvz.Visible = False
        hauptvzlabel2.Visible = False

        Off.Visible = False
        Offsocket1.Visible = False
        Offsocket2.Visible = False
        offsocket3.Visible = False
        offvz.Visible = False
        offvzlabel2.Visible = False

        Distanz.Visible = False
        Distanzsocket1.Visible = False
        Distanzsocket2.Visible = False
        distanzsocket3.Visible = False
        distanzvz.Visible = False
        distanzvzlabel2.Visible = False

        Haende.Visible = False
        haendesocket1.Visible = False
        haendesocket2.Visible = False
        haendesocket3.Visible = False
        haendevz.Visible = False

        Guertel.Visible = False
        guertelsocket1.Visible = False
        guertelsocket2.Visible = False
        guertelsocket3.Visible = False
        guertelvz.Visible = False

        Beine.Visible = False
        beinesocket1.Visible = False
        beinesocket2.Visible = False
        beinesocket3.Visible = False
        beinevz.Visible = False

        Stiefel.Visible = False
        stiefelsocket1.Visible = False
        stiefelsocket2.Visible = False
        stiefelsocket3.Visible = False
        stiefelvz.Visible = False

        Ring1.Visible = False
        Ring1socket1.Visible = False
        ring1socket2.Visible = False
        ring1socket3.Visible = False
        ring1vz.Visible = False

        Ring2.Visible = False
        ring2socket1.Visible = False
        ring2socket2.Visible = False
        ring2socket3.Visible = False
        ring2vz.Visible = False

        Schmuck1.Visible = False

        schmuck1vz.Visible = False

        Schmuck2.Visible = False

        schmuck2vz.Visible = False
        Glyphs.prim1.Visible = False
        Glyphs.prim2.Visible = False
        Glyphs.prim3.Visible = False
        Glyphs.erheb1.Visible = False
        Glyphs.erheb2.Visible = False
        Glyphs.erheb3.Visible = False
        Glyphs.gering1.Visible = False
        Glyphs.gering2.Visible = False
        Glyphs.gering3.Visible = False
        Glyphs.prim1pic.Image = My.Resources.empty
        Glyphs.prim2pic.Image = My.Resources.empty
        Glyphs.prim3pic.Image = My.Resources.empty
        Glyphs.erheb1pic.Image = My.Resources.empty
        Glyphs.erheb2pic.Image = My.Resources.empty
        Glyphs.erheb3pic.Image = My.Resources.empty
        Glyphs.gering1pic.Image = My.Resources.empty
        Glyphs.gering2pic.Image = My.Resources.empty
        Glyphs.gering3pic.Image = My.Resources.empty

        Glyphs.secprim1.Visible = False
        Glyphs.secprim2.Visible = False
        Glyphs.secprim3.Visible = False
        Glyphs.secerheb1.Visible = False
        Glyphs.secerheb2.Visible = False
        Glyphs.secerheb3.Visible = False
        Glyphs.secgering1.Visible = False
        Glyphs.secgering2.Visible = False
        Glyphs.secgering3.Visible = False
        Glyphs.secprim1pic.Image = My.Resources.empty
        Glyphs.secprim2pic.Image = My.Resources.empty
        Glyphs.secprim3pic.Image = My.Resources.empty
        Glyphs.secerheb1pic.Image = My.Resources.empty
        Glyphs.secerheb2pic.Image = My.Resources.empty
        Glyphs.secerheb3pic.Image = My.Resources.empty
        Glyphs.secgering1pic.Image = My.Resources.empty
        Glyphs.secgering2pic.Image = My.Resources.empty
        Glyphs.secgering3pic.Image = My.Resources.empty

        kopfpic.Image = My.Resources.empty
        Schulterpic.Image = My.Resources.empty
        Halspic.Image = My.Resources.empty
        Brustpic.Image = My.Resources.empty
        Rueckenpic.Image = My.Resources.empty
        Hemdpic.Image = My.Resources.empty
        Wappenrockpic.Image = My.Resources.empty
        Handgelenkepic.Image = My.Resources.empty
        Hauptpic.Image = My.Resources.empty
        Offpic.Image = My.Resources.empty
        Distanzpic.Image = My.Resources.empty
        Haendepic.Image = My.Resources.empty
        Guertelpic.Image = My.Resources.empty
        Stiefelpic.Image = My.Resources.empty
        Beinepic.Image = My.Resources.empty
        Ring1pic.Image = My.Resources.empty
        Ring2pic.Image = My.Resources.empty
        Schmuck1pic.Image = My.Resources.empty
        Schmuck2pic.Image = My.Resources.empty
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        ' Dim SQLStatement As String = "UPDATE characters SET name=LOL WHERE (guc
        My.Settings.shellclose = False
        My.Settings.Save()
        Process_Status.Close()
        Connect.Show()
    End Sub

    Private Sub Off_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Off.Click
    End Sub


    Private Sub Button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button6.Click

        Application.DoEvents()
        Filtern.Show()
    End Sub


    Private Sub kopfpic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles kopfpic.Click
        If kopfid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & kopfid.ToString)
        End If
    End Sub

    Private Sub kopfpic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles kopfpic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub kopfpic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles kopfpic.MouseMove
        If Kopf.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(59, 42)

            panelname.Text = Kopf.Text
            panelvz.Text = kopfvz.Text
            panelgem1.Text = kopfsocket1.Text
            panelgem2.Text = kopfsocket2.Text
            panelgem3.Text = kopfsocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Halspic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Halspic.Click
        If halsid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & halsid.ToString)
        End If
    End Sub

    Private Sub Halspic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Halspic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Halspic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Halspic.MouseMove
        If Hals.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(59, 98)

            panelname.Text = Hals.Text
            panelvz.Text = halsvz.Text
            panelgem1.Text = halssocket1.Text
            panelgem2.Text = halssocket2.Text
            panelgem3.Text = halssocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Schulterpic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Schulterpic.Click
        If schulterid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & schulterid.ToString)
        End If
    End Sub

    Private Sub Schulterpic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Schulterpic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Schulterpic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Schulterpic.MouseMove
        If Schulter.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(59, 154)

            panelname.Text = Schulter.Text
            panelvz.Text = schultervz.Text
            panelgem1.Text = schultersocket1.Text
            panelgem2.Text = schultersocket2.Text
            panelgem3.Text = schultersocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Rueckenpic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Rueckenpic.Click
        If rueckenid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & rueckenid.ToString)
        End If
    End Sub

    Private Sub Rueckenpic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Rueckenpic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Rueckenpic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Rueckenpic.MouseMove
        If Ruecken.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(59, 210)

            panelname.Text = Ruecken.Text
            panelvz.Text = rueckenvz.Text
            panelgem1.Text = rueckensocket1.Text
            panelgem2.Text = rueckensocket2.Text
            panelgem3.Text = rueckensocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Brustpic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Brustpic.Click
        If brustid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & brustid.ToString)
        End If
    End Sub

    Private Sub Brustpic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Brustpic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Brustpic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Brustpic.MouseMove
        If Brust.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(59, 266)

            panelname.Text = Brust.Text
            panelvz.Text = brustvz.Text
            panelgem1.Text = brustsocket1.Text
            panelgem2.Text = brustsocket2.Text
            panelgem3.Text = brustsocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Hemdpic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Hemdpic.Click
        If hemdid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & hemdid.ToString)
        End If
    End Sub

    Private Sub Hemdpic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Hemdpic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Hemdpic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Hemdpic.MouseMove
        If Hemd.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(59, 322)

            panelname.Text = Hemd.Text

            infopanel.Visible = True
        End If
    End Sub

    Private Sub Wappenrockpic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Wappenrockpic.Click
        If wappenrockid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & wappenrockid.ToString)
        End If
    End Sub

    Private Sub Wappenrockpic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Wappenrockpic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Wappenrockpic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) _
        Handles Wappenrockpic.MouseMove
        If Wappenrock.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(59, 378)

            panelname.Text = Wappenrock.Text

            infopanel.Visible = True
        End If
    End Sub

    Private Sub Handgelenkepic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Handgelenkepic.Click
        If handgelenkeid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & handgelenkeid.ToString)
        End If
    End Sub

    Private Sub Handgelenkepic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) _
        Handles Handgelenkepic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Handgelenkepic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) _
        Handles Handgelenkepic.MouseMove
        If Handgelenke.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(59, 434)

            panelname.Text = Handgelenke.Text
            panelvz.Text = handgelenkevz.Text
            panelgem1.Text = Handgelenkesocket1.Text
            panelgem2.Text = handgelenkesocket2.Text
            panelgem3.Text = Handgelenkesocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Hauptpic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Hauptpic.Click
        If hauptid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & hauptid.ToString)
        End If
    End Sub

    Private Sub Hauptpic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Hauptpic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Hauptpic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Hauptpic.MouseMove
        If Haupt.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(63, 525)

            panelname.Text = Haupt.Text
            panelvz.Text = hauptvz.Text
            panelgem1.Text = Hauptsocket1.Text
            panelgem2.Text = Hauptsocket2.Text
            panelgem3.Text = hauptsocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Offpic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Offpic.Click
        If offid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & offid.ToString)
        End If
    End Sub

    Private Sub Offpic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Offpic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Offpic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Offpic.MouseMove
        If Off.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(313, 525)

            panelname.Text = Off.Text
            panelvz.Text = offvz.Text
            panelgem1.Text = Offsocket1.Text
            panelgem2.Text = Offsocket2.Text
            panelgem3.Text = offsocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Distanzpic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Distanzpic.Click
        If distanzid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & distanzid.ToString)
        End If
    End Sub

    Private Sub Distanzpic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Distanzpic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Distanzpic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Distanzpic.MouseMove
        If Distanz.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(533, 525)

            panelname.Text = Distanz.Text
            panelvz.Text = distanzvz.Text
            panelgem1.Text = Distanzsocket1.Text
            panelgem2.Text = Distanzsocket2.Text
            panelgem3.Text = distanzsocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Haendepic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Haendepic.Click
        If haendeid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & haendeid.ToString)
        End If
    End Sub

    Private Sub Haendepic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Haendepic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Haendepic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Haendepic.MouseMove
        If Haende.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(534, 42)

            panelname.Text = Haende.Text
            panelvz.Text = haendevz.Text
            panelgem1.Text = haendesocket1.Text
            panelgem2.Text = haendesocket2.Text
            panelgem3.Text = haendesocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Guertelpic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Guertelpic.Click
        If guertelid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & guertelid.ToString)
        End If
    End Sub

    Private Sub Guertelpic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Guertelpic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Guertelpic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Guertelpic.MouseMove
        If Guertel.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(534, 98)

            panelname.Text = Guertel.Text
            panelvz.Text = guertelvz.Text
            panelgem1.Text = guertelsocket1.Text
            panelgem2.Text = guertelsocket2.Text
            panelgem3.Text = guertelsocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Beinepic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Beinepic.Click
        If beineid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & beineid.ToString)
        End If
    End Sub

    Private Sub Beinepic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Beinepic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Beinepic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Beinepic.MouseMove
        If Beine.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(534, 154)

            panelname.Text = Beine.Text
            panelvz.Text = beinevz.Text
            panelgem1.Text = beinesocket1.Text
            panelgem2.Text = beinesocket2.Text
            panelgem3.Text = beinesocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Stiefelpic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Stiefelpic.Click
        If stiefelid.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & stiefelid.ToString)
        End If
    End Sub

    Private Sub Stiefelpic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Stiefelpic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Stiefelpic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Stiefelpic.MouseMove
        If Stiefel.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(534, 210)

            panelname.Text = Stiefel.Text
            panelvz.Text = stiefelvz.Text
            panelgem1.Text = stiefelsocket1.Text
            panelgem2.Text = stiefelsocket2.Text
            panelgem3.Text = stiefelsocket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Ring1pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Ring1pic.Click
        If ring1id.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & ring1id.ToString)
        End If
    End Sub

    Private Sub Ring1pic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Ring1pic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Ring1pic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Ring1pic.MouseMove
        If Ring1.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(534, 266)

            panelname.Text = Ring1.Text
            panelvz.Text = ring1vz.Text
            panelgem1.Text = Ring1socket1.Text
            panelgem2.Text = ring1socket2.Text
            panelgem3.Text = ring1socket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Ring2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Ring2pic.Click
        If ring2id.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & ring2id.ToString)
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        Glyphs.prim1.Text = textprimeglyph1
        Glyphs.prim2.Text = textprimeglyph2
        Glyphs.prim3.Text = textprimeglyph3
        Glyphs.erheb1.Text = textmajorglyph1
        Glyphs.erheb2.Text = textmajorglyph2
        Glyphs.erheb3.Text = textmajorglyph3
        Glyphs.gering1.Text = textminorglyph1
        Glyphs.gering2.Text = textminorglyph2
        Glyphs.gering3.Text = textminorglyph3

        Glyphs.prim1pic.Image = glyphpic1
        Glyphs.prim2pic.Image = glyphpic2
        Glyphs.prim3pic.Image = glyphpic3
        Glyphs.erheb1pic.Image = glyphpic4
        Glyphs.erheb2pic.Image = glyphpic5
        Glyphs.erheb3pic.Image = glyphpic6
        Glyphs.gering1pic.Image = glyphpic7
        Glyphs.gering2pic.Image = glyphpic8
        Glyphs.gering3pic.Image = glyphpic9

        Glyphs.secprim1.Text = sectextprimeglyph1
        Glyphs.secprim2.Text = sectextprimeglyph2
        Glyphs.secprim3.Text = sectextprimeglyph3
        Glyphs.secerheb1.Text = sectextmajorglyph1
        Glyphs.secerheb2.Text = sectextmajorglyph2
        Glyphs.secerheb3.Text = sectextmajorglyph3
        Glyphs.secgering1.Text = sectextminorglyph1
        Glyphs.secgering2.Text = sectextminorglyph2
        Glyphs.secgering3.Text = sectextminorglyph3

        Glyphs.secprim1pic.Image = secglyphpic1
        Glyphs.secprim2pic.Image = secglyphpic2
        Glyphs.secprim3pic.Image = secglyphpic3
        Glyphs.secerheb1pic.Image = secglyphpic4
        Glyphs.secerheb2pic.Image = secglyphpic5
        Glyphs.secerheb3pic.Image = secglyphpic6
        Glyphs.secgering1pic.Image = secglyphpic7
        Glyphs.secgering2pic.Image = secglyphpic8
        Glyphs.secgering3pic.Image = secglyphpic9
        Glyphs.Show()
    End Sub

    Private Sub Ring2pic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Ring2pic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Ring2pic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Ring2pic.MouseMove
        If Ring2.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(534, 322)

            panelname.Text = Ring2.Text
            panelvz.Text = ring2vz.Text
            panelgem1.Text = ring2socket1.Text
            panelgem2.Text = ring2socket2.Text
            panelgem3.Text = ring2socket3.Text
            infopanel.Visible = True
        End If
    End Sub

    Private Sub Schmuck1pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Schmuck1pic.Click
        If schmuck1id.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & schmuck1id.ToString)
        End If
    End Sub

    Private Sub Schmuck1pic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Schmuck1pic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Schmuck1pic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Schmuck1pic.MouseMove
        If Schmuck1.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(534, 378)

            panelname.Text = Schmuck1.Text
            panelvz.Text = schmuck1vz.Text

            infopanel.Visible = True
        End If
    End Sub

    Private Sub Schmuck2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Schmuck2pic.Click
        If schmuck2id.ToString = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & schmuck2id.ToString)
        End If
    End Sub

    Private Sub Schmuck2pic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Schmuck2pic.MouseLeave
        infopanel.Visible = False
        panelname.Text = ""
        panelvz.Text = ""
        panelgem1.Text = ""
        panelgem2.Text = ""
        panelgem3.Text = ""
    End Sub

    Private Sub Schmuck2pic_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Schmuck2pic.MouseMove
        If Schmuck2.Text = "-" Then
            panelname.Text = ""
            panelvz.Text = ""
            panelgem1.Text = ""
            panelgem2.Text = ""
            panelgem3.Text = ""
        Else
            infopanel.Location = New Point(534, 434)

            panelname.Text = Schmuck2.Text
            panelvz.Text = schmuck2vz.Text

            infopanel.Visible = True
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As Object, ByVal e As EventArgs)
        AboutBox.Show()
    End Sub


    Private Sub Button8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button8.Click
        Me.Close()
        Starter.Show()
    End Sub


    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click

        If My.Settings.language = "de" Then
            MsgBox(localeDE.armoryinterface_txt1)
        Else
            MsgBox(localeEN.armoryinterface_txt1)
        End If

        Dim locOFD As New SaveFileDialog()
        Dim writepath As String = ""
        With locOFD
            '  .Filter = "(.ciu)|.ciu"
            .Filter = "CharImport Universal files (*.ciu)|*.ciu|All files (*.*)|*.*"
            If My.Settings.language = "de" Then
                .Title = localeDE.armoryinterface_txt2
            Else
                .Title = localeEN.armoryinterface_txt2
            End If

            .FileName = char_name & ".ciu"

            .DefaultExt = "ciu"

            .CheckPathExists = True

            If (.ShowDialog() = DialogResult.OK) Then

                writepath = .FileName()
            Else
                Exit Sub

            End If
        End With
        If writepath = "" Then
            If My.Settings.language = "de" Then
                MsgBox(localeDE.armoryinterface_txt3, MsgBoxStyle.Critical, localeDE.armoryinterface_txt4)
            Else
                MsgBox(localeEN.armoryinterface_txt3, MsgBoxStyle.Critical, localeEN.armoryinterface_txt4)
            End If
            Exit Sub
        End If
        If Not datasets = 1 Then datasets += 1
        ciu.adddataset()
        ciu.createfile(writepath)
        If My.Settings.language = "de" Then
            MsgBox(localeDE.main_txt1)
        Else
            MsgBox(localeEN.main_txt1)
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim cit As New CIUFile
        cit.getfile()
    End Sub


    Private Sub Schmuck2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Schmuck2.Click
    End Sub

    Private Sub Panel23_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
    End Sub

    Private Sub Button7_Click(ByVal sender As Object, ByVal e As EventArgs)
        level.Visible = True
    End Sub

    Private Sub Hals_Click(sender As System.Object, e As System.EventArgs) Handles Hals.Click

    End Sub
End Class
