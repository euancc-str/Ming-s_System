Public Class Form1
    Private Sub ProductToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductToolStripMenuItem.Click

    End Sub

    Private Sub ProductToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ProductToolStripMenuItem1.Click
        choice = 1
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub SupplierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplierToolStripMenuItem.Click
        choice = 2
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub CustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerToolStripMenuItem.Click
        choice = 3
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub SeriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SeriesToolStripMenuItem.Click
        choice = 6
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub CourierToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CourierToolStripMenuItem.Click
        choice = 5
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub

    Private Sub EmployeeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmployeeToolStripMenuItem.Click
        choice = 4
        MainPanel.MdiParent = Me
        MainPanel.Show()
    End Sub
End Class
