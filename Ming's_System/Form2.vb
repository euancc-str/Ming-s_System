Imports MySql.Data.MySqlClient
Imports Mysqlx

Public Class MainPanel

    Private currentID As Integer = 0
    Private currentTimestamp As String = ""
    Private selQty As Integer = 0
    Private currentCustID As Integer = 0
    Private currentProdID As Integer = 0
    Private currentOrderDate As String = ""
    Dim controller As New TransactionController
    Dim service As New TransactionService(controller)

    Private Sub PriceOnly_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If
        If e.KeyChar = "."c AndAlso CType(sender, TextBox).Text.IndexOf("."c) > -1 Then
            e.Handled = True
        End If
    End Sub

    Private Sub WholeNumberOnly_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub showReports()
        btnSave.Visible = False : btnClear.Visible = False : btnDelete.Visible = False
        dgvData.Visible = False

        If txtSearch IsNot Nothing Then txtSearch.Visible = True
        txtField1.Visible = False : txtField2.Visible = False : txtField3.Visible = False
        txtField5.Visible = False : txtField6.Visible = False : txtField7.Visible = False
        cbBox1.Visible = False
        lblField1.Visible = False : lblField2.Visible = False : lblField3.Visible = False
        lblField4.Visible = False : lblField5.Visible = False : lblField6.Visible = False
        lblField7.Visible = False
        lblSeries.Visible = False : cboSeries.Visible = False

        lblField.Text = "SELECT REPORT:"
        cboReport.Items.Clear()
        cboReport.Items.Add("0 - Executive Business Snapshot")
        cboReport.Items.Add("1 - Branch Inventory Overview")
        cboReport.Items.Add("2 - Sales Performance by Customer")
        cboReport.Items.Add("3 - Supplier Restock History")
        cboReport.Items.Add("4 - Low Stock Alert (Stock <= 5)")
        cboReport.Items.Add("5 - Full Supply Chain Trace")

        Label1.Location = New Point(40, 148)
        Label1.Visible = True

        If txtSearch IsNot Nothing Then
            txtSearch.Location = New Point(Label1.Location.X + Label1.Width + 10, 148)
            txtSearch.Visible = True
        End If

        cboReport.Location = New Point(txtSearch.Location.X + txtSearch.Width + 50, 148)
        cboReport.Visible = True

        btnRunReport.Location = New Point(cboReport.Location.X + cboReport.Width + 10, 148)
        btnRunReport.Visible = True
        dgvReport.Visible = True

        ' Auto-Run the Dashboard
        cboReport.SelectedIndex = 0
        btnRunReport.PerformClick()
        Return
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        If cboReport IsNot Nothing Then cboReport.Visible = False
        If btnRunReport IsNot Nothing Then btnRunReport.Visible = False
        If dgvReport IsNot Nothing Then dgvReport.Visible = False
        dgvData.Visible = True
        btnDelete.Enabled = False
        btnSave.Text = "Save Record"

        cboSeries.Items.Clear()
        cboSeries.Items.Add("None")
        readquery("SELECT series_name FROM series ORDER BY series_name")
        While cmdread.HasRows AndAlso cmdread.Read()
            cboSeries.Items.Add(cmdread("series_name").ToString())
        End While

        If operation = 5 Then
            showReports()
            Return
        End If

        btnSave.Visible = True : btnClear.Visible = True : btnDelete.Visible = True
        If txtSearch IsNot Nothing Then txtSearch.Visible = True

        If choice = 1 Then
            lblField.Text = "PRODUCT INFORMATION"
            lblField1.Text = "Item Name:" : lblField2.Text = "Buying Price:" : lblField3.Text = "Selling Price:"
            lblField4.Text = "Status:" : lblField5.Text = "Color:" : lblField6.Text = "Size:" : lblField7.Text = "Stock Count:"
            lblField4.Visible = True : cbBox1.Visible = True : lblField5.Visible = True : txtField5.Visible = True
            lblField6.Visible = True : txtField6.Visible = True : lblField7.Visible = True : txtField7.Visible = True
            lblSeries.Visible = True : cboSeries.Visible = True
        ElseIf choice = 2 Then
            lblField.Text = "SUPPLIER INFORMATION"
            lblField1.Text = "Company Name:" : lblField2.Text = "Contact Person:" : lblField3.Text = "Country of Origin:"
            lblField4.Visible = False : cbBox1.Visible = False : lblField5.Visible = False : txtField5.Visible = False
            lblField6.Visible = False : txtField6.Visible = False : lblField7.Visible = False : txtField7.Visible = False
            lblSeries.Visible = False : cboSeries.Visible = False
        ElseIf choice = 3 Then
            lblField.Text = "CUSTOMER INFORMATION"
            lblField1.Text = "Customer Name:" : lblField2.Text = "Address:"
            lblField3.Visible = False : txtField3.Visible = False : lblField4.Visible = False : cbBox1.Visible = False
            lblField5.Visible = False : txtField5.Visible = False : lblField6.Visible = False : txtField6.Visible = False
            lblField7.Visible = False : txtField7.Visible = False
            lblSeries.Visible = False : cboSeries.Visible = False
        ElseIf choice = 4 Then
            lblField.Text = "EMPLOYEE INFORMATION"
            lblField1.Text = "Employee Name:" : lblField2.Text = "Role:" : lblField3.Text = "Email Address:"
            lblField3.Visible = True : txtField3.Visible = True : lblField4.Visible = False : cbBox1.Visible = False
            lblField5.Visible = False : txtField5.Visible = False : lblField6.Visible = False : txtField6.Visible = False
            lblField7.Visible = False : txtField7.Visible = False
            lblSeries.Visible = False : cboSeries.Visible = False
        ElseIf choice = 5 Then
            lblField.Text = "COURIER INFORMATION"
            lblField1.Text = "Company Name:" : lblField2.Text = "Address:" : lblField3.Text = "Contact Number:"
            lblField3.Visible = True : txtField3.Visible = True : lblField4.Visible = False : cbBox1.Visible = False
            lblField5.Visible = False : txtField5.Visible = False : lblField6.Visible = False : txtField6.Visible = False
            lblField7.Visible = False : txtField7.Visible = False
            lblSeries.Visible = False : cboSeries.Visible = False
        ElseIf choice = 6 Then
            lblField.Text = "SERIES INFORMATION"
            lblField1.Text = "Series Name:" : lblField2.Text = "Manufacturer:" : lblField3.Text = "Release Year:"
            lblField5.Text = "Total in Set:"
            lblField3.Visible = True : txtField3.Visible = True : lblField4.Visible = False : cbBox1.Visible = False
            lblField5.Visible = True : txtField5.Visible = True : lblField6.Visible = False : txtField6.Visible = False
            lblField7.Visible = False : txtField7.Visible = False
            lblSeries.Visible = False : cboSeries.Visible = False
        ElseIf choice = 7 Then
            lblField.Text = "BRANCH INFORMATION"
            lblField1.Text = "Branch Name:" : lblField2.Text = "Address:" : lblField3.Text = "Operating Hours:"
            lblField3.Visible = True : txtField3.Visible = True : lblField4.Visible = False : cbBox1.Visible = False
            lblField5.Visible = False : txtField5.Visible = False : lblField6.Visible = False : txtField6.Visible = False
            lblField7.Visible = False : txtField7.Visible = False
            lblSeries.Visible = False : cboSeries.Visible = False
        ElseIf choice = 8 Then
            lblField.Text = "ORDER MANAGER"
            lblField1.Text = "Customer:" : lblField2.Text = "Product Ordered:" : lblField3.Text = "Order Date:"
            lblField1.Visible = True : txtField1.Visible = True : txtField1.ReadOnly = True
            lblField2.Visible = True : txtField2.Visible = True : txtField2.ReadOnly = True
            lblField3.Visible = True : txtField3.Visible = True : txtField3.ReadOnly = True
            lblField4.Visible = False : cbBox1.Visible = False
            lblField5.Visible = False : txtField5.Visible = False
            lblField6.Visible = False : txtField6.Visible = False
            lblField7.Visible = False : txtField7.Visible = False
            lblSeries.Visible = False : cboSeries.Visible = False
            btnSave.Text = "✔ Mark Delivered"
            btnClear.Visible = True
        ElseIf choice = 9 Then
            lblField.Text = "BULK BRANCH RETRIEVAL"
            cboReport.Items.Clear()
            readquery("SELECT branch_name FROM branch ORDER BY branch_name")
            While cmdread.Read()
                cboReport.Items.Add(cmdread("branch_name").ToString())
            End While
            cboReport.Text = "Select Branch to Empty..."


            setcontrolsvisible(False, lblField1, txtField1, lblField2, txtField2, lblField3, txtField3,
                               lblField4, cbBox1, lblField5, txtField5, lblField6, txtField6,
                               lblField7, txtField7, lblSeries, cboSeries)

            lblField2.Visible = False
            lblField3.Visible = False

            cboReport.Visible = True
            cboReport.Text = "Select Branch to Empty..."


            btnClear.Text = "Run Bulk Retrieval"
            btnClear.Visible = True
            btnSave.Visible = False
            btnDelete.Visible = False

            dgvReport.Visible = True
        End If
        LoadGridData()
    End Sub
    Private Sub LoadGridData()
        Dim sql As String = ""
        Dim searchVal As String = ""

        If txtSearch IsNot Nothing Then
            searchVal = txtSearch.Text.Trim().Replace("'", "''")
        End If

        Try

            sql = generateSearchQuery(searchVal)
            dgvData.DataSource = getDataTable(sql)
            dgvData.AutoResizeColumns()
            If dgvData.Rows.Count = 1 Then
                Dim row As DataGridViewRow = dgvData.Rows(0)
                currentID = Val(row.Cells("ID").Value.ToString())
                LoadRecordDetails(currentID)
                btnDelete.Enabled = True
                If choice <> 9 Then
                    btnSave.Text = "Update Record"
                End If
            End If
        Catch ex As Exception
            MsgBox("Error loading grid: " & ex.Message)
        End Try
    End Sub
    Private Sub setcontrolsvisible(isVisible As Boolean, ParamArray controls() As Control)
        For Each ctrl As Control In controls
            ctrl.Visible = isVisible
        Next
    End Sub
    Private Function generateSearchQuery(searchVal As String) As String
        Dim sql As String = ""
        If choice = 1 OrElse choice = 9 Then
            sql = "SELECT p.product_id AS ID, p.item_name AS Name, p.buying_price AS 'Buy Price', p.selling_price AS 'Sell Price', p.color AS Color, p.size AS Size, p.status AS Status, p.stock_count AS Stock, s.series_name AS Series " &
                  "FROM product p LEFT JOIN series s ON p.series_id = s.series_id"
            If searchVal <> "" Then sql &= " WHERE p.item_name LIKE '%" & searchVal & "%'"
        ElseIf choice = 2 Then
            sql = "SELECT supplier_id AS ID, company_name AS Company, contact_person AS Contact FROM supplier"
            If searchVal <> "" Then sql &= " WHERE company_name LIKE '%" & searchVal & "%'"
        ElseIf choice = 3 Then
            sql = "SELECT customer_id AS ID, customer_name AS Name, address AS Address FROM customer"
            If searchVal <> "" Then sql &= " WHERE customer_name LIKE '%" & searchVal & "%'"
        ElseIf choice = 4 Then
            sql = "SELECT employee_id AS ID, employee_name AS Name, role AS Role FROM employee"
            If searchVal <> "" Then sql &= " WHERE employee_name LIKE '%" & searchVal & "%'"
        ElseIf choice = 5 Then
            sql = "SELECT courier_id AS ID, company_name AS Company, contact_number AS Contact FROM courier"
            If searchVal <> "" Then sql &= " WHERE company_name LIKE '%" & searchVal & "%'"
        ElseIf choice = 6 Then
            sql = "SELECT series_id AS ID, series_name AS Name, release_year AS Year FROM series"
            If searchVal <> "" Then sql &= " WHERE series_name LIKE '%" & searchVal & "%'"
        ElseIf choice = 7 Then
            sql = "SELECT branch_id AS ID, branch_name AS Name, address AS Address, operating_hours AS 'Operating Hours' FROM branch"
            If searchVal <> "" Then sql &= " WHERE branch_name LIKE '%" & searchVal & "%'"
        ElseIf choice = 8 Then
            sql = "SELECT pu.customer_id AS CustID, pu.product_id AS ProdID, " &
                  "c.customer_name AS Customer, p.item_name AS Product, " &
                  "pu.quantity AS Qty, pu.reservation_date AS 'Order Date', " &
                  "pu.status AS Status " &
                  "FROM purchases pu " &
                  "INNER JOIN customer c ON pu.customer_id = c.customer_id " &
                  "INNER JOIN product p ON pu.product_id = p.product_id"
            If searchVal <> "" Then
                sql &= " WHERE (c.customer_name LIKE '%" & searchVal & "%' OR p.item_name LIKE '%" & searchVal & "%')"
            End If
            sql &= " ORDER BY pu.reservation_date DESC"
        End If
        Return sql
    End Function



    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs)
        If operation = 5 Then
            If cboReport.Text <> "" Then btnRunReport.PerformClick
        Else
            LoadGridData()
        End If
    End Sub

    Private Sub dgvData_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Dim row = dgvData.Rows(e.RowIndex)
            If choice = 8 Then
                currentCustID = Val(row.Cells("CustID").Value.ToString)
                currentProdID = Val(row.Cells("ProdID").Value.ToString)
                selQty = Val(row.Cells("Qty").Value.ToString)
                Dim rawDate = row.Cells("Order Date").Value.ToString
                Dim parsedDate As Date
                If Date.TryParse(rawDate, parsedDate) Then

                    currentOrderDate = parsedDate.ToString("yyyy-MM-dd HH:mm:ss")


                    txtField3.Text = parsedDate.ToString("yyyy-MM-dd HH:mm:ss")
                Else
                    currentOrderDate = rawDate ' Failsafe
                    txtField3.Text = rawDate
                End If

                txtField1.Text = row.Cells("Customer").Value.ToString
                txtField2.Text = row.Cells("Product").Value.ToString
                txtField3.Text = currentOrderDate

                Dim status = row.Cells("Status").Value.ToString
                If status = "Pending" Then
                    btnSave.Enabled = True : btnSave.Text = "✔ Mark Delivered"
                    btnDelete.Enabled = True : btnDelete.Text = "Cancel Order"
                ElseIf status = "Delivered" Then
                    btnSave.Enabled = False : btnSave.Text = "Already Delivered"
                    btnDelete.Enabled = True : btnDelete.Text = "Process Return"
                Else
                    btnSave.Enabled = False : btnSave.Text = status
                    btnDelete.Enabled = False : btnDelete.Text = "Locked"
                End If
                Return
            End If

            currentID = Val(row.Cells("ID").Value.ToString)
            LoadRecordDetails(currentID)
            btnDelete.Enabled = True
            btnSave.Text = "Update Record"
        End If
    End Sub

    Private Function returnQuery(id As Integer) As String
        Dim str As String = ""
        If choice = 1 OrElse choice = 9 Then str = "SELECT p.*, s.series_name FROM product p LEFT JOIN series s ON p.series_id = s.series_id WHERE p.product_id = " & id
        If choice = 2 Then str = "SELECT * FROM supplier WHERE supplier_id = " & id
        If choice = 3 Then str = "SELECT * FROM customer WHERE customer_id = " & id
        If choice = 4 Then str = "SELECT * FROM employee WHERE employee_id = " & id
        If choice = 5 Then str = "SELECT * FROM courier WHERE courier_id = " & id
        If choice = 6 Then str = "SELECT * FROM series WHERE series_id = " & id
        If choice = 7 Then str = "SELECT * FROM branch WHERE branch_id = " & id
        Return str

    End Function
    Private Sub LoadRecordDetails(id As Integer)

        Dim str As String = returnQuery(id)
        Try
            readquery(str)
            If cmdread.Read() Then
                If choice = 1 Then
                    txtField1.Text = cmdread("item_name").ToString()
                    txtField2.Text = cmdread("buying_price").ToString()
                    txtField3.Text = cmdread("selling_price").ToString()
                    cbBox1.Text = cmdread("status").ToString()
                    txtField5.Text = cmdread("color").ToString()
                    txtField6.Text = cmdread("size").ToString()
                    txtField7.Text = cmdread("stock_count").ToString()
                    If IsDBNull(cmdread("series_name")) OrElse cmdread("series_name").ToString() = "" Then
                        cboSeries.Text = "None"
                    Else
                        cboSeries.Text = cmdread("series_name").ToString()
                    End If
                ElseIf choice = 2 Then
                    txtField1.Text = cmdread("company_name").ToString()
                    txtField2.Text = cmdread("contact_person").ToString()
                    txtField3.Text = cmdread("country_origin").ToString()
                ElseIf choice = 3 Then
                    txtField1.Text = cmdread("customer_name").ToString()
                    txtField2.Text = cmdread("address").ToString()
                ElseIf choice = 4 Then
                    txtField1.Text = cmdread("employee_name").ToString()
                    txtField2.Text = cmdread("role").ToString()
                    txtField3.Text = cmdread("email_address").ToString()
                ElseIf choice = 5 Then
                    txtField1.Text = cmdread("company_name").ToString()
                    txtField2.Text = cmdread("address").ToString()
                    txtField3.Text = cmdread("contact_number").ToString()
                ElseIf choice = 6 Then
                    txtField1.Text = cmdread("series_name").ToString()
                    txtField2.Text = cmdread("manufacturer").ToString()
                    txtField3.Text = cmdread("release_year").ToString()
                    txtField5.Text = cmdread("total_in_set").ToString()
                ElseIf choice = 7 Then
                    txtField1.Text = cmdread("branch_name").ToString()
                    txtField2.Text = cmdread("address").ToString()
                    txtField3.Text = cmdread("operating_hours").ToString()
                End If
            End If
        Catch ex As Exception
            MsgBox("Error loading details: " & ex.Message)
        End Try
    End Sub

    Private Function isSellingPriceValid() As Boolean
        If txtField2.Text.Trim() = "" OrElse txtField3.Text.Trim() = "" OrElse txtField5.Text.Trim() = "" OrElse txtField6.Text.Trim() = "" OrElse txtField7.Text.Trim() = "" OrElse cbBox1.Text.Trim() = "" Then
            MsgBox("Please completely fill out all product details.", MsgBoxStyle.Exclamation)
            Return False
        End If
        Dim bPrice, sPrice As Double
        Dim stock As Integer
        If Not Double.TryParse(txtField2.Text.Trim(), bPrice) Then MsgBox("Buying price must be a valid number.", MsgBoxStyle.Exclamation) : Return False
        If Not Double.TryParse(txtField3.Text.Trim(), sPrice) Then MsgBox("Selling price must be a valid number.", MsgBoxStyle.Exclamation) : Return False
        If Not Integer.TryParse(txtField7.Text.Trim(), stock) Then MsgBox("Stock count must be a whole number.", MsgBoxStyle.Exclamation) : Return False

        If bPrice < 0 OrElse sPrice < 0 Then
            MsgBox("Prices cannot be negative.", MsgBoxStyle.Exclamation, "Invalid Entry") : Return False
        End If
        If sPrice <= bPrice Then
            Dim warn = MsgBox("The Selling Price is lower than or equal to the Buying Price. This will result in a loss. Proceed anyway?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Profit Margin Warning")
            If warn = MsgBoxResult.No Then Return False
        End If
        Return True
    End Function

    Private Sub processOrderDelivery()
        Try
            Dim checkFinance As String = "SELECT pu.down_payment, pu.shipping_fee, pu.quantity, p.selling_price " &
                                         "FROM purchases pu INNER JOIN product p ON pu.product_id = p.product_id " &
                                         "WHERE pu.customer_id = " & currentCustID & " AND pu.product_id = " & currentProdID &
                                         " AND pu.reservation_date = '" & currentOrderDate & "'"
            readquery(checkFinance)

            If cmdread.HasRows Then
                cmdread.Read()
                Dim dp As Double = Val(cmdread("down_payment").ToString())
                Dim sf As Double = Val(cmdread("shipping_fee").ToString())
                Dim qty As Integer = Val(cmdread("quantity").ToString())
                Dim sp As Double = Val(cmdread("selling_price").ToString())

                Dim grandTotal As Double = (sp * qty) + sf

                If dp < grandTotal Then
                    Dim overrideAns = MsgBox("WARNING: This order has an unpaid balance!" & vbCrLf &
                           "System Paid: ₱" & dp & vbCrLf & "Grand Total: ₱" & grandTotal & vbCrLf & vbCrLf &
                           "Did the customer pay the remaining balance directly? Click YES to override and force delivery.", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Balance Due Warning")


                    If overrideAns = MsgBoxResult.No Then
                        Return
                    End If
                End If
            End If

            If MsgBox("Confirm marking this order as Delivered?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                readquery("UPDATE purchases SET status = 'Delivered' WHERE customer_id = " & currentCustID &
                          " AND product_id = " & currentProdID & " AND reservation_date = '" & currentOrderDate & "'")
                MsgBox("Order fulfilled!")
                LoadGridData() : btnClear.PerformClick()
            End If
        Catch ex As Exception
            MsgBox("Error updating order: " & ex.Message)
        End Try
    End Sub



    Private Function handleDuplicateProduct() As Boolean
        Dim ans = MsgBox("This product already exists. Do you want to add the stock count (" & txtField7.Text.Trim() & ") to the existing inventory instead of creating a duplicate?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Duplicate Found")

        If ans = MsgBoxResult.Yes Then
            Dim stockToAdd As Integer = Integer.Parse(txtField7.Text.Trim())
            readquery("UPDATE product SET stock_count = COALESCE(stock_count, 0) + " & stockToAdd & " WHERE item_name = '" & txtField1.Text.Trim() & "'")
            MsgBox("Stock count successfully updated for the existing product!", MsgBoxStyle.Information)
            LoadGridData()
            btnClear.PerformClick()
            Return True
        End If
        Return False
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs)
        If txtField1.Text.Trim = "" Then
            MsgBox("Please enter a valid name in the first field.", MsgBoxStyle.Exclamation)
            Return
        End If

        If choice = 1 Then
            If Not isSellingPriceValid() Then Return
        ElseIf choice = 2 Then
            If txtField2.Text.Trim = "" OrElse txtField3.Text.Trim = "" Then MsgBox("Please completely fill out all supplier details.", MsgBoxStyle.Exclamation) : Return
        ElseIf choice = 3 Then
            If txtField2.Text.Trim = "" Then MsgBox("Please fill in the customer address.", MsgBoxStyle.Exclamation) : Return
        ElseIf choice = 4 Then
            If txtField2.Text.Trim = "" OrElse txtField3.Text.Trim = "" Then MsgBox("Please completely fill out all employee details.", MsgBoxStyle.Exclamation) : Return
        ElseIf choice = 5 Then
            If txtField2.Text.Trim = "" OrElse txtField3.Text.Trim = "" Then MsgBox("Please completely fill out all courier details.", MsgBoxStyle.Exclamation) : Return
        ElseIf choice = 6 Then
            If txtField2.Text.Trim = "" OrElse txtField3.Text.Trim = "" OrElse txtField5.Text.Trim = "" Then MsgBox("Please completely fill out all series details.", MsgBoxStyle.Exclamation) : Return
            Dim rYear, tSet As Integer
            If Not Integer.TryParse(txtField3.Text.Trim, rYear) Then MsgBox("Release year must be a valid number.", MsgBoxStyle.Exclamation) : Return
            If Not Integer.TryParse(txtField5.Text.Trim, tSet) Then MsgBox("Total in set must be a whole number.", MsgBoxStyle.Exclamation) : Return
        ElseIf choice = 7 Then
            If txtField2.Text.Trim = "" OrElse txtField3.Text.Trim = "" Then MsgBox("Please completely fill out all branch details.", MsgBoxStyle.Exclamation) : Return
        End If

        If choice = 8 Then
            processOrderDelivery()
            Return
        End If

        Dim str = ""
        Dim seriesSQL = "NULL"
        If choice = 1 AndAlso cboSeries.Text.Trim <> "" AndAlso cboSeries.Text <> "None" Then
            seriesSQL = "(SELECT series_id FROM series WHERE series_name='" & cboSeries.Text.Trim & "')"
        End If

        Try
            If currentID = 0 Then

                entityIsExisting()

                If cmdread.HasRows Then
                    If choice = 1 Then
                        If handleDuplicateProduct() Then Return
                    Else
                        MsgBox("This record already exists. Please alter the name to create a unique record.", MsgBoxStyle.Exclamation)
                        Return
                    End If
                End If

                'Insert
                str = generateInsertQuery(seriesSQL)
                readquery(str)
                MsgBox("New Record Successfully Added!", MsgBoxStyle.Information)

            Else
                str = generateUpdateQuery(seriesSQL)
                readquery(str)
                MsgBox("Record Successfully Updated!", MsgBoxStyle.Information)
            End If

            LoadGridData()
            btnClear.PerformClick


        Catch ex As Exception
            MsgBox("Error saving record: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub entityIsExisting()

        Dim checkQuery As String = ""
        If choice = 1 Then checkQuery = "SELECT product_id FROM product WHERE item_name = '" & txtField1.Text.Trim() & "' AND color = '" & txtField5.Text.Trim() & "' AND size = '" & txtField6.Text.Trim() & "'"
        If choice = 2 Then checkQuery = "SELECT supplier_id FROM supplier WHERE company_name = '" & txtField1.Text.Trim() & "'"
        If choice = 3 Then checkQuery = "SELECT customer_id FROM customer WHERE customer_name = '" & txtField1.Text.Trim() & "'"
        If choice = 4 Then checkQuery = "SELECT employee_id FROM employee WHERE employee_name = '" & txtField1.Text.Trim() & "'"
        If choice = 5 Then checkQuery = "SELECT courier_id FROM courier WHERE company_name = '" & txtField1.Text.Trim() & "'"
        If choice = 6 Then checkQuery = "SELECT series_id FROM series WHERE series_name = '" & txtField1.Text.Trim() & "'"

        readquery(checkQuery)
    End Sub
    Private Function generateInsertQuery(seriesSql As String) As String
        If choice = 1 Then Return "INSERT INTO product (item_name, buying_price, selling_price, status, color, size, stock_count, series_id) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "', '" & txtField3.Text.Trim() & "', '" & cbBox1.Text.Trim() & "', '" & txtField5.Text.Trim() & "', '" & txtField6.Text.Trim() & "', '" & txtField7.Text.Trim() & "', " & seriesSql & ")"
        If choice = 2 Then Return "INSERT INTO supplier (company_name, contact_person, country_origin) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "', '" & txtField3.Text.Trim() & "')"
        If choice = 3 Then Return "INSERT INTO customer (customer_name, address) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "')"
        If choice = 4 Then Return "INSERT INTO employee (employee_name, role, email_address) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "', '" & txtField3.Text.Trim() & "')"
        If choice = 5 Then Return "INSERT INTO courier (company_name, address, contact_number) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "', '" & txtField3.Text.Trim() & "')"
        If choice = 6 Then Return "INSERT INTO series (series_name, manufacturer, release_year, total_in_set) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "', '" & txtField3.Text.Trim() & "', '" & txtField5.Text.Trim() & "')"
        If choice = 7 Then Return "INSERT INTO branch (branch_name, address, operating_hours) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "', '" & txtField3.Text.Trim() & "')"
        Return ""
    End Function
    Private Function generateUpdateQuery(seriesSql As String) As String
        If choice = 1 Then Return "UPDATE product SET item_name='" & txtField1.Text.Trim() & "', buying_price='" & txtField2.Text.Trim() & "', selling_price='" & txtField3.Text.Trim() & "', status='" & cbBox1.Text.Trim() & "', color='" & txtField5.Text.Trim() & "', size='" & txtField6.Text.Trim() & "', stock_count='" & txtField7.Text.Trim() & "', series_id=" & seriesSql & " WHERE product_id=" & currentID
        If choice = 2 Then Return "UPDATE supplier SET company_name='" & txtField1.Text.Trim() & "', contact_person='" & txtField2.Text.Trim() & "', country_origin='" & txtField3.Text.Trim() & "' WHERE supplier_id=" & currentID
        If choice = 3 Then Return "UPDATE customer SET customer_name='" & txtField1.Text.Trim() & "', address='" & txtField2.Text.Trim() & "' WHERE customer_id=" & currentID
        If choice = 4 Then Return "UPDATE employee SET employee_name='" & txtField1.Text.Trim() & "', role='" & txtField2.Text.Trim() & "', email_address='" & txtField3.Text.Trim() & "' WHERE employee_id=" & currentID
        If choice = 5 Then Return "UPDATE courier SET company_name='" & txtField1.Text.Trim() & "', address='" & txtField2.Text.Trim() & "', contact_number='" & txtField3.Text.Trim() & "' WHERE courier_id=" & currentID
        If choice = 6 Then Return "UPDATE series SET series_name='" & txtField1.Text.Trim() & "', manufacturer='" & txtField2.Text.Trim() & "', release_year='" & txtField3.Text.Trim() & "', total_in_set='" & txtField5.Text.Trim() & "' WHERE series_id=" & currentID
        If choice = 7 Then Return "UPDATE branch SET branch_name='" & txtField1.Text.Trim() & "', address='" & txtField2.Text.Trim() & "', operating_hours='" & txtField3.Text.Trim() & "' WHERE branch_id=" & currentID
        Return ""
    End Function
    Private Sub btnClear_Click(sender As Object, e As EventArgs)
        txtField1.Clear : txtField2.Clear : txtField3.Clear
        txtField5.Clear : txtField6.Clear : txtField7.Clear
        txtField1.ReadOnly = False : txtField2.ReadOnly = False : txtField3.ReadOnly = False
        currentID = 0 : currentTimestamp = ""
        currentCustID = 0 : currentProdID = 0 : currentOrderDate = ""
        btnDelete.Enabled = False
        btnSave.Text = If(choice = 8, "✔ Mark Delivered", "Save Record")
        If choice = 8 Then btnSave.Enabled = False
        If choice = 9 Then
            Dim ans = MsgBox($"Are you sure you want to retrieve ALL stock from {cboReport.Text} back to the Main Warehouse?",
                             MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Confirm Bulk Action")

            If ans = MsgBoxResult.Yes Then
                Dim res = service.ProcessBulkRetrieval(cboReport.Text.Trim)
                If res.Success Then
                    MsgBox(res.Message, MsgBoxStyle.Information)

                    LoadGridData()
                    dgvReport.DataSource = Nothing
                    cboReport.Text = "Select Branch to Empty..."
                Else
                    MsgBox(res.Message, MsgBoxStyle.Critical)
                End If
            End If
            Return
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        If choice = 8 Then
            If currentCustID = 0 Then Return

            Dim action = ""
            Dim newStatus = ""
            If btnDelete.Text = "Cancel Order" Then
                action = "Cancel this pending order"
                newStatus = "Cancelled"
            ElseIf btnDelete.Text = "Process Return" Then
                action = "Process a RETURN for this delivered order"
                newStatus = "Returned"
            Else
                Return
            End If

            If MsgBox(action & " and return " & selQty & " unit(s) to the Main Warehouse?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Confirm Action") = MsgBoxResult.Yes Then
                Try
                    readquery("START TRANSACTION")


                    readquery("UPDATE purchases SET status = '" & newStatus & "' " &
                              "WHERE customer_id = " & currentCustID & " AND product_id = " & currentProdID &
                              " AND reservation_date = '" & currentOrderDate & "'")

                    readquery("UPDATE product SET stock_count = stock_count + " & selQty &
                              " WHERE item_name = '" & txtField2.Text.Trim.Replace("'", "''") & "'")

                    readquery("COMMIT")

                    MsgBox("Success! Order marked as " & newStatus & " and stock restored.", MsgBoxStyle.Information)
                    LoadGridData()
                    btnClear.PerformClick
                Catch ex As Exception
                    readquery("ROLLBACK")
                    MsgBox("Error updating order: " & ex.Message, MsgBoxStyle.Critical)
                End Try
            End If
            Return
        End If
        If currentID = 0 Then Return

        Dim ask = MsgBox("Are you sure you want to permanently delete " & txtField1.Text & "?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Confirm Delete")

        If ask = MsgBoxResult.Yes Then
            Dim str As String = deleteEntity()

            Try
                readquery(str)
                MsgBox("Record deleted successfully!")
                LoadGridData()
                btnClear.PerformClick
            Catch ex As Exception
                MsgBox("Error deleting: " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub

    Private Function deleteEntity()
        Dim str As String = ""
        If choice = 1 Then str = "DELETE FROM product WHERE product_id = " & currentID
        If choice = 2 Then str = "DELETE FROM supplier WHERE supplier_id = " & currentID
        If choice = 3 Then str = "DELETE FROM customer WHERE customer_id = " & currentID
        If choice = 4 Then str = "DELETE FROM employee WHERE employee_id = " & currentID
        If choice = 5 Then str = "DELETE FROM courier WHERE courier_id = " & currentID
        If choice = 6 Then str = "DELETE FROM series WHERE series_id = " & currentID
        If choice = 7 Then str = "DELETE FROM branch WHERE branch_id = " & currentID
        Return str
    End Function
    Private Sub btnRunReport_Click(sender As Object, e As EventArgs)
        If cboReport.Text = "" Then
            MsgBox("Please select a report to run.", MsgBoxStyle.Exclamation)
            Return
        End If

        Dim reportIndex As Integer = Val(cboReport.Text.Substring(0, 1))
        Dim searchVal = ""
        If txtSearch IsNot Nothing Then searchVal = txtSearch.Text.Trim.Replace("'", "''")

        Dim sql = GetReportQuery(reportIndex, searchVal)

        Try
            Dim dt As New DataTable
            dt = getDataTable(sql)
            dgvReport.DataSource = dt

            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            dgvReport.ClearSelection

            lblField.Text = "REPORT: " & cboReport.Text
        Catch ex As Exception
            MsgBox("Report error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Function getDataTable(query As String) As DataTable
        Dim dt As New DataTable()
        Try
            Using localConn As New MySqlConnection(strconn)
                Dim da As New MySqlDataAdapter(query, localConn)
                da.Fill(dt)
            End Using
        Catch ex As Exception
            Throw New Exception("getDataTable failed: " & ex.Message)
        End Try
        Return dt
    End Function

    Private Function GetReportQuery(reportIndex As Integer, searchVal As String) As String
        Dim sql As String = ""

        Select Case reportIndex
            Case 0
                sql = "SELECT 'Total Registered Products' AS 'Business Metric', COUNT(product_id) AS 'Current Value' FROM product " &
                      "UNION ALL " &
                      "SELECT 'Total Items in Main Warehouse', COALESCE(SUM(stock_count), 0) FROM product " &
                      "UNION ALL " &
                      "SELECT 'Total Items in Branches', COALESCE(SUM(quantity), 0) FROM stores " &
                      "UNION ALL " &
                      "SELECT 'Pending Orders (Needs Delivery)', COUNT(*) FROM purchases WHERE status = 'Pending' " &
                      "UNION ALL " &
                      "SELECT 'Total Lifetime Sales Revenue (PHP)', COALESCE(SUM(p.selling_price * pu.quantity), 0) FROM purchases pu INNER JOIN product p ON pu.product_id = p.product_id WHERE pu.status = 'Delivered' " &
                      "UNION ALL " &
                      "SELECT 'Total Branch Locations', COUNT(branch_id) FROM branch"

            Case 1
                sql = "SELECT b.branch_name AS 'Branch', p.item_name AS 'Product', p.stock_count AS 'Stock', " &
                      "p.selling_price AS 'Price (PHP)', s.last_restocked_date AS 'Last Restocked' " &
                      "FROM stores s INNER JOIN branch b ON s.branch_id = b.branch_id " &
                      "INNER JOIN product p ON s.product_id = p.product_id "
                If searchVal <> "" Then
                    sql &= "WHERE b.branch_name LIKE '%" & searchVal & "%' OR p.item_name LIKE '%" & searchVal & "%' "
                End If
                sql &= "ORDER BY b.branch_name, s.last_restocked_date DESC"

            Case 2
                sql = "SELECT c.customer_name AS 'Customer', p.item_name AS 'Product Purchased', " &
                      "pu.quantity AS 'Qty', pu.reservation_date AS 'Order Date', pu.status AS 'Status', " &
                      "p.selling_price AS 'Sell Price', p.buying_price AS 'Buy Price', " &
                      "((p.selling_price - p.buying_price) * pu.quantity) AS 'Total Profit (PHP)', " &
                      "COALESCE(co.company_name, 'Walk-in / Pickup') AS 'Courier Used' " &
                      "FROM purchases pu INNER JOIN customer c ON pu.customer_id = c.customer_id " &
                      "INNER JOIN product p ON pu.product_id = p.product_id " &
                      "LEFT JOIN delivers_to dt ON dt.customer_id = c.customer_id " &
                      "LEFT JOIN courier co ON dt.courier_id = co.courier_id " &
                      "WHERE pu.status != 'Cancelled' "

                If searchVal <> "" Then
                    sql &= "AND (c.customer_name LIKE '%" & searchVal & "%' OR p.item_name LIKE '%" & searchVal & "%' OR pu.status LIKE '%" & searchVal & "%') "
                End If
                sql &= "ORDER BY pu.reservation_date DESC"

            Case 3
                sql = "SELECT sup.company_name AS 'Supplier', sup.country_origin AS 'Origin', " &
                      "p.item_name AS 'Product', pr.supply_date AS 'Supply Date', " &
                      "pr.quantity_supplied AS 'Qty Supplied', pr.supply_price AS 'Unit Cost', " &
                      "(pr.quantity_supplied * pr.supply_price) AS 'Total Cost (PHP)' " &
                      "FROM provides pr INNER JOIN supplier sup ON pr.supplier_id = sup.supplier_id " &
                      "INNER JOIN product p ON pr.product_id = p.product_id "
                If searchVal <> "" Then
                    sql &= "WHERE sup.company_name LIKE '%" & searchVal & "%' OR p.item_name LIKE '%" & searchVal & "%' "
                End If
                sql &= "ORDER BY pr.supply_date DESC"

            Case 4
                sql = "SELECT p.item_name AS 'Product', p.stock_count AS 'Remaining Stock', " &
                      "p.selling_price AS 'Sell Price', sup.company_name AS 'Last Supplier', " &
                      "sup.country_origin AS 'Supplier Origin', pr.supply_date AS 'Last Supply Date' " &
                      "FROM product p LEFT JOIN provides pr ON p.product_id = p.product_id " &
                      "AND pr.supply_date = (SELECT MAX(supply_date) FROM provides WHERE product_id = p.product_id) " &
                      "LEFT JOIN supplier sup ON pr.supplier_id = sup.supplier_id " &
                      "WHERE p.stock_count <= 5 "
                If searchVal <> "" Then
                    sql &= "AND (p.item_name LIKE '%" & searchVal & "%' OR sup.company_name LIKE '%" & searchVal & "%') "
                End If
                sql &= "ORDER BY p.stock_count ASC"

            Case 5
                sql = "SELECT p.item_name AS 'Product', sup.company_name AS 'Supplied By', " &
                      "b.branch_name AS 'Stored At', c.customer_name AS 'Sold To', " &
                      "pu.status AS 'Order Status', co.company_name AS 'Shipped By', " &
                      "dt.delivery_date AS 'Ship Date' " &
                      "FROM product p LEFT JOIN provides pr ON p.product_id = pr.product_id " &
                      "LEFT JOIN supplier sup ON pr.supplier_id = sup.supplier_id " &
                      "LEFT JOIN stores st ON p.product_id = st.product_id " &
                      "LEFT JOIN branch b ON st.branch_id = b.branch_id " &
                      "LEFT JOIN purchases pu ON p.product_id = pu.product_id " &
                      "LEFT JOIN customer c ON pu.customer_id = c.customer_id " &
                      "LEFT JOIN delivers_to dt ON c.customer_id = dt.customer_id " &
                      "LEFT JOIN courier co ON dt.courier_id = co.courier_id "
                If searchVal <> "" Then
                    sql &= "WHERE p.item_name LIKE '%" & searchVal & "%' OR sup.company_name LIKE '%" & searchVal & "%' OR b.branch_name LIKE '%" & searchVal & "%' OR c.customer_name LIKE '%" & searchVal & "%' "
                End If
                sql &= "ORDER BY p.item_name"

            Case 9

                sql = "SELECT p.product_id AS ID, p.item_name AS Name, s.quantity AS 'Branch Qty', " &
                      "p.color AS Color, p.size AS Size " &
                      "FROM stores s " &
                      "INNER JOIN product p ON s.product_id = p.product_id " &
                      "INNER JOIN branch b ON s.branch_id = b.branch_id " &
                      "WHERE b.branch_name = '" & cboReport.Text.Replace("'", "''") & "'"
            Case Else

                Return "SELECT 'Invalid report selected' AS Error"
        End Select

        Return sql
    End Function
    Private Sub cboReport_SelectedIndexChanged(sender As Object, e As EventArgs)

        If choice = 9 Then
            Dim branchSearch = cboReport.Text.Trim

            Dim sql = GetReportQuery(9, "")
            dgvReport.DataSource = getDataTable(sql)
            dgvReport.AutoResizeColumns
        End If
    End Sub

    Private Sub txtField2_TextChanged(sender As Object, e As EventArgs)
    End Sub
    Private Sub dgvReport_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
    End Sub
    Private Sub txtField3_TextChanged(sender As Object, e As EventArgs)
    End Sub

    Private Sub dgvData_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub
End Class