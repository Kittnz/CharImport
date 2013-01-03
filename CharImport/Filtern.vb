'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* The form Filtern allows the user to filter items by patch
'*
'* Developed by Alcanmage/megasus

Imports System.Threading
Imports System.Globalization

Public Class Filtern
    Dim patchstring As String = ""
    Dim procarmory As New prozedur_armory
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN

    Public Sub New()
        MyBase.New()

        Thread.CurrentThread.CurrentUICulture = New CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        wait.Show()
        My.Application.DoEvents()
        If bc.Checked = True Then
            patchstring = "2430"
            checkpatch()
        ElseIf wotlk1.Checked = True Then
            patchstring = "3090"
            checkpatch()
        ElseIf wotlk2.Checked = True Then
            patchstring = "3290"
            checkpatch()
        ElseIf wotlk3.Checked = True Then
            patchstring = "3350"
            checkpatch()
        ElseIf cata1.Checked = True Then
            patchstring = "4030"
            checkpatch()
        ElseIf cata2.Checked = True Then
            patchstring = "4060"
            checkpatch()
        ElseIf cata3.Checked = True Then
            patchstring = "4190"
            checkpatch()
        ElseIf cata4.Checked = True Then
            patchstring = "4220"
            checkpatch()
        ElseIf cata5.Checked = True Then
            patchstring = "4300"
            checkpatch()
        Else
            If My.Settings.language = "de" Then
                MsgBox(localeDE.filter_txt1, MsgBoxStyle.Critical, localeDE.connect_txt24)
            Else
                MsgBox(localeEN.filter_txt1, MsgBoxStyle.Critical, localeEN.connect_txt24)
            End If

        End If

        Me.Close()
    End Sub

    Private Sub checkpatch()
        procarmory.checkpatchversion(patchstring)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Filtern_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximumSize = Me.Size
    End Sub

    Private Sub cata3_CheckedChanged(sender As Object, e As EventArgs) Handles cata3.CheckedChanged
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cata5.CheckedChanged
    End Sub
End Class