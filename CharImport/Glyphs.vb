Imports System.Threading
Imports System.Globalization

Public Class Glyphs
    '****************************************************************************************
    '****************************************************************************************
    '***************************** CharImport- Glyphs ***************************************
    '****************************************************************************************
    '..................Status
    '...................Code:       90%
    '...................Design:     95%
    '...................Functions:  90%
    '****************************************************************************************
    '****************************************************************************************
    '..................Last modified: 06.01.12
    '****************************************************************************************
    '****************************************************************************************
    '..................Comments:
    'Mouse-Over Event hinzufügen (Link zu Buffed/WoWhead)

    Public Sub New()
        MyBase.New()

        Thread.CurrentThread.CurrentUICulture = New CultureInfo(My.Settings.language)
        InitializeComponent()
    End Sub

    Private Sub Glyphs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.MaximumSize = Me.Size
        Panel1.Visible = False
        Button1.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel1.Visible = False
        Button2.Enabled = True
        Button1.Enabled = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Panel1.Visible = True
        Button1.Enabled = True
        Button2.Enabled = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
    End Sub

    Private Sub prim1pic_Click(sender As Object, e As EventArgs) Handles prim1pic.Click
        If Main.primeglyph1 = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & Main.primeglyph1)
        End If
    End Sub

    Private Sub prim2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles prim2pic.Click
        If Main.primeglyph2 = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & Main.primeglyph2)
        End If
    End Sub

    Private Sub prim3pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles prim3pic.Click
        Dim useglyphid As String = Main.primeglyph3
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub erheb1pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles erheb1pic.Click
        Dim useglyphid As String = Main.majorglyph1
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub erheb2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles erheb2pic.Click
        Dim useglyphid As String = Main.majorglyph2
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub erheb3pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles erheb3pic.Click
        Dim useglyphid As String = Main.majorglyph3
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub gering1pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles gering1pic.Click
        Dim useglyphid As String = Main.minorglyph1
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub gering2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles gering2pic.Click
        Dim useglyphid As String = Main.minorglyph2
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub gering3pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles gering3pic.Click
        Dim useglyphid As String = Main.minorglyph3
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secprim1pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secprim1pic.Click
        Dim useglyphid As String = Main.secprimeglyph1
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secprim2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secprim2pic.Click
        Dim useglyphid As String = Main.secprimeglyph2
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secprim3pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secprim3pic.Click
        Dim useglyphid As String = Main.secprimeglyph3
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secerheb1pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secerheb1pic.Click
        Dim useglyphid As String = Main.secmajorglyph1
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secerheb2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secerheb2pic.Click
        Dim useglyphid As String = Main.secmajorglyph2
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secerheb3pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secerheb3pic.Click
        Dim useglyphid As String = Main.secmajorglyph3
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secgering1pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secgering1pic.Click
        Dim useglyphid As String = Main.secminorglyph1
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secgering2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secgering2pic.Click
        Dim useglyphid As String = Main.secminorglyph2
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secgering3pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secgering3pic.Click
        Dim useglyphid As String = Main.secminorglyph3
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub Label6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label6.Click
    End Sub
End Class