Imports System.Net.Mail

Public Class Database_Check
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN
    Dim runfunction As New Functions
    Public Sub New()
        MyBase.New()

        Threading.Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        runfunction.writelog("Cancel Transmission call")
        Main.nowexit = True
        Armory2Database.Close()
        Database2Database.Close()
        Database_Interface.Close()
        Process_Status.Close()
        Connect.Close()
        Starter.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        runfunction.writelog("Continue Transmission call with startcondition: " & Main.startcond.ToString)
        Me.Hide()
        Main.nowgoon = True

        If Main.startcond = 14 Then
            Armory2Database.button4click()
        ElseIf Main.startcond = 23 Then
            Database_Interface.button3click()
        ElseIf Main.startcond = 24 Then
            Database_Interface.button4click()
        ElseIf Main.startcond = 34 Then
            Database2Database.button4click()
        ElseIf Main.startcond = 42 Then
            Connect.button2click()
        ElseIf Main.startcond = 22 Then

            Main.ausgangsformat = 1
            Database2Database.Show()
        Else
        End If
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        runfunction.writelog("Send report call")
        Try

            Dim smtpserver As New SmtpClient()
            Dim mail As New MailMessage()
            smtpserver.Credentials = New Net.NetworkCredential("charimport@gmx.de", ".ciupass#")
            ' smtpserver.Port = 465
            smtpserver.Host = "mail.gmx.net"
            smtpserver.EnableSsl = True
            mail = New MailMessage()
            mail.From = New MailAddress("charimport@gmx.de")
            mail.To.Add("geslauncher@web.de")
            mail.Subject = "Bugreport - CharImport"
            mail.Body = "Fehlerbericht in Version " & Me.ProductVersion & " Tabellen-Überprüfungs-Bericht: " & vbNewLine & report.Text & vbNewLine & "////////////" & vbNewLine & Main.tableschema & vbNewLine & vbNewLine & " / System: " & System.Environment.OSVersion.ToString
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

    Private Sub Database_Check_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        runfunction.writelog("Database_Check_Load call")
        Me.UseWaitCursor = False
        runfunction.writelog("DB Report: " & report.Text & vbNewLine & "Table_Schema: " & vbNewLine & Main.tableschema)
    End Sub

   
   
End Class