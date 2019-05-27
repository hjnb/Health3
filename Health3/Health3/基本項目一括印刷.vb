Imports System.Data.OleDb
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class 基本項目一括印刷

    '健診項目○印用
    Private circleTypeArray() As String = {"メデック"}

    '採血種類
    Private bloodTypeArray() As String = {"生活習慣病予防健診"}

    'その他の検査項目用文字列
    Private itemArray() As String = {"腰椎ＸＰ：", "ＡＢＣ：", "ピロリ："}

    '事業所名
    Private ind As String

    '印刷状態
    Private printState As Boolean

    '全チェック制御用
    Private allCheckFlg As Boolean = True

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="ind">事業所名</param>
    ''' <param name="printState">印刷モード</param>
    ''' <remarks></remarks>
    Public Sub New(ind As String, printState As Boolean)
        InitializeComponent()
        Me.MinimizeBox = False
        Me.MaximizeBox = False
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle

        Me.ind = ind
        Me.printState = printState
    End Sub

    ''' <summary>
    ''' loadイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub 基本項目一括印刷_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'データグリッドビュー初期設定
        initDgvNamList()

        '一覧データ表示
        displayNamList()

        'コンボボックス初期設定
        initComboBox()
    End Sub

    ''' <summary>
    ''' コンボボックス初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initComboBox()
        '健診項目の○印
        circleTypeBox.Items.AddRange(circleTypeArray)

        '採血種類
        bloodTypeBox.Items.AddRange(bloodTypeArray)

        'その他の検査項目
        cb1.Items.AddRange(itemArray)
        cb2.Items.AddRange(itemArray)
        cb3.Items.AddRange(itemArray)
    End Sub

    ''' <summary>
    ''' データグリッドビュー初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initDgvNamList()
        Util.EnableDoubleBuffering(dgvNamList)

        With dgvNamList
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
            .RowHeadersVisible = False
            .RowTemplate.Height = 18
            .BackgroundColor = Color.FromKnownColor(KnownColor.Control)
            .ShowCellToolTips = False
            .EnableHeadersVisualStyles = False
            .Font = New Font("ＭＳ Ｐゴシック", 10)
            .ReadOnly = False
        End With
    End Sub

    ''' <summary>
    ''' 一覧データ表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub displayNamList()
        'データ取得
        Dim cnn As New ADODB.Connection
        cnn.Open(TopForm.DB_Health3)
        Dim rs As New ADODB.Recordset
        Dim sql As String = "select Nam, Kana, Sex, Birth, Int((Format(NOW(),'YYYYMMDD')-Format(Birth, 'YYYYMMDD'))/10000) as Age from UsrM where Ind = '" & ind & "' order by Kana"
        rs.Open(sql, cnn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly)
        Dim da As OleDbDataAdapter = New OleDbDataAdapter()
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, rs, "UsrM")
        Dim dt As DataTable = ds.Tables("UsrM")

        '列追加
        dt.Columns.Add("Check", GetType(Boolean)) 'チェックボックス
        For Each row As DataRow In dt.Rows
            row("Check") = False
        Next

        '表示
        dgvNamList.DataSource = dt
        cnn.Close()

        '幅設定等
        With dgvNamList
            If dgvNamList.Rows.Count >= 35 Then
                dgvNamList.Size = New Size(255, 654)
            End If

            With .Columns("Check")
                .DisplayIndex = 0
                .HeaderText = ""
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 35
                .HeaderCell.Style.Font = New Font("ＭＳ Ｐゴシック", 9)
            End With
            With .Columns("Nam")
                .HeaderText = "氏名"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 100
                .Frozen = True
                .ReadOnly = True
            End With
            With .Columns("Kana")
                .HeaderText = "カナ"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 100
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
                .Width = 90
                .ReadOnly = True
            End With
            With .Columns("Age")
                .HeaderText = "年齢"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .Width = 40
                .HeaderCell.Style.Font = New Font("ＭＳ Ｐゴシック", 9)
                .ReadOnly = True
            End With
        End With
    End Sub

    ''' <summary>
    ''' 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        '対象者のデータ取得
        Dim bloodType As String = bloodTypeBox.Text
        Dim dataList As New List(Of String(,))
        Dim dataArray(4, 7) As String
        For Each row As DataGridViewRow In dgvNamList.Rows
            If row.Cells("Check").Value = True Then
                'カナ
                dataArray(0, 0) = row.Cells("Kana").Value
                '氏名
                dataArray(1, 0) = row.Cells("Nam").Value
                '性別
                Dim sex As String = row.Cells("Sex").Value
                dataArray(3, 4) = If(sex = "1", "① 男 ・ 2 女　", "1 男 ・ ② 女　")
                '生年月日
                Dim age As Integer = row.Cells("Age").Value
                Dim birth As String = row.Cells("Birth").Value
                dataArray(4, 0) = birth.Split("/")(0) & "　年　" & birth.Split("/")(1) & "　月　" & birth.Split("/")(2) & "　日"
                dataArray(4, 7) = age & "　歳"

                'リストへ追加
                dataList.Add(dataArray.Clone())
                Array.Clear(dataArray, 0, dataArray.Length)
            End If
        Next
        If dataList.Count = 0 Then
            MsgBox("印刷対象者がいません。対象者にチェックを付けて下さい。", MsgBoxStyle.Exclamation)
            Return
        End If

        'エクセル準備
        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWorkBooks As Excel.Workbooks = objExcel.Workbooks
        Dim objWorkBook As Excel.Workbook = objWorkBooks.Open(TopForm.excelFilePass)
        Dim oSheet As Excel.Worksheet = objWorkBook.Worksheets("診断書２改")
        Dim xlShapes As Excel.Shapes = DirectCast(oSheet.Shapes, Excel.Shapes)
        objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
        objExcel.ScreenUpdating = False

        '受診日
        oSheet.Range("S3").Value = "受診日：令和　　　　年　　　　月　　　　日 (　　　　　　)"
        '事業所名
        oSheet.Range("W5").Value = ind
        'その他の検査項目1
        oSheet.Range("R10").Value = cb1.Text
        'その他の検査項目2
        oSheet.Range("R11").Value = cb2.Text
        'その他の検査項目3
        oSheet.Range("R12").Value = cb3.Text

        '必要枚数コピペ
        For i As Integer = 0 To dataList.Count - 2
            Dim xlPasteRange As Excel.Range = oSheet.Range("A" & (1 + 64 * (i + 1))) 'ペースト先
            oSheet.Rows("1:64").copy(xlPasteRange)
            oSheet.HPageBreaks.Add(oSheet.Range("A" & (1 + 64 * (i + 1)))) '改ページ
        Next

        'データ貼り付け
        Dim image3aPath As String = TopForm.health3aPath '胸部画像
        Dim image3bPath As String = TopForm.health3bPath '胃部画像
        For i As Integer = 0 To dataList.Count - 1
            oSheet.Range("H" & (5 + 64 * i), "O" & (9 + 64 * i)).Value = dataList(i)
            Dim cell As Excel.Range = DirectCast(oSheet.Cells(24 + 64 * i, "S"), Excel.Range)
            xlShapes.AddPicture(image3aPath, False, True, cell.Left, cell.Top, 70, 60)
            cell = DirectCast(oSheet.Cells(31 + 64 * i, "S"), Excel.Range)
            xlShapes.AddPicture(image3bPath, False, True, cell.Left, cell.Top, 60, 50)
        Next

        objExcel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
        objExcel.ScreenUpdating = True

        '変更保存確認ダイアログ非表示
        objExcel.DisplayAlerts = False

        '印刷
        If printState = True Then
            oSheet.PrintOut()
        Else
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