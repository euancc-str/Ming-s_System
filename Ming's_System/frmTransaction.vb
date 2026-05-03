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

    Private ctrl As New TransactionController()
    Private service As New TransactionService(ctrl)

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

    ' ========================================================================
    '  UI KEYPRESS VALIDATIONS (Keep these, they stop bad typing early)
    ' ========================================================================
    Private Sub DecimalOnly_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewBuyPrice.KeyPress, txtNewSellPrice.KeyPress, txtShippingFee.KeyPress, txtDownPayment.KeyPress
        Dim isControl = Char.IsControl(e.KeyChar)
        Dim isDigit = Char.IsDigit(e.KeyChar)
        Dim isDot = (e.KeyChar = "."c)
        Dim dotAlreadyPresent = isDot AndAlso CType(sender, TextBox).Text.Contains(".")
        e.Handled = Not isControl AndAlso Not isDigit AndAlso Not isDot OrElse dotAlreadyPresent
    End Sub

    Private Sub IntegerOnly_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQuantity.KeyPress, txtNewStockCount.KeyPress
        e.Handled = Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar)
    End Sub

    ' ========================================================================
    '  UI CONFIGURATION (Unchanged - Handles hiding/showing boxes)
    ' ========================================================================
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
        SetVisible(False, lblStatus, cboStatus, lblShippingFee, txtShippingFee, lblShippingDate, dtpShippingDate, cboCourier, lblCourier, lblDownPayment, txtDownPayment, lblQuantity, txtQuantity, lblSalesLocation, cboSalesLocation, lblDelivery)
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
        SetVisible(True, lblDownPayment, txtDownPayment, lblQuantity, txtQuantity, lblStatus, cboStatus, lblSalesLocation, cboSalesLocation)

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

    Private Sub LoadTransactionDropdowns()
        cboTarget.Items.Clear()
        cboProduct.DataSource = Nothing
        cboProduct.Items.Clear()
        cboCourier.Items.Clear()
        cboSeries.Items.Clear()
        If tag1 <> TX_SALES Then cboSalesLocation.Items.Clear()

        Try
            Select Case tag1
                Case TX_RESTOCK
                    LoadComboFromList(cboTarget, ctrl.GetSuppliers())
                    LoadProductDropdown()
                    LoadComboFromList(cboSeries, ctrl.GetSeries())
                Case TX_SALES
                    LoadComboFromList(cboTarget, ctrl.GetCustomers())
                    LoadProductDropdown()
                    LoadComboFromList(cboSeries, ctrl.GetSeries())
                    LoadComboFromList(cboCourier, ctrl.GetCouriers())
                Case TX_STORE_ASSIGN
                    LoadComboFromList(cboTarget, ctrl.GetBranches())
                    LoadProductDropdown()
                    LoadComboFromList(cboSeries, ctrl.GetSeries())

                Case TX_WORK_SCHEDULE
                    LoadComboFromList(cboTarget, ctrl.GetEmployees())
                    LoadComboFromList(cboProduct, ctrl.GetBranches())
                    LoadComboFromList(cboSeries, ctrl.GetSeries())
                Case TX_INTERNAL_TRANSFER
                    LoadComboFromList(cboSeries, ctrl.GetSeries())
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

    Private Sub LoadComboFromList(combo As ComboBox, items As List(Of String))
        For Each item In items : combo.Items.Add(item) : Next
    End Sub

    Private Sub LoadProductDropdown()
        Dim dt = ctrl.GetAllProducts()
        cboProduct.DataSource = dt
        cboProduct.DisplayMember = "DisplayName"
        cboProduct.ValueMember = "product_id"
        cboProduct.SelectedIndex = -1
    End Sub

    ' ========================================================================
    '  DYNAMIC UI EVENTS
    ' ========================================================================
    Private Sub cboSalesLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSalesLocation.SelectedIndexChanged
        Dim isRelevant = (tag1 = TX_SALES OrElse tag1 = TX_INTERNAL_TRANSFER)
        If Not isRelevant OrElse cboSalesLocation.Text.Trim() = "" Then Return

        Try
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

    Private Sub cboProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProduct.SelectedIndexChanged
        If cboProduct.SelectedIndex = -1 Then
            HideProductPriceAndStock()
            Return
        End If
        If tag1 = TX_WORK_SCHEDULE OrElse TypeOf cboProduct.SelectedValue Is DataRowView Then Return

        Try
            Dim productId = Convert.ToInt32(cboProduct.SelectedValue)
            Dim isBranchSelected = (tag1 = TX_SALES OrElse tag1 = TX_INTERNAL_TRANSFER) AndAlso cboSalesLocation.Text <> LOCATION_MAIN_WAREHOUSE AndAlso cboSalesLocation.Text <> ""
            Dim location = If(isBranchSelected, cboSalesLocation.Text.Trim(), LOCATION_MAIN_WAREHOUSE)

            Dim detail = ctrl.GetProductDetail(productId, location)
            lblNewStockCount.Text = detail.StockLabel
            txtNewBuyPrice.Text = detail.Price.ToString()
            txtNewStockCount.Text = detail.Stock.ToString()
            SetVisible(True, lblNewBuyPrice, txtNewBuyPrice, lblNewStockCount, txtNewStockCount)
        Catch ex As Exception
            ShowError("Error fetching product details: " & ex.Message)
        End Try
    End Sub

    Private Sub cboDeliveryType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDeliveryType.SelectedIndexChanged
        dtpShippingDate.Format = DateTimePickerFormat.Long
        dtpShippingDate.ShowUpDown = False
        Dim isCourier = (cboDeliveryType.Text = DELIVERY_TYPE_COURIER)
        SetVisible(isCourier, lblCourier, cboCourier, lblShippingFee, txtShippingFee, lblShippingDate, dtpShippingDate)
    End Sub

    Private Sub chkNewTarget_CheckedChanged(sender As Object, e As EventArgs) Handles chkNewTarget.CheckedChanged
        Dim isNew = chkNewTarget.Checked
        cboTarget.Visible = Not isNew
        SetVisible(isNew, lblNewCompanyName, txtNewCompanyName, lblNewContactPerson, txtNewContactPerson)
        Dim showThirdField = isNew AndAlso tag1 <> TX_SALES
        lblNewCountryOrigin.Visible = showThirdField
        txtNewCountryOrigin.Visible = showThirdField
        If Not isNew Then ClearNewTargetInputs()
    End Sub

    Private Sub chkNewProduct_CheckedChanged(sender As Object, e As EventArgs) Handles chkNewProduct.CheckedChanged
        Dim isNew = chkNewProduct.Checked
        cboProduct.Visible = Not isNew
        SetVisible(isNew, lblNewItemName, txtNewItemName)
        If isNew Then ShowNewProductFields() Else HideNewProductFields()
    End Sub

    Private Sub HideNewProductFields()
        lblNewBuyPrice.Text = "Current Price:" : txtNewBuyPrice.ReadOnly = True
        lblNewStockCount.Text = "Current Stock:" : txtNewStockCount.ReadOnly = True
        HideProductPriceAndStock()

        SetVisible(False, lblNewSellPrice, txtNewSellPrice, lblNewColor, txtNewColor, lblNewSize, txtNewSize, lblNewStatus, cboNewStatus, lblSeries, cboSeries)
    End Sub
    Private Sub ShowNewProductFields()
        lblNewBuyPrice.Text = "Buying Price:" : txtNewBuyPrice.ReadOnly = False
        lblNewStockCount.Text = "Initial Stock:" : txtNewStockCount.ReadOnly = False
        SetVisible(True, lblNewBuyPrice, txtNewBuyPrice, lblNewStockCount, txtNewStockCount, lblNewSellPrice, txtNewSellPrice)
        Dim isWorkSchedule = (tag1 = TX_WORK_SCHEDULE)

        SetVisible(Not isWorkSchedule, lblNewColor, txtNewColor, lblNewSize, txtNewSize, lblNewStatus, cboNewStatus, lblSeries, cboSeries)

        ClearNewProductInputs()
    End Sub

    ' ========================================================================
    '  THE MASTER PROCESSOR (Now relies heavily on the Service!)
    ' ========================================================================
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
            ShowError("System Error: " & ex.Message)
        End Try
    End Sub

    Private Function IsFormReadyToProcess() As Boolean
        If Not chkNewTarget.Checked AndAlso cboTarget.Text = "" AndAlso tag1 <> TX_INTERNAL_TRANSFER Then
            ShowWarning("Please select a target entity to proceed.", "Validation Error")
            Return False
        End If
        If Not chkNewProduct.Checked AndAlso cboProduct.Text = "" AndAlso tag1 <> TX_WORK_SCHEDULE AndAlso tag1 <> TX_INTERNAL_TRANSFER Then
            ShowWarning("Please select a secondary entity to proceed.", "Validation Error")
            Return False
        End If
        Return True
    End Function

    Private Sub OnTransactionSuccess()
        If tag1 <> TX_SALES Then MsgBox("Transaction processed successfully!", MsgBoxStyle.Information)
        txtQuantity.Clear()
        If txtDownPayment.Visible Then txtDownPayment.Clear()
        If txtShippingFee.Visible Then txtShippingFee.Clear()
        cboProduct.SelectedIndex = -1
        chkNewTarget.Checked = False
        chkNewProduct.Checked = False
    End Sub

    Private Function GetProductInfoFromUI() As TransactionService.ProductInfo
        Return New TransactionService.ProductInfo() With {
            .IsNew = chkNewProduct.Checked,
            .Id = If(chkNewProduct.Checked, -1, Convert.ToInt32(cboProduct.SelectedValue)),
            .Name = If(chkNewProduct.Checked, txtNewItemName.Text.Trim(), cboProduct.Text.Trim()),
            .BuyPrice = ParseDecimal(txtNewBuyPrice.Text),
            .SellPrice = ParseDecimal(txtNewSellPrice.Text),
            .Color = txtNewColor.Text.Trim(),
            .Size = txtNewSize.Text.Trim(),
            .Status = cboNewStatus.Text.Trim(),
            .InitialStock = CInt(Val(txtNewStockCount.Text)),
            .Series = cboSeries.Text.Trim()
        }
    End Function
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Function ProcessRestocking() As Boolean
        Dim req As New TransactionService.RestockRequest() With {
            .Quantity = CInt(Val(txtQuantity.Text)),
            .SupplyDate = FormatDate(dtpReservationDate),
            .Product = GetProductInfoFromUI(),
            .Supplier = New TransactionService.SupplierInfo() With {
                .IsNew = chkNewTarget.Checked,
                .Name = If(chkNewTarget.Checked, txtNewCompanyName.Text.Trim(), cboTarget.Text.Trim()),
                .ContactPerson = txtNewContactPerson.Text.Trim(),
                .CountryOrigin = txtNewCountryOrigin.Text.Trim()
            }
        }

        Dim res = service.ProcessRestock(req)

        If Not res.Success Then

            If res.Message = "DUPLICATE_FOUND" Then

                Dim prompt As String = "Wait! This exact product already exists in the database." & vbCrLf & "Do you want to just add this stock to the existing inventory instead?"
                Dim answer = MsgBox(prompt, MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Duplicate Found")

                If answer = MsgBoxResult.Yes Then
                    Dim existingId As Integer = Convert.ToInt32(res.Receipt)


                    Dim mergeRes = service.MergeDuplicateStock(existingId, req.Quantity)

                    If mergeRes.Success Then
                        MsgBox(mergeRes.Message, MsgBoxStyle.Information, "Stock Merged")
                        Return True
                    Else
                        ShowWarning(mergeRes.Message, "Merge Error")
                        Return False
                    End If
                Else

                    Return False
                End If
            Else

                ShowWarning(res.Message, "Restock Error")
                Return False
            End If
        End If

        Return True
    End Function

    Private Function ProcessSales() As Boolean
        Dim req As New TransactionService.SaleRequest() With {
            .Quantity = CInt(Val(txtQuantity.Text)),
            .SalesLocation = cboSalesLocation.Text.Trim(),
            .ReservationDate = FormatDate(dtpReservationDate),
            .OrderStatus = If(cboStatus.Text.Trim() = "", STATUS_PENDING, cboStatus.Text.Trim()),
            .DownPayment = ParseDecimal(txtDownPayment.Text),
            .IsCourier = (cboDeliveryType.Text = DELIVERY_TYPE_COURIER),
            .CourierName = cboCourier.Text.Trim(),
            .ShippingFee = ParseDecimal(txtShippingFee.Text),
            .ShippingDate = If(cboDeliveryType.Text = DELIVERY_TYPE_COURIER, $"'{dtpShippingDate.Value:yyyy-MM-dd}'", "NULL"),
            .Product = GetProductInfoFromUI(),
            .Customer = New TransactionService.CustomerInfo() With {
                .IsNew = chkNewTarget.Checked,
                .Name = If(chkNewTarget.Checked, txtNewCompanyName.Text.Trim(), cboTarget.Text.Trim()),
                .Address = txtNewContactPerson.Text.Trim()
            }
        }

        Dim res = service.ProcessSale(req)
        If res.Success Then
            MsgBox(res.Receipt, MsgBoxStyle.Information, "Transaction Processed")
            CheckAndShowLowStockAlert(req.Product.Id, req.SalesLocation)
            Return True
        Else
            ShowWarning(res.Message, "Sales Error")
            Return False
        End If
    End Function

    Private Function ProcessStoreAssignment() As Boolean
        Dim req As New TransactionService.StoreAssignRequest() With {
            .Quantity = CInt(Val(txtQuantity.Text)),
            .RestockDate = FormatDate(dtpReservationDate),
            .Product = GetProductInfoFromUI(),
            .Branch = New TransactionService.BranchInfo() With {
                .IsNew = chkNewTarget.Checked,
                .Name = If(chkNewTarget.Checked, txtNewCompanyName.Text.Trim(), cboTarget.Text.Trim()),
                .Address = txtNewContactPerson.Text.Trim(),
                .OperatingHours = txtNewCountryOrigin.Text.Trim()
            }
        }

        Dim res = service.ProcessStoreAssignment(req)
        If Not res.Success Then ShowWarning(res.Message, "Store Assignment Error")
        Return res.Success
    End Function

    Private Function ProcessWorkSchedule() As Boolean
        Dim req As New TransactionService.WorkScheduleRequest() With {
            .ScheduledDate = FormatDate(dtpReservationDate),
            .StartTime = dtpStartTime.Value.ToString("HH:mm:ss"),
            .EndTime = dtpShippingDate.Value.ToString("HH:mm:ss"),
            .Employee = New TransactionService.EmployeeInfo() With {
                .IsNew = chkNewTarget.Checked,
                .Name = If(chkNewTarget.Checked, txtNewCompanyName.Text.Trim(), cboTarget.Text.Trim()),
                .Role = txtNewContactPerson.Text.Trim(),
                .Email = txtNewCountryOrigin.Text.Trim()
            },
            .Branch = New TransactionService.BranchInfo() With {
                .IsNew = chkNewProduct.Checked,
                .Name = If(chkNewProduct.Checked, txtNewItemName.Text.Trim(), cboProduct.Text.Trim()),
                .Address = txtNewBuyPrice.Text.Trim(),
                .OperatingHours = txtNewSellPrice.Text.Trim()
            }
        }

        Dim res = service.ProcessWorkSchedule(req)
        If Not res.Success Then ShowWarning(res.Message, "Work Schedule Error")
        Return res.Success
    End Function

    Private Function ProcessInternalTransfer() As Boolean
        Dim req As New TransactionService.InternalTransferRequest() With {
            .ProductId = Convert.ToInt32(cboProduct.SelectedValue),
            .Quantity = CInt(Val(txtQuantity.Text)),
            .SourceLocation = cboSalesLocation.Text.Trim(),
            .DestLocation = cboTarget.Text.Trim(),
            .TransferDate = FormatDate(dtpReservationDate)
        }

        Dim res = service.ProcessInternalTransfer(req)
        If res.Success Then
            CheckAndShowLowStockAlert(req.ProductId, req.SourceLocation)
            Return True
        Else
            ShowWarning(res.Message, "Transfer Error")
            Return False
        End If
    End Function
    Private Sub CheckAndShowLowStockAlert(productId As Integer, location As String)
        Try
            ' We safely use the Controller just to check the UI popup condition
            Dim ls = ctrl.CheckLowStock(productId, location.Replace("'", "''"))
            If ls.IsLow Then
                Dim locationLabel = If(location = LOCATION_MAIN_WAREHOUSE, "Main Warehouse", location)
                ShowWarning($"⚠️ CRITICAL: '{ls.ItemName}' at {locationLabel} has dropped to {ls.CurrentStock} units!" &
                            vbCrLf & "Please arrange a restock immediately.", "Automated Low Stock Warning")
            End If
        Catch
        End Try
    End Sub

    Private Function FormatDate(dtp As DateTimePicker) As String
        Return dtp.Value.ToString("yyyy-MM-dd")
    End Function

    Private Function ParseDecimal(raw As String) As Double
        Dim result As Double
        Double.TryParse(raw.Trim(), result)
        Return result
    End Function

    Private Sub SetVisible(visible As Boolean, ParamArray controls() As Control)
        For Each ctrlObj As Control In controls
            If ctrlObj IsNot Nothing Then ctrlObj.Visible = visible
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
        cboSeries.SelectedIndex = -1
    End Sub

    Private Sub ShowError(message As String)
        MsgBox(message, MsgBoxStyle.Critical, "Error")
    End Sub

    Private Sub ShowWarning(message As String, title As String)
        MsgBox(message, MsgBoxStyle.Exclamation, title)
    End Sub


End Class