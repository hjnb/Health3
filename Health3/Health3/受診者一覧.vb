Public Class 受診者一覧

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
    Private Sub 受診者一覧_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        'データグリッドビュー初期設定
        initDgvList()

        'データ表示
        displayDgvList()
    End Sub

    ''' <summary>
    ''' データグリッドビュー初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initDgvList()
        Util.EnableDoubleBuffering(dgvList)

        With dgvList
            .AllowUserToAddRows = False '行追加禁止
            .AllowUserToResizeColumns = False '列の幅をユーザーが変更できないようにする
            .AllowUserToResizeRows = False '行の高さをユーザーが変更できないようにする
            .AllowUserToDeleteRows = False '行削除禁止
            .BorderStyle = BorderStyle.FixedSingle
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .RowHeadersWidth = 50
            .RowTemplate.Height = 18
            .RowTemplate.HeaderCell = New dgvRowHeaderCell() '行ヘッダの三角マークを非表示に
            .BackgroundColor = Color.FromKnownColor(KnownColor.Control)
            .ShowCellToolTips = False
            .EnableHeadersVisualStyles = False
            .Font = New Font("ＭＳ Ｐゴシック", 10)
            .ReadOnly = True
        End With
    End Sub

    ''' <summary>
    ''' データ表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub displayDgvList()
        '内容クリア
        dgvList.Columns.Clear()

        'データ取得
        Dim dt As New DataTable()
        dt.Columns.Add("Kana", GetType(String))
        dt.Columns.Add("Nam", GetType(String))
        dt.Columns.Add("Birth", GetType(String))
        dt.Columns.Add("Age", GetType(String))
        dt.Columns.Add("Ind", GetType(String))
        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Health3)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select Kana, Nam, Birth, Int((Format(NOW(),'YYYYMMDD')-Format(Birth, 'YYYYMMDD'))/10000) as Age, Ind from UsrM order by Kana"
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
        While Not rs.EOF
            Dim row As DataRow = dt.NewRow()
            row("Kana") = Util.checkDBNullValue(rs.Fields("Kana").Value)
            row("Nam") = Util.checkDBNullValue(rs.Fields("Nam").Value)
            row("Birth") = Util.checkDBNullValue(rs.Fields("Birth").Value)
            row("Age") = Util.checkDBNullValue(rs.Fields("Age").Value)
            row("Ind") = Util.checkDBNullValue(rs.Fields("Ind").Value)
            dt.Rows.Add(row)
            rs.MoveNext()
        End While
        rs.Close()
        cnn.Close()

        '表示
        dgvList.DataSource = dt
        If Not IsNothing(dgvList.CurrentRow) Then
            dgvList.CurrentRow.Selected = False
        End If

        
        '幅設定
        With dgvList
            With .Columns("Kana")
                .HeaderText = "ｶﾅ"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 120
            End With
            With .Columns("Nam")
                .HeaderText = "氏名"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 120
            End With
            With .Columns("Birth")
                .HeaderText = "生年月日"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 90
            End With
            With .Columns("Age")
                .HeaderText = "年齢"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 70
            End With
            With .Columns("Ind")
                .HeaderText = "事業所"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 270
            End With
        End With
    End Sub

    ''' <summary>
    ''' CellPaintingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvList_CellPainting(sender As Object, e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvList.CellPainting
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
    End Sub

    ''' <summary>
    ''' 列ヘッダーダブルクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvList_ColumnHeaderMouseDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvList.ColumnHeaderMouseDoubleClick
        Dim targetColumn As DataGridViewColumn = dgvList.Columns(e.ColumnIndex) '選択列
        dgvList.Sort(targetColumn, System.ComponentModel.ListSortDirection.Ascending) '昇順でソート
    End Sub

    ''' <summary>
    ''' 対象の頭文字までスクロール
    ''' </summary>
    ''' <param name="initialChar">頭文字</param>
    ''' <remarks></remarks>
    Private Sub initialSearch(initialChar As String)
        Dim rowsCount As Integer = dgvList.Rows.Count
        For i As Integer = 0 To rowsCount - 1
            Dim kana As String = Util.checkDBNullValue(dgvList("Kana", i).Value)
            If System.Text.RegularExpressions.Regex.IsMatch(kana, "^" & initialChar) Then
                dgvList.Rows(i).Selected = True
                dgvList.FirstDisplayedScrollingRowIndex = i
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' ｱ～ﾜボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchKana_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchA.Click, btnSearchKA.Click, btnSearchSA.Click, btnSearchTA.Click, btnSearchNA.Click, btnSearchHA.Click, btnSearchMA.Click, btnSearchYA.Click, btnSearchRA.Click, btnSearchWA.Click
        Dim searchText As String = StrConv(sender.text, VbStrConv.Narrow)
        initialSearch(searchText)
    End Sub

End Class