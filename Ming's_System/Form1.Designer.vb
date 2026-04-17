<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        BackgroundWorker1 = New ComponentModel.BackgroundWorker()
        MenuStrip1 = New MenuStrip()
        ProductToolStripMenuItem = New ToolStripMenuItem()
        ProductToolStripMenuItem1 = New ToolStripMenuItem()
        SupplierToolStripMenuItem = New ToolStripMenuItem()
        EmployeeToolStripMenuItem = New ToolStripMenuItem()
        CourierToolStripMenuItem = New ToolStripMenuItem()
        SeriesToolStripMenuItem = New ToolStripMenuItem()
        CustomerToolStripMenuItem = New ToolStripMenuItem()
        TransactionsToolStripMenuItem = New ToolStripMenuItem()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.ImageScalingSize = New Size(20, 20)
        MenuStrip1.Items.AddRange(New ToolStripItem() {ProductToolStripMenuItem, TransactionsToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(1205, 28)
        MenuStrip1.TabIndex = 0
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' ProductToolStripMenuItem
        ' 
        ProductToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ProductToolStripMenuItem1, SupplierToolStripMenuItem, EmployeeToolStripMenuItem, CourierToolStripMenuItem, SeriesToolStripMenuItem, CustomerToolStripMenuItem})
        ProductToolStripMenuItem.Name = "ProductToolStripMenuItem"
        ProductToolStripMenuItem.Size = New Size(56, 24)
        ProductToolStripMenuItem.Text = "Main"
        ' 
        ' ProductToolStripMenuItem1
        ' 
        ProductToolStripMenuItem1.Name = "ProductToolStripMenuItem1"
        ProductToolStripMenuItem1.Size = New Size(224, 26)
        ProductToolStripMenuItem1.Text = "Product"
        ' 
        ' SupplierToolStripMenuItem
        ' 
        SupplierToolStripMenuItem.Name = "SupplierToolStripMenuItem"
        SupplierToolStripMenuItem.Size = New Size(224, 26)
        SupplierToolStripMenuItem.Text = "Supplier"
        ' 
        ' EmployeeToolStripMenuItem
        ' 
        EmployeeToolStripMenuItem.Name = "EmployeeToolStripMenuItem"
        EmployeeToolStripMenuItem.Size = New Size(224, 26)
        EmployeeToolStripMenuItem.Text = "Employee"
        ' 
        ' CourierToolStripMenuItem
        ' 
        CourierToolStripMenuItem.Name = "CourierToolStripMenuItem"
        CourierToolStripMenuItem.Size = New Size(224, 26)
        CourierToolStripMenuItem.Text = "Courier"
        ' 
        ' SeriesToolStripMenuItem
        ' 
        SeriesToolStripMenuItem.Name = "SeriesToolStripMenuItem"
        SeriesToolStripMenuItem.Size = New Size(224, 26)
        SeriesToolStripMenuItem.Text = "Series"
        ' 
        ' CustomerToolStripMenuItem
        ' 
        CustomerToolStripMenuItem.Name = "CustomerToolStripMenuItem"
        CustomerToolStripMenuItem.Size = New Size(224, 26)
        CustomerToolStripMenuItem.Text = "Customer"
        ' 
        ' TransactionsToolStripMenuItem
        ' 
        TransactionsToolStripMenuItem.Name = "TransactionsToolStripMenuItem"
        TransactionsToolStripMenuItem.Size = New Size(104, 24)
        TransactionsToolStripMenuItem.Text = "Transactions"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1205, 704)
        Controls.Add(MenuStrip1)
        IsMdiContainer = True
        MainMenuStrip = MenuStrip1
        Name = "Form1"
        Text = "m"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ProductToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProductToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SupplierToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EmployeeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TransactionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CourierToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SeriesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CustomerToolStripMenuItem As ToolStripMenuItem

End Class
