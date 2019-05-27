<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 受診者マスタ
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.indBox = New System.Windows.Forms.ComboBox()
        Me.bangoBox = New System.Windows.Forms.TextBox()
        Me.namBox = New System.Windows.Forms.TextBox()
        Me.kanaBox = New System.Windows.Forms.TextBox()
        Me.sexBox = New System.Windows.Forms.TextBox()
        Me.birthBox = New ymdBox.ymdBox()
        Me.kubunBox = New System.Windows.Forms.TextBox()
        Me.TelBox = New System.Windows.Forms.TextBox()
        Me.postBox = New System.Windows.Forms.TextBox()
        Me.jyuBox = New System.Windows.Forms.TextBox()
        Me.commentBox = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnRegist = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnPaper = New System.Windows.Forms.Button()
        Me.btnList = New System.Windows.Forms.Button()
        Me.rbtnPreview = New System.Windows.Forms.RadioButton()
        Me.rbtnPrint = New System.Windows.Forms.RadioButton()
        Me.dgvMaster = New System.Windows.Forms.DataGridView()
        Me.btnBasicPaperPrint = New System.Windows.Forms.Button()
        CType(Me.dgvMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(45, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "事業所名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(45, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "健保番号"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(45, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "性別"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(45, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 16)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "ＴＥＬ"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(45, 188)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 16)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "コメント"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(261, 80)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 16)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "氏名"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(501, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(32, 16)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "カナ"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(195, 116)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(89, 16)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "１：男　２：女"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(323, 116)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 16)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "生年月日"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(547, 116)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(96, 16)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "本人・配偶者"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(696, 116)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(137, 16)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "１：本人　２：配偶者"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(288, 150)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(24, 16)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "〒"
        '
        'indBox
        '
        Me.indBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.indBox.Font = New System.Drawing.Font("MS UI Gothic", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.indBox.FormattingEnabled = True
        Me.indBox.Location = New System.Drawing.Point(149, 36)
        Me.indBox.Name = "indBox"
        Me.indBox.Size = New System.Drawing.Size(331, 27)
        Me.indBox.TabIndex = 12
        '
        'bangoBox
        '
        Me.bangoBox.Font = New System.Drawing.Font("MS UI Gothic", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.bangoBox.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.bangoBox.Location = New System.Drawing.Point(149, 74)
        Me.bangoBox.Name = "bangoBox"
        Me.bangoBox.Size = New System.Drawing.Size(84, 25)
        Me.bangoBox.TabIndex = 13
        '
        'namBox
        '
        Me.namBox.Font = New System.Drawing.Font("MS UI Gothic", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.namBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.namBox.Location = New System.Drawing.Point(322, 74)
        Me.namBox.Name = "namBox"
        Me.namBox.Size = New System.Drawing.Size(158, 25)
        Me.namBox.TabIndex = 14
        '
        'kanaBox
        '
        Me.kanaBox.Font = New System.Drawing.Font("MS UI Gothic", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.kanaBox.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.kanaBox.Location = New System.Drawing.Point(549, 74)
        Me.kanaBox.Name = "kanaBox"
        Me.kanaBox.Size = New System.Drawing.Size(158, 25)
        Me.kanaBox.TabIndex = 15
        '
        'sexBox
        '
        Me.sexBox.Font = New System.Drawing.Font("MS UI Gothic", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.sexBox.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.sexBox.Location = New System.Drawing.Point(149, 111)
        Me.sexBox.MaxLength = 1
        Me.sexBox.Name = "sexBox"
        Me.sexBox.Size = New System.Drawing.Size(42, 25)
        Me.sexBox.TabIndex = 16
        Me.sexBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'birthBox
        '
        Me.birthBox.boxType = 1
        Me.birthBox.DateText = ""
        Me.birthBox.EraLabelText = "R01"
        Me.birthBox.EraText = ""
        Me.birthBox.Location = New System.Drawing.Point(410, 109)
        Me.birthBox.MonthLabelText = "05"
        Me.birthBox.MonthText = ""
        Me.birthBox.Name = "birthBox"
        Me.birthBox.Size = New System.Drawing.Size(112, 30)
        Me.birthBox.TabIndex = 17
        '
        'kubunBox
        '
        Me.kubunBox.Font = New System.Drawing.Font("MS UI Gothic", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.kubunBox.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.kubunBox.Location = New System.Drawing.Point(649, 111)
        Me.kubunBox.MaxLength = 1
        Me.kubunBox.Name = "kubunBox"
        Me.kubunBox.Size = New System.Drawing.Size(42, 25)
        Me.kubunBox.TabIndex = 18
        Me.kubunBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TelBox
        '
        Me.TelBox.Font = New System.Drawing.Font("MS UI Gothic", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TelBox.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TelBox.Location = New System.Drawing.Point(149, 147)
        Me.TelBox.Name = "TelBox"
        Me.TelBox.Size = New System.Drawing.Size(123, 25)
        Me.TelBox.TabIndex = 19
        '
        'postBox
        '
        Me.postBox.Font = New System.Drawing.Font("MS UI Gothic", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.postBox.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.postBox.Location = New System.Drawing.Point(322, 147)
        Me.postBox.Name = "postBox"
        Me.postBox.Size = New System.Drawing.Size(82, 25)
        Me.postBox.TabIndex = 20
        '
        'jyuBox
        '
        Me.jyuBox.Font = New System.Drawing.Font("MS UI Gothic", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.jyuBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.jyuBox.Location = New System.Drawing.Point(410, 147)
        Me.jyuBox.Name = "jyuBox"
        Me.jyuBox.Size = New System.Drawing.Size(438, 25)
        Me.jyuBox.TabIndex = 21
        '
        'commentBox
        '
        Me.commentBox.Font = New System.Drawing.Font("MS UI Gothic", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.commentBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.commentBox.Location = New System.Drawing.Point(149, 184)
        Me.commentBox.Name = "commentBox"
        Me.commentBox.Size = New System.Drawing.Size(427, 25)
        Me.commentBox.TabIndex = 22
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(102, 250)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(200, 14)
        Me.Label13.TabIndex = 23
        Me.Label13.Text = "ﾀﾞﾌﾞﾙｸﾘｯｸした項目名で並べます。"
        '
        'btnRegist
        '
        Me.btnRegist.Location = New System.Drawing.Point(346, 226)
        Me.btnRegist.Name = "btnRegist"
        Me.btnRegist.Size = New System.Drawing.Size(67, 33)
        Me.btnRegist.TabIndex = 24
        Me.btnRegist.Text = "登録"
        Me.btnRegist.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(412, 226)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(67, 33)
        Me.btnDelete.TabIndex = 25
        Me.btnDelete.Text = "削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnPaper
        '
        Me.btnPaper.Location = New System.Drawing.Point(478, 226)
        Me.btnPaper.Name = "btnPaper"
        Me.btnPaper.Size = New System.Drawing.Size(67, 33)
        Me.btnPaper.TabIndex = 26
        Me.btnPaper.Text = "診断書"
        Me.btnPaper.UseVisualStyleBackColor = True
        '
        'btnList
        '
        Me.btnList.Location = New System.Drawing.Point(544, 226)
        Me.btnList.Name = "btnList"
        Me.btnList.Size = New System.Drawing.Size(67, 33)
        Me.btnList.TabIndex = 27
        Me.btnList.Text = "名簿"
        Me.btnList.UseVisualStyleBackColor = True
        '
        'rbtnPreview
        '
        Me.rbtnPreview.AutoSize = True
        Me.rbtnPreview.Location = New System.Drawing.Point(644, 234)
        Me.rbtnPreview.Name = "rbtnPreview"
        Me.rbtnPreview.Size = New System.Drawing.Size(63, 16)
        Me.rbtnPreview.TabIndex = 28
        Me.rbtnPreview.TabStop = True
        Me.rbtnPreview.Text = "ﾌﾟﾚﾋﾞｭｰ"
        Me.rbtnPreview.UseVisualStyleBackColor = True
        '
        'rbtnPrint
        '
        Me.rbtnPrint.AutoSize = True
        Me.rbtnPrint.Location = New System.Drawing.Point(727, 234)
        Me.rbtnPrint.Name = "rbtnPrint"
        Me.rbtnPrint.Size = New System.Drawing.Size(47, 16)
        Me.rbtnPrint.TabIndex = 29
        Me.rbtnPrint.TabStop = True
        Me.rbtnPrint.Text = "印刷"
        Me.rbtnPrint.UseVisualStyleBackColor = True
        '
        'dgvMaster
        '
        Me.dgvMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMaster.Location = New System.Drawing.Point(105, 274)
        Me.dgvMaster.Name = "dgvMaster"
        Me.dgvMaster.RowTemplate.Height = 21
        Me.dgvMaster.Size = New System.Drawing.Size(779, 402)
        Me.dgvMaster.TabIndex = 30
        '
        'btnBasicPaperPrint
        '
        Me.btnBasicPaperPrint.Location = New System.Drawing.Point(916, 274)
        Me.btnBasicPaperPrint.Name = "btnBasicPaperPrint"
        Me.btnBasicPaperPrint.Size = New System.Drawing.Size(130, 36)
        Me.btnBasicPaperPrint.TabIndex = 120
        Me.btnBasicPaperPrint.Text = "基本項目一括印刷"
        Me.btnBasicPaperPrint.UseVisualStyleBackColor = True
        '
        '受診者マスタ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1075, 691)
        Me.Controls.Add(Me.btnBasicPaperPrint)
        Me.Controls.Add(Me.dgvMaster)
        Me.Controls.Add(Me.rbtnPrint)
        Me.Controls.Add(Me.rbtnPreview)
        Me.Controls.Add(Me.btnList)
        Me.Controls.Add(Me.btnPaper)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnRegist)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.commentBox)
        Me.Controls.Add(Me.jyuBox)
        Me.Controls.Add(Me.postBox)
        Me.Controls.Add(Me.TelBox)
        Me.Controls.Add(Me.kubunBox)
        Me.Controls.Add(Me.birthBox)
        Me.Controls.Add(Me.sexBox)
        Me.Controls.Add(Me.kanaBox)
        Me.Controls.Add(Me.namBox)
        Me.Controls.Add(Me.bangoBox)
        Me.Controls.Add(Me.indBox)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "受診者マスタ"
        Me.Text = "受診者マスタ"
        CType(Me.dgvMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents indBox As System.Windows.Forms.ComboBox
    Friend WithEvents bangoBox As System.Windows.Forms.TextBox
    Friend WithEvents namBox As System.Windows.Forms.TextBox
    Friend WithEvents kanaBox As System.Windows.Forms.TextBox
    Friend WithEvents sexBox As System.Windows.Forms.TextBox
    Friend WithEvents birthBox As ymdBox.ymdBox
    Friend WithEvents kubunBox As System.Windows.Forms.TextBox
    Friend WithEvents TelBox As System.Windows.Forms.TextBox
    Friend WithEvents postBox As System.Windows.Forms.TextBox
    Friend WithEvents jyuBox As System.Windows.Forms.TextBox
    Friend WithEvents commentBox As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnRegist As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnPaper As System.Windows.Forms.Button
    Friend WithEvents btnList As System.Windows.Forms.Button
    Friend WithEvents rbtnPreview As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnPrint As System.Windows.Forms.RadioButton
    Friend WithEvents dgvMaster As System.Windows.Forms.DataGridView
    Friend WithEvents btnBasicPaperPrint As System.Windows.Forms.Button
End Class
