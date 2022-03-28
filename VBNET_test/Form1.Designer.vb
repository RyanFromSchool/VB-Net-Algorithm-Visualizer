<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HomeForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.BtnDijkstra = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnDijkstra
        '
        Me.BtnDijkstra.Location = New System.Drawing.Point(39, 32)
        Me.BtnDijkstra.Name = "BtnDijkstra"
        Me.BtnDijkstra.Size = New System.Drawing.Size(180, 23)
        Me.BtnDijkstra.TabIndex = 0
        Me.BtnDijkstra.Text = "Go to Dijkstra's Algortihm"
        Me.BtnDijkstra.UseVisualStyleBackColor = True
        '
        'HomeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.BtnDijkstra)
        Me.Name = "HomeForm"
        Me.Text = "VB.Net Adventure"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnDijkstra As Button
End Class
