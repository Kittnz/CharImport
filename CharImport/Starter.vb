'Copyright (C) 2011-2012 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* The form Starter is the main menu of the application and lets the user choose
'* the desired feature.
'*
'* Developed by Alcanmage/megasus

Imports System.Threading
Imports System.Globalization
Imports System.Net

Public Class Starter
    Public runfunction As New Functions
    Public programversion As Integer = 10030 '0.10.03
    Public required_template_version As Integer = 2 'increasing this value will cause the outdating of older template file formats
    Public downloadlink As String = ""
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN

    Private Sub updatebutton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles updatebutton.Click
        Process.Start(downloadlink)
        Application.Exit()
    End Sub

    Public Sub New()
        MyBase.New()

        Thread.CurrentThread.CurrentUICulture = New CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub

    Private Sub Starter_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If My.Settings.shellclose = True Then
            AboutBox.Close()
            Armory_Interface.Close()
            Armory2Database.Close()
            Bugreport.Close()
            Connect.Close()
            Database_Check.Close()
            Database_Interface.Close()
            Database2Database.Close()
            Filtern.Close()
            Glyphs.Close()
            Main.Close()
            Welcome.Close()
            Application.Exit()

        Else

        End If
    End Sub

    Private Sub Starter_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.Text = "CharImport [Universal] - " & Me.ProductVersion & " (Test)"
        My.Settings.writelog = True
        ' ###### Change it to false at pre-release ######
        My.Settings.savecontent = ""
        My.Settings.Save()
        If My.Settings.writelog = True Then
            If My.Computer.FileSystem.FileExists(Application.StartupPath + "\Errorlog.log") Then
                My.Computer.FileSystem.DeleteFile(Application.StartupPath + "\Errorlog.log")
            Else : End If
        End If
        runfunction.writelog(Me.Text, False)
        runfunction.writelog(
            "System: " & My.Computer.Info.OSFullName & " // " & My.Computer.Info.OSVersion & " // " &
            My.Computer.Info.OSPlatform, False)
        runfunction.writelog("Application started")
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size
        Me.AutoSize = True
        Application.DoEvents()
        If Not My.Computer.FileSystem.FileExists(Application.StartupPath & "\mysql.data.dll") Then
            MsgBox("mysql.data.dll is missing. Please make sure to extract the whole archive!", MsgBoxStyle.Critical,
                   "Warning")
            Application.Exit()
        End If
        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String
            quellcodeyx88 = clienyx88.DownloadString("http://www.google.com")
        Catch
            If My.Settings.language = "de" Then
                MsgBox(localeDE.nointernetconnection, MsgBoxStyle.Critical, localeDE.attention)
            Else
                MsgBox(localeEN.nointernetconnection, MsgBoxStyle.Critical, localeEN.attention)
            End If
        End Try
        Try
            If My.Settings.language = "de" Then
                DeleteUrlCacheEntry(localeDE.starter_link1)
            Else
                DeleteUrlCacheEntry(localeEN.starter_link1)
            End If

        Catch ex As Exception

        End Try
        Try
            If My.Settings.language = "de" Then
                DeleteUrlCacheEntry(localeDE.starter_link1)
            Else
                DeleteUrlCacheEntry(localeEN.starter_link1)
            End If

        Catch ex As Exception

        End Try

        Try
            Dim clienyx88 As New WebClient
            Dim quellcodeyx88 As String
            If My.Settings.language = "de" Then
                quellcodeyx88 = clienyx88.DownloadString(localeDE.starter_link1)
            Else
                quellcodeyx88 = clienyx88.DownloadString(localeEN.starter_link1)
            End If

            Dim anfangyx88 As String = "<locked>"
            Dim endeyx88 As String = "</locked>"
            Dim quellcodeSplityx88 As String
            quellcodeSplityx88 = Split(quellcodeyx88, anfangyx88, 5)(1)
            quellcodeSplityx88 = Split(quellcodeSplityx88, endeyx88, 6)(0)
            If quellcodeSplityx88 = "true" Then
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.starter_txt1, MsgBoxStyle.Critical, localeDE.connect_txt25)
                Else
                    MsgBox(localeEN.starter_txt1, MsgBoxStyle.Critical, localeEN.connect_txt25)
                End If

                Application.Exit()
            Else
                Dim zquellcodeyx880 As String = quellcodeyx88
                Dim zanfangyx880 As String = "<version>"
                Dim zendeyx880 As String = "</version>"
                Dim zquellcodeSplityx880 As String
                zquellcodeSplityx880 = Split(zquellcodeyx880, zanfangyx880, 5)(1)
                zquellcodeSplityx880 = Split(zquellcodeSplityx880, zendeyx880, 6)(0)
                If CInt(Val(zquellcodeSplityx880)) > programversion Then
                    'neue version verfügbar
                    Dim zquellcodeyx88 As String = quellcodeyx88
                    Dim zanfangyx88 As String = "<downloadlink>"
                    Dim zendeyx88 As String = "</downloadlink>"
                    Dim zquellcodeSplityx88 As String
                    zquellcodeSplityx88 = Split(zquellcodeyx88, zanfangyx88, 5)(1)
                    zquellcodeSplityx88 = Split(zquellcodeSplityx88, zendeyx88, 6)(0)
                    downloadlink = zquellcodeSplityx88
                    With updatebutton
                        If My.Settings.language = "de" Then
                            .Text = localeDE.starter_txt2
                        Else
                            .Text = localeEN.starter_txt2
                        End If

                        .Enabled = True
                        .BackColor = Color.Red
                    End With
                    UpdateMe.Show()
                Else
                    If My.Settings.firststart = True Then
                        Welcome.Show()
                        Me.Hide()
                    End If

                End If
            End If


        Catch ex As Exception
            runfunction.writelog("Failed to connect to FTP-Server at start: " & vbNewLine & ex.ToString)
        End Try
        Me.BringToFront()
    End Sub

    Private Declare Function URLDownloadToFile Lib "urlmon" _
        Alias "URLDownloadToFileA"(
                                   ByVal pCaller As Long,
                                   ByVal szURL As String,
                                   ByVal szFileName As String,
                                   ByVal dwReserved As Long,
                                   ByVal lpfnCB As Long) As Long

    Private Declare Function DeleteUrlCacheEntry Lib "wininet.dll" _
        Alias "DeleteUrlCacheEntryA"(
                                     ByVal lpszUrlName As String) As Long

    ' Datei-Download mit oder ohne Leerung des URL-Cache
    Public Function FileDownload(ByVal sURL As String,
                                 Optional ByVal bClearCache As Boolean = True) As Boolean

        Dim lResult As Long

        ' URL-Cache leeren?
        If bClearCache Then
            lResult = DeleteUrlCacheEntry(sURL)
        End If
        Return True
    End Function

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button4.Click
        AboutBox.Show()
    End Sub

    Private Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        runfunction.writelog("Starter_Application_exit call")
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs)

        Main.progressmode = 1
        Armory_Interface.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs)
        Main.progressmode = 3
        Main.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs)
        Main.progressmode = 2
        Main.Show()
        Me.Hide()
    End Sub

    Private Sub Button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button6.Click

        Bugreport.Show()
    End Sub

    Private Sub Panel1_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel1.MouseClick
        Main.progressmode = 1
        Main.importmode = 1
        Armory_Interface.Show()
        Me.Hide()
    End Sub

    Private Sub Panel1_MouseHover(ByVal sender As Object, ByVal e As EventArgs) Handles Panel1.MouseHover
        Panel1.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Panel1_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Panel1.MouseLeave
        Panel1.BackColor = Color.RoyalBlue
    End Sub

    Private Sub Panel1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel1.MouseMove
        Panel1.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Panel1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Panel1.Paint
    End Sub

    Private Sub Panel2_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel2.MouseClick
        Main.cuisets = 0
        Main.progressmode = 3
        Main.importmode = 3
        Database_Interface.Show()
        Me.Hide()
    End Sub

    Private Sub Panel2_MouseHover(ByVal sender As Object, ByVal e As EventArgs) Handles Panel2.MouseHover
        Panel2.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Panel2_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Panel2.MouseLeave
        Panel2.BackColor = Color.RoyalBlue
    End Sub

    Private Sub Panel2_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel2.MouseMove
        Panel2.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Panel2_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Panel2.Paint
    End Sub

    Private Sub Panel3_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel3.MouseClick
        My.Settings.savecontent = ""
        ' ???? 29/07 should fix: (Load char from db > load char from armory > store it > import template = first character)
        My.Settings.Save()
        Main.setallempty()
        If My.Settings.language = "de" Then
            MsgBox(localeDE.armoryinterface_txt1)
        Else
            MsgBox(localeEN.armoryinterface_txt1)
        End If

        Dim locOFD As New OpenFileDialog()
        Dim locPath As String
        With locOFD
            .Filter = "CharImport Template File (*.ciu)|*.ciu"
            If My.Settings.language = "de" Then
                .Title = localeDE.starter_txt3
            Else
                .Title = localeEN.starter_txt3
            End If

            .DefaultExt = ".ciu"

            .Multiselect = False
            .CheckFileExists = True
            .CheckPathExists = True

            If (.ShowDialog() = DialogResult.OK) Then

                locPath = .FileName()
                If Not locPath = "" Then

                    Dim cit As New CIUFile
                    Main.tmplpath = locPath
                    Main.cuisets = 0
                    Dim ciu As New CIUFile
                    ciu.nowread()
                End If

            End If
        End With
    End Sub

    Private Sub Panel3_MouseHover(ByVal sender As Object, ByVal e As EventArgs) Handles Panel3.MouseHover
        Panel3.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Panel3_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Panel3.MouseLeave
        Panel3.BackColor = Color.RoyalBlue
    End Sub

    Private Sub Panel3_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Panel3.MouseMove
        Panel3.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Panel3_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Panel3.Paint
    End Sub


    Private Sub Label3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label3.Click
        Main.progressmode = 1
        Main.importmode = 1
        Armory_Interface.Show()
        Me.Hide()
    End Sub

    Private Sub Label3_MouseHover(ByVal sender As Object, ByVal e As EventArgs) Handles Label3.MouseHover
        Panel1.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Label3_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Label3.MouseLeave
        Panel1.BackColor = Color.RoyalBlue
    End Sub

    Private Sub Label3_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Label3.MouseMove
        Panel1.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Label2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label2.Click
        Main.progressmode = 1
        Main.importmode = 1
        Armory_Interface.Show()
        Me.Hide()
    End Sub

    Private Sub Label2_MouseHover(ByVal sender As Object, ByVal e As EventArgs) Handles Label2.MouseHover
        Panel1.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Label2_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Label2.MouseLeave
        Panel1.BackColor = Color.RoyalBlue
    End Sub

    Private Sub Label2_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Label2.MouseMove
        Panel1.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Label4_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label4.Click
        Main.cuisets = 0
        Main.progressmode = 3
        Main.importmode = 3
        Database_Interface.Show()
        Me.Hide()
    End Sub

    Private Sub Label4_MouseHover(ByVal sender As Object, ByVal e As EventArgs) Handles Label4.MouseHover
        Panel2.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Label4_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Label4.MouseLeave
        Panel2.BackColor = Color.RoyalBlue
    End Sub

    Private Sub Label4_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Label4.MouseMove
        Panel2.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Label5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label5.Click
        Main.cuisets = 0
        Main.progressmode = 3
        Main.importmode = 3
        Database_Interface.Show()
        Me.Hide()
    End Sub

    Private Sub Label5_MouseHover(ByVal sender As Object, ByVal e As EventArgs) Handles Label5.MouseHover
        Panel2.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Label5_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Label5.MouseLeave
        Panel2.BackColor = Color.RoyalBlue
    End Sub

    Private Sub Label5_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Label5.MouseMove
        Panel2.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Label6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label6.Click
        My.Settings.savecontent = ""
        ' ???? 29/07 should fix: (Load char from db > load char from armory > store it > import template = first character)
        My.Settings.Save()
        Main.setallempty()
        If My.Settings.language = "de" Then
            MsgBox(localeDE.armoryinterface_txt1)
        Else
            MsgBox(localeEN.armoryinterface_txt1)
        End If

        Dim locOFD As New OpenFileDialog()
        Dim locPath As String
        With locOFD
            .Filter = "CharImport Template File (*.ciu)|*.ciu"
            If My.Settings.language = "de" Then
                .Title = localeDE.starter_txt3
            Else
                .Title = localeEN.starter_txt3
            End If

            .DefaultExt = ".ciu"

            .Multiselect = False
            .CheckFileExists = True
            .CheckPathExists = True

            If (.ShowDialog() = DialogResult.OK) Then

                locPath = .FileName()
                If Not locPath = "" Then

                    Dim cit As New CIUFile
                    Main.tmplpath = locPath
                    Main.cuisets = 0
                    Dim ciu As New CIUFile
                    ciu.nowread()
                End If

            End If
        End With
    End Sub

    Private Sub Label6_MouseHover(ByVal sender As Object, ByVal e As EventArgs) Handles Label6.MouseHover
        Panel3.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Label6_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Label6.MouseLeave
        Panel3.BackColor = Color.RoyalBlue
    End Sub

    Private Sub Label6_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Label6.MouseMove
        Panel3.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Label7_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label7.Click
        My.Settings.savecontent = ""
        ' ???? 29/07 should fix: (Load char from db > load char from armory > store it > import template = first character)
        My.Settings.Save()
        Main.setallempty()
        If My.Settings.language = "de" Then
            MsgBox(localeDE.armoryinterface_txt1)
        Else
            MsgBox(localeEN.armoryinterface_txt1)
        End If

        Dim locOFD As New OpenFileDialog()
        Dim locPath As String
        With locOFD
            .Filter = "CharImport Template File (*.ciu)|*.ciu"
            If My.Settings.language = "de" Then
                .Title = localeDE.starter_txt3
            Else
                .Title = localeEN.starter_txt3
            End If

            .DefaultExt = ".ciu"

            .Multiselect = False
            .CheckFileExists = True
            .CheckPathExists = True

            If (.ShowDialog() = DialogResult.OK) Then

                locPath = .FileName()
                If Not locPath = "" Then

                    Dim cit As New CIUFile
                    Main.tmplpath = locPath
                    Main.cuisets = 0
                    Dim ciu As New CIUFile
                    ciu.nowread()
                End If

            End If
        End With
    End Sub

    Private Sub Label7_MouseHover(ByVal sender As Object, ByVal e As EventArgs) Handles Label7.MouseHover
        Panel3.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Label7_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Label7.MouseLeave
        Panel3.BackColor = Color.RoyalBlue
    End Sub

    Private Sub Label7_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Label7.MouseMove
        Panel3.BackColor = Color.CornflowerBlue
    End Sub

    Private Sub Button1_Click_1(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        Welcome.Show()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs)
        Welcome.Close()
    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles ComboBox1.SelectedIndexChanged
        My.Settings.shellclose = False
        My.Settings.Save()
        If ComboBox1.SelectedItem.ToString = "Deutsch" Then
            If My.Settings.language = "de" Then

            Else
                My.Settings.language = "de"
                My.Settings.Save()
                Dim newForm1 As Starter
                newForm1 = New Starter

                newForm1.Show()
                newForm1.Focus()
                Me.Close()
                ' Localechange.Show()
            End If

        Else
            If My.Settings.language = "en" Then

            Else
                My.Settings.language = "en"
                My.Settings.Save()
                Dim newForm1 As Starter
                newForm1 = New Starter

                newForm1.Show()
                newForm1.Focus()
                Me.Close()
                ' Localechange.Show()
            End If
        End If
        My.Settings.shellclose = True
        My.Settings.Save()
    End Sub

    Private Sub Label1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label1.Click
    End Sub
End Class