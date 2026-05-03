Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub CloseActiveWindows()
        For Each child As Form In Me.MdiChildren
            child.Close()
        Next
    End Sub

    Private Sub ProductToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ProductToolStripMenuItem1.Click
        CloseActiveWindows()
        choice = 1
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub SupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierToolStripMenuItem.Click
        CloseActiveWindows()
        choice = 2
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub CustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerToolStripMenuItem.Click
        CloseActiveWindows()
        choice = 3
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub EmployeeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmployeeToolStripMenuItem.Click
        CloseActiveWindows()
        choice = 4
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub CourierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CourierToolStripMenuItem.Click
        CloseActiveWindows()
        choice = 5
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub SeriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SeriesToolStripMenuItem.Click
        CloseActiveWindows()
        choice = 6
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub


    Private Sub RestockFromSupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestockFromSupplierToolStripMenuItem.Click
        CloseActiveWindows()
        tag1 = 1 ' PROVIDES
        frmTransaction.MdiParent = Me
        frmTransaction.Show()
    End Sub

    Private Sub RecordSaleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecordSaleToolStripMenuItem.Click
        CloseActiveWindows()
        tag1 = 2 ' PURCHASES
        frmTransaction.MdiParent = Me
        frmTransaction.Show()
    End Sub



    Private Sub AssignToBranchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssignToBranchToolStripMenuItem.Click
        CloseActiveWindows()
        tag1 = 5 ' STORES
        frmTransaction.MdiParent = Me
        frmTransaction.Show()
    End Sub

    Private Sub AssignWorkScheduleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AssignWorkScheduleToolStripMenuItem.Click
        CloseActiveWindows()
        tag1 = 6 ' WORKS_IN
        frmTransaction.MdiParent = Me
        frmTransaction.Show()
    End Sub

    Private Sub ViewReportsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewReportsToolStripMenuItem.Click
        CloseActiveWindows()
        operation = 5
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    ' Blank handler to prevent designer errors
    Private Sub ProductToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductToolStripMenuItem.Click
    End Sub

    Private Sub BranchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BranchToolStripMenuItem.Click
        CloseActiveWindows()
        choice = 7
        operation = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub OperationsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OperationsToolStripMenuItem.Click

    End Sub

    Private Sub OrderManagerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrderManagerToolStripMenuItem.Click
        CloseActiveWindows()

        choice = 8
        operation = 1


        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub ProductTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductTransferToolStripMenuItem.Click
        CloseActiveWindows()
        choice = 9
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub
End Class