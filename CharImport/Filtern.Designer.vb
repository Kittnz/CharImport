<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Filtern
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Filtern))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.wotlk3 = New System.Windows.Forms.RadioButton()
        Me.cata2 = New System.Windows.Forms.RadioButton()
        Me.cata1 = New System.Windows.Forms.RadioButton()
        Me.cata3 = New System.Windows.Forms.RadioButton()
        Me.cata4 = New System.Windows.Forms.RadioButton()
        Me.bc = New System.Windows.Forms.RadioButton()
        Me.wotlk1 = New System.Windows.Forms.RadioButton()
        Me.wotlk2 = New System.Windows.Forms.RadioButton()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.cata5 = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label1.Name = "Label1"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.Color.RoyalBlue
        Me.Label2.Name = "Label2"
        '
        'wotlk3
        '
        resources.ApplyResources(Me.wotlk3, "wotlk3")
        Me.wotlk3.ForeColor = System.Drawing.Color.RoyalBlue
        Me.wotlk3.Name = "wotlk3"
        Me.wotlk3.TabStop = True
        Me.wotlk3.UseVisualStyleBackColor = True
        '
        'cata2
        '
        resources.ApplyResources(Me.cata2, "cata2")
        Me.cata2.ForeColor = System.Drawing.Color.RoyalBlue
        Me.cata2.Name = "cata2"
        Me.cata2.TabStop = True
        Me.cata2.UseVisualStyleBackColor = True
        '
        'cata1
        '
        resources.ApplyResources(Me.cata1, "cata1")
        Me.cata1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.cata1.Name = "cata1"
        Me.cata1.TabStop = True
        Me.cata1.UseVisualStyleBackColor = True
        '
        'cata3
        '
        resources.ApplyResources(Me.cata3, "cata3")
        Me.cata3.ForeColor = System.Drawing.Color.RoyalBlue
        Me.cata3.Name = "cata3"
        Me.cata3.TabStop = True
        Me.cata3.UseVisualStyleBackColor = True
        '
        'cata4
        '
        resources.ApplyResources(Me.cata4, "cata4")
        Me.cata4.ForeColor = System.Drawing.Color.RoyalBlue
        Me.cata4.Name = "cata4"
        Me.cata4.TabStop = True
        Me.cata4.UseVisualStyleBackColor = True
        '
        'bc
        '
        resources.ApplyResources(Me.bc, "bc")
        Me.bc.ForeColor = System.Drawing.Color.RoyalBlue
        Me.bc.Name = "bc"
        Me.bc.TabStop = True
        Me.bc.UseVisualStyleBackColor = True
        '
        'wotlk1
        '
        resources.ApplyResources(Me.wotlk1, "wotlk1")
        Me.wotlk1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.wotlk1.Name = "wotlk1"
        Me.wotlk1.TabStop = True
        Me.wotlk1.UseVisualStyleBackColor = True
        '
        'wotlk2
        '
        resources.ApplyResources(Me.wotlk2, "wotlk2")
        Me.wotlk2.ForeColor = System.Drawing.Color.RoyalBlue
        Me.wotlk2.Name = "wotlk2"
        Me.wotlk2.TabStop = True
        Me.wotlk2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.BackColor = System.Drawing.Color.DimGray
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button2
        '
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.BackColor = System.Drawing.Color.DimGray
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.Name = "Button2"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'cata5
        '
        resources.ApplyResources(Me.cata5, "cata5")
        Me.cata5.ForeColor = System.Drawing.Color.RoyalBlue
        Me.cata5.Name = "cata5"
        Me.cata5.TabStop = True
        Me.cata5.UseVisualStyleBackColor = True
        '
        'Filtern
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Controls.Add(Me.cata5)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.wotlk2)
        Me.Controls.Add(Me.wotlk1)
        Me.Controls.Add(Me.bc)
        Me.Controls.Add(Me.cata4)
        Me.Controls.Add(Me.cata3)
        Me.Controls.Add(Me.cata1)
        Me.Controls.Add(Me.cata2)
        Me.Controls.Add(Me.wotlk3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Filtern"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents wotlk3 As System.Windows.Forms.RadioButton
    Friend WithEvents cata2 As System.Windows.Forms.RadioButton
    Friend WithEvents cata1 As System.Windows.Forms.RadioButton
    Friend WithEvents cata3 As System.Windows.Forms.RadioButton
    Friend WithEvents cata4 As System.Windows.Forms.RadioButton
    Friend WithEvents bc As System.Windows.Forms.RadioButton
    Friend WithEvents wotlk1 As System.Windows.Forms.RadioButton
    Friend WithEvents wotlk2 As System.Windows.Forms.RadioButton
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents cata5 As System.Windows.Forms.RadioButton
End Class
