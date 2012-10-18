'****************************************************************************************
'****************************************************************************************
'***************************** CharImpor - UpdateMe *****************************************
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

Imports System.Windows.Forms

Public Class UpdateMe

    

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Update_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = Me.Size
    End Sub

    Private Sub OK_Button_Click(sender As System.Object, e As System.EventArgs) Handles OK_Button.Click
        Process.Start(Starter.downloadlink)
        Application.Exit()
    End Sub
End Class
