Imports System.Data.OleDb
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

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
        If Not IsNothing(dgvIndM.CurrentRow) Then
            dgvIndM.CurrentRow.Selected = False
        End If

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
            MsgBox("事業所名を入力して下さい。", MsgBoxStyle.Exclamation)
            indBox.Focus()
            Return
        End If
        'カナ
        Dim kana As String = kanaBox.Text
        If kana = "" Then
            MsgBox("カナを入力して下さい。", MsgBoxStyle.Exclamation)
            kanaBox.Focus()
            Return
        End If
        '健保記号(4桁)
        Dim kigo4 As String = kigo4Box.Text
        If Not System.Text.RegularExpressions.Regex.IsMatch(kigo4, "^\d\d\d\d$") Then
            MsgBox("健保記号は4桁の数値を入力して下さい。", MsgBoxStyle.Exclamation)
            kigo4Box.Focus()
            Return
        End If
        '健保符号(6桁)
        Dim fugo6 As String = fugo6Box.Text
        If Not System.Text.RegularExpressions.Regex.IsMatch(fugo6, "^\d\d\d\d\d\d$") Then
            MsgBox("健保符号は6桁の数値を入力して下さい。", MsgBoxStyle.Exclamation)
            fugo6Box.Focus()
            Return
        End If
        '〒
        Dim post As String = postBox.Text
        '住所
        Dim jyu As String = jyuBox.Text
        'TEL
        Dim tel As String = telBox.Text
        'FAX
        Dim fax As String = faxBox.Text
        '担当者
        Dim tanto As String = tantoBox.Text

        '登録
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Health3)
        Dim sql As String = "select * from IndM where Ind = '" & ind & "'"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If rs.RecordCount <= 0 Then
            '新規登録
            rs.AddNew()
            rs.Fields("Ind").Value = ind
            rs.Fields("Kana").Value = kana
            rs.Fields("Kigo4").Value = kigo4
            rs.Fields("Fugo6").Value = fugo6
            rs.Fields("Post").Value = post
            rs.Fields("Jyu").Value = jyu
            rs.Fields("Tel").Value = tel
            rs.Fields("Fax").Value = fax
            rs.Fields("Tanto").Value = tanto
            rs.Update()
            rs.Close()
            cn.Close()

            '再表示
            displayDgvIndM()
        Else
            '更新登録
            Dim result As DialogResult = MessageBox.Show("変更してよろしいですか？", "変更", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = Windows.Forms.DialogResult.Yes Then
                rs.Fields("Ind").Value = ind
                rs.Fields("Kana").Value = kana
                rs.Fields("Kigo4").Value = kigo4
                rs.Fields("Fugo6").Value = fugo6
                rs.Fields("Post").Value = post
                rs.Fields("Jyu").Value = jyu
                rs.Fields("Tel").Value = tel
                rs.Fields("Fax").Value = fax
                rs.Fields("Tanto").Value = tanto
                rs.Update()
                rs.Close()
                cn.Close()

                '再表示
                displayDgvIndM()
            Else
                rs.Close()
                cn.Close()
            End If
        End If

    End Sub

    ''' <summary>
    ''' クリアボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        clearInput()
        If Not IsNothing(dgvIndM.CurrentRow) Then
            dgvIndM.CurrentRow.Selected = False
        End If
        indBox.Focus()
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
            MsgBox("事業所名を入力して下さい。", MsgBoxStyle.Exclamation)
            indBox.Focus()
            Return
        End If

        '削除
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Health3)
        Dim sql As String = "select * from IndM where Ind = '" & ind & "'"
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
                displayDgvIndM()
            Else
                rs.Close()
                cn.Close()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        '件数
        Dim rowsCount As Integer = dgvIndM.Rows.Count

        '現在日付
        Dim nowYmd As String = DateTime.Now.ToString("yyyy/MM/dd")

        '貼り付けデータ作成
        Dim dataList As New List(Of String(,))
        Dim dataArray(35, 10) As String
        Dim arrayRowIndex As Integer = 0
        For i As Integer = 0 To rowsCount - 1
            If arrayRowIndex = 36 Then
                dataList.Add(dataArray.Clone())
                Array.Clear(dataArray, 0, dataArray.Length)
                arrayRowIndex = 0
            End If

            'No.
            dataArray(arrayRowIndex, 0) = i + 1
            '事業所名
            dataArray(arrayRowIndex, 1) = Util.checkDBNullValue(dgvIndM("Ind", i).Value)
            'ｶﾅ
            dataArray(arrayRowIndex, 2) = Util.checkDBNullValue(dgvIndM("Kana", i).Value)
            '記号
            dataArray(arrayRowIndex, 3) = Util.checkDBNullValue(dgvIndM("Kigo4", i).Value)
            '符号
            dataArray(arrayRowIndex, 4) = Util.checkDBNullValue(dgvIndM("Fugo6", i).Value)
            '登録数
            dataArray(arrayRowIndex, 5) = Util.checkDBNullValue(dgvIndM("PCount", i).Value)
            '最終日付
            dataArray(arrayRowIndex, 6) = Util.checkDBNullValue(dgvIndM("MaxYmd", i).Value)
            'TEL
            dataArray(arrayRowIndex, 7) = Util.checkDBNullValue(dgvIndM("Tel", i).Value)
            '担当者
            dataArray(arrayRowIndex, 8) = Util.checkDBNullValue(dgvIndM("Tanto", i).Value)
            '〒
            dataArray(arrayRowIndex, 9) = Util.checkDBNullValue(dgvIndM("Post", i).Value)
            '住所
            dataArray(arrayRowIndex, 10) = Util.checkDBNullValue(dgvIndM("Jyu", i).Value)

            arrayRowIndex += 1
        Next
        dataList.Add(dataArray.Clone())

        'エクセル
        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWorkBooks As Excel.Workbooks = objExcel.Workbooks
        Dim objWorkBook As Excel.Workbook = objWorkBooks.Open(TopForm.excelFilePass)
        Dim oSheet As Excel.Worksheet = objWorkBook.Worksheets("事業所一覧改")
        objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
        objExcel.ScreenUpdating = False

        '日付
        oSheet.Range("G2").Value = nowYmd

        '必要枚数コピペ
        For i As Integer = 0 To dataList.Count - 2
            Dim xlPasteRange As Excel.Range = oSheet.Range("A" & (41 + (40 * i))) 'ペースト先
            oSheet.Rows("1:40").copy(xlPasteRange)
            oSheet.HPageBreaks.Add(oSheet.Range("A" & (41 + (40 * i)))) '改ページ
        Next

        'データ貼り付け
        For i As Integer = 0 To dataList.Count - 1
            oSheet.Range("L" & (2 + 40 * i)).Value = (i + 1) & " 頁"
            oSheet.Range("B" & (4 + 40 * i), "L" & (39 + 40 * i)).Value = dataList(i)
        Next

        objExcel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
        objExcel.ScreenUpdating = True

        '変更保存確認ダイアログ非表示
        objExcel.DisplayAlerts = False

        '印刷
        Dim printState As String = Util.getIniString("System", "Printer", TopForm.iniFilePath)
        If printState = "Y" Then
            oSheet.PrintOut()
        ElseIf printState = "N" Then
            objExcel.Visible = True
            oSheet.PrintPreview(1)
        End If

        ' EXCEL解放
        objExcel.Quit()
        Marshal.ReleaseComObject(objWorkBook)
        Marshal.ReleaseComObject(objExcel)
        oSheet = Nothing
        objWorkBook = Nothing
        objExcel = Nothing
    End Sub
End Class