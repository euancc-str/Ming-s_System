Imports MySql.Data.MySqlClient

Public Class MainPanel

    Private currentID As Integer = 0
    Private currentTimestamp As String = ""
    Private selQty As Integer = 0
    Private currentCustID As Integer = 0   ' ← ADD
    Private currentProdID As Integer = 0
    Private Sub PriceOnly_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtField2.KeyPress, txtField3.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> "."c) Then
            e.Handled = True ' Block the keystroke
        End If
        ' Prevent a second decimal point from being typed
        If (e.KeyChar = "."c) AndAlso (CType(sender, TextBox).Text.IndexOf("."c) > -1) Then
            e.Handled = True
        End If
    End Sub

    ' 2. Only allow Whole Numbers (For Stock/Quantity)
    Private Sub WholeNumberOnly_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtField7.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Block the keystroke (No decimals or letters allowed)
        End If
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
            If choice = 1 Then
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

            dgvData.DataSource = getDataTable(sql)
            dgvData.AutoResizeColumns()
        Catch ex As Exception
            MsgBox("Error loading grid: " & ex.Message)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        If operation = 5 Then
            If cboReport.Text <> "" Then btnRunReport.PerformClick()
        Else
            LoadGridData()
        End If
    End Sub

    Private Sub dgvData_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvData.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvData.Rows(e.RowIndex)
            If choice = 8 Then
                currentCustID = Val(row.Cells("CustID").Value.ToString())
                currentProdID = Val(row.Cells("ProdID").Value.ToString())
                selQty = Val(row.Cells("Qty").Value.ToString())

                txtField1.Text = row.Cells("Customer").Value.ToString()
                txtField2.Text = row.Cells("Product").Value.ToString()
                txtField3.Text = row.Cells("Order Date").Value.ToString()

                Dim status As String = row.Cells("Status").Value.ToString()
                If status = "Delivered" OrElse status = "Cancelled" Then
                    btnSave.Enabled = False : btnDelete.Enabled = False
                    MsgBox("This order is already " & status & ".", MsgBoxStyle.Information)
                Else
                    btnSave.Enabled = True : btnDelete.Enabled = True
                End If
                btnSave.Text = "✔ Mark Delivered"
                Return
            End If
            currentID = Val(row.Cells("ID").Value.ToString())

            LoadRecordDetails(currentID)

            btnDelete.Enabled = True
            btnSave.Text = "Update Record"
        End If
    End Sub

    Private Sub LoadRecordDetails(id As Integer)
        Dim str As String = ""
        If choice = 1 Then str = "SELECT p.*, s.series_name FROM product p LEFT JOIN series s ON p.series_id = s.series_id WHERE p.product_id = " & id
        If choice = 2 Then str = "SELECT * FROM supplier WHERE supplier_id = " & id
        If choice = 3 Then str = "SELECT * FROM customer WHERE customer_id = " & id
        If choice = 4 Then str = "SELECT * FROM employee WHERE employee_id = " & id
        If choice = 5 Then str = "SELECT * FROM courier WHERE courier_id = " & id
        If choice = 6 Then str = "SELECT * FROM series WHERE series_id = " & id
        If choice = 7 Then str = "SELECT * FROM branch WHERE branch_id = " & id

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

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtField1.Text.Trim() = "" Then
            MsgBox("Please enter a valid name in the first field.", MsgBoxStyle.Exclamation)
            Return
        End If

        If choice = 1 Then
            If txtField2.Text.Trim() = "" OrElse txtField3.Text.Trim() = "" OrElse txtField5.Text.Trim() = "" OrElse txtField6.Text.Trim() = "" OrElse txtField7.Text.Trim() = "" OrElse cbBox1.Text.Trim() = "" Then
                MsgBox("Please completely fill out all product details.", MsgBoxStyle.Exclamation)
                Return
            End If
            Dim bPrice, sPrice As Double
            Dim stock As Integer
            If Not Double.TryParse(txtField2.Text.Trim(), bPrice) Then MsgBox("Buying price must be a valid number.", MsgBoxStyle.Exclamation) : Return
            If Not Double.TryParse(txtField3.Text.Trim(), sPrice) Then MsgBox("Selling price must be a valid number.", MsgBoxStyle.Exclamation) : Return
            If Not Integer.TryParse(txtField7.Text.Trim(), stock) Then MsgBox("Stock count must be a whole number.", MsgBoxStyle.Exclamation) : Return


            If bPrice < 0 OrElse sPrice < 0 Then
                MsgBox("Prices cannot be negative.", MsgBoxStyle.Exclamation, "Invalid Entry") : Return
            End If
            If sPrice <= bPrice Then
                Dim warn = MsgBox("The Selling Price is lower than or equal to the Buying Price. This will result in a loss. Proceed anyway?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Profit Margin Warning")
                If warn = MsgBoxResult.No Then Return
            End If
        ElseIf choice = 2 Then
            If txtField2.Text.Trim() = "" OrElse txtField3.Text.Trim() = "" Then MsgBox("Please completely fill out all supplier details.", MsgBoxStyle.Exclamation) : Return
        ElseIf choice = 3 Then
            If txtField2.Text.Trim() = "" Then MsgBox("Please fill in the customer address.", MsgBoxStyle.Exclamation) : Return
        ElseIf choice = 4 Then
            If txtField2.Text.Trim() = "" OrElse txtField3.Text.Trim() = "" Then MsgBox("Please completely fill out all employee details.", MsgBoxStyle.Exclamation) : Return
        ElseIf choice = 5 Then
            If txtField2.Text.Trim() = "" OrElse txtField3.Text.Trim() = "" Then MsgBox("Please completely fill out all courier details.", MsgBoxStyle.Exclamation) : Return
        ElseIf choice = 6 Then
            If txtField2.Text.Trim() = "" OrElse txtField3.Text.Trim() = "" OrElse txtField5.Text.Trim() = "" Then MsgBox("Please completely fill out all series details.", MsgBoxStyle.Exclamation) : Return
            Dim rYear, tSet As Integer
            If Not Integer.TryParse(txtField3.Text.Trim(), rYear) Then MsgBox("Release year must be a valid number.", MsgBoxStyle.Exclamation) : Return
            If Not Integer.TryParse(txtField5.Text.Trim(), tSet) Then MsgBox("Total in set must be a whole number.", MsgBoxStyle.Exclamation) : Return
        ElseIf choice = 7 Then
            If txtField2.Text.Trim() = "" OrElse txtField3.Text.Trim() = "" Then MsgBox("Please completely fill out all branch details.", MsgBoxStyle.Exclamation) : Return

        End If
        If choice = 8 Then
            If currentCustID = 0 Then MsgBox("Please select an order first.") : Return
            If MsgBox("Mark as Delivered?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Try
                    readquery("UPDATE purchases SET status = 'Delivered' " &
                      "WHERE customer_id = " & currentCustID & " AND product_id = " & currentProdID)
                    MsgBox("Order fulfilled!")
                    LoadGridData() : btnClear.PerformClick()
                Catch ex As Exception
                    MsgBox("Error: " & ex.Message)
                End Try
            End If
            Return
        End If
        Dim str As String = ""
        Dim seriesSQL As String = "NULL"
        If choice = 1 AndAlso cboSeries.Text.Trim() <> "" AndAlso cboSeries.Text <> "None" Then
            seriesSQL = "(SELECT series_id FROM series WHERE series_name='" & cboSeries.Text.Trim() & "')"
        End If

        Try
            If currentID = 0 Then

                Dim checkQuery As String = ""
                If choice = 1 Then checkQuery = "SELECT product_id FROM product WHERE item_name = '" & txtField1.Text.Trim() & "' AND color = '" & txtField5.Text.Trim() & "' AND size = '" & txtField6.Text.Trim() & "'"
                If choice = 2 Then checkQuery = "SELECT supplier_id FROM supplier WHERE company_name = '" & txtField1.Text.Trim() & "'"
                If choice = 3 Then checkQuery = "SELECT customer_id FROM customer WHERE customer_name = '" & txtField1.Text.Trim() & "'"
                If choice = 4 Then checkQuery = "SELECT employee_id FROM employee WHERE employee_name = '" & txtField1.Text.Trim() & "'"
                If choice = 5 Then checkQuery = "SELECT courier_id FROM courier WHERE company_name = '" & txtField1.Text.Trim() & "'"
                If choice = 6 Then checkQuery = "SELECT series_id FROM series WHERE series_name = '" & txtField1.Text.Trim() & "'"

                readquery(checkQuery)

                If cmdread.HasRows Then
                    If choice = 1 Then
                        Dim ans = MsgBox("This product already exists. Do you want to add the stock count (" & txtField7.Text.Trim() & ") to the existing inventory instead of creating a duplicate?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Duplicate Found")

                        If ans = MsgBoxResult.Yes Then
                            Dim stockToAdd As Integer = Integer.Parse(txtField7.Text.Trim())
                            readquery("UPDATE product SET stock_count = COALESCE(stock_count, 0) + " & stockToAdd & " WHERE item_name = '" & txtField1.Text.Trim() & "'")
                            MsgBox("Stock count successfully updated for the existing product!", MsgBoxStyle.Information)
                            LoadGridData()
                            btnClear.PerformClick()
                        End If
                        Return
                    Else
                        MsgBox("This record already exists. Please alter the name to create a unique record.", MsgBoxStyle.Exclamation)
                        Return
                    End If
                End If

                If choice = 1 Then
                    str = "INSERT INTO product (item_name, buying_price, selling_price, status, color, size, stock_count, series_id) " &
                          "VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "', '" & txtField3.Text.Trim() & "', " &
                          "'" & cbBox1.Text.Trim() & "', '" & txtField5.Text.Trim() & "', '" & txtField6.Text.Trim() & "', '" & txtField7.Text.Trim() & "', " & seriesSQL & ")"
                ElseIf choice = 2 Then
                    str = "INSERT INTO supplier (company_name, contact_person, country_origin) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "', '" & txtField3.Text.Trim() & "')"
                ElseIf choice = 3 Then
                    str = "INSERT INTO customer (customer_name, address) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "')"
                ElseIf choice = 4 Then
                    str = "INSERT INTO employee (employee_name, role, email_address) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "', '" & txtField3.Text.Trim() & "')"
                ElseIf choice = 5 Then
                    str = "INSERT INTO courier (company_name, address, contact_number) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "', '" & txtField3.Text.Trim() & "')"
                ElseIf choice = 6 Then
                    str = "INSERT INTO series (series_name, manufacturer, release_year, total_in_set) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "', '" & txtField3.Text.Trim() & "', '" & txtField5.Text.Trim() & "')"
                ElseIf choice = 7 Then
                    str = "INSERT INTO branch (branch_name, address, operating_hours) VALUES ('" & txtField1.Text.Trim() & "', '" & txtField2.Text.Trim() & "', '" & txtField3.Text.Trim() & "')"
                End If

                readquery(str)
                MsgBox("New Record Successfully Added!", MsgBoxStyle.Information)

            Else
                If choice = 1 Then
                    str = "UPDATE product SET item_name='" & txtField1.Text.Trim() & "', buying_price='" & txtField2.Text.Trim() & "', selling_price='" & txtField3.Text.Trim() & "', status='" & cbBox1.Text.Trim() & "', color='" & txtField5.Text.Trim() & "', size='" & txtField6.Text.Trim() & "', stock_count='" & txtField7.Text.Trim() & "', series_id=" & seriesSQL & " WHERE product_id=" & currentID
                ElseIf choice = 2 Then
                    str = "UPDATE supplier SET company_name='" & txtField1.Text.Trim() & "', contact_person='" & txtField2.Text.Trim() & "', country_origin='" & txtField3.Text.Trim() & "' WHERE supplier_id=" & currentID
                ElseIf choice = 3 Then
                    str = "UPDATE customer SET customer_name='" & txtField1.Text.Trim() & "', address='" & txtField2.Text.Trim() & "' WHERE customer_id=" & currentID
                ElseIf choice = 4 Then
                    str = "UPDATE employee SET employee_name='" & txtField1.Text.Trim() & "', role='" & txtField2.Text.Trim() & "', email_address='" & txtField3.Text.Trim() & "' WHERE employee_id=" & currentID
                ElseIf choice = 5 Then
                    str = "UPDATE courier SET company_name='" & txtField1.Text.Trim() & "', address='" & txtField2.Text.Trim() & "', contact_number='" & txtField3.Text.Trim() & "' WHERE courier_id=" & currentID
                ElseIf choice = 6 Then
                    str = "UPDATE series SET series_name='" & txtField1.Text.Trim() & "', manufacturer='" & txtField2.Text.Trim() & "', release_year='" & txtField3.Text.Trim() & "', total_in_set='" & txtField5.Text.Trim() & "' WHERE series_id=" & currentID
                ElseIf choice = 7 Then
                    str = "UPDATE branch SET branch_name='" & txtField1.Text.Trim() & "', address='" & txtField2.Text.Trim() & "', operating_hours='" & txtField3.Text.Trim() & "' WHERE branch_id=" & currentID
                End If

                readquery(str)
                MsgBox("Record Successfully Updated!", MsgBoxStyle.Information)
            End If

            LoadGridData()
            btnClear.PerformClick()

        Catch ex As Exception
            MsgBox("Error saving record: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtField1.Clear() : txtField2.Clear() : txtField3.Clear()
        txtField5.Clear() : txtField6.Clear() : txtField7.Clear()
        txtField1.ReadOnly = False : txtField2.ReadOnly = False : txtField3.ReadOnly = False
        currentID = 0 : currentTimestamp = ""
        currentCustID = 0 : currentProdID = 0   ' ← ADD
        btnDelete.Enabled = False
        btnSave.Text = If(choice = 8, "✔ Mark Delivered", "Save Record")
        If choice = 8 Then btnSave.Enabled = False
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If choice = 8 Then
            If currentCustID = 0 Then Return
            If MsgBox("Cancel order and return " & selQty & " unit(s) to stock?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation) = MsgBoxResult.Yes Then
                Try
                    readquery("UPDATE purchases SET status = 'Cancelled' " &
                      "WHERE customer_id = " & currentCustID & " AND product_id = " & currentProdID)
                    readquery("UPDATE product SET stock_count = stock_count + " & selQty &
                      " WHERE item_name = '" & txtField2.Text.Trim().Replace("'", "''") & "'")
                    MsgBox("Cancelled and stock restored.")
                    LoadGridData() : btnClear.PerformClick()
                Catch ex As Exception
                    MsgBox("Error: " & ex.Message)
                End Try
            End If
            Return
        End If
        If currentID = 0 Then Return

        Dim ask = MsgBox("Are you sure you want to permanently delete " & txtField1.Text & "?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Confirm Delete")

        If ask = MsgBoxResult.Yes Then
            Dim str As String = ""
            If choice = 1 Then str = "DELETE FROM product WHERE product_id = " & currentID
            If choice = 2 Then str = "DELETE FROM supplier WHERE supplier_id = " & currentID
            If choice = 3 Then str = "DELETE FROM customer WHERE customer_id = " & currentID
            If choice = 4 Then str = "DELETE FROM employee WHERE employee_id = " & currentID
            If choice = 5 Then str = "DELETE FROM courier WHERE courier_id = " & currentID
            If choice = 6 Then str = "DELETE FROM series WHERE series_id = " & currentID
            If choice = 7 Then str = "DELETE FROM branch WHERE branch_id = " & currentID

            Try
                readquery(str)
                MsgBox("Record deleted successfully!")
                LoadGridData()
                btnClear.PerformClick()
            Catch ex As Exception
                MsgBox("Error deleting: " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub

    Private Sub btnRunReport_Click(sender As Object, e As EventArgs) Handles btnRunReport.Click
        If cboReport.Text = "" Then
            MsgBox("Please select a report to run.", MsgBoxStyle.Exclamation)
            Return
        End If

        Dim reportIndex As Integer = Val(cboReport.Text.Substring(0, 1))
        Dim searchVal As String = ""
        If txtSearch IsNot Nothing Then searchVal = txtSearch.Text.Trim().Replace("'", "''")

        Dim sql As String = GetReportQuery(reportIndex, searchVal)

        Try
            Dim dt As New DataTable()
            dt = getDataTable(sql)
            dgvReport.DataSource = dt
            dgvReport.AutoResizeColumns()
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
        Dim searchFilter As String = ""
        Select Case reportIndex
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
                      "pu.reservation_date AS 'Order Date', pu.status AS 'Status', " &
                      "p.selling_price AS 'Sell Price', p.buying_price AS 'Buy Price', " &
                      "(p.selling_price - p.buying_price) AS 'Profit Margin (PHP)', " &
                      "COALESCE(co.company_name, 'Walk-in / Pickup') AS 'Courier Used' " &
                      "FROM purchases pu INNER JOIN customer c ON pu.customer_id = c.customer_id " &
                      "INNER JOIN product p ON pu.product_id = p.product_id " &
                      "LEFT JOIN delivers_to dt ON dt.customer_id = c.customer_id " &
                      "LEFT JOIN courier co ON dt.courier_id = co.courier_id "
                If searchVal <> "" Then
                    sql &= "WHERE c.customer_name LIKE '%" & searchVal & "%' OR p.item_name LIKE '%" & searchVal & "%' OR pu.status LIKE '%" & searchVal & "%' "
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
                      "FROM product p LEFT JOIN provides pr ON p.product_id = pr.product_id " &
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
                      "sh.shipping_date AS 'Ship Date' " &
                      "FROM product p LEFT JOIN provides pr ON p.product_id = pr.product_id " &
                      "LEFT JOIN supplier sup ON pr.supplier_id = sup.supplier_id " &
                      "LEFT JOIN stores st ON p.product_id = st.product_id " &
                      "LEFT JOIN branch b ON st.branch_id = b.branch_id " &
                      "LEFT JOIN purchases pu ON p.product_id = pu.product_id " &
                      "LEFT JOIN customer c ON pu.customer_id = c.customer_id " &
                      "LEFT JOIN ships sh ON p.product_id = sh.product_id " &
                      "LEFT JOIN courier co ON sh.courier_id = co.courier_id "
                If searchVal <> "" Then
                    sql &= "WHERE p.item_name LIKE '%" & searchVal & "%' OR sup.company_name LIKE '%" & searchVal & "%' OR b.branch_name LIKE '%" & searchVal & "%' OR c.customer_name LIKE '%" & searchVal & "%' "
                End If
                sql &= "ORDER BY p.item_name"

            Case Else
                Return "SELECT 'Invalid report selected' AS Error"
        End Select

        Return sql
    End Function

    Private Sub txtField2_TextChanged(sender As Object, e As EventArgs) Handles txtField2.TextChanged
    End Sub

    Private Sub dgvReport_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvReport.CellContentClick
    End Sub
End Class