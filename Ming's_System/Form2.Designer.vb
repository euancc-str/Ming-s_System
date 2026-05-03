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
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        TabPage2 = New TabPage()
        cboSeries = New ComboBox()
        lblSeries = New Label()
        txtSearch = New TextBox()
        Panel1 = New Panel()
        dgvData = New DataGridView()
        btnRunReport = New Button()
        cboReport = New ComboBox()
        Label1 = New Label()
        btnClear = New Button()
        dgvReport = New DataGridView()
        btnDelete = New Button()
        cbBox1 = New ComboBox()
        btnSave = New Button()
        lblField7 = New Label()
        txtField7 = New TextBox()
        lblField6 = New Label()
        txtField6 = New TextBox()
        lblField5 = New Label()
        txtField5 = New TextBox()
        lblField4 = New Label()
        lblField3 = New Label()
        txtField3 = New TextBox()
        lblField2 = New Label()
        txtField2 = New TextBox()
        lblField = New Label()
        lblField1 = New Label()
        txtField1 = New TextBox()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(dgvData, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvReport, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Location = New Point(-7, 12)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(1851, 872)
        TabControl1.TabIndex = 0
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(cboSeries)
        TabPage1.Controls.Add(lblSeries)
        TabPage1.Controls.Add(txtSearch)
        TabPage1.Controls.Add(Panel1)
        TabPage1.Controls.Add(dgvData)
        TabPage1.Controls.Add(btnRunReport)
        TabPage1.Controls.Add(cboReport)
        TabPage1.Controls.Add(Label1)
        TabPage1.Controls.Add(btnClear)
        TabPage1.Controls.Add(dgvReport)
        TabPage1.Controls.Add(btnDelete)
        TabPage1.Controls.Add(cbBox1)
        TabPage1.Controls.Add(btnSave)
        TabPage1.Controls.Add(lblField7)
        TabPage1.Controls.Add(txtField7)
        TabPage1.Controls.Add(lblField6)
        TabPage1.Controls.Add(txtField6)
        TabPage1.Controls.Add(lblField5)
        TabPage1.Controls.Add(txtField5)
        TabPage1.Controls.Add(lblField4)
        TabPage1.Controls.Add(lblField3)
        TabPage1.Controls.Add(txtField3)
        TabPage1.Controls.Add(lblField2)
        TabPage1.Controls.Add(txtField2)
        TabPage1.Controls.Add(lblField)
        TabPage1.Controls.Add(lblField1)
        TabPage1.Controls.Add(txtField1)
        TabPage1.Location = New Point(4, 29)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(1843, 839)
        TabPage1.TabIndex = 0
        TabPage1.Text = "TabPage1"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' TabPage2
        ' 
        TabPage2.Location = New Point(4, 29)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(242, 92)
        TabPage2.TabIndex = 1
        TabPage2.Text = "TabPage2"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' cboSeries
        ' 
        cboSeries.Font = New Font("Segoe UI", 13F)
        cboSeries.FormattingEnabled = True
        cboSeries.Items.AddRange(New Object() {"Available", "Out of Stock", "Pre-Order"})
        cboSeries.Location = New Point(1378, 199)
        cboSeries.Name = "cboSeries"
        cboSeries.Size = New Size(299, 38)
        cboSeries.TabIndex = 58
        ' 
        ' lblSeries
        ' 
        lblSeries.AutoSize = True
        lblSeries.Location = New Point(1378, 169)
        lblSeries.Name = "lblSeries"
        lblSeries.Size = New Size(48, 20)
        lblSeries.TabIndex = 57
        lblSeries.Text = "Series"
        ' 
        ' txtSearch
        ' 
        txtSearch.Font = New Font("Segoe UI", 13F)
        txtSearch.Location = New Point(409, 540)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(314, 36)
        txtSearch.TabIndex = 56
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Gainsboro
        Panel1.Location = New Point(176, 490)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1478, 2)
        Panel1.TabIndex = 33
        ' 
        ' dgvData
        ' 
        dgvData.AllowUserToAddRows = False
        dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvData.Location = New Point(176, 593)
        dgvData.Name = "dgvData"
        dgvData.RowHeadersWidth = 51
        dgvData.Size = New Size(1477, 243)
        dgvData.TabIndex = 55
        ' 
        ' btnRunReport
        ' 
        btnRunReport.Location = New Point(1466, 526)
        btnRunReport.Name = "btnRunReport"
        btnRunReport.Size = New Size(177, 48)
        btnRunReport.TabIndex = 54
        btnRunReport.Text = "Load Report"
        btnRunReport.UseVisualStyleBackColor = True
        ' 
        ' cboReport
        ' 
        cboReport.Font = New Font("Segoe UI", 13F)
        cboReport.FormattingEnabled = True
        cboReport.Items.AddRange(New Object() {"Available", "Out of Stock", "Pre-Order"})
        cboReport.Location = New Point(1108, 532)
        cboReport.Name = "cboReport"
        cboReport.Size = New Size(299, 38)
        cboReport.TabIndex = 53
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 17F)
        Label1.Location = New Point(165, 534)
        Label1.Name = "Label1"
        Label1.Size = New Size(209, 40)
        Label1.TabIndex = 52
        Label1.Text = "Search Records"
        ' 
        ' btnClear
        ' 
        btnClear.Location = New Point(1437, 334)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(220, 61)
        btnClear.TabIndex = 51
        btnClear.Text = "Clear"
        btnClear.UseVisualStyleBackColor = True
        ' 
        ' dgvReport
        ' 
        dgvReport.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvReport.Location = New Point(176, 265)
        dgvReport.Name = "dgvReport"
        dgvReport.RowHeadersWidth = 51
        dgvReport.Size = New Size(1168, 201)
        dgvReport.TabIndex = 50
        ' 
        ' btnDelete
        ' 
        btnDelete.BackColor = Color.Red
        btnDelete.Location = New Point(1466, 401)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(168, 77)
        btnDelete.TabIndex = 49
        btnDelete.Text = "Delete Record?"
        btnDelete.UseVisualStyleBackColor = False
        ' 
        ' cbBox1
        ' 
        cbBox1.Font = New Font("Segoe UI", 13F)
        cbBox1.FormattingEnabled = True
        cbBox1.Items.AddRange(New Object() {"Available", "Out of Stock", "Pre-Order"})
        cbBox1.Location = New Point(1378, 86)
        cbBox1.Name = "cbBox1"
        cbBox1.Size = New Size(299, 38)
        cbBox1.TabIndex = 48
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(1437, 265)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(220, 61)
        btnSave.TabIndex = 47
        btnSave.Text = "Save Record"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' lblField7
        ' 
        lblField7.AutoSize = True
        lblField7.Location = New Point(915, 156)
        lblField7.Name = "lblField7"
        lblField7.Size = New Size(88, 20)
        lblField7.TabIndex = 46
        lblField7.Text = "Stock Count"
        ' 
        ' txtField7
        ' 
        txtField7.Font = New Font("Segoe UI", 13F)
        txtField7.Location = New Point(915, 201)
        txtField7.Name = "txtField7"
        txtField7.Size = New Size(314, 36)
        txtField7.TabIndex = 45
        ' 
        ' lblField6
        ' 
        lblField6.AutoSize = True
        lblField6.Location = New Point(520, 156)
        lblField6.Name = "lblField6"
        lblField6.Size = New Size(36, 20)
        lblField6.TabIndex = 44
        lblField6.Text = "Size"
        ' 
        ' txtField6
        ' 
        txtField6.Font = New Font("Segoe UI", 13F)
        txtField6.Location = New Point(520, 201)
        txtField6.Name = "txtField6"
        txtField6.Size = New Size(311, 36)
        txtField6.TabIndex = 43
        ' 
        ' lblField5
        ' 
        lblField5.AutoSize = True
        lblField5.Location = New Point(177, 156)
        lblField5.Name = "lblField5"
        lblField5.Size = New Size(45, 20)
        lblField5.TabIndex = 42
        lblField5.Text = "Color"
        ' 
        ' txtField5
        ' 
        txtField5.Font = New Font("Segoe UI", 13F)
        txtField5.Location = New Point(177, 201)
        txtField5.Name = "txtField5"
        txtField5.Size = New Size(269, 36)
        txtField5.TabIndex = 41
        ' 
        ' lblField4
        ' 
        lblField4.AutoSize = True
        lblField4.Location = New Point(1378, 56)
        lblField4.Name = "lblField4"
        lblField4.Size = New Size(49, 20)
        lblField4.TabIndex = 40
        lblField4.Text = "Status"
        ' 
        ' lblField3
        ' 
        lblField3.AutoSize = True
        lblField3.Location = New Point(915, 58)
        lblField3.Name = "lblField3"
        lblField3.Size = New Size(94, 20)
        lblField3.TabIndex = 39
        lblField3.Text = "Sellling Price"
        ' 
        ' txtField3
        ' 
        txtField3.Font = New Font("Segoe UI", 13F)
        txtField3.Location = New Point(915, 88)
        txtField3.Name = "txtField3"
        txtField3.Size = New Size(314, 36)
        txtField3.TabIndex = 38
        ' 
        ' lblField2
        ' 
        lblField2.AutoSize = True
        lblField2.Location = New Point(520, 58)
        lblField2.Name = "lblField2"
        lblField2.Size = New Size(91, 20)
        lblField2.TabIndex = 37
        lblField2.Text = "Buying price"
        ' 
        ' txtField2
        ' 
        txtField2.Font = New Font("Segoe UI", 13F)
        txtField2.Location = New Point(520, 88)
        txtField2.Name = "txtField2"
        txtField2.Size = New Size(311, 36)
        txtField2.TabIndex = 36
        ' 
        ' lblField
        ' 
        lblField.AutoSize = True
        lblField.Font = New Font("Segoe UI", 15F)
        lblField.Location = New Point(166, 2)
        lblField.Name = "lblField"
        lblField.Size = New Size(175, 35)
        lblField.TabIndex = 35
        lblField.Text = "Record Details"
        ' 
        ' lblField1
        ' 
        lblField1.AutoSize = True
        lblField1.Location = New Point(177, 58)
        lblField1.Name = "lblField1"
        lblField1.Size = New Size(83, 20)
        lblField1.TabIndex = 34
        lblField1.Text = "Item Name"
        ' 
        ' txtField1
        ' 
        txtField1.Font = New Font("Segoe UI", 13F)
        txtField1.Location = New Point(177, 88)
        txtField1.Name = "txtField1"
        txtField1.Size = New Size(269, 36)
        txtField1.TabIndex = 32
        ' 
        ' MainPanel
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1820, 857)
        Controls.Add(TabControl1)
        Name = "MainPanel"
        Text = "Mings"
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        CType(dgvData, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvReport, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents cboSeries As ComboBox
    Friend WithEvents lblSeries As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents dgvData As DataGridView
    Friend WithEvents btnRunReport As Button
    Friend WithEvents cboReport As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnClear As Button
    Friend WithEvents dgvReport As DataGridView
    Friend WithEvents btnDelete As Button
    Friend WithEvents cbBox1 As ComboBox
    Friend WithEvents btnSave As Button
    Friend WithEvents lblField7 As Label
    Friend WithEvents txtField7 As TextBox
    Friend WithEvents lblField6 As Label
    Friend WithEvents txtField6 As TextBox
    Friend WithEvents lblField5 As Label
    Friend WithEvents txtField5 As TextBox
    Friend WithEvents lblField4 As Label
    Friend WithEvents lblField3 As Label
    Friend WithEvents txtField3 As TextBox
    Friend WithEvents lblField2 As Label
    Friend WithEvents txtField2 As TextBox
    Friend WithEvents lblField As Label
    Friend WithEvents lblField1 As Label
    Friend WithEvents txtField1 As TextBox
End Class
