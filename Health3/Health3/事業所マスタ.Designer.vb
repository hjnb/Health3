<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 事業所マスタ
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
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.indBox = New System.Windows.Forms.TextBox()
        Me.kanaBox = New System.Windows.Forms.TextBox()
        Me.kigo4Box = New System.Windows.Forms.TextBox()
        Me.fugo6Box = New System.Windows.Forms.TextBox()
        Me.postBox = New System.Windows.Forms.TextBox()
        Me.jyuBox = New System.Windows.Forms.TextBox()
        Me.telBox = New System.Windows.Forms.TextBox()
        Me.faxBox = New System.Windows.Forms.TextBox()
        Me.tantoBox = New System.Windows.Forms.TextBox()
        Me.btnRegist = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.dgvIndM = New System.Windows.Forms.DataGridView()
        Me.Label14 = New System.Windows.Forms.Label()
        CType(Me.dgvIndM, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(75, 195)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 16)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "ＴＥＬ"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(75, 173)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 16)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "住所"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(75, 128)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 16)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "健保符号"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(75, 151)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 16)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "〒"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(75, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "健保記号"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(75, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "カナ"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(75, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "事業所名"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(75, 216)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 16)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "ＦＡＸ"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(75, 240)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 16)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "担当者"
        '
        'indBox
        '
        Me.indBox.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.indBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.indBox.Location = New System.Drawing.Point(177, 41)
        Me.indBox.Name = "indBox"
        Me.indBox.Size = New System.Drawing.Size(262, 23)
        Me.indBox.TabIndex = 16
        '
        'kanaBox
        '
        Me.kanaBox.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.kanaBox.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.kanaBox.Location = New System.Drawing.Point(177, 71)
        Me.kanaBox.Name = "kanaBox"
        Me.kanaBox.Size = New System.Drawing.Size(62, 23)
        Me.kanaBox.TabIndex = 17
        '
        'kigo4Box
        '
        Me.kigo4Box.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.kigo4Box.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.kigo4Box.Location = New System.Drawing.Point(177, 103)
        Me.kigo4Box.Name = "kigo4Box"
        Me.kigo4Box.Size = New System.Drawing.Size(89, 23)
        Me.kigo4Box.TabIndex = 18
        '
        'fugo6Box
        '
        Me.fugo6Box.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.fugo6Box.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.fugo6Box.Location = New System.Drawing.Point(177, 125)
        Me.fugo6Box.Name = "fugo6Box"
        Me.fugo6Box.Size = New System.Drawing.Size(89, 23)
        Me.fugo6Box.TabIndex = 19
        '
        'postBox
        '
        Me.postBox.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.postBox.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.postBox.Location = New System.Drawing.Point(177, 147)
        Me.postBox.Name = "postBox"
        Me.postBox.Size = New System.Drawing.Size(89, 23)
        Me.postBox.TabIndex = 20
        '
        'jyuBox
        '
        Me.jyuBox.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.jyuBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.jyuBox.Location = New System.Drawing.Point(177, 169)
        Me.jyuBox.Name = "jyuBox"
        Me.jyuBox.Size = New System.Drawing.Size(262, 23)
        Me.jyuBox.TabIndex = 21
        '
        'telBox
        '
        Me.telBox.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.telBox.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.telBox.Location = New System.Drawing.Point(177, 191)
        Me.telBox.Name = "telBox"
        Me.telBox.Size = New System.Drawing.Size(135, 23)
        Me.telBox.TabIndex = 22
        '
        'faxBox
        '
        Me.faxBox.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.faxBox.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.faxBox.Location = New System.Drawing.Point(177, 213)
        Me.faxBox.Name = "faxBox"
        Me.faxBox.Size = New System.Drawing.Size(135, 23)
        Me.faxBox.TabIndex = 23
        '
        'tantoBox
        '
        Me.tantoBox.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.tantoBox.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.tantoBox.Location = New System.Drawing.Point(177, 235)
        Me.tantoBox.Name = "tantoBox"
        Me.tantoBox.Size = New System.Drawing.Size(135, 23)
        Me.tantoBox.TabIndex = 24
        '
        'btnRegist
        '
        Me.btnRegist.Location = New System.Drawing.Point(521, 216)
        Me.btnRegist.Name = "btnRegist"
        Me.btnRegist.Size = New System.Drawing.Size(75, 31)
        Me.btnRegist.TabIndex = 25
        Me.btnRegist.Text = "登録"
        Me.btnRegist.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(595, 216)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 31)
        Me.btnClear.TabIndex = 26
        Me.btnClear.Text = "ｸﾘｱ"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(669, 216)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 31)
        Me.btnDelete.TabIndex = 27
        Me.btnDelete.Text = "削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(743, 216)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 31)
        Me.btnPrint.TabIndex = 28
        Me.btnPrint.Text = "印刷"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'dgvIndM
        '
        Me.dgvIndM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvIndM.Location = New System.Drawing.Point(26, 292)
        Me.dgvIndM.Name = "dgvIndM"
        Me.dgvIndM.RowTemplate.Height = 21
        Me.dgvIndM.Size = New System.Drawing.Size(998, 312)
        Me.dgvIndM.TabIndex = 29
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(355, 275)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(200, 14)
        Me.Label14.TabIndex = 32
        Me.Label14.Text = "ﾀﾞﾌﾞﾙｸﾘｯｸした項目名で並べます。"
        '
        '事業所マスタ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1053, 645)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.dgvIndM)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnRegist)
        Me.Controls.Add(Me.tantoBox)
        Me.Controls.Add(Me.faxBox)
        Me.Controls.Add(Me.telBox)
        Me.Controls.Add(Me.jyuBox)
        Me.Controls.Add(Me.postBox)
        Me.Controls.Add(Me.fugo6Box)
        Me.Controls.Add(Me.kigo4Box)
        Me.Controls.Add(Me.kanaBox)
        Me.Controls.Add(Me.indBox)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "事業所マスタ"
        Me.Text = "事業所マスタ"
        CType(Me.dgvIndM, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents indBox As System.Windows.Forms.TextBox
    Friend WithEvents kanaBox As System.Windows.Forms.TextBox
    Friend WithEvents kigo4Box As System.Windows.Forms.TextBox
    Friend WithEvents fugo6Box As System.Windows.Forms.TextBox
    Friend WithEvents postBox As System.Windows.Forms.TextBox
    Friend WithEvents jyuBox As System.Windows.Forms.TextBox
    Friend WithEvents telBox As System.Windows.Forms.TextBox
    Friend WithEvents faxBox As System.Windows.Forms.TextBox
    Friend WithEvents tantoBox As System.Windows.Forms.TextBox
    Friend WithEvents btnRegist As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents dgvIndM As System.Windows.Forms.DataGridView
    Friend WithEvents Label14 As System.Windows.Forms.Label
End Class
