﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Armory2Database
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Armory2Database))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.connectpanel = New System.Windows.Forms.Panel()
        Me.mangos = New System.Windows.Forms.RadioButton()
        Me.trinity1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.auth = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.characters = New System.Windows.Forms.TextBox()
        Me.manually = New System.Windows.Forms.RadioButton()
        Me.automatic = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
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
        Me.arcemu = New System.Windows.Forms.RadioButton()
        Me.optionspanel = New System.Windows.Forms.Panel()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.namechange2 = New System.Windows.Forms.CheckBox()
        Me.namechange1 = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.sockets = New System.Windows.Forms.CheckBox()
        Me.vzs = New System.Windows.Forms.CheckBox()
        Me.items = New System.Windows.Forms.CheckBox()
        Me.race = New System.Windows.Forms.CheckBox()
        Me.level = New System.Windows.Forms.CheckBox()
        Me.playerclass = New System.Windows.Forms.CheckBox()
        Me.talents = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.genderstay = New System.Windows.Forms.CheckBox()
        Me.female = New System.Windows.Forms.CheckBox()
        Me.male = New System.Windows.Forms.CheckBox()
        Me.glyphs = New System.Windows.Forms.CheckBox()
        Me.goldlabel = New System.Windows.Forms.CheckBox()
        Me.alternateleveltext = New System.Windows.Forms.TextBox()
        Me.goldtext = New System.Windows.Forms.TextBox()
        Me.alternatelevellabel = New System.Windows.Forms.CheckBox()
        Me.cata = New System.Windows.Forms.CheckBox()
        Me.wotlk = New System.Windows.Forms.CheckBox()
        Me.tbc = New System.Windows.Forms.CheckBox()
        Me.classic = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.accnames = New System.Windows.Forms.TextBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.connectpanel.SuspendLayout
        Me.GroupBox4.SuspendLayout
        Me.GroupBox9.SuspendLayout
        Me.optionspanel.SuspendLayout
        Me.GroupBox6.SuspendLayout
        Me.GroupBox7.SuspendLayout
        Me.GroupBox8.SuspendLayout
        Me.GroupBox5.SuspendLayout
        Me.GroupBox3.SuspendLayout
        Me.GroupBox1.SuspendLayout
        Me.SuspendLayout
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label1.Name = "Label1"
        '
        'connectpanel
        '
        Me.connectpanel.Controls.Add(Me.mangos)
        Me.connectpanel.Controls.Add(Me.GroupBox4)
        Me.connectpanel.Controls.Add(Me.arcemu)
        Me.connectpanel.Controls.Add(Me.Label5)
        Me.connectpanel.Controls.Add(Me.trinity1)
        resources.ApplyResources(Me.connectpanel, "connectpanel")
        Me.connectpanel.Name = "connectpanel"
        '
        'mangos
        '
        resources.ApplyResources(Me.mangos, "mangos")
        Me.mangos.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.mangos.Name = "mangos"
        Me.mangos.UseVisualStyleBackColor = True
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
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox9)
        Me.GroupBox4.Controls.Add(Me.manually)
        Me.GroupBox4.Controls.Add(Me.automatic)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.Button3)
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
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.TabStop = false
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.Label12)
        Me.GroupBox9.Controls.Add(Me.auth)
        Me.GroupBox9.Controls.Add(Me.Label15)
        Me.GroupBox9.Controls.Add(Me.characters)
        Me.GroupBox9.ForeColor = System.Drawing.Color.CornflowerBlue
        resources.ApplyResources(Me.GroupBox9, "GroupBox9")
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.TabStop = false
        '
        'Label12
        '
        resources.ApplyResources(Me.Label12, "Label12")
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.Label12.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label12.Name = "Label12"
        '
        'auth
        '
        resources.ApplyResources(Me.auth, "auth")
        Me.auth.Name = "auth"
        '
        'Label15
        '
        resources.ApplyResources(Me.Label15, "Label15")
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.Label15.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label15.Name = "Label15"
        '
        'characters
        '
        resources.ApplyResources(Me.characters, "characters")
        Me.characters.Name = "characters"
        '
        'manually
        '
        resources.ApplyResources(Me.manually, "manually")
        Me.manually.Checked = true
        Me.manually.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.manually.Name = "manually"
        Me.manually.TabStop = true
        Me.manually.UseVisualStyleBackColor = true
        '
        'automatic
        '
        resources.ApplyResources(Me.automatic, "automatic")
        Me.automatic.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.automatic.Name = "automatic"
        Me.automatic.UseVisualStyleBackColor = true
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.Label6.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label6.Name = "Label6"
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.DimGray
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Button3, "Button3")
        Me.Button3.Name = "Button3"
        Me.Button3.UseVisualStyleBackColor = false
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.DimGray
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = false
        '
        'Label11
        '
        resources.ApplyResources(Me.Label11, "Label11")
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
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
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.Label10.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label10.Name = "Label10"
        '
        'Button14
        '
        Me.Button14.BackColor = System.Drawing.Color.DimGray
        Me.Button14.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Button14, "Button14")
        Me.Button14.Name = "Button14"
        Me.Button14.UseVisualStyleBackColor = false
        '
        'port
        '
        resources.ApplyResources(Me.port, "port")
        Me.port.Name = "port"
        '
        'Button13
        '
        Me.Button13.BackColor = System.Drawing.Color.DimGray
        Me.Button13.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Button13, "Button13")
        Me.Button13.Name = "Button13"
        Me.Button13.UseVisualStyleBackColor = false
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.Label9.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label9.Name = "Label9"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
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
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.Label8.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label8.Name = "Label8"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label5.Name = "Label5"
        '
        'arcemu
        '
        resources.ApplyResources(Me.arcemu, "arcemu")
        Me.arcemu.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.arcemu.Name = "arcemu"
        Me.arcemu.UseVisualStyleBackColor = true
        '
        'optionspanel
        '
        Me.optionspanel.Controls.Add(Me.GroupBox6)
        Me.optionspanel.Controls.Add(Me.Label4)
        Me.optionspanel.Controls.Add(Me.GroupBox1)
        Me.optionspanel.Controls.Add(Me.Label2)
        resources.ApplyResources(Me.optionspanel, "optionspanel")
        Me.optionspanel.Name = "optionspanel"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Button2)
        Me.GroupBox6.Controls.Add(Me.Button4)
        Me.GroupBox6.Controls.Add(Me.GroupBox7)
        Me.GroupBox6.Controls.Add(Me.cata)
        Me.GroupBox6.Controls.Add(Me.wotlk)
        Me.GroupBox6.Controls.Add(Me.tbc)
        Me.GroupBox6.Controls.Add(Me.classic)
        resources.ApplyResources(Me.GroupBox6, "GroupBox6")
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.TabStop = false
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.DimGray
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Button2, "Button2")
        Me.Button2.Name = "Button2"
        Me.Button2.UseVisualStyleBackColor = false
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.DimGray
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        resources.ApplyResources(Me.Button4, "Button4")
        Me.Button4.Name = "Button4"
        Me.Button4.UseVisualStyleBackColor = false
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.LinkLabel1)
        Me.GroupBox7.Controls.Add(Me.GroupBox8)
        Me.GroupBox7.Controls.Add(Me.GroupBox5)
        Me.GroupBox7.Controls.Add(Me.race)
        Me.GroupBox7.Controls.Add(Me.level)
        Me.GroupBox7.Controls.Add(Me.playerclass)
        Me.GroupBox7.Controls.Add(Me.talents)
        Me.GroupBox7.Controls.Add(Me.GroupBox3)
        Me.GroupBox7.Controls.Add(Me.glyphs)
        Me.GroupBox7.Controls.Add(Me.goldlabel)
        Me.GroupBox7.Controls.Add(Me.alternateleveltext)
        Me.GroupBox7.Controls.Add(Me.goldtext)
        Me.GroupBox7.Controls.Add(Me.alternatelevellabel)
        resources.ApplyResources(Me.GroupBox7, "GroupBox7")
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.TabStop = false
        '
        'LinkLabel1
        '
        resources.ApplyResources(Me.LinkLabel1, "LinkLabel1")
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.TabStop = true
        '
        'GroupBox8
        '
        Me.GroupBox8.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.GroupBox8.Controls.Add(Me.namechange2)
        Me.GroupBox8.Controls.Add(Me.namechange1)
        Me.GroupBox8.ForeColor = System.Drawing.Color.CornflowerBlue
        resources.ApplyResources(Me.GroupBox8, "GroupBox8")
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.TabStop = false
        '
        'namechange2
        '
        resources.ApplyResources(Me.namechange2, "namechange2")
        Me.namechange2.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.namechange2.Checked = true
        Me.namechange2.CheckState = System.Windows.Forms.CheckState.Checked
        Me.namechange2.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.namechange2.Name = "namechange2"
        Me.namechange2.UseVisualStyleBackColor = false
        '
        'namechange1
        '
        resources.ApplyResources(Me.namechange1, "namechange1")
        Me.namechange1.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.namechange1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.namechange1.Name = "namechange1"
        Me.namechange1.UseVisualStyleBackColor = false
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.GroupBox5.Controls.Add(Me.sockets)
        Me.GroupBox5.Controls.Add(Me.vzs)
        Me.GroupBox5.Controls.Add(Me.items)
        resources.ApplyResources(Me.GroupBox5, "GroupBox5")
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.TabStop = false
        '
        'sockets
        '
        resources.ApplyResources(Me.sockets, "sockets")
        Me.sockets.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.sockets.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.sockets.Name = "sockets"
        Me.sockets.UseVisualStyleBackColor = false
        '
        'vzs
        '
        resources.ApplyResources(Me.vzs, "vzs")
        Me.vzs.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.vzs.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.vzs.Name = "vzs"
        Me.vzs.UseVisualStyleBackColor = false
        '
        'items
        '
        resources.ApplyResources(Me.items, "items")
        Me.items.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.items.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.items.Name = "items"
        Me.items.UseVisualStyleBackColor = false
        '
        'race
        '
        resources.ApplyResources(Me.race, "race")
        Me.race.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.race.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.race.Name = "race"
        Me.race.UseVisualStyleBackColor = false
        '
        'level
        '
        resources.ApplyResources(Me.level, "level")
        Me.level.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.level.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.level.Name = "level"
        Me.level.UseVisualStyleBackColor = false
        '
        'playerclass
        '
        resources.ApplyResources(Me.playerclass, "playerclass")
        Me.playerclass.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.playerclass.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.playerclass.Name = "playerclass"
        Me.playerclass.UseVisualStyleBackColor = false
        '
        'talents
        '
        resources.ApplyResources(Me.talents, "talents")
        Me.talents.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.talents.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.talents.Name = "talents"
        Me.talents.UseVisualStyleBackColor = false
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.GroupBox3.Controls.Add(Me.genderstay)
        Me.GroupBox3.Controls.Add(Me.female)
        Me.GroupBox3.Controls.Add(Me.male)
        Me.GroupBox3.ForeColor = System.Drawing.Color.CornflowerBlue
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = false
        '
        'genderstay
        '
        resources.ApplyResources(Me.genderstay, "genderstay")
        Me.genderstay.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.genderstay.Checked = true
        Me.genderstay.CheckState = System.Windows.Forms.CheckState.Checked
        Me.genderstay.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.genderstay.Name = "genderstay"
        Me.genderstay.UseVisualStyleBackColor = false
        '
        'female
        '
        resources.ApplyResources(Me.female, "female")
        Me.female.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.female.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.female.Name = "female"
        Me.female.UseVisualStyleBackColor = false
        '
        'male
        '
        resources.ApplyResources(Me.male, "male")
        Me.male.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.male.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.male.Name = "male"
        Me.male.UseVisualStyleBackColor = false
        '
        'glyphs
        '
        resources.ApplyResources(Me.glyphs, "glyphs")
        Me.glyphs.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.glyphs.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.glyphs.Name = "glyphs"
        Me.glyphs.UseVisualStyleBackColor = false
        '
        'goldlabel
        '
        resources.ApplyResources(Me.goldlabel, "goldlabel")
        Me.goldlabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.goldlabel.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.goldlabel.Name = "goldlabel"
        Me.goldlabel.UseVisualStyleBackColor = false
        '
        'alternateleveltext
        '
        resources.ApplyResources(Me.alternateleveltext, "alternateleveltext")
        Me.alternateleveltext.Name = "alternateleveltext"
        '
        'goldtext
        '
        resources.ApplyResources(Me.goldtext, "goldtext")
        Me.goldtext.Name = "goldtext"
        '
        'alternatelevellabel
        '
        resources.ApplyResources(Me.alternatelevellabel, "alternatelevellabel")
        Me.alternatelevellabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.alternatelevellabel.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.alternatelevellabel.Name = "alternatelevellabel"
        Me.alternatelevellabel.UseVisualStyleBackColor = false
        '
        'cata
        '
        resources.ApplyResources(Me.cata, "cata")
        Me.cata.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.cata.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.cata.Name = "cata"
        Me.cata.UseVisualStyleBackColor = false
        '
        'wotlk
        '
        resources.ApplyResources(Me.wotlk, "wotlk")
        Me.wotlk.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.wotlk.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.wotlk.Name = "wotlk"
        Me.wotlk.UseVisualStyleBackColor = false
        '
        'tbc
        '
        resources.ApplyResources(Me.tbc, "tbc")
        Me.tbc.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.tbc.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.tbc.Name = "tbc"
        Me.tbc.UseVisualStyleBackColor = false
        '
        'classic
        '
        resources.ApplyResources(Me.classic, "classic")
        Me.classic.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.classic.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.classic.Name = "classic"
        Me.classic.UseVisualStyleBackColor = false
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label4.Name = "Label4"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.accnames)
        Me.GroupBox1.Controls.Add(Me.CheckBox3)
        Me.GroupBox1.Controls.Add(Me.CheckBox1)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = false
        '
        'accnames
        '
        resources.ApplyResources(Me.accnames, "accnames")
        Me.accnames.Name = "accnames"
        '
        'CheckBox3
        '
        resources.ApplyResources(Me.CheckBox3, "CheckBox3")
        Me.CheckBox3.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.UseVisualStyleBackColor = true
        '
        'CheckBox1
        '
        resources.ApplyResources(Me.CheckBox1, "CheckBox1")
        Me.CheckBox1.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.UseVisualStyleBackColor = true
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.Color.CornflowerBlue
        Me.Label2.Name = "Label2"
        '
        'Timer1
        '
        Me.Timer1.Enabled = true
        '
        'Armory2Database
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(38,Byte),Integer), CType(CType(50,Byte),Integer), CType(CType(82,Byte),Integer))
        Me.Controls.Add(Me.optionspanel)
        Me.Controls.Add(Me.connectpanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Armory2Database"
        Me.connectpanel.ResumeLayout(false)
        Me.connectpanel.PerformLayout
        Me.GroupBox4.ResumeLayout(false)
        Me.GroupBox4.PerformLayout
        Me.GroupBox9.ResumeLayout(false)
        Me.GroupBox9.PerformLayout
        Me.optionspanel.ResumeLayout(false)
        Me.optionspanel.PerformLayout
        Me.GroupBox6.ResumeLayout(false)
        Me.GroupBox6.PerformLayout
        Me.GroupBox7.ResumeLayout(false)
        Me.GroupBox7.PerformLayout
        Me.GroupBox8.ResumeLayout(false)
        Me.GroupBox8.PerformLayout
        Me.GroupBox5.ResumeLayout(false)
        Me.GroupBox5.PerformLayout
        Me.GroupBox3.ResumeLayout(false)
        Me.GroupBox3.PerformLayout
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents connectpanel As System.Windows.Forms.Panel
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
    Friend WithEvents optionspanel As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents mangos As System.Windows.Forms.RadioButton
    Friend WithEvents arcemu As System.Windows.Forms.RadioButton
    Friend WithEvents trinity1 As System.Windows.Forms.RadioButton
    Friend WithEvents accnames As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents sockets As System.Windows.Forms.CheckBox
    Friend WithEvents vzs As System.Windows.Forms.CheckBox
    Friend WithEvents items As System.Windows.Forms.CheckBox
    Friend WithEvents race As System.Windows.Forms.CheckBox
    Friend WithEvents level As System.Windows.Forms.CheckBox
    Friend WithEvents playerclass As System.Windows.Forms.CheckBox
    Friend WithEvents talents As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents female As System.Windows.Forms.CheckBox
    Friend WithEvents male As System.Windows.Forms.CheckBox
    Friend WithEvents glyphs As System.Windows.Forms.CheckBox
    Friend WithEvents goldlabel As System.Windows.Forms.CheckBox
    Friend WithEvents alternateleveltext As System.Windows.Forms.TextBox
    Friend WithEvents goldtext As System.Windows.Forms.TextBox
    Friend WithEvents alternatelevellabel As System.Windows.Forms.CheckBox
    Friend WithEvents cata As System.Windows.Forms.CheckBox
    Friend WithEvents wotlk As System.Windows.Forms.CheckBox
    Friend WithEvents tbc As System.Windows.Forms.CheckBox
    Friend WithEvents classic As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents namechange2 As System.Windows.Forms.CheckBox
    Friend WithEvents namechange1 As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents auth As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents manually As System.Windows.Forms.RadioButton
    Friend WithEvents automatic As System.Windows.Forms.RadioButton
    Friend WithEvents characters As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents genderstay As System.Windows.Forms.CheckBox
End Class
