Public NotInheritable Class AboutBox
    Public Sub New()
        MyBase.New()

        Threading.Thread.CurrentThread.CurrentUICulture = New Globalization.CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub

    Private Sub AboutBox_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Label2.Text = "Version " & Me.ProductVersion & " (Test)"

        Me.MaximumSize = Me.Size
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class
