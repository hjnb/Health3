<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class resultInputForm
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
        Me.components = New System.ComponentModel.Container()
        Me.historyListBox = New System.Windows.Forms.ListBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnRegist = New System.Windows.Forms.Button()
        Me.YmdBox = New ymdBox.ymdBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ageBox = New System.Windows.Forms.TextBox()
        Me.birthBox = New System.Windows.Forms.TextBox()
        Me.sexBox = New System.Windows.Forms.TextBox()
        Me.namBox = New System.Windows.Forms.TextBox()
        Me.indBox = New System.Windows.Forms.TextBox()
        Me.bangoBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.syuBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvInput = New Health3.resultInputDataGridView(Me.components)
        Me.Label4 = New System.Windows.Forms.Label()
        Me.eGFRBox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.dgvInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'historyListBox
        '
        Me.historyListBox.FormattingEnabled = True
        Me.historyListBox.ItemHeight = 12
        Me.historyListBox.Location = New System.Drawing.Point(434, 43)
        Me.historyListBox.Name = "historyListBox"
        Me.historyListBox.Size = New System.Drawing.Size(91, 64)
        Me.historyListBox.TabIndex = 49
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(767, 65)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(70, 30)
        Me.btnPrint.TabIndex = 48
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(698, 65)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(70, 30)
        Me.btnClear.TabIndex = 47
        Me.btnClear.Text = "クリア"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(629, 65)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(70, 30)
        Me.btnDelete.TabIndex = 46
        Me.btnDelete.Text = "削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnRegist
        '
        Me.btnRegist.Location = New System.Drawing.Point(560, 65)
        Me.btnRegist.Name = "btnRegist"
        Me.btnRegist.Size = New System.Drawing.Size(70, 30)
        Me.btnRegist.TabIndex = 45
        Me.btnRegist.Text = "登録"
        Me.btnRegist.UseVisualStyleBackColor = True
        '
        'YmdBox
        '
        Me.YmdBox.boxType = 1
        Me.YmdBox.DateText = ""
        Me.YmdBox.EraLabelText = "R01"
        Me.YmdBox.EraText = ""
        Me.YmdBox.Location = New System.Drawing.Point(108, 78)
        Me.YmdBox.MonthLabelText = "08"
        Me.YmdBox.MonthText = ""
        Me.YmdBox.Name = "YmdBox"
        Me.YmdBox.Size = New System.Drawing.Size(112, 30)
        Me.YmdBox.TabIndex = 44
        Me.YmdBox.textReadOnly = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 86)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "健診日"
        '
        'ageBox
        '
        Me.ageBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ageBox.ForeColor = System.Drawing.Color.Blue
        Me.ageBox.Location = New System.Drawing.Point(525, 12)
        Me.ageBox.Name = "ageBox"
        Me.ageBox.ReadOnly = True
        Me.ageBox.Size = New System.Drawing.Size(41, 19)
        Me.ageBox.TabIndex = 42
        Me.ageBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'birthBox
        '
        Me.birthBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.birthBox.ForeColor = System.Drawing.Color.Blue
        Me.birthBox.Location = New System.Drawing.Point(433, 12)
        Me.birthBox.Name = "birthBox"
        Me.birthBox.ReadOnly = True
        Me.birthBox.Size = New System.Drawing.Size(93, 19)
        Me.birthBox.TabIndex = 41
        Me.birthBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'sexBox
        '
        Me.sexBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.sexBox.ForeColor = System.Drawing.Color.Blue
        Me.sexBox.Location = New System.Drawing.Point(406, 12)
        Me.sexBox.Name = "sexBox"
        Me.sexBox.ReadOnly = True
        Me.sexBox.Size = New System.Drawing.Size(28, 19)
        Me.sexBox.TabIndex = 40
        Me.sexBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'namBox
        '
        Me.namBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.namBox.ForeColor = System.Drawing.Color.Blue
        Me.namBox.Location = New System.Drawing.Point(312, 12)
        Me.namBox.Name = "namBox"
        Me.namBox.ReadOnly = True
        Me.namBox.Size = New System.Drawing.Size(95, 19)
        Me.namBox.TabIndex = 39
        Me.namBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'indBox
        '
        Me.indBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.indBox.ForeColor = System.Drawing.Color.Blue
        Me.indBox.Location = New System.Drawing.Point(33, 12)
        Me.indBox.Name = "indBox"
        Me.indBox.ReadOnly = True
        Me.indBox.Size = New System.Drawing.Size(198, 19)
        Me.indBox.TabIndex = 38
        Me.indBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'bangoBox
        '
        Me.bangoBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.bangoBox.ForeColor = System.Drawing.Color.Blue
        Me.bangoBox.Location = New System.Drawing.Point(230, 12)
        Me.bangoBox.Name = "bangoBox"
        Me.bangoBox.ReadOnly = True
        Me.bangoBox.Size = New System.Drawing.Size(83, 19)
        Me.bangoBox.TabIndex = 50
        Me.bangoBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 12)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "健診の種類"
        '
        'syuBox
        '
        Me.syuBox.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.syuBox.Location = New System.Drawing.Point(110, 46)
        Me.syuBox.MaxLength = 1
        Me.syuBox.Name = "syuBox"
        Me.syuBox.Size = New System.Drawing.Size(31, 19)
        Me.syuBox.TabIndex = 52
        Me.syuBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(156, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(211, 12)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "(1：一般健診　2：一般健診及び付加健診)"
        '
        'dgvInput
        '
        Me.dgvInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInput.Location = New System.Drawing.Point(35, 122)
        Me.dgvInput.Name = "dgvInput"
        Me.dgvInput.RowTemplate.Height = 21
        Me.dgvInput.sex = Nothing
        Me.dgvInput.Size = New System.Drawing.Size(838, 579)
        Me.dgvInput.TabIndex = 54
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(901, 301)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 15)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "eGFR："
        '
        'eGFRBox
        '
        Me.eGFRBox.Font = New System.Drawing.Font("MS UI Gothic", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.eGFRBox.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.eGFRBox.Location = New System.Drawing.Point(949, 298)
        Me.eGFRBox.Name = "eGFRBox"
        Me.eGFRBox.Size = New System.Drawing.Size(46, 22)
        Me.eGFRBox.TabIndex = 56
        Me.eGFRBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Blue
        Me.Label5.Location = New System.Drawing.Point(902, 248)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(45, 12)
        Me.Label5.TabIndex = 57
        Me.Label5.Text = "整数値 "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(902, 280)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(160, 12)
        Me.Label6.TabIndex = 58
        Me.Label6.Text = "小数第一位まで入力して下さい "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(914, 263)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(15, 12)
        Me.Label7.TabIndex = 59
        Me.Label7.Text = "or"
        '
        'resultInputForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1065, 709)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.eGFRBox)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dgvInput)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.syuBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.bangoBox)
        Me.Controls.Add(Me.historyListBox)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnRegist)
        Me.Controls.Add(Me.YmdBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ageBox)
        Me.Controls.Add(Me.birthBox)
        Me.Controls.Add(Me.sexBox)
        Me.Controls.Add(Me.namBox)
        Me.Controls.Add(Me.indBox)
        Me.Name = "resultInputForm"
        Me.Text = "健診データ"
        CType(Me.dgvInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents historyListBox As System.Windows.Forms.ListBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnRegist As System.Windows.Forms.Button
    Friend WithEvents YmdBox As ymdBox.ymdBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ageBox As System.Windows.Forms.TextBox
    Friend WithEvents birthBox As System.Windows.Forms.TextBox
    Friend WithEvents sexBox As System.Windows.Forms.TextBox
    Friend WithEvents namBox As System.Windows.Forms.TextBox
    Friend WithEvents indBox As System.Windows.Forms.TextBox
    Friend WithEvents bangoBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents syuBox As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dgvInput As Health3.resultInputDataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents eGFRBox As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
