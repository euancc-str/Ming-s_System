Imports System.Text.RegularExpressions

Public Class TransactionService
    Private ReadOnly _repo As TransactionController

    Public Sub New(repo As TransactionController)
        _repo = repo
    End Sub

    Public Class ServiceResult
        Public Success As Boolean
        Public Message As String
        Public Receipt As String
        Public Sub New(s As Boolean, m As String, Optional r As String = "")
            Success = s : Message = m : Receipt = r
        End Sub
    End Class

    ' ========================================================================
    '  THE DATA CONTAINERS (Clean & Grouped)
    ' ========================================================================
    Public Class ProductInfo
        Public IsNew As Boolean
        Public Id As Integer
        Public Name As String
        Public Color As String
        Public Size As String
        Public BuyPrice As Decimal
        Public SellPrice As Decimal
        Public Status As String
        Public InitialStock As Integer
    End Class

    Public Class SupplierInfo
        Public IsNew As Boolean
        Public Name As String
        Public ContactPerson As String
        Public CountryOrigin As String
    End Class

    Public Class CustomerInfo
        Public IsNew As Boolean
        Public Name As String
        Public Address As String
    End Class

    Public Class BranchInfo
        Public IsNew As Boolean
        Public Name As String
        Public Address As String
        Public OperatingHours As String
    End Class

    Public Class EmployeeInfo
        Public IsNew As Boolean
        Public Name As String
        Public Role As String
        Public Email As String
    End Class

    ' ========================================================================
    '  THE SPECIFIC TRANSACTION REQUESTS
    ' ========================================================================
    Public Class RestockRequest
        Public Supplier As SupplierInfo
        Public Product As ProductInfo
        Public Quantity As Integer
        Public SupplyDate As String
    End Class

    Public Class SaleRequest
        Public Customer As CustomerInfo
        Public Product As ProductInfo
        Public Quantity As Integer
        Public SalesLocation As String
        Public ReservationDate As String
        Public OrderStatus As String
        Public DownPayment As Decimal
        Public IsCourier As Boolean
        Public CourierName As String
        Public ShippingFee As Decimal
        Public ShippingDate As String
    End Class

    Public Class StoreAssignRequest
        Public Branch As BranchInfo
        Public Product As ProductInfo
        Public Quantity As Integer
        Public RestockDate As String
    End Class

    Public Class InternalTransferRequest
        Public ProductId As Integer
        Public Quantity As Integer
        Public SourceLocation As String
        Public DestLocation As String
        Public TransferDate As String
    End Class

    Public Class WorkScheduleRequest
        Public Employee As EmployeeInfo
        Public Branch As BranchInfo
        Public ScheduledDate As String
        Public StartTime As String
        Public EndTime As String
    End Class

    ' ========================================================================
    '  THE TRANSACTION ENGINES
    ' ========================================================================

    Public Function ProcessRestock(req As RestockRequest) As ServiceResult
        If req.Quantity <= 0 Then Return New ServiceResult(False, "Invalid quantity.")
        If req.Supplier.IsNew AndAlso req.Supplier.Name = "" Then Return New ServiceResult(False, "Enter Supplier Name.")
        If req.Product.IsNew AndAlso req.Product.SellPrice <= req.Product.BuyPrice Then Return New ServiceResult(False, "Sell price must be > Buy price.")

        Dim suppId = If(req.Supplier.IsNew, _repo.ResolveSupplier(req.Supplier.Name, req.Supplier.ContactPerson, req.Supplier.CountryOrigin), _repo.GetExistingSupplierId(req.Supplier.Name))
        If suppId = -1 Then Return New ServiceResult(False, "Supplier not found.")

        Dim prodId = If(req.Product.IsNew, _repo.ResolveProduct(req.Product.Name, req.Product.Color, req.Product.Size, req.Product.BuyPrice, req.Product.SellPrice, req.Product.Status, req.Product.InitialStock), req.Product.Id)
        If prodId <= 0 Then Return New ServiceResult(False, "Invalid Product.")

        Dim finalBuyPrice = If(req.Product.IsNew, req.Product.BuyPrice, _repo.GetBuyingPrice(prodId))

        Try
            _repo.ProcessRestock(suppId, prodId, req.Quantity, req.SupplyDate, finalBuyPrice)
            Return New ServiceResult(True, "Restock processed successfully!")
        Catch ex As Exception
            Return New ServiceResult(False, "Restock failed: " & ex.Message)
        End Try
    End Function

    Public Function ProcessSale(req As SaleRequest) As ServiceResult
        If req.Quantity <= 0 Then Return New ServiceResult(False, "Invalid quantity.")
        If req.SalesLocation = "" Then Return New ServiceResult(False, "Select Sales Location.")

        Dim grandTotal = (req.Product.SellPrice * req.Quantity) + req.ShippingFee
        If req.DownPayment < 0 OrElse req.DownPayment > grandTotal Then Return New ServiceResult(False, "Invalid payment amount.")
        If req.OrderStatus = "Delivered" AndAlso req.DownPayment < grandTotal Then req.OrderStatus = "Pending"

        If req.IsCourier AndAlso req.CourierName = "" Then Return New ServiceResult(False, "Select Courier.")
        If req.Product.IsNew AndAlso req.Product.SellPrice <= req.Product.BuyPrice Then Return New ServiceResult(False, "Sell price must be > Buy price.")

        Dim custId = If(req.Customer.IsNew, _repo.ResolveCustomer(req.Customer.Name, req.Customer.Address), _repo.GetExistingCustomerId(req.Customer.Name))
        If custId = -1 Then Return New ServiceResult(False, "Customer not found.")

        Dim prodId = If(req.Product.IsNew, _repo.ResolveProduct(req.Product.Name, req.Product.Color, req.Product.Size, req.Product.BuyPrice, req.Product.SellPrice, req.Product.Status, req.Product.InitialStock), req.Product.Id)
        If prodId <= 0 Then Return New ServiceResult(False, "Invalid Product.")

        Dim currentStock = If(req.SalesLocation = "Main Warehouse", _repo.GetWarehouseStock(prodId), _repo.GetBranchStock(prodId, req.SalesLocation))
        If currentStock < req.Quantity Then Return New ServiceResult(False, "Insufficient stock! Available: " & currentStock)

        Try
            Dim dbReq As New TransactionController.SaleRequest() With {
                .CustomerId = custId, .ProductId = prodId, .Quantity = req.Quantity, .ReservationDate = req.ReservationDate,
                .Status = req.OrderStatus, .DownPayment = req.DownPayment, .ShippingFee = req.ShippingFee,
                .ShippingDateSql = req.ShippingDate, .SalesLocation = req.SalesLocation, .IsCourierDelivery = req.IsCourier, .CourierName = req.CourierName
            }
            _repo.ProcessSale(dbReq)
        Catch ex As Exception
            Return New ServiceResult(False, "Sale failed: " & ex.Message)
        End Try

        Dim receipt = $"MING'S CRAFT INVOICE{vbCrLf}Customer: {req.Customer.Name}{vbCrLf}Item: {req.Product.Name}{vbCrLf}Qty: {req.Quantity} @ ₱{req.Product.SellPrice}{vbCrLf}Total: ₱{grandTotal}{vbCrLf}Status: {req.OrderStatus}"
        Return New ServiceResult(True, "Sale successful!", receipt)
    End Function

    Public Function ProcessStoreAssignment(req As StoreAssignRequest) As ServiceResult
        If req.Quantity <= 0 Then Return New ServiceResult(False, "Invalid quantity.")
        If req.Branch.IsNew AndAlso req.Branch.Name = "" Then Return New ServiceResult(False, "Enter Branch Name.")

        Dim branchId = If(req.Branch.IsNew, _repo.ResolveBranch(req.Branch.Name, req.Branch.Address, req.Branch.OperatingHours), _repo.GetExistingBranchId(req.Branch.Name))
        If branchId = -1 Then Return New ServiceResult(False, "Branch not found.")

        Dim prodId = If(req.Product.IsNew, _repo.ResolveProduct(req.Product.Name, req.Product.Color, req.Product.Size, req.Product.BuyPrice, req.Product.SellPrice, req.Product.Status, req.Product.InitialStock), req.Product.Id)
        If prodId <= 0 Then Return New ServiceResult(False, "Invalid Product.")

        Dim warehouseStock = _repo.GetWarehouseStock(prodId)
        If warehouseStock < req.Quantity Then Return New ServiceResult(False, "Not enough warehouse stock. Available: " & warehouseStock)

        Try
            _repo.ProcessStoreAssignment(branchId, prodId, req.Quantity, req.RestockDate)
            Return New ServiceResult(True, "Assigned to branch successfully!")
        Catch ex As Exception
            Return New ServiceResult(False, "Store assignment failed: " & ex.Message)
        End Try
    End Function

    Public Function ProcessWorkSchedule(req As WorkScheduleRequest) As ServiceResult
        If String.Compare(req.StartTime, req.EndTime) >= 0 Then Return New ServiceResult(False, "Start Time must be earlier than End Time.")

        If req.Employee.IsNew Then
            If req.Employee.Name = "" Then Return New ServiceResult(False, "Enter Employee Name.")
            If Not Regex.IsMatch(req.Employee.Email, "^[^@\s]+@[^@\s]+\.[^@\s]+$") Then Return New ServiceResult(False, "Invalid Email Address.")
        End If

        Dim empId = If(req.Employee.IsNew, _repo.ResolveEmployee(req.Employee.Name, req.Employee.Role, req.Employee.Email), _repo.GetExistingEmployeeId(req.Employee.Name))
        Dim branchId = If(req.Branch.IsNew, _repo.ResolveBranch(req.Branch.Name, req.Branch.Address, req.Branch.OperatingHours), _repo.GetExistingBranchId(req.Branch.Name))

        If _repo.HasScheduleConflict(empId, req.ScheduledDate, req.StartTime, req.EndTime) Then Return New ServiceResult(False, "Schedule Conflict for this employee.")

        Try
            _repo.ProcessWorkSchedule(empId, branchId, req.ScheduledDate, req.StartTime, req.EndTime)
            Return New ServiceResult(True, "Work schedule assigned successfully!")
        Catch ex As Exception
            Return New ServiceResult(False, "Work schedule failed: " & ex.Message)
        End Try
    End Function

    Public Function ProcessInternalTransfer(req As InternalTransferRequest) As ServiceResult
        If req.Quantity <= 0 Then Return New ServiceResult(False, "Invalid quantity.")
        If req.SourceLocation = "" OrElse req.DestLocation = "" Then Return New ServiceResult(False, "Select Source and Destination.")
        If req.SourceLocation = req.DestLocation Then Return New ServiceResult(False, "Locations cannot be the same.")

        Dim currentStock = If(req.SourceLocation = "Main Warehouse", _repo.GetWarehouseStock(req.ProductId), _repo.GetBranchStock(req.ProductId, req.SourceLocation))
        If currentStock < req.Quantity Then Return New ServiceResult(False, "Insufficient stock at source! Available: " & currentStock)

        Try
            _repo.ProcessInternalTransfer(req.ProductId, req.Quantity, req.SourceLocation, req.DestLocation, req.TransferDate)
            Return New ServiceResult(True, "Transfer successful!")
        Catch ex As Exception
            Return New ServiceResult(False, "Transfer failed: " & ex.Message)
        End Try
    End Function

End Class