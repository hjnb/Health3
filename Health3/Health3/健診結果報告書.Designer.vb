<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 健診結果報告書
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
        Me.chkKaryo = New System.Windows.Forms.CheckBox()
        Me.chkSeisa = New System.Windows.Forms.CheckBox()
        Me.chkSaiken = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.sijiWordBox = New System.Windows.Forms.TextBox()
        Me.dgvResult = New System.Windows.Forms.DataGridView()
        Me.sijiLabel = New System.Windows.Forms.Label()
        Me.syokenLabel = New System.Windows.Forms.Label()
        Me.totalLabel = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnExecute = New System.Windows.Forms.Button()
        Me.toYmdBox = New ymdBox.ymdBox()
        Me.fromYmdBox = New ymdBox.ymdBox()
        Me.indBox = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        CType(Me.dgvResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkKaryo
        '
        Me.chkKaryo.AutoSize = True
        Me.chkKaryo.Checked = True
        Me.chkKaryo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkKaryo.Location = New System.Drawing.Point(735, 367)
        Me.chkKaryo.Name = "chkKaryo"
        Me.chkKaryo.Size = New System.Drawing.Size(60, 16)
        Me.chkKaryo.TabIndex = 43
        Me.chkKaryo.Text = "要加療"
        Me.chkKaryo.UseVisualStyleBackColor = True
        '
        'chkSeisa
        '
        Me.chkSeisa.AutoSize = True
        Me.chkSeisa.Checked = True
        Me.chkSeisa.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSeisa.Location = New System.Drawing.Point(669, 367)
        Me.chkSeisa.Name = "chkSeisa"
        Me.chkSeisa.Size = New System.Drawing.Size(60, 16)
        Me.chkSeisa.TabIndex = 42
        Me.chkSeisa.Text = "要精査"
        Me.chkSeisa.UseVisualStyleBackColor = True
        '
        'chkSaiken
        '
        Me.chkSaiken.AutoSize = True
        Me.chkSaiken.Checked = True
        Me.chkSaiken.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSaiken.Location = New System.Drawing.Point(604, 367)
        Me.chkSaiken.Name = "chkSaiken"
        Me.chkSaiken.Size = New System.Drawing.Size(60, 16)
        Me.chkSaiken.TabIndex = 41
        Me.chkSaiken.Text = "要再検"
        Me.chkSaiken.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Red
        Me.Label9.Location = New System.Drawing.Point(518, 368)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(83, 12)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "(デフォルト設定："
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(518, 388)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(211, 12)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "※全角カンマ（、）で区切って複数入力可能"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(416, 367)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(101, 12)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "医師指示キーワード"
        '
        'sijiWordBox
        '
        Me.sijiWordBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.sijiWordBox.Location = New System.Drawing.Point(418, 407)
        Me.sijiWordBox.Name = "sijiWordBox"
        Me.sijiWordBox.Size = New System.Drawing.Size(338, 19)
        Me.sijiWordBox.TabIndex = 37
        '
        'dgvResult
        '
        Me.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResult.Location = New System.Drawing.Point(59, 188)
        Me.dgvResult.Name = "dgvResult"
        Me.dgvResult.RowTemplate.Height = 21
        Me.dgvResult.Size = New System.Drawing.Size(337, 242)
        Me.dgvResult.TabIndex = 36
        '
        'sijiLabel
        '
        Me.sijiLabel.AutoSize = True
        Me.sijiLabel.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sijiLabel.Location = New System.Drawing.Point(159, 522)
        Me.sijiLabel.Name = "sijiLabel"
        Me.sijiLabel.Size = New System.Drawing.Size(0, 16)
        Me.sijiLabel.TabIndex = 35
        '
        'syokenLabel
        '
        Me.syokenLabel.AutoSize = True
        Me.syokenLabel.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.syokenLabel.Location = New System.Drawing.Point(159, 485)
        Me.syokenLabel.Name = "syokenLabel"
        Me.syokenLabel.Size = New System.Drawing.Size(0, 16)
        Me.syokenLabel.TabIndex = 34
        '
        'totalLabel
        '
        Me.totalLabel.AutoSize = True
        Me.totalLabel.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.totalLabel.Location = New System.Drawing.Point(159, 448)
        Me.totalLabel.Name = "totalLabel"
        Me.totalLabel.Size = New System.Drawing.Size(0, 16)
        Me.totalLabel.TabIndex = 33
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(306, 71)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 16)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "～"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(396, 131)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(69, 31)
        Me.btnPrint.TabIndex = 31
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnExecute
        '
        Me.btnExecute.Location = New System.Drawing.Point(328, 131)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Size = New System.Drawing.Size(69, 31)
        Me.btnExecute.TabIndex = 30
        Me.btnExecute.Text = "実行"
        Me.btnExecute.UseVisualStyleBackColor = True
        '
        'toYmdBox
        '
        Me.toYmdBox.boxType = 3
        Me.toYmdBox.DateText = ""
        Me.toYmdBox.EraLabelText = "R01"
        Me.toYmdBox.EraText = ""
        Me.toYmdBox.Location = New System.Drawing.Point(334, 55)
        Me.toYmdBox.MonthLabelText = "07"
        Me.toYmdBox.MonthText = ""
        Me.toYmdBox.Name = "toYmdBox"
        Me.toYmdBox.Size = New System.Drawing.Size(145, 46)
        Me.toYmdBox.TabIndex = 29
        Me.toYmdBox.textReadOnly = False
        '
        'fromYmdBox
        '
        Me.fromYmdBox.boxType = 3
        Me.fromYmdBox.DateText = ""
        Me.fromYmdBox.EraLabelText = "R01"
        Me.fromYmdBox.EraText = ""
        Me.fromYmdBox.Location = New System.Drawing.Point(157, 55)
        Me.fromYmdBox.MonthLabelText = "07"
        Me.fromYmdBox.MonthText = ""
        Me.fromYmdBox.Name = "fromYmdBox"
        Me.fromYmdBox.Size = New System.Drawing.Size(145, 46)
        Me.fromYmdBox.TabIndex = 28
        Me.fromYmdBox.textReadOnly = False
        '
        'indBox
        '
        Me.indBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.indBox.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.indBox.FormattingEnabled = True
        Me.indBox.Location = New System.Drawing.Point(158, 24)
        Me.indBox.Name = "indBox"
        Me.indBox.Size = New System.Drawing.Size(317, 24)
        Me.indBox.TabIndex = 27
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(56, 525)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(88, 16)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "医師指示数"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(56, 488)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "所見者数"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(56, 451)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 16)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "受診者数"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(56, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "受診日"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(56, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "事業所名"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(792, 368)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(9, 12)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = ")"
        '
        '健診結果報告書
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(932, 615)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.chkKaryo)
        Me.Controls.Add(Me.chkSeisa)
        Me.Controls.Add(Me.chkSaiken)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.sijiWordBox)
        Me.Controls.Add(Me.dgvResult)
        Me.Controls.Add(Me.sijiLabel)
        Me.Controls.Add(Me.syokenLabel)
        Me.Controls.Add(Me.totalLabel)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnExecute)
        Me.Controls.Add(Me.toYmdBox)
        Me.Controls.Add(Me.fromYmdBox)
        Me.Controls.Add(Me.indBox)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "健診結果報告書"
        Me.Text = "健診結果報告書"
        CType(Me.dgvResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkKaryo As System.Windows.Forms.CheckBox
    Friend WithEvents chkSeisa As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaiken As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents sijiWordBox As System.Windows.Forms.TextBox
    Friend WithEvents dgvResult As System.Windows.Forms.DataGridView
    Friend WithEvents sijiLabel As System.Windows.Forms.Label
    Friend WithEvents syokenLabel As System.Windows.Forms.Label
    Friend WithEvents totalLabel As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnExecute As System.Windows.Forms.Button
    Friend WithEvents toYmdBox As ymdBox.ymdBox
    Friend WithEvents fromYmdBox As ymdBox.ymdBox
    Friend WithEvents indBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
