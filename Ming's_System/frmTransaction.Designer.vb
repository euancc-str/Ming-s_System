<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransaction
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
        lblTransactionHeader = New Label()
        lblNewSupplierHeader = New Label()
        cboTarget = New ComboBox()
        cboProduct = New ComboBox()
        lblNewProductHeader = New Label()
        lblDownPayment = New Label()
        txtDownPayment = New TextBox()
        lblReservationDate = New Label()
        dtpReservationDate = New DateTimePicker()
        btnProcess = New Button()
        btnCancel = New Button()
        chkNewTarget = New CheckBox()
        chkNewProduct = New CheckBox()
        Label1 = New Label()
        lblNewColor = New Label()
        txtNewColor = New TextBox()
        lblNewItemName = New Label()
        txtNewItemName = New TextBox()
        lblNewSize = New Label()
        txtNewSize = New TextBox()
        lblNewBuyPrice = New Label()
        txtNewBuyPrice = New TextBox()
        lblNewStockCount = New Label()
        txtNewStockCount = New TextBox()
        lblNewSellPrice = New Label()
        txtNewSellPrice = New TextBox()
        cboNewStatus = New ComboBox()
        lblNewStatus = New Label()
        lblNewContactPerson = New Label()
        txtNewContactPerson = New TextBox()
        lblNewCountryOrigin = New Label()
        txtNewCountryOrigin = New TextBox()
        lblNewCompanyName = New Label()
        txtNewCompanyName = New TextBox()
        txtShippingFee = New TextBox()
        lblShippingFee = New Label()
        cboStatus = New ComboBox()
        lblStatus = New Label()
        lblShippingDate = New Label()
        dtpShippingDate = New DateTimePicker()
        txtQuantity = New TextBox()
        lblQuantity = New Label()
        cboCourier = New ComboBox()
        lblCourier = New Label()
        cboDeliveryType = New ComboBox()
        lblDelivery = New Label()
        cboSalesLocation = New ComboBox()
        lblSalesLocation = New Label()
        dtpStartTime = New DateTimePicker()
        lblStartTime = New Label()
        SuspendLayout()
        ' 
        ' lblTransactionHeader
        ' 
        lblTransactionHeader.AutoSize = True
        lblTransactionHeader.Font = New Font("Segoe UI", 20F)
        lblTransactionHeader.Location = New Point(204, 46)
        lblTransactionHeader.Name = "lblTransactionHeader"
        lblTransactionHeader.Size = New Size(189, 46)
        lblTransactionHeader.TabIndex = 0
        lblTransactionHeader.Text = "Transaction"
        ' 
        ' lblNewSupplierHeader
        ' 
        lblNewSupplierHeader.AutoSize = True
        lblNewSupplierHeader.Font = New Font("Segoe UI", 10F)
        lblNewSupplierHeader.Location = New Point(204, 117)
        lblNewSupplierHeader.Name = "lblNewSupplierHeader"
        lblNewSupplierHeader.Size = New Size(72, 23)
        lblNewSupplierHeader.TabIndex = 1
        lblNewSupplierHeader.Text = "Supplier"
        ' 
        ' cboTarget
        ' 
        cboTarget.Font = New Font("Segoe UI", 15F)
        cboTarget.FormattingEnabled = True
        cboTarget.Location = New Point(204, 143)
        cboTarget.Name = "cboTarget"
        cboTarget.Size = New Size(526, 43)
        cboTarget.TabIndex = 2
        ' 
        ' cboProduct
        ' 
        cboProduct.Font = New Font("Segoe UI", 15F)
        cboProduct.FormattingEnabled = True
        cboProduct.Location = New Point(825, 143)
        cboProduct.Name = "cboProduct"
        cboProduct.Size = New Size(847, 43)
        cboProduct.TabIndex = 4
        ' 
        ' lblNewProductHeader
        ' 
        lblNewProductHeader.AutoSize = True
        lblNewProductHeader.Font = New Font("Segoe UI", 10F)
        lblNewProductHeader.Location = New Point(825, 117)
        lblNewProductHeader.Name = "lblNewProductHeader"
        lblNewProductHeader.Size = New Size(120, 23)
        lblNewProductHeader.TabIndex = 3
        lblNewProductHeader.Text = "Select Product"
        ' 
        ' lblDownPayment
        ' 
        lblDownPayment.AutoSize = True
        lblDownPayment.Font = New Font("Segoe UI", 10F)
        lblDownPayment.Location = New Point(204, 544)
        lblDownPayment.Name = "lblDownPayment"
        lblDownPayment.Size = New Size(125, 23)
        lblDownPayment.TabIndex = 5
        lblDownPayment.Text = "Down Payment"
        ' 
        ' txtDownPayment
        ' 
        txtDownPayment.Font = New Font("Segoe UI", 15F)
        txtDownPayment.Location = New Point(204, 570)
        txtDownPayment.Name = "txtDownPayment"
        txtDownPayment.Size = New Size(249, 41)
        txtDownPayment.TabIndex = 6
        ' 
        ' lblReservationDate
        ' 
        lblReservationDate.AutoSize = True
        lblReservationDate.Font = New Font("Segoe UI", 10F)
        lblReservationDate.Location = New Point(204, 627)
        lblReservationDate.Name = "lblReservationDate"
        lblReservationDate.Size = New Size(139, 23)
        lblReservationDate.TabIndex = 7
        lblReservationDate.Text = "Reservation Date"
        ' 
        ' dtpReservationDate
        ' 
        dtpReservationDate.CalendarFont = New Font("Segoe UI", 25F)
        dtpReservationDate.Location = New Point(204, 662)
        dtpReservationDate.Name = "dtpReservationDate"
        dtpReservationDate.Size = New Size(249, 27)
        dtpReservationDate.TabIndex = 8
        ' 
        ' btnProcess
        ' 
        btnProcess.Location = New Point(1412, 823)
        btnProcess.Name = "btnProcess"
        btnProcess.Size = New Size(223, 59)
        btnProcess.TabIndex = 9
        btnProcess.Text = "Save"
        btnProcess.UseVisualStyleBackColor = True
        ' 
        ' btnCancel
        ' 
        btnCancel.Location = New Point(1210, 823)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(158, 59)
        btnCancel.TabIndex = 10
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = True
        ' 
        ' chkNewTarget
        ' 
        chkNewTarget.AllowDrop = True
        chkNewTarget.AutoSize = True
        chkNewTarget.Font = New Font("Segoe UI", 13F)
        chkNewTarget.Location = New Point(204, 208)
        chkNewTarget.Name = "chkNewTarget"
        chkNewTarget.Size = New Size(204, 34)
        chkNewTarget.TabIndex = 11
        chkNewTarget.Text = "Add new supplier"
        chkNewTarget.UseVisualStyleBackColor = True
        ' 
        ' chkNewProduct
        ' 
        chkNewProduct.AutoSize = True
        chkNewProduct.Font = New Font("Segoe UI", 13F)
        chkNewProduct.Location = New Point(827, 208)
        chkNewProduct.Name = "chkNewProduct"
        chkNewProduct.Size = New Size(200, 34)
        chkNewProduct.TabIndex = 12
        chkNewProduct.Text = "add new product"
        chkNewProduct.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 10F)
        Label1.ForeColor = SystemColors.ActiveBorder
        Label1.Location = New Point(198, 499)
        Label1.Name = "Label1"
        Label1.Size = New Size(153, 23)
        Label1.TabIndex = 13
        Label1.Text = "Transaction Details"
        ' 
        ' lblNewColor
        ' 
        lblNewColor.AutoSize = True
        lblNewColor.Location = New Point(1263, 549)
        lblNewColor.Name = "lblNewColor"
        lblNewColor.Size = New Size(45, 20)
        lblNewColor.TabIndex = 17
        lblNewColor.Text = "Color"
        ' 
        ' txtNewColor
        ' 
        txtNewColor.Font = New Font("Segoe UI", 15F)
        txtNewColor.Location = New Point(1263, 572)
        txtNewColor.Name = "txtNewColor"
        txtNewColor.Size = New Size(411, 41)
        txtNewColor.TabIndex = 16
        ' 
        ' lblNewItemName
        ' 
        lblNewItemName.AutoSize = True
        lblNewItemName.Location = New Point(827, 275)
        lblNewItemName.Name = "lblNewItemName"
        lblNewItemName.Size = New Size(83, 20)
        lblNewItemName.TabIndex = 15
        lblNewItemName.Text = "Item Name"
        ' 
        ' txtNewItemName
        ' 
        txtNewItemName.Font = New Font("Segoe UI", 15F)
        txtNewItemName.Location = New Point(827, 305)
        txtNewItemName.Name = "txtNewItemName"
        txtNewItemName.Size = New Size(845, 41)
        txtNewItemName.TabIndex = 14
        ' 
        ' lblNewSize
        ' 
        lblNewSize.AutoSize = True
        lblNewSize.Location = New Point(1263, 470)
        lblNewSize.Name = "lblNewSize"
        lblNewSize.Size = New Size(36, 20)
        lblNewSize.TabIndex = 21
        lblNewSize.Text = "Size"
        ' 
        ' txtNewSize
        ' 
        txtNewSize.Font = New Font("Segoe UI", 15F)
        txtNewSize.Location = New Point(1263, 493)
        txtNewSize.Name = "txtNewSize"
        txtNewSize.Size = New Size(409, 41)
        txtNewSize.TabIndex = 20
        ' 
        ' lblNewBuyPrice
        ' 
        lblNewBuyPrice.AutoSize = True
        lblNewBuyPrice.Location = New Point(827, 395)
        lblNewBuyPrice.Name = "lblNewBuyPrice"
        lblNewBuyPrice.Size = New Size(91, 20)
        lblNewBuyPrice.TabIndex = 19
        lblNewBuyPrice.Text = "Buying price"
        ' 
        ' txtNewBuyPrice
        ' 
        txtNewBuyPrice.Font = New Font("Segoe UI", 15F)
        txtNewBuyPrice.Location = New Point(827, 418)
        txtNewBuyPrice.Name = "txtNewBuyPrice"
        txtNewBuyPrice.Size = New Size(411, 41)
        txtNewBuyPrice.TabIndex = 18
        ' 
        ' lblNewStockCount
        ' 
        lblNewStockCount.AutoSize = True
        lblNewStockCount.Location = New Point(827, 470)
        lblNewStockCount.Name = "lblNewStockCount"
        lblNewStockCount.Size = New Size(88, 20)
        lblNewStockCount.TabIndex = 25
        lblNewStockCount.Text = "Stock Count"
        ' 
        ' txtNewStockCount
        ' 
        txtNewStockCount.Font = New Font("Segoe UI", 15F)
        txtNewStockCount.Location = New Point(827, 493)
        txtNewStockCount.Name = "txtNewStockCount"
        txtNewStockCount.Size = New Size(411, 41)
        txtNewStockCount.TabIndex = 24
        ' 
        ' lblNewSellPrice
        ' 
        lblNewSellPrice.AutoSize = True
        lblNewSellPrice.Location = New Point(1263, 395)
        lblNewSellPrice.Name = "lblNewSellPrice"
        lblNewSellPrice.Size = New Size(94, 20)
        lblNewSellPrice.TabIndex = 23
        lblNewSellPrice.Text = "Sellling Price"
        ' 
        ' txtNewSellPrice
        ' 
        txtNewSellPrice.Font = New Font("Segoe UI", 15F)
        txtNewSellPrice.Location = New Point(1263, 418)
        txtNewSellPrice.Name = "txtNewSellPrice"
        txtNewSellPrice.Size = New Size(411, 41)
        txtNewSellPrice.TabIndex = 22
        ' 
        ' cboNewStatus
        ' 
        cboNewStatus.Font = New Font("Segoe UI", 15F)
        cboNewStatus.FormattingEnabled = True
        cboNewStatus.Items.AddRange(New Object() {"Available", "Out of Stock", "Pre-Order"})
        cboNewStatus.Location = New Point(825, 570)
        cboNewStatus.Name = "cboNewStatus"
        cboNewStatus.Size = New Size(409, 43)
        cboNewStatus.TabIndex = 27
        ' 
        ' lblNewStatus
        ' 
        lblNewStatus.AutoSize = True
        lblNewStatus.Location = New Point(825, 547)
        lblNewStatus.Name = "lblNewStatus"
        lblNewStatus.Size = New Size(49, 20)
        lblNewStatus.TabIndex = 26
        lblNewStatus.Text = "Status"
        ' 
        ' lblNewContactPerson
        ' 
        lblNewContactPerson.AutoSize = True
        lblNewContactPerson.Location = New Point(204, 395)
        lblNewContactPerson.Name = "lblNewContactPerson"
        lblNewContactPerson.Size = New Size(107, 20)
        lblNewContactPerson.TabIndex = 33
        lblNewContactPerson.Text = "Contact Person"
        ' 
        ' txtNewContactPerson
        ' 
        txtNewContactPerson.Font = New Font("Segoe UI", 15F)
        txtNewContactPerson.Location = New Point(204, 418)
        txtNewContactPerson.Name = "txtNewContactPerson"
        txtNewContactPerson.Size = New Size(249, 41)
        txtNewContactPerson.TabIndex = 32
        ' 
        ' lblNewCountryOrigin
        ' 
        lblNewCountryOrigin.AutoSize = True
        lblNewCountryOrigin.Location = New Point(473, 395)
        lblNewCountryOrigin.Name = "lblNewCountryOrigin"
        lblNewCountryOrigin.Size = New Size(105, 20)
        lblNewCountryOrigin.TabIndex = 31
        lblNewCountryOrigin.Text = "Country Origin"
        ' 
        ' txtNewCountryOrigin
        ' 
        txtNewCountryOrigin.Font = New Font("Segoe UI", 15F)
        txtNewCountryOrigin.Location = New Point(473, 418)
        txtNewCountryOrigin.Name = "txtNewCountryOrigin"
        txtNewCountryOrigin.Size = New Size(257, 41)
        txtNewCountryOrigin.TabIndex = 30
        ' 
        ' lblNewCompanyName
        ' 
        lblNewCompanyName.AutoSize = True
        lblNewCompanyName.Location = New Point(204, 282)
        lblNewCompanyName.Name = "lblNewCompanyName"
        lblNewCompanyName.Size = New Size(116, 20)
        lblNewCompanyName.TabIndex = 29
        lblNewCompanyName.Text = "Company Name"
        ' 
        ' txtNewCompanyName
        ' 
        txtNewCompanyName.Font = New Font("Segoe UI", 15F)
        txtNewCompanyName.Location = New Point(204, 305)
        txtNewCompanyName.Name = "txtNewCompanyName"
        txtNewCompanyName.Size = New Size(526, 41)
        txtNewCompanyName.TabIndex = 28
        ' 
        ' txtShippingFee
        ' 
        txtShippingFee.Font = New Font("Segoe UI", 15F)
        txtShippingFee.Location = New Point(210, 739)
        txtShippingFee.Name = "txtShippingFee"
        txtShippingFee.Size = New Size(243, 41)
        txtShippingFee.TabIndex = 35
        ' 
        ' lblShippingFee
        ' 
        lblShippingFee.AutoSize = True
        lblShippingFee.Font = New Font("Segoe UI", 10F)
        lblShippingFee.Location = New Point(204, 713)
        lblShippingFee.Name = "lblShippingFee"
        lblShippingFee.Size = New Size(108, 23)
        lblShippingFee.TabIndex = 34
        lblShippingFee.Text = "Shipping Fee"
        ' 
        ' cboStatus
        ' 
        cboStatus.Font = New Font("Segoe UI", 15F)
        cboStatus.FormattingEnabled = True
        cboStatus.Items.AddRange(New Object() {"Available", "Out of Stock", "Pre-Order"})
        cboStatus.Location = New Point(473, 570)
        cboStatus.Name = "cboStatus"
        cboStatus.Size = New Size(257, 43)
        cboStatus.TabIndex = 37
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(473, 546)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(49, 20)
        lblStatus.TabIndex = 36
        lblStatus.Text = "Status"
        ' 
        ' lblShippingDate
        ' 
        lblShippingDate.AutoSize = True
        lblShippingDate.Location = New Point(473, 638)
        lblShippingDate.Name = "lblShippingDate"
        lblShippingDate.Size = New Size(104, 20)
        lblShippingDate.TabIndex = 38
        lblShippingDate.Text = "Shipping Date"
        ' 
        ' dtpShippingDate
        ' 
        dtpShippingDate.CalendarFont = New Font("Segoe UI", 25F)
        dtpShippingDate.Location = New Point(473, 662)
        dtpShippingDate.Name = "dtpShippingDate"
        dtpShippingDate.Size = New Size(257, 27)
        dtpShippingDate.TabIndex = 40
        ' 
        ' txtQuantity
        ' 
        txtQuantity.Font = New Font("Segoe UI", 15F)
        txtQuantity.Location = New Point(473, 739)
        txtQuantity.Name = "txtQuantity"
        txtQuantity.Size = New Size(257, 41)
        txtQuantity.TabIndex = 42
        ' 
        ' lblQuantity
        ' 
        lblQuantity.AutoSize = True
        lblQuantity.Font = New Font("Segoe UI", 10F)
        lblQuantity.Location = New Point(467, 713)
        lblQuantity.Name = "lblQuantity"
        lblQuantity.Size = New Size(76, 23)
        lblQuantity.TabIndex = 41
        lblQuantity.Text = "Quantity"
        ' 
        ' cboCourier
        ' 
        cboCourier.Font = New Font("Segoe UI", 15F)
        cboCourier.FormattingEnabled = True
        cboCourier.Items.AddRange(New Object() {"Available", "Out of Stock", "Pre-Order"})
        cboCourier.Location = New Point(210, 823)
        cboCourier.Name = "cboCourier"
        cboCourier.Size = New Size(243, 43)
        cboCourier.TabIndex = 44
        ' 
        ' lblCourier
        ' 
        lblCourier.AutoSize = True
        lblCourier.Location = New Point(210, 800)
        lblCourier.Name = "lblCourier"
        lblCourier.Size = New Size(57, 20)
        lblCourier.TabIndex = 43
        lblCourier.Text = "Courier"
        ' 
        ' cboDeliveryType
        ' 
        cboDeliveryType.Font = New Font("Segoe UI", 15F)
        cboDeliveryType.FormattingEnabled = True
        cboDeliveryType.Items.AddRange(New Object() {"Available", "Out of Stock", "Pre-Order"})
        cboDeliveryType.Location = New Point(473, 823)
        cboDeliveryType.Name = "cboDeliveryType"
        cboDeliveryType.Size = New Size(257, 43)
        cboDeliveryType.TabIndex = 46
        ' 
        ' lblDelivery
        ' 
        lblDelivery.AutoSize = True
        lblDelivery.Location = New Point(473, 800)
        lblDelivery.Name = "lblDelivery"
        lblDelivery.Size = New Size(98, 20)
        lblDelivery.TabIndex = 45
        lblDelivery.Text = "Delivery Type"
        ' 
        ' cboSalesLocation
        ' 
        cboSalesLocation.Font = New Font("Segoe UI", 15F)
        cboSalesLocation.FormattingEnabled = True
        cboSalesLocation.Items.AddRange(New Object() {"Available", "Out of Stock", "Pre-Order"})
        cboSalesLocation.Location = New Point(210, 906)
        cboSalesLocation.Name = "cboSalesLocation"
        cboSalesLocation.Size = New Size(243, 43)
        cboSalesLocation.TabIndex = 48
        ' 
        ' lblSalesLocation
        ' 
        lblSalesLocation.AutoSize = True
        lblSalesLocation.Location = New Point(210, 883)
        lblSalesLocation.Name = "lblSalesLocation"
        lblSalesLocation.Size = New Size(104, 20)
        lblSalesLocation.TabIndex = 47
        lblSalesLocation.Text = "Sales Location"
        ' 
        ' dtpStartTime
        ' 
        dtpStartTime.CalendarFont = New Font("Segoe UI", 25F)
        dtpStartTime.Format = DateTimePickerFormat.Time
        dtpStartTime.Location = New Point(467, 916)
        dtpStartTime.Name = "dtpStartTime"
        dtpStartTime.ShowUpDown = True
        dtpStartTime.Size = New Size(257, 27)
        dtpStartTime.TabIndex = 50
        ' 
        ' lblStartTime
        ' 
        lblStartTime.AutoSize = True
        lblStartTime.Location = New Point(467, 892)
        lblStartTime.Name = "lblStartTime"
        lblStartTime.Size = New Size(77, 20)
        lblStartTime.TabIndex = 49
        lblStartTime.Text = "Start Time"
        ' 
        ' frmTransaction
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1811, 961)
        Controls.Add(dtpStartTime)
        Controls.Add(lblStartTime)
        Controls.Add(cboSalesLocation)
        Controls.Add(lblSalesLocation)
        Controls.Add(cboDeliveryType)
        Controls.Add(lblDelivery)
        Controls.Add(cboCourier)
        Controls.Add(lblCourier)
        Controls.Add(txtQuantity)
        Controls.Add(lblQuantity)
        Controls.Add(dtpShippingDate)
        Controls.Add(lblShippingDate)
        Controls.Add(cboStatus)
        Controls.Add(lblStatus)
        Controls.Add(txtShippingFee)
        Controls.Add(lblShippingFee)
        Controls.Add(lblNewContactPerson)
        Controls.Add(txtNewContactPerson)
        Controls.Add(lblNewCountryOrigin)
        Controls.Add(txtNewCountryOrigin)
        Controls.Add(lblNewCompanyName)
        Controls.Add(txtNewCompanyName)
        Controls.Add(cboNewStatus)
        Controls.Add(lblNewStatus)
        Controls.Add(lblNewStockCount)
        Controls.Add(txtNewStockCount)
        Controls.Add(lblNewSellPrice)
        Controls.Add(txtNewSellPrice)
        Controls.Add(lblNewSize)
        Controls.Add(txtNewSize)
        Controls.Add(lblNewBuyPrice)
        Controls.Add(txtNewBuyPrice)
        Controls.Add(lblNewColor)
        Controls.Add(txtNewColor)
        Controls.Add(lblNewItemName)
        Controls.Add(txtNewItemName)
        Controls.Add(Label1)
        Controls.Add(chkNewProduct)
        Controls.Add(chkNewTarget)
        Controls.Add(btnCancel)
        Controls.Add(btnProcess)
        Controls.Add(dtpReservationDate)
        Controls.Add(lblReservationDate)
        Controls.Add(txtDownPayment)
        Controls.Add(lblDownPayment)
        Controls.Add(cboProduct)
        Controls.Add(lblNewProductHeader)
        Controls.Add(cboTarget)
        Controls.Add(lblNewSupplierHeader)
        Controls.Add(lblTransactionHeader)
        Name = "frmTransaction"
        Text = "frmTransaction"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblTransactionHeader As Label
    Friend WithEvents lblNewSupplierHeader As Label
    Friend WithEvents cboTarget As ComboBox
    Friend WithEvents cboProduct As ComboBox
    Friend WithEvents lblNewProductHeader As Label
    Friend WithEvents lblDownPayment As Label
    Friend WithEvents txtDownPayment As TextBox
    Friend WithEvents lblReservationDate As Label
    Friend WithEvents dtpReservationDate As DateTimePicker
    Friend WithEvents btnProcess As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents chkNewTarget As CheckBox
    Friend WithEvents chkNewProduct As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblNewColor As Label
    Friend WithEvents txtNewColor As TextBox
    Friend WithEvents lblNewItemName As Label
    Friend WithEvents txtNewItemName As TextBox
    Friend WithEvents lblNewSize As Label
    Friend WithEvents txtNewSize As TextBox
    Friend WithEvents lblNewBuyPrice As Label
    Friend WithEvents txtNewBuyPrice As TextBox
    Friend WithEvents lblNewStockCount As Label
    Friend WithEvents txtNewStockCount As TextBox
    Friend WithEvents lblNewSellPrice As Label
    Friend WithEvents txtNewSellPrice As TextBox
    Friend WithEvents cboNewStatus As ComboBox
    Friend WithEvents lblNewStatus As Label
    Friend WithEvents lblNewContactPerson As Label
    Friend WithEvents txtNewContactPerson As TextBox
    Friend WithEvents lblNewCountryOrigin As Label
    Friend WithEvents txtNewCountryOrigin As TextBox
    Friend WithEvents lblNewCompanyName As Label
    Friend WithEvents txtNewCompanyName As TextBox
    Friend WithEvents txtShippingFee As TextBox
    Friend WithEvents lblShippingFee As Label
    Friend WithEvents cboStatus As ComboBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblShippingDate As Label
    Friend WithEvents dtpShippingDate As DateTimePicker
    Friend WithEvents txtQuantity As TextBox
    Friend WithEvents lblQuantity As Label
    Friend WithEvents cboCourier As ComboBox
    Friend WithEvents lblCourier As Label
    Friend WithEvents cboDeliveryType As ComboBox
    Friend WithEvents lblDelivery As Label
    Friend WithEvents cboSalesLocation As ComboBox
    Friend WithEvents lblSalesLocation As Label
    Friend WithEvents dtpStartTime As DateTimePicker
    Friend WithEvents lblStartTime As Label
End Class
