<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 健診結果ＦＤ
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.sendDateBox = New ADBox.adBox()
        Me.dateBox = New ADBox.adBox()
        Me.btnExecute = New System.Windows.Forms.Button()
        Me.dgvResult = New System.Windows.Forms.DataGridView()
        Me.dgvCount = New System.Windows.Forms.DataGridView()
        Me.countChart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        CType(Me.dgvResult, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.countChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(50, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "送付年月日"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(339, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "受診年月"
        '
        'sendDateBox
        '
        Me.sendDateBox.dateText = "16"
        Me.sendDateBox.Location = New System.Drawing.Point(139, 25)
        Me.sendDateBox.Mode = 0
        Me.sendDateBox.monthText = "07"
        Me.sendDateBox.Name = "sendDateBox"
        Me.sendDateBox.Size = New System.Drawing.Size(152, 35)
        Me.sendDateBox.TabIndex = 2
        Me.sendDateBox.yearText = "2019"
        '
        'dateBox
        '
        Me.dateBox.dateText = "16"
        Me.dateBox.Location = New System.Drawing.Point(412, 25)
        Me.dateBox.Mode = 1
        Me.dateBox.monthText = "07"
        Me.dateBox.Name = "dateBox"
        Me.dateBox.Size = New System.Drawing.Size(105, 35)
        Me.dateBox.TabIndex = 3
        Me.dateBox.yearText = "2019"
        '
        'btnExecute
        '
        Me.btnExecute.Location = New System.Drawing.Point(561, 29)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(73, 31)
        Me.btnExecute.TabIndex = 4
        Me.btnExecute.Text = "実行"
        Me.btnExecute.UseVisualStyleBackColor = True
        '
        'dgvResult
        '
        Me.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResult.Location = New System.Drawing.Point(53, 76)
        Me.dgvResult.Name = "dgvResult"
        Me.dgvResult.RowTemplate.Height = 21
        Me.dgvResult.Size = New System.Drawing.Size(1052, 272)
        Me.dgvResult.TabIndex = 5
        '
        'dgvCount
        '
        Me.dgvCount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCount.Location = New System.Drawing.Point(109, 376)
        Me.dgvCount.Name = "dgvCount"
        Me.dgvCount.RowTemplate.Height = 21
        Me.dgvCount.Size = New System.Drawing.Size(913, 75)
        Me.dgvCount.TabIndex = 6
        '
        'countChart
        '
        Me.countChart.Location = New System.Drawing.Point(95, 469)
        Me.countChart.Name = "countChart"
        Series2.Name = "Series1"
        Me.countChart.Series.Add(Series2)
        Me.countChart.Size = New System.Drawing.Size(840, 161)
        Me.countChart.TabIndex = 145
        '
        '健診結果ＦＤ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1198, 642)
        Me.Controls.Add(Me.countChart)
        Me.Controls.Add(Me.dgvCount)
        Me.Controls.Add(Me.dgvResult)
        Me.Controls.Add(Me.btnExecute)
        Me.Controls.Add(Me.dateBox)
        Me.Controls.Add(Me.sendDateBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "健診結果ＦＤ"
        Me.Text = "健診結果ＦＤ"
        CType(Me.dgvResult, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.countChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents sendDateBox As ADBox.adBox
    Friend WithEvents dateBox As ADBox.adBox
    Friend WithEvents btnExecute As System.Windows.Forms.Button
    Friend WithEvents dgvResult As System.Windows.Forms.DataGridView
    Friend WithEvents dgvCount As System.Windows.Forms.DataGridView
    Friend WithEvents countChart As System.Windows.Forms.DataVisualization.Charting.Chart
End Class
