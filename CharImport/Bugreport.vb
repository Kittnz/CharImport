Imports MySql.Data.MySqlClient
Imports System.Net.Mail

Public Class Bugreport
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN

    Public Sub New()
        MyBase.New()

        Threading.Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub


    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        If sendername.Text = "" Then
            If My.Settings.language = "de" Then
                MsgBox(localeDE.bugreport_txt1, MsgBoxStyle.Critical, localeDE.bugreport_txt2)
            Else
                MsgBox(localeEN.bugreport_txt1, MsgBoxStyle.Critical, localeEN.bugreport_txt2)
            End If
             ElseIf message.Text = "" Then
            If My.Settings.language = "de" Then
                MsgBox(localeDE.bugreport_txt3, MsgBoxStyle.Critical, localeDE.bugreport_txt4)
            Else
                MsgBox(localeEN.bugreport_txt3, MsgBoxStyle.Critical, localeEN.bugreport_txt4)
            End If
        ElseIf message.Text.Length > 1000 Then
            If My.Settings.language = "de" Then
                MsgBox(localeDE.bugreport_txt5, MsgBoxStyle.Critical, localeDE.bugreport_txt6)
            Else
                MsgBox(localeEN.bugreport_txt5, MsgBoxStyle.Critical, localeEN.bugreport_txt6)
            End If
        Else
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
                mail.Body = "Fehlerbericht von " & sendername.Text & " in Version " & Me.ProductVersion & " Nachricht: " & vbNewLine & vbNewLine & message.Text & vbNewLine & vbNewLine & " / System: " & System.Environment.OSVersion.ToString
                smtpserver.Send(mail)
            Catch ex As Exception

            End Try


            If My.Settings.language = "de" Then
                MsgBox(localeDE.bugreport_txt7, vbInformation, localeDE.bugreport_txt8)
            Else
                MsgBox(localeEN.bugreport_txt7, vbInformation, localeEN.bugreport_txt8)
            End If

            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Main.BringToFront()
        Me.Close()
    End Sub

    Private Sub Bugreport_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = Me.Size
    End Sub
End Class