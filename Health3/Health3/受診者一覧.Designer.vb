<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 受診者一覧
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
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.btnSearchWA = New System.Windows.Forms.Button()
        Me.btnSearchRA = New System.Windows.Forms.Button()
        Me.btnSearchYA = New System.Windows.Forms.Button()
        Me.btnSearchMA = New System.Windows.Forms.Button()
        Me.btnSearchHA = New System.Windows.Forms.Button()
        Me.btnSearchNA = New System.Windows.Forms.Button()
        Me.btnSearchTA = New System.Windows.Forms.Button()
        Me.btnSearchSA = New System.Windows.Forms.Button()
        Me.btnSearchKA = New System.Windows.Forms.Button()
        Me.btnSearchA = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvList
        '
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(102, 72)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowTemplate.Height = 21
        Me.dgvList.Size = New System.Drawing.Size(739, 493)
        Me.dgvList.TabIndex = 0
        '
        'btnSearchWA
        '
        Me.btnSearchWA.Location = New System.Drawing.Point(54, 257)
        Me.btnSearchWA.Name = "btnSearchWA"
        Me.btnSearchWA.Size = New System.Drawing.Size(22, 19)
        Me.btnSearchWA.TabIndex = 585
        Me.btnSearchWA.Text = "ワ"
        Me.btnSearchWA.UseVisualStyleBackColor = True
        '
        'btnSearchRA
        '
        Me.btnSearchRA.Location = New System.Drawing.Point(54, 239)
        Me.btnSearchRA.Name = "btnSearchRA"
        Me.btnSearchRA.Size = New System.Drawing.Size(22, 19)
        Me.btnSearchRA.TabIndex = 584
        Me.btnSearchRA.Text = "ラ"
        Me.btnSearchRA.UseVisualStyleBackColor = True
        '
        'btnSearchYA
        '
        Me.btnSearchYA.Location = New System.Drawing.Point(54, 221)
        Me.btnSearchYA.Name = "btnSearchYA"
        Me.btnSearchYA.Size = New System.Drawing.Size(22, 19)
        Me.btnSearchYA.TabIndex = 583
        Me.btnSearchYA.Text = "ヤ"
        Me.btnSearchYA.UseVisualStyleBackColor = True
        '
        'btnSearchMA
        '
        Me.btnSearchMA.Location = New System.Drawing.Point(54, 203)
        Me.btnSearchMA.Name = "btnSearchMA"
        Me.btnSearchMA.Size = New System.Drawing.Size(22, 19)
        Me.btnSearchMA.TabIndex = 582
        Me.btnSearchMA.Text = "マ"
        Me.btnSearchMA.UseVisualStyleBackColor = True
        '
        'btnSearchHA
        '
        Me.btnSearchHA.Location = New System.Drawing.Point(54, 185)
        Me.btnSearchHA.Name = "btnSearchHA"
        Me.btnSearchHA.Size = New System.Drawing.Size(22, 19)
        Me.btnSearchHA.TabIndex = 581
        Me.btnSearchHA.Text = "ハ"
        Me.btnSearchHA.UseVisualStyleBackColor = True
        '
        'btnSearchNA
        '
        Me.btnSearchNA.Location = New System.Drawing.Point(54, 167)
        Me.btnSearchNA.Name = "btnSearchNA"
        Me.btnSearchNA.Size = New System.Drawing.Size(22, 19)
        Me.btnSearchNA.TabIndex = 580
        Me.btnSearchNA.Text = "ナ"
        Me.btnSearchNA.UseVisualStyleBackColor = True
        '
        'btnSearchTA
        '
        Me.btnSearchTA.Location = New System.Drawing.Point(54, 149)
        Me.btnSearchTA.Name = "btnSearchTA"
        Me.btnSearchTA.Size = New System.Drawing.Size(22, 19)
        Me.btnSearchTA.TabIndex = 579
        Me.btnSearchTA.Text = "タ"
        Me.btnSearchTA.UseVisualStyleBackColor = True
        '
        'btnSearchSA
        '
        Me.btnSearchSA.Location = New System.Drawing.Point(54, 131)
        Me.btnSearchSA.Name = "btnSearchSA"
        Me.btnSearchSA.Size = New System.Drawing.Size(22, 19)
        Me.btnSearchSA.TabIndex = 578
        Me.btnSearchSA.Text = "サ"
        Me.btnSearchSA.UseVisualStyleBackColor = True
        '
        'btnSearchKA
        '
        Me.btnSearchKA.Location = New System.Drawing.Point(54, 113)
        Me.btnSearchKA.Name = "btnSearchKA"
        Me.btnSearchKA.Size = New System.Drawing.Size(22, 19)
        Me.btnSearchKA.TabIndex = 577
        Me.btnSearchKA.Text = "カ"
        Me.btnSearchKA.UseVisualStyleBackColor = True
        '
        'btnSearchA
        '
        Me.btnSearchA.Location = New System.Drawing.Point(54, 95)
        Me.btnSearchA.Name = "btnSearchA"
        Me.btnSearchA.Size = New System.Drawing.Size(22, 19)
        Me.btnSearchA.TabIndex = 576
        Me.btnSearchA.Text = "ア"
        Me.btnSearchA.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(152, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(189, 15)
        Me.Label1.TabIndex = 586
        Me.Label1.Text = "ﾀﾞﾌﾞﾙｸﾘｯｸした項目で並べます"
        '
        '受診者一覧
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(950, 616)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSearchWA)
        Me.Controls.Add(Me.btnSearchRA)
        Me.Controls.Add(Me.btnSearchYA)
        Me.Controls.Add(Me.btnSearchMA)
        Me.Controls.Add(Me.btnSearchHA)
        Me.Controls.Add(Me.btnSearchNA)
        Me.Controls.Add(Me.btnSearchTA)
        Me.Controls.Add(Me.btnSearchSA)
        Me.Controls.Add(Me.btnSearchKA)
        Me.Controls.Add(Me.btnSearchA)
        Me.Controls.Add(Me.dgvList)
        Me.Name = "受診者一覧"
        Me.Text = "受診者一覧"
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearchWA As System.Windows.Forms.Button
    Friend WithEvents btnSearchRA As System.Windows.Forms.Button
    Friend WithEvents btnSearchYA As System.Windows.Forms.Button
    Friend WithEvents btnSearchMA As System.Windows.Forms.Button
    Friend WithEvents btnSearchHA As System.Windows.Forms.Button
    Friend WithEvents btnSearchNA As System.Windows.Forms.Button
    Friend WithEvents btnSearchTA As System.Windows.Forms.Button
    Friend WithEvents btnSearchSA As System.Windows.Forms.Button
    Friend WithEvents btnSearchKA As System.Windows.Forms.Button
    Friend WithEvents btnSearchA As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
