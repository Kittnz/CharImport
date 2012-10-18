Public Class Welcome

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        My.Settings.firststart = False
        My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub Welcome_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Text = "CharImport [Universal] - " & Me.ProductVersion & " (Test)"
        Me.MaximumSize = Me.Size

        Me.BringToFront()
     
    End Sub

    


    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        My.Settings.firststart = True
        My.Settings.Save()
    End Sub

    Private Sub lang_SelectedValueChanged(sender As Object, e As System.EventArgs)

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs)
        My.Settings.firststart = False
        My.Settings.Save()
        Me.Close()
    End Sub
End Class