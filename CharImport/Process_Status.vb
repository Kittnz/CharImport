Public Class Process_Status
    Dim localeDE As New LanguageDE
    Dim localeEN As New LanguageEN
    Public Sub New()
        MyBase.New()

        Threading.Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub
    Private Sub Process_Status_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.MaximumSize = Me.Size
        If My.Settings.language = "de" Then
            processreport.AppendText(Now.TimeOfDay.ToString & localeDE.process_status_txt1 & vbNewLine)
        Else
            processreport.AppendText(Now.TimeOfDay.ToString & localeEN.process_status_txt1 & vbNewLine)
        End If
       End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        My.Settings.shellclose = True
        My.Settings.Save()
        Me.Close()
    End Sub
End Class