'Copyright (C) 2011-2012 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* The form Welcome will show up at the first start of the application and
'* displays the changelog.
'*
'* Developed by Alcanmage/megasus

Public Class Welcome
    Private Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        My.Settings.firststart = False
        My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub Welcome_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Me.Text = "CharImport [Universal] - " & Me.ProductVersion & " (Test)"
        Me.MaximumSize = Me.Size

        Me.BringToFront()
    End Sub


    Private Sub LinkLabel1_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs)
        My.Settings.firststart = True
        My.Settings.Save()
    End Sub

    Private Sub lang_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Sub Button4_Click(ByVal sender As Object, ByVal e As EventArgs)
        My.Settings.firststart = False
        My.Settings.Save()
        Me.Close()
    End Sub
End Class