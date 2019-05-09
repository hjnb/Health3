Imports System.Data.OleDb

Public Class 受診者マスタ

    ''' <summary>
    ''' 行ヘッダーのカレントセルを表す三角マークを非表示に設定する為のクラス。
    ''' </summary>
    ''' <remarks></remarks>
    Public Class dgvRowHeaderCell

        'DataGridViewRowHeaderCell を継承
        Inherits DataGridViewRowHeaderCell

        'DataGridViewHeaderCell.Paint をオーバーライドして行ヘッダーを描画
        Protected Overrides Sub Paint(ByVal graphics As Graphics, ByVal clipBounds As Rectangle, _
           ByVal cellBounds As Rectangle, ByVal rowIndex As Integer, ByVal cellState As DataGridViewElementStates, _
           ByVal value As Object, ByVal formattedValue As Object, ByVal errorText As String, _
           ByVal cellStyle As DataGridViewCellStyle, ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle, _
           ByVal paintParts As DataGridViewPaintParts)
            '標準セルの描画からセル内容の背景だけ除いた物を描画(-5)
            MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, _
                     formattedValue, errorText, cellStyle, advancedBorderStyle, _
                     Not DataGridViewPaintParts.ContentBackground)
        End Sub

    End Class

    ''' <summary>
    ''' loadイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 受診者マスタ_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.KeyPreview = True

        '印刷ラジオボタン初期値設定
        initPrintState()

        '事業所名ボックス初期設定
        initIndBox()

        'データグリッドビュー初期設定
        initDgvMaster()
    End Sub

    ''' <summary>
    ''' keyDownイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 受診者マスタ_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            If e.Control = False Then
                Me.SelectNextControl(Me.ActiveControl, Not e.Shift, True, True, True)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 事業所名ボックス初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initIndBox()
        indBox.ImeMode = Windows.Forms.ImeMode.Hiragana

        indBox.Items.Clear()
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Health3)
        Dim sql As String = "SELECT Ind FROM IndM ORDER BY Kana"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic)
        While Not rs.EOF
            Dim txt As String = Util.checkDBNullValue(rs.Fields("Ind").Value)
            indBox.Items.Add(txt)
            rs.MoveNext()
        End While
        rs.Close()
        cn.Close()
    End Sub

    ''' <summary>
    ''' データグリッドビュー初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initDgvMaster()
        Util.EnableDoubleBuffering(dgvMaster)

        With dgvMaster
            .AllowUserToAddRows = False '行追加禁止
            .AllowUserToResizeColumns = False '列の幅をユーザーが変更できないようにする
            .AllowUserToResizeRows = False '行の高さをユーザーが変更できないようにする
            .AllowUserToDeleteRows = False '行削除禁止
            .BorderStyle = BorderStyle.FixedSingle
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control)
            .DefaultCellStyle.ForeColor = Color.Black
            .DefaultCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Control)
            .DefaultCellStyle.SelectionForeColor = Color.Black
            .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .RowHeadersWidth = 35
            .RowTemplate.Height = 18
            .RowTemplate.HeaderCell = New dgvRowHeaderCell() '行ヘッダの三角マークを非表示に
            .BackgroundColor = Color.FromKnownColor(KnownColor.Control)
            .ShowCellToolTips = False
            .EnableHeadersVisualStyles = False
            .Font = New Font("ＭＳ Ｐゴシック", 10)
            .ReadOnly = False
        End With
    End Sub

    ''' <summary>
    ''' 印刷ラジオボタン初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initPrintState()
        Dim state As String = Util.getIniString("System", "Printer", TopForm.iniFilePath)
        If state = "Y" Then
            rbtnPrint.Checked = True
        Else
            rbtnPreview.Checked = True
        End If
    End Sub

    ''' <summary>
    ''' ﾌﾟﾚﾋﾞｭｰラジオボタン値変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbtnPreview_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbtnPreview.CheckedChanged
        If rbtnPreview.Checked = True Then
            Util.putIniString("System", "Printer", "N", TopForm.iniFilePath)
        End If
    End Sub

    ''' <summary>
    ''' 印刷ラジオボタン値変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rbtnPrint_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbtnPrint.CheckedChanged
        If rbtnPrint.Checked = True Then
            Util.putIniString("System", "Printer", "Y", TopForm.iniFilePath)
        End If
    End Sub

    ''' <summary>
    ''' 対象事業所のデータ一覧表示
    ''' </summary>
    ''' <param name="ind">事業所名</param>
    ''' <remarks></remarks>
    Private Sub displayDgvMaster(ind As String)
        '内容クリア
        dgvMaster.Columns.Clear()
        bangoBox.Text = ""
        namBox.Text = ""
        kanaBox.Text = ""
        sexBox.Text = ""
        birthBox.clearText()
        kubunBox.Text = ""
        TelBox.Text = ""
        postBox.Text = ""
        jyuBox.Text = ""
        commentBox.Text = ""

        'データ取得
        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Health3)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select U.Bango, U.Nam, U.Kana, U.Sex, U.Birth, Int((Format(NOW(),'YYYYMMDD')-Format(U.Birth, 'YYYYMMDD'))/10000) as Age, U.Kubun, K.LastDate, U.Tel, U.Post, U.Jyu, U.Text from (select * from UsrM where Ind = '" & ind & "') as U left join (select Kana, Max(Ymd) as LastDate from KenD group by Kana) as K on U.Kana = K.Kana order by U.Kana"
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
        Dim da As OleDbDataAdapter = New OleDbDataAdapter()
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, rs, "UsrM")
        Dim dt As DataTable = ds.Tables("UsrM")

        '列追加
        dt.Columns.Add("List", GetType(Boolean)) '名簿
        For Each row As DataRow In dt.Rows
            '名簿列デフォルトでチェック有にする
            row("List") = True
        Next

        '表示
        dgvMaster.DataSource = dt
        cnn.Close()

        '幅設定等
        With dgvMaster
            With .Columns("List")
                .DisplayIndex = 0
                .HeaderText = "名簿"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 35
                .HeaderCell.Style.Font = New Font("ＭＳ Ｐゴシック", 9)
            End With
            With .Columns("Bango")
                .HeaderText = "健保番号"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 90
                .ReadOnly = True
            End With
            With .Columns("Nam")
                .HeaderText = "氏名"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 130
                .Frozen = True
                .ReadOnly = True
            End With
            With .Columns("Kana")
                .HeaderText = "カナ"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 130
                .ReadOnly = True
            End With
            With .Columns("Sex")
                .HeaderText = "性別"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 40
                .HeaderCell.Style.Font = New Font("ＭＳ Ｐゴシック", 9)
                .ReadOnly = True
            End With
            With .Columns("Birth")
                .HeaderText = "生年月日"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 95
                .ReadOnly = True
            End With
            With .Columns("Age")
                .HeaderText = "年齢"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 55
                .HeaderCell.Style.Font = New Font("ＭＳ Ｐゴシック", 9)
                .ReadOnly = True
            End With
            With .Columns("Kubun")
                .HeaderText = "本/配"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 55
                .HeaderCell.Style.Font = New Font("ＭＳ Ｐゴシック", 9)
                .ReadOnly = True
            End With
            With .Columns("LastDate")
                .HeaderText = "実施日"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 95
                .ReadOnly = True
            End With
            With .Columns("Tel")
                .HeaderText = "TEL"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 110
                .ReadOnly = True
            End With
            With .Columns("Post")
                .HeaderText = "〒"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 90
                .ReadOnly = True
            End With
            With .Columns("Jyu")
                .HeaderText = "住所"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 370
                .ReadOnly = True
            End With
            With .Columns("Text")
                .HeaderText = "コメント"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 350
                .ReadOnly = True
            End With
        End With

        'フォーカス
        bangoBox.Focus()

    End Sub

    ''' <summary>
    ''' 事業所名ボックス値変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub indBox_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles indBox.SelectedValueChanged
        Dim ind As String = indBox.Text
        If ind <> "" Then
            displayDgvMaster(ind)
        End If
    End Sub

    ''' <summary>
    ''' セルマウスクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvMaster_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvMaster.CellMouseClick
        If e.RowIndex >= 0 Then
            '値取得
            Dim bango As String = Util.checkDBNullValue(dgvMaster("Bango", e.RowIndex).Value)
            Dim nam As String = Util.checkDBNullValue(dgvMaster("Nam", e.RowIndex).Value)
            Dim kana As String = Util.checkDBNullValue(dgvMaster("Kana", e.RowIndex).Value)
            Dim sex As String = Util.checkDBNullValue(dgvMaster("Sex", e.RowIndex).Value)
            Dim birth As String = Util.checkDBNullValue(dgvMaster("Birth", e.RowIndex).Value)
            Dim kubun As String = Util.checkDBNullValue(dgvMaster("Kubun", e.RowIndex).Value)
            Dim tel As String = Util.checkDBNullValue(dgvMaster("Tel", e.RowIndex).Value)
            Dim post As String = Util.checkDBNullValue(dgvMaster("Post", e.RowIndex).Value)
            Dim jyu As String = Util.checkDBNullValue(dgvMaster("Jyu", e.RowIndex).Value)
            Dim comment As String = Util.checkDBNullValue(dgvMaster("Text", e.RowIndex).Value)

            '各ボックスへセット
            bangoBox.Text = bango
            namBox.Text = nam
            kanaBox.Text = kana
            sexBox.Text = sex
            birthBox.setWarekiStr(birth)
            kubunBox.Text = kubun
            telBox.Text = tel
            postBox.Text = post
            jyuBox.Text = jyu
            commentBox.Text = comment
        End If
    End Sub

    ''' <summary>
    ''' cellFormatingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvMaster_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvMaster.CellFormatting
        If e.RowIndex >= 0 AndAlso dgvMaster.Columns(e.ColumnIndex).Name = "LastDate" Then
            e.Value = Util.convADStrToWarekiStr(Util.checkDBNullValue(e.Value))
            e.FormattingApplied = True
        End If
    End Sub

    ''' <summary>
    ''' CellPaintingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvMaster_CellPainting(sender As Object, e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvMaster.CellPainting
        '行ヘッダーかどうか調べる
        If e.ColumnIndex < 0 AndAlso e.RowIndex >= 0 Then
            'セルを描画する
            e.Paint(e.ClipBounds, DataGridViewPaintParts.All)

            '行番号を描画する範囲を決定する
            'e.AdvancedBorderStyleやe.CellStyle.Paddingは無視しています
            Dim indexRect As Rectangle = e.CellBounds
            indexRect.Inflate(-2, -2)
            '行番号を描画する
            TextRenderer.DrawText(e.Graphics, _
                (e.RowIndex + 1).ToString(), _
                e.CellStyle.Font, _
                indexRect, _
                e.CellStyle.ForeColor, _
                TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)
            '描画が完了したことを知らせる
            e.Handled = True
        End If
        '選択したセルに枠を付ける
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 AndAlso (e.PaintParts And DataGridViewPaintParts.Background) = DataGridViewPaintParts.Background Then
            e.Graphics.FillRectangle(New SolidBrush(e.CellStyle.BackColor), e.CellBounds)

            If (e.PaintParts And DataGridViewPaintParts.SelectionBackground) = DataGridViewPaintParts.SelectionBackground AndAlso (e.State And DataGridViewElementStates.Selected) = DataGridViewElementStates.Selected Then
                e.Graphics.DrawRectangle(New Pen(Color.Black, 2I), e.CellBounds.X + 1I, e.CellBounds.Y + 1I, e.CellBounds.Width - 3I, e.CellBounds.Height - 3I)
            End If

            Dim pParts As DataGridViewPaintParts = e.PaintParts And Not DataGridViewPaintParts.Background
            e.Paint(e.ClipBounds, pParts)
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' 列ヘッダーダブルクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvMaster_ColumnHeaderMouseDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvMaster.ColumnHeaderMouseDoubleClick
        Dim targetColumn As DataGridViewColumn = dgvMaster.Columns(e.ColumnIndex) '選択列
        dgvMaster.Sort(targetColumn, System.ComponentModel.ListSortDirection.Ascending) '昇順でソート
    End Sub

    ''' <summary>
    ''' 登録ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRegist_Click(sender As System.Object, e As System.EventArgs) Handles btnRegist.Click
        '事業所名
        Dim ind As String = indBox.Text
        If ind = "" Then
            MsgBox("事業所名を選択して下さい。", MsgBoxStyle.Exclamation)
            indBox.Focus()
            indBox.DroppedDown = True
            Return
        End If
        '健保番号
        Dim bango As String = bangoBox.Text
        If bango = "" Then
            MsgBox("健保番号を入力して下さい。", MsgBoxStyle.Exclamation)
            bangoBox.Focus()
            Return
        ElseIf Not System.Text.RegularExpressions.Regex.IsMatch(bango, "^\d+$") Then
            MsgBox("健保番号は数値を入力して下さい。", MsgBoxStyle.Exclamation)
            bangoBox.Focus()
            Return
        End If
        '氏名
        Dim nam As String = namBox.Text
        If nam = "" Then
            MsgBox("氏名を入力して下さい。", MsgBoxStyle.Exclamation)
            namBox.Focus()
            Return
        End If
        'カナ
        Dim kana As String = kanaBox.Text
        If kana = "" Then
            MsgBox("カナを入力して下さい。", MsgBoxStyle.Exclamation)
            kanaBox.Focus()
            Return
        End If
        '性別
        Dim sex As String = sexBox.Text
        If Not (sex = "1" OrElse sex = "2") Then
            MsgBox("性別を正しく入力して下さい。", MsgBoxStyle.Exclamation)
            sexBox.Focus()
            Return
        End If
        '生年月日
        Dim birth As String = birthBox.getWarekiStr()
        If birth = "" Then
            MsgBox("生年月日を入力して下さい。", MsgBoxStyle.Exclamation)
            birthBox.Focus()
            Return
        End If
        '本人・配偶者
        Dim kubun As String = kubunBox.Text
        If Not (kubun = "1" OrElse kubun = "2") Then
            MsgBox("本人・配偶者を正しく入力して下さい。", MsgBoxStyle.Exclamation)
            kubunBox.Focus()
            Return
        End If
        'TEL
        Dim tel As String = TelBox.Text
        '郵便番号
        Dim post As String = postBox.Text
        '住所
        Dim jyu As String = jyuBox.Text
        'コメント
        Dim comment As String = commentBox.Text

        '登録
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Health3)
        Dim sql As String = "select * from UsrM where Ind = '" & ind & "' and Kana = '" & kana & "'"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If rs.RecordCount <= 0 Then
            '新規登録
            rs.AddNew()
            rs.Fields("Ind").Value = ind
            rs.Fields("Bango").Value = bango
            rs.Fields("Nam").Value = nam
            rs.Fields("Kana").Value = kana
            rs.Fields("Sex").Value = sex
            rs.Fields("Birth").Value = birth
            rs.Fields("Kubun").Value = kubun
            rs.Fields("Tel").Value = tel
            rs.Fields("Post").Value = post
            rs.Fields("Jyu").Value = jyu
            rs.Fields("Text").Value = comment
            rs.Update()
            rs.Close()
            cn.Close()

            '再表示
            displayDgvMaster(ind)
        Else
            '更新登録
            Dim result As DialogResult = MessageBox.Show("変更してよろしいですか？", "変更", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = Windows.Forms.DialogResult.Yes Then
                rs.Fields("Ind").Value = ind
                rs.Fields("Bango").Value = bango
                rs.Fields("Nam").Value = nam
                rs.Fields("Kana").Value = kana
                rs.Fields("Sex").Value = sex
                rs.Fields("Birth").Value = birth
                rs.Fields("Kubun").Value = kubun
                rs.Fields("Tel").Value = tel
                rs.Fields("Post").Value = post
                rs.Fields("Jyu").Value = jyu
                rs.Fields("Text").Value = comment
                rs.Update()
                rs.Close()
                cn.Close()

                '再表示
                displayDgvMaster(ind)
            Else
                rs.Close()
                cn.Close()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 削除ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        '事業所名
        Dim ind As String = indBox.Text
        If ind = "" Then
            MsgBox("事業所名を選択して下さい。", MsgBoxStyle.Exclamation)
            indBox.Focus()
            indBox.DroppedDown = True
            Return
        End If
        '健保番号
        Dim bango As String = bangoBox.Text
        If bango = "" Then
            MsgBox("健保番号を入力して下さい。", MsgBoxStyle.Exclamation)
            bangoBox.Focus()
            Return
        ElseIf Not System.Text.RegularExpressions.Regex.IsMatch(bango, "^\d+$") Then
            MsgBox("健保番号は数値を入力して下さい。", MsgBoxStyle.Exclamation)
            bangoBox.Focus()
            Return
        End If
        '氏名
        Dim nam As String = namBox.Text
        If nam = "" Then
            MsgBox("氏名を入力して下さい。", MsgBoxStyle.Exclamation)
            namBox.Focus()
            Return
        End If
        'カナ
        Dim kana As String = kanaBox.Text
        If kana = "" Then
            MsgBox("カナを入力して下さい。", MsgBoxStyle.Exclamation)
            kanaBox.Focus()
            Return
        End If
        '性別
        Dim sex As String = sexBox.Text
        If Not (sex = "1" OrElse sex = "2") Then
            MsgBox("性別を正しく入力して下さい。", MsgBoxStyle.Exclamation)
            sexBox.Focus()
            Return
        End If
        '生年月日
        Dim birth As String = birthBox.getWarekiStr()
        If birth = "" Then
            MsgBox("生年月日を入力して下さい。", MsgBoxStyle.Exclamation)
            birthBox.Focus()
            Return
        End If
        '本人・配偶者
        Dim kubun As String = kubunBox.Text
        If Not (kubun = "1" OrElse kubun = "2") Then
            MsgBox("本人・配偶者を正しく入力して下さい。", MsgBoxStyle.Exclamation)
            kubunBox.Focus()
            Return
        End If

        '削除
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Health3)
        Dim sql As String = "select * from UsrM where Ind = '" & ind & "' and Kana = '" & kana & "'"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If rs.RecordCount <= 0 Then
            MsgBox("登録されていません。", MsgBoxStyle.Exclamation)
            rs.Close()
            cn.Close()
            Return
        Else
            Dim result As DialogResult = MessageBox.Show("削除してよろしいですか？", "削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = Windows.Forms.DialogResult.Yes Then
                rs.Delete()
                rs.Update()
                rs.Close()
                cn.Close()

                '再表示
                displayDgvMaster(ind)
            Else
                rs.Close()
                cn.Close()
            End If
        End If
    End Sub
End Class