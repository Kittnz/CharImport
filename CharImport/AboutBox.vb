﻿'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* This form shows information about the software
'*
'* Developed by Alcanmage/megasus

Imports System.Threading
Imports System.Globalization

Public NotInheritable Class AboutBox
    Public Sub New()
        MyBase.New()

        Thread.CurrentThread.CurrentUICulture = New CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub

    Private Sub AboutBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = "Version " & Me.ProductVersion & " (Beta)"

        Me.MaximumSize = Me.Size
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
