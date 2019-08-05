Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

Public Class resultInputForm
    '事業所名
    Private ind As String
    '事業所　記号
    Private kigo As String
    '事業所　符号
    Private fugo As String
    '事業所　住所
    Private jyu As String
    '事業所　TEL
    Private tel As String
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

        '事業者情報取得
        loadIndInfo()

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
    ''' 事業所情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub loadIndInfo()
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Health3)
        Dim sql As String = "select Kigo4, Fugo6, Jyu, Tel from IndM where Ind = '" & ind & "'"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockOptimistic)
        While Not rs.EOF
            kigo = Util.checkDBNullValue(rs.Fields("Kigo4").Value)
            fugo = Util.checkDBNullValue(rs.Fields("Fugo6").Value)
            jyu = Util.checkDBNullValue(rs.Fields("Jyu").Value)
            tel = Util.checkDBNullValue(rs.Fields("Tel").Value)
            rs.MoveNext()
        End While
        rs.Close()
        cn.Close()
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

        '性別、生年月日設定等
        dgvInput.sex = sex
        dgvInput.eGFRBox = eGFRBox
        dgvInput.ageBox = ageBox
    End Sub

    ''' <summary>
    ''' 入力内容クリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clearInput()
        syuBox.Text = ""
        YmdBox.clearText()
        ageBox.Text = " 歳"
        eGFRBox.Text = ""
        For i As Integer = 0 To dgvInput.Rows.Count - 1
            dgvInput("Kubun", i).Value = ""
            dgvInput("Result", i).Value = ""
        Next
        syuBox.Focus()
    End Sub

    ''' <summary>
    ''' 結果データ表示
    ''' </summary>
    ''' <param name="ymd">日付(yyyy/MM/dd)</param>
    ''' <remarks></remarks>
    Private Sub displayKenData(ymd)
        'クリア
        clearInput()

        'データ取得、表示
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Health3)
        Dim sql As String = "select * from KenD where Ind = '" & ind & "' and Kana = '" & kana & "' and D6 = '" & birth & "' and Ymd = '" & ymd & "'"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If rs.RecordCount > 0 Then
            '年齢
            Dim age As Integer = Util.calcAge(Util.convWarekiStrToADStr(birth), Util.checkDBNullValue(rs.Fields("Ymd").Value))
            ageBox.Text = age & " 歳"
            '健診の種類
            syuBox.Text = Util.checkDBNullValue(rs.Fields("D2").Value)
            '健診日
            YmdBox.setADStr(Util.checkDBNullValue(rs.Fields("Ymd").Value))

            '診察等
            '指導区分
            dgvInput("Kubun", 0).Value = Util.checkDBNullValue(rs.Fields("D14").Value)
            '身長
            dgvInput("Result", 0).Value = Util.checkDBNullValue(rs.Fields("D17").Value)
            '体重
            dgvInput("Result", 1).Value = Util.checkDBNullValue(rs.Fields("D19").Value)
            '標準体重
            dgvInput("Result", 2).Value = Util.checkDBNullValue(rs.Fields("D21").Value)
            'ＢＭＩ
            dgvInput("Result", 3).Value = Util.checkDBNullValue(rs.Fields("D23").Value)
            '腹囲　実測
            dgvInput("Result", 4).Value = Util.checkDBNullValue(rs.Fields("D25").Value)
            '　　　内臓脂肪面積
            dgvInput("Result", 5).Value = Util.checkDBNullValue(rs.Fields("D31").Value)
            '既往歴
            dgvInput("Result", 6).Value = Util.checkDBNullValue(rs.Fields("D33").Value)
            '自覚症状
            dgvInput("Result", 7).Value = Util.checkDBNullValue(rs.Fields("D35").Value)
            '胸部・腹部　所見
            dgvInput("Result", 8).Value = Util.checkDBNullValue(rs.Fields("D38").Value)
            '視力　右　裸眼
            dgvInput("Result", 9).Value = Util.checkDBNullValue(rs.Fields("D40").Value)
            '　　　　　矯正
            dgvInput("Result", 10).Value = Util.checkDBNullValue(rs.Fields("D42").Value)
            '　　　左　裸眼
            dgvInput("Result", 11).Value = Util.checkDBNullValue(rs.Fields("D44").Value)
            '　　　　　矯正
            dgvInput("Result", 12).Value = Util.checkDBNullValue(rs.Fields("D46").Value)
            '聴力　右　1000Hz
            dgvInput("Result", 13).Value = Util.checkDBNullValue(rs.Fields("D47").Value)
            '　　　　　4000Hz
            dgvInput("Result", 14).Value = Util.checkDBNullValue(rs.Fields("D48").Value)
            '　　　左　1000Hz
            dgvInput("Result", 15).Value = Util.checkDBNullValue(rs.Fields("D49").Value)
            '　　　　　4000Hz
            dgvInput("Result", 16).Value = Util.checkDBNullValue(rs.Fields("D50").Value)

            '血圧
            '指導区分
            dgvInput("Kubun", 17).Value = Util.checkDBNullValue(rs.Fields("D52").Value)
            '最高血圧　1回目
            dgvInput("Result", 17).Value = Util.checkDBNullValue(rs.Fields("D54").Value)
            '　　　　　2回目
            dgvInput("Result", 18).Value = Util.checkDBNullValue(rs.Fields("D56").Value)
            '最低血圧　1回目
            dgvInput("Result", 19).Value = Util.checkDBNullValue(rs.Fields("D60").Value)
            '　　　　　2回目
            dgvInput("Result", 20).Value = Util.checkDBNullValue(rs.Fields("D62").Value)
            '採血時間
            dgvInput("Result", 21).Value = Util.checkDBNullValue(rs.Fields("D65").Value)

            '脂質
            '指導区分
            dgvInput("Kubun", 22).Value = Util.checkDBNullValue(rs.Fields("D67").Value)
            '総ｺﾚｽﾃﾛｰﾙ
            dgvInput("Result", 22).Value = Util.checkDBNullValue(rs.Fields("D69").Value)
            '中性脂肪
            dgvInput("Result", 23).Value = Util.checkDBNullValue(rs.Fields("D75").Value)
            'ＨＤＬ
            dgvInput("Result", 24).Value = Util.checkDBNullValue(rs.Fields("D81").Value)
            'ＬＤＬ
            dgvInput("Result", 25).Value = Util.checkDBNullValue(rs.Fields("D87").Value)

            '肝機能等
            '指導区分
            dgvInput("Kubun", 26).Value = Util.checkDBNullValue(rs.Fields("D89").Value)
            'ＧＯＴ
            dgvInput("Result", 26).Value = Util.checkDBNullValue(rs.Fields("D91").Value)
            'ＧＰＴ
            dgvInput("Result", 27).Value = Util.checkDBNullValue(rs.Fields("D95").Value)
            'γーＧＴＰ
            dgvInput("Result", 28).Value = Util.checkDBNullValue(rs.Fields("D101").Value)
            'ＡＬＰ
            dgvInput("Result", 29).Value = Util.checkDBNullValue(rs.Fields("D103").Value)
            '総蛋白
            dgvInput("Result", 30).Value = Util.checkDBNullValue(rs.Fields("D107").Value)
            'ｱﾙﾌﾞﾐﾝ
            dgvInput("Result", 31).Value = Util.checkDBNullValue(rs.Fields("D109").Value)
            '総ﾋﾞﾘﾙﾋﾞﾝ
            dgvInput("Result", 32).Value = Util.checkDBNullValue(rs.Fields("D111").Value)
            'ＬＤＨ
            dgvInput("Result", 33).Value = Util.checkDBNullValue(rs.Fields("D113").Value)
            'ｱﾐﾗｰｾﾞ
            dgvInput("Result", 34).Value = Util.checkDBNullValue(rs.Fields("D117").Value)

            '血糖
            '指導区分
            dgvInput("Kubun", 35).Value = Util.checkDBNullValue(rs.Fields("D121").Value)
            '空腹時血糖
            dgvInput("Result", 35).Value = Util.checkDBNullValue(rs.Fields("D129").Value)
            'ﾍﾓｸﾞﾛﾋﾞﾝＡ１ｃ
            dgvInput("Result", 36).Value = Util.checkDBNullValue(rs.Fields("D151").Value)
            '尿糖
            dgvInput("Result", 37).Value = Util.checkDBNullValue(rs.Fields("D161").Value)

            '尿酸
            '指導区分
            dgvInput("Kubun", 40).Value = Util.checkDBNullValue(rs.Fields("D163").Value)
            '尿酸
            dgvInput("Result", 40).Value = Util.checkDBNullValue(rs.Fields("D165").Value)

            '尿一般・腎
            '指導区分
            dgvInput("Kubun", 41).Value = Util.checkDBNullValue(rs.Fields("D167").Value)
            '尿蛋白
            dgvInput("Result", 41).Value = Util.checkDBNullValue(rs.Fields("D171").Value)
            '尿潜血
            dgvInput("Result", 43).Value = Util.checkDBNullValue(rs.Fields("D173").Value)
            '血清ｸﾚｱﾁﾆﾝ
            dgvInput("Result", 44).Value = Util.checkDBNullValue(rs.Fields("D180").Value)
            '尿沈渣　赤血球
            dgvInput("Result", 45).Value = Util.checkDBNullValue(rs.Fields("D174").Value)
            '　　　　白血球
            dgvInput("Result", 46).Value = Util.checkDBNullValue(rs.Fields("D175").Value)
            '　　　　上皮細胞
            dgvInput("Result", 47).Value = Util.checkDBNullValue(rs.Fields("D176").Value)
            '　　　　円柱
            dgvInput("Result", 48).Value = Util.checkDBNullValue(rs.Fields("D177").Value)
            '　　　　その他
            dgvInput("Result", 49).Value = Util.checkDBNullValue(rs.Fields("D178").Value)

            '血液一般
            '指導区分
            dgvInput("Kubun", 50).Value = Util.checkDBNullValue(rs.Fields("D182").Value)
            'ﾍﾏﾄｸﾘｯﾄ値
            dgvInput("Result", 50).Value = Util.checkDBNullValue(rs.Fields("D184").Value)
            '血色素量
            dgvInput("Result", 51).Value = Util.checkDBNullValue(rs.Fields("D186").Value)
            '赤血球数
            dgvInput("Result", 52).Value = Util.checkDBNullValue(rs.Fields("D188").Value)
            '白血球数
            dgvInput("Result", 53).Value = Util.checkDBNullValue(rs.Fields("D190").Value)
            '血小板数
            dgvInput("Result", 54).Value = Util.checkDBNullValue(rs.Fields("D192").Value)
            '末梢血液像　Baso
            dgvInput("Result", 55).Value = Util.checkDBNullValue(rs.Fields("D194").Value)
            '　　　　　　Eosino
            dgvInput("Result", 56).Value = Util.checkDBNullValue(rs.Fields("D196").Value)
            '　　　　　　Stab
            dgvInput("Result", 57).Value = Util.checkDBNullValue(rs.Fields("D198").Value)
            '　　　　　　Seg
            dgvInput("Result", 58).Value = Util.checkDBNullValue(rs.Fields("D200").Value)
            '　　　　　　Lympho
            dgvInput("Result", 59).Value = Util.checkDBNullValue(rs.Fields("D204").Value)
            '　　　　　　Mono
            dgvInput("Result", 60).Value = Util.checkDBNullValue(rs.Fields("D206").Value)
            '　　　　　　Other
            dgvInput("Result", 61).Value = Util.checkDBNullValue(rs.Fields("D208").Value)
            '　　　　　　実施理由
            dgvInput("Result", 62).Value = Util.checkDBNullValue(rs.Fields("D209").Value)

            '心電図
            '指導区分
            dgvInput("Kubun", 63).Value = Util.checkDBNullValue(rs.Fields("D211").Value)
            '所見
            dgvInput("Result", 63).Value = Util.checkDBNullValue(rs.Fields("D213").Value)
            '実施理由
            dgvInput("Result", 64).Value = Util.checkDBNullValue(rs.Fields("D214").Value)

            '胸部
            '指導区分
            dgvInput("Kubun", 65).Value = Util.checkDBNullValue(rs.Fields("D236").Value)
            '直接　間接
            dgvInput("Result", 65).Value = Util.checkDBNullValue(rs.Fields("D237").Value)
            '所見
            dgvInput("Result", 66).Value = Util.checkDBNullValue(rs.Fields("D238").Value)

            '眼底
            '指導区分
            dgvInput("Kubun", 67).Value = Util.checkDBNullValue(rs.Fields("D216").Value)
            'Ｋ．Ｗ．
            dgvInput("Result", 67).Value = Util.checkDBNullValue(rs.Fields("D218").Value)
            'Scheie H
            dgvInput("Result", 68).Value = Util.checkDBNullValue(rs.Fields("D220").Value)
            'Scheie S
            dgvInput("Result", 69).Value = Util.checkDBNullValue(rs.Fields("D222").Value)
            'SCOTT
            dgvInput("Result", 70).Value = Util.checkDBNullValue(rs.Fields("D224").Value)
            '所見
            dgvInput("Result", 71).Value = Util.checkDBNullValue(rs.Fields("D225").Value)
            '実施理由
            dgvInput("Result", 72).Value = Util.checkDBNullValue(rs.Fields("D226").Value)

            '肺機能
            '指導区分
            dgvInput("Kubun", 73).Value = Util.checkDBNullValue(rs.Fields("D228").Value)
            '肺活量
            dgvInput("Result", 73).Value = Util.checkDBNullValue(rs.Fields("D230").Value)
            '一秒量
            dgvInput("Result", 74).Value = Util.checkDBNullValue(rs.Fields("D232").Value)
            '一秒率
            dgvInput("Result", 75).Value = Util.checkDBNullValue(rs.Fields("D234").Value)

            '胃部
            '指導区分
            dgvInput("Kubun", 76).Value = Util.checkDBNullValue(rs.Fields("D240").Value)
            '直接　間接
            dgvInput("Result", 76).Value = Util.checkDBNullValue(rs.Fields("D241").Value)
            '所見
            dgvInput("Result", 77).Value = Util.checkDBNullValue(rs.Fields("D242").Value)
            '指導区分
            dgvInput("Kubun", 78).Value = Util.checkDBNullValue(rs.Fields("D243").Value)
            '内視鏡　所見
            dgvInput("Result", 78).Value = Util.checkDBNullValue(rs.Fields("D244").Value)

            '腹部
            '指導区分
            dgvInput("Kubun", 79).Value = Util.checkDBNullValue(rs.Fields("D246").Value)
            '腹部超音波　所見
            dgvInput("Result", 79).Value = Util.checkDBNullValue(rs.Fields("D247").Value)

            '大腸
            '指導区分
            dgvInput("Kubun", 80).Value = Util.checkDBNullValue(rs.Fields("D249").Value)
            '便潜血　1日目
            dgvInput("Result", 80).Value = Util.checkDBNullValue(rs.Fields("D251").Value)
            '便潜血　2日目
            dgvInput("Result", 81).Value = Util.checkDBNullValue(rs.Fields("D253").Value)
            '指導区分
            dgvInput("Kubun", 82).Value = Util.checkDBNullValue(rs.Fields("D254").Value)
            '直腸診　所見
            dgvInput("Result", 82).Value = Util.checkDBNullValue(rs.Fields("D255").Value)

            '乳房
            '指導区分
            dgvInput("Kubun", 83).Value = Util.checkDBNullValue(rs.Fields("D257").Value)
            '触診等　所見
            dgvInput("Result", 83).Value = Util.checkDBNullValue(rs.Fields("D258").Value)
            '乳房ｴｯｸｽ線　所見
            dgvInput("Result", 84).Value = Util.checkDBNullValue(rs.Fields("D260").Value)

            '子宮
            '指導区分
            dgvInput("Kubun", 85).Value = Util.checkDBNullValue(rs.Fields("D262").Value)
            '細胞診
            dgvInput("Result", 85).Value = Util.checkDBNullValue(rs.Fields("D263").Value)

            '肝炎
            '指導区分
            dgvInput("Kubun", 87).Value = Util.checkDBNullValue(rs.Fields("D265").Value)
            'ＨＢｓ抗原
            dgvInput("Result", 87).Value = Util.checkDBNullValue(rs.Fields("D267").Value)
            '指導区分
            dgvInput("Kubun", 88).Value = Util.checkDBNullValue(rs.Fields("D268").Value)
            'ＨＣＶ抗体
            dgvInput("Result", 88).Value = Util.checkDBNullValue(rs.Fields("D269").Value)
            'ＨＣＶ拡散増幅検査
            dgvInput("Result", 91).Value = Util.checkDBNullValue(rs.Fields("D270").Value)

            '総合所見
            '指導区分(1～6)
            dgvInput("Result", 93).Value = Util.checkDBNullValue(rs.Fields("D279").Value)

            '注意事項・医師の判断１
            dgvInput("Result", 94).Value = Util.checkDBNullValue(rs.Fields("D279a").Value)
            '　　　　　　　　　　２
            dgvInput("Result", 95).Value = Util.checkDBNullValue(rs.Fields("D279b").Value)
            '　　　　　　　　　　３
            dgvInput("Result", 96).Value = Util.checkDBNullValue(rs.Fields("D279c").Value)
            '　　　　　　　　　　４
            dgvInput("Result", 97).Value = Util.checkDBNullValue(rs.Fields("D279d").Value)
            '　　　　　　　　　　５
            dgvInput("Result", 98).Value = Util.checkDBNullValue(rs.Fields("D279e").Value)
            '　　　　　　　　　　６
            dgvInput("Result", 99).Value = Util.checkDBNullValue(rs.Fields("D279f").Value)

            'メタボ判定
            dgvInput("Result", 100).Value = Util.checkDBNullValue(rs.Fields("D281").Value)

            '質問票
            '服薬１（血圧）
            dgvInput("Result", 101).Value = Util.checkDBNullValue(rs.Fields("D285").Value)
            '　　　薬剤名
            dgvInput("Result", 102).Value = Util.checkDBNullValue(rs.Fields("D286").Value)
            '　　　服薬理由
            dgvInput("Result", 103).Value = Util.checkDBNullValue(rs.Fields("D287").Value)
            '服薬２（血糖）
            dgvInput("Result", 104).Value = Util.checkDBNullValue(rs.Fields("D288").Value)
            '　　　薬剤名
            dgvInput("Result", 105).Value = Util.checkDBNullValue(rs.Fields("D289").Value)
            '　　　服薬理由
            dgvInput("Result", 106).Value = Util.checkDBNullValue(rs.Fields("D290").Value)
            '服薬３（脂質）
            dgvInput("Result", 107).Value = Util.checkDBNullValue(rs.Fields("D291").Value)
            '　　　薬剤名
            dgvInput("Result", 108).Value = Util.checkDBNullValue(rs.Fields("D292").Value)
            '　　　服薬理由
            dgvInput("Result", 109).Value = Util.checkDBNullValue(rs.Fields("D293").Value)
            '既往歴１
            dgvInput("Result", 110).Value = Util.checkDBNullValue(rs.Fields("D294").Value)
            '既往歴２
            dgvInput("Result", 111).Value = Util.checkDBNullValue(rs.Fields("D295").Value)
            '既往歴３
            dgvInput("Result", 112).Value = Util.checkDBNullValue(rs.Fields("D296").Value)
            '貧血
            dgvInput("Result", 113).Value = Util.checkDBNullValue(rs.Fields("D297").Value)
            '喫煙
            dgvInput("Result", 114).Value = Util.checkDBNullValue(rs.Fields("D298").Value)
            '20歳からの体重変化
            dgvInput("Result", 115).Value = Util.checkDBNullValue(rs.Fields("D299").Value)
            '30分以上の運動習慣
            dgvInput("Result", 116).Value = Util.checkDBNullValue(rs.Fields("D300").Value)
            '歩行又は身体活動
            dgvInput("Result", 117).Value = Util.checkDBNullValue(rs.Fields("D301").Value)
            '歩行速度
            dgvInput("Result", 118).Value = Util.checkDBNullValue(rs.Fields("D302").Value)
            '1年間の体重変化
            dgvInput("Result", 119).Value = Util.checkDBNullValue(rs.Fields("D303").Value)
            '食べ方１
            dgvInput("Result", 120).Value = Util.checkDBNullValue(rs.Fields("D304").Value)
            '食べ方２
            dgvInput("Result", 121).Value = Util.checkDBNullValue(rs.Fields("D305").Value)
            '食べ方３
            dgvInput("Result", 122).Value = Util.checkDBNullValue(rs.Fields("D306").Value)
            '食習慣
            dgvInput("Result", 123).Value = Util.checkDBNullValue(rs.Fields("D307").Value)
            '飲酒
            dgvInput("Result", 124).Value = Util.checkDBNullValue(rs.Fields("D308").Value)
            '飲酒量
            dgvInput("Result", 125).Value = Util.checkDBNullValue(rs.Fields("D309").Value)
            '睡眠
            dgvInput("Result", 126).Value = Util.checkDBNullValue(rs.Fields("D310").Value)
            '生活習慣の改善
            dgvInput("Result", 127).Value = Util.checkDBNullValue(rs.Fields("D311").Value)
            '保健指導の希望
            dgvInput("Result", 128).Value = Util.checkDBNullValue(rs.Fields("D312").Value)

            'eGFR
            eGFRBox.Text = Util.checkDBNullValue(rs.Fields("D313").Value)

        End If
        rs.Close()
        cn.Close()

        'フォーカス
        YmdBox.Focus()
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
        ageBox.Text = age & "  歳"

        'dgvの１行目へ
        dgvInput.CurrentCell = dgvInput("Result", 0)
        dgvInput.Focus()
    End Sub

    Private Sub syuBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles syuBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            YmdBox.Focus()
        End If
    End Sub

    ''' <summary>
    ''' 履歴リストボックス値変更イベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub historyListBox_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles historyListBox.SelectedValueChanged
        Dim selectedYmd As String = historyListBox.Text
        If selectedYmd <> "" Then
            displayKenData(selectedYmd)
        End If
    End Sub

    ''' <summary>
    ''' 登録ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRegist_Click(sender As System.Object, e As System.EventArgs) Handles btnRegist.Click
        Dim d(313) As String
        Dim judgmentStr(5) As String
        '健診の種類
        d(2) = syuBox.Text
        If d(2) = "" OrElse Not (d(2) = "1" OrElse d(2) = "2") Then
            MsgBox("健診の種類を入力して下さい。", MsgBoxStyle.Exclamation)
            syuBox.Focus()
            Return
        End If
        '健診日
        Dim ymd As String = YmdBox.getADStr()
        If ymd = "" Then
            MsgBox("健診日を入力して下さい。", MsgBoxStyle.Exclamation)
            YmdBox.Focus()
            Return
        End If
        '指導区分チェック
        For Each row As DataGridViewRow In dgvInput.Rows
            Dim cell As DataGridViewCell = row.Cells("Kubun")
            Dim inputStr As String = Util.checkDBNullValue(cell.Value)
            If cell.ReadOnly = False AndAlso inputStr <> "" AndAlso Not System.Text.RegularExpressions.Regex.IsMatch(inputStr, "^[1-6]$") Then
                MsgBox("指導区分は数値(1～6)を入力して下さい。", MsgBoxStyle.Exclamation)
                dgvInput.CurrentCell = row.Cells("Kubun")
                dgvInput.BeginEdit(True)
                Return
            End If
        Next

        '
        d(3) = "1"
        '生年月日
        d(6) = birth
        '性別
        d(7) = sex
        '記号
        d(9) = kigo
        '符号
        d(10) = fugo
        '健保番号
        d(11) = bango

        '診察等
        '指導区分
        d(14) = Util.checkDBNullValue(dgvInput("Kubun", 0).Value)
        '身長
        d(17) = Util.checkDBNullValue(dgvInput("Result", 0).Value)
        '体重
        d(19) = Util.checkDBNullValue(dgvInput("Result", 1).Value)
        '標準体重
        d(21) = Util.checkDBNullValue(dgvInput("Result", 2).Value)
        'BMI
        d(23) = Util.checkDBNullValue(dgvInput("Result", 3).Value)
        '腹囲　実測
        d(25) = Util.checkDBNullValue(dgvInput("Result", 4).Value)
        '内臓脂肪面積
        d(31) = Util.checkDBNullValue(dgvInput("Result", 5).Value)
        '既往歴
        d(33) = Util.checkDBNullValue(dgvInput("Result", 6).Value)
        '自覚症状
        d(35) = Util.checkDBNullValue(dgvInput("Result", 7).Value)
        '胸部・腹部　所見
        d(38) = Util.checkDBNullValue(dgvInput("Result", 8).Value)
        '視力　右　裸眼
        d(40) = Util.checkDBNullValue(dgvInput("Result", 9).Value)
        '　　　　　矯正
        d(42) = Util.checkDBNullValue(dgvInput("Result", 10).Value)
        '　　　左　裸眼
        d(44) = Util.checkDBNullValue(dgvInput("Result", 11).Value)
        '　　　　　矯正
        d(46) = Util.checkDBNullValue(dgvInput("Result", 12).Value)
        '聴力　右　1000Hz
        d(47) = Util.checkDBNullValue(dgvInput("Result", 13).Value)
        '　　　　　4000Hz
        d(48) = Util.checkDBNullValue(dgvInput("Result", 14).Value)
        '　　　左　1000Hz
        d(49) = Util.checkDBNullValue(dgvInput("Result", 15).Value)
        '　　　　　4000Hz
        d(50) = Util.checkDBNullValue(dgvInput("Result", 16).Value)

        '血圧
        '指導区分
        d(52) = Util.checkDBNullValue(dgvInput("Kubun", 17).Value)
        '最高血圧　1回目
        d(54) = Util.checkDBNullValue(dgvInput("Result", 17).Value)
        '　　　　　2回目
        d(56) = Util.checkDBNullValue(dgvInput("Result", 18).Value)
        '最低血圧　1回目
        d(60) = Util.checkDBNullValue(dgvInput("Result", 19).Value)
        '　　　　　2回目
        d(62) = Util.checkDBNullValue(dgvInput("Result", 20).Value)
        '採血時間
        d(65) = Util.checkDBNullValue(dgvInput("Result", 21).Value)

        '脂質
        '指導区分
        d(67) = Util.checkDBNullValue(dgvInput("Kubun", 22).Value)
        '総ｺﾚｽﾃﾛｰﾙ
        d(69) = Util.checkDBNullValue(dgvInput("Result", 22).Value)
        '中性脂肪
        d(75) = Util.checkDBNullValue(dgvInput("Result", 23).Value)
        'ＨＤＬ
        d(81) = Util.checkDBNullValue(dgvInput("Result", 24).Value)
        'ＬＤＬ
        d(87) = Util.checkDBNullValue(dgvInput("Result", 25).Value)

        '肝機能等
        '指導区分
        d(89) = Util.checkDBNullValue(dgvInput("Kubun", 26).Value)
        'ＧＯＴ
        d(91) = Util.checkDBNullValue(dgvInput("Result", 26).Value)
        'ＧＰＴ
        d(95) = Util.checkDBNullValue(dgvInput("Result", 27).Value)
        'γーＧＴＰ
        d(101) = Util.checkDBNullValue(dgvInput("Result", 28).Value)
        'ＡＬＰ
        d(103) = Util.checkDBNullValue(dgvInput("Result", 29).Value)
        '総蛋白
        d(107) = Util.checkDBNullValue(dgvInput("Result", 30).Value)
        'ｱﾙﾌﾞﾐﾝ
        d(109) = Util.checkDBNullValue(dgvInput("Result", 31).Value)
        '総ﾋﾞﾘﾙﾋﾞﾝ
        d(111) = Util.checkDBNullValue(dgvInput("Result", 32).Value)
        'ＬＤＨ
        d(113) = Util.checkDBNullValue(dgvInput("Result", 33).Value)
        'ｱﾐﾗｰｾﾞ
        d(117) = Util.checkDBNullValue(dgvInput("Result", 34).Value)

        '血糖
        '指導区分
        d(121) = Util.checkDBNullValue(dgvInput("Kubun", 35).Value)
        '空腹時血糖
        d(129) = Util.checkDBNullValue(dgvInput("Result", 35).Value)
        '随時血糖
        d(137) = Util.checkDBNullValue(dgvInput("Result", 39).Value)
        'ﾍﾓｸﾞﾛﾋﾞﾝＡ１ｃ
        d(151) = Util.checkDBNullValue(dgvInput("Result", 36).Value)
        '尿糖
        d(161) = Util.checkDBNullValue(dgvInput("Result", 37).Value)
        
        '尿酸
        '指導区分
        d(163) = Util.checkDBNullValue(dgvInput("Kubun", 40).Value)
        '尿酸
        d(165) = Util.checkDBNullValue(dgvInput("Result", 40).Value)

        '尿一般・腎
        '指導区分
        d(167) = Util.checkDBNullValue(dgvInput("Kubun", 41).Value)
        '尿蛋白
        d(171) = Util.checkDBNullValue(dgvInput("Result", 41).Value)
        '尿潜血
        d(173) = Util.checkDBNullValue(dgvInput("Result", 43).Value)
        '血清ｸﾚｱﾁﾆﾝ
        d(180) = Util.checkDBNullValue(dgvInput("Result", 44).Value)
        '尿沈渣　赤血球
        d(174) = Util.checkDBNullValue(dgvInput("Result", 45).Value)
        '　　　　白血球
        d(175) = Util.checkDBNullValue(dgvInput("Result", 46).Value)
        '　　　　上皮細胞
        d(176) = Util.checkDBNullValue(dgvInput("Result", 47).Value)
        '　　　　円柱
        d(177) = Util.checkDBNullValue(dgvInput("Result", 48).Value)
        '　　　　その他
        d(178) = Util.checkDBNullValue(dgvInput("Result", 49).Value)

        '血液一般
        '指導区分
        d(182) = Util.checkDBNullValue(dgvInput("Kubun", 50).Value)
        'ﾍﾏﾄｸﾘｯﾄ値
        d(184) = Util.checkDBNullValue(dgvInput("Result", 50).Value)
        '血色素量
        d(186) = Util.checkDBNullValue(dgvInput("Result", 51).Value)
        '赤血球数
        d(188) = Util.checkDBNullValue(dgvInput("Result", 52).Value)
        '白血球数
        d(190) = Util.checkDBNullValue(dgvInput("Result", 53).Value)
        '血小板数
        d(192) = Util.checkDBNullValue(dgvInput("Result", 54).Value)
        '末梢血液像　Baso
        d(194) = Util.checkDBNullValue(dgvInput("Result", 55).Value)
        '　　　　　　Eosino
        d(196) = Util.checkDBNullValue(dgvInput("Result", 56).Value)
        '　　　　　　Stab
        d(198) = Util.checkDBNullValue(dgvInput("Result", 57).Value)
        '　　　　　　Seg
        d(200) = Util.checkDBNullValue(dgvInput("Result", 58).Value)
        '　　　　　　Lympho
        d(204) = Util.checkDBNullValue(dgvInput("Result", 59).Value)
        '　　　　　　Mono
        d(206) = Util.checkDBNullValue(dgvInput("Result", 60).Value)
        '　　　　　　Other
        d(208) = Util.checkDBNullValue(dgvInput("Result", 61).Value)
        '　　　　　　実施理由
        d(209) = Util.checkDBNullValue(dgvInput("Result", 62).Value)

        '心電図
        '指導区分
        d(211) = Util.checkDBNullValue(dgvInput("Kubun", 63).Value)
        '所見
        d(213) = Util.checkDBNullValue(dgvInput("Result", 63).Value)
        '実施理由
        d(214) = Util.checkDBNullValue(dgvInput("Result", 64).Value)

        '胸部
        '指導区分
        d(236) = Util.checkDBNullValue(dgvInput("Kubun", 65).Value)
        '直接　間接
        d(237) = Util.checkDBNullValue(dgvInput("Result", 65).Value)
        '所見
        d(238) = Util.checkDBNullValue(dgvInput("Result", 66).Value)

        '眼底
        '指導区分
        d(216) = Util.checkDBNullValue(dgvInput("Kubun", 67).Value)
        'Ｋ．Ｗ．
        d(218) = Util.checkDBNullValue(dgvInput("Result", 67).Value)
        'Scheie H
        d(220) = Util.checkDBNullValue(dgvInput("Result", 68).Value)
        'Scheie S
        d(222) = Util.checkDBNullValue(dgvInput("Result", 69).Value)
        'SCOTT
        d(224) = Util.checkDBNullValue(dgvInput("Result", 70).Value)
        '所見
        d(225) = Util.checkDBNullValue(dgvInput("Result", 71).Value)
        '実施理由
        d(226) = Util.checkDBNullValue(dgvInput("Result", 72).Value)

        '肺機能
        '指導区分
        d(228) = Util.checkDBNullValue(dgvInput("Kubun", 73).Value)
        '肺活量
        d(230) = Util.checkDBNullValue(dgvInput("Result", 73).Value)
        '一秒量
        d(232) = Util.checkDBNullValue(dgvInput("Result", 74).Value)
        '一秒率
        d(234) = Util.checkDBNullValue(dgvInput("Result", 75).Value)

        '胃部
        '指導区分
        d(240) = Util.checkDBNullValue(dgvInput("Kubun", 76).Value)
        '直接　間接
        d(241) = Util.checkDBNullValue(dgvInput("Result", 76).Value)
        '所見
        d(242) = Util.checkDBNullValue(dgvInput("Result", 77).Value)
        '指導区分
        d(243) = Util.checkDBNullValue(dgvInput("Kubun", 78).Value)
        '内視鏡　所見
        d(244) = Util.checkDBNullValue(dgvInput("Result", 78).Value)

        '腹部
        '指導区分
        d(246) = Util.checkDBNullValue(dgvInput("Kubun", 79).Value)
        '腹部超音波　所見
        d(247) = Util.checkDBNullValue(dgvInput("Result", 79).Value)

        '大腸
        '指導区分
        d(249) = Util.checkDBNullValue(dgvInput("Kubun", 80).Value)
        '便潜血　1日目
        d(251) = Util.checkDBNullValue(dgvInput("Result", 80).Value)
        '便潜血　2日目
        d(253) = Util.checkDBNullValue(dgvInput("Result", 81).Value)
        '指導区分
        d(254) = Util.checkDBNullValue(dgvInput("Kubun", 82).Value)
        '直腸診　所見
        d(255) = Util.checkDBNullValue(dgvInput("Result", 82).Value)

        '乳房
        '指導区分
        d(257) = Util.checkDBNullValue(dgvInput("Kubun", 83).Value)
        '触診等　所見
        d(258) = Util.checkDBNullValue(dgvInput("Result", 83).Value)
        '乳房ｴｯｸｽ線　所見
        d(260) = Util.checkDBNullValue(dgvInput("Result", 84).Value)

        '子宮
        '指導区分
        d(262) = Util.checkDBNullValue(dgvInput("Kubun", 85).Value)
        '細胞診
        d(263) = Util.checkDBNullValue(dgvInput("Result", 85).Value)

        '肝炎
        '指導区分
        d(265) = Util.checkDBNullValue(dgvInput("Kubun", 87).Value)
        'ＨＢｓ抗原
        d(267) = Util.checkDBNullValue(dgvInput("Result", 87).Value)
        '指導区分
        d(268) = Util.checkDBNullValue(dgvInput("Kubun", 88).Value)
        'ＨＣＶ抗体
        d(269) = Util.checkDBNullValue(dgvInput("Result", 88).Value)
        'ＨＣＶ拡散増幅検査
        d(270) = Util.checkDBNullValue(dgvInput("Result", 91).Value)

        '総合所見
        '指導区分(1～6)
        d(279) = Util.checkDBNullValue(dgvInput("Result", 93).Value)

        '注意事項・医師の判断１
        judgmentStr(0) = Util.checkDBNullValue(dgvInput("Result", 94).Value)
        '　　　　　　　　　　２
        judgmentStr(1) = Util.checkDBNullValue(dgvInput("Result", 95).Value)
        '　　　　　　　　　　３
        judgmentStr(2) = Util.checkDBNullValue(dgvInput("Result", 96).Value)
        '　　　　　　　　　　４
        judgmentStr(3) = Util.checkDBNullValue(dgvInput("Result", 97).Value)
        '　　　　　　　　　　５
        judgmentStr(4) = Util.checkDBNullValue(dgvInput("Result", 98).Value)
        '　　　　　　　　　　６
        judgmentStr(5) = Util.checkDBNullValue(dgvInput("Result", 99).Value)

        'メタボ判定
        Dim metabo() As String = {"メタボリックシンドロームです。", "予備群に該当します。", "メタボリックシンドロームではありません。", "判定ができません。"}
        d(281) = Util.checkDBNullValue(dgvInput("Result", 100).Value)
        If d(281) = "1" Then
            d(283) = metabo(0)
        ElseIf d(281) = "2" Then
            d(283) = metabo(1)
        ElseIf d(281) = "3" Then
            d(283) = metabo(2)
        Else
            d(283) = metabo(3)
        End If

        '質問票
        '服薬１（血圧）
        d(285) = Util.checkDBNullValue(dgvInput("Result", 101).Value)
        '　　　薬剤名
        d(286) = Util.checkDBNullValue(dgvInput("Result", 102).Value)
        '　　　服薬理由
        d(287) = Util.checkDBNullValue(dgvInput("Result", 103).Value)
        '服薬２（血糖）
        d(288) = Util.checkDBNullValue(dgvInput("Result", 104).Value)
        '　　　薬剤名
        d(289) = Util.checkDBNullValue(dgvInput("Result", 105).Value)
        '　　　服薬理由
        d(290) = Util.checkDBNullValue(dgvInput("Result", 106).Value)
        '服薬３（脂質）
        d(291) = Util.checkDBNullValue(dgvInput("Result", 107).Value)
        '　　　薬剤名
        d(292) = Util.checkDBNullValue(dgvInput("Result", 108).Value)
        '　　　服薬理由
        d(293) = Util.checkDBNullValue(dgvInput("Result", 109).Value)
        '既往歴１
        d(294) = Util.checkDBNullValue(dgvInput("Result", 110).Value)
        '既往歴２
        d(295) = Util.checkDBNullValue(dgvInput("Result", 111).Value)
        '既往歴３
        d(296) = Util.checkDBNullValue(dgvInput("Result", 112).Value)
        '貧血
        d(297) = Util.checkDBNullValue(dgvInput("Result", 113).Value)
        '喫煙
        d(298) = Util.checkDBNullValue(dgvInput("Result", 114).Value)
        '20歳からの体重変化
        d(299) = Util.checkDBNullValue(dgvInput("Result", 115).Value)
        '30分以上の運動習慣
        d(300) = Util.checkDBNullValue(dgvInput("Result", 116).Value)
        '歩行又は身体活動
        d(301) = Util.checkDBNullValue(dgvInput("Result", 117).Value)
        '歩行速度
        d(302) = Util.checkDBNullValue(dgvInput("Result", 118).Value)
        '1年間の体重変化
        d(303) = Util.checkDBNullValue(dgvInput("Result", 119).Value)
        '食べ方１
        d(304) = Util.checkDBNullValue(dgvInput("Result", 120).Value)
        '食べ方２
        d(305) = Util.checkDBNullValue(dgvInput("Result", 121).Value)
        '食べ方３
        d(306) = Util.checkDBNullValue(dgvInput("Result", 122).Value)
        '食習慣
        d(307) = Util.checkDBNullValue(dgvInput("Result", 123).Value)
        '飲酒
        d(308) = Util.checkDBNullValue(dgvInput("Result", 124).Value)
        '飲酒量
        d(309) = Util.checkDBNullValue(dgvInput("Result", 125).Value)
        '睡眠
        d(310) = Util.checkDBNullValue(dgvInput("Result", 126).Value)
        '生活習慣の改善
        d(311) = Util.checkDBNullValue(dgvInput("Result", 127).Value)
        '保健指導の希望
        d(312) = Util.checkDBNullValue(dgvInput("Result", 128).Value)

        'eGFR
        Dim egfr As String = eGFRBox.Text
        If Not System.Text.RegularExpressions.Regex.IsMatch(egfr, "^\d+(\.\d{1})?$") Then
            egfr = ""
        End If
        d(313) = egfr

        '登録
        Dim registNumber() As Integer = {2, 3, 6, 7, 9, 10, 11, 14, 17, 19, 21, 23, 25, 31, 33, 35, 38, 40, 42, 44, 46, 47, 48, 49, 50, 52, 54, 56, 60, 62, 65, 67, 69, 75, 81, 87, 89, 91, 95, 101, 103, 107, 109, 111, 113, 117, 121, 129, 137, 151, 161, 163, 165, 167, 171, 173, 174, 175, 176, 177, 178, 180, 182, 184, 186, 188, 190, 192, 194, 196, 198, 200, 204, 206, 208, 209, 211, 213, 214, 216, 218, 220, 222, 224, 225, 226, 228, 230, 232, 234, 236, 237, 238, 240, 241, 242, 243, 244, 246, 247, 249, 251, 253, 254, 255, 257, 258, 260, 262, 263, 265, 267, 268, 269, 270, 279, 281, 283, 285, 286, 287, 288, 289, 290, 291, 292, 293, 294, 295, 296, 297, 298, 299, 300, 301, 302, 303, 304, 305, 306, 307, 308, 309, 310, 311, 312, 313}
        Dim emptyRegistNumber() As Integer = {8, 15, 27, 29, 37, 58, 64, 71, 73, 77, 79, 83, 85, 93, 97, 99, 105, 115, 119, 123, 125, 127, 131, 133, 135, 139, 141, 143, 145, 147, 149, 153, 155, 157, 159, 169, 202, 282, 284}
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Health3)
        Dim sql As String = "select * from KenD where Ind = '" & ind & "' and Kana = '" & kana & "' and D6 = '" & birth & "' and Ymd = '" & ymd & "'"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If rs.RecordCount <= 0 Then
            '新規登録
            rs.AddNew()
        End If
        For Each num As Integer In registNumber
            rs.Fields("D" & num).Value = d(num)
        Next
        For Each num As Integer In emptyRegistNumber
            rs.Fields("D" & num).Value = ""
        Next
        rs.Fields("Ind").Value = ind
        rs.Fields("Kana").Value = kana
        rs.Fields("Ymd").Value = ymd
        rs.Fields("D279a").Value = judgmentStr(0)
        rs.Fields("D279b").Value = judgmentStr(1)
        rs.Fields("D279c").Value = judgmentStr(2)
        rs.Fields("D279d").Value = judgmentStr(3)
        rs.Fields("D279e").Value = judgmentStr(4)
        rs.Fields("D279f").Value = judgmentStr(5)

        rs.Update()
        rs.Close()
        cn.Close()

        '入力内容クリア
        clearInput()

        '履歴リスト更新
        initHistoryListBox()
    End Sub

    ''' <summary>
    ''' 削除ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        '健診日
        Dim ymd As String = YmdBox.getADStr()

        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Health3)
        Dim sql As String = "select * from KenD where Ind = '" & ind & "' and Kana = '" & kana & "' and D6 = '" & birth & "' and Ymd = '" & ymd & "'"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If rs.RecordCount <= 0 Then
            MsgBox("登録されていません。", MsgBoxStyle.Exclamation)
            rs.Close()
            cn.Close()
            Return
        End If
        '削除
        Dim result As DialogResult = MessageBox.Show("削除してよろしいですか？", "削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = Windows.Forms.DialogResult.Yes Then
            rs.Delete()
            rs.Update()
            rs.Close()
            cn.Close()

            clearInput()
            initHistoryListBox()
        Else
            rs.Close()
            cn.Close()
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
    End Sub

    ''' <summary>
    ''' 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        '健診日
        Dim ymd As String = YmdBox.getADStr()

        'データ取得
        Dim cn As New ADODB.Connection()
        cn.Open(TopForm.DB_Health3)
        Dim sql As String = "select * from KenD where Ind = '" & ind & "' and Kana = '" & kana & "' and D6 = '" & birth & "' and Ymd = '" & ymd & "'"
        Dim rs As New ADODB.Recordset
        rs.Open(sql, cn, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)
        If rs.RecordCount <= 0 Then
            MsgBox("データがある日付を選択して下さい。", MsgBoxStyle.Exclamation)
            rs.Close()
            cn.Close()
            Return
        End If

        '所見等の部分の入力値有、無の場合のフォント名
        Const FONT_NAME_INPUT As String = "ＭＳ Ｐゴシック"

        '尿糖、蛋白、潜血用
        Dim numDic As New Dictionary(Of String, String) From {{"1", "(－)"}, {"2", "(±)"}, {"3", "(1＋)"}, {"4", "(2＋)"}, {"5", "(3＋)"}}

        'エクセル準備
        Dim objExcel As Excel.Application = CreateObject("Excel.Application")
        Dim objWorkBooks As Excel.Workbooks = objExcel.Workbooks
        Dim objWorkBook As Excel.Workbook = objWorkBooks.Open(TopForm.excelFilePass)
        Dim oSheet As Excel.Worksheet
        objExcel.Calculation = Excel.XlCalculation.xlCalculationManual
        objExcel.ScreenUpdating = False

        '1枚目
        oSheet = objWorkBook.Worksheets("本人１改")
        '健診の種類
        Dim d2 As String = Util.checkDBNullValue(rs.Fields("D2").Value)
        If d2 = "1" Then
            oSheet.Range("F2").Value = "①．一般健診"
            oSheet.Range("F2").Font.Name = FONT_NAME_INPUT
            oSheet.Range("F3").Value = " 2．一般健診"
        ElseIf d2 = "2" Then
            oSheet.Range("F2").Value = " 1．一般健診"
            oSheet.Range("F3").Value = "②．一般健診"
            oSheet.Range("F3").Font.Name = FONT_NAME_INPUT
            oSheet.Range("F4").Font.Name = FONT_NAME_INPUT
        End If
        'ﾌﾘｶﾞﾅ
        oSheet.Range("I6").Value = kana
        '氏名
        oSheet.Range("I7").Value = nam
        '生年月日
        oSheet.Range("Y6").Value = birth.Split("/")(0) & " 年 " & birth.Split("/")(1) & " 月 " & birth.Split("/")(2) & " 日生"
        '性別
        If sex = "1" Then
            oSheet.Range("W8").Value = "①．男"
            oSheet.Range("W8").Font.Name = FONT_NAME_INPUT
            oSheet.Range("Z8").Value = "2．女"
        ElseIf sex = "2" Then
            oSheet.Range("W8").Value = "1．男"
            oSheet.Range("Z8").Value = "②．女"
            oSheet.Range("Z8").Font.Name = FONT_NAME_INPUT
        End If
        '年齢
        Dim age As Integer = Util.calcAge(Util.convWarekiStrToADStr(birth), ymd)
        oSheet.Range("AE8").Value = age
        '健康保険の記号
        oSheet.Range("AM6").Value = kigo & fugo
        '健康保険の番号
        oSheet.Range("AM8").Value = bango
        '事業所名
        oSheet.Range("AV7").Value = ind
        '事業所所在地
        oSheet.Range("BJ7").Value = jyu
        'TEL
        oSheet.Range("BM9").Value = If(tel = "", "", tel & ")")
        '健診日
        Dim wareki As String = Util.convADStrToWarekiStr(ymd)
        Dim kanji As String = Util.getKanji(wareki)
        '年号
        oSheet.Range("BX8").Value = kanji
        '日付
        oSheet.Range("BZ8").Value = wareki.Substring(1, 2) & " 年 " & wareki.Split("/")(1) & " 月 " & wareki.Split("/")(2) & " 日"

        '診察等
        '指導区分
        oSheet.Range("C30").Value = Util.checkDBNullValue(rs.Fields("D14").Value)
        '身長
        oSheet.Range("O13").Value = Util.checkDBNullValue(rs.Fields("D17").Value)
        '体重
        oSheet.Range("W13").Value = Util.checkDBNullValue(rs.Fields("D19").Value)
        '標準体重
        oSheet.Range("O14").Value = Util.checkDBNullValue(rs.Fields("D21").Value)
        'BMI
        oSheet.Range("W14").Value = Util.checkDBNullValue(rs.Fields("D23").Value)
        '腹囲　実測
        oSheet.Range("V15").Value = Util.checkDBNullValue(rs.Fields("D25").Value)
        '　　　内臓脂肪面積
        oSheet.Range("V18").Value = Util.checkDBNullValue(rs.Fields("D31").Value)
        '既往歴
        Dim d33 As String = Util.checkDBNullValue(rs.Fields("D33").Value)
        If d33 = "" Then
            oSheet.Range("M19").Value = "①．特記事項なし"
            oSheet.Range("M19").Font.Name = FONT_NAME_INPUT
            oSheet.Range("M20").Value = " 2．特記事項あり"
        Else
            oSheet.Range("M19").Value = " 1．特記事項なし"
            oSheet.Range("M20").Value = "②．特記事項あり"
            oSheet.Range("M20").Font.Name = FONT_NAME_INPUT
            oSheet.Range("Q21").Value = d33
        End If
        '服薬歴
        '血圧
        Dim d285 As String = Util.checkDBNullValue(rs.Fields("D285").Value)
        If d285 = "1" Then
            oSheet.Range("M23").Value = "①．服薬あり"
            oSheet.Range("M23").Font.Name = FONT_NAME_INPUT
            oSheet.Range("M24").Value = " 2．服薬なし"
            '薬剤名
            oSheet.Range("V23").Value = Util.checkDBNullValue(rs.Fields("D286").Value)
            '服薬理由
            oSheet.Range("V24").Value = Util.checkDBNullValue(rs.Fields("D287").Value)
        ElseIf d285 = "2" Then
            oSheet.Range("M23").Value = " 1．服薬あり"
            oSheet.Range("M24").Value = "②．服薬なし"
            oSheet.Range("M24").Font.Name = FONT_NAME_INPUT
        Else
            oSheet.Range("M23").Value = " 1．服薬あり"
            oSheet.Range("M24").Value = " 2．服薬なし"
        End If
        '血糖
        Dim d288 As String = Util.checkDBNullValue(rs.Fields("D288").Value)
        If d288 = "1" Then
            oSheet.Range("M25").Value = "①．服薬あり"
            oSheet.Range("M25").Font.Name = FONT_NAME_INPUT
            oSheet.Range("M26").Value = " 2．服薬なし"
            '薬剤名
            oSheet.Range("V25").Value = Util.checkDBNullValue(rs.Fields("D289").Value)
            '服薬理由
            oSheet.Range("V26").Value = Util.checkDBNullValue(rs.Fields("D290").Value)
        ElseIf d288 = "2" Then
            oSheet.Range("M25").Value = " 1．服薬あり"
            oSheet.Range("M26").Value = "②．服薬なし"
            oSheet.Range("M26").Font.Name = FONT_NAME_INPUT
        Else
            oSheet.Range("M25").Value = " 1．服薬あり"
            oSheet.Range("M26").Value = " 2．服薬なし"
        End If
        '脂質
        Dim d291 As String = Util.checkDBNullValue(rs.Fields("D291").Value)
        If d291 = "1" Then
            oSheet.Range("M27").Value = "①．服薬あり"
            oSheet.Range("M27").Font.Name = FONT_NAME_INPUT
            oSheet.Range("M28").Value = " 2．服薬なし"
            '薬剤名
            oSheet.Range("V27").Value = Util.checkDBNullValue(rs.Fields("D292").Value)
            '服薬理由
            oSheet.Range("V28").Value = Util.checkDBNullValue(rs.Fields("D293").Value)
        ElseIf d291 = "2" Then
            oSheet.Range("M27").Value = " 1．服薬あり"
            oSheet.Range("M28").Value = "②．服薬なし"
            oSheet.Range("M28").Font.Name = FONT_NAME_INPUT
        Else
            oSheet.Range("M27").Value = " 1．服薬あり"
            oSheet.Range("M28").Value = " 2．服薬なし"
        End If
        '喫煙歴
        Dim d298 As String = Util.checkDBNullValue(rs.Fields("D298").Value)
        If d298 = "1" Then
            oSheet.Range("M29").Value = "①．喫煙歴あり"
            oSheet.Range("M29").Font.Name = FONT_NAME_INPUT
            oSheet.Range("M30").Value = " 2．喫煙歴なし"
        ElseIf d298 = "2" Then
            oSheet.Range("M29").Value = " 1．喫煙歴あり"
            oSheet.Range("M30").Value = "②．喫煙歴なし"
            oSheet.Range("M30").Font.Name = FONT_NAME_INPUT
        Else
            oSheet.Range("M29").Value = " 1．喫煙歴あり"
            oSheet.Range("M30").Value = " 2．喫煙歴なし"
        End If
        '自覚症状
        Dim d35 As String = Util.checkDBNullValue(rs.Fields("D35").Value)
        If d35 = "" Then
            oSheet.Range("M31").Value = "①．特記事項なし"
            oSheet.Range("M31").Font.Name = FONT_NAME_INPUT
            oSheet.Range("M32").Value = " 2．特記事項あり"
        Else
            oSheet.Range("M31").Value = " 1．特記事項なし"
            oSheet.Range("M32").Value = "②．特記事項あり"
            oSheet.Range("M32").Font.Name = FONT_NAME_INPUT
            oSheet.Range("Q33").Value = d35
        End If
        '胸部・腹部　所見
        oSheet.Range("Q40").Value = Util.checkDBNullValue(rs.Fields("D38").Value)
        '視力
        '右　裸眼
        oSheet.Range("P43").Value = Util.checkDBNullValue(rs.Fields("D40").Value)
        '右　矯正
        oSheet.Range("X43").Value = Util.checkDBNullValue(rs.Fields("D42").Value)
        '左　裸眼
        oSheet.Range("P44").Value = Util.checkDBNullValue(rs.Fields("D44").Value)
        '左　矯正
        oSheet.Range("X44").Value = Util.checkDBNullValue(rs.Fields("D46").Value)
        '聴力
        '右　1000Hz
        Dim d47 As String = Util.checkDBNullValue(rs.Fields("D47").Value)
        If d47 = "1" Then
            oSheet.Range("P45").Value = "①．所見なし"
            oSheet.Range("P45").Font.Name = FONT_NAME_INPUT
            oSheet.Range("P46").Value = " 2．所見あり"
        ElseIf d47 = "2" Then
            oSheet.Range("P45").Value = " 1．所見なし"
            oSheet.Range("P46").Value = "②．所見あり"
            oSheet.Range("P46").Font.Name = FONT_NAME_INPUT
        Else
            oSheet.Range("P45").Value = " 1．所見なし"
            oSheet.Range("P46").Value = " 2．所見あり"
        End If
        '右　4000Hz
        Dim d48 As String = Util.checkDBNullValue(rs.Fields("D48").Value)
        If d48 = "1" Then
            oSheet.Range("X45").Value = "①．所見なし"
            oSheet.Range("X45").Font.Name = FONT_NAME_INPUT
            oSheet.Range("X46").Value = " 2．所見あり"
        ElseIf d48 = "2" Then
            oSheet.Range("X45").Value = " 1．所見なし"
            oSheet.Range("X46").Value = "②．所見あり"
            oSheet.Range("X46").Font.Name = FONT_NAME_INPUT
        Else
            oSheet.Range("X45").Value = " 1．所見なし"
            oSheet.Range("X46").Value = " 2．所見あり"
        End If
        '左　1000Hz
        Dim d49 As String = Util.checkDBNullValue(rs.Fields("D49").Value)
        If d49 = "1" Then
            oSheet.Range("P47").Value = "①．所見なし"
            oSheet.Range("P47").Font.Name = FONT_NAME_INPUT
            oSheet.Range("P48").Value = " 2．所見あり"
        ElseIf d49 = "2" Then
            oSheet.Range("P47").Value = " 1．所見なし"
            oSheet.Range("P48").Value = "②．所見あり"
            oSheet.Range("P48").Font.Name = FONT_NAME_INPUT
        Else
            oSheet.Range("P47").Value = " 1．所見なし"
            oSheet.Range("P48").Value = " 2．所見あり"
        End If
        '左　4000Hz
        Dim d50 As String = Util.checkDBNullValue(rs.Fields("D50").Value)
        If d50 = "1" Then
            oSheet.Range("X47").Value = "①．所見なし"
            oSheet.Range("X47").Font.Name = FONT_NAME_INPUT
            oSheet.Range("X48").Value = " 2．所見あり"
        ElseIf d50 = "2" Then
            oSheet.Range("X47").Value = " 1．所見なし"
            oSheet.Range("X48").Value = "②．所見あり"
            oSheet.Range("X48").Font.Name = FONT_NAME_INPUT
        Else
            oSheet.Range("X47").Value = " 1．所見なし"
            oSheet.Range("X48").Value = " 2．所見あり"
        End If

        '血圧
        '指導区分
        oSheet.Range("C51").Value = Util.checkDBNullValue(rs.Fields("D52").Value)
        '最高血圧
        '1回目
        oSheet.Range("S49").Value = Util.checkDBNullValue(rs.Fields("D54").Value)
        '2回目
        oSheet.Range("S50").Value = Util.checkDBNullValue(rs.Fields("D56").Value)
        '最低血圧
        '1回目
        oSheet.Range("S52").Value = Util.checkDBNullValue(rs.Fields("D60").Value)
        '2回目
        oSheet.Range("S53").Value = Util.checkDBNullValue(rs.Fields("D62").Value)
        '採血時間
        Dim d65 As String = Util.checkDBNullValue(rs.Fields("D65").Value)
        If d65 = "1" Then
            oSheet.Range("AO13").Value = "①．食後１０時間未満"
            oSheet.Range("AO13").Font.Name = FONT_NAME_INPUT
            oSheet.Range("AW13").Value = "2．以上"
        ElseIf d65 = "2" Then
            oSheet.Range("AO13").Value = "1．食後１０時間未満"
            oSheet.Range("AW13").Value = "②．以上"
            oSheet.Range("AW13").Font.Name = FONT_NAME_INPUT
        Else
            oSheet.Range("AO13").Value = "1．食後１０時間未満"
            oSheet.Range("AW13").Value = "2．以上"
        End If

        '脂質
        '指導区分
        oSheet.Range("AE18").Value = Util.checkDBNullValue(rs.Fields("D67").Value)
        '総ｺﾚｽﾃﾛｰﾙ
        oSheet.Range("AW14").Value = Util.checkDBNullValue(rs.Fields("D69").Value)
        '中性脂肪
        oSheet.Range("AW17").Value = Util.checkDBNullValue(rs.Fields("D75").Value)
        'ＨＤＬ
        oSheet.Range("AW20").Value = Util.checkDBNullValue(rs.Fields("D81").Value)
        'ＬＤＬ
        oSheet.Range("AW23").Value = Util.checkDBNullValue(rs.Fields("D87").Value)

        '肝機能等
        '指導区分
        oSheet.Range("AE29").Value = Util.checkDBNullValue(rs.Fields("D89").Value)
        'ＧＯＴ
        oSheet.Range("AW24").Value = Util.checkDBNullValue(rs.Fields("D91").Value)
        'ＧＰＴ
        oSheet.Range("AW26").Value = Util.checkDBNullValue(rs.Fields("D95").Value)
        'γーＧＴＰ
        oSheet.Range("AW29").Value = Util.checkDBNullValue(rs.Fields("D101").Value)
        'ＡＬＰ
        oSheet.Range("AQ30").Value = Util.checkDBNullValue(rs.Fields("D103").Value)
        '総蛋白
        oSheet.Range("AW31").Value = Util.checkDBNullValue(rs.Fields("D107").Value)
        'ｱﾙﾌﾞﾐﾝ
        oSheet.Range("AW32").Value = Util.checkDBNullValue(rs.Fields("D109").Value)
        '総ﾋﾞﾘﾙﾋﾞﾝ
        oSheet.Range("AW33").Value = Util.checkDBNullValue(rs.Fields("D111").Value)
        'ＬＤＨ
        oSheet.Range("AQ34").Value = Util.checkDBNullValue(rs.Fields("D113").Value)
        'ｱﾐﾗｰｾﾞ
        oSheet.Range("AQ35").Value = Util.checkDBNullValue(rs.Fields("D117").Value)

        '血糖
        '指導区分
        oSheet.Range("AE40").Value = Util.checkDBNullValue(rs.Fields("D121").Value)
        '空腹時血糖
        oSheet.Range("AW39").Value = Util.checkDBNullValue(rs.Fields("D129").Value)
        'ﾍﾓｸﾞﾛﾋﾞﾝＡ１ｃ
        oSheet.Range("AW40").Value = Util.checkDBNullValue(rs.Fields("D151").Value)
        '尿糖
        Dim d161 As String = Util.checkDBNullValue(rs.Fields("D161").Value)
        If numDic.ContainsKey(d161) Then
            oSheet.Range("AW45").Value = numDic(d161)
        End If

        '尿酸
        '指導区分
        oSheet.Range("BG13").Value = Util.checkDBNullValue(rs.Fields("D163").Value)
        '尿酸
        oSheet.Range("BZ13").Value = Util.checkDBNullValue(rs.Fields("D165").Value)

        '尿一般・腎
        '指導区分
        oSheet.Range("BG16").Value = Util.checkDBNullValue(rs.Fields("D167").Value)
        '尿蛋白
        Dim d171 As String = Util.checkDBNullValue(rs.Fields("D171").Value)
        If numDic.ContainsKey(d171) Then
            oSheet.Range("BY15").Value = numDic(d171)
        End If
        '尿潜血
        Dim d173 As String = Util.checkDBNullValue(rs.Fields("D173").Value)
        If numDic.ContainsKey(d173) Then
            oSheet.Range("BY16").Value = numDic(d173)
        End If
        '血清ｸﾚｱﾁﾆﾝ
        oSheet.Range("BY17").Value = Util.checkDBNullValue(rs.Fields("D180").Value)
        '尿沈渣　赤血球
        oSheet.Range("BT18").Value = Util.checkDBNullValue(rs.Fields("D174").Value)
        '　　　　白血球
        oSheet.Range("BY18").Value = Util.checkDBNullValue(rs.Fields("D175").Value)
        '　　　　上皮細胞
        oSheet.Range("CE18").Value = Util.checkDBNullValue(rs.Fields("D176").Value)
        '　　　　円柱
        oSheet.Range("BS19").Value = Util.checkDBNullValue(rs.Fields("D177").Value)
        '　　　　その他
        oSheet.Range("BY19").Value = Util.checkDBNullValue(rs.Fields("D178").Value)

        '血液一般
        '指導区分
        oSheet.Range("BG23").Value = Util.checkDBNullValue(rs.Fields("D182").Value)
        'ﾍﾏﾄｸﾘｯﾄ
        oSheet.Range("BW20").Value = Util.checkDBNullValue(rs.Fields("D184").Value)
        '血色素量
        oSheet.Range("BW21").Value = Util.checkDBNullValue(rs.Fields("D186").Value)
        '赤血球数
        oSheet.Range("BW22").Value = Util.checkDBNullValue(rs.Fields("D188").Value)
        '白血球数
        oSheet.Range("BW23").Value = Util.checkDBNullValue(rs.Fields("D190").Value)
        '血小板数
        oSheet.Range("BW24").Value = Util.checkDBNullValue(rs.Fields("D192").Value)
        'Baso
        oSheet.Range("BS25").Value = Util.checkDBNullValue(rs.Fields("D194").Value)
        'Eosino
        oSheet.Range("BZ25").Value = Util.checkDBNullValue(rs.Fields("D196").Value)
        'Stab
        oSheet.Range("BS26").Value = Util.checkDBNullValue(rs.Fields("D198").Value)
        'Seg
        oSheet.Range("BX26").Value = Util.checkDBNullValue(rs.Fields("D200").Value)
        'Lympho
        oSheet.Range("BT27").Value = Util.checkDBNullValue(rs.Fields("D204").Value)
        'Mono
        oSheet.Range("BY27").Value = Util.checkDBNullValue(rs.Fields("D206").Value)
        'Other
        oSheet.Range("CD27").Value = Util.checkDBNullValue(rs.Fields("D208").Value)

        '心電図
        '指導区分
        oSheet.Range("BG30").Value = Util.checkDBNullValue(rs.Fields("D211").Value)
        '所見
        oSheet.Range("BS30").Value = Util.checkDBNullValue(rs.Fields("D213").Value)

        '胸部
        '指導区分
        oSheet.Range("BG36").Value = Util.checkDBNullValue(rs.Fields("D236").Value)
        '直接　間接
        Dim d237 As String = Util.checkDBNullValue(rs.Fields("D237").Value)
        If d237 = "1" Then
            oSheet.Range("BJ37").Value = "（直接）"
            '所見
            oSheet.Range("BS36").Value = Util.checkDBNullValue(rs.Fields("D238").Value)
        ElseIf d237 = "2" Then
            oSheet.Range("BJ37").Value = "（間接）"
            '所見
            oSheet.Range("BS36").Value = Util.checkDBNullValue(rs.Fields("D238").Value)
        End If


        '2枚目
        oSheet = objWorkBook.Worksheets("本人２改")
        '上の共通部分
        Dim xlPasteRange As Excel.Range = oSheet.Range("A1") 'ペースト先
        objWorkBook.Worksheets("本人１改").Rows("1:9").copy(xlPasteRange)
        oSheet.Range("BL3").Value = "２／２"

        '眼底
        '指導区分
        oSheet.Range("D15").Value = Util.checkDBNullValue(rs.Fields("D216").Value)
        'Ｋ．Ｗ．
        oSheet.Range("X13").Value = Util.checkDBNullValue(rs.Fields("D218").Value)
        'Scheie H
        oSheet.Range("X14").Value = Util.checkDBNullValue(rs.Fields("D220").Value)
        'Scheie S
        oSheet.Range("AH14").Value = Util.checkDBNullValue(rs.Fields("D222").Value)
        'SCOTT
        oSheet.Range("X15").Value = Util.checkDBNullValue(rs.Fields("D224").Value)
        '所見
        oSheet.Range("X16").Value = Util.checkDBNullValue(rs.Fields("D225").Value)
        '実施理由
        oSheet.Range("X18").Value = Util.checkDBNullValue(rs.Fields("D226").Value)

        '肺機能
        '指導区分
        oSheet.Range("D19").Value = Util.checkDBNullValue(rs.Fields("D228").Value)
        '肺活量
        oSheet.Range("AI19").Value = Util.checkDBNullValue(rs.Fields("D230").Value)
        '一秒量
        oSheet.Range("X20").Value = Util.checkDBNullValue(rs.Fields("D232").Value)
        '一秒率
        oSheet.Range("AI20").Value = Util.checkDBNullValue(rs.Fields("D234").Value)

        '胃部
        'Ｘ線
        '指導区分
        oSheet.Range("D22").Value = Util.checkDBNullValue(rs.Fields("D240").Value)
        '直接　間接
        Dim d241 As String = Util.checkDBNullValue(rs.Fields("D241").Value)
        If d241 = "1" Then
            oSheet.Range("G23").Value = "（直接）"
            '所見
            oSheet.Range("V22").Value = Util.checkDBNullValue(rs.Fields("D242").Value)
        ElseIf d241 = "2" Then
            oSheet.Range("G23").Value = "（間接）"
            '所見
            oSheet.Range("V22").Value = Util.checkDBNullValue(rs.Fields("D242").Value)
        End If
        '内視鏡
        '指導区分
        oSheet.Range("D26").Value = Util.checkDBNullValue(rs.Fields("D243").Value)
        '所見
        oSheet.Range("V26").Value = Util.checkDBNullValue(rs.Fields("D244").Value)

        '腹部
        '指導区分
        oSheet.Range("D30").Value = Util.checkDBNullValue(rs.Fields("D246").Value)
        '所見
        oSheet.Range("V30").Value = Util.checkDBNullValue(rs.Fields("D247").Value)

        '大腸
        '便潜血
        '指導区分
        oSheet.Range("D33").Value = Util.checkDBNullValue(rs.Fields("D249").Value)
        '1日目
        Dim d251 As String = Util.checkDBNullValue(rs.Fields("D251").Value)
        If d251 = "1" Then
            oSheet.Range("V33").Value = "（－）"
        ElseIf d251 = "3" Then
            oSheet.Range("V33").Value = "（＋）"
        End If
        '2日目
        Dim d253 As String = Util.checkDBNullValue(rs.Fields("D253").Value)
        If d253 = "1" Then
            oSheet.Range("AH33").Value = "（－）"
        ElseIf d253 = "3" Then
            oSheet.Range("AH33").Value = "（＋）"
        End If
        '直腸診
        '指導区分
        oSheet.Range("D35").Value = Util.checkDBNullValue(rs.Fields("D254").Value)
        '所見
        oSheet.Range("V35").Value = Util.checkDBNullValue(rs.Fields("D255").Value)

        '乳房
        '指導区分
        oSheet.Range("D41").Value = Util.checkDBNullValue(rs.Fields("D257").Value)
        '触診等所見
        oSheet.Range("V39").Value = Util.checkDBNullValue(rs.Fields("D258").Value)
        '乳房ｴｯｸｽ線所見
        oSheet.Range("V43").Value = Util.checkDBNullValue(rs.Fields("D260").Value)

        '子宮
        '指導区分
        oSheet.Range("D46").Value = Util.checkDBNullValue(rs.Fields("D262").Value)
        '異常ありなし
        Dim d263 As String = Util.checkDBNullValue(rs.Fields("D263").Value)
        If d263 = "1" Then
            oSheet.Range("U46").Value = "①．特に異常を認めず"
            oSheet.Range("U46").Font.Name = FONT_NAME_INPUT
            oSheet.Range("AG46").Value = "2．要精密検査"
        ElseIf d263 = "2" Then
            oSheet.Range("U46").Value = "1．特に異常を認めず"
            oSheet.Range("AG46").Value = "②．要精密検査"
            oSheet.Range("AG46").Font.Name = FONT_NAME_INPUT
        Else
            oSheet.Range("U46").Value = "1．特に異常を認めず"
            oSheet.Range("AG46").Value = "2．要精密検査"
        End If

        '肝炎
        'HBs抗原
        '指導区分
        oSheet.Range("D48").Value = Util.checkDBNullValue(rs.Fields("D265").Value)
        '結果
        Dim d267 As String = Util.checkDBNullValue(rs.Fields("D267").Value)
        If d267 = "1" Then
            oSheet.Range("T48").Value = "（－）"
        ElseIf d267 = "2" Then
            oSheet.Range("T48").Value = "（±）"
        ElseIf d267 = "3" Then
            oSheet.Range("T48").Value = "（＋）"
        End If
        'HCV
        '指導区分
        oSheet.Range("D51").Value = Util.checkDBNullValue(rs.Fields("D268").Value)
        'HCV抗体
        Dim d269 As String = Util.checkDBNullValue(rs.Fields("D269").Value)
        If d269 = "1" Then
            oSheet.Range("S49").Value = "①.Ｃ型肝炎ウイルスに感染していない可能性が極めて高い。"
            oSheet.Range("S49").Font.Name = FONT_NAME_INPUT
            oSheet.Range("S50").Value = "2.Ｃ型肝炎ウイルスに感染している可能性が極めて高い。"
            oSheet.Range("S51").Value = "3.要ＨＣＶ核酸増幅検査。"
        ElseIf d269 = "2" Then
            oSheet.Range("S49").Value = "1.Ｃ型肝炎ウイルスに感染していない可能性が極めて高い。"
            oSheet.Range("S50").Value = "②.Ｃ型肝炎ウイルスに感染している可能性が極めて高い。"
            oSheet.Range("S50").Font.Name = FONT_NAME_INPUT
            oSheet.Range("S51").Value = "3.要ＨＣＶ核酸増幅検査。"
        ElseIf d269 = "3" Then
            oSheet.Range("S49").Value = "1.Ｃ型肝炎ウイルスに感染していない可能性が極めて高い。"
            oSheet.Range("S50").Value = "2.Ｃ型肝炎ウイルスに感染している可能性が極めて高い。"
            oSheet.Range("S51").Value = "③.要ＨＣＶ核酸増幅検査。"
            oSheet.Range("S51").Font.Name = FONT_NAME_INPUT
        Else
            oSheet.Range("S49").Value = "1.Ｃ型肝炎ウイルスに感染していない可能性が極めて高い。"
            oSheet.Range("S50").Value = "2.Ｃ型肝炎ウイルスに感染している可能性が極めて高い。"
            oSheet.Range("S51").Value = "3.要ＨＣＶ核酸増幅検査。"
        End If
        'HCV核酸増幅検査
        '何もしないようなので何もしない
        'Dim d270 As String = Util.checkDBNullValue(rs.Fields("D270").Value)
        'If d270 = "1" Then

        'ElseIf d270 = "2" Then

        'Else

        'End If

        '総合所見
        '指導区分
        Dim d279 As String = Util.checkDBNullValue(rs.Fields("D279").Value)
        If d279 = "1" Then
            oSheet.Range("AW11").Value = "①．"
            oSheet.Range("AW11").Font.Name = FONT_NAME_INPUT
            oSheet.Range("AX11").Font.Name = FONT_NAME_INPUT
        ElseIf d279 = "2" Then
            oSheet.Range("AW12").Value = "②．"
            oSheet.Range("AW12").Font.Name = FONT_NAME_INPUT
            oSheet.Range("AX12").Font.Name = FONT_NAME_INPUT
        ElseIf d279 = "3" Then
            oSheet.Range("AW13").Value = "③．"
            oSheet.Range("AW13").Font.Name = FONT_NAME_INPUT
            oSheet.Range("AX13").Font.Name = FONT_NAME_INPUT
        ElseIf d279 = "4" Then
            oSheet.Range("AW14").Value = "④．"
            oSheet.Range("AW14").Font.Name = FONT_NAME_INPUT
            oSheet.Range("AX14").Font.Name = FONT_NAME_INPUT
        ElseIf d279 = "5" Then
            oSheet.Range("AW15").Value = "⑤．"
            oSheet.Range("AW15").Font.Name = FONT_NAME_INPUT
            oSheet.Range("AX15").Font.Name = FONT_NAME_INPUT
        ElseIf d279 = "6" Then
            oSheet.Range("AW16").Value = "⑥．"
            oSheet.Range("AW16").Font.Name = FONT_NAME_INPUT
            oSheet.Range("AX16").Font.Name = FONT_NAME_INPUT
        End If
        '注意事項・医師の判断
        '1行目
        oSheet.Range("AX19").Value = Util.checkDBNullValue(rs.Fields("D279a").Value)
        '2行目
        oSheet.Range("AX21").Value = Util.checkDBNullValue(rs.Fields("D279b").Value)
        '3行目
        oSheet.Range("AX23").Value = Util.checkDBNullValue(rs.Fields("D279c").Value)
        '4行目
        oSheet.Range("AX25").Value = Util.checkDBNullValue(rs.Fields("D279d").Value)
        '5行目
        oSheet.Range("AX27").Value = Util.checkDBNullValue(rs.Fields("D279e").Value)
        '6行目
        oSheet.Range("AX29").Value = Util.checkDBNullValue(rs.Fields("D279f").Value)

        'メタボ判定
        oSheet.Range("BD33").Value = Util.checkDBNullValue(rs.Fields("D283").Value)

        objExcel.Calculation = Excel.XlCalculation.xlCalculationAutomatic
        objExcel.ScreenUpdating = True

        '変更保存確認ダイアログ非表示
        objExcel.DisplayAlerts = False

        '印刷
        If printState = True Then
            objWorkBook.Worksheets({"本人１改", "本人２改"}).PrintOut()
        Else
            objExcel.Visible = True
            objWorkBook.Worksheets({"本人１改", "本人２改"}).PrintPreview(1)
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