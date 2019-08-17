Imports System.Data.OleDb
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class 事業所別実施履歴

    '個人票印刷の基準値範囲外の記号
    Private HASHMARK As String = " #"

    '蛋白、糖、潜血用
    Private numberDic1 As New Dictionary(Of String, String) From {{"1", "(－)"}, {"2", "(±)"}, {"3", "(＋)"}, {"4", "(2＋)"}, {"5", "(3＋)"}}

    'ｳﾛﾋﾞﾘﾉｰｹﾞﾝ用
    Private numberDic2 As New Dictionary(Of String, String) From {{"2", "(±)"}, {"3", "(＋)"}, {"4", "(2＋)"}, {"5", "(3＋)"}}

    '男女で基準値が異なる項目名
    Private stdValName() As String = {"Ｆｅ", "ＨＤＬ－ｺﾚｽﾃﾛｰﾙ", "γ－ＧＴＰ", "ｸﾚｱﾁﾆﾝ", "血清ｸﾚｱﾁﾆﾝ", "赤沈", "赤血球数", "血色素量", "ﾍﾏﾄｸﾘｯﾄ", "ﾍﾓｸﾞﾛﾋﾞﾝ"}

    ''' <summary>
    ''' 個人票印刷用データクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private Class PrintData
        Public ind As String
        Public nam As String
        Public birth As String
        Public sex As String
        Public kenData(,) As String

        Public Sub New(ind As String, nam As String, birth As String, sex As String, kenData(,) As String)
            Me.ind = ind
            Me.nam = nam
            Me.birth = birth
            Me.sex = sex
            Me.kenData = kenData
        End Sub
    End Class

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
        Dim printDataList As New List(Of PrintData)
        For Each row As DataGridViewRow In dgvList.Rows
            Dim checked As Boolean = row.Cells("Check").Value
            If checked Then
                '事業所名
                Dim ind As String = indLabel.Text
                '氏名
                Dim nam As String = Util.checkDBNullValue(row.Cells("Nam").Value)
                '生年月日
                Dim birth As String = Util.checkDBNullValue(row.Cells("Birth").Value)
                '性別、カナ取得
                Dim sex As String = ""
                Dim kana As String = ""
                setSexAndKana(ind, nam, birth, sex, kana)
                '健診実施日
                Dim dateArray(3) As String
                For i As Integer = 1 To 4
                    dateArray(i - 1) = Util.checkDBNullValue(row.Cells("J" & i).Value)
                Next
                '検診データ
                Dim kenData(,) As String = createKenData(ind, nam, birth, sex, kana, dateArray)

                'リストに追加
                printDataList.Add(New PrintData(ind, nam, birth, sex, kenData))
            End If
        Next
        'チェックが無い場合
        If printDataList.Count = 0 Then
            MsgBox("個人票印刷対象者にチェックを入れて下さい。", MsgBoxStyle.Exclamation)
            Return
        End If

        'エクセル
        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWorkBooks As Excel.Workbooks = objExcel.Workbooks
        Dim objWorkBook As Excel.Workbook = objWorkBooks.Open(TopForm.excelFilePass)
        Dim oSheet As Excel.Worksheet = objWorkBook.Worksheets("個人票改")
        objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
        objExcel.ScreenUpdating = False

        '事業所名
        oSheet.Range("I2").Value = indLabel.Text

        '必要枚数コピペ
        Dim pageRowCount As Integer = 70
        For i As Integer = 0 To printDataList.Count - 2
            Dim xlPasteRange As Excel.Range = oSheet.Range("A" & (pageRowCount + 1 + (pageRowCount * i))) 'ペースト先
            oSheet.Rows("1:" & pageRowCount).copy(xlPasteRange)
            oSheet.HPageBreaks.Add(oSheet.Range("A" & (pageRowCount + 1 + (pageRowCount * i)))) '改ページ
        Next

        'データ貼り付け
        For i As Integer = 0 To printDataList.Count - 1
            '氏名
            oSheet.Range("C" & (5 + pageRowCount * i)).Value = printDataList(i).nam
            '生年月日
            oSheet.Range("F" & (5 + pageRowCount * i)).Value = printDataList(i).birth
            '性別
            oSheet.Range("F" & (6 + pageRowCount * i)).Value = If(printDataList(i).sex = "1", "男", "女")
            '検診データ
            oSheet.Range("D" & (7 + pageRowCount * i), "K" & (65 + pageRowCount * i)).Value = printDataList(i).kenData
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
    ''' 性別とカナを取得
    ''' </summary>
    ''' <param name="ind">事業所名</param>
    ''' <param name="nam">漢字氏名</param>
    ''' <param name="birth">生年月日(和暦)</param>
    ''' <param name="sex">性別用変数</param>
    ''' <param name="kana">カナ用変数</param>
    ''' <remarks></remarks>
    Private Sub setSexAndKana(ind As String, nam As String, birth As String, ByRef sex As String, ByRef kana As String)
        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Health3)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select Kana, Sex from UsrM where Ind = '" & ind & "' and Nam = '" & nam & "' and Birth = '" & birth & "'"
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
        sex = rs.Fields("Sex").Value
        kana = rs.Fields("Kana").Value
        rs.Close()
        cnn.Close()
    End Sub

    ''' <summary>
    ''' 個人票印刷用の検診結果直近４回分データ作成
    ''' </summary>
    ''' <param name="ind">事業所名</param>
    ''' <param name="nam">漢字氏名</param>
    ''' <param name="birth">生年月日</param>
    ''' <param name="sex">性別</param>
    ''' <param name="kana">カナ</param>
    ''' <param name="dateArray">健診実施日（４回分）</param>
    ''' <returns>検診データ</returns>
    ''' <remarks></remarks>
    Private Function createKenData(ind As String, nam As String, birth As String, sex As String, kana As String, dateArray() As String) As String(,)
        '基準値データ取得
        Dim cn As New ADODB.Connection()
        cn.Open(topForm.DB_Diagnose)
        Dim baseValDt As DataTable
        Dim rsBase As New ADODB.Recordset
        Dim sql As String = "select Nam, Low1, Upp1, Low2, Upp2 from StdM"
        rsBase.Open(sql, cn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
        Dim da As OleDbDataAdapter = New OleDbDataAdapter()
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, rsBase, "StdM")
        baseValDt = ds.Tables("StdM")

        '結果データ作成
        Dim cnHealth As New ADODB.Connection()
        cnHealth.Open(TopForm.DB_Health3)
        Dim result(58, 7) As String
        Dim count As Integer = 1
        For i As Integer = 0 To 3
            If dateArray(i) = "" Then
                Exit For
            Else
                Dim ymd As String = dateArray(i) '検診実施日

                'データ取得
                sql = "select * from KenD where Ind = '" & ind & "' and Kana = '" & kana & "' and D6 = '" & birth & "' and Ymd = '" & ymd & "'"
                Dim rs As New ADODB.Recordset
                rs.Open(sql, cnHealth, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

                '検診年月日
                result(0, i * 2) = ymd
                '年齢
                result(1, i * 2) = Util.calcAge(Util.convWarekiStrToADStr(birth), ymd)
                '職種 とりあえず空白
                result(2, i * 2) = ""
                '既往歴・自覚症状
                Dim d33 As String = Util.checkDBNullValue(rs.Fields("D33").Value)
                Dim d35 As String = Util.checkDBNullValue(rs.Fields("D35").Value)
                If d33 <> "" AndAlso d35 <> "" Then
                    result(3, i * 2) = d33 & Environment.NewLine & d35
                ElseIf d33 = "" Then
                    result(3, i * 2) = d35
                ElseIf d35 = "" Then
                    result(3, i * 2) = d33
                End If
                '他覚症状
                result(6, i * 2) = ""
                '内科診察
                result(7, i * 2) = Util.checkDBNullValue(rs.Fields("D38").Value)
                '身長
                Dim height As Decimal = 0
                Dim heightStr As String = Util.checkDBNullValue(rs.Fields("D17").Value)
                If System.Text.RegularExpressions.Regex.IsMatch(heightStr, "^\d+(\.\d+)?$") Then
                    height = CDec(heightStr)
                End If
                result(8, i * 2) = heightStr
                '体重
                Dim weight As Decimal = 0
                Dim weightStr As String = Util.checkDBNullValue(rs.Fields("D19").Value)
                If System.Text.RegularExpressions.Regex.IsMatch(weightStr, "^\d+(\.\d+)?$") Then
                    weight = CDec(weightStr)
                End If
                result(9, i * 2) = weightStr
                'ＢＭＩ
                If height <> 0 AndAlso weight <> 0 Then
                    Dim bmi As Decimal = Math.Round(weight / ((height / 100) * (height / 100)), 1, MidpointRounding.AwayFromZero)
                    result(10, i * 2) = bmi
                Else
                    result(10, i * 2) = ""
                End If
                '腹囲
                result(11, i * 2) = Util.checkDBNullValue(rs.Fields("D25").Value)
                '血圧最高
                Dim d54 As String = Util.checkDBNullValue(rs.Fields("D54").Value)
                Dim d56 As String = Util.checkDBNullValue(rs.Fields("D56").Value)
                Dim ketuH As String = If(d56 <> "", d56, d54)
                result(12, i * 2) = checkBaseValue(ketuH, "最高血圧", baseValDt, sex)
                '　　最低
                Dim d60 As String = Util.checkDBNullValue(rs.Fields("D60").Value)
                Dim d62 As String = Util.checkDBNullValue(rs.Fields("D62").Value)
                Dim ketuL As String = If(d62 <> "", d62, d60)
                result(13, i * 2) = checkBaseValue(ketuL, "最低血圧", baseValDt, sex)
                '視力　右
                result(14, i * 2) = Util.checkDBNullValue(rs.Fields("D40").Value) & " ( " & Util.checkDBNullValue(rs.Fields("D42").Value) & " )"
                '　　　左
                result(15, i * 2) = Util.checkDBNullValue(rs.Fields("D44").Value) & " ( " & Util.checkDBNullValue(rs.Fields("D46").Value) & " )"
                '聴力　右　1000Hz
                Dim d47 As String = Util.checkDBNullValue(rs.Fields("D47").Value)
                result(16, i * 2) = If(d47 = "1", "所見ﾅｼ", If(d47 = "2", "所見ｱﾘ", ""))
                '　　　　　4000Hz
                Dim d48 As String = Util.checkDBNullValue(rs.Fields("D48").Value)
                result(17, i * 2) = If(d48 = "1", "所見ﾅｼ", If(d48 = "2", "所見ｱﾘ", ""))
                '　　　左　1000Hz
                Dim d49 As String = Util.checkDBNullValue(rs.Fields("D49").Value)
                result(18, i * 2) = If(d49 = "1", "所見ﾅｼ", If(d49 = "2", "所見ｱﾘ", ""))
                '　　　　　4000Hz
                Dim d50 As String = Util.checkDBNullValue(rs.Fields("D50").Value)
                result(19, i * 2) = If(d50 = "1", "所見ﾅｼ", If(d50 = "2", "所見ｱﾘ", ""))
                '検査方法
                If d47 <> "" OrElse d48 <> "" OrElse d49 <> "" OrElse d50 <> "" Then
                    result(20, i * 2) = "ｵｰｼﾞｵ"
                End If
                '胸部Ｘ線　直接・間接
                Dim d237 As String = Util.checkDBNullValue(rs.Fields("D237").Value)
                Dim d238 As String = Util.checkDBNullValue(rs.Fields("D238").Value)
                If d237 = "1" Then
                    result(21, i * 2) = "直接"
                    '　　　　　撮影年月日
                    result(22, i * 2) = ymd
                    '　　　　　フィルムNo
                    result(23, i * 2) = ""
                    '　　　　　診断
                    result(24, i * 2) = d238
                ElseIf d237 = "2" Then
                    result(21, i * 2) = "間接"
                    '　　　　　撮影年月日
                    result(22, i * 2) = ymd
                    '　　　　　フィルムNo
                    result(23, i * 2) = ""
                    '　　　　　診断
                    result(24, i * 2) = d238
                End If
                '胃部　Ｘ線
                Dim d242 As String = Util.checkDBNullValue(rs.Fields("D242").Value)
                result(25, i * 2) = d242
                '　　　カメラ
                result(26, i * 2) = ""
                '心電図
                result(27, i * 2) = Util.checkDBNullValue(rs.Fields("D213").Value)
                '尿　糖
                Dim d161 As String = Util.checkDBNullValue(rs.Fields("D161").Value)
                If numberDic1.ContainsKey(d161) Then
                    result(28, i * 2) = numberDic1(d161)
                End If
                '　　蛋白
                Dim d171 As String = Util.checkDBNullValue(rs.Fields("D171").Value)
                If numberDic1.ContainsKey(d171) Then
                    result(29, i * 2) = numberDic1(d171)
                End If
                '　　ｳﾛﾋﾞﾘﾉｰｹﾞﾝ
                result(30, i * 2) = ""
                '　　潜血
                Dim d173 As String = Util.checkDBNullValue(rs.Fields("D173").Value)
                If numberDic1.ContainsKey(d173) Then
                    result(31, i * 2) = numberDic1(d173)
                End If
                '貧血　白血球数
                result(32, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D190").Value).Replace(".", ""), "白血球数", baseValDt, sex)
                '　　　赤血球数
                result(33, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D188").Value), "赤血球数", baseValDt, sex)
                '　　　血色素量
                result(34, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D186").Value), "血色素量", baseValDt, sex)
                '　　　ﾍﾏﾄｸﾘｯﾄ
                result(35, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D184").Value), "ﾍﾏﾄｸﾘｯﾄ", baseValDt, sex)
                '　　　血小板数
                result(36, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D192").Value), "血小板数", baseValDt, sex)
                '肝機能　ＧＯＴ
                result(37, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D91").Value), "ＧＯＴ", baseValDt, sex)
                '　　　　ＧＰＴ
                result(38, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D95").Value), "ＧＰＴ", baseValDt, sex)
                '　　　　γーＧＴＰ
                result(39, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D101").Value), "γ－ＧＴＰ", baseValDt, sex)
                '　　　　ＡＬＰ
                result(40, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D103").Value), "ＡＬＰ", baseValDt, sex)
                '血中脂質　総コレステロール
                result(41, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D69").Value), "総ｺﾚｽﾃﾛｰﾙ", baseValDt, sex)
                '　　　　　中性脂肪
                result(42, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D75").Value), "中性脂肪", baseValDt, sex)
                '　　　　　ＨＤＬコレステロール
                result(43, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D81").Value), "ＨＤＬ－ｺﾚｽﾃﾛｰﾙ", baseValDt, sex)
                '　　　　　ＬＤＬコレステロール
                result(44, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D87").Value), "ＬＤＬ－ｺﾚｽﾃﾛｰﾙ", baseValDt, sex)
                '糖尿　血糖（空腹時）
                result(45, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D129").Value), "血糖", baseValDt, sex)
                '　　　HbA1c
                result(46, i * 2) = ""
                ''腎機能　尿酸
                result(47, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D165").Value), "尿酸", baseValDt, sex)
                '　　　　ｸﾚｱﾁﾆﾝ
                result(48, i * 2) = checkBaseValue(Util.checkDBNullValue(rs.Fields("D180").Value), "ｸﾚｱﾁﾆﾝ", baseValDt, sex)
                '肝炎　ＨＢｓ抗原
                Dim d72 As String = Util.checkDBNullValue(rs.Fields("D267").Value)
                If d72 = "1" Then
                    result(49, i * 2) = "(－)"
                ElseIf d72 = "2" Then
                    result(49, i * 2) = "(±)"
                ElseIf d72 = "3" Then
                    result(49, i * 2) = "(＋)"
                End If
                '　　　ＨＣＶ抗体
                Dim d73 As String = Util.checkDBNullValue(rs.Fields("D269").Value)
                If d73 = "1" Then
                    result(50, i * 2) = "感染なし"
                ElseIf d73 = "2" Then
                    result(50, i * 2) = "感染あり"
                ElseIf d73 = "3" Then
                    result(50, i * 2) = "要検査"
                End If
                '便潜血　１日目
                Dim d69 As String = Util.checkDBNullValue(rs.Fields("D251").Value)
                result(51, i * 2) = If(d69 = "1", "－", If(d69 = "2", "＋", ""))
                '　　　　２日目
                Dim d70 As String = Util.checkDBNullValue(rs.Fields("D253").Value)
                result(52, i * 2) = If(d70 = "1", "－", If(d70 = "2", "＋", ""))
                '医師の指示注意
                Dim d279a As String = Util.checkDBNullValue(rs.Fields("D279a").Value)
                Dim d279b As String = Util.checkDBNullValue(rs.Fields("D279b").Value)
                Dim d279c As String = Util.checkDBNullValue(rs.Fields("D279c").Value)
                Dim d279d As String = Util.checkDBNullValue(rs.Fields("D279d").Value)
                Dim d279e As String = Util.checkDBNullValue(rs.Fields("D279e").Value)
                Dim d279f As String = Util.checkDBNullValue(rs.Fields("D279f").Value)
                result(53, i * 2) = d279a & d279b & d279c & d279d & d279e & d279f

                rs.Close()
            End If
        Next

        Return result
    End Function

    ''' <summary>
    ''' 検査値が基準値範囲外かチェック
    ''' </summary>
    ''' <param name="resultValue">検査結果値</param>
    ''' <param name="itemName">検査項目名</param>
    ''' <param name="baseDt">基準値データテーブル</param>
    ''' <param name="sex">性別</param>
    ''' <returns>範囲外の場合は#記号を付けて返す</returns>
    ''' <remarks></remarks>
    Private Function checkBaseValue(resultValue As String, itemName As String, baseDt As DataTable, sex As String) As String
        If Not System.Text.RegularExpressions.Regex.IsMatch(resultValue, "^\d+(\.\d+)?$") Then
            Return resultValue
        Else
            '基準値の取得
            Dim low As Decimal
            Dim upp As Decimal
            If sex = "2" AndAlso Array.IndexOf(stdValName, itemName) >= 0 Then
                '女性用の基準値
                low = baseDt.Select("Nam = '" & itemName & "'")(0).Item("Low2")
                upp = baseDt.Select("Nam = '" & itemName & "'")(0).Item("Upp2")
            Else
                low = baseDt.Select("Nam = '" & itemName & "'")(0).Item("Low1")
                upp = baseDt.Select("Nam = '" & itemName & "'")(0).Item("Upp1")
            End If

            '基準値範囲外の場合は"#"記号を付ける
            If Not (low <= resultValue AndAlso resultValue <= upp) Then
                Return resultValue & HASHMARK
            Else
                Return resultValue
            End If
        End If
    End Function
End Class