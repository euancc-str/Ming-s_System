Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    ' ==========================================
    ' MAIN ENTITIES - UNIFIED CRUD (choice=1-6, operation=1)
    ' ==========================================
    Private Sub ProductToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ProductToolStripMenuItem1.Click
        choice = 1
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub SupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierToolStripMenuItem.Click
        choice = 2
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub CustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerToolStripMenuItem.Click
        choice = 3
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub EmployeeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmployeeToolStripMenuItem.Click
        choice = 4
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub CourierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CourierToolStripMenuItem.Click
        choice = 5
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub SeriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SeriesToolStripMenuItem.Click
        choice = 6
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    ' ==========================================
    ' PHASE 4: TRANSACTIONS (tag1=1-6, opens frmTransaction)
    ' ==========================================
    Private Sub RestockFromSupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestockFromSupplierToolStripMenuItem.Click
        tag1 = 1 ' PROVIDES
        frmTransaction.MdiParent = Me
        frmTransaction.Show()
    End Sub

    Private Sub RecordSaleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecordSaleToolStripMenuItem.Click
        tag1 = 2 ' PURCHASES
        frmTransaction.MdiParent = Me
        frmTransaction.Show()
    End Sub

    Private Sub RecordDeliveryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecordDeliveryToolStripMenuItem.Click
        tag1 = 3 ' DELIVERS_TO
        frmTransaction.MdiParent = Me
        frmTransaction.Show()
    End Sub

    Private Sub RecordShipmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecordShipmentToolStripMenuItem.Click
        tag1 = 4 ' SHIPS
        frmTransaction.MdiParent = Me
        frmTransaction.Show()
    End Sub

    Private Sub AssignToBranchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssignToBranchToolStripMenuItem.Click
        tag1 = 5 ' STORES
        frmTransaction.MdiParent = Me
        frmTransaction.Show()
    End Sub

    Private Sub AssignWorkScheduleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssignWorkScheduleToolStripMenuItem.Click
        tag1 = 6 ' WORKS_IN
        frmTransaction.MdiParent = Me
        frmTransaction.Show()
    End Sub

    ' ==========================================
    ' PHASE 5: REPORTS (operation=5, opens MainPanel)
    ' ==========================================
    Private Sub ViewReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewReportsToolStripMenuItem.Click
        operation = 5 ' REPORT MODE
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    ' Blank handler to prevent designer errors
    Private Sub ProductToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductToolStripMenuItem.Click
    End Sub
End Class