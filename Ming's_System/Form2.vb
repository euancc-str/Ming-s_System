Imports MySql.Data.MySqlClient

Public Class MainPanel


    Private currentID As Integer = 0

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If cboReport IsNot Nothing Then cboReport.Visible = False
        If btnRunReport IsNot Nothing Then btnRunReport.Visible = False
        If dgvReport IsNot Nothing Then dgvReport.Visible = False
        dgvData.Visible = True
        btnDelete.Enabled = False
        btnSave.Text = "Save Record"

        If operation = 5 Then

            btnSave.Visible = False : btnClear.Visible = False : btnDelete.Visible = False
            dgvData.Visible = False
            txtField1.Visible = False : txtField2.Visible = False : txtField3.Visible = False
            txtField5.Visible = False : txtField6.Visible = False : txtField7.Visible = False
            cbBox1.Visible = False
            lblField1.Visible = False : lblField2.Visible = False : lblField3.Visible = False
            lblField4.Visible = False : lblField5.Visible = False : lblField6.Visible = False
            lblField7.Visible = False

            lblField.Text = "SELECT REPORT:"
            cboReport.Items.Clear()
            cboReport.Items.Add("1 - Branch Inventory Overview")
            cboReport.Items.Add("2 - Sales Performance by Customer")
            cboReport.Items.Add("3 - Supplier Restock History")
            cboReport.Items.Add("4 - Low Stock Alert (Stock <= 5)")
            cboReport.Items.Add("5 - Full Supply Chain Trace")
            cboReport.Visible = True : btnRunReport.Visible = True : dgvReport.Visible = True
            cboReport.Location = New Point(286, 148)
            Label1.Location = New Point(106, 148)
            btnRunReport.Location = New Point(cboReport.Location.X + cboReport.Width + 10, cboReport.Location.Y)
            Return
        End If

        btnSave.Visible = True : btnClear.Visible = True : btnDelete.Visible = True

        If choice = 1 Then
            lblField.Text = "PRODUCT INFORMATION"
            lblField1.Text = "Item Name:" : lblField2.Text = "Buying Price:" : lblField3.Text = "Selling Price:"
            lblField4.Text = "Status:" : lblField5.Text = "Color:" : lblField6.Text = "Size:" : lblField7.Text = "Stock Count:"
            lblField4.Visible = True : cbBox1.Visible = True : lblField5.Visible = True : txtField5.Visible = True
            lblField6.Visible = True : txtField6.Visible = True : lblField7.Visible = True : txtField7.Visible = True

        ElseIf choice = 2 Then
            lblField.Text = "SUPPLIER INFORMATION"
            lblField1.Text = "Company Name:" : lblField2.Text = "Contact Person:" : lblField3.Text = "Country of Origin:"
            lblField4.Visible = False : cbBox1.Visible = False : lblField5.Visible = False : txtField5.Visible = False
            lblField6.Visible = False : txtField6.Visible = False : lblField7.Visible = False : txtField7.Visible = False

        ElseIf choice = 3 Then
            lblField.Text = "CUSTOMER INFORMATION"
            lblField1.Text = "Customer Name:" : lblField2.Text = "Address:"
            lblField3.Visible = False : txtField3.Visible = False : lblField4.Visible = False : cbBox1.Visible = False
            lblField5.Visible = False : txtField5.Visible = False : lblField6.Visible = False : txtField6.Visible = False
            lblField7.Visible = False : txtField7.Visible = False

        ElseIf choice = 4 Then
            lblField.Text = "EMPLOYEE INFORMATION"
            lblField1.Text = "Employee Name:" : lblField2.Text = "Role:" : lblField3.Text = "Email Address:"
            lblField3.Visible = True : txtField3.Visible = True : lblField4.Visible = False : cbBox1.Visible = False
            lblField5.Visible = False : txtField5.Visible = False : lblField6.Visible = False : txtField6.Visible = False
            lblField7.Visible = False : txtField7.Visible = False

        ElseIf choice = 5 Then
            lblField.Text = "COURIER INFORMATION"
            lblField1.Text = "Company Name:" : lblField2.Text = "Address:" : lblField3.Text = "Contact Number:"
            lblField3.Visible = True : txtField3.Visible = True : lblField4.Visible = False : cbBox1.Visible = False
            lblField5.Visible = False : txtField5.Visible = False : lblField6.Visible = False : txtField6.Visible = False
            lblField7.Visible = False : txtField7.Visible = False

        ElseIf choice = 6 Then
            lblField.Text = "SERIES INFORMATION"
            lblField1.Text = "Series Name:" : lblField2.Text = "Manufacturer:" : lblField3.Text = "Release Year:"
            lblField5.Text = "Total in Set:"
            lblField3.Visible = True : txtField3.Visible = True : lblField4.Visible = False : cbBox1.Visible = False
            lblField5.Visible = True : txtField5.Visible = True : lblField6.Visible = False : txtField6.Visible = False
            lblField7.Visible = False : txtField7.Visible = False
        End If

        LoadGridData()
    End Sub

    Private Sub LoadGridData()
        Dim sql As String = ""
        Try
            If choice = 1 Then
                sql = "SELECT product_id AS ID, item_name AS Name, stock_count AS Stock, status AS Status FROM product"
            ElseIf choice = 2 Then
                sql = "SELECT supplier_id AS ID, company_name AS Company, contact_person AS Contact FROM supplier"
            ElseIf choice = 3 Then
                sql = "SELECT customer_id AS ID, customer_name AS Name, address AS Address FROM customer"
            ElseIf choice = 4 Then
                sql = "SELECT employee_id AS ID, employee_name AS Name, role AS Role FROM employee"
            ElseIf choice = 5 Then
                sql = "SELECT courier_id AS ID, company_name AS Company, contact_number AS Contact FROM courier"
            ElseIf choice = 6 Then
                sql = "SELECT series_id AS ID, series_name AS Name, release_year AS Year FROM series"
            End If

            dgvData.DataSource = getDataTable(sql)
            dgvData.AutoResizeColumns()
        Catch ex As Exception
            MsgBox("Error loading grid: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvData_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvData.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvData.Rows(e.RowIndex)
            currentID = Val(row.Cells("ID").Value.ToString())

            LoadRecordDetails(currentID)

            btnDelete.Enabled = True
            btnSave.Text = "Update Record"
        End If
    End Sub

    Private Sub LoadRecordDetails(id As Integer)
        Dim str As String = ""
        If choice = 1 Then str = "SELECT * FROM product WHERE product_id = " & id
        If choice = 2 Then str = "SELECT * FROM supplier WHERE supplier_id = " & id
        If choice = 3 Then str = "SELECT * FROM customer WHERE customer_id = " & id
        If choice = 4 Then str = "SELECT * FROM employee WHERE employee_id = " & id
        If choice = 5 Then str = "SELECT * FROM courier WHERE courier_id = " & id
        If choice = 6 Then str = "SELECT * FROM series WHERE series_id = " & id

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
                End If
            End If
        Catch ex As Exception
            MsgBox("Error loading details: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim str As String = ""
        Try
            If currentID = 0 Then

                If choice = 1 Then
                    str = "INSERT INTO product (item_name, buying_price, selling_price, status, color, size, stock_count) " &
                          "VALUES ('" & txtField1.Text & "', '" & txtField2.Text & "', '" & txtField3.Text & "', " &
                          "'" & cbBox1.Text & "', '" & txtField5.Text & "', '" & txtField6.Text & "', '" & txtField7.Text & "')"
                ElseIf choice = 2 Then
                    str = "INSERT INTO supplier (company_name, contact_person, country_origin) VALUES ('" & txtField1.Text & "', '" & txtField2.Text & "', '" & txtField3.Text & "')"
                ElseIf choice = 3 Then
                    str = "INSERT INTO customer (customer_name, address) VALUES ('" & txtField1.Text & "', '" & txtField2.Text & "')"
                ElseIf choice = 4 Then
                    str = "INSERT INTO employee (employee_name, role, email_address) VALUES ('" & txtField1.Text & "', '" & txtField2.Text & "', '" & txtField3.Text & "')"
                ElseIf choice = 5 Then
                    str = "INSERT INTO courier (company_name, address, contact_number) VALUES ('" & txtField1.Text & "', '" & txtField2.Text & "', '" & txtField3.Text & "')"
                ElseIf choice = 6 Then
                    str = "INSERT INTO series (series_name, manufacturer, release_year, total_in_set) VALUES ('" & txtField1.Text & "', '" & txtField2.Text & "', '" & txtField3.Text & "', '" & txtField5.Text & "')"
                End If

                readquery(str)
                MsgBox("New Record Successfully Added!", MsgBoxStyle.Information)

            Else
                ' --- UPDATE EXISTING RECORD ---
                If choice = 1 Then
                    str = "UPDATE product SET item_name='" & txtField1.Text & "', buying_price='" & txtField2.Text & "', selling_price='" & txtField3.Text & "', status='" & cbBox1.Text & "', color='" & txtField5.Text & "', size='" & txtField6.Text & "', stock_count='" & txtField7.Text & "' WHERE product_id=" & currentID
                ElseIf choice = 2 Then
                    str = "UPDATE supplier SET company_name='" & txtField1.Text & "', contact_person='" & txtField2.Text & "', country_origin='" & txtField3.Text & "' WHERE supplier_id=" & currentID
                ElseIf choice = 3 Then
                    str = "UPDATE customer SET customer_name='" & txtField1.Text & "', address='" & txtField2.Text & "' WHERE customer_id=" & currentID
                ElseIf choice = 4 Then
                    str = "UPDATE employee SET employee_name='" & txtField1.Text & "', role='" & txtField2.Text & "', email_address='" & txtField3.Text & "' WHERE employee_id=" & currentID
                ElseIf choice = 5 Then
                    str = "UPDATE courier SET company_name='" & txtField1.Text & "', address='" & txtField2.Text & "', contact_number='" & txtField3.Text & "' WHERE courier_id=" & currentID
                ElseIf choice = 6 Then
                    str = "UPDATE series SET series_name='" & txtField1.Text & "', manufacturer='" & txtField2.Text & "', release_year='" & txtField3.Text & "', total_in_set='" & txtField5.Text & "' WHERE series_id=" & currentID
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
        cbBox1.SelectedIndex = -1
        currentID = 0
        btnSave.Text = "Save Record"
        btnDelete.Enabled = False
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

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
        Dim sql As String = GetReportQuery(reportIndex)
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

    Private Function GetReportQuery(reportIndex As Integer) As String
        Select Case reportIndex
            Case 1
                Return "SELECT b.branch_name AS 'Branch', p.item_name AS 'Product', p.stock_count AS 'Stock', " &
                       "p.selling_price AS 'Price (PHP)', s.last_restocked_date AS 'Last Restocked' " &
                       "FROM stores s INNER JOIN branch b ON s.branch_id = b.branch_id " &
                       "INNER JOIN product p ON s.product_id = p.product_id ORDER BY b.branch_name, s.last_restocked_date DESC"
            Case 2
                Return "SELECT c.customer_name AS 'Customer', p.item_name AS 'Product Purchased', " &
                       "pu.reservation_date AS 'Order Date', pu.status AS 'Status', " &
                       "p.selling_price AS 'Sell Price', p.buying_price AS 'Buy Price', " &
                       "(p.selling_price - p.buying_price) AS 'Profit Margin (PHP)', " &
                       "COALESCE(co.company_name, 'Walk-in / Pickup') AS 'Courier Used' " &
                       "FROM purchases pu INNER JOIN customer c ON pu.customer_id = c.customer_id " &
                       "INNER JOIN product p ON pu.product_id = p.product_id " &
                       "LEFT JOIN delivers_to dt ON dt.customer_id = c.customer_id " &
                       "LEFT JOIN courier co ON dt.courier_id = co.courier_id ORDER BY pu.reservation_date DESC"
            Case 3
                Return "SELECT sup.company_name AS 'Supplier', sup.country_origin AS 'Origin', " &
                       "p.item_name AS 'Product', pr.supply_date AS 'Supply Date', " &
                       "pr.quantity_supplied AS 'Qty Supplied', pr.supply_price AS 'Unit Cost', " &
                       "(pr.quantity_supplied * pr.supply_price) AS 'Total Cost (PHP)' " &
                       "FROM provides pr INNER JOIN supplier sup ON pr.supplier_id = sup.supplier_id " &
                       "INNER JOIN product p ON pr.product_id = p.product_id ORDER BY pr.supply_date DESC"
            Case 4
                Return "SELECT p.item_name AS 'Product', p.stock_count AS 'Remaining Stock', " &
                       "p.selling_price AS 'Sell Price', sup.company_name AS 'Last Supplier', " &
                       "sup.country_origin AS 'Supplier Origin', pr.supply_date AS 'Last Supply Date' " &
                       "FROM product p LEFT JOIN provides pr ON p.product_id = pr.product_id " &
                       "AND pr.supply_date = (SELECT MAX(supply_date) FROM provides WHERE product_id = p.product_id) " &
                       "LEFT JOIN supplier sup ON pr.supplier_id = sup.supplier_id " &
                       "WHERE p.stock_count <= 5 ORDER BY p.stock_count ASC"
            Case 5
                Return "SELECT p.item_name AS 'Product', sup.company_name AS 'Supplied By', " &
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
                       "LEFT JOIN courier co ON sh.courier_id = co.courier_id ORDER BY p.item_name"
            Case Else
                Return "SELECT 'Invalid report selected' AS Error"
        End Select
    End Function

    Private Sub txtField2_TextChanged(sender As Object, e As EventArgs) Handles txtField2.TextChanged

    End Sub
End Class