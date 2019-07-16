Imports System.Data.OleDb
Imports System.Windows.Forms.DataVisualization.Charting

Public Class 健診結果ＦＤ

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
    Private Sub 健診結果ＦＤ_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        '送付年月日を現在日付に設定
        sendDateBox.setADStr(Today.ToString("yyyy/MM/dd"))

        'データグリッドビュー初期設定
        initDgvResult()
        initDgvCount()

        '受診月を先月、先月受診データ表示、先月までの受診者数表示
        Dim prevYm As String = Today.AddMonths(-1).ToString("yyyy/MM") '先月(yyyy/MM)
        dateBox.setADStr(prevYm & "/01")
        displayDgvResult(prevYm)
        displayDgvCount(prevYm)

        '受診月の1桁目選択させる
        'とりあえずの処理↓
        SendKeys.Send("{RIGHT},{RIGHT},{RIGHT},{RIGHT},{RIGHT}")
    End Sub

    ''' <summary>
    ''' データグリッドビュー（上）初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initDgvResult()
        Util.EnableDoubleBuffering(dgvResult)

        With dgvResult
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
            .ColumnHeadersHeight = 18
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
    ''' データグリッドビュー（下）初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initDgvCount()
        Util.EnableDoubleBuffering(dgvCount)

        With dgvCount
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
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .RowTemplate.Height = 18
            .BackgroundColor = Color.FromKnownColor(KnownColor.Control)
            .ShowCellToolTips = False
            .EnableHeadersVisualStyles = False
            .Font = New Font("ＭＳ Ｐゴシック", 10)
            .ReadOnly = True
        End With

        '列追加
        Dim dt As New DataTable()
        For i As Integer = 1 To 12
            dt.Columns.Add("M" & i, GetType(String))
        Next
        dt.Columns.Add("Total", GetType(String))

        '行追加
        For i As Integer = 0 To 3
            Dim row As DataRow = dt.NewRow()
            For j As Integer = 1 To 12
                row("M" & j) = ""
            Next
            row("Total") = If(i Mod 2 = 0, "計", "")
            dt.Rows.Add(row)
        Next

        '表示
        dgvCount.DataSource = dt

        '幅設定等
        With dgvCount
            For i As Integer = 1 To 12
                With .Columns("M" & i)
                    .SortMode = DataGridViewColumnSortMode.NotSortable
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .Width = 70
                End With
            Next
            With .Columns("Total")
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 70
            End With
        End With
    End Sub

    ''' <summary>
    ''' 指定年月受診データ表示
    ''' </summary>
    ''' <param name="ym">年月(yyyy/MM)</param>
    ''' <remarks></remarks>
    Private Sub displayDgvResult(ym As String)
        '内容クリア
        dgvResult.Columns.Clear()

        'データ取得
        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Health3)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select U.Ind, U.Nam, K.D6, K.Ymd, K.D2, K.D279, K.D242, K.D265, K.D249, K.D161 from KenD as K inner join UsrM as U on K.Kana = U.Kana and K.Ind = U.Ind where Ymd Like '" & ym & "%' order by Ymd, U.Ind, U.Kana"
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
        Dim da As OleDbDataAdapter = New OleDbDataAdapter()
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, rs, "UsrM")
        Dim dt As DataTable = ds.Tables("UsrM")

        '列追加
        dt.Columns.Add("Check", GetType(Boolean)) '名簿
        For Each row As DataRow In dt.Rows
            '名簿列デフォルトでチェック有にする
            row("Check") = True
        Next

        '表示
        dgvResult.DataSource = dt
        cnn.Close()

        '幅設定等
        With dgvResult
            With .Columns("Check")
                .DisplayIndex = 0
                .HeaderText = ""
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 35
                .HeaderCell.Style.Font = New Font("ＭＳ Ｐゴシック", 9)
            End With
            With .Columns("Ind")
                .HeaderText = "事業所名"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 270
                .ReadOnly = True
            End With
            With .Columns("Nam")
                .HeaderText = "氏名"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 120
                .ReadOnly = True
            End With
            With .Columns("D6")
                .HeaderText = "生年月日"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 85
                .ReadOnly = True
            End With
            With .Columns("Ymd")
                .HeaderText = "健診日"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 85
                .ReadOnly = True
            End With
            With .Columns("D2")
                .HeaderText = "健診区分"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 70
                .ReadOnly = True
            End With
            With .Columns("D279")
                .HeaderText = "指導区分"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 70
                .ReadOnly = True
            End With
            With .Columns("D242")
                .HeaderText = "胃部X線"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 70
                .ReadOnly = True
            End With
            With .Columns("D265")
                .HeaderText = "肝炎検査"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 70
                .ReadOnly = True
            End With
            With .Columns("D249")
                .HeaderText = "便検査"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 70
                .ReadOnly = True
            End With
            With .Columns("D161")
                .HeaderText = "尿検査"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 70
                .ReadOnly = True
            End With
            If .Rows.Count >= 15 Then
                .Size = New Size(1069, 272)
            Else
                .Size = New Size(1052, 272)
            End If

        End With

        'フォーカス
        dateBox.Focus()

    End Sub

    ''' <summary>
    ''' 指定年月より過去２年間の受診者数表示
    ''' </summary>
    ''' <param name="ym">年月(yyyy/MM)</param>
    ''' <remarks></remarks>
    Private Sub displayDgvCount(ym As String)
        '内容クリア
        For i As Integer = 0 To 3
            For j As Integer = 1 To 12
                dgvCount("M" & j, i).Value = ""
            Next
            If i Mod 2 = 1 Then
                dgvCount("Total", i).Value = ""
            End If
        Next

        '年月文字設定
        Dim currentYm As DateTime = New DateTime(CInt(ym.Split("/")(0)), CInt(ym.Split("/")(1)), 1) '現在年月
        For i As Integer = 0 To 1
            For j As Integer = 0 To 11
                dgvCount("M" & (12 - j), 0).Value = currentYm.AddMonths(-j).ToString("yyyy/MM")
                dgvCount("M" & (12 - j), 2).Value = currentYm.AddMonths(-(12 + j)).ToString("yyyy/MM")
            Next
        Next

        '受診者数取得、表示
        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Health3)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select Ymd from KenD where '" & currentYm.AddMonths(-23).ToString("yyyy/MM") & "/01" & "' <= Ymd and Ymd <= '" & currentYm.ToString("yyyy/MM") & "/31" & "'"
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        For i As Integer = 0 To 23
            rs.Filter = "Ymd Like '" & currentYm.AddMonths(-i).ToString("yyyy/MM") & "%'"
            If i < 12 Then
                dgvCount("M" & (12 - i), 1).Value = rs.RecordCount
            Else
                dgvCount("M" & (24 - i), 3).Value = rs.RecordCount
            End If
        Next
        rs.Close()
        cnn.Close()

        '合計表示
        Dim total1, total2 As Integer
        For i As Integer = 1 To 12
            total1 += dgvCount("M" & i, 1).Value
            total2 += dgvCount("M" & i, 3).Value
        Next
        dgvCount("Total", 1).Value = total1
        dgvCount("Total", 3).Value = total2

        'グラフ表示
        '初期化
        With countChart
            .Titles.Clear()
            .Series.Clear()
            .ChartAreas.Clear()
            .BackColor = Color.FromKnownColor(KnownColor.Control)
        End With

        '元データ作成
        Dim ymArray(11) As String '年月(yyyy/MM)データ
        For i As Integer = 0 To 11
            ymArray(i) = currentYm.AddMonths(-(11 - i)).ToString("yyyy/MM")
        Next
        Dim prev1CountArray(11) As String '1年前～現在年月まで受診者数データ
        For i As Integer = 1 To 12
            prev1CountArray(i - 1) = dgvCount("M" & i, 1).Value
        Next
        Dim prev2CountArray(11) As String '2年前～1年前まで受診者数データ
        For i As Integer = 1 To 12
            prev2CountArray(i - 1) = dgvCount("M" & i, 3).Value
        Next

        'データをセット
        '1年前～現在年月までデータ
        Dim prev1series As Series = New Series()
        prev1series.ChartType = SeriesChartType.Column '棒グラフ
        For i As Integer = 0 To 11
            prev1series.Points.Add(New DataPoint(i, prev1CountArray(i)))
            prev1series.Points(i).AxisLabel = ymArray(i)
            prev1series.Points(i).Color = Color.FromArgb(0, 255, 0)
        Next
        '2年前～1年前までデータ
        Dim prev2series As Series = New Series()
        prev2series.ChartType = SeriesChartType.Column '棒グラフ
        For i As Integer = 0 To 11
            prev2series.Points.Add(New DataPoint(i, prev2CountArray(i)))
            prev2series.Points(i).AxisLabel = ymArray(i)
            prev2series.Points(i).Color = Color.FromArgb(255, 0, 0)
        Next

        'エリア設定
        Dim area As New ChartArea()
        area.BackColor = Color.FromKnownColor(KnownColor.Control)
        With area.AxisY 'Y軸設定
            '目盛り
            .Maximum = 30 '最大値
            .Minimum = 0 '最小値
            .Interval = 5 '間隔
        End With
        With area.AxisX
            .IsLabelAutoFit = True
            .LabelAutoFitStyle = LabelAutoFitStyles.DecreaseFont Or LabelAutoFitStyles.IncreaseFont
            .LabelAutoFitMaxFontSize = 8
            .LabelAutoFitMinFontSize = 8
        End With

        countChart.ChartAreas.Add(area)
        countChart.Series.Add(prev2series)
        countChart.Series.Add(prev1series)

    End Sub

    ''' <summary>
    ''' 受診年月ボックスエンターキー押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dateBox_keyDownEnter(sender As Object, e As System.EventArgs) Handles dateBox.keyDownEnter
        '受診年月データ表示
        displayDgvResult(dateBox.getADymStr())
        displayDgvCount(dateBox.getADymStr())
    End Sub

    ''' <summary>
    ''' CellFormattingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvResult_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvResult.CellFormatting
        If e.RowIndex >= 0 Then
            Dim columnName As String = dgvResult.Columns(e.ColumnIndex).Name '列名
            If columnName = "Ymd" Then '健診日
                If Util.checkDBNullValue(e.Value) <> "" Then
                    e.Value = Util.convADStrToWarekiStr(e.Value)
                    e.FormattingApplied = True
                End If
            End If
            If columnName = "D242" OrElse columnName = "D161" Then '胃部X線、尿検査
                If Util.checkDBNullValue(e.Value) <> "" Then
                    e.Value = "○"
                    e.FormattingApplied = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' CellPaintingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvResult_CellPainting(sender As Object, e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvResult.CellPainting
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
    ''' 実行ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExecute_Click(sender As System.Object, e As System.EventArgs) Handles btnExecute.Click

    End Sub
End Class