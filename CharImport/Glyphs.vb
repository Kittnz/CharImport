Imports System.Threading
Imports System.Globalization

Public Class Glyphs
    'Copyright (C) 2011-2013 CharImport <http://sourceforge.net/projects/charimport/>
    '*
    '* This application is free and can be distributed.
    '*
    '* The form Glyphs shows the primary and secondary glyphs of a character
    '*
    '* Developed by Alcanmage/megasus

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
        If Main.MainInstance.primeglyph1 = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & Main.MainInstance.primeglyph1)
        End If
    End Sub

    Private Sub prim2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles prim2pic.Click
        If Main.MainInstance.primeglyph2 = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & Main.MainInstance.primeglyph2)
        End If
    End Sub

    Private Sub prim3pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles prim3pic.Click
        Dim useglyphid As String = Main.MainInstance.primeglyph3
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub erheb1pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles erheb1pic.Click
        Dim useglyphid As String = Main.MainInstance.majorglyph1
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub erheb2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles erheb2pic.Click
        Dim useglyphid As String = Main.MainInstance.majorglyph2
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub erheb3pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles erheb3pic.Click
        Dim useglyphid As String = Main.MainInstance.majorglyph3
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub gering1pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles gering1pic.Click
        Dim useglyphid As String = Main.MainInstance.minorglyph1
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub gering2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles gering2pic.Click
        Dim useglyphid As String = Main.MainInstance.minorglyph2
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub gering3pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles gering3pic.Click
        Dim useglyphid As String = Main.MainInstance.minorglyph3
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secprim1pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secprim1pic.Click
        Dim useglyphid As String = Main.MainInstance.secprimeglyph1
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secprim2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secprim2pic.Click
        Dim useglyphid As String = Main.MainInstance.secprimeglyph2
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secprim3pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secprim3pic.Click
        Dim useglyphid As String = Main.MainInstance.secprimeglyph3
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secerheb1pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secerheb1pic.Click
        Dim useglyphid As String = Main.MainInstance.secmajorglyph1
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secerheb2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secerheb2pic.Click
        Dim useglyphid As String = Main.MainInstance.secmajorglyph2
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secerheb3pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secerheb3pic.Click
        Dim useglyphid As String = Main.MainInstance.secmajorglyph3
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secgering1pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secgering1pic.Click
        Dim useglyphid As String = Main.MainInstance.secminorglyph1
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secgering2pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secgering2pic.Click
        Dim useglyphid As String = Main.MainInstance.secminorglyph2
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub secgering3pic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles secgering3pic.Click
        Dim useglyphid As String = Main.MainInstance.secminorglyph3
        If useglyphid = "" Then

        Else
            Process.Start("http://wowhead.com/item=" & useglyphid)
        End If
    End Sub

    Private Sub Label6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Label6.Click
    End Sub
End Class