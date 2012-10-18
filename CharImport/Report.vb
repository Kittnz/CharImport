'****************************************************************************************
'****************************************************************************************
'***************************** CharImpor - Main *****************************************
'****************************************************************************************
'..................Status
'...................Code:       100%
'...................Design:      95%
'...................Functions:  100%
'****************************************************************************************
'****************************************************************************************
'..................Last modified: 06.01.12
'****************************************************************************************
'****************************************************************************************
'..................Comments:
'


Imports System.Windows.Forms

Public Class xReport



    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Connect.Close()
        Me.Close()
    End Sub

    Private Sub xReport_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Connect.Close()
    End Sub

    Private Sub xReport_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size
    End Sub
End Class
