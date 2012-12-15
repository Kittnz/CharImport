<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Database_Interface
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Database_Interface))
        Me.connectpanel = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cata = New System.Windows.Forms.CheckBox()
        Me.mangos = New System.Windows.Forms.RadioButton()
        Me.wotlk = New System.Windows.Forms.CheckBox()
        Me.arcemu = New System.Windows.Forms.RadioButton()
        Me.tbc = New System.Windows.Forms.CheckBox()
        Me.trinity1 = New System.Windows.Forms.RadioButton()
        Me.classic = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.characters = New System.Windows.Forms.TextBox()
        Me.auth = New System.Windows.Forms.TextBox()
        Me.manually = New System.Windows.Forms.RadioButton()
        Me.automatic = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CoreCheck = New System.Windows.Forms.LinkLabel()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.address = New System.Windows.Forms.TextBox()
        Me.xlabel = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.port = New System.Windows.Forms.TextBox()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.user = New System.Windows.Forms.TextBox()
        Me.password = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.optionspanel = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.levelmax = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.gmlevel = New System.Windows.Forms.TextBox()
        Me.gmlevelcheck = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.datemax = New System.Windows.Forms.DateTimePicker()
        Me.datemin = New System.Windows.Forms.DateTimePicker()
        Me.lastlogincheck = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.levelmin = New System.Windows.Forms.TextBox()
        Me.levelrangecheck = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.accnames = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.charnames = New System.Windows.Forms.TextBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.connectpanel.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.optionspanel.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'connectpanel
        '
        resources.ApplyResources(Me.connectpanel, "connectpanel")
        Me.connectpanel.Controls.Add(Me.GroupBox2)
        Me.connectpanel.Controls.Add(Me.Label3)
        Me.connectpanel.Controls.Add(Me.GroupBox4)
        Me.connectpanel.Controls.Add(Me.Label5)
        Me.connectpanel.Name = "connectpanel"
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Controls.Add(Me.cata)
        Me.GroupBox2.Controls.Add(Me.mangos)
        Me.GroupBox2.Controls.Add(Me.wotlk)
        Me.GroupBox2.Controls.Add(Me.arcemu)
        Me.GroupBox2.Controls.Add(Me.tbc)
        Me.GroupBox2.Controls.Add(Me.trinity1)
        Me.GroupBox2.Controls.Add(Me.classic)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'cata
        '
        resources.ApplyResources(Me.cata, "cata")
        Me.cata.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.cata.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.cata.Name = "cata"
        Me.cata.UseVisualStyleBackColor = False
        '
        'mangos
        '
        resources.ApplyResources(Me.mangos, "mangos")
        Me.mangos.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.mangos.Name = "mangos"
        Me.mangos.UseVisualStyleBackColor = True
        '
        'wotlk
        '
        resources.ApplyResources(Me.wotlk, "wotlk")
        Me.wotlk.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.wotlk.Checked = True
        Me.wotlk.CheckState = System.Windows.Forms.CheckState.Checked
        Me.wotlk.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.wotlk.Name = "wotlk"
        Me.wotlk.UseVisualStyleBackColor = False
        '
        'arcemu
        '
        resources.ApplyResources(Me.arcemu, "arcemu")
        Me.arcemu.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.arcemu.Name = "arcemu"
        Me.arcemu.UseVisualStyleBackColor = True
        '
        'tbc
        '
        resources.ApplyResources(Me.tbc, "tbc")
        Me.tbc.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.tbc.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.tbc.Name = "tbc"
        Me.tbc.UseVisualStyleBackColor = False
        '
        'trinity1
        '
        resources.ApplyResources(Me.trinity1, "trinity1")
        Me.trinity1.Checked = True
        Me.trinity1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.trinity1.Name = "trinity1"
        Me.trinity1.TabStop = True
        Me.trinity1.UseVisualStyleBackColor = True
        '
        'classic
        '
        resources.ApplyResources(Me.classic, "classic")
        Me.classic.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.classic.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.classic.Name = "classic"
        Me.classic.UseVisualStyleBackColor = False
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label3.Name = "Label3"
        '
        'GroupBox4
        '
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.Controls.Add(Me.GroupBox9)
        Me.GroupBox4.Controls.Add(Me.manually)
        Me.GroupBox4.Controls.Add(Me.automatic)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.CoreCheck)
        Me.GroupBox4.Controls.Add(Me.Button7)
        Me.GroupBox4.Controls.Add(Me.Button1)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.address)
        Me.GroupBox4.Controls.Add(Me.xlabel)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.Button14)
        Me.GroupBox4.Controls.Add(Me.port)
        Me.GroupBox4.Controls.Add(Me.Button13)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.user)
        Me.GroupBox4.Controls.Add(Me.password)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.TabStop = False
        '
        'GroupBox9
        '
        resources.ApplyResources(Me.GroupBox9, "GroupBox9")
        Me.GroupBox9.Controls.Add(Me.Label14)
        Me.GroupBox9.Controls.Add(Me.Label15)
        Me.GroupBox9.Controls.Add(Me.characters)
        Me.GroupBox9.Controls.Add(Me.auth)
        Me.GroupBox9.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.TabStop = False
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Label14.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label14.Name = "Label14"
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Label15.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label15.Name = "Label15"
        '
        'characters
        '
        resources.ApplyResources(Me.characters, "characters")
        Me.characters.Name = "characters"
        '
        'auth
        '
        resources.ApplyResources(Me.auth, "auth")
        Me.auth.Name = "auth"
        '
        'manually
        '
        resources.ApplyResources(Me.manually, "manually")
        Me.manually.Checked = True
        Me.manually.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.manually.Name = "manually"
        Me.manually.TabStop = True
        Me.manually.UseVisualStyleBackColor = True
        '
        'automatic
        '
        resources.ApplyResources(Me.automatic, "automatic")
        Me.automatic.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.automatic.Name = "automatic"
        Me.automatic.UseVisualStyleBackColor = True
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Label6.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label6.Name = "Label6"
        '
        'CoreCheck
        '
        resources.ApplyResources(Me.CoreCheck, "CoreCheck")
        Me.CoreCheck.LinkColor = System.Drawing.Color.White
        Me.CoreCheck.Name = "CoreCheck"
        Me.CoreCheck.TabStop = True
        '
        'Button7
        '
        resources.ApplyResources(Me.Button7, "Button7")
        Me.Button7.BackColor = System.Drawing.Color.DimGray
        Me.Button7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button7.Name = "Button7"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.BackColor = System.Drawing.Color.DimGray
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Label11.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label11.Name = "Label11"
        '
        'address
        '
        resources.ApplyResources(Me.address, "address")
        Me.address.Name = "address"
        '
        'xlabel
        '
        resources.ApplyResources(Me.xlabel, "xlabel")
        Me.xlabel.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.xlabel.Name = "xlabel"
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Label10.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label10.Name = "Label10"
        '
        'Button14
        '
        resources.ApplyResources(Me.Button14, "Button14")
        Me.Button14.BackColor = System.Drawing.Color.DimGray
        Me.Button14.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button14.Name = "Button14"
        Me.Button14.UseVisualStyleBackColor = False
        '
        'port
        '
        resources.ApplyResources(Me.port, "port")
        Me.port.Name = "port"
        '
        'Button13
        '
        resources.ApplyResources(Me.Button13, "Button13")
        Me.Button13.BackColor = System.Drawing.Color.DimGray
        Me.Button13.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button13.Name = "Button13"
        Me.Button13.UseVisualStyleBackColor = False
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Label9.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label9.Name = "Label9"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Label7.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label7.Name = "Label7"
        '
        'user
        '
        resources.ApplyResources(Me.user, "user")
        Me.user.Name = "user"
        '
        'password
        '
        resources.ApplyResources(Me.password, "password")
        Me.password.Name = "password"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Label8.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label8.Name = "Label8"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label5.Name = "Label5"
        '
        'optionspanel
        '
        resources.ApplyResources(Me.optionspanel, "optionspanel")
        Me.optionspanel.Controls.Add(Me.GroupBox1)
        Me.optionspanel.Controls.Add(Me.Button5)
        Me.optionspanel.Controls.Add(Me.Button3)
        Me.optionspanel.Controls.Add(Me.Button2)
        Me.optionspanel.Controls.Add(Me.Button4)
        Me.optionspanel.Controls.Add(Me.Label4)
        Me.optionspanel.Controls.Add(Me.Label2)
        Me.optionspanel.Controls.Add(Me.charnames)
        Me.optionspanel.Controls.Add(Me.CheckBox3)
        Me.optionspanel.Name = "optionspanel"
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.CheckBox2)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        Me.GroupBox1.Controls.Add(Me.accnames)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'GroupBox3
        '
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.levelmax)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.gmlevel)
        Me.GroupBox3.Controls.Add(Me.gmlevelcheck)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.datemax)
        Me.GroupBox3.Controls.Add(Me.datemin)
        Me.GroupBox3.Controls.Add(Me.lastlogincheck)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.levelmin)
        Me.GroupBox3.Controls.Add(Me.levelrangecheck)
        Me.GroupBox3.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        '
        'Label17
        '
        resources.ApplyResources(Me.Label17, "Label17")
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Label17.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label17.Name = "Label17"
        '
        'levelmax
        '
        resources.ApplyResources(Me.levelmax, "levelmax")
        Me.levelmax.Name = "levelmax"
        '
        'Label16
        '
        resources.ApplyResources(Me.Label16, "Label16")
        Me.Label16.Name = "Label16"
        '
        'gmlevel
        '
        resources.ApplyResources(Me.gmlevel, "gmlevel")
        Me.gmlevel.Name = "gmlevel"
        '
        'gmlevelcheck
        '
        resources.ApplyResources(Me.gmlevelcheck, "gmlevelcheck")
        Me.gmlevelcheck.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.gmlevelcheck.Name = "gmlevelcheck"
        Me.gmlevelcheck.UseVisualStyleBackColor = True
        '
        'Label13
        '
        resources.ApplyResources(Me.Label13, "Label13")
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Label13.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label13.Name = "Label13"
        '
        'datemax
        '
        resources.ApplyResources(Me.datemax, "datemax")
        Me.datemax.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.datemax.Name = "datemax"
        '
        'datemin
        '
        resources.ApplyResources(Me.datemin, "datemin")
        Me.datemin.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.datemin.Name = "datemin"
        '
        'lastlogincheck
        '
        resources.ApplyResources(Me.lastlogincheck, "lastlogincheck")
        Me.lastlogincheck.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.lastlogincheck.Name = "lastlogincheck"
        Me.lastlogincheck.UseVisualStyleBackColor = True
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Label12.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label12.Name = "Label12"
        '
        'levelmin
        '
        resources.ApplyResources(Me.levelmin, "levelmin")
        Me.levelmin.Name = "levelmin"
        '
        'levelrangecheck
        '
        resources.ApplyResources(Me.levelrangecheck, "levelrangecheck")
        Me.levelrangecheck.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.levelrangecheck.Name = "levelrangecheck"
        Me.levelrangecheck.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        resources.ApplyResources(Me.CheckBox2, "CheckBox2")
        Me.CheckBox2.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        resources.ApplyResources(Me.CheckBox1, "CheckBox1")
        Me.CheckBox1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'accnames
        '
        resources.ApplyResources(Me.accnames, "accnames")
        Me.accnames.Name = "accnames"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label1.Name = "Label1"
        '
        'Button5
        '
        resources.ApplyResources(Me.Button5, "Button5")
        Me.Button5.BackColor = System.Drawing.Color.DimGray
        Me.Button5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button5.Name = "Button5"
        Me.Button5.UseVisualStyleBackColor = False
        '
        'Button3
        '
        resources.ApplyResources(Me.Button3, "Button3")
        Me.Button3.BackColor = System.Drawing.Color.DimGray
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.Name = "Button3"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.BackColor = System.Drawing.Color.DimGray
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.Name = "Button2"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button4
        '
        resources.ApplyResources(Me.Button4, "Button4")
        Me.Button4.BackColor = System.Drawing.Color.DimGray
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.Name = "Button4"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label4.Name = "Label4"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label2.Name = "Label2"
        '
        'charnames
        '
        resources.ApplyResources(Me.charnames, "charnames")
        Me.charnames.Name = "charnames"
        '
        'CheckBox3
        '
        resources.ApplyResources(Me.CheckBox3, "CheckBox3")
        Me.CheckBox3.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'Database_Interface
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(38, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(82, Byte), Integer))
        Me.Controls.Add(Me.optionspanel)
        Me.Controls.Add(Me.connectpanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Database_Interface"
        Me.connectpanel.ResumeLayout(False)
        Me.connectpanel.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.optionspanel.ResumeLayout(False)
        Me.optionspanel.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents connectpanel As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents mangos As System.Windows.Forms.RadioButton
    Friend WithEvents arcemu As System.Windows.Forms.RadioButton
    Friend WithEvents trinity1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents address As System.Windows.Forms.TextBox
    Friend WithEvents xlabel As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents port As System.Windows.Forms.TextBox
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents user As System.Windows.Forms.TextBox
    Friend WithEvents password As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents optionspanel As System.Windows.Forms.Panel
    Friend WithEvents accnames As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents charnames As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents CoreCheck As System.Windows.Forms.LinkLabel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents auth As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents characters As System.Windows.Forms.TextBox
    Friend WithEvents manually As System.Windows.Forms.RadioButton
    Friend WithEvents automatic As System.Windows.Forms.RadioButton
    Friend WithEvents cata As System.Windows.Forms.CheckBox
    Friend WithEvents wotlk As System.Windows.Forms.CheckBox
    Friend WithEvents tbc As System.Windows.Forms.CheckBox
    Friend WithEvents classic As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lastlogincheck As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents levelmin As System.Windows.Forms.TextBox
    Friend WithEvents levelrangecheck As System.Windows.Forms.CheckBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents gmlevel As System.Windows.Forms.TextBox
    Friend WithEvents gmlevelcheck As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents datemax As System.Windows.Forms.DateTimePicker
    Friend WithEvents datemin As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents levelmax As System.Windows.Forms.TextBox
End Class
