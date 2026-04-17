<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainPanel
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        txtField1 = New TextBox()
        lblField1 = New Label()
        lblField = New Label()
        lblField2 = New Label()
        txtField2 = New TextBox()
        lblField4 = New Label()
        lblField3 = New Label()
        txtField3 = New TextBox()
        lblField6 = New Label()
        txtField6 = New TextBox()
        lblField5 = New Label()
        txtField5 = New TextBox()
        lblField7 = New Label()
        txtField7 = New TextBox()
        btnCreate = New Button()
        btnClose = New Button()
        cbBox1 = New ComboBox()
        SuspendLayout()
        ' 
        ' txtField1
        ' 
        txtField1.Location = New Point(513, 159)
        txtField1.Name = "txtField1"
        txtField1.Size = New Size(417, 27)
        txtField1.TabIndex = 0
        ' 
        ' lblField1
        ' 
        lblField1.AutoSize = True
        lblField1.Location = New Point(370, 162)
        lblField1.Name = "lblField1"
        lblField1.Size = New Size(83, 20)
        lblField1.TabIndex = 1
        lblField1.Text = "Item Name"
        ' 
        ' lblField
        ' 
        lblField.AutoSize = True
        lblField.Font = New Font("Segoe UI", 17F)
        lblField.Location = New Point(600, 49)
        lblField.Name = "lblField"
        lblField.Size = New Size(116, 40)
        lblField.TabIndex = 2
        lblField.Text = "Product"
        ' 
        ' lblField2
        ' 
        lblField2.AutoSize = True
        lblField2.Location = New Point(370, 211)
        lblField2.Name = "lblField2"
        lblField2.Size = New Size(91, 20)
        lblField2.TabIndex = 4
        lblField2.Text = "Buying price"
        ' 
        ' txtField2
        ' 
        txtField2.Location = New Point(513, 208)
        txtField2.Name = "txtField2"
        txtField2.Size = New Size(417, 27)
        txtField2.TabIndex = 3
        ' 
        ' lblField4
        ' 
        lblField4.AutoSize = True
        lblField4.Location = New Point(370, 310)
        lblField4.Name = "lblField4"
        lblField4.Size = New Size(49, 20)
        lblField4.TabIndex = 8
        lblField4.Text = "Status"
        ' 
        ' lblField3
        ' 
        lblField3.AutoSize = True
        lblField3.Location = New Point(370, 261)
        lblField3.Name = "lblField3"
        lblField3.Size = New Size(94, 20)
        lblField3.TabIndex = 6
        lblField3.Text = "Sellling Price"
        ' 
        ' txtField3
        ' 
        txtField3.Location = New Point(513, 258)
        txtField3.Name = "txtField3"
        txtField3.Size = New Size(417, 27)
        txtField3.TabIndex = 5
        ' 
        ' lblField6
        ' 
        lblField6.AutoSize = True
        lblField6.Location = New Point(370, 405)
        lblField6.Name = "lblField6"
        lblField6.Size = New Size(36, 20)
        lblField6.TabIndex = 12
        lblField6.Text = "Size"
        ' 
        ' txtField6
        ' 
        txtField6.Location = New Point(513, 402)
        txtField6.Name = "txtField6"
        txtField6.Size = New Size(417, 27)
        txtField6.TabIndex = 11
        ' 
        ' lblField5
        ' 
        lblField5.AutoSize = True
        lblField5.Location = New Point(370, 356)
        lblField5.Name = "lblField5"
        lblField5.Size = New Size(45, 20)
        lblField5.TabIndex = 10
        lblField5.Text = "Color"
        ' 
        ' txtField5
        ' 
        txtField5.Location = New Point(513, 353)
        txtField5.Name = "txtField5"
        txtField5.Size = New Size(417, 27)
        txtField5.TabIndex = 9
        ' 
        ' lblField7
        ' 
        lblField7.AutoSize = True
        lblField7.Location = New Point(370, 455)
        lblField7.Name = "lblField7"
        lblField7.Size = New Size(88, 20)
        lblField7.TabIndex = 14
        lblField7.Text = "Stock Count"
        ' 
        ' txtField7
        ' 
        txtField7.Location = New Point(513, 452)
        txtField7.Name = "txtField7"
        txtField7.Size = New Size(417, 27)
        txtField7.TabIndex = 13
        ' 
        ' btnCreate
        ' 
        btnCreate.Location = New Point(981, 248)
        btnCreate.Name = "btnCreate"
        btnCreate.Size = New Size(209, 46)
        btnCreate.TabIndex = 15
        btnCreate.Text = "Create"
        btnCreate.UseVisualStyleBackColor = True
        ' 
        ' btnClose
        ' 
        btnClose.Location = New Point(981, 307)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(209, 46)
        btnClose.TabIndex = 16
        btnClose.Text = "Close"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' cbBox1
        ' 
        cbBox1.FormattingEnabled = True
        cbBox1.Items.AddRange(New Object() {"Available", "Out of Stock", "Pre-Order"})
        cbBox1.Location = New Point(513, 306)
        cbBox1.Name = "cbBox1"
        cbBox1.Size = New Size(417, 28)
        cbBox1.TabIndex = 17
        ' 
        ' MainPanel
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1368, 620)
        Controls.Add(cbBox1)
        Controls.Add(btnClose)
        Controls.Add(btnCreate)
        Controls.Add(lblField7)
        Controls.Add(txtField7)
        Controls.Add(lblField6)
        Controls.Add(txtField6)
        Controls.Add(lblField5)
        Controls.Add(txtField5)
        Controls.Add(lblField4)
        Controls.Add(lblField3)
        Controls.Add(txtField3)
        Controls.Add(lblField2)
        Controls.Add(txtField2)
        Controls.Add(lblField)
        Controls.Add(lblField1)
        Controls.Add(txtField1)
        Name = "MainPanel"
        Text = "Mings"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtField1 As TextBox
    Friend WithEvents lblField1 As Label
    Friend WithEvents lblField As Label
    Friend WithEvents lblField2 As Label
    Friend WithEvents txtField2 As TextBox
    Friend WithEvents lblField4 As Label
    Friend WithEvents lblField3 As Label
    Friend WithEvents txtField3 As TextBox
    Friend WithEvents lblField6 As Label
    Friend WithEvents txtField6 As TextBox
    Friend WithEvents lblField5 As Label
    Friend WithEvents txtField5 As TextBox
    Friend WithEvents lblField7 As Label
    Friend WithEvents txtField7 As TextBox
    Friend WithEvents btnCreate As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents cbBox1 As ComboBox
End Class
