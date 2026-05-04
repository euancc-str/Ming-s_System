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
        TabPage2 = New TabPage()
        Label3 = New Label()
        dgvBranchInventory = New DataGridView()
        btnRunSweep = New Button()
        cboBranchSweep = New ComboBox()
        TabPage3 = New TabPage()
        Label6 = New Label()
        dgvPurchases = New DataGridView()
        Label5 = New Label()
        dgvDelivers = New DataGridView()
        Label4 = New Label()
        dgvCust = New DataGridView()
        Label2 = New Label()
        txtGlobalSearch = New TextBox()
        TabPage4 = New TabPage()
        dgvProvidedProducts = New DataGridView()
        txtSearchSupplier = New TextBox()
        dgvSuppliers = New DataGridView()
        Label8 = New Label()
        TabPage5 = New TabPage()
        dgvStaffAssignments = New DataGridView()
        dgvEmployees = New DataGridView()
        txtAdminSearch = New TextBox()
        dgvBranches = New DataGridView()
        Label7 = New Label()
        TabPage6 = New TabPage()
        btnFullRecover = New Button()
        btnFullBackup = New Button()
        btnExportCSV = New Button()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(dgvData, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvReport, ComponentModel.ISupportInitialize).BeginInit()
        TabPage2.SuspendLayout()
        CType(dgvBranchInventory, ComponentModel.ISupportInitialize).BeginInit()
        TabPage3.SuspendLayout()
        CType(dgvPurchases, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvDelivers, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvCust, ComponentModel.ISupportInitialize).BeginInit()
        TabPage4.SuspendLayout()
        CType(dgvProvidedProducts, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvSuppliers, ComponentModel.ISupportInitialize).BeginInit()
        TabPage5.SuspendLayout()
        CType(dgvStaffAssignments, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvEmployees, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvBranches, ComponentModel.ISupportInitialize).BeginInit()
        TabPage6.SuspendLayout()
        SuspendLayout()
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Controls.Add(TabPage4)
        TabControl1.Controls.Add(TabPage5)
        TabControl1.Controls.Add(TabPage6)
        TabControl1.Location = New Point(-7, 12)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.ShowToolTips = True
        TabControl1.Size = New Size(1851, 872)
        TabControl1.TabIndex = 0
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(btnExportCSV)
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
        TabPage1.Text = "Main"
        TabPage1.UseVisualStyleBackColor = True
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
        dgvData.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
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
        dgvReport.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
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
        ' TabPage2
        ' 
        TabPage2.Controls.Add(Label3)
        TabPage2.Controls.Add(dgvBranchInventory)
        TabPage2.Controls.Add(btnRunSweep)
        TabPage2.Controls.Add(cboBranchSweep)
        TabPage2.Location = New Point(4, 29)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(1843, 839)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Batch Transfer"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 15F)
        Label3.Location = New Point(279, 97)
        Label3.Name = "Label3"
        Label3.Size = New Size(188, 35)
        Label3.TabIndex = 4
        Label3.Text = "Select a Branch:"
        ' 
        ' dgvBranchInventory
        ' 
        dgvBranchInventory.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvBranchInventory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvBranchInventory.Location = New Point(259, 186)
        dgvBranchInventory.Name = "dgvBranchInventory"
        dgvBranchInventory.RowHeadersWidth = 51
        dgvBranchInventory.Size = New Size(1004, 466)
        dgvBranchInventory.TabIndex = 2
        ' 
        ' btnRunSweep
        ' 
        btnRunSweep.Location = New Point(818, 93)
        btnRunSweep.Name = "btnRunSweep"
        btnRunSweep.Size = New Size(158, 52)
        btnRunSweep.TabIndex = 1
        btnRunSweep.Text = "Transfer to main?"
        btnRunSweep.UseVisualStyleBackColor = True
        ' 
        ' cboBranchSweep
        ' 
        cboBranchSweep.FormattingEnabled = True
        cboBranchSweep.Location = New Point(482, 104)
        cboBranchSweep.Name = "cboBranchSweep"
        cboBranchSweep.Size = New Size(307, 28)
        cboBranchSweep.TabIndex = 0
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(Label6)
        TabPage3.Controls.Add(dgvPurchases)
        TabPage3.Controls.Add(Label5)
        TabPage3.Controls.Add(dgvDelivers)
        TabPage3.Controls.Add(Label4)
        TabPage3.Controls.Add(dgvCust)
        TabPage3.Controls.Add(Label2)
        TabPage3.Controls.Add(txtGlobalSearch)
        TabPage3.Location = New Point(4, 29)
        TabPage3.Name = "TabPage3"
        TabPage3.Padding = New Padding(3)
        TabPage3.Size = New Size(1843, 839)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Customer Data"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Segoe UI", 15F)
        Label6.Location = New Point(1383, 191)
        Label6.Name = "Label6"
        Label6.RightToLeft = RightToLeft.Yes
        Label6.Size = New Size(280, 35)
        Label6.TabIndex = 7
        Label6.Text = "Complete Order History"
        ' 
        ' dgvPurchases
        ' 
        dgvPurchases.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPurchases.Location = New Point(1215, 247)
        dgvPurchases.Name = "dgvPurchases"
        dgvPurchases.RowHeadersWidth = 51
        dgvPurchases.Size = New Size(606, 557)
        dgvPurchases.TabIndex = 6
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI", 15F)
        Label5.Location = New Point(822, 191)
        Label5.Name = "Label5"
        Label5.RightToLeft = RightToLeft.Yes
        Label5.Size = New Size(177, 35)
        Label5.TabIndex = 5
        Label5.Text = "Delivery Status"
        ' 
        ' dgvDelivers
        ' 
        dgvDelivers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDelivers.Location = New Point(608, 247)
        dgvDelivers.Name = "dgvDelivers"
        dgvDelivers.RowHeadersWidth = 51
        dgvDelivers.Size = New Size(606, 557)
        dgvDelivers.TabIndex = 4
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 15F)
        Label4.Location = New Point(152, 191)
        Label4.Name = "Label4"
        Label4.RightToLeft = RightToLeft.Yes
        Label4.Size = New Size(200, 35)
        Label4.TabIndex = 3
        Label4.Text = "Customer Profile"
        ' 
        ' dgvCust
        ' 
        dgvCust.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCust.Location = New Point(0, 247)
        dgvCust.Name = "dgvCust"
        dgvCust.RowHeadersWidth = 51
        dgvCust.Size = New Size(606, 557)
        dgvCust.TabIndex = 2
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 15F)
        Label2.Location = New Point(291, 105)
        Label2.Name = "Label2"
        Label2.RightToLeft = RightToLeft.Yes
        Label2.Size = New Size(223, 35)
        Label2.TabIndex = 1
        Label2.Text = "Search a Customer"
        ' 
        ' txtGlobalSearch
        ' 
        txtGlobalSearch.Font = New Font("Segoe UI", 15F)
        txtGlobalSearch.Location = New Point(545, 99)
        txtGlobalSearch.Name = "txtGlobalSearch"
        txtGlobalSearch.Size = New Size(347, 41)
        txtGlobalSearch.TabIndex = 0
        ' 
        ' TabPage4
        ' 
        TabPage4.Controls.Add(dgvProvidedProducts)
        TabPage4.Controls.Add(txtSearchSupplier)
        TabPage4.Controls.Add(dgvSuppliers)
        TabPage4.Controls.Add(Label8)
        TabPage4.Location = New Point(4, 29)
        TabPage4.Name = "TabPage4"
        TabPage4.Padding = New Padding(3)
        TabPage4.Size = New Size(1843, 839)
        TabPage4.TabIndex = 3
        TabPage4.Text = "Suppliers & Partners"
        TabPage4.UseVisualStyleBackColor = True
        ' 
        ' dgvProvidedProducts
        ' 
        dgvProvidedProducts.AllowUserToAddRows = False
        dgvProvidedProducts.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvProvidedProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvProvidedProducts.Location = New Point(954, 288)
        dgvProvidedProducts.Name = "dgvProvidedProducts"
        dgvProvidedProducts.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvProvidedProducts.RowHeadersWidth = 51
        dgvProvidedProducts.ShowCellErrors = False
        dgvProvidedProducts.Size = New Size(835, 496)
        dgvProvidedProducts.TabIndex = 84
        ' 
        ' txtSearchSupplier
        ' 
        txtSearchSupplier.Font = New Font("Segoe UI", 13F)
        txtSearchSupplier.Location = New Point(272, 235)
        txtSearchSupplier.Name = "txtSearchSupplier"
        txtSearchSupplier.Size = New Size(447, 36)
        txtSearchSupplier.TabIndex = 83
        ' 
        ' dgvSuppliers
        ' 
        dgvSuppliers.AllowUserToAddRows = False
        dgvSuppliers.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvSuppliers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSuppliers.Location = New Point(39, 288)
        dgvSuppliers.Name = "dgvSuppliers"
        dgvSuppliers.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvSuppliers.RowHeadersWidth = 51
        dgvSuppliers.ShowCellErrors = False
        dgvSuppliers.Size = New Size(835, 496)
        dgvSuppliers.TabIndex = 82
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI", 17F)
        Label8.Location = New Point(28, 229)
        Label8.Name = "Label8"
        Label8.Size = New Size(209, 40)
        Label8.TabIndex = 79
        Label8.Text = "Search Records"
        ' 
        ' TabPage5
        ' 
        TabPage5.Controls.Add(dgvStaffAssignments)
        TabPage5.Controls.Add(dgvEmployees)
        TabPage5.Controls.Add(txtAdminSearch)
        TabPage5.Controls.Add(dgvBranches)
        TabPage5.Controls.Add(Label7)
        TabPage5.Location = New Point(4, 29)
        TabPage5.Name = "TabPage5"
        TabPage5.Padding = New Padding(3)
        TabPage5.Size = New Size(1843, 839)
        TabPage5.TabIndex = 4
        TabPage5.Text = "Branch & Emplloyees"
        TabPage5.UseVisualStyleBackColor = True
        ' 
        ' dgvStaffAssignments
        ' 
        dgvStaffAssignments.AllowUserToAddRows = False
        dgvStaffAssignments.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvStaffAssignments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvStaffAssignments.Location = New Point(1229, 244)
        dgvStaffAssignments.Name = "dgvStaffAssignments"
        dgvStaffAssignments.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvStaffAssignments.RowHeadersWidth = 51
        dgvStaffAssignments.ShowCellErrors = False
        dgvStaffAssignments.Size = New Size(582, 531)
        dgvStaffAssignments.TabIndex = 89
        ' 
        ' dgvEmployees
        ' 
        dgvEmployees.AllowUserToAddRows = False
        dgvEmployees.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvEmployees.Location = New Point(634, 244)
        dgvEmployees.Name = "dgvEmployees"
        dgvEmployees.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvEmployees.RowHeadersWidth = 51
        dgvEmployees.ShowCellErrors = False
        dgvEmployees.Size = New Size(565, 531)
        dgvEmployees.TabIndex = 88
        ' 
        ' txtAdminSearch
        ' 
        txtAdminSearch.Font = New Font("Segoe UI", 13F)
        txtAdminSearch.Location = New Point(285, 148)
        txtAdminSearch.Name = "txtAdminSearch"
        txtAdminSearch.Size = New Size(447, 36)
        txtAdminSearch.TabIndex = 87
        ' 
        ' dgvBranches
        ' 
        dgvBranches.AllowUserToAddRows = False
        dgvBranches.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvBranches.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvBranches.Location = New Point(11, 244)
        dgvBranches.Name = "dgvBranches"
        dgvBranches.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvBranches.RowHeadersWidth = 51
        dgvBranches.ShowCellErrors = False
        dgvBranches.Size = New Size(592, 531)
        dgvBranches.TabIndex = 86
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Segoe UI", 17F)
        Label7.Location = New Point(41, 142)
        Label7.Name = "Label7"
        Label7.Size = New Size(209, 40)
        Label7.TabIndex = 85
        Label7.Text = "Search Records"
        ' 
        ' TabPage6
        ' 
        TabPage6.Controls.Add(btnFullRecover)
        TabPage6.Controls.Add(btnFullBackup)
        TabPage6.Location = New Point(4, 29)
        TabPage6.Name = "TabPage6"
        TabPage6.Padding = New Padding(3)
        TabPage6.Size = New Size(1843, 839)
        TabPage6.TabIndex = 5
        TabPage6.Text = "Full Database Backup"
        TabPage6.UseVisualStyleBackColor = True
        ' 
        ' btnFullRecover
        ' 
        btnFullRecover.BackColor = Color.Red
        btnFullRecover.Location = New Point(634, 185)
        btnFullRecover.Name = "btnFullRecover"
        btnFullRecover.Size = New Size(171, 93)
        btnFullRecover.TabIndex = 1
        btnFullRecover.Text = "Execute Full System Recovery"
        btnFullRecover.UseVisualStyleBackColor = False
        ' 
        ' btnFullBackup
        ' 
        btnFullBackup.Location = New Point(633, 401)
        btnFullBackup.Name = "btnFullBackup"
        btnFullBackup.Size = New Size(172, 96)
        btnFullBackup.TabIndex = 0
        btnFullBackup.Text = "Generate Full System Backup"
        btnFullBackup.UseVisualStyleBackColor = True
        ' 
        ' btnExportCSV
        ' 
        btnExportCSV.Location = New Point(813, 523)
        btnExportCSV.Name = "btnExportCSV"
        btnExportCSV.Size = New Size(149, 61)
        btnExportCSV.TabIndex = 59
        btnExportCSV.Text = "Export to csv"
        btnExportCSV.UseVisualStyleBackColor = True
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
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        CType(dgvBranchInventory, ComponentModel.ISupportInitialize).EndInit()
        TabPage3.ResumeLayout(False)
        TabPage3.PerformLayout()
        CType(dgvPurchases, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvDelivers, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvCust, ComponentModel.ISupportInitialize).EndInit()
        TabPage4.ResumeLayout(False)
        TabPage4.PerformLayout()
        CType(dgvProvidedProducts, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvSuppliers, ComponentModel.ISupportInitialize).EndInit()
        TabPage5.ResumeLayout(False)
        TabPage5.PerformLayout()
        CType(dgvStaffAssignments, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvEmployees, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvBranches, ComponentModel.ISupportInitialize).EndInit()
        TabPage6.ResumeLayout(False)
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
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents btnRunSweep As Button
    Friend WithEvents cboBranchSweep As ComboBox
    Friend WithEvents dgvBranchInventory As DataGridView
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents dgvCust As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents txtGlobalSearch As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents dgvPurchases As DataGridView
    Friend WithEvents Label5 As Label
    Friend WithEvents dgvDelivers As DataGridView
    Friend WithEvents txtSearchSupplier As TextBox
    Friend WithEvents dgvSuppliers As DataGridView
    Friend WithEvents Label8 As Label
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents dgvProvidedProducts As DataGridView
    Friend WithEvents dgvEmployees As DataGridView
    Friend WithEvents txtAdminSearch As TextBox
    Friend WithEvents dgvBranches As DataGridView
    Friend WithEvents Label7 As Label
    Friend WithEvents dgvStaffAssignments As DataGridView
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents btnFullRecover As Button
    Friend WithEvents btnFullBackup As Button
    Friend WithEvents btnExportCSV As Button
End Class
