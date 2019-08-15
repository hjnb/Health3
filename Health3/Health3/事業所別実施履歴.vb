Imports System.Data.OleDb
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class 事業所別実施履歴

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

    Private Sub 事業所別実施履歴_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.KeyPreview = True

        '印刷ラジオボタン初期値設定
        initPrintState()

        '事業所リスト初期設定
        initIndList()

        'データグリッドビュー初期設定
        initDgvList()
    End Sub

    ''' <summary>
    ''' keyDownイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 事業所別実施履歴_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.F12 Then
            '(Alt + F12)キー押下
            nyPanel.Visible = Not nyPanel.Visible
        End If
    End Sub

    ''' <summary>
    ''' 事業所リスト初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initIndList()
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Health3)
        Dim sql As String = "select Ind from IndM order by Kana"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic)
        While Not rs.EOF
            Dim ind As String = Util.checkDBNullValue(rs.Fields("Ind").Value)
            indList.Items.Add(ind)
            rs.MoveNext()
        End While
        rs.Close()
        cn.Close()
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
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control)
            .DefaultCellStyle.ForeColor = Color.Black
            .DefaultCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Control)
            .DefaultCellStyle.SelectionForeColor = Color.Black
            .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .RowHeadersWidth = 30
            .ColumnHeadersHeight = 18
            .RowTemplate.Height = 18
            .RowTemplate.HeaderCell = New dgvRowHeaderCell() '行ヘッダの三角マークを非表示に
            .BackgroundColor = Color.FromKnownColor(KnownColor.Control)
            .ShowCellToolTips = False
            .EnableHeadersVisualStyles = False
            .Font = New Font("ＭＳ Ｐゴシック", 9)
            .ReadOnly = False
        End With
    End Sub

    ''' <summary>
    ''' 直近7回分の健診日を表示
    ''' </summary>
    ''' <param name="ind">事業所名</param>
    ''' <param name="dt">表示用データテーブル</param>
    ''' <remarks></remarks>
    Private Sub latest7DateSet(ind As String, dt As DataTable)
        'データ取得
        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Health3)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select U.Nam, U.Kana, K.Ymd from UsrM as U inner join (select Ind, Kana, Ymd from KenD where Ind = '" & ind & "') as K on U.Kana = K.Kana and U.Ind = K.Ind order by U.Kana, Ymd Desc"
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockReadOnly)
        For Each row As DataRow In dt.Rows
            Dim nam As String = row("Nam")
            rs.Filter = "Nam = '" & nam & "'"
            Dim rCount As Integer = 0
            While Not rs.EOF
                If rCount = 7 Then
                    row("Continued") = "・・・"
                    Exit While
                End If

                Dim ymd As String = Util.checkDBNullValue(rs.Fields("Ymd").Value)
                row("J" & (rCount + 1)) = ymd
                rCount += 1
                rs.MoveNext()
            End While
        Next
    End Sub

    ''' <summary>
    ''' 対象事業所のリスト表示
    ''' </summary>
    ''' <param name="ind">事業所名</param>
    ''' <remarks></remarks>
    Private Sub displayDgvList(ind As String)
        '内容クリア
        dgvList.Columns.Clear()

        'データ取得
        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Health3)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select Nam, Birth, Int((Format(NOW(),'YYYYMMDD')-Format(Birth, 'YYYYMMDD'))/10000) as Age from UsrM where Ind = '" & ind & "' order by Kana"
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
        Dim da As OleDbDataAdapter = New OleDbDataAdapter()
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, rs, "UsrM")
        Dim dt As DataTable = ds.Tables("UsrM")

        '列追加
        dt.Columns.Add("Check", GetType(Boolean)) 'チェックボックス
        dt.Columns.Add("J1", GetType(String)) '実施日1
        dt.Columns.Add("J2", GetType(String)) '実施日2
        dt.Columns.Add("J3", GetType(String)) '実施日3
        dt.Columns.Add("J4", GetType(String)) '実施日4
        dt.Columns.Add("J5", GetType(String)) '実施日5
        dt.Columns.Add("J6", GetType(String)) '実施日6
        dt.Columns.Add("J7", GetType(String)) '実施日7
        dt.Columns.Add("Continued", GetType(String)) '・・・
        For Each row As DataRow In dt.Rows
            row("Check") = False
        Next

        '直近5回分健診日表示
        latest7DateSet(ind, dt)

        '表示
        dgvList.DataSource = dt

        '幅設定等
        With dgvList

            'サイズ
            If dgvList.Rows.Count <= 30 Then
                .Size = New Size(752, 559)
            Else
                .Size = New Size(769, 559)
            End If

            With .Columns("Check")
                .DisplayIndex = 0
                .HeaderText = ""
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 30
            End With
            With .Columns("Nam")
                .HeaderText = "氏名"
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 95
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .ReadOnly = True
            End With
            With .Columns("Birth")
                .HeaderText = "生年月日"
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 75
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .ReadOnly = True
            End With
            With .Columns("Age")
                .HeaderText = "年齢"
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 45
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .ReadOnly = True
                .Frozen = True
            End With
            For i As Integer = 1 To 7
                With .Columns("J" & i)
                    .HeaderText = i
                    .SortMode = DataGridViewColumnSortMode.NotSortable
                    .Width = 95
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    .ReadOnly = True
                End With
            Next
            With .Columns("Continued")
                .HeaderText = "・・・"
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 35
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .ReadOnly = True
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
    ''' 事業所リスト値変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub indList_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles indList.SelectedValueChanged
        Dim ind As String = indList.Text
        indLabel.Text = ind
        displayDgvList(ind)
    End Sub

    ''' <summary>
    ''' 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        '事業所名
        Dim ind As String = indLabel.Text
        If ind = "" Then
            MsgBox("事業所を選択して下さい。", MsgBoxStyle.Exclamation)
            Return
        End If

        '印刷データ作成
        Dim dataList As New List(Of String(,))
        Dim dataArray(38, 11) As String
        Dim arrayRowIndex As Integer = 0
        For i As Integer = 0 To dgvList.Rows.Count - 1
            If arrayRowIndex = 39 Then
                dataList.Add(dataArray.Clone())
                Array.Clear(dataArray, 0, dataArray.Length)
                arrayRowIndex = 0
            End If

            'No.
            dataArray(arrayRowIndex, 0) = i + 1
            '氏名
            dataArray(arrayRowIndex, 1) = Util.checkDBNullValue(dgvList("Nam", i).Value)
            '生年月日
            dataArray(arrayRowIndex, 2) = Util.checkDBNullValue(dgvList("Birth", i).Value)
            '年齢
            dataArray(arrayRowIndex, 3) = Util.checkDBNullValue(dgvList("Age", i).Value)
            '1
            dataArray(arrayRowIndex, 4) = Util.checkDBNullValue(dgvList("J1", i).Value)
            '2
            dataArray(arrayRowIndex, 5) = Util.checkDBNullValue(dgvList("J2", i).Value)
            '3
            dataArray(arrayRowIndex, 6) = Util.checkDBNullValue(dgvList("J3", i).Value)
            '4
            dataArray(arrayRowIndex, 7) = Util.checkDBNullValue(dgvList("J4", i).Value)
            '5
            dataArray(arrayRowIndex, 8) = Util.checkDBNullValue(dgvList("J5", i).Value)
            '6
            dataArray(arrayRowIndex, 9) = Util.checkDBNullValue(dgvList("J6", i).Value)
            '7
            dataArray(arrayRowIndex, 10) = Util.checkDBNullValue(dgvList("J7", i).Value)
            '・・・
            dataArray(arrayRowIndex, 11) = Util.checkDBNullValue(dgvList("Continued", i).Value)

            arrayRowIndex += 1
        Next
        dataList.Add(dataArray.Clone())

        'エクセル
        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWorkBooks As Excel.Workbooks = objExcel.Workbooks
        Dim objWorkBook As Excel.Workbook = objWorkBooks.Open(topForm.excelFilePass)
        Dim oSheet As Excel.Worksheet = objWorkBook.Worksheets("健診実施履歴")
        objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
        objExcel.ScreenUpdating = False

        '事業所名
        oSheet.Range("E2").Value = ind
        '日付
        Dim nowYmd As String = DateTime.Now.ToString("yyyy/MM/dd")
        oSheet.Range("L2").Value = nowYmd

        '必要枚数コピペ
        For i As Integer = 0 To dataList.Count - 2
            Dim xlPasteRange As Excel.Range = oSheet.Range("A" & (45 + (44 * i))) 'ペースト先
            oSheet.Rows("1:44").copy(xlPasteRange)
            oSheet.HPageBreaks.Add(oSheet.Range("A" & (45 + (44 * i)))) '改ページ
        Next

        'データ貼り付け
        For i As Integer = 0 To dataList.Count - 1
            oSheet.Range("B" & (5 + 44 * i), "M" & (43 + 44 * i)).Value = dataList(i)
        Next

        objExcel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
        objExcel.ScreenUpdating = True

        '変更保存確認ダイアログ非表示
        objExcel.DisplayAlerts = False

        '印刷
        If rbtnPrint.Checked = True Then
            oSheet.PrintOut()
        ElseIf rbtnPreview.Checked = True Then
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

    ''' <summary>
    ''' 封筒ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEnvelope_Click(sender As System.Object, e As System.EventArgs) Handles btnEnvelope.Click
        '印刷データ作成
        Dim namList As New List(Of String)
        For Each row As DataGridViewRow In dgvList.Rows
            If row.Cells("Check").Value Then
                namList.Add(row.Cells("Nam").Value & "　様")
            End If
        Next

        If rbtnNaga3.Checked Then
            printNaga3(namList)
        ElseIf rbtnNaga4.Checked Then
            printNaga4(namList)
        ElseIf rbtnKaku2.Checked Then
            printKaku2(namList)
        End If
    End Sub

    ''' <summary>
    ''' 長形3号印刷
    ''' </summary>
    ''' <param name="namList"></param>
    ''' <remarks></remarks>
    Private Sub printNaga3(namList As List(Of String))
        'エクセル
        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWorkBooks As Excel.Workbooks = objExcel.Workbooks
        Dim objWorkBook As Excel.Workbook = objWorkBooks.Open(TopForm.excelFilePass)
        Dim oSheet As Excel.Worksheet = objWorkBook.Worksheets("長形３号")
        objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
        objExcel.ScreenUpdating = False

        '削除
        oSheet.Range("J2").Value = ""
        oSheet.Range("E8").Value = ""
        oSheet.Range("E9").Value = ""
        oSheet.Range("H21").Value = ""
        oSheet.Range("J21").Value = ""

        '必要枚数コピペ
        For i As Integer = 0 To namList.Count - 2
            Dim xlPasteRange As Excel.Range = oSheet.Range("A" & (44 + (43 * i))) 'ペースト先
            oSheet.Rows("1:43").copy(xlPasteRange)
            oSheet.HPageBreaks.Add(oSheet.Range("A" & (44 + (43 * i)))) '改ページ
        Next

        'データ書き込み
        Dim titleStr As String = n3textBox.Text
        For i As Integer = 0 To namList.Count - 1
            oSheet.Range("E" & (11 + 43 * i)).Value = namList(i)
            oSheet.Range("E" & (14 + 43 * i)).Value = titleStr
        Next

        objExcel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
        objExcel.ScreenUpdating = True

        '変更保存確認ダイアログ非表示
        objExcel.DisplayAlerts = False

        '印刷
        If rbtnPrint.Checked = True Then
            oSheet.PrintOut()
        ElseIf rbtnPreview.Checked = True Then
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

    ''' <summary>
    ''' 長形4号印刷
    ''' </summary>
    ''' <param name="namList"></param>
    ''' <remarks></remarks>
    Private Sub printNaga4(namList As List(Of String))
        'エクセル
        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWorkBooks As Excel.Workbooks = objExcel.Workbooks
        Dim objWorkBook As Excel.Workbook = objWorkBooks.Open(TopForm.excelFilePass)
        Dim oSheet As Excel.Worksheet = objWorkBook.Worksheets("長形４号")
        objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
        objExcel.ScreenUpdating = False

        '削除
        oSheet.Range("F2").Value = ""
        oSheet.Range("C7").Value = ""
        oSheet.Range("C8").Value = ""
        oSheet.Range("D19").Value = ""
        oSheet.Range("G19").Value = ""

        '必要枚数コピペ
        For i As Integer = 0 To namList.Count - 2
            Dim xlPasteRange As Excel.Range = oSheet.Range("A" & (29 + (28 * i))) 'ペースト先
            oSheet.Rows("1:28").copy(xlPasteRange)
            oSheet.HPageBreaks.Add(oSheet.Range("A" & (29 + (28 * i)))) '改ページ
        Next

        'データ書き込み
        For i As Integer = 0 To namList.Count - 1
            oSheet.Range("C" & (10 + 28 * i)).Value = namList(i)
        Next

        objExcel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
        objExcel.ScreenUpdating = True

        '変更保存確認ダイアログ非表示
        objExcel.DisplayAlerts = False

        '印刷
        If rbtnPrint.Checked = True Then
            oSheet.PrintOut()
        ElseIf rbtnPreview.Checked = True Then
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

    ''' <summary>
    ''' 角形2号印刷
    ''' </summary>
    ''' <param name="namList"></param>
    ''' <remarks></remarks>
    Private Sub printKaku2(namList As List(Of String))
        'エクセル
        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWorkBooks As Excel.Workbooks = objExcel.Workbooks
        Dim objWorkBook As Excel.Workbook = objWorkBooks.Open(TopForm.excelFilePass)
        Dim oSheet As Excel.Worksheet = objWorkBook.Worksheets("角形２号")
        objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
        objExcel.ScreenUpdating = False

        '削除
        oSheet.Range("Y6").Value = ""
        oSheet.Range("K19").Value = ""
        oSheet.Range("K20").Value = ""

        '必要枚数コピペ
        For i As Integer = 0 To namList.Count - 2
            Dim xlPasteRange As Excel.Range = oSheet.Range("A" & (58 + (57 * i))) 'ペースト先
            oSheet.Rows("1:57").copy(xlPasteRange)
            oSheet.HPageBreaks.Add(oSheet.Range("A" & (58 + (57 * i)))) '改ページ
        Next

        'データ書き込み
        For i As Integer = 0 To namList.Count - 1
            oSheet.Range("K" & (22 + 57 * i)).Value = namList(i)
        Next

        objExcel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
        objExcel.ScreenUpdating = True

        '変更保存確認ダイアログ非表示
        objExcel.DisplayAlerts = False

        '印刷
        If rbtnPrint.Checked = True Then
            oSheet.PrintOut()
        ElseIf rbtnPreview.Checked = True Then
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

    ''' <summary>
    ''' 個人票ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPersonal_Click(sender As System.Object, e As System.EventArgs) Handles btnPersonal.Click

    End Sub
End Class