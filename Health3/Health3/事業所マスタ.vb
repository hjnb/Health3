Imports System.Data.OleDb

Public Class 事業所マスタ

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
    ''' keyDownイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 事業所マスタ_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            If e.Control = False Then
                Me.SelectNextControl(Me.ActiveControl, Not e.Shift, True, True, True)
            End If
        End If
    End Sub

    ''' <summary>
    ''' loadイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 事業所マスタ_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.KeyPreview = True

        'データグリッドビュー初期設定
        initDgvIndM()

        '事業所データ表示
        displayDgvIndM()
    End Sub

    ''' <summary>
    ''' データグリッドビュー初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initDgvIndM()
        Util.EnableDoubleBuffering(dgvIndM)

        With dgvIndM
            .AllowUserToAddRows = False '行追加禁止
            .AllowUserToResizeColumns = False '列の幅をユーザーが変更できないようにする
            .AllowUserToResizeRows = False '行の高さをユーザーが変更できないようにする
            .AllowUserToDeleteRows = False '行削除禁止
            .BorderStyle = BorderStyle.FixedSingle
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control)
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
            .ReadOnly = True
        End With
    End Sub

    ''' <summary>
    ''' 入力内容クリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clearInput()
        indBox.Text = ""
        kanaBox.Text = ""
        kigo4Box.Text = ""
        fugo6Box.Text = ""
        postBox.Text = ""
        jyuBox.Text = ""
        telBox.Text = ""
        faxBox.Text = ""
        tantoBox.Text = ""
    End Sub

    ''' <summary>
    ''' 事業所マスタデータ表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub displayDgvIndM()
        '内容クリア
        dgvIndM.Columns.Clear()
        clearInput()

        'データ取得
        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Health3)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select IM.Ind, IM.Kana, IM.Kigo4, IM.Fugo6, IM.MaxYmd, U.PCount, IM.Post, IM.Jyu, IM.Tel, IM.Fax, IM.Tanto from (select I.Ind, Kana, Kigo4, Fugo6, M.MaxYmd, Post, Jyu, Tel, Fax, Tanto from IndM as I left outer join (select Ind, max(Ymd) as MaxYmd from KenD group by Ind) as M on I.Ind = M.Ind) as IM left outer join (select Ind, count(Ind) as PCount from UsrM group by Ind) as U on IM.Ind = U.Ind order by IM.Kana"
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
        Dim da As OleDbDataAdapter = New OleDbDataAdapter()
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, rs, "IndM")
        Dim dt As DataTable = ds.Tables("IndM")

        '表示
        dgvIndM.DataSource = dt
        cnn.Close()

        '幅設定等
        With dgvIndM
            With .Columns("Ind")
                .HeaderText = "事業所名"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 270
                .Frozen = True
            End With
            With .Columns("Kana")
                .HeaderText = "カナ"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 60
            End With
            With .Columns("Kigo4")
                .HeaderText = "健保記号"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 70
            End With
            With .Columns("Fugo6")
                .HeaderText = "健保符号"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 75
            End With
            With .Columns("PCount")
                .HeaderText = "登録数"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 60
            End With
            With .Columns("MaxYmd")
                .HeaderText = "最終日付"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 80
            End With
            With .Columns("Post")
                .HeaderText = "〒"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 80
            End With
            With .Columns("Jyu")
                .HeaderText = "住所"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 375
            End With
            With .Columns("Tel")
                .HeaderText = "TEL"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 105
            End With
            With .Columns("Fax")
                .HeaderText = "FAX"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 105
            End With
            With .Columns("Tanto")
                .HeaderText = "担当者"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 105
            End With

        End With

        'フォーカス
        indBox.Focus()
    End Sub

    ''' <summary>
    ''' CellFormattingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvIndM_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles dgvIndM.CellFormatting
        If e.RowIndex >= 0 AndAlso dgvIndM.Columns(e.ColumnIndex).Name = "MaxYmd" Then
            e.Value = Util.convADStrToWarekiStr(Util.checkDBNullValue(dgvIndM("MaxYmd", e.RowIndex).Value))
            e.FormattingApplied = True
        End If
    End Sub

    ''' <summary>
    ''' 列ヘッダーダブルクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvIndM_ColumnHeaderMouseDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvIndM.ColumnHeaderMouseDoubleClick
        Dim targetColumn As DataGridViewColumn = dgvIndM.Columns(e.ColumnIndex) '選択列
        dgvIndM.Sort(targetColumn, System.ComponentModel.ListSortDirection.Ascending) '昇順でソート
    End Sub

    ''' <summary>
    ''' CellMouseClickイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvIndM_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgvIndM.CellMouseClick
        If e.RowIndex >= 0 Then
            Dim ind As String = Util.checkDBNullValue(dgvIndM("Ind", e.RowIndex).Value)
            Dim kana As String = Util.checkDBNullValue(dgvIndM("Kana", e.RowIndex).Value)
            Dim kigo4 As String = Util.checkDBNullValue(dgvIndM("Kigo4", e.RowIndex).Value)
            Dim fugo6 As String = Util.checkDBNullValue(dgvIndM("Fugo6", e.RowIndex).Value)
            Dim post As String = Util.checkDBNullValue(dgvIndM("Post", e.RowIndex).Value)
            Dim jyu As String = Util.checkDBNullValue(dgvIndM("Jyu", e.RowIndex).Value)
            Dim tel As String = Util.checkDBNullValue(dgvIndM("Tel", e.RowIndex).Value)
            Dim fax As String = Util.checkDBNullValue(dgvIndM("Fax", e.RowIndex).Value)
            Dim tanto As String = Util.checkDBNullValue(dgvIndM("Tanto", e.RowIndex).Value)
            
            '値をセット
            indBox.Text = ind
            kanaBox.Text = kana
            kigo4Box.Text = kigo4
            fugo6Box.Text = fugo6
            postBox.Text = post
            jyuBox.Text = jyu
            telBox.Text = tel
            faxBox.Text = fax
            tantoBox.Text = tanto

            'フォーカス
            indBox.Focus()
            indBox.SelectionStart = indBox.TextLength
        End If
    End Sub

    ''' <summary>
    ''' CellPaintingイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dgvIndM_CellPainting(sender As Object, e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles dgvIndM.CellPainting
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
End Class