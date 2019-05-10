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

    '1列目セルスタイル
    Private titleCellStyle As DataGridViewCellStyle
    '3列目セルスタイル
    Private itemCellStyle As DataGridViewCellStyle
    '3列目(青)セルスタイル
    Private itemBlueCellStyle As DataGridViewCellStyle
    '4列目セルスタイル
    Private unitCellStyle As DataGridViewCellStyle
    '4列目(青)セルスタイル
    Private unitBlueCellStyle As DataGridViewCellStyle
    'disableセルスタイル
    Private disableCellStyle As DataGridViewCellStyle

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
        initCellStyle()

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
    ''' セルスタイル作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub initCellStyle()
        '1列目
        titleCellStyle = New DataGridViewCellStyle()
        titleCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control)
        titleCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Control)
        titleCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '3列目(項目)
        itemCellStyle = New DataGridViewCellStyle()
        itemCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control)
        itemCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Control)
        itemCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

        '3列目(項目)(青)
        itemBlueCellStyle = New DataGridViewCellStyle()
        itemBlueCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control)
        itemBlueCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Control)
        itemBlueCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        itemBlueCellStyle.ForeColor = Color.Blue

        '4列目(単位)
        unitCellStyle = New DataGridViewCellStyle()
        unitCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control)
        unitCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Control)
        unitCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '4列目(単位)(青)
        unitBlueCellStyle = New DataGridViewCellStyle()
        unitBlueCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control)
        unitBlueCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Control)
        unitBlueCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        unitBlueCellStyle.ForeColor = Color.Blue

        'disableセルスタイル
        disableCellStyle = New DataGridViewCellStyle()
        disableCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control)
        disableCellStyle.SelectionBackColor = Color.FromKnownColor(KnownColor.Control)
        disableCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
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
        For i As Integer = 0 To 128
            Dim row As DataRow = dt.NewRow()
            row(0) = ""
            row(1) = ""
            row(2) = ""
            row(3) = ""
            row(4) = ""
            dt.Rows.Add(row)
        Next

        '1列目初期値
        dt.Rows(0).Item("Title") = "診察等"
        dt.Rows(17).Item("Title") = "血圧"
        dt.Rows(22).Item("Title") = "脂質"
        dt.Rows(26).Item("Title") = "肝機能等"
        dt.Rows(35).Item("Title") = "血糖"
        dt.Rows(40).Item("Title") = "尿酸"
        dt.Rows(41).Item("Title") = "尿一般・腎"
        dt.Rows(50).Item("Title") = "血液一般"
        dt.Rows(63).Item("Title") = "心電図"
        dt.Rows(65).Item("Title") = "胸部"
        dt.Rows(67).Item("Title") = "眼底"
        dt.Rows(73).Item("Title") = "肺機能"
        dt.Rows(76).Item("Title") = "胃部"
        dt.Rows(79).Item("Title") = "腹部"
        dt.Rows(80).Item("Title") = "大腸"
        dt.Rows(83).Item("Title") = "乳房"
        dt.Rows(85).Item("Title") = "子宮"
        dt.Rows(87).Item("Title") = "肝炎"
        dt.Rows(93).Item("Title") = "総合所見"
        dt.Rows(100).Item("Title") = "ﾒﾀﾎﾞ判定"
        dt.Rows(101).Item("Title") = "質問票"

        '3列目初期値
        dt.Rows(0).Item("Item") = "身長"
        dt.Rows(1).Item("Item") = "体重"
        dt.Rows(2).Item("Item") = "標準体重"
        dt.Rows(3).Item("Item") = "ＢＭＩ"
        dt.Rows(4).Item("Item") = "腹囲　　実測"
        dt.Rows(5).Item("Item") = "　　　　　内臓脂肪面積"
        dt.Rows(6).Item("Item") = "既往歴　　　 （特記事項）"
        dt.Rows(7).Item("Item") = "自覚症状　　（特記事項）"
        dt.Rows(8).Item("Item") = "胸部・腹部　所見"
        dt.Rows(9).Item("Item") = "視力　　右　裸眼"
        dt.Rows(10).Item("Item") = "　　　　　　　 矯正"
        dt.Rows(11).Item("Item") = "　　　　　左　裸眼"
        dt.Rows(12).Item("Item") = "　　　　　　　 矯正"
        dt.Rows(13).Item("Item") = "聴力　　右　1000Hz　1 所見なし　2 あり"
        dt.Rows(14).Item("Item") = "　　　　　　　 4000Hz　1 所見なし　2 あり"
        dt.Rows(15).Item("Item") = "　　　　　左　1000Hz　1 所見なし　2 あり"
        dt.Rows(16).Item("Item") = "　　　　　　　 4000Hz　1 所見なし　2 あり"
        dt.Rows(17).Item("Item") = "最高血圧（収縮期血圧）　１回目"
        dt.Rows(18).Item("Item") = "　　　　　　　　　　　　　　　　２回目"
        dt.Rows(19).Item("Item") = "最低血圧（拡張期血圧）　１回目"
        dt.Rows(20).Item("Item") = "　　　　　　　　　　　　　　　　２回目"
        dt.Rows(21).Item("Item") = "採血時間　　1 食後10時間未満　2 以上"
        dt.Rows(22).Item("Item") = "総ｺﾚｽﾃﾛｰﾙ"
        dt.Rows(23).Item("Item") = "中性脂肪　　（その他）"
        dt.Rows(24).Item("Item") = "ＨＤＬ－ｺﾚｽﾃﾛｰﾙ　　（その他）"
        dt.Rows(25).Item("Item") = "ＬＤＬ－ｺﾚｽﾃﾛｰﾙ　　（その他）"
        dt.Rows(26).Item("Item") = "ＧＯＴ（ＡＳＴ）　　（紫外吸光光度法）"
        dt.Rows(27).Item("Item") = "ＧＰＴ（ＡＬＴ）　　（紫外吸光光度法）"
        dt.Rows(28).Item("Item") = "γ－ＧＴＰ　　　　（その他）"
        dt.Rows(29).Item("Item") = "ＡＬＰ"
        dt.Rows(30).Item("Item") = "総蛋白"
        dt.Rows(31).Item("Item") = "ｱﾙﾌﾞﾐﾝ"
        dt.Rows(32).Item("Item") = "総ﾋﾞﾘﾙﾋﾞﾝ"
        dt.Rows(33).Item("Item") = "ＬＤＨ"
        dt.Rows(34).Item("Item") = "ｱﾐﾗｰｾﾞ"
        dt.Rows(35).Item("Item") = "空腹時血糖　　（その他）"
        dt.Rows(36).Item("Item") = "ﾍﾓｸﾞﾛﾋﾞﾝＡ１ｃ　　（ﾗﾃｯｸｽ凝集比濁法）"
        dt.Rows(37).Item("Item") = "尿糖　　試験紙法（目視法）"
        dt.Rows(38).Item("Item") = "　　　　　1(－) 2(±) 3(1＋) 4(2＋) 5(3＋)"
        dt.Rows(39).Item("Item") = "随時血糖"
        dt.Rows(40).Item("Item") = "尿酸"
        dt.Rows(41).Item("Item") = "尿蛋白　　試験紙法（目視法）"
        dt.Rows(42).Item("Item") = "　　　　　　 1(－) 2(±) 3(1＋) 4(2＋) 5(3＋)"
        dt.Rows(43).Item("Item") = "尿潜血　　1(－) 2(±) 3(1＋) 4(2＋) 5(3＋)"
        dt.Rows(44).Item("Item") = "血清ｸﾚｱﾁﾆﾝ"
        dt.Rows(45).Item("Item") = "尿沈渣　　赤血球"
        dt.Rows(46).Item("Item") = "　　　　　　 白血球"
        dt.Rows(47).Item("Item") = "　　　　　　 上皮細胞"
        dt.Rows(48).Item("Item") = "　　　　　　 円柱"
        dt.Rows(49).Item("Item") = "　　　　　　 その他"
        dt.Rows(50).Item("Item") = "ﾍﾏﾄｸﾘｯﾄ値"
        dt.Rows(51).Item("Item") = "血色素量（ﾍﾓｸﾞﾛﾋﾞﾝ値）"
        dt.Rows(52).Item("Item") = "赤血球数"
        dt.Rows(53).Item("Item") = "白血球数"
        dt.Rows(54).Item("Item") = "血小板数"
        dt.Rows(55).Item("Item") = "末梢血液像　　Baso"
        dt.Rows(56).Item("Item") = "　　　　　　　　　 Eosino"
        dt.Rows(57).Item("Item") = "　　　　　　　　　 Stab"
        dt.Rows(58).Item("Item") = "　　　　　　　　　 Seg（又はNeutro）"
        dt.Rows(59).Item("Item") = "　　　　　　　　　 Lympho"
        dt.Rows(60).Item("Item") = "　　　　　　　　　 Mono"
        dt.Rows(61).Item("Item") = "　　　　　　　　　 Other"
        dt.Rows(62).Item("Item") = "　　　　　　　　　 実施理由"
        dt.Rows(63).Item("Item") = "心電図　　所見"
        dt.Rows(64).Item("Item") = "　　　　　　 実施理由"
        dt.Rows(65).Item("Item") = "Ｘ線　　1 直接　2 間接"
        dt.Rows(66).Item("Item") = "　　　　　所見"
        dt.Rows(67).Item("Item") = "眼底　　Ｋ. Ｗ."
        dt.Rows(68).Item("Item") = "　　　　　Scheie　H"
        dt.Rows(69).Item("Item") = "　　　　　Scheie　S"
        dt.Rows(70).Item("Item") = "　　　　　SCOTT"
        dt.Rows(71).Item("Item") = "　　　　　所見"
        dt.Rows(72).Item("Item") = "　　　　　実施理由"
        dt.Rows(73).Item("Item") = "肺活量"
        dt.Rows(74).Item("Item") = "一秒量"
        dt.Rows(75).Item("Item") = "一秒率"
        dt.Rows(76).Item("Item") = "Ｘ線　　　　1 直接　2 間接"
        dt.Rows(77).Item("Item") = "　　　　　　　所見"
        dt.Rows(78).Item("Item") = "内視鏡　　所見"
        dt.Rows(79).Item("Item") = "腹部超音波　　所見"
        dt.Rows(80).Item("Item") = "免疫便潜血反応　　1日目　1(－) 3(＋)"
        dt.Rows(81).Item("Item") = "　　　　　　　　　　　　 2日目　1(－) 3(＋)"
        dt.Rows(82).Item("Item") = "直腸診　　所見"
        dt.Rows(83).Item("Item") = "触診等　　所見"
        dt.Rows(84).Item("Item") = "乳房ｴｯｸｽ線　　所見"
        dt.Rows(85).Item("Item") = "細胞診（ｽﾒｱ）　　　1 特に異常を認めず"
        dt.Rows(86).Item("Item") = "　　　　　　　　　　　 2 要精密検査"
        dt.Rows(87).Item("Item") = "ＨＢｓ抗原　　 1：－　2：±　3：＋"
        dt.Rows(88).Item("Item") = "ＨＣＶ抗体　　1 感染していない"
        dt.Rows(89).Item("Item") = "　　　　　　　　 2 感染している"
        dt.Rows(90).Item("Item") = "　　　　　　　　 3 要HCV核酸増幅検査"
        dt.Rows(91).Item("Item") = "ＨＣＶ拡散増幅検査　　1 感染していない"
        dt.Rows(92).Item("Item") = "　　　　　　　　　　　　　　 2 感染している"
        dt.Rows(93).Item("Item") = "指導区分　　　（1～6）"
        dt.Rows(94).Item("Item") = "注意事項・医師の判断"
        dt.Rows(95).Item("Item") = ""
        dt.Rows(96).Item("Item") = ""
        dt.Rows(97).Item("Item") = ""
        dt.Rows(98).Item("Item") = ""
        dt.Rows(99).Item("Item") = ""
        dt.Rows(100).Item("Item") = "ﾒﾀﾎﾞﾘｯｸｼﾝﾄﾞﾛｰﾑ判定"
        dt.Rows(101).Item("Item") = "服薬１（血圧）"
        dt.Rows(102).Item("Item") = "　　　　　薬剤名"
        dt.Rows(103).Item("Item") = "　　　　　服薬理由"
        dt.Rows(104).Item("Item") = "服薬２（血糖）"
        dt.Rows(105).Item("Item") = "　　　　　薬剤名"
        dt.Rows(106).Item("Item") = "　　　　　服薬理由"
        dt.Rows(107).Item("Item") = "服薬３（脂質）"
        dt.Rows(108).Item("Item") = "　　　　　薬剤名"
        dt.Rows(109).Item("Item") = "　　　　　服薬理由"
        dt.Rows(110).Item("Item") = "既往歴１（脳血管）"
        dt.Rows(111).Item("Item") = "既往歴２（心血管）"
        dt.Rows(112).Item("Item") = "既往歴３（腎不全・人工透析）"
        dt.Rows(113).Item("Item") = "貧血"
        dt.Rows(114).Item("Item") = "喫煙"
        dt.Rows(115).Item("Item") = "２０歳からの体重変化"
        dt.Rows(116).Item("Item") = "３０分以上の運動習慣"
        dt.Rows(117).Item("Item") = "歩行又は身体活動"
        dt.Rows(118).Item("Item") = "歩行速度"
        dt.Rows(119).Item("Item") = "１年間の体重変化"
        dt.Rows(120).Item("Item") = "食べ方１（早食い等）"
        dt.Rows(121).Item("Item") = "食べ方２（就寝前）"
        dt.Rows(122).Item("Item") = "食べ方３（夜食／間食）"
        dt.Rows(123).Item("Item") = "食習慣"
        dt.Rows(124).Item("Item") = "飲酒"
        dt.Rows(125).Item("Item") = "飲酒量"
        dt.Rows(126).Item("Item") = "睡眠"
        dt.Rows(127).Item("Item") = "生活習慣の改善"
        dt.Rows(128).Item("Item") = "保健指導の希望"

        '4列目初期値
        dt.Rows(0).Item("Unit") = "cm"
        dt.Rows(1).Item("Unit") = "kg"
        dt.Rows(2).Item("Unit") = "kg"
        dt.Rows(3).Item("Unit") = "kg/㎡"
        dt.Rows(4).Item("Unit") = "cm"
        dt.Rows(5).Item("Unit") = "c㎡"
        dt.Rows(17).Item("Unit") = "mm/Hg"
        dt.Rows(18).Item("Unit") = "mm/Hg"
        dt.Rows(19).Item("Unit") = "mm/Hg"
        dt.Rows(20).Item("Unit") = "mm/Hg"
        dt.Rows(22).Item("Unit") = "mg/dl"
        dt.Rows(23).Item("Unit") = "mg/dl"
        dt.Rows(24).Item("Unit") = "mg/dl"
        dt.Rows(25).Item("Unit") = "mg/dl"
        dt.Rows(26).Item("Unit") = "U/I"
        dt.Rows(27).Item("Unit") = "U/I"
        dt.Rows(28).Item("Unit") = "U/I"
        dt.Rows(29).Item("Unit") = "IU"
        dt.Rows(30).Item("Unit") = "g/dl"
        dt.Rows(31).Item("Unit") = "g/dl"
        dt.Rows(32).Item("Unit") = "mg/dl"
        dt.Rows(33).Item("Unit") = "IU"
        dt.Rows(34).Item("Unit") = "IU"
        dt.Rows(35).Item("Unit") = "mg/dl"
        dt.Rows(36).Item("Unit") = "%"
        dt.Rows(40).Item("Unit") = "mg/dl"
        dt.Rows(44).Item("Unit") = "mg/dl"
        dt.Rows(50).Item("Unit") = "%"
        dt.Rows(51).Item("Unit") = "g/dl"
        dt.Rows(52).Item("Unit") = "x10(4)/mm3"
        dt.Rows(53).Item("Unit") = "x10(2)/mm3"
        dt.Rows(54).Item("Unit") = "x10(4)/mm3"
        dt.Rows(55).Item("Unit") = "%"
        dt.Rows(56).Item("Unit") = "%"
        dt.Rows(57).Item("Unit") = "%"
        dt.Rows(58).Item("Unit") = "%"
        dt.Rows(59).Item("Unit") = "%"
        dt.Rows(60).Item("Unit") = "%"
        dt.Rows(61).Item("Unit") = "%"
        dt.Rows(73).Item("Unit") = "cc"
        dt.Rows(74).Item("Unit") = "l"
        dt.Rows(75).Item("Unit") = "%"
        dt.Rows(101).Item("Unit") = "1"
        dt.Rows(104).Item("Unit") = "2"
        dt.Rows(107).Item("Unit") = "3"
        dt.Rows(110).Item("Unit") = "4"
        dt.Rows(111).Item("Unit") = "5"
        dt.Rows(112).Item("Unit") = "6"
        dt.Rows(113).Item("Unit") = "7"
        dt.Rows(114).Item("Unit") = "8"
        dt.Rows(115).Item("Unit") = "9"
        dt.Rows(116).Item("Unit") = "10"
        dt.Rows(117).Item("Unit") = "11"
        dt.Rows(118).Item("Unit") = "12"
        dt.Rows(119).Item("Unit") = "13"
        dt.Rows(120).Item("Unit") = "14"
        dt.Rows(121).Item("Unit") = "15"
        dt.Rows(122).Item("Unit") = "16"
        dt.Rows(123).Item("Unit") = "17"
        dt.Rows(124).Item("Unit") = "18"
        dt.Rows(125).Item("Unit") = "19"
        dt.Rows(126).Item("Unit") = "20"
        dt.Rows(127).Item("Unit") = "21"
        dt.Rows(128).Item("Unit") = "22"

        '表示
        dgvInput.DataSource = dt

        '幅設定等
        With dgvInput
            With .Columns("Title")
                .HeaderText = ""
                .DefaultCellStyle = titleCellStyle
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
                For i As Integer = 0 To 128
                    If i <> 0 AndAlso i <> 17 AndAlso i <> 22 AndAlso i <> 26 AndAlso i <> 35 AndAlso i <> 40 AndAlso i <> 41 AndAlso i <> 50 AndAlso i <> 63 AndAlso i <> 65 AndAlso i <> 67 AndAlso i <> 73 AndAlso i <> 76 AndAlso i <> 78 AndAlso i <> 79 AndAlso i <> 80 AndAlso i <> 82 AndAlso i <> 83 AndAlso i <> 85 AndAlso i <> 87 AndAlso i <> 88 Then
                        dgvInput("Kubun", i).Style = disableCellStyle
                        dgvInput("Kubun", i).ReadOnly = True
                    End If
                Next
            End With
            Dim blueRowIndex() As Integer = {30, 31, 32, 33, 34, 45, 46, 47, 48, 49, 54, 55, 56, 57, 58, 59, 60, 61, 62, 67, 68, 69, 70, 71, 72, 73, 74, 75, 79}
            With .Columns("Item")
                .HeaderText = "項目"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle = itemCellStyle
                .Width = 280
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
                For Each rIndex As Integer In blueRowIndex
                    dgvInput("Item", rIndex).Style = itemBlueCellStyle
                Next
            End With
            With .Columns("Unit")
                .HeaderText = "単位"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle = unitCellStyle
                .Width = 70
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
                For Each rIndex As Integer In blueRowIndex
                    dgvInput("Unit", rIndex).Style = unitBlueCellStyle
                Next
            End With
            With .Columns("Result")
                .HeaderText = "検査結果"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Width = 340
                .SortMode = DataGridViewColumnSortMode.NotSortable
                For i As Integer = 0 To 128
                    If i = 2 OrElse i = 3 OrElse i = 38 OrElse i = 42 OrElse i = 86 OrElse i = 89 OrElse i = 90 OrElse i = 92 Then
                        dgvInput("Result", i).Style = disableCellStyle
                        dgvInput("Result", i).ReadOnly = True
                    End If
                Next
            End With
        End With
    End Sub

    ''' <summary>
    ''' 日付ボックスエンターキー押下イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub YmdBox_keyDownEnterOrDown(sender As Object, e As System.EventArgs) Handles YmdBox.keyDownEnterOrDown
        If YmdBox.getADStr() = "" Then
            Return
        End If

        '年齢算出、ラベルに表示
        Dim age As Integer = Util.calcAge(Util.convWarekiStrToADStr(birth), YmdBox.getADStr())
        ageBox.Text = age & " 歳"

        'dgvの１行目へ
        dgvInput.CurrentCell = dgvInput("Result", 0)
        dgvInput.Focus()
    End Sub

    Private Sub syuBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles syuBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            YmdBox.Focus()
        End If
    End Sub
End Class