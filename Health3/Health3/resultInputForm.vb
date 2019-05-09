Public Class resultInputForm
    '事業所名
    Private ind As String
    '健保番号
    Private bango As String
    '氏名
    Private nam As String
    'カナ
    Private kana As String
    '性別（男：1、女：2）
    Private sex As String
    '生年月日(yyyy/MM/dd)
    Private birth As String
    '印刷状態(印刷:true, ﾌﾟﾚﾋﾞｭｰ:false)
    Private printState As Boolean

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="ind"></param>
    ''' <param name="bango"></param>
    ''' <param name="nam"></param>
    ''' <param name="kana"></param>
    ''' <param name="sex"></param>
    ''' <param name="birth"></param>
    ''' <param name="printState"></param>
    ''' <remarks></remarks>
    Public Sub New(ind As String, bango As String, nam As String, kana As String, sex As String, birth As String, printState As Boolean)
        InitializeComponent()

        Me.ind = ind
        Me.bango = bango
        Me.nam = nam
        Me.kana = kana
        Me.sex = sex
        Me.birth = birth
        Me.printState = printState
    End Sub

    ''' <summary>
    ''' loadイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub resultInputForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        YmdBox.canEnterKeyDown = True

        '受診者情報表示
        indBox.Text = ind
        bangoBox.Text = bango
        namBox.Text = nam
        sexBox.Text = If(sex = "1", "男", "女")
        birthBox.Text = birth & " 生"
        ageBox.Text = "   歳"

        '履歴リスト初期設定
        initHistoryListBox()

        'セルスタイル作成
        'initCellStyle()

        'データグリッドビュー初期設定
        initDgvInput()

        '初期フォーカス
        syuBox.Focus()
    End Sub

    ''' <summary>
    ''' 履歴リスト初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initHistoryListBox()
        historyListBox.Items.Clear()
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Health3)
        Dim sql As String = "select Ymd from KenD where Ind = '" & ind & "' and Kana = '" & kana & "' order by Ymd Desc"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic)
        While Not rs.EOF
            historyListBox.Items.Add(Util.checkDBNullValue(rs.Fields("Ymd").Value))
            rs.MoveNext()
        End While
        rs.Close()
        cn.Close()
    End Sub

    ''' <summary>
    ''' データグリッドビュー初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initDgvInput()
        Util.EnableDoubleBuffering(dgvInput)

        With dgvInput
            .AllowUserToAddRows = False '行追加禁止
            .AllowUserToResizeColumns = False '列の幅をユーザーが変更できないようにする
            .AllowUserToResizeRows = False '行の高さをユーザーが変更できないようにする
            .AllowUserToDeleteRows = False '行削除禁止
            .BorderStyle = BorderStyle.None
            .MultiSelect = False
            .RowHeadersVisible = False
            .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .ColumnHeadersVisible = True
            .ColumnHeadersHeight = 18
            .RowTemplate.Height = 16
            .BackgroundColor = Color.FromKnownColor(KnownColor.Control)
            .DefaultCellStyle.SelectionBackColor = Color.White
            .DefaultCellStyle.SelectionForeColor = Color.Black
            .ShowCellToolTips = False
            .EnableHeadersVisualStyles = False
            .ScrollBars = ScrollBars.Vertical
            .EditMode = DataGridViewEditMode.EditOnEnter
            .DefaultCellStyle.Font = New Font("ＭＳ Ｐゴシック", 9)
        End With

        '列追加、空の行追加
        Dim dt As New DataTable()
        dt.Columns.Add("Title", Type.GetType("System.String"))
        dt.Columns.Add("Kubun", Type.GetType("System.String"))
        dt.Columns.Add("Item", Type.GetType("System.String"))
        dt.Columns.Add("Unit", Type.GetType("System.String"))
        dt.Columns.Add("Result", Type.GetType("System.String"))
        For i As Integer = 0 To 79
            Dim row As DataRow = dt.NewRow()
            row(0) = ""
            row(1) = ""
            row(2) = ""
            row(3) = ""
            row(4) = ""
            dt.Rows.Add(row)
        Next

        '表示
        dgvInput.DataSource = dt

        '幅設定等
        With dgvInput
            With .Columns("Title")
                .HeaderText = ""
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 70
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            With .Columns("Kubun")
                .HeaderText = "指導区分"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 60
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            With .Columns("Item")
                .HeaderText = "項目"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Width = 280
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            With .Columns("Unit")
                .HeaderText = "単位"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 70
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            With .Columns("Result")
                .HeaderText = "検査結果"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Width = 340
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
        End With
    End Sub

End Class