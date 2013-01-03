'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* The form Armory_Interface allows the user to select multiple characters from
'* the official World of Warcraft Armory which will later be parsed.
'*
'* Developed by Alcanmage/megasus

Imports System.Threading
Imports System.Globalization
Imports System.Net

Public Class Armory_Interface
    Dim armoryproc As New prozedur_armory
    Dim reporttext As RichTextBox
    Dim procstatus As New Process_Status
    Dim ciu As New CIUFile
    Dim writepath As String
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN
    Dim runfunction As New Functions

    Public Sub New()
        MyBase.New()

        Thread.CurrentThread.CurrentUICulture = New CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        My.Settings.shellclose = False
        My.Settings.Save()
        runfunction.writelog("""Copy Characters directly into Database"" selected")
        Main.importmode = 1
        My.Settings.savecontent = ""
        ' ???? 29/07 should fix: (Load char from db > load char from armory > store it > import template = first character)
        My.Settings.Save()
        Main.overview = False
        Main.linklist.Clear()
        Main.linklist = New List(Of String)
        My.Settings.shellclose = True
        My.Settings.Save()
        '   runfunction.writelog("Characters are: " & TextBox1.Text)

        For Each xitem As ListViewItem In ListView1.Items
            Main.linklist.Add(xitem.SubItems(3).Text)
        Next

        runfunction.writelog("Armory_Interface_Load call")
        If ListView1.Items.Count = 1 Then
            runfunction.writelog("Connect open request")
            Main.getfromarmoryfirst = True
            Main.showstarter = True
            runfunction.writelog("Armory_Interface_closing call")

            Me.Close()
            Connect.Show()


        Else
            runfunction.writelog("Armory2Database open request")
            Main.ausgangsformat = 1
            Me.Close()
            Armory2Database.Show()
            runfunction.writelog("Armory_Interface_closing call")

        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs)


        Dim xcount As Integer = 0
        Dim removecount As Integer = 0

        If xcount - removecount = 1 Then
            Button3.Enabled = True
            Button1.Enabled = True
            Button2.Enabled = True
        ElseIf xcount - removecount >= 2 Then
            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = False
        Else
            Button3.Enabled = False
            Button1.Enabled = False
            Button2.Enabled = False
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        runfunction.writelog("""Character overview"" selected")
        runfunction.writelog("Set-All-Empty and Set-visible request")
        My.Settings.shellclose = False
        My.Settings.savecontent = ""
        ' ???? 29/07 should fix: (Load char from db > load char from armory > store it > import template = first character)
        My.Settings.Save()
        Main.setvisible(False)
        Main.setallempty()
        Main.overview = True
        runfunction.writelog("Character link is: " & ListView1.Items(0).SubItems(3).Text)

        Main.importmode = 1
        Main.progressmode = 1
        Main.Show()
        Process_Status.Close()
        Process_Status.Dispose()
        Process_Status.Show()

        Application.DoEvents()
        runfunction.writelog("Load character from armory request")
        armoryproc.prozedur(ListView1.Items(0).SubItems(3).Text, 1, True)
        Process_Status.Button1.Enabled = True
        Application.DoEvents()
        runfunction.writelog("Armory_Interface_closing call")
        My.Settings.shellclose = True
        My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        runfunction.writelog("""Store Armory Character into template"" selected")
        My.Settings.savecontent = ""
        My.Settings.Save()
        If My.Settings.language = "de" Then
            MsgBox(localeDE.armoryinterface_txt1)
        Else
            MsgBox(localeEN.armoryinterface_txt1)
        End If

        Dim locOFD As New SaveFileDialog()

        With locOFD
            '  .Filter = "(.ciu)|.ciu"
            .Filter = "CharImport Universal files (*.ciu)|*.ciu|All files (*.*)|*.*"
            If My.Settings.language = "de" Then
                .Title = localeDE.armoryinterface_txt2
            Else
                .Title = localeEN.armoryinterface_txt2
            End If

            .FileName = ListView1.Items(0).SubItems(2).Text & ".ciu"

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
        Main.overview = False
        Main.importmode = 1
        Main.progressmode = 1
        Main.linklist.Clear()
        Main.linklist = New List(Of String)
        ' runfunction.writelog("Links entered: " & TextBox1.Text)

        Dim xnumber As Integer = 0

        For Each xitem As ListViewItem In ListView1.Items
            Main.linklist.Add(xitem.SubItems(3).Text)
        Next


        Process_Status.Show()
        For Each link As String In Main.linklist

            runfunction.writelog("Load character from armory request")
            If ListView1.Items.Count = 1 Then

                Main.overview = True
                armoryproc.prozedur(link, xnumber, True)
            Else
                Main.overview = False
                armoryproc.prozedur(link, xnumber, False)
            End If

        Next
        runfunction.writelog("Create ciu request")
        My.Settings.shellclose = True
        ciu.createfile(writepath)
        Process_Status.processreport.AppendText(Now.TimeOfDay.ToString & "// Template File created!" & vbNewLine)
        Process_Status.Button1.Enabled = True
        runfunction.writelog("Armory_Interface_closing call")
        Me.Close()
        Starter.Show()
    End Sub

    Private Sub Button8_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button8.Click
        Me.Close()
        runfunction.writelog("Armory_Interface_closing call")
        Starter.Show()
    End Sub

    Private Sub Armory_Interface_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
        Handles Me.FormClosing
        If My.Settings.shellclose = False Then
            Starter.Show()


        End If
    End Sub

    Private Sub Armory_Interface_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Main.getfromarmoryfirst = False
        runfunction.writelog("Armory_Interface_Load call")
        Me.MaximumSize = Me.Size
        Main.linklist = New List(Of String)
        Main.datasets = vbEmpty
    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs)
    End Sub


    Private Sub add_button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles add_button.Click
        Dim templink As String =
                "http://#replaceregion#.battle.net/wow/#replacelang#/character/#replacerealm#/#replacecharacter#/advanced"
        If globalregion.SelectedItem Is Nothing Then
            If My.Settings.language = "de" Then
                MsgBox(localeDE.regionnotset, MsgBoxStyle.Critical, localeDE.attention)
            Else
                MsgBox(localeEN.regionnotset, MsgBoxStyle.Critical, localeEN.attention)
            End If
        ElseIf realmname.Text = "" Then
            If My.Settings.language = "de" Then
                MsgBox(localeDE.norealmname, MsgBoxStyle.Critical, localeDE.attention)
            Else
                MsgBox(localeEN.norealmname, MsgBoxStyle.Critical, localeEN.attention)
            End If
        ElseIf charname.Text = "" Then
            If My.Settings.language = "de" Then
                MsgBox(localeDE.nocharname, MsgBoxStyle.Critical, localeDE.attention)
            Else
                MsgBox(localeEN.nocharname, MsgBoxStyle.Critical, localeEN.attention)
            End If
        Else
            templink = templink.Replace("#replaceregion#", globalregion.SelectedItem.ToString)
            templink = templink.Replace("#replacerealm#", realmname.Text)
            templink = templink.Replace("#replacecharacter#", charname.Text)
            If My.Settings.language = "de" And globalregion.SelectedItem.ToString = "EU" Then
                templink = templink.Replace("#replacelang#", "de")
            Else
                If globalregion.SelectedItem.ToString = "KR" Then
                    templink = templink.Replace("#replacelang#", "ko")
                ElseIf globalregion.SelectedItem.ToString = "TW" Then
                    templink = templink.Replace("#replacelang#", "zh")
                Else
                    templink = templink.Replace("#replacelang#", "en")
                End If

            End If
            'Add battle.net maintenance check!!
            Dim testclient As New WebClient
            Dim checkcode As String = ""
            Try
                checkcode = testclient.DownloadString(templink)
            Catch ex As Exception
                If ex.ToString.Contains("404") Then
                    If My.Settings.language = "de" Then
                        MsgBox(localeDE.charnotfound, MsgBoxStyle.Critical, localeDE.attention)
                    Else
                        MsgBox(localeEN.charnotfound, MsgBoxStyle.Critical, localeEN.attention)
                    End If
                    Exit Sub
                End If
            End Try

            If checkcode.Contains("error=503") Then
                If My.Settings.language = "de" Then
                    MsgBox(localeDE.charnotfound, MsgBoxStyle.Critical, localeDE.attention)
                Else
                    MsgBox(localeEN.charnotfound, MsgBoxStyle.Critical, localeEN.attention)
                End If
            Else

                Dim str(3) As String
                Dim itm As ListViewItem
                str(0) = globalregion.SelectedItem.ToString
                str(1) = realmname.Text
                str(2) = charname.Text
                str(3) = templink
                itm = New ListViewItem(str)
                ListView1.Items.Add(itm)
                ListView1.EnsureVisible(ListView1.Items.Count - 1)
                ListView1.Update()
                realmname.Text = ""
                charname.Text = ""
                If ListView1.Items.Count = 1 Then
                    Button1.Enabled = True
                    Button2.Enabled = True
                    Button3.Enabled = True
                ElseIf ListView1.Items.Count > 1 Then
                    Button1.Enabled = True
                    Button2.Enabled = True
                    Button3.Enabled = False
                End If
            End If


        End If
    End Sub

    Private Sub ListView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles ListView1.MouseDown
        ' Rechtsklick?
        ' Rechtsklick?
        If e.Button = MouseButtons.Right Then
            ' Eintrag ermitteln ...
            Dim oItem As ListViewItem = ListView1.GetItemAt(e.X, e.Y)
            If oItem IsNot Nothing Then
                ' ... und Kontextmenü anzeigen
                Dim I As Integer
                For I = 0 To ListView1.SelectedItems.Count - 1

                    Dim xname As String = ListView1.SelectedItems(I).Text

                    ContextMenuStrip1.Show(ListView1, e.X, e.Y)

                Next

            End If
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolStripMenuItem1.Click
        Try
            Dim xnummer = ListView1.SelectedItems(0).SubItems(2).Text

        Catch ex As Exception

        End Try

        Dim I As Integer
        For I = 0 To ListView1.SelectedItems.Count - 1


            ListView1.SelectedItems(I).Remove()
        Next
        If ListView1.Items.Count = 0 Then
            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
        ElseIf ListView1.Items.Count = 1 Then
            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = True
        ElseIf ListView1.Items.Count > 1 Then
            Button1.Enabled = True
            Button2.Enabled = True
            Button3.Enabled = False
        End If
    End Sub

    Private Sub realmname_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles realmname.KeyDown
        If e.KeyCode = Keys.Enter And Me.ActiveControl.Name Is "realmname" Then
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub charname_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles charname.KeyDown
        If e.KeyCode = Keys.Enter And Me.ActiveControl.Name Is "charname" Then
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
            e.Handled = True
        End If
    End Sub

    Private Sub realmname_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles realmname.TextChanged
    End Sub

    Private Sub globalregion_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles globalregion.SelectedIndexChanged
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles ListView1.SelectedIndexChanged
    End Sub
End Class