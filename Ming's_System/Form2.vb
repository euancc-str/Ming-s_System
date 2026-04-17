Imports Org.BouncyCastle.Asn1.Cmp

Public Class MainPanel

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' --- THE CHAMELEON LOGIC ---

        If choice = 1 Then
            ' 1. PRODUCTS (7 Fields)
            lblField.Text = "PRODUCT INFORMATION"
            lblField1.Text = "Item Name:"
            lblField2.Text = "Buying Price:"
            lblField3.Text = "Selling Price:"
            lblField4.Text = "Status:"
            lblField5.Text = "Color:"
            lblField6.Text = "Size:"
            lblField7.Text = "Stock Count:"

            lblField4.Visible = True
            cbBox1.Visible = True
            lblField5.Visible = True
            txtField5.Visible = True
            lblField6.Visible = True
            txtField6.Visible = True
            lblField7.Visible = True
            txtField7.Visible = True

        ElseIf choice = 2 Then
            ' 2. SUPPLIERS (3 Fields)
            lblField.Text = "SUPPLIER INFORMATION"
            lblField1.Text = "Company Name:"
            lblField2.Text = "Contact Person:"
            lblField3.Text = "Country of Origin:"

            lblField4.Visible = False
            cbBox1.Visible = False
            lblField5.Visible = False
            txtField5.Visible = False
            lblField6.Visible = False
            txtField6.Visible = False
            lblField7.Visible = False
            txtField7.Visible = False

        ElseIf choice = 3 Then
            ' 3. CUSTOMERS (2 Fields - ID is Auto-Increment)
            lblField.Text = "CUSTOMER INFORMATION"
            lblField1.Text = "Customer Name:"
            lblField2.Text = "Address:"
            lblField3.Visible = False
            txtField3.Visible = False

            lblField4.Visible = False
            cbBox1.Visible = False
            lblField5.Visible = False
            txtField5.Visible = False
            lblField6.Visible = False
            txtField6.Visible = False
            lblField7.Visible = False
            txtField7.Visible = False

        ElseIf choice = 4 Then
            ' 4. EMPLOYEES (3 Fields)
            lblField.Text = "EMPLOYEE INFORMATION"
            lblField1.Text = "Employee Name:"
            lblField2.Text = "Role:"
            lblField3.Text = "Email Address:"
            lblField3.Visible = True
            txtField3.Visible = True

            lblField4.Visible = False
            cbBox1.Visible = False
            lblField5.Visible = False
            txtField5.Visible = False
            lblField6.Visible = False
            txtField6.Visible = False
            lblField7.Visible = False
            txtField7.Visible = False

        ElseIf choice = 5 Then
            ' 5. COURIER (3 Fields)
            lblField.Text = "COURIER INFORMATION"
            lblField1.Text = "Company Name:"
            lblField2.Text = "Address:"
            lblField3.Text = "Contact Number:"
            lblField3.Visible = True
            txtField3.Visible = True

            lblField4.Visible = False
            cbBox1.Visible = False
            lblField5.Visible = False
            txtField5.Visible = False
            lblField6.Visible = False
            txtField6.Visible = False
            lblField7.Visible = False
            txtField7.Visible = False

        ElseIf choice = 6 Then
            ' 6. SERIES (4 Fields - We skip cbBox1 and use txtField5 for the 4th input)
            lblField.Text = "SERIES INFORMATION"
            lblField1.Text = "Series Name:"
            lblField2.Text = "Manufacturer:"
            lblField3.Text = "Release Year:"
            lblField5.Text = "Total in Set:"
            lblField3.Visible = True
            txtField3.Visible = True

            lblField4.Visible = False
            cbBox1.Visible = False
            lblField5.Visible = True
            txtField5.Visible = True
            lblField6.Visible = False
            txtField6.Visible = False
            lblField7.Visible = False
            txtField7.Visible = False
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Dim str As String

        If choice = 1 Then
            str = "INSERT INTO product (item_name, buying_price, selling_price, status, color, size, stock_count) " &
              "VALUES ('" & txtField1.Text & "', '" & txtField2.Text & "', '" & txtField3.Text & "', " &
              "'" & cbBox1.Text & "', '" & txtField5.Text & "', '" & txtField6.Text & "', '" & txtField7.Text & "')"
            Try
                readquery(str)
                MsgBox("New Product Successfully Added!")
            Catch ex As Exception
                MsgBox("Error adding product: " & ex.Message, MsgBoxStyle.Critical)
            End Try

        ElseIf choice = 2 Then
            str = "INSERT INTO supplier (company_name, contact_person, country_origin) " &
              "VALUES ('" & txtField1.Text & "', '" & txtField2.Text & "', '" & txtField3.Text & "')"
            Try
                readquery(str)
                MsgBox("New Supplier Successfully Added!")
            Catch ex As Exception
                MsgBox("Error adding supplier: " & ex.Message, MsgBoxStyle.Critical)
            End Try

        ElseIf choice = 3 Then
            str = "INSERT INTO customer (customer_name, address) " &
              "VALUES ('" & txtField1.Text & "', '" & txtField2.Text & "')"
            Try
                readquery(str)
                MsgBox("New Customer Successfully Added!")
            Catch ex As Exception
                MsgBox("Error adding customer: " & ex.Message, MsgBoxStyle.Critical)
            End Try

        ElseIf choice = 4 Then
            str = "INSERT INTO employee (employee_name, role, email_address) " &
              "VALUES ('" & txtField1.Text & "', '" & txtField2.Text & "', '" & txtField3.Text & "')"
            Try
                readquery(str)
                MsgBox("New Employee Successfully Added!")
            Catch ex As Exception
                MsgBox("Error adding employee: " & ex.Message, MsgBoxStyle.Critical)
            End Try

        ElseIf choice = 5 Then
            str = "INSERT INTO courier (company_name, address, contact_number) " &
              "VALUES ('" & txtField1.Text & "', '" & txtField2.Text & "', '" & txtField3.Text & "')"
            Try
                readquery(str)
                MsgBox("New Courier Successfully Added!")
            Catch ex As Exception
                MsgBox("Error adding courier: " & ex.Message, MsgBoxStyle.Critical)
            End Try

        ElseIf choice = 6 Then
            ' Notice how it uses txtField5 here because we skipped cbBox1!
            str = "INSERT INTO series (series_name, manufacturer, release_year, total_in_set) " &
              "VALUES ('" & txtField1.Text & "', '" & txtField2.Text & "', '" & txtField3.Text & "', '" & txtField5.Text & "')"
            Try
                readquery(str)
                MsgBox("New Series Successfully Added!")
            Catch ex As Exception
                MsgBox("Error adding series: " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    ' --- AUTO GENERATED EVENTS BELOW ---
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblField1.Click
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles txtField1.TextChanged
    End Sub
    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles lblField7.Click
    End Sub
    Private Sub lblField2_Click(sender As Object, e As EventArgs) Handles lblField2.Click
    End Sub
    Private Sub lblField3_Click(sender As Object, e As EventArgs) Handles lblField3.Click
    End Sub
    Private Sub lblField5_Click(sender As Object, e As EventArgs) Handles lblField5.Click
    End Sub
    Private Sub lblField4_Click(sender As Object, e As EventArgs) Handles lblField4.Click
    End Sub
    Private Sub txtField7_TextChanged(sender As Object, e As EventArgs) Handles txtField7.TextChanged
    End Sub
    Private Sub txtField6_TextChanged(sender As Object, e As EventArgs) Handles txtField6.TextChanged
    End Sub
    Private Sub txtField5_TextChanged(sender As Object, e As EventArgs) Handles txtField5.TextChanged
    End Sub
    Private Sub txtField3_TextChanged(sender As Object, e As EventArgs) Handles txtField3.TextChanged
    End Sub
    Private Sub txtField2_TextChanged(sender As Object, e As EventArgs) Handles txtField2.TextChanged
    End Sub
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles lblField.Click
    End Sub
    Private Sub cbField4_SelectedIndexChanged(sender As Object, e As EventArgs)
    End Sub

End Class