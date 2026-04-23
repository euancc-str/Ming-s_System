Imports MySql.Data.MySqlClient
Imports System.Data

Public Class frmTransaction

    ' ==========================================
    ' VALIDATION: KEYSTROKE SHIELDS (SAFETY NET)
    ' ==========================================
    Private Sub TransactionPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNewBuyPrice.KeyPress, txtNewSellPrice.KeyPress, txtShippingFee.KeyPress, txtDownPayment.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> "."c) Then
            e.Handled = True
        End If
        If (e.KeyChar = "."c) AndAlso (CType(sender, TextBox).Text.IndexOf("."c) > -1) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TransactionQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQuantity.KeyPress, txtNewStockCount.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    ' ==========================================
    ' FORM INITIALIZATION
    ' ==========================================
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
            MsgBox("Error loading transaction form: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ConfigureTransactionUI()
        lblStatus.Visible = False : cboStatus.Visible = False
        lblShippingFee.Visible = False : txtShippingFee.Visible = False
        lblShippingDate.Visible = False : dtpShippingDate.Visible = False
        cboCourier.Visible = False : lblCourier.Visible = False
        If lblStartTime IsNot Nothing Then lblStartTime.Visible = False
        If dtpStartTime IsNot Nothing Then dtpStartTime.Visible = False
        If cboDeliveryType IsNot Nothing Then cboDeliveryType.Visible = False

        lblDownPayment.Visible = False : txtDownPayment.Visible = False
        lblQuantity.Visible = False : txtQuantity.Visible = False
        lblSalesLocation.Visible = False : cboSalesLocation.Visible = False
        lblDelivery.Visible = False


        Select Case tag1
            Case 1
                Me.Text = "Restock Inventory from Supplier"
                lblTransactionHeader.Text = "RESTOCKING TRANSACTION"
                lblNewSupplierHeader.Text = "Select Supplier:"
                lblNewProductHeader.Text = "Select Product:"
                lblQuantity.Visible = True : txtQuantity.Visible = True
                lblReservationDate.Text = "Supply Date:"
                dtpReservationDate.Visible = True
                btnProcess.Text = "Process Restock (Add Stock)"
                lblNewCompanyName.Text = "COMPANY NAME"
                lblNewContactPerson.Text = "CONTACT PERSON"
                lblNewCountryOrigin.Text = "COUNTRY ORIGIN"

            Case 2
                Me.Text = "Record Customer Purchase"
                lblTransactionHeader.Text = "SALES TRANSACTION"
                lblNewSupplierHeader.Text = "Select Customer:"
                lblNewProductHeader.Text = "Select Product:"
                lblDownPayment.Visible = True : txtDownPayment.Visible = True
                lblQuantity.Visible = True : txtQuantity.Visible = True
                lblStatus.Visible = True : cboStatus.Visible = True
                lblSalesLocation.Visible = True : cboSalesLocation.Visible = True
                cboStatus.Items.Clear()
                cboStatus.Items.Add("Pending")
                cboStatus.Items.Add("Delivered")
                cboStatus.SelectedIndex = 0
                If cboDeliveryType IsNot Nothing Then
                    cboDeliveryType.Visible = True
                    cboDeliveryType.Items.Clear()
                    cboDeliveryType.Items.Add("Walk-in / Pickup")
                    cboDeliveryType.Items.Add("Courier Delivery")
                    cboDeliveryType.SelectedIndex = 0
                End If

                lblReservationDate.Text = "Reservation Date:"
                dtpReservationDate.Visible = True
                btnProcess.Text = "Process Sale"
                lblNewCompanyName.Text = "CUSTOMER NAME"
                lblNewContactPerson.Text = "ADDRESS"
                chkNewTarget.Text = "Add New Customer"

                cboSalesLocation.Items.Clear()
                cboSalesLocation.Items.Add("Main Warehouse")
                readquery("SELECT branch_name FROM branch ORDER BY branch_name")
                While cmdread.HasRows AndAlso cmdread.Read()
                    cboSalesLocation.Items.Add(cmdread("branch_name").ToString())
                End While
                cboSalesLocation.SelectedIndex = 0

            Case 5
                Me.Text = "Assign Product to Branch"
                lblTransactionHeader.Text = "STORAGE TRANSACTION"
                lblNewSupplierHeader.Text = "Select Branch:"
                lblNewProductHeader.Text = "Select Product:"
                lblQuantity.Visible = True : txtQuantity.Visible = True
                lblReservationDate.Text = "Restock Date:"
                dtpReservationDate.Visible = True
                btnProcess.Text = "Assign to Branch"
                lblNewCompanyName.Text = "BRANCH NAME"
                lblNewContactPerson.Text = "ADDRESS"
                lblNewCountryOrigin.Text = "OPERATING HOURS"
                chkNewTarget.Text = "Add New Branch"

            Case 6
                Me.Text = "Assign Employee to Branch"
                lblTransactionHeader.Text = "WORK SCHEDULE TRANSACTION"
                lblNewSupplierHeader.Text = "Select Employee:"
                lblNewProductHeader.Text = "Select Branch:"
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
                btnProcess.Text = "Assign Work Schedule"
                lblNewCompanyName.Text = "EMPLOYEE NAME"
                lblNewContactPerson.Text = "ROLE"
                lblNewCountryOrigin.Text = "EMAIL ADDRESS"
                dtpShippingDate.Format = DateTimePickerFormat.Time
                dtpShippingDate.ShowUpDown = True
                dtpShippingDate.Location = txtQuantity.Location
                dtpShippingDate.Visible = True
                chkNewProduct.Text = "Add New Branch"
                chkNewTarget.Text = "Add New Employee"

            Case 7
                Me.Text = "Internal Stock Transfer"
                lblTransactionHeader.Text = "INTERNAL TRANSFER LOGISTICS"
                lblSalesLocation.Text = "Source (FROM):"
                lblSalesLocation.Visible = True : cboSalesLocation.Visible = True
                lblNewSupplierHeader.Text = "Destination (TO):"
                lblNewProductHeader.Text = "Select Product:"
                lblQuantity.Visible = True : txtQuantity.Visible = True
                lblReservationDate.Text = "Transfer Date:"
                dtpReservationDate.Visible = True
                btnProcess.Text = "Execute Transfer"
                chkNewTarget.Visible = False
                chkNewProduct.Visible = False
        End Select
    End Sub

    Private Sub LoadTransactionDropdowns()
        cboTarget.Items.Clear()
        cboProduct.DataSource = Nothing
        cboProduct.Items.Clear()
        cboCourier.Items.Clear()
        If tag1 <> 2 Then cboSalesLocation.Items.Clear() ' Keep Sales logic intact

        Try
            Select Case tag1
                Case 1
                    readquery("SELECT company_name FROM supplier ORDER BY company_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboTarget.Items.Add(cmdread("company_name").ToString())
                    End While
                    LoadEnterpriseProductDropdown("SELECT product_id, CONCAT_WS(' | ', item_name, NULLIF(color, ''), NULLIF(size, '')) AS DisplayName FROM product ORDER BY item_name")

                Case 2
                    readquery("SELECT customer_name FROM customer ORDER BY customer_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboTarget.Items.Add(cmdread("customer_name").ToString())
                    End While
                    LoadEnterpriseProductDropdown("SELECT product_id, CONCAT_WS(' | ', item_name, NULLIF(color, ''), NULLIF(size, '')) AS DisplayName FROM product ORDER BY item_name")

                    readquery("SELECT company_name FROM courier ORDER BY company_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboCourier.Items.Add(cmdread("company_name").ToString())
                    End While

                Case 5
                    readquery("SELECT branch_name FROM branch ORDER BY branch_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboTarget.Items.Add(cmdread("branch_name").ToString())
                    End While
                    LoadEnterpriseProductDropdown("SELECT product_id, CONCAT_WS(' | ', item_name, NULLIF(color, ''), NULLIF(size, '')) AS DisplayName FROM product ORDER BY item_name")

                Case 6
                    readquery("SELECT employee_name FROM employee ORDER BY employee_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboTarget.Items.Add(cmdread("employee_name").ToString())
                    End While
                    readquery("SELECT branch_name FROM branch ORDER BY branch_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboProduct.Items.Add(cmdread("branch_name").ToString())
                    End While

                Case 7
                    cboTarget.Items.Add("Main Warehouse")
                    cboSalesLocation.Items.Add("Main Warehouse")
                    readquery("SELECT branch_name FROM branch ORDER BY branch_name")
                    While cmdread.HasRows AndAlso cmdread.Read()
                        cboTarget.Items.Add(cmdread("branch_name").ToString())
                        cboSalesLocation.Items.Add(cmdread("branch_name").ToString())
                    End While
                    ' Product dropdown is loaded dynamically by cboSalesLocation_SelectedIndexChanged
            End Select
        Catch ex As Exception
            MsgBox("Error loading dropdowns: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' Helper function for clean DataBinding
    Private Sub LoadEnterpriseProductDropdown(sql As String)
        Dim dtProduct As New DataTable()
        Using localConn As New MySqlConnection(strconn)
            Dim da As New MySqlDataAdapter(sql, localConn)
            da.Fill(dtProduct)
        End Using
        cboProduct.DataSource = dtProduct
        cboProduct.DisplayMember = "DisplayName"
        cboProduct.ValueMember = "product_id"
        cboProduct.SelectedIndex = -1
    End Sub

    ' ==========================================
    ' DYNAMIC CASCADING DROPDOWNS
    ' ==========================================
    Private Sub cboSalesLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSalesLocation.SelectedIndexChanged
        If tag1 <> 7 OrElse cboSalesLocation.Text.Trim() = "" Then Return

        Dim sourceLoc As String = cboSalesLocation.Text.Trim().Replace("'", "''")
        Dim sqlProd As String = ""

        If sourceLoc = "Main Warehouse" Then
            sqlProd = "SELECT product_id, CONCAT_WS(' | ', item_name, NULLIF(color, ''), NULLIF(size, '')) AS DisplayName " &
                      "FROM product WHERE COALESCE(stock_count, 0) > 0 ORDER BY item_name"
        Else
            sqlProd = "SELECT p.product_id, CONCAT_WS(' | ', p.item_name, NULLIF(p.color, ''), NULLIF(p.size, '')) AS DisplayName " &
                      "FROM stores s INNER JOIN product p ON s.product_id = p.product_id " &
                      "INNER JOIN branch b ON s.branch_id = b.branch_id " &
                      "WHERE b.branch_name = '" & sourceLoc & "' AND COALESCE(s.quantity, 0) > 0 ORDER BY p.item_name"
        End If

        Try
            cboProduct.DataSource = Nothing
            cboProduct.Items.Clear()
            LoadEnterpriseProductDropdown(sqlProd)
            lblNewBuyPrice.Visible = False : txtNewBuyPrice.Visible = False
            lblNewStockCount.Visible = False : txtNewStockCount.Visible = False
            txtNewBuyPrice.Clear() : txtNewStockCount.Clear()
        Catch ex As Exception
            MsgBox("Error loading inventory for location: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub cboProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProduct.SelectedIndexChanged
        If cboProduct.SelectedIndex = -1 Then
            lblNewBuyPrice.Visible = False : txtNewBuyPrice.Visible = False
            lblNewStockCount.Visible = False : txtNewStockCount.Visible = False
            txtNewBuyPrice.Clear() : txtNewStockCount.Clear()
            Return
        End If

        Try
            If tag1 = 6 OrElse TypeOf cboProduct.SelectedValue Is DataRowView Then Return

            Dim selectedID As Integer = Convert.ToInt32(cboProduct.SelectedValue)
            Dim sqlDetails As String = ""

            ' SMART DISPLAY: Show stock based on selected location
            If tag1 = 7 AndAlso cboSalesLocation.Text <> "Main Warehouse" AndAlso cboSalesLocation.Text <> "" Then
                sqlDetails = "SELECT p.selling_price, s.quantity AS stock_count " &
                             "FROM product p INNER JOIN stores s ON p.product_id = s.product_id " &
                             "INNER JOIN branch b ON s.branch_id = b.branch_id " &
                             "WHERE p.product_id = " & selectedID & " AND b.branch_name = '" & cboSalesLocation.Text.Trim().Replace("'", "''") & "'"
                lblNewStockCount.Text = "Branch Stock:"
            Else
                sqlDetails = "SELECT selling_price, stock_count FROM product WHERE product_id = " & selectedID
                lblNewStockCount.Text = "Warehouse Stock:"
            End If

            readquery(sqlDetails)

            If cmdread.HasRows Then
                cmdread.Read()
                txtNewBuyPrice.Text = cmdread("selling_price").ToString()
                txtNewStockCount.Text = cmdread("stock_count").ToString()
                lblNewBuyPrice.Visible = True : txtNewBuyPrice.Visible = True
                lblNewStockCount.Visible = True : txtNewStockCount.Visible = True
            End If
        Catch ex As Exception
            MsgBox("Error fetching product details: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' ==========================================
    ' PROCESS ROUTING
    ' ==========================================
    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        If Not chkNewTarget.Checked AndAlso cboTarget.Text = "" Then
            MsgBox("Please select a target entity to proceed.", MsgBoxStyle.Exclamation, "Validation Error")
            Return
        End If

        If Not chkNewProduct.Checked AndAlso cboProduct.Text = "" Then
            MsgBox("Please select a secondary entity to proceed.", MsgBoxStyle.Exclamation, "Validation Error")
            Return
        End If

        Try
            Dim success As Boolean = False
            Select Case tag1
                Case 1 : success = ProcessRestocking()
                Case 2 : success = ProcessSales()
                Case 5 : success = ProcessStoreAssignment()
                Case 6 : success = ProcessWorkSchedule()
                Case 7 : success = ProcessInternalTransfer()
            End Select

            If success Then
                MsgBox("Transaction processed successfully!", MsgBoxStyle.Information)
                txtQuantity.Clear()
                If txtDownPayment.Visible Then txtDownPayment.Clear()
                If txtShippingFee.Visible Then txtShippingFee.Clear()
                cboProduct.SelectedIndex = -1
                chkNewTarget.Checked = False
                chkNewProduct.Checked = False
            End If
        Catch ex As Exception
            MsgBox("Error processing transaction: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' ==========================================
    ' TRANSACTION ENGINES
    ' ==========================================
    Private Function ProcessInternalTransfer() As Boolean
        Dim sourceLoc As String = cboSalesLocation.Text.Trim().Replace("'", "''")
        Dim destLoc As String = cboTarget.Text.Trim().Replace("'", "''")

        If sourceLoc = "" OrElse destLoc = "" Then MsgBox("Please select Source and Destination.", MsgBoxStyle.Exclamation) : Return False
        If sourceLoc = destLoc Then MsgBox("Source and Destination cannot be the same.", MsgBoxStyle.Exclamation) : Return False
        If cboProduct.SelectedIndex = -1 Then MsgBox("Please select a product.", MsgBoxStyle.Exclamation) : Return False

        Dim qty As Integer
        If Not Integer.TryParse(txtQuantity.Text.Trim(), qty) OrElse qty <= 0 Then MsgBox("Please enter a valid transfer quantity.", MsgBoxStyle.Exclamation) : Return False

        Dim prodID As String = cboProduct.SelectedValue.ToString()

        Try
            If sourceLoc = "Main Warehouse" Then
                readquery("SELECT stock_count FROM product WHERE product_id = " & prodID)
                If cmdread.HasRows Then
                    cmdread.Read()
                    If Val(cmdread("stock_count").ToString()) < qty Then MsgBox("Not enough stock in the Main Warehouse!", MsgBoxStyle.Exclamation) : Return False
                End If
            Else
                readquery("SELECT quantity FROM stores WHERE product_id = " & prodID & " AND branch_id = (SELECT branch_id FROM branch WHERE branch_name='" & sourceLoc & "')")
                If cmdread.HasRows Then
                    cmdread.Read()
                    If Val(cmdread("quantity").ToString()) < qty Then MsgBox("Not enough stock at source branch!", MsgBoxStyle.Exclamation) : Return False
                Else
                    MsgBox("Source branch does not have this product!", MsgBoxStyle.Exclamation) : Return False
                End If
            End If

            readquery("START TRANSACTION")

            If sourceLoc = "Main Warehouse" Then
                readquery("UPDATE product SET stock_count = stock_count - " & qty & " WHERE product_id = " & prodID)
            Else
                readquery("UPDATE stores SET quantity = quantity - " & qty & " WHERE product_id = " & prodID & " AND branch_id = (SELECT branch_id FROM branch WHERE branch_name='" & sourceLoc & "')")
            End If

            If destLoc = "Main Warehouse" Then
                readquery("UPDATE product SET stock_count = stock_count + " & qty & " WHERE product_id = " & prodID)
            Else
                Dim sqlUpsertDest As String = "INSERT INTO stores (branch_id, product_id, quantity, last_restocked_date) " &
                                              "VALUES ((SELECT branch_id FROM branch WHERE branch_name='" & destLoc & "'), " &
                                              prodID & ", " & qty & ", '" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "') " &
                                              "ON DUPLICATE KEY UPDATE quantity = quantity + " & qty & ", last_restocked_date = VALUES(last_restocked_date)"
                readquery(sqlUpsertDest)
            End If

            readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK") : Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function ProcessRestocking() As Boolean
        Dim finalTarget As String = cboTarget.Text.Trim().Replace("'", "''")
        If chkNewTarget.Checked Then
            finalTarget = txtNewCompanyName.Text.Trim().Replace("'", "''")
            If finalTarget = "" Then MsgBox("Please enter Supplier details.", MsgBoxStyle.Exclamation) : Return False
            readquery("SELECT company_name FROM supplier WHERE company_name='" & finalTarget & "'")
            If Not cmdread.HasRows Then
                readquery("INSERT INTO supplier (company_name, contact_person, country_origin) VALUES ('" & finalTarget & "', '" & txtNewContactPerson.Text.Trim().Replace("'", "''") & "', '" & txtNewCountryOrigin.Text.Trim().Replace("'", "''") & "')")
            End If
        End If

        Dim finalProdID As String = ""
        If chkNewProduct.Checked Then
            Dim pName = txtNewItemName.Text.Trim().Replace("'", "''")
            Dim pCol = txtNewColor.Text.Trim().Replace("'", "''")
            Dim pSize = txtNewSize.Text.Trim().Replace("'", "''")
            If pName = "" Then MsgBox("Please enter Product Name.", MsgBoxStyle.Exclamation) : Return False
            Dim bPrice As Double = Val(txtNewBuyPrice.Text.Trim())
            Dim sPrice As Double = Val(txtNewSellPrice.Text.Trim())
            If sPrice <= bPrice Then MsgBox("Selling price must be higher than buying price!", MsgBoxStyle.Exclamation) : Return False

            readquery("SELECT product_id FROM product WHERE item_name='" & pName & "' AND color='" & pCol & "' AND size='" & pSize & "'")
            If Not cmdread.HasRows Then
                readquery("INSERT INTO product (item_name, buying_price, selling_price, color, size, status, stock_count) VALUES ('" & pName & "', '" & bPrice & "', '" & sPrice & "', '" & pCol & "', '" & pSize & "', '" & cboNewStatus.Text.Trim() & "', '" & Val(txtNewStockCount.Text) & "')")
            End If
            finalProdID = "(SELECT product_id FROM product WHERE item_name='" & pName & "' AND color='" & pCol & "' AND size='" & pSize & "')"
        Else
            finalProdID = cboProduct.SelectedValue.ToString()
        End If

        Dim qty As Integer
        If Not Integer.TryParse(txtQuantity.Text.Trim(), qty) OrElse qty <= 0 Then MsgBox("Invalid Quantity.", MsgBoxStyle.Exclamation) : Return False

        Try
            readquery("START TRANSACTION")
            Dim sqlInsert As String = "INSERT INTO provides (supplier_id, product_id, supply_date, supply_price, quantity_supplied) VALUES (" &
                "(SELECT supplier_id FROM supplier WHERE company_name='" & finalTarget & "'), " & finalProdID & ", '" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "', (SELECT buying_price FROM product WHERE product_id=" & finalProdID & "), " & qty & ") " &
                "ON DUPLICATE KEY UPDATE quantity_supplied = quantity_supplied + VALUES(quantity_supplied), supply_date = VALUES(supply_date), supply_price = VALUES(supply_price)"
            readquery(sqlInsert)
            readquery("UPDATE product SET stock_count = COALESCE(stock_count, 0) + " & qty & " WHERE product_id = " & finalProdID)
            readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK") : Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function ProcessSales() As Boolean
        Dim finalTarget As String = cboTarget.Text.Trim().Replace("'", "''")
        If chkNewTarget.Checked Then
            finalTarget = txtNewCompanyName.Text.Trim().Replace("'", "''")
            If finalTarget = "" Then MsgBox("Please enter Customer details.", MsgBoxStyle.Exclamation) : Return False
            readquery("SELECT customer_name FROM customer WHERE customer_name='" & finalTarget & "'")
            If Not cmdread.HasRows Then
                readquery("INSERT INTO customer (customer_name, address) VALUES ('" & finalTarget & "', '" & txtNewContactPerson.Text.Trim().Replace("'", "''") & "')")
            End If
        End If

        Dim finalProdID As String = ""
        If chkNewProduct.Checked Then
            Dim pName = txtNewItemName.Text.Trim().Replace("'", "''")
            Dim pCol = txtNewColor.Text.Trim().Replace("'", "''")
            Dim pSize = txtNewSize.Text.Trim().Replace("'", "''")
            If pName = "" Then MsgBox("Please enter Product Name.", MsgBoxStyle.Exclamation) : Return False
            Dim bPrice As Double = Val(txtNewBuyPrice.Text.Trim())
            Dim sPrice As Double = Val(txtNewSellPrice.Text.Trim())
            If sPrice <= bPrice Then MsgBox("Selling price must be higher than buying price!", MsgBoxStyle.Exclamation) : Return False

            readquery("SELECT product_id FROM product WHERE item_name='" & pName & "' AND color='" & pCol & "' AND size='" & pSize & "'")
            If Not cmdread.HasRows Then
                readquery("INSERT INTO product (item_name, buying_price, selling_price, color, size, status, stock_count) VALUES ('" & pName & "', '" & bPrice & "', '" & sPrice & "', '" & pCol & "', '" & pSize & "', '" & cboNewStatus.Text.Trim() & "', '" & Val(txtNewStockCount.Text) & "')")
            End If
            finalProdID = "(SELECT product_id FROM product WHERE item_name='" & pName & "' AND color='" & pCol & "' AND size='" & pSize & "')"
        Else
            finalProdID = cboProduct.SelectedValue.ToString()
        End If

        Dim qty As Integer
        If Not Integer.TryParse(txtQuantity.Text.Trim(), qty) OrElse qty <= 0 Then MsgBox("Invalid Quantity.", MsgBoxStyle.Exclamation) : Return False

        Dim downPayment As Double = Val(txtDownPayment.Text.Trim())
        Dim shippingFee As Double = 0
        Dim shippingDateSQL As String = "NULL"

        If cboDeliveryType.Text = "Courier Delivery" Then
            If cboCourier.Text.Trim() = "" Then MsgBox("Select Courier.", MsgBoxStyle.Exclamation) : Return False
            shippingFee = Val(txtShippingFee.Text.Trim())
            shippingDateSQL = "'" & dtpShippingDate.Value.ToString("yyyy-MM-dd") & "'"
        End If

        Dim salesLoc = cboSalesLocation.Text.Trim().Replace("'", "''")
        If salesLoc = "" Then MsgBox("Select Sales Location.", MsgBoxStyle.Exclamation) : Return False
        Dim statusValue = If(cboStatus.Text.Trim() = "", "Pending", cboStatus.Text.Trim())

        Try
            If salesLoc = "Main Warehouse" Then
                readquery("SELECT stock_count FROM product WHERE product_id=" & finalProdID)
                If cmdread.HasRows Then
                    cmdread.Read()
                    If Val(cmdread("stock_count").ToString()) < qty Then MsgBox("Insufficient stock in Warehouse!", MsgBoxStyle.Exclamation) : Return False
                End If
            Else
                readquery("SELECT quantity FROM stores WHERE product_id=" & finalProdID & " AND branch_id = (SELECT branch_id FROM branch WHERE branch_name='" & salesLoc & "')")
                If cmdread.HasRows Then
                    cmdread.Read()
                    If Val(cmdread("quantity").ToString()) < qty Then MsgBox("Insufficient stock at Branch!", MsgBoxStyle.Exclamation) : Return False
                Else
                    MsgBox("Branch does not carry this product!", MsgBoxStyle.Exclamation) : Return False
                End If
            End If

            readquery("START TRANSACTION")
            readquery("INSERT INTO purchases (customer_id, product_id, quantity, reservation_date, status, down_payment, shipping_fee, shipping_date) VALUES ((SELECT customer_id FROM customer WHERE customer_name='" & finalTarget & "'), " & finalProdID & ", " & qty & ", '" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "', '" & statusValue & "', " & downPayment & ", " & shippingFee & ", " & shippingDateSQL & ")")

            If cboDeliveryType.Text = "Courier Delivery" Then
                readquery("INSERT INTO delivers_to (courier_id, customer_id, delivery_date, shipping_fee) VALUES ((SELECT courier_id FROM courier WHERE company_name='" & cboCourier.Text.Trim().Replace("'", "''") & "'), (SELECT customer_id FROM customer WHERE customer_name='" & finalTarget & "'), " & shippingDateSQL & ", " & shippingFee & ") ON DUPLICATE KEY UPDATE delivery_date = VALUES(delivery_date), shipping_fee = VALUES(shipping_fee)")
            End If

            If salesLoc = "Main Warehouse" Then
                readquery("UPDATE product SET stock_count = COALESCE(stock_count, 0) - " & qty & " WHERE product_id = " & finalProdID)
            Else
                readquery("UPDATE stores SET quantity = COALESCE(quantity, 0) - " & qty & " WHERE product_id = " & finalProdID & " AND branch_id = (SELECT branch_id FROM branch WHERE branch_name='" & salesLoc & "')")
            End If

            readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK") : Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function ProcessStoreAssignment() As Boolean
        Dim finalTarget = cboTarget.Text.Trim().Replace("'", "''")
        If chkNewTarget.Checked Then
            finalTarget = txtNewCompanyName.Text.Trim().Replace("'", "''")
            If finalTarget = "" Then Return False
            readquery("SELECT branch_name FROM branch WHERE branch_name='" & finalTarget & "'")
            If Not cmdread.HasRows Then
                readquery("INSERT INTO branch (branch_name, address, operating_hours) VALUES ('" & finalTarget & "', '" & txtNewContactPerson.Text.Trim().Replace("'", "''") & "', '" & txtNewCountryOrigin.Text.Trim().Replace("'", "''") & "')")
            End If
        End If

        Dim finalProdID As String = ""
        If chkNewProduct.Checked Then
            Dim pName = txtNewItemName.Text.Trim().Replace("'", "''")
            Dim pCol = txtNewColor.Text.Trim().Replace("'", "''")
            Dim pSize = txtNewSize.Text.Trim().Replace("'", "''")
            If pName = "" Then Return False
            Dim bPrice As Double = Val(txtNewBuyPrice.Text.Trim())
            Dim sPrice As Double = Val(txtNewSellPrice.Text.Trim())
            If sPrice <= bPrice Then Return False

            readquery("SELECT product_id FROM product WHERE item_name='" & pName & "' AND color='" & pCol & "' AND size='" & pSize & "'")
            If Not cmdread.HasRows Then
                readquery("INSERT INTO product (item_name, buying_price, selling_price, color, size, status, stock_count) VALUES ('" & pName & "', '" & bPrice & "', '" & sPrice & "', '" & pCol & "', '" & pSize & "', '" & cboNewStatus.Text.Trim() & "', '0')")
            End If
            finalProdID = "(SELECT product_id FROM product WHERE item_name='" & pName & "' AND color='" & pCol & "' AND size='" & pSize & "')"
        Else
            finalProdID = cboProduct.SelectedValue.ToString()
        End If

        Dim qty As Integer
        If Not Integer.TryParse(txtQuantity.Text.Trim(), qty) OrElse qty <= 0 Then Return False

        Try
            readquery("SELECT stock_count FROM product WHERE product_id=" & finalProdID)
            If cmdread.HasRows Then
                cmdread.Read()
                If Val(cmdread("stock_count").ToString()) < qty Then MsgBox("Not enough stock in main inventory to send to branch!", MsgBoxStyle.Exclamation) : Return False
            End If

            readquery("START TRANSACTION")
            readquery("INSERT INTO stores (branch_id, product_id, quantity, last_restocked_date) VALUES ((SELECT branch_id FROM branch WHERE branch_name='" & finalTarget & "'), " & finalProdID & ", " & qty & ", '" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "') ON DUPLICATE KEY UPDATE quantity = COALESCE(quantity, 0) + " & qty & ", last_restocked_date = VALUES(last_restocked_date)")
            readquery("UPDATE product SET stock_count = COALESCE(stock_count, 0) - " & qty & " WHERE product_id = " & finalProdID)
            readquery("COMMIT")
            Return True
        Catch ex As Exception
            readquery("ROLLBACK") : Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function ProcessWorkSchedule() As Boolean
        Dim finalTarget = cboTarget.Text.Trim().Replace("'", "''")
        If chkNewTarget.Checked Then
            finalTarget = txtNewCompanyName.Text.Trim().Replace("'", "''")
            If finalTarget = "" Then Return False
            readquery("SELECT employee_name FROM employee WHERE employee_name='" & finalTarget & "'")
            If Not cmdread.HasRows Then
                readquery("INSERT INTO employee (employee_name, role, email_address) VALUES ('" & finalTarget & "', '" & txtNewContactPerson.Text.Trim().Replace("'", "''") & "', '" & txtNewCountryOrigin.Text.Trim().Replace("'", "''") & "')")
            End If
        End If

        Dim finalProd = cboProduct.Text.Trim().Replace("'", "''")
        If dtpStartTime.Value.TimeOfDay >= dtpShippingDate.Value.TimeOfDay Then MsgBox("Start Time must be earlier than End Time.", MsgBoxStyle.Exclamation) : Return False

        If chkNewProduct.Checked Then
            finalProd = txtNewItemName.Text.Trim().Replace("'", "''")
            If finalProd = "" Then Return False
            readquery("SELECT branch_name FROM branch WHERE branch_name='" & finalProd & "'")
            If Not cmdread.HasRows Then
                readquery("INSERT INTO branch (branch_name, address, operating_hours) VALUES ('" & finalProd & "', '" & txtNewBuyPrice.Text.Trim().Replace("'", "''") & "', '" & txtNewSellPrice.Text.Trim().Replace("'", "''") & "')")
            End If
        End If

        Try
            readquery("INSERT INTO works_in (employee_id, branch_id, scheduled_date, start_time, end_time) VALUES ((SELECT employee_id FROM employee WHERE employee_name='" & finalTarget & "'), (SELECT branch_id FROM branch WHERE branch_name='" & finalProd & "'), '" & dtpReservationDate.Value.ToString("yyyy-MM-dd") & "', '" & dtpStartTime.Value.ToString("HH:mm:ss") & "', '" & dtpShippingDate.Value.ToString("HH:mm:ss") & "')")
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    ' ==========================================
    ' UI HELPERS
    ' ==========================================
    Private Sub cboDeliveryType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDeliveryType.SelectedIndexChanged
        dtpShippingDate.Format = DateTimePickerFormat.Long
        dtpShippingDate.ShowUpDown = False
        Dim isDelivery As Boolean = (cboDeliveryType.Text = "Courier Delivery")
        lblCourier.Visible = isDelivery : cboCourier.Visible = isDelivery
        lblShippingFee.Visible = isDelivery : txtShippingFee.Visible = isDelivery
        lblShippingDate.Visible = isDelivery : dtpShippingDate.Visible = isDelivery
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub chkNewTarget_CheckedChanged(sender As Object, e As EventArgs) Handles chkNewTarget.CheckedChanged
        Dim isNew As Boolean = chkNewTarget.Checked
        cboTarget.Visible = Not isNew
        lblNewCompanyName.Visible = isNew : txtNewCompanyName.Visible = isNew
        lblNewContactPerson.Visible = isNew : txtNewContactPerson.Visible = isNew
        If tag1 = 2 Then
            lblNewCountryOrigin.Visible = False : txtNewCountryOrigin.Visible = False
        Else
            lblNewCountryOrigin.Visible = isNew : txtNewCountryOrigin.Visible = isNew
        End If
        If Not isNew Then txtNewCompanyName.Clear() : txtNewContactPerson.Clear() : txtNewCountryOrigin.Clear()
    End Sub

    Private Sub chkNewProduct_CheckedChanged(sender As Object, e As EventArgs) Handles chkNewProduct.CheckedChanged
        Dim isNew As Boolean = chkNewProduct.Checked
        cboProduct.Visible = Not isNew
        lblNewItemName.Visible = isNew : txtNewItemName.Visible = isNew
        If isNew Then
            lblNewBuyPrice.Visible = True : txtNewBuyPrice.Visible = True : lblNewBuyPrice.Text = "Buying Price:" : txtNewBuyPrice.ReadOnly = False
            lblNewStockCount.Visible = True : txtNewStockCount.Visible = True : lblNewStockCount.Text = "Initial Stock:" : txtNewStockCount.ReadOnly = False
            If tag1 = 6 Then
                lblNewSellPrice.Visible = True : txtNewSellPrice.Visible = True
                lblNewColor.Visible = False : txtNewColor.Visible = False : lblNewSize.Visible = False : txtNewSize.Visible = False : lblNewStatus.Visible = False : cboNewStatus.Visible = False
            Else
                lblNewSellPrice.Visible = True : txtNewSellPrice.Visible = True : lblNewColor.Visible = True : txtNewColor.Visible = True : lblNewSize.Visible = True : txtNewSize.Visible = True : lblNewStatus.Visible = True : cboNewStatus.Visible = True
            End If
            txtNewItemName.Clear() : txtNewBuyPrice.Clear() : txtNewSellPrice.Clear() : txtNewColor.Clear() : txtNewSize.Clear() : txtNewStockCount.Clear() : cboNewStatus.SelectedIndex = -1
        Else
            lblNewBuyPrice.Visible = False : txtNewBuyPrice.Visible = False : lblNewBuyPrice.Text = "Current Price:" : txtNewBuyPrice.ReadOnly = True
            lblNewStockCount.Visible = False : txtNewStockCount.Visible = False : lblNewStockCount.Text = "Current Stock:" : txtNewStockCount.ReadOnly = True
            lblNewSellPrice.Visible = False : txtNewSellPrice.Visible = False : lblNewColor.Visible = False : txtNewColor.Visible = False : lblNewSize.Visible = False : txtNewSize.Visible = False : lblNewStatus.Visible = False : cboNewStatus.Visible = False
            txtNewBuyPrice.Clear() : txtNewStockCount.Clear()
        End If
    End Sub

    Private Sub cboTarget_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTarget.SelectedIndexChanged
    End Sub
    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtDownPayment.TextChanged
    End Sub
    Private Sub dtpDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpReservationDate.ValueChanged
    End Sub
    Private Sub lblField3_Click(sender As Object, e As EventArgs) Handles lblNewSellPrice.Click
    End Sub

End Class