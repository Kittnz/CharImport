'****************************************************************************************
'****************************************************************************************
'***************************** CharImport- Main *****************************************
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
