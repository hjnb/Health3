Imports System.Data.OleDb
Imports System.Windows.Forms.DataVisualization.Charting

Public Class 健診結果ＦＤ

    'KPNC.txtの保存先パス
    Private Const savePath As String = "A:KPNC.txt"

    '健診機関ｺｰﾄﾞ
    Private Const CODE As String = "0111415816"

    '院長氏名
    Private Const CEO_NAME As String = "竹内　實"

    '全角スペース
    Private Const SPACE_ZENKAKU As String = "　"

    '半角スペース
    Private Const SPACE_HANKAKU As String = " "

    '異常所見なしコメント
    Private Const NP_WORD As String = "異常なし"

    '元号対応値
    Private eraDic As New Dictionary(Of String, Integer) From {{"M", "1"}, {"T", "2"}, {"S", "3"}, {"H", "4"}, {"R", "5"}}

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
        Dim sql As String = "select U.Ind, U.Nam, U.Kana, K.* from KenD as K inner join UsrM as U on K.Kana = U.Kana and K.Ind = U.Ind where Ymd Like '" & ym & "%' order by Ymd, U.Ind, U.Kana"
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

            '非表示設定
            Dim showColumnName() As String = {"Check", "U.Ind", "Nam", "D6", "Ymd", "D2", "D279", "D242", "D265", "D249", "D161"}
            For Each col As DataGridViewColumn In .Columns
                If Array.IndexOf(showColumnName, col.Name) < 0 Then
                    col.Visible = False
                End If
            Next

            With .Columns("Check")
                .DisplayIndex = 0
                .HeaderText = ""
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 35
                .HeaderCell.Style.Font = New Font("ＭＳ Ｐゴシック", 9)
            End With
            With .Columns("U.Ind")
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
        If dgvResult.Rows.Count <= 0 Then
            MsgBox("データが存在しません。", MsgBoxStyle.Exclamation)
            Return
        End If

        '書き込みデータ作成
        Dim dataList As New List(Of String)
        For Each row As DataGridViewRow In dgvResult.Rows
            dataList.Add(createWriteDataText(row))
        Next

        'ファイル書き込み用
        Dim filePath As String = savePath '保存先パス
        Dim sw As System.IO.StreamWriter
        Try
            sw = New System.IO.StreamWriter(filePath, False, System.Text.Encoding.GetEncoding("shift_jis"))
        Catch ex As System.IO.DirectoryNotFoundException
            MsgBox("保存先を用意して下さい。(A:KPNC.txt)", MsgBoxStyle.Exclamation)
            Return
        End Try

        For Each txt As String In dataList
            sw.WriteLine(txt)
        Next

        '書き込み終了
        sw.Close()

        '終了メッセージ
        MsgBox("終了しました。（" & dataList.Count & "件）", MsgBoxStyle.Information)
    End Sub

    ''' <summary>
    ''' 健診データを出力テキスト用に変換
    ''' </summary>
    ''' <param name="row">健診データ行</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function createWriteDataText(row As DataGridViewRow) As String
        Dim sb As New System.Text.StringBuilder()

        '項目名称(桁数(Byte))

        '健診機関ｺｰﾄﾞ(10)
        sb.Append(CODE)
        '健診区分(1)
        sb.Append(Util.checkDBNullValue(row.Cells("D2").Value))
        '検査区分(1)
        sb.Append("1")
        '氏名（フリガナ）(20)
        sb.Append(String.Format("{0,-20}", Util.checkDBNullValue(row.Cells("K.Kana").Value)))
        '被保険者・被扶養者番号(2)
        sb.Append("00")
        '生年月日(和暦)
        Dim birth As String = Util.checkDBNullValue(row.Cells("D6").Value) 'そもそも和暦
        '元号(1)
        sb.Append(eraDic(birth.Substring(0, 1)))
        '年(2)
        sb.Append(birth.Substring(1, 2))
        '月(2)
        sb.Append(birth.Split("/")(1))
        '日(2)
        sb.Append(birth.Split("/")(2))
        '性別(1)
        sb.Append(Util.checkDBNullValue(row.Cells("D7").Value))
        '空白(8)
        sb.Append(Space(8))
        '健保記号(4)
        sb.Append(Util.checkDBNullValue(row.Cells("D9").Value))
        '健保符号(6)
        sb.Append(Util.checkDBNullValue(row.Cells("D10").Value))
        '健保番号(6)
        sb.Append(String.Format("{0:D6}", CInt(Util.checkDBNullValue(row.Cells("D11").Value))))
        '予備1(5)
        sb.Append(Space(5))
        '受診年月日(和暦)
        Dim ymd As String = Util.convADStrToWarekiStr(Util.checkDBNullValue(row.Cells("Ymd").Value)) '西暦なので和暦に変換
        '元号(1)
        sb.Append(eraDic(ymd.Substring(0, 1)))
        '年(2)
        sb.Append(ymd.Substring(1, 2))
        '月(2)
        sb.Append(ymd.Split("/")(1))
        '日(2)
        sb.Append(ymd.Split("/")(2))
        '診察等指導区分1(1)
        Dim d14 As String = Util.checkDBNullValue(row.Cells("D14").Value)
        sb.Append(If(d14 = "", SPACE_HANKAKU, d14))
        '診察等指導区分2(1)
        sb.Append(SPACE_HANKAKU)
        '身長(1)(4)
        Dim d17 As String = Util.checkDBNullValue(row.Cells("D17").Value)
        sb.Append(If(d17 = "", "0", "1"))
        sb.Append(convValue(d17, 1))
        '体重(1)(4)
        Dim d19 As String = Util.checkDBNullValue(row.Cells("D19").Value)
        sb.Append(If(d19 = "", "0", "1"))
        sb.Append(convValue(d19, 1))
        '標準体重(1)(4)
        Dim d21 As String = Util.checkDBNullValue(row.Cells("D21").Value)
        sb.Append(If(d21 = "", "0", "1"))
        sb.Append(convValue(d21, 1))
        'BMI(1)(4)
        Dim d23 As String = Util.checkDBNullValue(row.Cells("D23").Value)
        sb.Append(If(d23 = "", "0", "1"))
        sb.Append(convValue(d23, 1))
        '腹囲(実測)(1)(4)
        Dim d25 As String = Util.checkDBNullValue(row.Cells("D25").Value)
        sb.Append(If(d25 = "", "0", "1"))
        sb.Append(convValue(d25, 1))
        '腹囲(自己測定)(1)(4)
        Dim d27 As String = Util.checkDBNullValue(row.Cells("D27").Value)
        sb.Append(If(d27 = "", "0", "1"))
        sb.Append(convValue(d27, 1))
        '腹囲(自己申告)(1)(4)
        Dim d29 As String = Util.checkDBNullValue(row.Cells("D29").Value)
        sb.Append(If(d29 = "", "0", "1"))
        sb.Append(convValue(d29, 1))
        '内臓脂肪面積(1)(4)
        Dim d31 As String = Util.checkDBNullValue(row.Cells("D31").Value)
        sb.Append(If(d31 = "", "0", "1"))
        sb.Append(convValue(d31, 1))
        '既往歴(1)(40)
        Dim d33 As String = Util.checkDBNullValue(row.Cells("D33").Value)
        d33 = StrConv(d33, VbStrConv.Wide)
        sb.Append(If(d33 = "", "1", "2"))
        sb.Append(paddingZenkakuText(d33, 40))
        '自覚症状(1)(40)
        Dim d35 As String = Util.checkDBNullValue(row.Cells("D35").Value)
        d35 = StrConv(d35, VbStrConv.Wide)
        sb.Append(If(d35 = "", "1", "2"))
        sb.Append(paddingZenkakuText(d35, 40))
        '他覚症状(1)(40)
        sb.Append("1")
        sb.Append(paddingZenkakuText("", 40))
        '胸部・腹部所見(16)
        Dim d38 As String = Util.checkDBNullValue(row.Cells("D38").Value)
        d38 = StrConv(d38, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d38, 16))
        '視力
        '裸眼右(1)(4)
        Dim d40 As String = Util.checkDBNullValue(row.Cells("D40").Value)
        sb.Append(If(d40 = "", "0", "1"))
        sb.Append(convValue(d40, 2))
        '矯正右(1)(4)
        Dim d42 As String = Util.checkDBNullValue(row.Cells("D42").Value)
        sb.Append(If(d42 = "", "0", "1"))
        sb.Append(convValue(d42, 2))
        '裸眼左(1)(4)
        Dim d44 As String = Util.checkDBNullValue(row.Cells("D44").Value)
        sb.Append(If(d44 = "", "0", "1"))
        sb.Append(convValue(d44, 2))
        '矯正左(1)(4)
        Dim d46 As String = Util.checkDBNullValue(row.Cells("D46").Value)
        sb.Append(If(d46 = "", "0", "1"))
        sb.Append(convValue(d46, 2))
        '聴力
        '右1000Hz(1)
        Dim d47 As String = Util.checkDBNullValue(row.Cells("D47").Value)
        sb.Append(If(d47 = "", "", "1"))
        '右4000Hz(1)
        Dim d48 As String = Util.checkDBNullValue(row.Cells("D48").Value)
        sb.Append(If(d48 = "", "", "1"))
        '左1000Hz(1)
        Dim d49 As String = Util.checkDBNullValue(row.Cells("D49").Value)
        sb.Append(If(d49 = "", "", "1"))
        '左4000Hz(1)
        Dim d50 As String = Util.checkDBNullValue(row.Cells("D50").Value)
        sb.Append(If(d50 = "", "", "1"))
        '予備2(5)
        sb.Append(Space(5))
        '血圧
        '血圧指導区分(1)
        Dim d52 As String = Util.checkDBNullValue(row.Cells("D52").Value)
        sb.Append(If(d52 = "", SPACE_HANKAKU, d52))
        '収縮期血圧(1回目)(1)(4)
        Dim d54 As String = Util.checkDBNullValue(row.Cells("D54").Value)
        sb.Append(If(d54 = "", "0", "1"))
        sb.Append(convValue(d54, 1))
        '収縮期血圧(2回目)(1)(4)
        Dim d56 As String = Util.checkDBNullValue(row.Cells("D56").Value)
        sb.Append(If(d56 = "", "0", "1"))
        sb.Append(convValue(d56, 1))
        '収縮期血圧(その他)(1)(4)
        Dim d58 As String = Util.checkDBNullValue(row.Cells("D58").Value)
        sb.Append(If(d58 = "", "0", "1"))
        sb.Append(convValue(d58, 1))
        '拡張期血圧(1回目)(1)(4)
        Dim d60 As String = Util.checkDBNullValue(row.Cells("D60").Value)
        sb.Append(If(d60 = "", "0", "1"))
        sb.Append(convValue(d60, 1))
        '拡張期血圧(2回目)(1)(4)
        Dim d62 As String = Util.checkDBNullValue(row.Cells("D62").Value)
        sb.Append(If(d62 = "", "0", "1"))
        sb.Append(convValue(d62, 1))
        '拡張期血圧(その他)(1)(4)
        Dim d64 As String = Util.checkDBNullValue(row.Cells("D64").Value)
        sb.Append(If(d64 = "", "0", "1"))
        sb.Append(convValue(d64, 1))
        '採血時間(食後)(1)
        Dim d65 As String = Util.checkDBNullValue(row.Cells("D65").Value)
        sb.Append(If(d65 = "", SPACE_HANKAKU, "2")) '空腹時なので空でない場合は2
        '予備3(5)
        sb.Append(Space(5))
        '脂質
        '脂質指導区分(1)
        Dim d67 As String = Util.checkDBNullValue(row.Cells("D67").Value)
        sb.Append(If(d67 = "", SPACE_HANKAKU, d67))
        '総コレステロール(1)(8)
        Dim d69 As String = Util.checkDBNullValue(row.Cells("D69").Value)
        sb.Append(If(d69 = "", "0", "1"))
        sb.Append(convValue(d69, 3))
        '中性脂肪（可視吸光光度法）(1)(8)
        Dim d71 As String = Util.checkDBNullValue(row.Cells("D71").Value)
        sb.Append(If(d71 = "", "0", "1"))
        sb.Append(convValue(d71, 3))
        '中性脂肪（紫外吸光光度法）(1)(8)
        Dim d73 As String = Util.checkDBNullValue(row.Cells("D73").Value)
        sb.Append(If(d73 = "", "0", "1"))
        sb.Append(convValue(d73, 3))
        '中性脂肪（その他）(1)(8)
        Dim d75 As String = Util.checkDBNullValue(row.Cells("D75").Value)
        sb.Append(If(d75 = "", "0", "1"))
        sb.Append(convValue(d75, 3))
        'HDLコレステロール（可視吸光光度法）(1)(8)
        Dim d77 As String = Util.checkDBNullValue(row.Cells("D77").Value)
        sb.Append(If(d77 = "", "0", "1"))
        sb.Append(convValue(d77, 3))
        'HDLコレステロール（紫外吸光光度法）(1)(8)
        Dim d79 As String = Util.checkDBNullValue(row.Cells("D79").Value)
        sb.Append(If(d79 = "", "0", "1"))
        sb.Append(convValue(d79, 3))
        'HDLコレステロール（その他）(1)(8)
        Dim d81 As String = Util.checkDBNullValue(row.Cells("D81").Value)
        sb.Append(If(d81 = "", "0", "1"))
        sb.Append(convValue(d81, 3))
        'LDLコレステロール（可視吸光光度法）(1)(8)
        Dim d83 As String = Util.checkDBNullValue(row.Cells("D83").Value)
        sb.Append(If(d83 = "", "0", "1"))
        sb.Append(convValue(d83, 3))
        'LDLコレステロール（紫外吸光光度法）(1)(8)
        Dim d85 As String = Util.checkDBNullValue(row.Cells("D85").Value)
        sb.Append(If(d85 = "", "0", "1"))
        sb.Append(convValue(d85, 3))
        'LDLコレステロール（その他）(1)(8)
        Dim d87 As String = Util.checkDBNullValue(row.Cells("D87").Value)
        sb.Append(If(d87 = "", "0", "1"))
        sb.Append(convValue(d87, 3))
        '予備4(9)
        sb.Append(Space(9))
        '肝機能等
        '肝機能等指導区分(1)
        Dim d89 As String = Util.checkDBNullValue(row.Cells("D89").Value)
        sb.Append(If(d89 = "", SPACE_HANKAKU, d89))
        'GOT
        'GOT（紫外吸光光度表）(1)(8)
        Dim d91 As String = Util.checkDBNullValue(row.Cells("D91").Value)
        sb.Append(If(d91 = "", "0", "1"))
        sb.Append(convValue(d91, 3))
        'GOT（その他）(1)(8)
        Dim d93 As String = Util.checkDBNullValue(row.Cells("D93").Value)
        sb.Append(If(d93 = "", "0", "1"))
        sb.Append(convValue(d93, 3))
        'GPT
        'GPT（紫外吸光光度表）(1)(8)
        Dim d95 As String = Util.checkDBNullValue(row.Cells("D95").Value)
        sb.Append(If(d95 = "", "0", "1"))
        sb.Append(convValue(d95, 3))
        'GPT（その他）(1)(8)
        Dim d97 As String = Util.checkDBNullValue(row.Cells("D97").Value)
        sb.Append(If(d97 = "", "0", "1"))
        sb.Append(convValue(d97, 3))
        'γーＧＴＰ
        'γーＧＴＰ（可視吸光光度法）(1)(8)
        Dim d99 As String = Util.checkDBNullValue(row.Cells("D99").Value)
        sb.Append(If(d99 = "", "0", "1"))
        sb.Append(convValue(d99, 3))
        'γーＧＴＰ（その他）(1)(8)
        Dim d101 As String = Util.checkDBNullValue(row.Cells("D101").Value)
        sb.Append(If(d101 = "", "0", "1"))
        sb.Append(convValue(d101, 3))
        'ALP
        'ALP-IU(1)(8)
        Dim d103 As String = Util.checkDBNullValue(row.Cells("D103").Value)
        sb.Append(If(d103 = "", "0", "1"))
        sb.Append(convValue(d103, 3))
        'ALP-KAU(1)(8)
        Dim d105 As String = Util.checkDBNullValue(row.Cells("D105").Value)
        sb.Append(If(d105 = "", "0", "1"))
        sb.Append(convValue(d105, 3))

        '↓総蛋白～ｱﾐﾗｰｾﾞは空白で埋める((1+8)*9で63)
        sb.Append(Space(63))
        ''総蛋白(1)(8)
        'Dim d107 As String = Util.checkDBNullValue(row.Cells("D107").Value)
        'sb.Append(If(d107 = "", "0", "1"))
        'sb.Append(convValue(d107, 3))
        ''ｱﾙﾌﾞﾐﾝ(1)(8)
        'Dim d109 As String = Util.checkDBNullValue(row.Cells("D109").Value)
        'sb.Append(If(d109 = "", "0", "1"))
        'sb.Append(convValue(d109, 3))
        ''総ﾋﾞﾘﾙﾋﾞﾝ(1)(8)
        'Dim d111 As String = Util.checkDBNullValue(row.Cells("D111").Value)
        'sb.Append(If(d111 = "", "0", "1"))
        'sb.Append(convValue(d111, 3))
        ''LDH
        ''LDH-IU(1)(8)
        'Dim d113 As String = Util.checkDBNullValue(row.Cells("D113").Value)
        'sb.Append(If(d113 = "", "0", "1"))
        'sb.Append(convValue(d113, 3))
        ''LDH-WRU(1)(8)
        'Dim d115 As String = Util.checkDBNullValue(row.Cells("D115").Value)
        'sb.Append(If(d115 = "", "0", "1"))
        'sb.Append(convValue(d115, 3))
        ''ｱﾐﾗｰｾﾞ
        ''ｱﾐﾗｰｾﾞIU(1)(8)
        'Dim d117 As String = Util.checkDBNullValue(row.Cells("D117").Value)
        'sb.Append(If(d117 = "", "0", "1"))
        'sb.Append(convValue(d117, 3))
        ''ｱﾐﾗｰｾﾞSOU(1)(8)
        'Dim d119 As String = Util.checkDBNullValue(row.Cells("D119").Value)
        'sb.Append(If(d119 = "", "0", "1"))
        'sb.Append(convValue(d119, 3))

        '予備5(9)
        sb.Append(Space(9))
        '血糖
        '血糖指導区分(1)
        Dim d121 As String = Util.checkDBNullValue(row.Cells("D121").Value)
        sb.Append(If(d121 = "", SPACE_HANKAKU, d121))
        '空腹時血糖（電位差法）(1)(8)
        Dim d123 As String = Util.checkDBNullValue(row.Cells("D123").Value)
        sb.Append(If(d123 = "", "0", "1"))
        sb.Append(convValue(d123, 3))
        '空腹時血糖（可視吸光光度法）(1)(8)
        Dim d125 As String = Util.checkDBNullValue(row.Cells("D125").Value)
        sb.Append(If(d125 = "", "0", "1"))
        sb.Append(convValue(d125, 3))
        '空腹時血糖（紫外吸光光度法）(1)(8)
        Dim d127 As String = Util.checkDBNullValue(row.Cells("D127").Value)
        sb.Append(If(d127 = "", "0", "1"))
        sb.Append(convValue(d127, 3))
        '空腹時血糖（その他）(1)(8)
        Dim d129 As String = Util.checkDBNullValue(row.Cells("D129").Value)
        sb.Append(If(d129 = "", "0", "1"))
        sb.Append(convValue(d129, 3))

        '↓10項目(随時血糖（電位差法）～２時間後尿糖)はスペースで埋める((1+8)*10で90)
        sb.Append(Space(90))
        ''随時血糖（電位差法）(1)(8)
        'Dim d131 As String = Util.checkDBNullValue(row.Cells("D131").Value)
        'sb.Append(If(d131 = "", "0", "1"))
        'sb.Append(convValue(d131, 3))
        ''随時血糖（可視吸光光度法）(1)(8)
        'Dim d133 As String = Util.checkDBNullValue(row.Cells("D133").Value)
        'sb.Append(If(d133 = "", "0", "1"))
        'sb.Append(convValue(d133, 3))
        ''随時血糖（紫外吸光光度法）(1)(8)
        'Dim d135 As String = Util.checkDBNullValue(row.Cells("D135").Value)
        'sb.Append(If(d135 = "", "0", "1"))
        'sb.Append(convValue(d135, 3))
        ''随時血糖（その他）(1)(8)
        'Dim d137 As String = Util.checkDBNullValue(row.Cells("D137").Value)
        'sb.Append(If(d137 = "", "0", "1"))
        'sb.Append(convValue(d137, 3))
        ''糖負荷
        ''負荷前
        ''血糖(1)(8)
        'Dim d139 As String = Util.checkDBNullValue(row.Cells("D139").Value)
        'sb.Append(If(d139 = "", "0", "1"))
        'sb.Append(convValue(d139, 3))
        ''尿糖(1)(8)
        'Dim d141 As String = Util.checkDBNullValue(row.Cells("D141").Value)
        'sb.Append(If(d141 = "", "0", "1"))
        'sb.Append(convValue(d141, 3))
        ''1時間後
        ''血糖(1)(8)
        'Dim d143 As String = Util.checkDBNullValue(row.Cells("D143").Value)
        'sb.Append(If(d143 = "", "0", "1"))
        'sb.Append(convValue(d143, 3))
        ''尿糖(1)(8)
        'Dim d145 As String = Util.checkDBNullValue(row.Cells("D145").Value)
        'sb.Append(If(d145 = "", "0", "1"))
        'sb.Append(convValue(d145, 3))
        ''2時間後
        ''血糖(1)(8)
        'Dim d147 As String = Util.checkDBNullValue(row.Cells("D147").Value)
        'sb.Append(If(d147 = "", "0", "1"))
        'sb.Append(convValue(d147, 3))
        ''尿糖(1)(8)
        'Dim d149 As String = Util.checkDBNullValue(row.Cells("D149").Value)
        'sb.Append(If(d149 = "", "0", "1"))
        'sb.Append(convValue(d149, 3))


        'ﾍﾓｸﾞﾛﾋﾞﾝA1c(ラテックス凝集比濁法)(1)(8)
        Dim d151 As String = Util.checkDBNullValue(row.Cells("D151").Value)
        sb.Append(If(d151 = "", "0", "1"))
        sb.Append(convValue(d151, 3))
        'ﾍﾓｸﾞﾛﾋﾞﾝA1c(HPLC)(1)(8)
        Dim d153 As String = Util.checkDBNullValue(row.Cells("D153").Value)
        sb.Append(If(d153 = "", "0", "1"))
        sb.Append(convValue(d153, 3))
        'ﾍﾓｸﾞﾛﾋﾞﾝA1c(酵素法)(1)(8)
        Dim d155 As String = Util.checkDBNullValue(row.Cells("D155").Value)
        sb.Append(If(d155 = "", "0", "1"))
        sb.Append(convValue(d155, 3))
        'ﾍﾓｸﾞﾛﾋﾞﾝA1c(その他)(1)(8)
        Dim d157 As String = Util.checkDBNullValue(row.Cells("D157").Value)
        sb.Append(If(d157 = "", "0", "1"))
        sb.Append(convValue(d157, 3))
        '尿糖（機械読み取り）(1)(8)
        Dim d159 As String = Util.checkDBNullValue(row.Cells("D159").Value)
        sb.Append("0")
        sb.Append(Space(8))
        '尿糖（目視法）(1)(8)
        Dim d161 As String = Util.checkDBNullValue(row.Cells("D161").Value)
        sb.Append(If(d161 = "", "0", "1"))
        sb.Append(If(d161 = "", Space(8), "00000" & d161 & "00"))
        '予備6(9)
        sb.Append(Space(9))
        '尿酸
        '尿酸指導区分(1)
        Dim d163 As String = Util.checkDBNullValue(row.Cells("D163").Value)
        sb.Append(If(d163 = "", SPACE_HANKAKU, d163))
        '尿酸(1)(8)
        Dim d165 As String = Util.checkDBNullValue(row.Cells("D165").Value)
        sb.Append(If(d165 = "", "0", "1"))
        sb.Append(convValue(d165, 3))
        '予備7(9)
        sb.Append(Space(9))
        '尿一般・腎機能
        '尿一般・腎機能指導区分(1)
        Dim d167 As String = Util.checkDBNullValue(row.Cells("D167").Value)
        sb.Append(If(d167 = "", SPACE_HANKAKU, d167))
        '尿蛋白（機械読み取り）
        Dim d169 As String = Util.checkDBNullValue(row.Cells("D169").Value)
        sb.Append(If(d169 = "", "0", "1"))
        sb.Append(If(d169 = "", Space(8), "00000" & d169 & "00"))
        '尿蛋白（目視法）
        Dim d171 As String = Util.checkDBNullValue(row.Cells("D171").Value)
        sb.Append(If(d171 = "", "0", "1"))
        sb.Append(If(d171 = "", Space(8), "00000" & d171 & "00"))
        '尿潜血(1)(8)
        Dim d173 As String = Util.checkDBNullValue(row.Cells("D173").Value)
        sb.Append(If(d173 = "", "0", "1"))
        sb.Append(If(d173 = "", Space(8), "00000" & d173 & "00"))
        '尿沈渣　（やらないので空白で）
        '赤血球数(8)
        sb.Append(Space(8))
        '白血球数(8)
        sb.Append(Space(8))
        '上皮細胞(8)
        sb.Append(Space(8))
        '円柱(8)
        sb.Append(Space(8))
        'その他(8)
        sb.Append(Space(8))
        '血清ｸﾚｱﾁﾆﾝ(1)(8)
        Dim d180 As String = Util.checkDBNullValue(row.Cells("D180").Value)
        sb.Append(If(d180 = "", "0", "1"))
        sb.Append(convValue(d180, 3))
        '予備8(9)
        sb.Append(Space(9))
        '血液一般
        '血液一般指導区分(1)
        Dim d182 As String = Util.checkDBNullValue(row.Cells("D182").Value)
        sb.Append(If(d182 = "", SPACE_HANKAKU, d182))
        'ヘマトクリット値(1)(8)
        Dim d184 As String = Util.checkDBNullValue(row.Cells("D184").Value)
        sb.Append(If(d184 = "", "0", "1"))
        sb.Append(convValue(d184, 3))
        '血色素量(1)(8)
        Dim d186 As String = Util.checkDBNullValue(row.Cells("D186").Value)
        sb.Append(If(d186 = "", "0", "1"))
        sb.Append(convValue(d186, 3))
        '赤血球数(1)(8)
        Dim d188 As String = Util.checkDBNullValue(row.Cells("D188").Value)
        sb.Append(If(d188 = "", "0", "1"))
        sb.Append(convValue(d188, 3))
        '白血球数(1)(8)
        Dim d190 As String = Util.checkDBNullValue(row.Cells("D190").Value)
        sb.Append(If(d190 = "", "0", "1"))
        sb.Append(convValue(d190, 3))

        '↓9項目と実施理由はスペースで埋める((1+8)*9+40で121)
        sb.Append(Space(121))
        ''血小板数(1)(8)
        'Dim d192 As String = Util.checkDBNullValue(row.Cells("D192").Value)
        'sb.Append(If(d192 = "", "0", "1"))
        'sb.Append(convValue(d192, 3))
        ''末梢血液像
        ''Baso(1)(8)
        'Dim d194 As String = Util.checkDBNullValue(row.Cells("D194").Value)
        'sb.Append(If(d194 = "", "0", "1"))
        'sb.Append(convValue(d194, 3))
        ''Eosino(1)(8)
        'Dim d196 As String = Util.checkDBNullValue(row.Cells("D196").Value)
        'sb.Append(If(d196 = "", "0", "1"))
        'sb.Append(convValue(d196, 3))
        ''Stab(1)(8)
        'Dim d198 As String = Util.checkDBNullValue(row.Cells("D198").Value)
        'sb.Append(If(d198 = "", "0", "1"))
        'sb.Append(convValue(d198, 3))
        ''Seg(1)(8)
        'Dim d200 As String = Util.checkDBNullValue(row.Cells("D200").Value)
        'sb.Append(If(d200 = "", "0", "1"))
        'sb.Append(convValue(d200, 3))
        ''Neutro(1)(8)
        'Dim d202 As String = Util.checkDBNullValue(row.Cells("D202").Value)
        'sb.Append(If(d202 = "", "0", "1"))
        'sb.Append(convValue(d202, 3))
        ''Lympho(1)(8)
        'Dim d204 As String = Util.checkDBNullValue(row.Cells("D204").Value)
        'sb.Append(If(d204 = "", "0", "1"))
        'sb.Append(convValue(d204, 3))
        ''Mono(1)(8)
        'Dim d206 As String = Util.checkDBNullValue(row.Cells("D206").Value)
        'sb.Append(If(d206 = "", "0", "1"))
        'sb.Append(convValue(d206, 3))
        ''Other(1)(8)
        'Dim d208 As String = Util.checkDBNullValue(row.Cells("D208").Value)
        'sb.Append(If(d208 = "", "0", "1"))
        'sb.Append(convValue(d208, 3))
        ''実施理由(40)
        'Dim d209 As String = Util.checkDBNullValue(row.Cells("D209").Value)
        'sb.Append(paddingZenkakuText(d209, 40))


        '予備9(9)
        sb.Append(Space(9))
        '心電図
        '心電図指導区分(1)
        Dim d211 As String = Util.checkDBNullValue(row.Cells("D211").Value)
        sb.Append(If(d211 = "", SPACE_HANKAKU, d211))
        '心電図(1)(28)
        Dim d213 As String = Util.checkDBNullValue(row.Cells("D213").Value)
        d213 = StrConv(d213, VbStrConv.Wide)
        sb.Append(If(d213 = NP_WORD, "1", If(d213 = "", SPACE_HANKAKU, "2")))
        sb.Append(paddingZenkakuText(d213, 28))
        '実施理由(40)
        Dim d214 As String = Util.checkDBNullValue(row.Cells("D214").Value)
        d214 = StrConv(d214, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d214, 40))
        '予備10-1(1)
        sb.Append(Space(1))
        '予備10-2(28)
        sb.Append(Space(28))
        '眼底 （やってないのでやってないかんじで）
        '眼底指導区分(1)
        Dim d216 As String = Util.checkDBNullValue(row.Cells("D216").Value)
        sb.Append(If(d216 = "", SPACE_HANKAKU, d216))
        'K.W.(1)(8)
        sb.Append("0")
        sb.Append(Space(8))
        'ScheieH(1)(8)
        sb.Append("0")
        sb.Append(Space(8))
        'ScheieS(1)(8)
        sb.Append("0")
        sb.Append(Space(8))
        'SCOTT(1)(8)
        sb.Append("0")
        sb.Append(Space(8))
        '所見(40)
        Dim d225 As String = Util.checkDBNullValue(row.Cells("D225").Value)
        d225 = StrConv(d225, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d225, 40))
        '実施理由(40)
        Dim d226 As String = Util.checkDBNullValue(row.Cells("D226").Value)
        d226 = StrConv(d226, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d226, 40))
        '予備11(9)
        sb.Append(Space(9))

        '↓肺機能はスペースで埋める(1+(1+8)*3で28)
        sb.Append(Space(28))
        ''肺機能
        ''肺機能指導区分(1)
        'Dim d228 As String = Util.checkDBNullValue(row.Cells("D228").Value)
        'sb.Append(If(d228 = "", SPACE_HANKAKU, d228))
        ''肺活量(1)(8)
        'Dim d230 As String = Util.checkDBNullValue(row.Cells("D230").Value)
        'sb.Append(If(d230 = "", "0", "1"))
        'sb.Append(convValue(d230, 3))
        ''一秒量(1)(8)
        'Dim d232 As String = Util.checkDBNullValue(row.Cells("D232").Value)
        'sb.Append(If(d232 = "", "0", "1"))
        'sb.Append(convValue(d232, 3))
        ''一秒率(1)(8)
        'Dim d234 As String = Util.checkDBNullValue(row.Cells("D234").Value)
        'sb.Append(If(d234 = "", "0", "1"))
        'sb.Append(convValue(d234, 3))

        '予備12(9)
        sb.Append(Space(9))
        '胸部Ｘ線
        '胸部Ｘ線指導区分(1)
        Dim d236 As String = Util.checkDBNullValue(row.Cells("D236").Value)
        sb.Append(If(d236 = "", SPACE_HANKAKU, d236))
        '胸部Ｘ線撮影区分(1)
        Dim d237 As String = Util.checkDBNullValue(row.Cells("D237").Value)
        sb.Append(If(d237 = "", SPACE_HANKAKU, d237))
        '胸部Ｘ線所見(36)
        Dim d238 As String = Util.checkDBNullValue(row.Cells("D238").Value)
        d238 = StrConv(d238, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d238, 36))
        '予備13-1(1)
        sb.Append(Space(1))
        '予備13-2-1(1)
        sb.Append(Space(1))
        '予備13-2-2(8)
        sb.Append(Space(8))
        '胃部
        '胃部Ｘ線指導区分(1)
        Dim d240 As String = Util.checkDBNullValue(row.Cells("D240").Value)
        sb.Append(If(d240 = "", SPACE_HANKAKU, d240))
        '胃部Ｘ線撮影区分(1)
        Dim d241 As String = Util.checkDBNullValue(row.Cells("D241").Value)
        sb.Append(If(d241 = "", SPACE_HANKAKU, d241))
        '胃部Ｘ線所見(36)
        Dim d242 As String = Util.checkDBNullValue(row.Cells("D242").Value)
        d242 = StrConv(d242, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d242, 36))
        '胃内視鏡指導区分(1)
        Dim d243 As String = Util.checkDBNullValue(row.Cells("D243").Value)
        sb.Append(If(d243 = "", SPACE_HANKAKU, d243))
        '胃内視鏡所見(16)
        Dim d244 As String = Util.checkDBNullValue(row.Cells("D244").Value)
        d244 = StrConv(d244, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d244, 16))
        '予備14-1(1)
        sb.Append(Space(1))
        '予備14-2-1(1)
        sb.Append(Space(1))
        '予備14-2-2(8)
        sb.Append(Space(8))
        '腹部
        '腹部超音波指導区分(1)
        Dim d246 As String = Util.checkDBNullValue(row.Cells("D246").Value)
        sb.Append(If(d246 = "", SPACE_HANKAKU, d246))
        '腹部超音波所見(36)
        Dim d247 As String = Util.checkDBNullValue(row.Cells("D247").Value)
        d247 = StrConv(d247, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d247, 36))
        '予備15-1(9)
        sb.Append(Space(9))
        '大腸
        '免疫便潜血反応
        '免疫便潜血反応指導区分(1)
        Dim d249 As String = Util.checkDBNullValue(row.Cells("D249").Value)
        sb.Append(If(d249 = "", SPACE_HANKAKU, d249))
        '免疫便潜血反応(1日目)(1)(8)
        Dim d251 As String = Util.checkDBNullValue(row.Cells("D251").Value)
        sb.Append(If(d251 = "", "0", "1"))
        sb.Append(convValue(d251, 4))
        '免疫便潜血反応(2日目)(1)(8)
        Dim d253 As String = Util.checkDBNullValue(row.Cells("D253").Value)
        sb.Append(If(d253 = "", "0", "1"))
        sb.Append(convValue(d253, 4))
        '直腸診
        '直腸診指導区分(1)
        Dim d254 As String = Util.checkDBNullValue(row.Cells("D254").Value)
        sb.Append(If(d254 = "", SPACE_HANKAKU, d254))
        '直腸診所見(16)
        Dim d255 As String = Util.checkDBNullValue(row.Cells("D255").Value)
        d255 = StrConv(d255, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d255, 16))
        '予備16-1(1)
        sb.Append(Space(1))
        '予備16-2(36)
        sb.Append(Space(36))
        '乳房
        '乳房指導区分(1)
        Dim d257 As String = Util.checkDBNullValue(row.Cells("D257").Value)
        sb.Append(If(d257 = "", SPACE_HANKAKU, d257))
        '触診等所見(28)
        Dim d258 As String = Util.checkDBNullValue(row.Cells("D258").Value)
        sb.Append(paddingZenkakuText(d258, 28))
        '乳房Ｘ線実施有無(1)
        Dim d260 As String = Util.checkDBNullValue(row.Cells("D260").Value)
        sb.Append(If(d260 = "", "0", "1"))
        '乳房Ｘ線所見(28)
        d260 = StrConv(d260, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d260, 28))
        '予備17-1(1)
        sb.Append(Space(1))
        '予備17-2(28)
        sb.Append(Space(28))
        '子宮
        '子宮指導区分(1)
        Dim d262 As String = Util.checkDBNullValue(row.Cells("D262").Value)
        sb.Append(If(d262 = "", SPACE_HANKAKU, d262))
        '細胞診（スメア）(1)
        Dim d263 As String = Util.checkDBNullValue(row.Cells("D263").Value)
        sb.Append(If(d263 = "", SPACE_HANKAKU, d263))
        '予備18-1(1)
        sb.Append(Space(1))
        '予備18-2(8)
        sb.Append(Space(8))
        '肝炎
        'B型肝炎
        'HBs抗原指導区分(1)
        Dim d265 As String = Util.checkDBNullValue(row.Cells("D265").Value)
        sb.Append(If(d265 = "", SPACE_HANKAKU, d265))
        'HBs抗原(1)(8)
        Dim d267 As String = Util.checkDBNullValue(row.Cells("D267").Value)
        sb.Append(If(d267 = "", "0", "1"))
        sb.Append(convValue(d267, 5))
        'C型肝炎
        'HCV指導区分(1)
        Dim d268 As String = Util.checkDBNullValue(row.Cells("D268").Value)
        sb.Append(If(d268 = "", SPACE_HANKAKU, d268))
        'HCV抗体(1)
        Dim d269 As String = Util.checkDBNullValue(row.Cells("D269").Value)
        sb.Append(If(d269 = "", SPACE_HANKAKU, d269))
        'HCV核酸増幅検査(1)
        Dim d270 As String = Util.checkDBNullValue(row.Cells("D270").Value)
        sb.Append(If(d270 = "", SPACE_HANKAKU, d270))
        '予備19(10)
        sb.Append(Space(10))
        '総合所見指導区分
        Dim d279 As String = Util.checkDBNullValue(row.Cells("D279").Value)
        '総合所見指導区分1(1)
        sb.Append(If(d279 = "1", "1", SPACE_HANKAKU))
        '総合所見指導区分2(1)
        sb.Append(If(d279 = "2", "2", SPACE_HANKAKU))
        '総合所見指導区分3(1)
        sb.Append(If(d279 = "3", "3", SPACE_HANKAKU))
        '総合所見指導区分4(1)
        sb.Append(If(d279 = "4", "4", SPACE_HANKAKU))
        '総合所見指導区分5(1)
        sb.Append(If(d279 = "5", "5", SPACE_HANKAKU))
        '総合所見指導区分6(1)
        sb.Append(If(d279 = "6", "6", SPACE_HANKAKU))
        '予備20(5)
        sb.Append(Space(5))
        '注意事項(384)
        Dim d279a As String = Util.checkDBNullValue(row.Cells("D279a").Value)
        Dim d279b As String = Util.checkDBNullValue(row.Cells("D279b").Value)
        Dim d279c As String = Util.checkDBNullValue(row.Cells("D279c").Value)
        Dim d279d As String = Util.checkDBNullValue(row.Cells("D279d").Value)
        Dim d279e As String = Util.checkDBNullValue(row.Cells("D279e").Value)
        Dim d279f As String = Util.checkDBNullValue(row.Cells("D279f").Value)
        Dim d279Str As String = ""
        For Each s As String In {d279a, d279b, d279c, d279d, d279e, d279f}
            If s = "" Then
                Continue For
            End If
            If d279Str = "" Then
                d279Str = s
            Else
                d279Str &= "／" & s
            End If
        Next
        d279Str = StrConv(d279Str, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d279Str, 385))
        '医師の判断
        'ﾒﾀﾎﾞﾘｯｸｼﾝﾄﾞﾛｰﾑ判定(1)
        Dim d281 As String = Util.checkDBNullValue(row.Cells("D281").Value)
        sb.Append(If(d281 = "", SPACE_HANKAKU, d281))
        '保健指導レベル(1)　なしなので3
        sb.Append("3")
        '注意事項/医師の判断(40)
        Dim d283 As String = Util.checkDBNullValue(row.Cells("D283").Value)
        d283 = StrConv(d283, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d283, 40))
        '健康診断を実施した医師の氏名(40)
        sb.Append(paddingZenkakuText(CEO_NAME, 40))
        '質問票
        '服薬1(血圧)(1)
        Dim d285 As String = Util.checkDBNullValue(row.Cells("D285").Value)
        sb.Append(If(d285 = "", SPACE_HANKAKU, d285))
        '薬剤(血圧)(40)
        Dim d286 As String = Util.checkDBNullValue(row.Cells("D286").Value)
        d286 = StrConv(d286, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d286, 40))
        '服薬理由(血圧)(40)
        Dim d287 As String = Util.checkDBNullValue(row.Cells("D287").Value)
        d287 = StrConv(d287, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d287, 40))
        '服薬2(血糖)(1)
        Dim d288 As String = Util.checkDBNullValue(row.Cells("D288").Value)
        sb.Append(If(d288 = "", SPACE_HANKAKU, d288))
        '薬剤(血糖)(40)
        Dim d289 As String = Util.checkDBNullValue(row.Cells("D289").Value)
        d289 = StrConv(d289, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d289, 40))
        '服薬理由(血糖)(40)
        Dim d290 As String = Util.checkDBNullValue(row.Cells("D290").Value)
        d290 = StrConv(d290, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d290, 40))
        '服薬3(脂質)(1)
        Dim d291 As String = Util.checkDBNullValue(row.Cells("D291").Value)
        sb.Append(If(d291 = "", SPACE_HANKAKU, d291))
        '薬剤(脂質)(40)
        Dim d292 As String = Util.checkDBNullValue(row.Cells("D292").Value)
        d292 = StrConv(d292, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d292, 40))
        '服薬理由(脂質)(40)
        Dim d293 As String = Util.checkDBNullValue(row.Cells("D293").Value)
        d293 = StrConv(d293, VbStrConv.Wide)
        sb.Append(paddingZenkakuText(d293, 40))
        '既往歴1(脳血管)(1)
        Dim d294 As String = Util.checkDBNullValue(row.Cells("D294").Value)
        sb.Append(If(d294 = "", SPACE_HANKAKU, d294))
        '既往歴2(心血管)(1)
        Dim d295 As String = Util.checkDBNullValue(row.Cells("D295").Value)
        sb.Append(If(d295 = "", SPACE_HANKAKU, d295))
        '既往歴3(1)
        Dim d296 As String = Util.checkDBNullValue(row.Cells("D296").Value)
        sb.Append(If(d296 = "", SPACE_HANKAKU, d296))
        '貧血(1)
        Dim d297 As String = Util.checkDBNullValue(row.Cells("D297").Value)
        sb.Append(If(d297 = "", SPACE_HANKAKU, d297))
        '喫煙歴(1)
        Dim d298 As String = Util.checkDBNullValue(row.Cells("D298").Value)
        sb.Append(If(d298 = "", SPACE_HANKAKU, d298))
        '20歳からの体重変化(1)
        Dim d299 As String = Util.checkDBNullValue(row.Cells("D299").Value)
        sb.Append(If(d299 = "", SPACE_HANKAKU, d299))
        '30分以上の運動習慣(1)
        Dim d300 As String = Util.checkDBNullValue(row.Cells("D300").Value)
        sb.Append(If(d300 = "", SPACE_HANKAKU, d300))
        '歩行又は身体活動(1)
        Dim d301 As String = Util.checkDBNullValue(row.Cells("D301").Value)
        sb.Append(If(d301 = "", SPACE_HANKAKU, d301))
        '歩行速度(1)
        Dim d302 As String = Util.checkDBNullValue(row.Cells("D302").Value)
        sb.Append(If(d302 = "", SPACE_HANKAKU, d302))
        '1年間の体重変化(1)
        Dim d303 As String = Util.checkDBNullValue(row.Cells("D303").Value)
        sb.Append(If(d303 = "", SPACE_HANKAKU, d303))
        '食べ方1(早食い等)(1)
        Dim d304 As String = Util.checkDBNullValue(row.Cells("D304").Value)
        sb.Append(If(d304 = "", SPACE_HANKAKU, d304))
        '食べ方2(就寝前)(1)
        Dim d305 As String = Util.checkDBNullValue(row.Cells("D305").Value)
        sb.Append(If(d305 = "", SPACE_HANKAKU, d305))
        '食べ方3(夜食／間食)(1)
        Dim d306 As String = Util.checkDBNullValue(row.Cells("D306").Value)
        sb.Append(If(d306 = "", SPACE_HANKAKU, d306))
        '食習慣(1)
        Dim d307 As String = Util.checkDBNullValue(row.Cells("D307").Value)
        sb.Append(If(d307 = "", SPACE_HANKAKU, d307))
        '飲酒(1)
        Dim d308 As String = Util.checkDBNullValue(row.Cells("D308").Value)
        sb.Append(If(d308 = "", SPACE_HANKAKU, d308))
        '飲酒量(1)
        Dim d309 As String = Util.checkDBNullValue(row.Cells("D309").Value)
        sb.Append(If(d309 = "", SPACE_HANKAKU, d309))
        '睡眠(1)
        Dim d310 As String = Util.checkDBNullValue(row.Cells("D310").Value)
        sb.Append(If(d310 = "", SPACE_HANKAKU, d310))
        '生活習慣の改善(1)
        Dim d311 As String = Util.checkDBNullValue(row.Cells("D311").Value)
        sb.Append(If(d311 = "", SPACE_HANKAKU, d311))
        '保健指導の希望(1)
        Dim d312 As String = Util.checkDBNullValue(row.Cells("D312").Value)
        sb.Append(If(d312 = "", SPACE_HANKAKU, d312))
        '予備21(13)
        sb.Append(Space(13))

        '新しいフォーマット対応用↓
        '脂質は空白で埋める((1+8)*2で18)
        sb.Append(Space(18))
        '尿一般・腎機能
        '血清クレアチニン(1)(8)　空白で埋める
        sb.Append(Space(9))
        '対象者(1)　空白で埋める
        sb.Append(Space(1))
        '実施理由(40)　空白で埋める
        sb.Append(Space(40))
        'eGFR(1)(8)
        Dim d313 As String = Util.checkDBNullValue(row.Cells("D313").Value)
        sb.Append(If(d313 = "", "0", "1"))
        sb.Append(convValue(d313, 3))
        '心電図(1)　空白で埋める
        sb.Append(Space(1))
        '子宮(2)　空白で埋める
        sb.Append(Space(2))
        '眼底((1+8)*2+1で19)　空白で埋める
        sb.Append(Space(19))
        '質問票(1)(1)　空白で埋める
        sb.Append(Space(2))
        '伝達事項(1)(1)(1)　空白で埋める
        sb.Append(Space(3))

        Return sb.ToString()
    End Function

    ''' <summary>
    ''' 検査値の変換
    ''' </summary>
    ''' <param name="val">検査値</param>
    ''' <param name="pattern">変換パターン(1 or 2 or 3 or 4 or 5)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function convValue(val As String, pattern As Integer) As String
        '値が空、または、適切な数値ではない場合
        If val = "" Then
            If pattern = 1 OrElse pattern = 2 Then
                Return "".PadLeft(4, SPACE_HANKAKU)
            ElseIf pattern = 3 OrElse pattern = 4 OrElse pattern = 5 Then
                Return "".PadLeft(8, SPACE_HANKAKU)
            Else
                Return ""
            End If
        ElseIf Not System.Text.RegularExpressions.Regex.IsMatch(val, "^\d+(\.\d+)?$") Then
            Return ""
        End If

        Dim valLength As Integer = val.Length '文字数
        Dim decimalPointPosition As Integer = val.IndexOf(".") '小数点の位置
        Dim integerPart As String '整数部
        Dim decimalPart As String = 0 '小数部
        If decimalPointPosition > 0 Then
            '整数
            integerPart = val.Split(".")(0)

            '小数
            decimalPart = val.Split(".")(1)
        Else
            integerPart = val
        End If

        If pattern = 1 Then '定量　整数部3桁、小数部1桁
            Return integerPart.PadLeft(3, "0") & decimalPart.Substring(0, 1)
        ElseIf pattern = 2 Then '定量　整数部2桁、小数部2桁
            Return integerPart.PadLeft(2, "0") & decimalPart.PadRight(2, "0")
        ElseIf pattern = 3 Then '定量　整数部6桁、小数部2桁
            Return integerPart.PadLeft(6, "0") & decimalPart.PadRight(2, "0")
        ElseIf pattern = 4 Then '定性　整数部6桁、小数部2桁
            Return "00000" & integerPart & "00"
        ElseIf pattern = 5 Then '定性・定量　整数部6桁、小数部2桁
            '定性なので先頭は1
            Return "10000" & integerPart & "00"
        Else
            Return ""
        End If

    End Function

    ''' <summary>
    ''' 全角文章の空白埋め
    ''' </summary>
    ''' <param name="txt">文章</param>
    ''' <param name="targetByte">必要な桁(Byte数)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function paddingZenkakuText(txt As String, targetByte As Integer) As String
        '文字数
        Dim txtLength As Integer = txt.Length
        'byte数
        Dim txtByte As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(txt)

        Return txt.PadRight(targetByte - (txtByte - txtLength), SPACE_HANKAKU)
    End Function
End Class