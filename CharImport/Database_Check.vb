'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
'*
'* This application is free and can be distributed.
'*
'* The form Database_Check will show up if certain tables or columns are missing
'* in the database and will inform the user. It also provides the option to submit
'* a bugreport, to continue the transmission or to cancel it.
'*
'* Developed by Alcanmage/megasus

Imports System.Threading
Imports System.Globalization
Imports System.Net.Mail
Imports System.Net

Public Class Database_Check
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN
    Dim runfunction As New Functions

    Public Sub New()
        MyBase.New()

        Thread.CurrentThread.CurrentUICulture = New CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub

    Private Sub Button3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button3.Click
        runfunction.writelog("Cancel Transmission call")
        Main.MainInstance.nowexit = True
        Main.MainInstance.Close()
        Armory2Database.Close()
        Database2Database.Close()
        Database_Interface.Close()
        Process_Status.Close()
        Connect.Close()
        Starter.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button2.Click
        runfunction.writelog("Continue Transmission call with startcondition: " & Main.MainInstance.startcond.ToString)
        Me.Hide()
        Main.MainInstance.nowgoon = True

        If Main.MainInstance.startcond = 14 Then
            Armory2Database.button4click()
        ElseIf Main.MainInstance.startcond = 23 Then
            Database_Interface.button3click()
        ElseIf Main.MainInstance.startcond = 24 Then
            Database_Interface.button4click()
        ElseIf Main.MainInstance.startcond = 34 Then
            Database2Database.button4click()
        ElseIf Main.MainInstance.startcond = 42 Then
            Connect.button2click()
        ElseIf Main.MainInstance.startcond = 22 Then

            Main.MainInstance.ausgangsformat = 1
            Database2Database.Show()
        Else
        End If
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        runfunction.writelog("Send report call")
        Try

            Dim smtpserver As New SmtpClient()
            Dim mail As New MailMessage()
            smtpserver.Credentials = New NetworkCredential("charimport@gmx.de", ".ciupass#")
            ' smtpserver.Port = 465
            smtpserver.Host = "mail.gmx.net"
            smtpserver.EnableSsl = True
            mail = New MailMessage()
            mail.From = New MailAddress("charimport@gmx.de")
            mail.To.Add("geslauncher@web.de")
            mail.Subject = "Bugreport - CharImport"
            mail.Body = "Fehlerbericht in Version " & Me.ProductVersion & " Tabellen-Überprüfungs-Bericht: " & vbNewLine &
                        report.Text & vbNewLine & "////////////" & vbNewLine & Main.MainInstance.tableschema & vbNewLine & vbNewLine &
                        " / System: " & Environment.OSVersion.ToString
            smtpserver.Send(mail)
        Catch ex As Exception
            runfunction.writelog("Error sending report: " & ex.ToString)
        End Try
        If My.Settings.language = "de" Then
            MsgBox(localeDE.database_check_txt1, vbInformation)
        Else
            MsgBox(localeEN.database_check_txt1, vbInformation)
        End If
    End Sub

    Private Sub Database_Check_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        runfunction.writelog("Database_Check_Load call")
        Me.UseWaitCursor = False
        runfunction.writelog("DB Report: " & report.Text & vbNewLine & "Table_Schema: " & vbNewLine & Main.MainInstance.tableschema)
    End Sub
End Class