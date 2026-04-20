Imports MySql.Data.MySqlClient

Public Class frmTransaction

    ' tag1 = 1 → PROVIDES  (Supplier restocks → adds stock to product)
    ' tag1 = 2 → PURCHASES (Customer buys → reserves product)
    ' tag1 = 3 → DELIVERS_TO (Courier delivers to customer)
    ' tag1 = 4 → SHIPS (Courier ships product)
    ' tag1 = 5 → STORES (Branch stores product)
    ' tag1 = 6 → WORKS_IN (Employee works in branch)

    Private Sub frmTransaction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            chkNewTarget.Checked = False
            chkNewProduct.Checked = False

            chkNewTarget_CheckedChanged(Nothing, Nothing)
            chkNewProduct_CheckedChanged(Nothing, Nothing)
            ConfigureTransactionUI()
            LoadTransactionDropdowns()
        Catch ex As Exception
            MsgBox("Error loading transaction form: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub


    Private Sub ConfigureTransactionUI()
        lblStatus.Visible = False : cboStatus.Visible = False
        lblShippingFee.Visible = False : txtShippingFee.Visible = False
        lblShippingDate.Visible = False : dtpShippingDate.Visible = False

        Select Case tag1
            Case 1 ' PROVIDES: Supplier restocks inventory
                Me.Text = "Restock Inventory from Supplier"
                lblTransactionHeader.Text = "RESTOCKING TRANSACTION"
                lblNewSupplierHeader.Text = "Select Supplier:"
                lblNewProductHeader.Text = "Select Product:"

                lblDownPayment.Text = "Quantity:"
                lblDownPayment.Visible = True
                txtDownPayment.Visible = True

                lblReservationDate.Text = "Supply Date:"
                dtpReservationDate.Visible = True
                btnProcess.Text = "Process Restock (Add Stock)"
                lblNewCompanyName.Text = "COMPANY NAME"
                lblNewContactPerson.Text = "CONTACT PERSON"
                lblNewCountryOrigin.Text = "COUNTRY ORIGIN"

            Case 2 ' PURCHASES: Customer purchases items
                Me.Text = "Record Customer Purchase"
                lblTransactionHeader.Text = "SALES TRANSACTION"
                lblNewSupplierHeader.Text = "Select Customer:"
                lblNewProductHeader.Text = "Select Product:"

                lblDownPayment.Text = "Quantity:"
                lblDownPayment.Visible = True
                txtDownPayment.Visible = True

                lblStatus.Visible = True : cboStatus.Visible = True
                lblShippingFee.Visible = True : txtShippingFee.Visible = True
                lblShippingDate.Visible = True : dtpShippingDate.Visible = True

                lblReservationDate.Text = "Reservation Date:"
                dtpReservationDate.Visible = True
                btnProcess.Text = "Process Sale"
                lblNewCompanyName.Text = "CUSTOMER NAME"
                lblNewContactPerson.Text = "ADDRESS"
                chkNewTarget.Text = "Add New Customer"

            Case 3 ' DELIVERS_TO: Courier delivers to customer
                Me.Text = "Record Delivery"
                lblTransactionHeader.Text = "DELIVERY TRANSACTION"
                lblNewSupplierHeader.Text = "Select Courier:"
                lblNewProductHeader.Text = "Select Customer:"

                lblDownPayment.Visible = False
                txtDownPayment.Visible = False

                lblReservationDate.Text = "Delivery Date:"
                dtpReservationDate.Visible = True
                btnProcess.Text = "Process Delivery"
                lblNewCompanyName.Text = "COURIER COMPANY"
                lblNewContactPerson.Text = "ADDRESS"
                lblNewCountryOrigin.Text = "CONTACT NUMBER"
                chkNewTarget.Text = "Add New Courier"
                chkNewProduct.Text = "Add New Customer"

            Case 4 ' SHIPS: Courier ships product
                Me.Text = "Record Product Shipment"
                lblTransactionHeader.Text = "SHIPMENT TRANSACTION"
                lblNewSupplierHeader.Text = "Select Courier:"
                lblNewProductHeader.Text = "Select Product:"

                lblDownPayment.Visible = False
                txtDownPayment.Visible = False

                lblReservationDate.Text = "Shipping Date:"
                dtpReservationDate.Visible = True
                btnProcess.Text = "Process Shipment"
                lblNewCompanyName.Text = "COURIER COMPANY"
                lblNewContactPerson.Text = "ADDRESS"
                lblNewCountryOrigin.Text = "CONTACT NUMBER"

            Case 5 ' STORES: Branch stores product
                Me.Text = "Assign Product to Branch"
                lblTransactionHeader.Text = "STORAGE TRANSACTION"
                lblNewSupplierHeader.Text = "Select Branch:"
                lblNewProductHeader.Text = "Select Product:"
                lblDownPayment.Visible = False
                txtDownPayment.Visible = False
                lblReservationDate.Text = "Restock Date:"
                dtpReservationDate.Visible = True
                btnProcess.Text = "Assign to Branch"
                lblNewCompanyName.Text = "BRANCH NAME"
                lblNewContactPerson.Text = "ADDRESS"
                lblNewCountryOrigin.Text = "OPERATING HOURS"
                chkNewTarget.Text = "Add New Branch"

            Case 6 ' WORKS_IN: Employee works in branch
                Me.Text = "Assign Employee to Branch"
                lblTransactionHeader.Text = "WORK SCHEDULE TRANSACTION"
                lblNewSupplierHeader.Text = "Select Employee:"
                lblNewProductHeader.Text = "Select Branch:"
                lblDownPayment.Visible = False
                txtDownPayment.Visible = False
                lblReservationDate.Text = "Scheduled Date:"
                dtpReservationDate.Visible = True
                btnProcess.Text = "Assign Work Schedule"
                lblNewCompanyName.Text = "EMPLOYEE NAME"
                lblNewContactPerson.Text = "ROLE"
                lblNewCountryOrigin.Text = "EMAIL ADDRESS"
                chkNewProduct.Text = "Add New Branch"
                chkNewTarget.Text = "Add New Employee"
                lblNewItemName.Text = "BRANCH NAME"
                lblNewBuyPrice.Text = "ADDRESS"
                lblNewSellPrice.Text = "OPERATING HOURS"

        End Select
    End Sub
    Private Sub HideData()
        lblNewStockCount.Visible = False
        txtNewStockCount.Visible = False

        cboNewStatus.Visible = False
        lblStatus.Visible = False

        lblNewSize.Visible = False
        txtNewSize.Visible = False

        txtNewColor.Visible = False
        lblNewColor.Visible = False
    End Sub
    Private Sub LoadTransactionDropdowns()
        cboTarget.Items.Clear()
        cboProduct.Items.Clear()

        Try
            Select Case tag1

                Case 1 ' PROVIDES: Suppliers → Products
                    readquery("SELECT company_name FROM supplier ORDER BY company_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboTarget.Items.Add(cmdread("company_name").ToString())
                    End While
                    readquery("SELECT item_name FROM product ORDER BY item_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboProduct.Items.Add(cmdread("item_name").ToString())
                    End While

                Case 2 ' PURCHASES: Customers → Products
                    readquery("SELECT customer_name FROM customer ORDER BY customer_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboTarget.Items.Add(cmdread("customer_name").ToString())
                    End While
                    readquery("SELECT item_name FROM product ORDER BY item_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboProduct.Items.Add(cmdread("item_name").ToString())
                    End While

                Case 3
                    readquery("SELECT company_name FROM courier ORDER BY company_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboTarget.Items.Add(cmdread("company_name").ToString())
                    End While
                    readquery("SELECT customer_name FROM customer ORDER BY customer_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboProduct.Items.Add(cmdread("customer_name").ToString())
                    End While

                Case 4 ' SHIPS: Couriers → Products
                    readquery("SELECT company_name FROM courier ORDER BY company_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboTarget.Items.Add(cmdread("company_name").ToString())
                    End While
                    readquery("SELECT item_name FROM product ORDER BY item_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboProduct.Items.Add(cmdread("item_name").ToString())
                    End While

                Case 5 ' STORES: Branches → Products
                    readquery("SELECT branch_name FROM branch ORDER BY branch_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboTarget.Items.Add(cmdread("branch_name").ToString())
                    End While
                    readquery("SELECT item_name FROM product ORDER BY item_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboProduct.Items.Add(cmdread("item_name").ToString())
                    End While

                Case 6

                    readquery("SELECT employee_name FROM employee ORDER BY employee_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboTarget.Items.Add(cmdread("employee_name").ToString())
                    End While
                    readquery("SELECT branch_name FROM branch ORDER BY branch_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboProduct.Items.Add(cmdread("branch_name").ToString())
                    End While

            End Select

        Catch ex As Exception
            MsgBox("Error loading dropdowns: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click

        If Not chkNewTarget.Checked AndAlso cboTarget.Text = "" Then
            MsgBox("Please select a " & lblNewSupplierHeader.Text.Replace("Select ", "").Replace(":", ""), MsgBoxStyle.Exclamation)
            Return
        End If

        Try
            Dim success As Boolean = False
            Select Case tag1
                Case 1 : success = ProcessRestocking()
                Case 2 : success = ProcessSales()
                Case 3 : success = ProcessDelivery()
                Case 4 : success = ProcessShipment()
                Case 5 : success = ProcessStoreAssignment()
                Case 6 : success = ProcessWorkSchedule()
            End Select

            If success Then
                MsgBox("Transaction processed successfully!", MsgBoxStyle.Information)
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox("Error processing transaction: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' ==========================================
    ' TRANSACTION PROCESSORS (Returning True/False)
    ' ==========================================

    ' ★ TRANSACTION 1: PROVIDES (Supplier Restocking)
    Private Function ProcessRestocking() As Boolean
        Dim finalTarget As String = ""
        If chkNewTarget.Checked Then
            If txtNewCompanyName.Text = "" Then MsgBox("Enter Supplier Name.", MsgBoxStyle.Exclamation) : Return False
            readquery("INSERT INTO supplier (company_name, contact_person, country_origin) VALUES ('" & txtNewCompanyName.Text & "', '" & txtNewContactPerson.Text & "', '" & txtNewCountryOrigin.Text & "')")
            finalTarget = txtNewCompanyName.Text
        Else : finalTarget = cboTarget.Text : End If

        Dim finalProd As String = ""
        If chkNewProduct.Checked Then
            If txtNewItemName.Text = "" Then MsgBox("Enter Product Name.", MsgBoxStyle.Exclamation) : Return False
            readquery("INSERT INTO product (item_name, buying_price, selling_price, color, size, status, stock_count) VALUES ('" & txtNewItemName.Text & "', '" & txtNewBuyPrice.Text & "', '" & txtNewSellPrice.Text & "', '" & txtNewColor.Text & "', '" & txtNewSize.Text & "', '" & cboNewStatus.Text & "', '" & txtNewStockCount.Text & "')")
            finalProd = txtNewItemName.Text
        Else
            If cboProduct.Text = "" Then MsgBox("Select Product.", MsgBoxStyle.Exclamation) : Return False
            finalProd = cboProduct.Text
        End If

        Dim qty As Integer
        If Not Integer.TryParse(txtDownPayment.Text.Trim(), qty) OrElse qty <= 0 Then
            MsgBox("Quantity must be a whole number greater than zero.", MsgBoxStyle.Exclamation) : Return False
        End If

        Try
            readquery("START TRANSACTION")
            Dim sqlInsert As String = "INSERT INTO provides (supplier_id, product_id, supply_date, supply_price, quantity_supplied) VALUES (" &
                "(SELECT supplier_id FROM supplier WHERE company_name='" & finalTarget & "'), " &
                "(SELECT product_id FROM product WHERE item_name='" & finalProd & "'), " &
                "'" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "', " &
                "(SELECT buying_price FROM product WHERE item_name='" & finalProd & "'), " & qty & ") " &
                "ON DUPLICATE KEY UPDATE quantity_supplied = quantity_supplied + VALUES(quantity_supplied), supply_date = VALUES(supply_date), supply_price = VALUES(supply_price)"
            Dim sqlUpdate As String = "UPDATE product SET stock_count = stock_count + " & qty & " WHERE item_name = '" & finalProd & "'"

            readquery(sqlInsert) : readquery(sqlUpdate) : readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK") : Throw New Exception(ex.Message)
        End Try
    End Function

    ' ★ TRANSACTION 2: PURCHASES (Customer Sales)
    Private Function ProcessSales() As Boolean
        Dim finalTarget As String = ""
        If chkNewTarget.Checked Then
            If txtNewCompanyName.Text = "" Then MsgBox("Enter Customer Name.", MsgBoxStyle.Exclamation) : Return False
            readquery("INSERT INTO customer (customer_name, address) VALUES ('" & txtNewCompanyName.Text & "', '" & txtNewContactPerson.Text & "')")
            finalTarget = txtNewCompanyName.Text
        Else : finalTarget = cboTarget.Text : End If

        Dim finalProd As String = ""
        If chkNewProduct.Checked Then
            If txtNewItemName.Text = "" Then MsgBox("Enter Product Name.", MsgBoxStyle.Exclamation) : Return False
            readquery("INSERT INTO product (item_name, buying_price, selling_price, color, size, status, stock_count) VALUES ('" & txtNewItemName.Text & "', '" & txtNewBuyPrice.Text & "', '" & txtNewSellPrice.Text & "', '" & txtNewColor.Text & "', '" & txtNewSize.Text & "', '" & cboNewStatus.Text & "', '" & txtNewStockCount.Text & "')")
            finalProd = txtNewItemName.Text
        Else
            If cboProduct.Text = "" Then MsgBox("Select Product.", MsgBoxStyle.Exclamation) : Return False
            finalProd = cboProduct.Text
        End If

        Dim qty As Integer
        If Not Integer.TryParse(txtDownPayment.Text.Trim(), qty) OrElse qty <= 0 Then
            MsgBox("Quantity must be > 0.", MsgBoxStyle.Exclamation) : Return False
        End If

        Try
            readquery("SELECT COUNT(*) AS cnt FROM purchases WHERE customer_id = (SELECT customer_id FROM customer WHERE customer_name='" & finalTarget & "') AND product_id = (SELECT product_id FROM product WHERE item_name='" & finalProd & "')")
            If cmdread.HasRows AndAlso cmdread.Read() AndAlso Val(cmdread("cnt").ToString()) > 0 Then
                MsgBox("Duplicate Purchase Blocked.", MsgBoxStyle.Exclamation) : Return False
            End If

            readquery("SELECT stock_count FROM product WHERE item_name='" & finalProd & "'")
            If cmdread.HasRows Then
                cmdread.Read()
                If Val(cmdread("stock_count").ToString()) < qty Then MsgBox("Insufficient stock!", MsgBoxStyle.Exclamation) : Return False
            End If

            readquery("START TRANSACTION")
            readquery("INSERT INTO purchases (customer_id, product_id, reservation_date, status) VALUES ((SELECT customer_id FROM customer WHERE customer_name='" & finalTarget & "'), (SELECT product_id FROM product WHERE item_name='" & finalProd & "'), '" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "', '" & cboStatus.Text & "')")
            readquery("UPDATE product SET stock_count = stock_count - " & qty & " WHERE item_name = '" & finalProd & "'")
            readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK") : Throw New Exception(ex.Message)
        End Try
    End Function

    ' ★ TRANSACTION 3: DELIVERS_TO (Courier -> Customer)
    Private Function ProcessDelivery() As Boolean
        Dim finalTarget As String = ""
        If chkNewTarget.Checked Then
            If txtNewCompanyName.Text = "" Then MsgBox("Enter Courier Name.", MsgBoxStyle.Exclamation) : Return False
            readquery("INSERT INTO courier (company_name, address, contact_number) VALUES ('" & txtNewCompanyName.Text & "', '" & txtNewContactPerson.Text & "', '" & txtNewCountryOrigin.Text & "')")
            finalTarget = txtNewCompanyName.Text
        Else : finalTarget = cboTarget.Text : End If

        Dim finalProd As String = "" ' Note: This is Customer!
        If chkNewProduct.Checked Then
            If txtNewItemName.Text = "" Then MsgBox("Enter Customer Name.", MsgBoxStyle.Exclamation) : Return False
            readquery("INSERT INTO customer (customer_name, address) VALUES ('" & txtNewItemName.Text & "', '" & txtNewBuyPrice.Text & "')")
            finalProd = txtNewItemName.Text
        Else
            If cboProduct.Text = "" Then MsgBox("Select Customer.", MsgBoxStyle.Exclamation) : Return False
            finalProd = cboProduct.Text
        End If

        Try
            readquery("SELECT COUNT(*) AS cnt FROM delivers_to WHERE courier_id = (SELECT courier_id FROM courier WHERE company_name='" & finalTarget & "') AND customer_id = (SELECT customer_id FROM customer WHERE customer_name='" & finalProd & "')")
            If cmdread.HasRows AndAlso cmdread.Read() AndAlso Val(cmdread("cnt").ToString()) > 0 Then
                If MsgBox("Update delivery date?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Return False
                readquery("UPDATE delivers_to SET delivery_date='" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "' WHERE courier_id = (SELECT courier_id FROM courier WHERE company_name='" & finalTarget & "') AND customer_id = (SELECT customer_id FROM customer WHERE customer_name='" & finalProd & "')")
                Return True
            End If

            readquery("INSERT INTO delivers_to (courier_id, customer_id, delivery_date) VALUES ((SELECT courier_id FROM courier WHERE company_name='" & finalTarget & "'), (SELECT customer_id FROM customer WHERE customer_name='" & finalProd & "'), '" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "')")
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    ' ★ TRANSACTION 4: SHIPS (Courier -> Product)
    Private Function ProcessShipment() As Boolean
        Dim finalTarget As String = ""
        If chkNewTarget.Checked Then
            If txtNewCompanyName.Text = "" Then MsgBox("Enter Courier Name.", MsgBoxStyle.Exclamation) : Return False
            readquery("INSERT INTO courier (company_name, address, contact_number) VALUES ('" & txtNewCompanyName.Text & "', '" & txtNewContactPerson.Text & "', '" & txtNewCountryOrigin.Text & "')")
            finalTarget = txtNewCompanyName.Text
        Else : finalTarget = cboTarget.Text : End If

        Dim finalProd As String = ""
        If chkNewProduct.Checked Then
            If txtNewItemName.Text = "" Then MsgBox("Enter Product Name.", MsgBoxStyle.Exclamation) : Return False
            readquery("INSERT INTO product (item_name, buying_price, selling_price, color, size, status, stock_count) VALUES ('" & txtNewItemName.Text & "', '" & txtNewBuyPrice.Text & "', '" & txtNewSellPrice.Text & "', '" & txtNewColor.Text & "', '" & txtNewSize.Text & "', '" & cboNewStatus.Text & "', '" & txtNewStockCount.Text & "')")
            finalProd = txtNewItemName.Text
        Else
            If cboProduct.Text = "" Then MsgBox("Select Product.", MsgBoxStyle.Exclamation) : Return False
            finalProd = cboProduct.Text
        End If

        Try
            readquery("SELECT COUNT(*) AS cnt FROM ships WHERE courier_id = (SELECT courier_id FROM courier WHERE company_name='" & finalTarget & "') AND product_id = (SELECT product_id FROM product WHERE item_name='" & finalProd & "')")
            If cmdread.HasRows AndAlso cmdread.Read() AndAlso Val(cmdread("cnt").ToString()) > 0 Then
                If MsgBox("Update shipping date?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then Return False
                readquery("UPDATE ships SET shipping_date='" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "' WHERE courier_id = (SELECT courier_id FROM courier WHERE company_name='" & finalTarget & "') AND product_id = (SELECT product_id FROM product WHERE item_name='" & finalProd & "')")
                Return True
            End If

            readquery("INSERT INTO ships (courier_id, product_id, shipping_date) VALUES ((SELECT courier_id FROM courier WHERE company_name='" & finalTarget & "'), (SELECT product_id FROM product WHERE item_name='" & finalProd & "'), '" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "')")
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    ' ★ TRANSACTION 5: STORES (Branch -> Product)
    Private Function ProcessStoreAssignment() As Boolean
        Dim finalTarget As String = ""
        If chkNewTarget.Checked Then
            If txtNewCompanyName.Text = "" Then MsgBox("Enter Branch Name.", MsgBoxStyle.Exclamation) : Return False
            readquery("INSERT INTO branch (branch_name, address, operating_hours) VALUES ('" & txtNewCompanyName.Text & "', '" & txtNewContactPerson.Text & "', '" & txtNewCountryOrigin.Text & "')")
            finalTarget = txtNewCompanyName.Text
        Else : finalTarget = cboTarget.Text : End If

        Dim finalProd As String = ""
        If chkNewProduct.Checked Then
            If txtNewItemName.Text = "" Then MsgBox("Enter Product Name.", MsgBoxStyle.Exclamation) : Return False
            readquery("INSERT INTO product (item_name, buying_price, selling_price, color, size, status, stock_count) VALUES ('" & txtNewItemName.Text & "', '" & txtNewBuyPrice.Text & "', '" & txtNewSellPrice.Text & "', '" & txtNewColor.Text & "', '" & txtNewSize.Text & "', '" & cboNewStatus.Text & "', '" & txtNewStockCount.Text & "')")
            finalProd = txtNewItemName.Text
        Else
            If cboProduct.Text = "" Then MsgBox("Select Product.", MsgBoxStyle.Exclamation) : Return False
            finalProd = cboProduct.Text
        End If

        Try
            readquery("INSERT INTO stores (branch_id, product_id, last_restocked_date) VALUES ((SELECT branch_id FROM branch WHERE branch_name='" & finalTarget & "'), (SELECT product_id FROM product WHERE item_name='" & finalProd & "'), '" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "') ON DUPLICATE KEY UPDATE last_restocked_date = VALUES(last_restocked_date)")
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    ' ★ TRANSACTION 6: WORKS_IN (Employee -> Branch)
    Private Function ProcessWorkSchedule() As Boolean
        Dim finalTarget As String = ""
        If chkNewTarget.Checked Then
            If txtNewCompanyName.Text = "" Then MsgBox("Enter Employee Name.", MsgBoxStyle.Exclamation) : Return False
            readquery("INSERT INTO employee (employee_name, role, email_address) VALUES ('" & txtNewCompanyName.Text & "', '" & txtNewContactPerson.Text & "', '" & txtNewCountryOrigin.Text & "')")
            finalTarget = txtNewCompanyName.Text
        Else : finalTarget = cboTarget.Text : End If

        Dim finalProd As String = ""
        If chkNewProduct.Checked Then
            If txtNewItemName.Text = "" Then MsgBox("Enter Branch Name.", MsgBoxStyle.Exclamation) : Return False
            readquery("INSERT INTO branch (branch_name, address, operating_hours) VALUES ('" & txtNewItemName.Text & "', '" & txtNewBuyPrice.Text & "', '" & txtNewSellPrice.Text & "')")
            finalProd = txtNewItemName.Text
        Else
            If cboProduct.Text = "" Then MsgBox("Select Branch.", MsgBoxStyle.Exclamation) : Return False
            finalProd = cboProduct.Text
        End If

        Try
            readquery("INSERT INTO works_in (employee_id, branch_id, scheduled_date) VALUES ((SELECT employee_id FROM employee WHERE employee_name='" & finalTarget & "'), (SELECT branch_id FROM branch WHERE branch_name='" & finalProd & "'), '" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "') ON DUPLICATE KEY UPDATE scheduled_date = VALUES(scheduled_date)")
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub cboTarget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTarget.SelectedIndexChanged
    End Sub

    Private Sub cboProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProduct.SelectedIndexChanged
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtDownPayment.TextChanged
    End Sub

    Private Sub dtpDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpReservationDate.ValueChanged
    End Sub

    Private Sub lblField3_Click(sender As Object, e As EventArgs) Handles lblNewSellPrice.Click

    End Sub

    Private Sub chkNewTarget_CheckedChanged(sender As Object, e As EventArgs) Handles chkNewTarget.CheckedChanged
        Dim isNew As Boolean = chkNewTarget.Checked

        cboTarget.Visible = Not isNew

        lblNewCompanyName.Visible = isNew
        txtNewCompanyName.Visible = isNew
        lblNewContactPerson.Visible = isNew
        txtNewContactPerson.Visible = isNew

        If tag1 = 2 Then
            lblNewCountryOrigin.Visible = False
            txtNewCountryOrigin.Visible = False
        Else
            lblNewCountryOrigin.Visible = isNew
            txtNewCountryOrigin.Visible = isNew
        End If

        ' Clear textboxes if unchecked
        If Not isNew Then
            txtNewCompanyName.Clear()
            txtNewContactPerson.Clear()
            txtNewCountryOrigin.Clear()
        End If
    End Sub

    Private Sub chkNewProduct_CheckedChanged(sender As Object, e As EventArgs) Handles chkNewProduct.CheckedChanged
        Dim isNew As Boolean = chkNewProduct.Checked

        cboProduct.Visible = Not isNew

        lblNewItemName.Visible = isNew : txtNewItemName.Visible = isNew
        lblNewBuyPrice.Visible = isNew : txtNewBuyPrice.Visible = isNew


        If tag1 = 3 Then

            lblNewSellPrice.Visible = False : txtNewSellPrice.Visible = False
            lblNewColor.Visible = False : txtNewColor.Visible = False
            lblNewSize.Visible = False : txtNewSize.Visible = False
            lblNewStatus.Visible = False : cboNewStatus.Visible = False
            lblNewStockCount.Visible = False : txtNewStockCount.Visible = False

        ElseIf tag1 = 6 Then

            lblNewSellPrice.Visible = isNew : txtNewSellPrice.Visible = isNew
            lblNewColor.Visible = False : txtNewColor.Visible = False
            lblNewSize.Visible = False : txtNewSize.Visible = False
            lblNewStatus.Visible = False : cboNewStatus.Visible = False
            lblNewStockCount.Visible = False : txtNewStockCount.Visible = False

        Else

            lblNewSellPrice.Visible = isNew : txtNewSellPrice.Visible = isNew
            lblNewColor.Visible = isNew : txtNewColor.Visible = isNew
            lblNewSize.Visible = isNew : txtNewSize.Visible = isNew
            lblNewStatus.Visible = isNew : cboNewStatus.Visible = isNew
            lblNewStockCount.Visible = isNew : txtNewStockCount.Visible = isNew
        End If

        If Not isNew Then
            txtNewItemName.Clear() : txtNewBuyPrice.Clear() : txtNewSellPrice.Clear()
            txtNewColor.Clear() : txtNewSize.Clear() : txtNewStockCount.Clear()
            cboNewStatus.SelectedIndex = -1
        End If
    End Sub
End Class