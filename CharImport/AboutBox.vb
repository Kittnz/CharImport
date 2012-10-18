Imports System.Threading
Imports System.Globalization

Public NotInheritable Class AboutBox
    Public Sub New()
        MyBase.New()

        Thread.CurrentThread.CurrentUICulture = New CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub

    Private Sub AboutBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = "Version " & Me.ProductVersion & " (Test)"

        Me.MaximumSize = Me.Size
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
