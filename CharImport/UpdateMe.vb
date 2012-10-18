'****************************************************************************************
'****************************************************************************************
'***************************** CharImport- UpdateMe *****************************************
'****************************************************************************************
'..................Status
'...................Code:       100%
'...................Design:     100%
'...................Functions:  100%
'****************************************************************************************
'****************************************************************************************
'..................Last modified: 12.02.12
'****************************************************************************************
'****************************************************************************************
'..................Comments:
'


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
