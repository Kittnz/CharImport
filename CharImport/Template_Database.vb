Imports MySql.Data.MySqlClient
Imports MySqlLib3
Imports System.Text
Public Class Template_Database
    Private db As New MySqlLib("http://wowgeslauncher.bplaced.com/SQLScript/query.php", True)
    Private Sub Template_Database_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadtemplatelist()
    End Sub
    Private Sub loadtemplatelist()
        db.Encoding = Encoding.GetEncoding("ISO-8859-1")
        Dim returnvalue As String = ""
        Try
            Dim rc As MySqlLib.ResultCollection = db.Query("SELECT * FROM `template_store`")
            Dim rowcount As Integer = rc.Row.Count
            Dim numrow As Integer = 0
            Dim listcolcount As Integer = 0
            If rc.Row.Count > 0 Then
                Do
                    For Each kp As Generic.KeyValuePair(Of String, String) In rc.Row(numrow).Columns
                        returnvalue &= kp.Key & " -> " & kp.Value & Environment.NewLine
                    Next
                    Dim str(3) As String
                    Dim itm As ListViewItem
                    str(0) = globalregion.SelectedItem.ToString
                    str(1) = realmname.Text
                    str(2) = charname.Text
                    str(3) = templink
                    itm = New ListViewItem(str)
                    ListView1.Items.Add(itm)
                    ListView1.EnsureVisible(ListView1.Items.Count - 1)
                    ListView1.Update()
                    listcolcount = 0
                    numrow += 1
                Loop Until numrow = rowcount
            End If
        Catch scriptEx As MySqlLib.ScriptException
            MessageBox.Show(scriptEx.Message, "Script Fehler")
        Catch mysqlEx As MySqlLib.MySqlException
            MessageBox.Show(mysqlEx.ToShortString(), "MySql Fehler")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Allgemeiner Fehler")
        End Try
        If Not returnvalue = "" Then


        End If
    End Sub
End Class