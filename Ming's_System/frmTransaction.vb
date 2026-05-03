Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Text.RegularExpressions

Public Class frmTransaction

    Private Const TX_RESTOCK As Integer = 1
    Private Const TX_SALES As Integer = 2
    Private Const TX_STORE_ASSIGN As Integer = 5
    Private Const TX_WORK_SCHEDULE As Integer = 6
    Private Const TX_INTERNAL_TRANSFER As Integer = 7

    Private Const LOCATION_MAIN_WAREHOUSE As String = "Main Warehouse"
    Private Const DELIVERY_TYPE_COURIER As String = "Courier Delivery"
    Private Const STATUS_PENDING As String = "Pending"
    Private Const STATUS_DELIVERED As String = "Delivered"
    Private Const LOW_STOCK_THRESHOLD As Integer = 5
    Private Const EMAIL_REGEX As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"

    ' ── CONTROLLER INSTANCE ────────────────────────────────────────
    Private ctrl As New TransactionController()
    ' ───────────────────────────────────────────────────────────────


    ' ==============================================================
    '  FORM LOAD
    ' ==============================================================

    Private Sub frmTransaction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.WindowState = FormWindowState.Maximized
            chkNewTarget.Checked = False
            chkNewProduct.Checked = False

            chkNewTarget_CheckedChanged(Nothing, Nothing)
            chkNewProduct_CheckedChanged(Nothing, Nothing)

            ConfigureTransactionUI()
            LoadTransactionDropdowns()
        Catch ex As Exception
            ShowError("Error loading transaction form: " & ex.Message)
        End Try
    End Sub


    ' ==============================================================
    '  KEY-PRESS GUARDS  (unchanged)
    ' ==============================================================

    Private Sub DecimalOnly_KeyPress(sender As Object, e As KeyPressEventArgs) _
        Handles txtNewBuyPrice.KeyPress, txtNewSellPrice.KeyPress,
                txtShippingFee.KeyPress, txtDownPayment.KeyPress

        Dim isControl = Char.IsControl(e.KeyChar)
        Dim isDigit = Char.IsDigit(e.KeyChar)
        Dim isDot = (e.KeyChar = "."c)
        Dim dotAlreadyPresent = isDot AndAlso CType(sender, TextBox).Text.Contains(".")

        e.Handled = Not isControl AndAlso Not isDigit AndAlso Not isDot OrElse dotAlreadyPresent
    End Sub

    Private Sub IntegerOnly_KeyPress(sender As Object, e As KeyPressEventArgs) _
        Handles txtQuantity.KeyPress, txtNewStockCount.KeyPress

        e.Handled = Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar)
    End Sub


    ' ==============================================================
    '  UI CONFIGURATION  (unchanged except ConfigureSalesUI)
    ' ==============================================================

    Private Sub ConfigureTransactionUI()
        HideAllOptionalControls()

        Select Case tag1
            Case TX_RESTOCK : ConfigureRestockUI()
            Case TX_SALES : ConfigureSalesUI()
            Case TX_STORE_ASSIGN : ConfigureStoreAssignUI()
            Case TX_WORK_SCHEDULE : ConfigureWorkScheduleUI()
            Case TX_INTERNAL_TRANSFER : ConfigureInternalTransferUI()
        End Select
    End Sub

    Private Sub HideAllOptionalControls()
        SetVisible(False, lblStatus, cboStatus,
                         lblShippingFee, txtShippingFee,
                         lblShippingDate, dtpShippingDate,
                         cboCourier, lblCourier,
                         lblDownPayment, txtDownPayment,
                         lblQuantity, txtQuantity,
                         lblSalesLocation, cboSalesLocation,
                         lblDelivery)

        If lblStartTime IsNot Nothing Then lblStartTime.Visible = False
        If dtpStartTime IsNot Nothing Then dtpStartTime.Visible = False
        If cboDeliveryType IsNot Nothing Then cboDeliveryType.Visible = False
    End Sub

    Private Sub ConfigureRestockUI()
        Me.Text = "Restock Inventory from Supplier"
        lblTransactionHeader.Text = "RESTOCKING TRANSACTION"
        lblNewSupplierHeader.Text = "Select Supplier:"
        lblNewProductHeader.Text = "Select Product:"
        SetTargetLabels("COMPANY NAME", "CONTACT PERSON", "COUNTRY ORIGIN")
        SetVisible(True, lblQuantity, txtQuantity)
        lblReservationDate.Text = "Supply Date:"
        dtpReservationDate.Visible = True
        btnProcess.Text = "Process Restock (Add Stock)"
    End Sub

    Private Sub ConfigureSalesUI()
        Me.Text = "Record Customer Purchase"
        lblTransactionHeader.Text = "SALES TRANSACTION"
        lblNewSupplierHeader.Text = "Select Customer:"
        lblNewProductHeader.Text = "Select Product:"
        SetTargetLabels("CUSTOMER NAME", "ADDRESS", Nothing)
        chkNewTarget.Text = "Add New Customer"
        SetVisible(True, lblDownPayment, txtDownPayment,
                         lblQuantity, txtQuantity,
                         lblStatus, cboStatus,
                         lblSalesLocation, cboSalesLocation)

        PopulateComboBox(cboStatus, STATUS_PENDING, STATUS_DELIVERED)
        cboStatus.SelectedIndex = 0

        If cboDeliveryType IsNot Nothing Then
            cboDeliveryType.Visible = True
            PopulateComboBox(cboDeliveryType, "Walk-in / Pickup", DELIVERY_TYPE_COURIER)
            cboDeliveryType.SelectedIndex = 0
        End If

        lblReservationDate.Text = "Reservation Date:"
        dtpReservationDate.Visible = True
        btnProcess.Text = "Process Sale"

        ' ── CONTROLLER: load branch names instead of raw readquery ──
        cboSalesLocation.Items.Clear()
        cboSalesLocation.Items.Add(LOCATION_MAIN_WAREHOUSE)
        For Each b In ctrl.GetBranches()
            cboSalesLocation.Items.Add(b)
        Next
        cboSalesLocation.SelectedIndex = 0
    End Sub

    Private Sub ConfigureStoreAssignUI()
        Me.Text = "Assign Product to Branch"
        lblTransactionHeader.Text = "STORAGE TRANSACTION"
        lblNewSupplierHeader.Text = "Select Branch:"
        lblNewProductHeader.Text = "Select Product:"
        SetTargetLabels("BRANCH NAME", "ADDRESS", "OPERATING HOURS")
        chkNewTarget.Text = "Add New Branch"
        SetVisible(True, lblQuantity, txtQuantity)
        lblReservationDate.Text = "Restock Date:"
        dtpReservationDate.Visible = True
        btnProcess.Text = "Assign to Branch"
    End Sub

    Private Sub ConfigureWorkScheduleUI()
        Me.Text = "Assign Employee to Branch"
        lblTransactionHeader.Text = "WORK SCHEDULE TRANSACTION"
        lblNewSupplierHeader.Text = "Select Employee:"
        lblNewProductHeader.Text = "Select Branch:"
        SetTargetLabels("EMPLOYEE NAME", "ROLE", "EMAIL ADDRESS")
        chkNewTarget.Text = "Add New Employee"
        chkNewProduct.Text = "Add New Branch"

        lblReservationDate.Text = "Scheduled Date:"
        dtpReservationDate.Visible = True

        lblDownPayment.Text = "Start Time:"
        lblDownPayment.Visible = True
        txtDownPayment.Visible = False
        dtpStartTime.Location = txtDownPayment.Location
        dtpStartTime.Visible = True

        lblQuantity.Text = "End Time:"
        lblQuantity.Visible = True
        txtQuantity.Visible = False

        dtpShippingDate.Format = DateTimePickerFormat.Time
        dtpShippingDate.ShowUpDown = True
        dtpShippingDate.Location = txtQuantity.Location
        dtpShippingDate.Visible = True

        btnProcess.Text = "Assign Work Schedule"
    End Sub

    Private Sub ConfigureInternalTransferUI()
        Me.Text = "Internal Stock Transfer"
        lblTransactionHeader.Text = "INTERNAL TRANSFER LOGISTICS"
        lblSalesLocation.Text = "Source (FROM):"
        SetVisible(True, lblSalesLocation, cboSalesLocation, lblQuantity, txtQuantity)
        lblNewSupplierHeader.Text = "Destination (TO):"
        lblNewProductHeader.Text = "Select Product:"
        lblReservationDate.Text = "Transfer Date:"
        dtpReservationDate.Visible = True
        btnProcess.Text = "Execute Transfer"
        chkNewTarget.Visible = False
        chkNewProduct.Visible = False
    End Sub


    ' ==============================================================
    '  DROPDOWN LOADERS  — all routed through controller
    ' ==============================================================

    Private Sub LoadTransactionDropdowns()
        cboTarget.Items.Clear()
        cboProduct.DataSource = Nothing
        cboProduct.Items.Clear()
        cboCourier.Items.Clear()
        If tag1 <> TX_SALES Then cboSalesLocation.Items.Clear()

        Try
            Select Case tag1
                Case TX_RESTOCK
                    LoadComboFromList(cboTarget, ctrl.GetSuppliers())
                    LoadProductDropdown()

                Case TX_SALES
                    LoadComboFromList(cboTarget, ctrl.GetCustomers())
                    LoadProductDropdown()
                    LoadComboFromList(cboCourier, ctrl.GetCouriers())

                Case TX_STORE_ASSIGN
                    LoadComboFromList(cboTarget, ctrl.GetBranches())
                    LoadProductDropdown()

                Case TX_WORK_SCHEDULE
                    LoadComboFromList(cboTarget, ctrl.GetEmployees())
                    LoadComboFromList(cboProduct, ctrl.GetBranches())   ' branches into cboProduct slot

                Case TX_INTERNAL_TRANSFER
                    cboTarget.Items.Add(LOCATION_MAIN_WAREHOUSE)
                    cboSalesLocation.Items.Add(LOCATION_MAIN_WAREHOUSE)
                    For Each b In ctrl.GetBranches()
                        cboTarget.Items.Add(b)
                        cboSalesLocation.Items.Add(b)
                    Next
                    cboSalesLocation.SelectedIndex = 0
            End Select
        Catch ex As Exception
            ShowError("Error loading dropdowns: " & ex.Message)
        End Try
    End Sub

    ''' <summary>Adds a List(Of String) into any ComboBox.</summary>
    Private Sub LoadComboFromList(combo As ComboBox, items As List(Of String))
        For Each item In items
            combo.Items.Add(item)
        Next
    End Sub

    ''' <summary>Binds the product DataTable returned by the controller.</summary>
    Private Sub LoadProductDropdown()
        Dim dt = ctrl.GetAllProducts()
        cboProduct.DataSource = dt
        cboProduct.DisplayMember = "DisplayName"
        cboProduct.ValueMember = "product_id"
        cboProduct.SelectedIndex = -1
    End Sub


    ' ==============================================================
    '  COMBO-BOX EVENT HANDLERS
    ' ==============================================================

    Private Sub cboSalesLocation_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboSalesLocation.SelectedIndexChanged

        Dim isRelevant = (tag1 = TX_SALES OrElse tag1 = TX_INTERNAL_TRANSFER)
        If Not isRelevant OrElse cboSalesLocation.Text.Trim() = "" Then Return

        Try
            ' ── CONTROLLER: filtered product list for the chosen location ──
            Dim dt = ctrl.GetProductsInStock(cboSalesLocation.Text.Trim())
            cboProduct.DataSource = Nothing
            cboProduct.Items.Clear()
            cboProduct.DataSource = dt
            cboProduct.DisplayMember = "DisplayName"
            cboProduct.ValueMember = "product_id"
            cboProduct.SelectedIndex = -1
            HideProductPriceAndStock()
        Catch ex As Exception
            ShowError("Error loading inventory for location: " & ex.Message)
        End Try
    End Sub

    Private Sub cboProduct_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboProduct.SelectedIndexChanged

        If cboProduct.SelectedIndex = -1 Then
            HideProductPriceAndStock()
            Return
        End If
        If tag1 = TX_WORK_SCHEDULE OrElse TypeOf cboProduct.SelectedValue Is DataRowView Then Return

        Try
            Dim productId = Convert.ToInt32(cboProduct.SelectedValue)
            Dim isBranchSelected = (tag1 = TX_SALES OrElse tag1 = TX_INTERNAL_TRANSFER) _
                                   AndAlso cboSalesLocation.Text <> LOCATION_MAIN_WAREHOUSE _
                                   AndAlso cboSalesLocation.Text <> ""
            Dim location = If(isBranchSelected, cboSalesLocation.Text.Trim(), LOCATION_MAIN_WAREHOUSE)

            ' ── CONTROLLER: price + stock for the selected product/location ──
            Dim detail = ctrl.GetProductDetail(productId, location)
            lblNewStockCount.Text = detail.StockLabel
            txtNewBuyPrice.Text = detail.Price.ToString()
            txtNewStockCount.Text = detail.Stock.ToString()
            SetVisible(True, lblNewBuyPrice, txtNewBuyPrice, lblNewStockCount, txtNewStockCount)
        Catch ex As Exception
            ShowError("Error fetching product details: " & ex.Message)
        End Try
    End Sub

    Private Sub cboDeliveryType_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboDeliveryType.SelectedIndexChanged

        dtpShippingDate.Format = DateTimePickerFormat.Long
        dtpShippingDate.ShowUpDown = False

        Dim isCourier = (cboDeliveryType.Text = DELIVERY_TYPE_COURIER)
        SetVisible(isCourier, lblCourier, cboCourier,
                              lblShippingFee, txtShippingFee,
                              lblShippingDate, dtpShippingDate)
    End Sub


    ' ==============================================================
    '  NEW ENTITY TOGGLE HANDLERS  (unchanged)
    ' ==============================================================

    Private Sub chkNewTarget_CheckedChanged(sender As Object, e As EventArgs) _
        Handles chkNewTarget.CheckedChanged

        Dim isNew = chkNewTarget.Checked
        cboTarget.Visible = Not isNew
        SetVisible(isNew, lblNewCompanyName, txtNewCompanyName,
                          lblNewContactPerson, txtNewContactPerson)

        Dim showThirdField = isNew AndAlso tag1 <> TX_SALES
        lblNewCountryOrigin.Visible = showThirdField
        txtNewCountryOrigin.Visible = showThirdField

        If Not isNew Then ClearNewTargetInputs()
    End Sub

    Private Sub chkNewProduct_CheckedChanged(sender As Object, e As EventArgs) _
        Handles chkNewProduct.CheckedChanged

        Dim isNew = chkNewProduct.Checked
        cboProduct.Visible = Not isNew
        SetVisible(isNew, lblNewItemName, txtNewItemName)

        If isNew Then
            ShowNewProductFields()
        Else
            HideNewProductFields()
        End If
    End Sub

    Private Sub ShowNewProductFields()
        lblNewBuyPrice.Text = "Buying Price:"
        txtNewBuyPrice.ReadOnly = False
        lblNewStockCount.Text = "Initial Stock:"
        txtNewStockCount.ReadOnly = False
        SetVisible(True, lblNewBuyPrice, txtNewBuyPrice,
                         lblNewStockCount, txtNewStockCount,
                         lblNewSellPrice, txtNewSellPrice)

        Dim isWorkSchedule = (tag1 = TX_WORK_SCHEDULE)
        SetVisible(Not isWorkSchedule, lblNewColor, txtNewColor,
                                       lblNewSize, txtNewSize,
                                       lblNewStatus, cboNewStatus)
        ClearNewProductInputs()
    End Sub

    Private Sub HideNewProductFields()
        lblNewBuyPrice.Text = "Current Price:"
        txtNewBuyPrice.ReadOnly = True
        lblNewStockCount.Text = "Current Stock:"
        txtNewStockCount.ReadOnly = True
        HideProductPriceAndStock()
        SetVisible(False, lblNewSellPrice, txtNewSellPrice,
                          lblNewColor, txtNewColor,
                          lblNewSize, txtNewSize,
                          lblNewStatus, cboNewStatus)
    End Sub


    ' ==============================================================
    '  PROCESS BUTTON  (unchanged)
    ' ==============================================================

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        If Not IsFormReadyToProcess() Then Return
        Try
            Dim success As Boolean = False
            Select Case tag1
                Case TX_RESTOCK : success = ProcessRestocking()
                Case TX_SALES : success = ProcessSales()
                Case TX_STORE_ASSIGN : success = ProcessStoreAssignment()
                Case TX_WORK_SCHEDULE : success = ProcessWorkSchedule()
                Case TX_INTERNAL_TRANSFER : success = ProcessInternalTransfer()
            End Select
            If success Then OnTransactionSuccess()
        Catch ex As Exception
            ShowError("Error processing transaction: " & ex.Message)
        End Try
    End Sub

    Private Function IsFormReadyToProcess() As Boolean
        If Not chkNewTarget.Checked AndAlso cboTarget.Text = "" Then
            ShowWarning("Please select a target entity to proceed.", "Validation Error")
            Return False
        End If
        If Not chkNewProduct.Checked AndAlso cboProduct.Text = "" Then
            ShowWarning("Please select a secondary entity to proceed.", "Validation Error")
            Return False
        End If
        Return True
    End Function

    Private Sub OnTransactionSuccess()
        If tag1 <> TX_SALES Then
            MsgBox("Transaction processed successfully!", MsgBoxStyle.Information)
        End If
        txtQuantity.Clear()
        If txtDownPayment.Visible Then txtDownPayment.Clear()
        If txtShippingFee.Visible Then txtShippingFee.Clear()
        cboProduct.SelectedIndex = -1
        chkNewTarget.Checked = False
        chkNewProduct.Checked = False
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub


    ' ==============================================================
    '  TRANSACTION ENGINES  — delegate DB work to the controller
    ' ==============================================================

    ' ── RESTOCK ─────────────────────────────────────────────────

    Private Function ProcessRestocking() As Boolean
        Dim supplierId = ResolveOrCreateSupplier()
        If supplierId = -1 Then Return False

        Dim productId = ResolveOrCreateProduct()
        If productId = -1 Then Return False

        Dim qty As Integer
        If Not TryParseQuantity(txtQuantity.Text, qty) Then Return False

        ' Buying price: from the "new product" textbox or queried from DB
        Dim buyingPrice As Decimal = 0
        If chkNewProduct.Checked Then
            buyingPrice = ParseDecimal(txtNewBuyPrice.Text)
        Else
            readquery($"SELECT buying_price FROM product WHERE product_id = {productId}")
            If cmdread.HasRows AndAlso cmdread.Read() Then
                buyingPrice = Convert.ToDecimal(cmdread("buying_price"))
            End If
        End If

        ' ── CONTROLLER: writes provides + updates stock_count ──
        Return ctrl.ProcessRestock(supplierId, productId, qty, FormatDate(dtpReservationDate), buyingPrice)
    End Function

    ''' <summary>Returns supplier_id (-1 on failure).</summary>
    Private Function ResolveOrCreateSupplier() As Integer
        If Not chkNewTarget.Checked Then
            Dim name = EscSql(cboTarget.Text.Trim())
            readquery($"SELECT supplier_id FROM supplier WHERE company_name = '{name}'")
            If cmdread.HasRows AndAlso cmdread.Read() Then Return Convert.ToInt32(cmdread("supplier_id"))
            Return -1
        End If

        Dim newName = txtNewCompanyName.Text.Trim()
        If newName = "" Then
            ShowWarning("Please enter Supplier details.", "Validation Error")
            Return -1
        End If

        ' ── CONTROLLER: insert if not exists, return ID ──
        Return ctrl.ResolveSupplier(EscSql(newName),
                                    EscSql(txtNewContactPerson.Text.Trim()),
                                    EscSql(txtNewCountryOrigin.Text.Trim()))
    End Function


    ' ── SALES ────────────────────────────────────────────────────

    Private Function ProcessSales() As Boolean
        ' Capture display name before resolving (needed for receipt)
        Dim customerName = If(chkNewTarget.Checked,
                              txtNewCompanyName.Text.Trim(),
                              cboTarget.Text.Trim())

        Dim customerId = ResolveOrCreateCustomer()
        If customerId = -1 Then Return False

        Dim productId = ResolveOrCreateProduct()
        If productId = -1 Then Return False

        Dim qty As Integer
        If Not TryParseQuantity(txtQuantity.Text, qty) Then Return False

        Dim salesLocation = cboSalesLocation.Text.Trim()
        If salesLocation = "" Then
            ShowWarning("Select Sales Location.", "Validation Error")
            Return False
        End If

        Dim sellingPrice As Double = If(chkNewProduct.Checked,
                                        ParseDecimal(txtNewSellPrice.Text),
                                        ParseDecimal(txtNewBuyPrice.Text))
        Dim shippingFee As Double = 0
        Dim shippingDateSql = "NULL"
        Dim statusValue = If(cboStatus.Text.Trim() = "", STATUS_PENDING, cboStatus.Text.Trim())
        Dim downPayment = ParseDecimal(txtDownPayment.Text)
        Dim grandTotal = (sellingPrice * qty) + shippingFee

        If cboDeliveryType.Text = DELIVERY_TYPE_COURIER Then
            If Not IsValidCourierDelivery(shippingFee, shippingDateSql) Then Return False
            grandTotal = (sellingPrice * qty) + shippingFee
        End If

        If Not IsValidPayment(downPayment, grandTotal, statusValue) Then Return False
        If Not IsStockSufficient(productId, qty, salesLocation) Then Return False

        ' ── CONTROLLER: build request struct & execute sale ──
        Dim req As New TransactionController.SaleRequest() With {
            .CustomerId = customerId,
            .ProductId = productId,
            .Quantity = qty,
            .ReservationDate = FormatDate(dtpReservationDate),
            .Status = statusValue,
            .DownPayment = downPayment,
            .ShippingFee = shippingFee,
            .ShippingDateSql = shippingDateSql,
            .SalesLocation = EscSql(salesLocation),
            .IsCourierDelivery = (cboDeliveryType.Text = DELIVERY_TYPE_COURIER),
            .CourierName = EscSql(cboCourier.Text.Trim())
        }

        If ctrl.ProcessSale(req) Then
            Dim productDisplayName = If(chkNewProduct.Checked, txtNewItemName.Text, cboProduct.Text)
            MsgBox(BuildReceiptText(customerName, salesLocation, productDisplayName,
                                   qty, sellingPrice, shippingFee, grandTotal, downPayment, statusValue),
                   MsgBoxStyle.Information, "Transaction Processed - Digital Receipt")

            ' ── CONTROLLER: low-stock alert after deduction ──
            CheckAndShowLowStockAlert(productId, salesLocation)
            Return True
        End If
        Return False
    End Function

    ''' <summary>Returns customer_id (-1 on failure).</summary>
    Private Function ResolveOrCreateCustomer() As Integer
        If Not chkNewTarget.Checked Then
            Dim name = EscSql(cboTarget.Text.Trim())
            readquery($"SELECT customer_id FROM customer WHERE customer_name = '{name}'")
            If cmdread.HasRows AndAlso cmdread.Read() Then Return Convert.ToInt32(cmdread("customer_id"))
            Return -1
        End If

        Dim newName = txtNewCompanyName.Text.Trim()
        If newName = "" Then
            ShowWarning("Please enter Customer details.", "Validation Error")
            Return -1
        End If

        ' ── CONTROLLER: insert if not exists, return ID ──
        Return ctrl.ResolveCustomer(EscSql(newName), EscSql(txtNewContactPerson.Text.Trim()))
    End Function


    ' ── STORE ASSIGNMENT ─────────────────────────────────────────

    Private Function ProcessStoreAssignment() As Boolean
        Dim branchId = ResolveOrCreateBranch()
        If branchId = -1 Then Return False

        Dim productId = ResolveOrCreateProduct()
        If productId = -1 Then Return False

        Dim qty As Integer
        If Not TryParseQuantity(txtQuantity.Text, qty) Then Return False

        ' ── CONTROLLER: warehouse stock check ──
        If ctrl.GetWarehouseStock(productId) < qty Then
            ShowWarning("Not enough stock in main inventory to send to branch!", "Stock Error")
            Return False
        End If

        ' ── CONTROLLER: insert/update stores, deduct from warehouse ──
        Return ctrl.ProcessStoreAssignment(branchId, productId, qty, FormatDate(dtpReservationDate))
    End Function

    ''' <summary>Returns branch_id (-1 on failure).</summary>
    Private Function ResolveOrCreateBranch() As Integer
        If Not chkNewTarget.Checked Then
            Dim name = EscSql(cboTarget.Text.Trim())
            readquery($"SELECT branch_id FROM branch WHERE branch_name = '{name}'")
            If cmdread.HasRows AndAlso cmdread.Read() Then Return Convert.ToInt32(cmdread("branch_id"))
            Return -1
        End If

        Dim newName = txtNewCompanyName.Text.Trim()
        If newName = "" Then Return -1

        ' ── CONTROLLER: insert if not exists, return ID ──
        Return ctrl.ResolveBranch(EscSql(newName),
                                  EscSql(txtNewContactPerson.Text.Trim()),
                                  EscSql(txtNewCountryOrigin.Text.Trim()))
    End Function


    ' ── WORK SCHEDULE ────────────────────────────────────────────

    Private Function ProcessWorkSchedule() As Boolean
        Dim employeeId = ResolveOrCreateEmployee()
        If employeeId = -1 Then Return False

        Dim scheduledDate = FormatDate(dtpReservationDate)
        Dim startTime = dtpStartTime.Value.ToString("HH:mm:ss")
        Dim endTime = dtpShippingDate.Value.ToString("HH:mm:ss")

        If dtpStartTime.Value.TimeOfDay >= dtpShippingDate.Value.TimeOfDay Then
            ShowWarning("Start Time must be earlier than End Time.", "Validation Error")
            Return False
        End If

        ' ── CONTROLLER: overlap check using employee ID ──
        If ctrl.HasScheduleConflict(employeeId, scheduledDate, startTime, endTime) Then
            Dim empName = If(chkNewTarget.Checked,
                             txtNewCompanyName.Text.Trim(),
                             cboTarget.Text.Trim())
            ShowWarning($"Schedule Conflict! {empName} already has an overlapping shift on {scheduledDate}.",
                        "HR Warning")
            Return False
        End If

        Dim branchId = ResolveOrCreateBranchForSchedule()
        If branchId = -1 Then Return False

        ' ── CONTROLLER: insert works_in row ──
        Return ctrl.ProcessWorkSchedule(employeeId, branchId, scheduledDate, startTime, endTime)
    End Function

    ''' <summary>Returns employee_id (-1 on failure).</summary>
    Private Function ResolveOrCreateEmployee() As Integer
        If Not chkNewTarget.Checked Then
            Dim name = EscSql(cboTarget.Text.Trim())
            readquery($"SELECT employee_id FROM employee WHERE employee_name = '{name}'")
            If cmdread.HasRows AndAlso cmdread.Read() Then Return Convert.ToInt32(cmdread("employee_id"))
            Return -1
        End If

        Dim newName = txtNewCompanyName.Text.Trim()
        If newName = "" Then Return -1

        Dim email = txtNewCountryOrigin.Text.Trim()
        If Not Regex.IsMatch(email, EMAIL_REGEX) Then
            ShowWarning("Please enter a valid Email Address (e.g., user@domain.com).", "Data Hygiene Error")
            Return -1
        End If

        ' ── CONTROLLER: insert if not exists, return ID ──
        Return ctrl.ResolveEmployee(EscSql(newName),
                                    EscSql(txtNewContactPerson.Text.Trim()),
                                    EscSql(email))
    End Function

    ''' <summary>Resolves/creates the branch chosen in the schedule flow (cboProduct slot).</summary>
    Private Function ResolveOrCreateBranchForSchedule() As Integer
        If Not chkNewProduct.Checked Then
            Dim name = EscSql(cboProduct.Text.Trim())
            readquery($"SELECT branch_id FROM branch WHERE branch_name = '{name}'")
            If cmdread.HasRows AndAlso cmdread.Read() Then Return Convert.ToInt32(cmdread("branch_id"))
            Return -1
        End If

        Dim newName = txtNewItemName.Text.Trim()
        If newName = "" Then Return -1

        ' In work-schedule mode the "price" boxes are reused for address / operating hours
        Return ctrl.ResolveBranch(EscSql(newName),
                                  EscSql(txtNewBuyPrice.Text.Trim()),
                                  EscSql(txtNewSellPrice.Text.Trim()))
    End Function


    ' ── INTERNAL TRANSFER ────────────────────────────────────────

    Private Function ProcessInternalTransfer() As Boolean
        Dim sourceLoc = cboSalesLocation.Text.Trim()
        Dim destLoc = cboTarget.Text.Trim()

        If sourceLoc = "" OrElse destLoc = "" Then
            ShowWarning("Please select Source and Destination.", "Validation Error")
            Return False
        End If
        If sourceLoc = destLoc Then
            ShowWarning("Source and Destination cannot be the same.", "Validation Error")
            Return False
        End If
        If cboProduct.SelectedIndex = -1 Then
            ShowWarning("Please select a product.", "Validation Error")
            Return False
        End If

        Dim qty As Integer
        If Not TryParseQuantity(txtQuantity.Text, qty) Then Return False

        Dim productId = Convert.ToInt32(cboProduct.SelectedValue)

        If Not IsTransferStockSufficient(productId, qty, sourceLoc) Then Return False

        ' ── CONTROLLER: deduct source, add to destination ──
        If ctrl.ProcessInternalTransfer(productId, qty,
                                        EscSql(sourceLoc), EscSql(destLoc),
                                        FormatDate(dtpReservationDate)) Then
            CheckAndShowLowStockAlert(productId, sourceLoc)
            Return True
        End If
        Return False
    End Function


    ' ==============================================================
    '  SHARED PRODUCT RESOLVER  (used by all transaction types)
    ' ==============================================================

    ''' <summary>Returns product_id (-1 on failure).</summary>
    Private Function ResolveOrCreateProduct() As Integer
        If Not chkNewProduct.Checked Then
            Return Convert.ToInt32(cboProduct.SelectedValue)
        End If

        Dim pName = txtNewItemName.Text.Trim()
        If pName = "" Then
            ShowWarning("Please enter Product Name.", "Validation Error")
            Return -1
        End If

        Dim buyPrice = ParseDecimal(txtNewBuyPrice.Text)
        Dim sellPrice = ParseDecimal(txtNewSellPrice.Text)

        If sellPrice <= buyPrice Then
            ShowWarning("Selling price must be higher than buying price!", "Pricing Error")
            Return -1
        End If

        ' ── CONTROLLER: insert if not exists, return ID ──
        Return ctrl.ResolveProduct(EscSql(pName),
                                   EscSql(txtNewColor.Text.Trim()),
                                   EscSql(txtNewSize.Text.Trim()),
                                   buyPrice, sellPrice,
                                   EscSql(cboNewStatus.Text.Trim()),
                                   CInt(Val(txtNewStockCount.Text)))
    End Function


    ' ==============================================================
    '  VALIDATION HELPERS  (unchanged logic)
    ' ==============================================================

    Private Function IsValidCourierDelivery(ByRef shippingFee As Double,
                                            ByRef shippingDateSql As String) As Boolean
        If cboCourier.Text.Trim() = "" Then
            ShowWarning("Select Courier.", "Validation Error")
            Return False
        End If
        If dtpShippingDate.Value.Date < dtpReservationDate.Value.Date Then
            ShowWarning("Shipping Date cannot be earlier than the Reservation Date.", "Logistics Error")
            Return False
        End If
        shippingFee = ParseDecimal(txtShippingFee.Text)
        shippingDateSql = $"'{dtpShippingDate.Value:yyyy-MM-dd}'"
        Return True
    End Function

    Private Function IsValidPayment(downPayment As Double, grandTotal As Double,
                                    ByRef statusValue As String) As Boolean
        If downPayment < 0 Then
            ShowWarning("Payment cannot be a negative number.", "Payment Error")
            Return False
        End If
        If downPayment > grandTotal Then
            ShowWarning($"Payment (₱{downPayment}) cannot exceed Grand Total ₱{grandTotal}.", "Payment Error")
            Return False
        End If
        If statusValue = STATUS_DELIVERED AndAlso downPayment < grandTotal Then
            Dim answer = MsgBox("Order is marked 'Delivered' but balance is unpaid. Change status to 'Pending'?",
                                MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Balance Due Warning")
            If answer = MsgBoxResult.Yes Then
                statusValue = STATUS_PENDING
            Else
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>Checks sale stock using controller's stock getters.</summary>
    Private Function IsStockSufficient(productId As Integer, qty As Integer, location As String) As Boolean
        If location = LOCATION_MAIN_WAREHOUSE Then
            ' ── CONTROLLER ──
            If ctrl.GetWarehouseStock(productId) < qty Then
                ShowWarning("Insufficient stock in Warehouse!", "Stock Error")
                Return False
            End If
        Else
            ' ── CONTROLLER ──
            Dim branchStock = ctrl.GetBranchStock(productId, EscSql(location))
            If branchStock = -1 Then
                ShowWarning("Branch does not carry this product!", "Stock Error")
                Return False
            End If
            If branchStock < qty Then
                ShowWarning("Insufficient stock at Branch!", "Stock Error")
                Return False
            End If
        End If
        Return True
    End Function

    Private Function IsTransferStockSufficient(productId As Integer, qty As Integer, sourceLoc As String) As Boolean
        If sourceLoc = LOCATION_MAIN_WAREHOUSE Then

            If ctrl.GetWarehouseStock(productId) < qty Then
                ShowWarning("Not enough stock in the Main Warehouse!", "Stock Error")
                Return False
            End If
        Else

            Dim branchStock = ctrl.GetBranchStock(productId, EscSql(sourceLoc))
            If branchStock = -1 Then
                ShowWarning("Source branch does not have this product!", "Stock Error")
                Return False
            End If
            If branchStock < qty Then
                ShowWarning("Not enough stock at source branch!", "Stock Error")
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub CheckAndShowLowStockAlert(productId As Integer, location As String)
        Try
            Dim ls = ctrl.CheckLowStock(productId, EscSql(location))
            If ls.IsLow Then
                Dim locationLabel = If(location = LOCATION_MAIN_WAREHOUSE, "Main Warehouse", location)
                ShowWarning($"⚠️ CRITICAL: '{ls.ItemName}' at {locationLabel} has dropped to {ls.CurrentStock} units!" &
                            vbCrLf & "Please arrange a restock immediately.", "Automated Low Stock Warning")
            End If
        Catch
        End Try
    End Sub

    Private Function BuildReceiptText(customerName As String, location As String,
                                      productName As String, qty As Integer,
                                      unitPrice As Double, shippingFee As Double,
                                      grandTotal As Double, paid As Double,
                                      orderStatus As String) As String
        Dim balance = grandTotal - paid
        Return String.Join(vbCrLf,
            "===================================",
            "        MING'S CRAFT INVOICE       ",
            "===================================",
            $"Date:     {Now:yyyy-MM-dd HH:mm}",
            $"Customer: {customerName}",
            $"Location: {location}",
            "-----------------------------------",
            $"Item:     {productName}",
            $"Qty:      {qty} @ ₱{unitPrice}",
            $"Shipping: ₱{shippingFee}",
            "-----------------------------------",
            $"GRAND TOTAL:  ₱{grandTotal}",
            $"PAID:         ₱{paid}",
            $"BALANCE DUE:  ₱{balance}",
            "===================================",
            $"Order Status: {orderStatus}")
    End Function


    ' ==============================================================
    '  UTILITY HELPERS  (unchanged)
    ' ==============================================================

    Private Function EscSql(value As String) As String
        Return value.Replace("'", "''")
    End Function

    Private Function FormatDate(dtp As DateTimePicker) As String
        Return dtp.Value.ToString("yyyy-MM-dd")
    End Function

    Private Function TryParseQuantity(raw As String, ByRef result As Integer) As Boolean
        If Integer.TryParse(raw.Trim(), result) AndAlso result > 0 Then Return True
        ShowWarning("Please enter a valid positive quantity.", "Validation Error")
        Return False
    End Function

    Private Function ParseDecimal(raw As String) As Double
        Dim result As Double
        Double.TryParse(raw.Trim(), result)
        Return result
    End Function

    Private Sub SetVisible(visible As Boolean, ParamArray controls() As Control)
        For Each ctrl As Control In controls
            If ctrl IsNot Nothing Then ctrl.Visible = visible
        Next
    End Sub

    Private Sub SetTargetLabels(companyLabel As String, contactLabel As String, countryLabel As String)
        lblNewCompanyName.Text = companyLabel
        lblNewContactPerson.Text = contactLabel
        If countryLabel IsNot Nothing Then lblNewCountryOrigin.Text = countryLabel
    End Sub

    Private Sub PopulateComboBox(combo As ComboBox, ParamArray items() As String)
        combo.Items.Clear()
        combo.Items.AddRange(items)
    End Sub

    Private Sub HideProductPriceAndStock()
        SetVisible(False, lblNewBuyPrice, txtNewBuyPrice, lblNewStockCount, txtNewStockCount)
        txtNewBuyPrice.Clear()
        txtNewStockCount.Clear()
    End Sub

    Private Sub ClearNewTargetInputs()
        txtNewCompanyName.Clear()
        txtNewContactPerson.Clear()
        txtNewCountryOrigin.Clear()
    End Sub

    Private Sub ClearNewProductInputs()
        txtNewItemName.Clear()
        txtNewBuyPrice.Clear()
        txtNewSellPrice.Clear()
        txtNewColor.Clear()
        txtNewSize.Clear()
        txtNewStockCount.Clear()
        cboNewStatus.SelectedIndex = -1
    End Sub

    Private Sub ShowError(message As String)
        MsgBox(message, MsgBoxStyle.Critical, "Error")
    End Sub

    Private Sub ShowWarning(message As String, title As String)
        MsgBox(message, MsgBoxStyle.Exclamation, title)
    End Sub

End Class