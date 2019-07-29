<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TopForm
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
        Me.btnOfficeMaster = New System.Windows.Forms.Button()
        Me.btnExamineeMaster = New System.Windows.Forms.Button()
        Me.btnResultFD = New System.Windows.Forms.Button()
        Me.btnExamineeList = New System.Windows.Forms.Button()
        Me.btnResultReport = New System.Windows.Forms.Button()
        Me.btnImplementationHistory = New System.Windows.Forms.Button()
        Me.topPicture = New System.Windows.Forms.PictureBox()
        CType(Me.topPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOfficeMaster
        '
        Me.btnOfficeMaster.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnOfficeMaster.Location = New System.Drawing.Point(98, 53)
        Me.btnOfficeMaster.Name = "btnOfficeMaster"
        Me.btnOfficeMaster.Size = New System.Drawing.Size(258, 103)
        Me.btnOfficeMaster.TabIndex = 0
        Me.btnOfficeMaster.Text = "事業所マスタ"
        Me.btnOfficeMaster.UseVisualStyleBackColor = True
        '
        'btnExamineeMaster
        '
        Me.btnExamineeMaster.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExamineeMaster.Location = New System.Drawing.Point(98, 155)
        Me.btnExamineeMaster.Name = "btnExamineeMaster"
        Me.btnExamineeMaster.Size = New System.Drawing.Size(258, 103)
        Me.btnExamineeMaster.TabIndex = 1
        Me.btnExamineeMaster.Text = "受診者マスタ"
        Me.btnExamineeMaster.UseVisualStyleBackColor = True
        '
        'btnResultFD
        '
        Me.btnResultFD.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnResultFD.Location = New System.Drawing.Point(98, 257)
        Me.btnResultFD.Name = "btnResultFD"
        Me.btnResultFD.Size = New System.Drawing.Size(258, 103)
        Me.btnResultFD.TabIndex = 2
        Me.btnResultFD.Text = "健診結果ＦＤ"
        Me.btnResultFD.UseVisualStyleBackColor = True
        '
        'btnExamineeList
        '
        Me.btnExamineeList.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnExamineeList.Location = New System.Drawing.Point(355, 257)
        Me.btnExamineeList.Name = "btnExamineeList"
        Me.btnExamineeList.Size = New System.Drawing.Size(258, 103)
        Me.btnExamineeList.TabIndex = 4
        Me.btnExamineeList.Text = "受診者一覧"
        Me.btnExamineeList.UseVisualStyleBackColor = True
        '
        'btnResultReport
        '
        Me.btnResultReport.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnResultReport.Location = New System.Drawing.Point(355, 359)
        Me.btnResultReport.Name = "btnResultReport"
        Me.btnResultReport.Size = New System.Drawing.Size(258, 103)
        Me.btnResultReport.TabIndex = 5
        Me.btnResultReport.Text = "健診結果報告書"
        Me.btnResultReport.UseVisualStyleBackColor = True
        '
        'btnImplementationHistory
        '
        Me.btnImplementationHistory.Font = New System.Drawing.Font("MS UI Gothic", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnImplementationHistory.Location = New System.Drawing.Point(355, 461)
        Me.btnImplementationHistory.Name = "btnImplementationHistory"
        Me.btnImplementationHistory.Size = New System.Drawing.Size(258, 103)
        Me.btnImplementationHistory.TabIndex = 6
        Me.btnImplementationHistory.Text = "事業所別実施履歴"
        Me.btnImplementationHistory.UseVisualStyleBackColor = True
        '
        'topPicture
        '
        Me.topPicture.Location = New System.Drawing.Point(431, 81)
        Me.topPicture.Name = "topPicture"
        Me.topPicture.Size = New System.Drawing.Size(173, 155)
        Me.topPicture.TabIndex = 7
        Me.topPicture.TabStop = False
        '
        'TopForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(773, 653)
        Me.Controls.Add(Me.topPicture)
        Me.Controls.Add(Me.btnImplementationHistory)
        Me.Controls.Add(Me.btnResultReport)
        Me.Controls.Add(Me.btnExamineeList)
        Me.Controls.Add(Me.btnResultFD)
        Me.Controls.Add(Me.btnExamineeMaster)
        Me.Controls.Add(Me.btnOfficeMaster)
        Me.Name = "TopForm"
        Me.Text = "生活習慣病予防健診"
        CType(Me.topPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOfficeMaster As System.Windows.Forms.Button
    Friend WithEvents btnExamineeMaster As System.Windows.Forms.Button
    Friend WithEvents btnResultFD As System.Windows.Forms.Button
    Friend WithEvents btnExamineeList As System.Windows.Forms.Button
    Friend WithEvents btnResultReport As System.Windows.Forms.Button
    Friend WithEvents btnImplementationHistory As System.Windows.Forms.Button
    Friend WithEvents topPicture As System.Windows.Forms.PictureBox

End Class
