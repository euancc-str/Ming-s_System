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
        btnSave = New Button()
        cbBox1 = New ComboBox()
        btnDelete = New Button()
        dgvReport = New DataGridView()
        btnClear = New Button()
        Label1 = New Label()
        cboReport = New ComboBox()
        btnRunReport = New Button()
        dgvData = New DataGridView()
        CType(dgvReport, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvData, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' txtField1
        ' 
        txtField1.Font = New Font("Segoe UI", 13F)
        txtField1.Location = New Point(75, 120)
        txtField1.Name = "txtField1"
        txtField1.Size = New Size(250, 36)
        txtField1.TabIndex = 0
        ' 
        ' lblField1
        ' 
        lblField1.AutoSize = True
        lblField1.Location = New Point(75, 90)
        lblField1.Name = "lblField1"
        lblField1.Size = New Size(83, 20)
        lblField1.TabIndex = 1
        lblField1.Text = "Item Name"
        ' 
        ' lblField
        ' 
        lblField.AutoSize = True
        lblField.Font = New Font("Segoe UI", 15F)
        lblField.Location = New Point(84, 37)
        lblField.Name = "lblField"
        lblField.Size = New Size(175, 35)
        lblField.TabIndex = 2
        lblField.Text = "Record Details"
        ' 
        ' lblField2
        ' 
        lblField2.AutoSize = True
        lblField2.Location = New Point(409, 90)
        lblField2.Name = "lblField2"
        lblField2.Size = New Size(91, 20)
        lblField2.TabIndex = 4
        lblField2.Text = "Buying price"
        ' 
        ' txtField2
        ' 
        txtField2.Font = New Font("Segoe UI", 13F)
        txtField2.Location = New Point(409, 120)
        txtField2.Name = "txtField2"
        txtField2.Size = New Size(289, 36)
        txtField2.TabIndex = 3
        ' 
        ' lblField4
        ' 
        lblField4.AutoSize = True
        lblField4.Location = New Point(1116, 90)
        lblField4.Name = "lblField4"
        lblField4.Size = New Size(49, 20)
        lblField4.TabIndex = 8
        lblField4.Text = "Status"
        ' 
        ' lblField3
        ' 
        lblField3.AutoSize = True
        lblField3.Location = New Point(772, 90)
        lblField3.Name = "lblField3"
        lblField3.Size = New Size(94, 20)
        lblField3.TabIndex = 6
        lblField3.Text = "Sellling Price"
        ' 
        ' txtField3
        ' 
        txtField3.Font = New Font("Segoe UI", 13F)
        txtField3.Location = New Point(772, 120)
        txtField3.Name = "txtField3"
        txtField3.Size = New Size(289, 36)
        txtField3.TabIndex = 5
        ' 
        ' lblField6
        ' 
        lblField6.AutoSize = True
        lblField6.Location = New Point(409, 188)
        lblField6.Name = "lblField6"
        lblField6.Size = New Size(36, 20)
        lblField6.TabIndex = 12
        lblField6.Text = "Size"
        ' 
        ' txtField6
        ' 
        txtField6.Font = New Font("Segoe UI", 13F)
        txtField6.Location = New Point(409, 233)
        txtField6.Name = "txtField6"
        txtField6.Size = New Size(289, 36)
        txtField6.TabIndex = 11
        ' 
        ' lblField5
        ' 
        lblField5.AutoSize = True
        lblField5.Location = New Point(75, 188)
        lblField5.Name = "lblField5"
        lblField5.Size = New Size(45, 20)
        lblField5.TabIndex = 10
        lblField5.Text = "Color"
        ' 
        ' txtField5
        ' 
        txtField5.Font = New Font("Segoe UI", 13F)
        txtField5.Location = New Point(75, 233)
        txtField5.Name = "txtField5"
        txtField5.Size = New Size(250, 36)
        txtField5.TabIndex = 9
        ' 
        ' lblField7
        ' 
        lblField7.AutoSize = True
        lblField7.Location = New Point(772, 188)
        lblField7.Name = "lblField7"
        lblField7.Size = New Size(88, 20)
        lblField7.TabIndex = 14
        lblField7.Text = "Stock Count"
        ' 
        ' txtField7
        ' 
        txtField7.Font = New Font("Segoe UI", 13F)
        txtField7.Location = New Point(772, 233)
        txtField7.Name = "txtField7"
        txtField7.Size = New Size(289, 36)
        txtField7.TabIndex = 13
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(84, 487)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(220, 61)
        btnSave.TabIndex = 15
        btnSave.Text = "Save Record"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' cbBox1
        ' 
        cbBox1.Font = New Font("Segoe UI", 13F)
        cbBox1.FormattingEnabled = True
        cbBox1.Items.AddRange(New Object() {"Available", "Out of Stock", "Pre-Order"})
        cbBox1.Location = New Point(1116, 120)
        cbBox1.Name = "cbBox1"
        cbBox1.Size = New Size(299, 38)
        cbBox1.TabIndex = 17
        ' 
        ' btnDelete
        ' 
        btnDelete.BackColor = Color.Red
        btnDelete.Location = New Point(1088, 487)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(168, 77)
        btnDelete.TabIndex = 20
        btnDelete.Text = "Delete Record?"
        btnDelete.UseVisualStyleBackColor = False
        ' 
        ' dgvReport
        ' 
        dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvReport.Location = New Point(41, 275)
        dgvReport.Name = "dgvReport"
        dgvReport.RowHeadersWidth = 51
        dgvReport.Size = New Size(1332, 201)
        dgvReport.TabIndex = 23
        ' 
        ' btnClear
        ' 
        btnClear.Location = New Point(332, 487)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(225, 61)
        btnClear.TabIndex = 24
        btnClear.Text = "Clear"
        btnClear.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 17F)
        Label1.Location = New Point(41, 569)
        Label1.Name = "Label1"
        Label1.Size = New Size(117, 40)
        Label1.TabIndex = 25
        Label1.Text = "Records"
        ' 
        ' cboReport
        ' 
        cboReport.Font = New Font("Segoe UI", 13F)
        cboReport.FormattingEnabled = True
        cboReport.Items.AddRange(New Object() {"Available", "Out of Stock", "Pre-Order"})
        cboReport.Location = New Point(313, 569)
        cboReport.Name = "cboReport"
        cboReport.Size = New Size(299, 38)
        cboReport.TabIndex = 26
        ' 
        ' btnRunReport
        ' 
        btnRunReport.Location = New Point(728, 561)
        btnRunReport.Name = "btnRunReport"
        btnRunReport.Size = New Size(177, 48)
        btnRunReport.TabIndex = 27
        btnRunReport.Text = "Load Report"
        btnRunReport.UseVisualStyleBackColor = True
        ' 
        ' dgvData
        ' 
        dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvData.Location = New Point(51, 626)
        dgvData.Name = "dgvData"
        dgvData.RowHeadersWidth = 51
        dgvData.Size = New Size(1309, 243)
        dgvData.TabIndex = 28
        ' 
        ' MainPanel
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1442, 823)
        Controls.Add(dgvData)
        Controls.Add(btnRunReport)
        Controls.Add(cboReport)
        Controls.Add(Label1)
        Controls.Add(btnClear)
        Controls.Add(dgvReport)
        Controls.Add(btnDelete)
        Controls.Add(cbBox1)
        Controls.Add(btnSave)
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
        CType(dgvReport, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvData, ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents btnSave As Button
    Friend WithEvents cbBox1 As ComboBox
    Friend WithEvents btnDelete As Button
    Friend WithEvents dgvReport As DataGridView
    Friend WithEvents btnClear As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cboReport As ComboBox
    Friend WithEvents btnRunReport As Button
    Friend WithEvents dgvData As DataGridView
End Class
