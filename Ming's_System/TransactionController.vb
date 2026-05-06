Imports System.Data
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports MySql.Data.MySqlClient

Public Class TransactionController

    ' ========================================================================
    '  BASIC RETRIEVAL HELPERS
    ' ========================================================================
    Public Function GetSuppliers() As List(Of String)
        Return ReadToList("SELECT company_name FROM supplier ORDER BY company_name", "company_name")
    End Function

    Public Function GetCustomers() As List(Of String)
        Return ReadToList("SELECT customer_name FROM customer ORDER BY customer_name", "customer_name")
    End Function

    Public Function GetBranches() As List(Of String)
        Return ReadToList("SELECT branch_name FROM branch ORDER BY branch_name", "branch_name")
    End Function

    Public Function GetEmployees() As List(Of String)
        Return ReadToList("SELECT employee_name FROM employee ORDER BY employee_name", "employee_name")
    End Function

    Public Function GetCouriers() As List(Of String)
        Return ReadToList("SELECT company_name FROM courier ORDER BY company_name", "company_name")
    End Function

    Public Function GetAllProducts() As DataTable
        Return ReadToDataTable(
            "SELECT product_id, " &
            "CONCAT_WS(' | ', item_name, NULLIF(color,''), NULLIF(size,'')) AS DisplayName " &
            "FROM product ORDER BY item_name")
    End Function

    Public Function GetProductsInStock(location As String) As DataTable
        If location = "Main Warehouse" Then
            Return ReadToDataTable(
                "SELECT product_id, " &
                "CONCAT_WS(' | ', item_name, NULLIF(color,''), NULLIF(size,'')) AS DisplayName " &
                "FROM product WHERE COALESCE(stock_count,0) > 0 ORDER BY item_name")
        End If

        Return ReadToDataTable(
            "SELECT p.product_id, " &
            "CONCAT_WS(' | ', p.item_name, NULLIF(p.color,''), NULLIF(p.size,'')) AS DisplayName " &
            "FROM stores s " &
            "INNER JOIN product p ON s.product_id = p.product_id " &
            "INNER JOIN branch b ON s.branch_id = b.branch_id " &
            $"WHERE b.branch_name = '{location}' AND COALESCE(s.quantity,0) > 0 " &
            "ORDER BY p.item_name")
    End Function


    Public Function GetBuyingPrice(productId As Integer) As Decimal
        readquery($"SELECT buying_price FROM product WHERE product_id = {productId}")
        If cmdread.HasRows AndAlso cmdread.Read() Then
            Return Convert.ToDecimal(cmdread("buying_price"))
        End If
        Return 0
    End Function

    Public Function GetExistingSupplierId(name As String) As Integer
        Return GetId($"SELECT supplier_id FROM supplier WHERE company_name = '{name}'", "supplier_id")
    End Function

    Public Function GetExistingCustomerId(name As String) As Integer
        Return GetId($"SELECT customer_id FROM customer WHERE customer_name = '{name}'", "customer_id")
    End Function

    Public Function GetExistingBranchId(name As String) As Integer
        Return GetId($"SELECT branch_id FROM branch WHERE branch_name = '{name}'", "branch_id")
    End Function

    Public Function GetExistingEmployeeId(name As String) As Integer
        Return GetId($"SELECT employee_id FROM employee WHERE employee_name = '{name}'", "employee_id")
    End Function

    Public Function ResolveSupplier(companyName As String, contactPerson As String, countryOrigin As String) As Integer
        readquery($"SELECT supplier_id FROM supplier WHERE company_name = '{companyName}'")
        If Not cmdread.HasRows Then
            readquery($"INSERT INTO supplier (company_name, contact_person, country_origin) VALUES ('{companyName}', '{contactPerson}', '{countryOrigin}')")
        End If
        Return GetId("SELECT supplier_id FROM supplier WHERE company_name = '" & companyName & "'", "supplier_id")
    End Function

    Public Function ResolveCustomer(customerName As String, address As String) As Integer
        readquery($"SELECT customer_id FROM customer WHERE customer_name = '{customerName}'")
        If Not cmdread.HasRows Then
            Dim secureAddress = EncryptData(address)
            readquery($"INSERT INTO customer (customer_name, address) VALUES ('{customerName}', '{secureAddress}')")
        End If
        Return GetId("SELECT customer_id FROM customer WHERE customer_name = '" & customerName & "'", "customer_id")
    End Function

    Public Function ResolveBranch(branchName As String, address As String, operatingHours As String) As Integer
        readquery($"SELECT branch_id FROM branch WHERE branch_name = '{branchName}'")
        If Not cmdread.HasRows Then
            readquery($"INSERT INTO branch (branch_name, address, operating_hours) VALUES ('{branchName}', '{address}', '{operatingHours}')")
        End If
        Return GetId("SELECT branch_id FROM branch WHERE branch_name = '" & branchName & "'", "branch_id")
    End Function

    Public Function ResolveEmployee(employeeName As String, role As String, email As String) As Integer
        readquery($"SELECT employee_id FROM employee WHERE employee_name = '{employeeName}'")
        If Not cmdread.HasRows Then
            readquery($"INSERT INTO employee (employee_name, role, email_address) VALUES ('{employeeName}', '{role}', '{email}')")
        End If
        Return GetId("SELECT employee_id FROM employee WHERE employee_name = '" & employeeName & "'", "employee_id")
    End Function

    Public Function ResolveProduct(itemName As String, color As String, size As String, buyingPrice As Decimal, sellingPrice As Decimal, initialStock As Integer, seriesName As String) As Integer


        Dim seriesIdSql = "NULL"
        If Not String.IsNullOrWhiteSpace(seriesName) AndAlso seriesName <> "None" Then
            seriesIdSql = $"(SELECT series_id FROM series WHERE series_name = '{seriesName}')"
        End If

        readquery($"SELECT product_id FROM product WHERE item_name = '{itemName}' AND color = '{color}' AND size = '{size}'")

        If Not cmdread.HasRows Then
            readquery($"INSERT INTO product (item_name, buying_price, selling_price, color, size, stock_count, series_id) " &
                      $"VALUES ('{itemName}', {buyingPrice}, {sellingPrice}, '{color}', '{size}', {initialStock}, {seriesIdSql})")
        End If

        Return GetId($"SELECT product_id FROM product WHERE item_name='{itemName}' AND color='{color}' AND size='{size}'", "product_id")
    End Function

    Public Structure BatchTransferItem
        Public ProductId As Integer
        Public Quantity As Integer
    End Structure


    Public Function ProcessBatchTransfer(items As List(Of BatchTransferItem), sourceLocation As String, destLocation As String, transferDate As String) As Boolean
        Try
            readquery("START TRANSACTION")

            For Each item In items

                DeductStock(item.ProductId, item.Quantity, sourceLocation)

                If destLocation = "Main Warehouse" Then
                    readquery($"UPDATE product SET stock_count = stock_count + {item.Quantity} WHERE product_id = {item.ProductId}")
                Else
                    readquery(
                        $"INSERT INTO stores (branch_id, product_id, quantity, last_restocked_date) " &
                        $"VALUES ((SELECT branch_id FROM branch WHERE branch_name = '{destLocation}'), " &
                        $"{item.ProductId}, {item.Quantity}, '{transferDate}') " &
                        "ON DUPLICATE KEY UPDATE " &
                        "  quantity = quantity + VALUES(quantity), " &
                        "  last_restocked_date = VALUES(last_restocked_date)")
                End If
            Next

            readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK")
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Structure ProductDetail
        Public Price As Decimal
        Public Stock As Integer
        Public StockLabel As String
    End Structure

    Public Function GetProductDetail(productId As Integer, location As String) As ProductDetail
        Dim result As New ProductDetail()

        If location = "Main Warehouse" OrElse location = "" Then
            result.StockLabel = "Warehouse Stock:"
            readquery($"SELECT selling_price, stock_count FROM product WHERE product_id = {productId}")
        Else
            result.StockLabel = "Branch Stock:"
            readquery(
                $"SELECT p.selling_price, s.quantity AS stock_count " &
                $"FROM product p INNER JOIN stores s ON p.product_id = s.product_id " &
                $"INNER JOIN branch b ON s.branch_id = b.branch_id " &
                $"WHERE p.product_id = {productId} AND b.branch_name = '{location}'")
        End If

        If cmdread.HasRows AndAlso cmdread.Read() Then
            result.Price = Convert.ToDecimal(cmdread("selling_price"))
            result.Stock = Convert.ToInt32(cmdread("stock_count"))
        End If
        Return result
    End Function

    Public Function GetWarehouseStock(productId As Integer) As Integer
        readquery($"SELECT stock_count FROM product WHERE product_id = {productId}")
        If cmdread.HasRows AndAlso cmdread.Read() Then
            Return Convert.ToInt32(cmdread("stock_count"))
        End If
        Return 0
    End Function

    Public Function GetBranchStock(productId As Integer, branchName As String) As Integer
        readquery($"SELECT quantity FROM stores WHERE product_id = {productId} AND branch_id = (SELECT branch_id FROM branch WHERE branch_name = '{branchName}')")
        If cmdread.HasRows AndAlso cmdread.Read() Then
            Return Convert.ToInt32(cmdread("quantity"))
        End If
        Return -1
    End Function

    Private Sub DeductStock(productId As Integer, qty As Integer, location As String)
        If location = "Main Warehouse" Then
            readquery($"UPDATE product SET stock_count = COALESCE(stock_count,0) - {qty} WHERE product_id = {productId}")
        Else
            readquery($"UPDATE stores SET quantity = COALESCE(quantity,0) - {qty} WHERE product_id = {productId} AND branch_id = (SELECT branch_id FROM branch WHERE branch_name = '{location}')")
        End If
    End Sub

    Public Structure LowStockResult
        Public IsLow As Boolean
        Public ItemName As String
        Public CurrentStock As Integer
    End Structure

    Public Function CheckLowStock(productId As Integer, location As String) As LowStockResult
        Dim result As New LowStockResult()
        Dim LOW_STOCK_THRESHOLD As Integer = 5

        If location = "Main Warehouse" Then
            readquery($"SELECT item_name, stock_count AS qty FROM product WHERE product_id = {productId}")
        Else
            readquery(
                $"SELECT p.item_name, s.quantity AS qty FROM stores s " &
                $"INNER JOIN product p ON s.product_id = p.product_id " &
                $"INNER JOIN branch b ON s.branch_id = b.branch_id " &
                $"WHERE s.product_id = {productId} AND b.branch_name = '{location}'")
        End If

        If cmdread.HasRows AndAlso cmdread.Read() Then
            result.ItemName = cmdread("item_name").ToString()
            result.CurrentStock = Convert.ToInt32(cmdread("qty"))
            result.IsLow = (result.CurrentStock <= LOW_STOCK_THRESHOLD)
        End If
        Return result
    End Function

    Public Function HasScheduleConflict(employeeId As Integer, scheduledDate As String, startTime As String, endTime As String) As Boolean
        readquery(
            $"SELECT 1 FROM works_in " &
            $"WHERE employee_id = {employeeId} " &
            $"AND scheduled_date = '{scheduledDate}' " &
            $"AND start_time < '{endTime}' " &
            $"AND end_time > '{startTime}'")
        Return cmdread.HasRows
    End Function


    Public Function ProcessRestock(supplierId As Integer, productId As Integer, quantity As Integer, supplyDate As String, buyingPrice As Decimal) As Boolean
        Try
            readquery("START TRANSACTION")
            readquery(
                $"INSERT INTO provides (supplier_id, product_id, supply_date, supply_price, quantity_supplied) " &
                $"VALUES ({supplierId}, {productId}, '{supplyDate}', {buyingPrice}, {quantity}) " &
                "ON DUPLICATE KEY UPDATE " &
                "  quantity_supplied = quantity_supplied + VALUES(quantity_supplied), " &
                "  supply_date = VALUES(supply_date), " &
                "  supply_price = VALUES(supply_price)")
            readquery($"UPDATE product SET stock_count = COALESCE(stock_count,0) + {quantity} WHERE product_id = {productId}")
            readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK")
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Structure SaleRequest
        Public CustomerId As Integer
        Public ProductId As Integer
        Public Quantity As Integer
        Public ReservationDate As String
        Public Status As String
        Public DownPayment As Decimal
        Public ShippingFee As Decimal
        Public ShippingDateSql As String
        Public SalesLocation As String
        Public IsCourierDelivery As Boolean
        Public CourierName As String
    End Structure

    Public Function ProcessSale(req As SaleRequest) As Boolean
        Try
            readquery("START TRANSACTION")
            readquery(
                $"INSERT INTO purchases " &
                $"  (customer_id, product_id, quantity, reservation_date, status, down_payment, shipping_fee, shipping_date) " &
                $"VALUES ({req.CustomerId}, {req.ProductId}, {req.Quantity}, '{req.ReservationDate}', " &
                $"'{req.Status}', {req.DownPayment}, {req.ShippingFee}, {req.ShippingDateSql})")

            If req.IsCourierDelivery Then
                InsertDeliveryRecord(req.CourierName, req.CustomerId, req.ShippingDateSql, req.ShippingFee)
            End If

            DeductStock(req.ProductId, req.Quantity, req.SalesLocation)
            readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK")
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ProcessStoreAssignment(branchId As Integer, productId As Integer, quantity As Integer, restockDate As String) As Boolean
        Try
            readquery("START TRANSACTION")
            readquery(
                $"INSERT INTO stores (branch_id, product_id, quantity, last_restocked_date) " &
                $"VALUES ({branchId}, {productId}, {quantity}, '{restockDate}') " &
                "ON DUPLICATE KEY UPDATE " &
                "  quantity = COALESCE(quantity,0) + VALUES(quantity), " &
                "  last_restocked_date = VALUES(last_restocked_date)")
            readquery($"UPDATE product SET stock_count = COALESCE(stock_count,0) - {quantity} WHERE product_id = {productId}")
            readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK")
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ProcessWorkSchedule(employeeId As Integer, branchId As Integer, scheduledDate As String, startTime As String, endTime As String) As Boolean
        Try
            readquery($"INSERT INTO works_in (employee_id, branch_id, scheduled_date, start_time, end_time) VALUES ({employeeId}, {branchId}, '{scheduledDate}', '{startTime}', '{endTime}')")
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function ProcessInternalTransfer(productId As Integer, quantity As Integer, sourceLocation As String, destLocation As String, transferDate As String) As Boolean
        Try
            readquery("START TRANSACTION")
            DeductStock(productId, quantity, sourceLocation)

            If destLocation = "Main Warehouse" Then
                readquery($"UPDATE product SET stock_count = stock_count + {quantity} WHERE product_id = {productId}")
            Else
                readquery(
                    $"INSERT INTO stores (branch_id, product_id, quantity, last_restocked_date) " &
                    $"VALUES ((SELECT branch_id FROM branch WHERE branch_name = '{destLocation}'), " &
                    $"{productId}, {quantity}, '{transferDate}') " &
                    "ON DUPLICATE KEY UPDATE " &
                    "  quantity = quantity + VALUES(quantity), " &
                    "  last_restocked_date = VALUES(last_restocked_date)")
            End If

            readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK")
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub InsertDeliveryRecord(courierName As String, customerId As Integer, shippingDateSql As String, shippingFee As Decimal)
        readquery(
            $"INSERT INTO delivers_to (courier_id, customer_id, delivery_date, shipping_fee) " &
            $"VALUES ((SELECT courier_id FROM courier WHERE company_name = '{courierName}'), " &
            $"{customerId}, {shippingDateSql}, {shippingFee}) " &
            "ON DUPLICATE KEY UPDATE " &
            "  delivery_date = VALUES(delivery_date), " &
            "  shipping_fee = VALUES(shipping_fee)")
    End Sub

    Private Function GetId(sql As String, columnName As String) As Integer
        readquery(sql)
        If cmdread.HasRows AndAlso cmdread.Read() Then
            Return Convert.ToInt32(cmdread(columnName))
        End If
        Return -1
    End Function

    Private Function ReadToList(sql As String, columnName As String) As List(Of String)
        Dim list As New List(Of String)
        readquery(sql)
        While cmdread.HasRows AndAlso cmdread.Read()
            list.Add(cmdread(columnName).ToString())
        End While
        Return list
    End Function

    Public Function ReadToDataTable(sql As String) As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(strconn)
            Dim da As New MySqlDataAdapter(sql, conn)
            da.Fill(dt)
        End Using
        Return dt
    End Function

    Public Function checkProductExists(itemName As String, color As String, size As String) As Integer
        Return GetId($"SELECT product_id FROM product WHERE item_name = '{itemName}' AND color = '{color}' AND size = '{size}'", "product_id")
    End Function

    Public Sub AddStockOnly(productId As Integer, qty As Integer)
        readquery($"UPDATE product SET stock_count = stock_count + {qty} WHERE product_id = {productId}")
    End Sub
    Public Function GetSeries() As List(Of String)
        Return ReadToList("SELECT series_name FROM series ORDER BY series_name", "series_name")
    End Function
    Public Function BulkRetrieveFromBranch(branchName As String) As Boolean
        Try
            readquery("START TRANSACTION")

            ' 1. Move all quantities from 'stores' back to 'product' table stock_count
            Dim sqlSweep = "UPDATE product p " &
                           "INNER JOIN stores s ON p.product_id = s.product_id " &
                           "INNER JOIN branch b ON s.branch_id = b.branch_id " &
                           $"SET p.stock_count = p.stock_count + s.quantity " &
                           $"WHERE b.branch_name = '{branchName}'"
            readquery(sqlSweep)

            ' 2. "Delete" the items from the branch (Retrieval)
            Dim sqlClear = "DELETE s FROM stores s " &
                           "INNER JOIN branch b ON s.branch_id = b.branch_id " &
                           $"WHERE b.branch_name = '{branchName}'"
            readquery(sqlClear)

            readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK")
            Throw New Exception("Bulk sweep failed: " & ex.Message)
        End Try
    End Function
    Private Const SECRET_KEY As String = "MingsCraftSecureKey1234567890123"

    Public Function EncryptData(plainText As String) As String
        If String.IsNullOrWhiteSpace(plainText) Then Return ""

        Using aes As Aes = aes.Create()
            aes.Key = Encoding.UTF8.GetBytes(SECRET_KEY)
            aes.IV = New Byte(15) {}

            Dim encryptor As ICryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor, CryptoStreamMode.Write)
                    Using sw As New StreamWriter(cs)
                        sw.Write(plainText)
                    End Using
                    Return Convert.ToBase64String(ms.ToArray())
                End Using
            End Using
        End Using
    End Function

    Public Function DecryptData(cipherText As String) As String
        If String.IsNullOrWhiteSpace(cipherText) Then Return ""

        Try
            Using aes As Aes = aes.Create()
                aes.Key = Encoding.UTF8.GetBytes(SECRET_KEY)
                aes.IV = New Byte(15) {}

                Dim decryptor As ICryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV)
                Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)

                Using ms As New MemoryStream(cipherBytes)
                    Using cs As New CryptoStream(ms, decryptor, CryptoStreamMode.Read)
                        Using sr As New StreamReader(cs)
                            Return sr.ReadToEnd()
                        End Using
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Return cipherText
        End Try
    End Function
    Public Function ProcessSupplierAdjustment(supplierId As Integer, productId As Integer, supplyDate As String, qty As Integer, adjustmentType As String) As Boolean
        Try
            readquery("START TRANSACTION")


            readquery($"UPDATE product SET stock_count = stock_count - {qty} WHERE product_id = {productId}")

            Dim colToUpdate = If(adjustmentType = "Damaged", "damaged_qty", "returned_qty")
            Dim newStatus = If(adjustmentType = "Damaged", "Has Damages", "Partially Returned")

            readquery($"UPDATE provides SET {colToUpdate} = {colToUpdate} + {qty}, status = '{newStatus}' " &
                      $"WHERE supplier_id = {supplierId} AND product_id = {productId} AND supply_date = '{supplyDate}'")


            Dim logMsg = $"SUPPLIER ADJUSTMENT: Marked {qty} units of Product ID {productId} as {adjustmentType} (Supplier ID: {supplierId})."
            readquery($"INSERT INTO audit_logs (action_description) VALUES ('{logMsg}')")

            readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK")
            Throw New Exception("Database Adjustment Failed: " & ex.Message)
        End Try
    End Function
    Public Function GetProductIdByName(itemName As String) As Integer
        Return GetId($"SELECT product_id FROM product WHERE item_name = '{itemName.Replace("'", "''")}' LIMIT 1", "product_id")
    End Function
End Class