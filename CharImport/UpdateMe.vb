'Copyright (C) 2011-2012 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* The form UpdateMe informs the user if an update of the application
'* is available and redirects him to the downloadlink.
'*
'* Developed by Alcanmage/megasus



Public Class UpdateMe
    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Update_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximumSize = Me.Size
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Process.Start(Starter.downloadlink)
        Application.Exit()
    End Sub
End Class
