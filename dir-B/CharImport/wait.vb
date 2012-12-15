'Copyright (C) 2011-2012 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* The form wait will show up during minor operations
'*
'* Developed by Alcanmage/megasus



Public Class wait
    Private Sub OK_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub wait_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximumSize = Me.Size
        My.Application.DoEvents()
    End Sub
End Class
