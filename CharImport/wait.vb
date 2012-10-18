'****************************************************************************************
'****************************************************************************************
'***************************** CharImpor - Main *****************************************
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

Public Class wait

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub wait_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = Me.Size
        My.Application.DoEvents()

    End Sub
End Class
