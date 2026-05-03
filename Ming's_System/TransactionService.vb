Imports System.Text.RegularExpressions

''' <summary>
''' Business logic layer. Validates inputs, enforces rules, and orchestrates
''' calls to TransactionController (the data/repository layer).
''' The form only talks to this class — never to the controller directly.
''' </summary>
Public Class TransactionService

    ' ── DEPENDENCY ──────────────────────────────────────────────────────────
    Private ReadOnly _repo As TransactionController

    Public Sub New(repo As TransactionController)
        _repo = repo
    End Sub

    ' ========================================================================
    '  SECTION 1 — SERVICE RESULT
    '  Every public method returns one of these so the form never needs to
    '  know WHY something failed — it just reads .Success and .Message.
    ' ========================================================================

    Public Class ServiceResult
        Public ReadOnly Success As Boolean
        Public ReadOnly Message As String
        Public ReadOnly Receipt As String      ' populated only for Sales
        Public ReadOnly IsLowStock As Boolean
        Public ReadOnly LowStockMessage As String

        Private Sub New(ok As Boolean, msg As String,
                        Optional receipt As String = Nothing,
                        Optional isLow As Boolean = False,
                        Optional lowMsg As String = Nothing)
            Me.Success = ok
            Me.Message = msg
            Me.Receipt = receipt
            Me.IsLowStock = isLow
            Me.LowStockMessage = lowMsg
        End Sub

        Public Shared Function Ok(Optional receipt As String = Nothing,
                                  Optional isLow As Boolean = False,
                                  Optional lowMsg As String = Nothing) As ServiceResult
            Return New ServiceResult(True, Nothing, receipt, isLow, lowMsg)
        End Function

        Public Shared Function Fail(message As String) As ServiceResult
            Return New ServiceResult(False, message)
        End Function
    End Class


    ' ========================================================================
    '  SECTION 2 — REQUEST OBJECTS
    '  Plain data bags; no logic. The form fills these and passes them in.
    ' ========================================================================

    Public Class RestockRequest
        Public Property SupplierName As String
        Public Property IsNewSupplier As Boolean
        Public Property ContactPerson As String
        Public Property CountryOrigin As String

        Public Property ProductId As Integer   ' -1 when IsNewProduct = True
        Public Property IsNewProduct As Boolean
        Public Property ItemName As String
        Public Property Color As String
        Public Property Size As String
        Public Property BuyingPrice As Decimal
        Public Property SellingPrice As Decimal
        Public Property ProductStatus As String
        Public Property InitialStock As Integer

        Public Property Quantity As Integer
        Public Property SupplyDate As String    ' "yyyy-MM-dd"
    End Class

    Public Class SaleRequest
        Public Property CustomerName As String
        Public Property IsNewCustomer As Boolean
        Public Property CustomerAddress As String

        Public Property ProductId As Integer
        Public Property IsNewProduct As Boolean
        Public Property ItemName As String
        Public Property Color As String
        Public Property Size As String
        Public Property BuyingPrice As Decimal
        Public Property SellingPrice As Decimal
        Public Property ProductStatus As String
        Public Property InitialStock As Integer

        Public Property Quantity As Integer
        Public Property SalesLocation As String
        Public Property ReservationDate As String
        Public Property Status As String
        Public Property DownPayment As Decimal
        Public Property IsCourierDelivery As Boolean
        Public Property CourierName As String
        Public Property ShippingFee As Decimal
        Public Property ShippingDate As String    ' "NULL" or "'yyyy-MM-dd'"

        ' Display name shown on receipt (pre-resolved by the form)
        Public Property ProductDisplayName As String
    End Class

    Public Class StoreAssignRequest
        Public Property BranchName As String
        Public Property IsNewBranch As Boolean
        Public Property BranchAddress As String
        Public Property OperatingHours As String

        Public Property ProductId As Integer
        Public Property IsNewProduct As Boolean
        Public Property ItemName As String
        Public Property Color As String
        Public Property Size As String
        Public Property BuyingPrice As Decimal
        Public Property SellingPrice As Decimal
        Public Property ProductStatus As String
        Public Property InitialStock As Integer

        Public Property Quantity As Integer
        Public Property RestockDate As String
    End Class

    Public Class WorkScheduleRequest
        Public Property EmployeeName As String
        Public Property IsNewEmployee As Boolean
        Public Property Role As String
        Public Property Email As String

        Public Property BranchName As String
        Public Property IsNewBranch As Boolean
        Public Property BranchAddress As String
        Public Property OperatingHours As String

        Public Property ScheduledDate As String
        Public Property StartTime As String    ' "HH:mm:ss"
        Public Property EndTime As String
    End Class

    Public Class TransferRequest
        Public Property ProductId As Integer
        Public Property Quantity As Integer
        Public Property SourceLocation As String
        Public Property DestLocation As String
        Public Property TransferDate As String
    End Class


    ' ========================================================================
    '  SECTION 3 — CONSTANTS (single source of truth for all rules)
    ' ========================================================================

    Private Const LOCATION_MAIN_WAREHOUSE As String = "Main Warehouse"
    Private Const STATUS_PENDING As String = "Pending"
    Private Const STATUS_DELIVERED As String = "Delivered"
    Private Const LOW_STOCK_THRESHOLD As Integer = 5
    Private Const EMAIL_REGEX As String = "^[^@\s]+@[^@\s]+\.[^@\s]+$"


    ' ========================================================================
    '  SECTION 4 — RESTOCK
    ' ========================================================================

    Public Function ProcessRestock(req As RestockRequest) As ServiceResult
        ' ── 1. Validate quantity ────────────────────────────────────────────
        Dim qtyResult = ValidateQuantity(req.Quantity)
        If Not qtyResult.Success Then Return qtyResult

        ' ── 2. Validate / resolve supplier ──────────────────────────────────
        If req.IsNewSupplier AndAlso String.IsNullOrWhiteSpace(req.SupplierName) Then
            Return ServiceResult.Fail("Please enter Supplier details.")
        End If

        Dim supplierId As Integer
        If req.IsNewSupplier Then
            supplierId = _repo.ResolveSupplier(req.SupplierName, req.ContactPerson, req.CountryOrigin)
        Else
            supplierId = GetExistingSupplierId(req.SupplierName)
            If supplierId = -1 Then Return ServiceResult.Fail("Selected supplier not found.")
        End If

        ' ── 3. Validate / resolve product ───────────────────────────────────
        Dim productResult = ValidateAndResolveProduct(req.IsNewProduct, req.ProductId,
                                                      req.ItemName, req.Color, req.Size,
                                                      req.BuyingPrice, req.SellingPrice,
                                                      req.ProductStatus, req.InitialStock)
        If Not productResult.Success Then Return productResult
        Dim productId = productResult.ResolvedId

        ' ── 4. Determine buying price ────────────────────────────────────────
        Dim buyingPrice = If(req.IsNewProduct, req.BuyingPrice,
                             _repo.GetBuyingPrice(productId))

        ' ── 5. Execute via repository ────────────────────────────────────────
        Try
            _repo.ProcessRestock(supplierId, productId, req.Quantity, req.SupplyDate, buyingPrice)
            Return ServiceResult.Ok()
        Catch ex As Exception
            Return ServiceResult.Fail("Restock failed: " & ex.Message)
        End Try
    End Function


    ' ========================================================================
    '  SECTION 5 — SALES
    ' ========================================================================

    Public Function ProcessSale(req As SaleRequest) As ServiceResult
        ' ── 1. Basic field checks ────────────────────────────────────────────
        If String.IsNullOrWhiteSpace(req.SalesLocation) Then
            Return ServiceResult.Fail("Select Sales Location.")
        End If

        Dim qtyResult = ValidateQuantity(req.Quantity)
        If Not qtyResult.Success Then Return qtyResult

        ' ── 2. Courier delivery rules ────────────────────────────────────────
        If req.IsCourierDelivery Then
            Dim courierResult = ValidateCourierDelivery(req)
            If Not courierResult.Success Then Return courierResult
        End If

        ' ── 3. Payment rules ─────────────────────────────────────────────────
        Dim unitPrice = If(req.IsNewProduct, req.SellingPrice,
                            CDec(req.SellingPrice))       ' form already resolved this
        Dim grandTotal = (unitPrice * req.Quantity) + req.ShippingFee

        Dim paymentResult = ValidatePayment(req.DownPayment, grandTotal, req.Status)
        If Not paymentResult.Success Then Return paymentResult
        ' Status may have been corrected inside ValidatePayment
        req.Status = paymentResult.CorrectedStatus

        ' ── 4. Resolve customer ───────────────────────────────────────────────
        If req.IsNewCustomer AndAlso String.IsNullOrWhiteSpace(req.CustomerName) Then
            Return ServiceResult.Fail("Please enter Customer details.")
        End If
        Dim customerId = If(req.IsNewCustomer,
                            _repo.ResolveCustomer(req.CustomerName, req.CustomerAddress),
                            GetExistingCustomerId(req.CustomerName))
        If customerId = -1 Then Return ServiceResult.Fail("Selected customer not found.")

        ' ── 5. Resolve product ───────────────────────────────────────────────
        Dim productResult = ValidateAndResolveProduct(req.IsNewProduct, req.ProductId,
                                                      req.ItemName, req.Color, req.Size,
                                                      req.BuyingPrice, req.SellingPrice,
                                                      req.ProductStatus, req.InitialStock)
        If Not productResult.Success Then Return productResult
        Dim productId = productResult.ResolvedId

        ' ── 6. Stock check ───────────────────────────────────────────────────
        Dim stockResult = ValidateSaleStock(productId, req.Quantity, req.SalesLocation)
        If Not stockResult.Success Then Return stockResult

        ' ── 7. Execute via repository ─────────────────────────────────────────
        Try
            Dim dbReq As New TransactionController.SaleRequest() With {
                .CustomerId = customerId,
                .ProductId = productId,
                .Quantity = req.Quantity,
                .ReservationDate = req.ReservationDate,
                .Status = req.Status,
                .DownPayment = req.DownPayment,
                .ShippingFee = req.ShippingFee,
                .ShippingDateSql = req.ShippingDate,
                .SalesLocation = req.SalesLocation,
                .IsCourierDelivery = req.IsCourierDelivery,
                .CourierName = req.CourierName
            }
            _repo.ProcessSale(dbReq)
        Catch ex As Exception
            Return ServiceResult.Fail("Sale failed: " & ex.Message)
        End Try

        ' ── 8. Build receipt ─────────────────────────────────────────────────
        Dim receipt = BuildReceiptText(req.CustomerName, req.SalesLocation,
                                       req.ProductDisplayName, req.Quantity,
                                       unitPrice, req.ShippingFee,
                                       grandTotal, req.DownPayment, req.Status)

        ' ── 9. Low-stock check (non-blocking) ────────────────────────────────
        Dim lowResult = CheckLowStock(productId, req.SalesLocation)

        Return ServiceResult.Ok(receipt, lowResult.IsLow, lowResult.Message)
    End Function


    ' ========================================================================
    '  SECTION 6 — STORE ASSIGNMENT
    ' ========================================================================

    Public Function ProcessStoreAssignment(req As StoreAssignRequest) As ServiceResult
        Dim qtyResult = ValidateQuantity(req.Quantity)
        If Not qtyResult.Success Then Return qtyResult

        ' ── Resolve branch ────────────────────────────────────────────────────
        If req.IsNewBranch AndAlso String.IsNullOrWhiteSpace(req.BranchName) Then
            Return ServiceResult.Fail("Please enter Branch details.")
        End If
        Dim branchId = If(req.IsNewBranch,
                          _repo.ResolveBranch(req.BranchName, req.BranchAddress, req.OperatingHours),
                          GetExistingBranchId(req.BranchName))
        If branchId = -1 Then Return ServiceResult.Fail("Selected branch not found.")

        ' ── Resolve product ───────────────────────────────────────────────────
        Dim productResult = ValidateAndResolveProduct(req.IsNewProduct, req.ProductId,
                                                      req.ItemName, req.Color, req.Size,
                                                      req.BuyingPrice, req.SellingPrice,
                                                      req.ProductStatus, req.InitialStock)
        If Not productResult.Success Then Return productResult
        Dim productId = productResult.ResolvedId

        ' ── Warehouse stock check ─────────────────────────────────────────────
        Dim warehouseStock = _repo.GetWarehouseStock(productId)
        If warehouseStock < req.Quantity Then
            Return ServiceResult.Fail(
                $"Not enough stock in main inventory. Available: {warehouseStock}, Requested: {req.Quantity}.")
        End If

        ' ── Execute ───────────────────────────────────────────────────────────
        Try
            _repo.ProcessStoreAssignment(branchId, productId, req.Quantity, req.RestockDate)
            Return ServiceResult.Ok()
        Catch ex As Exception
            Return ServiceResult.Fail("Store assignment failed: " & ex.Message)
        End Try
    End Function


    ' ========================================================================
    '  SECTION 7 — WORK SCHEDULE
    ' ========================================================================

    Public Function ProcessWorkSchedule(req As WorkScheduleRequest) As ServiceResult
        ' ── Time rule ─────────────────────────────────────────────────────────
        If String.Compare(req.StartTime, req.EndTime) >= 0 Then
            Return ServiceResult.Fail("Start Time must be earlier than End Time.")
        End If

        ' ── Email rule (only for new employees) ───────────────────────────────
        If req.IsNewEmployee Then
            If String.IsNullOrWhiteSpace(req.EmployeeName) Then
                Return ServiceResult.Fail("Please enter Employee details.")
            End If
            If Not Regex.IsMatch(req.Email, EMAIL_REGEX) Then
                Return ServiceResult.Fail("Please enter a valid Email Address (e.g., user@domain.com).")
            End If
        End If

        ' ── Resolve employee ──────────────────────────────────────────────────
        Dim employeeId = If(req.IsNewEmployee,
                            _repo.ResolveEmployee(req.EmployeeName, req.Role, req.Email),
                            GetExistingEmployeeId(req.EmployeeName))
        If employeeId = -1 Then Return ServiceResult.Fail("Selected employee not found.")

        ' ── Schedule conflict check ───────────────────────────────────────────
        If _repo.HasScheduleConflict(employeeId, req.ScheduledDate, req.StartTime, req.EndTime) Then
            Return ServiceResult.Fail(
                $"Schedule Conflict! {req.EmployeeName} already has an overlapping shift on {req.ScheduledDate}.")
        End If

        ' ── Resolve branch ────────────────────────────────────────────────────
        If req.IsNewBranch AndAlso String.IsNullOrWhiteSpace(req.BranchName) Then
            Return ServiceResult.Fail("Please enter Branch details.")
        End If
        Dim branchId = If(req.IsNewBranch,
                          _repo.ResolveBranch(req.BranchName, req.BranchAddress, req.OperatingHours),
                          GetExistingBranchId(req.BranchName))
        If branchId = -1 Then Return ServiceResult.Fail("Selected branch not found.")

        ' ── Execute ───────────────────────────────────────────────────────────
        Try
            _repo.ProcessWorkSchedule(employeeId, branchId,
                                      req.ScheduledDate, req.StartTime, req.EndTime)
            Return ServiceResult.Ok()
        Catch ex As Exception
            Return ServiceResult.Fail("Work schedule failed: " & ex.Message)
        End Try
    End Function


    ' ========================================================================
    '  SECTION 8 — INTERNAL TRANSFER
    ' ========================================================================

    Public Function ProcessInternalTransfer(req As TransferRequest) As ServiceResult
        ' ── Location rules ────────────────────────────────────────────────────
        If String.IsNullOrWhiteSpace(req.SourceLocation) OrElse
           String.IsNullOrWhiteSpace(req.DestLocation) Then
            Return ServiceResult.Fail("Please select Source and Destination.")
        End If
        If req.SourceLocation = req.DestLocation Then
            Return ServiceResult.Fail("Source and Destination cannot be the same.")
        End If

        Dim qtyResult = ValidateQuantity(req.Quantity)
        If Not qtyResult.Success Then Return qtyResult

        ' ── Stock check on source ─────────────────────────────────────────────
        Dim stockResult = ValidateTransferStock(req.ProductId, req.Quantity, req.SourceLocation)
        If Not stockResult.Success Then Return stockResult

        ' ── Execute ───────────────────────────────────────────────────────────
        Try
            _repo.ProcessInternalTransfer(req.ProductId, req.Quantity,
                                          req.SourceLocation, req.DestLocation,
                                          req.TransferDate)
        Catch ex As Exception
            Return ServiceResult.Fail("Transfer failed: " & ex.Message)
        End Try

        ' ── Low-stock check on source (non-blocking) ──────────────────────────
        Dim lowResult = CheckLowStock(req.ProductId, req.SourceLocation)
        Return ServiceResult.Ok(Nothing, lowResult.IsLow, lowResult.Message)
    End Function


    ' ========================================================================
    '  SECTION 9 — PRIVATE VALIDATORS
    '  Each answers exactly one question and returns a result.
    ' ========================================================================

    ''' <summary>Quantity must be a positive integer.</summary>
    Private Function ValidateQuantity(qty As Integer) As ServiceResult
        If qty > 0 Then Return ServiceResult.Ok()
        Return ServiceResult.Fail("Please enter a valid positive quantity.")
    End Function

    ''' <summary>Courier-specific fields when delivery type is Courier.</summary>
    Private Function ValidateCourierDelivery(req As SaleRequest) As ServiceResult
        If String.IsNullOrWhiteSpace(req.CourierName) Then
            Return ServiceResult.Fail("Select Courier.")
        End If
        ' ShippingDate is stored as "'yyyy-MM-dd'" or "NULL" — parse to compare
        If req.ShippingDate <> "NULL" Then
            Dim shippingDateStr = req.ShippingDate.Trim("'"c)
            Dim shippingDate As Date
            If Date.TryParse(shippingDateStr, shippingDate) Then
                Dim reservationDate As Date
                If Date.TryParse(req.ReservationDate, reservationDate) Then
                    If shippingDate.Date < reservationDate.Date Then
                        Return ServiceResult.Fail(
                            "Shipping Date cannot be earlier than the Reservation Date.")
                    End If
                End If
            End If
        End If
        Return ServiceResult.Ok()
    End Function

    ''' <summary>
    ''' Payment must be >= 0, <= grand total.
    ''' If status = Delivered but balance unpaid, asks the caller to prompt user.
    ''' Returns a PaymentValidationResult which carries the (possibly corrected) status.
    ''' </summary>
    Private Function ValidatePayment(downPayment As Decimal,
                                     grandTotal As Decimal,
                                     status As String) As PaymentValidationResult
        If downPayment < 0 Then
            Return PaymentValidationResult.Fail("Payment cannot be a negative number.")
        End If
        If downPayment > grandTotal Then
            Return PaymentValidationResult.Fail(
                $"Payment (₱{downPayment}) cannot exceed Grand Total ₱{grandTotal}.")
        End If
        If status = STATUS_DELIVERED AndAlso downPayment < grandTotal Then
            ' Signal the form that it needs to ask the user about the mismatch
            Return PaymentValidationResult.StatusMismatch(STATUS_PENDING)
        End If
        Return PaymentValidationResult.Ok(status)
    End Function

    ''' <summary>Stock check for a sale (warehouse or branch).</summary>
    Private Function ValidateSaleStock(productId As Integer,
                                       qty As Integer,
                                       location As String) As ServiceResult
        If location = LOCATION_MAIN_WAREHOUSE Then
            Dim stock = _repo.GetWarehouseStock(productId)
            If stock < qty Then
                Return ServiceResult.Fail(
                    $"Insufficient stock in Warehouse. Available: {stock}, Requested: {qty}.")
            End If
        Else
            Dim stock = _repo.GetBranchStock(productId, location)
            If stock = -1 Then Return ServiceResult.Fail("Branch does not carry this product.")
            If stock < qty Then
                Return ServiceResult.Fail(
                    $"Insufficient stock at Branch. Available: {stock}, Requested: {qty}.")
            End If
        End If
        Return ServiceResult.Ok()
    End Function

    ''' <summary>Stock check for an internal transfer source.</summary>
    Private Function ValidateTransferStock(productId As Integer,
                                           qty As Integer,
                                           sourceLoc As String) As ServiceResult
        If sourceLoc = LOCATION_MAIN_WAREHOUSE Then
            Dim stock = _repo.GetWarehouseStock(productId)
            If stock < qty Then
                Return ServiceResult.Fail(
                    $"Not enough stock in Main Warehouse. Available: {stock}, Requested: {qty}.")
            End If
        Else
            Dim stock = _repo.GetBranchStock(productId, sourceLoc)
            If stock = -1 Then Return ServiceResult.Fail("Source branch does not have this product.")
            If stock < qty Then
                Return ServiceResult.Fail(
                    $"Not enough stock at source branch. Available: {stock}, Requested: {qty}.")
            End If
        End If
        Return ServiceResult.Ok()
    End Function

    ''' <summary>
    ''' Validates new-product fields (price rule) and resolves the product ID.
    ''' Returns a ProductResolutionResult that carries the resolved product_id.
    ''' </summary>
    Private Function ValidateAndResolveProduct(isNew As Boolean,
                                               existingId As Integer,
                                               itemName As String,
                                               color As String,
                                               size As String,
                                               buyPrice As Decimal,
                                               sellPrice As Decimal,
                                               status As String,
                                               initialStock As Integer) As ProductResolutionResult
        If Not isNew Then
            If existingId <= 0 Then
                Return ProductResolutionResult.Fail("Please select a product.")
            End If
            Return ProductResolutionResult.Ok(existingId)
        End If

        ' ── New product rules ─────────────────────────────────────────────────
        If String.IsNullOrWhiteSpace(itemName) Then
            Return ProductResolutionResult.Fail("Please enter Product Name.")
        End If
        If sellPrice <= buyPrice Then
            Return ProductResolutionResult.Fail("Selling price must be higher than buying price.")
        End If

        Dim id = _repo.ResolveProduct(itemName, color, size, buyPrice, sellPrice, status, initialStock)
        Return ProductResolutionResult.Ok(id)
    End Function


    ' ========================================================================
    '  SECTION 10 — PAYMENT VALIDATION RESULT
    '  Separate type because payment validation can produce a "corrected status"
    '  that the caller needs to feed back into the request.
    ' ========================================================================

    Public Class PaymentValidationResult
        Public ReadOnly Success As Boolean
        Public ReadOnly Message As String
        Public ReadOnly CorrectedStatus As String
        ''' <summary>
        ''' True when the order is marked Delivered but payment is incomplete.
        ''' The form must confirm with the user before proceeding.
        ''' </summary>
        Public ReadOnly RequiresUserConfirmation As Boolean

        Private Sub New(ok As Boolean, msg As String,
                        corrected As String,
                        requiresConfirm As Boolean)
            Me.Success = ok
            Me.Message = msg
            Me.CorrectedStatus = corrected
            Me.RequiresUserConfirmation = requiresConfirm
        End Sub

        Public Shared Function Ok(status As String) As PaymentValidationResult
            Return New PaymentValidationResult(True, Nothing, status, False)
        End Function

        Public Shared Function Fail(message As String) As PaymentValidationResult
            Return New PaymentValidationResult(False, message, Nothing, False)
        End Function

        ''' <summary>
        ''' Delivered + unpaid: let the form ask the user whether to downgrade to Pending.
        ''' The caller should show a Yes/No dialog; if Yes, resubmit with correctedStatus.
        ''' </summary>
        Public Shared Function StatusMismatch(correctedStatus As String) As PaymentValidationResult
            Return New PaymentValidationResult(False, Nothing, correctedStatus, True)
        End Function
    End Class


    ' ========================================================================
    '  SECTION 11 — PRODUCT RESOLUTION RESULT
    ' ========================================================================

    Private Class ProductResolutionResult
        Public ReadOnly Success As Boolean
        Public ReadOnly Message As String
        Public ReadOnly ResolvedId As Integer

        Private Sub New(ok As Boolean, msg As String, id As Integer)
            Me.Success = ok
            Me.Message = msg
            Me.ResolvedId = id
        End Sub

        Public Shared Function Ok(id As Integer) As ProductResolutionResult
            Return New ProductResolutionResult(True, Nothing, id)
        End Function

        Public Shared Function Fail(message As String) As ProductResolutionResult
            Return New ProductResolutionResult(False, message, -1)
        End Function
    End Class


    ' ========================================================================
    '  SECTION 12 — LOW STOCK CHECK (non-blocking, returns a message)
    ' ========================================================================

    Public Class LowStockAlert
        Public ReadOnly IsLow As Boolean
        Public ReadOnly Message As String

        Public Sub New(isLow As Boolean, Optional message As String = Nothing)
            Me.IsLow = isLow
            Me.Message = message
        End Sub
    End Class

    Public Function CheckLowStock(productId As Integer, location As String) As LowStockAlert
        Try
            Dim ls = _repo.CheckLowStock(productId, location)
            If Not ls.IsLow Then Return New LowStockAlert(False)

            Dim locationLabel = If(location = LOCATION_MAIN_WAREHOUSE, "Main Warehouse", location)
            Dim msg = $"⚠️ CRITICAL: '{ls.ItemName}' at {locationLabel} has dropped to " &
                      $"{ls.CurrentStock} units!" & vbCrLf &
                      "Please arrange a restock immediately."
            Return New LowStockAlert(True, msg)
        Catch
            ' Silent — alert failure must never block the main process
            Return New LowStockAlert(False)
        End Try
    End Function


    ' ========================================================================
    '  SECTION 13 — RECEIPT BUILDER (business formatting, not UI)
    ' ========================================================================

    Public Function BuildReceiptText(customerName As String,
                                     location As String,
                                     productName As String,
                                     qty As Integer,
                                     unitPrice As Decimal,
                                     shippingFee As Decimal,
                                     grandTotal As Decimal,
                                     paid As Decimal,
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


    ' ========================================================================
    '  SECTION 14 — ID LOOKUP HELPERS
    '  Thin wrappers that ask the repo for an ID by name.
    '  Kept here so callers never construct SQL.
    ' ========================================================================

    Private Function GetExistingSupplierId(name As String) As Integer
        readquery($"SELECT supplier_id FROM supplier WHERE company_name = '{name}'")
        If cmdread.HasRows AndAlso cmdread.Read() Then Return Convert.ToInt32(cmdread("supplier_id"))
        Return -1
    End Function

    Private Function GetExistingCustomerId(name As String) As Integer
        readquery($"SELECT customer_id FROM customer WHERE customer_name = '{name}'")
        If cmdread.HasRows AndAlso cmdread.Read() Then Return Convert.ToInt32(cmdread("customer_id"))
        Return -1
    End Function

    Private Function GetExistingBranchId(name As String) As Integer
        readquery($"SELECT branch_id FROM branch WHERE branch_name = '{name}'")
        If cmdread.HasRows AndAlso cmdread.Read() Then Return Convert.ToInt32(cmdread("branch_id"))
        Return -1
    End Function

    Private Function GetExistingEmployeeId(name As String) As Integer
        readquery($"SELECT employee_id FROM employee WHERE employee_name = '{name}'")
        If cmdread.HasRows AndAlso cmdread.Read() Then Return Convert.ToInt32(cmdread("employee_id"))
        Return -1
    End Function

End Class