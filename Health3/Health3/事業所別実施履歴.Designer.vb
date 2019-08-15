<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 事業所別実施履歴
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
        Me.nyPanel = New System.Windows.Forms.Panel()
        Me.n3textBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbtnNaga4 = New System.Windows.Forms.RadioButton()
        Me.rbtnNaga3 = New System.Windows.Forms.RadioButton()
        Me.rbtnKaku2 = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbtnPreview = New System.Windows.Forms.RadioButton()
        Me.rbtnPrint = New System.Windows.Forms.RadioButton()
        Me.btnEnvelope = New System.Windows.Forms.Button()
        Me.btnPersonal = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.indLabel = New System.Windows.Forms.Label()
        Me.indList = New System.Windows.Forms.ListBox()
        Me.nyPanel.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'nyPanel
        '
        Me.nyPanel.Controls.Add(Me.n3textBox)
        Me.nyPanel.Controls.Add(Me.Label1)
        Me.nyPanel.Location = New System.Drawing.Point(906, 16)
        Me.nyPanel.Name = "nyPanel"
        Me.nyPanel.Size = New System.Drawing.Size(154, 48)
        Me.nyPanel.TabIndex = 22
        Me.nyPanel.Visible = False
        '
        'n3textBox
        '
        Me.n3textBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.n3textBox.Location = New System.Drawing.Point(19, 23)
        Me.n3textBox.Name = "n3textBox"
        Me.n3textBox.Size = New System.Drawing.Size(111, 19)
        Me.n3textBox.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "長3に入れる文字列"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbtnNaga4)
        Me.Panel2.Controls.Add(Me.rbtnNaga3)
        Me.Panel2.Controls.Add(Me.rbtnKaku2)
        Me.Panel2.Location = New System.Drawing.Point(660, 46)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(235, 22)
        Me.Panel2.TabIndex = 21
        '
        'rbtnNaga4
        '
        Me.rbtnNaga4.AutoSize = True
        Me.rbtnNaga4.Location = New System.Drawing.Point(3, 3)
        Me.rbtnNaga4.Name = "rbtnNaga4"
        Me.rbtnNaga4.Size = New System.Drawing.Size(65, 16)
        Me.rbtnNaga4.TabIndex = 8
        Me.rbtnNaga4.TabStop = True
        Me.rbtnNaga4.Text = "長形4号"
        Me.rbtnNaga4.UseVisualStyleBackColor = True
        '
        'rbtnNaga3
        '
        Me.rbtnNaga3.AutoSize = True
        Me.rbtnNaga3.Checked = True
        Me.rbtnNaga3.Location = New System.Drawing.Point(74, 3)
        Me.rbtnNaga3.Name = "rbtnNaga3"
        Me.rbtnNaga3.Size = New System.Drawing.Size(65, 16)
        Me.rbtnNaga3.TabIndex = 9
        Me.rbtnNaga3.TabStop = True
        Me.rbtnNaga3.Text = "長形3号"
        Me.rbtnNaga3.UseVisualStyleBackColor = True
        '
        'rbtnKaku2
        '
        Me.rbtnKaku2.AutoSize = True
        Me.rbtnKaku2.Location = New System.Drawing.Point(159, 3)
        Me.rbtnKaku2.Name = "rbtnKaku2"
        Me.rbtnKaku2.Size = New System.Drawing.Size(65, 16)
        Me.rbtnKaku2.TabIndex = 10
        Me.rbtnKaku2.TabStop = True
        Me.rbtnKaku2.Text = "角形2号"
        Me.rbtnKaku2.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnPreview)
        Me.Panel1.Controls.Add(Me.rbtnPrint)
        Me.Panel1.Location = New System.Drawing.Point(738, 14)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(157, 26)
        Me.Panel1.TabIndex = 20
        '
        'rbtnPreview
        '
        Me.rbtnPreview.AutoSize = True
        Me.rbtnPreview.Location = New System.Drawing.Point(9, 6)
        Me.rbtnPreview.Name = "rbtnPreview"
        Me.rbtnPreview.Size = New System.Drawing.Size(63, 16)
        Me.rbtnPreview.TabIndex = 6
        Me.rbtnPreview.TabStop = True
        Me.rbtnPreview.Text = "ﾌﾟﾚﾋﾞｭｰ"
        Me.rbtnPreview.UseVisualStyleBackColor = True
        '
        'rbtnPrint
        '
        Me.rbtnPrint.AutoSize = True
        Me.rbtnPrint.Location = New System.Drawing.Point(81, 6)
        Me.rbtnPrint.Name = "rbtnPrint"
        Me.rbtnPrint.Size = New System.Drawing.Size(47, 16)
        Me.rbtnPrint.TabIndex = 7
        Me.rbtnPrint.TabStop = True
        Me.rbtnPrint.Text = "印刷"
        Me.rbtnPrint.UseVisualStyleBackColor = True
        '
        'btnEnvelope
        '
        Me.btnEnvelope.Location = New System.Drawing.Point(660, 13)
        Me.btnEnvelope.Name = "btnEnvelope"
        Me.btnEnvelope.Size = New System.Drawing.Size(75, 30)
        Me.btnEnvelope.TabIndex = 19
        Me.btnEnvelope.Text = "封筒"
        Me.btnEnvelope.UseVisualStyleBackColor = True
        '
        'btnPersonal
        '
        Me.btnPersonal.Location = New System.Drawing.Point(541, 13)
        Me.btnPersonal.Name = "btnPersonal"
        Me.btnPersonal.Size = New System.Drawing.Size(75, 30)
        Me.btnPersonal.TabIndex = 18
        Me.btnPersonal.Text = "個人票"
        Me.btnPersonal.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(467, 13)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 30)
        Me.btnPrint.TabIndex = 17
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'dgvList
        '
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(279, 70)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowTemplate.Height = 21
        Me.dgvList.Size = New System.Drawing.Size(769, 560)
        Me.dgvList.TabIndex = 16
        '
        'indLabel
        '
        Me.indLabel.AutoSize = True
        Me.indLabel.Font = New System.Drawing.Font("MS UI Gothic", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.indLabel.ForeColor = System.Drawing.Color.Blue
        Me.indLabel.Location = New System.Drawing.Point(180, 21)
        Me.indLabel.Name = "indLabel"
        Me.indLabel.Size = New System.Drawing.Size(0, 15)
        Me.indLabel.TabIndex = 15
        '
        'indList
        '
        Me.indList.BackColor = System.Drawing.SystemColors.Control
        Me.indList.FormattingEnabled = True
        Me.indList.ItemHeight = 12
        Me.indList.Location = New System.Drawing.Point(25, 49)
        Me.indList.Name = "indList"
        Me.indList.Size = New System.Drawing.Size(229, 580)
        Me.indList.TabIndex = 14
        '
        '事業所別実施履歴
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1096, 659)
        Me.Controls.Add(Me.nyPanel)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnEnvelope)
        Me.Controls.Add(Me.btnPersonal)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.indLabel)
        Me.Controls.Add(Me.indList)
        Me.Name = "事業所別実施履歴"
        Me.Text = "事業所別実施履歴"
        Me.nyPanel.ResumeLayout(False)
        Me.nyPanel.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents nyPanel As System.Windows.Forms.Panel
    Friend WithEvents n3textBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rbtnNaga4 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnNaga3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnKaku2 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbtnPreview As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnPrint As System.Windows.Forms.RadioButton
    Friend WithEvents btnEnvelope As System.Windows.Forms.Button
    Friend WithEvents btnPersonal As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents indLabel As System.Windows.Forms.Label
    Friend WithEvents indList As System.Windows.Forms.ListBox
End Class
